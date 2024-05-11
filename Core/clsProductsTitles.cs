using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsProductsTitles
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int _iRecord_ID;
        private int _iShare_ID;
        private string _sProductType;
        private string _sProductTitle;
        private string _sStandardTitle;
        private string _sProviderName;
        private string _sBrandProviderName;
        private string _sISIN;
        private int _iBondType;
        private int _iLegalStructure_ID;
        private int _iProductCategory;
        private int _iHFCategory;
        private int _iMiFIDInstrumentType;
        private int _iAIFMD;
        private string _sMinimumInvestment;
        private int _iGlobalBroad;
        private int _iCountry_ID;
        private int _iCountryGroup_ID;
        private int _iSector_ID;
        private int _iCategoryMorningStar;
        private int _iCountryRisk_ID;
        private int _iBenchmark;
        private string _sRiskCurr;
        private string _sDescriptionEn;
        private string _sDescriptionGr;
        private string _sDateIncorporation;
        private decimal _decMarketCapitalization;
        private string _sMarketCapitalizationCurr;
        private string _sMemberIndex;
        private string _sOfferingTypeDescription;
        private int _iInflationProtected;
        private decimal _decTotalAUM;
        private string _sTotalAUMDate;
        private decimal _decAmountOutstanding;
        private string _sAmountOutstandingDate;
        private int _iLeverage;
        private string _sURL;
        private string _sIR_URL;
        private int _iInvestmentType;
        private int _iExchangeTradedNotes;
        private int _iCommodityTracking;
        private float _sgMaturity;
        private string _sMaturityDate;
        private string _sFundID;
        private string _sInceptionDate;
        private string _sInstitutional;
        private int _iActivelyManaged;
        private string _sReplicationMethod;
        private string _sSwapBasedETF;
        private int _iIsProspectusAvailable;
        private int _iRatingGroup;
        private string _sCreditRating;
        private DateTime _dCreditRatingDate;
        private string _sMoodysRating;
        private DateTime _dMoodysRatingDate;
        private string _sFitchsRating;
        private DateTime _dFitchsRatingDate;
        private string _sSPRating;
        private DateTime _dSPRatingDate;
        private string _sICAPRating;
        private DateTime _dICAPRatingDate;
        private string _sCallDate;
        private int _iRank;
        private string _sDenominationType;
        private int _iIsConvertible;
        private int _iIsDualCurrency;
        private int _iIsHybrid;
        private int _iIsGuaranteed;
        private int _iIsPerpetualSecurity;
        private int _iIsTotalLoss;
        private string _sMinimumTotalLoss;
        private int _iIsCallable;
        private int _iIsPutable;
        private string _sEstimatedKIID;
        private string _sEstimatedKIID_Date;
        private float _sgSurveyedKIID;
        private string _sSurveyedKIID_Date;
        private string _sSurveyedKIID_History;
        private float _sgOngoingKIID;
        private string _sOngoingKIID_Date;
        private string _sRatingOverall;
        private string _sRatingDate;
        private string _sSRRIValues;
        private string _sSRRIValues_Date;
        private string _sManagmentFee;
        private string _sManagmentFee_Date;
        private string _sPerformanceFee;
        private string _sPerformanceFee_Date;
        private string _sCountryRegistered;
        private string _sCountryAvailable;
        private int _iComplexProduct;
        private string _sComplexAttribute;
        private string _sBBG_ComplexProduct;
        private string _sBBG_ComplexAttribute;
        private int _iGreeceRegistered;
        private int _iGreeceAvailable;
        private int _iInvestType_Retail;
        private int _iInvestType_Prof;
        private int _iInvestType_Eligible;
        private int _iExpertise_Basic;
        private int _iExpertise_Informed;
        private int _iExpertise_Advanced;
        private string _sRecHoldingPeriod;
        private int _iRetProfile_Preserv;
        private int _iRetProfile_Income;
        private int _iRetProfile_Growth;
        private int _iDistrib_ExecOnly;
        private int _iDistrib_Advice;
        private int _iDistrib_PortfolioManagment;
        private int _iCapitalLoss_None;
        private int _iCapitalLoss_Limited;
        private int _iCapitalLoss_NoGuarantee;
        private int _iCapitalLoss_BeyondInitial;
        private int _iCapitalLoss_Level;
        private DateTime _dLastEditDate;
        private int _iLastEditUser_ID;
        private int _iNotTradeable;

        private string _sProductType_Title;
        private string _sProductCategory_Title;
        private string _sHFCategory_Title;
        private string _sCountryGroup_Title;
        private string _sCountryRisk_Title;
        private string _sIndustryTitle;
        private string _sSectorTitle;
        private string _sLastEditUserName;

        private DataTable _dtList;
        private DataTable _dtComplexReasons;
        public clsProductsTitles()
        {
            this._iRecord_ID = 0;
            this._iShare_ID = 0;
            this._sProductType = "";
            this._sProductTitle = "";
            this._sStandardTitle = "";
            this._sProviderName = "";
            this._sBrandProviderName = "";
            this._sISIN = "";
            this._iBondType = 1;                       // 1 - Corp , 2 -Govt 
            this._iLegalStructure_ID = 0;
            this._iProductCategory = 0;
            this._iHFCategory = 0;
            this._iMiFIDInstrumentType = 0;
            this._iAIFMD = 0;
            this._sMinimumInvestment = "";
            this._iGlobalBroad = 0;
            this._iCountry_ID = 0;
            this._iCountryGroup_ID = 0;
            this._iSector_ID = 0;
            this._sIndustryTitle = "";
            this._sSectorTitle = "";
            this._iCategoryMorningStar = 0;
            this._iCountryRisk_ID = 0;
            this._iBenchmark = 0;
            this._sRiskCurr = "";
            this._sDescriptionEn = "";
            this._sDescriptionGr = "";
            this._sDateIncorporation = "";
            this._decMarketCapitalization = 0;
            this._sMarketCapitalizationCurr = "";
            this._sMemberIndex = "";
            this._sOfferingTypeDescription = "";
            this._iInflationProtected = 0;
            this._decTotalAUM = 0;
            this._sTotalAUMDate = "";
            this._decAmountOutstanding = 0;
            this._sAmountOutstandingDate = "";
            this._iLeverage = 0;
            this._sURL = "";
            this._sIR_URL = "";
            this._iInvestmentType = 0;
            this._iExchangeTradedNotes = 0;
            this._iCommodityTracking = 0;
            this._sgMaturity = 0;
            this._sMaturityDate = "";
            this._sFundID = "";
            this._sInceptionDate = "";
            this._sInstitutional = "";
            this._iActivelyManaged = 0;
            this._sReplicationMethod = "";
            this._sSwapBasedETF = "";
            this._iIsProspectusAvailable = 0;
            this._iRatingGroup = 0;
            this._sCreditRating = "";
            this._dCreditRatingDate = Convert.ToDateTime("1900/01/01");
            this._sMoodysRating = "";
            this._dMoodysRatingDate = Convert.ToDateTime("1900/01/01");
            this._sFitchsRating = "";
            this._dFitchsRatingDate = Convert.ToDateTime("1900/01/01");
            this._sSPRating = "";
            this._dSPRatingDate = Convert.ToDateTime("1900/01/01");
            this._sICAPRating = "";
            this._dICAPRatingDate = Convert.ToDateTime("1900/01/01");
            this._sCallDate = "";
            this._iRank = 0;
            this._sDenominationType = "";
            this._iIsConvertible = 0;
            this._iIsDualCurrency = 0;
            this._iIsHybrid = 0;
            this._iIsGuaranteed = 0;
            this._iIsPerpetualSecurity = 0;
            this._iIsTotalLoss = 0;
            this._sMinimumTotalLoss = "";
            this._iIsCallable = 0;
            this._iIsPutable = 0;
            this._sEstimatedKIID = "";
            this._sEstimatedKIID_Date = "";
            this._sgSurveyedKIID = 0;
            this._sSurveyedKIID_Date = "";
            this._sSurveyedKIID_History = "";
            this._sgOngoingKIID = 0;
            this._sOngoingKIID_Date = "";
            this._sRatingOverall = "";
            this._sRatingDate = "";
            this._sSRRIValues = "";
            this._sSRRIValues_Date = "";
            this._sManagmentFee = "";
            this._sManagmentFee_Date = "";
            this._sPerformanceFee = "";
            this._sPerformanceFee_Date = "";
            this._sCountryRegistered = "";
            this._sCountryAvailable = "";
            this._iGreeceRegistered = 0;
            this._iGreeceAvailable = 0;
            this._iComplexProduct = 0;
            this._sComplexAttribute = "";
            this._sBBG_ComplexProduct = "";
            this._sBBG_ComplexAttribute = "";
            this._iInvestType_Retail = 0;
            this._iInvestType_Prof = 0;
            this._iInvestType_Eligible = 0;
            this._iExpertise_Basic = 0;
            this._iExpertise_Informed = 0;
            this._iExpertise_Advanced = 0;
            this._sRecHoldingPeriod = "";
            this._iRetProfile_Preserv = 0;
            this._iRetProfile_Income = 0;
            this._iRetProfile_Growth = 0;
            this._iDistrib_ExecOnly = 0;
            this._iDistrib_Advice = 0;
            this._iDistrib_PortfolioManagment = 0;
            this._iCapitalLoss_None = 0;
            this._iCapitalLoss_Limited = 0;
            this._iCapitalLoss_NoGuarantee = 0;
            this._iCapitalLoss_BeyondInitial = 0;
            this._iCapitalLoss_Level = 0;
            this._dLastEditDate = Convert.ToDateTime("1900/01/01");
            this._iLastEditUser_ID = 0;
            this._sLastEditUserName = "";
            this._iNotTradeable = 0;

            this._sProductType_Title = "";
            this._sProductCategory_Title = "";
            this._sHFCategory_Title = "";
            this._sCountryGroup_Title = "";
            this._sCountryRisk_Title = "";
        }
        public void GetRecord()
        {
            int iL1 = 0, iL2 = 0;
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetShareTitle", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iShare_ID = Convert.ToInt32(drList["Share_ID"]);
                    this._sProductType = drList["ProductsTitle"] + "";
                    this._sProductTitle = drList["Title"] + "";
                    this._sStandardTitle = drList["StandardTitle"] + "";
                    this._sProviderName = drList["ProviderName"] + "";
                    this._sBrandProviderName = drList["BrandProviderName"] + "";
                    this._sISIN = drList["ISIN"] + "";
                    this._iBondType = (Convert.ToInt32(drList["BondType"]) != 0 ? Convert.ToInt32(drList["BondType"]) :  0);
                    this._iLegalStructure_ID = Convert.ToInt32(drList["LegalStructure_ID"]);
                    this._iProductCategory = Convert.ToInt32(drList["ProductType"]);
                    this._sProductCategory_Title = drList["ProductCategory_Title"] + "";
                    this._iHFCategory = Convert.ToInt32(drList["HFCategory"]);
                    this._sHFCategory_Title = drList["HFCategory_Title"] + "";
                    this._iMiFIDInstrumentType = Convert.ToInt32(drList["MiFIDInstrumentType"]);
                    this._iAIFMD = Convert.ToInt32(drList["AIFMD"]);
                    this._sMinimumInvestment = drList["MinimumInvestment"] + "";
                    this._iGlobalBroad = Convert.ToInt32(drList["GlobalBroad"]);
                    this._iCountry_ID = Convert.ToInt32(drList["Country_ID"]);
                    this._iCountryGroup_ID = Convert.ToInt32(drList["CountryGroup_ID"]);
                    this._sCountryGroup_Title = drList["CountryGroup_Title"] + "";
                    this._iSector_ID = Convert.ToInt32(drList["Sector_ID"]);
                    this._sSectorTitle = drList["Sector_Title"] + "";
                    if (Global.IsNumeric(drList["L1"]))
                       iL1 = ((Convert.ToInt32(drList["L1"]) != 0 )? Convert.ToInt32(drList["L1"]) : 0);
                    if (Global.IsNumeric(drList["L2"]))
                        iL2 = ((Convert.ToInt32(drList["L2"]) != 0) ? Convert.ToInt32(drList["L2"]) : 0);
                    this._iCategoryMorningStar = Convert.ToInt32(drList["CategoryMorningStar"]);
                    this._iCountryRisk_ID = Convert.ToInt32(drList["CountryRisk_ID"]);
                    this._sCountryRisk_Title = drList["CountryRisk_Title"] + "";
                    this._iBenchmark = Convert.ToInt32(drList["Benchmark"]);
                    this._sRiskCurr = drList["RiskCurr"] + "";
                    this._sDescriptionEn = drList["DescriptionEn"] + "";
                    this._sDescriptionGr = drList["DescriptionGr"] + "";
                    this._sDateIncorporation = drList["DateIncorporation"] + "";
                    this._decMarketCapitalization = Convert.ToDecimal(drList["MarketCapitalization"]);
                    this._sMarketCapitalizationCurr = drList["MarketCapitalizationCurr"] + "";
                    this._sMemberIndex = drList["MemberIndex"] + "";
                    this._sOfferingTypeDescription = drList["OfferingTypeDescription"] + "";
                    this._iInflationProtected = Convert.ToInt32(drList["InflationProtected"]);
                    this._decTotalAUM = Convert.ToDecimal(drList["TotalAUM"]);
                    this._sTotalAUMDate = drList["TotalAUMDate"] + "";
                    this._decAmountOutstanding = Convert.ToDecimal(drList["AmountOutstanding"]);
                    this._sAmountOutstandingDate = drList["AmountOutstandingDate"] + "";
                    this._iLeverage = Convert.ToInt32(drList["Leverage"]);
                    this._sURL = drList["URL"] + "";
                    this._sIR_URL = drList["IR_URL"] + "";
                    this._iInvestmentType = Convert.ToInt32(drList["InvestmentType"]);
                    this._iExchangeTradedNotes = Convert.ToInt32(drList["ExchangeTradedNotes"]);
                    this._iCommodityTracking = Convert.ToInt32(drList["CommodityTracking"]);
                    this._sgMaturity = Convert.ToSingle(drList["Maturity"]);
                    this._sMaturityDate = drList["MaturityDate"] + "";
                    this._sFundID = drList["FundID"] + "";
                    this._sInceptionDate = drList["InceptionDate"] + "";
                    this._sInstitutional = drList["Institutional"] + "";
                    this._iActivelyManaged = Convert.ToInt32(drList["ActivelyManaged"]);
                    this._sReplicationMethod = drList["ReplicationMethod"] + "";
                    this._sSwapBasedETF = drList["SwapBasedETF"] + "";
                    this._iIsProspectusAvailable = Convert.ToInt32(drList["IsProspectusAvailable"]);
                    this._iRatingGroup = Convert.ToInt32(drList["RatingGroup"]);
                    this._sCreditRating = drList["CreditRating"] + "";
                    this._dCreditRatingDate = Convert.ToDateTime(drList["CreditRatingDate"]);
                    this._sMoodysRating = drList["MoodysRating"] + "";
                    this._dMoodysRatingDate = Convert.ToDateTime(drList["MoodysRatingDate"]);
                    this._sFitchsRating = drList["FitchsRating"] + "";
                    this._dFitchsRatingDate = Convert.ToDateTime(drList["FitchsRatingDate"]);
                    this._sSPRating = drList["SPRating"] + "";
                    this._dSPRatingDate = Convert.ToDateTime(drList["SPRatingDate"]);
                    this._sICAPRating = drList["ICAPRating"] + "";
                    this._dICAPRatingDate = Convert.ToDateTime(drList["ICAPRatingDate"]);
                    this._sCallDate = drList["CallDate"] + "";
                    this._iRank = Convert.ToInt32(drList["Rank"]);
                    this._sDenominationType = drList["DenominationType"] + "";
                    this._iIsConvertible = Convert.ToInt32(drList["IsConvertible"]);
                    this._iIsDualCurrency = Convert.ToInt32(drList["IsDualCurrency"]);
                    this._iIsHybrid = Convert.ToInt32(drList["IsHybrid"]);
                    this._iIsGuaranteed = Convert.ToInt32(drList["IsGuaranteed"]);
                    this._iIsPerpetualSecurity = Convert.ToInt32(drList["IsPerpetualSecurity"]);
                    this._iIsTotalLoss = Convert.ToInt32(drList["IsTotalLoss"]);
                    this._sMinimumTotalLoss = drList["MinimumTotalLoss"] + "";
                    this._iIsCallable = Convert.ToInt32(drList["IsCallable"]);
                    this._iIsPutable = Convert.ToInt32(drList["IsPutable"]);
                    this._sEstimatedKIID = drList["EstimatedKIID"] + "";
                    this._sEstimatedKIID_Date = drList["EstimatedKIID_Date"] + "";
                    this._sgSurveyedKIID = Convert.ToSingle(drList["SurveyedKIID"]);
                    this._sSurveyedKIID_Date = drList["SurveyedKIID_Date"] + "";
                    this._sSurveyedKIID_History = drList["SurveyedKIID_History"] + "";
                    this._sgOngoingKIID = Convert.ToSingle(drList["OngoingKIID"]);
                    this._sOngoingKIID_Date = drList["OngoingKIID_Date"] + "";
                    this._sRatingOverall = drList["RatingOverall"] + "";
                    this._sRatingDate = drList["RatingDate"] + "";
                    this._sSRRIValues = drList["SRRIValues"] + "";
                    this._sSRRIValues_Date = drList["SRRIValues_Date"] + "";
                    this._sManagmentFee = drList["ManagmentFee"] + "";
                    this._sManagmentFee_Date = drList["ManagmentFee_Date"] + "";
                    this._sPerformanceFee = drList["PerformanceFee"] + "";
                    this._sPerformanceFee_Date = drList["PerformanceFee_Date"] + "";
                    this._sCountryRegistered = drList["CountryRegistered"] + "";
                    this._sCountryAvailable = drList["CountryAvailable"] + "";
                    this._iGreeceRegistered = Convert.ToInt32(drList["GreeceRegistered"]);
                    this._iGreeceAvailable = Convert.ToInt32(drList["GreeceAvailable"]);
                    this._iComplexProduct = Convert.ToInt32(drList["ComplexProduct"]);
                    this._sComplexAttribute = drList["ComplexAttribute"] + "";
                    this._sBBG_ComplexProduct = drList["BBG_ComplexProduct"] + "";
                    this._sBBG_ComplexAttribute = drList["BBG_ComplexAttribute"] + "";
                    this._iInvestType_Retail = Convert.ToInt32(drList["InvestType_Retail"]);
                    this._iInvestType_Prof = Convert.ToInt32(drList["InvestType_Prof"]);
                    this._iInvestType_Eligible = Convert.ToInt32(drList["InvestType_Eligible"]);
                    this._iExpertise_Basic = Convert.ToInt32(drList["Expertise_Basic"]);
                    this._iExpertise_Informed = Convert.ToInt32(drList["Expertise_Informed"]);
                    this._iExpertise_Advanced = Convert.ToInt32(drList["Expertise_Advanced"]);
                    this._sRecHoldingPeriod = drList["RecHoldingPeriod"] + "";
                    this._iRetProfile_Preserv = Convert.ToInt32(drList["RetProfile_Preserv"]);
                    this._iRetProfile_Income = Convert.ToInt32(drList["RetProfile_Income"]);
                    this._iRetProfile_Growth = Convert.ToInt32(drList["RetProfile_Growth"]);
                    this._iDistrib_ExecOnly = Convert.ToInt32(drList["Distrib_ExecOnly"]);
                    this._iDistrib_Advice = Convert.ToInt32(drList["Distrib_Advice"]);
                    this._iDistrib_PortfolioManagment = Convert.ToInt32(drList["Distrib_PortfolioManagment"]);
                    this._iCapitalLoss_None = Convert.ToInt32(drList["CapitalLoss_None"]);
                    this._iCapitalLoss_Limited = Convert.ToInt32(drList["CapitalLoss_Limited"]);
                    this._iCapitalLoss_NoGuarantee = Convert.ToInt32(drList["CapitalLoss_NoGuarantee"]);
                    this._iCapitalLoss_BeyondInitial = Convert.ToInt32(drList["CapitalLoss_BeyondInitial"]);
                    this._iCapitalLoss_Level = Convert.ToInt32(drList["CapitalLoss_Level"]);
                    this._dLastEditDate = Convert.ToDateTime(drList["LastEditDate"]);
                    this._iLastEditUser_ID = Convert.ToInt32(drList["LastEditUser_ID"]);
                    this._sLastEditUserName = drList["LastEditUserName"] + "";
                    this._iNotTradeable = Convert.ToInt32(drList["NotTradeable"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            if (iL1 != 0) {
                clsSectors Sectors = new clsSectors();
                Sectors.L1 = iL1;
                Sectors.L2 = iL2;
                Sectors.L3 = 0;
                Sectors.GetList();
                foreach (DataRow dtRow in Sectors.List.Rows)
                   _sIndustryTitle = dtRow["Title"]+"";
            }
        }
        public int GetRecord_ID(int iShareCode_ID)
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetShareCode", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", iShareCode_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["Share_ID"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            return this._iRecord_ID;
        }
        public void GetRecord_ISIN()
        {
            try
            {
                _iRecord_ID = 0;
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "ShareTitles"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ISIN"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._sISIN));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _iRecord_ID = Convert.ToInt32(drList["ID"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetComplexReasons_List()
        {
            _dtList = new DataTable("ProductsCashList");
            dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));                    
            dtCol = _dtList.Columns.Add("ComplexReason_ID", System.Type.GetType("System.Int32"));        
            dtCol = _dtList.Columns.Add("Title", System.Type.GetType("System.String"));                    

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetShareTitles_ComplexReasons", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ShareTitles_ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["ComplexReason_ID"] = drList["ComplexReason_ID"];
                    dtRow["Title"] = drList["Title"];
                    _dtList.Rows.Add(dtRow);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable("ProductsCashList");
            dtCol = _dtList.Columns.Add("Shares_ID", System.Type.GetType("System.Int32"));             // Shares.ID
            dtCol = _dtList.Columns.Add("ShareTitles_ID", System.Type.GetType("System.Int32"));        // ShareTitles.ID
            dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));                    // ShareCodes.ID

            dtCol = _dtList.Columns.Add("Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Code2", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Product", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ProductCategory", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("SecID", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ISIN", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("StockExchange_Code", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("StockExchange_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Curr", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ProductCategory_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("IR_URL", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Aktive", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Date2", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("HFCategory", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Gravity", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("MIFID_Risk", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("HFIC_Recom", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("InvestType_Retail", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("InvestType_Prof", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("TargetMarket", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("InvestmentArea", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("InvestGeography_ID", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("LastClosePrice", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("EntryPrice", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("TargetPrice", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("StopLoss", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ComplexProduct", System.Type.GetType("System.Int16"));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetShareCodes_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = this._dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];                                          // ShareCodes.ID
                    this.dtRow["Shares_ID"] = drList["Shares_ID"];                            // Shares.ID
                    this.dtRow["ShareTitles_ID"] = drList["ShareTitles_ID"];                  // ShareTitles.ID                                   
                    this.dtRow["Title"] = (drList["Onoma"] + "").Trim();
                    this.dtRow["Code"] = (drList["ShareCode"] + "").Trim();
                    this.dtRow["Code2"] = (drList["Code2"] + "").Trim();
                    this.dtRow["SecID"] = (drList["SecID"] + "").Trim();
                    this.dtRow["ISIN"] = (drList["ISIN"] + "").Trim();
                    this.dtRow["Product"] = (drList["Product_Title"] + "").Trim();
                    this.dtRow["ProductCategory"] = drList["ProductCategories_Title"] + "";
                    this.dtRow["StockExchange_Code"] = drList["StockExchange_Code"] + "";
                    this.dtRow["StockExchange_ID"] = drList["StockExchange_ID"];
                    this.dtRow["Curr"] = drList["Curr"] + "";
                    this.dtRow["Product_ID"] = drList["ShareType"];
                    this.dtRow["ProductCategory_ID"] = drList["ProductType"];
                    this.dtRow["IR_URL"] = drList["IR_URL"] + "";
                    this.dtRow["Aktive"] = drList["Aktive"];
                    this.dtRow["Date2"] = Convert.ToDateTime(drList["Date2"]).ToString("dd/MM/yyyy");
                    this.dtRow["HFCategory"] = drList["HFCategory"];
                    this.dtRow["Gravity"] = drList["Gravity"];
                    this.dtRow["MIFID_Risk"] = drList["MIFID_Risk"];
                    this.dtRow["HFIC_Recom"] = drList["HFIC_Recom"];
                    this.dtRow["InvestType_Retail"] = drList["InvestType_Retail"];
                    this.dtRow["InvestType_Prof"] = drList["InvestType_Prof"];
                    this.dtRow["TargetMarket"] = 1;
                    this.dtRow["InvestmentArea"] = drList["CountryRisk_ID"];
                    this.dtRow["InvestGeography_ID"] = (Global.IsNumeric(drList["InvestGeography_ID"]) ? drList["InvestGeography_ID"] : 0);
                    this.dtRow["LastClosePrice"] = drList["LastClosePrice"];
                    this.dtRow["EntryPrice"] = drList["EntryPrice"] + "";
                    this.dtRow["TargetPrice"] = drList["TargetPrice"] + "";
                    this.dtRow["StopLoss"] = drList["StopLoss"] + "";
                    this.dtRow["ComplexProduct"] = drList["ComplexProduct"];
                    this._dtList.Rows.Add(dtRow);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int InsertRecord()
        {
            try  {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("InsertShareTitle", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Share_ID", SqlDbType.Int).Value = this._iShare_ID;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 100).Value = this._sProductTitle.Trim();
                    cmd.Parameters.Add("@StandardTitle", SqlDbType.NVarChar, 100).Value = this._sStandardTitle.Trim();
                    cmd.Parameters.Add("@ProviderName", SqlDbType.NVarChar, 100).Value = this._sProviderName.Trim();
                    cmd.Parameters.Add("@BrandProviderName", SqlDbType.NVarChar, 100).Value = this._sBrandProviderName.Trim();
                    cmd.Parameters.Add("@ISIN", SqlDbType.NVarChar, 50).Value = this._sISIN.Trim().ToUpper();
                    cmd.Parameters.Add("@BondType", SqlDbType.Int).Value = this._iBondType;
                    cmd.Parameters.Add("@LegalStructure_ID", SqlDbType.Int).Value = this._iLegalStructure_ID;
                    cmd.Parameters.Add("@ProductType", SqlDbType.Int).Value = this._iProductCategory;
                    cmd.Parameters.Add("@HFCategory", SqlDbType.Int).Value = this._iHFCategory;
                    cmd.Parameters.Add("@MiFIDInstrumentType", SqlDbType.Int).Value = this._iMiFIDInstrumentType;
                    cmd.Parameters.Add("@AIFMD", SqlDbType.Int).Value = this._iAIFMD;
                    cmd.Parameters.Add("@MinimumInvestment", SqlDbType.NVarChar, 100).Value = this._sMinimumInvestment;
                    cmd.Parameters.Add("@GlobalBroad", SqlDbType.Int).Value = this._iGlobalBroad;
                    cmd.Parameters.Add("@Country_ID", SqlDbType.Int).Value = this._iCountry_ID;
                    cmd.Parameters.Add("@CountryGroup_ID", SqlDbType.Int).Value = this._iCountryGroup_ID;
                    cmd.Parameters.Add("@Sector_ID", SqlDbType.Int).Value = this._iSector_ID;
                    cmd.Parameters.Add("@CategoryMorningStar", SqlDbType.Int).Value = this._iCategoryMorningStar;
                    cmd.Parameters.Add("@CountryRisk_ID", SqlDbType.Int).Value = this._iCountryRisk_ID;
                    cmd.Parameters.Add("@Benchmark", SqlDbType.Int).Value = this._iBenchmark;
                    cmd.Parameters.Add("@RiskCurr", SqlDbType.NVarChar, 6).Value = this._sRiskCurr;
                    cmd.Parameters.Add("@DescriptionEn", SqlDbType.NVarChar, 3000).Value = this._sDescriptionEn;
                    cmd.Parameters.Add("@DescriptionGr", SqlDbType.NVarChar, 3000).Value = this._sDescriptionGr;
                    cmd.Parameters.Add("@DateIncorporation", SqlDbType.NVarChar, 20).Value = this._sDateIncorporation;
                    cmd.Parameters.Add("@MarketCapitalization", SqlDbType.Decimal).Value = this._decMarketCapitalization;
                    cmd.Parameters.Add("@MarketCapitalizationCurr", SqlDbType.NVarChar, 6).Value = this._sMarketCapitalizationCurr;
                    cmd.Parameters.Add("@MemberIndex", SqlDbType.NVarChar, 500).Value = this._sMemberIndex;
                    cmd.Parameters.Add("@OfferingTypeDescription", SqlDbType.NVarChar, 500).Value = this._sOfferingTypeDescription;
                    cmd.Parameters.Add("@InflationProtected", SqlDbType.Int).Value = this._iInflationProtected;
                    cmd.Parameters.Add("@TotalAUM", SqlDbType.Decimal).Value = this._decTotalAUM;
                    cmd.Parameters.Add("@TotalAUMDate", SqlDbType.NVarChar, 20).Value = this._sTotalAUMDate;
                    cmd.Parameters.Add("@AmountOutstanding", SqlDbType.Decimal).Value = this._decAmountOutstanding;
                    cmd.Parameters.Add("@AmountOutstandingDate", SqlDbType.NVarChar, 20).Value = this._sAmountOutstandingDate;
                    cmd.Parameters.Add("@Leverage", SqlDbType.Int).Value = this._iLeverage;
                    cmd.Parameters.Add("@URL", SqlDbType.NVarChar, 100).Value = this._sURL;
                    cmd.Parameters.Add("@IR_URL", SqlDbType.NVarChar, 1000).Value = this._sIR_URL;
                    cmd.Parameters.Add("@InvestmentType", SqlDbType.Int).Value = this._iInvestmentType;
                    cmd.Parameters.Add("@ExchangeTradedNotes", SqlDbType.Int).Value = this._iExchangeTradedNotes;
                    cmd.Parameters.Add("@CommodityTracking", SqlDbType.Int).Value = this._iCommodityTracking;
                    cmd.Parameters.Add("@Maturity", SqlDbType.Float).Value = this._sgMaturity;
                    cmd.Parameters.Add("@MaturityDate", SqlDbType.NVarChar, 20).Value = this._sMaturityDate;
                    cmd.Parameters.Add("@FundID", SqlDbType.NVarChar, 20).Value = this._sFundID;
                    cmd.Parameters.Add("@InceptionDate", SqlDbType.NVarChar, 20).Value = this._sInceptionDate;
                    cmd.Parameters.Add("@Institutional", SqlDbType.NVarChar, 20).Value = this._sInstitutional;
                    cmd.Parameters.Add("@ActivelyManaged", SqlDbType.Int).Value = this._iActivelyManaged;
                    cmd.Parameters.Add("@ReplicationMethod", SqlDbType.NVarChar, 20).Value = this._sReplicationMethod;
                    cmd.Parameters.Add("@SwapBasedETF", SqlDbType.NVarChar, 20).Value = this._sSwapBasedETF;
                    cmd.Parameters.Add("@IsProspectusAvailable", SqlDbType.Int).Value = this._iIsProspectusAvailable;
                    cmd.Parameters.Add("@RatingGroup", SqlDbType.Int).Value = this._iRatingGroup;
                    cmd.Parameters.Add("@CreditRating", SqlDbType.NVarChar, 20).Value = this._sCreditRating;
                    cmd.Parameters.Add("@CreditRatingDate", SqlDbType.DateTime).Value = this._dCreditRatingDate;
                    cmd.Parameters.Add("@MoodysRating", SqlDbType.NVarChar, 20).Value = this._sMoodysRating;
                    cmd.Parameters.Add("@MoodysRatingDate", SqlDbType.DateTime).Value = this._dMoodysRatingDate;
                    cmd.Parameters.Add("@FitchsRating", SqlDbType.NVarChar, 20).Value = this._sFitchsRating;
                    cmd.Parameters.Add("@FitchsRatingDate", SqlDbType.DateTime).Value = this._dFitchsRatingDate;
                    cmd.Parameters.Add("@SPRating", SqlDbType.NVarChar, 20).Value = this._sSPRating;
                    cmd.Parameters.Add("@SPRatingDate", SqlDbType.DateTime).Value = this._dSPRatingDate;
                    cmd.Parameters.Add("@ICAPRating", SqlDbType.NVarChar, 20).Value = this._sICAPRating;
                    cmd.Parameters.Add("@ICAPRatingDate", SqlDbType.DateTime).Value = this._dICAPRatingDate;
                    cmd.Parameters.Add("@CallDate", SqlDbType.NVarChar, 20).Value = this._sCallDate;
                    cmd.Parameters.Add("@Rank", SqlDbType.Int).Value = this._iRank;
                    cmd.Parameters.Add("@DenominationType", SqlDbType.NVarChar, 20).Value = this._sDenominationType;
                    cmd.Parameters.Add("@IsConvertible", SqlDbType.Int).Value = this._iIsConvertible;
                    cmd.Parameters.Add("@IsDualCurrency", SqlDbType.Int).Value = this._iIsDualCurrency;
                    cmd.Parameters.Add("@IsHybrid", SqlDbType.Int).Value = this._iIsHybrid;
                    cmd.Parameters.Add("@IsGuaranteed", SqlDbType.Int).Value = this._iIsGuaranteed;
                    cmd.Parameters.Add("@IsPerpetualSecurity", SqlDbType.Int).Value = this._iIsPerpetualSecurity;
                    cmd.Parameters.Add("@IsTotalLoss", SqlDbType.Int).Value = this._iIsTotalLoss;
                    cmd.Parameters.Add("@MinimumTotalLoss", SqlDbType.NVarChar, 20).Value = this._sMinimumTotalLoss;
                    cmd.Parameters.Add("@IsCallable", SqlDbType.Int).Value = this._iIsCallable;
                    cmd.Parameters.Add("@IsPutable", SqlDbType.Int).Value = this._iIsPutable;
                    cmd.Parameters.Add("@EstimatedKIID", SqlDbType.NVarChar, 20).Value = this._sEstimatedKIID;
                    cmd.Parameters.Add("@EstimatedKIID_Date", SqlDbType.NVarChar, 20).Value = this._sEstimatedKIID_Date;
                    cmd.Parameters.Add("@SurveyedKIID", SqlDbType.NVarChar, 20).Value = this._sgSurveyedKIID;
                    cmd.Parameters.Add("@SurveyedKIID_Date", SqlDbType.NVarChar, 20).Value = this._sSurveyedKIID_Date;
                    cmd.Parameters.Add("@SurveyedKIID_History", SqlDbType.NVarChar, 500).Value = this._sSurveyedKIID_History;
                    cmd.Parameters.Add("@OngoingKIID", SqlDbType.Float).Value = this._sgOngoingKIID;
                    cmd.Parameters.Add("@OngoingKIID_Date", SqlDbType.NVarChar, 20).Value = this._sOngoingKIID_Date;
                    cmd.Parameters.Add("@RatingOverall", SqlDbType.NVarChar, 20).Value = this._sRatingOverall;
                    cmd.Parameters.Add("@RatingDate", SqlDbType.NVarChar, 20).Value = this._sRatingDate;
                    cmd.Parameters.Add("@SRRIValues", SqlDbType.NVarChar, 20).Value = this._sSRRIValues;
                    cmd.Parameters.Add("@SRRIValues_Date", SqlDbType.NVarChar, 20).Value = this._sSRRIValues_Date;
                    cmd.Parameters.Add("@ManagmentFee", SqlDbType.NVarChar, 20).Value = this._sManagmentFee;
                    cmd.Parameters.Add("@ManagmentFee_Date", SqlDbType.NVarChar, 20).Value = this._sManagmentFee_Date;
                    cmd.Parameters.Add("@PerformanceFee", SqlDbType.NVarChar, 20).Value = this._sPerformanceFee;
                    cmd.Parameters.Add("@PerformanceFee_Date", SqlDbType.NVarChar, 20).Value = this._sPerformanceFee_Date;
                    cmd.Parameters.Add("@CountryRegistered", SqlDbType.NVarChar, 2000).Value = this._sCountryRegistered;
                    cmd.Parameters.Add("@CountryAvailable", SqlDbType.NVarChar, 2000).Value = this._sCountryAvailable;
                    cmd.Parameters.Add("@GreeceRegistered", SqlDbType.Int).Value = this._iGreeceRegistered;
                    cmd.Parameters.Add("@GreeceAvailable", SqlDbType.Int).Value = this._iGreeceAvailable;
                    cmd.Parameters.Add("@ComplexProduct", SqlDbType.Int).Value = this._iComplexProduct;
                    cmd.Parameters.Add("@ComplexAttribute", SqlDbType.NVarChar, 50).Value = this._sComplexAttribute;
                    cmd.Parameters.Add("@BBG_ComplexProduct", SqlDbType.NVarChar, 10).Value = this._sBBG_ComplexProduct;
                    cmd.Parameters.Add("@BBG_ComplexAttribute", SqlDbType.NVarChar, 50).Value = this._sBBG_ComplexAttribute;
                    cmd.Parameters.Add("@InvestType_Retail", SqlDbType.Int).Value = this._iInvestType_Retail;
                    cmd.Parameters.Add("@InvestType_Prof", SqlDbType.Int).Value = this._iInvestType_Prof;
                    cmd.Parameters.Add("@InvestType_Eligible", SqlDbType.Int).Value = this._iInvestType_Eligible;
                    cmd.Parameters.Add("@Expertise_Basic", SqlDbType.Int).Value = this._iExpertise_Basic;
                    cmd.Parameters.Add("@Expertise_Informed", SqlDbType.Int).Value = this._iExpertise_Informed;
                    cmd.Parameters.Add("@Expertise_Advanced", SqlDbType.Int).Value = this._iExpertise_Advanced;
                    cmd.Parameters.Add("@RecHoldingPeriod", SqlDbType.NVarChar, 20).Value = this._sRecHoldingPeriod;
                    cmd.Parameters.Add("@RetProfile_Preserv", SqlDbType.Int).Value = this._iRetProfile_Preserv;
                    cmd.Parameters.Add("@RetProfile_Income", SqlDbType.Int).Value = this._iRetProfile_Income;
                    cmd.Parameters.Add("@RetProfile_Growth", SqlDbType.Int).Value = this._iRetProfile_Growth;
                    cmd.Parameters.Add("@Distrib_ExecOnly", SqlDbType.Int).Value = this._iDistrib_ExecOnly;
                    cmd.Parameters.Add("@Distrib_Advice", SqlDbType.Int).Value = this._iDistrib_Advice;
                    cmd.Parameters.Add("@Distrib_PortfolioManagment", SqlDbType.Int).Value = this._iDistrib_PortfolioManagment;
                    cmd.Parameters.Add("@CapitalLoss_None", SqlDbType.Int).Value = this._iCapitalLoss_None;
                    cmd.Parameters.Add("@CapitalLoss_Limited", SqlDbType.Int).Value = this._iCapitalLoss_Limited;
                    cmd.Parameters.Add("@CapitalLoss_NoGuarantee", SqlDbType.Int).Value = this._iCapitalLoss_NoGuarantee;
                    cmd.Parameters.Add("@CapitalLoss_BeyondInitial", SqlDbType.Int).Value = this._iCapitalLoss_BeyondInitial;
                    cmd.Parameters.Add("@CapitalLoss_Level", SqlDbType.Int).Value = this._iCapitalLoss_Level;
                    cmd.Parameters.Add("@LastEditDate", SqlDbType.DateTime).Value = this._dLastEditDate;
                    cmd.Parameters.Add("@LastEditUser_ID", SqlDbType.Int).Value = this._iLastEditUser_ID;
                    cmd.Parameters.Add("@NotTradeable", SqlDbType.Int).Value = this._iNotTradeable;

                    cmd.ExecuteNonQuery();
                    _iRecord_ID = Convert.ToInt32(outParam.Value);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            return _iRecord_ID;

        }
        public void EditRecord()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("EditShareTitle", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = this._iRecord_ID;
                    //cmd.Parameters.Add("@Share_ID", SqlDbType.Int).Value = this._iShare_ID;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 100).Value = this._sProductTitle.Trim();
                    cmd.Parameters.Add("@StandardTitle", SqlDbType.NVarChar, 100).Value = this._sStandardTitle.Trim();
                    cmd.Parameters.Add("@ProviderName", SqlDbType.NVarChar, 100).Value = this._sProviderName.Trim();
                    cmd.Parameters.Add("@BrandProviderName", SqlDbType.NVarChar, 100).Value = this._sBrandProviderName.Trim();
                    cmd.Parameters.Add("@ISIN", SqlDbType.NVarChar, 50).Value = this._sISIN.Trim().ToUpper();
                    cmd.Parameters.Add("@BondType", SqlDbType.Int).Value = this._iBondType;
                    cmd.Parameters.Add("@LegalStructure_ID", SqlDbType.Int).Value = this._iLegalStructure_ID;
                    cmd.Parameters.Add("@ProductType", SqlDbType.Int).Value = this._iProductCategory;
                    cmd.Parameters.Add("@HFCategory", SqlDbType.Int).Value = this._iHFCategory;
                    cmd.Parameters.Add("@MiFIDInstrumentType", SqlDbType.Int).Value = this._iMiFIDInstrumentType;
                    cmd.Parameters.Add("@AIFMD", SqlDbType.Int).Value = this._iAIFMD;
                    cmd.Parameters.Add("@MinimumInvestment", SqlDbType.NVarChar, 100).Value = this._sMinimumInvestment;
                    cmd.Parameters.Add("@GlobalBroad", SqlDbType.Int).Value = this._iGlobalBroad;
                    cmd.Parameters.Add("@Country_ID", SqlDbType.Int).Value = this._iCountry_ID;
                    cmd.Parameters.Add("@CountryGroup_ID", SqlDbType.Int).Value = this._iCountryGroup_ID;
                    cmd.Parameters.Add("@Sector_ID", SqlDbType.Int).Value = this._iSector_ID;
                    cmd.Parameters.Add("@CategoryMorningStar", SqlDbType.Int).Value = this._iCategoryMorningStar;
                    cmd.Parameters.Add("@CountryRisk_ID", SqlDbType.Int).Value = this._iCountryRisk_ID;
                    cmd.Parameters.Add("@Benchmark", SqlDbType.Int).Value = this._iBenchmark;
                    cmd.Parameters.Add("@RiskCurr", SqlDbType.NVarChar, 6).Value = this._sRiskCurr;
                    cmd.Parameters.Add("@DescriptionEn", SqlDbType.NVarChar, 3000).Value = this._sDescriptionEn;
                    cmd.Parameters.Add("@DescriptionGr", SqlDbType.NVarChar, 3000).Value = this._sDescriptionGr;
                    cmd.Parameters.Add("@DateIncorporation", SqlDbType.NVarChar, 20).Value = this._sDateIncorporation;
                    cmd.Parameters.Add("@MarketCapitalization", SqlDbType.Decimal).Value = this._decMarketCapitalization;
                    cmd.Parameters.Add("@MarketCapitalizationCurr", SqlDbType.NVarChar, 6).Value = this._sMarketCapitalizationCurr;
                    cmd.Parameters.Add("@MemberIndex", SqlDbType.NVarChar, 500).Value = this._sMemberIndex;
                    cmd.Parameters.Add("@OfferingTypeDescription", SqlDbType.NVarChar, 500).Value = this._sOfferingTypeDescription;
                    cmd.Parameters.Add("@InflationProtected", SqlDbType.Int).Value = this._iInflationProtected;
                    cmd.Parameters.Add("@TotalAUM", SqlDbType.Decimal).Value = this._decTotalAUM;
                    cmd.Parameters.Add("@TotalAUMDate", SqlDbType.NVarChar, 20).Value = this._sTotalAUMDate;
                    cmd.Parameters.Add("@AmountOutstanding", SqlDbType.Decimal).Value = this._decAmountOutstanding;
                    cmd.Parameters.Add("@AmountOutstandingDate", SqlDbType.NVarChar, 20).Value = this._sAmountOutstandingDate;
                    cmd.Parameters.Add("@Leverage", SqlDbType.Int).Value = this._iLeverage;
                    cmd.Parameters.Add("@URL", SqlDbType.NVarChar, 100).Value = this._sURL;
                    cmd.Parameters.Add("@IR_URL", SqlDbType.NVarChar, 1000).Value = this._sIR_URL;
                    cmd.Parameters.Add("@InvestmentType", SqlDbType.Int).Value = this._iInvestmentType;
                    cmd.Parameters.Add("@ExchangeTradedNotes", SqlDbType.Int).Value = this._iExchangeTradedNotes;
                    cmd.Parameters.Add("@CommodityTracking", SqlDbType.Int).Value = this._iCommodityTracking;
                    cmd.Parameters.Add("@Maturity", SqlDbType.Float).Value = this._sgMaturity;
                    cmd.Parameters.Add("@MaturityDate", SqlDbType.NVarChar, 20).Value = this._sMaturityDate;
                    cmd.Parameters.Add("@FundID", SqlDbType.NVarChar, 20).Value = this._sFundID;
                    cmd.Parameters.Add("@InceptionDate", SqlDbType.NVarChar, 20).Value = this._sInceptionDate;
                    cmd.Parameters.Add("@Institutional", SqlDbType.NVarChar, 20).Value = this._sInstitutional;
                    cmd.Parameters.Add("@ActivelyManaged", SqlDbType.Int).Value = this._iActivelyManaged;
                    cmd.Parameters.Add("@ReplicationMethod", SqlDbType.NVarChar, 20).Value = this._sReplicationMethod;
                    cmd.Parameters.Add("@SwapBasedETF", SqlDbType.NVarChar, 20).Value = this._sSwapBasedETF;
                    cmd.Parameters.Add("@IsProspectusAvailable", SqlDbType.Int).Value = this._iIsProspectusAvailable;
                    cmd.Parameters.Add("@RatingGroup", SqlDbType.Int).Value = this._iRatingGroup;
                    cmd.Parameters.Add("@CreditRating", SqlDbType.NVarChar, 20).Value = this._sCreditRating;
                    cmd.Parameters.Add("@CreditRatingDate", SqlDbType.DateTime).Value = this._dCreditRatingDate;
                    cmd.Parameters.Add("@MoodysRating", SqlDbType.NVarChar, 20).Value = this._sMoodysRating;
                    cmd.Parameters.Add("@MoodysRatingDate", SqlDbType.DateTime).Value = this._dMoodysRatingDate;
                    cmd.Parameters.Add("@FitchsRating", SqlDbType.NVarChar, 20).Value = this._sFitchsRating;
                    cmd.Parameters.Add("@FitchsRatingDate", SqlDbType.DateTime).Value = this._dFitchsRatingDate;
                    cmd.Parameters.Add("@SPRating", SqlDbType.NVarChar, 20).Value = this._sSPRating;
                    cmd.Parameters.Add("@SPRatingDate", SqlDbType.DateTime).Value = this._dSPRatingDate;
                    cmd.Parameters.Add("@ICAPRating", SqlDbType.NVarChar, 20).Value = this._sICAPRating;
                    cmd.Parameters.Add("@ICAPRatingDate", SqlDbType.DateTime).Value = this._dICAPRatingDate;
                    cmd.Parameters.Add("@CallDate", SqlDbType.NVarChar, 20).Value = this._sCallDate;
                    cmd.Parameters.Add("@Rank", SqlDbType.Int).Value = this._iRank;
                    cmd.Parameters.Add("@DenominationType", SqlDbType.NVarChar, 20).Value = this._sDenominationType;
                    cmd.Parameters.Add("@IsConvertible", SqlDbType.Int).Value = this._iIsConvertible;
                    cmd.Parameters.Add("@IsDualCurrency", SqlDbType.Int).Value = this._iIsDualCurrency;
                    cmd.Parameters.Add("@IsHybrid", SqlDbType.Int).Value = this._iIsHybrid;
                    cmd.Parameters.Add("@IsGuaranteed", SqlDbType.Int).Value = this._iIsGuaranteed;
                    cmd.Parameters.Add("@IsPerpetualSecurity", SqlDbType.Int).Value = this._iIsPerpetualSecurity;
                    cmd.Parameters.Add("@IsTotalLoss", SqlDbType.Int).Value = this._iIsTotalLoss;
                    cmd.Parameters.Add("@MinimumTotalLoss", SqlDbType.NVarChar, 20).Value = this._sMinimumTotalLoss;
                    cmd.Parameters.Add("@IsCallable", SqlDbType.Int).Value = this._iIsCallable;
                    cmd.Parameters.Add("@IsPutable", SqlDbType.Int).Value = this._iIsPutable;
                    cmd.Parameters.Add("@EstimatedKIID", SqlDbType.NVarChar, 20).Value = this._sEstimatedKIID;
                    cmd.Parameters.Add("@EstimatedKIID_Date", SqlDbType.NVarChar, 20).Value = this._sEstimatedKIID_Date;
                    cmd.Parameters.Add("@SurveyedKIID", SqlDbType.NVarChar, 20).Value = this._sgSurveyedKIID;
                    cmd.Parameters.Add("@SurveyedKIID_Date", SqlDbType.NVarChar, 20).Value = this._sSurveyedKIID_Date;
                    cmd.Parameters.Add("@SurveyedKIID_History", SqlDbType.NVarChar, 500).Value = this._sSurveyedKIID_History;
                    cmd.Parameters.Add("@OngoingKIID", SqlDbType.Float).Value = this._sgOngoingKIID;
                    cmd.Parameters.Add("@OngoingKIID_Date", SqlDbType.NVarChar, 20).Value = this._sOngoingKIID_Date;
                    cmd.Parameters.Add("@RatingOverall", SqlDbType.NVarChar, 20).Value = this._sRatingOverall;
                    cmd.Parameters.Add("@RatingDate", SqlDbType.NVarChar, 20).Value = this._sRatingDate;
                    cmd.Parameters.Add("@SRRIValues", SqlDbType.NVarChar, 20).Value = this._sSRRIValues;
                    cmd.Parameters.Add("@SRRIValues_Date", SqlDbType.NVarChar, 20).Value = this._sSRRIValues_Date;
                    cmd.Parameters.Add("@ManagmentFee", SqlDbType.NVarChar, 20).Value = this._sManagmentFee;
                    cmd.Parameters.Add("@ManagmentFee_Date", SqlDbType.NVarChar, 20).Value = this._sManagmentFee_Date;
                    cmd.Parameters.Add("@PerformanceFee", SqlDbType.NVarChar, 20).Value = this._sPerformanceFee;
                    cmd.Parameters.Add("@PerformanceFee_Date", SqlDbType.NVarChar, 20).Value = this._sPerformanceFee_Date;
                    cmd.Parameters.Add("@CountryRegistered", SqlDbType.NVarChar, 2000).Value = this._sCountryRegistered;
                    cmd.Parameters.Add("@CountryAvailable", SqlDbType.NVarChar, 2000).Value = this._sCountryAvailable;
                    cmd.Parameters.Add("@GreeceRegistered", SqlDbType.Int).Value = this._iGreeceRegistered;
                    cmd.Parameters.Add("@GreeceAvailable", SqlDbType.Int).Value = this._iGreeceAvailable;
                    cmd.Parameters.Add("@ComplexProduct", SqlDbType.Int).Value = this._iComplexProduct;
                    cmd.Parameters.Add("@ComplexAttribute", SqlDbType.NVarChar, 50).Value = this._sComplexAttribute;
                    cmd.Parameters.Add("@BBG_ComplexProduct", SqlDbType.NVarChar, 10).Value = this._sBBG_ComplexProduct;
                    cmd.Parameters.Add("@BBG_ComplexAttribute", SqlDbType.NVarChar, 50).Value = this._sBBG_ComplexAttribute;
                    cmd.Parameters.Add("@InvestType_Retail", SqlDbType.Int).Value = this._iInvestType_Retail;
                    cmd.Parameters.Add("@InvestType_Prof", SqlDbType.Int).Value = this._iInvestType_Prof;
                    cmd.Parameters.Add("@InvestType_Eligible", SqlDbType.Int).Value = this._iInvestType_Eligible;
                    cmd.Parameters.Add("@Expertise_Basic", SqlDbType.Int).Value = this._iExpertise_Basic;
                    cmd.Parameters.Add("@Expertise_Informed", SqlDbType.Int).Value = this._iExpertise_Informed;
                    cmd.Parameters.Add("@Expertise_Advanced", SqlDbType.Int).Value = this._iExpertise_Advanced;
                    cmd.Parameters.Add("@RecHoldingPeriod", SqlDbType.NVarChar, 20).Value = this._sRecHoldingPeriod;
                    cmd.Parameters.Add("@RetProfile_Preserv", SqlDbType.Int).Value = this._iRetProfile_Preserv;
                    cmd.Parameters.Add("@RetProfile_Income", SqlDbType.Int).Value = this._iRetProfile_Income;
                    cmd.Parameters.Add("@RetProfile_Growth", SqlDbType.Int).Value = this._iRetProfile_Growth;
                    cmd.Parameters.Add("@Distrib_ExecOnly", SqlDbType.Int).Value = this._iDistrib_ExecOnly;
                    cmd.Parameters.Add("@Distrib_Advice", SqlDbType.Int).Value = this._iDistrib_Advice;
                    cmd.Parameters.Add("@Distrib_PortfolioManagment", SqlDbType.Int).Value = this._iDistrib_PortfolioManagment;
                    cmd.Parameters.Add("@CapitalLoss_None", SqlDbType.Int).Value = this._iCapitalLoss_None;
                    cmd.Parameters.Add("@CapitalLoss_Limited", SqlDbType.Int).Value = this._iCapitalLoss_Limited;
                    cmd.Parameters.Add("@CapitalLoss_NoGuarantee", SqlDbType.Int).Value = this._iCapitalLoss_NoGuarantee;
                    cmd.Parameters.Add("@CapitalLoss_BeyondInitial", SqlDbType.Int).Value = this._iCapitalLoss_BeyondInitial;
                    cmd.Parameters.Add("@CapitalLoss_Level", SqlDbType.Int).Value = this._iCapitalLoss_Level;
                    cmd.Parameters.Add("@LastEditDate", SqlDbType.DateTime).Value = this._dLastEditDate;
                    cmd.Parameters.Add("@LastEditUser_ID", SqlDbType.Int).Value = this._iLastEditUser_ID;
                    cmd.Parameters.Add("@NotTradeable", SqlDbType.Int).Value = this._iNotTradeable;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void DeleteRecord()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ShareTitles";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }

        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int Share_ID { get { return this._iShare_ID; } set { this._iShare_ID = value; } }
        public int ProductCategory { get { return this._iProductCategory; } set { this._iProductCategory = value; } }
        public string ProductType { get { return this._sProductType; } set { this._sProductType = value; } }
        public string ProductTitle { get { return this._sProductTitle; } set { this._sProductTitle = value; } }
        public string ProductCategory_Title { get { return this._sProductCategory_Title; } set { this._sProductCategory_Title = value; } }
        public string StandardTitle { get { return this._sStandardTitle; } set { this._sStandardTitle = value; } }
        public string ProviderName { get { return this._sProviderName; } set { this._sProviderName = value; } }
        public string BrandProviderName { get { return this._sBrandProviderName; } set { this._sBrandProviderName = value; } }
        public string ISIN { get { return this._sISIN; } set { this._sISIN = value; } } 
        public int BondType { get { return this._iBondType; } set { this._iBondType = value; } }
        public int LegalStructure_ID { get { return this._iLegalStructure_ID; } set { this._iLegalStructure_ID = value; } }
        public int HFCategory { get { return this._iHFCategory; } set { this._iHFCategory = value; } }
        public string HFCategory_Title { get { return this._sHFCategory_Title; } set { this._sHFCategory_Title = value; } }
        public int MiFIDInstrumentType { get { return this._iMiFIDInstrumentType; } set { this._iMiFIDInstrumentType = value; } }
        public int AIFMD { get { return this._iAIFMD; } set { this._iAIFMD = value; } }
        public string MinimumInvestment { get { return this._sMinimumInvestment; } set { this._sMinimumInvestment = value; } }
        public int GlobalBroad { get { return this._iGlobalBroad; } set { this._iGlobalBroad = value; } }
        public int Country_ID { get { return this._iCountry_ID; } set { this._iCountry_ID = value; } }
        public int CountryGroup_ID { get { return this._iCountryGroup_ID; } set { this._iCountryGroup_ID = value; } }
        public string CountryGroup_Title { get { return this._sCountryGroup_Title; } set { this._sCountryGroup_Title = value; } }
        public int Sector_ID { get { return this._iSector_ID; } set { this._iSector_ID = value; } }
        public string IndustryTitle { get { return this._sIndustryTitle; } set { this._sIndustryTitle = value; } }
        public string SectorTitle { get { return this._sSectorTitle; } set { this._sSectorTitle = value; } }
        public int CategoryMorningStar { get { return this._iCategoryMorningStar; } set { this._iCategoryMorningStar = value; } }
        public int CountryRisk_ID { get { return this._iCountryRisk_ID; } set { this._iCountryRisk_ID = value; } }
        public string CountryRisk_Title { get { return this._sCountryRisk_Title; } set { this._sCountryRisk_Title = value; } }
        public int Benchmark { get { return this._iBenchmark; } set { this._iBenchmark = value; } }
        public string RiskCurr { get { return this._sRiskCurr; } set { this._sRiskCurr = value; } }
        public string DescriptionEn { get { return this._sDescriptionEn; } set { this._sDescriptionEn = value; } }
        public string DescriptionGr { get { return this._sDescriptionGr; } set { this._sDescriptionGr = value; } }
        public string DateIncorporation { get { return this._sDateIncorporation; } set { this._sDateIncorporation = value; } }
        public decimal MarketCapitalization { get { return this._decMarketCapitalization; } set { this._decMarketCapitalization = value; } }
        public string MarketCapitalizationCurr { get { return this._sMarketCapitalizationCurr; } set { this._sMarketCapitalizationCurr = value; } }
        public string MemberIndex { get { return this._sMemberIndex; } set { this._sMemberIndex = value; } }
        public string OfferingTypeDescription { get { return this._sOfferingTypeDescription; } set { this._sOfferingTypeDescription = value; } }
        public int InflationProtected { get { return this._iInflationProtected; } set { this._iInflationProtected = value; } }
        public decimal TotalAUM { get { return this._decTotalAUM; } set { this._decTotalAUM = value; } }
        public string TotalAUMDate { get { return this._sTotalAUMDate; } set { this._sTotalAUMDate = value; } }
        public decimal AmountOutstanding { get { return this._decAmountOutstanding; } set { this._decAmountOutstanding = value; } }
        public string AmountOutstandingDate { get { return this._sAmountOutstandingDate; } set { this._sAmountOutstandingDate = value; } }
        public int Leverage { get { return this._iLeverage; } set { this._iLeverage = value; } }
        public string URL { get { return this._sURL; } set { this._sURL = value; } }
        public string IR_URL { get { return this._sIR_URL; } set { this._sIR_URL = value; } }
        public int InvestmentType { get { return this._iInvestmentType; } set { this._iInvestmentType = value; } }
        public int ExchangeTradedNotes { get { return this._iExchangeTradedNotes; } set { this._iExchangeTradedNotes = value; } }
        public int CommodityTracking { get { return this._iCommodityTracking; } set { this._iCommodityTracking = value; } }
        public float Maturity { get { return this._sgMaturity; } set { this._sgMaturity = value; } }
        public string MaturityDate { get { return this._sMaturityDate; } set { this._sMaturityDate = value; } }
        public string FundID { get { return this._sFundID; } set { this._sFundID = value; } }
        public string InceptionDate { get { return this._sInceptionDate; } set { this._sInceptionDate = value; } }
        public string Institutional { get { return this._sInstitutional; } set { this._sInstitutional = value; } }
        public int ActivelyManaged { get { return this._iActivelyManaged; } set { this._iActivelyManaged = value; } }
        public string ReplicationMethod { get { return this._sReplicationMethod; } set { this._sReplicationMethod = value; } }
        public string SwapBasedETF { get { return this._sSwapBasedETF; } set { this._sSwapBasedETF = value; } }
        public int IsProspectusAvailable { get { return this._iIsProspectusAvailable; } set { this._iIsProspectusAvailable = value; } }
        public int RatingGroup { get { return this._iRatingGroup; } set { this._iRatingGroup = value; } }
        public string CreditRating { get { return this._sCreditRating; } set { this._sCreditRating = value; } }
        public string MoodysRating { get { return this._sMoodysRating; } set { this._sMoodysRating = value; } }
        public DateTime MoodysRatingDate { get { return this._dMoodysRatingDate; } set { this._dMoodysRatingDate = value; } }
        public string FitchsRating { get { return this._sFitchsRating; } set { this._sFitchsRating = value; } }
        public DateTime FitchsRatingDate { get { return this._dFitchsRatingDate; } set { this._dFitchsRatingDate = value; } }
        public string SPRating { get { return this._sSPRating; } set { this._sSPRating = value; } }
        public DateTime SPRatingDate { get { return this._dSPRatingDate; } set { this._dSPRatingDate = value; } }
        public string ICAPRating { get { return this._sICAPRating; } set { this._sICAPRating = value; } }
        public DateTime ICAPRatingDate { get { return this._dICAPRatingDate; } set { this._dICAPRatingDate = value; } }
        public string CallDate { get { return this._sCallDate; } set { this._sCallDate = value; } }
        public int Rank { get { return this._iRank; } set { this._iRank = value; } }
        public string DenominationType { get { return this._sDenominationType; } set { this._sDenominationType = value; } }
        public int IsConvertible { get { return this._iIsConvertible; } set { this._iIsConvertible = value; } }
        public int IsDualCurrency { get { return this._iIsDualCurrency; } set { this._iIsDualCurrency = value; } }
        public int IsHybrid { get { return this._iIsHybrid; } set { this._iIsHybrid = value; } }
        public int IsGuaranteed { get { return this._iIsGuaranteed; } set { this._iIsGuaranteed = value; } }
        public int IsPerpetualSecurity { get { return this._iIsPerpetualSecurity; } set { this._iIsPerpetualSecurity = value; } }
        public int IsTotalLoss { get { return this._iIsTotalLoss; } set { this._iIsTotalLoss = value; } }
        public string MinimumTotalLoss { get { return this._sMinimumTotalLoss; } set { this._sMinimumTotalLoss = value; } }
        public int IsCallable { get { return this._iIsCallable; } set { this._iIsCallable = value; } }
        public int IsPutable { get { return this._iIsPutable; } set { this._iIsPutable = value; } }
        public string EstimatedKIID { get { return this._sEstimatedKIID; } set { this._sEstimatedKIID = value; } }
        public string EstimatedKIID_Date { get { return this._sEstimatedKIID_Date; } set { this._sEstimatedKIID_Date = value; } }
        public float SurveyedKIID { get { return this._sgSurveyedKIID; } set { this._sgSurveyedKIID = value; } }
        public string SurveyedKIID_Date { get { return this._sSurveyedKIID_Date; } set { this._sSurveyedKIID_Date = value; } }
        public string SurveyedKIID_History { get { return this._sSurveyedKIID_History; } set { this._sSurveyedKIID_History = value; } }
        public float OngoingKIID { get { return this._sgOngoingKIID; } set { this._sgOngoingKIID = value; } }
        public string OngoingKIID_Date { get { return this._sOngoingKIID_Date; } set { this._sOngoingKIID_Date = value; } }
        public string RatingOverall { get { return this._sRatingOverall; } set { this._sRatingOverall = value; } }
        public string RatingDate { get { return this._sRatingDate; } set { this._sRatingDate = value; } }
        public string SRRIValues { get { return this._sSRRIValues; } set { this._sSRRIValues = value; } }
        public string SRRIValues_Date { get { return this._sSRRIValues_Date; } set { this._sSRRIValues_Date = value; } }
        public string ManagmentFee { get { return this._sManagmentFee; } set { this._sManagmentFee = value; } }
        public string ManagmentFee_Date { get { return this._sManagmentFee_Date; } set { this._sManagmentFee_Date = value; } }
        public string PerformanceFee { get { return this._sPerformanceFee; } set { this._sPerformanceFee = value; } }
        public string PerformanceFee_Date { get { return this._sPerformanceFee_Date; } set { this._sPerformanceFee_Date = value; } }
        public string CountryRegistered { get { return this._sCountryRegistered; } set { this._sCountryRegistered = value; } }
        public string CountryAvailable { get { return this._sCountryAvailable; } set { this._sCountryAvailable = value; } }
        public int GreeceRegistered { get { return this._iGreeceRegistered; } set { this._iGreeceRegistered = value; } }
        public int GreeceAvailable { get { return this._iGreeceAvailable; } set { this._iGreeceAvailable = value; } }
        public int ComplexProduct { get { return this._iComplexProduct; } set { this._iComplexProduct = value; } }
        public string ComplexAttribute { get { return this._sComplexAttribute; } set { this._sComplexAttribute = value; } }
        public string BBG_ComplexProduct { get { return this._sBBG_ComplexProduct; } set { this._sBBG_ComplexProduct = value; } }
        public string BBG_ComplexAttribute { get { return this._sBBG_ComplexAttribute; } set { this._sBBG_ComplexAttribute = value; } }
        public int InvestType_Retail { get { return this._iInvestType_Retail; } set { this._iInvestType_Retail = value; } }
        public int InvestType_Prof { get { return this._iInvestType_Prof; } set { this._iInvestType_Prof = value; } }
        public int InvestType_Eligible { get { return this._iInvestType_Eligible; } set { this._iInvestType_Eligible = value; } }
        public int Expertise_Basic { get { return this._iExpertise_Basic; } set { this._iExpertise_Basic = value; } }
        public int Expertise_Informed { get { return this._iExpertise_Informed; } set { this._iExpertise_Informed = value; } }
        public int Expertise_Advanced { get { return this._iExpertise_Advanced; } set { this._iExpertise_Advanced = value; } }
        public string RecHoldingPeriod { get { return this._sRecHoldingPeriod; } set { this._sRecHoldingPeriod = value; } }
        public int RetProfile_Preserv { get { return this._iRetProfile_Preserv; } set { this._iRetProfile_Preserv = value; } }
        public int RetProfile_Income { get { return this._iRetProfile_Income; } set { this._iRetProfile_Income = value; } }
        public int RetProfile_Growth { get { return this._iRetProfile_Growth; } set { this._iRetProfile_Growth = value; } }
        public int Distrib_ExecOnly { get { return this._iDistrib_ExecOnly; } set { this._iDistrib_ExecOnly = value; } }
        public int Distrib_Advice { get { return this._iDistrib_Advice; } set { this._iDistrib_Advice = value; } }
        public int Distrib_PortfolioManagment { get { return this._iDistrib_PortfolioManagment; } set { this._iDistrib_PortfolioManagment = value; } }
        public int CapitalLoss_None { get { return this._iCapitalLoss_None; } set { this._iCapitalLoss_None = value; } }
        public int CapitalLoss_Limited { get { return this._iCapitalLoss_Limited; } set { this._iCapitalLoss_Limited = value; } }
        public int CapitalLoss_NoGuarantee { get { return this._iCapitalLoss_NoGuarantee; } set { this._iCapitalLoss_NoGuarantee = value; } }
        public int CapitalLoss_BeyondInitial { get { return this._iCapitalLoss_BeyondInitial; } set { this._iCapitalLoss_BeyondInitial = value; } }
        public int CapitalLoss_Level { get { return this._iCapitalLoss_Level; } set { this._iCapitalLoss_Level = value; } }
        public DateTime LastEditDate { get { return this._dLastEditDate; } set { this._dLastEditDate = value; } }
        public int LastEditUser_ID { get { return this._iLastEditUser_ID; } set { this._iLastEditUser_ID = value; } }
        public string LastEditUserName { get { return this._sLastEditUserName; } set { this._sLastEditUserName = value; } }
        public int NotTradeable { get { return this._iNotTradeable; } set { this._iNotTradeable = value; } }
        public DataTable ComplexReasons { get { return _dtComplexReasons; } set { _dtComplexReasons = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }

    }
}
