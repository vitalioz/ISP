using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Core;

namespace Accounting
{
    public partial class frmPortfolio_MonitoringOLD : Form
    {
        DataTable dtRecs;
        DataView dtView;
        DataRow dtRow;
        int i, j, iRightsLevel, iJobs, iOld_CP_ID;
        string sTemp, sExtra, sRisksFileName, sResults, sNotes, sClientFullName, sContractTitle, sCode, sSubcode, sService, sInvestPolice, sProvider;
        string[] sZtatus = {Global.GetLabel("new_registration"), Global.GetLabel("waiting"), Global.GetLabel("sent"), Global.GetLabel("no_sent") };
        Boolean bCheckList;
        DateTime dFrom, dTo;
        CellRange rng;
        CellStyle[] csZtatus = new CellStyle[4];
        CellStyle csYes, csNo, csOK, csProblem;
        clsContracts Contracts = new clsContracts();
        clsContracts_Monitoring Contracts_Monitoring = new clsContracts_Monitoring();
        public frmPortfolio_MonitoringOLD()
        {
            InitializeComponent();
        }

        private void frmPortfolio_AdvisoryMonitoring_Load(object sender, EventArgs e)
        {
            sRisksFileName = "ΚΙΝΔΥΝΟΙ ΑΠΟ ΕΠΕΝΔΥΣΕΙΣ ΣΕ ΧΡΗΜΑΤΟΠΙΣΤΩΤΙΚΑ ΜΕΣΑ.pdf";

            bCheckList = false;

            if (sExtra.Trim() == "1") cmbAdvisors.Enabled = true;
            else cmbAdvisors.Enabled = false;

            lblYear.Text = Global.GetLabel("year");
            lblMonth.Text = Global.GetLabel("month");
            lblAdvisor.Text = Global.GetLabel("advisor");
            lblFinanceService.Text = Global.GetLabel("service");
            lblInvestPolicies.Text = Global.GetLabel("investment_policy");
            btnSearch.Text = Global.GetLabel("search");

            btnSearch2.Text = Global.GetLabel("search");

            csZtatus[0] = fgList.Styles.Add("New");
            csZtatus[0].BackColor = Color.Transparent;

            csZtatus[1] = fgList.Styles.Add("WaitSend");
            csZtatus[1].BackColor = Color.Yellow;

            csZtatus[2] = fgList.Styles.Add("Sent");
            csZtatus[2].BackColor = Color.LightGreen;

            csZtatus[3] = fgList.Styles.Add("NotSent");
            csZtatus[3].BackColor = Color.LightCoral;

            iJobs = 6;           // Jobs Count - fgList, tmpArray, tmBrray depends of iJobs
            sResults = "";
            sNotes = "";
            for (i = 1; i <= iJobs - 1; i++) {
                sResults = sResults + "0~";
                sNotes = sNotes + "~";
            }
            sResults = sResults + "0";

            for (i = 2012; i <= DateTime.Now.Year ; i++)
            {
                cmbYear.Items.Add(i);
                cmbYearFrom.Items.Add(i);
                cmbYearTo.Items.Add(i);
            }
            cmbYear.SelectedIndex = cmbYear.Items.Count - 1;
            cmbYearFrom.SelectedIndex = cmbYear.Items.Count - 1;
            cmbYearTo.SelectedIndex = cmbYear.Items.Count - 1;

            cmbMonth.SelectedIndex = DateTime.Now.Month - 1;
            cmbMonthFrom.SelectedIndex = DateTime.Now.Month - 1;
            cmbMonthTo.SelectedIndex = DateTime.Now.Month - 1;


            //------- fgAttaches ----------------------------
            fgAttaches.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgAttaches.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgAttaches.Styles.Normal.WordWrap = true;
            Column col1 = fgAttaches.Cols[1];
            col1.Name = "Image";
            col1.DataType = typeof(String);
            col1.ComboList = "...";

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgList.Styles.Normal.WordWrap = true;
            fgList.DrawMode = DrawModeEnum.OwnerDraw;
            fgList.AutoResize = true;
            fgList.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgList.Cols[0].AllowMerging = true;
            rng = fgList.GetCellRange(0, 0, 1, 0);
            rng.Data = ".";

            fgList.Cols[1].AllowMerging = true;
            rng = fgList.GetCellRange(0, 1, 1, 1);
            rng.Data = Global.GetLabel("sn");

            fgList.Cols[2].AllowMerging = true;
            rng = fgList.GetCellRange(0, 2, 1, 2);
            rng.Data = Global.GetLabel("status");

            fgList.Cols[3].AllowMerging = true;
            rng = fgList.GetCellRange(0, 3, 1, 3);
            rng.Data = Global.GetLabel("customer_name");

            fgList.Cols[4].AllowMerging = true;
            rng = fgList.GetCellRange(0, 4, 1, 4);
            rng.Data = "Σύμβαση";

            fgList.Cols[5].AllowMerging = true;
            rng = fgList.GetCellRange(0, 5, 1, 5);
            rng.Data = Global.GetLabel("code");

            fgList.Cols[6].AllowMerging = true;
            rng = fgList.GetCellRange(0, 6, 1, 6);
            rng.Data = Global.GetLabel("subaccount");

            fgList.Cols[7].AllowMerging = true;
            rng = fgList.GetCellRange(0, 7, 1, 7);
            rng.Data = Global.GetLabel("provider");

            fgList.Cols[8].AllowMerging = true;
            rng = fgList.GetCellRange(0, 8, 1, 8);
            rng.Data = Global.GetLabel("service");

            fgList.Cols[9].AllowMerging = true;
            rng = fgList.GetCellRange(0, 9, 1, 9);
            rng.Data = Global.GetLabel("investment_policy");

            fgList.Cols[10].AllowMerging = true;
            rng = fgList.GetCellRange(0, 10, 1, 10);
            rng.Data = Global.GetLabel("month");

            fgList.Cols[11].AllowMerging = true;
            rng = fgList.GetCellRange(0, 11, 1, 11);
            rng.Data = Global.GetLabel("conventional_limits");

            fgList.Cols[12].AllowMerging = true;
            rng = fgList.GetCellRange(0, 12, 1, 12);
            rng.Data = Global.GetLabel("asset_allocation");

            fgList.Cols[13].AllowMerging = true;
            rng = fgList.GetCellRange(0, 13, 1, 13);
            rng.Data = Global.GetLabel("suggested_products");

            fgList.Cols[14].AllowMerging = true;
            rng = fgList.GetCellRange(0, 14, 1, 14);
            rng.Data = Global.GetLabel("weight_products_percent");

            fgList.Cols[15].AllowMerging = true;
            rng = fgList.GetCellRange(0, 15, 1, 15);
            rng.Data = Global.GetLabel("leverage");

            fgList.Cols[16].AllowMerging = true;
            rng = fgList.GetCellRange(0, 16, 1, 16);
            rng.Data = Global.GetLabel("debit_balance");

            csNo = fgList.Styles.Add("No");
            csNo.BackColor = Color.Transparent;

            csOK = fgList.Styles.Add("OK");
            csOK.BackColor = Color.LightGreen;

            csProblem = fgList.Styles.Add("Problem");
            csProblem.BackColor = Color.LightCoral;

            //----- initialize FINANCE SERVICES List -------
            dtView = Global.dtServices.Copy().DefaultView;
            dtView.RowFilter = "ID = 2 OR ID = 5";               // only Advisory & DialAdvisory
            cmbFinanceServices.DataSource = dtView;
            cmbFinanceServices.DisplayMember = "Title";
            cmbFinanceServices.ValueMember = "ID";
            cmbFinanceServices.SelectedValue = 0;

            //-------------- Define Advisors List ------------------   
            dtView = Global.dtUserList.Copy().DefaultView;
            dtView.RowFilter = "Chief = 1";
            cmbAdvisors.DataSource = dtView;
            cmbAdvisors.DisplayMember = "Title";
            cmbAdvisors.ValueMember = "ID";
            cmbAdvisors.SelectedValue = Global.User_ID;

            //----- initialize FINANCE SERVICES List -------
            dtView = Global.dtServices.Copy().DefaultView;
            dtView.RowFilter = "ID = 2 OR ID = 5";              // only Advisory & DialAdvisory
            cmbFinanceServices2.DataSource = dtView;
            cmbFinanceServices2.DisplayMember = "Title";
            cmbFinanceServices2.ValueMember = "ID";
            cmbFinanceServices2.SelectedValue = 0;

            //-------------- Define Advisors List ------------------   
            dtView = Global.dtUserList.Copy().DefaultView;
            dtView.RowFilter = "Chief = 1";
            cmbAdvisors2.DataSource = dtView;
            cmbAdvisors2.DisplayMember = "Title";
            cmbAdvisors2.ValueMember = "ID";
            cmbAdvisors2.SelectedValue = Global.User_ID;

            bCheckList = true;

            cmbFinanceServices.SelectedValue = 2;
        }
        protected override void OnResize(EventArgs e)
        {
            tabCrits.Width = this.Width - 30;
            btnSearch.Left = tabCrits.Width - 132;

            fgList.Width = this.Width - 30;
            fgList.Height = this.Height - 210;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DefineList(Convert.ToInt32(cmbYear.Text), (cmbMonth.SelectedIndex + 1), Convert.ToInt32(cmbYear.Text), (cmbMonth.SelectedIndex + 1), Convert.ToInt32(cmbAdvisors.SelectedValue),
                      Convert.ToInt32(cmbFinanceServices.SelectedValue), Convert.ToInt32(cmbInvestmentPolicy.SelectedValue));
        }
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            panCheck.Visible = true;
        }
        private void tsbSend_Click(object sender, EventArgs e)
        {

        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            panCheck.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panCheck.Visible = false;
        }
        private void DefineList(int iYearFrom, int iMonthFrom, int iYearTo, int iMonthTo, int iAdvisor_ID, int iService_ID, int iInvestmentPolicy_ID)
        {
            if (bCheckList)
            {

                //--- set DataTable Contracts_Balances columns --------------------------
                dtRecs = new DataTable("Recs");
                dtRecs.Columns.Add("ID", typeof(int));
                dtRecs.Columns.Add("CDP_ID", typeof(int));
                dtRecs.Columns.Add("Year", typeof(int));
                dtRecs.Columns.Add("Month", typeof(int));
                dtRecs.Columns.Add("Results", typeof(string));
                dtRecs.Columns.Add("Notes", typeof(string));
                dtRecs.Columns.Add("TotalNotes", typeof(string));
                dtRecs.Columns.Add("MainAttachFiles", typeof(string));
                dtRecs.Columns.Add("Status", typeof(int));
                dtRecs.Columns.Add("EMail", typeof(string));

                dFrom = Convert.ToDateTime("01/" + iMonthFrom + "/" + iYearFrom);

                if (iMonthTo < 12) dTo = Convert.ToDateTime("01/" + (iMonthTo + 1) + "/" + iYearTo);
                else dTo = Convert.ToDateTime("01/01/" + (iYearTo + 1));

                dTo = dTo.AddDays(-1);
                if (dTo.Date > DateTime.Now.Date) dTo = DateTime.Now.Date;


                fgList.Redraw = false;
                fgList.Rows.Count = 2;

                Contracts_Monitoring = new clsContracts_Monitoring();
                Contracts_Monitoring.YearFrom = iYearFrom;
                Contracts_Monitoring.MonthFrom = iMonthFrom;
                Contracts_Monitoring.YearTo = iYearTo;
                Contracts_Monitoring.MonthTo = iMonthTo;
                Contracts_Monitoring.Advisor_ID = iAdvisor_ID;
                Contracts_Monitoring.Service_ID = iService_ID;
                Contracts_Monitoring.GetList();

                foreach (DataRow dtRow1 in Contracts_Monitoring.List.Rows)
                {
                    dtRow = dtRecs.NewRow();
                    dtRow["ID"] = dtRow1["ID"];
                    dtRow["CDP_ID"] = dtRow1["CDP_ID"];
                    dtRow["Year"] = dtRow1["Year"];
                    dtRow["Month"] = dtRow1["Month"];
                    dtRow["Results"] = dtRow1["Results"];
                    dtRow["Notes"] = dtRow1["Notes"];
                    dtRow["TotalNotes"] = dtRow1["TotalNotes"];
                    dtRow["MainAttachFiles"] = dtRow1["StatementFile"] + "~" + dtRow1["AssetAllocationFile"] + "~" + dtRow1["RisksFile"] + "~" + dtRow1["MonitoringPDFile"];
                    dtRow["Status"] = dtRow1["Status"];
                    dtRow["EMail"] = dtRow1["EMail"];
                    dtRecs.Rows.Add(dtRow);
                };

                //-------------- Define Contracts List ------------------
                Contracts = new clsContracts();
                Contracts.PackageType = 0;
                Contracts.DateStart = dFrom;
                Contracts.DateFinish = dTo;
                Contracts.Client_ID = 0;
                Contracts.Advisor_ID = iAdvisor_ID;
                Contracts.Service_ID = iService_ID;
                Contracts.SurnameGreek = "%";
                Contracts.SurnameEnglish = "%";
                Contracts.ServiceProvider_ID = 0;
                Contracts.Division = 1;
                Contracts.DivisionFilter = 0;
                Contracts.Status = 1;
                Contracts.ClientStatus = -1;
                Contracts.GetList();
                foreach (DataRow dtRow1 in Contracts.List.Rows)
                {
                    if (Convert.ToInt32(dtRow1["Service_ID"]) == 2 || Convert.ToInt32(dtRow1["Service_ID"]) == 5)
                    {
                        iOld_CP_ID = -999;
                        sClientFullName = dtRow1["ClientName"] + " ";
                        sContractTitle = dtRow1["ContractTitle"] + "";
                        sCode = dtRow1["Code"] + "";
                        sSubcode = dtRow1["Portfolio"] + "";
                        sService = dtRow1["Service_Title"] + "";
                        sProvider = dtRow1["BrokerageServiceProvider_Title"] + "";
                        sInvestPolice = dtRow1["InvestmentPolicy_Title"] + "";

                        dtView = dtRecs.DefaultView;
                        dtView.RowFilter = "CDP_ID = " + dtRow1["CDP_ID"];
                        if (dtView.Count > 0)
                        {
                            foreach (DataRowView dtViewRow in dtView)
                            {

                                if (iOld_CP_ID != Convert.ToInt32(dtRow1["Contract_ID"])) iOld_CP_ID = Convert.ToInt32(dtRow1["Contract_ID"]);
                                else
                                {
                                    sClientFullName = "";
                                    sContractTitle = "";
                                    sCode = "";
                                    sSubcode = "";
                                    sService = "";
                                    sInvestPolice = "";
                                }

                                string[] tmpArray = (dtViewRow["Results"] + "").Split('~');
                                if (tmpArray.Length < iJobs)
                                {                      // in existing ClientsPackages_Monitoring record dtViewRow["Results") has less of iJobs elements
                                    sTemp = dtViewRow["Results"] + "";                // so add at end of dtViewRow["Results") missing elements
                                    for (i = tmpArray.Length + 1; i <= iJobs; i++)
                                        sTemp = sTemp + "~0";

                                    dtViewRow["Results"] = sTemp;
                                }

                                if (tmpArray.Length < iJobs)
                                {              // in existing ClientsPackages_Monitoring record dtViewRow["Notes") has less of iJobs elements
                                    sTemp = dtViewRow["Notes"] + "";               // so add at end of dtViewRow["Notes") missing elements
                                    for (i = tmpArray.Length + 1; i <= iJobs; i++)
                                        sTemp = sTemp + "~";

                                    dtViewRow["Notes"] = sTemp;
                                }


                                j = j + 1;
                                fgList.AddItem(false + "\t" + j + "\t" + sZtatus[Convert.ToInt32(dtViewRow["Status"])] + "\t" + sClientFullName + "\t" +
                                           sContractTitle + "\t" + sCode + "\t" + sSubcode + "\t" + sProvider + "\t" + sService + "\t" +
                                           sInvestPolice + "\t" + dtViewRow["Month"] + "/" + dtViewRow["Year"] + "\t" + "" + "\t" + "" + "\t" + "" +
                                           "\t" + "" + "\t" + "" + "\t" + "" + "\t" + dtRow1["Client_ID"] + "\t" + dtRow1["CDP_ID"] + "\t" +
                                           dtViewRow["ID"] + "\t" + dtRow1["User1_ID"] + "\t" + dtRow1["Service_ID"] + "\t" + dtRow1["CDP_ID"] + "~" +
                                           cmbYear.Text + "~" + (cmbMonth.SelectedIndex + 1) + "\t" + dtViewRow["Results"] + "\t" + dtViewRow["Notes"] + "\t" +
                                           dtViewRow["MainAttachFiles"] + "\t" + dtViewRow["TotalNotes"] + "\t" + dtRow1["InvestmentPolicy_ID"] + "\t" + dtViewRow["Status"] + "\t" + dtViewRow["EMail"]);
                            }
                        }
                        else
                        {
                            j = j + 1;
                            fgList.AddItem(false + "\t" + j + "\t" + sZtatus[0] + "\t" + sClientFullName + "\t" + dtRow1["ContractTitle"] + "\t" +
                                       dtRow1["Code"] + "\t" + dtRow1["Portfolio"] + "\t" + sProvider + "\t" + sService + "\t" +
                                       sInvestPolice + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" +
                                       "" + "\t" + dtRow1["Client_ID"] + "\t" + dtRow1["CDP_ID"] + "\t" + "0" + "\t" + dtRow1["User1_ID"] + "\t" +
                                       dtRow1["Service_ID"] + "\t" + dtRow1["CDP_ID"] + "~" + cmbYear.Text + "~" + (cmbMonth.SelectedIndex + 1) + "\t" +
                                       sResults + "\t" + sNotes + "\t" + "~~~" + "\t" + "" + "\t" + dtRow1["InvestmentPolicy_ID"] + "\t" + "0" + "\t" + dtRow1["EMail"]);
                        }
                    }
                };
                fgList.Redraw = true;
            }
        }    
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
    }
}
