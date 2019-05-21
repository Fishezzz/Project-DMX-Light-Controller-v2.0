using DMX.Entities;
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
    /// Interaction logic for TabLedScanner.xaml
    /// </summary>
    public partial class TabLedScanner : TabItem
    {
        private LedScanner ledScanner;

        public TabLedScanner(LedScanner ledScannerDevice)
        {
            InitializeComponent();
            this.DataContext = ledScannerDevice;
            ledScanner = ledScannerDevice;
            DmxDevice = ledScannerDevice;
        }

        private DmxDevice dmxDevice;
        public DmxDevice DmxDevice
        {
            get { return dmxDevice; }
            set { dmxDevice = value; }
        }

        // CH1 + CH2

        // CH3 + CH4

        // CH5

        // CH6

        // CH7

        // CH8

        // CH9

        // CH10

        // CH11

    }
}
