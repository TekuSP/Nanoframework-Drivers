using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Writes a solar storage TSP value by index.
    /// High byte: TSP index; Low byte: parameter value.
    /// </summary>
    public class SetSolarStorageTSPRequest : WriteRequest
    {
        #region Public Constructors

        public SetSolarStorageTSPRequest() : base()
        {
        }

        public SetSolarStorageTSPRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.SolarStorageTSPValueResponse);

        /// <summary>TSP index to write.</summary>
        public byte Index { get; set; }

        public override MessageID MessageID => MessageID.TSPindexTSPvalueSolarStorage;
        public override MessageType MessageType => MessageType.WRITE_DATA;

        /// <summary>Raw value to write (placed in low data byte).</summary>
        public ushort Value { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
        {
            return ProcessRequest(Utilities.MakeUShort(Index, (byte)(Value & 0xFF)));
        }

        protected override void SetRawDataCore(uint value)
        {
            Index = Utilities.GetHighByte(value);
            Value = Utilities.GetLowUShort(value);
        }

        #endregion Protected Methods
    }
}