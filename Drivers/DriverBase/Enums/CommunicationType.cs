namespace TekuSP.Drivers.DriverBase.Enums
{
    /// <summary>
    /// Supported transport/communication types for drivers.
    /// </summary>
    public enum CommunicationType
    {
        /// <summary>I2C bus.</summary>
        I2C,
        /// <summary>SPI bus.</summary>
        SPI,
        /// <summary>1-Wire bus.</summary>
        OneWire,
        /// <summary>UART/serial bus.</summary>
        Serial,
        /// <summary>Other or custom transport.</summary>
        Other = 100
    }
}