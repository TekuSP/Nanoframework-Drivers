using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// Solar Storage: Slave Configuration Flags and Member ID Code response.
    /// </summary>
    /// <remarks>
    /// Low byte contains <see cref="SlaveConfiguration"/> flags; high byte contains <see cref="MemberIdCode"/>.
    /// This type can be constructed for sending (ACK/error), or instantiated from a received frame for parsing.
    /// </remarks>
    public class SolarStorageSConfigResponse : Response
    {
        /// <summary>
        /// Initializes a new response for constructing a frame to send.
        /// </summary>
        /// <param name="messageType">Optional message type to use. Defaults to <see cref="TekuSP.Drivers.Nano_OpenTherm.Enums.MessageType.READ_ACK"/>.</param>
        public SolarStorageSConfigResponse(MessageType messageType = MessageType.READ_ACK)
        {
            MessageType = messageType;
        }
        /// <summary>
        /// Wrap a received frame and expose properties for flags and member ID.
        /// </summary>
        public SolarStorageSConfigResponse(Response baseResponse)
        {
            MessageType = baseResponse.MessageType;
            SetRawDataCore(baseResponse.RawData);
        }

        protected override uint GetRawDataCore()
        {
            var low = Utilities.SetSlaveConfiguration(SlaveConfiguration);
            ushort payload = Utilities.MakeUShort((byte)MemberIdCode, low);
            return ProcessResponse(payload);
        }
        protected override void SetRawDataCore(uint value)
        {
            SlaveConfiguration = Utilities.GetSlaveConfiguration(value);
            MemberIdCode = (MemberIdCode)Utilities.GetHighByte(value);
        }

        /// <inheritdoc />
        public override MessageType MessageType { get; set; }

        /// <inheritdoc />
        public override MessageID MessageID => MessageID.SConfigSMemberIDcodeSolarStorage;

        /// <summary>
        /// Slave configuration flags (low byte).
        /// </summary>
        public SlaveConfiguration SlaveConfiguration { get; set; }
        /// <summary>
        /// Manufacturer/member ID code (high byte).
        /// </summary>
        public MemberIdCode MemberIdCode { get; set; }

        // Convenience bit properties mirroring the request type
        /// <summary>Domestic Hot Water present on slave.</summary>
        public bool DHWPresent { get => SlaveConfiguration.IsSet(SlaveConfiguration.DHWPresent); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SlaveConfiguration.DHWPresent, value); }
        /// <summary>Control type flag.</summary>
        public bool ControlType { get => SlaveConfiguration.IsSet(SlaveConfiguration.ControlType); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SlaveConfiguration.ControlType, value); }
        /// <summary>Cooling capability configured on slave.</summary>
        public bool CoolingConfig { get => SlaveConfiguration.IsSet(SlaveConfiguration.CoolingConfig); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SlaveConfiguration.CoolingConfig, value); }
        /// <summary>Domestic Hot Water configuration bit on slave.</summary>
        public bool DHWConfig { get => SlaveConfiguration.IsSet(SlaveConfiguration.DHWConfig); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SlaveConfiguration.DHWConfig, value); }
        /// <summary>Master low/off pump control supported.</summary>
        public bool MasterLowOffPumpControl { get => SlaveConfiguration.IsSet(SlaveConfiguration.MasterLowOffPumpControl); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SlaveConfiguration.MasterLowOffPumpControl, value); }
        /// <summary>Second central heating circuit present on slave.</summary>
        public bool CH2Present { get => SlaveConfiguration.IsSet(SlaveConfiguration.CH2Present); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SlaveConfiguration.CH2Present, value); }
        /// <summary>Reserved bit 6.</summary>
        public bool Reserved6 { get => SlaveConfiguration.IsSet(SlaveConfiguration.Reserved6); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SlaveConfiguration.Reserved6, value); }
        /// <summary>Reserved bit 7.</summary>
        public bool Reserved7 { get => SlaveConfiguration.IsSet(SlaveConfiguration.Reserved7); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SlaveConfiguration.Reserved7, value); }
    }
}
