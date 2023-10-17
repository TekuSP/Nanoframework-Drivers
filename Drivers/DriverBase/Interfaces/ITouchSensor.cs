using System;

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
        /// Starts automatically polling touch sensor in set millis, use <see cref="GetCurrentState"/> to get current state
        /// </summary>
        /// <param name="millis">Millis how often to poll</param>
        public void StartPolling(int millis);
        /// <summary>
        /// Stops automatically polling
        /// </summary>
        public void StopPolling();
        /// <summary>
        /// Gets current state when <see cref="StartPolling(int)"/> is running
        /// </summary>
        /// <returns>Returns X, Y of a press</returns>
        public ITouchData GetCurrentState();
        /// <summary>
        /// Triggers when touch change happens, when <see cref="StartPolling(int)"/> is running
        /// </summary>
        public event EventHandler<ITouchData> OnStateChanged;
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
    }
}
