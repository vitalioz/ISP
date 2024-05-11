﻿using C1.Win.C1FlexGrid;
using Core;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Contracts
{
    public partial class ucEmail : UserControl
    {
        int i = 0, iClient_ID = 0, iDocType1_ID = 0, iDocType2_ID = 0;
        string sTemp = "", sDescription = "", sClientName = "", sNewFileName = "";
        bool bCheckList = false;
        SortedList lstDocTypes = new SortedList();
        DataRow[] foundRows;
        EmailData Email_Data;

        public ucEmail()
        {
            InitializeComponent();

            panDocs.Left = 22;
            panDocs.Top = 12;
        }

        private void ucEmail_Load(object sender, EventArgs e)
        {

        }
        // iStatus = 0 - New record, 1 - New (temporary saved), 2 - Sended for checking,  3 - OK (after checking), 4 - problem (after checking), -1 - Cancelled
        public void StartInit(int iStatus, string sCN, int iKlient_ID, int iDefaultDocType1_ID, int iDocDefaultType2_ID)
        {
            sClientName = sCN;
            iClient_ID = iKlient_ID;
            iDocType1_ID = iDefaultDocType1_ID;
            iDocType2_ID = iDocDefaultType2_ID;
            sClientName = sCN;

            lstDocTypes.Clear();
            foreach (DataRow dtRow in Global.dtDocTypes.Rows)
                lstDocTypes.Add(Convert.ToInt32(dtRow["ID"]), dtRow["Title"] + "");

            //------- fgDocs ----------------------------
            fgDocs.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgDocs.Styles.ParseString(Global.GridStyle);
            fgDocs.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(fgDocs_CellChanged);
            fgDocs.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgDocs_BeforeEdit);
            fgDocs.Cols[1].DataMap = lstDocTypes;

            txtNewValue.Text = "";
            bCheckList = false;

            //--- define request's parameters -----------------------------------------------                        
            if (sDescription.Length > 0)
            {
                string[] tokens = sDescription.Split('~');

                Email_Data = JsonConvert.DeserializeObject<EmailData>(tokens[0]);
                txtNewValue.Text = Email_Data.new_email;

                Email_Data = JsonConvert.DeserializeObject<EmailData>(tokens[1]);
                lnkEmail.Text = Email_Data.source_email;

                string[] bokens = tokens[2].Split('^');
                for (i = 0; i < bokens.Length - 1; i++)
                {
                    Email_Data = JsonConvert.DeserializeObject<EmailData>(bokens[i]);
                    if (Email_Data.file_name.Length > 0)
                    {
                        sTemp = "";
                        foundRows = Global.dtDocTypes.Select("ID = " + Email_Data.file_type);
                        if (foundRows.Length > 0) sTemp = foundRows[0]["Title"] + "";
                        sTemp = Email_Data.file_name + "\t" + sTemp + "\t" + Email_Data.file_type + "\t" + "";
                        fgDocs.AddItem(sTemp);
                    }
                }
            }

            bCheckList = true;
            CreateDescription();
        }

        private void txtNewValue_LostFocus(object sender, EventArgs e)
        {
            CreateDescription();
        }
        private void picEdit_Grp2_Click(object sender, EventArgs e)
        {
            sTemp = Global.FileChoice(Global.DefaultFolder);
            if (sTemp.Length > 0)
            {
                sNewFileName = Path.GetFileNameWithoutExtension(sTemp) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(sTemp);

                lnkEmail.Text = sNewFileName;

                if (Path.GetDirectoryName(sTemp) != Global.DMSTransferPoint)
                {   // Source file isn't in DMS TransferPoint folder, so ...
                    if (File.Exists(Global.DMSTransferPoint + "/" + sNewFileName))
                        sNewFileName = Path.GetFileNameWithoutExtension(sNewFileName) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(sNewFileName);
                    File.Copy(sTemp, Global.DMSTransferPoint + "/" + sNewFileName);         // ... copy this file into DMS TransferPoint folder
                }

                clsServerJobs ServerJobs = new clsServerJobs();
                ServerJobs.JobType_ID = 19;
                ServerJobs.Source_ID = 0;
                ServerJobs.Parameters = "{'source_file_full_name': '" + sTemp.Replace(@"\", "/") + "', 'file_name': '" + sNewFileName + "', 'file_type': '" +
                                        iDocType1_ID + "', " + "'target_folder': 'Customers/" + sClientName.Replace(".", "_") + "/', 'client_id': '" +
                                        iClient_ID + "', 'status' : '1'}";
                ServerJobs.DateStart = DateTime.Now;
                ServerJobs.DateFinish = Convert.ToDateTime("1900/01/01");
                ServerJobs.PubKey = "";
                ServerJobs.PrvKey = "";
                ServerJobs.Attempt = 0;
                ServerJobs.Status = 0;
                ServerJobs.InsertRecord();
            }

            CreateDescription();
        }

        private void lnkEmail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Global.DMS_ShowFile("Customers\\" + sClientName, lnkEmail.Text);
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            sTemp = Global.FileChoice(Global.DefaultFolder);
            if (sTemp.Length > 0)
            {
                fgDocs.AddItem("" + "\t" + "" + "\t" + "0" + "\t" + "");
                fgDocs[fgDocs.Rows.Count - 1, 0] = Path.GetFileNameWithoutExtension(sTemp) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(sTemp);

                foundRows = Global.dtDocTypes.Select("ID = " + iDocType1_ID);
                if (foundRows.Length > 0)
                    fgDocs[fgDocs.Rows.Count - 1, 1] = foundRows[0]["Title"] + "";

                fgDocs[fgDocs.Rows.Count - 1, 2] = iDocType1_ID.ToString();

                fgDocs[fgDocs.Rows.Count - 1, 3] = sTemp;
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
                    fgDocs[fgDocs.Row, 2] = fgDocs[fgDocs.Row, 1];
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

        private void picEdit_Grp1_Click(object sender, EventArgs e)
        {
            panDocs.Visible = true;
        }
        private void tsbView_Click(object sender, EventArgs e)
        {
            if ((fgDocs[fgDocs.Row, "FileFullPath"] + "").Trim() != "") Global.DMS_ShowFile("", fgDocs[fgDocs.Row, "FileFullPath"] + "");
            else Global.DMS_ShowFile("Customers/" + sClientName, fgDocs[fgDocs.Row, "File_Name"] + "");
        }

        private void picCancel1_Click(object sender, EventArgs e)
        {
            panDocs.Visible = false;
        }

        private void btnSave1_Click(object sender, EventArgs e)
        {
            string sFileFullPath = "";
            sNewFileName = "";
            for (i = 1; i <= fgDocs.Rows.Count - 1; i++)
            {
                sFileFullPath = (fgDocs[i, "FileFullPath"] + "").Trim();
                if (sFileFullPath != "")
                {
                    sNewFileName = (fgDocs[i, "File_Name"] + "").Trim();

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
                }
            }

            CreateDescription();
            panDocs.Visible = false;
        }

        private void CreateDescription()
        {
            lblStatus.Text = "1";

            //if (fgDocs.Rows.Count > 1) lblDocsCount.Text = (fgDocs.Rows.Count - 1) + " αρχείο(-α)";
            //else { lblDocsCount.Text = ""; lblStatus.Text = "0"; }



            if (lnkEmail.Text.Trim() == "") { lblStatus_Grp2.Visible = true; }
            else lblStatus_Grp2.Visible = false;

            if (txtNewValue.Text == "") lblStatus.Text = "0";

            sTemp = "";
            for (i = 1; i < fgDocs.Rows.Count; i++)
                sTemp = sTemp + "{ 'file_name' : '" + fgDocs[i, "File_Name"] + "','file_type' : " + fgDocs[i, "DocType_ID"] + "}^";

            sDescription = "{'new_email' : '" + txtNewValue.Text + "'}~{'source_email' : '" + lnkEmail.Text + "'}~" + sTemp;
        }
        public string Description { get { return this.sDescription; } set { this.sDescription = value; } }
        public class EmailData
        {
            public string new_email { get; set; }
            public string source_email { get; set; }
            public string file_name { get; set; }
            public int file_type { get; set; }
        }
    }
}
