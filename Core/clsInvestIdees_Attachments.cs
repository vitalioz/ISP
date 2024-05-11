using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsInvestIdees_Attachments
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int      _iRecord_ID;
        private int      _iII_ID;
        private int      _iShare_ID;
        private int      _iDocType_ID;
        private string   _sFileName;
        private string   _sFileFullPath;
        private string  _sServerFileName;
        private string   _sUploadFilePath;
        private string   _sRemoteFileName;
        private int      _iUploadAttempts;
        private int      _iStatus;
        private DateTime _dAktionDate;

        private DataTable _dtList;

        public clsInvestIdees_Attachments()
        {
            this._iRecord_ID = 0;
            this._iII_ID = 0;
            this._iShare_ID = 0;
            this._iDocType_ID = 0;
            this._sFileName = "";
            this._sFileFullPath = "";
            this._sServerFileName = "";
            this._sUploadFilePath = "";
            this._sRemoteFileName = "";
            this._iUploadAttempts = 0;
            this._iStatus = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetInvestIdees_AttachmentFile", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iII_ID = Convert.ToInt32(drList["II_ID"]);
                    this._iShare_ID = Convert.ToInt32(drList["Share_ID"]);
                    this._iDocType_ID = Convert.ToInt32(drList["DocType_ID"]);
                    this._sFileName = drList["FileName"] + "";
                    this._sFileFullPath = drList["FileFullPath"] + "";
                    this._sServerFileName = drList["ServerFileName"] + "";
                    this._sUploadFilePath = drList["UploadFilePath"] + "";
                    this._sRemoteFileName = drList["RemoteFileName"] + "";
                    this._iUploadAttempts = Convert.ToInt32(drList["UploadAttempts"]);
                    this._iStatus = Convert.ToInt32(drList["Status"]);
                    this._dAktionDate = Convert.ToDateTime(drList["AktionDate"]);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            int i = 0, iOldShare_ID = 0;

            try
            {
                _dtList = new DataTable("InvetIdees_Attachments_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Share_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DocType_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DocType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("FileName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("FileFullPath", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ServerFileName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("UploadFilePath", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RemoteFileName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("UploadAttempts", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetInvestIdees_AttachmentsList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@II_ID", _iII_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    if (iOldShare_ID != Convert.ToInt32(drList["Share_ID"])) {
                        iOldShare_ID = Convert.ToInt32(drList["Share_ID"]);
                        i = 0;
                    }

                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];

                    dtRow["Share_ID"] = drList["Share_ID"];
                    if (Convert.ToInt32(drList["DocType_ID"]) == 0) {
                        i = i + 1;
                        dtRow["DocType_Title"] = "Συνημμένο αρχείο " + i;
                        dtRow["DocType_ID"] = 0;
                    }
                    else {
                        dtRow["DocType_Title"] = drList["DocType_Title"];
                        dtRow["DocType_ID"] = drList["DocType_ID"];
                    }
                    dtRow["FileName"] = drList["FileName"] + "";
                    dtRow["FileFullPath"] = drList["FileFullPath"] + "";
                    dtRow["ServerFileName"] = drList["ServerFileName"] + "";
                    dtRow["UploadFilePath"] = drList["UploadFilePath"] + "";
                    dtRow["RemoteFileName"] = drList["RemoteFileName"] + "";
                    dtRow["UploadAttempts"] = drList["UploadAttempts"];
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
                using (SqlCommand cmd = new SqlCommand("InsertInvestIdees_Attachments", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@II_ID", SqlDbType.Int).Value = _iII_ID;
                    cmd.Parameters.Add("@Share_ID", SqlDbType.Int).Value = _iShare_ID;
                    cmd.Parameters.Add("@DocType_ID", SqlDbType.Int).Value = _iDocType_ID;
                    cmd.Parameters.Add("@FileName", SqlDbType.NVarChar, 100).Value = _sFileName;
                    cmd.Parameters.Add("@FileFullPath", SqlDbType.NVarChar, 500).Value = _sFileFullPath;
                    cmd.Parameters.Add("@ServerFileName", SqlDbType.NVarChar, 50).Value = _sServerFileName;
                    cmd.Parameters.Add("@UploadFilePath", SqlDbType.NVarChar, 500).Value = _sUploadFilePath;
                    cmd.Parameters.Add("@RemoteFileName", SqlDbType.NVarChar, 50).Value = _sRemoteFileName;

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
                using (SqlCommand cmd = new SqlCommand("EditInvestIdees_Attachments", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@II_ID", SqlDbType.Int).Value = _iII_ID;
                    cmd.Parameters.Add("@Share_ID", SqlDbType.Int).Value = _iShare_ID;
                    cmd.Parameters.Add("@DocType_ID", SqlDbType.Int).Value = _iDocType_ID;
                    cmd.Parameters.Add("@FileName", SqlDbType.NVarChar, 100).Value = _sFileName;
                    cmd.Parameters.Add("@FileFullPath", SqlDbType.NVarChar, 500).Value = _sFileFullPath;
                    cmd.Parameters.Add("@ServerFileName", SqlDbType.NVarChar, 50).Value = _sServerFileName;
                    cmd.Parameters.Add("@UploadFilePath", SqlDbType.NVarChar, 500).Value = _sUploadFilePath;
                    cmd.Parameters.Add("@RemoteFileName", SqlDbType.NVarChar, 50).Value = _sRemoteFileName;
                    cmd.Parameters.Add("@UploadAttempts", SqlDbType.Int).Value = _iUploadAttempts;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "InvestIdees_Attachments";
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
        public int Share_ID { get { return this._iShare_ID; } set { this._iShare_ID = value; } }
        public int DocType_ID { get { return this._iDocType_ID; } set { this._iDocType_ID = value; } }
        public string FileName { get { return this._sFileName; } set { this._sFileName = value; } }
        public string FileFullPath { get { return this._sFileFullPath; } set { this._sFileFullPath = value; } }
        public string ServerFileName { get { return this._sServerFileName; } set { this._sServerFileName = value; } }
        public string UploadFilePath { get { return this._sUploadFilePath; } set { this._sUploadFilePath = value; } }
        public string RemoteFileName { get { return this._sRemoteFileName; } set { this._sRemoteFileName = value; } }
        public int UploadAttempts { get { return this._iUploadAttempts; } set { this._iUploadAttempts = value; } }
        public int Status { get { return this._iStatus; } set { this._iStatus = value; } }
        public DateTime AktionDate { get { return this._dAktionDate; } set { this._dAktionDate = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}






