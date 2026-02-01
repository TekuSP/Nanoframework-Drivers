namespace TekuSP.Drivers.TSL2561
{
    /// <summary>
    /// TSL2561 register addresses.
    /// </summary>
    public enum Registers
    {
        /// <summary>Control/power register.</summary>
        TSL2561_REGISTER_CONTROL = 0x00,
        /// <summary>Integration timing register.</summary>
        TSL2561_REGISTER_TIMING = 0x01,
        /// <summary>Interrupt low threshold (low byte).</summary>
        TSL2561_REGISTER_THRESHHOLDL_LOW = 0x02,
        /// <summary>Interrupt low threshold (high byte).</summary>
        TSL2561_REGISTER_THRESHHOLDL_HIGH = 0x03,
        /// <summary>Interrupt high threshold (low byte).</summary>
        TSL2561_REGISTER_THRESHHOLDH_LOW = 0x04,
        /// <summary>Interrupt high threshold (high byte).</summary>
        TSL2561_REGISTER_THRESHHOLDH_HIGH = 0x05,
        /// <summary>Interrupt control register.</summary>
        TSL2561_REGISTER_INTERRUPT = 0x06,
        /// <summary>CRC register (factory use only).</summary>
        TSL2561_REGISTER_CRC = 0x08,
        /// <summary>Device ID register.</summary>
        TSL2561_REGISTER_ID = 0x0A,
        /// <summary>Channel 0 data low byte.</summary>
        TSL2561_REGISTER_CHAN0_LOW = 0x0C,
        /// <summary>Channel 0 data high byte.</summary>
        TSL2561_REGISTER_CHAN0_HIGH = 0x0D,
        /// <summary>Channel 1 data low byte.</summary>
        TSL2561_REGISTER_CHAN1_LOW = 0x0E,
        /// <summary>Channel 1 data high byte.</summary>
        TSL2561_REGISTER_CHAN1_HIGH = 0x0F
    }

    /// <summary>
    /// Integration time settings.
    /// </summary>
    public enum IntegrationTime
    {
        /// <summary>13.7ms integration time.</summary>
        TSL2561_INTEGRATIONTIME_13MS = 0x00,
        /// <summary>101ms integration time.</summary>
        TSL2561_INTEGRATIONTIME_101MS = 0x01,
        /// <summary>402ms integration time.</summary>
        TSL2561_INTEGRATIONTIME_402MS = 0x02
    }

    /// <summary>
    /// Gain settings.
    /// </summary>
    public enum Gain
    {
        /// <summary>1x gain (no gain).</summary>
        TSL2561_GAIN_1X = 0x00,
        /// <summary>16x gain.</summary>
        TSL2561_GAIN_16X = 0x10,
    }
}
