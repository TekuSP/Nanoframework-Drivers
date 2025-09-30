using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class VentilationTSPValueResponse : UShortValueResponseBase
    {
        #region Public Constructors

        public VentilationTSPValueResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public VentilationTSPValueResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.TSPindexTSPvalueVentilationHeatRecovery;

        #endregion Public Properties
    }
}