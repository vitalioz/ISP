using System;
using System.Data;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Globalization;
using System.Drawing;
using C1.Win.C1FlexGrid;
using Core;

namespace Products
{
    public partial class frmSelectedProducts : Form
    {
        DataTable dtList, dtWishList, dtHFCategoriesList, dtGlobalBroadList, dtCountryGroupList, dtCountryRiskList, dtCurrenciesList, dtViewsList, dtList4;
        DataColumn dtCol;
        DataRow dtRow, dtRow1;
        DataRow[] foundRows;
        DataView dtView;
        CellStyle csError;
        int i, j, k, m, iRightsLevel;
        string sTemp, sCategory, sViewString, sExtra;
        string[] sRatingGroup = { "", "InvestmentGrade", "High Yield", "Default", "No Rating" };
        string[] sDistrib = { "", "Both", "Professional", "Neither", "Retail" };
        bool bCheckList, bFound;
        clsProductsCodes klsProductsCode = new clsProductsCodes();
        clsProductsTitles klsProductTitle = new clsProductsTitles();
        public frmSelectedProducts()
        {
            InitializeComponent();
        }
        #region --- Start functions -----------------------------------------------------------------------------
        private void frmSelectedProducts_Load(object sender, EventArgs e)
        {
            bCheckList = false;
            bFound = false;
            sViewString = "";

            csError = fgList.Styles.Add("Error");
            csError.BackColor = Color.LightCoral;

            //--- dtList4 - table of products that are valid with currenct Contract -------------------------------
            dtList4 = new DataTable("ContractProductsList");
            dtCol = dtList4.Columns.Add("CodeTitle", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("ISIN", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("Product_Title", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("ProductCategory_Title", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("HFCategory_Title", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("SecID", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("Code", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("Code2", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("Currency", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("CreditRating", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("MoodysRating", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("FitchsRating", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("SPRating", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("ICAPRating", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("CountryRisk_Title", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("InvestGeography_ID", System.Type.GetType("System.Int32"));
            dtCol = dtList4.Columns.Add("Date2", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("Maturity", System.Type.GetType("System.Single"));
            dtCol = dtList4.Columns.Add("Maturity_Date", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("CurrencyHedge", System.Type.GetType("System.Int16"));
            dtCol = dtList4.Columns.Add("CurrencyHedge2", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("SurveyedKIID", System.Type.GetType("System.Single"));
            dtCol = dtList4.Columns.Add("SurveyedKIID_Date", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("StockExchange_ID", System.Type.GetType("System.Int32"));
            dtCol = dtList4.Columns.Add("StockExchange_Code", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("Weight", System.Type.GetType("System.Single"));
            dtCol = dtList4.Columns.Add("LastClosePrice", System.Type.GetType("System.Single"));
            dtCol = dtList4.Columns.Add("IR_URL", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("Retail", System.Type.GetType("System.Int16"));
            dtCol = dtList4.Columns.Add("Professional", System.Type.GetType("System.Int16"));
            dtCol = dtList4.Columns.Add("ComplexProduct", System.Type.GetType("System.Int16"));
            dtCol = dtList4.Columns.Add("Distrib_ExecOnly", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("Distrib_Advice", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("Distrib_PortfolioManagment", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("MIFID_Risk", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("ID", System.Type.GetType("System.Int32"));
            dtCol = dtList4.Columns.Add("Shares_ID", System.Type.GetType("System.Int32"));
            dtCol = dtList4.Columns.Add("ShareTitles_ID", System.Type.GetType("System.Int32"));
            dtCol = dtList4.Columns.Add("Product_ID", System.Type.GetType("System.Int16"));
            dtCol = dtList4.Columns.Add("ProductCategory_ID", System.Type.GetType("System.Int32"));
            dtCol = dtList4.Columns.Add("OK_Flag", System.Type.GetType("System.Int16"));
            dtCol = dtList4.Columns.Add("OK_String", System.Type.GetType("System.String"));
            dtCol = dtList4.Columns.Add("Aktive", System.Type.GetType("System.Int16"));
            dtCol = dtList4.Columns.Add("HFIC_Recom", System.Type.GetType("System.Int16"));

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgList.DrawMode = DrawModeEnum.OwnerDraw;
            //fgList.RowColChange += new EventHandler(fgList_RowColChange);
            fgList.MouseDown += new MouseEventHandler(fgList_MouseDown);
            fgList.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgList_BeforeEdit);
            fgList.DoubleClick += new System.EventHandler(fgList_DoubleClick);
            fgList.OwnerDrawCell += fgList_OwnerDrawCell;


            //------- fgHistory ----------------------------
            fgHistory.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgHistory.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");


            dtList = new DataTable("ProductTypesList");
            dtCol = dtList.Columns.Add("ID", System.Type.GetType("System.Int32"));
            dtCol = dtList.Columns.Add("Title", System.Type.GetType("System.String"));

            dtRow = dtList.NewRow();
            dtRow["ID"] = 1;
            dtRow["Title"] = "Μετοχές";
            dtList.Rows.Add(dtRow);

            dtRow = dtList.NewRow();
            dtRow["ID"] = 2;
            dtRow["Title"] = "Ομόλογα";
            dtList.Rows.Add(dtRow);

            dtRow = dtList.NewRow();
            dtRow["ID"] = 4;
            dtRow["Title"] = "ΔΑΚ";
            dtList.Rows.Add(dtRow);

            dtRow = dtList.NewRow();
            dtRow["ID"] = 6;
            dtRow["Title"] = "ΑΚ";
            dtList.Rows.Add(dtRow);

            //-------------- Define Products List ------------------
            cmbCritProducts.DataSource = dtList.Copy();
            cmbCritProducts.DisplayMember = "Title";
            cmbCritProducts.ValueMember = "ID";

            //-------------- Define Products List ------------------
            cmbHistoryProducts.DataSource = dtList.Copy();
            cmbHistoryProducts.DisplayMember = "Title";
            cmbHistoryProducts.ValueMember = "ID";

            //-------------- Define Products List ------------------
            cmbProducts.DataSource = dtList.Copy();
            cmbProducts.DisplayMember = "Title";
            cmbProducts.ValueMember = "ID";


            clsSystem Systems = new clsSystem();
            Systems.GetScreenFormViews();
            dtViewsList = Systems.List;

            cmbGroups.SelectedValue = 0;
            toolLeft2.Visible = false;
            fgList2.Visible = false;
            chkList2.Visible = false;

            cmbCritProducts.SelectedValue = 0;
            bCheckList = true;
            cmbCritProducts.SelectedValue = 1;              //  <------------  starts here

            if (iRightsLevel == 1) {
                tsbAdd.Enabled = false;
                tsbImport.Enabled = false;
                tsbDelete.Enabled = false;
                tslRiskProfiles.Enabled = false;
                chkList1.Visible = false;

                tcMain.TabPages.Remove(tpGroups);
            }
        }
        protected override void OnResize(EventArgs e)
        {
            tcMain.Height = this.Height - 48;
            tcMain.Width = this.Width - 24;
            panCrits.Width = tcMain.Width - 24;
            grpCrits2.Width = tcMain.Width - 24;

            fgList.Height = tcMain.Height - 164;
            fgList.Width = tcMain.Width - 24;

            fgList2.Height = tcMain.Height - 156;
        }
        #endregion
        #region --- Toolbar and Critiries functions -----------------------------------------------------------------------------
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            cmbProducts.SelectedValue = cmbCritProducts.SelectedValue;
            if (Convert.ToInt32(cmbProducts.SelectedValue) == 0) cmbProducts.Enabled = true;
            else cmbProducts.Enabled = false;

            txtShare.Text = "";
            fgShares.Rows.Count = 1;

            panCode.Top = (this.Height - panCode.Height) / 2;
            panCode.Left = (this.Width - panCode.Width) / 2;
            panCode.Visible = true;
        }

        private void tsbImport_Click(object sender, EventArgs e)
        {
            clsProductsCodes klsProductCode = new clsProductsCodes();

            frmImportData locImportData = new frmImportData();
            locImportData.FileType = 0;                                    // .csv file
            locImportData.Shema = 3;
            locImportData.ReadMode = 2;
            locImportData.ShowDialog();
            if (locImportData.Aktion == 1) {
                sTemp = "";
                foreach (DataRow dtRow in locImportData.Result.Rows) {
                    klsProductCode = new clsProductsCodes();
                    klsProductCode.ISIN = dtRow["f1"] +"";
                    klsProductCode.SecID = dtRow["f1"] + "";
                    klsProductCode.GetRecord_ISIN();
                    if (klsProductCode.Record_ID > 0) {
                        i = klsProductCode.Record_ID;
                        klsProductCode = new clsProductsCodes();
                        klsProductCode.Record_ID = i;
                        klsProductCode.GetRecord();
                        klsProductCode.HFIC_Recom = 1;
                        klsProductCode.EditRecord();

                        foundRows = Global.dtProducts.Select("ID = " + klsProductCode.Record_ID);
                        if (foundRows.Length > 0) foundRows[0]["HFIC_Recom"] = 1;

                        clsProductsRecomLogs klsProductRecomLog = new clsProductsRecomLogs();
                        klsProductRecomLog.ShareCodes_ID = Convert.ToInt32(fgShares[i, "ID"]);
                        klsProductRecomLog.EditAktion = 1;                                         // 1 - Add
                        klsProductRecomLog.EditDate = DateTime.Now;
                        klsProductRecomLog.InsertRecord();
                    }
                    else sTemp = sTemp + dtRow["f1"] + "\n";

                }

                bCheckList = false;
                DefineList();
                bCheckList = true;
                ShowList();

                if (sTemp.Length > 0) MessageBox.Show("Unknown ISINs \n\n" + sTemp);
            }
        }
        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                for (i = 1; i <= fgList.Rows.Count - 1; i++) {
                    if (Convert.ToBoolean(fgList[i, 0])) {

                        clsProductsCodes klsProductCode = new clsProductsCodes();
                        klsProductCode.Record_ID = Convert.ToInt32(fgList[i, "ID"]);
                        klsProductCode.GetRecord();
                        klsProductCode.HFIC_Recom = 0;
                        klsProductCode.MIFID_Risk = "";
                        klsProductCode.Gravity = 0;
                        klsProductCode.EditRecord();

                        foundRows = Global.dtProducts.Select("ID = " + fgShares[i, "ID"]);
                        if (foundRows.Length > 0) foundRows[0]["HFIC_Recom"] = 0;

                        clsProductsRecomLogs klsProductRecomLog = new clsProductsRecomLogs();
                        klsProductRecomLog.ShareCodes_ID = Convert.ToInt32(fgList[i, "ID"]);
                        klsProductRecomLog.EditAktion = 2;                                           // 2 - Delete
                        klsProductRecomLog.EditDate = DateTime.Now;
                        klsProductRecomLog.InsertRecord();
                    }
                }

                bCheckList = false;
                DefineList();
                bCheckList = true;
                ShowList();
            }
        }
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            EditProduct();
        }
        private void tsbCompare_Click(object sender, EventArgs e)
        {

        }

        private void tslRiskProfiles_Click(object sender, EventArgs e)
        {
            for (i = 1; i <= fgList.Rows.Count - 1; i++) {

                if (Convert.ToInt32(fgList[i, "ID"]) == 11919)
                    i = i;
                if (Convert.ToInt32(fgList[i, "Error"]) == 0) 
                     sTemp = Global.RecalcRiskProfile(Convert.ToInt32(fgList[i, "ID"]));
                else sTemp = "";

                clsProductsCodes klsProductCode = new clsProductsCodes();
                klsProductCode.Record_ID = Convert.ToInt32(fgList[i, "ID"]);
                klsProductCode.GetRecord();
                klsProductCode.MIFID_Risk = sTemp;
                klsProductCode.EditRecord();
            }

            bCheckList = false;
            DefineList();
            bCheckList = true;
            ShowList();
        }

        private void btnCleanUp_Click(object sender, EventArgs e)
        {
            Global.GetProductsList();
            ClearCritiries();
            DefineList();
            ShowList();
        }

        private void txtShare_TextChanged(object sender, EventArgs e)
        {
            ShareFiltering();
        }
        private void ShareFiltering()
        {
            if (bCheckList) {
                string sTemp, sFilter;
                sFilter = txtShare.Text.Trim();

                sTemp = "HFIC_Recom = 0";
                if (Convert.ToInt32(cmbCritProducts.SelectedValue) != 0 )  sTemp = sTemp + " AND Product_ID = " + cmbCritProducts.SelectedValue;
                sTemp = sTemp + " AND ( Code LIKE '%" + sFilter + "%' OR Code2 LIKE '%" + sFilter + "%' OR ISIN LIKE '%" + sFilter + "%' OR Title LIKE '%" + sFilter + "%' )";
                dtView = Global.dtProducts.DefaultView;
                dtView.RowFilter = sTemp;

                fgShares.Redraw = false;
                fgShares.Rows.Count = 1;
                foreach (DataRowView dtViewRow in dtView)
                    if ((Convert.ToInt32(cmbProducts.SelectedValue) == 0 || Convert.ToInt32(dtViewRow["Product_ID"]) == Convert.ToInt32(cmbProducts.SelectedValue)) && (Convert.ToInt32(dtViewRow["Aktive"]) == 1)) {
                    fgShares.AddItem(false + "\t" + dtViewRow["Title"] + "\t" + dtViewRow["Code"] + "\t" + dtViewRow["Code2"] + "\t" +
                                 dtViewRow["ISIN"] + "\t" + dtViewRow["Product"] + "/" + dtViewRow["ProductCategory"] + "\t" +
                                 dtViewRow["StockExchange_Code"] + "\t" + dtViewRow["Currency"] + "\t" + dtViewRow["StockExchange_ID"] + "\t" +
                                 dtViewRow["Product_ID"] + "\t" + dtViewRow["ProductCategory_ID"] + "\t" + dtViewRow["ID"] + "\t" + dtViewRow["HFIC_Recom"]);
                }
          
                fgShares.Redraw = true;
            }
        }
        private void fgList_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (e.Col == 0) e.Cancel = false;
            else e.Cancel = true;
        }
        private void fgList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                fgList.ContextMenuStrip = mnuContext;
                fgList.Row = fgList.MouseRow;
            }
        }
        private void fgList_DoubleClick(object sender, EventArgs e)
        {
            EditProduct();
        }
        private void fgList_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            if (e.Row > 0 && (fgList[e.Row, "Error"] + "") == "1") {
               
                if (e.Col == 9) 
                    if ((fgList[e.Row, "Currency"] + "") == "")
                        e.Style = csError;
                    else
                    {
                        foundRows = Global.dtCurrencies.Select("Title = '" + fgList[e.Row, "Currency"] + "'");
                        if (foundRows.Length <= 0) e.Style = csError;
                    }

                if (e.Col == 16)
                    if ((fgList[e.Row, "CountryGroup_Title"] + "") == "") e.Style = csError;

                if (e.Col == 17)
                    if ((fgList[e.Row, "CountryRisk_Title"] + "") == "") e.Style = csError;

                if (e.Col == 26)
                    if (Convert.ToSingle(fgList[e.Row, "Gravity"]) <= 0 || Convert.ToSingle(fgList[e.Row, "Gravity"]) >= 21) e.Style = csError;

                if (e.Col == 29)
                    if ((fgList[e.Row, "ComplexProduct"] + "") == "") e.Style = csError;

                if (e.Col == 30)
                    if ((fgList[e.Row, "Distribution_RTO"] + "") == "") e.Style = csError;

                if (e.Col == 31)
                    if ((fgList[e.Row, "Distribution_Advisory"] + "") == "") e.Style = csError;

                if (e.Col == 32)
                    if ((fgList[e.Row, "Distribution_Discret"] + "") == "") e.Style = csError;


                switch (cmbCritProducts.SelectedValue)
                {
                    case 1:
                    case 2:       // 2 - Bond
                        if (e.Col == 15)
                            if ((fgList[e.Row, "RatingGroup"] + "") == "-") e.Style = csError;

                        if (e.Col == 18)
                            if (!Global.IsDate(fgList[e.Row, "Date2"] + ""))
                            {
                                e.Style = csError;
                                fgList[e.Row, 18] = "";
                            }
                        break;

                    case 4:        // 4 - DAK
                        if (e.Col == 7)
                            if ((fgList[e.Row, "Code"] + "") == "") e.Style = csError;

                        if (e.Col == 10)
                            if (((fgList[e.Row, "CreditRating"] + "") == "") || (fgList[e.Row, "CreditRating"] + "") == "-")
                                if (((fgList[e.Row, "HFCategory_Title"] + "") == "FIXED INCOME") || ((fgList[e.Row, "HFCategory_Title"] + "") == "MONEY MARKET") || ((fgList[e.Row, "HFCategory_Title"] + "") == "MIXED"))
                                    e.Style = csError;

                        if (e.Col == 15)
                            if ((fgList[e.Row, "RatingGroup"] + "") == "-") e.Style = csError;

                        if (e.Col == 25)
                            if ((fgList[e.Row, "StockExchange_Code"] + "") == "") e.Style = csError;

                        break;
                    case 6:          // 6 - AK
                        if (e.Col == 10)
                            if (((fgList[e.Row, "CreditRating"] + "") == "") || (fgList[e.Row, "CreditRating"] + "") == "-")
                                if (((fgList[e.Row, "HFCategory_Title"] + "") == "FIXED INCOME") || ((fgList[e.Row, "HFCategory_Title"] + "") == "MONEY MARKET") || ((fgList[e.Row, "HFCategory_Title"] + "") == "MIXED"))
                                    e.Style = csError;

                        if (e.Col == 15)
                            if ((fgList[e.Row, "RatingGroup"] + "") == "-") e.Style = csError;

                        if (e.Col == 19)
                            if (Convert.ToSingle(fgList[e.Row, "Maturity"] + "") <= 0)
                                if (((fgList[e.Row, "HFCategory_Title"] + "") == "FIXED INCOME") || ((fgList[e.Row, "HFCategory_Title"] + "") == "MONEY MARKET") || ((fgList[e.Row, "HFCategory_Title"] + "") == "MIXED"))
                                    e.Style = csError;

                        if (e.Col == 20)
                            if (Convert.ToInt32(fgList[e.Row, "CurrencyHedged"]) == 1)
                                if ((fgList[e.Row, "CurrencyHedged2"] + "") == "") e.Style = csError;

                        if (e.Col == 23)
                            if (Convert.ToSingle(fgList[e.Row, "SurveyedKIID"]) <= 0) e.Style = csError;

                        break;
                }
            }
        }
        private void mnuShowProduct_Click(object sender, EventArgs e)
        {
            frmProductData locProductData = new frmProductData();
            locProductData.Product_ID = Convert.ToInt32(cmbCritProducts.SelectedValue);
            locProductData.ShareCode_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
            locProductData.Text = Global.GetLabel("product");
            locProductData.Show();
        }

        private void mnuCopyISIN_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 0)  {
                try {
                    if (!Convert.IsDBNull(Clipboard.GetText())) Clipboard.SetDataObject(fgList[fgList.Row, "ISIN"], true, 10, 100);
                }
                catch (Exception) {}
            }
        }
        private void EditProduct()
        {
            frmProductData locProductData = new frmProductData();
            locProductData.Mode = 3;                                                        // 1 - from ProductsList, 2 - from ProductsData, 3 - from SelectedProducts
            locProductData.RightsLevel = iRightsLevel;
            locProductData.Product_ID = Convert.ToInt32(cmbCritProducts.SelectedValue);
            locProductData.ShareCode_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
            locProductData.Text = Global.GetLabel("product");
            locProductData.ShowDialog();
            if (locProductData.LastAktion > 0 )
            {
                fgList.Redraw = false;

                k = fgList.Row;

                klsProductsCode = new clsProductsCodes();
                klsProductsCode.Record_ID = Convert.ToInt32(fgList[k, "ID"]);
                klsProductsCode.GetRecord();

                klsProductTitle = new clsProductsTitles();
                klsProductTitle.Record_ID = Convert.ToInt32(fgList[k, "ShareTitles_ID"]);
                klsProductTitle.GetRecord();

                fgList[k, "Title"] = klsProductTitle.ProductTitle;
                fgList[k, "ISIN"] = klsProductTitle.ISIN;
                fgList[k, "ProductCategory_Title"] = klsProductTitle.ProductCategory_Title;
                fgList[k, "HFCategory_Title"] = klsProductTitle.HFCategory_Title;
                fgList[k, "SecID"] = klsProductsCode.SecID;
                fgList[k, "Code"] = klsProductsCode.Code;
                fgList[k, "Code2"] = klsProductsCode.Code3;
                fgList[k, "Currency"] = klsProductsCode.Curr;
                fgList[k, "CreditRating"] = klsProductTitle.CreditRating;
                fgList[k, "MoodysRating"] = klsProductTitle.MoodysRating;
                fgList[k, "FitchsRating"] = klsProductTitle.FitchsRating;
                fgList[k, "SPRating"] = klsProductTitle.SPRating;
                fgList[k, "ICAPRating"] = klsProductTitle.ICAPRating;
                fgList[k, "RatingGroup"] = klsProductTitle.RatingGroup;
                fgList[k, "CountryGroup_Title"] = klsProductTitle.CountryGroup_Title;
                fgList[k, "CountryRisk_Title"] = klsProductTitle.CountryRisk_Title;
                fgList[k, "Date2"] = klsProductsCode.Date2;
                fgList[k, "Maturity"] = klsProductTitle.Maturity;
                fgList[k, "MaturityDate"] = klsProductTitle.MaturityDate;
                fgList[k, "CurrencyHedged"] = klsProductsCode.CurrencyHedge;
                fgList[k, "CurrencyHedged2"] = klsProductsCode.CurrencyHedge2;
                fgList[k, "SurveyedKIID"] = klsProductTitle.SurveyedKIID;
                fgList[k, "SurveyedKIID_Date"] = klsProductTitle.SurveyedKIID_Date;
                fgList[k, "StockExchange_Code"] = klsProductsCode.StockExchange_Code;
                fgList[k, "Gravity"] = klsProductsCode.Gravity;
                fgList[k, "Retail"] = (klsProductTitle.InvestType_Retail == 2 ? "Yes" : (klsProductTitle.InvestType_Retail == 1 ? "No" : ""));
                fgList[k, "Professional"] = (klsProductTitle.InvestType_Prof == 2 ? "Yes" : (klsProductTitle.InvestType_Prof == 1 ? "No" : ""));
                fgList[k, "ComplexProduct"] = klsProductTitle.ComplexProduct == 2 ? "Yes" : (klsProductTitle.ComplexProduct == 1 ? "No" : "");
                fgList[k, "Distribution_RTO"] = sDistrib[klsProductTitle.Distrib_ExecOnly]; 
                fgList[k, "Distribution_Advisory"] = sDistrib[klsProductTitle.Distrib_Advice];
                fgList[k, "Distribution_Discret"] = sDistrib[klsProductTitle.Distrib_PortfolioManagment];
                fgList[k, "RiskProfile"] = klsProductsCode.MIFID_Risk;

                fgList.Redraw = true;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            for (i = 1; i <= fgShares.Rows.Count - 1; i++) {
                if (Convert.ToBoolean(fgShares[i, 0])) {
                    clsProductsCodes klsProductCode = new clsProductsCodes();
                    klsProductCode.Record_ID = Convert.ToInt32(fgShares[i, "ID"]);
                    klsProductCode.GetRecord();
                    klsProductCode.HFIC_Recom = 1;
                    klsProductCode.MIFID_Risk = "000000";
                    klsProductCode.Gravity = 0;
                    klsProductCode.EditRecord();

                    foundRows = Global.dtProducts.Select("ID = " + fgShares[i, "ID"]);
                    if (foundRows.Length > 0)  foundRows[0]["HFIC_Recom"] = 1;

                    clsProductsRecomLogs klsProductRecomLog = new clsProductsRecomLogs();
                    klsProductRecomLog.ShareCodes_ID = Convert.ToInt32(fgShares[i, "ID"]);
                    klsProductRecomLog.EditAktion = 1;                                         // 1 - Add
                    klsProductRecomLog.EditDate = DateTime.Now;
                    klsProductRecomLog.InsertRecord();
                }
            }

            bCheckList = false;
            DefineList();
            bCheckList = true;
            ShowList();

            panCode.Visible = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            panCode.Visible = false;
        }

        private void picCode_Clean_Click(object sender, EventArgs e)
        {

        }

        private void tsbExport_Click(object sender, EventArgs e)
        {
            chkCrit_Shares.Checked = true;
            chkCrit_Bonds.Checked = true;
            chkCrit_ETFs.Checked = true;
            chkCrit_Funds.Checked = true;
            panCategoriesList.Visible = true;
        }

        private void picClose_CategoriesList_Click(object sender, EventArgs e)
        {
            panCategoriesList.Visible = false;
        }

        private void btnOK_CategoriesList_Click(object sender, EventArgs e)
        {
            panCategoriesList.Visible = false;

            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            m = Convert.ToInt32(cmbCritProducts.SelectedValue);

            var EXL = new Microsoft.Office.Interop.Excel.Application();
            var oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;

            // System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("el-GR");
            EXL.Workbooks.Add();
            EXL.ScreenUpdating = false;
            EXL.Caption = Global.AppTitle;
            Excel.Sheets worksheets = EXL.Worksheets;

            cmbCritProducts.SelectedValue = 0;
            ClearCritiries();           

            //---------------------------------------------
            if (chkCrit_Funds.Checked) {
                var xlSheets = EXL.Sheets as Excel.Sheets;
                var xlSheet4 = (Excel.Worksheet)xlSheets.Add(xlSheets[1], Type.Missing, Type.Missing, Type.Missing);
                xlSheet4.Name = "Funds";

                cmbCritProducts.SelectedValue = 6;
                for (i = 0; i <= fgList.Rows.Count - 1; i++) {
                    k = 0;
                    for (j = 1; j <= fgList.Cols.Count - 1; j++) {
                        if (fgList.Cols[j].Visible) { 
                            k = k + 1;
                            xlSheet4.Cells[i + 2, k].Value = fgList[i, j];
                        }
                    }
                }
            }

            //---------------------------------------------
            if (chkCrit_ETFs.Checked) {
                var xlSheets = EXL.Sheets as Excel.Sheets;
                var xlSheet3 = (Excel.Worksheet)xlSheets.Add(xlSheets[1], Type.Missing, Type.Missing, Type.Missing);
                xlSheet3.Name = "ETFs";

                cmbCritProducts.SelectedValue = 4;
                for (i = 0; i <= fgList.Rows.Count - 1; i++) {
                    k = 0;
                    for (j = 1; j <= fgList.Cols.Count - 1; j++) {
                        if (fgList.Cols[j].Visible) {
                            k = k + 1;
                            xlSheet3.Cells[i + 2, k].Value = fgList[i, j];
                        }
                    }
                }
            }

            //---------------------------------------------
            if (chkCrit_Bonds.Checked) {
                var xlSheets = EXL.Sheets as Excel.Sheets;
                var xlSheet2 = (Excel.Worksheet)xlSheets.Add(xlSheets[1], Type.Missing, Type.Missing, Type.Missing);
                xlSheet2.Name = "Bonds";

                cmbCritProducts.SelectedValue = 2;
                for (i = 0; i <= fgList.Rows.Count - 1; i++) {
                    k = 0;
                    for (j = 1; j <= fgList.Cols.Count - 1; j++) {
                        if (fgList.Cols[j].Visible) {
                            k = k + 1;
                            xlSheet2.Cells[i + 2, k].Value = fgList[i, j];
                        }
                    }
                }
            }

            //---------------------------------------------
            if (chkCrit_Shares.Checked) {
                var xlSheets = EXL.Sheets as Excel.Sheets;
                var xlSheet1 = (Excel.Worksheet)xlSheets.Add(xlSheets[1], Type.Missing, Type.Missing, Type.Missing);
                xlSheet1.Name = "Shares";

                cmbCritProducts.SelectedValue = 1;
                for (i = 0; i <= fgList.Rows.Count - 1; i++) {
                    k = 0;
                    for (j = 1; j <= fgList.Cols.Count - 1; j++) {
                        if (fgList.Cols[j].Visible) {
                            k = k + 1;
                            xlSheet1.Cells[i + 2, k].Value = fgList[i, j];
                        }
                    }
                }
            }

            EXL.ScreenUpdating = true;
            EXL.Visible = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;

            //EXL.Quit()
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            cmbCritProducts.SelectedValue = m;

            this.Cursor = Cursors.Default;
        }  
        private void cmbCritProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bCheckList) {
                if (Convert.ToInt32(cmbCritProducts.SelectedValue) > 0) {

                    // sViewString:  1 - Title, 2 -ISIN, 3 - ProductCategory_Title, 4 - HFCategory_Title (GlobalBroadCategories),  5 - SecID, 6 - Code, 7 - Code2, 8 - Currency, 9 - CreditRating, 
                    //               10 - MoodysRating, 11 - FitchsRating, 12 - SPRating, 13 - ICAPRating, 14 - RatingGroup, 15 - CountryGroup_Title, 16 - CountryRisk_Title, 
                    //               17 - Date2, 18 - Maturity, 19 - MaturityDate, 20 - CurrencyHedged, 21 - CurrencyHedged2, 22 - SurveyedKIID, 23 - Surveyed KIID Date,
                    //               24 - StockExchange_Code, 25 - Gravity, 26 - Retail, 27 - Professional, 28 - ComplexProduct, 29-Distribution-Execution Only, 30-Distribution-Investment Advice,
                    //               31-Distribution-Portfolio Management

                    sViewString = "";
                    foundRows = dtViewsList.Select("Tipos = " + cmbCritProducts.SelectedValue);
                    if (foundRows.Length > 0) sViewString = foundRows[0]["FieldsList"] + "";

                    //ClearCritiries();
                    bCheckList = false;
                    DefineList();
                    bCheckList = true;
                    ShowList();

                }
            }
        }
        private void txtCritShare_TextChanged(object sender, EventArgs e)
        {
            ShowList();
        }
        private void cmbCrit_Category_SelectedValueChanged(object sender, EventArgs e)
        {
            ShowList();
        }

        private void cmbCrit_CountryGroup_SelectedValueChanged(object sender, EventArgs e)
        {
            ShowList();
        }

        private void cmbCrit_CountryRisk_SelectedValueChanged(object sender, EventArgs e)
        {
            ShowList();
        }

        private void cmbCrit_Curr_SelectedValueChanged(object sender, EventArgs e)
        {
            ShowList();
        }

        private void cmbCrit_RatingGroup_SelectedValueChanged(object sender, EventArgs e)
        {
            ShowList();
        }

        private void cmbCrit_RiskProfile_SelectedValueChanged(object sender, EventArgs e)
        {
            ShowList();
        }
        private void ClearCritiries()
        {
            bCheckList = false;

            chkLow.SelectedIndex = 0;
            chkMid1.SelectedIndex = 0;
            chkMid2.SelectedIndex = 0;
            chkHigh1.SelectedIndex = 0;
            chkHigh2.SelectedIndex = 0;
            chkHigh3.SelectedIndex = 0;

            txtCritShare.Text = "";
            cmbCrit_Category.SelectedValue = 0;
            cmbCrit_CountryGroup.SelectedValue = 0;
            cmbCrit_Curr.SelectedValue = 0;
            cmbCrit_RiskProfile.SelectedIndex = 0;
            cmbCrit_RatingGroup.SelectedIndex = 0;

            bCheckList = true;
        }
        #endregion
        #region --- define list, show list ----------------------------------------------------
        private void DefineList()
        {
            //--- dtCountryGroupList -----------------
            dtCountryGroupList = new DataTable("CountryGroupList");
            dtCol = dtCountryGroupList.Columns.Add("ID", System.Type.GetType("System.Int32"));
            dtCol = dtCountryGroupList.Columns.Add("Title", System.Type.GetType("System.String"));

            dtRow = dtCountryGroupList.NewRow();
            dtRow["ID"] = 0;
            dtRow["Title"] = "Όλες";
            dtCountryGroupList.Rows.Add(dtRow);

            //--- dtCountryRiskList -----------------
            dtCountryRiskList = new DataTable("CountryRiskList");
            dtCol = dtCountryRiskList.Columns.Add("ID", System.Type.GetType("System.Int32"));
            dtCol = dtCountryRiskList.Columns.Add("Title", System.Type.GetType("System.String"));

            dtRow = dtCountryRiskList.NewRow();
            dtRow["ID"] = 0;
            dtRow["Title"] = "Όλες";
            dtCountryRiskList.Rows.Add(dtRow);

            //--- dtGlobalBroadList -----------------
            dtGlobalBroadList = new DataTable("CategoryList");
            dtCol = dtGlobalBroadList.Columns.Add("ID", System.Type.GetType("System.Int32"));
            dtCol = dtGlobalBroadList.Columns.Add("Title", System.Type.GetType("System.String"));

            dtRow = dtGlobalBroadList.NewRow();
            dtRow["ID"] = 0;
            dtRow["Title"] = "Όλες";
            dtGlobalBroadList.Rows.Add(dtRow);

            //--- dtHFCategoriesList -----------------
            dtHFCategoriesList = new DataTable("HFCategoriesList");
            dtCol = dtHFCategoriesList.Columns.Add("ID", System.Type.GetType("System.Int32"));
            dtCol = dtHFCategoriesList.Columns.Add("Title", System.Type.GetType("System.String"));

            dtRow = dtHFCategoriesList.NewRow();
            dtRow["ID"] = 0;
            dtRow["Title"] = "Όλες";
            dtHFCategoriesList.Rows.Add(dtRow);

            //--- dtCurrenciesList -----------------
            dtCurrenciesList = new DataTable("CurrenciesList");
            dtCol = dtCurrenciesList.Columns.Add("ID", System.Type.GetType("System.String"));
            dtCol = dtCurrenciesList.Columns.Add("Title", System.Type.GetType("System.String"));

            dtRow = dtCurrenciesList.NewRow();
            dtRow["ID"] = "Όλες";
            dtRow["Title"] = "Όλες";
            dtCurrenciesList.Rows.Add(dtRow);

            //--- define Wishs List of cmbCritProducts.SelectedValue products category -------------------
            clsProductsCodes klsProductCode = new clsProductsCodes();
            klsProductCode.Product_ID = Convert.ToInt32(cmbCritProducts.SelectedValue);
            klsProductCode.GetList_WishList();
            dtWishList = klsProductCode.List.Copy();

            foreach (DataRow dtRow in dtWishList.Rows) {
                foundRows = dtCountryGroupList.Select("ID = " + dtRow["CountryGroup_ID"]);
                if (foundRows.Length == 0) {
                    dtRow1 = dtCountryGroupList.NewRow();
                    dtRow1["ID"] = dtRow["CountryGroup_ID"];
                    dtRow1["Title"] = dtRow["CountryGroup_Title"].ToString().ToUpper();
                    dtCountryGroupList.Rows.Add(dtRow1);
                }

                foundRows = dtCountryRiskList.Select("ID = " + dtRow["CountryRisk_ID"]);
                if (foundRows.Length == 0) {
                    dtRow1 = dtCountryRiskList.NewRow();
                    dtRow1["ID"] = dtRow["CountryRisk_ID"];
                    dtRow1["Title"] = dtRow["CountryRisk_Title"].ToString().ToUpper();
                    dtCountryRiskList.Rows.Add(dtRow1);
                }

                foundRows = dtGlobalBroadList.Select("ID = " + dtRow["GlobalBroadCategory"]);
                if (foundRows.Length == 0) {
                    dtRow1 = dtGlobalBroadList.NewRow();
                    dtRow1["ID"] = dtRow["GlobalBroadCategory"];
                    dtRow1["Title"] = dtRow["GlobalBroadCategory_Title"].ToString().ToUpper();
                    dtGlobalBroadList.Rows.Add(dtRow1);
                }

                foundRows = dtHFCategoriesList.Select("ID = " + dtRow["HFCategory"]);
                if (foundRows.Length == 0) {
                    dtRow1 = dtHFCategoriesList.NewRow();
                    dtRow1["ID"] = dtRow["HFCategory"];
                    dtRow1["Title"] = dtRow["HFCategory_Title"].ToString().ToUpper();
                    dtHFCategoriesList.Rows.Add(dtRow1);
                }

                foundRows = dtCurrenciesList.Select("ID = '" + dtRow["Currency"] + "'");
                if (foundRows.Length == 0) {
                    dtRow1 = dtCurrenciesList.NewRow();
                    dtRow1["ID"] = dtRow["Currency"];
                    dtRow1["Title"] = dtRow["Currency"].ToString().ToUpper();
                    dtCurrenciesList.Rows.Add(dtRow1);
                }
            }

            //-------------- Define cmbCrit_CountryGroup List ------------------
            cmbCrit_CountryGroup.DataSource = dtCountryGroupList.Copy();
            cmbCrit_CountryGroup.DisplayMember = "Title";
            cmbCrit_CountryGroup.ValueMember = "ID";

            //-------------- Define cmbCrit_CountryRisk List ------------------
            cmbCrit_CountryRisk.DataSource = dtCountryRiskList.Copy();
            cmbCrit_CountryRisk.DisplayMember = "Title";
            cmbCrit_CountryRisk.ValueMember = "ID";

            //-------------- Define cmbCrit_Curr List ------------------
            cmbCrit_Curr.DataSource = dtCurrenciesList.Copy();
            cmbCrit_Curr.DisplayMember = "Title";
            cmbCrit_Curr.ValueMember = "ID";

            switch (Convert.ToInt32(cmbCritProducts.SelectedValue)) {
                case 1:
                    cmbCrit_Category.DataSource = dtHFCategoriesList.Copy();
                    cmbCrit_Category.DisplayMember = "Title";
                    cmbCrit_Category.ValueMember = "ID";
                    cmbCrit_Category.SelectedValue = 0;
                    //cmbCrit_Category.Enabled = true;

                    lblCategory.Text = "Κατηγορία διαχείρισης HF";

                    lblCrit_CountryRisk.Visible = true;
                    cmbCrit_CountryRisk.Visible = true;

                    lblCrit_RatingGroup.Visible = false;
                    cmbCrit_RatingGroup.Visible = false;

                    fgList.Cols[17].Caption = "Country of Risk";
                    break;
                case 2:
                    cmbCrit_Category.DataSource = dtHFCategoriesList.Copy();
                    cmbCrit_Category.DisplayMember = "Title";
                    cmbCrit_Category.ValueMember = "ID";
                    cmbCrit_Category.SelectedValue = 0;
                    //cmbCrit_Category.Enabled = false;

                    lblCategory.Text = "Κατηγορία διαχείρισης HF";

                    lblCrit_CountryRisk.Visible = true;
                    cmbCrit_CountryRisk.Visible = true;

                    lblCrit_RatingGroup.Visible = true;
                    cmbCrit_RatingGroup.Visible = true;

                    fgList.Cols[17].Caption = "Country of Risk";
                    break;
                case 4:
                    cmbCrit_Category.DataSource = dtGlobalBroadList.Copy();
                    cmbCrit_Category.DisplayMember = "Title";
                    cmbCrit_Category.ValueMember = "ID";
                    cmbCrit_Category.SelectedValue = 0;
                    //cmbCrit_Category.Enabled = true;

                    lblCategory.Text = "Global Broad Category";

                    lblCrit_CountryRisk.Visible = false;
                    cmbCrit_CountryRisk.Visible = false;

                    lblCrit_RatingGroup.Visible = true;
                    cmbCrit_RatingGroup.Visible = true;

                    fgList.Cols[17].Caption = "Investment Area";
                    break;
                case 6:
                    cmbCrit_Category.DataSource = dtGlobalBroadList.Copy();
                    cmbCrit_Category.DisplayMember = "Title";
                    cmbCrit_Category.ValueMember = "ID";
                    cmbCrit_Category.SelectedValue = 0;
                    //cmbCrit_Category.Enabled = true;

                    lblCategory.Text = "Global Broad Category";

                    lblCrit_CountryRisk.Visible = false;
                    cmbCrit_CountryRisk.Visible = false;

                    lblCrit_RatingGroup.Visible = true;
                    cmbCrit_RatingGroup.Visible = true;

                    fgList.Cols[17].Caption = "Investment Area";
                    break;
            }
            fgList.Cols[5].Caption = lblCategory.Text;
        }  
        private void ShowList()
        {
            if (bCheckList)  {
                fgList.Redraw = false;
                fgList.Rows.Count = 1;

                if (dtWishList.Rows.Count > 0) {
                    i = 0;
                    foreach (DataRow dtRow in dtWishList.Rows) {
                        sTemp = "";
                        bFound = false;
                        if (txtCritShare.Text.Length > 0) {
                            sTemp = txtCritShare.Text.ToUpper();
                            if (dtRow["ShareTitles_Title"].ToString().ToUpper().Contains(sTemp) ||
                                dtRow["ISIN"].ToString().ToUpper().Contains(sTemp) ||
                                dtRow["Code"].ToString().ToUpper().Contains(sTemp) ||
                                dtRow["Code2"].ToString().ToUpper().Contains(sTemp) ||
                                dtRow["SecID"].ToString().ToUpper().Contains(sTemp)) bFound = true;
                        }
                        else bFound = true;

                        sCategory = "";
                        if (bFound)
                        {
                            if (Global.IsNumeric(cmbCrit_Category.SelectedValue))
                            {
                                if (Convert.ToInt32(cmbCrit_Category.SelectedValue) > 0)
                                {
                                    if (Convert.ToInt32(cmbCritProducts.SelectedValue) == 1 || Convert.ToInt32(cmbCritProducts.SelectedValue) == 2)
                                    {      // mono gia 1-metoxes or 2-omologa
                                        if (Convert.ToInt32(dtRow["HFCategory"]) != Convert.ToInt32(cmbCrit_Category.SelectedValue)) bFound = false;
                                        else sCategory = dtRow["HFCategory_Title"].ToString().ToUpper();
                                    }
                                    else
                                    {
                                        if (Convert.ToInt32(dtRow["GlobalBroadCategory"]) != Convert.ToInt32(cmbCrit_Category.SelectedValue)) bFound = false;
                                        else sCategory = dtRow["GlobalBroadCategory_Title"].ToString().ToUpper();
                                    }
                                }
                                else
                                {
                                    if (Convert.ToInt32(cmbCritProducts.SelectedValue) == 1 || Convert.ToInt32(cmbCritProducts.SelectedValue) == 2)        // mono gia 1-metoxes or 2-omologa
                                        sCategory = dtRow["HFCategory_Title"].ToString().ToUpper();
                                    else
                                        sCategory = dtRow["GlobalBroadCategory_Title"].ToString().ToUpper();
                                }
                            }
                        }


                        if (bFound)
                            if (Global.IsNumeric(cmbCrit_CountryGroup.SelectedValue))
                            {
                                if (Convert.ToInt32(cmbCrit_CountryGroup.SelectedValue) > 0)
                                    if (Convert.ToInt32(dtRow["CountryGroup_ID"]) != Convert.ToInt32(cmbCrit_CountryGroup.SelectedValue)) bFound = false;
                            }

                        if (Convert.ToInt32(cmbCritProducts.SelectedValue) == 1 || Convert.ToInt32(cmbCritProducts.SelectedValue) == 2)
                        {                    // mono gia 1-metoxes or 2-omologa
                            if (bFound)
                                if (Global.IsNumeric(cmbCrit_CountryRisk.SelectedValue))
                                    if (Convert.ToInt32(cmbCrit_CountryRisk.SelectedValue) > 0)
                                        if (Convert.ToInt32(dtRow["CountryRisk_ID"]) != Convert.ToInt32(cmbCrit_CountryRisk.SelectedValue)) bFound = false;

                        }

                        if (bFound)
                            if (cmbCrit_RiskProfile.SelectedIndex > 0)
                            {
                                sTemp = dtRow["MIFID_Risk"] + "";
                                if (sTemp.Substring(Convert.ToInt32(cmbCrit_RiskProfile.SelectedIndex) - 1, 1) == "0") bFound = false;
                            }


                        if (bFound)
                            if (cmbCrit_RatingGroup.SelectedIndex > 0)
                                if (Convert.ToInt32(dtRow["RatingGroup"]) != (Convert.ToInt32(cmbCrit_RatingGroup.SelectedIndex) - 1)) bFound = false;


                        if (bFound)
                            if (cmbCrit_Curr.Text != "Όλες")
                                if (dtRow["Currency"] + "" != cmbCrit_Curr.Text + "") bFound = false;

                        if (bFound)
                        {
                            i = i + 1;

                            fgList.AddItem(false + "\t" + i + "\t" + dtRow["ShareTitles_Title"] + "\t" + dtRow["ISIN"] + "\t" + dtRow["ProductCategory_Title"] + "\t" +
                                            sCategory + "\t" + dtRow["SecID"] + "\t" + dtRow["Code"] + "\t" + dtRow["Code3"] + "\t" + dtRow["Currency"] + "\t" +
                                            dtRow["CreditRating"] + "\t" + dtRow["MoodysRating"] + "\t" + dtRow["FitchsRating"] + "\t" + dtRow["SPRating"] + "\t" + dtRow["ICAPRating"] + "\t" +
                                            sRatingGroup[Convert.ToInt32(dtRow["RatingGroup"])] + "\t" + dtRow["CountryGroup_Title"] + "\t" + dtRow["CountryRisk_Title"] + "\t" + dtRow["Date2"] + "\t" +
                                            dtRow["Maturity"] + "\t" + dtRow["Maturity_Date"] + "\t" + (Convert.ToInt32(dtRow["CurrencyHedge"]) == 1 ? "Fully Hedged" : "") + "\t" +
                                            dtRow["CurrencyHedge2"] + "\t" + dtRow["SurveyedKIID"] + "\t" + dtRow["SurveyedKIID_Date"] + "\t" + dtRow["StockExchange_Code"] + "\t" + dtRow["Weight"] + "\t" +
                                            (Convert.ToInt16(dtRow["Retail"]) == 2 ? "Yes" : (Convert.ToInt16(dtRow["Retail"]) == 1 ? "No" : "")) + "\t" +
                                            (Convert.ToInt16(dtRow["Professional"]) == 2 ? "Yes" : (Convert.ToInt16(dtRow["Professional"]) == 1 ? "No" : "")) + "\t" +
                                            (Convert.ToInt16(dtRow["ComplexProduct"]) == 2 ? "Yes" : (Convert.ToInt16(dtRow["ComplexProduct"]) == 1 ? "No" : "")) + "\t" +
                                            sDistrib[Convert.ToInt32(dtRow["Distrib_ExecOnly"])] + "\t" + sDistrib[Convert.ToInt32(dtRow["Distrib_Advice"])] + "\t" +
                                            sDistrib[Convert.ToInt32(dtRow["Distrib_PortfolioManagment"])] + "\t" + dtRow["MIFID_Risk"] + "\t" + "" + "\t" + dtRow["ID"] + "\t" +
                                            dtRow["ShareTitles_ID"] + "\t" + "" + "\t" + CheckError(dtRow));
                        }
                    }

                    j = sViewString.Length;
                    for (i = 0; i < j; i++)
                    {
                        if (sViewString.Substring(i, 1) == "1") fgList.Cols[i + 2].Visible = true;
                        else fgList.Cols[i + 2].Visible = false;
                    }
                }
                fgList.Redraw = true;
            }
        }
        private string CheckError(DataRow dtRow)
        {
            string sResult = "0";

            if (dtRow["Currency"] + "" == "") sResult = "1";
            else {
                foundRows = Global.dtCurrencies.Select("Title = '" + dtRow["Currency"] + "'");
                if (foundRows.Length <= 0 ) sResult = "1";
            }

            if (dtRow["CountryGroup_Title"] + "" == "") sResult = "1";

            if (dtRow["CountryRisk_Title"] + "" == "") sResult = "1";

            if (Convert.ToSingle(dtRow["Weight"]) <= 0 || (Convert.ToSingle(dtRow["Weight"]) >= 21)) sResult = "1";

            if (Convert.ToInt32(dtRow["ComplexProduct"]) != 1 && Convert.ToInt32(dtRow["ComplexProduct"]) != 2) sResult = "1";

            if (sResult == "0") {
                switch (Convert.ToInt32(cmbCritProducts.SelectedValue)) {
                    case 1:                                                        // 1 - Shares 
                        break;
                    case 2:                                                        // 2 - Bond
                        if (Convert.ToInt32(dtRow["RatingGroup"]) == 0) sResult = "1";

                        if (!Global.IsDate(dtRow["Date2"]+"")) sResult = "1";
                        break;

                    case 4:                                                         // 4 - DAK
                        if (dtRow["Code"] + "" == "") sResult = "1";
 
                        if (dtRow["CreditRating"]+"" == "") 
                            if (dtRow["HFCategory_Title"].ToString().ToUpper() == "FIXED INCOME" ||
                                dtRow["HFCategory_Title"].ToString().ToUpper() == "MONEY MARKET" ||
                                dtRow["HFCategory_Title"].ToString().ToUpper() == "MIXED") sResult = "1";    

                        if (Convert.ToInt32(dtRow["RatingGroup"]) == 0)
                                if (dtRow["HFCategory_Title"].ToString().ToUpper() == "FIXED INCOME" ||
                                    dtRow["HFCategory_Title"].ToString().ToUpper() == "MONEY MARKET" ||
                                    dtRow["HFCategory_Title"].ToString().ToUpper() == "MIXED") sResult = "1";
    
                        if (dtRow["StockExchange_Code"] + "" == "") sResult = "1";
                        break;
                    case 6:                                                   // 6 - AK

                        if (dtRow["CreditRating"] + "" == "")
                            if (dtRow["HFCategory_Title"].ToString().ToUpper() == "FIXED INCOME" ||
                                dtRow["HFCategory_Title"].ToString().ToUpper() == "MONEY MARKET" ||
                                dtRow["HFCategory_Title"].ToString().ToUpper() == "MIXED") sResult = "1";

                        if (Convert.ToInt32(dtRow["RatingGroup"]) == 0)
                            if (dtRow["HFCategory_Title"].ToString().ToUpper() == "FIXED INCOME" ||
                                dtRow["HFCategory_Title"].ToString().ToUpper() == "MONEY MARKET" ||
                                dtRow["HFCategory_Title"].ToString().ToUpper() == "MIXED") sResult = "1";


                        if (Convert.ToSingle(dtRow["Maturity"]) <= 0)
                            if (dtRow["HFCategory_Title"].ToString().ToUpper() == "FIXED INCOME" ||
                                dtRow["HFCategory_Title"].ToString().ToUpper() == "MONEY MARKET" ||
                                dtRow["HFCategory_Title"].ToString().ToUpper() == "MIXED") sResult = "1";


                        if (Convert.ToInt32(dtRow["CurrencyHedge"]) == 1)
                            if (dtRow["CurrencyHedge2"] + "" == "") sResult = "1";

                        if (Convert.ToSingle(dtRow["SurveyedKIID"]) <= 0) sResult = "1";
       
                        break;
                }
            }

            return sResult;
        }
        #endregion
        #region --- export data --------------------------------------------------
        private void tsbExcel_Click(object sender, EventArgs e)
        {
            var EXL = new Microsoft.Office.Interop.Excel.Application();
            var oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;

            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            // System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("el-GR");
            EXL.Workbooks.Add();
            EXL.ScreenUpdating = false;
            EXL.Caption = Global.AppTitle;
            EXL.Visible = true;

            Excel.Style cstrueStyle = EXL.Application.ActiveWorkbook.Styles.Add("trueStyle");
            cstrueStyle.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);

            Excel.Style csfalseStyle = EXL.Application.ActiveWorkbook.Styles.Add("falseStyle");
            csfalseStyle.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);

            EXL.Cells[1, 1].Value = "Επιλεγμένα Προϊόντα";
            EXL.Cells[2, 1].Value = "Τύπος Προϊόντων:  " + cmbCritProducts.Text;
            EXL.Cells[3, 1].Value = "Προϊόν: " + txtCritShare.Text;
            EXL.Cells[2, 12].Value = "Ημερομηνία:  " + DateTime.Now.ToString("dd/MM/yyyy");
            EXL.Cells[3, 12].Value = "Χρήστης: " + Global.UserName;
            //MessageBox.Show("Start");
            try
            {
                for (this.i = 0; this.i <= fgList.Rows.Count - 1; this.i++)
                {
                    k = 0;
                    for (j = 1; j <= fgList.Cols.Count - 1; j++)
                    {
                        if (fgList.Cols[j].Visible)
                        {
                            sTemp = fgList[i, j] + "";

                            if (fgList.Cols[j].Name + "" == "RiskProfile")
                            {

                                if (i == 0)
                                {
                                    k = k + 1;
                                    EXL.Cells[i + 4, k].Value = "LOW RISK INCOME";

                                    k = k + 1;
                                    EXL.Cells[i + 4, k].Value = "MID INCOME";

                                    k = k + 1;
                                    EXL.Cells[i + 4, k].Value = "MID INCOME & GROWTH";

                                    k = k + 1;
                                    EXL.Cells[i + 4, k].Value = "HIGH INCOME";

                                    k = k + 1;
                                    EXL.Cells[i + 4, k].Value = "HIGH INCOME & GROWTH";

                                    k = k + 1;
                                    EXL.Cells[i + 4, k].Value = "HIGH GROWTH";
                                }
                                else
                                {
                                    k = k + 1;
                                    if (sTemp.Substring(0, 1) == "1")
                                    {
                                        EXL.Cells[i + 4, k].Value = "true";
                                        EXL.Cells[i + 4, k].Style = "trueStyle";
                                    }
                                    else
                                    {
                                        EXL.Cells[i + 4, k].Value = "false";
                                        EXL.Cells[i + 4, k].Style = "falseStyle";
                                    }

                                    k = k + 1;
                                    if (sTemp.Substring(1, 1) == "1")
                                    {
                                        EXL.Cells[i + 4, k].Value = "true";
                                        EXL.Cells[i + 4, k].Style = "trueStyle";
                                    }
                                    else
                                    {
                                        EXL.Cells[i + 4, k].Value = "false";
                                        EXL.Cells[i + 4, k].Style = "falseStyle";
                                    }

                                    k = k + 1;
                                    if (sTemp.Substring(2, 1) == "1")
                                    {
                                        EXL.Cells[i + 4, k].Value = "true";
                                        EXL.Cells[i + 4, k].Style = "trueStyle";
                                    }
                                    else
                                    {
                                        EXL.Cells[i + 4, k].Value = "false";
                                        EXL.Cells[i + 4, k].Style = "falseStyle";
                                    }

                                    k = k + 1;
                                    if (sTemp.Substring(3, 1) == "1")
                                    {
                                        EXL.Cells[i + 4, k].Value = "true";
                                        EXL.Cells[i + 4, k].Style = "trueStyle";
                                    }
                                    else
                                    {
                                        EXL.Cells[i + 4, k].Value = "false";
                                        EXL.Cells[i + 4, k].Style = "falseStyle";
                                    }

                                    k = k + 1;
                                    if (sTemp.Substring(4, 1) == "1")
                                    {
                                        EXL.Cells[i + 4, k].Value = "true";
                                        EXL.Cells[i + 4, k].Style = "trueStyle";
                                    }
                                    else
                                    {
                                        EXL.Cells[i + 4, k].Value = "false";
                                        EXL.Cells[i + 4, k].Style = "falseStyle";
                                    }

                                    k = k + 1;
                                    if (sTemp.Substring(5, 1) == "1")
                                    {
                                        EXL.Cells[i + 4, k].Value = "true";
                                        EXL.Cells[i + 4, k].Style = "trueStyle";
                                    }
                                    else
                                    {
                                        EXL.Cells[i + 4, k].Value = "false";
                                        EXL.Cells[i + 4, k].Style = "falseStyle";
                                    }
                                }
                            }
                            else
                            {
                                k = k + 1;
                                EXL.Cells[i + 4, k].Value = fgList[i, j];
                            }
                        }
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally { }

            //MessageBox.Show("Finish");
            EXL.ScreenUpdating = true;
            EXL.Visible = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;

            this.Cursor = Cursors.Default;
        }
        #endregion ---------------------------------------------------------------------
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
