using TekuSP.Drivers.DriverBase.Enums;
using TekuSP.Drivers.DriverBase.Interfaces;
using System.Device.Spi;

namespace TekuSP.Drivers.DriverBase
{
    /// <summary>
    /// Base driver class for SPI devices
    /// </summary>
    public abstract class DriverBaseSPI : IDriverBase
    {
        #region Protected Fields

        /// <summary>SPI connection settings used to create the device.</summary>
        protected SpiConnectionSettings SpiConnectionSettings;

        /// <summary>Underlying SPI device instance.</summary>
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
            SpiConnectionSettings = new SpiConnectionSettings(SPIBusID, chipSelectPin)
            {
                Mode = SpiMode.Mode0
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

        /// <inheritdoc/>
        public virtual CommunicationType CommunicationType { get; }

        /// <summary>
        /// SPI uses Chip Select as Addresses, this value thereby is Chip Select Pin
        /// </summary>
        /// <inheritdoc/>
        public virtual int DeviceAddress { get; }

        /// <inheritdoc/>
        public virtual bool IsRunning => SpiDevice != null;

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
            if (SpiDevice != null)
                return; //We are already running
            SpiDevice = SpiDevice.Create(SpiConnectionSettings);
        }

        /// <inheritdoc/>
        public virtual void Stop()
        {
            SpiDevice?.Dispose();
            SpiDevice = null;
        }

        /// <inheritdoc/>
        public abstract void WriteData(byte[] data);

        #endregion Public Methods
    }
}