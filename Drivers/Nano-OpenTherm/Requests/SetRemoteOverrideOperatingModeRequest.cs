using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Remote override operating modes (HC1/HC2/DHW)
    /// </summary>
    public class SetRemoteOverrideOperatingModeRequest : WriteRequest
    {
        public SetRemoteOverrideOperatingModeRequest() : base() { }
        public SetRemoteOverrideOperatingModeRequest(Request baseReq) : base(baseReq) { }

        // Strongly-typed per-channel operating modes (2-bit fields in low byte)
        public OperatingMode HC1 { get; set; }
        public OperatingMode HC2 { get; set; }
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
        public bool HC1Off { get => HC1 == OperatingMode.Off; set { if (value) HC1 = OperatingMode.Off; } }
        public bool HC1Auto { get => HC1 == OperatingMode.Auto; set { if (value) HC1 = OperatingMode.Auto; } }
        public bool HC1Manual { get => HC1 == OperatingMode.Manual; set { if (value) HC1 = OperatingMode.Manual; } }
        public bool HC1Reserved { get => HC1 == OperatingMode.Reserved; set { if (value) HC1 = OperatingMode.Reserved; } }

        public bool HC2Off { get => HC2 == OperatingMode.Off; set { if (value) HC2 = OperatingMode.Off; } }
        public bool HC2Auto { get => HC2 == OperatingMode.Auto; set { if (value) HC2 = OperatingMode.Auto; } }
        public bool HC2Manual { get => HC2 == OperatingMode.Manual; set { if (value) HC2 = OperatingMode.Manual; } }
        public bool HC2Reserved { get => HC2 == OperatingMode.Reserved; set { if (value) HC2 = OperatingMode.Reserved; } }

        public bool DHWOff { get => DHW == OperatingMode.Off; set { if (value) DHW = OperatingMode.Off; } }
        public bool DHWAuto { get => DHW == OperatingMode.Auto; set { if (value) DHW = OperatingMode.Auto; } }
        public bool DHWManual { get => DHW == OperatingMode.Manual; set { if (value) DHW = OperatingMode.Manual; } }
        public bool DHWReserved { get => DHW == OperatingMode.Reserved; set { if (value) DHW = OperatingMode.Reserved; } }
    }
}
