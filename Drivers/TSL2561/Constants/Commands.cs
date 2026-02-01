namespace TekuSP.Drivers.TSL2561
{
    /// <summary>
    /// TSL2561 command bits and fixed constants.
    /// </summary>
    public static class Commands
    {
        /// <summary>Command bit to access registers.</summary>
        public const byte TSL2561_COMMAND_BIT = 0x80;
        /// <summary>Clear interrupt bit.</summary>
        public const byte TSL2561_CLEAR_BIT = 0x40;
        /// <summary>Word (16-bit) read bit.</summary>
        public const byte TSL2561_WORD_BIT = 0x20;
        /// <summary>Block read bit.</summary>
        public const byte TSL2561_BLOCK_BIT = 0x10;
        /// <summary>Power-on control value.</summary>
        public const byte TSL2561_CONTROL_POWERON = 0x03;
        /// <summary>Power-off control value.</summary>
        public const byte TSL2561_CONTROL_POWEROFF = 0x00;
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
