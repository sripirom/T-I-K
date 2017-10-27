using System;
using System.Threading.Tasks;
using TIK.Domain.Notifications;

namespace TIK.Applications.Notification.Commands.Sms
{
    public interface ISmsNotificationCommand
    {
        void Send(SmsCommand command);
    }
}
