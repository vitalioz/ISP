using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsClientsAdminFees
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int       _iRecord_ID;
        private int       _iContract_ID;
        private int       _iContract_Packages_ID;
        private int       _iSPAF_ID;
        private float     _sgAmountFrom;
        private float     _sgAmountTo;
        private float     _sgAdminFees;
        private DateTime  _dFrom;
        private DateTime  _dTo;
        private double    _dblAdminFees_Discount;
        private double    _dblFinishAdminFees;
        private float     _sgMinimumFees_Discount;
        private float     _sgMinimumFees;

        private decimal   _decAUM;
        private int       _iDays;
        private float     _sgDiscount_Percent;
        private float     _sgDiscount_Amount;
        private float     _sgFeesPercent;
        private float     _sgStartAmount;
        private string    _sAllManFees;
        private int       _iOption_ID;
        private int       _iServiceProvider_ID;
        private int       _iInvestmentProfile_ID;
        private int       _iInvestmentPolicy_ID;
        private DataTable _dtList;

        public clsClientsAdminFees()
        {
            this._iRecord_ID = 0;
            this._iContract_ID = 0;
            this._iContract_Packages_ID = 0;
            this._iSPAF_ID = 0;
            this._sgAmountFrom = 0;
            this._sgAmountTo = 0;
            this._sgAdminFees = 0;
            this._dFrom = Convert.ToDateTime("1900/01/01");
            this._dTo = Convert.ToDateTime("2070/12/31");
            this._dblAdminFees_Discount = 0;
            this._dblFinishAdminFees = 0;
            this._sgMinimumFees_Discount = 0;
            this._sgMinimumFees = 0;

            this._decAUM = 0;
            this._iDays = 0;
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
                cmd.Parameters.Add(new SqlParameter("@Table", "ClientsAdminFees"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iContract_ID = Convert.ToInt32(drList["Contract_ID"]);
                    this._iContract_Packages_ID = Convert.ToInt32(drList["Contract_Packages_ID"]);
                    this._iSPAF_ID = Convert.ToInt32(drList["SPAF_ID"]); ;
                    this._sgAmountFrom = Convert.ToSingle(drList["AmountFrom"]);
                    this._sgAmountTo = Convert.ToSingle(drList["AmountTo"]);
                    this._sgAdminFees = Convert.ToSingle(drList["AdminFees"]);
                    this._dFrom = Convert.ToDateTime(drList["DateFrom"]);
                    this._dTo = Convert.ToDateTime(drList["DateTo"]);
                    this._dblAdminFees_Discount = Convert.ToDouble(drList["AdminFees_Discount"]);
                    this._dblFinishAdminFees = Convert.ToDouble(drList["AdminFees"]);
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
                _dtList = new DataTable("AdminFees_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ServiceProvider_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AdminFees", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AmountFrom", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AmountTo", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MonthMinAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MonthMinCurr", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("OpenAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("OpenCurr", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ServiceAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ServiceCurr", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("MinAmount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MinCurr", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SPAF_ID", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetPackage_AdministrationFees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ServiceProvider_ID", _iServiceProvider_ID));
                cmd.Parameters.Add(new SqlParameter("@Option_ID", _iOption_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];                                                      // ID - is SPAF_ID
                    this.dtRow["ServiceProvider_ID"] = drList["ServiceProvider_ID"];
                    this.dtRow["AdminFees"] = drList["FeesPercent"];
                    this.dtRow["AmountFrom"] = drList["AmountFrom"];
                    this.dtRow["AmountTo"] = drList["AmountTo"];
                    this.dtRow["MonthMinAmount"] = drList["MonthMinAmount"];
                    this.dtRow["MonthMinCurr"] = drList["MonthMinCurr"];
                    this.dtRow["OpenAmount"] = drList["OpenAmount"];
                    this.dtRow["OpenCurr"] = drList["OpenCurr"];
                    this.dtRow["ServiceAmount"] = drList["ServiceAmount"];
                    this.dtRow["ServiceCurr"] = drList["ServiceCurr"];
                    this.dtRow["MinAmount"] = drList["MinAmount"];
                    this.dtRow["MinCurr"] = drList["MinCurr"];
                    this.dtRow["SPAF_ID"] = drList["ID"];
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
                _dtList = new DataTable("AdminFees_List");
                dtCol = _dtList.Columns.Add("ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ServiceProvider_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Contract_Packages_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AmountFrom", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AmountTo", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AdminFees", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("DiscountDateFrom", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("DiscountDateTo", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("AdminFees_Discount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishAdminFees", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MonthMinAmount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MonthMinCurr", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("MinimumFees_Discount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("MinimumFees", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AllManFees", Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SPAF_ID", Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetAdminFees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Contract_Packages_ID", _iContract_Packages_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    if (!String.IsNullOrEmpty(drList["ID"].ToString()))                                     // it's ClientsAdminFees.ID
                    {
                        if ((Convert.ToDateTime(drList["DateFrom"]) <= _dTo) && (Convert.ToDateTime(drList["DateTo"]) >= _dFrom))
                        {
                            dtRow = _dtList.NewRow();
                            this.dtRow["ServiceProvider_ID"] = drList["PackageProvider_ID"];
                            this.dtRow["AmountFrom"] = drList["AmountFrom"];
                            this.dtRow["AmountTo"] = drList["AmountTo"];
                            this.dtRow["AdminFees"] = drList["FeesPercent"];
                            this.dtRow["ID"] = drList["ID"];
                            this.dtRow["Contract_ID"] = drList["Contract_ID"];
                            this.dtRow["Contract_Packages_ID"] = drList["Contract_Packages_ID"];
                            this.dtRow["DiscountDateFrom"] = drList["DateFrom"];
                            this.dtRow["DiscountDateTo"] = drList["DateTo"];
                            this.dtRow["AdminFees_Discount"] = drList["AdminFees_Discount"];
                            this.dtRow["FinishAdminFees"] = drList["AdminFees"];
                            this.dtRow["MonthMinAmount"] = drList["MonthMinAmount"];
                            this.dtRow["MonthMinCurr"] = drList["MonthMinCurr"];
                            this.dtRow["MinimumFees_Discount"] = drList["MinimumFees_Discount"];
                            this.dtRow["MinimumFees"] = drList["MinimumFees"];
                            this.dtRow["AllManFees"] = drList["AllManFees"] + "";
                            this.dtRow["SPAF_ID"] = drList["SPAF_ID"];
                            _dtList.Rows.Add(dtRow);
                        }
                        else
                        {
                            dtRow = _dtList.NewRow();
                            this.dtRow["ServiceProvider_ID"] = drList["PackageProvider_ID"];
                            this.dtRow["AmountFrom"] = drList["AmountFrom"];
                            this.dtRow["AmountTo"] = drList["AmountTo"];
                            this.dtRow["AdminFees"] = drList["FeesPercent"];
                            this.dtRow["ID"] = drList["ID"];
                            this.dtRow["Contract_ID"] = drList["Contract_ID"];
                            this.dtRow["Contract_Packages_ID"] = drList["Contract_Packages_ID"];
                            this.dtRow["DiscountDateFrom"] = _dFrom;
                            this.dtRow["DiscountDateTo"] = _dTo;
                            this.dtRow["AdminFees_Discount"] = 0;
                            this.dtRow["FinishAdminFees"] = drList["FeesPercent"];
                            this.dtRow["MonthMinAmount"] = drList["MonthMinAmount"];
                            this.dtRow["MonthMinCurr"] = drList["MonthMinCurr"];
                            this.dtRow["MinimumFees_Discount"] = drList["MinimumFees_Discount"];
                            this.dtRow["MinimumFees"] = drList["MinimumFees"];
                            this.dtRow["AllManFees"] = drList["AllManFees"] + "";
                            this.dtRow["SPAF_ID"] = drList["SPAF_ID"];
                            _dtList.Rows.Add(dtRow);
                        }
                    }
                    else
                    {
                        dtRow = _dtList.NewRow();
                        this.dtRow["ServiceProvider_ID"] = drList["PackageProvider_ID"];
                        this.dtRow["AmountFrom"] = drList["AmountFrom"];
                        this.dtRow["AmountTo"] = drList["AmountTo"];
                        this.dtRow["AdminFees"] = drList["FeesPercent"];
                        this.dtRow["ID"] = 0;
                        this.dtRow["Contract_ID"] = _iContract_ID;
                        this.dtRow["Contract_Packages_ID"] = _iContract_Packages_ID;
                        this.dtRow["DiscountDateFrom"] = _dFrom;
                        this.dtRow["DiscountDateTo"] = _dTo;
                        this.dtRow["AdminFees_Discount"] = 0;
                        this.dtRow["FinishAdminFees"] = drList["FeesPercent"];
                        this.dtRow["MonthMinAmount"] = 0;
                        this.dtRow["MonthMinCurr"] = "EUR";
                        this.dtRow["MinimumFees_Discount"] = 0;
                        this.dtRow["MinimumFees"] = 0;
                        this.dtRow["AllManFees"] = drList["FeesPercent"] + "";
                        this.dtRow["SPAF_ID"] = drList["SPAF_ID"];
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
                _dtList = new DataTable("AdminFees_List");
                dtCol = _dtList.Columns.Add("ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AmountFrom", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AmountTo", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FeesPercent", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SPO_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("sp_GetServiceProviderAdministrationFees", conn);
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
                    this.dtRow["SPO_ID"] = drList["SPO_ID"];
                    this.dtRow["Status"] = 0;
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
                cmd = new SqlCommand("GetContract_AdminFees_Package_ID", conn);
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
                            this._sgDiscount_Amount = Convert.ToSingle(this._sgDiscount_Amount + sgTemp * Convert.ToSingle(drList["AdminFees_Discount"]) / 100.0);
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
                            this._sgDiscount_Amount = Convert.ToSingle(Math.Round(this._sgDiscount_Amount + sgTemp * Convert.ToSingle(drList["AdminFees_Discount"]) / 100.0, 2));
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
                    cmd = new SqlCommand("GetServiceProviderAdminFees_Package_ID", conn);
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
                using (cmd = new SqlCommand("InsertClientsAdminFees", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@SPAF_ID", SqlDbType.Int).Value = _iSPAF_ID;
                    cmd.Parameters.Add("@AmountFrom", SqlDbType.Float).Value = _sgAmountFrom;
                    cmd.Parameters.Add("@AmountTo", SqlDbType.Float).Value = _sgAmountTo;
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = _dFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = _dTo;
                    cmd.Parameters.Add("@AdminFees_Discount", SqlDbType.Float).Value = _dblAdminFees_Discount;
                    cmd.Parameters.Add("@AdminFees", SqlDbType.Float).Value = _dblFinishAdminFees;
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
                using (cmd = new SqlCommand("EditClientsAdminFees", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@SPAF_ID", SqlDbType.Int).Value = _iSPAF_ID;
                    cmd.Parameters.Add("@AmountFrom", SqlDbType.Float).Value = _sgAmountFrom;
                    cmd.Parameters.Add("@AmountTo", SqlDbType.Float).Value = _sgAmountTo;
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = _dFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = _dTo;
                    cmd.Parameters.Add("@AdminFees_Discount", SqlDbType.Float).Value = _dblAdminFees_Discount;
                    cmd.Parameters.Add("@AdminFees", SqlDbType.Float).Value = _dblFinishAdminFees;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ClientsAdminFees";
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
        public int SPAF_ID { get { return this._iSPAF_ID; } set { this._iSPAF_ID = value; } }
        public float AmountFrom { get { return this._sgAmountFrom; } set { this._sgAmountFrom = value; } }
        public float AmountTo { get { return this._sgAmountTo; } set { this._sgAmountTo = value; } }
        public float AdminFees { get { return this._sgAdminFees; } set { this._sgAdminFees = value; } }
        public DateTime DateFrom { get { return this._dFrom; } set { this._dFrom = value; } }
        public DateTime DateTo { get { return this._dTo; } set { this._dTo = value; } }
        public double AdminFees_Discount { get { return this._dblAdminFees_Discount; } set { this._dblAdminFees_Discount = value; } }
        public double FinishAdminFees { get { return this._dblFinishAdminFees; } set { this._dblFinishAdminFees = value; } }
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






