using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsInvestIdees_Commands
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;

        private int _iRecord_ID;
        private DateTime _dDateIns;
        private int _iII_ID;
        private int _iContract_ID;
        private int _iContract_Details_ID;
        private int _iContract_Packages_ID;
        private int _iClient_ID;     
        private string _sCode;
        private string _sPortfolio;
        private int _iAktion;
        private int _iShare_ID;
        private int _iProduct_ID;
        private int _iProductCategory_ID;
        private string _sQuantity;
        private string _sAmount;
        private int _iPriceType;
        private string _sPrice;
        private string _sPriceUp;
        private string _sPriceDown;
        private string _sCurr;
        private int _iConstant;
        private string _sConstantDate;
        private int _iStockCompany_ID;
        private int _iStockExchange_ID;
        private int _iConfirmationStatus;
        private DateTime _dConfirmationDate;
        private int _iCommand_ID;
        private DateTime _dRecieveDate;
        private int _iRecieveMethod_ID;
        private string _sRecieveVoicePath;
        private int _iStatus;
        private string _sRTO_Notes;

        private DataTable _dtList;

        public clsInvestIdees_Commands()
        {
            this._iRecord_ID = 0;                  
            this._dDateIns = Convert.ToDateTime("1900/01/01");
            this._iII_ID = 0;
            this._iContract_ID = 0;
            this._iContract_Details_ID = 0;
            this._iContract_Packages_ID = 0;
            this._iClient_ID = 0;
            this._sCode = "";
            this._sPortfolio = "";
            this._iAktion = 0;
            this._iShare_ID = 0;
            this._iProduct_ID = 0;
            this._iProductCategory_ID = 0;
            this._sQuantity = "";
            this._sAmount = "";
            this._iPriceType = 0;
            this._sPrice = "";
            this._sPriceUp = "";
            this._sPriceDown = "";
            this._sCurr = "";
            this._iConstant = 0;
            this._sConstantDate = "";
            this._iStockCompany_ID = 0; 
            this._iStockExchange_ID = 0;
            this._iConfirmationStatus = 0;
            this._dConfirmationDate = Convert.ToDateTime("1900/01/01");
            this._iCommand_ID = 0;
            this._dRecieveDate = Convert.ToDateTime("1900/01/01");
            this._iRecieveMethod_ID = 0;
            this._sRecieveVoicePath = "";
            this._iStatus = 0;
            this._sRTO_Notes = "";
        }
        public void GetRecord()
        {
            try {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "InvestIdees_Commands"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", _iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._dDateIns = Convert.ToDateTime(drList["DateIns"]);
                    this._iII_ID = Convert.ToInt32(drList["II_ID"]);
                    this._iContract_ID = Convert.ToInt32(drList["ClientPackage_ID"]);
                    this._iContract_Details_ID = Convert.ToInt32(drList["Contract_Details_ID"]);
                    this._iContract_Packages_ID = Convert.ToInt32(drList["Contract_Packages_ID"]);
                    this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                    this._sCode = drList["Code"] + "";
                    this._sPortfolio = drList["ProfitCenter"] + "";
                    this._iAktion = Convert.ToInt32(drList["Aktion"]);
                    this._iShare_ID = Convert.ToInt32(drList["Share_ID"]);
                    this._iProduct_ID = Convert.ToInt32(drList["Product_ID"]);
                    this._iProductCategory_ID = Convert.ToInt32(drList["ProductCategory_ID"]);
                    this._sQuantity = drList["Quantity"] + "";
                    this._sAmount = drList["Amount"] + "";
                    this._iPriceType = Convert.ToInt32(drList["PriceType"]);
                    this._sPrice = drList["Price"] + "";
                    this._sPriceUp = drList["PriceUp"] + "";
                    this._sPriceDown = drList["PriceDown"] + "";
                    this._sCurr = drList["Curr"] + "";
                    this._iConstant = Convert.ToInt32(drList["Constant"]);
                    this._sConstantDate = drList["ConstantDate"] + "";
                    this._iStockCompany_ID = Convert.ToInt32(drList["StockCompany_ID"]);
                    this._iStockExchange_ID = Convert.ToInt32(drList["StockExchange_ID"]);
                    this._iConfirmationStatus = Convert.ToInt32(drList["ConfirmationStatus"]);
                    this._dConfirmationDate = Convert.ToDateTime(drList["ConfirmationDate"]);
                    this._iCommand_ID = Convert.ToInt32(drList["Command_ID"]);
                    this._dRecieveDate = Convert.ToDateTime(drList["RecieveDate"]);
                    this._iRecieveMethod_ID = Convert.ToInt32(drList["RecieveMethod_ID"]);
                    this._sRecieveVoicePath = drList["RecieveVoicePath"] + "";
                    this._iStatus = Convert.ToInt32(drList["Status"]);
                    this._sRTO_Notes = drList["RTO_Notes"] + "";
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
                using (SqlCommand cmd = new SqlCommand("InsertInvestIdees_Commands", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@II_ID", SqlDbType.Int).Value = _iII_ID;
                    cmd.Parameters.Add("@ClientPackage_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Details_ID", SqlDbType.Int).Value = _iContract_Details_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 30).Value = _sCode;
                    cmd.Parameters.Add("@ProfitCenter", SqlDbType.NVarChar, 50).Value = _sPortfolio;
                    cmd.Parameters.Add("@Aktion", SqlDbType.Int).Value = _iAktion;
                    cmd.Parameters.Add("@Share_ID", SqlDbType.Int).Value = _iShare_ID;
                    cmd.Parameters.Add("@Product_ID", SqlDbType.Int).Value = _iProduct_ID;
                    cmd.Parameters.Add("@ProductCategory_ID", SqlDbType.Int).Value = _iProductCategory_ID;
                    cmd.Parameters.Add("@Quantity", SqlDbType.NVarChar, 20).Value = _sQuantity;
                    cmd.Parameters.Add("@Amount", SqlDbType.NVarChar, 20).Value = _sAmount;
                    cmd.Parameters.Add("@PriceType", SqlDbType.Int).Value = _iPriceType;
                    cmd.Parameters.Add("@Price", SqlDbType.NVarChar, 20).Value = _sPrice;
                    cmd.Parameters.Add("@PriceUp", SqlDbType.NVarChar, 20).Value = _sPriceUp;
                    cmd.Parameters.Add("@PriceDown", SqlDbType.NVarChar, 20).Value = _sPriceDown;
                    cmd.Parameters.Add("@Curr", SqlDbType.NVarChar, 6).Value = _sCurr;
                    cmd.Parameters.Add("@Constant", SqlDbType.Int).Value = _iConstant;
                    cmd.Parameters.Add("@ConstantDate", SqlDbType.NVarChar, 25).Value = _sConstantDate;
                    cmd.Parameters.Add("@StockCompany_ID", SqlDbType.Int).Value = _iStockCompany_ID;
                    cmd.Parameters.Add("@StockExchange_ID", SqlDbType.Int).Value = _iStockExchange_ID;
                    cmd.Parameters.Add("@Command_ID", SqlDbType.Int).Value = _iCommand_ID;
                    cmd.Parameters.Add("@RecieveDate", SqlDbType.DateTime).Value = _dRecieveDate;
                    cmd.Parameters.Add("@ConfirmationStatus", SqlDbType.Int).Value = _iConfirmationStatus;
                    cmd.Parameters.Add("@ConfirmationDate", SqlDbType.DateTime).Value = _dConfirmationDate;
                    cmd.Parameters.Add("@RecieveMethod_ID", SqlDbType.Int).Value = _iRecieveMethod_ID;
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
                using (SqlCommand cmd = new SqlCommand("EditInvestIdees_Commands", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@II_ID", SqlDbType.Int).Value = _iII_ID;
                    cmd.Parameters.Add("@ClientPackage_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Details_ID", SqlDbType.Int).Value = _iContract_Details_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@Code", SqlDbType.NVarChar, 30).Value = _sCode;
                    cmd.Parameters.Add("@ProfitCenter", SqlDbType.NVarChar, 50).Value = _sPortfolio;
                    cmd.Parameters.Add("@Aktion", SqlDbType.Int).Value = _iAktion;
                    cmd.Parameters.Add("@Share_ID", SqlDbType.Int).Value = _iShare_ID;
                    cmd.Parameters.Add("@Product_ID", SqlDbType.Int).Value = _iProduct_ID;
                    cmd.Parameters.Add("@ProductCategory_ID", SqlDbType.Int).Value = _iProductCategory_ID;
                    cmd.Parameters.Add("@Quantity", SqlDbType.NVarChar, 20).Value = _sQuantity;
                    cmd.Parameters.Add("@Amount", SqlDbType.NVarChar, 20).Value = _sAmount;
                    cmd.Parameters.Add("@PriceType", SqlDbType.Int).Value = _iPriceType;
                    cmd.Parameters.Add("@Price", SqlDbType.NVarChar, 20).Value = _sPrice;
                    cmd.Parameters.Add("@PriceUp", SqlDbType.NVarChar, 20).Value = _sPriceUp;
                    cmd.Parameters.Add("@PriceDown", SqlDbType.NVarChar, 20).Value = _sPriceDown;
                    cmd.Parameters.Add("@Curr", SqlDbType.NVarChar, 6).Value = _sCurr;
                    cmd.Parameters.Add("@Constant", SqlDbType.Int).Value = _iConstant;
                    cmd.Parameters.Add("@ConstantDate", SqlDbType.NVarChar, 25).Value = _sConstantDate;
                    cmd.Parameters.Add("@StockCompany_ID", SqlDbType.Int).Value = _iStockCompany_ID;
                    cmd.Parameters.Add("@StockExchange_ID", SqlDbType.Int).Value = _iStockExchange_ID;
                    cmd.Parameters.Add("@Command_ID", SqlDbType.Int).Value = _iCommand_ID;
                    cmd.Parameters.Add("@RecieveDate", SqlDbType.DateTime).Value = _dRecieveDate;
                    cmd.Parameters.Add("@ConfirmationStatus", SqlDbType.Int).Value = _iConfirmationStatus;
                    cmd.Parameters.Add("@ConfirmationDate", SqlDbType.DateTime).Value = _dConfirmationDate;
                    cmd.Parameters.Add("@RecieveMethod_ID", SqlDbType.Int).Value = _iRecieveMethod_ID;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void EditStatus()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_EditInvestIdees_Commands_Recieve", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Command_ID", SqlDbType.Int).Value = _iCommand_ID;
                    cmd.Parameters.Add("@RecieveDate", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.Parameters.Add("@RTO_Notes", SqlDbType.NVarChar, 1000).Value = _sRTO_Notes;
                    cmd.Parameters.Add("@RecieveVoicePath", SqlDbType.NVarChar, 500).Value = _sRecieveVoicePath;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
    
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public DateTime DateIns { get { return this._dDateIns; } set { this._dDateIns = value; } }
        public int II_ID { get { return this._iII_ID; } set { this._iII_ID = value; } }
        public int Contract_ID { get { return this._iContract_ID; } set { this._iContract_ID = value; } }
        public int Contract_Details_ID { get { return this._iContract_Details_ID; } set { this._iContract_Details_ID = value; } }
        public int Contract_Packages_ID { get { return this._iContract_Packages_ID; } set { this._iContract_Packages_ID = value; } }
        public int Client_ID { get { return this._iClient_ID; } set { this._iClient_ID = value; } }
        public string Code { get { return this._sCode; } set { this._sCode = value; } }
        public string Portfolio { get { return this._sPortfolio; } set { this._sPortfolio = value; } }
        public int Aktion { get { return this._iAktion; } set { this._iAktion = value; } }
        public int Share_ID { get { return this._iShare_ID; } set { this._iShare_ID = value; } }
        public int Product_ID { get { return this._iProduct_ID; } set { this._iProduct_ID = value; } }
        public int ProductCategory_ID { get { return this._iProductCategory_ID; } set { this._iProductCategory_ID = value; } }
        public string Quantity { get { return this._sQuantity; } set { this._sQuantity = value; } }
        public string Amount { get { return this._sAmount; } set { this._sAmount = value; } }
        public int PriceType { get { return this._iPriceType; } set { this._iPriceType = value; } }
        public string Price { get { return this._sPrice; } set { this._sPrice = value; } }
        public string PriceUp { get { return this._sPriceUp; } set { this._sPriceUp = value; } }
        public string PriceDown { get { return this._sPriceDown; } set { this._sPriceDown = value; } }
        public string Curr { get { return this._sCurr; } set { this._sCurr = value; } }
        public int Constant { get { return this._iConstant; } set { this._iConstant = value; } }
        public string ConstantDate { get { return this._sConstantDate; } set { this._sConstantDate = value; } }
        public int StockCompany_ID { get { return this._iStockCompany_ID; } set { this._iStockCompany_ID = value; } }
        public int StockExchange_ID { get { return this._iStockExchange_ID; } set { this._iStockExchange_ID = value; } }
        public int ConfirmationStatus { get { return this._iConfirmationStatus; } set { this._iConfirmationStatus = value; } }
        public DateTime ConfirmationDate { get { return this._dConfirmationDate; } set { this._dConfirmationDate = value; } }
        public int Command_ID { get { return this._iCommand_ID; } set { this._iCommand_ID = value; } }
        public DateTime RecieveDate { get { return this._dRecieveDate; } set { this._dRecieveDate = value; } }
        public int RecieveMethod_ID { get { return this._iRecieveMethod_ID; } set { this._iRecieveMethod_ID = value; } }
        public string RecieveVoicePath { get { return this._sRecieveVoicePath; } set { this._sRecieveVoicePath = value; } }   
        public int Status { get { return this._iStatus; } set { this._iStatus = value; } }
        public string RTO_Notes { get { return this._sRTO_Notes; } set { this._sRTO_Notes = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}






