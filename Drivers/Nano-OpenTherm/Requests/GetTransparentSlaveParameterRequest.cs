using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class GetTransparentSlaveParameterRequest : ReadRequest
    {
        public GetTransparentSlaveParameterRequest() : base() { }
        public GetTransparentSlaveParameterRequest(Request baseReq) : base(baseReq) { }

        /// <summary>
        /// Index number of referred transparent slave parameter
        /// </summary>
        public byte Index { get; set; }

        protected override ulong GetRawDataCore() => ProcessRequest(Index);
        protected override void SetRawDataCore(ulong value)
        {
            Index = Utilities.GetLowByte(value);
        }

        public override MessageType MessageType => MessageType.READ_DATA;
        public override MessageID MessageID => MessageID.TSPindexTSPvalue;
    }
}
