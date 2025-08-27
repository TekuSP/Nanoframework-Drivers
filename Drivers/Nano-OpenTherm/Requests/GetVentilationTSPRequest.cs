using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads a specific transparent ventilation parameter value by index.
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

        /// <summary>
        /// Index number of the transparent ventilation parameter to read (low byte).
        /// </summary>
        public byte Index { get; set; }

        public override MessageID MessageID => MessageID.TSPindexTSPvalueVentilationHeatRecovery;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(Index);

        protected override void SetRawDataCore(uint value)
        { Index = Utilities.GetLowByte(value); }

        #endregion Protected Methods
    }
}