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
            : base(name, startAddress, deviceType)
        {
            Channels = new byte[NUMBER_OF_CHANNELS];
        }

        private byte[] channels;
        public byte[] Channels { get; private set; }

        public void OpenDatasheet()
        {
            System.Diagnostics.Process.Start(@"Datasheets\Euro lite - LED Pixel Panel 16 DMX.pdf");
        }

        public void UpdateChannel(int channelNumber, byte value)
        {
            if (channelNumber >= 0 && channelNumber < channels.Count())
                channels[channelNumber] = value;
        }

        public void UpdateChannel(byte[] values)
        {
            if (values.Count() == NUMBER_OF_CHANNELS)
                channels = values;
        }

        public byte[] GetLedRGB(int ledNumber)
        {
            return new byte[3] { channels[3 * ledNumber], channels[(3 * ledNumber) + 1], channels[(3 * ledNumber) + 2] };
        }
    }
}
