using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Cooling control signal (%)
    /// </summary>
    public class SetCoolingControlRequest : WriteRequest
    {
        #region Private Fields

        private Ratio _percent;

        #endregion Private Fields

        #region Public Constructors

        public SetCoolingControlRequest() : base()
        {
        }

        public SetCoolingControlRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.CoolingControlResponse);

        public override MessageID MessageID => MessageID.CoolingControl;

        public override MessageType MessageType => MessageType.WRITE_DATA;

        /// <summary>
        /// Cooling control signal in % (encoded as 8.8 fixed-point in low 16 bits). Value is clamped 0..100.
        /// </summary>
        public Ratio Percent
        {
            get => _percent;
            set => _percent = Ratio.FromPercent(((float)value.Percent).Normalize());
        }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
        {
            return ProcessRequest(Utilities.GetRawPercentage((float)Percent.Percent));
        }

        protected override void SetRawDataCore(uint value)
        {
            Percent = Ratio.FromPercent(Utilities.GetPercentage(value));
        }

        #endregion Protected Methods
    }
}