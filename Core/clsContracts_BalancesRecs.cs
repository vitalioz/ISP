using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsContracts_BalancesRecs
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int       _iRecord_ID;
        private int       _iCDP_ID;
        private int       _iShareCodes_ID;
        private int       _iProduct_ID;
        private int       _iProductCategory_ID;
        private int       _iProduct_Group;
        private int       _iDepository_ID;
        private DateTime  _dRefDate;
        private float     _fltTotalUnits;
        private decimal   _decAvgNetPrice;
        private float     _fltCurrentPrice;
        private float     _fltCurrentValue_RepCcy;
        private float     _fltUnrealized_ProdCcy_PRC;
        private float     _fltUnrealized_RepCcy_PRC;
        private float     _fltParticipation_PRC;
        private int       _iInvestCategories_ID;
        private int       _iIC_AA_Recs_ID;

        private DateTime _dDateFrom;
        private DateTime _dDateTo;
        private DataTable _dtList;

        public clsContracts_BalancesRecs()
        {
            this._iRecord_ID = 0;
            this._iCDP_ID = 0;
            this._iShareCodes_ID = 0;
            this._iProduct_ID = 0;
            this._iProductCategory_ID = 0;
            this._iProduct_Group = 0;
            this._iDepository_ID = 0;
            this._dRefDate = Convert.ToDateTime("1900/01/01");
            this._fltTotalUnits = 0;
            this._decAvgNetPrice = 0;
            this._fltCurrentPrice = 0;
            this._fltCurrentValue_RepCcy = 0;
            this._fltUnrealized_ProdCcy_PRC = 0;
            this._fltUnrealized_RepCcy_PRC = 0;
            this._fltParticipation_PRC = 0;
            this._iInvestCategories_ID = 0;
            this._iIC_AA_Recs_ID = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetContracts_BalancesRecs", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iCDP_ID = Convert.ToInt32(drList["CDP_ID"]);
                    this._iShareCodes_ID = Convert.ToInt32(drList["ShareCodes_ID"]);
                    this._iProduct_ID = Convert.ToInt32(drList["Product_ID"]);
                    this._iProductCategory_ID = Convert.ToInt32(drList["ProductCategory_ID"]);

                    if (_iProduct_ID == 1) this._iProduct_Group = 2;
                    else if (_iProduct_ID == 2) this._iProduct_Group = 1;
                    else
                        switch (Convert.ToInt16(drList["GlobalBroad"]))
                        {
                            case 1:
                                this._iProduct_Group = 2;
                                break;
                            case 2:
                                this._iProduct_Group = 1;
                                break;
                            case 3:
                                this._iProduct_Group = 4;
                                break;
                            default:
                                this._iProduct_Group = 3;
                                break;
                        }

                    this._iDepository_ID = Convert.ToInt32(drList["Depository_ID"]);
                    this._dRefDate = Convert.ToDateTime(drList["RefDate"]);
                    this._fltTotalUnits = Convert.ToSingle(drList["TotalUnits"]);
                    this._decAvgNetPrice = Convert.ToDecimal(drList["AvgNetPrice"]);
                    this._fltCurrentValue_RepCcy = Convert.ToSingle(drList["CurrentValue_RepCcy"]);
                    this._fltUnrealized_ProdCcy_PRC = Convert.ToSingle(drList["Unrealized_ProdCcy_PRC"]);
                    this._fltUnrealized_RepCcy_PRC = Convert.ToSingle(drList["Unrealized_RepCcy_PRC"]);
                    this._fltParticipation_PRC = Convert.ToSingle(drList["Participation_PRC"]);
                    this._iInvestCategories_ID = Convert.ToInt32(drList["InvestCategories_ID"]);
                    this._iIC_AA_Recs_ID = Convert.ToInt32(drList["IC_AA_Recs_ID"]);
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
                _dtList = new DataTable("ContractsBalancesRecs_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ContractCode", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ContractPortfolio", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Product_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ProductCategory_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Product_Group", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ISIN", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Code2", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ShareCodes_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Curr", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Depository_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RefDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("TotalUnits", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AvgNetPrice", System.Type.GetType("System.Single"));               
                dtCol = _dtList.Columns.Add("CurrentPrice", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("CurrentValue_RepCcy", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Unrealized_ProdCcy_PRC", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Unrealized_RepCcy_PRC", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Participation_PRC", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("InvestCategories_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("InvestCategories_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Profile_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Service_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CountryRisk_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CountryGroup_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("CountriesGroups_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("GlobalBroad", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("GlobalBroad_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Sectors_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RiskCurr", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Product_Type", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CDP_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("IC_AA_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("IC_AA_Recs_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ShareCodes_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Depository_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ProductCategory_ID", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetContracts_BalancesRecs_List", conn);
                cmd.CommandTimeout = 6000;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dDateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dDateTo));
                cmd.Parameters.Add(new SqlParameter("@CDP_ID", _iCDP_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];
                    this.dtRow["ContractCode"] = drList["Code"];
                    this.dtRow["ContractPortfolio"] = drList["Portfolio"];
                    this.dtRow["ContractTitle"] = drList["ContractTitle"];
                    this.dtRow["Product_ID"] = drList["Product_ID"];
                    this.dtRow["ProductCategory_ID"] = drList["ProductCategory_ID"];
                    this.dtRow["ShareCodes_ID"] = drList["ShareCodes_ID"];
                    this.dtRow["Depository_ID"] = drList["Depository_ID"];
                    this.dtRow["Depository_Title"] = drList["Depository_Title"];

                    this._iProduct_Group = 0;
                    if (Convert.ToInt32(drList["Product_ID"]) == 7)                         // 7 - Cash
                    {                        
                        this.dtRow["ShareCodes_Title"] = "Cash";                        
                        this.dtRow["Product_Title"] = drList["Currency_Title"] + "";
                        this.dtRow["ProductCategory_Title"] = "";
                        this.dtRow["ISIN"] = drList["ISIN"] + "";
                        this.dtRow["Code"] = drList["Currency_Title"] + "";
                        this.dtRow["Code2"] = drList["Currency_Title"] + "";
                        this.dtRow["Curr"] = drList["Currency_Title"] + "";

                        this._iProduct_Group = 4;
                    }
                    else
                    {
                        this.dtRow["ShareCodes_Title"] = drList["ShareCodes_Title"] + "";
                        this.dtRow["Product_Title"] = drList["Product_Title"] + "";
                        this.dtRow["ProductCategory_Title"] = drList["ProductCategory_Title"] + "";
                        this.dtRow["ISIN"] = drList["ISIN"] + "";
                        this.dtRow["Code"] = drList["ShareCodes_Code"] + "";
                        this.dtRow["Code2"] = drList["ShareCodes_Code2"] + "";
                        this.dtRow["Curr"] = drList["Curr"] + "";

                        if (Convert.ToInt32(drList["Product_ID"]) == 1) this._iProduct_Group = 2;
                        else if (Convert.ToInt32(drList["Product_ID"]) == 2) this._iProduct_Group = 1;
                        else
                            switch (Convert.ToInt16("0" + drList["GlobalBroad"]))
                            {
                                case 1:
                                    this._iProduct_Group = 2;
                                    break;
                                case 2:
                                    this._iProduct_Group = 1;
                                    break;
                                case 3:
                                    this._iProduct_Group = 4;
                                    break;
                                default:
                                    this._iProduct_Group = 3;
                                    break;
                            }
                    }
                    this.dtRow["Product_Group"] = _iProduct_Group;

                    this.dtRow["RefDate"] = Convert.ToDateTime(drList["RefDate"]).ToString("dd/MM/yyyy");
                    this.dtRow["TotalUnits"] = drList["TotalUnits"];
                    this.dtRow["AvgNetPrice"] = drList["AvgNetPrice"];
                    this.dtRow["CurrentPrice"] = drList["CurrentPrice"];
                    this.dtRow["CurrentValue_RepCcy"] = drList["CurrentValue_RepCcy"];  
                    this.dtRow["Unrealized_ProdCcy_PRC"] = drList["Unrealized_ProdCcy_PRC"];
                    this.dtRow["Unrealized_RepCcy_PRC"] = drList["Unrealized_RepCcy_PRC"];
                    this.dtRow["Participation_PRC"] = drList["Participation_PRC"];
                    this.dtRow["InvestCategories_Title"] = drList["InvestCategories_Title"];
                    this.dtRow["InvestCategories_ID"] = drList["InvestCategories_ID"];
                    this.dtRow["Profile_Title"] = drList["Profile_Title"];
                    this.dtRow["Service_Title"] = drList["Service_Title"];
                    this.dtRow["CountryRisk_Title"] = drList["CountryRisk_Title"] + "";
                    this.dtRow["CountryGroup_ID"] = "0" + drList["CountryGroup_ID"];
                    this.dtRow["CountriesGroups_Title"] = drList["CountriesGroups_Title"] + "";
                    this.dtRow["GlobalBroad"] = "0" + drList["GlobalBroad"];
                    this.dtRow["GlobalBroad_Title"] = drList["GlobalBroad_Title"] + "";
                    this.dtRow["Sectors_Title"] = drList["Sectors_Title"] + "";
                    this.dtRow["RiskCurr"] = drList["RiskCurr"] + "";
                    this.dtRow["Product_Type"] = drList["Product_Title"] + "/" + drList["ProductCategory_Title"];
                    this.dtRow["CDP_ID"] = drList["CDP_ID"];
                    this.dtRow["Client_ID"] = drList["Client_ID"];
                    this.dtRow["IC_AA_ID"] = drList["IC_AA_ID"];
                    this.dtRow["IC_AA_Recs_ID"] = drList["IC_AA_Recs_ID"];
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally { conn.Close(); }
        }

        public int InsertRecord()
        {
            _iRecord_ID = 0;
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("InsertContracts_BalancesRecs", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@CDP_ID", SqlDbType.Int).Value = _iCDP_ID;
                    cmd.Parameters.Add("@ShareCodes_ID", SqlDbType.Int).Value = _iShareCodes_ID;
                    cmd.Parameters.Add("@Product_ID", SqlDbType.Int).Value = _iProduct_ID;
                    cmd.Parameters.Add("@ProductCategory_ID", SqlDbType.Int).Value = _iProductCategory_ID;
                    cmd.Parameters.Add("@Depository_ID", SqlDbType.Int).Value = _iDepository_ID;
                    cmd.Parameters.Add("@RefDate", SqlDbType.DateTime).Value = _dRefDate;
                    cmd.Parameters.Add("@TotalUnits", SqlDbType.Float).Value = _fltTotalUnits;
                    cmd.Parameters.Add("@AvgNetPrice", SqlDbType.Decimal).Value = _decAvgNetPrice;                 
                    cmd.Parameters.Add("@CurrentPrice", SqlDbType.Float).Value = _fltCurrentPrice;
                    cmd.Parameters.Add("@CurrentValue_RepCcy", SqlDbType.Float).Value = _fltCurrentValue_RepCcy;
                    cmd.Parameters.Add("@Unrealized_ProdCcy_PRC", SqlDbType.Float).Value = _fltUnrealized_ProdCcy_PRC;
                    cmd.Parameters.Add("@Unrealized_RepCcy_PRC", SqlDbType.Float).Value = _fltUnrealized_RepCcy_PRC;
                    cmd.Parameters.Add("@Participation_PRC", SqlDbType.Float).Value = _fltParticipation_PRC;
                    cmd.Parameters.Add("@InvestCategories_ID", SqlDbType.Int).Value = _iInvestCategories_ID;
                    cmd.Parameters.Add("@IC_AA_Recs_ID", SqlDbType.Int).Value = _iIC_AA_Recs_ID;

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
                using (SqlCommand cmd = new SqlCommand("EditContracts_BalancesRecs", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@CDP_ID", SqlDbType.Int).Value = _iCDP_ID;
                    cmd.Parameters.Add("@ShareCodes_ID", SqlDbType.Int).Value = _iShareCodes_ID;
                    cmd.Parameters.Add("@Product_ID", SqlDbType.Int).Value = _iProduct_ID;
                    cmd.Parameters.Add("@ProductCategory_ID", SqlDbType.Int).Value = _iProductCategory_ID;
                    cmd.Parameters.Add("@Depository_ID", SqlDbType.Int).Value = _iDepository_ID;
                    cmd.Parameters.Add("@RefDate", SqlDbType.DateTime).Value = _dRefDate;
                    cmd.Parameters.Add("@TotalUnits", SqlDbType.Float).Value = _fltTotalUnits;
                    cmd.Parameters.Add("@AvgNetPrice", SqlDbType.Decimal).Value = _decAvgNetPrice;
                    cmd.Parameters.Add("@CurrentPrice", SqlDbType.Float).Value = _fltCurrentPrice;
                    cmd.Parameters.Add("@CurrentValue_RepCcy", SqlDbType.Float).Value = _fltCurrentValue_RepCcy;
                    cmd.Parameters.Add("@Unrealized_ProdCcy_PRC", SqlDbType.Float).Value = _fltUnrealized_ProdCcy_PRC;
                    cmd.Parameters.Add("@Unrealized_RepCcy_PRC", SqlDbType.Float).Value = _fltUnrealized_RepCcy_PRC;
                    cmd.Parameters.Add("@Participation_PRC", SqlDbType.Float).Value = _fltParticipation_PRC;
                    cmd.Parameters.Add("@InvestCategories_ID", SqlDbType.Int).Value = _iInvestCategories_ID;
                    cmd.Parameters.Add("@IC_AA_Recs_ID", SqlDbType.Int).Value = _iIC_AA_Recs_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }

        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int CDP_ID { get { return this._iCDP_ID; } set { this._iCDP_ID = value; } }     
        public int ShareCodes_ID { get { return this._iShareCodes_ID; } set { this._iShareCodes_ID = value; } }
        public int Product_ID { get { return this._iProduct_ID; } set { this._iProduct_ID = value; } }
        public int ProductCategory_ID { get { return this._iProductCategory_ID; } set { this._iProductCategory_ID = value; } }
        public int Product_Group { get { return this._iProduct_Group; } set { this._iProduct_Group = value; } }
        public int Depository_ID { get { return this._iDepository_ID; } set { this._iDepository_ID = value; } }
        public DateTime RefDate { get { return this._dRefDate; } set { this._dRefDate = value; } }
        public float TotalUnits { get { return this._fltTotalUnits; } set { this._fltTotalUnits = value; } }
        public decimal AvgNetPrice { get { return this._decAvgNetPrice; } set { this._decAvgNetPrice = value; } }
        public float CurrentPrice { get { return this._fltCurrentPrice; } set { this._fltCurrentPrice = value; } }
        public float CurrentValue_RepCcy { get { return this._fltCurrentValue_RepCcy; } set { this._fltCurrentValue_RepCcy = value; } }
        public float Unrealized_ProdCcy_PRC { get { return this._fltUnrealized_ProdCcy_PRC; } set { this._fltUnrealized_ProdCcy_PRC = value; } }
        public float Unrealized_RepCcy_PRC { get { return this._fltUnrealized_RepCcy_PRC; } set { this._fltUnrealized_RepCcy_PRC = value; } }
        public float Participation_PRC { get { return this._fltParticipation_PRC; } set { this._fltParticipation_PRC = value; } }
        public int InvestCategories_ID { get { return this._iInvestCategories_ID; } set { this._iInvestCategories_ID = value; } }
        public int IC_AA_Recs_ID { get { return this._iIC_AA_Recs_ID; } set { this._iIC_AA_Recs_ID = value; } }
        
        public DateTime DateFrom { get { return this._dDateFrom; } set { this._dDateFrom = value; } }
        public DateTime DateTo { get { return this._dDateTo; } set { this._dDateTo = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}
