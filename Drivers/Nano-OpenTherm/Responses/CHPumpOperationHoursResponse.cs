using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class CHPumpOperationHoursResponse : UShortValueResponseBase
    {
        public CHPumpOperationHoursResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
    public CHPumpOperationHoursResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.CHPumpOperationHours;
    }
}
