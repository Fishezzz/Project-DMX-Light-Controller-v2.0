using Logging;
using Project_DMX_2._0.Event_Args;
using System;
using System.Collections.Generic;
using System.IO.Ports;
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
    /// Interaction logic for SettingsUI.xaml
    /// </summary>
    public partial class SettingsUI : Window
    {
        Logger logger;

        public SettingsUI(string currentComPort)
        {
            InitializeComponent();
            logger = Logger.GetLogger;
            logger.Log("Openend SettingsUI window");

            cbxComPort.Items.Add("None");
            foreach (string s in SerialPort.GetPortNames())
                cbxComPort.Items.Add(s);
            logger.Log("Available COM ports added");

            cbxComPort.SelectedItem = currentComPort;
        }

        public event EventHandler<SettingsEventArgs> SettingsUpdated;
        protected void OnSettingsUpdated()
        {
            if (SettingsUpdated != null)
                SettingsUpdated(this, new SettingsEventArgs(cbxComPort.SelectedItem.ToString()));
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            OnSettingsUpdated();
            logger.Log("Closing SettingsUI window");
            this.Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            logger.Warn("Closing SettingsUI window on Cancel");
            this.Close();
        }
    }
}
