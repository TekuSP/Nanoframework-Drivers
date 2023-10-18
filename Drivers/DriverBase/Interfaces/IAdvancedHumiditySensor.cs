namespace TekuSP.Drivers.DriverBase.Interfaces
{
    public interface IAdvancedHumiditySensor : IHumiditySensor
    {
        #region Public Methods

        /// <summary>
        /// Heat up sensor for period of time
        /// </summary>
        /// <param name="seconds">Seconds how long to heat up</param>
        void HeatUp(int seconds);

        /// <summary>
        /// Sets heater to on or off
        /// </summary>
        /// <param name="heaterTargetStatus">On or off?</param>
        void SetHeater(bool heaterTargetStatus);

        /// <summary>
        /// Sets sensor resolution
        /// </summary>
        /// <param name="resolution">Resolution to set for humidity sensor</param>
        void SetHumidityResolution(int resolution);

        #endregion Public Methods
    }
}