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
        public GetVentilationOEMDiagnosticCodeRequest() : base() { }
        public GetVentilationOEMDiagnosticCodeRequest(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// OEM-specific diagnostic code (low 16 bits).
        /// </summary>
        public ushort Code { get; set; }

        protected override uint GetRawDataCore() => ProcessRequest(Code);
        protected override void SetRawDataCore(uint value) { Code = Utilities.GetLowUShort(value); }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.OEMDiagnosticCodeVentilationHeatRecovery;
    }
}
