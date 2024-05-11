using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Core
{
    public class clsOptions
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;

        private int    _iRecord_ID;
        private string _sTitle;
        private string _sAddress;
        private string _sDOY;
        private string _sAFM;
        private string _sLEI;
        private string _sVersion;
        private string _sDocFilesPath_Win;
        private string _sSMTP;
        private string _sEMail_Sender;
        private string _sEMail_Username;
        private string _sEMail_Password;
        private string _sNonReplay_Sender;
        private string _sNonReplay_Username;
        private string _sNonReplay_Password;
        private string _sRequest_Sender;
        private string _sRequest_Username;
        private string _sRequest_Password;
        private string _sSupport_Sender;
        private string _sSupport_Username;
        private string _sSupport_Password;
        private string _sEMail_BO_Receiver;
        private string _sSMS_Username;
        private string _sSMS_Password;
        private string _sSMS_From;
        private string _sFTP_Username;
        private string _sFTP_Password;
        private string _sRS_Address;
        private string _sRS_Username;
        private string _sRS_Password;
        private string _sInvoicePrinter;
        private int    _iInvoiceCopies;
        private string _sInvoice_Template;
        private string _sInvoice_AnalysisTemplate;
        private string _sInvoice_MF_Template;
        private string _sInvoice_MF_AnalysisTemplate;
        private string _sInvoice_AF_Template;
        private string _sInvoice_CF_Template;
        private string _sInvoice_FX_Template;
        private string _sInvoice_PF_Template;
        private string _sExPostCostTemplate;
        private string _sFIX_DB_Server_Path;
        private int _iInvoiceFisiko;
        private int _iInvoiceNomiko;
        private int _iInvoicePistotikoFisiko;
        private int _iInvoicePistotikoNomiko;
        private int _iInvoiceAkyrotiko;
        private int _iCompany_ID;
        private int _iLastBulkCommand_ID;
        private int _iLastBulkCommandFX_ID;
        private int _iTaxDeclarations1Year;
        private int _iTaxDeclarationsLastYear;
        private int _iRequestsPeriod1;
        private int _iRequestsPeriod2;
        private int _iAllowInsertOldOrders;

        public clsOptions() : base()
        {
            this._iRecord_ID = 0;
            this._sTitle = "";
            this._sAddress = "";
            this._sDOY = "";
            this._sAFM = "";
            this._sLEI = "";
            this._sVersion = "";
            this._sDocFilesPath_Win = "";
            this._sSMTP = "";
            this._sEMail_Sender = "";
            this._sEMail_Username = "";
            this._sEMail_Password = "";
            this._sNonReplay_Sender = "";
            this._sNonReplay_Username = "";
            this._sNonReplay_Password = "";
            this._sRequest_Sender = "";
            this._sRequest_Username = "";
            this._sRequest_Password = "";
            this._sSupport_Sender = "";
            this._sSupport_Username = "";
            this._sSupport_Password = "";
            this._sEMail_BO_Receiver = "";
            this._sSMS_Username = "";
            this._sSMS_Password = "";
            this._sSMS_From = "";
            this._sFTP_Username = "";
            this._sFTP_Password = "";
            this._sRS_Address = "";
            this._sRS_Username = "";
            this._sRS_Password = "";
            this._sInvoicePrinter = "";
            this._iInvoiceCopies = 0;
            this._sInvoice_Template = "";
            this._sInvoice_AnalysisTemplate = "";
            this._sInvoice_MF_Template = "";
            this._sInvoice_MF_AnalysisTemplate = "";
            this._sInvoice_AF_Template = "";
            this._sInvoice_CF_Template = "";
            this._sInvoice_FX_Template = "";
            this._sInvoice_PF_Template = "";
            this._sExPostCostTemplate = "";
            this._sFIX_DB_Server_Path = "";
            this._iInvoiceFisiko = 0;
            this._iInvoiceNomiko = 0;
            this._iInvoicePistotikoFisiko = 0;
            this._iInvoicePistotikoNomiko = 0;
            this._iInvoiceAkyrotiko = 0;
            this._iCompany_ID = 0;
            this._iLastBulkCommand_ID = 0;
            this._iLastBulkCommandFX_ID = 0;
            this._iTaxDeclarations1Year = 0;
            this._iTaxDeclarationsLastYear = 0;
            this._iRequestsPeriod1 = 0;
            this._iRequestsPeriod2 = 0;
            this._iAllowInsertOldOrders = 0;
        }

        public void GetRecord()
        {
            try
            {
                conn = new SqlConnection(Global.connStr);
                conn.Open();
                cmd = new SqlCommand("GetOptions", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = 1;
                    this._sTitle = drList["Title"] + "";
                    this._sAddress = drList["Address"] + "";
                    this._sDOY = drList["DOY"] + "";
                    this._sAFM = drList["AFM"] + "";
                    this._sLEI = drList["LEI"] + "";
                    this._sVersion = "";
                    this._sDocFilesPath_Win = drList["DocFilesPath_Win"] + "";
                    this._sSMTP = drList["SMTP"] + "";
                    this._sEMail_Sender = drList["EMail_Sender"] + "";
                    this._sEMail_Username = drList["EMail_Username"] + "";
                    this._sEMail_Password = drList["EMail_Password"] + "";
                    this._sNonReplay_Sender = drList["NonReplay_Sender"] + "";
                    this._sNonReplay_Username = drList["NonReplay_Username"] + "";
                    this._sNonReplay_Password = drList["NonReplay_Password"] + "";
                    this._sRequest_Sender = drList["Request_Sender"] + "";
                    this._sRequest_Username = drList["Request_Username"] + "";
                    this._sRequest_Password = drList["Request_Password"] + "";
                    this._sSupport_Sender = drList["Support_Sender"] + "";
                    this._sSupport_Username = drList["Support_Username"] + "";
                    this._sSupport_Password = drList["Support_Password"] + "";
                    this._sEMail_BO_Receiver = drList["EMail_BO_Receiver"] + "";
                    this._sSMS_Username = drList["SMS_Username"] + "";
                    this._sSMS_Password = drList["SMS_Password"] + "";
                    this._sSMS_From = drList["SMS_From"] + "";
                    this._sFTP_Username = drList["FTP_Username"] + "";
                    this._sFTP_Password = drList["FTP_Password"] + "";
                    this._sRS_Address = drList["RS_Address"] + "";
                    this._sRS_Username = drList["RS_Username"] + "";
                    this._sRS_Password = drList["RS_Password"] + "";
                    this._sInvoicePrinter = drList["InvoicePrinter"] + "";
                    this._iInvoiceCopies = Convert.ToInt32(drList["InvoiceCopies"]);
                    this._sInvoice_Template = drList["Invoice_Template"] + "";
                    this._sInvoice_AnalysisTemplate = drList["Invoice_AnalysisTemplate"] + "";
                    this._sInvoice_MF_Template = drList["Invoice_MF_Template"] + "";
                    this._sInvoice_MF_AnalysisTemplate = drList["Invoice_MF_AnalysisTemplate"] + "";
                    this._sInvoice_AF_Template = drList["Invoice_AF_Template"] + "";
                    this._sInvoice_CF_Template = drList["Invoice_CF_Template"] + "";
                    this._sInvoice_FX_Template = drList["Invoice_FX_Template"] + "";
                    this._sInvoice_PF_Template = drList["Invoice_PF_Template"] + "";
                    this._sExPostCostTemplate = drList["ExPostCostTemplate"] + "";
                    this._sFIX_DB_Server_Path = drList["FIX_DB_Server_Path"] + "";
                    this._iInvoiceFisiko = Convert.ToInt32(drList["InvoiceFisiko"]);
                    this._iInvoiceNomiko = Convert.ToInt32(drList["InvoiceNomiko"]);
                    this._iInvoicePistotikoFisiko = Convert.ToInt32(drList["InvoicePistotikoFisiko"]);
                    this._iInvoicePistotikoNomiko = Convert.ToInt32(drList["InvoicePistotikoNomiko"]);
                    this._iInvoiceAkyrotiko = Convert.ToInt32(drList["InvoiceAkyrotiko"]);
                    this._iCompany_ID = Convert.ToInt32(drList["Company_ID"]);
                    this._iLastBulkCommand_ID = Convert.ToInt32(drList["LastBulkCommand_ID"]);
                    this._iLastBulkCommandFX_ID = Convert.ToInt32(drList["LastBulkCommandFX_ID"]);
                    this._iTaxDeclarations1Year = Convert.ToInt32(drList["TaxDeclarations1Year"]);
                    this._iTaxDeclarationsLastYear = Convert.ToInt32(drList["TaxDeclarationsLastYear"]);
                    this._iAllowInsertOldOrders = Convert.ToInt32(drList["AllowInsertOldOrders"]);
                }

                drList.Close();
            }
            catch (Exception ex)
            {
                string sTemp = ex.Message;
            }
            finally { conn.Close(); }
        }
        public int InsertRecord()
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("InsertOptions", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Tipos", SqlDbType.Int).Value = _iRecord_ID;
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
                using (SqlCommand cmd = new SqlCommand("EditOptions", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar, 50).Value = _sTitle;
                    cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 100).Value = _sAddress;
                    cmd.Parameters.Add("@DOY", SqlDbType.NVarChar, 50).Value = _sDOY;
                    cmd.Parameters.Add("@AFM", SqlDbType.NVarChar, 20).Value = _sAFM;
                    cmd.Parameters.Add("@LEI", SqlDbType.NVarChar, 50).Value = _sLEI;
                    cmd.Parameters.Add("@Version", SqlDbType.NVarChar, 50).Value = _sVersion;
                    cmd.Parameters.Add("@DocFilesPath_Win", SqlDbType.NVarChar, 200).Value = _sDocFilesPath_Win;
                    cmd.Parameters.Add("@SMTP", SqlDbType.NVarChar, 100).Value = _sSMTP;
                    cmd.Parameters.Add("@EMail_Sender", SqlDbType.NVarChar, 50).Value = _sEMail_Sender;
                    cmd.Parameters.Add("@EMail_Username", SqlDbType.NVarChar, 50).Value = _sEMail_Username;
                    cmd.Parameters.Add("@EMail_Password", SqlDbType.NVarChar, 50).Value = _sEMail_Password;
                    cmd.Parameters.Add("@NonReplay_Sender", SqlDbType.NVarChar, 50).Value = _sNonReplay_Sender;
                    cmd.Parameters.Add("@NonReplay_Username", SqlDbType.NVarChar, 50).Value = _sNonReplay_Username;
                    cmd.Parameters.Add("@NonReplay_Password", SqlDbType.NVarChar, 50).Value = _sNonReplay_Password;
                    cmd.Parameters.Add("@Request_Sender", SqlDbType.NVarChar, 50).Value = _sRequest_Sender;
                    cmd.Parameters.Add("@Request_Username", SqlDbType.NVarChar, 50).Value = _sRequest_Username;
                    cmd.Parameters.Add("@Request_Password", SqlDbType.NVarChar, 50).Value = _sRequest_Password;
                    cmd.Parameters.Add("@Support_Sender", SqlDbType.NVarChar, 50).Value = _sSupport_Sender;
                    cmd.Parameters.Add("@Support_Username", SqlDbType.NVarChar, 50).Value = _sSupport_Username;
                    cmd.Parameters.Add("@Support_Password", SqlDbType.NVarChar, 50).Value = _sSupport_Password;
                    cmd.Parameters.Add("@EMail_BO_Receiver", SqlDbType.NVarChar, 50).Value = _sEMail_BO_Receiver;
                    cmd.Parameters.Add("@SMS_Username", SqlDbType.NVarChar, 50).Value = _sSMS_Username;
                    cmd.Parameters.Add("@SMS_Password", SqlDbType.NVarChar, 50).Value = _sSMS_Password;
                    cmd.Parameters.Add("@SMS_From", SqlDbType.NVarChar, 50).Value = _sSMS_From;
                    cmd.Parameters.Add("@FTP_Username", SqlDbType.NVarChar, 50).Value = _sFTP_Username;
                    cmd.Parameters.Add("@FTP_Password", SqlDbType.NVarChar, 50).Value = _sFTP_Password;
                    cmd.Parameters.Add("@RS_Address", SqlDbType.NVarChar, 50).Value = _sRS_Address;
                    cmd.Parameters.Add("@RS_Username", SqlDbType.NVarChar, 50).Value = _sRS_Username;
                    cmd.Parameters.Add("@RS_Password", SqlDbType.NVarChar, 50).Value = _sRS_Password;
                    cmd.Parameters.Add("@InvoicePrinter", SqlDbType.NVarChar, 100).Value = _sInvoicePrinter;
                    cmd.Parameters.Add("@InvoiceCopies", SqlDbType.Int).Value = _iInvoiceCopies;
                    cmd.Parameters.Add("@Invoice_Template", SqlDbType.NVarChar, 100).Value = _sInvoice_Template;
                    cmd.Parameters.Add("@Invoice_AnalysisTemplate", SqlDbType.NVarChar, 100).Value = _sInvoice_AnalysisTemplate;
                    cmd.Parameters.Add("@Invoice_MF_Template", SqlDbType.NVarChar, 100).Value = _sInvoice_MF_Template;
                    cmd.Parameters.Add("@Invoice_MF_AnalysisTemplate", SqlDbType.NVarChar, 100).Value = _sInvoice_MF_AnalysisTemplate;
                    cmd.Parameters.Add("@Invoice_AF_Template", SqlDbType.NVarChar, 100).Value = _sInvoice_AF_Template;
                    cmd.Parameters.Add("@Invoice_CF_Template", SqlDbType.NVarChar, 100).Value = _sInvoice_CF_Template;
                    cmd.Parameters.Add("@Invoice_FX_Template", SqlDbType.NVarChar, 100).Value = _sInvoice_FX_Template;
                    cmd.Parameters.Add("@Invoice_PF_Template", SqlDbType.NVarChar, 100).Value = _sInvoice_PF_Template;
                    cmd.Parameters.Add("@ExPostCostTemplate", SqlDbType.NVarChar, 100).Value = _sExPostCostTemplate;
                    cmd.Parameters.Add("@FIX_DB_Server_Path", SqlDbType.NVarChar, 50).Value = _sFIX_DB_Server_Path;
                    cmd.Parameters.Add("@InvoiceFisiko", SqlDbType.Int).Value = _iInvoiceFisiko;
                    cmd.Parameters.Add("@InvoiceNomiko", SqlDbType.Int).Value = _iInvoiceNomiko;
                    cmd.Parameters.Add("@InvoicePistotikoFisiko", SqlDbType.Int).Value = _iInvoicePistotikoFisiko;
                    cmd.Parameters.Add("@InvoicePistotikoNomiko", SqlDbType.Int).Value = _iInvoicePistotikoNomiko;
                    cmd.Parameters.Add("@InvoiceAkyrotiko", SqlDbType.Int).Value = _iInvoiceAkyrotiko;
                    cmd.Parameters.Add("@Company_ID", SqlDbType.Int).Value = _iCompany_ID;
                    cmd.Parameters.Add("@LastBulkCommand_ID", SqlDbType.Int).Value = _iLastBulkCommand_ID;
                    cmd.Parameters.Add("@LastBulkCommandFX_ID", SqlDbType.Int).Value = _iLastBulkCommandFX_ID;
                    cmd.Parameters.Add("@TaxDeclarations1Year", SqlDbType.Int).Value = _iTaxDeclarations1Year;
                    cmd.Parameters.Add("@TaxDeclarationsLastYear", SqlDbType.Int).Value = _iTaxDeclarationsLastYear;
                    cmd.Parameters.Add("@RequestsPeriod1", SqlDbType.Int).Value = _iRequestsPeriod1;
                    cmd.Parameters.Add("@RequestsPeriod2", SqlDbType.Int).Value = _iRequestsPeriod2;
                    cmd.Parameters.Add("@AllowInsertOldOrders", SqlDbType.Int).Value = _iAllowInsertOldOrders;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }

        public int Record_ID { get { return this._iRecord_ID; } set { this._iRecord_ID = value; } }
        public string Title { get { return this._sTitle; } set { this._sTitle = value; } }
        public string Address { get { return this._sAddress; } set { this._sAddress = value; } }
        public string DOY { get { return this._sDOY; } set { this._sDOY = value; } }
        public string AFM { get { return this._sAFM; } set { this._sAFM = value; } }
        public string LEI { get { return this._sLEI; } set { this._sLEI = value; } }
        public string Version { get { return this._sVersion; } set { this._sVersion = value; } }
        public string DocFilesPath_Win { get { return this._sDocFilesPath_Win; } set { this._sDocFilesPath_Win = value; } }
        public string SMTP { get { return this._sSMTP; } set { this._sSMTP = value; } }
        public string EMail_Sender { get { return this._sEMail_Sender; } set { this._sEMail_Sender = value; } }
        public string EMail_Username { get { return this._sEMail_Username; } set { this._sEMail_Username = value; } }
        public string EMail_Password { get { return this._sEMail_Password; } set { this._sEMail_Password = value; } }
        public string NonReplay_Sender { get { return this._sNonReplay_Sender; } set { this._sNonReplay_Sender = value; } }
        public string NonReplay_Username { get { return this._sNonReplay_Username; } set { this._sNonReplay_Username = value; } }
        public string NonReplay_Password { get { return this._sNonReplay_Password; } set { this._sNonReplay_Password = value; } }
        public string Request_Sender { get { return this._sRequest_Sender; } set { this._sRequest_Sender = value; } }
        public string Request_Username { get { return this._sRequest_Username; } set { this._sRequest_Username = value; } }
        public string Request_Password { get { return this._sRequest_Password; } set { this._sRequest_Password = value; } }
        public string Support_Sender { get { return this._sSupport_Sender; } set { this._sSupport_Sender = value; } }
        public string Support_Username { get { return this._sSupport_Username; } set { this._sSupport_Username = value; } }
        public string Support_Password { get { return this._sSupport_Password; } set { this._sSupport_Password = value; } }
        public string EMail_BO_Receiver { get { return this._sEMail_BO_Receiver; } set { this._sEMail_BO_Receiver = value; } }        
        public string SMS_Username { get { return this._sSMS_Username; } set { this._sSMS_Username = value; } }
        public string SMS_Password { get { return this._sSMS_Password; } set { this._sSMS_Password = value; } }
        public string SMS_From { get { return this._sSMS_From; } set { this._sSMS_From = value; } }
        public string FTP_Username { get { return this._sFTP_Username; } set { this._sFTP_Username = value; } }
        public string FTP_Password { get { return this._sFTP_Password; } set { this._sFTP_Password = value; } }
        public string RS_Address { get { return this._sRS_Address; } set { this._sRS_Address = value; } }
        public string RS_Username { get { return this._sRS_Username; } set { this._sRS_Username = value; } }
        public string RS_Password { get { return this._sRS_Password; } set { this._sRS_Password = value; } }
        public string InvoicePrinter { get { return this._sInvoicePrinter; } set { this._sInvoicePrinter = value; } }
        public int InvoiceCopies { get { return this._iInvoiceCopies; } set { this._iInvoiceCopies = value; } }
        public string InvoiceTemplate { get { return this._sInvoice_Template; } set { this._sInvoice_Template = value; } }
        public string InvoiceAnalysisTemplate { get { return this._sInvoice_AnalysisTemplate; } set { this._sInvoice_AnalysisTemplate = value; } }
        public string InvoiceMFTemplate { get { return this._sInvoice_MF_Template; } set { this._sInvoice_MF_Template = value; } }
        public string InvoiceMFAnalysisTemplate { get { return this._sInvoice_MF_AnalysisTemplate; } set { this._sInvoice_MF_AnalysisTemplate = value; } }
        public string InvoiceAFTemplate { get { return this._sInvoice_AF_Template; } set { this._sInvoice_AF_Template = value; } }
        public string InvoiceCFTemplate { get { return this._sInvoice_CF_Template; } set { this._sInvoice_CF_Template = value; } }
        public string InvoiceFXTemplate { get { return this._sInvoice_FX_Template; } set { this._sInvoice_FX_Template = value; } }
        public string InvoicePFTemplate { get { return this._sInvoice_PF_Template; } set { this._sInvoice_PF_Template = value; } }
        public string ExPostCostTemplate { get { return this._sExPostCostTemplate; } set { this._sExPostCostTemplate = value; } }
        public int InvoiceFisiko { get { return this._iInvoiceFisiko; } set { this._iInvoiceFisiko = value; } }
        public int InvoiceNomiko { get { return this._iInvoiceNomiko; } set { this._iInvoiceNomiko = value; } }
        public int InvoicePistotikoFisiko {get { return _iInvoicePistotikoFisiko; } set {_iInvoicePistotikoFisiko = value; }}
        public int InvoicePistotikoNomiko { get { return _iInvoicePistotikoNomiko; }  set {_iInvoicePistotikoNomiko = value; }}
        public int InvoiceAkyrotiko { get {return _iInvoiceAkyrotiko;} set {_iInvoiceAkyrotiko = value; }}
        public int Company_ID { get {return _iCompany_ID;}  set {_iCompany_ID = value; } }
        public int LastBulkCommand_ID { get { return _iLastBulkCommand_ID; }  set { _iLastBulkCommand_ID = value; } }
        public int LastBulkCommandFX_ID { get { return _iLastBulkCommandFX_ID; } set { _iLastBulkCommandFX_ID = value; } }
        public int TaxDeclarations1Year { get { return _iTaxDeclarations1Year; } set { _iTaxDeclarations1Year = value; } }
        public int TaxDeclarationsLastYear { get { return _iTaxDeclarationsLastYear; } set { _iTaxDeclarationsLastYear = value; } }
        public int RequestsPeriod1 { get { return _iRequestsPeriod1; } set { _iRequestsPeriod1 = value; } }
        public int RequestsPeriod2 { get { return _iRequestsPeriod2; } set { _iRequestsPeriod2 = value; } }
        public int AllowInsertOldOrders { get { return _iAllowInsertOldOrders; } set { _iAllowInsertOldOrders = value; } }
        public string FIX_DB_Server_Path { get { return _sFIX_DB_Server_Path; } set { _sFIX_DB_Server_Path = value; } }
    }
}
