using System;                   
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsClientsSettlementFees
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;
        DataRow[] foundRows;

        private int       _iRecord_ID;
        private int       _iContract_ID;
        private int       _iContract_Packages_ID;
        private int       _iSPSF_ID;
        private int       _iProduct_ID;
        private int       _iProductCategory_ID;
        private DateTime  _dFrom;
        private DateTime  _dTo;
        private decimal   _decSettlementFeesDiscount;
        private decimal   _decTicketFeesDiscount;
        private decimal   _decSettlementFeesBuy;
        private decimal   _decSettlementFeesSell;
        private float     _fltTicketFeesBuy;
        private float     _fltTicketFeesSell;

        private bool      _bIncludeDiscount;
        private int       _iOption_ID;
        private int       _iStockExchange_ID;
        private DataTable _dtList;

        public clsClientsSettlementFees()
        {
            this._iRecord_ID = 0;
            this._iContract_ID = 0;
            this._iContract_Packages_ID = 0;
            this._iSPSF_ID = 0;
            this._iProduct_ID = 0;
            this._iProductCategory_ID = 0;
            this._dFrom = Convert.ToDateTime("1900/01/01");
            this._dTo = Convert.ToDateTime("2070/12/31");
            this._decSettlementFeesDiscount = 0;
            this._decTicketFeesDiscount = 0;
            this._decSettlementFeesBuy = 0;
            this._decSettlementFeesSell = 0;
            this._fltTicketFeesBuy = 0;
            this._fltTicketFeesSell = 0;

            this._bIncludeDiscount = false;
            this._iOption_ID = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "ClientsSettlementFees"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iContract_ID = Convert.ToInt32(drList["Contract_ID"]);
                    this._iContract_Packages_ID = Convert.ToInt32(drList["Contract_Packages_ID"]);
                    this._iSPSF_ID = Convert.ToInt32(drList["SPSF_ID"]);
                    this._iProduct_ID = Convert.ToInt32(drList["Product_ID"]);
                    this._iProductCategory_ID = Convert.ToInt32(drList["ProductCategory_ID"]);
                    this._dFrom = Convert.ToDateTime(drList["DateFrom"]);
                    this._dTo = Convert.ToDateTime(drList["DateTo"]);
                    this._decSettlementFeesDiscount = Convert.ToDecimal(drList["SettlementFeesDiscount"]);
                    this._decTicketFeesDiscount = Convert.ToDecimal(drList["TicketFeesDiscount"]);
                    this._decSettlementFeesBuy = Convert.ToDecimal(drList["SettlementFeesBuy"]);
                    this._decSettlementFeesSell = Convert.ToDecimal(drList["SettlementFeesSell"]);
                    this._fltTicketFeesBuy = Convert.ToSingle(drList["TicketFeesBuy"]);
                    this._fltTicketFeesSell = Convert.ToSingle(drList["TicketFeesSell"]);
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
                _dtList = new DataTable("SettlementFees_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Product_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ProductCategory_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Depositories_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AmountFrom", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AmountTo", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("BuyFeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SellFeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("TicketFeesBuyAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("TicketFeesSellAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("TicketFeesCurr", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("MinimumFees", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MinimumFeesCurr", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DiscountDateFrom", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DiscountDateTo", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("FeesDiscountPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("TicketFeesDiscountPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishBuyFeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishSellFeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("TicketFinishBuyFeesAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("TicketFinishSellFeesAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ProductCategory_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Depositories_ID", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetPackage_SettlementFees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Option_ID", _iOption_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];                                                              // ID -> SPBF_ID
                    if (Convert.ToInt32(drList["Product_ID"]) == 0) dtRow["Product_Title"] = "'Ολα";
                    else dtRow["Product_Title"] = drList["Product_Title"] + "";
                    if (Convert.ToInt32(drList["ProductCategory_ID"]) == 0) dtRow["ProductCategory_Title"] = "'Ολες";
                    else dtRow["ProductCategory_Title"] = drList["ProductCategory_Title"] + "";
                    if (Convert.ToInt32(drList["Depositories_ID"]) == 0) dtRow["Depositories_Title"] = "Όλα";
                    else dtRow["Depositories_Title"] = drList["Depositories_Title"];
                    dtRow["AmountFrom"] = drList["AmountFrom"];
                    dtRow["AmountTo"] = drList["AmountTo"];
                    dtRow["BuyFeesPercent"] = drList["BuyFeesPercent"];
                    dtRow["SellFeesPercent"] = drList["SellFeesPercent"];
                    dtRow["TicketFeesBuyAmount"] = drList["TicketFeesBuyAmount"];
                    dtRow["TicketFeesSellAmount"] = drList["TicketFeesSellAmount"];
                    dtRow["TicketFeesCurr"] = drList["TicketFeesCurr"];
                    dtRow["MinimumFees"] = drList["MinimumFees"];
                    dtRow["MinimumFeesCurr"] = drList["MinimumFeesCurr"];
                    dtRow["DiscountDateFrom"] = _dFrom.ToString("dd/MM/yyyy");
                    dtRow["DiscountDateTo"] = _dTo.ToString("dd/MM/yyyy");
                    dtRow["ID"] = 0;
                    dtRow["FeesDiscountPercent"] = 0;
                    dtRow["TicketFeesDiscountPercent"] = 0;
                    dtRow["FinishBuyFeesPercent"] = drList["BuyFeesPercent"];
                    dtRow["FinishSellFeesPercent"] = drList["SellFeesPercent"];
                    dtRow["TicketFinishBuyFeesAmount"] = drList["TicketFeesBuyAmount"];
                    dtRow["TicketFinishSellFeesAmount"] = drList["TicketFeesSellAmount"];
                    dtRow["Product_ID"] = drList["Product_ID"];
                    dtRow["ProductCategory_ID"] = drList["ProductCategory_ID"];
                    dtRow["Depositories_ID"] = drList["Depositories_ID"];
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();

                if (_bIncludeDiscount)
                {

                    cmd = new SqlCommand("GetContract_SettlementFees", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Contract_ID", _iContract_ID));
                    cmd.Parameters.Add(new SqlParameter("@Contract_Packages_ID", _iContract_Packages_ID));
                    drList = cmd.ExecuteReader();
                    while (drList.Read())
                    {
                        foundRows = _dtList.Select("SPBF_ID=" + drList["SPBF_ID"]);
                        if (foundRows.Length > 0)
                        {
                            foundRows[0]["DiscountDateFrom"] = drList["DateFrom"];
                            foundRows[0]["DiscountDateTo"] = drList["DateTo"];
                            foundRows[0]["FeesDiscountPercent"] = drList["SettlementFeesDiscount"];
                            foundRows[0]["TicketFeesDiscountPercent"] = drList["TicketFeesDiscount"];
                            foundRows[0]["FinishBuyFeesPercent"] = drList["SettlementFeesBuy"];
                            foundRows[0]["FinishSellFeesPercent"] = drList["SettlementFeesSell"];
                            foundRows[0]["TicketFinishBuyFeesAmount"] = drList["TicketFeesBuy"];
                            foundRows[0]["TicketFinishSellFeesAmount"] = drList["TicketFeesSell"];
                        }
                    }
                    drList.Close();
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
                using (SqlCommand cmd = new SqlCommand("InsertClientsSettlementFees", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@SPSF_ID", SqlDbType.Int).Value = _iSPSF_ID;
                    cmd.Parameters.Add("@Product_ID", SqlDbType.Float).Value = _iProduct_ID;
                    cmd.Parameters.Add("@ProductCategory_ID", SqlDbType.Float).Value = _iProductCategory_ID;
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = _dFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = _dTo;
                    cmd.Parameters.Add("@SettlementFeesDiscount", SqlDbType.Decimal).Value = _decSettlementFeesDiscount;
                    cmd.Parameters.Add("@TicketFeesDiscount", SqlDbType.Decimal).Value = _decTicketFeesDiscount;
                    cmd.Parameters.Add("@SettlementFeesBuy", SqlDbType.Decimal).Value = _decSettlementFeesBuy;
                    cmd.Parameters.Add("@SettlementFeesSell", SqlDbType.Decimal).Value = _decSettlementFeesSell;
                    cmd.Parameters.Add("@TicketFeesBuy", SqlDbType.Float).Value = _fltTicketFeesBuy;
                    cmd.Parameters.Add("@TicketFeesSell", SqlDbType.Float).Value = _fltTicketFeesSell;

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
                using (SqlCommand cmd = new SqlCommand("EditClientsSettlementFees", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@SPSF_ID", SqlDbType.Int).Value = _iSPSF_ID;
                    cmd.Parameters.Add("@Product_ID", SqlDbType.Float).Value = _iProduct_ID;
                    cmd.Parameters.Add("@ProductCategory_ID", SqlDbType.Float).Value = _iProductCategory_ID;
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = _dFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = _dTo;
                    cmd.Parameters.Add("@SettlementFeesDiscount", SqlDbType.Decimal).Value = _decSettlementFeesDiscount;
                    cmd.Parameters.Add("@TicketFeesDiscount", SqlDbType.Decimal).Value = _decTicketFeesDiscount;
                    cmd.Parameters.Add("@SettlementFeesBuy", SqlDbType.Decimal).Value = _decSettlementFeesBuy;
                    cmd.Parameters.Add("@SettlementFeesSell", SqlDbType.Decimal).Value = _decSettlementFeesSell;
                    cmd.Parameters.Add("@TicketFeesBuy", SqlDbType.Float).Value = _fltTicketFeesBuy;
                    cmd.Parameters.Add("@TicketFeesSell", SqlDbType.Float).Value = _fltTicketFeesSell;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ClientsSettlementFees";
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
        public int Contract_Packages_ID { get { return this._iContract_Packages_ID; } set { this._iContract_Packages_ID = value; } }
        public int SPSF_ID { get { return this._iSPSF_ID; } set { this._iSPSF_ID = value; } }
        public int Product_ID { get { return this._iProduct_ID; } set { this._iProduct_ID = value; } }
        public int ProductCategory_ID { get { return this._iProductCategory_ID; } set { this._iProductCategory_ID = value; } }
        public DateTime DateFrom { get { return this._dFrom; } set { this._dFrom = value; } }
        public DateTime DateTo { get { return this._dTo; } set { this._dTo = value; } }
        public decimal SettlementFeesDiscount { get { return this._decSettlementFeesDiscount; } set { this._decSettlementFeesDiscount = value; } }
        public decimal TicketFeesDiscount { get { return this._decTicketFeesDiscount; } set { this._decTicketFeesDiscount = value; } }
        public decimal SettlementFeesBuy { get { return this._decSettlementFeesBuy; } set { this._decSettlementFeesBuy = value; } }
        public decimal SettlementFeesSell { get { return this._decSettlementFeesSell; } set { this._decSettlementFeesSell = value; } }
        public float TicketFeesBuy { get { return this._fltTicketFeesBuy; } set { this._fltTicketFeesBuy = value; } }
        public float TicketFeesSell { get { return this._fltTicketFeesSell; } set { this._fltTicketFeesSell = value; } }
        public bool IncludeDiscount { get { return this._bIncludeDiscount; } set { this._bIncludeDiscount = value; } }
        public int Option_ID { get { return this._iOption_ID; } set { this._iOption_ID = value; } }
        public int StockExchange_ID { get { return this._iStockExchange_ID; } set { this._iStockExchange_ID = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}






