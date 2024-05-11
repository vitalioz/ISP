using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

namespace Core
{
    public partial class ucContractsSearch : UserControl
    {
        int i, iMode, iShowWidth, iShowHeight, iListType, iMaxWidth, iMaxHeight, iLocClient_ID, iLocContract_ID, iOldClient_ID, iOldContract_ID;
        string sTemp, s1, s2, sStatus, sFilters, sCodesList, sSurnameGreek = "", sSurnameEnglish = "";
        string[] sMIFIDCategories = { "-", "Ιδιώτης Πελάτης", "Επαγγελματίας Πελάτης", "Επιλέξιμοι Αντισυμβαλλόμενοι" };
        bool bShowContractsList;
        Global.ContractData locContractData = new Global.ContractData();
        DataView dtView;
        CellStyle csCancel;

        public event EventHandler TextOfLabelChanged;
        public event EventHandler ButtonClick;

        public ucContractsSearch()
        {
            InitializeComponent();
        }
        private void ucContracts_Load(object sender, EventArgs e)
        {
            iOldClient_ID = 0;
            iOldContract_ID = 0;
            bShowContractsList = true;
            sTemp = "";
            sStatus = "";
            if (sFilters.Trim().Length == 0) sFilters = "Client_ID > 0 AND Status = 1";
            s1 = "";
            s2 = "";

            //------- fgList ----------------------------
            fgList.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:Gold; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:Gold; ForeColor:Black;}");
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.DoubleClick += new System.EventHandler(fgList_DoubleClick);
            fgList.MouseDown += new MouseEventHandler(fgList_MouseDown);
            fgList.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgList_BeforeEdit);

