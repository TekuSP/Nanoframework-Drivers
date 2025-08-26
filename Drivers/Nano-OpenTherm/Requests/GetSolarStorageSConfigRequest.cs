using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using SC = TekuSP.Drivers.Nano_OpenTherm.Enums.SlaveConfiguration;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetSolarStorageSConfigRequest : ReadRequest
    {
        public GetSolarStorageSConfigRequest() : base() { }
        public GetSolarStorageSConfigRequest(Request baseReq) : base(baseReq) { }

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

    // Convenience bit properties
    /// <summary>Domestic Hot Water present on slave.</summary>
    public bool DHWPresent { get => (SlaveConfiguration & SC.DHWPresent) != 0; set { if (value) SlaveConfiguration |= SC.DHWPresent; else SlaveConfiguration &= ~SC.DHWPresent; } }
    /// <summary>Control type flag.</summary>
    public bool ControlType { get => (SlaveConfiguration & SC.ControlType) != 0; set { if (value) SlaveConfiguration |= SC.ControlType; else SlaveConfiguration &= ~SC.ControlType; } }
    /// <summary>Cooling capability configured on slave.</summary>
    public bool CoolingConfig { get => (SlaveConfiguration & SC.CoolingConfig) != 0; set { if (value) SlaveConfiguration |= SC.CoolingConfig; else SlaveConfiguration &= ~SC.CoolingConfig; } }
    /// <summary>Domestic Hot Water configuration bit on slave.</summary>
    public bool DHWConfig { get => (SlaveConfiguration & SC.DHWConfig) != 0; set { if (value) SlaveConfiguration |= SC.DHWConfig; else SlaveConfiguration &= ~SC.DHWConfig; } }
    /// <summary>Master low/off pump control supported.</summary>
    public bool MasterLowOffPumpControl { get => (SlaveConfiguration & SC.MasterLowOffPumpControl) != 0; set { if (value) SlaveConfiguration |= SC.MasterLowOffPumpControl; else SlaveConfiguration &= ~SC.MasterLowOffPumpControl; } }
    /// <summary>Second central heating circuit present on slave.</summary>
    public bool CH2Present { get => (SlaveConfiguration & SC.CH2Present) != 0; set { if (value) SlaveConfiguration |= SC.CH2Present; else SlaveConfiguration &= ~SC.CH2Present; } }
    /// <summary>Reserved bit 6.</summary>
    public bool Reserved6 { get => (SlaveConfiguration & SC.Reserved6) != 0; set { if (value) SlaveConfiguration |= SC.Reserved6; else SlaveConfiguration &= ~SC.Reserved6; } }
    /// <summary>Reserved bit 7.</summary>
    public bool Reserved7 { get => (SlaveConfiguration & SC.Reserved7) != 0; set { if (value) SlaveConfiguration |= SC.Reserved7; else SlaveConfiguration &= ~SC.Reserved7; } }
    }
}
