namespace TekuSP.Drivers.CST816D
{
    /// <summary>
    /// CST816D register addresses and fixed constants.
    /// </summary>
    public static class Registers
    {
        /// <summary>Default I2C address.</summary>
        public static byte I2CAddress => 0x15;
        /// <summary>Start register address for burst reads.</summary>
        public static byte StartRegisterAddress => 0x00;
        /// <summary>Number of bytes in a full register snapshot.</summary>
        public static byte AllRegistersByte => 20;

        /// <summary>Number of bytes in scan register read.</summary>
        public static byte ScanRegisterByte => 9;
        /// <summary>Scan register base address.</summary>
        public static byte ScanRegisterAddress => 0x00;
        /// <summary>Version register address.</summary>
        public static byte VersionAddress => 0x15;
        /// <summary>Version info register address.</summary>
        public static byte VersionInfoAddress => 0xA7;

        /// <summary>Gesture ID register address.</summary>
        public static byte GestureIDRegister => 0x01;
        /// <summary>XY data register base address.</summary>
        public static byte XYRegister => 0x03;

        /// <summary>X high byte register address.</summary>
        public static byte XHRegister => 0x03;
        /// <summary>X low byte register address.</summary>
        public static byte XLRegister => 0x04;
        /// <summary>Y high byte register address.</summary>
        public static byte YHRegister => 0x05;
        /// <summary>Y low byte register address.</summary>
        public static byte YLRegister => 0x06;

        /// <summary>Sleep command register.</summary>
        public static byte SleepRegister => 0xA5;
        /// <summary>Standby command value.</summary>
        public static byte StandbyCommand => 0x03;

        /// <summary>Mask for MSB extraction.</summary>
        public static byte MSBMask => 0x0F;
        /// <summary>Shift for MSB extraction.</summary>
        public static byte MSBShift => 0;
        /// <summary>Mask for LSB extraction.</summary>
        public static byte LSBMask => 0xFF;
        /// <summary>Shift for LSB extraction.</summary>
        public static byte LSBShift => 0;

        /// <summary>Maximum X coordinate.</summary>
        public static byte MaxX => 239;
        /// <summary>Minimum X coordinate.</summary>
        public static byte MinX => 0;
        /// <summary>Maximum Y coordinate.</summary>
        public static byte MaxY => 239;
        /// <summary>Minimum Y coordinate.</summary>
        public static byte MinY => 0;
    }
}
