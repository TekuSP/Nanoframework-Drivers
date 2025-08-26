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
            get => Utilities.IsSet(_slaveConfiguration, SlaveConfiguration.DHWPresent);
            set => Utilities.SetFlag(ref _slaveConfiguration, SlaveConfiguration.DHWPresent, value);
        }
        /// <summary>
        /// Control type.
        /// </summary>
    public bool ControlType
        {
            get => Utilities.IsSet(_slaveConfiguration, SlaveConfiguration.ControlType);
            set => Utilities.SetFlag(ref _slaveConfiguration, SlaveConfiguration.ControlType, value);
        }
        /// <summary>
        /// Cooling configuration.
        /// </summary>
    public bool CoolingConfig
        {
            get => Utilities.IsSet(_slaveConfiguration, SlaveConfiguration.CoolingConfig);
            set => Utilities.SetFlag(ref _slaveConfiguration, SlaveConfiguration.CoolingConfig, value);
        }
        /// <summary>
        /// DHW configuration.
        /// </summary>
    public bool DHWConfig
        {
            get => Utilities.IsSet(_slaveConfiguration, SlaveConfiguration.DHWConfig);
            set => Utilities.SetFlag(ref _slaveConfiguration, SlaveConfiguration.DHWConfig, value);
        }
        /// <summary>
        /// Master low-off & pump control function.
        /// </summary>
    public bool MasterLowOffPumpControl
        {
            get => Utilities.IsSet(_slaveConfiguration, SlaveConfiguration.MasterLowOffPumpControl);
            set => Utilities.SetFlag(ref _slaveConfiguration, SlaveConfiguration.MasterLowOffPumpControl, value);
        }
        /// <summary>
        /// CH2 (Central Heating circuit 2) present.
        /// </summary>
    public bool CH2Present
        {
            get => Utilities.IsSet(_slaveConfiguration, SlaveConfiguration.CH2Present);
            set => Utilities.SetFlag(ref _slaveConfiguration, SlaveConfiguration.CH2Present, value);
        }
        /// <summary>
        /// Slave MemberID Code
        /// </summary>
    public byte MemberIDCode { get => _memberIdCode; set => _memberIdCode = value; }
    }
}
