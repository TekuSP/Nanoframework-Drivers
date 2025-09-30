using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class SuccessfulBurnerStartsResponse : UShortValueResponseBase
    {
        #region Public Constructors

        public SuccessfulBurnerStartsResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public SuccessfulBurnerStartsResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.SuccessfulBurnerStarts;

        #endregion Public Properties
    }
}