using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using TekuSP.Drivers.Nano_OpenTherm.Interfaces;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Solar Storage application fault flags and OEM fault code (high=flags, low=OEM code).</summary>
    public class SolarStorageApplicationFaultCodesResponse : Response, IApplicationSpecificFaultFlags
    {
        #region Public Constructors

        public SolarStorageApplicationFaultCodesResponse(MessageType mt = MessageType.READ_ACK)
        { MessageType = mt; }

        public SolarStorageApplicationFaultCodesResponse(Response r) : base(r)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public bool FaultAirPressure { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.AirPressFault); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.AirPressFault, value); }
        public bool FaultGasFlame { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.GasFlameFault); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.GasFlameFault, value); }
        public bool FaultLockoutReset { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.LockoutReset); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.LockoutReset, value); }
        public bool FaultLowWaterPressure { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.LowWaterPress); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.LowWaterPress, value); }
        public bool FaultReserved6 { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.Reserved6); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.Reserved6, value); }
        public bool FaultReserved7 { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.Reserved7); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.Reserved7, value); }

        // Convenience bits
        public bool FaultServiceRequest { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.ServiceRequest); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.ServiceRequest, value); }

        public bool FaultWaterOverTemperature { get => ApplicationSpecificFaultFlags.IsSet(ApplicationSpecificFaultFlags.WaterOverTemp); set => ApplicationSpecificFaultFlags = ApplicationSpecificFaultFlags.SetFlag(ApplicationSpecificFaultFlags.WaterOverTemp, value); }
        public override MessageID MessageID => MessageID.ASFflagsOEMfaultCodeSolarStorage;
        public override MessageType MessageType { get; set; }
        public byte OEMFaultCodes { get; set; }

        #endregion Public Properties

        #region Protected Properties

        protected ApplicationSpecificFaultFlags ApplicationSpecificFaultFlags { get; set; }

        #endregion Protected Properties

        #region Protected Methods

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

        #endregion Protected Methods
    }
}