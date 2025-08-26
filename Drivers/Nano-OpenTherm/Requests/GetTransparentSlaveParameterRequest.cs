using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads a specific transparent slave parameter value by index.
    /// </summary>
    public class GetTransparentSlaveParameterRequest : ReadRequest
    {
        public GetTransparentSlaveParameterRequest() : base() { }
        public GetTransparentSlaveParameterRequest(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// Index number of referred transparent slave parameter
        /// </summary>
        public byte Index { get; set; }

        protected override uint GetRawDataCore() => ProcessRequest(Index);
        protected override void SetRawDataCore(uint value)
        {
            Index = Utilities.GetLowByte(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.TSPindexTSPvalue;
    }
}
