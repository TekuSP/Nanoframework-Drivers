using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// Relative Humidity (u8/u8). For ID 78 the OTGW map treats both bytes as percentage values (0..100).
    /// Typically HB may carry supply/ambient and LB the exhaust; treat both as raw percentages.
    /// </summary>
    public class RelativeHumidityExhaustResponse : Response
    {
        #region Public Constructors

        public RelativeHumidityExhaustResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public RelativeHumidityExhaustResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>High byte relative humidity.</summary>
        public RelativeHumidity High { get; set; }

        /// <summary>Low byte relative humidity.</summary>
        public RelativeHumidity Low { get; set; }

        public override MessageID MessageID => MessageID.RHexhaust;

        public override MessageType MessageType { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
                            => ProcessResponse(Utilities.MakeUShort((byte)Utilities.Normalize((float)High.Percent), (byte)Utilities.Normalize((float)Low.Percent)));

        protected override void SetRawDataCore(uint value)
        {
            High = RelativeHumidity.FromPercent(Utilities.GetHighByte(value));
            Low = RelativeHumidity.FromPercent(Utilities.GetLowByte(value));
        }

        #endregion Protected Methods
    }
}