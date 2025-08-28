using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class BoilerTemperatureResponse : FloatTemperatureResponseBase
    {
        public BoilerTemperatureResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public BoilerTemperatureResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.Tboiler;
    }
}
