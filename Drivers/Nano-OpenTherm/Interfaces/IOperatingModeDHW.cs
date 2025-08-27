using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Interfaces
{
    /// <summary>
    /// Operating mode selectors for Domestic Hot Water (DHW) for <see cref="Enums.OperatingMode"/>.
    /// </summary>
    public interface IOperatingModeDHW
    {
        #region Public Properties

        /// <summary>
        /// Is <see cref="OperatingMode"/> set to <see cref="OperatingMode.Auto"/>?
        /// </summary>
        bool DHWModeIsAuto { get; set; }

        /// <summary>
        /// Is <see cref="OperatingMode"/> set to <see cref="OperatingMode.Manual"/>?
        /// </summary>
        bool DHWModeIsManual { get; set; }

        /// <summary>
        /// Is <see cref="OperatingMode"/> set to <see cref="OperatingMode.Off"/>?
        /// </summary>
        bool DHWModeIsOff { get; set; }

        /// <summary>
        /// Is <see cref="OperatingMode"/> set to <see cref="OperatingMode.Reserved"/>?
        /// </summary>
        bool DHWModeIsReserved { get; set; }

        #endregion Public Properties
    }
}