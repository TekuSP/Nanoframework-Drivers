using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Interfaces
{
    /// <summary>
    /// Backing property and convenience selectors for <see cref="Enums.SlaveConfiguration"/>.
    /// </summary>
    public interface ISlaveConfiguration
    {
        #region Public Properties

        /// <summary>Is <see cref="SlaveConfiguration.ControlType"/> set?</summary>
        bool SlaveConfigControlType { get; set; }

        /// <summary>Is <see cref="SlaveConfiguration.CoolingConfig"/> set?</summary>
        bool SlaveConfigCooling { get; set; }

        /// <summary>Is <see cref="SlaveConfiguration.DHWConfig"/> set?</summary>
        bool SlaveConfigDHWConfig { get; set; }

        /// <summary>Is <see cref="SlaveConfiguration.DHWPresent"/> set?</summary>
        bool SlaveConfigDHWPresent { get; set; }

        /// <summary>Is <see cref="SlaveConfiguration.CH2Present"/> set?</summary>
        bool SlaveConfigCH2Present { get; set; }

        /// <summary>Is <see cref="SlaveConfiguration.MasterLowOffPumpControl"/> set?</summary>
        bool SlaveConfigMasterLowOffPumpControl { get; set; }

        /// <summary>Is <see cref="SlaveConfiguration.Reserved6"/> set?</summary>
        bool SlaveConfigReserved6 { get; set; }

        /// <summary>Is <see cref="SlaveConfiguration.Reserved7"/> set?</summary>
        bool SlaveConfigReserved7 { get; set; }

        #endregion Public Properties

        #region Protected Properties

        /// <summary>
        /// Backing property for <see cref="SlaveConfiguration"/>.
        /// </summary>
        protected SlaveConfiguration SlaveConfiguration { get; set; }

        #endregion Protected Properties
    }
}