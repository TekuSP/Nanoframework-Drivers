using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using TekuSP.Drivers.Nano_OpenTherm.Interfaces;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class StatusResponse : Response, IMasterStatus, ISlaveStatus
    {
    protected MasterStatus MasterStatus { get; set; }
    protected SlaveStatus SlaveStatus { get; set; }

        /// <summary>
        /// Initializes a new response for constructing a frame to send.
        /// </summary>
        /// <param name="messageType">Optional message type to use. Defaults to <see cref="TekuSP.Drivers.Nano_OpenTherm.Enums.MessageType.READ_ACK"/>.</param>
        public StatusResponse(MessageType messageType = MessageType.READ_ACK)
        {
            MessageType = messageType;
        }

        /// <summary>
        /// Convenience constructor to initialize master and slave status bitfields.
        /// </summary>
        public StatusResponse(MasterStatus master, SlaveStatus slave, MessageType mt = MessageType.READ_ACK)
        {
            MessageType = mt;
            MasterStatus = master;
            SlaveStatus = slave;
        }

    public StatusResponse(Response baseResponse) : base(baseResponse) { }

        protected override uint GetRawDataCore()
        {
            // Pack flags into low 16 bits: [Master (bits 15..8)] [Slave (bits 7..0)]
            var low = Utilities.SetMasterStatus(MasterStatus);
            var high = Utilities.SetSlaveStatus(SlaveStatus);
            return ProcessResponse(Utilities.MakeUShort(high, low));
        }
        protected override void SetRawDataCore(uint value)
        {
            MasterStatus = Utilities.GetMasterStatus(value);
            SlaveStatus = Utilities.GetSlaveStatus(value);
        }

        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.Status;

    // IMasterStatus
    /// <summary>Master: CH2 enable flag.</summary>
    public bool MasterIsCentralHeating2Active { get => MasterStatus.IsSet(MasterStatus.CH2Enabled); set => MasterStatus = MasterStatus.SetFlag(MasterStatus.CH2Enabled, value); }
    /// <summary>Master: Central Heating enable flag.</summary>
    public bool MasterIsCentralHeatingActive { get => MasterStatus.IsSet(MasterStatus.CHEnabled); set => MasterStatus = MasterStatus.SetFlag(MasterStatus.CHEnabled, value); }
    /// <summary>Master: Cooling enable flag.</summary>
    public bool MasterIsCoolingActive { get => MasterStatus.IsSet(MasterStatus.CoolingEnabled); set => MasterStatus = MasterStatus.SetFlag(MasterStatus.CoolingEnabled, value); }
    /// <summary>Master: Domestic Hot Water enable flag.</summary>
    public bool MasterIsHotWaterActive { get => MasterStatus.IsSet(MasterStatus.DHWEnabled); set => MasterStatus = MasterStatus.SetFlag(MasterStatus.DHWEnabled, value); }
    /// <summary>Master: Outside Temperature Compensation active.</summary>
    public bool MasterOTCActive { get => MasterStatus.IsSet(MasterStatus.OTCActive); set => MasterStatus = MasterStatus.SetFlag(MasterStatus.OTCActive, value); }
    public bool MasterSummerWinterMode { get => MasterStatus.IsSet(MasterStatus.SummerWinterMode); set => MasterStatus = MasterStatus.SetFlag(MasterStatus.SummerWinterMode, value); }
    public bool MasterDHWBlocking { get => MasterStatus.IsSet(MasterStatus.DHWBlocking); set => MasterStatus = MasterStatus.SetFlag(MasterStatus.DHWBlocking, value); }
    public bool MasterReserved7 { get => MasterStatus.IsSet(MasterStatus.Reserved7); set => MasterStatus = MasterStatus.SetFlag(MasterStatus.Reserved7, value); }

    // ISlaveStatus
    /// <summary>Slave: CH2 mode.</summary>
    public bool SlaveCH2Mode { get => SlaveStatus.IsSet(SlaveStatus.CH2Mode); set => SlaveStatus = SlaveStatus.SetFlag(SlaveStatus.CH2Mode, value); }
    /// <summary>Slave: Central Heating mode.</summary>
    public bool SlaveCHMode { get => SlaveStatus.IsSet(SlaveStatus.CHMode); set => SlaveStatus = SlaveStatus.SetFlag(SlaveStatus.CHMode, value); }
    /// <summary>Slave: Cooling status.</summary>
    public bool SlaveCoolingStatus { get => SlaveStatus.IsSet(SlaveStatus.CoolingStatus); set => SlaveStatus = SlaveStatus.SetFlag(SlaveStatus.CoolingStatus, value); }
    /// <summary>Slave: Domestic Hot Water mode.</summary>
    public bool SlaveDHWMode { get => SlaveStatus.IsSet(SlaveStatus.DHWMode); set => SlaveStatus = SlaveStatus.SetFlag(SlaveStatus.DHWMode, value); }
    /// <summary>Slave: Diagnostic indication.</summary>
    public bool SlaveDiagnosticIndication { get => SlaveStatus.IsSet(SlaveStatus.DiagnosticIndication); set => SlaveStatus = SlaveStatus.SetFlag(SlaveStatus.DiagnosticIndication, value); }
    /// <summary>Slave: Fault indication.</summary>
    public bool SlaveFaultIndication { get => SlaveStatus.IsSet(SlaveStatus.FaultIndication); set => SlaveStatus = SlaveStatus.SetFlag(SlaveStatus.FaultIndication, value); }
    /// <summary>Slave: Flame status.</summary>
    public bool SlaveFlameStatus { get => SlaveStatus.IsSet(SlaveStatus.FlameStatus); set => SlaveStatus = SlaveStatus.SetFlag(SlaveStatus.FlameStatus, value); }
    public bool SlaveReserved7 { get => SlaveStatus.IsSet(SlaveStatus.Reserved); set => SlaveStatus = SlaveStatus.SetFlag(SlaveStatus.Reserved, value); }

    }
}
