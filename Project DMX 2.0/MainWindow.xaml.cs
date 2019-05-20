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
            _dmxDevices.Add(e.DmxDevice);
            _availableDevices.Remove(e.DmxDevice);
            logger.Log("New DmxDevice added: " + e.DmxDevice.Name + " @ " + e.DmxDevice.StartAddress);
            TabItem tempTabItem;
            switch (e.DmxDevice.DeviceType)
            {
                case DmxDeviceTypes.Skytec_LedMovinghead:
                    tempTabItem = new TabLedMovinghead(new LedMovinghead(e.DmxDevice.Name, e.DmxDevice.StartAddress, e.DmxDevice.DeviceType));
                    tctDeviceTabs.Items.Add(tempTabItem);
                    tctDeviceTabs.SelectedItem = tempTabItem;
                    logger.Log("TabLedMovinghead added");
                    break;
                case DmxDeviceTypes.Ayra_LedLaserMovinghead:
                    tempTabItem = new TabLaserMovinghead(new LaserMovinghead(e.DmxDevice.Name, e.DmxDevice.StartAddress, e.DmxDevice.DeviceType));
                    tctDeviceTabs.Items.Add(tempTabItem);
                    logger.Log("TabLaserMovinghead added");
                    tctDeviceTabs.SelectedItem = tempTabItem;
                    break;
                case DmxDeviceTypes.Ayra_LedScanner:
                    tempTabItem = new TabLedScanner(new LedScanner(e.DmxDevice.Name, e.DmxDevice.StartAddress, e.DmxDevice.DeviceType));
                    tctDeviceTabs.Items.Add(tempTabItem);
                    logger.Log("TabLedScanner added");
                    tctDeviceTabs.SelectedItem = tempTabItem;
                    break;
                case DmxDeviceTypes.EuroLite_LedPanel:
                    tempTabItem = new TabLedPanel(new LedPanel(e.DmxDevice.Name, e.DmxDevice.StartAddress, e.DmxDevice.DeviceType));
                    tctDeviceTabs.Items.Add(tempTabItem);
                    logger.Log("TabLedPanel added");
                    tctDeviceTabs.SelectedItem = tempTabItem;
                    break;
                case DmxDeviceTypes.Showtec_LedSpot:
                    tempTabItem = new TabLedSpot(new LedSpot(e.DmxDevice.Name, e.DmxDevice.StartAddress, e.DmxDevice.DeviceType));
                    tctDeviceTabs.Items.Add(tempTabItem);
                    logger.Log("TabLedSpot added");
                    tctDeviceTabs.SelectedItem = tempTabItem;
                    break;
                case DmxDeviceTypes.Unknown:
                case DmxDeviceTypes.None:
                default:
                    logger.Warn("Cannot add device tab because DeviceType is None/Unknown");
                    break;
            }
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
            sbiComPort.Content = _sp.PortName;
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

        private void RemoveSelectedDevice_Click(object sender, RoutedEventArgs e)
        {
            if (tctDeviceTabs.SelectedIndex >= 0 && tctDeviceTabs.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to remove " + _dmxDevices[tctDeviceTabs.SelectedIndex].Name + "?\nThis can't be undone.", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Stop, MessageBoxResult.No);
                if (result == MessageBoxResult.Yes)
                {
                    _availableDevices.Add(_dmxDevices[tctDeviceTabs.SelectedIndex]);
                    _dmxDevices.RemoveAt(tctDeviceTabs.SelectedIndex);
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
                    sbiStartAddress.Content = _dmxDevices[tc.SelectedIndex].StartAddress;
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
