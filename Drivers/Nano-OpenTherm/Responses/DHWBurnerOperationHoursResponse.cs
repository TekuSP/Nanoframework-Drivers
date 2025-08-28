using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class DHWBurnerOperationHoursResponse : UShortValueResponseBase
    {
        public DHWBurnerOperationHoursResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
    public DHWBurnerOperationHoursResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.DHWBurnerOperationHours;
    }
}
