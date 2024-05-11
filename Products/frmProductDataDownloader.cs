using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;
using System.Drawing;
using System.ComponentModel;
using EikonDesktopDataAPI;
using Dex2;
using C1.Win.C1FlexGrid;
using Core;

namespace Products
{
    public partial class frmProductDataDownloader : Form
    {
        SqlConnection cn2;
        SqlCommand comm2;
        //SqlClient.SqlParameter prmSQL;
        SqlDataReader drList, drList1;
        DataTable dtReutersFields;
        DataColumn dtCol;
        DataRow dtRow;
        DataRow[] foundRows;
        DataView dtView;
        DataRowView dtViewRow;

        int i, j, iMode, iProduct_ID, iShares, iBonds, iETFs, iFunds, iRates, iIndexes, iGroup_ID, iRows, iSharesCols, iBondsCols, iETFsCols, iFundsCols,
            iRatesCols, iIndexesCols, iBondType, iFrequencyClipping, iCouponeType, iRevocationRight, iRank, iComplexReason_ID, iCountry_ID,
            iCountryRisk_ID, iSector_ID, iSE_ID, iPrimaryShare, iCountriesGroup_ID, iShare_ID, iShareTitle_ID, iShareCode_ID, iCountryAction_ID,
            iProductCategory_ID, iRightsLevel;
        string sTemp, sTemp2, sExtra, sSQL, sID, sCode, sCodes, sFields, sError, sParams, sSharesID, sShares, sBondsID, sBonds, sETFsID, sETFs, sFundsID, sFunds,
               sRatesID, sRates, sIndexesID, sIndexes, sDate2, sMoodysRating, sMoodysRatingDate, sSPRating, sSPRatingDate, sFitchsRating, sFitchsRatingDate,
               sCallDate, sDenominationType, sFloatingRate, sFRNFormula, sMonthDays, sBaseDays, sProviderName, sCurrency, sRiskCurrency, sDescriptionEn,
               sDateIncorporation, sMarketCapitalization, sMarketCapitalizationCurr, sMemberIndex, sInstrumentType;
        string[] tmpArray, tmpBrray;
        float sgQuantityMin, sgTemp, sgKoef, sgCoupone, sgClosePrice, sgQuantityStep, sgPrice, sgLimit, sgLastCoupone;
        decimal decAmountOutstanding;
        DateTime dIPO, dFrom, dTo, dClosePriceDate, dYesterday;
        bool bInitialize, bCheckList, bFound;
        CellStyle csError;

        clsProducts klsProducts = new clsProducts();
        clsProductsCodes klsProductsCodes = new clsProductsCodes();
        clsProductsTitles klsProductsTitles = new clsProductsTitles();
        clsProductsTitlesCodes klsProductsTitlesCodes = new clsProductsTitlesCodes();
        clsProductsTitles klsProductTitle_ComplexReasons = new clsProductsTitles();
        clsProductsPrices klsProductsPrices = new clsProductsPrices();
        clsSystem Systems = new clsSystem();
        public frmProductDataDownloader()
        {
            InitializeComponent();
            // Add any initialization after the InitializeComponent() call.
            UpdateUserFormAccordingToConnectionStatus(EEikonStatus.Offline);
        }

