using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the total operation hours of the electricity producer.
    /// </summary>
    public class GetElectricityProducerHoursRequest : ReadRequest
    {
        #region Public Constructors

        public GetElectricityProducerHoursRequest() : base()
        {
        }

        public GetElectricityProducerHoursRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.ElectricityProducerHoursResponse);

        /// <summary>
        /// Total operation hours of the electricity producer (encoded in the low 16 bits).
        /// </summary>
        public ushort Hours { get; set; }

        public override MessageID MessageID => MessageID.ElectricityProducerHours;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(Hours);

        protected override void SetRawDataCore(uint value)
        { Hours = Utilities.GetLowUShort(value); }

        #endregion Protected Methods
    }
}