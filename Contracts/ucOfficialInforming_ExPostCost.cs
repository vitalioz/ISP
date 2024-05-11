using C1.Win.C1FlexGrid;
using Core;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
namespace Contracts
{
    public partial class ucOfficialInforming_ExPostCost : UserControl
    {
        DataColumn dtCol;
        DataRow dtRow;
        DataTable dtInform;
        int i, iClient_ID, iEPC_ID;
        string sTemp, sRecipientName, sDate, sThema, sContractTitle, sConnectionMethod, sConnectionData, sAttachedFiles, sBody, sFileFullName;
        bool bCheckList;
        Global.ContractData stContractData;
        clsExPostCost_Title klsExPostCost_Titles = new clsExPostCost_Title();
        clsExPostCost_Recs klsExPostCost_Recs = new clsExPostCost_Recs();
        public ucOfficialInforming_ExPostCost()
        {
            InitializeComponent();
        }

        private void ucOfficialInforming_ExPostCost_Load(object sender, EventArgs e)
        {
            bCheckList = false;

            ucCS.StartInit(200, 400, 570, 20, 1);
            ucCS.TextOfLabelChanged += new EventHandler(ucCS_TextOfLabelChanged);
            ucCS.Filters = "Status = 1 AND CFP_Status = 1 AND PackageStatus = 1";
            ucCS.ListType = 1;

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString(Global.GridStyle);
            fgList.ShowCellLabels = true;
            fgList.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgList_BeforeEdit);
            fgList.DoubleClick += new System.EventHandler(fgList_DoubleClick);
            fgList.MouseDown += new MouseEventHandler(fgList_MouseDown);

            for (i = 2010; i <= DateTime.Now.Year; i++) cmbYear.Items.Add(i);
            cmbYear.SelectedItem = DateTime.Now.Year;

