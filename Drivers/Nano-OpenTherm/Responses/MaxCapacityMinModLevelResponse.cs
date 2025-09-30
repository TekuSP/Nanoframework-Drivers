using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

using UnitsNet;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// Boiler maximum capacity (high byte) and minimum relative modulation level (low byte, %).
    /// </summary>
    public class MaxCapacityMinModLevelResponse : Response
    {
        #region Public Constructors

        public MaxCapacityMinModLevelResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public MaxCapacityMinModLevelResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>Manufacturer-specific capacity unit, usually kW scaled to a byte.</summary>
        public byte MaxCapacity { get; set; }

        public override MessageID MessageID => MessageID.MaxCapacityMinModLevel;

        public override MessageType MessageType { get; set; }

        /// <summary>Minimum relative modulation level (0..100%).</summary>
        public Ratio MinRelativeModulation { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
            => ProcessResponse(Utilities.MakeUShort(MaxCapacity, (byte)Utilities.Normalize((float)MinRelativeModulation.Percent)));

        protected override void SetRawDataCore(uint value)
        {
            MaxCapacity = Utilities.GetHighByte(value);
            MinRelativeModulation = Ratio.FromPercent(Utilities.GetLowByte(value));
        }

        #endregion Protected Methods
    }
}