using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class DHWPumpValveStartsResponse : UShortValueResponseBase
    {
        #region Public Constructors

        public DHWPumpValveStartsResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public DHWPumpValveStartsResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.DHWPumpValveStarts;

        #endregion Public Properties
    }
}