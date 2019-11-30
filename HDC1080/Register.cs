using ESP32_DriverBase.Helpers;
using ESP32_DriverBase.Interfaces;

namespace HDC1080
{
    public class HDC1080_Register : IRegister
    {
        #region Public Properties

        public bool BatteryStatus { get; set; }
        public bool Heater { get; set; }
        public byte HumidityMeasurementResolution { get; set; } = 0;
        public bool ModeOfAcquisition { get; set; }
        public bool ReservedAgain { get; set; }
        public bool SoftwareReset { get; set; }
        public bool TemperatureMeasurementResolution { get; set; }

        #endregion Public Properties

        #region Public Methods

        public byte GetData()
        {
            byte b = new byte();
            b.SetBit(0, HumidityMeasurementResolution.GetBit(0));
            b.SetBit(1, HumidityMeasurementResolution.GetBit(1));
            b.SetBit(2, TemperatureMeasurementResolution);
            b.SetBit(3, BatteryStatus);
            b.SetBit(4, ModeOfAcquisition);
            b.SetBit(5, Heater);
            b.SetBit(6, ReservedAgain);
            b.SetBit(7, SoftwareReset);
            return b;
        }

        public void SetData(byte input)
        {
            byte b = new byte();
            b.SetBit(0, input.GetBit(0));
            b.SetBit(1, input.GetBit(1));
            HumidityMeasurementResolution = b;
            TemperatureMeasurementResolution = input.GetBit(2);
            BatteryStatus = input.GetBit(3);
            ModeOfAcquisition = input.GetBit(4);
            Heater = input.GetBit(5);
            ReservedAgain = input.GetBit(6);
            SoftwareReset = input.GetBit(7);
        }

        #endregion Public Methods
    }
}