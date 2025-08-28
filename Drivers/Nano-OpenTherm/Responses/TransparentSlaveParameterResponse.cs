using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Generic Transparent Slave Parameter value (encoding depends on parameter, exposed as ushort raw).</summary>
    public class TransparentSlaveParameterResponse : UShortValueResponseBase
    {
        public TransparentSlaveParameterResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public TransparentSlaveParameterResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.TSPindexTSPvalue;
    }
}
