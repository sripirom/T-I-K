using Akka.Actor;

using TIK.Applications.Messaging;

namespace TIK.Applications.Membership.JobSlots
{
    public class JobSlotsActor : ReceiveActor
    {
        private IActorRef JobsActor { get; }

        public JobSlotsActor(IActorRef jobsActor)
        {
            this.JobsActor = jobsActor;

            ReceiveAny(m => {
                if (m is MessageWithMemberId)
                {
                    var envelope = m as MessageWithMemberId;
                    var jobSlotActor = Context.Child(envelope.MemberId.ToString()) is Nobody ?
                        Context.ActorOf(JobSlotActor.Props(this.JobsActor), envelope.MemberId.ToString()) :
                        Context.Child(envelope.MemberId.ToString());
                    jobSlotActor.Forward(m);
                }
            });
        }
        public static Props Props(IActorRef jobsActor)
        {
            return Akka.Actor.Props.Create(() => new JobSlotsActor(jobsActor));
        }
    }
}
