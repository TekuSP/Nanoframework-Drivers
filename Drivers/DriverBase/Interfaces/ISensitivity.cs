namespace DriverBase.Interfaces
{
    /// <summary>
    /// Defines support of setting sensitivity to light or color
    /// </summary>
    public interface ISensitivity
    {
        /// <summary>
        /// Sets integration time
        /// </summary>
        /// <param name="integrationTime">Integration time</param>
        public void SetIntegrationTime(byte integrationTime);
        /// <summary>
        /// Set gain
        /// </summary>
        /// <param name="gain">Gain</param>
        public void SetGain(byte gain);
    }
}
