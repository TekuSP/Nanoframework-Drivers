using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using TekuSP.Drivers.Nano_OpenTherm.Interfaces;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses.BaseResponses
{
    /// <summary>
    /// Base for Product version/type responses. High byte = version, low nibble of low byte = product type.
    /// </summary>
    public abstract class ProductVersionTypeResponseBase : Response, IVersionProductType
    {
    protected ProductVersionTypeResponseBase() { }
    protected ProductVersionTypeResponseBase(Response baseResponse) : base(baseResponse) { }
        public byte ProductVersion { get; set; }
        protected VersionProductType ProductType { get; set; }

        protected override void SetRawDataCore(uint value)
        {
            ProductVersion = Utilities.GetHighByte(value);
            ProductType = (VersionProductType)(Utilities.GetLowByte(value) & 0x0F);
        }
        protected override uint GetRawDataCore()
        {
            byte low = (byte)((byte)ProductType & 0x0F);
            ushort payload = Utilities.MakeUShort(ProductVersion, low);
            return ProcessResponse(payload);
        }

        public override MessageType MessageType { get; set; }

        // IVersionProductType convenience selectors
        public bool IsBoiler { get => ProductType == VersionProductType.Boiler; set { if (value) ProductType = VersionProductType.Boiler; } }
        public bool IsController { get => ProductType == VersionProductType.Controller; set { if (value) ProductType = VersionProductType.Controller; } }
        public bool IsHeatPump { get => ProductType == VersionProductType.HeatPump; set { if (value) ProductType = VersionProductType.HeatPump; } }
        public bool IsSensor { get => ProductType == VersionProductType.Sensor; set { if (value) ProductType = VersionProductType.Sensor; } }
        public bool IsUnknown { get => ProductType == VersionProductType.Unknown; set { if (value) ProductType = VersionProductType.Unknown; } }
        public bool IsVentilation { get => ProductType == VersionProductType.Ventilation; set { if (value) ProductType = VersionProductType.Ventilation; } }
    }
}
