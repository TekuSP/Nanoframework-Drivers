using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Ventilation/HR unit product version/type (uses same packing as master/slave).</summary>
    public class VentilationProductVersionResponse : ProductVersionTypeResponseBase
    {
        #region Public Constructors

        public VentilationProductVersionResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public VentilationProductVersionResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.VentilationHeatRecoveryVersion;

        #endregion Public Properties
    }
}