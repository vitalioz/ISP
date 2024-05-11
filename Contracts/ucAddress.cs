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
    public partial class ucAddress : UserControl
    {
        int i = 0, iClient_ID = 0, iDocType1_ID = 0, iDocType2_ID = 0, iStatus2 = 0, iStatus4 = 0, iStatus5 = 0;
        string sTemp = "", sDescription = "", sClientName = "", sNewFileName = "";
        bool bCheckList = false;
        SortedList lstDocTypes = new SortedList();
        DataTable dtList;
        DataRow[] foundRows;
        AddressData Address_Data;
        clsClientsDocFiles klsClientDocFiles = new clsClientsDocFiles();
        public ucAddress()
        {
            InitializeComponent();

            panDocs.Left = 22;
            panDocs.Top = 12;

            panDocs2.Left = 22;
            panDocs2.Top = 12;
            rbOwner_Yes.Checked = true;
        }

        private void ucAddress_Load(object sender, EventArgs e)
        {

        }
        // iStatus = 0 - New record, 1 - New (temporary saved), 2 - Sended for checking,  3 - first check OK, 4 - second check OK, 5 - problem (after checking), -1 - Cancelled
        public void StartInit(int iStatus, string sCN, int iKlient_ID, int iDefaultDocType1_ID, int iDocDefaultType2_ID)
        {
            sClientName = sCN;
            iClient_ID = iKlient_ID;
            iDocType1_ID = iDefaultDocType1_ID;
            iDocType2_ID = iDocDefaultType2_ID;

            dtList = Global.dtCountries.Copy();
            dtList.DefaultView.Sort = "TitleGreek";
            cmbNewCountry.DataSource = dtList;
            cmbNewCountry.DisplayMember = "TitleGreek";
            cmbNewCountry.ValueMember = "ID";

            lstDocTypes.Clear();
            foreach (DataRow dtRow in Global.dtDocTypes.Rows)
                lstDocTypes.Add(Convert.ToInt32(dtRow["ID"]), dtRow["Title"] + "");

            //------- fgDocs ----------------------------
            fgDocs.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgDocs.Styles.ParseString(Global.GridStyle);
            fgDocs.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(fgDocs_CellChanged);
            fgDocs.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgDocs_BeforeEdit);
            fgDocs.Cols[1].DataMap = lstDocTypes;

            //------- fgDocs2 ----------------------------
            fgDocs2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgDocs2.Styles.ParseString(Global.GridStyle);
            fgDocs2.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(fgDocs2_CellChanged);
            fgDocs2.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgDocs2_BeforeEdit);
            fgDocs2.Cols[1].DataMap = lstDocTypes;

            txtNewAddress.Text = "";
            txtNewCity.Text = "";
            txtNewZip.Text = "";
            cmbNewCountry.SelectedValue = 1;
            bCheckList = false;

            //--- define request's parameters -----------------------------------------------                        
            if (sDescription.Length > 0)
            {
                string[] tokens = sDescription.Split('~');

                Address_Data = JsonConvert.DeserializeObject<AddressData>(tokens[0]);
                lblOldAddress.Text = Address_Data.old_address.Trim();

                Address_Data = JsonConvert.DeserializeObject<AddressData>(tokens[1]);
                lblOldCity.Text = Address_Data.old_city.Trim();

                Address_Data = JsonConvert.DeserializeObject<AddressData>(tokens[2]);
                lblOldZip.Text = Address_Data.old_zip.Trim();

                Address_Data = JsonConvert.DeserializeObject<AddressData>(tokens[3]);
                lblOldCountry.Text = Address_Data.old_country.Trim();

                Address_Data = JsonConvert.DeserializeObject<AddressData>(tokens[4]);
                txtNewAddress.Text = Address_Data.new_address.Trim();

                Address_Data = JsonConvert.DeserializeObject<AddressData>(tokens[5]);
                txtNewCity.Text = Address_Data.new_city.Trim();

                Address_Data = JsonConvert.DeserializeObject<AddressData>(tokens[6]);
                txtNewZip.Text = Address_Data.new_zip.Trim();

                Address_Data = JsonConvert.DeserializeObject<AddressData>(tokens[7]);
                cmbNewCountry.SelectedValue = Address_Data.new_country_id;

                Address_Data = JsonConvert.DeserializeObject<AddressData>(tokens[8]);
                lnkEmail.Text = Address_Data.source_email.Trim();

                Address_Data = JsonConvert.DeserializeObject<AddressData>(tokens[9]);
                if (Address_Data.owner.Trim() == "1") rbOwner_Yes.Checked = true;
                else rbOwner_Yes.Checked = false;

                string[] bokens = tokens[10].Split('^');
                for (i = 0; i < bokens.Length - 1; i++)
                {
                    Address_Data = JsonConvert.DeserializeObject<AddressData>(bokens[i]);
                    if (Address_Data.file_name.Length > 0)
                    {
                        sTemp = "";
                        foundRows = Global.dtDocTypes.Select("ID = " + Address_Data.file_type);
                        if (foundRows.Length > 0) sTemp = foundRows[0]["Title"] + "";
                        sTemp = Address_Data.file_name + "\t" + sTemp + "\t" + "0" + "\t" + Address_Data.file_type + "\t" + "" + "\t" + "1";        // 1 - Status - file exists, but isn't confirmed
                        fgDocs.AddItem(sTemp);
                    }
                }

                bokens = tokens[11].Split('^');
                for (i = 0; i < bokens.Length - 1; i++)
                {
                    Address_Data = JsonConvert.DeserializeObject<AddressData>(bokens[i]);
                    if (Address_Data.file_name.Length > 0)
                    {
                        sTemp = "";
                        foundRows = Global.dtDocTypes.Select("ID = " + Address_Data.file_type);
                        if (foundRows.Length > 0) sTemp = foundRows[0]["Title"] + "";
                        sTemp = Address_Data.file_name + "\t" + sTemp + "\t" + "0" + "\t" + Address_Data.file_type + "\t" + "" + "\t" + "1";        // 1 - Status - file exists, but isn't confirmed
                        fgDocs2.AddItem(sTemp);
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
                        if (i > 0) fgDocs[i, "ID"] = dtRow["ID"];
                        else
                        {
                            i = fgDocs2.FindRow(sTemp, 1, 0, false);
                            if (i > 0) fgDocs2[i, "ID"] = dtRow["ID"];
                            else
                            if (lnkEmail.Text == sTemp) lblEmail_ID.Text = dtRow["ID"] + "";
                        }
                    }
            }

            bCheckList = true;
            CreateDescription();
        }
        private void rbOwner_Yes_CheckedChanged(object sender, EventArgs e)
        {
            CheckOwner();
        }
        private void rbOwner_No_CheckedChanged(object sender, EventArgs e)
        {
            CheckOwner();
        }
        private void CheckOwner()
        {
            if (rbOwner_Yes.Checked)
            {
                rbOwner_No.Checked = false;
                lblDocs2Count.Text = "";
                iStatus5 = 0;
            }
            else
            {
                rbOwner_No.Checked = true;
                if (fgDocs2.Rows.Count == 1) iStatus5 = 1;
                else iStatus5 = 0;
            }
            CreateDescription();
        }
        private void picEdit_Grp5_Click(object sender, EventArgs e)
        {
            panDocs2.Visible = true;
        }

        private void tsbAdd4_Click(object sender, EventArgs e)
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
        private void tsbDelete4_Click(object sender, EventArgs e)
        {
            if (fgDocs.Row > 0)
            {
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    fgDocs.RemoveItem(fgDocs.Row);
                CreateDescription();
            }
        }
        private void tsbView4_Click(object sender, EventArgs e)
        {
            if ((fgDocs[fgDocs.Row, "FileFullPath"] + "").Trim() != "") Global.DMS_ShowFile("", fgDocs[fgDocs.Row, "FileFullPath"] + "");
            else Global.DMS_ShowFile("Customers/" + sClientName, fgDocs[fgDocs.Row, "File_Name"] + "");
        }
        private void picCancel_Click(object sender, EventArgs e)
        {
            panDocs.Visible = false;
        }

        private void btnSave4_Click(object sender, EventArgs e)
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

            if (fgDocs.Rows.Count == 1) iStatus4 = 1;
            else iStatus4 = 0;
            picEdit_Grp4.Visible = true;
            CreateDescription();
            panDocs.Visible = false;
        }
        private void tsbAdd5_Click(object sender, EventArgs e)
        {
            sTemp = Global.FileChoice(Global.DefaultFolder);
            sTemp = Global.FileChoice(Global.DefaultFolder);
            if (sTemp.Length > 0)
            {
                fgDocs2.AddItem("" + "\t" + "" + "\t" + "0" + "\t" + "0" + "\t" + "" + "\t" + "0");
                fgDocs2[fgDocs2.Rows.Count - 1, 0] = Path.GetFileNameWithoutExtension(sTemp) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(sTemp);

                foundRows = Global.dtDocTypes.Select("ID = " + iDocType2_ID);
                if (foundRows.Length > 0)
                    fgDocs2[fgDocs2.Rows.Count - 1, 1] = foundRows[0]["Title"] + "";

                fgDocs2[fgDocs2.Rows.Count - 1, 2] = "0";
                fgDocs2[fgDocs2.Rows.Count - 1, 3] = iDocType2_ID.ToString();
                fgDocs2[fgDocs2.Rows.Count - 1, 4] = sTemp;
                fgDocs2[fgDocs2.Rows.Count - 1, 5] = "0";             // 0 - Status of new file that will be uploaded later

                CreateDescription();
            }
        }
        private void fgDocs2_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (e.Col != 1) e.Cancel = true;
            else e.Cancel = false;
        }
        private void fgDocs2_CellChanged(object sender, RowColEventArgs e)
        {
            if (bCheckList)
                if (e.Row == fgDocs2.Row && e.Col == 1)
                    fgDocs2[fgDocs2.Row, "DocType_ID"] = fgDocs2[fgDocs2.Row, 1];
        }
        private void picEdit_Grp4_Click(object sender, EventArgs e)
        {
            panDocs.Visible = true;
            fgDocs.Focus();
        }

        private void tsbDelete5_Click(object sender, EventArgs e)
        {
            if (fgDocs2.Row > 0)
            {
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    fgDocs2.RemoveItem(fgDocs2.Row);
                CreateDescription();
            }
        }

        private void tsbView5_Click(object sender, EventArgs e)
        {
            if ((fgDocs2[fgDocs2.Row, "FileFullPath"] + "").Trim() != "") Global.DMS_ShowFile("", fgDocs2[fgDocs2.Row, "FileFullPath"] + "");
            else Global.DMS_ShowFile("Customers/" + sClientName, fgDocs2[fgDocs2.Row, "File_Name"] + "");
        }

        private void txtNewAddress_LostFocus(object sender, EventArgs e)
        {
            CreateDescription();
        }
        private void txtNewCity_LostFocus(object sender, EventArgs e)
        {
            CreateDescription();
        }

        private void txtNewZip_LostFocus(object sender, EventArgs e)
        {
            CreateDescription();
        }
        private void cmbNewCountry_LostFocus(object sender, EventArgs e)
        {
            CreateDescription();
        }
        private void picCancel2_Click(object sender, EventArgs e)
        {
            panDocs2.Visible = false;
        }
        private void btnSave5_Click(object sender, EventArgs e)
        {
            string sFileFullPath = "";
            sNewFileName = "";
            for (i = 1; i <= fgDocs2.Rows.Count - 1; i++)
            {
                if (fgDocs2[i, "Status"] + "" == "0")
                {
                    sFileFullPath = (fgDocs2[i, "FileFullPath"] + "").Trim();
                    if (sFileFullPath != "")
                    {
                        sNewFileName = (fgDocs2[i, "File_Name"] + "").Trim();

                        if (Path.GetDirectoryName(sFileFullPath) != Global.DMSTransferPoint)
                        {      // Source file isn't in DMS TransferPoint folder, so ...
                            File.Copy(sFileFullPath, Global.DMSTransferPoint + "/" + sNewFileName);   // ... copy this file into DMS TransferPoint folder
                        }

                        clsServerJobs ServerJobs = new clsServerJobs();
                        ServerJobs.JobType_ID = 19;
                        ServerJobs.Source_ID = 0;
                        ServerJobs.Parameters = "{'source_file_full_name': '" + sFileFullPath.Replace(@"\", "/") + "', 'file_name': '" + sNewFileName + "', 'file_type': '" +
                                                fgDocs2[i, "DocType_ID"] + "', 'target_folder': 'Customers/" + sClientName.Replace(".", "_") + "/', 'client_id': '" +
                                                iClient_ID + "', 'status' : '1'}";
                        ServerJobs.DateStart = DateTime.Now;
                        ServerJobs.DateFinish = Convert.ToDateTime("1900/01/01");
                        ServerJobs.PubKey = "";
                        ServerJobs.PrvKey = "";
                        ServerJobs.Attempt = 0;
                        ServerJobs.Status = 0;
                        ServerJobs.InsertRecord();

                        fgDocs2[i, "Status"] = "1";
                    }
                }
            }

            if (rbOwner_No.Checked && fgDocs2.Rows.Count == 1) iStatus5 = 1;
            else iStatus5 = 0;
            picEdit_Grp5.Visible = true;
            CreateDescription();
            panDocs2.Visible = false;
        }
        private void picEdit_Grp6_Click(object sender, EventArgs e)
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
                ServerJobs.Parameters = "{'source_file_full_name': '" + sTemp.Replace(@"\", "/") + "', 'file_name': '" + sNewFileName + "', 'file_type': '" + iDocType1_ID +
                                        "', " + "'target_folder': 'Customers/" + sClientName.Replace(".", "_") + "/', 'client_id': '" +
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
        private void CreateDescription()
        {
            if (rbOwner_Yes.Checked) rbOwner_No.Checked = false;
            else rbOwner_No.Checked = true;

            if (txtNewAddress.Text.Length == 0) iStatus2 = 1;
            else iStatus2 = 0;

            if (fgDocs.Rows.Count == 1) iStatus4 = 1;
            else iStatus4 = 0;

            if (rbOwner_No.Checked)
            {
                if (fgDocs2.Rows.Count == 1) iStatus5 = 1;
                else iStatus5 = 0;
            }
            else iStatus5 = 0;


            if (iStatus2 == 1) lblStatus_Grp2.Visible = true;
            else lblStatus_Grp2.Visible = false;

            if (iStatus4 == 1) lblStatus_Grp4.Visible = true;
            else lblStatus_Grp4.Visible = false;

            if (iStatus5 == 1) lblStatus_Grp5.Visible = true;
            else lblStatus_Grp5.Visible = false;

            if (rbOwner_Yes.Checked)
            {
                picEdit_Grp5.Visible = false;
                fgDocs2.Rows.Count = 1;
            }
            if (rbOwner_No.Checked)
            {
                picEdit_Grp5.Visible = true;

                if (fgDocs2.Rows.Count > 1) lblDocs2Count.Text = (fgDocs2.Rows.Count - 1) + " αρχείο(-α)";
                else lblDocs2Count.Text = "";
            }

            if (fgDocs.Rows.Count > 1) lblDocsCount.Text = (fgDocs.Rows.Count - 1) + " αρχείο(-α)";
            else lblDocsCount.Text = "";

            if (lnkEmail.Text.Trim() == "") lblStatus_Grp6.Visible = true;
            else lblStatus_Grp6.Visible = false;


            if (iStatus2 == 1 || iStatus4 == 1 || iStatus5 == 1) lblStatus.Text = "0";
            else lblStatus.Text = "1";

            sTemp = "";
            for (i = 1; i < fgDocs.Rows.Count; i++)
                sTemp = sTemp + "{ 'file_name' : '" + fgDocs[i, "File_Name"] + "','file_type' : " + fgDocs[i, "DocType_ID"] + "}^";
            sTemp = sTemp + "~";

            for (i = 1; i < fgDocs2.Rows.Count; i++)
                sTemp = sTemp + "{ 'file_name' : '" + fgDocs2[i, "File_Name"] + "','file_type' : " + fgDocs2[i, "DocType_ID"] + "}^";

            sDescription = "{'old_address' : '" + lblOldAddress.Text + "'}~{'old_city' : '" + lblOldCity.Text + "'}~{'old_zip' : '" + lblOldZip.Text + "'}~{'old_country' : '" + lblOldCountry.Text +
                           "'}~{'new_address' : '" + txtNewAddress.Text + "'}~{'new_city' : '" + txtNewCity.Text + "'}~{'new_Zip' : '" + txtNewZip.Text + "'}~{'new_country_id' : '" +
                           cmbNewCountry.SelectedValue + "'}~{'source_email' : '" + lnkEmail.Text + "'}~{'owner' : '" + (rbOwner_Yes.Checked ? "1" : "0") + "'}~" + sTemp;
        }
        public string Description { get { return this.sDescription; } set { this.sDescription = value; } }
        public class AddressData
        {
            public string old_address { get; set; }
            public string old_city { get; set; }
            public string old_zip { get; set; }
            public string old_country { get; set; }
            public string new_address { get; set; }
            public string new_city { get; set; }
            public string new_zip { get; set; }
            public int new_country_id { get; set; }
            public string owner { get; set; }
            public string source_email { get; set; }
            public string file_name { get; set; }
            public int file_type { get; set; }
        }
    }
}
