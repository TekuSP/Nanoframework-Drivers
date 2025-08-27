using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Interfaces
{
    /// <summary>
    /// Operating mode selectors for Heating Circuit 2 (HC2) for <see cref="Enums.OperatingMode"/>.
    /// </summary>
    public interface IOperatingModeHC2
    {
        #region Public Properties

        /// <summary>
        /// Is <see cref="OperatingMode"/> set to <see cref="OperatingMode.Auto"/>?
        /// </summary>
        bool HC2ModeIsAuto { get; set; }

        /// <summary>
        /// Is <see cref="OperatingMode"/> set to <see cref="OperatingMode.Manual"/>?
        /// </summary>
        bool HC2ModeIsManual { get; set; }

        /// <summary>
        /// Is <see cref="OperatingMode"/> set to <see cref="OperatingMode.Off"/>?
        /// </summary>
        bool HC2ModeIsOff { get; set; }

        /// <summary>
        /// Is <see cref="OperatingMode"/> set to <see cref="OperatingMode.Reserved"/>?
        /// </summary>
        bool HC2ModeIsReserved { get; set; }

        #endregion Public Properties
    }
}