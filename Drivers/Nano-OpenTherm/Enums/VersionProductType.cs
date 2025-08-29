namespace TekuSP.Drivers.Nano_OpenTherm.Enums
{
    /// <summary>
    /// Product type nibble used in Master/Slave product version/type (IDs 126 and 127 per v2.2).
    /// Note: IDs 124/125 carry the OpenTherm version numbers. The product type mapping below is
    /// OEM-defined and not standardised; adjust as needed for your devices.
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
