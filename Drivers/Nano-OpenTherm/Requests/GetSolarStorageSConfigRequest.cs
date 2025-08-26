using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the Solar Storage slave configuration and member ID code.
    /// </summary>
    /// <remarks>
    /// The low byte contains <see cref="SlaveConfiguration"/> flags and the high byte contains
    /// the <see cref="MemberIdCode"/> identifying the manufacturer/OEM. Convenience boolean
    /// properties are provided to access individual configuration bits.
    /// </remarks>
    public class GetSolarStorageSConfigRequest : ReadRequest
    {
        public GetSolarStorageSConfigRequest() : base() { }
        public GetSolarStorageSConfigRequest(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// Slave configuration flags (low byte). Use convenience properties to inspect individual bits.
        /// </summary>
        public SlaveConfiguration SlaveConfiguration { get; set; }
        /// <summary>
        /// Manufacturer/member ID code (high byte).
        /// </summary>
        public MemberIdCode MemberIdCode { get; set; }

        protected override uint GetRawDataCore()
        {
            uint raw = (uint)(((byte)MemberIdCode << 8) | (byte)SlaveConfiguration);
            return ProcessRequest(raw);
        }
        protected override void SetRawDataCore(uint value)
        {
            SlaveConfiguration = Utilities.GetSlaveConfiguration(value);
            MemberIdCode = (MemberIdCode)Utilities.GetHighByte(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.SConfigSMemberIDcodeSolarStorage;

        // Convenience bit properties using new extensions
        /// <summary>Domestic Hot Water present on slave.</summary>
        public bool DHWPresent { get => SlaveConfiguration.IsSet(SlaveConfiguration.DHWPresent); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SlaveConfiguration.DHWPresent, value); }
        /// <summary>Control type flag.</summary>
        public bool ControlType { get => SlaveConfiguration.IsSet(SlaveConfiguration.ControlType); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SlaveConfiguration.ControlType, value); }
        /// <summary>Cooling capability configured on slave.</summary>
        public bool CoolingConfig { get => SlaveConfiguration.IsSet(SlaveConfiguration.CoolingConfig); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SlaveConfiguration.CoolingConfig, value); }
        /// <summary>Domestic Hot Water configuration bit on slave.</summary>
        public bool DHWConfig { get => SlaveConfiguration.IsSet(SlaveConfiguration.DHWConfig); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SlaveConfiguration.DHWConfig, value); }
        /// <summary>Master low/off pump control supported.</summary>
        public bool MasterLowOffPumpControl { get => SlaveConfiguration.IsSet(SlaveConfiguration.MasterLowOffPumpControl); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SlaveConfiguration.MasterLowOffPumpControl, value); }
        /// <summary>Second central heating circuit present on slave.</summary>
        public bool CH2Present { get => SlaveConfiguration.IsSet(SlaveConfiguration.CH2Present); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SlaveConfiguration.CH2Present, value); }
        /// <summary>Reserved bit 6.</summary>
        public bool Reserved6 { get => SlaveConfiguration.IsSet(SlaveConfiguration.Reserved6); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SlaveConfiguration.Reserved6, value); }
        /// <summary>Reserved bit 7.</summary>
        public bool Reserved7 { get => SlaveConfiguration.IsSet(SlaveConfiguration.Reserved7); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SlaveConfiguration.Reserved7, value); }
    }
}
