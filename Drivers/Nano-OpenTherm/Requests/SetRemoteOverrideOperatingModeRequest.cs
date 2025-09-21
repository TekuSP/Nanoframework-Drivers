using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using TekuSP.Drivers.Nano_OpenTherm.Interfaces;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Sets remote override operating modes for HC1, HC2, and DHW.
    /// </summary>
    public class SetRemoteOverrideOperatingModeRequest : WriteRequest, IOperatingMode, IOperatingModeHC2, IOperatingModeDHW
    {
        #region Public Constructors

        public SetRemoteOverrideOperatingModeRequest() : base()
        {
        }

        public SetRemoteOverrideOperatingModeRequest(Request baseReq) : base(baseReq)
        {
        }

        /// <summary>
        /// Convenience constructor to initialize DHW, HC1 and HC2 operating modes.
        /// </summary>
        public SetRemoteOverrideOperatingModeRequest(OperatingMode hc1, OperatingMode hc2, OperatingMode dhw)
        {
            HC1 = hc1;
            HC2 = hc2;
            DHW = dhw;
        }

        #endregion Public Constructors

        #region Public Properties

    public override System.Type ExpectedResponse => typeof(TekuSP.Drivers.Nano_OpenTherm.Responses.RemoteOverrideOperatingModeResponse);

        // IOperatingModeDHW
        public bool DHWModeIsAuto
        { get => DHW == OperatingMode.Auto; set { if (value) DHW = OperatingMode.Auto; } }

        public bool DHWModeIsManual
        { get => DHW == OperatingMode.Manual; set { if (value) DHW = OperatingMode.Manual; } }

        public bool DHWModeIsOff
        { get => DHW == OperatingMode.Off; set { if (value) DHW = OperatingMode.Off; } }

        public bool DHWModeIsReserved
        { get => DHW == OperatingMode.Reserved; set { if (value) DHW = OperatingMode.Reserved; } }

        // IOperatingModeHC2
        public bool HC2ModeIsAuto
        { get => HC2 == OperatingMode.Auto; set { if (value) HC2 = OperatingMode.Auto; } }

        public bool HC2ModeIsManual
        { get => HC2 == OperatingMode.Manual; set { if (value) HC2 = OperatingMode.Manual; } }

        public bool HC2ModeIsOff
        { get => HC2 == OperatingMode.Off; set { if (value) HC2 = OperatingMode.Off; } }

        public bool HC2ModeIsReserved
        { get => HC2 == OperatingMode.Reserved; set { if (value) HC2 = OperatingMode.Reserved; } }

        public override MessageID MessageID => MessageID.RemoteOverrideOperatingModeHeatingDHW;

        public override MessageType MessageType => MessageType.WRITE_DATA;

        // IOperatingMode (mapped to HC1 channel) — interface-only surface
        public bool ModeIsAuto
        { get => HC1 == OperatingMode.Auto; set { if (value) HC1 = OperatingMode.Auto; } }

        public bool ModeIsManual
        { get => HC1 == OperatingMode.Manual; set { if (value) HC1 = OperatingMode.Manual; } }

        public bool ModeIsOff
        { get => HC1 == OperatingMode.Off; set { if (value) HC1 = OperatingMode.Off; } }

        public bool ModeIsReserved
        { get => HC1 == OperatingMode.Reserved; set { if (value) HC1 = OperatingMode.Reserved; } }

        #endregion Public Properties

        #region Protected Properties

        protected OperatingMode DHW { get; set; }

        // Single source of truth: protected enum auto-properties per channel (2-bit fields in low byte)
        protected OperatingMode HC1 { get; set; }

        protected OperatingMode HC2 { get; set; }

        #endregion Protected Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
        {
            // Pack 3x 2-bit modes into low byte using Utilities: HC1=b0..1, HC2=b3..4, DHW=b6..7
            byte low = Utilities.SetOperatingModes(HC1, HC2, DHW);
            // Use Utilities to compose the 16-bit payload (high byte is 0)
            ushort payload = Utilities.MakeUShort(0, low);
            return ProcessRequest(payload);
        }

        protected override void SetRawDataCore(uint value)
        {
            HC1 = Utilities.GetOperatingModeHC1(value);
            HC2 = Utilities.GetOperatingModeHC2(value);
            DHW = Utilities.GetOperatingModeDHW(value);
        }

        #endregion Protected Methods
    }
}