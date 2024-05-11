using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsClientsLombardFees
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int _iRecord_ID;
        private int _iContract_ID;
        private int _iContract_Packages_ID;
        private int _iSPFF_ID;
        private float _sgAmountFrom;
        private float _sgAmountTo;
        private float _sgLombardFees;
        private DateTime _dFrom;
        private DateTime _dTo;
        private double _dblLombardFees_Discount;
        private double _dblFinishLombardFees;
        private float _sgMinimumFees_Discount;
        private float _sgMinimumFees;

        private int _iOption_ID;
        private int _iServiceProvider_ID;
        private DataTable _dtList;

        public clsClientsLombardFees()
        {
            this._iRecord_ID = 0;
            this._iContract_ID = 0;
            this._iContract_Packages_ID = 0;
            this._iSPFF_ID = 0;
            this._sgAmountFrom = 0;
            this._sgAmountTo = 0;
            this._sgLombardFees = 0;
            this._dFrom = Convert.ToDateTime("1900/01/01");
            this._dTo = Convert.ToDateTime("2070/12/31");
            this._dblLombardFees_Discount = 0;
            this._dblFinishLombardFees = 0;
            this._sgMinimumFees_Discount = 0;
            this._sgMinimumFees = 0;

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
                cmd.Parameters.Add(new SqlParameter("@Table", "ClientsLombardFees"));
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
                    this._sgAmountFrom = Convert.ToSingle(drList["AmountFrom"]);
                    this._sgAmountTo = Convert.ToSingle(drList["AmountTo"]);
                    this._sgLombardFees = Convert.ToSingle(drList["LombardFees"]);
                    this._dFrom = Convert.ToDateTime(drList["DateFrom"]);
                    this._dTo = Convert.ToDateTime(drList["DateTo"]);
                    this._dblLombardFees_Discount = Convert.ToDouble(drList["LombardFees_Discount"]);
                    this._dblFinishLombardFees = Convert.ToDouble(drList["LombardFees"]);
                    this._sgMinimumFees_Discount = Convert.ToSingle(drList["MinimumFees_Discount"]);
                    this._sgMinimumFees = Convert.ToSingle(drList["MinimumFees"]);
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
                _dtList = new DataTable("LombardFees_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));

                conn.Open();
                cmd = new SqlCommand("GetContract_LombardFees", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ServiceProvider_ID", _iServiceProvider_ID));
                cmd.Parameters.Add(new SqlParameter("@Option_ID", _iOption_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];                                                      
                    this.dtRow["Currency"] = drList["Currency"];
                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int xxxInsertRecord()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("InsertClientsLombardFees", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@SPFF_ID", SqlDbType.Int).Value = _iSPFF_ID;
                    cmd.Parameters.Add("@AmountFrom", SqlDbType.Float).Value = _sgAmountFrom;
                    cmd.Parameters.Add("@AmountTo", SqlDbType.Float).Value = _sgAmountTo;
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = _dFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = _dTo;
                    cmd.Parameters.Add("@LombardFees_Discount", SqlDbType.Float).Value = _dblLombardFees_Discount;
                    cmd.Parameters.Add("@LombardFees", SqlDbType.Float).Value = _dblFinishLombardFees;
                    cmd.Parameters.Add("@MinimumFees_Discount", SqlDbType.Float).Value = _sgMinimumFees_Discount;
                    cmd.Parameters.Add("@MinimumFees", SqlDbType.Float).Value = _sgMinimumFees;

                    cmd.ExecuteNonQuery();
                    _iRecord_ID = Convert.ToInt32(outParam.Value);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            return _iRecord_ID;
        }
        public void xxxEditRecord()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("EditClientsLombardFees", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@Contract_Packages_ID", SqlDbType.Int).Value = _iContract_Packages_ID;
                    cmd.Parameters.Add("@SPFF_ID", SqlDbType.Int).Value = _iSPFF_ID;
                    cmd.Parameters.Add("@AmountFrom", SqlDbType.Float).Value = _sgAmountFrom;
                    cmd.Parameters.Add("@AmountTo", SqlDbType.Float).Value = _sgAmountTo;
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = _dFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = _dTo;
                    cmd.Parameters.Add("@LombardFees_Discount", SqlDbType.Float).Value = _dblLombardFees_Discount;
                    cmd.Parameters.Add("@LombardFees", SqlDbType.Float).Value = _dblFinishLombardFees;
                    cmd.Parameters.Add("@MinimumFees_Discount", SqlDbType.Float).Value = _sgMinimumFees_Discount;
                    cmd.Parameters.Add("@MinimumFees", SqlDbType.Float).Value = _sgMinimumFees;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ClientsLombardFees";
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
        public float AmountFrom { get { return this._sgAmountFrom; } set { this._sgAmountFrom = value; } }
        public float AmountTo { get { return this._sgAmountTo; } set { this._sgAmountTo = value; } }
        public float LombardFees { get { return this._sgLombardFees; } set { this._sgLombardFees = value; } }
        public DateTime DateFrom { get { return this._dFrom; } set { this._dFrom = value; } }
        public DateTime DateTo { get { return this._dTo; } set { this._dTo = value; } }
        public double LombardFees_Discount { get { return this._dblLombardFees_Discount; } set { this._dblLombardFees_Discount = value; } }
        public double FinishLombardFees { get { return this._dblFinishLombardFees; } set { this._dblFinishLombardFees = value; } }
        public float MinimumFees_Discount { get { return this._sgMinimumFees_Discount; } set { this._sgMinimumFees_Discount = value; } }
        public float MinimumFees { get { return this._sgMinimumFees; } set { this._sgMinimumFees = value; } }
        public int Option_ID { get { return this._iOption_ID; } set { this._iOption_ID = value; } }
        public int ServiceProvider_ID { get { return this._iServiceProvider_ID; } set { this._iServiceProvider_ID = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}






