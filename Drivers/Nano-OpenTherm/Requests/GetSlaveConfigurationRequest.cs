using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using SC = TekuSP.Drivers.Nano_OpenTherm.Enums.SlaveConfiguration;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the slave configuration flags and member ID code.
    /// </summary>
    public class GetSlaveConfigurationRequest : ReadRequest
    {
        public GetSlaveConfigurationRequest() : base() { }
        public GetSlaveConfigurationRequest(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// Slave configuration flags (low byte). Use convenience properties to access individual bits.
        /// </summary>
        public SC SlaveConfiguration { get; set; }
        /// <summary>
        /// Slave Member ID code (high byte).
        /// </summary>
        public MemberIdCode MemberIdCode { get; set; }

        protected override uint GetRawDataCore()
        {
            // High byte = MemberIdCode, Low byte = SlaveConfiguration
            byte low = Utilities.SetSlaveConfiguration(SlaveConfiguration);
            ushort payload = Utilities.MakeUShort((byte)MemberIdCode, low);
            return ProcessRequest(payload);
        }
        protected override void SetRawDataCore(uint value)
        {
            SlaveConfiguration = Utilities.GetSlaveConfiguration(value);
            MemberIdCode = (MemberIdCode)Utilities.GetHighByte(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.SConfigSMemberIDcode;

        // Convenience bit properties for SlaveConfiguration
        /// <summary>Domestic Hot Water present on slave.</summary>
        public bool DHWPresent { get => SlaveConfiguration.IsSet(SC.DHWPresent); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SC.DHWPresent, value); }
        /// <summary>Control type bit in slave configuration.</summary>
        public bool ControlType { get => SlaveConfiguration.IsSet(SC.ControlType); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SC.ControlType, value); }
        /// <summary>Cooling capability configured on slave.</summary>
        public bool CoolingConfig { get => SlaveConfiguration.IsSet(SC.CoolingConfig); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SC.CoolingConfig, value); }
        /// <summary>Domestic Hot Water configuration bit on slave.</summary>
        public bool DHWConfig { get => SlaveConfiguration.IsSet(SC.DHWConfig); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SC.DHWConfig, value); }
        /// <summary>Master controlled low/off pump control supported.</summary>
        public bool MasterLowOffPumpControl { get => SlaveConfiguration.IsSet(SC.MasterLowOffPumpControl); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SC.MasterLowOffPumpControl, value); }
        /// <summary>Second central heating circuit present on slave.</summary>
        public bool CH2Present { get => SlaveConfiguration.IsSet(SC.CH2Present); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SC.CH2Present, value); }
        /// <summary>Reserved bit 6.</summary>
        public bool Reserved6 { get => SlaveConfiguration.IsSet(SC.Reserved6); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SC.Reserved6, value); }
        /// <summary>Reserved bit 7.</summary>
        public bool Reserved7 { get => SlaveConfiguration.IsSet(SC.Reserved7); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SC.Reserved7, value); }
    }
}
