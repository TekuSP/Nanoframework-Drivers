using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class SupplyOutletTemperatureResponse : FloatTemperatureResponseBase
    {
        public SupplyOutletTemperatureResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public SupplyOutletTemperatureResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.Tso;
    }
}
