using System;                           //OK
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsSettlements
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int       _iRecord_ID;
        private DateTime  _dDateFrom;
        private DateTime  _dDateTo;
        private DateTime  _dDateExecFrom;
        private DateTime  _dDateExecTo;
        private DateTime  _dDateInsFrom;
        private DateTime  _dDateInsTo;
        private int       _iStockCompany_ID;
        private int       _iUser1_ID;
        private int       _iUser4_ID;
        private int       _iDivision_ID;
        private string    _sClientCode;
        private DataTable _dtList; 

        public clsSettlements()
        {
            this._iRecord_ID = 0;
            this._dDateFrom = Convert.ToDateTime("1900/01/01");
            this._dDateTo = Convert.ToDateTime("1900/01/01");
            this._dDateExecFrom = Convert.ToDateTime("1900/01/01");
            this._dDateExecTo = Convert.ToDateTime("1900/01/01");
            this._dDateInsFrom = Convert.ToDateTime("1900/01/01");
            this._dDateInsTo = Convert.ToDateTime("1900/01/01");
            this._iStockCompany_ID = 0;
            this._iUser1_ID = 0;
            this._iUser4_ID = 0;
            this._iDivision_ID = 0;
            this._sClientCode = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Contracts_Details_Packages"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", "ID"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable("Setlements_List");
            dtCol = _dtList.Columns.Add("ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ImageType", Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("BusinessType_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Code", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Portfolio", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("StockCompany_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("StockCompanyTitle", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Service_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ServiceTitle", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ContractTipos", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Contract_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Contracts_Details_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Contracts_Packages_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ContractTitle", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("MIFID_2", Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("Contract_Address", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Contract_City", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Contract_ZIP", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ProfileTitle", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ClientTipos", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Client_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ClientName", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Surname", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Firstname", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("FirstnameMother", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Address", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("City", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ZIP", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("CountryTitleEn", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("CountryTitleGr", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("AFM", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("DOY", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ISIN", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Product_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ShareTitle", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ProductTitle", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Aktion", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ExecuteDate", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("SettlementDate", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("RealQuantity", Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("Curr", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("CurrRate", Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("RealPrice", Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("RealAmount", Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("CashAccount", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("RTO_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("RTO_FeesPercent", Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("RTO_FeesAmount", Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("RTO_FeesDiscountPercent", Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("RTO_FinishFeesPercent", Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("RTO_FinishFeesAmount", Type.GetType("System.Single"));            
            dtCol = _dtList.Columns.Add("RTO_FeesAmountEUR", Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("RTO_MinFeesAmount", Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("RTO_MinFeesDiscountPercent", Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("RTO_MinFeesDiscountAmount", Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("RTO_FinishMinFeesAmount", Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("RTO_TicketFeeCurr", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("RTO_TicketFee", Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("RTO_TicketFeeDiscountAmount", Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("RTO_FinishTicketFee", Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("RTO_FeesProVAT", Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("RTO_FeesVAT", Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("RTO_CompanyFees", Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("InvoiceType", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Invoice_Titles_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Invoice_Num", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Invoice_DateIns", Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("FileName", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("AdvisorName", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("RMName", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Contract_ConnectionMethod", Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("Contract_EMail", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("OfficialInformingDate", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Notes", Type.GetType("System.String"));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetSettlements_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateExecFrom", Convert.ToDateTime(_dDateExecFrom)));
                cmd.Parameters.Add(new SqlParameter("@DateExecTo", Convert.ToDateTime(_dDateExecTo)));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", Convert.ToDateTime(_dDateFrom)));
                cmd.Parameters.Add(new SqlParameter("@DateTo", Convert.ToDateTime(_dDateTo)));
                cmd.Parameters.Add(new SqlParameter("@StockCompany_ID", _iStockCompany_ID));
                cmd.Parameters.Add(new SqlParameter("@User1_ID", _iUser1_ID));
                cmd.Parameters.Add(new SqlParameter("@User4_ID", _iUser4_ID));
                cmd.Parameters.Add(new SqlParameter("@Division_ID", _iDivision_ID));
                cmd.Parameters.Add(new SqlParameter("@ClientCode", _sClientCode));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = this._dtList.NewRow();                       
                    this.dtRow["ID"] = drList["ID"];
                    if (drList["InvoiceFileName"].ToString() != "") 
                    {
                        this.dtRow["ImageType"] = 1;
                        this.dtRow["FileName"] = drList["InvoiceFileName"] + "";
                    }
                    else
                    {
                        this.dtRow["ImageType"] = 0;
                        this.dtRow["FileName"] = "";
                    }
                    this.dtRow["BusinessType_ID"] = drList["BusinessType_ID"];
                    this.dtRow["Code"] = drList["Code"] + "";
                    this.dtRow["Portfolio"] = drList["Portfolio"] + "";
                    this.dtRow["StockCompany_ID"] = drList["StockCompany_ID"];
                    this.dtRow["StockCompanyTitle"] = drList["StockCompanyTitle"] + "";
                    this.dtRow["Service_ID"] = drList["Service_ID"];
                    this.dtRow["ServiceTitle"] = drList["ServiceTitle"] + "";
                    this.dtRow["ContractTipos"] = drList["ContractTipos"];
                    this.dtRow["Contract_ID"] = drList["ClientPackage_ID"];
                    this.dtRow["Contracts_Details_ID"] = drList["Contracts_Details_ID"];
                    this.dtRow["Contracts_Packages_ID"] = drList["Contracts_Packages_ID"];
                    this.dtRow["ContractTitle"] = drList["ContractTitle"] + "";
                    this.dtRow["MIFID_2"] = drList["MIFID_2"] + "";
                    this.dtRow["Contract_Address"] = drList["Contract_Address"] + "";
                    this.dtRow["Contract_City"] = drList["Contract_City"] + "";
                    this.dtRow["Contract_Zip"] = drList["Contract_Zip"] + "";
                    this.dtRow["ProfileTitle"] = drList["ProfileTitle"] + "";
                    this.dtRow["ClientTipos"] = drList["ClientTipos"];
                    this.dtRow["Client_ID"] = drList["Client_ID"];
                    this.dtRow["ClientName"] = drList["InvName"] + "";
                    this.dtRow["Surname"] = drList["Surname"] + "";
                    this.dtRow["Firstname"] = drList["Firstname"] + "";
                    this.dtRow["FirstnameMother"] = drList["FirstnameMother"] + "";
                    this.dtRow["Address"] = drList["InvAddress"] + "";
                    this.dtRow["City"] = drList["InvCity"] + "";
                    this.dtRow["Zip"] = drList["InvZip"] + "";
                    this.dtRow["CountryTitleEn"] = drList["Country_Title"] + "";
                    this.dtRow["CountryTitleGr"] = drList["Country_Title_Gr"] + "";
                    this.dtRow["AFM"] = drList["InvAFM"] + "";
                    this.dtRow["DOY"] = drList["InvDOY"] + "";
                    this.dtRow["ISIN"] = drList["ISIN"] + "";

                    this.dtRow["Product_ID"] = drList["Product_ID"];
                    this.dtRow["ShareTitle"] = drList["ShareTitle"] + "";
                    this.dtRow["ProductTitle"] = drList["ProductTitle"] + "/" + drList["ProductCategoryTitle"];
                    this.dtRow["Aktion"] = Convert.ToInt16(drList["Aktion"]) == 1 ? "BUY": "SELL"; // Interaction.IIf(Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(drList["Aktion"], 1, false)), "BUY", "SELL");
                    this.dtRow["ExecuteDate"] = drList["ExecuteDate"];
                  
                    if (Convert.ToDateTime(drList["SettlementDate"]) == Convert.ToDateTime("01/01/1900"))  this.dtRow["SettlementDate"] = "";
                    else this.dtRow["SettlementDate"] = drList["SettlementDate"];

                    this.dtRow["CashAccount"] = "";
                    if (Convert.ToInt32(drList["ClientTipos"]) == 1) this.dtRow["InvoiceType"] = 1;
                    else this.dtRow["InvoiceType"] = 2;
                    this.dtRow["RealQuantity"] = drList["RealQuantity"];
                    this.dtRow["Curr"] = drList["Curr"];
                    this.dtRow["CurrRate"] = drList["CurrRate"];
                    this.dtRow["RealPrice"] = drList["RealPrice"];
                    this.dtRow["RealAmount"] = drList["RealAmount"];
                    this.dtRow["RTO_ID"] = 0;
                    this.dtRow["RTO_FeesPercent"] = drList["RTO_FeesPercent"];
                    this.dtRow["RTO_FeesAmount"] = drList["RTO_FeesAmount"];
                    this.dtRow["RTO_FeesDiscountPercent"] = drList["RTO_FeesDiscountPercent"];
                    this.dtRow["RTO_FinishFeesPercent"] = drList["RTO_FinishFeesPercent"];
                    this.dtRow["RTO_FinishFeesAmount"] = drList["RTO_FinishFeesAmount"];
                    this.dtRow["RTO_FeesAmountEUR"] = drList["RTO_FeesAmountEUR"];
                    this.dtRow["RTO_MinFeesAmount"] = drList["RTO_MinFeesAmount"];
                    this.dtRow["RTO_MinFeesDiscountPercent"] = drList["RTO_MinFeesDiscountPercent"];
                    this.dtRow["RTO_MinFeesDiscountAmount"] = drList["RTO_MinFeesDiscountAmount"];
                    this.dtRow["RTO_FinishMinFeesAmount"] = drList["RTO_FinishMinFeesAmount"];
                    this.dtRow["RTO_FeesProVAT"] = drList["RTO_FeesProVAT"];
                    this.dtRow["RTO_FeesVAT"] = drList["RTO_FeesVAT"];
                    this.dtRow["RTO_CompanyFees"] = drList["RTO_CompanyFees"];

                    this.dtRow["Invoice_Titles_ID"] = drList["Invoice_Titles_ID"];
                    if (drList["Code"] + "" == "") this.dtRow["Invoice_Num"] = "";
                    else this.dtRow["Invoice_Num"] = drList["InvCode"] + " " + drList["InvSeira"] + " " + drList["InvNum"];
   
                    this.dtRow["Invoice_DateIns"] = drList["DateIns"];
                    this.dtRow["FileName"] = drList["InvoiceFileName"] + "";
                    this.dtRow["AdvisorName"] = drList["AdvisorSurname"] + " " + drList["AdvisorFirstname"];
                    this.dtRow["RMName"] = drList["RMSurname"] + " " + drList["RMFirstname"];
                    this.dtRow["Contract_ConnectionMethod"] = drList["Contract_ConnectionMethod"];
                    this.dtRow["Contract_EMail"] = drList["Contract_EMail"] + "";
                    this.dtRow["OfficialInformingDate"] = drList["Inv_OfficialInformingDate"] + "";
                    this.dtRow["Notes"] = drList["Notes"];
                    this._dtList.Rows.Add(dtRow);
                }
            }
            catch (Exception ex) { 
                MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetInvoicesList()
        {
            _dtList = new DataTable("Settlements_List");
            dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ImageType", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("BusinessType_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("StockCompany_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("StockCompanyTitle", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Service_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ServiceTitle", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Contract_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Contracts_Details_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Contracts_Packages_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ContractDateStart", System.Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("Contract_Address", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Contract_City", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Contract_ZIP", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ClientTipos", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Surname", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Firstname", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("FirstnameMother", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("BornPlace", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Address", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("City", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ZIP", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("CountryTitleEn", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("CountryTitleGr", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("AFM", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("DOY", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ISIN", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("ShareTitle", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ProductTitle", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Aktion", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ExecuteDate", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("SettlementDate", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("RTO_CompanyFees", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("Curr", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("RealPrice", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("RealAmount", System.Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("Invoice_Titles_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Invoice_Num", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Invoice_DateIns", System.Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("FileName", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("AdvisorName", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("RMName", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Contract_ConnectionMethod", System.Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("Contract_EMail", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("OfficialInformingDate", System.Type.GetType("System.String"));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetSettlements_InvoicesList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateInsFrom", Convert.ToDateTime(_dDateInsFrom)));
                cmd.Parameters.Add(new SqlParameter("@DateInsTo", Convert.ToDateTime(_dDateInsTo)));
                cmd.Parameters.Add(new SqlParameter("@StockCompany_ID", _iStockCompany_ID));
                cmd.Parameters.Add(new SqlParameter("@ClientCode", _sClientCode));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = this._dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];
                    if (drList["InvoiceFileName"].ToString() != "")
                    {
                        this.dtRow["ImageType"] = 1;
                        this.dtRow["FileName"] = drList["InvoiceFileName"] + "";
                    }
                    else
                    {
                        this.dtRow["ImageType"] = 0;
                        this.dtRow["FileName"] = "";
                    }
                    this.dtRow["BusinessType_ID"] = drList["BusinessType_ID"];
                    this.dtRow["Code"] = drList["Code"] + "";
                    this.dtRow["Portfolio"] = drList["Portfolio"] + "";
                    this.dtRow["StockCompany_ID"] = drList["StockCompany_ID"];
                    this.dtRow["StockCompanyTitle"] = drList["StockCompanyTitle"] + "";
                    this.dtRow["Service_ID"] = drList["Service_ID"];
                    this.dtRow["ServiceTitle"] = drList["ServiceTitle"] + "";
                    this.dtRow["Contract_ID"] = drList["ClientPackage_ID"];
                    this.dtRow["Contracts_Details_ID"] = drList["Contracts_Details_ID"];
                    this.dtRow["Contracts_Packages_ID"] = drList["Contracts_Packages_ID"];
                    this.dtRow["ContractTitle"] = drList["ContractTitle"] + "";
                    //this.dtRow["MIFID_2"] = drList["MIFID_2"];
                    this.dtRow["Contract_Address"] = drList["Contract_Address"] + "";
                    this.dtRow["Contract_City"] = drList["Contract_City"] + "";
                    this.dtRow["Contract_Zip"] = drList["Contract_Zip"] + "";
                    //this.dtRow["ProfileTitle"] = drList["ProfileTitle"] + "";
                    this.dtRow["ClientTipos"] = drList["ClientTipos"];
                    this.dtRow["Client_ID"] = drList["Client_ID"];
                    //this.dtRow["ClientName"] = drList["InvName"] + "";
                    this.dtRow["Surname"] = drList["Surname"] + "";
                    this.dtRow["Firstname"] = drList["Firstname"] + "";
                    this.dtRow["FirstnameMother"] = drList["FirstnameMother"] + "";
                    this.dtRow["BornPlace"] = drList["BornPlace"] + "";
                    //this.dtRow["Address"] = drList["InvAddress"] + "";
                    //this.dtRow["City"] = drList["InvCity"] + "";
                    //this.dtRow["Zip"] = drList["InvZip"] + "";
                    this.dtRow["CountryTitleEn"] = drList["Country_Title"] + "";
                    this.dtRow["CountryTitleGr"] = drList["Country_Title_Gr"] + "";

                    this.dtRow["RTO_CompanyFees"] = drList["RTO_CompanyFees"];
                    this.dtRow["Curr"] = drList["Curr"] + "";
                    this.dtRow["RealPrice"] = drList["RealPrice"];
                    this.dtRow["RealAmount"] = drList["RealAmount"];

                    this.dtRow["Invoice_Titles_ID"] = drList["Invoice_Titles_ID"];
                    if (drList["Code"] + "" == "")  this.dtRow["Invoice_Num"] = "";
                    else  this.dtRow["Invoice_Num"] = drList["InvCode"] + " " + drList["InvSeira"] + " " + drList["InvNum"];

                    this.dtRow["Invoice_DateIns"] = drList["DateIns"];
                    this.dtRow["FileName"] = drList["InvoiceFileName"] + "";
                    //this.dtRow["AdvisorName"] = drList["AdvisorSurname"] + " " + drList["AdvisorFirstname"];
                    //this.dtRow["RMName"] = drList["RMSurname"] + " " + drList["RMFirstname"];
                    this.dtRow["Contract_ConnectionMethod"] = drList["Contract_ConnectionMethod"];
                    this.dtRow["Contract_EMail"] = drList["Contract_EMail"] + "";
                    this.dtRow["OfficialInformingDate"] = drList["Inv_OfficialInformingDate"] + "";
                    //this.dtRow["Notes"] = drList["Notes"];
                    this._dtList.Rows.Add(dtRow);
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
                using (SqlCommand cmd = new SqlCommand("InsertContract_Details_Packages", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = this._dFrom;
                    //cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = this._dTo;
                    //cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = this._iContract_ID;
                    //cmd.Parameters.Add("@Contracts_Details_ID", SqlDbType.Int).Value = this._iContracts_Details_ID;
                    //cmd.Parameters.Add("@Contracts_Packages_ID", SqlDbType.Int).Value = this._iContracts_Packages_ID;
                    //cmd.ExecuteNonQuery();
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
                using (SqlCommand cmd = new SqlCommand("EditContract_Details_Packages", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = this._iRecord_ID;
                    //cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = this._dFrom;
                    //cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = this._dTo;
                    //cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = this._iContract_ID;
                    //cmd.Parameters.Add("@Contracts_Details_ID", SqlDbType.Int).Value = this._iContracts_Details_ID;
                    //cmd.Parameters.Add("@Contracts_Packages_ID", SqlDbType.Int).Value = this._iContracts_Packages_ID;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Settlements";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public DateTime DateFrom { get { return this._dDateFrom; } set { this._dDateFrom = value; } }
        public DateTime DateTo { get { return this._dDateTo; } set { this._dDateTo = value; } }
        public DateTime DateExecFrom { get { return this._dDateExecFrom; } set { this._dDateExecFrom = value; } }
        public DateTime DateExecTo { get { return this._dDateExecTo; } set { this._dDateExecTo = value; } }
        public DateTime DateInsFrom { get { return this._dDateInsFrom; } set { this._dDateInsFrom = value; } }
        public DateTime DateInsTo { get { return this._dDateInsTo; } set { this._dDateInsTo = value; } }
        public int StockCompany_ID { get { return this._iStockCompany_ID; } set { this._iStockCompany_ID = value; } }
        public int User1_ID { get { return this._iUser1_ID; } set { this._iUser1_ID = value; } }
        public int User4_ID { get { return this._iUser4_ID; } set { this._iUser4_ID = value; } }
        public int Division_ID { get { return this._iDivision_ID; } set { this._iDivision_ID = value; } }
        public string ClientCode { get { return this._sClientCode; } set { this._sClientCode = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
