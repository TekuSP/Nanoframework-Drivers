using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Remeha vendor-specific Data-ID 133. Semantics are OEM-specific; expose as ushort.</summary>
    public class Remeha133Response : UShortValueResponseBase
    {
        #region Public Constructors

        public Remeha133Response(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public Remeha133Response(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.Remeha133;

        #endregion Public Properties
    }
}