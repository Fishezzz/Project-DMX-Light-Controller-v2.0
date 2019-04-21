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
        public DmxDevice(string name, int startAddress, DmxDeviceTypes deviceType)
        {
            Name = name;
            StartAddress = startAddress;
            DeviceType = deviceType;
        }

        private readonly string name;
        public string Name { get; }

        private readonly int startAddress;
        public int StartAddress { get; }

        private readonly DmxDeviceTypes deviceType;
        public DmxDeviceTypes DeviceType { get; }

        public abstract byte[] Channels { get; }
    }
}
