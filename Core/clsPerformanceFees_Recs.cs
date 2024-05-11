using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsPerformanceFees_Recs
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int _iRecord_ID;
        private int _iPT_ID;
        private int _iClient_ID;
        private DateTime _dFrom;
        private DateTime _dTo;
        private string _sCode;
        private string _sPortfolio;
        private string _sCurrency;
        private int _iContract_ID;
        private int _iContract_Details_ID;
        private int _iContract_Packages_ID;

        private DateTime _dStartPeriod;
        private DateTime _dEndPeriod;
        private int _iDays;
        private decimal _decBMV;
        private decimal _decEMV;     
        private decimal _decNetFlows;
        private decimal _decAverageInvestedCapital;
        private decimal _decHWM_StartPeriod;
        private decimal _decAdjustedAssetValue;
        private float _fltIndex_Y;
        private float _fltIndex_P;
        private float _fltAmoiviHF;
        private float _fltDiscount_Percent;
        private float _fltFinishAmoiviHF;
        private decimal _decPerformanceResult;
        private decimal _decPerformanceIndex;
        private decimal _decNetPerformance;
        private decimal _decHWM;
        private float _fltMWR;
        private decimal _decNetAmount;
        private float _fltVAT_Percent;
        private decimal _decVAT_Amount;
        private decimal _decFinishAmount;
        private decimal _decHWM_EndPeriod;
        private int _iInvoice_ID;
        private int _iInvoice_Type;
        private string _sInvoice_Num;
        private string _sInvoice_File;
        private DateTime _dDateFees;
        private string _sOfficialInformingDate;
        private int _iUser_ID;
        private int _iStatus;
        private DateTime _dEdit;

        private DateTime _dDateFrom;
        private DateTime _dDateTo;
        private DataTable _dtList;

        public clsPerformanceFees_Recs()
        {
            this._iRecord_ID = 0;
            this._iPT_ID = 0;
            this._iClient_ID = 0;
            this._dDateFrom = Convert.ToDateTime("1900/01/01");
            this._dDateTo = Convert.ToDateTime("1900/01/01");
            this._sCode = "";
            this._sPortfolio = "";
            this._sCurrency = "";
            this._iContract_ID = 0;
            this._iContract_Details_ID = 0;
            this._iContract_Packages_ID = 0;
            this._dStartPeriod = Convert.ToDateTime("1900/01/01");
            this._dEndPeriod = Convert.ToDateTime("1900/01/01");
            this._iDays = 0;
            this._decBMV = 0;
            this._decEMV = 0;
            this._decNetFlows = 0;
            this._decAverageInvestedCapital = 0;
            this._decHWM_StartPeriod = 0;
            this._decAdjustedAssetValue = 0;
            this._fltIndex_Y = 0;
            this._fltIndex_P = 0;
            this._fltAmoiviHF = 0;
            this._fltDiscount_Percent = 0;
            this._fltFinishAmoiviHF = 0;
            this._decPerformanceResult = 0;
            this._decPerformanceIndex = 0;
            this._decNetPerformance = 0;
            this._decHWM = 0;
            this._fltMWR = 0;
            this._decNetAmount = 0;
            this._fltVAT_Percent = 0;
            this._decVAT_Amount = 0;
            this._decFinishAmount = 0;
            this._decHWM_EndPeriod = 0;

            this._iInvoice_ID = 0;
            this._iInvoice_Type = 0;
            this._sInvoice_Num = "";
            this._sInvoice_File = "";
            this._dDateFees = Convert.ToDateTime("1900/01/01");
            this._sOfficialInformingDate = "";
            this._iStatus = 0;
            this._iUser_ID = 0;
            this._dEdit = Convert.ToDateTime("1900/01/01");

            this._dDateFrom = Convert.ToDateTime("1900/01/01");
            this._dDateTo = Convert.ToDateTime("1900/01/01");
        }

        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "PerformanceFees_Recs"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = drList.GetInt32(0); //Convert.ToInt32(drList["ID"]);
                    this._iPT_ID = Convert.ToInt32(drList["PT_ID"]);
                    this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                    this._dDateFrom = Convert.ToDateTime(drList["DateFrom"]);
                    this._dDateTo = Convert.ToDateTime(drList["DateTo"]);
                    this._sCode = drList["Code"] + "";
                    this._sPortfolio = drList["Portfolio"] + "";
                    this._sCurrency = drList["Currency"] + "";
                    this._iContract_ID = Convert.ToInt32(drList["Contract_ID"]);
                    this._iContract_Details_ID = Convert.ToInt32(drList["Contract_Details_ID"]);
                    this._iContract_Packages_ID = Convert.ToInt32(drList["Contract_Packages_ID"]);
                    this._dStartPeriod = Convert.ToDateTime(drList["StartPeriod"]);
                    this._dEndPeriod = Convert.ToDateTime(drList["EndPeriod"]);
                    this._iDays = Convert.ToInt32(drList["Days"]);
                    this._decBMV = Convert.ToDecimal(drList["BMV"]);
                    this._decEMV = Convert.ToDecimal(drList["EMV"]);
                    this._decNetFlows = Convert.ToDecimal(drList["NetFlows"]);
                    this._decAverageInvestedCapital = Convert.ToDecimal(drList["AverageInvestedCapital"]);
                    this._decHWM_StartPeriod = Convert.ToDecimal(drList["HWM_StartPeriod"]);
                    this._decAdjustedAssetValue = Convert.ToDecimal(drList["AdjustedAssetValue"]);
                    this._fltIndex_Y = Convert.ToSingle(drList["Index_Y"]);
                    this._fltIndex_P = Convert.ToSingle(drList["Index_P"]);
                    this._fltAmoiviHF = Convert.ToSingle(drList["AmoiviHF"]);
                    this._fltDiscount_Percent = Convert.ToSingle(drList["Discount_Percent"]);
                    this._fltFinishAmoiviHF = Convert.ToSingle(drList["FinishAmoiviHF"]);
                    this._decPerformanceResult = Convert.ToDecimal(drList["PerformanceResult"]);
                    this._decPerformanceIndex = Convert.ToDecimal(drList["PerformanceIndex"]);
                    this._decNetPerformance = Convert.ToDecimal(drList["NetPerformance"]);
                    this._decHWM = Convert.ToDecimal(drList["HWM"]);
                    this._fltMWR = Convert.ToSingle(drList["MWR"]);
                    this._decNetAmount = Convert.ToDecimal(drList["NetAmount"]);
                    this._fltVAT_Percent = Convert.ToSingle(drList["VAT_Percent"]);
                    this._decVAT_Amount = Convert.ToDecimal(drList["VAT_Amount"]);
                    this._decFinishAmount = Convert.ToDecimal(drList["FinishAmount"]);
                    this._decHWM_EndPeriod = Convert.ToDecimal(drList["HWM_EndPeriod"]);
                    this._iStatus = Convert.ToInt32(drList["Status"]);
                    this._iInvoice_ID = Convert.ToInt32(drList["Invoice_ID"]);
                    this._sInvoice_Num = drList["Invoice_Num"] + "";
                    this._iInvoice_Type = Convert.ToInt32(drList["Invoice_Type"]);                              // 0 - new record, 1- APY, 2 -TPY, 3 - new record not MF, 4 - pistotiko, 5 - akyrotiko 
                    this._sInvoice_File = drList["Invoice_File"] + "";
                    this._dDateFees = Convert.ToDateTime(drList["DateFees"]);
                    this._sOfficialInformingDate = drList["OfficialInformingDate"] + "";
                    this._iStatus = Convert.ToInt32(drList["Status"]);
                    this._iUser_ID = Convert.ToInt32(drList["User_ID"]);
                    this._dEdit = Convert.ToDateTime(drList["DateEdit"]);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            try
            {
                _dtList = new DataTable("PerformanceFees_List");
                dtCol = _dtList.Columns.Add("ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("PT_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ImageType", Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("DateFrom", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("DateTo", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("ClientType", Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Client_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ContractTipos", Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("ContractTitle", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Package_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Package_Title", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Code", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Portfolio", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Currency", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Contract_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_Details_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_Packages_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("StartPeriod", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("EndPEriod", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("Days", Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("BMV", Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("EMV", Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("NetFlows", Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("AverageInvestedCapital", Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("HWM_StartPeriod", Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("AdjustedAssetValue", Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("Index_Y", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Index_P", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AmoiviHF", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Discount_Percent", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishAmoiviHF", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("PerformanceResult", Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("PerformanceIndex", Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("NetPerformance", Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("HWM", Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("MWR", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("NetAmount", Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("VAT_Percent", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("VAT_Amount", Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("FinishAmount", Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("HWM_EndPeriod", Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("InvestmentProfile", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("User1_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("User1_Name", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("BornPlace", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Advisory_Name", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RM_Name", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Introducer_Name", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Diaxiristis_Name", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("EMail", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DOY", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AFM", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Address", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("City", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Zip", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Country_Title", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CountryEnglish", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Invoice_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Invoice_Type", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Invoice_Num", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Invoice_File", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DateFees", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("OfficialInformingDate", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ConnectionMethod", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Service_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Service_Title", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("MIFID_2", Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Status", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("User_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Author_Name", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DateEdit", Type.GetType("System.String"));
                
                conn.Open();
                cmd = new SqlCommand("GetPerformanceFees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PT_ID", _iPT_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["PT_ID"] = drList["PT_ID"];
                    dtRow["ImageType"] = (drList["Invoice_File"] + "" == "") ? 0 : 1;
                    dtRow["DateFrom"] = drList["DateFrom"];
                    dtRow["DateTo"] = drList["DateTo"];
                    dtRow["ClientType"] = drList["ClientTipos"];
                    dtRow["Client_ID"] = drList["Client_ID"];
                    dtRow["ContractTipos"] = drList["ContractTipos"];
                    dtRow["ContractTitle"] = drList["ContractTitle"] + "";
                    dtRow["Package_ID"] = drList["Package_ID"];
                    dtRow["Package_Title"] = drList["Package_Title"] + "";
                    dtRow["Code"] = drList["Code"] + "";
                    dtRow["Portfolio"] = drList["Portfolio"] + "";
                    dtRow["Currency"] = drList["Currency"] + "";
                    dtRow["Contract_ID"] = drList["Contract_ID"];
                    dtRow["Contract_Details_ID"] = drList["Contracts_Details_ID"];
                    dtRow["Contract_Packages_ID"] = drList["Contract_Packages_ID"];
                    dtRow["StartPeriod"] = drList["StartPeriod"];   
                    dtRow["EndPeriod"] = drList["EndPeriod"];        
                    dtRow["Days"] = drList["Days"];
                    dtRow["BMV"] = drList["BMV"];
                    dtRow["EMV"] = drList["EMV"];
                    dtRow["NetFlows"] = drList["NetFlows"];
                    dtRow["AverageInvestedCapital"] = drList["AverageInvestedCapital"];
                    dtRow["HWM_StartPeriod"] = drList["HWM_StartPeriod"];
                    dtRow["AdjustedAssetValue"] = drList["AdjustedAssetValue"];
                    dtRow["Index_Y"] = drList["Index_Y"];
                    dtRow["Index_P"] = drList["Index_P"];
                    dtRow["AmoiviHF"] = drList["AmoiviHF"];
                    dtRow["Discount_Percent"] = drList["Discount_Percent"];
                    dtRow["FinishAmoiviHF"] = drList["FinishAmoiviHF"];
                    dtRow["PerformanceResult"] = drList["PerformanceResult"];
                    dtRow["PerformanceIndex"] = drList["PerformanceIndex"];
                    dtRow["NetPerformance"] = drList["NetPerformance"];
                    dtRow["HWM"] = drList["HWM"];
                    dtRow["MWR"] = drList["MWR"];
                    dtRow["NetAmount"] = drList["NetAmount"];
                    dtRow["VAT_Percent"] = drList["VAT_Percent"];
                    dtRow["VAT_Amount"] = drList["VAT_Amount"];
                    dtRow["FinishAmount"] = drList["FinishAmount"];
                    dtRow["HWM_EndPeriod"] = drList["HWM_EndPeriod"];
                    dtRow["InvestmentProfile"] = drList["Profile_Title"] + "";
                    dtRow["User1_ID"] = drList["User1_ID"];
                    dtRow["Advisory_Name"] = (drList["Advisor_Surname"] + " " + drList["Advisor_Firstname"]).Trim();
                    dtRow["RM_Name"] = (drList["RM_Surname"] + " " + drList["RM_Firstname"]).Trim();
                    dtRow["Introducer_Name"] = (drList["Introducer_Surname"] + " " + drList["Introducer_Firstname"]).Trim();
                    dtRow["Diaxiristis_Name"] = (drList["Diaxiristis_Surname"] + " " + drList["Diaxiristis_Firstname"]).Trim();
                    dtRow["User1_Name"] = drList["InvName"] + "";
                    dtRow["BornPlace"] = drList["BornPlace"] + "";
                    dtRow["Email"] = drList["Email"];
                    dtRow["DOY"] = drList["DOY"];
                    dtRow["AFM"] = drList["AFM"];
                    dtRow["Address"] = drList["Address"] + "";
                    dtRow["City"] = drList["City"] + "";
                    dtRow["Zip"] = drList["ZIP"] + "";
                    dtRow["Country_Title"] = drList["Country_Title"] + "";
                    dtRow["CountryEnglish"] = drList["Country_TitleEn"] + "";
                    dtRow["Invoice_ID"] = drList["Invoice_ID"];
                    dtRow["Invoice_Type"] = drList["Invoice_Type"];
                    if (Convert.ToInt32(dtRow["Invoice_Type"]) == 0) {
                        if (Convert.ToInt32(drList["ClientTipos"]) == 1) dtRow["Invoice_Type"] = 1;
                        else dtRow["Invoice_Type"] = 2;
                    }
                    dtRow["Invoice_Num"] = drList["Invoice_Num"] + "";
                    dtRow["Invoice_File"] = drList["Invoice_File"] + "";
                    dtRow["DateFees"] = "";
                    if (Convert.ToDateTime(drList["DateFees"]) != Convert.ToDateTime("1900/01/01")) dtRow["DateFees"] = drList["DateFees"];
                    dtRow["OfficialInformingDate"] = drList["OfficialInformingDate"] + "";
                    dtRow["ConnectionMethod"] = drList["ConnectionMethod"];
                    dtRow["Service_ID"] = drList["PackageType_ID"];
                    dtRow["Service_Title"] = drList["Service_Title"] + "";
                    dtRow["MIFID_2"] = drList["MIFID_2"];
                    dtRow["Status"] = drList["Status"];
                    dtRow["User_ID"] = drList["User_ID"];
                    dtRow["Author_Name"] = (drList["Author_Surname"] + " " + drList["Author_Firstname"]).Trim();
                    dtRow["DateEdit"] = Convert.ToDateTime(drList["DateEdit"]).ToString("dd/MM/yyyy");

                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }  
        public int InsertRecord()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("InsertPerformanceFees_Rec", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PT_ID", SqlDbType.Int).Value = _iPT_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = _dDateFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = _dDateTo;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 30).Value = _sCode;
                    cmd.Parameters.Add("@Portfolio", SqlDbType.NVarChar, 20).Value = _sPortfolio;
                    cmd.Parameters.Add("@Currency", SqlDbType.NVarChar, 6).Value = _sCurrency;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Details_ID", SqlDbType.Int).Value = _iContract_Details_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@StartPeriod", SqlDbType.DateTime).Value = _dStartPeriod;
                    cmd.Parameters.Add("@EndPeriod", SqlDbType.DateTime).Value = _dEndPeriod;
                    cmd.Parameters.Add("@Days", SqlDbType.Int).Value = _iDays;
                    cmd.Parameters.Add("@BMV", SqlDbType.Float).Value = _decBMV;
                    cmd.Parameters.Add("@EMV", SqlDbType.Float).Value = _decEMV;
                    cmd.Parameters.Add("@NetFlows", SqlDbType.Float).Value = _decNetFlows;
                    cmd.Parameters.Add("@AverageInvestedCapital", SqlDbType.Float).Value = _decAverageInvestedCapital;
                    cmd.Parameters.Add("@HWM_StartPeriod", SqlDbType.Float).Value = _decHWM_StartPeriod;
                    cmd.Parameters.Add("@AdjustedAssetValue", SqlDbType.Float).Value = _decAdjustedAssetValue;
                    cmd.Parameters.Add("@Index_Y", SqlDbType.Float).Value = _fltIndex_Y;
                    cmd.Parameters.Add("@Index_P", SqlDbType.Float).Value = _fltIndex_P;
                    cmd.Parameters.Add("@AmoiviHF", SqlDbType.Float).Value = _fltAmoiviHF;
                    cmd.Parameters.Add("@Discount_Percent", SqlDbType.Float).Value = _fltDiscount_Percent;
                    cmd.Parameters.Add("@FinishAmoiviHF", SqlDbType.Float).Value = _fltFinishAmoiviHF;
                    cmd.Parameters.Add("@PerformanceResult", SqlDbType.Float).Value = _decPerformanceResult;
                    cmd.Parameters.Add("@PerformanceIndex", SqlDbType.Decimal).Value = _decPerformanceIndex;
                    cmd.Parameters.Add("@NetPerformance", SqlDbType.Decimal).Value = _decNetPerformance;
                    cmd.Parameters.Add("@HWM", SqlDbType.Float).Value = _decHWM;
                    cmd.Parameters.Add("@MWR", SqlDbType.Float).Value = _fltMWR;
                    cmd.Parameters.Add("@NetAmount", SqlDbType.Float).Value = _decNetAmount;
                    cmd.Parameters.Add("@VAT_Percent", SqlDbType.Float).Value = _fltVAT_Percent;
                    cmd.Parameters.Add("@VAT_Amount", SqlDbType.Decimal).Value = _decVAT_Amount;
                    cmd.Parameters.Add("@FinishAmount", SqlDbType.Decimal).Value = _decFinishAmount;
                    cmd.Parameters.Add("@HWM_EndPeriod", SqlDbType.Decimal).Value = _decHWM_EndPeriod;
                    cmd.Parameters.Add("@Invoice_ID", SqlDbType.Int).Value = _iInvoice_ID;
                    cmd.Parameters.Add("@Invoice_Type", SqlDbType.Int).Value = _iInvoice_Type;
                    cmd.Parameters.Add("@Invoice_Num", SqlDbType.NVarChar, 30).Value = _sInvoice_Num;
                    cmd.Parameters.Add("@Invoice_File", SqlDbType.NVarChar, 50).Value = _sInvoice_File;
                    cmd.Parameters.Add("@DateFees", SqlDbType.DateTime).Value = _dDateFees;
                    cmd.Parameters.Add("@OfficialInformingDate", SqlDbType.NVarChar, 20).Value = _sOfficialInformingDate;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;                                                  // 1 - Active, 2 - Cancelled
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = _iUser_ID;
                    cmd.Parameters.Add("@DateEdit", SqlDbType.DateTime).Value = _dEdit;

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
                using (SqlCommand cmd = new SqlCommand("EditPerformanceFees_Rec", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = _dDateFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = _dDateTo;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 30).Value = _sCode;
                    cmd.Parameters.Add("@Portfolio", SqlDbType.NVarChar, 20).Value = _sPortfolio;
                    cmd.Parameters.Add("@Currency", SqlDbType.NVarChar, 6).Value = _sCurrency;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Details_ID", SqlDbType.Int).Value = _iContract_Details_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@StartPeriod", SqlDbType.DateTime).Value = _dStartPeriod;
                    cmd.Parameters.Add("@EndPeriod", SqlDbType.DateTime).Value = _dEndPeriod;
                    cmd.Parameters.Add("@Days", SqlDbType.Int).Value = _iDays;
                    cmd.Parameters.Add("@BMV", SqlDbType.Float).Value = _decBMV;
                    cmd.Parameters.Add("@EMV", SqlDbType.Float).Value = _decEMV;
                    cmd.Parameters.Add("@NetFlows", SqlDbType.Float).Value = _decNetFlows;
                    cmd.Parameters.Add("@AverageInvestedCapital", SqlDbType.Float).Value = _decAverageInvestedCapital;
                    cmd.Parameters.Add("@HWM_StartPeriod", SqlDbType.Float).Value = _decHWM_StartPeriod;
                    cmd.Parameters.Add("@AdjustedAssetValue", SqlDbType.Float).Value = _decAdjustedAssetValue;
                    cmd.Parameters.Add("@Index_Y", SqlDbType.Float).Value = _fltIndex_Y;
                    cmd.Parameters.Add("@Index_P", SqlDbType.Float).Value = _fltIndex_P;
                    cmd.Parameters.Add("@AmoiviHF", SqlDbType.Float).Value = _fltAmoiviHF;
                    cmd.Parameters.Add("@Discount_Percent", SqlDbType.Float).Value = _fltDiscount_Percent;
                    cmd.Parameters.Add("@FinishAmoiviHF", SqlDbType.Float).Value = _fltFinishAmoiviHF;
                    cmd.Parameters.Add("@PerformanceResult", SqlDbType.Float).Value = _decPerformanceResult;
                    cmd.Parameters.Add("@PerformanceIndex", SqlDbType.Decimal).Value = _decPerformanceIndex;
                    cmd.Parameters.Add("@NetPerformance", SqlDbType.Decimal).Value = _decNetPerformance;
                    cmd.Parameters.Add("@HWM", SqlDbType.Float).Value = _decHWM;
                    cmd.Parameters.Add("@MWR", SqlDbType.Float).Value = _fltMWR;
                    cmd.Parameters.Add("@NetAmount", SqlDbType.Float).Value = _decNetAmount;
                    cmd.Parameters.Add("@VAT_Percent", SqlDbType.Float).Value = _fltVAT_Percent;
                    cmd.Parameters.Add("@VAT_Amount", SqlDbType.Decimal).Value = _decVAT_Amount;
                    cmd.Parameters.Add("@FinishAmount", SqlDbType.Decimal).Value = _decFinishAmount;
                    cmd.Parameters.Add("@HWM_EndPeriod", SqlDbType.Decimal).Value = _decHWM_EndPeriod;
                    cmd.Parameters.Add("@Invoice_ID", SqlDbType.Int).Value = _iInvoice_ID;
                    cmd.Parameters.Add("@Invoice_Type", SqlDbType.Int).Value = _iInvoice_Type;
                    cmd.Parameters.Add("@Invoice_Num", SqlDbType.NVarChar, 30).Value = _sInvoice_Num;
                    cmd.Parameters.Add("@Invoice_File", SqlDbType.NVarChar, 50).Value = _sInvoice_File;
                    cmd.Parameters.Add("@DateFees", SqlDbType.DateTime).Value = _dDateFees;
                    cmd.Parameters.Add("@OfficialInformingDate", SqlDbType.NVarChar, 20).Value = _sOfficialInformingDate;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;                                                  // 1 - Active, 2 - Cancelled
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = _iUser_ID;
                    cmd.Parameters.Add("@DateEdit", SqlDbType.DateTime).Value = _dEdit;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "PerformanceFees_Recs";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int PT_ID { get { return this._iPT_ID; } set { this._iPT_ID = value; } }
        public int Client_ID { get { return this._iClient_ID; } set { this._iClient_ID = value; } }
        public DateTime DateFrom { get { return this._dDateFrom; } set { this._dDateFrom = value; } }
        public DateTime DateTo { get { return this._dDateTo; } set { this._dDateTo = value; } }
        public string Code { get { return this._sCode; } set { this._sCode = value; } }
        public string Portfolio { get { return this._sPortfolio; } set { this._sPortfolio = value; } }
        public string Currency { get { return this._sCurrency; } set { this._sCurrency = value; } }
        public int Contract_ID { get { return this._iContract_ID; } set { this._iContract_ID = value; } }
        public int Contract_Details_ID { get { return this._iContract_Details_ID; } set { this._iContract_Details_ID = value; } }
        public int Contract_Packages_ID { get { return this._iContract_Packages_ID; } set { this._iContract_Packages_ID = value; } }
        public DateTime StartPeriod { get { return this._dStartPeriod; } set { this._dStartPeriod = value; } }
        public DateTime EndPeriod { get { return this._dEndPeriod; } set { this._dEndPeriod = value; } }
        public int Days { get { return this._iDays; } set { this._iDays = value; } }
        public decimal BMV { get { return this._decBMV; } set { this._decBMV = value; } }
        public decimal EMV { get { return this._decEMV; } set { this._decEMV = value; } }
        public decimal NetFlows { get { return this._decNetFlows; } set { this._decNetFlows = value; } }
        public decimal AverageInvestedCapital { get { return this._decAverageInvestedCapital; } set { this._decAverageInvestedCapital = value; } }
        public decimal HWM_StartPeriod { get { return this._decHWM_StartPeriod; } set { this._decHWM_StartPeriod = value; } }
        public decimal AdjustedAssetValue { get { return this._decAdjustedAssetValue; } set { this._decAdjustedAssetValue = value; } }
        public float Index_Y { get { return this._fltIndex_Y; } set { this._fltIndex_Y = value; } }
        public float Index_P { get { return this._fltIndex_P; } set { this._fltIndex_P = value; } }
        public float AmoiviHF { get { return this._fltAmoiviHF; } set { this._fltAmoiviHF = value; } }
        public float Discount_Percent { get { return this._fltDiscount_Percent; } set { this._fltDiscount_Percent = value; } }
        public float FinishAmoiviHF { get { return this._fltFinishAmoiviHF; } set { this._fltFinishAmoiviHF = value; } }
        public decimal PerformanceResult { get { return this._decPerformanceResult; } set { this._decPerformanceResult = value; } }
        public decimal PerformanceIndex { get { return this._decPerformanceIndex; } set { this._decPerformanceIndex = value; } }
        public decimal NetPerformance { get { return this._decNetPerformance; } set { this._decNetPerformance = value; } }
        public decimal HWM { get { return this._decHWM; } set { this._decHWM = value; } }
        public float MWR { get { return this._fltMWR; } set { this._fltMWR = value; } }
        public decimal NetAmount { get { return this._decNetAmount; } set { this._decNetAmount = value; } }
        public float VAT_Percent { get { return this._fltVAT_Percent; } set { this._fltVAT_Percent = value; } }
        public decimal VAT_Amount { get { return this._decVAT_Amount; } set { this._decVAT_Amount = value; } }
        public decimal FinishAmount { get { return this._decFinishAmount; } set { this._decFinishAmount = value; } }
        public decimal HWM_EndPeriod { get { return this._decHWM_EndPeriod; } set { this._decHWM_EndPeriod = value; } }
        public int Invoice_ID { get { return this._iInvoice_ID; } set { this._iInvoice_ID = value; } }
        public int Invoice_Type { get { return this._iInvoice_Type; } set { this._iInvoice_Type = value; } }
        public string Invoice_Num { get { return this._sInvoice_Num; } set { this._sInvoice_Num = value; } }
        public string Invoice_File { get { return this._sInvoice_File; } set { this._sInvoice_File = value; } }
        public DateTime DateFees { get { return this._dDateFees; } set { this._dDateFees = value; } }
        public string OfficialInformingDate { get { return this._sOfficialInformingDate; } set { this._sOfficialInformingDate = value; } }
        public int Status { get { return this._iStatus; } set { this._iStatus = value; } }
        public int User_ID { get { return this._iUser_ID; } set { this._iUser_ID = value; } }
        public DateTime DateEdit { get { return this._dEdit; } set { this._dEdit = value; } }

        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}