using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses
{
    /// <summary>
    /// Base for 16-bit value responses (payload is a single ushort).
    /// </summary>
    public abstract class UShortValueResponseBase : Response
    {
        #region Protected Constructors

        protected UShortValueResponseBase()
        { }

        protected UShortValueResponseBase(Response baseResponse) : base(baseResponse)
        {
        }

        #endregion Protected Constructors

        #region Public Properties

        public override MessageType MessageType { get; set; }
        public ushort Value { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override uint GetRawDataCore() => ProcessResponse(Value);

        protected override void SetRawDataCore(uint value) => Value = Utilities.GetLowUShort(value);

        #endregion Protected Methods
    }
}