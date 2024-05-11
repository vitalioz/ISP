using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Core;

namespace Transactions
{
    public partial class frmOrderBasket : Form
    {
        DataTable dtList;
        DataRow dtRow;
        DataView dtView;
        int i, j, k, m, n, iBestExecution, iNewClientPackage_ID, iChoiceBusinessType_ID, iChoiceProvider_ID, iProduct_ID, 
            iProductCategory_ID, iPackageProvider_ID, iLastAktion, iClient_ID, iChoiceShare_ID, iChoiceProduct_ID, iPressAction, iBulcCommand_ID;
        decimal sgTemp, sgTemp1, sgTemp2;
        string sTemp, sTemp1, sNewCode, sNewPortfolio;
        bool bFound, bCheckList, bCanChoice;
        DateTime dToday;
        DataRow[] foundRows;
        C1.Win.C1FlexGrid.CellRange rng;
        CellStyle csBuy, csSell, csOver;
        clsOrdersSecurity klsOrder = new clsOrdersSecurity();
        clsProductsCodes klsProductsCodes = new clsProductsCodes();
        public frmOrderBasket()
        {
            InitializeComponent();

            iChoiceProvider_ID = 0;
            iPressAction = 0;
        }

        private void frmOrderBasket_Load(object sender, EventArgs e)
        {
            bCheckList = false;
            bCanChoice = false;
            iLastAktion = 0;
            iClient_ID = 0;

            btnFinish.Text = "Διαβίβαση εντολής";
            btnFinish.Enabled = false;

            //------- fgSummary ----------------------------
            fgSummary.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgSummary.Styles.ParseString(Global.GridStyle);
            //fgSummary.RowColChange += new EventHandler(fgSummary_RowColChange);
            //fgSummary.MouseDown += new MouseEventHandler(fgSummary_MouseDown);
            fgSummary.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgSummary_BeforeEdit);
            fgSummary.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgSummary_AfterEdit);
            fgSummary.OwnerDrawCell += fgSummary_OwnerDrawCell;

            fgSummary.DrawMode = DrawModeEnum.OwnerDraw;
            fgSummary.ShowCellLabels = true;

            fgSummary.Styles.Normal.WordWrap = true;
            fgSummary.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgSummary.Rows[0].AllowMerging = true;

            fgSummary.Cols[0].AllowMerging = true;
            rng = fgSummary.GetCellRange(0, 0, 1, 0);
            rng.Data = " ";

            rng = fgSummary.GetCellRange(0, 1, 0, 3);
            rng.Data = Global.GetLabel("product");

            fgSummary[1, 1] = Global.GetLabel("title");
            fgSummary[1, 2] = Global.GetLabel("code");
            fgSummary[1, 3] = Global.GetLabel("isin");

            fgSummary.Cols[4].AllowMerging = true;
            rng = fgSummary.GetCellRange(0, 4, 1, 4);
            rng.Data = Global.GetLabel("provider");

            fgSummary.Cols[5].AllowMerging = true;
            rng = fgSummary.GetCellRange(0, 5, 1, 5);
            rng.Data = "Χρηματιστήριο";

            fgSummary.Cols[6].AllowMerging = true;
            rng = fgSummary.GetCellRange(0, 6, 1, 6);
            rng.Data = "Σύνολο Εντολών";

            fgSummary.Cols[7].AllowMerging = true;
            rng = fgSummary.GetCellRange(0, 7, 1, 7);
            rng.Data = Global.GetLabel("transaction");

            fgSummary.Cols[8].AllowMerging = true;
            rng = fgSummary.GetCellRange(0, 8, 1, 8);
            rng.Data = Global.GetLabel("price");

            fgSummary.Cols[9].AllowMerging = true;
            rng = fgSummary.GetCellRange(0, 9, 1, 9);
            rng.Data = Global.GetLabel("quantity");

            fgSummary.Cols[10].AllowMerging = true;
            rng = fgSummary.GetCellRange(0, 10, 1, 10);
            rng.Data = "Ποσό Επενδύσεις";

            fgSummary.Cols[11].AllowMerging = true;
            rng = fgSummary.GetCellRange(0, 11, 1, 11);
            rng.Data = "Νόμισμα";

            fgSummary.Cols[12].AllowMerging = true;
            rng = fgSummary.GetCellRange(0, 12, 1, 12);
            rng.Data = "Ελάχιστη ποσότητα";

            fgSummary.Cols[13].AllowMerging = true;
            rng = fgSummary.GetCellRange(0, 13, 1, 13);
            rng.Data = "Ελάχιστο βήμα";

            fgSummary.Redraw = false;
            fgSummary.Rows.Count = 2;

