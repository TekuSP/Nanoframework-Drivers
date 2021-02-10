using DriverBase;
using DriverBase.Enums;
using DriverBase.Helpers;
using System.Threading;
using Windows.Devices.Gpio;
using Windows.Devices.Spi;

namespace SSD1331
{
    /// <summary>
    /// Driver for Solomon Systech (Adafruit) SSD 1331, <a href="https://cdn-shop.adafruit.com/datasheets/SSD1331_1.2.pdf"/>
    /// </summary>
    public class SSD1331 : DriverBaseSPI
    {
        #region Protected Fields

        //If dcPin is high, data is written to Graphic Display Data RAM (GDDRAM). If it is low, the inputs at D0-D15 are interpreted as a Command and it will be decoded and be written to the corresponding command register.
        protected GpioPin dcPin; //Command Decoder Interface, digital PIN
        protected int dcPinInt;
        protected GpioController gpio;
        protected GpioPin chipSelectPin;
        protected GpioPin rstPin;
        protected int rstPinInt;

        #endregion Protected Fields

        #region Public Constructors

        public SSD1331(string SPIBusID, int chipSelectPin, int commandDecoderPin, int resetPin, GpioController gpioController) : base("SSD1331", SPIBusID, chipSelectPin)
        {
            dcPinInt = commandDecoderPin;
            rstPinInt = resetPin;
            gpio = gpioController;
            Height = 96;
            Width = 64;
        }

        public SSD1331(string SPIBusID, SpiConnectionSettings spiConnectionSettings, int commandDecoderPin, int resetPin, GpioController gpioController) : base("SSD1331", SPIBusID, spiConnectionSettings)
        {
            dcPinInt = commandDecoderPin;
            rstPinInt = resetPin;
            gpio = gpioController;
            Height = 96;
            Width = 64;
        }

        public SSD1331(ESP32_SPI SPI_BUS, int chipSelectPin, int commandDecoderPin, int resetPin, GpioController gpioController) : base("SSD1331", SPI_BUS, chipSelectPin)
        {
            dcPinInt = commandDecoderPin;
            rstPinInt = resetPin;
            gpio = gpioController;
            Height = 96;
            Width = 64;
        }

        #endregion Public Constructors

        #region Public Enums

        /// <summary>
        /// Supported display modes
        /// </summary>
        public enum DisplayModes //TODO: Move to enums?
        {
            /// <summary>
            /// Normal Display
            /// </summary>
            Normal = 0xA4,

            /// <summary>
            /// Entire Display ON, all pixels turn ON at GS63
            /// </summary>
            AllPixelsOn = 0xA5,

            /// <summary>
            /// Entire Display OFF, all pixels turn OFF
            /// </summary>
            AllPixelsOff = 0xA6,

            /// <summary>
            /// Inverse Display
            /// </summary>
            Inverse = 0xA7
        }

        /// <summary>
        /// Supported display states
        /// </summary>
        public enum DisplayState //TODO: Move to enums?
        {
            /// <summary>
            /// Display ON in dim mode
            /// </summary>
            OnDim = 0xAC,

            /// <summary>
            /// Display OFF (sleep mode)
            /// </summary>
            OFF = 0xAE,

            /// <summary>
            /// Display ON in normal mode
            /// </summary>
            ON = 0xAF
        }

        #endregion Public Enums

        #region Public Properties

        public int Height { get; }//TODO: Move to interface
        public int Width { get; }//TODO: Move to interface

        #endregion Public Properties

        #region Fundamental Commands
        //TODO, Display specific commands, think about interface?
        /// <summary>
        /// Setup Column start and end address
        /// </summary>
        /// <param name="startAddress">Start address from 0x00 to 0x5F, default 0x00</param>
        /// <param name="stopAddress">End address from 0x00 to 0x5F, default 0x5F</param>
        public void SetColumnAddress(byte startAddress = 0x00, byte stopAddress = 0x5F)
        {
            WriteCommand(15, startAddress, stopAddress);
        }

        /// <summary>
        /// MCU protection status
        /// </summary>
        /// <param name="lockCommandLock">
        /// false = Unlock OLED driver IC MCU interface from entering command [reset] <br/>
        /// true = Lock OLED driver IC MCU interface from entering command
        /// </param>
        public void SetCommandLock(bool lockCommandLock)
        {
            byte lockStatus = 12;
            lockStatus = BitHelper.SetBit(lockStatus, 1, lockCommandLock);
            WriteCommand(0xFD, lockStatus);
        }

