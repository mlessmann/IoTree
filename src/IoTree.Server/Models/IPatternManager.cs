using IoTree.Gpio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTree.Server.Models
{
    public interface IPatternManager
    {
        List<PatternStep> Current { get; set; }

        void SetLed(PinId led, double value);
    }
}
