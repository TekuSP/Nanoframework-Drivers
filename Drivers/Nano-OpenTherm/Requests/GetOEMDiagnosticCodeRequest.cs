using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the OEM-specific diagnostic/service code.
    /// </summary>
    public class GetOEMDiagnosticCodeRequest : ReadRequest
    {
        public GetOEMDiagnosticCodeRequest() : base() { }
        public GetOEMDiagnosticCodeRequest(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// OEM-specific diagnostic/service code (low 16 bits).
        /// </summary>
        public ushort Code { get; set; }

        protected override uint GetRawDataCore() => ProcessRequest(Code);
        protected override void SetRawDataCore(uint value) { Code = Utilities.GetLowUShort(value); }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.OEMDiagnosticCode;
    }
}
