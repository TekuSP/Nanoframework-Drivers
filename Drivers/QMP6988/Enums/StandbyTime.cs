namespace TekuSP.Drivers.QMP6988.Enums
{
    /// <summary>
    /// Defines the standby time (wait time) between measurements when in Normal Mode.
    /// <para>
    /// Note: The actual sampling rate is slightly slower than the standby time implies, 
    /// as it equals [Standby Time] + [Measurement Time] (which depends on <see cref="Oversampling"/>).
    /// </para>
    /// </summary>
    public enum StandbyTime : byte
    {
        /// <summary>
        /// 1 ms standby time.
        /// <para>Shortest gap. Max theoretical rate ~1000Hz (if oversampling is low).</para>
        /// </summary>
        Ms1 = 0,

        /// <summary>
        /// 5 ms standby time.
        /// </summary>
        Ms5 = 1,

        /// <summary>
        /// 50 ms standby time.
        /// <para>Approx 20 measurements per second.</para>
        /// </summary>
        Ms50 = 2,

        /// <summary>
        /// 250 ms standby time.
        /// <para>4 measurements per second (4 Hz).</para>
        /// </summary>
        Ms250 = 3,

        /// <summary>
        /// 500 ms standby time.
        /// <para>2 measurements per second (2 Hz).</para>
        /// </summary>
        Ms500 = 4,

        /// <summary>
        /// 1000 ms standby time.
        /// <para>1 measurement per second (1 Hz).</para>
        /// </summary>
        Ms1000 = 5,

        /// <summary>
        /// 2000 ms standby time.
        /// <para>1 measurement every 2 seconds (0.5 Hz).</para>
        /// </summary>
        Ms2000 = 6,

        /// <summary>
        /// 4000 ms standby time.
        /// <para>1 measurement every 4 seconds (0.25 Hz). Best for very low power logging.</para>
        /// </summary>
        Ms4000 = 7
    }
}
