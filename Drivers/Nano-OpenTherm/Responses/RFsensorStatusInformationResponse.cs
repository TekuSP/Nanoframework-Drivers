using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// RF sensor status: high byte = RF strength (%), low byte = battery level (%).
    /// </summary>
    public class RFsensorStatusInformationResponse : Response
    {
        public RFsensorStatusInformationResponse(MessageType mt = MessageType.READ_ACK) { MessageType = mt; }
        public RFsensorStatusInformationResponse(Response r) : base(r) { }

        public override MessageID MessageID => MessageID.RFsensorStatusInformation;

        /// <summary>
        /// RF signal strength in percent (0-100). Stored in high byte of payload.
        /// </summary>
        public byte RFStrengthPercent { get; set; }

        /// <summary>
        /// Battery level in percent (0-100). Stored in low byte of payload.
        /// </summary>
        public byte BatteryLevelPercent { get; set; }

        protected override uint GetRawDataCore()
        {
            ushort data = Utilities.MakeUShort(RFStrengthPercent, BatteryLevelPercent);
            return ProcessResponse(data);
        }

        protected override void SetRawDataCore(uint value)
        {
            RFStrengthPercent = Utilities.GetHighByte(value);
            BatteryLevelPercent = Utilities.GetLowByte(value);
        }

        public override MessageType MessageType { get; set; }
    }
}
