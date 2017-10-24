using System;
using TIK.Domain.Notifications;

namespace TIK.Applications.Notification.Commands
{
    public interface IEmailNotificationCommand
    {
        void Send(EmailCommand command);
    }
}
