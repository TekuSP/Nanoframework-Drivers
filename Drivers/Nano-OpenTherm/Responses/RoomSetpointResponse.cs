using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Room setpoint (°C) for HC1.</summary>
    public class RoomSetpointResponse : FloatTemperatureResponseBase
    {
        #region Public Constructors

        public RoomSetpointResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public RoomSetpointResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.TrSet;

        #endregion Public Properties
    }
}