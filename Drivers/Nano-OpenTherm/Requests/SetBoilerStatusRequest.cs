using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class SetBoilerStatusRequest : Request
    {
        public override ulong RawData
        {
            get
            {
                uint data = (uint)((EnableCentralHeating ? 1 : 0) | ((EnableHotWater ? 1 : 0) << 1) | ((EnableCooling ? 1 : 0) << 2) | ((EnableOutsideTemperatureCompensation ? 1 : 0) << 3) | ((EnableCentralHeating2 ? 1 : 0) << 4));
                data <<= 8;
                return ProcessRequest(data);
            }
            set
            {
            
            }
        }
        public override MessageType MessageType => MessageType.READ_DATA;
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
