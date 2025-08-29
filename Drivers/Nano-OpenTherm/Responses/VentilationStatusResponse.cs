using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using TekuSP.Drivers.Nano_OpenTherm.Interfaces;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// Ventilation/Heat-recovery status flags response.
    /// Low byte = MasterStatus, High byte = SlaveStatus.
    /// </summary>
    public class VentilationStatusResponse : Response, IMasterStatus, ISlaveStatus
    {
        protected MasterStatus MasterStatus { get; set; }
        protected SlaveStatus SlaveStatus { get; set; }

        public VentilationStatusResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        /// <summary>
        /// Convenience constructor to initialize ventilation master/slave status fields.
        /// </summary>
        public VentilationStatusResponse(MasterStatus master, SlaveStatus slave, MessageType mt = MessageType.READ_ACK)
        {
            MessageType = mt;
            MasterStatus = master;
            SlaveStatus = slave;
        }
        public VentilationStatusResponse(Response r) : base(r) { }

        protected override uint GetRawDataCore()
        {
            byte low = Utilities.SetMasterStatus(MasterStatus);
            byte high = Utilities.SetSlaveStatus(SlaveStatus);
            return ProcessResponse(Utilities.MakeUShort(high, low));
        }

        protected override void SetRawDataCore(uint value)
        {
            MasterStatus = Utilities.GetMasterStatus(value);
            SlaveStatus = Utilities.GetSlaveStatus(value);
        }

        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.StatusVentilationHeatRecovery;

        // IMasterStatus
        public bool MasterIsCentralHeating2Active { get => MasterStatus.IsSet(MasterStatus.CH2Enabled); set => MasterStatus = MasterStatus.SetFlag(MasterStatus.CH2Enabled, value); }
        public bool MasterIsCentralHeatingActive { get => MasterStatus.IsSet(MasterStatus.CHEnabled); set => MasterStatus = MasterStatus.SetFlag(MasterStatus.CHEnabled, value); }
        public bool MasterIsCoolingActive { get => MasterStatus.IsSet(MasterStatus.CoolingEnabled); set => MasterStatus = MasterStatus.SetFlag(MasterStatus.CoolingEnabled, value); }
        public bool MasterIsHotWaterActive { get => MasterStatus.IsSet(MasterStatus.DHWEnabled); set => MasterStatus = MasterStatus.SetFlag(MasterStatus.DHWEnabled, value); }
        public bool MasterOTCActive { get => MasterStatus.IsSet(MasterStatus.OTCActive); set => MasterStatus = MasterStatus.SetFlag(MasterStatus.OTCActive, value); }
    public bool MasterSummerWinterMode { get => MasterStatus.IsSet(MasterStatus.SummerWinterMode); set => MasterStatus = MasterStatus.SetFlag(MasterStatus.SummerWinterMode, value); }
    public bool MasterDHWBlocking { get => MasterStatus.IsSet(MasterStatus.DHWBlocking); set => MasterStatus = MasterStatus.SetFlag(MasterStatus.DHWBlocking, value); }
        public bool MasterReserved7 { get => MasterStatus.IsSet(MasterStatus.Reserved7); set => MasterStatus = MasterStatus.SetFlag(MasterStatus.Reserved7, value); }

        // ISlaveStatus
        public bool SlaveCH2Mode { get => SlaveStatus.IsSet(SlaveStatus.CH2Mode); set => SlaveStatus = SlaveStatus.SetFlag(SlaveStatus.CH2Mode, value); }
        public bool SlaveCHMode { get => SlaveStatus.IsSet(SlaveStatus.CHMode); set => SlaveStatus = SlaveStatus.SetFlag(SlaveStatus.CHMode, value); }
        public bool SlaveCoolingStatus { get => SlaveStatus.IsSet(SlaveStatus.CoolingStatus); set => SlaveStatus = SlaveStatus.SetFlag(SlaveStatus.CoolingStatus, value); }
        public bool SlaveDHWMode { get => SlaveStatus.IsSet(SlaveStatus.DHWMode); set => SlaveStatus = SlaveStatus.SetFlag(SlaveStatus.DHWMode, value); }
        public bool SlaveDiagnosticIndication { get => SlaveStatus.IsSet(SlaveStatus.DiagnosticIndication); set => SlaveStatus = SlaveStatus.SetFlag(SlaveStatus.DiagnosticIndication, value); }
        public bool SlaveFaultIndication { get => SlaveStatus.IsSet(SlaveStatus.FaultIndication); set => SlaveStatus = SlaveStatus.SetFlag(SlaveStatus.FaultIndication, value); }
        public bool SlaveFlameStatus { get => SlaveStatus.IsSet(SlaveStatus.FlameStatus); set => SlaveStatus = SlaveStatus.SetFlag(SlaveStatus.FlameStatus, value); }
        public bool SlaveReserved7 { get => SlaveStatus.IsSet(SlaveStatus.Reserved); set => SlaveStatus = SlaveStatus.SetFlag(SlaveStatus.Reserved, value); }
    }
}
