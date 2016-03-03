using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTree.Gpio
{
    /// <summary>
    /// Modes for the pull-up/down resistors of an input pin.
    /// </summary>
    public enum ResistorMode : int
    {
        /// <summary>
        /// No pull up or pull down
        /// </summary>
        Off = 0,

        /// <summary>
        /// Pull to ground
        /// </summary>
        Down = 1,

        /// <summary>
        /// Pull to 3.3v
        /// </summary>
        Up = 2
    }
}
