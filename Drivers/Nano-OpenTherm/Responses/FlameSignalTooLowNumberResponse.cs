using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class FlameSignalTooLowNumberResponse : UShortValueResponseBase
    {
        public FlameSignalTooLowNumberResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
    public FlameSignalTooLowNumberResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.FlameSignalTooLowNumber;
    }
}
