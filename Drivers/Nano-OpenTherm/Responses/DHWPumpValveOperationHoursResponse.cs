using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class DHWPumpValveOperationHoursResponse : UShortValueResponseBase
    {
        #region Public Constructors

        public DHWPumpValveOperationHoursResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public DHWPumpValveOperationHoursResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.DHWPumpValveOperationHours;

        #endregion Public Properties
    }
}