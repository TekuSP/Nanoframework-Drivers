namespace TekuSP.Drivers.PI4IOE5V6408.Enums
{
    /// <summary>
    /// PI4IOE5V6408 register addresses (registers are at odd offsets: 0x01..0x13).
    /// </summary>
    public enum PI4IOE5V6408Register : byte
    {
        /// <summary>
        /// Device ID and reset register. Bits 7..5 contain device ID (0b101), bit 1 is reset notification flag, bit 0 is reset command write.
        /// </summary>
        DeviceIdReset = 0x01,

        /// <summary>
        /// I/O direction register. 1 = input, 0 = output for each bit.
        /// </summary>
        IoDirection = 0x03,

        /// <summary>
        /// Output state register (output latch values).
        /// </summary>
        OutputState = 0x05,

        /// <summary>
        /// Output impedance register. 1 = high impedance, 0 = driven.
        /// </summary>
        OutputImpedence = 0x07,

        /// <summary>
        /// Input default state register. Used as 'normal' state for interrupt comparison.
        /// </summary>
        InputDefaultState = 0x09,

        /// <summary>
        /// Input pull-up/pull-down enable register. 1 = enabled, 0 = disabled.
        /// </summary>
        InputPullupdownEnable = 0x0B,

        /// <summary>
        /// Input pull-up/pull-down selection register. 1 = pull-up, 0 = pull-down.
        /// </summary>
        InputPullupPullDown = 0x0D,

        /// <summary>
        /// Input status register (read current pin values).
        /// </summary>
        InputStatus = 0x0F,

        /// <summary>
        /// Interrupt mask register. 0 = enabled, 1 = masked/disabled (per datasheet semantics).
        /// </summary>
        InterruptMask = 0x11,

        /// <summary>
        /// Interrupt status register (set when a triggered interrupt occurs). Reading clears flags.
        /// </summary>
        InterruptStatus = 0x13
    }
}