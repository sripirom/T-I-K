using System;
using Akka.Actor;
using Akka.Event;
using TIK.Applications.Messaging;

namespace TIK.Applications.Online.BackLogs
{
    public class BackLogsActor : ReceiveActor
    {
        private readonly ILoggingAdapter _logger = Context.GetLogger();

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

        #region Lifecycle hooks

        protected override void PreStart()
        {
            _logger.Debug("BackLogsActor PreStart");
        }

        protected override void PostStop()
        {
            _logger.Debug("BackLogsActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            _logger.Debug("BackLogsActor PreRestart because {Reason}", reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            _logger.Debug("BackLogsActor PostRestart because {Reason}", reason);

            base.PostRestart(reason);
        }
        #endregion
    }
}
