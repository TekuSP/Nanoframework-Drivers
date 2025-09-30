using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// Generic response builder for OpenTherm message types and IDs.
    /// Use this to construct responses (READ_ACK/WRITE_ACK/DATA_INVALID/UNKNOWN_DATA_ID)
    /// with an optional 16-bit payload.
    /// </summary>
    public class CustomResponse : Response
    {
        #region Private Fields

        private uint data;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Creates a new response with the given type, id and payload.
        /// </summary>
        /// <param name="messageType">Typically READ_ACK or WRITE_ACK (or DATA_INVALID / UNKNOWN_DATA_ID)</param>
        /// <param name="messageID">OpenTherm message ID</param>
        /// <param name="data">16-bit payload placed in the low word</param>
        public CustomResponse(MessageType messageType, MessageID messageID, uint data = 0)
        {
            MessageType = messageType;
            MessageID = messageID;
            this.data = Utilities.MakeUShort(Utilities.GetHighByte(data), Utilities.GetLowByte(data));
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID { get; }

        public override MessageType MessageType { get; set; }

        #endregion Public Properties

        #region Protected Methods

        /// <summary>
        /// Encode header and parity from the stored payload.
        /// </summary>
        protected override uint GetRawDataCore() => ProcessResponse(data);

        /// <summary>
        /// Update stored payload (low 16 bits) from a raw frame.
        /// </summary>
        protected override void SetRawDataCore(uint value) => data = Utilities.MakeUShort(Utilities.GetHighByte(value), Utilities.GetLowByte(value));

        #endregion Protected Methods
    }
}