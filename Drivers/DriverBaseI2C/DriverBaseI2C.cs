using TekuSP.Drivers.DriverBase.Enums;
using TekuSP.Drivers.DriverBase.Interfaces;
using System.Device.I2c;

namespace TekuSP.Drivers.DriverBase
{
    /// <summary>
    /// Base driver class for I2C devices
    /// </summary>
    public abstract class DriverBaseI2C : IDriverBase
    {
        #region Protected Fields

        /// <summary>I2C connection settings used to create the device.</summary>
        protected I2cConnectionSettings I2CConnectionSettings;
        /// <summary>Underlying I2C device instance.</summary>
        protected I2cDevice I2CDevice;

        #endregion Protected Fields

        #region Public Constructors

        /// <summary>
        /// Constructs I2C Driver, but does not start it, call <see cref="Start"/> for starting it
        /// </summary>
        /// <param name="name">Name of the device</param>
        /// <param name="I2CBusID">I2C Bus ID</param>
        /// <param name="deviceAddress">I2C Device Address</param>
        public DriverBaseI2C(string name, int I2CBusID, int deviceAddress)
        {
            Name = name;
            CommunicationType = CommunicationType.I2C;
            DeviceAddress = deviceAddress;
            I2CConnectionSettings = new I2cConnectionSettings(I2CBusID, deviceAddress, I2cBusSpeed.FastMode);
        }

        /// <summary>
        /// Constructs I2C Driver, but does not start it, call <see cref="Start"/> for starting it
        /// </summary>
        /// <param name="name">Name of the device</param>
        /// <param name="I2CBusID">I2C Bus ID</param>
        /// <param name="connectionSettings">I2C Custom connection settings</param>
        /// <param name="deviceAddress">I2C Device Address</param>
        public DriverBaseI2C(string name, int I2CBusID, I2cConnectionSettings connectionSettings, int deviceAddress)
        {
            Name = name;
            CommunicationType = CommunicationType.I2C;
            DeviceAddress = deviceAddress;
            I2CConnectionSettings = connectionSettings;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <inheritdoc/>
        public virtual CommunicationType CommunicationType { get; }
        /// <inheritdoc/>
        public virtual int DeviceAddress { get; }
        /// <inheritdoc/>
        public virtual bool IsRunning => I2CDevice != null;
        /// <inheritdoc/>
        public virtual string Name { get; }

        #endregion Public Properties

        #region Public Methods

        /// <inheritdoc/>
        public abstract long ReadData(byte pointer);

        /// <inheritdoc/>
        public abstract long ReadData(byte[] data);

        /// <inheritdoc/>
        public abstract string ReadDeviceId();

        /// <inheritdoc/>
        public abstract string ReadManufacturerId();

        /// <inheritdoc/>
        public abstract string ReadSerialNumber();

        /// <inheritdoc/>
        public virtual void Restart()
        {
            Stop();
            Start();
        }

        /// <inheritdoc/>
        public virtual void Start()
        {
            if (I2CDevice != null)
                return; //We are already running
            I2CDevice = new I2cDevice(I2CConnectionSettings);
        }

        /// <inheritdoc/>
        public virtual void Stop()
        {
            I2CDevice?.Dispose();
            I2CDevice = null;
        }

        /// <inheritdoc/>
        public abstract void WriteData(byte[] data);

        #endregion Public Methods
    }
}