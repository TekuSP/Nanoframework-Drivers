using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using TekuSP.Drivers.Nano_OpenTherm.Interfaces;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the ventilation/heat-recovery product type and version bytes.
    /// </summary>
    public class GetVentilationVersionRequest : ReadRequest, IVersionProductType
    {
        #region Public Constructors

        public GetVentilationVersionRequest() : base()
        {
        }

        public GetVentilationVersionRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        // IVersionProductType
        /// <summary>Product is a Boiler.</summary>
        public bool IsBoiler
        { get => ProductType == VersionProductType.Boiler; set { if (value) ProductType = VersionProductType.Boiler; } }

        /// <summary>Product is a Controller.</summary>
        public bool IsController
        { get => ProductType == VersionProductType.Controller; set { if (value) ProductType = VersionProductType.Controller; } }

        /// <summary>Product is a Heat Pump.</summary>
        public bool IsHeatPump
        { get => ProductType == VersionProductType.HeatPump; set { if (value) ProductType = VersionProductType.HeatPump; } }

        /// <summary>Product is a Sensor.</summary>
        public bool IsSensor
        { get => ProductType == VersionProductType.Sensor; set { if (value) ProductType = VersionProductType.Sensor; } }

        /// <summary>Product is Unknown.</summary>
        public bool IsUnknown
        { get => ProductType == VersionProductType.Unknown; set { if (value) ProductType = VersionProductType.Unknown; } }

        /// <summary>Product is a Ventilation unit.</summary>
        public bool IsVentilation
        { get => ProductType == VersionProductType.Ventilation; set { if (value) ProductType = VersionProductType.Ventilation; } }

        public override MessageID MessageID => MessageID.VentilationHeatRecoveryVersion;

        public override MessageType MessageType => MessageType.READ_DATA;

        /// <summary>
        /// Product firmware/hardware version (high byte).
        /// </summary>
        public byte Version { get; set; }

        #endregion Public Properties

        #region Protected Properties

        /// <summary>
        /// Product type identifier (low byte). OEM-specific mapping.
        /// </summary>
        protected VersionProductType ProductType { get; set; }

        #endregion Protected Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
        {
            ushort payload = Utilities.MakeUShort(Version, (byte)ProductType);
            return ProcessRequest(payload);
        }

        protected override void SetRawDataCore(uint value)
        {
            Version = Utilities.GetHighByte(value);
            ProductType = (VersionProductType)Utilities.GetLowByte(value);
        }

        #endregion Protected Methods
    }
}