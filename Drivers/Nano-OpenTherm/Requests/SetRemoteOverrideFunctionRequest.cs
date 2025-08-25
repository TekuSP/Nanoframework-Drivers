using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Function of manual/program changes in master and remote room setpoint
    /// </summary>
    public class SetRemoteOverrideFunctionRequest : WriteRequest
    {
        public SetRemoteOverrideFunctionRequest() : base() { }
        public SetRemoteOverrideFunctionRequest(Request baseReq) : base(baseReq) { }

        public Enums.RemoteOverrideFunction Function { get; set; }

        protected override uint GetRawDataCore()
        {
            uint raw = (uint)((byte)Function); // low byte
            return ProcessRequest(raw);
        }
        protected override void SetRawDataCore(uint value)
        {
            Function = Utilities.GetRemoteOverrideFunction(value);
        }

        public override MessageType MessageType => MessageType.WRITE_DATA;
        public override MessageID MessageID => MessageID.RemoteOverrideFunction;
    }
}
