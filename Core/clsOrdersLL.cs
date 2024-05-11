using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsOrdersLL
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;
 
        private int _iRecord_ID;
        private int _iStockCompany_ID;
        private int _iClient_ID;
        private int _iContract_ID;
        private int _iContract_Details_ID;
        private int _iContract_Packages_ID;
        private string _sCode;
        private string _sPortfolio;
        private DateTime _dAktionDate;
        private int _iCashAccount_ID;
        private float _fltAmount;
        private string _sCurr;
        private float _fltLTV;
        private float _fltLL_AS;
        private float _fltProviderRate;
        private float _fltAdditionalRate;
        private float _fltDiscount;
        private float _fltFinalMargin;
        private float _fltGrossRate;
        private DateTime _dPeriodStart;
        private DateTime _dPeriodEnd;
        private int _iDays;
        private decimal _decCurrRate;
        private float _fltBasicFees;
        private DateTime _dRecieveDate;
        private int _iRecieveMethod_ID;
        private DateTime _dSentDate;
        private DateTime _dExecuteDate;   
        private string _sNotes;
        private int _iUser_ID;
        private DateTime _dDateIns;
        private int _iStatus;
        private float _fltCompanyFeesPercent;

        private int _iClientTipos;
        private string _sClientFullName;
        private string _sContractTitle;
        private string _sStockCompany_Title;
        private string _sMainCurr;
        private int _iSent;
        private int _iActions;
        private int _iUser1_ID;
        private int _iUser3_ID;
        private DateTime _dDateFrom;
        private DateTime _dDateTo;
        private DataTable _dtList;

        public clsOrdersLL()
        {
            this._iRecord_ID = 0;
            this._iStockCompany_ID = 0;
            this._iClient_ID = 0;
            this._iContract_ID = 0;
            this._iContract_Details_ID = 0;
            this._iContract_Packages_ID = 0;
            this._sCode = "";
            this._sPortfolio = "";
            this._dAktionDate = Convert.ToDateTime("1900/01/01");
            this._iCashAccount_ID = 0;
            this._fltAmount = 0;
            this._sCurr = "";
            this._fltLTV = 0;
            this._fltLL_AS = 0;
            this._fltProviderRate = 0;
            this._fltAdditionalRate = 0;
            this._fltDiscount = 0;
            this._fltFinalMargin = 0;
            this._fltGrossRate = 0;
            this._dPeriodStart = Convert.ToDateTime("1900/01/01");
            this._dPeriodEnd = Convert.ToDateTime("1900/01/01");
            this._iDays = 0;
            this._decCurrRate = 0;
            this._fltBasicFees = 0;
            this._dRecieveDate = Convert.ToDateTime("1900/01/01");
            this._iRecieveMethod_ID = 0;
            this._dSentDate = Convert.ToDateTime("1900/01/01");
            this._dExecuteDate = Convert.ToDateTime("1900/01/01");
            this._sNotes = "";
            this._iUser_ID = 0;
            this._dDateIns = Convert.ToDateTime("1900/01/01");
            this._iStatus = 0;
            this._fltCompanyFeesPercent = 0;

            this._iClientTipos = 0;
            this._sClientFullName = "";
            this._sContractTitle = "";
            this._sStockCompany_Title = "";
            this._sMainCurr = "";
            this._iSent = 0;
            this._iActions = 0;
            this._iUser1_ID = 0;
            this._iUser3_ID = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("sp_GetCommandLL", conn);  
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", this._iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {  
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iStockCompany_ID = Convert.ToInt32(drList["StockCompany_ID"]);
                    this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                    this._iContract_ID = Convert.ToInt32(drList["ClientPackage_ID"]);
                    this._iContract_Details_ID = Convert.ToInt32(drList["Contract_Details_ID"]);
                    this._iContract_Packages_ID = Convert.ToInt32(drList["Contract_Packages_ID"]);
                    this._sCode = drList["Code"] + "";
                    this._sPortfolio = drList["ProfitCenter"] + "";
                    this._dAktionDate = Convert.ToDateTime(drList["AktionDate"]);
                    this._iCashAccount_ID = Convert.ToInt32(drList["CashAccount_ID"]);
                    this._fltAmount = Convert.ToSingle(drList["Amount"]);
                    this._sCurr = drList["Curr"] + "";
                    this._fltLTV = Convert.ToSingle(drList["LTV"]);
                    this._fltLL_AS = Convert.ToSingle(drList["LL_AS"]);
                    this._fltProviderRate = Convert.ToSingle(drList["ProviderRate"]);
                    this._fltAdditionalRate = Convert.ToSingle(drList["AdditionalRate"]);
                    this._fltDiscount = Convert.ToSingle(drList["Discount"]);
                    this._fltFinalMargin = Convert.ToSingle(drList["FinalMargin"]);
                    this._fltGrossRate = Convert.ToSingle(drList["GrossRate"]);
                    this._dPeriodStart = Convert.ToDateTime(drList["PeriodStart"]);
                    this._dPeriodEnd = Convert.ToDateTime(drList["PeriodEnd"]);
                    this._iDays = Convert.ToInt32(drList["Days"]);
                    this._decCurrRate = Convert.ToDecimal(drList["CurrRate"]);
                    this._fltBasicFees = Convert.ToSingle(drList["BasicFees"]);
                    this._dRecieveDate = Convert.ToDateTime(drList["RecieveDate"]);
                    this._iRecieveMethod_ID = Convert.ToInt32(drList["RecieveMethod_ID"]);
                    this._dSentDate = Convert.ToDateTime(drList["SentDate"]);
                    this._dExecuteDate = Convert.ToDateTime(drList["ExecuteDate"]);
                    this._sNotes = drList["Notes"] + "";
                    this._iUser_ID = Convert.ToInt32(drList["User_ID"]);
                    this._dDateIns = Convert.ToDateTime(drList["DateIns"]);
                    this._iStatus = Convert.ToInt32(drList["Status"]);
                    this._fltCompanyFeesPercent = Convert.ToSingle(drList["CompanyFeesPercent"]);

                    this._iClientTipos = Convert.ToInt32(drList["ClientTipos"]);
                    this._sClientFullName = "";
                    if (Convert.ToInt32(drList["ClientTipos"]) == 1) this._sClientFullName = (drList["ClientSurname"] + " " + drList["ClientFirstname"]).Trim();
                    else this._sClientFullName = (drList["ClientSurname"]+"").Trim();

                    this._sContractTitle = drList["ContractTitle"] + "";
                    this._sStockCompany_Title = drList["ServiceProvider_Title"] + "";

                    this._sContractTitle = drList["ContractTitle"] + "";
                    this._sMainCurr = drList["MainCurr"] + "";
                    this._iSent = 0;
                    this._iActions = 0;
                    this._iUser1_ID = 0;
                    this._iUser3_ID = 0;

                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            int iOld_ID = -999;
            try
            {
                _dtList = new DataTable("OrdersLL_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("StockCompany_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("StockCompany_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ClientName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Contract_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contracts_Details_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contracts_Packages_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AktionDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("CashAccount_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("AccountNumber", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Amount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Curr", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("LTV", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("LL_AS", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ProviderRate", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AdditionalRate", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Discount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinalMargin", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("GrossRate", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("PeriodStart", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("PeriodEnd", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("Days", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("CurrRate", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("BasicFees", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RecieveDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("RecieveMethod_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("SentDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("ExecuteDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("AuthorName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("User_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DateIns", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("CompanyFeesPercent", System.Type.GetType("System.Single"));

                conn.Open();
                cmd = new SqlCommand("GetCommandsLL_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dDateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dDateTo));
                cmd.Parameters.Add(new SqlParameter("@StockCompany_ID", _iStockCompany_ID));
                cmd.Parameters.Add(new SqlParameter("@Sent", _iSent));
                cmd.Parameters.Add(new SqlParameter("@Actions", _iActions));
                cmd.Parameters.Add(new SqlParameter("@User1_ID", _iUser1_ID));
                cmd.Parameters.Add(new SqlParameter("@User3_ID", _iUser3_ID));
                cmd.Parameters.Add(new SqlParameter("@ClientCode", _sCode));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    if (iOld_ID != Convert.ToInt32(drList["ID"])) {
                        iOld_ID = Convert.ToInt32(drList["ID"]);

                        dtRow = _dtList.NewRow();
                        dtRow["ID"] = drList["ID"];
                        dtRow["Notes"] = drList["Notes"] + "";
                        dtRow["User_ID"] = drList["User_ID"];
                        dtRow["Client_ID"] = drList["Client_ID"];
                        if (Convert.ToInt32(drList["Tipos"]) == 1) dtRow["ClientName"] = drList["Surname"] + " " + drList["Firstname"];
                        else dtRow["ClientName"] = drList["Surname"];
                        dtRow["Code"] = drList["Code"];
                        dtRow["Portfolio"] = drList["ProfitCenter"];
                        dtRow["Contract_ID"] = drList["ClientPackage_ID"];
                        dtRow["ContractTitle"] = drList["ContractTitle"];
                        dtRow["StockCompany_ID"] = drList["StockCompany_ID"];
                        dtRow["StockCompany_Title"] = drList["StockCompany_Title"];
                        dtRow["AccountNumber"] = drList["AccountNumber"];
                        dtRow["Amount"] = drList["Amount"];
                        dtRow["Curr"] = drList["Curr"];
                        dtRow["LTV"] = drList["LTV"];
                        dtRow["LL_AS"] = drList["LL_AS"];
                        dtRow["ProviderRate"] = drList["ProviderRate"] + "";
                        dtRow["AdditionalRate"] = drList["AdditionalRate"] + "";
                        dtRow["Discount"] = drList["Discount"] + "";
                        dtRow["FinalMargin"] = drList["FinalMargin"] + "";
                        dtRow["GrossRate"] = drList["GrossRate"] + "";
                        dtRow["PeriodStart"] = drList["PeriodStart"] + "";
                        dtRow["PeriodEnd"] = drList["PeriodEnd"] + "";
                        dtRow["Days"] = drList["Days"] + "";
                        dtRow["ExecuteDate"] = drList["ExecuteDate"];
                        dtRow["AuthorName"] = (drList["AuthorSurname"] + " " + drList["AuthorFirstname"]).Trim();
                        dtRow["StockCompany_ID"] = drList["StockCompany_ID"];
                        dtRow["Status"] = drList["Status"];
                        dtRow["DateIns"] = drList["DateIns"];
                        dtRow["User_ID"] = drList["User_ID"];
                        _dtList.Rows.Add(dtRow);
                    }
                }
                drList.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally { conn.Close(); }
        }      
        public void GetChecks()
        {
            try
            {
                _dtList = new DataTable("CommandsLLCheckList");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DateIns", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("UserName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("ProblemType_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ProblemType_ID", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("FileName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ReversalRequestDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("User_ID", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("sp_GetCommandsLL_Check", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];
                    this.dtRow["DateIns"] = drList["DateIns"] + "";
                    this.dtRow["UserName"] = (drList["Surname"] + " " + drList["Firstname"]).Trim();
                    this.dtRow["Status"] = Convert.ToInt16(drList["Status"]);
                    this.dtRow["ProblemType_Title"] = drList["ProblemType_Title"] + "";
                    this.dtRow["ProblemType_ID"] = Convert.ToInt16(drList["ProblemType_ID"]);
                    this.dtRow["Notes"] = drList["Notes"] + "";
                    this.dtRow["FileName"] = drList["FileName"] + "";
                    this.dtRow["ReversalRequestDate"] = drList["ReversalRequestDate"] + "";
                    this.dtRow["User_ID"] = Convert.ToInt32(drList["User_ID"]);
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetRecievedFiles()
        {
            try
            {
                _dtList = new DataTable("CommandsLLRecievedFilesList");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DateIns", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Method_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("FileName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Method_ID", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("sp_GetCommandsLLRecieved", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];
                    this.dtRow["DateIns"] = Convert.ToDateTime(drList["DateIns"]).ToString("dd/MM/yyyy HH:mm:ss");
                    this.dtRow["Method_Title"] = drList["Title"] + "";
                    this.dtRow["FileName"] = drList["FileName"] + "";
                    this.dtRow["Method_ID"] = Convert.ToInt32(drList["Method_ID"]);
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int InsertRecord()
        {
            _iRecord_ID = 0;
            try
            {
                conn.Open();
                using (cmd = new SqlCommand("InsertCommandLL", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@StockCompany_ID", SqlDbType.Int).Value = _iStockCompany_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Details_ID", SqlDbType.Int).Value = _iContract_Details_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 30).Value = _sCode;
                    cmd.Parameters.Add("@Portfolio", SqlDbType.NVarChar, 50).Value = _sPortfolio;
                    cmd.Parameters.Add("@AktionDate", SqlDbType.DateTime).Value = _dAktionDate;
                    cmd.Parameters.Add("@CashAccount_ID", SqlDbType.Int).Value = _iCashAccount_ID;
                    cmd.Parameters.Add("@Amount", SqlDbType.Float).Value = _fltAmount;
                    cmd.Parameters.Add("@Curr", SqlDbType.NVarChar, 6).Value = _sCurr;
                    cmd.Parameters.Add("@LTV", SqlDbType.Float).Value = _fltLTV;
                    cmd.Parameters.Add("@LL_AS", SqlDbType.Float).Value = _fltLL_AS;
                    cmd.Parameters.Add("@ProviderRate", SqlDbType.Float).Value = _fltProviderRate;
                    cmd.Parameters.Add("@AdditionalRate", SqlDbType.Float).Value = _fltAdditionalRate;
                    cmd.Parameters.Add("@Discount", SqlDbType.Float).Value = _fltDiscount;
                    cmd.Parameters.Add("@FinalMargin", SqlDbType.Float).Value = _fltFinalMargin;
                    cmd.Parameters.Add("@GrossRate", SqlDbType.Float).Value = _fltGrossRate;
                    cmd.Parameters.Add("@PeriodStart", SqlDbType.DateTime).Value = _dPeriodStart;
                    cmd.Parameters.Add("@PeriodEnd", SqlDbType.DateTime).Value = _dPeriodEnd;
                    cmd.Parameters.Add("@Days", SqlDbType.Int).Value = _iDays;
                    cmd.Parameters.Add("@CurrRate", SqlDbType.Decimal).Value = _decCurrRate;
                    cmd.Parameters.Add("@BasicFees", SqlDbType.Float).Value = _fltBasicFees;
                    cmd.Parameters.Add("@RecieveDate", SqlDbType.DateTime).Value = _dRecieveDate;
                    cmd.Parameters.Add("@RecieveMethod_ID", SqlDbType.Int).Value = _iRecieveMethod_ID;
                    cmd.Parameters.Add("@SentDate", SqlDbType.DateTime).Value = _dSentDate;
                    cmd.Parameters.Add("@ExecuteDate", SqlDbType.DateTime).Value = _dExecuteDate;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 1000).Value = _sNotes;
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = _iUser_ID;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = _dDateIns;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.Parameters.Add("@CompanyFeesPercent", SqlDbType.Float).Value = _fltCompanyFeesPercent;

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
                using (cmd = new SqlCommand("EditCommandLL", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@AktionDate", SqlDbType.DateTime).Value = _dAktionDate;
                    cmd.Parameters.Add("@CashAccount_ID", SqlDbType.Int).Value = _iCashAccount_ID;
                    cmd.Parameters.Add("@Amount", SqlDbType.Float).Value = _fltAmount;
                    cmd.Parameters.Add("@Curr", SqlDbType.NVarChar, 6).Value = _sCurr;
                    cmd.Parameters.Add("@LTV", SqlDbType.Float).Value = _fltLTV;
                    cmd.Parameters.Add("@LL_AS", SqlDbType.Float).Value = _fltLL_AS;
                    cmd.Parameters.Add("@ProviderRate", SqlDbType.Float).Value = _fltProviderRate;
                    cmd.Parameters.Add("@AdditionalRate", SqlDbType.Float).Value = _fltAdditionalRate;
                    cmd.Parameters.Add("@Discount", SqlDbType.Float).Value = _fltDiscount;
                    cmd.Parameters.Add("@FinalMargin", SqlDbType.Float).Value = _fltFinalMargin;
                    cmd.Parameters.Add("@GrossRate", SqlDbType.Float).Value = _fltGrossRate;
                    cmd.Parameters.Add("@PeriodStart", SqlDbType.DateTime).Value = _dPeriodStart;
                    cmd.Parameters.Add("@PeriodEnd", SqlDbType.DateTime).Value = _dPeriodEnd;
                    cmd.Parameters.Add("@Days", SqlDbType.Int).Value = _iDays;
                    cmd.Parameters.Add("@CurrRate", SqlDbType.Decimal).Value = _decCurrRate;
                    cmd.Parameters.Add("@BasicFees", SqlDbType.Float).Value = _fltBasicFees;
                    cmd.Parameters.Add("@RecieveDate", SqlDbType.DateTime).Value = _dRecieveDate;
                    cmd.Parameters.Add("@RecieveMethod_ID", SqlDbType.Int).Value = _iRecieveMethod_ID;
                    cmd.Parameters.Add("@SentDate", SqlDbType.DateTime).Value = _dSentDate;
                    cmd.Parameters.Add("@ExecuteDate", SqlDbType.DateTime).Value = _dExecuteDate;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 1000).Value = _sNotes;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = _dDateIns;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.Parameters.Add("@CompanyFeesPercent", SqlDbType.Float).Value = _fltCompanyFeesPercent;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }

        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int StockCompany_ID { get { return this._iStockCompany_ID; } set { this._iStockCompany_ID = value; } }
        public int Client_ID { get { return this._iClient_ID; } set { this._iClient_ID = value; } }
        public int Contract_ID { get { return this._iContract_ID; } set { this._iContract_ID = value; } }
        public int Contract_Details_ID { get { return this._iContract_Details_ID; } set { this._iContract_Details_ID = value; } }
        public int Contract_Packages_ID { get { return this._iContract_Packages_ID; } set { this._iContract_Packages_ID = value; } }
        public string Code { get { return this._sCode; } set { this._sCode = value; } }
        public string Portfolio { get { return this._sPortfolio; } set { this._sPortfolio = value; } }
        public DateTime AktionDate { get { return this._dAktionDate; } set { this._dAktionDate = value; } }
        public int CashAccount_ID { get { return this._iCashAccount_ID; } set { this._iCashAccount_ID = value; } }
        public float Amount { get { return this._fltAmount; } set { this._fltAmount = value; } }
        public string Curr { get { return this._sCurr; } set { this._sCurr = value; } }
        public float LTV { get { return this._fltLTV; } set { this._fltLTV = value; } }
        public float LL_AS { get { return this._fltLL_AS; } set { this._fltLL_AS = value; } }
        public float ProviderRate { get { return this._fltProviderRate; } set { this._fltProviderRate = value; } }
        public float AdditionalRate { get { return this._fltAdditionalRate; } set { this._fltAdditionalRate = value; } }
        public float Discount { get { return this._fltDiscount; } set { this._fltDiscount = value; } }
        public float FinalMargin { get { return this._fltFinalMargin; } set { this._fltFinalMargin = value; } }
        public float GrossRate { get { return this._fltGrossRate; } set { this._fltGrossRate = value; } }
        public DateTime PeriodStart { get { return this._dPeriodStart; } set { this._dPeriodStart = value; } }
        public DateTime PeriodEnd { get { return this._dPeriodEnd; } set { this._dPeriodEnd = value; } }
        public int Days { get { return this._iDays; } set { this._iDays = value; } }
        public decimal CurrRate { get { return this._decCurrRate; } set { this._decCurrRate = value; } }
        public float BasicFees { get { return this._fltBasicFees; } set { this._fltBasicFees = value; } }
        public DateTime RecieveDate { get { return this._dRecieveDate; } set { this._dRecieveDate = value; } }
        public int RecieveMethod_ID { get { return this._iRecieveMethod_ID; } set { this._iRecieveMethod_ID = value; } }
        public DateTime SentDate { get { return this._dSentDate; } set { this._dSentDate = value; } }
        public DateTime ExecuteDate { get { return this._dExecuteDate; } set { this._dExecuteDate = value; } }
        public string Notes { get { return this._sNotes; } set { this._sNotes = value; } }
        public int User_ID { get { return this._iUser_ID; } set { this._iUser_ID = value; } }
        public DateTime DateIns { get { return this._dDateIns; } set { this._dDateIns = value; } }
        public int Status { get { return this._iStatus; } set { this._iStatus = value; } }
        public float CompanyFeesPercent { get { return this._fltCompanyFeesPercent; } set { this._fltCompanyFeesPercent = value; } }
        public int ClientTipos { get { return this._iClientTipos; } set { this._iClientTipos = value; } }
        public string ClientFullName { get { return this._sClientFullName; } set { this._sClientFullName = value; } }
        public string ContractTitle { get { return this._sContractTitle; } set { this._sContractTitle = value; } }
        public string StockCompany_Title { get { return this._sStockCompany_Title; } set { this._sStockCompany_Title = value; } }
        public string MainCurr { get { return this._sMainCurr; } set { this._sMainCurr = value; } }
        public int Sent { get { return this._iSent; } set { this._iSent = value; } }
        public int Actions { get { return this._iActions; } set { this._iActions = value; } }
        public int User1_ID { get { return this._iUser1_ID; } set { this._iUser1_ID = value; } }
        public int User3_ID { get { return this._iUser3_ID; } set { this._iUser3_ID = value; } } 
        public DateTime DateFrom { get { return this._dDateFrom; } set { this._dDateFrom = value; } }
        public DateTime DateTo { get { return this._dDateTo; } set { this._dDateTo = value; } } 
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}