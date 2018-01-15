using System;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Event;
using TIK.Applications.Online.Jobs;
using TIK.Domain.Jobs;

namespace TIK.Applications.Online.BackLogs
{
    public partial class BackLogActor : ReceiveActor
    {
        private readonly ILoggingAdapter _logger = Context.GetLogger();

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
                existingBackLogItemWithJob.Procedure = command;
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
                    JobId = jobToAdd.Id.ToString(),
                    Title = command,
                    Application = jobToAdd.Application,
                    //PricePerUnit = jobToAdd.PricePerUnit,
                    Procedure = jobToAdd.Procedure
                });

                return new ItemAdded(backLogItemId);
            }
        }

        #region Lifecycle hooks

        protected override void PreStart()
        {
            _logger.Debug("BackLogActor PreStart");
        }

        protected override void PostStop()
        {
            _logger.Debug("BackLogActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            _logger.Debug("BackLogActor PreRestart because {Reason}", reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            _logger.Debug("BackLogActor PostRestart because {Reason}", reason);

            base.PostRestart(reason);
        }
        #endregion
    }
}
