using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Vendor-specific Remeha Data-ID 133 request supporting both READ_DATA and WRITE_DATA.
    /// Payload semantics are OEM-defined; exposes a raw 16-bit <see cref="Value"/>.
    /// </summary>
    public class Remeha133Request : ReadWriteRequest
    {
        public Remeha133Request(MessageType messageType) : base(messageType)
        {  }

        public Remeha133Request(Request baseReq) : base(baseReq, baseReq.MessageType)
        {  }

        public override MessageID MessageID => MessageID.Remeha133;
        public override MessageType MessageType { get; }

        /// <summary>Raw 16-bit payload to send/receive.</summary>
        public ushort Value { get; set; }

        protected override uint GetRawDataCore() => ProcessRequest(Value);
        protected override void SetRawDataCore(uint value)
        { Value = (ushort)(value & 0xFFFF); }
    }
}
