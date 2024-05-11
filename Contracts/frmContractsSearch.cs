using C1.Win.C1FlexGrid;
using Core;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace Contracts
{
    public partial class frmContractsSearch : Form
    {
        int i, j, iOld_ID, iClient_ID, iRightsLevel;
        string sCode, sInvestPolicies, sAdvisors, sSpecials, sSurnameGreek, sSurnameEnglish, sRecipientName;
        string[] sMiFID = { "-", "Ιδιώτης Πελάτης", "Επαγγελματίας Πελάτης", "Επιλέξιμοι Αντισυμβαλλόμενοι" };
        string[] sPolitics = { "-", "Όχι", "Ναι" };
        string[] sRisk = { "-", "Υψηλός", "Μεσαίος", "Χαμηλός" };
        bool bCheckAdvisors, bFound;
        Global.ContractData stContractData;
        public frmContractsSearch()
        {
            InitializeComponent();

            panInvestProfiles.Left = 802;
            panInvestProfiles.Top = 40;

            panInvestPolicies.Left = 802;
            panInvestPolicies.Top = 64;

            panAdvisors.Left = 802;
            panAdvisors.Top = 88;

            panServices.Left = 802;
            panServices.Top = 112;

            panDatesIn.Visible = false;
            panDatesOut.Visible = false;

            panCols.Left = 12;
            panCols.Top = 200;

            ShowColumns();
        }

        private void frmContractsSearch_Load(object sender, EventArgs e)
        {
            cmbTypos.Items.Clear();

            ucCS.StartInit(700, 400, 220, 20, 1);
            ucCS.TextOfLabelChanged += new EventHandler(ucCS_TextChanged);
            ucCS.Filters = "Status > 0 And Contract_ID > 0";
            ucCS.ListType = 1;

            cmbTypos.Items.Add("Στοιχεία συμβάσεων");
            cmbTypos.Items.Add("Στοιχεία επικοινωνίας");
            cmbTypos.Items.Add("Cash Accounts List");
            cmbTypos.Items.Add("Στοιχεία συνδικαιούχων");
            cmbTypos.Items.Add("Προσωπικά στοιχεία");
            cmbTypos.Items.Add("Διαχείριση νομιμοποιητικών εγγράφων");
            cmbTypos.Items.Add("Στοιχεία Marketing");
            cmbTypos.SelectedIndex = 0;

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString(Global.GridStyle);
            fgList.DrawMode = DrawModeEnum.OwnerDraw;
            fgList.MouseDown += new MouseEventHandler(fgList_MouseDown);
        }
        protected override void OnResize(EventArgs e)
        {
            panCritiries.Width = this.Width - 30;
            btnSearch.Left = this.Width - 144;
            fgList.Width = this.Width - 30;
            fgList.Height = this.Height - 240;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            toolLeft.Visible = true;
            fgList.Visible = true;
            chkPrint.Visible = true;

            sInvestPolicies = ",";
            for (j = 1; j <= fgInvestPolicies.Rows.Count - 1; j++)
                if (Convert.ToBoolean(fgInvestPolicies[j, 0]))
                    sInvestPolicies = sInvestPolicies + fgInvestPolicies[j, 2] + ",";

            if (sInvestPolicies == ",") sInvestPolicies = "";

            sAdvisors = ",";
            bCheckAdvisors = false;
            for (j = 1; j <= fgAdvisors.Rows.Count - 1; j++)
                if (Convert.ToBoolean(fgAdvisors[j, 0]))
                {
                    sAdvisors = sAdvisors + fgAdvisors[j, 2] + ",";
                    bCheckAdvisors = true;
                }

            if (sAdvisors == ",") sAdvisors = "";

            fgList.Redraw = false;
            fgList.Rows.Count = 1;

            ShowColumns();

            Global.TranslateUserName(ucCS.txtContractTitle.Text, out sSurnameGreek, out sSurnameEnglish);

            clsContracts Contracts = new clsContracts();

            iOld_ID = -999;
            switch (Convert.ToInt32(cmbTypos.SelectedIndex))
            {
                case 0:
                    sCode = "~~~";
                    iOld_ID = -999;
                    i = 0;
                    if (chkActiveContracts.Checked)
                    {
                        Contracts.DateStart = dContractStatesDate.Value;
                        Contracts.DateFinish = dContractStatesDate.Value;
                    }
                    else
                    {
                        Contracts.DateStart = Convert.ToDateTime("1900/01/01");
                        Contracts.DateFinish = Convert.ToDateTime("2070/12/31");
                    }
                    Contracts.Client_ID = iClient_ID;
                    Contracts.ClientName = sSurnameGreek;
                    Contracts.ServiceProvider_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                    Contracts.Status = -1;
                    Contracts.ClientStatus = -1;
                    Contracts.GetList();
                    foreach (DataRow dtRow in Contracts.List.Rows)
                    {
                        if ((!chkMasters.Checked) || (chkMasters.Checked && (Convert.ToInt32(dtRow["IsMaster"]) == 1) && iOld_ID != Convert.ToInt32(dtRow["ID"])))
                        {
                            iOld_ID = Convert.ToInt32(dtRow["ID"]);

                            bFound = true;

                            if (chkDatesStart.Checked || chkDatesFinish.Checked)
                            {

                                if (chkDatesStart.Checked && (Convert.ToDateTime(dtRow["Pack_DateStart"]) < dFrom_Start.Value ||
                                                           Convert.ToDateTime(dtRow["Pack_DateStart"]) > dTo_Start.Value))
                                    bFound = false;

                                if (chkDatesFinish.Checked && (Convert.ToDateTime(dtRow["Pack_DateFinish"]) < dFrom_Finish.Value ||
                                                            Convert.ToDateTime(dtRow["Pack_DateFinish"]) > dTo_Finish.Value))
                                    bFound = false;
                            }
                            else
                            {
                                if (chkActiveContracts.Checked)
                                    if (Convert.ToDateTime(dtRow["Pack_DateStart"]) > dContractStatesDate.Value ||
                                        Convert.ToDateTime(dtRow["Pack_DateFinish"]) < dContractStatesDate.Value) bFound = false;
                                    else if (Convert.ToDateTime(dtRow["Pack_DateStart"]) > dContractStatesDate.Value) bFound = false;
                            }

                            if ((sInvestPolicies.Length == 0) || (sInvestPolicies.IndexOf("," + dtRow["InvestmentPolicy_ID"] + ",") >= 0))
                                if ((chkAktive.Checked && Convert.ToInt16(dtRow["Status"]) == 1) || !chkAktive.Checked)

                                    if (bFound)
                                    {
                                        i = i + 1;
                                        fgList.AddItem(false + "\t" + i + "\t" + dtRow["Clientname"] + "\t" + dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" +
                                                Convert.ToDateTime(dtRow["Pack_DateStart"]).ToString("dd/MM/yyyy") + "\t" + Convert.ToDateTime(dtRow["Pack_DateFinish"]).ToString("dd/MM/yyyy") + "\t" +
                                                dtRow["PackageTitle"] + " " + dtRow["PackageVersion"] + "\t" + dtRow["Service_Title"] + "\t" + dtRow["Client_Category"] + "\t" +
                                                sMiFID[Convert.ToInt32(dtRow["MiFIDCategory_ID"])] + "\t" + dtRow["ServiceProvider_Title"] + "\t" + dtRow["InvestmentProfile_Title"] + "\t" +
                                                dtRow["InvestmentPolicy_Title"] + "\t" + dtRow["AdvisorName"] + "\t" + dtRow["RMName"] + "\t" + dtRow["IntroName"] + "\t" + dtRow["DiaxName"] + "\t" +
                                                dtRow["IsMaster"] + "\t" + dtRow["MasterFullName"] + "\t" + dtRow["ContractEMail"] + "\t" + dtRow["ContractMobile"] + "\t" + dtRow["ContractTel"] + "\t" + dtRow["ContractFax"] + "\t" + dtRow["Address"] + "\t" + dtRow["Zip"] + "\t" + dtRow["City"] + "\t" +
                                                dtRow["Country_Title"] + "\t" + dtRow["Spec_Title"] + "\t" + dtRow["SpecialCategory"] + "\t" +
                                                sRisk[Convert.ToInt32(dtRow["Risk"])] + "\t" + dtRow["CountryCitizen_Title"] + "\t" + dtRow["CountryTaxes_Title"] + "\t" +
                                                dtRow["CountryHome_Title"] + "\t" + dtRow["Division_Title"] + "\t" + dtRow["ID"] + "\t" + dtRow["Client_ID"] + "\t" +
                                                dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"]);
                                    }
                        }
                    }

                    fgList.Cols[fgList.Cols.Count - 1].Visible = false;     // Contract_ConnectionMethod
                    break;

                case 1:

                    sCode = "~~~";
                    iOld_ID = -999;
                    i = 0;

                    Contracts.Client_ID = iClient_ID;
                    Contracts.ClientName = sSurnameGreek;
                    Contracts.ServiceProvider_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                    if (chkActiveContracts.Checked) Contracts.Status = 1;
                    else Contracts.Status = -1;
                    if (chkAktive.Checked) Contracts.ClientStatus = 1;
                    else Contracts.ClientStatus = -1;
                    Contracts.GetList();
                    foreach (DataRow dtRow in Contracts.List.Rows)
                    {
                        //--- анализ дат DateStart - это доп.условие, не включенное в запрос. Поэтому сделан кастомный фильтр
                        //--- поиск по множеству Investment Policies не включенный в запрос. Поэтому сделан кастомный фильтр
                        //--- поиск по множеству Advisors не включенный в запрос. Поэтому сделан кастомный фильтр
                        if ((!chkMasters.Checked) || (chkMasters.Checked && (Convert.ToInt32(dtRow["IsMaster"]) == 1) && iOld_ID != Convert.ToInt32(dtRow["ID"])))
                        {
                            bFound = false;
                            if ((sInvestPolicies.Length == 0) || (sInvestPolicies.IndexOf("," + dtRow["InvestmentPolicy_ID"] + ",") >= 0))
                                if ((chkAktive.Checked && Convert.ToInt16(dtRow["Status"]) == 1) || !chkAktive.Checked)
                                    if (iOld_ID != Convert.ToInt32(dtRow["ID"]))
                                    {
                                        iOld_ID = Convert.ToInt32(dtRow["ID"]);
                                        bFound = true;
                                    }

                            if (bFound)
                            {
                                i = i + 1;
                                fgList.AddItem(false + "\t" + i + "\t" + dtRow["Clientname"] + "\t" + dtRow["ContractTitle"] + "\t" +
                                               dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" + Convert.ToDateTime(dtRow["DateStart"]).ToString("dd/MM/yyyy") + "\t" +
                                               Convert.ToDateTime(dtRow["DateFinish"]).ToString("dd/MM/yyyy") + "\t" + dtRow["PackageTitle"] + "\t" +
                                               dtRow["Service_Title"] + "\t" + dtRow["Client_Category"] + "\t" + sMiFID[Convert.ToInt32(dtRow["MiFIDCategory_ID"])] + "\t" +
                                               dtRow["ServiceProvider_Title"] + "\t" + dtRow["InvestmentProfile_Title"] + "\t" + dtRow["InvestmentPolicy_Title"] + "\t" +
                                               dtRow["AdvisorName"] + "\t" + dtRow["RMName"] + "\t" + dtRow["IntroName"] + "\t" + dtRow["DiaxName"] + "\t" +
                                               dtRow["IsMaster"] + "\t" + dtRow["MasterFullName"] + "\t" + dtRow["ContractEMail"] + "\t" + dtRow["ContractMobile"] + "\t" + dtRow["ContractTel"] + "\t" +
                                               dtRow["ContractFax"] + "\t" + dtRow["Address"] + "\t" + dtRow["Zip"] + "\t" + dtRow["City"] + "\t" + dtRow["Country_Title"] + "\t" +
                                               dtRow["Spec_Title"] + "\t" + "" + "\t" + sRisk[Convert.ToInt32(dtRow["Risk"])] + "\t" + dtRow["CountryCitizen_Title"] + "\t" +
                                               dtRow["CountryTaxes_Title"] + "\t" + dtRow["CountryHome_Title"] + "\t" + dtRow["Division_Title"] + "\t" +
                                               dtRow["ID"] + "\t" + dtRow["Client_ID"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"]);
                            }
                        }
                    }
                    break;
                case 2:
                    break;
            }


            fgList.Redraw = true;
            this.Cursor = Cursors.Default;
        }
        private void ShowColumns()
        {
            for (i = 1; i <= fgCols.Rows.Count - 1; i++)
            {
                if (Convert.ToBoolean(fgCols[i, 2])) fgList.Cols[i + 1].Visible = true;
                else fgList.Cols[i + 1].Visible = false;
            }
        }
        private void tsbExcel_Click(object sender, EventArgs e)
        {
            int j, k, m;
            var EXL = new Microsoft.Office.Interop.Excel.Application();
            var oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            // System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US"]
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("el-GR");
            EXL.Workbooks.Add();
            EXL.ScreenUpdating = false;
            EXL.Caption = Global.AppTitle;
            EXL.Cells[1, 3].Value = "Αναζήτηση Συμβάσεων";

            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            k = 0;
            for (i = 0; i <= (fgList.Rows.Count - 1); i++)
                if (i == 0 || Convert.ToBoolean(fgList[i, 0]))
                {
                    k = k + 1;
                    m = 0;
                    for (j = 1; j < fgCols.Rows.Count - 1; j++)
                    {
                        if (Convert.ToBoolean(fgCols[j, 2]))
                        {
                            m = m + 1;
                            EXL.Cells[k + 2, m].Value = fgList[i, j + 1];
                        }

                    }
                }
            this.Cursor = Cursors.Default;

            EXL.ScreenUpdating = true;
            EXL.Visible = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
        }

        private void mnuClientData_Click(object sender, EventArgs e)
        {
            frmClientData locClientData = new frmClientData();
            locClientData.Client_ID = Convert.ToInt32(fgList[fgList.Row, "Client_ID"]);
            locClientData.Text = Global.GetLabel("customer_information");
            locClientData.Show();
        }

        private void mnuContractData_Click(object sender, EventArgs e)
        {
            frmContract locContract = new frmContract();
            locContract.Aktion = 1;
            locContract.Contract_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
            locContract.Contract_Details_ID = Convert.ToInt32(fgList[fgList.Row, "Contracts_Details_ID"]);
            locContract.Contract_Packages_ID = Convert.ToInt32(fgList[fgList.Row, "Contracts_Packages_ID"]);
            locContract.Client_ID = Convert.ToInt32(fgList[fgList.Row, "Client_ID"]);
            locContract.ClientType = 1;
            locContract.ClientFullName = fgList[fgList.Row, "ClientFullName"] + "";
            locContract.RightsLevel = iRightsLevel;
            locContract.ShowDialog();
        }
        private void tsbColsSetting_Click(object sender, EventArgs e)
        {
            panCols.Visible = true;
        }
        private void picClose_Cols_Click(object sender, EventArgs e)
        {
            ShowColumns();
            panCols.Visible = false;
        }
        private void chkDatesStart_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDatesStart.Checked)
            {
                panDatesIn.Visible = true;
                panActiveContract.Visible = false;
            }
            else
            {
                panDatesIn.Visible = false;
                if (!chkDatesFinish.Checked) panActiveContract.Visible = true;
            }
        }

        private void chkDatesFinish_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDatesFinish.Checked)
            {
                panDatesOut.Visible = true;
                panActiveContract.Visible = false;
            }
            else
            {
                panDatesOut.Visible = false;
                if (!chkDatesStart.Checked) panActiveContract.Visible = true;
            }
        }

        private void picEmptyName_Click(object sender, EventArgs e)
        {
            ucCS.ShowClientsList = false;
            ucCS.txtContractTitle.Text = "";
            ucCS.ShowClientsList = true;
            iClient_ID = 0;
        }

        private void cmbTypos_SelectedIndexChanged(object sender, EventArgs e)
        {
            fgCols.Redraw = false;
            fgCols.Rows.Count = 1;
            switch (cmbTypos.SelectedIndex)
            {
                case 0:
                    fgCols.AddItem("1\tΟνοματεπώνυμο\t1");
                    fgCols.AddItem("2\tΣύμβαση\t1");
                    fgCols.AddItem("3\tΚωδικός\t1");
                    fgCols.AddItem("4\tPortfolio\t1");
                    fgCols.AddItem("5\tΗμερ.Έναρξης\t1");
                    fgCols.AddItem("6\tΗμερ.Λήξης\t1");
                    fgCols.AddItem("7\tΠακέτο\t1");
                    fgCols.AddItem("8\tΥπηρεσία\t1");
                    fgCols.AddItem("9\tΚατηγορία προσώπου\t1");
                    fgCols.AddItem("10\tΚατηγορία MiFiD\t1");
                    fgCols.AddItem("11\tΠάροχος\t1");
                    fgCols.AddItem("12\tΕπενδ.Profile\t1");
                    fgCols.AddItem("13\tΕπενδ.πολιτική\t1");
                    fgCols.AddItem("14\tAdvisor\t1");
                    fgCols.AddItem("15\tRM\t1");
                    fgCols.AddItem("16\tIntroducer\t1");
                    fgCols.AddItem("17\tΔιαχειριστής\t1");
                    fgCols.AddItem("18\tMaster\t1");
                    fgCols.AddItem("19\tΟνοματεπώνυμο του Master\t1");
                    fgCols.AddItem("20\te-mail\t0");
                    fgCols.AddItem("21\tΚινητό\t0");
                    fgCols.AddItem("22\tΤηλέφωνο\t0");
                    fgCols.AddItem("23\tFax\t0");
                    fgCols.AddItem("24\tΔιεύθυνση\t0");
                    fgCols.AddItem("25\tΤΚ\t0");
                    fgCols.AddItem("26\tΠόλη\t0");
                    fgCols.AddItem("27\tΧώρα\t0");
                    fgCols.AddItem("28\tΕπάγγελμα\t0");
                    fgCols.AddItem("29\tΕιδική Κατηγορία\t0");
                    fgCols.AddItem("30\tΚίνδυνος AML\t1");
                    fgCols.AddItem("31\tΥπηκοότητα\t0");
                    fgCols.AddItem("32\tΧώρα φορολόγησης\t0");
                    fgCols.AddItem("33\tΧώρα κατοικίας\t0");
                    fgCols.AddItem("34\tΚατάστημα\t1");
                    break;
                case 1:
                    fgCols.AddItem("1\tΟνοματεπώνυμο\t1");
                    fgCols.AddItem("2\tΣύμβαση\t1");
                    fgCols.AddItem("3\tΚωδικός\t1");
                    fgCols.AddItem("4\tPortfolio\t1");
                    fgCols.AddItem("5\tΗμερ.Έναρξης\t0");
                    fgCols.AddItem("6\tΗμερ.Λήξης\t0");
                    fgCols.AddItem("7\tΠακέτο\t0");
                    fgCols.AddItem("8\tΥπηρεσία\t1");
                    fgCols.AddItem("9\tΚατηγορία προσώπου\t0");
                    fgCols.AddItem("10\tΚατηγορία MiFiD\t0");
                    fgCols.AddItem("11\tΠάροχος\t0");
                    fgCols.AddItem("12\tΕπενδ.Profile\t0");
                    fgCols.AddItem("13\tΕπενδ.πολιτική\t0");
                    fgCols.AddItem("14\tAdvisor\t0");
                    fgCols.AddItem("15\tRM\t0");
                    fgCols.AddItem("16\tIntroducer\t0");
                    fgCols.AddItem("17\tΔιαχειριστής\t0");
                    fgCols.AddItem("18\tMaster\t1");
                    fgCols.AddItem("19\tΟνοματεπώνυμο του Master\t1");
                    fgCols.AddItem("20\te-mail\t1");
                    fgCols.AddItem("21\tΚινητό\t1");
                    fgCols.AddItem("22\tΤηλέφωνο\t1");
                    fgCols.AddItem("23\tFax\t1");
                    fgCols.AddItem("24\tΔιεύθυνση\t1");
                    fgCols.AddItem("25\tΤΚ\t1");
                    fgCols.AddItem("26\tΠόλη\t1");
                    fgCols.AddItem("27\tΧώρα\t1");
                    fgCols.AddItem("28\tΕπάγγελμα\t1");
                    fgCols.AddItem("29\tΕιδική Κατηγορία\t0");
                    fgCols.AddItem("30\tΚίνδυνος AML\t1");
                    fgCols.AddItem("31\tΥπηκοότητα\t0");
                    fgCols.AddItem("32\tΧώρα φορολόγησης\t0");
                    fgCols.AddItem("33\tΧώρα κατοικίας\t0");
                    fgCols.AddItem("34\tΚατάστημα\t1");
                    break;
                case 2:
                    fgCols.AddItem("1\tΟνοματεπώνυμο\t1");
                    fgCols.AddItem("2\tΣύμβαση\t1");
                    fgCols.AddItem("3\tΚωδικός\t1");
                    fgCols.AddItem("4\tPortfolio\t1");
                    fgCols.AddItem("5\tΗμερ.Έναρξης\t1");
                    fgCols.AddItem("6\tΗμερ.Λήξης\t1");
                    fgCols.AddItem("7\tΠακέτο\t1");
                    fgCols.AddItem("8\tΥπηρεσία\t1");
                    fgCols.AddItem("9\tΚατηγορία προσώπου\t1");
                    fgCols.AddItem("10\tΚατηγορία MiFiD\t1");
                    fgCols.AddItem("11\tΠάροχος\t1");
                    fgCols.AddItem("12\tΕπενδ.Profile\t1");
                    fgCols.AddItem("13\tΕπενδ.πολιτική\t1");
                    fgCols.AddItem("14\tAdvisor\t1");
                    fgCols.AddItem("15\tRM\t1");
                    fgCols.AddItem("16\tIntroducer\t1");
                    fgCols.AddItem("17\tΔιαχειριστής\t1");
                    fgCols.AddItem("18\tMaster\t1");
                    fgCols.AddItem("19\tΟνοματεπώνυμο του Master\t1");
                    fgCols.AddItem("20\te-mail\t0");
                    fgCols.AddItem("21\tΚινητό\t0");
                    fgCols.AddItem("22\tΤηλέφωνο\t0");
                    fgCols.AddItem("23\tFax\t0");
                    fgCols.AddItem("24\tΔιεύθυνση\t0");
                    fgCols.AddItem("25\tΤΚ\t0");
                    fgCols.AddItem("26\tΠόλη\t0");
                    fgCols.AddItem("27\tΧώρα\t0");
                    fgCols.AddItem("28\tΕπάγγελμα\t0");
                    fgCols.AddItem("29\tΕιδική Κατηγορία\t0");
                    fgCols.AddItem("30\tΚίνδυνος AML\t1");
                    fgCols.AddItem("31\tΥπηκοότητα\t0");
                    fgCols.AddItem("32\tΧώρα φορολόγησης\t0");
                    fgCols.AddItem("33\tΧώρα κατοικίας\t0");
                    fgCols.AddItem("34\tΚατάστημα\t1");
                    break;
            }
            fgCols.Redraw = true;
        }

        private void chkPrint_CheckedChanged(object sender, EventArgs e)
        {
            for (i = 1; i <= fgList.Rows.Count - 1; i++) fgList[i, 0] = chkPrint.Checked;
        }

        private void fgList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                fgList.ContextMenuStrip = mnuContext;
                fgList.Row = fgList.MouseRow;
            }
        }
        //--- fgInvestProfiles functions ---------------------------------------------------------
        private void lnkProfiles_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panInvestProfiles.Visible = true;
        }
        private void picClose_InvestProfiles_Click(object sender, EventArgs e)
        {
            panInvestProfiles.Visible = false;
        }
        //--- fgInvestPolicies functions ---------------------------------------------------------
        private void lnkPolicies_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panInvestPolicies.Visible = true;
        }
        private void picClose_InvestPolicies_Click(object sender, EventArgs e)
        {
            panInvestPolicies.Visible = false;
        }
        //--- fgAdvisors functions ---------------------------------------------------------
        private void lnkAdvisors_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panAdvisors.Visible = true;
        }
        private void picClose_Advisors_Click(object sender, EventArgs e)
        {
            panAdvisors.Visible = false;
        }
        //--- fgServices functions ---------------------------------------------------------
        private void lnkServices_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panServices.Visible = true;
        }
        private void picClose_Services_Click(object sender, EventArgs e)
        {
            panServices.Visible = false;
        }


        protected void ucCS_TextChanged(object sender, EventArgs e)
        {
            stContractData = ucCS.SelectedContractData;
            //lnkPelatis.Text = stContractData.ClientName;
            //lblCode.Text = stContractData.Code;
            //lblProfitCenter.Text = stContractData.Portfolio;
            iClient_ID = stContractData.Client_ID;
        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
    }
}
