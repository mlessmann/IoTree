using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using IoTree.Contract;

namespace IoTree.Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class LedService : IManualLedService
    {
        private double[] leds = new double[41];

        public LedService()
        {
            leds[0] = -1;
        }

        public double[] GetLeds()
        {
            var remoteHost = OperationContext.Current.Channel.RemoteAddress;
            Console.WriteLine("Received GetLeds from " + remoteHost + ".");
            return leds;
        }

        public string SetLed(string led, string value)
        {
            var remoteHost = OperationContext.Current.Channel.RemoteAddress;
            Console.WriteLine("Received SetLed(" + led + ", " + value + ") from " + remoteHost + ".");
            leds[int.Parse(led)] = double.Parse(value);
            return "Success";
        }
    }
}
