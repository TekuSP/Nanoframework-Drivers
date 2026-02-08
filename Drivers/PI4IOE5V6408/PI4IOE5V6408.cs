using System;
using System.Device.I2c;
using TekuSP.Drivers.DriverBase;
using TekuSP.Drivers.DriverBase.Enums;
using TekuSP.Drivers.PI4IOE5V6408.Enums;
using System.Device.Gpio;
using TekuSP.Drivers.DriverBase.Interfaces;
using TekuSP.Drivers.PI4IOE5V6408.Structs;

namespace TekuSP.Drivers.PI4IOE5V6408
{
    /// <summary>
    /// Driver for PI4IOE5V6408 8-bit I2C-bus I/O expander.
    /// Implements register-level control: direction, output state, pull-ups, interrupts.
    /// </summary>
    public class PI4IOE5V6408 : DriverBaseI2C, IGpioPinController
    {
        /// <summary>
        /// Tracks which pins have interrupts enabled (by pin index 0..7).
        /// </summary>
        public bool[] Interrupts { get; } = new bool[8];

        /// <summary>
        /// Set true if the device reported it was in reset during initialization.
        /// </summary>
        public bool ResetInitFlag { get; private set; }

        #region Constructors

        /// <summary>
        /// Initialize with I2C bus id and optional device address (default 0x43).
        /// </summary>
        public PI4IOE5V6408(int I2CBusID, int deviceAddress = 0x43)
            : base("PI4IOE5V6408", I2CBusID, deviceAddress)
        {
        }

        /// <summary>
        /// Initialize with custom I2C connection settings.
        /// </summary>
        public PI4IOE5V6408(int I2CBusID, I2cConnectionSettings connectionSettings, int deviceAddress = 0x43)
            : base("PI4IOE5V6408", I2CBusID, connectionSettings, deviceAddress)
        {
        }

        #endregion

        #region Public Methods

        /// <inheritdoc/>
        public override void Start()
        {
            base.Start();
            // validate device id top bits (bits 7..5 == 0b101)
            byte idReg = ReadRegister(PI4IOE5V6408Register.DeviceIdReset);
            if ((idReg & 0b1110_0000) != 0b1010_0000)
                throw new InvalidOperationException($"PI4IOE5V6408 not found (device id byte 0b{idReg:8b})");

            ResetInitFlag = ReadBit(PI4IOE5V6408Register.DeviceIdReset, 1) == 1;
        }

        /// <inheritdoc/>
        public override void Stop()
        {
            base.Stop();
        }

        /// <inheritdoc/>
        public override long ReadData(byte pointer)
        {
            return ReadData(new byte[] { pointer });
        }

        /// <inheritdoc/>
        public override long ReadData(byte[] data)
        {
            return I2CDevice.Read(data).BytesTransferred;
        }

        /// <inheritdoc/>
        public override string ReadDeviceId()
        {
            var id = ReadRegister(PI4IOE5V6408Register.DeviceIdReset);
            return $"0x{id:X2}";
        }

        /// <inheritdoc/>
        public override string ReadManufacturerId()
        {
            return "PERICOM";
        }

        /// <inheritdoc/>
        public override string ReadSerialNumber()
        {
            return "Not supported";
        }

        /// <inheritdoc/>
        public override void WriteData(params byte[] data)
        {
            I2CDevice.Write(data);
        }

        #endregion

        #region Pin control

        /// <summary>
        /// Set a pin to output and optionally set its state and impedance.
        /// </summary>
        public void SetOutput(int pin, PinState state, bool highImpedance = false)
        {
            ValidatePin(pin);
            var s = (byte)(state == PinState.High ? 1 : 0);
            var imp = highImpedance ? (byte)OutputImpedance.High : (byte)OutputImpedance.Low;

            SetBit(PI4IOE5V6408Register.OutputImpedence, pin, imp);
            SetBit(PI4IOE5V6408Register.OutputState, pin, s);
            SetBit(PI4IOE5V6408Register.IoDirection, pin, (byte)PinDirection.Output);
        }

