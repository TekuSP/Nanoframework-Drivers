using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Maximum CH water setpoint (°C). Typically a write parameter, surfaced for completeness.</summary>
    public class MaxTSetResponse : FloatTemperatureResponseBase
    {
        #region Public Constructors

        public MaxTSetResponse(MessageType mt = MessageType.WRITE_ACK)
        { MessageType = mt; }

        public MaxTSetResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.MaxTSet;

        #endregion Public Properties
    }
}