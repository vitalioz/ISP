using C1.Win.C1FlexGrid;
using Core;
using System;
using System.Data;
using System.Windows.Forms;

namespace Contracts
{
    public partial class ucOfficialInforming_FX : UserControl
    {
        DataColumn dtCol;
        DataRow dtRow;
        DataTable dtInform;
        int i, iClient_ID;
        string sTemp, sClientName, sRecipientName, sConnectionMethod, sConnectionData, sAttachedFiles, sOldCode, sBody;
        Global.ContractData stContractData;
        clsOrdersFX OrdersFX = new clsOrdersFX();
        public ucOfficialInforming_FX()
        {
            InitializeComponent();
            EmptyContractData();
        }

        private void ucOfficialInforming_FX_Load(object sender, EventArgs e)
        {
            ucCS.StartInit(700, 400, 570, 20, 1);
            ucCS.TextOfLabelChanged += new EventHandler(ucCustomerChoice_TextOfLabelChanged);
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
            fgList.MouseDown += new MouseEventHandler(fgList_MouseDown);

            dFrom.Value = DateTime.Now.AddDays(-7);
            dTo.Value = DateTime.Now;
        }
        protected override void OnResize(EventArgs e)
        {
            btnSearch.Left = this.Width - 110;
            fgList.Width = this.Width - 20;
            fgList.Height = this.Height - 116;
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
        private void chkRTO_CheckedChanged(object sender, EventArgs e)
        {
            for (i = 1; i <= fgList.Rows.Count - 1; i++) fgList[i, 0] = chkRTO.Checked;
        }
        private void lnkPelatis_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmClientData locClientData = new frmClientData();
            locClientData.Client_ID = iClient_ID;
            locClientData.Text = Global.GetLabel("customer_information");
            locClientData.Show();
        }

