using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class DHWBurnerStartsResponse : UShortValueResponseBase
    {
        #region Public Constructors

        public DHWBurnerStartsResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public DHWBurnerStartsResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.DHWBurnerStarts;

        #endregion Public Properties
    }
}