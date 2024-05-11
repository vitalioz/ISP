using System;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace Core
{
    public partial class frmContract : Form
    {
        int i, iAktion, iContract_ID, iContract_Details_ID, iContract_Packages_ID, iClient_ID, iClientType, iDocFiles_ID, iRightsLevel, iContractType, iService_ID, 
            iEditMode, iRealClient_ID, iRP_Aktion, iQuestionary_ID, iFinishAktion, iOldContract_Details_ID, iOldContract_Packages_ID;
        string sTemp, sClientFullName, sFullFileName, sOldContractTitle, sOldCode, sUsers_List, sError;
        bool bEditPackages, bEditFees, bSpecificConstraints, bCheckRepPersons;
        DataView dtView;
        clsContracts Contracts = new clsContracts();
        clsClients Clients = new clsClients();
        clsContracts_Packages Contracts_Packages = new clsContracts_Packages();
        clsContracts_ComplexSigns Contract_ComplexSign = new clsContracts_ComplexSigns();
        clsRepresentPersons RepresentPersons = new clsRepresentPersons();

        public frmContract()
        {
            InitializeComponent();

            this.Width = 1288;
            this.Height = 800;

            panNotesFinish.Left = 730;
            panNotesFinish.Top = 40;

            panAtomiki.Left = 6;
            panAtomiki.Top = 32;

            panKoini.Left = 6;
            panKoini.Top = 32;

            panCompany.Left = 6;
            panCompany.Top = 32;

            panCommon.Left = 780;
            panCommon.Top = 32;

            panNotes.Left = 736;
            panNotes.Top = 188;

            panRepresent.Left = 322;
            panRepresent.Top = 122;

            panBlocks.Left = 990;
            panBlocks.Top = 60;

            tsbXM_Key.Visible = true;
            tsbXM_Save.Visible = false;
            panXM.Enabled = false;

            bEditPackages = false;
            bEditFees = false;
            bCheckRepPersons = false;
            sFullFileName = "";
            sOldContractTitle = "";
            sOldCode = "";
            iEditMode = 0;
            cmbContractType.SelectedIndex = 0;
            iRealClient_ID = 0;
            iQuestionary_ID = 0;
            iFinishAktion = 0;

            string[] sZtatus = { "Νέα πρόταση", "Αναμονή αποστολής", "Στάλθηκε", "Δεν στάλθηκε", "Άκυρο" };

            lblCode.Text = Global.GetLabel("code");
            lblSubaccount.Text = Global.GetLabel("subaccount");
            chkStatus.Text = Global.GetLabel("active");
            lblType.Text = Global.GetLabel("type");

            this.FormClosing += new FormClosingEventHandler(frmContracts_FormClosing);

        }
        private void frmContracts_Load(object sender, EventArgs e)
        {
            //------- fgDocs ----------------------------
            fgDocs.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgDocs.Styles.ParseString(Global.GridStyle);

            //------- fgDocFiles ----------------------------
            fgDocFiles.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgDocFiles.Styles.ParseString(Global.GridStyle);
            fgDocFiles.Rows.Count = 1;


            //------- fgInformings ----------------------------
            fgInformings.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgInformings.Styles.ParseString(Global.GridStyle);
            fgInformings.Rows.Count = 1;

            //------- fgInvestIdees ----------------------------
            fgInvestIdees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgInvestIdees.Styles.ParseString(Global.GridStyle);
            fgInvestIdees.Rows.Count = 1;

            //------- fgDateSums ----------------------------
            fgDateSums.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgDateSums.Styles.ParseString(Global.GridStyle);
            fgDateSums.Rows.Count = 1;

            //------- fgDateAssets ----------------------------
            fgDateAssets.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgDateAssets.Styles.ParseString(Global.GridStyle);
            fgDateAssets.Rows.Count = 1;

            //------- fgDateCash ----------------------------
            fgDateCash.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgDateCash.Styles.ParseString(Global.GridStyle);
            fgDateCash.Rows.Count = 1;

            //------- fgTransactions_Titles ----------------------------
            fgMovements.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgMovements.Styles.ParseString(Global.GridStyle);
            fgMovements.Rows.Count = 1;

            //------- fgTransactions_Cash ----------------------------
            fgStatements.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgStatements.Styles.ParseString(Global.GridStyle);
            fgStatements.Rows.Count = 1;

            //------- fgAttachedFiles ----------------------------
            fgAttachedFiles.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgAttachedFiles.Styles.ParseString(Global.GridStyle);
            fgAttachedFiles.Rows.Count = 1;
            fgAttachedFiles.DoubleClick += new System.EventHandler(fgAttachedFiles_DoubleClick);

            //------- fgRepresents ----------------------------
            fgRepresents.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgRepresents.Styles.ParseString(Global.GridStyle);
            fgRepresents.Rows.Count = 1;
            fgRepresents.DoubleClick += new System.EventHandler(fgRepresents_DoubleClick);

            //------- fgKEMOwners ----------------------------
            fgKEMOwners.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgKEMOwners.Styles.ParseString(Global.GridStyle);
            fgKEMOwners.Rows.Count = 1;
            fgKEMOwners.DoubleClick += new System.EventHandler(fgKEMOwners_DoubleClick);

            //------- fgBlocks ----------------------------
            fgBlocks.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgBlocks.Styles.ParseString(Global.GridStyle);
            fgBlocks.Rows.Count = 1;

            tsbXM_Key.Visible = true;
            tsbXM_Save.Visible = false;
            panXM.Enabled = false;

            dtView = Global.dtUserList.DefaultView;
            dtView.RowFilter = "ID = 0";
            foreach (DataRowView dtViewRow in dtView) dtViewRow["Title"] = "";          //<----  ""

            //-------------- Define Advisors List ------------------
            dtView = Global.dtUserList.Copy().DefaultView;
            dtView.RowFilter = "Chief = 1";
            cmbUser1.DataSource = dtView;
            cmbUser1.DisplayMember = "Title";
            cmbUser1.ValueMember = "ID";

            //-------------- Define RM List ------------------
            dtView = Global.dtUserList.Copy().DefaultView;
            dtView.RowFilter = "RM = 1";
            cmbUser2.DataSource = dtView;
            cmbUser2.DisplayMember = "Title";
            cmbUser2.ValueMember = "ID";

            //-------------- Define Introducer List ------------------
            dtView = Global.dtUserList.Copy().DefaultView;
            dtView.RowFilter = "Introducer = 1";
            cmbUser3.DataSource = dtView;
            cmbUser3.DisplayMember = "Title";
            cmbUser3.ValueMember = "ID";

            //-------------- Define Diaxeiristis List ------------------
            dtView = Global.dtUserList.Copy().DefaultView;
            dtView.RowFilter = "Diaxiristis >= 1";
            cmbUser4.DataSource = dtView;
            cmbUser4.DisplayMember = "Title";
            cmbUser4.ValueMember = "ID";

            dtView = Global.dtUserList.DefaultView;
            dtView.RowFilter = "ID = 0";
            foreach (DataRowView dtViewRow in dtView) dtViewRow["Title"] = "Όλοι";     // <---- "Όλοι"

            //-------------- Define NOMISMA ANAFORAS List ------------------
            cmbCurrencies.DataSource = Global.dtCurrencies.Copy();
            cmbCurrencies.DisplayMember = "Title";
            cmbCurrencies.ValueMember = "ID";

            //-------------- Define DocTypes List ------------------    
            cmbDocTypes.DataSource = Global.dtDocTypes.Copy();
            cmbDocTypes.DisplayMember = "Title";
            cmbDocTypes.ValueMember = "ID";

            //-------------- Define DocTypes List ------------------    
            cmbDocTypesFinish.DataSource = Global.dtDocTypes.Copy();
            cmbDocTypesFinish.DisplayMember = "Title";
            cmbDocTypesFinish.ValueMember = "ID";

            //-------------- Define NOMISMA ANAFORAS List ------------------
            cmbCurrencies.DataSource = Global.dtCurrencies.Copy();
            cmbCurrencies.DisplayMember = "Title";
            cmbCurrencies.ValueMember = "ID";

            //-------------- Define FP Specials List ------------------
            cmbFPSpecials.DataSource = Global.dtSpecials.Copy();
            cmbFPSpecials.DisplayMember = "Title";
            cmbFPSpecials.ValueMember = "ID";

            //-------------- Define FP Specials List ------------------
            cmbFPBrunches.DataSource = Global.dtBrunches.Copy();
            cmbFPBrunches.DisplayMember = "Title";
            cmbFPBrunches.ValueMember = "ID";

            //-------------- Define FP Citizens List ------------------
            cmbFPCitizen.DataSource = Global.dtCountries.Copy();
            cmbFPCitizen.DisplayMember = "Title";
            cmbFPCitizen.ValueMember = "ID";

            //-------------- Define FP CountryTaxes List ------------------
            cmbFPCountryTaxes.DataSource = Global.dtCountries.Copy();
            cmbFPCountryTaxes.DisplayMember = "Title";
            cmbFPCountryTaxes.ValueMember = "ID";

            //-------------- Define FP Xora List ------------------
            cmbFPXora.DataSource = Global.dtCountries.Copy();
            cmbFPXora.DisplayMember = "Title";
            cmbFPXora.ValueMember = "ID";

            //-------------- Define FP Country List ------------------
            cmbInvCountry.DataSource = Global.dtCountries.Copy();
            cmbInvCountry.DisplayMember = "Title";
            cmbInvCountry.ValueMember = "ID";

            //-------------- Define FP Division List ------------------
            cmbDivision.DataSource = Global.dtDivisions.Copy();
            cmbDivision.DisplayMember = "Title";
            cmbDivision.ValueMember = "ID";

            //-------------- Define cmbNPNation List ------------------
            cmbNPNation.DataSource = Global.dtCountries.Copy();
            cmbNPNation.DisplayMember = "Title";
            cmbNPNation.ValueMember = "ID";

            //-------------- Define cmbNPCountryTaxes List ------------------
            cmbNPCountryTaxes.DataSource = Global.dtCountries.Copy();
            cmbNPCountryTaxes.DisplayMember = "Title";
            cmbNPCountryTaxes.ValueMember = "ID";

            //-------------- Define cmbNPXora List ------------------
            cmbNPXora.DataSource = Global.dtCountries.Copy();
            cmbNPXora.DisplayMember = "Title";
            cmbNPXora.ValueMember = "ID";

            //-------------- Define cmbKEMXora List ------------------
            cmbKEMXora.DataSource = Global.dtCountries.Copy();
            cmbKEMXora.DisplayMember = "Title";
            cmbKEMXora.ValueMember = "ID";

            iFinishAktion = 0;

            ucCC.Mode = 1;

            Clients.Record_ID = iClient_ID;
            Clients.EMail = "";
            Clients.Mobile = "";
            Clients.AFM = "";
            Clients.DoB = Convert.ToDateTime("1900/01/01");
            Clients.GetRecord();
            sUsers_List = Clients.Users_List;

            ShowPanels();

            if (iAktion != 0) {

                ucCC.lblContract_ID.Text = iContract_ID.ToString();

                Contracts.Record_ID = iContract_ID;
                Contracts.Contract_Details_ID = iContract_Details_ID;
                Contracts.Contract_Packages_ID = iContract_Packages_ID;
                Contracts.GetRecord();

                iContract_Details_ID = Contracts.Contract_Details_ID;
                iContract_Packages_ID = Contracts.Contract_Packages_ID;
                iOldContract_Details_ID = iContract_Details_ID;
                iOldContract_Packages_ID = iContract_Packages_ID;
                iService_ID = Contracts.Service_ID;
                iContractType = Contracts.ContractType;
                iClientType = Contracts.ClientTipos;

                this.Text = "Σύμβαση (" + iContract_ID + "/ " + iContract_Details_ID + "/ " + iContract_Packages_ID + ")";

                cmbContractType.SelectedIndex = Contracts.ContractType;
                if (iClientType == 2) {
                    lblContractType.Visible = false;
                    cmbContractType.Visible = false;
                    panAtomiki.Visible = false;
                    panKoini.Visible = false;
                    panCompany.Visible = true;
                }
                txtContractTitle.Text = Contracts.ContractTitle;
                txtCode.Text = Contracts.Code;
                sOldContractTitle = Contracts.ContractTitle;
                sOldCode = Contracts.Code;
                txtPortfolio.Text = Contracts.Portfolio;
                txtPortfolio_Alias.Text = Contracts.Portfolio_Alias;
                txtPortfolio_Type.Text = Contracts.Portfolio_Type;
                dDateStart.Value = Contracts.DateStart;
                dDateFinish.Value = Contracts.DateFinish;
                cmbCurrencies.Text = Contracts.Currency;
                iContract_Details_ID = Contracts.Contract_Details_ID;
                iContract_Packages_ID = Contracts.Contract_Packages_ID;
                chkStatus.Checked = ((Contracts.Status == 1) ? true : false);
                chkXAA.Checked = ((Contracts.XAA == 1) ? true : false);
                chkMIIFID_2.Checked = ((Contracts.MiFID_2 == 1) ? true : false);
                dMIFID_2_StartDate.Value = Contracts.MiFID_2_StartDate;
                dMIFID_2_StartDate.Visible = chkMIIFID_2.Checked;

                chkComplex.Checked = ((Contracts.Details.ChkComplex == 1) ? true : false);
                chkWorld.Checked = ((Contracts.Details.ChkWorld == 1) ? true : false);
                chkGreece.Checked = ((Contracts.Details.ChkGreece == 1) ? true : false);
                chkEurope.Checked = ((Contracts.Details.ChkEurope == 1) ? true : false);
                chkAmerica.Checked = ((Contracts.Details.ChkAmerica == 1) ? true : false);
                chkAsia.Checked = ((Contracts.Details.ChkAsia == 1) ? true : false);
                txtIncomeProducts.Text = Contracts.Details.IncomeProducts;
                txtCapitalProducts.Text = Contracts.Details.CapitalProducts;
                if (Contracts.Details.ChkSpecificConstraints == 0) {
                    rbNoSpecificConstraints.Checked = true;
                    rbSpecificConstraints.Checked = false;
                }
                else {
                    rbNoSpecificConstraints.Checked = false;
                    rbSpecificConstraints.Checked = true;
                }

                DefineSpecificConstraints();

                txtNumberAccount.Text = Contracts.NumberAccount;
                txtContractNotes.Text = Contracts.Details.AgreementNotes;
                cmbUser1.SelectedValue = Contracts.Details.User1_ID;
                cmbUser2.SelectedValue = Contracts.Details.User2_ID;
                cmbUser3.SelectedValue = Contracts.Details.User3_ID;
                cmbUser4.SelectedValue = Contracts.Details.User4_ID;
                iQuestionary_ID = Contracts.Questionary_ID;

                if (iClientType == 1) {
                    txtFPSurname.Text = Contracts.Details.Surname;
                    txtFPFirstname.Text = Contracts.Details.Firstname;
                    txtFPFatherSurname.Text = Contracts.Details.SurnameFather;
                    txtFPFatherFirstname.Text = Contracts.Details.FirstnameFather;
                    txtFPMotherSurname.Text = Contracts.Details.SurnameMother;
                    txtFPMotherFirstname.Text = Contracts.Details.FirstnameMother;
                    txtFPSyzygosSurname.Text = Contracts.Details.SurnameSizigo;
                    txtFPSyzygosFirstname.Text = Contracts.Details.FirstnameSizigo;
                    cmbFPSpecials.SelectedValue = Contracts.Details.Spec_ID;
                    cmbFPBrunches.SelectedValue = Contracts.Details.Brunch_ID;
                    dFPDoB.Value = Contracts.Details.DoB;
                    txtFPBornPlace.Text = Contracts.Details.BornPlace;
                    cmbFPCitizen.SelectedValue = Contracts.Details.Citizen_ID;
                    cmbFPSex.Text = Contracts.Details.Sex;
                    txtFPADT.Text = Contracts.Details.ADT;
                    txtFPExpireDate.Text = Contracts.Details.ExpireDate;
                    txtFPPolice.Text = Contracts.Details.Police;
                    txtFPPassport.Text = Contracts.Details.Passport;
                    txtFPPassport_ExpireDate.Text = Contracts.Details.Passport_ExpireDate;
                    txtFPPassport_Police.Text = Contracts.Details.Passport_Police;
                    txtFPAFM.Text = Contracts.Details.AFM;
                    txtFPDOY.Text = Contracts.Details.DOY;
                    txtFPAFM2.Text = Contracts.Details.AFM2;
                    txtFPDOY2.Text = Contracts.Details.DOY2;
                    txtFPAMKA.Text = Contracts.Details.AMKA;
                    cmbFPCountryTaxes.SelectedValue = Contracts.Details.CountryTaxes_ID;
                    txtFPAddress.Text = Contracts.Details.Address;
                    txtFPCity.Text = Contracts.Details.City;
                    txtFPZip.Text = Contracts.Details.Zip;
                    cmbFPXora.SelectedValue = Contracts.Details.Country_ID;
                    txtFPTel.Text = Contracts.Details.Tel;
                    txtFPFax.Text = Contracts.Details.Fax;
                    txtFPMobile.Text = Contracts.Details.Mobile;
                    txtFPEMail.Text = Contracts.Details.EMail;
                    chkFPSMS.Checked = ((Contracts.Details.SendSMS == 1) ? true : false);
                    cmbFPConnectionMethod.SelectedIndex = Contracts.Details.ConnectionMethod;

                    txtKEMSurname.Text = Contracts.Details.Surname;
                    txtKEMRecipient.Text = Contracts.Details.BornPlace;
                    txtKEMAddress.Text = Contracts.Details.Address;
                    txtKEMCity.Text = Contracts.Details.City;
                    txtKEMZip.Text = Contracts.Details.Zip;
                    cmbKEMXora.SelectedValue = Contracts.Details.Country_ID;
                    txtKEMTel.Text = Contracts.Details.Tel;
                    txtKEMFax.Text = Contracts.Details.Fax;
                    txtKEMMobile.Text = Contracts.Details.Mobile;
                    txtKEMEMail.Text = Contracts.Details.EMail;
                    chkKEMSMS.Checked = ((Contracts.Details.SendSMS == 1) ? true : false);
                    cmbKEMConnectionMethod.SelectedIndex = Contracts.Details.ConnectionMethod;
                    txtKEMMerida.Text = Contracts.Details.Merida;
                    txtKEMLogAxion.Text = Contracts.Details.LogAxion;

                    ShowOwners();
                }
                else {
                    txtNPTitle.Text = Contracts.Details.Surname;
                    txtNPEdra.Text = Contracts.Details.SurnameFather;
                    txtNPMorfi.Text = Contracts.Details.FirstnameFather;
                    cmbNPBrunches.SelectedValue = Contracts.Details.Brunch_ID;
                    txtNPAM.Text = Contracts.Details.ADT;
                    txtNPIssueDate.Text = Contracts.Details.ExpireDate;
                    txtNPArmodiaArxi.Text = Contracts.Details.Police;
                    cmbNPNation.SelectedValue = Contracts.Details.Citizen_ID;
                    txtNPAFM.Text = Contracts.Details.AFM;
                    txtNPDOY.Text = Contracts.Details.DOY;
                    cmbNPCountryTaxes.SelectedValue = Contracts.Details.CountryTaxes_ID;
                    txtNPReciever.Text = Contracts.Details.BornPlace;
                    txtNPAddress.Text = Contracts.Details.Address;
                    txtNPCity.Text = Contracts.Details.City;
                    txtNPZip.Text = Contracts.Details.Zip;
                    cmbNPXora.SelectedValue = Contracts.Details.Country_ID;
                    txtNPTel.Text = Contracts.Details.Tel;
                    txtNPMobile.Text = Contracts.Details.Mobile;
                    chkNPSMS.Checked = ((Contracts.Details.SendSMS == 1) ? true : false);
                    txtNPFax.Text = Contracts.Details.Fax;
                    txtNPEMail.Text = Contracts.Details.EMail;
                    cmbNPConnectionMethod.SelectedIndex = Contracts.Details.ConnectionMethod;
                }

                cmbDivision.SelectedValue = Contracts.Details.Division;
                cmbRisk.SelectedIndex = Contracts.Details.Risk;
                cmbMiFiDCategory.SelectedIndex = Contracts.Details.MIFIDCategory_ID;
                txtFPA.Text = Contracts.Details.VAT_Percent.ToString();

                txtInvName.Text = Contracts.Details.InvName;
                txtInvAddress.Text = Contracts.Details.InvAddress;
                txtInvCity.Text = Contracts.Details.InvCity;
                txtInvZip.Text = Contracts.Details.InvZip;
                cmbInvCountry.SelectedValue = Contracts.Details.InvCountry_ID;
                txtInvAFM.Text = Contracts.Details.InvAFM;
                txtInvDOY.Text = Contracts.Details.InvDOY;
                Contracts_Packages.Record_ID = iContract_Packages_ID; //   Contracts.Contract_Packages_ID
                Contracts_Packages.GetRecord();
                ucCC.chkMIIFID_2.Checked = (Contracts.MiFID_2 == 1 ? true : false);
                ucCC.lblContract_ID.Text = Contracts_Packages.Contract_ID.ToString();
                ucCC.cmbProfile.SelectedValue = Contracts_Packages.Profile_ID;

                //-------------------------------------------                
                Contract_ComplexSign.Contract_ID = iContract_ID;
                Contract_ComplexSign.GetList();

                foreach (DataRow dtRow in Contract_ComplexSign.List.Rows)
                    for (i = 1; i <= fgXM.Rows.Count - 1; i++)
                        if (Convert.ToInt32(dtRow["ComplexSign_ID"]) == Convert.ToInt32(fgXM[i, 2])) fgXM[i, 0] = true;

                //-----------------------------------------------
                panAtomiki.Enabled = false;
                panKoini.Enabled = false;
                panCompany.Enabled = false;
                panCommon.Enabled = false;
                tsbHistory.Enabled = true;
                tsbKey.Visible = true;
                tsbSave.Visible = false;
                tsbHelp.Enabled = true;
                panContractGeneral.Enabled = false;
                toolPackage.Visible = true;
                tsbKeyGeneral.Visible = true;
                tsbSaveGeneral.Visible = false;
                tsbSave_Package.Enabled = false;
                tsbXM_Key.Visible = true;
                tsbXM_Save.Visible = false;
                toolXM.Visible = true;
            }
            else {                                                                                         // ADD Contract
                iEditMode = 999;

                txtContractTitle.Text = sClientFullName;
                dDateStart.Value = DateTime.Now;
                dDateFinish.Value = Convert.ToDateTime("31/12/2070");
                cmbCurrencies.Text = "EUR";
                chkStatus.Checked = true;
                chkXAA.Checked = false;

                ucCC.dPackageDateStart.Value = DateTime.Now;
                ucCC.dPackageDateFinish.Value = Convert.ToDateTime("31/12/2070");

                dMIFID_2_StartDate.Value = Convert.ToDateTime("01/01/1900");
                dMIFID_2_StartDate.Visible = false;
                chkMIIFID_2.Visible = false;

                if (iClientType == 1) {
                    iContractType = 0;                                                                           // 0 - ATOMIKH
                    lblContractType.Visible = true;
                    cmbContractType.Visible = true;

                    panAtomiki.Visible = true;
                    panKoini.Visible = false;
                    panCompany.Visible = false;

                    txtFPSurname.Text = Clients.Surname;
                    txtFPFirstname.Text = Clients.Firstname;
                    txtFPFatherSurname.Text = Clients.SurnameFather;
                    txtFPFatherFirstname.Text = Clients.FirstnameFather;
                    txtFPMotherSurname.Text = Clients.SurnameMother;
                    txtFPMotherFirstname.Text = Clients.FirstnameMother;
                    txtFPSyzygosSurname.Text = Clients.SurnameSizigo;
                    txtFPSyzygosFirstname.Text = Clients.FirstnameSizigo;
                    cmbFPSpecials.SelectedValue = Clients.Spec_ID;
                    txtFPADT.Text = Clients.ADT;
                    txtFPExpireDate.Text = Clients.ExpireDate;
                    txtFPPolice.Text = Clients.Police;
                    txtFPPassport.Text = Contracts.Details.Passport;
                    txtFPPassport_ExpireDate.Text = Contracts.Details.Passport_ExpireDate;
                    txtFPPassport_Police.Text = Contracts.Details.Passport_Police;
                    cmbFPCitizen.SelectedValue = Clients.Citizen_ID;
                    cmbFPSpecials.SelectedValue = Clients.Spec_ID;
                    cmbFPBrunches.SelectedValue = Clients.Brunch_ID;
                    dFPDoB.Value = Clients.DoB;
                    txtFPBornPlace.Text = Clients.BornPlace;
                    cmbFPSex.Text = Clients.Sex;
                    txtFPAFM.Text = Clients.AFM;
                    txtFPDOY.Text = Clients.DOY;
                    txtFPAFM2.Text = Clients.AFM2;
                    txtFPDOY2.Text = Clients.DOY2;
                    txtFPA.Text = Clients.VAT_Percent.ToString();
                    txtFPAMKA.Text = Clients.AMKA;
                    cmbFPCountryTaxes.SelectedValue = Clients.CountryTaxes_ID;
                    txtFPAddress.Text = Clients.Address;
                    txtFPCity.Text = Clients.City;
                    txtFPZip.Text = Clients.Zip;
                    cmbFPXora.SelectedValue = Clients.Country_ID;
                    txtFPTel.Text = Clients.Tel;
                    txtFPMobile.Text = Clients.Mobile;
                    chkFPSMS.Checked = ((Clients.SendSMS == 1) ? true : false);
                    txtFPFax.Text = Clients.Fax;
                    txtFPEMail.Text = Clients.EMail;
                    cmbFPConnectionMethod.SelectedIndex = Clients.ConnectionMethod;

                    txtNPTitle.Text = Clients.Surname;
                    txtNPEdra.Text = Clients.SurnameFather;
                    txtNPMorfi.Text = Clients.FirstnameFather;
                    cmbNPBrunches.SelectedValue = Clients.Brunch_ID;
                    txtNPAM.Text = Clients.ADT;
                    txtNPIssueDate.Text = Clients.ExpireDate;
                    txtNPArmodiaArxi.Text = Clients.Police;
                    txtNPAFM.Text = Clients.AFM;
                    txtNPDOY.Text = Clients.DOY;
                    txtNPReciever.Text = Clients.BornPlace;
                    cmbNPNation.SelectedValue = Clients.Citizen_ID;
                    cmbNPCountryTaxes.SelectedValue = Clients.CountryTaxes_ID;
                    txtNPAddress.Text = Clients.Address;
                    txtNPCity.Text = Clients.City;
                    txtNPZip.Text = Clients.Zip;
                    txtNPTel.Text = Clients.Tel;
                    cmbNPXora.SelectedValue = Clients.Country_ID;
                    txtNPMobile.Text = Clients.Mobile;
                    chkNPSMS.Checked = ((Clients.SendSMS == 1) ? true : false);
                    txtNPFax.Text = Clients.Fax;
                    txtNPEMail.Text = Clients.EMail;
                    cmbNPConnectionMethod.SelectedIndex = Clients.ConnectionMethod;

                    txtKEMSurname.Text = (Clients.Surname + " " + Clients.Firstname).Trim();
                    txtKEMRecipient.Text = (Clients.Surname + " " + Clients.Firstname).Trim();
                    txtKEMAddress.Text = Clients.Address;
                    txtKEMCity.Text = Clients.City;
                    txtKEMZip.Text = Clients.Zip;
                    txtKEMTel.Text = Clients.Tel;
                    cmbKEMXora.SelectedValue = Clients.Country_ID;
                    txtKEMMobile.Text = Clients.Mobile;
                    chkKEMSMS.Checked = ((Clients.SendSMS == 1) ? true : false);
                    txtKEMFax.Text = Clients.Fax;
                    txtKEMEMail.Text = Clients.EMail;
                    cmbKEMConnectionMethod.SelectedIndex = Clients.ConnectionMethod;
                    txtKEMMerida.Text = Clients.Merida;
                    txtKEMLogAxion.Text = Clients.LogAxion;

                    cmbDivision.SelectedValue = Clients.Division;
                    cmbRisk.SelectedIndex = 2;
                    cmbMiFiDCategory.SelectedIndex = 1;
                    txtFPA.Text = Clients.VAT_Percent.ToString();

                    switch (Convert.ToInt32(cmbContractType.SelectedIndex))
                    {
                        case 0:
                            txtInvName.Text = txtContractTitle.Text;
                            txtInvAddress.Text = txtFPAddress.Text;
                            txtInvCity.Text = txtFPCity.Text;
                            txtInvZip.Text = txtFPZip.Text;
                            cmbInvCountry.SelectedValue = cmbFPXora.SelectedValue;
                            txtInvAFM.Text = txtFPAFM.Text;
                            txtInvDOY.Text = txtFPDOY.Text;
                            break;
                        case 1:
                            txtInvName.Text = txtContractTitle.Text;
                            txtInvAddress.Text = txtNPAddress.Text;
                            txtInvCity.Text = txtNPCity.Text;
                            txtInvZip.Text = txtNPZip.Text;
                            cmbInvCountry.SelectedValue = cmbNPXora.SelectedValue;
                            txtInvAFM.Text = txtNPAFM.Text;
                            txtInvDOY.Text = txtNPDOY.Text;
                            break;
                        case 2:
                            txtInvName.Text = txtContractTitle.Text;
                            txtInvAddress.Text = txtKEMAddress.Text;
                            txtInvCity.Text = txtKEMCity.Text;
                            txtInvZip.Text = txtKEMZip.Text;
                            cmbInvCountry.SelectedValue = cmbKEMXora.SelectedValue;
                            txtInvAFM.Text = "";
                            txtInvDOY.Text = "";
                            break;
                    }

                    fgKEMOwners.Rows.Count = 1;
                    fgKEMOwners.AddItem(txtKEMSurname.Text + "\t" + Clients.FirstnameFather + "\t" + Clients.ADT + "\t" + Clients.Passport + "\t" + Clients.DOY + "\t" +
                                    Clients.AFM + "\t" + false + "\t" + true + "\t" + "0" + "\t" + iClient_ID + "\t" + Clients.DoB + "\t" + Clients.Spec_Title);
                }
                else {
                    iContractType = 2;                                                                           // 2 - ETAIRIKH
                    lblContractType.Visible = false;
                    cmbContractType.Visible = false;

                    panAtomiki.Visible = false;
                    panKoini.Visible = false;
                    panCompany.Visible = true;

                    txtNPTitle.Text = Clients.Surname;
                    txtNPTitle.Text = Clients.Firstname;
                    txtNPEdra.Text = Clients.SurnameFather;
                    txtNPMorfi.Text = Clients.FirstnameFather;
                    cmbMiFiDCategory.SelectedIndex = 1;
                    cmbDivision.SelectedValue = Clients.Division;
                    cmbNPBrunches.SelectedValue = Clients.Brunch_ID;
                    txtNPAM.Text = Clients.ADT;
                    txtNPIssueDate.Text = Clients.ExpireDate;
                    txtNPArmodiaArxi.Text = Clients.Police;
                    txtNPAFM.Text = Clients.AFM;
                    txtNPDOY.Text = Clients.DOY;
                    txtFPA.Text = Convert.ToString(Clients.VAT_Percent);
                    txtNPReciever.Text = Clients.BornPlace;
                    cmbNPNation.SelectedValue = Clients.Citizen_ID;
                    cmbNPCountryTaxes.SelectedValue = Clients.CountryTaxes_ID;
                    txtNPAddress.Text = Clients.Address;
                    txtNPCity.Text = Clients.City;
                    txtNPZip.Text = Clients.Zip;
                    txtNPTel.Text = Clients.Tel;
                    cmbNPXora.SelectedValue = Clients.Country_ID;
                    txtNPMobile.Text = Clients.Mobile;
                    chkNPSMS.Checked = (Clients.SendSMS == 1 ? true : false);
                    txtNPFax.Text = Clients.Fax;
                    txtNPEMail.Text = Clients.EMail;
                    cmbNPConnectionMethod.SelectedIndex = Clients.ConnectionMethod;
                    cmbRisk.SelectedIndex = 2;
                    txtInvName.Text = txtContractTitle.Text;
                    txtInvAddress.Text = txtNPAddress.Text;
                    txtInvCity.Text = txtNPCity.Text;
                    txtInvZip.Text = txtNPZip.Text;
                    cmbInvCountry.SelectedValue = cmbNPXora.SelectedValue;
                    txtInvAFM.Text = txtNPAFM.Text;
                    txtInvDOY.Text = txtNPDOY.Text;
                }
                rbNoSpecificConstraints.Checked = true;

                panAtomiki.Enabled = true;
                panKoini.Enabled = true;
                panCompany.Enabled = true;
                tsbKeyGeneral.Enabled = false;

                tsbHistory.Enabled = false;
                tsbKey.Enabled = false;
                tsbKey.Visible = false;
                tsbSave.Enabled = true;
                tsbSave.Visible = true;
                tsbHelp.Enabled = true;
                panContractGeneral.Enabled = true;
                toolGeneralData.Visible = false;
                toolPackage.Visible = false;

                toolXM.Visible = false;
                panXM.Enabled = true;

                cmbContractType.SelectedIndex = iContractType;
            }

            if (iRightsLevel <= 1)
            {
                tsbKey.Enabled = false;
                tsbKeyGeneral.Enabled = false;
                tslEditPackage.Enabled = false;
                tslEditVersion.Enabled = false;
            }
        }
        private void frmContracts_FormClosing(object sender, EventArgs e)
        {
            if (ucCC.lblFinishAktion.Text == "1") iFinishAktion = 1;
        }

        protected override void OnResize(EventArgs e)
        {
            tabContractData.Height = this.Height - 160;
        }

        private void tsbHistory_Click(object sender, EventArgs e)
        {
            frmShowHistory locShowHistory = new frmShowHistory();
            locShowHistory.RecType = 7;                                                     // 7 - Package
            locShowHistory.SrcRec_ID = iContract_ID;
            locShowHistory.Contract_ID = iContract_ID;
            locShowHistory.Client_ID = iClient_ID;
            locShowHistory.Code = txtCode.Text.Trim();
            locShowHistory.ClientFullName = txtContractTitle.Text.Replace(".", "_");
            locShowHistory.ClientsList = 1;                                                 // 1 - Customers List (Main List), 2 - Clients Black List
            locShowHistory.ClientType = iClientType;
            locShowHistory.ShowDialog();
        }
        private void chkComplex_CheckedChanged(object sender, EventArgs e)
        {
            clsComplexSigns Assets = new clsComplexSigns();
            Assets.GetList();

            fgXM.Rows.Count = 1;
            if (chkComplex.Checked) {
                Assets.GetList();
                foreach (DataRow dtRow in Assets.List.Rows)
                    if (Convert.ToInt32(dtRow["ID"]) > 2) fgXM.AddItem(false + "\t" + dtRow["Title"] + "\t" + dtRow["ID"]);
            }
            fgXM.Redraw = true;
        }
        private void tsbKey_Click(object sender, EventArgs e)
        {
            tsbKey.Visible = false;
            tsbSave.Visible = true;
            panContractGeneral.Enabled = true;
        }
        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (iAktion == 0)
            {
                panNotes.Left = 740;
                panNotes.Top = 38;

                if (SaveContract()) {
                    tsbKey.Enabled = true;
                    tsbKey.Visible = true;
                    tsbSave.Visible = false;
                    tabContractData.Enabled = true;
                    panContractGeneral.Enabled = false;
                }
            }
            else {
                clsContracts klsContract = new clsContracts();
                if (chkStatus.Checked) {
                    // --- save changes in "Header" of Contract -------------------------------------------

                    klsContract.Record_ID = iContract_ID;
                    klsContract.Contract_Details_ID = iContract_Details_ID;
                    klsContract.Contract_Packages_ID = iContract_Packages_ID;
                    klsContract.GetRecord();

                    klsContract.PackageType = 1;
                    klsContract.Client_ID = iClient_ID;
                    klsContract.ContractType = cmbContractType.SelectedIndex;      // 0 - ATOMIKH, 1 - ΚΟΙΝΟΣ, 2 - ΕΤΑΙΡΙΚΗ
                    klsContract.ContractTitle = txtContractTitle.Text.Trim();
                    klsContract.Code = txtCode.Text.Trim();
                    klsContract.Portfolio = txtPortfolio.Text.Trim();
                    klsContract.Portfolio_Alias = txtPortfolio_Alias.Text.Trim();
                    klsContract.Portfolio_Type = txtPortfolio_Type.Text.Trim();
                    klsContract.DateStart = dDateStart.Value;
                    klsContract.DateFinish = dDateFinish.Value;
                    klsContract.Currency = cmbCurrencies.Text;
                    klsContract.NumberAccount = txtNumberAccount.Text.Trim();
                    klsContract.MiFID_2 = chkMIIFID_2.Checked ? 1 : 0;
                    klsContract.XAA = (chkXAA.Checked ? 1 : 0);
                    klsContract.EditRecord();

                    if (!Global.DMS_CheckDirectoryExists("Customers/" + sOldContractTitle.Replace(".", "_") + "/" + sOldCode))
                        Global.DMS_CreateDirectory("Customers/" + txtContractTitle.Text.Replace(".", "_") + "/" + txtCode.Text.Trim());

                    if (sOldContractTitle.Trim() != txtContractTitle.Text.Trim())
                    {
                        if (!Global.DMS_CheckDirectoryExists("Customers/" + txtContractTitle.Text))
                            Global.DMS_RenameFolderName(sOldContractTitle, txtContractTitle.Text);

                        if (sOldCode.Trim() != txtCode.Text.Trim())
                            if (!Global.DMS_CheckDirectoryExists("Customers/" + txtContractTitle.Text.Replace(".", "_") + "/" + txtCode.Text.Trim()))
                                Global.DMS_RenameFolderName(txtContractTitle.Text.Replace(".", "_") + "/" + sOldCode, txtCode.Text.Trim());
                    }
                }
                else {
                    if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε την απενεργοποίηση της σύμβασης.\nΕίστε σίγουρος για αυτό;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        panNotesFinish.Visible = true;
                }

                tsbKey.Visible = true;
                tsbSave.Visible = false;
                panContractGeneral.Enabled = false;
            }
        }
        private void picFilePathFinish_Click(object sender, EventArgs e)
        {
            txtFileNameFinish.Text = Global.FileChoice(Global.DefaultFolder);
        }
        private void btnSaveFinish_Click(object sender, EventArgs e)
        {
            int iDoc_Files_ID = 0;

            dDateFinish.Value = dFinish.Value;
            ucCC.dPackageDateFinish.Value = dFinish.Value;

            clsContracts klsContract = new clsContracts();
            klsContract.Record_ID = iContract_ID;
            klsContract.Contract_Details_ID = iContract_Details_ID;
            klsContract.Contract_Packages_ID = iContract_Packages_ID;
            klsContract.DateFinish = dFinish.Value;
            klsContract.EditRecord_Cancel();

            clsClientsDocFiles ClientsDocFiles = new clsClientsDocFiles();
            ClientsDocFiles.PreContract_ID = 0;
            ClientsDocFiles.Contract_ID = iContract_ID;
            ClientsDocFiles.Client_ID = iClient_ID;
            ClientsDocFiles.ClientName = txtContractTitle.Text.Replace(".", "_");
            ClientsDocFiles.ContractCode = txtCode.Text;
            ClientsDocFiles.DocTypes = Convert.ToInt32(cmbDocTypesFinish.SelectedValue);
            ClientsDocFiles.DMS_Files_ID = 0;
            ClientsDocFiles.OldFileName = "";
            ClientsDocFiles.NewFileName = Path.GetFileName(txtFileNameFinish.Text);
            ClientsDocFiles.FullFileName = txtFileNameFinish.Text;
            ClientsDocFiles.DateIns = DateTime.Now;
            ClientsDocFiles.User_ID = Global.User_ID;
            ClientsDocFiles.Status = 2;                                                    // 2 - document confirmed
            iDoc_Files_ID = ClientsDocFiles.InsertRecord();

            Global.SaveHistory(7, iContract_Packages_ID, iClient_ID, iContract_ID, 2, "", iDoc_Files_ID, txtNotesFinish.Text, DateTime.Now, Global.User_ID);

            panNotesFinish.Visible = false;
        }

        private void btnCancelFinish_Click(object sender, EventArgs e)
        {
            panNotesFinish.Visible = false;
        }
        private void tsbHelp_Click(object sender, EventArgs e)
        {

        }
        private void tsbKeyGeneral_Click(object sender, EventArgs e)
        {
            panAtomiki.Enabled = true;
            panKoini.Enabled = true;
            panCompany.Enabled = true;
            panCommon.Enabled = true;
            tsbKeyGeneral.Visible = false;
            tsbSaveGeneral.Visible = true;
        }

        private void SaveContractData()
        {
            int i, jAktion = 0, iOldContract_ID, iOldContract_Details_ID, iOldContract_Packages_ID;
            iDocFiles_ID = 0;

            iOldContract_ID = iContract_ID;
            iOldContract_Details_ID = iContract_Details_ID;
            iOldContract_Packages_ID = iContract_Packages_ID;

            try
            {
                bEditPackages = false;
                bEditFees = false;

                //iEditMode = ucCC.lblEditMode.Text
                switch (iEditMode)
                {
                    case 999:                                                                   // 999 - Add new Contract
                        jAktion = 0;                                                            // 0 - ADD, 1 - EDIT, 2 - DELETE

                        clsContracts klsContract = new clsContracts();

                        switch (iClientType)
                        {
                            case 1:
                                if (cmbContractType.SelectedIndex == 0)
                                {
                                    fgKEMOwners.Rows.Count = 1;
                                    klsContract.PackageType = 1;
                                    klsContract.Client_ID = iClient_ID;
                                    klsContract.ContractType = 0;                                  // 0 - ATOMIKH ΣΥΜΒΑΣΗ 
                                    klsContract.ClientsList = iClient_ID + "^^^1^1^0~";            // format: Client_ID ^ DOY ^ AFM ^ IsMaster  ^ IsOrder ^ 0
                                    klsContract.ClientTipos = 0;
                                    klsContract.ContractTitle = txtContractTitle.Text;
                                    klsContract.Code = txtCode.Text;
                                    klsContract.Portfolio = txtPortfolio.Text;
                                    klsContract.Portfolio_Alias = txtPortfolio_Alias.Text;
                                    klsContract.Portfolio_Type = txtPortfolio_Type.Text;
                                    klsContract.DateStart = dDateStart.Value;
                                    klsContract.DateFinish = dDateFinish.Value;
                                    klsContract.Currency = cmbCurrencies.Text;
                                    klsContract.NumberAccount = txtNumberAccount.Text;
                                    klsContract.Contract_Details_ID = 0;
                                    klsContract.Contract_Packages_ID = 0;
                                    klsContract.MiFID_2 = chkMIIFID_2.Checked ? 1 : 0;
                                    klsContract.XAA = (chkXAA.Checked ? 1 : 0);
                                    klsContract.Status = (chkStatus.Checked ? 1 : 0);

                                    klsContract.Details.MIFIDCategory_ID = cmbMiFiDCategory.SelectedIndex;
                                    klsContract.Details.AgreementNotes = txtContractNotes.Text;
                                    klsContract.Details.PerformanceFees = 0;
                                    klsContract.Details.User1_ID = Convert.ToInt32(cmbUser1.SelectedValue);
                                    klsContract.Details.User2_ID = Convert.ToInt32(cmbUser2.SelectedValue);
                                    klsContract.Details.User3_ID = Convert.ToInt32(cmbUser3.SelectedValue);
                                    klsContract.Details.User4_ID = Convert.ToInt32(cmbUser4.SelectedValue);
                                    klsContract.Details.Surname = txtFPSurname.Text + "";
                                    klsContract.Details.Firstname = txtFPFirstname.Text + "";
                                    klsContract.Details.SurnameFather = txtFPFatherSurname.Text + "";
                                    klsContract.Details.FirstnameFather = txtFPFatherFirstname.Text + "";
                                    klsContract.Details.SurnameMother = txtFPMotherSurname.Text + "";
                                    klsContract.Details.FirstnameMother = txtFPMotherFirstname.Text + "";
                                    klsContract.Details.SurnameSizigo = txtFPSyzygosSurname.Text + "";
                                    klsContract.Details.FirstnameSizigo = txtFPSyzygosFirstname.Text + "";
                                    klsContract.Details.Division = Convert.ToInt32(cmbDivision.SelectedValue);
                                    klsContract.Details.Brunch_ID = Convert.ToInt32(cmbFPBrunches.SelectedValue);
                                    klsContract.Details.Spec_ID = Convert.ToInt32(cmbFPSpecials.SelectedValue);
                                    klsContract.Details.DoB = dFPDoB.Value;
                                    klsContract.Details.BornPlace = txtFPBornPlace.Text;
                                    klsContract.Details.Citizen_ID = Convert.ToInt32(cmbFPCitizen.SelectedValue);
                                    klsContract.Details.Sex = cmbFPSex.Text;
                                    klsContract.Details.ADT = txtFPADT.Text + ""; ;
                                    klsContract.Details.ExpireDate = txtFPExpireDate.Text + "";
                                    klsContract.Details.Police = txtFPPolice.Text + "";
                                    klsContract.Details.Passport = txtFPPassport.Text + ""; ;
                                    klsContract.Details.Passport_ExpireDate = txtFPPassport_ExpireDate.Text + "";
                                    klsContract.Details.Passport_Police = txtFPPassport_Police.Text + "";
                                    klsContract.Details.DOY = txtFPDOY.Text + "";
                                    klsContract.Details.AFM = txtFPAFM.Text + "";
                                    klsContract.Details.DOY2 = txtFPDOY2.Text + "";
                                    klsContract.Details.AFM2 = txtFPAFM2.Text + "";
                                    klsContract.Details.AMKA = txtFPAMKA.Text + "";
                                    klsContract.Details.CountryTaxes_ID = Convert.ToInt32(cmbFPCountryTaxes.SelectedValue);
                                    klsContract.Details.Address = txtFPAddress.Text + "";
                                    klsContract.Details.City = txtFPCity.Text + "";
                                    klsContract.Details.Zip = txtFPZip.Text + "";
                                    klsContract.Details.Country_ID = Convert.ToInt32(cmbFPXora.SelectedValue);
                                    klsContract.Details.Tel = txtFPTel.Text + "";
                                    klsContract.Details.Fax = txtFPFax.Text + "";
                                    klsContract.Details.Mobile = txtFPMobile.Text + "";
                                    klsContract.Details.SendSMS = (chkFPSMS.Checked ? 1 : 0);
                                    klsContract.Details.EMail = txtFPEMail.Text + "";
                                    klsContract.Details.ConnectionMethod = cmbFPConnectionMethod.SelectedIndex;
                                    klsContract.Details.Risk = cmbRisk.SelectedIndex;
                                    klsContract.Details.Merida = "";
                                    klsContract.Details.LogAxion = "";

                                    klsContract.Details.InvName = txtInvName.Text;
                                    klsContract.Details.InvAddress = txtInvAddress.Text + "";
                                    klsContract.Details.InvCity = txtInvCity.Text + "";
                                    klsContract.Details.InvZip = txtInvZip.Text + "";
                                    klsContract.Details.InvCountry_ID = (int)cmbInvCountry.SelectedValue;
                                    klsContract.Details.InvDOY = txtInvDOY.Text + "";
                                    klsContract.Details.InvAFM = txtInvAFM.Text + "";
                                }
                                else
                                {
                                    klsContract.PackageType = 1;
                                    klsContract.Client_ID = iClient_ID;
                                    sTemp = "";
                                    for (i = 1; i <= fgKEMOwners.Rows.Count - 1; i++)
                                    {
                                        sTemp = sTemp + fgKEMOwners[i, "Client_ID"] + "^" + fgKEMOwners[i, "DOY"] + "^" + fgKEMOwners[i, "AFM"] + "^" +
                                        (Convert.ToBoolean(fgKEMOwners[i, "Master"]) ? 1 : 0) + "^" + (Convert.ToBoolean(fgKEMOwners[i, "Order"]) ? "1" : "0") + "^" + fgKEMOwners[i, "ID"] + "~";         // format: Client_ID ^ DOY ^ AFM ^ IsMaster  ^ IsOrder ^ ID
                                    }
                                    klsContract.ClientsList = sTemp;
                                    klsContract.ContractType = 1;                                       // 1 - ΚΟΙΝΗ ΣΥΜΒΑΣΗ
                                    klsContract.ContractTitle = txtContractTitle.Text; ;
                                    klsContract.Code = txtCode.Text;
                                    klsContract.Portfolio = txtPortfolio.Text;
                                    klsContract.Portfolio_Alias = txtPortfolio_Alias.Text;
                                    klsContract.Portfolio_Type = txtPortfolio_Type.Text;
                                    klsContract.DateStart = dDateStart.Value;
                                    klsContract.DateFinish = dDateFinish.Value;
                                    klsContract.Currency = cmbCurrencies.Text;
                                    klsContract.NumberAccount = txtNumberAccount.Text;
                                    klsContract.Contract_Details_ID = 0;
                                    klsContract.Contract_Packages_ID = 0;
                                    klsContract.MiFID_2 = chkMIIFID_2.Checked ? 1 : 0;
                                    klsContract.XAA = (chkXAA.Checked ? 1 : 0);
                                    klsContract.Status = (chkStatus.Checked ? 1 : 0);

                                    klsContract.Details.MIFIDCategory_ID = cmbMiFiDCategory.SelectedIndex;
                                    klsContract.Details.AgreementNotes = txtContractNotes.Text;
                                    klsContract.Details.PerformanceFees = 0;
                                    klsContract.Details.User1_ID = Convert.ToInt32(cmbUser1.SelectedValue);
                                    klsContract.Details.User2_ID = Convert.ToInt32(cmbUser2.SelectedValue);
                                    klsContract.Details.User3_ID = Convert.ToInt32(cmbUser3.SelectedValue);
                                    klsContract.Details.User4_ID = Convert.ToInt32(cmbUser4.SelectedValue);
                                    klsContract.Details.Surname = txtKEMSurname.Text + "";
                                    klsContract.Details.Firstname = "";
                                    klsContract.Details.BornPlace = txtKEMRecipient.Text + "";
                                    klsContract.Details.Division = Convert.ToInt32(cmbDivision.SelectedValue);
                                    klsContract.Details.Address = txtKEMAddress.Text + "";
                                    klsContract.Details.City = txtKEMCity.Text + "";
                                    klsContract.Details.Zip = txtKEMZip.Text + "";
                                    klsContract.Details.Country_ID = Convert.ToInt32(cmbKEMXora.SelectedValue);
                                    klsContract.Details.Tel = txtKEMTel.Text + "";
                                    klsContract.Details.Fax = txtKEMFax.Text + "";
                                    klsContract.Details.Mobile = txtKEMMobile.Text + "";
                                    klsContract.Details.SendSMS = (chkKEMSMS.Checked ? 1 : 0);
                                    klsContract.Details.EMail = txtKEMEMail.Text + "";
                                    klsContract.Details.ConnectionMethod = cmbKEMConnectionMethod.SelectedIndex;
                                    klsContract.Details.Risk = cmbRisk.SelectedIndex;
                                    klsContract.Details.Merida = txtKEMMerida.Text + "";
                                    klsContract.Details.LogAxion = txtKEMLogAxion.Text + "";
                                }
                                break;
                            case 2:
                                fgKEMOwners.Rows.Count = 1;

                                klsContract.PackageType = 1;
                                klsContract.Client_ID = iClient_ID;
                                klsContract.ClientsList = iClient_ID + "^^^1^1^0~";            // format: Client_ID ^ DOY ^ AFM ^ IsMaster  ^ IsOrder ^ 0
                                klsContract.ContractType = 2;                                  // 2 - ΕΤΑΙΡΙΚΗ ΣΥΜΒΑΣΗ
                                klsContract.ContractTitle = txtContractTitle.Text;
                                klsContract.Code = txtCode.Text;
                                klsContract.Portfolio = txtPortfolio.Text;
                                klsContract.Portfolio_Alias = txtPortfolio_Alias.Text;
                                klsContract.Portfolio_Type = txtPortfolio_Type.Text;
                                klsContract.DateStart = dDateStart.Value;
                                klsContract.DateFinish = dDateFinish.Value;
                                klsContract.Currency = cmbCurrencies.Text;
                                klsContract.NumberAccount = txtNumberAccount.Text;
                                klsContract.Contract_Details_ID = 0;
                                klsContract.Contract_Packages_ID = 0;
                                klsContract.MiFID_2 = chkMIIFID_2.Checked ? 1 : 0;
                                klsContract.XAA = (chkXAA.Checked ? 1 : 0);
                                klsContract.Status = (chkStatus.Checked ? 1 : 0);

                                klsContract.Details.MIFIDCategory_ID = cmbMiFiDCategory.SelectedIndex;
                                klsContract.Details.AgreementNotes = txtContractNotes.Text;
                                klsContract.Details.PerformanceFees = 0;
                                klsContract.Details.User1_ID = Convert.ToInt32(cmbUser1.SelectedValue);
                                klsContract.Details.User2_ID = Convert.ToInt32(cmbUser2.SelectedValue);
                                klsContract.Details.User3_ID = Convert.ToInt32(cmbUser3.SelectedValue);
                                klsContract.Details.User4_ID = Convert.ToInt32(cmbUser4.SelectedValue);
                                klsContract.Details.Surname = txtNPTitle.Text + "";
                                klsContract.Details.Firstname = txtNPTitle.Text + "";
                                klsContract.Details.SurnameFather = txtNPEdra.Text + "";
                                klsContract.Details.FirstnameFather = txtNPMorfi.Text + "";
                                klsContract.Details.SurnameMother = "";
                                klsContract.Details.FirstnameMother = "";
                                klsContract.Details.SurnameSizigo = "";
                                klsContract.Details.FirstnameSizigo = "";
                                klsContract.Details.Division = Convert.ToInt32(cmbDivision.SelectedValue);
                                klsContract.Details.Spec_ID = 0;
                                klsContract.Details.Brunch_ID = Convert.ToInt32(cmbNPBrunches.SelectedValue);
                                klsContract.Details.Citizen_ID = Convert.ToInt32(cmbNPNation.SelectedValue);
                                klsContract.Details.ADT = txtNPAM.Text + "";
                                klsContract.Details.ExpireDate = txtNPIssueDate.Text + "";
                                klsContract.Details.Police = txtNPArmodiaArxi.Text + "";
                                klsContract.Details.DOY = txtNPDOY.Text + "";
                                klsContract.Details.AFM = txtNPAFM.Text + "";
                                klsContract.Details.CountryTaxes_ID = Convert.ToInt32(cmbNPCountryTaxes.SelectedValue);
                                klsContract.Details.BornPlace = txtNPReciever.Text + "";
                                klsContract.Details.Address = txtNPAddress.Text + "";
                                klsContract.Details.City = txtNPCity.Text + "";
                                klsContract.Details.Zip = txtNPZip.Text + "";
                                klsContract.Details.Country_ID = Convert.ToInt32(cmbNPXora.SelectedValue);
                                klsContract.Details.Tel = txtNPTel.Text + "";
                                klsContract.Details.Fax = txtNPFax.Text + "";
                                klsContract.Details.Mobile = txtNPMobile.Text + "";
                                klsContract.Details.SendSMS = (chkNPSMS.Checked ? 1 : 0);
                                klsContract.Details.EMail = txtNPEMail.Text + "";
                                klsContract.Details.ConnectionMethod = cmbNPConnectionMethod.SelectedIndex;
                                klsContract.Details.Risk = Convert.ToInt32(cmbRisk.SelectedIndex);
                                klsContract.Details.Merida = "";
                                klsContract.Details.LogAxion = "";
                                break;
                        }
                        klsContract.Details.InvName = txtInvName.Text + "";
                        klsContract.Details.InvAddress = txtInvAddress.Text + "";
                        klsContract.Details.InvCity = txtInvCity.Text + "";
                        klsContract.Details.InvZip = txtInvZip.Text + "";
                        klsContract.Details.InvCountry_ID = Convert.ToInt32(cmbInvCountry.SelectedValue);
                        klsContract.Details.InvDOY = txtInvDOY.Text + "";
                        klsContract.Details.InvAFM = txtInvAFM.Text + "";
                        klsContract.Details.VAT_Percent = Convert.ToSingle(txtFPA.Text);
                        klsContract.Details.ChkWorld = (chkWorld.Checked ? 1 : 0);
                        klsContract.Details.ChkGreece = (chkGreece.Checked ? 1 : 0);
                        klsContract.Details.ChkEurope = (chkEurope.Checked ? 1 : 0);
                        klsContract.Details.ChkAmerica = (chkAmerica.Checked ? 1 : 0);
                        klsContract.Details.ChkAsia = (chkAsia.Checked ? 1 : 0);
                        klsContract.Details.IncomeProducts = txtIncomeProducts.Text;
                        klsContract.Details.CapitalProducts = txtCapitalProducts.Text;
                        klsContract.Details.ChkSpecificConstraints = (rbNoSpecificConstraints.Checked ? 0 : 1);
                        klsContract.Details.ChkMonetaryRisk = (chkMonetaryRisk.Checked ? 1 : 0);
                        klsContract.Details.ChkIndividualBonds = (chkIndividualBonds.Checked ? 1 : 0);
                        klsContract.Details.ChkMutualFunds = (chkMutualFunds.Checked ? 1 : 0);
                        klsContract.Details.ChkBondedETFs = (chkBondedETFs.Checked ? 1 : 0);
                        klsContract.Details.ChkIndividualShares = (chkIndividualShares.Checked ? 1 : 0);
                        klsContract.Details.ChkMixedFunds = (chkMixedFunds.Checked ? 1 : 0);
                        klsContract.Details.ChkMixedETFs = (chkMixedETFs.Checked ? 1 : 0);
                        klsContract.Details.ChkFunds = (chkFunds.Checked ? 1 : 0);
                        klsContract.Details.ChkETFs = (chkETFs.Checked ? 1 : 0);
                        klsContract.Details.ChkInvestmentGrade = (chkInvestmentGrade.Checked ? 1 : 0);
                        klsContract.Details.MiscInstructions = txtMiscInstructions.Text;

                        klsContract.MiFID_2 = (ucCC.chkMIIFID_2.Checked ? 1 : 0);
                        klsContract.MiFID_2_StartDate = dDateStart.Value;
                        klsContract.Questionary_ID = iQuestionary_ID;
                        klsContract.Packages.Service_ID = Convert.ToInt32(ucCC.cmbFinanceServices.SelectedValue);
                        klsContract.Packages.CFP_ID = Convert.ToInt32(ucCC.cmbCompanyPackages.SelectedValue);
                        klsContract.Packages.DateStart = ucCC.dPackageDateStart.Value;
                        klsContract.Packages.DateFinish = ucCC.dPackageDateFinish.Value;
                        klsContract.Packages.Profile_ID = Convert.ToInt32(ucCC.cmbProfile.SelectedValue);
                        
                        iContract_ID = klsContract.InsertRecord();
                        iContract_Details_ID = klsContract.Details.Record_ID;
                        iContract_Packages_ID = klsContract.Packages.Record_ID;

                        clsContracts_ComplexSigns klsContracts_ComplexSigns = new clsContracts_ComplexSigns();
                        for (i = 1; i <= fgXM.Rows.Count - 1; i++)
                        {
                            if ((bool)fgXM[i, 0])
                            {
                                klsContracts_ComplexSigns.Contract_ID = iContract_ID;
                                klsContracts_ComplexSigns.ComplexSign_ID = Convert.ToInt32(fgXM[i, 2]);
                                klsContracts_ComplexSigns.InsertRecord();
                            }
                        }

                        if (cmbContractType.SelectedIndex == 1) Global.DMS_CreateDirectory("Customers/" + txtContractTitle.Text.Replace(".", "_"));
                        Global.DMS_CreateDirectory("Customers/" + txtContractTitle.Text.Replace(".", "_") + "/" + txtCode.Text);

                        /*
                        clsClientsDocFiles ClientsDocFiles = new clsClientsDocFiles();
                        ClientsDocFiles.PreContract_ID = 0;
                        ClientsDocFiles.Contract_ID = iContract_ID;
                        ClientsDocFiles.Client_ID = iClient_ID;
                        ClientsDocFiles.ClientName = txtContractTitle.Text.Replace(".", "_");
                        ClientsDocFiles.ContractCode = txtCode.Text;
                        ClientsDocFiles.DocTypes = Convert.ToInt32(cmbDocTypes.SelectedValue);
                        ClientsDocFiles.DMS_Files_ID = 0;
                        ClientsDocFiles.OldFileName = "";
                        ClientsDocFiles.NewFileName = txtFileName.Text;
                        ClientsDocFiles.FullFileName = sFullFileName;
                        ClientsDocFiles.DateIns = DateTime.Now;
                        ClientsDocFiles.User_ID = Global.User_ID;
                        iDocFiles_ID = ClientsDocFiles.InsertRecord();    
                        */

                        bEditPackages = false;                       // false, because Package's data was inserted above
                        bEditFees = true;
                        break;

                    case 1:                                          // 1 - EDIT Contract Details
                        if (!Global.DMS_CheckDirectoryExists("Customers/" + sOldContractTitle.Replace(".", "_") + "/" + sOldCode))
                            Global.DMS_CreateDirectory("Customers/" + txtContractTitle.Text.Replace(".", "_") + "/" + txtCode.Text);
                        
                        if (sOldContractTitle.Trim() != txtContractTitle.Text.Trim())
                        {
                            if (!Global.DMS_CheckDirectoryExists("Customers/" + txtContractTitle.Text))
                                Global.DMS_RenameFolderName(sOldContractTitle, txtContractTitle.Text);

                            if (sOldCode.Trim() != txtCode.Text.Trim())
                                if (!Global.DMS_CheckDirectoryExists("Customers/" + txtContractTitle.Text.Replace(".", "_") + "/" + txtCode.Text))
                                    Global.DMS_RenameFolderName(txtContractTitle.Text.Replace(".", "_") + "/" + sOldCode, txtCode.Text);
                        }

                        jAktion = 1;                     // 0 - ADD, 1 - EDIT, 2 - DELETE

                        klsContract = new clsContracts();
                        klsContract.Record_ID = iContract_ID;
                        klsContract.Contract_Details_ID = iContract_Details_ID;
                        klsContract.Contract_Packages_ID = iContract_Packages_ID;
                        klsContract.GetRecord();

                        switch (cmbContractType.SelectedIndex)
                        {
                            case 0:
                                fgKEMOwners.Rows.Count = 1;
                                klsContract.PackageType = 1;
                                klsContract.Client_ID = iClient_ID;
                                klsContract.ClientsList = iClient_ID + "^^^1^1^0~";                       // format: Client_ID ^ DOY ^ AFM ^ IsMaster  ^ IsOrder ^ 0
                                klsContract.ContractType = cmbContractType.SelectedIndex;
                                klsContract.ContractTitle = txtContractTitle.Text;
                                klsContract.Code = txtCode.Text;
                                klsContract.Portfolio = txtPortfolio.Text;
                                klsContract.Portfolio_Alias = txtPortfolio_Alias.Text;
                                klsContract.Portfolio_Type = txtPortfolio_Type.Text;
                                klsContract.DateStart = dDateStart.Value;
                                klsContract.DateFinish = dDateFinish.Value;
                                klsContract.Currency = cmbCurrencies.Text;
                                klsContract.NumberAccount = txtNumberAccount.Text;
                                klsContract.MiFID_2 = chkMIIFID_2.Checked ? 1 : 0;
                                klsContract.XAA = (chkXAA.Checked ? 1 : 0);
                                klsContract.Status = (chkStatus.Checked ? 1 : 0);

                                klsContract.Details.MIFIDCategory_ID = cmbMiFiDCategory.SelectedIndex;
                                klsContract.Details.AgreementNotes = txtContractNotes.Text;
                                klsContract.Details.PerformanceFees = 0;
                                klsContract.Details.User1_ID = Convert.ToInt32(cmbUser1.SelectedValue);
                                klsContract.Details.User2_ID = Convert.ToInt32(cmbUser2.SelectedValue);
                                klsContract.Details.User3_ID = Convert.ToInt32(cmbUser3.SelectedValue);
                                klsContract.Details.User4_ID = Convert.ToInt32(cmbUser4.SelectedValue);
                                klsContract.Details.Surname = txtFPSurname.Text + "";
                                klsContract.Details.Firstname = txtFPFirstname.Text + "";
                                klsContract.Details.SurnameFather = txtFPFatherSurname.Text + "";
                                klsContract.Details.FirstnameFather = txtFPFatherFirstname.Text + "";
                                klsContract.Details.SurnameMother = txtFPMotherSurname.Text + "";
                                klsContract.Details.FirstnameMother = txtFPMotherFirstname.Text + "";
                                klsContract.Details.SurnameSizigo = txtFPSyzygosSurname.Text + "";
                                klsContract.Details.FirstnameSizigo = txtFPSyzygosFirstname.Text + "";
                                klsContract.Details.Division = Convert.ToInt32(cmbDivision.SelectedValue);
                                klsContract.Details.Spec_ID = Convert.ToInt32(cmbFPSpecials.SelectedValue);
                                klsContract.Details.Brunch_ID = Convert.ToInt32(cmbFPBrunches.SelectedValue);
                                klsContract.Details.DoB = dFPDoB.Value;
                                klsContract.Details.BornPlace = txtFPBornPlace.Text;
                                klsContract.Details.Citizen_ID = Convert.ToInt32(cmbFPCitizen.SelectedValue);
                                klsContract.Details.Sex = cmbFPSex.Text;
                                klsContract.Details.ADT = txtFPADT.Text + "";
                                klsContract.Details.ExpireDate = txtFPExpireDate.Text + "";
                                klsContract.Details.Police = txtFPPolice.Text + "";
                                klsContract.Details.Passport = txtFPPassport.Text + ""; ;
                                klsContract.Details.Passport_ExpireDate = txtFPPassport_ExpireDate.Text + "";
                                klsContract.Details.Passport_Police = txtFPPassport_Police.Text + "";
                                klsContract.Details.DOY = txtFPDOY.Text + "";
                                klsContract.Details.AFM = txtFPAFM.Text + "";
                                klsContract.Details.DOY2 = txtFPDOY2.Text + "";
                                klsContract.Details.AFM2 = txtFPAFM2.Text + "";
                                klsContract.Details.AMKA = txtFPAMKA.Text + "";
                                klsContract.Details.CountryTaxes_ID = Convert.ToInt32(cmbFPCountryTaxes.SelectedValue);
                                klsContract.Details.Address = txtFPAddress.Text + "";
                                klsContract.Details.City = txtFPCity.Text + "";
                                klsContract.Details.Zip = txtFPZip.Text + "";
                                klsContract.Details.Country_ID = Convert.ToInt32(cmbFPXora.SelectedValue);
                                klsContract.Details.Tel = txtFPTel.Text + "";
                                klsContract.Details.Fax = txtFPFax.Text + "";
                                klsContract.Details.Mobile = txtFPMobile.Text + "";
                                klsContract.Details.SendSMS = (chkFPSMS.Checked ? 1 : 0);
                                klsContract.Details.EMail = txtFPEMail.Text + "";
                                klsContract.Details.ConnectionMethod = cmbFPConnectionMethod.SelectedIndex;
                                klsContract.Details.Risk = cmbRisk.SelectedIndex;
                                klsContract.Details.Merida = "";
                                klsContract.Details.LogAxion = "";
                                break;
                            case 1:
                                klsContract.PackageType = 1;
                                klsContract.Client_ID = iClient_ID;
                                sTemp = "";
                                for (i = 1; i <= fgKEMOwners.Rows.Count - 1; i++)
                                {
                                    sTemp = sTemp + fgKEMOwners[i, "Client_ID"] + "^" + fgKEMOwners[i, "DOY"] + "^" + fgKEMOwners[i, "AFM"] + "^" +
                                    ((bool)fgKEMOwners[i, "Master"] ? 1 : 0) + "^" + ((bool)fgKEMOwners[i, "Order"] ? 1 : 0) + "^" + fgKEMOwners[i, "ID"] + "~";   //format: Client_ID ^ DOY ^ AFM ^ IsMaster  ^ IsOrder ^ ID
                                }
                                klsContract.ClientsList = sTemp;
                                klsContract.ContractType = cmbContractType.SelectedIndex;
                                klsContract.ContractTitle = txtContractTitle.Text;
                                klsContract.Code = txtCode.Text;
                                klsContract.Portfolio = txtPortfolio.Text;
                                klsContract.Portfolio_Alias = txtPortfolio_Alias.Text;
                                klsContract.Portfolio_Type = txtPortfolio_Type.Text;
                                klsContract.DateStart = dDateStart.Value;
                                klsContract.DateFinish = dDateFinish.Value;
                                klsContract.Currency = cmbCurrencies.Text;
                                klsContract.NumberAccount = txtNumberAccount.Text;
                                klsContract.MiFID_2 = chkMIIFID_2.Checked ? 1 : 0;
                                klsContract.XAA = (chkXAA.Checked ? 1 : 0);
                                klsContract.Status = (chkStatus.Checked ? 1 : 0);

                                klsContract.Details.MIFIDCategory_ID = cmbMiFiDCategory.SelectedIndex;
                                klsContract.Details.AgreementNotes = txtContractNotes.Text;
                                klsContract.Details.PerformanceFees = 0;
                                klsContract.Details.User1_ID = Convert.ToInt32(cmbUser1.SelectedValue);
                                klsContract.Details.User2_ID = Convert.ToInt32(cmbUser2.SelectedValue);
                                klsContract.Details.User3_ID = Convert.ToInt32(cmbUser3.SelectedValue);
                                klsContract.Details.User4_ID = Convert.ToInt32(cmbUser4.SelectedValue);
                                klsContract.Details.Surname = txtKEMSurname.Text + "";
                                klsContract.Details.Firstname = "";
                                klsContract.Details.BornPlace = txtKEMRecipient.Text + "";
                                klsContract.Details.Division = Convert.ToInt32(cmbDivision.SelectedValue);
                                klsContract.Details.Address = txtKEMAddress.Text + "";
                                klsContract.Details.City = txtKEMCity.Text + "";
                                klsContract.Details.Zip = txtKEMZip.Text + "";
                                klsContract.Details.Country_ID = Convert.ToInt32(cmbKEMXora.SelectedValue);
                                klsContract.Details.Tel = txtKEMTel.Text + "";
                                klsContract.Details.Fax = txtKEMFax.Text + "";
                                klsContract.Details.Mobile = txtKEMMobile.Text + "";
                                klsContract.Details.SendSMS = (chkKEMSMS.Checked ? 1 : 0);
                                klsContract.Details.EMail = txtKEMEMail.Text + "";
                                klsContract.Details.ConnectionMethod = cmbKEMConnectionMethod.SelectedIndex;
                                klsContract.Details.Risk = Convert.ToInt32(cmbRisk.SelectedIndex);
                                klsContract.Details.Merida = txtKEMMerida.Text + "";
                                klsContract.Details.LogAxion = txtKEMLogAxion.Text + "";
                                break;
                            case 2:
                                fgKEMOwners.Rows.Count = 1;
                                klsContract.PackageType = 1;
                                klsContract.Client_ID = iClient_ID;
                                klsContract.ClientsList = iClient_ID + "^^^1^1^0~";              // format: Client_ID ^ DOY ^ AFM ^ IsMaster  ^ IsOrder ^ 0
                                klsContract.ContractType = cmbContractType.SelectedIndex;
                                klsContract.ContractTitle = txtContractTitle.Text;
                                klsContract.Code = txtCode.Text;
                                klsContract.Portfolio = txtPortfolio.Text;
                                klsContract.Portfolio_Alias = txtPortfolio_Alias.Text;
                                klsContract.Portfolio_Type = txtPortfolio_Type.Text;
                                klsContract.DateStart = dDateStart.Value;
                                klsContract.DateFinish = dDateFinish.Value;
                                klsContract.Currency = cmbCurrencies.Text;
                                klsContract.NumberAccount = txtNumberAccount.Text;
                                klsContract.Status = (chkStatus.Checked ? 1 : 0);

                                klsContract.Details.MIFIDCategory_ID = Convert.ToInt32(cmbMiFiDCategory.SelectedIndex);
                                klsContract.Details.AgreementNotes = txtContractNotes.Text;
                                klsContract.Details.PerformanceFees = 0;
                                klsContract.Details.User1_ID = Convert.ToInt32(cmbUser1.SelectedValue);
                                klsContract.Details.User2_ID = Convert.ToInt32(cmbUser2.SelectedValue);
                                klsContract.Details.User3_ID = Convert.ToInt32(cmbUser3.SelectedValue);
                                klsContract.Details.User4_ID = Convert.ToInt32(cmbUser4.SelectedValue);
                                klsContract.Details.Surname = txtNPTitle.Text + "";
                                klsContract.Details.Firstname = txtNPTitle.Text + "";
                                klsContract.Details.SurnameFather = txtNPEdra.Text + "";
                                klsContract.Details.FirstnameFather = txtNPMorfi.Text + "";
                                klsContract.Details.SurnameMother = "";
                                klsContract.Details.FirstnameMother = "";
                                klsContract.Details.SurnameSizigo = "";
                                klsContract.Details.FirstnameSizigo = "";
                                klsContract.Details.Division = Convert.ToInt32(cmbDivision.SelectedValue);
                                klsContract.Details.Spec_ID = 0;
                                klsContract.Details.Brunch_ID = Convert.ToInt32(cmbNPBrunches.SelectedValue);
                                klsContract.Details.Citizen_ID = Convert.ToInt32(cmbNPNation.SelectedValue);
                                klsContract.Details.ADT = txtNPAM.Text + "";
                                klsContract.Details.ExpireDate = txtNPIssueDate.Text + "";
                                klsContract.Details.Police = txtNPArmodiaArxi.Text + "";
                                klsContract.Details.DOY = txtNPDOY.Text + "";
                                klsContract.Details.AFM = txtNPAFM.Text + "";
                                klsContract.Details.CountryTaxes_ID = Convert.ToInt32(cmbNPCountryTaxes.SelectedValue);
                                klsContract.Details.BornPlace = txtNPReciever.Text + "";
                                klsContract.Details.Address = txtNPAddress.Text + "";
                                klsContract.Details.City = txtNPCity.Text + "";
                                klsContract.Details.Zip = txtNPZip.Text + "";
                                klsContract.Details.Country_ID = Convert.ToInt32(cmbNPXora.SelectedValue);
                                klsContract.Details.Tel = txtNPTel.Text + "";
                                klsContract.Details.Fax = txtNPFax.Text + "";
                                klsContract.Details.Mobile = txtNPMobile.Text + "";
                                klsContract.Details.SendSMS = (chkNPSMS.Checked ? 1 : 0);
                                klsContract.Details.EMail = txtNPEMail.Text + "";
                                klsContract.Details.ConnectionMethod = cmbNPConnectionMethod.SelectedIndex;
                                klsContract.Details.Risk = Convert.ToInt32(cmbRisk.SelectedIndex);
                                klsContract.Details.Merida = "";
                                klsContract.Details.LogAxion = "";
                                break;
                        }

                        klsContract.Details.InvName = txtInvName.Text + "";
                        klsContract.Details.InvAddress = txtInvAddress.Text + "";
                        klsContract.Details.InvCity = txtInvCity.Text + "";
                        klsContract.Details.InvZip = txtInvZip.Text + "";
                        klsContract.Details.InvCountry_ID = Convert.ToInt32(cmbInvCountry.SelectedValue);
                        klsContract.Details.InvDOY = txtInvDOY.Text + "";
                        klsContract.Details.InvAFM = txtInvAFM.Text + "";
                        klsContract.Details.VAT_Percent = Convert.ToSingle(txtFPA.Text);
                        klsContract.Details.ChkComplex = (chkComplex.Checked ? 1 : 0);
                        klsContract.Details.ChkWorld = (chkWorld.Checked ? 1 : 0);
                        klsContract.Details.ChkGreece = (chkGreece.Checked ? 1 : 0);
                        klsContract.Details.ChkEurope = (chkEurope.Checked ? 1 : 0);
                        klsContract.Details.ChkAmerica = (chkAmerica.Checked ? 1 : 0);
                        klsContract.Details.ChkAsia = (chkAsia.Checked ? 1 : 0);
                        klsContract.Details.IncomeProducts = txtIncomeProducts.Text;
                        klsContract.Details.CapitalProducts = txtCapitalProducts.Text;
                        klsContract.Details.ChkSpecificConstraints = (rbSpecificConstraints.Checked ? 1 : 0);
                        klsContract.Details.ChkMonetaryRisk = (chkMonetaryRisk.Checked ? 1 : 0);
                        klsContract.Details.ChkIndividualBonds = (chkIndividualBonds.Checked ? 1 : 0);
                        klsContract.Details.ChkMutualFunds = (chkMutualFunds.Checked ? 1 : 0);
                        klsContract.Details.ChkBondedETFs = (chkBondedETFs.Checked ? 1 : 0);
                        klsContract.Details.ChkIndividualShares = (chkIndividualShares.Checked ? 1 : 0);
                        klsContract.Details.ChkMixedFunds = (chkMixedFunds.Checked ? 1 : 0);
                        klsContract.Details.ChkMixedETFs = (chkMixedETFs.Checked ? 1 : 0);
                        klsContract.Details.ChkFunds = (chkFunds.Checked ? 1 : 0);
                        klsContract.Details.ChkETFs = (chkETFs.Checked ? 1 : 0);
                        klsContract.Details.ChkInvestmentGrade = (chkInvestmentGrade.Checked ? 1 : 0);
                        klsContract.Details.MiscInstructions = txtMiscInstructions.Text;

                        if (klsContract.MiFID_2 == 0)
                        {
                            if (ucCC.chkMIIFID_2.Checked) {
                                klsContract.MiFID_2 = 1;
                                klsContract.MiFID_2_StartDate = ucCC.dNewPackageDateStart.Value;

                                chkMIIFID_2.Checked = true;
                                dMIFID_2_StartDate.Value = ucCC.dNewPackageDateStart.Value;
                                dMIFID_2_StartDate.Visible = true;
                            }
                        }

                        klsContract.Questionary_ID = iQuestionary_ID;
                        klsContract.EditRecord_Details();

                        klsContracts_ComplexSigns = new clsContracts_ComplexSigns();
                        klsContracts_ComplexSigns.Contract_ID = iContract_ID;
                        klsContracts_ComplexSigns.DeleteRecord();

                        for (i = 1; i <= fgXM.Rows.Count - 1; i++)
                        {
                            if (Convert.ToBoolean(fgXM[i, 0]))
                            {
                                klsContracts_ComplexSigns = new clsContracts_ComplexSigns();
                                klsContracts_ComplexSigns.Contract_ID = iContract_ID;
                                klsContracts_ComplexSigns.ComplexSign_ID = Convert.ToInt32(fgXM[i, 2]);
                                klsContracts_ComplexSigns.InsertRecord();
                            }
                        }

                        bEditPackages = false;
                        bEditFees = true;
                        break;

                    case 2:                                // 2 - EDIT Contracts Packages Data
                        bEditPackages = true;
                        bEditFees = false;
                        break;

                    case 3:                          // 3 - EDIT Package Prices
                        bEditPackages = true;
                        bEditFees = false;
                        break;

                    case 4:                           // 4 - CHANGE Package
                        bEditPackages = true;
                        bEditFees = true;
                        break;

                    case 5:                          // 5 - CHANGE Package Version
                        bEditPackages = true;
                        bEditFees = false;
                        break;

                    case 6:                          // 6 - EDIT Investment Policy
                        bEditPackages = false;
                        bEditFees = false;
                        break;
                }

                if (bEditPackages)
                {
                    //--- Edit DateFinish of current version Contracts_Packages ----------------------
                    clsContracts klsContract = new clsContracts();
                    klsContract.Record_ID = iContract_ID;
                    klsContract.Contract_Details_ID = iContract_Details_ID;
                    klsContract.Contract_Packages_ID = iContract_Packages_ID;
                    klsContract.GetRecord();

                    if (klsContract.MiFID_2 == 0) {
                        if (ucCC.chkMIIFID_2.Checked) {
                            klsContract.MiFID_2 = 1;
                            klsContract.MiFID_2_StartDate = ucCC.dNewPackageDateStart.Value;

                            chkMIIFID_2.Checked = true;
                            dMIFID_2_StartDate.Value = ucCC.dNewPackageDateStart.Value;
                            dMIFID_2_StartDate.Visible = true;
                        }                        
                    }

                    klsContract.Packages.DateFinish = ucCC.dCurPackageDateFinish.Value;
                    klsContract.Packages.EditRecord();

                    //--- Edit DateFinish of OLD version of this Contracts_Details_Packages --------------
                    clsContracts_Details_Packages klsContracts_Details_Packages = new clsContracts_Details_Packages();
                    klsContracts_Details_Packages.Contract_ID = iOldContract_ID;
                    klsContracts_Details_Packages.Contracts_Details_ID = iOldContract_Details_ID;
                    klsContracts_Details_Packages.Contracts_Packages_ID = iOldContract_Packages_ID;
                    klsContracts_Details_Packages.GetRecord_Contract_ID();
                    klsContracts_Details_Packages.DateTo = ucCC.dCurPackageDateFinish.Value;
                    klsContracts_Details_Packages.EditRecord();

                    //--- Insert new records into Details & Packages tables---------------------------
                    klsContract.Packages.Contract_ID = iContract_ID;
                    klsContract.Packages.Service_ID = Convert.ToInt32(ucCC.cmbFinanceServices.SelectedValue);
                    klsContract.Packages.CFP_ID = Convert.ToInt32(ucCC.cmbCompanyPackages.SelectedValue);
                    klsContract.Packages.DateStart = ucCC.dNewPackageDateStart.Value;
                    klsContract.Packages.DateFinish = ucCC.dNewPackageDateFinish.Value;
                    klsContract.Packages.Profile_ID = Convert.ToInt32(ucCC.cmbProfile.SelectedValue);
                    klsContract.EditRecord_Packages();
                    iContract_Details_ID = klsContract.Contract_Details_ID;
                    iContract_Packages_ID = klsContract.Contract_Packages_ID;                    
                }

                if (bEditFees) EditFees();

                //--- recreate Users_List -----------------------------------------------------------
                clsClients Clients = new clsClients();
                Clients.Record_ID = iClient_ID;
                Clients.EMail = "";
                Clients.Mobile = "";
                Clients.AFM = "";
                Clients.DoB = Convert.ToDateTime("1900/01/01");
                Clients.GetRecord();

                if (Convert.ToInt32(cmbUser1.SelectedValue) > 0) {
                    i = sUsers_List.IndexOf(cmbUser1.SelectedValue + "");
                    if (i < 0) sUsers_List = sUsers_List.Trim() + cmbUser1.SelectedValue + ",";
                }

                if (Convert.ToInt32(cmbUser2.SelectedValue) > 0) {
                    i = sUsers_List.IndexOf(cmbUser2.SelectedValue + "");
                    if (i < 0) sUsers_List = sUsers_List.Trim() + cmbUser2.SelectedValue + ",";
                }

                if (Convert.ToInt32(cmbUser3.SelectedValue) > 0) {
                    i = sUsers_List.IndexOf(cmbUser3.SelectedValue + "");
                    if (i < 0) sUsers_List = sUsers_List.Trim() + cmbUser3.SelectedValue + ",";
                }

                if (Convert.ToInt32(cmbUser4.SelectedValue) > 0) {
                    i = sUsers_List.IndexOf(cmbUser4.SelectedValue + "");
                    if (i < 0) sUsers_List = sUsers_List.Trim() + cmbUser4.SelectedValue + ",";
                }

                Clients.Users_List = sUsers_List;
                Clients.EditRecord();

                //--- save historical document ----------------------------------------------------
                iDocFiles_ID = 0;
                if (txtFileName.Text.Trim().Length > 0)
                {
                    clsClientsDocFiles ClientsDocFiles = new clsClientsDocFiles();
                    ClientsDocFiles.PreContract_ID = 0;
                    ClientsDocFiles.Contract_ID = iContract_ID;
                    ClientsDocFiles.Client_ID = iClient_ID;
                    ClientsDocFiles.ClientName = txtContractTitle.Text.Replace(".", "_");
                    ClientsDocFiles.ContractCode = txtCode.Text;
                    ClientsDocFiles.DocTypes = Convert.ToInt32(cmbDocTypes.SelectedValue);
                    ClientsDocFiles.DMS_Files_ID = 0;
                    ClientsDocFiles.OldFileName = "";
                    ClientsDocFiles.NewFileName = txtFileName.Text;
                    ClientsDocFiles.FullFileName = sFullFileName;
                    ClientsDocFiles.DateIns = DateTime.Now;
                    ClientsDocFiles.User_ID = Global.User_ID;
                    ClientsDocFiles.Status = 2;                                           // 2 - document confirmed
                    iDocFiles_ID = ClientsDocFiles.InsertRecord();    
                }
                //--- save history record ---------------------------------------------------------
                sTemp = iContract_ID + "~" + ucCC.lblContract_ID.Text + "~" + "1" + "~" + iClient_ID + "~" + iOldContract_Details_ID + "~" + iOldContract_Packages_ID;
                Global.SaveHistory(7, iContract_Packages_ID, iClient_ID, iContract_ID, jAktion, sTemp, iDocFiles_ID, txtNotes.Text, DateTime.Now, Global.User_ID);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

            iFinishAktion = 1;
            panNotes.Visible = false;
        }
        private void rbNoSpecificConstraints_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNoSpecificConstraints.Checked) rbSpecificConstraints.Checked = false;
            else rbSpecificConstraints.Checked = true;
            DefineSpecificConstraints();
        }
        private void rbSpecificConstraints_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSpecificConstraints.Checked) rbNoSpecificConstraints.Checked = false;
            else rbNoSpecificConstraints.Checked = true;
            DefineSpecificConstraints();
        }
        private void DefineSpecificConstraints()
        {
            if (rbNoSpecificConstraints.Checked) {
                panSpecificConstraints.Enabled = false;
                chkMonetaryRisk.Checked = false;
                chkIndividualBonds.Checked = false;
                chkMutualFunds.Checked = false;
                chkBondedETFs.Checked = false;
                chkIndividualShares.Checked = false;
                chkMixedFunds.Checked = false;
                chkMixedETFs.Checked = false;
                chkFunds.Checked = false;
                chkETFs.Checked = false;
                chkInvestmentGrade.Checked = false;
                txtMiscInstructions.Text = "";
            }
            else {
                panSpecificConstraints.Enabled = true;
                chkMonetaryRisk.Checked = (Contracts.Details.ChkMonetaryRisk == 1 ? true : false);
                chkIndividualBonds.Checked = (Contracts.Details.ChkIndividualBonds == 1 ? true : false);
                chkMutualFunds.Checked = (Contracts.Details.ChkMutualFunds == 1 ? true : false);
                chkBondedETFs.Checked = (Contracts.Details.ChkBondedETFs == 1 ? true : false);
                chkIndividualShares.Checked = (Contracts.Details.ChkIndividualShares == 1 ? true : false);
                chkMixedFunds.Checked = (Contracts.Details.ChkMixedFunds == 1 ? true : false);
                chkMixedETFs.Checked = (Contracts.Details.ChkMixedETFs == 1 ? true : false);
                chkFunds.Checked = (Contracts.Details.ChkFunds == 1 ? true : false);
                chkETFs.Checked = (Contracts.Details.ChkETFs == 1 ? true : false);
                chkInvestmentGrade.Checked = (Contracts.Details.ChkInvestmentGrade == 1 ? true : false);
                txtMiscInstructions.Text = Contracts.Details.MiscInstructions + "";
            }
        }
        private void tsbAddDocFile_Click(object sender, EventArgs e)
        {
            frmDocFilesEdit locDocFilesEdit = new frmDocFilesEdit();
            locDocFilesEdit.Aktion = 0;
            locDocFilesEdit.Mode = 1;                            // 1 - Clients, 2 - Products
            locDocFilesEdit.Client_ID = iClient_ID;
            locDocFilesEdit.Contract_ID = iContract_ID;
            locDocFilesEdit.DocTypes = 0;
            locDocFilesEdit.PD_Group_ID = 0;
            locDocFilesEdit.DMS_Files_ID = 0;
            locDocFilesEdit.txtFileName.Text = "";
            locDocFilesEdit.ClientFullName = txtContractTitle.Text.Replace(".", "_");
            locDocFilesEdit.Code = txtCode.Text;
            locDocFilesEdit.ShowDialog();
            if (locDocFilesEdit.Aktion == 1) ShowDocFiles();
        }
        private void tsbEditDocFile_Click(object sender, EventArgs e)
        {
            EditDocFile();
        }
        private void EditDocFile()
        {
            if (fgDocFiles.Row > 0)
            {
                frmDocFilesEdit locDocFilesEdit = new frmDocFilesEdit();
                locDocFilesEdit.Aktion = 1;
                locDocFilesEdit.Mode = 1;                                                      // 1 - Clients, 2 - Products
                locDocFilesEdit.Rec_ID = Convert.ToInt32(fgDocFiles[fgDocFiles.Row, 5]);
                locDocFilesEdit.Client_ID = iClient_ID;
                locDocFilesEdit.ClientFullName = txtContractTitle.Text.Replace(".", "_");
                locDocFilesEdit.Contract_ID = iContract_ID;
                locDocFilesEdit.DocTypes = Convert.ToInt32(fgDocFiles[fgDocFiles.Row, 6]);
                locDocFilesEdit.PD_Group_ID = Convert.ToInt32(fgDocFiles[fgDocFiles.Row, "PD_Group_ID"]);
                locDocFilesEdit.DMS_Files_ID = Convert.ToInt32(fgDocFiles[fgDocFiles.Row, 7]);
                locDocFilesEdit.txtFileName.Text = fgDocFiles[fgDocFiles.Row, 3] + "";
                locDocFilesEdit.chkOldFiles.Checked = Convert.ToBoolean(fgDocFiles[fgDocFiles.Row, 4]);
                locDocFilesEdit.Code = txtCode.Text;
                locDocFilesEdit.ShowDialog();
                if (locDocFilesEdit.Aktion == 1) ShowDocFiles();
            }
        }
        private void tsbDelDocFile_Click(object sender, EventArgs e)
        {
            if (fgDocFiles.Row > 0)
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    clsClientsDocFiles ClientsDocFiles = new clsClientsDocFiles();
                    ClientsDocFiles.Record_ID = Convert.ToInt32(fgDocFiles[fgDocFiles.Row, "ID"]);
                    ClientsDocFiles.DeleteRecord();
                    fgDocFiles.RemoveItem(fgDocFiles.Row);
                    fgDocFiles.Redraw = true;
                }
        }

        private void tsbViewDocFile_Click(object sender, EventArgs e)
        {
            if (fgDocFiles.Rows.Count > 1)
            {
                if (Convert.ToBoolean(fgDocFiles[fgDocFiles.Row, 4]))
                    Global.DMS_ShowFile("Customers/" + txtContractTitle.Text.Trim() + "/" + txtCode.Text + "/OldDocs", fgDocFiles[fgDocFiles.Row, 3].ToString());
                else
                {
                    if (fgDocFiles[fgDocFiles.Row, 1].ToString() != "")
                        Global.DMS_ShowFile("Customers/" + txtContractTitle.Text.Replace(".", "_") + "/" + fgDocFiles[fgDocFiles.Row, 1], fgDocFiles[fgDocFiles.Row, 3].ToString());
                    else
                        Global.DMS_ShowFile("Customers/" + txtContractTitle.Text.Replace(".", "_"), fgDocFiles[fgDocFiles.Row, 3].ToString());
                }
            }
            else MessageBox.Show("Προβολή αρχείου δεν είναι δυνατών", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void tsbAddOwner_Click(object sender, EventArgs e)
        {
            frmOwnersEdit locOwnersEdit = new frmOwnersEdit();
            locOwnersEdit.LastAktion = 0;
            locOwnersEdit.Client_ID = 0;
            locOwnersEdit.Code = txtCode.Text;
            locOwnersEdit.ucCS.txtClientName.Text = "";
            locOwnersEdit.txtFather.Text = "";
            locOwnersEdit.txtADT.Text = "";
            locOwnersEdit.txtPassport.Text = "";
            locOwnersEdit.DOY = "";
            locOwnersEdit.AFM = "";
            locOwnersEdit.lblBorn.Text = "";
            locOwnersEdit.lblSpecial.Text = "";
            locOwnersEdit.chkMaster.Checked = false;
            locOwnersEdit.chkOrder.Checked = true;
            locOwnersEdit.ShowDialog();
            if (locOwnersEdit.LastAktion == 1)
                fgKEMOwners.AddItem(locOwnersEdit.ucCS.txtClientName.Text + "\t" + locOwnersEdit.txtFather.Text + "\t" + locOwnersEdit.txtADT.Text + "\t" + 
                                    locOwnersEdit.txtPassport.Text + "\t" + locOwnersEdit.cmbDOY.Text + "\t" + locOwnersEdit.cmbAFM.Text + "\t" + 
                                    locOwnersEdit.chkMaster.Checked + "\t" + locOwnersEdit.chkOrder.Checked + "\t" + "0" + "\t" + locOwnersEdit.Client_ID + "\t" +
                                    locOwnersEdit.lblBorn.Text + "\t" + locOwnersEdit.lblSpecial.Text);
        }
        private void fgKEMOwners_DoubleClick(object sender, EventArgs e)
        {
            frmOwnersEdit locOwnersEdit = new frmOwnersEdit();
            locOwnersEdit.LastAktion = 1;
            locOwnersEdit.Code = txtCode.Text;
            locOwnersEdit.ucCS.ShowClientsList = false;
            locOwnersEdit.ucCS.txtClientName.Text = fgKEMOwners[fgKEMOwners.Row, "Fullname"].ToString();
            locOwnersEdit.ucCS.ShowClientsList = true;
            locOwnersEdit.txtFather.Text = fgKEMOwners[fgKEMOwners.Row, "FirstnameFather"].ToString();
            locOwnersEdit.txtADT.Text = fgKEMOwners[fgKEMOwners.Row, "ADT"].ToString();
            locOwnersEdit.txtPassport.Text = fgKEMOwners[fgKEMOwners.Row, "Passport"].ToString();
            locOwnersEdit.DOY = fgKEMOwners[fgKEMOwners.Row, "DOY"].ToString();
            locOwnersEdit.AFM = fgKEMOwners[fgKEMOwners.Row, "AFM"].ToString();
            locOwnersEdit.chkMaster.Checked = Convert.ToBoolean(fgKEMOwners[fgKEMOwners.Row, "Master"]);
            locOwnersEdit.chkOrder.Checked = Convert.ToBoolean(fgKEMOwners[fgKEMOwners.Row, "Order"]);
            locOwnersEdit.Rec_ID = Convert.ToInt32(fgKEMOwners[fgKEMOwners.Row, "ID"]);
            locOwnersEdit.Client_ID = Convert.ToInt32(fgKEMOwners[fgKEMOwners.Row, "Client_ID"]);
            locOwnersEdit.ShowDialog();
            if (locOwnersEdit.LastAktion == 1)
            {
                fgKEMOwners[fgKEMOwners.Row, "Fullname"] = locOwnersEdit.ucCS.txtClientName.Text;
                fgKEMOwners[fgKEMOwners.Row, "FirstnameFather"] = locOwnersEdit.txtFather.Text;
                fgKEMOwners[fgKEMOwners.Row, "ADT"] = locOwnersEdit.txtADT.Text;
                fgKEMOwners[fgKEMOwners.Row, "Passport"] = locOwnersEdit.txtPassport.Text;
                fgKEMOwners[fgKEMOwners.Row, "DOY"] = locOwnersEdit.cmbDOY.Text;
                fgKEMOwners[fgKEMOwners.Row, "AFM"] = locOwnersEdit.cmbAFM.Text;
                fgKEMOwners[fgKEMOwners.Row, "Master"] = locOwnersEdit.chkMaster.Checked;
                fgKEMOwners[fgKEMOwners.Row, "Order"] = locOwnersEdit.chkOrder.Checked;
                fgKEMOwners[fgKEMOwners.Row, "ID"] = locOwnersEdit.Rec_ID;
                fgKEMOwners[fgKEMOwners.Row, "Client_ID"] = locOwnersEdit.Client_ID;
                fgKEMOwners[fgKEMOwners.Row, "DoB"] = locOwnersEdit.lblBorn.Text;
                fgKEMOwners[fgKEMOwners.Row, "Special"] = locOwnersEdit.lblSpecial.Text;
            }
        }
        private void tsbSave_Package_Click(object sender, EventArgs e)
        {
            panNotes.Top = 188;
            panNotes.Left = 736;
            if (SaveContract())
            {
                //ucCC.cmbInvestmentPolicy.Enabled = false;
                tslEditPackage.Enabled = true;
                tslEditVersion.Enabled = true;
                tsbSave_Package.Enabled = false;
            }
        }
        private void picFilePath_Click(object sender, EventArgs e)
        {
            sFullFileName = Global.FileChoice(Global.DefaultFolder);
            txtFileName.Text = Path.GetFileName(sFullFileName);
            panFolder.Visible = false;
        }

        private void btnSave_Notes_Click(object sender, EventArgs e)
        {
            SaveContractData();
        }
        private void btnCancel_Notes_Click(object sender, EventArgs e)
        {
            iFinishAktion = 0;
            panNotes.Visible = false;
        }
        private void cmbContractType_SelectedValueChanged(object sender, EventArgs e)
        {
            ShowPanels();
        }
        //--- Insert Documents from Folder functions -----------------------------
        private void tsbAddFolder_Click(object sender, EventArgs e)
        {
            panFolder.Visible = true;
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
                if (foundRows.Length > 0) iDocType = Convert.ToInt32(foundRows[0]["ID"]);
                else iDocType = 0;

                clsClientsDocFiles klsClientDocFiles = new clsClientsDocFiles();
                klsClientDocFiles.PreContract_ID = 0;
                klsClientDocFiles.Contract_ID = iContract_ID;
                klsClientDocFiles.Client_ID = iClient_ID;
                klsClientDocFiles.ClientName = txtContractTitle.Text + "";
                klsClientDocFiles.ContractCode = txtCode.Text + "";
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

            panFolder.Visible = false;

            ShowDocFiles();
        }

        private void btnCancelDoc_Click(object sender, EventArgs e)
        {
            panFolder.Visible = false;
        }
        //--- fgRepresents functinality -------------------------------------------------------------------------- 
        private void tsbAdd_Reps_Click(object sender, EventArgs e)
        {
            bCheckRepPersons = false;
            iRP_Aktion = 0;                                             // 0 - Add, 1 - Edit
            cmbRepPerson.SelectedValue = 0;
            lblFirstname.Text = "";
            lblFather.Text = "";
            lblADT.Text = "";
            lblExpireDate.Text = "";
            lblPolice.Text = "";
            lblAFM.Text = "";
            lblDOY.Text = "";
            lblAddress.Text = "";
            lblCity.Text = "";
            lblZip.Text = "";
            lblTel.Text = "";
            lblFax.Text = "";
            lblMobile.Text = "";
            lblEMail.Text = "";
            lblCountry.Text = "";
            chkAuthRep.Checked = false;
            chkOwner.Checked = false;
            chkLegalRep.Checked = false;
            chkDirector.Checked = false;
            chkSignature.Checked = false;

            panRepresent.Visible = true;
            cmbRepPerson.Focus();
            bCheckRepPersons = true;
        }

        private void tsbEdit_Reps_Click(object sender, EventArgs e)
        {
            EditRepresent();
        }
        private void fgAttachedFiles_DoubleClick(object sender, EventArgs e)
        {    
            if ((fgAttachedFiles[fgAttachedFiles.Row, 0] + "").Trim() != "")
                if (Global.DMS_CheckFileExists("Customers/" + txtContractTitle.Text.Replace(".", "_") + "/Informing", fgAttachedFiles[fgAttachedFiles.Row, 0]+"")) 
                    Global.DMS_ShowFile("Customers/" + txtContractTitle.Text.Replace(".", "_") + "/Informing", fgAttachedFiles[fgAttachedFiles.Row, 0] + "");   // is DMS file, so show it into Web mode
                else
                    Global.DMS_ShowFile("Customers/" + txtContractTitle.Text.Replace(".", "_") + "/Invoices", fgAttachedFiles[fgAttachedFiles.Row, 0] + "");  // is DMS file, so show it into Web mode
        }
        private void fgRepresents_DoubleClick(object sender, EventArgs e)
        {
            EditRepresent();
        }
        private void tsbDel_Reps_Click(object sender, EventArgs e)
        {
            if (fgRepresents.Row > 0)
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    clsRepresentPersons RepresentPersons = new clsRepresentPersons();
                    RepresentPersons.Record_ID = Convert.ToInt32(fgRepresents[fgRepresents.Row, "ID"]);
                    RepresentPersons.DeleteRecord();
                    fgRepresents.RemoveItem(fgRepresents.Row);
                }
        }
        private void cmbRepPerson_SelectedValueChanged(object sender, EventArgs e)
        {
            DataRow[] foundRows;

            if (bCheckRepPersons)
            {
                clsClients Client = new clsClients();
                Client.Record_ID = Convert.ToInt32(cmbRepPerson.SelectedValue);
                Client.EMail = "";
                Client.Mobile = "";
                Client.AFM = "";
                Client.DoB = Convert.ToDateTime("1900/01/01");
                Client.GetRecord();
                lblFirstname.Text = Client.Firstname;
                lblFather.Text = Client.FirstnameFather;
                lblADT.Text = Client.ADT;
                lblExpireDate.Text = Client.ExpireDate;
                lblPolice.Text = Client.Police;
                lblAFM.Text = Client.AFM;
                lblDOY.Text = Client.DOY;
                lblAddress.Text = Client.Address;
                lblCity.Text = Client.City;
                lblZip.Text = Client.Zip;
                lblTel.Text = Client.Tel;
                lblFax.Text = Client.Fax;
                lblMobile.Text = Client.Mobile;
                lblEMail.Text = Client.EMail;                

                foundRows = Global.dtCountries.Select("ID = " + Client.Country_ID);
                if (foundRows.Length > 0) lblCountry.Text = foundRows[0]["Title"] + "";

                chkAuthRep.Checked = false;
                chkOwner.Checked = false;
                chkLegalRep.Checked = false;
                chkDirector.Checked = false;
                chkSignature.Checked = false;
            }
        }

        private void EditRepresent()
        {
            iRP_Aktion = 1;                                                                    // 0 - Add, 1 - Edit            
            RepresentPersons.Record_ID = Convert.ToInt32(fgRepresents[fgRepresents.Row, "ID"]);
            RepresentPersons.GetRecord();
            bCheckRepPersons = false;
            cmbRepPerson.SelectedValue = RepresentPersons.Client_ID;
            bCheckRepPersons = true;
            lblFirstname.Text = RepresentPersons.Firstname;
            lblFather.Text = RepresentPersons.Father;
            lblADT.Text = RepresentPersons.ADT;
            lblExpireDate.Text = RepresentPersons.ExpireDate;
            lblPolice.Text = RepresentPersons.Police;
            lblAFM.Text = RepresentPersons.AFM;
            lblDOY.Text = RepresentPersons.DOY;
            lblAddress.Text = RepresentPersons.Address;
            lblCity.Text = RepresentPersons.City;
            lblZip.Text = RepresentPersons.Zip;
            lblTel.Text = RepresentPersons.Tel;
            lblFax.Text = RepresentPersons.Fax;
            lblMobile.Text = RepresentPersons.Mobile;
            lblEMail.Text = RepresentPersons.EMail;
            lblCountry.Text = RepresentPersons.Country_Title;

            chkAuthRep.Checked = (RepresentPersons.AuthRep == 1 ? true : false);
            chkOwner.Checked = (RepresentPersons.Owner == 1 ? true : false);
            chkLegalRep.Checked = (RepresentPersons.LegalRep == 1 ? true : false);
            chkDirector.Checked = (RepresentPersons.Director == 1 ? true : false);
            chkSignature.Checked = (RepresentPersons.Signature == 1 ? true : false);

            panRepresent.Visible = true;
        }
        private void btnOK_Represent_Click(object sender, EventArgs e)
        {
            if (iRP_Aktion == 0) {
                clsRepresentPersons RepresentPersons = new clsRepresentPersons();
                RepresentPersons.Client_ID = Convert.ToInt32(cmbRepPerson.SelectedValue);
                RepresentPersons.Contract_ID = Convert.ToInt32(iContract_ID);
                RepresentPersons.AuthRep = (chkAuthRep.Checked? 1 : 0);
                RepresentPersons.Owner = (chkOwner.Checked? 1 : 0);
                RepresentPersons.LegalRep = (chkLegalRep.Checked? 1 : 0);
                RepresentPersons.Director = (chkDirector.Checked? 1 : 0);
                RepresentPersons.Signature = (chkSignature.Checked? 1 : 0);
                RepresentPersons.InsertRecord();
            }
            else {
                RepresentPersons.Record_ID = Convert.ToInt32(fgRepresents[fgRepresents.Row, "ID"]);
                RepresentPersons.GetRecord();
                RepresentPersons.Client_ID = Convert.ToInt32(cmbRepPerson.SelectedValue);
                RepresentPersons.Contract_ID = Convert.ToInt32(iContract_ID);
                RepresentPersons.AuthRep = (chkAuthRep.Checked? 1 : 0);
                RepresentPersons.Owner = (chkOwner.Checked? 1 : 0);
                RepresentPersons.LegalRep = (chkLegalRep.Checked? 1 : 0);
                RepresentPersons.Director = (chkDirector.Checked? 1 : 0);
                RepresentPersons.Signature = (chkSignature.Checked? 1 : 0);
                RepresentPersons.EditRecord();
            }
            panRepresent.Visible = false;

            ShowRepresents();
        }

        private void btnCancel_Represent_Click(object sender, EventArgs e)
        {
            panRepresent.Visible = false;
        }
        private void ShowRepresents()
        {
            bCheckRepPersons = false;

            if (iContract_ID != 0)
            {
                fgRepresents.Redraw = false;
                fgRepresents.Rows.Count = 1;

                RepresentPersons.Contract_ID = iContract_ID;
                RepresentPersons.GetList();
                foreach (DataRow dtRow in RepresentPersons.List.Rows)
                    fgRepresents.AddItem(dtRow["Fullname"] + "\t" + dtRow["Properties"] + "\t" + dtRow["ADT"] + "\t" + dtRow["ID"]);

                fgRepresents.AutoSizeRows();
                fgRepresents.Redraw = true;

                //-------------- Define cmbRepPerson List ------------------
                dtView = Global.dtClients.Copy().DefaultView;
                dtView.RowFilter = "Is_RepresentPerson = 1";
                cmbRepPerson.DataSource = dtView;
                cmbRepPerson.DisplayMember = "Fullname";
                cmbRepPerson.ValueMember = "ID";

                bCheckRepPersons = true;
            }
        }
        //--------------------------------------------------------------------------------------------------------
        private void ShowPanels()
        {
            switch (Convert.ToInt32(cmbContractType.SelectedIndex))
            {
                case 0:
                    panAtomiki.Visible = true;
                    tpGeneral.BackColor = panAtomiki.BackColor;
                    panKoini.Visible = false;
                    panCompany.Visible = false;
                    tpXM.BackColor = panAtomiki.BackColor;
                    tpPackage.BackColor = panAtomiki.BackColor;
                    break;
                case 1:
                    panAtomiki.Visible = false;
                    panKoini.Visible = true;
                    tpGeneral.BackColor = panKoini.BackColor;
                    panCompany.Visible = false;
                    tpXM.BackColor = panKoini.BackColor;
                    tpPackage.BackColor = panKoini.BackColor;
                    break;
                case 2:
                    panAtomiki.Visible = false;
                    panKoini.Visible = false;
                    panCompany.Visible = true;
                    tpGeneral.BackColor = panCompany.BackColor;
                    tpXM.BackColor = panCompany.BackColor;
                    tpPackage.BackColor = panCompany.BackColor;
                    break;
            }
        }

        private void tsbSave_Notes_Click(object sender, EventArgs e)
        {
            if (iContract_ID != 0)
            {
                clsContracts_Details klsContract_Details = new clsContracts_Details();
                klsContract_Details.Record_ID = iContract_Details_ID;
                klsContract_Details.GetRecord();
                klsContract_Details.AgreementNotes = txtContractNotes.Text;
                klsContract_Details.EditRecord();
            }
        }

        private void tsbViewInformings_Click(object sender, EventArgs e)
        {
            if ((fgInformings[fgInformings.Row, 4] + "").Trim() != "")
                if (Convert.ToInt32(fgInformings[fgInformings.Row, 7]) == 3 || Convert.ToInt32(fgInformings[fgInformings.Row, 7]) == 6)
                    Global.DMS_ShowFile("Customers/" + txtContractTitle.Text.Replace(".", "_") + "/Invoices", fgInformings[fgInformings.Row, 4] + "");   // is DMS file, so show it into Web mode
                else
                    Global.DMS_ShowFile("Customers/" + txtContractTitle.Text.Replace(".", "_") + "/Informing", fgInformings[fgInformings.Row, 4] + "");  // is DMS file, so show it into Web mode
        }
        private void tslAttachedFiles_Click(object sender, EventArgs e)
        {
            int j = 0;
            sTemp = fgInformings[fgInformings.Row, 8] + "";
            string[] tokens = sTemp.Split('~');
            j = tokens.Length - 2;

            fgAttachedFiles.Redraw = false;
            fgAttachedFiles.Rows.Count = 1;
            for (i = 0; i <= j; i++)
                fgAttachedFiles.AddItem(tokens[i]);
            fgAttachedFiles.Redraw = true;
            panAttachedFiles.Visible = true;
        }

        private void picCloseCommandBuffer_Click(object sender, EventArgs e)
        {
            panAttachedFiles.Visible = false;
        }

        private void tsbDeleteOwner_Click(object sender, EventArgs e)
        {
            if (fgKEMOwners.Row > 0)
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    clsClients_Contracts Clients_Contracts = new clsClients_Contracts();
                    Clients_Contracts.Record_ID = Convert.ToInt32(fgKEMOwners[fgKEMOwners.Row, "ID"]);
                    Clients_Contracts.DeleteRecord();
                    fgKEMOwners.RemoveItem(fgKEMOwners.Row);
                }
        }
        private void tsbSaveGeneral_Click(object sender, EventArgs e)
        {
            iEditMode = 1;
            panNotes.Left = 740;
            panNotes.Top = 170;

            if (SaveContract())
            {
                panAtomiki.Enabled = false;
                panKoini.Enabled = false;
                panCompany.Enabled = false;
                panCommon.Enabled = false;

                tsbKeyGeneral.Enabled = true;
                tsbKeyGeneral.Visible = true;
                tsbSaveGeneral.Visible = false;
                this.Refresh();
            }
        }

        private void tabContractData_SelectedIndexChanged(Object sender, EventArgs e)
        {
            switch (tabContractData.TabPages[tabContractData.SelectedIndex].Name)
            {
                case "tpGeneral":
                    break;
                case "tpPackage":
                    ucCC.BackColor = tpPackage.BackColor;
                    ucCC.lblEditMode.Text = iEditMode.ToString();
                    ucCC.lblClientFullName.Text = sClientFullName;
                    ucCC.lblClient_ID.Text = iClient_ID.ToString();
                    ucCC.lblContract_ID.Text = iContract_ID.ToString();
                    ucCC.lblContractTitle.Text = txtContractTitle.Text.Replace(".", "_");
                    ucCC.lblCode.Text = txtCode.Text;
                    ucCC.lblPortfolio.Text = txtPortfolio.Text;
                    ucCC.lblRealClient_ID.Text = iClient_ID.ToString();
                    ucCC.ShowRecord(1, iRealClient_ID, iClient_ID, iContract_ID, iContract_Details_ID, iContract_Packages_ID, 1, iRightsLevel);
                    break;
                case "tpXM":
                    if (iService_ID == 3) panRTO.Visible = true;
                    else panRTO.Visible = false;
                    //panXM.Enabled = false;
                    break;
                case "tpDocuments":
                    ShowDocFiles();
                    break;
                case "tpInforming":
                    ShowInfos();
                    break;
                case "tpInvestmentProposals":
                    ShowInvestOffers();
                    break;
                case "tpAUM":
                    ShowClientAUM();
                    break;
                case "tpTransactions":
                    ShowTransactions();
                    break;
                case "tpRepresent":
                    ShowRepresents();
                    break;
            }
        }
        private bool SaveContract()
        {
            bool bResult = false;
            chkEdit.Checked = false;
            sError = "";

            if (txtCode.Text.Trim() == "") sError = "Καταχωρήστε των κωδικό" + "\n";

            if (txtPortfolio.Text.Trim() == "") sError = sError + "Καταχωρήστε το Portfolio" + "\n";

            if (dDateStart.Text.Trim() == "") sError = sError + "Καταχωρήστε την ημερομηνία έναρξης της σύμβασης" + "\n";

            if (dDateFinish.Text.Trim() == "") sError = sError + "Καταχωρήστε την ημερομηνία λήξης της σύμβασης" + "\n";

            if ((ucCC.lblContract_ID.Text.Trim() == "") || (ucCC.lblContract_ID.Text == "0")) sError = sError + "Επιλέξτε πακέτο υπηρεσιών" + "\n";


            if (sError == "")
            {
                bResult = true;
                txtNotes.Text = "";
                cmbDocTypes.SelectedValue = 0;
                txtFileName.Text = "";
                panNotes.Visible = true;
            }
            else
            {
                bResult = false;
                MessageBox.Show(sError, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return bResult;
        }

        private void txtCode_LostFocus(object sender, EventArgs e)
        {
            txtCode.Text = txtCode.Text.Trim();
        }
        private void tslEditPackage_Click(object sender, EventArgs e)
        {
            iEditMode = 4;
            ucCC.EditPackage();
            tslEditPackage.Enabled = false;
            tslEditVersion.Enabled = false;
            tsbSave_Package.Enabled = true;
        }

        private void tslEditVersion_Click(object sender, EventArgs e)
        {
            iEditMode = 5;
            ucCC.EditPackageVersion();
            tslEditPackage.Enabled = false;
            tslEditVersion.Enabled = false;
            tsbSave_Package.Enabled = true;
        }
        private void tsbXM_History_Click(object sender, EventArgs e)
        {

        }
        private void tsbXM_Key_Click(object sender, EventArgs e)
        {
            tsbXM_Key.Visible = false;
            tsbXM_Save.Visible = true;
            panXM.Enabled = true;
        }

        private void tsbXM_Save_Click(object sender, EventArgs e)
        {
            bSpecificConstraints = true;

            if (iService_ID == 3 && rbSpecificConstraints.Checked)                                                                      // 3 - Diaxeirisi
                if (!chkMonetaryRisk.Checked && !chkIndividualBonds.Checked && !chkMutualFunds.Checked && !chkBondedETFs.Checked &&
                    !chkIndividualShares.Checked && !chkMixedFunds.Checked && !chkMixedETFs.Checked && !chkFunds.Checked &&
                    !chkETFs.Checked && !chkInvestmentGrade.Checked && txtMiscInstructions.Text == "")
                    bSpecificConstraints = false;

            if (bSpecificConstraints)  {
                iEditMode = 1;
                //SaveContractData();
                tsbXM_Key.Visible = true;
                tsbXM_Save.Visible = false;
                panXM.Enabled = false;

                panNotes.Left = 18;
                panNotes.Top = 178;
                panNotes.Visible = true;
            }
            else MessageBox.Show("Τσεκάρετε κάποιον ειδικό περιορισμό", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        //--- fgBlocks functionality-------------------------------------------------------------
        private void lnkBlocks_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            fgBlocks.Redraw = false;
            fgBlocks.Rows.Count = 1;

            clsContract_Blocks klsContract_Blocks = new clsContract_Blocks();
            klsContract_Blocks.Contract_ID = iContract_ID;
            klsContract_Blocks.GetList();
            foreach (DataRow dtRow in klsContract_Blocks.List.Rows)
                    fgBlocks.AddItem(dtRow["DateFrom"] + "\t" + dtRow["DateTo"] + "\t" + dtRow["ID"]);
            fgBlocks.Redraw = true;

            panBlocks.Visible = true;
        }

        private void btnOK_Blocks_Click(object sender, EventArgs e)
        {
            for (i = 1; i <= fgBlocks.Rows.Count - 1; i++) {
                clsContract_Blocks klsContract_Blocks = new clsContract_Blocks();
                klsContract_Blocks.Record_ID = Convert.ToInt32(fgBlocks[i, 2]);
                klsContract_Blocks.Contract_ID = iContract_ID;
                klsContract_Blocks.DateFrom = Convert.ToDateTime(fgBlocks[i, 0]);
                klsContract_Blocks.DateTo = Convert.ToDateTime(fgBlocks[i, 1]);
                if (Convert.ToInt32(fgBlocks[i, 2]) == 0) fgBlocks[i, 2] = klsContract_Blocks.InsertRecord();
                else klsContract_Blocks.EditRecord();
            }
            panBlocks.Visible = false;

            txtNotes.Text = "";
            cmbDocTypes.SelectedValue = 0;
            txtFileName.Text = "";
            panNotes.Visible = true;
        }

        private void btnCancel_Blocks_Click(object sender, EventArgs e)
        {
            panBlocks.Visible = false;
        }

        private void tsbAdd_Block_Click(object sender, EventArgs e)
        {
            fgBlocks.AddItem(DateTime.Now.ToString("dd/MM/yyyy") + "\t" + "31/12/2070" + "\t" + "0");
        }
        private void tsbDel_Block_Click(object sender, EventArgs e)
        {
            if (fgBlocks.Row > 0)   {
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes) { 
                    clsContract_Blocks klsContract_Blocks = new clsContract_Blocks();
                    klsContract_Blocks.Record_ID = Convert.ToInt32(fgBlocks[fgBlocks.Row, 2]);
                    klsContract_Blocks.DeleteRecord();
                    fgBlocks.RemoveItem(fgBlocks.Row);
                }
            }
        }
        //----------------------------------------------------------------------------------------
        private void EditFees()
        {
            //--- Brokerage Fees -----------
            for (i = 2; i <= ucCC.fgBrokerageFees.Rows.Count - 1; i++)
            {
                if ( (Convert.ToDecimal(ucCC.fgBrokerageFees[i, 14]) != 0) || (Convert.ToDecimal(ucCC.fgBrokerageFees[i, 15]) != 0))
                {
                    clsClientsBrokerageFees ClientsBrokerageFees = new clsClientsBrokerageFees();
                    ClientsBrokerageFees.Contract_ID = iContract_ID;
                    ClientsBrokerageFees.Contract_Packages_ID = iContract_Packages_ID;
                    ClientsBrokerageFees.SPBF_ID = Convert.ToInt32(ucCC.fgBrokerageFees[i, 20]);
                    ClientsBrokerageFees.Product_ID = Convert.ToInt32(ucCC.fgBrokerageFees[i, 21]);
                    ClientsBrokerageFees.ProductCategory_ID = Convert.ToInt32(ucCC.fgBrokerageFees[i, 22]);
                    ClientsBrokerageFees.DateFrom = Convert.ToDateTime(ucCC.fgBrokerageFees[i, 12]);
                    ClientsBrokerageFees.DateTo = Convert.ToDateTime(ucCC.fgBrokerageFees[i, 13]);
                    ClientsBrokerageFees.BrokerageFeesDiscount = Convert.ToSingle(ucCC.fgBrokerageFees[i, 14]);
                    ClientsBrokerageFees.TicketFeesDiscount = Convert.ToSingle(ucCC.fgBrokerageFees[i, 15]);
                    ClientsBrokerageFees.BrokerageFeesBuy = Convert.ToSingle(ucCC.fgBrokerageFees[i, 16]);
                    ClientsBrokerageFees.BrokerageFeesSell = Convert.ToSingle(ucCC.fgBrokerageFees[i, 17]);
                    ClientsBrokerageFees.TicketFeesBuy = Convert.ToSingle(ucCC.fgBrokerageFees[i, 18]);
                    ClientsBrokerageFees.TicketFeesSell = Convert.ToSingle(ucCC.fgBrokerageFees[i, 19]);
                    ClientsBrokerageFees.InsertRecord();
                }
            }

            for (i = 2; i <= ucCC.fgAdvisoryFees.Rows.Count - 1; i++)
            {
                clsClientsAdvisoryFees ClientsAdvisoryFees = new clsClientsAdvisoryFees();
                ClientsAdvisoryFees.Contract_ID = iContract_ID;
                ClientsAdvisoryFees.Contract_Packages_ID = iContract_Packages_ID;
                ClientsAdvisoryFees.SPAF_ID = Convert.ToInt32(ucCC.fgAdvisoryFees[i, "SPAF_ID"]);
                ClientsAdvisoryFees.AmountFrom = Convert.ToSingle(ucCC.fgAdvisoryFees[i, "AmountFrom"]);
                ClientsAdvisoryFees.AmountTo = Convert.ToSingle(ucCC.fgAdvisoryFees[i, "AmountTo"]);
                ClientsAdvisoryFees.DateFrom = Convert.ToDateTime(ucCC.fgAdvisoryFees[i, "DiscountDateFrom"]);
                ClientsAdvisoryFees.DateTo = Convert.ToDateTime(ucCC.fgAdvisoryFees[i, "DiscountDateTo"]);
                ClientsAdvisoryFees.AdvisoryFees_Discount = Convert.ToDecimal(ucCC.fgAdvisoryFees[i, "AdvisoryFees_Discount"]);
                ClientsAdvisoryFees.FinishAdvisoryFees = Convert.ToDecimal(ucCC.fgAdvisoryFees[i, "FinishAdvisoryFee"]);
                ClientsAdvisoryFees.MinimumFees_Discount = Convert.ToSingle(ucCC.txtAdvisory_MinimumFees_Discount.Text);
                ClientsAdvisoryFees.MinimumFees = Convert.ToSingle(ucCC.txtAdvisory_MinimumFees.Text);
                ClientsAdvisoryFees.AllManFees = ucCC.lblAdvisory_AllManFees.Text + "";
                ClientsAdvisoryFees.InsertRecord();
            }

            for (i = 2; i <= ucCC.fgDiscretFees.Rows.Count - 1; i++)
            {
                clsClientsDiscretFees ClientsDiscretFees = new clsClientsDiscretFees();
                ClientsDiscretFees.Contract_ID = iContract_ID;
                ClientsDiscretFees.Contract_Packages_ID = iContract_Packages_ID;
                ClientsDiscretFees.SPDF_ID = Convert.ToInt32(ucCC.fgDiscretFees[i, "SPDF_ID"]);
                ClientsDiscretFees.AmountFrom = Convert.ToSingle(ucCC.fgDiscretFees[i, "AmountFrom"]);
                ClientsDiscretFees.AmountTo = Convert.ToSingle(ucCC.fgDiscretFees[i, "AmountTo"]);
                ClientsDiscretFees.DateFrom = Convert.ToDateTime(ucCC.fgDiscretFees[i, "DiscountDateFrom"]);
                ClientsDiscretFees.DateTo = Convert.ToDateTime(ucCC.fgDiscretFees[i, "DiscountDateTo"]);
                ClientsDiscretFees.DiscretFees_Discount = Convert.ToDecimal(ucCC.fgDiscretFees[i, "DiscretFees_Discount"]);
                ClientsDiscretFees.FinishDiscretFees = Convert.ToDecimal(ucCC.fgDiscretFees[i, "FinishDiscretFee"]);
                ClientsDiscretFees.MinimumFees_Discount = Convert.ToSingle(ucCC.txtDiscret_MinimumFees_Discount.Text);
                ClientsDiscretFees.MinimumFees = Convert.ToSingle(ucCC.txtDiscret_MinimumFees.Text);
                ClientsDiscretFees.AllManFees = ucCC.lblDiscret_AllManFees.Text + "";
                ClientsDiscretFees.InsertRecord();
            }

            //--- Custody Fees -----------
            for (i = 2; i <= ucCC.fgCustodyFees.Rows.Count - 1; i++)
            {
                clsClientsCustodyFees ClientsCustodyFees = new clsClientsCustodyFees();
                ClientsCustodyFees.Contract_ID = iContract_ID;
                ClientsCustodyFees.Contract_Packages_ID = iContract_Packages_ID;
                ClientsCustodyFees.SPCF_ID = Convert.ToInt32(ucCC.fgCustodyFees[i, 7]);
                ClientsCustodyFees.AmountFrom = Convert.ToSingle(ucCC.fgCustodyFees[i, 0]);
                ClientsCustodyFees.AmountTo = Convert.ToSingle(ucCC.fgCustodyFees[i, 1]);
                ClientsCustodyFees.DateFrom = Convert.ToDateTime(ucCC.fgCustodyFees[i, 3]);
                ClientsCustodyFees.DateTo = Convert.ToDateTime(ucCC.fgCustodyFees[i, 4]);
                ClientsCustodyFees.CustodyFees_Discount = Convert.ToDecimal(ucCC.fgCustodyFees[i, 5]);
                ClientsCustodyFees.FinishCustodyFees = Convert.ToDecimal(ucCC.fgCustodyFees[i, 6]);
                ClientsCustodyFees.InsertRecord();
            }

            //--- Admin Fees -----------
            for (i = 2; i <= ucCC.fgAdminFees.Rows.Count - 1; i++)
            {
                clsClientsAdminFees ClientsAdminFees = new clsClientsAdminFees();
                ClientsAdminFees.Contract_ID = iContract_ID;
                ClientsAdminFees.Contract_Packages_ID = iContract_Packages_ID;
                ClientsAdminFees.SPAF_ID = Convert.ToInt32(ucCC.fgAdminFees[i, "SPAF_ID"]);
                ClientsAdminFees.AmountFrom = Convert.ToSingle(ucCC.fgAdminFees[i, "AmountFrom"]);
                ClientsAdminFees.AmountTo = Convert.ToSingle(ucCC.fgAdminFees[i, "AmountTo"]);
                ClientsAdminFees.DateFrom = Convert.ToDateTime(ucCC.fgAdminFees[i, "DiscountDateFrom"]);
                ClientsAdminFees.DateTo = Convert.ToDateTime(ucCC.fgAdminFees[i, "DiscountDateTo"]);
                ClientsAdminFees.AdminFees_Discount = Convert.ToSingle(ucCC.fgAdminFees[i, "AdminFees_Discount"]);
                ClientsAdminFees.FinishAdminFees = Convert.ToSingle(ucCC.fgAdminFees[i, "FinishAdminFee"]);
                ClientsAdminFees.MinimumFees_Discount = Convert.ToSingle(ucCC.txtAdminMinimumFees_Discount.Text);
                ClientsAdminFees.MinimumFees = Convert.ToSingle(ucCC.txtAdminMinimumFees.Text);
                ClientsAdminFees.InsertRecord();
            }

            //--- DealAdvisory Fees -----------
            for (i = 2; i <= ucCC.fgDealAdvisoryFees.Rows.Count - 1; i++)
            {
                clsClientsDealAdvisoryFees ClientsDealAdvisoryFees = new clsClientsDealAdvisoryFees();
                ClientsDealAdvisoryFees.Contract_ID = iContract_ID;
                ClientsDealAdvisoryFees.Contract_Packages_ID = iContract_Packages_ID;
                ClientsDealAdvisoryFees.SPDAF_ID = Convert.ToInt32(ucCC.fgDealAdvisoryFees[i, "SPDAF_ID"]);
                ClientsDealAdvisoryFees.AmountFrom = Convert.ToSingle(ucCC.fgDealAdvisoryFees[i, "AmountFrom"]);
                ClientsDealAdvisoryFees.AmountTo = Convert.ToSingle(ucCC.fgDealAdvisoryFees[i, "AmountTo"]);
                ClientsDealAdvisoryFees.DateFrom = Convert.ToDateTime(ucCC.fgDealAdvisoryFees[i, "DiscountDateFrom"]);
                ClientsDealAdvisoryFees.DateTo = Convert.ToDateTime(ucCC.fgDealAdvisoryFees[i, "DiscountDateTo"]);
                ClientsDealAdvisoryFees.DealAdvisoryFees_Discount = Convert.ToDecimal(ucCC.fgDealAdvisoryFees[i, "DealAdvisoryFees_Discount"]);
                ClientsDealAdvisoryFees.FinishDealAdvisoryFees = Convert.ToDecimal(ucCC.fgDealAdvisoryFees[i, "FinishDealAdvisoryFee"]);
                ClientsDealAdvisoryFees.InsertRecord();
             }

            //--- FX Fees -----------
            for (i = 2; i <= ucCC.fgFXFees.Rows.Count - 1; i++)
            {
                //If Convert.ToDecimal(ucCC.fgFXFees[i, 5)) != 0 Then
                clsClientsFXFees ClientsFXFees = new clsClientsFXFees();
                ClientsFXFees.Contract_ID = iContract_ID;
                ClientsFXFees.Contract_Packages_ID = iContract_Packages_ID;
                ClientsFXFees.SPFF_ID = Convert.ToInt32(ucCC.fgFXFees[i, "ID"]);
                ClientsFXFees.AmountFrom = Convert.ToSingle(ucCC.fgFXFees[i, 0]);
                ClientsFXFees.AmountTo = Convert.ToSingle(ucCC.fgFXFees[i, 1]);
                ClientsFXFees.DateFrom = Convert.ToDateTime(ucCC.fgFXFees[i, 3]);
                ClientsFXFees.DateTo = Convert.ToDateTime(ucCC.fgFXFees[i, 4]);
                ClientsFXFees.FXFees_Discount = Convert.ToSingle(ucCC.fgFXFees[i, 5]);
                ClientsFXFees.FinishFXFees = Convert.ToSingle(ucCC.fgFXFees[i, 6]);
                ClientsFXFees.InsertRecord();
                //End If
            }

            //--- Settlements Fees -----------
            for (i = 2; i <= ucCC.fgSettlementsFees.Rows.Count - 1; i++)
            {
                //if Convert.ToDecimal(ucCC.fgSettlementsFees[i, 14)) != 0 !! Convert.ToDecimal(ucCC.fgSettlementsFees[i, 15)) != 0 Then;
                clsClientsSettlementFees ClientsSettlementFees = new clsClientsSettlementFees(); ;
                ClientsSettlementFees.Contract_ID = iContract_ID;
                ClientsSettlementFees.Contract_Packages_ID = iContract_Packages_ID;
                ClientsSettlementFees.SPSF_ID = Convert.ToInt32(ucCC.fgSettlementsFees[i, 23]);
                ClientsSettlementFees.Product_ID = Convert.ToInt32(ucCC.fgSettlementsFees[i, 21]);
                ClientsSettlementFees.ProductCategory_ID = Convert.ToInt32(ucCC.fgSettlementsFees[i, 22]);
                ClientsSettlementFees.DateFrom = Convert.ToDateTime(ucCC.fgSettlementsFees[i, 12]);
                ClientsSettlementFees.DateTo = Convert.ToDateTime(ucCC.fgSettlementsFees[i, 13]);
                ClientsSettlementFees.SettlementFeesDiscount = Convert.ToDecimal(ucCC.fgSettlementsFees[i, 14]);
                ClientsSettlementFees.TicketFeesDiscount = Convert.ToDecimal(ucCC.fgSettlementsFees[i, 15]);
                ClientsSettlementFees.SettlementFeesBuy = Convert.ToDecimal(ucCC.fgSettlementsFees[i, 16]);
                ClientsSettlementFees.SettlementFeesSell = Convert.ToDecimal(ucCC.fgSettlementsFees[i, 17]);
                ClientsSettlementFees.TicketFeesBuy = Convert.ToSingle(ucCC.fgSettlementsFees[i, 18]);
                ClientsSettlementFees.TicketFeesSell = Convert.ToSingle(ucCC.fgSettlementsFees[i, 19]);
                ClientsSettlementFees.InsertRecord();
                //End If
            }
        }
        private void ShowDocFiles()
        {
            fgDocFiles.Redraw = false;
            fgDocFiles.Rows.Count = 1;

            if (iContract_ID != 0)
            {
                clsClientsDocFiles klsClientDocFiles = new clsClientsDocFiles();
                klsClientDocFiles.Client_ID = 0;
                klsClientDocFiles.PreContract_ID = 0;
                klsClientDocFiles.Contract_ID = iContract_ID;
                klsClientDocFiles.DocTypes = 0;
                klsClientDocFiles.GetList();

                foreach (DataRow dtRow in klsClientDocFiles.List.Rows)
                {
                    if (Convert.ToInt32(dtRow["OldFile"]) == 0 || (chkOldFiles.Checked && Convert.ToInt32(dtRow["OldFile"]) == 1)) 
                            fgDocFiles.AddItem(Convert.ToDateTime(dtRow["DateIns"]).ToString("dd/MM/yyyy") + "\t" + dtRow["Code"] + "\t" + dtRow["Tipos"] + "\t" +
                                   dtRow["FileName"] + "\t" + (Convert.ToInt32(dtRow["OldFile"]) == 1? true: false) + "\t" +
                                   dtRow["ID"] + "\t" + dtRow["DocTypes"] + "\t" + dtRow["DMS_Files_ID"] + "\t" + dtRow["PD_Group_ID"]);
                }   
            }
            fgDocFiles.Redraw = true;
        }
        private void ShowInfos()
        {
            if (iContract_ID != 0)
            {
                fgInformings.Redraw = false;
                fgInformings.Rows.Count = 1;

                clsInformings klsInforming = new clsInformings();
                klsInforming.Client_ID = 0;
                klsInforming.Contract_ID = iContract_ID;
                klsInforming.GetList();
                foreach (DataRow dtRow in klsInforming.List.Rows)
                {
                    sTemp = "";
                    if (Convert.ToInt32(dtRow["AttachedFilesCount"]) > 0) sTemp = dtRow["AttachedFilesCount"] + "";

                    fgInformings.AddItem(dtRow["DateIns"] + "\t" + dtRow["InformMethod"] + "\t" + dtRow["Subject"] + "\t" +
                           dtRow["Body"] + "\t" + dtRow["FileName"] + "\t" + sTemp + "\t" + dtRow["ID"] + "\t" +
                           dtRow["Source_ID"] + "\t" + dtRow["AttachedFiles"]);
                }
            fgInformings.Redraw = true;
            }
        }
        private void ShowInvestOffers()
        {

        }
        private void ShowClientAUM()
        {

        }
        private void ShowTransactions()
        {

        }    
        private void ShowOwners()
        {
            fgKEMOwners.Redraw = false;
            fgKEMOwners.Rows.Count = 1;
            if (iContract_ID != 0)
            {
                //Contracts = New clsContracts();
                Contracts.Record_ID = iContract_ID;
                Contracts.GetOwnersList();
                foreach (DataRow dtRow in Contracts.List.Rows)
                    fgKEMOwners.AddItem(dtRow["ClientName"] + "\t" + dtRow["FirstnameFather"] + "\t" + dtRow["ADT"] + "\t" + dtRow["Passport"] + "\t" + 
                        dtRow["DOY"] + "\t" + dtRow["AFM"] + "\t" + dtRow["IsMaster"] + "\t" + dtRow["IsOrder"] + "\t" + dtRow["ID"] + "\t" + 
                        dtRow["Client_ID"] + "\t" + dtRow["DoB"] + "\t" + dtRow["Special_Title"]);
            }
            fgKEMOwners.Redraw = true;
            if (fgKEMOwners.Rows.Count > 1) fgKEMOwners.Row = 1;
        }
        public int Aktion { get { return this.iAktion; } set { this.iAktion = value; } }
        public int Contract_ID { get { return this.iContract_ID; } set { this.iContract_ID = value; } }
        public int Contract_Details_ID { get { return this.iContract_Details_ID; } set { this.iContract_Details_ID = value; } }
        public int Contract_Packages_ID { get { return this.iContract_Packages_ID; } set { this.iContract_Packages_ID = value; } }
        public int Client_ID { get { return this.iClient_ID; } set { this.iClient_ID = value; } }
        public int ClientType { get { return this.iClientType; } set { this.iClientType = value; } }
        public string ClientFullName { get { return this.sClientFullName; } set { this.sClientFullName = value; } }
        public int FinishAktion { get { return this.iFinishAktion; } set { this.iFinishAktion = value; } }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
    }
}
