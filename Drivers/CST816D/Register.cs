using TekuSP.Drivers.DriverBase.Interfaces;

namespace TekuSP.Drivers.CST816D
{
    /// <summary>
    /// Parsed CST816D touch data register snapshot.
    /// </summary>
    public class Register : ITouchData
    {
        /// <summary>Reserved byte(s) from the register snapshot.</summary>
        public ushort Reserve0 { get; set; }
        /// <summary>Gesture ID raw value.</summary>
        public byte Gesture { get; set; }
        /// <summary>Number of touch points.</summary>
        public ushort TouchPoints { get; set; }
        /// <summary>X high byte.</summary>
        public ushort XH { get; set; }
        /// <summary>X low byte.</summary>
        public ushort XL { get; set; }
        /// <summary>Y high byte.</summary>
        public ushort YH { get; set; }
        /// <summary>Y low byte.</summary>
        public ushort YL { get; set; }
        /// <summary>Pressure raw value.</summary>
        public ushort Pressure { get; set; }
        /// <summary>Miscellaneous status flags.</summary>
        public ushort Miscellaneous { get; set; }

        /// <summary>Decoded X coordinate.</summary>
        public int X { get; set; }
        /// <summary>Decoded Y coordinate.</summary>
        public int Y { get; set; }
        /// <summary>Decoded touch pressure.</summary>
        public int TouchPressure { get; set; }
    }
}
