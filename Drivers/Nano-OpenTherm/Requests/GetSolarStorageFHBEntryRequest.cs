using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads an entry from the Solar Storage Fault History Buffer (FHB).
    /// Packs HB=index and LB=0 per v2.2 (FHB read: index in high byte).
    /// </summary>
    /// <remarks>
    /// The <see cref="Index"/> is sent in the low byte of the payload to select
    /// which FHB entry to read. The response encodes the entry value in the low 16 bits
    /// (OEM-specific format).
    /// </remarks>
    public class GetSolarStorageFHBEntryRequest : ReadRequest
    {
        #region Public Constructors

        public GetSolarStorageFHBEntryRequest() : base()
        {
        }

        public GetSolarStorageFHBEntryRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.SolarStorageFHBEntryResponse);

        /// <summary>
        /// Fault history buffer index to read (0-based). Encoded in the high data byte.
        /// </summary>
        public byte Index { get; set; }

        public override MessageID MessageID => MessageID.FHBindexFHBvalueSolarStorage;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(Utilities.MakeUShort(Index, 0));

        protected override void SetRawDataCore(uint value)
        { Index = Utilities.GetLowByte(value); }

        #endregion Protected Methods
    }
}