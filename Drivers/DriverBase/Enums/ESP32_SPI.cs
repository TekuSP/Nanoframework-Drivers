namespace DriverBase.Enums
{
    /// <summary>
    /// ESP32 SPI Pinout <a href="https://docs.nanoframework.net/content/esp32/esp32_pin_out.html#spi"/>
    /// </summary>
    public enum ESP32_SPI
    {
        /// <summary>
        /// MOSI GPIO 23 & MISO GPIO 25 & CLOCK GPIO 19
        /// </summary>
        SPI1,

        /// <summary>
        /// MOSI NOT CONNECTED & MISO NOT CONNECTED & CLOCK NOT CONNECTED
        /// </summary>
        SPI2
    }
}