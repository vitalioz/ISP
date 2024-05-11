using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsClientsDiscretFees
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int _iRecord_ID;
        private int _iContract_ID;
        private int _iContract_Packages_ID;
        private int _iSPDF_ID;
        private float _fltAmountFrom;
        private float _fltAmountTo;
        private DateTime _dFrom;
        private DateTime _dTo;
        private decimal _decDiscretFees_Discount;
        private decimal _decFinishDiscretFees;
        private float _fltMinimumFees_Discount;
        private float _fltMinimumFees;
        private string _sAllManFees;

        private decimal _decAUM;
        private int _iDays;
        private decimal _decDiscretFees;
        private float _fltDiscount_Percent;
        private float _fltDiscount_Amount;
        private float _fltFeesPercent;
        private float _fltStartAmount;
        private int _iOption_ID;
        private int _iServiceProvider_ID;
        private int _iInvestmentProfile_ID;
        private int _iInvestmentPolicy_ID;
        private DataTable _dtList;

        public clsClientsDiscretFees()
        {
            this._iRecord_ID = 0;
            this._iContract_ID = 0;
            this._iContract_Packages_ID = 0;
            this._iSPDF_ID = 0;
            this._fltAmountFrom = 0;
            this._fltAmountTo = 0;
            this._dFrom = Convert.ToDateTime("1900/01/01");
            this._dTo = Convert.ToDateTime("2070/12/31");
            this._decDiscretFees_Discount = 0;
            this._decFinishDiscretFees = 0;
            this._fltMinimumFees_Discount = 0;
            this._fltMinimumFees = 0;

            this._decAUM = 0;
            this._iDays = 0;
            this._decDiscretFees = 0;
            this._fltDiscount_Percent = 0;
            this._fltDiscount_Amount = 0;
            this._fltFeesPercent = 0;
            this._fltStartAmount = 0;
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
                cmd.Parameters.Add(new SqlParameter("@Table", "ClientsDiscretFees"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iContract_ID = Convert.ToInt32(drList["Contract_ID"]);
                    this._iContract_Packages_ID = Convert.ToInt32(drList["Contract_Packages_ID"]);
                    this._iSPDF_ID = Convert.ToInt32(drList["SPDF_ID"]); ;
                    this._fltAmountFrom = Convert.ToSingle(drList["AmountFrom"]);
                    this._fltAmountTo = Convert.ToSingle(drList["AmountTo"]);
                    this._dFrom = Convert.ToDateTime(drList["DateFrom"]);
                    this._dTo = Convert.ToDateTime(drList["DateTo"]);
                    this._decDiscretFees = Convert.ToDecimal(drList["DiscretFees"]);
                    this._decDiscretFees_Discount = Convert.ToDecimal(drList["DiscretFees_Discount"]);
                    this._decFinishDiscretFees = Convert.ToDecimal(drList["DiscretFees"]);
                    this._fltMinimumFees_Discount = Convert.ToSingle(drList["MinimumFees_Discount"]);
                    this._fltMinimumFees = Convert.ToSingle(drList["MinimumFees"]);
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
                _dtList = new DataTable("DiscretFees_List");
                dtCol = _dtList.Columns.Add("ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ServiceProvider_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AmountFrom", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AmountTo", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("DiscretFees", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("DiscountDateFrom", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("DiscountDateTo", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("DiscretFees_Discount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishDiscretFees", Type.GetType("System.Single"));
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
                cmd = new SqlCommand("GetPackage_DiscretFees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ServiceProvider_ID", _iServiceProvider_ID));
                cmd.Parameters.Add(new SqlParameter("@Option_ID", _iOption_ID));
                cmd.Parameters.Add(new SqlParameter("@InvestmentProfile_ID", _iInvestmentProfile_ID));
                cmd.Parameters.Add(new SqlParameter("@InvestmentPolicy_ID", _iInvestmentPolicy_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];                                                      // ID - is SPDF_ID
                    this.dtRow["ServiceProvider_ID"] = drList["ServiceProvider_ID"];
                    this.dtRow["AmountFrom"] = drList["AmountFrom"];
                    this.dtRow["AmountTo"] = drList["AmountTo"];
                    this.dtRow["DiscretFees"] = drList["FeesPercent"];
                    this.dtRow["DiscountDateFrom"] = _dFrom.ToString("dd/MM/yyyy");
                    this.dtRow["DiscountDateTo"] = _dTo.ToString("dd/MM/yyyy");
                    this.dtRow["DiscretFees_Discount"] = 0;
                    this.dtRow["FinishDiscretFees"] = drList["FeesPercent"];
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
                _dtList = new DataTable("DiscretFees_List");
                dtCol = _dtList.Columns.Add("ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ServiceProvider_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_Packages_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AmountFrom", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AmountTo", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("DiscretFees", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("DiscountDateFrom", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("DiscountDateTo", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("DiscretFees_Discount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishDiscretFees", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MonthMinAmount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MonthMinCurr", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("MinimumFees_Discount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MinimumFees", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AllManFees", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SPDF_ID", Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetDiscretFees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Contract_Packages_ID", _iContract_Packages_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    if (!String.IsNullOrEmpty(drList["ID"].ToString()))                                     // it's ClientsDiscretFees.ID
                    {
                        if ((Convert.ToDateTime(drList["DateFrom"]) <= _dTo) && (Convert.ToDateTime(drList["DateTo"]) >= _dFrom))
                        {
                            dtRow = _dtList.NewRow();
                            this.dtRow["ServiceProvider_ID"] = drList["PackageProvider_ID"];
                            this.dtRow["AmountFrom"] = drList["AmountFrom"];
                            this.dtRow["AmountTo"] = drList["AmountTo"];
                            this.dtRow["DiscretFees"] = drList["FeesPercent"];
                            this.dtRow["ID"] = drList["ID"];
                            this.dtRow["Contract_ID"] = drList["Contract_ID"];
                            this.dtRow["Contract_Packages_ID"] = drList["Contract_Packages_ID"];
                            this.dtRow["DiscountDateFrom"] = drList["DateFrom"];
                            this.dtRow["DiscountDateTo"] = drList["DateTo"];
                            this.dtRow["DiscretFees_Discount"] = drList["DiscretFees_Discount"];
                            this.dtRow["FinishDiscretFees"] = drList["DiscretFees"];
                            this.dtRow["MonthMinAmount"] = drList["MonthMinAmount"];
                            this.dtRow["MonthMinCurr"] = drList["MonthMinCurr"];
                            this.dtRow["MinimumFees_Discount"] = drList["MinimumFees_Discount"];
                            this.dtRow["MinimumFees"] = drList["MinimumFees"];
                            this.dtRow["AllManFees"] = drList["AllManFees"] + "";
                            this.dtRow["SPDF_ID"] = drList["SPDF_ID"];
                            _dtList.Rows.Add(dtRow);
                        }
                        else
                        {
                            /*
                            dtRow = _dtList.NewRow();
                            this.dtRow["ServiceProvider_ID"] = drList["PackageProvider_ID"];
                            this.dtRow["AmountFrom"] = drList["AmountFrom"];
                            this.dtRow["AmountTo"] = drList["AmountTo"];
                            this.dtRow["DiscretFees"] = drList["FeesPercent"];
                            this.dtRow["ID"] = drList["ID"];
                            this.dtRow["Contract_ID"] = drList["Contract_ID"];
                            this.dtRow["Contract_Packages_ID"] = drList["Contract_Packages_ID"];
                            this.dtRow["DiscountDateFrom"] = _dFrom;    
                            this.dtRow["DiscountDateTo"] = _dTo;
                            this.dtRow["DiscretFees_Discount"] = 0;
                            this.dtRow["FinishDiscretFees"] = drList["FeesPercent"];
                            this.dtRow["MonthMinAmount"] = drList["MonthMinAmount"];
                            this.dtRow["MonthMinCurr"] = drList["MonthMinCurr"];
                            this.dtRow["MinimumFees_Discount"] = drList["MinimumFees_Discount"];
                            this.dtRow["MinimumFees"] = drList["MinimumFees"];
                            this.dtRow["AllManFees"] = drList["AllManFees"] + "";
                            this.dtRow["SPDF_ID"] = drList["SPDF_ID"];
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
                        this.dtRow["DiscretFees"] = drList["FeesPercent"];
                        this.dtRow["ID"] = 0;
                        this.dtRow["Contract_ID"] = _iContract_ID;
                        this.dtRow["Contract_Packages_ID"] = _iContract_Packages_ID;
                        this.dtRow["DiscountDateFrom"] = _dFrom;
                        this.dtRow["DiscountDateTo"] = _dTo;
                        this.dtRow["DiscretFees_Discount"] = 0;
                        this.dtRow["FinishDiscretFees"] = drList["FeesPercent"];
                        this.dtRow["MonthMinAmount"] = 0;
                        this.dtRow["MonthMinCurr"] = "EUR";
                        this.dtRow["MinimumFees_Discount"] = 0;
                        this.dtRow["MinimumFees"] = 0;
                        this.dtRow["AllManFees"] = drList["FeesPercent"] + "";
                        this.dtRow["SPDF_ID"] = drList["SPDF_ID"];
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
            int j = 0;
            float sgTemp, sgTemp2, sgResult, sgDiff;

            sgTemp = 0;
            sgTemp2 = 0;
            sgResult = 0;
            sgDiff = Convert.ToSingle(this._decAUM);

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetContract_DiscretFees_Package_ID", conn);
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
                            this._fltDiscount_Amount = Convert.ToSingle(this._fltDiscount_Amount + sgTemp * Convert.ToSingle(drList["DiscretFees_Discount"]) / 100.0);
                        }

                        sgTemp2 = sgDiff * Convert.ToSingle(drList["FeesPercent"]);
                        this._fltFeesPercent = this._fltFeesPercent + sgTemp2;
                        break;
                    }
                    else
                    {
                        sgTemp = (Convert.ToSingle(drList["AmountTo"]) - Convert.ToSingle(drList["AmountFrom"]) + j) * Convert.ToSingle(drList["FeesPercent"]) * this._iDays / 36000;
                        sgResult = sgResult + sgTemp;
                        if (Convert.ToDateTime(drList["DateFrom"]) <= _dFrom && Convert.ToDateTime(drList["DateTo"]) >= _dTo)
                        {
                            this._fltDiscount_Amount = Convert.ToSingle(Math.Round(this._fltDiscount_Amount + sgTemp * Convert.ToSingle(drList["DiscretFees_Discount"]) / 100.0, 2));
                        }

                        sgDiff = sgDiff - (Convert.ToSingle(drList["AmountTo"]) - Convert.ToSingle(drList["AmountFrom"]) + j);
                        sgTemp2 = (Convert.ToSingle(drList["AmountTo"]) - Convert.ToSingle(drList["AmountFrom"]) + j) * Convert.ToSingle(drList["FeesPercent"]);
                        this._fltFeesPercent = this._fltFeesPercent + sgTemp2;
                        j = 1;
                    }
                }
                drList.Close();

                if (this._fltFeesPercent == 0)
                {
                    cmd = new SqlCommand("GetServiceProviderDiscretFees_Package_ID", conn);
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
                            this._fltFeesPercent = this._fltFeesPercent + sgTemp2;
                        }
                        else
                        {
                            sgTemp = (Convert.ToSingle(drList["AmountTo"]) - Convert.ToSingle(drList["AmountFrom"]) + j) * Convert.ToSingle(drList["FeesPercent"]) * this._iDays / 36000;
                            sgResult = sgResult + sgTemp;
                            sgDiff = sgDiff - (Convert.ToSingle(drList["AmountTo"]) - Convert.ToSingle(drList["AmountFrom"]) + j);
                            sgTemp2 = (Convert.ToSingle(drList["AmountTo"]) - Convert.ToSingle(drList["AmountFrom"]) + j) * Convert.ToSingle(drList["FeesPercent"]);
                            this._fltFeesPercent = this._fltFeesPercent + sgTemp2;
                            j = 1;
                        }
                    }
                }

                if (this._decAUM != 0) this._fltFeesPercent = this._fltFeesPercent / Convert.ToSingle(this._decAUM);
                this._fltStartAmount = sgResult;
                if (this._fltStartAmount != 0)
                {
                    this._fltDiscount_Percent = this._fltDiscount_Amount * 100 / this._fltStartAmount;
                }
                else
                {
                    this._fltDiscount_Amount = 0;
                }

                sgResult = sgResult - this._fltDiscount_Amount;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int InsertRecord()
        {
            try
            {
                conn.Open();
                using (cmd = new SqlCommand("InsertClientsDiscretFees", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@SPDF_ID", SqlDbType.Int).Value = _iSPDF_ID;
                    cmd.Parameters.Add("@AmountFrom", SqlDbType.Float).Value = _fltAmountFrom;
                    cmd.Parameters.Add("@AmountTo", SqlDbType.Float).Value = _fltAmountTo;
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = _dFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = _dTo;
                    cmd.Parameters.Add("@DiscretFees_Discount", SqlDbType.Float).Value = _decDiscretFees_Discount;
                    cmd.Parameters.Add("@DiscretFees", SqlDbType.Float).Value = _decFinishDiscretFees;
                    cmd.Parameters.Add("@MinimumFees_Discount", SqlDbType.Float).Value = _fltMinimumFees_Discount;
                    cmd.Parameters.Add("@MinimumFees", SqlDbType.Float).Value = _fltMinimumFees;
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
                using (cmd = new SqlCommand("EditClientsDiscretFees", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@SPDF_ID", SqlDbType.Int).Value = _iSPDF_ID;
                    cmd.Parameters.Add("@AmountFrom", SqlDbType.Float).Value = _fltAmountFrom;
                    cmd.Parameters.Add("@AmountTo", SqlDbType.Float).Value = _fltAmountTo;
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = _dFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = _dTo;
                    cmd.Parameters.Add("@DiscretFees_Discount", SqlDbType.Float).Value = _decDiscretFees_Discount;
                    cmd.Parameters.Add("@DiscretFees", SqlDbType.Float).Value = _decFinishDiscretFees;
                    cmd.Parameters.Add("@MinimumFees_Discount", SqlDbType.Float).Value = _fltMinimumFees_Discount;
                    cmd.Parameters.Add("@MinimumFees", SqlDbType.Float).Value = _fltMinimumFees;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ClientsDiscretFees";
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
        public int SPDF_ID { get { return this._iSPDF_ID; } set { this._iSPDF_ID = value; } }
        public float AmountFrom { get { return this._fltAmountFrom; } set { this._fltAmountFrom = value; } }
        public float AmountTo { get { return this._fltAmountTo; } set { this._fltAmountTo = value; } }
        public decimal DiscretFees { get { return this._decDiscretFees; } set { this._decDiscretFees = value; } }
        public DateTime DateFrom { get { return this._dFrom; } set { this._dFrom = value; } }
        public DateTime DateTo { get { return this._dTo; } set { this._dTo = value; } }
        public decimal DiscretFees_Discount { get { return this._decDiscretFees_Discount; } set { this._decDiscretFees_Discount = value; } }
        public decimal FinishDiscretFees { get { return this._decFinishDiscretFees; } set { this._decFinishDiscretFees = value; } }
        public float MinimumFees_Discount { get { return this._fltMinimumFees_Discount; } set { this._fltMinimumFees_Discount = value; } }
        public float MinimumFees { get { return this._fltMinimumFees; } set { this._fltMinimumFees = value; } }
        public decimal AUM { get { return this._decAUM; } set { this._decAUM = value; } }
        public int Days { get { return this._iDays; } set { this._iDays = value; } }
        public float Discount_Percent { get { return this._fltDiscount_Percent; } set { this._fltDiscount_Percent = value; } }
        public float Discount_Amount { get { return this._fltDiscount_Amount; } set { this._fltDiscount_Amount = value; } }
        public float FeesPercent { get { return this._fltFeesPercent; } set { this._fltFeesPercent = value; } }
        public float StartAmount { get { return this._fltStartAmount; } set { this._fltStartAmount = value; } }
        public string AllManFees { get { return this._sAllManFees; } set { this._sAllManFees = value; } }
        public int Option_ID { get { return this._iOption_ID; } set { this._iOption_ID = value; } }
        public int ServiceProvider_ID { get { return this._iServiceProvider_ID; } set { this._iServiceProvider_ID = value; } }
        public int InvestmentProfile_ID { get { return this._iInvestmentProfile_ID; } set { this._iInvestmentProfile_ID = value; } }
        public int InvestmentPolicy_ID { get { return this._iInvestmentPolicy_ID; } set { this._iInvestmentPolicy_ID = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}






