using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class ReturnTemperatureResponse : FloatTemperatureResponseBase
    {
        #region Public Constructors

        public ReturnTemperatureResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public ReturnTemperatureResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.Tret;

        #endregion Public Properties
    }
}