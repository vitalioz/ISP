using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsClients_Fees
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int    _iRecord_ID;
        private int    _iTipos;
        private int    _iSPO_ID;
        private int    _iClient_Type;
        private int    _iCountryTaxes_ID;
        private int    _iProduct_ID;
        private int    _iProductCategory_ID;
        private int    _iExecutionProvider_ID;
        private int    _iStockExchange_ID;
        private int    _iCountryIssue_ID;
        private int    _iCustodyProvider_ID;
        private int    _iDepository_ID;
        private int    _iReason_ID;
        private int    _iCalculationAlgorithm;
        private int    _iCalculationValue;
        private int    _iCalculationPeriod;
        private string _sCurrency;
        private float  _fltAmountFrom;
        private float  _fltAmountTo;
        private float  _fltBuyFeesPercent;
        private float  _fltSellFeesPercent;
        private float  _fltTicketFeesBuyAmount;
        private float  _fltTicketFeesSellAmount;
        private string _sTicketFeesCurr;
        private float  _fltMinimumFees;
        private string _sMinimumFeesCurr;
        private float  _fltCalculationIndex;
        private int    _iRetrosessionMethod;
        private float  _fltRetrosessionProvider;
        private float  _fltRetrosessionCompany;
        private int    _iIncome;
        private int    _iPerTitle;
        private int    _iPerItem;
        private int    _iUseFaceValue;

        private DataTable _dtList;

        public clsClients_Fees()
        {
            this._iRecord_ID = 0;
            this._iTipos = 0;
            this._iSPO_ID = 0;
            this._iClient_Type = 0;
            this._iCountryTaxes_ID = 0;
            this._iProduct_ID = 0;
            this._iProductCategory_ID = 0;
            this._iExecutionProvider_ID = 0;
            this._iStockExchange_ID = 0;
            this._iCountryIssue_ID = 0;
            this._iCustodyProvider_ID = 0;
            this._iDepository_ID = 0;
            this._iReason_ID = 0;
            this._iCalculationAlgorithm = 0;
            this._iCalculationValue = 0;
            this._iCalculationPeriod = 0;
            this._sCurrency = "EUR";
            this._fltAmountFrom = 0;
            this._fltAmountTo = 0;
            this._fltBuyFeesPercent = 0;
            this._fltSellFeesPercent = 0;
            this._fltTicketFeesBuyAmount = 0;
            this._fltTicketFeesSellAmount = 0;
            this._sTicketFeesCurr = "EUR";
            this._fltMinimumFees = 0;
            this._sMinimumFeesCurr = "EUR";
            this._fltCalculationIndex = 0;
            this._iRetrosessionMethod = 0;
            this._fltRetrosessionProvider = 0;
            this._fltRetrosessionCompany = 0;
            this._iIncome = 0;
            this._iPerTitle = 0;
            this._iPerItem = 0;
            this._iUseFaceValue = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Clients_Fees"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iSPO_ID = Convert.ToInt32(drList["SPO_ID"]);
                    this._iExecutionProvider_ID = Convert.ToInt32(drList["ExecutionProvider_ID"]);
                    this._iProduct_ID = Convert.ToInt32(drList["Product_ID"]);
                    this._iProductCategory_ID = Convert.ToInt32(drList["ProductCategory_ID"]);
                    this._iStockExchange_ID = Convert.ToInt32(drList["StockExchange_ID"]);
                    this._fltAmountFrom = Convert.ToSingle(drList["AmountFrom"]);
                    this._fltAmountTo = Convert.ToSingle(drList["AmountTo"]);
                    this._fltBuyFeesPercent = Convert.ToSingle(drList["BuyFeesPercent"]);
                    this._fltSellFeesPercent = Convert.ToSingle(drList["SellFeesPercent"]);
                    this._fltTicketFeesBuyAmount = Convert.ToSingle(drList["TicketFeesBuyAmount"]);
                    this._fltTicketFeesSellAmount = Convert.ToSingle(drList["TicketFeesSellAmount"]);
                    this._sTicketFeesCurr = drList["TicketFeesCurr"] + "";
                    this._fltMinimumFees = Convert.ToSingle(drList["MinimumFees"]);
                    this._sMinimumFeesCurr = drList["MinimumFeesCurr"] + "";
                    this._iRetrosessionMethod = Convert.ToInt32(drList["RetrosessionMethod"]);
                    this._fltRetrosessionProvider = Convert.ToSingle(drList["RetrosessionProvider"]);
                    this._fltRetrosessionCompany = Convert.ToSingle(drList["RetrosessionCompany"]);
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
                _dtList = new DataTable("BrokerageFees_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("SPBF_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Product_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ProductCategory_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockExchanges_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AmountFrom", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AmountTo", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("BuyFeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SellFeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("TicketFeesBuyAmountAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("TicketFeesSellAmountAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("TicketFeesCurr", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("MinimumFees", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MinimumFeesCurr", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DiscountDateFrom", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DiscountDateTo", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("FeesDiscountPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AmountToPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishBuyFeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishSellFeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("TicketFinishBuyFeesAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("TicketFinishSellFeesAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ProductCategory_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("StockExchange_ID", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetPackage_BrokerageFees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Option_ID", _fltMinimumFees));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["SPBF_ID"] = drList["ID"];                                                              // ID -> SPBF_ID
                    dtRow["Product_Title"] = drList["Product_Title"];
                    dtRow["ProductCategory_Title"] = drList["ProductCategory_Title"];
                    if (Convert.ToInt32(drList["StockExchange_ID"]) == 0) dtRow["StockExchanges_Title"] = "Όλα";
                    else dtRow["StockExchanges_Title"] = drList["StockExchanges_Title"];
                    dtRow["AmountFrom"] = drList["AmountFrom"];
                    dtRow["AmountTo"] = drList["AmountTo"];
                    dtRow["BuyFeesPercent"] = drList["BuyFeesPercent"];
                    dtRow["SellFeesPercent"] = drList["SellFeesPercent"];
                    dtRow["TicketFeesBuyAmountAmount"] = drList["TicketFeesBuyAmountAmount"];
                    dtRow["TicketFeesSellAmountAmount"] = drList["TicketFeesSellAmountAmount"];
                    dtRow["TicketFeesCurr"] = drList["TicketFeesCurr"];
                    dtRow["MinimumFees"] = drList["MinimumFees"];
                    dtRow["MinimumFeesCurr"] = drList["MinimumFeesCurr"];
                    dtRow["ID"] = 0;
                    dtRow["FeesDiscountPercent"] = 0;
                    dtRow["AmountToPercent"] = 0;
                    dtRow["FinishBuyFeesPercent"] = drList["BuyFeesPercent"];
                    dtRow["FinishSellFeesPercent"] = drList["SellFeesPercent"];
                    dtRow["TicketFinishBuyFeesAmount"] = drList["TicketFeesBuyAmountAmount"];
                    dtRow["TicketFinishSellFeesAmount"] = drList["TicketFeesSellAmountAmount"];
                    dtRow["Product_ID"] = drList["Product_ID"];
                    dtRow["ProductCategory_ID"] = drList["ProductCategory_ID"];
                    dtRow["StockExchange_ID"] = drList["StockExchange_ID"];
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetFees()
        {
            try
            {
                _dtList = new DataTable("BrokerageFees_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ProductCategory_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("SPO_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ProductTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ProductCategoryTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockExchange_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AmountFrom", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AmountTo", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("BuyFeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SellFeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("TicketFeesBuyAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("TicketFeesSellAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("TicketFeesCurr", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("MinimumFeesAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MinimumFeesCurr", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RetrosessionMethod", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("RetrosessionProvider", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RetrosessionCompany", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SettlementProvider_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockExchange_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("SettlementProvider_ID", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetClients_Fees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ExecutionProvider_ID", _iExecutionProvider_ID));
                cmd.Parameters.Add(new SqlParameter("@SPO_ID", _iSPO_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    if (Convert.ToInt32(drList["Product_ID"]) == 0)
                    {
                        dtRow["Product_ID"] = 0;
                        dtRow["ProductTitle"] = "Όλα";
                    }
                    else
                    {
                        dtRow["Product_ID"] = drList["Product_ID"];
                        dtRow["ProductTitle"] = drList["ProductTitle"];
                    }

                    if (Convert.ToInt32(drList["ProductCategory_ID"]) == 0)
                    {
                        dtRow["ProductCategory_ID"] = 0;
                        dtRow["ProductCategoryTitle"] = "Όλα";
                    }
                    else
                    {
                        dtRow["ProductCategory_ID"] = drList["ProductCategory_ID"];
                        dtRow["ProductCategoryTitle"] = drList["ProductCategoryTitle"];
                    }
                    dtRow["SPO_ID"] = drList["SPO_ID"];
                    if (Convert.ToInt32(drList["StockExchange_ID"]) == 0) dtRow["StockExchange_Title"] = "'Ολα";
                    else dtRow["StockExchange_Title"] = drList["StockExchange_Title"] + "";
                    dtRow["AmountFrom"] = drList["AmountFrom"];
                    dtRow["AmountTo"] = drList["AmountTo"];
                    dtRow["BuyFeesPercent"] = drList["BuyFeesPercent"];
                    dtRow["SellFeesPercent"] = drList["SellFeesPercent"];
                    dtRow["TicketFeesBuyAmount"] = drList["TicketFeesBuyAmount"];
                    dtRow["TicketFeesSellAmount"] = drList["TicketFeesSellAmount"];
                    dtRow["TicketFeesCurr"] = drList["TicketFeesCurr"];
                    dtRow["MinimumFeesAmount"] = drList["MinimumFees"];
                    dtRow["MinimumFeesCurr"] = drList["MinimumFeesCurr"];
                    dtRow["RetrosessionMethod"] = drList["RetrosessionMethod"];
                    dtRow["RetrosessionProvider"] = drList["RetrosessionProvider"];
                    dtRow["RetrosessionCompany"] = drList["RetrosessionCompany"];
                    dtRow["SettlementProvider_Title"] = drList["SettlementProvider_Title"];

                    dtRow["StockExchange_ID"] = drList["StockExchange_ID"];
                    dtRow["SettlementProvider_ID"] = drList["SettlementProvider_ID"];
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList_Package_ID()
        {
            try
            {
                _dtList = new DataTable("BrokerageFeesList");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("SPBF_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Product_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ProductCategory_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockExchanges_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AmountFrom", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AmountTo", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("BuyFeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SellFeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("TicketFeesBuyAmountAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("TicketFeesSellAmountAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("TicketFeesCurr", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("MinimumFees", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MinimumFeesCurr", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DiscountDateFrom", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DiscountDateTo", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("FeesDiscountPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AmountToPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishBuyFeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishSellFeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("TicketFinishBuyFeesAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("TicketFinishSellFeesAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ProductCategory_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("StockExchange_ID", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetBrokerageFees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ExecutionProvider_ID", _iExecutionProvider_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["SPBF_ID"] = drList["SPBF_ID"];
                    dtRow["Product_Title"] = drList["Product_Title"];
                    dtRow["ProductCategory_Title"] = drList["ProductCategory_Title"];
                    if (Convert.ToInt32(drList["StockExchange_ID"]) == 0) dtRow["StockExchanges_Title"] = "Όλα";
                    else dtRow["StockExchanges_Title"] = drList["StockExchange_Code"];
                    dtRow["Product_ID"] = drList["Product_ID"];
                    dtRow["ProductCategory_ID"] = drList["ProductCategory_ID"];
                    dtRow["StockExchange_ID"] = drList["StockExchange_ID"];
                    dtRow["AmountFrom"] = drList["AmountFrom"];
                    dtRow["AmountTo"] = drList["AmountTo"];
                    dtRow["BuyFeesPercent"] = drList["BuyFeesPercent"];
                    dtRow["SellFeesPercent"] = drList["SellFeesPercent"];
                    dtRow["TicketFeesBuyAmountAmount"] = drList["TicketFeesBuyAmountAmount"];
                    dtRow["TicketFeesSellAmountAmount"] = drList["TicketFeesSellAmountAmount"];
                    dtRow["TicketFeesCurr"] = drList["TicketFeesCurr"];
                    dtRow["MinimumFees"] = drList["MinimumFees"];
                    dtRow["MinimumFeesCurr"] = drList["MinimumFeesCurr"];
                    if (!String.IsNullOrEmpty(drList["ID"].ToString()))
                    {
                        dtRow["ID"] = drList["ID"];
                        dtRow["FeesDiscountPercent"] = drList["AmountFrom"];
                        dtRow["AmountToPercent"] = drList["AmountTo"];
                        dtRow["FinishBuyFeesPercent"] = drList["BuyFeesPercent"];
                        dtRow["FinishSellFeesPercent"] = drList["SellFeesPercent"];
                        dtRow["TicketFinishBuyFeesAmount"] = drList["TicketFeesBuyAmount"];
                        dtRow["TicketFinishSellFeesAmount"] = drList["TicketFeesSellAmount"];
                    }
                    else
                    {
                        dtRow["ID"] = 0;
                        dtRow["FeesDiscountPercent"] = 0;
                        dtRow["AmountToPercent"] = 0;
                        dtRow["FinishBuyFeesPercent"] = 0;
                        dtRow["FinishSellFeesPercent"] = 0;
                        dtRow["TicketFinishBuyFeesAmount"] = 0;
                        dtRow["TicketFinishSellFeesAmount"] = 0;
                    }
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
                using (cmd = new SqlCommand("sp_InsertClients_Fees", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Tipos", SqlDbType.Int).Value = _iTipos;
                    cmd.Parameters.Add("@SPO_ID", SqlDbType.Int).Value = _iSPO_ID;
                    cmd.Parameters.Add("@Client_Type", SqlDbType.Int).Value = _iClient_Type;
                    cmd.Parameters.Add("@CountryTaxes_ID", SqlDbType.Int).Value = _iCountryTaxes_ID;
                    cmd.Parameters.Add("@Product_ID", SqlDbType.Int).Value = _iProduct_ID;
                    cmd.Parameters.Add("@ProductCategory_ID", SqlDbType.Int).Value = _iProductCategory_ID;
                    cmd.Parameters.Add("@ExecutionProvider_ID", SqlDbType.Int).Value = _iExecutionProvider_ID;
                    cmd.Parameters.Add("@StockExchange_ID", SqlDbType.Int).Value = _iStockExchange_ID;
                    cmd.Parameters.Add("@CountryIssue_ID", SqlDbType.Int).Value = _iCountryIssue_ID;
                    cmd.Parameters.Add("@CustodyProvider_ID", SqlDbType.Int).Value = _iCustodyProvider_ID;
                    cmd.Parameters.Add("@Depository_ID", SqlDbType.Int).Value = _iDepository_ID;
                    cmd.Parameters.Add("@Reason_ID", SqlDbType.Int).Value = _iReason_ID;
                    cmd.Parameters.Add("@CalculationAlgorithm", SqlDbType.Int).Value = _iCalculationAlgorithm;
                    cmd.Parameters.Add("@CalculationValue", SqlDbType.Int).Value = _iCalculationValue;
                    cmd.Parameters.Add("@CalculationPeriod", SqlDbType.Int).Value = _iCalculationPeriod;
                    cmd.Parameters.Add("@Currency", SqlDbType.NVarChar, 6).Value = _sCurrency;
                    cmd.Parameters.Add("@AmountFrom", SqlDbType.Float).Value = _fltAmountFrom;
                    cmd.Parameters.Add("@AmountTo", SqlDbType.Float).Value = _fltAmountTo;
                    cmd.Parameters.Add("@BuyFeesPercent", SqlDbType.Float).Value = _fltBuyFeesPercent;
                    cmd.Parameters.Add("@SellFeesPercent", SqlDbType.Float).Value = _fltSellFeesPercent;
                    cmd.Parameters.Add("@TicketFeesBuyAmount", SqlDbType.Float).Value = _fltTicketFeesBuyAmount;
                    cmd.Parameters.Add("@TicketFeesSellAmount", SqlDbType.Float).Value = _fltTicketFeesSellAmount;
                    cmd.Parameters.Add("@TicketFeesCurr", SqlDbType.NVarChar, 6).Value = _sTicketFeesCurr;
                    cmd.Parameters.Add("@MinimumFees", SqlDbType.Float).Value = _fltMinimumFees;
                    cmd.Parameters.Add("@MinimumFeesCurr", SqlDbType.NVarChar, 6).Value = _sMinimumFeesCurr;
                    cmd.Parameters.Add("@CalculationIndex", SqlDbType.Float).Value = _fltCalculationIndex;
                    cmd.Parameters.Add("@RetrosessionMethod", SqlDbType.Int).Value = _iRetrosessionMethod;
                    cmd.Parameters.Add("@RetrosessionProvider", SqlDbType.Float).Value = _fltRetrosessionProvider;
                    cmd.Parameters.Add("@RetrosessionCompany", SqlDbType.Float).Value = _fltRetrosessionCompany;
                    cmd.Parameters.Add("@Income", SqlDbType.Int).Value = _iIncome;
                    cmd.Parameters.Add("@PerTitle", SqlDbType.Int).Value = _iPerTitle;
                    cmd.Parameters.Add("@PerItem", SqlDbType.Int).Value = _iPerItem;
                    cmd.Parameters.Add("@UseFaceValue", SqlDbType.Int).Value = _iUseFaceValue;
                    cmd.ExecuteNonQuery();
                    _iRecord_ID = Convert.ToInt32(outParam.Value);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            return _iRecord_ID;
        }
        public int EditRecord()
        {
            try
            {
                conn.Open();
                using (cmd = new SqlCommand("sp_EditClients_Fees", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Tipos", SqlDbType.Int).Value = _iTipos;
                    cmd.Parameters.Add("@SPO_ID", SqlDbType.Int).Value = _iSPO_ID;
                    cmd.Parameters.Add("@Client_Type", SqlDbType.Int).Value = _iClient_Type;
                    cmd.Parameters.Add("@CountryTaxes_ID", SqlDbType.Int).Value = _iCountryTaxes_ID;
                    cmd.Parameters.Add("@Product_ID", SqlDbType.Int).Value = _iProduct_ID;
                    cmd.Parameters.Add("@ProductCategory_ID", SqlDbType.Int).Value = _iProductCategory_ID;
                    cmd.Parameters.Add("@ExecutionProvider_ID", SqlDbType.Int).Value = _iExecutionProvider_ID;
                    cmd.Parameters.Add("@StockExchange_ID", SqlDbType.Int).Value = _iStockExchange_ID;
                    cmd.Parameters.Add("@CountryIssue_ID", SqlDbType.Int).Value = _iCountryIssue_ID;
                    cmd.Parameters.Add("@CustodyProvider_ID", SqlDbType.Int).Value = _iCustodyProvider_ID;
                    cmd.Parameters.Add("@Depository_ID", SqlDbType.Int).Value = _iDepository_ID;
                    cmd.Parameters.Add("@Reason_ID", SqlDbType.Int).Value = _iReason_ID;
                    cmd.Parameters.Add("@CalculationAlgorithm", SqlDbType.Int).Value = _iCalculationAlgorithm;
                    cmd.Parameters.Add("@CalculationValue", SqlDbType.Int).Value = _iCalculationValue;
                    cmd.Parameters.Add("@CalculationPeriod", SqlDbType.Int).Value = _iCalculationPeriod;
                    cmd.Parameters.Add("@Currency", SqlDbType.NVarChar, 6).Value = _sCurrency;
                    cmd.Parameters.Add("@AmountFrom", SqlDbType.Float).Value = _fltAmountFrom;
                    cmd.Parameters.Add("@AmountTo", SqlDbType.Float).Value = _fltAmountTo;
                    cmd.Parameters.Add("@BuyFeesPercent", SqlDbType.Float).Value = _fltBuyFeesPercent;
                    cmd.Parameters.Add("@SellFeesPercent", SqlDbType.Float).Value = _fltSellFeesPercent;
                    cmd.Parameters.Add("@TicketFeesBuyAmount", SqlDbType.Float).Value = _fltTicketFeesBuyAmount;
                    cmd.Parameters.Add("@TicketFeesSellAmount", SqlDbType.Float).Value = _fltTicketFeesSellAmount;
                    cmd.Parameters.Add("@TicketFeesCurr", SqlDbType.NVarChar, 6).Value = _sTicketFeesCurr;
                    cmd.Parameters.Add("@MinimumFees", SqlDbType.Float).Value = _fltMinimumFees;
                    cmd.Parameters.Add("@MinimumFeesCurr", SqlDbType.NVarChar, 6).Value = _sMinimumFeesCurr;
                    cmd.Parameters.Add("@CalculationIndex", SqlDbType.Float).Value = _fltCalculationIndex;
                    cmd.Parameters.Add("@RetrosessionMethod", SqlDbType.Int).Value = _iRetrosessionMethod;
                    cmd.Parameters.Add("@RetrosessionProvider", SqlDbType.Float).Value = _fltRetrosessionProvider;
                    cmd.Parameters.Add("@RetrosessionCompany", SqlDbType.Float).Value = _fltRetrosessionCompany;
                    cmd.Parameters.Add("@Income", SqlDbType.Int).Value = _iIncome;
                    cmd.Parameters.Add("@PerTitle", SqlDbType.Int).Value = _iPerTitle;
                    cmd.Parameters.Add("@PerItem", SqlDbType.Int).Value = _iPerItem;
                    cmd.Parameters.Add("@UseFaceValue", SqlDbType.Int).Value = _iUseFaceValue;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Clients_Fees";
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
        public int SPO_ID { get { return this._iSPO_ID; } set { this._iSPO_ID = value; } }
        public int Client_Type { get { return this._iClient_Type; } set { this._iClient_Type = value; } }
        public int CountryTaxes_ID { get { return this._iCountryTaxes_ID; } set { this._iCountryTaxes_ID = value; } }
        public int Product_ID { get { return this._iProduct_ID; } set { this._iProduct_ID = value; } }
        public int ProductCategory_ID { get { return this._iProductCategory_ID; } set { this._iProductCategory_ID = value; } }
        public int ExecutionProvider_ID { get { return this._iExecutionProvider_ID; } set { this._iExecutionProvider_ID = value; } }
        public int StockExchange_ID { get { return this._iStockExchange_ID; } set { this._iStockExchange_ID = value; } }
        public int CountryIssue_ID { get { return this._iCountryIssue_ID; } set { this._iCountryIssue_ID = value; } }
        public int CustodyProvider_ID { get { return this._iCustodyProvider_ID; } set { this._iCustodyProvider_ID = value; } }
        public int Depository_ID { get { return this._iDepository_ID; } set { this._iDepository_ID = value; } }
        public int Reason_ID { get { return this._iReason_ID; } set { this._iReason_ID = value; } }
        public int CalculationAlgorithm { get { return this._iCalculationAlgorithm; } set { this._iCalculationAlgorithm = value; } }
        public int CalculationValue { get { return this._iCalculationValue; } set { this._iCalculationValue = value; } }
        public int CalculationPeriod { get { return this._iCalculationPeriod; } set { this._iCalculationPeriod = value; } }
        public string Currency { get { return this._sCurrency; } set { this._sCurrency = value; } }
        public float AmountFrom { get { return this._fltAmountFrom; } set { this._fltAmountFrom = value; } }
        public float AmountTo { get { return this._fltAmountTo; } set { this._fltAmountTo = value; } }
        public float BuyFeesPercent { get { return this._fltBuyFeesPercent; } set { this._fltBuyFeesPercent = value; } }
        public float SellFeesPercent { get { return this._fltSellFeesPercent; } set { this._fltSellFeesPercent = value; } }
        public float TicketFeesBuyAmount { get { return this._fltTicketFeesBuyAmount; } set { this._fltTicketFeesBuyAmount = value; } }
        public float TicketFeesSellAmount { get { return this._fltTicketFeesSellAmount; } set { this._fltTicketFeesSellAmount = value; } }
        public string TicketFeesCurr { get { return this._sTicketFeesCurr; } set { this._sTicketFeesCurr = value; } }
        public float MinimumFees { get { return this._fltMinimumFees; } set { this._fltMinimumFees = value; } }
        public string MinimumFeesCurr { get { return this._sMinimumFeesCurr; } set { this._sMinimumFeesCurr = value; } }
        public float CalculationIndex { get { return this._fltCalculationIndex; } set { this._fltCalculationIndex = value; } }
        public int RetrosessionMethod { get { return this._iRetrosessionMethod; } set { this._iRetrosessionMethod = value; } }
        public float RetrosessionProvider { get { return this._fltRetrosessionProvider; } set { this._fltRetrosessionProvider = value; } }
        public float RetrosessionCompany { get { return this._fltRetrosessionCompany; } set { this._fltRetrosessionCompany = value; } }
        public int Income { get { return this._iIncome; } set { this._iIncome = value; } }

        public int PerTitle { get { return this._iPerTitle; } set { this._iPerTitle = value; } }
        public int PerItem { get { return this._iPerItem; } set { this._iPerItem = value; } }
        public int UseFaceValue { get { return this._iUseFaceValue; } set { this._iUseFaceValue = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}
