using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class RoomTemperatureCH2Response : FloatTemperatureResponseBase
    {
        #region Public Constructors

        public RoomTemperatureCH2Response(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public RoomTemperatureCH2Response(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.TrCH2;
        public override MessageType MessageType { get; set; }

        #endregion Public Properties
    }
}