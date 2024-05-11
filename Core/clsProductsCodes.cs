using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsProductsCodes
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int _iRecord_ID;
        private int _iShare_ID;
        private DateTime _dDateFrom;
        private DateTime _dDateTo;
        private string   _sCodeTitle;
        private string _sISIN;
        private string _sSecID;
        private int _iCountryIssue;
        private int _iStockExchange_ID;
        private int _iStockExchange_Issue_ID;
        private string _sCode;
        private string _sCode2;
        private string _sCode3;
        private string _sCurr;
        private int _iPrimaryShare;
        private int _iCurrencyHedge;
        private string _sCurrencyHedge2;
        private string _sDistributionStatus;
        private DateTime _dDate1;
        private DateTime _dDate2;
        private DateTime _dDate3;
        private DateTime _dDate4;
        private string _sMonthDays;
        private string _sBaseDays;
        private int _iCouponeType;
        private float _fltCoupone;
        private float _fltLastCoupone;
        private float _fltPrice;
        private int _iFrequencyClipping;
        private int _iRevocationRight;
        private float _fltQuantitryMin;
        private float _fltQuantitryStep;
        private int _iCoveredBond;
        private string _sFRNFormula;
        private string _sFloatingRate;
        private float _fltLimits;
        private float _fltLastClosePrice;
        private string _sEntryPrice;
        private string _sTargetPrice;
        private string   _sStopLoss;
        private float    _fltGravity;
        private int      _iHFIC_Recom;
        private string   _sMIFID_Risk;
        private DateTime _dDateIPO;
        private int      _iAktive;
        private int      _iInfoFlag;

        private int _iProduct_ID;
        private string _sProduct_Title;
        private int _iProductCategory_ID;
        private string _sProductCategory_Title;
        private int _iProduct_Group;
        private int _iBondType;
        private string _sStockExchange_Code;
        private int _iCountryAction;
        private int _iCountryRisk_ID;
        private int _iCountryGroup_ID;
        private string _sHFCategory_Title;
        private string _sGlobalBroadCategory_Title;
        private int _iRatingGroup;
        private string _sCreditRating;
        private string _sMoodysRating;
        private string _sFitchsRating;
        private string _sSPRating;
        private float _fltSurveyedKIID;
        private float _fltMaturity;
        private float _fltAmountOutstanding;
        private DateTime _dDateIns;
        private DateTime _dRateDate;

        private int _iShareTitles_ID;
        private int _iInvestType_Retail;
        private int _iInvestType_Prof;
        private int _iDistrib_ExecOnly;
        private int _iDistrib_Advice;
        private int _iDistrib_PortfolioManagment;
        private int _iComplexProduct;
        private int _iIsCallable;
        private int _iIsPutable;
        private int _iIsConvertible;
        private int _iIsPerpetualSecurity;
        private int _iIsDualCurrency;
        private int _iIsHybrid;
        private int _iIsGuaranteed;
        private int _iIsTotalLoss;
        private int _iLeverage;
        private int _iMiFIDInstrumentType;
        private int _iAIFMD;
        private int _iInvestGeography_ID;
        private int _iRank;
        private string _sRiskCurr;
        private string _sFilter;
        private string _sComplexReasonsList;
        private string _sRank_Title; 
        private string _sComplexAttribute;
        private int _iOldShare_ID;
        private int _iNewShare_ID;

        private int _iStatus;
        private string _sCurrency;
        private DataTable _dtList;
        public clsProductsCodes()
        {
            this._iRecord_ID = 0;
            this._iShare_ID = 0;
            this._dDateFrom = Convert.ToDateTime("1900/01/01");
            this._dDateTo = Convert.ToDateTime("1900/01/01");
            this._sCodeTitle = "";
            this._sSecID = "";
            this._sISIN = "";
            this._iCountryIssue = 0;
            this._iStockExchange_Issue_ID = 0;
            this._iStockExchange_ID = 0;
            this._sCode = "";
            this._sCode2 = "";
            this._sCode3 = "";
            this._sCurr = "";
            this._iPrimaryShare = 0;
            this._iCurrencyHedge = 0;
            this._sCurrencyHedge2 = "";
            this._sDistributionStatus = "";
            this._dDate1 = Convert.ToDateTime("1900/01/01");
            this._dDate2 = Convert.ToDateTime("1900/01/01");
            this._dDate3 = Convert.ToDateTime("1900/01/01");
            this._dDate4 = Convert.ToDateTime("1900/01/01");
            this._sMonthDays = "";
            this._sBaseDays = "";
            this._iCouponeType = 0;
            this._fltCoupone = 0;
            this._fltLastCoupone = 0;
            this._fltPrice = 0;
            this._iFrequencyClipping = 0;
            this._iRevocationRight = 0;
            this._fltQuantitryMin = 0;
            this._fltQuantitryStep = 0;
            this._iCoveredBond = 0;
            this._sFRNFormula = "";
            this._sFloatingRate = "";
            this._fltLimits = 0;
            this._fltLastClosePrice = 0;
            this._sEntryPrice = "";
            this._sTargetPrice = "";
            this._sStopLoss = "";
            this._fltGravity = 0;
            this._iHFIC_Recom = 0;
            this._sMIFID_Risk = "000000";
            this._dDateIPO = Convert.ToDateTime("1900/01/01");
            this._iAktive = 0;
            this._iInfoFlag = 0;

            this._iProduct_ID = 0;
            this._sProduct_Title = "";
            this._iProductCategory_ID = 0;
            this._iBondType = 0;
            this._iCountryAction = 0;
            this._iCountryRisk_ID = 0;
            this._iCountryGroup_ID = 0;
            this._sProductCategory_Title = "";
            this._iProduct_Group = 0;
            this._sStockExchange_Code = "";
            this._sHFCategory_Title = "";
            this._sGlobalBroadCategory_Title = "";
            this._iRatingGroup = 0;
            this._sCreditRating = "";
            this._sMoodysRating = "";
            this._sFitchsRating = "";
            this._sSPRating = "";
            this._fltSurveyedKIID = 0;
            this._fltMaturity = 0;
            this._fltAmountOutstanding = 0;

            this._iShareTitles_ID = 0;
            this._iInvestType_Retail = 0;
            this._iInvestType_Prof = 0;
            this._iDistrib_ExecOnly = 0;
            this._iDistrib_Advice = 0;
            this._iDistrib_PortfolioManagment = 0;
            this._iComplexProduct = 0;
            this._iIsCallable = 0;
            this._iIsPutable = 0;
            this._iIsConvertible = 0;
            this._iIsPerpetualSecurity = 0;
            this._iLeverage = 0;
            this._iMiFIDInstrumentType = 0;
            this._iAIFMD = 0;
            this._iInvestGeography_ID = 0;
            this._iRank = 0;
            this._sRiskCurr = "";
            this._sFilter = "";
            this._sComplexReasonsList = "";
            this._sRank_Title = "";
            this._sComplexAttribute = "";
            this._iOldShare_ID = 0;
            this._iNewShare_ID = 0;

            this._iStatus = 0;
            this._sCurrency = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetShareCode", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iShare_ID = Convert.ToInt32(drList["Share_ID"]);
                    this._dDateFrom = Convert.ToDateTime(drList["DateFrom"]);
                    this._dDateTo = Convert.ToDateTime(drList["DateTo"]);
                    this._sCodeTitle = drList["Title"] + "";
                    this._sSecID = drList["SecID"] + "";
                    this._sISIN = drList["ISIN"] + "";
                    this._iCountryIssue = Convert.ToInt32(drList["CountryIssue"]);
                    this._iStockExchange_Issue_ID = Convert.ToInt32(drList["StockExchange_Issue_ID"]);
                    this._iStockExchange_ID = Convert.ToInt32(drList["StockExchange_ID"]);
                    this._sCode = drList["Code"] + "";
                    this._sCode2 = drList["Code2"] + "";
                    this._sCode3 = drList["Code3"] + "";
                    this._sCurr = drList["Curr"] + "";
                    this._iPrimaryShare = Convert.ToInt32(drList["PrimaryShare"]);
                    this._iCurrencyHedge = Convert.ToInt32(drList["CurrencyHedge"]);
                    this._sCurrencyHedge2 = drList["CurrencyHedge2"] + "";
                    this._sDistributionStatus = drList["DistributionStatus"] + "";
                    this._dDate1 = Convert.ToDateTime(drList["Date1"]);
                    this._dDate2 = Convert.ToDateTime(drList["Date2"]);
                    this._dDate3 = Convert.ToDateTime(drList["Date3"]);
                    this._dDate4 = Convert.ToDateTime(drList["Date4"]);
                    this._sMonthDays = drList["MonthDays"] + "";
                    this._sBaseDays = drList["BaseDays"] + "";
                    this._iCouponeType = Convert.ToInt32(drList["CouponeType"]);
                    this._fltCoupone = Convert.ToSingle(drList["Coupone"]);
                    this._fltLastCoupone = Convert.ToSingle(drList["LastCoupone"]);
                    this._fltPrice = Convert.ToSingle(drList["Price"]);
                    this._iFrequencyClipping = Convert.ToInt32(drList["FrequencyClipping"]);
                    this._iRevocationRight = Convert.ToInt32(drList["RevocationRight"]);
                    this._fltQuantitryMin = Convert.ToInt32(drList["QuantityMin"]);
                    this._fltQuantitryStep = Convert.ToInt32(drList["QuantityStep"]);
                    this._iCoveredBond = Convert.ToInt32(drList["CoveredBond"]);
                    this._sFRNFormula = drList["FRNFormula"] + "";
                    this._sFloatingRate = drList["FloatingRate"] + "";
                    this._fltLimits = Convert.ToSingle(drList["Limits"]);
                    this._fltLastClosePrice = Convert.ToSingle(drList["LastClosePrice"]);
                    this._sEntryPrice = drList["EntryPrice"] + "";
                    this._sTargetPrice = drList["TargetPrice"] + "";
                    this._sStopLoss = drList["StopLoss"] + "";
                    this._fltGravity = Convert.ToSingle(drList["Gravity"]);
                    this._iHFIC_Recom = Convert.ToInt32(drList["HFIC_Recom"]);
                    this._sMIFID_Risk = drList["MIFID_Risk"] + "";
                    this._dDateIPO = Convert.ToDateTime(drList["DateIPO"]);
                    this._iProduct_ID = Convert.ToInt32(drList["Product_ID"]);
                    this._sProduct_Title = drList["Product_Title"] + "";
                    this._iProductCategory_ID = Convert.ToInt32(drList["ProductCategory_ID"]);
                    this._sProductCategory_Title = drList["ProductCategory_Title"] + "";

                    if (_iProduct_ID == 1) this._iProduct_Group = 2;
                    else if (_iProduct_ID == 2) this._iProduct_Group = 1;
                    else
                        switch (Convert.ToInt16(drList["GlobalBroad"]))
                        {
                            case 1:
                                this._iProduct_Group = 2;
                                break;
                            case 2:
                                this._iProduct_Group = 1;
                                break;
                            case 3:
                                this._iProduct_Group = 4;
                                break;
                            default:
                                this._iProduct_Group = 3;
                                break;
                        }

                    this._sStockExchange_Code = drList["StockExchange_Code"] + "";
                    this._iBondType = ((drList["BondType"] + "") != "") ? Convert.ToInt32(drList["BondType"]) : 0;
                    this._iCountryAction = ((drList["CountryAction_ID"] + "") != "") ? Convert.ToInt32(drList["CountryAction_ID"]) : 0;
                    this._iCountryRisk_ID = ((drList["CountryRisk_ID"] + "") != "") ? Convert.ToInt32(drList["CountryRisk_ID"]) : 0;
                    this._iCountryGroup_ID = ((drList["CountryGroup_ID"] + "") != "") ? Convert.ToInt32(drList["CountryGroup_ID"]) : 0;
                    this._sHFCategory_Title = drList["HFCategory_Title"] + "";
                    this._sGlobalBroadCategory_Title = drList["GlobalBroadCategory_Title"] + "";
                    this._iRatingGroup = Convert.ToInt32(drList["RatingGroup"]);
                    this._sCreditRating = drList["CreditRating"] + "";
                    this._sMoodysRating = drList["MoodysRating"] + "";
                    this._sFitchsRating = drList["FitchsRating"] + "";
                    this._sSPRating = drList["SPRating"] + "";
                    this._fltSurveyedKIID = Convert.ToSingle(drList["SurveyedKIID"]);
                    this._fltMaturity = Convert.ToSingle(drList["Maturity"]);
                    this._fltAmountOutstanding = Convert.ToSingle(drList["AmountOutstanding"]);
                    this._iInvestType_Retail = Convert.ToInt32(drList["InvestType_Retail"]);
                    this._iInvestType_Prof = Convert.ToInt32(drList["InvestType_Prof"]);
                    this._iDistrib_ExecOnly = Convert.ToInt32(drList["Distrib_ExecOnly"]);
                    this._iDistrib_Advice = Convert.ToInt32(drList["Distrib_Advice"]);
                    this._iDistrib_PortfolioManagment = Convert.ToInt32(drList["Distrib_PortfolioManagment"]);
                    this._iComplexProduct = Convert.ToInt32(drList["ComplexProduct"]);
                    if (_iCountryRisk_ID != 0) this._iInvestGeography_ID = Convert.ToInt32(drList["InvestGeography_ID"]);
                    else this._iInvestGeography_ID = 0;
                    this._iAktive = Convert.ToInt32(drList["Aktive"]);
                    this._iInfoFlag = Convert.ToInt32(drList["InfoFlag"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList_WishList()
        {
            DataRow[] foundRows;

            _dtList = new DataTable("ProductCodesWishList");
            dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Shares_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ShareTitles_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ShareTitles_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Product_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ProductCategory_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ProductCategory_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("HFCategory", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("HFCategory_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("GlobalBroadCategory", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("GlobalBroadCategory_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("CodeTitle", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ISIN", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("SecID", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Code2", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Code3", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("StockExchange_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("StockExchange_Code", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("RiskCurr", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("CreditRating", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("MoodysRating", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("FitchsRating", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("SPRating", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ICAPRating", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("RatingGroup", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("CountryGroup_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("CountryGroup_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("CountryRisk_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("CountryRisk_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("InvestGeography_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Date2", System.Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("Maturity", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("Maturity_Date", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("CurrencyHedge", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("CurrencyHedge2", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("SurveyedKIID", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("SurveyedKIID_Date", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Distrib_ExecOnly", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("Distrib_Advice", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("Distrib_PortfolioManagment", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("Weight", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("LastClosePrice", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("MIFID_Risk", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Rank_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("IR_URL", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Retail", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("Professional", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("Leverage", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("MiFIDInstrumentType", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("AIFMD", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("IsCallable", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("IsPutable", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("IsConvertible", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("IsPerpetualSecurity", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("IsGuaranteed", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("ComplexProduct", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("ComplexAttribute", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ComplexReasonsList", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Aktive", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("HFIC_Recom", System.Type.GetType("System.Int16"));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetShareCodes_ProductType", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Product_ID", _iProduct_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["Shares_ID"] = drList["Shares_ID"];
                    dtRow["ShareTitles_ID"] = drList["ShareTitles_ID"];
                    dtRow["ShareTitles_Title"] = drList["ShareTitles_Title"] + "";
                    dtRow["Product_ID"] = drList["ShareType"];
                    dtRow["Product_Title"] = drList["Product_Title"];
                    dtRow["ProductCategory_ID"] = drList["ProductType"];
                    dtRow["ProductCategory_Title"] = drList["ProductCategory_Title"];
                    dtRow["HFCategory"] = drList["HFCategory"];
                    dtRow["HFCategory_Title"] = drList["HFCategory_Title"] + "";
                    dtRow["GlobalBroadCategory"] = drList["GlobalBroad"];
                    dtRow["GlobalBroadCategory_Title"] = drList["GlobalBroadCategory_Title"] + "";
                    dtRow["CodeTitle"] = drList["CodeTitle"] + "";
                    dtRow["ISIN"] = drList["ISIN"] + "";
                    dtRow["SecID"] = drList["SecID"] + "";
                    dtRow["Code"] = drList["ShareCode"] + "";
                    dtRow["Code2"] = drList["ShareCode2"] + "";
                    dtRow["Code3"] = drList["ShareCode3"] + "";
                    dtRow["StockExchange_ID"] = (Global.IsNumeric(drList["StockExchange_ID"])? Convert.ToInt32(drList["StockExchange_ID"]) : 0);
                    dtRow["StockExchange_Code"] = drList["StockExchange_Code"] + "";
                    dtRow["Currency"] = drList["Curr"] + "";
                    dtRow["RiskCurr"] = drList["RiskCurr"] + "";
                    dtRow["CreditRating"] = drList["CreditRating"] + "";
                    dtRow["MoodysRating"] = drList["MoodysRating"] + "";
                    dtRow["FitchsRating"] = drList["FitchsRating"] + "";
                    dtRow["SPRating"] = drList["SPRating"] + "";
                    dtRow["ICAPRating"] = drList["ICAPRating"] + "";
                    dtRow["RatingGroup"] = drList["RatingGroup"];
                    dtRow["CountryGroup_ID"] = (Global.IsNumeric(drList["CountryGroup_ID"])? Convert.ToInt32(drList["CountryGroup_ID"]) : 0);
                    dtRow["CountryGroup_Title"] = drList["CountryGroup_Title"] + "";
                    dtRow["CountryRisk_ID"] = (Global.IsNumeric(drList["CountryRisk_ID"])? Convert.ToInt32(drList["CountryRisk_ID"]) : 0);
                    dtRow["CountryRisk_Title"] = drList["CountryRisk_Title"] + "";
                    dtRow["InvestGeography_ID"] = (Global.IsNumeric(drList["InvestGeography_ID"])? Convert.ToInt32(drList["InvestGeography_ID"]) : 0);
                    dtRow["Date2"] = drList["Date2"];
                    dtRow["Maturity"] = drList["Maturity"];
                    dtRow["Maturity_Date"] = drList["MaturityDate"];
                    dtRow["CurrencyHedge"] = drList["CurrencyHedge"];
                    dtRow["CurrencyHedge2"] = drList["CurrencyHedge2"] + "";
                    dtRow["SurveyedKIID"] = drList["SurveyedKIID"];
                    dtRow["SurveyedKIID_Date"] = drList["SurveyedKIID_Date"];
                    dtRow["Distrib_ExecOnly"] = drList["Distrib_ExecOnly"];
                    dtRow["Distrib_Advice"] = drList["Distrib_Advice"];
                    dtRow["Distrib_PortfolioManagment"] = drList["Distrib_PortfolioManagment"];
                    dtRow["Weight"] = drList["Gravity"];
                    dtRow["LastClosePrice"] = drList["LastClosePrice"];
                    dtRow["MIFID_Risk"] = drList["MIFID_Risk"];
                    dtRow["Rank_Title"] = drList["Rank_Title"];
                    dtRow["IR_URL"] = drList["IR_URL"] + "";
                    dtRow["Retail"] = drList["InvestType_Retail"];
                    dtRow["Professional"] = drList["InvestType_Prof"];
                    dtRow["Leverage"] = drList["Leverage"];
                    dtRow["MiFIDInstrumentType"] = drList["MiFIDInstrumentType"];
                    dtRow["AIFMD"] = drList["AIFMD"];
                    dtRow["IsCallable"] = drList["IsCallable"];
                    dtRow["IsPutable"] = drList["IsPutable"];
                    dtRow["IsConvertible"] = drList["IsConvertible"];
                    dtRow["IsPerpetualSecurity"] = drList["IsPerpetualSecurity"];
                    dtRow["IsGuaranteed"] = drList["IsGuaranteed"];
                    dtRow["ComplexProduct"] = drList["ComplexProduct"];
                    dtRow["ComplexAttribute"] = drList["ComplexAttribute"];
                    dtRow["ComplexReasonsList"] = "";
                    dtRow["Aktive"] = drList["Aktive"];
                    dtRow["HFIC_Recom"] = drList["HFIC_Recom"];
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();

                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "ShareTitles_ComplexReasons"));
                cmd.Parameters.Add(new SqlParameter("@Col", ""));
                cmd.Parameters.Add(new SqlParameter("@Value", ""));
                cmd.Parameters.Add(new SqlParameter("@Order", "ID"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    foundRows = _dtList.Select("ShareTitles_ID = " + drList["ShareTitles_ID"]);
                    if (foundRows.Length > 0)
                       foundRows[0]["ComplexReasonsList"] = (foundRows[0]["ComplexReasonsList"]+"") + (drList["ComplexReason_ID"]+"") + ",";     
                }
                drList.Close();

            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList_InfoFlag()
        {
            try
            {
                _dtList = new DataTable("ProductList_ZeroInfoFlagList");
                dtCol = _dtList.Columns.Add("ShareCode_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ISIN", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SE_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("InfoFlag", System.Type.GetType("System.Int16"));

                conn.Open();
                cmd = new SqlCommand("GetProductList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Product_ID", _iProduct_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ShareCode_ID"] = drList["ID"];
                    dtRow["ISIN"] = drList["ISIN"] + "";
                    dtRow["Code"] = drList["Code"] + "";
                    dtRow["Currency"] = drList["Curr"] + "";
                    dtRow["SE_Code"] = drList["SE_Code"] + "";
                    dtRow["InfoFlag"] = drList["InfoFlag"];
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetRecord_Code()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetShareData_AllCodes", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Code", _sCode));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);                                          // ShareCodes.ID
                    this._sCode = (drList["ShareCode"] + "").Trim();
                    this._sISIN = (drList["ISIN"] + "").Trim();
                    this._sCodeTitle = (drList["Title"] + "").Trim();
                    this._iProduct_ID = Convert.ToInt32(drList["ShareType"]);
                    this._sProduct_Title = (drList["ProductTitle"] + "").Trim();
                    this._iProductCategory_ID = Convert.ToInt32(drList["ProductType"]);
                    this._iStockExchange_ID = Convert.ToInt32(drList["StockExchange_ID"]);
                    this._sStockExchange_Code = drList["StockExchange_Code"] + "";
                    this._sCurr = drList["Curr"] + "";
                    this._iAktive = Convert.ToInt32(drList["Aktive"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetRecord_ISIN()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetShareData_ISIN", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ISIN", _sISIN));
                cmd.Parameters.Add(new SqlParameter("@Currency", _sCurrency));
                cmd.Parameters.Add(new SqlParameter("@StockExchange_ID", _iStockExchange_ID));
                cmd.Parameters.Add(new SqlParameter("@Aktive", _iStatus));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._sCurr = drList["Curr"] + "";
                    _iRecord_ID = Convert.ToInt32(drList["ID"]);
                    _iShareTitles_ID = Convert.ToInt32(drList["ShareTitles_ID"]);
                    if (Global.IsNumeric(drList["ShareType"])) {
                        _sCodeTitle = (drList["Title"] + "").Trim();
                        _sCode = (drList["Code"] + "").Trim();
                        _sISIN = (drList["ISIN"] + "").Trim();
                        _sCurr = drList["Curr"] + "";
                        _iProduct_ID = Convert.ToInt32(drList["ShareType"]);
                        _iProductCategory_ID = Convert.ToInt32(drList["ProductType"]);
                        _iProduct_Group = Convert.ToInt32(drList["GlobalBroad"]);
                        _iStockExchange_ID = (Global.IsNumeric(drList["StockExchange_ID"])? Convert.ToInt32(drList["StockExchange_ID"]): 0);
                        _sMIFID_Risk = drList["MIFID_Risk"] + "";
                        _iInvestType_Retail = Convert.ToInt32(drList["InvestType_Retail"]);
                        _iInvestType_Prof = Convert.ToInt32(drList["InvestType_Prof"]);
                        _iDistrib_ExecOnly = Convert.ToInt32(drList["Distrib_ExecOnly"]);
                        _iDistrib_Advice = Convert.ToInt32(drList["Distrib_Advice"]);
                        _iDistrib_PortfolioManagment = Convert.ToInt32(drList["Distrib_PortfolioManagment"]);
                        _sRiskCurr = drList["RiskCurr"] + "";
                        _sCurrencyHedge2 = drList["CurrencyHedge2"] + "";
                        _iComplexProduct = Convert.ToInt32(drList["ComplexProduct"]);
                        _iRank = Convert.ToInt32(drList["Rank"]);
                        _sRank_Title = drList["Rank_Title"] + "";
                        _iIsCallable = Convert.ToInt32(drList["IsCallable"]);
                        _iIsPutable = Convert.ToInt32(drList["IsPutable"]);
                        _iIsConvertible = Convert.ToInt32(drList["IsConvertible"]);
                        _iIsPerpetualSecurity = Convert.ToInt32(drList["IsPerpetualSecurity"]);
                        _sComplexAttribute = drList["ComplexAttribute"] + "";
                        _iLeverage = Convert.ToInt32(drList["Leverage"]);
                        _iMiFIDInstrumentType = Convert.ToInt32(drList["MiFIDInstrumentType"]);
                        _iAIFMD = Convert.ToInt32(drList["AIFMD"]);
                        _sGlobalBroadCategory_Title = ""; 
                        _iInvestGeography_ID = (Global.IsNumeric(drList["InvestGeography_ID"]) ? Convert.ToInt32(drList["InvestGeography_ID"]) : 0);
                        _iCountryGroup_ID = (Global.IsNumeric(drList["CountryGroup_ID"]) ? Convert.ToInt32(drList["CountryGroup_ID"]) : 0);

                        this._iProduct_Group = 0;

                        if (_iProduct_ID == 1) this._iProduct_Group = 2;
                        else if (_iProduct_ID == 2) this._iProduct_Group = 1;
                        else 
                            switch (Convert.ToInt16(drList["GlobalBroad"]))
                            {
                                case 1:
                                    this._iProduct_Group = 2;
                                    break;
                                case 2:
                                    this._iProduct_Group = 1;
                                    break;
                                case 3:
                                    this._iProduct_Group = 4;
                                    break;
                                default:
                                    this._iProduct_Group = 3;
                                    break;
                            }
                    }
                }
                drList.Close();

                _sComplexReasonsList = "";  
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "ShareTitles_ComplexReasons"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ShareTitles_ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", _iShareTitles_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", "ID"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                   _sComplexReasonsList = _sComplexReasonsList + (drList["ComplexReason_ID"] + "") + ",";
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

        }
        public void GetPricesList()
        {
            _dtList = new DataTable("ProductsCashList");
            dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));                    // SharePrices.ID
            dtCol = _dtList.Columns.Add("ShareCodes_ID", System.Type.GetType("System.Int32"));         // ShareCodes.ID
            dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Product_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ProductCategory_Title", System.Type.GetType("System.String"));                  
            dtCol = _dtList.Columns.Add("Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Code2", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ISIN", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("DateIns", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Close", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Last", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetPricesList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dDateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dDateTo));
                cmd.Parameters.Add(new SqlParameter("@ProductType_ID", _iProduct_ID));
                cmd.Parameters.Add(new SqlParameter("@ProductCategory_ID", _iProductCategory_ID));
                cmd.Parameters.Add(new SqlParameter("@ShareCodes_ID", _iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Filter", _sFilter));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = this._dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];                                                  // SharePrices.ID
                    this.dtRow["ShareCodes_ID"] = drList["ShareCodes_ID"];                            // ShareCodes.ID
                    this.dtRow["Product_ID"] = drList["ShareType"];
                    this.dtRow["Product_Title"] = drList["Product_Title"] + "";
                    this.dtRow["ProductCategory_Title"] = drList["ProductCategory_Title"] + "";
                    this.dtRow["Title"] = drList["Title"] + "";
                    this.dtRow["Code"] = drList["ShareCode"] + "";
                    this.dtRow["Code2"] = drList["Code2"] + "";
                    this.dtRow["ISIN"] = drList["ISIN"] + "";
                    this.dtRow["DateIns"] = Convert.ToDateTime(drList["DateIns"]).ToString("yyyy/MM/dd");
                    this.dtRow["Close"] = Convert.ToDouble(drList["Close"]) == -999999 ? "-" : drList["Close"] + "";
                    this.dtRow["Last"] = Convert.ToDouble(drList["Last"]) == -999999 ? "-" : drList["Last"] + "";
                    this.dtRow["Currency"] = drList["Curr"] + "";                   
                    this._dtList.Rows.Add(dtRow);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetPrices_Period()
        {
            _dtList = new DataTable("ProductsCashList");
            dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));                    // SharePrices.ID
            dtCol = _dtList.Columns.Add("ShareCodes_ID", System.Type.GetType("System.Int32"));         // ShareCodes.ID
            //dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
            //dtCol = _dtList.Columns.Add("Product_Title", System.Type.GetType("System.String"));
            //dtCol = _dtList.Columns.Add("ProductCategory_Title", System.Type.GetType("System.String"));
            //dtCol = _dtList.Columns.Add("Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
            //dtCol = _dtList.Columns.Add("Code2", System.Type.GetType("System.String"));
            //dtCol = _dtList.Columns.Add("ISIN", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("DateIns", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Close", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Last", System.Type.GetType("System.String"));
            //dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetCurrencyRate_Period", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dDateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dDateTo));
                cmd.Parameters.Add(new SqlParameter("@Code", _sCode));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = this._dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];                                             // SharePrices.ID
                    this.dtRow["ShareCodes_ID"] = drList["Share_ID"];                            // ShareCodes.ID
                    //this.dtRow["Product_ID"] = drList["ShareType"];
                    //this.dtRow["Product_Title"] = drList["Product_Title"] + "";
                    //this.dtRow["ProductCategory_Title"] = drList["ProductCategory_Title"] + "";
                    //this.dtRow["Title"] = drList["Title"] + "";
                    this.dtRow["Code"] = drList["Code"] + "";
                    //this.dtRow["Code2"] = drList["Code2"] + "";
                    //this.dtRow["ISIN"] = drList["ISIN"] + "";
                    this.dtRow["DateIns"] = Convert.ToDateTime(drList["DateIns"]).ToString("yyyy/MM/dd");
                    this.dtRow["Close"] = Convert.ToDouble(drList["Close"]) == -999999 ? "-" : drList["Close"] + "";
                    this.dtRow["Last"] = Convert.ToDouble(drList["Last"]) == -999999 ? "-" : drList["Last"] + "";
                    //this.dtRow["Currency"] = drList["Curr"] + "";
                    this._dtList.Rows.Add(dtRow);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetPrice_Code()
        {
            try                
            {
                conn.Open();
                cmd = new SqlCommand("GetCurrencyRate", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateIns", _dDateIns));
                cmd.Parameters.Add(new SqlParameter("@Code", _sCode));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dDateIns = Convert.ToDateTime(drList["DateIns"]);
                    _fltLastClosePrice = Convert.ToSingle(drList["Close"]);                  
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetPrice_ISIN()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetSharePrices", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateIns", _dDateIns));
                cmd.Parameters.Add(new SqlParameter("@ISIN", _sISIN));
                cmd.Parameters.Add(new SqlParameter("@Currency", _sCurr));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dDateIns = Convert.ToDateTime(drList["DateIns"]);
                    _fltLastClosePrice = Convert.ToSingle(drList["Close"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable("ProductsCodesList");
            dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("DateFrom", System.Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("DateTo", System.Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("CodeTitle", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ISIN", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Code2", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Code3", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("SecID", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("StockExchange_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("StockExchange_Code", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("CountryAction_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("CountryAction_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("RiskCurr", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("CurrencyHedge", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("CurrencyHedge2", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("CountryIssue_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("CountryIssue_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("StockExchange_Issue_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("StockExchange_Issue_Code", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("PrimaryShare", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("PrimaryShare_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("DistributionStatus", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Date1", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Date2", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Date3", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Date4", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("MonthDays", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("BaseDays", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("CouponeType", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("CouponeType_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Coupone", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("LastCoupone", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("Price", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("FrequencyClipping", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("RevocationRight", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("RevocationRights_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("QuantityMin", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("QuantityStep", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("CoveredBond", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("Rank", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("Rank_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("FloatingRate", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("FRNFormula", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Limits", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("LastClosePrice", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("EntryPrice", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("TargetPrice", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("StopLoss", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("DateIPO", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("HFIC_Recom", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("HFIC_Recom_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("MIFID_Risk", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Weight", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("Aktive", System.Type.GetType("System.Int16"));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetShareCodes", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Share_ID", _iShare_ID));
                cmd.Parameters.Add(new SqlParameter("@ISIN", _sISIN));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = this._dtList.NewRow();                                
                    this.dtRow["ID"] = drList["ID"];
                    this.dtRow["Product_ID"] = drList["ShareType"];
                    this.dtRow["DateFrom"] = drList["DateFrom"];
                    this.dtRow["DateTo"] = drList["DateTo"];
                    this.dtRow["CodeTitle"] = drList["Onoma"] + "";
                    this.dtRow["ISIN"] = (drList["ISIN"] + "").Trim();
                    this.dtRow["Code"] = (drList["ShareCode"] + "").Trim();
                    this.dtRow["Code2"] = (drList["Code2"] + "").Trim();
                    this.dtRow["Code3"] = (drList["Code3"] + "").Trim();
                    this.dtRow["SecID"] = (drList["SecID"] + "").Trim();
                    this.dtRow["StockExchange_ID"] = drList["StockExchange_ID"];
                    this.dtRow["StockExchange_Code"] = drList["StockExchange_Code"] + "";
                    this.dtRow["CountryAction_ID"] = ((drList["CountryAction_ID"]+"") != "")? Convert.ToInt32(drList["CountryAction_ID"]): 0;
                    this.dtRow["CountryAction_Title"] = drList["CountryAction_Title"] + "";
                    this.dtRow["Currency"] = drList["Curr"] + "";
                    this.dtRow["RiskCurr"] = drList["RiskCurr"] + "";
                    this.dtRow["CurrencyHedge"] = drList["CurrencyHedge"];
                    this.dtRow["CurrencyHedge2"] = drList["CurrencyHedge2"] + "";
                    this.dtRow["CountryIssue_ID"] = drList["CountryIssue"];
                    this.dtRow["CountryIssue_Title"] = drList["CountryIssue_Title"] + "";
                    this.dtRow["StockExchange_Issue_ID"] = drList["StockExchange_Issue_ID"];
                    this.dtRow["StockExchange_Issue_Code"] = drList["StockExchange_Issue_Code"] + "";
                    this.dtRow["PrimaryShare"] = drList["PrimaryShare"];
                    this.dtRow["PrimaryShare_Title"] = ((Convert.ToInt32(drList["PrimaryShare"]) == 2)? "Yes": (Convert.ToInt32(drList["PrimaryShare"]) == 1? "No": ""));
                    this.dtRow["DistributionStatus"] = drList["DistributionStatus"];
                    this.dtRow["Date1"] = Convert.ToDateTime(drList["Date1"]).ToString("dd/MM/yyyy");
                    this.dtRow["Date2"] = Convert.ToDateTime(drList["Date2"]).ToString("dd/MM/yyyy");
                    this.dtRow["Date3"] = Convert.ToDateTime(drList["Date3"]).ToString("dd/MM/yyyy");
                    this.dtRow["Date4"] = Convert.ToDateTime(drList["Date4"]).ToString("dd/MM/yyyy");
                    this.dtRow["MonthDays"] = drList["MonthDays"];
                    this.dtRow["BaseDays"] = drList["BaseDays"];
                    this.dtRow["CouponeType"] = drList["CouponeType"];
                    this.dtRow["CouponeType_Title"] = drList["CouponeTypes_Title"] + "";
                    this.dtRow["Coupone"] = drList["Coupone"];
                    this.dtRow["LastCoupone"] = drList["LastCoupone"];
                    this.dtRow["Price"] = drList["Price"];
                    this.dtRow["FrequencyClipping"] = drList["FrequencyClipping"];
                    this.dtRow["RevocationRight"] = drList["RevocationRight"];
                    this.dtRow["RevocationRights_Title"] = drList["RevocationRights_Title"] + "";
                    this.dtRow["QuantityMin"] = drList["QuantityMin"];
                    this.dtRow["QuantityStep"] = drList["QuantityStep"];
                    this.dtRow["CoveredBond"] = drList["CoveredBond"];
                    this.dtRow["Rank"] = drList["Rank"];
                    this.dtRow["Rank_Title"] = drList["Ranks_Title"] + "";
                    this.dtRow["FloatingRate"] = drList["FloatingRate"];
                    this.dtRow["FRNFormula"] = drList["FRNFormula"];
                    this.dtRow["Limits"] = drList["Limits"];
                    this.dtRow["LastClosePrice"] = drList["LastClosePrice"];
                    this.dtRow["EntryPrice"] = drList["EntryPrice"] + "";
                    this.dtRow["TargetPrice"] = drList["TargetPrice"] + "";
                    this.dtRow["StopLoss"] = drList["StopLoss"] + "";
                    this.dtRow["DateIPO"] = drList["DateIPO"];
                    this.dtRow["HFIC_Recom"] = drList["HFIC_Recom"];
                    this.dtRow["HFIC_Recom_Title"] = (Convert.ToInt32(drList["HFIC_Recom"]) == 1? "Yes": "No");
                    this.dtRow["MIFID_Risk"] = drList["MIFID_Risk"];
                    this.dtRow["Weight"] = drList["Gravity"];
                    this.dtRow["Aktive"] = drList["Aktive"];
                    this._dtList.Rows.Add(dtRow);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList_ProductType()
        {
            _dtList = new DataTable("ProductsCodesList");
            dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));                         // ShareCodes.ID
            dtCol = _dtList.Columns.Add("Share_ID", System.Type.GetType("System.Int32"));                   // Shares.ID
            dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("DateFrom", System.Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("DateTo", System.Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("Title", System.Type.GetType("System.String"));                     // ShareCodes.Title
            dtCol = _dtList.Columns.Add("ISIN", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Code2", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Code3", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("SecID", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ProductCategories_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("HFCategory_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("StockExchange_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("StockExchange_Code", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("CountryAction_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("CountryAction_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("CountriesGroup_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("CountriesGroups_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("RiskCurr", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("CurrencyHedge", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("CurrencyHedge2", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("CountryIssue_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("CountryIssue_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("SectorTitle", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("InvestArea_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("PrimaryShare", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("PrimaryShare_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("DistributionStatus", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("LegalStructure_ID", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("Date1", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Date2", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Date3", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Date4", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("MonthDays", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("BaseDays", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("CouponeType", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("CouponeType_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Coupone", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("LastCoupone", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("Price", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("FrequencyClipping", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("RevocationRight", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("RevocationRights_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("QuantityMin", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("QuantityStep", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("CoveredBond", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("Ranks_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("FloatingRate", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("CreditRating", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("InvestType_Retail", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("InvestType_Prof", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("Distrib_ExecOnly", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("Distrib_Advice", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("Distrib_PortfolioManagment", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("ComplexProduct", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("FundCategoriesMorningStar_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("FundLegalStructures_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Benchmarks_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Leverage", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("ProviderName", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("FRNFormula", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Limits", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("LastClosePrice", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("EntryPrice", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("TargetPrice", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("StopLoss", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("DateIPO", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("HFIC_Recom", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("HFIC_Recom_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("MIFID_Risk", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Weight", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("InfoFlag", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("Aktive", System.Type.GetType("System.Int16"));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetProductsList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Product_ID", _iProduct_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = this._dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];
                    this.dtRow["Share_ID"] = drList["Share_ID"];
                    this.dtRow["Product_ID"] = drList["ShareType"];
                    this.dtRow["DateFrom"] = drList["DateFrom"];
                    this.dtRow["DateTo"] = drList["DateTo"];
                    this.dtRow["Title"] = drList["Title"] + "";
                    this.dtRow["ISIN"] = (drList["ISIN"] + "").Trim();
                    this.dtRow["Code"] = (drList["Code"] + "").Trim();
                    this.dtRow["Code2"] = (drList["Code2"] + "").Trim();
                    this.dtRow["Code3"] = (drList["Code3"] + "").Trim();
                    this.dtRow["SecID"] = (drList["SecID"] + "").Trim();
                    this.dtRow["ProductCategories_Title"] = (drList["ProductCategories_Title"] + "").Trim();
                    this.dtRow["HFCategory_Title"] = (drList["HFCategory_Title"] + "").Trim();
                    this.dtRow["StockExchange_ID"] = drList["StockExchange_ID"];
                    this.dtRow["StockExchange_Code"] = drList["StockExchange_Code"] + "";
                    this.dtRow["CountryAction_ID"] = ((drList["CountryAction_ID"] + "") != "") ? Convert.ToInt32(drList["CountryAction_ID"]) : 0;
                    this.dtRow["CountryAction_Title"] = drList["CountryAction_Title"] + "";
                    this.dtRow["CountriesGroup_ID"] = ((drList["CountriesGroup_ID"] + "") != "") ? Convert.ToInt32(drList["CountriesGroup_ID"]) : 0;
                    this.dtRow["CountriesGroups_Title"] = drList["CountriesGroups_Title"] + "";
                    this.dtRow["Currency"] = drList["Curr"] + "";
                    this.dtRow["RiskCurr"] = drList["RiskCurr"] + "";
                    this.dtRow["CurrencyHedge"] = drList["CurrencyHedge"];
                    this.dtRow["CurrencyHedge2"] = drList["CurrencyHedge2"] + "";
                    this.dtRow["CountryIssue_ID"] = drList["CountryIssue"];
                    this.dtRow["CountryIssue_Title"] = drList["CountryIssue_Title"] + "";
                    this.dtRow["SectorTitle"] = drList["SectorTitle"] + "";
                    this.dtRow["InvestArea_Title"] = drList["InvestArea_Title"] + "";
                    this.dtRow["PrimaryShare"] = drList["PrimaryShare"];
                    this.dtRow["PrimaryShare_Title"] = ((Convert.ToInt32(drList["PrimaryShare"]) == 2) ? "Yes" : (Convert.ToInt32(drList["PrimaryShare"]) == 1 ? "No" : ""));
                    this.dtRow["DistributionStatus"] = drList["DistributionStatus"];
                    this.dtRow["LegalStructure_ID"] = drList["LegalStructure_ID"];
                    this.dtRow["Date1"] = Convert.ToDateTime(drList["Date1"]).ToString("dd/MM/yyyy");
                    this.dtRow["Date2"] = Convert.ToDateTime(drList["Date2"]).ToString("dd/MM/yyyy");
                    this.dtRow["Date3"] = Convert.ToDateTime(drList["Date3"]).ToString("dd/MM/yyyy");
                    this.dtRow["Date4"] = Convert.ToDateTime(drList["Date4"]).ToString("dd/MM/yyyy");
                    this.dtRow["MonthDays"] = drList["MonthDays"];
                    this.dtRow["BaseDays"] = drList["BaseDays"];
                    this.dtRow["CouponeType"] = drList["CouponeType"];
                    this.dtRow["CouponeType_Title"] = drList["CouponeTypes_Title"] + "";
                    this.dtRow["Coupone"] = drList["Coupone"];
                    this.dtRow["LastCoupone"] = drList["LastCoupone"];
                    this.dtRow["Price"] = drList["Price"];
                    this.dtRow["FundCategoriesMorningStar_Title"] = drList["FundCategoriesMorningStar_Title"] + "";
                    this.dtRow["FundLegalStructures_Title"] = drList["FundLegalStructures_Title"] + "";
                    this.dtRow["Benchmarks_Title"] = drList["Benchmarks_Title"] + "";
                    this.dtRow["Leverage"] = drList["Leverage"];
                    this.dtRow["ProviderName"] = drList["ProviderName"] + "";
                    this.dtRow["FrequencyClipping"] = drList["FrequencyClipping"];
                    this.dtRow["RevocationRight"] = drList["RevocationRight"];
                    this.dtRow["RevocationRights_Title"] = drList["RevocationRights_Title"] + "";
                    this.dtRow["QuantityMin"] = drList["QuantityMin"];
                    this.dtRow["QuantityStep"] = drList["QuantityStep"];
                    this.dtRow["CoveredBond"] = drList["CoveredBond"];
                    this.dtRow["Ranks_Title"] = drList["Ranks_Title"] + "";
                    this.dtRow["FloatingRate"] = drList["FloatingRate"];
                    this.dtRow["CreditRating"] = drList["CreditRating"] + "";
                    this.dtRow["InvestType_Retail"] = drList["InvestType_Retail"];
                    this.dtRow["InvestType_Prof"] = drList["InvestType_Prof"];
                    this.dtRow["Distrib_ExecOnly"] = drList["Distrib_ExecOnly"];
                    this.dtRow["Distrib_Advice"] = drList["Distrib_Advice"];
                    this.dtRow["Distrib_PortfolioManagment"] = drList["Distrib_PortfolioManagment"];
                    this.dtRow["ComplexProduct"] = drList["ComplexProduct"];
                    this.dtRow["FRNFormula"] = drList["FRNFormula"];
                    this.dtRow["Limits"] = drList["Limits"];
                    this.dtRow["LastClosePrice"] = drList["LastClosePrice"];
                    this.dtRow["EntryPrice"] = drList["EntryPrice"] + "";
                    this.dtRow["TargetPrice"] = drList["TargetPrice"] + "";
                    this.dtRow["StopLoss"] = drList["StopLoss"] + "";
                    this.dtRow["DateIPO"] = drList["DateIPO"];
                    this.dtRow["HFIC_Recom"] = drList["HFIC_Recom"];
                    this.dtRow["HFIC_Recom_Title"] = (Convert.ToInt32(drList["HFIC_Recom"]) == 1 ? "Yes" : "No");
                    this.dtRow["MIFID_Risk"] = drList["MIFID_Risk"];
                    this.dtRow["Weight"] = drList["Gravity"];
                    this.dtRow["InfoFlag"] = drList["InfoFlag"];
                    this.dtRow["Aktive"] = drList["Aktive"];
                    this._dtList.Rows.Add(dtRow);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetCashList()
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
            dtCol = _dtList.Columns.Add("Code_ISIN", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("StockExchange_Code", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("StockExchange_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
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
            dtCol = _dtList.Columns.Add("Weight", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("URL_ID", System.Type.GetType("System.String"));

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
                    this.dtRow["Title"] = drList["Onoma"] +  "";
                    this.dtRow["Code"] = drList["ShareCode"] + "";
                    this.dtRow["Code2"] = drList["Code2"] + "";
                    this.dtRow["SecID"] = drList["SecID"] + "";
                    this.dtRow["ISIN"] = drList["ISIN"] + "";
                    this.dtRow["Code_ISIN"] = drList["Code_ISIN"] + "";
                    this.dtRow["Product"] = drList["Product_Title"] + "";
                    this.dtRow["ProductCategory"] = drList["ProductCategories_Title"] + "";
                    this.dtRow["StockExchange_Code"] = drList["StockExchange_Code"] + "";
                    this.dtRow["StockExchange_ID"] = drList["StockExchange_ID"];
                    this.dtRow["Currency"] = drList["Curr"] + "";
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
                    this.dtRow["InvestGeography_ID"] = drList["InvestGeography_ID"];
                    this.dtRow["LastClosePrice"] = drList["LastClosePrice"];
                    this.dtRow["EntryPrice"] = drList["EntryPrice"] + "";
                    this.dtRow["TargetPrice"] = drList["TargetPrice"] + "";
                    this.dtRow["StopLoss"] = drList["StopLoss"] + "";
                    this.dtRow["ComplexProduct"] = drList["ComplexProduct"];
                    this.dtRow["Weight"] = drList["Gravity"];
                    this.dtRow["URL_ID"] = drList["IR_URL"] + "";
                    this._dtList.Rows.Add(dtRow);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }      
        public int InsertRecord()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("InsertShareCode", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Share_ID", SqlDbType.Int).Value = this._iShare_ID;
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = this._dDateFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = this._dDateTo;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 100).Value = this._sCodeTitle.Trim();
                    cmd.Parameters.Add("@ISIN", SqlDbType.NVarChar, 50).Value = this._sISIN.Trim();
                    cmd.Parameters.Add("@SecID", SqlDbType.NVarChar, 50).Value = this._sSecID.Trim();
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 50).Value = this._sCode.Trim();
                    cmd.Parameters.Add("@Code2", SqlDbType.NVarChar, 50).Value = this._sCode2.Trim();
                    cmd.Parameters.Add("@Code3", SqlDbType.NVarChar, 50).Value = this._sCode3.Trim();
                    cmd.Parameters.Add("@CountryIssue", SqlDbType.Int).Value = this._iCountryIssue;
                    cmd.Parameters.Add("@StockExchange_Issue_ID", SqlDbType.Int).Value = this._iStockExchange_Issue_ID;
                    cmd.Parameters.Add("@StockExchange_ID", SqlDbType.Int).Value = this._iStockExchange_ID;
                    cmd.Parameters.Add("@Curr", SqlDbType.NVarChar, 6).Value = this._sCurr.Trim();
                    cmd.Parameters.Add("@PrimaryShare", SqlDbType.Int).Value = this._iPrimaryShare;
                    cmd.Parameters.Add("@CurrencyHedge", SqlDbType.Int).Value = this._iCurrencyHedge;
                    cmd.Parameters.Add("@CurrencyHedge2", SqlDbType.NVarChar, 6).Value = this._sCurrencyHedge2.Trim();
                    cmd.Parameters.Add("@DistributionStatus", SqlDbType.NVarChar, 10).Value = this._sDistributionStatus.Trim();
                    cmd.Parameters.Add("@Date1", SqlDbType.DateTime).Value = this._dDate1;
                    cmd.Parameters.Add("@Date2", SqlDbType.DateTime).Value = this._dDate2;
                    cmd.Parameters.Add("@Date3", SqlDbType.DateTime).Value = this._dDate3;
                    cmd.Parameters.Add("@Date4", SqlDbType.DateTime).Value = this._dDate4;
                    cmd.Parameters.Add("@MonthDays", SqlDbType.NVarChar, 20).Value = this._sMonthDays.Trim();
                    cmd.Parameters.Add("@BaseDays", SqlDbType.NVarChar, 20).Value = this._sBaseDays.Trim();
                    cmd.Parameters.Add("@CouponeType", SqlDbType.Int).Value = this._iCouponeType;
                    cmd.Parameters.Add("@Coupone", SqlDbType.Float).Value = this._fltCoupone;
                    cmd.Parameters.Add("@LastCoupone", SqlDbType.Float).Value = this._fltLastCoupone;
                    cmd.Parameters.Add("@Price", SqlDbType.Float).Value = this._fltPrice;
                    cmd.Parameters.Add("@FrequencyClipping", SqlDbType.Int).Value = this._iFrequencyClipping;
                    cmd.Parameters.Add("@RevocationRight", SqlDbType.Int).Value = this._iRevocationRight;
                    cmd.Parameters.Add("@QuantityMin", SqlDbType.Float).Value = this._fltQuantitryMin;
                    cmd.Parameters.Add("@QuantityStep", SqlDbType.Float).Value = this._fltQuantitryStep;
                    cmd.Parameters.Add("@CoveredBond", SqlDbType.Int).Value = this._iCoveredBond;
                    cmd.Parameters.Add("@FRNFormula", SqlDbType.NVarChar, 50).Value = this._sFRNFormula.Trim();
                    cmd.Parameters.Add("@FloatingRate", SqlDbType.NVarChar, 100).Value = this._sFloatingRate.Trim();
                    cmd.Parameters.Add("@Limits", SqlDbType.Float).Value = this._fltLimits;
                    cmd.Parameters.Add("@LastClosePrice", SqlDbType.Float).Value = this._fltLastClosePrice;
                    cmd.Parameters.Add("@EntryPrice", SqlDbType.NVarChar, 20).Value = this._sEntryPrice.Trim();
                    cmd.Parameters.Add("@TargetPrice", SqlDbType.NVarChar, 20).Value = this._sTargetPrice.Trim();
                    cmd.Parameters.Add("@StopLoss", SqlDbType.NVarChar, 20).Value = this._sStopLoss.Trim();
                    cmd.Parameters.Add("@Gravity", SqlDbType.Float).Value = this._fltGravity;
                    cmd.Parameters.Add("@HFIC_Recom", SqlDbType.Int).Value = this._iHFIC_Recom;
                    cmd.Parameters.Add("@MIFID_Risk", SqlDbType.NVarChar, 20).Value = this._sMIFID_Risk.Trim();
                    cmd.Parameters.Add("@DateIPO", SqlDbType.DateTime).Value = this._dDateIPO;
                    cmd.Parameters.Add("@Aktive", SqlDbType.Int).Value = this._iAktive;
                    cmd.Parameters.Add("@InfoFlag", SqlDbType.Int).Value = this._iInfoFlag;

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
                using (SqlCommand cmd = new SqlCommand("EditShareCode", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = this._iRecord_ID;
                    cmd.Parameters.Add("@Share_ID", SqlDbType.Int).Value = this._iShare_ID;
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = this._dDateFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = this._dDateTo;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 100).Value = this._sCodeTitle.Trim();
                    cmd.Parameters.Add("@ISIN", SqlDbType.NVarChar, 50).Value = this._sISIN.Trim();
                    cmd.Parameters.Add("@SecID", SqlDbType.NVarChar, 50).Value = this._sSecID.Trim();
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 50).Value = this._sCode.Trim();
                    cmd.Parameters.Add("@Code2", SqlDbType.NVarChar, 50).Value = this._sCode2.Trim();
                    cmd.Parameters.Add("@Code3", SqlDbType.NVarChar, 50).Value = this._sCode3.Trim();
                    cmd.Parameters.Add("@CountryIssue", SqlDbType.Int).Value = this._iCountryIssue;
                    cmd.Parameters.Add("@StockExchange_Issue_ID", SqlDbType.Int).Value = this._iStockExchange_Issue_ID;
                    cmd.Parameters.Add("@StockExchange_ID", SqlDbType.Int).Value = this._iStockExchange_ID;
                    cmd.Parameters.Add("@Curr", SqlDbType.NVarChar, 6).Value = this._sCurr.Trim();
                    cmd.Parameters.Add("@PrimaryShare", SqlDbType.Int).Value = this._iPrimaryShare;
                    cmd.Parameters.Add("@CurrencyHedge", SqlDbType.Int).Value = this._iCurrencyHedge;
                    cmd.Parameters.Add("@CurrencyHedge2", SqlDbType.NVarChar, 6).Value = this._sCurrencyHedge2.Trim();
                    cmd.Parameters.Add("@DistributionStatus", SqlDbType.NVarChar, 10).Value = this._sDistributionStatus.Trim();
                    cmd.Parameters.Add("@Date1", SqlDbType.DateTime).Value = this._dDate1;
                    cmd.Parameters.Add("@Date2", SqlDbType.DateTime).Value = this._dDate2;
                    cmd.Parameters.Add("@Date3", SqlDbType.DateTime).Value = this._dDate3;
                    cmd.Parameters.Add("@Date4", SqlDbType.DateTime).Value = this._dDate4;
                    cmd.Parameters.Add("@MonthDays", SqlDbType.NVarChar, 20).Value = this._sMonthDays.Trim();
                    cmd.Parameters.Add("@BaseDays", SqlDbType.NVarChar, 20).Value = this._sBaseDays.Trim();
                    cmd.Parameters.Add("@CouponeType", SqlDbType.Int).Value = this._iCouponeType;
                    cmd.Parameters.Add("@Coupone", SqlDbType.Float).Value = this._fltCoupone;
                    cmd.Parameters.Add("@LastCoupone", SqlDbType.Float).Value = this._fltLastCoupone;
                    cmd.Parameters.Add("@Price", SqlDbType.Float).Value = this._fltPrice;
                    cmd.Parameters.Add("@FrequencyClipping", SqlDbType.Int).Value = this._iFrequencyClipping;
                    cmd.Parameters.Add("@RevocationRight", SqlDbType.Int).Value = this._iRevocationRight;
                    cmd.Parameters.Add("@QuantityMin", SqlDbType.Float).Value = this._fltQuantitryMin;
                    cmd.Parameters.Add("@QuantityStep", SqlDbType.Float).Value = this._fltQuantitryStep;
                    cmd.Parameters.Add("@CoveredBond", SqlDbType.Int).Value = this._iCoveredBond;
                    cmd.Parameters.Add("@FRNFormula", SqlDbType.NVarChar, 50).Value = this._sFRNFormula.Trim();
                    cmd.Parameters.Add("@FloatingRate", SqlDbType.NVarChar, 100).Value = this._sFloatingRate.Trim();
                    cmd.Parameters.Add("@Limits", SqlDbType.Float).Value = this._fltLimits;
                    cmd.Parameters.Add("@LastClosePrice", SqlDbType.Float).Value = this._fltLastClosePrice;
                    cmd.Parameters.Add("@EntryPrice", SqlDbType.NVarChar, 20).Value = this._sEntryPrice.Trim();
                    cmd.Parameters.Add("@TargetPrice", SqlDbType.NVarChar, 20).Value = this._sTargetPrice.Trim();
                    cmd.Parameters.Add("@StopLoss", SqlDbType.NVarChar, 20).Value = this._sStopLoss.Trim();
                    cmd.Parameters.Add("@Gravity", SqlDbType.Float).Value = this._fltGravity;
                    cmd.Parameters.Add("@HFIC_Recom", SqlDbType.Int).Value = this._iHFIC_Recom;
                    cmd.Parameters.Add("@MIFID_Risk", SqlDbType.NVarChar, 20).Value = this._sMIFID_Risk.Trim();
                    cmd.Parameters.Add("@DateIPO", SqlDbType.DateTime).Value = this._dDateIPO;
                    cmd.Parameters.Add("@Aktive", SqlDbType.Int).Value = this._iAktive;
                    cmd.Parameters.Add("@InfoFlag", SqlDbType.Int).Value = this._iInfoFlag;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void EditRecord_ZeroInfoFlag()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("EditShareCode_ZeroInfoFlag", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Product_ID", SqlDbType.Int).Value = this._iProduct_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void EditRecord_Active()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_EditShareCode_Aktive", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = this._iRecord_ID;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@Aktive", SqlDbType.Int).Value = this._iAktive;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void EditRecord_LastClosePrice()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_EditShareCode_LastClosePrice", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = this._iRecord_ID;
                    cmd.Parameters.Add("@LastClosePrice", SqlDbType.Float).Value = this._fltLastClosePrice;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void EditRecord_Shares_ID()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_EditShareCodes_Share_ID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OldShare_ID", SqlDbType.Int).Value = this._iOldShare_ID;
                    cmd.Parameters.Add("@NewShare_ID", SqlDbType.Int).Value = this._iNewShare_ID;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Share_Codes";
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
        public DateTime DateFrom { get { return this._dDateFrom; } set { this._dDateFrom = value; } }
        public DateTime DateTo { get { return this._dDateTo; } set { this._dDateTo = value; } }
        public string CodeTitle { get { return this._sCodeTitle; } set { this._sCodeTitle = value; } }
        public string SecID { get { return this._sSecID; } set { this._sSecID = value; } }
        public string ISIN { get { return this._sISIN; } set { this._sISIN = value; } }
        public int StockExchange_ID { get { return this._iStockExchange_ID; } set { this._iStockExchange_ID = value; } }
        public string Code { get { return this._sCode; } set { this._sCode = value; } }
        public string Code2 { get { return this._sCode2; } set { this._sCode2 = value; } }
        public string Code3 { get { return this._sCode3; } set { this._sCode3 = value; } }
        public string Curr { get { return this._sCurr; } set { this._sCurr = value; } }
        public int PrimaryShare { get { return this._iPrimaryShare; } set { this._iPrimaryShare = value; } }
        public int CurrencyHedge { get { return this._iCurrencyHedge; } set { this._iCurrencyHedge = value; } }
        public string CurrencyHedge2 { get { return this._sCurrencyHedge2; } set { this._sCurrencyHedge2 = value; } }
        public string DistributionStatus { get { return this._sDistributionStatus; } set { this._sDistributionStatus = value; } }
        public int CountryIssue { get { return this._iCountryIssue; } set { this._iCountryIssue = value; } }
        public int StockExchange_Issue_ID { get { return this._iStockExchange_Issue_ID; } set { this._iStockExchange_Issue_ID = value; } }
        public int Aktive { get { return this._iAktive; } set { this._iAktive = value; } }
        public DateTime Date1  {  get { return this._dDate1; }   set { this._dDate1 = value; } }
        public DateTime Date2 { get { return this._dDate2; } set { this._dDate2 = value; } }
        public DateTime Date3 { get { return this._dDate3; } set { this._dDate3 = value; } }
        public DateTime Date4 { get { return this._dDate4; } set { this._dDate4 = value; } }
        public string MonthDays { get { return this._sMonthDays; } set { this._sMonthDays = value; } }
        public string BaseDays { get { return this._sBaseDays; } set { this._sBaseDays = value; } }
        public int CouponeType { get { return this._iCouponeType; } set { this._iCouponeType = value; } }
        public float Coupone { get { return this._fltCoupone; } set { this._fltCoupone = value; } }
        public float LastCoupone { get { return this._fltLastCoupone; } set { this._fltLastCoupone = value; } }
        public float Price { get { return this._fltPrice; } set { this._fltPrice = value; } }
        public int FrequencyClipping { get { return this._iFrequencyClipping; } set { this._iFrequencyClipping = value; } }
        public int RevocationRight { get { return this._iRevocationRight; } set { this._iRevocationRight = value; } }
        public float QuantityMin { get { return this._fltQuantitryMin; } set { this._fltQuantitryMin = value; } }
        public float QuantityStep { get { return this._fltQuantitryStep; } set { this._fltQuantitryStep = value; } }
        public int CoveredBond { get { return this._iCoveredBond; } set { this._iCoveredBond = value; } }
        public string FRNFormula { get { return this._sFRNFormula; } set { this._sFRNFormula = value; } }
        public string FloatingRate { get { return this._sFloatingRate; } set { this._sFloatingRate = value; } }
        public float Limits { get { return this._fltLimits; } set { this._fltLimits = value; } }
        public float LastClosePrice { get { return this._fltLastClosePrice; } set { this._fltLastClosePrice = value; } }
        public string EntryPrice { get { return this._sEntryPrice; } set { this._sEntryPrice = value; } }
        public string TargetPrice { get { return this._sTargetPrice; } set { this._sTargetPrice = value; } }
        public string StopLoss { get { return this._sStopLoss; } set { this._sStopLoss = value; } }
        public float Gravity { get { return this._fltGravity; } set { this._fltGravity = value; } }
        public int Product_ID { get { return this._iProduct_ID; } set { this._iProduct_ID = value; } }
        public string Product_Title { get { return this._sProduct_Title; } set { this._sProduct_Title = value; } }
        public int ProductCategory_ID { get { return this._iProductCategory_ID; } set { this._iProductCategory_ID = value; } }
        public string ProductCategory_Title { get { return this._sProductCategory_Title; } set { this._sProductCategory_Title = value; } }
        public int Product_Group { get { return this._iProduct_Group; } set { this._iProduct_Group = value; } }
        public string StockExchange_Code { get { return this._sStockExchange_Code; } set { this._sStockExchange_Code = value; } }
        public int HFIC_Recom { get { return this._iHFIC_Recom; } set { this._iHFIC_Recom = value; } }
        public string MIFID_Risk { get { return this._sMIFID_Risk; } set { this._sMIFID_Risk = value; } }
        public DateTime DateIPO { get { return this._dDateIPO; } set { this._dDateIPO = value; } }
        public int InfoFlag { get { return this._iInfoFlag; } set { this._iInfoFlag = value; } }
        public int BondType { get { return this._iBondType; } set { this._iBondType = value; } }
        public int CountryAction { get { return this._iCountryAction; } set { this._iCountryAction = value; } }
        public int CountryRisk_ID { get { return this._iCountryRisk_ID; } set { this._iCountryRisk_ID = value; } }
        public int CountryGroup_ID { get { return this._iCountryGroup_ID; } set { this._iCountryGroup_ID = value; } }
        public string HFCategory_Title { get { return this._sHFCategory_Title; } set { this._sHFCategory_Title = value; } }
        public string GlobalBroadCategory_Title { get { return this._sGlobalBroadCategory_Title; } set { this._sGlobalBroadCategory_Title = value; } }
        public int RatingGroup { get { return this._iRatingGroup; } set { this._iRatingGroup = value; } }
        public string CreditRating { get { return this._sCreditRating; } set { this._sCreditRating = value; } }
        public string MoodysRating { get { return this._sMoodysRating; } set { this._sMoodysRating = value; } }
        public string FitchsRating { get { return this._sFitchsRating; } set { this._sFitchsRating = value; } }
        public string SPRating { get { return this._sSPRating; } set { this._sSPRating = value; } }
        public float SurveyedKIID { get { return this._fltSurveyedKIID; } set { this._fltSurveyedKIID = value; } }
        public float Maturity { get { return this._fltMaturity; } set { this._fltMaturity = value; } }
        public float AmountOutstanding { get { return this._fltAmountOutstanding; } set { this._fltAmountOutstanding = value; } }
        public DateTime DateIns { get { return this._dDateIns; }  set { this._dDateIns = value; } }
        public DateTime RateDate { get { return this._dRateDate; } set { this._dRateDate = value; } }
        public int ShareTitles_ID { get { return this._iShareTitles_ID; } set { this._iShareTitles_ID = value; } }
        public int InvestType_Retail { get { return this._iInvestType_Retail; } set { this._iInvestType_Retail = value; } }
        public int InvestType_Prof { get { return this._iInvestType_Prof; } set { this._iInvestType_Prof = value; } }
        public int Distrib_ExecOnly { get { return this._iDistrib_ExecOnly; } set { this._iDistrib_ExecOnly = value; } }
        public int Distrib_Advice { get { return this._iDistrib_Advice; } set { this._iDistrib_Advice = value; } }
        public int Distrib_PortfolioManagment { get { return this._iDistrib_PortfolioManagment; } set { this._iDistrib_PortfolioManagment = value; } }
        public int ComplexProduct { get { return this._iComplexProduct; } set { this._iComplexProduct = value; } }
        public int Rank { get { return this._iRank; } set { this._iRank = value; } }
        public string Rank_Title { get { return this._sRank_Title; } set { this._sRank_Title = value; } }
        public int IsConvertible { get { return this._iIsConvertible; } set { this._iIsConvertible = value; } }
        public int IsDualCurrency { get { return this._iIsDualCurrency; } set { this._iIsDualCurrency = value; } }
        public int IsHybrid { get { return this._iIsHybrid; } set { this._iIsHybrid = value; } }
        public int IsGuaranteed { get { return this._iIsGuaranteed; } set { this._iIsGuaranteed = value; } }
        public int IsPerpetualSecurity { get { return this._iIsPerpetualSecurity; } set { this._iIsPerpetualSecurity = value; } }
        public int IsTotalLoss { get { return this._iIsTotalLoss; } set { this._iIsTotalLoss = value; } }
        public int IsCallable { get { return this._iIsCallable; } set { this._iIsCallable = value; } }
        public int IsPutable { get { return this._iIsPutable; } set { this._iIsPutable = value; } }
        public int Leverage { get { return this._iLeverage; } set { this._iLeverage = value; } }
        public int MiFIDInstrumentType { get { return this._iMiFIDInstrumentType; } set { this._iMiFIDInstrumentType = value; } }
        public int AIFMD { get { return this._iAIFMD; } set { this._iAIFMD = value; } }
        public int InvestGeography_ID { get { return this._iInvestGeography_ID; } set { this._iInvestGeography_ID = value; } }
        public string RiskCurr { get { return this._sRiskCurr; } set { this._sRiskCurr = value; } }
        public string Filter { get { return this._sFilter; } set { this._sFilter = value; } }
        public string ComplexAttribute { get { return this._sComplexAttribute; } set { this._sComplexAttribute = value; } }
        public string ComplexReasonsList { get { return this._sComplexReasonsList; } set { this._sComplexReasonsList = value; } }
        public int OldShare_ID { get { return this._iOldShare_ID; } set { this._iOldShare_ID = value; } }
        public int NewShare_ID { get { return this._iNewShare_ID; } set { this._iNewShare_ID = value; } }
        public int Status { get { return this._iStatus; } set { this._iStatus = value; } }
        public string Currency { get { return this._sCurrency; } set { this._sCurrency = value; } }
        public DataTable List  { get { return _dtList; } set { _dtList = value; } }

    }
}
