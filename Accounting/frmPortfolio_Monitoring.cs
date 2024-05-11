using System;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using C1.Win.C1FlexGrid;
using Core;

namespace Accounting
{
    public partial class frmPortfolio_Monitoring : Form
    {
        int i, iRightsLevel, iCountryGroup_ID;
        float fltTemp, fltCurrentValue_EUR;
        string sTemp = "";
        string[] sProduct_Group = { "", "Fixed Income", "Equities",  "Others", "Cash" };
        DataTable dtList, dtAssetAllocations, dtDB, dtAA, dtSI, dtSP;
        DataRow[] foundRows, seekRows;
        DataRow dtRow1;
        CellStyle csBalance, csWarning;
        Global.ContractData stContract = new Global.ContractData();
        Global.ProductData stProduct = new Global.ProductData();
        clsProductsCodes ProductCode = new clsProductsCodes();
        clsContracts_Balances Contracts_Balances = new clsContracts_Balances();
        clsContracts_BalancesRecs Contracts_BalancesRecs = new clsContracts_BalancesRecs();
        clsContracts_Details_Packages Contracts_Details_Packages = new clsContracts_Details_Packages();
        clsContracts klsContract = new clsContracts();
        clsContracts_Details klsContractDetails = new clsContracts_Details();
        clsProductsCodes klsProductsCodes = new clsProductsCodes();
        clsContracts_ComplexSigns Contract_ComplexSigns = new clsContracts_ComplexSigns();
        clsInvestmentCommetties_AssetAllocation InvestmentCommetties_AssetAllocation = new clsInvestmentCommetties_AssetAllocation();

        public frmPortfolio_Monitoring()
        {
            InitializeComponent();
        }

        private void frmPortfolio_Monitoring_Load(object sender, EventArgs e)
        {
            csBalance = fgDebitBalance.Styles.Add("Balance");
            csBalance.ForeColor = Color.Red;

            csWarning = fgAssetAllocation.Styles.Add("Warning");
            csWarning.ForeColor = Color.Red;

            //--- set DataTable for fgDebitBalances table --------------------------
            dtDB = new DataTable("SpecialInstructions");
            dtDB.Columns.Add("Currency", typeof(string));
            dtDB.Columns.Add("Balance", typeof(float));

            //--- set DataTable for fgAssetAllocation table --------------------------
            dtAA = new DataTable("AssetAllocation_Table");
            dtAA.Columns.Add("AA", typeof(int));
            dtAA.Columns.Add("ID", typeof(int));
            dtAA.Columns.Add("Title", typeof(string));
            dtAA.Columns.Add("HF_Percent", typeof(string));
            dtAA.Columns.Add("Current_Percent", typeof(float));
            dtAA.Columns.Add("Difference", typeof(float));
            dtAA.Columns.Add("MainValue", typeof(float));
            dtAA.Columns.Add("MinValue", typeof(float));
            dtAA.Columns.Add("MaxValue", typeof(float));
            dtAA.Columns.Add("Flag", typeof(int));

            //--- set DataTable for fgSpecialInstructions table --------------------------
            dtSI = new DataTable("SpecialInstructions");
            dtSI.Columns.Add("ID", typeof(int));
            dtSI.Columns.Add("Title", typeof(string));
            dtSI.Columns.Add("Contract_Data", typeof(string));
            dtSI.Columns.Add("Current_Data", typeof(string));
            dtSI.Columns.Add("Flag", typeof(int));

            //--- set DataTable for fgSuitableProducts table --------------------------
            dtSP = new DataTable("SuitableProducts");
            dtSP.Columns.Add("ID", typeof(int));
            dtSP.Columns.Add("Title", typeof(string));
            dtSP.Columns.Add("ISIN", typeof(string));
            dtSP.Columns.Add("Category_SubCategory", typeof(string));


            dDateControl.Value = DateTime.Now.Date;

            gridView1 = grdList.MainView as GridView;
            gridView1.FocusedRowObjectChanged += gridView1_FocusedRowObjectChanged;
            gridView1.DoubleClick += gridView1_DoubleClick;
            gridView1.RowCellStyle += new RowCellStyleEventHandler(gridView1_RowCellStyle);
            gridView1.HorzScrollVisibility = ScrollVisibility.Always;

            //------- fgDebitBalance ----------------------------
            fgDebitBalance.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            fgDebitBalance.Styles.ParseString(Global.GridStyle);

            //------- fgAssetAllocation ----------------------------
            fgAssetAllocation.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            fgAssetAllocation.Styles.ParseString(Global.GridStyle);

            //------- fgSpecialInstructions ----------------------------
            fgSpecialInstructions.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            fgSpecialInstructions.Styles.ParseString(Global.GridStyle);
            fgSpecialInstructions.ShowCellLabels = true;

            //------- fgSuitableProducts ----------------------------
            fgSuitableProducts.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            fgSuitableProducts.Styles.ParseString("Normal{Font:Microsoft Sans Serif, 8.25pt; BackColor:White; ForeColor:Red;} Focus{Font:Microsoft Sans Serif, 8.25pt; BackColor:White; ForeColor:Red;} Highlight{Font:Microsoft Sans Serif, 8.25pt; BackColor:White; ForeColor:Red;}");

            DefineList();
        }
        protected override void OnResize(EventArgs e)
        {
            panCritiries.Width = this.Width - 30;

            panTools.Width = this.Width - 30;

            grdList.Width = this.Width - 518;
            grdList.Height = this.Height - 146;

            panDetails.Left = this.Width - 502;
            panDetails.Height = this.Height - 146;
            panDetails.Width = 480;

            grpNotes.Width = panDetails.Width - 12;
            txtNotes.Width = panDetails.Width - 28;

            grpSpecialInstructions.Width = panDetails.Width - 12;

            grpSuitableProducts.Width = panDetails.Width - 12;
            lblComplexData.Width = panDetails.Width - 32;

            panRecs.Left = (Screen.PrimaryScreen.Bounds.Width - panRecs.Width) / 2;
            panRecs.Top = (Screen.PrimaryScreen.Bounds.Height - panRecs.Height) / 2;
        }
        private void dDateControl_ValueChanged(object sender, EventArgs e)
        {
            DefineList();
        }
        private void DefineList()
        {
            InvestmentCommetties_AssetAllocation = new clsInvestmentCommetties_AssetAllocation();
            InvestmentCommetties_AssetAllocation.DateControl = dDateControl.Value.Date;
            InvestmentCommetties_AssetAllocation.Tipos = 0;
            InvestmentCommetties_AssetAllocation.Profile_ID = 0;
            InvestmentCommetties_AssetAllocation.GetAssetAllocationRecs();
            dtAssetAllocations = InvestmentCommetties_AssetAllocation.List.Copy();

            fgDebitBalance.Rows.Count = 1;
            fgAssetAllocation.Rows.Count = 1;
            fgSpecialInstructions.Rows.Count = 1;
            txtSpecialInstructions.Text = "";
            fgSuitableProducts.Rows.Count = 1;

            Contracts_Balances = new clsContracts_Balances();
            Contracts_Balances.DateIns = dDateControl.Value.Date;
            Contracts_Balances.GetList();
            cmbTotalValue.SelectedIndex = 0;
            SetListFilters();

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

            GridColumn colDebitBalance = gridView1.Columns["DebitBalance"];
            colDebitBalance.Width = 60;

            GridColumn colAssetAllocation = gridView1.Columns["AssetAllocation"];
            colAssetAllocation.Width = 60;

            GridColumn colSpecialInstructions = gridView1.Columns["SpecialInstructions"];
            colSpecialInstructions.Width = 60;

            GridColumn colSuitableProducts = gridView1.Columns["SuitableProducts"];
            colSuitableProducts.Width = 60;

            GridColumn colLeverage = gridView1.Columns["Leverage"];
            colLeverage.Width = 60;

            GridColumn colNotes = gridView1.Columns["Notes"];
            colNotes.Width = 100;       
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
            if (e.Column.FieldName == "DebitBalance") {
                if (Convert.ToSingle(currentView.GetRowCellValue(e.RowHandle, "DebitBalance")) == 0)
                     { e.Appearance.BackColor = System.Drawing.Color.LightCoral; e.Appearance.ForeColor = System.Drawing.Color.LightCoral; }
                else e.Appearance.ForeColor = System.Drawing.Color.Transparent;                
            }

            if (e.Column.FieldName == "AssetAllocation")
            {
                if (Convert.ToSingle(currentView.GetRowCellValue(e.RowHandle, "AssetAllocation")) == 0)
                     { e.Appearance.BackColor = System.Drawing.Color.LightCoral; e.Appearance.ForeColor = System.Drawing.Color.LightCoral; }
                else e.Appearance.ForeColor = System.Drawing.Color.Transparent;
            }

            if (e.Column.FieldName == "SpecialInstructions")
            {
                if (Convert.ToSingle(currentView.GetRowCellValue(e.RowHandle, "SpecialInstructions")) == 0)
                { e.Appearance.BackColor = System.Drawing.Color.LightCoral; e.Appearance.ForeColor = System.Drawing.Color.LightCoral; }
                else e.Appearance.ForeColor = System.Drawing.Color.Transparent;
            }

            if (e.Column.FieldName == "SuitableProducts")
            {
                if (Convert.ToSingle(currentView.GetRowCellValue(e.RowHandle, "SuitableProducts")) == 0)
                { e.Appearance.BackColor = System.Drawing.Color.LightCoral; e.Appearance.ForeColor = System.Drawing.Color.LightCoral; }
                else e.Appearance.ForeColor = System.Drawing.Color.Transparent;
            }

            if (e.Column.FieldName == "Leverage")
            {
                if (Convert.ToSingle(currentView.GetRowCellValue(e.RowHandle, "Leverage")) == 0)
                { e.Appearance.BackColor = System.Drawing.Color.LightCoral; e.Appearance.ForeColor = System.Drawing.Color.LightCoral; }
                else e.Appearance.ForeColor = System.Drawing.Color.Transparent;
            }
        }
 
        void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            fgDebitBalance.Redraw = false;
            fgDebitBalance.Rows.Count = 1;

            fgAssetAllocation.Redraw = false;
            fgAssetAllocation.Rows.Count = 1;

            fgSpecialInstructions.Redraw = false;
            fgSpecialInstructions.Rows.Count = 1;
            txtSpecialInstructions.Text = "";

            fgSuitableProducts.Redraw = false;
            fgSuitableProducts.Rows.Count = 1;

