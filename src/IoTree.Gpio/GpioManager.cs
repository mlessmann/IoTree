using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTree.Gpio
{
    /// <summary>
    /// Handles actions that affect all pins, such as the discovery
    /// of pins.
    /// </summary>
    public class GpioManager
    {
        private readonly GpioPin[] pins;

        /// <summary>
        /// Gets all gpio pins.
        /// </summary>
        public GpioPin[] Pins { get { return pins; } }

        /// <summary>
        /// Initializes and discovers the gpio pins.
        /// </summary>
        public GpioManager()
        {
            Wpi.SetupGpio();

            pins = (from phys in Enumerable.Range(1, 40)
                    let bcm = Wpi.PhysPinToGpio(phys)
                    where bcm >= 0 // Filters all non-gpio pins such as ground
                    select new GpioPin(new PinId(phys, bcm))).ToArray();
        }

        /// <summary>
        /// Discovers all gpio pins and sets them all to the given mode.
        /// </summary>
        /// <param name="mode"></param>
        public GpioManager(PinMode mode) : this()
        {
            foreach (var pin in pins)
                pin.Mode = mode;
        }

        /// <summary>
        /// Discovers all gpio pins, sets them all to output mode and
        /// writes the given values to them all.
        /// </summary>
        /// <param name="value"></param>
        public GpioManager(PinValue value) : this(PinMode.Output)
        {
            foreach (var pin in pins)
                pin.Value = value;
        }
    }
}