            bCheckList = true;
        }
        protected override void OnResize(EventArgs e)
        {
            btnSearch.Left = this.Width - 110;
            fgList.Width = this.Width - 20;
            fgList.Height = this.Height - 100;

            panEditData.Left = (Screen.PrimaryScreen.Bounds.Width - panEditData.Width) / 2;
            panEditData.Top = (Screen.PrimaryScreen.Bounds.Height - panEditData.Height) / 2;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            fgList.Visible = false;

            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            sTemp = cmbYear.Text;

            //---- Define PT_ID ---------------------
            klsExPostCost_Titles = new clsExPostCost_Title();
            klsExPostCost_Titles.EPC_Year = Convert.ToInt32(cmbYear.Text);
            klsExPostCost_Titles.GetRecord_Title();
            iEPC_ID = klsExPostCost_Titles.Record_ID;

            if (iEPC_ID != 0)
            {
                klsExPostCost_Recs.EPCT_ID = iEPC_ID;
                klsExPostCost_Recs.GetList();
                ShowList();
            }

            this.Cursor = Cursors.Default;

            fgList.Visible = true;
        }
        private void ShowList()
        {
            if (bCheckList)
            {
                fgList.Redraw = false;
                fgList.Rows.Count = 1;
                int i = 0;

                foreach (DataRow dtRow in klsExPostCost_Recs.List.Rows)
                {
                    if (lblCode.Text.Trim() == "" || dtRow["Code"].ToString().Contains(lblCode.Text))
                    {
                        i = i + 1;

                        sDate = "";
                        if ((dtRow["DateSent"] + "") != "")
                            sDate = Convert.ToDateTime(dtRow["DateSent"]).ToString("dd/MM/yy");

                        sConnectionMethod = "";
                        sConnectionData = "";
                        if (Convert.ToInt32(dtRow["ConnectionMethod_ID"]) == 1)
                        {
                            sConnectionMethod = "e-mail";
                            sConnectionData = dtRow["EMail"] + "";
                        }

                        sRecipientName = "";
                        if (Convert.ToInt32(dtRow["ContractTipos"]) == 0)                                             // Atomiki
                            sRecipientName = dtRow["User1_Name"].ToString().Trim();
                        else
                            sRecipientName = dtRow["BornPlace"].ToString();


                        if (Convert.ToInt32(dtRow["ConnectionMethod_ID"]) == 2)
                        {
                            sConnectionMethod = "Ταχ/κη αποστολή";
                            sConnectionData = sRecipientName + "\r\n" + dtRow["Address"] + "\r\n" +
                                              dtRow["City"] + " " + dtRow["ZIP"];
                            if (dtRow["Country_Title"] + "" != "Greece")
                                sConnectionData = sConnectionData + " " + dtRow["Country_Title"];
                        }

                        fgList.AddItem(false + "\t" + i + "\t" + dtRow["User1_Name"] + "\t" + dtRow["ContractTitle"] + "\t" + dtRow["ServiceProvider_Title"] + "\t" +
                                       dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" + dtRow["DateSent"] + "\t" + sConnectionMethod + "\t" + sConnectionData + "\t" +
                                       dtRow["FileName"] + "\t" + dtRow["ID"] + "\t" + dtRow["Client_ID"] + "\t" + dtRow["ConnectionMethod_ID"] + "\t" +
                                       dtRow["Contract_ID"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"]);
                    }
                }
                fgList.Redraw = true;
            }
        }
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            EditData();
        }
        private void fgList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                fgList.ContextMenuStrip = mnuContext;
                fgList.Row = fgList.MouseRow;
            }
        }

        private void picAttachedInvoice_Click_1(object sender, EventArgs e)
        {
            sFileFullName = Global.FileChoice(Global.DefaultFolder);
            if (sFileFullName.Length > 0) txtInvoice.Text = Path.GetFileName(sFileFullName);
            else txtInvoice.Text = "";
        }
        private void fgList_BeforeEdit(object sender, RowColEventArgs e)
        {

        }
        private void fgList_DoubleClick(object sender, EventArgs e)
        {
            EditData();
        }
        private void EditData()
        {
            if (Convert.ToInt32(fgList.Row) > 0)
            {
                lblInformingDate.Text = fgList[fgList.Row, "DateSent"] + "";
                lblInformingMethod.Text = fgList[fgList.Row, "ConnectionMethod"] + "";
                lblInformingClientData.Text = fgList[fgList.Row, "Client_Data"] + "";
                txtInvoice.Text = fgList[fgList.Row, "FileName"] + "";
                panEditData.Visible = true;
                btnCancel.Focus();
            }
        }
        private void chkList_CheckedChanged(object sender, EventArgs e)
        {
            for (i = 1; i <= fgList.Rows.Count - 1; i++) fgList[i, 0] = chkList.Checked;
        }
        private void mnuContractData_Click(object sender, EventArgs e)
        {
            frmContract locContract = new frmContract();
            locContract.Aktion = 1;
            locContract.Contract_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_ID"]);
            locContract.Contract_Details_ID = Convert.ToInt32(fgList[fgList.Row, "Contracts_Details_ID"]);
            locContract.Contract_Packages_ID = Convert.ToInt32(fgList[fgList.Row, "Contracts_Packages_ID"]);
            locContract.Client_ID = Convert.ToInt32(fgList[fgList.Row, "Client_ID"]);
            locContract.ClientType = Convert.ToInt32(fgList[fgList.Row, "ClientTipos"]);
            locContract.ClientFullName = fgList[fgList.Row, "ClientName"] + "";
            locContract.RightsLevel = 1;                                                                                     //iRightsLevel
            locContract.ShowDialog();
        }
        private void mnuClientData_Click(object sender, EventArgs e)
        {
            frmClientData locClientData = new frmClientData();
            locClientData.Client_ID = Convert.ToInt32(fgList[fgList.Row, "Client_ID"]);
            locClientData.Text = Global.GetLabel("customer_information");
            locClientData.Show();
        }
        private void mnuViewInvoice_Click(object sender, EventArgs e)
        {
            sTemp = fgList[fgList.Row, "ContractTitle"] + "";
            if (sTemp.Length > 0) Global.DMS_ShowFile("Customers/" + sTemp.Replace(".", "_") + "/Informing", fgList[fgList.Row, "FileName"].ToString());
        }
        private void tsbSend_Click(object sender, EventArgs e)
        {
            int iRec_ID = 0;
            sThema = "Eκ των υστέρων πληροφόρηση κόστους (Ex-post cost report)";

            for (i = 1; i <= fgList.Rows.Count - 1; i++)
            {
                if (Convert.ToBoolean(fgList[i, 0]))
                {
                    sContractTitle = fgList[i, "ContractTitle"] + "";
                    sContractTitle = sContractTitle.Replace(".", "_");

                    sAttachedFiles = "";
                    if (Convert.ToInt32(fgList[i, "ConnectionMethod_ID"]) == 1)                                     // 1 - e-mail
                    {
                        sBody = ExPostCostEmailBody();
                        sAttachedFiles = fgList[i, "FileName"] + "~";
                        iRec_ID = Global.AddInformingRecord(0, 0, 5, 9, Convert.ToInt32(fgList[i, "Client_ID"]), Convert.ToInt32(fgList[i, "Contract_ID"]), fgList[i, "Client_Data"] + "",
                                                  "", sThema, sBody, "", sAttachedFiles, "", 0, 0, "");                                                     // 5 - e-mail  

                        clsServerJobs ServerJob = new clsServerJobs();
                        ServerJob.JobType_ID = 43;                                           // 43  - send e-mail from Informings table
                        ServerJob.Source_ID = 0;
                        ServerJob.Parameters = "{'informing_id': '" + iRec_ID + "'}";
                        ServerJob.DateStart = DateTime.Now;
                        ServerJob.DateFinish = Convert.ToDateTime("1900/01/01");
                        ServerJob.PubKey = "";
                        ServerJob.PrvKey = "";
                        ServerJob.Attempt = 0;
                        ServerJob.Status = 0;
                        ServerJob.InsertRecord();

                        fgList[i, "DateSent"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        klsExPostCost_Recs = new clsExPostCost_Recs();
                        klsExPostCost_Recs.Record_ID = Convert.ToInt32(fgList[i, "ID"]);
                        klsExPostCost_Recs.GetRecord();
                        klsExPostCost_Recs.DateSent = fgList[i, "DateSent"] + "";
                        klsExPostCost_Recs.EditRecord();
                    }
                    if (Convert.ToInt32(fgList[i, "ConnectionMethod_ID"]) == 2)                  // 2 - post
                    {
                        dtInform = new DataTable("OfficialInforming");
                        dtCol = dtInform.Columns.Add("f1", System.Type.GetType("System.String"));
                        dtCol = dtInform.Columns.Add("f2", System.Type.GetType("System.String"));
                        dtCol = dtInform.Columns.Add("f3", System.Type.GetType("System.String"));
                        dtCol = dtInform.Columns.Add("f4", System.Type.GetType("System.String"));
                        dtCol = dtInform.Columns.Add("f5", System.Type.GetType("System.String"));
                        dtCol = dtInform.Columns.Add("f6", System.Type.GetType("System.String"));
                        dtCol = dtInform.Columns.Add("f7", System.Type.GetType("System.String"));

                        dtRow = dtInform.NewRow();
                        sTemp = fgList[i, "Client_Data"] + "";
                        dtRow["f1"] = sTemp.Replace("\t", "\n");
                        dtRow["f2"] = "";
                        dtRow["f3"] = "";
                        dtRow["f4"] = "ΘΕΣΣΑΛΟΝΙΚΗ " + DateTime.Now.ToString("dd/MM/yyyy");
                        dtRow["f5"] = "ΘΕΜΑ: Eκ των υστέρων πληροφόρηση κόστους (Ex-post cost report) " + cmbYear.Text;
                        sTemp = "Στο πλαίσιο που ορίζει ο νόμος 4514/2018, σας αποστέλλουμε την «Εκ των υστέρων πληροφόρηση κόστους (Ex-post cost report)», σχετικά με όλες τις κατηγορίες κόστους που επήλθαν στο χαρτοφυλάκιό σας κατά το προηγούμενο ημερολογιακό έτος.\n\n";
                        dtRow["f6"] = sTemp + "\n\n" + "Στη διάθεσή σας για οποιαδήποτε διευκρίνιση.";
                        dtInform.Rows.Add(dtRow);

                        ReportDocument rptOfficialInforming = new ReportDocument();
                        sTemp = Application.StartupPath + @"\Reports\repOfficialInforming.rpt";
                        rptOfficialInforming.Load(sTemp);
                        rptOfficialInforming.Database.Tables[0].SetDataSource(dtInform);
                        rptOfficialInforming.PrintToPrinter(1, true, 1, 999);

                        /*
                        frmReports locReports = new frmReports();
                        locReports.ReportID = 19;
                        locReports.Params = sTemp;
                        locReports.ShowResult = dtInform;
                        locReports.Text = "Επίσημη Ενημέρωση Πελατών";
                        locReports.Show();
                        if ((fgList[i, "FileName"] + "") != "")
                        {
                            sTemp = fgList[i, "ContractTitle"] + "";
                            Global.DMS_PrintFile("Customers/" + sTemp.Replace(".", "_") + "/Informing".ToString(), fgList[i, "FileName"].ToString());
                        }
                        sTemp = fgList[i, "ContractTitle"] + "";
                        */

                        sAttachedFiles = fgList[i, "FileName"] + "~";
                        Global.AddInformingRecord(0, 0, 8, 9, Convert.ToInt32(fgList[i, "Client_ID"]), Convert.ToInt32(fgList[i, "Contract_ID"]), fgList[i, "Client_Data"] + "",
                                                  "backoffice@hellasfin.gr", sThema, "", "", sAttachedFiles, "", 1, 1, "");                                              // 8 - post 

                        fgList[i, "DateSent"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        klsExPostCost_Recs = new clsExPostCost_Recs();
                        klsExPostCost_Recs.Record_ID = Convert.ToInt32(fgList[i, "ID"]);
                        klsExPostCost_Recs.GetRecord();
                        klsExPostCost_Recs.DateSent = fgList[i, "DateSent"] + "";
                        klsExPostCost_Recs.EditRecord();
                    }

                    clsExPostCost_Recs ExPostCost_Recs = new clsExPostCost_Recs();
                    ExPostCost_Recs.Record_ID = Convert.ToInt32(fgList[i, "ID"]);
                    ExPostCost_Recs.GetRecord();
                    ExPostCost_Recs.DateSent = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    ExPostCost_Recs.EditRecord();

                    fgList[i, "DateSent"] = DateTime.Now.ToString("dd/MM/yy");
                    fgList[i, 0] = false;
                }
            }
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            dtInform = new DataTable("OfficialInformingList");
            dtCol = dtInform.Columns.Add("f1", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f2", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f3", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f4", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f5", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f6", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f7", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f8", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f9", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f10", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f11", System.Type.GetType("System.String"));

            for (i = 1; i <= fgList.Rows.Count - 1; i++)
            {
                dtRow = dtInform.NewRow();
                dtRow["f1"] = fgList[i, "AA"];
                dtRow["f2"] = "01/01/" + cmbYear.Text;
                dtRow["f3"] = "31/12/" + cmbYear.Text;
                dtRow["f4"] = fgList[i, "ContractTitle"];
                dtRow["f5"] = fgList[i, "Code"];
                dtRow["f6"] = fgList[i, "Portfolio"];
                dtRow["f7"] = "";
                dtRow["f8"] = fgList[i, "DateSent"];
                dtRow["f9"] = fgList[i, "ConnectionMethod"];
                dtRow["f10"] = fgList[i, "Client_Data"];
                dtRow["f11"] = fgList[i, "FileName"];
                dtInform.Rows.Add(dtRow);
            }

            frmReports locReports = new frmReports();
            locReports.Params = "" + "~" + cmbYear.Text + "~" + Global.UserName + "~" + Global.CompanyName + "~" + "Επίσημη Ενημέρωση Πελατών ExPostCost" + "~";

            locReports.ReportID = 22;
            locReports.ShowResult = dtInform;
            locReports.Text = "Επίσημη Ενημέρωση Πελατών";
            locReports.Show();
        }
        private void picAttachedInvoice_Click(object sender, EventArgs e)
        {
            sFileFullName = Global.FileChoice(Global.DefaultFolder);
            if (sFileFullName.Length > 0) txtInvoice.Text = Path.GetFileName(sFileFullName);
            else txtInvoice.Text = "";
        }

        private void picShowInvoice_Click(object sender, EventArgs e)
        {
            sTemp = (fgList[fgList.Row, "ContractTitle"] + "").Trim();
            if (sTemp.Length > 0 && txtInvoice.Text.Length > 0)
                Global.DMS_ShowFile("Customers/" + sTemp.Replace(".", "_") + "/Informing", txtInvoice.Text);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtInvoice.Text.Length > 0)
            {
                sTemp = (fgList[fgList.Row, "ContractTitle"] + "").Trim();
                txtInvoice.Text = Global.DMS_UploadFile(sFileFullName, "Customers/" + sTemp.Replace(".", "_") + "/Informing", txtInvoice.Text);
                txtInvoice.Text = Path.GetFileName(txtInvoice.Text);
            }
            fgList[fgList.Row, "FileName"] = txtInvoice.Text;

            panEditData.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panEditData.Visible = false;
        }
        private string ExPostCostEmailBody()
        {
            string sBody;

            sBody = "<html xmlns='http://www.w3.org/1999/xhtml'><head><title></title><meta http-equiv='Content-Type' content='text/html; charset=utf-8' />" +
            "<style>img.logo {height: 60%;width: 40%;}</style></head><body style='width: 800px;'><br/><br/><table><tr><td width=800>" +
            "<div style='height: 150px;'><img class='logo' src='http://www.hellasfin.gr/signs/images/Logo_500px.jpg' alt='' /></div><br/><br/>" +
            "Δ/ΝΣΗ<br/>ΕΝΗΜΕΡΩΣΗΣ + ΕΞΥΠΗΡΕΤΗΣΗΣ ΕΠΕΝΔΥΤΩΝ <br/><br/><br/><br/>" +
            "<div align='right'>ΘΕΣΣΑΛΟΝΙΚΗ " + DateTime.Now.ToString("dd/MM/yyyy") + "</div><br/><br/><br/><br/><br/><br/>" +
            "<center> ΘΕΜΑ: Eκ των υστέρων πληροφόρηση κόστους (Ex-post cost report) " + cmbYear.Text + "</center>" + "<br/><br/><br/><br/>" +
            "Αγαπητέ πελάτη,<br/><br/>" +
            "Στο πλαίσιο που ορίζει ο νόμος 4514/2018, σας αποστέλλουμε την «Εκ των υστέρων πληροφόρηση κόστους (Ex-post cost report)», σχετικά με όλες τις κατηγορίες κόστους που επήλθαν στο χαρτοφυλάκιό σας κατά το προηγούμενο ημερολογιακό έτος. <br/><br/><br/>" +
            "Στη διάθεσή σας για οποιαδήποτε διευκρίνιση." + "<br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>" +
            "Διεύθυνση Λειτουργικής Υποστήριξης και Εξυπηρέτησης πελατών<br/><br/><br/><br/><br/><br/><br/><br/>" +
            "</td></tr></table><br/><br/>" +
            "</body></html>";

            return sBody;
        }
        protected void ucCS_TextOfLabelChanged(object sender, EventArgs e)
        {
            stContractData = ucCS.SelectedContractData;
            lnkPelatis.Text = stContractData.ClientName;
            lblCode.Text = stContractData.Code;
            //lblProfitCenter.Text = stContractData.Portfolio;
            iClient_ID = stContractData.Client_ID;
        }
    }
}
