using DMX.Entities.Enumerations;
using System.ComponentModel;

namespace DMX.Entities
{
    public class LedScanner : DmxDevice, INotifyPropertyChanged
    {
        const int NUMBER_OF_CHANNELS = 11;
        const double MAX_X_ROTATION = 180.0;
        const double MAX_Y_ROTATION = 75.0;
        const double DEGREES_PER_BYTE = 1 / 65535.0;

        public LedScanner(string name, int startAddress, DmxDeviceTypes deviceType)
            : base(name, startAddress, deviceType, NUMBER_OF_CHANNELS)
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
        private string axisSpeed = "100,00%";
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
        private string dimmerStatus = "0,00%";
        public string DimmerStatus
        {
            get { return dimmerStatus; }
            set
            {
                dimmerStatus = value;
                OnPropertyChanged("DimmerStatus");
            }
        }

        // CH7
        private string shutterStatus = "Blackout";
        public string ShutterStatus
        {
            get { return shutterStatus; }
            set
            {
                shutterStatus = value;
                OnPropertyChanged("ShutterStatus");
            }
        }

        // CH10
        private string goboRotation = "0,00°";
        public string GoboRotation
        {
            get { return goboRotation; }
            set
            {
                goboRotation = value;
                OnPropertyChanged("GoboRotation");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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
            AxisSpeed = string.Format("{0:F2}%", 100 * (1 - (Channels[4] / (double)255)));  // 100->0%
        }

        // CH6
        public void UpdateDimmerStatus(byte? dimmer)
        {
            Channels[5] = dimmer ?? 0;
            DimmerStatus = string.Format("{0:F2}%", 100 * Channels[5] / (double)255);
        }

        // CH7
        public void UpdateShutterStatus(byte? status)
        {
            Channels[6] = status ?? 0;

            if (Channels[6] >= 0 && Channels[6] <= 7)
                ShutterStatus = "Blackout";
            else if (Channels[6] <= 15)
                ShutterStatus = "Shutter open";
            else if (Channels[6] <= 131)
                ShutterStatus = string.Format("Strobe speed {0:F2}%", 100 * (Channels[6] - 15) / (double)116);  // 131-15=116
            else if (Channels[6] <= 139)
                ShutterStatus = "Shutter open";
            else if (Channels[6] <= 181)
                ShutterStatus = "Slow open, fast close";
            else if (Channels[6] <= 189)
                ShutterStatus = "Shutter open";
            else if (Channels[6] <= 231)
                ShutterStatus = "Fast open, slow close";
            else if (Channels[6] <= 239)
                ShutterStatus = "Shutter open";
            else if (Channels[6] <= 247)
                ShutterStatus = "Random strobe";
            else if (Channels[6] <= 255)
                ShutterStatus = "Shutter open";
            else
                ShutterStatus = "??";
        }

        // CH8
        public void UpdatePreSetColor(byte? color)
        {
            Channels[7] = color ?? 0;
        }

        // CH9
        public void UpdateGobo(byte? gobo)
        {
            Channels[8] = gobo ?? 0;
        }

        // CH10
        public void UpdateGoboRotation(byte? rotation)
        {
            Channels[9] = rotation ?? 0;

            if (Channels[9] >= 0 && Channels[9] <= 127)
                GoboRotation = string.Format("{0:F2}°", 360 * Channels[9] / (double)128);
            else if (Channels[9] <= 189)
                GoboRotation = string.Format("CW {0:F0}%", 100 * (1 - ((Channels[9] - 127) / (double)62)));    // 189-127=62   100->0%
            else if (Channels[9] <= 193)
                GoboRotation = "Stop";
            else if (Channels[9] <= 255)
                GoboRotation = string.Format("CCW {0:F0}%", 100 * (Channels[9] - 193) / (double)62);   // 255-193=62
            else
                GoboRotation = "??";
        }

        // CH11
        public void UpdateSpecialFunction(byte? function)
        {
            Channels[10] = function ?? 0;
        }
    }
}
