using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class ExhaustTemperatureResponse : FloatTemperatureResponseBase
    {
        #region Public Constructors

        public ExhaustTemperatureResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public ExhaustTemperatureResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.Texhaust;

        #endregion Public Properties
    }
}