        /// <summary>
        /// Set contrast for all color "A" segment
        /// </summary>
        /// <param name="contrast">Contrast from 0x00 to 0x80, default 0x80</param>
        public void SetContrastForAColor(byte contrast = 0x80)
        {
            WriteCommand(0x81, contrast);
        }

        /// <summary>
        /// Set contrast for all color "B" segment
        /// </summary>
        /// <param name="contrast">Contrast from 0x00 to 0x80, default 0x80</param>
        public void SetContrastForBColor(byte contrast = 0x80)
        {
            WriteCommand(0x82, contrast);
        }

        /// <summary>
        /// Set contrast for all color "C" segment
        /// </summary>
        /// <param name="contrast">Contrast from 0x00 to 0x80, default 0x80</param>
        public void SetContrastForCColor(byte contrast = 0x80)
        {
            WriteCommand(0x83, contrast);
        }

        /// <summary>
        /// Configure dim mode setting
        /// </summary>
        /// <param name="contrastSettingForColorA">Contrast setting for Color A from 0x00 to 0xFF</param>
        /// <param name="contrastSettingForColorB">Contrast setting for Color B from 0x00 to 0xFF</param>
        /// <param name="contrastSettingForColorC">Contrast setting for Color C from 0x00 to 0xFF</param>
        /// <param name="prechargeVoltageSetting">Precharge voltage setting from 0x00 to 0x1F</param>
        public void SetDimModeSetting(byte contrastSettingForColorA, byte contrastSettingForColorB, byte contrastSettingForColorC, byte prechargeVoltageSetting)
        {
            WriteCommand(0xAB, 0x00, contrastSettingForColorA, contrastSettingForColorB, contrastSettingForColorC, prechargeVoltageSetting);
        }

        /// <summary>
        /// Set Display Clock Divider / Oscillator Frequency
        /// </summary>
        /// <param name="divideRatio">
        /// Define the divide ratio (D) of the display clocks (DCLK): Divide ratio (D) = <paramref name="divideRatio"/> + 1 (i.e., 1 to 16) <br/>
        /// From 0x00 to 0x07, default 0x0D
        /// </param>
        /// <param name="foscFrequency">
        /// Fosc frequency. Frequency increases as setting value increases. <br/>
        /// From 0x00 to 0x1F, default 0x00
        /// </param>
        public void SetDisplayClockDividerOscillatorFrequency(byte divideRatio = 0x0D, byte foscFrequency = 0x00) //TODO: Verify
        {
            byte resultByte = 0; //byte magic, combine two bytes into one
            resultByte = BitHelper.SetBit(resultByte, 0, BitHelper.GetBit(divideRatio, 0));
            resultByte = BitHelper.SetBit(resultByte, 1, BitHelper.GetBit(divideRatio, 1));
            resultByte = BitHelper.SetBit(resultByte, 2, BitHelper.GetBit(divideRatio, 2));
            resultByte = BitHelper.SetBit(resultByte, 3, BitHelper.GetBit(foscFrequency, 0));
            resultByte = BitHelper.SetBit(resultByte, 4, BitHelper.GetBit(foscFrequency, 1));
            resultByte = BitHelper.SetBit(resultByte, 5, BitHelper.GetBit(foscFrequency, 2));
            resultByte = BitHelper.SetBit(resultByte, 6, BitHelper.GetBit(foscFrequency, 3));
            resultByte = BitHelper.SetBit(resultByte, 7, BitHelper.GetBit(foscFrequency, 4));
            WriteCommand(0xB3, resultByte);
        }
        /// <summary>
        /// Set Display Clock Divider / Oscillator Frequency
        /// </summary>
        /// <param name="dividerWithFosc">
        /// Define the divide ratio (D) of the display clocks (DCLK): Divide ratio (D) = <paramref name="divideRatio"/> + 1 (i.e., 1 to 16) <br/>
        /// Fosc frequency. Frequency increases as setting value increases. <br/>
        /// </param>
        public void SetDisplayClockDividerOscillatorFrequency(byte dividerWithFosc) //TODO: Verify
        {
            WriteCommand(0xB3, dividerWithFosc);
        }
        /// <summary>
        /// Set Display Mode
        /// </summary>
        /// <param name="displayMode">Display Mode to set</param>
        public void SetDisplayMode(DisplayModes displayMode = DisplayModes.Normal)
        {
            WriteCommand((byte)displayMode);
        }

