using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsClientsBlackList
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;

        private int _iRecord_ID;
        private string _sSurname;
        private string _sFirstname;
        private string _sFirstnameFather;
        private string _sFirstnameMother;
        private string _sAddress;
        private string _sDOY;
        private string _sAFM;
        private string _sADT;
        private DateTime _dDoB;
        private string _sBornPlace;
        private string _sIssuedDoc;
        private string _sIssuedNotes;
        private string _sIssuedActions;
        private int _iFound;

        private DataTable _dtList;
        public clsClientsBlackList()
        {
            this._iRecord_ID = 0;
            this._sSurname = "";
            this._sFirstname = "";
            this._sFirstnameFather = "";
            this._sFirstnameMother = "";
            this._sAddress = "";
            this._sDOY = "";
            this._sAFM = "";
            this._sADT = "";
            this._dDoB = Convert.ToDateTime("1900/01/01");
            this._sBornPlace = "";
            this._sIssuedDoc = "";
            this._sIssuedNotes = "";
            this._sIssuedActions = "";
            this._iFound = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "ClientsBlackList"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", _iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._sSurname = drList["Surname"] + "";
                    this._sFirstname = drList["Firstname"] + "";
                    this._sFirstnameFather = drList["FirstnameFather"] + "";
                    this._sFirstnameMother = drList["FirstnameMother"] + "";
                    this._sAddress = drList["Address"] + "";
                    this._sDOY = drList["DOY"] + "";
                    this._sAFM = drList["AFM"] + "";
                    this._sADT = drList["ADT"] + "";
                    this._dDoB = Convert.ToDateTime(drList["DoB"]);
                    this._sBornPlace = drList["BornPlace"] + "";
                    this._sIssuedDoc = drList["IssuedDoc"] + "";
                    this._sIssuedNotes = drList["IssuedNotes"] + "";
                    this._sIssuedActions = drList["IssuedActions"] + "";
                    this._iFound = Convert.ToInt32(drList["Found"]);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("Surname", typeof(string));
            _dtList.Columns.Add("Firstname", typeof(string));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "ClientsBlackList"));
                cmd.Parameters.Add(new SqlParameter("@Col", ""));
                cmd.Parameters.Add(new SqlParameter("@Value", ""));
                cmd.Parameters.Add(new SqlParameter("@Order", "Surname, Firstname"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["Surname"], drList["Firstname"]);
                }
                _dtList.Load(drList);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetCheckList()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("Surname", typeof(string));
            _dtList.Columns.Add("Firstname", typeof(string));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetCheckBlackList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", "0"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["Surname"], drList["Firstname"]);
                }
                _dtList.Load(drList);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int InsertRecord()
        {
            try
            {
                conn.Open();
                using (cmd = new SqlCommand("InsertClientBlackList", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Surname", SqlDbType.NVarChar, 100).Value = _sSurname.Trim();
                    cmd.Parameters.Add("@Firstname", SqlDbType.NVarChar, 40).Value = _sFirstname.Trim();
                    cmd.Parameters.Add("@FirstnameFather", SqlDbType.NVarChar, 40).Value = _sFirstnameFather.Trim();
                    cmd.Parameters.Add("@FirstnameMother", SqlDbType.Text).Value = _sFirstnameMother.Trim();
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 80).Value = _sAddress.Trim();
                    cmd.Parameters.Add("@DOY", SqlDbType.NVarChar, 30).Value = _sDOY.Trim();
                    cmd.Parameters.Add("@AFM", SqlDbType.NVarChar, 12).Value = _sAFM.Trim();
                    cmd.Parameters.Add("@ADT", SqlDbType.NVarChar, 30).Value = _sADT.Trim();
                    cmd.Parameters.Add("@DoB", SqlDbType.DateTime).Value = _dDoB;
                    cmd.Parameters.Add("@BornPlace", SqlDbType.NVarChar, 20).Value = _sBornPlace.Trim();
                    cmd.Parameters.Add("@IssuedDoc", SqlDbType.NVarChar, 1500).Value = _sIssuedDoc.Trim();
                    cmd.Parameters.Add("@IssuedNotes", SqlDbType.NVarChar, 1500).Value = _sIssuedNotes.Trim();
                    cmd.Parameters.Add("@IssuedActions", SqlDbType.NVarChar, 1500).Value = _sIssuedActions.Trim();
                    cmd.Parameters.Add("@Found", SqlDbType.Int).Value = _iFound;
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
                using (cmd = new SqlCommand("EditClientBlackList", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Surname", SqlDbType.NVarChar, 100).Value = _sSurname.Trim();
                    cmd.Parameters.Add("@Firstname", SqlDbType.NVarChar, 40).Value = _sFirstname.Trim();
                    cmd.Parameters.Add("@FirstnameFather", SqlDbType.NVarChar, 40).Value = _sFirstnameFather.Trim();
                    cmd.Parameters.Add("@FirstnameMother", SqlDbType.Text).Value = _sFirstnameMother.Trim();
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 80).Value = _sAddress.Trim();
                    cmd.Parameters.Add("@DOY", SqlDbType.NVarChar, 30).Value = _sDOY.Trim();
                    cmd.Parameters.Add("@AFM", SqlDbType.NVarChar, 12).Value = _sAFM.Trim();
                    cmd.Parameters.Add("@ADT", SqlDbType.NVarChar, 30).Value = _sADT.Trim();
                    cmd.Parameters.Add("@DoB", SqlDbType.DateTime).Value = _dDoB;
                    cmd.Parameters.Add("@BornPlace", SqlDbType.NVarChar, 20).Value = _sBornPlace.Trim();
                    cmd.Parameters.Add("@IssuedDoc", SqlDbType.NVarChar, 1500).Value = _sIssuedDoc.Trim();
                    cmd.Parameters.Add("@IssuedNotes", SqlDbType.NVarChar, 1500).Value = _sIssuedNotes.Trim();
                    cmd.Parameters.Add("@IssuedActions", SqlDbType.NVarChar, 1500).Value = _sIssuedActions.Trim();
                    cmd.Parameters.Add("@Found", SqlDbType.Int).Value = _iFound;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ClientsBlackList";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public string Surname { get { return _sSurname; } set { _sSurname = value; } }
        public string Firstname { get { return _sFirstname; } set { _sFirstname = value; } }
        public string FirstnameFather { get { return _sFirstnameFather; } set { _sFirstnameFather = value; } }
        public string FirstnameMother { get { return _sFirstnameMother; } set { _sFirstnameMother = value; } }
        public string Address { get { return _sAddress; } set { _sAddress = value; } }
        public string DOY { get { return _sDOY; } set { _sDOY = value; } }
        public string AFM { get { return _sAFM; } set { _sAFM = value; } }
        public string ADT { get { return _sADT; } set { _sADT = value; } }
        public DateTime DoB { get { return this._dDoB; } set { this._dDoB = value; } }
        public string BornPlace { get { return _sBornPlace; } set { _sBornPlace = value; } }
        public string IssuedDoc { get { return _sIssuedDoc; } set { _sIssuedDoc = value; } }
        public string IssuedNotes { get { return _sIssuedNotes; } set { _sIssuedNotes = value; } }
        public string IssuedActions { get { return _sIssuedActions; } set { _sIssuedActions = value; } }
        public int Found { get { return this._iFound; } set { this._iFound = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
