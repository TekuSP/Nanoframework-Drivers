using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class DHWPumpValveStartsResponse : UShortValueResponseBase
    {
        public DHWPumpValveStartsResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
    public DHWPumpValveStartsResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.DHWPumpValveStarts;
    }
}
