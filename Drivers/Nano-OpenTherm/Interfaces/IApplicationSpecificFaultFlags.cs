using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Interfaces
{
    /// <summary>
    /// Backing property and convenience selectors for <see cref="Enums.ApplicationSpecificFaultFlags"/>.
    /// </summary>
    public interface IApplicationSpecificFaultFlags
    {
        #region Public Properties

        /// <summary>
        /// Is <see cref="ApplicationSpecificFaultFlags.AirPressFault"/> set?
        /// </summary>
        bool FaultAirPressure { get; set; }

        /// <summary>
        /// Is <see cref="ApplicationSpecificFaultFlags.GasFlameFault"/> set?
        /// </summary>
        bool FaultGasFlame { get; set; }

        /// <summary>
        /// Is <see cref="ApplicationSpecificFaultFlags.LockoutReset"/> set?
        /// </summary>
        bool FaultLockoutReset { get; set; }

        /// <summary>
        /// Is <see cref="ApplicationSpecificFaultFlags.LowWaterPress"/> set?
        /// </summary>
        bool FaultLowWaterPressure { get; set; }

        /// <summary>
        /// Is <see cref="ApplicationSpecificFaultFlags.Reserved6"/> set?
        /// </summary>
        bool FaultReserved6 { get; set; }

        /// <summary>
        /// Is <see cref="ApplicationSpecificFaultFlags.Reserved7"/> set?
        /// </summary>
        bool FaultReserved7 { get; set; }

        /// <summary>
        /// Is <see cref="ApplicationSpecificFaultFlags.ServiceRequest"/> set?
        /// </summary>
        bool FaultServiceRequest { get; set; }

        /// <summary>
        /// Is <see cref="ApplicationSpecificFaultFlags.WaterOverTemp"/> set?
        /// </summary>
        bool FaultWaterOverTemperature { get; set; }

        #endregion Public Properties

        #region Protected Properties

        /// <summary>
        /// Backing property for <see cref="ApplicationSpecificFaultFlags"/>.
        /// </summary>
        protected ApplicationSpecificFaultFlags ApplicationSpecificFaultFlags { get; set; }

        #endregion Protected Properties
    }
}