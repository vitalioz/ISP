using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsAdminFees_Recs
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int      _iRecord_ID;
        private int      _iAT_ID;
        private int      _iClient_ID;
        private DateTime _dFrom;
        private DateTime _dTo;
        private string   _sCode;
        private string   _sPortfolio;
        private string   _sCurrency;
        private int      _iContract_ID;
        private int      _iContract_Details_ID;
        private int      _iContract_Packages_ID;
        private decimal  _decAUM;
        private int      _iDays;
        private float    _fltAmoiviPro;
        private float    _fltAxiaPro;
        private float    _fltAmoiviAfter;
        private float    _fltAxiaAfter;
        private float    _fltDiscount_Percent1;
        private float    _fltDiscount_Amount1;
        private float    _fltDiscount_Percent2;
        private float    _fltDiscount_Amount2;
        private float    _fltDiscount_Percent;
        private float    _fltDiscount_Amount;
        private float    _fltMinAmoivi;
        private float    _fltMinAmoivi_Percent;
        private float    _fltMinAmoivi_Percent2;
        private decimal  _decFinishMinAmoivi;
        private decimal  _decLastAmount;
        private float    _fltLastAmount_Percent;
        private float    _fltVAT_Percent;
        private float    _fltVAT_Amount;
        private decimal   _decFinishAmount;
        private int      _iMaxDays;
        private float    _fltAverageAUM;
        private float    _fltWeights;
        private decimal  _decMinYearly;
        private int      _iService_ID;
        private int      _iInvoice_ID;
        private DateTime _dFees;
        private string   _sOfficialInformingDate;
        private int      _iTipos;
        private int      _iStatus;
        private int      _iUser_ID;
        private DateTime _dEdit;

        private DataTable _dtList;

        public clsAdminFees_Recs()
        {
            this._iRecord_ID = 0;
            this._iAT_ID = 0;
            this._iClient_ID = 0;
            this._dFrom = Convert.ToDateTime("1900/01/01");
            this._dTo = Convert.ToDateTime("1900/01/01");
            this._sCode = "";
            this._sPortfolio = "";
            this._sCurrency = "";
            this._iContract_ID = 0;
            this._iContract_Details_ID = 0;
            this._iContract_Packages_ID = 0;
            this._decAUM = 0;
            this._iDays = 0;
            this._fltAmoiviPro = 0;
            this._fltAxiaPro = 0;
            this._fltAmoiviAfter = 0;
            this._fltAxiaAfter = 0;
            this._fltDiscount_Percent1 = 0;
            this._fltDiscount_Amount1 = 0;
            this._fltDiscount_Percent2 = 0;
            this._fltDiscount_Amount2 = 0;
            this._fltDiscount_Percent = 0;
            this._fltDiscount_Amount = 0;
            this._fltMinAmoivi = 0;
            this._fltMinAmoivi_Percent = 0;
            this._fltMinAmoivi_Percent2 = 0;
            this._decFinishMinAmoivi = 0;
            this._decLastAmount = 0;
            this._fltLastAmount_Percent = 0;
            this._fltVAT_Percent = 0;
            this._fltVAT_Amount = 0;
            this._decFinishAmount = 0;
            this._iMaxDays = 0;
            this._fltAverageAUM = 0;
            this._fltWeights = 0;
            this._decMinYearly = 0;
            this._iService_ID = 0;
            this._iInvoice_ID = 0;
            this._dFees = Convert.ToDateTime("1900/01/01");
            this._sOfficialInformingDate = "";
            this._iTipos = 0;
            this._iStatus = 0;
            this._iUser_ID = 0;
            this._dEdit = Convert.ToDateTime("1900/01/01");
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "AdminFees_Recs"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iAT_ID = Convert.ToInt32(drList["AT_ID"]);
                    this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                    this._dFrom = Convert.ToDateTime(drList["DateFrom"]);
                    this._dTo = Convert.ToDateTime(drList["DateTo"]);
                    this._sCode = drList["Code"] + "";
                    this._sPortfolio = drList["Portfolio"] + "";
                    this._sCurrency = drList["Currency"] + "";
                    this._iContract_ID = Convert.ToInt32(drList["Contract_ID"]);
                    this._iContract_Details_ID = Convert.ToInt32(drList["Contract_Details_ID"]);
                    this._iContract_Packages_ID = Convert.ToInt32(drList["Contract_Packages_ID"]);
                    this._decAUM = Convert.ToDecimal(drList["AUM"]);
                    this._iDays = Convert.ToInt32(drList["Days"]);
                    this._fltAmoiviPro = Convert.ToSingle(drList["AmoiviPro"]);
                    this._fltAxiaPro = Convert.ToSingle(drList["AxiaPro"]);
                    this._fltAmoiviAfter = Convert.ToSingle(drList["AmoiviAfter"]);
                    this._fltAxiaAfter = Convert.ToSingle(drList["AxiaAfter"]);
                    this._fltDiscount_Percent1 = Convert.ToSingle(drList["Discount_Percent1"]);
                    this._fltDiscount_Amount1 = Convert.ToSingle(drList["Discount_Amount1"]);
                    this._fltDiscount_Percent2 = Convert.ToSingle(drList["Discount_Percent2"]);
                    this._fltDiscount_Amount2 = Convert.ToSingle(drList["Discount_Amount2"]);
                    this._fltDiscount_Percent = Convert.ToSingle(drList["Discount_Percent"]);
                    this._fltDiscount_Amount = Convert.ToSingle(drList["Discount_Amount"]);
                    this._fltMinAmoivi = Convert.ToSingle(drList["MinAmoivi"]);
                    this._fltMinAmoivi_Percent = Convert.ToSingle(drList["MinAmoivi_Percent"]);
                    this._fltMinAmoivi_Percent2 = Convert.ToSingle(drList["MinAmoivi_Percent2"]);
                    this._decFinishMinAmoivi = Convert.ToDecimal(drList["FinishMinAmoivi"]);
                    this._decLastAmount = Convert.ToDecimal(drList["LastAmount"]);
                    this._fltLastAmount_Percent = Convert.ToSingle(drList["LastAmount_Percent"]);
                    this._fltVAT_Percent = Convert.ToSingle(drList["VAT_Percent"]);
                    this._fltVAT_Amount = Convert.ToSingle(drList["VAT_Amount"]);
                    this._decFinishAmount = Convert.ToDecimal(drList["FinishAmount"]);
                    this._iMaxDays = Convert.ToInt32(drList["MaxDays"]);
                    this._fltAverageAUM = Convert.ToSingle(drList["AverageAUM"]);
                    this._fltWeights = Convert.ToSingle(drList["Weights"]);
                    this._decMinYearly = Convert.ToDecimal(drList["MinYearly"]);
                    this._iService_ID = Convert.ToInt32(drList["Service_ID"]);
                    this._iInvoice_ID = Convert.ToInt32(drList["Invoice_ID"]);
                    this._dFees = Convert.ToDateTime(drList["DateFees"]);
                    this._sOfficialInformingDate = drList["OfficialInformingDate"] + "";
                    this._iTipos = Convert.ToInt32(drList["Tipos"]);
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
                _dtList = new DataTable("AdminFees_List");
                dtCol = _dtList.Columns.Add("ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AT_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ImageType", Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("DateFrom", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("DateTo", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("ContractTipos", Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("ContractTitle", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Code", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Portfolio", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Currency", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Package_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("PackageTitle", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AUM", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Days", Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("AmoiviPro", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AxiaPro", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AmoiviAfter", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AxiaAfter", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Discount_Percent1", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Discount_Amount1", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Discount_Percent2", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Discount_Amount2", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Discount_Percent", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Discount_Amount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MinAmoivi", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MinAmoivi_Percent", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MinAmoivi_Percent2", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishMinAmoivi", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("LastAmount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("VAT_Amount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishAmount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("LastAmount_Percent", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MaxDays", Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("AverageAUM", Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("Weights", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MinYearly", Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("DateFees", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("Service_Title", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("InvestmentPolicy", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("InvestmentProfile", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Advisory_Name", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RM_Name", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Introducer_Name", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Diaxiristis_Name", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("User1_Name", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Address", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("City", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Zip", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Country", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DOY", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AFM", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CountryEnglish", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("PostAddress", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientType", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Client_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ClientName", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Invoice_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Invoice_Num", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Invoice_Type", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Invoice_File", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("L4", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("VAT_Percent", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Contract_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contracts_Details_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contracts_Packages_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Service_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Status", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("User1_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ConnectionMethod", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("User_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ContractDateStart", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("OfficialInformingDate", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("EMail", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("BornPlace", Type.GetType("System.String"));

                conn.Open();
                cmd = new SqlCommand("GetAdminFees_OldList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AT_ID", _iAT_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["ImageType"] = (drList["FileName"] + "" == "") ? 0 : 1;
                    dtRow["DateFrom"] = drList["DateFrom"];
                    dtRow["DateTo"] = drList["DateTo"];
                    dtRow["ContractTitle"] = drList["ContractTitle"];
                    dtRow["Code"] = drList["Code"];
                    dtRow["Portfolio"] = drList["Portfolio"];
                    dtRow["Package_ID"] = drList["Package_ID"];
                    dtRow["PackageTitle"] = drList["Title"];
                    dtRow["Currency"] = drList["Currency"];
                    dtRow["AUM"] = drList["AUM"];
                    dtRow["Days"] = drList["Days"];
                    dtRow["AmoiviPro"] = drList["AmoiviPro"];
                    dtRow["AxiaPro"] = drList["AxiaPro"];
                    dtRow["AmoiviAfter"] = drList["AmoiviAfter"];
                    dtRow["AxiaAfter"] = drList["AxiaAfter"];
                    dtRow["Discount_Percent1"] = drList["Discount_Percent1"];
                    dtRow["Discount_Amount1"] = drList["Discount_Amount1"];
                    dtRow["Discount_Percent2"] = drList["Discount_Percent2"];
                    dtRow["Discount_Amount2"] = drList["Discount_Amount2"];
                    dtRow["Discount_Percent"] = drList["Discount_Percent"];
                    dtRow["Discount_Amount"] = drList["Discount_Amount"];
                    dtRow["MinAmoivi"] = drList["MinAmoivi"];
                    dtRow["MinAmoivi_Percent"] = drList["MinAmoivi_Percent"];
                    dtRow["MinAmoivi_Percent2"] = drList["MinAmoivi_Percent2"];
                    dtRow["FinishMinAmoivi"] = drList["FinishMinAmoivi"];
                    dtRow["LastAmount"] = drList["LastAmount"];
                    dtRow["LastAmount_Percent"] = drList["LastAmount_Percent"];
                    dtRow["VAT_Percent"] = drList["VAT_Percent"];
                    dtRow["VAT_Amount"] = drList["VAT_Amount"];
                    dtRow["FinishAmount"] = drList["FinishAmount"];
                    dtRow["MaxDays"] = drList["MaxDays"];
                    dtRow["AverageAUM"] = drList["AverageAUM"];
                    dtRow["Weights"] = drList["Weights"];
                    dtRow["MinYearly"] = drList["MinYearly"];
                    dtRow["User1_Name"] = (drList["Surname"] + " " + drList["Firstname"]).Trim();
                    dtRow["Address"] = drList["Address"] + "";
                    dtRow["City"] = drList["City"] + "";
                    dtRow["Zip"] = drList["ZIP"] + "";
                    dtRow["Country"] = drList["Country_Title"] + "";
                    dtRow["CountryEnglish"] = drList["Country_TitleEn"] + "";
                    dtRow["PostAddress"] = drList["Address"] + "~" + drList["City"] + "~" + drList["ZIP"];
                    dtRow["DateFees"] = drList["DateFees"];
                    dtRow["InvestmentPolicy"] = drList["AdvisoryInvestmentPolicy"] + "";
                    dtRow["InvestmentProfile"] = drList["AdvisoryInvestmentProfile"] + "";
                    dtRow["Contract_ID"] = drList["Contract_ID"];
                    dtRow["Contracts_Details_ID"] = drList["Contracts_Details_ID"];
                    dtRow["Contracts_Packages_ID"] = drList["Contract_Packages_ID"];
                    dtRow["Service_ID"] = drList["PackageType_ID"];
                    dtRow["User1_ID"] = drList["User1_ID"];
                    dtRow["Advisory_Name"] = (drList["Advisor_Surname"] + " " + drList["Advisor_Firstname"]).Trim();
                    dtRow["RM_Name"] = (drList["RM_Surname"] + " " + drList["RM_Firstname"]).Trim();
                    dtRow["Introducer_Name"] = (drList["Introducer_Surname"] + " " + drList["Introducer_Firstname"]).Trim();
                    dtRow["Diaxiristis_Name"] = (drList["Diaxiristis_Surname"] + " " + drList["Diaxiristis_Firstname"]).Trim();
                    //dtRow["Address"] = drList["Address") + "~" + drList["City") + "~" + drList["ZIP")  - SEE ABOVE
                    dtRow["DOY"] = drList["DOY"] + "";
                    dtRow["AFM"] = drList["AFM"] + "";
                    dtRow["Client_ID"] = drList["Client_ID"];
                    dtRow["ClientType"] = drList["ClientTipos"];
                    dtRow["ClientName"] = (drList["ClientName"] + "").Trim();
                    dtRow["Invoice_ID"] = drList["Invoice_ID"];
                    if (Convert.ToInt32(drList["Invoice_ID"]) == 0)
                    {
                        dtRow["Invoice_Num"] = "";
                        dtRow["Invoice_Type"] = dtRow["ClientType"];        // if Invoice_ID = 0  Invoice_Type is unknown, so InvoiceType = ClientType (1-Idiotis, 2-Company)
                        dtRow["Invoice_File"] = "";
                    }
                    else
                    {
                        dtRow["Invoice_Num"] = drList["InvCode"] + " " + drList["InvSeira"] + " " + drList["InvNum"];
                        dtRow["Invoice_Type"] = drList["InvType"];
                        dtRow["Invoice_File"] = drList["FileName"] + "";
                    }

                    dtRow["L4"] = drList["L4"] + "";
                    dtRow["ContractTitle"] = drList["ContractTitle"] + "";
                    dtRow["Service_ID"] = drList["Service_ID"];
                    dtRow["Service_Title"] = drList["Service_Title"] + "";
                    dtRow["ContractDateStart"] = drList["DateStart"];
                    dtRow["Status"] = drList["Status"];
                    dtRow["User_ID"] = drList["User_ID"];
                    dtRow["ConnectionMethod"] = drList["ConnectionMethod"];
                    dtRow["OfficialInformingDate"] = drList["OfficialInformingDate"] + "";
                    dtRow["EMail"] = (drList["EMail"] + "").Trim();
                    dtRow["ContractTipos"] = 1;
                    dtRow["BornPlace"] = "";
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void CheckRecord()
        {
            this._iRecord_ID = 0;
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetAdminFees_CheckRecord", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AT_ID", _iAT_ID));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dTo));
                cmd.Parameters.Add(new SqlParameter("@Code", _sCode));
                cmd.Parameters.Add(new SqlParameter("@Portfolio", _sPortfolio));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
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
                using (SqlCommand cmd = new SqlCommand("InsertAdminFees_Rec", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@AT_ID", SqlDbType.Int).Value = _iAT_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = _dFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = _dTo;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 30).Value = _sCode;
                    cmd.Parameters.Add("@Portfolio", SqlDbType.NVarChar, 20).Value = _sPortfolio;
                    cmd.Parameters.Add("@Currency", SqlDbType.NVarChar, 6).Value = _sCurrency;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Details_ID", SqlDbType.Int).Value = _iContract_Details_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@AUM", SqlDbType.Float).Value = _decAUM;
                    cmd.Parameters.Add("@Days", SqlDbType.Int).Value = _iDays;
                    cmd.Parameters.Add("@AmoiviPro", SqlDbType.Float).Value = _fltAmoiviPro;
                    cmd.Parameters.Add("@AxiaPro", SqlDbType.Float).Value = _fltAxiaPro;
                    cmd.Parameters.Add("@AmoiviAfter", SqlDbType.Float).Value = _fltAmoiviAfter;
                    cmd.Parameters.Add("@AxiaAfter", SqlDbType.Float).Value = _fltAxiaAfter;
                    cmd.Parameters.Add("@Discount_Percent1", SqlDbType.Float).Value = _fltDiscount_Percent1;
                    cmd.Parameters.Add("@Discount_Amount1", SqlDbType.Float).Value = _fltDiscount_Amount1;
                    cmd.Parameters.Add("@Discount_Percent2", SqlDbType.Float).Value = _fltDiscount_Percent2;
                    cmd.Parameters.Add("@Discount_Amount2", SqlDbType.Float).Value = _fltDiscount_Amount2;
                    cmd.Parameters.Add("@Discount_Percent", SqlDbType.Float).Value = _fltDiscount_Percent;
                    cmd.Parameters.Add("@Discount_Amount", SqlDbType.Float).Value = _fltDiscount_Amount;
                    cmd.Parameters.Add("@MinAmoivi", SqlDbType.Float).Value = _fltMinAmoivi;
                    cmd.Parameters.Add("@MinAmoivi_Percent", SqlDbType.Float).Value = _fltMinAmoivi_Percent;
                    cmd.Parameters.Add("@MinAmoivi_Percent2", SqlDbType.Float).Value = _fltMinAmoivi_Percent2;
                    cmd.Parameters.Add("@FinishMinAmoivi", SqlDbType.Float).Value = _decFinishMinAmoivi;
                    cmd.Parameters.Add("@LastAmount", SqlDbType.Float).Value = _decLastAmount;
                    cmd.Parameters.Add("@LastAmount_Percent", SqlDbType.Float).Value = _fltLastAmount_Percent;
                    cmd.Parameters.Add("@VAT_Percent", SqlDbType.Float).Value = _fltVAT_Percent;
                    cmd.Parameters.Add("@VAT_Amount", SqlDbType.Float).Value = _fltVAT_Amount;
                    cmd.Parameters.Add("@FinishAmount", SqlDbType.Float).Value = _decFinishAmount;
                    cmd.Parameters.Add("@MaxDays", SqlDbType.Float).Value = _iMaxDays;
                    cmd.Parameters.Add("@AverageAUM", SqlDbType.Float).Value = _fltAverageAUM;
                    cmd.Parameters.Add("@Weights", SqlDbType.Float).Value = _fltWeights;
                    cmd.Parameters.Add("@MinYearly", SqlDbType.Float).Value = _decMinYearly;
                    cmd.Parameters.Add("@Service_ID", SqlDbType.Int).Value = _iService_ID;
                    cmd.Parameters.Add("@Invoice_ID", SqlDbType.Int).Value = _iInvoice_ID;
                    cmd.Parameters.Add("@DateFees", SqlDbType.DateTime).Value = _dFees;
                    cmd.Parameters.Add("@Tipos", SqlDbType.Int).Value = _iTipos;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;             // 1 - Active, 2 - Cancelled
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
                using (SqlCommand cmd = new SqlCommand("EditAdminFees_Rec", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Tipos", SqlDbType.Int).Value = _iTipos;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;             // 1 - Active, 2 - Cancelled
                    cmd.Parameters.Add("@AT_ID", SqlDbType.Int).Value = _iAT_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = _dFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = _dTo;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 30).Value = _sCode;
                    cmd.Parameters.Add("@Portfolio", SqlDbType.NVarChar, 20).Value = _sPortfolio;
                    cmd.Parameters.Add("@Currency", SqlDbType.NVarChar, 6).Value = _sCurrency;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Details_ID", SqlDbType.Int).Value = _iContract_Details_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@AUM", SqlDbType.Float).Value = _decAUM;
                    cmd.Parameters.Add("@Days", SqlDbType.Int).Value = _iDays;
                    cmd.Parameters.Add("@AmoiviPro", SqlDbType.Float).Value = _fltAmoiviPro;
                    cmd.Parameters.Add("@AxiaPro", SqlDbType.Float).Value = _fltAxiaPro;
                    cmd.Parameters.Add("@AmoiviAfter", SqlDbType.Float).Value = _fltAmoiviAfter;
                    cmd.Parameters.Add("@AxiaAfter", SqlDbType.Float).Value = _fltAxiaAfter;
                    cmd.Parameters.Add("@Discount_Percent1", SqlDbType.Float).Value = _fltDiscount_Percent1;
                    cmd.Parameters.Add("@Discount_Amount1", SqlDbType.Float).Value = _fltDiscount_Amount1;
                    cmd.Parameters.Add("@Discount_Percent2", SqlDbType.Float).Value = _fltDiscount_Percent2;
                    cmd.Parameters.Add("@Discount_Amount2", SqlDbType.Float).Value = _fltDiscount_Amount2;
                    cmd.Parameters.Add("@Discount_Percent", SqlDbType.Float).Value = _fltDiscount_Percent;
                    cmd.Parameters.Add("@Discount_Amount", SqlDbType.Float).Value = _fltDiscount_Amount;
                    cmd.Parameters.Add("@MinAmoivi", SqlDbType.Float).Value = _fltMinAmoivi;
                    cmd.Parameters.Add("@MinAmoivi_Percent", SqlDbType.Float).Value = _fltMinAmoivi_Percent;
                    cmd.Parameters.Add("@MinAmoivi_Percent2", SqlDbType.Float).Value = _fltMinAmoivi_Percent2;
                    cmd.Parameters.Add("@FinishMinAmoivi", SqlDbType.Float).Value = _decFinishMinAmoivi;
                    cmd.Parameters.Add("@LastAmount", SqlDbType.Float).Value = _decLastAmount;
                    cmd.Parameters.Add("@LastAmount_Percent", SqlDbType.Float).Value = _fltLastAmount_Percent;
                    cmd.Parameters.Add("@VAT_Percent", SqlDbType.Float).Value = _fltVAT_Percent;
                    cmd.Parameters.Add("@VAT_Amount", SqlDbType.Float).Value = _fltVAT_Amount;
                    cmd.Parameters.Add("@FinishAmount", SqlDbType.Float).Value = _decFinishAmount;
                    cmd.Parameters.Add("@MaxDays", SqlDbType.Float).Value = _iMaxDays;
                    cmd.Parameters.Add("@AverageAUM", SqlDbType.Float).Value = _fltAverageAUM;
                    cmd.Parameters.Add("@Weights", SqlDbType.Float).Value = _fltWeights;
                    cmd.Parameters.Add("@MinYearly", SqlDbType.Float).Value = _decMinYearly;
                    cmd.Parameters.Add("@Service_ID", SqlDbType.Int).Value = _iService_ID;
                    cmd.Parameters.Add("@Invoice_ID", SqlDbType.Int).Value = _iInvoice_ID;
                    cmd.Parameters.Add("@DateFees", SqlDbType.DateTime).Value = _dFees;
                    cmd.Parameters.Add("@OfficialInformingDate", SqlDbType.NVarChar, 20).Value = _sOfficialInformingDate;
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = _iUser_ID;
                    cmd.Parameters.Add("@DateEdit", SqlDbType.DateTime).Value = _dEdit;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "AdminFees_Recs";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "AT_ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }

                using (SqlCommand cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "AdminFees_Titles";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int AT_ID { get { return this._iAT_ID; } set { this._iAT_ID = value; } }
        public int Client_ID { get { return this._iClient_ID; } set { this._iClient_ID = value; } }
        public DateTime DateFrom { get { return this._dFrom; } set { this._dFrom = value; } }
        public DateTime DateTo { get { return this._dTo; } set { this._dTo = value; } }
        public string Code { get { return this._sCode; } set { this._sCode = value; } }
        public string Portfolio { get { return this._sPortfolio; } set { this._sPortfolio = value; } }
        public string Currency { get { return this._sCurrency; } set { this._sCurrency = value; } }
        public int Contract_ID { get { return this._iContract_ID; } set { this._iContract_ID = value; } }
        public int Contract_Details_ID { get { return this._iContract_Details_ID; } set { this._iContract_Details_ID = value; } }
        public int Contract_Packages_ID { get { return this._iContract_Packages_ID; } set { this._iContract_Packages_ID = value; } }
        public decimal AUM { get { return this._decAUM; } set { this._decAUM = value; } }
        public int Days { get { return this._iDays; } set { this._iDays = value; } }
        public float AmoiviPro { get { return this._fltAmoiviPro; } set { this._fltAmoiviPro = value; } }
        public float AxiaPro { get { return this._fltAxiaPro; } set { this._fltAxiaPro = value; } }
        public float AmoiviAfter { get { return this._fltAmoiviAfter; } set { this._fltAmoiviAfter = value; } }
        public float AxiaAfter { get { return this._fltAxiaAfter; } set { this._fltAxiaAfter = value; } }
        public float Discount_Percent1 { get { return this._fltDiscount_Percent1; } set { this._fltDiscount_Percent1 = value; } }
        public float Discount_Amount1 { get { return this._fltDiscount_Amount1; } set { this._fltDiscount_Amount1 = value; } }
        public float Discount_Percent2 { get { return this._fltDiscount_Percent2; } set { this._fltDiscount_Percent2 = value; } }
        public float Discount_Amount2 { get { return this._fltDiscount_Amount2; } set { this._fltDiscount_Amount2 = value; } }
        public float Discount_Percent { get { return this._fltDiscount_Percent; } set { this._fltDiscount_Percent = value; } }
        public float Discount_Amount { get { return this._fltDiscount_Amount; } set { this._fltDiscount_Amount = value; } }
        public float MinAmoivi { get { return this._fltMinAmoivi; } set { this._fltMinAmoivi = value; } }
        public float MinAmoivi_Percent { get { return this._fltMinAmoivi_Percent; } set { this._fltMinAmoivi_Percent = value; } }
        public float MinAmoivi_Percent2 { get { return this._fltMinAmoivi_Percent2; } set { this._fltMinAmoivi_Percent2 = value; } }
        public decimal FinishMinAmoivi { get { return this._decFinishMinAmoivi; } set { this._decFinishMinAmoivi = value; } }
        public decimal LastAmount { get { return this._decLastAmount; } set { this._decLastAmount = value; } }
        public float LastAmount_Percent { get { return this._fltLastAmount_Percent; } set { this._fltLastAmount_Percent = value; } }
        public float VAT_Percent { get { return this._fltVAT_Percent; } set { this._fltVAT_Percent = value; } }
        public float VAT_Amount { get { return this._fltVAT_Amount; } set { this._fltVAT_Amount = value; } }
        public decimal FinishAmount { get { return this._decFinishAmount; } set { this._decFinishAmount = value; } }
        public int MaxDays { get { return this._iMaxDays; } set { this._iMaxDays = value; } }
        public float AverageAUM { get { return this._fltAverageAUM; } set { this._fltAverageAUM = value; } }
        public float Weights { get { return this._fltWeights; } set { this._fltWeights = value; } }
        public decimal MinYearly { get { return this._decMinYearly; } set { this._decMinYearly = value; } }        
        public int Service_ID { get { return this._iService_ID; } set { this._iService_ID = value; } }
        public int Invoice_ID { get { return this._iInvoice_ID; } set { this._iInvoice_ID = value; } }
        public DateTime DateFees { get { return this._dFees; } set { this._dFees = value; } }
        public string OfficialInformingDate { get { return this._sOfficialInformingDate; } set { this._sOfficialInformingDate = value; } }
        public int Tipos { get { return this._iTipos; } set { this._iTipos = value; } }
        public int Status { get { return this._iStatus; } set { this._iStatus = value; } }
        public int User_ID { get { return this._iUser_ID; } set { this._iUser_ID = value; } }
        public DateTime DateEdit { get { return this._dEdit; } set { this._dEdit = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}