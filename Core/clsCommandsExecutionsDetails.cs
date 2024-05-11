using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsCommandsExecutionsDetails
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;
        DataRow[] foundRows;

        private int _iRecord_ID;
        private int _iCommand_ID;
        private int _iCommandExecution_ID;
        private DateTime _dCurrentTimestamp;
        private string _sSecondOrdID;
        private int _iStockExchange_ID;
        private int _iStockCompany_ID;
        private decimal _decPrice;
        private decimal _decQuantity;

        private int i;
        private string sTemp;
        private DateTime _dDateFrom;
        private DateTime _dDateTo;
        private DataTable _dtList;

        clsOrdersSecurity Orders3 = new clsOrdersSecurity();
        public clsCommandsExecutionsDetails()
        {
            this._iRecord_ID = 0;
            this._iCommand_ID = 0;
            this._iCommandExecution_ID = 0;
            this._dCurrentTimestamp = Convert.ToDateTime("1900/01/01");
            this._sSecondOrdID = "";
            this._iStockExchange_ID = 0;
            this._iStockCompany_ID = 0;
            this._decPrice = 0;
            this._decQuantity = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Commands_ExecutionsDetails"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iCommand_ID = Convert.ToInt32(drList["Command_ID"]);
                    this._iCommandExecution_ID = Convert.ToInt32(drList["CommandExecution_ID"]);
                    this._dCurrentTimestamp = Convert.ToDateTime(drList["CurrentTimestamp"]);
                    this._sSecondOrdID = drList["SecondOrdID"] + "";
                    this._iStockExchange_ID = Convert.ToInt32(drList["StockExchange_ID"]);
                    this._iStockCompany_ID = Convert.ToInt32(drList["StockCompany_ID"]);
                    this._decPrice = Convert.ToDecimal(drList["Price"]);
                    this._decQuantity = Convert.ToDecimal(drList["Quantity"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("Command_ID", typeof(int));
            _dtList.Columns.Add("CommandExecution_ID", typeof(int));
            _dtList.Columns.Add("CurrentTimestamp", typeof(DateTime));
            _dtList.Columns.Add("SecondOrdID", typeof(string));
            _dtList.Columns.Add("StockExchange_ID", typeof(int));
            _dtList.Columns.Add("StockExchange_MIC", typeof(string));
            _dtList.Columns.Add("StockCompany_ID", typeof(int));
            _dtList.Columns.Add("StockCompany_Title", typeof(string));
            _dtList.Columns.Add("Price", typeof(decimal));
            _dtList.Columns.Add("Quantity", typeof(decimal));

            SqlDataReader drList = null;
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetCommandsExecutionDetails_Command_ID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Command_ID", this._iCommand_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["Command_ID"], drList["CommandExecution_ID"], drList["CurrentTimestamp"], drList["SecondOrdID"],
                                     drList["StockExchange_ID"], drList["StockExchange_MIC"], drList["StockCompany_ID"], drList["StockCompany_Title"],
                                     drList["Price"], drList["Quantity"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetExecutedCommands()
        {
            string sOld_ClientOrder_ID = "~~~";
            try
            {
                _dtList = new DataTable("Commands_Execution_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ClientOrder_ID", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Command_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("BulkCommand", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CommandType_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Aktion", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("AktionDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Provider_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ClientFullName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Share_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Share_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ISIN", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ExecuteDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RealQuantity", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("RealPrice", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("RealAmount", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("FeesDiff", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("FeesMarket", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("AccruedInterest", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("Commission", System.Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SE_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SE_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Depository_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("MaxQuantity", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int16"));

                conn.Open();
                cmd = new SqlCommand("GetCommands_ExecutionDetails", conn);                                 
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@StockCompany_ID", _iStockCompany_ID));
                cmd.Parameters.Add(new SqlParameter("@ExecuteDateFrom", _dDateFrom));
                cmd.Parameters.Add(new SqlParameter("@ExecuteDateTo", _dDateTo)); 
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    if (Convert.ToInt32(drList["CommandType_ID"]) == 1 && Convert.ToInt32(drList["Status"]) >= 0)
                    {
                        foundRows = _dtList.Select("Command_ID = " + drList["Command_ID"]);
                        if (foundRows.Length == 0)
                        {
                            dtRow = _dtList.NewRow();
                            this.dtRow["ID"] = drList["ID"];
                            this.dtRow["ClientOrder_ID"] = drList["SecondOrdID"];
                            this.dtRow["Command_ID"] = drList["Command_ID"];
                            this.dtRow["BulkCommand"] = drList["BulkCommand"];
                            this.dtRow["CommandType_ID"] = drList["CommandType_ID"];
                            this.dtRow["Aktion"] = drList["Aktion"];
                            this.dtRow["AktionDate"] = drList["AktionDate"];
                            this.dtRow["Client_ID"] = drList["Client_ID"];
                            this.dtRow["Contract_ID"] = drList["Contract_ID"];
                            this.dtRow["Provider_ID"] = drList["StockCompany_ID"];
                            this.dtRow["ClientFullName"] = (drList["Surname"] + " " + drList["FirstName"]).Trim();
                            this.dtRow["ContractTitle"] = drList["ContractTitle"];
                            this.dtRow["Code"] = drList["Code"];
                            this.dtRow["Portfolio"] = drList["Portfolio"];
                            this.dtRow["Product_ID"] = drList["Product_ID"];
                            this.dtRow["Share_ID"] = drList["Share_ID"];
                            this.dtRow["Share_Title"] = drList["Share_Title"];
                            this.dtRow["Share_Code"] = drList["Share_Code"];
                            this.dtRow["ISIN"] = drList["ISIN"];
                            this.dtRow["Currency"] = drList["Curr"];
                            this.dtRow["ExecuteDate"] = drList["ExecuteDate"];
                            this.dtRow["RealQuantity"] = drList["Quantity"];
                            this.dtRow["RealPrice"] = drList["Price"];
                            this.dtRow["RealAmount"] = Convert.ToDecimal(drList["Quantity"]) * Convert.ToDecimal(drList["Price"]);
                            this.dtRow["FeesDiff"] = drList["FeesDiff"];
                            this.dtRow["FeesMarket"] = drList["FeesMarket"];
                            this.dtRow["AccruedInterest"] = drList["AccruedInterest"];
                            this.dtRow["Commission"] = drList["Commission"];
                            this.dtRow["SE_Code"] = drList["SE_Code"];
                            this.dtRow["SE_ID"] = drList["StockExchange_ID"];
                            this.dtRow["Depository_Code"] = drList["Depository_Code"];
                            this.dtRow["Notes"] = drList["Notes"];
                            if (sOld_ClientOrder_ID + "" != drList["SecondOrdID"] + "")
                            {
                                this.dtRow["MaxQuantity"] = 1;
                                sOld_ClientOrder_ID = drList["SecondOrdID"] + "";
                            }
                            else this.dtRow["MaxQuantity"] = 0;
                            this.dtRow["Status"] = drList["Status"];
                            _dtList.Rows.Add(dtRow);
                        }
                    }
                    else
                    {
                        sTemp = drList["BulkCommand"] + "";
                        i = sTemp.IndexOf("/");
                        if (i > 0) sTemp = sTemp.Substring(i + 1);                        
                        Orders3 = new clsOrdersSecurity();
                        Orders3.AktionDate = Convert.ToDateTime(drList["AktionDate"]);                        
                        Orders3.BulkCommand = sTemp.Replace("<", "").Replace(">", "");
                        Orders3.GetList_BulkCommand();
                        foreach (DataRow dtRow1 in Orders3.List.Rows)
                        {
                            if (Convert.ToInt32(dtRow1["CommandType_ID"]) == 1 && Convert.ToInt32(dtRow1["Status"]) >= 0)
                            {
                                foundRows = _dtList.Select("Command_ID = " + dtRow1["ID"]);
                                if (foundRows.Length == 0)
                                {
                                    dtRow = _dtList.NewRow();
                                    this.dtRow["ID"] = drList["ID"];
                                    this.dtRow["ClientOrder_ID"] = drList["SecondOrdID"];
                                    this.dtRow["Command_ID"] = dtRow1["ID"];
                                    this.dtRow["BulkCommand"] = sTemp;
                                    this.dtRow["CommandType_ID"] = 1;
                                    this.dtRow["Aktion"] = dtRow1["Aktion"];
                                    this.dtRow["AktionDate"] = dtRow1["AktionDate"];
                                    this.dtRow["Client_ID"] = dtRow1["Client_ID"];
                                    this.dtRow["Contract_ID"] = dtRow1["Contract_ID"];
                                    this.dtRow["Provider_ID"] = drList["StockCompany_ID"];
                                    this.dtRow["ClientFullName"] = dtRow1["ClientFullName"];
                                    this.dtRow["ContractTitle"] = dtRow1["ContractTitle"];
                                    this.dtRow["Code"] = dtRow1["Code"];
                                    this.dtRow["Portfolio"] = dtRow1["Portfolio"];
                                    this.dtRow["Product_ID"] = dtRow1["Product_ID"];
                                    this.dtRow["Share_ID"] = dtRow1["Share_ID"];
                                    this.dtRow["Share_Title"] = dtRow1["Share_Title"];
                                    this.dtRow["Share_Code"] = dtRow1["Share_Code"];
                                    this.dtRow["ISIN"] = dtRow1["ISIN"];
                                    this.dtRow["Currency"] = dtRow1["Currency"];
                                    this.dtRow["ExecuteDate"] = drList["ExecuteDate"];
                                    this.dtRow["RealQuantity"] = dtRow1["RealQuantity"];
                                    this.dtRow["RealPrice"] = dtRow1["RealPrice"];
                                    this.dtRow["RealAmount"] = dtRow1["RealAmount"];
                                    this.dtRow["FeesDiff"] = dtRow1["FeesDiff"];
                                    this.dtRow["FeesMarket"] = dtRow1["FeesMarket"];
                                    this.dtRow["AccruedInterest"] = dtRow1["AccruedInterest"];
                                    this.dtRow["Commission"] = dtRow1["Commission"];
                                    this.dtRow["SE_Code"] = dtRow1["SE_Code"];
                                    this.dtRow["SE_ID"] = drList["StockExchange_ID"];
                                    this.dtRow["Depository_Code"] = drList["Depository_Code"];
                                    this.dtRow["Notes"] = drList["Notes"];
                                    if (sOld_ClientOrder_ID + "" != drList["SecondOrdID"] + "")
                                    {
                                        this.dtRow["MaxQuantity"] = 1;
                                        sOld_ClientOrder_ID = drList["SecondOrdID"] + "";
                                    }
                                    else this.dtRow["MaxQuantity"] = 0;
                                    this.dtRow["Status"] = dtRow1["Status"];
                                    _dtList.Rows.Add(dtRow);
                                }
                            }
                        }
                    }
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
                using (SqlCommand cmd = new SqlCommand("InsertCommands_ExecutionsDetails", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Command_ID", SqlDbType.Int).Value = _iCommand_ID;
                    cmd.Parameters.Add("@CommandExecution_ID", SqlDbType.Int).Value = _iCommandExecution_ID;
                    cmd.Parameters.Add("@CurrentTimestamp", SqlDbType.DateTime).Value = _dCurrentTimestamp;
                    cmd.Parameters.Add("@SecondOrdID", SqlDbType.NVarChar, 32).Value = _sSecondOrdID;
                    cmd.Parameters.Add("@StockExchange_ID", SqlDbType.Int).Value = _iStockExchange_ID;
                    cmd.Parameters.Add("@StockCompany_ID", SqlDbType.Int).Value = _iStockCompany_ID;
                    cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = _decPrice;
                    cmd.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = _decQuantity;
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
                using (SqlCommand cmd = new SqlCommand("EditCommands_ExecutionsDetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Command_ID", SqlDbType.Int).Value = _iCommand_ID;
                    cmd.Parameters.Add("@CommandExecution_ID", SqlDbType.Int).Value = _iCommandExecution_ID;
                    cmd.Parameters.Add("@CurrentTimestamp", SqlDbType.DateTime).Value = _dCurrentTimestamp;
                    cmd.Parameters.Add("@SecondOrdID", SqlDbType.NVarChar, 32).Value = _sSecondOrdID;
                    cmd.Parameters.Add("@StockExchange_ID", SqlDbType.Int).Value = _iStockExchange_ID;
                    cmd.Parameters.Add("@StockCompany_ID", SqlDbType.Int).Value = _iStockCompany_ID;
                    cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = _decPrice;
                    cmd.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = _decQuantity;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Commands_ExecutionsDetails";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void DeleteRecord_Command_ID()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Commands_ExecutionsDetails";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "Command_ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iCommand_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return _iRecord_ID; } set { _iRecord_ID = value; } }
        public int Command_ID { get { return _iCommand_ID; } set { _iCommand_ID = value; } }
        public int CommandExecution_ID { get { return _iCommandExecution_ID; } set { _iCommandExecution_ID = value; } }
        public DateTime CurrentTimestamp { get { return _dCurrentTimestamp; } set { _dCurrentTimestamp = value; } }
        public string SecondOrdID { get { return _sSecondOrdID; } set { _sSecondOrdID = value; } }
        public int StockExchange_ID { get { return _iStockExchange_ID; } set { _iStockExchange_ID = value; } }
        public int StockCompany_ID { get { return _iStockCompany_ID; } set { _iStockCompany_ID = value; } }
        public decimal Price { get { return _decPrice; } set { _decPrice = value; } }
        public decimal Quantity { get { return _decQuantity; } set { _decQuantity = value; } }
        public DateTime DateFrom { get { return this._dDateFrom; } set { this._dDateFrom = value; } }
        public DateTime DateTo { get { return this._dDateTo; } set { this._dDateTo = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
