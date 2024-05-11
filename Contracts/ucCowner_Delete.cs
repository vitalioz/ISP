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
    public partial class ucCowner_Delete : UserControl
    {
        int i = 0, iClient_ID = 0, iDocType1_ID = 0, iDocType2_ID = 0;
        string sTemp = "", sDescription = "", sClientName = "", sNewFileName = "";
        bool bCheckList = false;
        SortedList lstDocTypes = new SortedList();
        DataRow[] foundRows;
        CoOwnerData_Child CoOwner_Data;
        clsClients klsClients = new clsClients();
        clsClients_Clients klsClients_Clients = new clsClients_Clients();
        clsClientsDocFiles klsClientDocFiles = new clsClientsDocFiles();
        public ucCowner_Delete()
        {
            InitializeComponent();
            panDocs.Left = 22;
            panDocs.Top = 12;
        }
        private void ucCowner_Delete_Load(object sender, EventArgs e)
        {

        }
        public void StartInit(int iStatus, int iRecord_ID, int iDefaultDocType1_ID, int iDocDefaultType2_ID)
        {
            iDocType1_ID = iDefaultDocType1_ID;
            iDocType2_ID = iDocDefaultType2_ID;
            lstDocTypes.Clear();
            foreach (DataRow dtRow in Global.dtDocTypes.Rows)
                lstDocTypes.Add(Convert.ToInt32(dtRow["ID"]), dtRow["Title"] + "");

            //------- fgDocs ----------------------------
            fgDocs.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgDocs.Styles.ParseString(Global.GridStyle);
            fgDocs.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(fgDocs_CellChanged);
            fgDocs.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgDocs_BeforeEdit);
            fgDocs.Cols[1].DataMap = lstDocTypes;

            lblSurname.Text = "";
            lblFirstname.Text = "";
            lblDoB.Text = "";
            lblRec_ID.Text = "";
            bCheckList = false;

            //--- define request's parameters -----------------------------------------------                        
            if (sDescription.Length > 0)
            {
                string[] tokens = sDescription.Split('~');

                CoOwner_Data = JsonConvert.DeserializeObject<CoOwnerData_Child>(tokens[0]);
                iRecord_ID = CoOwner_Data.id;

                string[] bokens = tokens[1].Split('^');
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
                    }
            }

            klsClients_Clients = new clsClients_Clients();
            klsClients_Clients.Record_ID = iRecord_ID;
            klsClients_Clients.GetRecord();

            klsClients = new clsClients();
            klsClients.Record_ID = klsClients_Clients.Client2_ID;
            klsClients.GetRecord();
            lblSurname.Text = klsClients.Surname;
            lblFirstname.Text = klsClients.Firstname;
            lblDoB.Text = klsClients.DoB.ToString("dd/MM/yyyy");
            lblRec_ID.Text = klsClients_Clients.Client2_ID.ToString();

            bCheckList = true;
            lblRec_ID.Text = iRecord_ID.ToString();
            CreateDescription();
        }
        private void picEdit_Grp3_Click(object sender, EventArgs e)
        {
            panDocs.Visible = true;
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
                fgDocs.AddItem("\t\t0\t0\t");
                fgDocs[fgDocs.Rows.Count - 1, "File_Name"] = Path.GetFileNameWithoutExtension(sTemp) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(sTemp);

                foundRows = Global.dtDocTypes.Select("ID = " + iDocType1_ID);
                if (foundRows.Length > 0)
                    fgDocs[fgDocs.Rows.Count - 1, "DocType_Title"] = foundRows[0]["Title"] + "";

                fgDocs[fgDocs.Rows.Count - 1, "DocType_ID"] = iDocType1_ID.ToString();
                fgDocs[fgDocs.Rows.Count - 1, "FileFullPath"] = sTemp;
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
        private void btnSave3_Click(object sender, EventArgs e)
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

        private void txtFirstname_LostFocus(object sender, EventArgs e)
        {
            CreateDescription();
        }

        private void txtSurname_LostFocus(object sender, EventArgs e)
        {
            CreateDescription();
        }
        private void lblDoB_LostFocus(object sender, EventArgs e)
        {
            CreateDescription();
        }
        private void CreateDescription()
        {
            if (fgDocs.Rows.Count > 1) lblDocsCount.Text = (fgDocs.Rows.Count - 1) + " αρχείο(-α)";
            else lblDocsCount.Text = "";

            if (fgDocs.Rows.Count == 1) lblStatus_Grp3.Visible = true;
            else lblStatus_Grp3.Visible = false;

            sTemp = "{'id' : '" + lblRec_ID.Text + "'}~";
            for (i = 1; i < fgDocs.Rows.Count; i++)
                sTemp = sTemp + "{ 'file_name' : '" + fgDocs[i, "File_Name"] + "','file_type' : " + fgDocs[i, "DocType_ID"] + "}^";

            sDescription = sTemp + "~";
        }
        public string Description { get { return this.sDescription; } set { this.sDescription = value; } }
        public class CoOwnerData_Child
        {
            public int id { get; set; }
            public string source_email { get; set; }
            public string file_name { get; set; }
            public int file_type { get; set; }

        }
    }
}

