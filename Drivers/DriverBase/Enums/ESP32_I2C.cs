namespace DriverBase.Enums
{
    /// <summary>
    /// ESP32 I2C Pinout <a href="https://docs.nanoframework.net/content/esp32/esp32_pin_out.html#i2c"/>
    /// </summary>
    public enum ESP32_I2C
    {
        /// <summary>
        /// DATA GPIO 18 & Clock GPIO 19
        /// </summary>
        I2C1,

        /// <summary>
        /// DATA GPIO 25 & CLOCK GPIO 26
        /// </summary>
        I2C2
    }
}