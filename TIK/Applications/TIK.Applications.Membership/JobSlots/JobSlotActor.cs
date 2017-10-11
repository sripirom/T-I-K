using System;
using System.Threading.Tasks;
using Akka.Actor;

using TIK.Applications.Membership.Jobs;

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
            ReceiveAsync<AddItemToJobSlot>(m => AddItemToJobSlotAction(m).PipeTo(Sender), m => m.Amount > 0);
            Receive<RemoveItemFromJobSlot>(m => Sender.Tell(RemoveItemToJobSlotAction(m)));
        }

        public static Props Props(IActorRef jobsActor)
        {
            return Akka.Actor.Props.Create(() => new JobSlotActor(jobsActor));
        }

        public async Task<JobSlotEvent> AddItemToJobSlotAction(AddItemToJobSlot message)
        {
            var jobActorResult = await this.JobsActorRef.Ask<JobsActor.JobEvent>(
                new JobsActor.UpdateStock
                (
                    jobId: message.JobId,
                    amountChanged: -message.Amount
                )
            );

            if (jobActorResult is JobsActor.StockUpdated)
            {
                var job = ((JobsActor.StockUpdated)jobActorResult).Job;
                return AddToJobSlot(job, message.Amount) as ItemAdded;
            }
            else if (jobActorResult is JobsActor.JobNotFound)
            {
                return new JobNotFound();
            }
            else if (jobActorResult is JobsActor.InsuffientStock)
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

        private ItemAdded AddToJobSlot(Job jobToAdd, int amount)
        {
            var existingJobSlotItemWithJob = this.JobSlotState.Items.Find(item => item.JobId == jobToAdd.Id);
            if (existingJobSlotItemWithJob is JobItem)
            {
                // Add to existing basket item
                existingJobSlotItemWithJob.Amount += amount;
                return new ItemAdded(
                    jobSlotItemId: existingJobSlotItemWithJob.Id
                );
            }
            else
            {
                // Create a new basket item
                var jobItemId = Guid.NewGuid();
                this.JobSlotState.Items.Add(new JobItem {
                    Id = jobItemId,
                    JobId = jobToAdd.Id,
                    Title = jobToAdd.Title,
                    Brand = jobToAdd.Brand,
                    PricePerUnit = jobToAdd.PricePerUnit,
                    Amount = amount
                });

                return new ItemAdded(jobItemId);
            }
        }
    }
}
