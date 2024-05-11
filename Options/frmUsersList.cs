using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Core;


namespace Options
{
    public partial class frmUsersList : Form
    {
        int i, iID, iTipos, iRow, iChange, iRightsLevel, iAction;
        string sTemp, sUserFullName, sExtra;
        SortedList lstStatus = new SortedList();
        bool bCheckList;
        public frmUsersList()
        {
            InitializeComponent();
            panFysiko.Left = 2;
            panFysiko.Top = 2;
            panNomiko.Left = 2;
            panNomiko.Top = 2;
            panTipos.Left = 6;
            panTipos.Top = 36;
            panCopy.Left = 74;
            panCopy.Top = 48;
        }
        private void frmUsersList_Load(object sender, EventArgs e)
        {
            bCheckList = false;
            iChange = 0;
            iTipos = 1;

            lstStatus.Clear();
            lstStatus.Add(0, "Μη Διαθέσιμο");
            lstStatus.Add(1, "Μονο Ανάγνωση");
            lstStatus.Add(2, "Πλήρης");

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString(Global.GridStyle);
            fgList.RowColChange += new EventHandler(fgList_RowColChange);

            //------- fgMenusItems ----------------------------
            fgMenus.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgMenus.Styles.ParseString(Global.GridStyle);
            fgMenus.DrawMode = DrawModeEnum.OwnerDraw;
            fgMenus.Cols[1].DataMap = lstStatus;
            fgMenus.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(fgMenus_CellChanged);

            //------- fgAlerts ----------------------------
            fgAlerts.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgAlerts.Styles.ParseString(Global.GridStyle);

            //-------------- Define Divisions List ------------------
            cmbDivision.DataSource = Global.dtDivisions.Copy();
            cmbDivision.DisplayMember = "Title";
            cmbDivision.ValueMember = "ID";

            //-------------- Define Countries List ------------------
            cmbXora.DataSource = Global.dtCountries.Copy();
            cmbXora.DisplayMember = "Title";
            cmbXora.ValueMember = "ID";

            //-------------- Define Countries List ------------------
            cmbNomXora.DataSource = Global.dtCountries.Copy();
            cmbNomXora.DisplayMember = "Title";
            cmbNomXora.ValueMember = "ID";

            //-------------- Define CountryTaxes List ------------------
            cmbCountryTaxes.DataSource = Global.dtCountries.Copy();
            cmbCountryTaxes.DisplayMember = "Title";
            cmbCountryTaxes.ValueMember = "ID";

            //-------------- Define NomCountryTaxes List ------------------
            cmbNomCountryTaxes.DataSource = Global.dtCountries.Copy();
            cmbNomCountryTaxes.DisplayMember = "Title";
            cmbNomCountryTaxes.ValueMember = "ID";

            //----------------Define ClientsFilters ----------------------
            clsSystem System = new clsSystem();
            System.GetList_ClientsFilters();
            cmbClientsFilters.DataSource = System.List.Copy();
            cmbClientsFilters.DisplayMember = "Title";
            cmbClientsFilters.ValueMember = "ID";
            cmbClientsFilters.SelectedValue = Global.ClientsFilter_ID;

            //-------------- Define Users List ------------------
            cmbUsers.DataSource = Global.dtUserList.Copy().DefaultView;
            cmbUsers.DisplayMember = "Title";
            cmbUsers.ValueMember = "ID";

            //----------------Define UsersDocTypes ----------------------
            System.GetList_UsersDocTypes();
            cmbDocTypes.DataSource = System.List.Copy();
            cmbDocTypes.DisplayMember = "Title";
            cmbDocTypes.ValueMember = "ID";


            DefineList();
            bCheckList = true;
            if (fgList.Rows.Count > 1)
            {
                fgList.Focus();
                ShowRecord();
            }

            if (iRightsLevel == 1) {
                tsbAdd.Enabled = false;
                tsbDelete.Enabled = false;
                tsbEdit.Enabled = false;                
            }
            if (Global.UserStatus != 1) {                                         // isn't Superuser
                tabData.TabPages.Remove(tpRights);
                tabData.TabPages.Remove(tpFilters);
                tabData.TabPages.Remove(tpDocs);
                lblEMail_Password.Visible = false;
                txtEMail_Password.Visible = false;
                lblNomEMail_Password.Visible = false;
                txtNomEMail_Password.Visible = false;
            }
            tsbSave.Enabled = false;
        }
        private void DefineList()
        {
            bCheckList = false;
            fgList.Redraw = false;
            fgList.Rows.Count = 1;


            foreach (DataRow dtRow in Global.dtUserList.Rows)
                if (Convert.ToInt32(dtRow["ID"]) != 0) 
                        fgList.AddItem(dtRow["Title"] + "\t" + dtRow["ID"] + "\t" + dtRow["Tipos"]);

            fgList.Redraw = true;
            bCheckList = true;

            if (fgList.Rows.Count > 1)
            {
                fgList.Focus();
                ShowRecord();
                iAction = 1;
            }
        }
        private void fgList_RowColChange(object sender, EventArgs e)
        {
            iAction = 1;
            if (fgList.Row > 0) {
                iChange = 0;

                if (Convert.ToInt32(fgList[fgList.Row, "ID"]) == Global.User_ID || Global.UserStatus == 1)
                {
                    lblEMail_Password.Visible = true;
                    txtEMail_Password.Visible = true;
                    tsbEdit.Enabled = true;
                }
                else {
                    lblEMail_Password.Visible = false;
                    txtEMail_Password.Visible = false;
                    tsbEdit.Enabled = false;
                }

                for (i = 1; iAction <= fgAlerts.Rows.Count - 1; iAction++) fgAlerts[i, 0] = false;

                ShowRecord();
            }
        }
        private void ShowRecord()
        {
            if (bCheckList)
            {
                iID = Convert.ToInt32(fgList[fgList.Row, 1]);
                iTipos = Convert.ToInt32(fgList[fgList.Row, 2]);

                if (iTipos == 1)
                {
                    panFysiko.Visible = true;
                    panNomiko.Visible = false;
                }
                else
                {
                    panFysiko.Visible = false;
                    panNomiko.Visible = true;
                }

                clsUsers User = new clsUsers();
                User.Record_ID = iID;
                User.GetRecord();

                if (Convert.ToInt16(fgList[fgList.Row, 2]) == 1)
                {
                    tpGeneral.BackColor = panFysiko.BackColor;
                    panFysiko.Visible = true;
                    panNomiko.Visible = false;

                    txtSurname.Text = User.Surname + "";
                    txtFirstname.Text = User.Firstname + "";
                    txtSurnameEng.Text = User.SurnameEng + "";
                    txtFirstnameEng.Text = User.FirstnameEng + "";
                    sUserFullName = (txtSurname.Text + " " + txtFirstname.Text).Trim();
                    txtFather.Text = User.Father + "";
                    txtMother.Text = User.Mother + "";
                    txtADT.Text = User.ADT + "";
                    txtIssueDate.Text = User.IssueDate + "";
                    txtPolice.Text = User.PoliceDepart + "";

                    dDoB.Value = Convert.ToDateTime(User.DoB);
                    txtFamily.Text = User.Family + "";
                    cmbSex.Text = User.Sex + "";
                    txtChildren.Text = User.Children + "";

                    txtAFM.Text = User.AFM + "";
                    txtDOY.Text = User.DOY + "";
                    cmbCountryTaxes.SelectedValue = User.CountryTax_ID;
                    txtAddress.Text = User.Adress + "";
                    txtCity.Text = User.City + "";
                    txtZip.Text = User.TK + "";
                    txtTel.Text = User.Tel + "";
                    txtFax.Text = User.Fax + "";
                    txtMobile.Text = User.Mobile + "";
                    txtEMail.Text = User.EMail + "";
                    txtEMail_Username.Text = User.EMail_Username + "";
                    txtEMail_Password.Text = User.EMail_Password + "";
                    txtEducation.Text = User.Education + "";
                    txtRelations.Text = User.Relation + "";
                    dStart.Value = Convert.ToDateTime(User.StartDate);
                    txtDuration.Text = User.Duration + "";
                    txtPosition.Text = User.Position + "";
                    txtEidikotita.Text = User.Eidikotita + "";
                    txtBank.Text = User.Bank + "";
                    txtBankAccount.Text = User.BankAccount + "";
                    cmbDMSAccess.SelectedIndex = User.DMSAccess;
                    txtPasword.Text = User.Pasword + "";
                    cmbUserStatus.SelectedIndex = User.Status;
                    cmbDivision.SelectedValue = User.Division;

                    txtPhoto.Text = User.Photo + "";

                    chkDivisionFilter.Checked = (Convert.ToInt16(User.DivisionFilter) == 1 ? true : false);
                    cmbAktive.SelectedIndex = Convert.ToInt16(User.Aktive);
                }
                else
                {
                    tpGeneral.BackColor = panNomiko.BackColor;
                    panFysiko.Visible = false;
                    panNomiko.Visible = true;

                    txtNomTitle.Text = User.Surname + "";
                    sUserFullName = User.Surname + "";
                    txtNomTitleEng.Text = User.SurnameEng + "";
                    txtNomDiakr.Text = User.Firstname + "";
                    txtNomKatastat.Text = User.Father + "";
                    txtNomMorfi.Text = User.Mother + "";
                    txtNomSkopos.Text = User.ADT + "";
                    txtNomAM.Text = User.IssueDate + "";
                    txtNomArxi.Text = User.PoliceDepart + "";
                    dNomKatax.Value = Convert.ToDateTime(User.DoB);
                    txtNomAFM.Text = User.AFM + "";
                    txtNomDOY.Text = User.DOY + "";
                    cmbNomXora.SelectedValue = User.Country_ID;
                    txtNomAddress.Text = User.Adress + "";
                    txtNomCity.Text = User.City + "";
                    txtNomZip.Text = User.TK + "";
                    cmbNomCountryTaxes.SelectedValue = User.CountryTax_ID;
                    txtNomTel.Text = User.Tel + "";
                    txtNomFax.Text = User.Fax + "";
                    txtNomMobile.Text = User.Mobile + "";
                    txtNomEMail.Text = User.EMail + "";
                    txtNomEMail_Username.Text = User.EMail_Username + "";
                    txtNomEMail_Password.Text = User.EMail_Password + "";
                    txtNomRelations.Text = User.Relation + "";
                    dNomStart.Value = Convert.ToDateTime(User.StartDate);
                    txtNomDuration.Text = User.Duration + "";
                    txtNomPosition.Text = User.Position + "";
                    txtNomEidikotita.Text = User.Eidikotita + "";
                    txtNomBank.Text = User.Bank + "";
                    txtNomBankAccount.Text = User.BankAccount + "";

                    cmbDMSAccess.SelectedIndex = User.DMSAccess;
                    txtPasword.Text = User.Pasword + "";
                    cmbUserStatus.SelectedIndex = User.Status;
                    cmbDivision.SelectedValue = User.Division;
                }


                chkDivisionFilter.Checked = (Convert.ToInt16(User.DivisionFilter) == 1 ? true : false);
                cmbAktive.SelectedIndex = Convert.ToInt16(User.Aktive);
                cmbClientsFilters.SelectedValue = User.ClientsFilter_ID;
                txtDefaultFolder.Text = User.DefaultFolder + "";
                txtUploadFolder.Text = User.UploadFolder + "";
                txtDMSTransferPoint.Text = User.DMSTransferPoint + "";
                txtDMSDownloadPath.Text = User.DMSDownloadPath + "";
                chkChief.Checked = (Convert.ToInt16(User.Chief) == 1 ? true : false);
                chkRM.Checked = (Convert.ToInt16(User.RM) == 1 ? true : false);
                chkSender.Checked = (Convert.ToInt16(User.Sender) == 1 ? true : false);
                chkIntroducer.Checked = (Convert.ToInt16(User.Introducer) == 1 ? true : false);
                chkDiaxiristis.Checked = (Convert.ToInt16(User.Diaxiristis) == 1 ? true : false);
                chkSender.Checked = (Convert.ToInt16(User.Sender) == 1 ? true : false);                
                dDiax_DateStart.Value = User.Diax_DateStart;
                dDiax_DateFinish.Value = User.Diax_DateFinish;
                cmbClientsRequests_Status.SelectedIndex = User.ClientsRequests_Status;

                //-------------- Define USERS MENUS  ------------------
                fgMenus.Redraw = false;
                fgMenus.Rows.Count = 1;
                User.Record_ID = iID;
                User.GetMenu();
                foreach (DataRow dtRow in User.List.Rows)
                   fgMenus.AddItem(dtRow["TitleGr"] + "\t" + lstStatus[dtRow["Status"]]  + "\t" + dtRow["Extra"] + "\t" + dtRow["ID"] + "\t" + dtRow["Status"] + "\t" + 
                                   dtRow["Menu_ID"] + "\t" + dtRow["MenuGroup_ID"] + "\t" + dtRow["MenuView_ID"] + "\t" + dtRow["Extra_Exists"]);                
                fgMenus.Redraw = true;

                //-------------- Define USERS DOCUMENTS  ------------------
                fgDocFiles.Redraw = false;
                fgDocFiles.Rows.Count = 1;
                User.Record_ID = iID;
                User.GetUser_Documents();
                foreach (DataRow dtRow in User.List.Rows)
                    fgDocFiles.AddItem(dtRow["DateIns"] + "\t" + dtRow["DocType_Title"] + "\t" + dtRow["FileName"] + "\t" + dtRow["ID"] + "\t" + dtRow["DocType_ID"] + "\t" + "");
                fgDocFiles.Redraw = true;

                //-------------- Define USERS_ALERTS  ------------------
                fgAlerts.Redraw = false;
                fgAlerts.Rows.Count = 1;
                fgAlerts.AddItem(false + "\t" + "Λήξη έκπτωσης προμήθειας" + "\t" + "0" + "\t" + "1");
                fgAlerts.AddItem(false + "\t" + "Γρήγορη καταχώρηση προϊοντων" + "\t" + "0" + "\t" + "2");
                fgAlerts.AddItem(false + "\t" + "Unknown CIF or CashAccounts" + "\t" + "0" + "\t" + "3");
                fgAlerts.AddItem(false + "\t" + "Unknown ISIN" + "\t" + "0" + "\t" + "4");

                User.Record_ID = iID;
                User.GetUser_Alerts();
                foreach (DataRow dtRow in User.List.Rows)
                    for (i = 1; i <= fgAlerts.Rows.Count - 1; i++)
                        if (Convert.ToInt16(fgAlerts[i, 3]) == Convert.ToInt16(dtRow["AlertType"]))
                        {
                            if (Convert.ToInt16(dtRow["OK"]) == 1) fgAlerts[i, 0] = true;
                            else                                   fgAlerts[i, 0] = false;

                            fgAlerts[i, 2] = Convert.ToInt16(dtRow["ID"]);
                        }

                fgAlerts.Redraw = true;
            }
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            tsbSave.Enabled = true;
        }

