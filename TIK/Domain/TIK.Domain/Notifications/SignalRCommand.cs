using System;
using TIK.Core.Domain;

namespace TIK.Domain.Notifications
{
    public class SignalRCommand : BaseModel<Int32>
    {
        public SignalRCommand()
        {
        }

        public string Message { get; set; }
    }
}
