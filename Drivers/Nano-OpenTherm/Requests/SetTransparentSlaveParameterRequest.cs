using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Writes a transparent slave parameter (generic domain). ID=11.
    /// High byte: TSP index; Low byte: parameter value (encoding is TSP-specific).
    /// </summary>
    public class SetTransparentSlaveParameterRequest : WriteRequest
    {
        public SetTransparentSlaveParameterRequest() : base() { }
        public SetTransparentSlaveParameterRequest(Request baseReq) : base(baseReq) { }

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.TransparentSlaveParameterResponse);

        public override MessageID MessageID => MessageID.TSPindexTSPvalue;
        public override MessageType MessageType => MessageType.WRITE_DATA;

        /// <summary>TSP index to write (0..255), placed in the high data byte.</summary>
        public byte Index { get; set; }

    /// <summary>Raw value to write (placed in low data byte). Encoding is parameter-specific.</summary>
        public ushort Value { get; set; }

        protected override uint GetRawDataCore()
        {
            // Compose as: HB = Index, LB = low byte of Value. Some TSPs use only LB; keep full ushort for flexibility.
            return ProcessRequest(Utilities.MakeUShort(Index, (byte)(Value & 0xFF)));
        }

        protected override void SetRawDataCore(uint value)
        {
            Index = Utilities.GetHighByte(value);
            Value = Utilities.GetLowUShort(value);
        }
    }
}
