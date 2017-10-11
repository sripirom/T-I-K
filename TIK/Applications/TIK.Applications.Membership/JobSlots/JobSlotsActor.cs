using Akka.Actor;

using TIK.Applications.Messaging;

namespace TIK.Applications.Membership.JobSlots
{
    public class JobSlotsActor : ReceiveActor
    {
        private IActorRef JobActor { get; }

        public JobSlotsActor(IActorRef jobActor)
        {
            this.JobActor = jobActor;

            ReceiveAny(m => {
                if (m is MessageWithMemberId)
                {
                    var envelope = m as MessageWithMemberId;
                    var basketActor = Context.Child(envelope.MemberId.ToString()) is Nobody ?
                        Context.ActorOf(JobSlotActor.Props(this.JobActor), envelope.MemberId.ToString()) :
                        Context.Child(envelope.MemberId.ToString());
                    basketActor.Forward(m);
                }
            });
        }
        public static Props Props(IActorRef jobsActor)
        {
            return Akka.Actor.Props.Create(() => new JobSlotsActor(jobsActor));
        }
    }
}
