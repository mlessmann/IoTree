using IoTree.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace IoTree.Server
{
    public class LedServiceHost
    {
        private readonly WebServiceHost host;
        private readonly LedService singletonService;

        public LedServiceHost(int port)
        {
            singletonService = new LedService();

            var address = new Uri("http://localhost:" + port);
            host = new WebServiceHost(singletonService, address);
            host.AddServiceEndpoint(typeof(IManualLedService), new WebHttpBinding(), "tree/manual");

            host.Closed += (obj, e) => Console.WriteLine("ServiceHost closed.");
            host.Closing += (obj, e) => Console.WriteLine("ServiceHost closing.");
            host.Faulted += (obj, e) => Console.WriteLine("ServiceHost faulted!");
            host.Opened += (obj, e) => Console.WriteLine("ServiceHost opened.");
            host.Opening += (obj, e) => Console.WriteLine("ServiceHost opening.");
            host.UnknownMessageReceived += (obj, e) => Console.WriteLine("ServiceHost received unknown message: " + e.Message.ToString());
        }

        public Task Start()
        {
            return Task.Run(() => host.Open());
        }

        public Task Stop()
        {
            return Task.Run(() => host.Close());
        }
    }
}
