using DMX.Entities.Enumerations;
using System;

namespace DMX
{
    public class DmxDevice
    {
        public DmxDevice(string name, int startAddress, DmxDeviceTypes deviceType, int numberOfChannels)
        {
            Name = name;
            StartAddress = startAddress;
            DeviceType = deviceType;
            Channels = new byte[numberOfChannels];
        }

        public DmxDevice(JsonDmxDeviceObject jsonDmxDeviceObject)
        {
            Name = jsonDmxDeviceObject.Name;
            StartAddress = jsonDmxDeviceObject.StartAddress;
            DeviceType = Enum.IsDefined(typeof(DmxDeviceTypes), jsonDmxDeviceObject.DeviceType) ? (DmxDeviceTypes)jsonDmxDeviceObject.DeviceType : DmxDeviceTypes.Unknown;
            Channels = new byte[jsonDmxDeviceObject.NumberOfChannels];
        }

        private readonly string name;
        public virtual string Name { get; }

        private readonly int startAddress;
        public virtual int StartAddress { get; }

        private readonly DmxDeviceTypes deviceType;
        public virtual DmxDeviceTypes DeviceType { get; }

        private readonly byte[] channels;
        public virtual byte[] Channels { get; private set; }
    }
}
