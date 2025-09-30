using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// Relative ventilation level percentage (0..100%).
    /// Note: Use U8 encoding (0..100) per project decision for ID77.
    /// </summary>
    public class RelativeVentilationLevelResponse : Response
    {
        #region Public Constructors

        public RelativeVentilationLevelResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public RelativeVentilationLevelResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.RelVentLevel;
        public override MessageType MessageType { get; set; }
        public Ratio RelativeVentilation { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
            => ProcessResponse(Utilities.MakeUShort(0, (byte)Utilities.Normalize((float)RelativeVentilation.Percent)));

        protected override void SetRawDataCore(uint value) => RelativeVentilation = Ratio.FromPercent(Utilities.GetLowByte(value));

        #endregion Protected Methods
    }
}