
using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.DriverBase.Interfaces;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// Response data from OpenTherm device
    /// </summary>
    public abstract class Response : IOpenThermData
    {
        /// <summary>
        /// Raw ulong data from OpenTherm device
        /// </summary>
        public abstract ulong RawData
        {
            get;
            set;
        }

        public abstract MessageType MessageType
        {
            get;
        }

        public abstract MessageID MessageID
        {
            get;
        }

        /// <summary>
        /// Processes response
        /// </summary>
        /// <param name="data">Input data</param>
        /// <returns>Returns Raw Request</returns>
        protected ulong ProcessResponse(ulong data)
        {
            data |= (ulong)MessageType << 28;
            data |= (ulong)MessageID << 16;
            if (Utilities.Parity(data))
                data |= 1ul << 31;
            return data;
        }
        /// <summary>
        /// Is valid Response?
        /// </summary>
        /// <returns>Validity</returns>
        public bool IsValidResponse()
        {
            if (Utilities.Parity(RawData))
                return false;
            var msgType = (byte)(RawData << 1 >> 29);
            return msgType == (byte)MessageType.READ_ACK || msgType == (byte)MessageType.WRITE_ACK;

        }
    }
}
