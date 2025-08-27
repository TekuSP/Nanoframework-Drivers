using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Interfaces
{
    /// <summary>
    /// Backing property and convenience selectors for <see cref="Enums.MasterStatus"/>.
    /// </summary>
    public interface IMasterStatus
    {
        #region Public Properties

        /// <summary>Is <see cref="MasterStatus.CH2Enabled"/> set?</summary>
        bool MasterIsCentralHeating2Active { get; set; }

        /// <summary>Is <see cref="MasterStatus.CHEnabled"/> set?</summary>
        bool MasterIsCentralHeatingActive { get; set; }

        /// <summary>Is <see cref="MasterStatus.CoolingEnabled"/> set?</summary>
        bool MasterIsCoolingActive { get; set; }

        /// <summary>Is <see cref="MasterStatus.DHWEnabled"/> set?</summary>
        bool MasterIsHotWaterActive { get; set; }

        /// <summary>Is <see cref="MasterStatus.OTCActive"/> set?</summary>
        bool MasterOTCActive { get; set; }

        /// <summary>Is <see cref="MasterStatus.Reserved5"/> set?</summary>
        bool MasterReserved5 { get; set; }

        /// <summary>Is <see cref="MasterStatus.Reserved6"/> set?</summary>
        bool MasterReserved6 { get; set; }

        /// <summary>Is <see cref="MasterStatus.Reserved7"/> set?</summary>
        bool MasterReserved7 { get; set; }

        #endregion Public Properties
    }
}