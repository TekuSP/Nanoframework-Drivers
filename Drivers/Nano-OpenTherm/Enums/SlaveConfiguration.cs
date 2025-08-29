using System;

namespace TekuSP.Drivers.Nano_OpenTherm.Enums
{
    [Flags]
    public enum SlaveConfiguration : byte
    {
        /// <summary>
        /// DHW (Domestic Hot Water) present.
        /// </summary>
        DHWPresent = 1 << 0,
        /// <summary>
        /// Control type.
        /// </summary>
        ControlType = 1 << 1,
        /// <summary>
        /// Cooling configuration.
        /// </summary>
        CoolingConfig = 1 << 2,
        /// <summary>
        /// DHW configuration.
        /// </summary>
        DHWConfig = 1 << 3,
        /// <summary>
        /// Master low-off & pump control function.
        /// </summary>
        MasterLowOffPumpControl = 1 << 4,
        /// <summary>
        /// CH2 (Central Heating circuit 2) present.
        /// </summary>
        CH2Present = 1 << 5,
    /// <summary>
    /// Remote water filling function (per OT 2.3b, ID3: bit 6).
    /// </summary>
    RemoteWaterFillingFunction = 1 << 6,
    /// <summary>
    /// Heat/cool mode control (per OT 2.3b, ID3: bit 7).
    /// </summary>
    HeatCoolModeControl = 1 << 7,
    }
}
