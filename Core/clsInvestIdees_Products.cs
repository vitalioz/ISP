using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsInvestIdees_Products
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int _iRecord_ID;
        private DateTime _dDateIns;
        private int _iII_ID;
        private int _iShareCodes_ID;
        private int _iProduct_ID;
        private int _iProductCategories_ID;
        private string _sCurrency;
        private int _iStockExchange_ID;
        private int _iEnergia;
        private int _iAktion;
        private int _iConstant;
        private string _sConstantDate;
        private int    _iType;
        private string _sPrice;
        private string _sPriceUp;
        private string _sPriceDown;
        private string _sQuantity;        
        private string _sAmount;
        private string _sAmount_NA;
        private string _sWeight;
        private int _iAttachFiles;        
        private int _iLineStatus;
        private string _sNotes;
        private string _sURL_IR;
        private string _sSummaryLink;

        private DataTable _dtList;

        public clsInvestIdees_Products()
        {
            this._iRecord_ID = 0;
            this._dDateIns = Convert.ToDateTime("1900/01/01");
            this._iII_ID = 0;
            this._iShareCodes_ID = 0;
            this._iProduct_ID = 0;
            this._iProductCategories_ID = 0;
            this._sCurrency = "";
            this._iStockExchange_ID = 0;
            this._iEnergia = 0;
            this._iAktion = 0;
            this._iConstant = 0;
            this._sConstantDate = "";
            this._iType = 0;
            this._sPrice = "";
            this._sPriceUp = "";
            this._sPriceDown = "";
            this._sQuantity = "";
            this._sAmount = "";
            this._sAmount_NA = "";
            this._sWeight = "";
            this._iAttachFiles = 0;
            this._iLineStatus = 0;
            this._sNotes = "";
            this._sURL_IR = "";
            this._sSummaryLink = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "InvestIdees_Products"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                { 
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iII_ID = Convert.ToInt32(drList["II_ID"]);
                    this._iShareCodes_ID = Convert.ToInt32(drList["ShareCodes_ID"]);
                    this._iProduct_ID = Convert.ToInt32(drList["Product_ID"]);
                    this._iProductCategories_ID = Convert.ToInt32(drList["ProductCategories_ID"]);
                    this._sCurrency = drList["Curr"] + "";
                    this._iStockExchange_ID = Convert.ToInt32(drList["StockExchange_ID"]);
                    this._iEnergia = Convert.ToInt32(drList["Energia"]);
                    this._iAktion = Convert.ToInt32(drList["Aktion"]);
                    this._iConstant = Convert.ToInt32(drList["Constant"]);
                    this._sConstantDate = drList["ConstantDate"] + "";
                    this._iType = Convert.ToInt32(drList["Type"]);
                    this._sPrice = drList["Price"] + "";
                    this._sPriceUp = drList["PriceUp"] + "";
                    this._sPriceDown = drList["PriceDown"] + "";
                    this._sQuantity = drList["Quantity"] + "";
                    this._sAmount = drList["Amount"] + "";
                    this._sAmount_NA = drList["Amount_NA"] + "";
                    this._sWeight = drList["Weight"] + "";
                    this._iAttachFiles = Convert.ToInt32(drList["AttachFiles"]);
                    this._iLineStatus = Convert.ToInt32(drList["LineStatus"]);
                    this._sNotes = drList["Notes"] + "";
                    this._sURL_IR = drList["URL_IR"] + "";
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
                dtCol = _dtList.Columns.Add("II_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Aktion", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Code2", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Code3", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ISIN", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Curr", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockExchange_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockExchange_FullTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ShareType", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ShareTitles_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ShareCodes_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Product_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ProductCategories_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ProductsCategories_Title", System.Type.GetType("System.String"));                
                dtCol = _dtList.Columns.Add("BondType", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Constant", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("ConstantDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Type", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Price", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("PriceUp", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("PriceDown", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Quantity", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Amount", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Amount_NA", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Weight", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AttachFiles", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Coupone", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CouponeTypes_Title", System.Type.GetType("System.String"));                
                dtCol = _dtList.Columns.Add("SectorTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("MoodysRating", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("FitchsRating", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SPRating", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ICAPRating", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("BBG_ComplexAttribute", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Date2", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("FrequencyClipping", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("InvestmentAreaTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("CountryTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DescriptionEn", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DescriptionGr", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("InvestGoal", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RiskCurr", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("StockExchange_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("LineStatus", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Energia", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("ComplexProduct", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("InvestType_Retail", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("InvestType_Prof", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Distrib_ExecOnly", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Distrib_Advice", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("Distrib_PortfolioManagment", System.Type.GetType("System.Int16"));
                dtCol = _dtList.Columns.Add("SurveyedKIID", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Maturity", System.Type.GetType("System.String"));                
                dtCol = _dtList.Columns.Add("Notes", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("URL", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("URL_IR", System.Type.GetType("System.String"));

                conn.Open();
                cmd = new SqlCommand("GetInvestIdees_Products", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@II_ID", _iII_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["II_ID"] = drList["II_ID"];
                    dtRow["Aktion"] = drList["Aktion"];
                    dtRow["Title"] = drList["Title"] + "";
                    dtRow["Code"] = drList["Code"] + "";
                    dtRow["Code2"] = drList["Code2"] + "";
                    dtRow["Code3"] = drList["Code3"] + "";
                    dtRow["ISIN"] = drList["ISIN"] + "";
                    dtRow["Curr"] = drList["Curr"] + "";
                    dtRow["StockExchange_Title"] = drList["StockExchange_Title"];
                    dtRow["StockExchange_FullTitle"] = drList["StockExchange_FullTitle"];
                    dtRow["ShareType"] = drList["ShareType"];
                    dtRow["ShareCodes_ID"] = drList["ShareCodes_ID"];
                    dtRow["ShareTitles_ID"] = drList["ShareTitles_ID"];
                    dtRow["Product_ID"] = drList["Product_ID"];
                    dtRow["Product_Title"] = drList["Product_Title"] + "";
                    dtRow["ProductCategories_ID"] = drList["ProductCategories_ID"];
                    dtRow["ProductsCategories_Title"] = drList["ProductsCategories_Title"] + "";
                    dtRow["BondType"] = drList["BondType"];
                    dtRow["Constant"] = drList["Constant"];
                    dtRow["ConstantDate"] = drList["ConstantDate"] + "";
                    dtRow["Type"] = drList["Type"];
                    dtRow["Price"] = drList["Price"] + "";
                    dtRow["PriceUp"] = drList["PriceUp"] + "";
                    dtRow["PriceDown"] = drList["PriceDown"] + "";
                    dtRow["Quantity"] = drList["Quantity"] + "";
                    dtRow["Amount"] = drList["Amount"] + "";
                    if (Global.IsNumeric(drList["Amount_NA"] + ""))  dtRow["Amount_NA"] = drList["Amount_NA"] + "";
                    dtRow["Amount_NA"] = "0";
                    dtRow["Weight"] = drList["Weight"] + "";
                    dtRow["AttachFiles"] = drList["AttachFiles"] + "";
                    dtRow["Coupone"] = drList["Coupone"] + "";
                    dtRow["CouponeTypes_Title"] = drList["CouponeTypes_Title"] + "";                    
                    dtRow["SectorTitle"] = drList["SectorTitle"] + "";
                    dtRow["MoodysRating"] = drList["MoodysRating"] + "";
                    dtRow["FitchsRating"] = drList["FitchsRating"] + "";
                    dtRow["SPRating"] = drList["SPRating"] + "";
                    dtRow["ICAPRating"] = drList["ICAPRating"] + "";
                    dtRow["BBG_ComplexAttribute"] = drList["BBG_ComplexAttribute"] + "";
                    dtRow["Date2"] = drList["Date2"];
                    dtRow["FrequencyClipping"] = drList["FrequencyClipping"];
                    dtRow["InvestmentAreaTitle"] = drList["InvestmentAreaTitle"];
                    dtRow["CountryTitle"] = drList["CountryTitle"];
                    dtRow["DescriptionEn"] = drList["DescriptionEn"];
                    dtRow["DescriptionGr"] = drList["DescriptionGr"];
                    dtRow["InvestGoal"] = drList["InvestGoal"];
                    dtRow["RiskCurr"] = drList["RiskCurr"];
                    dtRow["StockExchange_ID"] = drList["StockExchange_ID"];
                    dtRow["LineStatus"] = drList["LineStatus"];
                    dtRow["Energia"] = drList["Energia"];
                    dtRow["ComplexProduct"] = drList["ComplexProduct"];                    
                    dtRow["InvestType_Retail"] = drList["InvestType_Retail"];
                    dtRow["InvestType_Prof"] = drList["InvestType_Prof"];
                    dtRow["Distrib_ExecOnly"] = drList["Distrib_ExecOnly"];
                    dtRow["Distrib_Advice"] = drList["Distrib_Advice"];
                    dtRow["Distrib_PortfolioManagment"] = drList["Distrib_PortfolioManagment"];                    
                    dtRow["SurveyedKIID"] = drList["SurveyedKIID"];
                    dtRow["Maturity"] = drList["Maturity"];
                    dtRow["Notes"] = drList["Notes"] + "";
                    dtRow["URL"] = drList["URL"] + "";
                    dtRow["URL_IR"] = drList["URL_IR"] + "";
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
                using (SqlCommand cmd = new SqlCommand("InsertInvestIdees_Products", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@II_ID", SqlDbType.Int).Value = _iII_ID;
                    cmd.Parameters.Add("@ShareCodes_ID", SqlDbType.Int).Value = _iShareCodes_ID;
                    cmd.Parameters.Add("@Product_ID", SqlDbType.Int).Value = _iProduct_ID;
                    cmd.Parameters.Add("@ProductCategories_ID", SqlDbType.Int).Value = _iProductCategories_ID;
                    cmd.Parameters.Add("@Curr", SqlDbType.NVarChar, 6).Value = _sCurrency;
                    cmd.Parameters.Add("@StockExchange_ID", SqlDbType.Int).Value = _iStockExchange_ID;
                    cmd.Parameters.Add("@Energia", SqlDbType.Int).Value = _iEnergia;
                    cmd.Parameters.Add("@Aktion", SqlDbType.Int).Value = _iAktion;
                    cmd.Parameters.Add("@Constant", SqlDbType.Int).Value = _iConstant;
                    cmd.Parameters.Add("@ConstantDate", SqlDbType.NVarChar, 20).Value = _sConstantDate;
                    cmd.Parameters.Add("@Type", SqlDbType.Int).Value = _iType;                      
                    cmd.Parameters.Add("@Price", SqlDbType.NVarChar, 20).Value = _sPrice;
                    cmd.Parameters.Add("@PriceUp", SqlDbType.NVarChar, 20).Value = _sPriceUp;
                    cmd.Parameters.Add("@PriceDown", SqlDbType.NVarChar, 20).Value = _sPriceDown;
                    cmd.Parameters.Add("@Quantity", SqlDbType.NVarChar, 20).Value = _sQuantity;
                    cmd.Parameters.Add("@Amount", SqlDbType.NVarChar, 20).Value = _sAmount;
                    cmd.Parameters.Add("@Amount_NA", SqlDbType.NVarChar, 20).Value = _sAmount_NA;
                    cmd.Parameters.Add("@Weight", SqlDbType.NVarChar, 20).Value = _sWeight;
                    cmd.Parameters.Add("@AttachFiles", SqlDbType.Int).Value = _iAttachFiles;
                    cmd.Parameters.Add("@LineStatus", SqlDbType.Int).Value = _iLineStatus;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 1000).Value = _sNotes;
                    cmd.Parameters.Add("@URL_IR", SqlDbType.NVarChar, 100).Value = _sURL_IR;
                    cmd.Parameters.Add("@SummaryLink", SqlDbType.NVarChar, 1000).Value = _sSummaryLink;

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
                using (SqlCommand cmd = new SqlCommand("EditInvestIdees_Products", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@II_ID", SqlDbType.Int).Value = _iII_ID;
                    cmd.Parameters.Add("@ShareCodes_ID", SqlDbType.Int).Value = _iShareCodes_ID;
                    cmd.Parameters.Add("@Product_ID", SqlDbType.Int).Value = _iProduct_ID;
                    cmd.Parameters.Add("@ProductCategories_ID", SqlDbType.Int).Value = _iProductCategories_ID;
                    cmd.Parameters.Add("@Curr", SqlDbType.NVarChar, 6).Value = _sCurrency;
                    cmd.Parameters.Add("@StockExchange_ID", SqlDbType.Int).Value = _iStockExchange_ID;
                    cmd.Parameters.Add("@Energia", SqlDbType.Int).Value = _iEnergia;
                    cmd.Parameters.Add("@Aktion", SqlDbType.Int).Value = _iAktion;
                    cmd.Parameters.Add("@Constant", SqlDbType.Int).Value = _iConstant;
                    cmd.Parameters.Add("@ConstantDate", SqlDbType.NVarChar, 20).Value = _sConstantDate;
                    cmd.Parameters.Add("@Type", SqlDbType.Int).Value = _iType;
                    cmd.Parameters.Add("@Price", SqlDbType.NVarChar, 20).Value = _sPrice;
                    cmd.Parameters.Add("@PriceUp", SqlDbType.NVarChar, 20).Value = _sPriceUp;
                    cmd.Parameters.Add("@PriceDown", SqlDbType.NVarChar, 20).Value = _sPriceDown;
                    cmd.Parameters.Add("@Quantity", SqlDbType.NVarChar, 20).Value = _sQuantity;
                    cmd.Parameters.Add("@Amount", SqlDbType.NVarChar, 20).Value = _sAmount;
                    cmd.Parameters.Add("@Amount_NA", SqlDbType.NVarChar, 20).Value = _sAmount_NA;
                    cmd.Parameters.Add("@Weight", SqlDbType.NVarChar, 20).Value = _sWeight;
                    cmd.Parameters.Add("@AttachFiles", SqlDbType.Int).Value = _iAttachFiles;
                    cmd.Parameters.Add("@LineStatus", SqlDbType.Int).Value = _iLineStatus;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 1000).Value = _sNotes;
                    cmd.Parameters.Add("@URL_IR", SqlDbType.NVarChar, 100).Value = _sURL_IR;
                    cmd.Parameters.Add("@SummaryLink", SqlDbType.NVarChar, 1000).Value = _sSummaryLink;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void EditEnergia()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_EditInvestIdees_Commands_Recieve", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@LineStatus", SqlDbType.Int).Value = _iLineStatus;
                    cmd.Parameters.Add("@RecieveDate", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@Energia", SqlDbType.Int).Value = _iEnergia;
                    cmd.Parameters.Add("@Weight", SqlDbType.Int).Value = _sWeight;
                    cmd.Parameters.Add("@Notes", SqlDbType.Int).Value = _sNotes;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }

        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int II_ID { get { return this._iII_ID; } set { this._iII_ID = value; } }
        public int ShareCodes_ID { get { return this._iShareCodes_ID; } set { this._iShareCodes_ID = value; } }
        public int Product_ID { get { return this._iProduct_ID; } set { this._iProduct_ID = value; } }
        public int ProductCategories_ID { get { return this._iProductCategories_ID; } set { this._iProductCategories_ID = value; } }
        public string Currency { get { return this._sCurrency; } set { this._sCurrency = value; } }
        public int StockExchange_ID { get { return this._iStockExchange_ID; } set { this._iStockExchange_ID = value; } }
        public int Energia { get { return this._iEnergia; } set { this._iEnergia = value; } }
        public int Aktion { get { return this._iAktion; } set { this._iAktion = value; } }
        public int Constant { get { return this._iConstant; } set { this._iConstant = value; } }
        public string ConstantDate { get { return this._sConstantDate; } set { this._sConstantDate = value; } }
        public int Type { get { return this._iType; } set { this._iType = value; } }
        public string Price { get { return this._sPrice; } set { this._sPrice = value; } }
        public string PriceUp { get { return this._sPriceUp; } set { this._sPriceUp = value; } }
        public string PriceDown { get { return this._sPriceDown; } set { this._sPriceDown = value; } }
        public string Quantity { get { return this._sQuantity; } set { this._sQuantity = value; } }
        public string Amount { get { return this._sAmount; } set { this._sAmount = value; } }
        public string Amount_NA { get { return this._sAmount_NA; } set { this._sAmount_NA = value; } }
        public string Weight { get { return this._sWeight; } set { this._sWeight = value; } }
        public int AttachFiles { get { return this._iAttachFiles; } set { this._iAttachFiles = value; } }
        public int LineStatus { get { return this._iLineStatus; } set { this._iLineStatus = value; } }
        public string Notes { get { return this._sNotes; } set { this._sNotes = value; } }
        public string URL_IR { get { return this._sURL_IR; } set { this._sURL_IR = value; } }
        public string SummaryLink { get { return this._sSummaryLink; } set { this._sSummaryLink = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}






