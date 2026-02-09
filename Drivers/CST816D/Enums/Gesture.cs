namespace TekuSP.Drivers.CST816D
{
    /// <summary>
    /// CST816D gesture identifiers.
    /// </summary>
    public enum Gesture
    {
        /// <summary>No gesture detected.</summary>
        GEST_NONE = 0x00,
        /// <summary>Swipe up gesture.</summary>
        GEST_MOVE_UP = 0x01,
        /// <summary>Swipe down gesture.</summary>
        GEST_MOVE_DOWN = 0x02,
        /// <summary>Swipe right gesture.</summary>
        GEST_MOVE_RIGHT = 0x03,
        /// <summary>Swipe left gesture.</summary>
        GEST_MOVE_LEFT = 0x04,
        /// <summary>Single tap gesture.</summary>
        GEST_SINGLE_CLICK = 0x05,
        /// <summary>Long press gesture.</summary>
        GEST_LONG_PRESS = 0x0c
    }
}
