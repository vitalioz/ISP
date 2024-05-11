using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsContracts_Balances
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataRow dtRow;

        private int      _iRecord_ID;
        private DateTime _dDateIns;
        private int      _iCDP_ID;
        private int      _iIC_AA_ID;
        private decimal  _decTotalSecurutiesValue;
        private decimal  _decTotalCashValue;
        private decimal  _decTotalValue;
        private int      _iDebitBalance;
        private int      _iAssetAllocation;
        private int      _iSpecialInstructions;
        private int      _iSuitableProducts;
        private int      _iLeverage;
        private string   _sNotes;

        private DateTime _dDateFrom;
        private DateTime _dDateTo;
        private string _sCode;
        private string _sPortfolio;
        private string _sContractTitle;
        private string _sCurrency;
        private string _sProfile_Title;
        private int _iContract_ID;
        private int _iContract_Details_ID;
        private int _iContract_Packages_ID;

        private DataTable _dtList;

        public clsContracts_Balances()
        {
            this._iRecord_ID = 0;
            this._dDateIns = Convert.ToDateTime("1900/01/01");
            this._iCDP_ID = 0; 
            this._iIC_AA_ID = 0; 
            this._decTotalSecurutiesValue = 0;
            this._decTotalCashValue = 0;
            this._decTotalValue = 0;
            this._iDebitBalance = 1;
            this._iAssetAllocation = 1;
            this._iSpecialInstructions = 1;
            this._iSuitableProducts = 1;
            this._iLeverage = 1;
            this._sNotes = "";

            this._dDateFrom = Convert.ToDateTime("1900/01/01");
            this._dDateTo = Convert.ToDateTime("2070/12/31");
            this._sCode = "";
            this._sPortfolio = "";
            this._sContractTitle = "";
            this._sCurrency = "";
            this._sProfile_Title = "";
            this._iContract_ID = 0;
            this._iContract_Details_ID = 0;
            this._iContract_Packages_ID = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetContracts_Balances", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._dDateIns = Convert.ToDateTime(drList["DateIns"]);
                    this._iCDP_ID = Convert.ToInt32(drList["CDP_ID"]);
                    this._iIC_AA_ID = Convert.ToInt32(drList["IC_AA_ID"]);
                    this._decTotalSecurutiesValue = Convert.ToDecimal(drList["TotalSecurutiesValue"]);
                    this._decTotalCashValue = Convert.ToDecimal(drList["TotalCashValue"]);
                    this._decTotalValue = Convert.ToDecimal(drList["TotalValue"]);
                    this._iDebitBalance = Convert.ToInt32(drList["DebitBalance"]);
                    this._iAssetAllocation = Convert.ToInt32(drList["AssetAllocation"]);
                    this._iSpecialInstructions = Convert.ToInt32(drList["SpecialInstructions"]);
                    this._iSuitableProducts = Convert.ToInt32(drList["SuitableProducts"]);
                    this._iLeverage =  Convert.ToInt32(drList["Leverage"]);
                    this._sNotes = drList["Notes"] + "";

                    this._sCode = drList["Code"] + "";
                    this._sPortfolio = drList["Portfolio"] + "";
                    this._sContractTitle = drList["ContractTitle"] + "";
                    this._sCurrency = drList["Currency"] + "";
                    this._sProfile_Title = drList["Profile_Title"] + "";
                    this._iContract_ID = Convert.ToInt32(drList["Contract_ID"]);
                    this._iContract_Details_ID = Convert.ToInt32(drList["Contracts_Details_ID"]);
                    this._iContract_Packages_ID = Convert.ToInt32(drList["Contracts_Packages_ID"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            int i = 0;
            _dtList = new DataTable();
            _dtList.Columns.Add("AA", typeof(int));
            _dtList.Columns.Add("DateIns", typeof(string));
            _dtList.Columns.Add("Code", typeof(string));
            _dtList.Columns.Add("Portfolio", typeof(string));
            _dtList.Columns.Add("ContractTitle", typeof(string));
            _dtList.Columns.Add("Profile_Title", typeof(string));
            _dtList.Columns.Add("Currency", typeof(string));
            _dtList.Columns.Add("TotalSecurutiesValue", typeof(decimal));
            _dtList.Columns.Add("TotalCashValue", typeof(decimal));
            _dtList.Columns.Add("TotalValue", typeof(decimal));
            _dtList.Columns.Add("DebitBalance", typeof(int));
            _dtList.Columns.Add("AssetAllocation", typeof(int));
            _dtList.Columns.Add("SpecialInstructions", typeof(int));
            _dtList.Columns.Add("SuitableProducts", typeof(int));
            _dtList.Columns.Add("Leverage", typeof(int));
            _dtList.Columns.Add("MiFID_II", typeof(Boolean));
            _dtList.Columns.Add("Advisor", typeof(string));
            _dtList.Columns.Add("RM", typeof(string));
            _dtList.Columns.Add("Notes", typeof(string));

            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("CDP_ID", typeof(int));
            _dtList.Columns.Add("IC_AA_ID", typeof(int));
            _dtList.Columns.Add("Contract_ID", typeof(int));
            _dtList.Columns.Add("Contracts_Details_ID", typeof(int));
            _dtList.Columns.Add("Contracts_Packages_ID", typeof(int));
            _dtList.Columns.Add("Profile_ID", typeof(int));
            _dtList.Columns.Add("Client_ID", typeof(int));
            _dtList.Columns.Add("Tipos", typeof(int));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetContracts_Balances_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateIns", _dDateIns));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    i = i + 1;
                    dtRow = _dtList.NewRow();
                    dtRow["AA"] = i;
                    dtRow["DateIns"] = Convert.ToDateTime(drList["DateIns"]).ToString("dd/MM/yyyy");
                    dtRow["Code"] = drList["Code"];
                    dtRow["Portfolio"] = drList["Portfolio"];
                    dtRow["ContractTitle"] = drList["ContractTitle"];
                    dtRow["Profile_Title"] = drList["Profile_Title"];
                    dtRow["Currency"] = drList["Currency"];
                    dtRow["TotalSecurutiesValue"] = drList["TotalSecurutiesValue"];
                    dtRow["TotalCashValue"] = drList["TotalCashValue"];
                    dtRow["TotalValue"] = drList["TotalValue"];
                    dtRow["DebitBalance"] = drList["DebitBalance"];
                    dtRow["AssetAllocation"] = drList["AssetAllocation"];
                    dtRow["SpecialInstructions"] = drList["SpecialInstructions"];
                    dtRow["SuitableProducts"] = drList["SuitableProducts"];
                    dtRow["Leverage"] = drList["Leverage"];
                    dtRow["MiFID_II"] = Convert.ToInt16(drList["MIFID"]) == 2 ? true : false;
                    dtRow["Advisor"] = (drList["Advisor_Surname"] + " " + drList["Advisor_Firstname"]).Trim();
                    dtRow["RM"] = (drList["RM_Surname"] + " " + drList["RM_Firstname"]).Trim();
                    dtRow["Notes"] = drList["Notes"];                   
                    dtRow["ID"] = drList["ID"];
                    dtRow["CDP_ID"] = drList["CDP_ID"];
                    dtRow["IC_AA_ID"] = drList["IC_AA_ID"];
                    dtRow["Contract_ID"] = drList["Contract_ID"];
                    dtRow["Contracts_Details_ID"] = drList["Contracts_Details_ID"];
                    dtRow["Contracts_Packages_ID"] = drList["Contracts_Packages_ID"];
                    dtRow["Profile_ID"] = drList["Profile_ID"];
                    dtRow["Client_ID"] = drList["Client_ID"];
                    switch (dtRow["Currency"])
                    {
                        case "EUR":
                            dtRow["Tipos"] = 1;
                            break;
                        case "USD":
                            dtRow["Tipos"] = 2;
                            break;
                        case 3:
                            dtRow["Tipos"] = 3;
                            break;
                    }
                    _dtList.Rows.Add(dtRow);
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
                using (SqlCommand cmd = new SqlCommand("InsertContracts_Balances", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = _dDateIns;
                    cmd.Parameters.Add("@CDP_ID", SqlDbType.Int).Value = _iCDP_ID;
                    cmd.Parameters.Add("@IC_AA_ID", SqlDbType.Int).Value = _iIC_AA_ID;
                    cmd.Parameters.Add("@TotalSecurutiesValue", SqlDbType.Decimal).Value = _decTotalSecurutiesValue;
                    cmd.Parameters.Add("@TotalCashValue", SqlDbType.Decimal).Value = _decTotalCashValue;
                    cmd.Parameters.Add("@TotalValue", SqlDbType.Decimal).Value = _decTotalValue;
                    cmd.Parameters.Add("@DebitBalance", SqlDbType.Int).Value = _iDebitBalance;
                    cmd.Parameters.Add("@AssetAllocation", SqlDbType.Int).Value = _iAssetAllocation;
                    cmd.Parameters.Add("@SpecialInstructions", SqlDbType.Int).Value = _iSpecialInstructions;
                    cmd.Parameters.Add("@SuitableProducts", SqlDbType.Int).Value = _iSuitableProducts;
                    cmd.Parameters.Add("@Leverage", SqlDbType.Int).Value = _iLeverage;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 200).Value = _sNotes;
                    cmd.ExecuteNonQuery();
                    _iRecord_ID = Convert.ToInt32(outParam.Value);
                }
            }
            catch (Exception ex) { 
                MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
            }
            finally { conn.Close(); }

            return _iRecord_ID;
        }
        public int EditRecord()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("EditContracts_Balances", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = _dDateIns;
                    cmd.Parameters.Add("@CDP_ID", SqlDbType.Int).Value = _iCDP_ID;
                    cmd.Parameters.Add("@IC_AA_ID", SqlDbType.Int).Value = _iIC_AA_ID;
                    cmd.Parameters.Add("@TotalSecurutiesValue", SqlDbType.Decimal).Value = _decTotalSecurutiesValue;
                    cmd.Parameters.Add("@TotalCashValue", SqlDbType.Decimal).Value = _decTotalCashValue;
                    cmd.Parameters.Add("@TotalValue", SqlDbType.Decimal).Value = _decTotalValue;
                    cmd.Parameters.Add("@DebitBalance", SqlDbType.Int).Value = _iDebitBalance;
                    cmd.Parameters.Add("@AssetAllocation", SqlDbType.Int).Value = _iAssetAllocation;
                    cmd.Parameters.Add("@SpecialInstructions", SqlDbType.Int).Value = _iSpecialInstructions;
                    cmd.Parameters.Add("@SuitableProducts", SqlDbType.Int).Value = _iSuitableProducts;
                    cmd.Parameters.Add("@Leverage", SqlDbType.Int).Value = _iLeverage;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 200).Value = _sNotes;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            return _iRecord_ID;
        }
        public void DeleteRecord()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Contracts_Balances";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public DateTime DateIns { get { return _dDateIns; } set { _dDateIns = value; } }
        public int CDP_ID { get { return _iCDP_ID; } set { _iCDP_ID = value; } }
        public int IC_AA_ID { get { return this._iIC_AA_ID; } set { this._iIC_AA_ID = value; } }
        public decimal TotalSecurutiesValue { get { return _decTotalSecurutiesValue; } set { _decTotalSecurutiesValue = value; } }
        public decimal TotalCashValue { get { return _decTotalCashValue; } set { _decTotalCashValue = value; } }
        public decimal TotalValue { get { return _decTotalValue; } set { _decTotalValue = value; } }
        public int DebitBalance { get { return _iDebitBalance; } set { _iDebitBalance = value; } }
        public int AssetAllocation { get { return _iAssetAllocation; } set { _iAssetAllocation = value; } }
        public int SpecialInstructions { get { return _iSpecialInstructions; } set { _iSpecialInstructions = value; } }
        public int SuitableProducts { get { return _iSuitableProducts; } set { _iSuitableProducts = value; } }
        public int Leverage { get { return _iLeverage; } set { _iLeverage = value; } }
        public string Notes { get { return _sNotes; } set { _sNotes = value; } }
        public DateTime DateFrom { get { return _dDateFrom; } set { _dDateFrom = value; } }
        public DateTime DateTo { get { return _dDateTo; } set { _dDateTo = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
