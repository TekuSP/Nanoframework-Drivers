using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Writes a ventilation heat recovery TSP value by index.
    /// High byte: TSP index; Low byte: parameter value.
    /// </summary>
    public class SetVentilationTSPRequest : WriteRequest
    {
        public SetVentilationTSPRequest() : base() { }
        public SetVentilationTSPRequest(Request baseReq) : base(baseReq) { }

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.VentilationTSPValueResponse);

        public override MessageID MessageID => MessageID.TSPindexTSPvalueVentilationHeatRecovery;
        public override MessageType MessageType => MessageType.WRITE_DATA;

        /// <summary>TSP index to write.</summary>
        public byte Index { get; set; }
    /// <summary>Raw value to write (placed in low data byte).</summary>
        public ushort Value { get; set; }

        protected override uint GetRawDataCore()
        {
            return ProcessRequest(Utilities.MakeUShort(Index, (byte)(Value & 0xFF)));
        }

        protected override void SetRawDataCore(uint value)
        {
            Index = Utilities.GetHighByte(value);
            Value = Utilities.GetLowUShort(value);
        }
    }
}
