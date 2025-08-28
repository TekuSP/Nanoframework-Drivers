using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Total number of Transparent Slave Parameters supported (ushort).</summary>
    public class TransparentSlaveParametersCountResponse : UShortValueResponseBase
    {
        public TransparentSlaveParametersCountResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public TransparentSlaveParametersCountResponse(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.TSP;
    }
}
