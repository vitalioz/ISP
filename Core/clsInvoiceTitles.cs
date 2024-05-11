using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsInvoiceTitles
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int _iRecord_ID;
        private DateTime _dDateIssued;
        private int _iTipos;
        private string _sSeira;
        private int _iArithmos;
        private string _sSelida;
        private int _iClient_ID;
        private int _iTroposApostolis;
        private int _iTroposPliromis;
        private float _sgPosotita;
        private float _sgAxiaMikti;
        private float _sgEkptosi;
        private float _sgAxiaKathari;
        private float _sgAxiaFPA;
        private float _sgAxiaTeliki;
        private string _sFileName;
        private int    _iSourceType;
        private int    _iSource_ID;
        private int    _iContract_ID;
        private string _sOfficialInformingDate;
        private int _iAuthor_ID;
        private DateTime _dDateIns;

        private DateTime _dFrom;
        private DateTime _dTo;
        private DateTime _dIssuedFrom;
        private DateTime _dIssuedTo;
        private DataTable _dtList;

        public clsInvoiceTitles()
        {
            this._iRecord_ID = 0;
            this._dDateIssued = Convert.ToDateTime("1900/01/01");
            this._iTipos = 0;
            this._sSeira = "";
            this._iArithmos = 0;
            this._sSelida = "";
            this._iClient_ID = 0;
            this._iTroposApostolis = 0;
            this._iTroposPliromis = 0;
            this._sgPosotita = 0;
            this._sgAxiaMikti = 0;
            this._sgEkptosi = 0;
            this._sgAxiaKathari = 0;
            this._sgAxiaFPA = 0;
            this._sgAxiaTeliki = 0;
            this._sFileName = "";
            this._iSourceType = 0;
            this._iSource_ID = 0;
            this._iContract_ID = 0;
            this._sOfficialInformingDate = "";
            this._iAuthor_ID = 0;
            this._dDateIns = Convert.ToDateTime("1900/01/01");
            this._dFrom = Convert.ToDateTime("1900/01/01");
            this._dTo = Convert.ToDateTime("1900/01/01");
            this._dIssuedFrom = Convert.ToDateTime("1900/01/01");
            this._dIssuedTo = Convert.ToDateTime("1900/01/01");
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetTable", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Table", "Invoice_Titles"));
                cmd.Parameters.Add(new SqlParameter("@Col", "ID"));
                cmd.Parameters.Add(new SqlParameter("@Value", this._iRecord_ID));
                cmd.Parameters.Add(new SqlParameter("@Order", ""));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._dDateIssued = Convert.ToDateTime(drList["DateIssued"]);
                    this._iTipos = Convert.ToInt32(drList["Tipos"]);
                    this._sSeira = drList["Seira"] + "";
                    this._iArithmos = Convert.ToInt32(drList["Arithmos"]);
                    this._sSelida = drList["Selida"] + "";
                    this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                    this._iTroposApostolis = Convert.ToInt16(drList["TroposApostolis"]);
                    this._iTroposPliromis = Convert.ToInt16(drList["TroposPliromis"]);
                    this._sgPosotita = Convert.ToSingle(drList["Posotita"]);
                    this._sgAxiaMikti = Convert.ToSingle(drList["AxiaMikti"]);
                    this._sgEkptosi = Convert.ToSingle(drList["Ekptosi"]);
                    this._sgAxiaKathari = Convert.ToSingle(drList["AxiaKathari"]);
                    this._sgAxiaFPA = Convert.ToSingle(drList["AxiaFPA"]);
                    this._sgAxiaTeliki = Convert.ToSingle(drList["AxiaTeliki"]);
                    this._sFileName = drList["FileName"] + "";
                    this._iSourceType = Convert.ToInt32(drList["SourceType"]);
                    this._iSource_ID = Convert.ToInt32(drList["Source_ID"]);
                    this._iContract_ID = Convert.ToInt32(drList["Contract_ID"]);
                    this._sOfficialInformingDate = drList["OfficialInformingDate"] + "";
                    this._iAuthor_ID = Convert.ToInt32(drList["Author_ID"]);
                    this._dDateIns = Convert.ToDateTime(drList["DateIns"]);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int GetInvoice_LastNumber()
        {
            int iLastNum = 0;
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetInvoices_LastNumber", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Tipos", _iTipos));
                cmd.Parameters.Add(new SqlParameter("@Seira", _sSeira));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    if (!String.IsNullOrEmpty(drList["LastNumber"].ToString())) iLastNum = Convert.ToInt32(drList["LastNumber"]);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }

            return iLastNum;
        }
        public void GetList()
        {
            try
            {
                _dtList = new DataTable("InvoiceTitles_List");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DateIssued", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("InvoiceNum", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Description", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ClientName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("AFM", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DOY", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Address", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("City", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Zip", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Country_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Posotita", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AxiaKathari", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AxiaFPA", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("AxiaTeliki", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("FileName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ImageType", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ClientTipos", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ContractTipos", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Code", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Portfolio", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SourceType", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Service_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ServiceProvider_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ServiceProvider_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("SubPath", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("DateIns", System.Type.GetType("System.DateTime"));

                conn.Open();
                cmd = new SqlCommand("GetInvoicesList", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dFrom));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dTo));
                cmd.Parameters.Add(new SqlParameter("@DateIssuedFrom", _dIssuedFrom));
                cmd.Parameters.Add(new SqlParameter("@DateIssuedTo", _dIssuedTo));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["DateIssued"] = drList["DateIssued"];
                    dtRow["InvoiceNum"] = (drList["InvoiceCode"] + " " + drList["Seira"]).Trim() + " " + drList["Arithmos"];
                    dtRow["Description"] = "";
                    dtRow["Client_ID"] = drList["Client_ID"];
                    if (Convert.ToInt32(drList["ClientTipos"]) == 1) dtRow["ClientName"] = (drList["Surname"] + " " + drList["Firstname"]).Trim();
                    else dtRow["ClientName"] = drList["Surname"] + "";
                    dtRow["AFM"] = drList["AFM"]+"";
                    dtRow["DOY"] = drList["DOY"] + "";                    
                    dtRow["Address"] = drList["Address"] + "";
                    dtRow["City"] = drList["City"] + "";
                    dtRow["Zip"] = drList["Zip"] + "";
                    dtRow["Country_Title"] = drList["Country_Title"] + "";
                    dtRow["Posotita"] = drList["Posotita"];
                    dtRow["AxiaKathari"] = drList["AxiaKathari"];
                    dtRow["AxiaFPA"] = drList["AxiaFPA"];
                    dtRow["AxiaTeliki"] = drList["AxiaTeliki"];
                    if (drList["FileName"].ToString() != "")
                    {
                        this.dtRow["ImageType"] = 1;
                        this.dtRow["FileName"] = drList["FileName"] + "";
                    }
                    else
                    {
                        this.dtRow["ImageType"] = 0;
                        this.dtRow["FileName"] = "";
                    }
                    dtRow["ClientTipos"] = drList["ClientTipos"];
                    dtRow["ContractTipos"] = drList["ContractTipos"];
                    dtRow["Code"] = drList["Code"] + "";
                    dtRow["Portfolio"] = drList["Portfolio"] + "";
                    dtRow["SourceType"] = drList["SourceType"];
                    dtRow["Service_ID"] = drList["Service_ID"];
                    dtRow["ServiceProvider_ID"] = drList["ServiceProvider_ID"];
                    dtRow["ServiceProvider_Title"] = drList["ServiceProvider_Title"] + "";
                    if (Convert.ToInt32(drList["ContractTipos"]) == 1) dtRow["SubPath"] = drList["ContractTitle"] + "";                     // Joint, KEM, Koinos
                    else if (Convert.ToInt32(drList["ClientTipos"]) == 2) dtRow["SubPath"] = (drList["ContractTitle"] + "").Trim();         // Company
                         else dtRow["SubPath"] = (drList["Surname"] + " " + drList["Firstname"]).Trim();                                      // Person
                    dtRow["DateIns"] = drList["DateIns"];
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
                using (cmd = new SqlCommand("InsertInvoice_Title", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@DateIssued", SqlDbType.DateTime).Value = _dDateIssued;
                    cmd.Parameters.Add("@Tipos", SqlDbType.Int).Value = _iTipos;
                    cmd.Parameters.Add("@Seira", SqlDbType.NVarChar, 10).Value = _sSeira;
                    cmd.Parameters.Add("@Arithmos", SqlDbType.Int).Value = _iArithmos;
                    cmd.Parameters.Add("@Selida", SqlDbType.NVarChar, 10).Value = _sSelida;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@TroposApostolis", SqlDbType.Int).Value = _iTroposApostolis;
                    cmd.Parameters.Add("@TroposPliromis", SqlDbType.Int).Value = _iTroposPliromis;
                    cmd.Parameters.Add("@Posotita", SqlDbType.Float).Value = _sgPosotita;
                    cmd.Parameters.Add("@AxiaMikti", SqlDbType.Float).Value = _sgAxiaMikti;
                    cmd.Parameters.Add("@Ekptosi", SqlDbType.Float).Value = _sgEkptosi;
                    cmd.Parameters.Add("@AxiaKathari", SqlDbType.Float).Value = _sgAxiaKathari;
                    cmd.Parameters.Add("@AxiaFPA", SqlDbType.Float).Value = _sgAxiaFPA;
                    cmd.Parameters.Add("@AxiaTeliki", SqlDbType.Float).Value = _sgAxiaTeliki;
                    cmd.Parameters.Add("@FileName", SqlDbType.NVarChar, 50).Value = _sFileName;
                    cmd.Parameters.Add("@SourceType", SqlDbType.Int).Value = _iSourceType;
                    cmd.Parameters.Add("@Source_ID", SqlDbType.Int).Value = _iSource_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@OfficialInformingDate", SqlDbType.NVarChar, 20).Value = _sOfficialInformingDate;
                    cmd.Parameters.Add("@Author_ID", SqlDbType.Int).Value = _iAuthor_ID;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = _dDateIns;

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
                using (cmd = new SqlCommand("EditInvoice_Title", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@DateIssued", SqlDbType.DateTime).Value = _dDateIssued;
                    cmd.Parameters.Add("@Tipos", SqlDbType.Int).Value = _iTipos;
                    cmd.Parameters.Add("@Seira", SqlDbType.NVarChar, 10).Value = _sSeira;
                    cmd.Parameters.Add("@Arithmos", SqlDbType.Int).Value = _iArithmos;
                    cmd.Parameters.Add("@Selida", SqlDbType.NVarChar, 10).Value = _sSelida;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@TroposApostolis", SqlDbType.Int).Value = _iTroposApostolis;
                    cmd.Parameters.Add("@TroposPliromis", SqlDbType.Int).Value = _iTroposPliromis;
                    cmd.Parameters.Add("@Posotita", SqlDbType.Float).Value = _sgPosotita;
                    cmd.Parameters.Add("@AxiaMikti", SqlDbType.Float).Value = _sgAxiaMikti;
                    cmd.Parameters.Add("@Ekptosi", SqlDbType.Float).Value = _sgEkptosi;
                    cmd.Parameters.Add("@AxiaKathari", SqlDbType.Float).Value = _sgAxiaKathari;
                    cmd.Parameters.Add("@AxiaFPA", SqlDbType.Float).Value = _sgAxiaFPA;
                    cmd.Parameters.Add("@AxiaTeliki", SqlDbType.Float).Value = _sgAxiaTeliki;
                    cmd.Parameters.Add("@FileName", SqlDbType.NVarChar, 50).Value = _sFileName;
                    cmd.Parameters.Add("@SourceType", SqlDbType.Int).Value = _iSourceType;
                    cmd.Parameters.Add("@Source_ID", SqlDbType.Int).Value = _iSource_ID;
                    cmd.Parameters.Add("@Contract_ID", SqlDbType.Int).Value = _iContract_ID;
                    cmd.Parameters.Add("@OfficialInformingDate", SqlDbType.NVarChar, 20).Value = _sOfficialInformingDate;
                    cmd.Parameters.Add("@Author_ID", SqlDbType.Int).Value = _iAuthor_ID;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = _dDateIns;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "Invoice_Titles";
                    cmd.Parameters.Add("@Col", SqlDbType.NVarChar, 100).Value = "ID";
                    cmd.Parameters.Add("@Value", SqlDbType.NVarChar, 100).Value = _iRecord_ID;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public DateTime DateIssued { get { return this._dDateIssued; } set { this._dDateIssued = value; } }
        public int Tipos { get { return this._iTipos; } set { this._iTipos = value; } }
        public string Seira { get { return this._sSeira; } set { this._sSeira = value; } }
        public int Arithmos { get { return this._iArithmos; } set { this._iArithmos = value; } }
        public string Selida { get { return this._sSelida; } set { this._sSelida = value; } }
        public int Client_ID { get { return this._iClient_ID; } set { this._iClient_ID = value; } }
        public int TroposApostolis { get { return this._iTroposApostolis; } set { this._iTroposApostolis = value; } }
        public int TroposPliromis { get { return this._iTroposPliromis; } set { this._iTroposPliromis = value; } }
        public float Posotita { get { return this._sgPosotita; } set { this._sgPosotita = value; } }
        public float AxiaMikti { get { return this._sgAxiaMikti; } set { this._sgAxiaMikti = value; } }
        public float Ekptosi { get { return this._sgEkptosi; } set { this._sgEkptosi = value; } }
        public float AxiaKathari { get { return this._sgAxiaKathari; } set { this._sgAxiaKathari = value; } }
        public float AxiaFPA { get { return this._sgAxiaFPA; } set { this._sgAxiaFPA = value; } }
        public float AxiaTeliki { get { return this._sgAxiaTeliki; } set { this._sgAxiaTeliki = value; } }
        public string FileName { get { return this._sFileName; } set { this._sFileName = value; } }
        public int SourceType { get { return this._iSourceType; } set { this._iSourceType = value; } }
        public int Source_ID { get { return this._iSource_ID; } set { this._iSource_ID = value; } }
        public int Contract_ID { get { return this._iContract_ID; } set { this._iContract_ID = value; } }
        public string OfficialInformingDate { get { return this._sOfficialInformingDate; } set { this._sOfficialInformingDate = value; } }
        public int Author_ID { get { return this._iAuthor_ID; } set { this._iAuthor_ID = value; } }
        public DateTime DateIns { get { return this._dDateIns; } set { this._dDateIns = value; } }
        public DateTime DateFrom { get { return this._dFrom; } set { this._dFrom = value; } }
        public DateTime DateTo { get { return this._dTo; } set { this._dTo = value; } }
        public DateTime DateIssuedFrom { get { return this._dIssuedFrom; } set { this._dIssuedFrom = value; } }
        public DateTime DateIssuedTo { get { return this._dIssuedTo; } set { this._dIssuedTo = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}