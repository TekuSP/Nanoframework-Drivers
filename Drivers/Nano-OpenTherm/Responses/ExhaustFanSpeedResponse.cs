using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Exhaust fan speed in RPM (ushort).</summary>
    public class ExhaustFanSpeedResponse : UShortValueResponseBase
    {
        public ExhaustFanSpeedResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public ExhaustFanSpeedResponse(Response r) : base(r) { }
        public ushort RPM { get => Value; set => Value = value; }
        public override MessageID MessageID => MessageID.RPMexhaust;
    }
}
