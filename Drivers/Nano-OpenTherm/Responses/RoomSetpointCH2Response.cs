using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Room setpoint (°C) for HC2.</summary>
    public class RoomSetpointCH2Response : FloatTemperatureResponseBase
    {
        public RoomSetpointCH2Response(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public RoomSetpointCH2Response(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.TrSetCH2;
    }
}
