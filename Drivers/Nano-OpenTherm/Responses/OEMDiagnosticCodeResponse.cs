using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Generic OEM diagnostic/service code response (low 16 bits).</summary>
    public class OEMDiagnosticCodeResponse : UShortValueResponseBase
    {
        public OEMDiagnosticCodeResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public OEMDiagnosticCodeResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.OEMDiagnosticCode;
    }
}
