namespace TekuSP.Drivers.TCS34725.Enums
{
    /// <summary>
    /// ALS interrupt persistence settings.
    /// </summary>
    public enum Pers
    {
        /// <summary>Persistence register address.</summary>
        TCS34725_PERS = 0x0C,
        /// <summary>Interrupt disabled.</summary>
        TCS34725_PERS_NONE = 0x00,
        /// <summary>Interrupt after 1 cycle.</summary>
        TCS34725_PERS_1_CYCLE = 0x01,
        /// <summary>Interrupt after 2 cycles.</summary>
        TCS34725_PERS_2_CYCLE = 0x02,
        /// <summary>Interrupt after 3 cycles.</summary>
        TCS34725_PERS_3_CYCLE = 0x03,
        /// <summary>Interrupt after 5 cycles.</summary>
        TCS34725_PERS_5_CYCLE = 0x04,
        /// <summary>Interrupt after 10 cycles.</summary>
        TCS34725_PERS_10_CYCLE = 0x05,
        /// <summary>Interrupt after 15 cycles.</summary>
        TCS34725_PERS_15_CYCLE = 0x06,
        /// <summary>Interrupt after 20 cycles.</summary>
        TCS34725_PERS_20_CYCLE = 0x07,
        /// <summary>Interrupt after 25 cycles.</summary>
        TCS34725_PERS_25_CYCLE = 0x08,
        /// <summary>Interrupt after 30 cycles.</summary>
        TCS34725_PERS_30_CYCLE = 0x09,
        /// <summary>Interrupt after 35 cycles.</summary>
        TCS34725_PERS_35_CYCLE = 0x0a,
        /// <summary>Interrupt after 40 cycles.</summary>
        TCS34725_PERS_40_CYCLE = 0x0b,
        /// <summary>Interrupt after 45 cycles.</summary>
        TCS34725_PERS_45_CYCLE = 0x0c,
        /// <summary>Interrupt after 50 cycles.</summary>
        TCS34725_PERS_50_CYCLE = 0x0d,
        /// <summary>Interrupt after 55 cycles.</summary>
        TCS34725_PERS_55_CYCLE = 0x0e,
        /// <summary>Interrupt after 60 cycles.</summary>
        TCS34725_PERS_60_CYCLE = 0x0f
    }
}
