namespace TekuSP.Drivers.QMP6988.Enums
{
    /// <summary>
    /// OTP coefficient register addresses (A0..B8) for the QMP6988 device.
    /// Each enum value is the register address (byte) contained in the sensor's OTP area.
    /// </summary>
    public enum OtpRegister : byte
    {
        /// <summary>COE_b00_a0_ex: contains extension nibbles for <c>b00</c> (high nibble) and <c>a0</c> (low nibble). (0xB8)</summary>
        CoeB00A0Ex = 0xB8,

        /// <summary>Lower byte of <c>a2</c> (0xB7)</summary>
        A2_0 = 0xB7,
        /// <summary>Upper byte of <c>a2</c> (0xB6)</summary>
        A2_1 = 0xB6,
        /// <summary>Lower byte of <c>a1</c> (0xB5)</summary>
        A1_0 = 0xB5,
        /// <summary>Upper byte of <c>a1</c> (0xB4)</summary>
        A1_1 = 0xB4,
        /// <summary>Lower byte of <c>a0</c> (0xB3)</summary>
        A0_0 = 0xB3,
        /// <summary>Upper byte of <c>a0</c> (0xB2)</summary>
        A0_1 = 0xB2,

        /// <summary>Lower byte of <c>bp3</c> (0xB1)</summary>
        Bp3_0 = 0xB1,
        /// <summary>Upper byte of <c>bp3</c> (0xB0)</summary>
        Bp3_1 = 0xB0,

        /// <summary>Lower byte of <c>b21</c> (0xAF)</summary>
        B21_0 = 0xAF,
        /// <summary>Upper byte of <c>b21</c> (0xAE)</summary>
        B21_1 = 0xAE,
        /// <summary>Lower byte of <c>b12</c> (0xAD)</summary>
        B12_0 = 0xAD,
        /// <summary>Upper byte of <c>b12</c> (0xAC)</summary>
        B12_1 = 0xAC,
        /// <summary>Lower byte of <c>bp2</c> (0xAB)</summary>
        Bp2_0 = 0xAB,
        /// <summary>Upper byte of <c>bp2</c> (0xAA)</summary>
        Bp2_1 = 0xAA,
        /// <summary>Lower byte of <c>b11</c> (0xA9)</summary>
        B11_0 = 0xA9,
        /// <summary>Upper byte of <c>b11</c> (0xA8)</summary>
        B11_1 = 0xA8,
        /// <summary>Lower byte of <c>bp1</c> (0xA7)</summary>
        Bp1_0 = 0xA7,
        /// <summary>Upper byte of <c>bp1</c> (0xA6)</summary>
        Bp1_1 = 0xA6,
        /// <summary>Lower byte of <c>bt2</c> (0xA5)</summary>
        Bt2_0 = 0xA5,
        /// <summary>Upper byte of <c>bt2</c> (0xA4)</summary>
        Bt2_1 = 0xA4,
        /// <summary>Lower byte of <c>bt1</c> (0xA3)</summary>
        Bt1_0 = 0xA3,
        /// <summary>Upper byte of <c>bt1</c> (0xA2)</summary>
        Bt1_1 = 0xA2,
        /// <summary>Lower byte of <c>b00</c> (0xA1)</summary>
        B00_0 = 0xA1,
        /// <summary>Upper byte of <c>b00</c> (0xA0)</summary>
        B00_1 = 0xA0
    }
}
