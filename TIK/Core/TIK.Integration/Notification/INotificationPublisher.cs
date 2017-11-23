using System;
using System.Threading.Tasks;

namespace TIK.Integration.Notification
{
    public interface INotificationPublisher
    {
        Task SendEmail();
        Task SendSms();
        Task SendSocket();
    } 
}
