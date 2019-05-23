using DMX;
using DMX.Entities;
using DMX.Entities.Enumerations;
using DMX.Tabs;
using Logging;
using Newtonsoft.Json;
using Project_DMX_2._0.Event_Args;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Project_DMX_2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        Logger logger;
        List<DmxDevice> _dmxDevices;
        List<DmxDevice> _availableDevices;
        NewDeviceUI _newDeviceUI;
        SettingsUI _settingsUI;

        SerialPort _sp;
        byte[] _data = new byte[513];
        DispatcherTimer _dt;

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

            _dmxDevices = new List<DmxDevice>();

            string devicesJSON  = new System.IO.StreamReader(Environment.CurrentDirectory + "\\devices.json").ReadToEnd();
            logger.Log("devices.json read");
            List<JsonDmxDeviceObject> tempDmxDevices = JsonConvert.DeserializeObject<List<JsonDmxDeviceObject>>(devicesJSON);
            _availableDevices = new List<DmxDevice>();
            foreach (JsonDmxDeviceObject deviceObject in tempDmxDevices)
                _availableDevices.Add(new DmxDevice(deviceObject));
            logger.Log("All available devices added");

            _dt.Start();
            logger.Log("Dispatcher timer started...");
        }

        private void NewDeviceUI_NewDmxDevice(object sender, NewDmxDeviceEventArgs e)
        {
            _newDeviceUI = null;
            //_dmxDevices.Add(_availableDevices.Find(x => x.StartAddress == e.DmxDevice.StartAddress));
            logger.Log("New DmxDevice added: " + e.DmxDevice.Name + " @ " + e.DmxDevice.StartAddress);
            switch (e.DmxDevice.DeviceType)
            {
                case DmxDeviceTypes.Skytec_LedMovinghead:
                    TabLedMovinghead tempTabLedMovinghead = new TabLedMovinghead(new LedMovinghead(e.DmxDevice.Name, e.DmxDevice.StartAddress, e.DmxDevice.DeviceType));
                    _dmxDevices.Add(tempTabLedMovinghead.DmxDevice);
                    tctDeviceTabs.Items.Add(tempTabLedMovinghead);
                    logger.Log("TabLedMovinghead added");
                    tctDeviceTabs.SelectedItem = tempTabLedMovinghead;
                    sbiStartAddress.Content = tempTabLedMovinghead.DmxDevice.StartAddress;
                    break;
                case DmxDeviceTypes.Ayra_LedLaserMovinghead:
                    //TabLaserMovinghead tempTabLaserMovinghead = new TabLaserMovinghead(new LaserMovinghead(e.DmxDevice.Name, e.DmxDevice.StartAddress, e.DmxDevice.DeviceType));
                    TabLaserMovinghead tempTabLaserMovinghead = new TabLaserMovinghead(new LaserMovinghead(_availableDevices.Find(x => x.StartAddress == e.DmxDevice.StartAddress)));
                    _dmxDevices.Add(tempTabLaserMovinghead.DmxDevice);
                    tctDeviceTabs.Items.Add(tempTabLaserMovinghead);
                    logger.Log("TabLaserMovinghead added");
                    tctDeviceTabs.SelectedItem = tempTabLaserMovinghead;
                    sbiStartAddress.Content = tempTabLaserMovinghead.DmxDevice.StartAddress;
                    break;
                case DmxDeviceTypes.Ayra_LedScanner:
                    TabLedScanner tempTabLedScanner = new TabLedScanner(new LedScanner(e.DmxDevice.Name, e.DmxDevice.StartAddress, e.DmxDevice.DeviceType));
                    _dmxDevices.Add(tempTabLedScanner.DmxDevice);
                    tctDeviceTabs.Items.Add(tempTabLedScanner);
                    logger.Log("TabLedScanner added");
                    tctDeviceTabs.SelectedItem = tempTabLedScanner;
                    sbiStartAddress.Content = tempTabLedScanner.DmxDevice.StartAddress;
                    break;
                case DmxDeviceTypes.EuroLite_LedPanel:
                    TabLedPanel tempTabLedPanel = new TabLedPanel(new LedPanel(e.DmxDevice.Name, e.DmxDevice.StartAddress, e.DmxDevice.DeviceType));
                    _dmxDevices.Add(tempTabLedPanel.DmxDevice);
                    tctDeviceTabs.Items.Add(tempTabLedPanel);
                    logger.Log("TabLedPanel added");
                    tctDeviceTabs.SelectedItem = tempTabLedPanel;
                    sbiStartAddress.Content = tempTabLedPanel.DmxDevice.StartAddress;
                    break;
                case DmxDeviceTypes.Showtec_LedSpot:
                    TabLedSpot tempTabLedSpot = new TabLedSpot(new LedSpot(e.DmxDevice.Name, e.DmxDevice.StartAddress, e.DmxDevice.DeviceType));
                    _dmxDevices.Add(tempTabLedSpot.DmxDevice);
                    tctDeviceTabs.Items.Add(tempTabLedSpot);
                    logger.Log("TabLedSpot added");
                    tctDeviceTabs.SelectedItem = tempTabLedSpot;
                    sbiStartAddress.Content = tempTabLedSpot.DmxDevice.StartAddress;
                    break;
                case DmxDeviceTypes.Unknown:
                case DmxDeviceTypes.None:
                default:
                    logger.Warn("Cannot add device tab because DeviceType is None/Unknown");
                    break;
            }
            _availableDevices.Remove(_availableDevices.Find(x => x.StartAddress == e.DmxDevice.StartAddress));
        }

        private void NewDevice_Click(object sender, RoutedEventArgs e)
        {
            _newDeviceUI = new NewDeviceUI(_availableDevices);
            _newDeviceUI.NewDmxDevice += new EventHandler<NewDmxDeviceEventArgs>(NewDeviceUI_NewDmxDevice);
            _newDeviceUI.ShowDialog();
        }

        private void SettingsUI_SettingsUpdated(object sender, SettingsEventArgs e)
        {
            _settingsUI = null;
            if (_sp != null && _sp.IsOpen)
            {
                TransferData(new byte[513]);
                _sp.Close();
            }

            _sp.PortName = e.ComPort;
            logger.Log("COM port changed to " + e.ComPort);

            if (!_sp.IsOpen && _sp.PortName != "None")
            {
                try
                {
                    _sp.Open();
                    logger.Log("Serial port opened");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("This COM port cannot be opened!\nCOM port will be set to \"None\"", "COM Port Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    _sp.PortName = "None";
                    logger.Error(ex);
                }
            }
            sbiComPort.Content = _sp.PortName;
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            _settingsUI = new SettingsUI(_sp.PortName);
            _settingsUI.SettingsUpdated += new EventHandler<SettingsEventArgs>(SettingsUI_SettingsUpdated);
            _settingsUI.ShowDialog();
        }

        private void RemoveSelectedDevice_Click(object sender, RoutedEventArgs e)
        {
            if (tctDeviceTabs.SelectedIndex >= 0 && tctDeviceTabs.SelectedItem != null)
            {
                DmxDevice tempDmxDevice = _dmxDevices[tctDeviceTabs.SelectedIndex];
                MessageBoxResult result = MessageBox.Show("Are you sure you want to remove " + tempDmxDevice.Name + "?\nThis can't be undone.", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Stop, MessageBoxResult.No);
                if (result == MessageBoxResult.Yes)
                {
                    _availableDevices.Add(tempDmxDevice);
                    logger.Log("DmxDevice removed: " + tempDmxDevice.Name + " @ " + _dmxDevices[tctDeviceTabs.SelectedIndex].StartAddress);
                    _dmxDevices.Remove(tempDmxDevice);
                    tctDeviceTabs.Items.Remove(tctDeviceTabs.SelectedItem);
                }
            }
        }

        private void TctDeviceTabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                TabControl tc = sender as TabControl;
                try
                {
                    if (tc.SelectedIndex >= 0)
                        sbiStartAddress.Content = _dmxDevices[tc.SelectedIndex].StartAddress;
                    else
                        sbiStartAddress.Content = string.Empty;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void _dt_Tick(object sender, EventArgs e)
        {
            foreach (DmxDevice dmxDevice in _dmxDevices)
            {
                byte[] channels = dmxDevice.Channels;
                int startAddress = dmxDevice.StartAddress;
                for (int i = 0; i < channels.Length; i++)
                    _data[startAddress + i] = channels[i];
            }

            TransferData(_data);
        }

        public void TransferData(byte[] data)
        {
            if (_sp != null && _sp.IsOpen)
            {
                _sp.BreakState = true;
                Thread.Sleep(1);
                _sp.BreakState = false;
                Thread.Sleep(1);
                _sp.Write(data, 0, 513);
            }
        }

        private void Main_Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_sp.IsOpen)
                TransferData(new byte[513]);

            if (_sp != null)
            {
                if (_sp.IsOpen)
                    TransferData(new byte[513]);
                _sp.Dispose();
            }

            if (_newDeviceUI != null)
                _newDeviceUI.Close();
            logger.Warn("Closing application.....");
            logger = null;
        }
    }
}
