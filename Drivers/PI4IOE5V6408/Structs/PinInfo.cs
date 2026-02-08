namespace TekuSP.Drivers.PI4IOE5V6408.Structs
{
    /// <summary>
    /// Simple struct to return a pin value and direction together.
    /// nanoFramework does not support tuples yet, so use this container.
    /// </summary>
    public struct PinInfo
    {
        /// <summary>
        /// Current pin logical level.
        /// </summary>
        public Enums.PinState Value
        {
            get; private set;
        }

        /// <summary>
        /// Current pin direction configuration.
        /// </summary>
        public Enums.PinDirection Direction
        {
            get; private set;
        }

        /// <summary>
        /// Constructs the simple struct for pininfo
        /// </summary>
        /// <param name="value">Current value of pin</param>
        /// <param name="direction">Direction of pin</param>
        public PinInfo(Enums.PinState value, Enums.PinDirection direction)
        {
            Value = value;
            Direction = direction;
        }
    }
}