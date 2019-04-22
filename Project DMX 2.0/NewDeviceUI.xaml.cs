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
        public NewDeviceUI()
        {
            InitializeComponent();
            logger = Logger.GetLogger;
            logger.Log("Openend NewDeviceUI window");
        }

        public event EventHandler<NewDmxDeviceEventArgs> NewDmxDevice;
        public void OnNewDmxDevice()
        {
            int startAddress;
            if (NewDmxDevice != null)
                NewDmxDevice(this,
                    new NewDmxDeviceEventArgs(
                        Enum.IsDefined(typeof(DmxDeviceTypes), cbxDeviceType.SelectedIndex) ? (DmxDeviceTypes)cbxDeviceType.SelectedIndex : DmxDeviceTypes.Unknown,
                        tbxName.Text,
                        int.TryParse(tbxStart.Text, out startAddress) ? ((startAddress >= 0 && startAddress <= 512) ? startAddress : 0) : 0
                    )
                );
        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OnNewDmxDevice();
            }
            catch (UnknownDeviceTypeException ex)
            {
                logger.Error(ex.Message + "\n" + ex.StackTrace);
                MessageBox.Show(ex.Message, "Warning!Unknow device type...");
                logger.Warn("Closing NewDeviceUI window on Error");
                this.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message + "\n" + ex.StackTrace);
                logger.Warn("Closing NewDeviceUI window on Error");
                this.Close();
            }

            logger.Log("Closing NewDeviceUI window");
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            logger.Warn("Closing NewDeviceUI window on Cancel");
            this.Close();
        }
    }
}
