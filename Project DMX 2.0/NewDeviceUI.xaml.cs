using DMX;
using DMX.Entities.Enumerations;
using Logging;
using Project_DMX_2._0.Event_Args;
using Project_DMX_2._0.Exceptions;
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
using System.Windows.Shapes;

namespace Project_DMX_2._0
{
    /// <summary>
    /// Interaction logic for NewDmxDevice.xaml
    /// </summary>
    public partial class NewDeviceUI : Window
    {
        Logger logger;
        
        public List<DmxDevice> AvailableDevices { get; set; }

        public NewDeviceUI(List<DmxDevice> availableDevices)
        {
            InitializeComponent();
            logger = Logger.GetLogger;
            logger.Log("Openend NewDeviceUI window");
            AvailableDevices = availableDevices;
            cbxDeviceType.ItemsSource = availableDevices;
        }

        public event EventHandler<NewDmxDeviceEventArgs> NewDmxDevice;
        protected void OnNewDmxDevice()
        {
            int index = cbxDeviceType.SelectedIndex;
            if (index >= 0)
                NewDmxDevice?.Invoke(this, new NewDmxDeviceEventArgs(AvailableDevices[index]));
            else
            {
                MessageBox.Show("Unable to add device!\nNo device selected.", "Warning! Cannot add device...", MessageBoxButton.OK, MessageBoxImage.Warning);
                logger.Warn("Unable to add device. No device selected");
            }
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            OnNewDmxDevice();
            logger.Log("Closing NewDeviceUI window");
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            logger.Warn("Closing NewDeviceUI window on Cancel");
            this.Close();
        }

        private void CbxDeviceType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DmxDevice tempDevice = AvailableDevices[cbxDeviceType.SelectedIndex];
            Tuple<string, string, int, int> properties = Tuple.Create(tempDevice.Name, tempDevice.DeviceType.ToString(), tempDevice.StartAddress, tempDevice.Channels.Count());
            gbxProperties.DataContext = properties;
            imgDevice.DataContext = Tuple.Create("/DMX;component/Resources/Images/" + tempDevice.DeviceType.ToString() + ".png");
        }
    }
}
