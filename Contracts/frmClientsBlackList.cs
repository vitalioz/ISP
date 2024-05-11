using C1.Win.C1FlexGrid;
using Core;
using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Contracts
{
    public partial class frmClientsBlackList : Form
    {
        DataRow[] foundRows;
        int i = 0, j = 0, iID = 0, iAktion = 0, iReportNum, iNewID, iOldRow, iDocFiles_ID, iRightsLevel;
        string sTemp, sClientFullName, sOldClientFullName, sSurnameGreek, sSurnameEnglish, sFullFileName;
        bool bCheckGrid;
        SortedList lstCheck = new SortedList();
        SortedList lstStatus = new SortedList();
        clsClientsBlackList ClientsBlackList = new clsClientsBlackList();
        clsClientsBlackListChecks ClientBlackList_Checks = new clsClientsBlackListChecks();
        public frmClientsBlackList()
        {
            InitializeComponent();

            panNotes.Left = 360;
            panNotes.Top = 36;

            panClients.Left = 468;
            panClients.Top = 62;

            panReports.Left = 100;
            panReports.Top = 38;

            lstCheck.Clear();
            lstCheck.Add(0, "");
            lstCheck.Add(1, "Έλεγχος");

            lstStatus.Clear();
            lstStatus.Add(0, "Επιστολή");
            lstStatus.Add(1, "e-mail");
            lstStatus.Add(2, "Fax");
            lstStatus.Add(3, "Άλλο");
        }

        private void frmClientsBlackList_Load(object sender, EventArgs e)
        {
            bCheckGrid = false;

            //-------------- Define DocTypes List ------------------    
            cmbDocTypes.DataSource = Global.dtDocTypes.Copy();
            cmbDocTypes.DisplayMember = "Title";
            cmbDocTypes.ValueMember = "ID";

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString(Global.GridStyle);
            fgList.RowColChange += new EventHandler(fgList_RowColChange);

            //------- fgCheck ----------------------------
            fgCheck.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
            fgCheck.Styles.ParseString(Global.GridStyle);
            fgCheck.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;

            fgCheck.Cols[1].DataMap = lstCheck;
            fgCheck.Cols[2].DataMap = lstStatus;

            Column c = fgCheck.Cols[4];
            c.DataType = typeof(string);
            c.ComboList = "...";
            fgCheck.CellButtonClick += new C1.Win.C1FlexGrid.RowColEventHandler(fgCheck_CellButtonClick);
            fgCheck.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(fgCheck_CellChanged);

            ClientsBlackList.GetList();
            DataFiltering();
            bCheckGrid = true;

            fgList.Row = 0;
            if (fgList.Rows.Count > 1)
            {
                fgList.Row = 1;
                fgList.Focus();
            }

            tsbSave.Enabled = false;
            if (iRightsLevel == 1)
            {
                tsbAdd.Enabled = false;
                tsbEdit.Enabled = false;
                tsbDelete.Enabled = false;
            }
        }
        protected override void OnResize(EventArgs e)
        {
            fgList.Height = this.Height - 94;
            grpData.Height = this.Height - 88;
            fgCheck.Height = this.Height - 628;
        }
        //--- fgList functions --------------------------------------------------------------------------------
        private void DataFiltering()
        {
            string sSurnameGreek, sSurnameEnglish;

            Global.TranslateUserName(txtFilter.Text, out sSurnameGreek, out sSurnameEnglish);

            foundRows = ClientsBlackList.List.Select("(Surname LIKE '%" + sSurnameEnglish + "%' OR Surname LIKE '%" + sSurnameGreek + "%')", "Surname");

            fgList.Redraw = false;
            fgList.Rows.Count = 1;

            for (int i = 0; i < foundRows.Length; i++)
                fgList.AddItem((foundRows[i]["Surname"] + " " + foundRows[i]["Firstname"]).Trim() + "\t" + foundRows[i]["ID"]);

            fgList.Redraw = true;
        }
        private void fgList_RowColChange(object sender, EventArgs e)
        {
            if (bCheckGrid)
            {
                if (fgList.Row > 0)
                {
                    iID = Convert.ToInt32(fgList[fgList.Row, 1]);
                    sClientFullName = fgList[fgList.Row, 0] + "";
                    sOldClientFullName = fgList[fgList.Row, 0] + "";
                    iAktion = 1;                                                         //1 - EDIT Mode
                    EmptyData();
                    ShowRecord();
                    tsbSave.Enabled = false;
                }
            }
        }
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            DataFiltering();
            if (fgList.Rows.Count > 1)
            {
                bCheckGrid = false;
                fgList.Row = 0;
                bCheckGrid = true;
                fgList.Row = 1;
            }
        }
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            iAktion = 0;                                                   // 0 - ADD Mode
            EmptyData();
            tsbSave.Enabled = true;
            txtSurname.Focus();
        }
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            tsbSave.Enabled = true;
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 1)
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    ClientsBlackList.Record_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                    ClientsBlackList.DeleteRecord();
                    fgList.RemoveItem(fgList.Row);

                    if (fgList.Rows.Count > 1)
                    {
                        fgList.Focus();
                        iID = Convert.ToInt32(fgList[fgList.Row, 1]);
                    }
                    fgList.Redraw = true;

                    iAktion = 1;                                                          // 1 - EDIT Mode
                    ShowRecord();
                }
        }

        private void tsbPrint_Click(object sender, EventArgs e)
        {
            panReports.Visible = true;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Global.TranslateUserName(txtFilter.Text, out sSurnameGreek, out sSurnameEnglish);
            sSurnameGreek = sSurnameGreek + "%";
            sSurnameEnglish = sSurnameEnglish + "%";

            iAktion = 1;                                             // 1 - Print, 2 - Excel
            if (rbReport6.Checked) iReportNum = 6;
            if (rbReport7.Checked) iReportNum = 7;
            if (rbReport9.Checked) iReportNum = 9;

            frmReports locReports = new frmReports();
            locReports.Params = sSurnameGreek + "~" + sSurnameEnglish + "~" + 0 + "~" + 0 + "~";
            locReports.ReportID = iReportNum;
            locReports.Show();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {

        }

        private void picClose_Reports_Click(object sender, EventArgs e)
        {
            panReports.Visible = false;
        }
        private void tsbSave_Click(object sender, EventArgs e)
        {
            panNotes.Visible = true;
        }

        private void tsbHistory_Click(object sender, EventArgs e)
        {
            frmShowHistory locShowHistory = new frmShowHistory();
            locShowHistory.RecType = 11;                                                     // 11-BlackList
            locShowHistory.SrcRec_ID = 0;
            locShowHistory.Client_ID = iID;
            locShowHistory.Contract_ID = 0;
            locShowHistory.Code = "";
            locShowHistory.ClientFullName = sClientFullName;
            locShowHistory.ClientsList = 2;                                                 // 1 - Customers List (Main List), 2 - Clients Black List
            locShowHistory.ClientType = 1;
            //locShowHistory.Doc_Tipos = 1                                                    // 1 - Client Personal Data
            locShowHistory.ShowDialog();
        }

        private void fgCheck_CellButtonClick(object sender, RowColEventArgs e)
        {
            if (e.Col == 4)
            {
                fgCheck[fgCheck.Row, "FileFullName"] = Global.FileChoice(Global.DefaultFolder);
                fgCheck[fgCheck.Row, 4] = Path.GetFileName(fgCheck[fgCheck.Row, "FileFullName"] + "");
            }
        }
        private void fgCheck_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Col == 1) fgCheck[e.Row, 9] = fgCheck[e.Row, 1];
            if (e.Col == 2) fgCheck[e.Row, 7] = fgCheck[e.Row, 2];
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSurname.Text.Length != 0)
            {
                if (iAktion == 0)
                {                                                                                     //0 - ADD Mode
                    Global.DMS_CreateDirectory("Customers/" + sClientFullName);
                    Global.DMS_CreateDirectory("Customers/" + sClientFullName + "/Compliance");
                }
                else
                {                                                                                                       // <> 0 - EDIT Mode
                    if (sOldClientFullName != sClientFullName)
                        Global.DMS_RenameFolderName(sOldClientFullName, sClientFullName);
                }

                ClientsBlackList.Surname = txtSurname.Text + "";
                ClientsBlackList.Firstname = txtFirstname.Text + "";
                ClientsBlackList.FirstnameFather = txtFatherFirstname.Text + "";
                ClientsBlackList.FirstnameMother = txtMotherFirstname.Text + "";
                ClientsBlackList.Address = txtAddress.Text + "";
                ClientsBlackList.DOY = txtDOY.Text + "";
                ClientsBlackList.AFM = txtAFM.Text + "";
                ClientsBlackList.DoB = dDoB.Value;
                ClientsBlackList.BornPlace = txtBorn.Text + "";
                ClientsBlackList.ADT = txtADT.Text + "";
                ClientsBlackList.IssuedDoc = txtIssuedDoc.Text + "";
                ClientsBlackList.IssuedNotes = txtIssuedNotes.Text + "";
                ClientsBlackList.Found = cmbFound.SelectedIndex;
                ClientsBlackList.IssuedActions = txtIssuedActions.Text + "";
                ClientsBlackList.Address = txtAddress.Text + "";
                ClientsBlackList.Address = txtAddress.Text + "";
                ClientsBlackList.Address = txtAddress.Text + "";

                if (iAktion == 0) iID = ClientsBlackList.InsertRecord();                                              //0 - ADD Mode
                else ClientsBlackList.EditRecord();                                                                   // <> 0 - EDIT Mode

                j = fgCheck.Rows.Count - 1;
                for (i = 1; i <= j; i++)
                {
                    if ((fgCheck[i, 8] + "") != "")
                    {                                                                 // FileFullName - Not Empty means that it's a new file
                        sTemp = Global.DMS_UploadFile(fgCheck[i, 8] + "", "Customers/" + sClientFullName + "/Compliance", fgCheck[i, 4] + "");
                        fgCheck[i, 4] = Path.GetFileName(sTemp);
                    }

                    clsClientsBlackList ClientsBlackList = new clsClientsBlackList();
                    if ((fgCheck[i, 5] + "") != "0")
                    {
                        ClientBlackList_Checks.Record_ID = Convert.ToInt32(fgCheck[i, 5]);
                        ClientBlackList_Checks.GetRecord();
                    }

                    ClientBlackList_Checks.Client_ID = iID;
                    ClientBlackList_Checks.User_ID = Convert.ToInt32(fgCheck[i, 6]);
                    ClientBlackList_Checks.CheckStatus = Convert.ToInt32(fgCheck[i, 9]);
                    ClientBlackList_Checks.Status = Convert.ToInt32(fgCheck[i, 7]);
                    ClientBlackList_Checks.Notes = fgCheck[i, "Notes"] + "";
                    ClientBlackList_Checks.FileName = fgCheck[i, "Filename"] + "";

                    if ((fgCheck[i, 5] + "") == "0") ClientBlackList_Checks.InsertRecord();
                    else ClientBlackList_Checks.EditRecord();
                }

                iNewID = iID;
                iOldRow = fgList.Row;

                ClientsBlackList.GetList();
                bCheckGrid = false;
                DataFiltering();
                bCheckGrid = true;

                sTemp = iNewID.ToString();
                i = fgList.FindRow(sTemp, 1, 1, false);
                if (i < 0) fgList.Row = iOldRow;
                else fgList.Row = i;

                tsbSave.Enabled = false;

                iDocFiles_ID = 0;
                if (txtFileName.Text.Length > 0) AddDocument();

                sTemp = txtSurname.Text;
                SaveBlackListHistory(iID, iAktion, sTemp, iDocFiles_ID, txtNotes.Text, DateTime.Now, Global.User_ID);
            }
            else
                MessageBox.Show("Η εισαγωγή του επωνύμου είναι υποχρεωτική", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            panNotes.Visible = false;
        }

        private void picClose_Clients_Click(object sender, EventArgs e)
        {
            panClients.Visible = false;
        }
        //--- fgCheck functions ---------------------------------------------------------------------------
        private void tsbAdd_Check_Click(object sender, EventArgs e)
        {
            fgCheck.AddItem(Global.UserName + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "" + "\t" + "0" + "\t" + Global.User_ID + "\t" + "0" + "\t" + "" + "\t" + "0");
        }

        private void tsbDel_Check_Click(object sender, EventArgs e)
        {
            if (fgCheck.Row > 0)
            {
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    ClientBlackList_Checks.Record_ID = Convert.ToInt32(fgCheck[fgCheck.Row, "ID"]);
                    ClientBlackList_Checks.DeleteRecord();
                    fgCheck.RemoveItem(fgCheck.Row);
                    fgCheck.Redraw = true;
                }
            }
        }

        private void tsbShow_Check_Click(object sender, EventArgs e)
        {
            if ((fgCheck[fgCheck.Row, "FileName"] + "") != "")
                Global.DMS_ShowFile("Customers/" + sClientFullName + "/Compliance", fgCheck[fgCheck.Row, "FileName"] + "");
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            panNotes.Visible = false;
        }
        private void picFilePath_Click(object sender, EventArgs e)
        {
            sFullFileName = Global.FileChoice(Global.DefaultFolder);
            txtFileName.Text = Path.GetFileName(sFullFileName);
        }


        private void tslCheck_Click(object sender, EventArgs e)
        {
            fgClients.Redraw = false;
            fgClients.Rows.Count = 1;

            //--------- initialise Black List -----------
            ClientsBlackList.GetCheckList();
            foreach (DataRow dtRow in ClientsBlackList.List.Rows)
                fgClients.AddItem(dtRow["Surname"] + " " + dtRow["Firstname"] + "\t" + "" + "\t" + "" + dtRow["ID"]);

            fgClients.Redraw = true;
            panClients.Visible = true;
        }


        private void EmptyData()
        {
            txtSurname.Text = "";
            txtFirstname.Text = "";
            txtFatherFirstname.Text = "";
            txtMotherFirstname.Text = "";
            txtAddress.Text = "";
            txtDOY.Text = "";
            txtAFM.Text = "";
            txtADT.Text = "";
            txtBorn.Text = "";
            txtIssuedDoc.Text = "";
            txtIssuedNotes.Text = "";
            cmbFound.SelectedIndex = 0;
            txtIssuedActions.Text = "";
            fgCheck.Rows.Count = 1;
        }
        private void ShowRecord()
        {
            ClientsBlackList.Record_ID = iID;
            ClientsBlackList.GetRecord();

            txtSurname.Text = ClientsBlackList.Surname + "";
            txtFirstname.Text = ClientsBlackList.Firstname + "";
            txtFatherFirstname.Text = ClientsBlackList.FirstnameFather + "";
            txtMotherFirstname.Text = ClientsBlackList.FirstnameMother + "";
            txtAddress.Text = ClientsBlackList.Address + "";
            txtDOY.Text = ClientsBlackList.DOY + "";
            txtAFM.Text = ClientsBlackList.AFM + "";
            txtADT.Text = ClientsBlackList.ADT + "";
            dDoB.Value = ClientsBlackList.DoB;
            txtBorn.Text = ClientsBlackList.BornPlace + "";
            txtIssuedDoc.Text = ClientsBlackList.IssuedDoc + "";
            txtIssuedNotes.Text = ClientsBlackList.IssuedNotes + "";
            txtIssuedActions.Text = ClientsBlackList.IssuedActions + "";
            cmbFound.SelectedIndex = ClientsBlackList.Found;

            fgCheck.Redraw = false;
            fgCheck.Rows.Count = 1;
            ClientBlackList_Checks.Client_ID = iID;
            ClientBlackList_Checks.GetList();
            foreach (DataRow dtRow in ClientBlackList_Checks.List.Rows)
            {
                fgCheck.AddItem((dtRow["Surname"] + " " + dtRow["Firstname"]).Trim() + "\t" + lstCheck[dtRow["CheckStatus"]] + "\t" + lstStatus[dtRow["Status"]] + "\t" +
                dtRow["Notes"] + "\t" + dtRow["FileName"] + "\t" + dtRow["ID"] + "\t" + dtRow["User_ID"] + "\t" + dtRow["Status"] + "\t" + "" + "\t" + dtRow["CheckStatus"]);
            }
            fgCheck.Redraw = true;
        }
        private void SaveBlackListHistory(int iClient_ID, int iAktion, string sValue, int iDocFiles_ID, string sNotes, DateTime dIns, int iUser_ID)
        {
            clsHistory klsHistory = new clsHistory();
            klsHistory.RecType = 11;                                                                            // 11-BlackList
            klsHistory.SrcRec_ID = 0;
            klsHistory.Client_ID = iClient_ID;
            klsHistory.Contract_ID = 0;
            klsHistory.Action = iAktion;
            klsHistory.CurrentValues = sValue;
            klsHistory.DocFiles_ID = iDocFiles_ID;
            klsHistory.Notes = sNotes;
            klsHistory.User_ID = iUser_ID;
            klsHistory.DateIns = dIns;
            klsHistory.InsertRecord();
        }
        private void AddDocument()
        {
            if (sFullFileName != "")
            {
                //--- this file is personal file, soContract_ID = 0, Code = "" ----
                clsClientsDocFiles klsClientDocFiles = new clsClientsDocFiles();
                klsClientDocFiles.PreContract_ID = 0;
                klsClientDocFiles.Contract_ID = 0;
                klsClientDocFiles.Client_ID = iID;
                klsClientDocFiles.ClientName = sClientFullName;
                klsClientDocFiles.ContractCode = "";
                klsClientDocFiles.DocTypes = Convert.ToInt32(cmbDocTypes.SelectedValue);
                klsClientDocFiles.DMS_Files_ID = 0;
                klsClientDocFiles.OldFileName = "";
                klsClientDocFiles.NewFileName = txtFileName.Text;
                klsClientDocFiles.FullFileName = sFullFileName;
                klsClientDocFiles.DateIns = DateTime.Now;
                klsClientDocFiles.User_ID = Global.User_ID;
                klsClientDocFiles.Status = 2;                                           // 2 - document confirmed
                iDocFiles_ID = klsClientDocFiles.InsertRecord();
            }
        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
    }
}
