using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class CoolingOperationHoursResponse : UShortValueResponseBase
    {
        #region Public Constructors

        public CoolingOperationHoursResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public CoolingOperationHoursResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.CoolingOperationHours;

        #endregion Public Properties
    }
}