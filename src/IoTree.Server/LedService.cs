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

        public ResponseToken<double[]> GetLeds()
        {
            try
            {
                Console.WriteLine("Received GetLeds().");
                return ResponseToken.CreateOk(leds, "GetLeds");
            }
            catch(Exception e)
            {
                return ResponseToken.CreateError<double[]>(e, "GetLeds");
            }
        }

        public ResponseToken SetLed(string led, string value)
        {
            try
            {
                Console.WriteLine("Received SetLed(" + led + ", " + value + ").");
                leds[int.Parse(led)] = double.Parse(value);
                return ResponseToken.CreateOk("SetLed", led, value);
            }
            catch(Exception e)
            {
                return ResponseToken.CreateError(e, "SetLed", led, value);
            }
        }
    }
}