            i = gridView1.FocusedRowHandle;

            int[] selectedRows = gridView1.GetSelectedRows();
            foreach (int rowHandle in selectedRows)
            {
                Check_Step1(Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "CDP_ID")), Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "Tipos")), Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "Profile_ID")));

                foreach (DataRow dtRow in dtDB.Rows)
                {
                    fgDebitBalance.AddItem(dtRow["Currency"] + "\t" + dtRow["Balance"]);
                }
                fgDebitBalance.Redraw = true;

                foreach (DataRow dtRow in dtAA.Rows)
                {
                    fgAssetAllocation.AddItem(dtRow["Title"] + "\t" + dtRow["MainValue"] + " (" + dtRow["MinValue"] + " - " + dtRow["MaxValue"] + ") \t" + dtRow["Current_Percent"] + "\t" + 
                                              dtRow["Difference"] + "\t" + dtRow["ID"] + "\t" + dtRow["MainValue"] + "\t" + dtRow["MinValue"] + "\t" + dtRow["MaxValue"] + "\t" + dtRow["Flag"]);
                }
                fgAssetAllocation.Redraw = true;

                foreach (DataRow dtRow in dtSP.Rows)
                {
                    fgSuitableProducts.AddItem(dtRow["Title"] + "\t" + dtRow["ISIN"] + "\t" + dtRow["Category_SubCategory"]);
                }
                fgSuitableProducts.Redraw = true;


                //--------------------------------------------------------------
                txtSpecialInstructions.Text = Check_Step2(Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "Contract_ID")), Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "Contracts_Details_ID")));
                foreach (DataRow dtRow in dtSI.Rows)
                {
                    fgSpecialInstructions.AddItem(dtRow["Title"] + "\t" + dtRow["Contract_Data"] + "\t" + dtRow["Current_Data"] + "\t" + dtRow["Flag"]);
                }
                fgSpecialInstructions.AutoSizeRows();
                fgSpecialInstructions.Redraw = true;

                //--------------------------------------------------------------
                fgSuitableProducts.Redraw = true;

                //--------------------------------------------------------------
                i = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "ID"));
                lblContracts_Balances_ID.Text = gridView1.GetRowCellValue(rowHandle, "ID") + "";
                lblCDP_ID.Text = gridView1.GetRowCellValue(rowHandle, "CDP_ID") + "";
                lblCustodian.Text = gridView1.GetRowCellValue(rowHandle, "Custodian") + "";
                lblMiFID_2.Text = gridView1.GetRowCellValue(rowHandle, "MiFID_2") + "";
                lblXAA.Text = gridView1.GetRowCellValue(rowHandle, "XAA") + "";
                txtNotes.Text = gridView1.GetRowCellValue(rowHandle, "Notes") + "";
                //lblSpecialInstructions.Text = gridView1.GetRowCellValue(rowHandle, "SpecialInstructions") + "";
                lblComplexData.Text = (gridView1.GetRowCellValue(rowHandle, "ComplexSigns") + "").Replace(",", "\n");

                lblSumTotal.Text = gridView1.GetRowCellValue(rowHandle, "TotalValue") + "";
            }

            fgDebitBalance.Redraw = true;
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

            Contracts_Details_Packages = new clsContracts_Details_Packages();
            Contracts_Details_Packages.Record_ID = Convert.ToInt32(lblCDP_ID.Text);
            Contracts_Details_Packages.GetRecord();
            Contracts_Details_Packages.Notes = txtNotes.Text;
            Contracts_Details_Packages.EditRecord();
        }
        private void tsbImport_Click(object sender, EventArgs e)
        {
            txtFilePath_Import.Text = "";
            txtFilePath2_Import.Text = "";
            panImport.Visible = true;
        }

        private void cmbTotalValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetListFilters();
        }

        private void chkMiFID2_CheckedChanged(object sender, EventArgs e)
        {
            SetListFilters();
        }
        private void SetListFilters()
        {
            if (Contracts_Balances.List.Rows.Count > 0)
            {
                sTemp = "";
                if (cmbTotalValue.SelectedIndex == 0) sTemp = "ID > 0";
                else if (cmbTotalValue.SelectedIndex == 1) sTemp = "TotalValue > 0";
                else if (cmbTotalValue.SelectedIndex == 2) sTemp = "TotalValue < 0";
                else if (cmbTotalValue.SelectedIndex == 3) sTemp = "TotalValue = 0";
                if (chkMiFID2.Checked) sTemp = sTemp + " AND MIFID_II = 1";
                dtList = Contracts_Balances.List.Select(sTemp).CopyToDataTable();
                grdList.DataSource = dtList;
            }
            else
                grdList.DataSource = Contracts_Balances.List;
        }

        private void picFilesPath_Click(object sender, EventArgs e)
        {
            txtFilePath_Import.Text = Global.FileChoice(Global.DefaultFolder);
        }

        private void fgAssetAllocation_DoubleClick(object sender, EventArgs e)
        {
            lblCategory.Text = fgAssetAllocation[fgAssetAllocation.Row, "Title"]+"";
            fgRecs.Redraw = false;
            fgRecs.Rows.Count = 1;

            fltTemp = 0;
            i = 0;
            foreach (DataRow dtRow in Contracts_BalancesRecs.List.Rows)
            {
                if (sProduct_Group[Convert.ToInt32(dtRow["Product_Group"])] == lblCategory.Text) {
                    i = i + 1;
                    fltTemp = fltTemp + Convert.ToSingle(dtRow["CurrentValue_RepCcy"]);
                    fgRecs.AddItem(i + "\t" + dtRow["ISIN"] + "\t" + dtRow["ShareCodes_Title"] + "\t" + dtRow["Curr"] + "\t" + dtRow["TotalUnits"] + "\t" + dtRow["AvgNetPrice"] + "\t" +
                                   dtRow["CurrentPrice"] + "\t" + dtRow["CurrentValue_RepCcy"] + "\t" + dtRow["Participation_PRC"] + "\t" + sProduct_Group[Convert.ToInt32(dtRow["Product_Group"])] + "\t" +
                                   dtRow["ProductCategory_Title"] + "\t" + dtRow["ID"] + "\t" + dtRow["Product_Group"]);
                }
            }
            fgRecs.Redraw = true;

            lblSumCategory.Text = fltTemp.ToString("#.00");
            lblPrcCategory.Text = (fltTemp * 100.0 / Convert.ToSingle(lblSumTotal.Text)).ToString("#.00");

            panRecs.Visible = true;
        }

        private void picRefresh_Click(object sender, EventArgs e)
        {
            DefineList();
        }

        private void fgDebitBalance_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Row > 0)
            {
                if (e.Col == 1)    {                                                                           // 1 - Balance
                    if (Convert.ToSingle(fgDebitBalance[e.Row, "Balance"]) < 0) fgDebitBalance.Rows[e.Row].Style = csBalance;
                    else fgDebitBalance.Rows[e.Row].Style = null;
                }
            }
        }
        private void fgAssetAllocation_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Row > 0)
            {
                if (e.Col == 8) {                                                                              // 8 - Flag
                    if (Convert.ToInt32(fgAssetAllocation[e.Row, "Flag"]) < 0) fgAssetAllocation.Rows[e.Row].Style = csWarning;
                    else fgAssetAllocation.Rows[e.Row].Style = null;
                }
            }
        }
        private void picClose_Recs_Click(object sender, EventArgs e)
        {
            panRecs.Visible = false;
        }

        private void picFilesPath2_Click(object sender, EventArgs e)
        {
            txtFilePath2_Import.Text = Global.FileChoice(Global.DefaultFolder);
        }
        private void picClose_Click(object sender, EventArgs e)
        {
            panImport.Visible = false;
        }
               
        private void btnGetImport_Click(object sender, EventArgs e)
        {
            DataTable dtContracts_Balances, dtContracts_BalancesRecs;
            DataRow dtRow;
            DataView dtView;
            int i, iTemp, iShareCodes_ID, iIC_AA_Tipos, iIC_AA_ID, iIC_AA_Recs_ID, iInvestCategories_ID, iCountryGroup_ID, iDepository_ID, iProduct_ID, iProductCategory_ID, iProduct_Group;
            string sTemp = "", sCurrency = "";
            Boolean bDebitBalanceProblem = false, bAssetAllocationProblem = false, bSpecialInstructionsProblem = false, bSuitableProductsProblem = false, bLeverageProblem = false;

            iIC_AA_Tipos = 0;
            fltTemp = 0;
            fltCurrentValue_EUR = 0;

            var ExApp = new Microsoft.Office.Interop.Excel.Application();

            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            //--- set DataTable Contracts_Balances columns --------------------------
            dtContracts_Balances = new DataTable("Contracts_Balances_Table");
            dtContracts_Balances.Columns.Add("DateIns", typeof(DateTime));
            dtContracts_Balances.Columns.Add("CDP_ID", typeof(int));
            dtContracts_Balances.Columns.Add("Contract_ID", typeof(int));
            dtContracts_Balances.Columns.Add("Contracts_Details_ID", typeof(int));
            dtContracts_Balances.Columns.Add("Contracts_Packages_ID", typeof(int));
            dtContracts_Balances.Columns.Add("Profile_ID", typeof(int));
            dtContracts_Balances.Columns.Add("IC_AA_Tipos", typeof(int));
            dtContracts_Balances.Columns.Add("IC_AA_ID", typeof(int));
            dtContracts_Balances.Columns.Add("TotalSecurutiesValue", typeof(decimal));
            dtContracts_Balances.Columns.Add("TotalCashValue", typeof(decimal));
            dtContracts_Balances.Columns.Add("TotalValue", typeof(decimal));
            dtContracts_Balances.Columns.Add("DebitBalance", typeof(float));
            dtContracts_Balances.Columns.Add("AssetAllocation", typeof(float));
            dtContracts_Balances.Columns.Add("SpecialInstructions", typeof(float));
            dtContracts_Balances.Columns.Add("SuitableProducts", typeof(float));
            dtContracts_Balances.Columns.Add("Leverage", typeof(float));
            dtContracts_Balances.Columns.Add("Notes", typeof(string));
            dtContracts_Balances.Columns.Add("HF_FixedIncome_Min", typeof(float));
            dtContracts_Balances.Columns.Add("HF_FixedIncome_Max", typeof(float));
            dtContracts_Balances.Columns.Add("HF_Equities_Min", typeof(float));
            dtContracts_Balances.Columns.Add("HF_Equities_Max", typeof(float));
            dtContracts_Balances.Columns.Add("HF_Others_Min", typeof(float));
            dtContracts_Balances.Columns.Add("HF_Others_Max", typeof(float));
            dtContracts_Balances.Columns.Add("HF_Cash_Min", typeof(float));
            dtContracts_Balances.Columns.Add("HF_Cash_Max", typeof(float));
            dtContracts_Balances.Columns.Add("HF_EUR_Min", typeof(float));
            dtContracts_Balances.Columns.Add("HF_EUR_Max", typeof(float));
            dtContracts_Balances.Columns.Add("HF_USD_etc_Min", typeof(float));
            dtContracts_Balances.Columns.Add("HF_USD_etc_Max", typeof(float));
            dtContracts_Balances.Columns.Add("HF_EmergingCurrencies_Min", typeof(float));
            dtContracts_Balances.Columns.Add("HF_EmergingCurrencies_Max", typeof(float));
            dtContracts_Balances.Columns.Add("HF_DevelopedMarkets_Min", typeof(float));
            dtContracts_Balances.Columns.Add("HF_DevelopedMarkets_Max", typeof(float));
            dtContracts_Balances.Columns.Add("HF_EmergingMarkets_Min", typeof(float));
            dtContracts_Balances.Columns.Add("HF_EmergingMarkets_Max", typeof(float));
            dtContracts_Balances.Columns.Add("FixedIncome", typeof(float));
            dtContracts_Balances.Columns.Add("Equities", typeof(float));
            dtContracts_Balances.Columns.Add("Others", typeof(float));
            dtContracts_Balances.Columns.Add("Cash", typeof(float));
            dtContracts_Balances.Columns.Add("EUR", typeof(float));
            dtContracts_Balances.Columns.Add("USD_etc", typeof(float));
            dtContracts_Balances.Columns.Add("EmergingCurrencies", typeof(float));
            dtContracts_Balances.Columns.Add("DevelopedMarkets", typeof(float));
            dtContracts_Balances.Columns.Add("EmergingMarkets", typeof(float));


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
            dtContracts_BalancesRecs.Columns.Add("IC_AA_Recs_ID", typeof(int));

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
                    sCurrency = xlRange.Cells[i, 7].Value.ToString() + "";                    
                    iIC_AA_Recs_ID = 0;                    

                    //--- define Contract_ID ---------------------------------
                    klsContract = new clsContracts();
                    klsContract.Code = xlRange.Cells[i, 1].Value.ToString();
                    klsContract.Portfolio = xlRange.Cells[i, 2].Value.ToString();
                    klsContract.GetRecord_Code_Portfolio();
                    iIC_AA_Tipos = klsContract.Currency == "EUR" ? 1 : 2;

                    if (klsContract.Code == "275")
                        i = i;
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
                        klsProductsCodes.Currency = sCurrency;
                        klsProductsCodes.StockExchange_ID = iTemp;
                        klsProductsCodes.Status = -1;                                        // - 1  - select active and nonactive products

                        klsProductsCodes.GetRecord_ISIN();
                        iShareCodes_ID = klsProductsCodes.Record_ID;
                        iProduct_ID = klsProductsCodes.Product_ID;
                        iProductCategory_ID = klsProductsCodes.ProductCategory_ID;
                        iProduct_Group = klsProductsCodes.Product_Group;
                        iCountryGroup_ID = klsProductsCodes.CountryGroup_ID;

                        foundRows = dtAssetAllocations.Select("Tipos = " + iIC_AA_Tipos + " AND Profile_ID = " + klsContract.Profile_ID);
                        if (foundRows.Length > 0)
                            iIC_AA_Recs_ID = Convert.ToInt32(foundRows[0]["Recs_ID"]);
                    }
                    else                                                     // 7 - cash
                    {
                        foundRows = Global.dtCurrencies.Select("Title = '" + sCurrency + "'");
                        if (foundRows.Length > 0)
                            iShareCodes_ID = Convert.ToInt32(foundRows[0]["ID"]);

                        iProduct_ID = 7;
                        iProductCategory_ID = 0;
                        iProduct_Group = 4;                                 // 1 - FIXED INCOME, 2 - EQUITIES, 3 - OTHER, 4 - CASH
                        iCountryGroup_ID = 0;
                        iIC_AA_Recs_ID = 0;
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
                    dtRow["IC_AA_Recs_ID"] = iIC_AA_Recs_ID;
                    dtContracts_BalancesRecs.Rows.Add(dtRow);

                    foundRows = dtContracts_Balances.Select("CDP_ID = " + Convert.ToInt32(klsContract.CDP_ID) + " AND DateIns = '" + Convert.ToDateTime(xlRange.Cells[i, 8].Value + "").ToString("yyyy/MM/dd") + "'");
                    if (foundRows.Length == 0)
                    {
                        dtRow = dtContracts_Balances.NewRow();
                        dtRow["DateIns"] = Convert.ToDateTime(xlRange.Cells[i, 8].Value + "").Date;
                        dtRow["CDP_ID"] = Convert.ToInt32(klsContract.CDP_ID);
                        dtRow["Contract_ID"] = Convert.ToInt32(klsContract.Record_ID);
                        dtRow["Contracts_Details_ID"] = Convert.ToInt32(klsContract.Contract_Details_ID);
                        dtRow["Contracts_Packages_ID"] = Convert.ToInt32(klsContract.Contract_Packages_ID);
                        dtRow["Profile_ID"] = Convert.ToInt32(klsContract.Profile_ID);
                        dtRow["IC_AA_Tipos"] = iIC_AA_Tipos;

                        iIC_AA_ID = 0;
                        seekRows = dtAssetAllocations.Select("Tipos = " + iIC_AA_Tipos + " AND Profile_ID = " + klsContract.Profile_ID);
                        if (seekRows.Length > 0) iIC_AA_ID = Convert.ToInt32(seekRows[0]["ID"]);
                        dtRow["IC_AA_ID"] = iIC_AA_ID;

                        if (iProduct_ID != 7)
                        {
                            dtRow["TotalSecurutiesValue"] = fltCurrentValue_EUR;
                            dtRow["TotalCashValue"] = 0;
                        }
                        else
                        {
                            dtRow["TotalSecurutiesValue"] = 0;
                            dtRow["TotalCashValue"] = fltCurrentValue_EUR;
                            dtRow["Cash"] = fltCurrentValue_EUR;
                        }
                        dtRow["TotalValue"] = 0;
                        dtRow["Notes"] = klsContract.CDP_Notes;

                        //--- initsialize statuses -------------------------------------------------------------
                        dtRow["DebitBalance"] = 1;
                        dtRow["AssetAllocation"] = 1;
                        dtRow["SpecialInstructions"] = 1;
                        dtRow["SuitableProducts"] = 1;
                        dtRow["Leverage"] = 1;

                        //--- define HF data -------------------------------------------------------------------
                        dtRow["HF_FixedIncome_Min"] = 0;
                        dtRow["HF_FixedIncome_Max"] = 0;
                        dtRow["HF_Equities_Min"] = 0;
                        dtRow["HF_Equities_Max"] = 0;
                        dtRow["HF_Others_Min"] = 0;
                        dtRow["HF_Others_Max"] = 0;
                        dtRow["HF_Cash_Min"] = 0;
                        dtRow["HF_Cash_Max"] = 0;
                        dtRow["HF_EUR_Min"] = 0;
                        dtRow["HF_EUR_Max"] = 0;
                        dtRow["HF_USD_etc_Min"] = 0;
                        dtRow["HF_USD_etc_Max"] = 0;
                        dtRow["HF_EmergingCurrencies_Min"] = 0;
                        dtRow["HF_EmergingCurrencies_Max"] = 0;
                        dtRow["HF_DevelopedMarkets_Min"] = 0;
                        dtRow["HF_DevelopedMarkets_Max"] = 0;
                        dtRow["HF_EmergingMarkets_Min"] = 0;
                        dtRow["HF_EmergingMarkets_Max"] = 0;

                        foreach (DataRow dtRow1 in dtAssetAllocations.Rows)
                        {
                            if (Convert.ToInt32(dtRow1["ID"]) == iIC_AA_ID && Convert.ToInt32(dtRow1["Tipos"]) == iIC_AA_Tipos && Convert.ToInt32(dtRow1["Profile_ID"]) == klsContract.Profile_ID)
                            {
                                switch (dtRow1["Title"])
                                {
                                    case "Fixed Income":
                                        dtRow["HF_FixedIncome_Min"] = dtRow1["MinValue"];
                                        dtRow["HF_FixedIncome_Max"] = dtRow1["MaxValue"];
                                        break;
                                    case "Equities":
                                        dtRow["HF_Equities_Min"] = dtRow1["MinValue"];
                                        dtRow["HF_Equities_Max"] = dtRow1["MaxValue"];
                                        break;
                                    case "Others":
                                        dtRow["HF_Others_Min"] = dtRow1["MinValue"];
                                        dtRow["HF_Others_Max"] = dtRow1["MaxValue"];
                                        break;
                                    case "Cash":
                                        dtRow["HF_Cash_Min"] = dtRow1["MinValue"];
                                        dtRow["HF_Cash_Max"] = dtRow1["MaxValue"];
                                        break;
                                }
                            }
                        }

                        //--- define Current data -------------------------------------------------------------------
                        dtRow["FixedIncome"] = 0;
                        dtRow["Equities"] = 0;
                        dtRow["Others"] = 0;
                        dtRow["Cash"] = 0;
                        dtRow["EUR"] = 0;
                        dtRow["USD_etc"] = 0;
                        dtRow["EmergingCurrencies"] = 0;
                        dtRow["DevelopedMarkets"] = 0;
                        dtRow["EmergingMarkets"] = 0;


                        if (iProduct_Group == 1) dtRow["FixedIncome"] = fltCurrentValue_EUR;
                        if (iProduct_Group == 2) dtRow["Equities"] = fltCurrentValue_EUR;
                        if (iProduct_Group == 3) dtRow["Others"] = fltCurrentValue_EUR;
                        if (iProduct_Group == 4) dtRow["Cash"] = fltCurrentValue_EUR;

                        if (iCountryGroup_ID == 1 || iCountryGroup_ID == 8 || iCountryGroup_ID == 9 || iCountryGroup_ID == 11) dtRow["DevelopedMarkets"] = fltCurrentValue_EUR;
                        if (iCountryGroup_ID == 2 || iCountryGroup_ID == 10 || iCountryGroup_ID == 12) dtRow["EmergingMarkets"] = fltCurrentValue_EUR;

                        if (sCurrency == "EUR") dtRow["EUR"] = fltCurrentValue_EUR;
                        else if (sCurrency == "USD" || sCurrency == "CHF" || sCurrency == "GBP" || sCurrency == "AUD" || sCurrency == "NZD" || sCurrency == "CAD" || sCurrency == "JPY") dtRow["USD_etc"] = fltCurrentValue_EUR;
                        else if (sCurrency == "RUB" || sCurrency == "HKD" || sCurrency == "BRL" || sCurrency == "INR" || sCurrency == "CNH" || sCurrency == "ZAR") dtRow["EmergingCurrencies"] = fltCurrentValue_EUR;
                        
                        if (iCountryGroup_ID == 1 || iCountryGroup_ID == 8 || iCountryGroup_ID == 9 || iCountryGroup_ID == 11) dtRow["DevelopedMarkets"] = fltCurrentValue_EUR;
                        else dtRow["EmergingMarkets"] = fltCurrentValue_EUR;

                        dtContracts_Balances.Rows.Add(dtRow);
                    }
                    else
                    {
                        if (iProduct_ID != 7)
                        {
                            foundRows[0]["TotalSecurutiesValue"] = Convert.ToDecimal(foundRows[0]["TotalSecurutiesValue"]) + Convert.ToDecimal(fltCurrentValue_EUR);

                            if (iProduct_Group == 1) foundRows[0]["FixedIncome"] = Convert.ToSingle(foundRows[0]["FixedIncome"]) + fltCurrentValue_EUR;
                            if (iProduct_Group == 2) foundRows[0]["Equities"] = Convert.ToSingle(foundRows[0]["Equities"]) + fltCurrentValue_EUR;
                            if (iProduct_Group == 3) foundRows[0]["Others"] = Convert.ToSingle(foundRows[0]["Others"]) + fltCurrentValue_EUR;
                            if (iProduct_Group == 4) foundRows[0]["Cash"] = Convert.ToSingle(foundRows[0]["Cash"]) + fltCurrentValue_EUR;

                            if (iCountryGroup_ID == 1 || iCountryGroup_ID == 8 || iCountryGroup_ID == 9 || iCountryGroup_ID == 11) foundRows[0]["DevelopedMarkets"] = Convert.ToSingle(foundRows[0]["DevelopedMarkets"]) + fltCurrentValue_EUR;
                            if (iCountryGroup_ID == 2 || iCountryGroup_ID == 10 || iCountryGroup_ID == 12) foundRows[0]["EmergingMarkets"] = Convert.ToSingle(foundRows[0]["EmergingMarkets"]) + fltCurrentValue_EUR;
                        }
                        else
                        {
                            foundRows[0]["TotalCashValue"] = Convert.ToDecimal(foundRows[0]["TotalCashValue"]) + Convert.ToDecimal(fltCurrentValue_EUR);
                            foundRows[0]["Cash"] = Convert.ToSingle(foundRows[0]["Cash"]) + fltCurrentValue_EUR;
                        }

                        if (sCurrency == "EUR") foundRows[0]["EUR"] = Convert.ToSingle(foundRows[0]["EUR"]) + fltCurrentValue_EUR;
                        else if (sCurrency == "USD" || sCurrency == "CHF" || sCurrency == "GBP" || sCurrency == "AUD" || sCurrency == "NZD" || sCurrency == "CAD" || sCurrency == "JPY")
                            foundRows[0]["USD_etc"] = Convert.ToSingle(foundRows[0]["USD_etc"]) + fltCurrentValue_EUR;
                        else if (sCurrency == "RUB" || sCurrency == "HKD" || sCurrency == "BRL" || sCurrency == "INR" || sCurrency == "CNH" || sCurrency == "ZAR")
                            foundRows[0]["EmergingCurrencies"] = Convert.ToSingle(foundRows[0]["EmergingCurrencies"]) + fltCurrentValue_EUR;

                        if (iCountryGroup_ID == 1 || iCountryGroup_ID == 8 || iCountryGroup_ID == 9 || iCountryGroup_ID == 11) foundRows[0]["DevelopedMarkets"] = Convert.ToSingle(foundRows[0]["DevelopedMarkets"]) + fltCurrentValue_EUR;
                        else foundRows[0]["EmergingMarkets"] = Convert.ToSingle(foundRows[0]["EmergingMarkets"]) + fltCurrentValue_EUR;

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
                Contracts_BalancesRecs.IC_AA_Recs_ID = Convert.ToInt32(dtViewRow["IC_AA_Recs_ID"]);
                Contracts_BalancesRecs.InsertRecord();
            }

            foreach (DataRow dtRow1 in dtContracts_Balances.Rows)
            {
                int iContract_ID, iContract_Details_ID, iContract_Packages_ID, iProfile_ID;
                iContract_ID = Convert.ToInt32(dtRow1["Contract_ID"]);
                iContract_Details_ID = Convert.ToInt32(dtRow1["Contracts_Details_ID"]);
                iContract_Packages_ID = Convert.ToInt32(dtRow1["Contracts_Packages_ID"]);
                iProfile_ID = Convert.ToInt32(dtRow1["Profile_ID"]);
                iIC_AA_Tipos = Convert.ToInt32(dtRow1["IC_AA_Tipos"]);

                Contracts_Balances = new clsContracts_Balances();
                Contracts_Balances.DateIns = Convert.ToDateTime(dtRow1["DateIns"]);
                Contracts_Balances.CDP_ID = Convert.ToInt32(dtRow1["CDP_ID"]);
                Contracts_Balances.IC_AA_ID = Convert.ToInt32(dtRow1["IC_AA_ID"]);
                Contracts_Balances.TotalSecurutiesValue = Convert.ToDecimal(dtRow1["TotalSecurutiesValue"]);
                Contracts_Balances.TotalCashValue = Convert.ToDecimal(dtRow1["TotalCashValue"]);
                Contracts_Balances.TotalValue = Convert.ToDecimal(dtRow1["TotalSecurutiesValue"]) + Convert.ToDecimal(dtRow1["TotalCashValue"]);

                Contracts_Balances.DebitBalance = Convert.ToInt16(dtRow1["DebitBalance"]);

                i = 1;
                if (Contracts_Balances.TotalValue != 0)
                {
                    fltTemp = Convert.ToSingle(dtRow1["FixedIncome"]) * 100 / Convert.ToSingle(Contracts_Balances.TotalValue);
                    if ((Convert.ToSingle(dtRow1["HF_FixedIncome_Min"]) > fltTemp) || (Convert.ToSingle(dtRow1["HF_FixedIncome_Max"]) < fltTemp)) i = 0;

                    fltTemp = Convert.ToSingle(dtRow1["Equities"]) * 100 / Convert.ToSingle(Contracts_Balances.TotalValue);
                    if ((Convert.ToSingle(dtRow1["HF_Equities_Min"]) > fltTemp) || (Convert.ToSingle(dtRow1["HF_Equities_Max"]) < fltTemp)) i = 0;

                    fltTemp = Convert.ToSingle(dtRow1["Others"]) * 100 / Convert.ToSingle(Contracts_Balances.TotalValue);
                    if ((Convert.ToSingle(dtRow1["HF_Others_Min"]) > fltTemp) || (Convert.ToSingle(dtRow1["HF_Others_Max"]) < fltTemp)) i = 0;

                    fltTemp = Convert.ToSingle(dtRow1["Cash"]) * 100 / Convert.ToSingle(Contracts_Balances.TotalValue);
                    if ((Convert.ToSingle(dtRow1["HF_Cash_Min"]) > fltTemp) || (Convert.ToSingle(dtRow1["HF_Cash_Max"]) < fltTemp)) i = 0;

                    fltTemp = Convert.ToSingle(dtRow1["EUR"]) * 100 / Convert.ToSingle(Contracts_Balances.TotalValue);
                    if ((Convert.ToSingle(dtRow1["HF_EUR_Min"]) > fltTemp) || (Convert.ToSingle(dtRow1["HF_EUR_Max"]) < fltTemp)) i = 0;

                    fltTemp = Convert.ToSingle(dtRow1["USD_etc"]) * 100 / Convert.ToSingle(Contracts_Balances.TotalValue);
                    if ((Convert.ToSingle(dtRow1["HF_USD_etc_Min"]) > fltTemp) || (Convert.ToSingle(dtRow1["HF_USD_etc_Max"]) < fltTemp)) i = 0;

                    fltTemp = Convert.ToSingle(dtRow1["EmergingCurrencies"]) * 100 / Convert.ToSingle(Contracts_Balances.TotalValue);
                    if ((Convert.ToSingle(dtRow1["HF_EmergingCurrencies_Min"]) > fltTemp) || (Convert.ToSingle(dtRow1["HF_EmergingCurrencies_Max"]) < fltTemp)) i = 0;
                }
                else i = 0;

                Contracts_Balances.AssetAllocation = i;

                //--- define statuses -------------------------------------------------------------
                Check_Step1(Contracts_Balances.CDP_ID, iIC_AA_Tipos, iProfile_ID);

                bDebitBalanceProblem = false;
                foreach (DataRow dtRow2 in dtDB.Rows)
                    if (Convert.ToInt32(dtRow2["Balance"]) < 0) bDebitBalanceProblem = true;

                bAssetAllocationProblem = false;
                foreach (DataRow dtRow2 in dtAA.Rows)
                    if (Convert.ToInt32(dtRow2["Flag"]) == -1) bAssetAllocationProblem = true;

                Check_Step2(iContract_ID, iContract_Details_ID);
                bSpecialInstructionsProblem = false;
                foreach (DataRow dtRow2 in dtSI.Rows)
                    if (Convert.ToInt32(dtRow2["Flag"]) == -1) bSpecialInstructionsProblem = true;

                bSuitableProductsProblem = false;
                foreach (DataRow dtRow2 in dtSP.Rows)
                    bSuitableProductsProblem = true;

                bLeverageProblem = false;
                                

                if (bDebitBalanceProblem) Contracts_Balances.DebitBalance = 0;
                else Contracts_Balances.DebitBalance = 1;

                if (bSpecialInstructionsProblem) Contracts_Balances.SpecialInstructions = 0;
                else Contracts_Balances.SpecialInstructions = 1;

                if (bSuitableProductsProblem) Contracts_Balances.SuitableProducts = 0;
                else Contracts_Balances.SuitableProducts = 1;

                if (bLeverageProblem) Contracts_Balances.Leverage = 0;
                else Contracts_Balances.Leverage = 1;

                Contracts_Balances.Notes = dtRow1["Notes"] + "";

                Contracts_Balances.InsertRecord();
            }

            DefineList();
            this.Cursor = Cursors.Default;
            panImport.Visible = false;
        }
        private void btnGetImport2_Click(object sender, EventArgs e)
        {
            DataTable dtContracts_Balances, dtContracts_BalancesRecs;
            DataRow dtRow;
            DataView dtView;
            int i, iTemp, iShareCodes_ID, iIC_AA_Tipos, iIC_AA_ID, iIC_AA_Recs_ID, iInvestCategories_ID, iCountryGroup_ID, iDepository_ID, iProduct_ID, iProductCategory_ID, iProduct_Group;
            string sTemp = "", sCurrency = "";
            Boolean bDebitBalanceProblem = false, bAssetAllocationProblem = false, bSpecialInstructionsProblem = false, bSuitableProductsProblem = false, bLeverageProblem = false;

            fltTemp = 0;
            fltCurrentValue_EUR = 0;

            var ExApp = new Microsoft.Office.Interop.Excel.Application();

            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            //--- set DataTable Contracts_Balances columns --------------------------
            dtContracts_Balances = new DataTable("Contracts_Balances_Table");
            dtContracts_Balances.Columns.Add("DateIns", typeof(DateTime));
            dtContracts_Balances.Columns.Add("CDP_ID", typeof(int));
            dtContracts_Balances.Columns.Add("IC_AA_ID", typeof(int));
            dtContracts_Balances.Columns.Add("TotalSecurutiesValue", typeof(decimal));
            dtContracts_Balances.Columns.Add("TotalCashValue", typeof(decimal));
            dtContracts_Balances.Columns.Add("TotalValue", typeof(decimal));
            dtContracts_Balances.Columns.Add("DebitBalance", typeof(float));
            dtContracts_Balances.Columns.Add("AssetAllocation", typeof(float));
            dtContracts_Balances.Columns.Add("SpecialInstructions", typeof(float));
            dtContracts_Balances.Columns.Add("SuitableProducts", typeof(float));
            dtContracts_Balances.Columns.Add("Leverage", typeof(float));
            dtContracts_Balances.Columns.Add("Notes", typeof(string));
            dtContracts_Balances.Columns.Add("HF_FixedIncome_Min", typeof(float));
            dtContracts_Balances.Columns.Add("HF_FixedIncome_Max", typeof(float));
            dtContracts_Balances.Columns.Add("HF_Equities_Min", typeof(float));
            dtContracts_Balances.Columns.Add("HF_Equities_Max", typeof(float));
            dtContracts_Balances.Columns.Add("HF_Others_Min", typeof(float));
            dtContracts_Balances.Columns.Add("HF_Others_Max", typeof(float));
            dtContracts_Balances.Columns.Add("HF_Cash_Min", typeof(float));
            dtContracts_Balances.Columns.Add("HF_Cash_Max", typeof(float));
            dtContracts_Balances.Columns.Add("HF_EUR_Min", typeof(float));
            dtContracts_Balances.Columns.Add("HF_EUR_Max", typeof(float));
            dtContracts_Balances.Columns.Add("HF_USD_etc_Min", typeof(float));
            dtContracts_Balances.Columns.Add("HF_USD_etc_Max", typeof(float));
            dtContracts_Balances.Columns.Add("HF_EmergingCurrencies_Min", typeof(float));
            dtContracts_Balances.Columns.Add("HF_EmergingCurrencies_Max", typeof(float));
            dtContracts_Balances.Columns.Add("HF_DevelopedMarkets_Min", typeof(float));
            dtContracts_Balances.Columns.Add("HF_DevelopedMarkets_Max", typeof(float));
            dtContracts_Balances.Columns.Add("HF_EmergingMarkets_Min", typeof(float));
            dtContracts_Balances.Columns.Add("HF_EmergingMarkets_Max", typeof(float));
            dtContracts_Balances.Columns.Add("FixedIncome", typeof(float));
            dtContracts_Balances.Columns.Add("Equities", typeof(float));
            dtContracts_Balances.Columns.Add("Others", typeof(float));
            dtContracts_Balances.Columns.Add("Cash", typeof(float));
            dtContracts_Balances.Columns.Add("EUR", typeof(float));
            dtContracts_Balances.Columns.Add("USD_etc", typeof(float));
            dtContracts_Balances.Columns.Add("EmergingCurrencies", typeof(float));
            dtContracts_Balances.Columns.Add("DevelopedMarkets", typeof(float));
            dtContracts_Balances.Columns.Add("EmergingMarkets", typeof(float));

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
            dtContracts_BalancesRecs.Columns.Add("IC_AA_Recs_ID", typeof(int));

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
                    sCurrency = xlRange.Cells[i, 7].Value.ToString() + "";
                    iIC_AA_Recs_ID = 0;

                    //--- define Contract_ID ---------------------------------
                    klsContract = new clsContracts();
                    klsContract.Code = xlRange.Cells[i, 1].Value.ToString();
                    klsContract.Portfolio = xlRange.Cells[i, 2].Value.ToString();
                    klsContract.GetRecord_Code_Portfolio();
                    iIC_AA_Tipos = klsContract.Currency == "EUR" ? 1 : 2;

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
                        klsProductsCodes.Currency = sCurrency;
                        klsProductsCodes.StockExchange_ID = iTemp;
                        klsProductsCodes.Status = -1;                                        // - 1  - select active and nonactive products

                        //if (klsProductsCodes.ISIN == "US5949181045") 
                        //    i = i;
                        klsProductsCodes.GetRecord_ISIN();
                        iShareCodes_ID = klsProductsCodes.Record_ID;
                        iProduct_ID = klsProductsCodes.Product_ID;
                        iProductCategory_ID = klsProductsCodes.ProductCategory_ID;
                        iProduct_Group = klsProductsCodes.Product_Group;
                        iCountryGroup_ID = klsProductsCodes.CountryGroup_ID;

                        //if (klsContract.CDP_ID == 26339)
                        //    i = i;

                        //if (klsProductsCodes.ISIN == "JE00B6T5S470")
                        //    i = i;

                        Check_Step1(klsContract.CDP_ID, iIC_AA_Tipos, klsContract.Profile_ID);
                        bAssetAllocationProblem = false;
                        foreach (DataRow dtRow1 in dtAA.Rows)
                            if (Convert.ToInt32(dtRow1["Flag"]) == -1) bAssetAllocationProblem = true;

                        Check_Step2(klsContract.Record_ID, klsContract.Contract_Details_ID);
                        bSpecialInstructionsProblem = false;
                        foreach (DataRow dtRow1 in dtSI.Rows)
                            if (Convert.ToInt32(dtRow1["Flag"]) == -1) bSpecialInstructionsProblem = true;

                        bSuitableProductsProblem = false;
                        foreach (DataRow dtRow1 in dtSP.Rows)
                            bSuitableProductsProblem = true;

                        foundRows = dtAssetAllocations.Select("Tipos = " + iIC_AA_Tipos + " AND Profile_ID = " + klsContract.Profile_ID);
                        if (foundRows.Length > 0)
                            iIC_AA_Recs_ID = Convert.ToInt32(foundRows[0]["Recs_ID"]);

                    }
                    else                                                     // 7 - cash
                    {
                        foundRows = Global.dtCurrencies.Select("Title = '" + sCurrency + "'");
                        if (foundRows.Length > 0)
                            iShareCodes_ID = Convert.ToInt32(foundRows[0]["ID"]);

                        iProduct_ID = 7;
                        iProductCategory_ID = 0;
                        iProduct_Group = 4;                                 // 1 - FIXED INCOME, 2 - EQUITIES, 3 - OTHER, 4 - CASH
                        iCountryGroup_ID = 0;
                        iIC_AA_Recs_ID = 0;
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
                    dtRow["IC_AA_Recs_ID"] = iIC_AA_Recs_ID;
                    dtContracts_BalancesRecs.Rows.Add(dtRow);

                    foundRows = dtContracts_Balances.Select("CDP_ID = " + Convert.ToInt32(klsContract.CDP_ID) + " AND DateIns = '" + Convert.ToDateTime(xlRange.Cells[i, 8].Value + "").ToString("yyyy/MM/dd") + "'");
                    if (foundRows.Length == 0)
                    {
                        bDebitBalanceProblem = false;

                        dtRow = dtContracts_Balances.NewRow();
                        dtRow["DateIns"] = Convert.ToDateTime(xlRange.Cells[i, 8].Value + "").Date;
                        dtRow["CDP_ID"] = Convert.ToInt32(klsContract.CDP_ID);

                        iIC_AA_ID = 0;
                        seekRows = dtAssetAllocations.Select("Tipos = " + iIC_AA_Tipos + " AND Profile_ID = " + klsContract.Profile_ID);
                        if (seekRows.Length > 0) iIC_AA_ID = Convert.ToInt32(seekRows[0]["ID"]);
                        dtRow["IC_AA_ID"] = iIC_AA_ID;

                        if (iProduct_ID != 7)
                        {
                            dtRow["TotalSecurutiesValue"] = fltCurrentValue_EUR;
                            dtRow["TotalCashValue"] = 0;
                        }
                        else
                        {
                            dtRow["TotalSecurutiesValue"] = 0;
                            dtRow["TotalCashValue"] = fltCurrentValue_EUR;
                            dtRow["Cash"] = fltCurrentValue_EUR;
                            if (fltCurrentValue_EUR < 0) bDebitBalanceProblem = true;
                        }
                        dtRow["TotalValue"] = 0;

                        //--- define Contracts_Balances flags ----------------------------------------------------
                        dtRow["DebitBalance"] = 1;
                        if (bDebitBalanceProblem) dtRow["DebitBalance"] = 0;

                        dtRow["AssetAllocation"] = 1;
                        if (bAssetAllocationProblem) dtRow["AssetAllocation"] = 0;

                        dtRow["SpecialInstructions"] = 1;
                        if (bSpecialInstructionsProblem) dtRow["SpecialInstructions"] = 0;

                        dtRow["SuitableProducts"] = 1;
                        if (bSuitableProductsProblem) dtRow["SuitableProducts"] = 0;

                        dtRow["Leverage"] = 1;
                        if (bLeverageProblem) dtRow["Leverage"] = 0;

                        dtRow["Notes"] = "";


                        //--- define HF data -------------------------------------------------------------------
                        dtRow["HF_FixedIncome_Min"] = 0;
                        dtRow["HF_FixedIncome_Max"] = 0;
                        dtRow["HF_Equities_Min"] = 0;
                        dtRow["HF_Equities_Max"] = 0;
                        dtRow["HF_Others_Min"] = 0;
                        dtRow["HF_Others_Max"] = 0;
                        dtRow["HF_Cash_Min"] = 0;
                        dtRow["HF_Cash_Max"] = 0;
                        dtRow["HF_EUR_Min"] = 0;
                        dtRow["HF_EUR_Max"] = 0;
                        dtRow["HF_USD_etc_Min"] = 0;
                        dtRow["HF_USD_etc_Max"] = 0;
                        dtRow["HF_EmergingCurrencies_Min"] = 0;
                        dtRow["HF_EmergingCurrencies_Max"] = 0;
                        dtRow["HF_DevelopedMarkets_Min"] = 0;
                        dtRow["HF_DevelopedMarkets_Max"] = 0;
                        dtRow["HF_EmergingMarkets_Min"] = 0;
                        dtRow["HF_EmergingMarkets_Max"] = 0;

                        foreach (DataRow dtRow1 in dtAssetAllocations.Rows)
                        {
                            if (Convert.ToInt32(dtRow1["ID"]) == iIC_AA_ID && Convert.ToInt32(dtRow1["Tipos"]) == iIC_AA_Tipos && Convert.ToInt32(dtRow1["Profile_ID"]) == klsContract.Profile_ID)
                            {
                                switch (dtRow1["Title"])
                                {
                                    case "Fixed Income":
                                        dtRow["HF_FixedIncome_Min"] = dtRow1["MinValue"];
                                        dtRow["HF_FixedIncome_Max"] = dtRow1["MaxValue"];
                                        break;
                                    case "Equities":
                                        dtRow["HF_Equities_Min"] = dtRow1["MinValue"];
                                        dtRow["HF_Equities_Max"] = dtRow1["MaxValue"];
                                        break;
                                    case "Others":
                                        dtRow["HF_Others_Min"] = dtRow1["MinValue"];
                                        dtRow["HF_Others_Max"] = dtRow1["MaxValue"];
                                        break;
                                    case "Cash":
                                        dtRow["HF_Cash_Min"] = dtRow1["MinValue"];
                                        dtRow["HF_Cash_Max"] = dtRow1["MaxValue"];
                                        break;
                                }
                            }
                        }

                        //--- define Currenct data -------------------------------------------------------------------
                        dtRow["FixedIncome"] = 0;
                        dtRow["Equities"] = 0;
                        dtRow["Others"] = 0;
                        dtRow["Cash"] = 0;
                        dtRow["EUR"] = 0;
                        dtRow["USD_etc"] = 0;
                        dtRow["EmergingCurrencies"] = 0;
                        dtRow["DevelopedMarkets"] = 0;
                        dtRow["EmergingMarkets"] = 0;


                        if (iProduct_Group == 1) dtRow["FixedIncome"] = fltCurrentValue_EUR;
                        if (iProduct_Group == 2) dtRow["Equities"] = fltCurrentValue_EUR;
                        if (iProduct_Group == 3) dtRow["Others"] = fltCurrentValue_EUR;
                        if (iProduct_Group == 4) dtRow["Cash"] = fltCurrentValue_EUR;

                        if (iCountryGroup_ID == 1 || iCountryGroup_ID == 8 || iCountryGroup_ID == 9 || iCountryGroup_ID == 11) dtRow["DevelopedMarkets"] = fltCurrentValue_EUR;
                        if (iCountryGroup_ID == 2 || iCountryGroup_ID == 10 || iCountryGroup_ID == 12) dtRow["EmergingMarkets"] = fltCurrentValue_EUR;

                        if (sCurrency == "EUR") dtRow["EUR"] = fltCurrentValue_EUR;
                        else if (sCurrency == "USD" || sCurrency == "CHF" || sCurrency == "GBP" || sCurrency == "AUD" || sCurrency == "NZD" || sCurrency == "CAD" || sCurrency == "JPY") dtRow["USD_etc"] = fltCurrentValue_EUR;
                        else if (sCurrency == "RUB" || sCurrency == "HKD" || sCurrency == "BRL" || sCurrency == "INR" || sCurrency == "CNH" || sCurrency == "ZAR") dtRow["EmergingCurrencies"] = fltCurrentValue_EUR;

                        if (iCountryGroup_ID == 1 || iCountryGroup_ID == 8 || iCountryGroup_ID == 9 || iCountryGroup_ID == 11) dtRow["DevelopedMarkets"] = fltCurrentValue_EUR;
                        else dtRow["EmergingMarkets"] = fltCurrentValue_EUR;

                        dtContracts_Balances.Rows.Add(dtRow);
                    }
                    else
                    {
                        if (iProduct_ID != 7)
                        {
                            foundRows[0]["TotalSecurutiesValue"] = Convert.ToDecimal(foundRows[0]["TotalSecurutiesValue"]) + Convert.ToDecimal(fltCurrentValue_EUR);

                            if (iProduct_Group == 1) foundRows[0]["FixedIncome"] = Convert.ToSingle(foundRows[0]["FixedIncome"]) + fltCurrentValue_EUR;
                            if (iProduct_Group == 2) foundRows[0]["Equities"] = Convert.ToSingle(foundRows[0]["Equities"]) + fltCurrentValue_EUR;
                            if (iProduct_Group == 3) foundRows[0]["Others"] = Convert.ToSingle(foundRows[0]["Others"]) + fltCurrentValue_EUR;
                            if (iProduct_Group == 4) foundRows[0]["Cash"] = Convert.ToSingle(foundRows[0]["Cash"]) + fltCurrentValue_EUR;

                            if (iCountryGroup_ID == 1 || iCountryGroup_ID == 8 || iCountryGroup_ID == 9 || iCountryGroup_ID == 11) foundRows[0]["DevelopedMarkets"] = Convert.ToSingle(foundRows[0]["DevelopedMarkets"]) + fltCurrentValue_EUR;
                            if (iCountryGroup_ID == 2 || iCountryGroup_ID == 10 || iCountryGroup_ID == 12) foundRows[0]["EmergingMarkets"] = Convert.ToSingle(foundRows[0]["EmergingMarkets"]) + fltCurrentValue_EUR;
                        }
                        else
                        {
                            foundRows[0]["TotalCashValue"] = Convert.ToDecimal(foundRows[0]["TotalCashValue"]) + Convert.ToDecimal(fltCurrentValue_EUR);
                            foundRows[0]["Cash"] = Convert.ToSingle(foundRows[0]["Cash"]) + fltCurrentValue_EUR;

                            if (fltCurrentValue_EUR < 0) bDebitBalanceProblem = true;
                            if (bDebitBalanceProblem) foundRows[0]["DebitBalance"] = 0;
                        }

                        if (sCurrency == "EUR") foundRows[0]["EUR"] = Convert.ToSingle(foundRows[0]["EUR"]) + fltCurrentValue_EUR;
                        else if (sCurrency == "USD" || sCurrency == "CHF" || sCurrency == "GBP" || sCurrency == "AUD" || sCurrency == "NZD" || sCurrency == "CAD" || sCurrency == "JPY")
                            foundRows[0]["USD_etc"] = Convert.ToSingle(foundRows[0]["USD_etc"]) + fltCurrentValue_EUR;
                        else if (sCurrency == "RUB" || sCurrency == "HKD" || sCurrency == "BRL" || sCurrency == "INR" || sCurrency == "CNH" || sCurrency == "ZAR")
                            foundRows[0]["EmergingCurrencies"] = Convert.ToSingle(foundRows[0]["EmergingCurrencies"]) + fltCurrentValue_EUR;

                        if (iCountryGroup_ID == 1 || iCountryGroup_ID == 8 || iCountryGroup_ID == 9 || iCountryGroup_ID == 11) foundRows[0]["DevelopedMarkets"] = Convert.ToSingle(foundRows[0]["DevelopedMarkets"]) + fltCurrentValue_EUR;
                        else foundRows[0]["EmergingMarkets"] = Convert.ToSingle(foundRows[0]["EmergingMarkets"]) + fltCurrentValue_EUR;

                        if (bAssetAllocationProblem) foundRows[0]["AssetAllocation"] = 0;
                        if (bSpecialInstructionsProblem) foundRows[0]["SpecialInstructions"] = 0;
                        if (bSuitableProductsProblem) foundRows[0]["SuitableProducts"] = 0;
                        if (bLeverageProblem) foundRows[0]["Leverage"] = 0;
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
                Contracts_BalancesRecs.IC_AA_Recs_ID = Convert.ToInt32(dtViewRow["IC_AA_Recs_ID"]);
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

                Contracts_Balances.DebitBalance = Convert.ToInt16(dtRow1["DebitBalance"]);

                i = 1;
                if (Contracts_Balances.TotalValue != 0)
                {
                    fltTemp = Convert.ToSingle(dtRow1["FixedIncome"]) * 100 / Convert.ToSingle(Contracts_Balances.TotalValue);
                    if ((Convert.ToSingle(dtRow1["HF_FixedIncome_Min"]) > fltTemp) || (Convert.ToSingle(dtRow1["HF_FixedIncome_Max"]) < fltTemp)) i = 0;

                    fltTemp = Convert.ToSingle(dtRow1["Equities"]) * 100 / Convert.ToSingle(Contracts_Balances.TotalValue);
                    if ((Convert.ToSingle(dtRow1["HF_Equities_Min"]) > fltTemp) || (Convert.ToSingle(dtRow1["HF_Equities_Max"]) < fltTemp)) i = 0;

                    fltTemp = Convert.ToSingle(dtRow1["Others"]) * 100 / Convert.ToSingle(Contracts_Balances.TotalValue);
                    if ((Convert.ToSingle(dtRow1["HF_Others_Min"]) > fltTemp) || (Convert.ToSingle(dtRow1["HF_Others_Max"]) < fltTemp)) i = 0;

                    fltTemp = Convert.ToSingle(dtRow1["Cash"]) * 100 / Convert.ToSingle(Contracts_Balances.TotalValue);
                    if ((Convert.ToSingle(dtRow1["HF_Cash_Min"]) > fltTemp) || (Convert.ToSingle(dtRow1["HF_Cash_Max"]) < fltTemp)) i = 0;

                    fltTemp = Convert.ToSingle(dtRow1["EUR"]) * 100 / Convert.ToSingle(Contracts_Balances.TotalValue);
                    if ((Convert.ToSingle(dtRow1["HF_EUR_Min"]) > fltTemp) || (Convert.ToSingle(dtRow1["HF_EUR_Max"]) < fltTemp)) i = 0;

                    fltTemp = Convert.ToSingle(dtRow1["USD_etc"]) * 100 / Convert.ToSingle(Contracts_Balances.TotalValue);
                    if ((Convert.ToSingle(dtRow1["HF_USD_etc_Min"]) > fltTemp) || (Convert.ToSingle(dtRow1["HF_USD_etc_Max"]) < fltTemp)) i = 0;

                    fltTemp = Convert.ToSingle(dtRow1["EmergingCurrencies"]) * 100 / Convert.ToSingle(Contracts_Balances.TotalValue);
                    if ((Convert.ToSingle(dtRow1["HF_EmergingCurrencies_Min"]) > fltTemp) || (Convert.ToSingle(dtRow1["HF_EmergingCurrencies_Max"]) < fltTemp)) i = 0;
                }
                else i = 0;

                Contracts_Balances.AssetAllocation = i;

                Contracts_Balances.SpecialInstructions = 1;
                Contracts_Balances.SuitableProducts = 1;
                Contracts_Balances.Leverage = 1;
                Contracts_Balances.Notes = dtRow1["Notes"] + "";
                Contracts_Balances.InsertRecord();
            }

            DefineList();
            this.Cursor = Cursors.Default;
            panImport.Visible = false;
        }
        private void Check_Step1(int iCDP_ID, int iTipos, int iProfile_ID)
        {
            int i = 0, iContract_ID, iContracts_Details_ID, iContracts_Packages_ID;
            string sComplexProduct;

            dtDB.Clear();
            dtAA.Clear();
            dtSP.Clear();

            foreach (DataRow dtRow in dtAssetAllocations.Rows)
                if ((Convert.ToInt32(dtRow["Tipos"]) == iTipos) && ((Convert.ToInt32(dtRow["Profile_ID"]) == iProfile_ID)))
                {
                    i = i + 1;
                    dtRow1 = dtAA.NewRow();
                    dtRow1["AA"] = i;
                    dtRow1["ID"] = Convert.ToInt32(dtRow["Recs_ID"]);
                    dtRow1["Title"] = dtRow["Title"].ToString();
                    dtRow1["HF_Percent"] = dtRow["MainValue"] + " (" + dtRow["MinValue"] + " - " + dtRow["MaxValue"] + ")";
                    dtRow1["Current_Percent"] = 0;
                    dtRow1["Difference"] = 0;
                    dtRow1["MainValue"] = Convert.ToSingle(dtRow["MainValue"]);
                    dtRow1["MinValue"] = Convert.ToSingle(dtRow["MinValue"]);
                    dtRow1["MaxValue"] = Convert.ToSingle(dtRow["MaxValue"]);
                    dtRow1["Flag"] = 1;
                    dtAA.Rows.Add(dtRow1);
                }

            Contracts_Details_Packages = new clsContracts_Details_Packages();
            Contracts_Details_Packages.Record_ID = iCDP_ID;
            Contracts_Details_Packages.GetRecord();
            iContract_ID = Contracts_Details_Packages.Contract_ID;
            iContracts_Details_ID = Contracts_Details_Packages.Contracts_Details_ID;
            iContracts_Packages_ID = Contracts_Details_Packages.Contracts_Packages_ID;

            fltTemp = 0;
            Contracts_BalancesRecs = new clsContracts_BalancesRecs();
            Contracts_BalancesRecs.DateFrom = dDateControl.Value.Date;
            Contracts_BalancesRecs.DateTo = dDateControl.Value.Date;
            Contracts_BalancesRecs.CDP_ID = iCDP_ID;
            Contracts_BalancesRecs.GetList();
            foreach (DataRow dtRow in Contracts_BalancesRecs.List.Rows)
            {
                fltTemp = fltTemp + Convert.ToSingle(dtRow["CurrentValue_RepCcy"]);
                if (Convert.ToInt32(dtRow["Product_ID"]) == 7)
                {
                    dtRow1 = dtDB.NewRow();
                    dtRow1["Currency"] = dtRow["Product_Title"].ToString();
                    dtRow1["Balance"] = Convert.ToSingle(dtRow["TotalUnits"]);
                    dtDB.Rows.Add(dtRow1);
                }
     
                i = Convert.ToInt16(dtRow["Product_Group"]);
                if (i > 0)
                {
                    foundRows = dtAA.Select("AA = " + i);
                    if (foundRows.Length > 0)
                        foundRows[0]["Current_Percent"] = Convert.ToSingle(foundRows[0]["Current_Percent"]) + Convert.ToSingle(dtRow["Participation_PRC"]);
                }

                if (dtRow["Curr"] + "" == "EUR")
                {
                    foundRows = dtAA.Select("Title = 'EUR'");
                    if (foundRows.Length > 0) foundRows[0]["Current_Percent"] = Convert.ToSingle(foundRows[0]["Current_Percent"]) + Convert.ToSingle(dtRow["Participation_PRC"]);

                    foundRows = dtAA.Select("Title = 'Hard Currencies'");
                    if (foundRows.Length > 0) foundRows[0]["Current_Percent"] = Convert.ToSingle(foundRows[0]["Current_Percent"]) + Convert.ToSingle(dtRow["Participation_PRC"]);
                }

                if (dtRow["Curr"] + "" == "USD" || dtRow["Curr"] + "" == "CHF" || dtRow["Curr"] + "" == "GBP" || dtRow["Curr"] + "" == "AUD" || dtRow["Curr"] + "" == "NZD" || dtRow["Curr"] + "" == "CAD" || dtRow["Curr"] + "" == "JPY")
                {
                    foundRows = dtAA.Select("Title = 'USD_etc'");
                    if (foundRows.Length > 0) foundRows[0]["Current_Percent"] = Convert.ToSingle(foundRows[0]["Current_Percent"]) + Convert.ToSingle(dtRow["Participation_PRC"]);

                    foundRows = dtAA.Select("Title = 'Hard Currencies'");
                    if (foundRows.Length > 0) foundRows[0]["Current_Percent"] = Convert.ToSingle(foundRows[0]["Current_Percent"]) + Convert.ToSingle(dtRow["Participation_PRC"]);
                }
                if (dtRow["Curr"] + "" == "RUB" || dtRow["Curr"] + "" == "HKD" || dtRow["Curr"] + "" == "BRL" || dtRow["Curr"] + "" == "INR" || dtRow["Curr"] + "" == "CNH" || dtRow["Curr"] + "" == "ZAR")
                {
                    foundRows = dtAA.Select("Title = 'Emerging Currencies'");
                    if (foundRows.Length > 0) foundRows[0]["Current_Percent"] = Convert.ToSingle(foundRows[0]["Current_Percent"]) + Convert.ToSingle(dtRow["Participation_PRC"]);
                }

                iCountryGroup_ID = Convert.ToInt32(dtRow["CountryGroup_ID"]);
                if (iCountryGroup_ID == 1 || iCountryGroup_ID == 8 || iCountryGroup_ID == 9 || iCountryGroup_ID == 11)
                {
                    foundRows = dtAA.Select("Title = 'Developed Markets'");
                    if (foundRows.Length > 0) foundRows[0]["Current_Percent"] = Convert.ToSingle(foundRows[0]["Current_Percent"]) + Convert.ToSingle(dtRow["Participation_PRC"]);
                }
                else
                {
                    foundRows = dtAA.Select("Title = 'Emerging Markets'");
                    if (foundRows.Length > 0) foundRows[0]["Current_Percent"] = Convert.ToSingle(foundRows[0]["Current_Percent"]) + Convert.ToSingle(dtRow["Participation_PRC"]);
                }

                if (Convert.ToInt32(dtRow["Product_ID"]) != 7)
                {
                    ProductCode = new clsProductsCodes();
                    ProductCode.Record_ID = Convert.ToInt32(dtRow["ShareCodes_ID"]);
                    ProductCode.GetRecord();

                    stProduct = new Global.ProductData();
                    stProduct.ShareCode_ID = ProductCode.Record_ID;
                    stProduct.Product_ID = ProductCode.Product_ID;
                    stProduct.ProductCategory_ID = ProductCode.ProductCategory_ID;
                    stProduct.StockExchange_ID = ProductCode.StockExchange_ID;
                    stProduct.Title = ProductCode.CodeTitle;
                    stProduct.Code = ProductCode.Code;
                    stProduct.ISIN = ProductCode.ISIN;
                    stProduct.Currency = ProductCode.Curr;
                    stProduct.MIFID_Risk = ProductCode.MIFID_Risk;
                    stProduct.Retail = ProductCode.InvestType_Retail;
                    stProduct.Professional = ProductCode.InvestType_Prof;
                    stProduct.Distrib_ExecOnly = ProductCode.Distrib_ExecOnly;
                    stProduct.Distrib_Advice = ProductCode.Distrib_Advice;
                    stProduct.Distrib_PortfolioManagment = ProductCode.Distrib_PortfolioManagment;
                    stProduct.RiskCurr = ProductCode.RiskCurr;
                    stProduct.CurrencyHedge2 = ProductCode.CurrencyHedge2;
                    stProduct.ComplexProduct = ProductCode.ComplexProduct;
                    stProduct.ComplexReasonsList = ProductCode.ComplexReasonsList;
                    stProduct.Rank_Title = ProductCode.Rank_Title;
                    stProduct.IsCallable = ProductCode.IsCallable;
                    stProduct.IsPutable = ProductCode.IsPutable;
                    stProduct.IsConvertible = ProductCode.IsConvertible;
                    stProduct.IsPerpetualSecurity = ProductCode.IsPerpetualSecurity;
                    stProduct.ComplexAttribute = ProductCode.ComplexAttribute;
                    stProduct.Leverage = ProductCode.Leverage;
                    stProduct.MiFIDInstrumentType = ProductCode.MiFIDInstrumentType;
                    stProduct.AIFMD = ProductCode.AIFMD;
                    stProduct.GlobalBroadCategory_Title = ProductCode.Rank_Title;
                    stProduct.InvestGeography_ID = ProductCode.InvestGeography_ID;
                    stProduct.RatingGroup = ProductCode.RatingGroup;

                    //--- define Contract_ID ---------------------------------
                    klsContract = new clsContracts();
                    klsContract.Record_ID = iContract_ID;
                    klsContract.Contract_Details_ID = iContracts_Details_ID;
                    klsContract.Contract_Packages_ID = iContracts_Packages_ID;
                    klsContract.GetRecord();

                    stContract = new Global.ContractData();
                    stContract.Contract_ID = klsContract.Record_ID;
                    stContract.Contracts_Details_ID = klsContract.Contract_Details_ID;
                    stContract.Contracts_Packages_ID = klsContract.Contract_Packages_ID;

                    foundRows = Global.dtContracts.Select("Contract_ID = " + klsContract.Record_ID + " AND Client_ID = " + klsContract.Client_ID);
                    if (foundRows.Length > 0)
                    {
                        stContract.MIFIDCategory_ID = Convert.ToInt32(foundRows[0]["MIFIDCategory_ID"]);
                        stContract.MIFID_Risk_Index = Convert.ToInt32(foundRows[0]["MIFID_Risk_Index"]);
                    }

                    sComplexProduct = "";
                    if (klsContract.Details.ChkComplex == 1)
                    {
                        clsContracts_ComplexSigns klsContracts_ComplexSigns = new clsContracts_ComplexSigns();
                        klsContracts_ComplexSigns.Contract_ID = klsContract.Record_ID;
                        klsContracts_ComplexSigns.GetList();
                        foreach (DataRow dtRow1 in klsContracts_ComplexSigns.List.Rows)
                            sComplexProduct = sComplexProduct + "," + dtRow1["ComplexSign_ID"];

                        if (sComplexProduct.Length > 0) sComplexProduct = sComplexProduct + ",";
                    }
                    stContract.ComplexProduct = sComplexProduct;

                    stContract.Geography = (klsContract.Details.ChkWorld == 1 ? "1" : "0") + (klsContract.Details.ChkGreece == 1 ? "1" : "0") + (klsContract.Details.ChkEurope == 1 ? "1" : "0") +
                    (klsContract.Details.ChkAmerica == 1 ? "1" : "0") + (klsContract.Details.ChkAsia == 1 ? "1" : "0");

                    stContract.SpecRules = (klsContract.Details.ChkSpecificConstraints == 1 ? "1" : "0") + (klsContract.Details.ChkMonetaryRisk == 1 ? "1" : "0") + (klsContract.Details.ChkIndividualBonds == 1 ? "1" : "0") +
                             (klsContract.Details.ChkMutualFunds == 1 ? "1" : "0") + (klsContract.Details.ChkBondedETFs == 1 ? "1" : "0") + (klsContract.Details.ChkIndividualShares == 1 ? "1" : "0") +
                             (klsContract.Details.ChkMixedFunds == 1 ? "1" : "0") + (klsContract.Details.ChkMixedETFs == 1 ? "1" : "0") + (klsContract.Details.ChkFunds == 1 ? "1" : "0") +
                             (klsContract.Details.ChkETFs == 1 ? "1" : "0") + (klsContract.Details.ChkInvestmentGrade == 1 ? "1" : "0");


                    if (!Global.AccordanceContractProduct(stContract, stProduct, out int iOK_Flag, out string sOK_String))
                    {
                        dtRow1 = dtSP.NewRow();
                        dtRow1["ID"] = Convert.ToInt32(dtRow["ID"]);
                        dtRow1["Title"] = dtRow["ShareCodes_Title"].ToString();
                        dtRow1["ISIN"] = dtRow["ISIN"].ToString();
                        dtRow1["Category_SubCategory"] = dtRow["Product_Title"] + "/" + dtRow["ProductCategory_Title"];
                        dtSP.Rows.Add(dtRow1);
                    }
                }
            }

            foreach (DataRow dtRow in dtAA.Rows)
            {
                dtRow["Current_Percent"] = Convert.ToSingle(dtRow["Current_Percent"]).ToString("0.##");
                dtRow["Difference"] = (Convert.ToSingle(dtRow["MainValue"]) - Convert.ToSingle(dtRow["Current_Percent"])).ToString("0.##");
                dtRow["Flag"] = (Convert.ToSingle(dtRow["Current_Percent"]) >= Convert.ToSingle(dtRow["MinValue"]) &&
                                                Convert.ToSingle(dtRow["Current_Percent"]) <= Convert.ToSingle(dtRow["MaxValue"])) ? "1" : "-1";
            }
        }
        private string Check_Step2(int iContract_ID, int iContracts_Details_ID)
        {
            string sTemp = "";

            dtSI.Clear();

            klsContractDetails = new clsContracts_Details();
            klsContractDetails.Contract_ID = 0;
            klsContractDetails.Record_ID = iContracts_Details_ID;
            klsContractDetails.GetRecord();

            if (klsContractDetails.ChkComplex == 1)
            {
                sTemp = "";
                Contract_ComplexSigns = new clsContracts_ComplexSigns();
                Contract_ComplexSigns.Contract_ID = iContract_ID;
                Contract_ComplexSigns.GetList();

                foreach (DataRow dtRow in Contract_ComplexSigns.List.Rows)
                    sTemp = sTemp + dtRow["ComplexSign_Title"] + " / ";
            }
            else sTemp = "No";

            dtRow1 = dtSI.NewRow();
            dtRow1["Title"] = "Πολυπλοκότητα ΧΜ";
            dtRow1["Contract_Data"] = sTemp;
            dtRow1["Current_Data"] = "";
            dtRow1["Flag"] = 1;
            dtSI.Rows.Add(dtRow1);

            sTemp = "";
            if (klsContractDetails.ChkWorld == 1) sTemp = "World";
            if (klsContractDetails.ChkGreece == 1) sTemp = (sTemp + " Greece").Trim();
            if (klsContractDetails.ChkEurope == 1) sTemp = (sTemp + " Europe").Trim();
            if (klsContractDetails.ChkAmerica == 1) sTemp = (sTemp + " America").Trim();
            if (klsContractDetails.ChkAsia == 1) sTemp = (sTemp + " Asia").Trim();
            dtRow1 = dtSI.NewRow();
            dtRow1["Title"] = "Γεωγραφική Κατανομή Επενδύσεων";
            dtRow1["Contract_Data"] = sTemp;
            dtRow1["Current_Data"] = "";
            dtRow1["Flag"] = 1;
            dtSI.Rows.Add(dtRow1);

            sTemp = (klsContractDetails.IncomeProducts + " / " + klsContractDetails.CapitalProducts).Trim();
            dtRow1 = dtSI.NewRow();
            dtRow1["Title"] = "Επιθυμητή Κατανομή Κεφαλαίων";
            dtRow1["Contract_Data"] = sTemp;
            dtRow1["Current_Data"] = "";
            dtRow1["Flag"] = 1;
            dtSI.Rows.Add(dtRow1);

            if (klsContractDetails.ChkMonetaryRisk == 1)
            {
                dtRow1 = dtSI.NewRow();
                dtRow1["Title"] = "Νομισματικό κίνδυνο";
                dtRow1["Contract_Data"] = "No";
                dtRow1["Current_Data"] = "";
                dtRow1["Flag"] = 1;
                dtSI.Rows.Add(dtRow1);
            }

            if (klsContractDetails.ChkIndividualBonds == 1)
            {
                dtRow1 = dtSI.NewRow();
                dtRow1["Title"] = "Μεμονωμένα ομόλογα";
                dtRow1["Contract_Data"] = "No";
                dtRow1["Current_Data"] = "";
                dtRow1["Flag"] = 1;
                dtSI.Rows.Add(dtRow1);
            }
            if (klsContractDetails.ChkMutualFunds == 1)
            {
                dtRow1 = dtSI.NewRow();
                dtRow1["Title"] = "Ομολογιακά ΑΚ";
                dtRow1["Contract_Data"] = "No";
                dtRow1["Current_Data"] = "";
                dtRow1["Flag"] = 1;
                dtSI.Rows.Add(dtRow1);
            }
            if (klsContractDetails.ChkBondedETFs == 1)
            {
                dtRow1 = dtSI.NewRow();
                dtRow1["Title"] = "Ομολογιακά ΔΑΚ";
                dtRow1["Contract_Data"] = "No";
                dtRow1["Current_Data"] = "";
                dtRow1["Flag"] = 1;
                dtSI.Rows.Add(dtRow1);
            }
            if (klsContractDetails.ChkIndividualShares == 1)
            {
                dtRow1 = dtSI.NewRow();
                dtRow1["Title"] = "Μεμονωμένες Μετοχές";
                dtRow1["Contract_Data"] = "No";
                dtRow1["Current_Data"] = "";
                dtRow1["Flag"] = 1;
                dtSI.Rows.Add(dtRow1);
            }
            if (klsContractDetails.ChkMixedFunds == 1)
            {
                dtRow1 = dtSI.NewRow();
                dtRow1["Title"] = "Μετοχικά και Μεικτά ΑΚ";
                dtRow1["Contract_Data"] = "No";
                dtRow1["Current_Data"] = "";
                dtRow1["Flag"] = 1;
                dtSI.Rows.Add(dtRow1);
            }
            if (klsContractDetails.ChkMixedETFs == 1)
            {
                dtRow1 = dtSI.NewRow();
                dtRow1["Title"] = "Μετοχικά και Μεικτά ΔΑΚ";
                dtRow1["Contract_Data"] = "No";
                dtRow1["Current_Data"] = "";
                dtRow1["Flag"] = 1;
                dtSI.Rows.Add(dtRow1);
            }
            if (klsContractDetails.ChkFunds == 1)
            {
                dtRow1 = dtSI.NewRow();
                dtRow1["Title"] = "ΑΚ";
                dtRow1["Contract_Data"] = "No";
                dtRow1["Current_Data"] = "";
                dtRow1["Flag"] = 1;
                dtSI.Rows.Add(dtRow1);
            }
            if (klsContractDetails.ChkETFs == 1)
            {
                dtRow1 = dtSI.NewRow();
                dtRow1["Title"] = "ΔΑΚ";
                dtRow1["Contract_Data"] = "No";
                dtRow1["Current_Data"] = "";
                dtRow1["Flag"] = 1;
                dtSI.Rows.Add(dtRow1);
            }
            if (klsContractDetails.ChkInvestmentGrade == 1)
            {
                dtRow1 = dtSI.NewRow();
                dtRow1["Title"] = "Μόνο Investment Grade";
                dtRow1["Contract_Data"] = "No";
                dtRow1["Current_Data"] = "";
                dtRow1["Flag"] = 1;
                dtSI.Rows.Add(dtRow1);
            }

            return klsContractDetails.MiscInstructions.Trim();
        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }

    }
}
