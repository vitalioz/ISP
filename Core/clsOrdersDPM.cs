using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsOrdersDPM
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int      _iRecord_ID;
        private int      _iOrderType;
        private int      _iClient_ID;
        private int      _iContract_ID;
        private int      _iContract_Details_ID;
        private int      _iContract_Packages_ID;
        private float    _fltAllocationPercent;
        private int      _iStockCompany_ID;
        private float    _fltAUM;
        private int      _iAktion;
        private DateTime _dAktionDate;
        private int      _iShareCodes_ID;
        private string   _sShare_Title ;
        private string   _sShare_ISIN;
        private string   _sShare_Code;
        private int      _iStockExchange_ID;
        private string   _sCurrency;
        private int      _iProductsCount;
        private string   _sProducts;
        private int      _iPriceType;
        private string   _sPrice;
        private string   _sQuantity;
        private int      _iConstant;
        private DateTime _dConstantDate;
        private DateTime _dSentDate;
        private string   _sNotes;
        private int      _iStatus;                          // 0 - new order, 1 - sent to RTO,  2-μην αποδοχή, 3- αποδοχή, 4 - cancelled
        private int      _iUser_ID;
        private int      _iAuthor_ID;
        private int      _iProduct_ID;

        private DateTime _dDateFrom;
        private DateTime _dDateTo;

        private DataTable _dtList;
        public clsOrdersDPM()
        {
            this._iRecord_ID = 0;
            this._iOrderType = 0;
            this._iClient_ID = 0;
            this._iContract_ID = 0;
            this._iContract_Details_ID = 0;
            this._iContract_Packages_ID = 0;
            this._fltAllocationPercent = 0;
            this._iStockCompany_ID = 0;
            this._fltAUM = 0;
            this._iAktion = 0;
            this._dAktionDate = Convert.ToDateTime("1900/01/01");
            this._iShareCodes_ID = 0;
            this._sShare_Title = "";
            this._sShare_ISIN =  "";
            this._sShare_Code = "";
            this._iStockExchange_ID = 0;
            this._sCurrency = "";
            this._iProductsCount = 0;
            this._sProducts = "";
            this._iPriceType = 0;
            this._sPrice = "";
            this._sQuantity = "";
            this._iConstant = 0;
            this._dConstantDate = Convert.ToDateTime("1900/01/01");
            this._dSentDate = Convert.ToDateTime("1900/01/01");
            this._sNotes = "";
            this._iStatus = 0;
            this._iUser_ID = 0;
            this._iAuthor_ID = 0;
            this._iProduct_ID = 0;
        }
        public void GetRecord()
        {
            drList = null;
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetOrdersDPM_Title", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", this._iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iOrderType = Convert.ToInt32(drList["OrderType"]);
                    this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                    this._iContract_ID = Convert.ToInt32(drList["Contract_ID"]);
                    this._iContract_Details_ID = Convert.ToInt32(drList["Contract_Details_ID"]);
                    this._iContract_Packages_ID = Convert.ToInt32(drList["Contract_Packages_ID"]);
                    this._fltAllocationPercent = Convert.ToSingle(drList["AllocationPercent"]);
                    this._iStockCompany_ID = Convert.ToInt32(drList["StockCompany_ID"]);
                    this._fltAUM = Convert.ToSingle(drList["AUM"]);
                    this._iAktion = Convert.ToInt32(drList["Aktion"]);                    
                    this._dAktionDate = Convert.ToDateTime(drList["AktionDate"]);
                    this._iShareCodes_ID = Convert.ToInt32(drList["ShareCodes_ID"]);
                    if (this._iShareCodes_ID != 0) {
                        this._sShare_Title = drList["Share_Title"] + "";
                        this._sShare_ISIN = drList["Share_ISIN"] + "";
                        this._sShare_Code = drList["Share_Code"] + "";
                        this._iStockExchange_ID = Convert.ToInt32(drList["StockExchange_ID"]);
                        this._sCurrency = drList["Curr"] + "";
                        this._iPriceType = Convert.ToInt32(drList["PriceType"]);
                        this._sPrice = drList["Price"] + "";
                        this._sQuantity = drList["Quantity"] + "";
                        this._iProductsCount = 1;
                        this._sProducts = "";
                        this._iProduct_ID = Convert.ToInt32(drList["Product_ID"]);
                    }
                    else {
                        this._sShare_Title = "";
                        this._sShare_ISIN = "";
                        this._sShare_Code = "";
                        this._iStockExchange_ID = 0;
                        this._sCurrency = "";
                        this._iPriceType = 0;
                        this._sPrice = "0";
                        this._sQuantity = "0";
                        this._iProductsCount = Convert.ToInt32(drList["ProductsCount"]);
                        this._sProducts = drList["Products"] + "";
                        this._iProduct_ID = 0;
                    }
                    this._iConstant = Convert.ToInt32(drList["Constant"]);
                    this._dConstantDate = Convert.ToDateTime(drList["ConstantDate"]);
                    this._dSentDate = Convert.ToDateTime(drList["SentDate"]);
                    this._sNotes = drList["Notes"] + "";
                    this._iStatus = Convert.ToInt32(drList["Status"]);
                    this._iUser_ID = Convert.ToInt32(drList["User_ID"]);
                    this._iAuthor_ID = Convert.ToInt32(drList["Author_ID"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("Client_ID", typeof(int));
            _dtList.Columns.Add("Contract_ID", typeof(int));
            _dtList.Columns.Add("Contract_Details_ID", typeof(int));
            _dtList.Columns.Add("Contract_Packages_ID", typeof(int));
            _dtList.Columns.Add("StockCompany_ID", typeof(int));
            _dtList.Columns.Add("AUM", typeof(float));
            _dtList.Columns.Add("AktionDate", typeof(DateTime));
            _dtList.Columns.Add("SentDate", typeof(DateTime));
            _dtList.Columns.Add("Products", typeof(string));
            _dtList.Columns.Add("PriceType", typeof(int));
            _dtList.Columns.Add("Constant", typeof(int));
            _dtList.Columns.Add("ConstantDate", typeof(DateTime));
            _dtList.Columns.Add("Notes", typeof(string));
            _dtList.Columns.Add("Status", typeof(int));
            _dtList.Columns.Add("User_ID", typeof(int));
            _dtList.Columns.Add("ClientSurname", typeof(string));
            _dtList.Columns.Add("ClientFirstname", typeof(string));
            _dtList.Columns.Add("ContractTitle", typeof(string));
            _dtList.Columns.Add("Code", typeof(string));
            _dtList.Columns.Add("Portfolio", typeof(string));
            _dtList.Columns.Add("UserSurname", typeof(string));
            _dtList.Columns.Add("UserFirstname", typeof(string));
            _dtList.Columns.Add("Provider_Title", typeof(string));
            drList = null;
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetDMPOrders", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dDateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dDateTo));
                cmd.Parameters.Add(new SqlParameter("@User_ID", this._iUser_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["Client_ID"], drList["Contract_ID"], drList["Contract_Details_ID"], drList["Contract_Packages_ID"],
                                     drList["StockCompany_ID"], drList["AUM"], drList["AktionDate"], drList["SentDate"], drList["Products"], 
                                     drList["PriceType"], drList["Constant"], drList["ConstantDate"], drList["Notes"], drList["Status"], drList["User_ID"], 
                                     drList["ClientSurname"], drList["ClientFirstname"], drList["ContractTitle"], drList["Code"], drList["Portfolio"], 
                                     drList["UserSurname"], drList["UserFirstname"], drList["Provider_Title"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList_NewOrders()
        {
            decimal decTemp = 0;
            _dtList = new DataTable("DPMOrders_List");
            dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("DPM_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("OrderType", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("StockCompany_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("StockCompany_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("StockExchange_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("StockExchange_Code", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Aktion", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Product_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ProductCategory_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Product_Category", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Share_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Share_Code", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Share_Code2", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Share_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Share_ISIN", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("PriceType", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Price", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Quantity", System.Type.GetType("System.Decimal"));
            dtCol = _dtList.Columns.Add("Amount", System.Type.GetType("System.Decimal"));
            dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Constant", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ConstantDate", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ProductStockExchange_Code", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("DPM_Notes", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Diax_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Diax_Fullname", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ClientFullName", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Contract_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Contract_Details_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Contract_Packages_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("SentDate", System.Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int16"));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetOrdersDPM_New", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dDateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dDateTo));
                cmd.Parameters.Add(new SqlParameter("@User_ID", this._iUser_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];                    
                    this.dtRow["DPM_ID"] = drList["DPM_ID"];
                    this.dtRow["OrderType"] = drList["OrderType"];
                    this.dtRow["ContractTitle"] = drList["ContractTitle"] + "";
                    this.dtRow["StockCompany_ID"] = drList["StockCompany_ID"];
                    this.dtRow["StockCompany_Title"] = drList["StockCompanyTitle"] + "";
                    this.dtRow["StockExchange_ID"] = drList["StockExchange_ID"];
                    this.dtRow["StockExchange_Code"] = drList["StockExchange_Code"] + "";
                    this.dtRow["ProductStockExchange_Code"] = drList["StockExchange_Code"] + "";
                    this.dtRow["Code"] = drList["Code"] + "";
                    this.dtRow["Portfolio"] = drList["Portfolio"] + "";
                    this.dtRow["Aktion"] = drList["Aktion"];
                    this.dtRow["Product_ID"] = drList["Product_ID"];
                    this.dtRow["Product_Title"] = drList["Product_Title"] + "";
                    this.dtRow["ProductCategory_ID"] = drList["ProductCategories_ID"];
                    this.dtRow["Product_Category"] = drList["Product_Category"];
                    this.dtRow["Share_ID"] = drList["ShareCodes_ID"];
                    this.dtRow["Share_Code"] = drList["Share_Code"] + "";
                    this.dtRow["Share_Code2"] = drList["Share_Code2"] + "";
                    this.dtRow["Share_Title"] = drList["Share_Title"] + "";
                    this.dtRow["Share_ISIN"] = drList["Share_ISIN"] + "";
                    this.dtRow["PriceType"] = drList["PriceType"];
                    this.dtRow["Price"] = drList["Price"];
                    this.dtRow["Quantity"] = drList["Quantity"];
                    this.dtRow["Amount"] = drList["Amount"];
                    this.dtRow["Currency"] = drList["Currency"] + "";
                    this.dtRow["Constant"] = drList["Constant"];
                    this.dtRow["ConstantDate"] = drList["ConstantDate"];
                    this.dtRow["DPM_Notes"] = drList["Notes"];
                    this.dtRow["Diax_ID"] = drList["User_ID"];
                    this.dtRow["Diax_Fullname"] = drList["Diax_Fullname"];
                    this.dtRow["ClientFullName"] = drList["ClientFullName"];
                    this.dtRow["Client_ID"] = drList["Client_ID"];
                    this.dtRow["Contract_ID"] = drList["Contract_ID"];
                    this.dtRow["Contract_Details_ID"] = drList["Contract_Details_ID"];
                    this.dtRow["Contract_Packages_ID"] = drList["Contract_Packages_ID"];
                    this.dtRow["Status"] = drList["Status"];
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();

                cmd = new SqlCommand("GetOrdersDPM_New2", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dDateFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dDateTo));
                cmd.Parameters.Add(new SqlParameter("@User_ID", this._iUser_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = 0;
                    this.dtRow["DPM_ID"] = drList["ID"];
                    this.dtRow["OrderType"] = drList["OrderType"];
                    this.dtRow["ContractTitle"] = "";
                    this.dtRow["StockCompany_ID"] = drList["StockCompany_ID"];
                    this.dtRow["StockCompany_Title"] = drList["StockCompanyTitle"] + "";
                    this.dtRow["StockExchange_ID"] = drList["StockExchange_ID"];
                    this.dtRow["StockExchange_Code"] = drList["StockExchange_Code"] + "";
                    this.dtRow["ProductStockExchange_Code"] = drList["StockExchange_Code"] + "";
                    this.dtRow["Code"] = "";
                    this.dtRow["Portfolio"] = "";
                    this.dtRow["Aktion"] = drList["Aktion"];
                    this.dtRow["Product_ID"] = drList["Product_ID"];
                    this.dtRow["Product_Title"] = drList["Product_Title"] + "";
                    this.dtRow["ProductCategory_ID"] = drList["ProductCategory_ID"];
                    this.dtRow["Product_Category"] = drList["Product_Category"];
                    this.dtRow["Share_ID"] = drList["ShareCodes_ID"];
                    this.dtRow["Share_Code"] = drList["Share_Code"] + "";
                    this.dtRow["Share_Code2"] = drList["Share_Code2"] + "";
                    this.dtRow["Share_Title"] = drList["Share_Title"] + "";
                    this.dtRow["Share_ISIN"] = drList["Share_ISIN"] + "";
                    this.dtRow["PriceType"] = drList["PriceType"];
                    this.dtRow["Price"] = drList["Price"];
                    this.dtRow["Quantity"] = drList["Quantity"];
                    decTemp = 0;
                    if (Global.IsNumeric(drList["Price"]) && Global.IsNumeric(drList["Quantity"]))
                        decTemp = Convert.ToDecimal(drList["Price"]) * Convert.ToDecimal(drList["Quantity"]);
                    this.dtRow["Amount"] = decTemp.ToString("0.00");
                    this.dtRow["Currency"] = drList["Currency"] + "";
                    this.dtRow["Constant"] = drList["Constant"];
                    this.dtRow["ConstantDate"] = drList["ConstantDate"];
                    this.dtRow["DPM_Notes"] = drList["Notes"];
                    this.dtRow["Diax_ID"] = drList["User_ID"];
                    this.dtRow["Diax_Fullname"] = drList["Diax_Fullname"];
                    this.dtRow["ClientFullName"] = "";
                    this.dtRow["Client_ID"] = 0;
                    this.dtRow["Contract_ID"] = 0;
                    this.dtRow["Contract_Details_ID"] = 0;
                    this.dtRow["Contract_Packages_ID"] = 0;
                    this.dtRow["SentDate"] = drList["SentDate"];
                    this.dtRow["Status"] = drList["Status"];
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
                using (SqlCommand cmd = new SqlCommand("InsertDPMOrders", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@OrderType", SqlDbType.Int).Value = _iOrderType;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Details_ID", SqlDbType.Int).Value = _iContract_Details_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@AllocationPercent", SqlDbType.Float).Value = _fltAllocationPercent;
                    cmd.Parameters.Add("@StockCompany_ID", SqlDbType.Int).Value = _iStockCompany_ID;
                    cmd.Parameters.Add("@AUM", SqlDbType.Float).Value = _fltAUM;
                    cmd.Parameters.Add("@Aktion", SqlDbType.Int).Value = _iAktion;
                    cmd.Parameters.Add("@AktionDate", SqlDbType.DateTime).Value = _dAktionDate;
                    cmd.Parameters.Add("@ShareCodes_ID", SqlDbType.Int).Value = _iShareCodes_ID;
                    cmd.Parameters.Add("@ProductsCount", SqlDbType.Int).Value = _iProductsCount;
                    cmd.Parameters.Add("@Products", SqlDbType.NVarChar, 100).Value = _sProducts;
                    cmd.Parameters.Add("@PriceType", SqlDbType.Int).Value = _iPriceType;
                    cmd.Parameters.Add("@Price", SqlDbType.NVarChar, 20).Value = _sPrice;
                    cmd.Parameters.Add("@Quantity", SqlDbType.NVarChar, 20).Value = _sQuantity;
                    cmd.Parameters.Add("@Constant", SqlDbType.Int).Value = _iConstant;
                    cmd.Parameters.Add("@ConstantDate", SqlDbType.DateTime).Value = _dConstantDate;
                    cmd.Parameters.Add("@SentDate", SqlDbType.DateTime).Value = _dSentDate;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 500).Value = _sNotes;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = _iUser_ID;
                    cmd.Parameters.Add("@Author_ID", SqlDbType.Int).Value = _iAuthor_ID;

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
                using (SqlCommand cmd = new SqlCommand("EditDPMOrders", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Details_ID", SqlDbType.Int).Value = _iContract_Details_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@AllocationPercent", SqlDbType.Float).Value = _fltAllocationPercent;
                    cmd.Parameters.Add("@StockCompany_ID", SqlDbType.Int).Value = _iStockCompany_ID;
                    cmd.Parameters.Add("@AUM", SqlDbType.Float).Value = _fltAUM;
                    cmd.Parameters.Add("@Aktion", SqlDbType.Int).Value = _iAktion;
                    cmd.Parameters.Add("@AktionDate", SqlDbType.DateTime).Value = _dAktionDate;
                    cmd.Parameters.Add("@ShareCodes_ID", SqlDbType.Int).Value = _iShareCodes_ID;
                    cmd.Parameters.Add("@ProductsCount", SqlDbType.Int).Value = _iProductsCount;
                    cmd.Parameters.Add("@Products", SqlDbType.NVarChar, 100).Value = _sProducts;
                    cmd.Parameters.Add("@PriceType", SqlDbType.Int).Value = _iPriceType;
                    cmd.Parameters.Add("@Price", SqlDbType.NVarChar, 20).Value = _sPrice;
                    cmd.Parameters.Add("@Quantity", SqlDbType.NVarChar, 20).Value = _sQuantity;
                    cmd.Parameters.Add("@Constant", SqlDbType.Int).Value = _iConstant;
                    cmd.Parameters.Add("@ConstantDate", SqlDbType.DateTime).Value = _dConstantDate;
                    cmd.Parameters.Add("@SentDate", SqlDbType.DateTime).Value = _dSentDate;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 500).Value = _sNotes;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "DPMOrders";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return _iRecord_ID; } set { _iRecord_ID = value; } }
        public int OrderType { get { return _iOrderType; } set { _iOrderType = value; } }
        public int Client_ID { get { return _iClient_ID; } set { _iClient_ID = value; } }
        public int Contract_ID { get { return _iContract_ID; } set { _iContract_ID = value; } }
        public int Contract_Details_ID { get { return this._iContract_Details_ID; } set { this._iContract_Details_ID = value; } }
        public int Contract_Packages_ID { get { return this._iContract_Packages_ID; } set { this._iContract_Packages_ID = value; } }
        public float AllocationPercent { get { return this._fltAllocationPercent; } set { this._fltAllocationPercent = value; } }
        public int StockCompany_ID { get { return this._iStockCompany_ID; } set { this._iStockCompany_ID = value; } }
        public float AUM { get { return this._fltAUM; } set { this._fltAUM = value; } }
        public int Aktion { get { return _iAktion; } set { _iAktion = value; } }
        public DateTime AktionDate { get { return _dAktionDate; } set { _dAktionDate = value; } }
        public int ShareCodes_ID { get { return _iShareCodes_ID; } set { _iShareCodes_ID = value; } }
        public string Share_Title { get { return _sShare_Title; } set { _sShare_Title = value; } }
        public string Share_ISIN { get { return _sShare_ISIN; } set { _sShare_ISIN = value; } }
        public string Share_Code { get { return _sShare_Code; } set { _sShare_Code = value; } }
        public int StockExchange_ID { get { return _iStockExchange_ID; } set { _iStockExchange_ID = value; } }
        public string Currency { get { return _sCurrency; } set { _sCurrency = value; } }
        public int ProductsCount { get { return _iProductsCount; } set { _iProductsCount = value; } }
        public string Products { get { return _sProducts; } set { _sProducts = value; } }
        public int PriceType { get { return _iPriceType; } set { _iPriceType = value; } }
        public string Price { get { return _sPrice; } set { _sPrice = value; } }
        public string Quantity { get { return _sQuantity; } set { _sQuantity = value; } }
        public int Constant { get { return _iConstant; } set { _iConstant = value; } }
        public DateTime ConstantDate { get { return _dConstantDate; } set { _dConstantDate = value; } }
        public DateTime SentDate { get { return _dSentDate; } set { _dSentDate = value; } }
        public string Notes { get { return _sNotes; } set { _sNotes = value; } }
        public int Status { get { return _iStatus; } set { _iStatus = value; } }
        public int User_ID { get { return _iUser_ID; } set { _iUser_ID = value; } }
        public int Author_ID { get { return _iAuthor_ID; } set { _iAuthor_ID = value; } }
        public int Product_ID { get { return _iProduct_ID; } set { _iProduct_ID = value; } }
        public DateTime DateFrom { get { return this._dDateFrom; } set { this._dDateFrom = value; } }
        public DateTime DateTo { get { return this._dDateTo; } set { this._dDateTo = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
