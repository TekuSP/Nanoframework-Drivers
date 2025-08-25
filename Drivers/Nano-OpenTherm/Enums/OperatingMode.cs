namespace TekuSP.Drivers.Nano_OpenTherm.Enums
{
    /// <summary>
    /// Remote override operating modes per OT 2.2 (ID 99)
    /// Low byte packs HC1/HC2/DHW in 3-3-2 bits:
    /// - b0..b2: OperatingMode HC1 (field is 3 bits; values 0..3 used)
    /// - b3..b5: OperatingMode HC2 (field is 3 bits; values 0..3 used)
    /// - b6..b7: OperatingMode DHW (field is 2 bits; values 0..3 used)
    /// </summary>
    public enum OperatingMode : byte
    {
        Off = 0,
        Auto = 1,
        Manual = 2,
        Reserved = 3,
    }
}
