using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMX.Entities.Enumerations;

namespace DMX.Entities
{
    public class LedMovinghead : DmxDevice, INotifyPropertyChanged
    {
        const int NUMBER_OF_CHANNELS = 13;
        const double MAX_X_ROTATION = 540.0;
        const double MAX_Y_ROTATION = 270.0;
        const double DEGREES_PER_BYTE = 1 / 65536;

        public LedMovinghead(string name, int startAddress, DmxDeviceTypes deviceType)
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

        // CH7 + CH8 + CH9
        private string rgbColor = "#000000";
        public string RGBColor
        {
            get { return rgbColor; }
            private set
            {
                rgbColor = value;
                OnPropertyChanged("RGBColor");
            }
        }

        // CH11
        private string ledSpeed = "0,00%";
        public string LedSpeed
        {
            get { return ledSpeed; }
            private set
            {
                ledSpeed = value;
                OnPropertyChanged("LedSpeed");
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

            if (channels[5] >= 0 && channels[5] <= 9)
                ShutterStatus = "Shutter closed";
            else if (channels[5] <= 134)
                ShutterStatus = string.Format("Shutter {0:F2}% open", 100.0 * (1 - (channels[5] - 9) / 125)); // 134-9=125   100->0%
            else if (channels[5] <= 239)
                ShutterStatus = string.Format("Strobe speed {0:F2}%", 100.0 * (channels[5] - 134) / 105);  // 239-134=105
            else if (channels[5] <= 255)
                ShutterStatus = "Shutter open";
            else
                ShutterStatus = "??";
        }

        // CH7 + CH8 + CH9
        public void GetRGB(byte? R, byte? G, byte? B)
        {
            channels[6] = R ?? 0;
            channels[7] = G ?? 0;
            channels[8] = B ?? 0;

            RGBColor = string.Format("#{0:X2}{1:X2}{2:X2}", channels[6], channels[7], channels[8]);
        }

        // CH11
        public void GetLedSpeed(byte? speed)
        {
            channels[10] = speed ?? 0;

            if (channels[10] >= 0 && channels[10] <= 255)
                LedSpeed = string.Format("Speed: {0:F2}%", 100.0 * (1 - channels[10] / 255));   // 100->0%
            else
                LedSpeed = "??";
        }

        public void OpenDatasheet()
        {
            System.Diagnostics.Process.Start(@"Datasheets\Skytec LED Movinghead.pdf");
        }
    }
}
