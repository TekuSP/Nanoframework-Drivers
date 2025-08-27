using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the cumulative electricity production total from the device.
    /// </summary>
    /// <remarks>
    /// Response payload: low 16 bits as an unsigned value representing kilowatt-hours (kWh).
    /// </remarks>
    public class GetCumulativeElectricityProductionRequest : ReadRequest
    {
        #region Public Constructors

        public GetCumulativeElectricityProductionRequest() : base()
        {
        }

        public GetCumulativeElectricityProductionRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Cumulative electricity production in kWh (low 16 bits).
        /// </summary>
        public ushort KWh { get; set; }

        public override MessageID MessageID => MessageID.CumulativElectricityProduction;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(KWh);

        protected override void SetRawDataCore(uint value)
        { KWh = Utilities.GetLowUShort(value); }

        #endregion Protected Methods
    }
}