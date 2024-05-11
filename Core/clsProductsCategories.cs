using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsProductsCategories
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlDataReader drList = null;

        private int       _iRecord_ID;
        private int       _iProduct_ID;
        private string    _sTitle;
        private string    _sInvestGoal;
        private DataTable _dtList;

        public clsProductsCategories()
        {
            this._iRecord_ID = 0;
            this._iProduct_ID = 0;
            this._sTitle = "";
            this._sInvestGoal = "";           
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Products_Categories"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iProduct_ID = Convert.ToInt32(drList["Product_ID"]);
                    this._sTitle = drList["Title"].ToString();
                    this._sInvestGoal = drList["InvestGoal"].ToString();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("Product_ID", typeof(int));
            _dtList.Columns.Add("ProductTitle", typeof(string));
            _dtList.Columns.Add("Title", typeof(string));
            _dtList.Columns.Add("InvestGoal", typeof(string));

            _dtList.Rows.Add(0, 0, "Όλοι", "");

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetProduct_ProductsCategories", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ProductCategories_ID"], drList["Product_ID"], drList["Product_Title"], drList["ProductCategories_Title"], drList["InvestGoal"]);
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
                using (SqlCommand cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Products_Categories";
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
        public string Title { get { return _sTitle; } set { _sTitle = value; } }
        public string InvestGoal { get { return _sInvestGoal; } set { _sInvestGoal = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
