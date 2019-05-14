﻿using DMX;
using DMX.Entities;
using DMX.Entities.Enumerations;
using DMX.Tabs;
using Logging;
using Project_DMX_2._0.Event_Args;
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

namespace Project_DMX_2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Logger logger;
        public List<DmxDevice> _dmxDevices;
        NewDeviceUI newDeviceUI;

        public MainWindow()
        {
            InitializeComponent();
            logger = Logger.GetLogger;
            logger.Log("Initialized application at " + DateTime.Now.ToString());

            TabLaserMovinghead tabLaserMovinghead = new TabLaserMovinghead(new LaserMovinghead("Ayra Laser Movinghead", 120, DmxDeviceTypes.Ayra_LedLaserMovinghead));
            tctDeviceTabs.Items.Add(tabLaserMovinghead);
            logger.Log("tabLaserMovinghead added to tctDeviceTabs in MainWindow");

            TabLedMovinghead tabLedMovinghead = new TabLedMovinghead(new LedMovinghead("Skytec LED Movinghead", 120, DmxDeviceTypes.Skytec_LedMovinghead));
            tctDeviceTabs.Items.Add(tabLedMovinghead);
            logger.Log("tabLedMovinghead added to tctDeviceTabs in MainWindow");
        }

        private void NewDeviceUI_NewDmxDevice(object sender, NewDmxDeviceEventArgs e)
        {
            newDeviceUI = null;
            _dmxDevices.Add(e.DmxDevice);
            logger.Log("New DmxDevice added: " + e.DmxDevice.Name + " @ " + e.DmxDevice.StartAddress);
        }

        private void NewDevice_Click(object sender, RoutedEventArgs e)
        {
            newDeviceUI = new NewDeviceUI();
            newDeviceUI.NewDmxDevice += new EventHandler<NewDmxDeviceEventArgs>(NewDeviceUI_NewDmxDevice);
            newDeviceUI.ShowDialog();
        }

        private void Main_Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (newDeviceUI != null)
                newDeviceUI.Close();
            logger.Warn("Closing application.....");
            logger = null;
        }
    }
}
