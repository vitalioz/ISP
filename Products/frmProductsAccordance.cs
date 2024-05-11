using System;
using System.Data;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing;
using C1.Win.C1FlexGrid;
using Excel = Microsoft.Office.Interop.Excel;
using Core;

namespace Products
{
    public partial class frmProductsAccordance : Form
    {
        DataTable dtViewsList, dtList4;
        DataColumn dtCol;
        int i, i1, i2, i4, i6, j, k, iContract_ID, iProfile_ID, iContractProfile_ID, iService_ID, iMiFID_Category, iComplexProduct, iRightsLevel;
        string sCurrency, sGeography, sViewString4, sExtra;
        DataRow[] foundRows;
        CellStyle csError;
        Global.ContractData stContract = new Global.ContractData();
        public frmProductsAccordance()
        {
            InitializeComponent();
        }

        private void frmProductsAccordance_Load(object sender, EventArgs e)
        {
            csError = fgList4_1.Styles.Add("Error");
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

            ucCS.StartInit(700, 400, 200, 20, 1);
            ucCS.TextOfLabelChanged += new EventHandler(ucCS_TextChanged);
            ucCS.Filters = "Contract_ID > 0";
            ucCS.Mode = 1;
            ucCS.ListType = 2;
            ucCS.Visible = true;

            //-------------- Define FinanceServices List ------------------
            cmbFinanceServices.DataSource = Global.dtServices.Copy();
            cmbFinanceServices.DisplayMember = "Title";
            cmbFinanceServices.ValueMember = "ID";

            //-------------- Define Clients Profiles List ------------------    
            cmbProfile.DataSource = Global.dtCustomersProfiles.Copy();
            cmbProfile.DisplayMember = "Title";
            cmbProfile.ValueMember = "ID";

            //-------------- Define Currencies4 List ------------------
            cmbCurrency4.DataSource = Global.dtCurrencies.Copy();
            cmbCurrency4.DisplayMember = "Title";
            cmbCurrency4.ValueMember = "ID";

            clsSystem Systems = new clsSystem();
            Systems.GetScreenFormViews();
            dtViewsList = Systems.List;

            //------- fgList4_1 ----------------------------
            fgList4_1.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList4_1.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgList4_1.CellChanged += new RowColEventHandler(fgList4_1_CellChanged);
            fgList4_1.DoubleClick += new System.EventHandler(fgList4_1_DoubleClick);

            //------- fgList4_2 ----------------------------
            fgList4_2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList4_2.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgList4_2.CellChanged += new RowColEventHandler(fgList4_2_CellChanged);
            fgList4_2.DoubleClick += new System.EventHandler(fgList4_2_DoubleClick);

            //------- fgList4_4 ----------------------------
            fgList4_4.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList4_4.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgList4_4.CellChanged += new RowColEventHandler(fgList4_4_CellChanged);
            fgList4_4.DoubleClick += new System.EventHandler(fgList4_4_DoubleClick);

            //------- fgList4_6 ----------------------------
            fgList4_6.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList4_6.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgList4_6.CellChanged += new RowColEventHandler(fgList4_6_CellChanged);
            fgList4_6.DoubleClick += new System.EventHandler(fgList4_6_DoubleClick);

        }
        protected override void OnResize(EventArgs e)
        {
            panCrits.Width = this.Width - 30;

            tabList4.Height = this.Height - 256;
            tabList4.Width = this.Width - 30;

            fgList4_1.Height = tabList4.Height - 32;
            fgList4_1.Width = tabList4.Width - 24;

            fgList4_2.Height = tabList4.Height - 32;
            fgList4_2.Width = tabList4.Width - 24;

            fgList4_4.Height = tabList4.Height - 32;
            fgList4_4.Width = tabList4.Width - 24;

            fgList4_6.Height = tabList4.Height - 32;
            fgList4_6.Width = tabList4.Width - 24;
        }
        private void picClean4_Click(object sender, EventArgs e)
        {
            ucCS.ShowClientsList = false;
            ucCS.Contract_ID.Text = "0";
            ucCS.txtContractTitle.Text = "";
            ucCS.ShowClientsList = true;

            lblContractCode.Text = "";
            lblContractPortfolio.Text = "";
            lblContractTitle.Text = "";
            iContract_ID = 0;
            iProfile_ID = 0;
            cmbProfile.SelectedValue = 0;
            iContractProfile_ID = 0;
            lblContractProfile_ID.Text = "";
            iService_ID = 0;
            cmbFinanceServices.SelectedValue = 0;
            sCurrency = "";
            cmbCurrency4.Text = "";
            iMiFID_Category = 0;
            cmbMiFiD.SelectedIndex = 0;
            lblGeorgraphy.Text = "";
            sGeography = "";
            iComplexProduct = 0;
            cmbComplexProduct.SelectedIndex = iComplexProduct;     // 0 - Unknown, 1 - No, 2 - Yes

            fgList4_1.Rows.Count = 1;
            fgList4_2.Rows.Count = 1;
            fgList4_4.Rows.Count = 1;
            fgList4_6.Rows.Count = 1;

            dtList4.Rows.Clear();
        }
        private void chkShowAll_CheckedChanged(object sender, EventArgs e)
        {
            ShowList();
        }
        private void DefineList()
        {

            dtList4.Rows.Clear();
            Global.DefineContractProductsList(dtList4, stContract.Contract_ID, stContract.Contracts_Details_ID, stContract.Contracts_Packages_ID, false);
            ShowList();
        }
        private void ShowList()
        {
            i1 = 0;
            i2 = 0;
            i4 = 0;
            i6 = 0;

            fgList4_1.Redraw = false;
            fgList4_1.Rows.Count = 1;

            fgList4_2.Redraw = false;
            fgList4_2.Rows.Count = 1;

            fgList4_4.Redraw = false;
            fgList4_4.Rows.Count = 1;

            fgList4_6.Redraw = false;
            fgList4_6.Rows.Count = 1;

            foreach (DataRow dtRow in dtList4.Rows)
            {
                if ((Convert.ToInt32(dtRow["OK_Flag"]) == 1) || chkShowAll.Checked)
                {
                    switch (Convert.ToInt32(dtRow["Product_ID"]))
                    {
                        case 1:
                            i1 = i1 + 1;
                            fgList4_1.AddItem(false + "\t" + i1 + "\t" + dtRow["CodeTitle"] + "\t" + dtRow["ISIN"] + "\t" + dtRow["ProductCategory_Title"] + "\t" +
                                       dtRow["HFCategory_Title"] + "\t" + dtRow["SecID"] + "\t" + dtRow["Code"] + "\t" + dtRow["Code2"] + "\t" + dtRow["Currency"] + "\t" +
                                       dtRow["CreditRating"] + "\t" + dtRow["MoodysRating"] + "\t" + dtRow["FitchsRating"] + "\t" + dtRow["SPRating"] + "\t" + dtRow["ICAPRating"] + "\t" +
                                       dtRow["CountryRisk_Title"] + "\t" + dtRow["Date2"] + "\t" + dtRow["Maturity"] + "\t" + dtRow["Maturity_Date"] + "\t" +
                                       dtRow["CurrencyHedge"] + "\t" + dtRow["CurrencyHedge2"] + "\t" + dtRow["SurveyedKIID"] + "\t" + dtRow["SurveyedKIID_Date"] + "\t" +
                                       dtRow["StockExchange_Code"] + "\t" + dtRow["Weight"] + "\t" + (Convert.ToInt32(dtRow["Retail"]) == 2 ? "Yes" : (Convert.ToInt32(dtRow["Retail"]) == 1 ? "No" : "")) + "\t" +
                                       (Convert.ToInt32(dtRow["Professional"]) == 2 ? "Yes" : (Convert.ToInt32(dtRow["Professional"]) == 1 ? "No" : "")) + "\t" +
                                       (Convert.ToInt32(dtRow["ComplexProduct"]) == 2 ? "Yes" : (Convert.ToInt32(dtRow["ComplexProduct"]) == 1 ? "No" : "")) + "\t" +
                                       dtRow["Distrib_ExecOnly"] + "\t" + dtRow["Distrib_Advice"] + "\t" + dtRow["Distrib_PortfolioManagment"] + "\t" + dtRow["MIFID_Risk"] + "\t" +
                                       "" + "\t" + dtRow["ID"] + "\t" + dtRow["ShareTitles_ID"] + "\t" + "" + "\t" + dtRow["OK_Flag"] + "\t" + dtRow["OK_String"]);
                            break;
                        case 2:
                            i2 = i2 + 1;
                            fgList4_2.AddItem(false + "\t" + i2 + "\t" + dtRow["CodeTitle"] + "\t" + dtRow["ISIN"] + "\t" + dtRow["ProductCategory_Title"] + "\t" +
                                       dtRow["HFCategory_Title"] + "\t" + dtRow["SecID"] + "\t" + dtRow["Code"] + "\t" + dtRow["Code2"] + "\t" + dtRow["Currency"] + "\t" +
                                       dtRow["CreditRating"] + "\t" + dtRow["MoodysRating"] + "\t" + dtRow["FitchsRating"] + "\t" + dtRow["SPRating"] + "\t" + dtRow["ICAPRating"] + "\t" +
                                       dtRow["CountryRisk_Title"] + "\t" + dtRow["Date2"] + "\t" + dtRow["Maturity"] + "\t" + dtRow["Maturity_Date"] + "\t" +
                                       dtRow["CurrencyHedge"] + "\t" + dtRow["CurrencyHedge2"] + "\t" + dtRow["SurveyedKIID"] + "\t" + dtRow["SurveyedKIID_Date"] + "\t" +
                                       dtRow["StockExchange_Code"] + "\t" + dtRow["Weight"] + "\t" + (Convert.ToInt32(dtRow["Retail"]) == 2 ? "Yes" : (Convert.ToInt32(dtRow["Retail"]) == 1 ? "No" : "")) + "\t" +
                                       (Convert.ToInt32(dtRow["Professional"]) == 2 ? "Yes" : (Convert.ToInt32(dtRow["Professional"]) == 1 ? "No" : "")) + "\t" +
                                       (Convert.ToInt32(dtRow["ComplexProduct"]) == 2 ? "Yes" : (Convert.ToInt32(dtRow["ComplexProduct"]) == 1 ? "No" : "")) + "\t" +
                                       dtRow["Distrib_ExecOnly"] + "\t" + dtRow["Distrib_Advice"] + "\t" + dtRow["Distrib_PortfolioManagment"] + "\t" + dtRow["MIFID_Risk"] + "\t" +
                                       "" + "\t" + dtRow["ID"] + "\t" + dtRow["ShareTitles_ID"] + "\t" + "" + "\t" + dtRow["OK_Flag"] + "\t" + dtRow["OK_String"]);
                            break;
                        case 4:
                            i4 = i4 + 1;
                            fgList4_4.AddItem(false + "\t" + i4 + "\t" + dtRow["CodeTitle"] + "\t" + dtRow["ISIN"] + "\t" + dtRow["ProductCategory_Title"] + "\t" +
                                       dtRow["HFCategory_Title"] + "\t" + dtRow["SecID"] + "\t" + dtRow["Code"] + "\t" + dtRow["Code2"] + "\t" + dtRow["Currency"] + "\t" +
                                       dtRow["CreditRating"] + "\t" + dtRow["MoodysRating"] + "\t" + dtRow["FitchsRating"] + "\t" + dtRow["SPRating"] + "\t" + dtRow["ICAPRating"] + "\t" +
                                       dtRow["CountryRisk_Title"] + "\t" + dtRow["Date2"] + "\t" + dtRow["Maturity"] + "\t" + dtRow["Maturity_Date"] + "\t" +
                                       dtRow["CurrencyHedge"] + "\t" + dtRow["CurrencyHedge2"] + "\t" + dtRow["SurveyedKIID"] + "\t" + dtRow["SurveyedKIID_Date"] + "\t" +
                                       dtRow["StockExchange_Code"] + "\t" + dtRow["Weight"] + "\t" + (Convert.ToInt32(dtRow["Retail"]) == 2 ? "Yes" : (Convert.ToInt32(dtRow["Retail"]) == 1 ? "No" : "")) + "\t" +
                                       (Convert.ToInt32(dtRow["Professional"]) == 2 ? "Yes" : (Convert.ToInt32(dtRow["Professional"]) == 1 ? "No" : "")) + "\t" +
                                       (Convert.ToInt32(dtRow["ComplexProduct"]) == 2 ? "Yes" : (Convert.ToInt32(dtRow["ComplexProduct"]) == 1 ? "No" : "")) + "\t" +
                                       dtRow["Distrib_ExecOnly"] + "\t" + dtRow["Distrib_Advice"] + "\t" + dtRow["Distrib_PortfolioManagment"] + "\t" + dtRow["MIFID_Risk"] + "\t" +
                                       "" + "\t" + dtRow["ID"] + "\t" + dtRow["ShareTitles_ID"] + "\t" + "" + "\t" + dtRow["OK_Flag"] + "\t" + dtRow["OK_String"]);
                            break;
                        case 6:
                            i6 = i6 + 1;
                            fgList4_6.AddItem(false + "\t" + i6 + "\t" + dtRow["CodeTitle"] + "\t" + dtRow["ISIN"] + "\t" + dtRow["ProductCategory_Title"] + "\t" +
                                       dtRow["HFCategory_Title"] + "\t" + dtRow["SecID"] + "\t" + dtRow["Code"] + "\t" + dtRow["Code2"] + "\t" + dtRow["Currency"] + "\t" +
                                       dtRow["CreditRating"] + "\t" + dtRow["MoodysRating"] + "\t" + dtRow["FitchsRating"] + "\t" + dtRow["SPRating"] + "\t" + dtRow["ICAPRating"] + "\t" +
                                       dtRow["CountryRisk_Title"] + "\t" + dtRow["Date2"] + "\t" + dtRow["Maturity"] + "\t" + dtRow["Maturity_Date"] + "\t" +
                                       dtRow["CurrencyHedge"] + "\t" + dtRow["CurrencyHedge2"] + "\t" + dtRow["SurveyedKIID"] + "\t" + dtRow["SurveyedKIID_Date"] + "\t" +
                                       dtRow["StockExchange_Code"] + "\t" + dtRow["Weight"] + "\t" + (Convert.ToInt32(dtRow["Retail"]) == 2 ? "Yes" : (Convert.ToInt32(dtRow["Retail"]) == 1 ? "No" : "")) + "\t" +
                                       (Convert.ToInt32(dtRow["Professional"]) == 2 ? "Yes" : (Convert.ToInt32(dtRow["Professional"]) == 1 ? "No" : "")) + "\t" +
                                       (Convert.ToInt32(dtRow["ComplexProduct"]) == 2 ? "Yes" : (Convert.ToInt32(dtRow["ComplexProduct"]) == 1 ? "No" : "")) + "\t" +
                                       dtRow["Distrib_ExecOnly"] + "\t" + dtRow["Distrib_Advice"] + "\t" + dtRow["Distrib_PortfolioManagment"] + "\t" + dtRow["MIFID_Risk"] + "\t" +
                                       "" + "\t" + dtRow["ID"] + "\t" + dtRow["ShareTitles_ID"] + "\t" + "" + "\t" + dtRow["OK_Flag"] + "\t" + dtRow["OK_String"]);
                            break;
                    }
                }

            }

            //----------------------------------------------------------------------------
            foundRows = dtViewsList.Select("Tipos = 1");
            if (foundRows.Length > 0) sViewString4 = foundRows[0]["FieldsList"].ToString();
            j = sViewString4.Length;

            fgList4_1.Cols[0].Visible = false;
            for (i = 0; i < j; i++) {
                if (sViewString4.Substring(i, 1) == "1") fgList4_1.Cols[i+2].Visible = true;
                else fgList4_1.Cols[i].Visible = false;
            }
            if (fgList4_1.Rows.Count > 1) fgList4_1.Row = 1;
            fgList4_1.Redraw = true;


            //----------------------------------------------------------------------------
            foundRows = dtViewsList.Select("Tipos = 2");
            if (foundRows.Length > 0) sViewString4 = foundRows[0]["FieldsList"].ToString();
            j = sViewString4.Length;

            fgList4_2.Cols[0].Visible = false;
            for (i = 0; i < j; i++)
            {
                if (sViewString4.Substring(i, 1) == "1") fgList4_2.Cols[i + 2].Visible = true;
                else fgList4_2.Cols[i].Visible = false;
            }
            if (fgList4_2.Rows.Count > 1) fgList4_2.Row = 1;
            fgList4_2.Redraw = true;

            //----------------------------------------------------------------------------
            foundRows = dtViewsList.Select("Tipos = 4");
            if (foundRows.Length > 0) sViewString4 = foundRows[0]["FieldsList"].ToString();
            j = sViewString4.Length;

            fgList4_4.Cols[0].Visible = false;
            for (i = 0; i < j; i++)
            {
                if (sViewString4.Substring(i, 1) == "1") fgList4_4.Cols[i + 2].Visible = true;
                else fgList4_4.Cols[i].Visible = false;
            }
            if (fgList4_4.Rows.Count > 1) fgList4_4.Row = 1;
            fgList4_4.Redraw = true;

            //----------------------------------------------------------------------------
            foundRows = dtViewsList.Select("Tipos = 6");
            if (foundRows.Length > 0) sViewString4 = foundRows[0]["FieldsList"].ToString();
            j = sViewString4.Length;

            fgList4_6.Cols[0].Visible = false;
            for (i = 0; i < j; i++)
            {
                if (sViewString4.Substring(i, 1) == "1") fgList4_6.Cols[i + 2].Visible = true;
                else fgList4_6.Cols[i].Visible = false;
            }
            if (fgList4_6.Rows.Count > 1) fgList4_6.Row = 1;
            fgList4_6.Redraw = true;
        }
        private void fgList4_1_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Row > 0) {
                if (e.Col == 36)
                    if (Convert.ToInt32(fgList4_1[e.Row, 36]) == 0) fgList4_1.Rows[e.Row].Style = csError;
                    else fgList4_1.Rows[e.Row].Style = null;
            }
        }
        private void fgList4_1_DoubleClick(object sender, EventArgs e)
        {
            frmProductData locProductData = new frmProductData();
            locProductData.Product_ID = 1;                                                                  // 1 - Shares
            locProductData.ShareCode_ID = Convert.ToInt32(fgList4_1[fgList4_1.Row, "ID"]);
            locProductData.Text = Global.GetLabel("product");
            locProductData.Show();

            k = fgList4_1.Row;
            Global.GetProductsList();
            DefineList();
            ShowList();
            fgList4_1.Row = k;
        }
        private void fgList4_2_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Row > 0)
            {
                if (e.Col == 36)
                    if (Convert.ToInt32(fgList4_2[e.Row, 36]) == 0) fgList4_2.Rows[e.Row].Style = csError;
                    else fgList4_2.Rows[e.Row].Style = null;
            }
        }
        private void fgList4_2_DoubleClick(object sender, EventArgs e)
        {
            frmProductData locProductData = new frmProductData();
            locProductData.Product_ID = 2;                                                                  // 2 - Bonds
            locProductData.ShareCode_ID = Convert.ToInt32(fgList4_2[fgList4_2.Row, "ID"]);
            locProductData.Text = Global.GetLabel("product");
            locProductData.Show();

            k = fgList4_2.Row;
            Global.GetProductsList();
            DefineList();
            ShowList();
            fgList4_2.Row = k;
        }
        private void fgList4_4_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Row > 0)
            {
                if (e.Col == 36)
                    if (Convert.ToInt32(fgList4_4[e.Row, 36]) == 0) fgList4_4.Rows[e.Row].Style = csError;
                    else fgList4_4.Rows[e.Row].Style = null;
            }
        }
        private void fgList4_4_DoubleClick(object sender, EventArgs e)
        {
            frmProductData locProductData = new frmProductData();
            locProductData.Product_ID = 4;                                                                  // 4 - ETFs
            locProductData.ShareCode_ID = Convert.ToInt32(fgList4_4[fgList4_4.Row, "ID"]);
            locProductData.Text = Global.GetLabel("product");
            locProductData.Show();

            k = fgList4_4.Row;
            Global.GetProductsList();
            DefineList();
            ShowList();
            fgList4_4.Row = k;
        }
        private void fgList4_6_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Row > 0)
            {
                if (e.Col == 36)
                    if (Convert.ToInt32(fgList4_6[e.Row, 36]) == 0) fgList4_6.Rows[e.Row].Style = csError;
                    else fgList4_6.Rows[e.Row].Style = null;
            }
        }
        private void fgList4_6_DoubleClick(object sender, EventArgs e)
        {
            frmProductData locProductData = new frmProductData();
            locProductData.Product_ID = 6;                                                                  // 6 - Funds
            locProductData.ShareCode_ID = Convert.ToInt32(fgList4_6[fgList4_6.Row, "ID"]);
            locProductData.Text = Global.GetLabel("product");
            locProductData.Show();

            k = fgList4_6.Row;
            Global.GetProductsList();
            DefineList();
            ShowList();
            fgList4_6.Row = k;
        }
        protected void ucCS_TextChanged(object sender, EventArgs e)
        {
            if (ucCS.Contract_ID.Text != "0") {
                stContract = ucCS.SelectedContractData;
                lblContractCode.Text = stContract.Code;
                lblContractPortfolio.Text = stContract.Portfolio;
                lblContractTitle.Text = stContract.ContractTitle;

                iContract_ID = stContract.Contract_ID;

                iProfile_ID = stContract.Profile_ID;
                cmbProfile.SelectedValue = iProfile_ID;

                iContractProfile_ID = 0;
                foundRows = Global.dtCustomersProfiles.Select("ID = " + iProfile_ID);
                if (foundRows.Length > 0) iContractProfile_ID = Convert.ToInt32(foundRows[0]["MiFID_Risk"]);

                iService_ID = stContract.Service_ID;
                cmbFinanceServices.SelectedValue = iService_ID;

                sCurrency = stContract.Currency;
                cmbCurrency4.Text = sCurrency;

                iMiFID_Category = stContract.MIFIDCategory_ID;
                cmbMiFiD.SelectedIndex = iMiFID_Category;

                sGeography = "";
                clsContracts klsContract = new clsContracts();
                klsContract.Details.Record_ID = iContract_ID;
                klsContract.GetRecord();
                lblGeorgraphy.Text = "Παγκόσμια - " + (klsContract.Details.ChkWorld == 1 ? "Ναί" : "Όχι") + "; Ελλάδα - " + (klsContract.Details.ChkGreece == 1 ? "Ναί" : "Όχι") + "; " +
                                     "Ευρώπη (εκτός Ελλάδας) - " + (klsContract.Details.ChkEurope == 1 ? "Ναί" : "Όχι") + "; Αμερική - " + (klsContract.Details.ChkAmerica == 1 ? "Ναί" : "Όχι") + "; " +
                                     "Ασία - " + (klsContract.Details.ChkAsia == 1 ? "Ναί" : "Όχι");
                sGeography = (klsContract.Details.ChkWorld == 1 ? "1" : "0") + (klsContract.Details.ChkGreece == 1 ? "1" : "0") + (klsContract.Details.ChkEurope == 1 ? "1" : "0") +
                             (klsContract.Details.ChkAmerica == 1 ? "1" : "0") + (klsContract.Details.ChkAsia == 1 ? "1" : "0");


                iComplexProduct = 0;
                clsContracts_ComplexSigns klsContract_ComplexSigns = new clsContracts_ComplexSigns();
                klsContract_ComplexSigns.Contract_ID = iContract_ID;
                klsContract_ComplexSigns.GetList();
                foreach (DataRow dtRow in klsContract_ComplexSigns.List.Rows)
                {
                    if (Convert.ToInt32(dtRow["ComplexSign_ID"]) == 1) iComplexProduct = 1;
                    if (Convert.ToInt32(dtRow["ComplexSign_ID"]) == 2) iComplexProduct = 2;
                }
                cmbComplexProduct.SelectedIndex = iComplexProduct;     // 0 - Unknown, 1 - No, 2 - Yes

                chkShowAll.Checked = false;
                panCrits.Enabled = true;

                DefineList();
            }
        }
        private void tsbExcel_List4_Click(object sender, EventArgs e)
        {
            int i, j, k, m;
            var EXL = new Microsoft.Office.Interop.Excel.Application();
            var oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;

            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            // System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("el-GR");
            var WB = EXL.Workbooks.Add();
            EXL.ScreenUpdating = false;
            EXL.Caption = Global.AppTitle;

            Excel.Style cstrueStyle = EXL.Application.ActiveWorkbook.Styles.Add("trueStyle");
            cstrueStyle.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Green);

            Excel.Style csfalseStyle = EXL.Application.ActiveWorkbook.Styles.Add("falseStyle");
            csfalseStyle.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);


            //--- Sheet 1 ---------------------------------------------------------------------
            EXL.Cells[1, 1].Value = "Καταλληλότητα Προϊόντων - Μετοχές";
            EXL.Cells[2, 1].Value = "Ημερομηνία:  " + DateTime.Now.ToString("dd/MM/yyyy");
            EXL.Cells[3, 1].Value = "Χρήστης: " + Global.UserName;

            m = 3;
            for (i = 0; i <= fgList4_1.Rows.Count - 1; i++) {
                m = m + 1;
                k = 0;
                for (j = 1; j <= fgList4_1.Cols.Count - 1; j++) {
                    if (fgList4_1.Cols[j].Visible) k = k + 1;
                    EXL.Cells[m, k].Value = fgList4_1[i, j];
                }
            }

            //--- Sheet 2 ---------------------------------------------------------------------
            Excel.Worksheet ws2 = WB.Worksheets.Add(System.Reflection.Missing.Value, WB.Worksheets[WB.Worksheets.Count], System.Reflection.Missing.Value, System.Reflection.Missing.Value);

            ws2.Cells[1, 1].Value = "Καταλληλότητα Προϊόντων - Ομόλογα";
            ws2.Cells[2, 1].Value = "Ημερομηνία:  " + DateTime.Now.ToString("dd/MM/yyyy");
            ws2.Cells[3, 1].Value = "Χρήστης: " + Global.UserName;
            m = 3;
            for (i = 0; i <= fgList4_2.Rows.Count - 1; i++)
            {
                m = m + 1;
                k = 0;
                for (j = 1; j <= fgList4_2.Cols.Count - 1; j++)
                {
                    if (fgList4_2.Cols[j].Visible) k = k + 1;
                    ws2.Cells[m, k].Value = fgList4_2[i, j];
                }
            }

            //--- Sheet 3 ---------------------------------------------------------------------
            Excel.Worksheet ws3 = WB.Worksheets.Add(System.Reflection.Missing.Value, WB.Worksheets[WB.Worksheets.Count], System.Reflection.Missing.Value, System.Reflection.Missing.Value);

            ws3.Cells[1, 1].Value = "Καταλληλότητα Προϊόντων - ΔΑΚ";
            ws3.Cells[2, 1].Value = "Ημερομηνία:  " + DateTime.Now.ToString("dd/MM/yyyy");
            ws3.Cells[3, 1].Value = "Χρήστης: " + Global.UserName;
            m = 3;
            for (i = 0; i <= fgList4_4.Rows.Count - 1; i++)
            {
                m = m + 1;
                k = 0;
                for (j = 1; j <= fgList4_4.Cols.Count - 1; j++)
                {
                    if (fgList4_4.Cols[j].Visible) k = k + 1;
                    ws3.Cells[m, k].Value = fgList4_4[i, j];
                }
            }

            //--- Sheet 4 ---------------------------------------------------------------------
            Excel.Worksheet ws4 = WB.Worksheets.Add(System.Reflection.Missing.Value, WB.Worksheets[WB.Worksheets.Count], System.Reflection.Missing.Value, System.Reflection.Missing.Value);

            ws4.Cells[1, 1].Value = "Καταλληλότητα Προϊόντων - ΑΚ";
            ws4.Cells[2, 1].Value = "Ημερομηνία:  " + DateTime.Now.ToString("dd/MM/yyyy");
            ws4.Cells[3, 1].Value = "Χρήστης: " + Global.UserName;
            m = 3;
            for (i = 0; i <= fgList4_6.Rows.Count - 1; i++)
            {
                m = m + 1;
                k = 0;
                for (j = 1; j <= fgList4_6.Cols.Count - 1; j++)
                {
                    if (fgList4_6.Cols[j].Visible) k = k + 1;
                    ws4.Cells[m, k].Value = fgList4_6[i, j];
                }
            }

            EXL.ScreenUpdating = true;
            EXL.Visible = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;

            this.Cursor = Cursors.Default;

            EXL.Quit();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();

        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
