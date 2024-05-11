using System;                                   
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsClientsFXFees
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;
        DataRow[] foundRows;

        private int      _iRecord_ID;
        private int      _iContract_ID;
        private int      _iContract_Packages_ID;
        private int      _iSPFF_ID;
        private float    _fltAmountFrom;
        private float    _fltAmountTo;
        private DateTime _dFrom;
        private DateTime _dTo;
        private float    _fltFXFees_Discount;
        private float    _fltFinishFXFees;

        private DateTime _dAktionDate;
        private bool     _bIncludeDiscount;
        private int      _iOption_ID;
        private int      _iServiceProvider_ID;
        private DataTable _dtList;

        public clsClientsFXFees()
        {
            this._iRecord_ID = 0;
            this._iContract_ID = 0;
            this._iContract_Packages_ID = 0;
            this._iSPFF_ID = 0;
            this._fltAmountFrom = 0;
            this._fltAmountTo = 0;
            this._dFrom = Convert.ToDateTime("1900/01/01");
            this._dTo = Convert.ToDateTime("2070/12/31");
            this._fltFXFees_Discount = 0;
            this._fltFinishFXFees = 0;

            this._bIncludeDiscount = false;
            this._iOption_ID = 0;
            this._iServiceProvider_ID = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "ClientsFXFees"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iContract_ID = Convert.ToInt32(drList["Contract_ID"]);
                    this._iContract_Packages_ID = Convert.ToInt32(drList["Contract_Packages_ID"]);
                    this._iSPFF_ID = Convert.ToInt32(drList["SPFF_ID"]); ;
                    this._fltAmountFrom = Convert.ToSingle(drList["AmountFrom"]);
                    this._fltAmountTo = Convert.ToSingle(drList["AmountTo"]);
                    this._dFrom = Convert.ToDateTime(drList["DateFrom"]);
                    this._dTo = Convert.ToDateTime(drList["DateTo"]);
                    this._fltFXFees_Discount = Convert.ToSingle(drList["FXFees_Discount"]);
                    this._fltFinishFXFees = Convert.ToSingle(drList["FXFees"]);
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
                _dtList = new DataTable("FXFees_List");
                dtCol = _dtList.Columns.Add("ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ServiceProvider_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AmountFrom", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AmountTo", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FXFees", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("DiscountDateFrom", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("DiscountDateTo", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("FXFees_Discount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishFXFees", Type.GetType("System.Single"));

                conn.Open();
                cmd = new SqlCommand("GetPackage_FXFees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ServiceProvider_ID", _iServiceProvider_ID));
                cmd.Parameters.Add(new SqlParameter("@Option_ID", _iOption_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["SPFF_ID"];                                              // ID - is SPFF_ID
                    this.dtRow["ServiceProvider_ID"] = drList["ServiceProvider_ID"];
                    this.dtRow["AmountFrom"] = drList["AmountFrom"];
                    this.dtRow["AmountTo"] = drList["AmountTo"];
                    this.dtRow["FXFees"] = drList["FeesPercent"];
                    this.dtRow["DiscountDateFrom"] = _dFrom.ToString("dd/MM/yyyy");
                    this.dtRow["DiscountDateTo"] = _dTo.ToString("dd/MM/yyyy");
                    this.dtRow["FXFees_Discount"] = 0;
                    this.dtRow["FinishFXFees"] = drList["FeesPercent"];
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();

                if (_bIncludeDiscount)
                {
                    cmd = new SqlCommand("GetContract_FXFees", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Contract_ID", _iContract_ID));
                    cmd.Parameters.Add(new SqlParameter("@Contract_Packages_ID", _iContract_Packages_ID));
                    drList = cmd.ExecuteReader();
                    while (drList.Read())
                    {
                        foundRows = _dtList.Select("ID=" + drList["SPFF_ID"]);
                        if (foundRows.Length > 0)
                        {
                            foundRows[0]["DiscountDateFrom"] = drList["DateFrom"];
                            foundRows[0]["DiscountDateTo"] = drList["DateTo"];
                            foundRows[0]["FXFees_Discount"] = drList["FXFees_Discount"];
                            foundRows[0]["FinishFXFees"] = drList["FXFees"];
                        }
                    }
                    drList.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList_Package_ID()
        {
            try
            {
                _dtList = new DataTable("FXFees_List");
                dtCol = _dtList.Columns.Add("ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ServiceProvider_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AmountFrom", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AmountTo", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FXFees", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("DiscountDateFrom", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("DiscountDateTo", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("FXFees_Discount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishFXFees", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("SPFF_ID", Type.GetType("System.Int32"));

                conn.Open();
                cmd = new SqlCommand("GetPackage_FXFees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ServiceProvider_ID", _iServiceProvider_ID));
                cmd.Parameters.Add(new SqlParameter("@Option_ID", _iOption_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = 0;                                                                             // ID = 0, because it's Package record YET
                    this.dtRow["ServiceProvider_ID"] = drList["ServiceProvider_ID"];
                    this.dtRow["AmountFrom"] = drList["AmountFrom"];
                    this.dtRow["AmountTo"] = drList["AmountTo"];
                    this.dtRow["FXFees"] = drList["FeesPercent"];
                    this.dtRow["DiscountDateFrom"] = _dFrom.ToString("dd/MM/yyyy");
                    this.dtRow["DiscountDateTo"] = _dTo.ToString("dd/MM/yyyy");
                    this.dtRow["FXFees_Discount"] = 0;
                    this.dtRow["FinishFXFees"] = drList["FeesPercent"];
                    this.dtRow["SPFF_ID"] = drList["SPFF_ID"];
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();

                if (_bIncludeDiscount)
                {
                    cmd = new SqlCommand("GetContract_FXFees", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Contract_ID", _iContract_ID));
                    cmd.Parameters.Add(new SqlParameter("@Contract_Packages_ID", _iContract_Packages_ID));
                    drList = cmd.ExecuteReader();
                    while (drList.Read())
                    {
                        foundRows = _dtList.Select("SPFF_ID=" + drList["SPFF_ID"]);
                        if (foundRows.Length > 0)
                        {
                            foundRows[0]["ID"] = drList["ID"];                                                       // ID - it's Clients record       
                            foundRows[0]["DiscountDateFrom"] = drList["DateFrom"];
                            foundRows[0]["DiscountDateTo"] = drList["DateTo"];
                            foundRows[0]["FXFees_Discount"] = drList["FXFees_Discount"];
                            foundRows[0]["FinishFXFees"] = drList["FXFees"];
                        }
                    }
                    drList.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList_Contract_ID()
        {
            try
            {
                _dtList = new DataTable("FXFees_List");
                dtCol = _dtList.Columns.Add("ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ServiceProvider_ID", Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AmountFrom", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AmountTo", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FXFees", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("DiscountDateFrom", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("DiscountDateTo", Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("FXFees_Discount", Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FinishFXFees", Type.GetType("System.Single"));

                conn.Open();
                cmd = new SqlCommand("GetContract_FXData", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Contract_ID", _iContract_ID));
                cmd.Parameters.Add(new SqlParameter("@AktionDate", _dAktionDate));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];                                                      // ID - is SPFF_ID
                    this.dtRow["ServiceProvider_ID"] = drList["ServiceProvider_ID"];
                    this.dtRow["AmountFrom"] = drList["AmountFrom"];
                    this.dtRow["AmountTo"] = drList["AmountTo"];
                    this.dtRow["FXFees"] = drList["FeesPercent"];
                    this.dtRow["DiscountDateFrom"] = _dFrom.ToString("dd/MM/yyyy");
                    this.dtRow["DiscountDateTo"] = _dTo.ToString("dd/MM/yyyy");
                    this.dtRow["FXFees_Discount"] = drList["FXFees_Discount"];
                    this.dtRow["FinishFXFees"] = drList["FXFees"];
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();

                if (_bIncludeDiscount)
                {
                    cmd = new SqlCommand("GetContract_FXFees", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Contract_ID", _iContract_ID));
                    cmd.Parameters.Add(new SqlParameter("@Contract_Packages_ID", _iContract_Packages_ID));
                    drList = cmd.ExecuteReader();
                    while (drList.Read())
                    {
                        foundRows = _dtList.Select("ID=" + drList["SPFF_ID"]);
                        if (foundRows.Length > 0)
                        {
                            foundRows[0]["DiscountDateFrom"] = drList["DateFrom"];
                            foundRows[0]["DiscountDateTo"] = drList["DateTo"];
                            foundRows[0]["FXFees_Discount"] = drList["FXFees_Discount"];
                            foundRows[0]["FinishFXFees"] = drList["FXFees"];
                        }
                    }
                    drList.Close();
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
                using (SqlCommand cmd = new SqlCommand("InsertClientsFXFees", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@SPFF_ID", SqlDbType.Int).Value = _iSPFF_ID;
                    cmd.Parameters.Add("@AmountFrom", SqlDbType.Float).Value = _fltAmountFrom;
                    cmd.Parameters.Add("@AmountTo", SqlDbType.Float).Value = _fltAmountTo;
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = _dFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = _dTo;
                    cmd.Parameters.Add("@FXFees_Discount", SqlDbType.Float).Value = _fltFXFees_Discount;
                    cmd.Parameters.Add("@FXFees", SqlDbType.Float).Value = _fltFinishFXFees;
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
                using (SqlCommand cmd = new SqlCommand("EditClientsFXFees", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@AmountFrom", SqlDbType.Float).Value = _fltAmountFrom;
                    cmd.Parameters.Add("@AmountTo", SqlDbType.Float).Value = _fltAmountTo;
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = _dFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = _dTo;
                    cmd.Parameters.Add("@FXFees_Discount", SqlDbType.Float).Value = _fltFXFees_Discount;
                    cmd.Parameters.Add("@FXFees", SqlDbType.Float).Value = _fltFinishFXFees;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ClientsFXFees";
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
        public int SPFF_ID { get { return this._iSPFF_ID; } set { this._iSPFF_ID = value; } }
        public float AmountFrom { get { return this._fltAmountFrom; } set { this._fltAmountFrom = value; } }
        public float AmountTo { get { return this._fltAmountTo; } set { this._fltAmountTo = value; } }
        public float FXFees { get { return this._fltFinishFXFees; } set { this._fltFinishFXFees = value; } }
        public DateTime DateFrom { get { return this._dFrom; } set { this._dFrom = value; } }
        public DateTime DateTo { get { return this._dTo; } set { this._dTo = value; } }
        public float FXFees_Discount { get { return this._fltFXFees_Discount; } set { this._fltFXFees_Discount = value; } }
        public float FinishFXFees { get { return this._fltFinishFXFees; } set { this._fltFinishFXFees = value; } }
        public bool IncludeDiscount { get { return this._bIncludeDiscount; } set { this._bIncludeDiscount = value; } }
        public int Option_ID { get { return this._iOption_ID; } set { this._iOption_ID = value; } }
        public int ServiceProvider_ID { get { return this._iServiceProvider_ID; } set { this._iServiceProvider_ID = value; } }
        public DateTime AktionDate { get { return this._dAktionDate; } set { this._dAktionDate = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}