using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Maximum relative modulation level setting (%). Although typically written by master, provide a typed response for symmetry and testability.</summary>
    public class MaxRelModLevelSettingResponse : Response
    {
        #region Public Constructors

        public MaxRelModLevelSettingResponse(MessageType mt = MessageType.WRITE_ACK)
        { MessageType = mt; }

        public MaxRelModLevelSettingResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>Maximum relative modulation level.</summary>
        public Ratio MaxRelativeModulation { get; set; }

        public override MessageID MessageID => MessageID.MaxRelModLevelSetting;

        public override MessageType MessageType { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessResponse(Utilities.GetRawPercentage((float)MaxRelativeModulation.Percent));

        protected override void SetRawDataCore(uint value) => MaxRelativeModulation = Ratio.FromPercent(Utilities.GetPercentage(value));

        #endregion Protected Methods
    }
}