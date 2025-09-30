using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class MasterProductVersionResponse : ProductVersionTypeResponseBase
    {
        #region Public Constructors

        public MasterProductVersionResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public MasterProductVersionResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.MasterVersion;

        #endregion Public Properties
    }
}