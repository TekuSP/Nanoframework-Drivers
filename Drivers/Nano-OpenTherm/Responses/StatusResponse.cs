using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class StatusResponse : Response
    {
        private MasterStatus _masterStatus;
        private SlaveStatus _slaveStatus;

        /// <summary>
        /// Initializes a new response for constructing a frame to send.
        /// </summary>
        /// <param name="messageType">Optional message type to use. Defaults to <see cref="TekuSP.Drivers.Nano_OpenTherm.Enums.MessageType.READ_ACK"/>.</param>
        public StatusResponse(MessageType messageType = MessageType.READ_ACK)
        {
            MessageType = messageType;
        }

        public StatusResponse(Response baseResponse)
        {
            MessageType = baseResponse.MessageType;
            SetRawDataCore(baseResponse.RawData);
        }

        protected override uint GetRawDataCore()
        {
            // Pack flags into low 16 bits: [Master (bits 15..8)] [Slave (bits 7..0)]
            var low = Utilities.SetMasterStatus(_masterStatus);
            var high = Utilities.SetSlaveStatus(_slaveStatus);
            return ProcessResponse(Utilities.MakeUShort(high, low));
        }
        protected override void SetRawDataCore(uint value)
        {
            _masterStatus = Utilities.GetMasterStatus(value);
            _slaveStatus = Utilities.GetSlaveStatus(value);
        }

    public override MessageType MessageType { get; set; }
    public override MessageID MessageID => MessageID.Status;

    /// <summary>Master: Central Heating enable flag.</summary>
    public bool MasterIsCentralHeatingActive
        {
            get => Utilities.IsSet(_masterStatus, MasterStatus.CHEnabled);
            set => Utilities.SetFlag(ref _masterStatus, MasterStatus.CHEnabled, value);
        }
    /// <summary>Master: Domestic Hot Water enable flag.</summary>
    public bool MasterIsHotWaterActive
        {
            get => Utilities.IsSet(_masterStatus, MasterStatus.DHWEnabled);
            set => Utilities.SetFlag(ref _masterStatus, MasterStatus.DHWEnabled, value);
        }
    /// <summary>Master: Cooling enable flag.</summary>
    public bool MasterIsCoolingActive
        {
            get => Utilities.IsSet(_masterStatus, MasterStatus.CoolingEnabled);
            set => Utilities.SetFlag(ref _masterStatus, MasterStatus.CoolingEnabled, value);
        }
    /// <summary>Master: Outside Temperature Compensation active.</summary>
    public bool MasterOTCActive
        {
            get => Utilities.IsSet(_masterStatus, MasterStatus.OTCActive);
            set => Utilities.SetFlag(ref _masterStatus, MasterStatus.OTCActive, value);
        }
    /// <summary>Master: CH2 enable flag.</summary>
    public bool MasterIsCentralHeating2Active
        {
            get => Utilities.IsSet(_masterStatus, MasterStatus.CH2Enabled);
            set => Utilities.SetFlag(ref _masterStatus, MasterStatus.CH2Enabled, value);
        }

    /// <summary>Slave: Fault indication.</summary>
    public bool SlaveIsFault
        {
            get => Utilities.IsSet(_slaveStatus, SlaveStatus.FaultIndication);
            set => Utilities.SetFlag(ref _slaveStatus, SlaveStatus.FaultIndication, value);
        }
    /// <summary>Slave: Central Heating mode.</summary>
    public bool SlaveIsCentralHeatingActive
        {
            get => Utilities.IsSet(_slaveStatus, SlaveStatus.CHMode);
            set => Utilities.SetFlag(ref _slaveStatus, SlaveStatus.CHMode, value);
        }
    /// <summary>Slave: Domestic Hot Water mode.</summary>
    public bool SlaveIsHotWaterActive
        {
            get => Utilities.IsSet(_slaveStatus, SlaveStatus.DHWMode);
            set => Utilities.SetFlag(ref _slaveStatus, SlaveStatus.DHWMode, value);
        }
    /// <summary>Slave: Flame status.</summary>
    public bool SlaveIsFlameOn
        {
            get => Utilities.IsSet(_slaveStatus, SlaveStatus.FlameStatus);
            set => Utilities.SetFlag(ref _slaveStatus, SlaveStatus.FlameStatus, value);
        }
    /// <summary>Slave: Cooling status.</summary>
    public bool SlaveIsCoolingActive
        {
            get => Utilities.IsSet(_slaveStatus, SlaveStatus.CoolingStatus);
            set => Utilities.SetFlag(ref _slaveStatus, SlaveStatus.CoolingStatus, value);
        }
    /// <summary>Slave: CH2 mode.</summary>
    public bool SlaveIsCentralHeating2Active
        {
            get => Utilities.IsSet(_slaveStatus, SlaveStatus.CH2Mode);
            set => Utilities.SetFlag(ref _slaveStatus, SlaveStatus.CH2Mode, value);
        }
    /// <summary>Slave: Diagnostic indication.</summary>
    public bool SlaveDiagnosticIndicationActive
        {
            get => Utilities.IsSet(_slaveStatus, SlaveStatus.DiagnosticIndication);
            set => Utilities.SetFlag(ref _slaveStatus, SlaveStatus.DiagnosticIndication, value);
        }

    }
}
