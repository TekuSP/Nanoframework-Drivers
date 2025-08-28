using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class CollectorTemperatureResponse : FloatTemperatureResponseBase
    {
        public CollectorTemperatureResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public CollectorTemperatureResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.Tcollector;
    }
}
