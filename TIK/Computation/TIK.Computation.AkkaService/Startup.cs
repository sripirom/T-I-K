using System;
using System.IO;
using Akka.Actor;
using Microsoft.AspNetCore.Builder;
using Serilog;
using TIK.Applications.Membership.Jobs;
using TIK.Applications.Membership.JobSlots;
using TIK.Applications.Membership.Members;
using TIK.Core.Logging;
using TIK.Integration.WebApi.Batch;

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
                var huconConfig = Path.Combine(Directory.GetCurrentDirectory(), "Hucon.txt");
                var config = HoconLoader.FromFile(huconConfig);
                ActorSystemInstance = ActorSystem.Create("MembershipSystem", config);

                var provider = new JobActorProvider(actorSystem: ActorSystemInstance, batchPublisher: new BatchPublisher(new Uri("http://localhost:5102")));
                var jobsActor = provider.Get();
                //var jobSlotsActorInstance = ActorSystemInstance.ActorOf(JobSlotsActor.Props(provider.Get()), "jobSlots");
                var jobsSlotProvider = new JobSlotsActorProvider(ActorSystemInstance, provider);
                    var memberController = ActorSystemInstance.ActorOf(Props.Create(typeof(MemberControllerActor), jobsSlotProvider), "MemberController");

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
