using System;
using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Day of Week and Time of Day
    /// </summary>
    public class SetDayTimeRequest : WriteRequest
    {
        public SetDayTimeRequest() : base() { }
        public SetDayTimeRequest(Request baseReq) : base(baseReq) { }

    /// <summary>
    /// Day of week (1..7). Packed in low byte bits 5..7.
    /// </summary>
    public DayOfWeek DayOfWeek { get; set; }
    /// <summary>
    /// Hour of day (0..23). Packed in low byte bits 0..4.
    /// </summary>
    public byte Hour { get; set; }
    /// <summary>
    /// Minute (0..59). Packed in the high byte bits 8..13.
    /// </summary>
    public byte Minute { get; set; }

        protected override uint GetRawDataCore()
        {
            // low byte: (day<<5) | hour(0-23), high byte: minutes (0-59)
            byte low = (byte)(((int)DayOfWeek & 0x07) << 5 | (Hour & 0x1F));
            byte high = (byte)(Minute & 0x3F);
            ushort payload = Utilities.MakeUShort(high, low);
            return ProcessRequest(payload);
        }
        protected override void SetRawDataCore(uint value)
        {
            var low = Utilities.GetLowByte(value);
            var high = Utilities.GetHighByte(value);
            DayOfWeek = (DayOfWeek)((low >> 5) & 0x07);
            Hour = (byte)(low & 0x1F);
            Minute = (byte)(high & 0x3F);
        }

        public override MessageType MessageType => MessageType.WRITE_DATA;
        public override MessageID MessageID => MessageID.DayTime;

    // Convenience selectors
        public bool IsMonday { get => DayOfWeek == DayOfWeek.Monday; set { if (value) DayOfWeek = DayOfWeek.Monday; } }
        public bool IsTuesday { get => DayOfWeek == DayOfWeek.Tuesday; set { if (value) DayOfWeek = DayOfWeek.Tuesday; } }
        public bool IsWednesday { get => DayOfWeek == DayOfWeek.Wednesday; set { if (value) DayOfWeek = DayOfWeek.Wednesday; } }
        public bool IsThursday { get => DayOfWeek == DayOfWeek.Thursday; set { if (value) DayOfWeek = DayOfWeek.Thursday; } }
        public bool IsFriday { get => DayOfWeek == DayOfWeek.Friday; set { if (value) DayOfWeek = DayOfWeek.Friday; } }
        public bool IsSaturday { get => DayOfWeek == DayOfWeek.Saturday; set { if (value) DayOfWeek = DayOfWeek.Saturday; } }
        public bool IsSunday { get => DayOfWeek == DayOfWeek.Sunday; set { if (value) DayOfWeek = DayOfWeek.Sunday; } }
    }
}
