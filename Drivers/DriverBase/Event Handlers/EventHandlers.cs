using TekuSP.Drivers.DriverBase.Interfaces;

namespace TekuSP.Drivers.DriverBase.Event_Handlers
{
    /// <summary>
    /// Common event handler delegates for driver data callbacks.
    /// </summary>
    public static class EventHandlers
    {
        /// <summary>
        /// Delegate for touch data callbacks.
        /// </summary>
        /// <param name="sender">Event source.</param>
        /// <param name="data">Touch data payload.</param>
        public delegate void ITouchDataHandler(object sender, ITouchData data);

        /// <summary>
        /// Delegate for color data callbacks.
        /// </summary>
        /// <param name="sender">Event source.</param>
        /// <param name="data">Color data payload.</param>
        public delegate void IColorDataEventHandler(object sender, IColorData data);
    }
}
