using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using TIK.Domain.TheSet;
using TIK.Integration.Online;

namespace TIK.Integration.SignalR.Online
{
    public class StockDiscussionPublisher : IStockDiscussionPublisher
    {
        private readonly IEndpointDiscovery _dns;
        private readonly string _serviceName;
        private readonly string _stockDiscussion = "{0}/stockDiscussion";
        public StockDiscussionPublisher(string serviceName, IEndpointDiscovery dns)
        {
            _serviceName = serviceName;

            _dns = dns ?? throw new ArgumentNullException(nameof(dns));
        }


        public async Task<Boolean> AddStockDiscussion(Int32 stockId, DiscussionItem discussionItem)
        {
            var uri = _dns.Resolve(_serviceName).Result;

            string baseUrl = string.Format(_stockDiscussion, uri.OriginalString);

            var loggerFactory = new LoggerFactory();
            var logger = loggerFactory.CreateLogger<StockDiscussionPublisher>();


            var connection = await ConnectAsync(baseUrl);
            try
            {
                var cts = new CancellationTokenSource();

                await connection.InvokeAsync<DiscussionItem>("addStockDiscussion", stockId, discussionItem, cts.Token);

                return true;
            }
            catch (AggregateException aex) when (aex.InnerExceptions.All(e => e is OperationCanceledException))
            {
                return false;
            }
            catch (OperationCanceledException)
            {
                return false;
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
