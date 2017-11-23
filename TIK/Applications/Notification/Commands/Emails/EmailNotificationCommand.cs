using System;
using System.Threading.Tasks;
using TIK.Core.Application;
using TIK.Domain.Notifications;

namespace TIK.Applications.Notification.Commands.Emails
{
    public class EmailNotificationCommand :BaseAppService,  IEmailNotificationCommand
    {
        public EmailNotificationCommand()
        {
        }

        public void Send(EmailCommand command)
        {
           
        }
    }
}