            csCancel = fgList.Styles.Add("Cancelled");
            csCancel.ForeColor = Color.Red;
        }
        public void StartInit(int iWidth, int iHeight, int iTxtWidth, int iTxtHeight, int iShownListType)
        {
            bShowContractsList = false;
            txtContractTitle.Text = "";
            bShowContractsList = true;

            iMaxWidth = iWidth ;
            iMaxHeight = iHeight;
            iShowWidth = iTxtWidth;
            iShowHeight = iTxtHeight;
            iListType = iShownListType;

            Contract_ID.Text = "-999";

            if (iShowWidth != 0) txtContractTitle.Width = iShowWidth;
            if (iShowHeight != 0) txtContractTitle.Height = iShowHeight;

            this.Width = txtContractTitle.Width;
            this.Height = txtContractTitle.Height;
        }
        protected override void OnResize(EventArgs e)
        {
            panList.Width = this.Width - 1;
            panList.Height = this.Height - 22;

            if (iMode == 2) fgList.Height = this.Height - 90;
            else fgList.Height = this.Height - 60;

            fgList.Width = this.Width - 15;

            picClose.Left = this.Width - 26;
        }
        private void txtContractTitle_TextChanged(object sender, EventArgs e)
        {
            if (bShowContractsList) {
                if (iMode == 2) {
                    chkSelect.Visible = true;
                    fgList.Cols[0].Visible = true;
                    fgList.Cols[8].Width = 55;
                    btnChoice.Visible = true;
                    //iMaxWidth = 784;
                    //iMaxHeight = 400;
                }
                else
                {
                    chkSelect.Visible = false;
                    fgList.Cols[0].Visible = false;
                    fgList.Cols[8].Width = 80;
                    btnChoice.Visible = false;
                    //iMaxWidth = 784;
                    //iMaxHeight = 400;
                }

                this.Width = iMaxWidth;
                this.Height = iMaxHeight;

                s1 = txtContractTitle.Text;
                DataFiltering();
            }
        }
        private void txtContractTitle_KeyPress(object sender, KeyPressEventArgs e)
        {  
            switch (e.KeyChar)
            {
                case (char)Keys.Enter:
                    ContractChoice();
                    break;
                case (char)Keys.Escape:
                    this.Width = txtContractTitle.Width;
                    this.Height = txtContractTitle.Height;
                    break;
                case (char)Keys.Tab:
                    if (fgList.Rows.Count > 1) {
                        fgList.Row = 1;
                        fgList.Focus();
                    }
                    break;
            }
        }
        private void mnuClientData_Click(object sender, EventArgs e)
        {
            frmClientData locClientData = new frmClientData();
            locClientData.Client_ID = Convert.ToInt32(fgList[fgList.Row, "Client_ID"]);
            locClientData.Text = Global.GetLabel("customer_information");
            locClientData.Show();
        }

        private void mnuContractData_Click(object sender, EventArgs e)
        {
            frmContract locContract = new frmContract();
            locContract.Aktion = 1;
            locContract.Contract_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_ID"]);
            locContract.Contract_Details_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_Details_ID"]);
            locContract.Contract_Packages_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_Packages_ID"]);
            locContract.Client_ID = Convert.ToInt32(fgList[fgList.Row, "Client_ID"]);
            locContract.ClientType = 1;
            locContract.ClientFullName = fgList[fgList.Row, "ClientName"] + "";
            locContract.RightsLevel = 1;
            locContract.ShowDialog();
        }

        private void DataFiltering()
        {
            Contract_ID.Text = "-888";

            fgList.Redraw = false;
            fgList.Rows.Count = 1;

            Global.TranslateUserName(s1, out sSurnameGreek, out sSurnameEnglish);

            switch (iListType)
            {
                case 1:
                        iOldClient_ID = -999;
                        iOldContract_ID = -999;
                        dtView = Global.dtContracts.Copy().DefaultView;
                        if (txtContractTitle.Text.IndexOf("/") < 0)
                        {
                            s1 = txtContractTitle.Text;
                            sTemp = sFilters + " AND (Fullname LIKE '%" + sSurnameEnglish + "%' OR Fullname LIKE '%" + sSurnameGreek + "%' OR Code LIKE '%" + s1 + 
                                               "%' OR Portfolio LIKE '%" + s1 + "%' OR ContractTitle LIKE '%" + sSurnameEnglish + "%' OR ContractTitle LIKE '%" + sSurnameGreek + 
                                               "%' OR NumberAccount LIKE '%" + s1 + "%') ";                                   // was      AND Tipos <> 3 
                        dtView.RowFilter = sTemp;
                        }
                        else
                        {
                            i = txtContractTitle.Text.IndexOf("/");
                            s1 = txtContractTitle.Text.Substring(0, i);
                            s2 = txtContractTitle.Text.Substring(i + 1);
                            sTemp = sFilters + " AND Code = '" + s1 + "' AND Portfolio LIKE '%" + s2 + "%' AND Tipos <> 3 ";
                            dtView.RowFilter = sTemp;
                        };

                        foreach (DataRowView dtViewRow in dtView) {

                            iLocClient_ID = Convert.ToInt32(dtViewRow["Client_ID"]);
                            iLocContract_ID = Convert.ToInt32(dtViewRow["Contract_ID"]);

                            if ((iOldClient_ID != iLocClient_ID) || (iOldContract_ID != iLocContract_ID))
                            {
                                iOldClient_ID = iLocClient_ID;
                                iOldContract_ID = iLocContract_ID;
                                sStatus = "1";
                                if (Convert.ToInt32(dtViewRow["Status"]) == 0) sStatus = "0";

                                fgList.AddItem(false + "\t" + dtViewRow["ContractTitle"] + "\t" + dtViewRow["Code"] + "\t" + dtViewRow["Portfolio"] + "\t" + dtViewRow["Fullname"] + "\t" +
                                               dtViewRow["Service_Title"] + "\t" + dtViewRow["InvestmentProfile_Title"] + "\t" + dtViewRow["InvestmentPolicy_Title"] + "\t" +
                                               dtViewRow["ServiceProvider_Title"] + "\t" + dtViewRow["Package_Title"] + "\t" + dtViewRow["Currency"] + "\t" + dtViewRow["ContractEMail"] + "\t" +
                                               dtViewRow["ContractMobile"] + "\t" + dtViewRow["NumberAccount"] + "\t" + dtViewRow["Contract_ID"] + "\t" + dtViewRow["Client_ID"] + "\t" +
                                               dtViewRow["ServiceProvider_ID"] + "\t" + dtViewRow["ServiceProvider_Type"] + "\t" + dtViewRow["InvestmentPolicy_ID"] + "\t" + 
                                               dtViewRow["InvestmentProfile_ID"] + "\t" + dtViewRow["Service_ID"] + "\t" + sStatus + "\t" + dtViewRow["Tipos"] + "\t" + 
                                               dtViewRow["CFP_ID"] + "\t" + dtViewRow["Contracts_Details_ID"] + "\t" + dtViewRow["Contracts_Packages_ID"] + "\t" +
                                               dtViewRow["ContractType"] + "\t" + dtViewRow["MIFID_Risk_Index"] + "\t" + dtViewRow["MIFIDCategory_ID"] + "\t" + 
                                               dtViewRow["MIFID_2"] + "\t" + dtViewRow["XAA"] + "\t" + dtViewRow["VAT_Percent"]);
                            }
                        }
                        break;
                case 2:
                    dtView = Global.dtContracts.Copy().DefaultView;
                    //sFilters = sFilters + " AND Status = 1 ";
                    //sFilters = sFilters + " AND (Contracts_Details_ID = Contract_Details_ID AND Contracts_Packages_ID = Contract_Packages_ID) ";
                    if (txtContractTitle.Text.IndexOf("/") < 0)
                    {
                        s1 = txtContractTitle.Text;
                        sTemp = sFilters + " AND (Fullname LIKE '%" + sSurnameEnglish + "%' OR Fullname LIKE '%" + sSurnameGreek + "%' OR Code LIKE '%" + s1 +
                                           "%' OR Portfolio LIKE '%" + s1 + "%' OR ContractTitle LIKE '%" + sSurnameEnglish + "%' OR ContractTitle LIKE '%" + sSurnameGreek +
                                           "%' OR NumberAccount LIKE '%" + s1 + "%')";
                        dtView.RowFilter = sTemp;
                    }
                    else
                    {
                        i = txtContractTitle.Text.IndexOf("/");
                        s1 = txtContractTitle.Text.Substring(0, i);
                        s2 = txtContractTitle.Text.Substring(i + 1);
                        sTemp = sFilters + " AND Code = '" + s1 + "' AND  Portfolio LIKE '%" + s2 + "%'";
                        dtView.RowFilter = sTemp;
                    };

                    foreach (DataRowView dtViewRow in dtView)
                    {
                        sStatus = "1";
                        if (Convert.ToInt32(dtViewRow["Status"]) == 0) sStatus = "0";

                        fgList.AddItem(false + "\t" + dtViewRow["ContractTitle"] + "\t" + dtViewRow["Code"] + "\t" + dtViewRow["Portfolio"] + "\t" + dtViewRow["Fullname"] + "\t" +
                                        dtViewRow["Service_Title"] + "\t" + dtViewRow["InvestmentProfile_Title"] + "\t" + dtViewRow["InvestmentPolicy_Title"] + "\t" +
                                        dtViewRow["ServiceProvider_Title"] + "\t" + dtViewRow["Package_Title"] + "\t" + dtViewRow["Currency"] + "\t" + dtViewRow["ContractEMail"] + "\t" +
                                        dtViewRow["ContractMobile"] + "\t" + dtViewRow["NumberAccount"] + "\t" + dtViewRow["Contract_ID"] + "\t" + dtViewRow["Client_ID"] + "\t" +
                                        dtViewRow["ServiceProvider_ID"] + "\t" + dtViewRow["ServiceProvider_Type"] + "\t" + dtViewRow["InvestmentPolicy_ID"] + "\t" + 
                                        dtViewRow["InvestmentProfile_ID"] + "\t" + dtViewRow["Service_ID"] + "\t" + sStatus + "\t" + dtViewRow["Tipos"] + "\t" + 
                                        dtViewRow["CFP_ID"] + "\t" + dtViewRow["Contracts_Details_ID"] + "\t" + dtViewRow["Contracts_Packages_ID"] + "\t" +
                                        dtViewRow["ContractType"] + "\t" + dtViewRow["MIFID_Risk_Index"] + "\t" + dtViewRow["MIFIDCategory_ID"] + "\t" + 
                                        dtViewRow["MIFID_2"] + "\t" + dtViewRow["XAA"] + "\t" + dtViewRow["VAT_Percent"]);
                    }
                    break;
                case 3:
                        break;
                case 4:
                        break;
            }

            fgList.Sort(SortFlags.Ascending, 1);
            fgList.Redraw = true;
        }

        private void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            for (i = 1; i <= fgList.Rows.Count - 1; i++) fgList[i, 0] = chkSelect.Checked;
        }

        private void fgList_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Col == 21)                                                                                    // 21 - ContractStatus
            {
               if (Convert.ToInt32(fgList[e.Row, 21]) == 0) fgList.Rows[e.Row].Style = csCancel;
            }
        }
        private void fgList_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (iMode == 2)  {
                if (e.Col == 0) e.Cancel = false;
                else e.Cancel = true;
            }
        }
        private void fgList_DoubleClick(object sender, EventArgs e)
        {
            ContractChoice();
        }
        private void ContractChoice()
        {
            if (txtContractTitle.Text.Length > 0) DefineClientData();
            else {
                Contract_ID.Text = "0";
                Contract_ID.Text = locContractData.Contract_ID.ToString();
            }

            this.Width = txtContractTitle.Width;
            this.Height = txtContractTitle.Height;
        }
        private void fgList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                fgList.ContextMenuStrip = mnuContext;
                fgList.Row = fgList.MouseRow;
            }
        }
        private void DefineClientData()
        {
            locContractData.ContractTitle = fgList[fgList.Row, "ContractTitle"].ToString();
            locContractData.Code = fgList[fgList.Row, "Code"].ToString();
            locContractData.Portfolio = fgList[fgList.Row, "Portfolio"].ToString();
            locContractData.ClientName = fgList[fgList.Row, "ClientName"].ToString();
            locContractData.Service_Title = fgList[fgList.Row, "Service_Title"].ToString();
            locContractData.Profile_Title = fgList[fgList.Row, "Profile_Title"].ToString();
            locContractData.Policy_Title = fgList[fgList.Row, "Policy_Title"].ToString();
            locContractData.Provider_Title = fgList[fgList.Row, "Provider_Title"].ToString();
            locContractData.Package_Title = fgList[fgList.Row, "Package_Title"].ToString();            
            locContractData.Currency = fgList[fgList.Row, "Currency"].ToString();
            locContractData.EMail = fgList[fgList.Row, "EMail"].ToString();
            locContractData.Mobile = fgList[fgList.Row, "Mobile"].ToString();
            locContractData.NumberAccount = fgList[fgList.Row, "NumberAccount"].ToString();
            locContractData.Contract_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_ID"]);
            locContractData.ContractType = Convert.ToInt32(fgList[fgList.Row, "ContractType"]);
            locContractData.Client_ID = Convert.ToInt32(fgList[fgList.Row, "Client_ID"]);
            locContractData.Provider_ID = Convert.ToInt32(fgList[fgList.Row, "Provider_ID"]);
            locContractData.ProviderType = Convert.ToInt32(fgList[fgList.Row, "ProviderType"]);
            locContractData.Policy_ID = Convert.ToInt32(fgList[fgList.Row, "InvestPolicy_ID"]);
            locContractData.Profile_ID = Convert.ToInt32(fgList[fgList.Row, "InvestProfile_ID"]);
            locContractData.Service_ID = Convert.ToInt32(fgList[fgList.Row, "Service_ID"]);
            locContractData.Status = Convert.ToInt32(fgList[fgList.Row, "Status"]);
            locContractData.ClientType = Convert.ToInt32(fgList[fgList.Row, "ClientType"]);
            locContractData.VAT_Percent = Convert.ToSingle(fgList[fgList.Row, "VAT_Percent"]);
            locContractData.CFP_ID = Convert.ToInt32(fgList[fgList.Row, "CFP_ID"]);
            locContractData.Contracts_Details_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_Details_ID"]);
            locContractData.Contracts_Packages_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_Packages_ID"]);
            locContractData.MIFID_Risk_Index = Convert.ToInt32(fgList[fgList.Row, "MIFID_Risk_Index"]);
            locContractData.MIFIDCategory_ID = Convert.ToInt32(fgList[fgList.Row, "MIFIDCategory_ID"]);
            locContractData.MIFID_2 = Convert.ToInt32(fgList[fgList.Row, "MIFID_2"]);
            locContractData.XAA = Convert.ToInt32(fgList[fgList.Row, "XAA"]);

            locContractData.MIFIDCategory_Title = sMIFIDCategories[locContractData.MIFIDCategory_ID];

            bShowContractsList = false;
            txtContractTitle.Text = fgList[fgList.Row, "ContractTitle"].ToString();
            bShowContractsList = true;
            Contract_ID.Text = fgList[fgList.Row, "Contract_ID"].ToString();
        }
        protected void btnChoice_Click(object sender, EventArgs e)
        {
            sCodesList = "";
            for (i = 1; i <= fgList.Rows.Count - 1; i++)
            {
                if (Convert.ToBoolean(fgList[i, 0]))
                    sCodesList = sCodesList + fgList[i, "ContractTitle"] + "\t" + fgList[i, "Code"] + "\t" + fgList[i, "Portfolio"] + "\t" + fgList[i, "Contract_ID"] + "\t" + fgList[i, "Status"] + "~";
            }
            Contract_ID.Text = "-999";                                 // -1 - multiple records choice

            //bubble the event up to the parent
            //if (this.ButtonClick != null)
            //    this.ButtonClick(this, e);

            this.Width = txtContractTitle.Width;
            this.Height = txtContractTitle.Height;
        }
        public void Contract_ID_TextChanged(object sender, EventArgs e)
        {
            if (Contract_ID.Text != "-888")
                if (TextOfLabelChanged != null)
                TextOfLabelChanged(this, e);
        } 
        private void picClose_Click(object sender, EventArgs e)
        {
            this.Width = txtContractTitle.Width;
            this.Height = txtContractTitle.Height;
        }
        public bool ShowClientsList { get { return this.bShowContractsList; } set { this.bShowContractsList = value; } }
        public int Mode { get { return this.iMode; } set { this.iMode = value; } }                              // 1 - one record selection mode, 2 - multiple records selection mode
        public int ShowWidth { get { return this.iShowWidth; } set { this.iShowWidth = value; } }
        public int ShowHeight { get { return this.iShowHeight; } set { this.iShowHeight = value; } }
        public string Filters { get { return this.sFilters; } set { this.sFilters = value; } }
        public Global.ContractData SelectedContractData { get { return this.locContractData; } set { this.locContractData = value; } }
        public string CodesList { get { return this.sCodesList; } set { this.sCodesList = value; } }
        public int ListType { get { return this.iListType; } set { this.iListType = value; } }                     // 1 - Contracts, 2 - HF SS Codes, 3 - HF Accounts
    }
}
