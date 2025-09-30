using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class OutsideTemperatureResponse : FloatTemperatureResponseBase
    {
        #region Public Constructors

        public OutsideTemperatureResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public OutsideTemperatureResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.Toutside;

        #endregion Public Properties
    }
}