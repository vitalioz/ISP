using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Core
{
    public partial class frmShowHistory : Form
    {
        int iContract_ID, iClient_ID, iSrcRec_ID, iRecType, iClientsList, iClientType;
        string sCode, sClientFullName, sFullFileName, sFileName;
        string[] sAktion = { "Προσθήκη", "Διόρθωση", "Ακύρωση" };
        DateTime dTemp;
        public frmShowHistory()
        {
            InitializeComponent();
        }
        private void frmShowHistory_Load(object sender, EventArgs e)
        {
            sFullFileName = "";
            sFileName = txtFileName.Text;
            clsHistory History = new clsHistory();

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString(Global.GridStyle);
            fgList.RowColChange += new EventHandler(fgList_RowColChange);
            fgList.ShowCellLabels = true;

            switch (iRecType) {
                // 1-ClientData, 2-Contracts.Code, 3-Contracts.Portfolio, 4-BankAccount Code, 5-InvestProposals, 6-, 7-ContractData, 8-ManagmentFees, 9-ShareCodes, 10-Commands, 11-BlackList
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 7:                    
                    History.RecType = iRecType;
                    History.SrcRec_ID = iSrcRec_ID;
                    History.Client_ID = iClient_ID;
                    History.Contract_ID = iContract_ID;
                    History.GetList();
                    foreach (DataRow dtRow in History.List.Rows)
                    {
                        dTemp = Convert.ToDateTime(dtRow["DateIns"]);
                        fgList.AddItem(dTemp.ToString("dd/MM/yyyy HH:MM:ss") + "\t" + sAktion[Convert.ToInt32(dtRow["Aktion"])] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" +
                                       dtRow["UserName"] + "\t" + dtRow["Notes"] + "\t" + dtRow["FileName"] + "\t" + dtRow["ID"] + "\t" +
                                       dtRow["RecType"] + "\t" + dtRow["SrcRec_ID"] + "\t" + dtRow["CurrentValues"] + "\t" + dtRow["Contract_ID"]);
                    }
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 11:
                    History.Client_ID = iClient_ID;
                    History.GetBlackList();
                    foreach (DataRow dtRow in History.List.Rows)
                    {
                        dTemp = Convert.ToDateTime(dtRow["DateIns"]);
                        fgList.AddItem(dTemp.ToString("g") + "\t" + sAktion[Convert.ToInt32(dtRow["Aktion"])] + "\t" + "" + "\t" + "" + "\t" +
                                       dtRow["UserName"] + "\t" + dtRow["Notes"] + "\t" + dtRow["FileName"] + "\t" + dtRow["ID"] + "\t" +
                                       "0" + "\t" + "0" + "\t" + dtRow["CurrentValues"] + "\t" + "0");
                    }
                    break;
            }

            cmbDocTypes.DataSource = Global.dtDocTypes.Copy();
            cmbDocTypes.DisplayMember = "Title";
            cmbDocTypes.ValueMember = "ID";

            if (fgList.Rows.Count > 1) {
                toolLeft.Visible = true;
                fgList.Row = 1;
                DefineButtons();
            }
            else toolLeft.Visible = false;

        }
        private void fgList_RowColChange(object sender, EventArgs e)
        {
            DefineButtons();
        }
        private void DefineButtons()
        {
            if ((fgList[fgList.Row, "CurrentValues"] + "") != "" || (fgList[fgList.Row, "Contract_ID"] + "") != "" )  tslElectronicDocument.Enabled = true;
            else tslElectronicDocument.Enabled = false;

            if ((fgList[fgList.Row, "File_Name"] + "") != "") {
                tslPrototypeDocument.Visible = true;
                tslAddDocument.Visible = false;
            }
            else {
                tslPrototypeDocument.Visible = false;
                tslAddDocument.Visible = true;
            }
        }
        private void tslElectronicDocument_Click(object sender, EventArgs e)
        {
            sFileName = "";

            if ((fgList[fgList.Row, "CurrentValues"] + "")  != "") {
                string[] tokens = (fgList[fgList.Row, "CurrentValues"] + "").Split('~');
                switch (Convert.ToInt32(fgList[fgList.Row, "RecType"]))
                {
                    case 1:
                        frmClientData locClientData = new frmClientData();
                        locClientData.Client_ID = Convert.ToInt32(fgList[fgList.Row, "Client_ID"]);
                        locClientData.Text = Global.GetLabel("customer_information");
                        locClientData.Show();
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        if (Convert.ToDateTime(fgList[fgList.Row, 0]) > Convert.ToDateTime("2017/06/01")) {
                            frmContract locContract = new frmContract();
                            locContract.Aktion = 1;
                            locContract.Contract_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_ID"]);
                            //locContract.PackageVersion = Convert.ToInt32(tokens[2]);
                            //locContract.CFP_ID = Convert.ToInt32(tokens[1]);
                            locContract.Client_ID = Convert.ToInt32(tokens[3]);
                            locContract.Contract_Details_ID = Convert.ToInt32(tokens[4]);
                            locContract.Contract_Packages_ID = Convert.ToInt32(tokens[5]);
                            locContract.ClientType = 1;
                            locContract.ClientFullName = "";
                            locContract.RightsLevel = 1;
                            locContract.ShowDialog();
                        }
                        break;
                    case 8:
                        break;
                    case 9:
                        break;
                }
            }
            else
                MessageBox.Show("Ιστορικά στοιχεία δεν υπάρχουν", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void tslPrototypeDocument_Click(object sender, EventArgs e)
        {
            if (iRecType == 1) Global.DMS_ShowFile("Customers/" + sClientFullName, fgList[fgList.Row, "File_Name"] + "");                 // 1 - it's personal data
            else               Global.DMS_ShowFile("Customers/" + sClientFullName + "/" + sCode, fgList[fgList.Row, "File_Name"] + "");
        }
        private void tslAddDocument_Click(object sender, EventArgs e)
        {
            cmbDocTypes.SelectedValue = 0;
            txtFileName.Text = "";
            panFileName.Visible = true;
        }
        private void picFileName_Click(object sender, EventArgs e)
        {
            sFullFileName = Global.FileChoice(Global.DefaultFolder);
            txtFileName.Text = Path.GetFileName(sFullFileName);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            int iDocFiles_ID = 0;

            fgList[fgList.Row, "File_Name"] = txtFileName.Text;            

            clsClientsDocFiles klsClientDocFiles = new clsClientsDocFiles();
            klsClientDocFiles.PreContract_ID = 0;
            klsClientDocFiles.Contract_ID = iContract_ID;
            klsClientDocFiles.Client_ID = iClient_ID;
            klsClientDocFiles.ClientName = sClientFullName;
            klsClientDocFiles.ContractCode = sCode;
            klsClientDocFiles.DocTypes = Convert.ToInt32(cmbDocTypes.SelectedValue);
            klsClientDocFiles.DMS_Files_ID = 0;
            klsClientDocFiles.OldFileName = "";
            klsClientDocFiles.NewFileName = txtFileName.Text;
            klsClientDocFiles.FullFileName = sFullFileName;
            klsClientDocFiles.DateIns = DateTime.Now;
            klsClientDocFiles.User_ID = Global.User_ID;
            klsClientDocFiles.Status = 2;                                           // 2 - document confirmed
            iDocFiles_ID = klsClientDocFiles.InsertRecord();

            clsHistory History = new clsHistory();
            History.Record_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
            History.GetRecord();
            History.DocFiles_ID = iDocFiles_ID;
            History.EditRecord();

            tslPrototypeDocument.Visible = true;
            tslAddDocument.Visible = false;
            panFileName.Visible = false;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            panFileName.Visible = false;
        }
        public int Client_ID { get { return this.iClient_ID; } set { this.iClient_ID = value; } }
        public int Contract_ID { get { return this.iContract_ID; } set { this.iContract_ID = value; } }
        public int SrcRec_ID { get { return this.iSrcRec_ID; } set { this.iSrcRec_ID = value; } }
        public int RecType { get { return this.iRecType; } set { this.iRecType = value; } }
        public string Code { get { return this.sCode; } set { this.sCode = value; } }
        public string ClientFullName { get { return this.sClientFullName; } set { this.sClientFullName = value; } }
        public int ClientsList { get { return this.iClientsList; } set { this.iClientsList = value; } }
        public int ClientType { get { return this.iClientType; } set { this.iClientType = value; } }
    }
}
