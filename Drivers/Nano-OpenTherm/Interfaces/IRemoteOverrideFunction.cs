using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Interfaces
{
    /// <summary>
    /// Backing property and convenience selectors for <see cref="Enums.RemoteOverrideFunction"/>.
    /// </summary>
    public interface IRemoteOverrideFunction
    {
        #region Public Properties

        /// <summary>Is <see cref="Enums.RemoteOverrideFunction.ManualChangePriority"/> set?</summary>
        bool ManualChangePriority { get; set; }

        /// <summary>Is <see cref="Enums.RemoteOverrideFunction.ProgramChangePriority"/> set?</summary>
        bool ProgramChangePriority { get; set; }

        /// <summary>Is <see cref="Enums.RemoteOverrideFunction.Reserved2"/> set?</summary>
        bool RemoteOverrideReserved2 { get; set; }

        /// <summary>Is <see cref="Enums.RemoteOverrideFunction.Reserved3"/> set?</summary>
        bool RemoteOverrideReserved3 { get; set; }

        /// <summary>Is <see cref="Enums.RemoteOverrideFunction.Reserved4"/> set?</summary>
        bool RemoteOverrideReserved4 { get; set; }

        /// <summary>Is <see cref="Enums.RemoteOverrideFunction.Reserved5"/> set?</summary>
        bool RemoteOverrideReserved5 { get; set; }

        /// <summary>Is <see cref="Enums.RemoteOverrideFunction.Reserved6"/> set?</summary>
        bool RemoteOverrideReserved6 { get; set; }

        /// <summary>Is <see cref="Enums.RemoteOverrideFunction.Reserved7"/> set?</summary>
        bool RemoteOverrideReserved7 { get; set; }

        #endregion Public Properties

        #region Protected Properties

        /// <summary>
        /// Backing property for <see cref="RemoteOverrideFunction"/>.
        /// </summary>
        protected RemoteOverrideFunction RemoteOverrideFunction { get; set; }

        #endregion Protected Properties
    }
}