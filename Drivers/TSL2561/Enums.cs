namespace TekuSP.Drivers.TSL2561
{
    public enum Registers
    {
        TSL2561_REGISTER_CONTROL = 0x00,          // Control/power register
        TSL2561_REGISTER_TIMING = 0x01,           // Set integration time register
        TSL2561_REGISTER_THRESHHOLDL_LOW = 0x02,  // Interrupt low threshold low-byte
        TSL2561_REGISTER_THRESHHOLDL_HIGH = 0x03, // Interrupt low threshold high-byte
        TSL2561_REGISTER_THRESHHOLDH_LOW = 0x04,  // Interrupt high threshold low-byte
        TSL2561_REGISTER_THRESHHOLDH_HIGH = 0x05,  // Interrupt high threshold high-byte
        TSL2561_REGISTER_INTERRUPT = 0x06,  // Interrupt settings
        TSL2561_REGISTER_CRC = 0x08,        // Factory use only
        TSL2561_REGISTER_ID = 0x0A,         // TSL2561 identification setting
        TSL2561_REGISTER_CHAN0_LOW = 0x0C,  // Light data channel 0, low byte
        TSL2561_REGISTER_CHAN0_HIGH = 0x0D, // Light data channel 0, high byte
        TSL2561_REGISTER_CHAN1_LOW = 0x0E,  // Light data channel 1, low byte
        TSL2561_REGISTER_CHAN1_HIGH = 0x0F  // Light data channel 1, high byte
    }

    /** Three options for how long to integrate readings for */
    public enum IntegrationTime
    {
        TSL2561_INTEGRATIONTIME_13MS = 0x00,  // 13.7ms
        TSL2561_INTEGRATIONTIME_101MS = 0x01, // 101ms
        TSL2561_INTEGRATIONTIME_402MS = 0x02  // 402ms
    }

    /** TSL2561 offers 2 gain settings */
    public enum Gain
    {
        TSL2561_GAIN_1X = 0x00,  // No gain
        TSL2561_GAIN_16X = 0x10, // 16x gain
    }
}
