using System.Device.Gpio;

namespace TekuSP.Drivers.DriverBase.Interfaces
{
    /// <summary>
    /// Simple GPIO pin controller interface for driver devices.
    /// Mirrors a subset of <see cref="System.Device.Gpio.GpioPin"/> semantics to make drivers easier to use.
    /// </summary>
    public interface IGpioPinController
    {
        /// <summary>
        /// Open a pin and set its drive mode.
        /// </summary>
        void OpenPin(int pinNumber, PinMode mode);

        /// <summary>
        /// Close a pin and release it (driver may set it to a safe state).
        /// </summary>
        void ClosePin(int pinNumber);

        /// <summary>
        /// Read the current pin value.
        /// </summary>
        PinValue ReadPin(int pinNumber);

        /// <summary>
        /// Write the pin to the specified value. Pin is configured as output if not already.
        /// </summary>
        void WritePin(int pinNumber, PinValue value);

        /// <summary>
        /// Change the drive mode of an already-open pin.
        /// </summary>
        /// <exception cref="NotSupportedException">Thrown if the specified mode is not supported by the implementation.</exception>
        void SetDriveMode(int pinNumber, PinMode mode);
    }
}