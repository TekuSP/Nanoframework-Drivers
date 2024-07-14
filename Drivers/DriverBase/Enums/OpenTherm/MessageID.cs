namespace TekuSP.Drivers.DriverBase.Enums.OpenTherm
{
    /// <summary>
    /// Message ID for OpenTherm
    /// </summary>
    /// <remarks>
    /// This enum represents all possible message IDs defined in the OpenTherm protocol.
    /// Each entry is annotated with the type of data it represents and a brief description.
    /// </remarks>
    public enum MessageID : byte
    {
        /// <summary>
        /// Master and Slave Status flags.
        /// </summary>
        Status = 0,
        /// <summary>
        /// Control Setpoint i.e., CH water temperature Setpoint (°C).
        /// </summary>
        TSet = 1,
        /// <summary>
        /// Master Configuration Flags / Master MemberID Code.
        /// </summary>
        MConfigMMemberIDcode = 2,
        /// <summary>
        /// Slave Configuration Flags / Slave MemberID Code.
        /// </summary>
        SConfigSMemberIDcode = 3,
        /// <summary>
        /// Remote Request.
        /// </summary>
        RemoteRequest = 4,
        /// <summary>
        /// Application-specific fault flags and OEM fault code.
        /// </summary>
        ASFflags = 5,
        /// <summary>
        /// Remote boiler parameter transfer-enable & read/write flags.
        /// </summary>
        RBPflags = 6,
        /// <summary>
        /// Cooling control signal (%).
        /// </summary>
        CoolingControl = 7,
        /// <summary>
        /// Control Setpoint for 2nd CH circuit (°C).
        /// </summary>
        TsetCH2 = 8,
        /// <summary>
        /// Remote override room Setpoint.
        /// </summary>
        TrOverride = 9,
        /// <summary>
        /// Number of Transparent-Slave-Parameters supported by slave.
        /// </summary>
        TSP = 10,
        /// <summary>
        /// Index number / Value of referred-to transparent slave parameter.
        /// </summary>
        TSPindexTSPvalue = 11,
        /// <summary>
        /// Size of Fault-History-Buffer supported by slave.
        /// </summary>
        FHBsize = 12,
        /// <summary>
        /// Index number / Value of referred-to fault-history buffer entry.
        /// </summary>
        FHBindexFHBvalue = 13,
        /// <summary>
        /// Maximum relative modulation level setting (%).
        /// </summary>
        MaxRelModLevelSetting = 14,
        /// <summary>
        /// Maximum boiler capacity (kW) / Minimum boiler modulation level (%).
        /// </summary>
        MaxCapacityMinModLevel = 15,
        /// <summary>
        /// Room Setpoint (°C).
        /// </summary>
        TrSet = 16,
        /// <summary>
        /// Relative Modulation Level (%).
        /// </summary>
        RelModLevel = 17,
        /// <summary>
        /// Water pressure in CH circuit (bar).
        /// </summary>
        CHPressure = 18,
        /// <summary>
        /// Water flow rate in DHW circuit (litres/minute).
        /// </summary>
        DHWFlowRate = 19,
        /// <summary>
        /// Day of Week and Time of Day.
        /// </summary>
        DayTime = 20,
        /// <summary>
        /// Calendar date.
        /// </summary>
        Date = 21,
        /// <summary>
        /// Calendar year.
        /// </summary>
        Year = 22,
        /// <summary>
        /// Room Setpoint for 2nd CH circuit (°C).
        /// </summary>
        TrSetCH2 = 23,
        /// <summary>
        /// Room temperature (°C).
        /// </summary>
        Tr = 24,
        /// <summary>
        /// Boiler flow water temperature (°C).
        /// </summary>
        Tboiler = 25,
        /// <summary>
        /// DHW temperature (°C).
        /// </summary>
        Tdhw = 26,
        /// <summary>
        /// Outside temperature (°C).
        /// </summary>
        Toutside = 27,
        /// <summary>
        /// Return water temperature (°C).
        /// </summary>
        Tret = 28,
        /// <summary>
        /// Solar storage temperature (°C).
        /// </summary>
        Tstorage = 29,
        /// <summary>
        /// Solar collector temperature (°C).
        /// </summary>
        Tcollector = 30,
        /// <summary>
        /// Flow water temperature CH2 circuit (°C).
        /// </summary>
        TflowCH2 = 31,
        /// <summary>
        /// Domestic hot water temperature 2 (°C).
        /// </summary>
        Tdhw2 = 32,
        /// <summary>
        /// Boiler exhaust temperature (°C).
        /// </summary>
        Texhaust = 33,
        /// <summary>
        /// Boiler heat exchanger temperature (°C).
        /// </summary>
        TboilerHeatExchanger = 34,
        /// <summary>
        /// Boiler fan speed Setpoint and actual value.
        /// </summary>
        BoilerFanSpeedSetpointAndActual = 35,
        /// <summary>
        /// Electrical current through burner flame [μA].
        /// </summary>
        FlameCurrent = 36,
        /// <summary>
        /// Room temperature for 2nd CH circuit (°C).
        /// </summary>
        TrCH2 = 37,
        /// <summary>
        /// Actual relative humidity as a percentage.
        /// </summary>
        RelativeHumidity = 38,
        /// <summary>
        /// Remote Override Room Setpoint 2.
        /// </summary>
        TrOverride2 = 39,
        /// <summary>
        /// DHW Setpoint upper & lower bounds for adjustment(°C)
        /// </summary>
        TdhwSetUBTdhwSetLB = 48,
        /// <summary>
        /// Max CH water Setpoint upper & lower bounds for adjustment(°C)
        /// </summary>
        MaxTSetUBMaxTSetLB = 49,
        /// <summary>
        /// DHW Setpoint(°C) (Remote parameter 1)
        /// </summary>
        TdhwSet = 56,
        /// <summary>
        /// Max CH water Setpoint(°C) (Remote parameters 2)
        /// </summary>
        MaxTSet = 57,
        /// <summary>
        /// Master and Slave Status flags ventilation / heat - recovery
        /// </summary>
        StatusVentilationHeatRecovery = 70,
        /// <summary>
        /// Relative ventilation position (0-100%). 0% is the minimum set ventilation and 100% is the maximum set ventilation. 
        /// </summary>
        Vset = 71,
        /// <summary>
        /// Application-specific fault flags and OEM fault code ventilation / heat-recovery 
        /// </summary>
        ASFflagsOEMfaultCodeVentilationHeatRecovery = 72,
        /// <summary>
        /// An OEM-specific diagnostic/service code for ventilation / heat-recovery system 
        /// </summary>
        OEMDiagnosticCodeVentilationHeatRecovery = 73,
        /// <summary>
        /// Slave Configuration Flags / Slave MemberID Code ventilation / heat-recovery 
        /// </summary>
        SConfigSMemberIDCodeVentilationHeatRecovery = 74,
        /// <summary>
        /// The implemented version of the OpenTherm Protocol Specification in the ventilation / heat-recovery system. 
        /// </summary>
        OpenThermVersionVentilationHeatRecovery = 75,
        /// <summary>
        /// Ventilation / heat-recovery product version number and type 
        /// </summary>
        VentilationHeatRecoveryVersion = 76,
        /// <summary>
        /// Relative ventilation (0-100%) 
        /// </summary>
        RelVentLevel = 77,
        /// <summary>
        /// Relative humidity exhaust air (0-100%) 
        /// </summary>
        RHexhaust = 78,
        /// <summary>
        /// CO2 level exhaust air (0-2000 ppm) 
        /// </summary>
        CO2exhaust = 79,
        /// <summary>
        /// Supply inlet temperature (°C) 
        /// </summary>
        Tsi = 80,
        /// <summary>
        /// Supply outlet temperature (°C) 
        /// </summary>
        Tso = 81,
        /// <summary>
        /// Exhaust inlet temperature (°C) 
        /// </summary>
        Tei = 82,
        /// <summary>
        /// Exhaust outlet temperature (°C) 
        /// </summary>
        Teo = 83,
        /// <summary>
        /// Exhaust fan speed in rpm 
        /// </summary>
        RPMexhaust = 84,
        /// <summary>
        /// Supply fan speed in rpm 
        /// </summary>
        RPMsupply = 85,
        /// <summary>
        /// Remote ventilation / heat-recovery parameter transfer-enable & read/write flags 
        /// </summary>
        RBPflagsVentilationHeatRecovery = 86,
        /// <summary>
        /// Nominal relative value for ventilation (0-100 %) 
        /// </summary>
        NominalVentilationValue = 87,
        /// <summary>
        /// Number of Transparent-Slave-Parameters supported by TSP’s ventilation / heat-recovery 
        /// </summary>
        TSPventilationHeatRecovery = 88,
        /// <summary>
        /// Index number / Value of referred-to transparent TSP’s ventilation / heat-recovery parameter. 
        /// </summary>
        TSPindexTSPvalueVentilationHeatRecovery = 89,
        /// <summary>
        /// Size of Fault-History-Buffer supported by ventilation / heat-recovery 
        /// </summary>
        FHBsizeVentilationHeatRecovery = 90,
        /// <summary>
        /// Index number / Value of referred-to fault-history buffer entry ventilation / heat-recovery 
        /// </summary>
        FHBindexFHBvalueVentilationHeatRecovery = 91,
        /// <summary>
        /// Index number of the character in the text string ASCII character referenced by the above index number 
        /// </summary>
        Brand = 93,
        /// <summary>
        /// Index number of the character in the text string ASCII character referenced by the above index number 
        /// </summary>
        BrandVersion = 94,
        /// <summary>
        /// Index number of the character in the text string ASCII character referenced by the above index number 
        /// </summary>
        BrandSerialNumber = 95,
        /// <summary>
        /// Number of hours that the slave is in Cooling Mode. Reset by zero is optional for slave 
        /// </summary>
        CoolingOperationHours = 96,
        /// <summary>
        /// Number of Power Cycles of a slave (wake-up after Reset), Reset by zero is optional for slave 
        /// </summary>
        PowerCycles = 97,
        /// <summary>
        /// For a specific RF sensor the RF strength and battery level is written 
        /// </summary>
        RFsensorStatusInformation = 98,
        /// <summary>
        /// Operating Mode HC1, HC2/ Operating Mode DHW 
        /// </summary>
        RemoteOverrideOperatingModeHeatingDHW = 99,
        /// <summary>
        /// Function of manual and program changes in master and remote room Setpoint 
        /// </summary>
        RemoteOverrideFunction = 100,
        /// <summary>
        /// Master and Slave Status flags Solar Storage 
        /// </summary>
        StatusSolarStorage = 101,
        /// <summary>
        /// Application-specific fault flags and OEM fault code Solar Storage 
        /// </summary>
        ASFflagsOEMfaultCodeSolarStorage = 102,
        /// <summary>
        /// Slave Configuration Flags / Slave MemberID Code Solar Storage 
        /// </summary>
        SConfigSMemberIDcodeSolarStorage = 103,
        /// <summary>
        /// Solar Storage product version number and type
        /// </summary>
        SolarStorageVersion = 104,
        /// <summary>
        /// Number of Transparent - Slave - Parameters supported by TSP’s Solar Storage
        /// </summary>
        TSPSolarStorage = 105,
        /// <summary>
        /// Index number / Value of referred - to transparent TSP’s Solar Storage parameter.
        /// </summary>
        TSPindexTSPvalueSolarStorage = 106,
        /// <summary>
        /// Size of Fault - History - Buffer supported by Solar Storage
        /// </summary>
        FHBsizeSolarStorage = 107,
        /// <summary>
        /// Index number / Value of referred - to fault - history buffer entry Solar Storage
        /// </summary>
        FHBindexFHBvalueSolarStorage = 108,
        /// <summary>
        /// Number of start of the electricity producer.
        /// </summary>
        ElectricityProducerStarts = 109,
        /// <summary>
        /// Number of hours the electricity produces is in operation
        /// </summary>
        ElectricityProducerHours = 110,
        /// <summary>
        /// Current electricity production in Watt.
        /// </summary>
        ElectricityProduction = 111,
        /// <summary>
        /// Cumulative electricity production in KWh.
        /// </summary>
        CumulativElectricityProduction = 112,
        /// <summary>
        /// Number of un - successful burner starts
        /// </summary>
        UnsuccessfulBurnerStarts = 113,
        /// <summary>
        /// Number of times flame signal was too low
        /// </summary>
        FlameSignalTooLowNumber = 114,
        /// <summary>
        /// OEM - specific diagnostic / service code
        /// </summary>
        OEMDiagnosticCode = 115,
        /// <summary>
        /// Number of succesful starts burner
        /// </summary>
        SuccessfulBurnerStarts = 116,
        /// <summary>
        /// Number of starts CH pump
        /// </summary>
        CHPumpStarts = 117,
        /// <summary>
        /// Number of starts DHW pump / valve
        /// </summary>
        DHWPumpValveStarts = 118,
        /// <summary>
        /// Number of starts burner during DHW mode
        /// </summary>
        DHWBurnerStarts = 119,
        /// <summary>
        /// Number of hours that burner is in operation(i.e.flame on)
        /// </summary>
        BurnerOperationHours = 120,
        /// <summary>
        /// Number of hours that CH pump has been running
        /// </summary>
        CHPumpOperationHours = 121,
        /// <summary>
        /// Number of hours that DHW pump has been running or DHW valve has been opened
        /// </summary>
        DHWPumpValveOperationHours = 122,
        /// <summary>
        /// Number of hours that burner is in operation during DHW mode
        /// </summary>
        DHWBurnerOperationHours = 123,
        /// <summary>
        /// The implemented version of the OpenTherm Protocol Specification in the master.
        /// </summary>
        OpenThermVersionMaster = 124,
        /// <summary>
        /// The implemented version of the OpenTherm Protocol Specification in the slave.
        /// </summary>
        OpenThermVersionSlave = 125,
        /// <summary>
        /// Master product version number and type
        /// </summary>
        MasterVersion = 126,
        /// <summary>
        /// Slave product version number and type
        /// </summary>
        SlaveVersion = 127,
    }
}
