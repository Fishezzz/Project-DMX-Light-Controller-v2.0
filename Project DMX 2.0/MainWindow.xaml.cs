using DMX;
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
            TabLedScanner tabLedScanner = new TabLedScanner();
            tctDeviceTabs.Items.Add(tabLedScanner);
            LaserMovinghead laserMovinghead = new LaserMovinghead("test", 120, DmxDeviceTypes.Ayra_LedLaserMovinghead);
            TabLaserMovinghead tabLaserMovinghead = new TabLaserMovinghead(laserMovinghead);
            tctDeviceTabs.Items.Add(tabLaserMovinghead);
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
            newDeviceUI.Show();
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
