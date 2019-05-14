using DMX.Entities.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMX
{
    public abstract class DmxDevice
    {
        public DmxDevice(string name, int startAddress, DmxDeviceTypes deviceType, byte[] channels)
        {
            Name = name;
            StartAddress = startAddress;
            DeviceType = deviceType;
            Channels = channels;
        }

        private readonly string name;
        public string Name { get; }

        private readonly int startAddress;
        public int StartAddress { get; }

        private readonly DmxDeviceTypes deviceType;
        public DmxDeviceTypes DeviceType { get; }

        private byte[] channels;
        public byte[] Channels { get; private set; }
    }
}
