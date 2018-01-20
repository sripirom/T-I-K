using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TIK.Core.Application;
using TIK.Domain.Notifications;

namespace TIK.Applications.Notification.Commands.Socket
{
    public class SignalRNotificationCommand :BaseAppService,  ISignalRNotificationCommand
    {
        //private string _endpoint = "localhost:5100";

        public SignalRNotificationCommand()
        {
            //_endpoint = "localhost:5100";//endpoint;
        }

        public void Send(SignalRCommand command)
        {
            //RunWebSockets(command.Message).GetAwaiter().GetResult();
        }

        /*
        private async Task RunWebSockets(string message)
        {

            var ws = new ClientWebSocket();

            try
            {
                await ws.ConnectAsync(new Uri(_endpoint), CancellationToken.None);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }


            Console.WriteLine("Connected");

            var sending = Task.Run(async () =>
            {
 
                var bytes = Encoding.UTF8.GetBytes(message);
                await ws.SendAsync(new ArraySegment<byte>(bytes), WebSocketMessageType.Text, endOfMessage: true, cancellationToken: CancellationToken.None);


                await ws.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
            });


            await Task.WhenAll(sending);
        }
        private static async Task Receiving(ClientWebSocket ws)
        {
            var buffer = new byte[2048];

            while (true)
            {
                var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Text)
                {
                    Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, result.Count));
                }
                else if (result.MessageType == WebSocketMessageType.Binary)
                {
                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    await ws.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                    break;
                }

            }
        }
        */
    }
}
