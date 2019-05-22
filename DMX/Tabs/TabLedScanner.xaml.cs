using DMX.Entities;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DMX.Tabs
{
    /// <summary>
    /// Interaction logic for TabLedScanner.xaml
    /// </summary>
    public partial class TabLedScanner : TabItem
    {
        private LedScanner ledScanner;

        public TabLedScanner(LedScanner ledScannerDevice)
        {
            InitializeComponent();
            this.DataContext = ledScannerDevice;
            ledScanner = ledScannerDevice;
            DmxDevice = ledScannerDevice;
        }

        private DmxDevice dmxDevice;
        public DmxDevice DmxDevice
        {
            get { return dmxDevice; }
            set { dmxDevice = value; }
        }

        // CH1 + CH2
        private void SldrRotationX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ledScanner.UpdateRotationX((byte)sldrChannel1.Value, (byte)sldrChannel2.Value);
        }

        // CH3 + CH4
        private void SldrRotationY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ledScanner.UpdateRotationY((byte)sldrChannel3.Value, (byte)sldrChannel4.Value);
        }

        // CH5
        private void SldrAxisSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ledScanner.UpdateAxisSpeed((byte)sldrChannel5.Value);
        }

        // CH6
        private void SldrDimmerStatus_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ledScanner.UpdateDimmerStatus((byte)sldrChannel6.Value);
        }

        // CH7
        private void SldrShutterStatus_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ledScanner.UpdateShutterStatus((byte)sldrChannel7.Value);
        }

        // CH8
        private void Rb_ColorsChecked(object sender, RoutedEventArgs e)
        {
            int index = Convert.ToInt32(((RadioButton)sender).Name.Split('_')[1]);
            if (index == 11)
                ledScanner.UpdatePreSetColor((byte)255);
            else
                ledScanner.UpdatePreSetColor((byte)((index - 1) * 15));
        }

        // CH9
        private void Rb_GobosChecked(object sender, RoutedEventArgs e)
        {
            int index = Convert.ToInt32(((RadioButton)sender).Name.Split('_')[1]);
            if (index == 17)
                ledScanner.UpdateGobo((byte)191);
            else if (index == 18)
                ledScanner.UpdateGobo((byte)225);
            else
                ledScanner.UpdateGobo((byte)((index - 1) * 9));
        }

        // CH10
        private void SldrGoboRotation_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ledScanner.UpdateGoboRotation((byte)sldrChannel10.Value);
        }

        // CH11
        private void Rb_ProgramsChecked(object sender, RoutedEventArgs e)
        {
            int index = Convert.ToInt32(((RadioButton)sender).Name.Split('_')[1]);
            if (index == 9)
                ledScanner.UpdateSpecialFunction((byte)205);
            else if (index == 10)
                ledScanner.UpdateSpecialFunction((byte)225);
            else if (index == 11)
                ledScanner.UpdateSpecialFunction((byte)250);
            else
                ledScanner.UpdateSpecialFunction((byte)(((index - 1) * 10) + 65));
        }
    }
}
