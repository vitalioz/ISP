using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Newtonsoft.Json;
namespace Core
{
    public class clsClientsRequests
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        SqlDataReader drList = null;
        DataColumn dtCol;
        DataRow dtRow;

        private int      _iRecord_ID;
        private int      _iClient_ID;
        private string   _sGroup_ID;
        private int      _iTipos;
        private int      _iAction;
        private int      _iSource_ID;                           //  1 - сам клиент, 2 - наш сотрудник
        private string   _sDescription;
        private string   _sWarning;
        private DateTime _dDateIns;
        private DateTime _dDateWarning;
        private DateTime _dDateClose;
        private int      _iUser_ID;
        private int      _iStatus;                               // 1 - заявка находится на этапе подготовки(draft)
                                                                 // 2 - заявка отправлена на рассмотрение ответственным лицам(Compliance, BackOffice)
                                                                 // 3 – сделана первая проверка заявки 
                                                                 // 4 - сделана вторая проверка заявки 
                                                                 // 5 – сделана первая проверка результатов видеочата 
                                                                 // 6 - сделана вторая проверка результатов видеочата
                                                                 // 7 - заявка проверена и отклонена, т.к.неправильно введена.Комментарий проверяющего хранится в поле Warning

        private int      _iVideoChatStatus;                      // 0 – видеочат не нужен
                                                                 // 1 - видеочат нужен, но он пока не проведен
                                                                 // 2 - видеочат проведен и сделана первая проверка
                                                                 // 3 - видеочат проведен, сделана первая проверка, а значит он успешно завершен
                                                                 // 4 - видеочат проведен, но завершен отменой запросов(аннулирован)

        private string   _sVideoChatFile;

        private string _sRequestType_Title;
        private string _sClientName;
        private string _sAuthor_EMail;
        private DateTime _dFrom;
        private DateTime _dTo;
        private string   _sEmail;
        private DataTable _dtList;

