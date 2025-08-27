using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Interfaces
{
    /// <summary>
    /// Backing property and convenience selectors for <see cref="Enums.SlaveStatus"/>.
    /// </summary>
    public interface ISlaveStatus
    {
        #region Public Properties

        /// <summary>Is <see cref="SlaveStatus.CoolingStatus"/> set?</summary>
        bool SlaveCoolingStatus { get; set; }

        /// <summary>Is <see cref="SlaveStatus.DHWMode"/> set?</summary>
        bool SlaveDHWMode { get; set; }

        /// <summary>Is <see cref="SlaveStatus.DiagnosticIndication"/> set?</summary>
        bool SlaveDiagnosticIndication { get; set; }

        /// <summary>Is <see cref="SlaveStatus.FaultIndication"/> set?</summary>
        bool SlaveFaultIndication { get; set; }

        /// <summary>Is <see cref="SlaveStatus.FlameStatus"/> set?</summary>
        bool SlaveFlameStatus { get; set; }

        /// <summary>Is <see cref="SlaveStatus.CH2Mode"/> set?</summary>
        bool SlaveCH2Mode { get; set; }

        /// <summary>Is <see cref="SlaveStatus.CHMode"/> set?</summary>
        bool SlaveCHMode { get; set; }

        /// <summary>Is <see cref="SlaveStatus.Reserved"/> set?</summary>
        bool SlaveReserved7 { get; set; }

        #endregion Public Properties
    }
}