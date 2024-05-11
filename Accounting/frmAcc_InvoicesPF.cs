using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Globalization;
using System.Windows.Forms;
using System.Drawing;
using C1.Win.C1FlexGrid;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections.Generic;
using Core;
namespace Accounting
{ 
    public partial class frmAcc_InvoicesPF : Form
    {
        DataView dtView;
        DataRow[] foundRows;
        int i, j, iClient_ID, iID, iPT_ID, iPF_Semestr, iRightsLevel, iCopies, iInvoice_Type,
            iInvoiceFisiko, iInvoiceNomiko, iInvoicePistotikoFisiko, iInvoicePistotikoNomiko, iInvoiceAkyrotiko;
        int iService_ID, iContract_ID, iContract_Details_ID, iContract_Packages_ID, iServiceProvider_ID, iClientTipos;
        string sTemp = "", sInvoicePFTemplate, sInvoicePrinter, sSeiraFisiko, sSeiraNomiko, sCodeAkyrotiko,
            sSeiraPistotikoFisiko, sSeiraPistotikoNomiko, sSeiraAkyrotiko, sPrinter, sContractTitle, sParast, sExtra, tmpArray, sInvoiceCodeFisiko = "",
            sInvTitleNomikoGr = "", sInvTitleNomikoEn = "", sInvoiceCodeNomiko = "", sInvoiceTypeFisiko = "", sInvoiceTypeNomiko = "",
            sInvoiceFXTemplate = "", sInvTitleFisikoGr = "", sInvTitleFisikoEn = "",
       sInvoiceCodePistotikoFisiko = "", sInvTitlePistotikoFisikoGr = "", sInvTitlePistotikoFisikoEn = "", sInvoiceTypePistotikoFisiko = "",
       sInvoiceCodePistotikoNomiko = "", sInvTitlePistotikoNomikoGr = "", sInvTitlePistotikoNomikoEn = "", sInvoiceTypePistotikoNomiko = "",
       sInvoiceCodeAkyrotiko = "", sInvTitleAkyrotikoGr = "", sInvTitleAkyrotikoEn = "", sInvoiceTypeAkyrotiko = "";

        private void chkPrint_CheckedChanged(object sender, EventArgs e)
        {
            for (i = 1; i <= fgList.Rows.Count - 1; i++) fgList[i, 0] = chkPrint.Checked;
        }

        Global.ContractData stContractData;
        DateTime dIssueDate, dStart, dFinish;
        bool bCheckList;
        CellStyle csCancel, csChecked, csFound;
        Hashtable imgMap = new Hashtable();
        clsContracts klsContract = new clsContracts();
        clsClients klsClient = new clsClients();
        clsPerformanceFees_Titles klsPerformanceFees_Titles = new clsPerformanceFees_Titles();
        clsPerformanceFees_Recs klsPerformanceFees_Recs = new clsPerformanceFees_Recs();
        clsOptions klsOptions = new clsOptions();
        public frmAcc_InvoicesPF()
        {
            InitializeComponent();
        }
        private void frmAcc_InvoicesPF_Load(object sender, EventArgs e)
        {
            bCheckList = false;
            sSeiraFisiko = "";
            sSeiraNomiko = "";
            sSeiraPistotikoFisiko = "";
            sSeiraPistotikoNomiko = "";
            sSeiraAkyrotiko = "";

            panTools.Visible = false;
            chkPrint.Visible = false;
            fgList.Visible = false;

            csCancel = fgList.Styles.Add("Cancelled");
            csCancel.ForeColor = Color.Red;

            csFound = fgList.Styles.Add("FinishAmount");
            csFound.BackColor = Color.LimeGreen;

            csChecked = fgList.Styles.Add("Checked");
            csChecked.BackColor = Color.Yellow;

            for (i = 0; i < imgFiles.Images.Count; i++) imgMap.Add(i, imgFiles.Images[i]);

            for (i = 2010; i <= DateTime.Now.Year; i++)  cmbYear.Items.Add(i);
            cmbYear.SelectedItem = DateTime.Now.Year;

            if (DateTime.Now.Month <= 6) rb1.Checked = true;
            else rb2.Checked = true;

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.Focus.BackColor = Global.GridHighlightForeColor;
            fgList.Styles.ParseString(Global.GridStyle);
            fgList.DrawMode = DrawModeEnum.OwnerDraw;
            fgList.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgList_BeforeEdit);
            fgList.DoubleClick += new System.EventHandler(fgList_DoubleClick);
            fgList.Click += new System.EventHandler(fgList_Click);
            fgList.MouseDown += new MouseEventHandler(fgList_MouseDown);
            fgList.ShowCellLabels = true;

            Column clm1 = fgList.Cols["image_map"];
            clm1.ImageMap = imgMap;
            clm1.ImageAndText = false;
            clm1.ImageAlign = ImageAlignEnum.CenterCenter;


            //-------------- Define Advisorys List ------------------  
            dtView = Global.dtUserList.Copy().DefaultView;
            dtView.RowFilter = "Chief = 1";
            cmbAdvisors.DataSource = dtView;
            cmbAdvisors.DisplayMember = "Title";
            cmbAdvisors.ValueMember = "ID";

            DefineOptions();

            if (Global.User_ID == 1) {               //@@@@@@@@@@@@@@@@@@@@@@@@@@@
                if (Global.UserStatus == 1) {
                    cmbAdvisors.SelectedValue = 0;
                    cmbAdvisors.Enabled = true;
                }
                else {
                    cmbAdvisors.Enabled = false;
                    cmbAdvisors.SelectedValue = Global.User_ID;
                }
            }

            tscbFilter.SelectedIndex = 0;
            bCheckList = true;

            ucCS.StartInit(620, 400, 240, 20, 1);
            ucCS.TextOfLabelChanged += new EventHandler(ucCS_TextChanged);
            ucCS.Filters = " Contract_ID > 0";
            ucCS.Mode = 1;
            ucCS.ListType = 1;