        /// <summary>
        /// Set vertical offset by Com
        /// </summary>
        /// <param name="verticalOffsetByCom">Vertical offset by Com from 0x00 to 0x3F, default 0x00</param>
        public void SetDisplayOffset(byte verticalOffsetByCom = 0x00)
        {
            WriteCommand(0xA2, verticalOffsetByCom);
        }

        /// <summary>
        /// Set Display DIM/ON/OFF
        /// </summary>
        /// <param name="displayState">Display state to set, default OFF</param>
        public void SetDisplayOnOff(DisplayState displayState = DisplayState.OFF)
        {
            WriteCommand((byte)displayState);
        }

        /// <summary>
        /// Set display start line register by Row
        /// </summary>
        /// <param name="startLineRow">Start line register by Row from 0x00 to 0x3F, default 0x00</param>
        public void SetDisplayStartLine(byte startLineRow = 0x00)
        {
            WriteCommand(0xA1, startLineRow);
        }

        /// <summary>
        /// These 32 parameters define pulse widths of GS1 to GS63 in terms of DCLK
        /// </summary>
        /// <param name="GS1">Pulse width for GS1, from 0x00 to 0x7D</param>
        /// <param name="GS3">Pulse width for GS3, from 0x00 to 0x7D</param>
        /// <param name="GS5">Pulse width for GS5, from 0x00 to 0x7D</param>
        /// <param name="GS7">Pulse width for GS7, from 0x00 to 0x7D</param>
        /// <param name="GS9">Pulse width for GS9, from 0x00 to 0x7D</param>
        /// <param name="GS11">Pulse width for GS11, from 0x00 to 0x7D</param>
        /// <param name="GS13">Pulse width for GS13, from 0x00 to 0x7D</param>
        /// <param name="GS15">Pulse width for GS15, from 0x00 to 0x7D</param>
        /// <param name="GS17">Pulse width for GS17, from 0x00 to 0x7D</param>
        /// <param name="GS19">Pulse width for GS19, from 0x00 to 0x7D</param>
        /// <param name="GS21">Pulse width for GS21, from 0x00 to 0x7D</param>
        /// <param name="GS23">Pulse width for GS23, from 0x00 to 0x7D</param>
        /// <param name="GS25">Pulse width for GS25, from 0x00 to 0x7D</param>
        /// <param name="GS27">Pulse width for GS27, from 0x00 to 0x7D</param>
        /// <param name="GS29">Pulse width for GS29, from 0x00 to 0x7D</param>
        /// <param name="GS31">Pulse width for GS31, from 0x00 to 0x7D</param>
        /// <param name="GS33">Pulse width for GS33, from 0x00 to 0x7D</param>
        /// <param name="GS35">Pulse width for GS35, from 0x00 to 0x7D</param>
        /// <param name="GS37">Pulse width for GS37, from 0x00 to 0x7D</param>
        /// <param name="GS39">Pulse width for GS39, from 0x00 to 0x7D</param>
        /// <param name="GS41">Pulse width for GS41, from 0x00 to 0x7D</param>
        /// <param name="GS43">Pulse width for GS43, from 0x00 to 0x7D</param>
        /// <param name="GS45">Pulse width for GS45, from 0x00 to 0x7D</param>
        /// <param name="GS47">Pulse width for GS47, from 0x00 to 0x7D</param>
        /// <param name="GS49">Pulse width for GS49, from 0x00 to 0x7D</param>
        /// <param name="GS51">Pulse width for GS51, from 0x00 to 0x7D</param>
        /// <param name="GS53">Pulse width for GS53, from 0x00 to 0x7D</param>
        /// <param name="GS55">Pulse width for GS55, from 0x00 to 0x7D</param>
        /// <param name="GS57">Pulse width for GS57, from 0x00 to 0x7D</param>
        /// <param name="GS59">Pulse width for GS59, from 0x00 to 0x7D</param>
        /// <param name="GS61">Pulse width for GS61, from 0x00 to 0x7D</param>
        /// <param name="GS63">Pulse width for GS63, from 0x00 to 0x7D</param>
        public void SetGrayScaleTable(byte GS1, byte GS3, byte GS5, byte GS7, byte GS9, byte GS11, byte GS13, byte GS15, byte GS17, byte GS19, byte GS21, byte GS23, byte GS25, byte GS27, byte GS29, byte GS31, byte GS33, byte GS35, byte GS37, byte GS39, byte GS41, byte GS43, byte GS45, byte GS47, byte GS49, byte GS51, byte GS53, byte GS55, byte GS57, byte GS59, byte GS61, byte GS63)
        {
            WriteCommand(0xB8, GS1, GS3, GS5, GS7, GS9, GS11, GS13, GS15, GS17, GS19, GS21, GS23, GS25, GS27, GS29, GS31, GS33, GS35, GS37, GS39, GS41, GS43, GS45, GS47, GS49, GS51, GS53, GS55, GS57, GS59, GS61, GS63);
        }

