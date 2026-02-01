namespace TekuSP.Drivers.CST816D.Constants
{
    /// <summary>
    /// CST816D fixed constants and limits.
    /// </summary>
    public static class Cst816DConstants
    {
        /// <summary>Default I2C address.</summary>
        public const byte I2CAddress = 0x15;
        /// <summary>Number of bytes in a full register snapshot.</summary>
        public const byte AllRegistersByte = 20;
        /// <summary>Number of bytes in scan register read.</summary>
        public const byte ScanRegisterByte = 9;
        /// <summary>Standby command value.</summary>
        public const byte StandbyCommand = 0x03;

        /// <summary>Mask for MSB extraction.</summary>
        public const byte MSBMask = 0x0F;
        /// <summary>Shift for MSB extraction.</summary>
        public const byte MSBShift = 0;
        /// <summary>Mask for LSB extraction.</summary>
        public const byte LSBMask = 0xFF;
        /// <summary>Shift for LSB extraction.</summary>
        public const byte LSBShift = 0;

        /// <summary>Maximum X coordinate.</summary>
        public const byte MaxX = 239;
        /// <summary>Minimum X coordinate.</summary>
        public const byte MinX = 0;
        /// <summary>Maximum Y coordinate.</summary>
        public const byte MaxY = 239;
        /// <summary>Minimum Y coordinate.</summary>
        public const byte MinY = 0;
    }
}
