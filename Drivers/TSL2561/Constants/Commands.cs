namespace TekuSP.Drivers.TSL2561
{
    /// <summary>
    /// TSL2561 lux calculation constants.
    /// </summary>
    public static class Commands
    {
        /// <summary>Lux calculation scale factor.</summary>
        public const byte TSL2561_LUX_LUXSCALE = 14;
        /// <summary>Lux ratio scale factor.</summary>
        public const byte TSL2561_LUX_RATIOSCALE = 9;
        /// <summary>Channel scale factor.</summary>
        public const byte TSL2561_LUX_CHSCALE = 10;
        /// <summary>Channel scale for 13ms integration time.</summary>
        public const ushort TSL2561_LUX_CHSCALE_TINT0 = 0x7517;
        /// <summary>Channel scale for 101ms integration time.</summary>
        public const ushort TSL2561_LUX_CHSCALE_TINT1 = 0x0FE7;
    }
}
