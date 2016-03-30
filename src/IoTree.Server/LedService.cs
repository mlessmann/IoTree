using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;
using IoTree.Contract;
using IoTree.Gpio;
using NLog;
using System.Net;

namespace IoTree.Server
{
    [ServiceBehavior(
        InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Single)]
    public class LedService : IManualLedService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IGpioManager gpio;

        public LedService(IGpioManager gpio)
        {
            this.gpio = gpio;
        }

        public ResponseToken<LedState[]> GetLeds()
        {
            try
            {
                logger.Debug("Received GetLeds().");
                var data = gpio.SoftPwmPins.Values
                    .Select(p => new LedState { Led = p.Id.BroadcomId, Value = p.Value })
                    .OrderBy(p => p.Led)
                    .ToArray();
                return ResponseToken.OkData(data, "GetLeds");
            }
            catch(Exception e)
            {
                logger.Error(e, "GetLeds()");
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.InternalServerError;
                return ResponseToken.ErrorData<LedState[]>(e, null, "GetLeds");
            }
        }

        public ResponseToken SetLed(string led, string value)
        {
            try
            {
                logger.Debug("Received SetLed({0}, {1}).", led, value);
                int bcmPin;
                if (!int.TryParse(led, out bcmPin))
                {
                    logger.Info("Received invalid led parameter ({0}), canceling request.", led);
                    return ResponseToken.Error(led + " is not a valid number.", "SetLed", led, value);
                }

                double pinValue;
                if (!double.TryParse(value, out pinValue))
                {
                    logger.Info("Received invalid value parameter ({0}), canceling request.", value);
                    return ResponseToken.Error(value + " is not a valid number.", "SetLed", led, value);
                }

                if (!PinId.IsValidBroadcomId(bcmPin))
                {
                    logger.Info("Received invalid led id ({0}), canceling request.", bcmPin);
                    return ResponseToken.Error(bcmPin + " is not a valid led id.", "SetLed", led, value);
                }

                var pin = gpio.SoftPwmPins[PinId.FromBroadcom(bcmPin)];
                pin.Value = pinValue;
                return ResponseToken.Ok("SetLed", bcmPin, pin.Value);
            }
            catch(Exception e)
            {
                logger.Error(e, "SetLed({0}, {1})", led, value);
                WebOperationContext.Current.OutgoingResponse.StatusCode = HttpStatusCode.InternalServerError;
                return ResponseToken.Error(e, "SetLed", led, value);
            }
        }
    }
}
