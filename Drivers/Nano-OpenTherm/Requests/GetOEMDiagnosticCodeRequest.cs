using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the OEM-specific diagnostic/service code.
    /// </summary>
    public class GetOEMDiagnosticCodeRequest : ReadRequest
    {
        #region Public Constructors

        public GetOEMDiagnosticCodeRequest() : base()
        {
        }

        public GetOEMDiagnosticCodeRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// OEM-specific diagnostic/service code (low 16 bits).
        /// </summary>
        public ushort Code { get; set; }

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.OEMDiagnosticCodeResponse);
        public override MessageID MessageID => MessageID.OEMDiagnosticCode;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(Code);

        protected override void SetRawDataCore(uint value)
        { Code = Utilities.GetLowUShort(value); }

        #endregion Protected Methods
    }
}