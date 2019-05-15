using DMX.Entities;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DMX.Tabs
{
    /// <summary>
    /// Interaction logic for TabLedMovinghead.xaml
    /// </summary>
    public partial class TabLedMovinghead : TabItem
    {
        public TabLedMovinghead(LedMovinghead ledMovingheadDevice)
        {
            InitializeComponent();
            this.DataContext = ledMovinghead = ledMovingheadDevice;
            DmxDevice = LedMovinghead;
        }

        private DmxDevice dmxDevice;
        public DmxDevice DmxDevice
        {
            get { return dmxDevice; }
            set { dmxDevice = value; }
        }


        private LedMovinghead ledMovinghead;
        public LedMovinghead LedMovinghead
        {
            get { return ledMovinghead; }
            set { ledMovinghead = value; }
        }

        // CH1 + CH2
        private void SldrRotationX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ledMovinghead.UpdateRotationX((byte)sldrChannel1.Value, (byte)sldrChannel2.Value);
        }

        // CH3 + CH4
        private void SldrRotationY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ledMovinghead.UpdateRotationY((byte)sldrChannel3.Value, (byte)sldrChannel4.Value);
        }

        // CH5
        private void SldrAxisSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ledMovinghead.UpdateAxisSpeed((byte)sldrChannel5.Value);
        }

        // CH6
        private void SldrShutterStatus_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ledMovinghead.UpdateShutterStatus((byte)sldrChannel6.Value);
        }

        // CH7 + CH8 + CH9
        private void BudRGB_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (budChannel7 != null && budChannel8 != null && budChannel9 != null && ledMovinghead != null)
                ledMovinghead.UpdateRGB(budChannel7.Value, budChannel8.Value, budChannel9.Value);
        }

        // CH10
        private void rb_ColorsChecked(object sender, RoutedEventArgs e)
        {
            // Waarde hardcoded doen met switch
            int index = Convert.ToInt32(((RadioButton)sender).Name.Split('_')[1]);
            ledMovinghead.UpdatePreSetColor((byte)(index * 14));
        }

        // CH11
        private void SldrLedSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ledMovinghead.UpdateLedSpeed((byte)sldrChannel11.Value);
        }

        // CH12
        private void rb_ProgramsChecked(object sender, RoutedEventArgs e)
        {
            // Waarde hardcoded doen met switch
            int index = Convert.ToInt32(((RadioButton)sender).Name.Split('_')[1]);
            ledMovinghead.UpdateAutoProgram((byte)(index * 15));
        }

        // CH13
        private void rb_GobosChecked(object sender, RoutedEventArgs e)
        {
            // Waarde hardcoded doen met switch
            int index = Convert.ToInt32(((RadioButton)sender).Name.Split('_')[1]);
            if (index == 12)
                ledMovinghead.UpdateGobo((byte)200);
            else
                ledMovinghead.UpdateGobo((byte)(index * 14));
        }
    }
}
