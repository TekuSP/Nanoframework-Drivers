using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Remeha vendor-specific Data-ID 132. Semantics are OEM-specific; expose as ushort.</summary>
    public class Remeha132Response : UShortValueResponseBase
    {
        public Remeha132Response(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public Remeha132Response(Response r) : base(r) { }
        public override MessageID MessageID => MessageID.Remeha132;
    }
}
