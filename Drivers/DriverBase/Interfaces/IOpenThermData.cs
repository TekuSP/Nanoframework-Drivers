using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

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
        /// <summary>
        /// Message Type
        /// </summary>
        MessageType MessageType
        {
            get;
        }
        /// <summary>
        /// Message ID
        /// </summary>
        MessageID MessageID
        {
            get;
        }
    }
}
