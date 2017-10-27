using System;
using System.Threading.Tasks;
using TIK.Domain.Notifications;

namespace TIK.Applications.Notification.Commands.Emails
{
    public class EmailNotificationCommand : IEmailNotificationCommand
    {
        public EmailNotificationCommand()
        {
        }

        public void Send(EmailCommand command)
        {
           
        }
    }
}
