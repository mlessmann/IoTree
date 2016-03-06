using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTree.Gpio.Internal
{
    internal class OutputPin : BasePin, IOutputPin
    {
        public PinValue Value
        {
            get { return Wpi.DigitalRead(Id.InteropId); }
            set { Wpi.DigitalWrite(Id.InteropId, value); }
        }

        internal OutputPin(PinId id, PinValue value = PinValue.Low) : base(id)
        {
            Wpi.PinMode(id.InteropId, PinMode.Output);
            Value = value;
        }
    }
}
