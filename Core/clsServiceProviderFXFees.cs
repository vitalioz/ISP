using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsServiceProviderFXFees
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int   _iRecord_ID;
        private int   _iSPO_ID;
        private int   _iServiceProvider_ID;
        private float _fltAmountFrom;
        private float _fltAmountTo;
        private float _fltFeesPercent;
        private int   _iRetrosessionMethod;
        private float _fltRetrosessionProvider;
        private float _fltRetrosessionCompany;
        private DataTable _dtList;

        public clsServiceProviderFXFees()
        {
            this._iRecord_ID = 0;
            this._iSPO_ID = 0;
            this._iServiceProvider_ID = 0;
            this._fltAmountFrom = 0;
            this._fltAmountTo = 0;
            this._fltFeesPercent = 0;
            this._iRetrosessionMethod = 0;
            this._fltRetrosessionProvider = 0;
            this._fltRetrosessionCompany = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "ServiceProviderFXFees"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iSPO_ID = Convert.ToInt32(drList["SPO_ID"]);
                    this._iServiceProvider_ID = Convert.ToInt32(drList["ServiceProvider_ID"]);
                    this._fltAmountFrom = Convert.ToSingle(drList["AmountFrom"]);
                    this._fltAmountTo = Convert.ToSingle(drList["AmountTo"]);
                    this._fltFeesPercent = Convert.ToSingle(drList["FeesPercent"]);
                    this._iRetrosessionMethod = Convert.ToInt16(drList["RetrosessionMethod"]);
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
                _dtList = new DataTable("ServiceProviderFXFees_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("SPBF_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Product_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ProductCategory_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockExchanges_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AmountFrom", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AmountTo", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesPercent", System.Type.GetType("System.Single"));
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
                dtCol = _dtList.Columns.Add("FinishFeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishSellFeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("TicketFinishBuyFeesAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("TicketFinishSellFeesAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ProductCategory_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("StockExchange_ID", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetPackage_ServiceProviderFXFees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Option_ID", _iSPO_ID));
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
                    dtRow["FeesPercent"] = drList["FeesPercent"];
                    dtRow["SellFeesPercent"] = drList["SellFeesPercent"];
                    dtRow["TicketFeesBuyAmountAmount"] = drList["TicketFeesBuyAmountAmount"];
                    dtRow["TicketFeesSellAmountAmount"] = drList["TicketFeesSellAmountAmount"];
                    dtRow["TicketFeesCurr"] = drList["TicketFeesCurr"];
                    dtRow["MinimumFees"] = drList["MinimumFees"];
                    dtRow["MinimumFeesCurr"] = drList["MinimumFeesCurr"];
                    dtRow["ID"] = 0;
                    dtRow["FeesDiscountPercent"] = 0;
                    dtRow["AmountToPercent"] = 0;
                    dtRow["FinishFeesPercent"] = drList["FeesPercent"];
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
            try  {
                _dtList = new DataTable("ServiceProviderFXFees_List");
                dtCol = _dtList.Columns.Add("AmountFrom", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AmountTo", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RetrosessionMethod", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("RetrosessionProvider", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RetrosessionCompany", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("SPO_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Pseudo_ID", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetServiceProviderFXFees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ServiceProvider_ID", this._iServiceProvider_ID));
                cmd.Parameters.Add(new SqlParameter("@SPO_ID", _iSPO_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["AmountFrom"] = drList["AmountFrom"];
                    dtRow["AmountTo"] = drList["AmountTo"];
                    dtRow["FeesPercent"] = drList["FeesPercent"];
                    dtRow["RetrosessionMethod"] = drList["RetrosessionMethod"];
                    dtRow["RetrosessionProvider"] = drList["RetrosessionProvider"];
                    dtRow["RetrosessionCompany"] = drList["RetrosessionCompany"];
                    dtRow["ID"] = drList["ID"];
                    dtRow["SPO_ID"] = drList["SPO_ID"];
                    dtRow["Status"] = 0;
                    dtRow["Pseudo_ID"] = drList["SPO_ID"];
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
                using (cmd = new SqlCommand("sp_InsertServiceProviderFXFees", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@SPO_ID", SqlDbType.Int).Value = _iSPO_ID;
                    cmd.Parameters.Add("@ServiceProvider_ID", SqlDbType.Int).Value = _iServiceProvider_ID;
                    cmd.Parameters.Add("@AmountFrom", SqlDbType.Float).Value = _fltAmountFrom;
                    cmd.Parameters.Add("@AmountTo", SqlDbType.Float).Value = _fltAmountTo;
                    cmd.Parameters.Add("@FeesPercent", SqlDbType.Float).Value = _fltFeesPercent;
                    cmd.Parameters.Add("@RetrosessionMethod", SqlDbType.Int).Value = _iRetrosessionMethod;
                    cmd.Parameters.Add("@RetrosessionProvider", SqlDbType.Float).Value = _fltRetrosessionProvider;
                    cmd.Parameters.Add("@RetrosessionCompany", SqlDbType.Float).Value = _fltRetrosessionCompany;

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
                using (cmd = new SqlCommand("sp_EditServiceProviderFXFees", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@SPO_ID", SqlDbType.Int).Value = _iSPO_ID;
                    cmd.Parameters.Add("@ServiceProvider_ID", SqlDbType.Int).Value = _iServiceProvider_ID;
                    cmd.Parameters.Add("@AmountFrom", SqlDbType.Float).Value = _fltAmountFrom;
                    cmd.Parameters.Add("@AmountTo", SqlDbType.Float).Value = _fltAmountTo;
                    cmd.Parameters.Add("@FeesPercent", SqlDbType.Float).Value = _fltFeesPercent;
                    cmd.Parameters.Add("@RetrosessionMethod", SqlDbType.Int).Value = _iRetrosessionMethod;
                    cmd.Parameters.Add("@RetrosessionProvider", SqlDbType.Float).Value = _fltRetrosessionProvider;
                    cmd.Parameters.Add("@RetrosessionCompany", SqlDbType.Float).Value = _fltRetrosessionCompany;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ServiceProviderFXFees";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int SPO_ID { get { return this._iSPO_ID; } set { this._iSPO_ID = value; } }
        public int ServiceProvider_ID { get { return this._iServiceProvider_ID; } set { this._iServiceProvider_ID = value; } }
        public float AmountFrom { get { return this._fltAmountFrom; } set { this._fltAmountFrom = value; } }
        public float AmountTo { get { return this._fltAmountTo; } set { this._fltAmountTo = value; } }
        public float FeesPercent { get { return this._fltFeesPercent; } set { this._fltFeesPercent = value; } }
        public int RetrosessionMethod { get { return this._iRetrosessionMethod; } set { this._iRetrosessionMethod = value; } }
        public float RetrosessionProvider { get { return this._fltRetrosessionProvider; } set { this._fltRetrosessionProvider = value; } }
        public float RetrosessionCompany { get { return this._fltRetrosessionCompany; } set { this._fltRetrosessionCompany = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}
