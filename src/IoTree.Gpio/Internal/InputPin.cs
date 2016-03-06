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

        public PinValue Value { get { return Wpi.DigitalRead(Id.InteropId); } }

        public ResistorMode ResistorMode
        {
            get { return resistorMode; }
            set
            {
                Wpi.PullUpDnControl(Id.InteropId, value);
                this.resistorMode = value;
            }
        }

        internal InputPin(PinId id, ResistorMode resistorMode = ResistorMode.Off) : base(id)
        {
            this.resistorMode = resistorMode;
            Wpi.PinMode(id.InteropId, PinMode.Input);
        }
    }
}
