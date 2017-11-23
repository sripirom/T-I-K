using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Hangfire;
using TIK.Core.Application;
using TIK.Domain.Notifications;

namespace TIK.Applications.Notification.Commands
{
    public interface IEmailNotificationCommand : IAppService
    {
        [DisplayName("Send Email Notification")]
        [Queue("EmailNoti")]
        void Send(EmailCommand command);
    }
}
