namespace TekuSP.Drivers.TSL2561
{
    public static class Commands
    {
        public const byte TSL2561_COMMAND_BIT = 0x80;
        public const byte TSL2561_CLEAR_BIT = 0x40;
        public const byte TSL2561_WORD_BIT = 0x20;
        public const byte TSL2561_BLOCK_BIT = 0x10;
        public const byte TSL2561_CONTROL_POWERON = 0x03;
        public const byte TSL2561_CONTROL_POWEROFF = 0x00;
        public const byte TSL2561_LUX_LUXSCALE = 14;
        public const byte TSL2561_LUX_RATIOSCALE = 9;
        public const byte TSL2561_LUX_CHSCALE = 10;
        public const ushort TSL2561_LUX_CHSCALE_TINT0 = 0x7517;
        public const ushort TSL2561_LUX_CHSCALE_TINT1 = 0x0FE7;
    }
}
