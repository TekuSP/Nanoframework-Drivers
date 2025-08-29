using System;

using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.DriverBase.Interfaces;

namespace TekuSP.Drivers.Nano_OpenTherm.Responses
{
    /// <summary>
    /// Response data from OpenTherm device
    /// </summary>
    public abstract class Response : IOpenThermData
    {
        /// <summary>
        /// Base constructor.
        /// </summary>
        protected Response() { }

        /// <summary>
        /// Initializes this response from another already-parsed response instance.
        /// Copies MessageType and decodes RawData via SetRawDataCore.
        /// </summary>
        /// <param name="baseResponse">Existing response with raw frame and message type.</param>
        protected Response(Response baseResponse)
        {
            MessageType = baseResponse.MessageType;
            SetRawDataCore(baseResponse.RawData);
        }
        /// <summary>
        /// Encoded 32-bit OpenTherm frame for this response.
        /// Derived classes should pack/unpack payload in <see cref="GetRawDataCore"/>/<see cref="SetRawDataCore"/>.
        /// </summary>
        public uint RawData
        {
            get => GetRawDataCore();
            set => SetRawDataCore(value);
        }

        /// <summary>
        /// Derived classes encode the frame here by packing current properties and calling <see cref="ProcessResponse(uint)"/>.
        /// </summary>
        protected abstract uint GetRawDataCore();
        /// <summary>
        /// Derived classes decode the provided frame here, updating their properties from the low 16-bit payload.
        /// </summary>
        protected abstract void SetRawDataCore(uint value);

        public abstract MessageType MessageType { get; set; }