        /// <summary>
        /// Open a pin and set its drive mode (maps to input/output and pull settings).
        /// </summary>
        public void OpenPin(int pinNumber, PinMode mode)
        {
            switch (mode)
            {
                case PinMode.Input:
                    SetInput(pinNumber, PullSelection.Disabled);
                    break;
                case PinMode.InputPullUp:
                    SetInput(pinNumber, PullSelection.PullUp);
                    break;
                case PinMode.InputPullDown:
                    SetInput(pinNumber, PullSelection.PullDown);
                    break;
                case PinMode.Output:
                    SetOutput(pinNumber, PinState.Low, false);
                    break;
                case PinMode.OutputOpenDrain:
                    // Open-drain is not supported by this expander in the same way as a microcontroller
                    // Use Output mode or control impedance explicitly; throw to make user aware.
                    throw new NotSupportedException("PinMode.OutputOpenDrain is not supported by PI4IOE5V6408. Use Output or manage impedance explicitly.");
                default:
                    throw new NotSupportedException($"PinMode {mode} is not supported by PI4IOE5V6408");
            }
        }

        /// <summary>
        /// Close a pin and release it to a safe state (pulldown input).
        /// </summary>
        public void ClosePin(int pinNumber)
        {
            SetInput(pinNumber, PullSelection.Disabled);
        }

        /// <summary>
        /// Read the current pin value and return <see cref="PinValue"/>.
        /// </summary>
        public PinValue ReadPin(int pinNumber)
        {
            var state = GetInput(pinNumber);
            return state == PinState.High ? PinValue.High : PinValue.Low;
        }

        /// <summary>
        /// Write a value to the pin (configures as output if needed).
        /// </summary>
        public void WritePin(int pinNumber, PinValue value)
        {
            SetOutput(pinNumber, value == PinValue.High ? PinState.High : PinState.Low, false);
        }

        /// <summary>
        /// Change pin drive mode for an already-open pin.
        /// Note: <see cref="PinMode.OutputOpenDrain"/> is not supported and will throw <see cref="NotSupportedException"/>.
        /// </summary>
        public void SetDriveMode(int pinNumber, PinMode mode)
        {
            if (mode == PinMode.OutputOpenDrain)
            {
                throw new NotSupportedException("PinMode.OutputOpenDrain is not supported by PI4IOE5V6408.");
            }

            OpenPin(pinNumber, mode);
        }

        /// <summary>
        /// Set a pin to input and configure optional bias (Disabled = leave pull disabled).
        /// </summary>
        public void SetInput(int pin, PullSelection bias = PullSelection.Disabled)
        {
            ValidatePin(pin);
            // ensure output impedance high and output state cleared
            SetBit(PI4IOE5V6408Register.OutputImpedence, pin, (byte)OutputImpedance.High);
            SetBit(PI4IOE5V6408Register.OutputState, pin, (byte)PinState.Low);

            if (bias == PullSelection.Disabled)
            {
                SetBit(PI4IOE5V6408Register.InputPullupdownEnable, pin, 0); // disabled
            }
            else
            {
                SetBit(PI4IOE5V6408Register.InputPullupPullDown, pin, (byte)bias);
                SetBit(PI4IOE5V6408Register.InputPullupdownEnable, pin, 1); // enabled
            }

            SetBit(PI4IOE5V6408Register.IoDirection, pin, (byte)PinDirection.Input);
        }

        /// <summary>
        /// Fast read of the input status pin value (ignores output state on outputs).
        /// </summary>
        public PinState GetInput(int pin)
        {
            ValidatePin(pin);
            int v = ReadBit(PI4IOE5V6408Register.InputStatus, pin);
            return v == 0 ? PinState.Low : PinState.High;
        }

        /// <summary>
        /// Read current pin state and direction.
        /// Returns a <see cref="PinInfo"/> container with value and direction.
        /// </summary>
        public PinInfo GetPin(int pin)
        {
            ValidatePin(pin);
            var dir = ReadBit(PI4IOE5V6408Register.IoDirection, pin) == (byte)PinDirection.Input ? PinDirection.Input : PinDirection.Output;
            PinState val;
            if (dir == PinDirection.Input)
                val = GetInput(pin);
            else
                val = ReadBit(PI4IOE5V6408Register.OutputState, pin) == 0 ? PinState.Low : PinState.High;

            return new PinInfo(val, dir);
        }

