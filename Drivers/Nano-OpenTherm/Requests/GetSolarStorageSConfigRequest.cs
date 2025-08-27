using TekuSP.Drivers.DriverBase.Enums.OpenTherm;
using TekuSP.Drivers.Nano_OpenTherm.Enums;
using TekuSP.Drivers.Nano_OpenTherm.Interfaces;

namespace TekuSP.Drivers.Nano_OpenTherm.Requests
{
    /// <summary>
    /// Reads the Solar Storage slave configuration and member ID code.
    /// </summary>
    /// <remarks>
    /// The low byte contains <see cref="SlaveConfiguration"/> flags and the high byte contains
    /// the <see cref="MemberIdCode"/> identifying the manufacturer/OEM. Convenience boolean
    /// properties are provided to access individual configuration bits.
    /// </remarks>
    public class GetSolarStorageSConfigRequest : ReadRequest, ISlaveConfiguration, IMemberIdCode
    {
        #region Public Constructors

        public GetSolarStorageSConfigRequest() : base()
        {
        }

        public GetSolarStorageSConfigRequest(Request baseReq) : base(baseReq)
        {
        }

        #endregion Public Constructors

        #region Public Properties

        // IMemberIdCode
        public bool IsAET80FormerNordgasSrl { get => MemberIdCode == MemberIdCode.AET80FormerNordgasSrl; set => MemberIdCode = value ? MemberIdCode.AET80FormerNordgasSrl : MemberIdCode; }

