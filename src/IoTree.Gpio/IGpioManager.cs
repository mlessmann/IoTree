using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTree.Gpio
{
    public interface IGpioManager
    {
        /// <summary>
        /// Gets all pins that were initialized as input pins.
        /// </summary>
        IReadOnlyDictionary<PinId, IInputPin> InputPins { get; }

        /// <summary>
        /// Gets all pins that were initialized as output pins.
        /// </summary>
        IReadOnlyDictionary<PinId, IOutputPin> OutputPins { get; }

        /// <summary>
        /// Gets all pins that were initialized as output with software
        /// pulse width modulation.
        /// </summary>
        IReadOnlyDictionary<PinId, ISoftPwmPin> SoftPwmPins{ get; }

        /// <summary>
        /// Initializes pins with the given ids for reading.
        /// </summary>
        /// <param name="mode">Sets the resistor mode of the pins.</param>
        /// <param name="ids">Ids of the pins. If this is empty, all pins will
        /// be initialized.</param>
        void InitializeInputPins(ResistorMode mode = ResistorMode.Off, params PinId[] ids);

        /// <summary>
        /// Initializes pins with the given ids for digital writing.
        /// </summary>
        /// <param name="value">The initial digital value of the pins.</param>
        /// <param name="ids">Ids of the pins. If this is empty, all pins will
        /// be initialized.</param>
        void InitializeOutputPins(PinValue value = PinValue.Low, params PinId[] ids);

        /// <summary>
        /// Initializes pins with the given ids for pulse width modulated writing.
        /// </summary>
        /// <param name="value">The initial value of the pins.</param>
        /// <param name="range">The resolution within every pwm cycle.</param>
        /// <param name="ids">Ids of the pins. If this is empty, all pins will
        /// be initialized.</param>
        void InitializeSoftPwmPins(double value = 0.0, int range = 100, params PinId[] ids);
    }
}
