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
    /// Interaction logic for TabLedMovinghead.xaml
    /// </summary>
    public partial class TabLedMovinghead : TabItem
    {
        LedMovinghead _ledMovinghead;

        public TabLedMovinghead(LedMovinghead ledMovinghead)
        {
            InitializeComponent();
            this.DataContext = _ledMovinghead = ledMovinghead;
        }

        private void SldrRotationX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _ledMovinghead.UpdateRotationX((byte)sldrChannel1.Value, (byte)sldrChannel2.Value);
        }

        private void SldrRotationY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _ledMovinghead.UpdateRotationY((byte)sldrChannel3.Value, (byte)sldrChannel4.Value);
        }

        private void SldrAxisSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _ledMovinghead.UpdateAxisSpeed((byte)sldrChannel5.Value);
        }

        private void SldrShutterStatus_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _ledMovinghead.UpdateShutterStatus((byte)sldrChannel6.Value);
        }

        private void BudRGB_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (budChannel7 != null && budChannel8 != null && budChannel9 != null && _ledMovinghead != null)
                _ledMovinghead.UpdateRGB(budChannel7.Value, budChannel8.Value, budChannel9.Value);
        }
        // Kleuren CH10 => byte = SelectedIndex * 14;
        // Auto programma's CH12 => byte = SelectedIndex * 15;
        // Gobo's CH13 => byte = SelectedIndex * 14; if(SelectedIndex == 11 || SelectedIndex == Items.Last()) { byte = 200; }

        private void rb_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
