using System;                    //OK
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsContracts_Details_Packages
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int       _iRecord_ID;
        private DateTime  _dFrom;
        private DateTime  _dTo;
        private int       _iContract_ID;
        private int       _iContracts_Details_ID;
        private int       _iContracts_Packages_ID;
        private string    _sNotes;
        private DataTable _dtList;
        public clsContracts_Details_Packages()
        {
            this._iRecord_ID = 0;
            this._dFrom = Convert.ToDateTime("1900/01/01");
            this._dTo = Convert.ToDateTime("1900/01/01");
            this._iContract_ID = 0;
            this._iContracts_Details_ID = 0;
            this._iContracts_Packages_ID = 0;
            this._sNotes = "";
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
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._dFrom = Convert.ToDateTime(drList["DateFrom"]);
                    this._dTo = Convert.ToDateTime(drList["DateTo"]);
                    this._iContract_ID = Convert.ToInt32(drList["Contract_ID"]);
                    this._iContracts_Details_ID = Convert.ToInt32(drList["Contracts_Details_ID"]);
                    this._iContracts_Packages_ID = Convert.ToInt32(drList["Contracts_Packages_ID"]);
                    this._sNotes = drList["Notes"] + "";
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetRecord_Contract_ID()
        {
            this._iRecord_ID = 0;
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetContract_Details_Packages", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Contract_ID", _iContract_ID));
                cmd.Parameters.Add(new SqlParameter("@Contract_Details_ID", _iContracts_Details_ID));
                cmd.Parameters.Add(new SqlParameter("@Contract_Packages_ID", _iContracts_Packages_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._dFrom = Convert.ToDateTime(drList["DateFrom"]);
                    this._dTo = Convert.ToDateTime(drList["DateTo"]);
                    this._iContract_ID = Convert.ToInt32(drList["Contract_ID"]);
                    this._iContracts_Details_ID = Convert.ToInt32(drList["Contracts_Details_ID"]);
                    this._iContracts_Packages_ID = Convert.ToInt32(drList["Contracts_Packages_ID"]);
                    this._sNotes = drList["Notes"] + "";
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable("Contract_Details_Packages_List");
            dtCol = _dtList.Columns.Add("ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("DateFrom", Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("DateTo", Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("Contract_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Contracts_Details_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Contracts_Packages_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Notes", Type.GetType("System.String"));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Contracts_Details_Packages"));
                cmd.Parameters.Add(new SqlParameter("@Col", "Contract_ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iContract_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", "ID"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = this._dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];
                    this.dtRow["DateFrom"] = drList["DateFrom"];
                    this.dtRow["DateTo"] = drList["DateTo"];
                    this.dtRow["Contract_ID"] = drList["Contract_ID"];
                    this.dtRow["Contracts_Details_ID"] = drList["Contracts_Details_ID"];
                    this.dtRow["Contracts_Packages_ID"] = drList["Contracts_Packages_ID"];
                    this.dtRow["Notes"] = drList["Notes"] + "";
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
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = this._dFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = this._dTo;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = this._iContract_ID;
                    cmd.Parameters.Add("@Contracts_Details_ID", SqlDbType.Int).Value = this._iContracts_Details_ID;
                    cmd.Parameters.Add("@Contracts_Packages_ID", SqlDbType.Int).Value = this._iContracts_Packages_ID;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 100).Value = this._sNotes;
                    cmd.ExecuteNonQuery();
                    _iRecord_ID = Convert.ToInt32(outParam.Value);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            Edit_DateTo(_iContract_ID, _iRecord_ID);

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
                    cmd.Parameters.Add("@DateFrom", SqlDbType.DateTime).Value = this._dFrom;
                    cmd.Parameters.Add("@DateTo", SqlDbType.DateTime).Value = this._dTo;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = this._iContract_ID;
                    cmd.Parameters.Add("@Contracts_Details_ID", SqlDbType.Int).Value = this._iContracts_Details_ID;
                    cmd.Parameters.Add("@Contracts_Packages_ID", SqlDbType.Int).Value = this._iContracts_Packages_ID;
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 100).Value = this._sNotes;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            //Edit_DateTo(_iContract_ID);
        }
        private void Edit_DateTo(int iContract_ID, int iRecord_ID)
        {
            SqlConnection conn = new SqlConnection(Global.connStr);
            SqlConnection conn1 = new SqlConnection(Global.connStr);
            SqlDataReader drList;
            DateTime dLast;

            dLast = this._dFrom;       // Convert.ToDateTime("2070/12/31");
            dLast = dLast.AddDays(1);

            conn.Open();
            conn1.Open();
            try
            {
                cmd = new SqlCommand("SELECT * FROM dbo.Contracts_Details_Packages WHERE Contract_ID = " + iContract_ID + 
                                     " ORDER BY DateFrom DESC, DateTo DESC, ID DESC", conn);
                cmd.CommandType = CommandType.Text;
                drList = cmd.ExecuteReader();

                while (drList.Read())
                {
                    dLast = dLast.AddDays(-1);
                    if (Convert.ToDateTime(drList["DateFrom"]) > dLast) dLast = Convert.ToDateTime(drList["DateFrom"]);

                    if (Convert.ToInt32(drList["ID"]) != iRecord_ID)
                    {
                        cmd = new SqlCommand("UPDATE Contracts_Details_Packages SET DateTo = '" + dLast.ToString("yyyy/MM/dd") + "' WHERE ID = " + drList["ID"], conn1);
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }

                    dLast = Convert.ToDateTime(drList["DateFrom"]);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); conn1.Close(); }
        }
        public void DeleteRecord()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Contracts_Details_Packages";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public DateTime DateFrom { get { return this._dFrom; } set { this._dFrom = value; } }
        public DateTime DateTo { get { return this._dTo; } set { this._dTo = value; } }
        public int Contract_ID { get { return this._iContract_ID; } set { this._iContract_ID = value; } }
        public int Contracts_Details_ID { get { return this._iContracts_Details_ID; } set { this._iContracts_Details_ID = value; } }
        public int Contracts_Packages_ID { get { return this._iContracts_Packages_ID; } set { this._iContracts_Packages_ID = value; } }
        public string Notes { get { return this._sNotes; } set { this._sNotes = value; } }
        public DataTable List  { get { return _dtList; }  set { _dtList = value; }
        }
    }
}
