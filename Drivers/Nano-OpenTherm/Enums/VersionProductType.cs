namespace TekuSP.Drivers.Nano_OpenTherm.Enums
{
    /// <summary>
    /// Product type nibble used in Master/Slave product version/type (ID 125/126)
    /// The mapping below is not standardized across all OEMs; adjust as needed.
    /// </summary>
    public enum VersionProductType : byte
    {
        Unknown = 0,
        Boiler = 1,
        HeatPump = 2,
        Ventilation = 3,
        Controller = 4,
        Sensor = 5,
        // Extend with device-specific mapping if available
    }
}
