﻿using DMX.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DMX.Tabs
{
    /// <summary>
    /// Interaction logic for TabLedSpot.xaml
    /// </summary>
    public partial class TabLedSpot : TabItem
    {
        private LedSpot ledSpot;

        public TabLedSpot(LedSpot ledSpotDevice)
        {
            InitializeComponent();
            this.DataContext = ledSpotDevice;
            ledSpot = ledSpotDevice;
            DmxDevice = ledSpotDevice;
        }

        private DmxDevice dmxDevice;
        public DmxDevice DmxDevice
        {
            get { return dmxDevice; }
            set { dmxDevice = value; }
        }

        // CH1
        private void SldrRed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ledSpot.UpdateRed((byte)sldrChannel1.Value);
        }

        // CH2
        private void SldrGreen_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ledSpot.UpdateGreen((byte)sldrChannel2.Value);
        }

        // CH3
        private void SldrBlue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ledSpot.UpdateBlue((byte)sldrChannel3.Value);
        }

        // CH4
        private void SldrMacro_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ledSpot.UpdateMacro((byte)sldrChannel4.Value);
        }

        // CH5
        private void SldrSpeedStrobe_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ledSpot.UpdateSpeedStrobe((byte)sldrChannel5.Value);
        }

        // CH6
        private void SldrMode_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ledSpot.UpdateMode((byte)sldrChannel6.Value);
        }
    }
}
