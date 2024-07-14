using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class ApplicationFaultCodesResponse : Response
    {
        public ApplicationFaultCodesResponse(Response baseResponse)
        {
            RawData = baseResponse.RawData;
            MessageType = baseResponse.MessageType;
            MessageID = baseResponse.MessageID;
            ApplicationSpecificFaultFlags = Utilities.GetApplicationSpecificFaultFlags(RawData);
        }
        private ApplicationSpecificFaultFlags ApplicationSpecificFaultFlags
        {
            get;
        }
        public override ulong RawData
        {
            get;
            set;
        }

        public override MessageType MessageType
        {
            get;
        }

        public override MessageID MessageID
        {
            get;
        }

        /// <summary>
        /// Service request.
        /// </summary>
        public bool ServiceRequest => (ApplicationSpecificFaultFlags & ApplicationSpecificFaultFlags.ServiceRequest) == ApplicationSpecificFaultFlags.ServiceRequest;
        /// <summary>
        /// Lockout-reset.
        /// </summary>
        public bool LockoutReset => (ApplicationSpecificFaultFlags & ApplicationSpecificFaultFlags.LockoutReset) == ApplicationSpecificFaultFlags.LockoutReset;
        /// <summary>
        /// Low water pressure.
        /// </summary>
        public bool LowWaterPress => (ApplicationSpecificFaultFlags & ApplicationSpecificFaultFlags.LowWaterPress) == ApplicationSpecificFaultFlags.LowWaterPress;
        /// <summary>
        /// Gas/flame fault.
        /// </summary>
        public bool GasFlameFault => (ApplicationSpecificFaultFlags & ApplicationSpecificFaultFlags.GasFlameFault) == ApplicationSpecificFaultFlags.GasFlameFault;
        /// <summary>
        /// Air pressure fault.
        /// </summary>
        public bool AirPressFault => (ApplicationSpecificFaultFlags & ApplicationSpecificFaultFlags.AirPressFault) == ApplicationSpecificFaultFlags.AirPressFault;
        /// <summary>
        /// Water over-temperature.
        /// </summary>
        public bool WaterOverTemp => (ApplicationSpecificFaultFlags & ApplicationSpecificFaultFlags.WaterOverTemp) == ApplicationSpecificFaultFlags.WaterOverTemp;
        /// <summary>
        /// OEM Fault Codes
        /// </summary>
        public byte OEMFaultCodes => Utilities.GetHighByte(RawData);
    }
}
