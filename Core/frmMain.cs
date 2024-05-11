using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace Core
{
    public partial class frmMain : Form
    {
        public frmMain(string sParams)
        {
            InitializeComponent();
            this.FormClosing += frmMain_FormClosing;

            Thread t = new Thread(new ThreadStart(StartForm));
            t.Start();

            string[] tokens = sParams.Split(';');

            Global Global = new Global();
            Global.User_ID = Convert.ToInt32(tokens[0]);
            Global.DBSuffix = tokens[1] + "";

            Global.Initialization();

            clsUsers User = new clsUsers();
            User.Record_ID = Global.User_ID;
            User.GetMenu();
            foreach (DataRow dtRow in User.List.Rows)
            {
                if (Global.UserStatus == 1) {                       // it's Superuser - full access
                    dtRow["Status"] = 2;
                    dtRow["Extra"] = "";
                }
                switch (Convert.ToInt32(dtRow["Menu_ID"]))
                {
                    //--- menu Contracts -------------------------------------------------------------------
                    case 1:
                        menuClients.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        break;
                    case 2:
                        menuPreContractSteps.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuPreContractSteps.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 3:
                        menuClientsList.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuClientsList.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 4:
                        menuInfluenceCenters.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuInfluenceCenters.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 5:
                        menuIntroducersList.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuIntroducersList.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 6:
                        menuResponsePersons.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuResponsePersons.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 7:
                        tsmi1.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        break;
                    case 8:
                        menuCandidatesSearch.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuCandidatesSearch.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 9:
                        menuClientsSearch.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuClientsSearch.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 10:
                        menuContractsSearch.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuContractsSearch.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 11:
                        tsmi2.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        break;
                    case 12:
                        menuRandevou.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuRandevou.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 13:
                        menuEvents.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuEvents.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 14:
                        menuRMActivityReports.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuRMActivityReports.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 15:
                        tsmi3.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        break;
                    case 16:
                        menuBlackList.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuBlackList.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 17:
                        menuClientsRequests.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuClientsRequests.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 18:
                        tsmi4.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        break;
                    case 19:
                        menuOfficialInforming.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuOfficialInforming.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 20:
                        menuClientsRequests.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuClientsRequests.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 21:
                        tsmi5.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        break;
                    case 22:
                        menuExit.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        break;
                    //--- menu Products -------------------------------------------------------------------
                    case 27:
                        menuProducts.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        break;
                    case 28:
                        menuProductsList.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuProductsList.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 29:
                        menuSelectedProductsList.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuSelectedProductsList.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 30:
                        menuProductsAccordance.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuProductsAccordance.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 31:
                        menuStandardPortfolios.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuStandardPortfolios.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 32:
                        menuProductsPrices.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuProductsPrices.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 33:
                        menuInvestmentCommittees.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuInvestmentCommittees.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    //--- menu Transactions -------------------------------------------------------------------
                    case 37:
                        menuTransactions.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        break;
                    case 38:
                        menuInvestProposals.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuInvestProposals.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 39:
                        menuDPMOrdersList.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuDPMOrdersList.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 40:
                        tsmi11.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        break;
                    case 41:
                        menuSecurities_Lists.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuSecurities_Lists.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 42:
                        menuCommandsSearch.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuCommandsSearch.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 43:
                        menuStatementsCheck.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuStatementsCheck.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 44:
                        tsmi12.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        break;
                    case 45:
                        menuFX_List.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuFX_List.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 46:
                        menuFX_Search.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuFX_Search.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 47:
                        tsmi13.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        break;
                    case 48:
                        menuLL_List.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuLL_List.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 49:
                        menuLL_Search.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuLL_Search.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    //--- menu Custody Services -------------------------------------------------------------------
                    case 53:
                        tsmi22.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        break;
                    case 54:
                        menuExecutionFiles.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuExecutionFiles.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 55:
                        menuExecutionFilesNew.Visible = true;
                        menuExecutionFilesNew.Tag = "3;";
                        break;
                    case 56:
                        menuExecutionFilesFX.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuExecutionFilesFX.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;

                    //--- menu Reports -------------------------------------------------------------------
                    case 65:
                        menuReports.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        break;
                    case 66:
                        menuRpt_TRS.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuRpt_TRS.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 67:
                        menuRpt_PeriodicalEvaluation.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuRpt_PeriodicalEvaluation.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 68:
                        menuRpt_ExPostCost.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuRpt_ExPostCost.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    //--- menu Accounting -------------------------------------------------------------------
                    case 72:
                        menuAccounting.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        break;
                    case 73:
                        menuAcc_Contracts.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuAcc_Contracts.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 74:
                        menuAcc_InvoicesRTO.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuAcc_InvoicesRTO.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 75:
                        menuAcc_InvoicesFX.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuAcc_InvoicesFX.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 76:
                        menuAcc_InvoicesMF.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuAcc_InvoicesMF.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 77:
                        menuAcc_InvoicesAF.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuAcc_InvoicesAF.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 78:
                        break;
                    case 79:
                        menuAcc_InvoicesPF.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuAcc_InvoicesPF.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 80:
                        menuAcc_InvoicesRF.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuAcc_InvoicesRF.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 81:
                        menuInvoicesControl.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuInvoicesControl.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    //--- menu Options -------------------------------------------------------------------
                    case 86:
                        menuOptions.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        break;
                    case 87:
                        menuOpt_MiscLists.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuOpt_MiscLists.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 88:
                        menuOpt_ProcurementCategories.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuOpt_ProcurementCategories.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 89:
                        menuOpt_ServicesProviders.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuOpt_ServicesProviders.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 90:
                        menuOpt_ServicesPackages.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuOpt_ServicesPackages.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 91:
                        tsmi71.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        break;
                    case 92:
                        menuOpt_Settings.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuOpt_Settings.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 93:
                        menuOpt_Alerts.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuOpt_Alerts.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 94:
                        menuOpt_Users.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuOpt_Users.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    //--- menu Tools -------------------------------------------------------------------
                    case 100:
                        menuTools.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        break;
                    case 101:
                        menuImportData.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuImportData.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 102:
                        menuInvestProposalMonitoring.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuInvestProposalMonitoring.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 103:
                        menuBackOfficeDocuments.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuBackOfficeDocuments.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                    case 104:
                        menuSendSMS.Visible = (Convert.ToInt32(dtRow["Status"]) > 0 ? true : false);
                        menuSendSMS.Tag = dtRow["Status"] + ";" + dtRow["Extra"];
                        break;
                }
            }

            t.Abort();

            slUserName.Text = Global.UserName;
            slUserLocation.Text = "Location: " + Global.UserLocation;
            slVersion.Text = "Version: " + Global.Version;
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        public void StartForm()
        {
            Application.Run(new frmSplash());
        }
        private void menuPreContractSteps_Click(object sender, EventArgs e)
        {
            OpenForm("1;1;" + menuPreContractSteps.Tag);
        }
        private void menuClientsList_Click(object sender, EventArgs e)
        {
            OpenForm("1;2;" + menuClientsList.Tag);
        }
        private void menuInfluenceCenters_Click(object sender, EventArgs e)
        {
            OpenForm("1;3;" + menuInfluenceCenters.Tag);
        }
        private void menuIntroducersList_Click(object sender, EventArgs e)
        {
            OpenForm("1;4;" + menuIntroducersList.Tag);
        }
        private void menuResponsePersons_Click(object sender, EventArgs e)
        {
            OpenForm("1;5;" + menuResponsePersons.Tag);
        }
        private void menuCandidatesSearch_Click(object sender, EventArgs e)
        {
            OpenForm("1;7;" + menuCandidatesSearch.Tag);
        }
        private void menuClientsSearch_Click(object sender, EventArgs e)
        {
            OpenForm("1;8;" + menuClientsSearch.Tag);
        }
        private void menuContractsSearch_Click(object sender, EventArgs e)
        {
            OpenForm("1;9;" + menuContractsSearch.Tag);
        }
        private void menuBlackList_Click(object sender, EventArgs e)
        {
            OpenForm("1;15;" + menuBlackList.Tag);
        }
        private void menuOfficialInforming_Click(object sender, EventArgs e)
        {
            OpenForm("1;18;" + menuOfficialInforming.Tag);
        }        
        private void menuClientsRequests_Click(object sender, EventArgs e)
        {
            OpenForm("1;19;" + menuClientsRequests.Tag);
        }
        private void menuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void menuProductsList_Click(object sender, EventArgs e)
        {
            OpenForm("2;1;" + menuProductsList.Tag);
        }
        private void menuSelectedProductsList_Click(object sender, EventArgs e)
        {
            OpenForm("2;2;" + menuSelectedProductsList.Tag);
        }
        private void menuProductsAccordance_Click(object sender, EventArgs e)
        {
            OpenForm("2;3;" + menuProductsAccordance.Tag);
        }
        private void menuStandardPortfolios_Click(object sender, EventArgs e)
        {
            OpenForm("2;4;" + menuStandardPortfolios.Tag);
        }
        private void menuProductsPrices_Click(object sender, EventArgs e)
        {
            OpenForm("2;5;" + menuProductsPrices.Tag);
        }
        private void menuInvestmentCommittees_Click(object sender, EventArgs e)
        {
            OpenForm("2;6;" + menuInvestmentCommittees.Tag);
        }
        private void menuProductDataDownloader_Click(object sender, EventArgs e)
        {
            OpenForm("2;7;" + menuProductDataDownloader.Tag);
        }
        private void menuInvestProposals_Click(object sender, EventArgs e)
        {
            OpenForm("3;1;" + menuInvestProposals.Tag);
        }
        private void menuDPMOrdersList_Click(object sender, EventArgs e)
        {
            OpenForm("3;2;" + menuDPMOrdersList.Tag);
        }
        private void menuSecurities_Lists_Click(object sender, EventArgs e)
        {
            OpenForm("3;4;" + menuSecurities_Lists.Tag);
        }
        private void menuCommandsSearch_Click(object sender, EventArgs e)
        {
            OpenForm("3;5;" + menuCommandsSearch.Tag);
        }
        private void menuStatementsCheck_Click(object sender, EventArgs e)
        {
            OpenForm("3;6;" + menuStatementsCheck.Tag);
        }
        private void menuFX_List_Click(object sender, EventArgs e)
        {
            OpenForm("3;8;" + menuFX_List.Tag);
        }
        private void menuFX_Search_Click(object sender, EventArgs e)
        {
            OpenForm("3;9;" + menuFX_Search.Tag);
        }
        private void menuLL_List_Click(object sender, EventArgs e)
        {
            OpenForm("3;11;" + menuLL_List.Tag);
        }
        private void menuLL_Search_Click(object sender, EventArgs e)
        {
            OpenForm("3;12;" + menuLL_Search.Tag);
        }
        private void menuExecutionFiles_Click(object sender, EventArgs e)
        {
            OpenForm("4;1;" + menuExecutionFiles.Tag);
        }
        private void menuExecutionFilesNew_Click(object sender, EventArgs e)
        {
            OpenForm("4;3;" + menuExecutionFiles.Tag);
        }
        private void menuExecutionFilesFX_Click(object sender, EventArgs e)
        {
            OpenForm("4;2;" + menuExecutionFilesFX.Tag);
        }
        private void menuTrx_Kinisis_Click(object sender, EventArgs e)
        {
            OpenForm("4;4;" + menuTrx_Kinisis.Tag);
        }
        private void menuTrx_Parastatika_Click(object sender, EventArgs e)
        {
            OpenForm("4;6;" + menuTrx_Parastatika.Tag);
        }
        private void menuTrx_Epivarinsis_Click(object sender, EventArgs e)
        {
            OpenForm("4;7;" + menuTrx_Epivarinsis.Tag);
        }
        private void menuTrx_EpivarinsisKinisis_Click(object sender, EventArgs e)
        {
            OpenForm("4;8;" + menuTrx_EpivarinsisKinisis.Tag);
        }
        private void menuRpt_TRS_Click(object sender, EventArgs e)
        {
            OpenForm("5;1;" + menuRpt_TRS.Tag);
        }
        private void menuRpt_PeriodicalEvaluation_Click(object sender, EventArgs e)
        {
            OpenForm("5;2;" + menuRpt_PeriodicalEvaluation.Tag);
        }
        private void menuRpt_ExPostCost_Click(object sender, EventArgs e)
        {
            OpenForm("5;3;" + menuRpt_ExPostCost.Tag);
        }
        private void menuAcc_Contracts_Click(object sender, EventArgs e)
        {
            OpenForm("6;1;" + menuAcc_Contracts.Tag);
        }
        private void menuAcc_InvoicesRTO_Click(object sender, EventArgs e)
        {
            OpenForm("6;2;" + menuAcc_InvoicesRTO.Tag);
        }
        private void menuAcc_InvoicesFX_Click(object sender, EventArgs e)
        {
            OpenForm("6;3;" + menuAcc_InvoicesFX.Tag);
        }
        private void menuAcc_InvoicesMF_Click(object sender, EventArgs e)
        {
            OpenForm("6;4;" + menuAcc_InvoicesMF.Tag);
        }
        private void menuAcc_InvoicesAF_Click(object sender, EventArgs e)
        {
            OpenForm("6;5;" + menuAcc_InvoicesAF.Tag);
        }
        private void menuAcc_InvoicesCF_Click(object sender, EventArgs e)
        {
            OpenForm("6;6;" + menuAcc_InvoicesCF.Tag);
        }
        private void menuAcc_InvoicesPF_Click(object sender, EventArgs e)
        {
            OpenForm("6;7;" + menuAcc_InvoicesPF.Tag);
        }
        private void menuAcc_InvoicesRF_Click(object sender, EventArgs e)
        {
            OpenForm("6;8;" + menuAcc_InvoicesRF.Tag);
        }
        private void menuInvoicesControl_Click(object sender, EventArgs e)
        {
            OpenForm("6;9;" + menuInvoicesControl.Tag);
        }
        private void menuGenikoLogistikoSxedio_Click(object sender, EventArgs e)
        {
            OpenForm("6;10;" + menuGenikoLogistikoSxedio.Tag);
        }
        private void menuOpt_MiscLists_Click(object sender, EventArgs e)
        {
            OpenForm("7;1;" + menuOpt_MiscLists.Tag);
        }
        private void menuOpt_ProcurementCategories_Click(object sender, EventArgs e)
        {
            OpenForm("7;2;" + menuOpt_ProcurementCategories.Tag);
        }

        private void menuOpt_ServicesProviders_Click(object sender, EventArgs e)
        {
            OpenForm("7;3;" + menuOpt_ServicesProviders.Tag);
        }

        private void menuOpt_ServicesPackages_Click(object sender, EventArgs e)
        {
            OpenForm("7;4;" + menuOpt_ServicesPackages.Tag);
        }

        private void menuOpt_Settings_Click(object sender, EventArgs e)
        {
            OpenForm("7;6;" + menuOpt_Settings.Tag);
        }

        private void menuOpt_Alerts_Click(object sender, EventArgs e)
        {
            OpenForm("7;7;" + menuOpt_Alerts.Tag);
        }
        private void menuOpt_Users_Click(object sender, EventArgs e)
        {
            OpenForm("7;8;" + menuOpt_Users.Tag);
        }
        private void menuImportData_Click(object sender, EventArgs e)
        {
            OpenForm("8;1;" + menuImportData.Tag);
        }
        private void menuSystemServices_Click(object sender, EventArgs e)
        {
            OpenForm("8;7;" + menuSystemServices.Tag);
        }
        private void OpenForm(string sParams)
        {
            sParams = sParams + ";;;";
            string[] tokens = sParams.Split(';');

            switch (tokens[0])
            {
                case "1":                       // menu Contracts
                    Assembly cAssembly = Assembly.LoadFrom("Contracts.dll");
                    Type cType = cAssembly.GetType("Contracts.clsEntryPoint");
                    object cInstance = Activator.CreateInstance(cType, tokens[1] + ";" + tokens[2] + ";" + tokens[3]);
                    break;
                case "2":
                    Assembly pAssembly = Assembly.LoadFrom("Products.dll");
                    Type pType = pAssembly.GetType("Products.clsEntryPoint");
                    object pInstance = Activator.CreateInstance(pType, tokens[1] + ";" + tokens[2] + ";" + tokens[3]);
                    break;
                case "3":                      // menu Transactions
                    Assembly tAssembly = Assembly.LoadFrom("Transactions.dll");
                    Type tType = tAssembly.GetType("Transactions.clsEntryPoint");
                    object tInstance = Activator.CreateInstance(tType, tokens[1] + ";" + tokens[2] + ";" + tokens[3]);
                    break;
                case "4":                      // menu Custody Services 
                    Assembly qAssembly = Assembly.LoadFrom("Custody.dll");
                    Type qType = qAssembly.GetType("Custody.clsEntryPoint");
                    object qInstance = Activator.CreateInstance(qType, tokens[1] + ";" + tokens[2] + ";" + tokens[3]);
                    break;
                case "5":                       // menu Reports
                    Assembly rAssembly = Assembly.LoadFrom("Reports.dll");
                    Type rType = rAssembly.GetType("Reports.clsEntryPoint");
                    object rInstance = Activator.CreateInstance(rType, tokens[1] + ";" + tokens[2] + ";" + tokens[3]);
                    break;
                case "6":                       // menu Accounting
                    Assembly aAssembly = Assembly.LoadFrom("Accounting.dll");
                    Type aType = aAssembly.GetType("Accounting.clsEntryPoint");
                    object aInstance = Activator.CreateInstance(aType, tokens[1] + ";" + tokens[2] + ";" + tokens[3]);
                    break;
                case "7":                       // menu Options
                    Assembly oAssembly = Assembly.LoadFrom("Options.dll");
                    Type oType = oAssembly.GetType("Options.clsEntryPoint");
                    object oInstance = Activator.CreateInstance(oType, tokens[1] + ";" + tokens[2] + ";" + tokens[3]);
                    break;
                case "8":                      // menu Tools
                    Assembly zAssembly = Assembly.LoadFrom("Tools.dll");
                    Type zType = zAssembly.GetType("Tools.clsEntryPoint");
                    object zInstance = Activator.CreateInstance(zType, tokens[1] + ";" + tokens[2] + ";" + tokens[3]);
                    break;
            }
        }

    }
}
