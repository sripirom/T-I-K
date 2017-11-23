using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Hangfire;
using TIK.Core.Application;
using TIK.Domain.Notifications;

namespace TIK.Applications.Notification.Commands.Sms
{
    public interface ISmsNotificationCommand : IAppService
    {
        [DisplayName("Send SMS Notification")]
        [Queue("SmsNoti")]
        void Send(SmsCommand command);
    }
}
