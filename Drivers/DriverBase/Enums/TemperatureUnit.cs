namespace TekuSP.Drivers.DriverBase.Enums
{
    /// <summary>
    /// Supported temperature unit types.
    /// </summary>
    public enum TemperatureUnit
    {
        /// <summary>Kelvin.</summary>
        Kelvin,
        /// <summary>Kelvin in Q16 fixed-point format.</summary>
        KelvinQ16,
        /// <summary>Celsius.</summary>
        Celsius,
        /// <summary>Celsius in Q16 fixed-point format.</summary>
        CelsiusQ16,
        /// <summary>Fahrenheit.</summary>
        Fahrenheit,
        /// <summary>Fahrenheit in Q16 fixed-point format.</summary>
        FahrenheitQ16,
        /// <summary>Rankine.</summary>
        Rankine,
        /// <summary>Rankine in Q16 fixed-point format.</summary>
        RankineQ16,
        /// <summary>Other or custom unit.</summary>
        Other = 100
    }
}