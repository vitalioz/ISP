using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsUsers
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataRow dtRow;

        private int    _iRecord_ID;
        private int    _iType;
        private string _sSurname;
        private string _sSurnameEng;
        private string _sFirstname;
        private string _sFirstnameEng;
        private string _sFather;
        private string _sMother;
        private string _sFamily;
        private string _sChildren;
        private string _sSex;
        private DateTime _dDoB;
        private string _sADT;
        private string _sIssueDate;
        private string _sPoliceDepart;
        private string _sAdress;
        private string _sCity;
        private int    _iCountry_ID;
        private string _sTK;
        private int    _iCountryTax_ID;
        private string _sAFM;
        private string _sDOY;
        private string _sTel;
        private string _sFax;
        private string _sMobile;
        private string _sEMail;
        private string _sEMail_Username;
        private string _sEMail_Password;
        private string _sEducation;
        private string _sCertifikates;
        private string _sEidikotita;
        private string _sDuration;
        private string _sPosition;
        private int    _iLocation;
        private string _sRelation;
        private string _sPasword;
        private int    _iDMSAccess;
        private int    _iLanguage;
        private int    _iDivision;
        private string _sBank;
        private string _sBankAccount;
        private string _sDefaultFolder;
        private string _sUploadFolder;
        private string _sDMSTransferPoint;
        private string _sDMSDownloadPath;
        private int    _iChief;
        private int    _iRM;
        private int    _iSender;
        private int    _iIntroducer;
        private int    _iDiaxiristis;
        private DateTime _dDiax_DateStart;
        private DateTime _dDiax_DateFinish;
        private int      _iClientsRequests_Status;
        private int      _iDivisionFilter;
        private DateTime _dStartDate;
        private int    _iClientsFilter_ID;
        private string _sPhoto;
        private int    _iStatus;
        private int    _iAktive;

        private string _sClientsFilter;
        private DataTable _dtList;

        public clsUsers()
        {
            this._iRecord_ID = 0;
            this._iType = 0;
            this._sSurname = "";
            this._sFirstname = "";
            this._sSurnameEng = "";
            this._sFirstname = "";
            this._sFirstnameEng = "";
            this._sFather = "";
            this._sMother = "";
            this._sFamily = "";
            this._sChildren = "";
            this._sSex = "";
            this._dDoB = Convert.ToDateTime("1900/01/01");
            this._sADT = "";
            this._sIssueDate = "";
            this._sPoliceDepart = "";
            this._sAdress = "";
            this._sCity = "";
            this._sTK = "";
            this._iCountry_ID = 0;
            this._iCountryTax_ID = 0;
            this._sAFM = "";
            this._sDOY = "";
            this._sTel = "";
            this._sFax = "";
            this._sMobile = "";
            this._sEMail = "";
            this._sEMail_Username = "";
            this._sEMail_Password = "";
            this._sEducation = "";
            this._sCertifikates = "";
            this._sEidikotita = "";
            this._sDuration = "";
            this._sPosition = "";
            this._iLocation = 0;
            this._sRelation = "";
            this._sPasword = "";
            this._iDMSAccess = 0;
            this._iLanguage = 0;
            this._iDivision = 0;
            this._sBank = "";
            this._sBankAccount = "";
            this._sDefaultFolder = "";
            this._sUploadFolder = "";
            this._sDMSTransferPoint = "";
            this._sDMSDownloadPath = "";
            this._iChief = 0;
            this._iRM = 0;
            this._iSender = 0;
            this._iIntroducer = 0;
            this._iDivisionFilter = 0;
            this._dStartDate = Convert.ToDateTime("1900/01/01");
            this._iClientsFilter_ID = 0;
            this._sPhoto = "";
            this._iStatus = 0;
            this._iAktive = 0;

            this._sClientsFilter = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetUserData", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@User_ID", this._iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iType = Convert.ToInt32(drList["Tipos"]);
                    this._sSurname = drList["Surname"] + "";
                    this._sFirstname = drList["Firstname"] + "";
                    this._sSurnameEng = drList["SurnameEng"] + "";
                    this._sFirstnameEng = drList["FirstnameEng"] + "";
                    this._sFather = drList["Father"] + "";
                    this._sMother = drList["Mother"] + "";
                    this._sFamily = drList["Family"] + "";
                    this._sChildren = drList["Children"] + "";
                    this._sSex = drList["Sex"] + "";
                    this._dDoB = Convert.ToDateTime(drList["DoB"]);
                    this._sADT = drList["ADT"] + "";
                    this._sIssueDate = drList["IssueDate"] + "";
                    this._sPoliceDepart = drList["PoliceDepart"] + "";
                    this._sAdress = drList["Adress"] + "";
                    this._sCity = drList["City"] + "";
                    this._sTK = drList["TK"] + "";
                    this._iCountry_ID = Convert.ToInt32(drList["Country_ID"]);
                    this._iCountryTax_ID = Convert.ToInt32(drList["CountryTax_ID"]);
                    this._sAFM = drList["AFM"] + "";
                    this._sDOY = drList["DOY"] + "";
                    this._sTel = drList["Tel"] + "";
                    this._sFax = drList["Fax"] + "";
                    this._sMobile = drList["Mobile"] + "";
                    this._sEMail = drList["EMail"] + "";
                    this._sEMail_Username = drList["EMail_Username"] + "";
                    this._sEMail_Password = drList["EMail_Password"] + "";
                    this._sEducation = drList["Education"] + "";
                    this._sCertifikates = drList["Certifikates"] + "";
                    this._sEidikotita = drList["Eidikotita"] + "";
                    this._sDuration = drList["Duration"] + "";
                    this._sPosition = drList["Position"] + "";
                    this._iLocation = Convert.ToInt32(drList["Location"]);
                    this._sRelation = drList["Relation"] + "";
                    this._sPasword = drList["Pasword"] + "";
                    this._iDMSAccess = Convert.ToInt32(drList["DMSAccess"]);
                    this._iLanguage = Convert.ToInt32(drList["Language"]);
                    this._iDivision = Convert.ToInt32(drList["Division"]);
                    this._sBank = drList["Bank"] + "";
                    this._sBankAccount = drList["BankAccount"] + "";
                    this._sDefaultFolder = drList["DefaultFolder"] + "";
                    this._sUploadFolder = drList["UploadFolder"] + "";
                    this._sDMSTransferPoint = drList["DMSTransferPoint"] + "";
                    this._sDMSDownloadPath = drList["DMSDownloadPath"] + "";
                    this._iChief = Convert.ToInt32(drList["Chief"]);
                    this._iRM = Convert.ToInt32(drList["RM"]);
                    this._iSender = Convert.ToInt32(drList["Sender"]);
                    this._iIntroducer = Convert.ToInt32(drList["Introducer"]);
                    this._iDiaxiristis = Convert.ToInt32(drList["Diaxiristis"]);
                    this._dDiax_DateStart = Convert.ToDateTime(drList["Diax_DateStart"]);
                    this._dDiax_DateFinish = Convert.ToDateTime(drList["Diax_DateFinish"]);                    
                    this._iClientsRequests_Status = Convert.ToInt32(drList["ClientsRequests_Status"]);
                    this._iDivisionFilter = Convert.ToInt32(drList["DivisionFilter"]);
                    this._dStartDate = Convert.ToDateTime(drList["StartDate"]);
                    this._iClientsFilter_ID = Convert.ToInt32(drList["ClientsFilter_ID"]);
                    this._sPhoto = drList["Photo"] + "";
                    this._iStatus = Convert.ToInt32(drList["Status"]);
                    this._iAktive = Convert.ToInt32(drList["Aktive"]);
                    this._sClientsFilter = drList["ClientsFilter"] + "";
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("Tipos", typeof(int));
            _dtList.Columns.Add("Title", typeof(string));
            _dtList.Columns.Add("Chief", typeof(int));
            _dtList.Columns.Add("RM", typeof(int));
            _dtList.Columns.Add("Sender", typeof(int));
            _dtList.Columns.Add("Introducer", typeof(int));
            _dtList.Columns.Add("Diaxiristis", typeof(int));
            _dtList.Columns.Add("Aktive", typeof(int));
            _dtList.Columns.Add("EMail", typeof(string));

            dtRow = _dtList.NewRow();
            dtRow["ID"] = 0;
            dtRow["Tipos"] = 0;
            dtRow["Title"] = "Όλοι";
            dtRow["Chief"] = 1;
            dtRow["RM"] = 1;
            dtRow["Sender"] = 1;
            dtRow["Introducer"] = 1;
            dtRow["Diaxiristis"] = 1;
            dtRow["Aktive"] = 1;
            dtRow["EMail"] = "";
            _dtList.Rows.Add(dtRow);

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Keys"));
                cmd.Parameters.Add(new SqlParameter("@Col", ""));
                cmd.Parameters.Add(new SqlParameter("@Value", ""));
                cmd.Parameters.Add(new SqlParameter("@Order", "Surname, Firstname"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["Tipos"] = drList["Tipos"];
                    dtRow["Title"] = (drList["Surname"] + " " + drList["Firstname"]).Trim();
                    dtRow["Chief"] = drList["Chief"];
                    dtRow["RM"] = drList["RM"];
                    dtRow["Sender"] = drList["Sender"];
                    dtRow["Introducer"] = drList["Introducer"];
                    dtRow["Diaxiristis"] = drList["Diaxiristis"];
                    dtRow["Aktive"] = drList["Aktive"];
                    dtRow["EMail"] = drList["EMail"];
                    _dtList.Rows.Add(dtRow);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetMenu()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("Status", typeof(int));
            _dtList.Columns.Add("Extra", typeof(string));
            _dtList.Columns.Add("TitleGr", typeof(string));
            _dtList.Columns.Add("TitleEn", typeof(string));
            _dtList.Columns.Add("Menu_ID", typeof(int));
            _dtList.Columns.Add("MenuGroup_ID", typeof(int));
            _dtList.Columns.Add("MenuView_ID", typeof(int));
            _dtList.Columns.Add("Extra_Exists", typeof(string));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetMenuUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@User_ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["Status"] = drList["Status"];
                    dtRow["Extra"] = drList["Extra"];
                    dtRow["TitleGr"] = drList["TitleGr"];
                    dtRow["TitleEn"] = drList["TitleEn"];
                    dtRow["Menu_ID"] = drList["Menu_ID"];
                    dtRow["MenuGroup_ID"] = drList["MenuGroup_ID"];
                    dtRow["MenuView_ID"] = drList["MenuView_ID"];
                    dtRow["Extra_Exists"] = drList["Extra_Exists"];
                    _dtList.Rows.Add(dtRow);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetUser_Documents()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("DateIns", typeof(DateTime));
            _dtList.Columns.Add("DocType_Title", typeof(string));
            _dtList.Columns.Add("DocType_ID", typeof(int));
            _dtList.Columns.Add("FileName", typeof(string));

            try
            {
                conn.Open();
                cmd = new SqlCommand("sp_GetUsersDocuments", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@User_ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["DateIns"] = drList["DateIns"];
                    dtRow["DocType_Title"] = drList["Title"];
                    dtRow["DocType_ID"] = drList["DocType_ID"];
                    dtRow["FileName"] = drList["FileName"];
                    _dtList.Rows.Add(dtRow);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetUser_Alerts()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("AlertType", typeof(int));
            _dtList.Columns.Add("OK", typeof(int));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "UsersAlerts"));
                cmd.Parameters.Add(new SqlParameter("@Col", "User_ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", _iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", "ID"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["AlertType"] = drList["AlertType"];
                    dtRow["OK"] = drList["OK"];
                    _dtList.Rows.Add(dtRow);
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
                using (cmd = new SqlCommand("InsertUser", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Tipos", SqlDbType.Int).Value = _iType;
                    cmd.Parameters.Add("@Surname", SqlDbType.NVarChar, 100).Value = _sSurname.Trim();
                    cmd.Parameters.Add("@SurnameEng", SqlDbType.NVarChar, 100).Value = _sSurnameEng.Trim();
                    cmd.Parameters.Add("@Firstname", SqlDbType.NVarChar, 30).Value = _sFirstname.Trim();
                    cmd.Parameters.Add("@FirstnameEng", SqlDbType.NVarChar, 30).Value = _sFirstnameEng.Trim();
                    cmd.Parameters.Add("@Father", SqlDbType.NVarChar, 30).Value = _sFather.Trim();
                    cmd.Parameters.Add("@Mother", SqlDbType.NVarChar, 30).Value = _sMother.Trim();
                    cmd.Parameters.Add("@Family", SqlDbType.NVarChar, 30).Value = _sFamily.Trim();
                    cmd.Parameters.Add("@Children", SqlDbType.NVarChar, 10).Value = _sChildren.Trim();
                    cmd.Parameters.Add("@Sex", SqlDbType.NVarChar, 6).Value = _sSex.Trim();
                    cmd.Parameters.Add("@DoB", SqlDbType.DateTime).Value = _dDoB;
                    cmd.Parameters.Add("@ADT", SqlDbType.NVarChar, 30).Value = _sADT.Trim();
                    cmd.Parameters.Add("@IssueDate", SqlDbType.NVarChar, 30).Value = _sIssueDate.Trim();
                    cmd.Parameters.Add("@PoliceDepart", SqlDbType.NVarChar, 30).Value = _sPoliceDepart.Trim();
                    cmd.Parameters.Add("@Adress", SqlDbType.NVarChar, 40).Value = _sAdress.Trim();
                    cmd.Parameters.Add("@City", SqlDbType.NVarChar, 20).Value = _sCity.Trim();
                    cmd.Parameters.Add("@Country_ID", SqlDbType.Int).Value = _iCountry_ID;
                    cmd.Parameters.Add("@TK", SqlDbType.NVarChar, 10).Value = _sTK.Trim();
                    cmd.Parameters.Add("@CountryTax_ID", SqlDbType.Int).Value = _iCountryTax_ID;
                    cmd.Parameters.Add("@AFM", SqlDbType.NVarChar, 30).Value = _sAFM.Trim();
                    cmd.Parameters.Add("@DOY", SqlDbType.NVarChar, 30).Value = _sDOY.Trim();
                    cmd.Parameters.Add("@Tel", SqlDbType.NVarChar, 30).Value = _sTel.Trim();
                    cmd.Parameters.Add("@Fax", SqlDbType.NVarChar, 30).Value = _sFax.Trim();
                    cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar, 30).Value = _sMobile.Trim();
                    cmd.Parameters.Add("@EMail", SqlDbType.NVarChar, 80).Value = _sEMail.Trim();
                    cmd.Parameters.Add("@EMail_Username", SqlDbType.NVarChar, 80).Value = _sEMail_Username.Trim();
                    cmd.Parameters.Add("@EMail_Password", SqlDbType.NVarChar, 30).Value = _sEMail_Password.Trim();
                    cmd.Parameters.Add("@Education", SqlDbType.NVarChar, 30).Value = _sEducation.Trim();
                    cmd.Parameters.Add("@Certifikates", SqlDbType.NVarChar, 30).Value = _sCertifikates.Trim();
                    cmd.Parameters.Add("@Eidikotita", SqlDbType.NVarChar, 30).Value = _sEidikotita.Trim();
                    cmd.Parameters.Add("@Duration", SqlDbType.NVarChar, 30).Value = _sDuration.Trim();
                    cmd.Parameters.Add("@Position", SqlDbType.NVarChar, 60).Value = _sPosition.Trim();
                    cmd.Parameters.Add("@Location", SqlDbType.Int).Value = _iLocation;
                    cmd.Parameters.Add("@Relation", SqlDbType.NVarChar, 30).Value = _sRelation.Trim();
                    cmd.Parameters.Add("@Pasword", SqlDbType.NVarChar, 50).Value = _sPasword.Trim();
                    cmd.Parameters.Add("@DMSAccess", SqlDbType.Int).Value = _iDMSAccess;
                    cmd.Parameters.Add("@Language", SqlDbType.Int).Value = _iLanguage;
                    cmd.Parameters.Add("@Division", SqlDbType.Int).Value = _iDivision;
                    cmd.Parameters.Add("@Bank", SqlDbType.NVarChar, 30).Value = _sBank.Trim();
                    cmd.Parameters.Add("@BankAccount", SqlDbType.NVarChar, 30).Value = _sBankAccount.Trim();
                    cmd.Parameters.Add("@DefaultFolder", SqlDbType.NVarChar, 200).Value = _sDefaultFolder.Trim();
                    cmd.Parameters.Add("@UploadFolder", SqlDbType.NVarChar, 200).Value = _sUploadFolder.Trim();
                    cmd.Parameters.Add("@DMSTransferPoint", SqlDbType.NVarChar, 50).Value = _sDMSTransferPoint.Trim();
                    cmd.Parameters.Add("@DMSDownloadPath", SqlDbType.NVarChar, 200).Value = _sDMSDownloadPath.Trim();
                    cmd.Parameters.Add("@Chief", SqlDbType.Int).Value = _iChief;
                    cmd.Parameters.Add("@RM", SqlDbType.Int).Value = _iRM;
                    cmd.Parameters.Add("@Sender", SqlDbType.Int).Value = _iSender;
                    cmd.Parameters.Add("@Introducer", SqlDbType.Int).Value = _iIntroducer;
                    cmd.Parameters.Add("@Diaxiristis", SqlDbType.Int).Value = _iDiaxiristis;
                    cmd.Parameters.Add("@Diax_DateStart", SqlDbType.DateTime).Value = _dDiax_DateStart;
                    cmd.Parameters.Add("@Diax_DateFinish", SqlDbType.DateTime).Value = _dDiax_DateFinish;                    
                    cmd.Parameters.Add("@ClientsRequests_Status", SqlDbType.Int).Value = _iClientsRequests_Status;
                    cmd.Parameters.Add("@DivisionFilter", SqlDbType.Int).Value = _iDivisionFilter;
                    cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = _dStartDate;
                    cmd.Parameters.Add("@ClientsFilter_ID", SqlDbType.Int).Value = _iClientsFilter_ID;
                    cmd.Parameters.Add("@Photo", SqlDbType.NVarChar, 100).Value = _sPhoto.Trim();
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.Parameters.Add("@Aktive", SqlDbType.Int).Value = _iAktive;
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
                using (cmd = new SqlCommand("EditUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Surname", SqlDbType.NVarChar, 50).Value = _sSurname.Trim();
                    cmd.Parameters.Add("@SurnameEng", SqlDbType.NVarChar, 50).Value = _sSurnameEng.Trim();
                    cmd.Parameters.Add("@Firstname", SqlDbType.NVarChar, 30).Value = _sFirstname.Trim();
                    cmd.Parameters.Add("@FirstnameEng", SqlDbType.NVarChar, 30).Value = _sFirstnameEng.Trim();
                    cmd.Parameters.Add("@Father", SqlDbType.NVarChar, 30).Value = _sFather.Trim();
                    cmd.Parameters.Add("@Mother", SqlDbType.NVarChar, 30).Value = _sMother.Trim();
                    cmd.Parameters.Add("@Family", SqlDbType.NVarChar, 30).Value = _sFamily.Trim();
                    cmd.Parameters.Add("@Children", SqlDbType.NVarChar, 10).Value = _sChildren.Trim();
                    cmd.Parameters.Add("@Sex", SqlDbType.NVarChar, 6).Value = _sSex.Trim();
                    cmd.Parameters.Add("@DoB", SqlDbType.DateTime).Value = _dDoB;
                    cmd.Parameters.Add("@ADT", SqlDbType.NVarChar, 30).Value = _sADT.Trim();
                    cmd.Parameters.Add("@IssueDate", SqlDbType.NVarChar, 30).Value = _sIssueDate.Trim();
                    cmd.Parameters.Add("@PoliceDepart", SqlDbType.NVarChar, 30).Value = _sPoliceDepart.Trim();
                    cmd.Parameters.Add("@Adress", SqlDbType.NVarChar, 30).Value = _sAdress.Trim();
                    cmd.Parameters.Add("@City", SqlDbType.NVarChar, 30).Value = _sCity.Trim();
                    cmd.Parameters.Add("@Country_ID", SqlDbType.Int).Value = _iCountry_ID;
                    cmd.Parameters.Add("@TK", SqlDbType.NVarChar, 10).Value = _sTK.Trim();
                    cmd.Parameters.Add("@CountryTax_ID", SqlDbType.Int).Value = _iCountryTax_ID;
                    cmd.Parameters.Add("@AFM", SqlDbType.NVarChar, 30).Value = _sAFM.Trim();
                    cmd.Parameters.Add("@DOY", SqlDbType.NVarChar, 30).Value = _sDOY.Trim();
                    cmd.Parameters.Add("@Tel", SqlDbType.NVarChar, 30).Value = _sTel.Trim();
                    cmd.Parameters.Add("@Fax", SqlDbType.NVarChar, 30).Value = _sFax.Trim();
                    cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar, 30).Value = _sMobile.Trim();
                    cmd.Parameters.Add("@EMail", SqlDbType.NVarChar, 80).Value = _sEMail.Trim();
                    cmd.Parameters.Add("@EMail_Username", SqlDbType.NVarChar, 80).Value = _sEMail_Username.Trim();
                    cmd.Parameters.Add("@EMail_Password", SqlDbType.NVarChar, 30).Value = _sEMail_Password.Trim();
                    cmd.Parameters.Add("@Education", SqlDbType.NVarChar, 30).Value = _sEducation.Trim();
                    cmd.Parameters.Add("@Certifikates", SqlDbType.NVarChar, 30).Value = _sCertifikates.Trim();
                    cmd.Parameters.Add("@Eidikotita", SqlDbType.NVarChar, 30).Value = _sEidikotita.Trim();
                    cmd.Parameters.Add("@Duration", SqlDbType.NVarChar, 30).Value = _sDuration.Trim();
                    cmd.Parameters.Add("@Position", SqlDbType.NVarChar, 60).Value = _sPosition.Trim();
                    cmd.Parameters.Add("@Location", SqlDbType.Int).Value = _iLocation;
                    cmd.Parameters.Add("@Relation", SqlDbType.NVarChar, 30).Value = _sRelation.Trim();
                    cmd.Parameters.Add("@Pasword", SqlDbType.NVarChar, 50).Value = _sPasword.Trim();
                    cmd.Parameters.Add("@DMSAccess", SqlDbType.Int).Value = _iDMSAccess;
                    cmd.Parameters.Add("@Language", SqlDbType.Int).Value = _iLanguage;
                    cmd.Parameters.Add("@Division", SqlDbType.Int).Value = _iDivision;
                    cmd.Parameters.Add("@Bank", SqlDbType.NVarChar, 30).Value = _sBank.Trim();
                    cmd.Parameters.Add("@BankAccount", SqlDbType.NVarChar, 30).Value = _sBankAccount.Trim();
                    cmd.Parameters.Add("@DefaultFolder", SqlDbType.NVarChar, 200).Value = _sDefaultFolder.Trim();
                    cmd.Parameters.Add("@UploadFolder", SqlDbType.NVarChar, 200).Value = _sUploadFolder.Trim();
                    cmd.Parameters.Add("@DMSTransferPoint", SqlDbType.NVarChar, 50).Value = _sDMSTransferPoint.Trim();
                    cmd.Parameters.Add("@DMSDownloadPath", SqlDbType.NVarChar, 200).Value = _sDMSDownloadPath.Trim();
                    cmd.Parameters.Add("@Chief", SqlDbType.Int).Value = _iChief;
                    cmd.Parameters.Add("@RM", SqlDbType.Int).Value = _iRM;
                    cmd.Parameters.Add("@Sender", SqlDbType.Int).Value = _iSender;
                    cmd.Parameters.Add("@Introducer", SqlDbType.Int).Value = _iIntroducer;
                    cmd.Parameters.Add("@Diaxiristis", SqlDbType.Int).Value = _iDiaxiristis;
                    cmd.Parameters.Add("@Diax_DateStart", SqlDbType.DateTime).Value = _dDiax_DateStart;
                    cmd.Parameters.Add("@Diax_DateFinish", SqlDbType.DateTime).Value = _dDiax_DateFinish;
                    cmd.Parameters.Add("@ClientsRequests_Status", SqlDbType.Int).Value = _iClientsRequests_Status;
                    cmd.Parameters.Add("@DivisionFilter", SqlDbType.Int).Value = _iDivisionFilter;
                    cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = _dStartDate;
                    cmd.Parameters.Add("@ClientsFilter_ID", SqlDbType.Int).Value = _iClientsFilter_ID;
                    cmd.Parameters.Add("@Photo", SqlDbType.NVarChar, 100).Value = _sPhoto.Trim();
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.Parameters.Add("@Aktive", SqlDbType.Int).Value = _iAktive;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Keys";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int Type { get { return this._iType; } set { this._iType = value; } }
        public string Surname { get { return _sSurname; } set { _sSurname = value; } }         
        public string Firstname { get { return _sFirstname; } set { _sFirstname = value; } }
        public string SurnameEng { get { return this._sSurnameEng; } set { this._sSurnameEng = value; } }
        public string FirstnameEng { get { return this._sFirstnameEng; } set { this._sFirstnameEng = value; } }
        public string Father { get { return this._sFather; } set { this._sFather = value; } }
        public string Mother { get { return this._sMother; } set { this._sMother = value; } }
        public string Family { get { return this._sFamily; } set { this._sFamily = value; } }
        public string Children { get { return this._sChildren; } set { this._sChildren = value; } }
        public string Sex { get { return this._sSex; } set { this._sSex = value; } }
        public DateTime DoB { get { return this._dDoB; } set { this._dDoB = value; } }
        public string ADT { get { return this._sADT; } set { this._sADT = value; } }
        public string IssueDate { get { return this._sIssueDate; } set { this._sIssueDate = value; } }
        public string PoliceDepart { get { return this._sPoliceDepart; } set { this._sPoliceDepart = value; } }
        public string Adress { get { return this._sAdress; } set { this._sAdress = value; } }
        public string City { get { return this._sCity; } set { this._sCity = value; } }
        public int Country_ID { get { return this._iCountry_ID; } set { this._iCountry_ID = value; } }
        public string TK { get { return this._sTK; } set { this._sTK = value; } }
        public int CountryTax_ID { get { return this._iCountryTax_ID; } set { this._iCountryTax_ID = value; } }
        public string AFM { get { return this._sAFM; } set { this._sAFM = value; } }
        public string DOY { get { return this._sDOY; } set { this._sDOY = value; } }
        public string Tel { get { return _sTel; } set { _sTel = value; } }
        public string Fax { get { return this._sFax; } set { this._sFax = value; } }
        public string Mobile { get { return this._sMobile; } set { this._sMobile = value; } }
        public string EMail { get { return _sEMail; } set { _sEMail = value; } }
        public string EMail_Username { get { return this._sEMail_Username; } set { this._sEMail_Username = value; } }
        public string EMail_Password { get { return this._sEMail_Password; } set { this._sEMail_Password = value; } }
        public string Education { get { return this._sEducation; } set { this._sEducation = value; } }
        public string Certifikates { get { return this._sCertifikates; } set { this._sCertifikates = value; } }
        public string Eidikotita { get { return this._sEidikotita; } set { this._sEidikotita = value; } }
        public string Duration { get { return this._sDuration; } set { this._sDuration = value; } }
        public string Position { get { return this._sPosition; } set { this._sPosition = value; } }
        public int Location { get { return this._iLocation; } set { this._iLocation = value; } }
        public string Relation { get { return this._sRelation; } set { this._sRelation = value; } }
        public string Pasword { get { return this._sPasword; } set { this._sPasword = value; } }
        public int DMSAccess { get { return this._iDMSAccess; } set { this._iDMSAccess = value; } }
        public int Language { get { return this._iLanguage; } set { this._iLanguage = value; } }
        public int Division { get { return this._iDivision; } set { this._iDivision = value; } }
        public string Bank { get { return this._sBank; } set { this._sBank = value; } }
        public string BankAccount { get { return this._sBankAccount; } set { this._sBankAccount = value; } }
        public string DefaultFolder { get { return _sDefaultFolder; } set { _sDefaultFolder = value; } }
        public string UploadFolder { get { return _sUploadFolder; } set { _sUploadFolder = value; } }
        public string DMSTransferPoint { get { return _sDMSTransferPoint; } set { _sDMSTransferPoint = value; } }
        public string DMSDownloadPath { get { return this._sDMSDownloadPath; } set { this._sDMSDownloadPath = value; } }
        public int Chief { get { return this._iChief; } set { this._iChief = value; } }
        public int RM { get { return this._iRM; } set { this._iRM = value; } }
        public int Sender { get { return this._iSender; } set { this._iSender = value; } }
        public int Introducer { get { return this._iIntroducer; } set { this._iIntroducer = value; } }
        public int Diaxiristis { get { return this._iDiaxiristis; } set { this._iDiaxiristis = value; } }
        public DateTime Diax_DateStart { get { return this._dDiax_DateStart; } set { this._dDiax_DateStart = value; } }
        public DateTime Diax_DateFinish { get { return this._dDiax_DateFinish; } set { this._dDiax_DateFinish = value; } }
        public int ClientsRequests_Status { get { return this._iClientsRequests_Status; } set { this._iClientsRequests_Status = value; } }
        public int DivisionFilter { get { return this._iDivisionFilter; } set { this._iDivisionFilter = value; } }
        public DateTime StartDate { get { return this._dStartDate; } set { this._dStartDate = value; } }
        public int ClientsFilter_ID { get { return this._iClientsFilter_ID; } set { this._iClientsFilter_ID = value; } }
        public string ClientsFilter { get { return this._sClientsFilter; } set { this._sClientsFilter = value; } }
        public string Photo { get { return this._sPhoto; } set { this._sPhoto = value; } }
        public int Status { get { return this._iStatus; } set { this._iStatus = value; } }
        public int Aktive { get { return this._iAktive; } set { this._iAktive = value; } }
        public DataTable List { get { return _dtList; } set { _dtList = value; } }
    }
}
