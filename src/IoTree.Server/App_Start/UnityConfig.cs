using IoTree.Gpio;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace IoTree.Server
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            InitGpioManager(container);

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        private static void InitGpioManager(IUnityContainer container)
        {
#if DEBUG
            var gpio = new MockGpioManager();
#else
            var gpio = new GpioManager();
#endif
            gpio.InitializeSoftPwmPins();
            container.RegisterInstance<IGpioManager>(gpio);
        }
    }
}