using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Interfaces
{
    /// <summary>
    /// Backing property and convenience selectors for <see cref="Enums.OperatingMode"/>.
    /// </summary>
    public interface IOperatingMode
    {
        #region Protected Properties

        /// <summary>
        /// Is <see cref="OperatingMode"/> set to <see cref="OperatingMode.Auto"/>?
        /// </summary>
        protected bool ModeIsAuto { get; set; }

        /// <summary>
        /// Is <see cref="OperatingMode"/> set to <see cref="OperatingMode.Manual"/>?
        /// </summary>
        protected bool ModeIsManual { get; set; }

        /// <summary>
        /// Is <see cref="OperatingMode"/> set to <see cref="OperatingMode.Off"/>?
        /// </summary>
        protected bool ModeIsOff { get; set; }

        /// <summary>
        /// Is <see cref="OperatingMode"/> set to <see cref="OperatingMode.Reserved"/>?
        /// </summary>
        protected bool ModeIsReserved { get; set; }

        /// <summary>
        /// Backing property for Operating Mode.
        /// </summary>
        protected OperatingMode OperatingMode { get; set; }

        #endregion Protected Properties
    }
}