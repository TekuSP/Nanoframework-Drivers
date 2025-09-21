using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Request class for retrieving RF sensor status information.
    /// </summary>
    /// <remarks>
    /// Request payload low byte selects the sensor ID to query.
    /// </remarks>
    public class GetRFsensorStatusInformationRequest : ReadRequest
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRFsensorStatusInformationRequest"/> class.
        /// </summary>
        public GetRFsensorStatusInformationRequest() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetRFsensorStatusInformationRequest"/> class
        /// with a base request.
        /// </summary>
        /// <param name="baseReq">The base request.</param>
        public GetRFsensorStatusInformationRequest(Request baseReq) : base(baseReq) { }

        #endregion Public Constructors

        #region Public Properties

    public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.RFsensorStatusInformationResponse);

        /// <summary>
        /// Gets the message ID for the request.
        /// </summary>
        public override MessageID MessageID => MessageID.RFsensorStatusInformation;

        /// <summary>
        /// Gets the message type for the request.
        /// </summary>
        public override MessageType MessageType => MessageType.READ_DATA;

        /// <summary>
        /// RF sensor identifier (low byte), selecting which sensor’s status to query.
        /// </summary>
        public byte SensorId { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessRequest(SensorId);

        protected override void SetRawDataCore(uint value)
        { SensorId = Utilities.GetLowByte(value); }

        #endregion Protected Methods
    }
}