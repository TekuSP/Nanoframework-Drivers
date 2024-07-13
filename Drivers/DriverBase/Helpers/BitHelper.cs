namespace TekuSP.Drivers.DriverBase.Helpers
{
    public static class BitHelper
    {
        #region Public Methods

        public static bool GetBit(this byte b, int bitNumber)
        {
            return (b & (1 << bitNumber - 1)) != 0;
        }
        public static bool GetBit(this uint b, int bitNumber)
        {
            return (b & (1 << bitNumber - 1)) != 0;
        }
        public static bool GetBit(this ulong b, int bitNumber)
        {
            return (b & ((ulong)1 << bitNumber - 1)) != 0;
        }
        public static uint HighWord(this uint number) => number & 0xFFFF0000;

        public static uint HighWord(this uint number, uint newValue) => (number & 0x0000FFFF) + (newValue << 16);

        public static uint LowWord(this uint number) => number & 0x0000FFFF;

        public static uint LowWord(this uint number, uint newValue) => (number & 0xFFFF0000) + (newValue & 0x0000FFFF);

        public static byte SetBit(this byte b, int pos, bool value)
        {
            if (value)
                return (byte)(b | (1 << pos));
            return (byte)(b & ~(1 << pos));
        }

        #endregion Public Methods
    }
}