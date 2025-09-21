using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads a specific fault-history buffer entry by index from the slave.
    /// Packs HB=index and LB=0 per v2.2 (FHB read: index in high byte).
    /// </summary>
    public class GetFaultHistoryBufferEntryRequest : ReadRequest
    {
        public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.FaultHistoryBufferEntryResponse);
        #region Public Constructors

        public GetFaultHistoryBufferEntryRequest() : base()
        {
        }

        public GetFaultHistoryBufferEntryRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
    /// Index number of fault-history buffer entry to read (encoded in the high data byte).
        /// </summary>
        public byte Index { get; set; }

        public override MessageID MessageID => MessageID.FHBindexFHBvalue;

        public override MessageType MessageType => MessageType.READ_DATA;

        #endregion Public Properties

        #region Protected Methods

    protected override uint GetRawDataCore() => ProcessRequest(Utilities.MakeUShort(Index, 0));

        protected override void SetRawDataCore(uint value)
        {
            Index = Utilities.GetLowByte(value);
        }

        #endregion Protected Methods
    }
}