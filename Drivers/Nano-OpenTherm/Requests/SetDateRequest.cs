using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Calendar date (day and month)
    /// </summary>
    public class SetDateRequest : WriteRequest
    {
        public SetDateRequest() : base() { }
        public SetDateRequest(Request baseReq) : base(baseReq) { }

    /// <summary>
    /// Day of month (1..31), encoded in the low byte (bits 0-4).
    /// </summary>
    public byte Day { get; set; }
    /// <summary>
    /// Month of year (1..12), encoded in the high byte (bits 8-12).
    /// </summary>
    public Month Month { get; set; }

        protected override uint GetRawDataCore()
        {
            // low byte: day (1-31, 5 bits), high byte: month (1-12, 5 bits)
            var day = (byte)(Day & 0x1F);
            var month = (byte)(((byte)Month) & 0x1F);
            ushort payload = 0;
            payload = Utilities.SetLowByte(payload, day);
            payload = Utilities.SetHighByte(payload, month);
            return ProcessRequest(payload);
        }
        protected override void SetRawDataCore(uint value)
        {
            Day = (byte)(Utilities.GetLowByte(value) & 0x1F);
            Month = (Month)(Utilities.GetHighByte(value) & 0x1F);
        }

        public override MessageType MessageType => MessageType.WRITE_DATA;
        public override MessageID MessageID => MessageID.Date;

    // Convenience selectors for Month
        public bool IsJanuary { get => Month == Month.January; set { if (value) Month = Month.January; } }
        public bool IsFebruary { get => Month == Month.February; set { if (value) Month = Month.February; } }
        public bool IsMarch { get => Month == Month.March; set { if (value) Month = Month.March; } }
        public bool IsApril { get => Month == Month.April; set { if (value) Month = Month.April; } }
        public bool IsMay { get => Month == Month.May; set { if (value) Month = Month.May; } }
        public bool IsJune { get => Month == Month.June; set { if (value) Month = Month.June; } }
        public bool IsJuly { get => Month == Month.July; set { if (value) Month = Month.July; } }
        public bool IsAugust { get => Month == Month.August; set { if (value) Month = Month.August; } }
        public bool IsSeptember { get => Month == Month.September; set { if (value) Month = Month.September; } }
        public bool IsOctober { get => Month == Month.October; set { if (value) Month = Month.October; } }
        public bool IsNovember { get => Month == Month.November; set { if (value) Month = Month.November; } }
        public bool IsDecember { get => Month == Month.December; set { if (value) Month = Month.December; } }
    }
}
