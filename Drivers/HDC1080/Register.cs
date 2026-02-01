using TekuSP.Drivers.DriverBase.Helpers;
using TekuSP.Drivers.DriverBase.Interfaces;

namespace TekuSP.Drivers.HDC1080
{
    /// <summary>
    /// HDC1080 configuration register model.
    /// </summary>
    public class HDC1080_Register : IRegister
    {
        #region Public Properties

        /// <summary>Battery status flag.</summary>
        public bool BatteryStatus { get; set; }
        /// <summary>Heater enable flag.</summary>
        public bool Heater { get; set; }
        /// <summary>Humidity measurement resolution setting bits.</summary>
        public byte HumidityMeasurementResolution { get; set; } = 0;
        /// <summary>Mode of acquisition flag.</summary>
        public bool ModeOfAcquisition { get; set; }
        /// <summary>Reserved flag bit.</summary>
        public bool ReservedAgain { get; set; }
        /// <summary>Software reset flag.</summary>
        public bool SoftwareReset { get; set; }
        /// <summary>Temperature measurement resolution flag.</summary>
        public bool TemperatureMeasurementResolution { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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