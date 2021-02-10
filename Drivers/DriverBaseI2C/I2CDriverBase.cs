using DriverBase.Enums;
using Windows.Devices.I2c;

namespace DriverBase
{
    public abstract class DriverBaseI2C
    {
        #region Protected Fields

        protected I2cConnectionSettings I2CConnectionSettings;
        protected I2cDevice I2CDevice;

        #endregion Protected Fields

        #region Public Constructors

        public DriverBaseClass(string name, CommunicationType communicationType, int deviceAddress)
        {
            Name = name;
            CommunicationType = communicationType;
            DeviceAddress = deviceAddress;
            I2CConnectionSettings = new I2cConnectionSettings(deviceAddress)
            {
                BusSpeed = I2cBusSpeed.FastMode,
                SharingMode = I2cSharingMode.Shared
            };
        }

        #endregion Public Constructors

        #region Public Properties

        public virtual CommunicationType CommunicationType { get; }
        public virtual int DeviceAddress { get; }
        public virtual bool IsRunning => I2CDevice != null;
        public virtual string Name { get; }

        #endregion Public Properties

        #region Public Methods

        public abstract long ReadData(byte pointer);

        public abstract long ReadData(byte[] data);

        public abstract string ReadDeviceId();

        public abstract string ReadManufacturerId();

        public abstract string ReadSerialNumber();

        public virtual void Restart()
        {
            Stop();
            Start();
        }

        public virtual void Start()
        {
            I2CDevice = I2cDevice.FromId("I2C1", I2CConnectionSettings);
        }

        public virtual void Stop()
        {
            I2CDevice.Dispose();
            I2CDevice = null;
        }

        public abstract void WriteData(byte[] data);

        #endregion Public Methods
    }
}