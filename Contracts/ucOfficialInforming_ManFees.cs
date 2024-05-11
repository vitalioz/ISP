using C1.Win.C1FlexGrid;
using Core;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Contracts
{
    public partial class ucOfficialInforming_ManFees : UserControl
    {
        DataColumn dtCol;
        DataRow dtRow;
        DataTable dtInform;
        int i, j, iClient_ID, iFT_ID, iMF_Quart;
        string sTemp, sPeriod, sDate, sThema, sContractTitle, sRecipientName, sConnectionMethod, sConnectionData, sAttachedFiles, sOldCode, sBody, sFile1, sFile2, sFile3;
        Global.ContractData stContractData;
        public ucOfficialInforming_ManFees()
        {
            InitializeComponent();

            panFolders.Left = 42;
            panFolders.Top = 104;
        }

        private void ucOfficialInforming_ManFees_Load(object sender, EventArgs e)
        {
            ucCS.StartInit(200, 400, 570, 20, 1);
            ucCS.TextOfLabelChanged += new EventHandler(ucCS_TextOfLabelChanged);
            ucCS.Filters = "Status = 1 And Contract_ID > 0";
            ucCS.ListType = 1;

            cmbProviders.DataSource = Global.dtServiceProviders.Copy();
            cmbProviders.DisplayMember = "Title";
            cmbProviders.ValueMember = "ID";
            cmbProviders.SelectedItem = 1;

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString(Global.GridStyle);
            fgList.ShowCellLabels = true;
            fgList.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgList_BeforeEdit);
            fgList.DoubleClick += new System.EventHandler(fgList_DoubleClick);
            fgList.MouseDown += new MouseEventHandler(fgList_MouseDown);

            for (i = 2010; i <= DateTime.Now.Year; i++) cmbYear.Items.Add(i);

            i = Convert.ToInt16((DateTime.Now.Month + 2) / 3);
            if (i == 1)
            {
                i = 4;
                cmbYear.SelectedIndex = cmbYear.Items.Count - 2;
            }
            else
            {
                i = i - 1;
                cmbYear.SelectedIndex = cmbYear.Items.Count - 1;
            }

            switch (i)
            {
                case 1:
                    rb1.Checked = true;
                    break;
                case 2:
                    rb2.Checked = true;
                    break;
                case 3:
                    rb3.Checked = true;
                    break;
                case 4:
                    rb4.Checked = true;
                    break;
            }
        }
        protected override void OnResize(EventArgs e)
        {
            btnSearch.Left = this.Width - 110;
            fgList.Width = this.Width - 20;
            fgList.Height = this.Height - 100;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DefineCommandsList();
        }

        private void picEmptyClient_Click(object sender, EventArgs e)
        {
            iClient_ID = 0;
            EmptyContractData();
            ucCS.ShowClientsList = false;
            ucCS.txtContractTitle.Text = "";
            ucCS.ShowClientsList = true;
            lblCode.Text = "";
            lnkPelatis.Text = "";
        }
        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            DefineCommandsList();
        }
        private void DefineCommandsList()
        {
            iMF_Quart = 0;
            sPeriod = "";
            if (Convert.ToInt32(cmbProviders.SelectedValue) != 0)
            {
                sTemp = cmbYear.Text;
                if (rb1.Checked)
                {
                    iMF_Quart = 1;
                    sPeriod = "31/12/" + (Convert.ToInt16(cmbYear.Text) - 1) + " - " + "31/03/" + sTemp;
                }
                else
                {
                    if (rb2.Checked)
                    {
                        iMF_Quart = 2;
                        sPeriod = "31/03/" + sTemp + " - " + "30/06/" + sTemp;
                    }
                    else
                    {
                        if (rb3.Checked)
                        {
                            iMF_Quart = 3;
                            sPeriod = "30/06/" + sTemp + " - " + "30/09/" + sTemp;
                        }
                        else
                        {
                            if (rb4.Checked)
                            {
                                iMF_Quart = 4;
                                sPeriod = "30/09/" + sTemp + " - " + "31/12/" + sTemp;
                            }
                        }
                    }
                }

                i = 0;
                clsManagmentFees_Titles klsManagmentFees_Title = new clsManagmentFees_Titles();
                klsManagmentFees_Title.SC_ID = Convert.ToInt32(cmbProviders.SelectedValue);
                klsManagmentFees_Title.MF_Year = Convert.ToInt32(cmbYear.Text);
                klsManagmentFees_Title.MF_Quart = iMF_Quart;
                klsManagmentFees_Title.GetRecord_Title();
                iFT_ID = klsManagmentFees_Title.Record_ID;

                fgList.Redraw = false;
                fgList.Rows.Count = 1;
                //-------------- Define ManagmentFees_Recs List ------------------
                clsManagmentFees_Recs ManagmentFees_Recs = new clsManagmentFees_Recs();
                ManagmentFees_Recs.FT_ID = iFT_ID;
                ManagmentFees_Recs.GetList();
                foreach (DataRow dtRow in ManagmentFees_Recs.List.Rows)
                {
                    if (lblCode.Text == "" || lblCode.Text == (dtRow["Code"] + ""))
                    {
                        sDate = "";
                        if ((dtRow["OfficialInformingDate"] + "") != "") sDate = Convert.ToDateTime(dtRow["OfficialInformingDate"]).ToString("dd/MM/yy");


                        sConnectionMethod = "";
                        sConnectionData = "";
                        if (Convert.ToInt32(dtRow["ConnectionMethod"]) == 1)
                        {
                            sConnectionMethod = "e-mail";
                            sConnectionData = dtRow["EMail"] + "";
                        }

                        sContractTitle = "";
                        sRecipientName = "";
                        if (Convert.ToInt32(dtRow["ContractTipos"]) == 0)                             // 0 - Atomiki
                            sRecipientName = dtRow["User1_Name"] + "";
                        else
                            sRecipientName = dtRow["BornPlace"] + "";

                        sContractTitle = dtRow["ContractTitle"] + "";

                        if (Convert.ToInt16(dtRow["ConnectionMethod"]) == 2)
                        {
                            sConnectionMethod = "Ταχ/κη αποστολή";
                            sConnectionData = sRecipientName + "\r\n" + dtRow["Address"] + "\r\n" + dtRow["City"] + " " + dtRow["ZIP"];
                            if ((dtRow["Country_Title"] + "") != "Greece")
                                sConnectionData = sConnectionData + " " + dtRow["Country_Title"];
                        }
                        sTemp = "";
                        if ((dtRow["DateFees"] + "") != "") sTemp = Convert.ToDateTime(dtRow["DateFees"]).ToString("dd/MM/yyyy");

                        i = i + 1;
                        fgList.AddItem(false + "\t" + i + "\t" + dtRow["DateFrom"] + "\t" + dtRow["DateTo"] + "\t" + sContractTitle + "\t" +
                                      dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" + dtRow["Package_Title"] + "\t" +
                                      dtRow["FinishAmount"] + "\t" + dtRow["Invoice_Num"] + "\t" + sTemp + "\t" +
                                      sDate + "\t" + sConnectionMethod + "\t" + sConnectionData + "\t" + dtRow["Invoice_File"] + "\t" + dtRow["Statement_File"] + "\t" +
                                      dtRow["Misc_File"] + "\t" + dtRow["ID"] + "\t" + dtRow["Client_ID"] + "\t" + dtRow["Code"] + "_" + dtRow["Portfolio"] + "\t" +
                                      dtRow["ConnectionMethod"] + "\t" + dtRow["ClientType"] + "\t" + dtRow["Package_ID"] + "\t" + dtRow["Advisor_Email"] + "\t" +
                                      dtRow["Contract_ID"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"]);
                    }

                }
                fgList.Redraw = true;
                toolManFees.Visible = true;
            }
            else MessageBox.Show("Επιλέξτε των Πάροχο", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void fgList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                fgList.ContextMenuStrip = mnuContext;
                fgList.Row = fgList.MouseRow;
            }
        }
        private void fgList_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (e.Col == 0) e.Cancel = false;
            else e.Cancel = true;
        }
        protected void ucCS_TextOfLabelChanged(object sender, EventArgs e)
        {
            stContractData = ucCS.SelectedContractData;
            lnkPelatis.Text = stContractData.ClientName;
            lblCode.Text = stContractData.Code;
            //lblProfitCenter.Text = stContractData.Portfolio;
            iClient_ID = stContractData.Client_ID;
        }
        private void tsbEditProposal_ManFees_Click(object sender, EventArgs e)
        {
            EditCommandsInformingData_ManFees();
        }
        private void fgList_DoubleClick(object sender, EventArgs e)
        {
            EditCommandsInformingData_ManFees();
        }
        private void tsbDownload_Click(object sender, EventArgs e)
        {
            panFolders.Visible = true;
        }

        private void tsbSend_Click(object sender, EventArgs e)
        {
            int iRec_ID = 0;
            int iInvoiceType, iStatement;

            sOldCode = "~~~";
            sThema = "ΕΝΗΜΕΡΩΣΗ ΤΡΙΜΗΝΟΥ " + sPeriod;

            for (i = 1; i <= fgList.Rows.Count - 1; i++)
            {
                if (Convert.ToBoolean(fgList[i, 0]))
                {
                    sContractTitle = fgList[i, "ContractTitle"] + "";
                    sContractTitle = sContractTitle.Replace(".", "_");

                    sAttachedFiles = "";
                    if ((fgList[i, "Statement_File"] + "") != "") sAttachedFiles = sAttachedFiles + fgList[i, "Statement_File"] + "~";
                    if ((fgList[i, "Misc_File"] + "") != "") sAttachedFiles = sAttachedFiles + fgList[i, "Misc_File"] + "~";

                    if (Convert.ToInt32(fgList[i, "ConnectionMethod"]) == 1)                                     // 1 - e-mail
                    {
                        iInvoiceType = 0;
                        if ((fgList[i, "Invoice_File"] + "") != "") iInvoiceType = (Convert.ToInt32(fgList[i, "ClientTipos"]) == 2 ? 2 : 1);

                        iStatement = 0;
                        if ((fgList[i, "Statement_File"] + "") != "") iStatement = 1;

                        sBody = ManFeesEmailBody(iInvoiceType, iStatement);

                        if (sOldCode != (fgList[i, "Code"] + ""))                     // if it's a new code write into Informings table record that will be send
                        {
                            sOldCode = fgList[i, "Code"] + "";
                            iRec_ID = Global.AddInformingRecord(0, Convert.ToInt32(fgList[i, "ID"]), 5, 3, Convert.ToInt32(fgList[i, "Client_ID"]), Convert.ToInt32(fgList[i, "Contract_ID"]), fgList[i, "ClientData"] + "",
                                               "", sThema, sBody, fgList[i, "Invoice_File"] + "", sAttachedFiles, "", 0, 0, "");                        // 5 - e-mail 
                        }
                        else                                             // if it's an old code write into Informings table record that will not be send - last 3 parameters say that this record was sent
                        {
                            iRec_ID = Global.AddInformingRecord(0, Convert.ToInt32(fgList[i, "ID"]), 5, 3, Convert.ToInt32(fgList[i, "Client_ID"]), Convert.ToInt32(fgList[i, "Contract_ID"]), fgList[i, "ClientData"] + "",
                                               "", sThema, sBody, fgList[i, "Invoice_File"] + "", sAttachedFiles, DateTime.Now.ToString(), 1, 1, "");   // 5 - e-mail  
                        }

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
                    }
                    if (Convert.ToInt32(fgList[i, "ConnectionMethod"]) == 2)                  // 2 - post
                    {
                        dtInform = new DataTable("OfficialInforming");
                        dtCol = dtInform.Columns.Add("f1", System.Type.GetType("System.String"));
                        dtCol = dtInform.Columns.Add("f2", System.Type.GetType("System.String"));
                        dtCol = dtInform.Columns.Add("f3", System.Type.GetType("System.String"));
                        dtCol = dtInform.Columns.Add("f4", System.Type.GetType("System.String"));
                        dtCol = dtInform.Columns.Add("f5", System.Type.GetType("System.String"));
                        dtCol = dtInform.Columns.Add("f6", System.Type.GetType("System.String"));
                        //dtCol = dtInform.Columns.Add("f7", System.Type.GetType("System.String"));

                        dtRow = dtInform.NewRow();
                        sTemp = fgList[i, "ClientData"] + "";
                        dtRow["f1"] = sTemp.Replace("\t", "\n");
                        dtRow["f2"] = "";
                        dtRow["f3"] = "";
                        dtRow["f4"] = "ΘΕΣΣΑΛΟΝΙΚΗ " + DateTime.Now.ToString("dd/MM/yyyy");
                        dtRow["f5"] = "ΘΕΜΑ: ΕΝΗΜΕΡΩΣΗ ΤΡΙΜΗΝΟΥ " + sPeriod;
                        sTemp = "Στα πλαίσια της τρίμηνης ενημέρωσής σας, αποστέλλουμε:" + "\n\n";
                        if ((fgList[i, "Statement_File"] + "") != "") sTemp = sTemp + "- Statement περιόδου " + sPeriod + "\n";
                        if ((fgList[i, "Invoice_File"] + "") != "")
                        {
                            if (Convert.ToInt16(fgList[i, "ClientTipos"]) == 2) sTemp = sTemp + " - Τιμολόγιο";
                            else sTemp = sTemp + "- Απόδειξη";
                            sTemp = sTemp + " παροχής επενδυτικών υπηρεσιών";
                        }
                        dtRow["f6"] = sTemp + "\n\nΣτη διάθεσή σας για οποιαδήποτε διευκρίνιση.";
                        dtInform.Rows.Add(dtRow);

                        frmReports locReports = new frmReports();
                        locReports.ReportID = 19;
                        locReports.Params = sTemp;
                        locReports.ShowResult = dtInform;
                        locReports.Text = "Επίσημη Ενημέρωση Πελατών";
                        locReports.Show();
                        if ((fgList[i, "Invoice_File"] + "") != "") Global.DMS_ShowFile("Customers/" + sContractTitle + "/Invoices".ToString(), fgList[i, "Invoice_File"].ToString());
                        if ((fgList[i, "Statement_File"] + "") != "") Global.DMS_ShowFile("Customers/" + sContractTitle + "/Informing".ToString(), fgList[i, "Statement_File"].ToString());
                        if ((fgList[i, "Misc_File"] + "") != "") Global.DMS_ShowFile("Customers/" + sContractTitle + "/Informing".ToString(), fgList[i, "Misc_File"].ToString());
                        sTemp = fgList[i, "ContractTitle"] + "";

                        /*
                        var WordApp = new Microsoft.Office.Interop.Word.Application();
                        var curDoc = new Microsoft.Office.Interop.Word.Document();
                        WordApp.Visible = false;
                        string sPDF_FullPath;
                        // --- check Temp folder  -------------
                        sPDF_FullPath = Application.StartupPath + "\\Temp";
                        if (!Directory.Exists(sPDF_FullPath)) Directory.CreateDirectory(sPDF_FullPath);

                        sTemp = sPDF_FullPath + "\\OfficialInform_MF_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".docx";
                        if (File.Exists(sTemp)) File.Delete(sTemp);

                        File.Copy(Application.StartupPath + "\\Templates\\OfficialInfo.docx", sTemp);
                        curDoc = WordApp.Documents.Open(sTemp);

                        sTemp = fgList[i, "ClientData"] + "";
                        curDoc.Content.Find.Execute(FindText: "{address}", ReplaceWith: sTemp.Replace("\n", "\v"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{city_date}", ReplaceWith: "ΘΕΣΣΑΛΟΝΙΚΗ " + DateTime.Now.ToString("dd/MM/yyyy"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{thema}", ReplaceWith: "ΘΕΜΑ: ΕΝΗΜΕΡΩΣΗ ΤΡΙΜΗΝΟΥ " + sPeriod, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        sTemp = "Στα πλαίσια της τρίμηνης ενημέρωσής σας, αποστέλλουμε:" + "\v\v";
                        if ((fgList[i, "Statement_File"] + "") != "") sTemp = sTemp + "- Statement περιόδου " + sPeriod + "\v";
                        if ((fgList[i, "Invoice_File"] + "") != "")
                        {
                            if (Convert.ToInt16(fgList[i, "ClientTipos"]) == 2) sTemp = sTemp + " - Τιμολόγιο";
                            else sTemp = sTemp + "- Απόδειξη";
                            sTemp = sTemp + " παροχής επενδυτικών υπηρεσιών";
                        }
                        sTemp = sTemp + "\v\vΣτη διάθεσή σας για οποιαδήποτε διευκρίνιση.";
                        curDoc.Content.Find.Execute(FindText: "{message}", ReplaceWith: sTemp, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                       
                        curDoc.PrintOut();
                        WordApp.Documents.Close();
                        WordApp.Quit();
                        */
                        Global.AddInformingRecord(0, Convert.ToInt32(fgList[i, "ID"]), 8, 3, Convert.ToInt32(fgList[i, "Client_ID"]), Convert.ToInt32(fgList[i, "Contract_ID"]),
                                                  fgList[i, "ClientData"] + "", "", "Επίσημη Ενημέρωση Πελατών", "", "", sAttachedFiles, DateTime.Now.ToString(), 1, 1, "");           // 8 - post                      
                    }

                    clsManagmentFees_Recs ManagmentFees_Recs = new clsManagmentFees_Recs();
                    ManagmentFees_Recs.Record_ID = Convert.ToInt32(fgList[i, "ID"]);
                    ManagmentFees_Recs.GetRecord();
                    ManagmentFees_Recs.OfficialInformingDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    ManagmentFees_Recs.EditRecord();

                    fgList[i, "DateInform"] = DateTime.Now.ToString("dd/MM/yy");
                    fgList[i, 0] = false;
                }
            }
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
            locContract.RightsLevel = 1;                                          //iRightsLevel
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
            if (sTemp.Length > 0 && ((fgList[fgList.Row, "Invoice_File"] + "") != "")) Global.DMS_ShowFile("Customers/" + sTemp.Replace(".", "_") + "/Invoices", fgList[fgList.Row, "Invoice_File"].ToString());
        }

        private void mnuViewStatement_Click(object sender, EventArgs e)
        {
            sTemp = fgList[fgList.Row, "ContractTitle"] + "";
            if (sTemp.Length > 0 && ((fgList[fgList.Row, "Statement_File"] + "") != "")) Global.DMS_ShowFile("Customers/" + sTemp.Replace(".", "_") + "/Informing", fgList[fgList.Row, "Statement_File"].ToString());
        }

        private void mnuViewResult_Click(object sender, EventArgs e)
        {
            sTemp = fgList[fgList.Row, "ContractTitle"] + "";
            if (sTemp.Length > 0 && ((fgList[fgList.Row, "Misc_File"] + "") != "")) Global.DMS_ShowFile("Customers/" + sTemp.Replace(".", "_") + "/Informing", fgList[fgList.Row, "Misc_File"].ToString());
        }

        private void picCleanUp_Click(object sender, EventArgs e)
        {
            iClient_ID = 0;
            ucCS.ShowClientsList = false;
            ucCS.txtContractTitle.Text = "";
            ucCS.ShowClientsList = true;
            lblProfitCenter.Text = "";
            lblCode.Text = "";
            lnkPelatis.Text = "";
        }

        private void chkList_CheckedChanged(object sender, EventArgs e)
        {
            for (i = 1; i <= fgList.Rows.Count - 1; i++) fgList[i, 0] = chkList.Checked;
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
            dtCol = dtInform.Columns.Add("f12", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f13", System.Type.GetType("System.String"));

            for (i = 1; i <= fgList.Rows.Count - 1; i++)
            {
                dtRow = dtInform.NewRow();
                dtRow["f1"] = fgList[i, "AA"];
                dtRow["f2"] = fgList[i, "DateFrom"];
                dtRow["f3"] = fgList[i, "DateTo"];
                dtRow["f4"] = fgList[i, "ContractTitle"];
                dtRow["f5"] = fgList[i, "Code"];
                dtRow["f6"] = fgList[i, "Portfolio"];
                dtRow["f7"] = fgList[i, "DateFees"];
                dtRow["f8"] = fgList[i, "DateInform"];
                dtRow["f9"] = fgList[i, "InformingMethod_Title"];
                dtRow["f10"] = fgList[i, "ClientData"];
                dtRow["f11"] = fgList[i, "Invoice_File"];
                dtRow["f12"] = fgList[i, "Statement_File"];
                dtRow["f13"] = fgList[i, "Misc_File"];
                dtInform.Rows.Add(dtRow);
            }

            frmReports locReports = new frmReports();
            locReports.Params = cmbProviders.Text + "~" + sPeriod + "~" + Global.UserName + "~" + Global.CompanyName + "~";

            locReports.ReportID = 21;
            locReports.ShowResult = dtInform;
            locReports.Text = "Επίσημη Ενημέρωση Πελατών";
            locReports.Show();
        }

        private void tsbRefresh_ManFees_Click(object sender, EventArgs e)
        {
            DefineCommandsList();
        }
        private void EditCommandsInformingData_ManFees()
        {
            if (Convert.ToInt32(fgList.Row) > 0)
            {
                sFile1 = "";
                sFile2 = "";
                sFile3 = "";
                lblInformingDate.Text = fgList[fgList.Row, "DateInform"] + "";
                lblInformingMethod.Text = fgList[fgList.Row, "InformingMethod_Title"] + "";
                lblInformingClientData.Text = fgList[fgList.Row, "ClientData"] + "";
                txtInvoice.Text = fgList[fgList.Row, "Invoice_File"] + "";
                txtStatement.Text = fgList[fgList.Row, "Statement_File"] + "";
                txtMisc.Text = fgList[fgList.Row, "Misc_File"] + "";
                panEditData.Visible = true;
                btnCancel.Focus();
            }
        }
        private void picStatemenetsFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.  
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtStatementsFolder.Text = folderDlg.SelectedPath;
                Environment.SpecialFolder root = folderDlg.RootFolder;
            }
        }
        private void btnDownload_OK_Click(object sender, EventArgs e)
        {
            string sCode, sSubcode, sAppendix, sStatementFile, sMiscFile;
            string[] tokens;
            string[] bokens;

            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            if (txtStatementsFolder.Text != "")
            {

                if (chkConvert_File.Checked)
                {
                    var docFiles = new DirectoryInfo(txtStatementsFolder.Text).GetFiles("*.*");
                    foreach (FileInfo file in docFiles)
                        sTemp = Convert2PDF(txtStatementsFolder.Text + "/" + file.Name);
                }

                var pdfFiles = new DirectoryInfo(txtStatementsFolder.Text).GetFiles("*.pdf");
                foreach (FileInfo file in pdfFiles)
                {
                    sTemp = file.Name;
                    sCode = "";
                    sSubcode = "";
                    sAppendix = "";
                    sStatementFile = "";
                    sMiscFile = "";

                    switch (cmbStatement_File.SelectedIndex)
                    {
                        case 0:
                            break;
                        case 1:
                            tokens = Path.GetFileNameWithoutExtension(sTemp).Split('_');
                            bokens = cmbStatement_File.Text.Split('_');
                            if (tokens.Length >= 2)
                            {
                                sCode = tokens[0];
                                sSubcode = tokens[1];
                                sAppendix = tokens[2];
                                if (sAppendix == bokens[2]) sStatementFile = sTemp;
                            }
                            break;
                        case 2:
                            tokens = Path.GetFileNameWithoutExtension(sTemp).Split('_');
                            bokens = cmbStatement_File.Text.Split('_');
                            if (tokens.Length >= 2)
                            {
                                sCode = tokens[0];
                                sSubcode = tokens[1];
                                sAppendix = tokens[2];
                                if (sAppendix == bokens[2]) sStatementFile = sTemp;
                            }
                            break;
                        case 3:
                            sCode = Path.GetFileNameWithoutExtension(sTemp);
                            sSubcode = "";
                            sAppendix = "";
                            sStatementFile = sTemp;
                            break;
                        case 4:
                            sCode = "";
                            sSubcode = Path.GetFileNameWithoutExtension(sTemp);
                            sAppendix = "";
                            sStatementFile = sTemp;
                            break;
                    }

                    switch (cmbMisc_File.SelectedIndex)
                    {
                        case 0:
                            break;
                        case 1:
                            tokens = Path.GetFileNameWithoutExtension(sTemp).Split('_');
                            bokens = cmbMisc_File.Text.Split('_');
                            if (tokens.Length >= 2)
                            {
                                sCode = tokens[0];
                                sSubcode = tokens[1];
                                sAppendix = tokens[2];
                                if (sAppendix == bokens[2]) sStatementFile = sTemp;
                            }
                            break;
                        case 2:
                            tokens = Path.GetFileNameWithoutExtension(sTemp).Split('_');
                            bokens = cmbMisc_File.Text.Split('_');
                            if (tokens.Length >= 2)
                            {
                                sCode = tokens[0];
                                sSubcode = tokens[1];
                                sAppendix = tokens[2];
                                if (sAppendix == bokens[2]) sStatementFile = sTemp;
                            }
                            break;
                        case 3:
                            sCode = Path.GetFileNameWithoutExtension(sTemp);
                            sSubcode = "";
                            sAppendix = "";
                            sMiscFile = sTemp;
                            break;
                        case 4:
                            sCode = "";
                            sSubcode = Path.GetFileNameWithoutExtension(sTemp);
                            sAppendix = "";
                            sMiscFile = sTemp;
                            break;
                    }

                    if (sStatementFile.Length > 0)
                    {
                        j = 1;
                        while (true)
                        {
                            switch (cmbStatement_File.SelectedIndex)
                            {
                                case 0:
                                case 1:
                                    j = fgList.FindRow(sCode + "_" + sSubcode, j, 20, false);
                                    break;
                                case 2:
                                    j = fgList.FindRow(sCode + "_" + sSubcode, j, 20, false);
                                    break;
                                case 3:
                                    j = fgList.FindRow(sCode, j, 6, false);
                                    break;
                                case 4:
                                    j = fgList.FindRow(sSubcode, j, 7, false);
                                    break;
                            }

                            if (j > 0)
                            {
                                if (sStatementFile.Length > 0)
                                {
                                    sTemp = fgList[j, 5] + "";
                                    sStatementFile = Global.DMS_UploadFile(txtStatementsFolder.Text + "/" + sStatementFile, "Customers/" + sTemp.Replace(".", "_") + "/Informing", sStatementFile);

                                    sStatementFile = Path.GetFileName(sStatementFile);

                                    fgList[j, 16] = sStatementFile;

                                    clsManagmentFees_Recs ManagmentFees_Rec = new clsManagmentFees_Recs();
                                    ManagmentFees_Rec.Record_ID = Convert.ToInt32(fgList[j, 18]);
                                    ManagmentFees_Rec.GetRecord();
                                    ManagmentFees_Rec.Statement_File = sStatementFile;
                                    ManagmentFees_Rec.EditRecord();

                                }

                                if (sSubcode.Length == 0) j = j + 1;
                                else break;
                            }
                            else break;
                        }
                    }

                    if (sMiscFile.Length > 0)
                    {
                        j = 1;
                        while (true)
                        {
                            switch (cmbMisc_File.SelectedIndex)
                            {
                                case 0:
                                case 1:
                                    j = fgList.FindRow(sCode + "_" + sSubcode, j, 20, false);
                                    break;
                                case 2:
                                    j = fgList.FindRow(sCode + "_" + sSubcode, j, 20, false);
                                    break;
                                case 3:
                                    j = fgList.FindRow(sCode, j, 6, false);
                                    break;
                                case 4:
                                    j = fgList.FindRow(sSubcode, j, 7, false);
                                    break;
                            }

                            if (j > 0)
                            {
                                if (sMiscFile.Length > 0)
                                {
                                    sTemp = fgList[j, 5] + "";
                                    sMiscFile = Global.DMS_UploadFile(txtStatementsFolder.Text + "/" + sMiscFile, "Customers/" + sTemp.Replace(".", "_") + "/Informing", sMiscFile);

                                    sMiscFile = Path.GetFileName(sMiscFile);

                                    fgList[j, 17] = sMiscFile;

                                    clsManagmentFees_Recs ManagmentFees_Rec = new clsManagmentFees_Recs();
                                    ManagmentFees_Rec.Record_ID = Convert.ToInt32(fgList[j, 18]);
                                    ManagmentFees_Rec.GetRecord();
                                    ManagmentFees_Rec.Misc_File = sMiscFile;
                                    ManagmentFees_Rec.EditRecord();

                                }
                                if (sSubcode.Length == 0) j = j + 1;
                                else break;
                            }
                            else break;
                        }
                    }
                }
            }
            panFolders.Visible = false;
            this.Refresh();
            this.Cursor = Cursors.Default;
        }

        private void btnDownload_Cancel_Click(object sender, EventArgs e)
        {
            panFolders.Visible = false;
        }
        private void picAttachedInvoice_Click(object sender, EventArgs e)
        {
            sFile1 = Global.FileChoice(Global.DefaultFolder);
            if (sFile1.Length > 0) txtInvoice.Text = Path.GetFileName(sFile1);
        }

        private void picShowInvoice_Click(object sender, EventArgs e)
        {
            sTemp = (fgList[fgList.Row, 4] + "").Trim();
            if (sTemp.Length > 0 && txtInvoice.Text.Length > 0)
                Global.DMS_ShowFile("Customers/" + sTemp.Replace(".", "_") + "/Invoices", txtInvoice.Text);
        }

        private void picAttachedStatement_Click(object sender, EventArgs e)
        {
            sFile2 = Global.FileChoice(Global.DefaultFolder);
            if (sFile2.Length > 0) txtStatement.Text = Path.GetFileName(sFile2);
        }

        private void picShowStatement_Click(object sender, EventArgs e)
        {
            sTemp = (fgList[fgList.Row, 5] + "").Trim();
            if (sTemp.Length > 0 && txtStatement.Text.Length > 0)
                Global.DMS_ShowFile("Customers/" + sTemp.Replace(".", "_") + "/Informing", txtStatement.Text);
        }

        private void picAttachedMisc_Click(object sender, EventArgs e)
        {
            sFile3 = Global.FileChoice(Global.DefaultFolder);
            if (sFile3.Length > 0) txtMisc.Text = Path.GetFileName(sFile3);
        }

        private void picShowMisc_Click(object sender, EventArgs e)
        {
            sTemp = (fgList[fgList.Row, 5] + "").Trim();
            if (sTemp.Length > 0 && txtMisc.Text.Length > 0)
                Global.DMS_ShowFile("Customers/" + sTemp.Replace(".", "_") + "/Informing", txtMisc.Text);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            sTemp = fgList[fgList.Row, 5] + "";

            if (sFile1.Length > 0)
            {
                txtInvoice.Text = Global.DMS_UploadFile(sFile1, "Customers/" + sTemp.Replace(".", "_") + "/Invoices", txtInvoice.Text);
                txtInvoice.Text = Path.GetFileName(txtInvoice.Text);
            }
            fgList[fgList.Row, 15] = txtInvoice.Text;

            if (sFile2.Length > 0)
            {
                txtStatement.Text = Global.DMS_UploadFile(sFile2, "Customers/" + sTemp.Replace(".", "_") + "/Informing", txtStatement.Text);
                txtStatement.Text = Path.GetFileName(txtStatement.Text);
            }
            fgList[fgList.Row, 16] = txtStatement.Text;

            if (sFile3.Length > 0)
            {
                txtMisc.Text = Global.DMS_UploadFile(sFile3, "Customers/" + sTemp.Replace(".", "_") + "/Informing", txtMisc.Text);
                txtMisc.Text = Path.GetFileName(txtMisc.Text);
            }
            fgList[fgList.Row, 17] = txtMisc.Text;

            clsManagmentFees_Recs ManagmentFees_Rec = new clsManagmentFees_Recs();
            ManagmentFees_Rec.Record_ID = Convert.ToInt32(fgList[fgList.Row, 18]);
            ManagmentFees_Rec.GetRecord();
            ManagmentFees_Rec.Invoice_File = txtInvoice.Text;
            ManagmentFees_Rec.Statement_File = txtStatement.Text;
            ManagmentFees_Rec.Misc_File = txtMisc.Text;
            ManagmentFees_Rec.EditRecord();

            panEditData.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panEditData.Visible = false;
        }
        private void EmptyContractData()
        {
            stContractData.ContractTitle = "";
            stContractData.Code = "";
            stContractData.Portfolio = "";
            stContractData.ClientName = "";
            stContractData.Service_Title = "";
            stContractData.Profile_Title = "";
            stContractData.Policy_Title = "";
            stContractData.Provider_Title = "";
            stContractData.Package_Title = "";
            stContractData.Currency = "";
            stContractData.EMail = "";
            stContractData.Mobile = "";
            stContractData.NumberAccount = "";
            stContractData.Contract_ID = 0;
            stContractData.Client_ID = 0;
            stContractData.Provider_ID = 0;
            stContractData.Policy_ID = 0;
            stContractData.Profile_ID = 0;
            stContractData.Service_ID = 0;
            stContractData.Status = 0;
            stContractData.ClientType = 0;
            stContractData.VAT_Percent = 0;
            stContractData.CFP_ID = 0;
            stContractData.Contracts_Details_ID = 0;
            stContractData.Contracts_Packages_ID = 0;
            stContractData.MIFID_Risk_Index = 0;
            stContractData.MIFIDCategory_ID = 0;
            stContractData.MIFID_2 = 0;
        }
        private string ManFeesEmailBody(int iInvoice, int iStatemenet)
        {
            string sBody, sInvoice, sStatement;

            sStatement = "";
            if (iStatemenet != 0) sStatement = "- Statement περιόδου " + sPeriod;

            sInvoice = "";
            if (iInvoice != 0)
            {
                if (iInvoice == 1) sInvoice = "- Απόδειξη";
                else
                if (iInvoice == 2) sInvoice = "- Τιμολόγιο";

                sInvoice = sInvoice + " παροχής επενδυτικών υπηρεσιών";
            }

            sBody = "<html xmlns='http://www.w3.org/1999/xhtml'><head><title></title><meta http-equiv='Content-Type' content='text/html; charset=utf-8' />" +
            "<style>img.logo {height: 60%;width: 40%;}</style></head><body style='width: 800px;'><br/><br/><table><tr><td width=800>" +
            "<div style='height: 150px;'><img class='logo' src='http://www.hellasfin.gr/signs/images/Logo_500px.jpg' alt='' /></div><br/><br/>" +
            "Δ/ΝΣΗ<br/>ΕΝΗΜΕΡΩΣΗΣ ΚΑΙ ΕΞΥΠΗΡΕΤΗΣΗΣ ΕΠΕΝΔΥΤΩΝ <br/><br/><br/><br/>" +
            "<div align='right'>ΘΕΣΣΑΛΟΝΙΚΗ " + DateTime.Now.ToString("dd/MM/yyyy") + "</div><br/><br/><br/><br/><br/><br/>" +
            "<center> ΘΕΜΑ: ΕΝΗΜΕΡΩΣΗ ΤΡΙΜΗΝΟΥ " + sPeriod + "</center>" + "<br/><br/><br/><br/>" +
            "Στα πλαίσια της τρίμηνης ενημέρωσής σας, αποστέλλουμε : <br/><br/><br/>" +
            sStatement + "<br/>" +
            sInvoice + "<br/><br/><br/>" +
            "Στη διάθεσή σας για οποιαδήποτε διευκρίνιση." + "<br/><br/><br/><br/><br/><br/><br/><br/><br/><br/>" +
            "<div align='right'>HELLASFIN Α.Ε.Π.Ε.Υ.</div>" + "<br/><br/><br/><br/><br/><br/><br/>" +
            "Παρακαλούμε για οποιαδήποτε διευκρίνηση επικοινωνήστε με τον Επενδυτικό σας Σύμβουλο ή τον Υπεύθυνο Σχέσης (RM) στα τηλ. Θεσσαλονίκη: +30 2310 517800, " +
            "Αθήνα: +30 210 3387710, Κρήτη: +30 2810 343366<br/><br/>" +
            "*Tυχόν αντιρρήσεις σας σε οποιοδήποτε στοιχείο της παρούσας ενημέρωσης καλείστε να τις υποβάλλετε στην Εταιρία μας εγγράφως εντός δεκαπέντε (15) ημερολογιακών ημερών, αλλιώς θεωρούμε ότι συμφωνείτε απολύτως. " +
            "</td></tr></table><br/><br/>" +
            "</body></html>";
            return sBody;
        }

        private string Convert2PDF(string sSourceFile)
        {
            var WordApp = new Microsoft.Office.Interop.Word.Application();
            var curDoc = new Microsoft.Office.Interop.Word.Document();
            string sTargetFile = Path.GetDirectoryName(sSourceFile) + "/" + Path.GetFileNameWithoutExtension(sSourceFile) + ".pdf";
            curDoc = WordApp.Documents.Open(sSourceFile);
            curDoc.SaveAs2(sTargetFile, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF);
            WordApp.Quit();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            return sTargetFile;
        }
    }
}
