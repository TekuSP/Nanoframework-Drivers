using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class DHW2TemperatureResponse : FloatTemperatureResponseBase
    {
        public DHW2TemperatureResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public DHW2TemperatureResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.Tdhw2;
    }
}
