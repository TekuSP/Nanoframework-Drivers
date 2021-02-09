using DriverBase.Enums;

namespace DriverBase.Interfaces
{
    public interface IDriverBase
    {
        CommunicationType CommunicationType { get; }
        int DeviceAddress { get; }
        bool IsRunning { get; }
        string Name { get; }

        long ReadData(byte pointer);

        long ReadData(byte[] data);

        string ReadDeviceId();

        string ReadManufacturerId();

        string ReadSerialNumber();

        void Restart();

        void Start();

        void Stop();

        void WriteData(byte[] data);
    }
}