            csBuy = fgSummary.Styles.Add("Buy");
            csBuy.BackColor = Color.LightGreen;
            csBuy.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);

            csSell = fgSummary.Styles.Add("Sell");
            csSell.BackColor = Color.LightCoral;
            csSell.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);

            csOver = fgSummary.Styles.Add("Over");
            csOver.BackColor = Color.Yellow;

            //------- fgSimpleCommands ----------------------------
            fgSimpleCommands.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgSimpleCommands.Styles.ParseString(Global.GridStyle);
            fgSimpleCommands.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgSimpleCommands_BeforeEdit);
            fgSimpleCommands.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgSimpleCommands_AfterEdit);
            fgSimpleCommands.ShowCellLabels = true;

            //--- Define Unsent Commands List (only Single & DMP Commands) ----------------------
            klsOrder.DateFrom = dToday;
            klsOrder.DateTo = dToday;
            klsOrder.GetUnsentList();

            foreach (DataRow dtRow in klsOrder.List.Rows) {
                if (Convert.ToDateTime(dtRow["RecieveDate"]) != Convert.ToDateTime("1900/01/01") && 
                   (Convert.ToDateTime(dtRow["SentDate"]) == Convert.ToDateTime("1900/01/01"))) { 
                    bFound = false;
                    j = 0;
                    if (Convert.ToInt32(dtRow["Client_ID"]) == 3227) iClient_ID = Convert.ToInt32(dtRow["Client_ID"]);   // 3227 - is HellasFin AEPE. It can't merge with other clients                    
                    else                                             iClient_ID = 0;

                    sTemp = dtRow["Share_ID"] + "";
                    k = fgSummary.FindRow(sTemp, 2, 14, false);      // 14 - Share_ID
                    if (k > 0)                                       // if k > 0 - it means that product with ID = dtRow["Share_ID"] exists in fgSummary list
                        for (j = 2; j <= fgSummary.Rows.Count - 1; j++)
                            if ( (Convert.ToInt32(dtRow["Share_ID"]) == Convert.ToInt32(fgSummary[j, "Share_ID"])) &&
                                 ((dtRow["Aktion"] + "") == (fgSummary[j, "Aktion"] + "")) && 
                                 ((dtRow["ProductStockExchange_Code"] + "") == (fgSummary[j, "StockExchange_Code"] + "")) &&
                                 (Convert.ToInt32(dtRow["ServiceProvider_ID"]) == Convert.ToInt32(fgSummary[j, "Provider_ID"])) && 
                                 (iClient_ID == Convert.ToInt32(fgSummary[j, "Client_ID"])))  {                                             // dtRow["BusinessType_ID"] = fgSummary[j, "BusinessType_ID") And 
                               bFound = true;
                               break;
                            }

                    if (bFound) {
                        fgSummary[j, "Commands_Count"] = Convert.ToInt32(fgSummary[j, "Commands_Count"]) + 1;

                        sTemp = (Global.IsNumeric(dtRow["Quantity"])? (dtRow["Quantity"] + ""): "0");
                        fgSummary[j, "Quantity"] = Convert.ToDouble(fgSummary[j, "Quantity"]) + Convert.ToDouble(sTemp.Replace(".", ""));

                            sTemp = (Global.IsNumeric(dtRow["Amount"])? (dtRow["Amount"] + "") : "0");
                        fgSummary[j, "Amount"] = Convert.ToDouble(fgSummary[j, "Amount"]) + Convert.ToDouble(sTemp.Replace(".", ""));
                    }
                    else  // add new Row into fgSummary
                        fgSummary.AddItem(false + "\t" + dtRow["Share_Title"] + "\t" + dtRow["Share_Code"] + "\t" + dtRow["Share_ISIN"] + "\t" +
                                      dtRow["ServiceProvider_Title"] + "\t" + dtRow["ProductStockExchange_Code"] + "\t" + "1" + "\t" + dtRow["Aktion"] + "\t" +
                                      dtRow["Price"] + "\t" + dtRow["Quantity"] + "\t" + dtRow["Amount"] + "\t" + dtRow["Currency"] + "\t" + dtRow["QuantityMin"] + "\t" +
                                      dtRow["QuantityStep"] + "\t" + dtRow["Share_ID"] + "\t" + dtRow["ServiceProvider_ID"] + "\t" + dtRow["BusinessType_ID"] + "\t" +
                                      dtRow["Product_ID"] + "\t" + dtRow["ProductCategory_ID"] + "\t" + dtRow["ProductStockExchange_ID"] + "\t" + iClient_ID + "\t" +
                                      dtRow["ID"] + "\t" + dtRow["CommandType_ID"] + "\t" + dtRow["PriceType"]);               
                }
            }
            fgSummary.Sort(SortFlags.Ascending, 1);
            fgSummary.Redraw = true;

            bCheckList = true;
            iChoiceShare_ID = 0;
            iChoiceProduct_ID = 0;
        }
        private void cmbServiceProviders_SelectedValueChanged(object sender, EventArgs e)
        {
            DefineProviderData();
        }
        private void fgSummary_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (bCheckList) {
                if (e.Col == 0) e.Cancel = false;
                else e.Cancel = true;

                DefineFinishButton();
            }
        }
        private void fgSummary_AfterEdit(object sender, RowColEventArgs e)
        {
            if (bCheckList) {
                if (e.Col == 0) {
                    if (iChoiceShare_ID == 0) {                                                          // it's first choice from fgSummary List
                        sNewCode = "";
                        sNewPortfolio = "";
                        iNewClientPackage_ID = 0;
                        iBestExecution = 0;
                        cmbConstant.SelectedIndex = 0;

                        k = fgSummary.Row;
                        iChoiceBusinessType_ID = Convert.ToInt32(fgSummary[k, "BusinessType_ID"]);
                        iChoiceProvider_ID = Convert.ToInt32(fgSummary[k, "Provider_ID"]);
                        iChoiceShare_ID = Convert.ToInt32(fgSummary[k, "Share_ID"]);
                        iChoiceProduct_ID = Convert.ToInt32(fgSummary[k, "Product_ID"]);
                        lblProductTitle.Text = fgSummary[k, "Share_Title"] + "";
                        lblProductCode.Text = fgSummary[k, "Share_Code"] + "";
                        lblProductISIN.Text = fgSummary[k, "Share_ISIN"] + "";
                        lblServiceProvider.Text = fgSummary[k, "Provider_Title"] + "";
                        lblAction.Text = fgSummary[k, "Aktion"] + "";
                        if (Convert.ToInt32(fgSummary[k, "PriceType"] + "") == 0) {
                            lstType.SelectedIndex = 0;
                            sTemp = fgSummary[k, "Price"] + "";
                            txtPrice.Text = sTemp.Replace(".", ",");
                        }
                        else  {
                            lstType.SelectedIndex = Convert.ToInt32((fgSummary[k, "PriceType"]) + "");
                            txtPrice.Text = "0";
                        }
                        lblQuantity.Text = fgSummary[k, "Quantity"] + "";
                        lblAmount.Text = fgSummary[k, "Amount"] + "";
                        if (Convert.ToDecimal(lblAmount.Text) == 0) {
                            if (Global.IsNumeric(txtPrice.Text) && Global.IsNumeric(lblQuantity.Text)) 
                               lblAmount.Text = (Convert.ToDecimal(txtPrice.Text) * Convert.ToDecimal(lblQuantity.Text)).ToString();
                        }
                        lblAmountCurr.Text = fgSummary[k, "Currency"] + "";
                        if (Global.IsNumeric(lblAmount.Text)) CalcAmountEUR();

                        iProduct_ID = Convert.ToInt32(fgSummary[k, "Product_ID"]);
                        iProductCategory_ID = Convert.ToInt32(fgSummary[k, "ProductCategory_ID"]);

                        DefineServiceProvidersList();

                        if (iChoiceBusinessType_ID == 1) {                                                // 1 - non execution order
                               clsCompanyCodes klsCompanyCode = new clsCompanyCodes();
                            klsCompanyCode.Record_ID = 0;
                            klsCompanyCode.ServiceProvider_ID = Convert.ToInt32(fgSummary[k, "Provider_ID"]);
                            klsCompanyCode.GetRecord();
                            sNewCode = klsCompanyCode.Code;
                            sNewPortfolio = klsCompanyCode.Portfolio;
                        }
                        else {                                                            // else - execution order
                            DefineProviderData();
                        }

                        dtList = new DataTable();
                        dtList.Columns.Add("ID", typeof(int));
                        dtList.Columns.Add("Title", typeof(string));

                        clsProductsCodes klsProductCode = new clsProductsCodes();
                        klsProductCode.Share_ID = 0;
                        klsProductCode.ISIN = lblProductISIN.Text;
                        klsProductCode.GetList();
                        foreach (DataRow dtRow1 in klsProductCode.List.Rows)
                        {
                            if (fgSummary[k, "Currency"] + "" == dtRow1["Currency"] + "" && Convert.ToInt32(dtRow1["Aktive"]) == 1)
                            {
                                foundRows = dtList.Select("ID = " + dtRow1["StockExchange_ID"]);
                                if (foundRows.Length == 0)
                                {
                                    dtRow = dtList.NewRow();
                                    dtRow["ID"] = dtRow1["StockExchange_ID"];
                                    dtRow["Title"] = dtRow1["StockExchange_Code"];
                                    dtList.Rows.Add(dtRow);
                                }
                            }
                        }

                        cmbStockExchange.DataSource = dtList.Copy();
                        cmbStockExchange.DisplayMember = "Title";
                        cmbStockExchange.ValueMember = "ID";
                        cmbStockExchange.SelectedValue = Convert.ToInt32(fgSummary[k, "StockExchange_ID"]);

                        panExecutors.Visible = true;
                        bCanChoice = true;
                    }
                    else {                                                                                                        // it isn't first choice from fgSummary
                        if ((iChoiceShare_ID != Convert.ToInt32(fgSummary[fgSummary.Row, "Share_ID"])) ||                         // iChoiceBusinessType_ID <> fgSummary[fgSummary.Row, "BusinessType_ID") Or 
                             (iChoiceProvider_ID != Convert.ToInt32(fgSummary[fgSummary.Row, "Provider_ID"])) ||
                             (lblAction.Text != (fgSummary[fgSummary.Row, "Aktion"]+""))) {
                            fgSummary[fgSummary.Row, 0] = false;
                            MessageBox.Show("Δεν γίνεται ομαδοποίηση εντολών με διαφορετικό πάροχο ή προϊόν ή πράξη ή ISIN", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            bCanChoice = false;
                        }
                        else bCanChoice = true;
                    }

                    if (bCanChoice) {
                        fgSimpleCommands.Redraw = false;
                        fgSimpleCommands.Rows.Count = 1;

                        m = 0;
                        k = 0;
                        sgTemp = 0;
                        sgTemp1 = 0;
                        for (j = 2; j <= fgSummary.Rows.Count - 1; j++) {
                            if (Convert.ToBoolean(fgSummary[j, 0])) {
                                k = k + 1;
                                foreach (DataRow dtRow in klsOrder.List.Rows)
                                {
                                    if (Convert.ToInt32(dtRow["Client_ID"]) == 3227) iClient_ID = Convert.ToInt32(dtRow["Client_ID"]);    // 3227 - is HellasFin AEPE. It can't merge with other clients
                                    else iClient_ID = 0;

                                    sTemp = dtRow["ID"] + "";
                                    n = fgSimpleCommands.FindRow(sTemp, 1, 12, false);      // 12 - fgSimpleCommands.ID
                                    if (n < 0) {                                            // n < 0 - order with dtRow["ID"] not exists in fgSimpleCommands yet
                                        if ((Convert.ToInt32(dtRow["Share_ID"]) == Convert.ToInt32(fgSummary[j, "Share_ID"])) && ((dtRow["Aktion"] + "") == (fgSummary[j, 7] + "")) &&
                                             ((dtRow["ProductStockExchange_Code"] + "") == (fgSummary[j, "StockExchange_Code"] + "")) &&
                                             (Convert.ToInt32(dtRow["ServiceProvider_ID"]) == Convert.ToInt32(fgSummary[j, "Provider_ID"])) && (iClient_ID == Convert.ToInt32(fgSummary[j, "Client_ID"])))
                                        {          //    dtRow["BusinessType_ID") = fgSummary[j, "BusinessType_ID") And 
                                            sgTemp2 = 0;
                                            if (Global.IsNumeric(dtRow["Amount"])) sgTemp2 = Convert.ToDecimal(dtRow["Amount"]);

                                            if (Global.IsNumeric(dtRow["Price"]) && Global.IsNumeric(dtRow["Quantity"]))
                                                sgTemp2 = (Convert.ToDecimal(dtRow["Price"]) * Convert.ToDecimal(dtRow["Quantity"]));

                                            if (Convert.ToInt32(dtRow["Product_ID"]) == 2) sgTemp2 = sgTemp2 / 100;          // 2 - bond (omologo)

                                            if (Convert.ToInt32(dtRow["CommandType_ID"]) == 4) {                             // 4 - it's DPM Order, so...
                                                sTemp = dtRow["Company_Title"]+"";
                                                sTemp1 = "";
                                            }
                                            else
                                            {
                                                sTemp = dtRow["ClientFullName"] + "";
                                                sTemp1 = dtRow["ContractTitle"] + "";
                                            }

                                            m = m + 1;
                                            fgSimpleCommands.AddItem(true + "\t" + m + "\t" + sTemp + "\t" + sTemp1 + "\t" + 
                                                                     dtRow["ServiceProvider_Title"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" +
                                                                     dtRow["Price"] + "\t" + dtRow["Quantity"] + "\t" + sgTemp2 + "\t" + dtRow["Currency"] + "\t" +
                                                                     dtRow["Constant"] + " " + dtRow["ConstantDate"] + "\t" + dtRow["ID"] + "\t" + dtRow["PriceType"] + "\t" +
                                                                     dtRow["CommandType_ID"] + "\t" + dtRow["BulkCommand"] + "\t" + dtRow["AktionDate"]);

                                            sgTemp = sgTemp + Convert.ToDecimal(dtRow["Quantity"]);
                                            sgTemp1 = sgTemp1 + sgTemp2;

                                            if (m == 1) {                                                       //  Price & Constant will visible only for 1-st row in fgSimpleCommands
                                                if (Convert.ToInt32(dtRow["PriceType"]) == 0) {
                                                    lstType.SelectedIndex = 0;
                                                    sTemp = dtRow["Price"] + "";
                                                    txtPrice.Text = sTemp.Replace(".", ",");
                                                }
                                                else {
                                                    lstType.SelectedIndex = Convert.ToInt32(dtRow["PriceType"]);
                                                    txtPrice.Text = "0";
                                                }

                                                cmbConstant.SelectedIndex = Convert.ToInt32(dtRow["Constant_ID"]);
                                                if (Convert.ToInt32(dtRow["Constant_ID"]) == 2) {
                                                    dConstant.Value = Convert.ToDateTime(dtRow["ConstantDate"]);
                                                    dConstant.Visible = true;
                                                }
                                                else dConstant.Visible = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        /*
                        if (((fgSimpleCommands.Rows.Count - 1) == 1) && (Convert.ToInt32(fgSummary[fgSummary.Row, "BusinessType_ID"]) == 1))
                        {                 // was     if fgSummary[fgSummary.Row, "Commands_Count") = 1 And fgSummary[fgSummary.Row, "BusinessType_ID") = 1
                            //btnFinish.Text = "Διαβίβαση εντολής";
                            iPressAction = 1;
                            
                        }
                        else  {
                            if (fgSimpleCommands.Rows.Count > 1) {
                                btnFinish.Text = "Create Order";
                                iPressAction = 2;
                            }
                            else {
                                btnFinish.Text = "Διαβίβαση εντολής";
                                btnFinish.Enabled = false;
                            }
                        }
                        */
                        DefineFinishButton();

                        if (k == 0) {                 // not exists checked record
                            Empty_Selection();
                        }

                        fgSimpleCommands.Redraw = true;

                        lblQuantity.Text = sgTemp.ToString();                                                        // was sgTemp.ToString("0.00");
                        lblAmount.Text = sgTemp1.ToString();                                                         // sgTemp1.ToString("0.00");
                        //lblAmountCurr.Text = fgSummary[fgSummary.Row, "Currency"] + "";
                        if (Global.IsNumeric(lblAmount.Text)) CalcAmountEUR();
                    }

                    if (fgSimpleCommands.Rows.Count > 1) btnFinish.Enabled = true;
                    else {
                        DefineFinishButton();
                        //btnFinish.Text = "Διαβίβαση εντολής";
                        //btnFinish.Enabled = false;
                    }
                }
            }
        }
        private void fgSummary_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            if (e.Row > 1) {
                if (e.Col == 7)                                                                             // 7 - Aktion
                    if (fgSummary[e.Row, "Aktion"] + "" == "BUY") e.Style = csBuy;
                    else e.Style = csSell;

                if (e.Col == 12)                                                                           // 12 - QuantityMin
                    if (Convert.ToDecimal(fgSummary[e.Row, "Quantity"]) >= Convert.ToDecimal(fgSummary[e.Row, "QuantityMin"]))
                        fgSummary.Rows[e.Row].Style = csOver;
            }
        }
        private void fgSimpleCommands_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (bCheckList) {
                if (e.Col == 0) e.Cancel = false;
                else e.Cancel = true;
            }
        }
        private void fgSimpleCommands_AfterEdit(object sender, RowColEventArgs e)
        {
            if (bCheckList) {
                sgTemp = 0;                                                                               // <----- Quantity
                for (i = 1; i <= fgSimpleCommands.Rows.Count - 1; i++) {
                    if (Convert.ToBoolean(fgSimpleCommands[i, 0])) {
                        if (Global.IsNumeric(fgSimpleCommands[i, "Price"] + ""))  txtPrice.Text = fgSimpleCommands[i, "Price"] + "";
                        if (Global.IsNumeric(fgSimpleCommands[i, "Quantity"])) sgTemp = sgTemp + Convert.ToDecimal(fgSimpleCommands[i, "Quantity"]);
                    }
                }
                lblQuantity.Text = sgTemp.ToString("0.00");

                if (Global.IsNumeric(txtPrice.Text))
                    lblAmount.Text = (Convert.ToDecimal(txtPrice.Text) * Convert.ToDecimal(lblQuantity.Text)).ToString("0.00");
                else {
                    sgTemp = 0;                                                                             // <----- Amount
                    for (i = 1; i <= fgSimpleCommands.Rows.Count - 1; i++) {
                        if (Convert.ToBoolean(fgSimpleCommands[i, 0]))
                            if (Global.IsNumeric(fgSimpleCommands[i, "Amount"]))
                                sgTemp = sgTemp + Convert.ToDecimal(fgSimpleCommands[i, "Amount"]);

                        lblAmount.Text = sgTemp.ToString("0.00");
                    }
                }
                if (Convert.ToInt32(fgSummary[fgSummary.Row, "Product_ID"]) == 2)                          // 2 - bond (omologo)
                    lblAmount.Text = (Convert.ToDecimal(lblAmount.Text) / Convert.ToDecimal(100)).ToString("0.00");

                CalcAmountEUR();

                if (sgTemp == 0) btnFinish.Enabled = false;
                else btnFinish.Enabled = true;

                DefineFinishButton();
            }
        }
        private void cmbConstant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbConstant.SelectedIndex) == 2) {
                dConstant.Value = DateTime.Now;
                dConstant.Visible = true;
            }
            else dConstant.Visible = false;
        }
        private void txtPrice_LostFocus(object sender, EventArgs e)
        {
            if (Global.IsNumeric(txtPrice.Text) && Global.IsNumeric(lblQuantity.Text))
                lblAmount.Text = (Convert.ToDecimal(txtPrice.Text) * Convert.ToDecimal(lblQuantity.Text)).ToString();

            if (fgSummary.Rows.Count > 2) lblAmountCurr.Text = fgSummary[fgSummary.Row, "Currency"] + "";
            if (Global.IsNumeric(lblAmount.Text)) CalcAmountEUR();
        }
        private void DefineFinishButton()
        {
            int j = 0;                                                              // j - selected data rows count into fgSimpleCommands grid
            for (i = 1; i <= fgSimpleCommands.Rows.Count - 1; i++) {
                if (Convert.ToBoolean(fgSimpleCommands[i, 0]))
                    j = j + 1;
            }

            switch (Convert.ToInt32(fgSummary[fgSummary.Row, "BusinessType_ID"])) {
                case 1:
                    if (j == 1)  {
                        btnFinish.Enabled = true;
                        btnFinish.Text = "Διαβίβαση εντολής";
                        iPressAction = 1;
                    }
                    else if (j > 1) {
                        btnFinish.Enabled = true;
                        btnFinish.Text = "Create Bulk Order";
                        iPressAction = 3;
                    }
                    else {
                        Empty_Selection();
                        btnFinish.Enabled = false;
                        btnFinish.Text = "Διαβίβαση εντολής";
                        iPressAction = 0;
                        cmbServiceProviders.DataSource = null;
                    }
                    break;
                case 2:
                    if (j > 0)
                    {
                        btnFinish.Enabled = true;
                        btnFinish.Text = "Create Execution Order";
                        iPressAction = 2;
                    }
                    else {
                        Empty_Selection();
                        btnFinish.Enabled = false;
                        btnFinish.Text = "Create Execution Order";
                        iPressAction = 0;
                        cmbServiceProviders.DataSource = null;
                    }
                    break;
            }
        }
        private void btnFinish_Click(object sender, EventArgs e)
        {
            int n = 0;
            string sTemp = "", sTemp1 = "";

            if (Convert.ToInt32(cmbServiceProviders.SelectedValue) == 0)
                MessageBox.Show("Επιλέξτε έναν πάροχο", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                if (iPressAction == 1) {                                                        // Only Diavivasi
                    clsOrdersSecurity klsOrder = new clsOrdersSecurity();
                    klsOrder.Record_ID = Convert.ToInt32(fgSimpleCommands[1, 12]);
                    klsOrder.GetRecord();
                    klsOrder.Constant = cmbConstant.SelectedIndex;
                    klsOrder.ConstantDate = dConstant.Value.ToString("dd/MM/yyyy");
                    //klsOrder.Price = Convert.ToDecimal(txtPrice.Text);
                    //klsOrder.Amount = Convert.ToDecimal(txtPrice.Text) * klsOrder.Quantity;
                    klsOrder.SentDate = DateTime.Now;
                    klsOrder.EditRecord();
                }
                else {                                                                           // Create Bulk or Execution Command
                    int iID, iChoiceProduct_ID, iChoiceProductCategory_ID, iCustodyProvider_ID, iProvider_ID;

                    txtPrice.Text = txtPrice.Text.Replace(".", ",");
                    iID = 0;
                    clsOrdersSecurity klsOrder = new clsOrdersSecurity();
                    iBulcCommand_ID = klsOrder.GetNextBulkCommand();

                    //--- edit BulkCommand_ID in current simple Command -------------------
                    for (i = 1; i <= fgSimpleCommands.Rows.Count - 1; i++) {
                        if (Convert.ToBoolean(fgSimpleCommands[i, 0])) {
                            if ((fgSimpleCommands[i, "BulkCommand"] + "") == "" || (fgSimpleCommands[i, "BulkCommand"] + "") == "0") {
                                klsOrder = new clsOrdersSecurity();
                                klsOrder.Record_ID = Convert.ToInt32(fgSimpleCommands[i, "ID"]);
                                klsOrder.GetRecord();
                                if (klsOrder.BulkCommand == "" || klsOrder.BulkCommand == "0") klsOrder.BulkCommand = "<" + iBulcCommand_ID + ">";
                                else klsOrder.BulkCommand = klsOrder.BulkCommand + "/<" + iBulcCommand_ID + ">";
                                //klsOrder.Constant = cmbConstant.SelectedIndex;
                                //klsOrder.ConstantDate = dConstant.Value.ToString("dd/MM/yyyy");
                                //klsOrder.Price = Convert.ToDecimal(txtPrice.Text);
                                //klsOrder.Amount = Convert.ToDecimal(txtPrice.Text) * klsOrder.Quantity;
                                klsOrder.SentDate = DateTime.Now;  // ??????
                                klsOrder.EditRecord();
                            }
                            else {
                                klsOrder = new clsOrdersSecurity();
                                klsOrder.Record_ID = Convert.ToInt32(fgSimpleCommands[i, "ID"]);
                                klsOrder.GetRecord();
                                if (Convert.ToInt32(fgSimpleCommands[i, "CommandType_ID"]) == 4) {
                                    //--- recreate BulkCommand for this order---     
                                    sTemp1 = fgSimpleCommands[i, "BulkCommand"] + "";
                                    n = sTemp1.IndexOf("/");
                                    if (n >= 0) sTemp = "<" + iBulcCommand_ID + ">/" + sTemp1.Substring(n + 1);
                                    else sTemp = "<" + iBulcCommand_ID + ">/" + fgSimpleCommands[i, "BulkCommand"];
                                    //-----------------------------------------
                                    klsOrder.BulkCommand = sTemp;
                                    klsOrder.Depository_ID = 4;                   // it's Depository_ID, it's flag that order was transfered for execution or create new execution order (4)
                                    //klsOrder.Constant = cmbConstant.SelectedIndex;
                                    //klsOrder.ConstantDate = dConstant.Value.ToString("dd/MM/yyyy");
                                    //klsOrder.Price = Convert.ToDecimal(txtPrice.Text);
                                    //klsOrder.Amount = Convert.ToDecimal(txtPrice.Text) * klsOrder.Quantity;
                                    klsOrder.SentDate = DateTime.Now;
                                }
                                else {
                                    sTemp = "<" + iBulcCommand_ID + ">";
                                    klsOrder.BulkCommand = sTemp;
                                    //klsOrder.Constant = cmbConstant.SelectedIndex;
                                    //klsOrder.ConstantDate = dConstant.Value.ToString("dd/MM/yyyy");
                                    //klsOrder.Price = Convert.ToDecimal(txtPrice.Text);
                                    //klsOrder.Amount = Convert.ToDecimal(txtPrice.Text) * klsOrder.Quantity;
                                    klsOrder.SentDate = DateTime.Now;
                                }                                
                                klsOrder.EditRecord();
                            }
                        }
                    }

                    //--- Define Product data --------------------------------------------
                    clsProductsCodes klsProductCode = new clsProductsCodes();
                    klsProductCode.ISIN = lblProductISIN.Text;
                    klsProductCode.Currency = fgSummary[fgSummary.Row, "Currency"] + "";
                    klsProductCode.StockExchange_ID = Convert.ToInt32(cmbStockExchange.SelectedValue);
                    klsProductCode.Status = 1;
                    klsProductCode.GetRecord_ISIN();
                    iChoiceProduct_ID = klsProductCode.Product_ID;
                    iChoiceProductCategory_ID = klsProductCode.ProductCategory_ID;

                    //--- Define Custody Provider ID -------------------------------------
                    clsServiceProviders klsServiceProviders = new clsServiceProviders();
                    if (iChoiceBusinessType_ID == 1) iProvider_ID = Convert.ToInt32(fgSummary[fgSummary.Row, "Provider_ID"]);
                    else iProvider_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);

                    klsServiceProviders.Record_ID = iProvider_ID;
                    klsServiceProviders.Product_ID = iProduct_ID;
                    klsServiceProviders.ProductCategory_ID = iProductCategory_ID;
                    klsServiceProviders.GetRecord_Executions_Settlement();
                    iCustodyProvider_ID = klsServiceProviders.CustodyProvider_ID;

                    //--- add new Bulk or Execution Command - depend on iChoiceBusinessType_ID ----------------------------------
                    klsOrder = new clsOrdersSecurity();
                    klsOrder.BulkCommand = "<" + iBulcCommand_ID + ">";
                    klsOrder.BusinessType_ID = 2;

                    if (iPressAction == 2) klsOrder.CommandType_ID = 2;                                        // 2 - Execution
                    else                   klsOrder.CommandType_ID = 3;                                        // 3 - Bulk 

                    klsOrder.Client_ID = 0;
                    klsOrder.Company_ID = Global.Company_ID;
                    klsOrder.ServiceProvider_ID = Global.Company_ID;
                    klsOrder.StockExchange_ID = Convert.ToInt32(cmbStockExchange.SelectedValue);              
                    klsOrder.ServiceProvider_ID = iProvider_ID;
                    klsOrder.CustodyProvider_ID = iCustodyProvider_ID;
                    klsOrder.Depository_ID = 0;
                    klsOrder.II_ID = 0;
                    klsOrder.Parent_ID = 0;
                    klsOrder.Contract_ID = iNewClientPackage_ID;
                    klsOrder.Code = sNewCode;
                    klsOrder.ProfitCenter = sNewPortfolio;
                    klsOrder.AllocationPercent = 100;                                                           
                    klsOrder.Aktion = (lblAction.Text == "BUY" ? 1 : 2);
                    klsOrder.AktionDate = DateTime.Now;
                    klsOrder.Share_ID = iChoiceShare_ID;
                    klsOrder.Product_ID = iChoiceProduct_ID;
                    klsOrder.ProductCategory_ID = iChoiceProductCategory_ID;
                    klsOrder.Curr = fgSimpleCommands[1, "Currency"] + "";
                    if (lstType.SelectedIndex == 0) {
                        klsOrder.PriceType = 0;
                        klsOrder.Price = Convert.ToDecimal(txtPrice.Text);
                    }
                    else {
                        klsOrder.PriceType = lstType.SelectedIndex;
                        klsOrder.Price = 0;
                    }
                    klsOrder.Quantity = Convert.ToDecimal(lblQuantity.Text);
                    klsOrder.Amount = Convert.ToDecimal(lblAmount.Text);
                    klsOrder.Constant = cmbConstant.SelectedIndex;
                    klsOrder.ConstantDate = (cmbConstant.SelectedIndex == 2 ? dConstant.Value.ToString("dd/MM/yyyy") : "");
                    klsOrder.RecieveDate = DateTime.Now;
                    klsOrder.RecieveMethod_ID = 0;
                    klsOrder.BestExecution = chkBestExecution.Checked ? 1 : 0;
                    klsOrder.SentDate = Convert.ToDateTime("1900/01/01");
                    klsOrder.FIX_A = -1;
                    klsOrder.Notes = "";
                    klsOrder.User_ID = Global.User_ID;
                    klsOrder.DateIns = DateTime.Now;
                    klsOrder.Status = 0;
                    iID = klsOrder.InsertRecord();                  

                    frmOrderExecution locOrderExecution = new frmOrderExecution();
                    if (iChoiceBusinessType_ID == 2) {                               //open  Execution order                    
                        locOrderExecution.Rec_ID = iID;
                        locOrderExecution.CommandType_ID = 2;                        //fgSimpleCommands[fgSimpleCommands.Row, 14)                       ' 2 - Execution Order
                        locOrderExecution.RightsLevel = 2;
                        locOrderExecution.Editable = 1;
                        locOrderExecution.ShowDialog();
                    }
                    else {                                                           // open Bulk Order
                        locOrderExecution.Rec_ID = iID;
                        locOrderExecution.CommandType_ID = 3;                        //fgSimpleCommands[fgSimpleCommands.Row, 14)                        ' 3 - Bulk Order
                        locOrderExecution.RightsLevel = 2;
                        locOrderExecution.Editable = 1;
                        locOrderExecution.ShowDialog();
                    }
                }
                iLastAktion = 1;
                this.Close();
            }
        }
        private void DefineProviderData()
        {
            if (bCheckList) {
                clsContracts klsContract = new clsContracts();
                klsContract.PackageType = 2;
                klsContract.ServiceProvider_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                klsContract.DateStart = Convert.ToDateTime("1900/01/01");
                klsContract.DateFinish = Convert.ToDateTime("2071/12/31");
                klsContract.GetList_Provider_ID();
                foreach (DataRow dtRow in klsContract.List.Rows) {
                    //if (Convert.ToInt32(dtRow["BrokerageServiceProvider_ID"]) == Convert.ToInt32(cmbServiceProviders.SelectedValue)) {
                        sNewCode = dtRow["Code"] + "";
                        sNewPortfolio = dtRow["Portfolio"] + "";
                        iNewClientPackage_ID = Convert.ToInt32(dtRow["ID"]);
                        iPackageProvider_ID = Convert.ToInt32(dtRow["ServiceProvider_ID"]);
                        iBestExecution = Convert.ToInt32(dtRow["BestExecution"]);
                    //}
                }
            }
        }
        private void DefineServiceProvidersList()
        {
            if (iChoiceProvider_ID == 9) {                       // 9 - HellasFin
                dtList = new DataTable();
                dtList.Columns.Add("ID", typeof(int));
                dtList.Columns.Add("Title", typeof(string));

                if (iChoiceProduct_ID == 6) {                   // 6 - AK    
                    dtRow = dtList.NewRow();
                    dtRow["ID"] = 18;
                    dtRow["Title"] = "MFEX";
                    dtList.Rows.Add(dtRow);

                    dtRow = dtList.NewRow();
                    dtRow["ID"] = 17;
                    dtRow["Title"] = "PIRAEUS SECURITIES";
                    dtList.Rows.Add(dtRow);

                    dtRow = dtList.NewRow();
                    dtRow["ID"] = 14;
                    dtRow["Title"] = "BNP PARIBAS";
                    dtList.Rows.Add(dtRow);
                }
                else  {
                    dtRow = dtList.NewRow();
                    dtRow["ID"] = 19;
                    dtRow["Title"] = "INTESA SANPAOLO S.p.A";
                    dtList.Rows.Add(dtRow);

                    dtRow = dtList.NewRow();
                    dtRow["ID"] = 17;
                    dtRow["Title"] = "PIRAEUS SECURITIES";
                    dtList.Rows.Add(dtRow);

                    dtRow = dtList.NewRow();
                    dtRow["ID"] = 20;
                    dtRow["Title"] = "BNP ARBITRAGE";
                    dtList.Rows.Add(dtRow);
                }
                bCheckList = false;
                cmbServiceProviders.DataSource = dtList;
                cmbServiceProviders.DisplayMember = "Title";
                cmbServiceProviders.ValueMember = "ID";
                bCheckList = true;
                cmbServiceProviders.SelectedIndex = 0;
            }
            else  {                                             // 2 - ΠΕΙΡΑΙΩΣ Α.Ε.Π.Ε.Υ.   // 7 - CREDIT SUISSE // 12 - Rothschild (Luxemburg)                                 
                bCheckList = false;
                dtView = Global.dtServiceProviders.Copy().DefaultView;
                dtView.RowFilter = "Aktive = 1";
                cmbServiceProviders.DataSource = dtView;
                cmbServiceProviders.DisplayMember = "Title";
                cmbServiceProviders.ValueMember = "ID";
                bCheckList = true;
                cmbServiceProviders.SelectedValue = iChoiceProvider_ID;
            }
        }
        private void picCopy2Clipboard_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Convert.IsDBNull(Clipboard.GetText())) Clipboard.SetDataObject(lblProductISIN.Text + "", true, 10, 100);
                //Clipboard.SetText(txtISIN.Text + "");
            }
            catch (Exception)
            {
            }
        }
        private void CalcAmountEUR()
        {
            lblAmount_EUR.Text = Global.ConvertAmount(Convert.ToDecimal(lblAmount.Text), lblAmountCurr.Text, "EUR", DateTime.Now).ToString("0.00"); 
            switch (Convert.ToInt32(fgSummary[fgSummary.Row, "Product_ID"])) {
                case 1:             // Equities
                case 4:             // ETF
                    if (Convert.ToDecimal(lblAmount_EUR.Text) >= 100000) lblAmount_EUR.BackColor = Color.Red;
                    else lblAmount_EUR.BackColor = Color.Transparent;
                    break;
                case 2:              // Bond
                    if (Convert.ToDecimal(lblAmount_EUR.Text) >= 500000) lblAmount_EUR.BackColor = Color.Red;
                    else lblAmount_EUR.BackColor = Color.Transparent;
                    break;
                case 6:               // Fund   
                    if (Convert.ToDecimal(lblAmount_EUR.Text) >= 300000) lblAmount_EUR.BackColor = Color.Red;
                    else lblAmount_EUR.BackColor = Color.Transparent;
                    break;
            }
            lblEUR.Text = "EUR";
        }
        private void Empty_Selection()
        {
            iChoiceBusinessType_ID = 0;
            iChoiceProvider_ID = 0;
            iChoiceShare_ID = 0;
            lblProductTitle.Text = "";
            lblProductCode.Text = "";
            lblProductISIN.Text = "";
            lblServiceProvider.Text = "";
            cmbServiceProviders.SelectedValue = 0;
            lblAction.Text = "";
            lstType.SelectedIndex = 0;
            txtPrice.Text = "";
            lblQuantity.Text = "";
            lblAmount.Text = "";
            lblAmountCurr.Text = "";
            lblAmount_EUR.Text = "";
            lblAmount_EUR.BackColor = Color.Transparent;
            lblEUR.Text = "";
            cmbConstant.SelectedIndex = 0;
            dConstant.Value = Convert.ToDateTime("1900/01/01");
            dConstant.Visible = false;
        }
        public DateTime Today { get { return dToday; } set { dToday = value; } }
        public int LastAktion { get { return iLastAktion; } set { iLastAktion = value; } }
    }
}
