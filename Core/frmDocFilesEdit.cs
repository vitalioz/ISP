using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Core
{
    public partial class frmDocFilesEdit : Form
    {
        int iAktion, iMode, iRec_ID, iClient_ID, iContract_ID, iShare_ID, iDocTypes, iPD_Group_ID, iDMS_Files_ID;
        string sClientFullName, sCode, sFullFileName, sOldFileName;
        public frmDocFilesEdit()
        {
            InitializeComponent();
        }
        private void frmDocFilesEdit_Load(object sender, EventArgs e)
        {
            sFullFileName = "";
            sOldFileName = txtFileName.Text;

            cmbDocTypes.DataSource = Global.dtDocTypes.Copy();
            cmbDocTypes.DisplayMember = "Title";
            cmbDocTypes.ValueMember = "ID";
            cmbDocTypes.SelectedValue = iDocTypes;
        }
        private void picDocFilesPath_Click(object sender, EventArgs e)
        {
            sFullFileName = Global.FileChoice(Global.DefaultFolder);
            txtFileName.Text = Path.GetFileName(sFullFileName);
        }

        private void picShow_Click(object sender, EventArgs e)
        {
            if (sFullFileName.Length == 0) Global.DMS_ShowFile("Customers/" + sClientFullName + "/" + sCode, Path.GetFileName(txtFileName.Text));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {            
            switch (iMode)
            {
                case 1:
                    clsClientsDocFiles ClientDocFiles = new clsClientsDocFiles();
                    ClientDocFiles.Contract_ID = iContract_ID;
                    ClientDocFiles.Client_ID = iClient_ID;
                    ClientDocFiles.ClientName = sClientFullName + "";
                    ClientDocFiles.ContractCode = sCode + "";
                    ClientDocFiles.DocTypes = Convert.ToInt32(cmbDocTypes.SelectedValue);
                    ClientDocFiles.PD_Group_ID = iPD_Group_ID;
                    ClientDocFiles.DMS_Files_ID = iDMS_Files_ID;
                    ClientDocFiles.OldFileName = sOldFileName;
                    ClientDocFiles.NewFileName = txtFileName.Text + "";
                    ClientDocFiles.FullFileName = sFullFileName + "";
                    ClientDocFiles.DateIns = DateTime.Now;
                    ClientDocFiles.User_ID = Global.User_ID;
                    ClientDocFiles.Status = 2;                                           // 2 - document confirmed
                    if (iAktion == 0)
                    {
                        ClientDocFiles.PreContract_ID = 0;                        
                        ClientDocFiles.InsertRecord();
                    }
                    else
                    {
                        ClientDocFiles.Record_ID = iRec_ID;
                        ClientDocFiles.EditRecord();
                    }
                    break;
                case 2:
                    clsProductsDocFiles ProductDocFiles = new clsProductsDocFiles();
                    ProductDocFiles.Share_ID = iShare_ID;
                    ProductDocFiles.DocTypes = Convert.ToInt32(cmbDocTypes.SelectedValue);
                    ProductDocFiles.DMS_Files_ID = iDMS_Files_ID;
                    ProductDocFiles.NewFileName = txtFileName.Text + "";
                    ProductDocFiles.FullFileName = sFullFileName + "";
                    ProductDocFiles.DateIns = DateTime.Now;
                    if (iAktion == 0) ProductDocFiles.InsertRecord();
                    else
                    { 
                       ProductDocFiles.Record_ID = iRec_ID;
                       ProductDocFiles.EditRecord();
                    }
                    break;
            }
            iAktion = 1;                                                      //  was saved (added)
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            iAktion = 0;                                                    // don't saved(cancelled)
            this.Close();
        }
        public int Aktion { get { return this.iAktion; } set { this.iAktion = value; } }
        public int Mode { get { return this.iMode; } set { this.iMode = value; } }
        public int Rec_ID { get { return this.iRec_ID; } set { this.iRec_ID = value; } }
        public int Client_ID { get { return this.iClient_ID; } set { this.iClient_ID = value; } }
        public int Contract_ID { get { return this.iContract_ID; } set { this.iContract_ID = value; } }
        public int Share_ID { get { return this.iShare_ID; } set { this.iShare_ID = value; } }
        public int DocTypes { get { return this.iDocTypes; } set { this.iDocTypes = value; } }
        public int PD_Group_ID { get { return this.iPD_Group_ID; } set { this.iPD_Group_ID = value; } }
        public int DMS_Files_ID { get { return this.iDMS_Files_ID; } set { this.iDMS_Files_ID = value; } }
        public string ClientFullName { get { return this.sClientFullName; } set { this.sClientFullName = value; } }    
        public string Code { get { return this.sCode; } set { this.sCode = value; } }
    }
}
