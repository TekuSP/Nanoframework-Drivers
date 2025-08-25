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
        protected Request() { }
        protected Request(Request baseReq) { SetRawDataCore(baseReq.GetRawDataCore()); }

        // Explicit IOpenThermData implementation to allow public accessor shape to vary in derived classes
        ulong IOpenThermData.RawData { get => GetRawDataCore(); set => SetRawDataCore(value); }

        /// <summary>
        /// Derived classes must provide core getters/setters. Use NotSupportedException in the accessor you don't support.
        /// </summary>
        protected abstract ulong GetRawDataCore();
        protected abstract void SetRawDataCore(ulong value);

        /// <summary>Message Type</summary>
        public abstract MessageType MessageType { get; }
        /// <summary>Message ID</summary>
        public abstract MessageID MessageID { get; }

        /// <summary>Returns the encoded 32-bit OpenTherm frame for this request.</summary>
        public ulong BuildFrame() => GetRawDataCore();

        /// <summary>
        /// Automatically selects a strongly-typed Request from a received frame
        /// </summary>
        /// <returns>Request instance matching the MessageID when available, otherwise this</returns>
        public Request SelectRequest()
        {
            switch (MessageID)
            {
                // Status and core control
                case MessageID.Status: return new SetBoilerStatusRequest(this);
                case MessageID.TSet: return new SetBoilerTemperatureRequest(this);
                case MessageID.MConfigMMemberIDcode: return new GetMasterConfigurationRequest(this);
                case MessageID.SConfigSMemberIDcode: return new GetSlaveConfigurationRequest(this);
                case MessageID.RemoteRequest: return new GetRemoteRequestRequest(this);

                // Faults / flags
                case MessageID.ASFflags: return new GetFaultRequest(this);
                case MessageID.RBPflags: return new GetRemoteBoilerParameterFlagsRequest(this);

                // Cooling and CH2 setpoint
                case MessageID.CoolingControl: return new SetCoolingControlRequest(this);
                case MessageID.TsetCH2: return new SetCH2SetpointRequest(this);
                case MessageID.TrOverride: return new SetRoomOverrideRequest(this);
                case MessageID.TrOverride2: return new SetRoomOverride2Request(this);

                // Transparent slave parameters and fault history buffer
                case MessageID.TSP: return new GetTransparentSlaveParametersCountRequest(this);
                case MessageID.TSPindexTSPvalue: return new GetTransparentSlaveParameterRequest(this);
                case MessageID.FHBsize: return new GetFaultHistoryBufferSizeRequest(this);
                case MessageID.FHBindexFHBvalue: return new GetFaultHistoryBufferEntryRequest(this);

                // Capacity / modulation
                case MessageID.MaxRelModLevelSetting: return new SetMaxRelModulationRequest(this);
                case MessageID.MaxCapacityMinModLevel: return new GetBoilerCapacityAndMinModRequest(this);

                // Room setpoints and levels
                case MessageID.TrSet: return new SetRoomSetpointRequest(this);
                case MessageID.RelModLevel: return new GetModulationRequest(this);

                // Pressure / flow
                case MessageID.CHPressure: return new GetPressureRequest(this);
                case MessageID.DHWFlowRate: return new GetDHWFlowRateRequest(this);

                // Time / calendar
                case MessageID.DayTime: return new SetDayTimeRequest(this);
                case MessageID.Date: return new SetDateRequest(this);
                case MessageID.Year: return new SetYearRequest(this);

                // CH2 room setpoint
                case MessageID.TrSetCH2: return new SetRoomSetpointCH2Request(this);

                // Temperatures
                case MessageID.Tr: return new GetRoomTemperatureRequest(this);
                case MessageID.Tboiler: return new GetBoilerTemperatureRequest(this);
                case MessageID.Tdhw: return new GetDWHSetPointRequest(this);
                case MessageID.Toutside: return new GetOutsideTemperatureRequest(this);
                case MessageID.Tret: return new GetReturnTemperatureRequest(this);
                case MessageID.Tstorage: return new GetStorageTemperatureRequest(this);
                case MessageID.Tcollector: return new GetCollectorTemperatureRequest(this);
                case MessageID.TflowCH2: return new GetCH2FlowTemperatureRequest(this);
                case MessageID.Tdhw2: return new GetDHW2TemperatureRequest(this);
                case MessageID.Texhaust: return new GetExhaustTemperatureRequest(this);
                case MessageID.TboilerHeatExchanger: return new GetBoilerHeatExchangerTemperatureRequest(this);

                // Fan speed / flame / humidity
                case MessageID.BoilerFanSpeedSetpointAndActual: return new GetBoilerFanSpeedRequest(this);
                case MessageID.FlameCurrent: return new GetFlameCurrentRequest(this);
                case MessageID.TrCH2: return new GetRoomTemperatureCH2Request(this);
                case MessageID.RelativeHumidity: return new GetRelativeHumidityRequest(this);

                // Bounds and max setpoints
                case MessageID.TdhwSetUBTdhwSetLB: return new GetDhwSetpointBoundsRequest(this);
                case MessageID.MaxTSetUBMaxTSetLB: return new GetMaxTSetBoundsRequest(this);
                case MessageID.TdhwSet:
                    break;
                case MessageID.MaxTSet: return new SetMaxCHSetpointRequest(this);

                // Ventilation / heat recovery
                case MessageID.StatusVentilationHeatRecovery: return new GetVentilationStatusRequest(this);
                case MessageID.Vset: return new SetVentilationPositionRequest(this);
                case MessageID.ASFflagsOEMfaultCodeVentilationHeatRecovery: return new GetVentilationFaultRequest(this);
                case MessageID.OEMDiagnosticCodeVentilationHeatRecovery: return new GetVentilationOEMDiagnosticCodeRequest(this);
                case MessageID.SConfigSMemberIDCodeVentilationHeatRecovery: return new GetVentilationSConfigRequest(this);
                case MessageID.OpenThermVersionVentilationHeatRecovery: return new GetOpenThermVersionVentilationRequest(this);
                case MessageID.VentilationHeatRecoveryVersion: return new GetVentilationVersionRequest(this);
                case MessageID.RelVentLevel: return new GetRelativeVentilationLevelRequest(this);
                case MessageID.RHexhaust: return new GetRelativeHumidityExhaustRequest(this);
                case MessageID.CO2exhaust: return new GetCO2ExhaustRequest(this);
                case MessageID.Tsi: return new GetSupplyInletTemperatureRequest(this);
                case MessageID.Tso: return new GetSupplyOutletTemperatureRequest(this);
                case MessageID.Tei: return new GetExhaustInletTemperatureRequest(this);
                case MessageID.Teo: return new GetExhaustOutletTemperatureRequest(this);
                case MessageID.RPMexhaust: return new GetExhaustFanSpeedRequest(this);
                case MessageID.RPMsupply: return new GetSupplyFanSpeedRequest(this);
                case MessageID.RBPflagsVentilationHeatRecovery: return new GetRemoteVentilationParameterFlagsRequest(this);
                case MessageID.NominalVentilationValue: return new SetNominalVentilationValueRequest(this);
                case MessageID.TSPventilationHeatRecovery: return new GetSolarStorageTSPCountRequest(this);
                case MessageID.TSPindexTSPvalueVentilationHeatRecovery: return new GetSolarStorageTSPRequest(this);
                case MessageID.FHBsizeVentilationHeatRecovery: return new GetSolarStorageFHBSizeRequest(this);
                case MessageID.FHBindexFHBvalueVentilationHeatRecovery: return new GetSolarStorageFHBEntryRequest(this);

                // Branding and product info
                case MessageID.Brand: return new GetManufacturerRequest(this);
                case MessageID.BrandVersion: return new GetManufacturerVersionRequest(this);
                case MessageID.BrandSerialNumber: return new GetSerialRequest(this);

                // Counters & diagnostics
                case MessageID.CoolingOperationHours: return new GetCoolingOperationHoursRequest(this);
                case MessageID.PowerCycles: return new GetPowerCyclesRequest(this);
                case MessageID.RFsensorStatusInformation: return new GetRFsensorStatusInformationRequest(this);
                case MessageID.RemoteOverrideOperatingModeHeatingDHW: return new SetRemoteOverrideOperatingModeRequest(this);
                case MessageID.RemoteOverrideFunction: return new SetRemoteOverrideFunctionRequest(this);
                case MessageID.OEMDiagnosticCode: return new GetOEMDiagnosticCodeRequest(this);
                case MessageID.UnsuccessfulBurnerStarts: return new GetUnsuccessfulBurnerStartsRequest(this);
                case MessageID.FlameSignalTooLowNumber: return new GetFlameSignalTooLowNumberRequest(this);
                case MessageID.SuccessfulBurnerStarts: return new GetSuccessfulBurnerStartsRequest(this);
                case MessageID.CHPumpStarts: return new GetCHPumpStartsRequest(this);
                case MessageID.DHWPumpValveStarts: return new GetDHWPumpValveStartsRequest(this);
                case MessageID.DHWBurnerStarts: return new GetDHWBurnerStartsRequest(this);
                case MessageID.BurnerOperationHours: return new GetBurnerOperationHoursRequest(this);
                case MessageID.CHPumpOperationHours: return new GetCHPumpOperationHoursRequest(this);
                case MessageID.DHWPumpValveOperationHours: return new GetDHWPumpValveOperationHoursRequest(this);
                case MessageID.DHWBurnerOperationHours: return new GetDHWBurnerOperationHoursRequest(this);

                // Electricity producer stats
                case MessageID.ElectricityProducerStarts: return new GetElectricityProducerStartsRequest(this);
                case MessageID.ElectricityProducerHours: return new GetElectricityProducerHoursRequest(this);
                case MessageID.ElectricityProduction: return new GetElectricityProductionRequest(this);
                case MessageID.CumulativElectricityProduction: return new GetCumulativeElectricityProductionRequest(this);

                // Versions
                case MessageID.OpenThermVersionMaster: return new GetOpenThermVersionMasterRequest(this);
                case MessageID.OpenThermVersionSlave: return new GetOpenThermVersionSlaveRequest(this);
                case MessageID.MasterVersion:
                    break;
                case MessageID.SlaveVersion:
                    break;

                // Solar storage
                case MessageID.StatusSolarStorage: return new GetSolarStorageStatusRequest(this);
                case MessageID.ASFflagsOEMfaultCodeSolarStorage:
                    break;
                case MessageID.SConfigSMemberIDcodeSolarStorage: return new GetSolarStorageSConfigRequest(this);
                case MessageID.SolarStorageVersion: return new GetSolarStorageVersionRequest(this);
                case MessageID.TSPSolarStorage: return new GetSolarStorageTSPCountRequest(this);
                case MessageID.TSPindexTSPvalueSolarStorage: return new GetSolarStorageTSPRequest(this);
                case MessageID.FHBsizeSolarStorage: return new GetSolarStorageFHBSizeRequest(this);
                case MessageID.FHBindexFHBvalueSolarStorage: return new GetSolarStorageFHBEntryRequest(this);

                default:
                    return this;
            }
            return this; // placeholders kept for visibility of missing mappings
        }

        /// <summary>Processes request</summary>
        protected ulong ProcessRequest(ulong data)
        {
            data |= (((ulong)MessageType) & 0x7) << 28;
            data |= ((ulong)MessageID) << 16;
            if (!Utilities.Parity(data))
                data |= (1ul << 31);
            return data;
        }

        /// <summary>Is Valid Request?</summary>
        public bool IsValidRequest()
        {
            var raw = GetRawDataCore();
            if (!Utilities.Parity(raw))
                return false;
            var msgType = (MessageType)((raw >> 28) & 0x7);
            if (msgType != MessageType.READ_DATA && msgType != MessageType.WRITE_DATA)
                return false;
            return OpenThermAccess.IsOperationAllowed(MessageID, msgType);
        }
    }
}
