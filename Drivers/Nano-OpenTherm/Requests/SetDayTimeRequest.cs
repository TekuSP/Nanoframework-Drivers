using System;

using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Interfaces;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Day of Week and Time of Day
    /// </summary>
    public class SetDayTimeRequest : WriteRequest, IDayOfWeekConvenience
    {
        #region Public Constructors

        public SetDayTimeRequest() : base()
        {
        }

        public SetDayTimeRequest(Request baseReq) : base(baseReq)
        {
        }

        /// <summary>
        /// Convenience constructor to initialize day-of-week, hour and minute.
        /// </summary>
        public SetDayTimeRequest(DayOfWeek day, byte hour, byte minute)
        {
            DayOfWeek = day;
            Hour = hour;
            Minute = minute;
        }

        #endregion Public Constructors

        #region Public Properties

    public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.DayTimeResponse);

        /// <summary>
        /// Hour of day (0..23). Packed in low byte bits 0..4.
        /// </summary>
        public byte Hour { get; set; }

        public bool IsFriday
        { get => DayOfWeek == DayOfWeek.Friday; set { if (value) DayOfWeek = DayOfWeek.Friday; } }

        // IDayOfWeekConvenience
        public bool IsMonday
        { get => DayOfWeek == DayOfWeek.Monday; set { if (value) DayOfWeek = DayOfWeek.Monday; } }

        public bool IsSaturday
        { get => DayOfWeek == DayOfWeek.Saturday; set { if (value) DayOfWeek = DayOfWeek.Saturday; } }

        public bool IsSunday
        { get => DayOfWeek == DayOfWeek.Sunday; set { if (value) DayOfWeek = DayOfWeek.Sunday; } }

        public bool IsThursday
        { get => DayOfWeek == DayOfWeek.Thursday; set { if (value) DayOfWeek = DayOfWeek.Thursday; } }

        public bool IsTuesday
        { get => DayOfWeek == DayOfWeek.Tuesday; set { if (value) DayOfWeek = DayOfWeek.Tuesday; } }

        public bool IsWednesday
        { get => DayOfWeek == DayOfWeek.Wednesday; set { if (value) DayOfWeek = DayOfWeek.Wednesday; } }

        public override MessageID MessageID => MessageID.DayTime;

        public override MessageType MessageType => MessageType.WRITE_DATA;

        /// <summary>
        /// Minute (0..59). Packed in the high byte bits 8..13.
        /// </summary>
        public byte Minute { get; set; }

        #endregion Public Properties

        #region Protected Properties

        /// <summary>
        /// Day of week (1..7). Packed in low byte bits 5..7.
        /// </summary>
        protected DayOfWeek DayOfWeek { get; set; }

        #endregion Protected Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
        {
            return ProcessRequest(Utilities.SetDayTime(DayOfWeek, Hour, Minute));
        }

        protected override void SetRawDataCore(uint value)
        {
            DayOfWeek = Utilities.GetDayOfWeekFromDayTime(value);
            Hour = Utilities.GetHourFromDayTime(value);
            Minute = Utilities.GetMinuteFromDayTime(value);
        }

        #endregion Protected Methods
    }
}