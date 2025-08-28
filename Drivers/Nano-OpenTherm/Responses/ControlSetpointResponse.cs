using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>CH water control setpoint (°C) for HC1.</summary>
    public class ControlSetpointResponse : FloatTemperatureResponseBase
    {
        public ControlSetpointResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public ControlSetpointResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.TSet;
    }
}
