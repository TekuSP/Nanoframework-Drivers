using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads fault flags and OEM fault code from the ventilation/heat-recovery subsystem.
    /// </summary>
    public class GetVentilationFaultRequest : ReadRequest
    {
        public GetVentilationFaultRequest() : base() { }
        public GetVentilationFaultRequest(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// Application-specific fault flags (low byte). See <see cref="Enums.ApplicationSpecificFaultFlags"/>.
        /// </summary>
        public ApplicationSpecificFaultFlags FaultFlags { get; set; }
        /// <summary>
        /// OEM-specific fault/diagnostic code reported by ventilation/heat-recovery (high byte).
        /// </summary>
        public byte OEMFaultCode { get; set; }

        protected override uint GetRawDataCore()
        {
            uint data = (uint)(((uint)OEMFaultCode << 8) | (byte)FaultFlags);
            return ProcessRequest(data);
        }
        protected override void SetRawDataCore(uint value)
        {
            FaultFlags = Utilities.GetApplicationSpecificFaultFlags(value);
            OEMFaultCode = Utilities.GetHighByte(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.ASFflagsOEMfaultCodeVentilationHeatRecovery;

        // Convenience flags
        public bool ServiceRequest { get => FaultFlags.IsSet(ApplicationSpecificFaultFlags.ServiceRequest); set => FaultFlags = FaultFlags.SetFlag(ApplicationSpecificFaultFlags.ServiceRequest, value); }
        public bool LockoutReset { get => FaultFlags.IsSet(ApplicationSpecificFaultFlags.LockoutReset); set => FaultFlags = FaultFlags.SetFlag(ApplicationSpecificFaultFlags.LockoutReset, value); }
        public bool LowWaterPress { get => FaultFlags.IsSet(ApplicationSpecificFaultFlags.LowWaterPress); set => FaultFlags = FaultFlags.SetFlag(ApplicationSpecificFaultFlags.LowWaterPress, value); }
        public bool GasFlameFault { get => FaultFlags.IsSet(ApplicationSpecificFaultFlags.GasFlameFault); set => FaultFlags = FaultFlags.SetFlag(ApplicationSpecificFaultFlags.GasFlameFault, value); }
        public bool AirPressFault { get => FaultFlags.IsSet(ApplicationSpecificFaultFlags.AirPressFault); set => FaultFlags = FaultFlags.SetFlag(ApplicationSpecificFaultFlags.AirPressFault, value); }
        public bool WaterOverTemp { get => FaultFlags.IsSet(ApplicationSpecificFaultFlags.WaterOverTemp); set => FaultFlags = FaultFlags.SetFlag(ApplicationSpecificFaultFlags.WaterOverTemp, value); }
        public bool Reserved6 { get => FaultFlags.IsSet(ApplicationSpecificFaultFlags.Reserved6); set => FaultFlags = FaultFlags.SetFlag(ApplicationSpecificFaultFlags.Reserved6, value); }
        public bool Reserved7 { get => FaultFlags.IsSet(ApplicationSpecificFaultFlags.Reserved7); set => FaultFlags = FaultFlags.SetFlag(ApplicationSpecificFaultFlags.Reserved7, value); }
    }
}
