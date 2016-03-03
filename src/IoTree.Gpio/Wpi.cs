using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IoTree.Gpio
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

        #region Hardware Pwm

        [DllImport(LibraryName)]
        private static extern void pwmSetMode(int mode);

        internal static void PwmSetMode(HardwarePwmMode mode)
        {
            pwmSetMode((int)mode);
        }

        [DllImport(LibraryName)]
        private static extern void pwmSetRange(uint range);

        internal static void PwmSetRange(uint range)
        {
            pwmSetRange(range);
        }

        [DllImport(LibraryName)]
        private static extern void pwmSetClock(int divisor);

        internal static void PwmSetClock(int divisor)
        {
            pwmSetClock(divisor);
        }

        [DllImport(LibraryName)]
        private static extern void pwmWrite(int pin, int value);

        internal static void PwmWrite(int pin, int value)
        {
            pwmWrite(pin, value);
        }

        #endregion

        #region Utility

        [DllImport(LibraryName)]
        private static extern int piBoardRev();

        internal static int PiBoardRev()
        {
            return piBoardRev();
        }

        [DllImport(LibraryName)]
        private static extern int wpiPinToGpio(int wpiPin);

        internal static int WpiPinToGpio(int wpiPin)
        {
            return wpiPinToGpio(wpiPin);
        }

        [DllImport(LibraryName)]
        private static extern int physPinToGpio(int physPin);

        internal static int PhysPinToGpio(int physPin)
        {
            return physPinToGpio(physPin);
        }

        #endregion
    }
}
