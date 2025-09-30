using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class SupplyOutletTemperatureResponse : FloatTemperatureResponseBase
    {
        #region Public Constructors

        public SupplyOutletTemperatureResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public SupplyOutletTemperatureResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.Tso;

        #endregion Public Properties
    }
}