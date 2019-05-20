using DMX.Entities.Enumerations;
using System.ComponentModel;

namespace DMX.Entities
{
    public class LedMovinghead : DmxDevice, INotifyPropertyChanged
    {
        const int NUMBER_OF_CHANNELS = 13;
        const double MAX_X_ROTATION = 540.0;
        const double MAX_Y_ROTATION = 270.0;
        const double DEGREES_PER_BYTE = 1 / 65535.0;

        public LedMovinghead(string name, int startAddress, DmxDeviceTypes deviceType)
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
        private string ledSpeed = "100,00%";
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
            AxisSpeed = string.Format("{0:F2}%", 100 * Channels[4] / (double)255);
        }

        // CH6
        public void UpdateShutterStatus(byte? status)
        {
            Channels[5] = status ?? 0;

            if (Channels[5] >= 0 && Channels[5] <= 9)
                ShutterStatus = "Shutter closed";
            else if (Channels[5] <= 134)
                ShutterStatus = string.Format("Shutter {0:F2}% open", 100 * ((Channels[5] - 9) / (double)125)); // 134-9=125
            else if (Channels[5] <= 239)
                ShutterStatus = string.Format("Strobe speed {0:F2}%", 100 * (Channels[5] - 134) / (double)105);  // 239-134=105
            else if (Channels[5] <= 255)
                ShutterStatus = "Shutter open";
            else
                ShutterStatus = "??";
        }

        // CH7 + CH8 + CH9
        public void UpdateRGB(byte? R, byte? G, byte? B)
        {
            Channels[6] = R ?? 0;
            Channels[7] = G ?? 0;
            Channels[8] = B ?? 0;

            RGBColor = string.Format("#{0:X2}{1:X2}{2:X2}", Channels[6], Channels[7], Channels[8]);
        }

        // CH10
        public void UpdatePreSetColor(byte? color)
        {
            Channels[9] = color ?? 0;
        }

        // CH11
        public void UpdateLedSpeed(byte? speed)
        {
            Channels[10] = speed ?? 0;

            if (Channels[10] >= 0 && Channels[10] <= 255)
                LedSpeed = string.Format("{0:F2}%", 100 * (1 - (Channels[10] / (double)255)));  // 100->0%
            else
                LedSpeed = "??";
        }

        // CH12
        public void UpdateAutoProgram(byte? program)
        {
            Channels[11] = program ?? 0;
        }

        // CH13
        public void UpdateGobo(byte? gobo)
        {
            Channels[12] = gobo ?? 0;
        }
    }
}
