using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the OEM-specific diagnostic code from a ventilation/heat recovery unit.
    /// </summary>
    /// <remarks>
    /// The diagnostic code is returned in the low 16 bits of the payload. The meaning of the
    /// value is OEM-specific and should be interpreted using the manufacturer documentation.
    /// </remarks>
    public class GetVentilationOEMDiagnosticCodeRequest : ReadRequest
    {
        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.VentilationOEMDiagnosticCodeResponse);
        #region Public Constructors

        public GetVentilationOEMDiagnosticCodeRequest() : base()
        {
        }

        public GetVentilationOEMDiagnosticCodeRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// OEM-specific diagnostic code (low 16 bits).
        /// </summary>
        public ushort Code { get; set; }

        public override MessageID MessageID => MessageID.OEMDiagnosticCodeVentilationHeatRecovery;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(Code);

        protected override void SetRawDataCore(uint value)
        { Code = Utilities.GetLowUShort(value); }

        #endregion Protected Methods
    }
}