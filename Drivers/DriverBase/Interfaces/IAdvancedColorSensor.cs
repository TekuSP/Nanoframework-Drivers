using static DriverBase.Event_Handlers.EventHandlers;

namespace DriverBase.Interfaces
{
    /// <summary>
    /// Advanced color sensor with interrupt polling
    /// </summary>
    public interface IAdvancedColorSensor : IColorSensor
    {
        /// <summary>
        /// Enable or disable interrupt on pin
        /// </summary>
        /// <param name="enable">Enable interrupt or disable</param>
        /// <param name="interruptPin">Interrupt gpio pin</param>
        public void SetInterrupt(bool enable, int interruptPin);
        /// <summary>
        /// Sets limits when to trigger interrupt
        /// </summary>
        /// <param name="low">Low limit</param>
        /// <param name="high">High limit</param>
        public void SetInterruptLimits(int low, int high);
        /// <summary>
        /// Triggers when interrupt is enabled via <see cref="SetInterrupt(bool, int)"/> and limit is reached <see cref="SetInterruptLimits(int, int)"/>
        /// </summary>
        public event IColorDataEventHandler OnLimitReached;
        /// <summary>
        /// Triggers when interrupt is enabled via <see cref="SetInterrupt(bool, int)"/> and limit is reached <see cref="SetInterruptLimits(int, int)"/> Returns raw values
        /// </summary>
        public event IColorDataEventHandler OnLimitRawReached;
    }
}
