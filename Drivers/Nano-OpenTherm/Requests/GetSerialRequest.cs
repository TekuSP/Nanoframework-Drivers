using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads a byte of the device brand/serial number by index.
    /// </summary>
    /// <remarks>
    /// The <see cref="Index"/> is encoded in the low byte of the request. The response returns
    /// the selected byte in the low 8 bits of the payload (low 16 bits used by some devices).
    /// Iterate index to reconstruct the full identifier.
    /// </remarks>
    public class GetSerialRequest : ReadRequest
    {
        #region Public Constructors

        public GetSerialRequest() : base()
        {
        }

        public GetSerialRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.BrandSerialByteResponse);

        /// <summary>
        /// Index of the serial/brand number byte to read from the device (0-based).
        /// </summary>
        public byte Index { get; set; }

        public override MessageID MessageID => MessageID.BrandSerialNumber;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(Index);

        protected override void SetRawDataCore(uint value)
        {
            Index = Utilities.GetLowByte(value);
        }

        #endregion Protected Methods
    }
}