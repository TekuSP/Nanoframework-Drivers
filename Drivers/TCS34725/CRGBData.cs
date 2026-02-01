using TekuSP.Drivers.DriverBase.Interfaces;

namespace TekuSP.Drivers.TCS34725
{
    /// <summary>
    /// Clear/Red/Green/Blue channel data container.
    /// </summary>
    public class CRGBData : IColorData
    {
        /// <summary>Clear channel value.</summary>
        public float C {  get; set; }
        /// <summary>Red channel value.</summary>
        public float R { get; set; }
        /// <summary>Green channel value.</summary>
        public float G { get; set; }
        /// <summary>Blue channel value.</summary>
        public float B { get; set; }
    }
}
