using C1.Win.C1FlexGrid;
using Core;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Contracts
{
    public partial class ucCowner_Child : UserControl
    {
        int i = 0, iClient_ID = 0, iDocType1_ID = 0, iDocType2_ID = 0, iListNumber = 0;
        string sTemp = "", sDescription = "", sClientName = "", sNewFileName = "", sFiles1, sFiles2, sFiles3, sFiles4, sFiles5, sFiles6;
        bool bCheckList = false;
        SortedList lstDocTypes = new SortedList();
        DataRow[] foundRows;
        CoOwnerData_Child CoOwner_Data;
        clsClients klsClients = new clsClients();
        clsClientsDocFiles klsClientDocFiles = new clsClientsDocFiles();

        public ucCowner_Child()
        {
            InitializeComponent();

            panDocs.Left = 22;
            panDocs.Top = 12;
        }

        private void ucCowner_Child_Load(object sender, EventArgs e)
        {
        }
        public void StartInit(int iStatus, string sCN, int iKlient_ID, int iDefaultDocType1_ID, int iDocDefaultType2_ID)
        {
            sClientName = sCN;
            iClient_ID = iKlient_ID;
            iDocType1_ID = iDefaultDocType1_ID;
            iDocType2_ID = iDocDefaultType2_ID;

            ucCS.StartInit(360, 240, 356, 20, 1);
            ucCS.TextOfLabelChanged += new EventHandler(ucCS_TextChanged);
            ucCS.Filters = "Status = 1 AND Tipos < 3";             // Status = 0 - Cancelled, Status = 1 - Αctive       Tipos = 1 - idiotis, 2 - company, 3- join
            ucCS.ListType = 1;
            ucCS.Left = 130;
            ucCS.Top = 40;

            lstDocTypes.Clear();
            foreach (DataRow dtRow in Global.dtDocTypes.Rows)
                lstDocTypes.Add(Convert.ToInt32(dtRow["ID"]), dtRow["Title"] + "");

            //------- fgDocs ----------------------------
            fgDocs.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgDocs.Styles.ParseString(Global.GridStyle);
            fgDocs.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(fgDocs_CellChanged);
            fgDocs.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgDocs_BeforeEdit);
            fgDocs.Cols[1].DataMap = lstDocTypes;

            txtSurname.Text = "";
            txtFirstname.Text = "";
            lblClient2_ID.Text = "0";
            dBoD.Value = DateTime.Now;
            sFiles1 = "";
            sFiles2 = "";
            sFiles3 = "";
            sFiles4 = "";
            sFiles5 = "";
            sFiles6 = "";
            bCheckList = false;

            //--- define request's parameters -----------------------------------------------                        
            if (sDescription.Length > 0)
            {
                string[] tokens = sDescription.Split('~');

                CoOwner_Data = JsonConvert.DeserializeObject<CoOwnerData_Child>(tokens[0]);
                lblClient2_ID.Text = CoOwner_Data.client2_id.ToString();


                CoOwner_Data = JsonConvert.DeserializeObject<CoOwnerData_Child>(tokens[1]);
                txtSurname.Text = CoOwner_Data.surname;

                CoOwner_Data = JsonConvert.DeserializeObject<CoOwnerData_Child>(tokens[2]);
                txtFirstname.Text = CoOwner_Data.firstname;

                CoOwner_Data = JsonConvert.DeserializeObject<CoOwnerData_Child>(tokens[3]);
                dBoD.Text = CoOwner_Data.dob;

                sFiles1 = tokens[4];
                sFiles2 = tokens[5];
                sFiles3 = tokens[6];
                sFiles4 = tokens[7];
                sFiles5 = tokens[8];
                sFiles6 = tokens[9];

                string[] bokens = tokens[3].Split('^');
                for (i = 0; i < bokens.Length - 1; i++)
                {
                    CoOwner_Data = JsonConvert.DeserializeObject<CoOwnerData_Child>(bokens[i]);
                    if (CoOwner_Data.file_name.Length > 0)
                    {
                        sTemp = "";
                        foundRows = Global.dtDocTypes.Select("ID = " + CoOwner_Data.file_type);
                        if (foundRows.Length > 0) sTemp = foundRows[0]["Title"] + "";
                        sTemp = CoOwner_Data.file_name + "\t" + sTemp + "\t" + "0" + "\t" + CoOwner_Data.file_type + "\t" + "1";
                        fgDocs.AddItem(sTemp);
                    }
                }


                klsClientDocFiles = new clsClientsDocFiles();
                klsClientDocFiles.Client_ID = iClient_ID;
                klsClientDocFiles.PreContract_ID = 0;
                klsClientDocFiles.Contract_ID = 0;
                klsClientDocFiles.DocTypes = 0;
                klsClientDocFiles.GetList();
                foreach (DataRow dtRow in klsClientDocFiles.List.Rows)
                    if ((dtRow["FileName"] + "").Trim() != "" && Convert.ToInt32(dtRow["Status"]) > 0)
                    {
                        sTemp = dtRow["FileName"] + "";
                        i = fgDocs.FindRow(sTemp, 1, 0, false);
                        if (i > 0) fgDocs[i, 3] = dtRow["ID"];
                        //else if (lnkEmail.Text == sTemp) lblEmail_ID.Text = dtRow["ID"] + "";
                    }
            }
            bCheckList = true;
            CreateDescription();
        }
        private void picCancel3_Click(object sender, EventArgs e)
        {
            panDocs.Visible = false;
        }
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            sTemp = Global.FileChoice(Global.DefaultFolder);
            if (sTemp.Length > 0)
            {
                fgDocs.AddItem("" + "\t" + "" + "\t" + "0" + "\t" + "0" + "\t" + "" + "\t" + "0");
                fgDocs[fgDocs.Rows.Count - 1, 0] = Path.GetFileNameWithoutExtension(sTemp) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(sTemp);

                foundRows = Global.dtDocTypes.Select("ID = " + iDocType1_ID);
                if (foundRows.Length > 0)
                    fgDocs[fgDocs.Rows.Count - 1, 1] = foundRows[0]["Title"] + "";

                fgDocs[fgDocs.Rows.Count - 1, 2] = "0";
                fgDocs[fgDocs.Rows.Count - 1, 3] = iDocType1_ID.ToString();
                fgDocs[fgDocs.Rows.Count - 1, 4] = sTemp;
                fgDocs[fgDocs.Rows.Count - 1, 5] = "0";             // 0 - Status of new file that will be uploaded later
                CreateDescription();
            }
        }
        private void fgDocs_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (e.Col != 1) e.Cancel = true;
            else e.Cancel = false;
        }
        private void fgDocs_CellChanged(object sender, RowColEventArgs e)
        {
            if (bCheckList)
                if (e.Row == fgDocs.Row && e.Col == 1)
                    fgDocs[fgDocs.Row, "DocType_ID"] = fgDocs[fgDocs.Row, 1];
        }
        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (fgDocs.Row > 0)
            {
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    fgDocs.RemoveItem(fgDocs.Row);
                CreateDescription();
            }
        }
        private void tsbView_Click(object sender, EventArgs e)
        {
            //if ((fgDocs[fgDocs.Row, "FileFullPath"] + "").Trim() != "") Global.DMS_ShowFile("", fgDocs[fgDocs.Row, "FileFullPath"] + "");
            //else
            Global.DMS_ShowFile("Customers/" + sClientName, fgDocs[fgDocs.Row, "File_Name"] + "");
        }
        private void picEdit_Grp2_Click(object sender, EventArgs e)
        {
            iListNumber = 1;
            fgDocs.Rows.Count = 1;

            string[] bokens = sFiles1.Split('^');
            for (i = 0; i < bokens.Length - 1; i++)
            {
                CoOwner_Data = JsonConvert.DeserializeObject<CoOwnerData_Child>(bokens[i]);
                if (CoOwner_Data.file_name.Length > 0)
                {
                    sTemp = "";
                    foundRows = Global.dtDocTypes.Select("ID = " + CoOwner_Data.file_type);
                    if (foundRows.Length > 0) sTemp = foundRows[0]["Title"] + "";
                    sTemp = CoOwner_Data.file_name + "\t" + sTemp + "\t" + "0" + "\t" + CoOwner_Data.file_type + "\t" + "" + "\t" + "1";        // 1 - Status - file exists, but isn't confirmed
                    fgDocs.AddItem(sTemp);
                }
            }

            panDocs.Visible = true;
        }

        private void picClientsList_Click(object sender, EventArgs e)
        {
            ucCS.ShowClientsList = false;
            ucCS.txtClientName.Text = (txtSurname.Text + " " + txtFirstname.Text).Trim();
            ucCS.ShowClientsList = true;
            ucCS.Visible = true;
            ucCS.Focus();
        }

        private void picEdit_Grp3_Click(object sender, EventArgs e)
        {
            iListNumber = 2;
            fgDocs.Rows.Count = 1;

            string[] bokens = sFiles2.Split('^');
            for (i = 0; i < bokens.Length - 1; i++)
            {
                CoOwner_Data = JsonConvert.DeserializeObject<CoOwnerData_Child>(bokens[i]);
                if (CoOwner_Data.file_name.Length > 0)
                {
                    sTemp = "";
                    foundRows = Global.dtDocTypes.Select("ID = " + CoOwner_Data.file_type);
                    if (foundRows.Length > 0) sTemp = foundRows[0]["Title"] + "";
                    sTemp = CoOwner_Data.file_name + "\t" + sTemp + "\t" + "0" + "\t" + CoOwner_Data.file_type + "\t" + "" + "\t" + "1";        // 1 - Status - file exists, but isn't confirmed
                    fgDocs.AddItem(sTemp);
                }
            }

            panDocs.Visible = true;
        }
        private void picEdit_Grp4_1_Click(object sender, EventArgs e)
        {
            iListNumber = 3;
            fgDocs.Rows.Count = 1;

            string[] bokens = sFiles3.Split('^');
            for (i = 0; i < bokens.Length - 1; i++)
            {
                CoOwner_Data = JsonConvert.DeserializeObject<CoOwnerData_Child>(bokens[i]);
                if (CoOwner_Data.file_name.Length > 0)
                {
                    sTemp = "";
                    foundRows = Global.dtDocTypes.Select("ID = " + CoOwner_Data.file_type);
                    if (foundRows.Length > 0) sTemp = foundRows[0]["Title"] + "";
                    sTemp = CoOwner_Data.file_name + "\t" + sTemp + "\t" + "0" + "\t" + CoOwner_Data.file_type + "\t" + "" + "\t" + "1";        // 1 - Status - file exists, but isn't confirmed
                    fgDocs.AddItem(sTemp);
                }
            }

            panDocs.Visible = true;
        }
        private void picEdit_Grp4_2_Click(object sender, EventArgs e)
        {
            iListNumber = 4;
            fgDocs.Rows.Count = 1;

            string[] bokens = sFiles4.Split('^');
            for (i = 0; i < bokens.Length - 1; i++)
            {
                CoOwner_Data = JsonConvert.DeserializeObject<CoOwnerData_Child>(bokens[i]);
                if (CoOwner_Data.file_name.Length > 0)
                {
                    sTemp = "";
                    foundRows = Global.dtDocTypes.Select("ID = " + CoOwner_Data.file_type);
                    if (foundRows.Length > 0) sTemp = foundRows[0]["Title"] + "";
                    sTemp = CoOwner_Data.file_name + "\t" + sTemp + "\t" + "0" + "\t" + CoOwner_Data.file_type + "\t" + "" + "\t" + "1";        // 1 - Status - file exists, but isn't confirmed
                    fgDocs.AddItem(sTemp);
                }
            }

            panDocs.Visible = true;
        }
        private void picEdit_Grp4_3_Click(object sender, EventArgs e)
        {
            iListNumber = 5;
            fgDocs.Rows.Count = 1;

            string[] bokens = sFiles5.Split('^');
            for (i = 0; i < bokens.Length - 1; i++)
            {
                CoOwner_Data = JsonConvert.DeserializeObject<CoOwnerData_Child>(bokens[i]);
                if (CoOwner_Data.file_name.Length > 0)
                {
                    sTemp = "";
                    foundRows = Global.dtDocTypes.Select("ID = " + CoOwner_Data.file_type);
                    if (foundRows.Length > 0) sTemp = foundRows[0]["Title"] + "";
                    sTemp = CoOwner_Data.file_name + "\t" + sTemp + "\t" + "0" + "\t" + CoOwner_Data.file_type + "\t" + "" + "\t" + "1";        // 1 - Status - file exists, but isn't confirmed
                    fgDocs.AddItem(sTemp);
                }
            }

            panDocs.Visible = true;
        }

        private void picEdit_Grp5_Click(object sender, EventArgs e)
        {
            iListNumber = 6;
            fgDocs.Rows.Count = 1;

            string[] bokens = sFiles6.Split('^');
            for (i = 0; i < bokens.Length - 1; i++)
            {
                CoOwner_Data = JsonConvert.DeserializeObject<CoOwnerData_Child>(bokens[i]);
                if (CoOwner_Data.file_name.Length > 0)
                {
                    sTemp = "";
                    foundRows = Global.dtDocTypes.Select("ID = " + CoOwner_Data.file_type);
                    if (foundRows.Length > 0) sTemp = foundRows[0]["Title"] + "";
                    sTemp = CoOwner_Data.file_name + "\t" + sTemp + "\t" + "0" + "\t" + CoOwner_Data.file_type + "\t" + "" + "\t" + "1";        // 1 - Status - file exists, but isn't confirmed
                    fgDocs.AddItem(sTemp);
                }
            }

            panDocs.Visible = true;
        }
        private void picCancel_Click(object sender, EventArgs e)
        {
            panDocs.Visible = false;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            string sFileFullPath = "";
            sNewFileName = "";
            sTemp = "";
            for (i = 1; i <= fgDocs.Rows.Count - 1; i++)
            {
                if (fgDocs[i, "Status"] + "" == "1" || (fgDocs[i, "FileFullPath"] + "" != "" && fgDocs[i, "Status"] + "" == "0"))
                {
                    sNewFileName = (fgDocs[i, "File_Name"] + "").Trim();
                    sFileFullPath = (fgDocs[i, "FileFullPath"] + "").Trim();
                    if (sFileFullPath != "")
                    {
                        if (Path.GetDirectoryName(sFileFullPath) != Global.DMSTransferPoint)
                        {      // Source file isn't in DMS TransferPoint folder, so ...
                            File.Copy(sFileFullPath, Global.DMSTransferPoint + "/" + sNewFileName);   // ... copy this file into DMS TransferPoint folder
                        }

                        clsServerJobs ServerJobs = new clsServerJobs();
                        ServerJobs.JobType_ID = 19;
                        ServerJobs.Source_ID = 0;
                        ServerJobs.Parameters = "{'source_file_full_name': '" + sFileFullPath.Replace(@"\", "/") + "', 'file_name': '" + sNewFileName + "', 'file_type': '" +
                                                fgDocs[i, "DocType_ID"] + "', 'target_folder': 'Customers/" + sClientName.Replace(".", "_") + "/', 'client_id': '" +
                                                iClient_ID + "', 'status' : '1'}";
                        ServerJobs.DateStart = DateTime.Now;
                        ServerJobs.DateFinish = Convert.ToDateTime("1900/01/01");
                        ServerJobs.PubKey = "";
                        ServerJobs.PrvKey = "";
                        ServerJobs.Attempt = 0;
                        ServerJobs.Status = 0;
                        ServerJobs.InsertRecord();

                        fgDocs[i, "Status"] = "1";
                    }
                    sTemp = sTemp + "{'file_name' : '" + sNewFileName + "','file_type' : '" + fgDocs[i, "DocType_ID"] + "'}^";
                }
            }

            switch (iListNumber)
            {
                case 1:
                    sFiles1 = sTemp;
                    break;
                case 2:
                    sFiles2 = sTemp;
                    break;
                case 3:
                    sFiles3 = sTemp;
                    break;
                case 4:
                    sFiles4 = sTemp;
                    break;
                case 5:
                    sFiles5 = sTemp;
                    break;
                case 6:
                    sFiles6 = sTemp;
                    break;
            }
            CreateDescription();
            panDocs.Visible = false;
        }
        private void txtFirstname_LostFocus(object sender, EventArgs e)
        {
            CreateDescription();
        }

        private void txtSurname_LostFocus(object sender, EventArgs e)
        {
            CreateDescription();
        }
        private void dBoD_LostFocus(object sender, EventArgs e)
        {
            CreateDescription();
        }
        private void CreateDescription()
        {
            if (txtSurname.Text.Trim() == "" || txtFirstname.Text.Trim() == "" || dBoD.Text.Trim() == "" || lblClient2_ID.Text == "0") lblStatus_Grp1.Visible = true;
            else lblStatus_Grp1.Visible = false;

            if (sFiles1.Length == 0)
            {
                lblDocs2Count.Text = "";
                lblStatus_Grp2.Visible = true;
            }
            else
            {
                string[] bokens = sFiles1.Split('^');
                lblDocs2Count.Text = (bokens.Length - 1) + " αρχείο(-α)";
                lblStatus_Grp2.Visible = false;
            }

            if (sFiles2.Length == 0)
            {
                lblDocs3Count.Text = "";
                lblStatus_Grp3.Visible = true;
            }
            else
            {
                string[] bokens = sFiles2.Split('^');
                lblDocs3Count.Text = (bokens.Length - 1) + " αρχείο(-α)";
                lblStatus_Grp3.Visible = false;
            }

            if (sFiles3.Length > 0)
            {
                string[] bokens = sFiles3.Split('^');
                lblDocs4_1Count.Text = (bokens.Length - 1) + " αρχείο(-α)";
            }

            if (sFiles4.Length > 0)
            {
                string[] bokens = sFiles4.Split('^');
                lblDocs4_2Count.Text = (bokens.Length - 1) + " αρχείο(-α)";
            }

            if (sFiles5.Length > 0)
            {
                string[] bokens = sFiles5.Split('^');
                lblDocs4_3Count.Text = (bokens.Length - 1) + " αρχείο(-α)";
            }

            if (sFiles6.Length == 0)
            {
                lblDocs5Count.Text = "";
                lblStatus_Grp5.Visible = true;
            }
            else
            {
                string[] bokens = sFiles6.Split('^');
                lblDocs5Count.Text = (bokens.Length - 1) + " αρχείο(-α)";
                lblStatus_Grp5.Visible = false;
            }

            sTemp = "{'client2_id' : '" + lblClient2_ID.Text + "'}~{'surname' : '" + txtSurname.Text + "'}~{'firstname' : '" + txtFirstname.Text + "'}~{'dob' : '" + dBoD.Text + "'}~" +
                    sFiles1 + "~" + sFiles2 + "~" + sFiles3 + "~" + sFiles4 + "~" + sFiles5 + "~" + sFiles6 + "~";
            sDescription = sTemp;
        }
        protected void ucCS_TextChanged(object sender, EventArgs e)
        {
            lblClient2_ID.Text = ucCS.Client_ID.Text;
            if (lblClient2_ID.Text != "0")
            {
                klsClients = new clsClients();
                klsClients.Record_ID = Convert.ToInt32(lblClient2_ID.Text);
                klsClients.GetRecord();
                txtSurname.Text = klsClients.Surname;
                txtFirstname.Text = klsClients.Firstname;
                dBoD.Value = Convert.ToDateTime(klsClients.DoB);
                sClientName = (txtSurname.Text + " " + txtFirstname.Text).Trim();
            }

            CreateDescription();
            ucCS.Visible = false;
        }
        public string Description { get { return this.sDescription; } set { this.sDescription = value; } }
        public class CoOwnerData_Child
        {
            public int client2_id { get; set; }
            public string surname { get; set; }
            public string firstname { get; set; }
            public string dob { get; set; }
            public string file_name { get; set; }
            public int file_type { get; set; }

        }
    }
}
