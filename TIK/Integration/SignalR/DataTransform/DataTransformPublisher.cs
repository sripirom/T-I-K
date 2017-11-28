using System;
using System.Threading.Tasks;
using System.Linq;
using TIK.Applications.DataTransformation;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Sockets.Client;
using System.Threading;
using System.Text;
using TIK.Integration.DataTransformation;

namespace TIK.Integration.SignalR.DataTransform
{
    public class DataTransformPublisher : IDataTransformPublisher
    {
        public DataTransformPublisher() 
        {
        }


        public async Task CallBackResult(string filename, string state, byte[] dataStream)
        {
            string baseUrl = "http://localhost:5103/hangfireSR/DataTransformation";

            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger<DataTransformPublisher>();


            var connection = await ConnectAsync(baseUrl);
            try
            {
                var cts = new CancellationTokenSource();

                await connection.InvokeAsync<object>("FileStreamCallBack", filename, state, dataStream,cts.Token);
           

            }
            catch (AggregateException aex) when (aex.InnerExceptions.All(e => e is OperationCanceledException))
            {
            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                await connection.DisposeAsync();
            }

        }

        private static async Task<HubConnection> ConnectAsync(string baseUrl)
        {
            // Keep trying to until we can start
            while (true)
            {
                var connection = new HubConnectionBuilder()
                                .WithUrl(baseUrl)
                                .WithConsoleLogger(LogLevel.Trace)
                                .Build();
                try
                {
                    await connection.StartAsync();
                    return connection;
                }
                catch (Exception)
                {
                    await Task.Delay(1000);
                }
            }
        }
    }
}
