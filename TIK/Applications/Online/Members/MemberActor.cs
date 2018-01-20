using System;
using Akka.Actor;
using Akka.Event;
using TIK.Core.Application.Messaging;
using TIK.Domain.Membership;

namespace TIK.Applications.Online.Members
{
    public partial class MemberActor : ReceiveActor
    {
        private readonly ILoggingAdapter _logger = Context.GetLogger();

        private Member _memberInfo;

        public MemberActor(Member memberInfo)
        {
            _memberInfo = memberInfo;

            Receive<ActiveMember>(message =>
            {
                if(_memberInfo != null)
                {
                    Sender.Tell(_memberInfo);
                }else
                {
                    Member member = new Member();
                    member.Id = message.MemberId;
                    member.IsActive = false;
                    Sender.Tell(member);
                }
            });

            Receive<RefreshMemberStatusMessage>(message =>
            {
                Sender.Tell(_memberInfo);
            });

        }


        public static Props Props(Member memberInfo)
        {
            return Akka.Actor.Props.Create(() => new MemberActor(memberInfo));
        }

        #region Lifecycle hooks

        protected override void PreStart()
        {
            _logger.Debug("MemberActor PreStart");
        }

        protected override void PostStop()
        {
            _logger.Debug("MemberActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            _logger.Debug("MemberActor PreRestart because {Reason}", reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            _logger.Debug("MemberActor PostRestart because {Reason}", reason);

            base.PostRestart(reason);
        }
        #endregion

    }
}
