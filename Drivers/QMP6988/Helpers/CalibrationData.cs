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
        // Internal fixed-point converted coefficients used by the integer compensation routines.
        // Match C++ M5Stack types: a0,b00,a1,a2 are 32-bit (int); higher-order bt*/bp* are 64-bit.
        private int IkA0;
        private int IkB00;
        private int IkA1;
        private int IkA2;
        private long IkBt1; private long IkBt2; private long IkBp1; private long IkB11;
        private long IkBp2; private long IkB12; private long IkB21; private long IkBp3;

        public CalibrationData(int a0, short a1, short a2, int b00, short bt1, short bt2, short bp1, short b11, short bp2, short b12, short b21, short bp3)
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

            // Precompute internal integer-format coefficients following the reference M5Stack implementation.
            ComputeInternalCalibration();
        }

        // --- Temperature Coefficients ---

        /// <summary>
        /// Temperature Coefficient a0.
        /// <para>20-bit signed integer (two's complement).</para>
        /// <para>Represents the base temperature offset (fixed-point Q16 scaling).</para>
        /// </summary>
        public int A0
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
        /// <para>20-bit signed integer (two's complement).</para>
        /// <para>Represents the base pressure offset (fixed-point Q16 scaling).</para>
        /// </summary>
        public int B00
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

            // Use the integer fixed-point algorithm ported from the reference implementation
            // (M5Stack QMP6988 driver) to get consistent results with device-specific scaling.
            int dT = (int)(rawTemperature - 8388608.0);
            short tInt = ConvTx02e(dT);
            double celsius = tInt / 256.0; // per reference: T = T_int / 256
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

            // Port of the reference integer pressure algorithm (M5Stack QMP6988 driver)
            int dP = (int)(rawPressure - 8388608.0);
            int dT = (int)(rawTemperature - 8388608.0);
            short tx = ConvTx02e(dT);
            int pInt = GetPressure02e(dP, tx);
            double pascals = pInt / 16.0; // per reference: pressure = P_int / 16
            return Pressure.FromPascals(pascals).ToUnit(targetUnit);
        }

        private void ComputeInternalCalibration()
        {
            // Follow M5Stack reference conversions (integer fixed-point)
            IkA0 = A0; // 20Q4 per reference comments (stored as 32-bit)
            IkB00 = B00; // 32-bit
            IkA1 = (int)(3608L * A1 - 1731677965L);   // 31Q23 -> store as 32-bit
            IkA2 = (int)(16889L * A2 - 87619360L);    // 30Q47 -> store as 32-bit

            IkBt1 = 2982L * Bt1 + 107370906L;  // 28Q15 (64-bit)
            IkBt2 = 329854L * Bt2 + 108083093L; // 34Q38
            IkBp1 = 19923L * Bp1 + 1133836764L; // 31Q20
            IkB11 = 2406L * B11 + 118215883L;   // 28Q34
            IkBp2 = 3079L * Bp2 - 181579595L;   // 29Q43
            IkB12 = 6846L * B12 + 85590281L;    // 29Q53
            IkB21 = 13836L * B21 + 79333336L;   // 29Q60
            IkBp3 = 2915L * Bp3 + 157155561L;   // 28Q65
        }

        private short ConvTx02e(int dt)
        {
            long wk1, wk2;
            wk1 = ((long)IkA1) * (long)dt;
            wk2 = (((long)IkA2) * (long)dt) >> 14;
            wk2 = (wk2 * (long)dt) >> 10;
            wk2 = ((wk1 + wk2) / 32767L) >> 19;
            short ret = (short)(((long)IkA0 + wk2) >> 4);
            return ret;
        }

        private int GetPressure02e(int dp, short tx)
        {
            // Reimplemented using the same stepwise operations as shown in the diagnostic breakdown
            // to ensure identical intermediate results and avoid subtle ordering/truncation differences.
            long wk1 = IkBt1 * (long)tx;
            long wk2 = (IkBp1 * (long)dp) >> 5;
            wk1 += wk2;

            // first-stage higher-order terms
            long tmp = (IkBt2 * (long)tx) >> 1;
            tmp = (tmp * (long)tx) >> 8;
            long wk3 = tmp;

            long t2 = (IkB11 * (long)tx) >> 4;
            t2 = (t2 * (long)dp) >> 1;
            wk3 += t2;

            long t3 = (IkBp2 * (long)dp) >> 13;
            t3 = (t3 * (long)dp) >> 1;
            wk3 += t3;

            // reduce stage1
            long stage1 = (wk1 + (wk3 >> 14)) / 32767L;
            long stage1_q4 = stage1 >> 11; // reduce to Q4

            // second stage terms
            long s2 = IkB12 * (long)tx;
            s2 = (s2 * (long)tx) >> 22;
            s2 = (s2 * (long)dp) >> 1;

            long s3 = IkB21 * (long)tx;
            s3 = (s3 >> 6);
            s3 = (s3 * (long)dp) >> 23;
            s3 = (s3 * (long)dp) >> 1;

            long s4 = IkBp3 * (long)dp;
            s4 = (s4 >> 12);
            s4 = (s4 * (long)dp) >> 23;
            s4 = (s4 * (long)dp);

            long stage2 = (s2 + s3 + s4) >> 15;
            long stage2_div = stage2 / 32767L;
            long stage2_q4 = stage2_div >> 11;

            long final_int = stage1_q4 + stage2_q4 + IkB00;
            return (int)final_int;
        }
    }
}
