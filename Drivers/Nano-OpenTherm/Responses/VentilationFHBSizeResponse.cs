using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class VentilationFHBSizeResponse : UShortValueResponseBase
    {
        #region Public Constructors

        public VentilationFHBSizeResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public VentilationFHBSizeResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.FHBsizeVentilationHeatRecovery;

        #endregion Public Properties
    }
}