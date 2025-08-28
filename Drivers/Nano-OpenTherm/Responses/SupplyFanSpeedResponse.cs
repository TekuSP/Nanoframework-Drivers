using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Supply fan speed in RPM (ushort).</summary>
    public class SupplyFanSpeedResponse : UShortValueResponseBase
    {
        public SupplyFanSpeedResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public SupplyFanSpeedResponse(Response r) : base(r) { }
        public ushort RPM { get => Value; set => Value = value; }
        public override MessageID MessageID => MessageID.RPMsupply;
    }
}
