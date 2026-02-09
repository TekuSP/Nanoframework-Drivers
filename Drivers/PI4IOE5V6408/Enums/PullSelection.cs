namespace TekuSP.Drivers.PI4IOE5V6408.Enums
{
    /// <summary>
    /// Pull selection for input pins.
    /// </summary>
    public enum PullSelection : byte
    {
        /// <summary>
        /// No pull resistor enabled.
        /// </summary>
        Disabled = 0,

        /// <summary>
        /// Pull-down selected (logic low bias).
        /// </summary>
        PullDown = 0,

        /// <summary>
        /// Pull-up selected (logic high bias).
        /// </summary>
        PullUp = 1
    }
}