using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class VentilationTSPCountResponse : UShortValueResponseBase
    {
        #region Public Constructors

        public VentilationTSPCountResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public VentilationTSPCountResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.TSPventilationHeatRecovery;

        #endregion Public Properties
    }
}