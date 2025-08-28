using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Fault History Buffer entry value (device-specific encoding, ushort raw).</summary>
    public class FaultHistoryBufferEntryResponse : UShortValueResponseBase
    {
        public FaultHistoryBufferEntryResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public FaultHistoryBufferEntryResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.FHBindexFHBvalue;
    }
}
