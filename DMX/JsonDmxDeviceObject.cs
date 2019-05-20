namespace DMX
{
    public class JsonDmxDeviceObject
    {
        public JsonDmxDeviceObject()
        { }

        private string name;
        public string Name { get; set; }

        private int startAddress;
        public int StartAddress { get; set; }

        private int deviceType;
        public int DeviceType { get; set; }

        private int numberOfChannels;
        public int NumberOfChannels { get; set; }
    }
}
