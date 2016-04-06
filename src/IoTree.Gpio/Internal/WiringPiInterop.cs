using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IoTree.Gpio.Internal
{
    internal class WiringPiInterop : IWiringPiInterop
    {
        const string LibraryName = "libwiringPi.so";

        #region Setup

        [DllImport(LibraryName)]
        private static extern int wiringPiSetup();

        public int Setup()
        {
            return wiringPiSetup();
        }

        [DllImport(LibraryName)]
        private static extern int wiringPiSetupGpio();

        public int SetupGpio()
        {
            return wiringPiSetupGpio();
        }

        [DllImport(LibraryName)]
        private static extern int wiringPiSetupPhys();

        public int SetupPhys()
        {
            return wiringPiSetupPhys();
        }

        [DllImport(LibraryName)]
        private static extern int wiringPiSetupSys();

        public int SetupSys()
        {
            return wiringPiSetupSys();
        }

        #endregion

        #region Core functions

        [DllImport(LibraryName)]
        private static extern void pinMode(int pin, int mode);

        public void PinMode(int pin, PinMode mode)
        {
            pinMode(pin, (int)mode);
        }

        [DllImport(LibraryName)]
        private static extern int getAlt(int pin);

        public PinMode GetAlt(int pin)
        {
            return (PinMode)getAlt(pin);
        }

        [DllImport(LibraryName)]
        private static extern void pullUpDnControl(int pin, int pud);

        public void PullUpDnControl(int pin, ResistorMode mode)
        {
            pullUpDnControl(pin, (int)mode);
        }

        [DllImport(LibraryName)]
        private static extern void digitalWrite(int pin, int value);

        public void DigitalWrite(int pin, PinValue value)
        {
            digitalWrite(pin, (int)value);
        }

        [DllImport(LibraryName)]
        private static extern int digitalRead(int pin);

        public PinValue DigitalRead(int pin)
        {
            return (PinValue)digitalRead(pin);
        }

        #endregion

        #region Software Pwm

        [DllImport(LibraryName)]
        private static extern int softPwmCreate(int pin, int initialValue, int pwmRange);

        public void SoftPwmCreate(int pin, int initialValue, int pwmRange)
        {
            softPwmCreate(pin, initialValue, pwmRange);
        }

        [DllImport(LibraryName)]
        private static extern int softPwmWrite(int pin, int value);

        public void SoftPwmWrite(int pin, int value)
        {
            softPwmWrite(pin, value);
        }

        #endregion
    }
}
