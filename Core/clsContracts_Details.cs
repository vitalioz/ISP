using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsContracts_Details
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int      _iRecord_ID;
        private int      _iContract_ID;
        private int      _iInvestmentPolicy_ID;
        private string   _sAgreementNotes;
        private int      _iPerformanceFees;
        private int      _iUser1_ID;
        private string   _sAdvisory_Name;
        private int      _iUser2_ID;
        private string   _sRM_Name;
        private int      _iUser3_ID;
        private string   _sIntroducer_Name;
        private int      _iUser4_ID;
        private string   _sDiaxiristis_Name;
        private string   _sSurname;
        private string   _sFirstname;
        private string   _sSurnameFather;
        private string   _sFirstnameFather;
        private string   _sSurnameMother;
        private string   _sFirstnameMother;
        private string   _sSurnameSizigo;
        private string   _sFirstnameSizigo;
        private int      _iMIFIDCategory_ID;
        private int      _iDivision;
        private int      _iBrunch_ID;
        private int      _iSpec_ID;
        private DateTime _dDoB;
        private string   _sBornPlace;
        private int      _iCitizen_ID;
        private string   _sSex;
        private string   _sADT;
        private string   _sExpireDate;
        private string   _sPolice;
        private string   _sPassport;
        private string   _sPassport_ExpireDate;
        private string   _sPassport_Police;
        private string   _sDOY;
        private string   _sAFM;
        private string   _sDOY2;
        private string   _sAFM2;
        private string   _sAMKA;
        private float    _sgVAT_Percent;
        private int      _iCountryTaxes_ID;
        private string   _sAddress;
        private string   _sCity;
        private string   _sZip;
        private int      _iCountry_ID;
        private string   _sTel;
        private string   _sFax;
        private string   _sMobile;
        private int      _iSendSMS;
        private string   _sEMail;
        private int      _iConnectionMethod;
        private int      _iRisk;
        private string   _sMerida;
        private string   _sLogAxion;
        private string   _sInvName;
        private string   _sInvAddress;
        private string   _sInvCity;
        private string   _sInvZip;
        private int      _iInvCountry_ID;
        private string   _sInvDOY;
        private string   _sInvAFM;
        private int      _iChkComplex;
        private int      _iChkWorld;
        private int      _iChkGreece;
        private int      _iChkEurope;
        private int      _iChkAmerica;
        private int      _iChkAsia;
        private string   _sIncomeProducts;
        private string   _sCapitalProducts;
        private int      _iChkSpecificConstraints;
        private int      _iChkMonetaryRisk;
        private int      _iChkIndividualBonds;
        private int      _iChkMutualFunds;
        private int      _iChkBondedETFs;
        private int      _iChkIndividualShares;
        private int      _iChkMixedFunds;
        private int      _iChkMixedETFs;
        private int      _iChkFunds;
        private int      _iChkETFs;
        private int      _iChkInvestmentGrade;
        private string   _sMiscInstructions;

        private string   _sInvestmentPolicy_Title;
        private DataTable _dtList;
        public clsContracts_Details()
        {
            this._iRecord_ID = 0;
            this._iContract_ID = 0;
            this._iInvestmentPolicy_ID = 0;
            this._sInvestmentPolicy_Title = "";
            this._sAgreementNotes = "";
            this._iPerformanceFees = 0;
            this._iUser1_ID = 0;
            this._sAdvisory_Name = "";
            this._iUser2_ID = 0;
            this._sRM_Name = "";
            this._iUser3_ID = 0;
            this._sIntroducer_Name = "";
            this._iUser4_ID = 0;
            this._sDiaxiristis_Name = "";
            this._sSurname = "";
            this._sFirstname = "";
            this._sSurnameFather = "";
            this._sFirstnameFather = "";
            this._sSurnameMother = "";
            this._sFirstnameMother = "";
            this._sSurnameSizigo = "";
            this._sFirstnameSizigo = "";
            this._iMIFIDCategory_ID = 0;
            this._iDivision = 0;
            this._iBrunch_ID = 0;
            this._iSpec_ID = 0;
            this._dDoB = Convert.ToDateTime("1900/01/01");
            this._sBornPlace = "";
            this._sSex = "ΑΡ";
            this._iCitizen_ID = 0;
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
            this._sgVAT_Percent = 0;
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
            this._iRisk = 0;
            this._sMerida = "";
            this._sLogAxion = "";
            this._sInvName = "";
            this._sInvAddress = "";
            this._sInvCity = "";
            this._sInvZip = "";
            this._iInvCountry_ID = 0;
            this._sInvDOY = "";
            this._sInvAFM = "";
            this._iChkComplex = 0;
            this._iChkWorld = 0;
            this._iChkGreece = 0;
            this._iChkEurope = 0;
            this._iChkAmerica = 0;
            this._iChkAsia = 0;
            this._sIncomeProducts = "";
            this._sCapitalProducts = "";
            this._iChkSpecificConstraints = 0;
            this._iChkMonetaryRisk = 0;
            this._iChkIndividualBonds = 0;
            this._iChkMutualFunds = 0;
            this._iChkBondedETFs = 0;
            this._iChkIndividualShares = 0;
            this._iChkMixedFunds = 0;
            this._iChkMixedETFs = 0;
            this._iChkFunds = 0;
            this._iChkETFs = 0;
            this._iChkInvestmentGrade = 0;
            this._sMiscInstructions = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetContract_Details", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Contract_ID", _iContract_ID));
                cmd.Parameters.Add(new SqlParameter("@Record_ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iContract_ID = Convert.ToInt32(drList["Contract_ID"]);
                    this._iInvestmentPolicy_ID = Convert.ToInt32(drList["InvestmentPolicy_ID"]);
                    this._sInvestmentPolicy_Title = drList["InvestmentPolicy_Title"] + "";
                    this._sAgreementNotes = drList["AgreementNotes"] + "";
                    this._iPerformanceFees = Convert.ToInt32(drList["PerformanceFees"]);
                    this._iUser1_ID = Convert.ToInt32(drList["User1_ID"]);
                    this._sAdvisory_Name = drList["AdvisorSurname"] + " " + drList["AdvisorFirstname"];
                    this._iUser2_ID = Convert.ToInt32(drList["User2_ID"]);
                    this._sRM_Name = drList["RMSurname"] + " " + drList["RMFirstname"];
                    this._iUser3_ID = Convert.ToInt32(drList["User3_ID"]);
                    this._sIntroducer_Name = drList["IntroSurname"] + " " + drList["IntroFirstname"];
                    this._iUser4_ID = Convert.ToInt32(drList["User4_ID"]);
                    this._sDiaxiristis_Name = drList["DiaxSurname"] + " " + drList["DiaxFirstname"];
                    this._sSurname = drList["Surname"] + "";
                    this._sFirstname = drList["Firstname"] + "";
                    this._sSurnameFather = drList["SurnameFather"] + "";
                    this._sFirstnameFather = drList["FirstnameFather"] + "";
                    this._sSurnameMother = drList["SurnameMother"] + "";
                    this._sFirstnameMother = drList["FirstnameMother"] + "";
                    this._sSurnameSizigo = drList["SurnameSizigo"] + "";
                    this._sFirstnameSizigo = drList["FirstnameSizigo"] + "";
                    this._iMIFIDCategory_ID = Convert.ToInt32(drList["MIFIDCategory_ID"]);
                    this._iDivision = Convert.ToInt32(drList["Division"]);
                    this._iBrunch_ID = Convert.ToInt32(drList["Brunch_ID"]);
                    this._iSpec_ID = Convert.ToInt32(drList["Spec_ID"]);
                    this._dDoB =  Convert.ToDateTime(drList["DoB"]);
                    this._sBornPlace = drList["BornPlace"] + "";
                    this._sSex = drList["Sex"] + "";
                    this._iCitizen_ID = Convert.ToInt32(drList["Citizen_ID"]);
                    this._sADT = drList["ADT"] + "";
                    this._sExpireDate = drList["ExpireDate"] + "";
                    this._sPolice = drList["Police"] + "";
                    this._sPassport = drList["Passport"] + "";
                    this._sPassport_ExpireDate = drList["Passport_ExpireDate"] + "";
                    this._sPassport_Police = drList["Passport_Police"] + "";
                    this._sDOY = drList["DOY"] + "";
                    this._sAFM = drList["AFM"] + "";
                    this._sDOY2 = drList["DOY2"] + "";
                    this._sAFM2 = drList["AFM2"] + "";
                    this._sAMKA = drList["AMKA"] + "";
                    this._sgVAT_Percent = Convert.ToSingle(drList["VAT_Percent"]);
                    this._iCountryTaxes_ID = Convert.ToInt32(drList["CountryTaxes_ID"]);
                    this._sAddress = drList["Address"] + "";
                    this._sCity = drList["City"] + "";
                    this._sZip = drList["Zip"] + "";
                    this._iCountry_ID = Convert.ToInt32(drList["Country_ID"]);
                    this._sTel = drList["Tel"] + "";
                    this._sFax = drList["Fax"] + "";
                    this._sMobile = drList["Mobile"] + "";
                    this._iSendSMS = Convert.ToInt32(drList["SendSMS"]);
                    this._sEMail = drList["EMail"] + "";
                    this._iConnectionMethod = Convert.ToInt32(drList["ConnectionMethod"]);
                    this._iRisk = Convert.ToInt32(drList["Risk"]);
                    this._sMerida = drList["Merida"] + "";
                    this._sLogAxion = drList["LogAxion"] + "";
                    this._sInvName = drList["InvName"] + "";
                    this._sInvAddress = drList["InvAddress"] + "";
                    this._sInvCity = drList["InvCity"] + "";
                    this._sInvZip = drList["InvZip"] + "";
                    this._iInvCountry_ID = Convert.ToInt32(drList["InvCountry_ID"]);
                    this._sInvDOY = drList["InvDOY"] + "";
                    this._sInvAFM = drList["InvAFM"] + "";
                    this._iChkComplex = Convert.ToInt32(drList["ChkComplex"]);
                    this._iChkWorld = Convert.ToInt32(drList["ChkWorld"]);
                    this._iChkGreece = Convert.ToInt32(drList["ChkGreece"]);
                    this._iChkEurope = Convert.ToInt32(drList["ChkEurope"]);
                    this._iChkAmerica = Convert.ToInt32(drList["ChkAmerica"]);
                    this._iChkAsia = Convert.ToInt32(drList["ChkAsia"]);
                    this._sIncomeProducts = drList["IncomeProducts"] + "";
                    this._sCapitalProducts = drList["CapitalProducts"] + "";
                    this._iChkSpecificConstraints = Convert.ToInt32(drList["ChkSpecificConstraints"]);
                    this._iChkMonetaryRisk = Convert.ToInt32(drList["ChkMonetaryRisk"]);
                    this._iChkIndividualBonds = Convert.ToInt32(drList["ChkIndividualBonds"]);
                    this._iChkMutualFunds = Convert.ToInt32(drList["ChkMutualFunds"]);
                    this._iChkBondedETFs = Convert.ToInt32(drList["ChkBondedETFs"]);
                    this._iChkIndividualShares = Convert.ToInt32(drList["ChkIndividualShares"]);
                    this._iChkMixedFunds = Convert.ToInt32(drList["ChkMixedFunds"]);
                    this._iChkMixedETFs = Convert.ToInt32(drList["ChkMixedETFs"]);
                    this._iChkFunds = Convert.ToInt32(drList["ChkFunds"]);
                    this._iChkETFs = Convert.ToInt32(drList["ChkETFs"]);
                    this._iChkInvestmentGrade = Convert.ToInt32(drList["ChkInvestmentGrade"]);
                    this._sMiscInstructions = drList["MiscInstructions"] + "";
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            _dtList = new DataTable("Contract_Details_List");
            dtCol = _dtList.Columns.Add("ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Contract_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("InvestmentPolicy_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("AgreementNotes", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("PerformanceFees", Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("User1_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("User2_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("User3_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("User4_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Surname", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Firstname", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("SurnameFather", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("FirstnameFather", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("SurnameMother", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("FirstnameMother", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("SurnameSizigo", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("FirstnameSizigo", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Division", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Brunch_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Spec_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("DoB", Type.GetType("System.DateTime"));
            dtCol = _dtList.Columns.Add("BornPlace", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Citizen_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Sex", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ADT", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ExpireDate", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Police", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("DOY", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("AFM", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("DOY2", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("AFM2", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("AMKA", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("VAT_Percent", Type.GetType("System.Single"));
            dtCol = _dtList.Columns.Add("CountryTaxes_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Address", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("City", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Zip", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Country_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("Tel", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Fax", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("Mobile", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("SendSMS", Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("EMail", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("ConnectionMethod", Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("Risk", Type.GetType("System.Int16"));
            dtCol = _dtList.Columns.Add("Merida", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("LogAxion", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("InvName", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("InvAddress", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("InvCity", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("InvZip", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("InvCountry_ID", Type.GetType("System.Int32"));
            dtCol = _dtList.Columns.Add("InvDOY", Type.GetType("System.String"));
            dtCol = _dtList.Columns.Add("InvAFM", Type.GetType("System.String"));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Contracts_Details"));
                cmd.Parameters.Add(new SqlParameter("@Col", "User4_ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", _iUser4_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", "ID"));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = this._dtList.NewRow();
                    this.dtRow["ID"] = drList["ID"];
                    this.dtRow["Contract_ID"] = drList["Contract_ID"];
                    this.dtRow["InvestmentPolicy_ID"] = drList["InvestmentPolicy_ID"];
                    this.dtRow["AgreementNotes"] = drList["AgreementNotes"];
                    this.dtRow["PerformanceFees"] = drList["PerformanceFees"];
                    this.dtRow["User1_ID"] = drList["User1_ID"];
                    this.dtRow["User2_ID"] = drList["User2_ID"];
                    this.dtRow["User3_ID"] = drList["User3_ID"];
                    this.dtRow["User4_ID"] = drList["User4_ID"];
                    this.dtRow["Surname"] = drList["Surname"]+ "";
                    this.dtRow["Firstname"] = drList["Firstname"] + "";
                    this.dtRow["SurnameFather"] = drList["SurnameFather"] + "";
                    this.dtRow["FirstnameFather"] = drList["FirstnameFather"] + "";
                    this.dtRow["SurnameMother"] = drList["SurnameMother"] + "";
                    this.dtRow["FirstnameMother"] = drList["FirstnameMother"] + "";
                    this.dtRow["SurnameSizigo"] = drList["SurnameSizigo"] + "";
                    this.dtRow["FirstnameSizigo"] = drList["FirstnameSizigo"] + "";
                    this.dtRow["Division"] = drList["Division"];
                    this.dtRow["Brunch_ID"] = drList["Brunch_ID"];
                    this.dtRow["Spec_ID"] = drList["Spec_ID"];
                    this.dtRow["DoB"] = drList["DoB"];
                    this.dtRow["BornPlace"] = drList["BornPlace"];
                    this.dtRow["Citizen_ID"] = drList["Citizen_ID"];
                    this.dtRow["Sex"] = drList["Sex"] + "";
                    this.dtRow["ADT"] = drList["ADT"] + "";
                    this.dtRow["ExpireDate"] = drList["ExpireDate"] + "";
                    this.dtRow["Police"] = drList["Police"] + "";
                    this.dtRow["DOY"] = drList["DOY"] + "";
                    this.dtRow["AFM"] = drList["AFM"] + "";
                    this.dtRow["DOY2"] = drList["DOY2"] + "";
                    this.dtRow["AFM2"] = drList["AFM2"] + "";
                    this.dtRow["AMKA"] = drList["AMKA"] + "";
                    this.dtRow["VAT_Percent"] = drList["VAT_Percent"];
                    this.dtRow["CountryTaxes_ID"] = drList["CountryTaxes_ID"];
                    this.dtRow["Address"] = drList["Address"] + "";
                    this.dtRow["City"] = drList["City"] + "";
                    this.dtRow["Zip"] = drList["Zip"] + "";
                    this.dtRow["Country_ID"] = drList["Country_ID"];
                    this.dtRow["Tel"] = drList["Tel"] + "";
                    this.dtRow["Fax"] = drList["Fax"] + "";
                    this.dtRow["Mobile"] = drList["Mobile"] + "";
                    this.dtRow["SendSMS"] = drList["SendSMS"];
                    this.dtRow["EMail"] = drList["EMail"] + "";
                    this.dtRow["ConnectionMethod"] = drList["ConnectionMethod"];
                    this.dtRow["Risk"] = drList["Risk"];
                    this.dtRow["Merida"] = drList["Merida"] + "";
                    this.dtRow["LogAxion"] = drList["LogAxion"] + "";
                    this.dtRow["InvName"] = drList["InvName"] + "";
                    this.dtRow["InvCity"] = drList["InvCity"] + "";
                    this.dtRow["InvZip"] = drList["InvZip"] + "";
                    this.dtRow["InvCountry_ID"] = drList["InvCountry_ID"];
                    this.dtRow["InvDOY"] = drList["InvDOY"] + "";
                    this.dtRow["InvAFM"] = drList["InvAFM"] + "";
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
                using (SqlCommand cmd = new SqlCommand("InsertContract_Details", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = this._iContract_ID;
                    cmd.Parameters.Add("@InvestmentPolicy_ID", SqlDbType.Int).Value = this._iInvestmentPolicy_ID;
                    cmd.Parameters.Add("@AgreementNotes", SqlDbType.NVarChar, 1000).Value = this._sAgreementNotes;
                    cmd.Parameters.Add("@PerformanceFees", SqlDbType.Int).Value = this._iPerformanceFees;
                    cmd.Parameters.Add("@User1_ID", SqlDbType.Int).Value = this._iUser1_ID;
                    cmd.Parameters.Add("@User2_ID", SqlDbType.Int).Value = this._iUser2_ID;
                    cmd.Parameters.Add("@User3_ID", SqlDbType.Int).Value = this._iUser3_ID;
                    cmd.Parameters.Add("@User4_ID", SqlDbType.Int).Value = this._iUser4_ID;
                    cmd.Parameters.Add("@Surname", SqlDbType.NVarChar, 100).Value = this._sSurname;
                    cmd.Parameters.Add("@Firstname", SqlDbType.NVarChar, 40).Value = this._sFirstname;
                    cmd.Parameters.Add("@SurnameFather", SqlDbType.NVarChar, 100).Value = this._sSurnameFather;
                    cmd.Parameters.Add("@FirstnameFather", SqlDbType.NVarChar, 40).Value = this._sFirstnameFather;
                    cmd.Parameters.Add("@SurnameMother", SqlDbType.NVarChar, 100).Value = this._sSurnameMother;
                    cmd.Parameters.Add("@FirstnameMother", SqlDbType.NVarChar, 40).Value = this._sFirstnameMother;
                    cmd.Parameters.Add("@SurnameSizigo", SqlDbType.NVarChar, 100).Value = this._sSurnameSizigo;
                    cmd.Parameters.Add("@FirstnameSizigo", SqlDbType.NVarChar, 40).Value = this._sFirstnameSizigo;
                    cmd.Parameters.Add("@MIFIDCategory_ID", SqlDbType.Int).Value = this._iMIFIDCategory_ID;
                    cmd.Parameters.Add("@Division", SqlDbType.Int).Value = this._iDivision;
                    cmd.Parameters.Add("@Brunch_ID", SqlDbType.Int).Value = this._iBrunch_ID;
                    cmd.Parameters.Add("@Spec_ID", SqlDbType.Int).Value = this._iSpec_ID;
                    cmd.Parameters.Add("@DoB", SqlDbType.DateTime).Value = this._dDoB;
                    cmd.Parameters.Add("@BornPlace", SqlDbType.NVarChar, 50).Value = this._sBornPlace;
                    cmd.Parameters.Add("@Citizen_ID", SqlDbType.Int).Value = this._iCitizen_ID;
                    cmd.Parameters.Add("@Sex", SqlDbType.NVarChar, 6).Value = this._sSex;
                    cmd.Parameters.Add("@ADT", SqlDbType.NVarChar, 30).Value = this._sADT;
                    cmd.Parameters.Add("@ExpireDate", SqlDbType.NVarChar, 20).Value = this._sExpireDate;
                    cmd.Parameters.Add("@Police", SqlDbType.NVarChar, 50).Value = this._sPolice;
                    cmd.Parameters.Add("@Passport", SqlDbType.NVarChar, 30).Value = this._sPassport;
                    cmd.Parameters.Add("@Passport_ExpireDate", SqlDbType.NVarChar, 20).Value = this._sPassport_ExpireDate;
                    cmd.Parameters.Add("@Passport_Police", SqlDbType.NVarChar, 50).Value = this._sPassport_Police;
                    cmd.Parameters.Add("@DOY", SqlDbType.NVarChar, 30).Value = this._sDOY;
                    cmd.Parameters.Add("@AFM", SqlDbType.NVarChar, 12).Value = this._sAFM;
                    cmd.Parameters.Add("@DOY2", SqlDbType.NVarChar, 30).Value = this._sDOY2;
                    cmd.Parameters.Add("@AFM2", SqlDbType.NVarChar, 20).Value = this._sAFM2;
                    cmd.Parameters.Add("@AMKA", SqlDbType.NVarChar, 30).Value = this._sAMKA;
                    cmd.Parameters.Add("@VAT_Percent", SqlDbType.Float).Value = this._sgVAT_Percent;
                    cmd.Parameters.Add("@CountryTaxes_ID", SqlDbType.Int).Value = this._iCountryTaxes_ID;
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 100).Value = this._sAddress;
                    cmd.Parameters.Add("@City", SqlDbType.NVarChar, 30).Value = this._sCity;
                    cmd.Parameters.Add("@Zip", SqlDbType.NVarChar, 20).Value = this._sZip;
                    cmd.Parameters.Add("@Country_ID", SqlDbType.Int).Value = this._iCountry_ID;
                    cmd.Parameters.Add("@Tel", SqlDbType.NVarChar, 30).Value = this._sTel;
                    cmd.Parameters.Add("@Fax", SqlDbType.NVarChar, 30).Value = this._sFax;
                    cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar, 30).Value = this._sMobile;
                    cmd.Parameters.Add("@SendSMS", SqlDbType.Int).Value = this._iSendSMS;
                    cmd.Parameters.Add("@EMail", SqlDbType.NVarChar, 80).Value = this._sEMail.ToLower();
                    cmd.Parameters.Add("@ConnectionMethod", SqlDbType.Int).Value = this._iConnectionMethod;
                    cmd.Parameters.Add("@Risk", SqlDbType.Int).Value = this._iRisk;
                    cmd.Parameters.Add("@Merida", SqlDbType.NVarChar, 30).Value = this._sMerida;
                    cmd.Parameters.Add("@LogAxion", SqlDbType.NVarChar, 30).Value = this._sLogAxion;
                    cmd.Parameters.Add("@InvName", SqlDbType.NVarChar, 100).Value = this._sInvName;
                    cmd.Parameters.Add("@InvAddress", SqlDbType.NVarChar, 100).Value = this._sInvAddress;
                    cmd.Parameters.Add("@InvCity", SqlDbType.NVarChar, 30).Value = this._sInvCity;
                    cmd.Parameters.Add("@InvZip", SqlDbType.NVarChar, 20).Value = this._sInvZip;
                    cmd.Parameters.Add("@InvCountry_ID", SqlDbType.Int).Value = this._iInvCountry_ID;
                    cmd.Parameters.Add("@InvDOY", SqlDbType.NVarChar, 30).Value = this._sInvDOY;
                    cmd.Parameters.Add("@InvAFM", SqlDbType.NVarChar, 12).Value = this._sInvAFM;
                    cmd.Parameters.Add("@ChkComplex", SqlDbType.Int).Value = this._iChkComplex;
                    cmd.Parameters.Add("@ChkWorld", SqlDbType.Int).Value = this._iChkWorld;
                    cmd.Parameters.Add("@ChkGreece", SqlDbType.Int).Value = this._iChkGreece;
                    cmd.Parameters.Add("@ChkEurope", SqlDbType.Int).Value = this._iChkEurope;
                    cmd.Parameters.Add("@ChkAmerica", SqlDbType.Int).Value = this._iChkAmerica;
                    cmd.Parameters.Add("@ChkAsia", SqlDbType.Int).Value = this._iChkAsia;
                    cmd.Parameters.Add("@IncomeProducts", SqlDbType.NVarChar, 20).Value = this._sIncomeProducts;
                    cmd.Parameters.Add("@CapitalProducts", SqlDbType.NVarChar, 20).Value = this._sCapitalProducts;
                    cmd.Parameters.Add("@ChkSpecificConstraints", SqlDbType.Int).Value = this._iChkSpecificConstraints;
                    cmd.Parameters.Add("@ChkMonetaryRisk", SqlDbType.Int).Value = this._iChkMonetaryRisk;
                    cmd.Parameters.Add("@ChkIndividualBonds", SqlDbType.Int).Value = this._iChkIndividualBonds;
                    cmd.Parameters.Add("@ChkMutualFunds", SqlDbType.Int).Value = this._iChkMutualFunds;
                    cmd.Parameters.Add("@ChkBondedETFs", SqlDbType.Int).Value = this._iChkBondedETFs;
                    cmd.Parameters.Add("@ChkIndividualShares", SqlDbType.Int).Value = this._iChkIndividualShares;
                    cmd.Parameters.Add("@ChkMixedFunds", SqlDbType.Int).Value = this._iChkMixedFunds;
                    cmd.Parameters.Add("@ChkMixedETFs", SqlDbType.Int).Value = this._iChkMixedETFs;
                    cmd.Parameters.Add("@ChkFunds", SqlDbType.Int).Value = this._iChkFunds;
                    cmd.Parameters.Add("@ChkETFs", SqlDbType.Int).Value = this._iChkETFs;
                    cmd.Parameters.Add("@ChkInvestmentGrade", SqlDbType.Int).Value = this._iChkInvestmentGrade;
                    cmd.Parameters.Add("@MiscInstructions", SqlDbType.NVarChar, 1000).Value = this._sMiscInstructions;
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
                using (SqlCommand cmd = new SqlCommand("EditContract_Details", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = this._iRecord_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = this._iContract_ID;
                    cmd.Parameters.Add("@InvestmentPolicy_ID", SqlDbType.Int).Value = this._iInvestmentPolicy_ID;
                    cmd.Parameters.Add("@AgreementNotes", SqlDbType.NVarChar, 1000).Value = this._sAgreementNotes;
                    cmd.Parameters.Add("@PerformanceFees", SqlDbType.Int).Value = this._iPerformanceFees;
                    cmd.Parameters.Add("@User1_ID", SqlDbType.Int).Value = this._iUser1_ID;
                    cmd.Parameters.Add("@User2_ID", SqlDbType.Int).Value = this._iUser2_ID;
                    cmd.Parameters.Add("@User3_ID", SqlDbType.Int).Value = this._iUser3_ID;
                    cmd.Parameters.Add("@User4_ID", SqlDbType.Int).Value = this._iUser4_ID;
                    cmd.Parameters.Add("@Surname", SqlDbType.NVarChar, 100).Value = this._sSurname;
                    cmd.Parameters.Add("@Firstname", SqlDbType.NVarChar, 40).Value = this._sFirstname;
                    cmd.Parameters.Add("@SurnameFather", SqlDbType.NVarChar, 100).Value = this._sSurnameFather;
                    cmd.Parameters.Add("@FirstnameFather", SqlDbType.NVarChar, 40).Value = this._sFirstnameFather;
                    cmd.Parameters.Add("@SurnameMother", SqlDbType.NVarChar, 100).Value = this._sSurnameMother;
                    cmd.Parameters.Add("@FirstnameMother", SqlDbType.NVarChar, 40).Value = this._sFirstnameMother;
                    cmd.Parameters.Add("@SurnameSizigo", SqlDbType.NVarChar, 100).Value = this._sSurnameSizigo;
                    cmd.Parameters.Add("@FirstnameSizigo", SqlDbType.NVarChar, 40).Value = this._sFirstnameSizigo;
                    cmd.Parameters.Add("@MIFIDCategory_ID", SqlDbType.Int).Value = this._iMIFIDCategory_ID;
                    cmd.Parameters.Add("@Division", SqlDbType.Int).Value = this._iDivision;
                    cmd.Parameters.Add("@Brunch_ID", SqlDbType.Int).Value = this._iBrunch_ID;
                    cmd.Parameters.Add("@Spec_ID", SqlDbType.Int).Value = this._iSpec_ID;
                    cmd.Parameters.Add("@DoB", SqlDbType.DateTime).Value = this._dDoB;
                    cmd.Parameters.Add("@BornPlace", SqlDbType.NVarChar, 50).Value = this._sBornPlace;
                    cmd.Parameters.Add("@Citizen_ID", SqlDbType.Int).Value = this._iCitizen_ID;
                    cmd.Parameters.Add("@Sex", SqlDbType.NVarChar, 6).Value = this._sSex;
                    cmd.Parameters.Add("@ADT", SqlDbType.NVarChar, 30).Value = this._sADT;
                    cmd.Parameters.Add("@ExpireDate", SqlDbType.NVarChar, 20).Value = this._sExpireDate;
                    cmd.Parameters.Add("@Police", SqlDbType.NVarChar, 50).Value = this._sPolice;
                    cmd.Parameters.Add("@Passport", SqlDbType.NVarChar, 30).Value = this._sPassport;
                    cmd.Parameters.Add("@Passport_ExpireDate", SqlDbType.NVarChar, 20).Value = this._sPassport_ExpireDate;
                    cmd.Parameters.Add("@Passport_Police", SqlDbType.NVarChar, 50).Value = this._sPassport_Police;
                    cmd.Parameters.Add("@DOY", SqlDbType.NVarChar, 30).Value = this._sDOY;
                    cmd.Parameters.Add("@AFM", SqlDbType.NVarChar, 12).Value = this._sAFM;
                    cmd.Parameters.Add("@DOY2", SqlDbType.NVarChar, 30).Value = this._sDOY2;
                    cmd.Parameters.Add("@AFM2", SqlDbType.NVarChar, 20).Value = this._sAFM2;
                    cmd.Parameters.Add("@AMKA", SqlDbType.NVarChar, 30).Value = this._sAMKA;
                    cmd.Parameters.Add("@VAT_Percent", SqlDbType.Float).Value = this._sgVAT_Percent;
                    cmd.Parameters.Add("@CountryTaxes_ID", SqlDbType.Int).Value = this._iCountryTaxes_ID;
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 100).Value = this._sAddress;
                    cmd.Parameters.Add("@City", SqlDbType.NVarChar, 30).Value = this._sCity;
                    cmd.Parameters.Add("@Zip", SqlDbType.NVarChar, 20).Value = this._sZip;
                    cmd.Parameters.Add("@Country_ID", SqlDbType.Int).Value = this._iCountry_ID;
                    cmd.Parameters.Add("@Tel", SqlDbType.NVarChar, 30).Value = this._sTel;
                    cmd.Parameters.Add("@Fax", SqlDbType.NVarChar, 30).Value = this._sFax;
                    cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar, 30).Value = this._sMobile;
                    cmd.Parameters.Add("@SendSMS", SqlDbType.Int).Value = this._iSendSMS;
                    cmd.Parameters.Add("@EMail", SqlDbType.NVarChar, 80).Value = this._sEMail.ToLower();
                    cmd.Parameters.Add("@ConnectionMethod", SqlDbType.Int).Value = this._iConnectionMethod;
                    cmd.Parameters.Add("@Risk", SqlDbType.Int).Value = this._iRisk;
                    cmd.Parameters.Add("@Merida", SqlDbType.NVarChar, 30).Value = this._sMerida;
                    cmd.Parameters.Add("@LogAxion", SqlDbType.NVarChar, 30).Value = this._sLogAxion;
                    cmd.Parameters.Add("@InvName", SqlDbType.NVarChar, 100).Value = this._sInvName;
                    cmd.Parameters.Add("@InvAddress", SqlDbType.NVarChar, 100).Value = this._sInvAddress;
                    cmd.Parameters.Add("@InvCity", SqlDbType.NVarChar, 30).Value = this._sInvCity;
                    cmd.Parameters.Add("@InvZip", SqlDbType.NVarChar, 20).Value = this._sInvZip;
                    cmd.Parameters.Add("@InvCountry_ID", SqlDbType.Int).Value = this._iInvCountry_ID;
                    cmd.Parameters.Add("@InvDOY", SqlDbType.NVarChar, 30).Value = this._sInvDOY;
                    cmd.Parameters.Add("@InvAFM", SqlDbType.NVarChar, 12).Value = this._sInvAFM;
                    cmd.Parameters.Add("@ChkComplex", SqlDbType.Int).Value = this._iChkComplex;
                    cmd.Parameters.Add("@ChkWorld", SqlDbType.Int).Value = this._iChkWorld;
                    cmd.Parameters.Add("@ChkGreece", SqlDbType.Int).Value = this._iChkGreece;
                    cmd.Parameters.Add("@ChkEurope", SqlDbType.Int).Value = this._iChkEurope;
                    cmd.Parameters.Add("@ChkAmerica", SqlDbType.Int).Value = this._iChkAmerica;
                    cmd.Parameters.Add("@ChkAsia", SqlDbType.Int).Value = this._iChkAsia;
                    cmd.Parameters.Add("@IncomeProducts", SqlDbType.NVarChar, 20).Value = this._sIncomeProducts;
                    cmd.Parameters.Add("@CapitalProducts", SqlDbType.NVarChar, 20).Value = this._sCapitalProducts;
                    cmd.Parameters.Add("@ChkSpecificConstraints", SqlDbType.Int).Value = this._iChkSpecificConstraints;
                    cmd.Parameters.Add("@ChkMonetaryRisk", SqlDbType.Int).Value = this._iChkMonetaryRisk;
                    cmd.Parameters.Add("@ChkIndividualBonds", SqlDbType.Int).Value = this._iChkIndividualBonds;
                    cmd.Parameters.Add("@ChkMutualFunds", SqlDbType.Int).Value = this._iChkMutualFunds;
                    cmd.Parameters.Add("@ChkBondedETFs", SqlDbType.Int).Value = this._iChkBondedETFs;
                    cmd.Parameters.Add("@ChkIndividualShares", SqlDbType.Int).Value = this._iChkIndividualShares;
                    cmd.Parameters.Add("@ChkMixedFunds", SqlDbType.Int).Value = this._iChkMixedFunds;
                    cmd.Parameters.Add("@ChkMixedETFs", SqlDbType.Int).Value = this._iChkMixedETFs;
                    cmd.Parameters.Add("@ChkFunds", SqlDbType.Int).Value = this._iChkFunds;
                    cmd.Parameters.Add("@ChkETFs", SqlDbType.Int).Value = this._iChkETFs;
                    cmd.Parameters.Add("@ChkInvestmentGrade", SqlDbType.Int).Value = this._iChkInvestmentGrade;
                    cmd.Parameters.Add("@MiscInstructions", SqlDbType.NVarChar, 1000).Value = this._sMiscInstructions;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Contracts_Details";
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
        public int InvestmentPolicy_ID { get { return this._iInvestmentPolicy_ID; } set { this._iInvestmentPolicy_ID = value; } }        
        public string InvestmentPolicy_Title { get { return this._sInvestmentPolicy_Title; } set { this._sInvestmentPolicy_Title = value; } }
        public string AgreementNotes { get { return this._sAgreementNotes; } set { this._sAgreementNotes = value; } }
        public int PerformanceFees { get { return this._iPerformanceFees; } set { this._iPerformanceFees = value; } }
        public int User1_ID { get { return this._iUser1_ID; } set { this._iUser1_ID = value; } }
        public string Advisory_Name { get { return this._sAdvisory_Name; } set { this._sAdvisory_Name = value; } }
        public int User2_ID { get { return this._iUser2_ID; } set { this._iUser2_ID = value; } }
        public string RM_Name { get { return this._sRM_Name; } set { this._sRM_Name = value; } }
        public int User3_ID { get { return this._iUser3_ID; } set { this._iUser3_ID = value; } }
        public string Introducer_Name { get { return this._sIntroducer_Name; } set { this._sIntroducer_Name = value; } }
        public int User4_ID { get { return this._iUser4_ID; } set { this._iUser4_ID = value; } }
        public string Diaxiristis_Name { get { return this._sDiaxiristis_Name; } set { this._sDiaxiristis_Name = value; } }
        public string Surname { get { return this._sSurname; } set { this._sSurname = value; } }
        public string Firstname { get { return this._sFirstname; } set { this._sFirstname = value; } }
        public string SurnameFather { get { return this._sSurnameFather; } set { this._sSurnameFather = value; } }
        public string FirstnameFather { get { return this._sFirstnameFather; } set { this._sFirstnameFather = value; } }
        public string SurnameMother { get { return this._sSurnameMother; } set { this._sSurnameMother = value; } }
        public string FirstnameMother { get { return this._sFirstnameMother; } set { this._sFirstnameMother = value; } }
        public string SurnameSizigo { get { return this._sSurnameSizigo; } set { this._sSurnameSizigo = value; } }
        public string FirstnameSizigo { get { return this._sFirstnameSizigo; } set { this._sFirstnameSizigo = value; } }
        public int MIFIDCategory_ID { get { return this._iMIFIDCategory_ID; } set { this._iMIFIDCategory_ID = value; } }
        public int Division { get { return this._iDivision; } set { this._iDivision = value; } }
        public int Brunch_ID { get { return this._iBrunch_ID; } set { this._iBrunch_ID = value; } }
        public int Spec_ID { get { return this._iSpec_ID; } set { this._iSpec_ID = value; } }
        public DateTime DoB { get { return this._dDoB; } set { this._dDoB = value; } }
        public string BornPlace { get { return this._sBornPlace; } set { this._sBornPlace = value; } }
        public int Citizen_ID { get { return this._iCitizen_ID; } set { this._iCitizen_ID = value; } }
        public string Sex { get { return this._sSex; } set { this._sSex = value; } }
        public string ADT { get { return this._sADT; } set { this._sADT = value; } }
        public string ExpireDate { get { return this._sExpireDate; } set { this._sExpireDate = value; } }
        public string Police { get { return this._sPolice; } set { this._sPolice = value; } }
        public string Passport { get { return this._sPassport; } set { this._sPassport = value; } }
        public string Passport_ExpireDate { get { return this._sPassport_ExpireDate; } set { this._sPassport_ExpireDate = value; } }
        public string Passport_Police { get { return this._sPassport_Police; } set { this._sPassport_Police = value; } }
        public string DOY { get { return this._sDOY; } set { this._sDOY = value; } }
        public string AFM { get { return this._sAFM; } set { this._sAFM = value; } }
        public string DOY2 { get { return this._sDOY2; } set { this._sDOY2 = value; } }
        public string AFM2 { get { return this._sAFM2; } set { this._sAFM2 = value; } }
        public string AMKA { get { return this._sAMKA; } set { this._sAMKA = value; } }
        public float VAT_Percent { get { return this._sgVAT_Percent; } set { this._sgVAT_Percent = value; } }
        public int CountryTaxes_ID { get { return this._iCountryTaxes_ID; } set { this._iCountryTaxes_ID = value; } }
        public string Address { get { return this._sAddress; } set { this._sAddress = value; } }
        public string City { get { return this._sCity; } set { this._sCity = value; } }
        public string Zip { get { return this._sZip; } set { this._sZip = value; } }
        public int Country_ID { get { return this._iCountry_ID; } set { this._iCountry_ID = value; } }
        public string Tel { get { return this._sTel; } set { this._sTel = value; } }
        public string Fax { get { return this._sFax; } set { this._sFax = value; } }
        public string Mobile { get { return this._sMobile; } set { this._sMobile = value; } }
        public int SendSMS { get { return this._iSendSMS; } set { this._iSendSMS = value; } }
        public string EMail { get { return this._sEMail; } set { this._sEMail = value; } }
        public int ConnectionMethod { get { return this._iConnectionMethod; } set { this._iConnectionMethod = value; } }
        public int Risk { get { return this._iRisk; } set { this._iRisk = value; } }
        public string Merida { get { return this._sMerida; } set { this._sMerida = value; } }
        public string LogAxion { get { return this._sLogAxion; } set { this._sLogAxion = value; } }
        public string InvName { get { return this._sInvName; } set { this._sInvName = value; } }
        public string InvAddress { get { return this._sInvAddress; } set { this._sInvAddress = value; } }
        public string InvCity { get { return this._sInvCity; } set { this._sInvCity = value; } }
        public string InvZip { get { return this._sInvZip; } set { this._sInvZip = value; } }
        public int InvCountry_ID { get { return this._iInvCountry_ID; } set { this._iInvCountry_ID = value; } }
        public string InvDOY { get { return this._sInvDOY; } set { this._sInvDOY = value; } }
        public string InvAFM { get { return this._sInvAFM; } set { this._sInvAFM = value; } }
        public int ChkComplex { get { return this._iChkComplex; } set { this._iChkComplex = value; } }
        public int ChkWorld { get { return this._iChkWorld; } set { this._iChkWorld = value; } }
        public int ChkGreece { get { return this._iChkGreece; } set { this._iChkGreece = value; } }
        public int ChkEurope { get { return this._iChkEurope; } set { this._iChkEurope = value; } }
        public int ChkAmerica { get { return this._iChkAmerica; } set { this._iChkAmerica = value; } }
        public int ChkAsia { get { return this._iChkAsia; } set { this._iChkAsia = value; } }
        public string IncomeProducts { get { return this._sIncomeProducts; } set { this._sIncomeProducts = value; } }
        public string CapitalProducts { get { return this._sCapitalProducts; } set { this._sCapitalProducts = value; } }
        public int ChkSpecificConstraints { get { return this._iChkSpecificConstraints; } set { this._iChkSpecificConstraints = value; } }
        public int ChkMonetaryRisk { get { return this._iChkMonetaryRisk; } set { this._iChkMonetaryRisk = value; } }
        public int ChkIndividualBonds { get { return this._iChkIndividualBonds; } set { this._iChkIndividualBonds = value; } }
        public int ChkMutualFunds { get { return this._iChkMutualFunds; } set { this._iChkMutualFunds = value; } }
        public int ChkBondedETFs { get { return this._iChkBondedETFs; } set { this._iChkBondedETFs = value; } }
        public int ChkIndividualShares { get { return this._iChkIndividualShares; } set { this._iChkIndividualShares = value; } }
        public int ChkMixedFunds  { get { return this._iChkMixedFunds; } set { this._iChkMixedFunds = value; } }
        public int ChkMixedETFs { get { return this._iChkMixedETFs; } set { this._iChkMixedETFs = value; } }
        public int ChkFunds { get { return this._iChkFunds; } set { this._iChkFunds = value; } }
        public int ChkETFs { get { return this._iChkETFs; } set { this._iChkETFs = value; } }
        public int ChkInvestmentGrade { get { return this._iChkInvestmentGrade; } set { this._iChkInvestmentGrade = value; } }
        public string MiscInstructions { get { return this._sMiscInstructions; } set { this._sMiscInstructions = value; } }
        public DataTable List  { get { return _dtList; } set { _dtList = value; } }
    }
}
