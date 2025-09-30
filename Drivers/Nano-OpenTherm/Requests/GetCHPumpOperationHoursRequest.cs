using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the total operation hours of the central heating pump.
    /// </summary>
    public class GetCHPumpOperationHoursRequest : ReadRequest
    {
        #region Public Constructors

        public GetCHPumpOperationHoursRequest() : base()
        {
        }

        public GetCHPumpOperationHoursRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.CHPumpOperationHoursResponse);

        /// <summary>
        /// Total operation hours of the central heating pump (low 16 bits, unsigned).
        /// Units: hours.
        /// </summary>
        public ushort Hours { get; set; }

        public override MessageID MessageID => MessageID.CHPumpOperationHours;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(Hours);

        protected override void SetRawDataCore(uint value)
        { Hours = Utilities.GetLowUShort(value); }

        #endregion Protected Methods
    }
}