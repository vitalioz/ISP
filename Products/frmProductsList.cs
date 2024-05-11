using System;
using System.Data;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing;
using Core;

namespace Products
{
    public partial class frmProductsList : Form
    {
        int i, j, m, iRightsLevel, iOld_ID, iLastAktion, iProduct_ID, iFoundChoicedList, iLogs, iShare_ID, iShareTitle_ID, iShareCode_ID,
            iProductCategory_ID, iHFCategory_ID, iCountry_ID, iSector_ID, iCountryRisk_ID, iSE_ID, iCountryAction_ID, iPrimaryShare, iCountriesGroup_ID,
            iRatingGroup, iComplexProduct, iComplexReason_ID, iInstrumentType;
        float fltQuantityMin;
        string sTemp, sExtra, sStockExchanges, sSQL, sCurrency, sRiskCurrency, sProviderName, sDescriptionEn, sDateIncorporation, sMarketCapitalizationCurr, sOldRiskProfile;
        string[] tmpArray;
        Decimal decMarketCapitalization;
        bool bCheckList, bFound, bError;
        DateTime dIPO;
        DataView dtView;
        DataRow[] foundRows;
        clsProducts klsProducts = new clsProducts();
        clsProductsTitles klsProductsTitles = new clsProductsTitles();

        private void tsbExcel_Log_Click(object sender, EventArgs e)
        {
            var EXL = new Microsoft.Office.Interop.Excel.Application();
            var oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            // System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("el-GR");
            EXL.Workbooks.Add();
            EXL.ScreenUpdating = false;
            EXL.Caption = Global.AppTitle;
            m = 2;
            EXL.Cells[m, 1].Value = "AA";
            EXL.Cells[m, 1].Value = "Message";
            for (this.i = 0; this.i <= fgWarnings.Rows.Count - 1; this.i++)
            {
                EXL.Cells[m + i, 1].Value = fgWarnings[i, 0];
                EXL.Cells[m + i, 2].Value = fgWarnings[i, 1];
            }

            EXL.ScreenUpdating = true;
            EXL.Visible = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;

            this.Cursor = Cursors.Default;
        }

        clsProductsCodes klsProductsCodes = new clsProductsCodes();
        clsProductsLogger ProductsLogger = new clsProductsLogger();
        clsSystem Systems = new clsSystem();
        public frmProductsList()
        {
            InitializeComponent();

            ucShares.Visible = false;
            ucShares.Width = 1324;
            ucShares.Height = 720;
            ucShares.Left = 386;
            ucShares.Top = 4;

            ucBonds.Visible = false;
            ucBonds.Width = 1324;
            ucBonds.Height = 720;
            ucBonds.Left = 386;
            ucBonds.Top = 4;

            ucETFs.Visible = false;
            ucETFs.Width = 1324;
            ucETFs.Height = 720;
            ucETFs.Left = 386;
            ucETFs.Top = 4;

            ucFunds.Visible = false;
            ucFunds.Width = 1324;
            ucFunds.Height = 720;
            ucFunds.Left = 386;
            ucFunds.Top = 4;

            ucRates.Visible = false;
            ucRates.Width = 1324;
            ucRates.Height = 720;
            ucRates.Left = 386;
            ucRates.Top = 4;

            ucIndexes.Visible = false;
            ucIndexes.Width = 1324;
            ucIndexes.Height = 720;
            ucIndexes.Left = 386;
            ucIndexes.Top = 4;

            ucShares.lblFlagEdit.TextChanged += close_me;
            ucBonds.lblFlagEdit.TextChanged += close_me;
            ucETFs.lblFlagEdit.TextChanged += close_me;
            ucFunds.lblFlagEdit.TextChanged += close_me;
            ucRates.lblFlagEdit.TextChanged += close_me;
            ucIndexes.lblFlagEdit.TextChanged += close_me;

            panFilters.Left = 8;
            panFilters.Top = 40;

            panAddProduct.Left = 8;
            panAddProduct.Top = 40;

            panMerge.Left = 382;
            panMerge.Top = 40;

            panChangeGroup.Left = 382;
            panChangeGroup.Top = 40;
        }

        private void frmProductsList_Load(object sender, EventArgs e)
        {
            //-------------- Define Products List ------------------
            cmbProductType.DataSource = Global.dtProductTypes.Copy();
            cmbProductType.DisplayMember = "Title";
            cmbProductType.ValueMember = "ID";

            //-------------- Define Products List ------------------
            cmbProducts.DataSource = Global.dtProductTypes.Copy();
            cmbProducts.DisplayMember = "Title";
            cmbProducts.ValueMember = "ID";

            //-------------- Define Products List ------------------
            cmbProduct.DataSource = Global.dtProductTypes.Copy();
            cmbProduct.DisplayMember = "Title";
            cmbProduct.ValueMember = "ID";

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString(Global.GridStyle);
            fgList.RowColChange += new EventHandler(fgList_RowColChange);

            sStockExchanges = "0";
            tscbProductTypes.SelectedIndex = 0;

        }
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            DataFiltering(0);
        }

        private void tsbFilter_Click(object sender, EventArgs e)
        {
            panFilters.Visible = true;
        }

        private void tsbExcel_Click(object sender, EventArgs e)
        {
            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            int m = 0; ;

            var EXL = new Microsoft.Office.Interop.Excel.Application();
            var oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            // System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("el-GR");
            EXL.Workbooks.Add();
            EXL.ScreenUpdating = false;
            EXL.Caption = Global.AppTitle;

            iOld_ID = -999;

            m = 1;
            EXL.Cells[m, 2].Value = "Προϊόντα";

            m = 2;

            switch (tscbProductTypes.SelectedIndex)
            {
                case 1:
                    EXL.Cells[m, 1].Value = "AA";
                    EXL.Cells[m, 2].Value = "Όνομα προϊόντος";
                    EXL.Cells[m, 3].Value = "Κατηγορία Προμήθειας";
                    EXL.Cells[m, 4].Value = "Κατηγορία διαχείρισης HF";
                    EXL.Cells[m, 5].Value = "ISIN";
                    EXL.Cells[m, 6].Value = "SecID";
                    EXL.Cells[m, 7].Value = "Reuters Κωδικός";
                    EXL.Cells[m, 8].Value = "Bloomberg Κωδικός";
                    EXL.Cells[m, 9].Value = "MorningStar Κωδικός";
                    EXL.Cells[m, 10].Value = "Χρηματιστήριο Διαπραγμάτευσης";
                    EXL.Cells[m, 11].Value = "Χώρα Διαπραγμάτευσης";
                    EXL.Cells[m, 12].Value = "Νόμισμα";
                    EXL.Cells[m, 13].Value = "Χώρα έδρας";
                    EXL.Cells[m, 14].Value = "Countries group";
                    EXL.Cells[m, 15].Value = "Sector";
                    EXL.Cells[m, 16].Value = "Country of Risk";
                    EXL.Cells[m, 17].Value = "Primary Share";
                    break;
                case 2:
                    EXL.Cells[m, 1].Value = "AA";
                    EXL.Cells[m, 2].Value = "Όνομα προϊόντος";
                    EXL.Cells[m, 3].Value = "Κατηγορία Προμήθειας";
                    EXL.Cells[m, 4].Value = "Κατηγορία διαχείρισης HF";
                    EXL.Cells[m, 5].Value = "ISIN";
                    EXL.Cells[m, 6].Value = "Reuters Κωδικός";
                    EXL.Cells[m, 7].Value = "Bloomberg Κωδικός";
                    EXL.Cells[m, 8].Value = "MorningStar Κωδικός";
                    EXL.Cells[m, 9].Value = "Χρηματιστήριο Διαπραγμάτευσης";
                    EXL.Cells[m, 10].Value = "Χώρα Διαπραγμάτευσης";
                    EXL.Cells[m, 11].Value = "Νόμισμα";
                    EXL.Cells[m, 12].Value = "Χώρα έδρας";
                    EXL.Cells[m, 13].Value = "Countries group";
                    EXL.Cells[m, 14].Value = "Sector";
                    EXL.Cells[m, 15].Value = "Country of Risk";
                    EXL.Cells[m, 16].Value = "Τύπος ομολόγου";
                    EXL.Cells[m, 17].Value = "Ημερ.Έκδοσης";
                    EXL.Cells[m, 18].Value = "Ημερ.Λήξης";
                    EXL.Cells[m, 19].Value = "Ημερ. 1ου Διακαν/μού";
                    EXL.Cells[m, 20].Value = "Ημερ.Αποκοπής 1ου Κουπονιού";
                    EXL.Cells[m, 21].Value = "Είδος Κουπονιού";
                    EXL.Cells[m, 22].Value = "Κουπόνι";
                    EXL.Cells[m, 23].Value = "Τρέχον Κουπόνι";
                    EXL.Cells[m, 24].Value = "Τιμή Έκδοσης";
                    EXL.Cells[m, 25].Value = "Συχνότητα Αποκοπής Κουπονιού";
                    EXL.Cells[m, 26].Value = "Δικαίωμα Ανάκλησης";
                    EXL.Cells[m, 27].Value = "Ελάχιστη Ποσότητα Διαπραγμάτευσης";
                    EXL.Cells[m, 28].Value = "Βήμα Διαπραγμάτευσης";
                    EXL.Cells[m, 29].Value = "Καλυμένη Ομολογία";
                    EXL.Cells[m, 30].Value = "Rank";
                    EXL.Cells[m, 31].Value = "Μεταβαλλόμενο Επιτόκιο";
                    EXL.Cells[m, 32].Value = "Περιθώριο";
                    EXL.Cells[m, 33].Value = "Πιστοληπτική Αξιολόγηση";
                    EXL.Cells[m, 34].Value = "Investor Type - Retail";
                    EXL.Cells[m, 35].Value = "Investor Type - Professional";
                    EXL.Cells[m, 36].Value = "Distribution - Execution Only";
                    EXL.Cells[m, 37].Value = "Distribution - Investment Advice";
                    EXL.Cells[m, 38].Value = "Distribution - Portfolio Management";
                    EXL.Cells[m, 39].Value = "Complex Product";
                    break;
                case 4:
                case 6:
                    EXL.Cells[m, 1].Value = "AA";
                    EXL.Cells[m, 2].Value = "Όνομα προϊόντος";
                    EXL.Cells[m, 3].Value = "Κατηγορία Προμήθειας";
                    EXL.Cells[m, 4].Value = "Κατηγορία διαχείρισης HF";
                    EXL.Cells[m, 5].Value = "ISIN";
                    EXL.Cells[m, 6].Value = "SecID";
                    EXL.Cells[m, 7].Value = "Reuters Κωδικός";
                    EXL.Cells[m, 8].Value = "Bloomberg Κωδικός";
                    EXL.Cells[m, 9].Value = "MorningStar Κωδικός";
                    EXL.Cells[m, 10].Value = "Χρηματιστήριο Διαπραγμάτευσης";
                    EXL.Cells[m, 11].Value = "Χώρα Διαπραγμάτευσης";
                    EXL.Cells[m, 12].Value = "Νόμισμα";
                    EXL.Cells[m, 13].Value = "Χώρα έδρας";
                    EXL.Cells[m, 14].Value = "Countries group";
                    EXL.Cells[m, 15].Value = "Sector";
                    EXL.Cells[m, 16].Value = "Investment Area";
                    EXL.Cells[m, 17].Value = "Primary Share";
                    EXL.Cells[m, 18].Value = "Κατηγορία Morning Star";
                    EXL.Cells[m, 19].Value = "Fund Legal Structure";
                    EXL.Cells[m, 20].Value = "Benchmark";
                    EXL.Cells[m, 21].Value = "Leverage";
                    EXL.Cells[m, 22].Value = "Provider Name";
                    EXL.Cells[m, 23].Value = "Currency Hedge Indicator";
                    EXL.Cells[m, 24].Value = "Currency Hedge 2";
                    EXL.Cells[m, 25].Value = "Distribution Status";
                    EXL.Cells[m, 26].Value = "Dividend Distribution Frequency";
                    break;
            }
            klsProductsCodes = new clsProductsCodes();
            klsProductsCodes.Product_ID = tscbProductTypes.SelectedIndex;
            klsProductsCodes.GetList_ProductType();
            foreach (DataRow dtRow in klsProductsCodes.List.Rows)
            {

                sTemp = dtRow["Share_ID"] + "";
                i = fgList.FindRow(sTemp, 1, 1, false);
                if (i > 0) {
                    if (Convert.ToInt32(dtRow["ID"]) != iOld_ID) iOld_ID = Convert.ToInt32(dtRow["ID"]);
                    m = m + 1;
                    EXL.Cells[m, 1].Value = m - 2;
                }

                switch (tscbProductTypes.SelectedIndex)
                {
                    case 1:
                        EXL.Cells[m, 2].Value = dtRow["Title"];
                        EXL.Cells[m, 3].Value = dtRow["ProductCategories_Title"];
                        EXL.Cells[m, 4].Value = dtRow["HFCategory_Title"];
                        EXL.Cells[m, 5].Value = dtRow["ISIN"];
                        EXL.Cells[m, 6].Value = dtRow["SecID"];
                        EXL.Cells[m, 7].Value = dtRow["Code"];
                        EXL.Cells[m, 8].Value = dtRow["Code2"];
                        EXL.Cells[m, 9].Value = dtRow["Code3"];
                        EXL.Cells[m, 10].Value = dtRow["StockExchange_Code"];
                        EXL.Cells[m, 11].Value = dtRow["CountryAction_Title"];
                        EXL.Cells[m, 12].Value = dtRow["Currency"];
                        EXL.Cells[m, 13].Value = dtRow["CountryAction_Title"];
                        EXL.Cells[m, 14].Value = dtRow["CountriesGroups_Title"];
                        EXL.Cells[m, 15].Value = dtRow["SectorTitle"];                   //dtRow["Sector1Title"]; + " / " + dtRow["SectorTitle"];
                        EXL.Cells[m, 16].Value = dtRow["InvestArea_Title"];
                        EXL.Cells[m, 17].Value = (Convert.ToInt32(dtRow["PrimaryShare"]) == 2 ? "YES" : (Convert.ToInt32(dtRow["PrimaryShare"]) == 1 ? "NO" : ""));
                        break;
                    case 2:
                        EXL.Cells[m, 2].Value = dtRow["Title"];
                        EXL.Cells[m, 3].Value = dtRow["ProductCategories_Title"];
                        EXL.Cells[m, 4].Value = dtRow["HFCategory_Title"];
                        EXL.Cells[m, 5].Value = dtRow["ISIN"];
                        EXL.Cells[m, 6].Value = dtRow["Code"];
                        EXL.Cells[m, 7].Value = dtRow["Code2"];
                        EXL.Cells[m, 8].Value = dtRow["Code3"];
                        EXL.Cells[m, 9].Value = dtRow["StockExchange_Code"];
                        EXL.Cells[m, 10].Value = dtRow["CountryAction_Title"];
                        EXL.Cells[m, 11].Value = dtRow["Currency"];
                        EXL.Cells[m, 12].Value = dtRow["CountryAction_Title"];
                        EXL.Cells[m, 13].Value = dtRow["CountriesGroups_Title"];
                        EXL.Cells[m, 14].Value = dtRow["SectorTitle"];                              //dtRow["Sector1Title"]; + " / " + dtRow["SectorTitle"];
                        EXL.Cells[m, 15].Value = dtRow["InvestArea_Title"];
                        EXL.Cells[m, 16].Value = Convert.ToInt32(dtRow["LegalStructure_ID"]) == 1 ? "Εταιρικό" : "Κρατικό";
                        EXL.Cells[m, 17].Value = dtRow["Date1"];
                        EXL.Cells[m, 18].Value = dtRow["Date2"];
                        EXL.Cells[m, 19].Value = dtRow["Date3"];
                        EXL.Cells[m, 20].Value = dtRow["Date4"];
                        EXL.Cells[m, 21].Value = dtRow["CouponeType_Title"];
                        EXL.Cells[m, 22].Value = dtRow["Coupone"];
                        EXL.Cells[m, 23].Value = Convert.ToInt32(dtRow["LastCoupone"]) == -1 ? "N/A" : dtRow["LastCoupone"];
                        EXL.Cells[m, 24].Value = dtRow["Price"];
                        EXL.Cells[m, 25].Value = dtRow["FrequencyClipping"];
                        EXL.Cells[m, 26].Value = dtRow["RevocationRights_Title"];
                        EXL.Cells[m, 27].Value = dtRow["QuantityMin"];
                        EXL.Cells[m, 28].Value = dtRow["QuantityStep"];
                        EXL.Cells[m, 29].Value = Convert.ToInt32(dtRow["CoveredBond"]) == 1 ? "Yes" : "No";
                        EXL.Cells[m, 30].Value = dtRow["Ranks_Title"];
                        EXL.Cells[m, 31].Value = dtRow["FloatingRate"];
                        EXL.Cells[m, 32].Value = Convert.ToInt32(dtRow["Limits"]) == -1 ? "N/A" : dtRow["Limits"];
                        EXL.Cells[m, 33].Value = dtRow["CreditRating"];
                        EXL.Cells[m, 34].Value = Convert.ToInt32(dtRow["InvestType_Retail"]) == 1 ? "NO" : Convert.ToInt32(dtRow["InvestType_Retail"]) == 2 ? "YES" : "-";
                        EXL.Cells[m, 35].Value = Convert.ToInt32(dtRow["InvestType_Prof"]) == 1 ? "NO" : Convert.ToInt32(dtRow["InvestType_Prof"]) == 2 ? "YES" : "-";

                        foundRows = Global.dtTargetMarketList2.Select("ID = " + dtRow["Distrib_ExecOnly"]);
                        if (foundRows.Length > 0) EXL.Cells[m, 36].Value = foundRows[0]["Title"];

                        foundRows = Global.dtTargetMarketList2.Select("ID = " + dtRow["Distrib_Advice"]);
                        if (foundRows.Length > 0) EXL.Cells[m, 37].Value = foundRows[0]["Title"];

                        foundRows = Global.dtTargetMarketList2.Select("ID = " + dtRow["Distrib_PortfolioManagment"]);
                        if (foundRows.Length > 0) EXL.Cells[m, 38].Value = foundRows[0]["Title"];

                        EXL.Cells[m, 39].Value = Convert.ToInt32(dtRow["ComplexProduct"]) == 1 ? "NO" : Convert.ToInt32(dtRow["ComplexProduct"]) == 2 ? "YES" : "-";
                        break;
                    case 4:
                    case 6:
                        EXL.Cells[m, 2].Value = dtRow["Title"];
                        EXL.Cells[m, 3].Value = dtRow["ProductCategories_Title"];
                        EXL.Cells[m, 4].Value = dtRow["HFCategory_Title"];
                        EXL.Cells[m, 5].Value = dtRow["ISIN"];
                        EXL.Cells[m, 6].Value = dtRow["SecID"];
                        EXL.Cells[m, 7].Value = dtRow["Code"];
                        EXL.Cells[m, 8].Value = dtRow["Code2"];
                        EXL.Cells[m, 9].Value = dtRow["Code3"];
                        EXL.Cells[m, 10].Value = dtRow["StockExchange_Code"];
                        EXL.Cells[m, 11].Value = dtRow["CountryAction_Title"];
                        EXL.Cells[m, 12].Value = dtRow["Currency"];
                        EXL.Cells[m, 13].Value = dtRow["CountryAction_Title"];
                        EXL.Cells[m, 14].Value = dtRow["CountriesGroups_Title"];
                        EXL.Cells[m, 15].Value = dtRow["SectorTitle"];                                 //dtRow["Sector1Title"]; + " / " + dtRow["SectorTitle"];
                        EXL.Cells[m, 16].Value = dtRow["InvestArea_Title"];
                        EXL.Cells[m, 17].Value = (Convert.ToInt32(dtRow["PrimaryShare"]) == 2 ? "YES" : (Convert.ToInt32(dtRow["PrimaryShare"]) == 1 ? "NO" : ""));
                        EXL.Cells[m, 18].Value = dtRow["FundCategoriesMorningStar_Title"];
                        EXL.Cells[m, 19].Value = dtRow["FundLegalStructures_Title"];
                        EXL.Cells[m, 20].Value = dtRow["Benchmarks_Title"];
                        EXL.Cells[m, 21].Value = (Convert.ToInt32(dtRow["Leverage"]) == 1 ? "Yes" : "No");
                        EXL.Cells[m, 22].Value = dtRow["ProviderName"];
                        EXL.Cells[m, 23].Value = dtRow["CurrencyHedge"];
                        EXL.Cells[m, 24].Value = dtRow["CurrencyHedge2"];
                        EXL.Cells[m, 25].Value = dtRow["DistributionStatus"];
                        EXL.Cells[m, 26].Value = dtRow["FrequencyClipping"];
                        break;
                }
            }


            EXL.ScreenUpdating = true;
            EXL.Visible = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;

            this.Cursor = Cursors.Default;
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            if (tscbProductTypes.SelectedIndex > 0) cmbProduct.SelectedValue = tscbProductTypes.SelectedIndex;
            else
                cmbProduct.SelectedValue = 1;

            panAddProduct.Visible = true;
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            switch (Convert.ToInt32(fgList[fgList.Row, "Product_ID"])) {
                case 1:
                    ucShares.EditRecord();
                    break;
                case 2:
                    ucBonds.EditRecord();
                    break;
                case 3:
                    ucRates.EditRecord();
                    break;
                case 4:
                    ucETFs.EditRecord();
                    break;
                case 5:
                    ucIndexes.EditRecord();
                    break;
                case 6:
                    ucFunds.EditRecord();
                    break;
            }
        }

