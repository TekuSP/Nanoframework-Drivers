using TekuSP.Drivers.DriverBase.Enums;
using TekuSP.Drivers.DriverBase.Interfaces;

using System.IO.Ports;

namespace TekuSP.Drivers.DriverBase
{
    /// <summary>
    /// Base driver for UART (Serial) devices
    /// </summary>
    public abstract class DriverBaseUART : IDriverBase
    {
        #region Protected Fields

        /// <summary>Serial bus identifier (COM port).</summary>
        protected string serialBusID;
        /// <summary>Underlying serial port instance.</summary>
        protected SerialPort serialDevice;

        #endregion Protected Fields

        #region Public Constructors

        /// <summary>
        /// Constructs Serial Driver, but does not start it, call <see cref="Start"/> for starting it
        /// </summary>
        /// <param name="name">Name of the device</param>
        /// <param name="serialBusID">Serial Bus ID (COM port identifier)</param>
        public DriverBaseUART(string name, string serialBusID)
        {
            this.serialBusID = serialBusID;
            Name = name;
            CommunicationType = CommunicationType.Serial;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <inheritdoc/>
        public virtual CommunicationType CommunicationType { get; }

        /// <summary>
        /// Serial Devices do not have address
        /// </summary>
        /// <inheritdoc/>
        public virtual int DeviceAddress => -1;

        /// <inheritdoc/>
        public virtual bool IsRunning => serialDevice != null;
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
            if (serialDevice != null)
                return; //We are already running
            serialDevice = new SerialPort(serialBusID);
        }

        /// <inheritdoc/>
        public virtual void Stop()
        {
            serialDevice?.Dispose();
            serialDevice = null;
        }

        /// <inheritdoc/>
        public abstract void WriteData(byte[] data);

        #endregion Public Methods
    }
}