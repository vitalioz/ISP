using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using Core;

namespace Accounting
{
    public partial class frmPortfolios_List : Form
    {
        int i = 0, iRightsLevel;
        DataRow[] foundRows;
        clsContracts_Balances Contracts_Balances = new clsContracts_Balances();
        clsContracts_BalancesRecs Contracts_BalancesRecs = new clsContracts_BalancesRecs();
        clsContracts klsContract = new clsContracts();
        clsProductsCodes klsProductsCodes = new clsProductsCodes();  
        clsContracts_ComplexSigns klsContracts_ComplexSigns = new clsContracts_ComplexSigns();
        clsInvestmentCommetties_AssetAllocation InvestmentCommetties_AssetAllocation = new clsInvestmentCommetties_AssetAllocation();

        public frmPortfolios_List()
        {
            InitializeComponent();
        }

        private void frmPortfolios_List_Load(object sender, EventArgs e)
        {
            gridView1 = grdList.MainView as GridView;
            gridView1.FocusedRowObjectChanged += gridView1_FocusedRowObjectChanged;            
            gridView1.DoubleClick += gridView1_DoubleClick;
            gridView1.RowCellStyle += new RowCellStyleEventHandler(gridView1_RowCellStyle);
            gridView1.HorzScrollVisibility = ScrollVisibility.Always;

            DefineList();
        }
        protected override void OnResize(EventArgs e)
        {
            panCritiries.Width = this.Width - 30;
            btnSearch.Left = panCritiries.Width - 120;

            panTools.Width = this.Width - 30;
            picMin.Left = panTools.Width - 28;
            picMax.Left = panTools.Width - 28;
            picMin.Visible = false;
            picMax.Visible = true;

            grdList.Width = this.Width - 302;
            grdList.Height = this.Height - 144;

            panDetails.Left = this.Width - 284;
            panDetails.Height = this.Height - 144;
            panDetails.Width = 260;

            grpNotes.Width = panDetails.Width - 12;
            txtNotes.Width = panDetails.Width - 28;

            grpSpecialInstructions.Width = panDetails.Width - 12;
            lblSpecialInstructions.Width = panDetails.Width - 32;

            grpComplexData.Width = panDetails.Width - 12;
            lblComplexData.Width = panDetails.Width - 32;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DefineList();
        }
        private void DefineList()
        {
            Contracts_Balances = new clsContracts_Balances();
            Contracts_Balances.DateIns = dDateControl.Value.Date;
            Contracts_Balances.GetList();  
            grdList.DataSource = Contracts_Balances.List;

            GridColumn colAA = gridView1.Columns["AA"];
            colAA.Width = 30;

            GridColumn colDateIns = gridView1.Columns["DateIns"];
            colDateIns.Visible = false;

            GridColumn colCode = gridView1.Columns["Code"];
            colCode.Width = 70;

            GridColumn colPortfolio = gridView1.Columns["Portfolio"];
            colPortfolio.Width = 80;

            GridColumn colContractTitle = gridView1.Columns["ContractTitle"];
            colContractTitle.Width = 240;

            GridColumn colProfile_Title = gridView1.Columns["Profile_Title"];
            colProfile_Title.Caption = "Investment Profile";
            colProfile_Title.Visible = true;
            colProfile_Title.Width = 100;

            GridColumn colCurrency = gridView1.Columns["Currency"];
            colCurrency.Caption = "Currency";
            colCurrency.Visible = true;
            colCurrency.Width = 30;

            GridColumn colTotalSecurutiesValue = gridView1.Columns["TotalSecurutiesValue"];
            colTotalSecurutiesValue.Caption = "Total Securuties Value";
            colTotalSecurutiesValue.DisplayFormat.FormatType = FormatType.Numeric;
            colTotalSecurutiesValue.DisplayFormat.FormatString = "n2";
            colTotalSecurutiesValue.Width = 80;

            GridColumn colTotalCashValue = gridView1.Columns["TotalCashValue"];
            colTotalCashValue.Caption = "Total Cash Value";
            colTotalCashValue.DisplayFormat.FormatType = FormatType.Numeric;
            colTotalCashValue.DisplayFormat.FormatString = "n2";
            colTotalCashValue.Width = 80;

            GridColumn colTotalValue = gridView1.Columns["TotalValue"];
            colTotalValue.Caption = "Total Value";
            colTotalValue.DisplayFormat.FormatType = FormatType.Numeric;
            colTotalValue.DisplayFormat.FormatString = "n2";
            colTotalValue.Width = 80;

            GridColumn colHF_FixedIncome = gridView1.Columns["HF_FixedIncome"];
            colHF_FixedIncome.Caption = "HF FixedIncome";
            colHF_FixedIncome.AppearanceCell.BackColor = System.Drawing.Color.Thistle;
            colHF_FixedIncome.AppearanceCell.Options.UseBackColor = true;
            colHF_FixedIncome.AppearanceHeader.BackColor = System.Drawing.Color.Thistle;
            colHF_FixedIncome.AppearanceHeader.Options.UseBackColor = true;
            colHF_FixedIncome.DisplayFormat.FormatType = FormatType.Numeric;
            colHF_FixedIncome.DisplayFormat.FormatString = "n2";
            colHF_FixedIncome.Width = 50;

            GridColumn colHF_Equities = gridView1.Columns["HF_Equities"];
            colHF_Equities.Caption = "HF Equities";
            colHF_Equities.AppearanceCell.BackColor = System.Drawing.Color.Thistle;
            colHF_Equities.AppearanceCell.Options.UseBackColor = true;
            colHF_Equities.AppearanceHeader.BackColor = System.Drawing.Color.Thistle;
            colHF_Equities.AppearanceHeader.Options.UseBackColor = true;
            colHF_Equities.DisplayFormat.FormatType = FormatType.Numeric;
            colHF_Equities.DisplayFormat.FormatString = "n2";
            colHF_Equities.Width = 50;


            GridColumn colHF_Cash = gridView1.Columns["HF_Cash"];
            colHF_Cash.Caption = "HF Cash";
            colHF_Cash.AppearanceCell.BackColor = System.Drawing.Color.Thistle;
            colHF_Cash.AppearanceCell.Options.UseBackColor = true;
            colHF_Cash.AppearanceHeader.BackColor = System.Drawing.Color.Thistle;
            colHF_Cash.AppearanceHeader.Options.UseBackColor = true;
            colHF_Cash.DisplayFormat.FormatType = FormatType.Numeric;
            colHF_Cash.DisplayFormat.FormatString = "n2";
            colHF_Cash.Width = 50;

            GridColumn colHF_EUR = gridView1.Columns["HF_EUR"];
            colHF_EUR.Caption = "HF EUR";
            colHF_EUR.AppearanceCell.BackColor = System.Drawing.Color.Thistle;
            colHF_EUR.AppearanceCell.Options.UseBackColor = true;
            colHF_EUR.AppearanceHeader.BackColor = System.Drawing.Color.Thistle;
            colHF_EUR.AppearanceHeader.Options.UseBackColor = true;
            colHF_EUR.DisplayFormat.FormatType = FormatType.Numeric;
            colHF_EUR.DisplayFormat.FormatString = "n2";
            colHF_EUR.Width = 50;

            GridColumn colHF_USD_etc = gridView1.Columns["HF_USD_etc"];
            colHF_USD_etc.Caption = "HF USD_etc";
            colHF_USD_etc.AppearanceCell.BackColor = System.Drawing.Color.Thistle;
            colHF_USD_etc.AppearanceCell.Options.UseBackColor = true;
            colHF_USD_etc.AppearanceHeader.BackColor = System.Drawing.Color.Thistle;
            colHF_USD_etc.AppearanceHeader.Options.UseBackColor = true;
            colHF_USD_etc.DisplayFormat.FormatType = FormatType.Numeric;
            colHF_USD_etc.DisplayFormat.FormatString = "n2";
            colHF_USD_etc.Width = 50;

            GridColumn colHF_EmergingCurrencies = gridView1.Columns["HF_EmergingCurrencies"];
            colHF_EmergingCurrencies.Caption = "HF EmergingCurrencies";
            colHF_EmergingCurrencies.AppearanceCell.BackColor = System.Drawing.Color.Thistle;
            colHF_EmergingCurrencies.AppearanceCell.Options.UseBackColor = true;
            colHF_EmergingCurrencies.AppearanceHeader.BackColor = System.Drawing.Color.Thistle;
            colHF_EmergingCurrencies.AppearanceHeader.Options.UseBackColor = true;
            colHF_EmergingCurrencies.DisplayFormat.FormatType = FormatType.Numeric;
            colHF_EmergingCurrencies.DisplayFormat.FormatString = "n2";
            colHF_EmergingCurrencies.Width = 50;

            GridColumn colHF_DevelopedMarkets = gridView1.Columns["HF_DevelopedMarkets"];
            colHF_DevelopedMarkets.Caption = "HF DevelopedMarkets";
            colHF_DevelopedMarkets.AppearanceCell.BackColor = System.Drawing.Color.Thistle;
            colHF_DevelopedMarkets.AppearanceCell.Options.UseBackColor = true;
            colHF_DevelopedMarkets.AppearanceHeader.BackColor = System.Drawing.Color.Thistle;
            colHF_DevelopedMarkets.AppearanceHeader.Options.UseBackColor = true;
            colHF_DevelopedMarkets.DisplayFormat.FormatType = FormatType.Numeric;
            colHF_DevelopedMarkets.DisplayFormat.FormatString = "n2";
            colHF_DevelopedMarkets.Width = 50;

            GridColumn colHF_EmergingMarkets = gridView1.Columns["HF_EmergingMarkets"];
            colHF_EmergingMarkets.Caption = "HF EmergingMarkets";
            colHF_EmergingMarkets.AppearanceCell.BackColor = System.Drawing.Color.Thistle;
            colHF_EmergingMarkets.AppearanceCell.Options.UseBackColor = true;
            colHF_EmergingMarkets.AppearanceHeader.BackColor = System.Drawing.Color.Thistle;
            colHF_EmergingMarkets.AppearanceHeader.Options.UseBackColor = true;
            colHF_EmergingMarkets.DisplayFormat.FormatType = FormatType.Numeric;
            colHF_EmergingMarkets.DisplayFormat.FormatString = "n2";
            colHF_EmergingMarkets.Width = 50;

            GridColumn colFixedIncome = gridView1.Columns["FixedIncome"];
            colFixedIncome.Caption = "FixedIncome";
            colFixedIncome.AppearanceCell.BackColor = System.Drawing.Color.Moccasin;
            colFixedIncome.AppearanceCell.Options.UseBackColor = true;
            colFixedIncome.AppearanceHeader.BackColor = System.Drawing.Color.Moccasin;
            colFixedIncome.AppearanceHeader.Options.UseBackColor = true;
            colFixedIncome.DisplayFormat.FormatType = FormatType.Numeric;
            colFixedIncome.DisplayFormat.FormatString = "n2";
            colFixedIncome.Width = 50;

            GridColumn colEquities = gridView1.Columns["Equities"];
            colEquities.Caption = "Equities";
            colEquities.AppearanceCell.BackColor = System.Drawing.Color.Moccasin;
            colEquities.AppearanceCell.Options.UseBackColor = true;
            colEquities.AppearanceHeader.BackColor = System.Drawing.Color.Moccasin;
            colEquities.AppearanceHeader.Options.UseBackColor = true;
            colEquities.DisplayFormat.FormatType = FormatType.Numeric;
            colEquities.DisplayFormat.FormatString = "n2";
            colEquities.Width = 50;


            GridColumn colCash = gridView1.Columns["Cash"];
            colCash.Caption = "Cash";
            colCash.AppearanceCell.BackColor = System.Drawing.Color.Moccasin;
            colCash.AppearanceCell.Options.UseBackColor = true;
            colCash.AppearanceHeader.BackColor = System.Drawing.Color.Moccasin;
            colCash.AppearanceHeader.Options.UseBackColor = true;
            colCash.DisplayFormat.FormatType = FormatType.Numeric;
            colCash.DisplayFormat.FormatString = "n2";
            colCash.Width = 50;

            GridColumn colEUR = gridView1.Columns["EUR"];
            colEUR.Caption = "EUR";
            colEUR.AppearanceCell.BackColor = System.Drawing.Color.Moccasin;
            colEUR.AppearanceCell.Options.UseBackColor = true;
            colEUR.AppearanceHeader.BackColor = System.Drawing.Color.Moccasin;
            colEUR.AppearanceHeader.Options.UseBackColor = true;
            colEUR.DisplayFormat.FormatType = FormatType.Numeric;
            colEUR.DisplayFormat.FormatString = "n2";
            colEUR.Width = 50;

            GridColumn colUSD_etc = gridView1.Columns["USD_etc"];
            colUSD_etc.Caption = "USD_etc";
            colUSD_etc.AppearanceCell.BackColor = System.Drawing.Color.Moccasin;
            colUSD_etc.AppearanceCell.Options.UseBackColor = true;
            colUSD_etc.AppearanceHeader.BackColor = System.Drawing.Color.Moccasin;
            colUSD_etc.AppearanceHeader.Options.UseBackColor = true;
            colUSD_etc.DisplayFormat.FormatType = FormatType.Numeric;
            colUSD_etc.DisplayFormat.FormatString = "n2";
            colUSD_etc.Width = 50;

            GridColumn colEmergingCurrencies = gridView1.Columns["EmergingCurrencies"];
            colEmergingCurrencies.Caption = "EmergingCurrencies";
            colEmergingCurrencies.AppearanceCell.BackColor = System.Drawing.Color.Moccasin;
            colEmergingCurrencies.AppearanceCell.Options.UseBackColor = true;
            colEmergingCurrencies.AppearanceHeader.BackColor = System.Drawing.Color.Moccasin;
            colEmergingCurrencies.AppearanceHeader.Options.UseBackColor = true;
            colEmergingCurrencies.DisplayFormat.FormatType = FormatType.Numeric;
            colEmergingCurrencies.DisplayFormat.FormatString = "n2";
            colEmergingCurrencies.Width = 50;

            GridColumn colDevelopedMarkets = gridView1.Columns["DevelopedMarkets"];
            colDevelopedMarkets.Caption = "DevelopedMarkets";
            colDevelopedMarkets.AppearanceCell.BackColor = System.Drawing.Color.Moccasin;
            colDevelopedMarkets.AppearanceCell.Options.UseBackColor = true;
            colDevelopedMarkets.AppearanceHeader.BackColor = System.Drawing.Color.Moccasin;
            colDevelopedMarkets.AppearanceHeader.Options.UseBackColor = true;
            colDevelopedMarkets.DisplayFormat.FormatType = FormatType.Numeric;
            colDevelopedMarkets.DisplayFormat.FormatString = "n2";
            colDevelopedMarkets.Width = 50;

            GridColumn colEmergingMarkets = gridView1.Columns["EmergingMarkets"];
            colEmergingMarkets.Caption = "EmergingMarkets";
            colEmergingMarkets.AppearanceCell.BackColor = System.Drawing.Color.Moccasin;
            colEmergingMarkets.AppearanceCell.Options.UseBackColor = true;
            colEmergingMarkets.AppearanceHeader.BackColor = System.Drawing.Color.Moccasin;
            colEmergingMarkets.AppearanceHeader.Options.UseBackColor = true;
            colEmergingMarkets.DisplayFormat.FormatType = FormatType.Numeric;
            colEmergingMarkets.DisplayFormat.FormatString = "n2";
            colEmergingMarkets.Width = 50;

            GridColumn colCustodian = gridView1.Columns["Custodian"];
            colCustodian.Width = 80;

            GridColumn colMiFID_2 = gridView1.Columns["MiFID_2"];
            colMiFID_2.Width = 60;

            GridColumn colXAA = gridView1.Columns["XAA"];
            colXAA.Width = 40;

            GridColumn colNotes = gridView1.Columns["Notes"];
            colNotes.Width = 100;

            GridColumn colSpecialInstructions = gridView1.Columns["SpecialInstructions"];
            colSpecialInstructions.Width = 100;

            GridColumn colComplexSigns = gridView1.Columns["ComplexSigns"];
            colComplexSigns.Width = 100;
        }
        private void tsbView_Click(object sender, EventArgs e)
        {
            PortfolioView();
        }

