using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTree.Gpio.Internal
{
    internal class InputPin : BasePin, IInputPin
    {
        private ResistorMode resistorMode;

        public PinValue Value { get { return wpi.DigitalRead(Id.InteropId); } }

        public ResistorMode ResistorMode
        {
            get { return resistorMode; }
            set
            {
                wpi.PullUpDnControl(Id.InteropId, value);
                this.resistorMode = value;
            }
        }

        internal InputPin(IWiringPiInterop wpi, PinId id, ResistorMode resistorMode = ResistorMode.Off) : base(wpi, id)
        {
            this.resistorMode = resistorMode;
            wpi.PinMode(id.InteropId, PinMode.Input);
        }
    }
}
