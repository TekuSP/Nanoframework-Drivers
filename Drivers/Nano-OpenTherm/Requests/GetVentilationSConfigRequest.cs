using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using SC = TekuSP.Drivers.Nano_OpenTherm.Enums.SlaveConfiguration;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetVentilationSConfigRequest : ReadRequest
    {
        public GetVentilationSConfigRequest() : base() { }
        public GetVentilationSConfigRequest(Request baseReq) : base(baseReq) { }

        public SC SlaveConfiguration { get; set; }
        public MemberIdCode MemberIdCode { get; set; }

        protected override uint GetRawDataCore()
        {
            // High byte = Member ID, Low byte = Slave configuration flags
            uint raw = (uint)(((byte)MemberIdCode << 8) | (byte)SlaveConfiguration);
            return ProcessRequest(raw);
        }
        protected override void SetRawDataCore(uint value)
        {
            SlaveConfiguration = Utilities.GetSlaveConfiguration(value);
            MemberIdCode = (MemberIdCode)Utilities.GetHighByte(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.SConfigSMemberIDCodeVentilationHeatRecovery;

        // Convenience bit properties for SlaveConfiguration
        public bool DHWPresent { get => (SlaveConfiguration & SC.DHWPresent) != 0; set { if (value) SlaveConfiguration |= SC.DHWPresent; else SlaveConfiguration &= ~SC.DHWPresent; } }
        public bool ControlType { get => (SlaveConfiguration & SC.ControlType) != 0; set { if (value) SlaveConfiguration |= SC.ControlType; else SlaveConfiguration &= ~SC.ControlType; } }
        public bool CoolingConfig { get => (SlaveConfiguration & SC.CoolingConfig) != 0; set { if (value) SlaveConfiguration |= SC.CoolingConfig; else SlaveConfiguration &= ~SC.CoolingConfig; } }
        public bool DHWConfig { get => (SlaveConfiguration & SC.DHWConfig) != 0; set { if (value) SlaveConfiguration |= SC.DHWConfig; else SlaveConfiguration &= ~SC.DHWConfig; } }
        public bool MasterLowOffPumpControl { get => (SlaveConfiguration & SC.MasterLowOffPumpControl) != 0; set { if (value) SlaveConfiguration |= SC.MasterLowOffPumpControl; else SlaveConfiguration &= ~SC.MasterLowOffPumpControl; } }
        public bool CH2Present { get => (SlaveConfiguration & SC.CH2Present) != 0; set { if (value) SlaveConfiguration |= SC.CH2Present; else SlaveConfiguration &= ~SC.CH2Present; } }
        public bool Reserved6 { get => (SlaveConfiguration & SC.Reserved6) != 0; set { if (value) SlaveConfiguration |= SC.Reserved6; else SlaveConfiguration &= ~SC.Reserved6; } }
        public bool Reserved7 { get => (SlaveConfiguration & SC.Reserved7) != 0; set { if (value) SlaveConfiguration |= SC.Reserved7; else SlaveConfiguration &= ~SC.Reserved7; } }
    }
}