        private void tsbSend_Click(object sender, EventArgs e)
        {
            int iRec_ID = 0;
            clsInvoiceTitles klsInvoiceTitles = new clsInvoiceTitles();
            sOldCode = "~~~";

            for (i = 1; i <= fgList.Rows.Count - 1; i++)
            {
                if (Convert.ToBoolean(fgList[i, 0]))
                {
                    sAttachedFiles = fgList[i, "FileName"] + "~";

                    if (Convert.ToInt32(fgList[i, "ConnectionMethod"]) == 1)                                     // 1 - e-mail
                    {
                        sBody = DailyEmailBody(Convert.ToInt32(fgList[i, "ClientTipos"]));

                        if (sOldCode != (fgList[i, "Code"] + ""))                     // if it's a new code write into Informings table record that will be send
                        {
                            sOldCode = fgList[i, "Code"] + "";
                            iRec_ID = Global.AddInformingRecord(0, Convert.ToInt32(fgList[i, "ID"]), 5, 6, Convert.ToInt32(fgList[i, "Client_ID"]), Convert.ToInt32(fgList[i, "Contract_ID"]), fgList[i, "ClientData"] + "",
                                               "backoffice@hellasfin.gr", "ΑΜΟΙΒΗ ΔΙΑΒΙΒΑΣΗΣ ΕΝΤΟΛΗΣ ΜΕΤΑΤΡΟΠΗΣ ΣΥΝΑΛΛΑΓΜΑΤΟΣ", sBody, fgList[i, "FileName"] + "", "", "", 0, 0, "");                        // 5 - e-mail 
                        }
                        else                                             // if it's an old code write into Informings table record that will not be send - last 3 parameters say that this record was sent
                        {
                            iRec_ID = Global.AddInformingRecord(0, Convert.ToInt32(fgList[i, "ID"]), 5, 6, Convert.ToInt32(fgList[i, "Client_ID"]), Convert.ToInt32(fgList[i, "Contract_ID"]), fgList[i, "ClientData"] + "",
                                               "backoffice@hellasfin.gr", "ΑΜΟΙΒΗ ΔΙΑΒΙΒΑΣΗΣ ΕΝΤΟΛΗΣ ΜΕΤΑΤΡΟΠΗΣ ΣΥΝΑΛΛΑΓΜΑΤΟΣ", sBody, fgList[i, "FileName"] + "", "", DateTime.Now.ToString(), 1, 1, "");   // 5 - e-mail  
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
                        dtRow["f5"] = "ΘΕΜΑ: ΑΜΟΙΒΗ ΔΙΑΒΙΒΑΣΗΣ ΕΝΤΟΛΗΣ ΜΕΤΑΤΡΟΠΗΣ ΣΥΝΑΛΛΑΓΜΑΤΟΣ";
                        dtRow["f6"] = "Στα πλαίσια ενημέρωσής σας, αποστέλλουμε:" + "\n\n" + (Convert.ToInt32(fgList[i, "ClientTipos"]) == 2 ? " -	Τιμολόγιο παροχής υπηρεσιών" : "-	Απόδειξη παροχής υπηρεσιών") + "\n\n" +
                                      "Στη διάθεσή σας για οποιαδήποτε διευκρίνιση.";
                        dtInform.Rows.Add(dtRow);

                        frmReports locReports = new frmReports();
                        locReports.ReportID = 19;
                        locReports.Params = sTemp;
                        locReports.ShowResult = dtInform;
                        locReports.Text = "Επίσημη Ενημέρωση Πελατών";
                        locReports.Show();

                        sTemp = fgList[i, "ContractTitle"] + "";

                        Global.AddInformingRecord(0, Convert.ToInt32(fgList[i, "ID"]), 8, 6, Convert.ToInt32(fgList[i, "Client_ID"]), Convert.ToInt32(fgList[i, "Contract_ID"]), fgList[i, "ClientData"] + "", "",
                                                  "ΑΜΟΙΒΗ ΔΙΑΒΙΒΑΣΗΣ ΕΝΤΟΛΗΣ ΜΕΤΑΤΡΟΠΗΣ ΣΥΝΑΛΛΑΓΜΑΤΟΣ", "", "", sAttachedFiles, DateTime.Now.ToString(), 1, 1, "");                        // 8 - post                      
                    }

                    klsInvoiceTitles.Record_ID = Convert.ToInt32(fgList[i, "Invoice_Titles_ID"]);
                    klsInvoiceTitles.GetRecord();
                    klsInvoiceTitles.OfficialInformingDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    klsInvoiceTitles.EditRecord();

                    fgList[i, "InformingDate"] = DateTime.Now.ToString("dd/MM/yy");
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
            dtCol = dtInform.Columns.Add("f12", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f13", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f14", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f15", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f16", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f17", System.Type.GetType("System.String"));
            dtCol = dtInform.Columns.Add("f18", System.Type.GetType("System.String"));

            for (i = 1; i <= fgList.Rows.Count - 1; i++)
            {
                dtRow = dtInform.NewRow();
                dtRow["f1"] = fgList[i, "AA"];
                dtRow["f2"] = fgList[i, "ClientName"];
                dtRow["f3"] = fgList[i, "ContractTitle"];
                dtRow["f4"] = fgList[i, "Code"];
                dtRow["f5"] = fgList[i, "Portfolio"];
                dtRow["f6"] = fgList[i, "Service_Title"];
                dtRow["f7"] = fgList[i, "Invoice_Num"];
                dtRow["f8"] = fgList[i, "DateIssued"];
                dtRow["f9"] = fgList[i, "PayAmount"];
                dtRow["f10"] = fgList[i, "InformingDate"];
                dtRow["f11"] = fgList[i, "InformingMethod"];
                dtRow["f12"] = fgList[i, "ClientData"];
                dtRow["f13"] = fgList[i, "FileName"];
                dtInform.Rows.Add(dtRow);
            }

            frmReports locReports = new frmReports();
            locReports.Params = Convert.ToDateTime(dFrom.Value).ToString("dd/MM/yyyy") + " - " + Convert.ToDateTime(dTo.Value).ToString("dd/MM/yyyy") + "~" +
                            cmbProviders.Text + "~" + ucCS.txtContractTitle.Text + "~" + Global.UserName + "~" + "Τιμολόγια εντολών μετατροπής νομίσματος";

            locReports.ReportID = 18;
            locReports.ShowResult = dtInform;
            locReports.Text = "Επίσημη Ενημέρωση Πελατών";
            locReports.Show();
        }
        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            DefineCommandsList();
        }
        private void DefineCommandsList()
        {
            fgList.Redraw = false;
            fgList.Rows.Count = 1;

            OrdersFX = new clsOrdersFX();
            OrdersFX.CommandType_ID = 1;
            OrdersFX.DateFrom = dFrom.Value;
            OrdersFX.DateTo = dTo.Value;
            OrdersFX.StockCompany_ID = Convert.ToInt32(cmbProviders.SelectedValue);
            OrdersFX.Code = lblCode.Text;
            OrdersFX.GetInvoicesList();

            i = 0;
            foreach (DataRow dtRow in OrdersFX.List.Rows)
            {
                if ((dtRow["Invoice_Num"] + "") != "")
                {
                    i = i + 1;
                    sConnectionMethod = "";
                    sConnectionData = "";
                    sRecipientName = "";

                    if (Convert.ToInt32(dtRow["ClientTipos"]) == 1)                                                                      //Fisiko Prosopo
                        sRecipientName = dtRow["ClientName"] + "";
                    else
                    {
                        if (Convert.ToInt32(dtRow["ClientTipos"]) == 2) sRecipientName = (dtRow["BornPlace"] + " ").Trim();              // Nomiko Prosopo 
                        else sRecipientName = (dtRow["SurnameFather"] + " ").Trim();          // KEM                            
                    }

                    if (Convert.ToInt32(dtRow["Contract_ConnectionMethod"]) == 1)
                    {
                        sConnectionMethod = "e-mail";
                        sConnectionData = dtRow["EMail"] + "";
                    }
                    else
                    {
                        sConnectionMethod = "Ταχ/κη αποστολή";
                        sConnectionData = sRecipientName + "\n" + dtRow["InvAddress"] + "\n" + dtRow["InvCity"] + " " + dtRow["InvZIP"];
                        if (dtRow["CountryTitleEn"] + "" != "Greece") sConnectionData = sConnectionData + " " + dtRow["CountryTitleEn"];
                    }

                    fgList.AddItem(false + "\t" + i + "\t" + dtRow["ClientName"] + "\t" + dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" +
                                  dtRow["ServiceTitle"] + "\t" + dtRow["Invoice_Num"] + "\t" + dtRow["Inv_DateIns"] + "\t" + dtRow["RTO_FeesAmountEUR"] + "\t" +
                                  dtRow["OfficialInformingDate"] + "\t" + sConnectionMethod + "\t" + sConnectionData + "\t" + dtRow["FileName"] + "\t" + dtRow["ID"] + "\t" +
                                  dtRow["Client_ID"] + "\t" + dtRow["Code"] + "_" + dtRow["Portfolio"] + "\t" + dtRow["Contract_ConnectionMethod"] + "\t" +
                                  dtRow["ClientTipos"] + "\t" + dtRow["StockCompany_ID"] + "\t" + dtRow["InvoiceTitle_ID"] + "\t" + "" + "\t" +
                                  dtRow["Contract_ID"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + dtRow["Contracts_Packages_ID"]);
                }
            }
            fgList.Redraw = true;
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
        protected void ucCustomerChoice_TextOfLabelChanged(object sender, EventArgs e)
        {
            stContractData = ucCS.SelectedContractData;
            lnkPelatis.Text = stContractData.ClientName;
            lblCode.Text = stContractData.Code;
            //lblProfitCenter.Text = stContractData.Portfolio;
            iClient_ID = stContractData.Client_ID;
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
        private string DailyEmailBody(int iClientTipos)
        {
            string sBody;
            sBody = "<html xmlns='http://www.w3.org/1999/xhtml'><head><title></title><meta http-equiv='Content-Type' content='text/html; charset=utf-8' />" +
            "<style>img.logo {height: 60%;width: 40%;}</style></head><body style='width: 800px;'><font face='verdana'><br/><br/><table><tr><td width=800>" +
            "<div style='height: 150px;'><img class='logo' src='http://www.hellasfin.gr/signs/images/Logo_500px.jpg' width='50%' alt='' /></div><br/><br/><br/><br/><br/><br/>" +
            "<div align='right'>ΘΕΣΣΑΛΟΝΙΚΗ " + DateTime.Now.ToString("dd/MM/yyyy") + "</div><br/><br/><br/><br/><br/><br/>" +
            "<center> ΘΕΜΑ: ΑΜΟΙΒΗ ΔΙΑΒΙΒΑΣΗΣ ΕΝΤΟΛΗΣ ΜΕΤΑΤΡΟΠΗΣ ΣΥΝΑΛΛΑΓΜΑΤΟΣ </center>" + "<br/><br/><br/><br/><br/>" +
            "Αγαπητέ πελάτη,<br/><br/><br/>" +
            "Στα πλαίσια ενημέρωσής σας, αποστέλλουμε:<br/><br/><br/>" +
            (iClientTipos == 2 ? " - Τιμολόγιο παροχής υπηρεσιών" : " - Απόδειξη παροχής υπηρεσιών") + "<br/>" +
            "<br/><br/><br/><br/><br/><br/><br/>" +
            "Στη διάθεσή σας για οποιαδήποτε διευκρίνιση.<br/><br/><br/>" +
            "<div align='left'>HELLASFIN Α.Ε.Π.Ε.Υ.</div><br/><div align='left'>Διεύθυνση Λειτουργικής Υποστήριξης και Εξυπηρέτησης Πελατών</div><br/>" + "<br/><br/><br/><br/><br/><br/><br/>" +
            "Παρακαλούμε για οποιαδήποτε διευκρίνηση επικοινωνήστε με το Λογιστήριο της εταιρείας στα τηλ. Θεσσαλονίκη: +30 2310 517800, " +
            "Αθήνα: +30 210 3387710, Κρήτη: +30 2810 343366<br/><br/>" +
            "*Tυχόν αντιρρήσεις σας σε οποιοδήποτε στοιχείο της παρούσας ενημέρωσης καλείστε να τις υποβάλλετε στην Εταιρία μας εγγράφως εντός δεκαπέντε (15) " +
            "ημερολογιακών ημερών, αλλιώς θεωρούμε ότι συμφωνείτε απολύτως.</td></tr></table><br/><br/></font></body></html>";
            return sBody;
        }
        private void mnuContractData_Click(object sender, EventArgs e)
        {
            frmContract locContract = new frmContract();
            locContract.Aktion = 1;
            locContract.Contract_ID = Convert.ToInt32(fgList[fgList.Row, 22]);
            locContract.Contract_Details_ID = Convert.ToInt32(fgList[fgList.Row, 23]);
            locContract.Contract_Packages_ID = Convert.ToInt32(fgList[fgList.Row, 24]);
            locContract.Client_ID = Convert.ToInt32(fgList[fgList.Row, 15]);
            locContract.ClientType = Convert.ToInt32(fgList[fgList.Row, 18]);
            locContract.ClientFullName = fgList[fgList.Row, 2] + "";
            locContract.RightsLevel = 1;                                          //iRightsLevel
            locContract.ShowDialog();
        }
        private void mnuClientData_Click(object sender, EventArgs e)
        {
            frmClientData locClientData = new frmClientData();
            locClientData.Client_ID = Convert.ToInt32(fgList[fgList.Row, 15]);
            locClientData.Text = Global.GetLabel("customer_information");
            locClientData.Show();
        }

        private void mnuCommandData_Click(object sender, EventArgs e)
        {
            frmOrderFX locOrderFX = new frmOrderFX();
            locOrderFX.Record_ID = Convert.ToInt32(fgList[fgList.Row, 14]);
            locOrderFX.Editable = 0;
            locOrderFX.Mode = 1;                                                            // 1 - from frmDailyFX, 2 - from frmAcc_InvoicesFX
            locOrderFX.ShowDialog();
        }

        private void mnuViewInvoice_Click(object sender, EventArgs e)
        {
            sTemp = fgList[fgList.Row, 3] + "";
            if (sTemp.Length > 0) Global.DMS_ShowFile("Customers/" + sTemp.Replace(".", "_") + "/Invoices", fgList[fgList.Row, 13].ToString());
        }
    }
}
