namespace TekuSP.Drivers.MHZ19B
{
    /// <summary>
    /// MH-Z19B command set for UART transactions.
    /// </summary>
    public enum MHZCommands
    {
        /// <summary>Recovery reset (changes operation mode and resets MCU).</summary>
        RecoveryReset = 0x78,
        /// <summary>Auto-calibration (ABC) enable/disable command.</summary>
        ABC = 0x79,
        /// <summary>Get ABC logic status.</summary>
        GetABC = 0x7D,
        /// <summary>Raw CO2 reading.</summary>
        RawCO2 = 0x84,
        /// <summary>CO2 with unlimited temperature range.</summary>
        CO2UnlimitedTemp = 0x85,
        /// <summary>CO2 with limited temperature range.</summary>
        CO2LimitedTemp = 0x86,
        /// <summary>Zero-point calibration command.</summary>
        ZeroCalibration = 0x87,
        /// <summary>Span calibration command.</summary>
        SpanCalibration = 0x88,
        /// <summary>Set measurement range.</summary>
        Range = 0x99,
        /// <summary>Read measurement range.</summary>
        GetRange = 0x9B,
        /// <summary>Read background CO2 value.</summary>
        GetBackgroundCO2 = 0x9C,
        /// <summary>Read firmware version.</summary>
        GetFirmwaveVersion = 0xA0,
        /// <summary>Request last response.</summary>
        ResendMessage = 0xA2,
        /// <summary>Read temperature calibration value.</summary>
        GetTemperatureCalibration = 0xA3
    }
}
