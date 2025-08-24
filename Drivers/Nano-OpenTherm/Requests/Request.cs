using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

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
            // Write full 3-bit message type (bits 30..28)
            data |= (((ulong)MessageType) & 0x7) << 28;
            // Write message id (bits 23..16)
            data |= ((ulong)MessageID) << 16;
            // Ensure overall frame has odd parity (bit count over 32 bits is odd)
            if (!Utilities.Parity(data))
                data |= (1ul << 31);
            return data;
        }
        /// <summary>
        /// Is Valid Request?
        /// </summary>
        /// <returns>Validity</returns>
        public bool IsValidRequest()
        {
            // Parity over full 32-bit frame must be odd
            if (!Utilities.Parity(RawData))
                return false;
            byte msgType = (byte)((RawData >> 28) & 0x7);
            // Only master request types are valid here
            return msgType == (byte)MessageType.READ_DATA || msgType == (byte)MessageType.WRITE_DATA;
        }
    }
}
