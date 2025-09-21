using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the number of starts of the electricity producer.
    /// </summary>
    public class GetElectricityProducerStartsRequest : ReadRequest
    {
        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.ElectricityProducerStartsResponse);
        #region Public Constructors

        public GetElectricityProducerStartsRequest() : base()
        {
        }

        public GetElectricityProducerStartsRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Number of starts of the electricity producer (encoded in the low 16 bits).
        /// </summary>
        public ushort Count { get; set; }

        public override MessageID MessageID => MessageID.ElectricityProducerStarts;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(Count);

        protected override void SetRawDataCore(uint value)
        { Count = Utilities.GetLowUShort(value); }

        #endregion Protected Methods
    }
}