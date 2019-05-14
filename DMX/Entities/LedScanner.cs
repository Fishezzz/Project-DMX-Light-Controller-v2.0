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
        const int NUMBER_OF_CHANNELS = 11;
        const double MAX_X_ROTATION = 180.0;
        const double MAX_Y_ROTATION = 75.0;
        const double DEGREES_PER_BYTE = 1 / 65535.0;

        public LedScanner(string name, int startAddress, DmxDeviceTypes deviceType)
            : base(name, startAddress, deviceType)
        {
            Channels = new byte[NUMBER_OF_CHANNELS];
        }

        private byte[] channels;
        public byte[] Channels { get; private set; }

        public void OpenDatasheet()
        {
            System.Diagnostics.Process.Start(@"Datasheet LED Scanner - ALO60.pdf");
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

        // CH1 + CH2
        public string GetRotationX()
        {
            return string.Format("X: {0:F2}°", MAX_X_ROTATION * ((channels[0] << 8) + channels[1]) * (double)DEGREES_PER_BYTE);
        }
        // CH3 + CH4
        public string GetRotationY()
        {
            return string.Format("Y: {0:F2}°", MAX_Y_ROTATION * ((channels[2] << 8) + channels[3]) * (double)DEGREES_PER_BYTE);
        }
        // CH5
        public string GetAxisSpeed()
        {
            return string.Format("Speed: {0:F2}%", 100 * (1 - (channels[4] / (double)255)));    // 100->0%
        }
        // CH6
        public string GetDimmerStatus()
        {
            return string.Format("Dimmed {0:F2}%", 100 * channels[5] / (double)255);
        }
        // CH7
        public string GetShutterStrobeStatus()
        {
            if (channels[6] >= 0 && channels[6] <= 7)
                return "Blackout";
            else if (channels[6] <= 15)
                return "Shutter open";
            else if (channels[6] <= 131)
                return string.Format("Strobe speed {0:F2}%", 100 * (channels[6] - 15) / (double)116);  // 131-15=116
            else if (channels[6] <= 139)
                return "Shutter open";
            else if (channels[6] <= 181)
                return "Slow open, fast close";
            else if (channels[6] <= 189)
                return "Shutter open";
            else if (channels[6] <= 231)
                return "Fast open, slow close";
            else if (channels[6] <= 239)
                return "Shutter open";
            else if (channels[6] <= 247)
                return "Random strobe";
            else if (channels[6] <= 255)
                return "Shutter open";
            return "??";
        }
        // CH10
        public string GetGoboRotation()
        {
            if (channels[9] >= 0 && channels[9] <= 127)
                return string.Format("Rotation: {0:F2}°", 360 * channels[9] / (double)128);
            else if (channels[9] <= 189)
                return "CW fast to slow";
            else if (channels[9] <= 193)
                return "Stop";
            else if (channels[9] <= 255)
                return "CCW slow to fast";
            return "??";
        }
        // CH8 + CH9 + CH11 => ListBox
    }
}
