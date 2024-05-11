using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using C1.Win.C1FlexGrid;
using Core;

namespace Custody
{    public partial class frmExecutionFiles : Form

    {
        DataTable dtList, dtCompare, dtChildOrders, dtDepositories_Alias;
        DataView dtView;
        DataColumn dtCol;
        DataRow dtRow;
        DataRow[] foundRows;
        int i, j, iRow, iOdd, iStockExchange_ID, iDepository_ID, iCommand_Executions_ID, iRightsLevel, iMaxQuantity_Command_ID;
        decimal decMaxQuantity;
        float fltTemp = 0;
        string sTemp, sTradeTime, sEffectCode, sExtra, sDepository_Code;
        bool bCheckList, bEmptyClientOrderID, bCheckCompare;
        DateTime dTemp, dTradeDate, dSettlementDate;
        CellStyle csExported, csOdd, csFinish, csDiff;
        clsServiceProviders ServiceProviders = new clsServiceProviders();
        clsCustodyCommands CustodyCommands = new clsCustodyCommands();
        clsDepositories Depositories = new clsDepositories();
        clsOrdersSecurity Orders = new clsOrdersSecurity();
        clsOrdersSecurity Orders2 = new clsOrdersSecurity();   
        clsOrdersSecurity Orders4 = new clsOrdersSecurity();
        clsOrders_ProvidersRecs Orders_ProvidersRecs = new clsOrders_ProvidersRecs();
        public frmExecutionFiles()
        {
            InitializeComponent();

            panWarnings.Left = (Screen.PrimaryScreen.Bounds.Width - panWarnings.Width) / 2;
            panWarnings.Top = (Screen.PrimaryScreen.Bounds.Height - panWarnings.Height) / 2;

            panEdit.Left = (Screen.PrimaryScreen.Bounds.Width - panEdit.Width) / 2;
            panEdit.Top = (Screen.PrimaryScreen.Bounds.Height - panEdit.Height) / 2;
        }
        private void frmExecutionFiles_Load(object sender, EventArgs e)
        {
            bCheckList = false;
            bCheckCompare = false;
            toolLeft.Visible = false;

            btnSearch.Enabled = false;
            dAktionDate.Value = DateTime.Now.AddDays(-1);

            csExported = fgProvider.Styles.Add("Exported");
            csExported.BackColor = Color.LightGreen;

            csOdd = fgCompare.Styles.Add("Odd");
            csOdd.BackColor = Color.LightGray;

            csFinish = fgCompare.Styles.Add("Finish");
            csFinish.BackColor = Color.LightGreen;            

            csDiff = fgProvider.Styles.Add("Buy");
            csDiff.BackColor = Color.LightCoral;

            dtView = Global.dtServiceProviders.Copy().DefaultView;
            dtView.RowFilter = "ProviderType = 0 OR ProviderType = 3";
            cmbServiceProviders.DataSource = dtView;
            cmbServiceProviders.DisplayMember = "Title";
            cmbServiceProviders.ValueMember = "ID";

            cmbProducts.DataSource = Global.dtProductTypes.Copy();
            cmbProducts.DisplayMember = "Title";
            cmbProducts.ValueMember = "ID";
            cmbProducts.SelectedValue = 0;

            //-------------- Define MiFID InstrumentType --------------------
            clsSystem System = new clsSystem();
            System.GetDepositoriesAlias();
            dtDepositories_Alias = System.List;

            //------- fgProvider ----------------------------
            fgProvider.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgProvider.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgProvider.OwnerDrawCell += fgProvider_OwnerDrawCell;
            fgProvider.DrawMode = DrawModeEnum.OwnerDraw;

            //------- fgCompare ----------------------------
            fgCompare.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            fgCompare.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgCompare.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgCompare_BeforeEdit);
            fgCompare.AfterEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgCompare_AfterEdit);
            fgCompare.CellChanged += fgCompare_CellChanged;
            fgCompare.OwnerDrawCell += fgCompare_OwnerDrawCell;
            fgCompare.RowColChange += new EventHandler(fgCompare_RowColChange);
            fgCompare.DrawMode = DrawModeEnum.OwnerDraw;

            //------- fgProvider2 ----------------------------
            fgProvider2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgProvider2.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgProvider2.DoubleClick += new System.EventHandler(fgProvider2_DoubleClick);

            //------- fgList2 ----------------------------
            fgList2.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList2.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgList2.DoubleClick += new System.EventHandler(fgList2_DoubleClick);

