namespace TekuSP.Drivers.QMP6988.Enums
{
    /// <summary>
    /// Power Mode (CTRL_MEAS bits 1-0)
    /// </summary>
    public enum PowerMode : byte
    {
        /// <summary>
        /// No measurements. Sensor is in low power sleep mode.
        /// </summary>
        Sleep = 0x00,
        /// <summary>
        /// Measurement right now. Sensor will automatically go back to sleep after measurement is done.
        /// </summary>
        Forced = 0x01, // Or 0x02 both are valid
        /// <summary>
        /// Periodical measurements at the configured <see cref="StandbyTime"/>. Sensor will automatically take measurements at the configured <see cref="StandbyTime"/> until power mode is changed.
        /// </summary>
        Normal = 0x03
    }
}
