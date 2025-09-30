using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads a character from the manufacturer/brand name by index.
    /// </summary>
    public class GetManufacturerRequest : ReadRequest
    {
        #region Public Constructors

        public GetManufacturerRequest() : base()
        {
        }

        public GetManufacturerRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.BrandCharacterResponse);

        /// <summary>
        /// Index of the character to read from the brand text (0-based, low byte).
        /// </summary>
        public byte Index { get; set; }

        public override MessageID MessageID => MessageID.Brand;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(Index);

        protected override void SetRawDataCore(uint value)
        {
            // Populate Index from incoming raw payload (low byte)
            Index = Utilities.GetLowByte(value);
        }

        #endregion Protected Methods
    }
}