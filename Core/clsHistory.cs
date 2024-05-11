using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsHistory
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;

        private int _iRecord_ID;
        private int _iRecType;       // 1-ClientData, 2-Contracts.Code, 3-Contracts.Portfolio, 4-BankAccount Code, 5-InvestProposals, 6-, 7-ContractData, 8-ManagmentFees, 9-ShareCodes, 10-Commands, 11-BlackList
        private int _iSrcRec_ID;
        private int _iClient_ID;
        private int _iContract_ID;
        private int _iAction;
        private string _sCurrentValues;
        private int _iDocFiles_ID;
        private string _sNotes;
        private int _iUser_ID;
        private DateTime _dDateIns;
        private DateTime _dFrom;
        private DateTime _dTo;

        private DataTable _dtList;
        public clsHistory()
        {
            this._iRecord_ID = 0;
            this._iRecType = 0;
            this._iSrcRec_ID = 0;
            this._iClient_ID = 0;
            this._iContract_ID = 0;
            this._iAction = 0;
            this._sCurrentValues = "";
            this._iDocFiles_ID = 0;
            this._sNotes = "";
            this._iUser_ID = 0;
            this._dDateIns = Convert.ToDateTime("1900/01/01");
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "History"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iRecType = Convert.ToInt32(drList["RecType"]);
                    this._iSrcRec_ID = Convert.ToInt32(drList["SrcRec_ID"]);
                    this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                    this._iContract_ID = Convert.ToInt32(drList["Contract_ID"]);
                    this._iAction = Convert.ToInt32(drList["Aktion"]);
                    this._sCurrentValues = drList["CurrentValues"] + "";
                    this._iDocFiles_ID = Convert.ToInt32(drList["DocFiles_ID"]);
                    this._sNotes = drList["Notes"] + "";
                    this._iUser_ID = Convert.ToInt32(drList["User_ID"]);
                    this._dDateIns = Convert.ToDateTime(drList["DateIns"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("AktionTitle", typeof(string));
            _dtList.Columns.Add("Code", typeof(string));
            _dtList.Columns.Add("Portfolio", typeof(string));
            _dtList.Columns.Add("ContractTitle", typeof(string));
            _dtList.Columns.Add("Notes", typeof(string));
            _dtList.Columns.Add("FileName", typeof(string));
            _dtList.Columns.Add("Client_ID", typeof(int));
            _dtList.Columns.Add("Contract_ID", typeof(int));
            _dtList.Columns.Add("RecType", typeof(int));
            _dtList.Columns.Add("SrcRec_ID", typeof(int));
            _dtList.Columns.Add("Aktion", typeof(int));
            _dtList.Columns.Add("CurrentValues", typeof(string));
            _dtList.Columns.Add("DocFiles_ID", typeof(int));
            _dtList.Columns.Add("DateIns", typeof(DateTime));
            _dtList.Columns.Add("User_ID", typeof(int));
            _dtList.Columns.Add("UserName", typeof(string));

            SqlDataReader drList = null;
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetHistory", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@RecType", _iRecType));
                cmd.Parameters.Add(new SqlParameter("@SrcRec_ID", _iSrcRec_ID));
                cmd.Parameters.Add(new SqlParameter("@Contract_ID", _iContract_ID));
                cmd.Parameters.Add(new SqlParameter("@Client_ID", _iClient_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["AktionTitle"], drList["Code"], drList["Portfolio"], drList["ContractTitle"], drList["Notes"], drList["FileName"], 
                                     drList["Client_ID"], drList["Contract_ID"], drList["RecType"], drList["SrcRec_ID"], 
                                     drList["Aktion"], drList["CurrentValues"], drList["DocFiles_ID"], drList["DateIns"], drList["User_ID"], drList["UserName"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetBlackList()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("AktionTitle", typeof(string));
            _dtList.Columns.Add("Code", typeof(string));
            _dtList.Columns.Add("Portfolio", typeof(string));
            _dtList.Columns.Add("ContractTitle", typeof(string));
            _dtList.Columns.Add("Notes", typeof(string));
            _dtList.Columns.Add("FileName", typeof(string));
            _dtList.Columns.Add("Client_ID", typeof(int));
            _dtList.Columns.Add("Contract_ID", typeof(int));
            _dtList.Columns.Add("RecType", typeof(int));
            _dtList.Columns.Add("SrcRec_ID", typeof(int));
            _dtList.Columns.Add("Aktion", typeof(int));
            _dtList.Columns.Add("CurrentValues", typeof(string));
            _dtList.Columns.Add("DocFiles_ID", typeof(int));
            _dtList.Columns.Add("DateIns", typeof(DateTime));
            _dtList.Columns.Add("User_ID", typeof(int));
            _dtList.Columns.Add("UserName", typeof(string));

            SqlDataReader drList = null;
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetClientsBlackListHistory", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Client_ID", _iClient_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["AktionTitle"], "", "", "", drList["Notes"], drList["FileName"],
                                     drList["Client_ID"], drList["Contract_ID"], drList["RecType"], drList["SrcRec_ID"],
                                     drList["Aktion"], drList["CurrentValues"], drList["DocFiles_ID"], drList["DateIns"], drList["User_ID"], drList["UserName"]);
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
                using (SqlCommand cmd = new SqlCommand("InsertHistory", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@RecType", SqlDbType.Int).Value = _iRecType;
                    cmd.Parameters.Add("@SrcRec_ID", SqlDbType.Int).Value = _iSrcRec_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Aktion", SqlDbType.Int).Value = _iAction;
                    cmd.Parameters.Add("@CurrentValues", SqlDbType.NVarChar, 2000).Value = _sCurrentValues;
                    cmd.Parameters.Add("@DocFiles_ID", SqlDbType.Int).Value = _iDocFiles_ID;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 2000).Value = _sNotes;
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = _iUser_ID;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = DateIns; 
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
                using (SqlCommand cmd = new SqlCommand("EditHistory", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@RecType", SqlDbType.Int).Value = _iRecType;
                    cmd.Parameters.Add("@SrcRec_ID", SqlDbType.Int).Value = _iSrcRec_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Aktion", SqlDbType.Int).Value = _iAction;
                    cmd.Parameters.Add("@CurrentValues", SqlDbType.NVarChar, 2000).Value = _sCurrentValues;
                    cmd.Parameters.Add("@DocFiles_ID", SqlDbType.Int).Value = _iDocFiles_ID;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 2000).Value = _sNotes;
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = _iUser_ID;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = DateIns;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "History";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = this._iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return _iRecord_ID; } set { _iRecord_ID = value; } }
        public int RecType { get { return _iRecType; } set { _iRecType = value; } }
        public int SrcRec_ID { get { return _iSrcRec_ID; } set { _iSrcRec_ID = value; } }
        public int Client_ID { get { return _iClient_ID; } set { _iClient_ID = value; } }
        public int Contract_ID { get { return _iContract_ID; } set { _iContract_ID = value; } }
        public int Action { get { return _iAction; } set { _iAction = value; } }
        public int DocFiles_ID { get { return _iDocFiles_ID; } set { _iDocFiles_ID = value; } }
        public string CurrentValues { get { return _sCurrentValues; } set { _sCurrentValues = value; } }
        public string Notes { get { return _sNotes; } set { _sNotes = value; } }
        public int User_ID { get { return _iUser_ID; } set { _iUser_ID = value; } }
        public DateTime DateIns { get { return _dDateIns; } set { _dDateIns = value; } }
        public DateTime DateFrom { get { return _dFrom; } set { _dFrom = value; } }
        public DateTime DateTo { get { return _dTo; } set { _dTo = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
