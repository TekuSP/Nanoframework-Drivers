using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using MC = TekuSP.Drivers.Nano_OpenTherm.Enums.MasterConfiguration;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetSlaveConfigurationRequest : ReadRequest
    {
        public GetSlaveConfigurationRequest() : base() { }
        public GetSlaveConfigurationRequest(Request baseReq) : base(baseReq) { }

        // Expose the enum publicly and allow set so callers can compose flags and have them encoded
        public MC MasterConfiguration { get; set; }

        protected override ulong GetRawDataCore()
        {
            uint data = (uint)(byte)MasterConfiguration;
            return ProcessRequest(data);
        }
        protected override void SetRawDataCore(ulong value)
        {
            MasterConfiguration = Utilities.GetMasterConfiguration(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.SConfigSMemberIDcode;

        // Convenience bit properties that read/write the underlying enum flags
        public bool Reserved0 { get => (MasterConfiguration & MC.Reserved0) != 0; set { if (value) MasterConfiguration |= MC.Reserved0; else MasterConfiguration &= ~MC.Reserved0; } }
        public bool Reserved1 { get => (MasterConfiguration & MC.Reserved1) != 0; set { if (value) MasterConfiguration |= MC.Reserved1; else MasterConfiguration &= ~MC.Reserved1; } }
        public bool Reserved2 { get => (MasterConfiguration & MC.Reserved2) != 0; set { if (value) MasterConfiguration |= MC.Reserved2; else MasterConfiguration &= ~MC.Reserved2; } }
        public bool Reserved3 { get => (MasterConfiguration & MC.Reserved3) != 0; set { if (value) MasterConfiguration |= MC.Reserved3; else MasterConfiguration &= ~MC.Reserved3; } }
        public bool Reserved4 { get => (MasterConfiguration & MC.Reserved4) != 0; set { if (value) MasterConfiguration |= MC.Reserved4; else MasterConfiguration &= ~MC.Reserved4; } }
        public bool Reserved5 { get => (MasterConfiguration & MC.Reserved5) != 0; set { if (value) MasterConfiguration |= MC.Reserved5; else MasterConfiguration &= ~MC.Reserved5; } }
        public bool Reserved6 { get => (MasterConfiguration & MC.Reserved6) != 0; set { if (value) MasterConfiguration |= MC.Reserved6; else MasterConfiguration &= ~MC.Reserved6; } }
        public bool Reserved7 { get => (MasterConfiguration & MC.Reserved7) != 0; set { if (value) MasterConfiguration |= MC.Reserved7; else MasterConfiguration &= ~MC.Reserved7; } }
    }
}
