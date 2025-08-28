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
            switch (MessageID)
            {
                case MessageID.Status:
                    return new StatusResponse(this);
                case MessageID.SConfigSMemberIDcode:
                    return new SlaveConfigResponse(this);
                case MessageID.SConfigSMemberIDcodeSolarStorage:
                    return new SolarStorageSConfigResponse(this);
                case MessageID.ASFflags:
                    return new ApplicationFaultCodesResponse(this);
                case MessageID.RBPflags:
                    return new RemoteBoilerParameterResponse(this);
                case MessageID.TrOverride:
                    return new RemoteOverrideRoomSetPointResponse(this);
                case MessageID.TSP:
                    return new TransparentSlaveParametersCountResponse(this);
                case MessageID.TSPindexTSPvalue:
                    return new TransparentSlaveParameterResponse(this);
                case MessageID.FHBsize:
                    return new FaultHistoryBufferSizeResponse(this);
                case MessageID.FHBindexFHBvalue:
                    return new FaultHistoryBufferEntryResponse(this);
                case MessageID.MaxCapacityMinModLevel:
                    return new MaxCapacityMinModLevelResponse(this);
                case MessageID.RelModLevel:
                    return new RelModulationResponse(this);
                case MessageID.CHPressure:
                    return new CHPressureResponse(this);
                case MessageID.DHWFlowRate:
                    return new DHWFlowRateResponse(this);
                case MessageID.DayTime:
                    return new DayTimeResponse(this);
                case MessageID.Date:
                    return new DateResponse(this);
                case MessageID.Year:
                    return new YearResponse(this);
                case MessageID.Tboiler:
                    return new BoilerTemperatureResponse(this);
                case MessageID.Tdhw:
                    return new DHWTemperatureResponse(this);
                case MessageID.Toutside:
                    return new OutsideTemperatureResponse(this);
                case MessageID.Tret:
                    return new ReturnTemperatureResponse(this);
                case MessageID.Tr:
                    return new RoomTemperatureResponse(this);
                case MessageID.Tstorage:
                    return new StorageTemperatureResponse(this);
                case MessageID.OpenThermVersionMaster:
                    return new OpenThermVersionMasterResponse(this);
                case MessageID.Tcollector:
                    return new CollectorTemperatureResponse(this);
                case MessageID.TflowCH2:
                    return new CH2FlowTemperatureResponse(this);
                case MessageID.Tdhw2:
                    return new DHW2TemperatureResponse(this);
                case MessageID.Texhaust:
                    return new ExhaustTemperatureResponse(this);
                case MessageID.TboilerHeatExchanger:
                    return new BoilerHeatExchangerTemperatureResponse(this);
                case MessageID.BoilerFanSpeedSetpointAndActual:
                    break;
                case MessageID.FlameCurrent:
                    return new FlameCurrentResponse(this);
                case MessageID.TrCH2:
                    return new RoomTemperatureCH2Response(this);
                case MessageID.RelativeHumidity:
                    return new RelativeHumidityResponse(this);
                case MessageID.TrOverride2:
                    break;
                case MessageID.TdhwSetUBTdhwSetLB:
                    return new DhwSetpointBoundsResponse(this);
                case MessageID.MaxTSetUBMaxTSetLB:
                    return new MaxTSetBoundsResponse(this);
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
                    return new VentilationOpenThermVersionResponse(this);
                case MessageID.VentilationHeatRecoveryVersion:
                    return new VentilationProductVersionResponse(this);
                case MessageID.RelVentLevel:
                    return new RelativeVentilationLevelResponse(this);
                case MessageID.RHexhaust:
                    return new RelativeHumidityExhaustResponse(this);
                case MessageID.CO2exhaust:
                    return new CO2ExhaustResponse(this);
                case MessageID.Tsi:
                    return new SupplyInletTemperatureResponse(this);
                case MessageID.Tso:
                    return new SupplyOutletTemperatureResponse(this);
                case MessageID.Tei:
                    return new ExhaustInletTemperatureResponse(this);
                case MessageID.Teo:
                    return new ExhaustOutletTemperatureResponse(this);
                case MessageID.RPMexhaust:
                    return new ExhaustFanSpeedResponse(this);
                case MessageID.RPMsupply:
                    return new SupplyFanSpeedResponse(this);
                case MessageID.RBPflagsVentilationHeatRecovery:
                    break;
                case MessageID.NominalVentilationValue:
                    return new NominalVentilationValueResponse(this);
                case MessageID.TSPventilationHeatRecovery:
                    return new VentilationTSPCountResponse(this);
                case MessageID.TSPindexTSPvalueVentilationHeatRecovery:
                    return new VentilationTSPValueResponse(this);
                case MessageID.FHBsizeVentilationHeatRecovery:
                    return new VentilationFHBSizeResponse(this);
                case MessageID.FHBindexFHBvalueVentilationHeatRecovery:
                    return new VentilationFHBEntryResponse(this);
                case MessageID.Brand:
                    return new BrandCharacterResponse(this);
                case MessageID.BrandVersion:
                    return new BrandVersionCharacterResponse(this);
                case MessageID.BrandSerialNumber:
                    return new BrandSerialByteResponse(this);
                case MessageID.CoolingOperationHours:
                    return new CoolingOperationHoursResponse(this);
                case MessageID.PowerCycles:
                    return new PowerCyclesResponse(this);
                case MessageID.RFsensorStatusInformation:
                    break;
                case MessageID.RemoteOverrideOperatingModeHeatingDHW:
                    break;
                case MessageID.RemoteOverrideFunction:
                    return new RemoteOverrideFunctionResponse(this);
                case MessageID.StatusSolarStorage:
                    break;
                case MessageID.ASFflagsOEMfaultCodeSolarStorage:
                    break;
                case MessageID.UnsuccessfulBurnerStarts:
                    return new UnsuccessfulBurnerStartsResponse(this);
                case MessageID.FlameSignalTooLowNumber:
                    return new FlameSignalTooLowNumberResponse(this);
                case MessageID.OEMDiagnosticCode:
                    break;
                case MessageID.SuccessfulBurnerStarts:
                    return new SuccessfulBurnerStartsResponse(this);
                case MessageID.CHPumpStarts:
                    return new CHPumpStartsResponse(this);
                case MessageID.DHWPumpValveStarts:
                    return new DHWPumpValveStartsResponse(this);
                case MessageID.DHWBurnerStarts:
                    return new DHWBurnerStartsResponse(this);
                case MessageID.BurnerOperationHours:
                    return new BurnerOperationHoursResponse(this);
                case MessageID.CHPumpOperationHours:
                    return new CHPumpOperationHoursResponse(this);
                case MessageID.DHWPumpValveOperationHours:
                    return new DHWPumpValveOperationHoursResponse(this);
                case MessageID.DHWBurnerOperationHours:
                    return new DHWBurnerOperationHoursResponse(this);
                case MessageID.SolarStorageVersion:
                    break;
                case MessageID.TSPSolarStorage:
                    return new SolarStorageTSPCountResponse(this);
                case MessageID.TSPindexTSPvalueSolarStorage:
                    return new SolarStorageTSPValueResponse(this);
                case MessageID.FHBsizeSolarStorage:
                    return new SolarStorageFHBSizeResponse(this);
                case MessageID.FHBindexFHBvalueSolarStorage:
                    break;
                case MessageID.ElectricityProducerStarts:
                    return new ElectricityProducerStartsResponse(this);
                case MessageID.ElectricityProducerHours:
                    return new ElectricityProducerHoursResponse(this);
                case MessageID.ElectricityProduction:
                    return new ElectricityProductionResponse(this);
                case MessageID.CumulativElectricityProduction:
                    return new CumulativeElectricityProductionResponse(this);
                case MessageID.OpenThermVersionSlave:
                    return new OpenThermVersionSlaveResponse(this);
                case MessageID.MasterVersion:
                    return new MasterProductVersionResponse(this);
                case MessageID.SlaveVersion:
                    return new SlaveProductVersionResponse(this);
                default:
                    return this;
            }
            // Unreachable
            return this;
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
