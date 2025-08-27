using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads a character from the manufacturer/brand version string by index.
    /// </summary>
    public class GetManufacturerVersionRequest : ReadRequest
    {
        #region Public Constructors

        public GetManufacturerVersionRequest() : base()
        {
        }

        public GetManufacturerVersionRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Index of the character to read (0-based, low byte of request payload).
        /// </summary>
        public byte Index { get; set; }

        public override MessageID MessageID => MessageID.BrandVersion;

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