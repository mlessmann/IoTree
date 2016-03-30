using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTree.Gpio
{
    public interface IOutputPin
    {
        /// <summary>
        /// The unique id of this gpio pin.
        /// </summary>
        PinId Id { get; }

        /// <summary>
        /// Gets or sets the current digital value of this gpio pin.
        /// </summary>
        PinValue Value { get; set; }
    }
}
