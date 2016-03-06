using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IoTree.Gpio.Internal
{
    internal static class Wpi
    {
        const string LibraryName = "libwiringPi.so";

        #region Setup

        [DllImport(LibraryName)]
        private static extern int wiringPiSetup();

        internal static int Setup()
        {
            return wiringPiSetup();
        }

        [DllImport(LibraryName)]
        private static extern int wiringPiSetupGpio();

        internal static int SetupGpio()
        {
            return wiringPiSetupGpio();
        }

        [DllImport(LibraryName)]
        private static extern int wiringPiSetupPhys();

        internal static int SetupPhys()
        {
            return wiringPiSetupPhys();
        }

        [DllImport(LibraryName)]
        private static extern int wiringPiSetupSys();

        internal static int SetupSys()
        {
            return wiringPiSetupSys();
        }

        #endregion

        #region Core functions

        [DllImport(LibraryName)]
        private static extern void pinMode(int pin, int mode);

        internal static void PinMode(int pin, PinMode mode)
        {
            pinMode(pin, (int)mode);
        }

        [DllImport(LibraryName)]
        private static extern int getAlt(int pin);

        internal static PinMode GetAlt(int pin)
        {
            return (PinMode)getAlt(pin);
        }

        [DllImport(LibraryName)]
        private static extern void pullUpDnControl(int pin, int pud);

        internal static void PullUpDnControl(int pin, ResistorMode mode)
        {
            pullUpDnControl(pin, (int)mode);
        }

        [DllImport(LibraryName)]
        private static extern void digitalWrite(int pin, int value);

        internal static void DigitalWrite(int pin, PinValue value)
        {
            digitalWrite(pin, (int)value);
        }

        [DllImport(LibraryName)]
        private static extern int digitalRead(int pin);

        internal static PinValue DigitalRead(int pin)
        {
            return (PinValue)digitalRead(pin);
        }

        #endregion

        #region Software Pwm

        [DllImport(LibraryName)]
        private static extern int softPwmCreate(int pin, int initialValue, int pwmRange);

        internal static void SoftPwmCreate(int pin, int initialValue, int pwmRange)
        {
            softPwmCreate(pin, initialValue, pwmRange);
        }

        [DllImport(LibraryName)]
        private static extern int softPwmWrite(int pin, int value);

        internal static void SoftPwmWrite(int pin, int value)
        {
            softPwmWrite(pin, value);
        }

        #endregion

        #region Utility

        [DllImport(LibraryName)]
        private static extern int piBoardRev();

        internal static int PiBoardRev()
        {
            return piBoardRev();
        }

        #endregion
    }
}