        /// <summary>
        /// Resets <see cref="SetGrayScaleTable(byte, byte, byte, byte, byte, byte, byte, byte, byte, byte, byte, byte, byte, byte, byte, byte, byte, byte, byte, byte, byte, byte, byte, byte, byte, byte, byte, byte, byte, byte, byte, byte)"/> and sets it to linear
        /// </summary>
        public void SetLinearGrayScaleTable()
        {
            WriteCommand(0xB9);
        }

        /// <summary>
        /// Set Master Configuration
        /// </summary>
        /// <param name="powerSupply">
        /// 0x00 - Select external VCC supply <br/>
        /// 0x01 - Reserved (RESET) <br/>
        /// default 0x01
        /// </param>
        public void SetMasterConfiguration(byte powerSupply = 0x01)
        {
            WriteCommand(0xAD, powerSupply);
        }

        /// <summary>
        /// Set master current attenuation factor <paramref name="attenuationFactor"/> from 0x00 to 0x0F corresponding to 1/16, 2/16… to 16/16 attenuation
        /// </summary>
        /// <param name="attenuationFactor">Attenuation factor from 0x00 to 0x00, default 0x0F</param>
        public void SetMasterCurrentControl(byte attenuationFactor = 0x0F)
        {
            WriteCommand(0x87, attenuationFactor);
        }

        /// <summary>
        /// Set MUX ratio to N+1 Mux
        /// </summary>
        /// <param name="MUXRatio">MUX Ratio N+1 from 0x0F to 0x3F, default 0x3F</param>
        public void SetMultiplexRatio(byte MUXRatio = 0x3F)
        {
            WriteCommand(0xA8, MUXRatio);
        }

        /// <summary>
        /// Phase 1 and 2 period adjustment
        /// </summary>
        /// <param name="phase1">Phase 1 period adjustment from 0x01 to 0x0F, default 0x04</param>
        /// <param name="phase2">Phase 2 period adjustment from 0x01 to 0x0F, default 0x07</param>
        public void SetPhaseOneAndTwoPeriodAdjustment(byte phase1 = 0x04, byte phase2 = 0x07) //TODO: Verify
        {
            byte resultByte = 0; //byte magic, combine two bytes into one
            resultByte = BitHelper.SetBit(resultByte, 0, BitHelper.GetBit(phase1, 0));
            resultByte = BitHelper.SetBit(resultByte, 1, BitHelper.GetBit(phase1, 1));
            resultByte = BitHelper.SetBit(resultByte, 2, BitHelper.GetBit(phase1, 2));
            resultByte = BitHelper.SetBit(resultByte, 3, BitHelper.GetBit(phase1, 3));
            resultByte = BitHelper.SetBit(resultByte, 4, BitHelper.GetBit(phase2, 0));
            resultByte = BitHelper.SetBit(resultByte, 5, BitHelper.GetBit(phase2, 1));
            resultByte = BitHelper.SetBit(resultByte, 6, BitHelper.GetBit(phase2, 2));
            resultByte = BitHelper.SetBit(resultByte, 7, BitHelper.GetBit(phase2, 3));
            WriteCommand(0xB1, resultByte);
        }
        /// <summary>
        /// Phase 1 and 2 period adjustment
        /// </summary>
        /// <param name="phases">Phases period adujstment</param>
        public void SetPhaseOneAndTwoPeriodAdjustment(byte phases) 
        {
            WriteCommand(0xB1, phases);
        }
        /// <summary>
        /// Set Power Save Mode
        /// </summary>
        /// <param name="powerSaveMode">
        /// 0x1A - Enable Power save mode (RESET) <br/>
        /// 0x0B - Disable Power save mode
        /// </param>
        public void SetPowerSaveMode(byte powerSaveMode = 0x1A)
        {
            WriteCommand(0xB0, powerSaveMode);
        }

