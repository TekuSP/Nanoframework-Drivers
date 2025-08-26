using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using MC = TekuSP.Drivers.Nano_OpenTherm.Enums.MasterConfiguration;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the master configuration flags and member ID code.
    /// </summary>
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
            byte low = Utilities.SetMasterConfiguration(MasterConfiguration);
            ushort payload = Utilities.MakeUShort((byte)MemberIdCode, low);
            return ProcessRequest(payload);
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
    public bool Reserved0 { get => Utilities.IsSet(MasterConfiguration, MC.Reserved0); set => Utilities.SetFlag(ref MasterConfiguration, MC.Reserved0, value); }
    /// <summary>Reserved bit 1.</summary>
    public bool Reserved1 { get => Utilities.IsSet(MasterConfiguration, MC.Reserved1); set => Utilities.SetFlag(ref MasterConfiguration, MC.Reserved1, value); }
    /// <summary>Reserved bit 2.</summary>
    public bool Reserved2 { get => Utilities.IsSet(MasterConfiguration, MC.Reserved2); set => Utilities.SetFlag(ref MasterConfiguration, MC.Reserved2, value); }
    /// <summary>Reserved bit 3.</summary>
    public bool Reserved3 { get => Utilities.IsSet(MasterConfiguration, MC.Reserved3); set => Utilities.SetFlag(ref MasterConfiguration, MC.Reserved3, value); }
    /// <summary>Reserved bit 4.</summary>
    public bool Reserved4 { get => Utilities.IsSet(MasterConfiguration, MC.Reserved4); set => Utilities.SetFlag(ref MasterConfiguration, MC.Reserved4, value); }
    /// <summary>Reserved bit 5.</summary>
    public bool Reserved5 { get => Utilities.IsSet(MasterConfiguration, MC.Reserved5); set => Utilities.SetFlag(ref MasterConfiguration, MC.Reserved5, value); }
    /// <summary>Reserved bit 6.</summary>
    public bool Reserved6 { get => Utilities.IsSet(MasterConfiguration, MC.Reserved6); set => Utilities.SetFlag(ref MasterConfiguration, MC.Reserved6, value); }
    /// <summary>Reserved bit 7.</summary>
    public bool Reserved7 { get => Utilities.IsSet(MasterConfiguration, MC.Reserved7); set => Utilities.SetFlag(ref MasterConfiguration, MC.Reserved7, value); }
    }
}
