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

    // Convenience bit properties for SlaveConfiguration
    /// <summary>Domestic hot water present on slave.</summary>
    public bool DHWPresent { get => Utilities.IsSet(SlaveConfiguration, SC.DHWPresent); set => Utilities.SetFlag(ref SlaveConfiguration, SC.DHWPresent, value); }
    /// <summary>Control type (0: on/off, 1: modulating).</summary>
    public bool ControlType { get => Utilities.IsSet(SlaveConfiguration, SC.ControlType); set => Utilities.SetFlag(ref SlaveConfiguration, SC.ControlType, value); }
    /// <summary>Cooling capability configured on slave.</summary>
    public bool CoolingConfig { get => Utilities.IsSet(SlaveConfiguration, SC.CoolingConfig); set => Utilities.SetFlag(ref SlaveConfiguration, SC.CoolingConfig, value); }
    /// <summary>Domestic hot water configuration bit on slave.</summary>
    public bool DHWConfig { get => Utilities.IsSet(SlaveConfiguration, SC.DHWConfig); set => Utilities.SetFlag(ref SlaveConfiguration, SC.DHWConfig, value); }
    /// <summary>Master-controlled low/off pump control supported.</summary>
    public bool MasterLowOffPumpControl { get => Utilities.IsSet(SlaveConfiguration, SC.MasterLowOffPumpControl); set => Utilities.SetFlag(ref SlaveConfiguration, SC.MasterLowOffPumpControl, value); }
    /// <summary>Second central heating circuit present on slave.</summary>
    public bool CH2Present { get => Utilities.IsSet(SlaveConfiguration, SC.CH2Present); set => Utilities.SetFlag(ref SlaveConfiguration, SC.CH2Present, value); }
    /// <summary>Reserved bit 6.</summary>
    public bool Reserved6 { get => Utilities.IsSet(SlaveConfiguration, SC.Reserved6); set => Utilities.SetFlag(ref SlaveConfiguration, SC.Reserved6, value); }
    /// <summary>Reserved bit 7.</summary>
    public bool Reserved7 { get => Utilities.IsSet(SlaveConfiguration, SC.Reserved7); set => Utilities.SetFlag(ref SlaveConfiguration, SC.Reserved7, value); }
    }
}
