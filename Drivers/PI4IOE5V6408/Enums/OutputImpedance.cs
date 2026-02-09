namespace TekuSP.Drivers.PI4IOE5V6408.Enums
{
    /// <summary>
    /// Output impedance (high-impedance or driven low/high).
    /// </summary>
    public enum OutputImpedance : byte
    {
        /// <summary>
        /// Output actively driven (low impedance).
        /// </summary>
        Low = 0,

        /// <summary>
        /// Output high-impedance (tri-stated).
        /// </summary>
        High = 1
    }
}