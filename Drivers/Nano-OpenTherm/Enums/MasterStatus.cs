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
        /// Reserved bit 5.
        /// </summary>
        Reserved5 = 1 << 5,
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
