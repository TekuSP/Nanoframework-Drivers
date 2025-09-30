using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// Relative Modulation Level (%).
    /// </summary>
    public class RelModulationResponse : Response
    {
        #region Public Constructors

        public RelModulationResponse(MessageType messageType = MessageType.READ_ACK) => MessageType = messageType;

        public RelModulationResponse(Response baseResponse) : base(baseResponse)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.RelModLevel;

        public override MessageType MessageType { get; set; }

        /// <summary>Relative modulation level (0..100%).</summary>
        public Ratio RelativeModulation { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
            => ProcessResponse(Utilities.GetRawPercentage((float)RelativeModulation.Percent));

        protected override void SetRawDataCore(uint value)
            => RelativeModulation = Ratio.FromPercent(Utilities.GetPercentage(value));

        #endregion Protected Methods
    }
}