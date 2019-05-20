using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMX.Entities.Enumerations;

namespace DMX.Entities
{
    public class LedPanel : DmxDevice
    {
        const int NUMBER_OF_CHANNELS = 48;

        public LedPanel(string name, int startAddress, DmxDeviceTypes deviceType)
            : base(name, startAddress, deviceType, NUMBER_OF_CHANNELS)
        { }

        public byte[] GetLedRGB(int ledNumber)
        {
            return new byte[3] { Channels[3 * ledNumber], Channels[(3 * ledNumber) + 1], Channels[(3 * ledNumber) + 2] };
        }
    }
}
