using DMX;
using DMX.Entities;
using DMX.Entities.Enumerations;
using DMX.Tabs;
using Logging;
using Project_DMX_2._0.Event_Args;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace Project_DMX_2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Logger logger;
        public List<DmxDevice> _dmxDevices;
        NewDeviceUI _newDeviceUI;
        SettingsUI _settingsUI;

        SerialPort _sp;
        byte[] _data = new byte[513];
        DispatcherTimer _dt;

        TabLedMovinghead tabLedMovinghead;

        public MainWindow()
        {
            InitializeComponent();
            logger = Logger.GetLogger;
            logger.Log("Initialized application at " + DateTime.Now.ToString());

            _sp = new SerialPort();
            logger.Log("Serial port instance created");
            _sp.BaudRate = 250000;
            _sp.StopBits = StopBits.Two;

            _dt = new DispatcherTimer();
            logger.Log("Dispatcher timer instance created");
            _dt.Interval = TimeSpan.FromMilliseconds(23);
            _dt.Tick += _dt_Tick;

            TabLaserMovinghead tabLaserMovinghead = new TabLaserMovinghead(new LaserMovinghead("Ayra Laser Movinghead", 120, DmxDeviceTypes.Ayra_LedLaserMovinghead));
            tctDeviceTabs.Items.Add(tabLaserMovinghead);
            logger.Log("tabLaserMovinghead added to tctDeviceTabs in MainWindow");

            tabLedMovinghead = new TabLedMovinghead(new LedMovinghead("Skytec LED Movinghead", 120, DmxDeviceTypes.Skytec_LedMovinghead));
            tctDeviceTabs.Items.Add(tabLedMovinghead);
            logger.Log("tabLedMovinghead added to tctDeviceTabs in MainWindow");

            _dt.Start();
            logger.Log("Dispatcher timer started...");
        }

        private void NewDeviceUI_NewDmxDevice(object sender, NewDmxDeviceEventArgs e)
        {
            _newDeviceUI = null;
            _dmxDevices.Add(e.DmxDevice);
            logger.Log("New DmxDevice added: " + e.DmxDevice.Name + " @ " + e.DmxDevice.StartAddress);
        }

        private void NewDevice_Click(object sender, RoutedEventArgs e)
        {
            _newDeviceUI = new NewDeviceUI();
            _newDeviceUI.NewDmxDevice += new EventHandler<NewDmxDeviceEventArgs>(NewDeviceUI_NewDmxDevice);
            _newDeviceUI.ShowDialog();
        }

        private void SettingsUI_SettingsUpdated(object sender, SettingsEventArgs e)
        {
            _settingsUI = null;
            _sp.PortName = e.ComPort;
            logger.Log("COM port changed to " + e.ComPort);

            if (!_sp.IsOpen && _sp.PortName != "None")
            {
                _sp.Open();
                logger.Log("Serial port opened");
            }
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            _settingsUI = new SettingsUI(_sp.PortName);
            _settingsUI.SettingsUpdated += new EventHandler<SettingsEventArgs>(SettingsUI_SettingsUpdated);
            _settingsUI.ShowDialog();
        }

        private void _dt_Tick(object sender, EventArgs e)
        {
            //  foreach (TabItem tabItem in TabControl)
            //  {
            //      byte[] channels = tabItem.<DmxDevice>.Channels;
            //      int startAddress = tabItem.<DmxDevice>.StartAddress;
            //      for (int i = 0; i < channels.Length; i++)
            //          _data[startAddress + i] = channels[i];
            //  }
            byte[] temp = tabLedMovinghead.LedMovinghead.Channels;

            TransferData();
        }

        public void TransferData()
        {
            if (_sp != null && _sp.IsOpen)
            {
                _sp.BreakState = true;
                Thread.Sleep(1);
                _sp.BreakState = false;
                Thread.Sleep(1);
                _sp.Write(_data, 0, 513);
            }
        }

        private void Main_Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_newDeviceUI != null)
                _newDeviceUI.Close();
            logger.Warn("Closing application.....");
            logger = null;
        }
    }
}
