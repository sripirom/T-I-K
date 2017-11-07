using System;
using System.Threading.Tasks;
using Akka.Actor;

using TIK.Applications.Online.Jobs;
using TIK.Domain.Jobs;

namespace TIK.Applications.Online.BackLogs
{
    public partial class BackLogActor : ReceiveActor
    {
        public BackLog BackLogState { get; set; }
        private IActorRef JobsActorRef { get; set; }
        public BackLogActor(IActorRef jobsActor)
        {
            this.BackLogState = new BackLog();
            this.JobsActorRef = jobsActor;

            Receive<GetBackLog>(_ => Sender.Tell(this.BackLogState));
            ReceiveAsync<AddItemToBackLog>(m => AddItemToBackLogAction(m).PipeTo(Sender), m => m.Command != "");
            Receive<RemoveItemFromBackLog>(m => Sender.Tell(RemoveItemToBackLogAction(m)));
        }

        public static Props Props(IActorRef jobsActor)
        {
            return Akka.Actor.Props.Create(() => new BackLogActor(jobsActor));
        }

        public async Task<BackLogEvent> AddItemToBackLogAction(AddItemToBackLog message)
        {
            var jobActorResult = await this.JobsActorRef.Ask<JobsActor.JobEvent>(
                new JobsActor.AddJob(
                    jobId: message.JobId,
                    command: message.Command
                )
            );

            if (jobActorResult is JobsActor.JobAdded)
            {
                var job = ((JobsActor.JobAdded)jobActorResult).Job;
                return AddToBackLog(job, message.Command) as ItemAdded;
            }
            else if (jobActorResult is JobsActor.JobNotFound)
            {
                return new JobNotFound();
            }
            else if (jobActorResult is JobsActor.InsuffientJob)
            {
                return new NotInStock();
            }
            else
            {
                throw new NotImplementedException($"Unknown response: {jobActorResult.GetType().ToString()}");
            }
        }

        public BackLogEvent RemoveItemToBackLogAction(RemoveItemFromBackLog message)
        {
            var backLogItem = this.BackLogState.Items.Find(item => item.Id == message.BackLogItemId);
            if (backLogItem is BackLogItem)
            {
                this.BackLogState.Items.Remove(backLogItem);
                return new ItemRemoved();
            }
            else
            {
                return new ItemNotFound();
            }
        }

        private ItemAdded AddToBackLog(Job jobToAdd, string command)
        {
            var existingBackLogItemWithJob = this.BackLogState.Items.Find(item => item.JobId == jobToAdd.Id);
            if (existingBackLogItemWithJob is BackLogItem)
            {
                // Add to existing backLog item
                existingBackLogItemWithJob.Command = command;
                return new ItemAdded(
                    backLogItemId: existingBackLogItemWithJob.Id
                );
            }
            else
            {
                // Create a new backLog item
                var backLogItemId = Guid.NewGuid();
                this.BackLogState.Items.Add(new BackLogItem {
                    Id = backLogItemId,
                    JobId = jobToAdd.Id,
                    Title = jobToAdd.Application,
                    Brand = jobToAdd.Procedure,
                    PricePerUnit = jobToAdd.PricePerUnit,
                    Command = command
                });

                return new ItemAdded(backLogItemId);
            }
        }
    }
}