        /// <summary>
        /// Set pre-charge voltage level. All three color share the same pre-charge voltage.
        /// </summary>
        /// <param name="preChargeLevel">
        /// Pre-charge level, from 0x00, to 0x3E <br/>
        /// 0x00 = 0.10 x VCC <br/>
        /// 0x3E = 0.50 x VCC <br/>
        /// </param>
        public void SetPreChargeLevel(byte preChargeLevel = 0x3E)
        {
            preChargeLevel = (byte)(preChargeLevel << 1 & 0xFF); //Bit shift to the left by the specification
            WriteCommand(0xBB, preChargeLevel);
        }

        /// <summary>
        /// Set driver remap and color depth, use <see cref="DriverBase.Helpers.BitHelper"/> to set bits correctly
        /// </summary>
        /// <param name="remapAndColorDepth">
        /// <paramref name="remapAndColorDepth"/>[0]=0, Horizontal address increment <br/>
        /// <paramref name="remapAndColorDepth"/>[0]=1, Vertical address increment <br/>
        /// <br/>
        /// <paramref name="remapAndColorDepth"/>[1]=0, RAM Column 0 to 95 maps to Pin Seg(SA,SB,SC) 0 to 95 <br/>
        /// <paramref name="remapAndColorDepth"/>[1]=1, RAM Column 0 to 95 maps to Pin Seg(SA,SB,SC) 95 to 0 <br/>
        /// <br/>
        /// <paramref name="remapAndColorDepth"/>[2]=0, normal order SA,SB,SC (e.g. RGB) <br/>
        /// <paramref name="remapAndColorDepth"/>[2]=1, reverse order SC,SB,SA (e.g. BGR) <br/>
        /// <br/>
        /// <paramref name="remapAndColorDepth"/>[3]=0, Disable left-right swapping on COM <br/>
        /// <paramref name="remapAndColorDepth"/>[3]=1, Set left-right swapping on COM <br/>
        /// <br/>
        /// <paramref name="remapAndColorDepth"/>[4]=0, Scan from COM 0 to COM [N –1] <br/>
        /// <paramref name="remapAndColorDepth"/>[4]=1, Scan from COM [N-1] to COM0. Where N is the multiplex ratio. <br/>
        /// <br/>
        /// <paramref name="remapAndColorDepth"/>[5]=0, Disable COM Split Odd Even (RESET) <br/>
        /// <paramref name="remapAndColorDepth"/>[5]=1, Enable COM Split Odd Even <br/>
        /// <br/>
        /// <paramref name="remapAndColorDepth"/>[7:6] = 00; 256 color format <br/>
        /// <paramref name="remapAndColorDepth"/>[7:6] = 01; 65k color format <br/>
        /// <paramref name="remapAndColorDepth"/>[7:6] = 10; 65k color format 2 <br/>
        /// <b>If 9 / 18 bit mode is selected, color depth will be fixed to 65k regardless of the setting.</b> <br/>
        /// default 0x01
        /// </param>
        public void SetRemapAndColorDepth(byte remapAndColorDepth = 0x01)
        {
            WriteCommand(0xA0, remapAndColorDepth);
        }

        /// <summary>
        /// Setup Row start and end address
        /// </summary>
        /// <param name="startAddress">Start address from 0x00 to 0x3F, default 0x00</param>
        /// <param name="stopAddress">End address from 0x00 to 0x3F, default 0x3F</param>
        public void SetRowAddress(byte startAddress = 0x00, byte stopAddress = 0x3F)
        {
            WriteCommand(75, startAddress, stopAddress);
        }

