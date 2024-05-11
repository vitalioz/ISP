using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsContracts_PeriodicalEvaluation
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int    _iRecord_ID;
        private int    _iContract_ID;
        private int    _iYear;
        private string _sFileName;
        private string _sDateSent;

        private DataTable _dtList;
        public clsContracts_PeriodicalEvaluation()
        {
            this._iRecord_ID = 0;
            this._iContract_ID = 0;
            this._iYear = 1900;
            this._sFileName = "";
            this._sDateSent = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Contracts_PeriodicalEvaluation"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", _iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iContract_ID = Convert.ToInt32(drList["Contract_ID"]);
                    this._iYear = Convert.ToInt32(drList["Year"]);
                    this._sFileName = drList["FileName"]+"";
                    this._sDateSent = drList["DateSent"] + "";
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }     
        public void GetList()
        {
            _dtList = new DataTable("Contract_Blocks_List");
            dtCol = _dtList.Columns.Add("ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Contract_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Contract_Details_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Contract_Packages_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ContractTipos", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ClientTipos", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Surname", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Firstname", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ProviderTitle", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ConnectionMethod_ID", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("EMail", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Address", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("City", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ZIP", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Country_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("User1_Name", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("BornPlace", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Year", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Filename", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("DateSent", System.Type.GetType("System.String"));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetPeriodicalEvaluation_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Year", _iYear));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = this._dtList.NewRow();
                    this.dtRow["ID"] = Convert.ToInt32(drList["ID"]);
                    this.dtRow["Contract_ID"] = Convert.ToInt32(drList["Contract_ID"]);
                    this.dtRow["Contract_Details_ID"] = Convert.ToInt32(drList["Contracts_Details_ID"]);
                    this.dtRow["Contract_Packages_ID"] = Convert.ToInt32(drList["Contracts_Packages_ID"]);
                    this.dtRow["ContractTipos"] = Convert.ToInt32(drList["Tipos"]);
                    this.dtRow["ContractTitle"] = drList["ContractTitle"]+"";
                    this.dtRow["Client_ID"] = Convert.ToInt32(drList["Client_ID"]);
                    this.dtRow["ClientTipos"] = Convert.ToInt32(drList["ClientTipos"]);
                    this.dtRow["Surname"] = drList["Surname"] + "";
                    this.dtRow["Firstname"] = drList["Firstname"] + "";
                    this.dtRow["ProviderTitle"] = drList["ProviderTitle"] + "";
                    this.dtRow["Code"] = drList["Code"] + "";
                    this.dtRow["Portfolio"] = drList["Portfolio"] + "";
                    this.dtRow["ConnectionMethod_ID"] = Convert.ToInt32(drList["ConnectionMethod"]);
                    this.dtRow["EMail"] = drList["EMail"] + "";
                    this.dtRow["Address"] = drList["Address"] + "";
                    this.dtRow["City"] = drList["City"] + "";
                    this.dtRow["ZIP"] = drList["ZIP"] + "";
                    this.dtRow["Country_Title"] = drList["Country_Title"] + "";
                    this.dtRow["User1_Name"] = (drList["Surname"] + " " + drList["Firstname"]).Trim();
                    this.dtRow["BornPlace"] = drList["BornPlace"] + "";
                    this.dtRow["Year"] = Convert.ToInt32(drList["Year"]);
                    this.dtRow["Filename"] = drList["Filename"] + "";
                    this.dtRow["DateSent"] = drList["DateSent"] + "";
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
                using (SqlCommand cmd = new SqlCommand("InsertContracts_PeriodicalEvaluation", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = this._iContract_ID;
                    cmd.Parameters.Add("@Year", SqlDbType.Int).Value = this._iYear;
                    cmd.Parameters.Add("@FileName", SqlDbType.NVarChar, 100).Value = this._sFileName;
                    cmd.Parameters.Add("@DateSent", SqlDbType.NVarChar, 20).Value = this._sDateSent;
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
                using (SqlCommand cmd = new SqlCommand("EditContracts_PeriodicalEvaluation", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = this._iRecord_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = this._iContract_ID;
                    cmd.Parameters.Add("@Year", SqlDbType.Int).Value = this._iYear;
                    cmd.Parameters.Add("@FileName", SqlDbType.NVarChar, 100).Value = this._sFileName;
                    cmd.Parameters.Add("@DateSent", SqlDbType.NVarChar, 20).Value = this._sDateSent;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Contracts_PeriodicalEvaluation";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }

        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int Contract_ID { get { return this._iContract_ID; } set { this._iContract_ID = value; } }
        public int Year { get { return this._iYear; } set { this._iYear = value; } }
        public string FileName { get { return this._sFileName; } set { this._sFileName = value; } }
        public string DateSent { get { return this._sDateSent; } set { this._sDateSent = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
