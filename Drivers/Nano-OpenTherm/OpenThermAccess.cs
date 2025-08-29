using System;

using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;

namespace TekuSP.Drivers.Nano_OpenTherm
{
    public static class OpenThermAccess
    {
        public static MessageType GetMessageType(AccessMode accessMode)
        {
            if (accessMode == AccessMode.Read)
                return MessageType.READ_DATA;
            if (accessMode == AccessMode.Write)
                return MessageType.WRITE_DATA;

            return MessageType.UNKNOWN_DATA_ID; // ambiguous, could be either read or write
        }
        public static MessageType GetMessageType(MessageID id) => GetMessageType(GetAccess(id));
        public static AccessMode GetAccess(MessageID id)
        {
            switch (id)
            {
                // Core control and status
                case MessageID.Status: return AccessMode.ReadWrite;
                case MessageID.TSet: return AccessMode.Write; // control setpoint from master
                case MessageID.MConfigMMemberIDcode: return AccessMode.Write; // master writes its config to slave per v2.2
                case MessageID.SConfigSMemberIDcode: return AccessMode.Read;
                case MessageID.RemoteRequest: return AccessMode.ReadWrite;
                case MessageID.ASFflags: return AccessMode.Read;
                case MessageID.RBPflags: return AccessMode.ReadWrite;
                case MessageID.CoolingControl: return AccessMode.Write;
                case MessageID.TsetCH2: return AccessMode.Write;
                case MessageID.TrOverride: return AccessMode.Read; // v2.2: remote override setpoint is read-only
                case MessageID.TrOverride2: return AccessMode.Read; // mirror read-only for second override
                case MessageID.TSP: return AccessMode.Read;
                case MessageID.TSPindexTSPvalue: return AccessMode.ReadWrite; // v2.2: TSP index/value is RW
                case MessageID.FHBsize: return AccessMode.Read;
                case MessageID.FHBindexFHBvalue: return AccessMode.Read;
                case MessageID.MaxRelModLevelSetting: return AccessMode.Write;
                case MessageID.MaxCapacityMinModLevel: return AccessMode.Read;
                case MessageID.TrSet: return AccessMode.Write;
                case MessageID.TrSetCH2: return AccessMode.Write;

                // Measurements
                case MessageID.RelModLevel:
                case MessageID.CHPressure:
                case MessageID.DHWFlowRate:
                case MessageID.Tr:
                case MessageID.Tboiler:
                case MessageID.Tdhw:
                case MessageID.Toutside:
                case MessageID.Tret:
                case MessageID.Tstorage:
                case MessageID.Tcollector:
                case MessageID.TflowCH2:
                case MessageID.Tdhw2:
                case MessageID.Texhaust:
                case MessageID.TboilerHeatExchanger:
                case MessageID.BoilerFanSpeedSetpointAndActual:
                case MessageID.FlameCurrent:
                case MessageID.TrCH2:
                case MessageID.RelativeHumidity:
                    return AccessMode.Read;

                // Time/Date can often be set by master
                case MessageID.DayTime:
                case MessageID.Date:
                case MessageID.Year:
                    return AccessMode.ReadWrite;

                // Bounds and setpoints
                case MessageID.TdhwSetUBTdhwSetLB:
                case MessageID.MaxTSetUBMaxTSetLB:
                case MessageID.OTCHCRatioBounds:
                    return AccessMode.Read;
                case MessageID.TdhwSet:
                case MessageID.MaxTSet:
                case MessageID.OTCHeatCurveRatio:
                    return AccessMode.ReadWrite; // v2.2: RW (read back and write)

                // Ventilation/heat-recovery
                case MessageID.StatusVentilationHeatRecovery: return AccessMode.ReadWrite;
                case MessageID.Vset: return AccessMode.Write;
                case MessageID.ASFflagsOEMfaultCodeVentilationHeatRecovery: return AccessMode.Read;
                case MessageID.OEMDiagnosticCodeVentilationHeatRecovery: return AccessMode.Read;
                case MessageID.SConfigSMemberIDCodeVentilationHeatRecovery: return AccessMode.Read;
                case MessageID.OpenThermVersionVentilationHeatRecovery: return AccessMode.Read;
                case MessageID.VentilationHeatRecoveryVersion: return AccessMode.Read;
                case MessageID.RelVentLevel: return AccessMode.Read;
                case MessageID.RHexhaust: return AccessMode.Read;
                case MessageID.CO2exhaust: return AccessMode.Read;
                case MessageID.Tsi: return AccessMode.Read;
                case MessageID.Tso: return AccessMode.Read;
                case MessageID.Tei: return AccessMode.Read;
                case MessageID.Teo: return AccessMode.Read;
                case MessageID.RPMexhaust: return AccessMode.Read;
                case MessageID.RPMsupply: return AccessMode.Read;
                case MessageID.RBPflagsVentilationHeatRecovery: return AccessMode.ReadWrite;
                case MessageID.NominalVentilationValue: return AccessMode.ReadWrite;
                case MessageID.TSPventilationHeatRecovery: return AccessMode.Read;
                case MessageID.TSPindexTSPvalueVentilationHeatRecovery: return AccessMode.ReadWrite; // mirror TSP RW behavior
                case MessageID.FHBsizeVentilationHeatRecovery: return AccessMode.Read;
                case MessageID.FHBindexFHBvalueVentilationHeatRecovery: return AccessMode.Read;

                // Branding and product info
                case MessageID.Brand:
                case MessageID.BrandVersion:
                case MessageID.BrandSerialNumber:
                    return AccessMode.Read;

                // Counters (some allow reset-by-zero optional) keep as Read for safety
                case MessageID.CoolingOperationHours:
                case MessageID.PowerCycles:
                case MessageID.UnsuccessfulBurnerStarts:
                case MessageID.FlameSignalTooLowNumber:
                case MessageID.OEMDiagnosticCode:
                case MessageID.SuccessfulBurnerStarts:
                case MessageID.CHPumpStarts:
                case MessageID.DHWPumpValveStarts:
                case MessageID.DHWBurnerStarts:
                case MessageID.BurnerOperationHours:
                case MessageID.CHPumpOperationHours:
                case MessageID.DHWPumpValveOperationHours:
                case MessageID.DHWBurnerOperationHours:
                    return AccessMode.Read;

                // RF / overrides
                case MessageID.RFsensorStatusInformation: return AccessMode.Read;
                case MessageID.RemoteOverrideOperatingModeHeatingDHW: return AccessMode.ReadWrite;
                case MessageID.RemoteOverrideFunction: return AccessMode.Read; // v2.2: function flags are read-only

                // Solar storage
                case MessageID.StatusSolarStorage: return AccessMode.ReadWrite;
                case MessageID.ASFflagsOEMfaultCodeSolarStorage: return AccessMode.Read;
                case MessageID.SConfigSMemberIDcodeSolarStorage: return AccessMode.Read;
                case MessageID.SolarStorageVersion: return AccessMode.Read;
                case MessageID.TSPSolarStorage: return AccessMode.Read;
                case MessageID.TSPindexTSPvalueSolarStorage: return AccessMode.ReadWrite; // mirror TSP RW behavior
                case MessageID.FHBsizeSolarStorage: return AccessMode.Read;
                case MessageID.FHBindexFHBvalueSolarStorage: return AccessMode.Read;

                // Electricity producer
                case MessageID.ElectricityProducerStarts:
                case MessageID.ElectricityProducerHours:
                case MessageID.ElectricityProduction:
                case MessageID.CumulativElectricityProduction:
                    return AccessMode.Read;

                // Versions
                case MessageID.OpenThermVersionMaster:
                case MessageID.OpenThermVersionSlave:
                case MessageID.MasterVersion:
                case MessageID.SlaveVersion:
                    return AccessMode.Read;
                // Vendor-specific (Remeha)
                case MessageID.Remeha131: return AccessMode.ReadWrite;
                case MessageID.Remeha132: return AccessMode.Read;
                case MessageID.Remeha133: return AccessMode.Read;

                default:
                    return AccessMode.Read; // safe default
            }
        }

        public static bool IsOperationAllowed(MessageID id, MessageType type)
        {
            var access = GetAccess(id);
            if (type == MessageType.READ_DATA || type == MessageType.READ_ACK)
                return (access & AccessMode.Read) != 0;
            if (type == MessageType.WRITE_DATA || type == MessageType.WRITE_ACK)
                return (access & AccessMode.Write) != 0;
            // For DATA_INVALID/UNKNOWN_DATA_ID, allow through; higher layers will handle
            return true;
        }
    }
}
