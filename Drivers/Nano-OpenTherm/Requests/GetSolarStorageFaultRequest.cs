using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetSolarStorageFaultRequest : ReadRequest
    {
        public GetSolarStorageFaultRequest() : base() { }
        public GetSolarStorageFaultRequest(Request baseReq) : base(baseReq) { }

        public ApplicationSpecificFaultFlags FaultFlags { get; set; }
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
        public override MessageID MessageID => MessageID.ASFflagsOEMfaultCodeSolarStorage;

        // Convenience flags
        public bool ServiceRequest { get => (FaultFlags & ApplicationSpecificFaultFlags.ServiceRequest) != 0; set { if (value) FaultFlags |= ApplicationSpecificFaultFlags.ServiceRequest; else FaultFlags &= ~ApplicationSpecificFaultFlags.ServiceRequest; } }
        public bool LockoutReset { get => (FaultFlags & ApplicationSpecificFaultFlags.LockoutReset) != 0; set { if (value) FaultFlags |= ApplicationSpecificFaultFlags.LockoutReset; else FaultFlags &= ~ApplicationSpecificFaultFlags.LockoutReset; } }
        public bool LowWaterPress { get => (FaultFlags & ApplicationSpecificFaultFlags.LowWaterPress) != 0; set { if (value) FaultFlags |= ApplicationSpecificFaultFlags.LowWaterPress; else FaultFlags &= ~ApplicationSpecificFaultFlags.LowWaterPress; } }
        public bool GasFlameFault { get => (FaultFlags & ApplicationSpecificFaultFlags.GasFlameFault) != 0; set { if (value) FaultFlags |= ApplicationSpecificFaultFlags.GasFlameFault; else FaultFlags &= ~ApplicationSpecificFaultFlags.GasFlameFault; } }
        public bool AirPressFault { get => (FaultFlags & ApplicationSpecificFaultFlags.AirPressFault) != 0; set { if (value) FaultFlags |= ApplicationSpecificFaultFlags.AirPressFault; else FaultFlags &= ~ApplicationSpecificFaultFlags.AirPressFault; } }
        public bool WaterOverTemp { get => (FaultFlags & ApplicationSpecificFaultFlags.WaterOverTemp) != 0; set { if (value) FaultFlags |= ApplicationSpecificFaultFlags.WaterOverTemp; else FaultFlags &= ~ApplicationSpecificFaultFlags.WaterOverTemp; } }
        public bool Reserved6 { get => (FaultFlags & ApplicationSpecificFaultFlags.Reserved6) != 0; set { if (value) FaultFlags |= ApplicationSpecificFaultFlags.Reserved6; else FaultFlags &= ~ApplicationSpecificFaultFlags.Reserved6; } }
        public bool Reserved7 { get => (FaultFlags & ApplicationSpecificFaultFlags.Reserved7) != 0; set { if (value) FaultFlags |= ApplicationSpecificFaultFlags.Reserved7; else FaultFlags &= ~ApplicationSpecificFaultFlags.Reserved7; } }
    }
}
