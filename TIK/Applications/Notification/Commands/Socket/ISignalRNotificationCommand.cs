using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Hangfire;
using TIK.Core.Application;
using TIK.Domain.Notifications;

namespace TIK.Applications.Notification.Commands.Socket
{
    public interface ISignalRNotificationCommand : IAppService
    {
        [DisplayName("Send SignalR Notification")]
        //[Queue("SignalRNoti")]
        void Send(SignalRCommand command);
    }
}
