using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class ElectricityProductionResponse : UShortValueResponseBase
    {
        public ElectricityProductionResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
    public ElectricityProductionResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.ElectricityProduction;
    }
}
