using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Interfaces
{
    /// <summary>
    /// Backing property and convenience selectors for <see cref="Enums.RemoteParameterTransferEnable"/>.
    /// </summary>
    public interface IRemoteParameterTransferEnable
    {
        #region Public Properties

        /// <summary>Is <see cref="RemoteParameterTransferEnable.DHWSetpoint"/> enabled?</summary>
        bool EnableDHWSetpoint { get; set; }

        /// <summary>Is <see cref="RemoteParameterTransferEnable.MaxCHSetpoint"/> enabled?</summary>
        bool EnableMaxCHSetpoint { get; set; }

        /// <summary>Is <see cref="RemoteParameterTransferEnable.Reserved2"/> set?</summary>
        bool EnableReserved2 { get; set; }

        /// <summary>Is <see cref="RemoteParameterTransferEnable.Reserved3"/> set?</summary>
        bool EnableReserved3 { get; set; }

        /// <summary>Is <see cref="RemoteParameterTransferEnable.Reserved4"/> set?</summary>
        bool EnableReserved4 { get; set; }

        /// <summary>Is <see cref="RemoteParameterTransferEnable.Reserved5"/> set?</summary>
        bool EnableReserved5 { get; set; }

        /// <summary>Is <see cref="RemoteParameterTransferEnable.Reserved6"/> set?</summary>
        bool EnableReserved6 { get; set; }

        /// <summary>Is <see cref="RemoteParameterTransferEnable.Reserved7"/> set?</summary>
        bool EnableReserved7 { get; set; }

        #endregion Public Properties
    }
}