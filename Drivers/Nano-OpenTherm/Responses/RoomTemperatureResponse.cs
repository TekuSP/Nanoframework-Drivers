using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class RoomTemperatureResponse : FloatTemperatureResponseBase
    {
        #region Public Constructors

        public RoomTemperatureResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public RoomTemperatureResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.Tr;
        public override MessageType MessageType { get; set; }

        #endregion Public Properties
    }
}