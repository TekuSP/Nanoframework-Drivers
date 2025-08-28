using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class SuccessfulBurnerStartsResponse : UShortValueResponseBase
    {
        public SuccessfulBurnerStartsResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
    public SuccessfulBurnerStartsResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.SuccessfulBurnerStarts;
    }
}
