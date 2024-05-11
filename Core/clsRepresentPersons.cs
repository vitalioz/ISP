using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsRepresentPersons
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataRow dtRow;

        private int _iRecord_ID;
        private int _iClient_ID;
        private int _iContract_ID;
        private int _iAuthRep;
        private int _iOwner;
        private int _iLegalRep;
        private int _iDirector;
        private int _iSignature;

        private string _sSurname;
        private string _sFirstname;
        private string _sFather;
        private string _sFullName;
        private string _sADT;
        private string _sExpireDate;
        private string _sPolice;
        private string _sAFM;
        private string _sDOY;
        private string _sAddress;
        private string _sCity;
        private string _sZip;
        private int _iCountry_ID;
        private string _sTel;
        private string _sFax;
        private string _sMobile;
        private string _sEMail;
        private int _iMember_Index;
        private string _sCountry_Title;

        private DataTable _dtList;

        public clsRepresentPersons()
        {
            this._iRecord_ID = 0;
            this._iClient_ID = 0;
            this._iContract_ID = 0;
            this._iAuthRep = 0;
            this._iOwner = 0;
            this._iLegalRep = 0;
            this._iDirector = 0;
            this._iSignature = 0;

            this._sSurname = "";
            this._sFirstname = "";
            this._sFather = "";
            this._sFullName = "";
            this._sADT = "";
            this._sExpireDate = "";
            this._sPolice = "";
            this._sAFM = "";
            this._sDOY = "";
            this._sAddress = "";
            this._sCity = "";
            this._sZip = "";
            this._iCountry_ID = 0;
            this._sTel = "";
            this._sFax = "";
            this._sMobile = "";
            this._sEMail = "";
            this._iMember_Index = 0;
            this._sCountry_Title = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetContract_Represents", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ID", _iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                    this._iContract_ID = Convert.ToInt32(drList["Contract_ID"]);
                    this._sSurname = drList["Surname"] + "";
                    this._sFirstname = drList["Firstname"] + "";
                    this._sFather = drList["SurnameFather"] + "";
                    this._iAuthRep = Convert.ToInt32(drList["AuthRep"]);
                    this._iOwner = Convert.ToInt32(drList["Owner"]);
                    this._iLegalRep = Convert.ToInt32(drList["LegalRep"]);
                    this._iDirector = Convert.ToInt32(drList["Director"]);
                    this._iSignature = Convert.ToInt32(drList["Signature"]);
                    this._sADT = drList["ADT"] + "";
                    this._sExpireDate = drList["ExpireDate"] + "";
                    this._sPolice = drList["Police"] + "";
                    this._sAFM = drList["AFM"] + "";
                    this._sDOY = drList["DOY"] + "";
                    this._sAddress = drList["Address"] + "";
                    this._sCity = drList["City"] + "";
                    this._sZip = drList["Zip"] + "";
                    this._iCountry_ID = Convert.ToInt32(drList["Country_ID"]);
                    this._sTel = drList["Tel"] + "";
                    this._sFax = drList["Fax"] + "";
                    this._sMobile = drList["Mobile"] + "";
                    this._sEMail = drList["EMail"] + "";
                    this._sFullName = (drList["Surname"] + " " + drList["Firstname"]).Trim();
                    this._sCountry_Title = drList["Country_Title"] + "";
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            string sTemp = "";

            _dtList = new DataTable();
            _dtList.Columns.Add("ID", typeof(int));
            _dtList.Columns.Add("FullName", typeof(string));
            _dtList.Columns.Add("Properties", typeof(string));
            _dtList.Columns.Add("ADT", typeof(string));

            try
            {
                conn.Open();
                cmd = new SqlCommand("GetContracts_Represents_List", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Contract_ID", _iContract_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["FullName"] = (drList["Surname"] + " " + drList["Firstname"] + " " + drList["SurnameFather"]).Trim();

                    sTemp = "";
                    if (Convert.ToInt32(drList["AuthRep"]) == 1) sTemp = "Εξουσιοδοτούμενος Αντιπρόσωπος" + "\n";
                    if (Convert.ToInt32(drList["Owner"]) == 1) sTemp = sTemp + "Δικαιούχος της Εταιρίας" + "\n";
                    if (Convert.ToInt32(drList["LegalRep"]) == 1) sTemp = sTemp + "Νόμιμος Εκπρόσωπος" + "\n";
                    if (Convert.ToInt32(drList["Director"]) == 1) sTemp = sTemp + "Director" + "\n";
                    if (Convert.ToInt32(drList["Signature"]) == 1) sTemp = sTemp + "Δικαίωμα Υπογραφής/Εντολής" + "\n";
                    dtRow["Properties"] = sTemp;

                    dtRow["ADT"] = "Δελτίο Ταυτότητας: " + drList["ADT"] + "\n" +
                                 "Ημ/νία Έκδοσης: " + drList["ExpireDate"] + "\n" +
                                 "Αρχή Έκδοσης: " + drList["Police"];
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
                using (cmd = new SqlCommand("InsertContracts_Represents", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@AuthRep", SqlDbType.Int).Value = _iAuthRep;
                    cmd.Parameters.Add("@Owner", SqlDbType.Int).Value = _iOwner;
                    cmd.Parameters.Add("@LegalRep", SqlDbType.Int).Value = _iLegalRep;
                    cmd.Parameters.Add("@Director", SqlDbType.Int).Value = _iDirector;
                    cmd.Parameters.Add("@Signature", SqlDbType.Int).Value = _iSignature;
              
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
                using (cmd = new SqlCommand("EditContracts_Represents", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@AuthRep", SqlDbType.Int).Value = _iAuthRep;
                    cmd.Parameters.Add("@Owner", SqlDbType.Int).Value = _iOwner;
                    cmd.Parameters.Add("@LegalRep", SqlDbType.Int).Value = _iLegalRep;
                    cmd.Parameters.Add("@Director", SqlDbType.Int).Value = _iDirector;
                    cmd.Parameters.Add("@Signature", SqlDbType.Int).Value = _iSignature;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Contracts_Represents";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public int Client_ID { get { return this._iClient_ID; } set { this._iClient_ID = value; } }
        public int Contract_ID { get { return this._iContract_ID; } set { this._iContract_ID = value; } }
        public string Surname { get { return _sSurname; } set { _sSurname = value; } }
        public string Firstname { get { return _sFirstname; } set { _sFirstname = value; } }
        public string Father { get { return _sFather; } set { _sFather = value; } }
        public int AuthRep { get { return this._iAuthRep; } set { this._iAuthRep = value; } }
        public int Owner { get { return this._iOwner; } set { this._iOwner = value; } }
        public int LegalRep { get { return this._iLegalRep; } set { this._iLegalRep = value; } }
        public int Director { get { return this._iDirector; } set { this._iDirector = value; } }
        public int Signature { get { return this._iSignature; } set { this._iSignature = value; } }
        public string ADT { get { return _sADT; } set { _sADT = value; } }
        public string ExpireDate { get { return _sExpireDate; } set { _sExpireDate = value; } }
        public string Police { get { return _sPolice; } set { _sPolice = value; } }
        public string AFM { get { return _sAFM; } set { _sAFM = value; } }
        public string DOY { get { return _sDOY; } set { _sDOY = value; } }
        public string Address { get { return _sAddress; } set { _sAddress = value; } }
        public string City { get { return _sCity; } set { _sCity = value; } }
        public string Zip { get { return _sZip; } set { _sZip = value; } }
        public int Country_ID { get { return this._iCountry_ID; } set { this._iCountry_ID = value; } }
        public string Tel { get { return _sTel; } set { _sTel = value; } }
        public string Fax { get { return _sFax; } set { _sFax = value; } }
        public string Mobile { get { return _sMobile; } set { _sMobile = value; } }
        public string EMail { get { return _sEMail; } set { _sEMail = value; } }
        public string Country_Title { get { return _sCountry_Title; } set { _sCountry_Title = value; } }        
        public DataTable List { get { return _dtList; } set { _dtList = value; } }

    }
}
