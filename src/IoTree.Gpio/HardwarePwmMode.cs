using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTree.Gpio
{
    /// <summary>
    /// Modes of a pin using hardware pwm output
    /// </summary>
    public enum HardwarePwmMode : int
    {
        MarkSpace = 0,
        Balanced = 1
    }
}
