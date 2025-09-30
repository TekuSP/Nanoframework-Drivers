using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// Nominal ventilation value as percent (0..100). Project decision: U8 in low byte (ID87).
    /// TODO: If an implementation needs OEM-specific units, restore UShortValueResponseBase.
    /// </summary>
    public class NominalVentilationValueResponse : Response
    {
        #region Public Constructors

        public NominalVentilationValueResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public NominalVentilationValueResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.NominalVentilationValue;
        public override MessageType MessageType { get; set; }
        public Ratio NominalVentilation { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessResponse(Utilities.MakeUShort(0, (byte)Utilities.Normalize((float)NominalVentilation.Percent)));

        protected override void SetRawDataCore(uint value) => NominalVentilation = Ratio.FromPercent(Utilities.GetLowByte(value));

        #endregion Protected Methods
    }
}