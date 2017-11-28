using System;
using System.Threading.Tasks;
using TIK.Core.Application;
using TIK.Domain.Notifications;

namespace TIK.Applications.Notification.Commands.Sms
{
    public class SmsNotificationCommand :BaseAppService,  ISmsNotificationCommand
    {
        public SmsNotificationCommand()
        {
        }

        public void Send(SmsCommand command)
        {
           
        }
    }
}
