using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Cooling control signal (%).</summary>
    public class CoolingControlResponse : Response
    {
        #region Public Constructors

        public CoolingControlResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public CoolingControlResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public Ratio CoolingControl { get; set; }

        public override MessageID MessageID => MessageID.CoolingControl;

        public override MessageType MessageType { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessResponse(Utilities.GetRawPercentage((float)CoolingControl.Percent));

        protected override void SetRawDataCore(uint value) => CoolingControl = Ratio.FromPercent(Utilities.GetPercentage(value));

        #endregion Protected Methods
    }
}