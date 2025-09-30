using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class SupplyInletTemperatureResponse : FloatTemperatureResponseBase
    {
        #region Public Constructors

        public SupplyInletTemperatureResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public SupplyInletTemperatureResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.Tsi;

        #endregion Public Properties
    }
}