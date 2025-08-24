using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Remote override operating modes (HC1/HC2/DHW)
    /// </summary>
    public class SetRemoteOverrideOperatingModeRequest : WriteRequest
    {
        public SetRemoteOverrideOperatingModeRequest() : base() { }
        public SetRemoteOverrideOperatingModeRequest(Request baseReq) : base(baseReq) { }

        public byte Modes { get; set; }

        protected override ulong GetRawDataCore()
        {
            uint raw = Modes; // low byte
            return ProcessRequest(raw);
        }
        protected override void SetRawDataCore(ulong value)
        {
            Modes = Utilities.GetLowByte(value);
        }

        public override MessageType MessageType => MessageType.WRITE_DATA;
        public override MessageID MessageID => MessageID.RemoteOverrideOperatingModeHeatingDHW;
    }
}
