using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Domestic hot water setpoint (°C). Typically a write parameter, but expose it for typed decoding.</summary>
    public class DhwSetpointResponse : FloatTemperatureResponseBase
    {
        #region Public Constructors

        public DhwSetpointResponse(MessageType mt = MessageType.WRITE_ACK)
        { MessageType = mt; }

        public DhwSetpointResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.TdhwSet;

        #endregion Public Properties
    }
}