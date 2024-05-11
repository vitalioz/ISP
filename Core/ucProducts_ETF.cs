using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

namespace Core
{
    public partial class ucProducts_ETF : UserControl
    {
        DataView dtView;
        int i, iMode, iShare_ID, iShareTitle_ID, iShareCode_ID, iAction, iOldCode_ID, iProduct_ID, iSector_ID, iSharesTitlesCodes_ID, iActionMode, iRightsLevel;
        string sTemp, sTitle, sISIN, sMIFID_Risk;
        string[] sDividendDistributionFrequency = { "", "Annually", "Weekly", "Monthly", "Quarterly", "Yearly", "Semi-Annually", "None" };
        bool bEditProductType;
        CellStyle csAktive, csCancel;
        clsProductsTitles klsProductTitle = new clsProductsTitles();
        clsCashTables CashTable = new clsCashTables();
        public ucProducts_ETF()
        {
            InitializeComponent();

            panISIN.Left = 104;
            panISIN.Top = 87;
            iProduct_ID = 4;

            lblISIN_Warning.Text = "";
            lblNewISIN_Warning.Text = "";
        }

        private void ucProducts_ETF_Load(object sender, EventArgs e)
        {
            bEditProductType = false;

            //------- fgCodes ----------------------------
            fgCodes.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgCodes.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgCodes.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.fgCodes_CellChanged);
            fgCodes.MouseDown += new MouseEventHandler(fgCodes_MouseDown);
        }
        protected override void OnResize(EventArgs e)
        {
            grpDetails.Width = this.Width - 10;
            grpDetails.Height = this.Height - 34;

            tcData.Width = grpDetails.Width - 14;

            fgCodes.Width = grpDetails.Width - 12;
            fgCodes.Height = grpDetails.Height - 586;
        }
        public void ShowRecord(int iLocShare_ID, int iLocShareTitle_ID, int iLocShareCode_ID, int iRightsLevel)
        {
            StartInit();
            txtISIN.Enabled = false;
            btnEditISIN.Visible = true;

            iProduct_ID = 4;
            iSharesTitlesCodes_ID = 0;
            if (iLocShare_ID == 0)
                if (iLocShareCode_ID != 0) iLocShare_ID = klsProductTitle.GetRecord_ID(iLocShareCode_ID);

            iShare_ID = iLocShare_ID;

            clsProductsTitlesCodes klsProductTitleCode = new clsProductsTitlesCodes();
            klsProductTitleCode.Share_ID = iShare_ID;
            klsProductTitleCode.Today = DateTime.Now;
            klsProductTitleCode.GetRecord_Date();
            iSharesTitlesCodes_ID = klsProductTitleCode.Record_ID;
            iLocShareTitle_ID = klsProductTitleCode.ShareTitle_ID;

            clsProducts klsProduct = new clsProducts();
            klsProduct.Record_ID = iShare_ID;
            klsProduct.GetRecord();
            iProduct_ID = klsProduct.Product_ID;
            iShareTitle_ID = iLocShareTitle_ID;
            klsProductTitle.Record_ID = iShareTitle_ID;
            klsProductTitle.GetRecord();

            lblLastEdit.Text = "Last Edit :" + Convert.ToDateTime(klsProductTitle.LastEditDate).ToString("dd/MM/yyyy") + " " + klsProductTitle.LastEditUserName;

            ShowTitleData();
            ShowCodesList();

            if (iRightsLevel > 1)
            {
                switch (iMode)
                {
                    case 1:                                      // 1 - from ProductsList
                        btnEditISIN.Visible = true;
                        tsbSave.Enabled = false;
                        toolCode.Enabled = false;
                        break;
                    case 2:                                      // 2 - from ProductData
                        btnEditISIN.Visible = false;
                        tsbSave.Enabled = true;
                        toolCode.Enabled = false;
                        break;
                    case 3:                                      // 3 - from ProductsWishList (RecommendedList)
                        btnEditISIN.Visible = false;
                        tsbSave.Enabled = true;
                        toolCode.Enabled = true;
                        break;
                }
            }
            else
            {
                tsbSave.Enabled = false;
                toolCode.Enabled = false;
            }

            iAction = 1;                        // 1 - EDIT Mode
        }
        public void AddRecord()
        {
            StartInit();
            txtISIN.Enabled = true;
            btnEditISIN.Visible = false;

            iProduct_ID = 4;                    // 4 - ETF

            clsProductsTitles klsProductTitle = new clsProductsTitles();
            cmbProductType.SelectedValue = iProduct_ID;
            ShowTitleData();
            fgCodes.Rows.Count = 1;

            tsbSave.Enabled = true;
            toolCode.Enabled = true;

            ComponentsOnOff(true);
            iAction = 0;                       // 0 - ADD Mode
            txtTitle.Focus();
        }
        public void EditRecord()
        {
            tsbSave.Enabled = true;
            toolCode.Enabled = true;
            picKey.Visible = true;
            ComponentsOnOff(true);
            iAction = 1;                       // 1 - EDIT Mode
            txtTitle.Focus();
        }
        public void StartInit()
        {
            bEditProductType = false;
            cmbProductType.Enabled = false;
            ComponentsOnOff(false);

            cmbProductType.DataSource = Global.dtProductTypes.Copy();
            cmbProductType.DisplayMember = "Title";
            cmbProductType.ValueMember = "ID";
            cmbProductType.SelectedValue = iProduct_ID;

            //-------------- Define Countries Groups List ------------------
            cmbCountryGroup.DataSource = Global.dtCountriesGroups.Copy();
            cmbCountryGroup.DisplayMember = "Title";
            cmbCountryGroup.ValueMember = "ID";

            //-------------- Define Countries List ------------------
            dtView = Global.dtCountries.Copy().DefaultView;
            dtView.RowFilter = "Tipos = 1";
            cmbCountry.DataSource = dtView;
            cmbCountry.DisplayMember = "Title";
            cmbCountry.ValueMember = "ID";

            //-------------- Define Investment Areas List ------------------
            cmbCountryRisk.DataSource = Global.dtCountries.Copy();
            cmbCountryRisk.DisplayMember = "Title";
            cmbCountryRisk.ValueMember = "ID";

            //-------------- Define CountryAction List ------------------
            cmbCountryAction.DataSource = Global.dtCountries.Copy();
            cmbCountryAction.DisplayMember = "Title";
            cmbCountryAction.ValueMember = "ID";

            //-------------- Define Currencies List ------------------
            cmbCurrency.DataSource = Global.dtCurrencies.Copy();
            cmbCurrency.DisplayMember = "Title";
            cmbCurrency.ValueMember = "ID";

            //-------------- Define cmbCurrencyHedge2 List ------------------
            cmbCurrencyHedge2.DataSource = Global.dtCurrencies.Copy();
            cmbCurrencyHedge2.DisplayMember = "Title";
            cmbCurrencyHedge2.ValueMember = "ID";

            //-------------- Define RiskCurrencies List ------------------
            cmbRiskCurr.DataSource = Global.dtCurrencies.Copy();
            cmbRiskCurr.DisplayMember = "Title";
            cmbRiskCurr.ValueMember = "ID";

            //-------------- Define Managment Categories List ------------------  
            cmbHFCategory.DataSource = Global.dtHFCategories.Copy();
            cmbHFCategory.DisplayMember = "Title";
            cmbHFCategory.ValueMember = "ID";

            //-------------- Define Product Categories List ------------------
            dtView = Global.dtProductsCategories.Copy().DefaultView;
            dtView.RowFilter = "Product_ID = 4";
            cmbProductCategory.DataSource = dtView;
            cmbProductCategory.DisplayMember = "Title";
            cmbProductCategory.ValueMember = "ID";

            //-------------- Define StockExcahnges  List ------------------
            cmbStockExchanges.DataSource = Global.dtStockExchanges.Copy();
            cmbStockExchanges.DisplayMember = "Code";
            cmbStockExchanges.ValueMember = "ID";

            //-------------- Define MiFID InstrumentType --------------------
            clsSystem System = new clsSystem();
            System.GetMiFIDInstrumentTypes();
            cmbMiFIDInstrumentType.DataSource = System.List.Copy();
            cmbMiFIDInstrumentType.DisplayMember = "Title";
            cmbMiFIDInstrumentType.ValueMember = "ID";

            //-------------- Define Global Broad -----------------------------
            System = new clsSystem();
            System.GetList_GlobalBroadCategories();
            cmbGlobalBroad.DataSource = System.List.Copy();
            cmbGlobalBroad.DisplayMember = "Title";
            cmbGlobalBroad.ValueMember = "ID";

            //-------------- Define Benchmarks List ---------------------------
            System = new clsSystem();
            System.GetBenchmarks();
            cmbBenchmarks.DataSource = System.List.Copy();
            cmbBenchmarks.DisplayMember = "Title";
            cmbBenchmarks.ValueMember = "ID";

            //-------------- Define Category MorningStar List ------------------
            System = new clsSystem();
            System.GetFundCategoriesMorningStar();
            cmbMorningStar.DataSource = System.List.Copy();
            cmbMorningStar.DisplayMember = "Title";
            cmbMorningStar.ValueMember = "ID";

            //-------------- Define Moodys Ratings List ------------------   
            dtView = Global.dtRatingCodes.Copy().DefaultView;
            dtView.RowFilter = "RatingAgency_ID = 0 OR RatingAgency_ID = 5";
            cmbCreditRating.DataSource = dtView;
            cmbCreditRating.DisplayMember = "Code";
            cmbCreditRating.ValueMember = "ID";

            chkShowAktive.Checked = false;           

            csAktive = fgCodes.Styles.Add("Aktive");
            csAktive.ForeColor = Color.Black;
            csCancel = fgCodes.Styles.Add("Cancelled");
            csCancel.ForeColor = Color.Red;

            cmbDistrib_ExecOnly.DataSource = Global.dtTargetMarketList2.Copy();
            cmbDistrib_ExecOnly.DisplayMember = "Title";
            cmbDistrib_ExecOnly.ValueMember = "ID";

            cmbDistrib_Advice.DataSource = Global.dtTargetMarketList2.Copy();
            cmbDistrib_Advice.DisplayMember = "Title";
            cmbDistrib_Advice.ValueMember = "ID";

            cmbDistrib_PortfolioManagment.DataSource = Global.dtTargetMarketList2.Copy();
            cmbDistrib_PortfolioManagment.DisplayMember = "Title";
            cmbDistrib_PortfolioManagment.ValueMember = "ID";

            //------- fgCodes ----------------------------
            fgCodes.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            fgCodes.Styles.ParseString(Global.GridStyle);
            fgCodes.DrawMode = DrawModeEnum.OwnerDraw;
            fgCodes.ShowCellLabels = true;
        }
        private void ShowTitleData()
        {
            cmbProductType.Text = klsProductTitle.ProductType;
            txtStandardTitle.Text = klsProductTitle.StandardTitle;
            txtTitle.Text = klsProductTitle.ProductTitle;
            txtISIN.Text = klsProductTitle.ISIN;
            txtProviderName.Text = klsProductTitle.ProviderName;
            txtBrandProviderName.Text = klsProductTitle.BrandProviderName;
            cmbLegalStructure.SelectedValue = klsProductTitle.LegalStructure_ID;
            cmbProductCategory.SelectedValue = klsProductTitle.ProductCategory;
            cmbHFCategory.SelectedValue = klsProductTitle.HFCategory;
            cmbMiFIDInstrumentType.SelectedValue = klsProductTitle.MiFIDInstrumentType;
            cmbAIFMD.SelectedIndex = klsProductTitle.AIFMD;
            txtMinInvestment.Text = klsProductTitle.MinimumInvestment;
            cmbGlobalBroad.SelectedValue = klsProductTitle.GlobalBroad;
            cmbCountry.SelectedValue = klsProductTitle.Country_ID;
            cmbCountryGroup.SelectedValue = klsProductTitle.CountryGroup_ID;
            iSector_ID = klsProductTitle.Sector_ID;
            cmbMorningStar.SelectedValue = klsProductTitle.CategoryMorningStar;
            cmbCountryRisk.SelectedValue = klsProductTitle.CountryRisk_ID;
            cmbBenchmarks.SelectedValue = klsProductTitle.Benchmark;
            cmbRiskCurr.Text = klsProductTitle.RiskCurr;
            txtDescriptionEn.Text = klsProductTitle.DescriptionEn;
            txtKIIDObjective.Text = klsProductTitle.DescriptionGr;
            cmbLeverage.SelectedIndex = klsProductTitle.Leverage;
            txtURL.Text = klsProductTitle.URL;
            txtIR_URL.Text = klsProductTitle.IR_URL;
            chkNonTradeable.Checked = klsProductTitle.NotTradeable == 1 ? true : false;
            cmbCreditRating.Text = klsProductTitle.CreditRating;
            cmbRatingGroup.SelectedIndex = klsProductTitle.RatingGroup;
            txtTotalAUM.Text = klsProductTitle.TotalAUM.ToString();
            dTotalAUM.Text = klsProductTitle.TotalAUMDate;
            txtMaturity.Text = klsProductTitle.Maturity.ToString();
            dMaturity.Text = klsProductTitle.MaturityDate;
            cmbInvestmentType.SelectedValue = klsProductTitle.InvestmentType;
            txtFundID.Text = klsProductTitle.FundID;
            //dInceptionDate.Value = IIf(IsDate(klsProductTitle.InceptionDate), klsProductTitle.InceptionDate, "01/01/1900");
            txtInstitutional.Text = klsProductTitle.Institutional;
            cmbActivelyManaged.SelectedIndex = klsProductTitle.ActivelyManaged;
            txtReplicationMethod.Text = klsProductTitle.ReplicationMethod;
            txtSwapBasedETF.Text = klsProductTitle.SwapBasedETF;
            txtEstimatedKIID.Text = klsProductTitle.EstimatedKIID;
            if (klsProductTitle.EstimatedKIID_Date != "")
            {
                if (Convert.ToDateTime(klsProductTitle.EstimatedKIID_Date) != Convert.ToDateTime("1900/01/01"))
                {
                    dEstimatedKIID.Value = Convert.ToDateTime(klsProductTitle.EstimatedKIID_Date);
                    dEstimatedKIID.CustomFormat = "dd/MM/yyyy";
                }
                else
                {
                    dEstimatedKIID.CustomFormat = "          ";
                    dEstimatedKIID.Format = DateTimePickerFormat.Custom;
                }
            }
            else  {
                dEstimatedKIID.CustomFormat = "          ";
                dEstimatedKIID.Format = DateTimePickerFormat.Custom;
            }
                        
            txtSurveyedKIID.Text = klsProductTitle.SurveyedKIID.ToString();
            if (Global.IsDate(klsProductTitle.SurveyedKIID_Date)) {
                if (Convert.ToDateTime(klsProductTitle.SurveyedKIID_Date).Date != Convert.ToDateTime("1900/01/01").Date) {
                    dSurveyedKIID.Value = Convert.ToDateTime(klsProductTitle.SurveyedKIID_Date).Date;
                    dSurveyedKIID.CustomFormat = "dd/MM/yyyy";
                }
                else {
                    dSurveyedKIID.CustomFormat = "          ";
                    dSurveyedKIID.Format = DateTimePickerFormat.Custom;
                }
            }
            else {
                dSurveyedKIID.CustomFormat = "          ";
                dSurveyedKIID.Format = DateTimePickerFormat.Custom;
            }

            txtOngoingKIID.Text = klsProductTitle.OngoingKIID.ToString();
            if (Global.IsDate(klsProductTitle.OngoingKIID_Date)) {
                if (Convert.ToDateTime(klsProductTitle.OngoingKIID_Date).Date != Convert.ToDateTime("1900/01/01").Date) {
                    dOngoingKIID.Value = Convert.ToDateTime(klsProductTitle.OngoingKIID_Date).Date;
                    dOngoingKIID.CustomFormat = "dd/MM/yyyy";
                }
                else {
                    dOngoingKIID.CustomFormat = "          ";
                    dOngoingKIID.Format = DateTimePickerFormat.Custom;
                }
            }
            else {
                dOngoingKIID.CustomFormat = "          ";
                dOngoingKIID.Format = DateTimePickerFormat.Custom;
            }

            txtRatingOverall.Text = klsProductTitle.RatingOverall;
            if (Global.IsDate(klsProductTitle.RatingDate)) {
                if (Convert.ToDateTime(klsProductTitle.RatingDate).Date != Convert.ToDateTime("1900/01/01").Date) { 
                    dRatingDate.Value = Convert.ToDateTime(klsProductTitle.RatingDate).Date;
                    dRatingDate.CustomFormat = "dd/MM/yyyy";
                 }
                else {
                    dRatingDate.CustomFormat = "          ";
                    dRatingDate.Format = DateTimePickerFormat.Custom;
                }
            }
            else {
                dRatingDate.CustomFormat = "          ";
                dRatingDate.Format = DateTimePickerFormat.Custom;
            }

            txtSurveyedKIIDHistory.Text = klsProductTitle.SurveyedKIID_History;
            txtSRRIValues.Text = klsProductTitle.SRRIValues;
            if (Global.IsDate(klsProductTitle.SRRIValues_Date)) {
                if (Convert.ToDateTime(klsProductTitle.SRRIValues_Date).Date != Convert.ToDateTime("1900/01/01").Date)  {
                    dSRRIValue.Value = Convert.ToDateTime(klsProductTitle.SRRIValues_Date).Date;
                    dSRRIValue.CustomFormat = "dd/MM/yyyy";
                }
                else
                {
                    dSRRIValue.CustomFormat = "          ";
                    dSRRIValue.Format = DateTimePickerFormat.Custom;
                }
            }
            else {
                dSRRIValue.CustomFormat = "          ";
                dSRRIValue.Format = DateTimePickerFormat.Custom;
            }

            txtManagmentFee.Text = klsProductTitle.ManagmentFee;
            if (Global.IsDate(klsProductTitle.ManagmentFee_Date)) {
                if (Convert.ToDateTime(klsProductTitle.ManagmentFee_Date).Date != Convert.ToDateTime("1900/01/01").Date) {
                    dManagmentFee.Value = Convert.ToDateTime(klsProductTitle.ManagmentFee_Date).Date;
                    dManagmentFee.CustomFormat = "dd/MM/yyyy";
                }
                else {
                    dManagmentFee.CustomFormat = "          ";
                    dManagmentFee.Format = DateTimePickerFormat.Custom;
                }
            }
            else {
                dManagmentFee.CustomFormat = "          ";
                dManagmentFee.Format = DateTimePickerFormat.Custom;
            }

            txtPerformanceFee.Text = klsProductTitle.PerformanceFee;
            if (Global.IsDate(klsProductTitle.PerformanceFee_Date)) {
                if (Convert.ToDateTime(klsProductTitle.PerformanceFee_Date).Date != Convert.ToDateTime("1900/01/01").Date) {
                    dPerformanceFee.Value = Convert.ToDateTime(klsProductTitle.PerformanceFee_Date).Date;
                    dPerformanceFee.CustomFormat = "dd/MM/yyyy";
                }
                else {
                    dPerformanceFee.CustomFormat = "          ";
                    dPerformanceFee.Format = DateTimePickerFormat.Custom;
                }
            }
            else {
                dPerformanceFee.CustomFormat = "          ";
                dPerformanceFee.Format = DateTimePickerFormat.Custom;
            }            

            txtCountryRegistered.Text = klsProductTitle.CountryRegistered;
            txtCountryAvailable.Text = klsProductTitle.CountryAvailable;
            chkGreeceRegistered.Checked = (klsProductTitle.GreeceRegistered == 1 ? true : false);
            chkGreeceAvailable.Checked = (klsProductTitle.GreeceAvailable == 1 ? true : false);
            cmbComplexProduct.SelectedIndex = klsProductTitle.ComplexProduct;
            txtComplexAttribute.Text = klsProductTitle.ComplexAttribute;
            cmbExchangeTradedNotes.SelectedValue = klsProductTitle.ExchangeTradedNotes;
            cmbCommodityTracking.SelectedValue = klsProductTitle.CommodityTracking;
            cmbInvestType_Retail.SelectedIndex = klsProductTitle.InvestType_Retail;
            cmbInvestType_Prof.SelectedIndex = klsProductTitle.InvestType_Prof;
            cmbInvestType_Eligible.SelectedIndex = klsProductTitle.InvestType_Eligible;
            cmbExpertise_Basic.SelectedValue = klsProductTitle.Expertise_Basic;
            cmbExpertise_Informed.SelectedValue = klsProductTitle.Expertise_Informed;
            cmbExpertise_Advanced.SelectedValue = klsProductTitle.Expertise_Advanced;
            txtRecHoldingPeriod.Text = klsProductTitle.RecHoldingPeriod;
            cmbRetProfile_Preserv.SelectedValue = klsProductTitle.RetProfile_Preserv;
            cmbRetProfile_Income.SelectedValue = klsProductTitle.RetProfile_Income;
            cmbRetProfile_Growth.SelectedValue = klsProductTitle.RetProfile_Growth;
            cmbDistrib_ExecOnly.SelectedValue = klsProductTitle.Distrib_ExecOnly;
            cmbDistrib_Advice.SelectedValue = klsProductTitle.Distrib_Advice;
            cmbDistrib_PortfolioManagment.SelectedValue = klsProductTitle.Distrib_PortfolioManagment;
            cmbCapitalLoss_None.SelectedValue = klsProductTitle.CapitalLoss_None;
            cmbCapitalLoss_Limited.SelectedValue = klsProductTitle.CapitalLoss_Limited;
            cmbCapitalLoss_NoGuarantee.SelectedValue = klsProductTitle.CapitalLoss_NoGuarantee;
            cmbCapitalLoss_BeyondInitial.SelectedValue = klsProductTitle.CapitalLoss_BeyondInitial;
            cmbCapitalLoss_Level.SelectedValue = klsProductTitle.CapitalLoss_Level;
        }
        private void ShowCodesList()
        {
            if (iShare_ID != 0)
            {

                fgCodes.Redraw = false;
                fgCodes.Rows.Count = 1;

                clsProductsCodes klsProductCode = new clsProductsCodes();
                klsProductCode.Share_ID = iShare_ID;
                klsProductCode.ISIN = "";
                klsProductCode.GetList();
                foreach (DataRow dtRow in klsProductCode.List.Rows)
                {
                    if (chkShowAktive.Checked || Convert.ToInt32(dtRow["Aktive"]) > 0)
                    {
                        if (Convert.ToInt32(dtRow["Aktive"]) > 0)
                        {
                            sTitle = txtTitle.Text + "";
                            sISIN = txtISIN.Text + "";
                        }
                        else
                        {
                            sTitle = dtRow["CodeTitle"] + "";
                            sISIN = dtRow["ISIN"] + "";
                        }

                        fgCodes.AddItem(dtRow["ID"] + "\t" + dtRow["DateFrom"] + "\t" + dtRow["DateTo"] + "\t" + sTitle + "\t" + sISIN + "\t" +
                                        dtRow["Code"] + "\t" + dtRow["Code2"] + "\t" + dtRow["Code3"] + "\t" + dtRow["SecID"] + "\t" + dtRow["Currency"] + "\t" +
                                        (Convert.ToInt16(dtRow["CurrencyHedge"]) == 1 ? "Fully Hedged" : "") + "\t" + dtRow["CurrencyHedge2"] + "\t" +
                                        dtRow["StockExchange_Code"] + "\t" + dtRow["CountryAction_Title"] + "\t" + dtRow["PrimaryShare_Title"] + "\t" +
                                        dtRow["DistributionStatus"] + "\t" + sDividendDistributionFrequency[Convert.ToInt32(dtRow["FrequencyClipping"])] + "\t" +
                                        dtRow["Weight"] + "\t" + dtRow["HFIC_Recom_Title"] + "\t" + dtRow["MIFID_Risk"] + "\t" + "0" + "\t" +
                                        dtRow["StockExchange_ID"] + "\t" + dtRow["CountryAction_ID"] + "\t" + dtRow["Aktive"] + "\t" + "0" + "\t" +
                                        dtRow["HFIC_Recom"] + "\t" + dtRow["DateIPO"] + "\t" + dtRow["PrimaryShare"]);
                    }
                }
                fgCodes.Redraw = true;
            }
        }
        //--- "header" data edit functions ------------------------------------------------------------------
        private void txtTitle_LostFocus(object sender, EventArgs e)
        {
            if (txtTitle.Text.Trim() != "")
            {
                for (i = 1; i < fgCodes.Rows.Count; i++)
                {
                    if (Convert.ToInt32(fgCodes[i, "Aktive"]) == 1)
                    {
                        fgCodes[i, "Title"] = txtTitle.Text;
                        fgCodes[i, "Edited"] = 1;
                    }
                }
            }
        }
        private void txtISIN_LostFocus(object sender, EventArgs e)
        {
            if (txtISIN.Text.Trim() != "")
            {
                i = Global.CheckISIN(txtISIN.Text);
                if (i == 0 || i == iShareTitle_ID)
                {
                    tsbSave.Enabled = true;
                    lblISIN_Warning.Text = "";
                    for (i = 1; i < fgCodes.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(fgCodes[i, "Aktive"]) == 1)
                        {
                            fgCodes[i, "ISIN"] = txtISIN.Text;
                            fgCodes[i, "Edited"] = 1;
                        }
                    }
                }
                else
                {
                    tsbSave.Enabled = false;
                    lblISIN_Warning.Text = "Το ISIN υπάρχει ήδη καταχωρημένο";
                    txtISIN.Focus();
                }
            }
        }
        private void txtNewISIN_LostFocus(object sender, EventArgs e)
        {
            if (txtNewISIN.Text.Trim() != "")
            {
                i = Global.CheckISIN(txtNewISIN.Text);
                if (i == 0 || i == iShareTitle_ID)
                {
                    btnISIN_OK.Enabled = true;
                    lblNewISIN_Warning.Text = "";
                    for (i = 1; i < fgCodes.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(fgCodes[i, "Aktive"]) == 1)
                        {
                            fgCodes[i, "ISIN"] = txtISIN.Text;
                            fgCodes[i, "Edited"] = 1;
                        }
                    }
                }
                else
                {
                    btnISIN_OK.Enabled = false;
                    lblNewISIN_Warning.Text = "Το ISIN υπάρχει ήδη καταχωρημένο";
                    txtNewISIN.Focus();
                }
            }
        }
        private void btnISIN_OK_Click(object sender, EventArgs e)
        {
            clsProductsTitles klsProductTitle = new clsProductsTitles();
            clsProductsCodes klsProductCode = new clsProductsCodes();

            for (i = 1; i <= fgCodes.Rows.Count - 1; i++)
            {
                if (Convert.ToInt32(fgCodes[i, "Aktive"]) == 1)
                {
                    klsProductTitle = new clsProductsTitles();
                    klsProductTitle.Record_ID = iShareTitle_ID;
                    klsProductTitle.GetRecord();
                    klsProductTitle.ISIN = txtNewISIN.Text;
                    iShareTitle_ID = klsProductTitle.InsertRecord();

                    klsProductCode = new clsProductsCodes();
                    klsProductCode.Product_ID = iProduct_ID;
                    klsProductCode.Share_ID = iShare_ID;
                    klsProductCode.DateFrom = DateTime.Now;
                    klsProductCode.DateTo = Convert.ToDateTime("2070/12/31");
                    klsProductCode.CodeTitle = txtTitle.Text;
                    klsProductCode.ISIN = txtNewISIN.Text;
                    klsProductCode.SecID = fgCodes[i, "SecID"] + "";
                    klsProductCode.Code = fgCodes[i, "Code"] + "";
                    klsProductCode.Code2 = fgCodes[i, "Code2"] + "";
                    klsProductCode.Code3 = fgCodes[i, "Code3"] + "";
                    klsProductCode.Curr = fgCodes[i, "Curr"] + "";
                    klsProductCode.CurrencyHedge = (fgCodes[i, "CurrencyHedge"]+"") == "Fully Hedged" ? 1 : 0;
                    klsProductCode.CurrencyHedge2 = fgCodes[i, "CurrencyHedge2"]+"";
                    klsProductCode.PrimaryShare = Convert.ToInt32(fgCodes[i, "PrimaryShare"]);                         // 0 - Unknown, 1 - No, 2 - Yes
                    klsProductCode.DistributionStatus = fgCodes[i, "DistributionStatus"]+"";
                    klsProductCode.FrequencyClipping = Convert.ToInt32(cmbFrequencyClipping.SelectedIndex) < 0 ? 0 : Convert.ToInt32(cmbFrequencyClipping.SelectedIndex);
                    klsProductCode.Gravity = Convert.ToSingle(fgCodes[i, "Gravity"]);
                    klsProductCode.MIFID_Risk = fgCodes[i, "MIFID_Risk"] + "";
                    klsProductCode.StockExchange_ID = Convert.ToInt32(fgCodes[i, "StockExchange_ID"]);
                    klsProductCode.CountryAction = Convert.ToInt32(fgCodes[i, "CountryAction_ID"]);
                    klsProductCode.Aktive = 1;
                    klsProductCode.DateIPO = Convert.ToDateTime(fgCodes[i, "DateIPO"]);
                    klsProductCode.HFIC_Recom = Convert.ToInt32(fgCodes[i, "HFIC_Recom_ID"]); 
                    iShareCode_ID = klsProductCode.InsertRecord();

                    clsProductsTitlesCodes klsProductTitleCode = new clsProductsTitlesCodes();
                    klsProductTitleCode.Record_ID = iSharesTitlesCodes_ID;
                    klsProductTitleCode.GetRecord();
                    klsProductTitleCode.DateTo = DateTime.Now.AddDays(-1);
                    klsProductTitleCode.EditRecord();

                    klsProductTitleCode = new clsProductsTitlesCodes();
                    klsProductTitleCode.DateFrom = DateTime.Now;
                    klsProductTitleCode.DateTo = Convert.ToDateTime("2070/12/31");
                    klsProductTitleCode.Share_ID = iShare_ID;
                    klsProductTitleCode.ShareTitle_ID = iShareTitle_ID;
                    klsProductTitleCode.ShareCode_ID = iShareCode_ID;
                    klsProductTitleCode.InsertRecord();
                }
            }

            for (i = 1; i <= fgCodes.Rows.Count - 1; i++)
            {
                if (Convert.ToInt32(fgCodes[i, "Aktive"]) == 1)
                {
                    klsProductCode = new clsProductsCodes();
                    klsProductCode.Record_ID = Convert.ToInt32(fgCodes[i, 0]);
                    klsProductCode.GetRecord();
                    klsProductCode.DateTo = DateTime.Now.AddDays(-1);
                    klsProductCode.Aktive = 0;
                    klsProductCode.EditRecord();
                }
            }

            txtISIN.Text = txtNewISIN.Text;
            panISIN.Visible = false;

            ShowCodesList();
        }

        private void btnEditISIN_Click(object sender, EventArgs e)
        {
            txtNewISIN.Text = "";
            lblNewISIN_Warning.Text = "";
            btnISIN_OK.Enabled = true;
            panISIN.Visible = true;
        }

        private void chkShowAktive_CheckedChanged(object sender, EventArgs e)
        {
            ShowCodesList();
        }

        private void picView_Click(object sender, EventArgs e)
        {
            iActionMode = 1;                       // 0 - Add, 1 - Tropopoiisi, 2 - Allagi
            i = fgCodes.Row;
            ShowCodeMask();
            ShowCodeData();
        }

        private void picPrices_Click(object sender, EventArgs e)
        {
            frmProductsPricesView locProductsPricesView = new frmProductsPricesView();
            locProductsPricesView.ShareCodes_ID = Convert.ToInt32(fgCodes[fgCodes.Row, "ID"]);
            locProductsPricesView.StartPosition = FormStartPosition.CenterScreen;
            locProductsPricesView.RightsLevel = iRightsLevel;
            locProductsPricesView.Show();
        }
        private void btnISIN_Cancel_Click(object sender, EventArgs e)
        {
            panISIN.Visible = false;
        }
        private void menuCopyISIN_Click(object sender, EventArgs e)
        {
            if (fgCodes.Row > 0) Clipboard.SetText(fgCodes[fgCodes.Row, "ISIN"] + "");
        }

        private void menuCopyReuters_Click(object sender, EventArgs e)
        {
            if (fgCodes.Row > 0) Clipboard.SetText(fgCodes[fgCodes.Row, "Code"] + "");
        }

        private void menuCopyBloomberg_Click(object sender, EventArgs e)
        {
            if (fgCodes.Row > 0) Clipboard.SetText(fgCodes[fgCodes.Row, "Code2"] + "");
        }
        private void picKey_Click(object sender, EventArgs e)
        {
            cmbProductType.Enabled = true;
            bEditProductType = true;
            picKey.Visible = false;
        }

        private void cmbProductType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Global.IsNumeric(cmbProductType.SelectedValue))
            {
                iProduct_ID = Convert.ToInt32(cmbProductType.SelectedValue);
                dtView = Global.dtProductsCategories.Copy().DefaultView;
                dtView.RowFilter = "Product_ID = " + iProduct_ID;
                cmbProductCategory.DataSource = dtView;
                cmbProductCategory.DisplayMember = "Title";
                cmbProductCategory.ValueMember = "ID";
            }
        }
        private void menuCallReutersCom_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.reuters.com/finance/stocks/overview?symbol=" + fgCodes[fgCodes.Row, "Code"]);
        }

        private void menuCallBloombergCom_Click(object sender, EventArgs e)
        {
            sTemp = fgCodes[fgCodes.Row, "Code2"] + "";
            Process.Start("http://www.bloomberg.com/quote/" + sTemp.Replace(" ", ":"));
        }
        //---------------------------------------------------------------------------------------------------
        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (fgCodes.Rows.Count > 1) {
                if (Convert.ToInt32(cmbProductCategory.SelectedValue) != 0) {
                    if (Convert.ToInt32(cmbProductType.SelectedValue) != 0) {
                        if (txtTitle.Text.Trim() != "") {
                            if (txtISIN.Text.Trim() != "") {

                                //--- recalc MIFID_Risk --------------------------------------------                                
                                for (i = 1; i <= fgCodes.Rows.Count - 1; i++)
                                {
                                    sTemp = Global.RecalcRiskProfile(Convert.ToInt32(fgCodes[i, 0]));
                                    clsProductsCodes klsProductCode = new clsProductsCodes();
                                    klsProductCode.Record_ID = Convert.ToInt32(fgCodes[i, 0]);
                                    klsProductCode.GetRecord();
                                    fgCodes[i, "MIFID_Risk"] = sTemp;
                                    klsProductCode.MIFID_Risk = sTemp;
                                    klsProductCode.EditRecord();
                                }
                                fgCodes.Redraw = true;

                                clsProductsTitles klsProductTitle = new clsProductsTitles();
                                if (iAction == 0)
                                {                                                   // 0 - ADD Mode
                                    clsProducts klsProduct = new clsProducts();
                                    klsProduct.Product_ID = iProduct_ID;
                                    klsProduct.Aktive = 1;
                                    iShare_ID = klsProduct.InsertRecord();

                                    klsProductTitle = new clsProductsTitles();
                                    klsProductTitle.Share_ID = iShare_ID;
                                    klsProductTitle.ProductTitle = txtTitle.Text;
                                    klsProductTitle.StandardTitle = txtStandardTitle.Text;
                                    klsProductTitle.ProviderName = txtProviderName.Text;
                                    klsProductTitle.BrandProviderName = txtBrandProviderName.Text;
                                    klsProductTitle.ISIN = txtISIN.Text;
                                    klsProductTitle.LegalStructure_ID = Convert.ToInt32(cmbLegalStructure.SelectedValue);
                                    klsProductTitle.ProductCategory = Convert.ToInt32(cmbProductCategory.SelectedValue);
                                    klsProductTitle.HFCategory = Convert.ToInt32(cmbHFCategory.SelectedValue);
                                    klsProductTitle.MiFIDInstrumentType = Convert.ToInt32(cmbMiFIDInstrumentType.SelectedValue);
                                    klsProductTitle.AIFMD = cmbAIFMD.SelectedIndex;
                                    klsProductTitle.MinimumInvestment = txtMinInvestment.Text;
                                    klsProductTitle.GlobalBroad = Convert.ToInt32(cmbGlobalBroad.SelectedValue);
                                    klsProductTitle.Country_ID = Convert.ToInt32(cmbCountry.SelectedValue);
                                    klsProductTitle.CountryGroup_ID = Convert.ToInt32(cmbCountryGroup.SelectedValue);
                                    klsProductTitle.Sector_ID = iSector_ID;
                                    klsProductTitle.CategoryMorningStar = Convert.ToInt32(cmbMorningStar.SelectedValue);
                                    klsProductTitle.CountryRisk_ID = Convert.ToInt32(cmbCountryRisk.SelectedValue);
                                    klsProductTitle.Benchmark = Convert.ToInt32(cmbBenchmarks.SelectedValue);
                                    klsProductTitle.RiskCurr = cmbRiskCurr.Text;
                                    klsProductTitle.DescriptionEn = txtDescriptionEn.Text;
                                    klsProductTitle.DescriptionGr = txtKIIDObjective.Text;
                                    klsProductTitle.Leverage = cmbLeverage.SelectedIndex;
                                    klsProductTitle.URL = txtURL.Text;
                                    klsProductTitle.IR_URL = txtIR_URL.Text;
                                    klsProductTitle.CreditRating = cmbCreditRating.Text;
                                    klsProductTitle.RatingGroup = cmbRatingGroup.SelectedIndex;
                                    klsProductTitle.TotalAUM = Convert.ToDecimal(txtTotalAUM.Text);
                                    klsProductTitle.TotalAUMDate = dTotalAUM.Text;
                                    klsProductTitle.Maturity = Convert.ToSingle(txtMaturity.Text);
                                    klsProductTitle.MaturityDate = dMaturity.Text;
                                    klsProductTitle.InvestmentType = Convert.ToInt32(cmbInvestmentType.SelectedValue);
                                    klsProductTitle.FundID = txtFundID.Text;
                                    klsProductTitle.InceptionDate = dInceptionDate.Value.ToString("dd/MM/yyyy");
                                    klsProductTitle.Institutional = txtInstitutional.Text;
                                    klsProductTitle.ActivelyManaged = cmbActivelyManaged.SelectedIndex;
                                    klsProductTitle.ReplicationMethod = txtReplicationMethod.Text;
                                    klsProductTitle.SwapBasedETF = txtSwapBasedETF.Text;
                                    klsProductTitle.EstimatedKIID = txtEstimatedKIID.Text;
                                    klsProductTitle.EstimatedKIID_Date = dEstimatedKIID.Value.ToString("dd/MM/yyyy");
                                    klsProductTitle.SurveyedKIID = Convert.ToSingle(txtSurveyedKIID.Text);
                                    klsProductTitle.SurveyedKIID_Date = dSurveyedKIID.Value.ToString("dd/MM/yyyy");
                                    klsProductTitle.OngoingKIID = Convert.ToSingle(txtOngoingKIID.Text);
                                    klsProductTitle.OngoingKIID_Date = dOngoingKIID.Value.ToString("dd/MM/yyyy");
                                    klsProductTitle.RatingOverall = txtRatingOverall.Text;
                                    klsProductTitle.RatingDate = dRatingDate.Value.ToString("dd/MM/yyyy");
                                    klsProductTitle.SurveyedKIID_History = txtSurveyedKIIDHistory.Text;
                                    klsProductTitle.SRRIValues = txtSRRIValues.Text;
                                    klsProductTitle.SRRIValues_Date = dSRRIValue.Value.ToString("dd/MM/yyyy");
                                    klsProductTitle.ManagmentFee = txtManagmentFee.Text;
                                    klsProductTitle.ManagmentFee_Date = dManagmentFee.Value.ToString("dd/MM/yyyy");
                                    klsProductTitle.PerformanceFee = txtPerformanceFee.Text;
                                    klsProductTitle.PerformanceFee_Date = dPerformanceFee.Value.ToString("dd/MM/yyyy");
                                    klsProductTitle.CountryRegistered = txtCountryRegistered.Text;
                                    klsProductTitle.CountryAvailable = txtCountryAvailable.Text;
                                    klsProductTitle.GreeceRegistered = chkGreeceRegistered.Checked ? 1 : 0;
                                    klsProductTitle.GreeceAvailable = chkGreeceAvailable.Checked ? 1 : 0;
                                    klsProductTitle.ComplexProduct = cmbComplexProduct.SelectedIndex;
                                    klsProductTitle.ComplexAttribute = txtComplexAttribute.Text;
                                    klsProductTitle.ExchangeTradedNotes = Convert.ToInt32(cmbExchangeTradedNotes.SelectedValue);
                                    klsProductTitle.CommodityTracking = Convert.ToInt32(cmbCommodityTracking.SelectedValue);
                                    klsProductTitle.InvestType_Retail = cmbInvestType_Retail.SelectedIndex;
                                    klsProductTitle.InvestType_Prof = cmbInvestType_Prof.SelectedIndex;
                                    klsProductTitle.InvestType_Eligible = cmbInvestType_Eligible.SelectedIndex;
                                    klsProductTitle.Expertise_Basic = Convert.ToInt32(cmbExpertise_Basic.SelectedValue);
                                    klsProductTitle.Expertise_Informed = Convert.ToInt32(cmbExpertise_Informed.SelectedValue);
                                    klsProductTitle.Expertise_Advanced = Convert.ToInt32(cmbExpertise_Advanced.SelectedValue);
                                    klsProductTitle.RecHoldingPeriod = txtRecHoldingPeriod.Text;
                                    klsProductTitle.RetProfile_Preserv = Convert.ToInt32(cmbRetProfile_Preserv.SelectedValue);
                                    klsProductTitle.RetProfile_Income = Convert.ToInt32(cmbRetProfile_Income.SelectedValue);
                                    klsProductTitle.RetProfile_Growth = Convert.ToInt32(cmbRetProfile_Growth.SelectedValue);
                                    klsProductTitle.Distrib_ExecOnly = Convert.ToInt32(cmbDistrib_ExecOnly.SelectedValue);
                                    klsProductTitle.Distrib_Advice = Convert.ToInt32(cmbDistrib_Advice.SelectedValue);
                                    klsProductTitle.Distrib_PortfolioManagment = Convert.ToInt32(cmbDistrib_PortfolioManagment.SelectedValue);
                                    klsProductTitle.CapitalLoss_None = Convert.ToInt32(cmbCapitalLoss_None.SelectedValue);
                                    klsProductTitle.CapitalLoss_Limited = Convert.ToInt32(cmbCapitalLoss_Limited.SelectedValue);
                                    klsProductTitle.CapitalLoss_NoGuarantee = Convert.ToInt32(cmbCapitalLoss_NoGuarantee.SelectedValue);
                                    klsProductTitle.CapitalLoss_BeyondInitial = Convert.ToInt32(cmbCapitalLoss_BeyondInitial.SelectedValue);
                                    klsProductTitle.CapitalLoss_Level = Convert.ToInt32(cmbCapitalLoss_Level.SelectedValue);
                                    klsProductTitle.NotTradeable = chkNonTradeable.Checked ? 1 : 0;
                                    klsProductTitle.LastEditDate = DateTime.Now;
                                    klsProductTitle.LastEditUser_ID = Global.User_ID;
                                    iShareTitle_ID = klsProductTitle.InsertRecord();

                                    sTemp = "";
                                    for (i = 1; i <= fgCodes.Rows.Count - 1; i++)
                                    {
                                        clsProductsCodes klsProductCode = new clsProductsCodes();
                                        klsProductCode.Product_ID = iProduct_ID;
                                        klsProductCode.Share_ID = iShare_ID;
                                        klsProductCode.DateFrom = Convert.ToDateTime(fgCodes[i, "DateFrom"]);
                                        klsProductCode.DateTo = Convert.ToDateTime(fgCodes[i, "DateTo"]);
                                        klsProductCode.CodeTitle = fgCodes[i, "CodeTitle"] + "";
                                        klsProductCode.ISIN = fgCodes[i, "ISIN"] + "";
                                        klsProductCode.Code = fgCodes[i, "Code"] + "";
                                        klsProductCode.Code2 = fgCodes[i, "Code2"] + "";
                                        klsProductCode.Code3 = fgCodes[i, "Code3"] + "";
                                        klsProductCode.SecID = fgCodes[i, "SecID"] + "";
                                        klsProductCode.Curr = fgCodes[i, "Curr"] + "";
                                        klsProductCode.CurrencyHedge = (fgCodes[i, "CurrencyHedge"] + "") == "Fully Hedged" ? 1 : 0;
                                        klsProductCode.CurrencyHedge2 = fgCodes[i, "CurrencyHedge2"] + "";
                                        klsProductCode.PrimaryShare = Convert.ToInt32(fgCodes[i, "PrimaryShare"]);
                                        klsProductCode.DistributionStatus = fgCodes[i, "DistributionStatus"] + "";
                                        klsProductCode.FrequencyClipping = (cmbFrequencyClipping.SelectedIndex < 0) ? 0 : cmbFrequencyClipping.SelectedIndex;
                                        klsProductCode.Gravity = Convert.ToSingle(fgCodes[i, "Gravity"]);
                                        klsProductCode.MIFID_Risk = fgCodes[i, "MIFID_Risk"] + "";
                                        klsProductCode.StockExchange_ID = Convert.ToInt32(fgCodes[i, "StockExchange_ID"]);
                                        klsProductCode.CountryAction = Convert.ToInt32(fgCodes[i, "CountryAction_ID"]);
                                        klsProductCode.Aktive = Convert.ToInt16(fgCodes[i, "Aktive"]);
                                        klsProductCode.HFIC_Recom = Convert.ToInt16(fgCodes[i, "HFIC_Recom_ID"]);
                                        klsProductCode.DateIPO = Convert.ToDateTime(fgCodes[i, "DateIPO"]);
                                        iShareCode_ID = klsProductCode.InsertRecord();

                                        klsProductCode.Gravity = Convert.ToSingle(fgCodes[i, "Gravity"]);
                                        klsProductCode.DateIPO = Convert.ToDateTime(fgCodes[i, "DateIPO"]);
                                        klsProductCode.HFIC_Recom = Convert.ToInt32(fgCodes[i, "HFIC_Recom_ID"]);
                                        klsProductCode.MIFID_Risk = fgCodes[i, "MIFID_Risk"] + "";
                                        klsProductCode.Aktive = (Convert.ToInt32(fgCodes[i, "Aktive"]) == 0 ? 0 : 1);
                                        iShareCode_ID = klsProductCode.InsertRecord();

                                        clsProductsTitlesCodes klsProductTitleCode = new clsProductsTitlesCodes();
                                        klsProductTitleCode.DateFrom = Convert.ToDateTime(fgCodes[i, "DateFrom"]);
                                        klsProductTitleCode.DateTo = Convert.ToDateTime(fgCodes[i, "DateTo"]);
                                        klsProductTitleCode.Share_ID = iShare_ID;
                                        klsProductTitleCode.ShareTitle_ID = iShareTitle_ID;
                                        klsProductTitleCode.ShareCode_ID = iShareCode_ID;
                                        klsProductTitleCode.InsertRecord();
                                    }
                                }
                                else
                                {
                                    klsProductTitle = new clsProductsTitles();
                                    klsProductTitle.Record_ID = iShareTitle_ID;
                                    klsProductTitle.GetRecord();
                                    klsProductTitle.ProductTitle = txtTitle.Text;
                                    klsProductTitle.StandardTitle = txtStandardTitle.Text;
                                    klsProductTitle.ProviderName = txtProviderName.Text;
                                    klsProductTitle.BrandProviderName = txtBrandProviderName.Text;
                                    klsProductTitle.ISIN = txtISIN.Text;
                                    klsProductTitle.LegalStructure_ID = Convert.ToInt32(cmbLegalStructure.SelectedValue);
                                    klsProductTitle.ProductCategory = Convert.ToInt32(cmbProductCategory.SelectedValue);
                                    klsProductTitle.HFCategory = Convert.ToInt32(cmbHFCategory.SelectedValue);
                                    klsProductTitle.MiFIDInstrumentType = Convert.ToInt32(cmbMiFIDInstrumentType.SelectedValue);
                                    klsProductTitle.AIFMD = cmbAIFMD.SelectedIndex;
                                    klsProductTitle.MinimumInvestment = txtMinInvestment.Text;
                                    klsProductTitle.GlobalBroad = Convert.ToInt32(cmbGlobalBroad.SelectedValue);
                                    klsProductTitle.Country_ID = Convert.ToInt32(cmbCountry.SelectedValue);
                                    klsProductTitle.CountryGroup_ID = Convert.ToInt32(cmbCountryGroup.SelectedValue);
                                    klsProductTitle.Sector_ID = iSector_ID;
                                    klsProductTitle.CategoryMorningStar = Convert.ToInt32(cmbMorningStar.SelectedValue);
                                    klsProductTitle.CountryRisk_ID = Convert.ToInt32(cmbCountryRisk.SelectedValue);
                                    klsProductTitle.Benchmark = Convert.ToInt32(cmbBenchmarks.SelectedValue);
                                    klsProductTitle.RiskCurr = cmbRiskCurr.Text;
                                    klsProductTitle.DescriptionEn = txtDescriptionEn.Text;
                                    klsProductTitle.DescriptionGr = txtKIIDObjective.Text;
                                    klsProductTitle.Leverage = cmbLeverage.SelectedIndex;
                                    klsProductTitle.URL = txtURL.Text;
                                    klsProductTitle.IR_URL = txtIR_URL.Text;
                                    klsProductTitle.CreditRating = cmbCreditRating.Text;
                                    klsProductTitle.RatingGroup = cmbRatingGroup.SelectedIndex;
                                    klsProductTitle.TotalAUM = Convert.ToDecimal(txtTotalAUM.Text);
                                    klsProductTitle.TotalAUMDate = dTotalAUM.Text;
                                    klsProductTitle.Maturity = Convert.ToSingle(txtMaturity.Text);
                                    klsProductTitle.MaturityDate = dMaturity.Text;
                                    klsProductTitle.InvestmentType = Convert.ToInt32(cmbInvestmentType.SelectedValue);
                                    klsProductTitle.FundID = txtFundID.Text;
                                    klsProductTitle.InceptionDate = dInceptionDate.Value.ToString("dd/MM/yyyy");
                                    klsProductTitle.Institutional = txtInstitutional.Text;
                                    klsProductTitle.ActivelyManaged = cmbActivelyManaged.SelectedIndex;
                                    klsProductTitle.ReplicationMethod = txtReplicationMethod.Text;
                                    klsProductTitle.SwapBasedETF = txtSwapBasedETF.Text;
                                    klsProductTitle.EstimatedKIID = txtEstimatedKIID.Text;
                                    klsProductTitle.EstimatedKIID_Date = dEstimatedKIID.Value.ToString("dd/MM/yyyy");
                                    klsProductTitle.SurveyedKIID = Convert.ToSingle(txtSurveyedKIID.Text);
                                    klsProductTitle.SurveyedKIID_Date = dSurveyedKIID.Value.ToString("dd/MM/yyyy");
                                    klsProductTitle.OngoingKIID = Convert.ToSingle(txtOngoingKIID.Text);
                                    klsProductTitle.OngoingKIID_Date = dOngoingKIID.Value.ToString("dd/MM/yyyy");
                                    klsProductTitle.RatingOverall = txtRatingOverall.Text;
                                    klsProductTitle.RatingDate = dRatingDate.Value.ToString("dd/MM/yyyy");
                                    klsProductTitle.SurveyedKIID_History = txtSurveyedKIIDHistory.Text;
                                    klsProductTitle.SRRIValues = txtSRRIValues.Text;
                                    klsProductTitle.SRRIValues_Date = dSRRIValue.Value.ToString("dd/MM/yyyy");
                                    klsProductTitle.ManagmentFee = txtManagmentFee.Text;
                                    klsProductTitle.ManagmentFee_Date = dManagmentFee.Value.ToString("dd/MM/yyyy");
                                    klsProductTitle.PerformanceFee = txtPerformanceFee.Text;
                                    klsProductTitle.PerformanceFee_Date = dPerformanceFee.Value.ToString("dd/MM/yyyy");
                                    klsProductTitle.CountryRegistered = txtCountryRegistered.Text;
                                    klsProductTitle.CountryAvailable = txtCountryAvailable.Text;
                                    klsProductTitle.GreeceRegistered = chkGreeceRegistered.Checked ? 1 : 0;
                                    klsProductTitle.GreeceAvailable = chkGreeceAvailable.Checked ? 1 : 0;
                                    klsProductTitle.ComplexProduct = cmbComplexProduct.SelectedIndex;
                                    klsProductTitle.ComplexAttribute = txtComplexAttribute.Text;
                                    klsProductTitle.ExchangeTradedNotes = Convert.ToInt32(cmbExchangeTradedNotes.SelectedValue);
                                    klsProductTitle.CommodityTracking = Convert.ToInt32(cmbCommodityTracking.SelectedValue);
                                    klsProductTitle.InvestType_Retail = cmbInvestType_Retail.SelectedIndex;
                                    klsProductTitle.InvestType_Prof = cmbInvestType_Prof.SelectedIndex;
                                    klsProductTitle.InvestType_Eligible = cmbInvestType_Eligible.SelectedIndex;
                                    klsProductTitle.Expertise_Basic = Convert.ToInt32(cmbExpertise_Basic.SelectedValue);
                                    klsProductTitle.Expertise_Informed = Convert.ToInt32(cmbExpertise_Informed.SelectedValue);
                                    klsProductTitle.Expertise_Advanced = Convert.ToInt32(cmbExpertise_Advanced.SelectedValue);
                                    klsProductTitle.RecHoldingPeriod = txtRecHoldingPeriod.Text;
                                    klsProductTitle.RetProfile_Preserv = Convert.ToInt32(cmbRetProfile_Preserv.SelectedValue);
                                    klsProductTitle.RetProfile_Income = Convert.ToInt32(cmbRetProfile_Income.SelectedValue);
                                    klsProductTitle.RetProfile_Growth = Convert.ToInt32(cmbRetProfile_Growth.SelectedValue);
                                    klsProductTitle.Distrib_ExecOnly = Convert.ToInt32(cmbDistrib_ExecOnly.SelectedValue);
                                    klsProductTitle.Distrib_Advice = Convert.ToInt32(cmbDistrib_Advice.SelectedValue);
                                    klsProductTitle.Distrib_PortfolioManagment = Convert.ToInt32(cmbDistrib_PortfolioManagment.SelectedValue);
                                    klsProductTitle.CapitalLoss_None = Convert.ToInt32(cmbCapitalLoss_None.SelectedValue);
                                    klsProductTitle.CapitalLoss_Limited = Convert.ToInt32(cmbCapitalLoss_Limited.SelectedValue);
                                    klsProductTitle.CapitalLoss_NoGuarantee = Convert.ToInt32(cmbCapitalLoss_NoGuarantee.SelectedValue);
                                    klsProductTitle.CapitalLoss_BeyondInitial = Convert.ToInt32(cmbCapitalLoss_BeyondInitial.SelectedValue);
                                    klsProductTitle.CapitalLoss_Level = Convert.ToInt32(cmbCapitalLoss_Level.SelectedValue);
                                    klsProductTitle.LastEditDate = DateTime.Now;
                                    klsProductTitle.LastEditUser_ID = Global.User_ID;
                                    klsProductTitle.NotTradeable = chkNonTradeable.Checked ? 1 : 0;
                                    klsProductTitle.EditRecord();

                                    for (i = 1; i <= fgCodes.Rows.Count - 1; i++)
                                    {
                                        if (Convert.ToInt32(fgCodes[i, "Edited"]) == 1)
                                        {
                                            clsProductsCodes klsProductCode = new clsProductsCodes();
                                            klsProductCode.Record_ID = Convert.ToInt32(fgCodes[i, 0]);
                                            klsProductCode.GetRecord();
                                            klsProductCode.Product_ID = iProduct_ID;
                                            klsProductCode.Share_ID = iShare_ID;
                                            klsProductCode.DateFrom = Convert.ToDateTime(fgCodes[i, "DateFrom"]);
                                            klsProductCode.DateTo = Convert.ToDateTime(fgCodes[i, "DateTo"]);
                                            klsProductCode.CodeTitle = fgCodes[i, "CodeTitle"] + "";
                                            klsProductCode.ISIN = fgCodes[i, "ISIN"] + "";
                                            klsProductCode.Code = fgCodes[i, "Code"] + "";
                                            klsProductCode.Code2 = fgCodes[i, "Code2"] + "";
                                            klsProductCode.Code3 = fgCodes[i, "Code3"] + "";
                                            klsProductCode.SecID = fgCodes[i, "SecID"] + "";
                                            klsProductCode.Curr = fgCodes[i, "Curr"] + "";
                                            klsProductCode.CurrencyHedge = (fgCodes[i, "CurrencyHedge"] + "") == "Fully Hedged" ? 1 : 0;
                                            klsProductCode.CurrencyHedge2 = fgCodes[i, "CurrencyHedge2"] + "";
                                            klsProductCode.PrimaryShare = Convert.ToInt32(fgCodes[i, "PrimaryShare"]);
                                            klsProductCode.DistributionStatus = fgCodes[i, "DistributionStatus"] + "";
                                            klsProductCode.FrequencyClipping = (cmbFrequencyClipping.SelectedIndex < 0) ? 0 : cmbFrequencyClipping.SelectedIndex;
                                            klsProductCode.Gravity = Convert.ToSingle(fgCodes[i, "Gravity"]);
                                            klsProductCode.MIFID_Risk = fgCodes[i, "MIFID_Risk"] + "";
                                            klsProductCode.StockExchange_ID = Convert.ToInt32(fgCodes[i, "StockExchange_ID"]);
                                            klsProductCode.CountryAction = Convert.ToInt32(fgCodes[i, "CountryAction_ID"]);
                                            klsProductCode.Aktive = Convert.ToInt16(fgCodes[i, "Aktive"]);
                                            klsProductCode.HFIC_Recom = Convert.ToInt16(fgCodes[i, "HFIC_Recom_ID"]);
                                            klsProductCode.DateIPO = Convert.ToDateTime(fgCodes[i, "DateIPO"]);

                                            if (Convert.ToInt32(fgCodes[i, "ID"]) == 0)
                                            {
                                                //--- Add New Record --------
                                                iShareCode_ID = klsProductCode.InsertRecord();

                                                clsProductsTitlesCodes klsProductTitleCode = new clsProductsTitlesCodes();
                                                klsProductTitleCode.DateFrom = Convert.ToDateTime(fgCodes[i, "DateFrom"]);
                                                klsProductTitleCode.DateTo = Convert.ToDateTime(fgCodes[i, "DateTo"]);
                                                klsProductTitleCode.Share_ID = iShare_ID;
                                                klsProductTitleCode.ShareTitle_ID = iShareTitle_ID;
                                                klsProductTitleCode.ShareCode_ID = iShareCode_ID;
                                                klsProductTitleCode.InsertRecord();
                                            }
                                            else
                                            {
                                                //--- Edit Record --------
                                                iShareCode_ID = Convert.ToInt32(fgCodes[i, "ID"]);
                                                klsProductCode.Record_ID = iShareCode_ID;
                                                klsProductCode.EditRecord();
                                            }
                                        }
                                        else iShareCode_ID = Convert.ToInt32(fgCodes[i, "ID"]);

                                        if (Convert.ToInt32(fgCodes[i, "Old_ID"]) != 0)
                                        {
                                            //--- Edit all Transactions in period fgCodes[i, "DateFrom") - fgCodes[i, "DateTo") that had old Official Data
                                            clsOrdersSecurity klsOrderSecurity = new clsOrdersSecurity();
                                            klsOrderSecurity.DateFrom = Convert.ToDateTime(fgCodes[i, "DateFrom"]);
                                            klsOrderSecurity.DateTo = Convert.ToDateTime(fgCodes[i, "DateTo"]);
                                            klsOrderSecurity.Share_ID = Convert.ToInt32(fgCodes[i, "Old_ID"]);
                                            klsOrderSecurity.GetList_Period();
                                            foreach (DataRow dtRow in klsOrderSecurity.List.Rows)
                                            {
                                                klsOrderSecurity.Record_ID = Convert.ToInt32(dtRow["ID"]);
                                                klsOrderSecurity.GetRecord();
                                                klsOrderSecurity.Share_ID = iShareCode_ID;
                                                klsOrderSecurity.EditRecord();
                                            }
                                        }
                                    }

                                    if (bEditProductType)
                                    {
                                        clsProducts klsProduct = new clsProducts();
                                        klsProduct.Record_ID = iShare_ID;
                                        klsProduct.GetRecord();
                                        klsProduct.Product_ID = iProduct_ID;
                                        klsProduct.Aktive = 1;
                                        klsProduct.EditRecord();
                                    }
                                }

                                CashTable.Record_ID = 41;                         // ListsTables.ID = 41 - ShareCodes
                                CashTable.GetRecord();
                                CashTable.LastEdit_Time = DateTime.Now;
                                CashTable.LastEdit_User_ID = Global.User_ID;
                                CashTable.EditRecord();
                                Global.GetProductsList();
                                lblFlagEdit.Text = iShare_ID.ToString();

                                ComponentsOnOff(false);
                                tsbSave.Enabled = false;
                                toolCode.Enabled = false;
                            }
                            else MessageBox.Show("Η εισαγωγή του ISIN είναι υποχρεωτική", "Προϊοντα", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else MessageBox.Show("Η εισαγωγή όνομας εταιρίας είναι υποχρεωτική", "Προϊοντα", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else MessageBox.Show("Η εισαγωγή Τύπου Προϊόντος είναι υποχρεωτική", "Προϊοντα", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else MessageBox.Show("Η εισαγωγή Κατηγορίας Προμήθειας είναι υποχρεωτική", "Προϊοντα", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else MessageBox.Show("Η εισαγωγή τουλάχιστον ενός κωδικού είναι υποχρεωτική", "Προϊοντα", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        //----fgCodes functions-----------------------------------------------------------------------------
        private void tslAdd_Click(object sender, EventArgs e)
        {
            iActionMode = 0;                       //0 - Add, 1 - Tropopoiisi, 2 - Allagi
            lblCode.Text = "Καταχωρίστε στοιχεία νέου κωδικού ";
            dFrom.Value = Convert.ToDateTime("01/01/" + DateTime.Now.Year);
            dTo.Value = Convert.ToDateTime("31/12/2070");
            txtTitleCode.Text = txtTitle.Text;
            txtISINCode.Text = txtISIN.Text;
            txtReutersCode.Text = "";
            txtBloombergCode.Text = "";
            txtExchangeTicker.Text = "";
            txtMSCeID.Text = "";
            cmbCurrencyHedge.Text = "";
            cmbCurrencyHedge2.Text = "";
            cmbCurrency.Text = "";
            cmbCountryAction.SelectedValue = 0;
            cmbStockExchanges.SelectedValue = 0;
            cmbPrimaryShare.SelectedIndex = 0;                                             // 0 - Unknown, 1 - No, 2 - Yes
            cmbDistributionStatus.SelectedIndex = 0;
            cmbFrequencyClipping.Text = "";
            txtGravity.Text = "0";
            chkAktive.Checked = true;

            txtMSCeID.Text = "";
            cmbCountryAction.SelectedValue = 0;
            cmbStockExchanges.SelectedValue = 0;
            cmbCurrency.Text = "";
            cmbPrimaryShare.Text = "No";
            chkAktive.Checked = true;
            dIPO.Value = Convert.ToDateTime("01/01/1900");
            cmbHFIC_Recom.SelectedIndex = 0;
            chkMIFID_Risk_1.Checked = false;
            chkMIFID_Risk_2.Checked = false;
            chkMIFID_Risk_3.Checked = false;
            chkMIFID_Risk_4.Checked = false;
            chkMIFID_Risk_5.Checked = false;
            chkMIFID_Risk_6.Checked = false;

            ShowCodeMask();
            btnOKCode.Enabled = true;
            btnCancelCode.Enabled = true;
        }

        private void tslEdit_Click(object sender, EventArgs e)
        {
            if (fgCodes.Rows.Count > 1)
            {
                iActionMode = 1;                       // 0 - Add, 1 - Tropopoiisi, 2 - Allagi
                lblCode.Text = "Διόρθωση Λανθασμένων Στοιχείων Κωδικού";
                ShowCodeMask();
                btnOKCode.Enabled = true;
                btnCancelCode.Enabled = true;
                i = fgCodes.Row;
                if (i < 1) i = 1;
                dFrom.Value = Convert.ToDateTime(fgCodes[i, "DateFrom"]);
                dTo.Value = Convert.ToDateTime(fgCodes[i, "DateTo"]);
                ShowCodeData();
            }
        }

        private void tslChange_Click(object sender, EventArgs e)
        {
            iActionMode = 2;                       // 0 - Add, 1 - Tropopoiisi, 2 - Allagi
            lblCode.Text = "Με την ενέργια αυτήν θα ακυρωθούν τρέχον στοιχεία κωδικού και θα καταχωριθούν στοιχεία νέου κωδικου";
            dFrom.Value = DateTime.Now;
            dTo.Value = Convert.ToDateTime("31-12-2070");
            txtTitleCode.Text = txtTitle.Text;
            txtISINCode.Text = txtISIN.Text;
            cmbCountryAction.SelectedValue = 0;
            cmbStockExchanges.SelectedValue = 0;
            cmbCurrency.Text = "";
            chkAktive.Checked = true;
            cmbHFIC_Recom.SelectedIndex = 0;
            chkMIFID_Risk_1.Checked = false;
            chkMIFID_Risk_2.Checked = false;
            chkMIFID_Risk_3.Checked = false;
            chkMIFID_Risk_4.Checked = false;
            chkMIFID_Risk_5.Checked = false;
            chkMIFID_Risk_6.Checked = false;

            ShowCodeMask();
            btnOKCode.Enabled = true;
            btnCancelCode.Enabled = true;
            ShowCodeData();
        }

        private void tslCancel_Click(object sender, EventArgs e)
        {
            if (fgCodes.Row > 0)
            {
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να ακυρωθεί ο κωδικός.\nΕίστε σίγουρος για τη ακύρωση του;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {

                    clsProductsCodes klsProductCode = new clsProductsCodes();
                    klsProductCode.Record_ID = Convert.ToInt32(fgCodes[fgCodes.Row, "ID"]);
                    klsProductCode.Aktive = 0;
                    klsProductCode.EditRecord_Active();

                    ShowCodesList();
                }
            }
        }

        private void btnOKCode_Click(object sender, EventArgs e)
        {
            sMIFID_Risk = (chkMIFID_Risk_1.Checked ? "1" : "0") + (chkMIFID_Risk_2.Checked ? "1" : "0") + (chkMIFID_Risk_3.Checked ? "1" : "0") +
                (chkMIFID_Risk_4.Checked ? "1" : "0") + (chkMIFID_Risk_5.Checked ? "1" : "0") + (chkMIFID_Risk_6.Checked ? "1" : "0");

            switch (iActionMode)
            {
                case 0:
                    fgCodes.AddItem("0" + "\t" + dFrom.Value.ToString("d") + "\t" + dTo.Value.ToString("d") + "\t" + txtTitle.Text + "\t" + txtISIN.Text + "\t" +
                                    txtReutersCode.Text + "\t" + txtBloombergCode.Text + "\t" + txtExchangeTicker.Text + "\t" + txtMSCeID.Text + "\t" + cmbCurrency.Text + "\t" +
                                    cmbCurrencyHedge.Text + "\t" + cmbCurrencyHedge2.Text + "\t" + cmbStockExchanges.Text + "\t" + cmbCountryAction.Text + "\t" +
                                    cmbPrimaryShare.Text + "\t" + cmbDistributionStatus.Text + "\t" + cmbFrequencyClipping.Text + "\t" +
                                    "0" + "\t" + (cmbHFIC_Recom.SelectedIndex == 1 ? "Yes" : "No") + "\t" + sMIFID_Risk + "\t" + "0" + "\t" + cmbStockExchanges.SelectedValue + "\t" +
                                    cmbCountryAction.SelectedValue + "\t" + "1" + "\t" + "1" + "\t" + (cmbHFIC_Recom.SelectedIndex == 1 ? 1 : 0) + "\t" +
                                    dIPO.Value + "\t" + cmbPrimaryShare.SelectedIndex);
                    break;
                case 1:
                    i = fgCodes.Row;
                    fgCodes[i, "DateFrom"] = dFrom.Value;
                    fgCodes[i, "DateTo"] = dTo.Value;
                    fgCodes[i, "CodeTitle"] = txtTitle.Text;
                    fgCodes[i, "ISIN"] = txtISIN.Text;
                    fgCodes[i, "Code"] = txtReutersCode.Text;
                    fgCodes[i, "Code2"] = txtBloombergCode.Text;
                    fgCodes[i, "Code3"] = txtExchangeTicker.Text;
                    fgCodes[i, "SecID"] = txtMSCeID.Text;
                    fgCodes[i, "Curr"] = cmbCurrency.Text;
                    fgCodes[i, "CurrencyHedge"] = cmbCurrencyHedge.Text;
                    fgCodes[i, "CurrencyHedge2"] = cmbCurrencyHedge2.Text;
                    fgCodes[i, "StockExchange_Code"] = cmbStockExchanges.Text;
                    fgCodes[i, "CountryAction_Title"] = cmbCountryAction.Text;
                    fgCodes[i, "PrimaryShare_Title"] = cmbPrimaryShare.Text;
                    fgCodes[i, "DistributionStatus"] = cmbDistributionStatus.Text;
                    fgCodes[i, "FrequencyClipping"] = cmbFrequencyClipping.Text;
                    fgCodes[i, "Gravity"] = txtGravity.Text;
                    fgCodes[i, "HFIC_Recom"] = (Convert.ToInt32(cmbHFIC_Recom.SelectedIndex) == 1 ? "Yes" : "No");
                    fgCodes[i, "MIFID_Risk"] = sMIFID_Risk;
                    fgCodes[i, "Aktive"] = (chkAktive.Checked ? 1 : 0);
                    fgCodes[i, "Old_ID"] = 0;
                    fgCodes[i, "Edited"] = 1;
                    fgCodes[i, "StockExchange_ID"] = cmbStockExchanges.SelectedValue;
                    fgCodes[i, "CountryAction_ID"] = cmbCountryAction.SelectedValue;
                    fgCodes[i, "Aktive"] = chkAktive.Checked;
                    fgCodes[i, "DateIPO"] = dIPO.Value;
                    fgCodes[i, "HFIC_Recom_ID"] = (cmbHFIC_Recom.SelectedIndex == 1 ? 1 : 0);
                    fgCodes[i, "PrimaryShare"] = cmbPrimaryShare.SelectedIndex;
                    break;
                case 2:
                    fgCodes[fgCodes.Row, "DateFrom"] = dFrom.Value.AddDays(-1);
                    fgCodes[fgCodes.Row, "Aktive"] = 0;                             // Aktive
                    iOldCode_ID = Convert.ToInt32(fgCodes[fgCodes.Row, 0]);         // ID       

                    clsProductsCodes klsProductCode = new clsProductsCodes();
                    klsProductCode.Record_ID = Convert.ToInt32(fgCodes[fgCodes.Row, 0]);
                    klsProductCode.DateTo = Convert.ToDateTime(fgCodes[fgCodes.Row, "DateFrom"]);
                    klsProductCode.Aktive = Convert.ToInt32(fgCodes[fgCodes.Row, "Aktive"]);
                    klsProductCode.EditRecord_Active();

                    fgCodes.AddItem("0" + "\t" + dFrom.Value.ToString("d") + "\t" + dTo.Value.ToString("d") + "\t" + txtTitle.Text + "\t" + txtISIN.Text + "\t" +
                                 txtReutersCode.Text + "\t" + txtBloombergCode.Text + "\t" + txtExchangeTicker.Text + "\t" + txtMSCeID.Text + "\t" + cmbCurrency.Text + "\t" +
                                 cmbCurrencyHedge.Text + "\t" + cmbCurrencyHedge2.Text + "\t" + cmbStockExchanges.Text + "\t" +
                                 cmbCountryAction.Text + "\t" + cmbPrimaryShare.Text + "\t" + cmbDistributionStatus.Text + "\t" + cmbFrequencyClipping.Text + "\t" +
                                 "0" + "\t" + "No" + "\t" + sMIFID_Risk + "\t" + iOldCode_ID + "\t" + cmbStockExchanges.SelectedValue + "\t" + cmbCountryAction.SelectedValue + "\t" +
                                 "1" + "\t" + "1" + "\t" + "0" + "\t" + dIPO.Value + "\t" + cmbPrimaryShare.SelectedIndex);
                    break;
            }
            panCode.Visible = false;
        }

        private void btnCancelCode_Click(object sender, EventArgs e)
        {
            panCode.Visible = false;
        }
        private void picClose_Code_Click(object sender, EventArgs e)
        {
            panCode.Visible = false;
        }
        private void fgCodes_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Row > 0)
            {
                if (e.Col == 12) fgCodes[e.Row, "StockExchange_ID"] = fgCodes[e.Row, "StockExchange_Code"];                      // 12 - StockExchange_Code
                if (e.Col == 13) fgCodes[e.Row, "CountryAction_ID"] = fgCodes[e.Row, "CountryAction_Title"];                     // 13 - CountryAction_ID

                if (e.Col == 23)                                                                                                 // 23 - Aktive
                    if (Convert.ToInt32(fgCodes[e.Row, "Aktive"]) == 0) fgCodes.Rows[e.Row].Style = csCancel;
                    else fgCodes.Rows[e.Row].Style = csAktive;
            }
        }
        private void fgCodes_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                fgCodes.ContextMenuStrip = mnuContext;
                fgCodes.Row = fgCodes.MouseRow;
            }
        }
        //---------------------------------------------------------------------------------------------------
        public void ComponentsOnOff(bool bFlag)
        {
            Color backColor, foreColor;

            if (bFlag)
            {
                backColor = Color.White;
                foreColor = Color.Black;
            }
            else
            {
                backColor = Color.Gainsboro;
                foreColor = Color.Black;
            }

            foreach (Control parControl in TabPage1.Controls)
                if (parControl is TextBox)
                {
                    parControl.BackColor = backColor;
                    parControl.ForeColor = foreColor;
                }
        }
        private void ShowCodeMask()
        {
            panCode.Top = (this.Height - panCode.Height) / 2;
            panCode.Left = (this.Width - panCode.Width) / 2;
            lblReutersCode.Visible = true;
            lblMStar.Visible = true;
            lblMSCeID.Visible = true;
            txtMSCeID.Visible = true;

            panCode.Visible = true;
        }
        private void ShowCodeData()
        {
            i = fgCodes.Row;
            dFrom.Value = Convert.ToDateTime(fgCodes[i, "DateFrom"]);
            dTo.Value = Convert.ToDateTime(fgCodes[i, "DateTo"]);
            txtTitleCode.Text = txtTitle.Text;
            txtISINCode.Text = txtISIN.Text;
            txtReutersCode.Text = fgCodes[i, "Code"] + "";
            txtBloombergCode.Text = fgCodes[i, "Code2"] + "";
            txtExchangeTicker.Text = fgCodes[i, "Code3"] + "";
            txtMSCeID.Text = fgCodes[i, "SecID"] + "";
            cmbCurrency.Text = fgCodes[i, "Curr"] + "";
            cmbCurrencyHedge.Text = fgCodes[i, "CurrencyHedge"] + "";
            cmbCurrencyHedge2.Text = fgCodes[i, "CurrencyHedge2"] + "";
            cmbStockExchanges.SelectedValue = Convert.ToInt32(fgCodes[i, "StockExchange_ID"]);
            cmbCountryAction.SelectedValue = Convert.ToInt32(fgCodes[i, "CountryAction_ID"]);
            cmbPrimaryShare.SelectedIndex = Convert.ToInt32(fgCodes[i, "PrimaryShare"]);
            cmbDistributionStatus.Text = fgCodes[i, "DistributionStatus"] + "";
            cmbFrequencyClipping.Text = fgCodes[i, "FrequencyClipping"] + "";
            txtGravity.Text = fgCodes[i, "Gravity"] + "";
            cmbHFIC_Recom.SelectedIndex = Convert.ToInt32(fgCodes[i, "HFIC_Recom_ID"]);
            sTemp = fgCodes[i, "MIFID_Risk"] + "      ";
            chkMIFID_Risk_1.Checked = (sTemp.Substring(0, 1) == "1" ? true : false);
            chkMIFID_Risk_2.Checked = (sTemp.Substring(1, 1) == "1" ? true : false);
            chkMIFID_Risk_3.Checked = (sTemp.Substring(2, 1) == "1" ? true : false);
            chkMIFID_Risk_4.Checked = (sTemp.Substring(3, 1) == "1" ? true : false);
            chkMIFID_Risk_5.Checked = (sTemp.Substring(4, 1) == "1" ? true : false);
            chkMIFID_Risk_6.Checked = (sTemp.Substring(5, 1) == "1" ? true : false);
            dIPO.Value = Convert.ToDateTime(fgCodes[i, "DateIPO"]);

            if (Convert.ToInt32(fgCodes[i, "Aktive"]) == 2) chkAktive.Checked = true;
            else chkAktive.Checked = (Convert.ToInt32(fgCodes[i, "Aktive"]) == 1 ? true : false);
        }
        public int Mode { get { return this.iMode; } set { this.iMode = value; } }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
    }
}
