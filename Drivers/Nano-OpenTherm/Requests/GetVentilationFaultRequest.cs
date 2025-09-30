using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using TekuSP.Drivers.Nano_OpenTherm.Interfaces;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads fault flags and OEM fault code from the ventilation/heat-recovery subsystem.
    /// </summary>
    public class GetVentilationFaultRequest : ReadRequest, IApplicationSpecificFaultFlags
    {
        #region Public Constructors

        public GetVentilationFaultRequest() : base()
        {
        }

        public GetVentilationFaultRequest(Request baseReq) : base(baseReq)
        {
        }

        /// <summary>
        /// Convenience constructor to initialize application-specific fault flags (LB) and OEM fault code (HB).
        /// </summary>
        public GetVentilationFaultRequest(ApplicationSpecificFaultFlags flags, byte oemFaultCode)
        {
            ApplicationSpecificFaultFlags = flags;
            OEMFaultCode = oemFaultCode;
        }

        #endregion Public Constructors

        #region Public Properties

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.VentilationApplicationFaultCodesResponse);
        public bool FaultAirPressure { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.AirPressFault); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.AirPressFault, value); }
        public bool FaultGasFlame { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.GasFlameFault); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.GasFlameFault, value); }
        public bool FaultLockoutReset { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.LockoutReset); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.LockoutReset, value); }
        public bool FaultLowWaterPressure { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.LowWaterPress); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.LowWaterPress, value); }
        public bool FaultReserved6 { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.Reserved6); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.Reserved6, value); }
        public bool FaultReserved7 { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.Reserved7); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.Reserved7, value); }

        // IApplicationSpecificFaultFlags
        public bool FaultServiceRequest { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.ServiceRequest); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.ServiceRequest, value); }

        public bool FaultWaterOverTemperature { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.WaterOverTemp); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.WaterOverTemp, value); }
        public override MessageID MessageID => MessageID.ASFflagsOEMfaultCodeVentilationHeatRecovery;
        public override MessageType MessageType => MessageType.READ_DATA;

        /// <summary>
        /// OEM-specific fault/diagnostic code reported by ventilation/heat-recovery (high byte).
        /// </summary>
        public byte OEMFaultCode { get; set; }

        #endregion Public Properties

        #region Protected Properties

        protected ApplicationSpecificFaultFlags ApplicationSpecificFaultFlags { get; set; }

        #endregion Protected Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
        {
            // Pack OEM fault code (high byte) and application-specific flags (low byte) using Utilities
            byte low = Utilities.SetApplicationSpecificFaultFlags(ApplicationSpecificFaultFlags);
            ushort payload = Utilities.MakeUShort(OEMFaultCode, low);
            return ProcessRequest(payload);
        }

        protected override void SetRawDataCore(uint value)
        {
            ApplicationSpecificFaultFlags = Utilities.GetApplicationSpecificFaultFlags(value);
            OEMFaultCode = Utilities.GetHighByte(value);
        }

        #endregion Protected Methods
    }
}