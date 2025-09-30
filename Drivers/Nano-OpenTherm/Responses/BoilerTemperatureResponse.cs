using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class BoilerTemperatureResponse : FloatTemperatureResponseBase
    {
        #region Public Constructors

        public BoilerTemperatureResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public BoilerTemperatureResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.Tboiler;

        #endregion Public Properties
    }
}