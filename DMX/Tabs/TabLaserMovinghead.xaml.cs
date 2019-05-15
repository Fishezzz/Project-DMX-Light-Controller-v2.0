using DMX.Entities;
using System.Windows;
using System.Windows.Controls;

namespace DMX.Tabs
{
    /// <summary>
    /// Interaction logic for TabLaserMovinghead.xaml
    /// </summary>
    public partial class TabLaserMovinghead : TabItem
    {
        private LaserMovinghead laserMovinghead;

        public TabLaserMovinghead(LaserMovinghead laserMovingheadDevice)
        {
            InitializeComponent();
            this.DataContext = laserMovinghead = laserMovingheadDevice;
            DmxDevice = laserMovinghead;
        }

        private DmxDevice dmxDevice;
        public DmxDevice DmxDevice
        {
            get { return dmxDevice; }
            set { dmxDevice = value; }
        }
        
        private void SldrRotationX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            laserMovinghead.UpdateRotationX((byte)sldrChannel1.Value, (byte)sldrChannel2.Value);
        }

        private void SldrRotationY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            laserMovinghead.UpdateRotationY((byte)sldrChannel3.Value, (byte)sldrChannel4.Value);
        }

        private void SldrAxisSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            laserMovinghead.UpdateAxisSpeed((byte)sldrChannel5.Value);
        }

        private void SldrShutterStatus_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            laserMovinghead.UpdateShutterStatus((byte)sldrChannel6.Value);
        }
    }
}
