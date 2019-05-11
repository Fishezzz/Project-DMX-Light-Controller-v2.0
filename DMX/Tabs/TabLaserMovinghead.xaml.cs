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
