using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the instantaneous electricity production (Watts) from the device.
    /// </summary>
    public class GetElectricityProductionRequest : ReadRequest
    {
        #region Public Constructors

        public GetElectricityProductionRequest() : base()
        {
        }

        public GetElectricityProductionRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.ElectricityProduction;

        public override MessageType MessageType => MessageType.READ_DATA;

        /// <summary>
        /// Current electricity production in Watts (encoded in the low 16 bits).
        /// </summary>
        public ushort Watts { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(Watts);

        protected override void SetRawDataCore(uint value)
        { Watts = Utilities.GetLowUShort(value); }

        #endregion Protected Methods
    }
}