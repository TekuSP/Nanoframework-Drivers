using DriverBase.Interfaces;

namespace CST816D
{
    public static class Registers
    {
        public static byte I2CAddress => 0x15;
        public static byte StartRegisterAddress => 0x00;
        public static byte AllRegistersByte => 20;

        public static byte ScanRegisterByte => 9;
        public static byte ScanRegisterAddress => 0x00;
        public static byte VersionAddress => 0x15;
        public static byte VersionInfoAddress => 0xA7;

        public static byte GestureIDRegister => 0x01;
        public static byte XYRegister => 0x03;

        public static byte XHRegister => 0x03;
        public static byte XLRegister => 0x04;
        public static byte YHRegister => 0x05;
        public static byte YLRegister => 0x06;

        public static byte SleepRegister => 0xA5;
        public static byte StandbyCommand => 0x03;

        public static byte MSBMask => 0x0F;
        public static byte MSBShift => 0;
        public static byte LSBMask => 0xFF;
        public static byte LSBShift => 0;

        public static byte MaxX => 239;
        public static byte MinX => 0;
        public static byte MaxY => 239;
        public static byte MinY => 0;
    }
    public class Register : ITouchData
    {
        public ushort Reserve0 { get; set; }
        public byte Gesture { get; set; }
        public ushort TouchPoints { get; set; }
        public ushort XH { get; set; }
        public ushort XL { get; set; }
        public ushort YH { get; set; }
        public ushort YL { get; set; }
        public ushort Pressure { get; set; }
        public ushort Miscellaneous { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
    }
    public enum Gesture
    {
        GEST_NONE = 0x00,
        GEST_MOVE_UP = 0x01,
        GEST_MOVE_DOWN = 0x02,
        GEST_MOVE_RIGHT = 0x03,
        GEST_MOVE_LEFT = 0x04,
        GEST_SINGLE_CLICK = 0x05,
        GEST_LONG_PRESS = 0x0c
    }
}