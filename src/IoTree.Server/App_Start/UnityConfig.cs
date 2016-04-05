using IoTree.Gpio;
using IoTree.Server.Models;
using Microsoft.Practices.Unity;
using NLog;
using System.Web.Http;
using Unity.WebApi;

namespace IoTree.Server
{
    public static class UnityConfig
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            InitGpioManager(container);
            container.RegisterInstance<IPatternManager>(new PatternManager(container.Resolve<IGpioManager>()));

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        private static void InitGpioManager(IUnityContainer container)
        {
            logger.Info("Setting up GPIO interface.");
#if DEBUG
            var gpio = new MockGpioManager();
#else
            var gpio = new GpioManager();
#endif
            logger.Info("Creating software pwm pins.");
            gpio.InitializeSoftPwmPins();
            container.RegisterInstance<IGpioManager>(gpio);
        }
    }
}