using Core;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;

namespace Contracts
{
    public partial class ucW8BEN : UserControl
    {
        int i = 0;
        string sTemp = "", sDescription = "", sClientName = "", sNewFileName = "";
        CountryData Country_Data;
        public ucW8BEN()
        {
            InitializeComponent();

            panDocs.Left = 68;
            panDocs.Top = 12;
        }

        private void ucW8BEN_Load(object sender, EventArgs e)
        {

        }
        public void StartInit(int iStatus, string sCN)          // iStatus = 0 - New record, 1 - New (temporary saved), 2 - Sended for checking,  3 - OK (after checking), 4 - problem (after checking), -1 - Cancelled
        {
            sClientName = sCN;

            sTemp = sDescription;
            cmbNewW8BEN.DataSource = Global.dtCountries.Copy();
            cmbNewW8BEN.DisplayMember = "Title";
            cmbNewW8BEN.ValueMember = "ID";
            cmbNewW8BEN.SelectedValue = 1;

            //------- fgDocs ----------------------------
            fgDocs.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgDocs.Styles.ParseString(Global.GridStyle);


            cmbNewW8BEN.SelectedValue = 0;
            sDescription = sTemp;

            //--- define request's parameters -----------------------------------------------                        
            if (sDescription.Length > 0)
            {
                string[] tokens = sDescription.Split('~');

                Country_Data = JsonConvert.DeserializeObject<CountryData>(tokens[0]);
                lblOldW8BEN.Text = Country_Data.old_country;

                Country_Data = JsonConvert.DeserializeObject<CountryData>(tokens[1]);
                cmbNewW8BEN.Text = Country_Data.new_country;

                Country_Data = JsonConvert.DeserializeObject<CountryData>(tokens[2]);
                cmbNewW8BEN.SelectedValue = Country_Data.new_country_id;

                Country_Data = JsonConvert.DeserializeObject<CountryData>(tokens[3]);
                lnkEmail.Text = Country_Data.source_email;

                string[] bokens = tokens[4].Split('^');
                for (i = 0; i < bokens.Length - 1; i++)
                {
                    Country_Data = JsonConvert.DeserializeObject<CountryData>(bokens[i]);
                    if (Country_Data.file_name.Length > 0)
                    {
                        sTemp = Country_Data.file_name + "\t" + Country_Data.file_id + "\t" + "";
                        fgDocs.AddItem(sTemp);
                    }
                }
            }
            CreateDescription();
        }
        private void picEdit_Grp3_Click(object sender, EventArgs e)
        {
            panDocs.Visible = true;
            fgDocs.Focus();
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            sTemp = Global.FileChoice(Global.DefaultFolder);
            if (sTemp.Length > 0)
            {
                fgDocs.AddItem("" + "\t" + "0" + "\t" + "");
                fgDocs[fgDocs.Rows.Count - 1, 2] = sTemp;
                fgDocs[fgDocs.Rows.Count - 1, 0] = Path.GetFileNameWithoutExtension(sTemp) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(sTemp);
            }
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
            if ((fgDocs[fgDocs.Row, 2] + "").Trim() != "") Global.DMS_ShowFile("", fgDocs[fgDocs.Row, 2] + "");
            else Global.DMS_ShowFile("Customers\\" + sClientName + "\\", fgDocs[fgDocs.Row, 0] + "");
        }
        private void btnCancel3_Click(object sender, EventArgs e)
        {
            panDocs.Visible = false;
        }

