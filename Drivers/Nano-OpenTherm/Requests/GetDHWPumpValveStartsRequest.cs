using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the number of DHW pump starts or valve openings.
    /// </summary>
    public class GetDHWPumpValveStartsRequest : ReadRequest
    {
        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.DHWPumpValveStartsResponse);
        #region Public Constructors

        public GetDHWPumpValveStartsRequest() : base()
        {
        }

        public GetDHWPumpValveStartsRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Number of DHW pump starts or valve openings (low 16 bits, unsigned).
        /// Units: count.
        /// </summary>
        public ushort Count { get; set; }

        public override MessageID MessageID => MessageID.DHWPumpValveStarts;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(Count);

        protected override void SetRawDataCore(uint value)
        { Count = Utilities.GetLowUShort(value); }

        #endregion Protected Methods
    }
}