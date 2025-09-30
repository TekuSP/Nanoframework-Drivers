using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class CollectorTemperatureResponse : FloatTemperatureResponseBase
    {
        #region Public Constructors

        public CollectorTemperatureResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public CollectorTemperatureResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.Tcollector;

        #endregion Public Properties
    }
}