        private void tsbCopy_Click(object sender, EventArgs e)
        {
            cmbUsers.SelectedValue = 0;
            panCopy.Visible = true;
        }

        private void btnOK_Copy_Click(object sender, EventArgs e)
        {
            clsUsers User = new clsUsers();
            User.Record_ID = Convert.ToInt32(cmbUsers.SelectedValue);
            User.GetMenu();
            foreach (DataRow dtRow in User.List.Rows)
            {
                sTemp = Convert.ToString(dtRow["Menu_ID"]);
                i = fgMenus.FindRow(sTemp, 1, 5, false);
                if (i > 0) {
                    fgMenus[i, "Status"] = dtRow["Status"];
                    fgMenus[i, "Extra"] = dtRow["Extra"];
                }
            }
            panCopy.Visible = false;
        }

        private void btnCancel_Copy_Click(object sender, EventArgs e)
        {
            panCopy.Visible = false;
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            panTipos.Visible = true;
        }

        private void btnOK_Tipos_Click(object sender, EventArgs e)
        {
            iAction = 0;
            EmptyDetails();
            tsbSave.Enabled = true;
            panTipos.Visible = false;
            txtSurname.Focus();
        }

        private void btnCancel_Tipos_Click(object sender, EventArgs e)
        {
            panTipos.Visible = false;
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (txtSurname.Text.Length != 0 || txtNomTitle.Text.Length != 0)
            {
                iRow = fgList.Row;
                sTemp = "";

                clsUsers User = new clsUsers();
                if (iAction == 0) {                                  // ==0 - ADD Mode  
                    User.Type = iTipos;
                }
                else {                                               // !=0 - EDIT Mode
                    User.Record_ID = iID;
                    User.GetRecord();
                }

                if (iTipos == 1) {
                    User.Surname = txtSurname.Text;
                    User.SurnameEng = txtSurnameEng.Text;       
                    User.Firstname = txtFirstname.Text;
                    User.FirstnameEng = txtFirstnameEng.Text;  
                    User.Father = txtFather.Text;
                    User.Mother = txtMother.Text;
                    User.Family = txtFamily.Text;
                    User.Children = txtChildren.Text;
                    User.Sex = cmbSex.Text;
                    User.DoB = dDoB.Value;
                    User.ADT = txtADT.Text;
                    User.IssueDate = txtIssueDate.Text;
                    User.PoliceDepart = txtPolice.Text;
                    User.Adress = txtAddress.Text;
                    User.City = txtCity.Text;
                    User.Country_ID = Convert.ToInt32(cmbXora.SelectedValue);
                    User.TK = txtZip.Text;
                    User.CountryTax_ID = Convert.ToInt32(cmbCountryTaxes.SelectedValue);
                    User.AFM = txtAFM.Text;
                    User.DOY = txtDOY.Text;
                    User.Tel = txtTel.Text;
                    User.Fax = txtFax.Text;
                    User.Mobile = txtMobile.Text;
                    User.EMail = txtEMail.Text;
                    User.EMail_Username = txtEMail_Username.Text;
                    User.EMail_Password = txtEMail_Password.Text;
                    User.Education = txtEducation.Text;
                    User.Certifikates = txtCertificates.Text;
                    User.Eidikotita = txtEidikotita.Text;
                    User.Duration = txtDuration.Text;
                    User.Position = txtPosition.Text;
                    User.Location = 0; //@@@@@@@@@@@@@@@@@@@
                    User.Relation = txtRelations.Text;
                    User.Pasword = txtPasword.Text;
                    User.DMSAccess = cmbDMSAccess.SelectedIndex;
                    User.Language = 1; //@@@@@@@@@@@@@@@@@@@
                    User.Division = Convert.ToInt32(cmbDivision.SelectedValue);
                    User.Bank = txtBank.Text;
                    User.BankAccount = txtBankAccount.Text;
                    User.DefaultFolder = txtDefaultFolder.Text;
                    User.UploadFolder = txtUploadFolder.Text;
                    User.DMSTransferPoint = txtDMSTransferPoint.Text;
                    User.DMSDownloadPath = txtDMSDownloadPath.Text;
                    User.Chief = (chkChief.Checked ? 1 : 0);
                    User.RM = (chkRM.Checked ? 1 : 0);
                    User.Sender = (chkSender.Checked ? 1 : 0);
                    User.Introducer = (chkIntroducer.Checked ? 1 : 0);
                    User.Diaxiristis = (chkDiaxiristis.Checked ? 1 : 0);
                    User.Diax_DateStart = dDiax_DateStart.Value;
                    User.Diax_DateFinish = dDiax_DateFinish.Value;
                    User.DivisionFilter = (chkDivisionFilter.Checked ? 1 : 0);
                    User.ClientsRequests_Status = cmbClientsRequests_Status.SelectedIndex;
                    User.StartDate = dStart.Value;
                    User.ClientsFilter_ID = Convert.ToInt32(cmbClientsFilters.SelectedValue);
                    User.Photo = txtPhoto.Text;
                    User.Status = cmbUserStatus.SelectedIndex;
                    User.Aktive = cmbAktive.SelectedIndex;
                }
                else
                {
                    User.Surname = txtNomTitle.Text;
                    User.SurnameEng = txtNomTitleEng.Text;      
                    User.Firstname = txtNomDiakr.Text;
                    User.FirstnameEng = txtNomDiakr.Text;   //@@@@@@@@@@@@@@@@
                    User.Father = txtNomKatastat.Text;
                    User.Mother = txtNomMorfi.Text;
                    User.Family = txtFamily.Text;
                    User.Children = "";
                    User.Sex = "";
                    User.DoB = dNomKatax.Value;
                    User.ADT = txtNomSkopos.Text;
                    User.IssueDate = txtNomAM.Text;
                    User.PoliceDepart = txtNomArxi.Text;
                    User.Adress = txtNomAddress.Text;
                    User.City = txtNomCity.Text;                   
                    User.Country_ID = Convert.ToInt32(cmbNomXora.SelectedValue);
                    User.TK = txtNomZip.Text;
                    User.CountryTax_ID = Convert.ToInt32(cmbNomCountryTaxes.SelectedValue);
                    User.AFM = txtNomAFM.Text;
                    User.DOY = txtNomDOY.Text; 
                    User.Tel = txtNomTel.Text;
                    User.Fax = txtNomFax.Text;
                    User.Mobile = txtNomMobile.Text;
                    User.EMail = txtNomEMail.Text;
                    User.EMail_Username = txtNomEMail_Username.Text;
                    User.EMail_Password = txtNomEMail_Password.Text;
                    User.Education = txtNomRelations.Text;
                    User.Certifikates = "";
                    User.Eidikotita = txtNomEidikotita.Text;
                    User.Duration = txtNomDuration.Text;
                    User.Position = txtNomPosition.Text;
                    User.Location = 0;
                    User.Relation = "";
                    User.Pasword = txtPasword.Text;
                    User.DMSAccess = cmbDMSAccess.SelectedIndex;
                    User.Language = 0;
                    User.Division = Convert.ToInt32(cmbDivision.SelectedValue);
                    User.Bank = txtNomBank.Text;
                    User.BankAccount = txtNomBankAccount.Text;
                    User.DMSTransferPoint = txtDMSTransferPoint.Text;
                    User.DMSDownloadPath = txtDMSDownloadPath.Text;
                    User.Chief = (chkChief.Checked ? 1 : 0);
                    User.RM = (chkRM.Checked ? 1 : 0);
                    User.Sender = (chkSender.Checked ? 1 : 0);
                    User.Introducer = (chkIntroducer.Checked ? 1 : 0);
                    User.Diaxiristis = (chkDiaxiristis.Checked ? 1 : 0);
                    User.Diax_DateStart = dDiax_DateStart.Value;
                    User.Diax_DateFinish = dDiax_DateFinish.Value;
                    User.ClientsRequests_Status = 0;
                    User.DivisionFilter = (chkNomDivisionFilter.Checked ? 1 : 0);
                    User.StartDate = Convert.ToDateTime(dNomStart.Value);
                    User.Photo = "";
                    User.Status = cmbUserStatus.SelectedIndex;
                    User.Aktive = cmbNomAktive.SelectedIndex;
                }

                if (iAction == 0) {                              // 0 - ADD Mode
                    iID = User.InsertRecord();
                }
                else  {
                    User.EditRecord();

                    //if (sUserFullName.Trim() != (txtSurname.Text + " " + txtFirstname.Text).Trim())
                        //Global.DMS_RenameFolderName("Company/Users/" + sUserFullName.Trim(), (txtSurname.Text + " " + txtFirstname.Text).Trim());
                }
                 

                if (iAction != 0) {
                    clsMenusUsers MenusUser = new clsMenusUsers();
                    for (i = 1; i <= fgMenus.Rows.Count - 1; i++) {                        
                        if (Convert.ToInt32(fgMenus[i,"ID"]) == 0) {
                            MenusUser.User_ID = iID;
                            MenusUser.Menu_ID = Convert.ToInt32(fgMenus[i, "Menu_ID"]);
                            MenusUser.Status = Convert.ToInt32(fgMenus[i, "Status_ID"]);
                            MenusUser.Extra = fgMenus[i, "Extra"] + "";
                            MenusUser.InsertRecord();
                        }
                        else {
                            MenusUser.User_ID = iID;
                            MenusUser.Record_ID = Convert.ToInt32(fgMenus[i, "ID"]);
                            MenusUser.Menu_ID = Convert.ToInt32(fgMenus[i, "Menu_ID"]);
                            MenusUser.Status = Convert.ToInt32(fgMenus[i, "Status_ID"]);
                            MenusUser.Extra = fgMenus[i, "Extra"] + "";
                            MenusUser.EditRecord();
                        }
                    }

                /*
                '--- Save User's Alerts-------------------------------- -
                For Me.i = 1 To fgAlerts.Rows.Count - 1

                    If fgAlerts(i, 2) = "0" Then
                        With comm
                            .Connection = cn
                            .CommandText = "sp_InsertUsersAlerts"
                            .CommandType = CommandType.StoredProcedure
                        End With
                        comm.Parameters.Clear()
                        prmSQL = comm.Parameters.AddWithValue("@ID", Nothing)
                        prmSQL.Direction = ParameterDirection.Output
                        prmSQL.SqlDbType = SqlDbType.Int
                    Else
                        With comm
                            .Connection = cn
                            .CommandText = "sp_EditUsersAlerts"
                            .CommandType = CommandType.StoredProcedure
                        End With
                        comm.Parameters.Clear()
                        prmSQL = comm.Parameters.AddWithValue("@ID", fgAlerts(i, 2))
                    End If

                    prmSQL = comm.Parameters.AddWithValue("@User_ID", iID)
                    prmSQL = comm.Parameters.AddWithValue("@AlertType", fgAlerts(i, 3))
                    prmSQL = comm.Parameters.AddWithValue("@OK", IIf(fgAlerts(i, 0), "1", "0"))
                    comm.ExecuteNonQuery()

                Next

                //--- Save User's Documents---------------------------- -
                for (i = 1; i <= fgDocFiles.Rows.Count - 1; i++)
                {
                    sTemp = fgDocFiles(i, 2);
                    if (fgDocFiles(i, 5) != "" )
                        sTemp = DMS_UploadFile(fgDocFiles(i, 5), "Company/Users/" & sUserFullName, fgDocFiles(i, 2));


                    if (fgDocFiles(i, 3) == "0")
                    {
                        User
                    }
                        With comm
                            .Connection = cn
                            .CommandText = "sp_InsertUsersDocuments"
                            .CommandType = CommandType.StoredProcedure
                        End With
                        comm.Parameters.Clear()
                        prmSQL = comm.Parameters.AddWithValue("@ID", Nothing)
                        prmSQL.Direction = ParameterDirection.Output
                        prmSQL.SqlDbType = SqlDbType.Int
                    Else
                        With comm
                            .Connection = cn
                            .CommandText = "sp_EditUsersDocuments"
                            .CommandType = CommandType.StoredProcedure
                        End With
                        comm.Parameters.Clear()
                        prmSQL = comm.Parameters.AddWithValue("@ID", fgDocFiles(i, 3))
                    End If

                    prmSQL = comm.Parameters.AddWithValue("@DateIns", fgDocFiles(i, 0))
                    prmSQL = comm.Parameters.AddWithValue("@User_ID", iID)
                    prmSQL = comm.Parameters.AddWithValue("@DocType_ID", fgDocFiles(i, 4))
                    prmSQL = comm.Parameters.AddWithValue("@FileName", Path.GetFileName(sTemp))   'fgDocFiles(i, 2))
                    comm.ExecuteNonQuery()
                    */
                }
                Global.GetUsersList();
                DefineList();
                fgList.Row = iRow;
                tsbSave.Enabled = false;
            }
            else
                MessageBox.Show("Η εισαγωγή του επώνυμου είναι υποχρεωτική", "Λίστα Χρηστών", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void picCheckFilesFolder_Click(object sender, EventArgs e)
        {

        }
        private void EmptyDetails()
        {
            iID = 0;
            txtSurname.Text = "";
            txtFirstname.Text = "";
            txtSurnameEng.Text = "";
            txtFirstnameEng.Text = "";
            txtFather.Text = "";
            txtMother.Text = "";
            txtADT.Text = "";
            txtIssueDate.Text = "";
            txtPolice.Text = "";
            dDoB.Value = Convert.ToDateTime("01-01-1900");
            txtFamily.Text = "";
            cmbSex.Text = "";
            txtChildren.Text = "";
            txtAFM.Text = "";
            txtDOY.Text = "";
            cmbCountryTaxes.SelectedValue = 0;
            txtAddress.Text = "";
            txtCity.Text = "";
            txtZip.Text = "";
            txtTel.Text = "";
            txtFax.Text = "";
            txtMobile.Text = "";
            txtEMail.Text = "";
            txtEMail_Username.Text = "";
            txtEMail_Password.Text = "";
            txtEducation.Text = "";
            txtRelations.Text = "";
            dStart.Value = DateTime.Now;
            dNomStart.Value = Convert.ToDateTime("01-01-1900");
            txtDuration.Text = "";
            txtPosition.Text = "";
            txtEidikotita.Text = "";
            txtBank.Text = "";
            txtBankAccount.Text = "";

            cmbDMSAccess.SelectedIndex = 0;
            txtPasword.Text = "";
            cmbUserStatus.SelectedIndex = 2;
            cmbDivision.SelectedValue = 1;

            txtDMSDownloadPath.Text = "";

            chkChief.Checked = false;
            chkRM.Checked = false;
            chkSender.Checked = false;
            chkIntroducer.Checked = false;
            chkDiaxiristis.Checked = false;

            cmbClientsRequests_Status.SelectedIndex = 0;
            cmbClientsFilters.SelectedValue = 1;
            chkDivisionFilter.Checked = false;

            for (i = 1; i <= fgMenus.Rows.Count - 1; i++)
            {
                fgMenus[i, 1] = lstStatus[0];
                fgMenus[i, 4] = 0;
            }

            for (i = 1; i <= fgAlerts.Rows.Count - 1; i++)
                fgAlerts[i, 0] = false;

            txtPhoto.Text = "";
        }
        private void tsbExcel_Click(object sender, EventArgs e)
        {

        }
        private void tsbRefresh_Click(object sender, EventArgs e)
        {

            fgMenus.Redraw = false;
            clsSystem System = new clsSystem();
            System.GetList_Menus();
            foreach (DataRow dtRow in System.List.Rows)
            {
                if (Convert.ToInt16(dtRow["MenuGroup_ID"]) != 0)
                { 
                    sTemp = Convert.ToString(dtRow["ID"]);
                    i = fgMenus.FindRow(sTemp, 1, 7, false);
                    if (i < 0)
                        fgMenus.AddItem(dtRow["TitleGr"] + "\t" + lstStatus[0] + "\t" + "0" + "\t" + "0" + "\t" + "0" + "\t" +
                                        dtRow["ID"] + "\t" + dtRow["MenuGroup_ID"] + "\t" + dtRow["MenuView_ID"] + "\t" + dtRow["Extra"]);
                }
            }
            fgMenus.Redraw = true;
        }
        private void fgMenus_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Col == 1) fgMenus[e.Row, 4] = fgMenus[e.Row, 1];
        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