        private void btnAdd_OK_Click(object sender, EventArgs e)
        {
            panAddProduct.Visible = false;

            frmProductData locProductData = new frmProductData();
            locProductData.Product_ID = Convert.ToInt32(cmbProduct.SelectedValue);
            locProductData.ShareCode_ID = 0;
            locProductData.Text = Global.GetLabel("new_product");
            locProductData.ShowDialog();
            if (locProductData.LastAktion > 0)
                DataFiltering(locProductData.LastAktion);
        }

        private void tsbShowExtraCommands_Click(object sender, EventArgs e)
        {
            cmbProductType.Text = tscbProductTypes.Text;
            cmbMorfi.SelectedIndex = 0;
            cmbFilter.SelectedIndex = 0;
            panExtraCommands.Visible = true;
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {

            if (fgList.Row > 0) {
                klsProducts = new clsProducts();
                klsProducts.Record_ID = Convert.ToInt32(fgList[fgList.Row, 1]);
                klsProducts.GetUsing();
                if (klsProducts.Aktive == 0) {
                    if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes) {
                        klsProducts = new clsProducts();
                        klsProducts.Record_ID = Convert.ToInt32(fgList[fgList.Row, 1]);
                        klsProducts.DeleteRecord();
                        Global.GetProductsList();
                        DataFiltering(0);
                        MessageBox.Show("Το προϊόν διαγράφτηκε οριστικά", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else MessageBox.Show("Το προϊόν αυτό δεν μπορεί να διαγραφεί γιατί είδη χρησιμοποιείτε", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            fgList.Height = this.Height - 88;

            ucShares.Width = this.Width - 402;
            ucShares.Height = this.Height - 52;

            ucBonds.Width = this.Width - 402;
            ucBonds.Height = this.Height - 52;

            ucETFs.Width = this.Width - 402;
            ucETFs.Height = this.Height - 52;

            ucFunds.Width = this.Width - 402;
            ucFunds.Height = this.Height - 52;

            ucIndexes.Width = this.Width - 402;
            ucIndexes.Height = this.Height - 52;
        }

        private void fgList_RowColChange(object sender, EventArgs e)
        {
            if (bCheckList) {
                if (fgList.Row > 0) {

                    this.Text = "Προϊόντα (" + fgList[fgList.Row, 1] + "/" + fgList[fgList.Row, 2] + "/" + fgList[fgList.Row, 3] + ")";
                    iProduct_ID = Convert.ToInt16(fgList[fgList.Row, "Product_ID"]);

                    switch (iProduct_ID)
                    {
                        case 1:
                            ucShares.Mode = 1;           // 1 - from ProductsList, 2 - from ProductsData, 3 - from ProductsWishList                                                        
                            ucShares.ShowRecord(Convert.ToInt32(fgList[fgList.Row, 1]), Convert.ToInt32(fgList[fgList.Row, 3]), 0, iRightsLevel);     // fgList(fgList.Row, 1) - Share_ID,  iRightsLevel
                            ucShares.Visible = true;
                            ucBonds.Visible = false;
                            ucETFs.Visible = false;
                            ucFunds.Visible = false;
                            ucRates.Visible = false;
                            ucIndexes.Visible = false;
                            break;
                        case 2:
                            ucBonds.Mode = 1;           // 1 - from ProductsList, 2 - from ProductsData, 3 - from ProductsWishList
                            ucBonds.ShowRecord(Convert.ToInt32(fgList[fgList.Row, 1]), Convert.ToInt32(fgList[fgList.Row, 3]), 0, iRightsLevel);     // fgList(fgList.Row, 1) - Share_ID,  iRightsLevel
                            ucBonds.Visible = true;
                            ucShares.Visible = false;
                            ucETFs.Visible = false;
                            ucFunds.Visible = false;
                            ucRates.Visible = false;
                            ucIndexes.Visible = false;
                            break;
                        case 3:
                            ucRates.Mode = 1;           // 1 - from ProductsList, 2 - from ProductsData, 3 - from ProductsWishList
                            ucRates.ShowRecord(Convert.ToInt32(fgList[fgList.Row, 1]), Convert.ToInt32(fgList[fgList.Row, 3]), 0, iRightsLevel);     // fgList(fgList.Row, 1) - Share_ID,  iRightsLevel
                            ucRates.Visible = true;
                            ucShares.Visible = false;
                            ucBonds.Visible = false;
                            ucETFs.Visible = false;
                            ucFunds.Visible = false;
                            ucIndexes.Visible = false;
                            ucIndexes.Visible = false;
                            break;
                        case 4:
                            ucETFs.Mode = 1;           // 1 - from ProductsList, 2 - from ProductsData, 3 - from ProductsWishList
                            ucETFs.ShowRecord(Convert.ToInt32(fgList[fgList.Row, 1]), Convert.ToInt32(fgList[fgList.Row, 3]), 0, iRightsLevel);     // fgList(fgList.Row, 1) - Share_ID,  iRightsLevel
                            ucETFs.Visible = true;
                            ucShares.Visible = false;
                            ucBonds.Visible = false;
                            ucFunds.Visible = false;
                            ucRates.Visible = false;
                            ucIndexes.Visible = false;
                            break;
                        case 5:
                            ucIndexes.Mode = 1;           // 1 - from ProductsList, 2 - from ProductsData, 3 - from ProductsWishList                                                        
                            ucIndexes.ShowRecord(Convert.ToInt32(fgList[fgList.Row, 1]), Convert.ToInt32(fgList[fgList.Row, 3]), 0, iRightsLevel);     // fgList(fgList.Row, 1) - Share_ID,  iRightsLevel
                            ucIndexes.Visible = true;
                            ucShares.Visible = false;
                            ucBonds.Visible = false;
                            ucETFs.Visible = false;
                            ucFunds.Visible = false;
                            ucRates.Visible = false;
                            break;
                        case 6:
                            ucFunds.Mode = 1;           // 1 - from ProductsList, 2 - from ProductsData, 3 - from ProductsWishList
                            ucFunds.ShowRecord(Convert.ToInt32(fgList[fgList.Row, 1]), Convert.ToInt32(fgList[fgList.Row, 3]), 0, iRightsLevel);     // fgList(fgList.Row, 1) - Share_ID,  iRightsLevel
                            ucFunds.Visible = true;
                            ucShares.Visible = false;
                            ucBonds.Visible = false;
                            ucETFs.Visible = false;
                            ucRates.Visible = false;
                            ucIndexes.Visible = false;
                            break;
                    }
                }
            }
        }
        private void tscbProductTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tscbProductTypes.SelectedIndex != 0)
            {
                dtView = Global.dtProductsCategories.Copy().DefaultView;
                dtView.RowFilter = "Product_ID = " + tscbProductTypes.SelectedIndex;
            }

            DataFiltering(0);
        }
        private void DataFiltering(int iCurShare_ID)
        {
            iOld_ID = -999;
            bCheckList = false;
            bFound = true;

            fgList.Redraw = false;
            fgList.Rows.Count = 1;

            dtView = Global.dtProducts.DefaultView;

            if (rbAllProducts.Checked) sTemp = "Aktive > -1 AND ";
            else sTemp = "Aktive > 0 AND ";

            if (tscbProductTypes.SelectedIndex != 0)
                sTemp = sTemp + "Product_ID = " + tscbProductTypes.SelectedIndex + " AND ";

            if (sStockExchanges != "0") sTemp = sTemp + " StockExchange_ID IN (" + sStockExchanges + ") AND ";

            // iFoundChoicedList - flag for filtering by choiced lists: 1 - all choiced lists so not need to filter , 0 - some choiced lists so need to filter
            iFoundChoicedList = 1;                                         // 1 - all choiced lists
            if (rbListsProducts.Checked)
            {
                for (i = 1; i <= fgListsProducts.Rows.Count - 1; i++)
                    if (!Convert.ToBoolean(fgListsProducts[i, 0])) iFoundChoicedList = 0;                             // 0 - not all choiced lists 

                if (iFoundChoicedList == 1) sTemp = sTemp + " HFIC_Recom = 1 AND ";
            }
            sTemp = sTemp + "(Title LIKE '%" + txtFilter.Text.Trim() + "%' OR ISIN LIKE '%" + txtFilter.Text.Trim() + "%' OR Code LIKE '%" +
                    txtFilter.Text.Trim() + "%' OR Code2 LIKE '%" + txtFilter.Text.Trim() + "%' OR SecID LIKE '%" + txtFilter.Text.Trim() + "%')";

            dtView.RowFilter = sTemp;

            foreach (DataRowView dtViewRow in dtView)
            {
                bFound = false;
                if (iOld_ID != Convert.ToInt32(dtViewRow["Shares_ID"]) && iFoundChoicedList == 1) bFound = true;

                if (bFound) {
                    iOld_ID = Convert.ToInt32(dtViewRow["Shares_ID"]);
                    fgList.AddItem(dtViewRow["Title"] + "\t" + dtViewRow["Shares_ID"] + "\t" + dtViewRow["ShareTitles_ID"] + "\t" + dtViewRow["ID"] + "\t" +
                                   dtViewRow["Product_ID"]);
                }
            }
            fgList.Redraw = true;

            if (fgList.Rows.Count > 1)
            {
                bCheckList = false;
                fgList.Row = 0;
                bCheckList = true;
                if (iCurShare_ID == 0) fgList.Row = 1;
                else
                {
                    sTemp = iCurShare_ID.ToString();
                    i = fgList.FindRow(sTemp, 1, 1, false);
                    if (i > 0) fgList.Row = i;
                }
            }
            bCheckList = true;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            sStockExchanges = "0";
            for (i = 1; i <= fgStockExchanges.Rows.Count - 1; i++) {
                if (Convert.ToBoolean(fgStockExchanges[i, 0]))
                    sStockExchanges = sStockExchanges + ", " + fgStockExchanges[i, 2];
            }
            DataFiltering(0);

            panFilters.Visible = false;
        }

        private void lnkImportETF_MorningStar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panExtraCommands.Visible = false;

            frmImportData locImportData = new frmImportData();
            locImportData.FileType = 0;                        // 0 - xlsx Excel 2007
            locImportData.Shema = 5;                           // 5 - εισαγωγή ETF
            locImportData.ReadMode = 2;
            locImportData.ShowDialog();
            if (locImportData.Aktion == 1)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("el-GR");

                panImport.BackColor = Color.Moccasin;
                panImport.Left = (this.Width - 654) / 2;
                panImport.Height = 76;
                panImport.Top = (this.Height - 76) / 2;
                panImport.Visible = true;

                pbImport.Minimum = 0;
                pbImport.Maximum = locImportData.Result.Rows.Count;
                pbImport.Value = 0;

                fgWarnings.Redraw = false;
                fgWarnings.Rows.Count = 1;
                this.Refresh();

                klsProductsCodes = new clsProductsCodes();
                klsProductsCodes.Product_ID = 4;                      // ShareType = 4 - ETF 
                klsProductsCodes.EditRecord_ZeroInfoFlag();

                iLogs = 0;

                foreach (DataRow dtRow in locImportData.Result.Rows)
                {
                    try
                    {
                        bError = false;
                        if (dtRow["f4"] + "" == "")
                        {
                            AddLogRec("ISIN " + dtRow["f4"] + " ISIN is mandatory");
                            bError = true;
                        }

                        if (!bError)
                        {
                            iSE_ID = DefineItemID("StockExchanges", "MstarTitle", dtRow["f7"] + "", false, "");
                            iPrimaryShare = ((dtRow["f8"] + "").ToUpper() == "YES") ? 2 : ((dtRow["f8"] + "").ToUpper() == "NO") ? 1 : 0;
                            sCurrency = DefineCurrency("Currencies", "Code_MorningStar", dtRow["f9"] + "");
                            sRiskCurrency = DefineCurrency("Currencies", "Code_MorningStar", dtRow["f9"] + "");

                            sTemp = dtRow["f13"] + "";
                            sProviderName = sTemp.Replace("'", "`");

                            sTemp = dtRow["f29"] + "";
                            sDescriptionEn = sTemp.Replace("'", "`");

                            foundRows = Global.dtProducts.Select("ISIN = '" + dtRow["f4"] + "' AND StockExchange_ID = " + iSE_ID +
                                        " AND Currency = '" + sCurrency + "' AND Aktive >= 1");
                            if (foundRows.Length > 0)
                            {
                                if ((dtRow["f4"] + "").Trim() == (foundRows[0]["ISIN"] + "").Trim())
                                {
                                    if (Convert.ToInt32(foundRows[0]["Product_ID"]) == 4)
                                    {
                                        iShare_ID = Convert.ToInt32(foundRows[0]["Shares_ID"]);
                                        iShareTitle_ID = Convert.ToInt32(foundRows[0]["ShareTitles_ID"]);
                                        iShareCode_ID = Convert.ToInt32(foundRows[0]["ID"]);

                                        klsProducts = new clsProducts();
                                        klsProducts.Record_ID = iShare_ID;
                                        klsProducts.GetRecord();
                                        klsProducts.Product_ID = 4;                         // ShareType = 4 - ETF 
                                        klsProducts.EditRecord();

                                        if (iPrimaryShare == 2)
                                        {                                   // 2 - YES
                                            klsProductsTitles = new clsProductsTitles();
                                            klsProductsTitles.Record_ID = iShareTitle_ID;
                                            klsProductsTitles.GetRecord();
                                            klsProductsTitles.ProductTitle = dtRow["f1"] + "";
                                            klsProductsTitles.StandardTitle = dtRow["f2"] + "";
                                            klsProductsTitles.FundID = dtRow["f3"] + "";
                                            klsProductsTitles.ISIN = dtRow["f4"] + "";
                                            klsProductsTitles.BrandProviderName = dtRow["f12"] + "";
                                            klsProductsTitles.ProviderName = sProviderName;
                                            sTemp = (dtRow["f14"] + "").Trim();
                                            if (sTemp.Length > 0 && sTemp.IndexOf("NULL") < 0 ) klsProductsTitles.URL = sTemp;
                                            if ((dtRow["f71"]+"") != "" && (dtRow["f71"]+"") != "NULL") klsProductsTitles.CreditRating = dtRow["f71"]+"";
                                            klsProductsTitles.AmountOutstanding = (Global.IsNumeric(dtRow["f72"]) ? Convert.ToDecimal(dtRow["f72"]+"") : 0);
                                            klsProductsTitles.MiFIDInstrumentType = DefineItemID("MiFID_InstrumentType", "Title", dtRow["f15"] + "", false, "");
                                            klsProductsTitles.AIFMD = ((dtRow["f16"]+"") == "No" ? 0 : ((dtRow["f16"]+"") == "Yes" ? 1 : 2));
                                            klsProductsTitles.Leverage = ((dtRow["f17"] + "") == "No" ? 0 : ((dtRow["f17"] + "") == "Yes" ? 1 : 2));
                                            klsProductsTitles.MinimumInvestment = dtRow["f18"] + "";
                                            if (Global.IsNumeric(dtRow["f19"] + "")) {
                                                klsProductsTitles.SurveyedKIID = Convert.ToSingle(dtRow["f19"]);
                                                klsProductsTitles.SurveyedKIID_Date = dtRow["f20"] + "";
                                            }
                                            if (Global.IsNumeric(dtRow["f21"] + "")) {
                                                klsProductsTitles.OngoingKIID = Convert.ToSingle(dtRow["f21"]);
                                                klsProductsTitles.OngoingKIID_Date = dtRow["f22"] + "";
                                            }
                                            klsProductsTitles.RatingOverall = dtRow["f23"] + "";
                                            klsProductsTitles.RatingDate = dtRow["f24"] + "";
                                            klsProductsTitles.GlobalBroad = DefineItemID("GlobalBroadCategories", "Title", dtRow["f25"]+"", false, "");
                                            klsProductsTitles.CategoryMorningStar = DefineItemID("FundCategoriesMorningStar", "Title", dtRow["f26"] + "", true, ""); ;
                                            klsProductsTitles.Benchmark = DefineItemID("Benchmarks", "Title", dtRow["f27"] + "", true, "");
                                            klsProductsTitles.CountryRisk_ID = DefineCountryID("Countries", "Title_MorningStar", dtRow["f28"]+"", false);
                                            klsProductsTitles.RiskCurr = sRiskCurrency;
                                            klsProductsTitles.DescriptionEn = sDescriptionEn;
                                            klsProductsTitles.DescriptionGr = dtRow["f30"] + "";
                                            klsProductsTitles.InvestmentType = DefineItemID("InvestmentTypes", "Title", dtRow["f33"] + "", false, "");
                                            klsProductsTitles.LegalStructure_ID = DefineItemID("FundLegalStructures", "Title", dtRow["f34"] + "", false, "");
                                            klsProductsTitles.InceptionDate = dtRow["f35"] + "";
                                            klsProductsTitles.Country_ID = DefineCountryID("Countries", "Title_MorningStar", dtRow["f36"] + "", false);
                                            klsProductsTitles.Institutional = dtRow["f37"] + "";
                                            klsProductsTitles.ActivelyManaged = DefineItemID("TargetMarketList1", "Title", dtRow["f38"] + "", false, "");
                                            klsProductsTitles.ReplicationMethod = dtRow["f39"] + "";
                                            klsProductsTitles.SwapBasedETF = dtRow["f40"] + "";
                                            klsProductsTitles.CountryRegistered = dtRow["f41"] + "";
                                            klsProductsTitles.EstimatedKIID = dtRow["f42"] + "";
                                            klsProductsTitles.EstimatedKIID_Date = dtRow["f43"] + "";
                                            klsProductsTitles.SurveyedKIID_History = dtRow["f44"] + "";
                                            if (Global.IsNumeric(dtRow["f45"] + ""))  klsProductsTitles.SRRIValues = dtRow["f45"] + "";
                                            klsProductsTitles.SRRIValues_Date = dtRow["f46"] + "";
                                            klsProductsTitles.ManagmentFee = dtRow["f47"] + "";
                                            klsProductsTitles.ManagmentFee_Date = dtRow["f48"] + "";
                                            klsProductsTitles.PerformanceFee = dtRow["f49"] + "";
                                            klsProductsTitles.PerformanceFee_Date = dtRow["f50"] + "";
                                            klsProductsTitles.InvestType_Retail = DefineItemID("TargetMarketList1", "Title", dtRow["f51"] + "", false, "");
                                            klsProductsTitles.InvestType_Prof = DefineItemID("TargetMarketList1", "Title", dtRow["f52"] + "", false, "");
                                            klsProductsTitles.InvestType_Eligible = DefineItemID("TargetMarketList1", "Title", dtRow["f53"] + "", false, "");
                                            klsProductsTitles.Expertise_Basic = DefineItemID("TargetMarketList1", "Title", dtRow["f54"] + "", false, "");
                                            klsProductsTitles.Expertise_Informed = DefineItemID("TargetMarketList1", "Title", dtRow["f55"] + "", false, "");
                                            klsProductsTitles.Expertise_Advanced = DefineItemID("TargetMarketList1", "Title", dtRow["f56"] + "", false, "");
                                            klsProductsTitles.RecHoldingPeriod = dtRow["f57"] + "";
                                            klsProductsTitles.RetProfile_Preserv = DefineItemID("TargetMarketList1", "Title", dtRow["f58"] + "", false, "");
                                            klsProductsTitles.RetProfile_Income = DefineItemID("TargetMarketList1", "Title", dtRow["f59"] + "", false, "");
                                            klsProductsTitles.RetProfile_Growth = DefineItemID("TargetMarketList1", "Title", dtRow["f60"] + "", false, "");
                                            klsProductsTitles.Distrib_ExecOnly = DefineItemID("TargetMarketList2", "Title", dtRow["f61"] + "", false, "");
                                            klsProductsTitles.Distrib_Advice = DefineItemID("TargetMarketList2", "Title", dtRow["f62"] + "", false, "");
                                            klsProductsTitles.Distrib_PortfolioManagment = DefineItemID("TargetMarketList2", "Title", dtRow["f63"] + "", false, "");
                                            klsProductsTitles.CapitalLoss_None = DefineItemID("TargetMarketList1", "Title", dtRow["f64"] + "", false, "");
                                            klsProductsTitles.CapitalLoss_Limited = DefineItemID("TargetMarketList1", "Title", dtRow["f65"] + "", false, "");
                                            klsProductsTitles.CapitalLoss_NoGuarantee = DefineItemID("TargetMarketList1", "Title", dtRow["f66"] + "", false, "");
                                            klsProductsTitles.CapitalLoss_BeyondInitial = DefineItemID("TargetMarketList1", "Title", dtRow["f67"] + "", false, "");
                                            klsProductsTitles.CapitalLoss_Level = DefineItemID("TargetMarketList1", "Title", dtRow["f68"] + "", false, "");
                                            klsProductsTitles.CountryAvailable = dtRow["f69"] + "";

                                            klsProductsTitles.LastEditDate = DateTime.Now;
                                            klsProductsTitles.LastEditUser_ID = Global.User_ID;
                                            klsProductsTitles.EditRecord();
                                        }

                                        klsProductsCodes = new clsProductsCodes();
                                        klsProductsCodes.Record_ID = iShareCode_ID;
                                        klsProductsCodes.GetRecord();
                                        klsProductsCodes.CodeTitle = dtRow["f1"] + "";
                                        klsProductsCodes.ISIN = dtRow["f4"] + "";
                                        klsProductsCodes.SecID = dtRow["f5"] + "";
                                        klsProductsCodes.Code3 = dtRow["f6"] + "";
                                        klsProductsCodes.PrimaryShare = iPrimaryShare;
                                        klsProductsCodes.StockExchange_ID = iSE_ID;
                                        klsProductsCodes.Curr = sCurrency;
                                        klsProductsCodes.CurrencyHedge = (dtRow["f10"] + "" == "Fully Hedged" ? 1 : 0);
                                        klsProductsCodes.CurrencyHedge2 = DefineCurrency("Currencies", "Code_MorningStar", dtRow["f11"] + "");
                                        klsProductsCodes.DistributionStatus = dtRow["f31"] + "";

                                        switch (dtRow["f32"] + "") {
                                            case "Annually":
                                                klsProductsCodes.FrequencyClipping = 1;
                                                break;
                                            case "Weekly":
                                                klsProductsCodes.FrequencyClipping = 2;
                                                break;
                                            case "Monthly":
                                                klsProductsCodes.FrequencyClipping = 3;
                                                break;
                                            case "Quarterly":
                                                klsProductsCodes.FrequencyClipping = 4;
                                                break;
                                            case "Yearly":
                                                klsProductsCodes.FrequencyClipping = 5;
                                                break;
                                            case "Semi-Annually":
                                                klsProductsCodes.FrequencyClipping = 6;
                                                break;
                                            case "None":
                                                klsProductsCodes.FrequencyClipping = 7;
                                                break;
                                            default:
                                                klsProductsCodes.FrequencyClipping = 0;
                                                break;
                                        }

                                        klsProductsCodes.Aktive = 1;
                                        klsProductsCodes.InfoFlag = 1;
                                        klsProductsCodes.EditRecord();

                                        sTemp = Global.RecalcRiskProfile(iShareCode_ID);
                                        klsProductsCodes = new clsProductsCodes();
                                        klsProductsCodes.Record_ID = iShareCode_ID;
                                        klsProductsCodes.GetRecord();
                                        sOldRiskProfile = klsProductsCodes.MIFID_Risk;
                                        klsProductsCodes.MIFID_Risk = sTemp;
                                        klsProductsCodes.EditRecord();


                                        if (sOldRiskProfile != sTemp) {
                                            ProductsLogger = new clsProductsLogger();
                                            ProductsLogger.ShareCodes_ID = iShareCode_ID;
                                            ProductsLogger.OldMIFID_Risk = sOldRiskProfile;
                                            ProductsLogger.NewMIFID_Risk = sTemp;
                                            ProductsLogger.EditDate = DateTime.Now;
                                            ProductsLogger.EditMethod = 1;                               // 1 - Enimerosi, 2- Edit
                                            ProductsLogger.InsertRecord();
                                        } 
                                    }
                                    else AddLogRec("ISIN " + dtRow["f4"] + "     ISIN = '" + dtRow["f3"] + "'.    Δεν είναι ETF");
                                }
                                else AddLogRec("ISIN " + dtRow["f4"] + "     Wrong ISIN = '" + dtRow["f3"]);
                            }
                            else AddLogRec("ISIN " + dtRow["f4"] + "      Unknown ISIN + StockExchange + Currency ");

                            pbImport.Value = pbImport.Value + 1;
                        }
                    }
                    catch (Exception z)
                    {
                        MessageBox.Show(dtRow["f1"] + "  " + z.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }                

                fgWarnings.Redraw = true;
                pbImport.Visible = false;

                if (fgWarnings.Rows.Count > 0) lblResult.Text = "See Log";
                else lblResult.Text = "OK";

                panImport.BackColor = Color.Silver;
                panImport.Left = (this.Width - 654) / 2;
                panImport.Height = 380;
                panImport.Top = (this.Height - 380) / 2;
                this.Refresh();

                Systems = new clsSystem();
                Systems.EditCashTables_LastEdit_Time(2);
            }

            Global.GetProductsList();
            DataFiltering(0);
        }
        private void lnkFinish_ETFs_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            foreach (DataRow dtRow in Global.dtProducts.Copy().Rows)
            {
                if (Convert.ToInt32(dtRow["Product_ID"]) == 4)
                {
                    klsProductsTitles = new clsProductsTitles();
                    klsProductsTitles.Record_ID = Convert.ToInt32(dtRow["ShareTitles_ID"]);
                    klsProductsTitles.GetRecord();

                    foundRows = Global.dtCountries.Select("ID = " + klsProductsTitles.CountryRisk_ID);
                    if (foundRows.Length > 0)
                        klsProductsTitles.CountryGroup_ID = Convert.ToInt32(foundRows[0]["CountriesGroup_ID"]);

                    klsProductsTitles.RatingGroup = Global.DefineRatingGroup("", "", "", "", klsProductsTitles.CreditRating);

                    klsProductsTitles.LastEditDate = DateTime.Now;
                    klsProductsTitles.LastEditUser_ID = Global.User_ID;
                    klsProductsTitles.EditRecord();

                    klsProductsCodes = new clsProductsCodes();
                    klsProductsCodes.Record_ID = Convert.ToInt32(dtRow["ID"]);
                    klsProductsCodes.GetRecord();
                    sTemp = Global.RecalcRiskProfile(Convert.ToInt32(dtRow["ID"]));
                    if (sTemp == "000000") sTemp = "";
                    klsProductsCodes.MIFID_Risk = sTemp;
                    klsProductsCodes.EditRecord();
                }
            }

            fgWarnings.Redraw = false;
            fgWarnings.Rows.Count = 1;

            klsProductsCodes = new clsProductsCodes();
            klsProductsCodes.Product_ID = 4;                  // 4 - ETFs
            klsProductsCodes.GetList_ProductType();
            foreach (DataRow dtRow in klsProductsCodes.List.Rows)
            {
                if ((dtRow["ISIN"] + "").Trim() == "") AddLogRec("ETF RIC = '" + dtRow["Code"] + "'  missing ISIN");
                if ((dtRow["Currency"] + "").Trim() == "") AddLogRec("ETF ISIN = '" + dtRow["ISIN"] + "'  missing Currency");
                if ((dtRow["ISIN"] + "").Trim() == "" || (dtRow["Currency"] + "").Trim() == "" || (dtRow["StockExchange_Code"] + "").Trim() == "")
                    AddLogRec("ETF RIC = '" + dtRow["Code"] + "'  missing one of data:  ISIN = '" + dtRow["ISIN"] + "' or Currency = '" + 
                              dtRow["Currency"] + "' or StockExchange = '" + dtRow["StockExchange_Code"] + "'");
                if (Convert.ToInt32(dtRow["InfoFlag"]) == 0) 
                    AddLogRec("Uninformed ISIN = '" + dtRow["ISIN"] + "'  RIC = '" + dtRow["Code"] + "'   ID = " + dtRow["ID"]);    // ID = ShareCodes.ID
            }

            fgWarnings.Redraw = true;
            panImport.Visible = true;
            this.Refresh();

            this.Cursor = Cursors.Default;
        }
        private void picCloseExtraCommands_Click(object sender, EventArgs e)
        {
            panExtraCommands.Visible = false;
        }

        private void lnkChangeGroup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panExtraCommands.Visible = false;
            switch (Convert.ToInt32(fgList[fgList.Row, "Product_ID"])) {
                case 1:
                    lblShareType.Text = ucShares.cmbProductType.Text;
                    lblProductType.Text = ucShares.cmbProductCategory.Text;
                    break;
                case 2:
                    lblShareType.Text = ucBonds.cmbProductType.Text;
                    lblProductType.Text = ucBonds.cmbProductCategory.Text;
                    break;
                case 4:
                    lblShareType.Text = ucETFs.cmbProductType.Text;
                    lblProductType.Text = ucETFs.cmbProductCategory.Text;
                    break;
                case 5:
                    lblShareType.Text = ucIndexes.cmbProductType.Text;
                    break;
                case 6:
                    lblShareType.Text = ucFunds.cmbProductType.Text;
                    lblProductType.Text = ucFunds.cmbProductCategory.Text;
                    break;
            }
            panChangeGroup.Visible = true;
        }

        private void btnOKChangeGroup_Click(object sender, EventArgs e)
        {
            clsProducts Products = new clsProducts();
            Products.Record_ID = Convert.ToInt32(fgList[fgList.Row, 1]);
            Products.GetRecord();
            Products.Product_ID = Convert.ToInt32(cmbProducts.SelectedValue);
            Products.EditRecord();

            clsProductsTitles ProductsTitles = new clsProductsTitles();
            ProductsTitles.Record_ID = Convert.ToInt32(fgList[fgList.Row, 1]);
            ProductsTitles.GetRecord();
            ProductsTitles.ProductCategory = Convert.ToInt32(cmbCategories.SelectedValue);
            ProductsTitles.EditRecord();

            if (tscbProductTypes.SelectedIndex != 0 && tscbProductTypes.SelectedIndex != Convert.ToInt32(fgList[fgList.Row, 2]))
                fgList.RemoveItem(fgList.Row);
            else {
                fgList[fgList.Row, "Product_ID"] = cmbProducts.SelectedValue;
                fgList[fgList.Row, "ShareTitles.ID"] = cmbCategories.SelectedValue;

                switch (Convert.ToInt32(fgList[fgList.Row, "Product_ID"]))
                {
                    case 1:
                        ucShares.cmbProductType.SelectedValue = Convert.ToInt32(cmbProducts.SelectedValue);
                        ucShares.cmbProductCategory.SelectedValue = Convert.ToInt32(cmbCategories.SelectedValue);
                        break;
                    case 2:
                        ucBonds.cmbProductType.SelectedValue = Convert.ToInt32(cmbProducts.SelectedValue);
                        ucBonds.cmbProductCategory.SelectedValue = Convert.ToInt32(cmbCategories.SelectedValue);
                        break;
                    case 4:
                        ucETFs.cmbProductType.SelectedValue = Convert.ToInt32(cmbProducts.SelectedValue);
                        ucETFs.cmbProductCategory.SelectedValue = Convert.ToInt32(cmbCategories.SelectedValue);
                        break;
                    case 5:
                        ucIndexes.cmbProductType.SelectedValue = Convert.ToInt32(cmbProducts.SelectedValue);
                        break;
                    case 6:
                        ucFunds.cmbProductType.SelectedValue = Convert.ToInt32(cmbProducts.SelectedValue);
                        ucFunds.cmbProductCategory.SelectedValue = Convert.ToInt32(cmbCategories.SelectedValue);
                        break;
                }
            }

            dtView = Global.dtProducts.DefaultView;
            sTemp = "Shares_ID = " + fgList[fgList.Row, 1];
            dtView.RowFilter = sTemp;
            foreach (DataRowView dtViewRow in dtView)
                dtViewRow["Product_ID"] = cmbProducts.SelectedValue;

            Systems = new clsSystem();
            Systems.EditCashTables_LastEdit_Time(2);

            Global.GetProductsList();

            panChangeGroup.Visible = false;
        }

        private void cmbProducts_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) {
                dtView = Global.dtProductsCategories.Copy().DefaultView;
                dtView.RowFilter = "Product_ID = " + cmbProducts.SelectedValue;
                cmbCategories.DataSource = dtView;
                cmbCategories.DisplayMember = "Title";
                cmbCategories.ValueMember = "ID";
                //cmbCategories.SelectedIndex = 1;
            }
        }
        private void lnkImportShare_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panExtraCommands.Visible = false;

            frmImportData locImportData = new frmImportData();
            locImportData.FileType = 0;                        // 0 - xlsx Excel 2007
            locImportData.Shema = 7;                           // 7 - εισαγωγή μετοχών
            locImportData.ReadMode = 2;
            locImportData.ShowDialog();
            if (locImportData.Aktion == 1) {
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("el-GR");

                panImport.BackColor = Color.Moccasin;
                panImport.Left = (this.Width - 654) / 2;
                panImport.Height = 76;
                panImport.Top = (this.Height - 76) / 2;
                panImport.Visible = true;

                pbImport.Minimum = 0;
                pbImport.Maximum = locImportData.Result.Rows.Count;
                pbImport.Value = 0;

                fgWarnings.Redraw = false;
                fgWarnings.Rows.Count = 1;
                this.Refresh();

                klsProductsCodes = new clsProductsCodes();
                klsProductsCodes.Product_ID = 1;                  // 1 - Share
                klsProductsCodes.EditRecord_ZeroInfoFlag();

                iLogs = 0;
                switch (Convert.ToInt32(locImportData.Shema)) {
                    case 7:                                                         // 7 - Ενημέρωση Μετοχών από Reuters
                        foreach (DataRow dtRow in locImportData.Result.Rows) {
                            try
                            {
                                bError = false;
                                if ((dtRow["f1"]+"") != (dtRow["f2"]+""))
                                {
                                    AddLogRec("Reuters Code " + dtRow["f1"] + " not equal Reuters Code in second column " + dtRow["f2"]);
                                    bError = true;
                                }
                                if (dtRow["f3"] + "" == "")
                                {
                                    AddLogRec("Reuters Code " + dtRow["f1"] + " ISIN is mandatory");
                                    bError = true;
                                }
                                if (!bError)
                                {
                                    sTemp = dtRow["f4"] + "";
                                    sProviderName = sTemp.Replace("'", "`");
                                    iCountry_ID = DefineItemID("Countries", "Code", dtRow["f5"] + "", false, "");
                                    iCountryRisk_ID = DefineItemID("Countries", "Code", dtRow["f5"] + "", false, "");
                                    iSector_ID = DefineItemID("Sectors", "Title", dtRow["f9"] + "", false, " AND Sectors.L1 = 1");
                                    iSE_ID = DefineItemID("StockExchanges", "Code", dtRow["f13"] + "", false, "");
                                    sCurrency = DefineCurrency("Currencies", "Title", dtRow["f14"] + "");
                                    sRiskCurrency = DefineCurrency("Currencies", "Title", dtRow["f15"] + "");
                                    iPrimaryShare = (dtRow["f16"] + "" == "1" ? 2 : (dtRow["f16"] + "" == "0" ? 1 : 0));
                                    dIPO = (Global.IsDate(dtRow["f17"] + "") ? Convert.ToDateTime(dtRow["f17"] + "") : Convert.ToDateTime("1900/01/01"));
                                    sTemp = dtRow["f18"] + "";
                                    sDescriptionEn = sTemp.Replace("'", "`");
                                    fltQuantityMin = (Global.IsNumeric(dtRow["f19"] + "") ? Convert.ToInt16(dtRow["f19"] + "") : -1);
                                    sDateIncorporation = dtRow["f20"] + "";
                                    decMarketCapitalization = (Global.IsNumeric(dtRow["f21"] + "") ? Convert.ToDecimal(dtRow["f21"] + "") : -1);
                                    sMarketCapitalizationCurr = dtRow["f22"] + "";
                                    iInstrumentType = (Global.IsNumeric(dtRow["f23"] + "") ? Convert.ToInt32(dtRow["f23"] + "") : -1);

                                    iCountriesGroup_ID = 0;
                                    foundRows = Global.dtCountries.Select("ID = " + iCountry_ID);
                                    if (foundRows.Length > 0) iCountriesGroup_ID = Convert.ToInt32(foundRows[0]["CountriesGroup_ID"]);
                                    foundRows = Global.dtProducts.Select("Code = '" + dtRow["f1"] + "' AND Aktive >=1 ");
                                    if (foundRows.Length > 0)
                                    {
                                        if (dtRow["f3"] + "" == foundRows[0]["ISIN"] + "")
                                        {
                                            if (Convert.ToInt32(foundRows[0]["Product_ID"]) == 1)
                                            {
                                                iShare_ID = Convert.ToInt32(foundRows[0]["Shares_ID"]);
                                                iShareTitle_ID = Convert.ToInt32(foundRows[0]["ShareTitles_ID"]);
                                                iShareCode_ID = Convert.ToInt32(foundRows[0]["ID"]);

                                                klsProducts = new clsProducts();
                                                klsProducts.Record_ID = iShare_ID;
                                                klsProducts.GetRecord();
                                                klsProducts.Product_ID = 1;                          // ShareType = 1 - Shares 
                                                klsProducts.EditRecord();

                                                klsProductsTitles = new clsProductsTitles();
                                                klsProductsTitles.Record_ID = iShareTitle_ID;
                                                klsProductsTitles.GetRecord();
                                                klsProductsTitles.ProviderName = sProviderName;
                                                klsProductsTitles.ProductTitle = dtRow["f11"] + "";
                                                klsProductsTitles.ISIN = dtRow["f3"] + "";
                                                klsProductsTitles.Country_ID = iCountry_ID;
                                                klsProductsTitles.Sector_ID = iSector_ID;
                                                klsProductsTitles.CountryRisk_ID = iCountryRisk_ID;
                                                klsProductsTitles.CountryGroup_ID = iCountriesGroup_ID;
                                                sTemp = (dtRow["f10"] + "").Trim();
                                                if (sTemp.Length > 0 && sTemp.IndexOf("NULL") < 0)
                                                    klsProductsTitles.URL = sTemp;
                                                klsProductsTitles.RiskCurr = sRiskCurrency;
                                                klsProductsTitles.DescriptionEn = sDescriptionEn;


                                                if (sDateIncorporation.Length > 0) klsProductsTitles.DateIncorporation = sDateIncorporation;
                                                if (decMarketCapitalization >= 0) klsProductsTitles.MarketCapitalization = decMarketCapitalization;
                                                if (sMarketCapitalizationCurr.Length > 0) klsProductsTitles.MarketCapitalizationCurr = sMarketCapitalizationCurr;

                                                klsProductsTitles.LastEditDate = DateTime.Now;
                                                klsProductsTitles.LastEditUser_ID = Global.User_ID;
                                                klsProductsTitles.EditRecord();

                                                klsProductsCodes = new clsProductsCodes();
                                                klsProductsCodes.Record_ID = iShareCode_ID;
                                                klsProductsCodes.GetRecord();
                                                klsProductsCodes.CodeTitle = dtRow["f11"] + "";
                                                klsProductsCodes.ISIN = dtRow["f3"] + "";
                                                klsProductsCodes.Code3 = dtRow["f12"] + "";
                                                klsProductsCodes.StockExchange_ID = iSE_ID;
                                                klsProductsCodes.CountryAction = iCountryAction_ID;
                                                klsProductsCodes.Curr = sCurrency;
                                                klsProductsCodes.QuantityMin = fltQuantityMin;
                                                klsProductsCodes.PrimaryShare = iPrimaryShare;
                                                klsProductsCodes.DateIPO = dIPO;
                                                klsProductsCodes.Aktive = 1;
                                                klsProductsCodes.InfoFlag = 1;
                                                klsProductsCodes.EditRecord();

                                                sTemp = Global.RecalcRiskProfile(iShareCode_ID);
                                                klsProductsCodes = new clsProductsCodes();
                                                klsProductsCodes.Record_ID = iShareCode_ID;
                                                klsProductsCodes.GetRecord();
                                                klsProductsCodes.MIFID_Risk = sTemp;
                                                klsProductsCodes.EditRecord();
                                            }
                                            else AddLogRec("Reuters Code = '" + dtRow["f1"] + "     ISIN = '" + dtRow["f3"] + "'.    Δεν είναι μετοχή");
                                        }
                                        else AddLogRec("Reuters Code = '" + dtRow["f1"] + "     Wrong ISIN = '" + dtRow["f3"]);
                                    }
                                    else AddLogRec("Reuters Code = '" + dtRow["f1"] + "      Unknown Reuters Code");

                                    pbImport.Value = pbImport.Value + 1;
                                }                               
                            }
                            catch (Exception z) { 
                                MessageBox.Show(dtRow["f1"] + "  " + z.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
                            }
                        }
                        break;

                    case 25:                                                                    // 25 - PIREAUS Trade Files
                        foreach (DataRow dtRow in locImportData.Result.Rows) {
                            try {
                                iProductCategory_ID = DefineItemID("Products_Categories", "Title", dtRow["f2"] + "", false, "");
                                iHFCategory_ID = DefineItemID("HFCategories", "Title", dtRow["f3"] + "", false, "");

                                foundRows = Global.dtProducts.Select("Code = '" + dtRow["f1"] + "' AND Aktive >=1 ");
                                if (foundRows.Length > 0) {
                                    if (Convert.ToInt32(foundRows[0]["Product_ID"]) == 1) {
                                        iShare_ID = Convert.ToInt32(foundRows[0]["Shares_ID"]);
                                        iShareTitle_ID = Convert.ToInt32(foundRows[0]["ShareTitles_ID"]);
                                        iShareCode_ID = Convert.ToInt32(foundRows[0]["ID"]);

                                        klsProductsTitles = new clsProductsTitles();
                                        klsProductsTitles.Record_ID = iShareTitle_ID;
                                        klsProductsTitles.GetRecord();
                                        klsProductsTitles.ProductCategory = iProductCategory_ID;
                                        klsProductsTitles.HFCategory = iHFCategory_ID;
                                        klsProductsTitles.DescriptionGr = dtRow["f4"] + "";
                                        klsProductsTitles.ComplexProduct = (dtRow["f5"]+"" == "Yes"? 2 : (dtRow["f5"]+"" == "No"? 1: 0));
                                        klsProductsTitles.ComplexAttribute = dtRow["f6"] + "";
                                        klsProductsTitles.InvestType_Retail = (dtRow["f7"] + "" == "Yes" ? 2 : (dtRow["f7"] + "" == "No" ? 1 : 0));
                                        klsProductsTitles.InvestType_Prof = (dtRow["f8"] + "" == "Yes" ? 2 : (dtRow["f8"] + "" == "No" ? 1 : 0));
                                        klsProductsTitles.LastEditDate = DateTime.Now;
                                        klsProductsTitles.LastEditUser_ID = Global.User_ID;
                                        klsProductsTitles.EditRecord();
                                    }
                                    else AddLogRec("Reuters Code = '" + dtRow["f1"] + "'.    Δεν είναι μετοχή");
                                }
                                else AddLogRec("Reuters Code = '" + dtRow["f1"] + "      Unknown Reuters Code");

                                pbImport.Value = pbImport.Value + 1;
                            }
                            catch (Exception z) { MessageBox.Show(dtRow["f1"] + "  " + z.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                        }
                        break;
                }

                fgWarnings.Redraw = true;
                pbImport.Visible = false;

                if (fgWarnings.Rows.Count > 0) lblResult.Text = "See Log";
                else lblResult.Text = "OK";

                panImport.BackColor = Color.Silver;
                panImport.Left = (this.Width - 654) / 2;
                panImport.Height = 380;
                panImport.Top = (this.Height - 380) / 2;
                this.Refresh();

                Systems = new clsSystem();
                Systems.EditCashTables_LastEdit_Time(2);
            }

            Global.GetProductsList();
            DataFiltering(0);
        }
        private void lnkFinish_Shares_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            fgWarnings.Redraw = false;
            fgWarnings.Rows.Count = 1;

            klsProductsCodes = new clsProductsCodes();
            klsProductsCodes.Product_ID = 1;                // 1 - Share
            klsProductsCodes.GetList_InfoFlag();
            foreach (DataRow dtRow in klsProductsCodes.List.Rows) {
                if (dtRow["ISIN"]+"" == "")  AddLogRec("Share RIC = '" + dtRow["Code"] + "'  missing ISIN");
                if (dtRow["Currency"]+"" == "") AddLogRec("Share ISIN = '" + dtRow["ISIN"] + "'  missing Currency");
                if (Convert.ToInt16(dtRow["InfoFlag"]+"") == 0)  AddLogRec("Uninformed ISIN = '" + dtRow["ISIN"] + "'  RIC = '" + dtRow["Code"] + "'   ID = " + dtRow["ShareCode_ID"]);
            }

            fgWarnings.Redraw = true;
            panImport.Visible = true;
            this.Refresh();
        }

        private void lnkImportBond_Reuters_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panExtraCommands.Visible = false;

            frmImportData locImportData = new frmImportData();
            locImportData.FileType = 0;                        // 0 - xlsx Excel 2007
            locImportData.Shema = 6;                           // 7 - εισαγωγή μετοχών
            locImportData.ReadMode = 2;
            locImportData.ShowDialog();
            if (locImportData.Aktion == 1)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("el-GR");

                panImport.BackColor = Color.Moccasin;
                panImport.Left = (this.Width - 654) / 2;
                panImport.Height = 76;
                panImport.Top = (this.Height - 76) / 2;
                panImport.Visible = true;

                pbImport.Minimum = 0;
                pbImport.Maximum = locImportData.Result.Rows.Count;
                pbImport.Value = 0;

                fgWarnings.Redraw = false;
                fgWarnings.Rows.Count = 1;
                this.Refresh();

                klsProductsCodes = new clsProductsCodes();
                klsProductsCodes.Product_ID = 2;                  // 2 - Bonds
                klsProductsCodes.EditRecord_ZeroInfoFlag();

                iLogs = 0;

                foreach (DataRow dtRow in locImportData.Result.Rows)
                {
                    try
                    {
                        bError = false;
                        if ((dtRow["f2"] + "") != (dtRow["f3"] + ""))
                        {
                            if (dtRow["f3"] + "" != "NULL")
                            {
                                AddLogRec("ISIN " + dtRow["f2"] + " not equal ISIN in third column " + dtRow["f3"]);
                                bError = true;
                            }
                            else
                            {
                                iComplexReason_ID = DefineItemID("ComplexReasons", "Title", dtRow["f51"] + "", false, "");
                                SaveComplexReason(iComplexReason_ID);
                                bError = true;
                            }
                        }  

                        if (!bError)
                        {
                            sTemp = dtRow["f4"] + "";
                            sProviderName = sTemp.Replace("'", "`");
                            iCountry_ID = DefineItemID("Countries", "Code", dtRow["f5"] + "", false, "");
                            iCountryRisk_ID = DefineItemID("Countries", "Code", dtRow["f6"] + "", false, "");
                            iSector_ID = DefineItemID("Sectors", "Title", dtRow["f8"] + "", false, " AND Sectors.L1 = 1");
                            sTemp = dtRow["f10"] + "";
                            sDescriptionEn = sTemp.Replace("'", "`");
                            sCurrency = DefineCurrency("Currencies", "Title", dtRow["f13"] + "");
                            sRiskCurrency = DefineCurrency("Currencies", "Title", dtRow["f13"] + "");
                            iComplexReason_ID = DefineItemID("ComplexReasons", "Title", dtRow["f51"] + "", false, "");

                            iCountriesGroup_ID = 0;
                            foundRows = Global.dtCountries.Select("ID = " + iCountryRisk_ID);
                            if (foundRows.Length > 0)
                                iCountriesGroup_ID = Convert.ToInt32(foundRows[0]["CountriesGroup_ID"]);

                            foundRows = Global.dtProducts.Select("ISIN = '" + dtRow["f2"] + "' AND Aktive >= 1");
                            if (foundRows.Length > 0)
                            {
                                if ((dtRow["f2"] + "").Trim() == (foundRows[0]["ISIN"] + "").Trim())
                                {
                                    if (Convert.ToInt32(foundRows[0]["Product_ID"]) == 2)
                                    {
                                        iShare_ID = Convert.ToInt32(foundRows[0]["Shares_ID"]);
                                        iShareTitle_ID = Convert.ToInt32(foundRows[0]["ShareTitles_ID"]);
                                        iShareCode_ID = Convert.ToInt32(foundRows[0]["ID"]);

                                        klsProducts = new clsProducts();
                                        klsProducts.Record_ID = iShare_ID;
                                        klsProducts.GetRecord();
                                        klsProducts.Product_ID = 2;                          // ShareType = 2 - Bonds 
                                        klsProducts.EditRecord();

                                        klsProductsTitles = new clsProductsTitles();
                                        klsProductsTitles.Record_ID = iShareTitle_ID;
                                        klsProductsTitles.GetRecord();
                                        klsProductsTitles.ISIN = dtRow["f2"] + "";
                                        klsProductsTitles.ProviderName = sProviderName;
                                        klsProductsTitles.Country_ID = iCountry_ID;
                                        klsProductsTitles.Sector_ID = iSector_ID;
                                        klsProductsTitles.CountryRisk_ID = iCountryRisk_ID;
                                        klsProductsTitles.CountryGroup_ID = iCountriesGroup_ID;

                                        switch (dtRow["f9"] + "")
                                        {
                                            case "CORP":
                                                j = 1;             // 1 - corporate
                                                break;
                                            case "SOVR":
                                                j = 2;             // 2 - goverment
                                                break;
                                            case "SUPR":
                                                j = 3;             // 3 - yperethniko
                                                break;
                                            default:
                                                j = 0;
                                                break;
                                        }
                                        klsProductsTitles.BondType = j;
                                        klsProductsTitles.RiskCurr = sRiskCurrency;
                                        klsProductsTitles.DescriptionEn = sDescriptionEn;
                                        sTemp = (dtRow["f11"] + "").Trim();
                                        if (sTemp.Length > 0 && sTemp.IndexOf("NULL") < 0)
                                            klsProductsTitles.URL = sTemp;

                                        klsProductsTitles.ProductTitle = dtRow["f4"] + " " + dtRow["f12"] + "% " + sCurrency + " " + Convert.ToDateTime(dtRow["f14"] + "").ToString("dd/MM/yyyy");
                                        klsProductsTitles.CreditRating = dtRow["f17"] + "";

                                        foundRows = Global.dtRatingCodes.Select("RatingAgency_ID = 1 AND Code = '" + dtRow["f17"] + "'");
                                        if (foundRows.Length > 0)
                                        {
                                            klsProductsTitles.MoodysRating = dtRow["f17"] + "";
                                            if (Global.IsDate(dtRow["f18"] + ""))
                                                klsProductsTitles.MoodysRatingDate = Convert.ToDateTime(dtRow["f18"] + "");
                                        }

                                        foundRows = Global.dtRatingCodes.Select("RatingAgency_ID = 3 AND Code = '" + dtRow["f19"] + "'");
                                        if (foundRows.Length > 0)
                                        {
                                            klsProductsTitles.SPRating = dtRow["f19"] + "";
                                            if (Global.IsDate(dtRow["f20"] + ""))
                                                klsProductsTitles.SPRatingDate = Convert.ToDateTime(dtRow["f20"] + "");
                                        }

                                        foundRows = Global.dtRatingCodes.Select("RatingAgency_ID = 2 AND Code = '" + dtRow["f21"] + "'");
                                        if (foundRows.Length > 0)
                                        {
                                            klsProductsTitles.FitchsRating = dtRow["f21"] + "";
                                            if (Global.IsDate(dtRow["f22"] + ""))
                                                klsProductsTitles.FitchsRatingDate = Convert.ToDateTime(dtRow["f22"] + "");
                                        }

                                        klsProductsTitles.RatingGroup = Global.DefineRatingGroup(klsProductsTitles.MoodysRating, klsProductsTitles.FitchsRating, klsProductsTitles.SPRating, "", "");
                                        if (Global.IsNumeric(dtRow["f23"] + ""))
                                        {
                                            sTemp = dtRow["f23"] + "";
                                            klsProductsTitles.AmountOutstanding = Convert.ToDecimal(sTemp.Replace(".", ","));
                                        }
                                        else
                                            klsProductsTitles.AmountOutstanding = 0;

                                        if (dtRow["f25"] + "" != "NULL")
                                            klsProductsTitles.CallDate = dtRow["f25"] + "";

                                        klsProductsTitles.DenominationType = dtRow["f26"] + "";
                                        klsProductsTitles.IsConvertible = (dtRow["f27"] + "" == "N" ? 1 : (dtRow["f27"] + "" == "Y" ? 2 : 0));
                                        klsProductsTitles.IsDualCurrency = (dtRow["f28"] + "" == "N" ? 1 : (dtRow["f28"] + "" == "Y" ? 2 : 0));
                                        klsProductsTitles.IsHybrid = (dtRow["f29"] + "" == "N" ? 1 : (dtRow["f29"] + "" == "Y" ? 2 : 0));
                                        klsProductsTitles.IsGuaranteed = (dtRow["f30"] + "" == "N" ? 1 : (dtRow["f30"] + "" == "Y" ? 2 : 0));
                                        klsProductsTitles.IsPerpetualSecurity = (dtRow["f31"] + "" == "N" ? 1 : (dtRow["f31"] + "" == "Y" ? 2 : 0));
                                        klsProductsTitles.IsTotalLoss = (dtRow["f32"] + "" == "N" ? 1 : (dtRow["f32"] + "" == "Y" ? 2 : 0));
                                        klsProductsTitles.MinimumTotalLoss = dtRow["f33"] + "";
                                        klsProductsTitles.IsProspectusAvailable = (dtRow["f34"] + "" == "N" ? 1 : (dtRow["f34"] + "" == "Y" ? 2 : 0));
                                        klsProductsTitles.Rank = DefineItemID("Ranks", "Title", dtRow["f42"] + "", false, "");
                                        klsProductsTitles.IsCallable = (dtRow["f47"] + "" == "N" ? 1 : (dtRow["f47"] + "" == "Y" ? 2 : 0));
                                        klsProductsTitles.IsPutable = (dtRow["f48"] + "" == "N" ? 1 : (dtRow["f48"] + "" == "Y" ? 2 : 0));
                                        klsProductsTitles.InflationProtected = (dtRow["f49"] + "" == "N" ? 1 : (dtRow["f49"] + "" == "Y" ? 2 : 0));
                                        if (dtRow["f50"] + "" != "NULL")
                                            klsProductsTitles.OfferingTypeDescription = dtRow["f50"] + "";

                                        klsProductsTitles.LastEditDate = DateTime.Now;
                                        klsProductsTitles.LastEditUser_ID = Global.User_ID;
                                        klsProductsTitles.EditRecord();

                                        klsProductsCodes = new clsProductsCodes();
                                        klsProductsCodes.Record_ID = iShareCode_ID;
                                        klsProductsCodes.GetRecord();
                                        klsProductsCodes.ISIN = dtRow["f2"] + "";
                                        klsProductsCodes.CodeTitle = dtRow["f4"] + " " + dtRow["f12"] + "% " + sCurrency + " " + Convert.ToDateTime(dtRow["f14"] + "").ToString("dd/MM/yyyy");

                                        if (Global.IsNumeric(dtRow["f12"] + ""))
                                        {
                                            sTemp = dtRow["f12"] + "";
                                            klsProductsCodes.Coupone = Convert.ToSingle(sTemp.Replace(".", ","));
                                        }
                                        else
                                        {
                                            klsProductsCodes.Coupone = -1;
                                            AddLogRec("ISIN = '" + dtRow["f2"] + "'    Wrong Κουπόνι");
                                        }
                                        klsProductsCodes.Curr = sCurrency;

                                        if (Global.IsDate(dtRow["f14"] + ""))
                                            klsProductsCodes.Date2 = Convert.ToDateTime(dtRow["f14"] + "");
                                        else
                                        {
                                            klsProductsCodes.Date2 = Convert.ToDateTime("1900/01/01");
                                            AddLogRec("ISIN = '" + dtRow["f2"] + "'   Wrong Ημερομηνία Λήξης");
                                        }

                                        sTemp = dtRow["f15"] + "";
                                        sTemp = sTemp.Replace(".", "");
                                        klsProductsCodes.QuantityMin = (Global.IsNumeric(sTemp) ? Convert.ToSingle(sTemp) : 0);
                                        sTemp = dtRow["f16"] + "";
                                        sTemp = sTemp.Replace(".", "");
                                        klsProductsCodes.QuantityStep = (Global.IsNumeric(sTemp) ? Convert.ToSingle(sTemp) : 0);

                                        if (Global.IsNumeric(dtRow["f24"] + ""))
                                        {
                                            sTemp = dtRow["f24"] + "";
                                            klsProductsCodes.FrequencyClipping = Convert.ToInt32(sTemp.Replace(".", ","));
                                        }
                                        else
                                        {
                                            klsProductsCodes.FrequencyClipping = -1;
                                            AddLogRec("ISIN = '" + dtRow["f2"] + "'   Wrong Συχνότητα Αποκοπής Κουπονιού");
                                        }

                                        if (Global.IsDate(dtRow["f35"] + ""))
                                            klsProductsCodes.Date1 = Convert.ToDateTime(dtRow["f35"] + "");
                                        else
                                        {
                                            klsProductsCodes.Date1 = Convert.ToDateTime("1900/01/01");
                                            AddLogRec("ISIN = '" + dtRow["f2"] + "'   Wrong Ημερομηνία Έκδοσης");
                                        }

                                        if (Global.IsDate(dtRow["f36"] + ""))
                                            klsProductsCodes.Date3 = Convert.ToDateTime(dtRow["f36"] + "");
                                        else
                                        {
                                            klsProductsCodes.Date3 = Convert.ToDateTime("1900/01/01");
                                            AddLogRec("ISIN = '" + dtRow["f2"] + "'   Wrong Ημερ. 1ου Διακαν/μού");
                                        }

                                        if (Global.IsDate(dtRow["f37"] + ""))
                                            klsProductsCodes.Date4 = Convert.ToDateTime(dtRow["f37"] + "");
                                        else
                                        {
                                            klsProductsCodes.Date4 = Convert.ToDateTime("1900/01/01");
                                            AddLogRec("ISIN = '" + dtRow["f2"] + "'   Wrong Ημερ. Αποκ. 1ου Κουπ.");
                                        }

                                        klsProductsCodes.CouponeType = DefineItemID("CouponeTypes", "Title", dtRow["f38"] + "", false, "");

                                        sTemp = dtRow["f39"] + "";
                                        if (sTemp.IndexOf("#") < 0)
                                            klsProductsCodes.FloatingRate = dtRow["f39"] + "";
                                        else
                                            klsProductsCodes.FloatingRate = "N/A";

                                        if (Global.IsNumeric(dtRow["f40"] + ""))
                                        {
                                            sTemp = dtRow["f40"] + "";
                                            klsProductsCodes.Price = Convert.ToSingle(sTemp.Replace(".", ","));
                                        }
                                        else
                                        {
                                            klsProductsCodes.Price = -1;
                                            AddLogRec("ISIN = '" + dtRow["f2"] + "'   Wrong Τιμή Έκδοσης");
                                        }

                                        klsProductsCodes.RevocationRight = DefineItemID("RevocationRights", "Title", dtRow["f41"] + "", false, "");

                                        klsProductsCodes.FRNFormula = dtRow["f43"] + "";

                                        if (Global.IsNumeric(dtRow["f44"] + ""))
                                        {
                                            sTemp = dtRow["f44"] + "";
                                            klsProductsCodes.Limits = Convert.ToSingle(sTemp.Replace(".", ","));
                                        }
                                        else
                                            klsProductsCodes.Limits = -1;

                                        sTemp = dtRow["f45"] + "";
                                        tmpArray = sTemp.Split('/');
                                        klsProductsCodes.MonthDays = tmpArray[0] + "";
                                        klsProductsCodes.BaseDays = tmpArray[1] + "";

                                        if (Global.IsNumeric(dtRow["f46"] + ""))
                                        {
                                            sTemp = dtRow["f46"] + "";
                                            klsProductsCodes.LastCoupone = Convert.ToSingle(sTemp.Replace(".", ","));
                                        }
                                        else
                                        {
                                            klsProductsCodes.LastCoupone = -1;
                                            AddLogRec("ISIN = '" + dtRow["f2"] + "'   Wrong Τρέχον Κουπόνι");
                                        }
                                        klsProductsCodes.Aktive = 1;
                                        klsProductsCodes.InfoFlag = 1;
                                        klsProductsCodes.EditRecord();

                                        sTemp = Global.RecalcRiskProfile(iShareCode_ID);
                                        klsProductsCodes = new clsProductsCodes();
                                        klsProductsCodes.Record_ID = iShareCode_ID;
                                        klsProductsCodes.GetRecord();
                                        klsProductsCodes.MIFID_Risk = sTemp;
                                        klsProductsCodes.EditRecord();

                                        SaveComplexReason(iComplexReason_ID);
                                    }
                                    else AddLogRec("ISIN = '" + dtRow["f2"] + "'    Δεν είναι ομόλογο");
                                }
                                else AddLogRec("ISIN = '" + dtRow["f2"] + "     Wrong ISIN = '" + dtRow["f1"]);
                            }
                            else AddLogRec("ISIN = '" + dtRow["f2"] + "      Unknown ISIN");

                            pbImport.Value = pbImport.Value + 1;
                        }
                    }
                    catch (Exception z)
                    {
                        MessageBox.Show(dtRow["f2"] + "  " + z.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }                

                fgWarnings.Redraw = true;
                pbImport.Visible = false;

                if (fgWarnings.Rows.Count > 0) lblResult.Text = "See Log";
                else lblResult.Text = "OK";

                panImport.BackColor = Color.Silver;
                panImport.Left = (this.Width - 654) / 2;
                panImport.Height = 380;
                panImport.Top = (this.Height - 380) / 2;
                this.Refresh();

                Systems = new clsSystem();
                Systems.EditCashTables_LastEdit_Time(2);
            }

            Global.GetProductsList();
            DataFiltering(0);
        }       
        private void lnkFinish_Bonds_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            foreach (DataRow dtRow in Global.dtProducts.Copy().Rows)
            {
                if (Convert.ToInt32(dtRow["Product_ID"]) == 2)
                {
                    klsProductsTitles = new clsProductsTitles();
                    klsProductsTitles.Record_ID = Convert.ToInt32(dtRow["ShareTitles_ID"]);
                    klsProductsTitles.GetRecord();
                    iRatingGroup = Global.DefineRatingGroup(klsProductsTitles.MoodysRating + "", klsProductsTitles.FitchsRating + "", klsProductsTitles.SPRating + "", klsProductsTitles.ICAPRating + "", "");
                    iComplexProduct = DefineComplexProduct(Convert.ToInt32(dtRow["ShareTitles_ID"]), klsProductsTitles.BBG_ComplexProduct.Trim(), klsProductsTitles.BBG_ComplexAttribute.Trim());

                    foundRows = Global.dtCountries.Select("ID = " + klsProductsTitles.CountryRisk_ID);
                    if (foundRows.Length > 0)
                        klsProductsTitles.CountryGroup_ID = Convert.ToInt32(foundRows[0]["CountriesGroup_ID"]);


                    klsProductsTitles.RatingGroup = iRatingGroup;
                    klsProductsTitles.ComplexProduct = iComplexProduct;
                    klsProductsTitles.LastEditDate = DateTime.Now;
                    klsProductsTitles.LastEditUser_ID = Global.User_ID;
                    klsProductsTitles.EditRecord();

                    klsProductsCodes = new clsProductsCodes();
                    klsProductsCodes.Record_ID = Convert.ToInt32(dtRow["ID"]);
                    klsProductsCodes.GetRecord();
                    sTemp = Global.RecalcRiskProfile(Convert.ToInt32(dtRow["ID"]));            // dtRow["ID"] is ShareCodes_ID
                    if (sTemp == "000000") sTemp = "";
                    klsProductsCodes.MIFID_Risk = sTemp;
                    klsProductsCodes.EditRecord();
                }
            }

            fgWarnings.Redraw = false;
            fgWarnings.Rows.Count = 1;

            klsProductsCodes = new clsProductsCodes();
            klsProductsCodes.Product_ID = 2;                  // 2 - Bonds
            klsProductsCodes.GetList_ProductType();
            foreach (DataRow dtRow in klsProductsCodes.List.Rows)
            {
                if ((dtRow["ISIN"] + "").Trim() == "") AddLogRec("Bond RIC = '" + dtRow["Code"] + "'  missing ISIN");
                if ((dtRow["Currency"] + "").Trim() == "") AddLogRec("Bond ISIN = '" + dtRow["ISIN"] + "'  missing Currency");
                if (Convert.ToInt32(dtRow["InfoFlag"]) == 0) AddLogRec("Uninformed ISIN = '" + dtRow["ISIN"] + "'  RIC = '" + dtRow["Code"] + "'   ID = " + dtRow["ID"]);    // ID = ShareCodes.ID
            }

            fgWarnings.Redraw = true;
            panImport.Visible = true;
            this.Refresh();

            this.Cursor = Cursors.Default;
        }
        private void lnkImportFund_MorningStar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            panExtraCommands.Visible = false;

            frmImportData locImportData = new frmImportData();
            locImportData.FileType = 0;                        // 0 - xlsx Excel 2007
            locImportData.Shema = 4;                           // 4 - εισαγωγή Funds
            locImportData.ReadMode = 2;
            locImportData.ShowDialog();
            if (locImportData.Aktion == 1)
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("el-GR");

                panImport.BackColor = Color.Moccasin;
                panImport.Left = (this.Width - 654) / 2;
                panImport.Height = 76;
                panImport.Top = (this.Height - 76) / 2;
                panImport.Visible = true;

                pbImport.Minimum = 0;
                pbImport.Maximum = locImportData.Result.Rows.Count;
                pbImport.Value = 0;

                fgWarnings.Redraw = false;
                fgWarnings.Rows.Count = 1;
                this.Refresh();

                klsProductsCodes = new clsProductsCodes();
                klsProductsCodes.Product_ID = 6;                      // ShareType = 6 - Funds
                klsProductsCodes.EditRecord_ZeroInfoFlag();

                iLogs = 0;

                foreach (DataRow dtRow in locImportData.Result.Rows)
                {
                    try
                    {
                        bError = false;
                        if (dtRow["f4"] + "" == "")
                        {
                            AddLogRec("ISIN " + dtRow["f4"] + " ISIN is mandatory");
                            bError = true;
                        }

                        if (!bError)
                        {
                            iSE_ID = DefineItemID("StockExchanges", "MstarTitle", dtRow["f7"] + "", false, "");
                            iPrimaryShare = ((dtRow["f8"] + "").ToUpper() == "YES") ? 2 : ((dtRow["f8"] + "").ToUpper() == "NO") ? 1 : 0;
                            sCurrency = DefineCurrency("Currencies", "Code_MorningStar", dtRow["f9"] + "");
                            sRiskCurrency = DefineCurrency("Currencies", "Code_MorningStar", dtRow["f9"] + "");

                            sTemp = dtRow["f13"] + "";
                            sProviderName = sTemp.Replace("'", "`");

                            sTemp = dtRow["f29"] + "";
                            sDescriptionEn = sTemp.Replace("'", "`");

                            foundRows = Global.dtProducts.Select("ISIN = '" + dtRow["f4"] + "' AND StockExchange_ID = " + iSE_ID +
                                        " AND Currency = '" + sCurrency + "' AND Aktive >= 1");
                            if (foundRows.Length > 0)
                            {
                                if ((dtRow["f4"] + "").Trim() == (foundRows[0]["ISIN"] + "").Trim())
                                {
                                    if (Convert.ToInt32(foundRows[0]["Product_ID"]) == 6)
                                    {
                                        iShare_ID = Convert.ToInt32(foundRows[0]["Shares_ID"]);
                                        iShareTitle_ID = Convert.ToInt32(foundRows[0]["ShareTitles_ID"]);
                                        iShareCode_ID = Convert.ToInt32(foundRows[0]["ID"]);

                                        klsProducts = new clsProducts();
                                        klsProducts.Record_ID = iShare_ID;
                                        klsProducts.GetRecord();
                                        klsProducts.Product_ID = 6;                         // ShareType = 6 - Funds 
                                        klsProducts.EditRecord();

                                        klsProductsTitles = new clsProductsTitles();
                                        klsProductsTitles.Record_ID = iShareTitle_ID;
                                        klsProductsTitles.GetRecord();
                                        klsProductsTitles.ProductTitle = dtRow["f1"] + "";
                                        klsProductsTitles.StandardTitle = dtRow["f2"] + "";
                                        klsProductsTitles.FundID = dtRow["f3"] + "";
                                        klsProductsTitles.ISIN = dtRow["f4"] + "";
                                        klsProductsTitles.BrandProviderName = dtRow["f12"] + "";
                                        klsProductsTitles.ProviderName = sProviderName;
                                        sTemp = (dtRow["f14"] + "").Trim();
                                        if (sTemp.Length > 0 && sTemp.IndexOf("NULL") < 0) klsProductsTitles.URL = sTemp;
                                        if ((dtRow["f71"] + "") != "" && (dtRow["f71"] + "") != "NULL") klsProductsTitles.CreditRating = dtRow["f71"] + "";
                                        klsProductsTitles.AmountOutstanding = (Global.IsNumeric(dtRow["f72"]) ? Convert.ToDecimal(dtRow["f72"] + "") : 0);
                                        klsProductsTitles.MiFIDInstrumentType = DefineItemID("MiFID_InstrumentType", "Title", dtRow["f15"] + "", false, "");
                                        klsProductsTitles.AIFMD = ((dtRow["f16"] + "") == "No" ? 0 : ((dtRow["f16"] + "") == "Yes" ? 1 : 2));
                                        klsProductsTitles.Leverage = ((dtRow["f17"] + "") == "No" ? 0 : ((dtRow["f17"] + "") == "Yes" ? 1 : 2));
                                        klsProductsTitles.MinimumInvestment = dtRow["f18"] + "";
                                        if (Global.IsNumeric(dtRow["f19"] + ""))
                                        {
                                            klsProductsTitles.SurveyedKIID = Convert.ToSingle(dtRow["f19"]);
                                            klsProductsTitles.SurveyedKIID_Date = dtRow["f20"] + "";
                                        }
                                        if (Global.IsNumeric(dtRow["f21"] + ""))
                                        {
                                            klsProductsTitles.OngoingKIID = Convert.ToSingle(dtRow["f21"]);
                                            klsProductsTitles.OngoingKIID_Date = dtRow["f22"] + "";
                                        }
                                        klsProductsTitles.RatingOverall = dtRow["f23"] + "";
                                        klsProductsTitles.RatingDate = dtRow["f24"] + "";
                                        klsProductsTitles.GlobalBroad = DefineItemID("GlobalBroadCategories", "Title", dtRow["f25"] + "", false, "");
                                        klsProductsTitles.CategoryMorningStar = DefineItemID("FundCategoriesMorningStar", "Title", dtRow["f26"] + "", true, ""); ;
                                        klsProductsTitles.Benchmark = DefineItemID("Benchmarks", "Title", dtRow["f27"] + "", true, "");
                                        klsProductsTitles.CountryRisk_ID = DefineCountryID("Countries", "Title_MorningStar", dtRow["f28"] + "", false);
                                        klsProductsTitles.RiskCurr = sRiskCurrency;
                                        klsProductsTitles.DescriptionEn = sDescriptionEn;
                                        klsProductsTitles.DescriptionGr = dtRow["f30"] + "";
                                        klsProductsTitles.InvestmentType = DefineItemID("InvestmentTypes", "Title", dtRow["f33"] + "", false, "");
                                        klsProductsTitles.LegalStructure_ID = DefineItemID("FundLegalStructures", "Title", dtRow["f34"] + "", false, "");
                                        klsProductsTitles.InceptionDate = dtRow["f35"] + "";
                                        klsProductsTitles.Country_ID = DefineCountryID("Countries", "Title_MorningStar", dtRow["f36"] + "", false);
                                        klsProductsTitles.Institutional = dtRow["f37"] + "";
                                        klsProductsTitles.ActivelyManaged = DefineItemID("TargetMarketList1", "Title", dtRow["f38"] + "", false, "");
                                        klsProductsTitles.ReplicationMethod = dtRow["f39"] + "";
                                        klsProductsTitles.SwapBasedETF = dtRow["f40"] + "";
                                        klsProductsTitles.CountryRegistered = dtRow["f41"] + "";
                                        klsProductsTitles.EstimatedKIID = dtRow["f42"] + "";
                                        klsProductsTitles.EstimatedKIID_Date = dtRow["f43"] + "" == "" ? "" : Convert.ToDateTime(dtRow["f43"] + "").ToString("dd/MM/yyyy");
                                        klsProductsTitles.SurveyedKIID_History = dtRow["f44"] + "";
                                        if (Global.IsNumeric(dtRow["f45"] + "")) klsProductsTitles.SRRIValues = dtRow["f45"] + "";
                                        klsProductsTitles.SRRIValues_Date = dtRow["f46"] + "";
                                        klsProductsTitles.ManagmentFee = dtRow["f47"] + "";
                                        klsProductsTitles.ManagmentFee_Date = dtRow["f48"] + "";
                                        klsProductsTitles.PerformanceFee = dtRow["f49"] + "";
                                        klsProductsTitles.PerformanceFee_Date = dtRow["f50"] + "";
                                        klsProductsTitles.InvestType_Retail = DefineItemID("TargetMarketList1", "Title", dtRow["f51"] + "", false, "");
                                        klsProductsTitles.InvestType_Prof = DefineItemID("TargetMarketList1", "Title", dtRow["f52"] + "", false, "");
                                        klsProductsTitles.InvestType_Eligible = DefineItemID("TargetMarketList1", "Title", dtRow["f53"] + "", false, "");
                                        klsProductsTitles.Expertise_Basic = DefineItemID("TargetMarketList1", "Title", dtRow["f54"] + "", false, "");
                                        klsProductsTitles.Expertise_Informed = DefineItemID("TargetMarketList1", "Title", dtRow["f55"] + "", false, "");
                                        klsProductsTitles.Expertise_Advanced = DefineItemID("TargetMarketList1", "Title", dtRow["f56"] + "", false, "");
                                        klsProductsTitles.RecHoldingPeriod = dtRow["f57"] + "";
                                        klsProductsTitles.RetProfile_Preserv = DefineItemID("TargetMarketList1", "Title", dtRow["f58"] + "", false, "");
                                        klsProductsTitles.RetProfile_Income = DefineItemID("TargetMarketList1", "Title", dtRow["f59"] + "", false, "");
                                        klsProductsTitles.RetProfile_Growth = DefineItemID("TargetMarketList1", "Title", dtRow["f60"] + "", false, "");
                                        klsProductsTitles.Distrib_ExecOnly = DefineItemID("TargetMarketList2", "Title", dtRow["f61"] + "", false, "");
                                        klsProductsTitles.Distrib_Advice = DefineItemID("TargetMarketList2", "Title", dtRow["f62"] + "", false, "");
                                        klsProductsTitles.Distrib_PortfolioManagment = DefineItemID("TargetMarketList2", "Title", dtRow["f63"] + "", false, "");
                                        klsProductsTitles.CapitalLoss_None = DefineItemID("TargetMarketList1", "Title", dtRow["f64"] + "", false, "");
                                        klsProductsTitles.CapitalLoss_Limited = DefineItemID("TargetMarketList1", "Title", dtRow["f65"] + "", false, "");
                                        klsProductsTitles.CapitalLoss_NoGuarantee = DefineItemID("TargetMarketList1", "Title", dtRow["f66"] + "", false, "");
                                        klsProductsTitles.CapitalLoss_BeyondInitial = DefineItemID("TargetMarketList1", "Title", dtRow["f67"] + "", false, "");
                                        klsProductsTitles.CapitalLoss_Level = DefineItemID("TargetMarketList1", "Title", dtRow["f68"] + "", false, "");
                                        klsProductsTitles.CountryAvailable = dtRow["f69"] + "";

                                        klsProductsTitles.LastEditDate = DateTime.Now;
                                        klsProductsTitles.LastEditUser_ID = Global.User_ID;
                                        klsProductsTitles.EditRecord();

                                        klsProductsCodes = new clsProductsCodes();
                                        klsProductsCodes.Record_ID = iShareCode_ID;
                                        klsProductsCodes.GetRecord();
                                        klsProductsCodes.CodeTitle = dtRow["f1"] + "";
                                        klsProductsCodes.ISIN = dtRow["f4"] + "";
                                        klsProductsCodes.SecID = dtRow["f5"] + "";
                                        klsProductsCodes.Code3 = dtRow["f6"] + "";
                                        klsProductsCodes.PrimaryShare = iPrimaryShare;
                                        klsProductsCodes.StockExchange_ID = iSE_ID;
                                        klsProductsCodes.Curr = sCurrency;
                                        klsProductsCodes.CurrencyHedge = (dtRow["f10"] + "" == "Fully Hedged" ? 1 : 0);
                                        klsProductsCodes.CurrencyHedge2 = DefineCurrency("Currencies", "Code_MorningStar", dtRow["f11"] + "");
                                        klsProductsCodes.DistributionStatus = dtRow["f31"] + "";
                                        ;
                                        switch (dtRow["f32"] + "")
                                        {
                                            case "Annually":
                                                klsProductsCodes.FrequencyClipping = 1;
                                                break;
                                            case "Weekly":
                                                klsProductsCodes.FrequencyClipping = 2;
                                                break;
                                            case "Monthly":
                                                klsProductsCodes.FrequencyClipping = 3;
                                                break;
                                            case "Quarterly":
                                                klsProductsCodes.FrequencyClipping = 4;
                                                break;
                                            case "Yearly":
                                                klsProductsCodes.FrequencyClipping = 5;
                                                break;
                                            case "Semi-Annually":
                                                klsProductsCodes.FrequencyClipping = 6;
                                                break;
                                            case "None":
                                                klsProductsCodes.FrequencyClipping = 7;
                                                break;
                                            default:
                                                klsProductsCodes.FrequencyClipping = 0;
                                                break;
                                        }

                                        klsProductsCodes.Aktive = 1;
                                        klsProductsCodes.InfoFlag = 1;
                                        klsProductsCodes.EditRecord();

                                        sTemp = Global.RecalcRiskProfile(iShareCode_ID);
                                        klsProductsCodes = new clsProductsCodes();
                                        klsProductsCodes.Record_ID = iShareCode_ID;
                                        klsProductsCodes.GetRecord();
                                        sOldRiskProfile = klsProductsCodes.MIFID_Risk;
                                        klsProductsCodes.MIFID_Risk = sTemp;
                                        klsProductsCodes.EditRecord();


                                        if (sOldRiskProfile != sTemp)
                                        {
                                            ProductsLogger = new clsProductsLogger();
                                            ProductsLogger.ShareCodes_ID = iShareCode_ID;
                                            ProductsLogger.OldMIFID_Risk = sOldRiskProfile;
                                            ProductsLogger.NewMIFID_Risk = sTemp;
                                            ProductsLogger.EditDate = DateTime.Now;
                                            ProductsLogger.EditMethod = 1;                               // 1 - Enimerosi, 2- Edit
                                            ProductsLogger.InsertRecord();
                                        }
                                    }
                                    else AddLogRec("ISIN " + dtRow["f4"] + "     ISIN = '" + dtRow["f3"] + "'.    Δεν είναι Fund");
                                }
                                else AddLogRec("ISIN " + dtRow["f4"] + "     Wrong ISIN = '" + dtRow["f3"]);
                            }
                            else 
                               AddLogRec("ISIN " + dtRow["f4"] + " Currency = " + sCurrency + " SecID = " + dtRow["f5"] + "   Title " + dtRow["f1"] + "    Unknown ISIN + Currency ");

                            pbImport.Value = pbImport.Value + 1;
                        }
                    }
                    catch (Exception z)
                    {
                        MessageBox.Show(dtRow["f1"] + "  " + z.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }                

                fgWarnings.Redraw = true;
                pbImport.Visible = false;

                if (fgWarnings.Rows.Count > 0) lblResult.Text = "See Log";
                else lblResult.Text = "OK";

                panImport.BackColor = Color.Silver;
                panImport.Left = (this.Width - 654) / 2;
                panImport.Height = 380;
                panImport.Top = (this.Height - 380) / 2;
                this.Refresh();

                Systems = new clsSystem();
                Systems.EditCashTables_LastEdit_Time(2);
            }

            Global.GetProductsList();
            DataFiltering(0);
        }

        private void lnkFinish_Funds_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            foreach (DataRow dtRow in Global.dtProducts.Copy().Rows)
            {
                if (Convert.ToInt32(dtRow["Product_ID"]) == 6)
                {
                    klsProductsTitles = new clsProductsTitles();
                    klsProductsTitles.Record_ID = Convert.ToInt32(dtRow["ShareTitles_ID"]);
                    klsProductsTitles.GetRecord();

                    foundRows = Global.dtCountries.Select("ID = " + klsProductsTitles.CountryRisk_ID);
                    if (foundRows.Length > 0)
                        klsProductsTitles.CountryGroup_ID = Convert.ToInt32(foundRows[0]["CountriesGroup_ID"]);

                    klsProductsTitles.RatingGroup = Global.DefineRatingGroup("", "", "", "", klsProductsTitles.CreditRating);

                    klsProductsTitles.LastEditDate = DateTime.Now;
                    klsProductsTitles.LastEditUser_ID = Global.User_ID;
                    klsProductsTitles.EditRecord();

                    klsProductsCodes = new clsProductsCodes();
                    klsProductsCodes.Record_ID = Convert.ToInt32(dtRow["ID"]);
                    klsProductsCodes.GetRecord();
                    //if (klsProductsCodes.ISIN == "LU0171296279")
                    //    i = i;
                    sTemp = Global.RecalcRiskProfile(Convert.ToInt32(dtRow["ID"]));
                    if (sTemp == "000000") sTemp = "";
                    klsProductsCodes.MIFID_Risk = sTemp;
                    klsProductsCodes.EditRecord();
                }
            }

            fgWarnings.Redraw = false;
            fgWarnings.Rows.Count = 1;

            klsProductsCodes = new clsProductsCodes();
            klsProductsCodes.Product_ID = 6;                  // 6 - Funds
            klsProductsCodes.GetList_ProductType();
            foreach (DataRow dtRow in klsProductsCodes.List.Rows)
            {
                if ((dtRow["ISIN"] + "").Trim() == "") AddLogRec("Fund RIC = '" + dtRow["Code"] + "'  missing ISIN");
                if ((dtRow["Currency"] + "").Trim() == "") AddLogRec("Fund ISIN = '" + dtRow["ISIN"] + "'  missing Currency");
                if (Convert.ToInt32(dtRow["InfoFlag"]) == 0) 
                    AddLogRec("Uninformed ISIN = '" + dtRow["ISIN"] + "'  RIC = '" + dtRow["Code"] + "'   ID = " + dtRow["ID"]);  // ID = ShareCodes.ID
            }

            fgWarnings.Redraw = true;
            panImport.Visible = true;
            this.Refresh();

            this.Cursor = Cursors.Default;
        }
        private void lnkExport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }


        private void picCloseImport_Click(object sender, EventArgs e)
        {
            panImport.Visible = false;
        }

        #region --- Merge functions -----------------------------------------------------------------------------
        private void lnkMerge_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            fgMerge.Rows.Count = 1;
            panMerge.Visible = true;
            panExtraCommands.Visible = false;
        }

        private void tsbAddMerge_Click(object sender, EventArgs e)
        {
            fgMerge.AddItem(fgList[fgList.Row, 0] + "\t" + fgList[fgList.Row, 1]);
            if (fgMerge.Rows.Count > 2) btnOKMerge.Enabled = true;
            else btnOKMerge.Enabled = false;
        }

        private void tsbDelMerge_Click(object sender, EventArgs e)
        {
            fgMerge.RemoveItem(fgMerge.Row);
            if (fgMerge.Rows.Count > 2) btnOKMerge.Enabled = true;
            else btnOKMerge.Enabled = false;
        }
        private void btnOKMerge_Click(object sender, EventArgs e)
        {
            clsProductsCodes ProductsCode = new clsProductsCodes();
            for (i = 2; i <= fgMerge.Rows.Count - 1; i++)
            {
                ProductsCode = new clsProductsCodes();
                ProductsCode.OldShare_ID = Convert.ToInt32(fgMerge[i, 1]);
                ProductsCode.NewShare_ID = Convert.ToInt32(fgMerge[1, 1]);
                ProductsCode.EditRecord_Shares_ID();
            }

            clsSystem System = new clsSystem();
            System.EditCashTables_LastEdit_Time(2);

            Global.GetProductsList();

            DataFiltering(0);
            panMerge.Visible = false;
        }
        private void picCloseMerge_Click(object sender, EventArgs e)
        {
            panMerge.Visible = false;
        }
        #endregion -----------------------------------------------------------------------------------------
        private void SaveComplexReason(int iComplexReason_ID)
        {
            if (iComplexReason_ID != 0) {
                bFound = false;
                clsProductsTitles klsProductsTitles_ComplexReasons = new clsProductsTitles();
                klsProductsTitles_ComplexReasons.Record_ID = iShareTitle_ID;
                klsProductsTitles_ComplexReasons.GetComplexReasons_List();
                foreach (DataRow dtRow1 in klsProductsTitles_ComplexReasons.ComplexReasons.Rows) {
                    if (Convert.ToInt32(dtRow1["ComplexReason_ID"]) == iComplexReason_ID)
                        bFound = true;
                }
            }

            if (!bFound) {
                clsShareTitles_ComplexReasons ShareTitles_ComplexReasons = new clsShareTitles_ComplexReasons();
                ShareTitles_ComplexReasons.ShareTitles_ID = iShareTitle_ID;
                ShareTitles_ComplexReasons.ComplexReason_ID = iComplexReason_ID;
                ShareTitles_ComplexReasons.InsertRecord();
            }
        }  
        private int DefineComplexProduct(int iShareTitles_ID, string sBBG_ComplexProduct, string sBBG_ComplexAttribute)
        {
            int iComplex = 1;

            if (sBBG_ComplexProduct == "Y") iComplex = 2;
            else {
                clsProductsTitles klsProductsTitle_ComplexReasons = new clsProductsTitles();
                klsProductsTitle_ComplexReasons.Record_ID = iShareTitles_ID;
                klsProductsTitle_ComplexReasons.GetComplexReasons_List();
                if (klsProductsTitle_ComplexReasons.List.Rows.Count > 0) iComplex = 2;
            }

            return iComplex;
        }
        private void AddLogRec(string sMessage)
        {
            iLogs = iLogs + 1;
            fgWarnings.AddItem(iLogs + "\t" + sMessage);
        }
        private int DefineItemID(string sTableName, string sField, string sItem, bool bAutoAdd, string sExtra) {

            int iItem = 0;

            sItem = sItem + "";
            try {
                if (sItem != "")
                {
                    sTemp = "SELECT * FROM " + sTableName + " WHERE " + sField + " = '" + sItem + "'" + sExtra;
                    Systems = new clsSystem();
                    Systems.GetRecord(sTableName, sField, sItem);
                    foreach (DataRow dtRow in Systems.List.Rows)
                        iItem = Convert.ToInt32(dtRow["ID"]);

                    if (iItem == 0)
                    {
                        if (bAutoAdd)
                        {
                            sSQL = "INSERT INTO " + sTableName + " (" + sField + ") VALUES ('" + sItem + "')";
                            Systems = new clsSystem();
                            iItem = Systems.ExecSQL(sSQL);

                            if (sItem != "")
                                AddLogRec("Unknown value " + sTableName + "." + sField + " = " + sItem);
                        }
                    }
                }
            }
            catch (Exception z) { MessageBox.Show(z.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

            return iItem;
        }
        private int DefineCountryID(string sTableName, string sField, string sItem, bool bAutoAdd)
        {
            int iItem = 0;

            try
            {
                if (sItem != "") {
                    sTemp = "SELECT * FROM " + sTableName + " WHERE " + sField + " = '" + sItem + "' OR Title_Alias = '" + sItem + "'";
                    Systems = new clsSystem();
                    Systems.GetRecord(sTableName, sField, sItem);
                    foreach (DataRow dtRow in Systems.List.Rows)
                        iItem = Convert.ToInt32(dtRow["ID"]);

                    if (iItem == 0)
                    {
                        if (bAutoAdd)
                        {
                            sSQL = "INSERT INTO " + sTableName + " (" + sField + ") VALUES ('" + sItem + "')";
                            Systems = new clsSystem();
                            iItem = Systems.ExecSQL(sSQL);

                            if (sItem != "")
                                AddLogRec("Unknown value " + sTableName + "." + sField + " = " + sItem);
                        }
                    }
                }
            }
            catch (Exception z) { MessageBox.Show(z.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }

            return iItem;
        }
        private string DefineCurrency(string sTableName, string sField, string sItem) {
            string sFind = "";
            if (sItem != "") {

                foundRows = Global.dtCurrencies.Select(sField + " = '" + sItem + "'");
                if (foundRows.Length > 0) sFind = foundRows[0]["Code_Convert"]+"";
            }
            return sFind;
        }
        
        public void close_me(object sender, EventArgs e)
        {
             switch (iProduct_ID)
            {
                case 1:
                    iLastAktion = Convert.ToInt32(ucShares.lblFlagEdit.Text);
                    break;
                case 2:
                    iLastAktion = Convert.ToInt32(ucBonds.lblFlagEdit.Text);
                    break;
                case 3:
                    iLastAktion = Convert.ToInt32(ucRates.lblFlagEdit.Text);
                    break;
                case 4:
                    iLastAktion = Convert.ToInt32(ucETFs.lblFlagEdit.Text);
                    break;
                case 5:
                    iLastAktion = Convert.ToInt32(ucIndexes.lblFlagEdit.Text);
                    break;
                case 6:
                    iLastAktion = Convert.ToInt32(ucFunds.lblFlagEdit.Text);
                    break;
            }
            DataFiltering(iLastAktion);
        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
