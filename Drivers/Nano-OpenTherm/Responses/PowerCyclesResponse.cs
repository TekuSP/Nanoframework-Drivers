using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class PowerCyclesResponse : UShortValueResponseBase
    {
        public PowerCyclesResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
    public PowerCyclesResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.PowerCycles;
    }
}