        private void frmProductDataDownloader_Load(object sender, EventArgs e)
        {
            dBondPrices.Value = DateTime.Now.AddDays(-1);

            bInitialize = false;
            bCheckList = false;
            panMain.Visible = false;
            panData.Visible = false;
            iMode = 0;                  //0 -unknown,  1 - from Database, 2 - from Trader Eikon

            panReuters.Top = 4;
            panReuters.Left = 4;

            panDatabase.Top = 4;
            panDatabase.Left = 4;

            panFields.Top = 150;
            panFields.Left = 300;

            panProductsSearch.Top = 54;
            panProductsSearch.Left = 148;

            //------- fgShares ----------------------------
            fgShares.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgShares.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgShares.DrawMode = DrawModeEnum.OwnerDraw;

            csError = fgShares.Styles.Add("Cancelled");
            csError.BackColor = Color.LightCoral;

            //------- fgBonds ----------------------------
            fgBonds.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgBonds.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgBonds.DrawMode = DrawModeEnum.OwnerDraw;

            csError = fgBonds.Styles.Add("Cancelled");
            csError.BackColor = Color.LightCoral;

            //------- fgETFs ----------------------------
            fgETFs.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgETFs.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgETFs.DrawMode = DrawModeEnum.OwnerDraw;

            csError = fgETFs.Styles.Add("Cancelled");
            csError.BackColor = Color.LightCoral;

            //------- fgFunds ----------------------------
            fgFunds.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgFunds.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgFunds.DrawMode = DrawModeEnum.OwnerDraw;

            csError = fgFunds.Styles.Add("Cancelled");
            csError.BackColor = Color.LightCoral;

            //------- fgRates ----------------------------
            fgRates.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgRates.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgRates.DrawMode = DrawModeEnum.OwnerDraw;

            csError = fgRates.Styles.Add("Cancelled");
            csError.BackColor = Color.LightCoral;

            //------- fgIndexes ----------------------------
            fgIndexes.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgIndexes.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgIndexes.DrawMode = DrawModeEnum.OwnerDraw;

            csError = fgIndexes.Styles.Add("Cancelled");
            csError.BackColor = Color.LightCoral;

            cn2 = new SqlConnection(Global.connStr + "ExternalData");

            bCheckList = true;
        }
        protected override void OnResize(EventArgs e)
        {
            panMain.Width = this.Width - 30;
            panMain.Height = this.Height - 96;

            panData.Width = this.Width - 40;
            panData.Height = this.Height - 200;

            tbProducts.Width = panData.Width - 6;
            tbProducts.Height = panData.Height - 50;

            fgShares.Width = tbProducts.Width - 6;
            fgShares.Height = tbProducts.Height - 28;

            fgBonds.Width = tbProducts.Width - 6;
            fgBonds.Height = tbProducts.Height - 28;

            fgETFs.Width = tbProducts.Width - 6;
            fgETFs.Height = tbProducts.Height - 28;

            fgFunds.Width = tbProducts.Width - 6;
            fgFunds.Height = tbProducts.Height - 28;

            fgRates.Width = tbProducts.Width - 6;
            fgRates.Height = tbProducts.Height - 28;

            fgIndexes.Width = tbProducts.Width - 6;
            fgIndexes.Height = tbProducts.Height - 28;
        }
        private void dDatabase_ValueChanged(object sender, EventArgs e)
        {
            cmbNums.Items.Clear();

            try
            {
                cn2.Open();
                comm2 = new SqlCommand("GetDownloadsList", cn2);
                comm2.CommandType = CommandType.StoredProcedure;
                comm2.Parameters.Add(new SqlParameter("@Today", dDatabase.Value));
                drList = comm2.ExecuteReader();
                while (drList.Read())
                {
                    cmbNums.Items.Add(drList["ID"] + "");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { cn2.Close(); }

            if (cmbNums.Items.Count > 0) {
                cmbNums.SelectedIndex = 0;
                btnGet.Enabled = true;
            }
            else btnGet.Enabled = false;
        }
        private void btnDatabase_Click(object sender, EventArgs e)
        {
            StartInit();

            iMode = 1;   // 1 - from Database, 2 - from Trader Eikon
            dDatabase.Value = DateTime.Now.Date;

            panReuters.Visible = false;
            panDatabase.Visible = true;
            panData.Visible = false;
            panMain.Visible = true;
        }
        private void btnGet_Click(object sender, EventArgs e)
        {
            string sTraderRIC;

            lblStartTime.Text = DateTime.Now.ToString("s");

            this.Cursor = Cursors.WaitCursor;
            this.Refresh();

            iShares = 0;
            iBonds = 0;
            iETFs = 0;
            iFunds = 0;
            iRates = 0;
            iIndexes = 0;

            fgShares.Redraw = false;
            fgShares.Rows.Count = 1;
            fgShares[0, 0] = "AA";
            fgShares[0, 1] = "Trader RIC";
            fgShares[0, 2] = "RIC";
            fgShares[0, 3] = "ISIN";
            fgShares[0, 4] = "ClosePrice Date";
            fgShares[0, 5] = "ClosePrice";
            fgShares[0, 6] = "ClosePrice Currency";
            fgShares[0, 7] = "ID";
            fgShares[0, 8] = "Validation Flag";

            fgBonds.Redraw = false;
            fgBonds.Rows.Count = 1;
            fgBonds[0, 0] = "AA";
            fgBonds[0, 1] = "Trader RIC";
            fgBonds[0, 2] = "RIC";
            fgBonds[0, 3] = "ISIN";
            fgBonds[0, 4] = "ClosePrice Date";
            fgBonds[0, 5] = "ClosePrice";
            fgBonds[0, 6] = "ClosePrice Currency";
            fgBonds[0, 7] = "MoodysRating";
            fgBonds[0, 8] = "MoodysRating Date";
            fgBonds[0, 9] = "SPRating";
            fgBonds[0, 10] = "SPRating Date";
            fgBonds[0, 11] = "FitchsRating";
            fgBonds[0, 12] = "FitchsRating Date";
            fgBonds[0, 13] = "Active";
            fgBonds[0, 14] = "ID";
            fgBonds[0, 15] = "Validation Flag";

            fgETFs.Redraw = false;
            fgETFs.Rows.Count = 1;
            fgETFs[0, 0] = "AA";
            fgETFs[0, 1] = "Trader RIC";
            fgETFs[0, 2] = "RIC";
            fgETFs[0, 3] = "ISIN";
            fgETFs[0, 4] = "ClosePrice Date";
            fgETFs[0, 5] = "ClosePrice";
            fgETFs[0, 6] = "ClosePrice Currency";
            fgETFs[0, 7] = "ID";
            fgETFs[0, 8] = "Validation Flag";

            fgFunds.Redraw = false;
            fgFunds.Rows.Count = 1;
            fgFunds[0, 0] = "AA";
            fgFunds[0, 1] = "Trader RIC";
            fgFunds[0, 2] = "RIC";
            fgFunds[0, 3] = "ISIN";
            fgFunds[0, 4] = "ClosePrice Date";
            fgFunds[0, 5] = "ClosePrice";
            fgFunds[0, 6] = "ClosePrice Currency";
            fgFunds[0, 7] = "ID";
            fgFunds[0, 8] = "Validation Flag";

            fgRates.Redraw = false;
            fgRates.Rows.Count = 1;
            fgRates[0, 0] = "AA";
            fgRates[0, 1] = "Trader RIC";
            fgRates[0, 2] = "RIC";
            fgRates[0, 3] = "ClosePrice Date";
            fgRates[0, 4] = "ClosePrice";
            fgRates[0, 5] = "ID";
            fgRates[0, 6] = "Validation Flag";

            fgIndexes.Redraw = false;
            fgIndexes.Rows.Count = 1;
            fgIndexes[0, 0] = "AA";
            fgIndexes[0, 1] = "Trader RIC";
            fgIndexes[0, 2] = "RIC";
            fgIndexes[0, 3] = "ClosePrice Date";
            fgIndexes[0, 4] = "ClosePrice";
            fgIndexes[0, 5] = "ClosePrice Currency";
            fgIndexes[0, 6] = "ID";
            fgIndexes[0, 7] = "Validation Flag";

            panData.Visible = true;

            cn2.Open();
            comm2 = new SqlCommand("GetTable", cn2);
            comm2.CommandType = CommandType.StoredProcedure;
            comm2.Parameters.Add(new SqlParameter("@Table", "ReutersPrices_Recs"));
            comm2.Parameters.Add(new SqlParameter("@Col", "RPT_ID"));
            comm2.Parameters.Add(new SqlParameter("@Value", cmbNums.Text));
            comm2.Parameters.Add(new SqlParameter("@Order", "ID"));
            drList = comm2.ExecuteReader();
            while (drList.Read())
            {
                iProduct_ID = 0;
                sTraderRIC = "";
                foundRows = Global.dtProducts.Select("ID = " + drList["ShareCodes_ID"]);
                if (foundRows.Length > 0) {
                    iProduct_ID = Convert.ToInt32(foundRows[0]["Product_ID"]);
                    sTraderRIC = foundRows[0]["Code"] + "";
                }

                switch (iProduct_ID) {
                    case 1:
                        iShares = iShares + 1;
                        sTemp = iShares + "\t" + sTraderRIC + "\t" + drList["RIC"] + "\t" + drList["ISIN"] + "\t" + drList["ClosePriceDate"] + "\t" +
                            drList["ClosePrice"] + "\t" + drList["ClosePriceCurrency"] + "\t" + drList["ShareCodes_ID"] + "\t" + drList["ValidationFlag"];
                        AddGridLine(iProduct_ID, i, sTemp);
                        break;
                    case 2:
                        iBonds = iBonds + 1;
                        sTemp = iBonds + "\t" + sTraderRIC + "\t" + drList["RIC"] + "\t" + drList["ISIN"] + "\t" + drList["ClosePriceDate"] + "\t" +
                            drList["ClosePrice"] + "\t" + drList["ClosePriceCurrency"] + "\t" + drList["MoodysRating"] + "\t" + drList["MoodysRatingDate"] + "\t" +
                            drList["SPRating"] + "\t" + drList["SPRatingDate"] + "\t" + drList["FitchsRating"] + "\t" + drList["FitchsRatingDate"] + "\t" +
                            drList["Aktive"] + "\t" + drList["ShareCodes_ID"] + "\t" + drList["ValidationFlag"];
                        AddGridLine(iProduct_ID, i, sTemp);
                        break;
                    case 3:
                        iRates = iRates + 1;
                        sTemp = iRates + "\t" + sTraderRIC + "\t" + drList["RIC"] + "\t" + drList["ClosePriceDate"] + "\t" +
                            drList["ClosePrice"] + "\t" + drList["ShareCodes_ID"] + "\t" + drList["ValidationFlag"];
                        AddGridLine(iProduct_ID, i, sTemp);
                        break;
                    case 4:
                        iETFs = iETFs + 1;
                        sTemp = iETFs + "\t" + sTraderRIC + "\t" + drList["RIC"] + "\t" + drList["ISIN"] + "\t" + drList["ClosePriceDate"] + "\t" +
                            drList["ClosePrice"] + "\t" + drList["ClosePriceCurrency"] + "\t" + drList["ShareCodes_ID"] + "\t" + drList["ValidationFlag"];
                        AddGridLine(iProduct_ID, i, sTemp);
                        break;
                    case 5:
                        iIndexes = iIndexes + 1;
                        sTemp = iIndexes + "\t" + sTraderRIC + "\t" + drList["RIC"] + "\t" + drList["ClosePriceDate"] + "\t" +
                            drList["ClosePrice"] + "\t" + drList["ClosePriceCurrency"] + "\t" + drList["ShareCodes_ID"] + "\t" + drList["ValidationFlag"];
                        AddGridLine(iProduct_ID, i, sTemp);
                        break;
                    case 6:
                        iFunds = iFunds + 1;
                        sTemp = iFunds + "\t" + sTraderRIC + "\t" + drList["RIC"] + "\t" + drList["ISIN"] + "\t" + drList["ClosePriceDate"] + "\t" +
                            drList["ClosePrice"] + "\t" + drList["ClosePriceCurrency"] + "\t" + drList["ShareCodes_ID"] + "\t" + drList["ValidationFlag"];
                        AddGridLine(iProduct_ID, i, sTemp);
                        break;
                }
            }
            drList.Close();
            cn2.Close();

            fgShares.Redraw = true;
            fgBonds.Redraw = true;
            fgETFs.Redraw = true;
            fgFunds.Redraw = true;
            fgRates.Redraw = true;
            fgIndexes.Redraw = true;

            lblFinishTime.Text = DateTime.Now.ToString("s");
            this.Cursor = Cursors.Default;
            this.Refresh();
        }        
        private void AddGridLine(int iProd_ID, int i, string sLine)
        {
            switch (iProd_ID)
            {
                case 1:
                    lblSharesRows.Text = i.ToString();
                    lblSharesRows.Refresh();
                    fgShares.AddItem(sLine);
                    break;
                case 2:
                lblBondsRows.Text = i.ToString();
                    lblBondsRows.Refresh();
                    fgBonds.AddItem(sLine);
                    break;
                case 3:
                    lblRatesRows.Text = i.ToString();
                    lblRatesRows.Refresh();
                    fgRates.AddItem(sLine);
                    break;
                case 4:
                    lblETFsRows.Text = i.ToString();
                    lblETFsRows.Refresh();
                    fgETFs.AddItem(sLine);
                    break;
                case 5:
                    lblIndexesRows.Text = i.ToString();
                    lblIndexesRows.Refresh();
                    fgIndexes.AddItem(sLine);
                    break;
                case 6:
                    lblFundsRows.Text = i.ToString();
                    lblFundsRows.Refresh();
                    fgFunds.AddItem(sLine);
                    break;
            }
        }
        private void btnEikon_Click(object sender, EventArgs e)
        {
            if (!bInitialize) ConnectToEikon();

            StartInit();

            iMode = 2;                                                      // 1 - from Database, 2 - from Trader Eikon
            cmbDataType.SelectedIndex = 0;
            ucDC.DateFrom = DateTime.Now.AddDays(-1).Date;
            ucDC.DateTo = DateTime.Now.AddDays(-1).Date;
            ucDC.Visible = false;

            chkShares.Checked = true;
            chkBonds.Checked = true;
            chkETFs.Checked = true;
            chkFunds.Checked = true;
            chkRates.Checked = true;
            chkIndexes.Checked = true;

            iShares = 0;
            sSharesID = "";
            sShares = "";
            iSharesCols = 0;
            iBonds = 0;
            sBondsID = "";
            sBonds = "";
            iBondsCols = 0;
            iETFs = 0;
            sETFsID = "";
            sETFs = "";
            iETFsCols = 0;
            iFunds = 0;
            sFundsID = "";
            sFunds = "";
            iFundsCols = 0;
            iRates = 0;
            sRatesID = "";
            sRates = "";
            iRatesCols = 0;
            iIndexes = 0;
            sIndexesID = "";
            sIndexes = "";
            iIndexesCols = 0;

            DefineReutersFields();

            dtView = Global.dtProducts.DefaultView;
            dtView.RowFilter = "Aktive > 0";
            foreach (DataRowView dtViewRow in dtView) {
                switch (Convert.ToInt32(dtViewRow["Product_ID"])) {
                    case 1:
                        iShares = iShares + 1;
                        sSharesID = sSharesID + dtViewRow["ID"] + ";";
                        sShares = sShares + dtViewRow["Code"] + ";";
                        break;
                    case 2:
                        iBonds = iBonds + 1;
                        sBondsID = sBondsID + dtViewRow["ID"] + ";";
                        sBonds = sBonds + dtViewRow["ISIN"] + ";";
                        break;
                    case 3:
                        iRates = iRates + 1;
                        sRatesID = sRatesID + dtViewRow["ID"] + ";";
                        sRates = sRates + dtViewRow["Code"] + ";";
                        break;
                    case 4:
                        iETFs = iETFs + 1;
                        sETFsID = sETFsID + dtViewRow["ID"] + ";";
                        sETFs = sETFs + dtViewRow["Code"] + ";";
                        break;
                    case 5:
                        iIndexes = iIndexes + 1;
                        sIndexesID = sIndexesID + dtViewRow["ID"] + ";";
                        sIndexes = sIndexes + dtViewRow["Code"] + ";";
                        break;
                    case 6:
                        iFunds = iFunds + 1;
                        sFundsID = sFundsID + dtViewRow["ID"] + ";";
                        sFunds = sFunds + dtViewRow["Code"] + ";";
                        break;
                }
            }


            lblSharesList.Text = iShares.ToString();
            lblSharesRows.Text = "";
            lblSharesTime.Text = "";

            lblBondsList.Text = iBonds.ToString();
            lblBondsRows.Text = "";
            lblBondsTime.Text = "";

            lblETFsList.Text = iETFs.ToString();
            lblETFsRows.Text = "";
            lblETFsTime.Text = "";

            lblFundsList.Text = iFunds.ToString();
            lblFundsRows.Text = "";
            lblFundsTime.Text = "";

            lblRatesList.Text = iRates.ToString();
            lblRatesRows.Text = "";
            lblRatesTime.Text = "";

            lblIndexesList.Text = iIndexes.ToString();
            lblIndexesRows.Text = "";
            lblIndexesTime.Text = "";

            panReuters.Visible = true;
            panDatabase.Visible = false;
            panData.Visible = false;
            panMain.Visible = true;
        }
        
        private void ConnectToEikon()
        {

        }
        private void StartInit()
        {
            fgShares.Rows.Count = 1;
            fgBonds.Rows.Count = 1;
            fgETFs.Rows.Count = 1;
            fgFunds.Rows.Count = 1;
            fgRates.Rows.Count = 1;
            fgIndexes.Rows.Count = 1;
        }
        private void cmbDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDataType.SelectedIndex == 1) ucDC.Visible = true;
            else ucDC.Visible = false;
        }
        private void picIndexesFields_Click(object sender, EventArgs e)
        {
            switch (Convert.ToInt32(cmbDataType.SelectedIndex))
            {
                case 0:             // Indexes Prices
                    iGroup_ID = 51;
                    break;
                case 1:             // Indexes Historical Prices
                    iGroup_ID = 51;
                    break;
                case 2:             // Indexes Data      
                    iGroup_ID = 5;
                    break;
            }

            fgFields.Redraw = false;
            fgFields.Rows.Count = 1;
            dtView = dtReutersFields.DefaultView;
            dtView.RowFilter = "Group_ID = " + iGroup_ID;
            foreach (DataRowView dtViewRow in dtView)
                fgFields.AddItem(dtViewRow["Title"] + "\t" + dtViewRow["TR_Code"] + "\t" + dtViewRow["ID"]);

            fgFields.Redraw = true;
            panFields.Visible = true;
        }

