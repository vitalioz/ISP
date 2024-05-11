using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsContracts_ComplexSigns
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;

        private int _iRecord_ID;
        private int _iContract_ID;
        private int _iComplexSign_ID;

        private DataTable _dtList;

        public clsContracts_ComplexSigns()
        {
            this._iRecord_ID = 0;
            this._iContract_ID = 0;
            this._iComplexSign_ID = 0;
        }
        public void GetRecord()
        {
            SqlConnection conn = new SqlConnection(Global.connStr);
            drList = null;
            try {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Contracts_ComplexSigns"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iContract_ID = Convert.ToInt32(drList["Contract_ID"]);
                    this._iComplexSign_ID = Convert.ToInt32(drList["ComplexSign_ID"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("Contract_ID", typeof(int));
            _dtList.Columns.Add("ComplexSign_ID", typeof(int));
            _dtList.Columns.Add("ComplexSign_Title", typeof(string));

            SqlConnection conn = new SqlConnection(Global.connStr);
            drList = null;
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetContracts_ComplexSigns", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Contract_ID", _iContract_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["Contract_ID"], drList["ComplexSign_ID"], drList["ComplexSign_Title"]);
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
                using (cmd = new SqlCommand("InsertContracts_ComplexSigns", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@ComplexSign_ID", SqlDbType.Int).Value = _iComplexSign_ID;
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
                using (cmd = new SqlCommand("EditContracts_ComplexSigns", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = this._iRecord_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@ComplexSign_ID", SqlDbType.Int).Value = _iComplexSign_ID;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Contracts_ComplexSigns";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "Contract_ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = this._iContract_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID  { get { return _iRecord_ID; } set { _iRecord_ID = value; } }      
        public int Contract_ID { get { return _iContract_ID; } set { _iContract_ID = value; } }
        public int ComplexSign_ID { get { return _iComplexSign_ID; } set { _iComplexSign_ID = value; } }  
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
