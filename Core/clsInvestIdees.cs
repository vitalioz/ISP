using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsInvestIdees
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int      _iRecord_ID;
        private int      _iAdvisor_ID;
        private int      _iUser_ID;
        private int      _iSendMethod;
        private int      _iDescription_ID;
        private DateTime _dAktionDate;
        private int      _iClient_ID;
        private int      _iCC_ID;
        private float    _fltAUM;
        private string   _sCurrency;
        private string   _sProducts;
        private string   _sIdeasText;
        private string   _sCostBenefits;
        private string   _sStatementFile;
        private string   _sProposalPDFile;
        private int      _iAttachedFilesCount;
        private int      _iUploadedFilesCount;
        private int      _iRemotedFilesCount;
        private string   _sUploadStartTime;
        private string   _sUploadFinishTime;
        private DateTime _dSentDate;
        private DateTime _dRecievedDate;
        private DateTime _dRTODate;
        private int      _iRecievedOrder;
        private int      _iSendAttemptsCount;
        private string   _sSendMessage;
        private int      _iStatus;
        private string   _sNotes;
        private int      _iLineStatus;
        private string   _sWebPassword;

        private string   _sCode;
        private string   _sISIN;
        private string   _sCC_EMail;
        private string   _sAdvisorName;
        private string   _sUserName;
        private int      _iContract_ID;
        private int      _iDivision_ID;
        private DateTime _dDateFrom;
        private DateTime _dDateTo;
        private DataTable _dtList;

        public clsInvestIdees()
        {
            this._iRecord_ID = 0;
            this._iAdvisor_ID = 0;
            this._iUser_ID = 0;
            this._iSendMethod = 0;
            this._iDescription_ID = 0;
            this._dAktionDate = Convert.ToDateTime("1900/01/01");
            this._iClient_ID = 0;
            this._iCC_ID = 0;
            this._fltAUM = 0;
            this._sCurrency = "";
            this._sProducts = "";
            this._sIdeasText = "";
            this._sCostBenefits = "";
            this._sStatementFile = "";
            this._sProposalPDFile = "";
            this._iAttachedFilesCount = 0;
            this._iUploadedFilesCount = 0;
            this._iRemotedFilesCount = 0;
            this._sUploadStartTime = "";
            this._sUploadFinishTime = "";
            this._dSentDate = Convert.ToDateTime("1900/01/01");
            this._dRecievedDate = Convert.ToDateTime("1900/01/01");
            this._dRTODate = Convert.ToDateTime("1900/01/01");
            this._iRecievedOrder = 0;
            this._iSendAttemptsCount = 0;
            this._sSendMessage = "";
            this._iStatus = 0;
            this._sNotes = "";
            this._iLineStatus = 0;
            this._sWebPassword = "";

            this._sCode = "";
            this._sISIN = "";
            this._sCC_EMail = "";
            this._sAdvisorName = "";
            this._sUserName = "";
            this._iContract_ID = 0;
            this._iDivision_ID = 0;
            this._dDateFrom = Convert.ToDateTime("1900/01/01");
            this._dDateTo = Convert.ToDateTime("1900/01/01");
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetInvestIdees_Data", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iAdvisor_ID = Convert.ToInt32(drList["Advisor_ID"]);
                    this._iUser_ID = Convert.ToInt32(drList["User_ID"]);
                    this._iSendMethod = Convert.ToInt32(drList["SendMethod"]);
                    this._iDescription_ID = Convert.ToInt32(drList["Description_ID"]);
                    this._dAktionDate = Convert.ToDateTime(drList["AktionDate"]);  
                    this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                    this._iCC_ID = Convert.ToInt32(drList["CC_ID"]);
                    this._sCC_EMail = drList["CC_EMail"] + ""; 
                    this._fltAUM = Convert.ToSingle(drList["AUMs"]);
                    this._sCurrency = drList["Currency"] + ""; 
                    this._sProducts = drList["Products"] + ""; 
                    this._sIdeasText = drList["IdeasText"] + ""; 
                    this._sCostBenefits = drList["CostBenefits"] + ""; 
                    this._sStatementFile = drList["StatementFile"] + ""; 
                    this._sProposalPDFile = drList["ProposalPDFile"] + ""; 
                    this._iAttachedFilesCount = Convert.ToInt32(drList["AttachedFilesCount"]);
                    this._iUploadedFilesCount = Convert.ToInt32(drList["UploadedFilesCount"]);
                    this._iRemotedFilesCount = Convert.ToInt32(drList["RemotedFilesCount"]); 
                    this._sUploadStartTime = drList["UploadStartTime"] + "";
                    this._sUploadFinishTime = drList["UploadFinishTime"] + "";
                    this._dSentDate = Convert.ToDateTime(drList["SentDate"]);
                    this._dRecievedDate = Convert.ToDateTime(drList["RecievedDate"]);
                    this._dRTODate = Convert.ToDateTime(drList["RTODate"]);
                    this._iRecievedOrder = Convert.ToInt32(drList["RecievedOrder"]);
                    this._iSendAttemptsCount = Convert.ToInt32(drList["SendAttemptsCount"]);
                    this._sSendMessage = drList["SendMessage"] + ""; 
                    this._iStatus = Convert.ToInt32(drList["Status"]);
                    this._iLineStatus = Convert.ToInt32(drList["LineStatus"]);
                    this._sNotes = drList["Notes"] + "";
                    this._sAdvisorName = (drList["AdvisorSurname"] + " " + drList["AdvisorFirstname"]).Trim();
                    this._sUserName = (drList["UserSurname"] + " " + drList["UserFirstname"]).Trim();
                    this._sWebPassword = drList["WebPassword"] + "";
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            string[] sZtatus = { "Νέα πρόταση", "Αναμονή αποστολής", "Στάλθηκε", "Δεν στάλθηκε", "Άκυρο" };

            try
            {
                _dtList = new DataTable("InvestProposals_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Status_Text", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("II_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AktionDate", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("Description_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientFullName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Products", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("EMail", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Mobile", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("InformationMethods_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AttachedFilesCount", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("UploadedFilesCount", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("RemotedFilesCount", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("SentDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RecievedDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RTODate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Advisor_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AdvisorName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("User_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("UserName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StatementFile", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ProposalPDFile", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("CC_Email", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("LineStatus", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("WebPassword", System.Type.GetType("System.String"));

                conn.Open();
                cmd = new SqlCommand("GetInvestIdees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Advisor_ID", _iAdvisor_ID));
                cmd.Parameters.Add(new SqlParameter("@User_ID", _iUser_ID));
                cmd.Parameters.Add(new SqlParameter("@Client_ID", _iClient_ID));
                cmd.Parameters.Add(new SqlParameter("@Division_ID", _iDivision_ID));
                cmd.Parameters.Add(new SqlParameter("@Contract_ID", _iContract_ID));                           
                cmd.Parameters.Add(new SqlParameter("@DateSend", _dSentDate));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];

                    this.dtRow["Status_Text"] = sZtatus[Convert.ToInt32(drList["Status"])];
                    this.dtRow["II_ID"] = drList["ID"];
                    this.dtRow["AktionDate"] = drList["AktionDate"];
                    this.dtRow["Description_Title"] = drList["Description_Title"] + "";
                    this.dtRow["ClientFullName"] = (drList["Surname"] + " " + drList["Firstname"]).Trim();
                    this.dtRow["ContractTitle"] = drList["ContractTitle"] + "";
                    this.dtRow["Products"] = drList["Products"];
                    this.dtRow["EMail"] = drList["EMail"];
                    this.dtRow["Mobile"] = drList["Mobile"];
                    this.dtRow["InformationMethods_Title"] = drList["InformationMethods_Title"];
                    this.dtRow["AttachedFilesCount"] = drList["AttachedFilesCount"];
                    this.dtRow["UploadedFilesCount"] = drList["UploadedFilesCount"];
                    this.dtRow["RemotedFilesCount"] = drList["RemotedFilesCount"];
                    if (Convert.ToDateTime(drList["SentDate"]) != Convert.ToDateTime("1900/01/01"))  this.dtRow["SentDate"] = Convert.ToDateTime(drList["SentDate"]).ToString("dd/MM/yy HH:mm:ss");
                    else this.dtRow["SentDate"] = "";
 
                    if (Convert.ToDateTime(drList["RecievedDate"]) != Convert.ToDateTime("1900/01/01")) this.dtRow["RecievedDate"] = Convert.ToDateTime(drList["RecievedDate"]).ToString("dd/MM/yy HH:mm:ss");
                    else  this.dtRow["RecievedDate"] = "";
  
                    if (Convert.ToDateTime(drList["RTODate"]) != Convert.ToDateTime("1900/01/01")) this.dtRow["RTODate"] = Convert.ToDateTime(drList["RTODate"]).ToString("dd/MM/yy HH:mm:ss");
                    else this.dtRow["RTODate"] = "";

                    this.dtRow["Advisor_ID"] = drList["Advisor_ID"];
                    this.dtRow["AdvisorName"] = (drList["Advisor_Surname"] + " " + drList["Advisor_Firstname"]).Trim();
                    this.dtRow["User_ID"] = drList["User_ID"];
                    this.dtRow["UserName"] = (drList["User_Surname"] + " " + drList["User_Firstname"]).Trim();
                    this.dtRow["StatementFile"] = drList["StatementFile"] + "";
                    this.dtRow["ProposalPDFile"] = drList["ProposalPDFile"] + "";
                    this.dtRow["Status"] = drList["Status"];
                    this.dtRow["CC_Email"] = drList["CC_Email"] + "";
                    this.dtRow["LineStatus"] = drList["LineStatus"];
                    this.dtRow["WebPassword"] = drList["WebPassword"] + "";
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList_NonRecieved()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("II_ID", typeof(int));
            _dtList.Columns.Add("ClientFullName", typeof(string));
            _dtList.Columns.Add("ContractTitle", typeof(string));
            _dtList.Columns.Add("ServiceProviders_Title", typeof(string));
            _dtList.Columns.Add("Code", typeof(string));
            _dtList.Columns.Add("Portfolio", typeof(string));
            _dtList.Columns.Add("Aktion", typeof(string));
            _dtList.Columns.Add("Products_Title", typeof(string));
            _dtList.Columns.Add("Products_Categories_Title", typeof(string));
            _dtList.Columns.Add("ShareTitle", typeof(string));
            _dtList.Columns.Add("ISIN", typeof(string));
            _dtList.Columns.Add("ShareCode", typeof(string));
            _dtList.Columns.Add("Price", typeof(string));
            _dtList.Columns.Add("Quantity", typeof(string));
            _dtList.Columns.Add("Amount", typeof(string));
            _dtList.Columns.Add("Curr", typeof(string));
            _dtList.Columns.Add("Constant", typeof(string));
            _dtList.Columns.Add("StockExchanges_Title", typeof(string));
            _dtList.Columns.Add("DateIns", typeof(string));
            _dtList.Columns.Add("RTODate", typeof(string));
            _dtList.Columns.Add("Notes", typeof(string));
            _dtList.Columns.Add("StatusTitle", typeof(string));
            _dtList.Columns.Add("RTO_Notes", typeof(string));
            _dtList.Columns.Add("Advisor_Name", typeof(string));
            _dtList.Columns.Add("Author_Name", typeof(string));
            _dtList.Columns.Add("Client_ID", typeof(int));
            _dtList.Columns.Add("Client_Type", typeof(int));
            _dtList.Columns.Add("StockCompany_ID", typeof(int));
            _dtList.Columns.Add("ConfirmationStatus", typeof(int));
            _dtList.Columns.Add("Share_ID", typeof(int));
            _dtList.Columns.Add("Contract_ID", typeof(int));
            _dtList.Columns.Add("Product_ID", typeof(int));
            _dtList.Columns.Add("ProductCategory_ID", typeof(int));
            _dtList.Columns.Add("StockExchange_ID", typeof(int));
            _dtList.Columns.Add("Advisor_ID", typeof(int));
            _dtList.Columns.Add("Author_ID", typeof(int));
            _dtList.Columns.Add("PriceType", typeof(int));
            _dtList.Columns.Add("PriceUp", typeof(float));
            _dtList.Columns.Add("PriceDown", typeof(float));
            _dtList.Columns.Add("Tel", typeof(string));
            _dtList.Columns.Add("Mobile", typeof(string));
            _dtList.Columns.Add("ConstantDate", typeof(string));
            _dtList.Columns.Add("ShareCode2", typeof(string));
            _dtList.Columns.Add("ProviderType", typeof(int));
            _dtList.Columns.Add("Status", typeof(int));
            _dtList.Columns.Add("CFP_ID", typeof(int));
            _dtList.Columns.Add("Contract_Details_ID", typeof(int));
            _dtList.Columns.Add("Contract_Packages_ID", typeof(int));
            _dtList.Columns.Add("LineStatus", typeof(int));
            _dtList.Columns.Add("WebPassword", typeof(string));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetInvestIdees_Commands_NonRecieved", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AktionDate", _dAktionDate));
                cmd.Parameters.Add(new SqlParameter("@Client_ID", _iClient_ID));
                cmd.Parameters.Add(new SqlParameter("@Code", _sCode));
                cmd.Parameters.Add(new SqlParameter("@ISIN", _sISIN));
                cmd.Parameters.Add(new SqlParameter("@Advisor_ID", _iAdvisor_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();

                    dtRow["ID"] = drList["ID"];                                                            // InvestIdees_Commands.ID
                    dtRow["II_ID"] = drList["II_ID"];                                                      // InvestIdees.ID
                    dtRow["ClientFullName"] = (drList["Surname"] + " " + drList["Firstname"]).Trim();
                    dtRow["ContractTitle"] = drList["ContractTitle"];
                    dtRow["ServiceProviders_Title"] = drList["ServiceProviders_Title"];
                    dtRow["Code"] = drList["Code"];
                    dtRow["Portfolio"] = drList["ProfitCenter"];
                    dtRow["Aktion"] = (Convert.ToInt32(drList["Aktion"]) == 1? "BUY": "SELL");
                    dtRow["Products_Title"] = drList["Products_Title"];
                    dtRow["Products_Categories_Title"] = drList["Products_Categories_Title"];
                    dtRow["ShareTitle"] = drList["ShareTitle"];
                    dtRow["ISIN"] = drList["ISIN"];
                    dtRow["ShareCode"] = drList["ShareCode"];
                    dtRow["Price"] = drList["Price"];

                    dtRow["Quantity"] = drList["Quantity"];
                    dtRow["Amount"] = Convert.ToSingle(drList["Amount"]);

                    dtRow["Curr"] = drList["Curr"];
                    dtRow["Constant"] = drList["Constant"];
                    dtRow["StockExchanges_Title"] = drList["StockExchanges_Title"];
                    dtRow["DateIns"] = Convert.ToDateTime(drList["DateIns"]).ToString("dd/MM/yy HH:mm:ss");

                    dtRow["StatusTitle"] = "";
                    if (Convert.ToInt32(drList["Status"]) == 2) dtRow["StatusTitle"] = Global.GetLabel("pensive");
                    if (Convert.ToInt32(drList["Status"]) == 3) dtRow["StatusTitle"] = Global.GetLabel("waiting");
                    if (Convert.ToDateTime(drList["RTODate"]) != Convert.ToDateTime("1900/01/01")) dtRow["RTODate"] = drList["RTODate"];
                    else dtRow["RTODate"] = "1900/01/01";

                    dtRow["Notes"] = drList["Notes"];
                    dtRow["RTO_Notes"] = drList["RTO_Notes"];
                    dtRow["Advisor_Name"] = (drList["AdvisorSurname"] + " " + drList["AdvisorFirstname"]).Trim();
                    dtRow["Author_Name"] = (drList["AuthorSurname"] + " " + drList["AuthorFirstname"]).Trim();
                    dtRow["Client_ID"] = drList["Client_ID"];
                    dtRow["Client_Type"] = drList["Tipos"];
                    dtRow["StockCompany_ID"] = drList["StockCompany_ID"];
                    dtRow["ConfirmationStatus"] = drList["ConfirmationStatus"];
                    dtRow["Share_ID"] = drList["Share_ID"];
                    dtRow["Contract_ID"] = drList["ClientPackage_ID"];
                    dtRow["Product_ID"] = drList["Product_ID"];
                    dtRow["ProductCategory_ID"] = drList["Products_Categories_ID"];    // was drList["ProductCategory_ID"], but sometimes it's creates problems, so it was changed in 12/04/2021
                    dtRow["StockExchange_ID"] = drList["StockExchange_ID"];
                    dtRow["Advisor_ID"] = drList["Advisor_ID"];
                    dtRow["Author_ID"] = drList["Author_ID"];
                    dtRow["PriceType"] = drList["PriceType"];
                    dtRow["PriceUp"] = drList["PriceUp"];
                    dtRow["PriceDown"] = drList["PriceDown"];
                    dtRow["Tel"] = drList["Tel"];
                    dtRow["Mobile"] = drList["Mobile"];
                    dtRow["ConstantDate"] = drList["ConstantDate"];
                    dtRow["ShareCode2"] = drList["ShareCode2"];
                    dtRow["ProviderType"] = drList["ProviderType"];
                    dtRow["Status"] = drList["Status"];
                    dtRow["CFP_ID"] = drList["CFP_ID"];
                    dtRow["Contract_Details_ID"] = drList["Contract_Details_ID"];
                    dtRow["Contract_Packages_ID"] = drList["Contract_Packages_ID"];
                    dtRow["WebPassword"] = drList["WebPassword"] + "";
                    _dtList.Rows.Add(dtRow);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetProposalsList()
        {
            string[] sConstant = { "Day Order", "GTC", "GTDate" };
            string[] sZtatus = { "Νέα συμβουλή", "Σκεπτικός", "Αναμονή λήψης RTO", "Μην αποδοχή", "Αποδοχή", "Άκυρο" };

            try
            {
                _dtList = new DataTable("InvestProposals_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Status_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DateSent", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ClientName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Aktion", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ProductType", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Code2", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ISIN", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Price", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Quantity", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("Constant", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RTO_Notes", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RecieveDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SentDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ExecuteDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RealPrice", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("RealQuantity", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("IIC_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("RecieveVoicePath", System.Type.GetType("System.String"));

                conn.Open();
                cmd = new SqlCommand("GetInvestIdees_Proposals", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Advisor_ID", _iAdvisor_ID));
                cmd.Parameters.Add(new SqlParameter("@User_ID", _iUser_ID));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dDateFrom));        
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dDateTo));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = Convert.ToInt32(drList["ID"]);                                                          // II_ID
                    dtRow["Status_ID"] = Convert.ToInt32(drList["Status_ID"]);
                    dtRow["Status"] = sZtatus[Convert.ToInt32(drList["Status_ID"]) - 1];
                    dtRow["DateSent"] = Convert.ToDateTime(drList["SentDate"]);
                    dtRow["Client_ID"] = Convert.ToInt32(drList["Client_ID"]);

                    if (Convert.ToInt32(drList["Tipos"]) == 1) dtRow["ClientName"] = (drList["Surname"] + " " + drList["Firstname"]).Trim();
                    else dtRow["ClientName"] = drList["Surname"] + "";
                    
                    dtRow["Aktion"] = (Convert.ToInt32(drList["Aktion"]) == 1 ? "BUY" : "SELL");
                    dtRow["Share_ID"] = Convert.ToInt32(drList["Share_ID"]);
                    dtRow["ProductType"] = drList["Product_Title"] + "/" + drList["ProductsCategories_Title"];
                    dtRow["Title"] = drList["Title"] + "";
                    dtRow["Code"] = drList["Code"] + "";
                    dtRow["Code2"] = drList["Code2"] + "";
                    dtRow["ISIN"] = drList["ISIN"] + "";
                    dtRow["Price"] = drList["Price"] + "";
                    dtRow["Quantity"] = drList["Quantity"];
                    dtRow["Constant"] = sConstant[Convert.ToInt32(drList["Constant"])];
                    dtRow["RTO_Notes"] = drList["RTO_Notes"] + "";

                    if ((drList["Commands_ID"] + "") != "") {
                        dtRow["RecieveDate"] = Convert.ToDateTime(drList["RecieveDate"]).ToString("dd/MM/yyyy");
                        dtRow["SentDate"] = Convert.ToDateTime(drList["SentDate"]).ToString("dd/MM/yyyy");
                        dtRow["ExecuteDate"] = Convert.ToDateTime(drList["ExecuteDate"]).ToString("dd/MM/yyyy");
                        dtRow["RealPrice"] = Convert.ToDecimal(drList["RealPrice"]);
                        dtRow["RealQuantity"] = Convert.ToDecimal(drList["RealQuantity"]);
                    }
                    else  {
                        dtRow["RecieveDate"] = "";
                        dtRow["SentDate"] = "";
                        dtRow["ExecuteDate"] = "";                      
                        dtRow["RealPrice"] = 0;
                        dtRow["RealQuantity"] = 0;
                    }
  
                    dtRow["IIC_ID"] = Convert.ToInt32(drList["IIC_ID"]);
                    dtRow["RecieveVoicePath"] = drList["RecieveVoicePath"] + "";
 
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { 
                  MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
            }
            finally { conn.Close(); }
        }
        public int InsertRecord()
        {
            _iRecord_ID = 0;
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("InsertInvestIdees", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Advisor_ID", SqlDbType.Int).Value = _iAdvisor_ID;
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = _iUser_ID;
                    cmd.Parameters.Add("@SendMethod", SqlDbType.Int).Value = _iSendMethod;
                    cmd.Parameters.Add("@Description_ID", SqlDbType.Int).Value = _iDescription_ID;
                    cmd.Parameters.Add("@AktionDate", SqlDbType.DateTime).Value = _dAktionDate;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@CC_ID", SqlDbType.Int).Value = _iCC_ID;
                    cmd.Parameters.Add("@AUMs", SqlDbType.Float).Value = _fltAUM;
                    cmd.Parameters.Add("@Currency", SqlDbType.NVarChar, 6).Value = _sCurrency;
                    cmd.Parameters.Add("@Products", SqlDbType.NVarChar, 100).Value = _sProducts;
                    cmd.Parameters.Add("@IdeasText", SqlDbType.NVarChar, 500).Value = _sIdeasText;
                    cmd.Parameters.Add("@CostBenefits", SqlDbType.NVarChar, 100).Value = _sCostBenefits;
                    cmd.Parameters.Add("@StatementFile", SqlDbType.NVarChar, 200).Value = _sStatementFile;
                    cmd.Parameters.Add("@ProposalPDFile", SqlDbType.NVarChar, 200).Value = _sProposalPDFile;
                    cmd.Parameters.Add("@AttachedFilesCount", SqlDbType.Int).Value = _iAttachedFilesCount;
                    cmd.Parameters.Add("@UploadedFilesCount", SqlDbType.Int).Value = _iUploadedFilesCount;
                    cmd.Parameters.Add("@RemotedFilesCount", SqlDbType.Int).Value = _iRemotedFilesCount;
                    cmd.Parameters.Add("@UploadStartTime", SqlDbType.NVarChar, 50).Value = _sUploadStartTime;
                    cmd.Parameters.Add("@UploadFinishTime", SqlDbType.NVarChar, 50).Value = _sUploadFinishTime;
                    cmd.Parameters.Add("@SentDate", SqlDbType.DateTime).Value = _dSentDate;
                    cmd.Parameters.Add("@RecievedDate", SqlDbType.DateTime).Value = _dRecievedDate;
                    cmd.Parameters.Add("@RTODate", SqlDbType.DateTime).Value = _dRTODate;
                    cmd.Parameters.Add("@RecievedOrder", SqlDbType.Int).Value = _iRecievedOrder;
                    cmd.Parameters.Add("@SendAttemptsCount", SqlDbType.Int).Value = _iSendAttemptsCount;
                    cmd.Parameters.Add("@SendMessage", SqlDbType.NVarChar, 1000).Value = _sSendMessage;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.Parameters.Add("@LineStatus", SqlDbType.Int).Value = _iLineStatus;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 500).Value = _sNotes;
                    cmd.Parameters.Add("@WebPassword", SqlDbType.NVarChar, 50).Value = _sWebPassword;
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
                using (SqlCommand cmd = new SqlCommand("EditInvestIdees", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@SendMethod", SqlDbType.Int).Value = _iSendMethod;
                    cmd.Parameters.Add("@Description_ID", SqlDbType.Int).Value = _iDescription_ID;
                    cmd.Parameters.Add("@AktionDate", SqlDbType.DateTime).Value = _dAktionDate;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@CC_ID", SqlDbType.Int).Value = _iCC_ID;
                    cmd.Parameters.Add("@AUMs", SqlDbType.Float).Value = _fltAUM;
                    cmd.Parameters.Add("@Currency", SqlDbType.NVarChar, 6).Value = _sCurrency;
                    cmd.Parameters.Add("@Products", SqlDbType.NVarChar, 100).Value = _sProducts;
                    cmd.Parameters.Add("@IdeasText", SqlDbType.NVarChar, 500).Value = _sIdeasText;
                    cmd.Parameters.Add("@CostBenefits", SqlDbType.NVarChar, 100).Value = _sCostBenefits;
                    cmd.Parameters.Add("@StatementFile", SqlDbType.NVarChar, 200).Value = _sStatementFile;
                    cmd.Parameters.Add("@ProposalPDFile", SqlDbType.NVarChar, 200).Value = _sProposalPDFile;
                    cmd.Parameters.Add("@AttachedFilesCount", SqlDbType.Int).Value = _iAttachedFilesCount;
                    cmd.Parameters.Add("@UploadedFilesCount", SqlDbType.Int).Value = _iUploadedFilesCount;
                    cmd.Parameters.Add("@RemotedFilesCount", SqlDbType.Int).Value = _iRemotedFilesCount;
                    cmd.Parameters.Add("@UploadStartTime", SqlDbType.NVarChar, 50).Value = _sUploadStartTime;
                    cmd.Parameters.Add("@UploadFinishTime", SqlDbType.NVarChar, 50).Value = _sUploadFinishTime;
                    cmd.Parameters.Add("@SentDate", SqlDbType.DateTime).Value = _dSentDate;
                    cmd.Parameters.Add("@RecievedDate", SqlDbType.DateTime).Value = _dRecievedDate;
                    cmd.Parameters.Add("@RTODate", SqlDbType.DateTime).Value = _dRTODate;
                    cmd.Parameters.Add("@RecievedOrder", SqlDbType.Int).Value = _iRecievedOrder;
                    cmd.Parameters.Add("@SendAttemptsCount", SqlDbType.Int).Value = _iSendAttemptsCount;
                    cmd.Parameters.Add("@SendMessage", SqlDbType.NVarChar, 1000).Value = _sSendMessage;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.Parameters.Add("@LineStatus", SqlDbType.Int).Value = _iLineStatus;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 500).Value = _sNotes;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }

        public int Record_ID { get { return _iRecord_ID; } set { _iRecord_ID = value; } }
        public int Advisor_ID { get { return _iAdvisor_ID; } set { _iAdvisor_ID = value; } }
        public int User_ID { get { return _iUser_ID; } set { _iUser_ID = value; } }
        public int SendMethod { get { return _iSendMethod; } set { _iSendMethod = value; } }
        public int Description_ID { get { return _iDescription_ID; } set { _iDescription_ID = value; } } 
        public DateTime AktionDate { get { return _dAktionDate; } set { _dAktionDate = value; } }
        public int Client_ID { get { return _iClient_ID; } set { _iClient_ID = value; } }
        public int CC_ID { get { return _iCC_ID; } set { _iCC_ID = value; } }
        public string CC_Email { get { return _sCC_EMail; } set { _sCC_EMail = value; } }
        public string Code { get { return _sCode; } set { _sCode = value; } }
        public string ISIN { get { return _sISIN; } set { _sISIN = value; } }
        public string CC_EMail { get { return _sCC_EMail; } set { _sCC_EMail = value; } }
        public float AUM { get { return _fltAUM; } set { _fltAUM = value; } }
        public string Currency { get { return _sCurrency; } set { _sCurrency = value; } }
        public string Products { get { return _sProducts; } set { _sProducts = value; } }
        public string IdeasText { get { return _sIdeasText; } set { _sIdeasText = value; } }
        public string CostBenefits { get { return _sCostBenefits; } set { _sCostBenefits = value; } }
        public string StatementFile { get { return _sStatementFile; } set { _sStatementFile = value; } }
        public string ProposalPDFile { get { return _sProposalPDFile; } set { _sProposalPDFile = value; } }
        public int AttachedFilesCount { get { return _iAttachedFilesCount; } set { _iAttachedFilesCount = value; } }
        public int UploadedFilesCount { get { return _iUploadedFilesCount; } set { _iUploadedFilesCount = value; } }
        public int RemotedFilesCount { get { return _iRemotedFilesCount; } set { _iRemotedFilesCount = value; } }
        public string UploadStartTime { get { return _sUploadStartTime; } set { _sUploadStartTime = value; } }
        public string UploadFinishTime { get { return _sUploadFinishTime; } set { _sUploadFinishTime = value; } }
        public DateTime SentDate { get { return _dSentDate; } set { _dSentDate = value; } }
        public DateTime RecievedDate { get { return _dRecievedDate; } set { _dRecievedDate = value; } }
        public DateTime RTODate { get { return _dRTODate; } set { _dRTODate = value; } }
        public int RecievedOrder { get { return _iRecievedOrder; } set { _iRecievedOrder = value; } }
        public int SendAttemptsCount { get { return _iSendAttemptsCount; } set { _iSendAttemptsCount = value; } }
        public string SendMessage { get { return _sSendMessage; } set { _sSendMessage = value; } }
        public int Status { get { return _iStatus; } set { _iStatus = value; } }
        public int LineStatus { get { return _iLineStatus; } set { _iLineStatus = value; } }
        public string Notes { get { return _sNotes; } set { _sNotes = value; } }
        public int Division_ID { get { return _iDivision_ID; } set { _iDivision_ID = value; } }
        public int Contract_ID { get { return _iContract_ID; } set { _iContract_ID = value; } }
        public string AdvisorName { get { return _sAdvisorName; } set { _sAdvisorName = value; } }
        public string UserName { get { return _sUserName; } set { _sUserName = value; } }
        public DateTime DateFrom { get { return _dDateFrom; } set { _dDateFrom = value; } }
        public DateTime DateTo { get { return _dDateTo; } set { _dDateTo = value; } }
        public string WebPassword { get { return _sWebPassword; } set { _sWebPassword = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
