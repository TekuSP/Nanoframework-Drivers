using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Interfaces
{
    /// <summary>
    /// Backing property and convenience selectors for <see cref="Enums.MasterConfiguration"/>.
    /// </summary>
    public interface IMasterConfiguration
    {
        #region Public Properties

        /// <summary>Is <see cref="MasterConfiguration.Reserved0"/> set?</summary>
        bool MasterConfigReserved0 { get; set; }

        /// <summary>Is <see cref="MasterConfiguration.Reserved1"/> set?</summary>
        bool MasterConfigReserved1 { get; set; }

        /// <summary>Is <see cref="MasterConfiguration.Reserved2"/> set?</summary>
        bool MasterConfigReserved2 { get; set; }

        /// <summary>Is <see cref="MasterConfiguration.Reserved3"/> set?</summary>
        bool MasterConfigReserved3 { get; set; }

        /// <summary>Is <see cref="MasterConfiguration.Reserved4"/> set?</summary>
        bool MasterConfigReserved4 { get; set; }

        /// <summary>Is <see cref="MasterConfiguration.Reserved5"/> set?</summary>
        bool MasterConfigReserved5 { get; set; }

        /// <summary>Is <see cref="MasterConfiguration.Reserved6"/> set?</summary>
        bool MasterConfigReserved6 { get; set; }

        /// <summary>Is <see cref="MasterConfiguration.Reserved7"/> set?</summary>
        bool MasterConfigReserved7 { get; set; }

        #endregion Public Properties
    }
}