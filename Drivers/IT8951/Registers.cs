using System;


namespace IT8951
{
    public enum Registers
    {
        DISPLAY_REG_BASE = 0x1000,               //Register RW access for I80 only
        //Base Address of Basic LUT Registers
        LUT0EWHR = (DISPLAY_REG_BASE + 0x00),   //LUT0 Engine Width Height Reg
        LUT0XYR = (DISPLAY_REG_BASE + 0x40),  //LUT0 XY Reg
        LUT0BADDR = (DISPLAY_REG_BASE + 0x80),  //LUT0 Base Address Reg
        LUT0MFN = (DISPLAY_REG_BASE + 0xC0),   //LUT0 Mode and Frame number Reg
        LUT01AF = (DISPLAY_REG_BASE + 0x114),  //LUT0 and LUT1 Active Flag Reg
                                               //Update Parameter Setting Register
        UP0SR = (DISPLAY_REG_BASE + 0x134),      //Update Parameter0 Setting Reg

        UP1SR = (DISPLAY_REG_BASE + 0x138),  //Update Parameter1 Setting Reg
        LUT0ABFRV = (DISPLAY_REG_BASE + 0x13C),  //LUT0 Alpha blend and Fill rectangle Value
        UPBBADDR = (DISPLAY_REG_BASE + 0x17C), //Update Buffer Base Address
        LUT0IMXY = (DISPLAY_REG_BASE + 0x180),  //LUT0 Image buffer X/Y offset Reg
        LUTAFSR = (DISPLAY_REG_BASE + 0x224),  //LUT Status Reg (status of All LUT Engines)

        BGVR = (DISPLAY_REG_BASE + 0x250),  //Bitmap (1bpp) image color table
                                            //-------System Registers----------------
        SYS_REG_BASE = 0x0000,

        //Address of System Registers
        I80CPCR = (SYS_REG_BASE + 0x04),
        //-------Memory Converter Registers----------------
        MCSR_BASE_ADDR = 0x0200,
        MCSR = (MCSR_BASE_ADDR + 0x0000),
        LISAR = (MCSR_BASE_ADDR + 0x0008)
    }
}
