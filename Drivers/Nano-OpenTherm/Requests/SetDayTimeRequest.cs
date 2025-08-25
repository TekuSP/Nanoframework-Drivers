using System;
using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Day of Week and Time of Day
    /// </summary>
    public class SetDayTimeRequest : WriteRequest
    {
        public SetDayTimeRequest() : base() { }
        public SetDayTimeRequest(Request baseReq) : base(baseReq) { }

        public DayOfWeek DayOfWeek { get; set; }
        public byte Hour { get; set; }
        public byte Minute { get; set; }

        protected override uint GetRawDataCore()
        {
            // low byte: (day<<5) | hour(0-23), high byte: minutes (0-59)
            byte low = (byte)(((int)DayOfWeek & 0x07) << 5 | (Hour & 0x1F));
            uint raw = (uint)((Minute & 0x3F) << 8 | low);
            return ProcessRequest(raw);
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
    }
}
