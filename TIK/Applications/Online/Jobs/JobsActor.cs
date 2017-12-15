using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Akka.Actor;
using Akka.Event;
using TIK.Domain.Jobs;
using TIK.Domain.SearchNews;
using TIK.Integration.Batch;

namespace TIK.Applications.Online.Jobs
{
    public partial class JobsActor : ReceiveActor
    {
        private readonly ILoggingAdapter _logger = Context.GetLogger();

        private IList<Job> Jobs { get; set; }
        private IBatchPublisher BatchPublisher { get; }

        public JobsActor(IList<Job> jobs, IBatchPublisher batchPublisher)
        {
            this.Jobs = jobs;
            BatchPublisher = batchPublisher;

            Receive<GetAllJobs>(_ => Sender.Tell(new ReadOnlyCollection<Job>(this.Jobs)));
            Receive<AddJob>(m => Sender.Tell(AddJobAction(m)));
        }

        public JobEvent AddJobAction(AddJob message)
        {

            var jobTask = BatchPublisher.SearchNews(new CriteriaSearchNews
            { Target = message.Command });
  
            var job = jobTask.Result;
  

            //var job = this.Jobs
                //.FirstOrDefault(p => p.Id == message.JobId);

            if (job is Job)
            {
                //if (job.InQueue + message.Command >= 0)
                //{
                    job.InQueue++;
                    return new JobAdded(job);
                //}
                //else
                //{
                  //  return new InsuffientJob();
                //}
            }

            return new JobNotFound();
        }

        #region Lifecycle hooks

        protected override void PreStart()
        {
            _logger.Debug("JobsActor PreStart");
        }

        protected override void PostStop()
        {
            _logger.Debug("JobsActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            _logger.Debug("JobsActor PreRestart because {Reason}", reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            _logger.Debug("JobsActor PostRestart because {Reason}", reason);

            base.PostRestart(reason);
        }
        #endregion
    }
}
