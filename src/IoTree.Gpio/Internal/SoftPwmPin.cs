using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTree.Gpio.Internal
{
    internal class SoftPwmPin : BasePin, ISoftPwmPin
    {
        private readonly int range;
        private double value;

        public double Value
        {
            get { return value; }
            set
            {
                var rangeValue = Clamp(PercentToRange(value), 0, range);
                Wpi.SoftPwmWrite(Id.InteropId, rangeValue);
                this.value = RangeToPercent(rangeValue);
            }
        }

        internal SoftPwmPin(PinId id, double initialValue = 0.0, int range = 100) : base(id)
        {
            this.range = range;
            var rangeValue = Clamp(PercentToRange(initialValue), 0, range);
            Wpi.SoftPwmCreate(id.InteropId, rangeValue, range);
            this.value = RangeToPercent(rangeValue);
        }

        private double RangeToPercent(int rangeValue)
        {
            return (double)rangeValue / (double)range;
        }

        private int PercentToRange(double percent)
        {
            return (int)Math.Round(percent * range);
        }

        private static int Clamp(int value, int lowerBound, int upperBound)
        {
            if (value < lowerBound)
                return lowerBound;
            else if (value > upperBound)
                return upperBound;
            else
                return value;
        }
    }
}
