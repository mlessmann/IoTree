using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTree.Gpio
{
    public interface IOutputPin
    {
        PinId Id { get; }

        PinValue Value { get; set; }
    }
}
