using System;
using System.Threading.Tasks;
using TIK.Integration.Notification;

namespace TIK.Integration.SignalR.Notification
{
    public class NotificationPublisher : INotificationPublisher
    {
        public NotificationPublisher()
        {
        }

        public Task SendEmail()
        {
            throw new NotImplementedException();
        }

        public Task SendSms()
        {
            throw new NotImplementedException();
        }

        public Task SendSocket()
        {
            throw new NotImplementedException();
        }
    }
}
