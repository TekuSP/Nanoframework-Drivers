using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Interfaces
{
    /// <summary>
    /// Backing property and convenience selectors for <see cref="Enums.RemoteParameterTransferReadWrite"/>.
    /// </summary>
    public interface IRemoteParameterTransferReadWrite
    {
        #region Public Properties

        /// <summary>Is <see cref="RemoteParameterTransferReadWrite.DHWSetpoint"/> writable?</summary>
        bool RWDHWSetpoint { get; set; }

        /// <summary>Is <see cref="RemoteParameterTransferReadWrite.MaxCHSetpoint"/> writable?</summary>
        bool RWMaxCHSetpoint { get; set; }

        /// <summary>Is <see cref="RemoteParameterTransferReadWrite.Reserved2"/> set?</summary>
        bool RWReserved2 { get; set; }

        /// <summary>Is <see cref="RemoteParameterTransferReadWrite.Reserved3"/> set?</summary>
        bool RWReserved3 { get; set; }

        /// <summary>Is <see cref="RemoteParameterTransferReadWrite.Reserved4"/> set?</summary>
        bool RWReserved4 { get; set; }

        /// <summary>Is <see cref="RemoteParameterTransferReadWrite.Reserved5"/> set?</summary>
        bool RWReserved5 { get; set; }

        /// <summary>Is <see cref="RemoteParameterTransferReadWrite.Reserved6"/> set?</summary>
        bool RWReserved6 { get; set; }

        /// <summary>Is <see cref="RemoteParameterTransferReadWrite.Reserved7"/> set?</summary>
        bool RWReserved7 { get; set; }

        #endregion Public Properties

        #region Protected Properties

        /// <summary>
        /// Backing property for <see cref="RemoteParameterTransferReadWrite"/>.
        /// </summary>
        protected RemoteParameterTransferReadWrite RemoteParameterTransferReadWrite { get; set; }

        #endregion Protected Properties
    }
}