using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsProducts
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;

        private int _iRecord_ID;
        private int _iProduct_ID;
        private int _iAktive;
        private DataTable _dtList;

        public clsProducts()
        {
            this._iRecord_ID = 0;
            this._iProduct_ID = 0;
            this._iAktive = 1;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("sp_GetShare", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iProduct_ID = Convert.ToInt32(drList["ShareType"]);
                    this._iAktive = Convert.ToInt32(drList["Aktive"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetProductTypes()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("Title", typeof(string));
            _dtList.Columns.Add("CalcFees", typeof(int));
            SqlDataReader drList = null;

            try
            {
                _dtList.Rows.Add(0, "Όλα", 0);

                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Products"));
                cmd.Parameters.Add(new SqlParameter("@Col", ""));
                cmd.Parameters.Add(new SqlParameter("@Value", ""));
                cmd.Parameters.Add(new SqlParameter("@Order", "ID"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["Title"], drList["CalcFees"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetUsing()
        {
            this._iAktive = 0;

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetShareUsing", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Share_ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iAktive = 1;
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
                using (SqlCommand cmd = new SqlCommand("InsertShare", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ShareType", SqlDbType.Int).Value = this._iProduct_ID;
                    cmd.Parameters.Add("@Aktive", SqlDbType.Int).Value = this._iAktive;

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
                using (SqlCommand cmd = new SqlCommand("EditShare", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = this._iRecord_ID;
                    cmd.Parameters.Add("@ShareType", SqlDbType.Int).Value = this._iProduct_ID;
                    cmd.Parameters.Add("@Aktive", SqlDbType.Int).Value = this._iAktive;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Shares";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return _iRecord_ID; } set { _iRecord_ID = value; } }
        public int Product_ID { get { return _iProduct_ID; } set { _iProduct_ID = value; } }
        public int Aktive { get { return _iAktive; } set { _iAktive = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
