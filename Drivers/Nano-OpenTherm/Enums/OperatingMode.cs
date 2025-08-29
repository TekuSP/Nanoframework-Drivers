namespace TekuSP.Drivers.Nano_OpenTherm.Enums
{
    /// <summary>
    /// Operating mode values for HC/DHW (OpenTherm 2.3b).
    /// </summary>
    public enum OperatingMode : byte
    {
        Off = 0,
        Auto = 1,
        Manual = 2,
        Reserved = 3,
    }
}
