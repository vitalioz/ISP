using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using System.Drawing;
using C1.Win.C1FlexGrid;
using Core;

namespace Transactions
{
    public partial class frmInvestProposalsList : Form
    {
        DataView dtView;
        int i, iClient_ID, iShare_ID, iRightsLevel;
        string sTemp, sExtra;
        string[] sStatus = { "", "Νέα συμβουλή", "Σκεπτικός", "Αναμονή", "Μην αποδοχή", "Αποδοχή", "Άκυρο"};
        bool bCheckList;
        CellStyle csCancel;
        CellStyle[] csStatus = new CellStyle[7];
        CellStyle[] csZtatus = new CellStyle[4];
        clsServerJobs ServerJobs = new clsServerJobs();
        clsInvestIdees klsInvestIdees = new clsInvestIdees();
        clsInvestIdees InvestProposals = new clsInvestIdees();
        public frmInvestProposalsList()
        {
            InitializeComponent();

            iShare_ID = 0;
        }

        #region --- Start functions ---------------------------------------------------
        private void frmInvestProposalsList_Load(object sender, EventArgs e)
        {
            csCancel = fgList.Styles.Add("Cancelled");
            csCancel.ForeColor = Color.Red;

            csZtatus[0] = fgProposals.Styles.Add("New");
            csZtatus[0].BackColor = Color.Transparent;

            csZtatus[1] = fgProposals.Styles.Add("WaitSend");
            csZtatus[1].BackColor = Color.Yellow;

            csZtatus[2] = fgProposals.Styles.Add("Sent");
            csZtatus[2].BackColor = Color.LightGreen;

            csZtatus[3] = fgProposals.Styles.Add("NotSent");
            csZtatus[3].BackColor = Color.LightCoral;


            csStatus[1] = fgProposals.Styles.Add("New");
            csStatus[1].ForeColor = Color.Black;

            csStatus[2] = fgProposals.Styles.Add("Think");
            csStatus[2].BackColor = Color.Yellow;

            csStatus[3] = fgProposals.Styles.Add("Wait");
            csStatus[3].BackColor = Color.Thistle;

            csStatus[4] = fgProposals.Styles.Add("Not");
            csStatus[4].BackColor = Color.LightCoral;

            csStatus[5] = fgProposals.Styles.Add("Yes");
            csStatus[5].BackColor = Color.LightGreen;

            csStatus[6] = fgProposals.Styles.Add("Cancelled");
            csStatus[6].BackColor = Color.Orange;

            ucCS.StartInit(700, 400, 540, 20, 1);
            ucCS.TextOfLabelChanged += new EventHandler(ucCS_TextChanged);
            ucCS.Filters = "Status = 1";
            ucCS.ListType = 1;

            ucPS.StartInit(700, 400, 200, 20, 1);
            ucPS.TextOfLabelChanged += new EventHandler(ucPS_TextChanged);
            ucPS.Mode = 1;
            ucPS.ListType = 1;
            ucPS.Filters = "Aktive >= 1 ";
            ucPS.ShowNonAccord = true;                                                          // Show NonAccordable products (oxi katallila) with red Background
            ucPS.ShowCancelled = false;                                                         // Don't show cancelled products
            ucPS.Visible = true;

            //-------------- Define Advisors List ------------------   
            dtView = Global.dtUserList.Copy().DefaultView;
            dtView.RowFilter = "Chief = 1";
            cmbAdvisors.DataSource = dtView;
            cmbAdvisors.DisplayMember = "Title";
            cmbAdvisors.ValueMember = "ID";

            //-------------- Define Users List ------------------   
            dtView = Global.dtUserList.Copy().DefaultView;
            dtView.RowFilter = "Aktive = 1";
            cmbUsers.DataSource = dtView;
            cmbUsers.DisplayMember = "Title";
            cmbUsers.ValueMember = "ID";

            if (Global.Chief == 1)
            {
                cmbAdvisors.SelectedValue = Global.User_ID;
                if (Global.IsNumeric(sExtra))
                {
                    if (Convert.ToInt32(sExtra) > 0) cmbAdvisors.Enabled = false;
                    else cmbAdvisors.Enabled = true;
                }

                cmbUsers.SelectedValue = 0;
                cmbUsers.Enabled = true;
            }
            else { 
                 if (Global.IsNumeric(sExtra)) cmbAdvisors.SelectedValue = Convert.ToInt32(sExtra);
                 cmbAdvisors.Enabled = false;

                cmbUsers.SelectedValue = Global.User_ID;
                cmbUsers.Enabled = false;
            }

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            //fgList.MouseDown += new MouseEventHandler(fgList_MouseDown);
            fgList.RowColChange += new EventHandler(fgList_RowColChange);
            fgList.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgList_BeforeEdit);
            fgList.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgList_AfterEdit);
            fgList.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.fgList_CellChanged);
            fgList.DoubleClick += new System.EventHandler(fgList_DoubleClick);

            //------- fgProposals ----------------------------
            fgProposals.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgProposals.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgProposals.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.fgProposals_CellChanged);
            fgProposals.MouseDown += new MouseEventHandler(fgProposals_MouseDown);
            //fgProposals.DoubleClick += new System.EventHandler(fgProposals_DoubleClick);


            dSend.Value = DateTime.Now;
            dSendFrom.Value = DateTime.Now.AddDays(-14);
            dSendTo.Value = DateTime.Now;
            chkNew.Checked = true;
            chkThink.Checked = true;
            chkNot.Checked = true;
            chkYes.Checked = true;
            chkCancel.Checked = true;

            bCheckList = true;

            DefineList();
            DefinePsoposalsList();
        }
        protected override void OnResize(EventArgs e)
        {
            tabInvestIdees.Height = this.Height - 48;
            tabInvestIdees.Width = this.Width - 26;
            fgList.Width = tabInvestIdees.Width - 24;
            fgList.Height = tabInvestIdees.Height - 100;
            fgProposals.Width = tabInvestIdees.Width - 24;
            fgProposals.Height = tabInvestIdees.Height - 150;
            ucCS.txtContractTitle.Width = 400;
        }
        private void DefineList()
        {
            if (bCheckList) {
                fgList.Redraw = false;
                fgList.Rows.Count = 1;

                klsInvestIdees = new clsInvestIdees();
                switch (Global.ClientsFilter_ID)
                {
                    case 1:                                                                        // 1 - Kanenan
                        klsInvestIdees.Advisor_ID = -999;
                        klsInvestIdees.User_ID = 0;
                        klsInvestIdees.Client_ID = 0;
                        klsInvestIdees.Contract_ID = 0;
                        klsInvestIdees.Division_ID = Global.Division;
                        break;
                    case 2:                                                                        // 2 - Oloi
                        klsInvestIdees.Advisor_ID = 0;
                        klsInvestIdees.User_ID = 0;
                        klsInvestIdees.Client_ID = 0;
                        klsInvestIdees.Contract_ID = 0;
                        klsInvestIdees.Division_ID = 0;
                        break;
                    case 3:                                                                         // 3 - Syndedemenoi pelates
                        klsInvestIdees.Advisor_ID = Convert.ToInt32(cmbAdvisors.SelectedValue);
                        klsInvestIdees.User_ID = Convert.ToInt32(cmbUsers.SelectedValue);
                        klsInvestIdees.Client_ID = 0;
                        klsInvestIdees.Contract_ID = 0;
                        klsInvestIdees.Division_ID = 0;
                        break;
                    case 4:                                                                         // 4 - ClientsFilter - Ypokatastima
                        klsInvestIdees.Advisor_ID = 0;
                        klsInvestIdees.User_ID = 0;
                        klsInvestIdees.Client_ID = 0;
                        klsInvestIdees.Contract_ID = 0;
                        klsInvestIdees.Division_ID = Global.Division;
                        break;
                }
                klsInvestIdees.SentDate = dSend.Value;
                klsInvestIdees.GetList();
                foreach (DataRow dtRow in klsInvestIdees.List.Rows)
                {
                    if (Convert.ToDateTime(dSend.Value).ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy") ||
                        Convert.ToDateTime(dSend.Value).ToString("dd/MM/yyyy") == Convert.ToDateTime(dtRow["AktionDate"]).ToString("dd/MM/yyyy"))
                    {
                        //--- Proposal Status : 0 - new (wasn't sent yet - white),  1 - wait(was sent from user, but not from server -yellow), 2 - sent from server (green), 3 - can't send (red)
                        fgList.AddItem(false + "\t" + dtRow["Status_Text"] + "\t" + dtRow["ID"] + "\t" + Convert.ToDateTime(dtRow["AktionDate"]).ToString("dd/MM/yyyy") + "\t" +
                                      dtRow["ClientFullName"] + "\t" + dtRow["ContractTitle"] + "\t" + dtRow["Products"] + "\t" + dtRow["StatementFile"] + "\t" + dtRow["ProposalPDFile"] + "\t" +
                                      dtRow["EMail"] + "\t" + dtRow["Mobile"] + "\t" + dtRow["InformationMethods_Title"] + "\t" + dtRow["SentDate"] + "\t" +
                                      dtRow["RecievedDate"] + "\t" + dtRow["RTODate"] + "\t" + dtRow["AdvisorName"] + "\t" + dtRow["UserName"] + "\t" + dtRow["ID"] + "\t" +
                                      dtRow["Status"] + "\t" + dtRow["CC_Email"] + "\t" + dtRow["LineStatus"]);
                    }
                }
                fgList.Redraw = true;
            }

        }
        private void dSendFrom_ValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) DefinePsoposalsList();
        }
        private void dSendTo_ValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) DefinePsoposalsList();
        }
        private void chkNew_CheckedChanged(object sender, EventArgs e)
        {
            if (bCheckList) DefinePsoposalsList();
        }
        private void chkThink_CheckedChanged(object sender, EventArgs e)
        {
            if (bCheckList) DefinePsoposalsList();
        }
        private void chkWait_CheckedChanged(object sender, EventArgs e)
        {
            if (bCheckList) DefinePsoposalsList();
        }
        private void chkNot_CheckedChanged(object sender, EventArgs e)
        {
            if (bCheckList) DefinePsoposalsList();
        }
        private void chkYes_CheckedChanged(object sender, EventArgs e)
        {
            if (bCheckList) DefinePsoposalsList();
        }
        private void chkCancel_CheckedChanged(object sender, EventArgs e)
        {
            if (bCheckList) DefinePsoposalsList();
        }
        private void picEmptyClient_Click(object sender, EventArgs e)
        {
            iClient_ID = 0;
            ucCS.ShowClientsList = false;
            ucCS.txtContractTitle.Text = "";          // client name
            ucCS.ShowClientsList = true;
            DefinePsoposalsList();
        }
        private void picEmptyShare_Click(object sender, EventArgs e)
        {
            iShare_ID = 0;
            ucPS.ShowProductsList = false;
            ucPS.txtShareTitle.Text = "";             // product name
            ucPS.ShowProductsList = true;
            lblShareTitle.Text = "";
            lblISIN.Text = "";
            DefinePsoposalsList();
        }
        #endregion
        #region --- fgList functions -------------------------------------------------------------
        private void dSend_ValueChanged(object sender, EventArgs e)
        {
            if (dSend.Value.Date == DateTime.Now.Date) tsbSend.Enabled = true;
            else tsbSend.Enabled = false;

            DefineList();
        }
        private void cmbAdvisors_SelectedValueChanged(object sender, EventArgs e)
        {
            DefineList();
        }
        private void cmbUsers_SelectedValueChanged(object sender, EventArgs e)
        {
            DefineList();
        }
        private void chkSend_CheckedChanged(object sender, EventArgs e)
        {
            for (i = 1; i <= fgList.Rows.Count - 1; i++)
            if ( ((fgList[i, "SentDate"]+"") == "") && ((fgList[i, "CommunicationMethod"]+"") == "E-mail") && (Convert.ToInt32(fgList[i, "Status"]) == 0))
                  fgList[i, 0] = chkSend.Checked;
            else  fgList[i, 0] = false;
        }
        private void tsbAddProposal_Click(object sender, EventArgs e)
        {
            frmInvestProposal locInvestProposal = new frmInvestProposal();
            locInvestProposal.Aktion = 0;                                           // 0 - Add
            locInvestProposal.II_ID = 0;
            locInvestProposal.dSend.Value = dSend.Value;
            locInvestProposal.txtAUM.Text = "0";
            locInvestProposal.ShowDialog();
            if (locInvestProposal.Aktion == 1)  DefineList();
        }
        private void fgList_DoubleClick(object sender, EventArgs e)
        {
            EditRecord();
        }
        private void tsbEditProposal_Click(object sender, EventArgs e)
        {
            EditRecord();
        }
        private void EditRecord()
        {
            if (fgList.Row > 0)
            {
                frmInvestProposal locInvestProposal = new frmInvestProposal();
                locInvestProposal.Aktion = 1;                                           // 1 - Edit
                locInvestProposal.II_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                //locInvestProposal.ucCS.txtContractTitle.Width = 500;
                locInvestProposal.ShowDialog();
                if (locInvestProposal.Aktion == 1)
                    DefineList();
            }
        }
        private void tsbCancelProposal_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 0)
                if (Convert.ToInt32(fgList[fgList.Row, "Status"]) == 0)                                                       // Status = 0

                    if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε ακύρωση εγγραφής.\nΕίστε σίγουρος για αυτό;",
                           Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes) {
                        klsInvestIdees = new clsInvestIdees();
                        klsInvestIdees.Record_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                        klsInvestIdees.GetRecord();
                        klsInvestIdees.SentDate = DateTime.Now;
                        klsInvestIdees.Status = 4;                                                                            // 4 - Cancel
                        klsInvestIdees.EditRecord();

                        fgList[fgList.Row, "Status"] = 4;
                    }
        }

        private void tsbSend_Click(object sender, EventArgs e)
        {
            string  sTemp2 = "";

            sTemp = "";

            for (i = 1; i <= fgList.Rows.Count - 1; i++) {
                if (Convert.ToBoolean(fgList[i, 0]) && ((fgList[i, "CommunicationMethod"]+"") == "E-mail") && Convert.ToInt32(fgList[i, "Status"]) == 0) {

                    if ((fgList[i, "EMail"]+"") != "") {
                        if ((fgList[i, "StatementFile"]+"") != "" && (fgList[i, "PDFFile"]+"") != "") {    // all neccecary file exists

                            fgList[i, 0] = false;
                            fgList[i, 1] = "Αναμονή αποστολής";
                            fgList[i, "SentDate"] = DateTime.Now.ToString("dd/MM/yy HH:mm:ss");
                            fgList[i, "Status"] = 1;

                            klsInvestIdees = new clsInvestIdees();
                            klsInvestIdees.Record_ID = Convert.ToInt32(fgList[i, "ID"]);
                            klsInvestIdees.GetRecord();
                            klsInvestIdees.SentDate = DateTime.Now;
                            klsInvestIdees.Status = 1;
                            klsInvestIdees.EditRecord();

                            clsServerJobs ServerJobs = new clsServerJobs();
                            ServerJobs.JobType_ID = 44;                                             // 44  - send e-mail from Investment Proposal Params: II_ID
                            ServerJobs.Source_ID = 0;
                            ServerJobs.Parameters = "{'ii_id': '" + fgList[i, "ID"] + "'}";
                            ServerJobs.DateStart = DateTime.Now;
                            ServerJobs.DateFinish = System.Convert.ToDateTime("1900/01/01");
                            ServerJobs.PubKey = "";
                            ServerJobs.PrvKey = "";
                            ServerJobs.Attempt = 0;
                            ServerJobs.Status = 0;
                            ServerJobs.InsertRecord();
                        }
                        else 
                            if (sTemp.Length == 0) sTemp = fgList[i, "ID"] + "";
                            else sTemp = sTemp + ", " + fgList[i, "ID"];
                    }
                    else 
                        if (sTemp2.Length == 0) sTemp2 = fgList[i, "ID"] + "";
                        else    sTemp2 = sTemp2 + ", " + fgList[i, "ID"];
                }
            }

            DefineList();

            if (sTemp.Length > 0)    MessageBox.Show("Οι Επενδυτικές Προτάσεις με αριθμό\n" + sTemp +
                                "\n δεν μπορούν να σταλούν επειδή δεν έχουν όλα απαραίτητα αρχεία", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            if (sTemp2.Length > 0)
                MessageBox.Show("Οι Επενδυτικές Προτάσεις με αριθμό \n" + sTemp2 +
                                "\n δεν μπορούν να σταλούν επειδή δεν έχουν e-mail", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
        }

        private void tsbRTO_Click(object sender, EventArgs e)
        {
            i = fgList.Row;
            if ((fgList[i, "StatementFile"] + "") != "" && (fgList[i, "PDFFile"] + "") != "")  {                

                klsInvestIdees = new clsInvestIdees();
                klsInvestIdees.Record_ID = Convert.ToInt32(fgList[i, "ID"]);
                klsInvestIdees.GetRecord();
                klsInvestIdees.RTODate = DateTime.Now;
                klsInvestIdees.EditRecord();

                fgList[i, "RTOTime"] = DateTime.Now.ToString("dd/MM/yy HH:mm:ss");
            }
            else MessageBox.Show("Επενδυτικές Προτάσεις με αριθμό " + fgList[i, "II_ID"] + " \n" +
                             "\n δεν μπορεί να σταλεί επειδή δεν έχει όλα απαραίτητα αρχεία", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            DefineList();
        }
        private void fgList_RowColChange(object sender, EventArgs e)
        {
            if (bCheckList) {
                if (fgList.Row > 0) {
                    if ( ((fgList[fgList.Row, "CommunicationMethod"] +"") == "Τηλέφωνο" || Convert.ToInt32(fgList[fgList.Row, "Status"]) == 2) &&
                          (fgList[fgList.Row, "RTOTime"] +"") == "")  tsbRTO.Enabled = true;
                   else                                               tsbRTO.Enabled = false;
                }
            }
        }    
        private void fgList_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (bCheckList) {
                if (e.Col == 0) e.Cancel = false;
                else e.Cancel = true;
            }
        }
        private void fgList_AfterEdit(object sender, RowColEventArgs e)
        {
            if (bCheckList) {
                if (e.Col == 0) e.Cancel = false;
                else e.Cancel = true;
            }
        }
        private void fgList_CellChanged(object sender, RowColEventArgs e)
        {
            if (bCheckList)
                if (Convert.ToInt32(fgList[e.Row, "Status"]) == 4) fgList.Rows[e.Row].Style = csCancel;
                else
                   if (e.Row > 0) fgList.Rows[e.Row].Style = csZtatus[Convert.ToInt32(fgList[e.Row, "Status"])];
        }
        private void fgProposals_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                fgProposals.ContextMenuStrip = mnuContextActions;
                fgProposals.Row = fgProposals.MouseRow;
            }
        }
        #endregion
        #region --- Inset Proposals List -------------------------------------------------
        private void DefinePsoposalsList()
        {
            if (bCheckList) {       
                InvestProposals = new clsInvestIdees();

                switch (Global.ClientsFilter_ID)
                {
                    case 1:                                                                        // 1 - Kanenan
                        InvestProposals.Advisor_ID = -999;
                        InvestProposals.User_ID = 0;
                        break;
                    case 2:                                                                        // 2 - Oloi
                        InvestProposals.Advisor_ID = 0;
                        InvestProposals.User_ID = 0;
                        break;
                    case 3:                                                                         // 3 - Syndedemenoi pelates
                        InvestProposals.Advisor_ID = Convert.ToInt32(cmbAdvisors.SelectedValue);
                        InvestProposals.User_ID = Convert.ToInt32(cmbUsers.SelectedValue);
                        break;
                    case 4:                                                                         // 4 - ClientsFilter - Ypokatastima
                        InvestProposals.Advisor_ID = 0;
                        InvestProposals.User_ID = 0;
                        break;
                }

                InvestProposals.DateFrom = dSendFrom.Value;
                InvestProposals.DateTo = dSendTo.Value;
                InvestProposals.GetProposalsList();

                fgProposals.Redraw = false;
                fgProposals.Rows.Count = 1;

                foreach (DataRow dtRow in InvestProposals.List.Rows) {
                    if(((chkNew.Checked && Convert.ToInt32(dtRow["Status_ID"]) == 1) || (chkThink.Checked && Convert.ToInt32(dtRow["Status_ID"]) == 2) ||
                        (chkWait.Checked && Convert.ToInt32(dtRow["Status_ID"]) == 3) || (chkNot.Checked && Convert.ToInt32(dtRow["Status_ID"]) == 4) || 
                        (chkYes.Checked && Convert.ToInt32(dtRow["Status_ID"]) == 5) || (chkCancel.Checked && Convert.ToInt32(dtRow["Status_ID"]) == 6)) &&
                        (iClient_ID == 0 || Convert.ToInt32(dtRow["Client_ID"]) == iClient_ID) && (iShare_ID == 0 || Convert.ToInt32(dtRow["Share_ID"]) == iShare_ID))

                        fgProposals.AddItem(dtRow["ID"] + "\t" + dtRow["Status"] + "\t" + dtRow["DateSent"] + "\t" + dtRow["ClientName"] + "\t" +
                            dtRow["Aktion"] + "\t" + dtRow["ProductType"] + "\t" + dtRow["Title"] + "\t" + dtRow["Code"] + "\t" + dtRow["Code2"] + "\t" +
                            dtRow["ISIN"] + "\t" + dtRow["Price"] + "\t" + dtRow["Quantity"] + "\t" + dtRow["Constant"] + "\t" + dtRow["RTO_Notes"] + "\t" +
                            dtRow["RecieveDate"] + "\t" + dtRow["SentDate"] + "\t" + dtRow["ExecuteDate"] + "\t" + dtRow["RealPrice"] + "\t" +
                            dtRow["RealQuantity"] + "\t" + dtRow["IIC_ID"] + "\t" + dtRow["Status_ID"] + "\t" + dtRow["RecieveVoicePath"]);

                }
                fgProposals.Redraw = true;
            }
        }
        private void fgProposals_CellChanged(object sender, RowColEventArgs e)
        {
            if (bCheckList)
                if (e.Row > 0)
                    fgProposals.Rows[e.Row].Style = csStatus[Convert.ToInt32(fgProposals[e.Row, "Status"])];
        }
        #endregion
        private void tsbRefreshProposals_Click(object sender, EventArgs e)
        {
            DefinePsoposalsList();
        }
        private void tsbPlayRecievedFile_Click(object sender, EventArgs e)
        {
            if (fgProposals[fgProposals.Row, "RecieveVoicePath"] + "" != "") {
                sTemp = fgProposals[fgProposals.Row, "ClientName"] + "";
                Global.DMS_ShowFile("/Customers/" + sTemp.Replace(".", "_") + "/InvestProposals/" + fgProposals[fgProposals.Row, 0], fgProposals[fgProposals.Row, "RecieveVoicePath"]+"");
            }
        }
        private void tsbExcel_Click(object sender, EventArgs e)
        {
            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            int j = 0;
            var EXL = new Microsoft.Office.Interop.Excel.Application();
            var oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            // System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("el-GR");
            EXL.Workbooks.Add();
            EXL.ScreenUpdating = false;
            EXL.Caption = Global.AppTitle;

            var loopTo = fgList.Rows.Count - 1;
            for (this.i = 0; this.i <= loopTo; this.i++)
            {
                j = j + 1;
                EXL.Cells[i + 1, 1].Value = fgList[i, 1];
                EXL.Cells[i + 1, 2].Value = fgList[i, 2];
                EXL.Cells[i + 1, 3].Value = fgList[i, 3];
                EXL.Cells[i + 1, 4].Value = fgList[i, 4];
                EXL.Cells[i + 1, 5].Value = fgList[i, 5];
                EXL.Cells[i + 1, 6].Value = fgList[i, 6];
                EXL.Cells[i + 1, 7].Value = fgList[i, 7];
                EXL.Cells[i + 1, 8].Value = fgList[i, 8];
                EXL.Cells[i + 1, 9].Value = fgList[i, 9];
                EXL.Cells[i + 1, 10].Value = fgList[i, 10];
                EXL.Cells[i + 1, 11].Value = fgList[i, 11];
                EXL.Cells[i + 1, 12].Value = fgList[i, 12];
                EXL.Cells[i + 1, 13].Value = fgList[i, 13];
                EXL.Cells[i + 1, 14].Value = fgList[i, 14];
                EXL.Cells[i + 1, 15].Value = fgList[i, 15];
               
            }
            EXL.ScreenUpdating = true;
            EXL.Visible = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;

            this.Cursor = Cursors.Default;
        }
        private void tsbExcelProposals_Click(object sender, EventArgs e)
        {
            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            int j = 0;
            var EXL = new Microsoft.Office.Interop.Excel.Application();
            var oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            // System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("el-GR");
            EXL.Workbooks.Add();
            EXL.ScreenUpdating = false;
            EXL.Caption = Global.AppTitle;

            var loopTo = fgProposals.Rows.Count - 1;
            for (this.i = 0; this.i <= loopTo; this.i++)
            {
                j = j + 1;
                EXL.Cells[i + 1, 1].Value = fgProposals[i, 0];
                EXL.Cells[i + 1, 2].Value = fgProposals[i, 1];
                EXL.Cells[i + 1, 3].Value = fgProposals[i, 2];
                EXL.Cells[i + 1, 4].Value = fgProposals[i, 3];
                EXL.Cells[i + 1, 5].Value = fgProposals[i, 4];
                EXL.Cells[i + 1, 6].Value = fgProposals[i, 5];
                EXL.Cells[i + 1, 7].Value = fgProposals[i, 6];
                EXL.Cells[i + 1, 8].Value = fgProposals[i, 7];
                EXL.Cells[i + 1, 9].Value = fgProposals[i, 8];
                EXL.Cells[i + 1, 10].Value = fgProposals[i, 9];
                EXL.Cells[i + 1, 11].Value = (Global.IsNumeric(fgProposals[i, 10]) ? Convert.ToDecimal(fgProposals[i, 10]).ToString("0.00###") : fgProposals[i, 10] + "");
                EXL.Cells[i + 1, 12].Value = (Global.IsNumeric(fgProposals[i, 11]) ? Convert.ToDecimal(fgProposals[i, 11]).ToString("0.00###") : fgProposals[i, 11] + "");
                EXL.Cells[i + 1, 13].Value = fgProposals[i, 12];
                EXL.Cells[i + 1, 14].Value = fgProposals[i, 13];
                EXL.Cells[i + 1, 15].Value = fgProposals[i, 14];
                EXL.Cells[i + 1, 16].Value = fgProposals[i, 15];
                EXL.Cells[i + 1, 17].Value = fgProposals[i, 16];
                EXL.Cells[i + 1, 18].Value = fgProposals[i, 17];
                EXL.Cells[i + 1, 19].Value = fgProposals[i, 18];

            }
            EXL.ScreenUpdating = true;
            EXL.Visible = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;

            this.Cursor = Cursors.Default;
        }

        private void tsmThink_Click(object sender, EventArgs e)
        {
            ChangeStatus(2);
        }

        private void tsmNotAgree_Click(object sender, EventArgs e)
        {
            ChangeStatus(4);
        }

        private void tsmRestore_Click(object sender, EventArgs e)
        {
            ChangeStatus(1);
        }

        private void tsmCancel_Click(object sender, EventArgs e)
        {
            ChangeStatus(0);
        }
        protected void ucCS_TextChanged(object sender, EventArgs e)
        {
            Global.ContractData stContract = new Global.ContractData();
            stContract = ucCS.SelectedContractData;
            iClient_ID = stContract.Client_ID;
            DefinePsoposalsList();
        }
        protected void ChangeStatus(int iStatus)
        {
            sTemp = "";
            sTemp = "UPDATE InvestIdees_Commands SET Status = " + iStatus + "WHERE ID = " + Convert.ToInt32(fgProposals[fgProposals.Row, 19]);             
            clsSystem System = new clsSystem();
            System.ExecSQL(sTemp);


            if (iStatus == 1) {
                klsInvestIdees = new clsInvestIdees();
                klsInvestIdees.Record_ID = Convert.ToInt32(fgProposals[fgProposals.Row, 0]);
                klsInvestIdees.GetRecord();
                klsInvestIdees.RTODate = DateTime.Now;
                klsInvestIdees.EditRecord();
            }

            //--- make chages into grid ----------------------------
            fgProposals[fgProposals.Row, 1] = sStatus[iStatus];
            fgProposals[fgProposals.Row, 20] = iStatus;

            //--- make changes into dtProposals table --------------
            dtView = InvestProposals.List.DefaultView;
            sTemp = "IIC_ID = " + fgProposals[fgProposals.Row, 20];
            dtView.RowFilter = sTemp;
            foreach (DataRowView dtViewRow in dtView) {
                dtViewRow["Status"] = sStatus[iStatus];
                dtViewRow["Status_ID"] = iStatus;
            }
        }
        protected void ucPS_TextChanged(object sender, EventArgs e)
        {
            Global.ProductData stProduct = new Global.ProductData();
            stProduct = ucPS.SelectedProductData;
            iShare_ID = stProduct.ShareCode_ID;
            lblShareTitle.Text = stProduct.Title;
            lblISIN.Text = stProduct.ISIN;
            DefinePsoposalsList();
        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
