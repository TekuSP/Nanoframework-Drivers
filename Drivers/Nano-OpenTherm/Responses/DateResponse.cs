using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Interfaces;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class DateResponse : Response, IMonthConvenience
    {
        public DateResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public DateResponse(Response r) : base(r) { }

        public byte Day { get; set; }
        public Month Month { get; set; }

        protected override uint GetRawDataCore()
        {
            ushort payload = Utilities.SetDate(Day, Month);
            return ProcessResponse(payload);
        }
        protected override void SetRawDataCore(uint value)
        {
            Day = Utilities.GetDateDay(value);
            Month = Utilities.GetDateMonth(value);
        }

        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.Date;

        // IMonthConvenience
        public bool IsApril { get => Month == Month.April; set { if (value) Month = Month.April; } }
        public bool IsAugust { get => Month == Month.August; set { if (value) Month = Month.August; } }
        public bool IsDecember { get => Month == Month.December; set { if (value) Month = Month.December; } }
        public bool IsFebruary { get => Month == Month.February; set { if (value) Month = Month.February; } }
        public bool IsJanuary { get => Month == Month.January; set { if (value) Month = Month.January; } }
        public bool IsJuly { get => Month == Month.July; set { if (value) Month = Month.July; } }
        public bool IsJune { get => Month == Month.June; set { if (value) Month = Month.June; } }
        public bool IsMarch { get => Month == Month.March; set { if (value) Month = Month.March; } }
        public bool IsMay { get => Month == Month.May; set { if (value) Month = Month.May; } }
        public bool IsNovember { get => Month == Month.November; set { if (value) Month = Month.November; } }
        public bool IsOctober { get => Month == Month.October; set { if (value) Month = Month.October; } }
        public bool IsSeptember { get => Month == Month.September; set { if (value) Month = Month.September; } }
    }
}
