using C1.Win.C1FlexGrid;
using Core;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Contracts
{
    public partial class frmClientsRequests : Form
    {
        DataView dtView;
        DataRow[] foundRows;
        int i, j, iRow, iClient_ID, iRightsLevel, iRowColor = 0;
        string sTemp, sRowColor = "", sGroup_ID = "", sContractTemplate, sContractTitle, sFolderPath;
        string[] tokens;
        string[] sContractTypes = { "", "Ατομικός", "Κοινός" };
        string[] sStatus = { "Διαγράφθηκε", "Πρόχειρο", "Προς έλεγχο", "Έλεγχος 1", "Έλεγχος 2", "Έλεγχος Video Κλήσης", "Οριστικοποήθηκε", "Απορρίφθηκε" };
        string[] sVideoChatStatus = { "", "Αναμονή", "Ελέγχθηκε", "Οριστικοποήθηκε", "Απορρίφθηκε" };
        Point position;
        bool pMove;
        CellStyle[] csZtatus = new CellStyle[4];
        SortedList lstProblems = new SortedList();
        ClientRequest Client_Request;
        clsClientsRequests klsClientsRequests = new clsClientsRequests();
        clsClientsDocFiles klsClientDocFiles = new clsClientsDocFiles();

        #region --- Start functions -----------------------------------------------------------------------------
        public frmClientsRequests()
        {
            InitializeComponent();

            csZtatus[0] = fgList.Styles.Add("C0");
            csZtatus[0].BackColor = Color.Transparent;

            csZtatus[1] = fgList.Styles.Add("C1");
            csZtatus[1].BackColor = Color.Thistle;

            csZtatus[2] = fgList.Styles.Add("C2");
            csZtatus[2].BackColor = Color.Bisque;

            csZtatus[3] = fgList.Styles.Add("C3");
            csZtatus[3].BackColor = Color.LightSalmon;

            panCritiries.Top = 4;
            panCritiries.Left = 4;

            ucCS.StartInit(400, 240, 396, 20, 1);
            ucCS.Top = 8;
            ucCS.Left = 88;

            panNewContract.Left = (Screen.PrimaryScreen.Bounds.Width - panNewContract.Width) / 2;
            panNewContract.Top = (Screen.PrimaryScreen.Bounds.Height - panNewContract.Height) / 2;
        }

        private void frmClientsRequests_Load(object sender, EventArgs e)
        {
            dtView = Global.dtUserList.DefaultView;
            foundRows = Global.dtUserList.Select("ID = 0");
            foundRows[0]["Title"] = "";                                 // <---- ""

            //-------------- Define Advisors List ------------------
            dtView = Global.dtUserList.Copy().DefaultView;
            dtView.RowFilter = "Chief = 1 And Aktive = 1";
            cmbUser1.DataSource = dtView;
            cmbUser1.DisplayMember = "Title";
            cmbUser1.ValueMember = "ID";

            //-------------- Define RM List ------------------
            dtView = Global.dtUserList.Copy().DefaultView;
            dtView.RowFilter = "RM = 1 And Aktive = 1";
            cmbUser2.DataSource = dtView;
            cmbUser2.DisplayMember = "Title";
            cmbUser2.ValueMember = "ID";

            //-------------- Define Introducer List ------------------
            dtView = Global.dtUserList.Copy().DefaultView;
            dtView.RowFilter = "Introducer = 1 And Aktive = 1";
            cmbUser3.DataSource = dtView;
            cmbUser3.DisplayMember = "Title";
            cmbUser3.ValueMember = "ID";

            dtView = Global.dtUserList.DefaultView;
            foundRows = Global.dtUserList.Select("ID = 0");
            foundRows[0]["Title"] = "Όλοι";                        // <---- "Όλοι"

            dtView = dtView = Global.dtUserList.Copy().DefaultView;
            dtView.RowFilter = "ID = 0 And Aktive = 1";

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString(Global.GridStyle);
            fgList.DoubleClick += new System.EventHandler(fgList_DoubleClick);
            fgList.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.fgList_CellChanged);

            //------- fgOwners ----------------------------
            fgOwners.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgOwners.Styles.ParseString(Global.GridStyle);

            //------- fgClients ----------------------------
            fgClients.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgClients.Styles.ParseString(Global.GridStyle);
            fgClients.RowColChange += new EventHandler(fgClients_RowColChange);

            ucDC.DateFrom = DateTime.Now.AddDays(-7);
            ucDC.DateTo = DateTime.Now;

            DefineList();
        }
        protected override void OnResize(EventArgs e)
        {
            panCritiries.Width = this.Width - 24;

            fgList.Width = this.Width - 24;
            fgList.Height = this.Height - 146;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DefineList();
        }
        private void DefineList()
        {
            clsClientsRequests klsClientRequests = new clsClientsRequests();
            klsClientRequests.DateFrom = ucDC.DateFrom;
            klsClientRequests.DateTo = ucDC.DateTo;
            klsClientRequests.User_ID = Global.ClientsRequests_Status == 2 ? 0 : Global.User_ID;
            klsClientRequests.Client_ID = 0;
            klsClientRequests.GetList();
            dtView = klsClientRequests.List.DefaultView;
            dtView.Sort = "ID DESC";

            sGroup_ID = "";
            iRowColor = 0;
            i = 0;
            fgList.Redraw = false;
            fgList.Rows.Count = 1;
            foreach (DataRowView dtViewRow in dtView)
            {
                //                Request's Status > 0               Source_ID = 1 from MobiUser         user has Status >= 1 (kataxoron or elengktis)       Source_ID == 2 from Windows ISP                                                                                       is For Checking   
                if ((Convert.ToInt32(dtViewRow["Status"]) > 0) && (Convert.ToInt32(dtViewRow["Source_ID"]) == 1) && (Global.ClientsRequests_Status > 1) || (Convert.ToInt32(dtViewRow["Source_ID"]) == 2))
                {
                    if (Convert.ToInt32(dtViewRow["Action"]) == 0) sTemp = dtViewRow["ClientsRequest_Type_0"] + "";
                    if (Convert.ToInt32(dtViewRow["Action"]) == 1) sTemp = dtViewRow["ClientsRequest_Type_1"] + "";
                    if (Convert.ToInt32(dtViewRow["Action"]) == 2) sTemp = dtViewRow["ClientsRequest_Type_2"] + "";

                    if (dtViewRow["Group_ID"] + "" == "")
                    {
                        sGroup_ID = "";
                        sRowColor = "0";
                    }
                    else
                    {
                        if (dtViewRow["Group_ID"] + "" == sGroup_ID)
                            sRowColor = iRowColor.ToString();
                        else
                        {
                            sGroup_ID = dtViewRow["Group_ID"] + "";
                            if (iRowColor < 3) iRowColor = iRowColor + 1;
                            else iRowColor = 1;
                            sRowColor = iRowColor.ToString();
                        }
                    }

                    i = i + 1;
                    fgList.AddItem(i + "\t" + dtViewRow["ClientFullName"] + "\t" + dtViewRow["ID"] + "\t" + dtViewRow["DateIns"] + "\t" + sTemp + "\t" +
                                   sStatus[Convert.ToInt32(dtViewRow["Status"])] + "\t" + sVideoChatStatus[Convert.ToInt32(dtViewRow["VideoChatStatus"])] + "\t" + dtViewRow["Group_ID"] + "\t" +
                                   dtViewRow["RequestTipos"] + "\t" + dtViewRow["Client_ID"] + "\t" + dtViewRow["Source_ID"] + "\t" + dtViewRow["Action"] + "\t" + dtViewRow["Status"] + "\t" + dtViewRow["VideoChatStatus"] + "\t" +
                                   sRowColor + "\t" + dtViewRow["ServiceProvider_ID"] + "\t" + dtViewRow["Service_ID"] + "\t" + dtViewRow["Amount"] + "\t" + dtViewRow["Currency"] + "\t" + dtViewRow["ContractTitle"] + "\t" +
                                   dtViewRow["ContractTipos"] + "\t" + "" + "\t" + dtViewRow["ServiceProvider_Title"] + "\t" + dtViewRow["FinanceService_Title"] + "\t" + dtViewRow["Description"]);
                }

            }
            fgList.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Free;
            //fgList.Cols["VideoChat"].AllowMerging = true;
            fgList.Cols["Group_ID"].AllowMerging = true;
            fgList.Redraw = true;
        }
        private void fgList_CellChanged(object sender, RowColEventArgs e)
        {
            //if (bCheckList)
            if (e.Row > 0) fgList.Rows[e.Row].Style = csZtatus[Convert.ToInt32(fgList[e.Row, "RowColor"])];
        }
        private void fgList_DoubleClick(object sender, EventArgs e)
        {
            OpenRequests();
        }
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            OpenRequests();
        }
        private void OpenRequests()
        {
            if (fgList.Rows.Count > 1)
                if (fgList[fgList.Row, "Group_ID"] + "" == "")
                    EditRequest(Convert.ToInt32(fgList[fgList.Row, "ID"]), Convert.ToInt32(fgList[fgList.Row, "RequestTipos"]), fgList[fgList.Row, "Group_ID"] + "", Convert.ToInt32(fgList[fgList.Row, "VideoChatStatus"]));
                else
                    EditRequest(Convert.ToInt32(fgList[fgList.Row, "ID"]), Convert.ToInt32(fgList[fgList.Row, "RequestTipos"]), fgList[fgList.Row, "Group_ID"] + "", Convert.ToInt32(fgList[fgList.Row, "VideoChatStatus"]));

        }
        private void tslProsopikaStoixeia_Click(object sender, EventArgs e)
        {
            EditRequest(0, 0, "", 0);
        }
        private void tsbDelete_Click(object sender, EventArgs e)
        {

        }
        private void EditRequest(int iRequest_ID, int iRequestType, string sGroup_ID, int iVideoChatStatus)
        {
            int i = 0;
            i = fgList.Row;

            switch (iRequestType)
            {
                case 0:                                                  // 0 - Νέα αίτημα ακόμη άγνωστου τύπου
                case 1:                                                  // 1 - Αίτημα αλλαγής ταυτότητας
                case 2:                                                  // 2 - Αίτημα αλλαγής κινητού τηλεφώνου 
                case 3:                                                  // 3 - Αίτημα αλλαγής σταθερού τηλεφώνου 
                case 4:                                                  // 4 - Αίτημα αλλαγής email επικοινωνίας  
                case 5:                                                  // 5 - Αίτημα αλλαγής διεύθυνσης κατοικίας
                case 6:                                                  // 6 - Αίτημα αλλαγής ΑΦΜ
                case 7:                                                  // 7 - Αίτημα προσθήκης εκκαθαριστικού
                case 8:                                                  // 8 - Αίτημα αλλαγής επαγγέλματος
                case 9:                                                  // 9 - Αίτημα αλλαγής xώρας φορολόγησης
                case 10:                                                 // 10 - Αίτημα αλλαγής ειδικής κατηγορίας προσώπου
                case 11:                                                 // 11 - Αίτημα προσθήκης/διαγραφής τραπεζικού λογαριασμού
                case 12:                                                 // 12 - Αίτημα προσθήκης νέου συνδεδεμένου ανήλικου προσώπου
                case 13:                                                 // 13 - Αίτημα αλλαγής  W8 ΒΕΝ
                case 14:                                                 // 14 - Αίτημα αλλαγής  Διαβατηρίου
                case 15:                                                 // 15 - Αίτημα αλλαγής Χρηματιστηριακής Μερίδας
                case 16:                                                 // 16 - Αίτημα αλλαγής  Λογαριασμόυ Αξιών
                case 17:                                                 // 17 - Αίτημα αλλαγής  ΑΜΚΑ

                    frmClientsRequests_PersonalData locClientsRequests_PersonalData = new frmClientsRequests_PersonalData();
                    locClientsRequests_PersonalData.Request_ID = iRequest_ID;
                    locClientsRequests_PersonalData.VideoChatStatus = iVideoChatStatus;
                    locClientsRequests_PersonalData.Group_ID = sGroup_ID;
                    locClientsRequests_PersonalData.RightsLevel = iRightsLevel;
                    locClientsRequests_PersonalData.ShowDialog();
                    break;
                case 100:                                                                         // 1 - Άνοιγμα Νέου Επενδυτικού Λογαριασμού
                    lblClientName.Text = fgList[i, "ClientName"] + "";
                    lblContractType.Text = sContractTypes[Convert.ToInt32(fgList[i, "ContractTipos"])];
                    lblAmount.Text = fgList[i, "Amount"] + "";
                    lblAmountCurr.Text = fgList[i, "Currency"] + "";
                    lblServiceProvider.Text = fgList[i, "ServiceProvider_Title"] + "";
                    lblService.Text = fgList[i, "Service_Title"] + "";

                    txtContractTitle.Text = fgList[i, "ContractTitle"] + "";
                    sFolderPath = fgList[i, "FolderPath"] + "";
                    sContractTitle = "Portfolio_" + fgList[i, "Source_ID"];
                    iClient_ID = Convert.ToInt32(fgList[i, "Client_ID"]);

                    if (Convert.ToInt32(fgList[i, "ContractTipos"]) == 1)
                    {
                        lblOwners.Visible = false;
                        fgOwners.Visible = false;
                    }
                    else
                    {
                        clsPreContracts_Clients PreContract_Clients = new clsPreContracts_Clients();
                        PreContract_Clients.PreContract_ID = Convert.ToInt32(fgList[fgList.Row, "Source_ID"]);
                        PreContract_Clients.GetList();

                        j = 0;
                        fgOwners.Redraw = false;
                        fgOwners.Rows.Count = 1;

                        fgClients.Redraw = false;
                        fgClients.Rows.Count = 1;
                        fgClients.AddItem((j + 1) + "\t" + lblClientName.Text + "\t" + "Name" + "\t" + "DoB" + "\t" + "ID");

                        foreach (DataRow dtRow in PreContract_Clients.List.Rows)
                        {
                            j = j + 1;
                            fgOwners.AddItem(j + "\t" + dtRow["Surname"] + "\t" + dtRow["Firstname"] + "\t" + dtRow["DoB"] + "\t" + dtRow["AFM"] + "\t" + "" + "\t" + dtRow["ID"]);
                            fgClients.AddItem((j + 1) + "\t" + dtRow["Surname"] + "\t" + dtRow["Firstname"] + "\t" + dtRow["DoB"] + "\t" + dtRow["ID"]);
                        }
                        fgOwners.Redraw = true;


                        lblOwners.Visible = true;
                        fgOwners.Visible = true;
                        fgClients.Redraw = true;
                    }

                    klsClientDocFiles = new clsClientsDocFiles();
                    klsClientDocFiles.Client_ID = Convert.ToInt32(fgList[i, "Client_ID"]);
                    klsClientDocFiles.PreContract_ID = Convert.ToInt32(fgList[i, "Source_ID"]);
                    klsClientDocFiles.Contract_ID = 0;
                    klsClientDocFiles.DocTypes = 0;
                    klsClientDocFiles.GetList();

                    fgDocs.Redraw = false;
                    fgDocs.Rows.Count = 1;
                    foreach (DataRow dtRow in klsClientDocFiles.List.Rows)
                    {
                        i = i + 1;
                        fgDocs.AddItem(dtRow["Tipos"] + "\t" + dtRow["FileName"] + "\t" + dtRow["DateIns"] + "\t" + dtRow["ID"]);
                    }
                    fgDocs.Redraw = true;
                    panNewContract.Visible = true;

                    break;


            }
            iRow = fgList.Row;
            DefineList();
            if (fgList.Rows.Count == 1) fgList.Row = 0;
            else if (fgList.Rows.Count - 1 > iRow) fgList.Row = iRow;
            else fgList.Row = fgList.Rows.Count - 1;
        }
        #endregion
        #region --- NewContract request functions -----------------------------------------------------------------------------
        private void picClose_NewContract_Click(object sender, EventArgs e)
        {
            panNewContract.Visible = false;
        }
        private void btnCreateContract_Click(object sender, EventArgs e)
        {
            string sTemplate_FullPath = @Application.StartupPath;
            string sTarget_FullPath = Global.DMSMapDrive + "\\Customers\\" + sFolderPath;     //  @Global.DMSMapDrive + "/Customers/" + sFolderPath;
            var WordApp = new Microsoft.Office.Interop.Word.Application();
            var curDoc = new Microsoft.Office.Interop.Word.Document();

            i = fgList.Row;

            switch (Convert.ToInt32(fgList[i, "ServiceProvider_ID"]))
            {
                case 7:              // CreditSwiss
                    break;
                case 9:              // HellasFin
                    switch (Convert.ToInt32(fgList[i, "Service_ID"]))
                    {
                        case 1:              // RTO 
                            sContractTemplate = "ContractTemplate_HF_RTO.docx";
                            break;
                        case 2:              // Advisory
                            sContractTemplate = "ContractTemplate_HF_Advisory.docx";
                            break;
                        case 3:              // Discretionary
                            sContractTemplate = "ContractTemplate_HF_Discret.docx";
                            break;
                    }
                    break;
            }

            Global.DisconnectDrive(Global.DMSMapDrive);
            Global.MapDrive(Global.DMSMapDrive, Global.DMSMapDriveAddress, Global.FTP_Username, Global.FTP_Password);
            sTemp = sTarget_FullPath + "\\" + sContractTitle + ".docx";
            if (System.IO.File.Exists(sTemp)) System.IO.File.Delete(sTemp);
            System.IO.File.Copy(@sTemplate_FullPath + "/Templates/" + sContractTemplate, sTemp);
            curDoc = WordApp.Documents.Open(sTemp);

            //--- Edit Template Content -----------------------------------------------
            sTemp = "";
            if (txtCode.Text != "" || txtPortfolio.Text != "") sTemp = txtCode.Text + " / " + txtPortfolio.Text;
            curDoc.Content.Find.Execute(FindText: "{code_portfolio}", ReplaceWith: sTemp, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
            curDoc.Content.Find.Execute(FindText: "{client_name}", ReplaceWith: fgList[i, "ClientName"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
            curDoc.Content.Find.Execute(FindText: "{user1_name}", ReplaceWith: cmbUser1.Text, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
            curDoc.Content.Find.Execute(FindText: "{user2_name}", ReplaceWith: cmbUser3.Text, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
            curDoc.Content.Find.Execute(FindText: "{user3_name}", ReplaceWith: cmbUser2.Text, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
            //-------------------------------------------------------------------------

            curDoc.SaveAs2(sTarget_FullPath + "/" + sContractTitle + ".pdf", Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF);

            lblContractFile.Text = sContractTitle + ".pdf";

            Global.DisconnectDrive(Global.DMSMapDrive);

            WordApp.Quit();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        private void picShowContract_Click(object sender, EventArgs e)
        {
            if (lblContractFile.Text.Trim() != "")
                Global.DMS_ShowFile("Customers/" + sFolderPath, lblContractFile.Text);     // is DMS file, so show it into Web mode
        }
        private void fgClients_RowColChange(object sender, EventArgs e)
        {
            if (fgClients.Row > 0)
            {
                //ShowDetails();
                //fgList.Focus();
            }
        }
        private void btnSendContract_Click(object sender, EventArgs e)
        {

        }
        #endregion
        private void panData_MouseDown(object sender, MouseEventArgs e)
        {
            this.position = e.Location;
            this.pMove = true;
        }
        private void panData_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.pMove == true)
                {
                    this.panNewContract.Location = new Point(this.panNewContract.Location.X + e.X - this.position.X, this.panNewContract.Location.Y + e.Y - this.position.Y);
                }
            }
        }
        private void panData_MouseUp(object sender, MouseEventArgs e)
        {
            this.pMove = false;
        }

        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
    }
    public class ClientRequest
    {
        public int action { get; set; }
        public int file_id { get; set; }
        public int oldfile_id { get; set; }
        public string videochat { get; set; }
    }
}
