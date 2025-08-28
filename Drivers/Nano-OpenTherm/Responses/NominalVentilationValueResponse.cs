using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Nominal ventilation value (device-specific units, ushort).</summary>
    public class NominalVentilationValueResponse : UShortValueResponseBase
    {
        public NominalVentilationValueResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public NominalVentilationValueResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.NominalVentilationValue;
    }
}
