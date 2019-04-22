using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMX.Entities.Enumerations
{
    /// <summary>
    /// Skytec_Mini_Spot_Led_Movinghead,
    /// Ayra_ERO_Laser_Movinghead,
    /// Ayra_ALO_060_DMX_LED_Scanner,
    /// EuroLite_LED_Pixel_Panel_16_DMX,
    /// Showtec_LED_PAR_56,
    /// </summary>
    public enum DmxDeviceTypes
    {
        Unknown = -1,
        None,
        Skytec_LedMovinghead,
        Ayra_LedLaserMovinghead,
        Ayra_LedScanner,
        EuroLite_LedPanel,
        Showtec_LedSpot,
    }
}
