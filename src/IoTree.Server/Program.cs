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
        const int DefaultPort = 80;

        static void Main(string[] args)
        {
            Console.WriteLine("Starting IoTree.Server...");

            var host = new LedServiceHost(DefaultPort);
            host.Start().Wait();

            Console.WriteLine("IoTree.Server started. Press enter to shut it down.");
            Console.ReadLine();

            host.Stop().Wait();

            Console.WriteLine("IoTree.Server has shut down.");
        }
    }
}
