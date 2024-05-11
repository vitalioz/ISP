using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsInformings
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;

        private int      _iRecord_ID;                          
        private int      _iCommand_Type;
        private int      _iCommand_ID;
        private int      _iInformMethod;                // 1-Τηλέφωνο, 4-SMS, 5-e-mail, 6-fax, 7-Personal, 8-Post, 9-EAMNet
        private int      _iSource_ID;                   // 2-DailyInform, 3-ManFeesInform, 4-MiscInform, 6-InvoiceRTO, 7-AdminFeesInform, 8-PeriodicalEvaluation Inform, 9-ExPostCost, 10-Custody Fees
        private int      _iClient_ID;
        private int      _iContract_ID;
        private string   _sClientData;
        private string   _sCC;
        private string   _sSubject;
        private string   _sBody;
        private string   _sFileName;
        private string   _sAttachedFiles;
        private int      _iAttachedFilesCount;
        private DateTime _dDateIns;
        private string   _sDateSent;
        private int      _iStatus;
        private int      _iSentAttempts;
        private string   _sSentMessage;
        private int      _iUser_ID;

        private string   _sClientName;
        private DataTable _dtList;
        public clsInformings()
        {
            this._iRecord_ID = 0;
            this._iCommand_Type = 0;
            this._iCommand_ID = 0;
            this._iInformMethod = 0;
            this._iSource_ID = 0;
            this._iClient_ID = 0;
            this._iContract_ID = 0;
            this._sClientData = "";
            this._sCC = "";
            this._sSubject = "";
            this._sBody = "";
            this._sFileName = "";
            this._sAttachedFiles = "";
            this._iAttachedFilesCount = 0;
            this._dDateIns = Convert.ToDateTime("1900/01/01");
            this._sDateSent = "";
            this._iStatus = 0;
            this._iSentAttempts = 0;
            this._sSentMessage = "";
            this._iUser_ID = 0;
            this._sClientName = "";
        }
        public void GetRecord()
        {
            drList = null;
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetInforming", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", this._iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iCommand_Type = Convert.ToInt32(drList["Command_Type"]);
                    this._sClientData = drList["ClientData"] + "";
                    this._sCC = drList["CC"] + "";
                    this._sSubject = drList["Subject"] + "";
                    this._sBody = drList["Body"] + "";
                    this._sFileName = drList["FileName"] + "";
                    this._sAttachedFiles = drList["AttachedFiles"] + "";
                    this._iInformMethod = Convert.ToInt32(drList["InformMethod"]);
                    this._iCommand_ID = Convert.ToInt32(drList["Command_ID"]);
                    this._iSource_ID = Convert.ToInt32(drList["Source_ID"]);
                    this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                    this._iContract_ID = Convert.ToInt32(drList["Contract_ID"]);
                    this._iAttachedFilesCount = Convert.ToInt32(drList["AttachedFilesCount"]);
                    this._dDateIns = Convert.ToDateTime(drList["DateIns"]);
                    this._iStatus = Convert.ToInt32(drList["Status"]);
                    this._iSentAttempts = Convert.ToInt32(drList["SentAttempts"]);
                    this._sSentMessage = drList["SentMessage"] + "";
                    this._iUser_ID = Convert.ToInt32(drList["User_ID"]);
                    if (Convert.ToInt32(drList["ContractTipos"]) == 1) this._sClientName = (drList["ContractTitle"] + "").Trim();
                    else
                       if (Convert.ToInt32(drList["Tipos"]) == 1) this._sClientName = (drList["Surname"] + " " + drList["Firstname"]).Trim();
                    else this._sClientName = (drList["Surname"] + "").Trim();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("Command_Type", typeof(int));
            _dtList.Columns.Add("Command_ID", typeof(int));
            _dtList.Columns.Add("InformMethod", typeof(int));
            _dtList.Columns.Add("Source_ID", typeof(int));
            _dtList.Columns.Add("Client_ID", typeof(int));
            _dtList.Columns.Add("Contract_ID", typeof(int));
            _dtList.Columns.Add("ClientData", typeof(string));
            _dtList.Columns.Add("CC", typeof(string));
            _dtList.Columns.Add("Subject", typeof(string));
            _dtList.Columns.Add("Body", typeof(string));
            _dtList.Columns.Add("FileName", typeof(string));
            _dtList.Columns.Add("AttachedFiles", typeof(string));
            _dtList.Columns.Add("AttachedFilesCount", typeof(int));
            _dtList.Columns.Add("DateIns", typeof(DateTime));
            _dtList.Columns.Add("DateSent", typeof(string));
            _dtList.Columns.Add("Status", typeof(int));
            _dtList.Columns.Add("SentAttempts", typeof(int));
            _dtList.Columns.Add("User_ID", typeof(int));
           
            drList = null;
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetClient_Informings", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Client_ID", _iClient_ID));
                cmd.Parameters.Add(new SqlParameter("@Contract_ID", _iContract_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["Command_Type"], drList["Command_ID"], drList["InformMethod"], drList["Source_ID"],
                                     drList["Client_ID"], drList["Contract_ID"], drList["ClientData"], drList["CC"], drList["Subject"], drList["Body"],
                                     drList["FileName"], drList["AttachedFiles"], drList["AttachedFilesCount"], drList["DateIns"],
                                     drList["DateSent"], drList["Status"], drList["SentAttempts"], drList["User_ID"]);
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
                using (cmd = new SqlCommand("sp_InsertInformings", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Command_Type", SqlDbType.Int).Value = _iCommand_Type;
                    cmd.Parameters.Add("@Command_ID", SqlDbType.Int).Value = _iCommand_ID;
                    cmd.Parameters.Add("@InformMethod", SqlDbType.Int).Value = _iInformMethod;
                    cmd.Parameters.Add("@Source_ID", SqlDbType.Int).Value = _iSource_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@ClientData", SqlDbType.NVarChar, 100).Value = _sClientData;
                    cmd.Parameters.Add("@CC", SqlDbType.NVarChar, 200).Value = _sCC;
                    cmd.Parameters.Add("@Subject", SqlDbType.NVarChar, 200).Value = _sSubject;
                    cmd.Parameters.Add("@Body", SqlDbType.Text).Value = _sBody;
                    cmd.Parameters.Add("@FileName", SqlDbType.NVarChar, 100).Value = _sFileName;
                    cmd.Parameters.Add("@AttachedFiles", SqlDbType.NVarChar, 2500).Value = _sAttachedFiles;
                    cmd.Parameters.Add("@AttachedFilesCount", SqlDbType.Int).Value = _iAttachedFilesCount;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = _dDateIns;
                    cmd.Parameters.Add("@DateSent", SqlDbType.NVarChar, 50).Value = _sDateSent;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.Parameters.Add("@SentAttempts", SqlDbType.Int).Value = _iSentAttempts;
                    cmd.Parameters.Add("@SentMessage", SqlDbType.NVarChar, 100).Value = _sSentMessage;
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = _iUser_ID;
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
                using (cmd = new SqlCommand("sp_EditInformings", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@InformMethod", SqlDbType.Int).Value = _iInformMethod;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = _dDateIns;
                    cmd.Parameters.Add("@DateSent", SqlDbType.NVarChar, 50).Value = _sDateSent;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.Parameters.Add("@SentAttempts", SqlDbType.Int).Value = _iSentAttempts;
                    cmd.Parameters.Add("@SentMessage", SqlDbType.NVarChar, 100).Value = _sSentMessage;
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = _iUser_ID;



                    /*
                    cmd.Parameters.Add("@Command_Type", SqlDbType.Int).Value = _iCommand_Type;
                    cmd.Parameters.Add("@Command_ID", SqlDbType.Int).Value = _iCommand_ID;

                    cmd.Parameters.Add("@Source_ID", SqlDbType.Int).Value = _iSource_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@ClientData", SqlDbType.NVarChar, 100).Value = _sClientData;
                    cmd.Parameters.Add("@CC", SqlDbType.NVarChar, 200).Value = _sCC;
                    cmd.Parameters.Add("@Subject", SqlDbType.NVarChar, 200).Value = _sSubject;
                    cmd.Parameters.Add("@Body", SqlDbType.Text).Value = _sBody;
                    cmd.Parameters.Add("@FileName", SqlDbType.NVarChar, 100).Value = _sFileName;
                    cmd.Parameters.Add("@AttachedFiles", SqlDbType.NVarChar, 2500).Value = _sAttachedFiles;
                    cmd.Parameters.Add("@AttachedFilesCount", SqlDbType.Int).Value = _iAttachedFilesCount;
                    */


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
                using (cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Informings";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return _iRecord_ID; } set { _iRecord_ID = value; } }
        public int Command_Type { get { return _iCommand_Type; } set { _iCommand_Type = value; } }
        public int Command_ID { get { return _iCommand_ID; } set { _iCommand_ID = value; } }
        public int InformMethod { get { return _iInformMethod; } set { _iInformMethod = value; } }
        public int Source_ID { get { return _iSource_ID; } set { _iSource_ID = value; } }
        public int Client_ID { get { return _iClient_ID; } set { _iClient_ID = value; } }
        public int Contract_ID { get { return _iContract_ID; } set { _iContract_ID = value; } }
        public string ClientData { get { return _sClientData; } set { _sClientData = value; } }
        public string CC { get { return _sCC; } set { _sCC = value; } }
        public string Subject { get { return _sSubject; } set { _sSubject = value; } }
        public string Body { get { return _sBody; } set { _sBody = value; } }
        public string FileName { get { return _sFileName; } set { _sFileName = value; } }
        public string AttachedFiles { get { return _sAttachedFiles; } set { _sAttachedFiles = value; } }
        public int AttachedFilesCount { get { return _iAttachedFilesCount; } set { _iAttachedFilesCount = value; } }
        public DateTime DateIns { get { return _dDateIns; } set { _dDateIns = value; } }
        public string DateSent { get { return _sDateSent; } set { _sDateSent = value; } }
        public int Status { get { return _iStatus; } set { _iStatus = value; } }
        public int SentAttempts { get { return _iSentAttempts; } set { _iSentAttempts = value; } }
        public string SentMessage { get { return _sSentMessage; } set { _sSentMessage = value; } }
        public int User_ID { get { return _iUser_ID; } set { _iUser_ID = value; } }
        public string ClientName { get { return _sClientName; } set { _sClientName = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
