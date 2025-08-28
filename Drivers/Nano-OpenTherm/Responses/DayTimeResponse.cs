using System;
using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Interfaces;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    public class DayTimeResponse : Response, IDayOfWeekConvenience
    {
        public DayTimeResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public DayTimeResponse(Response r) : base(r) { }

        public DayOfWeek DayOfWeek { get; set; }
        public byte Hour { get; set; }
        public byte Minute { get; set; }

        protected override uint GetRawDataCore()
        {
            ushort payload = Utilities.SetDayTime(DayOfWeek, Hour, Minute);
            return ProcessResponse(payload);
        }
        protected override void SetRawDataCore(uint value)
        {
            Minute = Utilities.GetMinuteFromDayTime(value);
            Hour = Utilities.GetHourFromDayTime(value);
            DayOfWeek = Utilities.GetDayOfWeekFromDayTime(value);
        }

        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.DayTime;

        // IDayOfWeekConvenience
        public bool IsMonday { get => DayOfWeek == DayOfWeek.Monday; set { if (value) DayOfWeek = DayOfWeek.Monday; } }
        public bool IsTuesday { get => DayOfWeek == DayOfWeek.Tuesday; set { if (value) DayOfWeek = DayOfWeek.Tuesday; } }
        public bool IsWednesday { get => DayOfWeek == DayOfWeek.Wednesday; set { if (value) DayOfWeek = DayOfWeek.Wednesday; } }
        public bool IsThursday { get => DayOfWeek == DayOfWeek.Thursday; set { if (value) DayOfWeek = DayOfWeek.Thursday; } }
        public bool IsFriday { get => DayOfWeek == DayOfWeek.Friday; set { if (value) DayOfWeek = DayOfWeek.Friday; } }
        public bool IsSaturday { get => DayOfWeek == DayOfWeek.Saturday; set { if (value) DayOfWeek = DayOfWeek.Saturday; } }
        public bool IsSunday { get => DayOfWeek == DayOfWeek.Sunday; set { if (value) DayOfWeek = DayOfWeek.Sunday; } }
    }
}
