namespace TekuSP.Drivers.SHT3x.Enums
{
    /// <summary>
    /// SHT3x command identifiers.
    /// </summary>
    public enum Commands
    {
        /// <summary>
        /// Single shot measurement, high repeatability, clock stretching disabled.
        /// </summary>
        MeasureSingleShotHighRepeatability = 0x2400,

        /// <summary>
        /// Single shot measurement, high repeatability, clock stretching enabled.
        /// </summary>
        MeasureSingleShotHighRepeatabilityClockStretching = 0x2C06,

        /// <summary>
        /// Single shot measurement, medium repeatability, clock stretching disabled.
        /// </summary>
        MeasureSingleShotMediumRepeatability = 0x240B,

        /// <summary>
        /// Single shot measurement, medium repeatability, clock stretching enabled.
        /// </summary>
        MeasureSingleShotMediumRepeatabilityClockStretching = 0x2C0D,

        /// <summary>
        /// Single shot measurement, low repeatability, clock stretching disabled.
        /// </summary>
        MeasureSingleShotLowRepeatability = 0x2416,

        /// <summary>
        /// Single shot measurement, low repeatability, clock stretching enabled.
        /// </summary>
        MeasureSingleShotLowRepeatabilityClockStretching = 0x2C10,

        /// <summary>
        /// Start periodic measurement 0.5 mps, high repeatability.
        /// </summary>
        StartMeasurement05MpsHighRepeatability = 0x2032,

        /// <summary>
        /// Start periodic measurement 0.5 mps, medium repeatability.
        /// </summary>
        StartMeasurement05MpsMediumRepeatability = 0x2024,

        /// <summary>
        /// Start periodic measurement 0.5 mps, low repeatability.
        /// </summary>
        StartMeasurement05MpsLowRepeatability = 0x202F,

        /// <summary>
        /// Start periodic measurement 1 mps, high repeatability.
        /// </summary>
        StartMeasurement1MpsHighRepeatability = 0x2130,

        /// <summary>
        /// Start periodic measurement 1 mps, medium repeatability.
        /// </summary>
        StartMeasurement1MpsMediumRepeatability = 0x2126,

        /// <summary>
        /// Start periodic measurement 1 mps, low repeatability.
        /// </summary>
        StartMeasurement1MpsLowRepeatability = 0x212D,

        /// <summary>
        /// Start periodic measurement 2 mps, high repeatability.
        /// </summary>
        StartMeasurement2MpsHighRepeatability = 0x2236,

        /// <summary>
        /// Start periodic measurement 2 mps, medium repeatability.
        /// </summary>
        StartMeasurement2MpsMediumRepeatability = 0x2220,

        /// <summary>
        /// Start periodic measurement 2 mps, low repeatability.
        /// </summary>
        StartMeasurement2MpsLowRepeatability = 0x222B,

        /// <summary>
        /// Start periodic measurement 4 mps, high repeatability.
        /// </summary>
        StartMeasurement4MpsHighRepeatability = 0x2334,

        /// <summary>
        /// Start periodic measurement 4 mps, medium repeatability.
        /// </summary>
        StartMeasurement4MpsMediumRepeatability = 0x2322,

        /// <summary>
        /// Start periodic measurement 4 mps, low repeatability.
        /// </summary>
        StartMeasurement4MpsLowRepeatability = 0x2329,

        /// <summary>
        /// Start periodic measurement 10 mps, high repeatability.
        /// </summary>
        StartMeasurement10MpsHighRepeatability = 0x2737,

        /// <summary>
        /// Start periodic measurement 10 mps, medium repeatability.
        /// </summary>
        StartMeasurement10MpsMediumRepeatability = 0x2721,

        /// <summary>
        /// Start periodic measurement 10 mps, low repeatability.
        /// </summary>
        StartMeasurement10MpsLowRepeatability = 0x273A,

        /// <summary>
        /// Start ART (accelerated response time) measurement.
        /// </summary>
        StartArtMeasurement = 0x2B32,

        /// <summary>
        /// Read measurement data (periodic mode).
        /// </summary>
        ReadMeasurement = 0xE000,

        /// <summary>
        /// Stop periodic measurement.
        /// </summary>
        StopMeasurement = 0x3093,

        /// <summary>
        /// Enable internal heater.
        /// </summary>
        EnableHeater = 0x306D,

        /// <summary>
        /// Disable internal heater.
        /// </summary>
        DisableHeater = 0x3066,

        /// <summary>
        /// Read status register.
        /// </summary>
        ReadStatusRegister = 0xF32D,

        /// <summary>
        /// Clear status register.
        /// </summary>
        ClearStatusRegister = 0x3041,

        /// <summary>
        /// Soft reset.
        /// </summary>
        SoftReset = 0x30A2
    }
}
