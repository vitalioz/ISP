using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsProductsDocFiles
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlDataReader drList = null;

        private int _iRecord_ID;
        private int _iShare_ID;
        private int _iPreContract_ID;
        private int _iContract_ID;
        private int _iDocTypes;
        private int _iDMS_Files_ID;
        private int _iOldFile;
        private DateTime _dDateIns;
        private int _iAuthor_ID;

        private string _sFullFileName;
        private string _sOldFileName;
        private string _sNewFileName;
        private string _sClientName;
        private string _sContractCode;
        private string _sTemp;

        private DataTable _dtList;

        public clsProductsDocFiles()
        {
            this._iRecord_ID = 0;
            this._iShare_ID = 0;
            this._iPreContract_ID = 0;
            this._iContract_ID = 0;
            this._iDocTypes = 0;
            this._iDMS_Files_ID = 0;
            this._iOldFile = 0;
            this._dDateIns = Convert.ToDateTime("1900/01/01");
            this._iAuthor_ID = 0;
            this._iContract_ID = 0;
            this._sNewFileName = "";
            this._sTemp = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "ClientsDocFiles"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iShare_ID = 0;
                    this._iPreContract_ID = 0;
                    this._iContract_ID = 0;
                    this._iDocTypes = 0;
                    this._iDMS_Files_ID = 0;
                    this._iOldFile = 0;
                    this._dDateIns = Convert.ToDateTime("1900/01/01");
                    this._iAuthor_ID = 0;
                    this._iContract_ID = 0;
                    this._sNewFileName = "";
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
            _dtList.Columns.Add("Share_ID", typeof(int));
            _dtList.Columns.Add("Code", typeof(string));
            _dtList.Columns.Add("PreContract_ID", typeof(int));
            _dtList.Columns.Add("Contract_ID", typeof(int));
            _dtList.Columns.Add("DocTypes", typeof(int));
            _dtList.Columns.Add("Tipos", typeof(string));
            _dtList.Columns.Add("DMS_Files_ID", typeof(int));
            _dtList.Columns.Add("FilePath", typeof(string));
            _dtList.Columns.Add("FileName", typeof(string));
            _dtList.Columns.Add("OldFile", typeof(int));
            _dtList.Columns.Add("DateIns", typeof(DateTime));
            _dtList.Columns.Add("Author_ID", typeof(int));

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetClient_DocFiles", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Share_ID", _iShare_ID));
                cmd.Parameters.Add(new SqlParameter("@PreContract_ID", _iPreContract_ID));
                cmd.Parameters.Add(new SqlParameter("@Contract_ID", _iContract_ID));
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
                    _dtList.Rows.Add(drList["ID"], drList["Share_ID"], drList["Code"], drList["PreContract_ID"], drList["Contract_ID"], drList["DocTypes"],
                                     drList["Tipos"], drList["DMS_Files_ID"], _sTemp, drList["FileName"], drList["OldFile"], drList["DateIns"], drList["Author_ID"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int InsertRecord()
        {


            using (var conn = new SqlConnection(Global.connStr))
            using (var command = new SqlCommand("Insert_ClientsDocFiles", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                command.ExecuteNonQuery();
            }

            return 0;
        }
        public void EditRecord()
        {

        }
        public void DeleteRecord()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ProductsDocFiles";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return _iRecord_ID; } set { _iRecord_ID = value; } }
        public int Share_ID { get { return _iShare_ID; } set { _iShare_ID = value; } }
        public int PreContract_ID { get { return _iPreContract_ID; } set { _iPreContract_ID = value; } }
        public int Contract_ID { get { return _iContract_ID; } set { _iContract_ID = value; } }
        public int DocTypes { get { return _iDocTypes; } set { _iDocTypes = value; } }
        public int DMS_Files_ID { get { return _iDMS_Files_ID; } set { _iDMS_Files_ID = value; } }
        public int OldFile { get { return _iOldFile; } set { _iOldFile = value; } }
        public DateTime DateIns { get { return _dDateIns; } set { _dDateIns = value; } }
        public int Author_ID { get { return _iAuthor_ID; } set { _iAuthor_ID = value; } }
        public string ClientName { get { return _sClientName; } set { _sClientName = value; } }
        public string OldFileName { get { return _sOldFileName; } set { _sOldFileName = value; } }
        public string NewFileName { get { return _sNewFileName; } set { _sNewFileName = value; } }
        public string FullFileName { get { return _sFullFileName; } set { _sFullFileName = value; } }
        public string ContractCode { get { return _sContractCode; } set { _sContractCode = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
