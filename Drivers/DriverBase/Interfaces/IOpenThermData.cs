namespace TekuSP.Drivers.DriverBase.Interfaces
{
    /// <summary>
    /// OpenTherm data
    /// </summary>
    public interface IOpenThermData
    {
        /// <summary>
        /// Raw data
        /// </summary>
        ulong RawData
        {
            get; set;
        }
    }
}
