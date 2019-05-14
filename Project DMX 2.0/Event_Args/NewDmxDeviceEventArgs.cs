using DMX;
using DMX.Entities;
using DMX.Entities.Enumerations;
using Logging;
using Project_DMX_2._0.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_DMX_2._0.Event_Args
{
    public class NewDmxDeviceEventArgs : EventArgs
    {
        public DmxDevice DmxDevice { get; set; }
        Logger logger;

        public NewDmxDeviceEventArgs(DmxDeviceTypes deviceType, string name, int startAddress)
        {
            logger = Logger.GetLogger;
            switch (deviceType)
            {
                case DmxDeviceTypes.Skytec_LedMovinghead:
                    DmxDevice = new LedMovinghead(name, startAddress, deviceType);
                    logger.Log("New Skytec_LedMovinghead created");
                    break;
                case DmxDeviceTypes.Ayra_LedLaserMovinghead:
                    DmxDevice = new LaserMovinghead(name, startAddress, deviceType);
                    logger.Log("New Ayra_LedLaserMovinghead created");
                    break;
                case DmxDeviceTypes.Ayra_LedScanner:
                    DmxDevice = new LedScanner(name, startAddress, deviceType);
                    logger.Log("New Ayra_LedScanner created");
                    break;
                case DmxDeviceTypes.EuroLite_LedPanel:
                    DmxDevice = new LedPanel(name, startAddress, deviceType);
                    logger.Log("New EuroLite_LedPanel created");
                    break;
                case DmxDeviceTypes.Showtec_LedSpot:
                    DmxDevice = new LedSpot(name, startAddress, deviceType);
                    logger.Log("New Showtec_LedSpot created");
                    break;
                case DmxDeviceTypes.Unknown:
                default:
                    logger.Warn("Unable to create new device. See Error message");
                    throw new UnknownDeviceTypeException("Device type is unknown. Unable to create a new device.");
            }
        }
    }
}
