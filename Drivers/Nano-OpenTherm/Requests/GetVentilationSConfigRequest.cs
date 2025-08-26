using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using SC = TekuSP.Drivers.Nano_OpenTherm.Enums.SlaveConfiguration;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the ventilation/heat-recovery slave configuration flags and member ID code.
    /// </summary>
    public class GetVentilationSConfigRequest : ReadRequest
    {
        public GetVentilationSConfigRequest() : base() { }
        public GetVentilationSConfigRequest(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// Slave configuration flags (low byte). Use convenience boolean properties to inspect individual bits.
        /// </summary>
        public SC SlaveConfiguration { get; set; }
        /// <summary>
        /// Manufacturer/member ID code (high byte).
        /// </summary>
        public MemberIdCode MemberIdCode { get; set; }

        protected override uint GetRawDataCore()
        {
            // High byte: Member ID Code, Low byte: Slave Configuration
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
        public override MessageID MessageID => MessageID.SConfigSMemberIDCodeVentilationHeatRecovery;

        // Convenience bit properties for SlaveConfiguration using extensions
        /// <summary>Domestic hot water present on slave.</summary>
        public bool DHWPresent { get => SlaveConfiguration.IsSet(SC.DHWPresent); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SC.DHWPresent, value); }
        /// <summary>Control type (0: on/off, 1: modulating).</summary>
        public bool ControlType { get => SlaveConfiguration.IsSet(SC.ControlType); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SC.ControlType, value); }
        /// <summary>Cooling capability configured on slave.</summary>
        public bool CoolingConfig { get => SlaveConfiguration.IsSet(SC.CoolingConfig); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SC.CoolingConfig, value); }
        /// <summary>Domestic hot water configuration bit on slave.</summary>
        public bool DHWConfig { get => SlaveConfiguration.IsSet(SC.DHWConfig); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SC.DHWConfig, value); }
        /// <summary>Master-controlled low/off pump control supported.</summary>
        public bool MasterLowOffPumpControl { get => SlaveConfiguration.IsSet(SC.MasterLowOffPumpControl); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SC.MasterLowOffPumpControl, value); }
        /// <summary>Second central heating circuit present on slave.</summary>
        public bool CH2Present { get => SlaveConfiguration.IsSet(SC.CH2Present); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SC.CH2Present, value); }
        /// <summary>Reserved bit 6.</summary>
        public bool Reserved6 { get => SlaveConfiguration.IsSet(SC.Reserved6); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SC.Reserved6, value); }
        /// <summary>Reserved bit 7.</summary>
        public bool Reserved7 { get => SlaveConfiguration.IsSet(SC.Reserved7); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SC.Reserved7, value); }
    }
}
