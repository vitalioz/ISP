using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsExPostCost_Recs
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int _iRecord_ID;
        private int _iEPCT_ID;
        private int _iContract_ID;
        private int _iContract_Details_ID;
        private int _iContract_Packages_ID;
        private decimal _decAverageExchangedFund;
        private decimal _decNetRTOFees;
        private decimal _decNetManagmetFees;
        private decimal _decNetSuccessFees;
        private decimal _decNetAdminFees;
        private decimal _decNetFXFees;
        private decimal _decVAT;
        private decimal _decTotalFees;
        private decimal _decRTOFees_Percent;
        private decimal _decManagmetFees_Percent;
        private decimal _decSuccessFees_Percent;
        private decimal _decAdminFees_Percent;
        private decimal _decFXFees_Percent;
        private decimal _decVAT_Percent;
        private decimal _decTotal_Percent;
        private string _sFileName;
        private string _sDateSent;

        private DateTime _dDateFrom;
        private DateTime _dDateTo;
        private DataTable _dtList;

        public clsExPostCost_Recs()
        {
            this._iRecord_ID = 0;
            this._iEPCT_ID = 0;
            this._iContract_ID = 0;
            this._iContract_Details_ID = 0;
            this._iContract_Packages_ID = 0;
            this._dDateFrom = Convert.ToDateTime("1900/01/01");
            this._dDateTo = Convert.ToDateTime("1900/01/01");
            this._decAverageExchangedFund = 0;
            this._decNetRTOFees = 0;
            this._decNetManagmetFees = 0;
            this._decNetSuccessFees = 0;
            this._decNetAdminFees = 0;
            this._decNetFXFees = 0;
            this._decVAT = 0;
            this._decTotalFees = 0;
            this._decRTOFees_Percent = 0;
            this._decManagmetFees_Percent = 0;
            this._decSuccessFees_Percent = 0;
            this._decAdminFees_Percent = 0;
            this._decFXFees_Percent = 0;
            this._decVAT_Percent = 0;
            this._decTotal_Percent = 0;
            this._sFileName = "";
            this._sDateSent = "";

            this._dDateFrom = Convert.ToDateTime("1900/01/01");
            this._dDateTo = Convert.ToDateTime("1900/01/01");
        }

        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "ExPostCost_Recs"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = drList.GetInt32(0);
                    this._iEPCT_ID = Convert.ToInt32(drList["EPCT_ID"]);
                    this._iContract_ID = Convert.ToInt32(drList["Contract_ID"]);
                    this._iContract_Details_ID = Convert.ToInt32(drList["Contract_Details_ID"]);
                    this._iContract_Packages_ID = Convert.ToInt32(drList["Contract_Packages_ID"]);
                    this._dDateFrom = Convert.ToDateTime(drList["DateFrom"]);
                    this._dDateTo = Convert.ToDateTime(drList["DateTo"]);
                    this._decAverageExchangedFund = Convert.ToDecimal(drList["AverageExchangedFund"]);
                    this._decNetRTOFees = Convert.ToDecimal(drList["NetRTOFees"]);
                    this._decNetManagmetFees = Convert.ToDecimal(drList["NetManagmetFees"]);
                    this._decNetSuccessFees = Convert.ToDecimal(drList["NetSuccessFees"]);
                    this._decNetAdminFees = Convert.ToDecimal(drList["NetAdminFees"]);
                    this._decNetFXFees = Convert.ToDecimal(drList["NetFXFees"]);
                    this._decVAT = Convert.ToDecimal(drList["VAT"]);
                    this._decTotalFees = Convert.ToDecimal(drList["TotalFees"]);
                    this._decRTOFees_Percent = Convert.ToDecimal(drList["RTOFees_Percent"]);
                    this._decManagmetFees_Percent = Convert.ToDecimal(drList["ManagmetFees_Percent"]);
                    this._decSuccessFees_Percent = Convert.ToDecimal(drList["SuccessFees_Percent"]);
                    this._decAdminFees_Percent = Convert.ToDecimal(drList["AdminFees_Percent"]);
                    this._decFXFees_Percent = Convert.ToDecimal(drList["FXFees_Percent"]);
                    this._decVAT_Percent = Convert.ToDecimal(drList["VAT_Percent"]);
                    this._decTotal_Percent = Convert.ToDecimal(drList["Total_Percent"]);
                    this._sFileName = drList["FileName"] + "";
                    this._sDateSent = drList["DateSent"] + "";
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
                _dtList = new DataTable("ExPostCostList");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("EPCT_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Surname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Firstname", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("User1_Name", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("BornPlace", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DateFrom", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DateTo", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Contract_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contracts_Details_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contracts_Packages_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AverageExchangedFund", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("NetRTOFees", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("NetManagmetFees", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("NetSuccessFees", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("NetAdminFees", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("NetFXFees", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("VAT", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("TotalFees", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("RTOFees_Percent", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("ManagmetFees_Percent", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("SuccessFees_Percent", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("AdminFees_Percent", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("FXFees_Percent", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("VAT_Percent", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("Total_Percent", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("FileName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DateSent", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ServiceProvider_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ServiceProvider_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Service_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Service_Title", System.Type.GetType("System.String"));

                dtCol = _dtList.Columns.Add("ConnectionMethod_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ContractTipos", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Address", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("City", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ZIP", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Country_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("EMail", System.Type.GetType("System.String"));

                conn.Open();
                cmd = new SqlCommand("GetExPostCostRecs_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EPCT_ID", _iEPCT_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["EPCT_ID"] = drList["EPCT_ID"];
                    dtRow["ContractTitle"] = drList["ContractTitle"];
                    dtRow["Code"] = drList["Code"];
                    dtRow["Currency"] = drList["Currency"];
                    dtRow["Portfolio"] = drList["Portfolio"];
                    dtRow["DateFrom"] = drList["DateFrom"];
                    dtRow["DateTo"] = drList["DateTo"];
                    dtRow["Client_ID"] = drList["Client_ID"];
                    dtRow["Surname"] = drList["Surname"] + "";
                    dtRow["Firstname"] = drList["Firstname"] + "";
                    dtRow["User1_Name"] = drList["InvName"] + "";
                    dtRow["BornPlace"] = drList["BornPlace"] + "";
                    dtRow["Contract_ID"] = drList["Contract_ID"];
                    dtRow["Contracts_Details_ID"] = drList["Contract_Details_ID"];
                    dtRow["Contracts_Packages_ID"] = drList["Contract_Packages_ID"];
                    dtRow["AverageExchangedFund"] = drList["AverageExchangedFund"];
                    dtRow["NetRTOFees"] = drList["NetRTOFees"];
                    dtRow["NetManagmetFees"] = drList["NetManagmetFees"];
                    dtRow["NetSuccessFees"] = drList["NetSuccessFees"];
                    dtRow["NetAdminFees"] = drList["NetAdminFees"];
                    dtRow["NetFXFees"] = drList["NetFXFees"];
                    dtRow["VAT"] = drList["VAT"];
                    dtRow["TotalFees"] = drList["TotalFees"];
                    dtRow["RTOFees_Percent"] = drList["RTOFees_Percent"];
                    dtRow["ManagmetFees_Percent"] = drList["ManagmetFees_Percent"];
                    dtRow["SuccessFees_Percent"] = drList["SuccessFees_Percent"];
                    dtRow["AdminFees_Percent"] = drList["AdminFees_Percent"];
                    dtRow["FXFees_Percent"] = drList["FXFees_Percent"];
                    dtRow["VAT_Percent"] = drList["VAT_Percent"];
                    dtRow["Total_Percent"] = drList["Total_Percent"];
                    dtRow["FileName"] = drList["FileName"] + "";
                    dtRow["DateSent"] = drList["DateSent"];
                    dtRow["ServiceProvider_ID"] = drList["PackageProvider_ID"];
                    dtRow["ServiceProvider_Title"] = drList["ServiceProvider_Title"];
                    dtRow["Service_ID"] = drList["Service_ID"];
                    dtRow["Service_Title"] = drList["Service_Title"];
                    dtRow["ConnectionMethod_ID"] = drList["ConnectionMethod_ID"];
                    dtRow["ContractTipos"] = drList["ContractTipos"];
                    dtRow["Address"] = drList["Address"] + "";
                    dtRow["City"] = drList["City"] + "";
                    dtRow["ZIP"] = drList["ZIP"] + "";
                    dtRow["Country_Title"] = drList["Country_Title"] + "";
                    dtRow["EMail"] = drList["EMail"] + "";

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
                using (SqlCommand cmd = new SqlCommand("InsertExPostCost_Rec", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@EPCT_ID", SqlDbType.Int).Value = _iEPCT_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Details_ID", SqlDbType.Int).Value = _iContract_Details_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = _dDateFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = _dDateTo;
                    cmd.Parameters.Add("@AverageExchangedFund", SqlDbType.Float).Value = _decAverageExchangedFund;
                    cmd.Parameters.Add("@NetRTOFees", SqlDbType.Float).Value = _decNetRTOFees;
                    cmd.Parameters.Add("@NetManagmetFees", SqlDbType.Float).Value = _decNetManagmetFees;
                    cmd.Parameters.Add("@NetSuccessFees", SqlDbType.Float).Value = _decNetSuccessFees;
                    cmd.Parameters.Add("@NetAdminFees", SqlDbType.Float).Value = _decNetAdminFees;
                    cmd.Parameters.Add("@NetFXFees", SqlDbType.Float).Value = _decNetFXFees;
                    cmd.Parameters.Add("@VAT", SqlDbType.Float).Value = _decVAT;
                    cmd.Parameters.Add("@TotalFees", SqlDbType.Float).Value = _decTotalFees;
                    cmd.Parameters.Add("@RTOFees_Percent", SqlDbType.Decimal).Value = _decRTOFees_Percent;
                    cmd.Parameters.Add("@ManagmetFees_Percent", SqlDbType.Decimal).Value = _decManagmetFees_Percent;
                    cmd.Parameters.Add("@SuccessFees_Percent", SqlDbType.Float).Value = _decSuccessFees_Percent;
                    cmd.Parameters.Add("@AdminFees_Percent", SqlDbType.Float).Value = _decAdminFees_Percent;
                    cmd.Parameters.Add("@FXFees_Percent", SqlDbType.Float).Value = _decFXFees_Percent;
                    cmd.Parameters.Add("@VAT_Percent", SqlDbType.Decimal).Value = _decVAT_Percent;
                    cmd.Parameters.Add("@Total_Percent", SqlDbType.Decimal).Value = _decTotal_Percent;
                    cmd.Parameters.Add("@FileName", SqlDbType.NVarChar, 50).Value = _sFileName;
                    cmd.Parameters.Add("@DateSent", SqlDbType.NVarChar, 20).Value = _sDateSent;

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
                using (SqlCommand cmd = new SqlCommand("EditExPostCost_Rec", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@EPCT_ID", SqlDbType.Int).Value = _iEPCT_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Details_ID", SqlDbType.Int).Value = _iContract_Details_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = _dDateFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = _dDateTo;
                    cmd.Parameters.Add("@AverageExchangedFund", SqlDbType.Float).Value = _decAverageExchangedFund;
                    cmd.Parameters.Add("@NetRTOFees", SqlDbType.Float).Value = _decNetRTOFees;
                    cmd.Parameters.Add("@NetManagmetFees", SqlDbType.Float).Value = _decNetManagmetFees;
                    cmd.Parameters.Add("@NetSuccessFees", SqlDbType.Float).Value = _decNetSuccessFees;
                    cmd.Parameters.Add("@NetAdminFees", SqlDbType.Float).Value = _decNetAdminFees;
                    cmd.Parameters.Add("@NetFXFees", SqlDbType.Float).Value = _decNetFXFees;
                    cmd.Parameters.Add("@VAT", SqlDbType.Float).Value = _decVAT;
                    cmd.Parameters.Add("@TotalFees", SqlDbType.Float).Value = _decTotalFees;
                    cmd.Parameters.Add("@RTOFees_Percent", SqlDbType.Decimal).Value = _decRTOFees_Percent;
                    cmd.Parameters.Add("@ManagmetFees_Percent", SqlDbType.Decimal).Value = _decManagmetFees_Percent;
                    cmd.Parameters.Add("@SuccessFees_Percent", SqlDbType.Float).Value = _decSuccessFees_Percent;
                    cmd.Parameters.Add("@AdminFees_Percent", SqlDbType.Float).Value = _decAdminFees_Percent;
                    cmd.Parameters.Add("@FXFees_Percent", SqlDbType.Float).Value = _decFXFees_Percent;
                    cmd.Parameters.Add("@VAT_Percent", SqlDbType.Decimal).Value = _decVAT_Percent;
                    cmd.Parameters.Add("@Total_Percent", SqlDbType.Decimal).Value = _decTotal_Percent;
                    cmd.Parameters.Add("@FileName", SqlDbType.NVarChar, 50).Value = _sFileName;
                    cmd.Parameters.Add("@DateSent", SqlDbType.NVarChar, 20).Value = _sDateSent;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ExPostCost_Recs";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int EPCT_ID { get { return this._iEPCT_ID; } set { this._iEPCT_ID = value; } }
        public int Contract_ID { get { return this._iContract_ID; } set { this._iContract_ID = value; } }
        public int Contract_Details_ID { get { return this._iContract_Details_ID; } set { this._iContract_Details_ID = value; } }
        public int Contract_Packages_ID { get { return this._iContract_Packages_ID; } set { this._iContract_Packages_ID = value; } }
        public DateTime DateFrom { get { return this._dDateFrom; } set { this._dDateFrom = value; } }
        public DateTime DateTo { get { return this._dDateTo; } set { this._dDateTo = value; } }
        public decimal AverageExchangedFund { get { return this._decAverageExchangedFund; } set { this._decAverageExchangedFund = value; } }
        public decimal NetRTOFees { get { return this._decNetRTOFees; } set { this._decNetRTOFees = value; } }
        public decimal NetManagmetFees { get { return this._decNetManagmetFees; } set { this._decNetManagmetFees = value; } }
        public decimal NetSuccessFees { get { return this._decNetSuccessFees; } set { this._decNetSuccessFees = value; } }
        public decimal NetAdminFees { get { return this._decNetAdminFees; } set { this._decNetAdminFees = value; } }
        public decimal NetFXFees { get { return this._decNetFXFees; } set { this._decNetFXFees = value; } }
        public decimal VAT { get { return this._decVAT; } set { this._decVAT = value; } }
        public decimal TotalFees { get { return this._decTotalFees; } set { this._decTotalFees = value; } }
        public decimal RTOFees_Percent { get { return this._decRTOFees_Percent; } set { this._decRTOFees_Percent = value; } }
        public decimal ManagmetFees_Percent { get { return this._decManagmetFees_Percent; } set { this._decManagmetFees_Percent = value; } }
        public decimal SuccessFees_Percent { get { return this._decSuccessFees_Percent; } set { this._decSuccessFees_Percent = value; } }
        public decimal AdminFees_Percent { get { return this._decAdminFees_Percent; } set { this._decAdminFees_Percent = value; } }
        public decimal FXFees_Percent { get { return this._decFXFees_Percent; } set { this._decFXFees_Percent = value; } }
        public decimal VAT_Percent { get { return this._decVAT_Percent; } set { this._decVAT_Percent = value; } }
        public decimal Total_Percent { get { return this._decTotal_Percent; } set { this._decTotal_Percent = value; } }
        public string FileName { get { return this._sFileName; } set { this._sFileName = value; } }
        public string DateSent { get { return this._sDateSent; } set { this._sDateSent = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}