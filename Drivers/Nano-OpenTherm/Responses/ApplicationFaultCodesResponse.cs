using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using TekuSP.Drivers.Nano_OpenTherm.Interfaces;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class ApplicationFaultCodesResponse : Response, IApplicationSpecificFaultFlags
    {
        // Use protected property for enum backing per project convention
        protected ApplicationSpecificFaultFlags ApplicationSpecificFaultFlags { get; set; }
        private byte _oemFaultCodes;

        /// <summary>
        /// Initializes a new response for constructing a frame to send.
        /// </summary>
        /// <param name="messageType">Optional message type to use. Defaults to <see cref="TekuSP.Drivers.Nano_OpenTherm.Enums.MessageType.READ_ACK"/>.</param>
        public ApplicationFaultCodesResponse(MessageType messageType = MessageType.READ_ACK)
        {
            MessageType = messageType;
        }
        /// <summary>
        /// Convenience constructor to initialize both flags (HB) and OEM fault codes (LB).
        /// </summary>
        public ApplicationFaultCodesResponse(ApplicationSpecificFaultFlags flags, byte oemCodes, MessageType mt = MessageType.READ_ACK)
        {
            MessageType = mt;
            ApplicationSpecificFaultFlags = flags;
            _oemFaultCodes = oemCodes;
        }

    public ApplicationFaultCodesResponse(Response baseResponse) : base(baseResponse) { }

        protected override uint GetRawDataCore()
        {
            // Low byte = OEM fault code, High byte = flags
            var high = Utilities.SetApplicationSpecificFaultFlags(ApplicationSpecificFaultFlags);
            ushort payload = Utilities.MakeUShort(high, _oemFaultCodes);
            return ProcessResponse(payload);
        }
        protected override void SetRawDataCore(uint value)
        {
            ApplicationSpecificFaultFlags = Utilities.GetApplicationSpecificFaultFlags(value);
            _oemFaultCodes = Utilities.GetLowByte(value);
        }

        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.ASFflags;

    // IApplicationSpecificFaultFlags
    public bool FaultServiceRequest { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.ServiceRequest); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.ServiceRequest, value); }
    public bool FaultLockoutReset { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.LockoutReset); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.LockoutReset, value); }
    public bool FaultLowWaterPressure { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.LowWaterPress); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.LowWaterPress, value); }
    public bool FaultGasFlame { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.GasFlameFault); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.GasFlameFault, value); }
    public bool FaultAirPressure { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.AirPressFault); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.AirPressFault, value); }
    public bool FaultWaterOverTemperature { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.WaterOverTemp); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.WaterOverTemp, value); }
    public bool FaultReserved6 { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.Reserved6); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.Reserved6, value); }
    public bool FaultReserved7 { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.Reserved7); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.Reserved7, value); }
        /// <summary>
        /// OEM Fault Codes
        /// </summary>
        public byte OEMFaultCodes { get => _oemFaultCodes; set => _oemFaultCodes = value; }
    }
}
