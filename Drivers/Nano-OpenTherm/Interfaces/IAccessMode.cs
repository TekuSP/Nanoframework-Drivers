using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Interfaces
{
    /// <summary>
    /// Backing property and convenience selectors for <see cref="Enums.AccessMode"/>.
    /// </summary>
    public interface IAccessMode
    {
        #region Public Properties

        /// <summary>
        /// Is <see cref="AccessMode.Read"/> set?
        /// </summary>
        bool CanRead { get; set; }

        /// <summary>
        /// Is <see cref="AccessMode.Write"/> set?
        /// </summary>
        bool CanWrite { get; set; }

        #endregion Public Properties
    }
}