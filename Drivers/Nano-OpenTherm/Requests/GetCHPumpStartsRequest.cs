using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the number of starts of the central heating pump.
    /// </summary>
    public class GetCHPumpStartsRequest : ReadRequest
    {
        #region Public Constructors

        public GetCHPumpStartsRequest() : base()
        {
        }

        public GetCHPumpStartsRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

    public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.CHPumpStartsResponse);

        /// <summary>
        /// Number of starts of the central heating pump (low 16 bits, unsigned).
        /// Units: count.
        /// </summary>
        public ushort Count { get; set; }

        public override MessageID MessageID => MessageID.CHPumpStarts;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(Count);

        protected override void SetRawDataCore(uint value)
        { Count = Utilities.GetLowUShort(value); }

        #endregion Protected Methods
    }
}