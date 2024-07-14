
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
        /// Raw ulong data from OpenTherm device
        /// </summary>
        public abstract ulong RawData
        {
            get;
            set;
        }

        public abstract MessageType MessageType
        {
            get;
        }

        public abstract MessageID MessageID
        {
            get;
        }
        /// <summary>
        /// Automatically selects correct cast of Response
        /// </summary>
        /// <returns>Casted Response</returns>
        public Response SelectResponse()
        {
            switch (MessageID)
            {
                case MessageID.Status:
                    return new StatusResponse(this);
                case MessageID.SConfigSMemberIDcode:
                    return new SlaveConfigResponse(this);
                case MessageID.ASFflags:
                    return new ApplicationFaultCodesResponse(this);
                case MessageID.RBPflags:
                    return new RemoteBoilerParameterResponse(this);
                case MessageID.TrOverride:
                    return new RemoteOverrideRoomSetPointResponse(this);
                case MessageID.TSP:
                    break;
                case MessageID.TSPindexTSPvalue:
                    break;
                case MessageID.FHBsize:
                    break;
                case MessageID.FHBindexFHBvalue:
                    break;
                case MessageID.MaxCapacityMinModLevel:
                    break;
                case MessageID.RelModLevel:
                    break;
                case MessageID.CHPressure:
                    break;
                case MessageID.DHWFlowRate:
                    break;
                case MessageID.DayTime:
                    break;
                case MessageID.Date:
                    break;
                case MessageID.Year:
                    break;
                case MessageID.Tboiler:
                    break;
                case MessageID.Tdhw:
                    break;
                case MessageID.Toutside:
                    break;
                case MessageID.Tret:
                    break;
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
                    break;
                case MessageID.BrandVersion:
                    break;
                case MessageID.BrandSerialNumber:
                    break;
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
                case MessageID.OpenThermVersionSlave:
                    break;
                case MessageID.SlaveVersion:
                    break;
                default:
                    return this;
            }
        }

        /// <summary>
        /// Processes response
        /// </summary>
        /// <param name="data">Input data</param>
        /// <returns>Returns Raw Request</returns>
        protected ulong ProcessResponse(ulong data)
        {
            data |= (ulong)MessageType << 28;
            data |= (ulong)MessageID << 16;
            if (Utilities.Parity(data))
                data |= 1ul << 31;
            return data;
        }
        /// <summary>
        /// Is valid Response?
        /// </summary>
        /// <returns>Validity</returns>
        public bool IsValidResponse()
        {
            if (Utilities.Parity(RawData))
                return false;
            var msgType = (byte)(RawData << 1 >> 29);
            return msgType == (byte)MessageType.READ_ACK || msgType == (byte)MessageType.WRITE_ACK;

        }
    }
}
