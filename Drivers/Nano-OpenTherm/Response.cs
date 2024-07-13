using TekuSP.Drivers.Nano_OpenTherm.Enums;

using TekuSP.Drivers.DriverBase.Interfaces;

namespace TekuSP.Drivers.Nano_OpenTherm
{
    /// <summary>
    /// Response data from OpenTherm device
    /// </summary>
    public class Response : IOpenThermData
    { 
        /// <summary>
        /// Initializes RawResponse
        /// </summary>
        public Response(ulong rawData = 0)
        {
            RawData = rawData;
        }
        /// <summary>
        /// Raw ulong data from OpenTherm device
        /// </summary>
        public ulong RawData
        {
            get;
            set;
        }
        /// <summary>
        /// Is fault recorded
        /// </summary>
        public bool IsFault => (RawData & 0x1) == 1;
        /// <summary>
        /// Is central heating active
        /// </summary>
        public bool IsCentralHeatingActive => (RawData & 0x2) == 1;
        /// <summary>
        /// Is hot water active
        /// </summary>
        public bool IsHotWaterActive => (RawData & 0x4) == 1;
        /// <summary>
        /// Is flame on
        /// </summary>
        public bool IsFlameOn => (RawData & 0x8) == 1;
        /// <summary>
        /// Is cooling active
        /// </summary>
        public bool IsCoolingActive => (RawData & 0x10) == 1;
        /// <summary>
        /// Is in diagnostic mode
        /// </summary>
        public bool IsDiagnostic => (RawData & 0x40) == 1;
        /// <summary>
        /// Get Uint from <see cref="RawData"/>
        /// </summary>
        /// <returns>Uint Response</returns>
        public uint GetUInt() => (uint)(RawData & 0xffff);
        /// <summary>
        /// Get Float from <see cref="RawData"/>
        /// </summary>
        /// <returns>Float Response</returns>
        public float GetFloat()
        {
            var temp = GetUInt();
            return ((temp & 0x8000) == 1) ? -(0x10000L - temp) / 256.0f : temp / 256.0f;
        }
        /// <summary>
        /// Is valid Response?
        /// </summary>
        /// <returns>Validity</returns>
        public bool IsValidResponse()
        {
            if (Utilities.Parity(RawData))
                return false;
            byte msgType = (byte)((RawData << 1) >> 29);
            return msgType == (byte)MessageType.READ_ACK || msgType == (byte)MessageType.WRITE_ACK;

        }
    }
}
