namespace DriverBase.Interfaces
{
    /// <summary>
    /// Color data
    /// </summary>
    public interface IColorData
    {
        /// <summary>
        /// Clear channel value
        /// </summary>
        public float C { get; }
        /// <summary>
        /// Red channel value
        /// </summary>
        public float R { get; }
        /// <summary>
        /// Green channel value
        /// </summary>
        public float G { get; }
        /// <summary>
        /// Blue channel value
        /// </summary>
        public float B { get; }
    }
}
