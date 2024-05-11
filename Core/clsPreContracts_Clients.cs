using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsPreContracts_Clients
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int      _iRecord_ID;
        private int      _iPreContract_ID;
        private int      _iClient_ID;
        private string   _sSurname;
        private string   _sFirstname;
        private DateTime _dDoB;
        private string   _sAFM;
        private int      _iGuardian_ID;
        private int      _iConfirmQuestionnarie;
        private int      _iConfirmRisks;
        private int      _iConfirmPriceList;
        private int      _iConfirmTerms;
        private string   _sUploadFile1;
        private string   _sUploadFile2;
        private string   _sUploadFile3;
        private string   _sUploadFile4;
        private string   _sUploadFile5;
        private string   _sUploadFile6_1;
        private string   _sUploadFile6_2;
        private string   _sUploadFile6_3;
        private string   _sUploadFile6_4;
        private string   _sUploadFile6_5;
        private string   _sUploadFile6_6;
        private string   _sUploadFile7;
        private string   _sUploadFile8;
        private int      _iStatus;
        private DataTable _dtList;

        public clsPreContracts_Clients()
        {
            this._iRecord_ID = 0;
            this._iPreContract_ID = 0;
            this._iClient_ID = 0;
            this._sSurname = "";
            this._sFirstname = "";
            this._dDoB = Convert.ToDateTime("1900/01/01");
            this._sAFM = "";
            this._iGuardian_ID = 0;
            this._iConfirmQuestionnarie = 0;
            this._iConfirmRisks = 0;
            this._iConfirmPriceList = 0;
            this._iConfirmTerms = 0;
            this._sUploadFile1 = "";
            this._sUploadFile2 = "";
            this._sUploadFile3 = "";
            this._sUploadFile4 = "";
            this._sUploadFile5 = "";
            this._sUploadFile6_1 = "";
            this._sUploadFile6_2 = "";
            this._sUploadFile6_3 = "";
            this._sUploadFile6_4 = "";
            this._sUploadFile6_5 = "";
            this._sUploadFile6_6 = "";
            this._sUploadFile7 = "";
            this._sUploadFile8 = "";
            this._iStatus = 0;
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "PreContracts_Clients"));
                cmd.Parameters.Add(new SqlParameter("@Col", ""));
                cmd.Parameters.Add(new SqlParameter("@Value", ""));
                cmd.Parameters.Add(new SqlParameter("@Order", "Title"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = 0;
                    this._iPreContract_ID = 0;
                    this._iClient_ID = 0;
                    this._sSurname = "";
                    this._sFirstname = "";
                    this._dDoB = Convert.ToDateTime("1900/01/01");
                    this._sAFM = "";
                    this._iGuardian_ID = 0;
                    this._iConfirmQuestionnarie = 0;
                    this._iConfirmRisks = 0;
                    this._iConfirmPriceList = 0;
                    this._iConfirmTerms = 0;
                    this._sUploadFile1 = "";
                    this._sUploadFile2 = "";
                    this._sUploadFile3 = "";
                    this._sUploadFile4 = "";
                    this._sUploadFile5 = "";
                    this._sUploadFile6_1 = "";
                    this._sUploadFile6_2 = "";
                    this._sUploadFile6_3 = "";
                    this._sUploadFile6_4 = "";
                    this._sUploadFile6_5 = "";
                    this._sUploadFile6_6 = "";
                    this._sUploadFile7 = "";
                    this._sUploadFile8 = "";
                    this._iStatus = 0;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable("PreContract_Clients_List");
            dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("PreContract_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Surname", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Firstname", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("DoB", System.Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("AFM", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Status_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("EMail", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Guardian_ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Guardian_Title", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Guardian_EMail", System.Type.GetType("System.String"));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetPreContracts_Clients_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PreContract_ID", _iPreContract_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = this._dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["PreContract_ID"] = drList["PreContract_ID"];
                    dtRow["Client_ID"] = drList["Client_ID"];
                    dtRow["Surname"] = drList["Surname"] + "";
                    dtRow["Firstname"] = drList["Firstname"] + "";
                    dtRow["DoB"] = drList["DoB"] + "";
                    dtRow["AFM"] = drList["AFM"] + "";
                    dtRow["EMail"] = drList["EMail"] + "";
                    dtRow["Guardian_ID"] = drList["Guardian_ID"];
                    dtRow["Guardian_Title"] = (drList["Guardian_Surname"] + " " + drList["Guardian_Firstname"]).Trim();
                    dtRow["Guardian_EMail"] = drList["Guardian_EMail"] + "";
                    dtRow["Status"] = drList["Status"];
                    switch (drList["Status"])
                    {
                        case 0:
                            dtRow["Status_Title"] = "Θα σταλεί πρόσκληση";
                            break;
                        case 1:
                            dtRow["Status_Title"] = "Περιμένουμε απάντηση";
                            break;
                            case 2:
                            dtRow["Status_Title"] = "OK";
                            break;
                    }
                    this._dtList.Rows.Add(dtRow);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList_ClientID()
        {
            _dtList = new DataTable("PreContract_Clients_List");
            dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("MasterFullName", System.Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int32"));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetPreContract_Client_ID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Client_ID", _iClient_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = this._dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["MasterFullName"] = (drList["MasterSurname"] + " " + drList["MasterFirstname"]).Trim();
                    dtRow["Status"] = drList["Status"];
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
                using (SqlCommand cmd = new SqlCommand("InsertPreContracts_Clients", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PreContract_ID", SqlDbType.Int).Value = this._iPreContract_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = this._iClient_ID;
                    cmd.Parameters.Add("@Surname", SqlDbType.NVarChar, 100).Value = this._sSurname;
                    cmd.Parameters.Add("@Firstname", SqlDbType.NVarChar, 40).Value = this._sFirstname;
                    cmd.Parameters.Add("@DoB", SqlDbType.DateTime).Value = this._dDoB;
                    cmd.Parameters.Add("@AFM", SqlDbType.NVarChar, 20).Value = this._sAFM;
                    cmd.Parameters.Add("@Guardian_ID", SqlDbType.Int).Value = this._iGuardian_ID;
                    cmd.Parameters.Add("@ConfirmQuestionnarie", SqlDbType.Int).Value = this._iConfirmQuestionnarie;
                    cmd.Parameters.Add("@ConfirmRisks", SqlDbType.Int).Value = this._iConfirmRisks;
                    cmd.Parameters.Add("@ConfirmPriceList", SqlDbType.Int).Value = this._iConfirmPriceList;
                    cmd.Parameters.Add("@ConfirmTerms", SqlDbType.Int).Value = this._iConfirmTerms;
                    cmd.Parameters.Add("@UploadFile1", SqlDbType.NVarChar, 100).Value = this._sUploadFile1;
                    cmd.Parameters.Add("@UploadFile2", SqlDbType.NVarChar, 100).Value = this._sUploadFile2;
                    cmd.Parameters.Add("@UploadFile3", SqlDbType.NVarChar, 100).Value = this._sUploadFile3;
                    cmd.Parameters.Add("@UploadFile4", SqlDbType.NVarChar, 100).Value = this._sUploadFile4;
                    cmd.Parameters.Add("@UploadFile5", SqlDbType.NVarChar, 100).Value = this._sUploadFile5;
                    cmd.Parameters.Add("@UploadFile6_1", SqlDbType.NVarChar, 100).Value = this._sUploadFile6_1;
                    cmd.Parameters.Add("@UploadFile6_2", SqlDbType.NVarChar, 100).Value = this._sUploadFile6_2;
                    cmd.Parameters.Add("@UploadFile6_3", SqlDbType.NVarChar, 100).Value = this._sUploadFile6_3;
                    cmd.Parameters.Add("@UploadFile6_4", SqlDbType.NVarChar, 100).Value = this._sUploadFile6_4;
                    cmd.Parameters.Add("@UploadFile6_5", SqlDbType.NVarChar, 100).Value = this._sUploadFile6_5;
                    cmd.Parameters.Add("@UploadFile6_6", SqlDbType.NVarChar, 100).Value = this._sUploadFile6_6;
                    cmd.Parameters.Add("@UploadFile7", SqlDbType.NVarChar, 100).Value = this._sUploadFile7;
                    cmd.Parameters.Add("@UploadFile8", SqlDbType.NVarChar, 100).Value = this._sUploadFile8;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = this._iStatus;
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
                using (SqlCommand cmd = new SqlCommand("EditPreContracts_Clients", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = this._iRecord_ID;
                    cmd.Parameters.Add("@PreContract_ID", SqlDbType.Int).Value = this._iPreContract_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = this._iClient_ID;
                    cmd.Parameters.Add("@Surname", SqlDbType.NVarChar, 100).Value = this._sSurname;
                    cmd.Parameters.Add("@Firstname", SqlDbType.NVarChar, 40).Value = this._sFirstname;
                    cmd.Parameters.Add("@DoB", SqlDbType.DateTime).Value = this._dDoB;
                    cmd.Parameters.Add("@AFM", SqlDbType.NVarChar, 20).Value = this._sAFM;
                    cmd.Parameters.Add("@Guardian_ID", SqlDbType.Int).Value = this._iGuardian_ID;
                    cmd.Parameters.Add("@ConfirmQuestionnarie", SqlDbType.Int).Value = this._iConfirmQuestionnarie;
                    cmd.Parameters.Add("@ConfirmRisks", SqlDbType.Int).Value = this._iConfirmRisks;
                    cmd.Parameters.Add("@ConfirmPriceList", SqlDbType.Int).Value = this._iConfirmPriceList;
                    cmd.Parameters.Add("@ConfirmTerms", SqlDbType.Int).Value = this._iConfirmTerms;
                    cmd.Parameters.Add("@UploadFile1", SqlDbType.NVarChar, 100).Value = this._sUploadFile1;
                    cmd.Parameters.Add("@UploadFile2", SqlDbType.NVarChar, 100).Value = this._sUploadFile2;
                    cmd.Parameters.Add("@UploadFile3", SqlDbType.NVarChar, 100).Value = this._sUploadFile3;
                    cmd.Parameters.Add("@UploadFile4", SqlDbType.NVarChar, 100).Value = this._sUploadFile4;
                    cmd.Parameters.Add("@UploadFile5", SqlDbType.NVarChar, 100).Value = this._sUploadFile5;
                    cmd.Parameters.Add("@UploadFile6_1", SqlDbType.NVarChar, 100).Value = this._sUploadFile6_1;
                    cmd.Parameters.Add("@UploadFile6_2", SqlDbType.NVarChar, 100).Value = this._sUploadFile6_2;
                    cmd.Parameters.Add("@UploadFile6_3", SqlDbType.NVarChar, 100).Value = this._sUploadFile6_3;
                    cmd.Parameters.Add("@UploadFile6_4", SqlDbType.NVarChar, 100).Value = this._sUploadFile6_4;
                    cmd.Parameters.Add("@UploadFile6_5", SqlDbType.NVarChar, 100).Value = this._sUploadFile6_5;
                    cmd.Parameters.Add("@UploadFile6_6", SqlDbType.NVarChar, 100).Value = this._sUploadFile6_6;
                    cmd.Parameters.Add("@UploadFile7", SqlDbType.NVarChar, 100).Value = this._sUploadFile7;
                    cmd.Parameters.Add("@UploadFile8", SqlDbType.NVarChar, 100).Value = this._sUploadFile8;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = this._iStatus;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "PreContracts_Clients";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return _iRecord_ID; } set { _iRecord_ID = value; } }
        public int PreContract_ID { get { return _iPreContract_ID; } set { _iPreContract_ID = value; } }
        public int Client_ID { get { return _iClient_ID; } set { _iClient_ID = value; } }
        public string Surname { get { return _sSurname; } set { _sSurname = value; } }
        public string Firstname { get { return _sFirstname; } set { _sFirstname = value; } }
        public DateTime DoB { get { return _dDoB; } set { _dDoB = value; } }
        public string AFM { get { return _sAFM; } set { _sAFM = value; } }
        public int Guardian_ID { get { return _iGuardian_ID; } set { _iGuardian_ID = value; } }
        public int ConfirmQuestionnarie { get { return _iConfirmQuestionnarie; } set { _iConfirmQuestionnarie = value; } }
        public int ConfirmRisks { get { return _iConfirmRisks; } set { _iConfirmRisks = value; } }
        public int ConfirmPriceList { get { return _iConfirmPriceList; } set { _iConfirmPriceList = value; } }
        public int ConfirmTerms { get { return _iConfirmTerms; } set { _iConfirmTerms = value; } }
        public string UploadFile1 { get { return _sUploadFile1; } set { _sUploadFile1 = value; } }
        public string UploadFile2 { get { return _sUploadFile2; } set { _sUploadFile2 = value; } }
        public string UploadFile3 { get { return _sUploadFile3; } set { _sUploadFile3 = value; } }
        public string UploadFile4 { get { return _sUploadFile4; } set { _sUploadFile4 = value; } }
        public string UploadFile5 { get { return _sUploadFile5; } set { _sUploadFile5 = value; } }
        public string UploadFile6_1 { get { return _sUploadFile6_1; } set { _sUploadFile6_1 = value; } }
        public string UploadFile6_2 { get { return _sUploadFile6_2; } set { _sUploadFile6_2 = value; } }
        public string UploadFile6_3 { get { return _sUploadFile6_3; } set { _sUploadFile6_3 = value; } }
        public string UploadFile6_4 { get { return _sUploadFile6_4; } set { _sUploadFile6_4 = value; } }
        public string UploadFile6_5 { get { return _sUploadFile6_5; } set { _sUploadFile6_5 = value; } }
        public string UploadFile6_6 { get { return _sUploadFile6_6; } set { _sUploadFile6_6 = value; } }
        public string UploadFile7 { get { return _sUploadFile7; } set { _sUploadFile7 = value; } }
        public string UploadFile8 { get { return _sUploadFile8; } set { _sUploadFile8 = value; } }
        public int Status { get { return _iStatus; } set { _iStatus = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
