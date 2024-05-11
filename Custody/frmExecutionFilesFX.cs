using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using Excel = Microsoft.Office.Interop.Excel;
using C1.Win.C1FlexGrid;
using Core;

namespace Custody
{
    public partial class frmExecutionFilesFX : Form
    {
        DataTable dtList, dtCompare, dtChildOrders, dtDepositories_Alias;
        DataView dtView;
        DataColumn dtCol;
        DataRow dtRow, dtRow1;
        DataRow[] foundRows;
        int i, j, iRow, iOdd, iStockExchange_ID, iDepository_ID, iCommand_Executions_ID, iRightsLevel, iWarning, iMaxQuantity_Command_ID;
        decimal decMaxQuantity;
        string sTemp, sTradeTime, sAktion, sEffectCode, sWarning, sExtra;
        bool bCheckList, bWarning, bEmptyClientOrderID, bCheckCompare;

        private void chkFinish_CheckedChanged(object sender, EventArgs e)
        {
            for (i = 1; i <= fgList.Rows.Count - 1; i++) fgList[i, 0] = chkExport.Checked;
        }

        DateTime dTemp, dTradeDate, dSettlementDate;
        CellStyle csExported, csOdd, csFinish, csDiff, csTotal;
        clsServiceProviders ServiceProviders = new clsServiceProviders();
        clsOrdersFX OrdersFX = new clsOrdersFX();
        clsOrdersSecurity Orders = new clsOrdersSecurity();
        clsOrdersSecurity Orders2 = new clsOrdersSecurity();
        clsOrdersSecurity Orders4 = new clsOrdersSecurity();
        clsOrders_ProvidersRecs Orders_ProvidersRecs = new clsOrders_ProvidersRecs();
        public frmExecutionFilesFX()
        {
            InitializeComponent();
        }

