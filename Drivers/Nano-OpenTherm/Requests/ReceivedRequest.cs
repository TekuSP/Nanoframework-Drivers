using TekuSP.Drivers.DriverBase.Enums.OpenTherm;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    public class ReceivedRequest : ReadWriteRequest
    {
        private ulong _raw;

        public ReceivedRequest(ulong rawData)
        {
            _raw = rawData;
            MessageType = (MessageType)((rawData >> 28) & 7);
            MessageID = (MessageID)((rawData >> 16) & 0xFF);
        }

        protected override ulong GetRawDataCore() => _raw;
        protected override void SetRawDataCore(ulong value) { _raw = value; }

        public override MessageType MessageType { get; }
        public override MessageID MessageID { get; }

        /// <summary>
        /// Automatically selects a strongly-typed Request from a received frame
        /// </summary>
        /// <returns>Request instance matching the MessageID when available, otherwise this</returns>
        public Request SelectRequest()
        {
            switch (MessageID)
            {
                case MessageID.Status:
                    return new SetBoilerStatusRequest();
                case MessageID.TSet:
                    return new SetBoilerTemperatureRequest();
                case MessageID.MConfigMMemberIDcode:
                    break;
                case MessageID.SConfigSMemberIDcode:
                    break;
                case MessageID.RemoteRequest:
                    break;
                case MessageID.ASFflags:
                    return new GetFaultRequest();
                case MessageID.RBPflags:
                    break;
                case MessageID.CoolingControl:
                    break;
                case MessageID.TsetCH2:
                    break;
                case MessageID.TrOverride:
                    break;
                case MessageID.TSP:
                    break;
                case MessageID.TSPindexTSPvalue:
                    break;
                case MessageID.FHBsize:
                    break;
                case MessageID.FHBindexFHBvalue:
                    break;
                case MessageID.MaxRelModLevelSetting:
                    break;
                case MessageID.MaxCapacityMinModLevel:
                    break;
                case MessageID.TrSet:
                    break;
                case MessageID.RelModLevel:
                    return new GetModulationRequest();
                case MessageID.CHPressure:
                    return new GetPressureRequest();
                case MessageID.DHWFlowRate:
                    break;
                case MessageID.DayTime:
                    break;
                case MessageID.Date:
                    break;
                case MessageID.Year:
                    break;
                case MessageID.TrSetCH2:
                    break;
                case MessageID.Tr:
                    break;
                case MessageID.Tboiler:
                    return new GetBoilerTemperatureRequest();
                case MessageID.Tdhw:
                    return new GetDWHSetPointRequest();
                case MessageID.Toutside:
                    break;
                case MessageID.Tret:
                    return new GetReturnTemperatureRequest();
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
                    return new GetManufacturerRequest();
                case MessageID.BrandVersion:
                    return new GetManufacturerVersionRequest();
                case MessageID.BrandSerialNumber:
                    return new GetSerialRequest();
                case MessageID.CoolingOperationHours:
                    break;
                case MessageID.PowerCycles:
                    break;
                case MessageID.RFsensorStatusInformation:
                    break;
                case MessageID.RemoteOverrideOperatingModeHeatingDHW:
                    break;
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
    }
}
