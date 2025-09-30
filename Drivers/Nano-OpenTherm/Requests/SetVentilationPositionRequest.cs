using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Sets the relative ventilation position (0–100%).
    /// Wire format: U8 in low byte (per project decision for ID71).
    /// </summary>
    public class SetVentilationPositionRequest : WriteRequest
    {
        #region Private Fields

        private Ratio _percent;

        #endregion Private Fields

        #region Public Constructors

        public SetVentilationPositionRequest() : base()
        {
        }

        public SetVentilationPositionRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.VentilationPositionResponse);
        public override MessageID MessageID => MessageID.Vset;

        public override MessageType MessageType => MessageType.WRITE_DATA;

        /// <summary>
        /// Relative ventilation position in % (U8 in low byte). Value is clamped to 0–100.
        /// </summary>
        /// <summary>
        /// Ventilation position (0..100%) exposed as Ratio. Encoded as U8 percent in low byte.
        /// </summary>
        public Ratio Percent
        {
            get => _percent;
            set => _percent = Ratio.FromPercent(Utilities.Normalize((float)value.Percent));
        }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
        {
            var pct = (byte)Utilities.Normalize((float)Percent.Percent);
            return ProcessRequest(Utilities.MakeUShort(0, pct));
        }

        protected override void SetRawDataCore(uint value)
        {
            Percent = Ratio.FromPercent(Utilities.GetLowByte(value));
        }

        #endregion Protected Methods
    }
}