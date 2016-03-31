using IoTree.Gpio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IoTree.Server
{
    public class MockGpioManager : IGpioManager
    {
        private Dictionary<PinId, ISoftPwmPin> softPwmPins = new Dictionary<PinId, ISoftPwmPin>();

        public IReadOnlyDictionary<PinId, IInputPin> InputPins
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IReadOnlyDictionary<PinId, IOutputPin> OutputPins
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public IReadOnlyDictionary<PinId, ISoftPwmPin> SoftPwmPins { get { return softPwmPins; } }

        public void InitializeInputPins(ResistorMode mode = ResistorMode.Off, params PinId[] ids)
        {
            throw new NotImplementedException();
        }

        public void InitializeOutputPins(PinValue value = PinValue.Low, params PinId[] ids)
        {
            throw new NotImplementedException();
        }

        public void InitializeSoftPwmPins(double value = 0, int range = 100, params PinId[] ids)
        {
            if (!ids.Any())
                ids = PinId.AllValidPinIds;

            foreach (var id in ids)
            {
                softPwmPins.Add(id, new SoftPwmPin { Id = id, Value = value });
            }
        }

        private class SoftPwmPin : ISoftPwmPin
        {
            public PinId Id { get; set; }

            public double Value { get; set; }
        }
    }
}