        private void btnSave3_Click(object sender, EventArgs e)
        {
            sNewFileName = "";
            for (i = 1; i <= fgDocs.Rows.Count - 1; i++)
            {
                if (Convert.ToInt32(fgDocs[i, 1]) == 0)
                {
                    sNewFileName = fgDocs[i, 0] + "";
                    if (Global.DMSTransferPoint.Length == 0)                                                     // DMS TransferPoint is Empty
                        sNewFileName = Global.DMS_UploadFile(fgDocs[i, 2] + "", "Customers/" + sClientName.Replace(".", "_"), sNewFileName);
                    else
                    {
                        if (Path.GetDirectoryName(fgDocs[i, 2] + "") != Global.DMSTransferPoint)
                        {      // Source file isn't in DMS TransferPoint folder, so ...
                            if (File.Exists(Global.DMSTransferPoint + "/" + sNewFileName))
                                sNewFileName = Path.GetFileNameWithoutExtension(sNewFileName) + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(sNewFileName);
                            File.Copy(fgDocs[i, 2] + "", Global.DMSTransferPoint + "/" + sNewFileName);         // ... copy this file into DMS TransferPoint folder
                        }

                        clsServerJobs ServerJobs = new clsServerJobs();
                        ServerJobs.JobType_ID = 15;
                        ServerJobs.Source_ID = 0;
                        ServerJobs.Parameters = "{'file_name': '" + sNewFileName + "', 'target_folder':'" + "Customers/" + sClientName.Replace(".", "_") + "/'}";
                        ServerJobs.DateStart = DateTime.Now;
                        ServerJobs.DateFinish = Convert.ToDateTime("1900/01/01");
                        ServerJobs.PubKey = "";
                        ServerJobs.PrvKey = "";
                        ServerJobs.Attempt = 0;
                        ServerJobs.Status = 0;
                        ServerJobs.InsertRecord();

                        sNewFileName = "Q:/" + "Customers/" + sClientName.Replace(".", "_") + "/" + sNewFileName;
                    }
                }
            }

            CreateDescription();
            panDocs.Visible = false;
        }
        private void picEdit_Grp4_Click(object sender, EventArgs e)
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
                ServerJobs.JobType_ID = 15;
                ServerJobs.Source_ID = 0;
                ServerJobs.Parameters = "{'file_name': '" + lnkEmail.Text + "', 'target_folder':'" + "Customers/" + sClientName.Replace(".", "_") + "/'}";
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
            Global.DMS_ShowFile("Customers\\" + sClientName + "\\", lnkEmail.Text);
        }

        private void cmbNewCountry_SelectedValueChanged(object sender, EventArgs e)
        {
            CreateDescription();
        }
        private void CreateDescription()
        {
            string sTemp = "";
            for (i = 1; i < fgDocs.Rows.Count; i++)
            {
                sTemp = sTemp + "{'file_name' : '" + fgDocs[i, 0] + "','file_id' : " + fgDocs[i, "ID"] + "}^";
            }
            sDescription = "{'old_country' : '" + lblOldW8BEN.Text + "'}~{'new_country' : '" + cmbNewW8BEN.Text + "'}~{'new_country_id' : '" + cmbNewW8BEN.SelectedValue + "'}~" +
                           "{'source_email' : '" + lnkEmail.Text + "'}~" + sTemp;


            if (fgDocs.Rows.Count > 1) lblDocsCount.Text = (fgDocs.Rows.Count - 1) + " αρχείο(-α)";
            else lblDocsCount.Text = "";

            if (cmbNewW8BEN.Text.Trim() == "") lblStatus_Grp2.Visible = true;
            else lblStatus_Grp2.Visible = false;

            if (fgDocs.Rows.Count == 1) lblStatus_Grp3.Visible = true;
            else lblStatus_Grp3.Visible = false;

            if (lnkEmail.Text.Trim() == "") lblStatus_Grp4.Visible = true;
            else lblStatus_Grp4.Visible = false;

            if (!lblStatus_Grp2.Visible && !lblStatus_Grp3.Visible) lblStatus.Text = "1";
            else lblStatus.Text = "0";
        }

        public string Description { get { return this.sDescription; } set { this.sDescription = value; } }
        public class CountryData
        {
            public string old_country { get; set; }
            public int old_country_id { get; set; }
            public string new_country { get; set; }
            public int new_country_id { get; set; }
            public string source_email { get; set; }
            public int file_id { get; set; }
            public string file_name { get; set; }
        }
    }
}
