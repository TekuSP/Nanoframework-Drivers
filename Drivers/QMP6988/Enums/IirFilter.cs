namespace TekuSP.Drivers.QMP6988.Enums
{
    /// <summary>
    /// IIR (Infinite Impulse Response) filter coefficient.
    /// <para>
    /// The IIR filter suppresses disturbances (e.g., slamming a door) and smooths the output data.
    /// </para>
    /// </summary>
    public enum IirFilter : byte
    {
        /// <summary>
        /// Filter is disabled. 
        /// <para>Data reaches the output register immediately. High noise, fastest response.</para>
        /// </summary>
        Off = 0x0,

        /// <summary>
        /// Filter coefficient 2. 
        /// <para>Light smoothing. Samples needed to reach 75% of step response: 2.</para>
        /// </summary>
        Coeff_2 = 0x1,

        /// <summary>
        /// Filter coefficient 4.
        /// <para>Samples needed to reach 75% of step response: 4.</para>
        /// </summary>
        Coeff_4 = 0x2,

        /// <summary>
        /// Filter coefficient 8.
        /// <para>Moderate smoothing. Samples needed to reach 75% of step response: 8.</para>
        /// </summary>
        Coeff_8 = 0x3,

        /// <summary>
        /// Filter coefficient 16.
        /// <para>High smoothing. Good for indoor navigation. Samples needed to reach 75% of step response: 16.</para>
        /// </summary>
        Coeff_16 = 0x4,

        /// <summary>
        /// Filter coefficient 32.
        /// <para>Maximum smoothing. Best for weather monitoring. Slowest response to changes.</para>
        /// </summary>
        Coeff_32 = 0x5
    }
}
