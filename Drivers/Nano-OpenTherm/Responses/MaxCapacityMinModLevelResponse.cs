using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// Boiler maximum capacity (high byte) and minimum relative modulation level (low byte, %).
    /// </summary>
    public class MaxCapacityMinModLevelResponse : Response
    {
        public MaxCapacityMinModLevelResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public MaxCapacityMinModLevelResponse(Response r) : base(r) { }

        /// <summary>Manufacturer-specific capacity unit, usually kW scaled to a byte.</summary>
        public byte MaxCapacity { get; set; }
        /// <summary>Minimum relative modulation level (0..100%).</summary>
        public byte MinRelModulationPercent { get; set; }

        protected override uint GetRawDataCore()
            => ProcessResponse(Utilities.MakeUShort(MaxCapacity, MinRelModulationPercent));

        protected override void SetRawDataCore(uint value)
        {
            MaxCapacity = Utilities.GetHighByte(value);
            MinRelModulationPercent = Utilities.GetLowByte(value);
        }

        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.MaxCapacityMinModLevel;
    }
}