        public abstract MessageID MessageID
        {
            get;
        }
        /// <summary>
        /// Creates a strongly-typed response wrapper based on <see cref="MessageID"/>.
        /// </summary>
        /// <remarks>
        /// Use this after parsing a raw frame (e.g., from <see cref="ReceivedResponse"/>)
        /// to obtain a convenient model with decoded properties for the given message.
        /// For unknown IDs, returns <c>this</c> unchanged so callers can still access <see cref="RawData"/>.
        /// </remarks>
        /// <returns>New typed <see cref="Response"/> instance where applicable; otherwise <c>this</c>.</returns>
        public Response SelectResponse()
        {
            return MessageID switch
            {
                MessageID.Status => new StatusResponse(this),
                MessageID.SConfigSMemberIDcode => new SlaveConfigResponse(this),
                MessageID.SConfigSMemberIDcodeSolarStorage => new SolarStorageSConfigResponse(this),
                MessageID.ASFflags => new ApplicationFaultCodesResponse(this),
                MessageID.RBPflags => new RemoteBoilerParameterResponse(this),
                MessageID.TrOverride => new RemoteOverrideRoomSetPointResponse(this),
                MessageID.TSP => new TransparentSlaveParametersCountResponse(this),
                MessageID.TSPindexTSPvalue => new TransparentSlaveParameterResponse(this),
                MessageID.FHBsize => new FaultHistoryBufferSizeResponse(this),
                MessageID.FHBindexFHBvalue => new FaultHistoryBufferEntryResponse(this),
                MessageID.MaxCapacityMinModLevel => new MaxCapacityMinModLevelResponse(this),
                MessageID.RelModLevel => new RelModulationResponse(this),
                MessageID.CHPressure => new CHPressureResponse(this),
                MessageID.DHWFlowRate => new DHWFlowRateResponse(this),
                MessageID.DayTime => new DayTimeResponse(this),
                MessageID.Date => new DateResponse(this),
                MessageID.Year => new YearResponse(this),
                MessageID.Tboiler => new BoilerTemperatureResponse(this),
                MessageID.Tdhw => new DHWTemperatureResponse(this),
                MessageID.Toutside => new OutsideTemperatureResponse(this),
                MessageID.Tret => new ReturnTemperatureResponse(this),
                MessageID.Tr => new RoomTemperatureResponse(this),
                MessageID.Tstorage => new StorageTemperatureResponse(this),
                MessageID.OpenThermVersionMaster => new OpenThermVersionMasterResponse(this),
                MessageID.Tcollector => new CollectorTemperatureResponse(this),
                MessageID.TflowCH2 => new CH2FlowTemperatureResponse(this),
                MessageID.Tdhw2 => new DHW2TemperatureResponse(this),
                MessageID.Texhaust => new ExhaustTemperatureResponse(this),
                MessageID.TboilerHeatExchanger => new BoilerHeatExchangerTemperatureResponse(this),
                MessageID.BoilerFanSpeedSetpointAndActual => new BoilerFanSpeedResponse(this),
                MessageID.FlameCurrent => new FlameCurrentResponse(this),
                MessageID.TrCH2 => new RoomTemperatureCH2Response(this),
                MessageID.RelativeHumidity => new RelativeHumidityResponse(this),
                MessageID.TrOverride2 => new RemoteOverrideRoomSetPoint2Response(this),
                MessageID.TdhwSetUBTdhwSetLB => new DhwSetpointBoundsResponse(this),
                MessageID.OTCHCRatioBounds => new OTCHCRatioBoundsResponse(this),
                MessageID.OTCHeatCurveRatio => new OTCHeatCurveRatioResponse(this),
                MessageID.MaxTSetUBMaxTSetLB => new MaxTSetBoundsResponse(this),
                MessageID.StatusVentilationHeatRecovery => new VentilationStatusResponse(this),
                MessageID.Vset => new VentilationPositionResponse(this),
                MessageID.ASFflagsOEMfaultCodeVentilationHeatRecovery => new VentilationApplicationFaultCodesResponse(this),
                MessageID.OEMDiagnosticCodeVentilationHeatRecovery => new VentilationOEMDiagnosticCodeResponse(this),
                MessageID.SConfigSMemberIDCodeVentilationHeatRecovery => new VentilationSConfigResponse(this),
                MessageID.OpenThermVersionVentilationHeatRecovery => new VentilationOpenThermVersionResponse(this),
                MessageID.VentilationHeatRecoveryVersion => new VentilationProductVersionResponse(this),
                MessageID.RelVentLevel => new RelativeVentilationLevelResponse(this),
                MessageID.RHexhaust => new RelativeHumidityExhaustResponse(this),
                MessageID.CO2exhaust => new CO2ExhaustResponse(this),
                MessageID.Tsi => new SupplyInletTemperatureResponse(this),
                MessageID.Tso => new SupplyOutletTemperatureResponse(this),
                MessageID.Tei => new ExhaustInletTemperatureResponse(this),
                MessageID.Teo => new ExhaustOutletTemperatureResponse(this),
                MessageID.RPMexhaust => new ExhaustFanSpeedResponse(this),
                MessageID.RPMsupply => new SupplyFanSpeedResponse(this),
                MessageID.RBPflagsVentilationHeatRecovery => new RemoteVentilationParameterResponse(this),
                MessageID.NominalVentilationValue => new NominalVentilationValueResponse(this),
                MessageID.TSPventilationHeatRecovery => new VentilationTSPCountResponse(this),
                MessageID.TSPindexTSPvalueVentilationHeatRecovery => new VentilationTSPValueResponse(this),
                MessageID.FHBsizeVentilationHeatRecovery => new VentilationFHBSizeResponse(this),
                MessageID.FHBindexFHBvalueVentilationHeatRecovery => new VentilationFHBEntryResponse(this),
                MessageID.Brand => new BrandCharacterResponse(this),
                MessageID.BrandVersion => new BrandVersionCharacterResponse(this),
                MessageID.BrandSerialNumber => new BrandSerialByteResponse(this),
                MessageID.CoolingOperationHours => new CoolingOperationHoursResponse(this),
                MessageID.PowerCycles => new PowerCyclesResponse(this),
                MessageID.RFsensorStatusInformation => new RFsensorStatusInformationResponse(this),
                MessageID.RemoteOverrideOperatingModeHeatingDHW => new RemoteOverrideOperatingModeResponse(this),
                MessageID.RemoteOverrideFunction => new RemoteOverrideFunctionResponse(this),
                MessageID.StatusSolarStorage => new SolarStorageStatusResponse(this),
                MessageID.ASFflagsOEMfaultCodeSolarStorage => new SolarStorageApplicationFaultCodesResponse(this),
                MessageID.UnsuccessfulBurnerStarts => new UnsuccessfulBurnerStartsResponse(this),
                MessageID.FlameSignalTooLowNumber => new FlameSignalTooLowNumberResponse(this),
                MessageID.OEMDiagnosticCode => new OEMDiagnosticCodeResponse(this),
                MessageID.SuccessfulBurnerStarts => new SuccessfulBurnerStartsResponse(this),
                MessageID.CHPumpStarts => new CHPumpStartsResponse(this),
                MessageID.DHWPumpValveStarts => new DHWPumpValveStartsResponse(this),
                MessageID.DHWBurnerStarts => new DHWBurnerStartsResponse(this),
                MessageID.BurnerOperationHours => new BurnerOperationHoursResponse(this),
                MessageID.CHPumpOperationHours => new CHPumpOperationHoursResponse(this),
                MessageID.DHWPumpValveOperationHours => new DHWPumpValveOperationHoursResponse(this),
                MessageID.DHWBurnerOperationHours => new DHWBurnerOperationHoursResponse(this),
                MessageID.SolarStorageVersion => new SolarStorageProductVersionResponse(this),
                MessageID.TSPSolarStorage => new SolarStorageTSPCountResponse(this),
                MessageID.TSPindexTSPvalueSolarStorage => new SolarStorageTSPValueResponse(this),
                MessageID.FHBsizeSolarStorage => new SolarStorageFHBSizeResponse(this),
                MessageID.FHBindexFHBvalueSolarStorage => new SolarStorageFHBEntryResponse(this),
                MessageID.ElectricityProducerStarts => new ElectricityProducerStartsResponse(this),
                MessageID.ElectricityProducerHours => new ElectricityProducerHoursResponse(this),
                MessageID.ElectricityProduction => new ElectricityProductionResponse(this),
                MessageID.CumulativElectricityProduction => new CumulativeElectricityProductionResponse(this),
                MessageID.OpenThermVersionSlave => new OpenThermVersionSlaveResponse(this),
                MessageID.MasterVersion => new MasterProductVersionResponse(this),
                MessageID.SlaveVersion => new SlaveProductVersionResponse(this),
                MessageID.Remeha131 => new Remeha131Response(this),
                MessageID.Remeha132 => new Remeha132Response(this),
                MessageID.Remeha133 => new Remeha133Response(this),
                MessageID.TSet => new ControlSetpointResponse(this),
                MessageID.MConfigMMemberIDcode => new MasterConfigResponse(this),
                MessageID.RemoteRequest => new RemoteRequestResponse(this),
                MessageID.CoolingControl => new CoolingControlResponse(this),
                MessageID.TsetCH2 => new ControlSetpointCH2Response(this),
                MessageID.MaxRelModLevelSetting => new MaxRelModLevelSettingResponse(this),
                MessageID.TrSet => new RoomSetpointResponse(this),
                MessageID.TrSetCH2 => new RoomSetpointCH2Response(this),
                MessageID.TdhwSet => new DhwSetpointResponse(this),
                MessageID.MaxTSet => new MaxTSetResponse(this),
                _ => this,
            };
        }

