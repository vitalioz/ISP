using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsAccountingTrx
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int _iRecord_ID;
        private DateTime _dTrxDate;
        private DateTime _dValeur;
        private DateTime _dDateIns;
        private int _iGAP_ID;
        private float _fltDebit;
        private float _fltCredit;
        private string _sReferenceNo;
        private string _sDescription;

        private DataTable _dtList;

        public clsAccountingTrx()
        {
            this._iRecord_ID = 0;
            this._dTrxDate = Convert.ToDateTime("1900/01/01");
            this._dValeur = Convert.ToDateTime("1900/01/01");
            this._dDateIns = Convert.ToDateTime("1900/01/01");
            this._iGAP_ID = 0;
            this._fltDebit = 0;
            this._fltCredit = 0;
            this._sReferenceNo = "";
            this._sDescription = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "AccountingTrx"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._dTrxDate = Convert.ToDateTime(drList["TrxDate"]);
                    this._dValeur = Convert.ToDateTime(drList["Valeur"]);
                    this._dDateIns = Convert.ToDateTime(drList["DateIns"]);
                    this._iGAP_ID = Convert.ToInt32(drList["GAP_ID"]);
                    this._fltDebit = Convert.ToSingle(drList["Debit"]);
                    this._fltCredit = Convert.ToSingle(drList["Credit"]);
                    this._sReferenceNo = drList["ReferenceNo"] + "";
                    this._sDescription = drList["Description"] + "";
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            int i = 0;
            try
            {
                _dtList = new DataTable("AccountingTrx_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("AA", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("TrxDate", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Valeur", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DateIns", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("GAP_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Debit", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Credit", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("ReferenceNo", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Description", System.Type.GetType("System.String"));

                conn.Open();
                cmd = new SqlCommand("GetAccountingTrx_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dDateIns.Date));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dDateIns.Date.AddDays(1).AddSeconds(-1)));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    i = i + 1;
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["AA"] = i;
                    dtRow["TrxDate"] = drList["TrxDate"];
                    dtRow["Valeur"] = drList["Valeur"];
                    dtRow["DateIns"] = drList["DateIns"];
                    dtRow["GAP_ID"] = drList["GAP_ID"];
                    dtRow["Code"] = drList["Code"];
                    dtRow["Title"] = drList["Title"];
                    dtRow["Debit"] = drList["Debit"];
                    dtRow["Credit"] = drList["Credit"];                    
                    dtRow["ReferenceNo"] = drList["ReferenceNo"];
                    dtRow["Description"] = drList["Description"];
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
                using (cmd = new SqlCommand("InsertAccountingTrx", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@TrxDate", SqlDbType.DateTime).Value = _dTrxDate;
                    cmd.Parameters.Add("@Valeur", SqlDbType.DateTime).Value = _dValeur;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = _dDateIns;
                    cmd.Parameters.Add("@GAP_ID", SqlDbType.Int).Value = _iGAP_ID;
                    cmd.Parameters.Add("@Debit", SqlDbType.Float).Value = _fltDebit;
                    cmd.Parameters.Add("@Credit", SqlDbType.Float).Value = _fltCredit;
                    cmd.Parameters.Add("@ReferenceNo", SqlDbType.NVarChar, 40).Value = _sReferenceNo;
                    cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 100).Value = _sDescription;
                    cmd.ExecuteNonQuery();
                    _iRecord_ID = Convert.ToInt32(outParam.Value);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            return _iRecord_ID;
        }
        public int EditRecord()
        {
            try
            {
                conn.Open();
                using (cmd = new SqlCommand("EditAccountingTrx", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@TrxDate", SqlDbType.DateTime).Value = _dTrxDate;
                    cmd.Parameters.Add("@Valeur", SqlDbType.DateTime).Value = _dValeur;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = _dDateIns;
                    cmd.Parameters.Add("@GAP_ID", SqlDbType.Int).Value = _iGAP_ID;
                    cmd.Parameters.Add("@Debit", SqlDbType.Float).Value = _fltDebit;
                    cmd.Parameters.Add("@Credit", SqlDbType.Float).Value = _fltCredit;
                    cmd.Parameters.Add("@ReferenceNo", SqlDbType.NVarChar, 40).Value = _sReferenceNo;
                    cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 100).Value = _sDescription;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            return _iRecord_ID;
        }
        public void DeleteRecord()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "AccountingTrx";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public DateTime TrxDate { get { return this._dTrxDate; } set { this._dTrxDate = value; } }
        public DateTime Valeur { get { return this._dValeur; } set { this._dValeur = value; } }
        public DateTime DateIns { get { return this._dDateIns; } set { this._dDateIns = value; } }
        public int GAP_ID { get { return this._iGAP_ID; } set { this._iGAP_ID = value; } }
        public float Debit { get { return this._fltDebit; } set { this._fltDebit = value; } }
        public float Credit { get { return this._fltCredit; } set { this._fltCredit = value; } }
        public string ReferenceNo { get { return this._sReferenceNo; } set { this._sReferenceNo = value; } }
        public string Description { get { return this._sDescription; } set { this._sDescription = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}
