using System;

namespace TekuSP.Drivers.Nano_OpenTherm.Enums
{
    [Flags]
    public enum MasterStatus : byte
    {
        /// <summary>
        /// CH is enabled.
        /// </summary>
        CHEnabled = 1 << 0,
        /// <summary>
        /// DHW is enabled.
        /// </summary>
        DHWEnabled = 1 << 1,
        /// <summary>
        /// Cooling is enabled.
        /// </summary>
        CoolingEnabled = 1 << 2,
        /// <summary>
        /// OTC is active.
        /// </summary>
        OTCActive = 1 << 3,
        /// <summary>
        /// CH2 is enabled.
        /// </summary>
        CH2Enabled = 1 << 4,
    /// <summary>
    /// Summer/Winter mode (bit 5 in v2.3b).
    /// </summary>
    SummerWinterMode = 1 << 5,
    /// <summary>
    /// DHW blocking (bit 6 in v2.3b).
    /// </summary>
    DHWBlocking = 1 << 6,
        /// <summary>
        /// Reserved bit 7.
        /// </summary>
        Reserved7 = 1 << 7,
    }
}
