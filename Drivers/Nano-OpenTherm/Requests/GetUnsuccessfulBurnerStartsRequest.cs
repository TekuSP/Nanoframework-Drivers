using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the number of unsuccessful burner start attempts recorded by the slave.
    /// </summary>
    public class GetUnsuccessfulBurnerStartsRequest : ReadRequest
    {
        #region Public Constructors

        public GetUnsuccessfulBurnerStartsRequest() : base()
        {
        }

        public GetUnsuccessfulBurnerStartsRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Number of burner start attempts that failed (low 16 bits, unsigned).
        /// Units: count.
        /// </summary>
        public ushort Count { get; set; }

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.UnsuccessfulBurnerStartsResponse);
        public override MessageID MessageID => MessageID.UnsuccessfulBurnerStarts;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(Count);

        protected override void SetRawDataCore(uint value)
        { Count = Utilities.GetLowUShort(value); }

        #endregion Protected Methods
    }
}