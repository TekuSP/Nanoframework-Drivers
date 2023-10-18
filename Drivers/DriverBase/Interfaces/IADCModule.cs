namespace TekuSP.Drivers.DriverBase.Interfaces
{
    /// <summary>
    /// ADC Compatible module
    /// </summary>
    public interface IADCModule
    {
        /// <summary>
        /// Read single value from ADC
        /// </summary>
        /// <param name="channelNumber">Channel to read from</param>
        /// <returns>Read data</returns>
        ushort SingleRead(int channelNumber);
        /// <summary>
        /// Read single value from ADC, between two channels as difference
        /// </summary>
        /// <param name="channelOne">Channel one to read from</param>
        /// <param name="channelTwo">Channel two to read from</param>
        /// <returns>Differential ADC data</returns>
        short DifferentialRead(int channelOne, int channelTwo);
    }
}
