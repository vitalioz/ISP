using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsInvoicesTypes
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataRow dtRow;

        private int    _iRecord_ID;
        private string _sTitle;
        private string _sTitleEn;
        private string _sCode;
        private string _sType;

        private DataTable _dtList;

        public clsInvoicesTypes()
        {
            this._iRecord_ID = 0;
            this._sTitle = "";
            this._sTitleEn = "";
            this._sCode = "";
            this._sType = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "InvoicesTypes"));
                cmd.Parameters.Add(new SqlParameter("@Col", ""));
                cmd.Parameters.Add(new SqlParameter("@Value", ""));
                cmd.Parameters.Add(new SqlParameter("@Order", "Title"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._sTitle = drList["Title"].ToString();
                    this._sTitleEn = drList["TitleEn"].ToString();
                    this._sCode = drList["Code"].ToString();
                    this._sType = drList["Tipos"].ToString();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("Title", typeof(string));
            _dtList.Columns.Add("TitleEn", typeof(string));
            _dtList.Columns.Add("Code", typeof(string));
            _dtList.Columns.Add("Type", typeof(string));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "InvoicesTypes"));
                cmd.Parameters.Add(new SqlParameter("@Col", ""));
                cmd.Parameters.Add(new SqlParameter("@Value", ""));
                cmd.Parameters.Add(new SqlParameter("@Order", "Title"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["Title"] = drList["Title"];
                    dtRow["TitleEn"] = drList["TitleEn"];
                    dtRow["Code"] = drList["Code"];
                    dtRow["Type"] = drList["Tipos"];
                    _dtList.Rows.Add(dtRow);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void InsertRecord()
        {

        }
        public void EditRecord()
        {

        }
        public void DeleteRecord()
        {
            try
            {
                conn.Open();
                using (cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "InvoicesTypes";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public string Title  { get { return _sTitle; }  set { _sTitle = value; } }
        public string TitleEn { get { return _sTitleEn; } set { _sTitleEn = value; } }
        public string Code { get { return _sCode; } set { _sCode = value; } }
        public string Type { get { return this._sType; } set { this._sType = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }

    }
}
