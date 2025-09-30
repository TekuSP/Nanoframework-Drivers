using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class ElectricityProducerHoursResponse : UShortValueResponseBase
    {
        #region Public Constructors

        public ElectricityProducerHoursResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public ElectricityProducerHoursResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.ElectricityProducerHours;

        #endregion Public Properties
    }
}