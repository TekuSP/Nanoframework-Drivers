using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads a ventilation fault-history buffer entry by index.
    /// Packs HB=index and LB=0 per v2.2 (FHB read: index in high byte).
    /// </summary>
    /// <remarks>
    /// The <see cref="Index"/> is encoded in the low byte. The response payload contains the entry value
    /// in the low 16 bits (OEM-specific encoding).
    /// </remarks>
    public class GetVentilationFHBEntryRequest : ReadRequest
    {
        #region Public Constructors

        public GetVentilationFHBEntryRequest() : base()
        {
        }

        public GetVentilationFHBEntryRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
    /// Index of ventilation fault-history entry to read (0-based). Encoded in the high data byte.
        /// </summary>
        public byte Index { get; set; }

        public override MessageID MessageID => MessageID.FHBindexFHBvalueVentilationHeatRecovery;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

    protected override uint GetRawDataCore() => ProcessRequest(Utilities.MakeUShort(Index, 0));

        protected override void SetRawDataCore(uint value)
        { Index = Utilities.GetLowByte(value); }

        #endregion Protected Methods
    }
}