using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using TekuSP.Drivers.Nano_OpenTherm.Interfaces;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// Ventilation/Heat-recovery application-specific fault flags and OEM fault code response.
    /// High byte = flags, Low byte = OEM fault code.
    /// </summary>
    public class VentilationApplicationFaultCodesResponse : Response, IApplicationSpecificFaultFlags
    {
        protected ApplicationSpecificFaultFlags ApplicationSpecificFaultFlags { get; set; }
        public byte OEMFaultCodes { get; set; }

        public VentilationApplicationFaultCodesResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public VentilationApplicationFaultCodesResponse(Response r) : base(r) { }

        protected override uint GetRawDataCore()
        {
            byte high = Utilities.SetApplicationSpecificFaultFlags(ApplicationSpecificFaultFlags);
            return ProcessResponse(Utilities.MakeUShort(high, OEMFaultCodes));
        }

        protected override void SetRawDataCore(uint value)
        {
            ApplicationSpecificFaultFlags = Utilities.GetApplicationSpecificFaultFlags(value);
            OEMFaultCodes = Utilities.GetLowByte(value);
        }

        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.ASFflagsOEMfaultCodeVentilationHeatRecovery;

        // IApplicationSpecificFaultFlags convenience
        public bool FaultServiceRequest { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.ServiceRequest); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.ServiceRequest, value); }
        public bool FaultLockoutReset { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.LockoutReset); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.LockoutReset, value); }
        public bool FaultLowWaterPressure { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.LowWaterPress); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.LowWaterPress, value); }
        public bool FaultGasFlame { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.GasFlameFault); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.GasFlameFault, value); }
        public bool FaultAirPressure { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.AirPressFault); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.AirPressFault, value); }
        public bool FaultWaterOverTemperature { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.WaterOverTemp); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.WaterOverTemp, value); }
        public bool FaultReserved6 { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.Reserved6); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.Reserved6, value); }
        public bool FaultReserved7 { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.Reserved7); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.Reserved7, value); }
    }
}
