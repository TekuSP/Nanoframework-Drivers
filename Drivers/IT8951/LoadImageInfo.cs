using System;


namespace IT8951
{
    public struct LoadImageInfo
    {
        ushort EndianType; //little or Big Endian
        ushort PixelFormat; //bpp
        ushort Rotate; //Rotate mode
        uint StartFrameBufferAddress; //Start address of source Frame buffer
        uint ImageBufferBaseAddress;//Base address of target image buffer
    }
}
