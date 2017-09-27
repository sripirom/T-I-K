using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace TIK.ProcessService.Hangfire.DataTransformation.Hubs
{
    public class DataTransformationHUB : Hub
    {
        public DataTransformationHUB()
        {
        }
        public Task FileStreamCallBack(string taskId, string who, byte[] message)
        {
            return Clients.All.InvokeAsync("FileStreamCallBack", taskId, who, message);
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

    }
}
