using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class ElectricityProducerHoursResponse : UShortValueResponseBase
    {
        public ElectricityProducerHoursResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
    public ElectricityProducerHoursResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.ElectricityProducerHours;
    }
}
