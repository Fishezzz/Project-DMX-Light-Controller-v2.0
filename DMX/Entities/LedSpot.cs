﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DMX.Entities.Enumerations;

namespace DMX.Entities
{
    public class LedSpot : DmxDevice
    {
        const int NUMBER_OF_CHANNELS = 6;

        public LedSpot(string name, int startAddress, DmxDeviceTypes deviceType)
            : base(name, startAddress, deviceType, NUMBER_OF_CHANNELS)
        { }

        //// CH1 + CH2 + CH3
        //public byte[] GetRGB()
        //{
        //    return new byte[3] { channels[0], channels[1], channels[2] };
        //}
    }
}
