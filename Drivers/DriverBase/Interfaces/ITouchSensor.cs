using System;

using static TekuSP.Drivers.DriverBase.Event_Handlers.EventHandlers;

namespace TekuSP.Drivers.DriverBase.Interfaces
{
    /// <summary>
    /// Touch sensor
    /// </summary>
    public interface ITouchSensor
    {
        /// <summary>
        /// Poll Touch Sensor for current state
        /// </summary>
        /// <returns>Returns X, Y of a press</returns>
        public ITouchData Poll();
        /// <summary>
        /// Gets current state while polling is active.
        /// </summary>
        /// <returns>Returns X, Y of a press</returns>
        public ITouchData GetCurrentState();
        /// <summary>
        /// Triggers when touch change happens during polling.
        /// </summary>
        public event ITouchDataHandler OnStateChanged;
    }
}
