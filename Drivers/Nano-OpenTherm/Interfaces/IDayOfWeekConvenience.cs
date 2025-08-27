using System;

namespace TekuSP.Drivers.Nano_OpenTherm.Interfaces
{
    /// <summary>
    /// Convenience selectors for <see cref="System.DayOfWeek"/>.
    /// </summary>
    public interface IDayOfWeekConvenience
    {
        #region Public Properties
        /// <summary>Is <see cref="DayOfWeek.Monday"/>?</summary>
        bool IsMonday { get; set; }
        /// <summary>Is <see cref="DayOfWeek.Tuesday"/>?</summary>
        bool IsTuesday { get; set; }
        /// <summary>Is <see cref="DayOfWeek.Wednesday"/>?</summary>
        bool IsWednesday { get; set; }
        /// <summary>Is <see cref="DayOfWeek.Thursday"/>?</summary>
        bool IsThursday { get; set; }
        /// <summary>Is <see cref="DayOfWeek.Friday"/>?</summary>
        bool IsFriday { get; set; }
        /// <summary>Is <see cref="DayOfWeek.Saturday"/>?</summary>
        bool IsSaturday { get; set; }
        /// <summary>Is <see cref="DayOfWeek.Sunday"/>?</summary>
        bool IsSunday { get; set; }

        #endregion Public Properties
    }
}
