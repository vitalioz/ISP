using System;
using System.Data;
using System.Windows.Forms;
using Core;

namespace Accounting
{
    public partial class frmGenikoLogistikoSxedio : Form
    {
        DataView dtView;
        int i, iContract_ID, iAktion, iOwner_ID, iShareCodes_ID, iDepository_ID, iProvider_ID, iCurrency_ID, iStatus, iRightsLevel;
        string sExtra, sISIN, sCurrency, sDepository_Code, sProvider_Code;
        bool bCheckList = false;
        DataRow[] foundRows;
        Global.ContractData stContractData;
        clsGAP GAP = new clsGAP();
        public frmGenikoLogistikoSxedio()
        {
            InitializeComponent();

            panAcc1_1.Left = 34;
            panAcc1_1.Top = 116;

            panAcc1_2.Left = 34;
            panAcc1_2.Top = 116;

            panAcc2_1.Left = 34;
            panAcc2_1.Top = 116;

            panAcc2_2.Left = 34;
            panAcc2_2.Top = 116;

            cmbAccGroup.SelectedIndex = 0;
            cmbAccType.SelectedIndex = 0;
        }

        private void frmGenikoLogistikoSxedio_Load(object sender, EventArgs e)
        {

            ucCS.StartInit(488, 240, 488, 20, 1);
            ucCS.TextOfLabelChanged += new EventHandler(ucCS_TextChanged);
            ucCS.Filters = " Client_ID > 0 AND Status = 1 AND ServiceProvider_ID = 9";
            ucCS.Mode = 1;
            ucCS.ListType = 1;

            ucCS2.StartInit(488, 240, 488, 20, 1);
            ucCS2.TextOfLabelChanged += new EventHandler(ucCS2_TextChanged);
            ucCS2.Filters = " Client_ID > 0 AND Status = 1 AND ServiceProvider_ID = 9";
            ucCS2.Mode = 1;
            ucCS2.ListType = 1;

            ucPS.StartInit(488, 160, 200, 20, 1);
            ucPS.TextOfLabelChanged += new EventHandler(ucPS_TextChanged);
            ucPS.Filters = "Aktive >= 1 ";
            ucPS.ListType = 1;                                                                  // iListType = 1 : Global.dtProducts - common list of products
            ucPS.ShowNonAccord = false;                                                         // Show NonAccordable products (oxi katallila) with red Background
            ucPS.ShowCancelled = false;                                                         // Don't show cancelled products

            ucPS4.StartInit(488, 220, 200, 20, 1);
            ucPS4.TextOfLabelChanged += new EventHandler(ucPS4_TextChanged);
            ucPS4.Filters = "Aktive >= 1 ";
            ucPS4.ListType = 1;                                                                  // iListType = 1 : Global.dtProducts - common list of products
            ucPS4.ShowNonAccord = false;                                                         // Show NonAccordable products (oxi katallila) with red Background
            ucPS4.ShowCancelled = false;

            bCheckList = false;
            //-------------- Define ServiceProviders List -----------------
            dtView = Global.dtServiceProviders.Copy().DefaultView;
            dtView.RowFilter = "Aktive = 1";
            cmbServiceProviders.DataSource = dtView;
            cmbServiceProviders.DisplayMember = "Title";
            cmbServiceProviders.ValueMember = "ID";
            cmbServiceProviders.SelectedValue = 0;

            dtView = Global.dtServiceProviders.Copy().DefaultView;
            dtView.RowFilter = "Aktive = 1";
            cmbServiceProviders3.DataSource = dtView;
            cmbServiceProviders3.DisplayMember = "Title";
            cmbServiceProviders3.ValueMember = "ID";
            cmbServiceProviders3.SelectedValue = 0;

            dtView = Global.dtServiceProviders.Copy().DefaultView;
            dtView.RowFilter = "Aktive = 1";
            cmbServiceProviders4.DataSource = dtView;
            cmbServiceProviders4.DisplayMember = "Title";
            cmbServiceProviders4.ValueMember = "ID";
            cmbServiceProviders4.SelectedValue = 0;

            //-------------- Define Depositories List -----------------
            cmbDepositories.DataSource = Global.dtDepositories.Copy().DefaultView;
            cmbDepositories.DisplayMember = "Code";
            cmbDepositories.ValueMember = "ID";
            cmbDepositories.SelectedValue = 0;

            cmbDepositories4.DataSource = Global.dtDepositories.Copy().DefaultView;
            cmbDepositories4.DisplayMember = "Code";
            cmbDepositories4.ValueMember = "ID";
            cmbDepositories4.SelectedValue = 0;

            //-------------- Define Currencies List -----------------
            cmbDepositories.DataSource = Global.dtDepositories.Copy().DefaultView;
            cmbDepositories.DisplayMember = "Code";
            cmbDepositories.ValueMember = "ID";
            cmbDepositories.SelectedValue = 0;

            //-------------- Define Currencies List -----------------
            cmbCurrency.DataSource = Global.dtCurrencies.Copy().DefaultView;
            cmbCurrency.DisplayMember = "Title";
            cmbCurrency.ValueMember = "ID";
            cmbCurrency.SelectedValue = 0;

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString(Global.GridStyle);
            fgList.DoubleClick += new System.EventHandler(fgList_DoubleClick);

            bCheckList = true;

            DefineList();
        }
        protected override void OnResize(EventArgs e)
        {
            panCritiries.Width = this.Width - 30;
            btnSearch.Left = panCritiries.Width - 120;

            fgList.Height = this.Height - 140;
            fgList.Width = this.Width - 30;
            panTools.Width = this.Width - 30;

            panEdit.Left = (Screen.PrimaryScreen.Bounds.Width - panEdit.Width) / 2;
            panEdit.Top = (Screen.PrimaryScreen.Bounds.Height - panEdit.Height) / 2;
        }
        private void DefineList()
        {
            i = 0;
            fgList.Redraw = false;
            fgList.Rows.Count = 1;

            GAP = new clsGAP();
            GAP.GetList();
            foreach (DataRow dtRow in GAP.List.Rows)
            {
                i = i + 1;
                fgList.AddItem(i + "\t" + dtRow["Code"] + "\t" + dtRow["Title"] + "\t" + dtRow["L1"] + "\t" + dtRow["L2"] + "\t" + dtRow["L3"] +
                               "\t" + dtRow["L4"] + "\t" + dtRow["L5"] + "\t" + dtRow["L6"] + "\t" + dtRow["L7"] + "\t" + dtRow["L8"] + "\t" + 
                               dtRow["L9"] + "\t" + dtRow["ID"]); 
            }
            fgList.Redraw = true;
        }
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            iAktion = 0;                                                     // 0 - Add mode
            EmptyRow();
            ShowAccPanel();
            panEdit.Visible = true;
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            EditRow();
        }
        private void fgList_DoubleClick(object sender, EventArgs e)
        {
            EditRow();
        }
        private void EditRow()
        {
            iAktion = 1;                                                     // 1 - Edit mode
            EmptyRow();
            cmbAccGroup.SelectedIndex = Convert.ToInt32(fgList[fgList.Row, "L1"]);
            cmbAccType.SelectedIndex = Convert.ToInt32(fgList[fgList.Row, "L2"]);
            ShowAccPanel();

            if (cmbAccGroup.SelectedIndex == 1)
            {
                if (cmbAccType.SelectedIndex == 1)
                {
                    foundRows = Global.dtContracts.Select("Contract_ID = " + fgList[fgList.Row, "L3"]);
                    if (foundRows.Length > 0)
                    {
                        ucCS.ShowClientsList = false;
                        ucCS.txtContractTitle.Text = foundRows[0]["ContractTitle"] + "";
                        ucCS.ShowClientsList = true;
                        iContract_ID = Convert.ToInt32(fgList[fgList.Row, "L3"]);
                        lblCode.Text = foundRows[0]["Code"] + "";
                        lblPortfolio.Text = foundRows[0]["Portfolio"] + "";
                        lblCurrency.Text = foundRows[0]["Currency"] + "";
                        sCurrency = foundRows[0]["Currency"] + "";
                    };

                    foundRows = Global.dtCurrencies.Select("Title = '" + sCurrency + "'");
                    if (foundRows.Length > 0) iCurrency_ID = Convert.ToInt32(foundRows[0]["ID"]);

                    foundRows = Global.dtProducts.Select("ID = " + fgList[fgList.Row, "L5"]);
                    if (foundRows.Length > 0)
                    {
                        iShareCodes_ID = Convert.ToInt32(fgList[fgList.Row, "L5"]);
                        sISIN = foundRows[0]["ISIN"] + "";
                        ucPS.ShowProductsList = false;
                        ucPS.txtShareTitle.Text = foundRows[0]["Title"] + "";
                        ucPS.ShowProductsList = true;
                    }

                    cmbServiceProviders.SelectedValue = Convert.ToInt32(fgList[fgList.Row, "L7"]);
                 
                    sProvider_Code = "";
                    foundRows = Global.dtServiceProviders.Select("ID = " + cmbServiceProviders.SelectedValue);
                    if (foundRows.Length > 0) sProvider_Code = foundRows[0]["Alias"] + "";
                   
                    cmbStatus.SelectedIndex = Convert.ToInt32("0" + fgList[fgList.Row, "L9"]);

                    txtGAPCode.Text = Global.CreateGAPCode(cmbAccGroup.SelectedIndex, cmbAccType.SelectedIndex, lblCode.Text, lblPortfolio.Text, sISIN, sCurrency, iOwner_ID, sDepository_Code, sProvider_Code, iStatus);
                }
                else
                {
                    foundRows = Global.dtContracts.Select("Contract_ID = " + fgList[fgList.Row, "L3"]);
                    if (foundRows.Length > 0)
                    {
                        ucCS2.ShowClientsList = false;
                        ucCS2.txtContractTitle.Text = foundRows[0]["ContractTitle"] + "";
                        ucCS2.ShowClientsList = true;
                        iContract_ID = Convert.ToInt32(fgList[fgList.Row, "L3"]);
                        lblCode2.Text = foundRows[0]["Code"] + "";
                        lblPortfolio2.Text = foundRows[0]["Portfolio"] + "";
                        lblCurrency2.Text = foundRows[0]["Currency"] + "";
                        sCurrency = foundRows[0]["Currency"] + "";
                    };

                    foundRows = Global.dtCurrencies.Select("Title = '" + sCurrency + "'");
                    if (foundRows.Length > 0) iCurrency_ID = Convert.ToInt32(foundRows[0]["ID"]);

                    foundRows = Global.dtProducts.Select("ID = " + fgList[fgList.Row, "L5"]);
                    if (foundRows.Length > 0)
                    {
                        iShareCodes_ID = Convert.ToInt32(fgList[fgList.Row, "L5"]);
                        sISIN = foundRows[0]["ISIN"] + "";
                        ucPS.ShowProductsList = false;
                        ucPS.txtShareTitle.Text = foundRows[0]["Title"] + "";
                        ucPS.ShowProductsList = true;
                    }
                    
                    cmbServiceProviders.SelectedValue = Convert.ToInt32(fgList[fgList.Row, "L7"]);
                    cmbDepositories.SelectedValue = Convert.ToInt32(fgList[fgList.Row, "L8"]);

                    sProvider_Code = "";
                    foundRows = Global.dtServiceProviders.Select("ID = " + cmbServiceProviders.SelectedValue);
                    if (foundRows.Length > 0) sProvider_Code = foundRows[0]["Alias"] + "";

                    cmbStatus.SelectedIndex = Convert.ToInt32(fgList[fgList.Row, "L9"]);

                    txtGAPCode.Text = Global.CreateGAPCode(cmbAccGroup.SelectedIndex, cmbAccType.SelectedIndex, lblCode2.Text, lblPortfolio2.Text, sISIN, sCurrency, iOwner_ID, sDepository_Code, sProvider_Code, iStatus);
                }
                txtTitle.Text = fgList[fgList.Row, "Title"] + ""; 
            }
            else
            {
                if (cmbAccType.SelectedIndex == 1)
                {
                    iCurrency_ID = Convert.ToInt32(fgList[fgList.Row, "L6"]);
                    sCurrency = "";
                    foundRows = Global.dtCurrencies.Select("ID = " + fgList[fgList.Row, "L6"]);
                    if (foundRows.Length > 0) sCurrency = foundRows[0]["Title"] + "";
                    cmbCurrency.SelectedValue = iCurrency_ID;

                    cmbOwners.SelectedIndex = Convert.ToInt32(fgList[fgList.Row, "L4"]);
                    iOwner_ID = cmbOwners.SelectedIndex;
                    cmbServiceProviders3.SelectedValue = Convert.ToInt32(fgList[fgList.Row, "L7"]);

                    sProvider_Code = "";
                    foundRows = Global.dtServiceProviders.Select("ID = " + cmbServiceProviders3.SelectedValue);
                    if (foundRows.Length > 0) sProvider_Code = foundRows[0]["Alias"] + "";

                    cmbStatus.SelectedIndex = Convert.ToInt32(fgList[fgList.Row, "L9"]);

                    txtGAPCode.Text = Global.CreateGAPCode(cmbAccGroup.SelectedIndex, cmbAccType.SelectedIndex, lblCode.Text, lblPortfolio.Text, sISIN, sCurrency, iOwner_ID, sDepository_Code, sProvider_Code, iStatus);
                }
                else
                {

                    iCurrency_ID = Convert.ToInt32(fgList[fgList.Row, "L6"]);
                    sCurrency = "";
                    foundRows = Global.dtCurrencies.Select("ID = " + fgList[fgList.Row, "L6"]);
                    if (foundRows.Length > 0) sCurrency = foundRows[0]["Title"] + "";
                    cmbCurrency.SelectedValue = iCurrency_ID;
                    
                    foundRows = Global.dtProducts.Select("ID = " + fgList[fgList.Row, "L5"]);
                    if (foundRows.Length > 0)
                    {
                        iShareCodes_ID = Convert.ToInt32(fgList[fgList.Row, "L5"]);
                        sISIN = foundRows[0]["ISIN"] + "";
                        ucPS4.ShowProductsList = false;
                        ucPS4.txtShareTitle.Text = foundRows[0]["Title"] + "";
                        ucPS4.ShowProductsList = true;
                        sCurrency = foundRows[0]["Currency"] + "";
                    }
                    lblCurrency4.Text = sCurrency;

                    cmbOwners4.SelectedIndex = Convert.ToInt32(fgList[fgList.Row, "L4"]);
                    iOwner_ID = cmbOwners4.SelectedIndex;

                    cmbServiceProviders4.SelectedValue = Convert.ToInt32(fgList[fgList.Row, "L7"]);
                    sProvider_Code = cmbServiceProviders4.SelectedText + "";

                    cmbDepositories4.SelectedValue = Convert.ToInt32(fgList[fgList.Row, "L8"]);

                    sProvider_Code = "";
                    foundRows = Global.dtServiceProviders.Select("ID = " + cmbServiceProviders4.SelectedValue);
                    if (foundRows.Length > 0) sProvider_Code = foundRows[0]["Alias"] + "";

                    cmbStatus4.SelectedIndex = Convert.ToInt32(fgList[fgList.Row, "L9"]);

                    txtGAPCode.Text = Global.CreateGAPCode(cmbAccGroup.SelectedIndex, cmbAccType.SelectedIndex, lblCode2.Text, lblPortfolio2.Text, sISIN, sCurrency, iOwner_ID, sDepository_Code, sProvider_Code, iStatus);
                }
                txtTitle.Text = fgList[fgList.Row, "Title"] + "";
            }
            panEdit.Visible = true;
        }
        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 0)

                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    GAP = new clsGAP();
                    GAP.Record_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                    GAP.DeleteRecord();
                    fgList.RemoveItem(fgList.Row);

                    if (fgList.Rows.Count > 1)
                        fgList.Focus();
                    fgList.Redraw = true;

                    iAktion = 1;                                                          // 1 - EDIT Mode
                }
        }

        private void tsbExcel_Click(object sender, EventArgs e)
        {

        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            this.Refresh();
            if (iAktion == 0)
            {
                GAP = new clsGAP();
                GAP.Code = txtGAPCode.Text + "";
                GAP.GetRecord_Code();
                if (GAP.Record_ID == 0)
                {
                    GAP = new clsGAP();
                    GAP.L1 = Convert.ToInt32(cmbAccGroup.SelectedIndex);
                    GAP.L2 = Convert.ToInt32(cmbAccType.SelectedIndex);
                    GAP.L3 = iContract_ID;
                    GAP.L4 = iOwner_ID;
                    GAP.L5 = iShareCodes_ID;
                    GAP.L6 = iCurrency_ID;
                    GAP.L7 = iProvider_ID;
                    GAP.L8 = iDepository_ID;
                    GAP.L9 = iStatus;
                    GAP.Title = txtTitle.Text + "";
                    GAP.Code = txtGAPCode.Text + "";
                    GAP.InsertRecord();
                }
                else
                    MessageBox.Show("ΠΡΟΣΟΧΗ! This code exists;", Global.AppTitle, MessageBoxButtons.OK);
            }
            else
            {
                GAP = new clsGAP();
                GAP.Record_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                GAP.GetRecord();
                GAP.L1 = Convert.ToInt32(cmbAccGroup.SelectedIndex);
                GAP.L2 = Convert.ToInt32(cmbAccType.SelectedIndex);
                GAP.L3 = iContract_ID;
                GAP.L4 = iOwner_ID;
                GAP.L5 = iShareCodes_ID;
                GAP.L6 = iCurrency_ID;
                GAP.L7 = iProvider_ID;
                GAP.L8 = iDepository_ID;
                GAP.L9 = iStatus;
                GAP.Title = txtTitle.Text + "";
                GAP.Code = txtGAPCode.Text + "";
                GAP.EditRecord();
            }
            DefineList();

            panEdit.Visible = false;
        }
        private void picClose_Click(object sender, EventArgs e)
        {
            panEdit.Visible = false;
        }
        private void EmptyRow()
        {
            lblCode.Text = "";
            lblPortfolio.Text = "";
            lblCurrency.Text = "";
            lblCode2.Text = "";
            lblPortfolio2.Text = "";
            lblCurrency2.Text = "";
            lblCurrency4.Text = "";

            iContract_ID = 0;
            iOwner_ID = 0;
            iShareCodes_ID = 0;
            sISIN = "";
            iCurrency_ID = 0;
            sCurrency = "";
            iProvider_ID = 0;
            sProvider_Code = "";
            iDepository_ID = 0;
            sDepository_Code = "";
            iStatus = 0;
            txtTitle.Text = "";
            txtGAPCode.Text = "";

            bCheckList = false;
            cmbAccGroup.SelectedIndex = 0;
            cmbAccType.SelectedIndex = 0;
            ucCS.ShowClientsList = false;
            ucCS.txtContractTitle.Text = "";
            ucCS.ShowClientsList = true;
            ucCS2.ShowClientsList = false;
            ucCS2.txtContractTitle.Text = "";
            ucCS2.ShowClientsList = true;
            ucPS.ShowProductsList = false;
            ucPS.txtShareTitle.Text = "";
            ucPS.ShowProductsList = true;
            ucPS4.ShowProductsList = false;
            ucPS4.txtShareTitle.Text = "";
            ucPS4.ShowProductsList = true;
            cmbServiceProviders.SelectedValue = 0;
            cmbServiceProviders3.SelectedValue = 0;
            cmbServiceProviders4.SelectedValue = 0;
            cmbDepositories.SelectedValue = 0;
            cmbDepositories4.SelectedValue = 0;
            cmbOwners.SelectedIndex = 0;
            cmbOwners4.SelectedIndex = 0;
            cmbServiceProviders3.SelectedValue = 0;
            cmbCurrency.SelectedValue = 0;
            cmbStatus.SelectedIndex = 0;
            cmbStatus2.SelectedIndex = 0;
            cmbStatus3.SelectedIndex = 0;
            cmbStatus4.SelectedIndex = 0;
            panAcc1_1.Visible = false;
            panAcc1_2.Visible = false;
            panAcc2_1.Visible = false;
            bCheckList = true;
        }
        private void cmbAccGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowAccPanel();
        }
        private void cmbAccType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowAccPanel();
        }
        private void cmbServiceProviders_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList)
            {
                sProvider_Code = "";
                iProvider_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                foundRows = Global.dtServiceProviders.Select("ID = " + iProvider_ID);
                if (foundRows.Length > 0) sProvider_Code = foundRows[0]["Alias"] + "";

                txtGAPCode.Text = Global.CreateGAPCode(cmbAccGroup.SelectedIndex, cmbAccType.SelectedIndex, lblCode.Text, lblPortfolio.Text, sISIN, sCurrency, iOwner_ID, sDepository_Code, sProvider_Code, iStatus);
            }
        }
        private void cmbServiceProviders3_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList)
            {
                sProvider_Code = "";
                iProvider_ID = Convert.ToInt32(cmbServiceProviders3.SelectedValue);
                foundRows = Global.dtServiceProviders.Select("ID = " + iProvider_ID);
                if (foundRows.Length > 0) sProvider_Code = foundRows[0]["Alias"] + "";

                txtGAPCode.Text = Global.CreateGAPCode(cmbAccGroup.SelectedIndex, cmbAccType.SelectedIndex, lblCode.Text, lblPortfolio.Text, sISIN, sCurrency, iOwner_ID, sDepository_Code, sProvider_Code, iStatus);
            }
        }
        private void cmbServiceProviders4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bCheckList)
            {
                sProvider_Code = "";
                iProvider_ID = Convert.ToInt32(cmbServiceProviders4.SelectedValue);
                foundRows = Global.dtServiceProviders.Select("ID = " + iProvider_ID);
                if (foundRows.Length > 0) sProvider_Code = foundRows[0]["Alias"] + "";

                txtGAPCode.Text = Global.CreateGAPCode(cmbAccGroup.SelectedIndex, cmbAccType.SelectedIndex, lblCode.Text, lblPortfolio.Text, sISIN, sCurrency, iOwner_ID, sDepository_Code, sProvider_Code, iStatus);
            }
        }
        private void cmbOwners_SelectedIndexChanged(object sender, EventArgs e)
        {
            iOwner_ID = cmbOwners.SelectedIndex;
            txtGAPCode.Text = Global.CreateGAPCode(cmbAccGroup.SelectedIndex, cmbAccType.SelectedIndex, lblCode.Text, lblPortfolio.Text, sISIN, sCurrency, iOwner_ID, sDepository_Code, sProvider_Code, iStatus);
        }

        private void cmbOwners4_SelectedIndexChanged(object sender, EventArgs e)
        {
            iOwner_ID = cmbOwners4.SelectedIndex;
            txtGAPCode.Text = Global.CreateGAPCode(cmbAccGroup.SelectedIndex, cmbAccType.SelectedIndex, lblCode.Text, lblPortfolio.Text, sISIN, sCurrency, iOwner_ID, sDepository_Code, sProvider_Code, iStatus);
        }
        private void cmbDepositories_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList)
            {
                iDepository_ID = Convert.ToInt32(cmbDepositories.SelectedValue);
                sDepository_Code = cmbDepositories.Text;
                txtGAPCode.Text = Global.CreateGAPCode(cmbAccGroup.SelectedIndex, cmbAccType.SelectedIndex, lblCode2.Text, lblPortfolio2.Text, sISIN, sCurrency, iOwner_ID, sDepository_Code, sProvider_Code, iStatus);
            }
        }
        private void cmbDepositories4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bCheckList)
            {
                iDepository_ID = Convert.ToInt32(cmbDepositories4.SelectedValue);
                sDepository_Code = cmbDepositories4.Text;
                txtGAPCode.Text = Global.CreateGAPCode(cmbAccGroup.SelectedIndex, cmbAccType.SelectedIndex, lblCode.Text, lblPortfolio.Text, sISIN, sCurrency, iOwner_ID, sDepository_Code, sProvider_Code, iStatus);
            }
        }
        private void cmbCurrency_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList)
            {
                iCurrency_ID = Convert.ToInt32(cmbCurrency.SelectedValue);
                sCurrency = cmbCurrency.Text + "";
                txtGAPCode.Text = Global.CreateGAPCode(cmbAccGroup.SelectedIndex, cmbAccType.SelectedIndex, lblCode.Text, lblPortfolio.Text, sISIN, sCurrency, iOwner_ID, sDepository_Code, sProvider_Code, iStatus);
            }
        }
        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            iStatus = cmbStatus.SelectedIndex;
            txtGAPCode.Text = Global.CreateGAPCode(cmbAccGroup.SelectedIndex, cmbAccType.SelectedIndex, lblCode.Text, lblPortfolio.Text, sISIN, sCurrency, iOwner_ID, sDepository_Code, sProvider_Code, iStatus);
        }
        private void cmbStatus2_SelectedIndexChanged(object sender, EventArgs e)
        {
            iStatus = cmbStatus2.SelectedIndex;
            txtGAPCode.Text = Global.CreateGAPCode(cmbAccGroup.SelectedIndex, cmbAccType.SelectedIndex, lblCode2.Text, lblPortfolio2.Text, sISIN, sCurrency, iOwner_ID, sDepository_Code, sProvider_Code, iStatus);
        }
        private void cmbStatus3_SelectedIndexChanged(object sender, EventArgs e)
        {
            iStatus = cmbStatus3.SelectedIndex;
            txtGAPCode.Text = Global.CreateGAPCode(cmbAccGroup.SelectedIndex, cmbAccType.SelectedIndex, lblCode.Text, lblPortfolio.Text, sISIN, sCurrency, iOwner_ID, sDepository_Code, sProvider_Code, iStatus);
        }
        private void cmbStatus4_SelectedIndexChanged(object sender, EventArgs e)
        {
            iStatus = cmbStatus4.SelectedIndex;
            txtGAPCode.Text = Global.CreateGAPCode(cmbAccGroup.SelectedIndex, cmbAccType.SelectedIndex, lblCode.Text, lblPortfolio.Text, sISIN, sCurrency, iOwner_ID, sDepository_Code, sProvider_Code, iStatus);
        }
        protected void ucCS_TextChanged(object sender, EventArgs e)
        {
            stContractData = ucCS.SelectedContractData;
            iContract_ID = stContractData.Contract_ID;
            lblCode.Text = stContractData.Code;
            lblPortfolio.Text = stContractData.Portfolio;
            lblCurrency.Text = stContractData.Currency;
            txtTitle.Text = stContractData.ContractTitle;
            cmbServiceProviders.SelectedValue = stContractData.Provider_ID;
            iProvider_ID = stContractData.Provider_ID;
            sCurrency = stContractData.Currency;
            iCurrency_ID = 0;
            foundRows = Global.dtCurrencies.Select("Title = '" + sCurrency + "'");
            if (foundRows.Length > 0) iCurrency_ID = Convert.ToInt32(foundRows[0]["ID"]);
            
            txtGAPCode.Text = Global.CreateGAPCode(cmbAccGroup.SelectedIndex, cmbAccType.SelectedIndex, lblCode.Text, lblPortfolio.Text, sISIN, sCurrency, iOwner_ID, sDepository_Code, sProvider_Code, iStatus);

        }
        protected void ucCS2_TextChanged(object sender, EventArgs e)
        {
            stContractData = ucCS2.SelectedContractData;
            iContract_ID = stContractData.Contract_ID;
            lblCode2.Text = stContractData.Code;
            lblPortfolio2.Text = stContractData.Portfolio;
            lblCurrency2.Text = stContractData.Currency;
            txtTitle.Text = stContractData.ContractTitle;
            cmbServiceProviders.SelectedValue = stContractData.Provider_ID;
            iProvider_ID = stContractData.Provider_ID;
            sCurrency = stContractData.Currency;
            iCurrency_ID = 0;
            foundRows = Global.dtCurrencies.Select("Title = '" + sCurrency + "'");
            if (foundRows.Length > 0) iCurrency_ID = Convert.ToInt32(foundRows[0]["ID"]);

            txtGAPCode.Text = Global.CreateGAPCode(cmbAccGroup.SelectedIndex, cmbAccType.SelectedIndex, lblCode2.Text, lblPortfolio2.Text, sISIN, sCurrency, iOwner_ID, sDepository_Code, sProvider_Code, iStatus);

        }
        protected void ucPS_TextChanged(object sender, EventArgs e)
        {
            Global.ProductData stProduct = new Global.ProductData();
            stProduct = ucPS.SelectedProductData;
            iShareCodes_ID = stProduct.ShareCode_ID;
            sISIN = stProduct.ISIN;
        }
        protected void ucPS4_TextChanged(object sender, EventArgs e)
        {
            Global.ProductData stProduct = new Global.ProductData();
            stProduct = ucPS4.SelectedProductData;
            iShareCodes_ID = stProduct.ShareCode_ID;
            sISIN = stProduct.ISIN;
            sCurrency = stProduct.Currency;
            lblCurrency4.Text = stProduct.Currency;
            iCurrency_ID = 0;
            foundRows = Global.dtCurrencies.Select("Title = '" + sCurrency + "'");
            if (foundRows.Length > 0) iCurrency_ID = Convert.ToInt32(foundRows[0]["ID"]);
        }
        private void ShowAccPanel()
        {
            panAcc1_1.Visible = false;
            panAcc1_2.Visible = false;
            panAcc2_1.Visible = false;
            panAcc2_2.Visible = false;

            if (Convert.ToInt32(cmbAccGroup.SelectedIndex) != 0 && Convert.ToInt32(cmbAccType.SelectedIndex) != 0)
            {
                switch (Convert.ToInt32(cmbAccGroup.SelectedIndex))
                {
                    case 1:
                        if (Convert.ToInt32(cmbAccType.SelectedIndex) == 1) panAcc1_1.Visible = true;
                        else panAcc1_2.Visible = true;
                        break;
                    case 2:
                        if (Convert.ToInt32(cmbAccType.SelectedIndex) == 1) panAcc2_1.Visible = true;
                        else panAcc2_2.Visible = true;
                        break;
                }
                txtGAPCode.Text = cmbAccGroup.SelectedIndex + "." + cmbAccType.SelectedIndex;
            }
            this.Refresh();
        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
