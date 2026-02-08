using UnitsNet;
using UnitsNet.Units;

namespace TekuSP.Drivers.QMP6988.Helpers
{
    /// <summary>
    /// Holds the factory calibration coefficients read from the device's OTP (One Time Programmable) memory.
    /// <para>
    /// These coefficients are used in the compensation formulas to convert raw ADC values 
    /// into actual Temperature (in °C) and Pressure (in Pa).
    /// </para>
    /// </summary>
    public class CalibrationData
    {
        /// <summary>
        /// Constructs the factory calibration data object with all coefficients.
        /// </summary>
        /// <param name="a0">Temperature Coefficient a0.</param>
        /// <param name="a1">Temperature Coefficient a1.</param>
        /// <param name="a2">Temperature Coefficient a2.</param>
        /// <param name="b00">Pressure Coefficient b00.</param>
        /// <param name="bt1">Pressure Coefficient bt1.</param>
        /// <param name="bt2">Pressure Coefficient bt2.</param>
        /// <param name="bp1">Pressure Coefficient bp1.</param>
        /// <param name="b11">Pressure Coefficient b11.</param>
        /// <param name="bp2">Pressure Coefficient bp2.</param>
        /// <param name="b12">Pressure Coefficient b12.</param>
        /// <param name="b21">Pressure Coefficient b21.</param>
        /// <param name="bp3">Pressure Coefficient bp3.</param>
        public CalibrationData(uint a0, short a1, short a2, uint b00, short bt1, short bt2, short bp1, short b11, short bp2, short b12, short b21, short bp3)
        {
            A0 = a0;
            A1 = a1;
            A2 = a2;
            B00 = b00;
            Bt1 = bt1;
            Bt2 = bt2;
            Bp1 = bp1;
            B11 = b11;
            Bp2 = bp2;
            B12 = b12;
            B21 = b21;
            Bp3 = bp3;
        }

        // --- Temperature Coefficients ---

        /// <summary>
        /// Temperature Coefficient a0.
        /// <para>20-bit unsigned integer.</para>
        /// <para>Represents the base temperature offset.</para>
        /// </summary>
        public uint A0
        {
            get; set;
        }

        /// <summary>
        /// Temperature Coefficient a1.
        /// <para>16-bit signed integer.</para>
        /// <para>Represents the linear temperature sensitivity.</para>
        /// </summary>
        public short A1
        {
            get; set;
        }

        /// <summary>
        /// Temperature Coefficient a2.
        /// <para>16-bit signed integer.</para>
        /// <para>Represents the quadratic temperature sensitivity.</para>
        /// </summary>
        public short A2
        {
            get; set;
        }

        // --- Pressure Coefficients ---

        /// <summary>
        /// Pressure Coefficient b00.
        /// <para>20-bit unsigned integer.</para>
        /// <para>Represents the base pressure offset.</para>
        /// </summary>
        public uint B00
        {
            get; set;
        }

        /// <summary>
        /// Pressure Coefficient bt1.
        /// <para>16-bit signed integer.</para>
        /// <para>Represents the pressure sensitivity to temperature (1st order).</para>
        /// </summary>
        public short Bt1
        {
            get; set;
        }

        /// <summary>
        /// Pressure Coefficient bt2.
        /// <para>16-bit signed integer.</para>
        /// <para>Represents the pressure sensitivity to temperature (2nd order).</para>
        /// </summary>
        public short Bt2
        {
            get; set;
        }

        /// <summary>
        /// Pressure Coefficient bp1.
        /// <para>16-bit signed integer.</para>
        /// <para>Represents the linear pressure sensitivity.</para>
        /// </summary>
        public short Bp1
        {
            get; set;
        }

        /// <summary>
        /// Pressure Coefficient b11.
        /// <para>16-bit signed integer.</para>
        /// <para>Represents the pressure-temperature cross-term sensitivity.</para>
        /// </summary>
        public short B11
        {
            get; set;
        }

