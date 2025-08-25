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
        uint IOpenThermData.RawData { get => GetRawDataCore(); set => SetRawDataCore(value); }

        /// <summary>
        /// Derived classes must provide core getters/setters. Use NotSupportedException in the accessor you don't support.
        /// </summary>
        protected abstract uint GetRawDataCore();
        protected abstract void SetRawDataCore(uint value);

        /// <summary>Message Type</summary>
        public abstract MessageType MessageType { get; }
        /// <summary>Message ID</summary>
        public abstract MessageID MessageID { get; }

        /// <summary>Returns the encoded 32-bit OpenTherm frame for this request.</summary>
        public uint BuildFrame() => GetRawDataCore();

        /// <summary>
        /// Automatically selects a strongly-typed Request from a received frame
        /// </summary>
        /// <returns>Request instance matching the MessageID when available, otherwise this</returns>
        public Request SelectRequest()
        {
            return MessageID switch
            {
                // Status and core control
                MessageID.Status => new SetBoilerStatusRequest(this),
                MessageID.TSet => new SetBoilerTemperatureRequest(this),
                MessageID.MConfigMMemberIDcode => new GetMasterConfigurationRequest(this),
                MessageID.SConfigSMemberIDcode => new GetSlaveConfigurationRequest(this),
                MessageID.RemoteRequest => new GetRemoteRequestRequest(this),
                // Faults / flags
                MessageID.ASFflags => new GetFaultRequest(this),
                MessageID.RBPflags => new GetRemoteBoilerParameterFlagsRequest(this),
                // Cooling and CH2 setpoint
                MessageID.CoolingControl => new SetCoolingControlRequest(this),
                MessageID.TsetCH2 => new SetCH2SetpointRequest(this),
                MessageID.TrOverride => new SetRoomOverrideRequest(this),
                MessageID.TrOverride2 => new SetRoomOverride2Request(this),
                // Transparent slave parameters and fault history buffer
                MessageID.TSP => new GetTransparentSlaveParametersCountRequest(this),
                MessageID.TSPindexTSPvalue => new GetTransparentSlaveParameterRequest(this),
                MessageID.FHBsize => new GetFaultHistoryBufferSizeRequest(this),
                MessageID.FHBindexFHBvalue => new GetFaultHistoryBufferEntryRequest(this),
                // Capacity / modulation
                MessageID.MaxRelModLevelSetting => new SetMaxRelModulationRequest(this),
                MessageID.MaxCapacityMinModLevel => new GetBoilerCapacityAndMinModRequest(this),
                // Room setpoints and levels
                MessageID.TrSet => new SetRoomSetpointRequest(this),
                MessageID.RelModLevel => new GetModulationRequest(this),
                // Pressure / flow
                MessageID.CHPressure => new GetPressureRequest(this),
                MessageID.DHWFlowRate => new GetDHWFlowRateRequest(this),
                // Time / calendar
                MessageID.DayTime => new SetDayTimeRequest(this),
                MessageID.Date => new SetDateRequest(this),
                MessageID.Year => new SetYearRequest(this),
                // CH2 room setpoint
                MessageID.TrSetCH2 => new SetRoomSetpointCH2Request(this),
                // Temperatures
                MessageID.Tr => new GetRoomTemperatureRequest(this),
                MessageID.Tboiler => new GetBoilerTemperatureRequest(this),
                MessageID.Tdhw => new GetDWHSetPointRequest(this),
                MessageID.Toutside => new GetOutsideTemperatureRequest(this),
                MessageID.Tret => new GetReturnTemperatureRequest(this),
                MessageID.Tstorage => new GetStorageTemperatureRequest(this),
                MessageID.Tcollector => new GetCollectorTemperatureRequest(this),
                MessageID.TflowCH2 => new GetCH2FlowTemperatureRequest(this),
                MessageID.Tdhw2 => new GetDHW2TemperatureRequest(this),
                MessageID.Texhaust => new GetExhaustTemperatureRequest(this),
                MessageID.TboilerHeatExchanger => new GetBoilerHeatExchangerTemperatureRequest(this),
                // Fan speed / flame / humidity
                MessageID.BoilerFanSpeedSetpointAndActual => new GetBoilerFanSpeedRequest(this),
                MessageID.FlameCurrent => new GetFlameCurrentRequest(this),
                MessageID.TrCH2 => new GetRoomTemperatureCH2Request(this),
                MessageID.RelativeHumidity => new GetRelativeHumidityRequest(this),
                // Bounds and max setpoints
                MessageID.TdhwSetUBTdhwSetLB => new GetDhwSetpointBoundsRequest(this),
                MessageID.MaxTSetUBMaxTSetLB => new GetMaxTSetBoundsRequest(this),
                MessageID.TdhwSet => new SetDWHSetPointRequest(this),
                MessageID.MaxTSet => new SetMaxCHSetpointRequest(this),
                // Ventilation / heat recovery
                MessageID.StatusVentilationHeatRecovery => new GetVentilationStatusRequest(this),
                MessageID.Vset => new SetVentilationPositionRequest(this),
                MessageID.ASFflagsOEMfaultCodeVentilationHeatRecovery => new GetVentilationFaultRequest(this),
                MessageID.OEMDiagnosticCodeVentilationHeatRecovery => new GetVentilationOEMDiagnosticCodeRequest(this),
                MessageID.SConfigSMemberIDCodeVentilationHeatRecovery => new GetVentilationSConfigRequest(this),
                MessageID.OpenThermVersionVentilationHeatRecovery => new GetOpenThermVersionVentilationRequest(this),
                MessageID.VentilationHeatRecoveryVersion => new GetVentilationVersionRequest(this),
                MessageID.RelVentLevel => new GetRelativeVentilationLevelRequest(this),
                MessageID.RHexhaust => new GetRelativeHumidityExhaustRequest(this),
                MessageID.CO2exhaust => new GetCO2ExhaustRequest(this),
                MessageID.Tsi => new GetSupplyInletTemperatureRequest(this),
                MessageID.Tso => new GetSupplyOutletTemperatureRequest(this),
                MessageID.Tei => new GetExhaustInletTemperatureRequest(this),
                MessageID.Teo => new GetExhaustOutletTemperatureRequest(this),
                MessageID.RPMexhaust => new GetExhaustFanSpeedRequest(this),
                MessageID.RPMsupply => new GetSupplyFanSpeedRequest(this),
                MessageID.RBPflagsVentilationHeatRecovery => new GetRemoteVentilationParameterFlagsRequest(this),
                MessageID.NominalVentilationValue => new SetNominalVentilationValueRequest(this),
                MessageID.TSPventilationHeatRecovery => new GetVentilationTSPCountRequest(this),
                MessageID.TSPindexTSPvalueVentilationHeatRecovery => new GetVentilationTSPRequest(this),
                MessageID.FHBsizeVentilationHeatRecovery => new GetVentilationFHBSizeRequest(this),
                MessageID.FHBindexFHBvalueVentilationHeatRecovery => new GetVentilationFHBEntryRequest(this),
                // Branding and product info
                MessageID.Brand => new GetManufacturerRequest(this),
                MessageID.BrandVersion => new GetManufacturerVersionRequest(this),
                MessageID.BrandSerialNumber => new GetSerialRequest(this),
                // Counters & diagnostics
                MessageID.CoolingOperationHours => new GetCoolingOperationHoursRequest(this),
                MessageID.PowerCycles => new GetPowerCyclesRequest(this),
                MessageID.RFsensorStatusInformation => new GetRFsensorStatusInformationRequest(this),
                MessageID.RemoteOverrideOperatingModeHeatingDHW => new SetRemoteOverrideOperatingModeRequest(this),
                MessageID.RemoteOverrideFunction => new SetRemoteOverrideFunctionRequest(this),
                MessageID.OEMDiagnosticCode => new GetOEMDiagnosticCodeRequest(this),
                MessageID.UnsuccessfulBurnerStarts => new GetUnsuccessfulBurnerStartsRequest(this),
                MessageID.FlameSignalTooLowNumber => new GetFlameSignalTooLowNumberRequest(this),
                MessageID.SuccessfulBurnerStarts => new GetSuccessfulBurnerStartsRequest(this),
                MessageID.CHPumpStarts => new GetCHPumpStartsRequest(this),
                MessageID.DHWPumpValveStarts => new GetDHWPumpValveStartsRequest(this),
                MessageID.DHWBurnerStarts => new GetDHWBurnerStartsRequest(this),
                MessageID.BurnerOperationHours => new GetBurnerOperationHoursRequest(this),
                MessageID.CHPumpOperationHours => new GetCHPumpOperationHoursRequest(this),
                MessageID.DHWPumpValveOperationHours => new GetDHWPumpValveOperationHoursRequest(this),
                MessageID.DHWBurnerOperationHours => new GetDHWBurnerOperationHoursRequest(this),
                // Electricity producer stats
                MessageID.ElectricityProducerStarts => new GetElectricityProducerStartsRequest(this),
                MessageID.ElectricityProducerHours => new GetElectricityProducerHoursRequest(this),
                MessageID.ElectricityProduction => new GetElectricityProductionRequest(this),
                MessageID.CumulativElectricityProduction => new GetCumulativeElectricityProductionRequest(this),
                // Versions
                MessageID.OpenThermVersionMaster => new GetOpenThermVersionMasterRequest(this),
                MessageID.OpenThermVersionSlave => new GetOpenThermVersionSlaveRequest(this),
                MessageID.MasterVersion => new GetMasterVersionRequest(this),
                MessageID.SlaveVersion => new GetSlaveVersionRequest(this),
                // Solar storage
                MessageID.StatusSolarStorage => new GetSolarStorageStatusRequest(this),
                MessageID.ASFflagsOEMfaultCodeSolarStorage => new GetSolarStorageFaultRequest(this),
                MessageID.SConfigSMemberIDcodeSolarStorage => new GetSolarStorageSConfigRequest(this),
                MessageID.SolarStorageVersion => new GetSolarStorageVersionRequest(this),
                MessageID.TSPSolarStorage => new GetSolarStorageTSPCountRequest(this),
                MessageID.TSPindexTSPvalueSolarStorage => new GetSolarStorageTSPRequest(this),
                MessageID.FHBsizeSolarStorage => new GetSolarStorageFHBSizeRequest(this),
                MessageID.FHBindexFHBvalueSolarStorage => new GetSolarStorageFHBEntryRequest(this),
                _ => this,
            };
        }

        /// <summary>Processes request</summary>
        protected uint ProcessRequest(uint data)
        {
            data |= (uint)(((uint)MessageType & 0x7) << 28);
            data |= (uint)((uint)MessageID << 16);
            if (!Utilities.Parity(data))
                data |= (1u << 31);
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
