using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsOrdersDPM_Recs
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int    _iRecord_ID;
        private int    _iDPM_ID;
        private int    _iClient_ID;
        private int    _iContract_ID;
        private int    _iContract_Details_ID;
        private int    _iContract_Packages_ID;
        private int    _iShareCodes_ID;
        private int    _iProduct_ID;
        private int    _iProductCategories_ID;
        private string _sCurrency;
        private int    _iStockExchange_ID;
        private int    _iAktion;
        private int    _iConstant;
        private string _sConstantDate;
        private int    _iPriceType;
        private string _sPrice;
        private string _sPriceUp;
        private string _sPriceDown;
        private string _sQuantity;
        private string _sAmount;
        private string _sTargetPrice;
        private string _sCurrRate_NA;
        private string _sAmount_NA;
        private string _sWeight;
        private int    _iStatus;                          // 0 - new order, 1 - sent to RTO,  2-μην αποδοχή, 3- αποδοχή, 4 - cancelled

        private DataTable _dtList;

        public clsOrdersDPM_Recs()
        {
            this._iRecord_ID = 0;
            this._iDPM_ID = 0;
            this._iClient_ID = 0;
            this._iContract_ID = 0;
            this._iContract_Details_ID = 0;
            this._iContract_Packages_ID = 0;
            this._iShareCodes_ID = 0;
            this._iProduct_ID = 0;
            this._iProductCategories_ID = 0;
            this._sCurrency = "";
            this._iStockExchange_ID = 0;
            this._iAktion = 0;
            this._iConstant = 0;
            this._sConstantDate = "";
            this._iPriceType = 0;
            this._sPrice = "";
            this._sPriceUp = "";
            this._sPriceDown = "";
            this._sQuantity = "";
            this._sAmount = "";
            this._sTargetPrice = "";
            this._sCurrRate_NA = "";
            this._sAmount_NA = "";
            this._sWeight = "";
            this._iStatus = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "DPMOrders_Recs"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iDPM_ID = Convert.ToInt32(drList["DPM_ID"]);
                    this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                    this._iContract_ID = Convert.ToInt32(drList["Contract_ID"]);
                    this._iContract_Details_ID = Convert.ToInt32(drList["Contract_Details_ID"]);
                    this._iContract_Packages_ID = Convert.ToInt32(drList["Contract_Packages_ID"]);
                    this._iShareCodes_ID = Convert.ToInt32(drList["ShareCodes_ID"]);
                    this._iProduct_ID = Convert.ToInt32(drList["Product_ID"]);
                    this._iProductCategories_ID = Convert.ToInt32(drList["ProductCategories_ID"]);
                    this._sCurrency = drList["Currency"] + "";
                    this._iStockExchange_ID = Convert.ToInt32(drList["StockExchange_ID"]);
                    this._iAktion = Convert.ToInt32(drList["Aktion"]);
                    this._iConstant = Convert.ToInt32(drList["Constant"]);
                    this._sConstantDate = drList["ConstantDate"] + "";
                    this._iPriceType = Convert.ToInt32(drList["PriceType"]);
                    this._sPrice = drList["Price"] + "";
                    this._sPriceUp = drList["PriceUp"] + "";
                    this._sPriceDown = drList["PriceDown"] + "";
                    this._sQuantity = drList["Quantity"] + "";
                    this._sAmount = drList["Amount"] + "";
                    this._sTargetPrice = drList["TargetPrice"] + "";
                    this._sCurrRate_NA = drList["CurrRate_NA"] + "";
                    this._sAmount_NA = drList["Amount_NA"] + "";
                    this._sWeight = drList["Weight"] + "";
                    this._iStatus = Convert.ToInt32(drList["Status"]);
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
                _dtList = new DataTable("Orders_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DPM_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Aktion", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_Details_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_Packages_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientFullName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_Code2", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Share_ISIN", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SE_Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SE_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ShareCodes_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ProductCategories_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Constant", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("ConstantDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("PriceType", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Price", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("PriceUp", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("PriceDown", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Quantity", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Amount", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("TargetPrice", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CurrRate_NA", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Amount_NA", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Weight", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("SE_ID", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetOrdersDPM_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DPM_ID", _iDPM_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["DPM_ID"] = drList["DPM_ID"];
                    dtRow["Aktion"] = drList["Aktion"];
                    dtRow["Client_ID"] = drList["Client_ID"];
                    dtRow["Contract_ID"] = drList["Contract_ID"];
                    dtRow["Contract_Details_ID"] = drList["Contract_Details_ID"];
                    dtRow["Contract_Packages_ID"] = drList["Contract_Packages_ID"];
                    dtRow["ContractTitle"] = drList["ContractTitle"];
                    dtRow["Code"] = drList["Code"];
                    dtRow["Portfolio"] = drList["Portfolio"];
                    dtRow["ClientFullName"] = drList["ClientFullName"];
                    dtRow["Share_Title"] = drList["Share_Title"] + "";
                    dtRow["Share_Code"] = drList["Share_Code"] + "";
                    dtRow["Share_Code2"] = drList["Share_Code2"] + "";
                    dtRow["Share_ISIN"] = drList["Share_ISIN"] + "";
                    dtRow["Currency"] = drList["Currency"] + "";
                    dtRow["SE_Code"] = drList["SE_Code"];
                    dtRow["SE_Title"] = drList["SE_Title"];
                    dtRow["ShareCodes_ID"] = drList["ShareCodes_ID"];
                    dtRow["Product_ID"] = drList["Product_ID"];
                    dtRow["ProductCategories_ID"] = drList["ProductCategories_ID"];
                    dtRow["Constant"] = drList["Constant"];
                    dtRow["ConstantDate"] = drList["ConstantDate"] + "";
                    dtRow["PriceType"] = drList["PriceType"];
                    dtRow["Price"] = drList["Price"] + "";
                    dtRow["PriceUp"] = drList["PriceUp"] + "";
                    dtRow["PriceDown"] = drList["PriceDown"] + "";
                    dtRow["Quantity"] = drList["Quantity"] + "";
                    dtRow["Amount"] = drList["Amount"] + "";
                    dtRow["TargetPrice"] = drList["TargetPrice"] + "";
                    dtRow["CurrRate_NA"] = drList["CurrRate_NA"] + "";
                    dtRow["Amount_NA"] = drList["Amount_NA"] + "";
                    dtRow["Weight"] = drList["Weight"] + "";
                    dtRow["Status"] = drList["Status"];
                    dtRow["SE_ID"] = drList["StockExchange_ID"];
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int InsertRecord()
        {
            _iRecord_ID = 0;
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("InsertDPMOrders_Recs", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DPM_ID", SqlDbType.Int).Value = _iDPM_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Details_ID", SqlDbType.Int).Value = _iContract_Details_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@ShareCodes_ID", SqlDbType.Int).Value = _iShareCodes_ID;
                    cmd.Parameters.Add("@Product_ID", SqlDbType.Int).Value = _iProduct_ID;
                    cmd.Parameters.Add("@ProductCategories_ID", SqlDbType.Int).Value = _iProductCategories_ID;
                    cmd.Parameters.Add("@Currency", SqlDbType.NVarChar, 6).Value = _sCurrency;
                    cmd.Parameters.Add("@StockExchange_ID", SqlDbType.Int).Value = _iStockExchange_ID;
                    cmd.Parameters.Add("@Aktion", SqlDbType.Int).Value = _iAktion;
                    cmd.Parameters.Add("@Constant", SqlDbType.Int).Value = _iConstant;
                    cmd.Parameters.Add("@ConstantDate", SqlDbType.NVarChar, 20).Value = _sConstantDate;
                    cmd.Parameters.Add("@PriceType", SqlDbType.Int).Value = _iPriceType;
                    cmd.Parameters.Add("@Price", SqlDbType.NVarChar, 20).Value = _sPrice;
                    cmd.Parameters.Add("@PriceUp", SqlDbType.NVarChar, 20).Value = _sPriceUp;
                    cmd.Parameters.Add("@PriceDown", SqlDbType.NVarChar, 20).Value = _sPriceDown;
                    cmd.Parameters.Add("@Quantity", SqlDbType.NVarChar, 20).Value = _sQuantity;
                    cmd.Parameters.Add("@Amount", SqlDbType.NVarChar, 20).Value = _sAmount;
                    cmd.Parameters.Add("@TargetPrice", SqlDbType.NVarChar, 20).Value = _sTargetPrice;
                    cmd.Parameters.Add("@CurrRate_NA", SqlDbType.NVarChar, 20).Value = _sCurrRate_NA;
                    cmd.Parameters.Add("@Amount_NA", SqlDbType.NVarChar, 20).Value = _sAmount_NA;
                    cmd.Parameters.Add("@Weight", SqlDbType.NVarChar, 20).Value = _sWeight;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;

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
                using (SqlCommand cmd = new SqlCommand("EditDPMOrders_Recs", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@DPM_ID", SqlDbType.Int).Value = _iDPM_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Details_ID", SqlDbType.Int).Value = _iContract_Details_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@ShareCodes_ID", SqlDbType.Int).Value = _iShareCodes_ID;
                    cmd.Parameters.Add("@Product_ID", SqlDbType.Int).Value = _iProduct_ID;
                    cmd.Parameters.Add("@ProductCategories_ID", SqlDbType.Int).Value = _iProductCategories_ID;
                    cmd.Parameters.Add("@Currency", SqlDbType.NVarChar, 6).Value = _sCurrency;
                    cmd.Parameters.Add("@StockExchange_ID", SqlDbType.Int).Value = _iStockExchange_ID;
                    cmd.Parameters.Add("@Aktion", SqlDbType.Int).Value = _iAktion;
                    cmd.Parameters.Add("@Constant", SqlDbType.Int).Value = _iConstant;
                    cmd.Parameters.Add("@ConstantDate", SqlDbType.NVarChar, 20).Value = _sConstantDate;
                    cmd.Parameters.Add("@PriceType", SqlDbType.Int).Value = _iPriceType;
                    cmd.Parameters.Add("@Price", SqlDbType.NVarChar, 20).Value = _sPrice;
                    cmd.Parameters.Add("@PriceUp", SqlDbType.NVarChar, 20).Value = _sPriceUp;
                    cmd.Parameters.Add("@PriceDown", SqlDbType.NVarChar, 20).Value = _sPriceDown;
                    cmd.Parameters.Add("@Quantity", SqlDbType.NVarChar, 20).Value = _sQuantity;
                    cmd.Parameters.Add("@Amount", SqlDbType.NVarChar, 20).Value = _sAmount;
                    cmd.Parameters.Add("@TargetPrice", SqlDbType.NVarChar, 20).Value = _sTargetPrice;
                    cmd.Parameters.Add("@CurrRate_NA", SqlDbType.NVarChar, 20).Value = _sCurrRate_NA;
                    cmd.Parameters.Add("@Amount_NA", SqlDbType.NVarChar, 20).Value = _sAmount_NA;
                    cmd.Parameters.Add("@Weight", SqlDbType.NVarChar, 20).Value = _sWeight;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "DPMOrders_Recs";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int DPM_ID { get { return this._iDPM_ID; } set { this._iDPM_ID = value; } }
        public int Client_ID { get { return this._iClient_ID; } set { this._iClient_ID = value; } }
        public int Contract_ID { get { return this._iContract_ID; } set { this._iContract_ID = value; } }
        public int Contract_Details_ID { get { return this._iContract_Details_ID; } set { this._iContract_Details_ID = value; } }
        public int Contract_Packages_ID { get { return this._iContract_Packages_ID; } set { this._iContract_Packages_ID = value; } } 
        public int ShareCodes_ID { get { return this._iShareCodes_ID; } set { this._iShareCodes_ID = value; } }
        public int Product_ID { get { return this._iProduct_ID; } set { this._iProduct_ID = value; } }
        public int ProductCategories_ID { get { return this._iProductCategories_ID; } set { this._iProductCategories_ID = value; } }
        public string Currency { get { return this._sCurrency; } set { this._sCurrency = value; } }
        public int StockExchange_ID { get { return this._iStockExchange_ID; } set { this._iStockExchange_ID = value; } }
        public int Aktion { get { return this._iAktion; } set { this._iAktion = value; } }
        public int Constant { get { return this._iConstant; } set { this._iConstant = value; } }
        public string ConstantDate { get { return this._sConstantDate; } set { this._sConstantDate = value; } }
        public int PriceType { get { return this._iPriceType; } set { this._iPriceType = value; } }
        public string Price { get { return this._sPrice; } set { this._sPrice = value; } }
        public string PriceUp { get { return this._sPriceUp; } set { this._sPriceUp = value; } }
        public string PriceDown { get { return this._sPriceDown; } set { this._sPriceDown = value; } }
        public string Quantity { get { return this._sQuantity; } set { this._sQuantity = value; } }
        public string Amount { get { return this._sAmount; } set { this._sAmount = value; } }
        public string TargetPrice { get { return this._sTargetPrice; } set { this._sTargetPrice = value; } }
        public string CurrRate_NA { get { return this._sCurrRate_NA; } set { this._sCurrRate_NA = value; } }
        public string Amount_NA { get { return this._sAmount_NA; } set { this._sAmount_NA = value; } }
        public string Weight { get { return this._sWeight; } set { this._sWeight = value; } }
        public int Status { get { return this._iStatus; } set { this._iStatus = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}