        /// <summary>
        /// Processes response
        /// </summary>
        /// <param name="data">Input data</param>
        /// <returns>Returns Raw Request</returns>
        protected uint ProcessResponse(uint data)
        {
            data |= (uint)MessageType << 28;
            data |= (uint)MessageID << 16;
            // Ensure overall frame has odd parity (bit count over 32 bits is odd)
            if (!Utilities.Parity(data))
                data |= 1u << 31;
            return data;
        }
        /// <summary>
        /// Is valid Response?
        /// </summary>
        /// <returns>Validity</returns>
        public bool IsValidResponse()
        {
            // Parity over full 32-bit frame must be odd
            if (!Utilities.Parity(RawData))
                return false;
            var msgType = (byte)Utilities.GetMessageType(RawData);
            bool typeOk = msgType == (byte)MessageType.READ_ACK
                || msgType == (byte)MessageType.WRITE_ACK
                || msgType == (byte)MessageType.DATA_INVALID
                || msgType == (byte)MessageType.UNKNOWN_DATA_ID;
            if (!typeOk) return false;

            // Enforce access mode for ACKs
            if (msgType == (byte)MessageType.READ_ACK && !OpenThermAccess.IsOperationAllowed(MessageID, MessageType.READ_DATA))
                return false;
            if (msgType == (byte)MessageType.WRITE_ACK && !OpenThermAccess.IsOperationAllowed(MessageID, MessageType.WRITE_DATA))
                return false;
            return true;
        }
    }
}