        /// <summary>
        /// Set Second Pre-charge Speed for Color "A", "B" and "C"
        /// </summary>
        /// <param name="preChargeSpeedForColorA">Second Pre-charge Speed for color "A" from 0x00 to 0xFF, default 0x80</param>
        /// <param name="preChargeSpeedForColorB">Second Pre-charge Speed for color "B" from 0x00 to 0xFF, default 0x80</param>
        /// <param name="preChargeSpeedForColorC">Second Pre-charge Speed for color "C" from 0x00 to 0xFF, default 0x80</param>
        public void SetSecondPreChargeSpeed(byte preChargeSpeedForColorA = 0x80, byte preChargeSpeedForColorB = 0x80, byte preChargeSpeedForColorC = 0x80)
        {
            WriteCommand(0x8A, preChargeSpeedForColorA, 0x8B, preChargeSpeedForColorB, 0x8C, preChargeSpeedForColorC);
        }

        /// <summary>
        /// Set COM deselect voltage level (V COMH)
        /// </summary>
        /// <param name="VCOMH">
        /// Usable bytes: <br/>
        /// <list type="table">
        /// <listheader>
        /// <term>Hex Code</term>
        /// <description>V COMH</description>
        /// </listheader>
        /// <item>
        /// <term>0x00</term>
        /// <description>0.44 x VCC</description>
        /// </item>
        /// <item>
        /// <term>0x10</term>
        /// <description>0.52 x VCC</description>
        /// </item>
        /// <item>
        /// <term>0x20</term>
        /// <description>0.61 x VCC</description>
        /// </item>
        /// <item>
        /// <term>0x30</term>
        /// <description>0.71 x VCC</description>
        /// </item>
        /// <item>
        /// <term>0x3E</term>
        /// <description>0.83 x VCC</description>
        /// </item>
        /// </list>
        /// </param>
        public void SetVCOMH(byte VCOMH = 0x3E)
        {
            VCOMH = (byte)(VCOMH << 1 & 0xFF); //Bit shift to the left by the specification
            WriteCommand(0xBE, VCOMH);
        }
        #endregion Fundamental Commands

