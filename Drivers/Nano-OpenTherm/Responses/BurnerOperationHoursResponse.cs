using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class BurnerOperationHoursResponse : UShortValueResponseBase
    {
        #region Public Constructors

        public BurnerOperationHoursResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public BurnerOperationHoursResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.BurnerOperationHours;

        #endregion Public Properties
    }
}