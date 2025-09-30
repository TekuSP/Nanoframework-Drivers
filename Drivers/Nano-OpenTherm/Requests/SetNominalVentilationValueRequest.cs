using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Sets the nominal relative ventilation value (0–100%).
    /// Wire format: U8 in low byte (per project decision for ID87).
    /// </summary>
    public class SetNominalVentilationValueRequest : WriteRequest
    {
        #region Private Fields

        private Ratio _percent;

        #endregion Private Fields

        #region Public Constructors

        public SetNominalVentilationValueRequest() : base()
        {
        }

        public SetNominalVentilationValueRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.NominalVentilationValueResponse);

        public override MessageID MessageID => MessageID.NominalVentilationValue;

        public override MessageType MessageType => MessageType.WRITE_DATA;

        /// <summary>
        /// Nominal relative ventilation value in % (U8 in low byte). Value is clamped to 0–100.
        /// </summary>
        /// <summary>
        /// Nominal ventilation value (0..100%) as Ratio. Encoded as U8 percent in low byte.
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