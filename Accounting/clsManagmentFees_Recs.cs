using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsManagmentFees_Recs
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        IDataReader drList;
        DataColumn dtCol;
        DataRow dtRow;

        private int       _iRecord_ID;
        private int       _iFT_ID;
        private int       _iClient_ID;
        private DateTime  _dDateFrom;
        private DateTime  _dDateTo;
        private string    _sCode;
        private string    _sPortfolio;
        private string    _sCurrency;
        private int       _iContract_ID;
        private int       _iContract_Details_ID;
        private int       _iContract_Packages_ID;
        private decimal   _decAUM;
        private int       _iDays;
        private float     _fltAmoiviPro;
        private float     _fltAxiaPro;
        private string    _sClimakas;
        private string    _sDiscount_DateFrom;
        private string    _sDiscount_DateTo;
        private float     _fltDiscount_Percent1;
        private float     _fltDiscount_Amount1;
        private float     _fltDiscount_Percent2;
        private float     _fltDiscount_Amount2;
        private float     _fltDiscount_Percent;
        private float     _fltDiscount_Amount;
        private float     _fltAmoiviAfter;
        private float     _fltAxiaAfter;
        private float     _fltMinAmoivi;
        private float     _fltMinAmoivi_Percent;
        private decimal   _decFinishMinAmoivi;
        private decimal   _decLastAmount;
        private float     _fltLastAmount_Percent;
        private float     _fltVAT_Percent;
        private float     _fltVAT_Amount;
        private decimal   _decFinishAmount;
        private int       _iService_ID;
        private int       _iInvoice_ID;
        private string    _sInvoice_Num;
        private string    _sInvoice_File;
        private DateTime  _dFees;
        private int       _iInvoice_Type;
        private string    _sStatement_File;
        private string    _sMisc_File;
        private string    _sOfficialInformingDate;
        private string    _sNotes;
        private string    _sInvoice_External;
        private int       _iStatus;
        private int       _iUser_ID;
        private DateTime  _dEdit;

        private int       _iCFP_ID;
        private int       _iYear;
        private int       _iQuart;
        private int       _iServiceProvider_ID;
        private DataTable _dtList;

        public clsManagmentFees_Recs()
        {
            this._iRecord_ID = 0;
            this._iFT_ID = 0;
            this._iClient_ID = 0;
            this._dDateFrom = Convert.ToDateTime("1900/01/01");
            this._dDateTo = Convert.ToDateTime("1900/01/01");
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
            this._sClimakas = "";
            this._sDiscount_DateFrom = "";
            this._sDiscount_DateTo = "";
            this._fltDiscount_Percent1 = 0;
            this._fltDiscount_Amount1 = 0;
            this._fltDiscount_Percent2 = 0;
            this._fltDiscount_Amount2 = 0;
            this._fltDiscount_Percent = 0;
            this._fltDiscount_Amount = 0;
            this._fltAmoiviAfter = 0;
            this._fltAxiaAfter = 0;
            this._fltMinAmoivi = 0;
            this._fltMinAmoivi_Percent = 0;
            this._decFinishMinAmoivi = 0;
            this._decLastAmount = 0;
            this._fltLastAmount_Percent = 0;
            this._fltVAT_Percent = 0;
            this._fltVAT_Amount = 0;
            this._decFinishAmount = 0;
            this._iService_ID = 0;
            this._iInvoice_ID = 0;
            this._sInvoice_Num = "";
            this._sInvoice_File = "";
            this._dFees = Convert.ToDateTime("1900/01/01");
            this._iInvoice_Type = 0;
            this._sStatement_File = "";
            this._sMisc_File = "";
            this._sOfficialInformingDate = "";
            this._sNotes = "";
            this._sInvoice_External = "";
            this._iStatus = 0;
            this._iUser_ID = 0;
            this._dEdit = Convert.ToDateTime("1900/01/01");

            this._iCFP_ID = 0;
            this._iYear = 0;
            this._iQuart = 0;
            this._iServiceProvider_ID = 0;
        }

        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "ManagmentFees_Recs"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = drList.GetInt32(0); //Convert.ToInt32(drList["ID"]);
                    this._iFT_ID = Convert.ToInt32(drList["FT_ID"]);
                    this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                    this._dDateFrom = Convert.ToDateTime(drList["DateFrom"]);
                    this._dDateTo = Convert.ToDateTime(drList["DateTo"]);
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
                    this._sClimakas = drList["Climakas"] + "";
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
                    this._decFinishMinAmoivi = Convert.ToDecimal(drList["FinishMinAmoivi"]);
                    this._decLastAmount = Convert.ToDecimal(drList["LastAmount"]);
                    this._fltLastAmount_Percent = Convert.ToSingle(drList["LastAmount_Percent"]);
                    this._fltVAT_Percent = Convert.ToSingle(drList["VAT_Percent"]);
                    this._fltVAT_Amount = Convert.ToSingle(drList["VAT_Amount"]);
                    this._decFinishAmount = Convert.ToDecimal(drList["FinishAmount"]);
                    this._iService_ID = Convert.ToInt32(drList["Service_ID"]);
                    this._iInvoice_ID = Convert.ToInt32(drList["Invoice_ID"]);
                    this._sInvoice_Num = drList["Invoice_Num"] + "";
                    this._sInvoice_File = drList["Invoice_File"] + "";
                    this._iInvoice_Type = Convert.ToInt32(drList["Invoice_Type"]);                              // 0 - new record, 1- APY, 2 -TPY, 3 - new record not MF, 4 - pistotiko, 5 - akyrotiko 
                    this._dFees = Convert.ToDateTime(drList["DateFees"]);
                    this._sStatement_File = drList["Statement_File"] + ""; 
                    this._sMisc_File = drList["Misc_File"] + ""; ;
                    this._sOfficialInformingDate = drList["OfficialInformingDate"] + ""; 
                    this._sNotes = drList["Notes"] + ""; 
                    this._sInvoice_External = drList["Invoice_External"] + "";
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
                _dtList = new DataTable("ManagmentFees_List");
                dtCol = _dtList.Columns.Add("ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("FT_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ImageType", Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("DateFrom", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("DateTo", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("ContractTitle", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Code", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Portfolio", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Currency", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Package", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AUM", Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("Days", Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("AmoiviPro", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AxiaPro", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Climakas", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Discount_DateFrom", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Discount_DateTo", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AmoiviAfter", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AxiaAfter", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MinAmoivi", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MinAmoivi_Percent", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishMinAmoivi", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("LastAmount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("VAT_Amount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishAmount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("LastAmount_Percent", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Invoice_Num", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DateFees", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Notes", Type.GetType("System.String"));
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
                //dtCol = _dtList.Columns.Add("PostAddress", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientType", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Client_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Invoice_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Invoice_Type", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Invoice_External", Type.GetType("System.String"));                
                dtCol = _dtList.Columns.Add("L4", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("VAT_Percent", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Contract_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contracts_Details_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contracts_Packages_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Service_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Status", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Discount_Percent1", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Discount_Amount1", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Discount_Percent2", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Discount_Amount2", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Discount_Percent", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Discount_Amount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("User1_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ConnectionMethod", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Invoice_Arithmos", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("FileName", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("User_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Author_Name", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DateEdit", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("MIFID_2", Type.GetType("System.Int16"));

                conn.Open();
                cmd = new SqlCommand("GetManagmentFees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@FT_ID", _iFT_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ImageType"] = (drList["Invoice_File"] + "" == "") ? 0 : 1 ;
                    dtRow["DateFrom"] = drList["DateFrom"];
                    dtRow["DateTo"] = drList["DateTo"];
                    dtRow["ContractTitle"] = drList["ContractTitle"];
                    dtRow["Code"] = drList["Code"];
                    dtRow["Portfolio"] = drList["Portfolio"];
                    dtRow["Package"] = drList["Title"];
                    dtRow["Currency"] = drList["Currency"];
                    dtRow["AUM"] = drList["AUM"];
                    dtRow["Days"] = drList["Days"];
                    dtRow["AmoiviPro"] = drList["AmoiviPro"];
                    dtRow["AxiaPro"] = drList["AxiaPro"];
                    dtRow["Climakas"] = drList["Climakas"];
                    dtRow["Discount_DateFrom"] = "";
                    dtRow["Discount_DateTo"] = drList["Discount_DateTo"];
                    dtRow["Discount_Percent1"] = drList["Discount_Percent1"];
                    dtRow["Discount_Amount1"] = drList["Discount_Amount1"];
                    dtRow["Discount_Percent2"] = drList["Discount_Percent2"];
                    dtRow["Discount_Amount2"] = drList["Discount_Amount2"];
                    dtRow["Discount_Percent"] = drList["Discount_Percent"];
                    dtRow["Discount_Amount"] = drList["Discount_Amount"];
                    dtRow["AmoiviAfter"] = drList["AmoiviAfter"];
                    dtRow["AxiaAfter"] = drList["AxiaAfter"];
                    dtRow["MinAmoivi"] = drList["MinAmoivi"];
                    dtRow["MinAmoivi_Percent"] = drList["MinAmoivi_Percent"];
                    dtRow["FinishMinAmoivi"] = drList["FinishMinAmoivi"];
                    dtRow["LastAmount"] = drList["LastAmount"];
                    dtRow["LastAmount_Percent"] = drList["LastAmount_Percent"];
                    dtRow["VAT_Percent"] = drList["VAT_Percent"];
                    dtRow["VAT_Amount"] = drList["VAT_Amount"];
                    dtRow["FinishAmount"] = drList["FinishAmount"];
                    dtRow["User1_Name"] = drList["Surname"] + " " + drList["Firstname"];
                    dtRow["Address"] = drList["Address"] + "";
                    dtRow["City"] = drList["City"] + "";
                    dtRow["Zip"] = drList["ZIP"] + "";
                    dtRow["Country"] = drList["Country_Title"] + "";
                    dtRow["CountryEnglish"] = drList["Country_TitleEn"] + "";
                    //dtRow["PostAddress"] = drList["Address"] + "~" + drList["City"] + "~" + drList["ZIP"];
                    dtRow["Invoice_Num"] = drList["Invoice_Num"] + "";
                    dtRow["FileName"] = drList["Invoice_File"] + "";
                    dtRow["DateFees"] = "";
                    if (Convert.ToDateTime(drList["DateFees"]) != Convert.ToDateTime("1900/01/01"))  dtRow["DateFees"] = drList["DateFees"];
                    dtRow["InvestmentPolicy"] = drList["AdvisoryInvestmentPolicy"] + "";
                    dtRow["InvestmentProfile"] = drList["AdvisoryInvestmentProfile"] + "";
                    dtRow["Contract_ID"] = drList["Contract_ID"];
                    dtRow["Contracts_Details_ID"] = drList["Contracts_Details_ID"];
                    dtRow["Contracts_Packages_ID"] = drList["Contract_Packages_ID"];
                    dtRow["Service_ID"] = drList["PackageType_ID"];
                    dtRow["Notes"] = drList["Notes"];
                    dtRow["User1_ID"] = drList["User1_ID"];
                    dtRow["Advisory_Name"] = drList["Advisor_Surname"] + " " + drList["Advisor_Firstname"];
                    dtRow["RM_Name"] = drList["RM_Surname"] + " " + drList["RM_Firstname"];
                    dtRow["Introducer_Name"] = drList["Introducer_Surname"] + " " + drList["Introducer_Firstname"];
                    dtRow["Diaxiristis_Name"] = drList["Diaxiristis_Surname"] + " " + drList["Diaxiristis_Firstname"];
                    //dtRow["Address"] = drList["Address") + "~" + drList["City") + "~" + drList["ZIP")  - SEE ABOVE
                    dtRow["DOY"] = drList["DOY"] + "";
                    dtRow["AFM"] = drList["AFM"] + "";
                    dtRow["ClientType"] = drList["ClientTipos"];
                    dtRow["ID"] = drList["ID"];
                    dtRow["Client_ID"] = drList["Client_ID"];
                    dtRow["Invoice_ID"] = drList["Invoice_ID"];
                    dtRow["Invoice_Type"] = drList["Invoice_Type"];
                    if (Convert.ToInt32(dtRow["Invoice_Type"]) == 0)
                    {
                        if (Convert.ToInt32(drList["ClientTipos"]) == 1) dtRow["Invoice_Type"] = 1;
                        else                                             dtRow["Invoice_Type"] = 2;
                    }
                    dtRow["Invoice_External"] = drList["Invoice_External"];
                    dtRow["L4"] = drList["L4"] + "";
                    dtRow["ContractTitle"] = drList["ContractTitle"] + "";
                    dtRow["Service_ID"] = drList["Service_ID"];
                    dtRow["Service_Title"] = drList["Service_Title"] + "";
                    dtRow["MIFID_2"] = drList["MIFID_2"];
                    dtRow["Status"] = drList["Status"];
                    dtRow["User_ID"] = drList["User_ID"];
                    dtRow["Author_Name"] = (drList["Author_Surname"] + " " + drList["Author_Firstname"]).Trim();
                    dtRow["DateEdit"] = Convert.ToDateTime(drList["DateEdit"]).ToString("dd/MM/yyyy");
                    dtRow["ConnectionMethod"] = drList["ConnectionMethod"];
                    dtRow["Invoice_Arithmos"] = drList["Invoice_Arithmos"] + "";

                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList_Quart()
        {
            try
            {
                _dtList = new DataTable("QuartManFees_List");
                dtCol = _dtList.Columns.Add("Year", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Quart", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Client_ID", Type.GetType("System.Int32"));               
                dtCol = _dtList.Columns.Add("Code", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Portfolio", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("NewAmount", Type.GetType("System.Single"));

                conn.Open();
                cmd = new SqlCommand("GetManagmentFees_Quart", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@MF_Year", _iYear));
                cmd.Parameters.Add(new SqlParameter("@MF_Quart", _iQuart));
                cmd.Parameters.Add(new SqlParameter("@Client_ID", _iClient_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["Year"] = drList["Year"];
                    dtRow["Quart"] = drList["Quart"];
                    dtRow["Client_ID"] = drList["Client_ID"];
                    dtRow["Code"] = drList["Code"];
                    dtRow["Portfolio"] = drList["Portfolio"];
                    dtRow["NewAmount"] = drList["NewAmount"];
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
                using (SqlCommand cmd = new SqlCommand("InsertManagmentFees_Rec", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@FT_ID", SqlDbType.Int).Value = _iFT_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = _dDateFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = _dDateTo;
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
                    cmd.Parameters.Add("@Climakas", SqlDbType.NVarChar, 50).Value = _sClimakas;
                    cmd.Parameters.Add("@Discount_DateFrom", SqlDbType.NVarChar, 20).Value = _sDiscount_DateFrom;
                    cmd.Parameters.Add("@Discount_DateTo", SqlDbType.NVarChar, 20).Value = _sDiscount_DateTo;
                    cmd.Parameters.Add("@Discount_Percent1", SqlDbType.Float).Value = _fltDiscount_Percent1;
                    cmd.Parameters.Add("@Discount_Amount1", SqlDbType.Float).Value = _fltDiscount_Amount1;
                    cmd.Parameters.Add("@Discount_Percent2", SqlDbType.Float).Value = _fltDiscount_Percent2;
                    cmd.Parameters.Add("@Discount_Amount2", SqlDbType.Float).Value = _fltDiscount_Amount2;
                    cmd.Parameters.Add("@Discount_Percent", SqlDbType.Float).Value = _fltDiscount_Percent;
                    cmd.Parameters.Add("@Discount_Amount", SqlDbType.Float).Value = _fltDiscount_Amount;
                    cmd.Parameters.Add("@AmoiviAfter", SqlDbType.Float).Value = _fltAmoiviAfter;
                    cmd.Parameters.Add("@AxiaAfter", SqlDbType.Float).Value = _fltAxiaAfter;
                    cmd.Parameters.Add("@MinAmoivi", SqlDbType.Float).Value = _fltMinAmoivi;
                    cmd.Parameters.Add("@MinAmoivi_Percent", SqlDbType.Float).Value = _fltMinAmoivi_Percent;
                    cmd.Parameters.Add("@FinishMinAmoivi", SqlDbType.Decimal).Value = _decFinishMinAmoivi;
                    cmd.Parameters.Add("@LastAmount", SqlDbType.Decimal).Value = _decLastAmount;
                    cmd.Parameters.Add("@LastAmount_Percent", SqlDbType.Float).Value = _fltLastAmount_Percent;
                    cmd.Parameters.Add("@VAT_Percent", SqlDbType.Float).Value = _fltVAT_Percent;
                    cmd.Parameters.Add("@VAT_Amount", SqlDbType.Float).Value = _fltVAT_Amount;
                    cmd.Parameters.Add("@FinishAmount", SqlDbType.Decimal).Value = _decFinishAmount;
                    cmd.Parameters.Add("@Service_ID", SqlDbType.Int).Value = _iService_ID;
                    cmd.Parameters.Add("@Invoice_ID", SqlDbType.Int).Value = _iInvoice_ID;
                    cmd.Parameters.Add("@Invoice_Num", SqlDbType.NVarChar, 30).Value = _sInvoice_Num;
                    cmd.Parameters.Add("@Invoice_File", SqlDbType.NVarChar, 50).Value = _sInvoice_File;
                    cmd.Parameters.Add("@DateFees", SqlDbType.DateTime).Value = _dFees;
                    cmd.Parameters.Add("@Invoice_Type", SqlDbType.Int).Value = _iInvoice_Type;                                      // 0 - regular record, 4 - pistotiko, 5 - akyrotiko
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 200).Value = _sNotes + "";
                    cmd.Parameters.Add("@Invoice_External", SqlDbType.NVarChar, 30).Value = _sInvoice_External;
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
                using (SqlCommand cmd = new SqlCommand("EditManagmentFees_Rec", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@FT_ID", SqlDbType.Int).Value = _iFT_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = _dDateFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = _dDateTo;
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
                    cmd.Parameters.Add("@Climakas", SqlDbType.NVarChar, 50).Value = _sClimakas;
                    cmd.Parameters.Add("@Discount_DateFrom", SqlDbType.NVarChar, 20).Value = _sDiscount_DateFrom;
                    cmd.Parameters.Add("@Discount_DateTo", SqlDbType.NVarChar, 20).Value = _sDiscount_DateTo;
                    cmd.Parameters.Add("@Discount_Percent1", SqlDbType.Float).Value = _fltDiscount_Percent1;
                    cmd.Parameters.Add("@Discount_Amount1", SqlDbType.Float).Value = _fltDiscount_Amount1;
                    cmd.Parameters.Add("@Discount_Percent2", SqlDbType.Float).Value = _fltDiscount_Percent2;
                    cmd.Parameters.Add("@Discount_Amount2", SqlDbType.Float).Value = _fltDiscount_Amount2;
                    cmd.Parameters.Add("@Discount_Percent", SqlDbType.Float).Value = _fltDiscount_Percent;
                    cmd.Parameters.Add("@Discount_Amount", SqlDbType.Float).Value = _fltDiscount_Amount;
                    cmd.Parameters.Add("@AmoiviAfter", SqlDbType.Float).Value = _fltAmoiviAfter;
                    cmd.Parameters.Add("@AxiaAfter", SqlDbType.Float).Value = _fltAxiaAfter;
                    cmd.Parameters.Add("@MinAmoivi", SqlDbType.Float).Value = _fltMinAmoivi;
                    cmd.Parameters.Add("@MinAmoivi_Percent", SqlDbType.Float).Value = _fltMinAmoivi_Percent;
                    cmd.Parameters.Add("@FinishMinAmoivi", SqlDbType.Decimal).Value = _decFinishMinAmoivi;
                    cmd.Parameters.Add("@LastAmount", SqlDbType.Decimal).Value = _decLastAmount;
                    cmd.Parameters.Add("@LastAmount_Percent", SqlDbType.Float).Value = _fltLastAmount_Percent;
                    cmd.Parameters.Add("@VAT_Percent", SqlDbType.Float).Value = _fltVAT_Percent;
                    cmd.Parameters.Add("@VAT_Amount", SqlDbType.Float).Value = _fltVAT_Amount;
                    cmd.Parameters.Add("@FinishAmount", SqlDbType.Decimal).Value = _decFinishAmount;
                    cmd.Parameters.Add("@Service_ID", SqlDbType.Int).Value = _iService_ID;
                    cmd.Parameters.Add("@Invoice_ID", SqlDbType.Int).Value = _iInvoice_ID;
                    cmd.Parameters.Add("@Invoice_Num", SqlDbType.NVarChar, 30).Value = _sInvoice_Num;
                    cmd.Parameters.Add("@Invoice_File", SqlDbType.NVarChar, 50).Value = _sInvoice_File;
                    cmd.Parameters.Add("@DateFees", SqlDbType.DateTime).Value = _dFees;
                    cmd.Parameters.Add("@Invoice_Type", SqlDbType.Int).Value = _iInvoice_Type;                                      // 0 - regular record, 4 - pistotiko, 5 - akyrotiko
                    cmd.Parameters.Add("@Statement_File", SqlDbType.NVarChar, 50).Value = _sStatement_File;
                    cmd.Parameters.Add("@Misc_File", SqlDbType.NVarChar, 50).Value = _sMisc_File;
                    cmd.Parameters.Add("@OfficialInformingDate", SqlDbType.NVarChar, 20).Value = _sOfficialInformingDate;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 200).Value = _sNotes + "";
                    cmd.Parameters.Add("@Invoice_External", SqlDbType.NVarChar, 30).Value = _sInvoice_External;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;                                                  // 1 - Active, 2 - Cancelled
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ManagmentFees_Recs";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void CalcFees()
        {   
            _fltDiscount_Amount = _fltDiscount_Amount1 + _fltDiscount_Amount2;                                                            // Axia ekptosis 
            _fltDiscount_Percent = (_fltDiscount_Percent1 + _fltDiscount_Percent2);                                                       // % Ekptosis  
            _fltAxiaAfter =_fltAxiaPro - _fltDiscount_Amount;                                                                             // Poso meta tin ekptosis

            _fltAmoiviAfter = 0;
            if (_fltAxiaPro != 0)  _fltAmoiviAfter = (float)(_fltAxiaAfter * 36000.0 / (float)_decAUM * (float)_iDays);

            if (_decFinishMinAmoivi > (decimal)_fltAxiaAfter) _decLastAmount = _decFinishMinAmoivi;
            else                                              _decLastAmount = (decimal)_fltAxiaAfter;

            _fltLastAmount_Percent = 0;
            if (_decAUM != 0 && _iDays != 0)  _fltLastAmount_Percent = (float)(_decLastAmount * 36000 / (_decAUM * _iDays));

            _fltVAT_Amount = ((float)_decLastAmount * _fltVAT_Percent / 100);                                                            // FPA
            _decFinishAmount = (_decLastAmount * (decimal)(100.0 + _fltVAT_Percent) / 100);                                              // Teliko poso - Poso me FPA
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int FT_ID { get { return this._iFT_ID; } set { this._iFT_ID = value; } }
        public int Client_ID { get { return this._iClient_ID; } set { this._iClient_ID = value; } }
        public DateTime DateFrom { get { return this._dDateFrom; } set { this._dDateFrom = value; } }
        public DateTime DateTo { get { return this._dDateTo; } set { this._dDateTo = value; } }
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
        public string Climakas { get { return this._sClimakas; } set { this._sClimakas = value; } }
        public float AmoiviAfter { get { return this._fltAmoiviAfter; } set { this._fltAmoiviAfter = value; } }
        public float AxiaAfter { get { return this._fltAxiaAfter; } set { this._fltAxiaAfter = value; } }
        public string Discount_DateFrom { get { return this._sDiscount_DateFrom; } set { this._sDiscount_DateFrom = value; } }
        public string Discount_DateTo { get { return this._sDiscount_DateTo; } set { this._sDiscount_DateTo = value; } }
        public float Discount_Percent1 { get { return this._fltDiscount_Percent1; } set { this._fltDiscount_Percent1 = value; } }
        public float Discount_Amount1 { get { return this._fltDiscount_Amount1; } set { this._fltDiscount_Amount1 = value; } }
        public float Discount_Percent2 { get { return this._fltDiscount_Percent2; } set { this._fltDiscount_Percent2 = value; } }
        public float Discount_Amount2 { get { return this._fltDiscount_Amount2; } set { this._fltDiscount_Amount2 = value; } }
        public float Discount_Percent { get { return this._fltDiscount_Percent; } set { this._fltDiscount_Percent = value; } }
        public float Discount_Amount { get { return this._fltDiscount_Amount; } set { this._fltDiscount_Amount = value; } }
        public float MinAmoivi { get { return this._fltMinAmoivi; } set { this._fltMinAmoivi = value; } }
        public float MinAmoivi_Percent { get { return this._fltMinAmoivi_Percent; } set { this._fltMinAmoivi_Percent = value; } }
        public decimal FinishMinAmoivi { get { return this._decFinishMinAmoivi; } set { this._decFinishMinAmoivi = value; } }
        public decimal LastAmount { get { return this._decLastAmount; } set { this._decLastAmount = value; } }
        public float LastAmount_Percent { get { return this._fltLastAmount_Percent; } set { this._fltLastAmount_Percent = value; } }
        public float VAT_Percent { get { return this._fltVAT_Percent; } set { this._fltVAT_Percent = value; } }
        public float VAT_Amount { get { return this._fltVAT_Amount; } set { this._fltVAT_Amount = value; } }
        public decimal FinishAmount { get { return this._decFinishAmount; } set { this._decFinishAmount = value; } }
        public int Service_ID { get { return this._iService_ID; } set { this._iService_ID = value; } }
        public int Invoice_ID { get { return this._iInvoice_ID; } set { this._iInvoice_ID = value; } }
        public string Invoice_Num { get { return this._sInvoice_Num; } set { this._sInvoice_Num = value; } }
        public string Invoice_File { get { return this._sInvoice_File; } set { this._sInvoice_File = value; } }
        public DateTime DateFees { get { return this._dFees; } set { this._dFees = value; } }
        public int Invoice_Type { get { return this._iInvoice_Type; } set { this._iInvoice_Type = value; } }
        public string Statement_File { get { return this._sStatement_File; } set { this._sStatement_File = value; } }
        public string Misc_File { get { return this._sMisc_File; } set { this._sMisc_File = value; } }
        public string OfficialInformingDate { get { return this._sOfficialInformingDate; } set { this._sOfficialInformingDate = value; } }
        public string Notes { get { return this._sNotes; } set { this._sNotes = value; } }
        public string Invoice_External { get { return this._sInvoice_External; } set { this._sInvoice_External = value; } }
        public int Status { get { return this._iStatus; } set { this._iStatus = value; } }
        public int User_ID { get { return this._iUser_ID; } set { this._iUser_ID = value; } }
        public DateTime DateEdit { get { return this._dEdit; } set { this._dEdit = value; } }
        public int Year { get { return this._iYear; } set { this._iYear = value; } }
        public int Quart { get { return this._iQuart; } set { this._iQuart = value; } }
        public int ServiceProvider_ID { get { return this._iServiceProvider_ID; } set { this._iServiceProvider_ID = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}