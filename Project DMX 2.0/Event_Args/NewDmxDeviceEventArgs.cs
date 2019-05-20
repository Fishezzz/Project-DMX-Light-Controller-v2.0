using DMX;
using System;

namespace Project_DMX_2._0.Event_Args
{
    public class NewDmxDeviceEventArgs : EventArgs
    {
        public DmxDevice DmxDevice { get; set; }

        public NewDmxDeviceEventArgs(DmxDevice dmxDevice)
        {
            DmxDevice = dmxDevice;
        }
    }
}
