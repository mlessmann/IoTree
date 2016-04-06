using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTree.Gpio
{
    public enum PinMode : int
    {
        /// <summary>
        /// Specifies a pin used for reading incoming signals.
        /// </summary>
        Input = 0,

        /// <summary>
        /// Specifies a pin used for outputting signals.
        /// </summary>
        Output = 1,
        
        /// <summary>
        /// Specifies a pin to use hardware pwm output. Only works with
        /// WiringPi pin 1 (BCM_GPIO 18, Phys pin 12).
        /// </summary>
        //PwmOutput = 2,
        //GpioClock = 3
        //SoftToneOutput = 4,
        //PwmToneOutput = 5
    }
}
