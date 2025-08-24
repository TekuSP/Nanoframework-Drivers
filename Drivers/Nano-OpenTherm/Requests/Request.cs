using System;
using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

using TekuSP.Drivers.DriverBase.Interfaces;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Abstract class for all requests
    /// </summary>
    public abstract class Request : IOpenThermData
    {
        protected Request()
        {
            
        }
        protected Request(Request baseReq)
        {
            SetRawDataCore(baseReq.GetRawDataCore());
        }
        // Explicit IOpenThermData implementation to allow public accessor shape to vary in derived classes
        ulong IOpenThermData.RawData
        {
            get => GetRawDataCore();
            set => SetRawDataCore(value);
        }

        /// <summary>
        /// Derived classes must provide core getters/setters. Use NotSupportedException in the accessor you don't support.
        /// </summary>
        protected abstract ulong GetRawDataCore();
        protected abstract void SetRawDataCore(ulong value);

        /// <summary>
        /// Message Type
        /// </summary>
        public abstract MessageType MessageType { get; }
        /// <summary>
        /// Message ID
        /// </summary>
        public abstract MessageID MessageID { get; }

        /// <summary>
        /// Returns the encoded 32-bit OpenTherm frame for this request.
        /// </summary>
        public ulong BuildFrame() => GetRawDataCore();

        /// <summary>
        /// Automatically selects a strongly-typed Request from a received frame
        /// </summary>
        /// <returns>Request instance matching the MessageID when available, otherwise this</returns>
        public Request SelectRequest()
        {
            switch (MessageID)
            {
                case MessageID.Status:
                    return new SetBoilerStatusRequest(this);
                case MessageID.TSet:
                    return new SetBoilerTemperatureRequest(this);
                case MessageID.MConfigMMemberIDcode:
                    break;
                case MessageID.SConfigSMemberIDcode:
                    break;
                case MessageID.RemoteRequest:
                    break;
                case MessageID.ASFflags:
                    return new GetFaultRequest(this);
                case MessageID.RBPflags:
                    break;
                case MessageID.CoolingControl:
                    return new SetCoolingControlRequest(this);
                case MessageID.TsetCH2:
                    return new SetCH2SetpointRequest(this);
                case MessageID.TrOverride:
                    return new SetRoomOverrideRequest(this);
                case MessageID.TSP:
                    break;
                case MessageID.TSPindexTSPvalue:
                    break;
                case MessageID.FHBsize:
                    break;
                case MessageID.FHBindexFHBvalue:
                    break;
                case MessageID.MaxRelModLevelSetting:
                    return new SetMaxRelModulationRequest(this);
                case MessageID.MaxCapacityMinModLevel:
                    break;
                case MessageID.TrSet:
                    return new SetRoomSetpointRequest(this);
                case MessageID.RelModLevel:
                    return new GetModulationRequest(this);
                case MessageID.CHPressure:
                    return new GetPressureRequest(this);
                case MessageID.DHWFlowRate:
                    break;
                case MessageID.DayTime:
                    return new SetDayTimeRequest(this);
                case MessageID.Date:
                    return new SetDateRequest(this);
                case MessageID.Year:
                    return new SetYearRequest(this);
                case MessageID.TrSetCH2:
                    return new SetRoomSetpointCH2Request(this);
                case MessageID.Tr:
                    break;
                case MessageID.Tboiler:
                    return new GetBoilerTemperatureRequest(this);
                case MessageID.Tdhw:
                    return new GetDWHSetPointRequest(this);
                case MessageID.Toutside:
                    break;
                case MessageID.Tret:
                    return new GetReturnTemperatureRequest(this);
                case MessageID.Tstorage:
                    break;
                case MessageID.Tcollector:
                    break;
                case MessageID.TflowCH2:
                    break;
                case MessageID.Tdhw2:
                    break;
                case MessageID.Texhaust:
                    break;
                case MessageID.TboilerHeatExchanger:
                    break;
                case MessageID.BoilerFanSpeedSetpointAndActual:
                    break;
                case MessageID.FlameCurrent:
                    break;
                case MessageID.TrCH2:
                    break;
                case MessageID.RelativeHumidity:
                    break;
                case MessageID.TrOverride2:
                    break;
                case MessageID.TdhwSetUBTdhwSetLB:
                    break;
                case MessageID.MaxTSetUBMaxTSetLB:
                    break;
                case MessageID.TdhwSet:
                    break;
                case MessageID.MaxTSet:
                    break;
                case MessageID.StatusVentilationHeatRecovery:
                    break;
                case MessageID.Vset:
                    break;
                case MessageID.ASFflagsOEMfaultCodeVentilationHeatRecovery:
                    break;
                case MessageID.OEMDiagnosticCodeVentilationHeatRecovery:
                    break;
                case MessageID.SConfigSMemberIDCodeVentilationHeatRecovery:
                    break;
                case MessageID.OpenThermVersionVentilationHeatRecovery:
                    break;
                case MessageID.VentilationHeatRecoveryVersion:
                    break;
                case MessageID.RelVentLevel:
                    break;
                case MessageID.RHexhaust:
                    break;
                case MessageID.CO2exhaust:
                    break;
                case MessageID.Tsi:
                    break;
                case MessageID.Tso:
                    break;
                case MessageID.Tei:
                    break;
                case MessageID.Teo:
                    break;
                case MessageID.RPMexhaust:
                    break;
                case MessageID.RPMsupply:
                    break;
                case MessageID.RBPflagsVentilationHeatRecovery:
                    break;
                case MessageID.NominalVentilationValue:
                    break;
                case MessageID.TSPventilationHeatRecovery:
                    break;
                case MessageID.TSPindexTSPvalueVentilationHeatRecovery:
                    break;
                case MessageID.FHBsizeVentilationHeatRecovery:
                    break;
                case MessageID.FHBindexFHBvalueVentilationHeatRecovery:
                    break;
                case MessageID.Brand:
                    return new GetManufacturerRequest(this);
                case MessageID.BrandVersion:
                    return new GetManufacturerVersionRequest(this);
                case MessageID.BrandSerialNumber:
                    return new GetSerialRequest(this);
                case MessageID.CoolingOperationHours:
                    break;
                case MessageID.PowerCycles:
                    break;
                case MessageID.RFsensorStatusInformation:
                    break;
                case MessageID.RemoteOverrideOperatingModeHeatingDHW:
                    return new SetRemoteOverrideOperatingModeRequest(this);
                case MessageID.RemoteOverrideFunction:
                    break;
                case MessageID.StatusSolarStorage:
                    break;
                case MessageID.ASFflagsOEMfaultCodeSolarStorage:
                    break;
                case MessageID.SConfigSMemberIDcodeSolarStorage:
                    break;
                case MessageID.SolarStorageVersion:
                    break;
                case MessageID.TSPSolarStorage:
                    break;
                case MessageID.TSPindexTSPvalueSolarStorage:
                    break;
                case MessageID.FHBsizeSolarStorage:
                    break;
                case MessageID.FHBindexFHBvalueSolarStorage:
                    break;
                case MessageID.ElectricityProducerStarts:
                    break;
                case MessageID.ElectricityProducerHours:
                    break;
                case MessageID.ElectricityProduction:
                    break;
                case MessageID.CumulativElectricityProduction:
                    break;
                case MessageID.UnsuccessfulBurnerStarts:
                    break;
                case MessageID.FlameSignalTooLowNumber:
                    break;
                case MessageID.OEMDiagnosticCode:
                    break;
                case MessageID.SuccessfulBurnerStarts:
                    break;
                case MessageID.CHPumpStarts:
                    break;
                case MessageID.DHWPumpValveStarts:
                    break;
                case MessageID.DHWBurnerStarts:
                    break;
                case MessageID.BurnerOperationHours:
                    break;
                case MessageID.CHPumpOperationHours:
                    break;
                case MessageID.DHWPumpValveOperationHours:
                    break;
                case MessageID.DHWBurnerOperationHours:
                    break;
                case MessageID.OpenThermVersionMaster:
                    break;
                case MessageID.OpenThermVersionSlave:
                    break;
                case MessageID.MasterVersion:
                    break;
                case MessageID.SlaveVersion:
                    break;
                default:
                    return this;
            }
            return this; // TODO keep until specific mappings are implemented
        }

        /// <summary>
        /// Processes request
        /// </summary>
        /// <param name="data">Input data</param>
        /// <returns>Returns Raw Request</returns>
        protected ulong ProcessRequest(ulong data)
        {
            // Write full 3-bit message type (bits 30..28)
            data |= (((ulong)MessageType) & 0x7) << 28;
            // Write message id (bits 23..16)
            data |= ((ulong)MessageID) << 16;
            // Ensure overall frame has odd parity (bit count over 32 bits is odd)
            if (!Utilities.Parity(data))
                data |= (1ul << 31);
            return data;
        }
        /// <summary>
        /// Is Valid Request?
        /// </summary>
        /// <returns>Validity</returns>
        public bool IsValidRequest()
        {
            // Parity over full 32-bit frame must be odd
            var raw = GetRawDataCore();
            if (!Utilities.Parity(raw))
                return false;
            var msgType = (MessageType)((raw >> 28) & 0x7);
            if (msgType != MessageType.READ_DATA && msgType != MessageType.WRITE_DATA)
                return false;
            // Validate ID capability vs requested operation
            return OpenThermAccess.IsOperationAllowed(MessageID, msgType);
        }
    }
}
