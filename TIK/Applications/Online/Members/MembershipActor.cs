using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka.Event;
using TIK.Core.Application.Messaging;
using TIK.Domain.Membership;

namespace TIK.Applications.Online.Members
{
    public class MembershipActor : ReceiveActor
    {
        private readonly ILoggingAdapter _logger = Context.GetLogger();

        private readonly IMemberRepository _memberRepository;
        public MembershipActor(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;


            ReceiveAny(m => 
            {
                if (m is MessageWithMemberId)
                {
                    if(m is MemberActor.ActiveMember)
                    {
                        var envelope = m as MemberActor.ActiveMember;
                        ActiveMember(envelope);
                    }else
                    {
                        var envelope = m as MessageWithMemberId;
                        var memberActor = Context.Child(envelope.MemberId.ToString());

                        memberActor.Forward(m);
                    }
                }
            });
        }
       
        private void ActiveMember(MemberActor.ActiveMember message)
        {
            var member = _memberRepository.Get(message.MemberId);
            if (member != null)
            {
               

                var memberActor = Context.Child(message.MemberId.ToString()) is Nobody ?
                                         Context.ActorOf(MemberActor.Props(member), message.MemberId.ToString()) :
                                         Context.Child(message.MemberId.ToString());

                memberActor.Forward(message);

                foreach (var memberRef in Context.GetChildren())
                {
                    memberRef.Tell(new MemberActor.RefreshMemberStatusMessage(message.MemberId), Sender);
                }
            }


        }

        public static Props SelfProps()
        {
            return Akka.Actor.Props.Create(typeof(MembershipActor));
        }

        #region Lifecycle hooks

        protected override void PreStart()
        {
            _logger.Debug("MembershipActor PreStart");
        }

        protected override void PostStop()
        {
            _logger.Debug("MembershipActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            _logger.Debug("MembershipActor PreRestart because {Reason}", reason);

            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            _logger.Debug("MembershipActor PostRestart because {Reason}", reason);

            base.PostRestart(reason);
        }
        #endregion
    }
}
