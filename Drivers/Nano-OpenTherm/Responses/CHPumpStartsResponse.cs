using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class CHPumpStartsResponse : UShortValueResponseBase
    {
        public CHPumpStartsResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
    public CHPumpStartsResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.CHPumpStarts;
    }
}
