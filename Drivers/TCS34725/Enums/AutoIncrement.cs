namespace TekuSP.Drivers.TCS34725.Enums
{
    /// <summary>
    /// Auto-increment threshold register addresses.
    /// </summary>
    public enum AutoIncrement
    {
        /// <summary>Low threshold (low byte).</summary>
        TCS34725_AILTL = 0x04,
        /// <summary>Low threshold (high byte).</summary>
        TCS34725_AILTH = 0x05,
        /// <summary>High threshold (low byte).</summary>
        TCS34725_AIHTL = 0x06,
        /// <summary>High threshold (high byte).</summary>
        TCS34725_AIHTH = 0x07
    }
}
