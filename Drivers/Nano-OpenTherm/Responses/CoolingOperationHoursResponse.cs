using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class CoolingOperationHoursResponse : UShortValueResponseBase
    {
        public CoolingOperationHoursResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
    public CoolingOperationHoursResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.CoolingOperationHours;
    }
}
