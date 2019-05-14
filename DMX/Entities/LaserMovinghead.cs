using DMX.Entities.Enumerations;
using System.ComponentModel;

namespace DMX.Entities
{
    public class LaserMovinghead : DmxDevice, INotifyPropertyChanged
    {
        const int NUMBER_OF_CHANNELS = 6;
        const double MAX_X_ROTATION = 540.0;
        const double MAX_Y_ROTATION = 180.0;
        const double DEGREES_PER_BYTE = 1 / 65535.0;

        public LaserMovinghead(string name, int startAddress, DmxDeviceTypes deviceType)
            : base(name, startAddress, deviceType, new byte[NUMBER_OF_CHANNELS])
        { }

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
            Channels[0] = X ?? 0;
            Channels[1] = X_fine ?? 0;
            RotationX = string.Format("{0:F2}°", MAX_X_ROTATION * (((int)Channels[0] << 8) + (int)Channels[1]) * (double)DEGREES_PER_BYTE);
        }

        // CH3 + CH4
        public void UpdateRotationY(byte? Y, byte? Y_fine)
        {
            Channels[2] = Y ?? 0;
            Channels[3] = Y_fine ?? 0;
            RotationY = string.Format("{0:F2}°", MAX_Y_ROTATION * (((int)Channels[2] << 8) + (int)Channels[3]) * (double)DEGREES_PER_BYTE);
        }

        // CH5
        public void UpdateAxisSpeed(byte? speed)
        {
            Channels[4] = speed ?? 0;
            AxisSpeed = string.Format("{0:F2}%", 100 * Channels[4] / (double)255);
        }

        // CH6
        public void UpdateShutterStatus(byte? status)
        {
            Channels[5] = status ?? 0;

            if (Channels[5] >= 0 && Channels[5] <= 7)
                ShutterStatus = "Shutter closed";
            else if (Channels[5] <= 134)
                ShutterStatus = string.Format("Shutter {0:F2}% open", 100 * (Channels[5] - 7) / (double)127);  // 134-7=127
            else if (Channels[5] <= 238)
                ShutterStatus = string.Format("Strobe speed {0:F2}%", 100 * (Channels[5] - 134) / (double)104);    // 238-134=104
            else if (Channels[5] <= 255)
                ShutterStatus = $"Shutter open";
            else
                ShutterStatus = "??";
        }
    }
}
