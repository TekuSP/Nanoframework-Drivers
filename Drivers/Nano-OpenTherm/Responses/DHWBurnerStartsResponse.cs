using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class DHWBurnerStartsResponse : UShortValueResponseBase
    {
        public DHWBurnerStartsResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
    public DHWBurnerStartsResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.DHWBurnerStarts;
    }
}
