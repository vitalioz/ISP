using C1.Win.C1FlexGrid;
using Core;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Contracts
{
    public partial class frmClientsList : Form
    {
        DataRow dtRow;
        DataView dtView;
        DataRow[] foundRows;
        int i, iID, ID, iClient_ID, iOldClient_ID, iOldStatus, iOldTipos, iMode, iDocFiles_ID, iAction, iFormat, iRightsLevel, jAktion = 0, iNewID = 0, iOldRow = 0;
        string sTemp, s1, s2, sTemp1, sMeTitle, sSurnameGreek, sSurnameEnglish, sOldValues, sExtra, sClientFullName, sOldClientFullName, sSurname, sFirstname, sTel, sMobile,
               sFullFileName;
        string[] sStatus = { Global.GetLabel("contact"),       // value = -2  "Επαφή"  
                             Global.GetLabel("prospective"),   // value = -1  "Υποψήφιος"  
                             Global.GetLabel("inactive"),      // value = 0   "Ανενεργός"
                             Global.GetLabel("active") };      // value = 1   "Ενεργός" 
        bool bCheckGrid, bCheckList, bCustomersListChanged, bError;
        Color clrBackground;
        CellStyle csOK, csCandidate, csCustomer, csDisable;
        clsCashTables CashTables = new clsCashTables();
        private void tsbHistory_Click(object sender, EventArgs e)
        {
            frmShowHistory locShowHistory = new frmShowHistory();
            locShowHistory.RecType = 1;                            // 1 - Client Personal Data
            locShowHistory.SrcRec_ID = iID;
            locShowHistory.Contract_ID = 0;
            locShowHistory.Client_ID = iID;
            locShowHistory.Code = "";                             // "" - show history of personal data (not code data)  ucCustomer.fgCodes(ucCustomer.fgCodes.Row, 1)
            locShowHistory.ClientFullName = sClientFullName;
            locShowHistory.ClientsList = 1;                       // 1 - Customers List (Main List), 2 - Clients Black List
            locShowHistory.ClientType = iFormat;

            locShowHistory.ShowDialog();
        }

        clsRMJobs RMJobs = new clsRMJobs();
        clsClients klsClient = new clsClients();

        public frmClientsList()
        {
            InitializeComponent();

            bCheckList = false;
            bCheckGrid = false;
            panNotes.Left = 415;
            panNotes.Top = 39;
            iOldTipos = -999;
            iOldStatus = -999;
            iFormat = -999;
            panClients.Left = 402;
            panClients.Top = 59;
        }
        private void frmClientsList_Load(object sender, EventArgs e)
        {
            //----- check if Clients list was changed @@@-------------------------------
            sTemp = "";
            foundRows = Global.dtCashTables.Select("ID=40");
            if (foundRows.Length > 0) sTemp = foundRows[0]["LastEdit_Time"] + "";

            CashTables = new clsCashTables();
            CashTables.Record_ID = 40;                          // 40 - Clients Table
            CashTables.GetRecord();
            if (CashTables.LastEdit_Time > Convert.ToDateTime(sTemp)) Global.GetClientsList();                   // @@@ 

            //-------------- Define DocTypes List ------------------    
            cmbDocTypes.DataSource = Global.dtDocTypes.Copy();
            cmbDocTypes.DisplayMember = "Title";
            cmbDocTypes.ValueMember = "ID";

            //-------------- Define Divisions List ------------------
            ucCD.cmbFPDivision.DataSource = Global.dtDivisions.Copy();
            ucCD.cmbFPDivision.DisplayMember = "Title";
            ucCD.cmbFPDivision.ValueMember = "ID";

            ucCD.cmbNPDivision.DataSource = Global.dtDivisions.Copy();
            ucCD.cmbNPDivision.DisplayMember = "Title";
            ucCD.cmbNPDivision.ValueMember = "ID";

            //-------------- Define Epaggelmata List ------------------
            ucCD.cmbFPSpecials.DataSource = Global.dtSpecials.Copy();
            ucCD.cmbFPSpecials.DisplayMember = "Title";
            ucCD.cmbFPSpecials.ValueMember = "ID";

            //-------------- Define Brunches List ------------------
            ucCD.cmbFPOccupation.DataSource = Global.dtBrunches.Copy();
            ucCD.cmbFPOccupation.DisplayMember = "Title";
            ucCD.cmbFPOccupation.ValueMember = "ID";

            ucCD.cmbNPBrunches.DataSource = Global.dtBrunches.Copy();
            ucCD.cmbNPBrunches.DisplayMember = "Title";
            ucCD.cmbNPBrunches.ValueMember = "ID";

            //-------------- Define Coutries List ------------------
            ucCD.cmbFPCitizen.DataSource = Global.dtCountries.Copy();
            ucCD.cmbFPCitizen.DisplayMember = "Title";
            ucCD.cmbFPCitizen.ValueMember = "ID";

            ucCD.cmbFPCountryTaxes.DataSource = Global.dtCountries.Copy();
            ucCD.cmbFPCountryTaxes.DisplayMember = "Title";
            ucCD.cmbFPCountryTaxes.ValueMember = "ID";

            ucCD.cmbFPXora.DataSource = Global.dtCountries.Copy();
            ucCD.cmbFPXora.DisplayMember = "Title";
            ucCD.cmbFPXora.ValueMember = "ID";

            ucCD.cmbNPNation.DataSource = Global.dtCountries.Copy();
            ucCD.cmbNPNation.DisplayMember = "Title";
            ucCD.cmbNPNation.ValueMember = "ID";

            ucCD.cmbNPCountryTaxes.DataSource = Global.dtCountries.Copy();
            ucCD.cmbNPCountryTaxes.DisplayMember = "Title";
            ucCD.cmbNPCountryTaxes.ValueMember = "ID";

            ucCD.cmbNPCountry.DataSource = Global.dtCountries.Copy();
            ucCD.cmbNPCountry.DisplayMember = "Title";
            ucCD.cmbNPCountry.ValueMember = "ID";

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString(Global.GridStyle);
            fgList.DrawMode = DrawModeEnum.OwnerDraw;
            fgList.RowColChange += new EventHandler(fgList_RowColChange);
            fgList.OwnerDrawCell += fgList_OwnerDrawCell;

            csOK = fgList.Styles.Add("OK");
            csOK.BackColor = Color.LightGreen;

            csCandidate = fgList.Styles.Add("Candidate");
            csCandidate.BackColor = Color.LightYellow;

            csCustomer = fgList.Styles.Add("Customer");
            csCustomer.BackColor = Color.PeachPuff;

            csDisable = fgList.Styles.Add("Disable");
            csDisable.BackColor = Color.Tomato;

            switch (iMode)
            {
                case 1:                                                         // 1 - Back Office Mode (only Clients List)
                    sMeTitle = Global.GetLabel("customers_list");
                    fgList.Cols[1].Width = 325;
                    chkContact.Checked = false;
                    chkCandidate.Checked = false;
                    chkCustomer.Checked = true;
                    chkDisable.Checked = false;
                    break;
                case 2:                                                       // 2 - RM Clients
                    sMeTitle = Global.GetLabel("customers_list");
                    fgList.Cols[1].Width = 220;
                    fgList.Cols[2].Visible = false;
                    fgList.Cols[3].Visible = false;
                    fgList.Cols[4].Visible = true;
                    fgList.Cols[5].Visible = true;
                    fgList.Cols[6].Visible = true;
                    fgList.Cols[7].Visible = true;
                    fgList.Cols[8].Visible = true;
                    fgList.Cols[9].Visible = true;
                    fgList.Cols[10].Visible = true;
                    chkContact.Checked = false;
                    chkCandidate.Checked = true;
                    chkCustomer.Checked = true;
                    chkDisable.Checked = false;
                    break;
                case 3:                                                          // 3 - Influence Centers
                    sMeTitle = "Κέντρα Επιρροής";
                    fgList.Cols[1].Width = 260;
                    fgList.Cols[2].Caption = "X.";
                    fgList.Cols[2].Visible = true;
                    fgList.Cols[3].Caption = "Αρ.σχέσεων";
                    fgList.Cols[3].Visible = true;
                    chkContact.Checked = true;
                    chkCandidate.Checked = true;
                    chkCustomer.Checked = true;
                    chkDisable.Checked = false;
                    break;
                case 4:                                                          // 4 - Introducers List
                    sMeTitle = "Introducers List";
                    fgList.Cols[1].Width = 325;
                    chkContact.Checked = true;
                    chkCandidate.Checked = true;
                    chkCustomer.Checked = true;
                    chkDisable.Checked = false;
                    ucCD.tabClientData.TabPages.Remove(ucCD.tpActivities);
                    ucCD.tabClientData.TabPages.Remove(ucCD.tpRMJobs);
                    ucCD.tabClientData.TabPages.Remove(ucCD.tpInfluenceCenters);
                    break;
                case 5:                                                         // 5 - Represent Persons List
                    sMeTitle = "Represent Persons List";
                    fgList.Cols[1].Width = 325;
                    chkContact.Checked = false;
                    chkCandidate.Checked = false;
                    chkCustomer.Checked = true;
                    chkDisable.Checked = false;
                    ucCD.tabClientData.TabPages.Remove(ucCD.tpActivities);
                    ucCD.tabClientData.TabPages.Remove(ucCD.tpRMJobs);
                    ucCD.tabClientData.TabPages.Remove(ucCD.tpInfluenceCenters);
                    break;
            }

            DefineList();

            tsbSave.Enabled = false;
            if (iRightsLevel != 2)
            {                         // 2 - Full Rights Level
                tsbAdd.Enabled = false;
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;

                ucCD.toolRandevou.Enabled = false;
                ucCD.toolNeeds.Enabled = false;
                ucCD.toolPackages.Enabled = false;
                ucCD.toolDocFiles.Enabled = false;
                ucCD.toolAccounts.Enabled = false;
                ucCD.toolNotes.Enabled = false;
                ucCD.toolInfluenceCenters.Enabled = false;
                ucCD.toolDepedentsList.Enabled = false;
            }

            bCheckList = true;
            bCheckGrid = true;
        }
        private void Form_Deactivate(object sender, EventArgs e)
        {
            if (bCustomersListChanged) Global.GetContractsList();
            bCustomersListChanged = false;
        }
        protected override void OnResize(EventArgs e)
        {
            fgList.Height = this.Height - 110;
            ucCD.Height = this.Height - 106;
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            rbPhysical.Checked = true;
            panSelectType.Visible = true;
        }
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            tsbSave.Enabled = true;
        }
        private void tsbDelete_Click(object sender, EventArgs e)
        {
            int j, k, m;

            RMJobs = new clsRMJobs();
            RMJobs.Client_ID = iID;
            j = RMJobs.CheckJobsExisting();

            if (ucCD.fgPackages.Rows.Count > 1) j = 1;
            if (ucCD.fgBankAccounts.Rows.Count > 1) j = 2;
            if (ucCD.fgDocFiles.Rows.Count > 1) j = 3;

            if (iMode == 1)
                if ((Convert.ToInt32(ucCD.cmbFPStatus.SelectedValue) > 0) || (Convert.ToInt32(ucCD.cmbNPStatus.SelectedValue) > 0)) j = 6;

            switch (j)
            {
                case 0:
                    if (MessageBox.Show(Global.GetLabel("attention_you_ask_for_deletion") + "." + "\n" + Global.GetLabel("are_you_sure_for_deletion"), Global.AppTitle,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        iID = Convert.ToInt32(fgList[fgList.Row, 11]);

                        clsContracts_CashAccounts Contracts_CashAccounts = new clsContracts_CashAccounts();
                        Contracts_CashAccounts.Client_ID = iID;
                        Contracts_CashAccounts.DeleteRecord_Client_ID();

                        clsClientsDocFiles ClientsDocFiles = new clsClientsDocFiles();
                        ClientsDocFiles.Client_ID = iID;
                        ClientsDocFiles.DeleteRecord_Client_ID();

                        clsWebUsers WebUsers = new clsWebUsers();
                        clsWebUsers WebUsers2 = new clsWebUsers();
                        clsWebUsersDevices WebUsersDevices = new clsWebUsersDevices();
                        clsWebUsersDevices WebUsersDevices2 = new clsWebUsersDevices();

                        WebUsers = new clsWebUsers();
                        WebUsers.Client_ID = iClient_ID;
                        WebUsers.EMail = "";
                        WebUsers.Mobile = "";
                        WebUsers.AFM = "";
                        WebUsers.DoB = "";
                        WebUsers.Password = "";
                        WebUsers.GetList();
                        foreach (DataRow dtRow in WebUsers.List.Rows)
                        {
                            m = Convert.ToInt32(dtRow["ID"]);
                            WebUsers2 = new clsWebUsers();
                            WebUsers2.Client_ID = m;
                            WebUsers2.DeleteRecord();

                            WebUsersDevices = new clsWebUsersDevices();
                            WebUsersDevices.Record_ID = 0;
                            WebUsersDevices.WU_ID = m;
                            WebUsersDevices.EMail = "";
                            WebUsersDevices.Mobile = "";
                            WebUsersDevices.AFM = "";
                            WebUsersDevices.DoB = "1900/01/01";
                            WebUsersDevices.Client_ID = 0;
                            WebUsersDevices.Password = "";
                            WebUsersDevices.GetList();
                            foreach (DataRow dtRow1 in WebUsersDevices.List.Rows)
                            {
                                k = Convert.ToInt32(dtRow1["ID"]);
                                WebUsersDevices2 = new clsWebUsersDevices();
                                WebUsersDevices2.Record_ID = k;
                                WebUsersDevices2.DeleteRecord();
                            }

                        }

                        clsClients Clients = new clsClients();
                        Clients.Record_ID = iID;
                        Clients.DeleteRecord();

                        fgList.RemoveItem(fgList.Row);
                        if (fgList.Rows.Count > 1)
                        {
                            fgList.Focus();
                            iID = Convert.ToInt32(fgList[fgList.Row, 11]);
                        }
                        fgList.Redraw = true;

                        iFormat = Convert.ToInt32(fgList[fgList.Row, 13]);
                        iAction = 1;

                        // 1 - EDIT Mode
                        ShowRecord();
                    }
                    break;
                case 1:
                    MessageBox.Show(Global.GetLabel("customer_cannot_be__deleted_due_code_list_is_not_empty"), Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                case 2:
                    MessageBox.Show(Global.GetLabel("customer_cannot_be__deleted_due_bank_accounts_list_is_not_empty"), Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                case 3:
                    MessageBox.Show(Global.GetLabel("customer_cannot_be__deleted_due_documents_list_is_not_empty"), Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                case 4:
                    MessageBox.Show(Global.GetLabel("customer_cannot_be__deleted_due_update_list_is_not_empty"), Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                case 5:
                    MessageBox.Show(Global.GetLabel("customer_cannot_be__deleted_due_investment_proposal__list_is_not_empty"), Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                case 6:
                    MessageBox.Show(Global.GetLabel("customer_cannot_be_deleted_cause_is_active"), Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                case 7:
                    MessageBox.Show(Global.GetLabel("__b46"), Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
            }
        }
        private void tsbPrint_Click(object sender, EventArgs e)
        {
            Global.TranslateUserName(txtFilter.Text, out sSurnameGreek, out sSurnameEnglish);
        }
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            DefineList();
            txtFilter.Focus();
        }

        private void tsbFilter_Click(object sender, EventArgs e)
        {

        }
        private void chkContact_CheckedChanged(object sender, EventArgs e)
        {
            if (bCheckGrid)
            {
                bCheckList = false;
                iClient_ID = 0;
                DefineList();
                bCheckList = true;
            }
        }
        private void chkCandidate_CheckedChanged(object sender, EventArgs e)
        {
            if (bCheckGrid)
            {
                bCheckList = false;
                iClient_ID = 0;
                DefineList();
                bCheckList = true;
            }
        }
        private void chkCustomer_CheckedChanged(object sender, EventArgs e)
        {
            if (bCheckGrid)
            {
                bCheckList = false;
                iClient_ID = 0;
                DefineList();
                bCheckList = true;
            }
        }
        private void chkDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (bCheckGrid)
            {
                bCheckList = false;
                iClient_ID = 0;
                DefineList();
                bCheckList = true;
            }
        }
        private void btnOK_Tipos_Click(object sender, EventArgs e)
        {
            tsbSave.Enabled = true;

            iAction = 0;                                                                // 0 - ADD Mode
            iID = 0;
            sOldValues = "";

            ucCD.InitLists(iMode);
            EmptyData();
            ucCD.tabClientData.SelectedIndex = 0;
            ucCD.cmbUser2.SelectedValue = Global.User_ID;
            if (rbPhysical.Checked)                                                         // 1 - Clients
            {
                ucCD.panFP.Visible = true;
                ucCD.panNP.Visible = false;

                iFormat = 1;
                ucCD.txtFPSurname.Focus();
            }
            else
            {
                ucCD.panFP.Visible = false;
                ucCD.panNP.Visible = true;

                iFormat = 2;
                ucCD.txtNPTitle.Focus();
            }
            panSelectType.Visible = false;
        }

        private void btnCancel_Tipos_Click(object sender, EventArgs e)
        {
            panSelectType.Visible = false;
            tsbSave.Enabled = false;
            fgList.Focus();
        }
        private void picFilePath_Click(object sender, EventArgs e)
        {
            sFullFileName = Global.FileChoice(Global.DefaultFolder);
            txtFileName.Text = Path.GetFileName(sFullFileName);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            panNotes.Visible = false;
            this.Refresh();

            if (iMode == 1) SaveRecord();                 // iMode = 1 - BO Mode, don't check if such customer exists    
            else                                          // iMode = 2 - RM Mode, so check if such customer exists
            {
                switch (iFormat)
                {
                    case 1:
                        sClientFullName = (ucCD.txtFPSurname.Text.Trim() + " " + ucCD.txtFPFirstname.Text.Trim()).Trim();
                        sSurname = ucCD.txtFPSurname.Text;
                        sFirstname = ucCD.txtFPFirstname.Text;
                        sTel = ucCD.txtFPTel.Text;
                        sMobile = ucCD.txtFPMobile.Text;
                        break;
                    case 2:
                        sClientFullName = ucCD.txtNPTitle.Text.Trim();
                        sSurname = ucCD.txtNPTitle.Text;
                        sFirstname = "";
                        sTel = ucCD.txtNPTel.Text;
                        sMobile = ucCD.txtNPMobile.Text;
                        break;
                }

                if (sOldClientFullName != sClientFullName)           //client's name was changed, than check this new name if such customer exists
                {
                    fgClients.Redraw = false;
                    fgClients.Rows.Count = 1;


                    //--------- initialise Black List -----------
                    clsClients Clients = new clsClients();
                    Clients.Surname = sSurname;
                    Clients.Firstname = sFirstname;
                    Clients.Tel = sTel;
                    Clients.Mobile = sMobile;
                    Clients.GetSameClients();
                    if (Clients.Record_ID != iID)
                        fgClients.AddItem(Clients.Surname + "\t" + Clients.Firstname + "\t" + Clients.FirstnameFather + "\t" +
                                          sStatus[Clients.Status + 2] + "\t" + Clients.RM_Surname + "  " + Clients.RM_Firstname + "\t" + Clients.EMail + "\t" + Clients.Record_ID);


                    fgClients.Redraw = true;
                    if (fgClients.Rows.Count > 1) panClients.Visible = true;
                    else SaveRecord();
                }
                else SaveRecord();                        //  client's name wasn't change, so it is not neccecary if such customer exists
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panNotes.Visible = false;
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            txtNotes.Text = "";
            txtFileName.Text = "";
            cmbDocTypes.SelectedValue = 0;
            panNotes.Visible = true;
        }
        private void SaveRecord()
        {
            int i = 0;
            clsClients klsClient = new clsClients();

            bCustomersListChanged = true;
            bError = false;

            switch (iFormat)
            {
                case 1:
                    if (ucCD.txtFPSurname.Text.Length != 0)
                    {
                        sClientFullName = (ucCD.txtFPSurname.Text.Trim() + " " + ucCD.txtFPFirstname.Text.Trim()).Trim();


                        if (iAction == 0) klsClient.Type = 1;             // 0 - ADD Mode
                        else
                        {
                            klsClient.Record_ID = iID;
                            klsClient.EMail = "";
                            klsClient.Mobile = "";
                            klsClient.AFM = "";
                            klsClient.DoB = Convert.ToDateTime("1900/01/01");
                            klsClient.GetRecord();
                        }

                        klsClient.Surname = ucCD.txtFPSurname.Text + "";
                        klsClient.Firstname = ucCD.txtFPFirstname.Text + "";
                        klsClient.SurnameEng = ucCD.txtFPSurnameEng.Text + "";
                        klsClient.FirstnameEng = ucCD.txtFPFirstnameEng.Text + "";
                        klsClient.SurnameEng = ucCD.txtFPSurnameEng.Text + "";
                        klsClient.FirstnameEng = ucCD.txtFPFirstnameEng.Text + "";
                        klsClient.SurnameFather = ucCD.txtFPFatherSurname.Text + "";
                        klsClient.FirstnameFather = ucCD.txtFPFatherFirstname.Text + "";
                        klsClient.SurnameMother = ucCD.txtFPMotherSurname.Text + "";
                        klsClient.FirstnameMother = ucCD.txtFPMotherFirstname.Text + "";
                        klsClient.SurnameSizigo = ucCD.txtFPSyzygosSurname.Text + "";
                        klsClient.FirstnameSizigo = ucCD.txtFPSyzygosFirstname.Text + "";
                        klsClient.Status = Convert.ToInt32(ucCD.cmbFPStatus.SelectedValue);
                        klsClient.BlockStatus = ucCD.chkFPBlockStatus.Checked ? 1 : 0;
                        klsClient.Division = Convert.ToInt32(ucCD.cmbFPDivision.SelectedValue);
                        klsClient.Is_InfluenceCenter = (ucCD.chkFPInfluenceCenter.Checked ? 1 : 0);
                        klsClient.Is_Introducer = (ucCD.chkFPIntroducer.Checked ? 1 : 0);
                        klsClient.Is_RepresentPerson = (ucCD.chkFPRepresentPerson.Checked ? 1 : 0);
                        klsClient.Spec_ID = Convert.ToInt32(ucCD.cmbFPSpecials.SelectedValue);
                        klsClient.Brunch_ID = Convert.ToInt32(ucCD.cmbFPOccupation.SelectedValue);
                        klsClient.DoB = ucCD.dFPDoB.Value;
                        klsClient.BornPlace = ucCD.txtFPBornPlace.Text;
                        klsClient.Citizen_ID = Convert.ToInt32(ucCD.cmbFPCitizen.SelectedValue);
                        klsClient.Sex = ucCD.cmbFPSex.Text;
                        klsClient.FamilyStatus = ucCD.cmbFPFamilyStatus.SelectedIndex;
                        klsClient.Category = ucCD.cmbFPCategory.SelectedIndex;
                        klsClient.Guardian_ID = 0;
                        klsClient.ADT = ucCD.txtFPADT.Text + "";
                        klsClient.ExpireDate = ucCD.txtFPExpireDate.Text + "";
                        klsClient.Police = ucCD.txtFPPolice.Text + "";
                        klsClient.Passport = ucCD.txtFPPassport.Text + "";
                        klsClient.Passport_ExpireDate = ucCD.txtFPPassport_ExpireDate.Text + "";
                        klsClient.Passport_Police = ucCD.txtFPPassport_Police.Text + "";
                        klsClient.AFM = ucCD.txtFPAFM.Text + "";
                        klsClient.DOY = ucCD.txtFPDOY.Text + "";
                        klsClient.AFM2 = ucCD.txtFPAFM2.Text + "";
                        klsClient.DOY2 = ucCD.txtFPDOY2.Text + "";
                        klsClient.AMKA = ucCD.txtFPAMKA.Text + "";
                        klsClient.VAT_Percent = Convert.ToSingle(ucCD.txtFPFPA.Text);
                        klsClient.CountryTaxes_ID = Convert.ToInt32(ucCD.cmbFPCountryTaxes.SelectedValue);
                        klsClient.Address = ucCD.txtFPAddress.Text + "";
                        klsClient.City = ucCD.txtFPCity.Text + "";
                        klsClient.Zip = ucCD.txtFPZip.Text + "";
                        klsClient.Country_ID = Convert.ToInt32(ucCD.cmbFPXora.SelectedValue);
                        klsClient.Tel = ucCD.txtFPTel.Text + "";
                        klsClient.Fax = ucCD.txtFPFax.Text + "";
                        klsClient.Mobile = ucCD.txtFPMobile.Text + "";
                        klsClient.SendSMS = (ucCD.chkFPSMS.Checked ? 1 : 0);
                        klsClient.EMail = ucCD.txtFPEMail.Text + "";
                        klsClient.ConnectionMethod = ucCD.cmbFPConnectionMethod.SelectedIndex;
                        klsClient.CompanyTitle = ucCD.txtFPCompany.Text + "";
                        klsClient.CompanyDescription = ucCD.txtFPCompanyDescription.Text + "";
                        klsClient.JobPosition = ucCD.txtFPJobPosition.Text + "";
                        klsClient.JobAddress = ucCD.txtFPJobAddress.Text + "";
                        klsClient.JobCity = ucCD.txtFPJobCity.Text + "";
                        klsClient.JobZip = ucCD.txtFPJobZip.Text + "";
                        klsClient.JobCountry_ID = Convert.ToInt32(ucCD.cmbFPJobCountry_ID.SelectedValue);
                        klsClient.JobTel = ucCD.txtFPJobTel.Text + "";
                        klsClient.JobMobile = ucCD.txtFPJobMobile.Text + "";
                        klsClient.JobEMail = ucCD.txtFPJobEMail.Text + "";
                        klsClient.JobURL = ucCD.txtFPJobURL.Text + "";
                        klsClient.Users_List = ucCD.Users_List;
                        klsClient.Ekkatharistika = Convert.ToInt32(ucCD.lblEkkatharistika.Text);
                        klsClient.SpecialCategory = ucCD.lblFPSpecialCategory.Text + "";
                        klsClient.Merida = ucCD.txtFPMerida.Text + "";
                        klsClient.LogAxion = ucCD.txtFPLogAxion.Text + "";
                        klsClient.Notes = ucCD.txtNotes.Text + "";
                        klsClient.RM_ID = ((Convert.ToInt32(ucCD.cmbUser2.SelectedValue) != 0) ? Convert.ToInt32(ucCD.cmbUser2.SelectedValue) : Global.User_ID);
                        klsClient.Conne = ucCD.txtConne.Text + "";
                        klsClient.SumAxion = Convert.ToSingle(ucCD.txtFPSumAxion.Text);
                        klsClient.SumAkiniton = Convert.ToSingle(ucCD.txtFPSumAkiniton.Text);
                        klsClient.Risk = ucCD.cmbFPRisk.SelectedIndex;
                        klsClient.DateIns = DateTime.Now;
                        if (iAction == 0)
                        {                                                    // 0 - ADD Mode
                            klsClient.LogSxedio_ID = 0;
                            iID = klsClient.InsertRecord();
                            jAktion = 0;                                                        // 0 - ADD, 1 - EDIT, 2 - DELETE
                            Global.CreateClientFolders(sClientFullName.Replace(".", "_"));
                        }
                        else
                        {
                            klsClient.EditRecord();
                            jAktion = 1;                                                        // 0 - ADD, 1 - EDIT, 2 - DELETE

                            if (!Global.DMS_CheckDirectoryExists("Customers/" + sOldClientFullName.Replace(".", "_")))
                                Global.CreateClientFolders(sClientFullName.Replace(".", "_"));
                            else
                                if (sOldClientFullName.Trim() != sClientFullName.Trim())
                                Global.DMS_RenameFolderName(sOldClientFullName.Replace(".", "_"), sClientFullName.Replace(".", "_"));
                        }
                        ucCD.Client_ID = iID;

                        if (iAction == 0) AddRecord2LogSxedio(iID, (ucCD.txtFPSurname.Text + " " + ucCD.txtFPFirstname.Text).Trim());                 // 0 - ADD Mode

                        iDocFiles_ID = 0;
                        if (txtFileName.Text.Trim().Length > 0)
                            if (MessageBox.Show(Global.GetLabel("add_this_document_to_the_customer_document_file"), Global.AppTitle,
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                AddDocument();
                        Global.SaveHistory(1, iID, iID, 0, jAktion, sOldValues, iDocFiles_ID, txtNotes.Text, DateTime.Now, Global.User_ID);

                        SaveTables();

                        if (ucCD.chkFPBlockStatus.Checked)
                        {
                            clsWebUsers WebUsers = new clsWebUsers();
                            WebUsers.Client_ID = iID;
                            WebUsers.DeleteRecord_Client_ID();
                        }
                        iNewID = iID;
                        iOldRow = fgList.Row;
                    }
                    else
                    {
                        bError = true;
                        MessageBox.Show(Global.GetLabel("the_introduction_of_the_surname_is_mandatory"), sMeTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    break;
                case 2:

                    if (ucCD.txtNPTitle.Text.Length != 0)
                    {
                        sClientFullName = ucCD.txtNPTitle.Text.Trim();

                        if (iAction == 0) klsClient.Type = 2;                             // 0 - ADD Mode
                        else
                        {
                            klsClient.Record_ID = iID;
                            klsClient.EMail = "";
                            klsClient.Mobile = "";
                            klsClient.AFM = "";
                            klsClient.DoB = Convert.ToDateTime("1900/01/01");
                            klsClient.GetRecord();
                        }

                        klsClient.Surname = ucCD.txtNPTitle.Text + "";
                        klsClient.SurnameEng = ucCD.txtNPTitleEng.Text + "";
                        klsClient.Firstname = ucCD.txtNPDiakritikosTitlos.Text + "";
                        klsClient.SurnameFather = ucCD.txtNPEdra.Text + "";
                        klsClient.FirstnameFather = ucCD.txtNPMorfi.Text + "";
                        klsClient.SurnameMother = "";
                        klsClient.FirstnameMother = "~";
                        klsClient.SurnameSizigo = "";
                        klsClient.FirstnameSizigo = ucCD.txtNPLEI.Text + "";
                        klsClient.Status = Convert.ToInt32(ucCD.cmbNPStatus.SelectedValue);
                        klsClient.BlockStatus = 0;
                        klsClient.Division = Convert.ToInt32(ucCD.cmbNPDivision.SelectedValue);
                        klsClient.Is_InfluenceCenter = (ucCD.chkNPInfluenceCenter.Checked ? 1 : 0);
                        klsClient.Is_Introducer = (ucCD.chkNPIntroducer.Checked ? 1 : 0);
                        klsClient.Is_RepresentPerson = (ucCD.chkNPRepresentPerson.Checked ? 1 : 0);
                        klsClient.Spec_ID = 0;
                        klsClient.Brunch_ID = Convert.ToInt32(ucCD.cmbNPBrunches.SelectedValue);
                        klsClient.DoB = Convert.ToDateTime("1900/01/01");
                        klsClient.BornPlace = "";
                        klsClient.Citizen_ID = Convert.ToInt32(ucCD.cmbNPNation.SelectedValue);
                        klsClient.VAT_Percent = Convert.ToSingle(ucCD.txtNPFPA.Text);
                        klsClient.FamilyStatus = 0;
                        klsClient.Category = ucCD.cmbNPCategory.SelectedIndex;
                        klsClient.Guardian_ID = 0;
                        klsClient.ADT = ucCD.txtNPAM.Text + "";
                        klsClient.ExpireDate = ucCD.txtNPExpireDate.Text + "";
                        klsClient.Police = ucCD.txtNPArmodiaArxi.Text + "";
                        klsClient.Passport = "";
                        klsClient.Passport_ExpireDate = "";
                        klsClient.Passport_Police = "";
                        klsClient.DOY = ucCD.txtNPDOY.Text + "";
                        klsClient.AFM = ucCD.txtNPAFM.Text + "";
                        klsClient.DOY2 = "";
                        klsClient.AFM2 = "";
                        klsClient.CountryTaxes_ID = Convert.ToInt32(ucCD.cmbNPCountryTaxes.SelectedValue);
                        klsClient.Address = ucCD.txtNPAddress.Text + "";
                        klsClient.City = ucCD.txtNPCity.Text + "";
                        klsClient.Zip = ucCD.txtNPZip.Text + "";
                        klsClient.Country_ID = Convert.ToInt32(ucCD.cmbNPCountry.SelectedValue);
                        klsClient.Tel = ucCD.txtNPTel.Text + "";
                        klsClient.Fax = ucCD.txtNPFax.Text + "";
                        klsClient.Mobile = ucCD.txtNPMobile.Text + "";
                        klsClient.SendSMS = (ucCD.chkNPSMS.Checked ? 1 : 0);
                        klsClient.EMail = ucCD.txtNPEMail.Text + "";
                        klsClient.ConnectionMethod = ucCD.cmbNPConnectionMethod.SelectedIndex;
                        klsClient.CompanyTitle = "";
                        klsClient.CompanyDescription = "";
                        klsClient.JobPosition = "";
                        klsClient.JobAddress = "";
                        klsClient.JobCity = "";
                        klsClient.JobZip = "";
                        klsClient.JobCountry_ID = 0;
                        klsClient.JobTel = "";
                        klsClient.JobMobile = "";
                        klsClient.JobEMail = "";
                        klsClient.JobURL = "";
                        klsClient.Users_List = ucCD.Users_List;
                        klsClient.Ekkatharistika = Convert.ToInt32(ucCD.lblEkkatharistika.Text);
                        klsClient.SpecialCategory = ucCD.lblNPSpecialCategory.Text + "";
                        klsClient.Merida = ucCD.txtNPMerida.Text + "";
                        klsClient.LogAxion = ucCD.txtNPLogAxion.Text + "";
                        klsClient.Notes = ucCD.txtNotes.Text + "";
                        klsClient.RM_ID = (Convert.ToInt32(ucCD.cmbUser2.SelectedValue) != 0 ? Convert.ToInt32(ucCD.cmbUser2.SelectedValue) : Global.User_ID);
                        klsClient.Conne = ucCD.txtConne.Text + "";
                        klsClient.SumAxion = Convert.ToSingle(ucCD.txtNPSumAxion.Text);
                        klsClient.SumAkiniton = Convert.ToSingle(ucCD.txtNPSumAkiniton.Text);
                        klsClient.Risk = ucCD.cmbNPRisk.SelectedIndex;
                        klsClient.DateIns = DateTime.Now;
                        if (iAction == 0)
                        {                              // 0 - ADD Mode
                            klsClient.LogSxedio_ID = 0;
                            iID = klsClient.InsertRecord();
                            jAktion = 0;                                 // 0 - ADD, 1 - EDIT, 2 - DELETE
                            Global.CreateClientFolders(sClientFullName);
                        }
                        else
                        {
                            klsClient.EditRecord();
                            jAktion = 1;                                 // 0 - ADD, 1 - EDIT, 2 - DELETE

                            if (!Global.DMS_CheckDirectoryExists("Customers/" + sOldClientFullName.Replace(".", "_")))
                                Global.CreateClientFolders(sClientFullName.Replace(".", "_"));
                            else
                               if (sOldClientFullName != sClientFullName)
                                Global.DMS_RenameFolderName(sOldClientFullName, sClientFullName);

                        }
                        ucCD.Client_ID = iID;

                        if (iAction == 0)                 // 0 - ADD Mode
                            AddRecord2LogSxedio(iID, ucCD.txtNPTitle.Text);

                        iDocFiles_ID = 0;
                        if (txtFileName.Text.Trim().Length > 0)
                            if (MessageBox.Show(Global.GetLabel("add_this_document_to_the_customer_document_file"), Global.AppTitle,
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                AddDocument();

                        Global.SaveHistory(1, iID, iID, 0, jAktion, sOldValues, iDocFiles_ID, txtNotes.Text, DateTime.Now, Global.User_ID);

                        SaveTables();
                        iNewID = iID;
                        iOldRow = fgList.Row;
                    }
                    else
                    {
                        bError = true;
                        MessageBox.Show(Global.GetLabel("the_introduction_of_the_surname_is_mandatory"), sMeTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    break;
            }

            if (iAction == 0)
            {
                clsContracts Contracts = new clsContracts();
                Contracts.Status = -1;                                    //  -1 - all contracts, 0 - only cancelled contracts, 1 - only actual contracts
                Contracts.ClientsFilter = Global.ClientsFilter;
                Contracts.GetCashList();
                Global.dtContracts = Contracts.List;

                Global.dtContracts.DefaultView.Sort = "Fullname, ID";
            }
            else
            {
                foundRows = Global.dtContracts.Select("ID = " + iID);
                dtRow = foundRows[0];
                switch (iFormat)
                {
                    case 1:
                        dtRow["Fullname"] = (ucCD.txtFPSurname.Text + " " + ucCD.txtFPFirstname.Text).Trim();
                        dtRow["Surname"] = ucCD.txtFPSurname.Text + "";
                        dtRow["Firstname"] = ucCD.txtFPFirstname.Text + "";
                        break;
                    case 2:
                        dtRow["Fullname"] = ucCD.txtNPTitle.Text + ""; ;
                        dtRow["Surname"] = ucCD.txtNPTitle.Text + ""; ;
                        dtRow["Firstname"] = "";
                        break;
                }
            }

            if (!bError)
            {
                bCustomersListChanged = false;
                MessageBox.Show(Global.GetLabel("saving_completed_successfully"), Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                bCustomersListChanged = true;
                iAction = 1;
            }

            DefineList();

            sTemp = iNewID.ToString();
            i = fgList.FindRow(sTemp, 1, 11, false);
            if (i < 0) fgList.Row = iOldRow;
            else fgList.Row = i;

            ucCD.ShowRecord(iID, iRightsLevel, iMode);
            tsbSave.Enabled = false;

            clsSystem System = new clsSystem();
            System.EditCashTables_LastEdit_Time(1);

            Global.GetClientsList();

            fgList.Focus();
        }
        private void SaveTables()
        {

        }
        private void fgList_RowColChange(object sender, EventArgs e)
        {
            if (bCheckGrid)
            {
                if (fgList.Row > 0)
                {
                    tsbSave.Enabled = false;
                    iID = Convert.ToInt32(fgList[fgList.Row, 11]);
                    this.Text = sMeTitle + " (" + iID.ToString() + ")";

                    iDocFiles_ID = 0;
                    iAction = 1;                                    // 1 - EDIT Mode
                    EmptyData();
                    ShowRecord();

                    //--------- initialise Black List -----------
                    if (sExtra == "1")
                    {
                        clsClients klsClient = new clsClients();
                        klsClient.Record_ID = iID;
                        if (klsClient.GetCheckBlackList())
                            MessageBox.Show(Global.GetLabel("customer_is_in_blacklist"), Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    sOldClientFullName = fgList[fgList.Row, 1].ToString();
                }
            }
        }
        private void DefineList()
        {
            bCheckGrid = false;
            if (txtFilter.Text.Length > 0) Global.TranslateUserName(txtFilter.Text, out sSurnameGreek, out sSurnameEnglish);

            if (txtFilter.Text.IndexOf("/") < 0)
                sTemp = "Tipos < 3 AND (Fullname LIKE '%" + sSurnameGreek + "%' OR Fullname LIKE '%" + sSurnameEnglish + "%' OR Code LIKE '%" + txtFilter.Text + "%' OR ContractTitle LIKE '%" + txtFilter.Text + "%' OR NumberAccount LIKE '%" + txtFilter.Text + "%')";
            else
            {
                i = txtFilter.Text.IndexOf("/");
                s1 = txtFilter.Text.Substring(0, i);
                s2 = txtFilter.Text.Substring(i + 1);
                sTemp = "Tipos < 3 AND (Code = '" + s1 + "' AND Portfolio LIKE '%" + s2 + "%')";
            }

            switch (iMode)
            {
                case 1:
                case 2:
                    sTemp1 = "";
                    if (chkCustomer.Checked) sTemp1 = "ClientStatus > 0";                           // >=0 Clients List

                    if (chkDisable.Checked)
                    {
                        if (sTemp1.Length > 0) sTemp1 = sTemp1 + " OR ";
                        sTemp1 = sTemp1 + " ClientStatus = 0";                                       // 0 - disabled  Clients List
                    }

                    if (chkCandidate.Checked)
                    {
                        if (sTemp1.Length > 0) sTemp1 = sTemp1 + " OR ";
                        sTemp1 = sTemp1 + " ClientStatus = - 1";                                     // -1  Candidates List
                    }

                    if (chkContact.Checked)
                    {
                        if (sTemp1.Length > 0) sTemp1 = sTemp1 + " OR ";
                        sTemp1 = sTemp1 + " ClientStatus = - 2";                                    // -2  Contacts
                    }
                    //sTemp1 = sTemp1 + " AND Is_RepresentPerson = 0 AND Is_InfluenceCenter = 0 AND Is_Introducer = 0 ";

                    if (sTemp1.Length == 0) sTemp = "Client_ID < 0 ";                         // not checked any clients group, so list must be empty => so client_id < 0
                    else sTemp = sTemp + " AND ( " + sTemp1 + " )";
                    break;
                case 3:
                    sTemp = sTemp + " AND Is_InfluenceCenter = 1";                           // Is_InfluenceCenter = 1  Kentro Epirrois
                    break;
                case 4:
                    sTemp = sTemp + " AND Is_Introducer = 1";                                // Is_Introducer = 1       Introducer
                    break;
                case 5:
                    sTemp = sTemp + " AND Is_RepresentPerson = 1";                           // Is_RepresentPerson = 1   Representation Person
                    break;
            }

            iOldClient_ID = -999;
            i = 0;
            fgList.Redraw = false;
            fgList.Rows.Count = 1;

            dtView = Global.dtContracts.DefaultView;
            dtView.RowFilter = sTemp;
            foreach (DataRowView dtViewRow in dtView)
            {
                if (iOldClient_ID != Convert.ToInt32(dtViewRow["Client_ID"]))  //&& (Convert.ToInt32(dtViewRow["Tipos"]) != 3))
                {
                    iOldClient_ID = Convert.ToInt32(dtViewRow["Client_ID"]);

                    i = i + 1;

                    sTemp = (dtViewRow["Fullname"] + "").Trim();
                    if (sTemp.Length == 0 && Convert.ToInt32(dtViewRow["ClientStatus"]) == -1) sTemp = dtViewRow["Client_ID"].ToString();           // it's for ypopsifious from Web or Mobi apps
                    fgList.AddItem(i + "\t" + sTemp + "\t" + "" + "\t" + dtViewRow["DependentPersons"] + "\t" +
                                   "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" +
                                   dtViewRow["Client_ID"] + "\t" + dtViewRow["RM_Step"] + "\t" + dtViewRow["Tipos"] + "\t" +
                                   dtViewRow["Conne"] + "\t" + dtViewRow["ClientStatus"]);
                }
            }

            bCheckGrid = false;
            fgList.Row = 0;
            if (fgList.Rows.Count > 1)
            {
                bCheckGrid = true;
                fgList.Row = 1;
                sOldClientFullName = fgList[1, 1].ToString().Trim();
                sClientFullName = fgList[1, 1].ToString().Trim();
            }
            fgList.Redraw = true;

            bCheckGrid = true;
        }
        private void fgList_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            if (e.Row > 0)
            {
                if (e.Col == 1)
                    if (Convert.ToInt32(fgList[e.Row, 15]) == -1) fgList.Rows[e.Row].Style = csCandidate;
                    else
                        if (Convert.ToInt32(fgList[e.Row, 15]) >= 0)
                        if (Convert.ToInt32(fgList[e.Row, 15]) == 0) fgList.Rows[e.Row].Style = csDisable;
                        else fgList.Rows[e.Row].Style = csCustomer;

                //--- colorize step cells ----
                if (e.Col > 3)
                    if (Convert.ToInt32(fgList[e.Row, 12]) >= (e.Col - 3)) fgList.Rows[e.Row].Style = csOK;

            }
        }
        private void ShowRecord()
        {
            //ucCD.ShowRecord(iID, iRightsLevel, iMode);

            if (bCheckGrid)
            {
                ucCD.ShowRecord(iID, iRightsLevel, iMode);

                //--- Define tabClientData Pages List (Onle for BackOffice Mode and RM Mode) --------------
                if (iMode == 1 || iMode == 2)
                    if (iOldStatus != Convert.ToInt32(fgList[fgList.Row, 15]))
                    {
                        iOldStatus = Convert.ToInt32(fgList[fgList.Row, 15]);
                        //if (iOldStatus >= 0) tabClientData_Mode1.Visible = false;
                        //else if (iOldStatus == -1) tabClientData_Mode1.Visible = true;
                    }


                if (iOldTipos != Convert.ToInt32(fgList[fgList.Row, 13]))
                {
                    iOldTipos = Convert.ToInt32(fgList[fgList.Row, 13]);

                    switch (iOldTipos)
                    {
                        case 1:                 // 1 - F(Ph)ysical Person
                            sMeTitle = Global.GetLabel("customers_list");
                            tsbFilter.Visible = false;
                            ucCD.panFP.Visible = true;
                            ucCD.panNP.Visible = false;
                            clrBackground = Color.DarkSalmon;
                            sClientFullName = (ucCD.txtFPSurname.Text.Trim() + " " + ucCD.txtFPFirstname.Text.Trim()).Trim();
                            iFormat = 1;
                            break;
                        case 2:                  // 2 - Nomical Person
                            sMeTitle = Global.GetLabel("prospective_clients");
                            tsbFilter.Visible = true;
                            ucCD.panFP.Visible = false;
                            ucCD.panNP.Visible = true;
                            clrBackground = Color.DarkSeaGreen;
                            sClientFullName = ucCD.txtNPTitle.Text.Trim();
                            iFormat = 2;
                            break;
                    }
                }

                switch (iOldTipos)
                {
                    case 1:                 //1 - F(Ph)ysical Person
                        sOldValues = ucCD.txtFPSurname.Text + "~" + ucCD.txtFPFirstname.Text + "~" + ucCD.txtFPFatherSurname.Text + "~" +
                            ucCD.txtFPFatherFirstname.Text + "~" + ucCD.txtFPMotherSurname.Text + "~" + ucCD.txtFPMotherFirstname.Text + "~" +
                            ucCD.txtFPSyzygosSurname.Text + "~" + ucCD.txtFPSyzygosFirstname.Text + "~" + "" + "~" +
                            ucCD.cmbFPDivision.Text + "~" + ucCD.cmbFPSpecials.Text + "~" + ucCD.dFPDoB.Value + "~" +
                            ucCD.cmbFPCitizen.Text + "~" + ucCD.cmbFPSex.Text + "~" + ucCD.cmbFPCategory.Text + "~" +
                            ucCD.cmbFPStatus.Text + "~" + ucCD.txtFPADT.Text + "~" + ucCD.txtFPExpireDate.Text + "~" +
                            ucCD.txtFPPolice.Text + "~" + ucCD.txtFPDOY.Text + "~" + ucCD.txtFPAFM.Text + "~" +
                            ucCD.cmbFPCountryTaxes.Text + "~" + ucCD.txtFPAddress.Text + "~" + "" + "~" +
                            ucCD.txtFPCity.Text + "~" + ucCD.txtFPZip.Text + "~" + ucCD.cmbFPXora.Text + "~" +
                            ucCD.txtFPTel.Text + "~" + ucCD.txtFPFax.Text + "~" + ucCD.txtFPMobile.Text + "~" +
                            ucCD.txtFPEMail.Text + "~" + ucCD.txtFPBornPlace.Text + "~" + ucCD.cmbFPOccupation.Text + "~" +
                            "" + "~" + ucCD.cmbFPRisk.Text + "~" + ucCD.txtFPAMKA.Text + "~" +
                            ucCD.cmbFPConnectionMethod.Text + "~" + ucCD.chkFPSMS.Checked + "~" + "" + "~" + "" + "~" +
                            txtNotes.Text + "~" + txtFileName.Text + "~" + cmbDocTypes.Text + "~" + ucCD.cmbFPFamilyStatus.Text + "~" +
                            ucCD.txtFPCompany.Text + "~" + ucCD.txtFPSumAxion.Text + "~" + ucCD.txtFPSumAkiniton.Text + "~" +
                            ucCD.chkFPInfluenceCenter.Checked + "~" + ucCD.chkFPIntroducer.Checked + "~" + ucCD.lblFPSpecialCategory.Text + "~" +
                            ucCD.txtFPMerida.Text + "~" + ucCD.txtFPLogAxion.Text + "~";
                        break;
                    case 2:                  // 2 - Nomical Person
                        sOldValues = ucCD.txtNPTitle.Text + "~" + ucCD.txtNPDiakritikosTitlos.Text + "~" + ucCD.txtNPEdra.Text + "~" +
                            ucCD.txtNPMorfi.Text + "~" + ucCD.cmbNPCategory.Text + "~" + ucCD.cmbNPDivision.Text + "~" +
                            ucCD.cmbNPStatus.Text + "~" + ucCD.txtNPAM.Text + "~" + ucCD.txtNPExpireDate.Text + "~" +
                            ucCD.txtNPArmodiaArxi.Text + "~" + ucCD.txtNPDOY.Text + "~" + ucCD.txtNPAFM.Text + "~" +
                            ucCD.cmbNPNation.Text + "~" + ucCD.cmbNPCountryTaxes.Text + "~" +
                            ucCD.txtNPAddress.Text + "~" + "" + "~" + ucCD.txtNPCity.Text + "~" + ucCD.txtNPZip.Text + "~" +
                            ucCD.cmbNPCountry.Text + "~" + ucCD.txtNPTel.Text + "~" + ucCD.txtNPFax.Text + "~" +
                            ucCD.txtNPMobile.Text + "~" + ucCD.txtNPEMail.Text + "~" + "" + "~" +
                            ucCD.cmbNPRisk.Text + "~" + ucCD.cmbNPBrunches.Text + "~" + ucCD.cmbNPConnectionMethod.Text + "~" +
                            ucCD.chkNPSMS.Checked + "~" + ucCD.txtNPFPA.Text + "~" + "" + "~" + "" + "~" + "" + "~" + "" + "~" + "" +
                            "~" + "" + "~" + "" + "~" + "" + "~" + "" + "~" + "" + "~" + txtNotes.Text + "~" + txtFileName.Text + "~" + "" + "~" + "" + "~" +
                            "" + "~" + ucCD.txtNPSumAxion.Text + "~" + ucCD.txtNPSumAkiniton.Text + "~" +
                            ucCD.chkNPInfluenceCenter.Checked + "~" + ucCD.chkNPIntroducer.Checked + "~" + ucCD.lblNPSpecialCategory.Text + "~" +
                            ucCD.txtNPMerida.Text + "~" + ucCD.txtNPLogAxion.Text + "~";
                        break;
                }

                for (i = 0; i <= ucCD.tabClientData.TabPages.Count - 1; i++)
                    ucCD.tabClientData.TabPages[i].BackColor = clrBackground;
            }
        }
        private void AddDocument()
        {
            sFullFileName = sFullFileName + "";
            if (sFullFileName != "")
            {
                //--- this file is personal file, so Contract_ID = 0, Code = "" ----
                clsClientsDocFiles klsClientsDocFiles = new clsClientsDocFiles();
                klsClientsDocFiles.PreContract_ID = 0;
                klsClientsDocFiles.Contract_ID = 0;
                klsClientsDocFiles.Client_ID = iID;
                klsClientsDocFiles.ClientName = sClientFullName;
                klsClientsDocFiles.ContractCode = "";
                klsClientsDocFiles.DocTypes = Convert.ToInt32(cmbDocTypes.SelectedValue);
                klsClientsDocFiles.DMS_Files_ID = 0;
                klsClientsDocFiles.OldFileName = "";
                klsClientsDocFiles.NewFileName = txtFileName.Text;
                klsClientsDocFiles.FullFileName = sFullFileName;
                klsClientsDocFiles.DateIns = DateTime.Now;
                klsClientsDocFiles.User_ID = Global.User_ID;
                klsClientsDocFiles.Status = 2;                                           // 2 - document confirmed
                iDocFiles_ID = klsClientsDocFiles.InsertRecord();

            }
        }
        private void EmptyData()
        {
            ucCD.txtFPSurname.Text = "";
            ucCD.txtFPFirstname.Text = "";
            ucCD.txtFPSurnameEng.Text = "";
            ucCD.txtFPFirstnameEng.Text = "";
            ucCD.txtFPFatherSurname.Text = "";
            ucCD.txtFPFatherFirstname.Text = "";
            ucCD.txtFPMotherSurname.Text = "";
            ucCD.txtFPMotherFirstname.Text = "";
            ucCD.txtFPSyzygosSurname.Text = "";
            ucCD.txtFPSyzygosFirstname.Text = "";
            ucCD.cmbFPSpecials.SelectedValue = 0;
            ucCD.cmbFPOccupation.SelectedValue = 0;
            ucCD.txtFPCompany.Text = "";
            ucCD.txtFPCompanyDescription.Text = "";
            ucCD.txtFPJobPosition.Text = "";
            ucCD.txtFPJobAddress.Text = "";
            ucCD.txtFPJobCity.Text = "";
            ucCD.txtFPJobZip.Text = "";
            ucCD.cmbFPJobCountry_ID.SelectedValue = 0;
            ucCD.txtFPJobTel.Text = "";
            ucCD.txtFPJobMobile.Text = "";
            ucCD.txtFPJobEMail.Text = "";
            ucCD.txtFPJobURL.Text = "";
            ucCD.dFPDoB.Value = Convert.ToDateTime("1900/01/01");
            ucCD.txtFPBornPlace.Text = "";
            ucCD.cmbFPCitizen.Text = "";
            ucCD.cmbFPSex.Text = "ΑΡ";
            ucCD.cmbFPFamilyStatus.SelectedIndex = 0;
            ucCD.cmbFPCategory.SelectedIndex = 0;           // 0 - IDIOTHS
            ucCD.cmbFPDivision.SelectedValue = 1;           // 1 - Thessaloniki
            ucCD.chkFPInfluenceCenter.Checked = false;
            ucCD.chkFPIntroducer.Checked = false;
            ucCD.chkFPRepresentPerson.Checked = false;
            ucCD.cmbFPStatus.SelectedValue = 1;
            ucCD.txtFPADT.Text = "";
            ucCD.txtFPExpireDate.Text = "";
            ucCD.txtFPPolice.Text = "";
            ucCD.txtFPPassport.Text = "";
            ucCD.txtFPPassport_ExpireDate.Text = "";
            ucCD.txtFPPassport_Police.Text = "";
            ucCD.txtFPAFM.Text = "";
            ucCD.txtFPDOY.Text = "";
            ucCD.txtFPAFM2.Text = "";
            ucCD.txtFPDOY2.Text = "";
            ucCD.txtFPFPA.Text = "24";
            ucCD.txtFPAMKA.Text = "";
            ucCD.cmbFPCountryTaxes.Text = "";
            ucCD.txtFPAddress.Text = "";
            ucCD.txtFPCity.Text = "";
            ucCD.txtFPZip.Text = "";
            ucCD.cmbFPXora.Text = "";
            ucCD.txtFPTel.Text = "";
            ucCD.txtFPFax.Text = "";
            ucCD.txtFPMobile.Text = "";
            ucCD.txtFPEMail.Text = "";
            ucCD.txtFPMerida.Text = "";
            ucCD.txtFPLogAxion.Text = "";
            ucCD.lblFPSpecialCategory.Text = "";
            ucCD.txtFPSumAxion.Text = "0";
            ucCD.txtFPSumAkiniton.Text = "0";
            ucCD.cmbFPRisk.SelectedIndex = 0;
            ucCD.txtFPExpireDate.Text = "";

            ucCD.txtNPTitle.Text = "";
            ucCD.txtNPTitleEng.Text = "";
            ucCD.txtNPDiakritikosTitlos.Text = "";
            ucCD.txtNPEdra.Text = "";
            ucCD.txtNPMorfi.Text = "";
            ucCD.cmbNPNation.Text = "";
            ucCD.cmbNPCategory.SelectedIndex = 1;           // 1 - ETAIRIA
            ucCD.cmbNPDivision.SelectedValue = 1;           // 1 - Thessaloniki
            ucCD.cmbNPStatus.SelectedValue = 1;
            ucCD.txtNPLEI.Text = "";
            ucCD.txtNPAM.Text = "";
            ucCD.txtNPExpireDate.Text = "";
            ucCD.txtNPArmodiaArxi.Text = "";
            ucCD.txtNPDOY.Text = "";
            ucCD.txtNPAFM.Text = "";
            ucCD.cmbNPCountryTaxes.Text = "";
            ucCD.txtNPAddress.Text = "";
            ucCD.txtNPCity.Text = "";
            ucCD.txtNPZip.Text = "";
            ucCD.cmbNPCountry.Text = "";
            ucCD.txtNPTel.Text = "";
            ucCD.txtNPFax.Text = "";
            ucCD.txtNPMobile.Text = "";
            ucCD.txtNPEMail.Text = "";
            ucCD.txtNPFPA.Text = "24";
            ucCD.txtNPMerida.Text = "";
            ucCD.txtNPLogAxion.Text = "";
            ucCD.lblNPSpecialCategory.Text = "";
            ucCD.txtNPSumAxion.Text = "0";
            ucCD.txtNPSumAkiniton.Text = "0";
            ucCD.cmbNPRisk.SelectedIndex = 0;
            ucCD.txtNPExpireDate.Text = "";
            ucCD.txtNotes.Text = "";
            ucCD.txtConne.Text = "";

            ucCD.cmbUser2.SelectedValue = 0;
            ucCD.fgNeeds.Rows.Count = 1;
            ucCD.fgRandevouz.Rows.Count = 1;
            ucCD.fgCorporateEvents.Rows.Count = 1;
            ucCD.fgDocFiles.Rows.Count = 1;
            ucCD.fgBankAccounts.Rows.Count = 1;
            ucCD.fgInfluenceCenters.Rows.Count = 1;
            ucCD.fgDependentsList.Rows.Count = 1;

            ucCD.fgStep2.Rows.Count = 1;
            ucCD.fgStep3.Rows.Count = 1;
            ucCD.fgStep4.Rows.Count = 1;
            ucCD.fgStep5.Rows.Count = 1;
            ucCD.fgStep6.Rows.Count = 1;
            ucCD.fgStep7.Rows.Count = 1;
            ucCD.fgPackages.Rows.Count = 1;
        }
        private void AddRecord2LogSxedio(int iClient_ID, string sClient_Name)
        {
            /*
                 int i = 0;
        With comm
            .Connection = cn
            .CommandText = "log_InsertLogSxedio"
            .CommandType = CommandType.StoredProcedure
        End With
        comm.Parameters.Clear()
        prmSQL = comm.Parameters.AddWithValue("@ID", Nothing)
        prmSQL.Direction = ParameterDirection.Output
        prmSQL.SqlDbType = SqlDbType.Int

        prmSQL = comm.Parameters.AddWithValue("@L1", "30")
        prmSQL = comm.Parameters.AddWithValue("@L2", "00")
        prmSQL = comm.Parameters.AddWithValue("@L3", "00")
        prmSQL = comm.Parameters.AddWithValue("@L4", iClient_ID.ToString("0000"))
        prmSQL = comm.Parameters.AddWithValue("@Title", sClient_Name)
        comm.ExecuteNonQuery()
        i = comm.Parameters("@ID").Value

        With comm
            .Connection = cn
            .CommandText = "sp_EditClient_LogSxedio_ID"
            .CommandType = CommandType.StoredProcedure
        End With
        comm.Parameters.Clear()
        prmSQL = comm.Parameters.AddWithValue("@ID", iClient_ID)
        prmSQL = comm.Parameters.AddWithValue("@LogSxedio_ID", i)
        comm.ExecuteNonQuery()
            */

        }
        public int Mode { get { return this.iMode; } set { this.iMode = value; } }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
