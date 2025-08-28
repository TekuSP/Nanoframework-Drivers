using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class CH2FlowTemperatureResponse : FloatTemperatureResponseBase
    {
        public CH2FlowTemperatureResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public CH2FlowTemperatureResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.TflowCH2;
    }
}
