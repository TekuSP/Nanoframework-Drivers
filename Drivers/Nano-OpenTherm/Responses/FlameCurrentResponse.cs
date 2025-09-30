using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class FlameCurrentResponse : Response
    {
        #region Public Constructors

        public FlameCurrentResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public FlameCurrentResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>Flame ionization current. OpenTherm encodes this as 8.8 fixed point (legacy float). Interpreted here as microamps (µA).</summary>
        public ElectricCurrent FlameCurrent { get; set; }

        public override MessageID MessageID => MessageID.FlameCurrent;

        public override MessageType MessageType { get; set; }

        #endregion Public Properties

        #region Protected Methods

        // F8.8 microampere representation via legacy alias.
        protected override uint GetRawDataCore() => ProcessResponse(Utilities.GetRawF88((float)FlameCurrent.Microamperes, 0, 100));

        protected override void SetRawDataCore(uint value) => FlameCurrent = ElectricCurrent.FromMicroamperes(Utilities.GetFloat(value));

        #endregion Protected Methods
    }
}