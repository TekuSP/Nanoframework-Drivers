using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class OutsideTemperatureResponse : FloatTemperatureResponseBase
    {
        public OutsideTemperatureResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public OutsideTemperatureResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.Toutside;
    }
}
