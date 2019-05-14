using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DMX_2._0.Event_Args
{
    public class SettingsEventArgs : EventArgs
    {
        public string ComPort { get; set; }

        public SettingsEventArgs(string comPort)
        {
            ComPort = comPort;
        }
    }
}
