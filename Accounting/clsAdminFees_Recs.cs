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
        IDataReader drList;
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
        private int      _iCFP_ID;
        private float    _fltAssetValue;
        private double   _fltAUM;
        private int      _iDays;
        private float   _fltAmoiviPro;
        private float   _fltAxiaPro;
        private string   _sAmoivi;
        private float    _fltFeesPercent;
        private float    _fltAxiaAfter;
        private float    _fltStartAmount;
        private float    _fltDiscount_Percent1;
        private float    _fltDiscount_Amount1;
        private float    _fltDiscount_Percent2;
        private float    _fltDiscount_Amount2;
        private float    _fltDiscount_Percent;
        private float    _fltDiscount_Amount;
        private double   _fltNewAmount;
        private float    _fltMinAmoivi;
        private float    _fltMinAmoivi_Percent;
        private decimal   _decFinishMinAmoivi;
        private decimal   _decLastAmount;
        private float    _decLastAmount_Percent;
        private float    _fltVAT_Percent;
        private float    _fltVAT_Amount;
        private decimal   _decFinishAmount;
        private string   _sAnalyze;
        private int      _iService_ID;
        private int      _iInvoice_ID;
        private string   _sInvoice_Num;
        private string   _sInvoice_File;
        private DateTime _dFees;
        private int      _iInvoice_Type;
        private string   _sStatement_File;
        private string   _sMisc_File;
        private string   _sOfficialInformingDate;
        private string   _sNotes;
        private int      _iTipos;
        private string   _sInvoice_External;
        private string   _sNotes_Logistic;
        private string   _sNotes_Advisor;
        private string   _sNotes_Cheef;
        private int      _iStatus;
        private string   _sDescription_Bottom;
        private string   _sParent_Seira;
        private int      _iUser_ID;
        private DateTime _dEdit;

        private DateTime _dDateFrom;
        private DateTime _dDateTo;
        private int _iServiceProvider_ID;
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
            this._iCFP_ID = 0;
            this._fltAssetValue = 0;
            this._fltAUM = 0;
            this._iDays = 0;
            this._fltAmoiviPro = 0;
            this._fltAxiaPro = 0;
            this._sAmoivi = "";
            this._fltFeesPercent = 0;
            this._fltAxiaAfter = 0;
            this._fltStartAmount = 0;
            this._fltDiscount_Percent1 = 0;
            this._fltDiscount_Amount1 = 0;
            this._fltDiscount_Percent2 = 0;
            this._fltDiscount_Amount2 = 0;
            this._fltDiscount_Percent = 0;
            this._fltDiscount_Amount = 0;
            this._fltNewAmount = 0;
            this._fltMinAmoivi = 0;
            this._fltMinAmoivi_Percent = 0;
            this._decFinishMinAmoivi = 0;
            this._decLastAmount = 0;
            this._decLastAmount_Percent = 0;
            this._fltVAT_Percent = 0;
            this._fltVAT_Amount = 0;
            this._decFinishAmount = 0;
            this._sAnalyze = "";
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
            this._iTipos = 0;
            this._sInvoice_External = "";
            this._sNotes_Logistic = "";
            this._sNotes_Advisor = "";
            this._sNotes_Cheef = "";
            this._iStatus = 0;
            this._sDescription_Bottom = "";
            this._sParent_Seira = "";
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
                    this._iStatus = Convert.ToInt32(drList["Status"]);
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
                _dtList = new DataTable("AdminFees_List");
                dtCol = _dtList.Columns.Add("ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AT_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ImageType", Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("DateFrom", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("DateTo", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("ContractTitle", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Code", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Portfolio", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Currency", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Package", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AUM", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Days", Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("AmoiviPro", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AxiaPro", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AmoiviAfter", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AxiaAfter", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Climakas", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("MinAmoivi", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MinAmoivi_Percent", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MinAmoivi_Percent2", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MinAmoivi_Contract", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishMinAmoivi", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("LastAmount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("VAT_Amount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishAmount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("LastAmount_Percent", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Invoice_Num", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("MaxDays", Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("AverageAUM", Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("Weights", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MinYearly", Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("DateFees", Type.GetType("System.DateTime"));
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
                dtCol = _dtList.Columns.Add("PostAddress", Type.GetType("System.String"));
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
                dtCol = _dtList.Columns.Add("Description_Bottom", Type.GetType("System.String"));
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
                dtCol = _dtList.Columns.Add("ContractDateStart", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("Parent_Seira", Type.GetType("System.String"));

                conn.Open();
                cmd = new SqlCommand("GetAdminFees_OldList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AT_ID", _iAT_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ImageType"] = (drList["Invoice_File"] + "" == "") ? 0 : 1;
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
                    dtRow["AmoiviAfter"] = drList["FeesPercent"];
                    dtRow["AxiaAfter"] = drList["AxiaAfter"];
                    dtRow["Discount_Percent1"] = drList["Discount_Percent1"];
                    dtRow["Discount_Amount1"] = drList["Discount_Amount1"];
                    dtRow["Discount_Percent2"] = drList["Discount_Percent2"];
                    dtRow["Discount_Amount2"] = drList["Discount_Amount2"];
                    dtRow["Discount_Percent"] = drList["Discount_Percent"];
                    dtRow["Discount_Amount"] = drList["Discount_Amount"];
                    dtRow["Climakas"] = drList["Climakas"] + "";
                    dtRow["MinAmoivi"] = drList["MinAmoivi"];
                    dtRow["MinAmoivi_Percent"] = drList["MinAmoivi_Percent"];
                    dtRow["MinAmoivi_Percent2"] = drList["MinAmoivi_Percent2"];
                    dtRow["MinAmoivi_Contract"] = drList["MinAmoivi_Contract"];
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
                    dtRow["User1_Name"] = drList["Surname"] + " " + drList["Firstname"];
                    dtRow["Address"] = drList["Address"] + "";
                    dtRow["City"] = drList["City"] + "";
                    dtRow["Zip"] = drList["ZIP"] + "";
                    dtRow["Country"] = drList["Country_Title"] + "";
                    dtRow["CountryEnglish"] = drList["Country_TitleEn"] + "";
                    dtRow["PostAddress"] = drList["Address"] + "~" + drList["City"] + "~" + drList["ZIP"];
                    dtRow["Invoice_Num"] = drList["Invoice_Num"] + "";
                    dtRow["FileName"] = drList["Invoice_File"] + "";
                    dtRow["DateFees"] = drList["DateFees"];
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
                    dtRow["Invoice_External"] = drList["Invoice_External"];
                    //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                    if (Convert.ToInt32(dtRow["Invoice_Type"]) == 0)
                    {
                        switch (Convert.ToInt32(drList["ClientTipos"]))
                        {
                            case 1:
                                dtRow["Invoice_Type"] = 0; // iInvoiceFisiko;
                                break;
                            case 2:
                                dtRow["Invoice_Type"] = 0; // iInvoiceNomiko;
                                break;
                        }
                    }
                    //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

                    dtRow["L4"] = drList["L4"] + "";


                    dtRow["ContractTitle"] = drList["ContractTitle"] + "";
                    dtRow["Service_ID"] = drList["Service_ID"];
                    dtRow["Service_Title"] = drList["Service_Title"] + "";
                    dtRow["ContractDateStart"] = drList["DateStart"];
                    dtRow["Status"] = drList["Status"];
                    dtRow["Description_Bottom"] = drList["Description_Bottom"] + "";
                    dtRow["Parent_Seira"] = ""; // drList["Parent_Seira"] + "";
                    dtRow["User_ID"] = drList["User_ID"];
                    dtRow["ConnectionMethod"] = drList["ConnectionMethod"];
                    dtRow["Invoice_Arithmos"] = ""; // IIf(IsNumeric(drList["Invoice_Arithmos") + ""), drList["Invoice_Arithmos"), "0"];

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
                using (SqlCommand cmd = new SqlCommand("InsertAdminFees_Rec", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = _dFrom;
                    cmd.Parameters.Add("@Author_ID", SqlDbType.Int).Value = _iStatus;
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
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = _dFrom;
                    cmd.Parameters.Add("@Author_ID", SqlDbType.Int).Value = _iStatus;
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
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "AT_ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }

                using (SqlCommand cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ManagmentFees_Titles";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int Client_ID { get { return this._iClient_ID; } set { this._iClient_ID = value; } }
        public int AT_ID { get { return this._iAT_ID; } set { this._iAT_ID = value; } }
        public DateTime DateFrom { get { return this._dFrom; } set { this._dFrom = value; } }
        public DateTime DateTo { get { return this._dTo; } set { this._dTo = value; } }
        public string Code { get { return this._sCode; } set { this._sCode = value; } }
        public string Portfolio { get { return this._sPortfolio; } set { this._sPortfolio = value; } }
        public string Currency { get { return this._sCurrency; } set { this._sCurrency = value; } }
        public int Contract_ID { get { return this._iContract_ID; } set { this._iContract_ID = value; } }
        public int Contract_Details_ID { get { return this._iContract_Details_ID; } set { this._iContract_Details_ID = value; } }
        public int Contract_Packages_ID { get { return this._iContract_Packages_ID; } set { this._iContract_Packages_ID = value; } }
        public int Days { get { return this._iDays; } set { this._iDays = value; } }
        public float AmoiviPro { get { return this._fltAmoiviPro; } set { this._fltAmoiviPro = value; } }
        public float AxiaPro { get { return this._fltAxiaPro; } set { this._fltAxiaPro = value; } }
        public float AxiaAfter { get { return this._fltAxiaAfter; } set { this._fltAxiaAfter = value; } }
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
        public float LastAmount_Percent { get { return this._decLastAmount_Percent; } set { this._decLastAmount_Percent = value; } }
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
        public int ServiceProvider_ID { get { return this._iServiceProvider_ID; } set { this._iServiceProvider_ID = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}