        private void picIndexesEdit_Click(object sender, EventArgs e)
        {
            dtView = Global.dtProducts.DefaultView;
            dtView.RowFilter = "Aktive > 0 AND Product_ID = 5";
            foreach (DataRowView dtViewRow in dtView)
                fgProductsList.AddItem(false + "\t" + dtViewRow["Title"] + "\t" + dtViewRow["Code"]);

            panProductsSearch.Visible = true;
        }

        private void picRatesFields_Click(object sender, EventArgs e)
        {
            switch (Convert.ToInt32(cmbDataType.SelectedIndex))
            {
                case 0:             // Rates Prices
                    iGroup_ID = 31;
                    break;
                case 1:             // Rates Historical Prices
                    iGroup_ID = 31;
                    break;
                case 2:             // Rates Data      
                    iGroup_ID = 3;
                    break;
            }

            fgFields.Redraw = false;
            fgFields.Rows.Count = 1;
            dtView = dtReutersFields.DefaultView;
            dtView.RowFilter = "Group_ID = " + iGroup_ID;
            foreach (DataRowView dtViewRow in dtView)
                fgFields.AddItem(dtViewRow["Title"] + "\t" + dtViewRow["TR_Code"] + "\t" + dtViewRow["ID"]);

            fgFields.Redraw = true;
            panFields.Visible = true;
        }

