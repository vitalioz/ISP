using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace Core
{
    public class clsClientsDocFiles
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlDataReader drList = null;

        private int      _iRecord_ID;
        private int      _iClient_ID;
        private int      _iPreContract_ID;
        private int      _iContract_ID;
        private int      _iDocTypes;
        private int      _iPD_Group_ID;
        private int      _iDMS_Files_ID;
        private int      _iOldFile;
        private DateTime _dDateIns;
        private int      _iUser_ID;
        private int      _iStatus;                                  // 0 - deleted file, 1 - non confirmed file,  2 - document confirmed file

        private string   _sFileName;
        private string   _sFullFileName;
        private string   _sOldFileName;
        private string   _sNewFileName;
        private string   _sClientName;
        private string   _sContractCode;
        private string   _sDocTypes_Title;
        private string   _sTemp;
        private int      i;
        private DataTable _dtList;

        public clsClientsDocFiles()
        {
            this._iRecord_ID = 0;
            this._iClient_ID = 0;
            this._iPreContract_ID = 0;
            this._iContract_ID = 0;
            this._iDocTypes = 0;
            this._iPD_Group_ID = 0;
            this._iDMS_Files_ID = 0;
            this._iOldFile = 0;
            this._dDateIns = Convert.ToDateTime("1900/01/01");
            this._iUser_ID = 0;
            this._iStatus = 0;
            this._iContract_ID = 0;
            this._sNewFileName = "";
            this._sDocTypes_Title = "";
            this._sFileName = "";
            this._sTemp = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetClients_DocFile", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@FileName", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                    this._iPreContract_ID = Convert.ToInt32(drList["PreContract_ID"]);
                    this._iContract_ID = Convert.ToInt32(drList["Contract_ID"]);
                    this._iDocTypes = Convert.ToInt32(drList["DocTypes_ID"]);
                    this._iPD_Group_ID = Convert.ToInt32(drList["PD_Group_ID"]);
                    this._iDMS_Files_ID = Convert.ToInt32(drList["DMS_Files_ID"]);
                    this._iOldFile = Convert.ToInt32(drList["OldFile"]);
                    this._dDateIns = Convert.ToDateTime(drList["DateIns"]);
                    this._iUser_ID = Convert.ToInt32(drList["User_ID"]);
                    this._iStatus = Convert.ToInt32(drList["Status"]);
                    this._sNewFileName = drList["FileName"] + "";
                    this._sDocTypes_Title = drList["DocTypes_Title"] + "";
                    this._iDocTypes = Convert.ToInt32(drList["DocTypes_ID"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetRecord_FileName()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetClients_DocFile", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", "0"));
                cmd.Parameters.Add(new SqlParameter("@FileName", this._sFileName));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                    this._iPreContract_ID = Convert.ToInt32(drList["PreContract_ID"]);
                    this._iContract_ID = Convert.ToInt32(drList["Contract_ID"]);
                    this._iDocTypes = Convert.ToInt32(drList["DocTypes_ID"]);
                    this._iPD_Group_ID = Convert.ToInt32(drList["PD_Group_ID"]);
                    this._iDMS_Files_ID = Convert.ToInt32(drList["DMS_Files_ID"]);
                    this._iOldFile = Convert.ToInt32(drList["OldFile"]);
                    this._dDateIns = Convert.ToDateTime(drList["DateIns"]);
                    this._iUser_ID = Convert.ToInt32(drList["User_ID"]);
                    this._iStatus = Convert.ToInt32(drList["Status"]);
                    this._sNewFileName = drList["FileName"] + "";
                    this._sDocTypes_Title = drList["DocTypes_Title"] + "";
                    this._iDocTypes = Convert.ToInt32(drList["DocTypes_ID"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }

        public void GetList()
        {
            string sClientFullName = "";

            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("Client_ID", typeof(int));
            _dtList.Columns.Add("Code", typeof(string));
            _dtList.Columns.Add("PreContract_ID", typeof(int));
            _dtList.Columns.Add("Contract_ID", typeof(int));
            _dtList.Columns.Add("DocTypes", typeof(int));
            _dtList.Columns.Add("Tipos", typeof(string));
            _dtList.Columns.Add("PD_Group_ID", typeof(int));
            _dtList.Columns.Add("DMS_Files_ID", typeof(int));
            _dtList.Columns.Add("FilePath", typeof(string));
            _dtList.Columns.Add("FileName", typeof(string));
            _dtList.Columns.Add("OldFile", typeof(int));
            _dtList.Columns.Add("DateIns", typeof(DateTime));
            _dtList.Columns.Add("User_ID", typeof(int));
            _dtList.Columns.Add("Status", typeof(int));

            try
            {          
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetClient_DocFiles", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Client_ID", _iClient_ID));
                cmd.Parameters.Add(new SqlParameter("@PreContract_ID", _iPreContract_ID));
                cmd.Parameters.Add(new SqlParameter("@Contract_ID", _iContract_ID));
                cmd.Parameters.Add(new SqlParameter("@DocTypes_ID", _iDocTypes));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _sTemp = "";
                    sClientFullName = (drList["Surname"] + " " + drList["Firstname"]).Trim().Replace(".", "_");
                    if (Convert.ToInt32(drList["PreContract_ID"]) != 0)
                        if (Convert.ToInt32(drList["PreContractType"]) == 1) _sTemp = "/Customers/" + sClientFullName + "/Portfolio_" + drList["PreContract_ID"];   // 1 - ATOMIKOS
                        else _sTemp = "/Customers/Portfolio_" + drList["PreContract_ID"] + "/Portfolio_" + drList["PreContract_ID"];                                // else 2 - KOINOS
                    else
                    { 
                        if (Convert.ToInt32(drList["Contract_ID"]) != 0)
                        {
                           if (Convert.ToInt32(drList["ContractType"]) == 1) _sTemp = "/Customers/" + sClientFullName + "/" + drList["Code"];                       // 1 - ATOMIKOS                            
                           else _sTemp = "/Customers/" + drList["Code"] + "/" + drList["Code"];                                                                     // else 2 - KOINOS

                        }
                        else _sTemp = "/Customers/" + sClientFullName;
                    }
                    _dtList.Rows.Add(drList["ID"], drList["Client_ID"], drList["Code"], drList["PreContract_ID"], drList["Contract_ID"], drList["DocTypes_ID"],
                                     drList["Tipos"], drList["PD_Group_ID"], drList["DMS_Files_ID"], _sTemp, drList["FileName"], drList["OldFile"], 
                                     drList["DateIns"], drList["User_ID"], drList["Status"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int InsertRecord()
        {
            try
            {
                if (_iDMS_Files_ID == 0)
                {
                    //--- copy file into client directory ---------------
                    //MessageBox.Show(_sFullFileName + "\n" + _sOldFileName + "\n" + _sNewFileName, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (_sFullFileName != "" || _sOldFileName != _sNewFileName)
                    {
                        if (_sContractCode.Length > 0)                        // this file dependes to Contract - it's Contract's file, so copy it into directory with path ...clientname\code\...
                            _sTemp = "Customers/" + _sClientName + "/" + _sContractCode.Replace(".", "_");
                        else                                                  // this file undepedendes to Contract - it's personal file, so copy it into directory with path...clientname\...
                            _sTemp = "Customers/" + _sClientName;

                        _sTemp = _sTemp.Replace(".", "_");
                        //MessageBox.Show(_sFullFileName + "\n" + _sTemp + "\n" + _sNewFileName, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        _sTemp = Global.DMS_UploadFile(_sFullFileName, _sTemp, Path.GetFileName(_sNewFileName));
                        _sNewFileName = Path.GetFileName(_sTemp);
                    }
                }

                if (this._iContract_ID == 0) i = 1;                  // 1 - Client's personal data document
                else i = 2;                                          // 2 - Client's package document

                clsDMS_Files DMS_Files = new clsDMS_Files();
                DMS_Files.Source_ID = i;
                DMS_Files.DocTypes_ID = this._iDocTypes;
                DMS_Files.FileName = this._sNewFileName;
                DMS_Files.DateIns = DateTime.Now;
                DMS_Files.User_ID = this._iUser_ID;
                this._iDMS_Files_ID = DMS_Files.InsertRecord();

                conn.Open();
                using (SqlCommand cmd = new SqlCommand("InsertClientDocFile", conn))
                {
                    SqlParameter outParam1 = new SqlParameter("@ID", SqlDbType.Int);
                    outParam1.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam1);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = this._iClient_ID;
                    cmd.Parameters.Add("@PreContract_ID", SqlDbType.Int).Value = this._iPreContract_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = this._iContract_ID;
                    cmd.Parameters.Add("@DocTypes", SqlDbType.Int).Value = this._iDocTypes;
                    cmd.Parameters.Add("@PD_Group_ID", SqlDbType.Int).Value = this._iPD_Group_ID;
                    cmd.Parameters.Add("@DMS_Files_ID", SqlDbType.Int).Value = this._iDMS_Files_ID;
                    cmd.Parameters.Add("@OldFile", SqlDbType.Int).Value = this._iOldFile;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = this._dDateIns;
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = this._iUser_ID;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = this._iStatus;                                        // 3 - document confirmed
                    cmd.ExecuteNonQuery();
                    this._iRecord_ID = Convert.ToInt32(outParam1.Value);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            return this._iRecord_ID;
        }
        public void EditRecord()
        {
            try
            {                
                //--- copy file into client directory ---------------
                if (_sFullFileName != "" && _sOldFileName != _sNewFileName)
                {
                    if (_sContractCode.Length > 0)                        // this file dependes to Contract - it's Contract's file, so copy it into directory with path ...clientname\code\...
                        _sTemp = "Customers/" + _sClientName + "/" + _sContractCode.Replace(".", "_");
                    else                                                  // this file undepedendes to Contract - it's personal file, so copy it into directory with path...clientname\...
                        _sTemp = "Customers/" + _sClientName;

                    _sTemp = _sTemp.Replace(".", "_");
                    _sTemp = Global.DMS_UploadFile(_sFullFileName, _sTemp, Path.GetFileName(_sNewFileName));
                    _sNewFileName = Path.GetFileName(_sTemp);

                    if (this._iContract_ID == 0) i = 1;                  // 1 - Client's personal data document
                    else i = 2;                                          // 2 - Client's package document

                    clsDMS_Files DMS_Files = new clsDMS_Files();
                    DMS_Files.Source_ID = i;
                    DMS_Files.DocTypes_ID = this._iDocTypes;
                    DMS_Files.FileName = this._sNewFileName;
                    DMS_Files.DateIns = DateTime.Now;
                    DMS_Files.User_ID = this._iUser_ID;
                    this._iDMS_Files_ID = DMS_Files.InsertRecord();
                }
                else
                {
                    clsDMS_Files DMS_Files = new clsDMS_Files();
                    DMS_Files.Record_ID = this._iDMS_Files_ID;
                    DMS_Files.DocTypes_ID = this._iDocTypes;
                    DMS_Files.FileName = this._sNewFileName;
                    DMS_Files.DateIns = DateTime.Now;
                    DMS_Files.User_ID = this._iUser_ID;
                    DMS_Files.EditRecord();
                }

                conn.Open();
                using (SqlCommand cmd = new SqlCommand("EditClientDocFile", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = this._iRecord_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = this._iClient_ID;
                    cmd.Parameters.Add("@PreContract_ID", SqlDbType.Int).Value = this._iPreContract_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = this._iContract_ID;
                    cmd.Parameters.Add("@DocTypes", SqlDbType.Int).Value = this._iDocTypes;
                    cmd.Parameters.Add("@PD_Group_ID", SqlDbType.Int).Value = this._iPD_Group_ID;
                    cmd.Parameters.Add("@DMS_Files_ID", SqlDbType.Int).Value = this._iDMS_Files_ID;
                    cmd.Parameters.Add("@OldFile", SqlDbType.Int).Value = this._iOldFile;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = this._dDateIns;
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = this._iUser_ID;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = this._iStatus;                          // 0 - document deleted, 1 - document non confirmed, 2 - document confirmed
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void EditStatus()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("EditClientDocFile_Status", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = this._iRecord_ID;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = this._iStatus;                          // 0 - document deleted, 1 - document non confirmed, 2 - document confirmed
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ClientsDocFiles";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void DeleteRecord_Client_ID()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ClientsDocFiles";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "Client_ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iClient_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return _iRecord_ID; } set { _iRecord_ID = value; } }
        public int Client_ID { get { return _iClient_ID; } set { _iClient_ID = value; } }
        public int PreContract_ID { get { return _iPreContract_ID; } set { _iPreContract_ID = value; } }
        public int Contract_ID { get { return _iContract_ID; } set { _iContract_ID = value; } }
        public int DocTypes { get { return _iDocTypes; } set { _iDocTypes = value; } }
        public string DocTypes_Title { get { return _sDocTypes_Title; } set { _sDocTypes_Title = value; } }
        public int PD_Group_ID { get { return _iPD_Group_ID; } set { _iPD_Group_ID = value; } }
        public int DMS_Files_ID { get { return _iDMS_Files_ID; } set { _iDMS_Files_ID = value; } }
        public int OldFile { get { return _iOldFile; } set { _iOldFile = value; } }
        public DateTime DateIns { get { return _dDateIns; } set { _dDateIns = value; } }
        public int User_ID { get { return _iUser_ID; } set { _iUser_ID = value; } }
        public int Status { get { return _iStatus; } set { _iStatus = value; } }
        public string ClientName { get { return _sClientName; } set { _sClientName = value; } }
        public string FileName { get { return _sFileName; } set { _sFileName = value; } }
        public string OldFileName { get { return _sOldFileName; } set { _sOldFileName = value; } }
        public string NewFileName { get { return _sNewFileName; } set { _sNewFileName = value; } }
        public string FullFileName { get { return _sFullFileName; } set { _sFullFileName = value; } }
        public string ContractCode { get { return _sContractCode; } set { _sContractCode = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
