using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class SupplyInletTemperatureResponse : FloatTemperatureResponseBase
    {
        public SupplyInletTemperatureResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public SupplyInletTemperatureResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.Tsi;
    }
}
