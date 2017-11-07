using System;
using System.Threading.Tasks;
using TIK.Domain.Notifications;

namespace TIK.Applications.Notification.Commands.Socket
{
    public interface ISignalRNotificationCommand
    {
        void Send(SignalRCommand command);
    }
}
