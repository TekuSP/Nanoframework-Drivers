using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Interfaces
{
    /// <summary>
    /// Backing property and convenience selectors for <see cref="Enums.Month"/>.
    /// </summary>
    public interface IMonthConvenience
    {
        #region Public Properties

        /// <summary>Is <see cref="Month.April"/>?</summary>
        bool IsApril { get; set; }

        /// <summary>Is <see cref="Month.August"/>?</summary>
        bool IsAugust { get; set; }

        /// <summary>Is <see cref="Month.December"/>?</summary>
        bool IsDecember { get; set; }

        /// <summary>Is <see cref="Month.February"/>?</summary>
        bool IsFebruary { get; set; }

        /// <summary>Is <see cref="Month.January"/>?</summary>
        bool IsJanuary { get; set; }

        /// <summary>Is <see cref="Month.July"/>?</summary>
        bool IsJuly { get; set; }

        /// <summary>Is <see cref="Month.June"/>?</summary>
        bool IsJune { get; set; }

        /// <summary>Is <see cref="Month.March"/>?</summary>
        bool IsMarch { get; set; }

        /// <summary>Is <see cref="Month.May"/>?</summary>
        bool IsMay { get; set; }

        /// <summary>Is <see cref="Month.November"/>?</summary>
        bool IsNovember { get; set; }

        /// <summary>Is <see cref="Month.October"/>?</summary>
        bool IsOctober { get; set; }

        /// <summary>Is <see cref="Month.September"/>?</summary>
        bool IsSeptember { get; set; }

        #endregion Public Properties
    }
}