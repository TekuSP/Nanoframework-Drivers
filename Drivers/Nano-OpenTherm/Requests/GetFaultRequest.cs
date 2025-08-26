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
        public bool ServiceRequest { get => FaultFlags.IsSet(ApplicationSpecificFaultFlags.ServiceRequest); set => FaultFlags = FaultFlags.SetFlag(ApplicationSpecificFaultFlags.ServiceRequest, value); }
        /// <summary>Lockout reset flag.</summary>
        public bool LockoutReset { get => FaultFlags.IsSet(ApplicationSpecificFaultFlags.LockoutReset); set => FaultFlags = FaultFlags.SetFlag(ApplicationSpecificFaultFlags.LockoutReset, value); }
        /// <summary>Low water pressure fault flag.</summary>
        public bool LowWaterPress { get => FaultFlags.IsSet(ApplicationSpecificFaultFlags.LowWaterPress); set => FaultFlags = FaultFlags.SetFlag(ApplicationSpecificFaultFlags.LowWaterPress, value); }
        /// <summary>Gas/flame fault flag.</summary>
        public bool GasFlameFault { get => FaultFlags.IsSet(ApplicationSpecificFaultFlags.GasFlameFault); set => FaultFlags = FaultFlags.SetFlag(ApplicationSpecificFaultFlags.GasFlameFault, value); }
        /// <summary>Air pressure fault flag.</summary>
        public bool AirPressFault { get => FaultFlags.IsSet(ApplicationSpecificFaultFlags.AirPressFault); set => FaultFlags = FaultFlags.SetFlag(ApplicationSpecificFaultFlags.AirPressFault, value); }
        /// <summary>Water over-temperature fault flag.</summary>
        public bool WaterOverTemp { get => FaultFlags.IsSet(ApplicationSpecificFaultFlags.WaterOverTemp); set => FaultFlags = FaultFlags.SetFlag(ApplicationSpecificFaultFlags.WaterOverTemp, value); }
        /// <summary>Reserved bit 6.</summary>
        public bool Reserved6 { get => FaultFlags.IsSet(ApplicationSpecificFaultFlags.Reserved6); set => FaultFlags = FaultFlags.SetFlag(ApplicationSpecificFaultFlags.Reserved6, value); }
        /// <summary>Reserved bit 7.</summary>
        public bool Reserved7 { get => FaultFlags.IsSet(ApplicationSpecificFaultFlags.Reserved7); set => FaultFlags = FaultFlags.SetFlag(ApplicationSpecificFaultFlags.Reserved7, value); }
    }
}
