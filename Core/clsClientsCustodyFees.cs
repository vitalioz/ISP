using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsClientsCustodyFees
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int _iRecord_ID;
        private int _iContract_ID;
        private int _iContract_Packages_ID;
        private int _iSPCF_ID;
        private float _sgAmountFrom;
        private float _sgAmountTo;
        private DateTime _dFrom;
        private DateTime _dTo;
        private decimal _decCustodyFees_Discount;
        private decimal _decFinishCustodyFees;
        private float _sgMinimumFees_Discount;
        private float _sgMinimumFees;

        private decimal _decAUM;
        private int _iDays;
        private decimal _decCustodyFees;
        private float _sgDiscount_Percent;
        private float _sgDiscount_Amount;
        private float _sgFeesPercent;
        private float _sgStartAmount;
        private string _sAllManFees;
        private int _iOption_ID;
        private int _iServiceProvider_ID;
        private int _iInvestmentProfile_ID;
        private int _iInvestmentPolicy_ID;
        private DataTable _dtList;

        public clsClientsCustodyFees()
        {
            this._iRecord_ID = 0;
            this._iContract_ID = 0;
            this._iContract_Packages_ID = 0;
            this._iSPCF_ID = 0;
            this._sgAmountFrom = 0;
            this._sgAmountTo = 0;
            this._dFrom = Convert.ToDateTime("1900/01/01");
            this._dTo = Convert.ToDateTime("2070/12/31");
            this._decCustodyFees_Discount = 0;
            this._decFinishCustodyFees = 0;
            this._sgMinimumFees_Discount = 0;
            this._sgMinimumFees = 0;

            this._decAUM = 0;
            this._iDays = 0;
            this._decCustodyFees = 0;
            this._sgDiscount_Percent = 0;
            this._sgDiscount_Amount = 0;
            this._sgFeesPercent = 0;
            this._sgStartAmount = 0;
            this._sAllManFees = "";
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
                cmd.Parameters.Add(new SqlParameter("@Table", "ClientsCustodyFees"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iContract_ID = Convert.ToInt32(drList["Contract_ID"]);
                    this._iContract_Packages_ID = Convert.ToInt32(drList["Contract_Packages_ID"]);
                    this._iSPCF_ID = Convert.ToInt32(drList["SPCF_ID"]); ;
                    this._sgAmountFrom = Convert.ToSingle(drList["AmountFrom"]);
                    this._sgAmountTo = Convert.ToSingle(drList["AmountTo"]);
                    this._decCustodyFees = Convert.ToDecimal(drList["CustodyFees"]);
                    this._dFrom = Convert.ToDateTime(drList["DateFrom"]);
                    this._dTo = Convert.ToDateTime(drList["DateTo"]);
                    this._decCustodyFees_Discount = Convert.ToDecimal(drList["CustodyFees_Discount"]);
                    this._decFinishCustodyFees = Convert.ToDecimal(drList["CustodyFees"]);
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
                _dtList = new DataTable("CustodyFees_List");
                dtCol = _dtList.Columns.Add("ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ServiceProvider_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AmountFrom", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AmountTo", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("CustodyFees", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("DiscountDateFrom", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("DiscountDateTo", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("CustodyFees_Discount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishCustodyFees", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MonthMinAmount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MonthMinCurr", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("OpenAmount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("OpenCurr", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ServiceAmount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ServiceCurr", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("MinAmount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MinCurr", Type.GetType("System.String"));

                conn.Open();
                cmd = new SqlCommand("GetPackage_CustodyFees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ServiceProvider_ID", _iServiceProvider_ID));
                cmd.Parameters.Add(new SqlParameter("@Option_ID", _iOption_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];                                                      // ID - is SPCF_ID
                    this.dtRow["ServiceProvider_ID"] = drList["ServiceProvider_ID"];
                    this.dtRow["AmountFrom"] = drList["AmountFrom"];
                    this.dtRow["AmountTo"] = drList["AmountTo"];
                    this.dtRow["CustodyFees"] = drList["FeesPercent"];
                    this.dtRow["DiscountDateFrom"] = _dFrom.ToString("dd/MM/yyyy");
                    this.dtRow["DiscountDateTo"] = _dTo.ToString("dd/MM/yyyy");
                    this.dtRow["CustodyFees_Discount"] = 0;
                    this.dtRow["FinishCustodyFees"] = drList["FeesPercent"];
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
                _dtList = new DataTable("CustodyFees_List");
                dtCol = _dtList.Columns.Add("ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ServiceProvider_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_Packages_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AmountFrom", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AmountTo", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("CustodyFees", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("DiscountDateFrom", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("DiscountDateTo", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("CustodyFees_Discount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishCustodyFees", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MonthMinAmount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MonthMinCurr", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("MinimumFees_Discount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MinimumFees", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AllManFees", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SPCF_ID", Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetCustodyFees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Contract_Packages_ID", _iContract_Packages_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    if (!String.IsNullOrEmpty(drList["ID"].ToString()))                                     // it's ClientsCustodyFees.ID
                    {
                        if ((Convert.ToDateTime(drList["DateFrom"]) <= _dTo) && (Convert.ToDateTime(drList["DateTo"]) >= _dFrom))
                        {
                            dtRow = _dtList.NewRow();
                            this.dtRow["ServiceProvider_ID"] = drList["PackageProvider_ID"];
                            this.dtRow["AmountFrom"] = drList["AmountFrom"];
                            this.dtRow["AmountTo"] = drList["AmountTo"];
                            this.dtRow["CustodyFees"] = drList["FeesPercent"];
                            this.dtRow["ID"] = drList["ID"];
                            this.dtRow["Contract_ID"] = drList["Contract_ID"];
                            this.dtRow["Contract_Packages_ID"] = drList["Contract_Packages_ID"];
                            this.dtRow["DiscountDateFrom"] = drList["DateFrom"];
                            this.dtRow["DiscountDateTo"] = drList["DateTo"];
                            this.dtRow["CustodyFees_Discount"] = drList["CustodyFees_Discount"];
                            this.dtRow["FinishCustodyFees"] = drList["CustodyFees"];
                            this.dtRow["MonthMinAmount"] = drList["MonthMinAmount"];
                            this.dtRow["MonthMinCurr"] = drList["MonthMinCurr"];
                            this.dtRow["MinimumFees_Discount"] = drList["MinimumFees_Discount"];
                            this.dtRow["MinimumFees"] = drList["MinimumFees"];
                            this.dtRow["AllManFees"] = drList["AllManFees"] + "";
                            this.dtRow["SPCF_ID"] = drList["SPCF_ID"];
                            _dtList.Rows.Add(dtRow);
                        }
                        else
                        {
                            dtRow = _dtList.NewRow();
                            this.dtRow["ServiceProvider_ID"] = drList["PackageProvider_ID"];
                            this.dtRow["AmountFrom"] = drList["AmountFrom"];
                            this.dtRow["AmountTo"] = drList["AmountTo"];
                            this.dtRow["CustodyFees"] = drList["FeesPercent"];
                            this.dtRow["ID"] = drList["ID"];
                            this.dtRow["Contract_ID"] = drList["Contract_ID"];
                            this.dtRow["Contract_Packages_ID"] = drList["Contract_Packages_ID"];
                            this.dtRow["DiscountDateFrom"] = _dFrom;
                            this.dtRow["DiscountDateTo"] = _dTo;
                            this.dtRow["CustodyFees_Discount"] = 0;
                            this.dtRow["FinishCustodyFees"] = drList["FeesPercent"];
                            this.dtRow["MonthMinAmount"] = drList["MonthMinAmount"];
                            this.dtRow["MonthMinCurr"] = drList["MonthMinCurr"];
                            this.dtRow["MinimumFees_Discount"] = drList["MinimumFees_Discount"];
                            this.dtRow["MinimumFees"] = drList["MinimumFees"];
                            this.dtRow["AllManFees"] = drList["AllManFees"] + "";
                            this.dtRow["SPCF_ID"] = drList["SPCF_ID"];
                            _dtList.Rows.Add(dtRow);
                        }
                    }
                    else
                    {
                        dtRow = _dtList.NewRow();
                        this.dtRow["ServiceProvider_ID"] = drList["PackageProvider_ID"];
                        this.dtRow["AmountFrom"] = drList["AmountFrom"];
                        this.dtRow["AmountTo"] = drList["AmountTo"];
                        this.dtRow["CustodyFees"] = drList["FeesPercent"];
                        this.dtRow["ID"] = 0;
                        this.dtRow["Contract_ID"] = _iContract_ID;
                        this.dtRow["Contract_Packages_ID"] = _iContract_Packages_ID;
                        this.dtRow["DiscountDateFrom"] = _dFrom;
                        this.dtRow["DiscountDateTo"] = _dTo;
                        this.dtRow["CustodyFees_Discount"] = 0;
                        this.dtRow["FinishCustodyFees"] = drList["FeesPercent"];
                        this.dtRow["MonthMinAmount"] = 0;
                        this.dtRow["MonthMinCurr"] = "EUR";
                        this.dtRow["MinimumFees_Discount"] = 0;
                        this.dtRow["MinimumFees"] = 0;
                        this.dtRow["AllManFees"] = drList["FeesPercent"] + "";
                        this.dtRow["SPCF_ID"] = drList["SPCF_ID"];
                        _dtList.Rows.Add(dtRow);
                    }
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList_Provider_ID()
        {
            try
            {
                _dtList = new DataTable("CustodyFees_List");
                dtCol = _dtList.Columns.Add("ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AmountFrom", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AmountTo", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RetrosessionMethod", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("RetrosessionProvider", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("RetrosessionCompany", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SPO_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Pseudo_ID", System.Type.GetType("System.Int32"));


                conn.Open();
                cmd = new SqlCommand("sp_GetServiceProviderCustodyFees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ServiceProvider_ID ", _iServiceProvider_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];
                    this.dtRow["AmountFrom"] = drList["AmountFrom"];
                    this.dtRow["AmountTo"] = drList["AmountTo"];
                    this.dtRow["FeesPercent"] = drList["FeesPercent"];
                    this.dtRow["RetrosessionMethod"] = drList["RetrosessionMethod"];
                    this.dtRow["RetrosessionProvider"] = drList["RetrosessionProvider"];
                    this.dtRow["RetrosessionCompany"] = drList["RetrosessionCompany"];
                    this.dtRow["SPO_ID"] = drList["SPO_ID"];
                    this.dtRow["Status"] = 0;
                    this.dtRow["Pseudo_ID"] = drList["SPO_ID"];
                    _dtList.Rows.Add(dtRow);
                }               
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList_FeesData()
        {
            int j = 0;
            float sgTemp, sgTemp2, sgResult, sgDiff;

            sgTemp = 0;
            sgTemp2 = 0;
            sgResult = 0;
            sgDiff = Convert.ToSingle(this._decAUM);

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetContract_CustodyFees_Package_ID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ClientsPackage_ID", _iContract_ID));
                cmd.Parameters.Add(new SqlParameter("@Contract_Packages_ID", _iContract_Packages_ID));
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dTo));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {

                    if (Convert.ToDecimal(drList["AmountTo"]) >= this._decAUM)
                    {
                        sgTemp = sgDiff * Convert.ToSingle(drList["FeesPercent"]) * this._iDays / 36000;
                        sgResult = sgResult + sgTemp;
                        if (Convert.ToDateTime(drList["DateFrom"]) <= _dFrom && Convert.ToDateTime(drList["DateTo"]) >= _dTo)
                        {
                            this._sgDiscount_Amount = Convert.ToSingle(this._sgDiscount_Amount + sgTemp * Convert.ToSingle(drList["CustodyFees_Discount"]) / 100.0);
                        }

                        sgTemp2 = sgDiff * Convert.ToSingle(drList["FeesPercent"]);
                        this._sgFeesPercent = this._sgFeesPercent + sgTemp2;
                        break;
                    }
                    else
                    {
                        sgTemp = (Convert.ToSingle(drList["AmountTo"]) - Convert.ToSingle(drList["AmountFrom"]) + j) * Convert.ToSingle(drList["FeesPercent"]) * this._iDays / 36000;
                        sgResult = sgResult + sgTemp;
                        if (Convert.ToDateTime(drList["DateFrom"]) <= _dFrom && Convert.ToDateTime(drList["DateTo"]) >= _dTo)
                        {
                            this._sgDiscount_Amount = Convert.ToSingle(Math.Round(this._sgDiscount_Amount + sgTemp * Convert.ToSingle(drList["CustodyFees_Discount"]) / 100.0, 2));
                        }

                        sgDiff = sgDiff - (Convert.ToSingle(drList["AmountTo"]) - Convert.ToSingle(drList["AmountFrom"]) + j);
                        sgTemp2 = (Convert.ToSingle(drList["AmountTo"]) - Convert.ToSingle(drList["AmountFrom"]) + j) * Convert.ToSingle(drList["FeesPercent"]);
                        this._sgFeesPercent = this._sgFeesPercent + sgTemp2;
                        j = 1;
                    }
                }
                drList.Close();

                if (this._sgFeesPercent == 0)
                {
                    cmd = new SqlCommand("GetServiceProviderCustodyFees_Package_ID", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ClientsPackage_ID", _iContract_ID));
                    cmd.Parameters.Add(new SqlParameter("@Contract_Packages_ID", _iContract_Packages_ID));
                    drList = cmd.ExecuteReader();
                    while (drList.Read())
                    {
                        if (Convert.ToSingle(drList["AmountTo"]) >= Convert.ToSingle(this._decAUM))
                        {
                            sgTemp = sgDiff * Convert.ToSingle(drList["FeesPercent"]) * this._iDays / 36000;
                            sgResult = sgResult + sgTemp;
                            sgTemp2 = sgDiff * Convert.ToSingle(drList["FeesPercent"]);
                            this._sgFeesPercent = this._sgFeesPercent + sgTemp2;
                        }
                        else
                        {
                            sgTemp = (Convert.ToSingle(drList["AmountTo"]) - Convert.ToSingle(drList["AmountFrom"]) + j) * Convert.ToSingle(drList["FeesPercent"]) * this._iDays / 36000;
                            sgResult = sgResult + sgTemp;
                            sgDiff = sgDiff - (Convert.ToSingle(drList["AmountTo"]) - Convert.ToSingle(drList["AmountFrom"]) + j);
                            sgTemp2 = (Convert.ToSingle(drList["AmountTo"]) - Convert.ToSingle(drList["AmountFrom"]) + j) * Convert.ToSingle(drList["FeesPercent"]);
                            this._sgFeesPercent = this._sgFeesPercent + sgTemp2;
                            j = 1;
                        }
                    }
                }

                this._sgFeesPercent = this._sgFeesPercent / Convert.ToSingle(this._decAUM);
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
                using (cmd = new SqlCommand("InsertClientsCustodyFees", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@SPCF_ID", SqlDbType.Int).Value = _iSPCF_ID;
                    cmd.Parameters.Add("@AmountFrom", SqlDbType.Float).Value = _sgAmountFrom;
                    cmd.Parameters.Add("@AmountTo", SqlDbType.Float).Value = _sgAmountTo;
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = _dFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = _dTo;
                    cmd.Parameters.Add("@CustodyFees_Discount", SqlDbType.Float).Value = _decCustodyFees_Discount;
                    cmd.Parameters.Add("@CustodyFees", SqlDbType.Float).Value = _decFinishCustodyFees;
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
                using (cmd = new SqlCommand("EditClientsCustodyFees", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@SPCF_ID", SqlDbType.Int).Value = _iSPCF_ID;
                    cmd.Parameters.Add("@AmountFrom", SqlDbType.Float).Value = _sgAmountFrom;
                    cmd.Parameters.Add("@AmountTo", SqlDbType.Float).Value = _sgAmountTo;
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = _dFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = _dTo;
                    cmd.Parameters.Add("@CustodyFees_Discount", SqlDbType.Float).Value = _decCustodyFees_Discount;
                    cmd.Parameters.Add("@CustodyFees", SqlDbType.Float).Value = _decFinishCustodyFees;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ClientsCustodyFees";
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
        public int SPCF_ID { get { return this._iSPCF_ID; } set { this._iSPCF_ID = value; } }
        public float AmountFrom { get { return this._sgAmountFrom; } set { this._sgAmountFrom = value; } }
        public float AmountTo { get { return this._sgAmountTo; } set { this._sgAmountTo = value; } }
        public decimal CustodyFees { get { return this._decCustodyFees; } set { this._decCustodyFees = value; } }
        public DateTime DateFrom { get { return this._dFrom; } set { this._dFrom = value; } }
        public DateTime DateTo { get { return this._dTo; } set { this._dTo = value; } }
        public decimal CustodyFees_Discount { get { return this._decCustodyFees_Discount; } set { this._decCustodyFees_Discount = value; } }
        public decimal FinishCustodyFees { get { return this._decFinishCustodyFees; } set { this._decFinishCustodyFees = value; } }
        public float MinimumFees_Discount { get { return this._sgMinimumFees_Discount; } set { this._sgMinimumFees_Discount = value; } }
        public float MinimumFees { get { return this._sgMinimumFees; } set { this._sgMinimumFees = value; } }
        public decimal AUM { get { return this._decAUM; } set { this._decAUM = value; } }
        public int Days { get { return this._iDays; } set { this._iDays = value; } }
        public float Discount_Percent { get { return this._sgDiscount_Percent; } set { this._sgDiscount_Percent = value; } }
        public float Discount_Amount { get { return this._sgDiscount_Amount; } set { this._sgDiscount_Amount = value; } }
        public float FeesPercent { get { return this._sgFeesPercent; } set { this._sgFeesPercent = value; } }
        public float StartAmount { get { return this._sgStartAmount; } set { this._sgStartAmount = value; } }
        public string AllManFees { get { return this._sAllManFees; } set { this._sAllManFees = value; } }
        public int Option_ID { get { return this._iOption_ID; } set { this._iOption_ID = value; } }
        public int ServiceProvider_ID { get { return this._iServiceProvider_ID; } set { this._iServiceProvider_ID = value; } }
        public int InvestmentProfile_ID { get { return this._iInvestmentProfile_ID; } set { this._iInvestmentProfile_ID = value; } }
        public int InvestmentPolicy_ID { get { return this._iInvestmentPolicy_ID; } set { this._iInvestmentPolicy_ID = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}






