namespace TekuSP.Drivers.DriverBase.Helpers
{
    /// <summary>
    /// Bit and word helper extensions.
    /// </summary>
    public static class BitHelper
    {
        #region Public Methods

        /// <summary>
        /// Gets a single bit from a byte.
        /// </summary>
        /// <param name="b">Source byte.</param>
        /// <param name="bitNumber">1-based bit position.</param>
        /// <returns>True if the bit is set.</returns>
        public static bool GetBit(this byte b, int bitNumber)
        {
            return (b & (1 << bitNumber - 1)) != 0;
        }
        public static bool GetBit(this uint b, int bitNumber)
        {
            return (b & (1 << bitNumber - 1)) != 0;
        }
        public static bool GetBit(this ulong b, int bitNumber)
        {
            return (b & ((ulong)1 << bitNumber - 1)) != 0;
        }

        /// <summary>Gets the high 16-bit word of a 32-bit value.</summary>
        /// <param name="number">Source value.</param>
        /// <returns>High word masked in upper 16 bits.</returns>
        public static uint HighWord(this uint number) => number & 0xFFFF0000;

        /// <summary>Sets the high 16-bit word of a 32-bit value.</summary>
        /// <param name="number">Source value.</param>
        /// <param name="newValue">New high word.</param>
        /// <returns>Updated value.</returns>
        public static uint HighWord(this uint number, uint newValue) => (number & 0x0000FFFF) + (newValue << 16);

        /// <summary>Gets the low 16-bit word of a 32-bit value.</summary>
        /// <param name="number">Source value.</param>
        /// <returns>Low word.</returns>
        public static uint LowWord(this uint number) => number & 0x0000FFFF;

        /// <summary>Sets the low 16-bit word of a 32-bit value.</summary>
        /// <param name="number">Source value.</param>
        /// <param name="newValue">New low word.</param>
        /// <returns>Updated value.</returns>
        public static uint LowWord(this uint number, uint newValue) => (number & 0xFFFF0000) + (newValue & 0x0000FFFF);

        /// <summary>
        /// Sets or clears a bit in a byte.
        /// </summary>
        /// <param name="b">Source byte.</param>
        /// <param name="pos">Zero-based bit position.</param>
        /// <param name="value">True to set, false to clear.</param>
        /// <returns>Updated byte.</returns>
        public static byte SetBit(this byte b, int pos, bool value)
        {
            if (value)
                return (byte)(b | (1 << pos));
            return (byte)(b & ~(1 << pos));
        }

        #endregion Public Methods
    }
}