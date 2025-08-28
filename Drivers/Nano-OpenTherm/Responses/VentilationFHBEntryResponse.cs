using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class VentilationFHBEntryResponse : UShortValueResponseBase
    {
        public VentilationFHBEntryResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public VentilationFHBEntryResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.FHBindexFHBvalueVentilationHeatRecovery;
    }
}