        #endregion

        #region Interrupts

        /// <summary>
        /// Enable interrupt on a pin. normalState is the "normal" state which when left triggers the interrupt.
        /// </summary>
        public void EnableInterrupt(int pin, PinState normalState, bool intPin = true)
        {
            ValidatePin(pin);
            SetBit(PI4IOE5V6408Register.InputDefaultState, pin, (byte)(normalState == PinState.High ? 1 : 0));
            SetBit(PI4IOE5V6408Register.InterruptMask, pin, intPin ? (byte)InterruptMaskValue.Enabled : (byte)InterruptMaskValue.Disabled);
            Interrupts[pin] = true;
        }

        /// <summary>
        /// Disable interrupts for a pin.
        /// </summary>
        public void DisableInterrupt(int pin)
        {
            ValidatePin(pin);
            Interrupts[pin] = false;
            SetBit(PI4IOE5V6408Register.InterruptMask, pin, (byte)InterruptMaskValue.Disabled);
        }

        /// <summary>
        /// Get list of triggered interrupts (reading clears the interrupt flags).
        /// </summary>
        public int[] GetInterrupts()
        {
            byte triggers = ReadRegister(PI4IOE5V6408Register.InterruptStatus);
            int[] tmp = new int[8];
            int count = 0;
            for (int pin = 0; pin < 8; pin++)
            {
                if (Interrupts[pin] && ((triggers & (1 << pin)) != 0))
                {
                    tmp[count++] = pin;
                }
            }
            int[] result = new int[count];
            for (int i = 0; i < count; i++) result[i] = tmp[i];
            return result;
        }

        #endregion

        #region Device control

        /// <summary>
        /// Generate a software reset (sets device to default reset state: pulldown inputs).
        /// </summary>
        public void Reset()
        {
            WriteRegister(PI4IOE5V6408Register.DeviceIdReset, 0x01);
        }

        /// <summary>
        /// Check whether reset-init flag was set. This reads the register bit (and does not modify other behaviour).
        /// </summary>
        public bool CheckResetFlag()
        {
            return ReadBit(PI4IOE5V6408Register.DeviceIdReset, 1) == 1;
        }

        #endregion

        #region Low-level helpers

        private void ValidatePin(int pin)
        {
            if (pin < 0 || pin > 7)
                throw new ArgumentOutOfRangeException(nameof(pin), "Pin must be in range 0..7");
        }

        private int ToBit(PinState state) => state == PinState.High ? 1 : 0;

        private byte ReadRegister(PI4IOE5V6408Register reg)
        {
            byte[] write = new byte[1] { (byte)reg };
            byte[] read = new byte[1];
            I2CDevice.WriteRead(write, read);
            return read[0];
        }

        private void WriteRegister(PI4IOE5V6408Register reg, byte value)
        {
            byte[] buf = new byte[2] { (byte)reg, value };
            I2CDevice.Write(buf);
        }

        private byte[] ReadAllRegisters()
        {
            // read 0x14 (20) bytes starting at 0x00 to match micropython approach
            byte[] write = new byte[1] { 0x00 };
            byte[] buf = new byte[0x14];
            I2CDevice.WriteRead(write, buf);
            return buf;
        }

        private void SetBit(PI4IOE5V6408Register reg, int bit, byte value)
        {
            if (bit < 0 || bit > 7) throw new ArgumentOutOfRangeException(nameof(bit));
            byte cur = ReadRegister(reg);
            byte mask = (byte)(1 << bit);
            byte newv = (byte)((cur & (byte)~mask) | ((value == 0 ? 0 : mask)));
            WriteRegister(reg, newv);
        }

        private byte ReadBit(PI4IOE5V6408Register reg, int bit)
        {
            if (bit < 0 || bit > 7) throw new ArgumentOutOfRangeException(nameof(bit));
            byte cur = ReadRegister(reg);
            return (byte)((cur & (1 << bit)) != 0 ? 1 : 0);
        }

        #endregion
    }
}
