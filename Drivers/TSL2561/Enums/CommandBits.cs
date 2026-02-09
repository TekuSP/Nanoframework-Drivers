using System;

namespace TekuSP.Drivers.TSL2561
{
    /// <summary>
    /// Command flag bits for TSL2561 register access.
    /// </summary>
    [Flags]
    public enum CommandBits : byte
    {
        /// <summary>Command bit to access registers.</summary>
        Command = 0x80,
        /// <summary>Clear interrupt bit.</summary>
        Clear = 0x40,
        /// <summary>Word (16-bit) read bit.</summary>
        Word = 0x20,
        /// <summary>Block read bit.</summary>
        Block = 0x10
    }

}
