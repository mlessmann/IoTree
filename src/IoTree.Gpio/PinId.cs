using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTree.Gpio
{
    /// <summary>
    /// Represents the unique id of a gpio pin.
    /// This class uses both the physical and the
    /// Broadcom id. It can infer one id from the other.
    /// </summary>
    public class PinId
    {
        private static readonly int[] physicalToBroadcomMap = { -1, -1, -1, 2, -1, 3, -1, 4, 14, -1, 15, 17, 18, 27, -1, 22, 23, -1, 24, 10, -1, 9, 25, 11, 8, -1, 7, 0, 1, 5, -1, 6, 12, 13, -1, 19, 16, 26, 20, -1, 21 };
        private static readonly int[] broadcomToPhysicalMap = { 27, 28, 3, 5, 7, 29, 31, 26, 24, 21, 19, 23, 32, 33, 8, 10, 36, 11, 12, 35, 38, 40, 15, 16, 18, 22, 37, 13 };

        /// <summary>
        /// Gets all valid PinIds for the Raspberry Pi (40 pin version).
        /// </summary>
        public static PinId[] AllValidPinIds
        {
            get { return broadcomToPhysicalMap.Select(p => FromPhysical(p)).ToArray(); }
        }

        private readonly int physicalId;
        private readonly int broadcomId;

        /// <summary>
        /// The Id based on the physical layout of the board.
        /// </summary>
        public int PhysicalId { get { return physicalId; } }

        /// <summary>
        /// The internal numbering scheme of the Broadcom-SoC.
        /// </summary>
        public int BroadcomId { get { return broadcomId; } }

        internal int InteropId { get { return BroadcomId; } }

        private PinId(int physicalId, int broadcomId)
        {
            this.physicalId = physicalId;
            this.broadcomId = broadcomId;
        }

        /// <summary>
        /// Creates a PinId from a physical id. The broadcom id is inferred.
        /// </summary>
        /// <param name="physicalId"></param>
        /// <returns></returns>
        public static PinId FromPhysical(int physicalId)
        {
            var broadcomId = physicalToBroadcomMap[physicalId];
            if (broadcomId < 0)
                throw new ArgumentException(physicalId + " is not a valid gpio pin number.", "physicalId");
            return new PinId(physicalId, broadcomId);
        }

        /// <summary>
        /// Creates a PinId from a broadcom id. THe physical id in inferred.
        /// </summary>
        /// <param name="broadcomId"></param>
        /// <returns></returns>
        public static PinId FromBroadcom(int broadcomId)
        {
            var physicalId = broadcomToPhysicalMap[broadcomId];
            return new PinId(physicalId, broadcomId);
        }
        
        public override bool Equals(object obj)
        {
            var other = obj as PinId;
            if (other == null)
                return false;
            return this.PhysicalId == other.PhysicalId;
        }

        public override int GetHashCode()
        {
            return this.PhysicalId.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("Phys={0:D2}, BCM={1:D2}", PhysicalId, BroadcomId);
        }
    }
}