        private void frmExecutionFilesFX_Load(object sender, EventArgs e)
        {
            bCheckList = false;
            bWarning = false;
            bCheckCompare = false;

            btnSearch.Enabled = false;
            dAktionDate.Value = DateTime.Now.AddDays(-1);

            csExported = fgList.Styles.Add("Exported");
            csExported.BackColor = Color.LightGreen;

            csDiff = fgList.Styles.Add("Buy");
            csDiff.BackColor = Color.LightCoral;

            dtView = Global.dtServiceProviders.Copy().DefaultView;
            dtView.RowFilter = "ProviderType = 0 OR ProviderType = 3";
            cmbServiceProviders.DataSource = dtView;
            cmbServiceProviders.DisplayMember = "Title";
            cmbServiceProviders.ValueMember = "ID";

            //------- fgProvider ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgList.OwnerDrawCell += fgList_OwnerDrawCell;
            fgList.DrawMode = DrawModeEnum.OwnerDraw;
            
            bCheckList = true;
        }
        protected override void OnResize(EventArgs e)
        {
            panCritiries.Width = this.Width - 30;
            btnSearch.Left = this.Width - 150;

            fgList.Width = this.Width - 12;
            fgList.Height = this.Height - 64;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DefineList();
        }
        private void DefineList()
        {
            switch (Convert.ToInt32(cmbServiceProviders.SelectedValue))
            {
                case 14:                                                                  // 14 - BMP Paribas
                    sEffectCode = "1";
                    break;
                case 16:                                                                  // 16 -  SocGen
                    sEffectCode = "1";
                    break;
                case 17:                                                                  // 17 - Pireaus
                    sEffectCode = "68";
                    break;
                case 19:                                                                  // 19 - INTESA
                    sEffectCode = "81";
                    break;
            }

            fgList.Redraw = false;
            fgList.Rows.Count = 1;

            if (Convert.ToInt32(cmbServiceProviders.SelectedValue) != 0)
            {

                //--- define CustodyCommands list -----------------------------------------------------
                OrdersFX = new clsOrdersFX();
                OrdersFX.StockCompany_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                OrdersFX.DateFrom = dAktionDate.Value;
                OrdersFX.DateTo = dAktionDate.Value;
                OrdersFX.GetList_Effect();
                  
                i = 0;
                fgList.Redraw = false;
                fgList.Rows.Count = 1;
                foreach (DataRow dtRow in OrdersFX.List.Rows)
                {
                    sTemp = "";
                    if ((dtRow["ValueDate"]+"") != "")
                        if (Convert.ToDateTime(dtRow["ValueDate"]) != Convert.ToDateTime("1900/01/01"))
                            sTemp = Convert.ToDateTime(dtRow["ValueDate"]).ToString("yyyy/MM/dd");

                    i = i + 1;
                    fgList.AddItem(false + "\t" + i + "\t" + sEffectCode + "\t" + Convert.ToDateTime(dtRow["ExecuteDate"]).ToString("yyyy/MM/dd") + "\t" + 
                                       Convert.ToDateTime(dtRow["ExecuteDate"]).ToString("HH:mm:ss") + "\t" + sTemp + "\t" + dtRow["ID"] + "\t" +
                                       dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" + dtRow["ClientName"] + "\t" + dtRow["CurrFrom"] + "\t" +
                                       dtRow["RealAmountFrom"] + "\t" + ((dtRow["AmountFrom"]+"") == "0" ? dtRow["FeesAmount"] : "") + "\t" + dtRow["CurrTo"] + "\t" +
                                       dtRow["RealAmountTo"] + "\t" + ((dtRow["AmountTo"]+"") == "0" ? dtRow["FeesAmount"] : "") + "\t" +
                                       (Convert.ToSingle(dtRow["RealCurrRate"]) == 0 ? "" : dtRow["RealCurrRate"]));
                }
                fgList.Redraw = true;
            }
        }
        private void fgList_OwnerDrawCell(object sender, OwnerDrawCellEventArgs e)
        {
            if (e.Row > 0)
            {
                if (e.Col == 22 && (fgList[e.Row, "ClientOrder_ID"] + "") == "") e.Style = csDiff;                   // 22 - ClientOrder_ID
                if (e.Col == 23 && Convert.ToInt32(fgList[e.Row, "SE_ID"]) == 0) e.Style = csDiff;                   // 23 - SE_Code (StockExchange_Code)
                if (e.Col == 24 && Convert.ToInt32(fgList[e.Row, "Depository_ID"]) == 0) e.Style = csDiff;           // 24 - PSET
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
        private void tsbEffect_Click(object sender, EventArgs e)
        {
            int j = 1;
            string sTemp = "";

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
            EXL.Cells[1, 4].Value = "Value Date";
            EXL.Cells[1, 5].Value = "N";
            EXL.Cells[1, 6].Value = "Κωδικός πελάτη";
            EXL.Cells[1, 7].Value = "Portfolio";
            EXL.Cells[1, 8].Value = "Όνομα Πελάτη";
            EXL.Cells[1, 9].Value = "Νομισμα ΠΩΛΗΣΗΣ";
            EXL.Cells[1, 10].Value = "ΠΩΛΗΣΗ";
            EXL.Cells[1, 11].Value = "Προμήθεια ΠΩΛΗΣΗΣ";
            EXL.Cells[1, 12].Value = "Νομισμα ΑΓΟΡΑΣ";
            EXL.Cells[1, 13].Value = "ΑΓΟΡΑ";
            EXL.Cells[1, 14].Value = "Προμήθεια ΑΓΟΡΑΣ";
            EXL.Cells[1, 15].Value = "Rate";

            var loopTo = fgList.Rows.Count - 1;
            for (this.i = 1; this.i <= loopTo; this.i++)
            {
                if (Convert.ToBoolean(fgList[i, 0])) {
                    j = j + 1;
                    EXL.Cells[j, 1].Value = fgList[i, 2];
                    EXL.Cells[j, 2].Value = Convert.ToDateTime(fgList[i, 3]).ToString("yyyy/MM/dd");
                    EXL.Cells[j, 3].Value = fgList[i, 4];
                    EXL.Cells[j, 4].Value = fgList[i, 5];
                    EXL.Cells[j, 5].Value = fgList[i, 6];
                    EXL.Cells[j, 6].Value = fgList[i, 7];
                    EXL.Cells[j, 7].Value = fgList[i, 8];
                    EXL.Cells[j, 8].Value = fgList[i, 9];
                    EXL.Cells[j, 9].Value = fgList[i, 10];

                    sTemp = fgList[i, 11] + "";
                    EXL.Cells[j, 10].Value = Convert.ToSingle(sTemp.Replace(".", ""));

                    sTemp = fgList[i, 12] + "";
                    if (sTemp.Length > 0) EXL.Cells[j, 11].Value = Convert.ToSingle(sTemp);

                    EXL.Cells[j, 12].Value = fgList[i, 13] + "";

                    sTemp = fgList[i, 14] + "";
                    EXL.Cells[j, 13].Value = Convert.ToSingle(sTemp.Replace(".", ""));

                    sTemp = fgList[i, 15]+"";
                    if (sTemp.Length > 0) EXL.Cells[j, 14].Value = Convert.ToSingle(sTemp);

                    EXL.Cells[j, 15].Value = (fgList[i, 16]+"").Replace(",", ".");
                }
            }

            EXL.ScreenUpdating = true;
            EXL.Visible = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
        }
        private void tsbImport_Click(object sender, EventArgs e)
        {

            bEmptyClientOrderID = false;
            iWarning = 0;

            frmImportData locImportData = new frmImportData();

            switch (Convert.ToInt32(cmbServiceProviders.SelectedValue))
            {
                case 17:                                                           //------------------------------17 - PIREAUS SECURITIES
                    locImportData.FileType = 2;                                    // .csv file
                    locImportData.Shema = 25;
                    locImportData.ReadMode = 2;
                    locImportData.ShowDialog();
                    if (locImportData.Aktion == 1)
                    {
                        dtList = locImportData.Result;

                        bEmptyClientOrderID = false;
                        foreach (DataRow dtRow in dtList.Rows)
                        {
                            if ((dtRow["f18"] + "").Trim() == "") bEmptyClientOrderID = true;

                            iStockExchange_ID = 0;
                            foundRows = Global.dtStockExchanges.Select("Code = '" + dtRow["f19"] + "'");
                            if (foundRows.Length > 0) iStockExchange_ID = Convert.ToInt32(foundRows[0]["ID"]);

                            iDepository_ID = 0;
                            foundRows = dtDepositories_Alias.Select("Code = '" + dtRow["f20"] + "'");
                            if (foundRows.Length > 0) iDepository_ID = Convert.ToInt32(foundRows[0]["Item_ID"]);   
                        }
                    }
                    break;

                case 19:                                                           //------------------------------ 19 - INTESA                    
                    locImportData.FileType = 1;                                    // .xls file
                    locImportData.Shema = 27;
                    locImportData.ReadMode = 2;
                    locImportData.ShowDialog();
                    if (locImportData.Aktion == 1)
                    {

                        dtList = locImportData.Result;

                        bEmptyClientOrderID = false;
                        foreach (DataRow dtRow in dtList.Rows)
                        {
                            if ((dtRow["f23"] + "").Trim() == "") bEmptyClientOrderID = true;                               // 23 - ClientOrder_ID

                            iStockExchange_ID = 0;
                            foundRows = Global.dtStockExchanges.Select("Code = '" + dtRow["f24"] + "'");
                            if (foundRows.Length > 0) iStockExchange_ID = Convert.ToInt32(foundRows[0]["ID"]);

                            iDepository_ID = 0;
                            foundRows = dtDepositories_Alias.Select("Code = '" + dtRow["f25"] + "'");
                            if (foundRows.Length > 0) iDepository_ID = Convert.ToInt32(foundRows[0]["Item_ID"]);

             
                        }
                    }
                    break;
            }
            DefineList();
        }
  
        public int RightsLevel { get { return iRightsLevel; } set { iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
