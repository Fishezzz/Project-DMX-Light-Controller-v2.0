using DMX.Entities.Enumerations;
using System;
using System.ComponentModel;

namespace DMX.Entities
{
    public class LedSpot : DmxDevice, INotifyPropertyChanged
    {
        const int NUMBER_OF_CHANNELS = 6;

        public LedSpot(string name, int startAddress, DmxDeviceTypes deviceType)
            : base(name, startAddress, deviceType, NUMBER_OF_CHANNELS)
        { }

        // CH1
        private string red = "OFF";
        public string Red
        {
            get { return red; }
            set
            {
                red = value;
                OnPropertyChanged("Red");
            }
        }

        // CH2
        private string green = "OFF";
        public string Green
        {
            get { return green; }
            set
            {
                green = value;
                OnPropertyChanged("Green");
            }
        }

        // CH3
        private string blue = "OFF";
        public string Blue
        {
            get { return blue; }
            set
            {
                blue = value;
                OnPropertyChanged("Blue");
            }
        }

        // CH4
        private string macro = "OFF";
        public string Macro
        {
            get { return macro; }
            set
            {
                macro = value;
                OnPropertyChanged("Macro");
            }
        }

        // CH5
        private string speedStrobe = "OFF";
        public string SpeedStrobe
        {
            get { return speedStrobe; }
            set
            {
                speedStrobe = value;
                OnPropertyChanged("SpeedStrobe");
            }
        }

        // CH6
        private string mode = "Color setting";
        public string Mode
        {
            get { return mode; }
            set
            {
                mode = value;
                OnPropertyChanged("Mode");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        // CH1
        public void UpdateRed(byte? red)
        {
            Channels[0] = red ?? 0;

            if (Channels[3] >= 0 && Channels[3] <= 15)
            {
                if (Channels[5] >= 0 && Channels[5] <= 31)
                {
                    if (Channels[0] == 0)
                        Red = "OFF";
                    else if (Channels[0] <= 255)
                        Red = string.Format("{0:F0}", 255 * (Channels[0] - 1) / (double)254);
                    else
                        Red = "??";
                }
                else if (Channels[5] <= 127)
                {
                    if (Channels[0] >= 0 && Channels[0] <= 15)
                        Red = "OFF";
                    else if (Channels[0] <= 255)
                        Red = string.Format("{0:F0}", 255 * (Channels[0] - 15) / (double)240);
                    else
                        Red = "??";
                }
                else if (Channels[5] <= 255)
                    Red = "NA";
                else
                    Red = "??";
            }
            else if (Channels[3] <= 255)
                Red = "NA";
            else
                Red = "??";
        }

        // CH2
        public void UpdateGreen(byte? green)
        {
            Channels[1] = green ?? 0;

            if (Channels[3] >= 0 && Channels[3] <= 15)
            {
                if (Channels[5] >= 0 && Channels[5] <= 31)
                {
                    if (Channels[1] == 0)
                        Green = "OFF";
                    else if (Channels[1] <= 255)
                        Green = string.Format("{0:F0}", 255 * (Channels[1] - 1) / (double)254);
                    else
                        Green = "??";
                }
                else if (Channels[5] <= 127)
                {
                    if (Channels[1] >= 0 && Channels[1] <= 15)
                        Green = "OFF";
                    else if (Channels[1] <= 255)
                        Green = string.Format("{0:F0}", 255 * (Channels[1] - 15) / (double)240);
                    else
                        Green = "??";
                }
                else if (Channels[5] <= 255)
                    Green = "NA";
                else
                    Green = "??";
            }
            else if (Channels[3] <= 255)
                Green = "NA";
            else
                Green = "??";
        }

        // CH3
        public void UpdateBlue(byte? blue)
        {
            Channels[2] = blue ?? 0;

            if (Channels[3] >= 0 && Channels[3] <= 15)
            {
                if (Channels[5] >= 0 && Channels[5] <= 31)
                {
                    if (Channels[2] == 0)
                        Blue = "OFF";
                    else if (Channels[2] <= 255)
                        Blue = string.Format("{0:F0}", 255 * (Channels[2] - 1) / (double)254);
                    else
                        Blue = "??";
                }
                else if (Channels[5] <= 127)
                {
                    if (Channels[2] >= 0 && Channels[2] <= 15)
                        Blue = "OFF";
                    else if (Channels[2] <= 255)
                        Blue = string.Format("{0:F0}", 255 * (Channels[2] - 15) / (double)240);
                    else
                        Blue = "??";
                }
                else if (Channels[5] <= 255)
                    Blue = "NA";
                else
                    Blue = "??";
            }
            else if (Channels[3] <= 255)
                Blue = "NA";
            else
                Blue = "??";
        }

        // CH4
        public void UpdateMacro(byte? macro)
        {
            Channels[3] = macro ?? 0;

            if (Channels[3] >= 0 && Channels[3] <= 15)
                Macro = "OFF";
            else if (Channels[3] <= 255)
                Macro = "ON";
            else
                Macro = "??";

            UpdateMode(Channels[5]);
        }

        // CH5
        public void UpdateSpeedStrobe(byte? speedStrobe)
        {
            Channels[4] = speedStrobe ?? 0;

            if (Channels[3] >= 0 && Channels[3] <= 15)
            {
                if (Channels[5] >= 0 && Channels[5] <= 31)
                {
                    if (Channels[4] >= 0 && Channels[4] <= 15)
                        SpeedStrobe = "OFF";
                    else if (Channels[4] <= 255)
                        SpeedStrobe = string.Format("Strobe speed {0}", Math.Ceiling((Channels[4] - 15) / (double)15));
                    else
                        SpeedStrobe = "??";
                }
                else if (Channels[5] <= 223)
                    SpeedStrobe = string.Format("LED speed {0:F2}%", 100 * Channels[4] / (double)255);
                else if (Channels[5] <= 255)
                    SpeedStrobe = "NA";
                else
                    SpeedStrobe = "??";
            }
            else if (Channels[3] <= 255)
                SpeedStrobe = string.Format("Strobe {0:F2}%", 100 * Channels[4] / (double)255);
            else
                SpeedStrobe = "??";
        }

        // CH6
        public void UpdateMode(byte? mode)
        {
            Channels[5] = mode ?? 0;

            if (Channels[3] >= 0 && Channels[3] <= 15)
            {
                if (Channels[5] >= 0 && Channels[5] <= 31)
                    Mode = "Color setting";
                else if (Channels[5] <= 63)
                    Mode = "Fade Out";
                else if (Channels[5] <= 95)
                    Mode = "Fade In";
                else if (Channels[5] <= 127)
                    Mode = "Fade In & Out";
                else if (Channels[5] <= 159)
                    Mode = "Color mix";
                else if (Channels[5] <= 191)
                    Mode = "3-Color flash";
                else if (Channels[5] <= 223)
                    Mode = "7-Color flash";
                else if (Channels[5] <= 255)
                    Mode = "Sound action";
                else
                    Mode = "??";
            }
            else if (Channels[3] >= 16 && Channels[3] <= 255)
                Mode = "NA";
            else
                Mode = "??";

            UpdateRed(Channels[0]);
            UpdateGreen(Channels[1]);
            UpdateBlue(Channels[2]);
            UpdateSpeedStrobe(Channels[4]);
        }
    }
}
