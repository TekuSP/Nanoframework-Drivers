using DriverBase.Enums;
using DriverBase.Interfaces;
using System.Device.Spi;

namespace DriverBase
{
    /// <summary>
    /// Base driver class for SPI devices
    /// </summary>
    public abstract class DriverBaseSPI : IDriverBase
    {
        #region Protected Fields

        protected SpiConnectionSettings SpiConnectionSettings;

        protected SpiDevice SpiDevice;

        #endregion Protected Fields

        #region Public Constructors

        /// <summary>
        /// Constructs SPI Driver, but does not start it, call <see cref="Start"/> for starting it
        /// </summary>
        /// <param name="name">Name of the device</param>
        /// <param name="SPIBusID">SPI Bus ID</param>
        /// <param name="chipSelectPin">Chip Select Pin</param>
        public DriverBaseSPI(string name, int SPIBusID, int chipSelectPin)
        {
            SpiConnectionSettings = new SpiConnectionSettings(chipSelectPin)
            {
                SharingMode = SpiSharingMode.Shared,
                Mode = SpiMode.Mode0,
                BusId = SPIBusID
            };
            Name = name;
            DeviceAddress = chipSelectPin;
        }

        /// <summary>
        /// Constructs SPI Driver, but does not start it, call <see cref="Start"/> for starting it
        /// </summary>
        /// <param name="name">Name of the device</param>
        /// <param name="SPIBusID">SPI Bus ID</param>
        /// <param name="spiConnectionSettings">SPI Custom connection settings</param>
        public DriverBaseSPI(string name, int SPIBusID, SpiConnectionSettings spiConnectionSettings)
        {
            SpiConnectionSettings = spiConnectionSettings;
            spiConnectionSettings.BusId = SPIBusID;
            Name = name;
            DeviceAddress = SpiConnectionSettings.ChipSelectLine;
        }

        #endregion Public Constructors

        #region Public Properties

        public virtual CommunicationType CommunicationType { get; }

        /// <summary>
        /// SPI uses Chip Select as Addresses, this value thereby is Chip Select Pin
        /// </summary>
        public virtual int DeviceAddress { get; }

        public virtual bool IsRunning => SpiDevice != null;

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
            SpiDevice = SpiDevice.Create(SpiConnectionSettings);
        }

        public virtual void Stop()
        {
            SpiDevice?.Dispose();
            SpiDevice = null;
        }

        public abstract void WriteData(byte[] data);

        #endregion Public Methods
    }
}