using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMX.Entities.Enumerations;

namespace DMX.Entities
{
    public class LedScanner : DmxDevice
    {
        public LedScanner(string name, int startAddress, DmxDeviceTypes deviceType, int numberOfChannels)
            : base(name, startAddress, deviceType)
        {
            Channels = new byte[numberOfChannels];
        }

        private byte[] channels;
        public override byte[] Channels { get; }
    }
}
