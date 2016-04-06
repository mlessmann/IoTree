using IoTree.Gpio.Internal;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTree.Gpio
{
    public class GpioManager : IGpioManager
    {
        private readonly IWiringPiInterop wpi;
        private readonly Dictionary<PinId, IInputPin> inputPins = new Dictionary<PinId, IInputPin>();
        private readonly Dictionary<PinId, IOutputPin> outputPins = new Dictionary<PinId, IOutputPin>();
        private readonly Dictionary<PinId, ISoftPwmPin> softPwmPins = new Dictionary<PinId, ISoftPwmPin>();

        /// <summary>
        /// Gets all pins that were initialized as input pins.
        /// </summary>
        public IReadOnlyDictionary<PinId, IInputPin> InputPins { get { return inputPins; } }

        /// <summary>
        /// Gets all pins that were initialized as output pins.
        /// </summary>
        public IReadOnlyDictionary<PinId, IOutputPin> OutputPins { get { return outputPins; } }

        /// <summary>
        /// Gets all pins that were initialized as output pins.
        /// </summary>
        public IReadOnlyDictionary<PinId, ISoftPwmPin> SoftPwmPins { get { return softPwmPins; } }

        /// <summary>
        /// Initializes the gpio interface.
        /// </summary>
        /// <param name="wpi">Optional parameter for injection of interop stub.</param>
        public GpioManager(IWiringPiInterop wpi = null)
        {
            this.wpi = wpi ?? new WiringPiInterop();
            this.wpi.SetupGpio();
        }

        /// <summary>
        /// Initializes pins with the given ids for reading.
        /// </summary>
        /// <param name="mode">Sets the resistor mode of the pins.</param>
        /// <param name="ids">Ids of the pins. If this is empty, all pins will
        /// be initialized.</param>
        public void InitializeInputPins(ResistorMode mode = ResistorMode.Off, params PinId[] ids)
        {
            if (!ids.Any())
                ids = PinId.AllValidPinIds;

            CheckAlreadyInitialized(ids);
            
            foreach (var id in ids)
            {
                inputPins.Add(id, new InputPin(wpi, id, mode));
            }
        }

        /// <summary>
        /// Initializes pins with the given ids for digital writing.
        /// </summary>
        /// <param name="value">The initial digital value of the pins.</param>
        /// <param name="ids">Ids of the pins. If this is empty, all pins will
        /// be initialized.</param>
        public void InitializeOutputPins(PinValue value = PinValue.Low, params PinId[] ids)
        {
            if (!ids.Any())
                ids = PinId.AllValidPinIds;

            CheckAlreadyInitialized(ids);
            
            foreach (var id in ids)
            {
                outputPins.Add(id, new OutputPin(wpi, id, value));
            }
        }

        /// <summary>
        /// Initializes pins with the given ids for pulse width modulated writing.
        /// </summary>
        /// <param name="value">The initial value of the pins.</param>
        /// <param name="range">The resolution within every pwm cycle.</param>
        /// <param name="ids">Ids of the pins. If this is empty, all pins will
        /// be initialized.</param>
        public void InitializeSoftPwmPins(double value = 0.0, int range = 100, params PinId[] ids)
        {
            if (!ids.Any())
                ids = PinId.AllValidPinIds;

            CheckAlreadyInitialized(ids);

            foreach (var id in ids)
            {
                softPwmPins.Add(id, new SoftPwmPin(wpi, id, value, range));
            }
        }

        private void CheckAlreadyInitialized(IEnumerable<PinId> ids)
        {
            var alreadyInitialized = inputPins.Keys.Concat(outputPins.Keys).Concat(softPwmPins.Keys);
            var falselyInitialized = ids.Intersect(alreadyInitialized);
            if (falselyInitialized.Any())
            {
                throw new ArgumentException("The following pins are already initialized:\n"
                    + String.Join("\n", falselyInitialized));
            }
        }
    }
}
