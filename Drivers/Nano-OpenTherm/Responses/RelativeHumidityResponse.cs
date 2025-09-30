using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class RelativeHumidityResponse : Response
    {
        #region Public Constructors

        public RelativeHumidityResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public RelativeHumidityResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override MessageID MessageID => MessageID.RelativeHumidity;

        public override MessageType MessageType { get; set; }

        /// <summary>Ambient relative humidity.</summary>
        public RelativeHumidity RelativeHumidity { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessResponse(Utilities.GetRawPercentage((float)RelativeHumidity.Percent));

        protected override void SetRawDataCore(uint value) => RelativeHumidity = RelativeHumidity.FromPercent(Utilities.GetPercentage(value));

        #endregion Protected Methods
    }
}