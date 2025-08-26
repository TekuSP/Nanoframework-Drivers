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
            get => _flags.IsSet(ApplicationSpecificFaultFlags.ServiceRequest);
            set => _flags = _flags.SetFlag(ApplicationSpecificFaultFlags.ServiceRequest, value);
        }
        /// <summary>
        /// Lockout-reset.
        /// </summary>
        public bool LockoutReset
        {
            get => _flags.IsSet(ApplicationSpecificFaultFlags.LockoutReset);
            set => _flags = _flags.SetFlag(ApplicationSpecificFaultFlags.LockoutReset, value);
        }
        /// <summary>
        /// Low water pressure.
        /// </summary>
        public bool LowWaterPress
        {
            get => _flags.IsSet(ApplicationSpecificFaultFlags.LowWaterPress);
            set => _flags = _flags.SetFlag(ApplicationSpecificFaultFlags.LowWaterPress, value);
        }
        /// <summary>
        /// Gas/flame fault.
        /// </summary>
        public bool GasFlameFault
        {
            get => _flags.IsSet(ApplicationSpecificFaultFlags.GasFlameFault);
            set => _flags = _flags.SetFlag(ApplicationSpecificFaultFlags.GasFlameFault, value);
        }
        /// <summary>
        /// Air pressure fault.
        /// </summary>
        public bool AirPressFault
        {
            get => _flags.IsSet(ApplicationSpecificFaultFlags.AirPressFault);
            set => _flags = _flags.SetFlag(ApplicationSpecificFaultFlags.AirPressFault, value);
        }
        /// <summary>
        /// Water over-temperature.
        /// </summary>
        public bool WaterOverTemp
        {
            get => _flags.IsSet(ApplicationSpecificFaultFlags.WaterOverTemp);
            set => _flags = _flags.SetFlag(ApplicationSpecificFaultFlags.WaterOverTemp, value);
        }
        /// <summary>
        /// OEM Fault Codes
        /// </summary>
        public byte OEMFaultCodes { get => _oemFaultCodes; set => _oemFaultCodes = value; }
    }
}
