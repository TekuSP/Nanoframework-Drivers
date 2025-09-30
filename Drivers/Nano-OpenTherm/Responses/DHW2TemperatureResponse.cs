using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class DHW2TemperatureResponse : FloatTemperatureResponseBase
    {
        #region Public Constructors

        public DHW2TemperatureResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public DHW2TemperatureResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.Tdhw2;

        #endregion Public Properties
    }
}