        /// <summary>
        /// Pressure Coefficient bp2.
        /// <para>16-bit signed integer.</para>
        /// <para>Represents the quadratic pressure sensitivity.</para>
        /// </summary>
        public short Bp2
        {
            get; set;
        }

        /// <summary>
        /// Pressure Coefficient b12.
        /// <para>16-bit signed integer.</para>
        /// <para>Represents the higher-order pressure-temperature cross-term.</para>
        /// </summary>
        public short B12
        {
            get; set;
        }

        /// <summary>
        /// Pressure Coefficient b21.
        /// <para>16-bit signed integer.</para>
        /// <para>Represents the higher-order pressure-temperature cross-term.</para>
        /// </summary>
        public short B21
        {
            get; set;
        }

        /// <summary>
        /// Pressure Coefficient bp3.
        /// <para>16-bit signed integer.</para>
        /// <para>Represents the cubic pressure sensitivity.</para>
        /// </summary>
        public short Bp3
        {
            get; set;
        }

        /// <summary>
        /// Automatically compensates the raw temperature reading from the sensor using the calibration coefficients.
        /// </summary>
        /// <param name="rawTemperature">Raw temperature reading from the sensor.</param>
        /// <param name="targetUnit">The target temperature unit for the compensation.</param>
        /// <returns>The compensated temperature in the target unit.</returns>
        public Temperature CompensateTemperature(uint rawTemperature, TemperatureUnit targetUnit)
        {
            // 1. Normalize the raw ADC values (subtract 2^23)
            double dt = rawTemperature - 8388608.0;

            // ---------------------------------------------------------
            // 2. Calculate Compensated Temperature
            // Formula: T = A0 + (A1 * dt) + (A2 * dt^2)
            // Note: The coefficients must be scaled by their respective powers of 2.
            // ---------------------------------------------------------

            // A0 is scaled by 2^-4 (16)
            // A1 is scaled by 2^-19
            // A2 is scaled by 2^-35

            double celsius = (A0 / 16.0) +
                          (A1 * dt / 524288.0) +
                          (A2 * (dt * dt) / 34359738368.0);

            return Temperature.FromDegreesCelsius(celsius).ToUnit(targetUnit);
        }

        /// <summary>
        /// Automatically compensates the raw pressure reading from the sensor using the calibration coefficients and the raw temperature for cross-compensation.
        /// </summary>
        /// <param name="rawPressure">Raw pressure reading from the sensor.</param>
        /// <param name="rawTemperature">Raw temperature reading from the sensor.</param>
        /// <param name="targetUnit">The target pressure unit for the compensation.</param>
        /// <returns>The compensated pressure in the target unit.</returns>
        public Pressure CompensatePressure(uint rawPressure, uint rawTemperature, PressureUnit targetUnit)
        {
            // 1. Normalize the raw ADC values (subtract 2^23)
            double dp = rawPressure - 8388608.0;
            double dt = rawTemperature - 8388608.0;

            // ---------------------------------------------------------
            // 2. Calculate Compensated Pressure
            // Formula is a mix of linear, quadratic, and cross-term (temp * press) parts.
            // ---------------------------------------------------------

            // Basic terms
            double press = (B00 / 16.0) +
                           (Bt1 * dt / 524288.0) +
                           (Bp1 * dp / 524288.0);

            // Quadratic and Cross terms (scaled by 2^-35)
            // Note: 2^35 = 34,359,738,368
            press += (B11 * dt * dp) / 34359738368.0;
            press += (Bt2 * (dt * dt)) / 34359738368.0;
            press += (Bp2 * (dp * dp)) / 34359738368.0;

            // Cubic and Higher Order terms (scaled by 2^-50)
            // Note: 2^50 = 1,125,899,906,842,624
            press += (B12 * dp * (dt * dt)) / 1125899906842624.0;
            press += (B21 * dt * (dp * dp)) / 1125899906842624.0;
            press += (Bp3 * (dp * dp * dp)) / 1125899906842624.0;

            return Pressure.FromPascals(press).ToUnit(targetUnit);
        }
    }
}
