using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads a Transparent Slave Parameter (TSP) value by index from the Solar Storage.
    /// Packs HB=index and LB=0 per v2.2 (TSP read: index in high byte).
    /// </summary>
    /// <remarks>
    /// The <see cref="Index"/> is encoded in the low byte of the request payload. The response
    /// carries the TSP value in the low 16 bits (encoding is parameter-specific).
    /// </remarks>
    public class GetSolarStorageTSPRequest : ReadRequest
    {
        #region Public Constructors

        public GetSolarStorageTSPRequest() : base()
        {
        }

        public GetSolarStorageTSPRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
    /// TSP index to read (0-based). Encoded in the high data byte.
        /// </summary>
        public byte Index { get; set; }

        public override MessageID MessageID => MessageID.TSPindexTSPvalueSolarStorage;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

    protected override uint GetRawDataCore() => ProcessRequest(Utilities.MakeUShort(Index, 0));

        protected override void SetRawDataCore(uint value)
        { Index = Utilities.GetLowByte(value); }

        #endregion Protected Methods
    }
}