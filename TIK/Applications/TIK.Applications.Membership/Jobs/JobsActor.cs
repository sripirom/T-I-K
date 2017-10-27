using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Akka.Actor;
using TIK.Applications.Integration;
using TIK.Domain.Membership;
using TIK.Domain.SearchNews;

namespace TIK.Applications.Membership.Jobs
{
    public partial class JobsActor : ReceiveActor
    {
        private IList<Job> Jobs { get; set; }
        private IBatchPublisher BatchPublisher { get; set; }
        public JobsActor(IBatchPublisher batchPublisher)
        {
            this.BatchPublisher = batchPublisher;


            Receive<GetAllJobs>(_ => Sender.Tell(new ReadOnlyCollection<Job>(BatchPublisher.GetAllJobs().ToList())));
            Receive<RequestJob>(m => Sender.Tell(UpdateStockAction(m)));
        }

        public JobEvent UpdateStockAction(RequestJob message)
        {
           // var job = this.Jobs
             //   .FirstOrDefault(p => p.Id == message.JobId);

            var job = BatchPublisher.SearchNews(new CriteriaSearchNews
                    { Id = message.JobId, Target = message.Application });
            if (job is Job)
            {
                
                //if (job.InStock + message.AmountChanged >= 0)
                //{
                    //job.InStock += message.AmountChanged;
                    return new JobUpdated(job);
               /* }
                else
                {
                    return new InsuffientStock();
                }
                */

            }

            return new JobNotFound();
        }
    }
}
