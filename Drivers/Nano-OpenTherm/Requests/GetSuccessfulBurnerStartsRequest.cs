using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the number of successful burner starts recorded by the slave.
    /// </summary>
    public class GetSuccessfulBurnerStartsRequest : ReadRequest
    {
        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.SuccessfulBurnerStartsResponse);
        #region Public Constructors

        public GetSuccessfulBurnerStartsRequest() : base()
        {
        }

        public GetSuccessfulBurnerStartsRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Number of successful burner starts (low 16 bits, unsigned).
        /// Units: count.
        /// </summary>
        public ushort Count { get; set; }

        public override MessageID MessageID => MessageID.SuccessfulBurnerStarts;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(Count);

        protected override void SetRawDataCore(uint value)
        { Count = Utilities.GetLowUShort(value); }

        #endregion Protected Methods
    }
}