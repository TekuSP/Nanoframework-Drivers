using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Interfaces
{
    /// <summary>
    /// Backing property and convenience selectors for <see cref="Enums.VersionProductType"/>.
    /// </summary>
    public interface IVersionProductType
    {
        #region Public Properties

        /// <summary>Is <see cref="VersionProductType.Boiler"/>?</summary>
        bool IsBoiler { get; set; }

        /// <summary>Is <see cref="VersionProductType.Controller"/>?</summary>
        bool IsController { get; set; }

        /// <summary>Is <see cref="VersionProductType.HeatPump"/>?</summary>
        bool IsHeatPump { get; set; }

        /// <summary>Is <see cref="VersionProductType.Sensor"/>?</summary>
        bool IsSensor { get; set; }

        /// <summary>Is <see cref="VersionProductType.Unknown"/>?</summary>
        bool IsUnknown { get; set; }

        /// <summary>Is <see cref="VersionProductType.Ventilation"/>?</summary>
        bool IsVentilation { get; set; }

        #endregion Public Properties
    }
}