            //------- fgFinish ----------------------------
            fgFinish.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgFinish.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt; BackColor:LightBlue; ForeColor:Black;}");

            if (iRightsLevel == 2) {
                toolLeft.Enabled = true;
                toolLeft2.Enabled = true;
            }
            else {
                toolLeft.Enabled = false;
                toolLeft2.Enabled = false;
            }
            bCheckList = true;
        }
        protected override void OnResize(EventArgs e)
        {
            panCritiries.Width = this.Width - 30;
            btnSearch.Left = this.Width - 150;

            tabMain.Width = this.Width - 30;
            tabMain.Height = this.Height - 128;

            fgProvider.Width = tabMain.Width - 12;
            fgProvider.Height = tabMain.Height - 64;

            fgList.Width = tabMain.Width - 36;
            fgList.Height = tabMain.Height - 88;

            fgCompare.Height = tabMain.Height - 64;

            fgProvider2.Width = tabMain.Width - fgCompare.Width - 24;
            fgProvider2.Height = (tabMain.Height - 64) / 2;

            lblCompanyRecords.Top = fgProvider2.Top + fgProvider2.Height + 8;
            fgList2.Top = lblCompanyRecords.Top + 20;
            fgList2.Width = tabMain.Width - fgCompare.Width - 24;
            fgList2.Height = (tabMain.Height - 64) / 2 - 32;

            fgFinish.Width = tabMain.Width - 12;
            fgFinish.Height = tabMain.Height - 64;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        { 
            DefineList();
            tabMain.SelectedIndex = 0;
            DefineFinishedRecords();
        }
        private void DefineList()
        {
            fgProvider.Rows.Count = 1;
            fgList.Rows.Count = 2;
            fgCompare.Rows.Count = 1;

            switch (Convert.ToInt32(cmbServiceProviders.SelectedValue))  {
                case 14:                                                                  // 14 - BMP Paribas
                    if (Convert.ToInt32(cmbProducts.SelectedValue) == 6)                  // AK
                        sEffectCode = "5";
                    break;

                case 16:                                                                  // 16 -  SocGen
                    if (Convert.ToInt32(cmbProducts.SelectedValue) == 6)                  // AK
                        sEffectCode = "5";
                    break;
                case 17:                                                                  // 17 - Pireaus
                    sEffectCode = "68";

                    fgProvider.Cols["Code"].Visible = true;
                    fgProvider.Cols["Market"].Visible = false;
                    fgProvider.Cols["PlaceOfSettlement"].Visible = false;                    
                    fgProvider.Cols["MarketFee"].Visible = true;
                    fgProvider.Cols["Taxes"].Visible = true;
                    fgProvider.Cols["ExchangeRate"].Visible = false;
                    break;
                case 19:                                                                  // 19 - INTESA
                    sEffectCode = "81";

                    fgProvider.Cols["Code"].Visible = false;
                    fgProvider.Cols["Market"].Visible = true;
                    fgProvider.Cols["PlaceOfSettlement"].Visible = true;
                    fgProvider.Cols["MarketFee"].Visible = false;
                    fgProvider.Cols["Taxes"].Visible = false;
                    fgProvider.Cols["ExchangeRate"].Visible = true;
                    break;
                case 20:                                                                  // 20 - BNP ARBITRAGE
                    sEffectCode = "85";

                    fgProvider.Cols["Code"].Visible = false;
                    fgProvider.Cols["Market"].Visible = true;
                    fgProvider.Cols["PlaceOfSettlement"].Visible = true;
                    fgProvider.Cols["MarketFee"].Visible = false;
                    fgProvider.Cols["Taxes"].Visible = false;
                    fgProvider.Cols["ExchangeRate"].Visible = true;
                    break;
            }

            if (Convert.ToInt32(cmbServiceProviders.SelectedValue) != 0)  {

                //--- define CustodyCommands list -----------------------------------------------------
                CustodyCommands = new clsCustodyCommands();
                CustodyCommands.ServiceProvider_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                CustodyCommands.DateFrom = dAktionDate.Value;
                CustodyCommands.DateTo = dAktionDate.Value;
                CustodyCommands.GetList();

                i = 0;
                fgProvider.Redraw = false;
                fgProvider.Rows.Count = 1;
                foreach (DataRow dtRow in CustodyCommands.List.Rows) {
                    i = i + 1;
                    fgProvider.AddItem(i + "\t" + Convert.ToDateTime(dtRow["TradeDate"]).ToString("yyyy/MM/dd") + "\t" + dtRow["TradeTime"] + "\t" +
                                       Convert.ToDateTime(dtRow["SettlementDate"]).ToString("yyyy/MM/dd") + "\t" + dtRow["TradeCurrency"] + "\t" +
                                       dtRow["ISIN"] + "\t" + dtRow["SecurityCode"] + "\t" + dtRow["SecurityDescription"] + "\t" + dtRow["Market"] + "\t" + dtRow["Sign"] + "\t" + 
                                       Convert.ToDecimal(dtRow["QuantityNominal"]).ToString("0.######") + "\t" + Convert.ToDecimal(dtRow["Price"]).ToString("0.######") + "\t" +
                                       Convert.ToDecimal(dtRow["AccruedInterest"]).ToString("0.######") + "\t" + Convert.ToDecimal(dtRow["Commission"]).ToString("0.######") + "\t" +
                                       Convert.ToDecimal(dtRow["Fees"]).ToString("0.######") + "\t" + Convert.ToDecimal(dtRow["MarketFee"]).ToString("0.######") + "\t" +
                                       Convert.ToDecimal(dtRow["Taxes"]).ToString("0.######") + "\t" + Math.Abs(Convert.ToDecimal(dtRow["SettlementAmount"])).ToString("0.######") + "\t" +
                                       Convert.ToDecimal(dtRow["ExchangeRate"]).ToString("0.######") + "\t" + Math.Abs(Convert.ToDecimal(dtRow["SettlementAmountCurr"])).ToString("0.######") + "\t" + 
                                       dtRow["SettlementPlace"] + "\t" + dtRow["RefNumber"] + "\t" + dtRow["MIC_Code"] + "\t" + dtRow["PSET"] + "\t" + dtRow["ID"] + "\t" +
                                       dtRow["StockExchange_ID"] + "\t" + dtRow["Depository_ID"] + "\t" + dtRow["StockExchange_Code"] + "\t" + dtRow["Depository_Code"]);
                }
                fgProvider.Redraw = true;

                //--- define Executions Orders list ----------------------------------------------
                Orders2 = new clsOrdersSecurity();
                Orders2.ServiceProvider_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                Orders2.DateFrom = dAktionDate.Value;
                Orders2.DateTo = dAktionDate.Value;
                Orders2.GetExecutedCommands();

                i = 0;
                fgList.Redraw = false;
                fgList.Rows.Count = 1;
                foreach (DataRow dtRow in Orders2.List.Rows)
                {
                    if (Convert.ToDateTime(dtRow["ExecuteDate"]) != Convert.ToDateTime("1900/01/01"))
                    {
                        i = i + 1;
                        fgList.AddItem(i + "\t" + dtRow["BulkCommand"] + "\t" + dtRow["ClientFullName"] + "\t" + dtRow["ContractTitle"] + "\t" + "" + "\t" +
                                       dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" + (Convert.ToInt16(dtRow["Aktion"]) == 1 ? "BUY" : "SELL") + "\t" + dtRow["Share_Title"] + "\t" +
                                       dtRow["Share_Code"] + "\t" + dtRow["ISIN"] + "\t" + dtRow["RealPrice"] + "\t" + dtRow["RealQuantity"] + "\t" +
                                       dtRow["RealAmount"] + "\t" + dtRow["Currency"] + "\t" + "" + "\t" + dtRow["SE_Code"] + "\t" + dtRow["Depository_Code"] + "\t" +
                                       dtRow["FeesDiff"] + "\t" + dtRow["FeesMarket"] + "\t" + dtRow["AccruedInterest"] + "\t" +
                                       dtRow["Commission"] + "\t" + dtRow["ID"]);
                    }
                }
                fgList.Redraw = true;
            }
            toolLeft.Visible = true;
            lblProviderRecords.Text = "Εγγραφές παρόχου " + cmbServiceProviders.Text;
            lblCompanyRecords.Text = "Εγγραφές HellasFin";            
        }
        private void fgProvider_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            if (e.Row > 0) {
                if (e.Col == 21 && (fgProvider[e.Row, "ClientOrder_ID"]+"") == "") e.Style = csDiff;                     // 21 - ClientOrder_ID
                if (e.Col == 22 && Convert.ToInt32(fgProvider[e.Row, "SE_ID"]) == 0) e.Style = csDiff;                   // 22 - SE_Code (StockExchange_Code)
                if (e.Col == 23 && Convert.ToInt32(fgProvider[e.Row, "Depository_ID"]) == 0) e.Style = csDiff;           // 23 - PSET
            }
        }
        private void cmbServiceProviders_SelectedValueChanged(object sender, EventArgs e)
        {
            sEffectCode = "";
            if (bCheckList)
            {
                if (Convert.ToInt32(cmbServiceProviders.SelectedValue) == 0)
                    btnSearch.Enabled = false;
                else
                {
                    ServiceProviders = new clsServiceProviders();
                    ServiceProviders.Record_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                    sEffectCode = ServiceProviders.EffectCode;
                    btnSearch.Enabled = true;
                }
            }
        }
        private void tsbFinish_Click(object sender, EventArgs e)
        {
            string sProductType = "", sRefNumber = "", sSE_ReutersCode = "", sDepository_Code = "";
            decimal decQuantityNominal = 0, decKoef = 0, decAccrued = 0, decMarketFee = 0, decFee = 0, decCommission = 0, locCommission = 0;

            for (j = 1; j <= fgCompare.Rows.Count - 1; j = j + 2)  {
                if (Convert.ToBoolean(fgCompare[j + 1, "Check"]) && Convert.ToInt16(fgCompare[j + 1, "Status"]) == 0) {
                  
                    iStockExchange_ID = Convert.ToInt32(fgCompare[j, "SE_ID"]);

                    sSE_ReutersCode = "";
                    foundRows = Global.dtStockExchanges.Select("ID = " + iStockExchange_ID);
                    if (foundRows.Length > 0)
                    {
                        sSE_ReutersCode = foundRows[0]["ReutersCode"] + "";
                    }

                    iDepository_ID = Convert.ToInt32(fgCompare[j, "Depository_ID"]); ;

                    //--- update Fees & etc. into Execution Order --------------------
                    Orders = new clsOrdersSecurity();
                    Orders.Record_ID = Convert.ToInt32(fgCompare[j + 1, "ID"]);
                    Orders.GetRecord();
                    Orders.StockExchange_ID = iStockExchange_ID;
                    Orders.FeesDiff = Convert.ToDecimal(fgCompare[j + 1, "Fee"]);
                    Orders.FeesMarket = Convert.ToDecimal(fgCompare[j + 1, "MarketFee"]);
                    Orders.AccruedInterest = Convert.ToDecimal(fgCompare[j + 1, "Accrued"]);
                    Orders.Commission = Convert.ToDecimal(fgCompare[j + 1, "Commission"]);
                    Orders.EditRecord();

                    sDepository_Code = fgCompare[j, "Depository"] + "";

                    //--- define child records and update Fees & etc. into them --------
                    sRefNumber = fgCompare[j + 1, 1] + "";                                                 // ClientOrder_ID
                    DefineChildOrders(sRefNumber, Convert.ToDateTime(fgCompare[j + 1, "AktionDate"]));
                    //if (sRefNumber == "33982258") i = i;    //M010100210126004001568051")

                    foreach (DataRow dtRow in dtChildOrders.Rows)
                    {
                        if (Convert.ToInt16(dtRow["CommandType_ID"]) == 1) {

                            dTradeDate = Convert.ToDateTime("1900/01/01");
                            sTradeTime = "";
                            dSettlementDate = Convert.ToDateTime("1900/01/01");
          
                            foundRows = CustodyCommands.List.Select("RefNumber = '" + sRefNumber + "'");
                            if (foundRows.Length > 0) {
                                dTradeDate = Convert.ToDateTime(foundRows[0]["TradeDate"]);
                                sTradeTime = foundRows[0]["TradeTime"] + "";
                                dSettlementDate = Convert.ToDateTime(foundRows[0]["SettlementDate"]);
                            }

                            decQuantityNominal = Convert.ToDecimal(fgCompare[j + 1, "Quantity"]);
                            decAccrued = Convert.ToDecimal(fgCompare[j + 1, "Accrued"]);
                            decMarketFee = Convert.ToDecimal(fgCompare[j + 1, "MarketFee"]);
                            decFee = Convert.ToDecimal(fgCompare[j + 1, "Fee"]);
                            decCommission = Convert.ToDecimal(fgCompare[j + 1, "Commission"]);

                            if (decQuantityNominal != 0) {
                                decKoef = Convert.ToDecimal(dtRow["RealQuantity"]) / decQuantityNominal;

                                //--- update Fees & etc. into Single Order --------------------
                                Orders = new clsOrdersSecurity();
                                Orders.Record_ID = Convert.ToInt32(dtRow["ID"]);
                                Orders.GetRecord();
                                Orders.StockExchange_ID = iStockExchange_ID;
                                Orders.FeesDiff = decKoef * decFee;
                                Orders.FeesMarket = decKoef * decMarketFee;
                                Orders.AccruedInterest = decKoef * decAccrued;
                                if (Convert.ToInt32(dtRow["BiggestQuantity"]) == 1) locCommission =  decCommission;
                                else locCommission = 0;
                                Orders.Commission = locCommission;
                                Orders.EditRecord();

                                //--- add finish record into Commands_ProvidersRecs table (oristikopoiimeni praxi) ------
                                sProductType = "";
                                switch (Convert.ToInt32(dtRow["Product_ID"]))
                                {
                                    case 1:                       // 1 - Metoxi Trader
                                        sProductType = "1";       // 1 - Metoxi Effect
                                        break;
                                    case 2:                       // 2 - Omologa Trader
                                        sProductType = "3";       // 3 - Omologa Effect
                                        break;
                                    case 4:                       // 4 - DAK Trader
                                        sProductType = "1";       // 1 - Metoxi Effect
                                        break;
                                    case 6:                       // 6 - AK Trader
                                        sProductType = "2";       // 2 - AK Effect
                                        break;
                                }

                                Orders_ProvidersRecs = new clsOrders_ProvidersRecs();
                                Orders_ProvidersRecs.StockCompany_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                                Orders_ProvidersRecs.TradeDate = dTradeDate;
                                Orders_ProvidersRecs.TradeTime = sTradeTime;
                                Orders_ProvidersRecs.SettlementDate = dSettlementDate;
                                Orders_ProvidersRecs.CompanyCode = sEffectCode;
                                Orders_ProvidersRecs.Command_ID = Convert.ToInt32(dtRow["ID"]);                 // dtChildOrders.ID = Orders.ID = Command_ID                    
                                Orders_ProvidersRecs.Aktion = dtRow["Aktion"] + "";
                                Orders_ProvidersRecs.Code = dtRow["Code"] + "";
                                Orders_ProvidersRecs.Portfolio = dtRow["Portfolio"] + "";
                                Orders_ProvidersRecs.ClientName = dtRow["ClientFullName"] + "";
                                Orders_ProvidersRecs.ContractTitle = dtRow["ContractTitle"] + "";
                                Orders_ProvidersRecs.SecurityCode = sProductType;
                                Orders_ProvidersRecs.ISIN = dtRow["ISIN"] + "";
                                Orders_ProvidersRecs.SecurityDescription = dtRow["Share_Title"] + "";
                                Orders_ProvidersRecs.Quantity = Convert.ToDecimal(dtRow["RealQuantity"]);
                                Orders_ProvidersRecs.Price = Convert.ToDecimal(dtRow["RealPrice"]);
                                Orders_ProvidersRecs.TradeCurrency = dtRow["Currency"] + "";
                                Orders_ProvidersRecs.AccruedInterest = decKoef * decAccrued;
                                Orders_ProvidersRecs.MarketFee = decKoef * decMarketFee;
                                Orders_ProvidersRecs.StockExchange_ID = iStockExchange_ID;
                                Orders_ProvidersRecs.StockExchange_Code = sSE_ReutersCode;                      //dtRow["SE_Code"] + "";
                                Orders_ProvidersRecs.Depository_ID = iDepository_ID;  
                                Orders_ProvidersRecs.Depository_Code = sDepository_Code;
                                Orders_ProvidersRecs.SettlementCurrency = dtRow["Currency"] + "";
                                Orders_ProvidersRecs.CurrencyRate = 1;
                                Orders_ProvidersRecs.Notes = "";
                                Orders_ProvidersRecs.Fee = decKoef * decFee;
                                Orders_ProvidersRecs.RefNumber = sRefNumber;
                                Orders_ProvidersRecs.Commission = locCommission;
                                Orders_ProvidersRecs.InsertRecord();
                            }
                        }
                    }
                }
            }
            DefineList();
            DefineFinishedRecords();
            Matching();
        }
        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
       
            switch (Convert.ToInt32(tabMain.SelectedIndex))
            {
                case 0:                                                            //  tpProvider
                    break;
                case 1:                                                            //  tpCompany
                    break;
                case 2:                                                            //  tpMatching
                    //DataMatching();
                    break;
                case 3:                                                            //  tpFinishing
                    DefineFinishedRecords();
                    break;
            }
        }

        private void picCloseChangeGroup_Click(object sender, EventArgs e)
        {
            panWarnings.Visible = false;
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 0)
            {
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) ==
                    System.Windows.Forms.DialogResult.Yes)
                {
                    CustodyCommands = new clsCustodyCommands();
                    CustodyCommands.Record_ID = Convert.ToInt32(fgProvider[fgProvider.Row, "ID"]);
                    CustodyCommands.DeleteRecord();
                    fgProvider.RemoveItem(fgProvider.Row);
                }
            }
        }
        private void tsbEffect_Click(object sender, EventArgs e)
        {
            var EXL = new Microsoft.Office.Interop.Excel.Application();
            var oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            // System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("el-GR");
            EXL.Workbooks.Add();
            EXL.ScreenUpdating = false;
            EXL.Caption = Global.AppTitle;
            EXL.Cells[1, 1].Value = "Εκτελούσα Επιχείρηση";
            EXL.Cells[1, 2].Value = "Ημερομηνία Εκτέλεσης";
            EXL.Cells[1, 3].Value = "Ωρα Εκτέλεσης";
            EXL.Cells[1, 4].Value = "Ημερομηνία Διακανονισμού";
            EXL.Cells[1, 5].Value = "N";
            EXL.Cells[1, 6].Value = "Ενέργεια";
            EXL.Cells[1, 7].Value = "Κωδικός πελάτη";
            EXL.Cells[1, 8].Value = "Portfolio";
            EXL.Cells[1, 9].Value = "Όνομα Πελάτη";
            EXL.Cells[1, 10].Value = "Προϊον";
            EXL.Cells[1, 11].Value = "ISIN";
            EXL.Cells[1, 12].Value = "Τίτλος";
            EXL.Cells[1, 13].Value = "Ποσότητα";
            EXL.Cells[1, 14].Value = "Τιμή εκτέλεσης";
            EXL.Cells[1, 15].Value = "Νόμισμα";
            EXL.Cells[1, 16].Value = "Δεδουλευμένοι τόκοι";
            EXL.Cells[1, 17].Value = "Εξοδα/προμήθειες";
            EXL.Cells[1, 18].Value = "Χρηματιστήριο";
            EXL.Cells[1, 19].Value = "Αποθετήριο";
            EXL.Cells[1, 20].Value = "Νομισμα διακανονισμού";
            EXL.Cells[1, 21].Value = "Ισοτιμία Νομίσματος";
            EXL.Cells[1, 22].Value = "Σχόλιο";
            EXL.Cells[1, 23].Value = "Μεταβιβαστικά";
            EXL.Cells[1, 24].Value = "Order Id";
            EXL.Cells[1, 25].Value = "Προμήθεια";
            j = 1;

            var loopTo = fgFinish.Rows.Count - 1;
            for (this.i = 1; this.i <= loopTo; this.i++)
            {
                if (Convert.ToBoolean(fgFinish[i, 0])) {
                    j = j + 1;
                    EXL.Cells[j + 1, 1].Value = fgFinish[i, 2];
                    EXL.Cells[j + 1, 2].Value = Convert.ToDateTime(fgFinish[i, 3]).ToString("yyyy/MM/dd");
                    EXL.Cells[j + 1, 3].Value = fgFinish[i, 4];
                    EXL.Cells[j + 1, 4].Value = Convert.ToDateTime(fgFinish[i, 5]).ToString("yyyy/MM/dd");
                    EXL.Cells[j + 1, 5].Value = fgFinish[i, 6];
                    EXL.Cells[j + 1, 6].Value = fgFinish[i, 7];
                    EXL.Cells[j + 1, 7].Value = fgFinish[i, 8];
                    EXL.Cells[j + 1, 8].Value = fgFinish[i, 9];
                    EXL.Cells[j + 1, 9].Value = fgFinish[i, 10];
                    EXL.Cells[j + 1, 10].Value = fgFinish[i, 11];
                    EXL.Cells[j + 1, 11].Value = fgFinish[i, 12];
                    EXL.Cells[j + 1, 12].Value = fgFinish[i, 13];
                    EXL.Cells[j + 1, 13].Value = Convert.ToDecimal(fgFinish[i, 14] + "");
                    EXL.Cells[j + 1, 14].Value = Convert.ToDecimal(fgFinish[i, 15] + "");
                    EXL.Cells[j + 1, 15].Value = fgFinish[i, 16];
                    EXL.Cells[j + 1, 16].Value = Convert.ToDecimal(fgFinish[i, "Accured"] + "");
                   
                    EXL.Cells[j + 1, 18].Value = fgFinish[i, 19];
                    EXL.Cells[j + 1, 19].Value = fgFinish[i, "Depository_Code"];
                    EXL.Cells[j + 1, 20].Value = fgFinish[i, 21];
                    EXL.Cells[j + 1, 21].Value = Convert.ToDecimal(fgFinish[i, 22] + "");
                    EXL.Cells[j + 1, 22].Value = "";                                                         // fgFinish[i, 23] - RTO notes can't export to Effect;

                    EXL.Cells[j + 1, 24].Value = fgFinish[i, "ClientOrder_ID"];
                    EXL.Cells[j + 1, 25].Value = Convert.ToDecimal(fgFinish[i, 26] + "");

                    switch (Convert.ToInt32(cmbServiceProviders.SelectedValue))
                    {
                        case 17:                                                                              // 17 - Pireaus
                            EXL.Cells[j + 1, 17].Value = Convert.ToDecimal(fgFinish[i, "MarketFee"] + "");
                            EXL.Cells[j + 1, 23].Value = Convert.ToDecimal(fgFinish[i, "Fee"] + "");
                            break;
                        case 19:                                                                              // 19 - INTESA
                            EXL.Cells[j + 1, 17].Value = Convert.ToDecimal(fgFinish[i, "MarketFee"] + "") + Convert.ToDecimal(fgFinish[i, "Fee"] + "");
                            EXL.Cells[j + 1, 23].Value = "";
                            break;
                        case 20:                                                                              // 20 - BNP ARBITRAGE
                            EXL.Cells[j + 1, 17].Value = Convert.ToDecimal(fgFinish[i, "MarketFee"] + "") + Convert.ToDecimal(fgFinish[i, "Fee"] + "");
                            EXL.Cells[j + 1, 23].Value = "";
                            break;
                    }
                }
            }

            EXL.ScreenUpdating = true;
            EXL.Visible = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
        }
        private void tsbDel_Click(object sender, EventArgs e)
        {
            if (fgFinish.Row > 0)
            {
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) ==
                    System.Windows.Forms.DialogResult.Yes)
                {
                    Orders_ProvidersRecs = new clsOrders_ProvidersRecs();
                    Orders_ProvidersRecs.Record_ID = Convert.ToInt32(fgFinish[fgFinish.Row, "ID"]);
                    Orders_ProvidersRecs.DeleteRecord();
                    fgFinish.RemoveItem(fgFinish.Row);
                }
            }
        }
        private void chkFinish_CheckedChanged(object sender, EventArgs e)
        {
            for (i = 1; i <= fgFinish.Rows.Count - 1; i++) fgFinish[i, 0] = chkFinish.Checked;
        }

        private void tsbImport_Click(object sender, EventArgs e)
        {
            bEmptyClientOrderID = false;
            fgStockExchanges.Rows.Count = 1;
            fgDepositories.Rows.Count = 1;
            panWarnings.Visible = false;

            frmImportData locImportData = new frmImportData();

            switch (Convert.ToInt32(cmbServiceProviders.SelectedValue)) {
                case 17:                                                           //------------------------------17 - PIREAUS SECURITIES
                    locImportData.FileType = 2;                                    // .csv file
                    locImportData.Shema = 25;
                    locImportData.ReadMode = 2;
                    locImportData.ShowDialog();
                    if (locImportData.Aktion == 1) {
                        dtList = locImportData.Result;

                        bEmptyClientOrderID = false;
                        foreach (DataRow dtRow in dtList.Rows) {
                            if ((dtRow["f18"] + "").Trim() == "") bEmptyClientOrderID = true;

                            iStockExchange_ID = 0;
                            foundRows = Global.dtStockExchanges.Select("Code = '" + dtRow["f19"] + "'");
                            if (foundRows.Length > 0) iStockExchange_ID = Convert.ToInt32(foundRows[0]["ID"]);

                            iDepository_ID = 0;
                            foundRows = dtDepositories_Alias.Select("Code = '" + dtRow["f20"] + "'");
                            if (foundRows.Length > 0) iDepository_ID = Convert.ToInt32(foundRows[0]["Item_ID"]);

                            CustodyCommands = new clsCustodyCommands();
                            CustodyCommands.ServiceProvider_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                            CustodyCommands.AktionDate = DateTime.Now;
                            dTemp = Convert.ToDateTime(dtRow["f1"]);
                            CustodyCommands.TradeDate = dTemp;
                            sTemp = dtRow["f2"] + "";
                            CustodyCommands.TradeTime = sTemp.Substring(0, 2) + ":" + sTemp.Substring(2, 2) + ":" + sTemp.Substring(4, 2);
                            dTemp = Convert.ToDateTime(dtRow["f3"]);
                            CustodyCommands.SettlementDate = dTemp;
                            CustodyCommands.TradeCurrency = dtRow["f4"] + "";
                            CustodyCommands.Counterparty = "";
                            CustodyCommands.ISIN = dtRow["f5"] + "";
                            CustodyCommands.SecurityCode = dtRow["f6"] + "";
                            CustodyCommands.SecurityDescription = dtRow["f7"] + "";
                            CustodyCommands.Market = dtRow["f19"] + "";
                            CustodyCommands.Sign = dtRow["f8"] + "";
                            CustodyCommands.QuantityNominal = Convert.ToDecimal(dtRow["f9"]);
                            CustodyCommands.Price = Convert.ToDecimal(dtRow["f10"]);
                            CustodyCommands.AccruedInterest = Convert.ToDecimal(dtRow["f11"]);
                            CustodyCommands.Commission = Convert.ToDecimal(dtRow["f12"]);
                            CustodyCommands.Fees = Convert.ToDecimal(dtRow["f13"]);
                            CustodyCommands.MarketFee = Convert.ToDecimal(dtRow["f14"]);
                            CustodyCommands.Taxes = Convert.ToDecimal(dtRow["f15"]);
                            CustodyCommands.SettlementAmount = Convert.ToDecimal(dtRow["f16"]);
                            CustodyCommands.ExchangeRate = 0;
                            CustodyCommands.SettlementAmountCurr = Convert.ToDecimal(dtRow["f17"]);
                            CustodyCommands.OrderNumber = dtRow["f18"] + "";
                            CustodyCommands.TradeID = "";
                            CustodyCommands.CancelledTradeID = "";
                            CustodyCommands.Against = "";
                            CustodyCommands.OriginarySystem = "";
                            CustodyCommands.SettlementPlace = dtRow["f20"] + "";
                            CustodyCommands.RefNumber = dtRow["f18"] + "";
                            CustodyCommands.MIC_Code = dtRow["f19"] + "";
                            CustodyCommands.PSET = dtRow["f20"] + "";
                            CustodyCommands.StockExchange_ID = iStockExchange_ID;
                            CustodyCommands.Depository_ID = iDepository_ID;
                            CustodyCommands.Command_Execution_ID = iCommand_Executions_ID;
                            CustodyCommands.InsertRecord();
                        }
                    }                    
                    break;

                case 19:                                                           //------------------------------ 19 - INTESA
                    locImportData.FileType = 1;                                    // .xls file
                    locImportData.Shema = 27;
                    locImportData.ReadMode = 2;
                    locImportData.ShowDialog();
                    if (locImportData.Aktion == 1)  {
                      
                        dtList = locImportData.Result;

                        bEmptyClientOrderID = false;
                        foreach (DataRow dtRow in dtList.Rows) {
                            if ((dtRow["f23"] + "").Trim() == "") bEmptyClientOrderID = true;                               // 23 - ClientOrder_ID

                            iStockExchange_ID = 0;
                            foundRows = Global.dtStockExchanges.Select("Code = '" + dtRow["f24"] + "'");
                            if (foundRows.Length > 0) iStockExchange_ID = Convert.ToInt32(foundRows[0]["ID"]);

                            iDepository_ID = 0;
                            foundRows = dtDepositories_Alias.Select("Code = '" + dtRow["f25"] + "'");
                            if (foundRows.Length > 0) iDepository_ID = Convert.ToInt32(foundRows[0]["Item_ID"]);

                            CustodyCommands = new clsCustodyCommands();
                            CustodyCommands.ServiceProvider_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                            CustodyCommands.AktionDate = DateTime.Now;

                            sTemp = dtRow["f1"] + "";
                            i = sTemp.IndexOf(" ");
                            dTradeDate = Convert.ToDateTime(sTemp.Substring(0, i).Trim());
                            sTradeTime = sTemp.Substring(i + 1);
                            CustodyCommands.TradeDate = dTradeDate;
                            CustodyCommands.TradeTime = sTradeTime;

                            CustodyCommands.SettlementDate = Convert.ToDateTime(dtRow["f2"]);
                            CustodyCommands.TradeCurrency = dtRow["f3"] + "";
                            CustodyCommands.Counterparty = dtRow["f4"] + "";
                            CustodyCommands.ISIN = dtRow["f5"] + "";
                            CustodyCommands.SecurityCode = "";
                            CustodyCommands.SecurityDescription = dtRow["f6"] + "";
                            CustodyCommands.Market = dtRow["f7"] + "";
                            CustodyCommands.Sign = dtRow["f8"] + "";
                            CustodyCommands.QuantityNominal = Convert.ToDecimal(dtRow["f9"].ToString().Replace(",", "").Replace(".", ","));
                            CustodyCommands.Price = Convert.ToDecimal(dtRow["f10"].ToString().Replace(",", "").Replace(".", ","));
                            CustodyCommands.AccruedInterest = Convert.ToDecimal(dtRow["f11"].ToString().Replace(",", "").Replace(".", ","));
                            CustodyCommands.Commission = Convert.ToDecimal(dtRow["f12"].ToString().Replace(",", "").Replace(".", ","));
                            CustodyCommands.Fees = Convert.ToDecimal(dtRow["f13"].ToString().Replace(",", "").Replace(".", ","));
                            CustodyCommands.MarketFee = 0;
                            CustodyCommands.Taxes = 0;
                            CustodyCommands.SettlementAmount = Convert.ToDecimal(dtRow["f14"].ToString().Replace(",", "").Replace(".", ","));
                            CustodyCommands.ExchangeRate = Convert.ToDecimal(dtRow["f15"].ToString().Replace(",", "").Replace(".", ","));
                            CustodyCommands.SettlementAmountCurr = Convert.ToDecimal(dtRow["f16"].ToString().Replace(",", "").Replace(".", ","));
                            CustodyCommands.OrderNumber = dtRow["f17"] + "";
                            CustodyCommands.TradeID = dtRow["f18"] + "";
                            CustodyCommands.CancelledTradeID = dtRow["f19"] + "";
                            CustodyCommands.Against = dtRow["f20"] + "";
                            CustodyCommands.OriginarySystem = dtRow["f21"] + "";
                            CustodyCommands.SettlementPlace = dtRow["f22"] + "";
                            CustodyCommands.RefNumber = dtRow["f23"] + "";
                            CustodyCommands.MIC_Code = dtRow["f24"] + "";
                            CustodyCommands.PSET = dtRow["f25"] + "";
                            CustodyCommands.StockExchange_ID = iStockExchange_ID;
                            CustodyCommands.Depository_ID = iDepository_ID;
                            CustodyCommands.Command_ID = 0;
                            CustodyCommands.Command_Execution_ID = iCommand_Executions_ID;
                            CustodyCommands.Exported = 0;
                            CustodyCommands.InsertRecord();
                        }
                    }
                    break;

                case 20:                                                           //------------------------------ 20 - BNP ARBITRAGE
                    locImportData.FileType = 2;                                    // .csv file
                    locImportData.Shema = 28;
                    locImportData.ReadMode = 2;
                    locImportData.cmbFileType.SelectedIndex = 2;
                    locImportData.ShowDialog();
                    if (locImportData.Aktion == 1)
                    {

                        dtList = locImportData.Result;

                        bEmptyClientOrderID = false;
                        foreach (DataRow dtRow in dtList.Rows)
                        {
                            if ((dtRow["f2"] + "").Trim() == "") bEmptyClientOrderID = true;                               // 2 - ClientOrder_ID

                            iStockExchange_ID = 0;
                            foundRows = Global.dtStockExchanges.Select("Code = '" + dtRow["f6"] + "'");
                            if (foundRows.Length > 0) iStockExchange_ID = Convert.ToInt32(foundRows[0]["ID"]);

                            iDepository_ID = 0;
                            sDepository_Code = "";
                            foundRows = dtDepositories_Alias.Select("Code = '" + dtRow["f18"] + "'");
                            if (foundRows.Length > 0)
                            {
                                iDepository_ID = Convert.ToInt32(foundRows[0]["Item_ID"]);

                                Depositories = new clsDepositories();
                                Depositories.Record_ID = iDepository_ID;
                                Depositories.GetRecord();                               
                                sDepository_Code = Depositories.Code;
                            }

                            CustodyCommands = new clsCustodyCommands();
                            CustodyCommands.ServiceProvider_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                            CustodyCommands.AktionDate = DateTime.Now;

                            dTradeDate = Convert.ToDateTime(dtRow["f10"]);
                            CustodyCommands.TradeDate = dTradeDate.Date;
                            CustodyCommands.TradeTime = dTradeDate.Hour + ":" + dTradeDate.Minute + ":" + dTradeDate.Second + ":" + dTradeDate.Millisecond;

                            CustodyCommands.SettlementDate = Convert.ToDateTime(dtRow["f17"] + "") ;
                            CustodyCommands.TradeCurrency = dtRow["f13"] + "";
                            CustodyCommands.Counterparty = "";
                            CustodyCommands.ISIN = dtRow["f5"] + "";
                            CustodyCommands.SecurityCode = dtRow["f3"] + "";
                            CustodyCommands.SecurityDescription = dtRow["f4"] + "";
                            CustodyCommands.Market = dtRow["f6"] + "";
                            CustodyCommands.Sign = dtRow["f7"] + "" == "Buy" ? "Α" : "Π";
                            CustodyCommands.QuantityNominal = Convert.ToDecimal(dtRow["f11"].ToString().Replace(",", "").Replace(".", ","));
                            CustodyCommands.Price = Convert.ToDecimal(dtRow["f12"].ToString().Replace(",", "").Replace(".", ","));
                            CustodyCommands.AccruedInterest = 0;
                            CustodyCommands.Commission = Convert.ToDecimal(dtRow["f14"].ToString().Replace(",", "").Replace(".", ","));
                            CustodyCommands.Fees = Convert.ToDecimal(dtRow["f15"].ToString().Replace(",", "").Replace(".", ","));
                            CustodyCommands.MarketFee = 0;
                            CustodyCommands.Taxes = 0;
                            CustodyCommands.SettlementAmount = Convert.ToDecimal(dtRow["f16"].ToString().Replace(",", "").Replace(".", ","));
                            CustodyCommands.ExchangeRate = 0;
                            CustodyCommands.SettlementAmountCurr = Convert.ToDecimal(dtRow["f16"].ToString().Replace(",", "").Replace(".", ","));
                            CustodyCommands.OrderNumber = "";
                            CustodyCommands.TradeID = "";
                            CustodyCommands.CancelledTradeID = "";
                            CustodyCommands.Against = "";
                            CustodyCommands.OriginarySystem = "";
                            CustodyCommands.SettlementPlace = dtRow["f18"] + "";
                            CustodyCommands.RefNumber = dtRow["f2"] + "";
                            CustodyCommands.MIC_Code = dtRow["f6"] + "";
                            CustodyCommands.PSET = sDepository_Code;
                            CustodyCommands.StockExchange_ID = iStockExchange_ID;
                            CustodyCommands.Depository_ID = iDepository_ID;
                            CustodyCommands.Command_ID = 0;
                            CustodyCommands.Command_Execution_ID = iCommand_Executions_ID;
                            CustodyCommands.Exported = 0;
                            CustodyCommands.InsertRecord();
                        }
                    }
                    break;
            }
            DefineList();
            tabMain.SelectedIndex = 0;
            CheckProblems();
        }
        private void tsbCheckErrors_Click(object sender, EventArgs e)
        {
            CheckProblems();
        }
        private void tsbCalcAccrued_Click(object sender, EventArgs e)
        {

        }

        private void tsbExportEffect_Click(object sender, EventArgs e)
        {

        }

        private void btnExportTrx_Click(object sender, EventArgs e)
        {

            clsCurrencies klsCurrency = new clsCurrencies();
            klsCurrency.DateFrom = dAktionDate.Value;
            klsCurrency.DateTo = dAktionDate.Value;
            klsCurrency.Code = "EUR";
            klsCurrency.GetCurrencyRates_Period();

            clsTrx Trx = new clsTrx();
            for (i = 1; i <= fgFinish.Rows.Count - 1; i++)
            {
                Trx = new clsTrx();
                Trx.TrxType_ID = 1;
                Trx.TrxDate = Convert.ToDateTime(fgFinish[i, "TradeDate"] + " " + fgFinish[i, "TradeTime"]);
                Trx.TrxJustification = fgFinish[i, "Aktion"] + "" == "BUY" ? "Αγορά" : fgFinish[i, "Aktion"] + "" == "SELL" ? "Πώληση" : "";
                Trx.ISettlementDate = Convert.ToDateTime(fgFinish[i, "SettlementDate"]);
                Trx.ASettlementDate = Convert.ToDateTime(fgFinish[i, "SettlementDate"]);
                Trx.SingleOrder_ID = Convert.ToInt32(fgFinish[i, "Command_ID"]);
                Trx.ExecutionOrder_ID = Convert.ToInt32(fgFinish[i, "Command_ID"]);
                Trx.ExecReference_ID = fgFinish[i, "ClientOrder_ID"] + "";
                Trx.InvoiceType_ID = 0;
                Trx.ReferenceNo = "Z/123";
                Trx.D_C = fgFinish[i, "Aktion"] + "" == "BUY" ? "Credit" : fgFinish[i, "Aktion"] + "" == "SELL" ? "Debit" : "";
                Trx.Contract_ID = Convert.ToInt32(fgFinish[i, "Contract_ID"]);
                Trx.Contract_Details_ID = Convert.ToInt32(fgFinish[i, "Contract_Details_ID"]);
                Trx.Contract_Packages_ID = Convert.ToInt32(fgFinish[i, "Contract_Packages_ID"]);
                Trx.ExecutionProvider_ID = Convert.ToInt32(fgFinish[i, "Provider_ID"]);
                Trx.Custodian_ID = Global.IsNumeric(fgFinish[i, "Custodian_ID"]) ? Convert.ToInt32(fgFinish[i, "Custodian_ID"]) : 0 ;
                Trx.TrxCurrency = fgFinish[i, "Currency"] + "";

                fltTemp = 0;
                foundRows = klsCurrency.List.Select("Currency = 'EUR" + fgFinish[i, "Currency"] + "='");
                if (foundRows.Length > 0)
                    fltTemp = Convert.ToSingle(foundRows[0]["Rate"]);

                Trx.TrxCurrencyRate = fltTemp;
                if (fltTemp != 0) Trx.ReverseCurrencyRate = 1 / fltTemp;
                else Trx.ReverseCurrencyRate = 0;

                Trx.DebitAmount_EUR = 0; 
                Trx.DebitAmount_Cur = 0;
                Trx.CreditAmount_EUR = 0;
                Trx.CreditAmount_Cur = 0;
                Trx.NetDebitAmount_EUR = 0;
                Trx.NetDebitAmount_Cur = 0;
                Trx.NetCreditAmount_EUR = 0;
                Trx.NetCreditAmount_Cur = 0;
                Trx.TotalExpences_EUR = 0;
                Trx.TotalExpences_Cur = 0;
                Trx.Amount_EUR = 0;
                Trx.Amount_Cur = 0;
                Trx.NetAmount_EUR = 0;
                Trx.NetAmount_Cur = 0;
                Trx.TrxComments = fgFinish[i, "Notes"] + "";
                Trx.ShareCodes_ID = Convert.ToInt32(fgFinish[i, "ShareCodes_ID"]);
                Trx.Quantity = Convert.ToSingle(fgFinish[i, "Quantity"]);
                Trx.Price = Convert.ToSingle(fgFinish[i, "Price"]);
                Trx.ExecutionVenue_ID = Convert.ToInt32(fgFinish[i, "StockExchange_ID"]);
                Trx.Depository_ID = Convert.ToInt32(fgFinish[i, "Depository_ID"]);
                Trx.TransferCustodian = "XXX";
                Trx.TransferAccount = "XXX";
                Trx.TransferAccountName = "XXX";
                Trx.Accruals_EUR = Convert.ToSingle(fgFinish[i, "Accured"]);
                Trx.Accruals_Cur = Convert.ToSingle(fgFinish[i, "Accured"]);
                Trx.ExecFee_EUR = Convert.ToSingle(fgFinish[i, "Commission"]);
                Trx.ExecFee_Cur = Convert.ToSingle(fgFinish[i, "Commission"]);
                Trx.ExecFeeReturn_EUR = Convert.ToSingle(fgFinish[i, "Fee"]);
                Trx.ExecFeeReturn_Cur = Convert.ToSingle(fgFinish[i, "Fee"]);
                Trx.ExecFeeIncome_EUR = Convert.ToSingle(fgFinish[i, "MarketFee"]);
                Trx.ExecFeeIncome_Cur = Convert.ToSingle(fgFinish[i, "MarketFee"]);
                Trx.SettleFee_EUR = 0;
                Trx.SettleFee_Cur = 0;
                Trx.SettleFeeReturn_EUR = 0;
                Trx.SettleFeeReturn_Cur = 0;
                Trx.SettleFeeIncome_EUR = 0;
                Trx.SettleFeeIncome_Cur = 0;
                Trx.ATHEXTransferFee_EUR = 0;
                Trx.ATHEXTransferFee_Cur = 0;
                Trx.ATHEXExpences_EUR = 0;
                Trx.ATHEXExpences_Cur = 0;
                Trx.ATHEXFileExpences_EUR = 0;
                Trx.ATHEXFileExpences_Cur = 0;
                Trx.StockXFee_EUR = 0;
                Trx.StockXFee_Cur = 0;
                Trx.PriSecExecFeesReturn_EUR = 0;
                Trx.PriSecExecFeesReturn_Cur = 0;
                Trx.PriSecSettleFeesReturn_EUR = 0;
                Trx.PriSecSettleFeesReturn_Cur = 0;
                Trx.ManagementFee_EUR = 0;
                Trx.ManagementFee_Cur = 0;
                Trx.ManagementFeeIncome_EUR = 0;
                Trx.ManagementFeeIncome_Cur = 0;
                Trx.SafekeepingFee_EUR = 0;
                Trx.SafekeepingFee_Cur = 0;
                Trx.SafekeepingFeeIncome_EUR = 0;
                Trx.SafekeepingFeeIncome_Cur = 0;
                Trx.PerformanceFee_EUR = 0;
                Trx.PerformanceFee_Cur = 0;
                Trx.PerformanceFeeIncome_EUR = 0;
                Trx.PerformanceFeeIncome_Cur = 0;
                Trx.SupportFee_EUR = 0;
                Trx.SupportFee_Cur = 0;
                Trx.SupportFeeIncome_EUR = 0;
                Trx.SupportFeeIncome_Cur = 0;
                Trx.FxFee_EUR = 0;
                Trx.FxFee_Cur = 0;
                Trx.CorpActionFee_EUR = 0;
                Trx.CorpActionFee_Cur = 0;
                Trx.SecTransferFee_EUR = 0;
                Trx.SecTransferFee_Cur = 0;
                Trx.SecTransferFeeReturn_EUR = 0;
                Trx.SecTransferFeeReturn_Cur = 0;
                Trx.SecTransferFeeIncome_EUR = 0;
                Trx.SecTransferFeeIncome_Cur = 0;
                Trx.CashTransferFee_EUR = 0;
                Trx.CashTransferFee_Cur = 0;
                Trx.CashTransferFeeReturn_EUR = 0;
                Trx.CashTransferFeeReturn_Cur = 0;
                Trx.CashTransferFeeIncome_EUR = 0;
                Trx.CashTransferFeeIncome_Cur = 0;
                Trx.TaxExpencesAbroad_EUR = 0;
                Trx.TaxExpencesAbroad_Cur = 0;
                Trx.SalesTax_EUR = 0;
                Trx.SalesTax_Cur = 0;
                Trx.VAT_EUR = 0;
                Trx.VAT_Cur = 0;
                Trx.WHTax_EUR = 0;
                Trx.WHTax_Cur = 0;
                Trx.GRTax_EUR = 0;
                Trx.GRTax_Cur = 0;
                Trx.EntryUser_ID = Global.User_ID;
                Trx.EntryDate = DateTime.Now;
                Trx.Status = 1;
                Trx.InsertRecord();
            }            
        }

        private void CheckProblems()
        {
            panWarnings.Visible = false;

            fgStockExchanges.Rows.Count = 1;
            fgDepositories.Rows.Count = 1;
            fgOrdersID.Rows.Count = 1;

            for (i=1; i <= fgProvider.Rows.Count -1; i++)
            {
                if (Convert.ToInt32(fgProvider[i, "SE_ID"]) == 0) fgStockExchanges.AddItem(fgProvider[i, "SE_Code"] + ""); 
                if (Convert.ToInt32(fgProvider[i, "Depository_ID"]) == 0) fgDepositories.AddItem(fgProvider[i, "PSET"] + "");
                if ((fgProvider[i, "ClientOrder_ID"]+"").Trim() == "" || (fgProvider[i, "ClientOrder_ID"] + "").Trim() == "-") fgOrdersID.AddItem("Εγγραφή με ΑΑ = " + fgProvider[i, "AA"]);
            }

            if (fgStockExchanges.Rows.Count > 1 || fgDepositories.Rows.Count > 1 || fgOrdersID.Rows.Count > 1) panWarnings.Visible = true;
            else MessageBox.Show("Είναι ΟΚ", "DB Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); ;

        }
        private void tsbCompare_Click(object sender, EventArgs e)
        {
            Matching();
        }
        private void Matching()
        {
            bCheckCompare = false;
            dtCompare = new DataTable("CompareTable");
            dtCol = dtCompare.Columns.Add("ClientOrder_ID", System.Type.GetType("System.String"));
            dtCol = dtCompare.Columns.Add("AktionDate_1", System.Type.GetType("System.String"));
            dtCol = dtCompare.Columns.Add("AktionDate_2", System.Type.GetType("System.String"));
            dtCol = dtCompare.Columns.Add("ExecuteDate_1", System.Type.GetType("System.String"));
            dtCol = dtCompare.Columns.Add("ExecuteDate_2", System.Type.GetType("System.String"));
            dtCol = dtCompare.Columns.Add("ISIN_1", System.Type.GetType("System.String"));
            dtCol = dtCompare.Columns.Add("ISIN_2", System.Type.GetType("System.String"));
            dtCol = dtCompare.Columns.Add("Aktion_1", System.Type.GetType("System.String"));
            dtCol = dtCompare.Columns.Add("Aktion_2", System.Type.GetType("System.String"));
            dtCol = dtCompare.Columns.Add("Currency_1", System.Type.GetType("System.String"));
            dtCol = dtCompare.Columns.Add("Currency_2", System.Type.GetType("System.String"));
            dtCol = dtCompare.Columns.Add("Quantity_1", System.Type.GetType("System.Decimal"));
            dtCol = dtCompare.Columns.Add("Quantity_2", System.Type.GetType("System.Decimal"));
            dtCol = dtCompare.Columns.Add("Amount_1", System.Type.GetType("System.Decimal"));
            dtCol = dtCompare.Columns.Add("Amount_2", System.Type.GetType("System.Decimal"));
            dtCol = dtCompare.Columns.Add("Price_1", System.Type.GetType("System.Decimal"));
            dtCol = dtCompare.Columns.Add("Price_2", System.Type.GetType("System.Decimal"));
            dtCol = dtCompare.Columns.Add("SE_Code_1", System.Type.GetType("System.String"));
            dtCol = dtCompare.Columns.Add("SE_Code_2", System.Type.GetType("System.String"));
            dtCol = dtCompare.Columns.Add("Depository_1", System.Type.GetType("System.String"));
            dtCol = dtCompare.Columns.Add("Depository_2", System.Type.GetType("System.String"));
            dtCol = dtCompare.Columns.Add("Fee_1", System.Type.GetType("System.Decimal"));
            dtCol = dtCompare.Columns.Add("Fee_2", System.Type.GetType("System.Decimal"));
            dtCol = dtCompare.Columns.Add("MarketFee_1", System.Type.GetType("System.Decimal"));
            dtCol = dtCompare.Columns.Add("MarketFee_2", System.Type.GetType("System.Decimal"));
            dtCol = dtCompare.Columns.Add("Accrued_1", System.Type.GetType("System.Decimal"));
            dtCol = dtCompare.Columns.Add("Accrued_2", System.Type.GetType("System.Decimal"));
            dtCol = dtCompare.Columns.Add("Commission_1", System.Type.GetType("System.Decimal"));
            dtCol = dtCompare.Columns.Add("Commission_2", System.Type.GetType("System.Decimal"));
            dtCol = dtCompare.Columns.Add("ID_1", System.Type.GetType("System.Int32"));
            dtCol = dtCompare.Columns.Add("ID_2", System.Type.GetType("System.Int32"));
            dtCol = dtCompare.Columns.Add("Status_1", System.Type.GetType("System.Int16"));
            dtCol = dtCompare.Columns.Add("Status_2", System.Type.GetType("System.Int16"));
            dtCol = dtCompare.Columns.Add("SE_ID_1", System.Type.GetType("System.Int32"));
            dtCol = dtCompare.Columns.Add("SE_ID_2", System.Type.GetType("System.Int32"));
            dtCol = dtCompare.Columns.Add("Check", System.Type.GetType("System.Int16"));
            dtCol = dtCompare.Columns.Add("TradeTime", System.Type.GetType("System.String"));
            dtCol = dtCompare.Columns.Add("SettlementDate", System.Type.GetType("System.String"));
            dtCol = dtCompare.Columns.Add("Depository_ID", System.Type.GetType("System.Int32"));

            //--- show Provider's rows groupped by ClientOrder_ID ---------------------
            for (i = 1; i <= fgProvider.Rows.Count - 1; i++)
            {
                //if (fgProvider[i, "ClientOrder_ID"] + "" == "M010400221017004001731000")
                //    j = j;
                foundRows = dtCompare.Select("ClientOrder_ID = '" + fgProvider[i, "ClientOrder_ID"] + "'");
                if (foundRows.Length == 0)
                {
                    dtRow = dtCompare.NewRow();
                    dtRow["ClientOrder_ID"] = fgProvider[i, "ClientOrder_ID"] + "";
                    dtRow["AktionDate_1"] = Convert.ToDateTime(fgProvider[i, "TradeDate"]).ToString("dd/MM/yyyy");
                    dtRow["AktionDate_2"] = "";
                    dtRow["ExecuteDate_1"] = Convert.ToDateTime(fgProvider[i, "TradeDate"]).ToString("dd/MM/yyyy");
                    dtRow["ExecuteDate_2"] = "";
                    dtRow["ISIN_1"] = fgProvider[i, "ISIN"] + "";
                    dtRow["ISIN_2"] = "";
                    switch (Convert.ToInt32(cmbServiceProviders.SelectedValue))
                    {
                        case 17:                                                           //------------------------------17 - PIREAUS SECURITIES
                            dtRow["Aktion_1"] = (fgProvider[i, "Sign"] + "" == "Α" ? "BUY" : "SELL");
                            break;

                        case 19:                                                           //------------------------------ 19 - INTESA                    
                            dtRow["Aktion_1"] = (fgProvider[i, "Sign"] + "" == "D" ? "BUY" : "SELL");
                            break;

                        case 20:                                                           //------------------------------ 20 - BNP ARBITRAGE                    
                            dtRow["Aktion_1"] = (fgProvider[i, "Sign"] + "" == "BUY" ? "BUY" : "SELL");
                            break;
                    }
                    dtRow["Aktion_2"] = "";
                    dtRow["Currency_1"] = fgProvider[i, "Currency"] + "";
                    dtRow["Currency_2"] = "";
                    dtRow["Quantity_1"] = Convert.ToDecimal(fgProvider[i, "Quantity"]);
                    dtRow["Quantity_2"] = 0;
                    dtRow["Price_1"] = Convert.ToDecimal(fgProvider[i, "Price"]);
                    dtRow["Price_2"] = 0;
                    dtRow["Amount_1"] = Convert.ToDecimal(fgProvider[i, "Quantity"]) * Convert.ToDecimal(fgProvider[i, "Price"]);
                    dtRow["Amount_2"] = 0;
                    dtRow["SE_Code_1"] = fgProvider[i, "SE_Code"] + "";
                    dtRow["SE_Code_2"] = "";
                    dtRow["Depository_1"] = fgProvider[i, "Depository_Code"] + "";
                    dtRow["Depository_2"] = "";
                    dtRow["Fee_1"] = Convert.ToDecimal(fgProvider[i, "Fee"]);
                    dtRow["Fee_2"] = 0;
                    dtRow["MarketFee_1"] = Convert.ToDecimal(fgProvider[i, "MarketFee"]);
                    dtRow["MarketFee_2"] = 0;
                    dtRow["Accrued_1"] = Convert.ToDecimal(fgProvider[i, "Accrued"]);
                    dtRow["Accrued_2"] = 0;
                    dtRow["Commission_1"] = Convert.ToDecimal(fgProvider[i, "Commission"]);
                    dtRow["Commission_2"] = 0;
                    dtRow["ID_1"] = fgProvider[i, "ID"];
                    dtRow["ID_2"] = 0;
                    dtRow["Status_1"] = 0;
                    dtRow["Status_2"] = 0;
                    dtRow["SE_ID_1"] = fgProvider[i, "SE_ID"];
                    dtRow["SE_ID_2"] = 0;
                    dtRow["Check"] = 0;
                    dtRow["TradeTime"] = fgProvider[i, "TradeTime"];
                    dtRow["SettlementDate"] = Convert.ToDateTime(fgProvider[i, "ValueDate"]).ToString("dd/MM/yyyy");
                    dtRow["Depository_ID"] = fgProvider[i, "Depository_ID"];
                    dtCompare.Rows.Add(dtRow);
                }
                else
                {
                    foundRows[0]["Quantity_1"] = Convert.ToDecimal(foundRows[0]["Quantity_1"]) + Convert.ToDecimal(fgProvider[i, "Quantity"]);
                    foundRows[0]["Amount_1"] = Convert.ToDecimal(foundRows[0]["Amount_1"]) + Convert.ToDecimal(fgProvider[i, "Quantity"]) * Convert.ToDecimal(fgProvider[i, "Price"]);
                    if (Convert.ToInt32(cmbServiceProviders.SelectedValue) != 17)  {                                                         // IS NOT 17 - PIREAUS SECURITIES
                        foundRows[0]["Fee_1"] = Convert.ToDecimal(foundRows[0]["Fee_1"]) + Convert.ToDecimal(fgProvider[i, "Fee"]);
                        foundRows[0]["MarketFee_1"] = Convert.ToDecimal(foundRows[0]["MarketFee_1"]) + Convert.ToDecimal(fgProvider[i, "MarketFee"]);
                        foundRows[0]["Accrued_1"] = Convert.ToDecimal(foundRows[0]["Accrued_1"]) + Convert.ToDecimal(fgProvider[i, "Accrued"]);
                        foundRows[0]["Commission_1"] = Convert.ToDecimal(foundRows[0]["Commission_1"]) + Convert.ToDecimal(fgProvider[i, "Commission"]);
                    }
                }
            }

            //--- show HellasFin Execution rows (CommandType_ID = 2) groupped by ClientOrder_ID---------------------
            foreach (DataRow dtRow1 in Orders2.List.Rows)
            {
                //if (dtRow1["ClientOrder_ID"] + "" == "M010400221017004001731000")
                //    j = j;

                foundRows = dtCompare.Select("ClientOrder_ID = '" + dtRow1["ClientOrder_ID"] + "'");

                if (foundRows.Length == 0)
                {
                    dtRow = dtCompare.NewRow();
                    dtRow["ClientOrder_ID"] = dtRow1["ClientOrder_ID"] + "";
                    dtRow["AktionDate_1"] = "";
                    dtRow["AktionDate_2"] = Convert.ToDateTime(dtRow1["AktionDate"]).ToString("dd/MM/yyyy");           
                    dtRow["ExecuteDate_1"] = "";
                    dtRow["ExecuteDate_2"] = Convert.ToDateTime(dtRow1["ExecuteDate"]).ToString("dd/MM/yyyy");         
                    dtRow["ISIN_1"] = "";
                    dtRow["ISIN_2"] = dtRow1["ISIN"] + "";
                    dtRow["Aktion_1"] = "";
                    dtRow["Aktion_2"] = Convert.ToInt32(dtRow1["Aktion"]) == 1 ? "BUY" : "SELL" + "";
                    dtRow["Currency_1"] = "";
                    dtRow["Currency_2"] = dtRow1["Currency"] + "";
                    dtRow["Quantity_1"] = 0;
                    dtRow["Quantity_2"] = Convert.ToDecimal(dtRow1["RealQuantity"]);
                    dtRow["Price_1"] = 0;
                    dtRow["Price_2"] = Convert.ToDecimal(dtRow1["RealPrice"]);
                    dtRow["Amount_1"] = 0;
                    dtRow["Amount_2"] = Convert.ToDecimal(dtRow1["RealQuantity"]) * Convert.ToDecimal(dtRow1["RealPrice"]);
                    dtRow["SE_Code_1"] = "";
                    dtRow["SE_Code_2"] = dtRow1["SE_Code"] + "";
                    dtRow["Depository_1"] = "";
                    dtRow["Depository_2"] = dtRow1["Depository_Code"] + "";
                    dtRow["Fee_1"] = 0;
                    dtRow["Fee_2"] = Convert.ToDecimal(dtRow1["FeesDiff"]);
                    dtRow["MarketFee_1"] = 0;
                    dtRow["MarketFee_2"] = Convert.ToDecimal(dtRow1["FeesMarket"]);
                    dtRow["Accrued_1"] = 0;
                    dtRow["Accrued_2"] = Convert.ToDecimal(dtRow1["AccruedInterest"]);
                    dtRow["Commission_1"] = 0;
                    dtRow["Commission_2"] = Convert.ToDecimal(dtRow1["Commission"]);
                    dtRow["ID_1"] = 0;
                    dtRow["ID_2"] = Convert.ToInt32(dtRow1["ID"]);
                    dtRow["Status_1"] = 0;
                    dtRow["Status_2"] = Convert.ToInt16(dtRow1["Status"]);
                    dtRow["SE_ID_1"] = 0;
                    dtRow["SE_ID_2"] = Convert.ToInt32(dtRow1["SE_ID"]);
                    dtRow["Check"] = 0;
                    dtRow["TradeTime"] = "";
                    dtRow["SettlementDate"] = "";
                    dtRow["Depository_ID"] = 0;
                    dtCompare.Rows.Add(dtRow);
                }
                else
                {
                    foundRows[0]["AktionDate_2"] = Convert.ToDateTime(dtRow1["AktionDate"]).ToString("dd/MM/yyyy");          
                    foundRows[0]["ExecuteDate_2"] = Convert.ToDateTime(dtRow1["ExecuteDate"]).ToString("dd/MM/yyyy");
                    foundRows[0]["ISIN_2"] = dtRow1["ISIN"] + "";
                    foundRows[0]["Aktion_2"] = Convert.ToInt32(dtRow1["Aktion"]) == 1 ? "BUY" : "SELL" + "";
                    foundRows[0]["Currency_2"] = dtRow1["Currency"] + "";
                    foundRows[0]["Quantity_2"] = Convert.ToDecimal(foundRows[0]["Quantity_2"]) + Convert.ToDecimal(dtRow1["RealQuantity"]);
                    foundRows[0]["Price_2"] = Convert.ToDecimal(dtRow1["RealPrice"]);
                    foundRows[0]["Amount_2"] = Convert.ToDecimal(foundRows[0]["Amount_2"]) + Convert.ToDecimal(dtRow1["RealQuantity"]) * Convert.ToDecimal(dtRow1["RealPrice"]);
                    foundRows[0]["SE_Code_2"] = dtRow1["SE_Code"] + "";
                    foundRows[0]["Depository_2"] = dtRow1["Depository_Code"] + "";
                    foundRows[0]["Fee_2"] = Convert.ToDecimal(foundRows[0]["Fee_2"]) + Convert.ToDecimal(dtRow1["FeesDiff"]);
                    foundRows[0]["MarketFee_2"] = Convert.ToDecimal(foundRows[0]["MarketFee_2"]) + Convert.ToDecimal(dtRow1["FeesMarket"]);
                    foundRows[0]["Accrued_2"] = Convert.ToDecimal(foundRows[0]["Accrued_2"]) + Convert.ToDecimal(dtRow1["AccruedInterest"]);
                    foundRows[0]["Commission_2"] = Convert.ToDecimal(foundRows[0]["Commission_2"]) + Convert.ToDecimal(dtRow1["Commission"]);
                    foundRows[0]["ID_2"] = Convert.ToInt32(dtRow1["ID"]);
                    foundRows[0]["Status_2"] = Convert.ToInt16(dtRow1["Status"]);
                    foundRows[0]["SE_ID_2"] = Convert.ToInt32(dtRow1["SE_ID"]);
                }
                bCheckCompare = true;
            }

            //---- define Orders_ProvidersRecs records List -------------
            DefineFinishedRecords();
            foreach (DataRow dtRow in Orders_ProvidersRecs.List.Rows)
            {
                if ((dtRow["RefNumber"] + "").Trim() != "")
                {
                    foundRows = dtCompare.Select("ClientOrder_ID = '" + dtRow["RefNumber"] + "'");
                    if (foundRows.Length > 0)
                    {
                        foundRows[0]["Check"] = 1;
                        foundRows[0]["Status_1"] = 1;
                        foundRows[0]["Status_2"] = 1;
                    }
                }
            }

            iOdd = 0;
            fgCompare.Redraw = false;
            fgCompare.Rows.Count = 1;
            foreach (DataRow dtRow in dtCompare.Rows)
            {
                if (iOdd == 1) iOdd = 0;
                else iOdd = 1;

                if (Convert.ToDecimal(dtRow["Quantity_1"]) != 0)
                    dtRow["Price_1"] = (Convert.ToDecimal(dtRow["Amount_1"]) / Convert.ToDecimal(dtRow["Quantity_1"])).ToString("0.00####");
                else dtRow["Price_1"] = 0;

                dtRow["Price_1"] = Convert.ToDecimal(dtRow["Amount_1"]) / Convert.ToDecimal(dtRow["Quantity_1"]);
                fgCompare.AddItem((Convert.ToInt32(dtRow["Check"]) == 0 ? false : true) + "\t" + dtRow["ClientOrder_ID"] + "\t" + cmbServiceProviders.Text + "\t" +
                                  dtRow["ExecuteDate_1"] + "\t" + dtRow["ISIN_1"] + "\t" + dtRow["Aktion_1"] + "\t" + dtRow["Currency_1"] + "\t" +
                                  Convert.ToDecimal(dtRow["Quantity_1"]).ToString("0.######") + "\t" + Convert.ToDecimal(dtRow["Price_1"]).ToString("0.######") + "\t" + 
                                  dtRow["SE_Code_1"] + "\t" + dtRow["Depository_1"] + "\t" + Convert.ToDecimal(dtRow["Fee_1"]).ToString("0.######") + "\t" + 
                                  Convert.ToDecimal(dtRow["MarketFee_1"]).ToString("0.######") + "\t" + Convert.ToDecimal(dtRow["Accrued_1"]).ToString("0.######") + "\t" +
                                  Convert.ToDecimal(dtRow["Commission_1"]).ToString("0.######") + "\t" + dtRow["ID_1"] + "\t" + dtRow["Status_1"] + "\t" +
                                  iOdd + "\t" + dtRow["TradeTime"] + "\t" + dtRow["AktionDate_1"] + "\t" + dtRow["SettlementDate"] + "\t" + dtRow["SE_ID_1"] + "\t" + 
                                  dtRow["Depository_ID"]);
                dtRow["Price_2"] = Convert.ToDecimal(dtRow["Amount_2"]) / Convert.ToDecimal(dtRow["Quantity_2"]);
                fgCompare.AddItem((Convert.ToInt32(dtRow["Check"]) == 0 ? false : true) + "\t" + dtRow["ClientOrder_ID"] + "\t" + "HellasFin" + "\t" +
                                  dtRow["ExecuteDate_2"] + "\t" + dtRow["ISIN_2"] + "\t" + dtRow["Aktion_2"] + "\t" + dtRow["Currency_2"] + "\t" +
                                  Convert.ToDecimal(dtRow["Quantity_2"]).ToString("0.######") + "\t" + Convert.ToDecimal(dtRow["Price_2"]).ToString("0.######") + "\t" +
                                  dtRow["SE_Code_2"] + "\t" + dtRow["Depository_2"] + "\t" + Convert.ToDecimal(dtRow["Fee_2"]).ToString("0.######") + "\t" +
                                  Convert.ToDecimal(dtRow["MarketFee_2"]).ToString("0.######") + "\t" + Convert.ToDecimal(dtRow["Accrued_2"]).ToString("0.######") + "\t" + 
                                  Convert.ToDecimal(dtRow["Commission_2"]).ToString("0.######") + "\t" + dtRow["ID_2"] + "\t" + dtRow["Status_2"] + "\t" +
                                  iOdd + "\t" + dtRow["TradeTime"] + "\t" + dtRow["AktionDate_2"] + "\t" + dtRow["SettlementDate"] + "\t" + dtRow["SE_ID_2"] + "\t" + 
                                  dtRow["Depository_ID"]);
            }
            fgCompare.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.Free;
            // Merge values in columns 1. 
            fgCompare.Cols[1].AllowMerging = true;
            fgCompare.Redraw = true;
        }
        private void fgCompare_RowColChange(object sender, EventArgs e)
        {
            if (bCheckCompare) {
                if (fgCompare.Row > 0) {
                    i = 0;
                    fgProvider2.Redraw = false;
                    fgProvider2.Rows.Count = 1;
                    foreach (DataRow dtRow in CustodyCommands.List.Rows)
                    {
                        if ((fgCompare[fgCompare.Row, 1] + "") == (dtRow["RefNumber"] + ""))
                        {
                            i = i + 1;
                            fgProvider2.AddItem(i + "\t" + dtRow["Sign"] + "\t" + dtRow["QuantityNominal"] + "\t" + dtRow["Price"] + "\t" +
                                        dtRow["Fees"] + "\t" + dtRow["MarketFee"] + "\t" + dtRow["AccruedInterest"] + "\t" + dtRow["Commission"] + "\t" + 
                                        dtRow["Taxes"] + "\t" + Math.Abs(Convert.ToDecimal(dtRow["SettlementAmount"])) + "\t" + dtRow["ExchangeRate"] + "\t" +
                                        dtRow["SettlementAmountCurr"] + "\t" + dtRow["SettlementPlace"] + "\t" + dtRow["StockExchange_Code"] + "\t" + 
                                        dtRow["Depository_Code"] + "\t" + Convert.ToDateTime(dtRow["TradeDate"]).ToString("yyyy/MM/dd") + "\t" + 
                                        dtRow["TradeTime"] + "\t" + Convert.ToDateTime(dtRow["SettlementDate"]).ToString("yyyy/MM/dd") + "\t" + 
                                        dtRow["TradeCurrency"] + "\t" + dtRow["ISIN"] + "\t" + dtRow["SecurityCode"] + "\t" + dtRow["SecurityDescription"] + "\t" +
                                        dtRow["Market"] + "\t" +  dtRow["RefNumber"] + "\t" + dtRow["ID"] + "\t" + 
                                        dtRow["StockExchange_ID"] + "\t" + dtRow["Depository_ID"]);
                        }
                    }

                    //CellStyle cs;
                    //cs = fgProvider2.Styles[CellStyleEnum.Subtotal0];
                    //cs.BackColor = Color.SteelBlue;
                    //cs.ForeColor = Color.White;
                    fgProvider2.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, -1, 2, "");
                    fgProvider2.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, -1, 4, "");
                    fgProvider2.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, -1, 5, "");
                    fgProvider2.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, -1, 6, "");
                    fgProvider2.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, -1, 7, "");
                    fgProvider2.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, -1, 8, "");
                    fgProvider2.Redraw = true;

                    DefineChildOrders(fgCompare[fgCompare.Row, 1] + "", dAktionDate.Value);

                    i = 0;
                    fgList2.Redraw = false;
                    fgList2.Rows.Count = 1;
                    foreach (DataRow dtRow in dtChildOrders.Rows)
                    {
                        if (Convert.ToDateTime(dtRow["ExecuteDate"]) != Convert.ToDateTime("1900/01/01"))
                        {
                            sTemp = "";
                            if (Convert.ToInt32(dtRow["CommandType_ID"]) != 1) sTemp = dtRow["Aktion"] + "";

                            i = i + 1;
                            fgList2.AddItem(i + "\t" + sTemp + "\t" + dtRow["RealQuantity"] + "\t" + dtRow["RealPrice"] + "\t" +
                                dtRow["FeesDiff"] + "\t" + dtRow["FeesMarket"] + "\t" + dtRow["AccruedInterest"] + "\t" + dtRow["Commission"] + "\t" +
                                dtRow["Currency"] + "\t" + dtRow["ContractTitle"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" +
                                dtRow["ClientFullName"] + "\t" + dtRow["BulkCommand"] + "\t" + dtRow["Share_Title"] + "\t" + dtRow["Share_Code"] + "\t" + dtRow["ISIN"] + "\t" +
                                dtRow["SE_Code"] + "\t" + dtRow["AktionDate"] + "\t" + dtRow["Notes"] + "\t" + dtRow["ID"] + "\t" + dtRow["CommandType_ID"]);
                        }
                    }
                    fgList2.Redraw = true;
                }
            }
        }
        private void fgCompare_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (e.Col == 0) {
                if (Convert.ToInt32(fgCompare[fgCompare.Row, "Status"]) == 0) {
                    e.Cancel = false;
                    sTemp = fgCompare[fgCompare.Row, "ClientOrder_ID"] + "";

                    for (j = 1; j <= fgCompare.Rows.Count - 1; j++)
                        if (fgCompare[j, "ClientOrder_ID"] + "" == sTemp) fgCompare[j, 0] = fgCompare[fgCompare.Row, 0];
                }
                else e.Cancel = true;
            }
            else  e.Cancel = true;
        }
        private void fgCompare_AfterEdit(object sender, RowColEventArgs e)
        {
            if (e.Col == 0) EditFlag(e.Row);
        }
        private void DefineChildOrders(string sClientOrder_ID, DateTime dActionDate)
        {
            int j = 0;
            decMaxQuantity = 0;
            iMaxQuantity_Command_ID = 0;

            dtChildOrders = new DataTable("ChildOrders_List");
            dtCol = dtChildOrders.Columns.Add("CommandType_ID", System.Type.GetType("System.Int32"));
            dtCol = dtChildOrders.Columns.Add("Product_ID", System.Type.GetType("System.Int32"));
            dtCol = dtChildOrders.Columns.Add("Aktion", System.Type.GetType("System.String"));
            dtCol = dtChildOrders.Columns.Add("RealQuantity", System.Type.GetType("System.Decimal"));
            dtCol = dtChildOrders.Columns.Add("RealPrice", System.Type.GetType("System.Decimal"));
            dtCol = dtChildOrders.Columns.Add("BiggestQuantity", System.Type.GetType("System.Int16"));
            dtCol = dtChildOrders.Columns.Add("FeesDiff", System.Type.GetType("System.Decimal"));
            dtCol = dtChildOrders.Columns.Add("FeesMarket", System.Type.GetType("System.Decimal"));
            dtCol = dtChildOrders.Columns.Add("AccruedInterest", System.Type.GetType("System.Decimal"));
            dtCol = dtChildOrders.Columns.Add("Commission", System.Type.GetType("System.Decimal"));            
            dtCol = dtChildOrders.Columns.Add("Currency", System.Type.GetType("System.String"));
            dtCol = dtChildOrders.Columns.Add("ContractTitle", System.Type.GetType("System.String"));
            dtCol = dtChildOrders.Columns.Add("Code", System.Type.GetType("System.String"));
            dtCol = dtChildOrders.Columns.Add("Portfolio", System.Type.GetType("System.String"));
            dtCol = dtChildOrders.Columns.Add("ClientFullName", System.Type.GetType("System.String"));
            dtCol = dtChildOrders.Columns.Add("BulkCommand", System.Type.GetType("System.String"));
            dtCol = dtChildOrders.Columns.Add("Share_Title", System.Type.GetType("System.String"));
            dtCol = dtChildOrders.Columns.Add("Share_Code", System.Type.GetType("System.String"));
            dtCol = dtChildOrders.Columns.Add("ISIN", System.Type.GetType("System.String"));
            dtCol = dtChildOrders.Columns.Add("SE_Code", System.Type.GetType("System.String"));
            dtCol = dtChildOrders.Columns.Add("Depository_Code", System.Type.GetType("System.String"));
            dtCol = dtChildOrders.Columns.Add("AktionDate", System.Type.GetType("System.String"));
            dtCol = dtChildOrders.Columns.Add("ExecuteDate", System.Type.GetType("System.String"));
            dtCol = dtChildOrders.Columns.Add("Notes", System.Type.GetType("System.String"));
            dtCol = dtChildOrders.Columns.Add("ID", System.Type.GetType("System.Int32"));

            foreach (DataRow dtRow in Orders2.List.Rows)
            {
                if ((dtRow["ClientOrder_ID"] + "") == sClientOrder_ID && Convert.ToDateTime(dtRow["ExecuteDate"]) != Convert.ToDateTime("1900/01/01"))
                {
                    AddChildOrderRecord(dtRow);                    

                    Orders = new clsOrdersSecurity();
                    Orders.AktionDate = dActionDate;
                    Orders.BulkCommand = (dtRow["BulkCommand"] + "").Replace("<", "").Replace(">", "");
                    Orders.GetList_BulkCommand();
                    foreach (DataRow dtRow1 in Orders.List.Rows)
                    {
                        if (Convert.ToDateTime(dtRow1["ExecuteDate"]) != Convert.ToDateTime("1900/01/01"))
                        {
                            switch (Convert.ToInt32(dtRow1["CommandType_ID"]))
                            {
                                case 1:
                                    
                                    AddChildOrderRecord(dtRow1);
                                    break;
                                case 4:
                                    sTemp = dtRow1["BulkCommand"] + "";
                                    j = sTemp.IndexOf("/");
                                    if (j >= 0)
                                    {
                                        sTemp = sTemp.Substring(j + 1);
                                        Orders4 = new clsOrdersSecurity();
                                        Orders4.AktionDate = dAktionDate.Value;
                                        Orders4.BulkCommand = sTemp.Replace("<", "").Replace(">", "");
                                        Orders4.GetList_BulkCommand();
                                        foreach (DataRow dtRow2 in Orders4.List.Rows)
                                        {
                                            sTemp = "";
                                            if (Convert.ToInt32(dtRow2["CommandType_ID"]) != 1) sTemp = (Convert.ToInt32(dtRow2["Aktion"]) == 1 ? "BUY" : "SELL");
                                            AddChildOrderRecord(dtRow2);
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }
            }

            if (iMaxQuantity_Command_ID > 0) {
                foundRows = dtChildOrders.Select("ID = " + iMaxQuantity_Command_ID);
                foundRows[0]["BiggestQuantity"] = 1;
            }
        }       
        private void AddChildOrderRecord(DataRow dtRow)
        {
            DataRow dtRow1;
            dtRow1 = dtChildOrders.NewRow();
            dtRow1["ID"] = dtRow["ID"];
            dtRow1["CommandType_ID"] = dtRow["CommandType_ID"];
            dtRow1["Product_ID"] = dtRow["Product_ID"];
            dtRow1["Aktion"] = (Convert.ToInt32(dtRow["Aktion"]) == 1 ? "BUY" : "SELL");
            dtRow1["RealQuantity"] = Convert.ToDecimal(dtRow["RealQuantity"]);            
            dtRow1["RealPrice"] = Convert.ToDecimal(dtRow["RealPrice"]);
            dtRow1["BiggestQuantity"] = 0;
            dtRow1["FeesDiff"] = Convert.ToDecimal(dtRow["FeesDiff"]);
            dtRow1["FeesMarket"] = Convert.ToDecimal(dtRow["FeesMarket"]);
            dtRow1["AccruedInterest"] = Convert.ToDecimal(dtRow["AccruedInterest"]);
            dtRow1["Commission"] = Convert.ToDecimal(dtRow["Commission"]);
            dtRow1["Currency"] = dtRow["Currency"] + "";
            dtRow1["ContractTitle"] = dtRow["ContractTitle"] + "";
            dtRow1["Code"] = dtRow["Code"] + "";
            dtRow1["Portfolio"] = dtRow["Portfolio"] + "";
            dtRow1["ClientFullName"] = dtRow["ClientFullName"] + "";
            dtRow1["BulkCommand"] = dtRow["BulkCommand"] + "";
            dtRow1["Share_Title"] = dtRow["Share_Title"] + "";
            dtRow1["Share_Code"] = dtRow["Share_Code"] + "";
            dtRow1["ISIN"] = dtRow["ISIN"] + "";
            dtRow1["SE_Code"] = dtRow["SE_Code"] + "";
            dtRow1["Depository_Code"] = dtRow["Depository_Code"] + "";
            dtRow1["AktionDate"] = Convert.ToDateTime(dtRow["AktionDate"]);
            dtRow1["ExecuteDate"] = Convert.ToDateTime(dtRow["ExecuteDate"]);
            dtRow1["Notes"] = dtRow["Notes"] + "";
            dtChildOrders.Rows.Add(dtRow1);

            if (Convert.ToInt32(dtRow["CommandType_ID"]) == 1)
               if (Convert.ToDecimal(dtRow["RealQuantity"]) > decMaxQuantity) {
                   decMaxQuantity = Convert.ToDecimal(dtRow["RealQuantity"]);
                   iMaxQuantity_Command_ID = Convert.ToInt32(dtRow["ID"]);
               }
        }
        private void chkMatching_CheckedChanged(object sender, EventArgs e)
        {
            for (i = 1; i <= fgCompare.Rows.Count - 1; i++) {
                if (Convert.ToInt32(fgCompare[i, "Status"]) == 0) {
                    fgCompare[i, "Check"] = chkMatching.Checked;
                    EditFlag(i);
                }
            }
        }
        private void EditFlag(int iRow)
        {
            if ((fgCompare[iRow, "Company"] + "").Trim() == (cmbServiceProviders.Text + "").Trim())  {
                iRow = iRow + 1;
                fgCompare[iRow, 0] = fgCompare[iRow - 1, 0];
            }
            if ((fgCompare[iRow, "Company"] + "").Trim() != (cmbServiceProviders.Text + "").Trim())
            {
                fgCompare[iRow - 1, 0] = fgCompare[iRow, 0];

                if (Convert.ToBoolean(fgCompare[iRow - 1, 0]))
                {
                    fgCompare[iRow, "Fee"] = fgCompare[iRow - 1, "Fee"];
                    fgCompare[iRow, "MarketFee"] = fgCompare[iRow - 1, "MarketFee"];
                    fgCompare[iRow, "Accrued"] = fgCompare[iRow - 1, "Accrued"];
                    fgCompare[iRow, "Commission"] = fgCompare[iRow - 1, "Commission"];
                }
                else
                {
                    fgCompare[iRow, "Fee"] = 0;
                    fgCompare[iRow, "MarketFee"] = 0;
                    fgCompare[iRow, "Accrued"] = 0;
                    fgCompare[iRow, "Commission"] = 0;
                }
            }
        }
        private void fgCompare_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Row > 0) {
                if (e.Col == 16)   {                                                                                              // 16 - Status of FinishAction
                    if (Convert.ToInt32(fgCompare[e.Row, "Status"]) == 1) fgCompare.Rows[e.Row].Style = csFinish;                      
                }
                if (e.Col == 17)   {                                                                                              // 17 - StyleFlag
                    if (Convert.ToInt32(fgCompare[e.Row, "Status"]) != 1)
                       if (Convert.ToInt32(fgCompare[e.Row, "StyleFlag"]) == 1) fgCompare.Rows[e.Row].Style = csOdd;                 
                }
            }
        }
        private void fgCompare_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            if (e.Row % 2 != 0)  {                                                                                             //e.Row > 0 && e.Row < fgCompare.Rows.Count - 1 )

                if (e.Col == 3)                                                                                                // 3 - AktionDate
                    if (fgCompare[e.Row, 3].ToString() != fgCompare[e.Row + 1, 3].ToString()) e.Style = csDiff;
               
                if (e.Col == 4)                                                                                                // 4 - ISIN
                    if (fgCompare[e.Row, 4].ToString() != fgCompare[e.Row + 1, 4].ToString()) e.Style = csDiff;

                if (e.Col == 5)                                                                                                // 5 - Aktion
                    if (fgCompare[e.Row, 5].ToString() != fgCompare[e.Row + 1, 5].ToString()) e.Style = csDiff;

                if (e.Col == 6)                                                                                                // 6 - Currency
                    if (fgCompare[e.Row, 6].ToString() != fgCompare[e.Row + 1, 6].ToString()) e.Style = csDiff;

                if (e.Col == 7)                                                                                                // 7 - Quantity
                    if (Convert.ToDecimal(fgCompare[e.Row, 7]) != Convert.ToDecimal(fgCompare[e.Row + 1, 7])) e.Style = csDiff;

                if (e.Col == 8)                                                                                                // 8 - Price
                    if (Convert.ToDecimal(fgCompare[e.Row, 8]) != Convert.ToDecimal(fgCompare[e.Row+1, 8])) e.Style = csDiff;

                if (e.Col == 9)                                                                                                // 9 - SE_Code
                    if (fgCompare[e.Row, 9].ToString() != fgCompare[e.Row + 1, 9].ToString()) e.Style = csDiff;

                //if (e.Col == 10)                                                                                              // 10 - Depository_Code
                //    if (fgCompare[e.Row, 10].ToString() != fgCompare[e.Row + 1, 10].ToString()) e.Style = csDiff;
            }
        }
        private void fgProvider2_DoubleClick(object sender, EventArgs e)
        {
            iRow = fgProvider2.Row;
            if (iRow > 0) {
                dTrade.Value = Convert.ToDateTime(fgProvider2[iRow, "TradeDate"]);
                sTemp = fgProvider2[iRow, "TradeTime"] + "";
                txtHour.Text = sTemp.Substring(0, 2);
                txtMinute.Text = sTemp.Substring(3, 2);
                txtSecond.Text = sTemp.Substring(6, 2);
                dValue.Value = Convert.ToDateTime(fgProvider2[iRow, "ValueDate"]);
                txtCurrency.Text = fgProvider2[iRow, "Currency"] + "";
                txtISIN.Text = fgProvider2[iRow, "ISIN"] + "";
                txtCode.Text = fgProvider2[iRow, "Code"] + "";
                txtDescription.Text = fgProvider2[iRow, "Description"] + "";
                txtMarket.Text = fgProvider2[iRow, "Market"] + "";
                txtSign.Text = fgProvider2[iRow, "Sign"] + "";

                txtQuantity.Text = fgProvider2[iRow, "Quantity"] + "";
                txtPrice.Text = fgProvider2[iRow, "Price"] + "";
                txtAccrued.Text = fgProvider2[iRow, "Accrued"] + "";

                txtCommission.Text = fgProvider2[iRow, "Commission"] + "";
                txtFee.Text = fgProvider2[iRow, "Fee"] + "";
                txtMarketFee.Text = fgProvider2[iRow, "MarketFee"] + "";
                txtTaxes.Text = fgProvider2[iRow, "Taxes"] + "";

                txtStatAmt_TradeCurr.Text = "";
                txtExchangeRate.Text = "";
                txtStatAmt_SettCurr.Text = "";
                txtPlaceOfSettlement.Text = "";

                txtClientOrder_ID.Text = fgProvider2[iRow, "ClientOrder_ID"] + "";

                panEdit.Left = (Screen.PrimaryScreen.Bounds.Width - panEdit.Width) / 2;
                panEdit.Top = (Screen.PrimaryScreen.Bounds.Height - panEdit.Height) / 2;
                panEdit.Visible = true;
            }
        }
        private void fgList2_DoubleClick(object sender, EventArgs e)
        {
            iRow = fgList2.Row;
            if (iRow > 0) {
                switch (Convert.ToInt32(fgList2[iRow, "CommandType_ID"])) {
                    case 1:
                        frmOrderSecurity locOrderSecurity = new frmOrderSecurity();
                        locOrderSecurity.Rec_ID = Convert.ToInt32(fgList2[iRow, "ID"]);                // Rec_ID != 0     EDIT mode
                        //locOrderSecurity.BusinessType = iBusinessType_ID;
                        locOrderSecurity.RightsLevel = iRightsLevel;
                        locOrderSecurity.Editable = 1;
                        locOrderSecurity.ShowDialog();
                        if (locOrderSecurity.LastAktion == 1)
                        {                                     // Aktion=1        was saved (added)
     
                        }
                        break;
                    case 2:
                        frmOrderExecution locOrderExecution = new frmOrderExecution();
                        locOrderExecution.Rec_ID = Convert.ToInt32(fgList2[iRow, "ID"]);
                        locOrderExecution.CommandType_ID = 2;                                   // 2 - Execution Order
                        locOrderExecution.RightsLevel = iRightsLevel;
                        locOrderExecution.Editable = 1;
                        locOrderExecution.ShowDialog();
                         break;
                    case 3:
                        frmOrderExecution locBulkExecution = new frmOrderExecution();
                        locBulkExecution.Rec_ID = Convert.ToInt32(fgList2[iRow, "ID"]);
                        locBulkExecution.CommandType_ID = 3;                      // 3 - Bulk Order
                        locBulkExecution.RightsLevel = iRightsLevel;
                        locBulkExecution.Editable = 1;
                        locBulkExecution.ShowDialog();
                        break;
                    case 4:
                        frmOrderDPM locOrderDPM = new frmOrderDPM();
                        locOrderDPM.Rec_ID = Convert.ToInt32(fgList2[iRow, "ID"]);
                        locOrderDPM.CommandType_ID = 4;                           // 4 - DPM Order
                        locOrderDPM.RightsLevel = iRightsLevel;
                        locOrderDPM.Editable = 1;
                        locOrderDPM.ShowDialog();
                        break;
                }
            }
        }
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            panEdit.Left = (Screen.PrimaryScreen.Bounds.Width - panEdit.Width) / 2;
            panEdit.Top = (Screen.PrimaryScreen.Bounds.Height - panEdit.Height) / 2;
            panEdit.Visible = true;
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            string sTemp = "";

            if (fgProvider.Row > 0)
            {
                iRow = fgProvider.Row;
                dTrade.Value = Convert.ToDateTime(fgProvider[iRow, "TradeDate"]);
                sTemp = fgProvider[iRow, "TradeTime"] + "";
                if (sTemp != "")   {
                    txtHour.Text = sTemp.Substring(0,2);
                    txtMinute.Text = sTemp.Substring(3, 2);
                    txtSecond.Text = sTemp.Substring(6, 2);
                }
                else {
                    txtHour.Text = "";
                    txtMinute.Text = "";
                    txtSecond.Text = "";
                }
                dValue.Value = Convert.ToDateTime(fgProvider[iRow, "ValueDate"]);
                txtCurrency.Text = fgProvider[iRow, "Currency"] + "";
                txtISIN.Text = fgProvider[iRow, "ISIN"] + "";
                txtCode.Text = fgProvider[iRow, "Code"] + "";
                txtDescription.Text = fgProvider[iRow, "Description"] + "";
                txtMarket.Text = fgProvider[iRow, "Market"] + "";
                txtSign.Text = fgProvider[iRow, "Sign"] + "";
                txtQuantity.Text = fgProvider[iRow, "Quantity"] + "";
                txtPrice.Text = fgProvider[iRow, "Price"] + "";
                txtAccrued.Text = fgProvider[iRow, "Accrued"] + "";
                txtCommission.Text = fgProvider[iRow, "Commission"] + "";
                txtFee.Text = fgProvider[iRow, "Fee"] + "";
                txtMarketFee.Text = fgProvider[iRow, "MarketFee"] + "";
                txtTaxes.Text = fgProvider[iRow, "Taxes"] + "";
                txtStatAmt_TradeCurr.Text = fgProvider[iRow, "Amount_SettleCurrency"] + "";
                txtExchangeRate.Text = fgProvider[iRow, "ExchangeRate"] + "";
                txtStatAmt_SettCurr.Text = fgProvider[iRow, "SettlementAmountCurr"] + "";
                txtPlaceOfSettlement.Text = fgProvider[iRow, "PlaceOfSettlement"] + "";
                txtClientOrder_ID.Text = fgProvider[iRow, "ClientOrder_ID"] + "";

                panEdit.Left = (Screen.PrimaryScreen.Bounds.Width - panEdit.Width) / 2;
                panEdit.Top = (Screen.PrimaryScreen.Bounds.Height - panEdit.Height) / 2;
                panEdit.Visible = true;
            }
        }

        private void tsbSave_Edit_Click(object sender, EventArgs e)
        {
            clsCustodyCommands CustodyCommand = new clsCustodyCommands();
            CustodyCommand.Record_ID = Convert.ToInt32(fgProvider2[fgProvider2.Row, "ID"]);
            CustodyCommand.GetRecord();
            CustodyCommand.RefNumber = txtClientOrder_ID.Text;
            CustodyCommand.EditRecord();
            panEdit.Visible = false;
            DefineList();
            DefineFinishedRecords();
            Matching();
        }
        private void DefineFinishedRecords()
        {
            i = 0;
            fgFinish.Redraw = false;
            fgFinish.Rows.Count = 1;

            Orders_ProvidersRecs = new clsOrders_ProvidersRecs();
            Orders_ProvidersRecs.StockCompany_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
            Orders_ProvidersRecs.TradeDate = dAktionDate.Value;
            Orders_ProvidersRecs.GetList();
            foreach (DataRow dtRow in Orders_ProvidersRecs.List.Rows)
            {
                i = i + 1;
                fgFinish.AddItem(false + "\t" + i + "\t" + dtRow["CompanyCode"] + "\t" + Convert.ToDateTime(dtRow["TradeDate"]).ToString("dd/MM/yyyy") + "\t" + 
                                 dtRow["TradeTime"] + "\t" + Convert.ToDateTime(dtRow["SettlementDate"]).ToString("dd/MM/yyyy") + "\t" + dtRow["Command_ID"] + "\t" + 
                                 dtRow["Aktion"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" + dtRow["ContractTitle"] + "\t" + dtRow["SecurityCode"] + "\t" + 
                                 dtRow["ISIN"] + "\t" + dtRow["SecurityDescription"] + "\t" + Convert.ToDecimal(dtRow["Quantity"]).ToString("0.######") + "\t" + 
                                 Convert.ToDecimal(dtRow["Price"]).ToString("0.00####") + "\t" + dtRow["TradeCurrency"] + "\t" + 
                                 Convert.ToDecimal(dtRow["AccruedInterest"]).ToString("0.00####") + "\t" + Convert.ToDecimal(dtRow["MarketFee"]).ToString("0.00####") + "\t" + 
                                 dtRow["StockExchange_Code"] + "\t" + dtRow["Depository_Code"] + "\t" + dtRow["SettlementCurrency"] + "\t" +
                                 Convert.ToDecimal(dtRow["CurrencyRate"]).ToString("0.00####") + "\t" + dtRow["Notes"] + "\t" +
                                 Convert.ToDecimal(dtRow["Fee"]).ToString("0.00####") + "\t" + dtRow["RefNumber"] + "\t" + 
                                 Convert.ToDecimal(dtRow["Commission"]).ToString("0.00####") + "\t" + dtRow["ID"] + "\t" + dtRow["Contract_ID"] + "\t" + 
                                 dtRow["Contract_Details_ID"] + "\t" + dtRow["Contract_Packages_ID"] + "\t" + dtRow["ShareCodes_ID"] + "\t" +
                                 dtRow["StockCompany_ID"] + "\t" + dtRow["Depository_ID"] + "\t" + dtRow["Custodian_ID"] + "\t" + dtRow["StockExchange_ID"]);
            }
            fgFinish.Redraw = true;
        }
        private void picClose_Edit_Click(object sender, EventArgs e)
        {
            panEdit.Visible = false;
        }
        public int RightsLevel { get { return iRightsLevel; } set { iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
