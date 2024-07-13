namespace TekuSP.Drivers.Nano_OpenTherm
{
    public static class Utilities
    {
        /// <summary>
        /// Gets raw temperature from float
        /// </summary>
        /// <param name="temperature">Temperature</param>
        /// <returns>Uint Raw Temperature</returns>
        public static uint GetRawTemperature(float temperature)
        {
            if (temperature < 0)
                temperature = 0;
            if (temperature > 100)
                temperature = 100;
            return (uint)(temperature * 256);
        }
        /// <summary>
        /// Parity check for a frame
        /// </summary>
        /// <param name="frame">Frame to check on</param>
        /// <returns>Parity</returns>
        public static bool Parity(ulong frame)
        {
            byte p = 0;
            while (frame > 0)
            {
                if ((frame & 1) == 1)
                    p++;
                frame >>= 1;
            }
            return (p & 1) == 1;
        }
    }
}
