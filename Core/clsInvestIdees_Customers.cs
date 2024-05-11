using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsInvestIdees_Customers
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int       _iRecord_ID;
        private int       _iII_ID;
        private int       _iClient_ID;      
        private int       _iContract_ID;
        private int       _iContract_Details_ID;
        private int       _iContract_Packages_ID;
        private int       _iStockCompany_ID;
        private string    _sCode;
        private string    _sPortfolio;

        private string    _sClientName;
        private string    _sMobile;
        private string    _sEmail;
        private int       _iAdvisor_ID;
        private string    _sAdvisorName;
        private string    _sAdvisorEmail;
        private string    _sAdvisorEmail_Username;
        private string    _sAdvisorEmail_Password;
        private string    _sAdvisorMobile;
        private string    _sAuthorEmail;
        private string    _sAuthorEmail_Username;
        private string    _sAuthorEmail_Password;
        private string    _sAuthorMobile;
        private int       _iAuthor_ID;
        private int       _iAuthor_Status;
        private DataTable _dtList;

        public clsInvestIdees_Customers()
        {
            this._iRecord_ID = 0;
            this._iII_ID = 0;
            this._iClient_ID = 0;
            this._iContract_ID = 0;
            this._iContract_Details_ID = 0;
            this._iContract_Packages_ID = 0;
            this._iStockCompany_ID = 0;            
            this._sCode = "";
            this._sPortfolio = "";
            this._sClientName = "";
            this._sMobile = "";
            this._sEmail = "";
            this._iAdvisor_ID = 0;
            this._sAdvisorName = "";
            this._sAdvisorEmail = "";
            this._sAdvisorEmail_Username = "";
            this._sAdvisorEmail_Password = "";
            this._sAdvisorMobile = "";
            this._sAuthorEmail = "";
            this._sAuthorEmail_Username = "";
            this._sAuthorEmail_Password = "";
            this._sAuthorMobile = "";
            this._iAuthor_ID = 0;
            this._iAuthor_Status = 0;
        }
        public void GetRecord()
        {
            try
            {
                _dtList = new DataTable("InvestIdees_Customers_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("II_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_Details_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_Packages_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("StockCompany_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientFullName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("MIFIDCategory_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("EMail", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Mobile", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Advisor_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AdvisorName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AdvisorTel", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AdvisorMobile", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AdvisorEMail", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CostBenefits", System.Type.GetType("System.String"));

                conn.Open();
                cmd = new SqlCommand("GetInvestIdees_Customers", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@II_ID", _iII_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["Client_ID"] = drList["Client_ID"];
                    dtRow["Contract_ID"] = drList["ClientPackage_ID"];
                    dtRow["Contract_Details_ID"] = drList["Contract_Details_ID"];
                    dtRow["Contract_Packages_ID"] = drList["Contract_Packages_ID"];
                    dtRow["StockCompany_ID"] = drList["StockCompany_ID"];
                    dtRow["Code"] = drList["Code"];
                    dtRow["Portfolio"] = drList["SubCode"] + "";
                    dtRow["ContractTitle"] = drList["ContractTitle"];

                    if (Convert.ToInt32(drList["Tipos"]) == 1) dtRow["ClientFullName"] = (drList["Surname"] + " " + drList["Firstname"]).Trim();
                    else dtRow["ClientFullName"] = drList["Surname"] + "";

                    dtRow["MIFIDCategory_ID"] = drList["MIFIDCategory_ID"];
                    dtRow["EMail"] = drList["EMail"];
                    dtRow["Mobile"] = drList["Mobile"];
                    dtRow["StockCompany_ID"] = drList["StockCompany_ID"];
                    dtRow["Advisor_ID"] = drList["AdvisorID"];
                    dtRow["AdvisorName"] = (drList["AdvisorSurname"] + " " + drList["AdvisorFirstname"]).Trim();
                    dtRow["AdvisorTel"] = drList["AdvisorTel"];
                    dtRow["AdvisorMobile"] = drList["AdvisorMobile"];
                    dtRow["AdvisorEMail"] = drList["AdvisorEMail"];
                    dtRow["CostBenefits"] = drList["CostBenefits"] + "";

                    _dtList.Rows.Add(dtRow);
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
                conn.Open();
                cmd = new SqlCommand("GetInvestIdees_Customers", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@II_ID", this._iII_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iII_ID = Convert.ToInt32(drList["II_ID"]);
                    this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                    this._iContract_ID = Convert.ToInt32(drList["ClientPackage_ID"]);
                    this._iContract_Details_ID = Convert.ToInt32(drList["Contract_Details_ID"]);
                    this._iContract_Packages_ID = Convert.ToInt32(drList["Contract_Packages_ID"]);
                    this._iStockCompany_ID = Convert.ToInt32(drList["StockCompany_ID"]);
                    if (Convert.ToInt32(drList["ContractTipos"]) == 1) this._sClientName = drList["ContractTitle"] + "";    // drList["ContractTipos"] == 1 meas that contract is JOINT-, KEM-, KOINOS-
                    else if (Convert.ToInt32(drList["Tipos"]) == 2) this._sClientName = (drList["Surname"] + "").Trim();    // drList["Tipos"] == 2 means that Client is Company
                    else this._sClientName = (drList["Surname"] + " " + drList["Firstname"]).Trim();                        // it means that Client is Person
                    this._sCode = drList["Code"] + "";
                    this._sPortfolio = drList["SubCode"] + "";
                    this._sMobile = drList["Mobile"] + "";
                    this._sEmail = drList["EMail"] + "";
                    this._iAdvisor_ID = Convert.ToInt32(drList["AdvisorID"]);
                    this._sAdvisorName = (drList["AdvisorSurname"] + " " + drList["AdvisorFirstname"]).Trim();
                    this._sAdvisorEmail = drList["AdvisorEMail"] + "";
                    this._sAdvisorEmail_Username = drList["EMail_Username"] + "";
                    this._sAdvisorEmail_Password = drList["EMail_Password"] + "";
                    this._sAdvisorMobile = drList["AdvisorMobile"] + "";
                    this._iAuthor_ID = Convert.ToInt32(drList["Author_ID"]);
                    this._sAuthorEmail = drList["AuthorEMail"] + "";
                    this._sAuthorEmail_Username = drList["AuthorEMail_Username"] + "";
                    this._sAuthorEmail_Password = drList["AuthorEMail_Password"] + "";
                    this._sAuthorMobile = drList["AuthorMobile"] + "";
                    this._iAuthor_Status = Convert.ToInt32(drList["Author_Status"]);
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
                using (SqlCommand cmd = new SqlCommand("InsertInvestIdees_Customers", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@II_ID", SqlDbType.Int).Value = _iII_ID;                          
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@ClientPackage_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Details_ID", SqlDbType.Int).Value = _iContract_Details_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@StockCompany_ID", SqlDbType.Int).Value = _iStockCompany_ID;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 30).Value = _sCode;
                    cmd.Parameters.Add("@Subcode", SqlDbType.NVarChar, 50).Value = _sPortfolio;
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
                using (SqlCommand cmd = new SqlCommand("EditInvestIdees_Customers", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@II_ID", SqlDbType.Int).Value = _iII_ID;                          
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@ClientPackage_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Details_ID", SqlDbType.Int).Value = _iContract_Details_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@StockCompany_ID", SqlDbType.Int).Value = _iStockCompany_ID;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 30).Value = _sCode;
                    cmd.Parameters.Add("@Subcode", SqlDbType.NVarChar, 50).Value = _sPortfolio;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "InvestIdees_Customers";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }

        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int II_ID { get { return this._iII_ID; } set { this._iII_ID = value; } }
        public int Client_ID { get { return this._iClient_ID; } set { this._iClient_ID = value; } }
        public int Contract_ID { get { return this._iContract_ID; } set { this._iContract_ID = value; } }
        public int Contract_Details_ID { get { return this._iContract_Details_ID; } set { this._iContract_Details_ID = value; } }
        public int Contract_Packages_ID { get { return this._iContract_Packages_ID; } set { this._iContract_Packages_ID = value; } }
        public int StockCompany_ID { get { return this._iStockCompany_ID; } set { this._iStockCompany_ID = value; } }
        public string Code { get { return this._sCode; } set { this._sCode = value; } }
        public string Portfolio { get { return this._sPortfolio; } set { this._sPortfolio = value; } }        
        public string ClientName { get { return this._sClientName; } set { this._sClientName = value; } }
        public string Mobile { get { return this._sMobile; } set { this._sMobile = value; } }
        public string Email { get { return this._sEmail; } set { this._sEmail = value; } }
        public int Advisor_ID { get { return this._iAdvisor_ID; } set { this._iAdvisor_ID = value; } }
        public string AdvisorName  { get { return this._sAdvisorName; } set { this._sAdvisorName = value; } }
        public string AdvisorEmail { get { return this._sAdvisorEmail; } set { this._sAdvisorEmail = value; } }
        public string AdvisorEmail_Username { get { return this._sAdvisorEmail_Username; } set { this._sAdvisorEmail_Username = value; } }
        public string AdvisorEmail_Password { get { return this._sAdvisorEmail_Password; } set { this._sAdvisorEmail_Password = value; } }
        public string AdvisorMobile { get { return this._sAdvisorMobile; } set { this._sAdvisorMobile = value; } }
        public string AuthorEmail { get { return this._sAuthorEmail; } set { this._sAuthorEmail = value; } }
        public string AuthorEmail_Username { get { return this._sAuthorEmail_Username; } set { this._sAuthorEmail_Username = value; } }
        public string AuthorEmail_Password { get { return this._sAuthorEmail_Password; } set { this._sAuthorEmail_Password = value; } }
        public string AuthorMobile { get { return this._sAuthorMobile; } set { this._sAuthorMobile = value; } }
        public int Author_ID { get { return this._iAuthor_ID; } set { this._iAuthor_ID = value; } }
        public int Author_Status { get { return this._iAuthor_Status; } set { this._iAuthor_Status = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}






