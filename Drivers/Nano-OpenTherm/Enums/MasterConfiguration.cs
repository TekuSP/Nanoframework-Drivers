using System;

namespace TekuSP.Drivers.Nano_OpenTherm.Enums
{
    [Flags]
    public enum MasterConfiguration : byte
    {
        /// <summary>
        /// Reserved bit 0.
        /// </summary>
        Reserved0 = 1 << 0,
        /// <summary>
        /// Reserved bit 1.
        /// </summary>
        Reserved1 = 1 << 1,
        /// <summary>
        /// Reserved bit 2.
        /// </summary>
        Reserved2 = 1 << 2,
        /// <summary>
        /// Reserved bit 3.
        /// </summary>
        Reserved3 = 1 << 3,
        /// <summary>
        /// Reserved bit 4.
        /// </summary>
        Reserved4 = 1 << 4,
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