            j = -1;
            //ReDim stPF(-1);
        }
        protected override void OnResize(EventArgs e)
        {
            panCritiries.Width = this.Width - 30;
            btnSearch.Left = panCritiries.Width - 120;

            fgList.Height = this.Height - 140;
            fgList.Width = this.Width - 30;
            panTools.Width = this.Width - 30;

            panEdit.Left = (Screen.PrimaryScreen.Bounds.Width - panEdit.Width) / 2;
            panEdit.Top = (Screen.PrimaryScreen.Bounds.Height - panEdit.Height) / 2;
        }
        //--- define fgList rows -----------------------------------------------------------------------
        private void btnSearch_Click(object sender, EventArgs e)
        {
            panTools.Visible = false;
            chkPrint.Visible = false;
            fgList.Visible = false;

            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            if (rb1.Checked) {
                iPF_Semestr = 1;

                dStart = Convert.ToDateTime("01-01-" + cmbYear.Text);
                dFinish = Convert.ToDateTime("30-06-" + cmbYear.Text);
            }
            else {
                if (rb2.Checked) {
                    iPF_Semestr = 2;

                    dStart = Convert.ToDateTime("01-07-" + cmbYear.Text);
                    dFinish = Convert.ToDateTime("31-12-" + cmbYear.Text);
                }
            }

            //---- Define FT_ID ---------------------
            klsPerformanceFees_Titles = new clsPerformanceFees_Titles();
            klsPerformanceFees_Titles.PF_Year = Convert.ToInt32(cmbYear.Text);
            klsPerformanceFees_Titles.PF_Semestr = iPF_Semestr;
            klsPerformanceFees_Titles.GetRecord_Title();
            iPT_ID = klsPerformanceFees_Titles.Record_ID;

            if (iPT_ID == 0) {
                if (MessageBox.Show("ΠΡΟΣΟΧΗ!  Νέο εξάμηνο.\n Είστε σίγουρος για αυτό;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {                                                                                                                                                                 // ADD Point
                    this.Text = "Performance Fees. Νέες Χρεώσεις";
                    toolLeft.Left = 4;
                    toolLeft.Width = 450;
                    toolLeft.Visible = true;
                    tscbFilter.Visible = true;

                    klsPerformanceFees_Titles = new clsPerformanceFees_Titles();
                    klsPerformanceFees_Titles.PF_Semestr = iPF_Semestr;
                    klsPerformanceFees_Titles.PF_Year = Convert.ToInt32(cmbYear.Text);
                    klsPerformanceFees_Titles.DateIns = DateTime.Now;
                    klsPerformanceFees_Titles.Author_ID = Global.User_ID;
                    iPT_ID = klsPerformanceFees_Titles.InsertRecord();
                }
            }
            toolLeft.Left = 4;
            toolLeft.Width = 590;
            toolLeft.Visible = true;
            tscbFilter.Visible = true;
            DefineList();
            ShowList();

            this.Cursor = Cursors.Default;

            panTools.Visible = true;
            chkPrint.Visible = true;
            fgList.Visible = true;
        }
        private void DefineList()
        {
            klsPerformanceFees_Recs.PT_ID = iPT_ID;
            klsPerformanceFees_Recs.GetList();
        }
        private void ShowList()
        {
            if (bCheckList)
            {
                fgList.Redraw = false;
                fgList.Rows.Count = 1;
                int i = 0;

                foreach (DataRow dtRow in klsPerformanceFees_Recs.List.Rows)
                {
                    if (((Convert.ToInt32(cmbAdvisors.SelectedValue) == 0) || (Convert.ToInt32(dtRow["User1_ID"]) == Convert.ToInt32(cmbAdvisors.SelectedValue))) &&
                       (txtCode.Text.Trim() == "" || dtRow["Code"].ToString().Contains(txtCode.Text))) {
                        i = i + 1;
                        fgList.AddItem(false + "\t" + Convert.ToInt16(dtRow["ImageType"]) + "\t" + i + "\t" + dtRow["DateFrom"] + "\t" + dtRow["DateTo"] + "\t" + dtRow["ContractTitle"] + "\t" +
                                       dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" + dtRow["StartPeriod"] + "\t" + dtRow["EndPeriod"] + "\t" + dtRow["Days"] + "\t" +
                                       dtRow["BMV"] + "\t" + dtRow["EMV"] + "\t" + dtRow["NetFlows"] + "\t" + dtRow["AverageInvestedCapital"] + "\t" + dtRow["HWM_StartPeriod"] + "\t" +
                                       dtRow["AdjustedAssetValue"] + "\t" + dtRow["Index_Y"] + "\t" + dtRow["Index_P"] + "\t" + dtRow["AmoiviHF"] + "\t" + dtRow["Discount_Percent"] + "\t" +
                                       dtRow["FinishAmoiviHF"] + "\t" + dtRow["PerformanceResult"] + "\t" + dtRow["PerformanceIndex"] + "\t" + dtRow["NetPerformance"] + "\t" +
                                       dtRow["HWM"] + "\t" + dtRow["MWR"] + "\t" + dtRow["NetAmount"] + "\t" + dtRow["VAT_Amount"] + "\t" + dtRow["FinishAmount"] + "\t" +
                                       dtRow["HWM_EndPeriod"] + "\t" + dtRow["Invoice_Num"] + "\t" + dtRow["Currency"] + "\t" + dtRow["Package_Title"] + "\t" +
                                       dtRow["Service_Title"] + "\t" + dtRow["InvestmentProfile"] + "\t" + dtRow["Advisory_Name"] + "\t" +
                                       dtRow["RM_Name"] + "\t" + dtRow["Introducer_Name"] + "\t" + dtRow["Diaxiristis_Name"] + "\t" + dtRow["User1_Name"] + "\t" + 
                                       dtRow["Address"] + "\t" + dtRow["City"] + "\t" + dtRow["Zip"] + "\t" + dtRow["Country_Title"] + "\t" +
                                       dtRow["AFM"] + "\t" + dtRow["DOY"] + "\t" + dtRow["ID"] + "\t" + dtRow["ClientType"] + "\t" + dtRow["Client_ID"] + "\t" +
                                       dtRow["Invoice_ID"] + "\t" + dtRow["Invoice_Type"] + "\t" + dtRow["VAT_Percent"] + "\t" + dtRow["Contract_ID"] + "\t" +
                                       dtRow["Contract_Details_ID"] + "\t" + dtRow["Contract_Packages_ID"] + "\t" + dtRow["Service_ID"] + "\t" + dtRow["Status"] + "\t" +
                                       dtRow["User1_ID"] + "\t" + dtRow["Invoice_File"] + "\t" + dtRow["User_ID"] + "\t" + dtRow["MIFID_2"] + "\t" + dtRow["CountryEnglish"]);
                    }
                }
                fgList.Redraw = true;
                DefineSums();
            }
        }
        private void DefineSums()
        {
            fgList.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, -1, 26, "");
            fgList.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, -1, 27, "");
            fgList.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, -1, 28, "");
        }
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            dFrom.Value = dStart;
            dTo.Value = dFinish;
            ucCS.ShowClientsList = false;            
            ucCS.txtContractTitle.Text = "";
            ucCS.ShowClientsList = true;
            lblCode.Text = "";
            lblPortfolio.Text = "";
            lblPackage.Text = "";
            lblCurrency.Text = "";
            lblDays.Text = "0";
            dStartPeriod.Value = dStart;
            dEndPeriod.Value = dFinish;            
            txtBMV.Text = "0";
            txtEMV.Text = "0";
            txtNetFlows.Text = "0";
            txtAIC.Text = "0";
            txtHWM_StartPeriod.Text = "0";
            txtAAV.Text = "0";
            txtDYY_Y.Text = "0";
            txtDYY_Period.Text = "0";
            txtAHF.Text = "0";
            txtDiscount.Text = "0";
            txtFinishAmoivi.Text = "0";
            txtPerformanceResult.Text = "0";
            txtDYY.Text = "0";
            txtNetPerformance.Text = "0";
            txtAAV_HW.Text = "0";
            txtMWR.Text = "0";
            txtNetAmount.Text = "0";
            txtVAT_Percent.Text = "0";
            lblVAT_Amount.Text = "0";
            txtFinishAmount.Text = "0";
            txtHWM_EndPeriod.Text = "0";
            panEdit.Visible = true;
            ucCS.Filters = " Contract_ID > 0";
        }
        private void fgList_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (bCheckList)
            {
                if (e.Col == 0) e.Cancel = false;
                else e.Cancel = true;
            }
        }
        private void fgList_DoubleClick(object sender, EventArgs e)
        {
            if (fgList.Row > 0)
            {
                if (fgList.Col == 1) ShowInvoice();
                else EditRec();
            }
        }
        private void fgList_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 0)
            {
                if (fgList.Col == 0)
                {
                    if (Convert.ToBoolean(fgList[fgList.Row, 0]))
                    {
                        if (fgList[fgList.Row, "FileName"].ToString() != "")
                        {
                            fgList[fgList.Row, 0] = false;
                            MessageBox.Show("Invoice was issued", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
        }
        private void fgList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                fgList.ContextMenuStrip = mnuContext;
                fgList.Row = fgList.MouseRow;
            }
        }
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 0 && fgList.Row < fgList.Rows.Count - 1) EditRec();
        }
        private void EditRec()
        {
            /*
            if (iMode < 4) {
                if stPF(j).Invoice_Type = 4 Then
                    iMode = 4
                Else
                If stPF(j).Invoice_Type = 5 Then
                    iMode = 5
                    End If
                End If
            }
            */
            j = fgList.Row;
            //iAktion = 1;
            dFrom.Value = Convert.ToDateTime(fgList[j, "DateFrom"]);
            dTo.Value = Convert.ToDateTime(fgList[j, "DateTo"]);
            ucCS.ShowClientsList = false;
            ucCS.txtContractTitle.Text = fgList[j, "ContractTitle"] + "";
            ucCS.Filters = " Contract_ID > 0";
            ucCS.ShowClientsList = true;
            lblCode.Text = fgList[j, "Code"] + "";
            lblPortfolio.Text = fgList[j, "Portfolio"] + "";
            lblPackage.Text = fgList[j, "Package_Title"] + "";
            lblCurrency.Text = fgList[j, "Currency"] + "";
            dFrom.Value = Convert.ToDateTime(fgList[j, "StartPeriod"]);
            dTo.Value = Convert.ToDateTime(fgList[j, "EndPeriod"]);
            lblDays.Text = fgList[j, "Days"] + "";
            txtBMV.Text = fgList[j, "BMV"] + "";
            txtEMV.Text = fgList[j, "EMV"] + "";
            txtNetFlows.Text = fgList[j, "NetFlows"] + "";
            txtAIC.Text = fgList[j, "AverageInvestedCapital"] + "";
            txtHWM_StartPeriod.Text = fgList[j, "HWM_StartPeriod"] + "";
            txtAAV.Text = fgList[j, "AdjustedAssetValue"] + "";
            txtDYY_Y.Text = fgList[j, "Index_Y"] + "";
            txtDYY_Period.Text = fgList[j, "Index_P"] + "";
            txtAHF.Text = fgList[j, "AmoiviHF"] + "";
            txtDiscount.Text = fgList[j, "Discount_Percent"] + "";
            txtFinishAmoivi.Text = fgList[j, "FinishAmoiviHF"] + "";
            txtPerformanceResult.Text = fgList[j, "PerformanceResult"] + "";
            txtDYY.Text = fgList[j, "PerformanceIndex"] + "";
            txtNetPerformance.Text = fgList[j, "NetPerformance"] + "";
            txtAAV_HW.Text = fgList[j, "HWM"] + "";
            txtMWR.Text = fgList[j, "MWR"] + "";
            txtNetAmount.Text = fgList[j, "LastAmount"] + "";
            txtVAT_Percent.Text = fgList[j, "VAT_Percent"] + "";
            lblVAT_Amount.Text = fgList[j, "VAT_Amount"] + "";
            txtFinishAmount.Text = fgList[j, "FinishAmount"] + "";
            txtHWM_EndPeriod.Text = fgList[j, "HWM_EndPeriod"] + "";
            panEdit.Visible = true;
        }
        private void ShowInvoice()
        {
            if (fgList[fgList.Row, "FileName"].ToString().Length > 0)
            {
                try
                {
                    Global.DMS_ShowFile("Customers\\" + fgList[fgList.Row, "ContractTitle"] + "\\Invoices", fgList[fgList.Row, "FileName"].ToString());     // is DMS file, so show it into Web mode          
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                finally { }
            }
        }
        private void dStartPeriod_ValueChanged(object sender, EventArgs e)
        {
            CalcData();
        }
        private void dFinishPeriod_ValueChanged(object sender, EventArgs e)
        {
            CalcData();
        }
        private void picClose_Edit_Click(object sender, EventArgs e)
        {
            panEdit.Visible = false;
        }
        private void picFilesPath_Click(object sender, EventArgs e)
        {
            txtFilePath_Import.Text = Global.FileChoice(Global.DefaultFolder);
        }

        private void btnGetImport_Click(object sender, EventArgs e)
        {
            if (txtFilePath_Import.Text.Length > 0)
            {
                int iIndex = 0;
                string sTemp = "";

                var ExApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = ExApp.Workbooks.Open(txtFilePath_Import.Text);
                Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

                iPT_ID = 0;

                if (rb1.Checked)
                {
                    iIndex = 1;
                    dStart = Convert.ToDateTime("01-01-" + cmbYear.Text);
                    dFinish = Convert.ToDateTime("31-03-" + cmbYear.Text);
                }
                if (rb2.Checked)
                {
                    iIndex = 2;
                    dStart = Convert.ToDateTime("01-04-" + cmbYear.Text);
                    dFinish = Convert.ToDateTime("30-06-" + cmbYear.Text);
                }

                clsPerformanceFees_Titles klsPerformanceFees_Title = new clsPerformanceFees_Titles();
                klsPerformanceFees_Title.PF_Year = Convert.ToInt32(cmbYear.Text);
                klsPerformanceFees_Title.PF_Semestr = iIndex;
                klsPerformanceFees_Title.GetRecord_Title();
                iPT_ID = klsPerformanceFees_Title.Record_ID;
                if (iPT_ID == 0)
                {
                    klsPerformanceFees_Title.PF_Semestr = iIndex;
                    klsPerformanceFees_Title.PF_Year = Convert.ToInt32(cmbYear.Text);
                    klsPerformanceFees_Title.DateIns = DateTime.Now;
                    klsPerformanceFees_Title.Author_ID = Global.User_ID;
                    iPT_ID = klsPerformanceFees_Title.InsertRecord();
                }

                this.Refresh();
                this.Cursor = Cursors.WaitCursor;

                i = 1;
                while (true)
                {
                    i = i + 1;

                    sTemp = (xlRange.Cells[i, 3].Value + "").ToString();
                    if (sTemp == "") break;

                    clsContracts klsContract = new clsContracts();
                    klsContract.Code = xlRange.Cells[i, 3].Value.ToString();
                    klsContract.Portfolio = xlRange.Cells[i, 4].Value.ToString();
                    klsContract.GetRecord_Code_Portfolio();

                    clsPerformanceFees_Recs PF_Recs = new clsPerformanceFees_Recs();
                    PF_Recs.PT_ID = iPT_ID;
                    PF_Recs.Client_ID = Convert.ToInt32(klsContract.Client_ID);
                    PF_Recs.DateFrom = dStart;
                    PF_Recs.DateTo = dFinish;
                    PF_Recs.Code = xlRange.Cells[i, 3].Value.ToString();
                    PF_Recs.Portfolio = xlRange.Cells[i, 4].Value.ToString();
                    PF_Recs.Currency = klsContract.Currency + "";
                    PF_Recs.Contract_ID = Convert.ToInt32(klsContract.Record_ID);
                    PF_Recs.Contract_Details_ID = Convert.ToInt32(klsContract.Contract_Details_ID);
                    PF_Recs.Contract_Packages_ID = Convert.ToInt32(klsContract.Contract_Packages_ID);

                    PF_Recs.StartPeriod = Convert.ToDateTime(xlRange.Cells[i, 15].Value.ToString());
                    PF_Recs.EndPeriod = Convert.ToDateTime(xlRange.Cells[i, 16].Value.ToString());
                    PF_Recs.Days = Convert.ToInt32(xlRange.Cells[i, 17].Value);
                    PF_Recs.BMV = Convert.ToDecimal(xlRange.Cells[i, 18].Value);
                    PF_Recs.EMV = Convert.ToDecimal(xlRange.Cells[i, 19].Value);
                    PF_Recs.NetFlows = Convert.ToDecimal(xlRange.Cells[i, 20].Value);
                    PF_Recs.AverageInvestedCapital = Convert.ToDecimal(xlRange.Cells[i, 21].Value);
                    PF_Recs.HWM_StartPeriod = Convert.ToDecimal(xlRange.Cells[i, 22].Value);
                    PF_Recs.AdjustedAssetValue = Convert.ToDecimal(xlRange.Cells[i, 23].Value);
                    PF_Recs.Index_Y = Convert.ToSingle(xlRange.Cells[i, 24].Value);
                    PF_Recs.Index_P = Convert.ToSingle(xlRange.Cells[i, 25].Value);
                    PF_Recs.AmoiviHF = Convert.ToSingle(xlRange.Cells[i, 26].Value);
                    PF_Recs.Discount_Percent = Convert.ToSingle(xlRange.Cells[i, 27].Value);
                    PF_Recs.FinishAmoiviHF = Convert.ToSingle(xlRange.Cells[i, 28].Value);
                    PF_Recs.PerformanceResult = Convert.ToDecimal(xlRange.Cells[i, 29].Value);
                    PF_Recs.PerformanceIndex = Convert.ToDecimal(xlRange.Cells[i, 30].Value);
                    PF_Recs.NetPerformance = Convert.ToDecimal(xlRange.Cells[i, 31].Value);
                    PF_Recs.HWM = Convert.ToDecimal(xlRange.Cells[i, 32].Value);
                    PF_Recs.MWR = Convert.ToSingle(xlRange.Cells[i, 33].Value);
                    PF_Recs.NetAmount = Convert.ToDecimal(xlRange.Cells[i, 35].Value);
                    PF_Recs.VAT_Percent = 24;
                    PF_Recs.VAT_Amount = Convert.ToDecimal(xlRange.Cells[i, 36].Value);
                    PF_Recs.FinishAmount = Convert.ToDecimal(xlRange.Cells[i, 37].Value);
                    PF_Recs.HWM_EndPeriod = Convert.ToDecimal(xlRange.Cells[i, 38].Value);
                    PF_Recs.Invoice_ID = 0;
                    PF_Recs.Invoice_Type = 0;
                    PF_Recs.Invoice_Num = "";
                    PF_Recs.Invoice_File = "";
                    PF_Recs.DateFees = Convert.ToDateTime("1900/01/01");
                    PF_Recs.OfficialInformingDate = "";
                    PF_Recs.Status = 1;                                                      // 1 - Active, 2 - Cancelled
                    PF_Recs.User_ID = Global.User_ID;
                    PF_Recs.DateEdit = DateTime.Now;
                    PF_Recs.InsertRecord();
                }
                //cleanup
                GC.Collect();
                GC.WaitForPendingFinalizers();

                //release com objects to fully kill excel process from running in the background
                Marshal.ReleaseComObject(xlRange);
                Marshal.ReleaseComObject(xlWorksheet);

                //close and release
                xlWorkbook.Close();
                Marshal.ReleaseComObject(xlWorkbook);

                //quit and release
                ExApp.Quit();
                Marshal.ReleaseComObject(ExApp);

                DefineList();
                ShowList();
                this.Cursor = Cursors.Default;

                panImport.Visible = false;

            }

            else
                MessageBox.Show("Καταχώρήστε το αρχείο εισαγωγής", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            panImport.Visible = false;
        }

        private void tsbImport_Click(object sender, EventArgs e)
        {
            panImport.Visible = true;
        }

        private void tsbSave_Edit_Click(object sender, EventArgs e)
        {
            klsPerformanceFees_Recs = new clsPerformanceFees_Recs();
            klsPerformanceFees_Recs.PT_ID = iPT_ID;
            klsPerformanceFees_Recs.Client_ID = iClient_ID;
            klsPerformanceFees_Recs.DateFrom = dFrom.Value;
            klsPerformanceFees_Recs.DateTo = dTo.Value;
            klsPerformanceFees_Recs.Code = lblCode.Text;
            klsPerformanceFees_Recs.Portfolio = lblPortfolio.Text;
            klsPerformanceFees_Recs.Currency = lblCurrency.Text;
            klsPerformanceFees_Recs.Contract_ID = iContract_ID;
            klsPerformanceFees_Recs.Contract_Details_ID = iContract_Details_ID;
            klsPerformanceFees_Recs.Contract_Packages_ID = iContract_Packages_ID;
            klsPerformanceFees_Recs.StartPeriod = dStartPeriod.Value;
            klsPerformanceFees_Recs.EndPeriod = dEndPeriod.Value;
            klsPerformanceFees_Recs.Days = Convert.ToInt32(lblDays.Text);
            klsPerformanceFees_Recs.BMV = Convert.ToDecimal(txtBMV.Text);
            klsPerformanceFees_Recs.EMV = Convert.ToDecimal(txtEMV.Text);
            klsPerformanceFees_Recs.NetFlows = Convert.ToDecimal(txtNetFlows.Text);
            klsPerformanceFees_Recs.AverageInvestedCapital = Convert.ToDecimal(txtAIC.Text);
            klsPerformanceFees_Recs.HWM_StartPeriod = Convert.ToDecimal(txtHWM_StartPeriod.Text);
            klsPerformanceFees_Recs.AdjustedAssetValue = Convert.ToDecimal(txtAAV.Text);
            klsPerformanceFees_Recs.Index_Y = Convert.ToSingle(txtDYY_Y.Text);
            klsPerformanceFees_Recs.Index_P = Convert.ToSingle(txtDYY_Period.Text);
            klsPerformanceFees_Recs.AmoiviHF = Convert.ToSingle(txtAHF.Text);
            klsPerformanceFees_Recs.Discount_Percent = Convert.ToSingle(txtDiscount.Text);
            klsPerformanceFees_Recs.FinishAmoiviHF = Convert.ToSingle(txtFinishAmoivi.Text);
            klsPerformanceFees_Recs.PerformanceResult = Convert.ToDecimal(txtPerformanceResult.Text);
            klsPerformanceFees_Recs.PerformanceIndex = Convert.ToDecimal(txtDYY.Text);
            klsPerformanceFees_Recs.NetPerformance = Convert.ToDecimal(txtNetPerformance.Text);
            klsPerformanceFees_Recs.MWR = Convert.ToSingle(txtMWR.Text);
            klsPerformanceFees_Recs.NetAmount = Convert.ToDecimal(txtNetAmount.Text);
            klsPerformanceFees_Recs.VAT_Percent = Convert.ToSingle(txtVAT_Percent.Text);
            klsPerformanceFees_Recs.VAT_Amount = Convert.ToDecimal(lblVAT_Amount.Text);
            klsPerformanceFees_Recs.FinishAmount = Convert.ToDecimal(txtFinishAmount.Text);
            klsPerformanceFees_Recs.HWM_EndPeriod = Convert.ToDecimal(txtHWM_EndPeriod.Text);
            klsPerformanceFees_Recs.Invoice_ID = 0;
            klsPerformanceFees_Recs.Invoice_Type = iInvoice_Type; 
            klsPerformanceFees_Recs.Invoice_Num = "";
            klsPerformanceFees_Recs.Invoice_File = "";
            klsPerformanceFees_Recs.DateFees = Convert.ToDateTime("1900/01/01");
            klsPerformanceFees_Recs.OfficialInformingDate = "";
            klsPerformanceFees_Recs.Status = 1;
            klsPerformanceFees_Recs.User_ID = Global.User_ID;
            klsPerformanceFees_Recs.DateEdit = DateTime.Now;

            klsPerformanceFees_Recs.InsertRecord();            
            klsPerformanceFees_Recs.GetList();

            DefineList();
            ShowList();
            DefineSums();                                                                   // add new SUBTOTAL row
            panEdit.Visible = false;
        }
        private void CalcData() {
            lblDays.Text = (Convert.ToInt32((dEndPeriod.Value - dStartPeriod.Value).TotalDays) + 1).ToString();

            if (Global.IsNumeric(txtNetAmount.Text) && Global.IsNumeric(txtVAT_Percent.Text))
                  lblVAT_Amount.Text = (Convert.ToDecimal(txtNetAmount.Text) * Convert.ToDecimal(txtVAT_Percent.Text) / 100).ToString("0.00");
        }
        private void tsbPrint_Click(object sender, EventArgs e)
        {
            PrintInvoice();
        }
        private void PrintInvoice()
        {
            sInvoicePrinter = Global.InvoicePrinter;
            frmPrintInvoiceOptions PrintInvoiceOptions = new frmPrintInvoiceOptions();
            PrintInvoiceOptions.Mode = 1;                                               // 1 - full options, 2 - hide IssueDate
            PrintInvoiceOptions.InvoicePrinter = Global.InvoicePrinter;
            PrintInvoiceOptions.NumCopies = iCopies;
            PrintInvoiceOptions.DateIssue = DateTime.Now;  //Convert.ToDateTime(fgList[fgList.Row, "ExecuteDate"]);
            PrintInvoiceOptions.ShowDialog();
            if (PrintInvoiceOptions.LastAktion == 1)
            {
                sInvoicePrinter = PrintInvoiceOptions.InvoicePrinter;
                iCopies = PrintInvoiceOptions.NumCopies;
                dIssueDate = PrintInvoiceOptions.DateIssue;

                PrintingInvoices();
            }
        }
        private void PrintingInvoices()
        {
            int iLine, iInvoiceType, iClientType, iNum;
            string sTemp, sPDF_FullPath, sInvoiceCode, sSeira, sInvTitleGr, sInvTitleEn, sCountry, sNewFile, sLastFileName, sNum, sInvType, sEafdss;
            var WordApp = new Microsoft.Office.Interop.Word.Application();
            var curDoc = new Microsoft.Office.Interop.Word.Document();

            bCheckList = false;
            iInvoiceType = 0;
            iClientType = 0;
            iNum = 0;
            iLine = 0;
            sNum = "";
            sSeira = "";
            sPDF_FullPath = "";
            sInvTitleGr = "";
            sInvTitleEn = "";
            sInvoiceCode = "";
            sCountry = "";
            sInvType = "";
            sEafdss = "";

            try
            {
                WordApp.Visible = false;

                for (iLine = 1; iLine <= (fgList.Rows.Count - 1); iLine++)
                {
                    if (Convert.ToBoolean(fgList[iLine, 0]))
                    {
                        iClientType = Convert.ToInt16(fgList[iLine, "ClientType"]);

                        switch (Convert.ToInt16(fgList[iLine, "Invoice_Type"]))
                        {
                            case 1:
                                iInvoiceType = iInvoiceFisiko;
                                sInvoiceCode = sInvoiceCodeFisiko;
                                sInvTitleGr = sInvTitleFisikoGr;
                                sInvTitleEn = sInvTitleFisikoEn;
                                sInvType = sInvoiceTypeFisiko;
                                sSeira = sSeiraFisiko;
                                break;
                            case 2:
                                iInvoiceType = iInvoiceNomiko;
                                sInvoiceCode = sInvoiceCodeNomiko;
                                sInvTitleGr = sInvTitleNomikoGr;
                                sInvTitleEn = sInvTitleNomikoEn;
                                sInvType = sInvoiceTypeNomiko;
                                sSeira = sSeiraNomiko;
                                break;
                            case 4:                                     //  4 - ΠΙΣΤΩΤΙΚΟ ΤΙΜΟΛΟΓΙΟ
                                if (iClientType == 1)
                                {
                                    iInvoiceType = iInvoicePistotikoFisiko;
                                    sInvoiceCode = sInvoiceCodePistotikoFisiko;
                                    sSeira = sSeiraPistotikoFisiko;
                                    sInvTitleGr = sInvTitlePistotikoFisikoGr;
                                    sInvTitleEn = sInvTitlePistotikoFisikoEn;
                                    sInvType = sInvoiceTypePistotikoFisiko;
                                }
                                else
                                {
                                    iInvoiceType = iInvoicePistotikoNomiko;
                                    sInvoiceCode = sInvoiceCodePistotikoNomiko;
                                    sSeira = sSeiraPistotikoNomiko;
                                    sInvTitleGr = sInvTitlePistotikoNomikoGr;
                                    sInvTitleEn = sInvTitlePistotikoNomikoEn;
                                    sInvType = sInvoiceTypePistotikoNomiko;
                                }
                                break;
                            case 5:                                             // 5 - ΑΚΥΡΩΤΙΚΟ ΣΗΜΕΙΩΜΑ
                                iInvoiceType = iInvoiceAkyrotiko;
                                sInvoiceCode = sCodeAkyrotiko;
                                //sSeira = fgList[iLine, "Invoice_External"].ToString();
                                sInvTitleGr = sInvTitleAkyrotikoGr;
                                sInvTitleEn = sInvTitleAkyrotikoEn;
                                sInvType = sInvoiceTypeAkyrotiko;
                                break;
                        }

                        clsInvoiceTitles InvoiceTitles = new clsInvoiceTitles();
                        InvoiceTitles.Tipos = iInvoiceType;
                        InvoiceTitles.Seira = sSeira;
                        iNum = Convert.ToInt32(InvoiceTitles.GetInvoice_LastNumber()) + 1;

                        // --- Country : Greece or Not ------------
                        if (fgList[iLine, "Country"].ToString() == "" || fgList[iLine, "Country"].ToString() == "Ελλάδα" || fgList[iLine, "Country"].ToString() == "Greece") sCountry = fgList[iLine, "Country"].ToString();
                        else sCountry = fgList[iLine, "CountryEnglish"].ToString();


                        sNum = iNum.ToString();
                        sTemp = sNum;
                        i = sTemp.Trim().Length;
                        switch (i)
                        {
                            case 1: { sNum = "00000000" + iNum; break; }
                            case 2: { sNum = "0000000" + iNum; break; }
                            case 3: { sNum = "000000" + iNum; break; }
                            case 4: { sNum = "00000" + iNum; break; }
                            case 5: { sNum = "0000" + iNum; break; }
                            case 6: { sNum = "000" + iNum; break; }
                            case 7: { sNum = "00" + iNum; break; }
                            case 8: { sNum = "0" + iNum; break; }
                        }

                        // --- check Temp folder  -------------
                        sPDF_FullPath = Application.StartupPath + "\\Temp";
                        if (!Directory.Exists(sPDF_FullPath)) Directory.CreateDirectory(sPDF_FullPath);

                        sTemp = sPDF_FullPath + "\\PF_" + sNum + ".docx";
                        if (File.Exists(sTemp)) File.Delete(sTemp);

                        File.Copy(Application.StartupPath + "\\Templates\\" + sInvoicePFTemplate, sTemp);
                        curDoc = WordApp.Documents.Open(sTemp);

                        sEafdss = "<%SL ;;" + fgList[iLine, "AFM"] + ";;;;;;" + sInvType + ";;" + sNum + ";0;0;" + Math.Abs(Convert.ToDecimal(fgList[iLine, "LastAmount"])) +
                                  ";0;0;0;0;" + Math.Abs(Convert.ToSingle(fgList[iLine, "VAT_Amount"])) + ";0;" + Math.Abs(Convert.ToDecimal(fgList[iLine, "FinishAmount"])) + ";" + "EUR" + ";>";

                        curDoc.Content.Find.Execute(FindText: "{title_gr}", ReplaceWith: sInvTitleGr, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{title_en}", ReplaceWith: sInvTitleEn, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{code}", ReplaceWith: fgList[iLine, "Code"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{portfolio}", ReplaceWith: fgList[iLine, "Portfolio"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{contract_title}", ReplaceWith: fgList[iLine, "ContractTitle"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{invest_services}", ReplaceWith: fgList[iLine, "ServiceTitle"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{invest_profile}", ReplaceWith: fgList[iLine, "InvestmentProfile"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{client_name}", ReplaceWith: fgList[iLine, "User1Name"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{user1name}", ReplaceWith: fgList[iLine, "User1Name"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{address}", ReplaceWith: fgList[iLine, "Address"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{city}", ReplaceWith: fgList[iLine, "City"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{zip}", ReplaceWith: fgList[iLine, "Zip"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{country}", ReplaceWith: sCountry, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{AFM}", ReplaceWith: fgList[iLine, "AFM"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{DOY}", ReplaceWith: fgList[iLine, "DOY"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{invoice_num}", ReplaceWith: iNum, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{issue_date}", ReplaceWith: dIssueDate.ToString("dd/MM/yyyy"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{amount}", ReplaceWith: Math.Abs(Convert.ToDecimal(fgList[iLine, "LastAmount"])).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{vat}", ReplaceWith: Math.Abs(Convert.ToSingle(fgList[iLine, "VAT_Amount"])).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{axia}", ReplaceWith: Math.Abs(Convert.ToDecimal(fgList[iLine, "FinishAmount"])).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{eafdss}", ReplaceWith: sEafdss, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{vp}", ReplaceWith: Math.Abs(Convert.ToSingle(fgList[iLine, "VAT_Percent"])).ToString("0"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{MFAITIOLOGIA_GR}", ReplaceWith: "Αμοιβή Υπεραπόδοσης", Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{APO}", ReplaceWith: fgList[iLine, "StartPeriod"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{EOS}", ReplaceWith: fgList[iLine, "EndPeriod"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);

                        sNewFile = sPDF_FullPath + "\\InvoicePF_" + sNum + ".pdf";
                        curDoc.SaveAs2(sNewFile, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF);
                        WordApp.ScreenUpdating = false;
                        WordApp.Documents.Close();
                        SendKeys.SendWait("{Enter}");

                        sLastFileName = Global.DMS_UploadFile(sNewFile, "Customers/" + (fgList[iLine, "ContractTitle"] + "").Replace(".", "_") + "/Invoices", Path.GetFileName(sNewFile));

                        iID = SaveRecord(iLine, iInvoiceType, sSeira, iNum, Path.GetFileName(sLastFileName), Convert.ToInt32(fgList[iLine, "ID"]), Convert.ToInt32(fgList[iLine, "Contract_ID"]));

                        //--- refresh fgList row ----------------------------------------------------------
                        fgList[iLine, 0] = false;
                        fgList[iLine, 1] = 1;
                        fgList[iLine, "Invoice_Num"] = sInvoiceCode + " " + (sSeira + " " + iNum).Trim();
                        fgList[iLine, "FileName"] = Path.GetFileName(sLastFileName);
                        fgList.Refresh();

                        clsPerformanceFees_Recs PF_Recs = new clsPerformanceFees_Recs();
                        PF_Recs.Record_ID = Convert.ToInt32(fgList[iLine, "ID"]);
                        PF_Recs.GetRecord();
                        PF_Recs.Invoice_ID = iID;
                        PF_Recs.Invoice_Type = Convert.ToInt32(fgList[iLine, "Invoice_Type"]);
                        PF_Recs.Invoice_Num = fgList[iLine, "Invoice_Num"].ToString();
                        PF_Recs.Invoice_File = Path.GetFileName(sLastFileName);
                        PF_Recs.DateFees = dIssueDate;
                        PF_Recs.Status = 1;
                        PF_Recs.EditRecord();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally
            {
                WordApp.Quit();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();

                //--- delete temporary files .docx and .pdf ----------------------------------------
                sTemp = sPDF_FullPath + "\\PF_" + sNum + ".docx";
                if (File.Exists(sTemp)) File.Delete(sTemp);

                sTemp = sPDF_FullPath + "\\InvoicePF_" + sNum + ".pdf";
                if (File.Exists(sTemp)) File.Delete(sTemp);
            }

            bCheckList = true;
        }
        private int SaveRecord(int iRow, int iInvType, string sSeira, int iArithmos, string sInvoiceFile, int iSource_ID, int iContract_ID)
        {
            int iRecord_ID = 0;

            clsInvoiceTitles InvoiceTitles = new clsInvoiceTitles();
            InvoiceTitles.DateIssued = dIssueDate;
            InvoiceTitles.Tipos = iInvType;
            InvoiceTitles.Seira = sSeira;
            InvoiceTitles.Arithmos = iArithmos;
            InvoiceTitles.Selida = "";
            InvoiceTitles.Client_ID = Convert.ToInt32(fgList[iRow, "Client_ID"]);
            InvoiceTitles.TroposApostolis = 0;
            InvoiceTitles.TroposPliromis = 1;
            InvoiceTitles.Posotita = 0;
            InvoiceTitles.AxiaMikti = Convert.ToSingle(fgList[iRow, "LastAmount"]);
            InvoiceTitles.Ekptosi = 0;
            InvoiceTitles.AxiaKathari = Convert.ToSingle(fgList[iRow, "LastAmount"]);
            InvoiceTitles.AxiaFPA = Convert.ToSingle(fgList[iRow, "VAT_Amount"]);
            InvoiceTitles.AxiaTeliki = Convert.ToSingle(fgList[iRow, "FinishAmount"]);
            InvoiceTitles.FileName = sInvoiceFile;
            InvoiceTitles.SourceType = 5;                                                   // 1 - RTO, 2 - FX, 3 - MF, 4 - AF, 5 - PF, 6 - CustodyF
            InvoiceTitles.Source_ID = iSource_ID;                                           // Commands.ID
            InvoiceTitles.Contract_ID = iContract_ID;
            InvoiceTitles.OfficialInformingDate = "";
            InvoiceTitles.Author_ID = Global.User_ID;
            InvoiceTitles.DateIns = DateTime.Now;
            iRecord_ID = InvoiceTitles.InsertRecord();

            clsInvoiceRecs InvoiceRecs = new clsInvoiceRecs();
            InvoiceRecs.IT_ID = iRecord_ID;
            InvoiceRecs.Good_Type = 2;                                             // 1-Good, 2- Service
            InvoiceRecs.Good_Code = "";
            InvoiceRecs.Good_Title = "";
            InvoiceRecs.Good_MM = "";
            InvoiceRecs.Price = Convert.ToSingle(fgList[iRow, "LastAmount"]);
            InvoiceRecs.Posotita = 1;
            InvoiceRecs.AxiaMikti = Convert.ToSingle(fgList[iRow, "LastAmount"]);
            InvoiceRecs.EkptosiPercent = 0;
            InvoiceRecs.EkptosiAxia = 0;
            InvoiceRecs.AxiaKathari = Convert.ToSingle(fgList[iRow, "LastAmount"]);
            InvoiceRecs.FPAPercent = 0;
            InvoiceRecs.FPAAxia = Convert.ToSingle(fgList[iRow, "VAT_Amount"]);
            InvoiceRecs.AxiaTeliki = Convert.ToSingle(fgList[iRow, "FinishAmount"]);
            InvoiceRecs.InsertRecord();

            return iRecord_ID;
        }
        private void tsbExcel_Click(object sender, EventArgs e)
        {
            var EXL = new Microsoft.Office.Interop.Excel.Application();
            var oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            // System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("el-GR");
            EXL.Workbooks.Add();
            EXL.ScreenUpdating = false;
            EXL.Caption = Global.AppTitle;
            EXL.Cells[1, 3].Value = "Τιμολόγηση Performance Fees";
            var loopTo = fgList.Rows.Count - 1;
            for (this.i = 0; this.i <= loopTo; this.i++)
            {
                for (this.j = 2; this.j <= 46; this.j++)
                    EXL.Cells[i + 2, j - 1].Value = fgList[i, j];
            }

            EXL.ScreenUpdating = true;
            EXL.Visible = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
        }
        protected void ucCS_TextChanged(object sender, EventArgs e)
        {
            stContractData = ucCS.SelectedContractData;
            iClient_ID = stContractData.Client_ID;
            lblCode.Text = stContractData.Code;
            lblPortfolio.Text = stContractData.Portfolio;
            lblPackage.Text = stContractData.Package_Title;
            lblCurrency.Text = stContractData.Currency;
            iService_ID = stContractData.Service_ID;
            iContract_ID = stContractData.Contract_ID;
            iContract_Details_ID = stContractData.Contracts_Details_ID;
            iContract_Packages_ID = stContractData.Contracts_Packages_ID;
            txtVAT_Percent.Text = stContractData.VAT_Percent.ToString();

            //----------- Define Contract Data ------------
            txtVAT_Percent.Text = "0";
            clsContracts klsContract = new clsContracts();
            klsContract.Record_ID = iContract_ID;
            klsContract.Contract_Details_ID = iContract_Details_ID;
            klsContract.Contract_Packages_ID = iContract_Packages_ID;
            klsContract.GetRecord();
            lblPackage.Text = klsContract.Package_Title;
            iServiceProvider_ID = klsContract.ServiceProvider_ID;
            if (klsContract.ClientTipos == 1)
                txtVAT_Percent.Text = klsContract.VAT_FP.ToString();
            else
                if (klsContract.ClientTipos == 2)
                txtVAT_Percent.Text = klsContract.VAT_NP.ToString();

            iClientTipos = klsContract.ClientTipos;
            if (iClientTipos == 1) iInvoice_Type = 1;
            if (iClientTipos == 2) iInvoice_Type = 2;

            //-------------- Define txtNotes.Text value  ------------------
            sTemp = "";
            foundRows = Global.dtServices.Select("ID=" + iService_ID);
            if (foundRows.Length > 0) sTemp = foundRows[0]["Title"].ToString();

            switch (iService_ID) {
                case 2:                       // Advisory
                    txtFinishAmoivi.Text = klsContract.Advisory_Month3_Discount.ToString();
                    txtPerformanceResult.Text = klsContract.Advisory_Month3_Fees.ToString();
                    break;
                case 3:                       // Discretionary
                    txtFinishAmoivi.Text = klsContract.Discret_Month3_Discount.ToString();
                    txtPerformanceResult.Text = klsContract.Discret_Month3_Fees.ToString();
                    break;
                case 5:                      // Dealing  Advisory
                    txtFinishAmoivi.Text = "0";
                    txtPerformanceResult.Text = "0";
                    break;
            }
            dStartPeriod.Focus();
        }
        private void DefineOptions()
        {
            clsOptions Options = new clsOptions();
            Options.GetRecord();
            sInvoicePrinter = Options.InvoicePrinter;
            iCopies = Options.InvoiceCopies;

            iInvoiceFisiko = Options.InvoiceFisiko;
            foundRows = Global.dtInvoicesTypes.Select("ID= " + iInvoiceFisiko);
            if (foundRows.Length > 0)
            {
                sInvTitleFisikoGr = foundRows[0]["Title"].ToString();
                sInvTitleFisikoEn = foundRows[0]["TitleEn"].ToString();
                sInvoiceCodeFisiko = foundRows[0]["Code"].ToString();
                sInvoiceTypeFisiko = foundRows[0]["Type"].ToString();
            }

            iInvoiceNomiko = Options.InvoiceNomiko;
            foundRows = Global.dtInvoicesTypes.Select("ID= " + iInvoiceNomiko);
            if (foundRows.Length > 0)
            {
                sInvTitleNomikoGr = foundRows[0]["Title"].ToString();
                sInvTitleNomikoEn = foundRows[0]["TitleEn"].ToString();
                sInvoiceCodeNomiko = foundRows[0]["Code"].ToString();
                sInvoiceTypeNomiko = foundRows[0]["Type"].ToString();
            }

            iInvoicePistotikoFisiko = Options.InvoicePistotikoFisiko;
            foundRows = Global.dtInvoicesTypes.Select("ID= " + iInvoicePistotikoFisiko);
            if (foundRows.Length > 0)
            {
                sInvTitlePistotikoFisikoGr = foundRows[0]["Title"].ToString();
                sInvTitlePistotikoFisikoEn = foundRows[0]["TitleEn"].ToString();
                sInvoiceCodePistotikoFisiko = foundRows[0]["Code"].ToString();
                sInvoiceTypePistotikoFisiko = foundRows[0]["Type"].ToString();
            }

            iInvoicePistotikoNomiko = Options.InvoicePistotikoNomiko;
            foundRows = Global.dtInvoicesTypes.Select("ID= " + iInvoicePistotikoNomiko);
            if (foundRows.Length > 0)
            {
                sInvTitlePistotikoNomikoGr = foundRows[0]["Title"].ToString();
                sInvTitlePistotikoNomikoEn = foundRows[0]["TitleEn"].ToString();
                sInvoiceCodePistotikoNomiko = foundRows[0]["Code"].ToString();
                sInvoiceTypePistotikoNomiko = foundRows[0]["Type"].ToString();
            }

            iInvoiceAkyrotiko = Options.InvoiceAkyrotiko;
            foundRows = Global.dtInvoicesTypes.Select("ID= " + iInvoiceAkyrotiko);
            if (foundRows.Length > 0)
            {
                sInvTitleAkyrotikoGr = foundRows[0]["Title"].ToString();
                sInvTitleAkyrotikoEn = foundRows[0]["TitleEn"].ToString();
                sInvoiceCodeAkyrotiko = foundRows[0]["Code"].ToString();
                sInvoiceTypeAkyrotiko = foundRows[0]["Type"].ToString();
            }

            sInvoicePFTemplate = Options.InvoicePFTemplate;
        }
        //----------------------------------------------------------------------------------------------
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public string  Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }

}
