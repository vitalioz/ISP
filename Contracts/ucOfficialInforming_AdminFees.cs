using C1.Win.C1FlexGrid;
using Core;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Contracts
{
    public partial class ucOfficialInforming_AdminFees : UserControl
    {
        DataColumn dtCol;
        DataRow dtRow;
        DataTable dtInform;
#pragma warning disable CS0169 // The field 'ucOfficialInforming_AdminFees.j' is never used
        int i, j, iClient_ID, iAT_ID, iAF_Quart;
#pragma warning restore CS0169 // The field 'ucOfficialInforming_AdminFees.j' is never used
        string sTemp, sPeriod, sDate, sThema, sContractTitle, sFileFullName, sConnectionMethod, sConnectionData, sAttachedFiles, sOldCode, sBody;
#pragma warning disable CS0414 // The field 'ucOfficialInforming_AdminFees.bCheckList' is assigned but its value is never used
        bool bCheckList;
#pragma warning restore CS0414 // The field 'ucOfficialInforming_AdminFees.bCheckList' is assigned but its value is never used
        Global.ContractData stContractData;
        public ucOfficialInforming_AdminFees()
        {
            InitializeComponent();
        }

        private void ucOfficialInforming_AdminFees_Load(object sender, EventArgs e)
        {
            bCheckList = false;

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

            bCheckList = true;
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

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            panEditData.Visible = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtInvoice.Text.Length > 0)
            {
                sTemp = (fgList[fgList.Row, "ContractTitle"] + "").Trim();
                txtInvoice.Text = Global.DMS_UploadFile(sFileFullName, "Customers/" + sTemp.Replace(".", "_") + "/Invoices", txtInvoice.Text);
                txtInvoice.Text = Path.GetFileName(txtInvoice.Text);
            }
            fgList[fgList.Row, "Invoice_File"] = txtInvoice.Text;

            panEditData.Visible = false;
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            panEditData.Visible = false;
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
            iAF_Quart = 0;
            sPeriod = "";
            if (Convert.ToInt32(cmbProviders.SelectedValue) != 0)
            {
                sTemp = cmbYear.Text;
                if (rb1.Checked)
                {
                    iAF_Quart = 1;
                    sPeriod = "31/12/" + (Convert.ToInt16(cmbYear.Text) - 1) + " - " + "31/03/" + sTemp;
                }
                else
                {
                    if (rb2.Checked)
                    {
                        iAF_Quart = 2;
                        sPeriod = "31/03/" + sTemp + " - " + "30/06/" + sTemp;
                    }
                    else
                    {
                        if (rb3.Checked)
                        {
                            iAF_Quart = 3;
                            sPeriod = "30/06/" + sTemp + " - " + "30/09/" + sTemp;
                        }
                        else
                        {
                            if (rb4.Checked)
                            {
                                iAF_Quart = 4;
                                sPeriod = "30/09/" + sTemp + " - " + "31/12/" + sTemp;
                            }
                        }
                    }
                }

                i = 0;
                clsAdminFees_Titles klsAdminFees_Title = new clsAdminFees_Titles();
                klsAdminFees_Title.SC_ID = Convert.ToInt32(cmbProviders.SelectedValue);
                klsAdminFees_Title.AF_Year = Convert.ToInt32(cmbYear.Text);
                klsAdminFees_Title.AF_Quart = iAF_Quart;
                klsAdminFees_Title.GetRecord_Title();
                iAT_ID = klsAdminFees_Title.Record_ID;

                fgList.Redraw = false;
                fgList.Rows.Count = 1;
                //-------------- Define AdminFees_Recs List ------------------
                clsAdminFees_Recs AdminFees_Recs = new clsAdminFees_Recs();
                AdminFees_Recs.AT_ID = iAT_ID;
                AdminFees_Recs.GetList();
                foreach (DataRow dtRow in AdminFees_Recs.List.Rows)
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
                        if (Convert.ToInt16(dtRow["ConnectionMethod"]) == 2)
                        {
                            sConnectionMethod = "Ταχ/κη αποστολή";
                            sConnectionData = dtRow["User1_Name"] + "\n" + dtRow["Address"] + "\n" + dtRow["City"] + " " + dtRow["ZIP"];
                            if ((dtRow["Country"] + "") != "Greece")
                                sConnectionData = sConnectionData + " " + dtRow["Country"];
                        }
                        sTemp = "";
                        if (Convert.ToDateTime(dtRow["DateFees"]) != Convert.ToDateTime("01/01/1900")) sTemp = Convert.ToDateTime(dtRow["DateFees"]).ToString("dd/MM/yyyy");

                        i = i + 1;
                        fgList.AddItem(false + "\t" + i + "\t" + dtRow["DateFrom"] + "\t" + dtRow["DateTo"] + "\t" + dtRow["ClientName"] + "\t" +
                                      dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" + dtRow["PackageTitle"] + "\t" +
                                      dtRow["FinishAmount"] + "\t" + dtRow["Invoice_Num"] + "\t" + sTemp + "\t" + sDate + "\t" +
                                      sConnectionMethod + "\t" + sConnectionData + "\t" + dtRow["Invoice_File"] + "\t" + dtRow["ID"] + "\t" +
                                      dtRow["Client_ID"] + "\t" + dtRow["Code"] + "_" + dtRow["Portfolio"] + "\t" + dtRow["ConnectionMethod"] + "\t" +
                                      dtRow["ClientType"] + "\t" + dtRow["Contract_ID"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"] + "\t");
                    }

                }
                fgList.Redraw = true;
                toolAdminFees.Visible = true;
            }
            else MessageBox.Show("Επιλέξτε των Πάροχο", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            if (sTemp.Length > 0) Global.DMS_ShowFile("Customers/" + sTemp.Replace(".", "_") + "/Invoices", fgList[fgList.Row, "Invoice_File"].ToString());
        }

        private void chkList_CheckedChanged(object sender, EventArgs e)
        {
            for (i = 1; i <= fgList.Rows.Count - 1; i++) fgList[i, 0] = chkList.Checked;
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
        private void tsbEditProposal_AdminFees_Click(object sender, EventArgs e)
        {
            EditCommandsInformingData_AdminFees();
        }
        private void fgList_DoubleClick(object sender, EventArgs e)
        {
            EditCommandsInformingData_AdminFees();
        }
        private void EditCommandsInformingData_AdminFees()
        {
            if (Convert.ToInt32(fgList.Row) > 0)
            {
                lblInformingDate.Text = fgList[fgList.Row, "DateInform"] + "";
                lblInformingMethod.Text = fgList[fgList.Row, "InformingMethod_Title"] + "";
                lblInformingClientData.Text = fgList[fgList.Row, "ClientData"] + "";
                txtInvoice.Text = fgList[fgList.Row, "Invoice_File"] + "";
                panEditData.Visible = true;
                btnCancel.Focus();
            }
        }
        private void tsbSend_Click(object sender, EventArgs e)
        {
            int iInvoiceType = 0;
            int iRec_ID = 0;

            sOldCode = "~~~";
            sThema = "ΑΜΟΙΒΗ ΥΠΟΣΤΗΡΙΞΗΣ ΧΑΡΤΟΦΥΛΑΚΙΟΥ ";

            for (i = 1; i <= fgList.Rows.Count - 1; i++)
            {
                if (Convert.ToBoolean(fgList[i, 0]))
                {
                    sContractTitle = fgList[i, "ContractTitle"] + "";
                    sContractTitle = sContractTitle.Replace(".", "_");

                    sAttachedFiles = "";
                    if (Convert.ToInt32(fgList[i, "ConnectionMethod"]) == 1)                                     // 1 - e-mail
                    {
                        iInvoiceType = 0;
                        if ((fgList[i, "Invoice_File"] + "") != "")
                        {
                            iInvoiceType = 1;
                            if (Convert.ToInt32(fgList[i, "ClientTipos"]) == 2) iInvoiceType = 2;
                        }
                        sBody = AdminFeesEmailBody(iInvoiceType);

                        if (sOldCode != (fgList[i, "Code"] + ""))                     // if it's a new code write into Informings table record that will be send
                        {
                            sOldCode = fgList[i, "Code"] + "";
                            iRec_ID = Global.AddInformingRecord(0, Convert.ToInt32(fgList[i, "ID"]), 5, 3, Convert.ToInt32(fgList[i, "Client_ID"]), Convert.ToInt32(fgList[i, "Contract_ID"]), fgList[i, "ClientData"] + "",
                                               "", "Επίσημη Ενημέρωση Πελατών", sBody, fgList[i, "Invoice_File"] + "", "", "", 0, 0, "");                        // 5 - e-mail 
                        }
                        else                                             // if it's an old code write into Informings table record that will not be send - last 3 parameters say that this record was sent
                        {
                            iRec_ID = Global.AddInformingRecord(0, Convert.ToInt32(fgList[i, "ID"]), 5, 3, Convert.ToInt32(fgList[i, "Client_ID"]), Convert.ToInt32(fgList[i, "Contract_ID"]), fgList[i, "ClientData"] + "",
                                               "", "Επίσημη Ενημέρωση Πελατών", sBody, fgList[i, "Invoice_File"] + "", "", DateTime.Now.ToString(), 1, 1, "");   // 5 - e-mail  
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
                        dtCol = dtInform.Columns.Add("f7", System.Type.GetType("System.String"));

                        dtRow = dtInform.NewRow();
                        sTemp = fgList[i, "ClientData"] + "";
                        dtRow["f1"] = sTemp.Replace("\t", "\n");
                        dtRow["f2"] = "";
                        dtRow["f3"] = "";
                        dtRow["f4"] = "ΘΕΣΣΑΛΟΝΙΚΗ " + DateTime.Now.ToString("dd/MM/yyyy");
                        dtRow["f5"] = "ΘΕΜΑ: ΑΜΟΙΒΗ ΥΠΟΣΤΗΡΙΞΗΣ ΧΑΡΤΟΦΥΛΑΚΙΟΥ";
                        sTemp = "Στα πλαίσια της ενημέρωσής σας, αποστέλλουμε :" + "\n\n";
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
                        if ((fgList[i, "Invoice_File"] + "") != "")
                        {
                            sTemp = fgList[i, 5] + "";
                            Global.DMS_PrintFile("Customers/" + sTemp.Replace(".", "_") + "/Invoices".ToString(), fgList[i, "Invoice_File"].ToString());
                        }
                        sTemp = fgList[i, "ContractTitle"] + "";

                        Global.AddInformingRecord(0, Convert.ToInt32(fgList[i, "ID"]), 8, 3, Convert.ToInt32(fgList[i, "Client_ID"]), Convert.ToInt32(fgList[i, "Contract_ID"]),
                                                  fgList[i, "ClientData"] + "", "", sThema, "", (fgList[i, "Invoice_File"] + ""), sAttachedFiles, DateTime.Now.ToString(), 1, 1, "");     // 8 - post                      
                    }

                    clsAdminFees_Recs AdminFees_Recs = new clsAdminFees_Recs();
                    AdminFees_Recs.Record_ID = Convert.ToInt32(fgList[i, "ID"]);
                    AdminFees_Recs.GetRecord();
                    AdminFees_Recs.OfficialInformingDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    AdminFees_Recs.EditRecord();

                    fgList[i, "DateInform"] = DateTime.Now.ToString("dd/MM/yy");
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
                dtInform.Rows.Add(dtRow);
            }

            frmReports locReports = new frmReports();
            locReports.Params = cmbProviders.Text + "~" + sPeriod + "~" + Global.UserName + "~" + Global.CompanyName + "~" + "Περιοδική Επίσημη Ενημέρωση Πελατών Administration Fees" + "~";

            locReports.ReportID = 22;
            locReports.ShowResult = dtInform;
            locReports.Text = "Επίσημη Ενημέρωση Πελατών";
            locReports.Show();
        }

        private void tsbRefresh_AdminFees_Click(object sender, EventArgs e)
        {
            DefineCommandsList();
        }
        private void picAttachedInvoice_Click(object sender, EventArgs e)
        {
            sFileFullName = Global.FileChoice(Global.DefaultFolder);
            if (sFileFullName.Length > 0) txtInvoice.Text = Path.GetFileName(sFileFullName);
            else txtInvoice.Text = "";
        }

        private void picShowInvoice_Click(object sender, EventArgs e)
        {
            sTemp = (fgList[fgList.Row, 4] + "").Trim();
            if (sTemp.Length > 0 && txtInvoice.Text.Length > 0)
                Global.DMS_ShowFile("Customers/" + sTemp.Replace(".", "_") + "/Invoices", txtInvoice.Text);
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
        private string AdminFeesEmailBody(int iInvoice)
        {
            string sBody, sInvoice;

            sInvoice = "";
            if (iInvoice != 0)
            {
                if (iInvoice == 1) sInvoice = "- Απόδειξη";
                else if (iInvoice == 2) sInvoice = "- Τιμολόγιο";
                sInvoice = sInvoice + " παροχής υπηρεσιών";
            }

            sBody = "<html xmlns='http://www.w3.org/1999/xhtml'><head><title></title><meta http-equiv='Content-Type' content='text/html; charset=utf-8' />" +
            "<style>img.logo {height: 60%;width: 40%;}</style></head><body style='width: 800px;'><br/><br/><table><tr><td width=800>" +
            "<div style='height: 150px;'><img class='logo' src='http://www.hellasfin.gr/signs/images/Logo_500px.jpg' alt='' /></div><br/><br/>" +
            "Δ/ΝΣΗ<br/>ΕΝΗΜΕΡΩΣΗΣ ΚΑΙ ΕΞΥΠΗΡΕΤΗΣΗΣ ΕΠΕΝΔΥΤΩΝ <br/><br/><br/><br/>" +
            "<div align='right'>ΘΕΣΣΑΛΟΝΙΚΗ " + DateTime.Now.ToString("dd/MM/yyyy") + "</div><br/><br/><br/><br/><br/><br/>" +
            "<center> ΘΕΜΑ: ΑΜΟΙΒΗ ΥΠΟΣΤΗΡΙΞΗΣ ΧΑΡΤΟΦΥΛΑΚΙΟΥ " + "</center>" + "<br/><br/><br/><br/>" +
            "Στα πλαίσια της ενημέρωσής σας, αποστέλλουμε : <br/><br/><br/>" +
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
    }
}
