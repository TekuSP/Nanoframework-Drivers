using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class SlaveProductVersionResponse : ProductVersionTypeResponseBase
    {
        #region Public Constructors

        public SlaveProductVersionResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public SlaveProductVersionResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.SlaveVersion;

        #endregion Public Properties
    }
}