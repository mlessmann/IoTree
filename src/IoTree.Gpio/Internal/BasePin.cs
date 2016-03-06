using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTree.Gpio.Internal
{
    internal abstract class BasePin
    {
        private readonly PinId id;

        public PinId Id { get { return id; } }

        internal BasePin(PinId id)
        {
            this.id = id;
        }
    }
}
