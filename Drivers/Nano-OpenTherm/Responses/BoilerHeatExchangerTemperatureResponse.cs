using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class BoilerHeatExchangerTemperatureResponse : FloatTemperatureResponseBase
    {
        public BoilerHeatExchangerTemperatureResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public BoilerHeatExchangerTemperatureResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.TboilerHeatExchanger;
    }
}
