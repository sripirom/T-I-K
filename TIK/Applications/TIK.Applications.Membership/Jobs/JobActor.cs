using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Akka.Actor;
using TIK.Integration.Batch;
using TIK.Domain.Membership;
using TIK.Domain.SearchNews;

namespace TIK.Applications.Membership.Jobs
{
    public partial class JobActor : ReceiveActor
    {
        private IBatchPublisher BatchPublisher { get; set; }

        public JobActor(IBatchPublisher batchPublisher)
        {
            this.BatchPublisher = batchPublisher;


            //Receive<GetAllJobs>(_ => Sender.Tell(new ReadOnlyCollection<Job>(BatchPublisher.GetAllJobs().ToList())));
            Receive<RequestJob>(m => Sender.Tell(RequestJobAction(m)));


        }

        public JobEvent RequestJobAction(RequestJob message)
        {
            
            var jobTask = BatchPublisher.SearchNews(new CriteriaSearchNews
                    { Target = message.Application });
            //jobTask.Wait();
            var job = jobTask.Result;
            if (job is Job)
            {
                return new JobUpdated(job);
            }

            return new JobNotFound();
        }
    }
}