        private void picRatesEdit_Click(object sender, EventArgs e)
        {
            dtView = Global.dtProducts.DefaultView;
            dtView.RowFilter = "Aktive > 0 AND Product_ID = 3";
            foreach (DataRowView dtViewRow in dtView)
                fgProductsList.AddItem(false + "\t" + dtViewRow["Title"] + "\t" + dtViewRow["Code"]);

            panProductsSearch.Visible = true;
        }

        private void picFundsFields_Click(object sender, EventArgs e)
        {
            switch (Convert.ToInt32(cmbDataType.SelectedIndex))
            {
                case 0:             // Funds Prices
                    iGroup_ID = 61;
                    break;
                case 1:             // Funds Historical Prices
                    iGroup_ID = 61;
                    break;
                case 2:             // Funds Data      
                    iGroup_ID = 6;
                    break;
            }

            fgFields.Redraw = false;
            fgFields.Rows.Count = 1;
            dtView = dtReutersFields.DefaultView;
            dtView.RowFilter = "Group_ID = " + iGroup_ID;
            foreach (DataRowView dtViewRow in dtView)
                fgFields.AddItem(dtViewRow["Title"] + "\t" + dtViewRow["TR_Code"] + "\t" + dtViewRow["ID"]);

            fgFields.Redraw = true;
            panFields.Visible = true;
        }

        private void picFundsEdit_Click(object sender, EventArgs e)
        {
            dtView = Global.dtProducts.DefaultView;
            dtView.RowFilter = "Aktive > 0 AND Product_ID = 6";
            foreach (DataRowView dtViewRow in dtView)
                fgProductsList.AddItem(false + "\t" + dtViewRow["Title"] + "\t" + dtViewRow["Code"]);

            panProductsSearch.Visible = true;
        }

        private void picETFsFields_Click(object sender, EventArgs e)
        {
            switch (Convert.ToInt32(cmbDataType.SelectedIndex))
            {
                case 0:             // ETFs Prices
                    iGroup_ID = 41;
                    break;
                case 1:             // ETFs Historical Prices
                    iGroup_ID = 41;
                    break;
                case 2:             // ETFs Data      
                    iGroup_ID = 4;
                    break;
            }

            fgFields.Redraw = false;
            fgFields.Rows.Count = 1;
            dtView = dtReutersFields.DefaultView;
            dtView.RowFilter = "Group_ID = " + iGroup_ID;
            foreach (DataRowView dtViewRow in dtView)
                fgFields.AddItem(dtViewRow["Title"] + "\t" + dtViewRow["TR_Code"] + "\t" + dtViewRow["ID"]);

            fgFields.Redraw = true;
            panFields.Visible = true;
        }

        private void picETFsEdit_Click(object sender, EventArgs e)
        {
            dtView = Global.dtProducts.DefaultView;
            dtView.RowFilter = "Aktive > 0 AND Product_ID = 4";
            foreach (DataRowView dtViewRow in dtView)
                fgProductsList.AddItem(false + "\t" + dtViewRow["Title"] + "\t" + dtViewRow["Code"]);

            panProductsSearch.Visible = true;
        }

        private void picBondsFields_Click(object sender, EventArgs e)
        {
            switch (Convert.ToInt32(cmbDataType.SelectedIndex))
            {
                case 0:             // Bonds Prices
                    iGroup_ID = 21;
                    break;
                case 1:             // Bonds Historical Prices
                    iGroup_ID = 21;
                    break;
                case 2:             // Bonds Data      
                    iGroup_ID = 2;
                    break;
            }

            fgFields.Redraw = false;
            fgFields.Rows.Count = 1;
            dtView = dtReutersFields.DefaultView;
            dtView.RowFilter = "Group_ID = " + iGroup_ID;
            foreach (DataRowView dtViewRow in dtView)
                fgFields.AddItem(dtViewRow["Title"] + "\t" + dtViewRow["TR_Code"] + "\t" + dtViewRow["ID"]);

            fgFields.Redraw = true;
            panFields.Visible = true;
        }

        private void picBondsEdit_Click(object sender, EventArgs e)
        {
            dtView = Global.dtProducts.DefaultView;
            dtView.RowFilter = "Aktive > 0 AND Product_ID = 2";
            foreach (DataRowView dtViewRow in dtView)
                fgProductsList.AddItem(false + "\t" + dtViewRow["Title"] + "\t" + dtViewRow["Code"]);

            panProductsSearch.Visible = true;
        }

        private void picSharesFields_Click(object sender, EventArgs e)
        {
            switch (Convert.ToInt32(cmbDataType.SelectedIndex))
            {
                case 0:             // Shares Prices
                    iGroup_ID = 11;
                    break;
                case 1:             // Shares Historical Prices
                    iGroup_ID = 11;
                    break;
                case 2:             // Shares Data      
                    iGroup_ID = 1;
                    break;
            }

            fgFields.Redraw = false;
            fgFields.Rows.Count = 1;
            dtView = dtReutersFields.DefaultView;
            dtView.RowFilter = "Group_ID = " + iGroup_ID;
            foreach (DataRowView dtViewRow in dtView)
                fgFields.AddItem(dtViewRow["Title"] + "\t" + dtViewRow["TR_Code"] + "\t" + dtViewRow["ID"]);

            fgFields.Redraw = true;
            panFields.Visible = true;
        }

        private void picSharesEdit_Click(object sender, EventArgs e)
        {
            dtView = Global.dtProducts.DefaultView;
            dtView.RowFilter = "Aktive > 0 AND Product_ID = 1";
            foreach (DataRowView dtViewRow in dtView)
                fgProductsList.AddItem(false + "\t" + dtViewRow["Title"] + "\t" + dtViewRow["Code"]);

            panProductsSearch.Visible = true;
        }
        private void tsbMainExcel_Click(object sender, EventArgs e)
        {

        }

        private void tsbMainSave_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            fgMessages.Rows.Count = 1;
            fgCancelled.Rows.Count = 1;
            fgNoPrices.Rows.Count = 1;

