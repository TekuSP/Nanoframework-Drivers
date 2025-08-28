using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class ElectricityProducerStartsResponse : UShortValueResponseBase
    {
        public ElectricityProducerStartsResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
    public ElectricityProducerStartsResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.ElectricityProducerStarts;
    }
}
