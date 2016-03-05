using IoTree.Contract;
using NLog;
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
        private static ILogger logger = LogManager.GetCurrentClassLogger();

        private readonly WebServiceHost host;
        private readonly LedService singletonService;

        public LedServiceHost(int port)
        {
            singletonService = new LedService();

            var address = new Uri("http://localhost:" + port);
            logger.Debug("Creating service host on port {0}.", port);
            host = new WebServiceHost(singletonService, address);

            var manualEndpointAddress = "tree/manual";
            logger.Debug("Creating service endpoint with IManualLedService contract at {0}.", address + manualEndpointAddress);
            host.AddServiceEndpoint(typeof(IManualLedService), new WebHttpBinding(), "tree/manual");

            host.Closed += (obj, e) => logger.Debug("ServiceHost closed.");
            host.Closing += (obj, e) => logger.Debug("ServiceHost closing.");
            host.Faulted += (obj, e) => logger.Error("ServiceHost faulted!");
            host.Opened += (obj, e) => logger.Debug("ServiceHost opened.");
            host.Opening += (obj, e) => logger.Debug("ServiceHost opening.");
            host.UnknownMessageReceived += (obj, e) => logger.Warn("ServiceHost received unknown message: " + e.Message.ToString());
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
