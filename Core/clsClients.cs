using System;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsClients
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;

        private int       _iRecord_ID;
        private int       _iType;
        private string    _sSurname;
        private string    _sFirstname;
        private string    _sSurnameEng;
        private string    _sFirstnameEng;
        private string    _sSurnameFather;
        private string    _sFirstnameFather;
        private string    _sSurnameMother;
        private string    _sFirstnameMother;
        private string    _sSurnameSizigo;
        private string    _sFirstnameSizigo;
        private int       _iStatus;
        private int       _iBlockStatus;
        private int       _iDivision;
        private int       _iBrunch_ID;
        private int       _iSpec_ID;
        private DateTime  _dDoB;
        private string    _sBornPlace;
        private int       _iCitizen_ID;
        private string    _sSex;
        private int       _iCategory;
        private int       _iGuardian_ID;
        private int       _iRisk;
        private string    _sADT;
        private string    _sExpireDate;
        private string    _sPolice;
        private string    _sPassport;
        private string    _sPassport_ExpireDate;
        private string    _sPassport_Police;
        private string    _sDOY;
        private string    _sAFM;
        private string    _sDOY2;
        private string    _sAFM2;
        private string    _sAMKA;
        private int       _iCountryTaxes_ID;
        private string    _sAddress;
        private string    _sCity;
        private string    _sZip;
        private int       _iCountry_ID;
        private string    _sTel;
        private string    _sFax;
        private string    _sMobile;
        private int       _iSendSMS;
        private string    _sEMail;
        private int       _iConnectionMethod;
        private int       _iLogSxedio_ID;
        private float     _fltVAT_Percent;
        private string    _sUsers_List;
        private int       _iEkkatharistika;
        private string    _sSpecialCategory;
        private string    _sMerida;
        private string    _sLogAxion;
        private string    _sNotes;
        private int       _iRM_ID;
        private int       _iRM_Step;
        private int       _iBO_Step;
        private string    _sConne;
        private string    _sCompanyTitle;
        private string    _sCompanyDescription;
        private string    _sJobPosition;
        private string    _sJobAddress;
        private string    _sJobCity;
        private string    _sJobZip;
        private int       _iJobCountry_ID;
        private string    _sJobTel;
        private string    _sJobMobile;
        private string    _sJobEMail;
        private string    _sJobURL;
        private int       _iFamilyStatus;
        private float     _fltSumAxion;
        private float     _fltSumAkiniton;
        private int       _iIs_InfluenceCenter;
        private int       _iIs_Introducer;
        private int       _iIs_RepresentPerson;
        private int       _iDependentPersons;
        private int       _iIdentification;
        private DateTime  _dDateIns;
         
        private string    _sFullname;
        private string    _sRM_Email;
        private string    _sRM_Surname;
        private string    _sRM_Firstname;
        private string    _sCode;
        private string    _sPortfolio;
        private string    _sSpec_Title;
        private string    _sBrunches_Title;
        private string    _sCountryCode;
        private string    _sCountry_Title_En;
        private string    _sCountry_Title_Gr;
        private string    _sCountryPhoneCode;
        private string    _sCountryTaxes_Code;
        private string    _sCountryTaxes_Title_En;
        private string    _sCountryTaxes_Title_Gr;
        private string    _sCountryTaxes_PhoneCode;

        private DataTable _dtList;

        public clsClients()
        {
            this._iRecord_ID = 0;
            this._iType = 0;
            this._sSurname = "";
            this._sFirstname = "";
            this._sSurnameEng = "";
            this._sFirstnameEng = "";
            this._sSurnameFather = "";
            this._sFirstnameFather = "";
            this._sSurnameMother = "";
            this._sFirstnameMother = "";
            this._sSurnameSizigo = "";
            this._sFirstnameSizigo = "";
            this._iStatus = 0;
            this._iBlockStatus = 0;
            this._iDivision = 0;
            this._iBrunch_ID = 0;
            this._iSpec_ID = 0;
            this._dDoB = Convert.ToDateTime("1900/01/01");
            this._sBornPlace = "";
            this._iCitizen_ID = 0;
            this._sSex = "";
            this._iCategory = 0;
            this._iGuardian_ID = 0;
            this._iRisk = 0;
            this._sADT = "";
            this._sExpireDate = "";
            this._sPolice = "";
            this._sPassport = "";
            this._sPassport_ExpireDate = "";
            this._sPassport_Police = "";
            this._sDOY = "";
            this._sAFM = "";
            this._sDOY2 = "";
            this._sAFM2 = "";
            this._sAMKA = "";
            this._iCountryTaxes_ID = 0;
            this._sAddress = "";
            this._sCity = "";
            this._sZip = "";
            this._iCountry_ID = 0;
            this._sTel = "";
            this._sFax = "";
            this._sMobile = "";
            this._iSendSMS = 0;
            this._sEMail = "";
            this._iConnectionMethod = 0;
            this._iLogSxedio_ID = 0;
            this._fltVAT_Percent = 0;
            this._sUsers_List = "";
            this._iEkkatharistika = 0;
            this._sSpecialCategory = "";
            this._sMerida = "";
            this._sLogAxion = "";
            this._sNotes = "";
            this._iRM_ID = 0;
            this._iRM_Step = 0;
            this._iBO_Step = 0;
            this._sConne = "";
            this._sCompanyTitle = "";
            this._sCompanyDescription = "";
            this._sJobPosition = "";
            this._sJobAddress = "";
            this._sJobCity = "";
            this._sJobZip = "";
            this._iJobCountry_ID = 0;
            this._sJobTel = "";
            this._sJobMobile = "";
            this._sJobEMail = "";
            this._sJobURL = "";
            this._iFamilyStatus = 0;
            this._fltSumAxion = 0;
            this._fltSumAkiniton = 0;
            this._iIs_InfluenceCenter = 0;
            this._iIs_Introducer = 0;
            this._iIs_RepresentPerson = 0;
            this._iDependentPersons = 0;
            this._iIdentification = 0;
            this._dDateIns = Convert.ToDateTime("1900/01/01");

            this._sFullname = "";
            this._sCode = "";
            this._sPortfolio = "";
            this._sRM_Email = "";
            this._sRM_Surname = "";
            this._sRM_Firstname = "";
            this._sSpec_Title = "";
            this._sBrunches_Title = "";
            this._sCountryCode = "";
            this._sCountry_Title_En = "";
            this._sCountry_Title_Gr = "";
            this._sCountryPhoneCode = "";
            this._sCountryTaxes_Code = "";
            this._sCountryTaxes_Title_En = "";
            this._sCountryTaxes_Title_Gr = "";
            this._sCountryTaxes_PhoneCode = "";
        }
        public void GetRecord()
        {
            //this._iRecord_ID = 0;

            try
            {
                conn = new SqlConnection(Global.connStr);
                conn.Open();
                
                cmd = new SqlCommand("GetClient_Data", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Email", this._sEMail));
                cmd.Parameters.Add(new SqlParameter("@Mobile", this._sMobile));
                cmd.Parameters.Add(new SqlParameter("@AFM", this._sAFM));
                cmd.Parameters.Add(new SqlParameter("@DoB", this._dDoB));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iType = Convert.ToInt32(drList["Tipos"]);
                    this._sSurname = drList["Surname"] + "";
                    this._sFirstname = drList["Firstname"] + "";
                    this._sSurnameEng = drList["SurnameEng"] + "";
                    this._sFirstnameEng = drList["FirstnameEng"] + "";
                    this._sSurnameFather = drList["SurnameFather"] + "";
                    this._sFirstnameFather = drList["FirstnameFather"] + "";
                    this._sSurnameMother = drList["SurnameMother"] + "";
                    this._sFirstnameMother = drList["FirstnameMother"] + "";
                    this._sSurnameSizigo = drList["SurnameSizigo"] + "";
                    this._sFirstnameSizigo = drList["FirstnameSizigo"] + "";
                    this._iStatus = Convert.ToInt32(drList["Status"]);
                    this._iBlockStatus = Convert.ToInt32(drList["BlockStatus"]);
                    this._iDivision = Convert.ToInt32(drList["Division"]);
                    this._iBrunch_ID = Convert.ToInt32(drList["Brunch_ID"]);
                    this._iSpec_ID = Convert.ToInt32(drList["Spec_ID"]);
                    this._dDoB = Convert.ToDateTime(drList["DoB"]);
                    this._sBornPlace = drList["BornPlace"] + "";
                    this._iCitizen_ID = Convert.ToInt32(drList["Citizen_ID"]);
                    this._sSex = drList["Sex"] + "";
                    this._iCategory = Convert.ToInt32(drList["Category"]);
                    this._iGuardian_ID = Convert.ToInt32(drList["Guardian_ID"]);
                    this._sADT = drList["ADT"] + "";
                    this._sExpireDate = drList["ExpireDate"] + "";
                    this._sPolice = drList["Police"] + "";
                    this._sPassport = drList["Passport"] + "";
                    this._sPassport_ExpireDate = drList["Passport_ExpireDate"] + "";
                    this._sPassport_Police = drList["Passport_Police"] + "";
                    this._sAFM = drList["AFM"] + "";
                    this._sDOY = drList["DOY"] + "";
                    this._sAFM2 = drList["AFM2"] + "";
                    this._sDOY2 = drList["DOY2"] + "";
                    this._sAMKA = drList["AMKA"] + "";
                    this._iCountryTaxes_ID = Convert.ToInt32(drList["CountryTaxes_ID"]);
                    this._sCountryTaxes_Code = drList["CountryTaxes_Code"] + "";
                    this._sCountryTaxes_Title_En = drList["CountryTaxes_Title"] + "";
                    this._sCountryTaxes_Title_Gr = drList["CountryTaxes_Title_Gr"] + "";
                    this._sCountryTaxes_PhoneCode = drList["CountryTaxes_PhoneCode"] + "";
                    this._sAddress = drList["Address"] + "";
                    this._sCity = drList["City"] + "";
                    this._sZip = drList["Zip"] + "";
                    this._iCountry_ID = Convert.ToInt32(drList["Country_ID"]);
                    this._sCountry_Title_En = drList["Country_Title"] + "";
                    this._sCountry_Title_Gr = drList["Country_Title_Gr"] + "";
                    this._sCountryPhoneCode = drList["Country_PhoneCode"] + "";
                    this._sTel = drList["Tel"] + "";
                    this._sFax = drList["Fax"] + "";
                    this._sMobile = drList["Mobile"] + "";
                    this._iSendSMS = Convert.ToInt32(drList["SendSMS"]);
                    this._sEMail = drList["EMail"] + "";
                    this._iConnectionMethod = Convert.ToInt32(drList["ConnectionMethod"]);
                    this._dDateIns = Convert.ToDateTime(drList["DateIns"]);
                    this._iLogSxedio_ID = Convert.ToInt32(drList["LogSxedio_ID"]);
                    this._fltVAT_Percent = Convert.ToSingle(drList["VAT_Percent"]);
                    this._sUsers_List = drList["Users_List"] + "";
                    this._iEkkatharistika = Convert.ToInt32(drList["Ekkatharistika"]);
                    this._sSpecialCategory = drList["SpecialCategory"] + "";
                    this._sMerida = drList["Merida"] + "";
                    this._sLogAxion = drList["LogAxion"] + "";
                    this._sNotes = drList["Notes"] + "";
                    this._iRM_ID = Convert.ToInt32(drList["RM_ID"]);
                    this._iRM_Step = Convert.ToInt32(drList["RM_Step"]);
                    this._iBO_Step = Convert.ToInt32(drList["BO_Step"]);
                    this._sConne = drList["Conne"] + "";
                    this._sCompanyTitle = drList["CompanyTitle"] + "";
                    this._sCompanyDescription = drList["CompanyDescription"] + "";
                    this._sJobPosition = drList["JobPosition"] + "";
                    this._sJobAddress = drList["JobAddress"] + "";
                    this._sJobCity = drList["JobCity"] + "";
                    this._sJobZip = drList["JobZip"] + "";
                    this._iJobCountry_ID = Convert.ToInt32(drList["JobCountry_ID"]);
                    this._sJobTel = drList["JobTel"] + "";
                    this._sJobMobile = drList["JobMobile"] + "";
                    this._sJobEMail = drList["JobEMail"] + "";
                    this._sJobURL = drList["JobURL"] + "";
                    this._iFamilyStatus = Convert.ToInt32(drList["FamilyStatus"]);
                    this._fltSumAxion = Convert.ToSingle(drList["SumAxion"]);
                    this._fltSumAkiniton = Convert.ToSingle(drList["SumAkiniton"]);
                    this._iIs_InfluenceCenter = Convert.ToInt32(drList["Is_InfluenceCenter"]);
                    this._iIs_Introducer = Convert.ToInt32(drList["Is_Introducer"]);
                    this._iIs_RepresentPerson = Convert.ToInt32(drList["Is_RepresentPerson"]);
                    this._iDependentPersons = Convert.ToInt32(drList["DependentPersons"]);
                    this._iIdentification = Convert.ToInt32(drList["Identification"]);
                    this._iRisk = Convert.ToInt32(drList["Risk"]);
                    this._sSpec_Title = drList["Spec_Title"] + "";
                    this._sBrunches_Title = drList["Brunches_Title"] + "";

                    if (this._iType == 1) this._sFullname = (drList["Surname"] + " " + drList["Firstname"]).Trim();
                    else this._sFullname = (drList["Surname"] + "").Trim();
                    //this._sCode = drList["Code"] + "";
                    //this._sPortfolio = drList["Portfolio"] + "";
                }
            }
            catch (Exception ex) {
                string sTemp = ex.Message;
            }
            finally { conn.Close(); }   
        }
        public void GetList()
        {
            string sDoB = "", sGroup = "";

             _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("Surname", typeof(string));
            _dtList.Columns.Add("Firstname", typeof(string));
            _dtList.Columns.Add("FirstnameFather", typeof(string));
            _dtList.Columns.Add("Group", typeof(string));
            _dtList.Columns.Add("Category", typeof(int));
            _dtList.Columns.Add("DoB", typeof(string));
            _dtList.Columns.Add("ADT", typeof(string));
            _dtList.Columns.Add("DOY", typeof(string));
            _dtList.Columns.Add("AFM", typeof(string));
            _dtList.Columns.Add("Address", typeof(string));
            _dtList.Columns.Add("Zip", typeof(string));
            _dtList.Columns.Add("City", typeof(string));
            _dtList.Columns.Add("Country_Title", typeof(string));
            _dtList.Columns.Add("Tel", typeof(string));
            _dtList.Columns.Add("Fax", typeof(string));
            _dtList.Columns.Add("Mobile", typeof(string));
            _dtList.Columns.Add("Email", typeof(string));
            _dtList.Columns.Add("Spec_Title", typeof(string));
            _dtList.Columns.Add("Status", typeof(int));
            _dtList.Columns.Add("DateIns", typeof(DateTime));
            _dtList.Columns.Add("CountryTaxes_Title", typeof(string));
            _dtList.Columns.Add("Citizen_Title", typeof(string));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetClientsList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Surname", _sSurname));
                cmd.Parameters.Add(new SqlParameter("@Firstname", _sFirstname));
                cmd.Parameters.Add(new SqlParameter("@Category", _iCategory));
                cmd.Parameters.Add(new SqlParameter("@Country_ID", _iCountry_ID));
                cmd.Parameters.Add(new SqlParameter("@Citizen_ID", _iCitizen_ID));
                cmd.Parameters.Add(new SqlParameter("@Risk", _iRisk));
                cmd.Parameters.Add(new SqlParameter("@AFM", _sAFM));
                drList = cmd.ExecuteReader();

                while (drList.Read())
                {
                    if (Convert.ToInt32(drList["Category"]) == 0) sDoB = Convert.ToDateTime(drList["DoB"]).ToString("dd/MM/yyyy");
                    else sDoB = "";
                    switch (Convert.ToInt32(drList["Status"])) {
                        case 0:
                            sGroup = "ΑΝΕΝΕΡΓΟΣ";
                            break;
                        case 1:
                            sGroup = "ΠΕΛΑΤΗΣ";
                            break;
                        case -1:
                            sGroup = "ΥΠΟΨΗΦΙΟΣ";
                            break;
                        case -2:
                            sGroup = "ΕΠΑΦΗ";
                            break;
                        case -3:
                            //sGroup = "ΚΕΝΤΡΟ ΕΠΙΡΡΟΗΣ";
                            break;
                        case -4:
                            //sGroup = "INTRODUCER";
                            break;
                        case -5:
                            //sGroup = "REPRESENT PERSON";
                            break;
                    }
                   _dtList.Rows.Add(drList["ID"], drList["Surname"], drList["Firstname"], (Convert.ToInt32(drList["Tipos"]) == 1 ? drList["FirstnameFather"]+"" : ""), 
                                    sGroup, drList["Category"], sDoB, drList["ADT"], drList["DOY"], drList["AFM"], drList["Address"], drList["Zip"], drList["City"], 
                                    drList["Country_Title"], drList["Tel"], drList["Fax"], drList["Mobile"], drList["EMail"], drList["Spec_Title"], drList["Status"], 
                                    drList["DateIns"], drList["CountryTaxes_Title"], drList["Citizen_Title"]);
                }
                _dtList.Load(drList);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetCashList()
        {
            _dtList = new DataTable("ClientsList");
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("Tipos", typeof(int));
            _dtList.Columns.Add("Fullname", typeof(string));
            _dtList.Columns.Add("FirstnameFather", typeof(string));
            _dtList.Columns.Add("ADT", typeof(string));
            _dtList.Columns.Add("DOY", typeof(string));
            _dtList.Columns.Add("AFM", typeof(string));
            _dtList.Columns.Add("DOY2", typeof(string));
            _dtList.Columns.Add("AFM2", typeof(string));
            _dtList.Columns.Add("DoB", typeof(DateTime));
            _dtList.Columns.Add("SpecialTitle", typeof(string));
            _dtList.Columns.Add("Is_RepresentPerson", typeof(int));
            _dtList.Columns.Add("Status", typeof(int));
            //_dtList.Columns.Add("Merida", typeof(string));
            //_dtList.Columns.Add("DependentPersons", typeof(int));
            //_dtList.Columns.Add("RM_Step", typeof(int));
            //_dtList.Columns.Add("Tipos", typeof(int));
            //_dtList.Columns.Add("Conne", typeof(string));
            //_dtList.Columns.Add("Status", typeof(int));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetClient_Data", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Email", _sEMail));
                cmd.Parameters.Add(new SqlParameter("@Mobile", _sMobile));
                cmd.Parameters.Add(new SqlParameter("@AFM", _sAFM));
                cmd.Parameters.Add(new SqlParameter("@DoB", _dDoB));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], drList["Tipos"], (drList["Surname"] + " " + drList["Firstname"]).Trim(), drList["FirstnameFather"],
                        drList["ADT"], drList["DOY"], drList["AFM"], drList["DOY2"], drList["AFM2"], drList["DoB"], drList["Spec_Title"], drList["Is_RepresentPerson"], drList["Status"]); 
                       //drList["DependentPersons"], drList["RM_Step"], drList["Tipos"], drList["Conne"], drList["Status"]);
                }
                _dtList.Load(drList);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetSameClients()
        {
        }
        public bool GetCheckBlackList()
        {
            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("Fullname", typeof(string));
            _dtList.Columns.Add("DOY", typeof(string));
            _dtList.Columns.Add("AFM", typeof(string));
            _dtList.Columns.Add("DependentPersons", typeof(int));
            _dtList.Columns.Add("RM_Step", typeof(int));
            _dtList.Columns.Add("Tipos", typeof(int));
            _dtList.Columns.Add("Conne", typeof(string));
            _dtList.Columns.Add("Status", typeof(int));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetCheckBlackList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                drList = cmd.ExecuteReader();

                while (drList.Read())
                {
                    _dtList.Rows.Add(drList["ID"], (drList["Surname"] + " " + drList["Firstname"]).Trim(), drList["DOY"]+"", drList["AFM"] + "",
                        drList["DependentPersons"] + "", drList["RM_Step"], drList["Tipos"], drList["Conne"] + "", drList["Status"]);
                }
                _dtList.Load(drList);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            return false;
        }
        public int InsertRecord()
        {
            try
            {
                conn = new SqlConnection(Global.connStr);
                conn.Open();
                using (cmd = new SqlCommand("InsertClient", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Tipos", SqlDbType.Int).Value = _iType;
                    cmd.Parameters.Add("@Surname", SqlDbType.NVarChar, 100).Value = (_sSurname + "").Trim();
                    cmd.Parameters.Add("@Firstname", SqlDbType.NVarChar, 40).Value = (_sFirstname + "").Trim();
                    cmd.Parameters.Add("@SurnameEng", SqlDbType.NVarChar, 100).Value = (_sSurnameEng + "").Trim();
                    cmd.Parameters.Add("@FirstnameEng", SqlDbType.NVarChar, 40).Value = (_sFirstnameEng + "").Trim();
                    cmd.Parameters.Add("@SurnameFather", SqlDbType.NVarChar, 100).Value = (_sSurnameFather + "").Trim();
                    cmd.Parameters.Add("@FirstnameFather", SqlDbType.NVarChar, 40).Value = (_sFirstnameFather + "").Trim();
                    cmd.Parameters.Add("@SurnameMother", SqlDbType.NVarChar, 100).Value = (_sSurnameMother + "").Trim();
                    cmd.Parameters.Add("@FirstnameMother", SqlDbType.NVarChar, 1000).Value = (_sFirstnameMother + "").Trim();
                    cmd.Parameters.Add("@SurnameSizigo", SqlDbType.NVarChar, 100).Value = (_sSurnameSizigo + "").Trim();
                    cmd.Parameters.Add("@FirstnameSizigo", SqlDbType.NVarChar, 40).Value = (_sFirstnameSizigo + "").Trim();
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.Parameters.Add("@BlockStatus", SqlDbType.Int).Value = _iBlockStatus;
                    cmd.Parameters.Add("@Division", SqlDbType.Int).Value = _iDivision;
                    cmd.Parameters.Add("@Is_InfluenceCenter", SqlDbType.Int).Value = _iIs_InfluenceCenter;
                    cmd.Parameters.Add("@Is_Introducer", SqlDbType.Int).Value = _iIs_Introducer;
                    cmd.Parameters.Add("@Is_RepresentPerson", SqlDbType.Int).Value = _iIs_RepresentPerson;
                    cmd.Parameters.Add("@Brunch_ID", SqlDbType.Int).Value = _iBrunch_ID;
                    cmd.Parameters.Add("@Spec_ID", SqlDbType.Int).Value = _iSpec_ID;
                    cmd.Parameters.Add("@DoB", SqlDbType.DateTime).Value = _dDoB;
                    cmd.Parameters.Add("@BornPlace", SqlDbType.NVarChar, 50).Value = _sBornPlace + "";
                    cmd.Parameters.Add("@Citizen_ID", SqlDbType.Int).Value = _iCitizen_ID;
                    cmd.Parameters.Add("@Sex", SqlDbType.NVarChar, 6).Value = _sSex + "";
                    cmd.Parameters.Add("@FamilyStatus", SqlDbType.Int).Value = _iFamilyStatus;
                    cmd.Parameters.Add("@Category", SqlDbType.Int).Value = _iCategory;
                    cmd.Parameters.Add("@Guardian_ID", SqlDbType.Int).Value = _iGuardian_ID;
                    cmd.Parameters.Add("@ADT", SqlDbType.NVarChar, 20).Value = _sADT + "";
                    cmd.Parameters.Add("@ExpireDate", SqlDbType.NVarChar, 20).Value = _sExpireDate + "";
                    cmd.Parameters.Add("@Police", SqlDbType.NVarChar, 50).Value = _sPolice + "";
                    cmd.Parameters.Add("@Passport", SqlDbType.NVarChar, 30).Value = _sPassport + "";
                    cmd.Parameters.Add("@Passport_ExpireDate", SqlDbType.NVarChar, 20).Value = _sPassport_ExpireDate + "";
                    cmd.Parameters.Add("@Passport_Police", SqlDbType.NVarChar, 50).Value = _sPassport_Police + "";
                    cmd.Parameters.Add("@AFM", SqlDbType.NVarChar, 20).Value = _sAFM + "";
                    cmd.Parameters.Add("@DOY", SqlDbType.NVarChar, 40).Value = _sDOY + "";
                    cmd.Parameters.Add("@AFM2", SqlDbType.NVarChar, 20).Value = _sAFM2 + "";
                    cmd.Parameters.Add("@DOY2", SqlDbType.NVarChar, 40).Value = _sDOY2 + "";
                    cmd.Parameters.Add("@AMKA", SqlDbType.NVarChar, 30).Value = _sAMKA + "";
                    cmd.Parameters.Add("@CountryTaxes_ID", SqlDbType.Int).Value = _iCountryTaxes_ID;
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 100).Value = _sAddress + "";
                    cmd.Parameters.Add("@City", SqlDbType.NVarChar, 30).Value = _sCity + "";
                    cmd.Parameters.Add("@Zip", SqlDbType.NVarChar, 20).Value = _sZip + "";
                    cmd.Parameters.Add("@Country_ID", SqlDbType.Int).Value = _iCountry_ID;
                    cmd.Parameters.Add("@Tel", SqlDbType.NVarChar, 30).Value = _sTel + "";
                    cmd.Parameters.Add("@Fax", SqlDbType.NVarChar, 30).Value = _sFax + "";
                    cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar, 30).Value = _sMobile + "";
                    cmd.Parameters.Add("@SendSMS", SqlDbType.Int).Value = _iSendSMS;
                    cmd.Parameters.Add("@EMail", SqlDbType.NVarChar, 80).Value = _sEMail + "".ToLower();
                    cmd.Parameters.Add("@ConnectionMethod", SqlDbType.Int).Value = _iConnectionMethod;
                    cmd.Parameters.Add("@CompanyTitle", SqlDbType.NVarChar, 100).Value = _sCompanyTitle + "";
                    cmd.Parameters.Add("@CompanyDescription", SqlDbType.NVarChar, 100).Value = _sCompanyDescription + "";
                    cmd.Parameters.Add("@JobPosition", SqlDbType.NVarChar, 100).Value = _sJobPosition + "";
                    cmd.Parameters.Add("@JobAddress", SqlDbType.NVarChar, 100).Value = _sJobAddress + "";
                    cmd.Parameters.Add("@JobCity", SqlDbType.NVarChar, 30).Value = _sJobCity + "";
                    cmd.Parameters.Add("@JobZip", SqlDbType.NVarChar, 20).Value = _sJobZip + "";
                    cmd.Parameters.Add("@JobCountry_ID", SqlDbType.Int).Value = _iJobCountry_ID;
                    cmd.Parameters.Add("@JobTel", SqlDbType.NVarChar, 30).Value = _sJobTel + "";
                    cmd.Parameters.Add("@JobMobile", SqlDbType.NVarChar, 30).Value = _sJobMobile + "";
                    cmd.Parameters.Add("@JobEMail", SqlDbType.NVarChar, 80).Value = _sJobEMail + "";
                    cmd.Parameters.Add("@JobURL", SqlDbType.NVarChar, 80).Value = _sJobURL + "";           
                    cmd.Parameters.Add("@LogSxedio_ID", SqlDbType.Int).Value = _iLogSxedio_ID;
                    cmd.Parameters.Add("@Users_List", SqlDbType.NVarChar, 100).Value = _sUsers_List + "";
                    cmd.Parameters.Add("@VAT_Percent", SqlDbType.Float).Value = _fltVAT_Percent;
                    cmd.Parameters.Add("@Ekkatharistika", SqlDbType.Int).Value = _iEkkatharistika;
                    cmd.Parameters.Add("@SpecialCategory", SqlDbType.NVarChar, 30).Value = _sSpecialCategory;
                    cmd.Parameters.Add("@Merida", SqlDbType.NVarChar, 30).Value = _sMerida + "";
                    cmd.Parameters.Add("@LogAxion", SqlDbType.NVarChar, 30).Value = _sLogAxion + "";
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 1000).Value = _sNotes + "";
                    cmd.Parameters.Add("@RM_ID", SqlDbType.Int).Value = _iRM_ID;
                    cmd.Parameters.Add("@RM_Step", SqlDbType.Int).Value = _iRM_Step;
                    cmd.Parameters.Add("@BO_Step", SqlDbType.Int).Value = _iBO_Step;
                    cmd.Parameters.Add("@Conne", SqlDbType.NVarChar, 100).Value = _sConne + "";
                    cmd.Parameters.Add("@SumAxion", SqlDbType.Float).Value = _fltSumAxion;
                    cmd.Parameters.Add("@SumAkiniton", SqlDbType.Float).Value = _fltSumAkiniton;
                    cmd.Parameters.Add("@Risk", SqlDbType.Int).Value = _iRisk;
                    cmd.Parameters.Add("@DependentPersons", SqlDbType.Int).Value = 0;
                    cmd.Parameters.Add("@Identification", SqlDbType.Int).Value = _iIdentification;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = DateTime.Now;
                    cmd.ExecuteNonQuery();
                    _iRecord_ID = Convert.ToInt32(outParam.Value);
                }
            }
            catch (Exception ex) { 
                string sTemp = ex.Message; 
            }
            finally { conn.Close(); }

            return _iRecord_ID;
        }
        public void EditRecord()
        {
            try
            {
                conn.Open();
                using (cmd = new SqlCommand("EditClient", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Surname", SqlDbType.NVarChar, 100).Value = (_sSurname + "").Trim();
                    cmd.Parameters.Add("@Firstname", SqlDbType.NVarChar, 40).Value = (_sFirstname + "").Trim();
                    cmd.Parameters.Add("@SurnameEng", SqlDbType.NVarChar, 100).Value = (_sSurnameEng + "").Trim();
                    cmd.Parameters.Add("@FirstnameEng", SqlDbType.NVarChar, 40).Value = (_sFirstnameEng + "").Trim();
                    cmd.Parameters.Add("@SurnameFather", SqlDbType.NVarChar, 100).Value = (_sSurnameFather + "").Trim();
                    cmd.Parameters.Add("@FirstnameFather", SqlDbType.NVarChar, 40).Value = (_sFirstnameFather + "").Trim();
                    cmd.Parameters.Add("@SurnameMother", SqlDbType.NVarChar, 100).Value = (_sSurnameMother + "").Trim();
                    cmd.Parameters.Add("@FirstnameMother", SqlDbType.NVarChar, 1000).Value = (_sFirstnameMother + "").Trim();
                    cmd.Parameters.Add("@SurnameSizigo", SqlDbType.NVarChar, 100).Value = (_sSurnameSizigo + "").Trim();
                    cmd.Parameters.Add("@FirstnameSizigo", SqlDbType.NVarChar, 40).Value = (_sFirstnameSizigo + "").Trim();
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.Parameters.Add("@BlockStatus", SqlDbType.Int).Value = _iBlockStatus;
                    cmd.Parameters.Add("@Division", SqlDbType.Int).Value = _iDivision;
                    cmd.Parameters.Add("@Is_InfluenceCenter", SqlDbType.Int).Value = _iIs_InfluenceCenter;
                    cmd.Parameters.Add("@Is_Introducer", SqlDbType.Int).Value = _iIs_Introducer;
                    cmd.Parameters.Add("@Is_RepresentPerson", SqlDbType.Int).Value = _iIs_RepresentPerson;
                    cmd.Parameters.Add("@Brunch_ID", SqlDbType.Int).Value = _iBrunch_ID;
                    cmd.Parameters.Add("@Spec_ID", SqlDbType.Int).Value = _iSpec_ID;
                    cmd.Parameters.Add("@DoB", SqlDbType.DateTime).Value = _dDoB;
                    cmd.Parameters.Add("@BornPlace", SqlDbType.NVarChar, 50).Value = _sBornPlace + "";
                    cmd.Parameters.Add("@Citizen_ID", SqlDbType.Int).Value = _iCitizen_ID;
                    cmd.Parameters.Add("@Sex", SqlDbType.NVarChar, 6).Value = _sSex + "";
                    cmd.Parameters.Add("@FamilyStatus", SqlDbType.Int).Value = _iFamilyStatus;
                    cmd.Parameters.Add("@Category", SqlDbType.Int).Value = _iCategory;
                    cmd.Parameters.Add("@Guardian_ID", SqlDbType.Int).Value = _iGuardian_ID;
                    cmd.Parameters.Add("@ADT", SqlDbType.NVarChar, 20).Value = _sADT + "";
                    cmd.Parameters.Add("@ExpireDate", SqlDbType.NVarChar, 20).Value = _sExpireDate + "";
                    cmd.Parameters.Add("@Police", SqlDbType.NVarChar, 50).Value = _sPolice + "";
                    cmd.Parameters.Add("@Passport", SqlDbType.NVarChar, 30).Value = _sPassport + "";
                    cmd.Parameters.Add("@Passport_ExpireDate", SqlDbType.NVarChar, 20).Value = _sPassport_ExpireDate + "";
                    cmd.Parameters.Add("@Passport_Police", SqlDbType.NVarChar, 50).Value = _sPassport_Police + "";
                    cmd.Parameters.Add("@AFM", SqlDbType.NVarChar, 20).Value = _sAFM + "";
                    cmd.Parameters.Add("@DOY", SqlDbType.NVarChar, 40).Value = _sDOY + "";
                    cmd.Parameters.Add("@AFM2", SqlDbType.NVarChar, 20).Value = _sAFM2 + "";
                    cmd.Parameters.Add("@DOY2", SqlDbType.NVarChar, 40).Value = _sDOY2 + "";
                    cmd.Parameters.Add("@AMKA", SqlDbType.NVarChar, 30).Value = _sAMKA + "";
                    cmd.Parameters.Add("@CountryTaxes_ID", SqlDbType.Int).Value = _iCountryTaxes_ID;
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 100).Value = _sAddress + "";
                    cmd.Parameters.Add("@City", SqlDbType.NVarChar, 30).Value = _sCity + "";
                    cmd.Parameters.Add("@Zip", SqlDbType.NVarChar, 20).Value = _sZip + "";
                    cmd.Parameters.Add("@Country_ID", SqlDbType.Int).Value = _iCountry_ID;
                    cmd.Parameters.Add("@Tel", SqlDbType.NVarChar, 30).Value = _sTel + "";
                    cmd.Parameters.Add("@Fax", SqlDbType.NVarChar, 30).Value = _sFax + "";
                    cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar, 30).Value = _sMobile + "";
                    cmd.Parameters.Add("@SendSMS", SqlDbType.Int).Value = _iSendSMS;
                    cmd.Parameters.Add("@EMail", SqlDbType.NVarChar, 80).Value = _sEMail + "".ToLower();
                    cmd.Parameters.Add("@ConnectionMethod", SqlDbType.Int).Value = _iConnectionMethod;
                    cmd.Parameters.Add("@CompanyTitle", SqlDbType.NVarChar, 100).Value = _sCompanyTitle + "";
                    cmd.Parameters.Add("@CompanyDescription", SqlDbType.NVarChar, 100).Value = _sCompanyDescription + "";
                    cmd.Parameters.Add("@JobPosition", SqlDbType.NVarChar, 100).Value = _sJobPosition + "";
                    cmd.Parameters.Add("@JobAddress", SqlDbType.NVarChar, 100).Value = _sJobAddress + "";
                    cmd.Parameters.Add("@JobCity", SqlDbType.NVarChar, 30).Value = _sJobCity + "";
                    cmd.Parameters.Add("@JobZip", SqlDbType.NVarChar, 20).Value = _sJobZip + "";
                    cmd.Parameters.Add("@JobCountry_ID", SqlDbType.Int).Value = _iJobCountry_ID;
                    cmd.Parameters.Add("@JobTel", SqlDbType.NVarChar, 30).Value = _sJobTel + "";
                    cmd.Parameters.Add("@JobMobile", SqlDbType.NVarChar, 30).Value = _sJobMobile + "";
                    cmd.Parameters.Add("@JobEMail", SqlDbType.NVarChar, 80).Value = _sJobEMail + "";
                    cmd.Parameters.Add("@JobURL", SqlDbType.NVarChar, 80).Value = _sJobURL + "";
                    cmd.Parameters.Add("@LogSxedio_ID", SqlDbType.Int).Value = _iLogSxedio_ID;
                    cmd.Parameters.Add("@Users_List", SqlDbType.NVarChar, 100).Value = _sUsers_List + "";
                    cmd.Parameters.Add("@VAT_Percent", SqlDbType.Float).Value = _fltVAT_Percent;
                    cmd.Parameters.Add("@Ekkatharistika", SqlDbType.Int).Value = _iEkkatharistika;
                    cmd.Parameters.Add("@SpecialCategory", SqlDbType.NVarChar, 30).Value = _sSpecialCategory;
                    cmd.Parameters.Add("@Merida", SqlDbType.NVarChar, 30).Value = _sMerida + "";
                    cmd.Parameters.Add("@LogAxion", SqlDbType.NVarChar, 30).Value = _sLogAxion + "";
                    cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, 1000).Value = _sNotes + "";
                    cmd.Parameters.Add("@RM_ID", SqlDbType.Int).Value = _iRM_ID;
                    cmd.Parameters.Add("@RM_Step", SqlDbType.Int).Value = _iRM_Step;
                    cmd.Parameters.Add("@BO_Step", SqlDbType.Int).Value = _iBO_Step;
                    cmd.Parameters.Add("@Conne", SqlDbType.NVarChar, 100).Value = _sConne + "";
                    cmd.Parameters.Add("@SumAxion", SqlDbType.Float).Value = _fltSumAxion;
                    cmd.Parameters.Add("@SumAkiniton", SqlDbType.Float).Value = _fltSumAkiniton;
                    cmd.Parameters.Add("@Risk", SqlDbType.Int).Value = _iRisk;
                    cmd.Parameters.Add("@DependentPersons", SqlDbType.Int).Value = 0;
                    cmd.Parameters.Add("@Identification", SqlDbType.Int).Value = _iIdentification;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                string sTemp = ex.Message;
            }
            finally { conn.Close(); }
        }
        public int Edit_InfluenceData()
        {
            int i = 0;
            try
            {
                conn.Open();

                using (cmd = new SqlCommand("EditClient_InfluenceData", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter outParam = new SqlParameter("@Dep_pers", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                    i = Convert.ToInt32(outParam.Value);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            return i;
        }
        public int Edit_DependenceData()
        { 
            int i = 0;
            try
            {
                conn.Open();

                using (cmd = new SqlCommand("EditClient_DependenceData", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter outParam = new SqlParameter("@Dep_pers", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                    i = Convert.ToInt32(outParam.Value);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            return i;
        }
        public void DeleteRecord()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("DeleteRecord", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Clients";
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
        public string Firstname  { get { return _sFirstname; } set { _sFirstname = value; } }
        public string SurnameEng { get { return _sSurnameEng; } set { _sSurnameEng = value; } }
        public string FirstnameEng { get { return _sFirstnameEng; } set { _sFirstnameEng = value; } }
        public string SurnameFather { get { return _sSurnameFather; } set { _sSurnameFather = value; } }
        public string FirstnameFather { get { return _sFirstnameFather; } set { _sFirstnameFather = value; } }
        public string SurnameMother { get { return _sSurnameMother; } set { _sSurnameMother = value; } }
        public string FirstnameMother { get { return _sFirstnameMother; } set { _sFirstnameMother = value; } }
        public string SurnameSizigo { get { return _sSurnameSizigo; } set { _sSurnameSizigo = value; } }
        public string FirstnameSizigo { get { return _sFirstnameSizigo; } set { _sFirstnameSizigo = value; } }
        public int Status { get { return this._iStatus; } set { this._iStatus = value; } }
        public int BlockStatus { get { return this._iBlockStatus; } set { this._iBlockStatus = value; } }
        public int Division { get { return this._iDivision; } set { this._iDivision = value; } }
        public int Brunch_ID { get { return this._iBrunch_ID; } set { this._iBrunch_ID = value; } }
        public int Spec_ID { get { return this._iSpec_ID; } set { this._iSpec_ID = value; } }
        public DateTime DoB { get { return this._dDoB; } set { this._dDoB = value; } }
        public string BornPlace { get { return _sBornPlace; } set { _sBornPlace = value; } }
        public int Citizen_ID { get { return this._iCitizen_ID; } set { this._iCitizen_ID = value; } }
        public string Sex { get { return _sSex; } set { _sSex = value; } }
        public int Category { get { return this._iCategory; } set { this._iCategory = value; } }
        public int Guardian_ID { get { return this._iGuardian_ID; } set { this._iGuardian_ID = value; } }
        public string ADT { get { return _sADT; } set { _sADT = value; } }
        public string ExpireDate { get { return _sExpireDate; } set { _sExpireDate = value; } }
        public string Police { get { return _sPolice; } set { _sPolice = value; } }
        public string Passport { get { return _sPassport; } set { _sPassport = value; } }
        public string Passport_ExpireDate { get { return _sPassport_ExpireDate; } set { _sPassport_ExpireDate = value; } }
        public string Passport_Police { get { return _sPassport_Police; } set { _sPassport_Police = value; } }
        public string AFM { get { return _sAFM; } set { _sAFM = value; } }
        public string DOY { get { return _sDOY; } set { _sDOY = value; } }
        public string AFM2 { get { return _sAFM2; } set { _sAFM2 = value; } }
        public string DOY2 { get { return _sDOY2; } set { _sDOY2 = value; } }
        public string AMKA { get { return _sAMKA; } set { _sAMKA = value; } }
        public int CountryTaxes_ID { get { return this._iCountryTaxes_ID; } set { this._iCountryTaxes_ID = value; } }
        public string Address { get { return _sAddress; } set { _sAddress = value; } }
        public string City { get { return _sCity; } set { _sCity = value; } }
        public string Zip { get { return _sZip; } set { _sZip = value; } }
        public int Country_ID { get { return this._iCountry_ID; } set { this._iCountry_ID = value; } }
        public string Tel { get { return _sTel; } set { _sTel = value; } }
        public string Fax { get { return _sFax; } set { _sFax = value; } }
        public string Mobile { get { return _sMobile; } set { _sMobile = value; } }
        public int SendSMS { get { return this._iSendSMS; } set { this._iSendSMS = value; } }
        public string EMail {get { return _sEMail; } set { _sEMail = value; } }
        public int ConnectionMethod { get { return this._iConnectionMethod; } set { this._iConnectionMethod = value; } }
        public string CompanyTitle { get { return _sCompanyTitle; } set { _sCompanyTitle = value; } }
        public string CompanyDescription { get { return _sCompanyDescription; } set { _sCompanyDescription = value; } }
        public string JobPosition { get { return _sJobPosition; } set { _sJobPosition = value; } }
        public string JobAddress { get { return _sJobAddress; } set { _sJobAddress = value; } }
        public string JobCity { get { return _sJobCity; } set { _sJobCity = value; } }
        public string JobZip { get { return _sJobZip; } set { _sJobZip = value; } }
        public int JobCountry_ID { get { return this._iJobCountry_ID; } set { this._iJobCountry_ID = value; } }
        public string JobTel { get { return _sJobTel; } set { _sJobTel = value; } }
        public string JobMobile { get { return _sJobMobile; } set { _sJobMobile = value; } }
        public string JobEMail { get { return _sJobEMail; } set { _sJobEMail = value; } }
        public string JobURL { get { return _sJobURL; } set { _sJobURL = value; } }
        public DateTime DateIns { get { return this._dDateIns; } set { this._dDateIns = value; } }
        public int LogSxedio_ID { get { return this._iLogSxedio_ID; } set { this._iLogSxedio_ID = value; } }
        public float VAT_Percent { get { return this._fltVAT_Percent; } set { this._fltVAT_Percent = value; } }
        public string Users_List { get { return _sUsers_List; } set { _sUsers_List = value; } }
        public int Ekkatharistika { get { return this._iEkkatharistika; } set { this._iEkkatharistika = value; } }
        public string SpecialCategory { get { return _sSpecialCategory; } set { _sSpecialCategory = value; } }
        public string Merida { get { return _sMerida; } set { _sMerida = value; } }
        public string LogAxion { get { return _sLogAxion; } set { _sLogAxion = value; } }
        public string Notes { get { return _sNotes; } set { _sNotes = value; } }
        public int RM_ID { get { return this._iRM_ID; } set { this._iRM_ID = value; } }
        public int RM_Step { get { return this._iRM_Step; } set { this._iRM_Step = value; } }
        public int BO_Step { get { return this._iBO_Step; } set { this._iBO_Step = value; } }
        public string Conne { get { return _sConne; } set { _sConne = value; } }
        public int FamilyStatus { get { return this._iFamilyStatus; } set { this._iFamilyStatus = value; } }
        public float SumAxion { get { return this._fltSumAxion; } set { this._fltSumAxion = value; } }
        public float SumAkiniton { get { return this._fltSumAkiniton; } set { this._fltSumAkiniton = value; } }
        public int Is_InfluenceCenter { get { return this._iIs_InfluenceCenter; } set { this._iIs_InfluenceCenter = value; } }
        public int Is_Introducer { get { return this._iIs_Introducer; } set { this._iIs_Introducer = value; } }
        public int Is_RepresentPerson { get { return this._iIs_RepresentPerson; } set { this._iIs_RepresentPerson = value; } }
        public int DependentPersons { get { return this._iDependentPersons; } set { this._iDependentPersons = value; } }
        public int Identification { get { return this._iIdentification; } set { this._iIdentification = value; } }
        public int Risk { get { return this._iRisk; } set { this._iRisk = value; } }
        public string Fullname { get { return _sFullname; } set { _sFullname = value; } }
        public string RM_EMail { get { return _sRM_Email; } set { _sRM_Email = value; } }
        public string RM_Surname { get { return _sRM_Surname; } set { _sRM_Surname = value; } }
        public string RM_Firstname { get { return _sRM_Firstname; } set { _sRM_Firstname = value; } }
        public string Code { get { return _sCode; } set { _sCode = value; } }
        public string Portfolio { get { return _sPortfolio; } set { _sPortfolio = value; } }
        public string Spec_Title { get { return _sSpec_Title; } set { _sSpec_Title = value; } }
        public string Brunches_Title { get { return _sBrunches_Title; } set { _sBrunches_Title = value; } }
        public string CountryCode { get { return _sCountryCode; } set { _sCountryCode = value; } }
        public string Country_Title_En { get { return _sCountry_Title_En; } set { _sCountry_Title_En = value; } }
        public string Country_Title_Gr { get { return _sCountry_Title_Gr; } set { _sCountry_Title_Gr = value; } }
        public string Country_PhoneCode { get { return _sCountryPhoneCode; } set { _sCountryPhoneCode = value; } }
        public string CountryTaxes_Code { get { return _sCountryTaxes_Code; } set { _sCountryTaxes_Code = value; } }
        public string CountryTaxes_Title_En { get { return _sCountryTaxes_Title_En; } set { _sCountryTaxes_Title_En = value; } }
        public string CountryTaxes_Title_Gr { get { return _sCountryTaxes_Title_Gr; } set { _sCountryTaxes_Title_Gr = value; } }
        public string CountryTaxes_PhoneCode { get { return _sCountryTaxes_PhoneCode; } set { _sCountryTaxes_PhoneCode = value; } }
        public DataTable List  {get { return _dtList; } set { _dtList = value; }}
    }
}
