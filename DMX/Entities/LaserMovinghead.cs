using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMX.Entities.Enumerations;

namespace DMX.Entities
{
    public class LaserMovinghead : DmxDevice, INotifyPropertyChanged
    {
        const int NUMBER_OF_CHANNELS = 6;
        const double MAX_X_ROTATION = 540.0;
        const double MAX_Y_ROTATION = 180.0;
        const int DEGREES_PER_BYTE = 65535;

        public LaserMovinghead(string name, int startAddress, DmxDeviceTypes deviceType)
            : base(name, startAddress, deviceType)
        {
            Channels = new byte[NUMBER_OF_CHANNELS];
        }

        // Channels
        private byte[] channels;
        public byte[] Channels
        {
            get { return channels; }
            private set { channels = value; }
        }

        // CH1 + CH2
        private string rotationX = "0,00°";
        public string RotationX
        {
            get { return rotationX; }
            private set
            {
                rotationX = value;
                OnPropertyChanged("RotationX");
            }
        }

        // CH3 + CH4
        private string rotationY = "0,00°";
        public string RotationY
        {
            get { return rotationY; }
            private set
            {
                rotationY = value;
                OnPropertyChanged("RotationY");
            }
        }

        // CH5
        private string axisSpeed = "0,00%";
        public string AxisSpeed
        {
            get { return axisSpeed; }
            set
            {
                axisSpeed = value;
                OnPropertyChanged("AxisSpeed");
            }
        }

        // CH6
        private string shutterStatus = "Shutter closed";
        public string ShutterStatus
        {
            get { return shutterStatus; }
            set
            {
                shutterStatus = value;
                OnPropertyChanged("ShutterStatus");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        // CH1 + CH2
        public void UpdateRotationX(byte? X, byte? X_fine)
        {
            channels[0] = X ?? 0;
            channels[1] = X_fine ?? 0;
            RotationX = string.Format("{0:F2}°", MAX_X_ROTATION * (((int)channels[0] << 8) + (int)channels[1]) / DEGREES_PER_BYTE);
        }

        // CH3 + CH4
        public void UpdateRotationY(byte? Y, byte? Y_fine)
        {
            channels[2] = Y ?? 0;
            channels[3] = Y_fine ?? 0;
            RotationY = string.Format("{0:F2}°", MAX_Y_ROTATION * (((int)channels[2] << 8) + (int)channels[3]) / DEGREES_PER_BYTE);
        }

        // CH5
        public void UpdateAxisSpeed(byte? speed)
        {
            channels[4] = speed ?? 0;
            AxisSpeed = string.Format("{0:F2}%", 100.0 * channels[4] / 255);
        }

        // CH6
        public void UpdateShutterStatus(byte? status)
        {
            channels[5] = status ?? 0;

            if (channels[5] >= 0 && channels[5] <= 7)
                ShutterStatus = "Shutter closed";
            else if (channels[5] <= 134)
                ShutterStatus = string.Format("Shutter {0:F2}% open", 100.0 * (channels[5] - 7) / 127);  // 134-7=127
            else if (channels[5] <= 238)
                ShutterStatus = string.Format("Strobe speed {0:F2}%", 100.0 * (channels[5] - 134) / 104);    // 238-134=104
            else if (channels[5] <= 255)
                ShutterStatus = $"Shutter open";
            else
                ShutterStatus = "??";
        }

        public void OpenDatasheet()
        {
            System.Diagnostics.Process.Start(@"Datasheets\Datasheet Laser - ERO Laser.pdf");
        }
    }
}
