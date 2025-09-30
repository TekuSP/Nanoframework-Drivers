using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class CH2FlowTemperatureResponse : FloatTemperatureResponseBase
    {
        #region Public Constructors

        public CH2FlowTemperatureResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public CH2FlowTemperatureResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.TflowCH2;

        #endregion Public Properties
    }
}