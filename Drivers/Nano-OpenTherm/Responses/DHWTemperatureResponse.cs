using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class DHWTemperatureResponse : FloatTemperatureResponseBase
    {
        #region Public Constructors

        public DHWTemperatureResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public DHWTemperatureResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.Tdhw;

        #endregion Public Properties
    }
}