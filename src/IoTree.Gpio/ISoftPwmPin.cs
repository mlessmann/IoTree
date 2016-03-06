using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTree.Gpio
{
    public interface ISoftPwmPin
    {
        PinId Id { get; }

        double Value { get; set; }
    }
}
