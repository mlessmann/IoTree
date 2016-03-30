using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTree.Gpio
{
    public interface ISoftPwmPin
    {
        /// <summary>
        /// The unique id of this gpio pin.
        /// </summary>
        PinId Id { get; }

        /// <summary>
        /// Gets or sets the pwm value of this pin. This value
        /// is always between 0 (Low) and 1 (High).
        /// </summary>
        double Value { get; set; }
    }
}