        public bool IsAirfitShanghaiHeatingEquipmentCoLtd { get => MemberIdCode == MemberIdCode.AirfitShanghaiHeatingEquipmentCoLtd; set => MemberIdCode = value ? MemberIdCode.AirfitShanghaiHeatingEquipmentCoLtd : MemberIdCode; }
        public bool IsAirios { get => MemberIdCode == MemberIdCode.Airios; set => MemberIdCode = value ? MemberIdCode.Airios : MemberIdCode; }
        public bool IsATAGVerwarmingNederlandBV { get => MemberIdCode == MemberIdCode.ATAGVerwarmingNederlandBV; set => MemberIdCode = value ? MemberIdCode.ATAGVerwarmingNederlandBV : MemberIdCode; }
        public bool IsAtlantic { get => MemberIdCode == MemberIdCode.Atlantic; set => MemberIdCode = value ? MemberIdCode.Atlantic : MemberIdCode; }
        public bool IsAvidsen { get => MemberIdCode == MemberIdCode.Avidsen; set => MemberIdCode = value ? MemberIdCode.Avidsen : MemberIdCode; }
        public bool IsAxenco { get => MemberIdCode == MemberIdCode.Axenco; set => MemberIdCode = value ? MemberIdCode.Axenco : MemberIdCode; }
        public bool IsBDRThermeaGroup { get => MemberIdCode == MemberIdCode.BDRThermeaGroup; set => MemberIdCode = value ? MemberIdCode.BDRThermeaGroup : MemberIdCode; }
        public bool IsBertelliAndPartnersSrl { get => MemberIdCode == MemberIdCode.BertelliAndPartnersSrl; set => MemberIdCode = value ? MemberIdCode.BertelliAndPartnersSrl : MemberIdCode; }
        public bool IsBoschTermotecnologiaSA { get => MemberIdCode == MemberIdCode.BoschTermotecnologiaSA; set => MemberIdCode = value ? MemberIdCode.BoschTermotecnologiaSA : MemberIdCode; }
        public bool IsBoschThermotechniekBV { get => MemberIdCode == MemberIdCode.BoschThermotechniekBV; set => MemberIdCode = value ? MemberIdCode.BoschThermotechniekBV : MemberIdCode; }
        public bool IsBRAHMASpA { get => MemberIdCode == MemberIdCode.BRAHMASpA; set => MemberIdCode = value ? MemberIdCode.BRAHMASpA : MemberIdCode; }
        public bool IsCaleffiSpA { get => MemberIdCode == MemberIdCode.CaleffiSpA; set => MemberIdCode = value ? MemberIdCode.CaleffiSpA : MemberIdCode; }
        public bool IsCentricaHiveLtd { get => MemberIdCode == MemberIdCode.CentricaHiveLtd; set => MemberIdCode = value ? MemberIdCode.CentricaHiveLtd : MemberIdCode; }
        public bool IsCercaTrova { get => MemberIdCode == MemberIdCode.CercaTrova; set => MemberIdCode = value ? MemberIdCode.CercaTrova : MemberIdCode; }
        public bool IsCloudGardenSentronicsBV { get => MemberIdCode == MemberIdCode.CloudGardenSentronicsBV; set => MemberIdCode = value ? MemberIdCode.CloudGardenSentronicsBV : MemberIdCode; }
        public bool IsCoollSustainableEnergySolutions { get => MemberIdCode == MemberIdCode.CoollSustainableEnergySolutions; set => MemberIdCode = value ? MemberIdCode.CoollSustainableEnergySolutions : MemberIdCode; }
        public bool IsCorporacionEmpresarialAltraSL { get => MemberIdCode == MemberIdCode.CorporacionEmpresarialAltraSL; set => MemberIdCode = value ? MemberIdCode.CorporacionEmpresarialAltraSL : MemberIdCode; }
        public bool IsDanfossLtd { get => MemberIdCode == MemberIdCode.DanfossLtd; set => MemberIdCode = value ? MemberIdCode.DanfossLtd : MemberIdCode; }
        public bool IsDELTADORE { get => MemberIdCode == MemberIdCode.DELTADORE; set => MemberIdCode = value ? MemberIdCode.DELTADORE : MemberIdCode; }
        public bool IsEbmPapstLandshutGmbH { get => MemberIdCode == MemberIdCode.EbmPapstLandshutGmbH; set => MemberIdCode = value ? MemberIdCode.EbmPapstLandshutGmbH : MemberIdCode; }
        public bool IsEbVElektronikbauUndVertriebsGmbH { get => MemberIdCode == MemberIdCode.EbVElektronikbauUndVertriebsGmbH; set => MemberIdCode = value ? MemberIdCode.EbVElektronikbauUndVertriebsGmbH : MemberIdCode; }
        public bool IsEnecoBV { get => MemberIdCode == MemberIdCode.EnecoBV; set => MemberIdCode = value ? MemberIdCode.EnecoBV : MemberIdCode; }
        public bool IsEnelX { get => MemberIdCode == MemberIdCode.EnelX; set => MemberIdCode = value ? MemberIdCode.EnelX : MemberIdCode; }
        public bool IsEOGBEnergyProductsLtd { get => MemberIdCode == MemberIdCode.EOGBEnergyProductsLtd; set => MemberIdCode = value ? MemberIdCode.EOGBEnergyProductsLtd : MemberIdCode; }
        public bool IsEPHControlsLtd { get => MemberIdCode == MemberIdCode.EPHControlsLtd; set => MemberIdCode = value ? MemberIdCode.EPHControlsLtd : MemberIdCode; }
        public bool IsEsiControlsLtd { get => MemberIdCode == MemberIdCode.EsiControlsLtd; set => MemberIdCode = value ? MemberIdCode.EsiControlsLtd : MemberIdCode; }
        public bool IsFantiniCosmiSpA { get => MemberIdCode == MemberIdCode.FantiniCosmiSpA; set => MemberIdCode = value ? MemberIdCode.FantiniCosmiSpA : MemberIdCode; }
        public bool IsFerroliSpA { get => MemberIdCode == MemberIdCode.FerroliSpA; set => MemberIdCode = value ? MemberIdCode.FerroliSpA : MemberIdCode; }
        public bool IsFlamcoIMZBV { get => MemberIdCode == MemberIdCode.FlamcoIMZBV; set => MemberIdCode = value ? MemberIdCode.FlamcoIMZBV : MemberIdCode; }
        public bool IsFortesEnergySystems { get => MemberIdCode == MemberIdCode.FortesEnergySystems; set => MemberIdCode = value ? MemberIdCode.FortesEnergySystems : MemberIdCode; }
        public bool IsGeneralLifeIsipark { get => MemberIdCode == MemberIdCode.GeneralLifeIsipark; set => MemberIdCode = value ? MemberIdCode.GeneralLifeIsipark : MemberIdCode; }
        public bool IsGiordanoControlsSpA { get => MemberIdCode == MemberIdCode.GiordanoControlsSpA; set => MemberIdCode = value ? MemberIdCode.GiordanoControlsSpA : MemberIdCode; }
        public bool IsGuangdongBaiweiElectronicCoLtd { get => MemberIdCode == MemberIdCode.GuangdongBaiweiElectronicCoLtd; set => MemberIdCode = value ? MemberIdCode.GuangdongBaiweiElectronicCoLtd : MemberIdCode; }
        public bool IsGuangdongHMJDElectricalApplianceCoLtd { get => MemberIdCode == MemberIdCode.GuangdongHMJDElectricalApplianceCoLtd; set => MemberIdCode = value ? MemberIdCode.GuangdongHMJDElectricalApplianceCoLtd : MemberIdCode; }
        public bool IsGuangDongJUSCEElectronicsCoLtd { get => MemberIdCode == MemberIdCode.GuangDongJUSCEElectronicsCoLtd; set => MemberIdCode = value ? MemberIdCode.GuangDongJUSCEElectronicsCoLtd : MemberIdCode; }
        public bool IsGuangdongMezicTechCoLtd { get => MemberIdCode == MemberIdCode.GuangdongMezicTechCoLtd; set => MemberIdCode = value ? MemberIdCode.GuangdongMezicTechCoLtd : MemberIdCode; }
        public bool IsGuangdongZhidaHangyiElectricalLtd { get => MemberIdCode == MemberIdCode.GuangdongZhidaHangyiElectricalLtd; set => MemberIdCode = value ? MemberIdCode.GuangdongZhidaHangyiElectricalLtd : MemberIdCode; }
        public bool IsHangzhouEzvizNetworkCoLtd { get => MemberIdCode == MemberIdCode.HangzhouEzvizNetworkCoLtd; set => MemberIdCode = value ? MemberIdCode.HangzhouEzvizNetworkCoLtd : MemberIdCode; }
        public bool IsHeatmiserUKLtd { get => MemberIdCode == MemberIdCode.HeatmiserUKLtd; set => MemberIdCode = value ? MemberIdCode.HeatmiserUKLtd : MemberIdCode; }
        public bool IsHeatronXBV { get => MemberIdCode == MemberIdCode.HeatronXBV; set => MemberIdCode = value ? MemberIdCode.HeatronXBV : MemberIdCode; }
        public bool IsHoneywellBV { get => MemberIdCode == MemberIdCode.HoneywellBV; set => MemberIdCode = value ? MemberIdCode.HoneywellBV : MemberIdCode; }
        public bool IsHubeiTelinEnergySavingEquipmentCoLtd { get => MemberIdCode == MemberIdCode.HubeiTelinEnergySavingEquipmentCoLtd; set => MemberIdCode = value ? MemberIdCode.HubeiTelinEnergySavingEquipmentCoLtd : MemberIdCode; }
        public bool IsHZCElectric { get => MemberIdCode == MemberIdCode.HZCElectric; set => MemberIdCode = value ? MemberIdCode.HZCElectric : MemberIdCode; }
        public bool IsChunhuiControls { get => MemberIdCode == MemberIdCode.ChunhuiControls; set => MemberIdCode = value ? MemberIdCode.ChunhuiControls : MemberIdCode; }
        public bool IsICYBV { get => MemberIdCode == MemberIdCode.ICYBV; set => MemberIdCode = value ? MemberIdCode.ICYBV : MemberIdCode; }
        public bool IsIMITControlSystem { get => MemberIdCode == MemberIdCode.IMITControlSystem; set => MemberIdCode = value ? MemberIdCode.IMITControlSystem : MemberIdCode; }
        public bool IsInArTechnologySrl { get => MemberIdCode == MemberIdCode.InArTechnologySrl; set => MemberIdCode = value ? MemberIdCode.InArTechnologySrl : MemberIdCode; }
        public bool IsIntergasVerwarmingBV { get => MemberIdCode == MemberIdCode.IntergasVerwarmingBV; set => MemberIdCode = value ? MemberIdCode.IntergasVerwarmingBV : MemberIdCode; }
        public bool IsInventumTechnologiesBV { get => MemberIdCode == MemberIdCode.InventumTechnologiesBV; set => MemberIdCode = value ? MemberIdCode.InventumTechnologiesBV : MemberIdCode; }
        public bool IsIRSAPSpA { get => MemberIdCode == MemberIdCode.IRSAPSpA; set => MemberIdCode = value ? MemberIdCode.IRSAPSpA : MemberIdCode; }
        public bool IsIthoDaalderopOperationsBV { get => MemberIdCode == MemberIdCode.IthoDaalderopOperationsBV; set => MemberIdCode = value ? MemberIdCode.IthoDaalderopOperationsBV : MemberIdCode; }
        public bool IsJouleTechnologiesBV { get => MemberIdCode == MemberIdCode.JouleTechnologiesBV; set => MemberIdCode = value ? MemberIdCode.JouleTechnologiesBV : MemberIdCode; }
        public bool IsKBlueSrl { get => MemberIdCode == MemberIdCode.KBlueSrl; set => MemberIdCode = value ? MemberIdCode.KBlueSrl : MemberIdCode; }
        public bool IsKDNavienCoLtd { get => MemberIdCode == MemberIdCode.KDNavienCoLtd; set => MemberIdCode = value ? MemberIdCode.KDNavienCoLtd : MemberIdCode; }
        public bool IsKOBERLtdVaduriBranch { get => MemberIdCode == MemberIdCode.KOBERLtdVaduriBranch; set => MemberIdCode = value ? MemberIdCode.KOBERLtdVaduriBranch : MemberIdCode; }
        public bool IsKooleControlsBV { get => MemberIdCode == MemberIdCode.KooleControlsBV; set => MemberIdCode = value ? MemberIdCode.KooleControlsBV : MemberIdCode; }
        public bool IsKZMeetEnRegelapparatuur { get => MemberIdCode == MemberIdCode.KZMeetEnRegelapparatuur; set => MemberIdCode = value ? MemberIdCode.KZMeetEnRegelapparatuur : MemberIdCode; }
        public bool IsMideaEuropeGmbH { get => MemberIdCode == MemberIdCode.MideaEuropeGmbH; set => MemberIdCode = value ? MemberIdCode.MideaEuropeGmbH : MemberIdCode; }
        public bool IsNestLabsEuropeLimited { get => MemberIdCode == MemberIdCode.NestLabsEuropeLimited; set => MemberIdCode = value ? MemberIdCode.NestLabsEuropeLimited : MemberIdCode; }
        public bool IsNetatmo { get => MemberIdCode == MemberIdCode.Netatmo; set => MemberIdCode = value ? MemberIdCode.Netatmo : MemberIdCode; }
        public bool IsNIBEAB { get => MemberIdCode == MemberIdCode.NIBEAB; set => MemberIdCode = value ? MemberIdCode.NIBEAB : MemberIdCode; }
        public bool IsOpenThermAssociation { get => MemberIdCode == MemberIdCode.OpenThermAssociation; set => MemberIdCode = value ? MemberIdCode.OpenThermAssociation : MemberIdCode; }
        public bool IsPactrolControlsLtd { get => MemberIdCode == MemberIdCode.PactrolControlsLtd; set => MemberIdCode = value ? MemberIdCode.PactrolControlsLtd : MemberIdCode; }
        public bool IsPlugwiseBV { get => MemberIdCode == MemberIdCode.PlugwiseBV; set => MemberIdCode = value ? MemberIdCode.PlugwiseBV : MemberIdCode; }
        public bool IsPlumSpZoo { get => MemberIdCode == MemberIdCode.PlumSpZoo; set => MemberIdCode = value ? MemberIdCode.PlumSpZoo : MemberIdCode; }
        public bool IsPSVProjectServiceAndValueSrl { get => MemberIdCode == MemberIdCode.PSVProjectServiceAndValueSrl; set => MemberIdCode = value ? MemberIdCode.PSVProjectServiceAndValueSrl : MemberIdCode; }
        public bool IsQingdaoHaierWaterHeaterCoLtd { get => MemberIdCode == MemberIdCode.QingdaoHaierWaterHeaterCoLtd; set => MemberIdCode = value ? MemberIdCode.QingdaoHaierWaterHeaterCoLtd : MemberIdCode; }
        public bool IsQualityHeatingBV { get => MemberIdCode == MemberIdCode.QualityHeatingBV; set => MemberIdCode = value ? MemberIdCode.QualityHeatingBV : MemberIdCode; }
        public bool IsRESEnerjiSistemleriAS { get => MemberIdCode == MemberIdCode.RESEnerjiSistemleriAS; set => MemberIdCode = value ? MemberIdCode.RESEnerjiSistemleriAS : MemberIdCode; }
        public bool IsResideo { get => MemberIdCode == MemberIdCode.Resideo; set => MemberIdCode = value ? MemberIdCode.Resideo : MemberIdCode; }
        public bool IsResideoUKHoneywellControlSystemsLtd { get => MemberIdCode == MemberIdCode.ResideoUKHoneywellControlSystemsLtd; set => MemberIdCode = value ? MemberIdCode.ResideoUKHoneywellControlSystemsLtd : MemberIdCode; }
        public bool IsRinnaiCorporation { get => MemberIdCode == MemberIdCode.RinnaiCorporation; set => MemberIdCode = value ? MemberIdCode.RinnaiCorporation : MemberIdCode; }
        public bool IsSALUSControlsPlc { get => MemberIdCode == MemberIdCode.SALUSControlsPlc; set => MemberIdCode = value ? MemberIdCode.SALUSControlsPlc : MemberIdCode; }
        public bool IsSecureMeters { get => MemberIdCode == MemberIdCode.SecureMeters; set => MemberIdCode = value ? MemberIdCode.SecureMeters : MemberIdCode; }
        public bool IsSEEKCOEnvironmentalTechnologyCoLtd { get => MemberIdCode == MemberIdCode.SEEKCOEnvironmentalTechnologyCoLtd; set => MemberIdCode = value ? MemberIdCode.SEEKCOEnvironmentalTechnologyCoLtd : MemberIdCode; }
        public bool IsShenzhenMyuetEnergySavingEquipmentCoLtd { get => MemberIdCode == MemberIdCode.ShenzhenMyuetEnergySavingEquipmentCoLtd; set => MemberIdCode = value ? MemberIdCode.ShenzhenMyuetEnergySavingEquipmentCoLtd : MemberIdCode; }
        public bool IsShenZhenSaswellTechnologyInc { get => MemberIdCode == MemberIdCode.ShenZhenSaswellTechnologyInc; set => MemberIdCode = value ? MemberIdCode.ShenZhenSaswellTechnologyInc : MemberIdCode; }
        public bool IsShenzhenTopbandCoLtd { get => MemberIdCode == MemberIdCode.ShenzhenTopbandCoLtd; set => MemberIdCode = value ? MemberIdCode.ShenzhenTopbandCoLtd : MemberIdCode; }
        public bool IsShenzhenXingHuoYuanIntelligenceTechnologyCoLtd { get => MemberIdCode == MemberIdCode.ShenzhenXingHuoYuanIntelligenceTechnologyCoLtd; set => MemberIdCode = value ? MemberIdCode.ShenzhenXingHuoYuanIntelligenceTechnologyCoLtd : MemberIdCode; }
        public bool IsSchneiderElectricControlsUKLtd { get => MemberIdCode == MemberIdCode.SchneiderElectricControlsUKLtd; set => MemberIdCode = value ? MemberIdCode.SchneiderElectricControlsUKLtd : MemberIdCode; }
        public bool IsSiemensAG { get => MemberIdCode == MemberIdCode.SiemensAG; set => MemberIdCode = value ? MemberIdCode.SiemensAG : MemberIdCode; }
        public bool IsSITControlsBV { get => MemberIdCode == MemberIdCode.SITControlsBV; set => MemberIdCode = value ? MemberIdCode.SITControlsBV : MemberIdCode; }
        public bool IsSwitch2EnergyLimited { get => MemberIdCode == MemberIdCode.Switch2EnergyLimited; set => MemberIdCode = value ? MemberIdCode.Switch2EnergyLimited : MemberIdCode; }
        public bool IsSwitcheeLtd { get => MemberIdCode == MemberIdCode.SwitcheeLtd; set => MemberIdCode = value ? MemberIdCode.SwitcheeLtd : MemberIdCode; }
        public bool IsTadoGmbH { get => MemberIdCode == MemberIdCode.TadoGmbH; set => MemberIdCode = value ? MemberIdCode.TadoGmbH : MemberIdCode; }
        public bool IsTECHSTEROWNIKI { get => MemberIdCode == MemberIdCode.TECHSTEROWNIKI; set => MemberIdCode = value ? MemberIdCode.TECHSTEROWNIKI : MemberIdCode; }
        public bool IsTEMAG { get => MemberIdCode == MemberIdCode.TEMAG; set => MemberIdCode = value ? MemberIdCode.TEMAG : MemberIdCode; }
        public bool IsThebenAG { get => MemberIdCode == MemberIdCode.ThebenAG; set => MemberIdCode = value ? MemberIdCode.ThebenAG : MemberIdCode; }
        public bool IsTheDynamicWay { get => MemberIdCode == MemberIdCode.TheDynamicWay; set => MemberIdCode = value ? MemberIdCode.TheDynamicWay : MemberIdCode; }
        public bool IsTherconNV { get => MemberIdCode == MemberIdCode.TherconNV; set => MemberIdCode = value ? MemberIdCode.TherconNV : MemberIdCode; }
        public bool IsTiemmeRaccorderieSpA { get => MemberIdCode == MemberIdCode.TiemmeRaccorderieSpA; set => MemberIdCode = value ? MemberIdCode.TiemmeRaccorderieSpA : MemberIdCode; }
        public bool IsTripleSolar { get => MemberIdCode == MemberIdCode.TripleSolar; set => MemberIdCode = value ? MemberIdCode.TripleSolar : MemberIdCode; }
        public bool IsUnicalAGSpa { get => MemberIdCode == MemberIdCode.UnicalAGSpa; set => MemberIdCode = value ? MemberIdCode.UnicalAGSpa : MemberIdCode; }
        public bool IsUniversalElectronicsInc { get => MemberIdCode == MemberIdCode.UniversalElectronicsInc; set => MemberIdCode = value ? MemberIdCode.UniversalElectronicsInc : MemberIdCode; }
        public bool IsUnknown { get => MemberIdCode == MemberIdCode.Unknown; set => MemberIdCode = value ? MemberIdCode.Unknown : MemberIdCode; }
        public bool IsVaillantGroupNetherlandsBV { get => MemberIdCode == MemberIdCode.VaillantGroupNetherlandsBV; set => MemberIdCode = value ? MemberIdCode.VaillantGroupNetherlandsBV : MemberIdCode; }
        public bool IsViessmannElektronikGmbH { get => MemberIdCode == MemberIdCode.ViessmannElektronikGmbH; set => MemberIdCode = value ? MemberIdCode.ViessmannElektronikGmbH : MemberIdCode; }
        public bool IsWattsEMEA { get => MemberIdCode == MemberIdCode.WattsEMEA; set => MemberIdCode = value ? MemberIdCode.WattsEMEA : MemberIdCode; }
        public bool IsWeHeat { get => MemberIdCode == MemberIdCode.WeHeat; set => MemberIdCode = value ? MemberIdCode.WeHeat : MemberIdCode; }
        public bool IsWolfGmbH { get => MemberIdCode == MemberIdCode.WolfGmbH; set => MemberIdCode = value ? MemberIdCode.WolfGmbH : MemberIdCode; }
        public bool IsWundaGroupPLC { get => MemberIdCode == MemberIdCode.WundaGroupPLC; set => MemberIdCode = value ? MemberIdCode.WundaGroupPLC : MemberIdCode; }
        public bool IsXiamenDavellAutoControlEquipmentCoLtd { get => MemberIdCode == MemberIdCode.XiamenDavellAutoControlEquipmentCoLtd; set => MemberIdCode = value ? MemberIdCode.XiamenDavellAutoControlEquipmentCoLtd : MemberIdCode; }
        public bool IsXiamenHysenControlTechnology { get => MemberIdCode == MemberIdCode.XiamenHysenControlTechnology; set => MemberIdCode = value ? MemberIdCode.XiamenHysenControlTechnology : MemberIdCode; }
        public override MessageID MessageID => MessageID.SConfigSMemberIDcodeSolarStorage;
        public override MessageType MessageType => MessageType.READ_DATA;
        public bool SlaveConfigControlType { get => SlaveConfiguration.IsSet(SlaveConfiguration.ControlType); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SlaveConfiguration.ControlType, value); }
        public bool SlaveConfigCooling { get => SlaveConfiguration.IsSet(SlaveConfiguration.CoolingConfig); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SlaveConfiguration.CoolingConfig, value); }
        public bool SlaveConfigDHWConfig { get => SlaveConfiguration.IsSet(SlaveConfiguration.DHWConfig); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SlaveConfiguration.DHWConfig, value); }

        // ISlaveConfiguration
        public bool SlaveConfigDHWPresent { get => SlaveConfiguration.IsSet(SlaveConfiguration.DHWPresent); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SlaveConfiguration.DHWPresent, value); }

        public bool SlaveConfigCH2Present { get => SlaveConfiguration.IsSet(SlaveConfiguration.CH2Present); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SlaveConfiguration.CH2Present, value); }
        public bool SlaveConfigMasterLowOffPumpControl { get => SlaveConfiguration.IsSet(SlaveConfiguration.MasterLowOffPumpControl); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SlaveConfiguration.MasterLowOffPumpControl, value); }
        public bool SlaveConfigReserved6 { get => SlaveConfiguration.IsSet(SlaveConfiguration.Reserved6); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SlaveConfiguration.Reserved6, value); }
        public bool SlaveConfigReserved7 { get => SlaveConfiguration.IsSet(SlaveConfiguration.Reserved7); set => SlaveConfiguration = SlaveConfiguration.SetFlag(SlaveConfiguration.Reserved7, value); }

        #endregion Public Properties

        #region Protected Properties

        protected MemberIdCode MemberIdCode { get; set; }
        protected SlaveConfiguration SlaveConfiguration { get; set; }

        #endregion Protected Properties

        #region Protected Methods

        protected override uint GetRawDataCore()
        {
            byte low = Utilities.SetSlaveConfiguration(SlaveConfiguration);
            ushort payload = Utilities.MakeUShort((byte)MemberIdCode, low);
            return ProcessRequest(payload);
        }

        protected override void SetRawDataCore(uint value)
        {
            SlaveConfiguration = Utilities.GetSlaveConfiguration(value);
            MemberIdCode = (MemberIdCode)Utilities.GetHighByte(value);
        }

        #endregion Protected Methods
    }
}