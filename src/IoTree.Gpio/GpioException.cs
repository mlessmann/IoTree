using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTree.Gpio
{
    internal class GpioException : Exception
    {
        internal GpioException(GpioPin pin, string message)
            : base("[" + pin.Id + "]: " + message)
        {

        }

        internal GpioException(PinId pinId, string message)
            : base("[" + pinId + "]: " + message)
        {

        }
    }
}
