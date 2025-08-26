using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class SlaveConfigResponse : Response
    {
        private SlaveConfiguration _slaveConfiguration;
        private byte _memberIdCode;

        /// <summary>
        /// Initializes a new response for constructing a frame to send.
        /// </summary>
        /// <param name="messageType">Optional message type to use. Defaults to <see cref="TekuSP.Drivers.Nano_OpenTherm.Enums.MessageType.READ_ACK"/>.</param>
        public SlaveConfigResponse(MessageType messageType = MessageType.READ_ACK)
        {
            MessageType = messageType;
        }

        public SlaveConfigResponse(Response baseResponse)
        {
            MessageType = baseResponse.MessageType;
            SetRawDataCore(baseResponse.RawData);
        }

        protected override uint GetRawDataCore()
        {
            // Low byte: SlaveConfiguration flags, High byte: Member ID (as per original mapping)
            var low = Utilities.SetSlaveConfiguration(_slaveConfiguration);
            ushort payload = Utilities.MakeUShort(_memberIdCode, low);
            return ProcessResponse(payload);
        }
        protected override void SetRawDataCore(uint value)
        {
            _slaveConfiguration = Utilities.GetSlaveConfiguration(value);
            _memberIdCode = Utilities.GetHighByte(value);
        }

        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.SConfigSMemberIDcode;
        /// <summary>
        /// DHW (Domestic Hot Water) present.
        /// </summary>
        public bool DHWPresent
        {
            get => _slaveConfiguration.IsSet(SlaveConfiguration.DHWPresent);
            set => _slaveConfiguration = _slaveConfiguration.SetFlag(SlaveConfiguration.DHWPresent, value);
        }
        /// <summary>
        /// Control type.
        /// </summary>
        public bool ControlType
        {
            get => _slaveConfiguration.IsSet(SlaveConfiguration.ControlType);
            set => _slaveConfiguration = _slaveConfiguration.SetFlag(SlaveConfiguration.ControlType, value);
        }
        /// <summary>
        /// Cooling configuration.
        /// </summary>
        public bool CoolingConfig
        {
            get => _slaveConfiguration.IsSet(SlaveConfiguration.CoolingConfig);
            set => _slaveConfiguration = _slaveConfiguration.SetFlag(SlaveConfiguration.CoolingConfig, value);
        }
        /// <summary>
        /// DHW configuration.
        /// </summary>
        public bool DHWConfig
        {
            get => _slaveConfiguration.IsSet(SlaveConfiguration.DHWConfig);
            set => _slaveConfiguration = _slaveConfiguration.SetFlag(SlaveConfiguration.DHWConfig, value);
        }
        /// <summary>
        /// Master low-off & pump control function.
        /// </summary>
        public bool MasterLowOffPumpControl
        {
            get => _slaveConfiguration.IsSet(SlaveConfiguration.MasterLowOffPumpControl);
            set => _slaveConfiguration = _slaveConfiguration.SetFlag(SlaveConfiguration.MasterLowOffPumpControl, value);
        }
        /// <summary>
        /// CH2 (Central Heating circuit 2) present.
        /// </summary>
        public bool CH2Present
        {
            get => _slaveConfiguration.IsSet(SlaveConfiguration.CH2Present);
            set => _slaveConfiguration = _slaveConfiguration.SetFlag(SlaveConfiguration.CH2Present, value);
        }
        /// <summary>
        /// Slave MemberID Code
        /// </summary>
        public byte MemberIDCode { get => _memberIdCode; set => _memberIdCode = value; }
    }
}
