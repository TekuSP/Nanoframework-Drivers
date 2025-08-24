using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class SetBoilerStatusRequest : WriteRequest
    {
        public SetBoilerStatusRequest() : base() { }
        public SetBoilerStatusRequest(Request baseReq) : base(baseReq) { }

        protected override ulong GetRawDataCore()
        {
            uint data = 0;
            // Bits 15..8 encode master status flags
            if (EnableCentralHeating) data |= 1u << 8;                 // CH enable
            if (EnableHotWater) data |= 1u << 9;                       // DHW enable
            if (EnableCooling) data |= 1u << 10;                       // Cooling enable
            if (EnableOutsideTemperatureCompensation) data |= 1u << 11; // OTC enable
            if (EnableCentralHeating2) data |= 1u << 12;               // CH2 enable
            return ProcessRequest(data);
        }
        protected override void SetRawDataCore(ulong value)
        {
            // Decode flags from the high byte of the 16-bit data field (bits 15..8)
            var b = Utilities.GetHighByte(value);
            EnableCentralHeating = (b & 0x01) != 0;
            EnableHotWater = (b & 0x02) != 0;
            EnableCooling = (b & 0x04) != 0;
            EnableOutsideTemperatureCompensation = (b & 0x08) != 0;
            EnableCentralHeating2 = (b & 0x10) != 0;
        }

        public override MessageType MessageType => MessageType.WRITE_DATA;
        public override MessageID MessageID => MessageID.Status;

        /// <summary>
        /// Enable Central Heating
        /// </summary>
        public bool EnableCentralHeating { get; set; }
        /// <summary>
        /// Enable Hot Water
        /// </summary>
        public bool EnableHotWater { get; set; }
        /// <summary>
        /// Enable Cooling
        /// </summary>
        public bool EnableCooling { get; set; }
        /// <summary>
        /// Enable Outside Temperature Compensation
        /// </summary>
        public bool EnableOutsideTemperatureCompensation { get; set; }
        /// <summary>
        /// Enable Central Heating 2
        /// </summary>
        public bool EnableCentralHeating2 { get; set; }
    }
}
