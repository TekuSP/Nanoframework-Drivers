using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// Relative ventilation position (0-100%).
    /// Note: Use U8 encoding (0..100) in low byte per project decision for ID71.
    /// TODO: If device expects f8.8, introduce per-ID switch.
    /// </summary>
    public class VentilationPositionResponse : Response
    {
        #region Public Constructors

        public VentilationPositionResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public VentilationPositionResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.Vset;

        public override MessageType MessageType { get; set; }

        /// <summary>Ventilation position.</summary>
        public Ratio Position { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
            => ProcessResponse(Utilities.MakeUShort(0, (byte)Utilities.Normalize((float)Position.Percent)));

        protected override void SetRawDataCore(uint value) => Position = Ratio.FromPercent(Utilities.GetLowByte(value));

        #endregion Protected Methods
    }
}