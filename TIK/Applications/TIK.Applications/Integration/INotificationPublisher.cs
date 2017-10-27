using System;
using System.Threading.Tasks;

namespace TIK.Applications.Integration
{
    public interface INotificationPublisher
    {
        Task SendEmail();
        Task SendSms();
        Task SendSocket();
    } 
}
