using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTree.Gpio
{
    public interface IInputPin
    {
        PinId Id { get; }

        PinValue Value { get; }

        ResistorMode ResistorMode { get; set; }
    }
}
