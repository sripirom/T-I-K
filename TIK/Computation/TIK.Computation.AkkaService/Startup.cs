using System;
using System.IO;
using Akka.Actor;
using Microsoft.AspNetCore.Builder;
using Serilog;
using TIK.Applications.Online.BackLogs;
using TIK.Applications.Online.Jobs;
using TIK.Applications.Online.Members;
using TIK.Core.Logging;
using TIK.Domain.Membership;
using TIK.Integration.Batch;
using TIK.Integration.WebApi.Batch;
using TIK.Persistance.ElasticSearch.Mocks;

namespace TIK.Computation.AkkaService
{
    public class Startup
    {
        private static ILog _logger = LogProvider.For<Program>();


        private static AkkaStateService ActorSystemInstance;

        public void Configure(IApplicationBuilder app)
        {
            Log.Logger = new LoggerConfiguration()
                      .MinimumLevel.Verbose()
                      .WriteTo.LiterateConsole()
                      .WriteTo.RollingFile("logs\\log-{Date}.txt")
                      .CreateLogger();

            ActorSystemInstance = new AkkaStateService();



            ActorSystemInstance.Start();

        
        }
    }

    public class AkkaStateService
    {
        private ActorSystem ActorSystemInstance;

        public void Start()
        {
            try
            {
                string host = @"127.0.0.1:5301";

                var huconConfig = Path.Combine(Directory.GetCurrentDirectory(), "Hucon.txt");
                var config = HoconLoader.FromFile(huconConfig);
                ActorSystemInstance = ActorSystem.Create("OnlineSystem", config);
                IMemberRepository memberRepository = new MockMemberRepository();
                IBatchPublisher batchPublisher = new BatchPublisher(new Uri("http://localhost:5102/"));

                var memberController = MemberActorProvider.CreateInstance(ActorSystemInstance, memberRepository);
                var jobsActorProvider = JobsActorProvider.CreateInstance(ActorSystemInstance, batchPublisher);
                var backLogsActorProvider = BackLogsActorProvider.CreateInstance(ActorSystemInstance, new JobsActorProvider(ActorSystemInstance, host));

            } 
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Stop()
        {
            ActorSystemInstance.Terminate().Wait(TimeSpan.FromSeconds(2));
        }
    }
}
