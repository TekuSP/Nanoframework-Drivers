using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>CH water control setpoint for HC2 (°C).</summary>
    public class ControlSetpointCH2Response : FloatTemperatureResponseBase
    {
        public ControlSetpointCH2Response(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public ControlSetpointCH2Response(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.TsetCH2;
    }
}
