using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Ventilation/HR OEM diagnostic code (low 16 bits).</summary>
    public class VentilationOEMDiagnosticCodeResponse : UShortValueResponseBase
    {
        public VentilationOEMDiagnosticCodeResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public VentilationOEMDiagnosticCodeResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.OEMDiagnosticCodeVentilationHeatRecovery;
    }
}
