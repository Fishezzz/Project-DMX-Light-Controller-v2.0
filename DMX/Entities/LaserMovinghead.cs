using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMX.Entities.Enumerations;

namespace DMX.Entities
{
    public class LaserMovinghead : DmxDevice
    {
        const int NUMBER_OF_CHANNELS = 6;
        const double MAX_X_ROTATION = 540.0;
        const double MAX_Y_ROTATION = 180.0;
        const double DEGREES_PER_BYTE = 1 / 65536;

        public LaserMovinghead(string name, int startAddress, DmxDeviceTypes deviceType)
            : base(name, startAddress, deviceType)
        {
            Channels = new byte[NUMBER_OF_CHANNELS];
        }

        private byte[] channels;
        public byte[] Channels { get; private set; }

        public void OpenDatasheet()
        {
            System.Diagnostics.Process.Start(@"Datasheets\Datasheet Laser - ERO Laser.pdf");
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
        public double GetRotationX()
        {
            return MAX_X_ROTATION * (int)((channels[0] << 4) + channels[1]) * DEGREES_PER_BYTE;
        }
        // CH3 + CH4
        public double GetRotationY()
        {
            return MAX_Y_ROTATION * (int)((channels[2] << 4) + channels[3]) * DEGREES_PER_BYTE;
        }
        // CH5
        public double GetAxisSpeed()
        {
            return 100.0 * (int)channels[4] / 256;
        }
        // CH6
        public string GetShutterStatus()
        {
            if (channels[5] <= 7)
                return "Shutter closesd";
            else if (channels[5] <= 134)
                return "Shutter open"; /* $"Shutter {100.0 * (channels[5] - 7) / 127:D2}% open";   // 134-7=127 */
            else if (channels[5] <= 238)
                return $"Strobe speed {100.0 * (channels[5] - 134) / 104:D2}%";  // 238-134=104
            else if (channels[5] <= 255)
                return $"Shutter open";
            return "??";
        }
    }
}
