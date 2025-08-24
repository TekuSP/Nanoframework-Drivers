using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Calendar date (day and month)
    /// </summary>
    public class SetDateRequest : WriteRequest
    {
        public SetDateRequest() : base() { }
        public SetDateRequest(Request baseReq) : base(baseReq) { }

        public byte Day { get; set; }
        public byte Month { get; set; }

        protected override ulong GetRawDataCore()
        {
            // low byte: day (1-31), high byte: month (1-12)
            uint raw = (uint)(((Month & 0x1F) << 8) | (Day & 0x1F));
            return ProcessRequest(raw);
        }
        protected override void SetRawDataCore(ulong value)
        {
            Day = (byte)(Utilities.GetLowByte(value) & 0x1F);
            Month = (byte)(Utilities.GetHighByte(value) & 0x1F);
        }

        public override MessageType MessageType => MessageType.WRITE_DATA;
        public override MessageID MessageID => MessageID.Date;
    }
}
