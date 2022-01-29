using System;


namespace IT8951
{
    public struct DeviceInfo
    {
        ushort PanelWidth;
        ushort PanelHeight;
        ushort BufferAddressL;
        ushort BufferAddressH;
        ushort[] FWVersion;   //16 Bytes String
        ushort[] LUTVersion;   //16 Bytes String
    }
}
