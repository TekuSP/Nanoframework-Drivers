namespace TekuSP.Drivers.Nano_OpenTherm.Enums
{
    [System.Flags]
    public enum ApplicationSpecificFaultFlags : byte
    {
        /// <summary>
        /// Service request.
        /// </summary>
        ServiceRequest = 1 << 0,
        /// <summary>
        /// Lockout-reset.
        /// </summary>
        LockoutReset = 1 << 1,
        /// <summary>
        /// Low water pressure.
        /// </summary>
        LowWaterPress = 1 << 2,
        /// <summary>
        /// Gas/flame fault.
        /// </summary>
        GasFlameFault = 1 << 3,
        /// <summary>
        /// Air pressure fault.
        /// </summary>
        AirPressFault = 1 << 4,
        /// <summary>
        /// Water over-temperature.
        /// </summary>
        WaterOverTemp = 1 << 5,
        /// <summary>
        /// Reserved bit 6.
        /// </summary>
        Reserved6 = 1 << 6,
        /// <summary>
        /// Reserved bit 7.
        /// </summary>
        Reserved7 = 1 << 7,
    }
}
