namespace TekuSP.Drivers.CST816D.Enums
{
    /// <summary>
    /// CST816D register addresses.
    /// </summary>
    public enum RegisterAddress : byte
    {
        /// <summary>Start register address for burst reads.</summary>
        Start = 0x00,
        /// <summary>Scan register base address.</summary>
        Scan = 0x00,
        /// <summary>Gesture ID register address.</summary>
        GestureId = 0x01,
        /// <summary>XY data register base address.</summary>
        XY = 0x03,
        /// <summary>X high byte register address.</summary>
        XH = 0x03,
        /// <summary>X low byte register address.</summary>
        XL = 0x04,
        /// <summary>Y high byte register address.</summary>
        YH = 0x05,
        /// <summary>Y low byte register address.</summary>
        YL = 0x06,
        /// <summary>Version register address.</summary>
        Version = 0x15,
        /// <summary>Version info register address.</summary>
        VersionInfo = 0xA7,
        /// <summary>Sleep command register.</summary>
        Sleep = 0xA5
    }
}
