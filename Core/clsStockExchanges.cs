using System;                                    
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsStockExchanges
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int    _iRecord_ID;
        private int    _iTipos;
        private int    _iParent_ID;
        private int    _iSortIndex;
        private string _sCode;
        private string _sTitle;
        private string _sReutersCode;
        private string _sBloombergCode;
        private string _sMstarTitle;
        private int    _iCountry_ID;

        private DataTable _dtList;

        public clsStockExchanges()
        {
            this._iRecord_ID = 0;
            this._iTipos = 0;
            this._iParent_ID = 0;
            this._iSortIndex = 0;
            this._sCode = "";
            this._sTitle = "";
            this._sReutersCode = "";
            this._sBloombergCode = "";
            this._sMstarTitle = "";
            this._iCountry_ID = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "StockExchanges"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iTipos = Convert.ToInt32(drList["Tipos"]);
                    this._iParent_ID = Convert.ToInt32(drList["Parent_ID"]);
                    this._iSortIndex = Convert.ToInt32(drList["SortIndex"]);
                    this._sCode = drList["Code"] + "";
                    this._sTitle = drList["Title"] + "";
                    this._sReutersCode = drList["ReutersCode"] + "";
                    this._sBloombergCode = drList["BloombergCode"] + "";
                    this._sMstarTitle = drList["MstarTitle"] + "";
                    this._iCountry_ID = Convert.ToInt32(drList["Country_ID"]);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            try
            {
                _dtList = new DataTable("StockExchanges_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Tipos", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Parent_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("SortIndex", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ReutersCode", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("BloombergCode", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("MstarTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Country_ID", System.Type.GetType("System.Int32"));

                dtRow = _dtList.NewRow();
                dtRow["ID"] = 0;
                dtRow["Tipos"] = _iTipos;
                dtRow["Parent_ID"] = 0;
                dtRow["SortIndex"] = 0;
                dtRow["Code"] = "";
                dtRow["Title"] = "";
                dtRow["ReutersCode"] = "";
                dtRow["BloombergCode"] = "";
                dtRow["MstarTitle"] = "";
                dtRow["Country_ID"] = 0;
                _dtList.Rows.Add(dtRow);

                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "StockExchanges"));
                cmd.Parameters.Add(new SqlParameter("@Col", ""));
                cmd.Parameters.Add(new SqlParameter("@Value", ""));
                cmd.Parameters.Add(new SqlParameter("@Order", "Title"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["Tipos"] = drList["Tipos"];
                    dtRow["Parent_ID"] = drList["Parent_ID"];
                    dtRow["SortIndex"] = drList["SortIndex"];
                    dtRow["Code"] = drList["Code"];
                    dtRow["Title"] = drList["Title"];
                    dtRow["ReutersCode"] = drList["ReutersCode"];
                    dtRow["BloombergCode"] = drList["BloombergCode"];
                    dtRow["MstarTitle"] = drList["MstarTitle"];
                    dtRow["Country_ID"] = drList["Country_ID"];
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList_Tree()
        {
            try
            {
                _dtList = new DataTable("StockExchanges_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Tipos", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Parent_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("SortIndex", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ReutersCode", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("BloombergCode", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("MstarTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Country_ID", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetStockExchanges_Tree", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["Tipos"] = drList["Tipos"];
                    dtRow["Parent_ID"] = drList["Parent_ID"];
                    dtRow["SortIndex"] = drList["SortIndex"];
                    dtRow["Code"] = drList["Code"];
                    dtRow["Title"] = drList["Title"];
                    dtRow["ReutersCode"] = drList["ReutersCode"];
                    dtRow["BloombergCode"] = drList["BloombergCode"];
                    dtRow["MstarTitle"] = drList["MstarTitle"];
                    dtRow["Country_ID"] = drList["Country_ID"];
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int InsertRecord()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("InsertStockExchanges", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Tipos", SqlDbType.Int).Value = _iTipos;
                    cmd.Parameters.Add("@Parent_ID", SqlDbType.Int).Value = _iParent_ID;
                    cmd.Parameters.Add("@SortIndex", SqlDbType.Int).Value = _iSortIndex;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 20).Value = _sCode;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 100).Value = _sTitle;
                    cmd.Parameters.Add("@ReutersCode", SqlDbType.NVarChar, 50).Value = _sReutersCode;
                    cmd.Parameters.Add("@BloombergCode", SqlDbType.NVarChar, 20).Value = _sBloombergCode;
                    cmd.Parameters.Add("@MstarTitle", SqlDbType.NVarChar, 100).Value = _sMstarTitle;
                    cmd.Parameters.Add("@Country_ID", SqlDbType.Int).Value = _iCountry_ID;
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
                using (SqlCommand cmd = new SqlCommand("EditStockExchanges", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Tipos", SqlDbType.Int).Value = _iTipos;
                    cmd.Parameters.Add("@Parent_ID", SqlDbType.Int).Value = _iParent_ID;
                    cmd.Parameters.Add("@SortIndex", SqlDbType.Int).Value = _iSortIndex;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 20).Value = _sCode;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 100).Value = _sTitle;
                    cmd.Parameters.Add("@ReutersCode", SqlDbType.NVarChar, 50).Value = _sReutersCode;
                    cmd.Parameters.Add("@BloombergCode", SqlDbType.NVarChar, 20).Value = _sBloombergCode;
                    cmd.Parameters.Add("@MstarTitle", SqlDbType.NVarChar, 100).Value = _sMstarTitle;
                    cmd.Parameters.Add("@Country_ID", SqlDbType.Int).Value = _iCountry_ID;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "StockExchanges";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int Tipos { get { return this._iTipos; } set { this._iTipos = value; } }
        public int Parent_ID { get { return this._iParent_ID; } set { this._iParent_ID = value; } }
        public int SortIndex { get { return this._iSortIndex; } set { this._iSortIndex = value; } }
        public string Code { get { return this._sCode; } set { this._sCode = value; } }
        public string Title { get { return this._sTitle; } set { this._sTitle = value; } }
        public string ReutersCode { get { return this._sReutersCode; } set { this._sReutersCode = value; } }       
        public string BloombergCode { get { return this._sBloombergCode; } set { this._sBloombergCode = value; } }
        public string MstarTitle { get { return this._sMstarTitle; } set { this._sMstarTitle = value; } }
        public int Country_ID { get { return this._iCountry_ID; } set { this._iCountry_ID = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}






