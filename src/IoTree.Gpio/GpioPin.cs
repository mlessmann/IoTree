using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTree.Gpio
{
    /// <summary>
    /// Represents a gpio-pin and its current state.
    /// </summary>
    public class GpioPin
    {
        private readonly PinId id;

        /// <summary>
        /// Gets the unique Id of this pin, both physical and Broadcom.
        /// </summary>
        public PinId Id { get { return id; } }

        /// <summary>
        /// Reads or Writes a value to the pin. Writing is only allowed
        /// if Mode is set to output.
        /// </summary>
        public PinValue Value
        {
            get { return Wpi.DigitalRead(Id); }
            set
            {
                if (Mode != PinMode.Output)
                    throw new GpioException(this, "Writing is only allowed if the pin is in output mode. Its current mode is " + Mode.ToString() + ".");

                Wpi.DigitalWrite(Id, value);
            }
        }

        /// <summary>
        /// Reads or writes the mode of this pin.
        /// </summary>
        public PinMode Mode
        {
            get { return Wpi.GetAlt(Id); }
            set { Wpi.PinMode(Id, value); }
        }

        internal GpioPin(PinId id)
        {
            this.id = id;
        }

        /// <summary>
        /// Sets the Pull up/down mode of the internal resistor of this pin.
        /// The pin needs to be in input mode.
        /// </summary>
        /// <param name="mode"></param>
        public void SetResistorMode(ResistorMode mode)
        {
            if (Mode != PinMode.Input)
                throw new GpioException(this, "Resistor Mode can only be changed on input pins.");

            Wpi.PullUpDnControl(Id, mode);
        }

        public override string ToString()
        {
            return String.Format("{0}, Mode={1}, Value={2}", Id, Mode.ToString(), Value.ToString());
        }
    }
}