            if (iMode == 1) {                                           // 1 - from Database, 2 - from Trader Eikon
                dYesterday = Convert.ToDateTime("1900/01/01");
                cmbDataType.SelectedIndex = 0;

                SavePrices(1, fgShares);
                SavePrices(2, fgBonds);
                SavePrices(4, fgETFs);
                SavePrices(6, fgFunds);
                SavePrices(3, fgRates);
                SavePrices(5, fgIndexes);

                dNoPrices.Value = dYesterday;
            }
            else {
                switch (cmbDataType.SelectedIndex) {
                    case 0:                                                 // 0 - Prices, 1 - Historical Prices
                    case 1:
                        if (chkShares.Checked) SavePrices(1, fgShares);
                        if (chkBonds.Checked) SavePrices(2, fgBonds);
                        if (chkETFs.Checked) SavePrices(4, fgETFs);
                        if (chkFunds.Checked) SavePrices(6, fgFunds);
                        if (chkRates.Checked) SavePrices(3, fgRates);
                        if (chkIndexes.Checked) SavePrices(5, fgIndexes);
                        break;
                    case 2:
                        if (chkShares.Checked) {
                            for (i = 1; i <= fgShares.Rows.Count - 1; i++) {
                                sTemp = fgShares[i, 4] + "";
                                sProviderName = sTemp.Replace("'", "`");
                                iCountry_ID = DefineItemID("Countries", "Code", fgShares[i, 5] + "", false, "");
                                iCountryRisk_ID = DefineItemID("Countries", "Code", fgShares[i, 5] + "", false, "");
                                iSector_ID = DefineItemID("Sectors", "Title", fgShares[i, 9] + "", false, " AND Sectors.L1 = 1");
                                iSE_ID = DefineItemID("StockExchanges", "Code", fgShares[i, 13] + "", false, "");
                                sCurrency = DefineCurrency("Currencies", "Title", fgShares[i, 14] + "");
                                sRiskCurrency = DefineCurrency("Currencies", "Title", fgShares[i, 15] + "");
                                iPrimaryShare = (fgShares[i, 16]+"" == "1" ? 2 : (fgShares[i, 16] + "" == "0" ? 1 : 0));
                                dIPO = (Global.IsDate(fgShares[i, 17]+"")? Convert.ToDateTime(fgShares[i, 17]+"") : Convert.ToDateTime("1900/01/01"));
                                sTemp = fgShares[i, 18] + "";
                                sDescriptionEn = sTemp.Replace("'", "`") + "";
                                sgQuantityMin = (Global.IsNumeric(fgShares[i, 19]+"") ? Convert.ToSingle(fgShares[i, 19]+"") : -1);
                                sDateIncorporation = (fgShares[i, 20] + "").Trim();
                                sMarketCapitalization = (fgShares[i, 21] + "").Trim();
                                sMarketCapitalizationCurr = (fgShares[i, 22] + "").Trim();
                                sMemberIndex = (fgShares[i, 23] + "").Trim();
                                sInstrumentType = (fgShares[i, 24] + "").Trim();
                                dClosePriceDate = Convert.ToDateTime("1900/01/01");
                                sTemp = (Global.IsDate(fgShares[i, 25]+"") ? fgShares[i, 25]+"" : "");
                                if (sTemp.Length > 0) dClosePriceDate = Convert.ToDateTime(sTemp);

                                sgClosePrice = (Global.IsNumeric(fgShares[i, 26]+"") ? Convert.ToSingle(fgShares[i, 26]+"") : -1);
                                iShareCode_ID = Convert.ToInt32(fgShares[i, 27]);

                                iCountriesGroup_ID = 0;
                                foundRows = Global.dtCountries.Select("ID = " + iCountry_ID);
                                if (foundRows.Length > 0) iCountriesGroup_ID = Convert.ToInt32(foundRows[0]["CountriesGroup_ID"]);
                                foundRows = Global.dtProducts.Select("Code = '" + fgShares[i, 1] + "' AND Aktive >=1 ");
                                if (foundRows.Length > 0) {
                                    if (fgShares[i, 3] == foundRows[0]["ISIN"]) {
                                        if (Convert.ToInt32(foundRows[0]["Product_ID"]) == 1) {
                                            iShare_ID = Convert.ToInt32(foundRows[0]["Shares_ID"]);
                                            iShareTitle_ID = Convert.ToInt32(foundRows[0]["ShareTitles_ID"]);
                                            iShareCode_ID = Convert.ToInt32(foundRows[0]["ID"]);

                                            //--- edit  ShareTitles table -----------------
                                            klsProductsTitles = new clsProductsTitles();
                                            klsProductsTitles.Record_ID = iShareTitle_ID;
                                            klsProductsTitles.GetRecord();

                                            if (sProviderName.Length > 0) klsProductsTitles.ProviderName = sProviderName;

                                            sTemp = (fgShares[i, 11] + "").Trim();
                                            if (sTemp.Length > 0) klsProductsTitles.ProductTitle = sTemp;

                                            sTemp = (fgShares[i, 3] + "").Trim();
                                            if (sTemp.Length > 0) klsProductsTitles.ISIN = sTemp;

                                            if (iCountry_ID != 0) klsProductsTitles.Country_ID = iCountry_ID;

                                            if (iSector_ID != 0) klsProductsTitles.Sector_ID = iSector_ID;

                                            if (iCountryRisk_ID != 0) klsProductsTitles.CountryRisk_ID = iCountryRisk_ID;

                                            if (iCountriesGroup_ID != 0) klsProductsTitles.CountryGroup_ID = iCountriesGroup_ID;

                                            sTemp = (fgShares[i, 10] + "").Trim();
                                            if (sTemp.Length > 0) klsProductsTitles.URL = sTemp;

                                            sTemp = sRiskCurrency.Trim();
                                            if (sTemp.Length > 0) klsProductsTitles.RiskCurr = sTemp;

                                            sTemp = sDescriptionEn.Trim();
                                            if (sTemp.Length > 0) klsProductsTitles.DescriptionEn = sTemp;

                                            sTemp = (sDateIncorporation).Trim();
                                            if (sTemp.Length > 0) klsProductsTitles.DateIncorporation = sTemp;

                                            if (Global.IsNumeric(sMarketCapitalization)) {
                                                klsProductsTitles.MarketCapitalization = Convert.ToDecimal(sMarketCapitalization);
                                                klsProductsTitles.MarketCapitalizationCurr = sMarketCapitalizationCurr;
                                            }

                                            if (sMemberIndex.Length > 0)                                               klsProductsTitles.DateIncorporation = sMemberIndex;
                                        if (sInstrumentType.Length > 0)                                                    klsProductsTitles.DateIncorporation = sInstrumentType;

                                            klsProductsTitles.LastEditDate = DateTime.Now;
                                            klsProductsTitles.LastEditUser_ID = Global.User_ID;
                                            klsProductsTitles.EditRecord();


                                        //--- edit  ShareCodes table -----------------
                                            klsProductsCodes = new clsProductsCodes();
                                            klsProductsCodes.Record_ID = iShareCode_ID;
                                            klsProductsCodes.GetRecord();

                                            sCode = klsProductsCodes.Code;


                                            sTemp = (fgShares[i, 11] + "").Trim();
                                            if (sTemp.Length > 0) klsProductsCodes.CodeTitle = sTemp;

                                            sTemp = (fgShares[i, 2] + "").Trim();
                                            if (sTemp.Length > 0) klsProductsCodes.ISIN = sTemp;


                                        sTemp = (fgShares[i, 12] + "").Trim();
                                            if (sTemp.Length > 0) klsProductsCodes.Code3 = sTemp;

                                            if (iSE_ID != 0)
                                                klsProductsCodes.StockExchange_ID = iSE_ID;


                                            if (iCountryAction_ID != 0)
                                                klsProductsCodes.CountryAction = iCountryAction_ID;


                                            sTemp = sCurrency.Trim();
                                            if (sTemp.Length > 0) klsProductsCodes.Curr = sTemp;


                                            klsProductsCodes.QuantityMin = sgQuantityMin;
                                            klsProductsCodes.PrimaryShare = iPrimaryShare;
                                            klsProductsCodes.DateIPO = dIPO;
                                            klsProductsCodes.Aktive = 1;
                                            klsProductsCodes.InfoFlag = 1;
                                            klsProductsCodes.EditRecord();

                                            SavePrice(1, sCode, iShareCode_ID, dClosePriceDate, sgClosePrice);
                                        }
                                        else fgMessages.AddItem("Step 3 >>> Reuters Code = '" + dtRow["f1"] + "     ISIN = '" + dtRow["f3"] + "'.    Δεν είναι μετοχή");
                                    }
                                    else fgMessages.AddItem("Step 3 >>> Reuters Code = '" + dtRow["f1"] + "     Wrong ISIN = '" + dtRow["f3"]);
                                }
                                else fgMessages.AddItem("Step 3 >>> Reuters Code = '" + dtRow["f1"] + "      Unknown Reuters Code");

                                if (chkBonds.Checked) {
                                    for (i = 1; i <= fgBonds.Rows.Count - 1; i++)
                                    {
                                        sTemp = fgBonds[i, 4] + "";
                                        sProviderName = sTemp.Replace("'", "`");
                                        iCountry_ID = DefineItemID("Countries", "Code", fgBonds[i, 5] + "", false, "");
                                        iCountryRisk_ID = DefineItemID("Countries", "Code", fgBonds[i, 6] + "", false, "");
                                        iSector_ID = DefineItemID("Sectors", "Title", fgBonds[i, 8] + "", false, " AND Sectors.L1 = 1");
                                        switch (fgBonds[i, 9] + "")
                                        {
                                            case "CORP":
                                                iBondType = 1;             // 1 - corporate
                                                break;
                                            case "SOVR":
                                                iBondType = 2;             // 2 - goverment
                                                break;
                                            case "SUPR":
                                                iBondType = 3;             // 3 - yperethniko
                                                break;
                                            default:
                                                iBondType = 0;
                                                break;
                                        }
                                        sTemp = fgBonds[i, 10] + "";
                                        sDescriptionEn = sTemp.Replace("'", "`") + "";

                                        sgCoupone = (Global.IsNumeric(fgBonds[i, 12] + "") ? Convert.ToSingle(fgBonds[i, 12]+"") : 0);
                                        sCurrency = DefineCurrency("Currencies", "Title", fgBonds[i, 13] + "");
                                        sgQuantityMin = (Global.IsNumeric(fgBonds[i, 15] + "") ? Convert.ToSingle(fgBonds[i, 15] + "") : -1);
                                        sgQuantityStep = (Global.IsNumeric(fgBonds[i, 16] + "") ? Convert.ToSingle(fgBonds[i, 16] + "") : -1);
                                        // fgBonds[i, 17)  - not use for bonds
                                        //fgBonds[i, 18)  - not use for bonds

                                        sMoodysRating = "";
                                        sMoodysRatingDate = "";
                                        foundRows = Global.dtRatingCodes.Select("RatingAgency_ID = 1 AND Code = '" + fgBonds[i, 19] + "'");
                                        if (foundRows.Length > 0) {
                                            sMoodysRating = fgBonds[i, 19] + "";
                                            sMoodysRatingDate = fgBonds[i, 20] + "";
                                        }

                                        sFitchsRating = "";
                                        sFitchsRatingDate = "";
                                        foundRows = Global.dtRatingCodes.Select("RatingAgency_ID = 2 AND Code = '" + fgBonds[i, 23] + "'");
                                        if (foundRows.Length > 0) {
                                            sFitchsRating = fgBonds[i, 23] + "";
                                            sFitchsRatingDate = fgBonds[i, 24] + "";
                                        }

                                        sSPRating = "";
                                        sSPRatingDate = "";
                                        foundRows = Global.dtRatingCodes.Select("RatingAgency_ID = 3 AND Code = '" + fgBonds[i, 21] + "'");
                                        if (foundRows.Length > 0) {
                                            sSPRating = fgBonds[i, 21] + "";
                                            sSPRatingDate = fgBonds[i, 22] + "";
                                        }

                                        decAmountOutstanding = (Global.IsNumeric(fgBonds[i, 25] + "") ? Convert.ToDecimal(fgBonds[i, 25]) : -1);
                                        iFrequencyClipping = (Global.IsNumeric(fgBonds[i, 26] + "") ? Convert.ToInt32(fgBonds[i, 26]) : 0);
                                        sCallDate = "";
                                        if (fgBonds[i, 27] + "" != "NULL") sCallDate = fgBonds[i, 27] + "";
                                        sDenominationType = fgBonds[i, 28] + "";
                                                                                
                                        iCouponeType = 0;
                                        foundRows = Global.dtCouponeTypes.Select("Title = '" + fgBonds[i, 40] + "'");
                                        if (foundRows.Length > 0)
                                            iCouponeType = Convert.ToInt32(foundRows[0]["ID"]);

                                        sFloatingRate = "";
                                        if (fgBonds[i, 41]+"" != "NULL") sFloatingRate = fgBonds[i, 41] + "";

                                        sgPrice = (Global.IsNumeric(fgBonds[i, 42] + "") ? Convert.ToSingle(fgBonds[i, 42]) : -1);

                                        iRevocationRight = 0;
                                        foundRows = Global.dtRevocationRights.Select("Title = '" + fgBonds[i, 43] + "'");
                                        if (foundRows.Length > 0)
                                            iRevocationRight = Convert.ToInt32(foundRows[0]["ID"]);

                                        iRank = 0;
                                        foundRows = Global.dtRanks.Select("Title = '" + fgBonds[i, 44] + "'");
                                        if (foundRows.Length > 0)
                                            iRank = Convert.ToInt32(foundRows[0]["ID"]);                                        

                                        sFRNFormula = "";
                                        if (fgBonds[i, 45]+"" != "NULL") sFRNFormula = fgBonds[i, 45] + "";

                                        sgLimit = (Global.IsNumeric(fgBonds[i, 46] + "") ? Convert.ToSingle(fgBonds[i, 46]) : -1);

                                        sMonthDays = "";
                                        sBaseDays = "";
                                        sTemp = fgBonds[i, 47] + "";
                                        j = sTemp.IndexOf("/");
                                        if (j >= 0) {
                                            sMonthDays = sTemp.Substring(1, i - 1) + "";
                                            sBaseDays = sTemp.Substring(i + 1) + "";
                                        }
                                        else {
                                            sMonthDays = sTemp;
                                            sBaseDays = "";
                                        }

                                        sgLastCoupone = (Global.IsNumeric(fgBonds[i, 48] + "") ? Convert.ToSingle(fgBonds[i, 48]) : -1);

                                        dClosePriceDate = Convert.ToDateTime("1900/01/01");
                                        sTemp = (Global.IsDate(fgBonds[i, 51]+"") ? fgBonds[i, 51] +"" : "");
                                        if (sTemp.Length > 0) dClosePriceDate = Convert.ToDateTime(sTemp);

                                        sgClosePrice = (Global.IsNumeric(fgBonds[i, 52]+"") ? Convert.ToSingle(fgBonds[i, 52]+"") : -1);

                                        foundRows = Global.dtProducts.Select("Code = '" + fgBonds[i, 2] + "' AND Aktive >= 1 ");
                                        if (foundRows.Length > 0) {
                                            if (fgBonds[i, 3] == foundRows[0]["ISIN"]) {
                                                if ((Convert.ToInt32(foundRows[0]["Product_ID"]+"") == 2)) {
                                                    iShare_ID = Convert.ToInt32(foundRows[0]["Shares_ID"]);
                                                    iShareTitle_ID = Convert.ToInt32(foundRows[0]["ShareTitles_ID"]);
                                                    iShareCode_ID = Convert.ToInt32(foundRows[0]["ID"]);

                                                    //--- edit  ShareTitles table -----------------
                                                    klsProductsTitles = new clsProductsTitles();
                                                    klsProductsTitles.Record_ID = iShareTitle_ID;
                                                    klsProductsTitles.GetRecord();

                                                    if (sProviderName.Length > 0) klsProductsTitles.ProviderName = sProviderName;

                                                    sTemp = (fgBonds[i, 3] + "").Trim();
                                                    if (sTemp.Length > 0) klsProductsTitles.ISIN = sTemp;

                                                    klsProductsTitles.BondType = iBondType;

                                                    if (sDescriptionEn.Length > 0) klsProductsTitles.DescriptionEn = sDescriptionEn;

                                                    if (iCountry_ID != 0) klsProductsTitles.Country_ID = iCountry_ID;

                                                    if (iSector_ID != 0) klsProductsTitles.Sector_ID = iSector_ID;

                                                    if (iCountryRisk_ID != 0) klsProductsTitles.CountryRisk_ID = iCountryRisk_ID;

                                                    if (iCountriesGroup_ID != 0) klsProductsTitles.CountryGroup_ID = iCountriesGroup_ID;

                                                    sTemp = (fgBonds[i, 11] + "").Trim();
                                                    if (sTemp.Length > 0) klsProductsTitles.URL = sTemp;

                                                    if (sMoodysRating.Length > 0) klsProductsTitles.MoodysRating = sMoodysRating;

                                                    if (Global.IsDate(sMoodysRatingDate))
                                                        klsProductsTitles.MoodysRatingDate = Convert.ToDateTime(sMoodysRatingDate);

                                                    if (sSPRating.Length > 0) klsProductsTitles.SPRating = sSPRating;

                                                    if (Global.IsDate(sSPRatingDate)) klsProductsTitles.SPRatingDate = Convert.ToDateTime(sSPRatingDate);


                                                    if (sFitchsRating.Length > 0) klsProductsTitles.FitchsRating = sFitchsRating;

                                                    if (Global.IsDate(sFitchsRatingDate)) klsProductsTitles.FitchsRatingDate = Convert.ToDateTime(sFitchsRatingDate);

                                                    klsProductsTitles.RatingGroup = Global.DefineRatingGroup(klsProductsTitles.MoodysRating, klsProductsTitles.FitchsRating, klsProductsTitles.SPRating, klsProductsTitles.ICAPRating, "");

                                                    if (decAmountOutstanding > 0) klsProductsTitles.AmountOutstanding = decAmountOutstanding;

                                                    if (sCallDate.Length > 0) klsProductsTitles.CallDate = sCallDate;
                                                    if (sDenominationType.Length > 0) klsProductsTitles.DenominationType = sDenominationType;

                                                    sTemp = (fgBonds[i, 29] + "" == "Y" ? "1" : (fgBonds[i, 29] + "" == "N" ? "0" : ""));
                                                    if (sTemp.Length > 0) klsProductsTitles.IsConvertible = Convert.ToInt16(sTemp);

                                                    sTemp = (fgBonds[i, 30] + "" == "Y" ? "1" : (fgBonds[i, 30] + "" == "N" ? "0" : ""));
                                                    if (sTemp.Length > 0) klsProductsTitles.IsDualCurrency = Convert.ToInt16(sTemp);

                                                    sTemp = (fgBonds[i, 31] + "" == "Y" ? "1" : (fgBonds[i, 31] + "" == "N" ? "0" : ""));
                                                    if (sTemp.Length > 0) klsProductsTitles.IsHybrid = Convert.ToInt16(sTemp);

                                                    sTemp = (fgBonds[i, 32] + "" == "Y" ? "1" : (fgBonds[i, 32] + "" == "N" ? "0" : ""));
                                                    if (sTemp.Length > 0) klsProductsTitles.IsGuaranteed = Convert.ToInt16(sTemp);

                                                    sTemp = (fgBonds[i, 33] + "" == "Y" ? "1" : (fgBonds[i, 33] + "" == "N" ? "0" : ""));
                                                    if (sTemp.Length > 0) klsProductsTitles.IsPerpetualSecurity = Convert.ToInt16(sTemp);

                                                    sTemp = (fgBonds[i, 34] + "" == "Y" ? "1" : (fgBonds[i, 34] + "" == "N" ? "0" : ""));
                                                    if (sTemp.Length > 0) klsProductsTitles.IsTotalLoss = Convert.ToInt16(sTemp);

                                                    sTemp = (fgBonds[i, 35] + "" == "Y" ? "1" : (fgBonds[i, 35] + "" == "N" ? "0" : ""));
                                                    if (sTemp.Length > 0) klsProductsTitles.MinimumTotalLoss = sTemp;

                                                    sTemp = (fgBonds[i, 36] + "" == "Y" ? "1" : (fgBonds[i, 36] + "" == "N" ? "0" : ""));
                                                    if (sTemp.Length > 0) klsProductsTitles.IsProspectusAvailable = Convert.ToInt16(sTemp);

                                                    if (iRank != 0)
                                                        klsProductsTitles.Rank = iRank;

                                                    sTemp = (fgBonds[i, 49] + "" == "Y" ? "1" : (fgBonds[i, 49] + "" == "N" ? "0" : ""));
                                                    if (sTemp.Length > 0) klsProductsTitles.IsCallable = Convert.ToInt16(sTemp);

                                                    sTemp = (fgBonds[i, 50] + "" == "Y" ? "1" : (fgBonds[i, 50] + "" == "N" ? "0" : ""));
                                                    if (sTemp.Length > 0) klsProductsTitles.IsPutable = Convert.ToInt16(sTemp);

                                                    klsProductsTitles.LastEditDate = DateTime.Now;
                                                    klsProductsTitles.LastEditUser_ID = Global.User_ID;
                                                    klsProductsTitles.EditRecord();

                                                    //--- edit  ShareCodes table -----------------
                                                    klsProductsCodes = new clsProductsCodes();
                                                    klsProductsCodes.Record_ID = iShareCode_ID;
                                                    klsProductsCodes.GetRecord();

                                                    sCode = klsProductsCodes.Code;

                                                    if (sgCoupone != 0) klsProductsCodes.Coupone = sgCoupone;

                                                    sTemp = sCurrency.Trim();
                                                    if (sTemp.Length > 0) klsProductsCodes.Curr = sTemp;

                                                    sTemp = (Global.IsDate(fgBonds[i, 14] + "") ? fgBonds[i, 14] + "" : "");
                                                    if (sTemp.Length > 0) klsProductsCodes.Date2 = Convert.ToDateTime(sTemp);

                                                    if (sgQuantityMin != 0)
                                                        klsProductsCodes.QuantityMin = sgQuantityMin;

                                                    if (sgQuantityStep != 0)
                                                        klsProductsCodes.QuantityStep = sgQuantityStep;

                                                    if (iFrequencyClipping != 0) klsProductsCodes.FrequencyClipping = iFrequencyClipping;

                                                    sTemp = (Global.IsDate(fgBonds[i, 37] + "") ? fgBonds[i, 37] + "" : "");
                                                    if (sTemp.Length > 0) klsProductsCodes.Date1 = Convert.ToDateTime(sTemp);

                                                    sTemp = (Global.IsDate(fgBonds[i, 38] + "") ? fgBonds[i, 38] + "" : "");
                                                    if (sTemp.Length > 0) klsProductsCodes.Date3 = Convert.ToDateTime(sTemp);

                                                    sTemp = (Global.IsDate(fgBonds[i, 39] + "") ? fgBonds[i, 39] + "" : "");
                                                    if (sTemp.Length > 0) klsProductsCodes.Date4 = Convert.ToDateTime(sTemp);

                                                    if (iCouponeType != 0) klsProductsCodes.CouponeType = iCouponeType;


                                                    sTemp = sFloatingRate.Trim();
                                                    if (sTemp.Length > 0) klsProductsCodes.FloatingRate = sTemp;

                                                    if (sgPrice > 0) klsProductsCodes.Price = sgPrice;

                                                    if (iRevocationRight != 0) klsProductsCodes.RevocationRight = iRevocationRight;


                                                    sTemp = sFRNFormula.Trim();
                                                    if (sTemp.Length > 0) klsProductsCodes.FRNFormula = sTemp;

                                                    if (sgLimit > 0) klsProductsCodes.Limits = sgLimit;

                                                    sTemp = sMonthDays.Trim();
                                                    if (sTemp.Length > 0) klsProductsCodes.MonthDays = sTemp;

                                                    sTemp = sBaseDays.Trim();
                                                    if (sTemp.Length > 0) klsProductsCodes.BaseDays = sTemp;

                                                    if (sgLastCoupone > 0) klsProductsCodes.LastCoupone = sgLastCoupone;

                                                    klsProductsCodes.Aktive = 1;
                                                    klsProductsCodes.InfoFlag = 1;
                                                    klsProductsCodes.EditRecord();

                                                    /*
                                              SavePrice(2, sCode, iShareCode_ID, dClosePriceDate, sgClosePrice);

                                              iComplexReason_ID = DefineItemID("ComplexReasons", "Title", fgBonds[i, 56] + "", false, "");

       if (iComplexReason_ID != 0) {
           bFound = false;
           klsProductTitle_ComplexReasons = new clsProductsTitles();
           klsProductTitle_ComplexReasons.Record_ID = iShareTitle_ID;
           klsProductTitle_ComplexReasons.GetComplexReasons_List();
           foreach (DataRow dtRow1 in klsProductTitle_ComplexReasons.ComplexReasons.Rows)
               if (dtRow1["ComplexReason_ID"] == iComplexReason_ID) bFound = true;

           if (!bFound) {
               cn.Open()
                   With comm
       .Connection = cn
       .CommandText = "InsertShareTitle_ComplexReason"
       .CommandType = CommandType.StoredProcedure
                   End With
                   comm.Parameters.Clear()
                   prmSQL = comm.Parameters.AddWithValue("@ID", Nothing)
                   prmSQL.Direction = ParameterDirection.Output
                   prmSQL.SqlDbType = SqlDbType.Int
                   prmSQL = comm.Parameters.AddWithValue("@ShareTitles_ID", iShareTitle_ID)
                   prmSQL = comm.Parameters.AddWithValue("@ComplexReason_ID", iComplexReason_ID)
                   comm.ExecuteNonQuery()
                   cn.Close()

          }
}
*/
                                                }
                                            }
                                            else
                                                fgMessages.AddItem("Step 4 >>> Reuters Code = '" + dtRow["f1"] + "     ISIN = '" + dtRow["f3"] + "'.    Δεν είναι μετοχή");
                                        }
                                        else
                                            fgMessages.AddItem("Step 4 >>> Reuters Code = '" + dtRow["f1"] + "     Wrong ISIN = '" + dtRow["f3"]);
                                    }
                                }
                                else
                                    fgMessages.AddItem("Step 4 >>> Reuters Code = '" + dtRow["f1"] + "      Unknown Reuters Code");
                            }
                        }
                        break;
                }
            }
            fgMessages.Redraw = true;
            fgCancelled.Redraw = true;
            fgNoPrices.Redraw = true;

