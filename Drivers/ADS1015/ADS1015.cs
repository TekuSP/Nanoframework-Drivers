using System;

namespace ADS1015
{
    public class ADS1015
    {
        
        private enum PointerRegister
        {
            ADS_POINTER_CONVERT = 0x00,
            ADS_POINTER_CONFIG = 0x01,
            ADS_POINTER_LOWTHRESH = 0x02,
            ADS_POINTER_HIGHTHRESH = 0x03
        }
    }
}
