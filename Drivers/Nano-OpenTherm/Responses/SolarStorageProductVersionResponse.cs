using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Solar Storage product version/type response.</summary>
    public class SolarStorageProductVersionResponse : ProductVersionTypeResponseBase
    {
        #region Public Constructors

        public SolarStorageProductVersionResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public SolarStorageProductVersionResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.SolarStorageVersion;

        #endregion Public Properties
    }
}