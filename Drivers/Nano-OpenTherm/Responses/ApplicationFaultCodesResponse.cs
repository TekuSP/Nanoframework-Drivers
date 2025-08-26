using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class ApplicationFaultCodesResponse : Response
    {
        private ApplicationSpecificFaultFlags _flags;
        private byte _oemFaultCodes;

        /// <summary>
        /// Initializes a new response for constructing a frame to send.
        /// </summary>
        /// <param name="messageType">Optional message type to use. Defaults to <see cref="TekuSP.Drivers.Nano_OpenTherm.Enums.MessageType.READ_ACK"/>.</param>
        public ApplicationFaultCodesResponse(MessageType messageType = MessageType.READ_ACK)
        {
            MessageType = messageType;
        }

        public ApplicationFaultCodesResponse(Response baseResponse)
        {
            MessageType = baseResponse.MessageType;
            SetRawDataCore(baseResponse.RawData);
        }

        protected override uint GetRawDataCore()
        {
            // Low byte = OEM fault code, High byte = flags
            var high = Utilities.SetApplicationSpecificFaultFlags(_flags);
            ushort payload = Utilities.MakeUShort(high, _oemFaultCodes);
            return ProcessResponse(payload);
        }
        protected override void SetRawDataCore(uint value)
        {
            _flags = Utilities.GetApplicationSpecificFaultFlags(value);
            _oemFaultCodes = Utilities.GetLowByte(value);
        }

    public override MessageType MessageType { get; set; }
    public override MessageID MessageID => MessageID.ASFflags;

        /// <summary>
        /// Service request.
        /// </summary>
    public bool ServiceRequest
        {
            get => Utilities.IsSet(_flags, ApplicationSpecificFaultFlags.ServiceRequest);
            set => Utilities.SetFlag(ref _flags, ApplicationSpecificFaultFlags.ServiceRequest, value);
        }
        /// <summary>
        /// Lockout-reset.
        /// </summary>
    public bool LockoutReset
        {
            get => Utilities.IsSet(_flags, ApplicationSpecificFaultFlags.LockoutReset);
            set => Utilities.SetFlag(ref _flags, ApplicationSpecificFaultFlags.LockoutReset, value);
        }
        /// <summary>
        /// Low water pressure.
        /// </summary>
    public bool LowWaterPress
        {
            get => Utilities.IsSet(_flags, ApplicationSpecificFaultFlags.LowWaterPress);
            set => Utilities.SetFlag(ref _flags, ApplicationSpecificFaultFlags.LowWaterPress, value);
        }
        /// <summary>
        /// Gas/flame fault.
        /// </summary>
    public bool GasFlameFault
        {
            get => Utilities.IsSet(_flags, ApplicationSpecificFaultFlags.GasFlameFault);
            set => Utilities.SetFlag(ref _flags, ApplicationSpecificFaultFlags.GasFlameFault, value);
        }
        /// <summary>
        /// Air pressure fault.
        /// </summary>
    public bool AirPressFault
        {
            get => Utilities.IsSet(_flags, ApplicationSpecificFaultFlags.AirPressFault);
            set => Utilities.SetFlag(ref _flags, ApplicationSpecificFaultFlags.AirPressFault, value);
        }
        /// <summary>
        /// Water over-temperature.
        /// </summary>
    public bool WaterOverTemp
        {
            get => Utilities.IsSet(_flags, ApplicationSpecificFaultFlags.WaterOverTemp);
            set => Utilities.SetFlag(ref _flags, ApplicationSpecificFaultFlags.WaterOverTemp, value);
        }
        /// <summary>
        /// OEM Fault Codes
        /// </summary>
    public byte OEMFaultCodes { get => _oemFaultCodes; set => _oemFaultCodes = value; }
    }
}
