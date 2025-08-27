using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the number of transparent ventilation parameters supported.
    /// </summary>
    public class GetVentilationTSPCountRequest : ReadRequest
    {
        #region Public Constructors

        public GetVentilationTSPCountRequest() : base()
        {
        }

        public GetVentilationTSPCountRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.TSPventilationHeatRecovery;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(0);

        protected override void SetRawDataCore(uint value)
        { }

        #endregion Protected Methods
    }
}