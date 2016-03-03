using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTree.Gpio
{
    /// <summary>
    /// Represents the unique Id of a gpio pin.
    /// This class uses both the physical and the
    /// Broadcom Id. It can infer the Broadcom Id
    /// from a given physical Id.
    /// </summary>
    public class PinId
    {
        /// <summary>
        /// The Id based on the physical layout of the board.
        /// </summary>
        public int PhysicalId { get; private set; }

        /// <summary>
        /// The internal numbering scheme of the Broadcom-SoC.
        /// </summary>
        public int BroadcomId { get; private set; }

        internal PinId(int physicalId)
        {
            PhysicalId = physicalId;
            BroadcomId = Wpi.PhysPinToGpio(physicalId);
        }

        internal PinId(int physicalId, int broadcomId)
        {
            PhysicalId = physicalId;
            BroadcomId = broadcomId;
        }

        public static implicit operator int(PinId id)
        {
            return id.BroadcomId;
        }

        public override string ToString()
        {
            return String.Format("Phys={0:D2}, BCM={1:D2}", PhysicalId, BroadcomId);
        }
    }
}
