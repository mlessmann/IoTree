using IoTree.Gpio;
using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace IoTree.Server
{
    class Program
    {
        const int DefaultPort = 10733;

        private static ILogger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            ConfigureLogger();
            logger.Info("Starting IoTree.Server...");

            try
            {
                var gpio = new GpioManager();
                logger.Debug("Gpio interface initialized.");
                gpio.InitializeSoftPwmPins();
                logger.Debug("Software pwm pins initialized.");

                var host = new LedServiceHost(DefaultPort, gpio);
                host.Start().Wait();

                logger.Info("IoTree.Server started. Press enter to shut it down.");
                Console.ReadLine();

                host.Stop().Wait();

                logger.Info("IoTree.Server has shut down.");
            }
            catch(Exception e)
            {
                logger.Fatal(e, "Exception caught in Main method!");
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        private static void ConfigureLogger()
        {
            var config = new LoggingConfiguration();

            var consoleTarget = new ColoredConsoleTarget();
            consoleTarget.Layout = "${time} ${message}";
            var consoleRule = new LoggingRule("*", LogLevel.Debug, consoleTarget);

            var fileTarget = new FileTarget();
            fileTarget.FileName = "IoTree.Server.log";
            var fileRule = new LoggingRule("*", LogLevel.Trace, fileTarget);

            config.AddTarget("ConsoleDebugLogger", consoleTarget);
            config.LoggingRules.Add(consoleRule);
            config.AddTarget("FileTraceLogger", fileTarget);
            config.LoggingRules.Add(fileRule);

            LogManager.Configuration = config;
        }
    }
}
