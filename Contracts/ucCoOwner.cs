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
    public partial class ucCoOwner : UserControl
    {
        int i = 0, iClient_ID = 0, iDocType1_ID = 0, iDocType2_ID = 0;
        string sTemp = "", sDescription = "", sClientName = "", sNewFileName = "";
        bool bCheckList = false;
        SortedList lstDocTypes = new SortedList();
        DataRow[] foundRows;
        CoOwnerData CoOwner_Data;
        clsClients klsClients = new clsClients();
        clsClientsDocFiles klsClientDocFiles = new clsClientsDocFiles();

        public ucCoOwner()
        {
            InitializeComponent();

            panDocs.Left = 22;
            panDocs.Top = 12;
        }
        private void ucCoOwner_Load(object sender, EventArgs e)
        {
        }
        // iStatus = 0 - New record, 1 - New (temporary saved), 2 - Sended for checking,  3 - OK (after checking), 4 - problem (after checking), -1 - Cancelled
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
            ucCS.Visible = true;

            lstDocTypes.Clear();
            foreach (DataRow dtRow in Global.dtDocTypes.Rows)
                lstDocTypes.Add(Convert.ToInt32(dtRow["ID"]), dtRow["Title"] + "");

            //------- fgDocs ----------------------------
            fgDocs.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgDocs.Styles.ParseString(Global.GridStyle);
            fgDocs.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(fgDocs_CellChanged);
            fgDocs.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgDocs_BeforeEdit);
            fgDocs.Cols[1].DataMap = lstDocTypes;

            lblClient2_ID.Text = "";
            txtAFM.Text = "";
            bCheckList = false;

            //--- define request's parameters -----------------------------------------------                        
            if (sDescription.Length > 0)
            {
                string[] tokens = sDescription.Split('~');

                CoOwner_Data = JsonConvert.DeserializeObject<CoOwnerData>(tokens[0]);
                lblClient2_ID.Text = CoOwner_Data.client2_id.ToString();

                if (lblClient2_ID.Text != "")
                {
                    foundRows = Global.dtClients.Select("ID = " + lblClient2_ID.Text);
                    if (foundRows.Length > 0)
                        sClientName = foundRows[0]["Fullname"] + "";
                }

                CoOwner_Data = JsonConvert.DeserializeObject<CoOwnerData>(tokens[1]);
                txtAFM.Text = CoOwner_Data.AFM;

                string[] bokens = tokens[2].Split('^');
                for (i = 0; i < bokens.Length - 1; i++)
                {
                    CoOwner_Data = JsonConvert.DeserializeObject<CoOwnerData>(bokens[i]);
                    if (CoOwner_Data.file_name.Length > 0)
                    {
                        sTemp = "";
                        foundRows = Global.dtDocTypes.Select("ID = " + CoOwner_Data.file_type);
                        if (foundRows.Length > 0) sTemp = foundRows[0]["Title"] + "";
                        sTemp = CoOwner_Data.file_name + "\t" + sTemp + "\t" + "0" + "\t" + CoOwner_Data.file_type + "\t" + "" + "\t" + "1";        // 1 - Status - file exists, but isn't confirmed
                        fgDocs.AddItem(sTemp);
                    }
                }

                ucCS.ShowClientsList = false;
                ucCS.txtClientName.Text = sClientName;
                ucCS.ShowClientsList = true;
            }
            bCheckList = true;
            CreateDescription();
        }
        private void picEdit_Grp2_Click(object sender, EventArgs e)
        {
            panDocs.Visible = true;
            ucCS.Focus();
        }

        private void picCancel2_Click(object sender, EventArgs e)
        {
            panDocs.Visible = false;
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            sTemp = Global.FileChoice(Global.DefaultFolder);
            if (sTemp.Length > 0)
            {
                fgDocs.AddItem("" + "\t" + "" + "\t" + "0" + "\t" + "0" + "\t" + "" + "\t" + "0");
                fgDocs[fgDocs.Rows.Count - 1, "File_Name"] = Path.GetFileNameWithoutExtension(sTemp) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(sTemp);

                foundRows = Global.dtDocTypes.Select("ID = " + iDocType1_ID);
                if (foundRows.Length > 0)
                    fgDocs[fgDocs.Rows.Count - 1, "DocType_Title"] = foundRows[0]["Title"] + "";

                fgDocs[fgDocs.Rows.Count - 1, "ID"] = "0";
                fgDocs[fgDocs.Rows.Count - 1, "DocType_ID"] = iDocType1_ID.ToString();
                fgDocs[fgDocs.Rows.Count - 1, "FileFullPath"] = sTemp;
                fgDocs[fgDocs.Rows.Count - 1, "Status"] = "0";                 // 0 - Status of new file that will be uploaded later
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
            if ((fgDocs[fgDocs.Row, "FileFullPath"] + "").Trim() != "") Global.DMS_ShowFile("", fgDocs[fgDocs.Row, "FileFullPath"] + "");
            else Global.DMS_ShowFile("Customers/" + sClientName, fgDocs[fgDocs.Row, "File_Name"] + "");
        }
        private void btnSave2_Click(object sender, EventArgs e)
        {
            string sFileFullPath = "";
            sNewFileName = "";
            for (i = 1; i <= fgDocs.Rows.Count - 1; i++)
            {
                if (fgDocs[i, "Status"] + "" == "0")
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

                        fgDocs[i, "Status"] = "1";
                    }
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
        private void dBoD_LostFocus(object sender, EventArgs e)
        {
            CreateDescription();
        }
        protected void ucCS_TextChanged(object sender, EventArgs e)
        {
            lblClient2_ID.Text = ucCS.Client_ID.Text;

            foundRows = Global.dtClients.Select("ID = " + lblClient2_ID.Text);
            if (foundRows.Length > 0)
            {
                sClientName = (foundRows[0]["Surname"] + " " + foundRows[0]["Firstname"]).Trim();
                txtAFM.Text = foundRows[0]["AFM"] + "";
            }

            CreateDescription();
        }
        private void CreateDescription()
        {
            if (fgDocs.Rows.Count > 1) lblDocsCount.Text = (fgDocs.Rows.Count - 1) + " αρχείο(-α)";
            else lblDocsCount.Text = "";

            if (lblClient2_ID.Text == "" || txtAFM.Text.Trim() == "") lblStatus_Grp1.Visible = true;
            else lblStatus_Grp1.Visible = false;

            if (fgDocs.Rows.Count == 1) lblStatus_Grp2.Visible = true;
            else lblStatus_Grp2.Visible = false;

            if (!lblStatus_Grp1.Visible && !lblStatus_Grp2.Visible) lblStatus.Text = "1";
            else lblStatus.Text = "0";


            sTemp = "{'client2_id' : '" + lblClient2_ID.Text + "'}~{'afm' : '" + txtAFM.Text + "'}~";
            for (i = 1; i < fgDocs.Rows.Count; i++)
                sTemp = sTemp + "{ 'file_name' : '" + fgDocs[i, "File_Name"] + "','file_type' : " + fgDocs[i, "DocType_ID"] + "}^";

            sDescription = sTemp + "~";
        }
        public string Description { get { return this.sDescription; } set { this.sDescription = value; } }
        public class CoOwnerData
        {
            public int client2_id { get; set; }
            public string surname { get; set; }
            public string firstname { get; set; }
            public string AFM { get; set; }
            public string file_name { get; set; }
            public int file_type { get; set; }

        }
    }
}
