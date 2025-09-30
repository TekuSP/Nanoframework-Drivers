using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class StorageTemperatureResponse : FloatTemperatureResponseBase
    {
        #region Public Constructors

        public StorageTemperatureResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public StorageTemperatureResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.Tstorage;

        #endregion Public Properties
    }
}