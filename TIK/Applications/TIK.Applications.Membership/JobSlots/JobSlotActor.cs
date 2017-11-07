using System;
using System.Threading.Tasks;
using Akka.Actor;

using TIK.Applications.Membership.Jobs;
using TIK.Domain.Membership;

namespace TIK.Applications.Membership.JobSlots
{
    public partial class JobSlotActor : ReceiveActor
    {
        public JobSlot JobSlotState { get; set; }
        private IActorRef JobsActorRef { get; set; }

        public JobSlotActor(IActorRef jobsActor)
        {
            this.JobSlotState = new JobSlot();
            this.JobsActorRef = jobsActor;

            Receive<GetJobSlot>(_ => Sender.Tell(this.JobSlotState));
            ReceiveAsync<AddItemToJobSlot>(m => AddItemToJobSlotAction(m).PipeTo(Sender), m => m.Application != "");
            Receive<RemoveItemFromJobSlot>(m => Sender.Tell(RemoveItemToJobSlotAction(m)));
        }
       // 11 - 26 huton 
        public static Props Props(IActorRef jobsActor)
        {
            return Akka.Actor.Props.Create(() => new JobSlotActor(jobsActor));
        }

        public async Task<JobSlotEvent> AddItemToJobSlotAction(AddItemToJobSlot message)
        {
            var jobActorResult = await this.JobsActorRef.Ask<JobActor.JobEvent>(
                new JobActor.RequestJob
                (
                    jobId: message.JobId ,
                    application: message.Application,
                    procedure: message.Procedure
                  
                )
            );

            if (jobActorResult is JobActor.JobUpdated)
            {
                var job = ((JobActor.JobUpdated)jobActorResult).Job;
                return AddToJobSlot(job, message.Application) as ItemAdded;
            }
            else if (jobActorResult is JobActor.JobNotFound)
            {
                return new JobNotFound();
            }
            else if (jobActorResult is JobActor.InsuffientStock)
            {
                return new NotInStock();
            }
            else
            {
                throw new NotImplementedException($"Unknown response: {jobActorResult.GetType().ToString()}");
            }
        }

        public JobSlotEvent RemoveItemToJobSlotAction(RemoveItemFromJobSlot message)
        {
            var jobSlotItem = this.JobSlotState.Items.Find(item => item.Id == message.JobSlotItemId);
            if (jobSlotItem is JobItem)
            {
                this.JobSlotState.Items.Remove(jobSlotItem);
                return new ItemRemoved();
            }
            else
            {
                return new ItemNotFound();
            }
        }

        private ItemAdded AddToJobSlot(Job jobToAdd, string application)
        {
            //var existingJobSlotItemWithJob = this.JobSlotState.Items.Find(item => item.JobId == jobToAdd.Id);

            // Create a new job item
            var jobItemId = Guid.NewGuid();
            this.JobSlotState.Items.Add(new JobItem {
                Id = jobItemId,
                JobId = jobToAdd.Id,
                Application = jobToAdd.Application,
                Procedure = jobToAdd.Procedure,
                Created = jobToAdd.Created
            });

            return new ItemAdded(jobItemId);

        }
    }
}
