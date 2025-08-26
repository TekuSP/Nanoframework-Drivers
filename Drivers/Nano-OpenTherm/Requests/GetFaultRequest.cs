using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the application-specific fault flags and OEM fault/diagnostic code.
    /// </summary>
    public class GetFaultRequest : ReadRequest
    {
        public GetFaultRequest() : base() { }
        public GetFaultRequest(Request baseReq) : base(baseReq) { }

    /// <summary>
    /// Application-specific fault flags (low byte). See <see cref="Enums.ApplicationSpecificFaultFlags"/>.
    /// </summary>
    public ApplicationSpecificFaultFlags FaultFlags { get; set; }
    /// <summary>
    /// OEM-specific fault/diagnostic code (high byte).
    /// </summary>
    public byte OEMFaultCode { get; set; }

        protected override uint GetRawDataCore()
        {
            // High byte = OEM fault code, low byte = ASF flags
            byte low = Utilities.SetApplicationSpecificFaultFlags(FaultFlags);
            ushort payload = Utilities.MakeUShort(OEMFaultCode, low);
            return ProcessRequest(payload);
        }
        protected override void SetRawDataCore(uint value)
        {
            FaultFlags = Utilities.GetApplicationSpecificFaultFlags(value);
            OEMFaultCode = Utilities.GetHighByte(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.ASFflags;

    // Convenience flag properties
    /// <summary>Service request flag.</summary>
    public bool ServiceRequest { get => Utilities.IsSet(FaultFlags, ApplicationSpecificFaultFlags.ServiceRequest); set => Utilities.SetFlag(ref FaultFlags, ApplicationSpecificFaultFlags.ServiceRequest, value); }
    /// <summary>Lockout reset flag.</summary>
    public bool LockoutReset { get => Utilities.IsSet(FaultFlags, ApplicationSpecificFaultFlags.LockoutReset); set => Utilities.SetFlag(ref FaultFlags, ApplicationSpecificFaultFlags.LockoutReset, value); }
    /// <summary>Low water pressure fault flag.</summary>
    public bool LowWaterPress { get => Utilities.IsSet(FaultFlags, ApplicationSpecificFaultFlags.LowWaterPress); set => Utilities.SetFlag(ref FaultFlags, ApplicationSpecificFaultFlags.LowWaterPress, value); }
    /// <summary>Gas/flame fault flag.</summary>
    public bool GasFlameFault { get => Utilities.IsSet(FaultFlags, ApplicationSpecificFaultFlags.GasFlameFault); set => Utilities.SetFlag(ref FaultFlags, ApplicationSpecificFaultFlags.GasFlameFault, value); }
    /// <summary>Air pressure fault flag.</summary>
    public bool AirPressFault { get => Utilities.IsSet(FaultFlags, ApplicationSpecificFaultFlags.AirPressFault); set => Utilities.SetFlag(ref FaultFlags, ApplicationSpecificFaultFlags.AirPressFault, value); }
    /// <summary>Water over-temperature fault flag.</summary>
    public bool WaterOverTemp { get => Utilities.IsSet(FaultFlags, ApplicationSpecificFaultFlags.WaterOverTemp); set => Utilities.SetFlag(ref FaultFlags, ApplicationSpecificFaultFlags.WaterOverTemp, value); }
    /// <summary>Reserved bit 6.</summary>
    public bool Reserved6 { get => Utilities.IsSet(FaultFlags, ApplicationSpecificFaultFlags.Reserved6); set => Utilities.SetFlag(ref FaultFlags, ApplicationSpecificFaultFlags.Reserved6, value); }
    /// <summary>Reserved bit 7.</summary>
    public bool Reserved7 { get => Utilities.IsSet(FaultFlags, ApplicationSpecificFaultFlags.Reserved7); set => Utilities.SetFlag(ref FaultFlags, ApplicationSpecificFaultFlags.Reserved7, value); }
    }
}
