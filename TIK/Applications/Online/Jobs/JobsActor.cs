using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Akka.Actor;
using TIK.Domain.Jobs;
using TIK.Domain.SearchNews;
using TIK.Integration.Batch;

namespace TIK.Applications.Online.Jobs
{
    public partial class JobsActor : ReceiveActor
    {
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
    }
}
