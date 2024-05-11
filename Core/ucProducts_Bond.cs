using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

namespace Core
{
    public partial class ucProducts_Bond : UserControl
    {
        DataView dtView;
        DataRow[] foundRows;
        int i, iMode, iShare_ID, iShareTitle_ID, iShareCode_ID, iAction, iOldCode_ID, iProduct_ID, iSector_ID, iSharesTitlesCodes_ID, iActionMode, iRightsLevel;
        string sTemp, sTitle, sISIN, sMIFID_Risk;
        bool bCheckList, bEditProductType;
        DateTime dTemp;
        CellStyle csAktive, csCancel;
        Point position;
        bool pMove;
        
        clsProductsTitles klsProductTitle = new clsProductsTitles();
        clsCashTables CashTable = new clsCashTables();
        public ucProducts_Bond()
        {
            InitializeComponent();

            panISIN.Left = 102;
            panISIN.Top = 92;
            iProduct_ID = 2;

            lblISIN_Warning.Text = "";
            lblNewISIN_Warning.Text = "";
        }

        private void ucProducts_Bond_Load(object sender, EventArgs e)
        {
            bEditProductType = false;
            lblISIN_Warning.Text = "";

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

            iProduct_ID = 2;
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
            ShowCommonData();
            ShowCodesList();

            //--- show Complex Reasons ---------------------------------------------------------------------------
            clsProductsTitles klsProductTitle_ComplexReasons = new clsProductsTitles(); ;
            klsProductTitle_ComplexReasons.Record_ID = iShareTitle_ID;
            klsProductTitle_ComplexReasons.GetComplexReasons_List();

            fgComplexReasons.Redraw = false;
            fgComplexReasons.Rows.Count = 1;
            foreach (DataRow dtRow in klsProductTitle_ComplexReasons.List.Rows)
                fgComplexReasons.AddItem(dtRow["Title"] + "\t" + dtRow["ID"] + "\t" + dtRow["ComplexReason_ID"]);
     
            fgComplexReasons.Redraw = true;

            //------------------------------------------------------------------------------------------------------
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

            iProduct_ID = 2;                    // 2 - Bond

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
            bCheckList = false;
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

            //-------------- Define CountryAction List ------------------
            dtView = Global.dtCountries.Copy().DefaultView;
            dtView.RowFilter = "Tipos = 1";
            cmbCountryAction.DataSource = dtView;
            cmbCountryAction.DisplayMember = "Title";
            cmbCountryAction.ValueMember = "ID";

            //-------------- Define cmbCountryIssue List ------------------
            dtView = Global.dtCountries.Copy().DefaultView;
            dtView.RowFilter = "Tipos = 1";
            cmbCountryIssue.DataSource = dtView;
            cmbCountryIssue.DisplayMember = "Title";
            cmbCountryIssue.ValueMember = "ID";
            
            //-------------- Define Investment Areas List ------------------
            cmbCountryRisk.DataSource = Global.dtCountries.Copy();
            cmbCountryRisk.DisplayMember = "Title";
            cmbCountryRisk.ValueMember = "ID";

            //-------------- Define StockExcahnges  List ------------------
            cmbStockExchanges.DataSource = Global.dtStockExchanges.Copy();
            cmbStockExchanges.DisplayMember = "Code";
            cmbStockExchanges.ValueMember = "ID";

            //-------------- Define cmbStockExchanges_Issue  List ------------------
            cmbStockExchanges_Issue.DataSource = Global.dtStockExchanges.Copy();
            cmbStockExchanges_Issue.DisplayMember = "Code";
            cmbStockExchanges_Issue.ValueMember = "ID";
            
            //-------------- Define Currencies List ------------------
            cmbCurrency.DataSource = Global.dtCurrencies.Copy();
            cmbCurrency.DisplayMember = "Title";
            cmbCurrency.ValueMember = "ID";

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
            dtView.RowFilter = "Product_ID = 2";
            cmbProductCategory.DataSource = dtView;
            cmbProductCategory.DisplayMember = "Title";
            cmbProductCategory.ValueMember = "ID";

            //-------------- Define Ranks List -----------------------------
            clsSystem System = new clsSystem();
            System = new clsSystem();
            System.GetRanks();
            cmbRank.DataSource = System.List.Copy();
            cmbRank.DisplayMember = "Title";
            cmbRank.ValueMember = "ID";

            //-------------- Define Coupone Types List ------------------
            System = new clsSystem();
            System.GetCouponeTypes();
            cmbCouponeType.DataSource = System.List.Copy(); 
            cmbCouponeType.DisplayMember = "Title";
            cmbCouponeType.ValueMember = "ID";

            //-------------- Define Revocation Rights List -----------------
            System = new clsSystem();
            System.GetRevocationRights(); 
            cmbRevocationRights.DataSource = System.List.Copy();
            cmbRevocationRights.DisplayMember = "Title";
            cmbRevocationRights.ValueMember = "ID";

            chkShowAktive.Checked = false;
            lblISIN_Warning.Text = "";

            csAktive = fgCodes.Styles.Add("Aktive");
            csAktive.ForeColor = Color.Black;
            csCancel = fgCodes.Styles.Add("Cancelled");
            csCancel.ForeColor = Color.Red;


            //-------------- Define Moodys Ratings List ------------------   
            dtView = Global.dtRatingCodes.Copy().DefaultView;
            dtView.RowFilter = "RatingAgency_ID = 0 OR RatingAgency_ID = 1";
            cmbMoodysRating.DataSource = dtView;
            cmbMoodysRating.DisplayMember = "Code";
            cmbMoodysRating.ValueMember = "ID";

            //-------------- Define Fitchs Ratings List ------------------   
            dtView = Global.dtRatingCodes.Copy().DefaultView;
            dtView.RowFilter = "RatingAgency_ID = 0 OR RatingAgency_ID = 2";
            cmbFitchsRating.DataSource = dtView;
            cmbFitchsRating.DisplayMember = "Code";
            cmbFitchsRating.ValueMember = "ID";

            //-------------- Define SP Ratings List ------------------   
            dtView = Global.dtRatingCodes.Copy().DefaultView;
            dtView.RowFilter = "RatingAgency_ID = 0 OR RatingAgency_ID = 3";
            cmbSPRating.DataSource = dtView;
            cmbSPRating.DisplayMember = "Code";
            cmbSPRating.ValueMember = "ID";

            //-------------- Define ICAP Ratings List ------------------   
            dtView = Global.dtRatingCodes.Copy().DefaultView;
            dtView.RowFilter = "RatingAgency_ID = 0 OR RatingAgency_ID = 4";
            cmbICAPRating.DataSource = dtView;
            cmbICAPRating.DisplayMember = "Code";
            cmbICAPRating.ValueMember = "ID";

            //-------------- Define Distribs List ------------------------
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

            bCheckList = true;
        }
        private void ShowTitleData()
        {
            cmbProductType.Text = klsProductTitle.ProductType;
            txtTitle.Text = klsProductTitle.ProductTitle;
            txtISIN.Text = klsProductTitle.ISIN;
            txtProviderName.Text = klsProductTitle.ProviderName;
            cmbProductCategory.SelectedValue = klsProductTitle.ProductCategory;
            cmbHFCategory.SelectedValue = klsProductTitle.HFCategory;
            cmbCountry.SelectedValue = klsProductTitle.Country_ID;
            cmbCountryGroup.SelectedValue = klsProductTitle.CountryGroup_ID;
            iSector_ID = klsProductTitle.Sector_ID;
            lblSector.Text = klsProductTitle.IndustryTitle + " / " + klsProductTitle.SectorTitle;
            cmbInvestType_Retail.SelectedIndex = klsProductTitle.InvestType_Retail;
            cmbInvestType_Prof.SelectedIndex = klsProductTitle.InvestType_Prof;
            cmbDistrib_ExecOnly.SelectedValue = klsProductTitle.Distrib_ExecOnly;
            cmbDistrib_Advice.SelectedValue = klsProductTitle.Distrib_Advice;
            cmbDistrib_PortfolioManagment.SelectedValue = klsProductTitle.Distrib_PortfolioManagment;
            cmbCountryRisk.SelectedValue = klsProductTitle.CountryRisk_ID;
            cmbRiskCurr.Text = klsProductTitle.RiskCurr;
            txtDescriptionEn.Text = klsProductTitle.DescriptionEn;
            txtDescriptionGr.Text = klsProductTitle.DescriptionGr;

            cmbComplexProduct.SelectedIndex = klsProductTitle.ComplexProduct;
            txtComplexAttribute.Text = klsProductTitle.ComplexAttribute;
            txtURL.Text = klsProductTitle.URL;
            txtIR_URL.Text = klsProductTitle.IR_URL;
            chkNonTradeable.Checked = klsProductTitle.NotTradeable == 1 ? true : false;
        }
        private void ShowCommonData()
        {
            cmbProductType.Text = klsProductTitle.ProductType;
            txtTitle.Text = klsProductTitle.ProductTitle;
            txtISIN.Text = klsProductTitle.ISIN;
            cmbCountry.SelectedValue = klsProductTitle.Country_ID;
            cmbCountryRisk.SelectedValue = klsProductTitle.CountryRisk_ID;
            iSector_ID = klsProductTitle.Sector_ID;
            lblSector.Text = klsProductTitle.IndustryTitle + " / " + klsProductTitle.SectorTitle;
            cmbRiskCurr.Text = klsProductTitle.RiskCurr;
            txtDescriptionGr.Text = klsProductTitle.DescriptionGr;
            txtURL.Text = klsProductTitle.URL;
            txtIR_URL.Text = klsProductTitle.IR_URL;
            cmbBondType.SelectedIndex = klsProductTitle.BondType;
            txtProviderName.Text = klsProductTitle.ProviderName;
            txtOfferingTypeDescription.Text = klsProductTitle.OfferingTypeDescription;
            txtAmountOutstanding.Text = klsProductTitle.AmountOutstanding.ToString();
            dAmountOutstanding.Text = klsProductTitle.AmountOutstandingDate;

            cmbMoodysRating.Text = klsProductTitle.MoodysRating;
            dMoodysRating.Text = klsProductTitle.MoodysRatingDate.ToString();
            cmbFitchsRating.Text = klsProductTitle.FitchsRating;
            dFitchsRating.Text = klsProductTitle.FitchsRatingDate.ToString();
            cmbSPRating.Text = klsProductTitle.SPRating;
            dSPRating.Text = klsProductTitle.SPRatingDate.ToString();
            cmbICAPRating.Text = klsProductTitle.ICAPRating;
            dICAPRating.Text = klsProductTitle.ICAPRatingDate.ToString();
            cmbRatingGroup.SelectedIndex = klsProductTitle.RatingGroup;

            txtCallDate.Text = klsProductTitle.CallDate;
            cmbRank.SelectedValue = klsProductTitle.Rank;
            txtDenominationType.Text = klsProductTitle.DenominationType;
            txtMinimumTotalLoss.Text = klsProductTitle.MinimumTotalLoss;
            cmbInflationProtected.SelectedIndex = klsProductTitle.InflationProtected;
            cmbIsProspectusAvailable.SelectedIndex = klsProductTitle.IsProspectusAvailable;
            cmbIsConvertible.SelectedIndex = klsProductTitle.IsConvertible;
            cmbIsDualCurrency.SelectedIndex = klsProductTitle.IsDualCurrency;
            cmbIsHybrid.SelectedIndex = klsProductTitle.IsHybrid;
            cmbIsGuaranteed.SelectedIndex = klsProductTitle.IsGuaranteed;
            cmbIsPerpetualSecurity.SelectedIndex = klsProductTitle.IsPerpetualSecurity;
            cmbIsTotalLoss.SelectedIndex = klsProductTitle.IsTotalLoss;
            cmbIsCallable.SelectedIndex = klsProductTitle.IsCallable;
            cmbIsPutable.SelectedIndex = klsProductTitle.IsPutable;

            cmbProductCategory.SelectedValue = klsProductTitle.ProductCategory;
            cmbHFCategory.SelectedValue = klsProductTitle.HFCategory;
            cmbCountryGroup.SelectedValue = klsProductTitle.CountryGroup_ID;

            cmbComplexProduct.SelectedIndex = klsProductTitle.ComplexProduct;
            txtComplexAttribute.Text = klsProductTitle.ComplexAttribute;

            cmbBBG_ComplexProduct.Text = klsProductTitle.BBG_ComplexProduct;
            txtBBG_ComplexAttribute.Text = klsProductTitle.BBG_ComplexAttribute;

            txtDescriptionEn.Text = klsProductTitle.DescriptionEn;
            txtDescriptionGr.Text = klsProductTitle.DescriptionGr;

            cmbInvestType_Retail.SelectedIndex = klsProductTitle.InvestType_Retail;
            cmbInvestType_Prof.SelectedIndex = klsProductTitle.InvestType_Prof;
            cmbInvestType_Eligible.SelectedIndex = klsProductTitle.InvestType_Eligible;
            cmbExpertise_Basic.SelectedValue = klsProductTitle.Expertise_Basic;
            cmbExpertise_Informed.SelectedValue = klsProductTitle.Expertise_Informed;
            cmbExpertise_Advanced.SelectedValue = klsProductTitle.Expertise_Advanced;
            cmbDistrib_ExecOnly.SelectedValue = klsProductTitle.Distrib_ExecOnly;
            cmbDistrib_Advice.SelectedValue = klsProductTitle.Distrib_Advice;
            cmbDistrib_PortfolioManagment.SelectedValue = klsProductTitle.Distrib_PortfolioManagment;

            txtRecHoldingPeriod.Text = klsProductTitle.RecHoldingPeriod;
        }
        private void ShowCodesList()
        {
            if (iShare_ID != 0) {

                fgCodes.Redraw = false;
                fgCodes.Rows.Count = 1;

                clsProductsCodes klsProductCode = new clsProductsCodes();
                klsProductCode.Share_ID = iShare_ID;
                klsProductCode.ISIN = "";
                klsProductCode.GetList();
                foreach (DataRow dtRow in klsProductCode.List.Rows) {
                    if (chkShowAktive.Checked || Convert.ToInt32(dtRow["Aktive"]) > 0) {
                        if (Convert.ToInt32(dtRow["Aktive"]) > 0) {
                            sTitle = txtTitle.Text + "";
                            sISIN = txtISIN.Text + "";
                        }
                        else {
                            sTitle = dtRow["CodeTitle"] + "";
                            sISIN = dtRow["ISIN"] + "";
                        }

                        fgCodes.AddItem(dtRow["ID"] + "\t" + dtRow["DateFrom"] + "\t" + dtRow["DateTo"] + "\t" + sTitle + "\t" + sISIN + "\t" +
                                        dtRow["Code"] + "\t" + dtRow["Code2"] + "\t" + dtRow["Code3"] + "\t" + dtRow["StockExchange_Code"] + "\t" +
                                        dtRow["CountryAction_Title"] + "\t" + dtRow["StockExchange_Issue_Code"] + "\t" + dtRow["CountryIssue_Title"] + "\t" +
                                        dtRow["Currency"] + "\t" + (Convert.ToDateTime(dtRow["Date1"]) != Convert.ToDateTime("01/01/1900")? dtRow["Date1"] : "") + "\t" +
                                        (Convert.ToDateTime(dtRow["Date2"]) != Convert.ToDateTime("01/01/1900")? dtRow["Date2"] : "") + "\t" +
                                        (Convert.ToDateTime(dtRow["Date3"]) != Convert.ToDateTime("01/01/1900") ? dtRow["Date3"] : "") + "\t" +
                                        (Convert.ToDateTime(dtRow["Date4"]) != Convert.ToDateTime("01/01/1900") ? dtRow["Date4"] : "") + "\t" +
                                        dtRow["CouponeType_Title"] + "\t" + (Convert.ToInt32(dtRow["Coupone"]) < 0? "N/A" : dtRow["Coupone"]) + "\t" + 
                                        (Convert.ToInt32(dtRow["LastCoupone"]) < 0 ? "N/A" : dtRow["LastCoupone"]) + "\t" +
                                        (Convert.ToInt32(dtRow["FrequencyClipping"]) < 0 ? "N/A" : dtRow["FrequencyClipping"]) + "\t" + 
                                        (Convert.ToSingle(dtRow["Price"]) < 0 ? "N/A" : dtRow["Price"]) + "\t" + dtRow["RevocationRights_Title"] + "\t" + 
                                        dtRow["Weight"] + "\t" + dtRow["QuantityMin"] + "\t" + dtRow["QuantityStep"] + "\t" +
                                        (Convert.ToInt32(dtRow["CoveredBond"]) == 1 ? "Yes" : "No") + "\t" + (Convert.ToInt32(dtRow["Limits"]) < 0 ? "N/A" : dtRow["Limits"]) + "\t" +
                                        ((dtRow["FloatingRate"]+"" == "N/A" || dtRow["FloatingRate"]+"" == "-1") ? "N/A" : dtRow["FloatingRate"]) + "\t" +
                                        dtRow["FRNFormula"] + "\t" + dtRow["MonthDays"] + "\t" + dtRow["BaseDays"] + "\t" + dtRow["HFIC_Recom_Title"] + "\t" + 
                                        dtRow["MIFID_Risk"] + "\t" + dtRow["StockExchange_ID"] + "\t" + dtRow["CountryAction_ID"] + "\t" + dtRow["Aktive"] + "\t" + 
                                        dtRow["CouponeType"] + "\t" + dtRow["RevocationRight"] + "\t" + "0" + "\t" + dtRow["CountryIssue_ID"] + "\t" +
                                        dtRow["StockExchange_Issue_ID"] + "\t" + "0" + "\t" + dtRow["HFIC_Recom"]);
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
                    klsProductCode.Share_ID = iShare_ID;
                    klsProductCode.DateFrom = DateTime.Now;
                    klsProductCode.DateTo = Convert.ToDateTime("2070/12/31");
                    klsProductCode.CodeTitle = txtTitle.Text;
                    klsProductCode.ISIN = txtNewISIN.Text;
                    klsProductCode.Code = fgCodes[i, "Code"] + "";
                    klsProductCode.Code2 = fgCodes[i, "Code2"] + "";
                    klsProductCode.Code3 = fgCodes[i, "Code3"] + "";
                    klsProductCode.MIFID_Risk = fgCodes[i, "MIFID_Risk"] + "";
                    klsProductCode.StockExchange_ID = Convert.ToInt32(fgCodes[i, "StockExchange_ID"]);
                    klsProductCode.CountryAction = Convert.ToInt32(fgCodes[i, "CountryAction_ID"]);
                    klsProductCode.Curr = fgCodes[i, "Curr"] + "";
                    klsProductCode.PrimaryShare = 0;
                    klsProductCode.CurrencyHedge = 0;
                    klsProductCode.CurrencyHedge2 = "";
                    klsProductCode.DistributionStatus = "";
                    klsProductCode.FrequencyClipping = 0;
                    klsProductCode.CouponeType = Convert.ToInt32(fgCodes[i, "CouponeType_ID"]);                                            // CouponeType_ID
                    klsProductCode.Coupone = Global.IsNumeric(fgCodes[i, "Coupone"]) ? Convert.ToSingle(fgCodes[i, "Coupone"]) :  -1;
                    klsProductCode.CountryIssue = Convert.ToInt32(fgCodes[i, "CountryIssue_ID"]);
                    klsProductCode.StockExchange_Issue_ID = Convert.ToInt32(fgCodes[i, "StockExchange_Issue_ID"]);

                    dTemp = Convert.ToDateTime("01/01/1900");
                    if (Global.IsDate(fgCodes[i, "Date1"]+"")) dTemp = Convert.ToDateTime(fgCodes[i, "Date1"]+"");
                    klsProductCode.Date1 = dTemp;

                    dTemp = Convert.ToDateTime("01/01/1900");
                    if (Global.IsDate(fgCodes[i, "Date2"] + "")) dTemp = Convert.ToDateTime(fgCodes[i, "Date2"] + "");
                    klsProductCode.Date2 = dTemp;

                    dTemp = Convert.ToDateTime("01/01/1900");
                    if (Global.IsDate(fgCodes[i, "Date3"] + "")) dTemp = Convert.ToDateTime(fgCodes[i, "Date3"] + "");
                    klsProductCode.Date3 = dTemp;

                    dTemp = Convert.ToDateTime("01/01/1900");
                    if (Global.IsDate(fgCodes[i, "Date4"] + "")) dTemp = Convert.ToDateTime(fgCodes[i, "Date4"] + "");
                    klsProductCode.Date4 = dTemp;

                    klsProductCode.MonthDays = fgCodes[i, "MonthDays"] + "";
                    klsProductCode.BaseDays = fgCodes[i, "BaseDays"] + "";
                    klsProductCode.CouponeType = Convert.ToInt32(fgCodes[i, "CouponeType_ID"]);
                    klsProductCode.Coupone = Convert.ToSingle(fgCodes[i, "Coupone"]);
                    if (Global.IsNumeric(fgCodes[i, "LastCoupone"])) klsProductCode.LastCoupone = Convert.ToSingle(fgCodes[i, "LastCoupone"]);
                    else klsProductCode.LastCoupone = -1;

                    if (Global.IsNumeric(fgCodes[i, "Price"])) klsProductCode.Price = Convert.ToSingle(fgCodes[i, "Price"]);
                    else klsProductCode.Price = -1;

                    if (Global.IsNumeric(fgCodes[i, "FrequencyClipping"])) klsProductCode.FrequencyClipping = Convert.ToInt32(fgCodes[i, "FrequencyClipping"]);
                    else klsProductCode.FrequencyClipping = -1;

                    klsProductCode.RevocationRight = Convert.ToInt32(fgCodes[i, "RevocationRight_ID"]);

                    klsProductCode.Gravity = Convert.ToSingle(fgCodes[i, "Gravity"]);
                    klsProductCode.QuantityMin = Convert.ToSingle(fgCodes[i, "QuantityMin"]);
                    klsProductCode.QuantityStep = Convert.ToSingle(fgCodes[i, "QuantityStep"]);
                    klsProductCode.CoveredBond = (fgCodes[i, "CoveredBond"] + "") == "Yes" ? 1 : 0;

                    if (Global.IsNumeric(fgCodes[i, "FloatingRate"])) klsProductCode.FloatingRate = fgCodes[i, "FloatingRate"] + "";
                    else klsProductCode.FloatingRate = "-1";

                    klsProductCode.FRNFormula = fgCodes[i, "FRNFormula"] + "";
                    if (Global.IsNumeric(fgCodes[i, "Limits"])) klsProductCode.Limits = Convert.ToSingle(fgCodes[i, "Limits"]);
                    else klsProductCode.Limits = -1;
         
                    klsProductCode.HFIC_Recom = Convert.ToInt32(fgCodes[i, "HFIC_Recom_ID"]);
                    klsProductCode.Aktive = 1;
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
            lblCode.Text = "";
            i = fgCodes.Row;
            ShowCodeMask();
            ShowCodeData();
            btnOKCode.Enabled = false;
            btnCancelCode.Enabled = false;
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

        private void menuCallReutersCom_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.reuters.com/finance/stocks/overview?symbol=" + fgCodes[fgCodes.Row, "Code"]);
        }

        private void menuCallBloombergCom_Click(object sender, EventArgs e)
        {
            sTemp = fgCodes[fgCodes.Row, "Code2"] + "";
            Process.Start("http://www.bloomberg.com/quote/" + sTemp.Replace(" ", ":"));
        }
        private void picClean_Click(object sender, EventArgs e)
        {
            iSector_ID = 0;
            lblSector.Text = "";
        }
        private void cmbCountryRisk_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) {
                foundRows = Global.dtCountries.Select("ID = " + cmbCountry.SelectedValue);
                cmbCountryGroup.SelectedValue = foundRows[0]["CountriesGroup_ID"];
            }
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

                                if (txtAmountOutstanding.Text == "") txtAmountOutstanding.Text = "0";

                                if (iAction == 0) {                                                   // 0 - ADD Mode
                                    //--- add record into Shares table -------------------------------
                                    clsProducts klsProduct = new clsProducts();
                                    klsProduct.Product_ID = iProduct_ID;
                                    klsProduct.Aktive = 1;
                                    iShare_ID = klsProduct.InsertRecord();

                                    //--- add record into ShareTitles table --------------------------
                                    clsProductsTitles klsProductTitle = new clsProductsTitles();
                                    klsProductTitle.Share_ID = iShare_ID;
                                    klsProductTitle.ProductTitle = txtTitle.Text;
                                    klsProductTitle.ProviderName = txtProviderName.Text;
                                    klsProductTitle.ISIN = txtISIN.Text;
                                    klsProductTitle.BondType = cmbBondType.SelectedIndex;
                                    klsProductTitle.ProductCategory = Convert.ToInt32(cmbProductCategory.SelectedValue);
                                    klsProductTitle.HFCategory = Convert.ToInt32(cmbHFCategory.SelectedValue);
                                    klsProductTitle.Country_ID = Convert.ToInt32(cmbCountry.SelectedValue);
                                    klsProductTitle.CountryGroup_ID = Convert.ToInt32(cmbCountryGroup.SelectedValue);
                                    klsProductTitle.Sector_ID = iSector_ID;
                                    klsProductTitle.SectorTitle = lblSector.Text;
                                    klsProductTitle.CountryRisk_ID = Convert.ToInt32(cmbCountryRisk.SelectedValue);
                                    klsProductTitle.RiskCurr = cmbRiskCurr.Text;
                                    klsProductTitle.DescriptionEn = txtDescriptionEn.Text;
                                    klsProductTitle.DescriptionGr = txtDescriptionGr.Text;
                                    klsProductTitle.OfferingTypeDescription = txtOfferingTypeDescription.Text;
                                    klsProductTitle.AmountOutstanding = Convert.ToDecimal(txtAmountOutstanding.Text);
                                    klsProductTitle.AmountOutstandingDate = dAmountOutstanding.Text;
                                    klsProductTitle.URL = txtURL.Text;
                                    klsProductTitle.IR_URL = txtIR_URL.Text;
                                    klsProductTitle.MoodysRating = cmbMoodysRating.Text;
                                    klsProductTitle.MoodysRatingDate = Convert.ToDateTime(dMoodysRating.Text);
                                    klsProductTitle.FitchsRating = cmbFitchsRating.Text;
                                    klsProductTitle.FitchsRatingDate = Convert.ToDateTime(dFitchsRating.Text);
                                    klsProductTitle.SPRating = cmbSPRating.Text;
                                    klsProductTitle.SPRatingDate = Convert.ToDateTime(dSPRating.Text);
                                    klsProductTitle.ICAPRating = cmbICAPRating.Text;
                                    klsProductTitle.ICAPRatingDate = Convert.ToDateTime(dICAPRating.Text);
                                    klsProductTitle.RatingGroup = cmbRatingGroup.SelectedIndex;
                                    klsProductTitle.CallDate = txtCallDate.Text;
                                    klsProductTitle.Rank = Convert.ToInt32(cmbRank.SelectedValue);
                                    klsProductTitle.DenominationType = txtDenominationType.Text;
                                    klsProductTitle.MinimumTotalLoss = txtMinimumTotalLoss.Text;
                                    klsProductTitle.InflationProtected = cmbInflationProtected.SelectedIndex;
                                    klsProductTitle.BBG_ComplexProduct = cmbBBG_ComplexProduct.Text;
                                    klsProductTitle.BBG_ComplexAttribute = txtBBG_ComplexAttribute.Text;
                                    klsProductTitle.IsProspectusAvailable = cmbIsProspectusAvailable.SelectedIndex;
                                    klsProductTitle.IsConvertible = cmbIsConvertible.SelectedIndex;
                                    klsProductTitle.IsDualCurrency = cmbIsDualCurrency.SelectedIndex;
                                    klsProductTitle.IsHybrid = cmbIsHybrid.SelectedIndex;
                                    klsProductTitle.IsGuaranteed = cmbIsGuaranteed.SelectedIndex;
                                    klsProductTitle.IsPerpetualSecurity = cmbIsPerpetualSecurity.SelectedIndex;
                                    klsProductTitle.IsTotalLoss = cmbIsTotalLoss.SelectedIndex;
                                    klsProductTitle.IsCallable = cmbIsCallable.SelectedIndex;
                                    klsProductTitle.IsPutable = cmbIsPutable.SelectedIndex;
                                    klsProductTitle.ComplexProduct = cmbComplexProduct.SelectedIndex;
                                    klsProductTitle.ComplexAttribute = txtComplexAttribute.Text;
                                    klsProductTitle.InvestType_Retail = Convert.ToInt16(cmbInvestType_Retail.SelectedIndex);
                                    klsProductTitle.InvestType_Prof = Convert.ToInt16(cmbInvestType_Prof.SelectedIndex);
                                    klsProductTitle.InvestType_Eligible = Convert.ToInt16(cmbInvestType_Eligible.SelectedIndex);
                                    klsProductTitle.Distrib_ExecOnly = Convert.ToInt16(cmbDistrib_ExecOnly.SelectedValue);
                                    klsProductTitle.Distrib_Advice = Convert.ToInt16(cmbDistrib_Advice.SelectedValue);
                                    klsProductTitle.Distrib_PortfolioManagment = Convert.ToInt16(cmbDistrib_PortfolioManagment.SelectedValue);
                                    klsProductTitle.LastEditDate = DateTime.Now;
                                    klsProductTitle.LastEditUser_ID = Global.User_ID;
                                    klsProductTitle.NotTradeable = chkNonTradeable.Checked ? 1 : 0;
                                    iShareTitle_ID = klsProductTitle.InsertRecord();

                                    clsProductsCodes klsProductCode = new clsProductsCodes();
                                    for (i = 1; i <= fgCodes.Rows.Count - 1; i++) {
                                        //--- add record into ShareCodes table --------------------------
                                        klsProductCode = new clsProductsCodes();

                                        klsProductCode.Share_ID = iShare_ID;
                                        klsProductCode.DateFrom = Convert.ToDateTime(fgCodes[i, "DateFrom"]);
                                        klsProductCode.DateTo = Convert.ToDateTime(fgCodes[i, "DateTo"]);
                                        klsProductCode.CodeTitle = txtTitle.Text;
                                        klsProductCode.ISIN = txtISIN.Text;
                                        klsProductCode.Code = fgCodes[i, "Code"] + "";
                                        klsProductCode.Code2 = fgCodes[i, "Code2"] + "";
                                        klsProductCode.Code3 = fgCodes[i, "Code3"] + "";
                                        klsProductCode.MIFID_Risk = fgCodes[i, "MIFID_Risk"] + "";
                                        klsProductCode.StockExchange_ID = Convert.ToInt32(fgCodes[i, "StockExchange_ID"]);
                                        klsProductCode.PrimaryShare = 0;
                                        klsProductCode.Curr = fgCodes[i, "Curr"] + "";
                                        klsProductCode.CurrencyHedge = 0;
                                        klsProductCode.CurrencyHedge2 = "";
                                        klsProductCode.DistributionStatus = "";
                                        klsProductCode.FrequencyClipping = 0;
                                        klsProductCode.CouponeType = Convert.ToInt32(fgCodes[i, "CouponeType_ID"]);        
                                        klsProductCode.Coupone = Global.IsNumeric(fgCodes[i, "Coupone"]) ? Convert.ToSingle(fgCodes[i, "Coupone"]) : -1;
                                        klsProductCode.CountryIssue = Convert.ToInt32(fgCodes[i, "CountryIssue_ID"]);
                                        klsProductCode.StockExchange_Issue_ID = Convert.ToInt32(fgCodes[i, "StockExchange_Issue_ID"]);

                                        if (Global.IsDate(fgCodes[i, "Date1"]+"")) dTemp = Convert.ToDateTime(fgCodes[i, "Date1"]);
                                        else  dTemp = Convert.ToDateTime("01/01/1900");
                                        klsProductCode.Date1 = dTemp;

                                        if (Global.IsDate(fgCodes[i, "Date2"] + "")) dTemp = Convert.ToDateTime(fgCodes[i, "Date2"]);
                                        else dTemp = Convert.ToDateTime("01/01/1900");
                                        klsProductCode.Date2 = dTemp;

                                        if (Global.IsDate(fgCodes[i, "Date3"] + "")) dTemp = Convert.ToDateTime(fgCodes[i, "Date3"]);
                                        else dTemp = Convert.ToDateTime("01/01/1900");
                                        klsProductCode.Date3 = dTemp;

                                        if (Global.IsDate(fgCodes[i, "Date4"] + "")) dTemp = Convert.ToDateTime(fgCodes[i, "Date4"]);
                                        else dTemp = Convert.ToDateTime("01/01/1900");
                                        klsProductCode.Date4 = dTemp;
                                       
                                        klsProductCode.MonthDays = fgCodes[i, "MonthDays"]+"";
                                        klsProductCode.BaseDays = fgCodes[i, "BaseDays"]+"";
                                        klsProductCode.CouponeType = Convert.ToInt32(fgCodes[i, "CouponeType_ID"]);
                                        klsProductCode.Coupone = Convert.ToSingle(fgCodes[i, "Coupone"]);
                                      
                                        if (Global.IsNumeric(fgCodes[i, "LastCoupone"])) klsProductCode.LastCoupone = Convert.ToSingle(fgCodes[i, "LastCoupone"]);
                                        else  klsProductCode.LastCoupone = -1;

                                        if (Global.IsNumeric(fgCodes[i, "Price"]))  klsProductCode.Price = Convert.ToSingle(fgCodes[i, "Price"]);
                                        klsProductCode.Price = -1;

                                        if (Global.IsNumeric(fgCodes[i, "FrequencyClipping"])) klsProductCode.FrequencyClipping = Convert.ToInt32(fgCodes[i, "FrequencyClipping"]);
                                        klsProductCode.FrequencyClipping = -1;

                                        klsProductCode.RevocationRight = Convert.ToInt32(fgCodes[i, "RevocationRight_ID"]);

                                        klsProductCode.Gravity = Convert.ToSingle(fgCodes[i, "Gravity"]);
                                        klsProductCode.QuantityMin = Convert.ToSingle(fgCodes[i, "QuantityMin"]);
                                        klsProductCode.QuantityStep = Convert.ToSingle(fgCodes[i, "QuantityStep"]);
                                        klsProductCode.CoveredBond = (fgCodes[i, "CoveredBond"]+"") == "Yes"? 1 : 0;

                                        if (Global.IsNumeric(fgCodes[i, "FloatingRate"])) klsProductCode.FloatingRate = fgCodes[i, "FloatingRate"] + "";
                                        else klsProductCode.FloatingRate = "-1";

                                        klsProductCode.FRNFormula = fgCodes[i, "FRNFormula"] + "";

                                        if (Global.IsNumeric(fgCodes[i, "Limits"])) klsProductCode.Limits = Convert.ToSingle(fgCodes[i, "Limits"]);
                                        else klsProductCode.Limits = -1;

                                        klsProductCode.Aktive = (fgCodes[i, "Aktive"] + "") == "0" ? 0 : 1;
                                        klsProductCode.HFIC_Recom = Convert.ToInt16(fgCodes[i, "HFIC_Recom_ID"]);

                                        if ((fgCodes[i, "QuantityMin"] + "") != "") klsProductCode.QuantityMin = Convert.ToSingle(fgCodes[i, "QuantityMin"]);
                                        else klsProductCode.QuantityMin = -1;
                                        klsProductCode.Gravity = Convert.ToSingle(fgCodes[i, "Gravity"]);
                                        klsProductCode.DateIPO = Convert.ToDateTime("1900/01/01");
                                        klsProductCode.HFIC_Recom = Convert.ToInt32(fgCodes[i, "HFIC_Recom_ID"]);
                                        
                                        klsProductCode.Aktive = (Convert.ToInt32(fgCodes[i, "Aktive"]) == 0 ? 0 : 1);
                                        iShareCode_ID = klsProductCode.InsertRecord();

                                        //--- add record into Shares_Titles_Codes table -------------------
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
                                    //--- edit record into ShareTitles table --------------------------
                                    clsProductsTitles klsProductTitle = new clsProductsTitles();
                                    klsProductTitle.Record_ID = iShareTitle_ID;
                                    klsProductTitle.GetRecord();

                                    klsProductTitle.ProductTitle = txtTitle.Text;
                                    klsProductTitle.ProviderName = txtProviderName.Text;
                                    klsProductTitle.ISIN = txtISIN.Text;
                                    klsProductTitle.BondType = cmbBondType.SelectedIndex;
                                    klsProductTitle.ProductCategory = Convert.ToInt32(cmbProductCategory.SelectedValue);
                                    klsProductTitle.HFCategory = Convert.ToInt32(cmbHFCategory.SelectedValue);
                                    klsProductTitle.Country_ID = Convert.ToInt32(cmbCountry.SelectedValue);
                                    klsProductTitle.CountryGroup_ID = Convert.ToInt32(cmbCountryGroup.SelectedValue);
                                    klsProductTitle.Sector_ID = iSector_ID;
                                    klsProductTitle.SectorTitle = lblSector.Text;
                                    klsProductTitle.CountryRisk_ID = Convert.ToInt32(cmbCountryRisk.SelectedValue);
                                    klsProductTitle.RiskCurr = cmbRiskCurr.Text;
                                    klsProductTitle.DescriptionEn = txtDescriptionEn.Text;
                                    klsProductTitle.DescriptionGr = txtDescriptionGr.Text;
                                    klsProductTitle.OfferingTypeDescription = txtOfferingTypeDescription.Text;
                                    klsProductTitle.AmountOutstanding = Convert.ToDecimal(txtAmountOutstanding.Text);
                                    klsProductTitle.AmountOutstandingDate = dAmountOutstanding.Text;
                                    klsProductTitle.URL = txtURL.Text;
                                    klsProductTitle.IR_URL = txtIR_URL.Text;
                                    klsProductTitle.MoodysRating = cmbMoodysRating.Text;
                                    klsProductTitle.MoodysRatingDate = Convert.ToDateTime(dMoodysRating.Text);
                                    klsProductTitle.FitchsRating = cmbFitchsRating.Text;
                                    klsProductTitle.FitchsRatingDate = Convert.ToDateTime(dFitchsRating.Text);
                                    klsProductTitle.SPRating = cmbSPRating.Text;
                                    klsProductTitle.SPRatingDate = Convert.ToDateTime(dSPRating.Text);
                                    klsProductTitle.ICAPRating = cmbICAPRating.Text;
                                    klsProductTitle.ICAPRatingDate = Convert.ToDateTime(dICAPRating.Text);
                                    klsProductTitle.RatingGroup = cmbRatingGroup.SelectedIndex;
                                    klsProductTitle.CallDate = txtCallDate.Text;
                                    klsProductTitle.Rank = Convert.ToInt32(cmbRank.SelectedValue);
                                    klsProductTitle.DenominationType = txtDenominationType.Text;
                                    klsProductTitle.MinimumTotalLoss = txtMinimumTotalLoss.Text;
                                    klsProductTitle.InflationProtected = cmbInflationProtected.SelectedIndex;
                                    klsProductTitle.BBG_ComplexProduct = cmbBBG_ComplexProduct.Text;
                                    klsProductTitle.BBG_ComplexAttribute = txtBBG_ComplexAttribute.Text;
                                    klsProductTitle.IsProspectusAvailable = cmbIsProspectusAvailable.SelectedIndex;
                                    klsProductTitle.IsConvertible = cmbIsConvertible.SelectedIndex;
                                    klsProductTitle.IsDualCurrency = cmbIsDualCurrency.SelectedIndex;
                                    klsProductTitle.IsHybrid = cmbIsHybrid.SelectedIndex;
                                    klsProductTitle.IsGuaranteed = cmbIsGuaranteed.SelectedIndex;
                                    klsProductTitle.IsPerpetualSecurity = cmbIsPerpetualSecurity.SelectedIndex;
                                    klsProductTitle.IsTotalLoss = cmbIsTotalLoss.SelectedIndex;
                                    klsProductTitle.IsCallable = cmbIsCallable.SelectedIndex;
                                    klsProductTitle.IsPutable = cmbIsPutable.SelectedIndex;
                                    klsProductTitle.ComplexProduct = cmbComplexProduct.SelectedIndex;
                                    klsProductTitle.ComplexAttribute = txtComplexAttribute.Text;
                                    klsProductTitle.InvestType_Retail = Convert.ToInt16(cmbInvestType_Retail.SelectedIndex);
                                    klsProductTitle.InvestType_Prof = Convert.ToInt16(cmbInvestType_Prof.SelectedIndex);
                                    klsProductTitle.InvestType_Eligible = Convert.ToInt16(cmbInvestType_Eligible.SelectedIndex);
                                    klsProductTitle.Distrib_ExecOnly = Convert.ToInt16(cmbDistrib_ExecOnly.SelectedValue);
                                    klsProductTitle.Distrib_Advice = Convert.ToInt16(cmbDistrib_Advice.SelectedValue);
                                    klsProductTitle.Distrib_PortfolioManagment = Convert.ToInt16(cmbDistrib_PortfolioManagment.SelectedValue);
                                    klsProductTitle.LastEditDate = DateTime.Now;
                                    klsProductTitle.LastEditUser_ID = Global.User_ID;
                                    klsProductTitle.NotTradeable = chkNonTradeable.Checked ? 1 : 0;
                                    klsProductTitle.EditRecord();

                                    for (i = 1; i <= fgCodes.Rows.Count - 1; i++) {
                                        if (Convert.ToInt32(fgCodes[i, "Edited"]) == 1) {
                                            //--- edit record into ShareCodes table --------------------------
                                            clsProductsCodes klsProductCode = new clsProductsCodes();
                                            klsProductCode.Record_ID = Convert.ToInt32(fgCodes[i, 0]);
                                            klsProductCode.GetRecord();
                                            klsProductCode.Share_ID = iShare_ID;
                                            klsProductCode.DateFrom = Convert.ToDateTime(fgCodes[i, "DateFrom"]);
                                            klsProductCode.DateTo = Convert.ToDateTime(fgCodes[i, "DateTo"]);
                                            klsProductCode.CodeTitle = txtTitle.Text;
                                            klsProductCode.ISIN = txtISIN.Text;
                                            klsProductCode.Code = fgCodes[i, "Code"] + "";
                                            klsProductCode.Code2 = fgCodes[i, "Code2"] + "";
                                            klsProductCode.Code3 = fgCodes[i, "Code3"] + "";
                                            klsProductCode.MIFID_Risk = fgCodes[i, "MIFID_Risk"] + "";
                                            klsProductCode.StockExchange_ID = Convert.ToInt32(fgCodes[i, "StockExchange_ID"]);
                                            klsProductCode.PrimaryShare = 0;
                                            klsProductCode.Curr = fgCodes[i, "Curr"] + "";
                                            klsProductCode.CurrencyHedge = 0;
                                            klsProductCode.CurrencyHedge2 = "";
                                            klsProductCode.DistributionStatus = "";
                                            klsProductCode.FrequencyClipping = 0;
                                            klsProductCode.CouponeType = Convert.ToInt32(fgCodes[i, "CouponeType_ID"]);
                                            klsProductCode.Coupone = Global.IsNumeric(fgCodes[i, "Coupone"]) ? Convert.ToSingle(fgCodes[i, "Coupone"]) : -1;
                                            klsProductCode.CountryIssue = Convert.ToInt32(fgCodes[i, "CountryIssue_ID"]);
                                            klsProductCode.StockExchange_Issue_ID = Convert.ToInt32(fgCodes[i, "StockExchange_Issue_ID"]);

                                            if (Global.IsDate(fgCodes[i, "Date1"] + "")) dTemp = Convert.ToDateTime(fgCodes[i, "Date1"]);
                                            else dTemp = Convert.ToDateTime("01/01/1900");
                                            klsProductCode.Date1 = dTemp;

                                            if (Global.IsDate(fgCodes[i, "Date2"] + "")) dTemp = Convert.ToDateTime(fgCodes[i, "Date2"]);
                                            else dTemp = Convert.ToDateTime("01/01/1900");
                                            klsProductCode.Date2 = dTemp;

                                            if (Global.IsDate(fgCodes[i, "Date3"] + "")) dTemp = Convert.ToDateTime(fgCodes[i, "Date3"]);
                                            else dTemp = Convert.ToDateTime("01/01/1900");
                                            klsProductCode.Date3 = dTemp;

                                            if (Global.IsDate(fgCodes[i, "Date4"] + "")) dTemp = Convert.ToDateTime(fgCodes[i, "Date4"]);
                                            else dTemp = Convert.ToDateTime("01/01/1900");
                                            klsProductCode.Date4 = dTemp;

                                            klsProductCode.MonthDays = fgCodes[i, "MonthDays"] + "";
                                            klsProductCode.BaseDays = fgCodes[i, "BaseDays"] + "";
                                            klsProductCode.CouponeType = Convert.ToInt32(fgCodes[i, "CouponeType_ID"]);
                                            klsProductCode.Coupone = Convert.ToSingle(fgCodes[i, "Coupone"]);

                                            if (Global.IsNumeric(fgCodes[i, "LastCoupone"])) klsProductCode.LastCoupone = Convert.ToSingle(fgCodes[i, "LastCoupone"]);
                                            else klsProductCode.LastCoupone = -1;

                                            if (Global.IsNumeric(fgCodes[i, "Price"])) klsProductCode.Price = Convert.ToSingle(fgCodes[i, "Price"]);
                                            klsProductCode.Price = -1;

                                            if (Global.IsNumeric(fgCodes[i, "FrequencyClipping"])) klsProductCode.FrequencyClipping = Convert.ToInt32(fgCodes[i, "FrequencyClipping"]);
                                            klsProductCode.FrequencyClipping = -1;

                                            klsProductCode.RevocationRight = Convert.ToInt32(fgCodes[i, "RevocationRight_ID"]);

                                            klsProductCode.Gravity = Convert.ToSingle(fgCodes[i, "Gravity"]);
                                            klsProductCode.QuantityMin = Convert.ToSingle(fgCodes[i, "QuantityMin"]);
                                            klsProductCode.QuantityStep = Convert.ToSingle(fgCodes[i, "QuantityStep"]);
                                            klsProductCode.CoveredBond = (fgCodes[i, "CoveredBond"] + "") == "Yes" ? 1 : 0;

                                            if (Global.IsNumeric(fgCodes[i, "FloatingRate"])) klsProductCode.FloatingRate = fgCodes[i, "FloatingRate"] + "";
                                            else klsProductCode.FloatingRate = "-1";

                                            klsProductCode.FRNFormula = fgCodes[i, "FRNFormula"] + "";

                                            if (Global.IsNumeric(fgCodes[i, "Limits"])) klsProductCode.Limits = Convert.ToSingle(fgCodes[i, "Limits"]);
                                            else klsProductCode.Limits = -1;

                                            klsProductCode.Aktive = (fgCodes[i, "Aktive"] + "") == "0" ? 0 : 1;
                                            klsProductCode.HFIC_Recom = Convert.ToInt16(fgCodes[i, "HFIC_Recom_ID"]);

                                            if ((fgCodes[i, "QuantityMin"] + "") != "") klsProductCode.QuantityMin = Convert.ToSingle(fgCodes[i, "QuantityMin"]);
                                            else klsProductCode.QuantityMin = -1;
                                            klsProductCode.Gravity = Convert.ToSingle(fgCodes[i, "Gravity"]);
                                            klsProductCode.DateIPO = Convert.ToDateTime("1900/01/01");
                                            klsProductCode.HFIC_Recom = Convert.ToInt32(fgCodes[i, "HFIC_Recom_ID"]);

                                            if (Convert.ToInt32(fgCodes[i, "ID"]) == 0)  {
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
                                            else {
                                                //--- Edit Record --------
                                                iShareCode_ID = Convert.ToInt32(fgCodes[i, "ID"]);
                                                klsProductCode.Record_ID = iShareCode_ID;
                                                klsProductCode.EditRecord();
                                            }
                                        }
                                        else iShareCode_ID = Convert.ToInt32(fgCodes[i, "ID"]);

                                        if (Convert.ToInt32(fgCodes[i, "Old_ID"]) != 0) {
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

                                //--- change EDIT data ----------------------------------------------
                                CashTable.Record_ID = 41;                    // ListsTables.ID = 41 - ShareCodes
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
            iActionMode = 0;                                          //0 - Add, 1 - Tropopoiisi, 2 - Allagi
            lblCode.Text = "Καταχωρίστε στοιχεία νέου κωδικού ";
            dFrom.Value = Convert.ToDateTime("01/01/" + DateTime.Now.Year);
            dTo.Value = Convert.ToDateTime("31/12/2070");
            txtTitleCode.Text = txtTitle.Text;
            txtISINCode.Text = txtISIN.Text;

            txtReutersCode.Text = "";
            txtBloombergCode.Text = "";
            txtExchangeTicker.Text = "";
            cmbCountryAction.Text = "Global";
            cmbStockExchanges.Text = "OTC";
            cmbCountryIssue.SelectedValue = 0;
            cmbStockExchanges_Issue.SelectedValue = 0;
            cmbCurrency.Text = "";
            txtWeight.Text = "0";
            dIssued.Value = DateTime.Now;
            dDeadLine.Value = DateTime.Now;
            dFirstDiak.Value = DateTime.Now;
            dFirst.Value = DateTime.Now;
            cmbMonthDays.SelectedIndex = 0;
            cmbBaseDays.SelectedIndex = 0;
            txtCoupone.Text = "0";
            txtLastCoupone.Text = "0";
            txtPrice.Text = "0";
            txtFrequencyClipping.Text = "0";
            cmbCouponeType.SelectedValue = 0;
            cmbRevocationRights.SelectedValue = 0;
            txtQuantityMin.Text = "0";
            txtQuantityStep.Text = "0";
            cmbCoveredBond.Text = "No";
            txtFloatingRate.Text = "";
            txtFRNFormula.Text = "";
            txtLimits.Text = "0";
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
        }
        private void tslEdit_Click(object sender, EventArgs e)
        {
            if (fgCodes.Rows.Count > 1) {
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

        private void dIssued_ValueChanged(object sender, EventArgs e)
        {
            dIssued.CustomFormat = "dd/MM/yyyy";
        }

        private void dDeadLine_ValueChanged(object sender, EventArgs e)
        {
            dDeadLine.CustomFormat = "dd/MM/yyyy";
        }

        private void dFirstDiak_ValueChanged(object sender, EventArgs e)
        {
            dFirstDiak.CustomFormat = "dd/MM/yyyy";
        }

        private void dFirst_ValueChanged(object sender, EventArgs e)
        {
            dFirst.CustomFormat = "dd/MM/yyyy";
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

        private void picKey_Click(object sender, EventArgs e)
        {
            cmbProductType.Enabled = true;
            bEditProductType = true;
            picKey.Visible = false;
        }

        private void tslChange_Click(object sender, EventArgs e)
        {
            iActionMode = 2;                       // 0 - Add, 1 - Tropopoiisi, 2 - Allagi
            lblCode.Text = "Με την ενέργια αυτήν θα ακυρωθούν τρέχον στοιχεία κωδικού και θα καταχωριθούν στοιχεία νέου κωδικου";
            dFrom.Value = DateTime.Now;
            dTo.Value = Convert.ToDateTime("31-12-2070");
            txtTitleCode.Text = txtTitle.Text;
            txtISINCode.Text = txtISIN.Text;
            txtReutersCode.Text = "";
            txtBloombergCode.Text = "";
            cmbCountryAction.SelectedValue = 0;
            cmbStockExchanges.SelectedValue = 0;
            cmbCurrency.Text = "";
            txtWeight.Text = "0";
            dIssued.Value = DateTime.Now;
            dDeadLine.Value = DateTime.Now;
            dFirstDiak.Value = DateTime.Now;
            dFirst.Value = DateTime.Now;
            cmbCouponeType.SelectedValue = 0;
            txtCoupone.Text = "0";
            txtLastCoupone.Text = "0";
            txtPrice.Text = "0";
            txtFrequencyClipping.Text = "0";
            cmbRevocationRights.SelectedValue = 0;
            txtQuantityMin.Text = "0";
            txtQuantityStep.Text = "0";
            cmbCoveredBond.Text = "No";
            txtFloatingRate.Text = "";
            txtFRNFormula.Text = "";
            txtLimits.Text = "0";
            cmbRatingGroup.SelectedIndex = 0;
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
            if (fgCodes.Row > 0) {
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
                                    txtReutersCode.Text + "\t" + txtBloombergCode.Text + "\t" + txtExchangeTicker.Text + "\t" + cmbStockExchanges.Text + "\t" +
                                    cmbCountryAction.Text + "\t" + cmbStockExchanges_Issue.Text + "\t" + cmbCountryIssue.Text + "\t" + cmbCurrency.Text + "\t" +
                                    dIssued.Value + "\t" + dDeadLine.Value + "\t" + dFirstDiak.Value + "\t" + dFirst.Value + "\t" + cmbCouponeType.Text + "\t" +
                                    txtCoupone.Text + "\t" + txtLastCoupone.Text + "\t" + txtFrequencyClipping.Text + "\t" + txtPrice.Text + "\t" + cmbRevocationRights.Text + "\t" +
                                    txtWeight.Text + "\t" + txtQuantityMin.Text + "\t" + txtQuantityStep.Text + "\t" + cmbCoveredBond.SelectedIndex + "\t" + txtLimits.Text + "\t" +
                                    txtFloatingRate.Text + "\t" + txtFRNFormula.Text + "\t" + cmbMonthDays.Text + "\t" + cmbBaseDays.Text + "\t" +
                                    (cmbHFIC_Recom.SelectedIndex == 1 ? "Yes" : "No") + "\t" + sMIFID_Risk + "\t" + cmbStockExchanges.SelectedValue + "\t" +
                                    cmbCountryAction.SelectedValue + "\t" + "1" + "\t" + cmbCouponeType.SelectedValue + "\t" + cmbRevocationRights.SelectedValue + "\t" +
                                    "0" + "\t" + cmbCountryIssue.SelectedValue + "\t" + cmbStockExchanges_Issue.SelectedValue + "\t" +
                                    "1" + "\t" + (cmbHFIC_Recom.SelectedIndex == 1 ? 1 : 0));
                    break;
                case 1:
                    i = fgCodes.Row;
                    fgCodes[i, "DateFrom"] = dFrom.Value;
                    fgCodes[i, "DateTo"] = dTo.Value;
                    fgCodes[i, "Title"] = txtTitle.Text;
                    fgCodes[i, "ISIN"] = txtISIN.Text;
                    fgCodes[i, "Code"] = txtReutersCode.Text;
                    fgCodes[i, "Code2"] = txtBloombergCode.Text;
                    fgCodes[i, "Code3"] = txtExchangeTicker.Text;
                    fgCodes[i, "StockExchange_Code"] = cmbStockExchanges.Text;
                    fgCodes[i, "Country_Title"] = cmbCountryAction.Text;
                    fgCodes[i, "Curr"] = cmbCurrency.Text;
                    fgCodes[i, "Gravity"] = txtWeight.Text;
                    fgCodes[i, "HFIC_Recom"] = cmbHFIC_Recom.Text;
                    fgCodes[i, "MIFID_Risk"] = sMIFID_Risk;
                    fgCodes[i, "Aktive"] = chkAktive.Checked ? "1" : "0";
                    fgCodes[i, "Old_ID"] = 0;
                    fgCodes[i, "HFIC_Recom_ID"] = cmbHFIC_Recom.SelectedIndex;
                    fgCodes[i, "Country_Issues"] = cmbCountryIssue.Text;
                    fgCodes[i, "StockExhange_Issues"] = cmbStockExchanges_Issue.Text;
                    
                    if (dIssued.Text.Trim().Length > 0)  fgCodes[i, "Date1"] = dIssued.Value.ToString("dd/MM/yyyy");
                    else  fgCodes[i, "Date1"] = "";

                    if (dDeadLine.Text.Trim().Length > 0) fgCodes[i, "Date2"] = dDeadLine.Value.ToString("dd/MM/yyyy");
                    else fgCodes[i, "Date2"] = "";

                    if (dFirstDiak.Text.Trim().Length > 0) fgCodes[i, "Date3"] = dFirstDiak.Value.ToString("dd/MM/yyyy");
                    else fgCodes[i, "Date3"] = "";

                    if (dFirst.Text.Trim().Length > 0) fgCodes[i, "Date4"] = dFirst.Value.ToString("dd/MM/yyyy");
                    else fgCodes[i, "Date4"] = "";

                    fgCodes[i, "MonthDays"] = cmbMonthDays.Text;
                    fgCodes[i, "BaseDays"] = cmbBaseDays.Text;
                    fgCodes[i, "CouponeType"] = cmbCouponeType.Text;
                    fgCodes[i, "Coupone"] = txtCoupone.Text;
                    fgCodes[i, "LastCoupone"] = txtLastCoupone.Text;
                    fgCodes[i, "Price"] = txtPrice.Text;
                    fgCodes[i, "FrequencyClipping"] = txtFrequencyClipping.Text;
                    fgCodes[i, "RevocationRight_Title"] = cmbRevocationRights.Text;
                    fgCodes[i, "QuantityMin"] = txtQuantityMin.Text;
                    fgCodes[i, "QuantityStep"] = txtQuantityStep.Text;
                    fgCodes[i, "CoveredBond"] = cmbCoveredBond.SelectedIndex == 0 ? "No" : "Yes";
                    fgCodes[i, "FloatingRate"] = txtFloatingRate.Text;
                    fgCodes[i, "FRNFormula"] = txtFRNFormula.Text;
                    fgCodes[i, "Limits"] = txtLimits.Text;
                    fgCodes[i, "HFIC_Recom"] = cmbHFIC_Recom.SelectedIndex == 1? "Yes" : "No";
                    fgCodes[i, "StockExchange_ID"] = cmbStockExchanges.SelectedValue;
                    fgCodes[i, "CountryAction_ID"] = cmbCountryAction.SelectedValue;
                    fgCodes[i, "CouponeType_ID"] = cmbCouponeType.SelectedValue;
                    fgCodes[i, "RevocationRight_ID"] = cmbRevocationRights.SelectedValue;
                    fgCodes[i, "CountryIssue_ID"] = cmbCountryIssue.SelectedValue;
                    fgCodes[i, "StockExchange_Issue_ID"] = cmbStockExchanges_Issue.SelectedValue;
                    fgCodes[i, "HFIC_Recom_ID"] = (cmbHFIC_Recom.SelectedIndex == 1 ? 1 : 0);
                    fgCodes[i, "Old_ID"] = 0;
                    fgCodes[i, "Edited"] = 1;
                    fgCodes[i, "Aktive"] = chkAktive.Checked ? 1 : 0;
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
                                    txtReutersCode.Text + "\t" + txtBloombergCode.Text + "\t" + txtExchangeTicker.Text + "\t" + cmbStockExchanges.Text + "\t" +
                                    cmbCountryAction.Text + "\t" + cmbStockExchanges_Issue.Text + "\t" + cmbCountryIssue.Text + "\t" + cmbCurrency.Text + "\t" +
                                    dIssued.Value + "\t" + dDeadLine.Value + "\t" + dFirstDiak.Value + "\t" + dFirst.Value + "\t" +
                                    cmbCouponeType.Text + "\t" + txtCoupone.Text + "\t" + txtLastCoupone.Text + "\t" + txtFrequencyClipping.Text + "\t" +
                                    txtPrice.Text + "\t" + cmbRevocationRights.Text + "\t" + txtWeight.Text + "\t" + txtQuantityMin.Text + "\t" + txtQuantityStep.Text + "\t" +
                                    cmbCoveredBond.Text + "\t" + txtLimits.Text + "\t" + txtFloatingRate.Text + "\t" + txtFRNFormula.Text + "\t" +
                                    cmbMonthDays.Text + "\t" + cmbBaseDays.Text + "\t" + (cmbHFIC_Recom.SelectedIndex == 1 ? "Yes" : "No") + "\t" + sMIFID_Risk + "\t" +
                                    cmbStockExchanges.SelectedValue + "\t" + cmbCountryAction.SelectedValue + "\t" + "1" + "\t" + cmbCouponeType.SelectedValue + "\t" +
                                    cmbRevocationRights.SelectedValue + "\t" + iOldCode_ID + "\t" + cmbCountryIssue.SelectedValue + "\t" +
                                    cmbStockExchanges_Issue.SelectedValue + "\t" + "1" + "\t" + (cmbHFIC_Recom.SelectedIndex == 1 ? 1 : 0));
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
                if (e.Col == 8) fgCodes[e.Row, "StockExchange_ID"] = fgCodes[e.Row, "StockExchange_Code"];                      // 8 - StockExchange_Code
                if (e.Col == 9) fgCodes[e.Row, "CountryAction_ID"] = fgCodes[e.Row, "Country_Title"];                           // 9 - CountryAction_ID

                if (e.Col == 36)                                                                                                // 36 - Aktive
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
            panCode.Visible = true;
        }
        private void ShowCodeData()
        {
            i = fgCodes.Row;
            dFrom.Value = Convert.ToDateTime(fgCodes[i, "DateFrom"]);
            dTo.Value = Convert.ToDateTime(fgCodes[i, "DateTo"]);
            txtTitleCode.Text = txtTitle.Text;
            txtISINCode.Text = txtISIN.Text; ;
            txtReutersCode.Text = fgCodes[i, "Code"] + "";
            txtBloombergCode.Text = fgCodes[i, "Code2"] + "";
            txtExchangeTicker.Text = fgCodes[i, "Code3"] + "";
            sTemp = fgCodes[i, "MIFID_Risk"] + "      ";
            chkMIFID_Risk_1.Checked = (sTemp.Substring(0, 1) == "1" ? true : false);
            chkMIFID_Risk_2.Checked = (sTemp.Substring(1, 1) == "1" ? true : false);
            chkMIFID_Risk_3.Checked = (sTemp.Substring(2, 1) == "1" ? true : false);
            chkMIFID_Risk_4.Checked = (sTemp.Substring(3, 1) == "1" ? true : false);
            chkMIFID_Risk_5.Checked = (sTemp.Substring(4, 1) == "1" ? true : false);
            chkMIFID_Risk_6.Checked = (sTemp.Substring(5, 1) == "1" ? true : false);
            cmbStockExchanges.SelectedValue = Convert.ToInt32(fgCodes[i, "StockExchange_ID"]);
            cmbCountryAction.SelectedValue = Convert.ToInt32(fgCodes[i, "CountryAction_ID"]);
            cmbCurrency.Text = fgCodes[i, "Curr"] + "";
            cmbCountryIssue.SelectedValue = Convert.ToInt32(fgCodes[i, "CountryIssue_ID"]);
            cmbStockExchanges_Issue.SelectedValue = Convert.ToInt32(fgCodes[i, "StockExchange_Issue_ID"]);

            if (Convert.ToInt32(fgCodes[i, "Aktive"]) == 2) chkAktive.Checked = true;
            else chkAktive.Checked = (Convert.ToInt32(fgCodes[i, "Aktive"]) == 1 ? true : false);

            if (fgCodes[i, "Date1"]+"" != "") dIssued.Value = Convert.ToDateTime(fgCodes[i, "Date1"]);
            else {
                dIssued.CustomFormat = "          ";
                dIssued.Format = DateTimePickerFormat.Custom;
            }

            if (fgCodes[i, "Date2"] + "" != "") dDeadLine.Value = Convert.ToDateTime(fgCodes[i, "Date2"]);
            else
            {
                dDeadLine.CustomFormat = "          ";
                dDeadLine.Format = DateTimePickerFormat.Custom;
            }

            if (fgCodes[i, "Date3"] + "" != "") dFirstDiak.Value = Convert.ToDateTime(fgCodes[i, "Date3"]);
            else
            {
                dFirstDiak.CustomFormat = "          ";
                dFirstDiak.Format = DateTimePickerFormat.Custom;
            }

            if (fgCodes[i, "Date4"] + "" != "") dFirst.Value = Convert.ToDateTime(fgCodes[i, "Date4"]);
            else
            {
                dFirst.CustomFormat = "          ";
                dFirst.Format = DateTimePickerFormat.Custom;
            }

            cmbMonthDays.Text = fgCodes[i, "MonthDays"]+"";
            cmbBaseDays.Text = fgCodes[i, "BaseDays"] + "";
            cmbCouponeType.SelectedValue = fgCodes[i, "CouponeType_ID"];
            txtCoupone.Text = fgCodes[i, "Coupone"] + "";
            txtLastCoupone.Text = fgCodes[i, "LastCoupone"] + "";
            txtPrice.Text = fgCodes[i, "Price"] + "";
            txtFrequencyClipping.Text = fgCodes[i, "FrequencyClipping"] + "";
            cmbRevocationRights.SelectedValue = fgCodes[i, "RevocationRight_ID"];
            txtWeight.Text = fgCodes[i, "Gravity"] + "";
            txtQuantityMin.Text = fgCodes[i, "QuantityMin"] + "";
            txtQuantityStep.Text = fgCodes[i, "QuantityStep"] + "";
            cmbCoveredBond.Text = fgCodes[i, "CoveredBond"] + "";
            txtFloatingRate.Text = fgCodes[i, "FloatingRate"] + "";
            txtFRNFormula.Text = fgCodes[i, "FRNFormula"] + "";
            txtLimits.Text = fgCodes[i, "Limits"] + "";
            cmbHFIC_Recom.SelectedIndex = Convert.ToInt32(fgCodes[i, "HFIC_Recom_ID"]);
        }
        private void panCode_MouseDown(object sender, MouseEventArgs e)
        {
            this.position = e.Location;
            this.pMove = true;
        }
        private void panCode_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) {
                if (this.pMove == true) {
                    this.panCode.Location = new Point(this.panCode.Location.X + e.X - this.position.X, this.panCode.Location.Y + e.Y - this.position.Y);
                }
            }
        }
        private void panCode_MouseUp(object sender, MouseEventArgs e)
        {
            this.pMove = false;
        }
        public int Mode { get { return this.iMode; } set { this.iMode = value; } }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
    }
}
