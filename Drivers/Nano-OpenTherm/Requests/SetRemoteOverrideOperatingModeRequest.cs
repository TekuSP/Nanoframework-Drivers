using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Sets remote override operating modes for HC1, HC2, and DHW.
    /// </summary>
    public class SetRemoteOverrideOperatingModeRequest : WriteRequest
    {
        public SetRemoteOverrideOperatingModeRequest() : base() { }
        public SetRemoteOverrideOperatingModeRequest(Request baseReq) : base(baseReq) { }

        // Strongly-typed per-channel operating modes (2-bit fields in low byte)
    /// <summary>
        /// Remote override operating mode for Heating Circuit 1 (bits 0..1 of low byte).
    /// </summary>
    public OperatingMode HC1 { get; set; }
    /// <summary>
        /// Remote override operating mode for Heating Circuit 2 (bits 3..4 of low byte).
    /// </summary>
    public OperatingMode HC2 { get; set; }
    /// <summary>
        /// Remote override operating mode for Domestic Hot Water (bits 6..7 of low byte).
    /// </summary>
    public OperatingMode DHW { get; set; }

        protected override uint GetRawDataCore()
        {
            // Pack 3x 2-bit modes into low byte: HC1=b0..1, HC2=b3..4, DHW=b6..7
            byte low = (byte)(((byte)HC1 & 0x03)
                             | (((byte)HC2 & 0x03) << 3)
                             | (((byte)DHW & 0x03) << 6));
            return ProcessRequest(low);
        }
        protected override void SetRawDataCore(uint value)
        {
            var b = Utilities.GetLowByte(value);
            HC1 = (OperatingMode)(b & 0x03);
            HC2 = (OperatingMode)((b >> 3) & 0x03);
            DHW = (OperatingMode)((b >> 6) & 0x03);
        }

        public override MessageType MessageType => MessageType.WRITE_DATA;
        public override MessageID MessageID => MessageID.RemoteOverrideOperatingModeHeatingDHW;

    // Convenience properties describing the enum values explicitly
    /// <summary>Set HC1 to Off.</summary>
    public bool HC1Off { get => HC1 == OperatingMode.Off; set { if (value) HC1 = OperatingMode.Off; } }
    /// <summary>Set HC1 to Auto.</summary>
    public bool HC1Auto { get => HC1 == OperatingMode.Auto; set { if (value) HC1 = OperatingMode.Auto; } }
    /// <summary>Set HC1 to Manual.</summary>
    public bool HC1Manual { get => HC1 == OperatingMode.Manual; set { if (value) HC1 = OperatingMode.Manual; } }
    /// <summary>Set HC1 to Reserved.</summary>
    public bool HC1Reserved { get => HC1 == OperatingMode.Reserved; set { if (value) HC1 = OperatingMode.Reserved; } }

    /// <summary>Set HC2 to Off.</summary>
    public bool HC2Off { get => HC2 == OperatingMode.Off; set { if (value) HC2 = OperatingMode.Off; } }
    /// <summary>Set HC2 to Auto.</summary>
    public bool HC2Auto { get => HC2 == OperatingMode.Auto; set { if (value) HC2 = OperatingMode.Auto; } }
    /// <summary>Set HC2 to Manual.</summary>
    public bool HC2Manual { get => HC2 == OperatingMode.Manual; set { if (value) HC2 = OperatingMode.Manual; } }
    /// <summary>Set HC2 to Reserved.</summary>
    public bool HC2Reserved { get => HC2 == OperatingMode.Reserved; set { if (value) HC2 = OperatingMode.Reserved; } }

    /// <summary>Set DHW to Off.</summary>
    public bool DHWOff { get => DHW == OperatingMode.Off; set { if (value) DHW = OperatingMode.Off; } }
    /// <summary>Set DHW to Auto.</summary>
    public bool DHWAuto { get => DHW == OperatingMode.Auto; set { if (value) DHW = OperatingMode.Auto; } }
    /// <summary>Set DHW to Manual.</summary>
    public bool DHWManual { get => DHW == OperatingMode.Manual; set { if (value) DHW = OperatingMode.Manual; } }
    /// <summary>Set DHW to Reserved.</summary>
    public bool DHWReserved { get => DHW == OperatingMode.Reserved; set { if (value) DHW = OperatingMode.Reserved; } }
    }
}
