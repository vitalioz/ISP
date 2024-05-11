using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsClientsDealAdvisoryFees
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int      _iRecord_ID;
        private int      _iContract_ID;
        private int      _iContract_Packages_ID;
        private int      _iSPDAF_ID;
        private float    _sgAmountFrom;
        private float    _sgAmountTo;
        private DateTime _dFrom;
        private DateTime _dTo;
        private decimal  _decDealAdvisoryFees_Discount;
        private decimal  _decFinishDealAdvisoryFees;
        private float    _sgMinimumFees_Discount;
        private float    _sgMinimumFees;
        private string   _sAllManFees;

        private decimal  _decAUM;
        private int      _iDays;
        private float    _sgDiscount_Percent;
        private float    _sgDiscount_Amount;
        private decimal  _decFeesPercent;
        private float    _sgStartAmount;
        private int      _iOption_ID;
        private int      _iServiceProvider_ID;
        private int      _iInvestmentProfile_ID;
        private int      _iInvestmentPolicy_ID;
        private DataTable _dtList;

        public clsClientsDealAdvisoryFees()
        {
            this._iRecord_ID = 0;
            this._iContract_ID = 0;
            this._iContract_Packages_ID = 0;
            this._iSPDAF_ID = 0;
            this._sgAmountFrom = 0;
            this._sgAmountTo = 0;
            this._dFrom = Convert.ToDateTime("1900/01/01");
            this._dTo = Convert.ToDateTime("2070/12/31");
            this._decDealAdvisoryFees_Discount = 0;
            this._decFinishDealAdvisoryFees = 0;
            this._sgMinimumFees_Discount = 0;
            this._sgMinimumFees = 0;
            this._sAllManFees = "";

            this._decAUM = 0;
            this._iDays = 0;
            this._sgDiscount_Percent = 0;
            this._sgDiscount_Amount = 0;
            this._decFeesPercent = 0;
            this._sgStartAmount = 0;
            this._iOption_ID = 0;
            this._iServiceProvider_ID = 0;
            this._iInvestmentProfile_ID = 0;
            this._iInvestmentPolicy_ID = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "ClientsDealAdvisoryFees"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iContract_ID = Convert.ToInt32(drList["Contract_ID"]);
                    this._iContract_Packages_ID = Convert.ToInt32(drList["Contract_Packages_ID"]);
                    this._iSPDAF_ID = Convert.ToInt32(drList["SPDAF_ID"]); ;
                    this._sgAmountFrom = Convert.ToSingle(drList["AmountFrom"]);
                    this._sgAmountTo = Convert.ToSingle(drList["AmountTo"]);
                    this._dFrom = Convert.ToDateTime(drList["DateFrom"]);
                    this._dTo = Convert.ToDateTime(drList["DateTo"]);
                    this._decDealAdvisoryFees_Discount = Convert.ToDecimal(drList["DealAdvisoryFees_Discount"]);
                    this._decFinishDealAdvisoryFees = Convert.ToDecimal(drList["DealAdvisoryFees"]);
                    this._sgMinimumFees_Discount = Convert.ToSingle(drList["MinimumFees_Discount"]);
                    this._sgMinimumFees = Convert.ToSingle(drList["MinimumFees"]);
                    this._sAllManFees = drList["AllManFees"] + "";
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
                _dtList = new DataTable("DealAdvisoryFees_List");
                dtCol = _dtList.Columns.Add("ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ServiceProvider_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AmountFrom", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AmountTo", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("DealAdvisoryFees", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("DiscountDateFrom", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("DiscountDateTo", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("DealAdvisoryFees_Discount", Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("DealAdvisoryFinish", Type.GetType("System.Decimal"));
                dtCol = _dtList.Columns.Add("YR_Return", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("YR_DateFrom", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("YR_DateTo", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("YR_DiscountPercent", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("YR_Return_Finish", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Variable1", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Variable2", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Variable2_Finish", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MonthMinAmount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MonthMinCurr", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("OpenAmount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("OpenCurr", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ServiceAmount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ServiceCurr", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("MinAmount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MinCurr", Type.GetType("System.String"));

                conn.Open();
                cmd = new SqlCommand("GetPackage_DealAdvisoryFees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ServiceProvider_ID", _iServiceProvider_ID));
                cmd.Parameters.Add(new SqlParameter("@Option_ID", _iOption_ID));
                cmd.Parameters.Add(new SqlParameter("@InvestmentPolicy_ID", _iInvestmentPolicy_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];                                                      // ID - is SPDAF_ID
                    this.dtRow["ServiceProvider_ID"] = drList["ServiceProvider_ID"];
                    this.dtRow["AmountFrom"] = drList["AmountFrom"];
                    this.dtRow["AmountTo"] = drList["AmountTo"];
                    this.dtRow["DealAdvisoryFees"] = drList["FeesPercent"];
                    this.dtRow["DiscountDateFrom"] = _dFrom.ToString("dd/MM/yyyy");
                    this.dtRow["DiscountDateTo"] = _dTo.ToString("dd/MM/yyyy");
                    this.dtRow["DealAdvisoryFees_Discount"] = 0;
                    this.dtRow["DealAdvisoryFinish"] = drList["FeesPercent"];
                    this.dtRow["YR_Return"] = drList["YperReturn"];
                    this.dtRow["YR_DateFrom"] = _dFrom.ToString("dd/MM/yyyy");
                    this.dtRow["YR_DateTo"] = _dTo.ToString("dd/MM/yyyy");
                    this.dtRow["YR_DiscountPercent"] = 0;
                    this.dtRow["YR_Return_Finish"] = drList["YperReturn"];
                    this.dtRow["Variable1"] = drList["Variable1"];
                    this.dtRow["Variable2"] = drList["Variable2"];
                    this.dtRow["Variable2_Finish"] = drList["Variable2"];
                    this.dtRow["MonthMinAmount"] = drList["MonthMinAmount"];
                    this.dtRow["MonthMinCurr"] = drList["MonthMinCurr"];
                    this.dtRow["OpenAmount"] = drList["OpenAmount"];
                    this.dtRow["OpenCurr"] = drList["OpenCurr"];
                    this.dtRow["ServiceAmount"] = drList["ServiceAmount"];
                    this.dtRow["ServiceCurr"] = drList["ServiceCurr"];
                    this.dtRow["MinAmount"] = drList["MinAmount"];
                    this.dtRow["MinCurr"] = drList["MinCurr"];
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
                _dtList = new DataTable("DealAdvisoryFees_List");
                dtCol = _dtList.Columns.Add("ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ServiceProvider_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_Packages_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AmountFrom", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AmountTo", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("DealAdvisoryFees", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("DiscountDateFrom", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("DiscountDateTo", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("DealAdvisoryFees_Discount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishDealAdvisoryFees", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MonthMinAmount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MonthMinCurr", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("MinimumFees_Discount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MinimumFees", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AllManFees", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SPDAF_ID", Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetDealAdvisoryFees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Contract_Packages_ID", _iContract_Packages_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    if (!String.IsNullOrEmpty(drList["ID"].ToString()))                                     
                    {
                        if ((Convert.ToDateTime(drList["DateFrom"]) <= _dTo) && (Convert.ToDateTime(drList["DateTo"]) >= _dFrom))
                        {
                            dtRow = _dtList.NewRow();
                            this.dtRow["ServiceProvider_ID"] = drList["PackageProvider_ID"];
                            this.dtRow["AmountFrom"] = drList["AmountFrom"];
                            this.dtRow["AmountTo"] = drList["AmountTo"];
                            this.dtRow["DealAdvisoryFees"] = drList["FeesAmount"];
                            this.dtRow["ID"] = drList["ID"];
                            this.dtRow["Contract_ID"] = drList["Contract_ID"];
                            this.dtRow["Contract_Packages_ID"] = drList["Contract_Packages_ID"];
                            this.dtRow["DiscountDateFrom"] = drList["DateFrom"];
                            this.dtRow["DiscountDateTo"] = drList["DateTo"];
                            this.dtRow["DealAdvisoryFees_Discount"] = drList["DealAdvisoryFees_Discount"];
                            this.dtRow["FinishDealAdvisoryFees"] = drList["DealAdvisoryFees"];
                            this.dtRow["MonthMinAmount"] = drList["MonthMinAmount"];
                            this.dtRow["MonthMinCurr"] = drList["MonthMinCurr"];
                            this.dtRow["MinimumFees_Discount"] = drList["MinimumFees_Discount"];
                            this.dtRow["MinimumFees"] = drList["MinimumFees"];
                            this.dtRow["AllManFees"] = drList["AllManFees"] + "";
                            this.dtRow["SPDAF_ID"] = drList["SPDAF_ID"];
                            _dtList.Rows.Add(dtRow);
                        }
                        else
                        {
                            /*
                            dtRow = _dtList.NewRow();
                            this.dtRow["ServiceProvider_ID"] = drList["PackageProvider_ID"];
                            this.dtRow["AmountFrom"] = drList["AmountFrom"];
                            this.dtRow["AmountTo"] = drList["AmountTo"];
                            this.dtRow["DealAdvisoryFees"] = drList["FeesAmount"];
                            this.dtRow["ID"] = drList["ID"];
                            this.dtRow["Contract_ID"] = drList["Contract_ID"];
                            this.dtRow["Contract_Packages_ID"] = drList["Contract_Packages_ID"];
                            this.dtRow["DiscountDateFrom"] = _dFrom;
                            this.dtRow["DiscountDateTo"] = _dTo;
                            this.dtRow["DealAdvisoryFees_Discount"] = 0;
                            this.dtRow["FinishDealAdvisoryFees"] = drList["FeesAmount"];
                            this.dtRow["MonthMinAmount"] = drList["MonthMinAmount"];
                            this.dtRow["MonthMinCurr"] = drList["MonthMinCurr"];
                            this.dtRow["MinimumFees_Discount"] = drList["MinimumFees_Discount"];
                            this.dtRow["MinimumFees"] = drList["MinimumFees"];
                            this.dtRow["AllManFees"] = drList["AllManFees"] + "";
                            this.dtRow["SPDAF_ID"] = drList["SPDAF_ID"];
                            _dtList.Rows.Add(dtRow);
                            */
                        }
                    }
                    else
                    {
                        dtRow = _dtList.NewRow();
                        this.dtRow["ServiceProvider_ID"] = drList["PackageProvider_ID"];
                        this.dtRow["AmountFrom"] = drList["AmountFrom"];
                        this.dtRow["AmountTo"] = drList["AmountTo"];
                        this.dtRow["DealAdvisoryFees"] = drList["FeesAmount"];
                        this.dtRow["ID"] = 0;
                        this.dtRow["Contract_ID"] = _iContract_ID;
                        this.dtRow["Contract_Packages_ID"] = _iContract_Packages_ID;
                        this.dtRow["DiscountDateFrom"] = _dFrom;
                        this.dtRow["DiscountDateTo"] = _dTo;
                        this.dtRow["DealAdvisoryFees_Discount"] = 0;
                        this.dtRow["FinishDealAdvisoryFees"] = drList["FeesAmount"];
                        this.dtRow["MonthMinAmount"] = 0;
                        this.dtRow["MonthMinCurr"] = "EUR";
                        this.dtRow["MinimumFees_Discount"] = 0;
                        this.dtRow["MinimumFees"] = 0;
                        this.dtRow["AllManFees"] = drList["FeesAmount"] + "";
                        this.dtRow["SPDAF_ID"] = drList["SPDAF_ID"];
                        _dtList.Rows.Add(dtRow);
                    }
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList_FeesData()
        {
            float sgResult, sgDiff;

            sgResult = 0;
            sgDiff = Convert.ToSingle(this._decAUM);

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetContract_DealAdvisoryFees_Package_ID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ClientsPackage_ID", _iContract_ID));
                cmd.Parameters.Add(new SqlParameter("@Contract_Packages_ID", _iContract_Packages_ID));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dTo));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    sgResult = ((float)drList["FeesAmount"] * this._iDays / 360);
                    if (Convert.ToDateTime(drList["DateFrom"]) <= _dFrom && Convert.ToDateTime(drList["DateTo"]) >= _dTo)
                        this._sgDiscount_Amount = (float)(sgResult * Convert.ToSingle(drList["DealAdvisoryFees_Discount"]) / 100);             
                }
                drList.Close();

                if (sgResult == 0)
                {
                    cmd = new SqlCommand("GetServiceProviderDealAdvisoryFees_Package_ID", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ClientsPackage_ID", _iContract_ID));
                    cmd.Parameters.Add(new SqlParameter("@Contract_Packages_ID", _iContract_Packages_ID));
                    drList = cmd.ExecuteReader();
                    while (drList.Read())
                    {
                        sgResult = ((float)drList["FeesAmount"] * this._iDays / 360);
                    }
                }

                this._sgStartAmount = sgResult;
                if (this._sgStartAmount != 0)
                {
                    this._sgDiscount_Percent = this._sgDiscount_Amount * 100 / this._sgStartAmount;
                }
                else
                {
                    this._sgDiscount_Amount = 0;
                }
                sgResult = sgResult - this._sgDiscount_Amount;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int InsertRecord()
        {
            try
            {
                conn.Open();
                using (cmd = new SqlCommand("InsertClientsDealAdvisoryFees", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@SPDAF_ID", SqlDbType.Int).Value = _iSPDAF_ID;
                    cmd.Parameters.Add("@AmountFrom", SqlDbType.Float).Value = _sgAmountFrom;
                    cmd.Parameters.Add("@AmountTo", SqlDbType.Float).Value = _sgAmountTo;
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = _dFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = _dTo;
                    cmd.Parameters.Add("@DealAdvisoryFees_Discount", SqlDbType.Decimal).Value = _decDealAdvisoryFees_Discount;
                    cmd.Parameters.Add("@DealAdvisoryFees", SqlDbType.Decimal).Value = _decFinishDealAdvisoryFees;
                    cmd.Parameters.Add("@MinimumFees_Discount", SqlDbType.Float).Value = _sgMinimumFees_Discount;
                    cmd.Parameters.Add("@MinimumFees", SqlDbType.Float).Value = _sgMinimumFees;
                    cmd.Parameters.Add("@AllManFees", SqlDbType.NVarChar, 50).Value = _sAllManFees;

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
                using (cmd = new SqlCommand("EditClientsDealAdvisoryFees", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@SPDAF_ID", SqlDbType.Int).Value = _iSPDAF_ID;
                    cmd.Parameters.Add("@AmountFrom", SqlDbType.Float).Value = _sgAmountFrom;
                    cmd.Parameters.Add("@AmountTo", SqlDbType.Float).Value = _sgAmountTo;
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = _dFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = _dTo;
                    cmd.Parameters.Add("@DealAdvisoryFees_Discount", SqlDbType.Decimal).Value = _decDealAdvisoryFees_Discount;
                    cmd.Parameters.Add("@DealAdvisoryFees", SqlDbType.Decimal).Value = _decFinishDealAdvisoryFees;
                    cmd.Parameters.Add("@MinimumFees_Discount", SqlDbType.Float).Value = _sgMinimumFees_Discount;
                    cmd.Parameters.Add("@MinimumFees", SqlDbType.Float).Value = _sgMinimumFees;
                    cmd.Parameters.Add("@AllManFees", SqlDbType.NVarChar, 50).Value = _sAllManFees;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ClientsDealAdvisoryFees";
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
        public int SPDAF_ID { get { return this._iSPDAF_ID; } set { this._iSPDAF_ID = value; } }
        public float AmountFrom { get { return this._sgAmountFrom; } set { this._sgAmountFrom = value; } }
        public float AmountTo { get { return this._sgAmountTo; } set { this._sgAmountTo = value; } }
        public DateTime DateFrom { get { return this._dFrom; } set { this._dFrom = value; } }
        public DateTime DateTo { get { return this._dTo; } set { this._dTo = value; } }
        public decimal DealAdvisoryFees_Discount { get { return this._decDealAdvisoryFees_Discount; } set { this._decDealAdvisoryFees_Discount = value; } }
        public decimal FinishDealAdvisoryFees { get { return this._decFinishDealAdvisoryFees; } set { this._decFinishDealAdvisoryFees = value; } }
        public float MinimumFees_Discount { get { return this._sgMinimumFees_Discount; } set { this._sgMinimumFees_Discount = value; } }
        public float MinimumFees { get { return this._sgMinimumFees; } set { this._sgMinimumFees = value; } }
        public decimal AUM { get { return this._decAUM; } set { this._decAUM = value; } }
        public int Days { get { return this._iDays; } set { this._iDays = value; } }
        public float Discount_Percent { get { return this._sgDiscount_Percent; } set { this._sgDiscount_Percent = value; } }
        public float Discount_Amount { get { return this._sgDiscount_Amount; } set { this._sgDiscount_Amount = value; } }
        public decimal FeesPercent { get { return this._decFeesPercent; } set { this._decFeesPercent = value; } }
        public float StartAmount { get { return this._sgStartAmount; } set { this._sgStartAmount = value; } }
        public string AllManFees { get { return this._sAllManFees; } set { this._sAllManFees = value; } }
        public int Option_ID { get { return this._iOption_ID; } set { this._iOption_ID = value; } }
        public int ServiceProvider_ID { get { return this._iServiceProvider_ID; } set { this._iServiceProvider_ID = value; } }
        public int InvestmentProfile_ID { get { return this._iInvestmentProfile_ID; } set { this._iInvestmentProfile_ID = value; } }
        public int InvestmentPolicy_ID { get { return this._iInvestmentPolicy_ID; } set { this._iInvestmentPolicy_ID = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}






