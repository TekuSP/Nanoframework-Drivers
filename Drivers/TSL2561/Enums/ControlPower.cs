namespace TekuSP.Drivers.TSL2561
{
    /// <summary>
    /// Control register power values.
    /// </summary>
    public enum ControlPower : byte
    {
        /// <summary>Power-off control value.</summary>
        PowerOff = 0x00,
        /// <summary>Power-on control value.</summary>
        PowerOn = 0x03
    }
}
