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
            get => _masterStatus.IsSet(MasterStatus.CHEnabled);
            set => _masterStatus = _masterStatus.SetFlag(MasterStatus.CHEnabled, value);
        }
        /// <summary>Master: Domestic Hot Water enable flag.</summary>
        public bool MasterIsHotWaterActive
        {
            get => _masterStatus.IsSet(MasterStatus.DHWEnabled);
            set => _masterStatus = _masterStatus.SetFlag(MasterStatus.DHWEnabled, value);
        }
        /// <summary>Master: Cooling enable flag.</summary>
        public bool MasterIsCoolingActive
        {
            get => _masterStatus.IsSet(MasterStatus.CoolingEnabled);
            set => _masterStatus = _masterStatus.SetFlag(MasterStatus.CoolingEnabled, value);
        }
        /// <summary>Master: Outside Temperature Compensation active.</summary>
        public bool MasterOTCActive
        {
            get => _masterStatus.IsSet(MasterStatus.OTCActive);
            set => _masterStatus = _masterStatus.SetFlag(MasterStatus.OTCActive, value);
        }
        /// <summary>Master: CH2 enable flag.</summary>
        public bool MasterIsCentralHeating2Active
        {
            get => _masterStatus.IsSet(MasterStatus.CH2Enabled);
            set => _masterStatus = _masterStatus.SetFlag(MasterStatus.CH2Enabled, value);
        }

        /// <summary>Slave: Fault indication.</summary>
        public bool SlaveIsFault
        {
            get => _slaveStatus.IsSet(SlaveStatus.FaultIndication);
            set => _slaveStatus = _slaveStatus.SetFlag(SlaveStatus.FaultIndication, value);
        }
        /// <summary>Slave: Central Heating mode.</summary>
        public bool SlaveIsCentralHeatingActive
        {
            get => _slaveStatus.IsSet(SlaveStatus.CHMode);
            set => _slaveStatus = _slaveStatus.SetFlag(SlaveStatus.CHMode, value);
        }
        /// <summary>Slave: Domestic Hot Water mode.</summary>
        public bool SlaveIsHotWaterActive
        {
            get => _slaveStatus.IsSet(SlaveStatus.DHWMode);
            set => _slaveStatus = _slaveStatus.SetFlag(SlaveStatus.DHWMode, value);
        }
        /// <summary>Slave: Flame status.</summary>
        public bool SlaveIsFlameOn
        {
            get => _slaveStatus.IsSet(SlaveStatus.FlameStatus);
            set => _slaveStatus = _slaveStatus.SetFlag(SlaveStatus.FlameStatus, value);
        }
        /// <summary>Slave: Cooling status.</summary>
        public bool SlaveIsCoolingActive
        {
            get => _slaveStatus.IsSet(SlaveStatus.CoolingStatus);
            set => _slaveStatus = _slaveStatus.SetFlag(SlaveStatus.CoolingStatus, value);
        }
        /// <summary>Slave: CH2 mode.</summary>
        public bool SlaveIsCentralHeating2Active
        {
            get => _slaveStatus.IsSet(SlaveStatus.CH2Mode);
            set => _slaveStatus = _slaveStatus.SetFlag(SlaveStatus.CH2Mode, value);
        }
        /// <summary>Slave: Diagnostic indication.</summary>
        public bool SlaveDiagnosticIndicationActive
        {
            get => _slaveStatus.IsSet(SlaveStatus.DiagnosticIndication);
            set => _slaveStatus = _slaveStatus.SetFlag(SlaveStatus.DiagnosticIndication, value);
        }

    }
}
