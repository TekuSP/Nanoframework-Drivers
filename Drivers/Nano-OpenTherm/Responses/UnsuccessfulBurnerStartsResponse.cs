using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class UnsuccessfulBurnerStartsResponse : UShortValueResponseBase
    {
        public UnsuccessfulBurnerStartsResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
    public UnsuccessfulBurnerStartsResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.UnsuccessfulBurnerStarts;
    }
}
