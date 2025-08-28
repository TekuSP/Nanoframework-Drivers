using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class ExhaustTemperatureResponse : FloatTemperatureResponseBase
    {
        public ExhaustTemperatureResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public ExhaustTemperatureResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.Texhaust;
    }
}
