using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTree.Gpio
{
    public interface IWiringPiInterop
    {
        #region Setup

        int Setup();

        int SetupGpio();

        int SetupPhys();

        int SetupSys();

        #endregion

        #region Core functions

        void PinMode(int pin, PinMode mode);

        PinMode GetAlt(int pin);

        void PullUpDnControl(int pin, ResistorMode mode);

        void DigitalWrite(int pin, PinValue value);

        PinValue DigitalRead(int pin);

        #endregion

        #region SOftware pwm

        void SoftPwmCreate(int pin, int initialValue, int pwmRange);

        void SoftPwmWrite(int pin, int value);

        #endregion
    }
}
