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
using DMX;
using DMX.Entities.Enumerations;

namespace Project_DMX_2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //DmxDevice dmxDevice = new DmxDevice(1, "test");
            //dmxDevice.Name = "a";
            //dmxDevice.StartAddress = 1;
            //dmxDevice.Channels = new byte[] { 1, 2 };
        }
    }
}
