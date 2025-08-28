using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class ExhaustInletTemperatureResponse : FloatTemperatureResponseBase
    {
        public ExhaustInletTemperatureResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public ExhaustInletTemperatureResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.Tei;
    }
}
