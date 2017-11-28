using Akka.Actor;

using TIK.Applications.Messaging;

namespace TIK.Applications.Online.BackLogs
{
    public class BackLogsActor : ReceiveActor
    {
        private IActorRef JobActor { get; }

        public BackLogsActor(IActorRef jobActor)
        {
            this.JobActor = jobActor;

            ReceiveAny(m => {
                if (m is MessageWithMemberId)
                {
                    var envelope = m as MessageWithMemberId;
                    var backLogActor = Context.Child(envelope.MemberId.ToString()) is Nobody ?
                                             Context.ActorOf(BackLogActor.Props(this.JobActor), envelope.MemberId.ToString()) :
                                             Context.Child(envelope.MemberId.ToString());
                    backLogActor.Forward(m);
                }
            });
        }
        public static Props Props(IActorRef jobsActor)
        {
            return Akka.Actor.Props.Create(() => new BackLogsActor(jobsActor));
        }
    }
}
