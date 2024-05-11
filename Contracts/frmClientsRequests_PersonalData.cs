using Core;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Contracts
{
    public partial class frmClientsRequests_PersonalData : Form
    {
        int i, j, m, iRequest_ID, iAction, iRequestTipos, iClient_ID, iClientStatus, iStatus, iVideoChatStatus, jStatus, iADTStatus, iPassportStatus, iClients_Clients_ID,
            iCountry_ID, iCountryTaxes_ID, iBankAccount_ID, iRightsLevel, iTaxDeclarations1Year = 0, iTaxDeclarationsLastYear = 0, iCancelType;
        string sTemp = "", sPolice = "", sExpireDate = "", sDescription = "", sCity = "", sZip = "", sCountry = "", sCountryTaxes = "", sSpec = "",
               sPassport_Police = "", sPassport_ExpireDate = "", sW8BEN = "", sSpecCateg = "", sAuthor_EMail = "", sGroup_ID = "", sNewFileName;
        string[] sStatus = { "Διαγράφθηκε", "Πρόχειρο", "Προς έλεγχο", "Έλεγχος 1", "Έλεγχος 2", "Ελέγχθηκε", "Οριστικοποήθηκε", "Απορρίφθηκε" };
        string[] tokens, bokens;

        DateTime dDateIns;
        DataRow[] foundRows;
        clsClients Clients = new clsClients();
        clsClientsRequests ClientsRequests = new clsClientsRequests();
        clsClientsRequests_Types ClientsRequests_Types = new clsClientsRequests_Types();
        clsClients_BankAccounts Clients_BankAccounts = new clsClients_BankAccounts();
        clsClientsDocFiles ClientDocFiles = new clsClientsDocFiles();
        clsClientsDocFiles allClientDocFiles = new clsClientsDocFiles();
        clsClients_Clients Clients_Clients = new clsClients_Clients();
        clsServerJobs ServerJobs = new clsServerJobs();
        clsWebUsers WebUsers = new clsWebUsers();
        clsWebUsersStates WebUsersStates = new clsWebUsersStates();
        clsCashTables CashTables = new clsCashTables();
        #region --- Start functions -----------------------------------------------------------------------------
        public frmClientsRequests_PersonalData()
        {
            InitializeComponent();
        }

        private void frmClientsRequests_PersonalData_Load(object sender, EventArgs e)
        {
            CloseAllEditWindows();

            panL1.Left = 12;
            panL1.Top = 42;

            panL2.Left = 12;
            panL2.Top = 42;

            btnCancel.Top = 78;
            btnDelete.Top = 78;
            btnConfirm.Top = 78;
            btnOK.Top = 78;

            panWarning.Left = 108;
            panWarning.Top = 150;

            grpWarning.Top = 536;
            grpVideoChat.Top = 610;
            grpFooter1.Top = 705;
            grpFooter2.Top = 705;

            ucADT.Left = 6;
            ucADT.Top = 128;
            ucADT.Width = 500;
            ucADT.Height = 550;

            ucMobile.Left = 6;
            ucMobile.Top = 128;
            ucMobile.Width = 500;
            ucMobile.Height = 550;

            ucTel.Left = 6;
            ucTel.Top = 128;
            ucTel.Width = 500;
            ucTel.Height = 550;

            ucEmail.Left = 6;
            ucEmail.Top = 128;
            ucEmail.Width = 500;
            ucEmail.Height = 284;

            ucAddress.Left = 6;
            ucAddress.Top = 128;
            ucAddress.Width = 500;
            ucAddress.Height = 550;

            ucAFM.Left = 6;
            ucAFM.Top = 128;
            ucAFM.Width = 500;
            ucAFM.Height = 550;

            ucEkkatharistika.Left = 6;
            ucEkkatharistika.Top = 128;
            ucEkkatharistika.Width = 500;
            ucEkkatharistika.Height = 550;

            ucCountryTaxes.Left = 6;
            ucCountryTaxes.Top = 128;
            ucCountryTaxes.Width = 500;
            ucCountryTaxes.Height = 550;

            ucSpecial.Left = 6;
            ucSpecial.Top = 128;
            ucSpecial.Width = 500;
            ucSpecial.Height = 550;

            ucPasport.Left = 12;
            ucPasport.Top = 128;
            ucPasport.Width = 500;
            ucPasport.Height = 550;

            ucW8BEN.Left = 6;
            ucW8BEN.Top = 128;
            ucW8BEN.Width = 500;
            ucW8BEN.Height = 550;

            ucSpecCateg.Left = 12;
            ucSpecCateg.Top = 128;
            ucSpecCateg.Width = 500;
            ucSpecCateg.Height = 550;

            ucMerida.Left = 6;
            ucMerida.Top = 128;
            ucMerida.Width = 500;
            ucMerida.Height = 550;

            ucLogAxion.Left = 12;
            ucLogAxion.Top = 128;
            ucLogAxion.Width = 500;
            ucLogAxion.Height = 550;

            ucAMKA.Left = 6;
            ucAMKA.Top = 128;
            ucAMKA.Width = 500;
            ucAMKA.Height = 550;

            ucBankAccount.Left = 6;
            ucBankAccount.Top = 128;
            ucBankAccount.Width = 500;
            ucBankAccount.Height = 550;

            ucBankAccount_Delete.Left = 6;
            ucBankAccount_Delete.Top = 128;
            ucBankAccount_Delete.Width = 500;
            ucBankAccount_Delete.Height = 550;

            ucCoOwner.Left = 6;
            ucCoOwner.Top = 128;
            ucCoOwner.Width = 500;
            ucCoOwner.Height = 550;

            ucCoOwner_Delete.Left = 6;
            ucCoOwner_Delete.Top = 128;
            ucCoOwner_Delete.Width = 500;
            ucCoOwner_Delete.Height = 550;

            ucCowner_Child.Left = 6;
            ucCowner_Child.Top = 128;
            ucCowner_Child.Width = 500;
            ucCowner_Child.Height = 550;

            ucCowner_Delete.Left = 6;
            ucCowner_Delete.Top = 128;
            ucCowner_Delete.Width = 500;
            ucCowner_Delete.Height = 550;

            ucCS.StartInit(400, 240, 396, 20, 1);
            ucCS.Top = 8;
            ucCS.Left = 88;

            rbAdult.Left = lblData1.Left;
            rbChild.Left = lblData2.Left;

            //----- check if Clients list was changed @@@@@@@@@@@@@-------------------------------
            sTemp = "";
            foundRows = Global.dtCashTables.Select("ID=40");
            if (foundRows.Length > 0) sTemp = foundRows[0]["LastEdit_Time"] + "";

            CashTables = new clsCashTables();
            CashTables.Record_ID = 40;                          // 40 - Clients Table
            CashTables.GetRecord();
            if (CashTables.LastEdit_Time > Convert.ToDateTime(sTemp)) Global.GetClientsList();
            // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ 

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString(Global.GridStyle);
            fgList.RowColChange += new EventHandler(fgList_RowColChange);

            //------- fgBankAccounts ----------------------------
            fgBankAccounts.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgBankAccounts.Styles.ParseString(Global.GridStyle);

            //------- fgCoOwners ----------------------------
            fgCoOwners.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgCoOwners.Styles.ParseString(Global.GridStyle);

            if (sGroup_ID == "")                                        // Standalone Request Mode
            {
                this.Text = "Προσωπικά Στοιχεία";
                panL2.Visible = false;

                if (iRequest_ID == 0)                                   // New request
                {
                    this.Width = 912;
                    this.Height = 1016;

                    ucCS.TextOfLabelChanged += new EventHandler(ucCS_TextChanged);
                    ucCS.Filters = "Status <> 0 AND Tipos < 3";             // Status = 0 - Cancelled, Status = 1 - Αctive       Tipos = 1 - idiotis, 2 - company, 3- join
                    ucCS.ListType = 1;
                    ucCS.Visible = true;
                    lblClientName.Visible = false;

                    panL1.Visible = false;

                    grpL2.Left = 370;
                    grpL2.Top = 60;
                    grpL2.Visible = false;

                    grpL3.Left = 370;
                    grpL3.Top = 60;
                    grpL3.Width = 516;
                    grpL3.Height = 800;
                    grpL3.Visible = false;

                    iClient_ID = 0;
                    iRequestTipos = 0;
                    sDescription = "";
                    txtWarning.Text = "";
                    dDateIns = DateTime.Now;
                    iStatus = 0;
                    iVideoChatStatus = 0;
                    lblVideoChatFile.Text = "";
                    lblVideoChatFile_ID.Text = "0";
                    iAction = 0;
                    lblEmail.Text = "";
                }
                else                                                               // View or Edit request Mode
                {
                    this.Width = 548;
                    this.Height = 890;

                    grpL3.Left = 8;
                    grpL3.Top = 42;
                    grpL3.Width = 516;
                    grpL3.Height = 800;

                    picClose_Close.Visible = false;

                    ClientsRequests = new clsClientsRequests();
                    ClientsRequests.Record_ID = iRequest_ID;
                    ClientsRequests.GetRecord();
                    iClient_ID = ClientsRequests.Client_ID;
                    iRequestTipos = ClientsRequests.Tipos;
                    iAction = ClientsRequests.Action;
                    sDescription = ClientsRequests.Description;
                    txtWarning.Text = ClientsRequests.Warning;
                    dDateIns = ClientsRequests.DateIns;
                    iStatus = ClientsRequests.Status;
                    iVideoChatStatus = ClientsRequests.VideoChatStatus;
                    lblVideoChatFile.Text = ClientsRequests.VideoChatFile;
                    lblEmail.Text = ClientsRequests.Email;
                    sAuthor_EMail = ClientsRequests.Author_EMail;

                    lblClientName.Visible = true;
                    ucCS.Visible = false;

                    if (iClient_ID != 0)
                    {
                        Clients = new clsClients();
                        Clients.Record_ID = iClient_ID;
                        Clients.EMail = "";
                        Clients.Mobile = "";
                        Clients.AFM = "";
                        Clients.DoB = Convert.ToDateTime("1900/01/01");
                        Clients.GetRecord();
                        iClientStatus = Clients.Status;
                        ShowClientData();
                        lblClientName.Text = Clients.Fullname;
                    }

                    ShowRequestWindow();

                    panL1.Visible = false;
                    grpL2.Visible = false;
                    grpL3.Visible = true;
                }
            }
            else                                                            // 2 - GroupRequests Mode
            {
                this.Text = "Ομάδα αιτημάτων";
                this.Width = 912;
                this.Height = 880;

                ClientsRequests = new clsClientsRequests();
                ClientsRequests.Record_ID = iRequest_ID;
                ClientsRequests.GetRecord();
                iClient_ID = ClientsRequests.Client_ID;
                lblGroupVideoChatFile.Text = ClientsRequests.VideoChatFile;
                lblEmail.Text = ClientsRequests.Email;
                sAuthor_EMail = ClientsRequests.Author_EMail;
                iStatus = ClientsRequests.Status;
                iVideoChatStatus = ClientsRequests.VideoChatStatus;

                Clients = new clsClients();
                Clients.Record_ID = iClient_ID;
                Clients.EMail = "";
                Clients.Mobile = "";
                Clients.AFM = "";
                Clients.DoB = Convert.ToDateTime("1900/01/01");
                Clients.GetRecord();
                ShowClientData();
                lblClientName.Text = Clients.Fullname;
                lblClientName.Visible = true;
                ucCS.Visible = false;

                DefineRequestsList();
                if (iStatus >= 4 && iVideoChatStatus > 0) grpGroupVideoChat.Visible = true;

                panL1.Visible = false;
                panL2.Visible = true;
                grpL3.Left = 370;
                grpL3.Top = 60;
                grpL3.Width = 516;
                grpL3.Height = 775;
            }

            //--- define All ClientsDocFiles for client with ID = Client_ID --------
            if (iClient_ID != 0)
            {
                allClientDocFiles = new clsClientsDocFiles();
                allClientDocFiles.Client_ID = iClient_ID;
                allClientDocFiles.PreContract_ID = 0;
                allClientDocFiles.Contract_ID = 0;
                allClientDocFiles.DocTypes = 0;
                allClientDocFiles.GetList();
            }

            this.CenterToScreen();
        }
        protected override void OnResize(EventArgs e)
        {
        }
        #endregion
        #region --- Requests actions ----------------------------------------------------------------------------
        private void btnCreateRequest_Click(object sender, EventArgs e)
        {
            ShowRequestWindow();
            grpL3.Visible = true;
        }
        private void picClose_Close_Click(object sender, EventArgs e)
        {
            CloseAllEditWindows();
        }
        private void btnDelete_Request_Click(object sender, EventArgs e)
        {
            SaveRequest(0);
            CloseAllEditWindows();
            this.Close();
        }
        private void btnSave_Temp_Click(object sender, EventArgs e)
        {
            SaveRequest(1);
            CloseAllEditWindows();
            this.Close();
        }
        private void CloseAllEditWindows()
        {
            ucADT.Visible = false;
            ucMobile.Visible = false;
            ucTel.Visible = false;
            ucEmail.Visible = false;
            ucAddress.Visible = false;
            ucAFM.Visible = false;
            ucCountryTaxes.Visible = false;
            ucEkkatharistika.Visible = false;
            ucSpecial.Visible = false;
            ucPasport.Visible = false;
            ucBankAccount.Visible = false;
            ucBankAccount_Delete.Visible = false;
            ucW8BEN.Visible = false;
            ucSpecCateg.Visible = false;
            ucMerida.Visible = false;
            ucLogAxion.Visible = false;
            ucAMKA.Visible = false;
            ucCoOwner.Visible = false;
            ucCoOwner_Delete.Visible = false;
            ucCowner_Child.Visible = false;
            ucCowner_Delete.Visible = false;
            grpL2.Visible = false;
            grpL3.Visible = false;
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            string sLocStatus = "";
            switch (iRequestTipos)
            {
                case 1:
                    sLocStatus = ucADT.lblStatus.Text;
                    break;
                case 2:
                    sLocStatus = ucMobile.lblStatus.Text;
                    break;
                case 3:
                    sLocStatus = ucTel.lblStatus.Text;
                    break;
                case 4:
                    sLocStatus = ucEmail.lblStatus.Text;
                    break;
                case 5:
                    sLocStatus = ucAddress.lblStatus.Text;
                    break;
                case 6:
                    sLocStatus = ucAFM.lblStatus.Text;
                    break;
                case 7:
                    sLocStatus = ucEkkatharistika.lblStatus.Text;
                    break;
                case 8:
                    sLocStatus = ucSpecial.lblStatus.Text;
                    break;
                case 9:
                    sLocStatus = ucCountryTaxes.lblStatus.Text;
                    break;
                case 10:
                    sLocStatus = ucSpecCateg.lblStatus.Text;
                    break;
                case 11:
                    if (iAction == 0) sLocStatus = ucBankAccount.lblStatus.Text;
                    if (iAction == 2) sLocStatus = ucBankAccount_Delete.lblStatus.Text;
                    break;
                case 12:
                    if (iAction == 0) sLocStatus = ucCoOwner.lblStatus.Text;
                    if (iAction == 2) sLocStatus = ucCoOwner_Delete.lblStatus.Text;
                    break;
                case 13:
                    sLocStatus = ucW8BEN.lblStatus.Text;
                    break;
                case 14:
                    sLocStatus = ucPasport.lblStatus.Text;
                    break;
                case 15:
                    sLocStatus = ucMerida.lblStatus.Text;
                    break;
                case 16:
                    sLocStatus = ucLogAxion.lblStatus.Text;
                    break;
                case 17:
                    sLocStatus = ucAMKA.lblStatus.Text;
                    break;
            }
            if (sLocStatus == "1")
            {
                SaveRequest(2);

                ServerJobs = new clsServerJobs();
                ServerJobs.JobType_ID = 46;
                ServerJobs.Source_ID = 0;
                ServerJobs.Parameters = "{'recipient_email': '" + lblEmail.Text + "', 'request_action' : '1', 'request_id': '" + iRequest_ID + "'}";
                ServerJobs.DateStart = DateTime.Now;
                ServerJobs.DateFinish = System.Convert.ToDateTime("1900/01/01");
                ServerJobs.PubKey = "";
                ServerJobs.PrvKey = "";
                ServerJobs.Attempt = 0;
                ServerJobs.Status = 0;
                ServerJobs.InsertRecord();

                ServerJobs = new clsServerJobs();
                ServerJobs.JobType_ID = 46;
                ServerJobs.Source_ID = 0;
                ServerJobs.Parameters = "{'recipient_email': '" + Global.Request_Sender + "', 'request_action' : '201', 'person_name': '" + lblClientName.Text + "'}";
                ServerJobs.DateStart = DateTime.Now;
                ServerJobs.DateFinish = System.Convert.ToDateTime("1900/01/01");
                ServerJobs.PubKey = "";
                ServerJobs.PrvKey = "";
                ServerJobs.Attempt = 0;
                ServerJobs.Status = 0;
                ServerJobs.InsertRecord();

                CloseAllEditWindows();
                this.Close();
            }
            else MessageBox.Show("Υποβολή αιτήματος δεν μπορεί να πραγματοποιηθεί επειδή υπάρχουν εκκρεμότητες", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (sGroup_ID == "") iCancelType = 1;        // 1 - Standalone Request, 2 - Standalone VideoMode, 3 - Group Requests, 4 - Group Requests VideoChat Mode
            else iCancelType = 3;        // 1 - Standalone Request, 2 - Standalone VideoMode, 3 - Group Requests, 4 - Group Requests VideoChat Mode

            chkDeleteUser.Text = iClientStatus == 1 ? "Block του πελάτη" : "Διαγραφή του πελάτη";
            if (txtWarning.Text.Length == 0) btnOK_Warning.Enabled = false;
            else btnOK_Warning.Enabled = true;
            panWarning.Visible = true;
            txtWarning.Focus();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteRequest();
            this.Close();
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            SaveRequest(3);
            DefineRequestsList();

            if (sGroup_ID == "") this.Close();                                                       // standalone request mode
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveRequest(4);
            DefineRequestsList();

            if (sGroup_ID == "")                                                                    // standalone request mode
            {
                if (iVideoChatStatus > 0)
                {
                    if (lblVideoChatFile.Text != "") picShowVideoChatFile.Visible = true;
                    else picShowVideoChatFile.Visible = false;

                    grpVideoChat.Visible = true;
                    MessageBox.Show("Σας υπενθυμίζουμε ότι θα χρειαστεί να γίνει Video Κλήση για να ταυτοποιηθεί ο χρήστης", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else SaveRequestDataIntoDB();

                this.Close();
            }
        }
        private void txtWarning_TextChanged(object sender, EventArgs e)
        {
            if (txtWarning.Text.Length == 0) btnOK_Warning.Enabled = false;
            else btnOK_Warning.Enabled = true;
        }
        private void btnOK_Warning_Click(object sender, EventArgs e)
        {
            if (iCancelType == 2 || iCancelType == 4)
            {                                                    // 1 - Standalone Request, 2 - Standalone VideoMode, 3 - Group Requests, 4 - Group Requests VideoChat Mode

                if (sGroup_ID == "")
                {
                    iVideoChatStatus = 4;
                    ClientsRequests = new clsClientsRequests();
                    ClientsRequests.Record_ID = iRequest_ID;
                    ClientsRequests.GetRecord();
                    ClientsRequests.Warning = txtWarning.Text;
                    ClientsRequests.VideoChatStatus = 4;                                    // 4 - видеочат проведен, но завершен отменой запросов (аннулирован)
                    ClientsRequests.EditRecord();
                }
                else
                {
                    for (i = 1; i <= fgList.Rows.Count - 1; i++)
                    {
                        ClientsRequests = new clsClientsRequests();
                        ClientsRequests.Record_ID = Convert.ToInt32(fgList[i, "ID"]);
                        ClientsRequests.GetRecord();
                        ClientsRequests.Warning = txtWarning.Text;
                        ClientsRequests.VideoChatStatus = 4;                                  // 4 - видеочат проведен, но завершен отменой запросов (аннулирован)
                        ClientsRequests.EditRecord();
                    }
                }

                ServerJobs = new clsServerJobs();
                ServerJobs.JobType_ID = 46;
                ServerJobs.Source_ID = 0;
                ServerJobs.Parameters = "{'recipient_email': '" + lblEmail.Text + "', 'request_action' : '25', 'request_id': '" + iRequest_ID + "', 'notes' : '" + txtWarning.Text + "'}";
                ServerJobs.DateStart = DateTime.Now;
                ServerJobs.DateFinish = System.Convert.ToDateTime("1900/01/01");
                ServerJobs.PubKey = "";
                ServerJobs.PrvKey = "";
                ServerJobs.Attempt = 0;
                ServerJobs.Status = 0;
                ServerJobs.InsertRecord();

                ServerJobs = new clsServerJobs();
                ServerJobs.JobType_ID = 46;
                ServerJobs.Source_ID = 0;
                ServerJobs.Parameters = "{'recipient_email': '" + Global.Request_Sender + "', 'request_action' : '205', 'person_name': '" + lblClientName.Text +
                                        "', 'afm': '" + lblAFM.Text + "', 'notes' : '" + txtWarning.Text + "'}";
                ServerJobs.DateStart = DateTime.Now;
                ServerJobs.DateFinish = System.Convert.ToDateTime("1900/01/01");
                ServerJobs.PubKey = "";
                ServerJobs.PrvKey = "";
                ServerJobs.Attempt = 0;
                ServerJobs.Status = 0;
                ServerJobs.InsertRecord();
            }
            else CancelSingleRequest(sGroup_ID, iRequest_ID, txtWarning.Text, lblVideoChatFile.Text);

            sGroup_ID = "";
            panWarning.Visible = false;
            this.Close();
        }
        private void btnCancel_Warning_Click(object sender, EventArgs e)
        {
            panWarning.Visible = false;
        }
        private void SaveRequest(int iStatus)
        {
            sDescription = "";
            switch (iRequestTipos)
            {
                case 1:
                    sDescription = ucADT.Description;
                    break;
                case 2:
                    sDescription = ucMobile.Description;
                    break;
                case 3:
                    sDescription = ucTel.Description;
                    break;
                case 4:
                    sDescription = ucEmail.Description;
                    break;
                case 5:
                    sDescription = ucAddress.Description;
                    break;
                case 6:
                    sDescription = ucAFM.Description;
                    break;
                case 7:
                    sDescription = ucEkkatharistika.Description;
                    break;
                case 8:
                    sDescription = ucSpecial.Description;
                    break;
                case 9:
                    sDescription = ucCountryTaxes.Description;
                    break;
                case 10:
                    sDescription = ucSpecCateg.Description;
                    break;
                case 11:
                    if (iAction == 0) sDescription = ucBankAccount.Description;
                    if (iAction == 2) sDescription = ucBankAccount_Delete.Description;
                    break;
                case 12:
                    if (rbAdult.Checked)
                    {
                        if (iAction == 0) sDescription = ucCoOwner.Description;
                        if (iAction == 2) sDescription = ucCoOwner_Delete.Description;
                    }
                    if (rbChild.Checked)
                    {
                        if (iAction == 0) sDescription = ucCowner_Child.Description;
                        if (iAction == 2) sDescription = ucCowner_Delete.Description;
                    }
                    break;
                case 13:
                    sDescription = ucW8BEN.Description;
                    break;
                case 14:
                    sDescription = ucPasport.Description;
                    break;
                case 15:
                    sDescription = ucMerida.Description;
                    break;
                case 16:
                    sDescription = ucLogAxion.Description;
                    break;
                case 17:
                    sDescription = ucAMKA.Description;
                    break;
            }

            ClientsRequests = new clsClientsRequests();
            if (iRequest_ID == 0)
            {
                ClientsRequests.Client_ID = iClient_ID;
                ClientsRequests.Group_ID = sGroup_ID;
                ClientsRequests.Tipos = iRequestTipos;
                ClientsRequests.Action = iAction;
                ClientsRequests.Source_ID = 2;                             //  1 - сам клиент, 2 - наш сотрудник
                ClientsRequests.Description = sDescription;
                ClientsRequests.Warning = "";
                ClientsRequests.DateIns = DateTime.Now;
                ClientsRequests.DateWarning = Convert.ToDateTime("1900/01/01");
                ClientsRequests.DateClose = Convert.ToDateTime("1900/01/01");
                ClientsRequests.User_ID = Global.User_ID;
                ClientsRequests.Status = iStatus;
                ClientsRequests.VideoChatStatus = 0;
                ClientsRequests.VideoChatFile = "";
                iRequest_ID = ClientsRequests.InsertRecord();
            }
            else
            {
                ClientsRequests.Record_ID = iRequest_ID;
                ClientsRequests.GetRecord();
                ClientsRequests.Group_ID = sGroup_ID;
                ClientsRequests.Description = sDescription;
                ClientsRequests.Warning = txtWarning.Text;
                if (iStatus == 4)
                    ClientsRequests.DateClose = DateTime.Now;
                ClientsRequests.Status = iStatus;
                ClientsRequests.VideoChatStatus = iVideoChatStatus;
                ClientsRequests.VideoChatFile = lblVideoChatFile.Text;
                ClientsRequests.EditRecord();
            }
        }
        private void SaveRequestDataIntoDB()
        {
            int i, m;
            string sFiles_ID = "";

            Clients = new clsClients();
            Clients.EMail = "";
            Clients.Mobile = "";
            Clients.AFM = "";
            Clients.DoB = Convert.ToDateTime("1900/01/01");
            Clients.Record_ID = iClient_ID;
            Clients.GetRecord();

            switch (iRequestTipos)
            {
                case 1:
                    Clients.ADT = ucADT.txtNewNumber.Text;
                    Clients.Police = ucADT.txtNewPolice.Text;
                    Clients.ExpireDate = ucADT.dNewExpireDate.Text;

                    for (i = 1; i <= ucADT.fgDocs.Rows.Count - 1; i++)
                        if ((ucADT.fgDocs[i, 0] + "").Trim().Length > 0 && (ucADT.fgDocs[i, 5] + "").Trim() == "1")
                            sFiles_ID = sFiles_ID + ucADT.fgDocs[i, "File_Name"] + "^" + ucADT.fgDocs[i, "ID"] + "~";
                    break;
                case 2:
                    Clients.Mobile = ucMobile.txtNewNumber.Text;
                    for (i = 1; i <= ucMobile.fgDocs.Rows.Count - 1; i++)
                        if ((ucMobile.fgDocs[i, 0] + "").Trim().Length > 0 && (ucMobile.fgDocs[i, 5] + "").Trim() == "1")
                            sFiles_ID = sFiles_ID + ucMobile.fgDocs[i, "File_Name"] + "^" + ucMobile.fgDocs[i, "ID"] + "~";
                    for (i = 1; i <= ucMobile.fgDocs2.Rows.Count - 1; i++)
                        if ((ucMobile.fgDocs2[i, 0] + "").Trim().Length > 0 && (ucMobile.fgDocs2[i, 5] + "").Trim() == "1")
                            sFiles_ID = sFiles_ID + ucMobile.fgDocs2[i, "File_Name"] + "^" + ucMobile.fgDocs2[i, "ID"] + "~";

                    if (ucMobile.lnkEmail.Text.Trim().Length > 0) sFiles_ID = sFiles_ID + ucMobile.lnkEmail.Text + "^" + ucMobile.lblEmail_ID.Text + "~";

                    break;
                case 3:
                    Clients.Tel = ucTel.txtNewNumber.Text;
                    for (i = 1; i <= ucTel.fgDocs.Rows.Count - 1; i++)
                        if ((ucTel.fgDocs[i, 0] + "").Trim().Length > 0 && (ucTel.fgDocs[i, 5] + "").Trim() == "1")
                            sFiles_ID = sFiles_ID + ucTel.fgDocs[i, "File_Name"] + "^" + ucTel.fgDocs[i, "ID"] + "~";
                    for (i = 1; i <= ucTel.fgDocs2.Rows.Count - 1; i++)
                        if ((ucTel.fgDocs2[i, 0] + "").Trim().Length > 0 && (ucTel.fgDocs2[i, 5] + "").Trim() == "1")
                            sFiles_ID = sFiles_ID + ucTel.fgDocs2[i, "File_Name"] + "^" + ucTel.fgDocs2[i, "ID"] + "~";

                    if (ucTel.lnkEmail.Text.Trim().Length > 0) sFiles_ID = sFiles_ID + ucTel.lnkEmail.Text + "^" + ucTel.lblEmail_ID.Text + "~";
                    break;
                case 4:
                    Clients.EMail = ucEmail.txtNewValue.Text;
                    break;
                case 5:
                    Clients.Address = ucAddress.txtNewAddress.Text;
                    Clients.City = ucAddress.txtNewCity.Text;
                    Clients.Zip = ucAddress.txtNewZip.Text;
                    Clients.Country_ID = Convert.ToInt32(ucAddress.cmbNewCountry.SelectedValue);

                    for (i = 1; i <= ucAddress.fgDocs.Rows.Count - 1; i++)
                        if ((ucAddress.fgDocs[i, 0] + "").Trim().Length > 0 && (ucAddress.fgDocs[i, 5] + "").Trim() == "1")
                            sFiles_ID = sFiles_ID + ucAddress.fgDocs[i, "File_Name"] + "^" + ucAddress.fgDocs[i, "ID"] + "~";
                    for (i = 1; i <= ucAddress.fgDocs2.Rows.Count - 1; i++)
                        if ((ucAddress.fgDocs2[i, 0] + "").Trim().Length > 0 && (ucAddress.fgDocs2[i, 5] + "").Trim() == "1")
                            sFiles_ID = sFiles_ID + ucAddress.fgDocs2[i, "File_Name"] + "^" + ucAddress.fgDocs2[i, "ID"] + "~";

                    if (ucAddress.lnkEmail.Text.Trim().Length > 0) sFiles_ID = sFiles_ID + ucAddress.lnkEmail.Text + "^" + ucAddress.lblEmail_ID.Text + "~";
                    break;
                case 6:
                    Clients.AFM = ucAFM.txtNewAFM.Text;

                    for (i = 1; i <= ucAFM.fgDocs.Rows.Count - 1; i++)
                        if ((ucAFM.fgDocs[i, 0] + "").Trim().Length > 0 && (ucAFM.fgDocs[i, 5] + "").Trim() == "1")
                            sFiles_ID = sFiles_ID + ucAFM.fgDocs[i, "File_Name"] + "^" + ucAFM.fgDocs[i, "ID"] + "~";
                    break;
                case 7:
                    Clients.Ekkatharistika = ucEkkatharistika.chkDenExo.Checked ? 1 : 0;
                    for (i = 1; i <= ucEkkatharistika.fgDocs.Rows.Count - 1; i++)
                        if ((ucEkkatharistika.fgDocs[i, 1] + "").Trim().Length > 0)
                            sFiles_ID = sFiles_ID + ucEkkatharistika.fgDocs[i, 1] + "^" + ucEkkatharistika.fgDocs[i, "ID"] + "~";           // col1 = File_Name

                    for (i = 1; i <= ucEkkatharistika.fgDocs2.Rows.Count - 1; i++)
                        if ((ucEkkatharistika.fgDocs2[i, 0] + "").Trim().Length > 0)
                            sFiles_ID = sFiles_ID + ucEkkatharistika.fgDocs2[i, "File_Name"] + "^" + ucEkkatharistika.fgDocs2[i, "ID"] + "~";

                    break;
                case 8:
                    Clients.Brunch_ID = Convert.ToInt32(ucSpecial.cmbNewSpec.SelectedValue);

                    for (i = 1; i <= ucSpecial.fgDocs.Rows.Count - 1; i++)
                        if ((ucSpecial.fgDocs[i, 0] + "").Trim().Length > 0 && (ucSpecial.fgDocs[i, 5] + "").Trim() == "1")
                            sFiles_ID = sFiles_ID + ucSpecial.fgDocs[i, "File_Name"] + "^" + ucSpecial.fgDocs[i, "ID"] + "~";

                    if (ucSpecial.lnkEmail.Text.Trim().Length > 0) sFiles_ID = sFiles_ID + ucSpecial.lnkEmail.Text + "^" + ucSpecial.lblEmail_ID.Text + "~";

                    break;
                case 9:
                    Clients.CountryTaxes_ID = Convert.ToInt32(ucCountryTaxes.cmbNewCountry.SelectedValue);

                    for (i = 1; i <= ucCountryTaxes.fgDocs.Rows.Count - 1; i++)
                        if ((ucCountryTaxes.fgDocs[i, 0] + "").Trim().Length > 0 && (ucCountryTaxes.fgDocs[i, 5] + "").Trim() == "1")
                            sFiles_ID = sFiles_ID + ucCountryTaxes.fgDocs[i, "File_Name"] + "^" + ucCountryTaxes.fgDocs[i, "ID"] + "~";

                    if (ucCountryTaxes.lnkEmail.Text.Trim().Length > 0) sFiles_ID = sFiles_ID + ucCountryTaxes.lnkEmail.Text + "^" + ucCountryTaxes.lblEmail_ID.Text + "~";
                    break;
                case 10:
                    sTemp = "";
                    sFiles_ID = "";
                    if (ucSpecCateg.chkDenAniko.Checked)
                    {
                        sTemp = "0;";
                        sFiles_ID = "~";
                    }
                    else
                    {
                        clsClients_SpecialCategories Clients_SpecialCategories = new clsClients_SpecialCategories();

                        Clients_SpecialCategories = new clsClients_SpecialCategories();
                        Clients_SpecialCategories.Client_ID = iClient_ID;
                        Clients_SpecialCategories.DeleteRecord_ClientID();

                        for (i = 1; i <= ucSpecCateg.fgList.Rows.Count - 1; i++)
                        {
                            if (Convert.ToBoolean(ucSpecCateg.fgList[i, 1]))
                            {
                                Clients_SpecialCategories = new clsClients_SpecialCategories();
                                Clients_SpecialCategories.Client_ID = iClient_ID;
                                Clients_SpecialCategories.SpecCategory_ID = Convert.ToInt32(ucSpecCateg.fgList[i, "Num"]);
                                Clients_SpecialCategories.FileName = ucSpecCateg.fgList[i, "File_Name"] + "";
                                Clients_SpecialCategories.InsertRecord();

                                sTemp = sTemp + ";" + ucSpecCateg.fgList[i, "Num"];
                                sFiles_ID = sFiles_ID + ucSpecCateg.fgList[i, "File_Name"] + "^" + ucSpecCateg.fgList[i, "ID"] + "~";
                            }
                        }

                        sTemp = sTemp + ";";
                    }

                    Clients.SpecialCategory = sTemp;
                    break;
                case 11:
                    if (iAction == 0)
                    {
                        Clients_BankAccounts = new clsClients_BankAccounts();
                        Clients_BankAccounts.Client_ID = iClient_ID;
                        Clients_BankAccounts.Bank_ID = Convert.ToInt32(ucBankAccount.cmbBanks.SelectedValue);
                        Clients_BankAccounts.AccNumber = ucBankAccount.txtAccNumber.Text;
                        Clients_BankAccounts.AccType = Convert.ToInt32(ucBankAccount.cmbType.SelectedIndex);
                        Clients_BankAccounts.AccOwners = ucBankAccount.txtOwners.Text;
                        Clients_BankAccounts.Currency = ucBankAccount.cmbCurrencies.Text;
                        Clients_BankAccounts.StartBalance = 0;
                        Clients_BankAccounts.Status = 1;
                        Clients_BankAccounts.InsertRecord();

                        for (i = 1; i <= ucBankAccount.fgDocs.Rows.Count - 1; i++)
                            if ((ucBankAccount.fgDocs[i, 0] + "").Trim().Length > 0 && (ucBankAccount.fgDocs[i, 5] + "").Trim() == "1")
                                sFiles_ID = sFiles_ID + ucBankAccount.fgDocs[i, "File_Name"] + "^" + ucBankAccount.fgDocs[i, "ID"] + "~";

                        if (ucBankAccount.lnkEmail.Text.Trim().Length > 0) sFiles_ID = sFiles_ID + ucBankAccount.lnkEmail.Text + "^" + ucBankAccount.lblEmail_ID.Text + "~";
                        break;
                    }
                    if (iAction == 2)
                    {
                        iBankAccount_ID = Convert.ToInt32(ucBankAccount_Delete.lblBankAccount_ID.Text);
                        Clients_BankAccounts = new clsClients_BankAccounts();
                        Clients_BankAccounts.Record_ID = iBankAccount_ID;
                        Clients_BankAccounts.GetRecord();
                        Clients_BankAccounts.Status = 0;
                        Clients_BankAccounts.EditRecord();
                    }
                    break;
                case 12:
                    if (rbAdult.Checked)
                    {
                        if (iAction == 0)
                        {
                            Clients_Clients = new clsClients_Clients();
                            Clients_Clients.Client_ID = iClient_ID;
                            Clients_Clients.Client2_ID = Convert.ToInt32(ucCoOwner.lblClient2_ID.Text);
                            Clients_Clients.Status = 1;
                            Clients_Clients.DateIns = DateTime.Now;
                            Clients_Clients.InsertRecord();
                        }
                        if (iAction == 2)
                        {
                            Clients_Clients = new clsClients_Clients();
                            Clients_Clients.Record_ID = iClients_Clients_ID;
                            Clients_Clients.DeleteRecord();
                        }
                    }
                    if (rbChild.Checked)
                    {
                        if (iAction == 0)
                        {
                            Clients_Clients = new clsClients_Clients();
                            Clients_Clients.Client_ID = iClient_ID;
                            Clients_Clients.Client2_ID = Convert.ToInt32(ucCowner_Child.lblClient2_ID.Text);
                            Clients_Clients.Status = 1;
                            Clients_Clients.DateIns = DateTime.Now;
                            Clients_Clients.InsertRecord();
                        }
                        if (iAction == 2)
                        {
                            Clients_Clients = new clsClients_Clients();
                            Clients_Clients.Record_ID = Convert.ToInt32(ucCowner_Delete.lblRec_ID.Text);
                            Clients_Clients.DeleteRecord();
                        }
                    }
                    break;
                case 13:
                    break;
                case 14:
                    Clients.Passport = ucPasport.txtNewNumber.Text;
                    Clients.Passport_Police = ucPasport.txtNewPolice.Text;
                    Clients.Passport_ExpireDate = ucPasport.dNewExpireDate.Text;

                    for (i = 1; i <= ucPasport.fgDocs.Rows.Count - 1; i++)
                        if ((ucPasport.fgDocs[i, 0] + "").Trim().Length > 0 && (ucPasport.fgDocs[i, 5] + "").Trim() == "1")
                            sFiles_ID = sFiles_ID + ucPasport.fgDocs[i, "File_Name"] + "^" + ucPasport.fgDocs[i, "ID"] + "~";

                    break;
                case 15:
                    Clients.Merida = ucMerida.txtNewMerida.Text;
                    for (i = 1; i <= ucMerida.fgDocs.Rows.Count - 1; i++)
                        if ((ucMerida.fgDocs[i, 0] + "").Trim().Length > 0 && (ucMerida.fgDocs[i, 5] + "").Trim() == "1")
                            sFiles_ID = sFiles_ID + ucMerida.fgDocs[i, "File_Name"] + "^" + ucMerida.fgDocs[i, "ID"] + "~";

                    break;
                case 16:
                    Clients.LogAxion = ucLogAxion.txtNewLogAxion.Text;
                    for (i = 1; i <= ucLogAxion.fgDocs.Rows.Count - 1; i++)
                        if ((ucLogAxion.fgDocs[i, 0] + "").Trim().Length > 0 && (ucLogAxion.fgDocs[i, 5] + "").Trim() == "1")
                            sFiles_ID = sFiles_ID + ucLogAxion.fgDocs[i, "File_Name"] + "^" + ucLogAxion.fgDocs[i, "ID"] + "~";

                    break;
                case 17:
                    Clients.AMKA = ucAMKA.txtNewAMKA.Text;
                    for (i = 1; i <= ucAMKA.fgDocs.Rows.Count - 1; i++)
                        if ((ucAMKA.fgDocs[i, 0] + "").Trim().Length > 0 && (ucAMKA.fgDocs[i, 5] + "").Trim() == "1")
                            sFiles_ID = sFiles_ID + ucAMKA.fgDocs[i, "File_Name"] + "^" + ucAMKA.fgDocs[i, "ID"] + "~";

                    break;
            }
            Clients.EditRecord();

            WebUsersStates = new clsWebUsersStates();
            WebUsersStates.Client_ID = iClient_ID;
            WebUsersStates.Status = 100;
            WebUsersStates.EditStatus();

            tokens = sFiles_ID.Split('~');
            for (i = 0; i < tokens.Length - 1; i++)
            {
                m = 0;
                bokens = (tokens[i] + "").Split('^');
                if (Global.IsNumeric(bokens[1] + ""))
                {
                    if (Convert.ToInt32(bokens[1] + "") != 0)
                    {
                        ClientDocFiles = new clsClientsDocFiles();
                        ClientDocFiles.Record_ID = Convert.ToInt32(bokens[1] + "");
                        ClientDocFiles.GetRecord();
                        ClientDocFiles.Status = 2;                                                    // 2 - document confirmed
                        ClientDocFiles.EditStatus();
                        m = 1;
                    }
                }
                if (m == 0)
                {
                    foreach (DataRow dtRow in allClientDocFiles.List.Rows)
                    {
                        if ((dtRow["FileName"] + "").Trim() == (bokens[0] + "").Trim() && Convert.ToInt32(dtRow["Status"]) > 0)
                        {
                            ClientDocFiles = new clsClientsDocFiles();
                            ClientDocFiles.Record_ID = Convert.ToInt32(dtRow["ID"] + "");
                            ClientDocFiles.GetRecord();
                            ClientDocFiles.Status = 2;                                                 // 2 - document confirmed
                            ClientDocFiles.EditStatus();
                            break;
                        }
                    }
                }
            }

            ServerJobs = new clsServerJobs();
            ServerJobs.JobType_ID = 46;
            ServerJobs.Source_ID = 0;
            ServerJobs.Parameters = "{'recipient_email': '" + lblEmail.Text + "', 'request_action' : '2', 'request_id': '" + iRequest_ID + "'}";
            ServerJobs.DateStart = DateTime.Now;
            ServerJobs.DateFinish = System.Convert.ToDateTime("1900/01/01");
            ServerJobs.PubKey = "";
            ServerJobs.PrvKey = "";
            ServerJobs.Attempt = 0;
            ServerJobs.Status = 0;
            ServerJobs.InsertRecord();

            ServerJobs = new clsServerJobs();
            ServerJobs.JobType_ID = 46;
            ServerJobs.Source_ID = 0;
            ServerJobs.Parameters = "{'recipient_email': '" + Global.Request_Sender + "', 'request_action' : '202', 'request_id': '" + iRequest_ID + "'}";
            ServerJobs.DateStart = DateTime.Now;
            ServerJobs.DateFinish = System.Convert.ToDateTime("1900/01/01");
            ServerJobs.PubKey = "";
            ServerJobs.PrvKey = "";
            ServerJobs.Attempt = 0;
            ServerJobs.Status = 0;
            ServerJobs.InsertRecord();

            if (sGroup_ID == "") this.Close();
            else
            {
                DefineRequestsList();
                grpL3.Visible = false;
            }
        }
        private void DeleteRequest()
        {
            Clients = new clsClients();
            Clients.Record_ID = iClient_ID;
            Clients.GetRecord();
            if (Clients.Status == 1)                                                        // 1 - Pelatis
            {
                Clients.BlockStatus = 1;                                                   //  1 - Block Pelatis
                Clients.EditRecord();

                WebUsers = new clsWebUsers();
                WebUsers.Client_ID = iClient_ID;
                WebUsers.DeleteRecord_Client_ID();

                if (sGroup_ID == "")
                {
                    iVideoChatStatus = 4;
                    ClientsRequests = new clsClientsRequests();
                    ClientsRequests.Record_ID = iRequest_ID;
                    ClientsRequests.GetRecord();
                    iClient_ID = ClientsRequests.Client_ID;
                    ClientsRequests.Warning = txtWarning.Text;
                    ClientsRequests.DateClose = DateTime.Now;
                    ClientsRequests.Status = 7;                                             // 7 - заявка проверена и отклонена, т.к. неправильно введена
                    ClientsRequests.VideoChatStatus = 4;                                    // 4 - видеочат проведен, но завершен отменой запросов (аннулирован)
                    ClientsRequests.VideoChatFile = lblVideoChatFile.Text;
                    ClientsRequests.EditRecord();
                }
                else
                {
                    for (i = 1; i <= fgList.Rows.Count - 1; i++)
                    {
                        ClientsRequests = new clsClientsRequests();
                        ClientsRequests.Record_ID = Convert.ToInt32(fgList[i, "ID"]);
                        ClientsRequests.GetRecord();
                        iClient_ID = ClientsRequests.Client_ID;
                        ClientsRequests.Warning = txtWarning.Text;
                        ClientsRequests.DateClose = DateTime.Now;
                        ClientsRequests.Status = 7;                                           // 7 - заявка проверена и отклонена, т.к.неправильно введена
                        ClientsRequests.VideoChatStatus = 4;                                  // 4 - видеочат проведен, но завершен отменой запросов (аннулирован)
                        ClientsRequests.VideoChatFile = lblGroupVideoChatFile.Text;
                        ClientsRequests.EditRecord();
                    }
                }

                ServerJobs = new clsServerJobs();
                ServerJobs.JobType_ID = 46;
                ServerJobs.Source_ID = 0;
                ServerJobs.Parameters = "{'recipient_email': '" + lblEmail.Text + "', 'request_action' : '24'}";
                ServerJobs.DateStart = DateTime.Now;
                ServerJobs.DateFinish = System.Convert.ToDateTime("1900/01/01");
                ServerJobs.PubKey = "";
                ServerJobs.PrvKey = "";
                ServerJobs.Attempt = 0;
                ServerJobs.Status = 0;
                ServerJobs.InsertRecord();
            }
            else                                                                               // Ypopsifios Pelatis
            {
                //--- delete WebUsers & WebUsersStates records --------------------------
                clsWebUsers WebUsers2 = new clsWebUsers();
                WebUsers = new clsWebUsers();
                clsWebUsersStates WebUsersStates2 = new clsWebUsersStates();
                WebUsersStates = new clsWebUsersStates();

                WebUsers.Client_ID = iClient_ID;
                WebUsers.GetList();
                foreach (DataRow dtRow in WebUsers.List.Rows)
                {
                    WebUsersStates2 = new clsWebUsersStates();
                    WebUsersStates = new clsWebUsersStates();
                    WebUsersStates.WU_ID = Convert.ToInt32(dtRow["ID"]);
                    WebUsersStates.GetList();
                    foreach (DataRow dtRow1 in WebUsersStates.List.Rows)
                    {
                        WebUsersStates2.Record_ID = Convert.ToInt32(dtRow1["ID"]);
                        WebUsersStates2.DeleteRecord();
                    }

                    WebUsers2.Record_ID = Convert.ToInt32(dtRow["ID"]);
                    WebUsers2.DeleteRecord();
                }

                iVideoChatStatus = 4;
                if (sGroup_ID == "")
                {
                    ClientsRequests = new clsClientsRequests();
                    ClientsRequests.Record_ID = iRequest_ID;
                    ClientsRequests.DeleteRecord();
                }
                else
                {
                    for (i = 1; i <= fgList.Rows.Count - 1; i++)
                    {
                        ClientsRequests = new clsClientsRequests();
                        ClientsRequests.Record_ID = Convert.ToInt32(fgList[i, "ID"]);
                        ClientsRequests.DeleteRecord();
                    }
                }

                //--- Delete Ypopsifios Pelatis ---------------------
                Clients.DeleteRecord();

                ServerJobs = new clsServerJobs();
                ServerJobs.JobType_ID = 46;
                ServerJobs.Source_ID = 0;
                ServerJobs.Parameters = "{'recipient_email': '" + lblEmail.Text + "', 'request_action' : '22'}";
                ServerJobs.DateStart = DateTime.Now;
                ServerJobs.DateFinish = System.Convert.ToDateTime("1900/01/01");
                ServerJobs.PubKey = "";
                ServerJobs.PrvKey = "";
                ServerJobs.Attempt = 0;
                ServerJobs.Status = 0;
                ServerJobs.InsertRecord();
            }
        }
        private void CancelSingleRequest(string sGroup_ID, int iRequest_ID, string sWarning, string sVideoChatFile)
        {
            iVideoChatStatus = 4;
            ClientsRequests = new clsClientsRequests();
            ClientsRequests.Record_ID = iRequest_ID;
            ClientsRequests.GetRecord();
            iClient_ID = ClientsRequests.Client_ID;
            ClientsRequests.Warning = sWarning;
            ClientsRequests.DateClose = DateTime.Now;
            ClientsRequests.Status = 7;                                             // 7 - заявка проверена и отклонена, т.к. неправильно введена
            ClientsRequests.VideoChatStatus = 4;                                    // 4 - видеочат проведен, но завершен отменой запросов (аннулирован)
            ClientsRequests.VideoChatFile = sVideoChatFile;
            ClientsRequests.EditRecord();

            if (sGroup_ID == "")
            {
                ServerJobs = new clsServerJobs();
                ServerJobs.JobType_ID = 46;
                ServerJobs.Source_ID = 0;
                ServerJobs.Parameters = "{'recipient_email': '" + lblEmail.Text + "', 'request_action' : '3', 'request_id': '" + iRequest_ID + "'}";
                ServerJobs.DateStart = DateTime.Now;
                ServerJobs.DateFinish = System.Convert.ToDateTime("1900/01/01");
                ServerJobs.PubKey = "";
                ServerJobs.PrvKey = "";
                ServerJobs.Attempt = 0;
                ServerJobs.Status = 0;
                ServerJobs.InsertRecord();

                ServerJobs = new clsServerJobs();
                ServerJobs.JobType_ID = 46;
                ServerJobs.Source_ID = 0;
                ServerJobs.Parameters = "{'recipient_email': '" + Global.Request_Sender + "', 'request_action' : '203', 'request_id': '" + iRequest_ID + "'}";
                ServerJobs.DateStart = DateTime.Now;
                ServerJobs.DateFinish = System.Convert.ToDateTime("1900/01/01");
                ServerJobs.PubKey = "";
                ServerJobs.PrvKey = "";
                ServerJobs.Attempt = 0;
                ServerJobs.Status = 0;
                ServerJobs.InsertRecord();

                if (chkDeleteUser.Checked) DeleteRequest();
            }
            else
                fgList[fgList.Row, "Warning"] = txtWarning.Text;
        }
        #endregion ----------------------------------------------------------------------------------
        #region --- fgList functionality ------------------------------------------------------------------------
        private void fgList_RowColChange(object sender, EventArgs e)
        {
            if (fgList.Row > 0 && fgList.Rows.Count > 1)
            {
                iRequest_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                ClientsRequests = new clsClientsRequests();
                ClientsRequests.Record_ID = iRequest_ID;
                ClientsRequests.GetRecord();
                iClient_ID = ClientsRequests.Client_ID;
                iRequestTipos = ClientsRequests.Tipos;
                iAction = ClientsRequests.Action;
                sDescription = ClientsRequests.Description;
                dDateIns = ClientsRequests.DateIns;
                iStatus = ClientsRequests.Status;
                lblEmail.Text = ClientsRequests.Email;
                sAuthor_EMail = ClientsRequests.Author_EMail;
                if (sGroup_ID == "") txtWarning.Text = ClientsRequests.Warning;
                else txtWarning.Text = fgList[fgList.Row, "Warning"] + "";

                lblClientName.Visible = true;
                ucCS.Visible = false;

                if (iClient_ID != 0)
                {
                    Clients = new clsClients();
                    Clients.Record_ID = iClient_ID;
                    Clients.EMail = "";
                    Clients.Mobile = "";
                    Clients.AFM = "";
                    Clients.DoB = Convert.ToDateTime("1900/01/01");
                    Clients.GetRecord();
                    ShowClientData();
                    lblClientName.Text = Clients.Fullname;
                }

                CloseAllEditWindows();
                ShowRequestWindow();
                grpL3.Visible = true;
            }

        }
        private void DefineRequestsList()
        {
            Boolean bAllRequestOK = true;

            if (sGroup_ID != "")
            {
                clsClientsRequests klsClientRequests = new clsClientsRequests();
                klsClientRequests.Group_ID = sGroup_ID;
                klsClientRequests.GetList_Group();

                i = 0;
                fgList.Redraw = false;
                fgList.Rows.Count = 1;
                foreach (DataRow dtRow in klsClientRequests.List.Rows)
                {
                    //                Request's Status > 0               Source_ID = 1 from MobiUser         user has Status >= 1 (kataxoron or elengktis)       Source_ID == 2 from Windows ISP                                                                                       is For Checking   
                    if ((Convert.ToInt32(dtRow["Status"]) > 0) && (Convert.ToInt32(dtRow["Source_ID"]) == 1) && (Global.ClientsRequests_Status > 1) || (Convert.ToInt32(dtRow["Source_ID"]) == 2))
                    {
                        if (Convert.ToInt32(dtRow["Action"]) == 0) sTemp = dtRow["ClientsRequest_Type_0"] + "";
                        if (Convert.ToInt32(dtRow["Action"]) == 1) sTemp = dtRow["ClientsRequest_Type_1"] + "";
                        if (Convert.ToInt32(dtRow["Action"]) == 2) sTemp = dtRow["ClientsRequest_Type_2"] + "";

                        i = i + 1;
                        fgList.AddItem(dtRow["ID"] + "\t" + sTemp + "\t" + sStatus[Convert.ToInt32(dtRow["Status"])] + "\t" + dtRow["Group_ID"] + "\t" + dtRow["RequestTipos"] + "\t" +
                                       dtRow["Action"] + "\t" + dtRow["Status"] + "\t" + dtRow["VideoChatStatus"] + "\t" + dtRow["Warning"]);
                        if (Convert.ToInt32(dtRow["Status"]) < 4) bAllRequestOK = false;
                        //iVideoChatStatus = Convert.ToInt32(dtRow["VideoChatStatus"]);
                        //lblGroupVideoChatFile.Text = dtRow["VideoChatFile"] + "";
                    }
                }
                fgList.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Free;
                fgList.Cols[3].AllowMerging = true;
                fgList.Row = 0;
                fgList.Redraw = true;
                fgList.Row = 1;
            }

            if (iVideoChatStatus > 0 && bAllRequestOK)
            {
                switch (iVideoChatStatus)
                {
                    case 1:
                        lblMultiVideoChatStatus.Text = "Video Κλήση είναι σε Αναμονή";
                        btnGroupVideoChat_Cancel.Visible = true;
                        btnGroupVideoChat_Confirm.Visible = true;
                        btnGroupVideoChat_OK.Visible = true;
                        break;
                    case 2:
                        lblMultiVideoChatStatus.Text = "Έγινε έλεγχος της Video Κλήσης";
                        btnGroupVideoChat_Cancel.Visible = true;
                        btnGroupVideoChat_Confirm.Visible = true;
                        btnGroupVideoChat_OK.Visible = true;
                        break;
                    case 3:
                        lblMultiVideoChatStatus.Text = "Video Κλήση Oριστικοποιήθηκε";
                        btnGroupVideoChat_Cancel.Visible = false;
                        btnGroupVideoChat_Confirm.Visible = false;
                        btnGroupVideoChat_OK.Visible = false;
                        break;
                    case 4:
                        lblMultiVideoChatStatus.Text = "Video Κλήση Απορρίφθηκε";
                        btnGroupVideoChat_Cancel.Visible = false;
                        btnGroupVideoChat_Confirm.Visible = false;
                        btnGroupVideoChat_OK.Visible = false;
                        break;
                }
            }
            else grpGroupVideoChat.Visible = false;
        }
        #endregion
        #region --- Search request ------------------------------------------------------------------------------
        private void picADT_Click(object sender, EventArgs e)
        {
            iRequestTipos = 1;                                   // 1 - Ταυτότητα
            if (iADTStatus == 0)
            {
                lblTitle.Text = "Ταυτότητα";
                lblData1.Text = "Aριθμός: " + lblADT.Text;
                lblData2.Text = "Αρχή Έκδοσης: " + sPolice;
                lblData3.Text = "Ημ/νία Λήξης: " + sExpireDate;
                if (lblADT.Text == "")
                {
                    iStatus = 0;
                    iAction = 0;
                    btnCreateRequest.Text = "Αίτημα προσθήκης";
                }
                else
                {
                    iStatus = 1;
                    iAction = 1;
                    btnCreateRequest.Text = "Αίτημα αλλαγής";
                }
                rbAdult.Visible = false;
                rbChild.Visible = false;
                lblText.Text = "Για να αλλάξετε τα στοιχεία της ταυτότητας πατήαστε '" + btnCreateRequest.Text + "'";
                grpL2.Visible = true;
            }
            else
            {
                ShowRequestWindow();
                grpL2.Visible = false;
                grpL3.Visible = true;
            }
        }
        private void picMobile_Click(object sender, EventArgs e)
        {
            iRequestTipos = 2;                                    // 2 - Κινητό τηλέφωνο
            lblTitle.Text = "Κινητό τηλέφωνο";
            lblData1.Text = lblMobile.Text;
            lblData2.Text = "";
            lblData3.Text = "";
            if (lblMobile.Text == "")
            {
                iStatus = 0;
                iAction = 0;
                btnCreateRequest.Text = "Αίτημα προσθήκης";
            }
            else
            {
                iStatus = 1;
                iAction = 1;
                btnCreateRequest.Text = "Αίτημα αλλαγής";
            }
            rbAdult.Visible = false;
            rbChild.Visible = false;
            lblText.Text = "Για να αλλάξετε το κινητό τηλέφωνο επικοινωνίας πατήαστε '" + btnCreateRequest.Text + "'";
            grpL2.Visible = true;
        }
        private void picTel_Click(object sender, EventArgs e)
        {
            iRequestTipos = 3;                                    // 3 - Σταθερό τηλέφωνο
            lblTitle.Text = "Σταθερό τηλέφωνο";
            lblData1.Text = lblTel.Text;
            lblData2.Text = "";
            lblData3.Text = "";
            if (lblTel.Text == "")
            {
                iStatus = 0;
                iAction = 0;
                btnCreateRequest.Text = "Αίτημα προσθήκης";
            }
            else
            {
                iStatus = 1;
                iAction = 1;
                btnCreateRequest.Text = "Αίτημα αλλαγής";
            }
            rbAdult.Visible = false;
            rbChild.Visible = false;
            lblText.Text = "Για να αλλάξετε το σταθερό τηλέφωνο επικοινωνίας πατήαστε '" + btnCreateRequest.Text + "'";
            grpL2.Visible = true;
        }
        private void picEMail_Click(object sender, EventArgs e)
        {
            iRequestTipos = 4;                                   // 4 - e-mail 
            lblTitle.Text = "email επικοινωνίας";
            lblData1.Text = lblEmail.Text;
            lblData2.Text = "";
            lblData3.Text = "";
            if (lblEmail.Text == "")
            {
                iStatus = 0;
                iAction = 0;
                btnCreateRequest.Text = "Αίτημα προσθήκης";
            }
            else
            {
                iStatus = 1;
                iAction = 1;
                btnCreateRequest.Text = "Αίτημα αλλαγής";
            }
            rbAdult.Visible = false;
            rbChild.Visible = false;
            lblText.Text = "Για να αλλάξετε το email σας πατήαστε '" + btnCreateRequest.Text + "'";
            grpL2.Visible = true;
        }
        private void picAddress_Click(object sender, EventArgs e)
        {
            iRequestTipos = 5;                                      // 5 - Διεύθυνση κατοικίας 
            lblTitle.Text = "Διεύθυνση κατοικίας";
            lblData1.Text = "Οδός: " + lblAddress.Text;
            lblData2.Text = "Πόλη: " + sCity;
            lblData3.Text = "Τ.Κ.: " + sZip + "    Χώρα: " + sCountry;
            if (lblAddress.Text == "")
            {
                iStatus = 0;
                iAction = 0;
                btnCreateRequest.Text = "Αίτημα προσθήκης";
            }
            else
            {
                iStatus = 1;
                iAction = 1;
                btnCreateRequest.Text = "Αίτημα αλλαγής";
            }
            rbAdult.Visible = false;
            rbChild.Visible = false;
            lblText.Text = "Για να αλλάξετε τα στοιχεία της διεύθυνσης κατοικίας σας πατήαστε '" + btnCreateRequest.Text + "'";
            grpL2.Visible = true;
        }
        private void picAFM_Click(object sender, EventArgs e)
        {
            iRequestTipos = 6;                                          // 6 - ΑΦΜ
            lblTitle.Text = "ΑΦΜ";
            lblData1.Text = lblAFM.Text;
            lblData2.Text = "";
            lblData3.Text = "";
            if (lblAFM.Text == "")
            {
                iStatus = 0;
                iAction = 0;
                btnCreateRequest.Text = "Αίτημα προσθήκης";
            }
            else
            {
                iStatus = 1;
                iAction = 1;
                btnCreateRequest.Text = "Αίτημα αλλαγής";
            }
            rbAdult.Visible = false;
            rbChild.Visible = false;
            lblText.Text = "Για να αλλάξετε το ΑΦΜ σας πατήαστε '" + btnCreateRequest.Text + "'";
            grpL2.Visible = true;
        }
        private void picEkkataristika_Click(object sender, EventArgs e)
        {
            iRequestTipos = 7;                                          // 7 - Εκκαθαριστικά
            lblTitle.Text = "Εκκαθαριστικά";
            j = 0;
            for (i = iTaxDeclarationsLastYear; i >= iTaxDeclarations1Year; i--)
            {
                j = j + 1;
                if (j == 1) lblData1.Text = i + "  - ";
                if (j == 2) lblData2.Text = i + "  - ";
                if (j == 3) lblData3.Text = i + "  - ";
            }

            ClientDocFiles = new clsClientsDocFiles();
            ClientDocFiles.Client_ID = iClient_ID;
            ClientDocFiles.PreContract_ID = 0;
            ClientDocFiles.Contract_ID = 0;
            ClientDocFiles.DocTypes = 3924;                          // 3924 - Ekkatharistika
            ClientDocFiles.GetList();
            foreach (DataRow dtRow in ClientDocFiles.List.Rows)
                if ((dtRow["FileName"] + "").Trim() != "" && Convert.ToInt32(dtRow["Status"]) == 2)
                {
                    sTemp = dtRow["FileName"] + "";
                    i = sTemp.IndexOf("ΕΚΚΑΘΑΡΙΣΤΙΚΟ ");
                    if (i >= 0)
                    {
                        sTemp = sTemp.Substring(14, 4);
                        if (sTemp == lblData1.Text.Substring(0, 4)) lblData1.Text = lblData1.Text + "   OK";
                        if (sTemp == lblData2.Text.Substring(0, 4)) lblData2.Text = lblData2.Text + "   OK";
                        if (sTemp == lblData3.Text.Substring(0, 4)) lblData3.Text = lblData3.Text + "   OK";
                    }
                }

            iStatus = 0;
            iAction = 0;
            btnCreateRequest.Text = "Προσθήκη στοιχείων";
            rbAdult.Visible = false;
            rbChild.Visible = false;
            lblText.Text = "Για να προσθέσετε την Εκκαθαριστική σας πατήαστε '" + btnCreateRequest.Text + "'";
            grpL2.Visible = true;
        }
        private void picSpec_Click(object sender, EventArgs e)
        {
            iRequestTipos = 8;                                           // 8 - Αίτημα αλλαγής επαγγέλματος
            lblTitle.Text = "Επάγγελμα";
            lblData1.Text = sSpec;
            lblData2.Text = "";
            lblData3.Text = "";
            if (sSpec == "")
            {
                iStatus = 0;
                iAction = 0;
                btnCreateRequest.Text = "Αίτημα προσθήκης";
            }
            else
            {
                iStatus = 1;
                iAction = 1;
                btnCreateRequest.Text = "Αίτημα αλλαγής";
            }
            rbAdult.Visible = false;
            rbChild.Visible = false;
            lblText.Text = "Για να αλλάξετε το επάγγελμα σας πατήαστε '" + btnCreateRequest.Text + "'";
            grpL2.Visible = true;
        }
        private void picCountryTaxes_Click(object sender, EventArgs e)
        {
            iRequestTipos = 9;                                                  // 9 - Χώρα φορολόγησης
            lblTitle.Text = "Χώρα φορολόγησης";
            lblData1.Text = sCountryTaxes;
            lblData2.Text = "";
            lblData3.Text = "";
            if (sCountryTaxes == "")
            {
                iStatus = 0;
                iAction = 0;
                btnCreateRequest.Text = "Αίτημα προσθήκης";
            }
            else
            {
                iStatus = 1;
                iAction = 1;
                btnCreateRequest.Text = "Αίτημα αλλαγής";
            }
            rbAdult.Visible = false;
            rbChild.Visible = false;
            lblText.Text = "Για να αλλάξετε την χώρα φορολόγησης σας πατήαστε '" + btnCreateRequest.Text + "'";
            grpL2.Visible = true;
        }
        private void picCategory_Click(object sender, EventArgs e)
        {
            iRequestTipos = 10;                                                  // 10 - Αίτημα αλλαγής της ειδικής κατηγορίας προσώπου
            lblTitle.Text = "Ειδική κατηγορία προσώπου";
            lblData1.Text = lblSpecialCategory.Text;
            lblData2.Text = "";
            lblData3.Text = "";
            if (sSpecCateg == "") iStatus = 0;
            else iStatus = 1;
            iAction = 1;
            lblText.Text = "Για να αλλάξετε την ειδική κατηγορία προσώπου σας πατήαστε 'Αίτημα αλλαγής'";
            grpL2.Visible = true;
        }
        private void tsbAdd_BankAccount_Click(object sender, EventArgs e)
        {
            iRequestTipos = 11;                                                   // 11 & iAction = 0 - Αίτημα προσθήκης νέου τραπεζικού λογαριασμού
            lblTitle.Text = "Προσθήκη νέου Τραπεζικού λογαριασμού";
            lblData1.Text = "";
            lblData2.Text = "";
            lblData3.Text = "";
            iStatus = 0;
            iAction = 0;
            btnCreateRequest.Text = "Αίτημα προσθήκης";
            rbAdult.Visible = false;
            rbChild.Visible = false;
            lblText.Text = "Για να προσθέσετε το νέο τραπεζικό λογαριασμό πατήαστε '" + btnCreateRequest.Text + "'";
            grpL2.Visible = true;
        }
        private void tsbDelete_BankAccount_Click(object sender, EventArgs e)
        {
            iRequestTipos = 11;                                                      // 11 & iAction = 2 - Αίτημα διαγραφής τραπεζικού λογαριασμού
            lblTitle.Text = "Διαγραφή Τραπεζικού λογαριασμού";

            Clients_BankAccounts = new clsClients_BankAccounts();
            Clients_BankAccounts.Record_ID = Convert.ToInt32(fgBankAccounts[fgBankAccounts.Row, 2]);
            Clients_BankAccounts.GetRecord();
            lblData1.Text = "Αρ.Λογαριασμόυ: " + Clients_BankAccounts.AccNumber;
            lblData2.Text = "Τράπεζα: " + Clients_BankAccounts.Bank_Title;
            lblData3.Text = "Νόμισμα: " + Clients_BankAccounts.Currency;
            iBankAccount_ID = Convert.ToInt32(fgBankAccounts[fgBankAccounts.Row, 2]);
            iStatus = 1;
            iAction = 2;
            btnCreateRequest.Text = "Αίτημα διαγραφής";
            rbAdult.Visible = false;
            rbChild.Visible = false;
            lblText.Text = "Για να διαγράφετε το τραπεζικό λογαριασμό πατήαστε '" + btnCreateRequest.Text + "'";
            grpL2.Visible = true;
        }
        private void tsbAdd_CoOwner_Click(object sender, EventArgs e)
        {
            iRequestTipos = 12;                                                   // 12 - Αίτημα προσθήκης συνδεδεμένου προσώπου (ανήλικο)
            lblTitle.Text = "Προσθήκη νέου συνδεδεμένου προσώπου";
            lblData1.Text = "";
            lblData2.Text = "";
            lblData3.Text = "";
            rbAdult.Checked = true;
            rbAdult.Visible = true;
            rbChild.Visible = true;
            iStatus = 0;
            iAction = 0;
            btnCreateRequest.Text = "Αίτημα προσθήκης";
            lblText.Text = "Για να προσθέσετε το νέο συνδεδεμένου προσώπου πατήαστε '" + btnCreateRequest.Text + "'";
            grpL2.Visible = true;
        }
        private void tsbDel_CoOwner_Click(object sender, EventArgs e)
        {
            iRequestTipos = 12;                                                      // 12 - Αίτημα διαγραφής συνδεδεμένου προσώπου (ανήλικο)
            lblTitle.Text = "Διαγραφή συνδεδεμένου προσώπου";
            lblData1.Text = fgCoOwners[fgCoOwners.Row, 0] + "";
            lblData2.Text = "";
            lblData3.Text = "";
            iClients_Clients_ID = Convert.ToInt32(fgCoOwners[fgCoOwners.Row, 1]);
            iStatus = 1;
            iAction = 2;
            btnCreateRequest.Text = "Αίτημα διαγραφής";
            rbAdult.Visible = false;
            rbChild.Visible = false;
            lblText.Text = "Για να διαγράφετε το συνδεδεμένου προσώπου πατήαστε '" + btnCreateRequest.Text + "'";
            grpL2.Visible = true;
        }
        private void picW8BEN_Click(object sender, EventArgs e)
        {
            iRequestTipos = 13;                                                         // 13 - Αίτημα αλλαγής  W8 ΒΕΝ
            lblTitle.Text = "W8 ΒΕΝ";
            lblData1.Text = sW8BEN;
            lblData2.Text = "";
            lblData3.Text = "";
            if (sW8BEN == "")
            {
                iStatus = 0;
                iAction = 0;
                btnCreateRequest.Text = "Αίτημα προσθήκης";
            }
            else
            {
                iStatus = 1;
                iAction = 1;
                btnCreateRequest.Text = "Αίτημα αλλαγής";
            }
            rbAdult.Visible = false;
            rbChild.Visible = false;
            lblText.Text = "Για να αλλάξετε το W8 ΒΕΝ σας πατήαστε '" + btnCreateRequest.Text + "'";
            grpL2.Visible = true;
        }
        private void picPassport_Click(object sender, EventArgs e)
        {
            iRequestTipos = 14;                                                       // 14 - Αίτημα αλλαγής  Διαβατηρίου
            if (iPassportStatus == 0)
            {
                lblTitle.Text = "Διαβατήριο";
                lblData1.Text = "Aριθμός: " + lblPassport.Text;
                lblData2.Text = "Αρχή Έκδοσης: " + sPassport_Police;
                lblData3.Text = "Ημ/νία Λήξης: " + sPassport_ExpireDate;
                if (lblPassport.Text == "")
                {
                    iStatus = 0;
                    iAction = 0;
                    btnCreateRequest.Text = "Αίτημα προσθήκης";
                }
                else
                {
                    iStatus = 1;
                    iAction = 1;
                    btnCreateRequest.Text = "Αίτημα αλλαγής";
                }
                rbAdult.Visible = false;
                rbChild.Visible = false;
                lblText.Text = "Για να αλλάξετε τα στοιχεία του διαβατηρίου σας πατήαστε '" + btnCreateRequest.Text + "'";
                grpL2.Visible = true;
            }
            else
            {
                ShowRequestWindow();
                grpL2.Visible = false;
                grpL3.Visible = true;
            }
        }
        private void picMerida_Click(object sender, EventArgs e)
        {
            iRequestTipos = 15;                                                         // 15 - Αίτημα αλλαγής Χρηματιστηριακής Μερίδας
            lblTitle.Text = "Χρηματιστηριακή Μερίδα";
            lblData1.Text = lblMerida.Text;
            lblData2.Text = "";
            lblData3.Text = "";
            if (lblMerida.Text == "")
            {
                iStatus = 0;
                iAction = 0;
                btnCreateRequest.Text = "Αίτημα προσθήκης";
            }
            else
            {
                iStatus = 1;
                iAction = 1;
                btnCreateRequest.Text = "Αίτημα αλλαγής";
            }
            rbAdult.Visible = false;
            rbChild.Visible = false;
            lblText.Text = "Για να αλλάξετε την Χρηματιστηριακή Μερίδα σας πατήαστε '" + btnCreateRequest.Text + "'";
            grpL2.Visible = true;
        }

        private void picLogAxion_Click(object sender, EventArgs e)
        {
            iRequestTipos = 16;                                                  // 16 - Αίτημα αλλαγής  Λογαριασμόυ Αξιών
            lblTitle.Text = "Λογαριασμός Αξιών";
            lblData1.Text = lblLogAxion.Text;
            lblData2.Text = "";
            lblData3.Text = "";
            if (lblLogAxion.Text == "")
            {
                iStatus = 0;
                iAction = 0;
                btnCreateRequest.Text = "Αίτημα προσθήκης";
            }
            else
            {
                iStatus = 1;
                iAction = 1;
                btnCreateRequest.Text = "Αίτημα αλλαγής";
            }
            rbAdult.Visible = false;
            rbChild.Visible = false;
            lblText.Text = "Για να αλλάξετε τον Λογαριασμό Αξιών σας πατήαστε '" + btnCreateRequest.Text + "'";
            grpL2.Visible = true;
        }
        private void picAMKA_Click(object sender, EventArgs e)
        {
            iRequestTipos = 17;                                                         // 17 - Αίτημα αλλαγής  ΑΜΚΑ
            lblTitle.Text = "ΑΜΚΑ";
            lblData1.Text = lblAMKA.Text;
            lblData2.Text = "";
            lblData3.Text = "";
            if (lblAMKA.Text == "")
            {
                iStatus = 0;
                iAction = 0;
                btnCreateRequest.Text = "Αίτημα προσθήκης";
            }
            else
            {
                iStatus = 1;
                iAction = 1;
                btnCreateRequest.Text = "Αίτημα αλλαγής";
            }
            rbAdult.Visible = false;
            rbChild.Visible = false;
            lblText.Text = "Για να αλλάξετε την ΑΜΚΑ σας πατήαστε '" + btnCreateRequest.Text + "'";
            grpL2.Visible = true;
        }
        #endregion
        #region --- VideoChat functionality ---------------------------------------------------------------------
        private void picLoadVideoChatFile_Click(object sender, EventArgs e)
        {
            lblVideoChatFile.Text = UploadVideoChatFile();
            if (lblVideoChatFile.Text != "") picShowVideoChatFile.Visible = true;
            else picShowVideoChatFile.Visible = false;

            btnVideoChat_Confirm.Visible = true;
            btnVideoChat_OK.Visible = true;
            btnVideoChat_Cancel.Visible = true;
        }
        private void picShowVideoChatFile_Click(object sender, EventArgs e)
        {
            if (lblVideoChatFile.Text != "") Global.DMS_ShowFile("Customers/" + lblClientName.Text.Replace(".", "_"), lblVideoChatFile.Text);
        }
        private void btnVideoChat_Cancel_Click(object sender, EventArgs e)
        {
            iCancelType = 2;        // 1 - Standalone Request, 2 - Standalone VideoMode, 3 - Group Requests, 4 - Group Requests VideoChat Mode
            chkDeleteUser.Text = iClientStatus == 1 ? "Block του πελάτη" : "Διαγραφή του πελάτη";
            if (txtWarning.Text.Length == 0) btnOK_Warning.Enabled = false;
            else btnOK_Warning.Enabled = true;
            panWarning.Visible = true;
            txtWarning.Focus();
        }
        private void btnVideoChat_Confirm_Click(object sender, EventArgs e)
        {
            iVideoChatStatus = 2;
            ClientsRequests = new clsClientsRequests();
            ClientsRequests.Record_ID = iRequest_ID;
            ClientsRequests.GetRecord();
            ClientsRequests.Status = 5;                                             // 5 – сделана первая проверка результатов видеочата
            ClientsRequests.VideoChatStatus = 2;                                    // 2 - видеочат проведен и сделана первая проверка
            ClientsRequests.VideoChatFile = lblVideoChatFile.Text;
            ClientsRequests.EditRecord();

            this.Close();
        }
        private void btnVideoChat_OK_Click(object sender, EventArgs e)
        {
            SaveRequest(4);
            DefineRequestsList();
            SaveRequestDataIntoDB();

            iVideoChatStatus = 3;
            ClientsRequests = new clsClientsRequests();
            ClientsRequests.Record_ID = iRequest_ID;
            ClientsRequests.GetRecord();
            ClientsRequests.Status = 6;                                             // 6 - сделана вторая проверка результатов видеочата
            ClientsRequests.VideoChatStatus = 3;                                    // 3 - видеочат проведен, сделана первая проверка, а значит он успешно завершен
            ClientsRequests.VideoChatFile = lblVideoChatFile.Text;
            ClientsRequests.EditRecord();

            if (lblVideoChatFile.Text.Trim() != "")
            {
                lblVideoChatFile_ID.Text = "0";

                foreach (DataRow dtRow in allClientDocFiles.List.Rows)
                {
                    if ((dtRow["FileName"] + "").Trim() == lblVideoChatFile.Text.Trim() && Convert.ToInt32(dtRow["Status"]) > 0)
                    {
                        lblVideoChatFile_ID.Text = dtRow["ID"] + "";
                        break;
                    }
                }

                ClientDocFiles = new clsClientsDocFiles();
                ClientDocFiles.Record_ID = Convert.ToInt32(lblVideoChatFile_ID.Text);
                ClientDocFiles.GetRecord();
                ClientDocFiles.Status = 2;                                                 // 2 - document confirmed
                ClientDocFiles.EditStatus();
            }

            WebUsersStates = new clsWebUsersStates();
            WebUsersStates.Client_ID = iClient_ID;
            WebUsersStates.Status = 100;
            WebUsersStates.EditStatus();

            sGroup_ID = "";
            this.Close();
        }
        private void picFilesPath_Group_Click(object sender, EventArgs e)
        {
            lblGroupVideoChatFile.Text = UploadVideoChatFile();
            btnGroupVideoChat_Cancel.Visible = true;
            btnGroupVideoChat_Confirm.Visible = true;
            btnGroupVideoChat_OK.Visible = true;
        }
        private void btnGroupVideoChat_Cancel_Click(object sender, EventArgs e)
        {
            iCancelType = 4;        // 1 - Standalone Request, 2 - Standalone VideoMode, 3 - Group Requests, 4 - Group Requests VideoChat Mode
            chkDeleteUser.Text = iClientStatus == 1 ? "Block του πελάτη" : "Διαγραφή του πελάτη";
            if (txtWarning.Text.Length == 0) btnOK_Warning.Enabled = false;
            else btnOK_Warning.Enabled = true;
            panWarning.Visible = true;
            txtWarning.Focus();
        }
        private void btnGroupVideoChat_Confirm_Click(object sender, EventArgs e)
        {
            iVideoChatStatus = 2;
            for (m = 1; m <= fgList.Rows.Count - 1; m++)
            {
                iRequest_ID = Convert.ToInt32(fgList[m, "ID"]);
                ClientsRequests = new clsClientsRequests();
                ClientsRequests.Record_ID = iRequest_ID;
                ClientsRequests.GetRecord();
                ClientsRequests.Status = 5;                                             // 5 – сделана первая проверка результатов видеочата
                ClientsRequests.VideoChatStatus = 2;                                    // 2 - видеочат проведен и сделана первая проверка
                ClientsRequests.VideoChatFile = lblGroupVideoChatFile.Text;
                ClientsRequests.EditRecord();
            }
            this.Close();
        }
        private void btnGroupVideoChat_OK_Click(object sender, EventArgs e)
        {
            Finish_GroupRequests();
            this.Close();
        }
        private void picShowGroupVideoChatFile_Click(object sender, EventArgs e)
        {
            if (lblGroupVideoChatFile.Text != "") Global.DMS_ShowFile("Customers/" + lblClientName.Text.Replace(".", "_"), lblGroupVideoChatFile.Text);
        }
        private string UploadVideoChatFile()
        {
            sTemp = Global.FileChoice(Global.DefaultFolder);
            if (sTemp.Length > 0)
            {
                sNewFileName = Path.GetFileNameWithoutExtension(sTemp) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(sTemp);

                if (Path.GetDirectoryName(sTemp) != Global.DMSTransferPoint)
                {   // Source file isn't in DMS TransferPoint folder, so ...
                    if (File.Exists(Global.DMSTransferPoint + "/" + sNewFileName))
                        sNewFileName = Path.GetFileNameWithoutExtension(sNewFileName) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(sNewFileName);
                    File.Copy(sTemp, Global.DMSTransferPoint + "/" + sNewFileName);         // ... copy this file into DMS TransferPoint folder
                }

                clsServerJobs ServerJobs = new clsServerJobs();
                ServerJobs.JobType_ID = 19;
                ServerJobs.Source_ID = 0;
                ServerJobs.Parameters = "{'source_file_full_name': '" + sTemp.Replace(@"\", "/") + "', 'file_name': '" + sNewFileName + "', 'file_type': '0', " +
                                        "'target_folder': 'Customers/" + lblClientName.Text.Replace(".", "_") + "/', 'client_id': '" + iClient_ID + "', 'status' : '1'}";

                ServerJobs.DateStart = DateTime.Now;
                ServerJobs.DateFinish = Convert.ToDateTime("1900/01/01");
                ServerJobs.PubKey = "";
                ServerJobs.PrvKey = "";
                ServerJobs.Attempt = 0;
                ServerJobs.Status = 0;
                ServerJobs.InsertRecord();
            }

            return sNewFileName;
        }
        #endregion
        #region --- GroupRequests functionality -----------------------------------------------------------------
        private void Finish_GroupRequests()
        {
            string sGroupVideoChatFile = lblGroupVideoChatFile.Text;

            for (m = 1; m < fgList.Rows.Count; m++)
            {
                iRequestTipos = Convert.ToInt16(fgList[m, "RequestTipos"]);
                iRequest_ID = Convert.ToInt32(fgList[m, "ID"]);
                ClientsRequests = new clsClientsRequests();
                ClientsRequests.Record_ID = iRequest_ID;
                ClientsRequests.GetRecord();
                iClient_ID = ClientsRequests.Client_ID;
                iRequestTipos = ClientsRequests.Tipos;
                iAction = ClientsRequests.Action;
                sDescription = ClientsRequests.Description;
                txtWarning.Text = ClientsRequests.Warning;
                dDateIns = ClientsRequests.DateIns;
                iStatus = ClientsRequests.Status;
                lblEmail.Text = ClientsRequests.Email;
                sAuthor_EMail = ClientsRequests.Author_EMail;
                ShowRequestWindow();
                grpL3.Visible = false;
                SaveRequestDataIntoDB();

                ClientsRequests = new clsClientsRequests();
                ClientsRequests.Record_ID = Convert.ToInt32(fgList[m, "ID"]);
                ClientsRequests.GetRecord();
                ClientsRequests.DateClose = DateTime.Now;
                ClientsRequests.Status = 6;                                             // 6 - сделана вторая проверка результатов видеочата
                ClientsRequests.VideoChatStatus = 3;                                    // 3 - видеочат проведен, сделана первая проверка, а значит он успешно завершен
                ClientsRequests.VideoChatFile = sGroupVideoChatFile;
                ClientsRequests.EditRecord();
            }

            if (lblGroupVideoChatFile.Text.Trim() != "")
            {
                lblGroupVideoChatFile_ID.Text = "0";

                allClientDocFiles = new clsClientsDocFiles();
                allClientDocFiles.Client_ID = iClient_ID;
                allClientDocFiles.PreContract_ID = 0;
                allClientDocFiles.Contract_ID = 0;
                allClientDocFiles.DocTypes = 0;
                allClientDocFiles.GetList();

                foreach (DataRow dtRow in allClientDocFiles.List.Rows)
                {
                    if ((dtRow["FileName"] + "").Trim() == lblGroupVideoChatFile.Text.Trim() && Convert.ToInt32(dtRow["Status"]) > 0)
                    {
                        lblGroupVideoChatFile_ID.Text = dtRow["ID"] + "";
                        break;
                    }
                }

                ClientDocFiles = new clsClientsDocFiles();
                ClientDocFiles.Record_ID = Convert.ToInt32(lblGroupVideoChatFile_ID.Text);
                ClientDocFiles.GetRecord();
                ClientDocFiles.Status = 2;                                                 // 2 - document confirmed
                ClientDocFiles.EditStatus();
            }

            WebUsersStates = new clsWebUsersStates();
            WebUsersStates.Client_ID = iClient_ID;
            WebUsersStates.Status = 100;
            WebUsersStates.EditStatus();

            sGroup_ID = "";
        }
        private void btnDelete_Group_Click(object sender, EventArgs e)
        {
            DeleteRequest();
            this.Close();
        }
        private void btnFinish_Group_Click(object sender, EventArgs e)
        {
            int iState = 2;                                           // iState = 1 - RequestsGroup is't confirmed yet, iState = 2 - RequestsGroup confirmed, iState = 3 - RequestsGroup has cancelled requests

            if (iVideoChatStatus == 0)
            {
                for (i = 1; i <= fgList.Rows.Count - 1; i++)
                {
                    if (Convert.ToInt16(fgList[i, "Status"]) < 4)
                    {
                        iState = 1;
                        break;
                    }
                }
            }
            else
            {
                for (i = 1; i <= fgList.Rows.Count - 1; i++)
                {
                    if (Convert.ToInt16(fgList[i, "Status"]) < 4)
                    {
                        iState = 1;
                        break;
                    }
                    if (Convert.ToInt16(fgList[i, "Status"]) == 7)
                    {
                        iState = 3;
                        break;
                    }
                }
            }

            switch (iState)
            {
                case 1:
                    MessageBox.Show("Επεξεργασία του Group αιτημάτων δεν ολοκληρώθηκε", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                case 2:
                    if (iVideoChatStatus > 0)
                    {
                        MessageBox.Show("Σας υπενθυμίζουμε ότι θα χρειαστεί να γίνει Video Κλήση για να ταυτοποιηθεί ο χρήστης", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.Close();
                    }
                    else Finish_GroupRequests();
                    this.Close();
                    break;
                case 3:
                    for (i = 1; i <= fgList.Rows.Count - 1; i++)
                    {
                        if (Convert.ToInt16(fgList[i, "Status"]) == 7)
                            CancelSingleRequest("", Convert.ToInt32(fgList[i, "ID"]), fgList[i, "Warning"] + "", lblGroupVideoChatFile.Text);
                    }
                    this.Close();
                    break;
            }
        }
        #endregion

        #region --- Show functions ------------------------------------------------------------------------------
        private int DefineMessage(int iRequestTipos, ref Label lbl_value, ref Label lbl_temp)
        {
            int iLocStatus = 0;

            foreach (DataRow dtRow in ClientsRequests.List.Rows)
            {
                if (Convert.ToInt32(dtRow["RequestTipos"]) == iRequestTipos && Convert.ToInt32(dtRow["Status"]) != 0)
                {
                    iLocStatus = Convert.ToInt32(dtRow["Status"]);             // iLocStatus - status of last ClientsRequests of current client (iClinet_ID) or current RequestType
                    if (iLocStatus == 1) { lbl_temp.Text = "Πρόχειρο αίτημα"; lbl_temp.ForeColor = Color.Blue; }
                    else if (iLocStatus == 2) { lbl_temp.Text = "Αίτημα προς έλεγχο"; lbl_temp.ForeColor = Color.Blue; }
                    else if (lbl_value.Text.Trim().Length == 0) { iLocStatus = 0; }
                    else { lbl_temp.Text = ""; lbl_temp.ForeColor = Color.Black; iLocStatus = 0; }
                }
            }

            if (iLocStatus == 0)
            {
                if (lbl_value.Text.Trim().Length == 0) { lbl_temp.Text = "Εκκρεμεί"; lbl_temp.ForeColor = Color.Red; }
                else { lbl_temp.Text = ""; lbl_temp.ForeColor = Color.Black; }
            }

            return iLocStatus;
        }
        private void ShowClientData()
        {
            ClientsRequests = new clsClientsRequests();
            ClientsRequests.DateFrom = DateTime.Now.AddDays(-15);       // check only requests at last 2 weeks (15 days), not older
            ClientsRequests.DateTo = DateTime.Now;
            ClientsRequests.User_ID = 0;
            ClientsRequests.Client_ID = iClient_ID;
            ClientsRequests.GetList();

            lblClientName.Text = Clients.Fullname;
            lblADT.Text = Clients.ADT;
            sPolice = Clients.Police;
            sExpireDate = Clients.ExpireDate;
            iADTStatus = DefineMessage(1, ref lblADT, ref lblADT_temp);

            lblPassport.Text = Clients.Passport;
            sPassport_Police = Clients.Passport_Police;
            sPassport_ExpireDate = Clients.Passport_ExpireDate;
            iPassportStatus = DefineMessage(14, ref lblPassport, ref lblPassport_temp);

            lblMobile.Text = Clients.Mobile;
            DefineMessage(2, ref lblMobile, ref lblMobile_temp);

            lblTel.Text = Clients.Tel;
            DefineMessage(3, ref lblTel, ref lblTel_temp);

            lblEmail.Text = Clients.EMail;
            DefineMessage(4, ref lblEmail, ref lblEmail_temp);

            lblAddress.Text = Clients.Address;
            sCity = Clients.City;
            sZip = Clients.Zip;
            iCountry_ID = Clients.Country_ID;
            sCountry = "";
            foundRows = Global.dtCountries.Select("ID=" + iCountry_ID);
            if (foundRows.Length > 0) sCountry = foundRows[0]["Title"] + "";
            DefineMessage(5, ref lblAddress, ref lblAddress_temp);

            lblAFM.Text = Clients.AFM;
            DefineMessage(6, ref lblAFM, ref lblAFM_temp);

            iCountryTaxes_ID = Clients.CountryTaxes_ID;
            sCountryTaxes = "";
            if (iCountryTaxes_ID > 0)
            {
                foundRows = Global.dtCountries.Select("ID=" + iCountryTaxes_ID);
                if (foundRows.Length > 0) sCountryTaxes = foundRows[0]["Title"] + "";
            }
            lblCountryTaxes.Text = sCountryTaxes;
            DefineMessage(9, ref lblCountryTaxes, ref lblCountryTaxes_temp);

            //-------------------------------------------------------------
            i = 0;

            clsOptions Options = new clsOptions();
            Options.GetRecord();
            iTaxDeclarations1Year = Options.TaxDeclarations1Year;
            iTaxDeclarationsLastYear = Options.TaxDeclarationsLastYear;

            clsClientsDocFiles klsClientDocFiles = new clsClientsDocFiles();
            klsClientDocFiles.Client_ID = iClient_ID;
            klsClientDocFiles.PreContract_ID = 0;
            klsClientDocFiles.Contract_ID = 0;
            klsClientDocFiles.DocTypes = 3924;                          // 3924 - Ekkatharistika
            klsClientDocFiles.GetList();
            foreach (DataRow dtRow in klsClientDocFiles.List.Rows)
            {
                sTemp = (dtRow["FileName"] + "").Trim();
                jStatus = Convert.ToInt32(dtRow["Status"]);
                if (sTemp.Length > 0)
                {
                    j = sTemp.IndexOf("ΕΚΚΑΘΑΡΙΣΤΙΚΟ ");
                    if (j >= 0)
                    {
                        j = Convert.ToInt32(sTemp.Substring(14, 4));
                        if (j >= iTaxDeclarations1Year && j <= iTaxDeclarationsLastYear && jStatus == 2) i = i + 1;
                    }
                }
            }

            lblEkkCount.Text = i.ToString();

            DefineMessage(7, ref lblEkkataristika_temp, ref lblEkkataristika_temp);
            if (i >= (iTaxDeclarationsLastYear - iTaxDeclarations1Year + 1)) lblEkkataristika_temp.Text = "";
            //---------------------------------------------

            lblSpec.Text = Clients.Brunches_Title;
            DefineMessage(8, ref lblSpec, ref lblSpec_temp);

            lblW8BEN.Text = "Όχι";
            lblW8BEN_temp.Text = "";            // temporary ?
            lblW8BEN_temp.Visible = false;      // temporary ?

            lblSpecialCategory.Text = Clients.SpecialCategory + "";

            DefineMessage(10, ref lblSpecialCategory, ref lblSpecialCategory_temp);

            lblMerida.Text = Clients.Merida;
            DefineMessage(15, ref lblMerida, ref lblMerida_temp);

            lblLogAxion.Text = Clients.LogAxion;
            DefineMessage(16, ref lblLogAxion, ref lblLogAxion_temp);

            lblAMKA.Text = Clients.AMKA;
            DefineMessage(17, ref lblAMKA, ref lblAMKA_temp);

            fgBankAccounts.Redraw = false;
            fgBankAccounts.Rows.Count = 1;
            Clients_BankAccounts = new clsClients_BankAccounts();
            Clients_BankAccounts.Client_ID = iClient_ID;
            Clients_BankAccounts.GetList();
            foreach (DataRow dtRow in Clients_BankAccounts.List.Rows)
                if (Convert.ToInt32(dtRow["Status"]) == 1)
                    fgBankAccounts.AddItem(dtRow["AccNumber"] + "\t" + dtRow["BankTitle"] + "\t" + dtRow["ID"]);
            fgBankAccounts.Redraw = true;
            DefineMessage(11, ref lblBankAccount_temp, ref lblBankAccount_temp);
            if (lblBankAccount_temp.Text == "Εκκρεμεί") lblBankAccount_temp.Text = "";

            fgCoOwners.Redraw = false;
            fgCoOwners.Rows.Count = 1;
            Clients_Clients = new clsClients_Clients();
            Clients_Clients.Client_ID = iClient_ID;
            Clients_Clients.GetList();
            foreach (DataRow dtRow in Clients_Clients.List.Rows)
                if (Convert.ToInt32(dtRow["Status"]) == 1)
                    if (Convert.ToInt32(dtRow["Client_ID"]) != iClient_ID) fgCoOwners.AddItem(dtRow["Client_Fullname"] + "\t" + dtRow["ID"]);
                    else if (Convert.ToInt32(dtRow["Client2_ID"]) != iClient_ID) fgCoOwners.AddItem(dtRow["Client2_Fullname"] + "\t" + dtRow["ID"]);
            fgCoOwners.Redraw = true;
        }
        private void ShowRequestWindow()
        {
            grpL2.Visible = false;

            ClientsRequests_Types = new clsClientsRequests_Types();
            ClientsRequests_Types.Record_ID = iRequestTipos;
            ClientsRequests_Types.GetRecord();

            switch (iRequestTipos)
            {
                case 1:
                    ucADT.Description = sDescription;
                    ucADT.lblOldNumber.Text = lblADT.Text;
                    ucADT.lblOldPolice.Text = sPolice;
                    ucADT.lblOldExpireDate.Text = sExpireDate;
                    ucADT.StartInit(iStatus, lblClientName.Text, iClient_ID, ClientsRequests_Types.DocType1_ID, ClientsRequests_Types.DocType2_ID);
                    switch (iAction)
                    {
                        case 0:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_0;
                            lblADT_temp.Text = "Εκκρεμεί";
                            break;
                        case 1:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_1;
                            lblADT_temp.Text = "";
                            break;
                    }
                    lblNum_Request.Text = (iRequest_ID == 0 ? "" : dDateIns.ToString("dd/MM/yyyy") + " ( N " + iRequest_ID + " )");
                    ucADT.Visible = true;
                    grpL3.BackColor = panADT.BackColor;
                    break;
                case 2:
                    ucMobile.Description = sDescription;
                    ucMobile.lblOldNumber.Text = lblMobile.Text;
                    ucMobile.StartInit(iStatus, lblClientName.Text, iClient_ID, ClientsRequests_Types.DocType1_ID, ClientsRequests_Types.DocType2_ID);
                    switch (iAction)
                    {
                        case 0:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_0;
                            lblMobile_temp.Visible = true;
                            break;
                        case 1:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_1;
                            lblMobile_temp.Visible = true;
                            break;
                    }
                    lblNum_Request.Text = (iRequest_ID == 0 ? "" : dDateIns.ToString("dd/MM/yyyy") + " ( N " + iRequest_ID + " )");
                    ucMobile.Visible = true;
                    grpL3.BackColor = panConnectionsData.BackColor;
                    break;
                case 3:
                    ucTel.Description = sDescription;
                    ucTel.lblOldNumber.Text = lblTel.Text;
                    ucTel.StartInit(iStatus, lblClientName.Text, iClient_ID, ClientsRequests_Types.DocType1_ID, ClientsRequests_Types.DocType2_ID);
                    switch (iAction)
                    {
                        case 0:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_0;
                            lblNum_Request.Text = "";
                            lblTel_temp.Visible = true;
                            break;
                        case 1:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_1;
                            lblTel_temp.Visible = true;
                            break;
                    }
                    lblNum_Request.Text = (iRequest_ID == 0 ? "" : dDateIns.ToString("dd/MM/yyyy") + " ( N " + iRequest_ID + " )");
                    ucTel.Visible = true;
                    grpL3.BackColor = panConnectionsData.BackColor;
                    break;
                case 4:
                    ucEmail.Description = sDescription;
                    ucEmail.StartInit(iStatus, lblClientName.Text, iClient_ID, ClientsRequests_Types.DocType1_ID, ClientsRequests_Types.DocType2_ID);
                    switch (iAction)
                    {
                        case 0:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_0;
                            lblEmail_temp.Visible = true;
                            break;
                        case 1:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_1;
                            lblEmail_temp.Visible = true;
                            break;
                    }
                    lblNum_Request.Text = (iRequest_ID == 0 ? "" : dDateIns.ToString("dd/MM/yyyy") + " ( N " + iRequest_ID + " )");
                    ucEmail.Visible = true;
                    grpL3.BackColor = panConnectionsData.BackColor;
                    break;
                case 5:
                    ucAddress.Description = sDescription;
                    ucAddress.lblOldAddress.Text = lblAddress.Text;
                    ucAddress.lblOldZip.Text = sZip;
                    ucAddress.lblOldCity.Text = sCity;
                    ucAddress.lblOldCountry.Text = sCountry;
                    ucAddress.StartInit(iStatus, lblClientName.Text, iClient_ID, ClientsRequests_Types.DocType1_ID, ClientsRequests_Types.DocType2_ID);
                    switch (iAction)
                    {
                        case 0:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_0;
                            lblAddress_temp.Visible = true;
                            break;
                        case 1:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_1;
                            lblAddress_temp.Visible = true;
                            break;
                    }
                    lblNum_Request.Text = (iRequest_ID == 0 ? "" : dDateIns.ToString("dd/MM/yyyy") + " ( N " + iRequest_ID + " )");
                    ucAddress.Visible = true;
                    grpL3.BackColor = panConnectionsData.BackColor;
                    break;
                case 6:
                    ucAFM.Description = sDescription;
                    ucAFM.lblOldAFM.Text = lblAFM.Text;
                    ucAFM.StartInit(iStatus, lblClientName.Text, iClient_ID, ClientsRequests_Types.DocType1_ID, ClientsRequests_Types.DocType2_ID);
                    switch (iAction)
                    {
                        case 0:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_0;
                            lblAFM_temp.Visible = true;
                            break;
                        case 1:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_1;
                            lblAFM_temp.Visible = true;
                            break;
                    }
                    lblNum_Request.Text = (iRequest_ID == 0 ? "" : dDateIns.ToString("dd/MM/yyyy") + " ( N " + iRequest_ID + " )");
                    ucAFM.Visible = true;
                    grpL3.BackColor = panEconomicData.BackColor;
                    break;
                case 7:
                    ucEkkatharistika.Description = sDescription;
                    ucEkkatharistika.chkDenExo.Checked = false;
                    ucEkkatharistika.StartInit(iStatus, lblClientName.Text, iClient_ID, ClientsRequests_Types.DocType1_ID, ClientsRequests_Types.DocType2_ID);
                    switch (iAction)
                    {
                        case 0:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_0;
                            lblEkkataristika_temp.Visible = true;
                            break;
                        case 1:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_1;
                            lblEkkataristika_temp.Visible = true;
                            break;
                    }
                    lblNum_Request.Text = (iRequest_ID == 0 ? "" : dDateIns.ToString("dd/MM/yyyy") + " ( N " + iRequest_ID + " )");
                    ucEkkatharistika.Visible = true;
                    grpL3.BackColor = panEconomicData.BackColor;
                    break;
                case 8:
                    ucSpecial.Description = sDescription;
                    ucSpecial.lblOldSpec.Text = sSpec;
                    ucSpecial.StartInit(iStatus, lblClientName.Text, iClient_ID, ClientsRequests_Types.DocType1_ID, ClientsRequests_Types.DocType2_ID);
                    switch (iAction)
                    {
                        case 0:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_0;
                            lblSpec_temp.Visible = true;
                            break;
                        case 1:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_1;
                            lblSpec_temp.Visible = true;
                            break;
                    }
                    lblNum_Request.Text = (iRequest_ID == 0 ? "" : dDateIns.ToString("dd/MM/yyyy") + " ( N " + iRequest_ID + " )");
                    ucSpecial.Visible = true;
                    grpL3.BackColor = panSpecialData.BackColor;
                    break;
                case 9:
                    ucCountryTaxes.Description = sDescription;
                    ucCountryTaxes.lblOldCountry.Text = sCountryTaxes;
                    ucCountryTaxes.StartInit(iStatus, lblClientName.Text, iClient_ID, ClientsRequests_Types.DocType1_ID, ClientsRequests_Types.DocType2_ID);
                    switch (iAction)
                    {
                        case 0:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_0;
                            lblCountryTaxes_temp.Visible = true;
                            break;
                        case 1:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_1;
                            lblCountryTaxes_temp.Visible = true;
                            break;
                    }
                    lblNum_Request.Text = (iRequest_ID == 0 ? "" : dDateIns.ToString("dd/MM/yyyy") + " ( N " + iRequest_ID + " )");
                    ucCountryTaxes.Visible = true;
                    grpL3.BackColor = panEconomicData.BackColor;
                    break;
                case 10:
                    ucSpecCateg.Description = sDescription;
                    ucSpecCateg.CategoriesList = lblData1.Text;
                    ucSpecCateg.StartInit(iStatus, lblClientName.Text, iClient_ID, ClientsRequests_Types.DocType1_ID, ClientsRequests_Types.DocType2_ID);

                    switch (iAction)
                    {
                        case 0:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_0;
                            lblSpecialCategory_temp.Visible = true;
                            break;
                        case 1:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_1;
                            lblSpecialCategory_temp.Visible = true;
                            break;
                    }
                    lblNum_Request.Text = (iRequest_ID == 0 ? "" : dDateIns.ToString("dd/MM/yyyy") + " ( N " + iRequest_ID + " )");
                    ucSpecCateg.Visible = true;
                    grpL3.BackColor = panMiscData.BackColor;
                    break;
                case 11:
                    switch (iAction)
                    {
                        case 0:
                            ucBankAccount.Description = sDescription;
                            ucBankAccount.StartInit(iStatus, lblClientName.Text, iClient_ID, ClientsRequests_Types.DocType1_ID, ClientsRequests_Types.DocType2_ID);

                            lblNum_Request.Text = (iRequest_ID == 0 ? "" : dDateIns.ToString("dd/MM/yyyy") + " ( N " + iRequest_ID + " )");
                            grpL3.BackColor = panMiscData.BackColor;
                            lblTitle_Request.Text = ClientsRequests_Types.Title_0;
                            ucBankAccount.Visible = true;
                            ucBankAccount.Refresh();
                            ucBankAccount.Visible = true;
                            break;
                        case 2:
                            if (fgBankAccounts.Row > 0) m = Convert.ToInt32(fgBankAccounts[fgBankAccounts.Row, "ID"]);
                            else m = 0;
                            ucBankAccount_Delete.Description = sDescription;
                            ucBankAccount_Delete.StartInit(iStatus, lblClientName.Text, iClient_ID, m, ClientsRequests_Types.DocType1_ID);
                            lblNum_Request.Text = (iRequest_ID == 0 ? "" : dDateIns.ToString("dd/MM/yyyy") + " ( N " + iRequest_ID + " )");
                            grpL3.BackColor = panMiscData.BackColor;
                            lblTitle_Request.Text = ClientsRequests_Types.Title_2;
                            ucBankAccount_Delete.Visible = true;
                            ucBankAccount_Delete.Refresh();
                            ucBankAccount_Delete.Visible = true;
                            break;
                    }
                    break;
                case 12:
                    if (sDescription.Length > 0)
                    {
                        if (sDescription.IndexOf("'afm'") > 0) { rbAdult.Checked = true; rbChild.Checked = false; }
                        else { rbAdult.Checked = false; rbChild.Checked = true; }
                    }
                    else { rbAdult.Checked = true; rbChild.Checked = false; }

                    if (rbAdult.Checked)
                    {
                        switch (iAction)
                        {
                            case 0:
                                ucCoOwner.Description = sDescription;
                                ucCoOwner.StartInit(iStatus, lblClientName.Text, iClient_ID, ClientsRequests_Types.DocType1_ID, ClientsRequests_Types.DocType2_ID);

                                lblNum_Request.Text = (iRequest_ID == 0 ? "" : dDateIns.ToString("dd/MM/yyyy") + " ( N " + iRequest_ID + " )");
                                grpL3.BackColor = panMiscData.BackColor;

                                lblTitle_Request.Text = ClientsRequests_Types.Title_0;
                                ucCoOwner.Visible = true;
                                ucCoOwner.Refresh();
                                ucCoOwner.Visible = true;
                                break;
                            case 2:
                                ucCoOwner_Delete.Description = sDescription;
                                ucCoOwner_Delete.StartInit(iStatus, Convert.ToInt32(fgCoOwners[fgCoOwners.Row, "ID"]), ClientsRequests_Types.DocType1_ID, ClientsRequests_Types.DocType2_ID);
                                lblNum_Request.Text = (iRequest_ID == 0 ? "" : dDateIns.ToString("dd/MM/yyyy") + " ( N " + iRequest_ID + " )");
                                grpL3.BackColor = panMiscData.BackColor;
                                lblTitle_Request.Text = ClientsRequests_Types.Title_2;
                                ucCoOwner_Delete.Visible = true;
                                ucCoOwner_Delete.Refresh();
                                ucCoOwner_Delete.Visible = true;
                                break;
                        }
                    }
                    if (rbChild.Checked)
                    {
                        switch (iAction)
                        {
                            case 0:
                                ucCowner_Child.Description = sDescription;
                                ucCowner_Child.StartInit(iStatus, lblClientName.Text, iClient_ID, ClientsRequests_Types.DocType1_ID, ClientsRequests_Types.DocType2_ID);

                                lblNum_Request.Text = (iRequest_ID == 0 ? "" : dDateIns.ToString("dd/MM/yyyy") + " ( N " + iRequest_ID + " )");
                                grpL3.BackColor = panMiscData.BackColor;

                                lblTitle_Request.Text = ClientsRequests_Types.Title_0;
                                ucCowner_Child.Visible = true;
                                ucCowner_Child.Refresh();
                                ucCowner_Child.Visible = true;
                                break;
                            case 2:
                                ucCowner_Delete.Description = sDescription;
                                ucCowner_Delete.StartInit(iStatus, Convert.ToInt32(fgCoOwners[fgCoOwners.Row, "ID"]), ClientsRequests_Types.DocType1_ID, ClientsRequests_Types.DocType2_ID);
                                lblNum_Request.Text = (iRequest_ID == 0 ? "" : dDateIns.ToString("dd/MM/yyyy") + " ( N " + iRequest_ID + " )");
                                grpL3.BackColor = panMiscData.BackColor;
                                lblTitle_Request.Text = ClientsRequests_Types.Title_2;
                                ucCowner_Delete.Visible = true;
                                ucCowner_Delete.Refresh();
                                ucCowner_Delete.Visible = true;
                                break;
                        }
                    }
                    break;
                case 13:
                    ucW8BEN.Description = sDescription;
                    ucW8BEN.lblOldW8BEN.Text = sW8BEN;
                    ucW8BEN.StartInit(iStatus, lblClientName.Text);
                    switch (iAction)
                    {
                        case 0:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_0;
                            lblW8BEN_temp.Visible = true;
                            break;
                        case 1:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_1;
                            lblW8BEN_temp.Visible = true;
                            break;
                    }
                    ucW8BEN.Visible = true;
                    grpL3.BackColor = panMiscData.BackColor;
                    break;
                case 14:
                    ucPasport.Description = sDescription;
                    ucPasport.lblOldNumber.Text = lblPassport.Text;
                    ucPasport.lblOldPolice.Text = sPassport_Police;
                    ucPasport.lblOldExpireDate.Text = sPassport_ExpireDate;
                    ucPasport.StartInit(iStatus, lblClientName.Text, iClient_ID, ClientsRequests_Types.DocType1_ID, ClientsRequests_Types.DocType2_ID);
                    switch (iAction)
                    {
                        case 0:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_0;
                            lblPassport_temp.Visible = true;
                            break;
                        case 1:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_1;
                            lblPassport_temp.Visible = true;
                            break;
                    }
                    lblNum_Request.Text = (iRequest_ID == 0 ? "" : dDateIns.ToString("dd/MM/yyyy") + " ( N " + iRequest_ID + " )");
                    ucPasport.Visible = true;
                    grpL3.BackColor = panADT.BackColor;
                    break;
                case 15:
                    ucMerida.Description = sDescription;
                    ucMerida.lblOldMerida.Text = lblMerida.Text;
                    ucMerida.StartInit(iStatus, lblClientName.Text, iClient_ID, ClientsRequests_Types.DocType1_ID, ClientsRequests_Types.DocType2_ID);

                    switch (iAction)
                    {
                        case 0:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_0;
                            lblMerida_temp.Visible = true;
                            break;
                        case 1:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_1;
                            lblMerida_temp.Visible = true;
                            break;
                    }
                    lblNum_Request.Text = (iRequest_ID == 0 ? "" : dDateIns.ToString("dd/MM/yyyy") + " ( N " + iRequest_ID + " )");
                    ucMerida.Visible = true;
                    grpL3.BackColor = panEconomicData.BackColor;
                    break;
                case 16:
                    ucLogAxion.Description = sDescription;
                    ucLogAxion.lblOldLogAxion.Text = lblLogAxion.Text;
                    ucLogAxion.StartInit(iStatus, lblClientName.Text, iClient_ID, ClientsRequests_Types.DocType1_ID, ClientsRequests_Types.DocType2_ID);

                    switch (iAction)
                    {
                        case 0:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_0;
                            lblLogAxion_temp.Visible = true;
                            break;
                        case 1:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_1;
                            lblLogAxion_temp.Visible = true;
                            break;
                    }
                    lblNum_Request.Text = (iRequest_ID == 0 ? "" : dDateIns.ToString("dd/MM/yyyy") + " ( N " + iRequest_ID + " )");
                    ucLogAxion.Visible = true;
                    grpL3.BackColor = panEconomicData.BackColor;
                    break;
                case 17:
                    ucAMKA.Description = sDescription;
                    ucAMKA.lblOldAMKA.Text = lblAMKA.Text;
                    ucAMKA.StartInit(iStatus, lblClientName.Text, iClient_ID, ClientsRequests_Types.DocType1_ID, ClientsRequests_Types.DocType2_ID);

                    switch (iAction)
                    {
                        case 0:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_0;
                            lblAMKA_temp.Visible = true;
                            break;
                        case 1:
                            lblTitle_Request.Text = ClientsRequests_Types.Title_1;
                            lblAMKA_temp.Visible = true;
                            break;
                    }
                    lblNum_Request.Text = (iRequest_ID == 0 ? "" : dDateIns.ToString("dd/MM/yyyy") + " ( N " + iRequest_ID + " )");
                    ucAMKA.Visible = true;
                    grpL3.BackColor = panEconomicData.BackColor;
                    break;
            }

            if (iStatus == 4 || iStatus == 6 || iStatus == 7)                           // iStatus == 4 || iStatus == 6 || iStatus == 7 means that this request was Agreed&Closed or Disagreed, so all buttons must be off
            {
                btnDelete_Request.Visible = false;
                btnSave_Temp.Visible = false;
                btnSend.Visible = false;
                btnCancel.Visible = false;
                btnDelete.Visible = false;
                btnConfirm.Visible = false;
                btnOK.Visible = false;
                grpFooter1.Visible = false;
                grpFooter2.Visible = false;
                if (iStatus == 7)
                {
                    lblWarning.Text = txtWarning.Text;
                    grpWarning.Visible = true;
                }
                else grpWarning.Visible = false;
            }
            else                                                        // in other cases i.e. iStatus != 6 and iStatus != 7 ...
            {
                if (Global.ClientsRequests_Status == 2 || Global.ClientsRequests_Status == 3)     // if user is BackOffice User (Global.ClientsRequests_Status == 2,3 ) edit buttons must be off, and agreement buttons must be on
                {
                    btnDelete_Request.Visible = false;
                    btnSave_Temp.Visible = false;
                    btnSend.Visible = false;
                    btnCancel.Visible = true;
                    btnDelete.Visible = true;
                    btnConfirm.Visible = true;
                    btnOK.Visible = true;
                    grpFooter1.Visible = false;
                    grpFooter2.Visible = true;
                }
                else                                     // if user is not SuperUser (Global.ClientsRequests_Status != 2,3) edit buttons must be on, and agreement buttons must be off
                {
                    btnDelete_Request.Visible = true;
                    btnSave_Temp.Visible = true;
                    btnSend.Visible = true;
                    btnCancel.Visible = false;
                    btnDelete.Visible = false;
                    btnConfirm.Visible = false;
                    btnOK.Visible = false;
                    grpFooter1.Visible = true;
                    grpFooter2.Visible = false;
                }
            }

            if (sGroup_ID == "" && (iStatus == 4 || iStatus == 5 || iStatus == 6 || iStatus == 7))
            {
                switch (iVideoChatStatus)
                {
                    case 1:
                        lblVideoChatStatus.Text = "Video Κλήση είναι σε Αναμονή";
                        btnVideoChat_Confirm.Visible = true;
                        btnVideoChat_OK.Visible = true;
                        btnVideoChat_Cancel.Visible = true;
                        break;
                    case 2:
                        lblVideoChatStatus.Text = "Έγινε έλεγχος της Video Κλήσης";
                        btnVideoChat_Confirm.Visible = true;
                        btnVideoChat_OK.Visible = true;
                        btnVideoChat_Cancel.Visible = true;
                        break;
                    case 3:
                        lblVideoChatStatus.Text = "Video Κλήση Oριστικοποιήθηκε";
                        btnVideoChat_Confirm.Visible = false;
                        btnVideoChat_OK.Visible = false;
                        btnVideoChat_Cancel.Visible = false;
                        break;
                    case 4:
                        lblVideoChatStatus.Text = "Video Κλήση Απορρίφθηκε";
                        btnVideoChat_Confirm.Visible = false;
                        btnVideoChat_OK.Visible = false;
                        btnVideoChat_Cancel.Visible = false;
                        break;
                }

                if (lblVideoChatFile.Text != "") picShowVideoChatFile.Visible = true;
                else picShowVideoChatFile.Visible = false;

                if (iVideoChatStatus != 0) grpVideoChat.Visible = true;
            }
            else
                grpVideoChat.Visible = false;
        }
        #endregion --------------------------------------------------------------------------------
        protected void ucCS_TextChanged(object sender, EventArgs e)
        {
            iClient_ID = Convert.ToInt32(ucCS.Client_ID.Text);
            Clients = new clsClients();
            Clients.Record_ID = iClient_ID;
            Clients.EMail = "";
            Clients.Mobile = "";
            Clients.AFM = "";
            Clients.DoB = Convert.ToDateTime("1900/01/01");
            Clients.GetRecord();
            iClientStatus = Clients.Status;
            ShowClientData();
            panL1.Visible = true;
        }
        private void btn5Days_Click(object sender, EventArgs e)
        {
            ServerJobs = new clsServerJobs();
            ServerJobs.JobType_ID = 46;
            ServerJobs.Source_ID = 0;
            ServerJobs.Parameters = "{'recipient_email': '" + lblEmail.Text + "', 'request_action' : '23'}";
            ServerJobs.DateStart = DateTime.Now;
            ServerJobs.DateFinish = System.Convert.ToDateTime("1900/01/01");
            ServerJobs.PubKey = "";
            ServerJobs.PrvKey = "";
            ServerJobs.Attempt = 0;
            ServerJobs.Status = 0;
            ServerJobs.InsertRecord();
        }
        private void btn10Days_Click(object sender, EventArgs e)
        {
            DeleteRequest();
            this.Close();
        }
        public int Request_ID { get { return this.iRequest_ID; } set { this.iRequest_ID = value; } }
        public int Client_ID { get { return this.iClient_ID; } set { this.iClient_ID = value; } }
        public int RequestTipos { get { return this.iRequestTipos; } set { this.iRequestTipos = value; } }
        public DateTime DateIns { get { return this.dDateIns; } set { this.dDateIns = value; } }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public int VideoChatStatus { get { return this.iVideoChatStatus; } set { this.iVideoChatStatus = value; } }
        public string Group_ID { get { return this.sGroup_ID; } set { this.sGroup_ID = value; } }
    }
}
