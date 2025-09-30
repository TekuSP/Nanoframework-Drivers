using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class ExhaustOutletTemperatureResponse : FloatTemperatureResponseBase
    {
        #region Public Constructors

        public ExhaustOutletTemperatureResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public ExhaustOutletTemperatureResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.Teo;

        #endregion Public Properties
    }
}