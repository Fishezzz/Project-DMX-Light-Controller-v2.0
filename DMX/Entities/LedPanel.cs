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
            : base(name, startAddress, deviceType, new byte[NUMBER_OF_CHANNELS])
        { }

        public void UpdateChannel(int channelNumber, byte value)
        {
            if (channelNumber >= 0 && channelNumber < Channels.Count())
                Channels[channelNumber] = value;
        }

        public byte[] GetLedRGB(int ledNumber)
        {
            return new byte[3] { Channels[3 * ledNumber], Channels[(3 * ledNumber) + 1], Channels[(3 * ledNumber) + 2] };
        }
    }
}