        #region Graphic Acceleration Commands
        /// <summary>
        /// Draws line, more complicated version of <see cref="DrawPixel(byte, byte, ushort)"/>
        /// </summary>
        /// <param name="xStart">X coordinate to start</param>
        /// <param name="yStart">Y coordinate to start</param>
        /// <param name="xEnd">X coordinate to end</param>
        /// <param name="yEnd">Y coordinate to end</param>
        /// <param name="colorC">Color C</param>
        /// <param name="colorB">Color B</param>
        /// <param name="colorA">Color A</param>
        public void DrawLine(byte xStart, byte yStart, byte xEnd, byte yEnd, byte colorC, byte colorB, byte colorA)
        {
            WriteCommand(0x21, xStart, yStart, xEnd, yEnd, colorC, colorB, colorA);
        }
        /// <summary>
        /// Draws rectangle, more complicated version of <see cref="DrawLine(byte, byte, byte, byte, byte, byte, byte)"/>
        /// </summary>
        /// <param name="xStart">X coordinate to start</param>
        /// <param name="yStart">Y coordinate to start</param>
        /// <param name="xEnd">X coordinate to end</param>
        /// <param name="yEnd">Y coordinate to end</param>
        /// <param name="lineColorC">Line color C</param>
        /// <param name="lineColorB">Line color B</param>
        /// <param name="lineColorA">Line color A</param>
        /// <param name="fillColorC">Fill color C, if fill enabled <see cref="Fill(bool, bool)"/></param>
        /// <param name="fillColorB">Fill color B, if fill enabled <see cref="Fill(bool, bool)"/></param>
        /// <param name="fillColorA">Fill color A, if fill enabled <see cref="Fill(bool, bool)"/></param>
        public void DrawRectangle(byte xStart, byte yStart, byte xEnd, byte yEnd, byte lineColorC, byte lineColorB, byte lineColorA, byte fillColorC, byte fillColorB, byte fillColorA)
        {
            WriteCommand(0x22, xStart, yStart, xEnd, yEnd, lineColorC, lineColorB, lineColorA, fillColorC, fillColorB, fillColorA);
        }
        /// <summary>
        /// Copies what is on screen
        /// </summary>
        /// <param name="xStart">X coordinate to start</param>
        /// <param name="yStart">Y coordinate to start</param>
        /// <param name="xEnd">X coordinate to end</param>
        /// <param name="yEnd">Y coordinate to end</param>
        /// <param name="xNew">New X coordinate to copy to</param>
        /// <param name="yNew">New Y coordinate to copy to</param>
        public void Copy(byte xStart, byte yStart, byte xEnd, byte yEnd, byte xNew, byte yNew)
        {
            WriteCommand(0x23, xStart, yStart, xEnd, yEnd, xNew, yNew);
        }
        /// <summary>
        /// Dims segment of a screen
        /// </summary>
        /// <param name="xStart">X coordinate to start</param>
        /// <param name="yStart">Y coordinate to start</param>
        /// <param name="xEnd">X coordinate to end</param>
        /// <param name="yEnd">Y coordinate to end</param>
        public void DimWindow(byte xStart, byte yStart, byte xEnd, byte yEnd)
        {
            WriteCommand(0x24, xStart, yStart, xEnd, yEnd);
        }
        /// <summary>
        /// Deletes segment of a screen
        /// </summary>
        /// <param name="xStart">X coordinate to start</param>
        /// <param name="yStart">Y coordinate to start</param>
        /// <param name="xEnd">X coordinate to end</param>
        /// <param name="yEnd">Y coordinate to end</param>
        public void ClearWindow(byte xStart, byte yStart, byte xEnd, byte yEnd)
        {
            WriteCommand(0x25, xStart, yStart, xEnd, yEnd);
        }
        /// <summary>
        /// Enable fill on <see cref="DrawRectangle(byte, byte, byte, byte, byte, byte, byte, byte, byte, byte)"/> and enable reverse during <see cref="Copy(byte, byte, byte, byte, byte, byte)"/>
        /// </summary>
        /// <param name="enable">Enable Fill</param>
        /// <param name="reverseCopyEnable">Enable Copy</param>
        public void Fill(bool enable, bool reverseCopyEnable)
        {
            byte result = 0x00;
            result = BitHelper.SetBit(result, 0, enable);
            result = BitHelper.SetBit(result, 4, reverseCopyEnable);
            WriteCommand(0x26, result);
        }
        /// <summary>
        /// Continuous Horizontal And Vertical Scrolling Setup
        /// </summary>
        /// <param name="horizontalScrollOffset">Set number of column as horizontal scroll offset, from 0x00 to 0x5F</param>
        /// <param name="yStart">Start row address</param>
        /// <param name="numberOfRowsHorizontallyScrolled">Set number of rows to be horizontal scrolled, but <paramref name="yStart"/> + <paramref name="numberOfRowsHorizontallyScrolled"/> must be more than 64, see <see cref="Width"/> </param>
        /// <param name="numberOfRowsVerticalScrollOffset">Set number of row as vertical scroll offset, from 0x00 to 0x3F</param>
        /// <param name="timeBetweenEachScrollStep">
        /// Set time interval between each scroll step <br/>
        /// <list type="table">
        /// <listheader>
        /// <term>Byte</term>
        /// <description>Frames</description>
        /// </listheader>
        /// <item>
        /// <term>0x00</term>
        /// <description>6 frames</description>
        /// </item>
        /// <item>
        /// <term>0x01</term>
        /// <description>10 frames</description>
        /// </item>
        /// <item>
        /// <term>0x02</term>
        /// <description>100 frames</description>
        /// </item>
        /// <item>
        /// <term>0x03</term>
        /// <description>200 frames</description>
        /// </item>
        /// </list>
        /// </param>
        public void ContinuousHorizontalAndVerticalScroll(byte horizontalScrollOffset, byte yStart, byte numberOfRowsHorizontallyScrolled, byte numberOfRowsVerticalScrollOffset, byte timeBetweenEachScrollStep)
        {
            if (timeBetweenEachScrollStep > 0x03)
                return;
            if ((yStart + numberOfRowsHorizontallyScrolled) < 64)
                return;
            WriteCommand(0x27, horizontalScrollOffset, yStart, numberOfRowsHorizontallyScrolled, numberOfRowsVerticalScrollOffset, timeBetweenEachScrollStep);
        }
        /// <summary>
        /// Deactivates scrolling of <see cref="ContinuousHorizontalAndVerticalScroll(byte, byte, byte, byte, byte)"/>
        /// </summary>
        public void DeactivateScrolling()
        {
            WriteCommand(0x2E);
        }
        /// <summary>
        /// Activates scrolling of <see cref="ContinuousHorizontalAndVerticalScroll(byte, byte, byte, byte, byte)"/>
        /// </summary>
        public void ActivateScrolling()
        {
            WriteCommand(0x2F);
        }
        /// <summary>
        /// Draws one pixel... yes that's all
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="color">Color to draw with</param>
        public void DrawPixel(byte x, byte y, ushort color)
        {
            if ((x >= 0) && (x < Width) && (y >= 0) && (y < Height))
            {
                SetColumnAddress(y, y);
                SetRowAddress(x, x);
                WriteData(color);
            }
        }
        #endregion

