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
            get { return wpi.DigitalRead(Id.InteropId); }
            set { wpi.DigitalWrite(Id.InteropId, value); }
        }

        internal OutputPin(IWiringPiInterop wpi, PinId id, PinValue value = PinValue.Low) : base(wpi, id)
        {
            wpi.PinMode(id.InteropId, PinMode.Output);
            Value = value;
        }
    }
}
