namespace TekuSP.Drivers.DriverBase.Interfaces
{
    /// <summary>
    /// XY touch data.
    /// </summary>
    public interface ITouchData
    {
        /// <summary>
        /// X of a press.
        /// </summary>
        int X { get; }
        /// <summary>
        /// Y of a press.
        /// </summary>
        int Y { get; }
        /// <summary>
        /// If supported, returns enum of gesture used for touch.
        /// </summary>
        byte Gesture { get; }
        /// <summary>
        /// If supported, returns how much pressed touch screen was.
        /// </summary>
        int TouchPressure { get; }
    }
}