        public clsClientsRequests()
        {
            this._iRecord_ID = 0;
            this._iClient_ID = 0;
            this._sGroup_ID = "";
            this._iTipos = 0;
            this._iAction = 0;
            this._iSource_ID = 0;
            this._sDescription = "";
            this._sWarning = "";
            this._dDateIns = Convert.ToDateTime("1900/01/01");
            this._dDateWarning = Convert.ToDateTime("1900/01/01");
            this._dDateClose = Convert.ToDateTime("1900/01/01");
            this._iUser_ID = 0;
            this._iStatus = 0;
            this._iVideoChatStatus = 0;
            this._sVideoChatFile = "";
            this._sEmail = "";
            this._sRequestType_Title = "";
            this._sClientName = "";
            this._sAuthor_EMail = "";
        }
        public void GetRecord()
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand("GetClientsRequest_Data", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.Add(new SqlParameter("@ID", this._iRecord_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    this._iRecord_ID = Convert.ToInt32(drList["ID"]);
                    this._iClient_ID = Convert.ToInt32(drList["Client_ID"]);
                    this._sGroup_ID = drList["Group_ID"] + "";
                    this._sClientName = (drList["Surname"] + " " + drList["Firstname"]).Trim();
                    this._iTipos = Convert.ToInt32(drList["Tipos"]);
                    this._iAction = Convert.ToInt32(drList["Aktion"]);
                    this._iSource_ID = Convert.ToInt32(drList["Source_ID"]);
                    this._sDescription = drList["Description"] + "";
                    this._sWarning = drList["Warning"] + "";
                    this._dDateIns = Convert.ToDateTime(drList["DateIns"]);
                    this._dDateWarning = Convert.ToDateTime(drList["DateWarning"]);
                    this._dDateClose = Convert.ToDateTime(drList["DateClose"]);
                    this._iUser_ID = Convert.ToInt32(drList["User_ID"]);
                    this._iStatus = Convert.ToInt32(drList["Status"]);
                    this._iVideoChatStatus = Convert.ToInt32(drList["VideoChatStatus"]);
                    this._sVideoChatFile = drList["VideoChatFile"] + "";
                    this._sEmail = drList["Email"] + "";
                    if (this._iAction == 0) this._sRequestType_Title = drList["RequestType_Title_0"] + "";
                    if (this._iAction == 1) this._sRequestType_Title = drList["RequestType_Title_1"] + "";
                    if (this._iAction == 2) this._sRequestType_Title = drList["RequestType_Title_2"] + "";
                    this._sAuthor_EMail = drList["Author_EMail"] + "";
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList()
        {
            try
            {
                _dtList = new DataTable("Client_RequestsList");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ClientFullName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientsRequest_Type_0", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientsRequest_Type_1", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientsRequest_Type_2", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientsRequest_Details", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ServiceProvider_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("FinanceService_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Amount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Group_ID", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RequestTipos", System.Type.GetType("System.Int32"));                                       // 1 - Άνοιγμα Νέου Επενδυτικού Λογαριασμού,2- ...
                dtCol = _dtList.Columns.Add("Action", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ContractTipos", System.Type.GetType("System.Int32"));                                      // 1 - ΑΤΟΜΙΚΟΣ,  <> 1 - KOIΝΟΣ
                dtCol = _dtList.Columns.Add("Source_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ServiceProvider_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Service_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DateIns", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("DateWarning", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("DateClose", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("VideoChatStatus", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("VideoChatFile", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Description", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Warning", System.Type.GetType("System.String"));

                conn.Open();
                cmd = new SqlCommand("GetClientsRequests", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@DateFrom", _dFrom.ToString("yyyy/MM/dd")));
                cmd.Parameters.Add(new SqlParameter("@DateTo", _dTo.ToString("yyyy/MM/dd")));
                cmd.Parameters.Add(new SqlParameter("@User_ID", _iUser_ID));
                cmd.Parameters.Add(new SqlParameter("@Client_ID", _iClient_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["ClientFullName"] = (drList["Surname"] + " " + drList["Firstname"]).Trim();
                    dtRow["ClientsRequest_Type_0"] = drList["ClientsRequest_Type_0"];
                    dtRow["ClientsRequest_Type_1"] = drList["ClientsRequest_Type_1"];
                    dtRow["ClientsRequest_Type_2"] = drList["ClientsRequest_Type_2"];
                    dtRow["ClientsRequest_Details"] = "";
                    dtRow["ContractTitle"] = drList["ContractTitle"];
                    dtRow["ServiceProvider_Title"] = drList["ServiceProvider_Title"];
                    dtRow["FinanceService_Title"] = drList["FinanceService_Title"];
                    dtRow["Amount"] = drList["Amount"];
                    dtRow["Currency"] = drList["Currency"];
                    dtRow["Client_ID"] = drList["Client_ID"];
                    dtRow["Group_ID"] = drList["Group_ID"] + "";
                    dtRow["RequestTipos"] = drList["Tipos"];
                    dtRow["Action"] = drList["Aktion"];
                    dtRow["ContractTipos"] = drList["ContractTipos"];
                    dtRow["Source_ID"] = drList["Source_ID"];
                    dtRow["ServiceProvider_ID"] = drList["ServiceProvider_ID"];
                    dtRow["Service_ID"] = drList["Service_ID"];
                    dtRow["DateIns"] = drList["DateIns"];
                    dtRow["DateWarning"] = drList["DateWarning"];
                    dtRow["DateClose"] = drList["DateClose"];
                    dtRow["Status"] = drList["Status"];
                    dtRow["VideoChatStatus"] = drList["VideoChatStatus"];
                    dtRow["VideoChatFile"] = drList["VideoChatFile"] + "";
                    dtRow["Description"] = drList["Description"] + "";
                    dtRow["Warning"] = drList["Warning"] + "";

                    _dtList.Rows.Add(dtRow);
                }
                drList.Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { conn.Close(); }
        }
        public void GetList_Group()
        {
            try
            {
                _dtList = new DataTable("Client_RequestsList");
                dtCol = _dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ClientFullName", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientsRequest_Type_0", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientsRequest_Type_1", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientsRequest_Type_2", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ClientsRequest_Details", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("ServiceProvider_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("FinanceService_Title", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Amount", System.Type.GetType("System.Single"));
                dtCol = _dtList.Columns.Add("Currency", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Client_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Group_ID", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("RequestTipos", System.Type.GetType("System.Int32"));                                       // 1 - Άνοιγμα Νέου Επενδυτικού Λογαριασμού,2- ...
                dtCol = _dtList.Columns.Add("Action", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ContractTipos", System.Type.GetType("System.Int32"));                                      // 1 - ΑΤΟΜΙΚΟΣ,  <> 1 - KOIΝΟΣ
                dtCol = _dtList.Columns.Add("Source_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("ServiceProvider_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("Service_ID", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("DateIns", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("DateWarning", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("DateClose", System.Type.GetType("System.DateTime"));
                dtCol = _dtList.Columns.Add("VideoChatStatus", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("VideoChatFile", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Status", System.Type.GetType("System.Int32"));
                dtCol = _dtList.Columns.Add("FolderPath", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Description", System.Type.GetType("System.String"));
                dtCol = _dtList.Columns.Add("Warning", System.Type.GetType("System.String"));

                conn.Open();
                cmd = new SqlCommand("GetClientsRequests_Group", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Group_ID", _sGroup_ID));
                drList = cmd.ExecuteReader();
                while (drList.Read())
                {
                    dtRow = _dtList.NewRow();
                    dtRow["ID"] = drList["ID"];
                    dtRow["ClientFullName"] = (drList["Surname"] + " " + drList["Firstname"]).Trim();
                    dtRow["ClientsRequest_Type_0"] = drList["ClientsRequest_Type_0"];
                    dtRow["ClientsRequest_Type_1"] = drList["ClientsRequest_Type_1"];
                    dtRow["ClientsRequest_Type_2"] = drList["ClientsRequest_Type_2"];
                    dtRow["ClientsRequest_Details"] = "";
                    dtRow["ContractTitle"] = drList["ContractTitle"];
                    dtRow["ServiceProvider_Title"] = drList["ServiceProvider_Title"];
                    dtRow["FinanceService_Title"] = drList["FinanceService_Title"];
                    dtRow["Amount"] = drList["Amount"];
                    dtRow["Currency"] = drList["Currency"];
                    dtRow["Client_ID"] = drList["Client_ID"];
                    dtRow["Group_ID"] = drList["Group_ID"] + "";
                    dtRow["RequestTipos"] = drList["Tipos"];
                    dtRow["Action"] = drList["Aktion"];
                    dtRow["ContractTipos"] = drList["ContractTipos"];
                    dtRow["Source_ID"] = drList["Source_ID"];
                    dtRow["ServiceProvider_ID"] = drList["ServiceProvider_ID"];
                    dtRow["Service_ID"] = drList["Service_ID"];
                    dtRow["DateIns"] = drList["DateIns"];
                    dtRow["DateWarning"] = drList["DateWarning"];
                    dtRow["DateClose"] = drList["DateClose"];
                    dtRow["Status"] = drList["Status"];
                    dtRow["VideoChatStatus"] = drList["VideoChatStatus"];
                    dtRow["VideoChatFile"] = drList["VideoChatFile"] + "";
                    dtRow["Description"] = drList["Description"] + "";
                    dtRow["Warning"] = drList["Warning"] + "";

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
                using (cmd = new SqlCommand("InsertClientsRequests", conn))
                {
                    SqlParameter outParam = new SqlParameter("@ID", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Client_ID", SqlDbType.Int).Value = _iClient_ID;
                    cmd.Parameters.Add("@Group_ID", SqlDbType.NVarChar, 50).Value = _sGroup_ID;
                    cmd.Parameters.Add("@Tipos", SqlDbType.Int).Value = _iTipos;
                    cmd.Parameters.Add("@Aktion", SqlDbType.Int).Value = _iAction;
                    cmd.Parameters.Add("@Source_ID", SqlDbType.Int).Value = _iSource_ID;
                    cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 1000).Value = _sDescription;
                    cmd.Parameters.Add("@Warning", SqlDbType.NVarChar, 500).Value = _sWarning;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = _dDateIns;
                    cmd.Parameters.Add("@DateWarning", SqlDbType.DateTime).Value = _dDateWarning;
                    cmd.Parameters.Add("@DateClose", SqlDbType.DateTime).Value = _dDateClose;
                    cmd.Parameters.Add("@User_ID", SqlDbType.Int).Value = _iUser_ID;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.Parameters.Add("@VideoChatStatus", SqlDbType.Int).Value = _iVideoChatStatus;
                    cmd.Parameters.Add("@VideoChatFile", SqlDbType.NVarChar, 50).Value = _sVideoChatFile;
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
                using (cmd = new SqlCommand("EditClientsRequests", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = _iRecord_ID;
                    cmd.Parameters.Add("@Group_ID", SqlDbType.NVarChar, 50).Value = _sGroup_ID;
                    cmd.Parameters.Add("@Description", SqlDbType.NVarChar, 1000).Value = _sDescription;
                    cmd.Parameters.Add("@Warning", SqlDbType.NVarChar, 500).Value = _sWarning;
                    cmd.Parameters.Add("@DateIns", SqlDbType.DateTime).Value = _dDateIns;
                    cmd.Parameters.Add("@DateWarning", SqlDbType.DateTime).Value = _dDateWarning;
                    cmd.Parameters.Add("@DateClose", SqlDbType.DateTime).Value = _dDateClose;
                    cmd.Parameters.Add("@Status", SqlDbType.Int).Value = _iStatus;
                    cmd.Parameters.Add("@VideoChatStatus", SqlDbType.Int).Value = _iVideoChatStatus;
                    cmd.Parameters.Add("@VideoChatFile", SqlDbType.NVarChar, 50).Value = _sVideoChatFile;
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
                    cmd.Parameters.Add("@Table", SqlDbType.NVarChar, 100).Value = "ClientsRequests";
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
        public string Group_ID { get { return this._sGroup_ID; } set { this._sGroup_ID = value; } }
        public int Tipos { get { return this._iTipos; } set { this._iTipos = value; } }
        public int Action { get { return this._iAction; } set { this._iAction = value; } }
        public int Source_ID { get { return this._iSource_ID; } set { this._iSource_ID = value; } }
        public string Description { get { return this._sDescription; } set { this._sDescription = value; } }
        public string Warning { get { return this._sWarning; } set { this._sWarning = value; } }
        public DateTime DateIns { get { return this._dDateIns; } set { this._dDateIns = value; } }
        public DateTime DateWarning { get { return this._dDateWarning; } set { this._dDateWarning = value; } }
        public DateTime DateClose { get { return this._dDateClose; } set { this._dDateClose = value; } }
        public int User_ID { get { return this._iUser_ID; } set { this._iUser_ID = value; } }
        public int Status { get { return this._iStatus; } set { this._iStatus = value; } }
        public int VideoChatStatus { get { return this._iVideoChatStatus; } set { this._iVideoChatStatus = value; } }
        public string VideoChatFile { get { return this._sVideoChatFile; } set { this._sVideoChatFile = value; } }        
        public string RequestType_Title { get { return this._sRequestType_Title; } set { this._sRequestType_Title = value; } }
        public string ClientName { get { return this._sClientName; } set { this._sClientName = value; } }
        public string Author_EMail { get { return this._sAuthor_EMail; } set { this._sAuthor_EMail = value; } }
        public DateTime DateFrom { get { return this._dFrom; } set { this._dFrom = value; } }
        public DateTime DateTo { get { return this._dTo; } set { this._dTo = value; } }
        public string Email { get { return this._sEmail; } set { this._sEmail = value; } }
        public DataTable List { get { return this._dtList; } set { this._dtList = value; } }
    }
}