using System;
using System.Reflection;
using Consul;

namespace TIK.Core.Governance
{
    public class ConsulProvider
    {
        private ConsulClient _client;
        private string _id;
        private string _name;
        private string _address;
        private int _port;

        public ConsulProvider(string address, int port)
        {
            _address = address;
            _port = port;

            _client = new ConsulClient();

            _name = Assembly.GetEntryAssembly().GetName().Name;
            _id = $"{_name}:{port}";
        }



        public void Start()
        {


            var tcpCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1),
                Interval = TimeSpan.FromSeconds(30),
                TCP = $"{_address}:{_port}"
            };

            var httpCheck = new AgentServiceCheck()
            {
                DeregisterCriticalServiceAfter = TimeSpan.FromMinutes(1),
                Interval = TimeSpan.FromSeconds(30),
                HTTP = $"http://{_address}:{_port}/HealthCheck"
            };

            var registration = new AgentServiceRegistration()
            {
                Checks = new[] { tcpCheck, httpCheck },
                Address = $"{_address}",
                ID = _id,
                Name = _name,
                Port = _port
            };

            _client.Agent.ServiceRegister(registration).GetAwaiter().GetResult();

            Console.WriteLine("Consul Registed.");

        }

        public void Stop()
        {

            _client.Agent.ServiceDeregister(_id).GetAwaiter().GetResult();
        }
    }
}
