using Core;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;

namespace Contracts
{
    public partial class ucBankAccount_Delete : UserControl
    {
        int iClient_ID = 0;
        string sTemp = "", sDescription = "", sClientName = "", sNewFileName = "";
        bool bCheckList = false;
        BankAccData Bank_AccData;
        clsClients_BankAccounts Clients_BankAccounts = new clsClients_BankAccounts();

        public ucBankAccount_Delete()
        {
            InitializeComponent();
        }
        private void ucBankAccount_Delete_Load(object sender, EventArgs e)
        {
        }
        // iStatus = 0 - New record, 1 - New (temporary saved), 2 - Sended for checking,  3 - OK (after checking), 4 - problem (after checking), -1 - Cancelled
        public void StartInit(int iStatus, string sCN, int iKlient_ID, int iBankAccount_ID, int iDefaultDocType1_ID)
        {
            bCheckList = false;
            sClientName = sCN;
            iClient_ID = iKlient_ID;
            lblBankAccount_ID.Text = iBankAccount_ID.ToString();

            lblAccNumber.Text = "";
            lblCurrencies.Text = "";
            lblType.Text = "";
            lblOwners.Text = "";
            lnkEmail.Text = "";
            lblBankTitle.Text = "";

            //--- define request's parameters -----------------------------------------------                        
            if (sDescription.Length > 0)
            {
                string[] tokens = sDescription.Split('~');

                Bank_AccData = JsonConvert.DeserializeObject<BankAccData>(tokens[0]);
                lnkEmail.Text = Bank_AccData.source_email;

                Bank_AccData = JsonConvert.DeserializeObject<BankAccData>(tokens[1]);
                lblBankAccount_ID.Text = Bank_AccData.bank_acc_id.ToString();
            }

            if (lblBankAccount_ID.Text != "0")
            {
                Clients_BankAccounts = new clsClients_BankAccounts();
                Clients_BankAccounts.Record_ID = Convert.ToInt32(lblBankAccount_ID.Text);
                Clients_BankAccounts.GetRecord();
                lblAccNumber.Text = Clients_BankAccounts.AccNumber;
                lblBankTitle.Text = Clients_BankAccounts.Bank_Title;
                lblCurrencies.Text = Clients_BankAccounts.Currency;
                lblType.Text = Clients_BankAccounts.AccType == 0 ? "ΟΧΙ" : "ΝΑΙ";
                lblOwners.Text = Clients_BankAccounts.AccOwners;
            }

            bCheckList = true;

            CreateDescription();
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
                ServerJobs.JobType_ID = 19;
                ServerJobs.Source_ID = 0;
                ServerJobs.Parameters = "{'source_file_full_name': '" + sTemp.Replace(@"\", "/") + "', 'file_name': '" + sNewFileName + "', 'file_type': '9434', " +
                                        "'target_folder': 'Customers/" + sClientName.Replace(".", "_") + "/', 'client_id': '" +
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
            if (bCheckList)
            {
                if (lnkEmail.Text.Trim() == "") lblStatus.Text = "0";
                else lblStatus.Text = "1";

                if (lnkEmail.Text.Trim() == "") lblStatus_Grp4.Visible = true;
                else lblStatus_Grp4.Visible = false;

                sDescription = "{'source_email' : '" + lnkEmail.Text + "'}~{'bank_acc_id' : '" + lblBankAccount_ID.Text + "', 'status' : '0'}~";
            }
        }
        public string Description { get { return this.sDescription; } set { this.sDescription = value; } }
        public class BankAccData
        {
            public int bank_acc_id { get; set; }
            public int status { get; set; }
            public string source_email { get; set; }
        }
    }
}