            this.Cursor = Cursors.Default;

            panMessages.Top = 172;
            panMessages.Left = 238;
            panMessages.Visible = true;
        }
        private void SavePrice(int iShareType, string sCode, int iShareCode_ID, DateTime dClosePrice, float sgClosePrice)
        {
            if (sgClosePrice > 0)
            {
                klsProductsCodes = new clsProductsCodes();
                klsProductsCodes.DateFrom = dClosePrice;
                klsProductsCodes.DateTo = dClosePrice;
                klsProductsCodes.Product_ID = 0;
                klsProductsCodes.ISIN = "";
                klsProductsCodes.Code = sCode;
                klsProductsCodes.GetPricesList();
                if (klsProductsCodes.List.Rows.Count > 0)
                {
                    foreach (DataRow dtRow in klsProductsCodes.List.Rows)
                    {
                        klsProductsPrices = new clsProductsPrices();
                        klsProductsPrices.Record_ID = Convert.ToInt32(dtRow["ID"] + "");
                        klsProductsPrices.GetRecord();
                        klsProductsPrices.Open = 0;
                        klsProductsPrices.High = 0;
                        klsProductsPrices.Low = 0;
                        klsProductsPrices.Close = sgClosePrice;
                        klsProductsPrices.Last = -999999;
                        klsProductsPrices.Volume = 0;
                        klsProductsPrices.EditRecord();
                    }
                }
                else
                {
                    klsProductsPrices = new clsProductsPrices();
                    klsProductsPrices.ShareType = iShareType;
                    klsProductsPrices.Code = sCode;
                    klsProductsPrices.ShareCodes_ID = iShareCode_ID;
                    klsProductsPrices.DateIns = dClosePrice;
                    klsProductsPrices.Open = 0;
                    klsProductsPrices.High = 0;
                    klsProductsPrices.Low = 0;
                    klsProductsPrices.Close = sgClosePrice;
                    klsProductsPrices.Last = -999999;
                    klsProductsPrices.Volume = 0;
                    klsProductsPrices.InsertRecord();
                }
            }
        }       
        private void SavePrices(int iProduct_ID, C1FlexGrid tmpFgList)
        {

        }
        private void DefineReutersFields()
        {

        }
        private int DefineItemID(string sTableName, string sField, string sItem, bool bAutoAdd, string sExtra)
        {

            int iItem = 0;

            sItem = sItem + "";
            try
            {
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
                if (sItem != "")
                {
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
        private string DefineCurrency(string sTableName, string sField, string sItem)
        {
            string sFind = "";
            if (sItem != "")
            {

                foundRows = Global.dtCurrencies.Select(sField + " = '" + sItem + "'");
                if (foundRows.Length > 0) sFind = foundRows[0]["Code_Convert"] + "";
            }
            return sFind;
        }
        private void AddLogRec(string sMessage)
        {
            //iLogs = iLogs + 1;
            //fgWarnings.AddItem(iLogs + "\t" + sMessage);
        }
        //================================================================================================
        private void UpdateUserFormAccordingToConnectionStatus(EikonDesktopDataAPI.EEikonStatus EStatus) {

            switch (EStatus) {
                case EikonDesktopDataAPI.EEikonStatus.Connected:

                case EikonDesktopDataAPI.EEikonStatus.Disconnected:
                    DisconnectFromEikon();
                    break;

                case EikonDesktopDataAPI.EEikonStatus.LocalMode:
                    ReleaseDex2Mgr();
                    break;
                case EikonDesktopDataAPI.EEikonStatus.Offline:
                    DisconnectFromEikon();
                    break;
            }
        }
        private void DisconnectFromEikon() {

        }
        private void ReleaseDex2Mgr() {

        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}