        #region Public Methods

        public override long ReadData(byte pointer)
        {
            WriteData(pointer);
            return -1; //This is Display, we never return any data as MISO is disconnected
        }

        public override long ReadData(params byte[] data)
        {
            WriteData(data);
            return -1; //This is Display, we never return any data as MISO is disconnected
        }

        public override string ReadDeviceId()
        {
            return SpiDevice.DeviceId;
        }

        public override string ReadManufacturerId()
        {
            return "Device does not support manufacturer id";
        }

        public override string ReadSerialNumber()
        {
            return "Device does not support serial number";
        }
        public override void Start()
        {
            //Start SPI communication
            base.Start();
            //Start DC/RST/CHP
            dcPin = gpio.OpenPin(dcPinInt, GpioSharingMode.Exclusive);
            dcPin.SetDriveMode(GpioPinDriveMode.Output);
            rstPin = gpio.OpenPin(rstPinInt, GpioSharingMode.Exclusive);
            rstPin.SetDriveMode(GpioPinDriveMode.Output);
            chipSelectPin = gpio.OpenPin(SpiConnectionSettings.ChipSelectLine, GpioSharingMode.SharedReadOnly);
            chipSelectPin.SetDriveMode(GpioPinDriveMode.Output);
            //Starting display
            SetDisplayOnOff(DisplayState.OFF); //Based on https://github.com/adafruit/Adafruit-SSD1331-OLED-Driver-Library-for-Arduino/blob/488737cb7ac00355365584edd5d060c8a691bd27/Adafruit_SSD1331.cpp#L87
            SetRemapAndColorDepth(0x72);
            SetDisplayStartLine();
            SetDisplayOffset();
            SetDisplayMode();
            SetMultiplexRatio();
            SetMasterConfiguration(0x8E);
            SetPowerSaveMode(0x0B);
            SetPhaseOneAndTwoPeriodAdjustment(0x31);
            SetDisplayClockDividerOscillatorFrequency(0xF0);
            SetSecondPreChargeSpeed(0x64, 0x78, 0x64);
            SetPreChargeLevel(0x3A);
            SetVCOMH(0x3E);
            SetMasterCurrentControl(0x06);
            SetContrastForAColor(0x91);
            SetContrastForBColor(0x50);
            SetContrastForCColor(0x7D);
            SetDisplayOnOff(DisplayState.ON);
            dcPin.Write(GpioPinValue.High);
        }

        public override void Stop()
        {
            //Stopping display
            SetDisplayMode(DisplayModes.AllPixelsOff);
            SetDisplayOnOff(DisplayState.OFF);
            dcPin.Write(GpioPinValue.Low);
            Thread.Sleep(100); //Waiting for power off
            //Cleanup DC/RST/CHP
            dcPin.Dispose();
            dcPin = null;
            rstPin.Dispose();
            rstPin = null;
            chipSelectPin.Dispose();
            chipSelectPin = null;
            //Stop SPI communication
            base.Stop();
        }

        public void WriteCommand(params byte[] command) //TODO: Move this to interface
        {
            foreach (var item in command) //Really retarded Adafruit communication speciality?
            {
                chipSelectPin.Write(GpioPinValue.Low); // Chip Select Low - Adafruit speciality?
                dcPin.Write(GpioPinValue.Low); // Command mode
                SpiDevice.Write(new byte[] { item }); // Send the command byte
                dcPin.Write(GpioPinValue.High); // Memory mode
                chipSelectPin.Write(GpioPinValue.High); // Chip Select High - Adafruit speciality?
            }
        }

        public override void WriteData(params byte[] data)
        {
            SpiDevice.Write(data);
        }

        public void WriteData(ushort data) //TODO: Decide if this should go to base interface
        {
            SpiDevice.Write(new ushort[] { data });
        }

        #endregion Public Methods
    }
}