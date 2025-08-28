using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Ventilation/HR unit product version/type (uses same packing as master/slave).</summary>
    public class VentilationProductVersionResponse : ProductVersionTypeResponseBase
    {
        public VentilationProductVersionResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public VentilationProductVersionResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.VentilationHeatRecoveryVersion;
    }
}
