using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsProductsRecomLogs
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int      _iRecord_ID;
        private int      _iShareCodes_ID;
        private int      _iEditAktion;
        private DateTime _dEditDate;

        private int      _iProduct_ID;
        private DateTime _dFrom;
        private DateTime _dTo;
        private DataTable _dtList;
        public clsProductsRecomLogs()
        {
            this._iRecord_ID = 0;
            this._iShareCodes_ID = 0;
            this._iEditAktion = 0;
            this._dEditDate = Convert.ToDateTime("1900/01/01");
            this._iProduct_ID = 0;
            this._dFrom = Convert.ToDateTime("1900/01/01");
            this._dTo = Convert.ToDateTime("2070/12/31");
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "ProductsRecom_Log"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", _iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iShareCodes_ID = Convert.ToInt32(drList["ShareCodes_ID"]);
                    this._iEditAktion = Convert.ToInt32(drList["EditAktion"]);
                    this._dEditDate = Convert.ToDateTime(drList["EditDate"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable("ProductsCodes_List");
            dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("EditDate", System.Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("AktionTitle", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Share_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ISIN", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Code2", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Code3", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Product_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ProductCategory_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ShareCode_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ProductCategory_ID", System.Type.GetType("System.Int32"));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetProductsRecom_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dTo));
                cmd.Parameters.Add(new SqlParameter("@Product_ID", _iProduct_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = this._dtList.NewRow();
                    this.dtRow["EditDate"] = Convert.ToDateTime(drList["EditDate"]).ToString("dd/MM/yyyy");
                    this.dtRow["AktionTitle"] = Convert.ToInt32(drList["EditAktion"]) == 1 ? "Add" : "Delete";
                    this.dtRow["Share_Title"] = drList["Share_Title"] + "";
                    this.dtRow["ISIN"] = drList["ISIN"] + "";
                    this.dtRow["Code"] = drList["Code"] + "";
                    this.dtRow["Code2"] = drList["Code2"] + "";
                    this.dtRow["Code3"] = drList["Code3"] + "";
                    this.dtRow["Product_Title"] = drList["Product_Title"] + "";
                    this.dtRow["ProductCategory_Title"] = drList["ProductCategory_Title"] + "";
                    this.dtRow["ShareCode_ID"] = drList["ShareCodes_ID"];
                    this.dtRow["Product_ID"] = drList["ShareType"];
                    this.dtRow["ProductCategory_ID"] = drList["ProductCategory_ID"];

                    this._dtList.Rows.Add(dtRow);
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
                using (SqlCommand cmd = new SqlCommand("InsertProductsRecom_Log", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ShareCodes_ID", SqlDbType.Int).Value = this._iShareCodes_ID;
                    cmd.Parameters.Add("@EditAktion", SqlDbType.Int).Value = this._iEditAktion;
                    cmd.Parameters.Add("@EditDate", SqlDbType.DateTime).Value = this._dEditDate;
                    cmd.ExecuteNonQuery();
                    _iRecord_ID = Convert.ToInt32(outParam.Value);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            return _iRecord_ID;
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int ShareCodes_ID { get { return this._iShareCodes_ID; } set { this._iShareCodes_ID = value; } }
        public int EditAktion { get { return this._iEditAktion; } set { this._iEditAktion = value; } }
        public DateTime EditDate { get { return this._dEditDate; } set { this._dEditDate = value; } }
        public int Product_ID { get { return this._iProduct_ID; } set { this._iProduct_ID = value; } }
        public DateTime DateFrom { get { return this._dFrom; } set { this._dFrom = value; } }
        public DateTime DateTo { get { return this._dTo; } set { this._dTo = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
