using System;
using TIK.Core.Domain;

namespace TIK.Domain.Notifications
{
    public class SignalRCommand : BaseModel<string>
    {
        public SignalRCommand()
        {
        }

        public string Message { get; set; }
    }
}
