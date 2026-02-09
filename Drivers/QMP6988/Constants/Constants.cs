using System;

namespace TekuSP.Drivers.QMP6988.Constants
{
    /// <summary>
    /// Common constants used by the QMP6988 driver. These replace magic numbers and make the code
    /// more readable and maintainable.
    /// </summary>
    public static class Qmp6988Constants
    {
        /// <summary>
        /// Soft reset value written to the <c>RESET</c> register to trigger a software reset.
        /// </summary>
        public const byte ResetValue = 0xE6;

        /// <summary>
        /// Expected CHIP ID value read from the <c>CHIP_ID</c> register.
        /// </summary>
        public const byte ExpectedChipId = 0x5C;

        /// <summary>
        /// Mask covering the <c>power_mode</c> bits (bit[1:0]) in <c>CTRL_MEAS</c>.
        /// </summary>
        public const byte PowerModeMask = 0x03;

        /// <summary>
        /// Mask for the <c>measure</c> bit (bit3) in <c>DEVICE_STAT</c>.
        /// </summary>
        public const byte DeviceStatMeasureMask = 0x08;

        /// <summary>
        /// Shift for the standby selection in <c>IO_SETUP</c> (t_standby bits start at bit 5).
        /// </summary>
        public const int IoSetupStandbyShift = 5;

        /// <summary>
        /// Bit mask for the standby field (bits 7..5) in <c>IO_SETUP</c>.
        /// </summary>
        public const byte IoSetupStandbyMask = 0xE0;

        /// <summary>
        /// Shift for temperature averaging in <c>CTRL_MEAS</c> (bits 7..5).
        /// </summary>
        public const int CtrlTempShift = 5;

        /// <summary>
        /// Shift for pressure averaging in <c>CTRL_MEAS</c> (bits 4..2).
        /// </summary>
        public const int CtrlPressShift = 2;

        /// <summary>
        /// Mask for temperature averaging field (bits 7..5) in <c>CTRL_MEAS</c>.
        /// </summary>
        public const byte CtrlTempMask = 0xE0;

        /// <summary>
        /// Mask for pressure averaging field (bits 4..2) in <c>CTRL_MEAS</c>.
        /// </summary>
        public const byte CtrlPressMask = 0x1C;

        /// <summary>
        /// Mask for the coefficient "extension" nibble stored in the shared COE register.
        /// Used when assembling 20-bit coefficients a0 and b00.
        /// </summary>
        public const byte CoeExtMask = 0x0F;

        /// <summary>
        /// Shift amount of the coefficient extension nibble contained in COE_b00_a0_ex.
        /// </summary>
        public const int CoeExtShift = 4;

        /// <summary>
        /// Default temperature averaging selection written at startup (value encoding per datasheet).
        /// 1 => 001 => 1 sample.
        /// </summary>
        public const byte DefaultTempAveraging = 1;

        /// <summary>
        /// Default pressure averaging selection written at startup (value encoding per datasheet).
        /// 3 => 011 => 4 samples.
        /// </summary>
        public const byte DefaultPressureAveraging = 3;
    }
}