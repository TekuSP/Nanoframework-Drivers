using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Wraps a raw 32-bit OpenTherm frame that was received, exposing its type and ID.
    /// </summary>
    /// <remarks>
    /// This class parses <see cref="MessageType"/> (bits 28..30) and <see cref="MessageID"/> (bits 16..23)
    /// from the provided frame and preserves the full raw value in <see cref="GetRawDataCore"/>.
    /// Payload decoding is left to higher-level request/response types.
    /// </remarks>
    public class ReceivedRequest : Request
    {
        #region Private Fields

        private uint _raw;

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance from a raw 32-bit OpenTherm frame.
        /// </summary>
        /// <param name="rawData">Raw frame including header and payload.</param>
        public ReceivedRequest(uint rawData)
        {
            _raw = rawData;
            MessageType = Utilities.GetMessageType(rawData);
            MessageID = Utilities.GetMessageID(rawData);
        }

        #endregion Public Constructors

        #region Public Properties

        /// <inheritdoc />
        public override MessageID MessageID { get; }

        /// <inheritdoc />
        public override MessageType MessageType { get; }

    /// <inheritdoc />
    public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.ReceivedResponse);

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => _raw;

        protected override void SetRawDataCore(uint value)
        { _raw = value; }

        #endregion Protected Methods
    }
}