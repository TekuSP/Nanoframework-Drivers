using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using MS = TekuSP.Drivers.Nano_OpenTherm.Enums.MasterStatus;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Sets master status flags: CH/DHW/Cooling enable, OTC active, and CH2 enable.
    /// </summary>
    public class SetBoilerStatusRequest : WriteRequest
    {
        public SetBoilerStatusRequest() : base() { }
        public SetBoilerStatusRequest(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// Master status flags to write (encoded in the high byte of payload).
        /// </summary>
        public MS MasterStatus { get; set; }

        protected override uint GetRawDataCore()
        {
            // Place the master flags in the high byte
            ushort payload = Utilities.MakeUShort(Utilities.SetMasterStatus(MasterStatus), 0);
            return ProcessRequest(payload);
        }
        protected override void SetRawDataCore(uint value)
        {
            // Decode flags from the high byte into MasterStatus
            MasterStatus = (MS)Utilities.GetHighByte(value);
        }

        public override MessageType MessageType => MessageType.WRITE_DATA;
        public override MessageID MessageID => MessageID.Status;

        /// <summary>
        /// Enables Central Heating demand (sets master CH enable flag, bit 8).
        /// </summary>
        public bool EnableCentralHeating { get => MasterStatus.IsSet(MS.CHEnabled); set => MasterStatus = MasterStatus.SetFlag(MS.CHEnabled, value); }
        /// <summary>
        /// Enables Domestic Hot Water demand (sets master DHW enable flag, bit 9).
        /// </summary>
        public bool EnableHotWater { get => MasterStatus.IsSet(MS.DHWEnabled); set => MasterStatus = MasterStatus.SetFlag(MS.DHWEnabled, value); }
        /// <summary>
        /// Enables Cooling demand (sets master Cooling enable flag, bit 10).
        /// </summary>
        public bool EnableCooling { get => MasterStatus.IsSet(MS.CoolingEnabled); set => MasterStatus = MasterStatus.SetFlag(MS.CoolingEnabled, value); }
        /// <summary>
        /// Enables Outside Temperature Compensation/OTC active (bit 11).
        /// </summary>
        public bool EnableOutsideTemperatureCompensation { get => MasterStatus.IsSet(MS.OTCActive); set => MasterStatus = MasterStatus.SetFlag(MS.OTCActive, value); }
        /// <summary>
        /// Enables Central Heating circuit 2 demand (bit 12).
        /// </summary>
        public bool EnableCentralHeating2 { get => MasterStatus.IsSet(MS.CH2Enabled); set => MasterStatus = MasterStatus.SetFlag(MS.CH2Enabled, value); }
    }
}
