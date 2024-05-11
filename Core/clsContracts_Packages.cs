using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsContracts_Packages
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int _iRecord_ID;
        private int _iContract_ID;
        private int _iService_ID;
        private int _iCFP_ID;
        private DateTime _dStart;
        private DateTime _dFinish;
        private int _iProfile_ID;

        private string _sPackage_Title;
        private string _sContractNotes;
        private DataTable _dtList;
        public clsContracts_Packages()
        {
            this._iRecord_ID = 0;
            this._iContract_ID = 0;
            this._iService_ID = 0;
            this._iCFP_ID = 0;
            this._dStart = Convert.ToDateTime("1900/01/01");
            this._dFinish = Convert.ToDateTime("1900/01/01");
            this._iProfile_ID = 0;
            this._sPackage_Title = "";
            this._sContractNotes = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetContract_Packages", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Contract_ID", _iContract_ID));
                cmd.Parameters.Add(new SqlParameter("@Record_ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iContract_ID = Convert.ToInt32(drList["Contract_ID"]);
                    this._iService_ID = Convert.ToInt32(drList["Service_ID"]);
                    this._iCFP_ID = Convert.ToInt32(drList["CFP_ID"]);
                    this._sPackage_Title = drList["Package_Title"] + "";
                    this._dStart = Convert.ToDateTime(drList["DateStart"]);
                    this._dFinish = Convert.ToDateTime(drList["DateFinish"]);
                    this._iProfile_ID = Convert.ToInt32(drList["Profile_ID"]);
                    this._sContractNotes = drList["Notes"] + "";
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable("Contract_Packages_List");
            dtCol = _dtList.Columns.Add("ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Contract_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Service_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("DateFrom", Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("DateTo", Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("CFP_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("DateStart", Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("DateFinish", Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("Profile_ID", Type.GetType("System.Int32"));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Contracts_Packages"));
                cmd.Parameters.Add(new SqlParameter("@Col", "CFP_ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iCFP_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));

                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = this._dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];
                    this.dtRow["Contract_ID"] = drList["Contract_ID"];
                    this.dtRow["Service_ID"] = drList["Service_ID"];
                    this.dtRow["CFP_ID"] = drList["CFP_ID"];
                    this.dtRow["DateStart"] = drList["DateStart"];
                    this.dtRow["DateFinish"] = drList["DateFinish"];
                    this.dtRow["Profile_ID"] = drList["Profile_ID"];
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
                using (SqlCommand cmd = new SqlCommand("InsertContract_Packages", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = this._iContract_ID;
                    cmd.Parameters.Add("@Service_ID", SqlDbType.Int).Value = this._iService_ID;
                    cmd.Parameters.Add("@CFP_ID", SqlDbType.Int).Value = this._iCFP_ID;
                    cmd.Parameters.Add("@DateStart", SqlDbType.DateTime).Value = this._dStart;
                    cmd.Parameters.Add("@DateFinish", SqlDbType.DateTime).Value = this._dFinish;
                    cmd.Parameters.Add("@Profile_ID", SqlDbType.Int).Value = this._iProfile_ID;             
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
                using (SqlCommand cmd = new SqlCommand("EditContract_Packages", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = this._iRecord_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = this._iContract_ID;
                    cmd.Parameters.Add("@Service_ID", SqlDbType.Int).Value = this._iService_ID;
                    cmd.Parameters.Add("@CFP_ID", SqlDbType.Int).Value = this._iCFP_ID;
                    cmd.Parameters.Add("@DateStart", SqlDbType.DateTime).Value = this._dStart;
                    cmd.Parameters.Add("@DateFinish", SqlDbType.DateTime).Value = this._dFinish;
                    cmd.Parameters.Add("@Profile_ID", SqlDbType.Int).Value = this._iProfile_ID;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Contracts_Packages";
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
        public int Service_ID { get { return this._iService_ID; } set { this._iService_ID = value; } }
        public int CFP_ID { get { return this._iCFP_ID; } set { this._iCFP_ID = value; } }
        public string Package_Title { get { return this._sPackage_Title; } set { this._sPackage_Title = value; } }
        public DateTime DateStart { get { return this._dStart; } set { this._dStart = value; } }
        public DateTime DateFinish { get { return this._dFinish; } set { this._dFinish = value; } }
        public int Profile_ID { get { return this._iProfile_ID; } set { this._iProfile_ID = value; } }
        public string ContractNotes { get { return this._sContractNotes; } set { this._sContractNotes = value; } }
        public DataTable List  { get { return _dtList; } set { _dtList = value; } }
    }
}
