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
    /// Interaction logic for TabLedPanel.xaml
    /// </summary>
    public partial class TabLedPanel : TabItem
    {
        private LedPanel ledPanel;

        public TabLedPanel(LedPanel ledPanelDevice)
        {
            InitializeComponent();
            this.DataContext = ledPanelDevice;
            ledPanel = ledPanelDevice;
            DmxDevice = ledPanelDevice;
        }

        private DmxDevice dmxDevice;
        public DmxDevice DmxDevice
        {
            get { return dmxDevice; }
            set { dmxDevice = value; }
        }
    }
}
