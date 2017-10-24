using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Akka.Actor;

namespace TIK.Applications.Membership.Jobs
{
    public partial class JobsActor : ReceiveActor
    {
        private IList<Job> Jobs { get; set; }
        public JobsActor(IList<Job> jobs)
        {
            this.Jobs = jobs;

            Receive<GetAllJobs>(_ => Sender.Tell(new ReadOnlyCollection<Job>(this.Jobs)));
            Receive<UpdateJob>(m => Sender.Tell(UpdateStockAction(m)));
        }

        public JobEvent UpdateStockAction(UpdateJob message)
        {
            var job = this.Jobs
                .FirstOrDefault(p => p.Id == message.JobId);

            if (job is Job)
            {
                if (job.InStock + message.AmountChanged >= 0)
                {
                    job.InStock += message.AmountChanged;
                    return new JobUpdated(job);
                }
                else
                {
                    return new InsuffientStock();
                }
            }

            return new JobNotFound();
        }
    }
}
