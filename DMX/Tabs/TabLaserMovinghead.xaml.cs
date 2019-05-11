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
        LaserMovinghead _laserMovinghead;

        public TabLaserMovinghead(LaserMovinghead laserMovinghead)
        {
            InitializeComponent();
            this.DataContext = _laserMovinghead = laserMovinghead;
        }

        private void SldrRotationX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _laserMovinghead.UpdateRotationX((byte)sldrChannel1.Value, (byte)sldrChannel2.Value);
        }

        private void SldrRotationY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _laserMovinghead.UpdateRotationY((byte)sldrChannel3.Value, (byte)sldrChannel4.Value);
        }

        private void SldrAxisSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _laserMovinghead.UpdateAxisSpeed((byte)sldrChannel5.Value);
        }

        private void SldrShutterStatus_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _laserMovinghead.UpdateShutterStatus((byte)sldrChannel6.Value);
        }
    }
}
