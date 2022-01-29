using System;

namespace IT8951
{
    public enum Commands
    {//Built in I80 Command Code
        IT8951_TCON_SYS_RUN = 0x0001,
        IT8951_TCON_STANDBY = 0x0002,
        IT8951_TCON_SLEEP = 0x0003,
        IT8951_TCON_REG_RD = 0x0010,
        IT8951_TCON_REG_WR = 0x0011,
        IT8951_TCON_MEM_BST_RD_T = 0x0012,
        IT8951_TCON_MEM_BST_RD_S = 0x0013,
        IT8951_TCON_MEM_BST_WR = 0x0014,
        IT8951_TCON_MEM_BST_END = 0x0015,
        IT8951_TCON_LD_IMG = 0x0020,
        IT8951_TCON_LD_IMG_AREA = 0x0021,
        IT8951_TCON_LD_IMG_END = 0x0022,

        //I80 User defined command code
        USDEF_I80_CMD_DPY_AREA = 0x0034,
        USDEF_I80_CMD_GET_DEV_INFO = 0x0302,
        USDEF_I80_CMD_DPY_BUF_AREA = 0x0037,
        //Panel
        IT8951_PANEL_WIDTH = 1024, //it Get Device information
        IT8951_PANEL_HEIGHT = 758,

        //Rotate mode
        IT8951_ROTATE_0 = 0,
        IT8951_ROTATE_90 = 1,
        IT8951_ROTATE_180 = 2,
        IT8951_ROTATE_270 = 3,

        //Pixel mode , BPP - Bit per Pixel
        IT8951_2BPP = 0,
        IT8951_3BPP = 1,
        IT8951_4BPP = 2,
        IT8951_8BPP = 3,

        //Waveform Mode
        IT8951_MODE_0 = 0,
        IT8951_MODE_1 = 1,
        IT8951_MODE_2 = 2,
        IT8951_MODE_3 = 3,
        IT8951_MODE_4 = 4,
        //Endian Type
        IT8951_LDIMG_L_ENDIAN = 0,
        IT8951_LDIMG_B_ENDIAN = 1,
        //Auto LUT
        IT8951_DIS_AUTO_LUT = 0,
        IT8951_EN_AUTO_LUT = 1,
        //LUT Engine Status
        IT8951_ALL_LUTE_BUSY = 0xFFFF
    }
}
