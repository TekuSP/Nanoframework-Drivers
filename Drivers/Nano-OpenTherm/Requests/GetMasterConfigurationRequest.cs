using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using MC = TekuSP.Drivers.Nano_OpenTherm.Enums.MasterConfiguration;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetMasterConfigurationRequest : ReadRequest
    {
        public GetMasterConfigurationRequest() : base() { }
        public GetMasterConfigurationRequest(Request baseReq) : base(baseReq) { }

    /// <summary>
    /// Master configuration flags (low byte). See <see cref="Enums.MasterConfiguration"/>.
    /// </summary>
    public MC MasterConfiguration { get; set; }
    /// <summary>
    /// Manufacturer/member ID code (high byte).
    /// </summary>
    public MemberIdCode MemberIdCode { get; set; }

        protected override uint GetRawDataCore()
        {
            // High byte = MemberIdCode, Low byte = MasterConfiguration
            uint data = (uint)(((byte)MemberIdCode << 8) | (byte)MasterConfiguration);
            return ProcessRequest(data);
        }
        protected override void SetRawDataCore(uint value)
        {
            MasterConfiguration = Utilities.GetMasterConfiguration(value);
            MemberIdCode = (MemberIdCode)Utilities.GetHighByte(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.MConfigMMemberIDcode;

    // Convenience bit properties to set/clear underlying flags
    /// <summary>Reserved bit 0.</summary>
    public bool Reserved0 { get => (MasterConfiguration & MC.Reserved0) != 0; set { if (value) MasterConfiguration |= MC.Reserved0; else MasterConfiguration &= ~MC.Reserved0; } }
    /// <summary>Reserved bit 1.</summary>
    public bool Reserved1 { get => (MasterConfiguration & MC.Reserved1) != 0; set { if (value) MasterConfiguration |= MC.Reserved1; else MasterConfiguration &= ~MC.Reserved1; } }
    /// <summary>Reserved bit 2.</summary>
    public bool Reserved2 { get => (MasterConfiguration & MC.Reserved2) != 0; set { if (value) MasterConfiguration |= MC.Reserved2; else MasterConfiguration &= ~MC.Reserved2; } }
    /// <summary>Reserved bit 3.</summary>
    public bool Reserved3 { get => (MasterConfiguration & MC.Reserved3) != 0; set { if (value) MasterConfiguration |= MC.Reserved3; else MasterConfiguration &= ~MC.Reserved3; } }
    /// <summary>Reserved bit 4.</summary>
    public bool Reserved4 { get => (MasterConfiguration & MC.Reserved4) != 0; set { if (value) MasterConfiguration |= MC.Reserved4; else MasterConfiguration &= ~MC.Reserved4; } }
    /// <summary>Reserved bit 5.</summary>
    public bool Reserved5 { get => (MasterConfiguration & MC.Reserved5) != 0; set { if (value) MasterConfiguration |= MC.Reserved5; else MasterConfiguration &= ~MC.Reserved5; } }
    /// <summary>Reserved bit 6.</summary>
    public bool Reserved6 { get => (MasterConfiguration & MC.Reserved6) != 0; set { if (value) MasterConfiguration |= MC.Reserved6; else MasterConfiguration &= ~MC.Reserved6; } }
    /// <summary>Reserved bit 7.</summary>
    public bool Reserved7 { get => (MasterConfiguration & MC.Reserved7) != 0; set { if (value) MasterConfiguration |= MC.Reserved7; else MasterConfiguration &= ~MC.Reserved7; } }
    }
}
