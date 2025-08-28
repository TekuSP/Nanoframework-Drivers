using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Fault History Buffer size (entries count as ushort).</summary>
    public class FaultHistoryBufferSizeResponse : UShortValueResponseBase
    {
        public FaultHistoryBufferSizeResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public FaultHistoryBufferSizeResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.FHBsize;
    }
}
