using System;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace Core

{
    public partial class ucClientData : UserControl
    {
        DataTable dtStatus;
        DataRow dtRow;
        DataColumn dtCol;
        DataView dtView;
        int i, iRightzLevel, iRec_ID, iRecord_ID, iClient_ID, iClientStatus, iTipos, iAktion, iMode_Param, iLastStep;
        string sCode, sClientFullName, sUsers_List, sGridStyle;
        bool bCheckList, bCheckTrack;
        SortedList lstNeeds = new SortedList();
        SortedList lstAccounts = new SortedList();
        clsClients Client = new clsClients();
        public ucClientData()
        {
            InitializeComponent();

            sGridStyle = "Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}"; ;
            tabClientData.Top = 0;
            tabClientData.Left = 0;
            panFP.Top = 0;
            panFP.Left = 0;
            panNP.Top = 0;
            panNP.Left = 0;

            //----- initialize Client's Status List------ -
            dtStatus = new System.Data.DataTable("List");
            dtCol = dtStatus.Columns.Add("ID", System.Type.GetType("System.Int32"));
            dtCol = dtStatus.Columns.Add("Title", System.Type.GetType("System.String"));

            dtRow = dtStatus.NewRow();
            dtRow["ID"] = -5;
            dtRow["Title"] = "Represent Persons";
            dtStatus.Rows.Add(dtRow);

            dtRow = dtStatus.NewRow();
            dtRow["ID"] = -4;
            dtRow["Title"] = "Introducers List";
            dtStatus.Rows.Add(dtRow);

            dtRow = dtStatus.NewRow();
            dtRow["ID"] = -3;
            dtRow["Title"] = "Κέντρα Επιρροής";
            dtStatus.Rows.Add(dtRow);

            dtRow = dtStatus.NewRow();
            dtRow["ID"] = -2;
            dtRow["Title"] = "Επαφή";
            dtStatus.Rows.Add(dtRow);

            dtRow = dtStatus.NewRow();
            dtRow["ID"] = -1;
            dtRow["Title"] = "Υποψήφιος";
            dtStatus.Rows.Add(dtRow);

            dtRow = dtStatus.NewRow();
            dtRow["ID"] = 1;
            dtRow["Title"] = "Ενεργός";
            dtStatus.Rows.Add(dtRow);

            dtRow = dtStatus.NewRow();
            dtRow["ID"] = 0;
            dtRow["Title"] = "Ανενεργός";
            dtStatus.Rows.Add(dtRow);
        }
        private void ucClientData_Load(object sender, EventArgs e)
        {
            chkOldFiles.Checked = false;
            chkCancelAccs.Checked = false;

            cmbBanks.DataSource = Global.dtBanks;
            cmbBanks.DisplayMember = "Title";
            cmbBanks.ValueMember = "ID";

            cmbCurrencies.DataSource = Global.dtCurrencies;
            cmbCurrencies.DisplayMember = "Title";
            cmbCurrencies.ValueMember = "ID";
            
            //------- fgPackages ----------------------------
            fgPackages.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            fgPackages.Styles.ParseString(sGridStyle);
            fgPackages.ShowCellLabels = true;
            fgPackages.Styles.Normal.WordWrap = true;
            fgPackages.DoubleClick += new System.EventHandler(fgPackages_DoubleClick);

            //------- fgAccounts ----------------------------
            fgBankAccounts.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgBankAccounts.Styles.ParseString(sGridStyle);

            //------- fgNeeds ----------------------------
            fgNeeds.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgNeeds.Styles.ParseString(sGridStyle);

            //------- fgStep2 ----------------------------
            fgStep2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgStep2.Styles.ParseString(sGridStyle);

            //------- fgStep3 ----------------------------
            fgStep3.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgStep3.Styles.ParseString(sGridStyle);

            //------- fgStep4 ----------------------------
            fgStep4.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgStep4.Styles.ParseString(sGridStyle);

            //------- fgStep5 ----------------------------
            fgStep5.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgStep5.Styles.ParseString(sGridStyle);

            //------- fgStep6 ----------------------------
            fgStep6.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgStep6.Styles.ParseString(sGridStyle);

            //------- fgStep7 ----------------------------
            fgStep7.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgStep7.Styles.ParseString(sGridStyle);

            //------- fgRandevouz ----------------------------
            fgRandevouz.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgRandevouz.Styles.ParseString(sGridStyle);

            //------- fgInfluenceCenters ----------------------------
            fgInfluenceCenters.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgInfluenceCenters.Styles.ParseString(sGridStyle);

            //------- fgDependentsList ----------------------------
            fgDependentsList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgDependentsList.Styles.ParseString(sGridStyle);

            //------- fgDocFiles ----------------------------
            fgDocFiles.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgDocFiles.Styles.ParseString(sGridStyle);
            fgDocFiles.DoubleClick += new System.EventHandler(fgDocFiles_DoubleClick);

            //------- fgBankAccounts ----------------------------
            fgBankAccounts.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgBankAccounts.Styles.ParseString(sGridStyle);
            fgBankAccounts.DoubleClick += new System.EventHandler(fgBankAccounts_DoubleClick);

            tpGeneral.Text = "Γεν.Στοιχεία";  // Global.GetLabel("general_information");
            //tabClientData.TabPages.Item(1).Text = "Δραστηριότητες"
            //tabClientData.TabPages.Item(2).Text = "Εργασίες RM"
            //tabClientData.TabPages.Item(3).Text = Global.GetLabel("contracts")
            //tabClientData.TabPages.Item(4).Text = Global.GetLabel("miscellaneous")
            //tabClientData.TabPages.Item(5).Text = "Κέντρα Επιρροής"
            //tabClientData.TabPages.Item(6).Text = "Ποιους επηρεάζει"
            //tabClientData.TabPages.Item(7).Text = "Δικαιούχοι/Εξουσιοδοτούμενοι"

            lblFPSurname.Text = Global.GetLabel("surname");
            lblFPFirstname.Text = Global.GetLabel("name");
            lblFPFatherSurname.Text = Global.GetLabel("fathers_surname");
            lblFPFatherFirstname.Text = Global.GetLabel("name");
            lblFPMotherSurname.Text = Global.GetLabel("mothers_surname");
            lblFPMotherFirstname.Text = Global.GetLabel("name");
            lblFPSyzygosSurname.Text = Global.GetLabel("spouses_surname");
            lblFPSyzygosFirstname.Text = Global.GetLabel("name");
            lblFPSpecials.Text = Global.GetLabel("profession");
            lblFPCompany.Text = Global.GetLabel("company");
            lblFPBorn.Text = Global.GetLabel("date_of_birth");
            lblFPBornPlace.Text = Global.GetLabel("place_of_birth");
            lblFPSex.Text = Global.GetLabel("sex");
            lblFPCitizen.Text = Global.GetLabel("citizenship");
            lblFPFamilyStatus.Text = Global.GetLabel("marital_status");
            lblFPStatus.Text = Global.GetLabel("status");
            lblFPCategory.Text = Global.GetLabel("category");
            lblFPDivision.Text = Global.GetLabel("branch");
            lblFPCertInform.Text = Global.GetLabel("certificate_Information");
            lblFP_AT_Misc.Text = Global.GetLabel("police_id_card"); 
            lblFPCountryTaxes.Text = Global.GetLabel("country_of_taxation");
            lblFPExpireDate.Text = Global.GetLabel("expire_date");
            lblFPMerida.Text = Global.GetLabel("investor_share");
            lblFPAFM.Text = Global.GetLabel("tin");
            lblFPAMKA.Text = Global.GetLabel("amka");
            lblFPDOY.Text = Global.GetLabel("tax_office");
            lblFPLogAxion.Text = Global.GetLabel("securities_account");
            lblEidikiKategoria.Text = "Ειδική κατηγορία";   // Global.GetLabel("politically_exposed_persons");
        }
        protected override void OnResize(EventArgs e)
        {
            tabClientData.Width = this.Width - 2;
            tabClientData.Height = this.Height - 2;
            
            panFP.Width = tabClientData.Width;
            panFP.Height = tabClientData.Height;

            grpGeneral1.Width = panFP.Width - 22;
            grpGeneral1.Height = panFP.Height - 232;

            grpGeneral2.Width = panFP.Width - 22;
            grpGeneral2.Height = 184;
            grpGeneral2.Top = grpGeneral1.Top + grpGeneral1.Height + 12;

            panNP.Width = tabClientData.Width;
            panNP.Height = tabClientData.Height;

            grpGeneral11.Width = panNP.Width - 22;
            grpGeneral11.Height = panNP.Height - 232;

            grpGeneral12.Width = panNP.Width - 22;
            grpGeneral12.Height = 184;
            grpGeneral12.Top = grpGeneral11.Top + grpGeneral11.Height + 12;

            fgPackages.Height = tabClientData.Height - 78;
        }
        public void ShowRecord(int iClient_ID_Local, int iRightsLevel, int iMode)
        {
            iRightzLevel = iRightsLevel;
            iMode_Param = iMode;
            iClient_ID = iClient_ID_Local;

            InitLists(iMode);

            
            Client.Record_ID = iClient_ID;
            Client.EMail = "";
            Client.Mobile = "";
            Client.AFM = "";
            Client.DoB = Convert.ToDateTime("1900/01/01");
            Client.GetRecord();

            iTipos = Client.Type;

            switch (iTipos)
            {
                case 1:
                    txtFPSurname.Text = Client.Surname;
                    txtFPFirstname.Text = Client.Firstname;
                    txtFPSurnameEng.Text = Client.SurnameEng;
                    txtFPFirstnameEng.Text = Client.FirstnameEng;
                    txtFPFatherSurname.Text = Client.SurnameFather;
                    txtFPFatherFirstname.Text = Client.FirstnameFather;
                    txtFPMotherSurname.Text = Client.SurnameMother;
                    txtFPMotherFirstname.Text = Client.FirstnameMother;
                    txtFPSyzygosSurname.Text = Client.SurnameSizigo;
                    txtFPSyzygosFirstname.Text = Client.FirstnameSizigo;
                    txtFPADT.Text = Client.ADT;
                    txtFPExpireDate.Text = Client.ExpireDate;
                    txtFPPolice.Text = Client.Police;
                    txtFPPassport.Text = Client.Passport;
                    txtFPPassport_ExpireDate.Text = Client.Passport_ExpireDate;
                    txtFPPassport_Police.Text = Client.Passport_Police;
                    cmbFPCitizen.SelectedValue = Client.Citizen_ID;
                    cmbFPDivision.SelectedValue = Client.Division;
                    chkFPInfluenceCenter.Checked = Client.Is_InfluenceCenter == 1 ? true : false;
                    chkFPIntroducer.Checked = Client.Is_Introducer == 1 ? true : false;
                    chkFPRepresentPerson.Checked = Client.Is_RepresentPerson == 1 ? true : false;
                    cmbFPSpecials.SelectedValue = Client.Spec_ID;
                    cmbFPOccupation.SelectedValue = Client.Brunch_ID;                    
                    dFPDoB.Value = Client.DoB;
                    txtFPBornPlace.Text = Client.BornPlace;
                    cmbFPSex.Text = Client.Sex;
                    cmbFPFamilyStatus.SelectedIndex = Client.FamilyStatus;
                    cmbFPCategory.SelectedIndex = Client.Category;
                    txtFPAFM.Text = Client.AFM;
                    txtFPDOY.Text = Client.DOY;
                    txtFPAFM2.Text = Client.AFM2;
                    txtFPDOY2.Text = Client.DOY2;
                    txtFPFPA.Text = Client.VAT_Percent.ToString();
                    txtFPAMKA.Text = Client.AMKA;
                    cmbFPCountryTaxes.SelectedValue = Client.CountryTaxes_ID;
                    txtFPAddress.Text = Client.Address;
                    txtFPCity.Text = Client.City;
                    txtFPZip.Text = Client.Zip;
                    cmbFPXora.SelectedValue = Client.Country_ID;
                    txtFPTel.Text = Client.Tel;
                    txtFPMobile.Text = Client.Mobile;
                    chkFPSMS.Checked = Convert.ToBoolean(Client.SendSMS);
                    txtFPFax.Text = Client.Fax;
                    txtFPEMail.Text = Client.EMail;
                    cmbFPConnectionMethod.SelectedIndex = Client.ConnectionMethod;
                    txtFPCompany.Text = Client.CompanyTitle;
                    txtFPCompanyDescription.Text = Client.CompanyDescription;
                    txtFPJobPosition.Text = Client.JobPosition;
                    txtFPJobAddress.Text = Client.JobAddress;
                    txtFPJobCity.Text = Client.JobCity;
                    txtFPJobZip.Text = Client.JobZip;
                    cmbFPJobCountry_ID.SelectedValue = Client.JobCountry_ID;
                    txtFPJobTel.Text = Client.JobTel;
                    txtFPJobMobile.Text = Client.JobMobile;
                    txtFPJobEMail.Text = Client.JobEMail;
                    txtFPJobURL.Text = Client.JobURL;
                    cmbFPStatus.SelectedValue = Client.Status;
                    chkFPBlockStatus.Checked = Client.BlockStatus == 1 ? true : false;
                    lblEkkatharistika.Text = Client.Ekkatharistika.ToString();
                    lblFPSpecialCategory.Text = Client.SpecialCategory;
                    txtFPMerida.Text = Client.Merida;
                    txtFPLogAxion.Text = Client.LogAxion;
                    txtFPSumAxion.Text = Client.SumAxion.ToString();
                    txtFPSumAkiniton.Text = Client.SumAkiniton.ToString();
                    cmbFPRisk.SelectedIndex = Client.Risk;
                    panFP.Visible = true;
                    panNP.Visible = false;
                    break;
                case 2:
                    txtNPTitle.Text = Client.Surname;
                    txtNPTitleEng.Text = Client.SurnameEng;
                    txtNPDiakritikosTitlos.Text = Client.Firstname;
                    txtNPEdra.Text = Client.SurnameFather;
                    txtNPMorfi.Text = Client.FirstnameFather;
                    cmbNPDivision.SelectedValue = Client.Division;
                    chkNPInfluenceCenter.Checked = Client.Is_InfluenceCenter == 1 ? true : false;
                    chkNPIntroducer.Checked = Client.Is_Introducer == 1 ? true : false;
                    chkNPRepresentPerson.Checked = Client.Is_RepresentPerson == 1 ? true : false;                    
                    cmbNPBrunches.SelectedValue = Client.Brunch_ID;
                    txtNPLEI.Text = Client.FirstnameSizigo;

                    txtNPAM.Text = Client.ADT;
                    txtNPExpireDate.Text = Client.ExpireDate;
                    txtNPArmodiaArxi.Text = Client.Police;
                    txtNPAFM.Text = Client.AFM;
                    txtNPDOY.Text = Client.DOY;
                    txtNPFPA.Text = Client.VAT_Percent.ToString();
                    cmbNPNation.SelectedValue = Client.Citizen_ID;
                    cmbNPCountryTaxes.SelectedValue = Client.CountryTaxes_ID;
                    cmbNPCategory.SelectedIndex = Client.Category;
                    txtNPAddress.Text = Client.Address;
                    txtNPCity.Text = Client.City;
                    txtNPZip.Text = Client.Zip;
                    cmbNPCountry.SelectedValue = Client.Country_ID;
                    txtNPTel.Text = Client.Tel;
                    txtNPMobile.Text = Client.Mobile;
                    chkNPSMS.Checked = Convert.ToBoolean(Client.SendSMS);
                    txtNPFax.Text = Client.Fax;
                    txtNPEMail.Text = Client.EMail;
                    cmbNPConnectionMethod.SelectedIndex = Client.ConnectionMethod;
                    cmbNPStatus.SelectedValue = Client.Status;
                    lblEkkatharistika.Text = Client.Ekkatharistika.ToString();
                    lblNPSpecialCategory.Text = Client.SpecialCategory;
                    txtNPMerida.Text = Client.Merida;
                    txtNPLogAxion.Text = Client.LogAxion;
                    txtNPSumAxion.Text = Client.SumAxion.ToString();
                    txtNPSumAkiniton.Text = Client.SumAkiniton.ToString();
                    cmbNPRisk.SelectedIndex = Client.Risk;
                    panFP.Visible = false;
                    panNP.Visible = true;
                    break;
            }
            txtNotes.Text = Client.Notes;
            txtConne.Text = Client.Conne; 
            sClientFullName = Client.Fullname;
            sUsers_List = Client.Users_List;
            txtNotes.Enabled = false;
            iClientStatus = Client.Status;
            cmbUser2.SelectedValue = Client.RM_ID;
            //Disable all cotnrols of the form

            DefineTabPageContent();
        }     

        public void InitLists(int iMode)
        {
            bCheckList = false;

            //-------------- Define Divisions List ------------------
            cmbFPDivision.DataSource = Global.dtDivisions.Copy();
            cmbFPDivision.DisplayMember = "Title";
            cmbFPDivision.ValueMember = "ID";

            cmbNPDivision.DataSource = Global.dtDivisions.Copy();
            cmbNPDivision.DisplayMember = "Title";
            cmbNPDivision.ValueMember = "ID";

            //-------------- Define Εξειδίκευση επαγγέλματος List ------------------
            cmbFPSpecials.DataSource = Global.dtSpecials.Copy();
            cmbFPSpecials.DisplayMember = "Title";
            cmbFPSpecials.ValueMember = "ID";

            //-------------- Define Epaggelmata List ------------------
            cmbFPOccupation.DataSource = Global.dtBrunches.Copy();
            cmbFPOccupation.DisplayMember = "Title";
            cmbFPOccupation.ValueMember = "ID";

            //-------------- Define Coutries List ------------------
            cmbFPCitizen.DataSource = Global.dtCountries.Copy();
            cmbFPCitizen.DisplayMember = "Title";
            cmbFPCitizen.ValueMember = "ID";

            cmbFPCountryTaxes.DataSource = Global.dtCountries.Copy();
            cmbFPCountryTaxes.DisplayMember = "Title";
            cmbFPCountryTaxes.ValueMember = "ID";

            cmbFPXora.DataSource = Global.dtCountries.Copy();
            cmbFPXora.DisplayMember = "Title";
            cmbFPXora.ValueMember = "ID";

            cmbFPJobCountry_ID.DataSource = Global.dtCountries.Copy();
            cmbFPJobCountry_ID.DisplayMember = "Title";
            cmbFPJobCountry_ID.ValueMember = "ID";

            cmbNPNation.DataSource = Global.dtCountries.Copy();
            cmbNPNation.DisplayMember = "Title";
            cmbNPNation.ValueMember = "ID";

            cmbNPCountryTaxes.DataSource = Global.dtCountries.Copy();
            cmbNPCountryTaxes.DisplayMember = "Title";
            cmbNPCountryTaxes.ValueMember = "ID";

            cmbNPCountry.DataSource = Global.dtCountries.Copy();
            cmbNPCountry.DisplayMember = "Title";
            cmbNPCountry.ValueMember = "ID";

            //----- initialize PROVIDERS List -------
            dtView = Global.dtServiceProviders.Copy().DefaultView;
            dtView.RowFilter = "ProviderType = 1 OR ProviderType = 2";
            cmbProviders_S3.DataSource = dtView;
            cmbProviders_S3.DisplayMember = "Title";
            cmbProviders_S3.ValueMember = "ID";

            //-------------- Define RM List ------------------
            dtView = Global.dtUserList.Copy().DefaultView;
            dtView.Sort = "ID";
            i = dtView.Find("0");
            dtView[i]["Title"] = "-";
            dtView.Sort = "Title";
            dtView.RowFilter = "RM = 1";
            cmbUser2.DataSource = dtView;
            cmbUser2.DisplayMember = "Title";
            cmbUser2.ValueMember = "ID";

            //------------- Needs List -----------------------
            lstNeeds.Clear();
            foreach (DataRow row in Global.dtNeeds.Rows)
            {
                if (Convert.ToInt32(row["ID"]) == 0) lstNeeds.Add(0, "");
                else lstNeeds.Add(row["ID"], row["Title"]);
            }
            fgNeeds.Cols[0].DataMap = lstNeeds;

            bCheckList = true;

            cmbFPStatus.DataSource = dtStatus.Copy();
            cmbFPStatus.DisplayMember = "Title";
            cmbFPStatus.ValueMember = "ID";
            cmbFPStatus.SelectedIndex = 0;

            cmbNPStatus.DataSource = dtStatus.Copy();
            cmbNPStatus.DisplayMember = "Title";
            cmbNPStatus.ValueMember = "ID";
            cmbNPStatus.SelectedIndex = 0;            
        }
        private void tabClientData_SelectedIndexChanged(object sender, EventArgs e)
        {
            DefineTabPageContent();
        }
        private void DefineTabPageContent()
        {
            switch (tabClientData.SelectedTab.Name)     //Convert.ToInt32(tabClientData.SelectedIndex)
            { 
                case "tpGeneral":
                     ShowActivities();
                     break;

                case "tpActivities":
                    break;

                case "tpRMJobs":
                     ShowJobsLists();
                     break;

                case "tpContracts":
                     if (iMode_Param == 4) ShowIntroducersContracts();
                     else ShowPackages();
                     break;

                case "tpDocuments":
                     if (iMode_Param == 4)
                     {
                        fgDocFiles.Cols[1].Visible = false;
                        fgDocFiles.Cols[4].Visible = false;
                     }
                     ShowDocFiles();
                     ShowBankAccounts();
                     break;

                case "tpInfluenceCenters":
                     ShowInfluenceCenters();
                     ShowDependentsList();
                     break; 

                case "tpRegistryData":
                      break;
            }
        }
        public void ShowActivities()
        {

        }
        public void ShowJobsLists()
        {

        }
        public void ShowIntroducersContracts()
        {

        }
        public void ShowPackages()
        {
            bCheckList = false;
            fgPackages.Redraw = false;
            fgPackages.Rows.Count = 1;

            clsContracts klsContract = new clsContracts();
            klsContract.PackageType = 1;
            klsContract.DateStart = Convert.ToDateTime("2000/01/01");
            klsContract.DateFinish = Convert.ToDateTime("2070/12/31");
            klsContract.Client_ID = iClient_ID;
            klsContract.Advisor_ID = 0;
            klsContract.Service_ID = 0;
            klsContract.ServiceProvider_ID = 0;
            klsContract.ClientName = "";
            if (chkCanceled.Checked) klsContract.Status = -1;
            else klsContract.Status = 1;
            klsContract.ClientStatus = -1;
            klsContract.GetList();
            foreach (DataRow dtRow in klsContract.List.Rows)
            {
                fgPackages.AddItem(dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "/" + "\n" + dtRow["Portfolio"] + "\t" + "Γενικά στοιχεία" + "\t" +
                                       "Πακέτο" + "\t" + dtRow["PackageTitle"] + "\t" + dtRow["ID"] + "\t" +
                                       "0" + "\t" + dtRow["CFP_ID"] + "\t" + dtRow["PackageVersion"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"]);
                fgPackages.AddItem(dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "/" + "\n" + dtRow["Portfolio"] + "\t" + "Γενικά στοιχεία" + "\t" +
                                       "Ημ/νίες έναρξης-λήξης" + "\t" + Convert.ToDateTime(dtRow["DateStart"]).ToString("dd/MM/yyyy") + " - " + 
                                       Convert.ToDateTime(dtRow["DateFinish"]).ToString("dd/MM/yyyy") + "\t" + dtRow["ID"] + "\t" +
                                       "0" + "\t" + dtRow["CFP_ID"] + "\t" + dtRow["PackageVersion"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"]);
                if (dtRow["Portfolio_Type"].ToString().Trim() != "")
                    fgPackages.AddItem(dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "/" + "\n" + dtRow["Portfolio"] + "\t" + "Γενικά στοιχεία" + "\t" +
                                           "Τύπος" + "\t" + dtRow["Portfolio_Type"] + "\t" + dtRow["ID"] + "\t" + "0" + "\t" +
                                           dtRow["CFP_ID"] + "\t" + dtRow["PackageVersion"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"]);

                fgPackages.AddItem(dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "/" + "\n" + dtRow["Portfolio"] + "\t" + "Γενικά στοιχεία" + "\t" +
                                       "Νόμισμα Αναφοράς" + "\t" + dtRow["Currency"] + "\t" + dtRow["ID"] + "\t" + "0" + "\t" +
                                       dtRow["CFP_ID"] + "\t" + dtRow["PackageVersion"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"]);
                if (Convert.ToInt32(dtRow["Service_ID"]) == 5)          // 5 - DealAdvisory
                {
                    //fgPackages.AddItem(dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "/" + "\n" + dtRow["Portfolio"] + "\t" + "Γενικά στοιχεία" + "\t" +
                    //       "Προτεινόμ. Χρηματ.μέσα" + "\t" + dtRow["SuggestedFinanceTool_Title"] + "\t" + dtRow["ID"] + "\t" + "0" + "\t" +
                    //       dtRow["CFP_ID"] + "\t" + dtRow["PackageVersion"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"]);
                    fgPackages.AddItem(dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "/" + "\n" + dtRow["Portfolio"] + "\t" + "Γενικά στοιχεία" + "\t" +
                           "Επιλεγμένη Χρηματ.μέσα" + "\t" + dtRow["FinanceTool_Title"] + "\t" + dtRow["ID"] + "\t" + "0" + "\t" +
                           dtRow["CFP_ID"] + "\t" + dtRow["PackageVersion"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"]);
                }
                else
                {
                    //fgPackages.AddItem(dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "/" + "\n" + dtRow["Portfolio"] + "\t" + "Γενικά στοιχεία" + "\t" +
                    //       "Προτειν.Επενδ.Πολιτική" + "\t" + dtRow["SuggestedInvestmentPolicy_Title"] + "\t" + dtRow["ID"] + "\t" + "0" + "\t" +
                    //       dtRow["CFP_ID"] + "\t" + dtRow["PackageVersion"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"]);
                    fgPackages.AddItem(dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "/" + "\n" + dtRow["Portfolio"] + "\t" + "Γενικά στοιχεία" + "\t" +
                           "Επιλεγμ.Επενδ.Πολιτική" + "\t" + dtRow["InvestmentPolicy_Title"] + "\t" + dtRow["ID"] + "\t" + "0" + "\t" +
                           dtRow["CFP_ID"] + "\t" + dtRow["PackageVersion"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"]);
                }

                if (Convert.ToInt32(dtRow["User1_ID"]) != 0)
                    if (Convert.ToInt32(dtRow["AdvisorStatus"]) == 1)
                        fgPackages.AddItem(dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "/" + "\n" + dtRow["Portfolio"] + "\t" + "Γενικά στοιχεία" + "\t" +
                                               "Advisory" + "\t" + dtRow["AdvisorName"] + "\t" + dtRow["ID"] + "\t" + dtRow["User1_ID"] + "\t" + dtRow["CFP_ID"] + "\t" +
                                               dtRow["PackageVersion"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"]);

                if (Convert.ToInt32(dtRow["User2_ID"]) != 0)
                    if (Convert.ToInt32(dtRow["RMStatus"]) == 1)
                        fgPackages.AddItem(dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "/" + "\n" + dtRow["Portfolio"] + "\t" + "Γενικά στοιχεία" + "\t" +
                                               "RM" + "\t" + dtRow["RMName"] + "\t" + dtRow["ID"] + "\t" + dtRow["User2_ID"] + "\t" + dtRow["CFP_ID"] + "\t" +
                                               dtRow["PackageVersion"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"]);


                if (Convert.ToInt32(dtRow["User3_ID"]) != 0)
                    if (Convert.ToInt32(dtRow["IntroStatus"]) == 1)
                        fgPackages.AddItem(dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "/" + "\n" + dtRow["Portfolio"] + "\t" + "Γενικά στοιχεία" + "\t" +
                                               "Introducer" + "\t" + dtRow["IntroName"] + "\t" + dtRow["ID"] + "\t" + dtRow["User3_ID"] + "\t" + dtRow["CFP_ID"] + "\t" +
                                               dtRow["PackageVersion"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"]);

                if (Convert.ToInt32(dtRow["User4_ID"]) != 0)
                    if (Convert.ToInt32(dtRow["DiaxStatus"]) == 1)
                        fgPackages.AddItem(dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "/" + "\n" + dtRow["Portfolio"] + "\t" + "Γενικά στοιχεία" + "\t" +
                                               "Διαχειριστής" + "\t" + dtRow["DiaxName"] + "\t" + dtRow["ID"] + "\t" + dtRow["User4_ID"] + "\t" + dtRow["CFP_ID"] + "\t" +
                                               dtRow["PackageVersion"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"]);


                if (Convert.ToInt32(dtRow["BrokerageServiceProvider_ID"]) != 0)
                    fgPackages.AddItem(dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "/" + "\n" + dtRow["Portfolio"] + "\t" + "Γενικά στοιχεία" + "\t" +
                                           "Πάροχος" + "\t" + dtRow["BrokerageServiceProvider_Title"] + "\t" + dtRow["ID"] + "\t" + "0" + "\t" +
                                           dtRow["CFP_ID"] + "\t" + dtRow["PackageVersion"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"]);

                if ((Convert.ToInt32(dtRow["Service_ID"]) == 2) && (Convert.ToInt32(dtRow["AdvisoryServiceProvider_ID"]) != 0))
                    fgPackages.AddItem(dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "/" + "\n" + dtRow["Portfolio"] + "\t" + "Advisory" + "\t" +
                                           "Πάροχος" + "\t" + dtRow["AdvisoryServiceProvider_Title"] + "\t" + dtRow["ID"] + "\t" + "0" + "\t" +
                                           dtRow["CFP_ID"] + "\t" + dtRow["PackageVersion"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"]);

                if ((Convert.ToInt32(dtRow["Service_ID"]) == 4) && (Convert.ToInt32(dtRow["CustodyServiceProvider_ID"]) != 0))
                    fgPackages.AddItem(dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "/" + "\n" + dtRow["Portfolio"] + "\t" + "Custody" + "\t" +
                                           "Πάροχος" + "\t" + dtRow["CustodyServiceProvider_Title"] + "\t" + dtRow["ID"] + "\t" + "0" + "\t" +
                                           dtRow["CFP_ID"] + "\t" + dtRow["PackageVersion"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"]);

                //if ((Convert.ToInt32(dtRow["Service_ID"]) == 5) && (Convert.ToInt32(dtRow["DealAdvisoryServiceProvider_ID"]) != 0))
                //fgPackages.AddItem(dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "/" + "\n" + dtRow["Portfolio"] + "\t" + "Deal Advisory" + "\t" +
                //                       "Πάροχος" + "\t" + dtRow["DealAdvisoryServiceProvider_Title"] + "\t" + dtRow["ID"] + "\t" + "0" + "\t" +
                //                       dtRow["CFP_ID"] + "\t" + dtRow["PackageVersion"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"]);
                //fgPackages.AddItem(dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "/" + "\n" + dtRow["Portfolio"] + "\t" + "Deal Advisory" + "\t" +
                //                       "Option" + "\t" + dtRow["DealAdvisoryOption_Title"] + "\t" + dtRow["ID"] + "\t" + "0" + "\t" +
                //                      dtRow["CFP_ID"] + "\t" + dtRow["PackageVersion"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"]);
                //fgPackages.AddItem(dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "/" + "\n" + dtRow["Portfolio"] + "\t" + "Deal Advisory" + "\t" +
                //       "Χρηματοπιστωτικά μέσα" + "\t" + dtRow["DealAdvisoryInvestmentPolicy_Title"] + "\t" + dtRow["ID"] + "\t" + "0" + "\t" +
                // dtRow["CFP_ID"] + "\t" + dtRow["PackageVersion"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"]);

                if ((Convert.ToInt32(dtRow["Service_ID"]) == 3) && (Convert.ToInt32(dtRow["DiscretServiceProvider_ID"]) != 0))
                    fgPackages.AddItem(dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "/" + "\n" + dtRow["Portfolio"] + "\t" + "Discretionary" + "\t" +
                                           "Πάροχος" + "\t" + dtRow["DiscretServiceProvider_Title"] + "\t" + dtRow["ID"] + "\t" + "0" + "\t" +
                                           dtRow["CFP_ID"] + "\t" + dtRow["PackageVersion"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"]);

                if (false)
                {
                    if (Convert.ToInt32(dtRow["LombardServiceProvider_ID"]) != 0)
                        fgPackages.AddItem(dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "/" + "\n" + dtRow["Portfolio"] + "\t" + "Lombard Lending" + "\t" +
                                       "Πάροχος" + "\t" + dtRow["LombardServiceProvider_Title"] + "\t" + dtRow["ID"] + "\t" + "0" + "\t" +
                                       dtRow["CFP_ID"] + "\t" + dtRow["PackageVersion"]);
                        fgPackages.AddItem(dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "/" + "\n" + dtRow["Portfolio"] + "\t" + "Lombard Lending" + "\t" +
                                       "Option" + "\t" + dtRow["LombardOption_Title"] + "\t" + dtRow["ID"] + "\t" + "0" + "\t" +
                                       dtRow["CFP_ID"] + "\t" + dtRow["PackageVersion"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"]);

                    if (Convert.ToInt32(dtRow["FXServiceProvider_ID"]) != 0)
                        {
                            fgPackages.AddItem(dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "/" + "\n" + dtRow["Portfolio"] + "\t" + "FX" + "\t" +
                                               "Πάροχος" + "\t" + dtRow["FXServiceProvider_Title"] + "\t" + dtRow["ID"] + "\t" + "0" + "\t" +
                                               dtRow["CFP_ID"] + "\t" + dtRow["PackageVersion"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"]);
                            fgPackages.AddItem(dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "/" + "\n" + dtRow["Portfolio"] + "\t" + "FX" + "\t" +
                                               "Option" + "\t" + dtRow["FXOption_Title"] + "\t" + dtRow["ID"] + "\t" + "0" + "\t" +
                                               dtRow["CFP_ID"] + "\t" + dtRow["PackageVersion"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"]);
                        }
                }
            }

            bCheckList = true;

            fgPackages.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Free;
            // Merge values in column 1. 
            fgPackages.Cols[0].AllowMerging = true;
            fgPackages.Cols[1].AllowMerging = true;
            fgPackages.Cols[2].AllowMerging = true;

            fgPackages.Redraw = true;
            if (fgPackages.Rows.Count > 1) fgPackages.Row = 1;
        }
        private void tsbEditPackage_Click(object sender, EventArgs e)
        {
            if (fgPackages.Row > 1) EditPackage();
        }
        private void fgPackages_DoubleClick(object sender, EventArgs e)
        {
            if (fgPackages.Row > 1)   EditPackage();
        }
         

        private void tsbAddPackage_Click(object sender, EventArgs e)
        {
            if (iMode_Param == 4)
            {
                frmIntroducerContract locIntroducerContract = new frmIntroducerContract();
                locIntroducerContract.ShowDialog();
                if (locIntroducerContract.FinishAktion == 1)
                    ShowIntroducersContracts();
            }
            else
            {
                frmContract locContract = new frmContract();
                locContract.Aktion = 0;
                locContract.Contract_ID = 0;
                locContract.Client_ID = iClient_ID;
                locContract.ClientType = iTipos;
                locContract.ClientFullName = sClientFullName;
                locContract.RightsLevel = iRightzLevel;
                locContract.ShowDialog();
                if (locContract.FinishAktion == 1) ShowPackages();
            }
        }
        private void chkCanceled_CheckedChanged(object sender, EventArgs e)
        {
            ShowPackages();
        }
        private void EditPackage()
        {
            frmContract locContract = new frmContract();
            locContract.Aktion = 1;
            locContract.Contract_ID = Convert.ToInt32(fgPackages[fgPackages.Row, 5]);
            locContract.Contract_Details_ID = Convert.ToInt32(fgPackages[fgPackages.Row, 9]);
            locContract.Contract_Packages_ID = Convert.ToInt32(fgPackages[fgPackages.Row, 10]);
            //locContract.PackageVersion = Convert.ToInt32(fgPackages[fgPackages.Row, 8]);
            //locContract.CFP_ID = Convert.ToInt32(fgPackages[fgPackages.Row, 7]);
            locContract.Client_ID = iClient_ID;
            locContract.ClientType = iTipos;
            locContract.ClientFullName = sClientFullName;
            locContract.RightsLevel = iRightzLevel;
            locContract.ShowDialog();
            if (locContract.FinishAktion == 1) ShowPackages();
        }

        // --- fgDocFiles functionality -----------------------------------------------
        private void tsbAddDocFile_Click(object sender, EventArgs e)
        {
            if (iMode_Param != 4)
            {
                frmDocFilesEdit locDocFilesEdit = new frmDocFilesEdit();
                locDocFilesEdit.Aktion = 0;
                locDocFilesEdit.Mode = 1;                                         // 1 - Clients, 2 - Products
                locDocFilesEdit.Client_ID = Client.Record_ID;   
                locDocFilesEdit.DocTypes = 0;
                locDocFilesEdit.PD_Group_ID = 0;
                locDocFilesEdit.DMS_Files_ID = 0;
                locDocFilesEdit.txtFileName.Text = "";
                locDocFilesEdit.Code = "";
                locDocFilesEdit.ClientFullName = sClientFullName;
                locDocFilesEdit.ShowDialog();
                if (locDocFilesEdit.Aktion == 1) ShowDocFiles();
            }
        }
        private void tsbAddFolder_Click(object sender, EventArgs e)
        {
            txtDocFilesPath.Text = "";
            chkArxeio.Checked = false;
            panFolder.Visible = true;
        }

        private void tsbEditDocFile_Click(object sender, EventArgs e)
        {
            EditDocFile();
        }
        private void fgDocFiles_DoubleClick(object sender, EventArgs e)
        {
            EditDocFile();
        }
        private void tsbDelDocFile_Click(object sender, EventArgs e)
        {
            if (fgDocFiles.Row > 0)
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    clsClientsDocFiles klsClientDocFiles = new clsClientsDocFiles();
                    klsClientDocFiles.Record_ID = Convert.ToInt32(fgDocFiles[fgDocFiles.Row, "ID"]);
                    klsClientDocFiles.DeleteRecord();
                    fgDocFiles.RemoveItem(fgDocFiles.Row);
                }
        }
        private void tsbShowDocFile_Click(object sender, EventArgs e)
        {
            if (fgDocFiles.Rows.Count > 1)
            {
                if (Convert.ToBoolean(fgDocFiles[fgDocFiles.Row, 4]))
                    Global.DMS_ShowFile("Customers/" + sClientFullName + "/" + fgDocFiles[fgDocFiles.Row, 1] + "/OldDocs", fgDocFiles[fgDocFiles.Row, 3] + "");
                else  if ((fgDocFiles[fgDocFiles.Row, 1] + "") != "")
                           Global.DMS_ShowFile("Customers/" + sClientFullName + "/" + fgDocFiles[fgDocFiles.Row, 1], fgDocFiles[fgDocFiles.Row, 3] + "");
                      else
                            Global.DMS_ShowFile("Customers/" + sClientFullName, fgDocFiles[fgDocFiles.Row, 3] + "");
            }
            else MessageBox.Show("Προβολή αρχείου δεν είναι δυνατών", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void picDocFilesPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.  
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtDocFilesPath.Text = folderDlg.SelectedPath;
                Environment.SpecialFolder root = folderDlg.RootFolder;
            }
        }
        private void btnOKDoc_Click(object sender, EventArgs e) 
        {
            DataRow[] foundRows;
            string[] Files = Directory.GetFiles(txtDocFilesPath.Text);
            int iDocType;
            string fName;

            foreach (string file in Files)
            {
                fName = Path.GetFileName(file);

                foundRows = Global.dtDocTypes.Select("Title='" + System.IO.Path.GetFileNameWithoutExtension(fName.Trim()) + "'");
                if (foundRows.Length > 0)  iDocType = Convert.ToInt32(foundRows[0]["ID"]);
                else iDocType = 0;

                clsClientsDocFiles klsClientDocFiles = new clsClientsDocFiles();
                klsClientDocFiles.PreContract_ID = 0;
                klsClientDocFiles.Contract_ID = 0;
                klsClientDocFiles.Client_ID = iClient_ID;
                klsClientDocFiles.ClientName = sClientFullName + "";
                klsClientDocFiles.ContractCode = sCode + "";
                klsClientDocFiles.DocTypes = iDocType;
                klsClientDocFiles.DMS_Files_ID = 0;
                klsClientDocFiles.OldFileName = "";
                klsClientDocFiles.NewFileName = fName + "";
                klsClientDocFiles.FullFileName = file + "";
                klsClientDocFiles.DateIns = DateTime.Now;
                klsClientDocFiles.User_ID = Global.User_ID;
                klsClientDocFiles.Status = 2;                                           // 2 - document confirmed
                klsClientDocFiles.InsertRecord();
            }

            ShowDocFiles();
            panFolder.Visible = false;
        }

        private void btnCancelDoc_Click(object sender, EventArgs e)
        {
            panFolder.Visible = false;
        }
        private void EditDocFile()
        {
            if (fgDocFiles.Row > 0)
            {
                frmDocFilesEdit locDocFilesEdit = new frmDocFilesEdit();
                locDocFilesEdit.Aktion = 1;
                locDocFilesEdit.Mode = 1;                            // 1 - Clients, 2 - Products
                locDocFilesEdit.Rec_ID = Convert.ToInt32(fgDocFiles[fgDocFiles.Row, 5]);
                locDocFilesEdit.Client_ID = Client.Record_ID;
                locDocFilesEdit.ClientFullName = sClientFullName;
                locDocFilesEdit.DocTypes = Convert.ToInt32(fgDocFiles[fgDocFiles.Row, 6]);
                locDocFilesEdit.DMS_Files_ID = Convert.ToInt32(fgDocFiles[fgDocFiles.Row, 7]);
                locDocFilesEdit.txtFileName.Text = fgDocFiles[fgDocFiles.Row, 3] + "";
                locDocFilesEdit.chkOldFiles.Checked = Convert.ToBoolean(fgDocFiles[fgDocFiles.Row, 4]);
                locDocFilesEdit.ShowDialog();
                if (locDocFilesEdit.Aktion == 1)  ShowDocFiles();
            }
        }
        public void ShowDocFiles()
        {
            fgDocFiles.Redraw = false;
            fgDocFiles.Rows.Count = 1;

            clsClientsDocFiles klsClientDocFiles = new clsClientsDocFiles();
            klsClientDocFiles.Client_ID = iClient_ID;
            klsClientDocFiles.PreContract_ID = 0;
            klsClientDocFiles.Contract_ID = 0;
            klsClientDocFiles.DocTypes = 0;
            klsClientDocFiles.GetList();
            foreach (DataRow dtRow in klsClientDocFiles.List.Rows)
                if (Convert.ToInt16(dtRow["Status"]) == 2 && (Convert.ToInt16(dtRow["OldFile"]) == 0 || (chkOldFiles.Checked && Convert.ToInt16(dtRow["OldFile"]) == 1)))
                    fgDocFiles.AddItem(Convert.ToDateTime(dtRow["DateIns"]).ToString("dd/MM/yyyy") + "\t" + dtRow["Code"] + "\t" + dtRow["Tipos"] + "\t" +
                                       dtRow["FileName"] + "\t" + (Convert.ToInt16(dtRow["OldFile"]) == 1? true : false) + "\t" +
                                       dtRow["ID"] + "\t" + dtRow["DocTypes"] + "\t" + dtRow["DMS_Files_ID"]);
    
           fgDocFiles.Redraw = true;
        }
        // --- fgBankAccounts functionality -----------------------------------------------
        private void tsbAddAcc_Click(object sender, EventArgs e)
        {
            
            iAktion = 0;
            txtAccNumber.Text = "";
            cmbBanks.SelectedValue = 0;
            txtStartBalance.Text = "0";
            cmbCurrencies.Text = "EUR";
            cmbType.SelectedIndex = 0;
            txtOwners.Text = "";
            cmbBankAcc_Status.SelectedIndex = 1;
            panEdit_BankAccount.Visible = true;
        }

        private void tsbEditAcc_Click(object sender, EventArgs e)
        {
            iAktion = 1;
            EditAccount();
        }
        private void fgBankAccounts_DoubleClick(object sender, EventArgs e)
        {
            iAktion = 1;
            EditAccount();
        }

        private void chkCancelAccs_CheckedChanged(object sender, EventArgs e)
        {
            ShowBankAccounts();
        }

        private void tsbFPSpecialCategories_Click(object sender, EventArgs e)
        {

        }

        private void tsbDelAcc_Click(object sender, EventArgs e)
        {
            if (fgBankAccounts.Row > 0)
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    clsClients_BankAccounts Clients_BankAccounts = new clsClients_BankAccounts();
                    Clients_BankAccounts.Record_ID = Convert.ToInt32(fgBankAccounts[fgBankAccounts.Row, "ID"]);
                    Clients_BankAccounts.DeleteRecord();
                    fgBankAccounts.RemoveItem(fgBankAccounts.Row);
                }
        }
        private void EditAccount()
        {
            if (fgBankAccounts.Row > 0)            {

                txtAccNumber.Text = fgBankAccounts[fgBankAccounts.Row, "AccNumber"] + "";
                cmbBanks.SelectedValue = Convert.ToInt32(fgBankAccounts[fgBankAccounts.Row, "Bank_ID"]);
                txtStartBalance.Text = fgBankAccounts[fgBankAccounts.Row, "StartBalance"] + "";
                cmbCurrencies.Text = fgBankAccounts[fgBankAccounts.Row, "Currency"] + "";
                cmbType.Text = fgBankAccounts[fgBankAccounts.Row, "AccType"] + "";
                txtOwners.Text = fgBankAccounts[fgBankAccounts.Row, "AccOwners"] + "";
                cmbBankAcc_Status.SelectedIndex = Convert.ToInt32(fgBankAccounts[fgBankAccounts.Row, "Status"]);
                panEdit_BankAccount.Visible = true;
            }
        }
        private void btnSave_BankAccount_Click(object sender, EventArgs e)
        {
            clsClients_BankAccounts Clients_BankAccounts = new clsClients_BankAccounts();
            if (iAktion == 0)
            {
                Clients_BankAccounts.Client_ID = iClient_ID;
                Clients_BankAccounts.Bank_ID = Convert.ToInt32(cmbBanks.SelectedValue);
                Clients_BankAccounts.AccNumber = txtAccNumber.Text;
                Clients_BankAccounts.StartBalance = Convert.ToDecimal(txtStartBalance.Text);
                Clients_BankAccounts.Currency = cmbCurrencies.Text;
                Clients_BankAccounts.AccType = cmbType.SelectedIndex;
                Clients_BankAccounts.AccOwners = txtOwners.Text;
                Clients_BankAccounts.Status = cmbBankAcc_Status.SelectedIndex;
                Clients_BankAccounts.InsertRecord();
            }
            else
            {
                Clients_BankAccounts.Record_ID = Convert.ToInt32(fgBankAccounts[fgBankAccounts.Row, "ID"]);
                Clients_BankAccounts.GetRecord();
                Clients_BankAccounts.Client_ID = iClient_ID;
                Clients_BankAccounts.Bank_ID = Convert.ToInt32(cmbBanks.SelectedValue);
                Clients_BankAccounts.AccNumber = txtAccNumber.Text;
                Clients_BankAccounts.StartBalance = Convert.ToDecimal(txtStartBalance.Text);
                Clients_BankAccounts.Currency = cmbCurrencies.Text;
                Clients_BankAccounts.AccType = cmbType.SelectedIndex;
                Clients_BankAccounts.AccOwners = txtOwners.Text;
                Clients_BankAccounts.Status = cmbBankAcc_Status.SelectedIndex;
                Clients_BankAccounts.EditRecord();
            }
            panEdit_BankAccount.Visible = false;
            ShowBankAccounts();
        }
        private void btnCancel_BankAccount_Click(object sender, EventArgs e)
        {
            panEdit_BankAccount.Visible = false;
        }
        public void ShowBankAccounts()
        {
            fgBankAccounts.Redraw = false;
            fgBankAccounts.Rows.Count = 1;

            clsClients_BankAccounts Clients_BankAccounts = new clsClients_BankAccounts();
            Clients_BankAccounts.Client_ID = iClient_ID;
            Clients_BankAccounts.GetList();

            foreach (DataRow dtRow in Clients_BankAccounts.List.Rows)
            {
                if (Convert.ToInt32(dtRow["ID"]) != 0 && (chkCancelAccs.Checked || Convert.ToInt32(dtRow["Status"]) == 1))
                   fgBankAccounts.AddItem(dtRow["AccNumber"] + "\t" + dtRow["BankTitle"] + "\t" + dtRow["StartBalance"] + "\t" +
                                          dtRow["Currency"] + "\t" + (Convert.ToInt32(dtRow["AccType"]) == 0 ? "ΟΧΙ" : "ΝΑΙ") + "\t" +
                                          dtRow["AccOwners"] + "\t" + dtRow["Status"] + "\t" + dtRow["ID"] + "\t" + dtRow["Bank_ID"]);
            }
            fgBankAccounts.Redraw = true;
        }
        // --- txtNotes functionality -----------------------------------------------
        private void tsbEditNotes_Click(object sender, EventArgs e)
        {
            txtNotes.Enabled = true;
        }

        private void tsbSaveNotes_Click(object sender, EventArgs e)
        {
            Client.Notes = txtNotes.Text + "";
            Client.EditRecord();

            txtNotes.Enabled = false;
        }
        // ------------------------------------------------------------------------------
        public void ShowInfluenceCenters()
        {

        }
        public void ShowDependentsList()
        {

        }
        public int Record_ID { get { return iRecord_ID; } set { iRecord_ID = value; } }
        public int Client_ID { get { return iClient_ID; } set { iClient_ID = value; } }
        public bool CheckTrack { get { return bCheckTrack; } set { bCheckTrack = value; } }
        public string Users_List { get { return sUsers_List; } set { sUsers_List = value; } }
    }
}
