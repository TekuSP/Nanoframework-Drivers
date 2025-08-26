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
        public override MessageID MessageID => MessageID.ASFflagsOEMfaultCodeVentilationHeatRecovery;

        // Convenience flags
    public bool ServiceRequest { get => Utilities.IsSet(FaultFlags, ApplicationSpecificFaultFlags.ServiceRequest); set => Utilities.SetFlag(ref FaultFlags, ApplicationSpecificFaultFlags.ServiceRequest, value); }
    public bool LockoutReset { get => Utilities.IsSet(FaultFlags, ApplicationSpecificFaultFlags.LockoutReset); set => Utilities.SetFlag(ref FaultFlags, ApplicationSpecificFaultFlags.LockoutReset, value); }
    public bool LowWaterPress { get => Utilities.IsSet(FaultFlags, ApplicationSpecificFaultFlags.LowWaterPress); set => Utilities.SetFlag(ref FaultFlags, ApplicationSpecificFaultFlags.LowWaterPress, value); }
    public bool GasFlameFault { get => Utilities.IsSet(FaultFlags, ApplicationSpecificFaultFlags.GasFlameFault); set => Utilities.SetFlag(ref FaultFlags, ApplicationSpecificFaultFlags.GasFlameFault, value); }
    public bool AirPressFault { get => Utilities.IsSet(FaultFlags, ApplicationSpecificFaultFlags.AirPressFault); set => Utilities.SetFlag(ref FaultFlags, ApplicationSpecificFaultFlags.AirPressFault, value); }
    public bool WaterOverTemp { get => Utilities.IsSet(FaultFlags, ApplicationSpecificFaultFlags.WaterOverTemp); set => Utilities.SetFlag(ref FaultFlags, ApplicationSpecificFaultFlags.WaterOverTemp, value); }
    public bool Reserved6 { get => Utilities.IsSet(FaultFlags, ApplicationSpecificFaultFlags.Reserved6); set => Utilities.SetFlag(ref FaultFlags, ApplicationSpecificFaultFlags.Reserved6, value); }
    public bool Reserved7 { get => Utilities.IsSet(FaultFlags, ApplicationSpecificFaultFlags.Reserved7); set => Utilities.SetFlag(ref FaultFlags, ApplicationSpecificFaultFlags.Reserved7, value); }
    }
}
