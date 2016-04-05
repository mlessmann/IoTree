using IoTree.Gpio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace IoTree.Server.Models
{
    public class PatternManager : IPatternManager, IDisposable
    {
        private readonly object syncRoot = new object();
        private readonly IGpioManager gpio;
        private readonly AutoResetEvent patternChangedSignal = new AutoResetEvent(false);
        private readonly Thread patternThread;
        private bool threadKillSignal = false;
        private List<PatternStep> current;

        public List<PatternStep> Current
        {
            get { return current; }
            set
            {
                lock (syncRoot)
                {
                    current = value;
                    RemoveRedundantSteps();
                    patternChangedSignal.Set();
                }
            }
        }

        public PatternManager(IGpioManager gpio)
        {
            this.gpio = gpio;
            this.current = new List<PatternStep>() { new PatternStep() };
            this.patternThread = new Thread(PatternLoop);
            this.patternThread.Start();
        }

        public void SetLed(PinId led, double value)
        {
            lock (syncRoot)
            {
                foreach (var step in current)
                    step.LedValues[led.BroadcomId] = value;
                RemoveRedundantSteps();
                gpio.SoftPwmPins[led].Value = value;
            }
        }

        private void PatternLoop()
        {
            int patternStep = 0;
            while (!threadKillSignal)
            {
                if (current.Count == 1)
                {
                    ApplyPatternStep(current[0]);
                    patternChangedSignal.WaitOne();
                }
                else
                {
                    ApplyPatternStep(current[patternStep]);
                    if (patternChangedSignal.WaitOne(current[patternStep].Duration))
                        patternStep = 0;
                    else
                        patternStep = (patternStep + 1) % current.Count;
                }
                patternChangedSignal.Reset();
            }
        }

        private void ApplyPatternStep(PatternStep step)
        {
            foreach(var pin in step.LedValues)
            {
                gpio.SoftPwmPins[PinId.FromBroadcom(pin.Key)].Value = pin.Value;
            }
        }

        private void RemoveRedundantSteps()
        {
            if (current.Count <= 1)
                return;

            for (int i = 0; i < current.Count; i++)
            {
                int next = (i + 1) % current.Count;
                if (current[next].IsUnchangedSuccessor(current[i]))
                {
                    current[i].Duration += current[next].Duration;
                    current.RemoveAt(next);
                    i--;
                }
            }
        }

        public void Dispose()
        {
            threadKillSignal = true;
            patternChangedSignal.Set();
            patternChangedSignal.Dispose();
        }
    }
}