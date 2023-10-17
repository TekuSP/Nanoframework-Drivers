using System;

using static DriverBase.Event_Handlers.EventHandlers;

namespace DriverBase.Interfaces
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
        /// Gets current state when <see cref="StartPolling(int)"/> is running
        /// </summary>
        /// <returns>Returns X, Y of a press</returns>
        public ITouchData GetCurrentState();
        /// <summary>
        /// Triggers when touch change happens, when <see cref="StartPolling(int)"/> is running
        /// </summary>
        public event ITouchDataHandler OnStateChanged;
    }
    /// <summary>
    /// XY Touch Data
    /// </summary>
    public interface ITouchData
    {
        /// <summary>
        /// X of a press
        /// </summary>
        public int X { get; }
        /// <summary>
        /// Y of a press
        /// </summary>
        public int Y { get; }
        /// <summary>
        /// If supported, returns enum of gesture used for touch
        /// </summary>
        public byte Gesture { get; }
    }
}
