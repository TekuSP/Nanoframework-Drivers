using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>Maximum relative modulation level setting (%). Although typically written by master, provide a typed response for symmetry and testability.</summary>
    public class MaxRelModLevelSettingResponse : Response
    {
        public MaxRelModLevelSettingResponse(MessageType mt = MessageType.WRITE_ACK) { MessageType = mt; }
        public MaxRelModLevelSettingResponse(Response r) : base(r) { }

        /// <summary>Percent 0..100 represented as 8.8 fixed-point.</summary>
        public float Percent { get; set; }

        protected override uint GetRawDataCore() => ProcessResponse(Utilities.GetRawPercentage(Percent));
        protected override void SetRawDataCore(uint value) => Percent = Utilities.GetPercentage(value);

        public override MessageType MessageType { get; set; }
        public override MessageID MessageID => MessageID.MaxRelModLevelSetting;
    }
}
