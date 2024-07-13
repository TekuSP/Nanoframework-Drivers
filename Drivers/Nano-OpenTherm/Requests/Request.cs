using TekuSP.Drivers.Nano_OpenTherm.Enums;

using TekuSP.Drivers.DriverBase.Interfaces;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Abstract class for all requests
    /// </summary>
    public abstract class Request : IOpenThermData
    {
        /// <summary>
        /// Request raw result
        /// </summary>
        public abstract ulong RawData
        {
            get; set;
        }
        /// <summary>
        /// Message Type
        /// </summary>
        public abstract MessageType MessageType
        {
            get; 
        }
        /// <summary>
        /// Message ID
        /// </summary>
        public abstract MessageID MessageID
        {
            get; 
        }
        /// <summary>
        /// Processes request
        /// </summary>
        /// <param name="data">Input data</param>
        /// <returns>Returns Raw Request</returns>
        protected ulong ProcessRequest(ulong data)
        {
            if (MessageType == MessageType.WRITE_DATA)
            {
                data |= 1ul << 28;
            }
            data |= ((ulong)MessageID) << 16;
            if (Utilities.Parity(data))
                data |= (1ul << 31);
            return data;
        }
        /// <summary>
        /// Is Valid Request?
        /// </summary>
        /// <returns>Validity</returns>
        public bool IsValidRequest()
        {
            if (Utilities.Parity(RawData))
                return false;
            byte msgType = (byte)((RawData << 1) >> 29);
            return msgType == (byte)MessageType.READ_DATA || msgType == (byte)MessageType.WRITE_DATA;
        }
    }
}
