using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class ExhaustInletTemperatureResponse : FloatTemperatureResponseBase
    {
        #region Public Constructors

        public ExhaustInletTemperatureResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public ExhaustInletTemperatureResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.Tei;

        #endregion Public Properties
    }
}