        private void tsbExcel_Click(object sender, EventArgs e)
        {

        }
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            PortfolioView();
        }
        void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView currentView = sender as GridView;
            if (e.Column.FieldName == "FixedIncome")
            {
                if (Convert.ToSingle(currentView.GetRowCellValue(e.RowHandle, "FixedIncome")) > Convert.ToSingle(currentView.GetRowCellValue(e.RowHandle, "HF_FixedIncome"))) 
                    e.Appearance.BackColor = System.Drawing.Color.Red;
            }
            if (e.Column.FieldName == "Equities")
            {
                if (Convert.ToSingle(currentView.GetRowCellValue(e.RowHandle, "Equities")) > Convert.ToSingle(currentView.GetRowCellValue(e.RowHandle, "HF_Equities")))
                    e.Appearance.BackColor = System.Drawing.Color.Red;
            }
            if (e.Column.FieldName == "Cash")
            {
                if (Convert.ToSingle(currentView.GetRowCellValue(e.RowHandle, "Cash")) > Convert.ToSingle(currentView.GetRowCellValue(e.RowHandle, "HF_Cash")))
                    e.Appearance.BackColor = System.Drawing.Color.Red;
            }
        }
        void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            i = gridView1.FocusedRowHandle;

            int[] selectedRows = gridView1.GetSelectedRows();
            foreach (int rowHandle in selectedRows)
            {
                i = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "ID"));
                lblContracts_Balances_ID.Text = gridView1.GetRowCellValue(rowHandle, "ID") + "";
                lblCustodian.Text = gridView1.GetRowCellValue(rowHandle, "Custodian") + "";
                lblMiFID_2.Text = gridView1.GetRowCellValue(rowHandle, "MiFID_2") + "";
                lblXAA.Text = gridView1.GetRowCellValue(rowHandle, "XAA") + "";
                txtNotes.Text = gridView1.GetRowCellValue(rowHandle, "Notes") + "";
                lblSpecialInstructions.Text = gridView1.GetRowCellValue(rowHandle, "SpecialInstructions") + "";
                lblComplexData.Text = (gridView1.GetRowCellValue(rowHandle, "ComplexSigns") + "").Replace(",", "\n");               
            }
        }
        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Right))
            {
                MessageBox.Show("aaaa");
                return;
            }

            //GridHitInfo info = (sender as GridView).CalcHitInfo(e.Location);
            //int rowHandle = info.InRow ? info.RowHandle : GridControl.InvalidRowHandle;
            //MessageBox.Show(rowHandle.ToString());
        }

        private void gridView1_FocusedRowHandle(object sender, FocusedRowChangedEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;
            if (view.IsGroupRow(e.FocusedRowHandle))
            {
                bool expanded = view.GetRowExpanded(e.FocusedRowHandle);
                view.SetRowExpanded(e.FocusedRowHandle, !expanded);
            }
        }

        private void fgList_DoubleClick(object sender, EventArgs e)
        {
            PortfolioView();
        }
        private void PortfolioView()
        {
            i = 0;
            int[] selectedRows = gridView1.GetSelectedRows();
            foreach (int rowHandle in selectedRows)
            {
                i = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "ID"));
                if (i != 0)
                {
                    frmPortfolio locPortfolio = new frmPortfolio();
                    locPortfolio.DateControl = dDateControl.Value.Date;
                    locPortfolio.Contracts_Balances_ID = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "ID"));
                    locPortfolio.CDP_ID = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "CDP_ID"));
                    locPortfolio.RightsLevel = iRightsLevel;
                    locPortfolio.ShowDialog();
                }
            }
        }
        private void txtNotes_LostFocus(object sender, EventArgs e)
        {
            gridView1.SetRowCellValue(gridView1.FocusedRowHandle, gridView1.Columns["Notes"], txtNotes.Text);

            Contracts_Balances = new clsContracts_Balances();
            Contracts_Balances.Record_ID = Convert.ToInt32(lblContracts_Balances_ID.Text);
            Contracts_Balances.GetRecord();
            Contracts_Balances.Notes = txtNotes.Text;
            Contracts_Balances.EditRecord();
        }
        private void tsbImport_Click(object sender, EventArgs e)
        {
            txtFilePath_Import.Text = "";
            txtFilePath2_Import.Text = "";  
            panImport.Visible = true;
        }
        private void picFilesPath_Click(object sender, EventArgs e)
        {
            txtFilePath_Import.Text = Global.FileChoice(Global.DefaultFolder);
        }
        private void picFilesPath2_Click(object sender, EventArgs e)
        {
            txtFilePath2_Import.Text = Global.FileChoice(Global.DefaultFolder);
        }
        private void picClose_Click(object sender, EventArgs e)
        {
            panImport.Visible = false;
        }

        private void picMin_Click(object sender, EventArgs e)
        {
            grdList.Width = this.Width - 302;
            panDetails.Visible = true;
            picMin.Visible = false;
            picMax.Visible = true;
        }

        private void picMax_Click(object sender, EventArgs e)
        {
            grdList.Width = this.Width - 32;
            panDetails.Visible = false;
            picMin.Visible = true;
            picMax.Visible = false;
        }

        private void btnGetImport_Click(object sender, EventArgs e)
        {
            DataTable dtContracts_Balances, dtContracts_BalancesRecs;
            DataRow dtRow;
            DataView dtView;
            int iTemp = 0, iShareCodes_ID = 0, iIC_AA_ID = 0, iInvestCategories_ID = 0, iCountryGroup_ID = 0, iDepository_ID = 0, iProduct_ID = 0, iProductCategory_ID = 0, iProduct_Group;
            float fltCurrentValue_EUR = 0;
            string sTemp = "",  sCurrency = "", sComplexSigns = "", sSpecialInstructions = ""; 

            var ExApp = new Microsoft.Office.Interop.Excel.Application();

            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            //--- set DataTable Contracts_Balances columns --------------------------
            dtContracts_Balances = new DataTable("Contracts_Balances_Table");
            dtContracts_Balances.Columns.Add("DateIns", typeof(DateTime));
            dtContracts_Balances.Columns.Add("CDP_ID", typeof(int));
            dtContracts_Balances.Columns.Add("TotalSecurutiesValue", typeof(decimal));
            dtContracts_Balances.Columns.Add("TotalCashValue", typeof(decimal));
            dtContracts_Balances.Columns.Add("TotalValue", typeof(decimal));
            dtContracts_Balances.Columns.Add("IC_AA_ID", typeof(int));
            dtContracts_Balances.Columns.Add("FixedIncome", typeof(float));
            dtContracts_Balances.Columns.Add("Equities", typeof(float));
            dtContracts_Balances.Columns.Add("Cash", typeof(float));
            dtContracts_Balances.Columns.Add("EUR", typeof(float));
            dtContracts_Balances.Columns.Add("USD_etc", typeof(float));
            dtContracts_Balances.Columns.Add("EmergingCurrencies", typeof(float));
            dtContracts_Balances.Columns.Add("DevelopedMarkets", typeof(float));
            dtContracts_Balances.Columns.Add("EmergingMarkets", typeof(float));
            dtContracts_Balances.Columns.Add("Notes", typeof(string));
            dtContracts_Balances.Columns.Add("SpecialInstructions", typeof(string));
            dtContracts_Balances.Columns.Add("ComplexSigns", typeof(string));

            //--- set DataTable Contracts_BalancesRecs columns --------------------------
            dtContracts_BalancesRecs = new DataTable("Contracts_BalancesRecs_Table");
            dtContracts_BalancesRecs.Columns.Add("Code", typeof(string));
            dtContracts_BalancesRecs.Columns.Add("Portfolio", typeof(string));
            dtContracts_BalancesRecs.Columns.Add("CDP_ID", typeof(int));
            dtContracts_BalancesRecs.Columns.Add("Product_ID", typeof(int));
            dtContracts_BalancesRecs.Columns.Add("ProductCategory_ID", typeof(int));
            dtContracts_BalancesRecs.Columns.Add("ISIN", typeof(string));
            dtContracts_BalancesRecs.Columns.Add("ShareCodes_ID", typeof(int));
            dtContracts_BalancesRecs.Columns.Add("Share_Title", typeof(string));
            dtContracts_BalancesRecs.Columns.Add("Currency", typeof(string));
            dtContracts_BalancesRecs.Columns.Add("Depository_ID", typeof(int));
            dtContracts_BalancesRecs.Columns.Add("Depository_Title", typeof(string));
            dtContracts_BalancesRecs.Columns.Add("RefDate", typeof(DateTime));
            dtContracts_BalancesRecs.Columns.Add("TotalUnits", typeof(float));
            dtContracts_BalancesRecs.Columns.Add("AvgNetPrice", typeof(decimal));
            dtContracts_BalancesRecs.Columns.Add("CurrentPrice", typeof(float));
            dtContracts_BalancesRecs.Columns.Add("CurrentValue_RepCcy", typeof(float));
            dtContracts_BalancesRecs.Columns.Add("Unrealized_ProdCcy_PRC", typeof(float));
            dtContracts_BalancesRecs.Columns.Add("Unrealized_RepCcy_PRC", typeof(float));
            dtContracts_BalancesRecs.Columns.Add("Participation_PRC", typeof(float));
            dtContracts_BalancesRecs.Columns.Add("InvestCategories_ID", typeof(int));
            dtContracts_BalancesRecs.Columns.Add("InvestCategories_Title", typeof(string));

            if (txtFilePath_Import.Text.Length > 0)
            {
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = ExApp.Workbooks.Open(txtFilePath_Import.Text);
                Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

                i = 1;
                while (true)
                {
                    i = i + 1;

                    sTemp = (xlRange.Cells[i, 1].Value + "").ToString();
                    if (sTemp == "") break;

                    fltCurrentValue_EUR = Convert.ToSingle(xlRange.Cells[i, 12].Value);

                    //--- define Contract_ID ---------------------------------
                    klsContract = new clsContracts();
                    klsContract.Code = xlRange.Cells[i, 1].Value.ToString();
                    klsContract.Portfolio = xlRange.Cells[i, 2].Value.ToString();
                    klsContract.GetRecord_Code_Portfolio();

                    //--- define iShareCodes_ID value ----------------------- 
                    iShareCodes_ID = 0;

                    sTemp = (xlRange.Cells[i, 6].Value + "").ToString();
                    if (sTemp.ToUpper() != "CASH")                                          // it's security
                    {
                        iTemp = 0;                                                          // 0 - SE_ID
                        if ((xlRange.Cells[i, 23].Value + "").ToString() != "")
                        {
                            foundRows = Global.dtStockExchanges.Select("Code = '" + (xlRange.Cells[i, 23].Value).ToString() + "'");
                            if (foundRows.Length > 0)
                                iTemp = Convert.ToInt32(foundRows[0]["ID"]);
                        }

                        klsProductsCodes = new clsProductsCodes();
                        klsProductsCodes.ISIN = xlRange.Cells[i, 5].Value.ToString();
                        sCurrency = xlRange.Cells[i, 7].Value.ToString() + "";
                        klsProductsCodes.Currency = sCurrency;
                        klsProductsCodes.StockExchange_ID = iTemp;
                        klsProductsCodes.Status = -1;                                        // - 1  - select active and nonactive products
                        klsProductsCodes.GetRecord_ISIN();
                        iShareCodes_ID = klsProductsCodes.Record_ID;

                        iProduct_ID = klsProductsCodes.Product_ID;
                        iProductCategory_ID = klsProductsCodes.ProductCategory_ID;
                        iProduct_Group = klsProductsCodes.Product_Group;
                        iCountryGroup_ID = klsProductsCodes.CountryGroup_ID;

                        //--- define sComplexSigns value ------------------------------------
                        sComplexSigns = "";
                        if (klsContract.Details.ChkComplex == 1)
                        {
                            klsContracts_ComplexSigns = new clsContracts_ComplexSigns();
                            klsContracts_ComplexSigns.Contract_ID = klsContract.Record_ID;
                            klsContracts_ComplexSigns.GetList();
                            foreach (DataRow dtRow1 in klsContracts_ComplexSigns.List.Rows)
                                sComplexSigns = sComplexSigns + "- " + dtRow1["ComplexSign_Title"] + ",";
                        }

                        //--- define sSpecialInstructions value ------------------------------------
                        sSpecialInstructions = "Γεωγραφική Κατανομή:";
                        if (klsContract.Details.ChkWorld == 1) sSpecialInstructions = sSpecialInstructions + " Παγκόσμια";
                        if (klsContract.Details.ChkGreece == 1) sSpecialInstructions = sSpecialInstructions + " Ελλάδα";
                        if (klsContract.Details.ChkEurope == 1) sSpecialInstructions = sSpecialInstructions + " Ευρώπη (εκτός Ελλάδας)";
                        if (klsContract.Details.ChkAmerica == 1) sSpecialInstructions = sSpecialInstructions + " Αμερική";
                        if (klsContract.Details.ChkAsia == 1) sSpecialInstructions = sSpecialInstructions + " Ασία";

                        sSpecialInstructions = sSpecialInstructions + "\nΕπιθυμητή Κατανομή Κεφαλαίων: " + klsContract.Details.IncomeProducts + " / " + klsContract.Details.CapitalProducts;
                        if (klsContract.Details.ChkSpecificConstraints == 1)
                        {
                            sSpecialInstructions = sSpecialInstructions + "\nΕιδικοι περιορισμοί: Επιθυμεί";
                            if (klsContract.Details.ChkMonetaryRisk == 1) sSpecialInstructions = sSpecialInstructions + "\n - Δεν επιθυμεί να αναλάβει νομισματικό κίνδυνο";
                            if (klsContract.Details.ChkIndividualBonds == 1) sSpecialInstructions = sSpecialInstructions + "\n - Μεμονωμένα ομόλογα";
                            if (klsContract.Details.ChkMutualFunds == 1) sSpecialInstructions = sSpecialInstructions + "\n - Ομολογιακά ΑΚ";
                            if (klsContract.Details.ChkBondedETFs == 1) sSpecialInstructions = sSpecialInstructions + "\n - Ομολογιακά ΔΑΚ";
                            if (klsContract.Details.ChkIndividualShares == 1) sSpecialInstructions = sSpecialInstructions + "\n - Μεμονωμένες Μετοχές";
                            if (klsContract.Details.ChkMixedFunds == 1) sSpecialInstructions = sSpecialInstructions + "\n - Μετοχικά και Μεικτά ΑΚ";
                            if (klsContract.Details.ChkMixedETFs == 1) sSpecialInstructions = sSpecialInstructions + "\n - Μετοχικά και Μεικτά ΔΑΚ";
                            if (klsContract.Details.ChkFunds == 1) sSpecialInstructions = sSpecialInstructions + "\n - Αμοιβαία Κεφάλαια";
                            if (klsContract.Details.ChkETFs == 1) sSpecialInstructions = sSpecialInstructions + "\n - Διαπραγματεύσιμα Αμοιβαία Κεφάλαια";
                            if (klsContract.Details.MiscInstructions.Length > 0) sSpecialInstructions = sSpecialInstructions + "\n - Άλλες Ειδικές Οδηγίες του Πελάτη : \n" + klsContract.Details.MiscInstructions;
                        }
                        else sSpecialInstructions = sSpecialInstructions + "\nΕιδικοι περιορισμοί: Δεν επιθυμεί";
                    }
                    else                                                     // 7 - cash
                    {
                        foundRows = Global.dtCurrencies.Select("Title = '" + sCurrency + "'");
                        if (foundRows.Length > 0)
                            iShareCodes_ID = Convert.ToInt32(foundRows[0]["ID"]);

                        iProduct_ID = 7;
                        iProductCategory_ID = 0;
                        iProduct_Group = 4;
                        iCountryGroup_ID = 0;
                    }

                    //--- define iInvestCategories_ID value ----------------------- 
                    iInvestCategories_ID = 0;
                    foundRows = Global.dtTrxInvestCategories.Select("Title = '" + (xlRange.Cells[i, 16].Value).ToString().Trim() + "'");
                    if (foundRows.Length > 0)
                        iInvestCategories_ID = Convert.ToInt32(foundRows[0]["ID"]);

                    //--- add record into dtContracts_BalancesRecs Table ------------------------------
                    dtRow = dtContracts_BalancesRecs.NewRow();
                    dtRow["Code"] = xlRange.Cells[i, 1].Value.ToString(); 
                    dtRow["Portfolio"] = xlRange.Cells[i, 2].Value.ToString(); 
                    dtRow["CDP_ID"] = Convert.ToInt32(klsContract.CDP_ID);
                    dtRow["Product_ID"] = iProduct_ID;
                    dtRow["ProductCategory_ID"] = iProductCategory_ID;
                    dtRow["ISIN"] = xlRange.Cells[i, 5].Value.ToString();
                    dtRow["ShareCodes_ID"] = iShareCodes_ID;
                    dtRow["Share_Title"] = xlRange.Cells[i, 6].Value.ToString();
                    dtRow["Currency"] = xlRange.Cells[i, 7].Value.ToString();
                    dtRow["Depository_ID"] = 0;
                    dtRow["Depository_Title"] = "";
                    dtRow["RefDate"] = Convert.ToDateTime(xlRange.Cells[i, 8].Value + "");
                    dtRow["TotalUnits"] = Convert.ToDecimal(xlRange.Cells[i, 9].Value);
                    dtRow["AvgNetPrice"] = Convert.ToDecimal(xlRange.Cells[i, 10].Value);
                    dtRow["CurrentPrice"] = Convert.ToSingle(xlRange.Cells[i, 11].Value);
                    dtRow["CurrentValue_RepCcy"] = Convert.ToSingle(xlRange.Cells[i, 12].Value);
                    dtRow["Unrealized_ProdCcy_PRC"] = Convert.ToSingle(xlRange.Cells[i, 13].Value);
                    dtRow["Unrealized_RepCcy_PRC"] = Convert.ToSingle(xlRange.Cells[i, 14].Value);
                    dtRow["Participation_PRC"] = Convert.ToSingle(xlRange.Cells[i, 15].Value);
                    dtRow["InvestCategories_ID"] = iInvestCategories_ID;
                    dtRow["InvestCategories_Title"] = xlRange.Cells[i, 16].Value.ToString();
                    dtContracts_BalancesRecs.Rows.Add(dtRow);                    

                    foundRows = dtContracts_Balances.Select("CDP_ID = " + Convert.ToInt32(klsContract.CDP_ID) + " AND DateIns = '" + Convert.ToDateTime(xlRange.Cells[i, 8].Value + "").ToString("yyyy/MM/dd") + "'");
                    if (foundRows.Length == 0)
                    {
                        dtRow = dtContracts_Balances.NewRow();
                        dtRow["DateIns"] = Convert.ToDateTime(xlRange.Cells[i, 8].Value + "").Date;
                        dtRow["CDP_ID"] = Convert.ToInt32(klsContract.CDP_ID);
                        if (iProduct_ID != 7)
                        {
                            dtRow["TotalSecurutiesValue"] = fltCurrentValue_EUR;
                            dtRow["TotalCashValue"] = 0;
                        }
                        else
                        {
                            dtRow["TotalSecurutiesValue"] = 0;
                            dtRow["TotalCashValue"] = fltCurrentValue_EUR;
                        }
                        dtRow["TotalValue"] = 0;

                        iIC_AA_ID = 0;
                        InvestmentCommetties_AssetAllocation = new clsInvestmentCommetties_AssetAllocation();
                        InvestmentCommetties_AssetAllocation.Tipos = sCurrency == "EUR" ? 1 : 2;
                        InvestmentCommetties_AssetAllocation.Profile_ID = klsContract.Profile_ID;
                        InvestmentCommetties_AssetAllocation.GetRecord_Tipos_Profile();
                        if (InvestmentCommetties_AssetAllocation.Record_ID > 0) iIC_AA_ID = InvestmentCommetties_AssetAllocation.Record_ID;
                        dtRow["IC_AA_ID"] = iIC_AA_ID;

                        //--- define Currenct data -------------------------------------------------------------------
                        dtRow["FixedIncome"] = 0;
                        dtRow["Equities"] = 0;
                        dtRow["Cash"] = 0;
                        dtRow["EUR"] = 0;
                        dtRow["USD_etc"] = 0;
                        dtRow["EmergingCurrencies"] = 0;
                        dtRow["DevelopedMarkets"] = 0;
                        dtRow["EmergingMarkets"] = 0;
                        if (iProduct_Group == 1) dtRow["FixedIncome"] = fltCurrentValue_EUR;
                        if (iProduct_Group == 2) dtRow["Equities"] = fltCurrentValue_EUR;
                        if (iProduct_Group == 4) dtRow["Cash"] = fltCurrentValue_EUR;

                        if (sCurrency == "EUR") dtRow["EUR"] = fltCurrentValue_EUR;
                        else if (sCurrency == "USD" || sCurrency == "CHF" || sCurrency == "GBP" || sCurrency == "AUD" || sCurrency == "NZD" || sCurrency == "CAD" || sCurrency == "JPY") dtRow["USD_etc"] = fltCurrentValue_EUR;
                        else if (sCurrency == "RUB" || sCurrency == "HKD" || sCurrency == "BRL" || sCurrency == "INR" || sCurrency == "CNH" || sCurrency == "ZAR") dtRow["EmergingCurrencies"] = fltCurrentValue_EUR;

                        if (iCountryGroup_ID == 1 || iCountryGroup_ID == 8 || iCountryGroup_ID == 9 || iCountryGroup_ID == 11) dtRow["DevelopedMarkets"] = fltCurrentValue_EUR;
                        if (iCountryGroup_ID == 2 || iCountryGroup_ID == 10 || iCountryGroup_ID == 12) dtRow["EmergingMarkets"] = fltCurrentValue_EUR;

                        dtRow["Notes"] = "";
                        dtRow["SpecialInstructions"] = sSpecialInstructions;
                        dtRow["ComplexSigns"] = sComplexSigns;
                        dtContracts_Balances.Rows.Add(dtRow);  
                    }
                    else
                    {
                        if (iProduct_ID != 7)
                            foundRows[0]["TotalSecurutiesValue"] = Convert.ToDecimal(foundRows[0]["TotalSecurutiesValue"]) + Convert.ToDecimal(fltCurrentValue_EUR);
                        else
                            foundRows[0]["TotalCashValue"] = Convert.ToDecimal(foundRows[0]["TotalCashValue"]) + Convert.ToDecimal(fltCurrentValue_EUR);

                        if (iProduct_Group == 1) foundRows[0]["FixedIncome"] = Convert.ToSingle(foundRows[0]["FixedIncome"]) + fltCurrentValue_EUR;
                        if (iProduct_Group == 2) foundRows[0]["Equities"] = Convert.ToSingle(foundRows[0]["Equities"]) + fltCurrentValue_EUR;
                        if (iProduct_Group == 4) foundRows[0]["Cash"] = Convert.ToSingle(foundRows[0]["Cash"]) + fltCurrentValue_EUR;

                        if (sCurrency == "EUR") foundRows[0]["EUR"] = Convert.ToSingle(foundRows[0]["EUR"]) + fltCurrentValue_EUR;
                        else if (sCurrency == "USD" || sCurrency == "CHF" || sCurrency == "GBP" || sCurrency == "AUD" || sCurrency == "NZD" || sCurrency == "CAD" || sCurrency == "JPY") 
                             foundRows[0]["USD_etc"] = Convert.ToSingle(foundRows[0]["USD_etc"]) + fltCurrentValue_EUR; 
                        else if (sCurrency == "RUB" || sCurrency == "HKD" || sCurrency == "BRL" || sCurrency == "INR" || sCurrency == "CNH" || sCurrency == "ZAR")
                            foundRows[0]["EmergingCurrencies"] = Convert.ToSingle(foundRows[0]["EmergingCurrencies"]) + fltCurrentValue_EUR;

                        if (iCountryGroup_ID == 1 || iCountryGroup_ID == 8 || iCountryGroup_ID == 9 || iCountryGroup_ID == 11) foundRows[0]["DevelopedMarkets"] = Convert.ToSingle(foundRows[0]["DevelopedMarkets"]) + fltCurrentValue_EUR;
                        if (iCountryGroup_ID == 2 || iCountryGroup_ID == 10 || iCountryGroup_ID == 12) foundRows[0]["EmergingMarkets"] = Convert.ToSingle(foundRows[0]["EmergingMarkets"]) + fltCurrentValue_EUR;
                    }
                }

                //release com objects to fully kill excel process from running in the background
                Marshal.ReleaseComObject(xlRange);
                Marshal.ReleaseComObject(xlWorksheet);

                //close and release
                xlWorkbook.Close();
                Marshal.ReleaseComObject(xlWorkbook);
            }

            sTemp = "";

            if (txtFilePath2_Import.Text.Length > 0)
            {
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook2 = ExApp.Workbooks.Open(txtFilePath2_Import.Text);
                Microsoft.Office.Interop.Excel._Worksheet xlWorksheet2 = xlWorkbook2.Sheets[1];
                Microsoft.Office.Interop.Excel.Range xlRange2 = xlWorksheet2.UsedRange;

                i = 1;
                while (true)
                {
                    i = i + 1;

                    sTemp = (xlRange2.Cells[i, 1].Value + "").ToString();
                    if (sTemp == "") break;

                    //--- define iDepository_ID value ----------------------- 
                    iDepository_ID = 0;
                    foundRows = Global.dtServiceProviders.Select("DepositoryTitle = '" + (xlRange2.Cells[i, 12].Value).ToString().Trim() + "'");
                    if (foundRows.Length > 0)
                        iDepository_ID = Convert.ToInt32(foundRows[0]["ID"]);

                    foundRows = dtContracts_BalancesRecs.Select("Code = '" + xlRange2.Cells[i, 1].Value.ToString() + "' AND Portfolio = '" + xlRange2.Cells[i, 3].Value.ToString() + 
                                         "' AND ISIN = '" + xlRange2.Cells[i, 5].Value.ToString() + "' AND Currency = '" + xlRange2.Cells[i, 7].Value.ToString() +
                                         "' AND TotalUnits = " + xlRange2.Cells[i, 9].Value.ToString().Replace(",", "."));
                    if (foundRows.Length > 0)
                    {
                        foundRows[0]["Depository_ID"] = iDepository_ID;
                        foundRows[0]["Depository_Title"] = xlRange2.Cells[i, 12].Value.ToString();
                    }
                }

                //release com objects to fully kill excel process from running in the background
                Marshal.ReleaseComObject(xlRange2);
                Marshal.ReleaseComObject(xlWorksheet2);

                //close and release
                xlWorkbook2.Close();
                Marshal.ReleaseComObject(xlWorkbook2);
            }

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();


            //quit and release
            ExApp.Quit();
            Marshal.ReleaseComObject(ExApp);


            dtView = new DataView(dtContracts_BalancesRecs);
            dtView.Sort = "Code, Portfolio";

            foreach (DataRowView dtViewRow in dtView)
            {
                Contracts_BalancesRecs = new clsContracts_BalancesRecs();
                Contracts_BalancesRecs.CDP_ID = Convert.ToInt32(dtViewRow["CDP_ID"]);
                Contracts_BalancesRecs.ShareCodes_ID = Convert.ToInt32(dtViewRow["ShareCodes_ID"]);
                Contracts_BalancesRecs.Product_ID = Convert.ToInt32(dtViewRow["Product_ID"]);
                Contracts_BalancesRecs.ProductCategory_ID = Convert.ToInt32(dtViewRow["ProductCategory_ID"]);
                Contracts_BalancesRecs.Depository_ID = Convert.ToInt32(dtViewRow["Depository_ID"]);
                Contracts_BalancesRecs.RefDate = Convert.ToDateTime(dtViewRow["RefDate"]);
                Contracts_BalancesRecs.TotalUnits = Convert.ToSingle(dtViewRow["TotalUnits"]);
                Contracts_BalancesRecs.AvgNetPrice = Convert.ToDecimal(dtViewRow["AvgNetPrice"]);
                Contracts_BalancesRecs.CurrentPrice = Convert.ToSingle(dtViewRow["CurrentPrice"]);
                Contracts_BalancesRecs.CurrentValue_RepCcy = Convert.ToSingle(dtViewRow["CurrentValue_RepCcy"]);
                Contracts_BalancesRecs.Unrealized_ProdCcy_PRC = Convert.ToSingle(dtViewRow["Unrealized_ProdCcy_PRC"]);
                Contracts_BalancesRecs.Unrealized_RepCcy_PRC = Convert.ToSingle(dtViewRow["Unrealized_RepCcy_PRC"]);
                Contracts_BalancesRecs.Participation_PRC = Convert.ToSingle(dtViewRow["Participation_PRC"]);
                Contracts_BalancesRecs.InvestCategories_ID = Convert.ToInt32(dtViewRow["InvestCategories_ID"]);
                Contracts_BalancesRecs.InsertRecord();
            }

            foreach (DataRow dtRow1 in dtContracts_Balances.Rows)
            {
                Contracts_Balances = new clsContracts_Balances();
                Contracts_Balances.DateIns = Convert.ToDateTime(dtRow1["DateIns"]);
                Contracts_Balances.CDP_ID = Convert.ToInt32(dtRow1["CDP_ID"]);
                Contracts_Balances.IC_AA_ID = Convert.ToInt32(dtRow1["IC_AA_ID"]);
                Contracts_Balances.TotalSecurutiesValue = Convert.ToDecimal(dtRow1["TotalSecurutiesValue"]);
                Contracts_Balances.TotalCashValue = Convert.ToDecimal(dtRow1["TotalCashValue"]);
                Contracts_Balances.TotalValue = Convert.ToDecimal(dtRow1["TotalSecurutiesValue"]) + Convert.ToDecimal(dtRow1["TotalCashValue"]);

                if (Contracts_Balances.TotalValue != 0)
                {
                    /*
                    Contracts_Balances.FixedIncome = Convert.ToSingle(dtRow1["FixedIncome"]) * 100 / Convert.ToSingle(Contracts_Balances.TotalValue);
                    Contracts_Balances.Equities = Convert.ToSingle(dtRow1["Equities"]) * 100 / Convert.ToSingle(Contracts_Balances.TotalValue);
                    Contracts_Balances.Cash = Convert.ToSingle(dtRow1["Cash"]) * 100 / Convert.ToSingle(Contracts_Balances.TotalValue);
                    Contracts_Balances.EUR = Convert.ToSingle(dtRow1["EUR"]) * 100 / Convert.ToSingle(Contracts_Balances.TotalValue);
                    Contracts_Balances.USD_etc = Convert.ToSingle(dtRow1["USD_etc"]) * 100 / Convert.ToSingle(Contracts_Balances.TotalValue);
                    Contracts_Balances.EmergingCurrencies = Convert.ToSingle(dtRow1["EmergingCurrencies"]) * 100 / Convert.ToSingle(Contracts_Balances.TotalValue);
                    Contracts_Balances.DevelopedMarkets = Convert.ToSingle(dtRow1["DevelopedMarkets"]) * 100 / Convert.ToSingle(Contracts_Balances.TotalValue);
                    Contracts_Balances.EmergingMarkets = Convert.ToSingle(dtRow1["EmergingMarkets"]) * 100 / Convert.ToSingle(Contracts_Balances.TotalValue);
                    */
                }
                Contracts_Balances.Notes = dtRow1["Notes"] + "";
                //Contracts_Balances.SpecialInstructions = dtRow1["SpecialInstructions"] + "";
                //Contracts_Balances.ComplexSigns = dtRow1["ComplexSigns"] + "";
                Contracts_Balances.InsertRecord();
            }

            DefineList();
            this.Cursor = Cursors.Default;
            panImport.Visible = false;
        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }

    }
}
