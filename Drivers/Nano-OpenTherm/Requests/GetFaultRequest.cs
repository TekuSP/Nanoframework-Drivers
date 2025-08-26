using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
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
            uint data = (uint)(((uint)OEMFaultCode << 8) | (byte)FaultFlags);
            return ProcessRequest(data);
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
    public bool ServiceRequest { get => (FaultFlags & ApplicationSpecificFaultFlags.ServiceRequest) != 0; set { if (value) FaultFlags |= ApplicationSpecificFaultFlags.ServiceRequest; else FaultFlags &= ~ApplicationSpecificFaultFlags.ServiceRequest; } }
    /// <summary>Lockout reset flag.</summary>
    public bool LockoutReset { get => (FaultFlags & ApplicationSpecificFaultFlags.LockoutReset) != 0; set { if (value) FaultFlags |= ApplicationSpecificFaultFlags.LockoutReset; else FaultFlags &= ~ApplicationSpecificFaultFlags.LockoutReset; } }
    /// <summary>Low water pressure fault flag.</summary>
    public bool LowWaterPress { get => (FaultFlags & ApplicationSpecificFaultFlags.LowWaterPress) != 0; set { if (value) FaultFlags |= ApplicationSpecificFaultFlags.LowWaterPress; else FaultFlags &= ~ApplicationSpecificFaultFlags.LowWaterPress; } }
    /// <summary>Gas/flame fault flag.</summary>
    public bool GasFlameFault { get => (FaultFlags & ApplicationSpecificFaultFlags.GasFlameFault) != 0; set { if (value) FaultFlags |= ApplicationSpecificFaultFlags.GasFlameFault; else FaultFlags &= ~ApplicationSpecificFaultFlags.GasFlameFault; } }
    /// <summary>Air pressure fault flag.</summary>
    public bool AirPressFault { get => (FaultFlags & ApplicationSpecificFaultFlags.AirPressFault) != 0; set { if (value) FaultFlags |= ApplicationSpecificFaultFlags.AirPressFault; else FaultFlags &= ~ApplicationSpecificFaultFlags.AirPressFault; } }
    /// <summary>Water over-temperature fault flag.</summary>
    public bool WaterOverTemp { get => (FaultFlags & ApplicationSpecificFaultFlags.WaterOverTemp) != 0; set { if (value) FaultFlags |= ApplicationSpecificFaultFlags.WaterOverTemp; else FaultFlags &= ~ApplicationSpecificFaultFlags.WaterOverTemp; } }
    /// <summary>Reserved bit 6.</summary>
    public bool Reserved6 { get => (FaultFlags & ApplicationSpecificFaultFlags.Reserved6) != 0; set { if (value) FaultFlags |= ApplicationSpecificFaultFlags.Reserved6; else FaultFlags &= ~ApplicationSpecificFaultFlags.Reserved6; } }
    /// <summary>Reserved bit 7.</summary>
    public bool Reserved7 { get => (FaultFlags & ApplicationSpecificFaultFlags.Reserved7) != 0; set { if (value) FaultFlags |= ApplicationSpecificFaultFlags.Reserved7; else FaultFlags &= ~ApplicationSpecificFaultFlags.Reserved7; } }
    }
}
