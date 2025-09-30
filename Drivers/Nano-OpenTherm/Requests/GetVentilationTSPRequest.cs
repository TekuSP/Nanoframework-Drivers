using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads a specific transparent ventilation parameter value by index.
    /// Packs HB=index and LB=0 per v2.2 (TSP read: index in high byte).
    /// </summary>
    public class GetVentilationTSPRequest : ReadRequest
    {
        #region Public Constructors

        public GetVentilationTSPRequest() : base()
        {
        }

        public GetVentilationTSPRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.VentilationTSPValueResponse);

        /// <summary>
        /// Index number of the transparent ventilation parameter to read (encoded in high data byte).
        /// </summary>
        public byte Index { get; set; }

        public override MessageID MessageID => MessageID.TSPindexTSPvalueVentilationHeatRecovery;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(Utilities.MakeUShort(Index, 0));

        protected override void SetRawDataCore(uint value)
        { Index = Utilities.GetLowByte(value); }

        #endregion Protected Methods
    }
}