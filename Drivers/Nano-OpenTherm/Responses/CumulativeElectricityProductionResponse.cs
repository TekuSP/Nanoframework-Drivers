using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class CumulativeElectricityProductionResponse : UShortValueResponseBase
    {
        public CumulativeElectricityProductionResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
    public CumulativeElectricityProductionResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.CumulativElectricityProduction;
    }
}
