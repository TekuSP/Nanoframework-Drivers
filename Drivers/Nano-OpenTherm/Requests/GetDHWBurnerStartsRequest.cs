using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the number of burner starts during Domestic Hot Water (DHW) mode.
    /// </summary>
    public class GetDHWBurnerStartsRequest : ReadRequest
    {
        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.DHWBurnerStartsResponse);
        #region Public Constructors

        public GetDHWBurnerStartsRequest() : base()
        {
        }

        public GetDHWBurnerStartsRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Number of burner starts during domestic hot water (DHW) mode (encoded in the low 16 bits).
        /// Units: count.
        /// </summary>
        public ushort Count { get; set; }

        public override MessageID MessageID => MessageID.DHWBurnerStarts;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(Count);

        protected override void SetRawDataCore(uint value)
        { Count = Utilities.GetLowUShort(value); }

        #endregion Protected Methods
    }
}