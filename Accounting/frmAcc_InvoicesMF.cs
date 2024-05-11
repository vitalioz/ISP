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

public struct MFRec
{
    public DateTime dFrom;
    public DateTime dTo;
    public int Days;
    public int Contract_ID;
    public int Contract_Packages_ID;
    public int Service_ID;
    public decimal AUM;
    public float AmoiviPro;
    public float AxiaPro;
    public string Climakas;
    public string Discount_DateFrom;
    public string Discount_DateTo;
    public float Discount_Percent1;
    public float Discount_Amount1;
    public float Discount_Percent2;
    public float Discount_Amount2;
    public float Discount_Percent;
    public float Discount_Amount;
    public float AmoiviAfter;
    public float AxiaAfter;
    public float MinAmoivi;
    public float MinAmoivi_Percent;
    public decimal FinishMinAmoivi;
    public decimal LastAmount;
    public float LastAmount_Percent;
    public float VAT_Percent;
    public float VAT_Amount;
    public decimal FinishAmount;
    public string Invoice_External;
}

namespace Accounting
{
    public partial class frmAcc_InvoicesMF : Form
    {
        DataView dtView;
        int i, j, iID, iFT_ID, iMF_Quart, iClient_ID, iClientType, iAktion, iRightsLevel, iNum, iInvoiceType, iInvoiceFisiko, iInvoiceNomiko, 
            iInvoicePistotikoFisiko, iInvoicePistotikoNomiko, iInvoiceAkyrotiko, iCopies, iMode_FilePath,
            iService_ID, iContract_ID, iContract_Details_ID, iContract_Packages_ID, iSourceRows, iFoundRows;
        string sSeira, sInvoicePrinter, sCodeAkyrotiko = "", sInvTitleFisikoGr = "", sInvTitleFisikoEn = "", sInvoiceCodeFisiko = "", 
               sInvTitleNomikoGr = "", sInvTitleNomikoEn = "", sInvoiceCodeNomiko = "", sInvoiceTypeFisiko = "", sInvoiceTypeNomiko = "", 
               sSeiraPistotikoFisiko = "", sSeiraPistotikoNomiko = "", sSeiraAkyrotiko = "", sInvoiceMFTemplate = "", sInvoiceMFAnalysisTemplate = "",
               sInvoiceCodePistotikoFisiko = "", sInvTitlePistotikoFisikoGr = "", sInvTitlePistotikoFisikoEn = "", sInvoiceTypePistotikoFisiko = "",
               sInvoiceCodePistotikoNomiko = "", sInvTitlePistotikoNomikoGr = "", sInvTitlePistotikoNomikoEn = "", sInvoiceTypePistotikoNomiko = "",
               sInvoiceCodeAkyrotiko = "", sInvTitleAkyrotikoGr = "", sInvTitleAkyrotikoEn = "", sInvoiceTypeAkyrotiko = "",
               sSeiraFisiko = "", sSeiraNomiko = "", sUnfoundRows, sExportFilePath, sExtra;
        decimal decSourceAmount, decFoundAmount;
        DateTime dStart, dFinish, dIssueDate;
        C1.Win.C1FlexGrid.CellRange rng;
        CellStyle csChecked, csFound, csNotFound, csDiscount, csFinish;
        Hashtable imgMap = new Hashtable();
        Global.ContractData stContractData;
        DataRow[] foundRows;
        MFRec stMFRec;
        bool bCheckList, bRecalcPrices;
        Point position;
        bool pMove;

        clsManagmentFees_Titles ManagmentFees_Titles = new clsManagmentFees_Titles();
        clsManagmentFees_Recs ManagmentFees_Recs = new clsManagmentFees_Recs();
        public frmAcc_InvoicesMF()
        {
            InitializeComponent();
        }
        private void frmAcc_InvoicesMF_Load(object sender, EventArgs e)
        {
            ucCS.TextOfLabelChanged += new EventHandler(ucCS_TextChanged);
            ucCS.ButtonClick += new EventHandler(ucCS_ButtonClick);

            bCheckList = false;
            iAktion = 0;                                    // 0 - Add, 1 - Edit

            panFilePath.Left = 4;
            panFilePath.Top = 92;

            csChecked = fgList.Styles.Add("Checked");
            csChecked.BackColor = Color.Yellow;

            csFound = fgList.Styles.Add("FinishAmount");
            csFound.BackColor = Color.LimeGreen;

            csNotFound = fgList.Styles.Add("Checked");
            csNotFound.BackColor = Color.Yellow;

            csDiscount = fgFees.Styles.Add("Discount");
            csDiscount.BackColor = Color.PeachPuff;

            csFinish = fgFees.Styles.Add("Finish");
            csFinish.BackColor = Color.LightGreen;

            panTools.Visible = false;
            chkPrint.Visible = false;
            fgList.Visible = false;

            for (i = 0; i < imgFiles.Images.Count; i++) imgMap.Add(i, imgFiles.Images[i]);

            for (i = 2010; i <= DateTime.Now.Year; i++)  cmbYear.Items.Add(i);

            i = (DateTime.Now.Month + 2) / 3;
            if (i == 1)  { i = 4; cmbYear.SelectedIndex = cmbYear.Items.Count - 2; }
            else         { i = i - 1; cmbYear.SelectedIndex = cmbYear.Items.Count - 1; }

            switch (i)
            {
                case 1:
                    rb1.Checked = true;
                    break;
                case 2:
                    rb2.Checked = true;
                    break;
                case 3:
                    rb3.Checked = true;
                    break;
                case 4:
                    rb4.Checked = true;
                    break;
            }

            cmbServiceProviders.DataSource = Global.dtServiceProviders.Copy();
            cmbServiceProviders.DisplayMember = "Title";
            cmbServiceProviders.ValueMember = "ID";
            cmbServiceProviders.SelectedItem = 1;

            dtView = Global.dtUserList.Copy().DefaultView;
            dtView.RowFilter = "Chief = 1";
            cmbAdvisors.DataSource = dtView;
            cmbAdvisors.DisplayMember = "Title";
            cmbAdvisors.ValueMember = "ID";
            cmbAdvisors.SelectedItem = 1;

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
            fgList.Styles.Normal.WordWrap = true;
            fgList.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;
            fgList.Rows[0].AllowMerging = true;
            fgList.Cols[0].AllowMerging = true;
            rng = fgList.GetCellRange(0, 0, 1, 0);
            rng.Data = " ";
            fgList.Cols[1].AllowMerging = true;
            rng = fgList.GetCellRange(0, 1, 1, 1);
            rng.Data = " ";

            fgList.Cols[2].AllowMerging = true;
            rng = fgList.GetCellRange(0, 2, 1, 2);
            rng.Data = Global.GetLabel("n");

            fgList.Cols[3].AllowMerging = true;
            rng = fgList.GetCellRange(0, 3, 1, 3);
            rng.Data = Global.GetLabel("from");

            fgList.Cols[4].AllowMerging = true;
            rng = fgList.GetCellRange(0, 4, 1, 4);
            rng.Data = Global.GetLabel("until_");

            fgList.Cols[5].AllowMerging = true;
            rng = fgList.GetCellRange(0, 5, 1, 5);
            rng.Data = Global.GetLabel("contract");

            fgList.Cols[6].AllowMerging = true;
            rng = fgList.GetCellRange(0, 6, 1, 6);
            rng.Data = Global.GetLabel("code");

            fgList.Cols[7].AllowMerging = true;
            rng = fgList.GetCellRange(0, 7, 1, 7);
            rng.Data = Global.GetLabel("subaccount");

            fgList.Cols[8].AllowMerging = true;
            rng = fgList.GetCellRange(0, 8, 1, 8);
            rng.Data = Global.GetLabel("package");

            fgList.Cols[9].AllowMerging = true;
            rng = fgList.GetCellRange(0, 9, 1, 9);
            rng.Data = Global.GetLabel("currency");

            fgList.Cols[10].AllowMerging = true;
            rng = fgList.GetCellRange(0, 10, 1, 10);
            rng.Data = Global.GetLabel("days");

            fgList.Cols[11].AllowMerging = true;
            rng = fgList.GetCellRange(0, 11, 1, 11);
            rng.Data = Global.GetLabel("aum");

            rng = fgList.GetCellRange(0, 12, 0, 16);
            rng.Data = "Έξοδα Management Fees";
            fgList[1, 12] = "% σύμβασης";
            fgList[1, 13] = "ποσό σύμβασης";
            fgList[1, 14] = "% έκπτωση";
            fgList[1, 15] = "% μετά την έκπτωση";
            fgList[1, 16] = "ποσό μετά την έκπτωση";

            fgList.Cols[17].AllowMerging = true;
            rng = fgList.GetCellRange(0, 17, 1, 17);
            rng.Data = "Κλήμακας";

            fgList.Cols[18].AllowMerging = true;
            rng = fgList.GetCellRange(0, 18, 1, 18);
            rng.Data = "Ημερ.έκπτωσης";

            rng = fgList.GetCellRange(0, 19, 0, 21);
            rng.Data = "Έξοδα Minimum Management Fees";
            fgList[1, 19] = "προ έκπτωσης";
            fgList[1, 20] = "% έκπτωσης";
            fgList[1, 21] = "τελικό";

            fgList.Cols[22].AllowMerging = true;
            rng = fgList.GetCellRange(0, 22, 1, 22);
            rng.Data = "Τελική Αξία";

            fgList.Cols[23].AllowMerging = true;
            rng = fgList.GetCellRange(0, 23, 1, 23);
            rng.Data = "ΦΠΑ";

            fgList.Cols[24].AllowMerging = true;
            rng = fgList.GetCellRange(0, 24, 1, 24);
            rng.Data = "Πληρωτέο Ποσό";

            fgList.Cols[25].AllowMerging = true;
            rng = fgList.GetCellRange(0, 25, 1, 25);
            rng.Data = "Τελική Αξία % ετήσια";

            fgList.Cols[26].AllowMerging = true;
            rng = fgList.GetCellRange(0, 26, 1, 26);
            rng.Data = "Αρ.Παραστατικου";

            fgList.Cols[27].AllowMerging = true;
            rng = fgList.GetCellRange(0, 27, 1, 27);
            rng.Data = "Ημερ.Χρέωσεις";

            fgList.Cols[28].AllowMerging = true;
            rng = fgList.GetCellRange(0, 28, 1, 28);
            rng.Data = Global.GetLabel("notes");

            fgList.Cols[29].AllowMerging = true;
            rng = fgList.GetCellRange(0, 29, 1, 29);
            rng.Data = Global.GetLabel("service");

            fgList.Cols[30].AllowMerging = true;
            rng = fgList.GetCellRange(0, 30, 1, 30);
            rng.Data = "Επενδ.πολιτική";

            fgList.Cols[31].AllowMerging = true;
            rng = fgList.GetCellRange(0, 31, 1, 31);
            rng.Data = Global.GetLabel("profile");

            fgList.Cols[32].AllowMerging = true;
            rng = fgList.GetCellRange(0, 32, 1, 32);
            rng.Data = "Advisor";

            fgList.Cols[33].AllowMerging = true;
            rng = fgList.GetCellRange(0, 33, 1, 33);
            rng.Data = "RM";

            fgList.Cols[34].AllowMerging = true;
            rng = fgList.GetCellRange(0, 34, 1, 34);
            rng.Data = "Introducer";

            fgList.Cols[35].AllowMerging = true;
            rng = fgList.GetCellRange(0, 35, 1, 35);
            rng.Data = "Διαχειρηστής";

            fgList.Cols[36].AllowMerging = true;
            rng = fgList.GetCellRange(0, 36, 1, 36);
            rng.Data = "1ος Δικαιούχος";

            fgList.Cols[37].AllowMerging = true;
            rng = fgList.GetCellRange(0, 37, 1, 37);
            rng.Data = Global.GetLabel("address");

            fgList.Cols[38].AllowMerging = true;
            rng = fgList.GetCellRange(0, 38, 1, 38);
            rng.Data = Global.GetLabel("city");

            fgList.Cols[39].AllowMerging = true;
            rng = fgList.GetCellRange(0, 39, 1, 39);
            rng.Data = Global.GetLabel("zip");

            fgList.Cols[40].AllowMerging = true;
            rng = fgList.GetCellRange(0, 40, 1, 40);
            rng.Data = Global.GetLabel("country");

            fgList.Cols[41].AllowMerging = true;
            rng = fgList.GetCellRange(0, 41, 1, 41);
            rng.Data = Global.GetLabel("afm");

            fgList.Cols[42].AllowMerging = true;
            rng = fgList.GetCellRange(0, 42, 1, 42);
            rng.Data = Global.GetLabel("doy");

            fgList.Cols[43].AllowMerging = true;
            rng = fgList.GetCellRange(0, 43, 1, 43);
            rng.Data = "ID εντολής";

            Column clm1 = fgList.Cols["image_map"];
            clm1.ImageMap = imgMap;
            clm1.ImageAndText = false;
            clm1.ImageAlign = ImageAlignEnum.CenterCenter;


            //------- fgFees ----------------------------
            fgFees.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Cell;
            //fgFees.Styles.ParseString(curGridStyle);
            fgFees.DrawMode = DrawModeEnum.OwnerDraw;
            fgFees.ShowCellLabels = true;

            fgFees.Styles.Normal.WordWrap = true;
            fgFees.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;

            fgFees.Rows[0].AllowMerging = true;

            rng = fgFees.GetCellRange(0, 0, 0, 1);
            rng.Data = "Ποσό";

            fgFees[1, 0] = "από";
            fgFees[1, 1] = "εώς";

            fgFees.Cols[2].AllowMerging = true;
            rng = fgFees.GetCellRange(0, 2, 1, 2);
            rng.Data = "Αμοιβή";

            rng = fgFees.GetCellRange(0, 3, 0, 5);
            rng.Data = "Έκπτωση";

            fgFees[1, 3] = "Ημερ.από";
            fgFees[1, 4] = "Ημερ.εώς";
            fgFees[1, 5] = "% Έκπτωσης";

            fgFees.Cols[6].AllowMerging = true;
            rng = fgFees.GetCellRange(0, 6, 1, 6);
            rng.Data = "Τελική Αμοιβή";

            csDiscount = fgFees.Styles.Add("Discount");
            csDiscount.BackColor = Color.PeachPuff;

            csFinish = fgFees.Styles.Add("Finish");
            csFinish.BackColor = Color.LightGreen;

            fgFees.Cols[3].Style = csDiscount;
            fgFees.Cols[4].Style = csDiscount;
            fgFees.Cols[5].Style = csDiscount;
            fgFees.Cols[6].Style = csFinish;


            DefineOptions();

            btnSearch.Enabled = false;
            cmbFilter.SelectedIndex = 0;
            bCheckList = true;
            bRecalcPrices = true;
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
        private void cmbServiceProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSearch.Enabled = true;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            if (rb1.Checked)
            {
                iMF_Quart = 1;
                dStart = Convert.ToDateTime("01-01-" + cmbYear.Text);
                dFinish = Convert.ToDateTime("31-03-" + cmbYear.Text);
            }
            else {
                if (rb2.Checked) {
                    iMF_Quart = 2;
                    dStart = Convert.ToDateTime("01-04-" + cmbYear.Text);
                    dFinish = Convert.ToDateTime("30-06-" + cmbYear.Text);
                }
                else
                {
                    if (rb3.Checked) {
                        iMF_Quart = 3;
                        dStart = Convert.ToDateTime("01-07-" + cmbYear.Text);
                        dFinish = Convert.ToDateTime("30-09-" + cmbYear.Text);
                    }
                    else
                    {
                        if (rb4.Checked) {
                            iMF_Quart = 4;
                            dStart = Convert.ToDateTime("01-10-" + cmbYear.Text);
                            dFinish = Convert.ToDateTime("31-12-" + cmbYear.Text);
                        }
                    }
                }
            }

            ManagmentFees_Titles = new clsManagmentFees_Titles();
            ManagmentFees_Titles.SC_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
            ManagmentFees_Titles.MF_Year = Convert.ToInt32(cmbYear.Text);
            ManagmentFees_Titles.MF_Quart = Convert.ToInt32(iMF_Quart);
            ManagmentFees_Titles.GetRecord_Title();
            iFT_ID = ManagmentFees_Titles.Record_ID;
            if (iFT_ID == 0)
            {
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Νέο τρίμηνο.\n Είστε σίγουρος για αυτό;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    toolLeft.Visible = true;
                    cmbFilter.Visible = true;

                    clsManagmentFees_Titles ManagmentFees_Titles = new clsManagmentFees_Titles();
                    ManagmentFees_Titles.SC_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                    ManagmentFees_Titles.MF_Quart = iMF_Quart;
                    ManagmentFees_Titles.MF_Year = Convert.ToInt32(cmbYear.Text);
                    ManagmentFees_Titles.DateIns = DateTime.Now;
                    ManagmentFees_Titles.Author_ID = Global.User_ID;
                    iFT_ID = ManagmentFees_Titles.InsertRecord();
                }
            }
            toolLeft.Visible = true;
            cmbFilter.Visible = true;
            DefineList();
            ShowList();

            this.Cursor = Cursors.Default;
            panTools.Visible = true;
            chkPrint.Visible = true;
            fgList.Visible = true;
        }
        private void dFrom_ValueChanged(object sender, EventArgs e)
        {
            CalcFees_Step1();
            CalcFees_Step2();
            ShowNewValues();
        }
        private void dTo_ValueChanged(object sender, EventArgs e)
        {
            CalcFees_Step1();
            CalcFees_Step2();
            ShowNewValues();
        }
        private void cmbAdvisors_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowList();
        }
        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            ShowList();
        }  
        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowList();
        }
        private void chkPrint_CheckedChanged(object sender, EventArgs e)
        {
            for (i = 2; i <= fgList.Rows.Count - 2; i++) fgList[i, 0] = chkPrint.Checked;
        }
        private void fgList_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (bCheckList)
            {
                if (e.Col == 0) e.Cancel = false;
                else e.Cancel = true;
            }
        }
        private void tsbAUM_Click(object sender, EventArgs e)
        {
            iMode_FilePath = 1;                             // 1 - AUM Mode, 2 - Export to .csv Mode
            lblFilePath.Text = "Εισαγωγή AUM";
            txtFilePath.Text = "";
            lblSourceRows.Text = "";
            lblSourceAmount.Text = "";
            lblFoundRows.Text = "";
            lblFoundAmount.Text = "";
            txtUnfound.Text = "";
            panFilePath.Height = 132;
            panFilePath.Visible = true;
        }
        private void tsbExport_Click(object sender, EventArgs e)
        {
            iMode_FilePath = 2;                             // 1 - AUM Mode, 2 - Export to .csv Mode
            lblFilePath.Text = "Εξαγωγή λίστας παραστατικών";
            txtFilePath.Text = Application.StartupPath + "/Temp/ManagFees_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".txt";
            lblSourceRows.Text = "";
            lblSourceAmount.Text = "";
            lblFoundRows.Text = "";
            lblFoundAmount.Text = "";
            txtUnfound.Text = "";
            panFilePath.Height = 132;
            panFilePath.Visible = true;
        }
        private void picFilePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            switch (Convert.ToInt32(cmbServiceProviders.SelectedValue))
            {
                case 2:

                    dialog.Filter = "CSV Files|*.csv;*.txt";
                    dialog.InitialDirectory = @"C:\";
                    dialog.Title = "Please select an file ";

                    if (dialog.ShowDialog() == DialogResult.OK) txtFilePath.Text = dialog.FileName;
                    break;
                default:
                    dialog.Filter = "Excel Files|*.xlsx;*.xls";
                    dialog.InitialDirectory = @"C:\";
                    dialog.Title = "Please select an Excel file ";

                    if (dialog.ShowDialog() == DialogResult.OK) txtFilePath.Text = dialog.FileName;
                    break;
            }
        }
        private void btnOK_FilePath_Click(object sender, EventArgs e)
        {
            if (iMode_FilePath == 1)                                                  // 1 - AUM Mode, 2 - Export to .csv Mode
            {                                                
                string sCode = "", sPortfolio = "", sInvoiceExternal = "";
                Decimal decAUM;
                DateTime dFrom, dTo;
                Excel.Range range;

                iSourceRows = 0;
                iFoundRows = 0;
                decSourceAmount = 0;
                decFoundAmount = 0;
                sUnfoundRows = "";

                switch (Convert.ToInt32(cmbServiceProviders.SelectedValue))
                {
                    case 2:
                        using (var reader = new StreamReader(txtFilePath.Text))
                        {
                            List<string> listA = new List<string>();
                            List<string> listB = new List<string>();
                            while (!reader.EndOfStream)
                            {
                                var line = reader.ReadLine();
                                var values = line.Split(';');

                                sPortfolio = values[4];
                                sCode = values[5];
                                dFrom = Convert.ToDateTime(values[0]);
                                dTo = Convert.ToDateTime(values[1]);
                                sInvoiceExternal = values[7];
                                decAUM = Convert.ToDecimal(values[8]);

                                iSourceRows = iSourceRows + 1;
                                decSourceAmount = decSourceAmount + decAUM;
                                if (decAUM != 0) SaveCalculations(sCode, sPortfolio, sInvoiceExternal, dFrom, dTo, decAUM, false);
                            }
                        }
                        break;

                    default:
                        Excel.Application excelApp = new Excel.Application();
                        if (excelApp != null)
                        {
                            Excel.Workbook excelWorkbook = excelApp.Workbooks.Open(txtFilePath.Text, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                            Excel.Worksheet excelWorksheet = (Excel.Worksheet)excelWorkbook.Sheets[1];

                            Excel.Range excelRange = excelWorksheet.UsedRange;
                            int rowCount = excelRange.Rows.Count;

                            try
                            {
                                for (i = 2; i <= rowCount; i++)
                                {
                                    range = (excelWorksheet.Cells[i, 1] as Excel.Range);
                                    dFrom = Convert.ToDateTime(range.Value.ToString());

                                    range = (excelWorksheet.Cells[i, 2] as Excel.Range);
                                    dTo = Convert.ToDateTime(range.Value.ToString());

                                    range = (excelWorksheet.Cells[i, 3] as Excel.Range);
                                    sCode = range.Value.ToString();

                                    range = (excelWorksheet.Cells[i, 4] as Excel.Range);
                                    sPortfolio = range.Value.ToString();

                                    range = (excelWorksheet.Cells[i, 5] as Excel.Range);
                                    decAUM = Convert.ToDecimal(range.Value);

                                    iSourceRows = iSourceRows + 1;
                                    decSourceAmount = decSourceAmount + decAUM;
                                    if (decAUM != 0) SaveCalculations(sCode, sPortfolio, sInvoiceExternal, dFrom, dTo, decAUM, true);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message + "\nError Line N = " + i.ToString() + "  Code = " + sCode + "   Portfolio = " + sPortfolio,
                                                Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            finally
                            {
                                excelWorkbook.Close();
                                excelApp.Quit();
                            }
                        }
                        break;
                }

                ShowList();

                lblSourceRows.Text = iSourceRows.ToString();
                lblSourceAmount.Text = decSourceAmount.ToString();
                lblFoundRows.Text = iFoundRows.ToString();
                lblFoundAmount.Text = decFoundAmount.ToString();
                txtUnfound.Text = sUnfoundRows;
                panFilePath.Height = 316;
            }
            else                                                                    // 1 - AUM Mode, 2 - Export to .csv Mode
            {
                using (var stream = File.CreateText(txtFilePath.Text))
                {
                    for (i = 2; i <= fgList.Rows.Count - 2; i++)
                    {
                        if ((fgList[i, "Invoice_Num"] + "") != "")
                           stream.WriteLine(fgList[i, "Invoice_External"] + ";" + fgList[i, "LastAmount"] + ";" + fgList[i, "VAT_Amount"] + ";" +
                                            fgList[i, "FinishAmount"] + ";" + fgList[i, "Invoice_Num"] + ";" + fgList[i, "Notes"] + ";");
                    }
                }
                panFilePath.Visible = false;
            }
        }
        private void SaveCalculations(string sCode, string sPortfolio, string sInvoiceExternal, DateTime dFrom, DateTime dTo, decimal decAUM, bool bCheckDates)
        {
            DataRow[] foundRows;
            string sFilter = "";

            if (bCheckDates) sFilter = "Code= '" + sCode + "' and Portfolio = '" + sPortfolio + "' and DateFrom = '" + dFrom.ToString("dd/MM/yyyy") + "' and DateTo = '" + dTo.ToString("dd/MM/yyyy") + "'";
            else             sFilter = "Code= '" + sCode + "' and Portfolio = '" + sPortfolio + "'";

            foundRows = ManagmentFees_Recs.List.Select(sFilter);
            if (foundRows.Length > 0)
            {
                stMFRec.dFrom = Convert.ToDateTime(foundRows[0]["DateFrom"]);
                stMFRec.dTo = Convert.ToDateTime(foundRows[0]["DateTo"]);
                stMFRec.Days = Convert.ToInt32(foundRows[0]["Days"]);
                stMFRec.Contract_ID = Convert.ToInt32(foundRows[0]["Contract_ID"]);
                stMFRec.Contract_Packages_ID = Convert.ToInt32(foundRows[0]["Contracts_Packages_ID"]);
                stMFRec.Service_ID = Convert.ToInt32(foundRows[0]["Service_ID"]);
                iService_ID = Convert.ToInt32(foundRows[0]["Service_ID"]);
                stMFRec.AUM = decAUM;
                stMFRec.AmoiviPro = Convert.ToSingle(foundRows[0]["AmoiviPro"]);
                stMFRec.AxiaPro = Convert.ToSingle(foundRows[0]["AxiaPro"]);
                stMFRec.Climakas = foundRows[0]["Climakas"] + "";
                stMFRec.Discount_DateTo = foundRows[0]["Discount_DateTo"] + "";
                stMFRec.Discount_Percent1 = Convert.ToSingle(foundRows[0]["Discount_Percent1"]);
                stMFRec.Discount_Amount1 = Convert.ToSingle(foundRows[0]["Discount_Amount1"]);
                stMFRec.Discount_Percent2 = Convert.ToSingle(foundRows[0]["Discount_Percent2"]);
                stMFRec.Discount_Amount2 = Convert.ToSingle(foundRows[0]["Discount_Amount2"]);
                stMFRec.Discount_Percent = Convert.ToSingle(foundRows[0]["Discount_Percent"]);
                stMFRec.Discount_Amount = Convert.ToSingle(foundRows[0]["Discount_Amount"]);
                stMFRec.AmoiviAfter = Convert.ToSingle(foundRows[0]["AmoiviAfter"]);
                stMFRec.AxiaAfter = Convert.ToSingle(foundRows[0]["AxiaAfter"]);
                stMFRec.MinAmoivi = Convert.ToSingle(foundRows[0]["MinAmoivi"]);
                stMFRec.MinAmoivi_Percent = Convert.ToSingle(foundRows[0]["MinAmoivi_Percent"]);
                stMFRec.FinishMinAmoivi = Convert.ToDecimal(foundRows[0]["FinishMinAmoivi"]);
                stMFRec.LastAmount = Convert.ToDecimal(foundRows[0]["LastAmount"]);
                stMFRec.LastAmount_Percent = Convert.ToSingle(foundRows[0]["LastAmount_Percent"]);
                stMFRec.VAT_Percent = Convert.ToSingle(foundRows[0]["VAT_Percent"]);
                stMFRec.VAT_Amount = Convert.ToSingle(foundRows[0]["VAT_Amount"]);
                stMFRec.FinishAmount = Convert.ToDecimal(foundRows[0]["FinishAmount"]);
                stMFRec.Invoice_External = sInvoiceExternal;

                CalcFees_Step1();
                CalcFees_Step2();

                foundRows[0]["AUM"] = decAUM;
                foundRows[0]["AmoiviPro"] = stMFRec.AmoiviPro.ToString();
                foundRows[0]["AxiaPro"] = stMFRec.AxiaPro.ToString();
                foundRows[0]["Discount_Percent1"] = stMFRec.Discount_Percent1.ToString();
                foundRows[0]["Discount_Amount1"] = stMFRec.Discount_Amount1.ToString();
                foundRows[0]["Discount_Percent"] = stMFRec.Discount_Percent.ToString();
                foundRows[0]["Discount_Amount"] = stMFRec.Discount_Amount.ToString();
                foundRows[0]["AmoiviAfter"] = stMFRec.AmoiviAfter.ToString();
                foundRows[0]["AxiaAfter"] = stMFRec.AxiaAfter.ToString();
                foundRows[0]["MinAmoivi"] = stMFRec.MinAmoivi.ToString();
                foundRows[0]["MinAmoivi_Percent"] = stMFRec.MinAmoivi_Percent.ToString();
                foundRows[0]["FinishMinAmoivi"] = stMFRec.FinishMinAmoivi.ToString();
                foundRows[0]["LastAmount"] = stMFRec.LastAmount.ToString();
                foundRows[0]["LastAmount_Percent"] = stMFRec.LastAmount_Percent.ToString();
                foundRows[0]["VAT_Percent"] = stMFRec.VAT_Percent.ToString();
                foundRows[0]["VAT_Amount"] = stMFRec.VAT_Amount.ToString();
                foundRows[0]["FinishAmount"] = stMFRec.FinishAmount.ToString();
                foundRows[0]["Invoice_External"] = stMFRec.Invoice_External;

                //--- save record with AUM and calculating data ---------------------------------
                clsManagmentFees_Recs MF_Recs = new clsManagmentFees_Recs();
                MF_Recs.Record_ID = Convert.ToInt32(foundRows[0]["ID"]);
                MF_Recs.GetRecord();

                MF_Recs.AUM = decAUM;
                MF_Recs.AmoiviPro = stMFRec.AmoiviPro;
                MF_Recs.AxiaPro = stMFRec.AxiaPro;
                MF_Recs.Discount_Percent1 = stMFRec.Discount_Percent1;
                MF_Recs.Discount_Amount1 = stMFRec.Discount_Amount1;
                MF_Recs.Discount_Percent2 = stMFRec.Discount_Percent2;
                MF_Recs.Discount_Amount2 = stMFRec.Discount_Amount2;
                MF_Recs.Discount_Percent = stMFRec.Discount_Percent;
                MF_Recs.Discount_Amount = stMFRec.Discount_Amount;
                MF_Recs.AmoiviAfter = stMFRec.AmoiviAfter;
                MF_Recs.AxiaAfter = stMFRec.AxiaAfter;
                MF_Recs.MinAmoivi = stMFRec.MinAmoivi;
                MF_Recs.MinAmoivi_Percent = stMFRec.MinAmoivi_Percent;
                MF_Recs.FinishMinAmoivi = stMFRec.FinishMinAmoivi;
                MF_Recs.LastAmount = stMFRec.LastAmount;
                MF_Recs.VAT_Percent = stMFRec.VAT_Percent;
                MF_Recs.VAT_Amount = stMFRec.VAT_Amount;
                MF_Recs.FinishAmount = stMFRec.FinishAmount;
                MF_Recs.LastAmount_Percent = stMFRec.LastAmount_Percent;
                MF_Recs.Invoice_External = stMFRec.Invoice_External;
                MF_Recs.DateEdit = DateTime.Now;
                MF_Recs.EditRecord();

                iFoundRows = iFoundRows + 1;
                decFoundAmount = decFoundAmount + decAUM;
            }
            else
            {
                sUnfoundRows = sUnfoundRows + "Code= '" + sCode + "' Portfolio = '" + sPortfolio + "'   Amount =" + decAUM.ToString() + (char)13 + (char)10;
            }

        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(sUnfoundRows, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void picClose_AUM_Click(object sender, EventArgs e)
        {
            panFilePath.Visible = false;
        }
        private void fgList_DoubleClick(object sender, EventArgs e)
        {
            if (fgList.Row > 1)
            {
                if (fgList.Col == 1) ShowInvoice();
                else ShowRecord(1);
            }
        }
        private void fgList_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 1) {
                if (fgList.Col == 0) {
                    if (Convert.ToBoolean(fgList[fgList.Row, 0])) {
                        if (fgList[fgList.Row, "FileName"].ToString() != "") {
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

        private void mnuContractData_Click(object sender, EventArgs e)
        {
            frmContract locContract = new frmContract();
            locContract.Aktion = 1;
            locContract.Contract_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_ID"]);
            locContract.Contract_Details_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_Details_ID"]);
            locContract.Contract_Packages_ID = Convert.ToInt32(fgList[fgList.Row, "Contract_Packages_ID"]);
            locContract.Client_ID = Convert.ToInt32(fgList[fgList.Row, "Client_ID"]);
            locContract.ClientFullName = fgList[fgList.Row, "ContractTitle"].ToString();
            locContract.RightsLevel = Convert.ToInt32(iRightsLevel);
            locContract.ShowDialog();
        }
        private void mnuClientData_Click(object sender, EventArgs e)
        {
            frmClientData locClientData = new frmClientData();
            locClientData.Client_ID = Convert.ToInt32(fgList[fgList.Row, "Client_ID"]);
            locClientData.Show();
        }
        private void fgList_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Row > 0)
            {
                if (Convert.ToInt32(fgList[e.Row, "User_ID"]) != 0) fgList.Rows[e.Row].Style = csChecked;
            }
        }
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            iAktion = 0;                                                    // 0 - Add, 1 - Edit
            ShowRecord(0);
            ucCS.Focus();
        }
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            iAktion = 1;                                                    // 0 - Add, 1 - Edit
            bRecalcPrices = false;
            if (fgList.Row > 1) ShowRecord(1);
            bRecalcPrices = true;
            ucCS.Focus();
        }
        private void mnuPistotiko_Click(object sender, EventArgs e)
        {
            iAktion = 0;                                                   // 0 - Add, 1 - Edit
            bRecalcPrices = false;
            if (fgList.Row > 1) ShowRecord(4);
            bRecalcPrices = true;
        }
        private void mnuAkyrotiko_Click(object sender, EventArgs e)
        {
            iAktion = 0;                                                   // 0 - Add, 1 - Edit
            bRecalcPrices = false;
            if (fgList.Row > 1) ShowRecord(5);
            bRecalcPrices = true;
        }
        private void mnuPrintInvoice_Click(object sender, EventArgs e)
        {
            fgList[fgList.Row,0 ] = true;
            PrintInvoice();
        }
        private void DefineList()
        {
            ManagmentFees_Recs.FT_ID = iFT_ID;
            ManagmentFees_Recs.GetList();
        }
        private void ShowList()
        {
            if (bCheckList) { 
                fgList.Redraw = false;
                fgList.Rows.Count = 2;
                int i = 0;

                foreach (DataRow dtRow in ManagmentFees_Recs.List.Rows)
                {
                    if (((Convert.ToInt32(cmbAdvisors.SelectedValue) == 0) || (Convert.ToInt32(dtRow["User1_ID"]) == Convert.ToInt32(cmbAdvisors.SelectedValue))) &&
                       ((cmbFilter.SelectedIndex < 1) || (cmbFilter.SelectedIndex == 1 && dtRow["Invoice_Num"].ToString() != "") || (cmbFilter.SelectedIndex == 2 && dtRow["Invoice_Num"].ToString() == "")) &&
                       (txtCode.Text.Trim() == "" || dtRow["Code"].ToString().Contains(txtCode.Text)))
                    {
                        if (Convert.ToInt32(dtRow["Invoice_Type"]) == 0)
                        {
                            if (Convert.ToInt32(dtRow["ClientType"]) == 1) dtRow["Invoice_Type"] = iInvoiceFisiko;          
                            else dtRow["Invoice_Type"] = iInvoiceNomiko;                                                         
                        }

                        i = i + 1;
                        fgList.AddItem(false + "\t" + Convert.ToInt16(dtRow["ImageType"]) + "\t" + i + "\t" + dtRow["DateFrom"] + "\t" + dtRow["DateTo"] + "\t" + dtRow["ContractTitle"] + "\t" +
                                       dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" + dtRow["Package_Title"] + "\t" + dtRow["Currency"] + "\t" + dtRow["Days"] + "\t" +
                                       dtRow["AUM"] + "\t" + dtRow["AmoiviPro"] + "\t" + dtRow["AxiaPro"] + "\t" + dtRow["Discount_Percent"] + "\t" + dtRow["AmoiviAfter"] + "\t" + 
                                       dtRow["AxiaAfter"] + "\t" + dtRow["Climakas"] + "\t" + dtRow["Discount_DateTo"] + "\t" + dtRow["MinAmoivi"] + "\t" + dtRow["MinAmoivi_Percent"] + "\t" + 
                                       dtRow["FinishMinAmoivi"] + "\t" + dtRow["LastAmount"] + "\t" + dtRow["VAT_Amount"] + "\t" + dtRow["FinishAmount"] + "\t" + 
                                       dtRow["LastAmount_Percent"] + "\t" + dtRow["Invoice_Num"] + "\t" + dtRow["DateFees"] + "\t" + dtRow["Notes"] + "\t" + dtRow["Service_Title"] + "\t" +
                                       dtRow["InvestmentPolicy"] + "\t" + dtRow["InvestmentProfile"] + "\t" + dtRow["Advisory_Name"] + "\t" +
                                       dtRow["RM_Name"] + "\t" + dtRow["Introducer_Name"] + "\t" + dtRow["Diaxiristis_Name"] + "\t" + dtRow["User1_Name"] + "\t" +
                                       dtRow["Address"] + "\t" + dtRow["City"] + "\t" + dtRow["Zip"] + "\t" + dtRow["Country_Title"] + "\t" + dtRow["AFM"] + "\t" + dtRow["DOY"] + "\t" +
                                       dtRow["ID"] + "\t" + dtRow["ClientType"] + "\t" + dtRow["Client_ID"] + "\t" + dtRow["Invoice_ID"] + "\t" + dtRow["Invoice_Type"] + "\t" + dtRow["L4"] + "\t" +
                                       dtRow["VAT_Percent"] + "\t" + dtRow["Invoice_External"] + "\t" + dtRow["Contract_ID"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" +
                                       dtRow["Contracts_Packages_ID"] + "\t" + dtRow["Service_ID"] + "\t" + dtRow["Status"] + "\t" + dtRow["AxiaPro"] + "\t" +
                                       dtRow["Discount_Percent1"] + "\t" + dtRow["Discount_Amount1"] + "\t" + dtRow["Discount_Percent2"] + "\t" + dtRow["Discount_Amount2"] + "\t" +
                                       dtRow["Discount_Amount"] + "\t" + dtRow["User1_ID"] + "\t" + dtRow["ConnectionMethod"] + "\t" + dtRow["Invoice_Arithmos"] + "\t" + dtRow["Invoice_File"] + "\t" +
                                       dtRow["MIFID_2"] + "\t" + dtRow["CountryEnglish"] + "\t" + dtRow["User_ID"] + "\t" + dtRow["Author_Name"] + "\t" + dtRow["DateEdit"]);
                    }
                }
                fgList.Redraw = true;
                DefineSums();
            }
        }
        private void DefineSums()
        {
            fgList.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, -1, 10, "");
            fgList.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, -1, 11, "");
            fgList.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, -1, 22, "");
            fgList.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, -1, 23, "");
            fgList.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, -1, 24, "");
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
        private void ShowRecord(int locMode)                // locMode: 0-Add new MF record , 1-Edit MF record, 3-Νέα χρέωση, 4-Πιστωτικό παραστατικό,  5-Ειδικό ακυρωτικό σημείωμα
        {
            sSeira = "";
            if (locMode == 0)
            {                
                iClient_ID = 0;
                iClientType = 0;
                lblContractTitle.Text = "";
                lblCode.Text = "";
                lblPortfolio.Text = "";
                lblPackage.Text = "";
                lblCurrency.Text = "";
                dFrom.Value = dStart;
                dTo.Value = dFinish;
                lblDays.Text = "90";
                txtAUM.Text = "0";
                lblAmoiviPro.Text = "0";
                lblAxiaPro.Text = "0";
                lblClimakas.Text = "";
                lblDiscount_DateTo.Text = "";
                txtDiscount_Percent1.Text = "0";
                txtDiscount_Amount1.Text = "0";
                txtDiscount_Percent2.Text = "0";
                txtDiscount_Amount2.Text = "0";
                lblDiscount_Percent.Text = "0";
                lblDiscount_Amount.Text = "0";
                lblAmoiviAfter.Text = "0";
                txtAxiaAfter.Text = "0";
                lblMinAmoivi.Text = "0";
                txtMinAmoivi_Percent.Text = "0";
                txtFinishMinAmoivi.Text = "0";
                txtLastAmount.Text = "0";
                txtVAT_Percent.Text = "0";
                txtVAT_Amount.Text = "0";
                txtFinishAmount.Text = "0";
                lblLastAmount_Percent.Text = "0";
                txtNotes.Text = "";
                lblUserName.Text = "";

                stMFRec.dFrom = dStart;
                stMFRec.dTo = dFinish;
                stMFRec.Days = 90;

                ucCS.StartInit(700, 400, 570, 20, 1);
                ucCS.Visible = true;
                lblContractTitle.Visible = false;

                iService_ID = 0;
                iContract_ID = 0;
                iContract_Details_ID = 0;
                iContract_Packages_ID = 0;
                hdnInvoice_Type.Text = "0";
                iAktion = 0;                               // 0 - Add, 1 - Edit
            }
            else
            {
                i = fgList.Row;
                iClient_ID = Convert.ToInt32(fgList[i, "Client_ID"]);
                iClientType = Convert.ToInt32(fgList[i, "ClientType"]);
                lblContractTitle.Text = fgList[i, "ContractTitle"].ToString();
                lblCode.Text = fgList[i, "Code"].ToString();
                lblPortfolio.Text = fgList[i, "Portfolio"].ToString();
                lblPackage.Text = fgList[i, "Package"].ToString();
                lblCurrency.Text = fgList[i, "Currency"].ToString();
                dFrom.Value = Convert.ToDateTime(fgList[i, "DateFrom"]);
                dTo.Value = Convert.ToDateTime(fgList[i, "DateTo"]);
                lblDays.Text = fgList[i, "Days"].ToString();
                txtAUM.Text = Convert.ToDecimal(fgList[i, "AUM"]).ToString("0.00");
                lblAmoiviPro.Text = fgList[i, "AmoiviPro"].ToString();
                lblClimakas.Text = fgList[i, "Climakas"].ToString();
                lblDiscount_DateTo.Text = fgList[i, "Discount_DateTo"].ToString();
                lblAxiaPro.Text = Convert.ToSingle(fgList[i, "AxiaPro"]).ToString("0.00");
                txtDiscount_Percent1.Text = fgList[i, "Discount_Percent1"].ToString();
                txtDiscount_Amount1.Text = fgList[i, "Discount_Amount1"].ToString();
                txtDiscount_Percent2.Text = fgList[i, "Discount_Percent2"].ToString();
                txtDiscount_Amount2.Text = fgList[i, "Discount_Amount2"].ToString();
                lblDiscount_Percent.Text = fgList[i, "Discount_Percent"].ToString();
                lblDiscount_Amount.Text = fgList[i, "Discount_Amount"].ToString();
                lblAmoiviAfter.Text = fgList[i, "AmoiviAfter"].ToString();
                txtAxiaAfter.Text = fgList[i, "AxiaAfter"].ToString();
                lblMinAmoivi.Text = fgList[i, "MinAmoivi"].ToString();
                txtMinAmoivi_Percent.Text = fgList[i, "MinAmoivi_Percent"].ToString();
                txtFinishMinAmoivi.Text = fgList[i, "FinishMinAmoivi"].ToString();
                txtLastAmount.Text = fgList[i, "LastAmount"].ToString();
                txtVAT_Percent.Text = fgList[i, "VAT_Percent"].ToString();
                txtVAT_Amount.Text = fgList[i, "VAT_Amount"].ToString();
                txtFinishAmount.Text = fgList[i, "FinishAmount"].ToString();
                lblLastAmount_Percent.Text = fgList[i, "LastAmount_Percent"].ToString();
                txtNotes.Text = fgList[i, "Notes"].ToString();
                sSeira = fgList[i, "Invoice_External"].ToString();
                lblUserName.Text = "";
                if (Convert.ToInt32(fgList[i, "User_ID"]) != 0) lblUserName.Text = fgList[i, "Author_Name"] + " " + fgList[i, "DateEdit"];

                ucCS.Visible = false;
                lblContractTitle.Visible = true;
                iService_ID = Convert.ToInt32(fgList[i, "Service_ID"]);
                iContract_ID = Convert.ToInt32(fgList[i, "Contract_ID"]);
                iContract_Details_ID = Convert.ToInt32(fgList[i, "Contract_Details_ID"]);
                iContract_Packages_ID = Convert.ToInt32(fgList[i, "Contract_Packages_ID"]);
                
                switch (locMode)
                {
                    case 1:
                        hdnInvoice_Type.Text = fgList[i, "Invoice_Type"].ToString();
                        iAktion = 1;                                                      // 0 - Add, 1 - Edit
                        break;
                    case 4:
                        hdnInvoice_Type.Text = "4";                                       // 4 - PISTOTIKO 
                        iAktion = 0;                                                      // 0 - Add, 1 - Edit
                        txtLastAmount.Text = "-" + txtLastAmount.Text;
                        txtVAT_Amount.Text = "-" + txtVAT_Amount.Text;
                        txtFinishAmount.Text = "-" + txtFinishAmount.Text;
                        break;
                    case 5: 
                        hdnInvoice_Type.Text = "5";                                       // 5 - AKYROTIKO 
                        iAktion = 0;                                                      // 0 - Add, 1 - Edit
                        txtLastAmount.Text = "-" + txtLastAmount.Text;
                        txtVAT_Amount.Text = "-" + txtVAT_Amount.Text;
                        txtFinishAmount.Text = "-" + txtFinishAmount.Text;

                        sSeira = "#";
                        switch (Convert.ToInt16(fgList[i, "Invoice_Type"]))
                        {
                            case 1:
                                sSeira = sSeira + sInvoiceTypeFisiko;
                                break;
                            case 2:
                                sSeira = sSeira + sInvoiceTypeNomiko;
                                break;
                            case 4:                                                      //  4 - ΠΙΣΤΩΤΙΚΟ ΤΙΜΟΛΟΓΙΟ
                                if (iClientType == 1) sSeira = sSeira + sInvoiceTypePistotikoFisiko;
                                else                  sSeira = sSeira + sInvoiceTypePistotikoNomiko;
                                break;
                            case 5:                                                      // 5 - ΑΚΥΡΩΤΙΚΟ ΣΗΜΕΙΩΜΑ
                                sSeira = sSeira + sInvoiceTypeAkyrotiko;
                                break;
                        }
                        sSeira = sSeira + "#" + fgList[i, "Invoice_Arithmos"].ToString() + "#";
                        break;
                }
            }
            hdnSeira.Text = sSeira;

            stMFRec.dFrom = dFrom.Value;
            stMFRec.dTo = dTo.Value;
            stMFRec.Days = Convert.ToInt32(lblDays.Text);
            stMFRec.Contract_ID = iContract_ID; ;
            stMFRec.Contract_Packages_ID = iContract_Packages_ID;
            stMFRec.Service_ID = iService_ID;
            stMFRec.AUM = Convert.ToDecimal(txtAUM.Text);
            stMFRec.AmoiviPro = Convert.ToSingle(lblAmoiviPro.Text);
            stMFRec.AxiaPro = Convert.ToSingle(lblAxiaPro.Text);
            stMFRec.Discount_DateTo = lblDiscount_DateTo.Text;
            stMFRec.Discount_Percent1 = Convert.ToSingle(txtDiscount_Percent1.Text);
            stMFRec.Discount_Amount1 = Convert.ToSingle(txtDiscount_Amount1.Text);
            stMFRec.Discount_Percent2 = Convert.ToSingle(txtDiscount_Percent2.Text);
            stMFRec.Discount_Amount2 = Convert.ToSingle(txtDiscount_Amount2.Text);
            stMFRec.Discount_Percent = Convert.ToSingle(lblDiscount_Percent.Text);
            stMFRec.Discount_Amount = Convert.ToSingle(lblDiscount_Amount.Text);
            stMFRec.AmoiviAfter = Convert.ToSingle(lblAmoiviAfter.Text);
            stMFRec.AxiaAfter = Convert.ToSingle(txtAxiaAfter.Text);
            stMFRec.Climakas = lblClimakas.Text;
            stMFRec.MinAmoivi = Convert.ToSingle(lblMinAmoivi.Text);
            stMFRec.MinAmoivi_Percent = Convert.ToSingle(txtMinAmoivi_Percent.Text);
            stMFRec.FinishMinAmoivi = Convert.ToDecimal(txtFinishMinAmoivi.Text);
            stMFRec.LastAmount = Convert.ToDecimal(txtLastAmount.Text);
            stMFRec.LastAmount_Percent = Convert.ToSingle(lblLastAmount_Percent.Text);
            stMFRec.VAT_Percent = Convert.ToSingle(txtVAT_Percent.Text);
            stMFRec.VAT_Amount = Convert.ToSingle(txtVAT_Amount.Text);
            stMFRec.FinishAmount = Convert.ToDecimal(txtFinishAmount.Text);

            ShowFeesTable();
            panEdit.Visible = true;
        }
        private void ShowFeesTable()
        {
            switch (iService_ID)
            {
                case 0:
                    fgFees.Rows.Count = 2;
                    lblMonthMinAmount.Text = "";
                    lblMonthMinCurrency.Text = "";
                    lblMonth3_Discount.Text = "";
                    lblMonth3_Fees.Text = "";
                    break;
                case 2:
                    //------------------ initialize Advisory Fees ------------------
                    clsContracts ContractAdv = new clsContracts();
                    ContractAdv.Record_ID = iContract_ID;
                    ContractAdv.Contract_Details_ID = iContract_Details_ID;
                    ContractAdv.Contract_Packages_ID = iContract_Packages_ID;
                    ContractAdv.GetRecord();
                    lblMonthMinAmount.Text = ContractAdv.Advisory_MonthMinAmount + "";
                    lblMonthMinCurrency.Text = ContractAdv.Advisory_MonthMinCurr + "";
                    lblMonth3_Discount.Text = ContractAdv.Advisory_Month3_Discount + "";
                    lblMonth3_Fees.Text = ContractAdv.Advisory_Month3_Fees + "";

                    fgFees.Redraw = false;
                    fgFees.Rows.Count = 2;
                    clsClientsAdvisoryFees ClientsAdvisoryFees = new clsClientsAdvisoryFees();
                    ClientsAdvisoryFees.ServiceProvider_ID = ContractAdv.AdvisoryServiceProvider_ID;
                    ClientsAdvisoryFees.Option_ID = ContractAdv.AdvisoryOption_ID;
                    ClientsAdvisoryFees.InvestmentPolicy_ID = ContractAdv.AdvisoryInvestmentPolicy_ID;
                    ClientsAdvisoryFees.InvestmentProfile_ID = ContractAdv.AdvisoryInvestmentProfile_ID;
                    ClientsAdvisoryFees.DateFrom = dFrom.Value;
                    ClientsAdvisoryFees.DateTo = dTo.Value;
                    ClientsAdvisoryFees.Contract_ID = iContract_ID;
                    ClientsAdvisoryFees.Contract_Packages_ID = iContract_Packages_ID;
                    ClientsAdvisoryFees.GetList_Package_ID();
                    foreach (DataRow dtRow in ClientsAdvisoryFees.List.Rows)
                    {
                        fgFees.AddItem(dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["AdvisoryFees"] + "\t" +
                                       dtRow["DiscountDateFrom"] + "\t" + dtRow["DiscountDateTo"] + "\t" +
                                       dtRow["AdvisoryFees_Discount"] + "\t" + dtRow["FinishAdvisoryFees"] + "\t" + dtRow["ID"]);
                    }
                    fgFees.Redraw = true;
                    break;
                case 3:
                    //------------------ initialize Discret Fees ------------------
                    clsContracts ContractDis = new clsContracts();
                    ContractDis.Record_ID = iContract_ID;
                    ContractDis.Contract_Details_ID = iContract_Details_ID;
                    ContractDis.Contract_Packages_ID = iContract_Packages_ID;
                    ContractDis.GetRecord();
                    lblMonthMinAmount.Text = ContractDis.Discret_MonthMinAmount + "";
                    lblMonthMinCurrency.Text = ContractDis.Discret_MonthMinCurr + "";
                    lblMonth3_Discount.Text = ContractDis.Discret_Month3_Discount + "";
                    lblMonth3_Fees.Text = ContractDis.Discret_Month3_Fees + "";

                    fgFees.Redraw = false;
                    fgFees.Rows.Count = 2;
                    clsClientsDiscretFees ClientsDiscretFees = new clsClientsDiscretFees();
                    ClientsDiscretFees.ServiceProvider_ID = ContractDis.DiscretServiceProvider_ID;
                    ClientsDiscretFees.Option_ID = ContractDis.DiscretOption_ID;
                    ClientsDiscretFees.InvestmentPolicy_ID = ContractDis.DiscretInvestmentPolicy_ID;
                    ClientsDiscretFees.InvestmentPolicy_ID = ContractDis.DiscretInvestmentProfile_ID;
                    ClientsDiscretFees.DateFrom = dFrom.Value;
                    ClientsDiscretFees.DateTo = dTo.Value;
                    ClientsDiscretFees.Contract_ID = iContract_ID;
                    ClientsDiscretFees.Contract_Packages_ID = iContract_Packages_ID;
                    ClientsDiscretFees.GetList_Package_ID();
                    foreach (DataRow dtRow in ClientsDiscretFees.List.Rows)
                    {
                        fgFees.AddItem(dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["DiscretFees"] + "\t" +
                                       dtRow["DiscountDateFrom"] + "\t" + dtRow["DiscountDateTo"] + "\t" +
                                       dtRow["DiscretFees_Discount"] + "\t" + dtRow["FinishDiscretFees"] + "\t" + dtRow["ID"]);
                    }
                    fgFees.Redraw = true;
                    break;
                case 5:
                    //------------------ initialize DealAdvisory Fees ------------------
                    clsContracts ContractDAdv = new clsContracts();
                    ContractDAdv.Record_ID = iContract_ID;
                    ContractDAdv.Contract_Details_ID = iContract_Details_ID;
                    ContractDAdv.Contract_Packages_ID = iContract_Packages_ID;
                    ContractDAdv.GetRecord();

                    fgFees.Redraw = false;
                    fgFees.Rows.Count = 2;
                    clsClientsDealAdvisoryFees ClientsDealAdvisoryFees = new clsClientsDealAdvisoryFees();
                    ClientsDealAdvisoryFees.ServiceProvider_ID = ContractDAdv.DealAdvisoryServiceProvider_ID;
                    ClientsDealAdvisoryFees.Option_ID = ContractDAdv.DealAdvisoryOption_ID;
                    ClientsDealAdvisoryFees.InvestmentPolicy_ID = ContractDAdv.DealAdvisoryInvestmentPolicy_ID;
                    ClientsDealAdvisoryFees.DateFrom = dFrom.Value;
                    ClientsDealAdvisoryFees.DateTo = dTo.Value;
                    ClientsDealAdvisoryFees.Contract_ID = iContract_ID;
                    ClientsDealAdvisoryFees.Contract_Packages_ID = iContract_Packages_ID;
                    ClientsDealAdvisoryFees.GetList_Package_ID();
                    foreach (DataRow dtRow in ClientsDealAdvisoryFees.List.Rows)
                    {
                        fgFees.AddItem(dtRow["AmountFrom"] + "\t" + dtRow["AmountTo"] + "\t" + dtRow["DealAdvisoryFees"] + "\t" +
                                       dtRow["DiscountDateFrom"] + "\t" + dtRow["DiscountDateTo"] + "\t" +
                                       dtRow["DealAdvisoryFees_Discount"] + "\t" + dtRow["FinishDealAdvisoryFees"] + "\t" + dtRow["ID"]);
                    }
                    fgFees.Redraw = true;
                    break;
            }
        }
        protected void ucCS_TextChanged(object sender, EventArgs e)
        {
            //handle the event 
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

            stMFRec.Service_ID = stContractData.Service_ID;
            stMFRec.Contract_ID = stContractData.Contract_ID;
            //stMFRec.Contract_Details_ID = stContractData.Contracts_Details_ID;
            stMFRec.Contract_Packages_ID = stContractData.Contracts_Packages_ID;
            stMFRec.VAT_Percent = stContractData.VAT_Percent;

            ShowFeesTable();

            dFrom.Focus();
        }
        protected void ucCS_ButtonClick(object sender, EventArgs e)
        {
            // этот модуль должен быть в другой форме - как пример реакции на нажатие клавиши Search в ucContracts @@@@@@@@@  
        }
        //--- Import file wiath AdminFees Data functions ------------------------------------
        private void tsbImport_Click(object sender, EventArgs e)
        {
            lblYear.Text = cmbYear.Text;
            rbc1.Checked = rb1.Checked;
            rbc2.Checked = rb2.Checked;
            rbc3.Checked = rb3.Checked;
            rbc4.Checked = rb4.Checked;
            panImport.Visible = true;
        }
        private void picFilesPath_Import_Click(object sender, EventArgs e)
        {
            txtFilePath_Import.Text = Global.FileChoice(Global.DefaultFolder);
        }

        private void btnGetImport_Click(object sender, EventArgs e)
        {
            if (txtFilePath_Import.Text.Length > 0)
            {
                int iIndex = 0;
                string sTemp = "";

                iFT_ID = 0;

                if (rbc1.Checked) {
                    iIndex = 1;
                    dStart = Convert.ToDateTime("01-01-" + cmbYear.Text);
                    dFinish = Convert.ToDateTime("31-03-" + cmbYear.Text);
                }
                if (rbc2.Checked) {
                    iIndex = 2;
                    dStart = Convert.ToDateTime("01-04-" + cmbYear.Text);
                    dFinish = Convert.ToDateTime("30-06-" + cmbYear.Text);
                }
                if (rbc3.Checked) {
                    iIndex = 3;
                    dStart = Convert.ToDateTime("01-07-" + cmbYear.Text);
                    dFinish = Convert.ToDateTime("30-09-" + cmbYear.Text);
                }
                if (rbc4.Checked) {
                    iIndex = 4;
                    dStart = Convert.ToDateTime("01-10-" + cmbYear.Text);
                    dFinish = Convert.ToDateTime("31-12-" + cmbYear.Text);
                }

                clsManagmentFees_Titles klsManagmentFees_Title = new clsManagmentFees_Titles();
                klsManagmentFees_Title.SC_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                klsManagmentFees_Title.MF_Year = Convert.ToInt32(cmbYear.Text);
                klsManagmentFees_Title.MF_Quart = iIndex;
                klsManagmentFees_Title.GetRecord_Title();
                iFT_ID = klsManagmentFees_Title.Record_ID;
                if (iFT_ID == 0)
                {
                    var ExApp = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook xlWorkbook = ExApp.Workbooks.Open(txtFilePath_Import.Text);
                    Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                    Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

                    klsManagmentFees_Title.SC_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                    klsManagmentFees_Title.MF_Quart = iIndex;
                    klsManagmentFees_Title.MF_Year = Convert.ToInt32(cmbYear.Text);
                    klsManagmentFees_Title.DateIns = DateTime.Now;
                    klsManagmentFees_Title.Author_ID = Global.User_ID;
                    iFT_ID = klsManagmentFees_Title.InsertRecord();

                    this.Refresh();
                    this.Cursor = Cursors.WaitCursor;

                    i = 1;
                    while (true)
                    {
                        i = i + 1;

                        sTemp = (xlRange.Cells[i, 2].Value + "").ToString();
                        if (sTemp == "") break;

                        clsContracts klsContract = new clsContracts();
                        klsContract.Code = xlRange.Cells[i, 1].Value.ToString();
                        klsContract.Portfolio = xlRange.Cells[i, 2].Value.ToString();
                        klsContract.GetRecord_Code_Portfolio();

                        clsManagmentFees_Recs MF_Recs = new clsManagmentFees_Recs();
                        MF_Recs.FT_ID = iFT_ID;
                        MF_Recs.Client_ID = Convert.ToInt32(klsContract.Client_ID);
                        MF_Recs.DateFrom = dStart;
                        MF_Recs.DateTo = dFinish;
                        MF_Recs.Code = xlRange.Cells[i, 1].Value.ToString();
                        MF_Recs.Portfolio = xlRange.Cells[i, 2].Value.ToString();
                        MF_Recs.Currency = klsContract.Currency + "";
                        MF_Recs.Contract_ID = Convert.ToInt32(klsContract.Record_ID);
                        MF_Recs.Contract_Details_ID = Convert.ToInt32(klsContract.Contract_Details_ID);
                        MF_Recs.Contract_Packages_ID = Convert.ToInt32(klsContract.Contract_Packages_ID);
                        MF_Recs.AUM = Convert.ToDecimal(xlRange.Cells[i, 23].Value);
                        MF_Recs.Days = 90;
                        MF_Recs.AmoiviPro = Convert.ToSingle(xlRange.Cells[i, 17].Value * 100);
                        MF_Recs.AxiaPro = 0;
                        MF_Recs.Climakas = "";
                        MF_Recs.Discount_DateFrom = "";
                        MF_Recs.Discount_DateTo = "";
                        MF_Recs.Discount_Percent1 = 0;
                        MF_Recs.Discount_Amount1 = 0;
                        MF_Recs.Discount_Percent2 = 0;
                        MF_Recs.Discount_Amount2 = 0;
                        MF_Recs.Discount_Percent = 0;
                        MF_Recs.Discount_Amount = 0;
                        MF_Recs.AmoiviAfter = Convert.ToSingle(xlRange.Cells[i, 17].Value * 100);
                        MF_Recs.AxiaAfter = Convert.ToSingle(xlRange.Cells[i, 19].Value);
                        MF_Recs.MinAmoivi = 0;
                        MF_Recs.MinAmoivi_Percent = 0;
                        MF_Recs.FinishMinAmoivi = 0;
                        MF_Recs.LastAmount = Convert.ToDecimal(xlRange.Cells[i, 19].Value);
                        MF_Recs.LastAmount_Percent = 0;
                        MF_Recs.VAT_Amount = Convert.ToSingle(xlRange.Cells[i, 20].Value);
                        MF_Recs.VAT_Percent = Convert.ToSingle(xlRange.Cells[i, 18].Value * 100);
                        MF_Recs.FinishAmount = Convert.ToDecimal(xlRange.Cells[i, 21].Value);
                        MF_Recs.Service_ID = klsContract.Service_ID;
                        MF_Recs.Invoice_ID = 0;
                        MF_Recs.Invoice_Num = xlRange.Cells[i, 14].Value.ToString();
                        MF_Recs.Invoice_File = "";
                        MF_Recs.DateFees = Convert.ToDateTime("1900/01/01");
                        MF_Recs.Invoice_Type = 0;
                        MF_Recs.Notes = xlRange.Cells[i, 16].Value.ToString();
                        MF_Recs.Invoice_External = "";
                        MF_Recs.Status = 1;                                                      // 1 - Active, 2 - Cancelled
                        MF_Recs.User_ID = Global.User_ID;
                        MF_Recs.DateEdit = DateTime.Now;
                        MF_Recs.InsertRecord();

                        /*
                        clsContracts_Details ContractDetails = new clsContracts_Details();
                        ContractDetails.Record_ID = Convert.ToInt32(klsContract.Contract_Details_ID);
                        ContractDetails.GetRecord();

                        ContractDetails.InvName = xlRange.Cells[i, 6].Value.ToString();
                        ContractDetails.InvAddress = xlRange.Cells[i, 7].Value.ToString();
                        ContractDetails.InvCity = xlRange.Cells[i, 8].Value.ToString();
                        ContractDetails.InvZip = xlRange.Cells[i, 9].Value.ToString();
                        ContractDetails.InvDOY = xlRange.Cells[i, 12].Value.ToString();
                        ContractDetails.InvAFM = xlRange.Cells[i, 11].Value.ToString();

                        ContractDetails.EditRecord();
                        */
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
                    MessageBox.Show("Snapshot για αυτό το περίοδο ήδη υπάρχει", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
                MessageBox.Show("Καταχώρήστε το αρχείο εισαγωγής", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void picClose_Import_Click(object sender, EventArgs e)
        {
            panImport.Visible = false;
        }
        //-----------------------------------------------------------------------------------

        private void dFrom_LostFocus(object sender, EventArgs e)
        {
            CalcFees_Step1();
            CalcFees_Step2();
            ShowNewValues();
        }
        private void dTo_LostFocus(object sender, EventArgs e)
        {
            CalcFees_Step1();
            CalcFees_Step2();
            ShowNewValues();
        }
        private void txtAUM_LostFocus(object sender, EventArgs e)
        {
            stMFRec.AUM = Convert.ToDecimal(txtAUM.Text);
            CalcFees_Step1();
            CalcFees_Step2();
            ShowNewValues();
        }
        private void txtDiscount_Percent1_LostFocus(object sender, EventArgs e)
        {
            stMFRec.Discount_Percent1 = Convert.ToSingle(txtDiscount_Percent1.Text);
            CalcFees_Step2();
            ShowNewValues();
        }
        private void txtDiscount_Amount1_LostFocus(object sender, EventArgs e)
        {
            stMFRec.Discount_Amount1 = Convert.ToSingle(txtDiscount_Amount1.Text);
            CalcFees_Step2();
            ShowNewValues();
        }
        private void txtDiscount_Percent2_LostFocus(object sender, EventArgs e)
        {
            stMFRec.Discount_Percent2 = Convert.ToSingle(txtDiscount_Percent2.Text);
            CalcFees_Step2();
            ShowNewValues();
        }
        private void txtDiscount_Amount2_LostFocus(object sender, EventArgs e)
        {
            stMFRec.Discount_Amount2 = Convert.ToSingle(txtDiscount_Amount2.Text);
            CalcFees_Step2();
            ShowNewValues();
        }
        private void txtAxiaAfter_LostFocus(object sender, EventArgs e)
        {
            stMFRec.AxiaAfter = Convert.ToSingle(txtAxiaAfter.Text);
            CalcFees_Step2();
            ShowNewValues();
        }
        private void txtLastAmount_LostFocus(object sender, EventArgs e)
        {
            stMFRec.LastAmount = Convert.ToDecimal(txtLastAmount.Text);
            stMFRec.VAT_Amount = (float)Math.Round((float)stMFRec.LastAmount * stMFRec.VAT_Percent / 100, 2);                                                  
            stMFRec.FinishAmount = Math.Round(stMFRec.LastAmount + (decimal)stMFRec.VAT_Amount, 2);                                                            
            ShowNewValues();
        }
        private void txtVAT_Percent_LostFocus(object sender, EventArgs e)
        {
            stMFRec.VAT_Percent = Convert.ToSingle(txtVAT_Percent.Text);
            stMFRec.VAT_Amount = (float)Math.Round((float)stMFRec.LastAmount * stMFRec.VAT_Percent / 100, 2);
            stMFRec.FinishAmount = Math.Round(stMFRec.LastAmount + (decimal)stMFRec.VAT_Amount, 2);
            ShowNewValues();
        }
        private void txtVAT_Amount_LostFocus(object sender, EventArgs e)
        {
            stMFRec.VAT_Amount = Convert.ToSingle(txtVAT_Amount.Text);
            stMFRec.FinishAmount = Math.Round(stMFRec.LastAmount + (decimal)stMFRec.VAT_Amount, 2);
            ShowNewValues();
        }
        private void CalcFees_Step1()
        {
            int i = 0;

            i = Convert.ToInt32((stMFRec.dTo - stMFRec.dFrom).TotalDays) + 1;
            if (i > 90) i = 90;
            stMFRec.Days = i;
            lblDays.Text = i.ToString();

            switch (iService_ID)
            {
                case 2:
                    clsClientsAdvisoryFees ContractAdvisoryFees = new clsClientsAdvisoryFees();
                    ContractAdvisoryFees.AUM = stMFRec.AUM; 
                    ContractAdvisoryFees.Contract_ID = stMFRec.Contract_ID;
                    ContractAdvisoryFees.Contract_Packages_ID = stMFRec.Contract_Packages_ID;
                    ContractAdvisoryFees.DateFrom = stMFRec.dFrom;
                    ContractAdvisoryFees.DateTo = stMFRec.dTo;
                    ContractAdvisoryFees.Days = stMFRec.Days;
                    ContractAdvisoryFees.GetList_FeesData();
                    stMFRec.AmoiviPro = (float)Math.Round(ContractAdvisoryFees.FeesPercent, 4);
                    stMFRec.AxiaPro = (float)Math.Round(ContractAdvisoryFees.StartAmount, 2);
                    stMFRec.Discount_Percent1 = (float)Math.Round(ContractAdvisoryFees.Discount_Percent, 4);
                    stMFRec.Discount_Amount1 = (float)Math.Round(ContractAdvisoryFees.Discount_Amount, 2);
                    break;
                case 3:
                    clsClientsDiscretFees ContractDiscretFees = new clsClientsDiscretFees();
                    ContractDiscretFees.AUM = stMFRec.AUM;
                    ContractDiscretFees.Contract_ID = stMFRec.Contract_ID;
                    ContractDiscretFees.Contract_Packages_ID = stMFRec.Contract_Packages_ID;
                    ContractDiscretFees.DateFrom = stMFRec.dFrom;
                    ContractDiscretFees.DateTo = stMFRec.dTo;
                    ContractDiscretFees.Days = stMFRec.Days;
                    ContractDiscretFees.GetList_FeesData();
                    stMFRec.AmoiviPro = (float)Math.Round(ContractDiscretFees.FeesPercent, 4);
                    stMFRec.AxiaPro = (float)Math.Round(ContractDiscretFees.StartAmount, 2);
                    stMFRec.Discount_Percent1 = (float)Math.Round(ContractDiscretFees.Discount_Percent, 4);
                    stMFRec.Discount_Amount1 = (float)Math.Round(ContractDiscretFees.Discount_Amount, 2);
                    break;
                case 5:
                    clsClientsDealAdvisoryFees ContractDealAdvisoryFees = new clsClientsDealAdvisoryFees();
                    ContractDealAdvisoryFees.AUM = stMFRec.AUM;
                    ContractDealAdvisoryFees.Contract_ID = stMFRec.Contract_ID;
                    ContractDealAdvisoryFees.Contract_Packages_ID = stMFRec.Contract_Packages_ID;
                    ContractDealAdvisoryFees.DateFrom = stMFRec.dFrom;
                    ContractDealAdvisoryFees.DateTo = stMFRec.dTo;
                    ContractDealAdvisoryFees.Days = stMFRec.Days;
                    ContractDealAdvisoryFees.GetList_FeesData();
                    stMFRec.AmoiviPro = (float)Math.Round(ContractDealAdvisoryFees.FeesPercent, 4);
                    stMFRec.AxiaPro = (float)Math.Round(ContractDealAdvisoryFees.StartAmount, 2);
                    stMFRec.Discount_Percent1 = (float)Math.Round(ContractDealAdvisoryFees.Discount_Percent, 4);
                    stMFRec.Discount_Amount1 = (float)Math.Round(ContractDealAdvisoryFees.Discount_Amount, 2);
                    break;
            }
        }
        private void CalcFees_Step2()
        {
            if (bRecalcPrices)
            { 
                stMFRec.Discount_Amount1 = (float)Math.Round(stMFRec.Discount_Percent1 * stMFRec.AxiaPro / 100, 2);                                                // Axia ekptosis 1
                stMFRec.Discount_Amount2 = (float)Math.Round(stMFRec.Discount_Percent2 * stMFRec.AxiaPro / 100, 2);                                                // Axia ekptosis 2
                stMFRec.Discount_Percent = (float)Math.Round(stMFRec.Discount_Percent1 + stMFRec.Discount_Percent2, 4);                                            // % Ekptosis    Synoliki
                stMFRec.Discount_Amount = (float)Math.Round(stMFRec.Discount_Amount1 + stMFRec.Discount_Amount2, 2);                                               // Axia ekptosis Synoliki  
                stMFRec.AxiaAfter = (float)Math.Round(stMFRec.AxiaPro - stMFRec.Discount_Amount, 2);                                                               // Poso meta tin ekptosis

                stMFRec.AmoiviAfter = 0;
                if (stMFRec.AxiaPro != 0) stMFRec.AmoiviAfter = (float)Math.Round((stMFRec.AxiaAfter * 36000) / (float)(stMFRec.AUM * stMFRec.Days), 4);

                if (stMFRec.FinishMinAmoivi > (decimal)stMFRec.AxiaAfter) stMFRec.LastAmount = Math.Round(stMFRec.FinishMinAmoivi, 2);
                else stMFRec.LastAmount = (decimal)Math.Round(stMFRec.AxiaAfter, 2);

                stMFRec.LastAmount_Percent = 0;
                if (stMFRec.AUM != 0 && stMFRec.Days != 0) stMFRec.LastAmount_Percent = (float)Math.Round((float)(stMFRec.LastAmount * 36000) / (float)(stMFRec.AUM * stMFRec.Days), 4);

                stMFRec.VAT_Amount = (float)Math.Round((float)stMFRec.LastAmount * stMFRec.VAT_Percent / 100, 2);                                                  // FPA
                stMFRec.FinishAmount = Math.Round(stMFRec.LastAmount + (decimal)stMFRec.VAT_Amount, 2);                                                            // Teliko poso - Poso me FPA
            }
        }
        private void ShowNewValues()
        {
            lblAmoiviPro.Text = stMFRec.AmoiviPro.ToString();
            lblAxiaPro.Text = stMFRec.AxiaPro.ToString();
            txtDiscount_Percent1.Text = stMFRec.Discount_Percent1.ToString();
            txtDiscount_Amount1.Text = stMFRec.Discount_Amount1.ToString();
            txtDiscount_Percent2.Text = stMFRec.Discount_Percent2.ToString();
            txtDiscount_Amount2.Text = stMFRec.Discount_Amount2.ToString();
            lblDiscount_Percent.Text = stMFRec.Discount_Percent.ToString();
            lblDiscount_Amount.Text = stMFRec.Discount_Amount.ToString();
            lblAmoiviAfter.Text = stMFRec.AmoiviAfter.ToString();
            txtAxiaAfter.Text = stMFRec.AxiaAfter.ToString();
            lblMinAmoivi.Text = stMFRec.MinAmoivi.ToString();
            txtMinAmoivi_Percent.Text = stMFRec.MinAmoivi_Percent.ToString();
            txtFinishMinAmoivi.Text = stMFRec.FinishMinAmoivi.ToString();
            txtLastAmount.Text = stMFRec.LastAmount.ToString();
            lblLastAmount_Percent.Text = stMFRec.LastAmount_Percent.ToString();
            txtVAT_Percent.Text = stMFRec.VAT_Percent.ToString();
            txtVAT_Amount.Text = stMFRec.VAT_Amount.ToString();
            txtFinishAmount.Text = stMFRec.FinishAmount.ToString();
        }
        private void DefineOptions()
        {
            clsOptions Options = new clsOptions();
            Options.GetRecord();
            sExportFilePath = Global.DocFilesPath_Win;
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

            sInvoiceMFTemplate = Options.InvoiceMFTemplate;
            sInvoiceMFAnalysisTemplate = Options.InvoiceMFAnalysisTemplate;            
        }
        private void tsbPrint_Click(object sender, EventArgs e)
        {
            PrintInvoice();
        }
        private void PrintInvoice()
        {
            sInvoicePrinter = Global.InvoicePrinter;
            frmPrintInvoiceOptions PrintInvoiceOptions = new frmPrintInvoiceOptions();
            PrintInvoiceOptions.InvoicePrinter = Global.InvoicePrinter;
            PrintInvoiceOptions.NumCopies = iCopies;
            PrintInvoiceOptions.DateIssue = DateTime.Now;
            PrintInvoiceOptions.ShowDialog();
            if (PrintInvoiceOptions.LastAktion == 1)
            {
                sInvoicePrinter = PrintInvoiceOptions.InvoicePrinter;
                iCopies = PrintInvoiceOptions.NumCopies;
                dIssueDate = PrintInvoiceOptions.DateIssue;

                if (Convert.ToInt32(cmbServiceProviders.SelectedValue) == 9) PrintingInvoices_HFS();                // DELETE THIS ROW - it's only for HellasFin Custody
                else PrintingInvoices();
            }
        }
        private void PrintingInvoices()
        {
            int iLine;
            string sTemp, sPDF_FullPath, sInvoiceCode, sAitiologia, sApo, sEos,
                   sInvTitleGr, sInvTitleEn, sCountry, sProfileGr, sProfileEn, sProfile, sNewFile, sLastFileName, sNum, sInvType, sEafdss;
            var WordApp = new Microsoft.Office.Interop.Word.Application();
            var curDoc = new Microsoft.Office.Interop.Word.Document();

            bCheckList = false;
            iInvoiceType = 0;
            iClientType = 0;
            iNum = 0;
            iLine = 0;
            sNum = "";
            sSeira = "";
            sAitiologia = "";
            sApo = "";
            sEos = "";
            sPDF_FullPath = "";
            sInvTitleGr = "";
            sInvTitleEn = "";
            sInvoiceCode = "";
            sCountry = "";
            sInvType = "";
            sEafdss = "";
            sAitiologia = "";
            sProfileGr = "";
            sProfileEn = "";
            sProfile = "";

            try
            {
                for (iLine = 2; iLine <= (fgList.Rows.Count - 1); iLine++)
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
                                    sSeira = fgList[iLine, "Invoice_External"].ToString();
                                    sInvTitleGr = sInvTitleAkyrotikoGr;
                                    sInvTitleEn = sInvTitleAkyrotikoEn;
                                    sInvType = sInvoiceTypeAkyrotiko;
                                    break;
                        }

                        sApo = Convert.ToDateTime(fgList[iLine, "DateFrom"]).ToString("dd/MM/yyyy");
                        sEos = Convert.ToDateTime(fgList[iLine, "DateTo"]).ToString("dd/MM/yyyy");

                        clsInvoiceTitles InvoiceTitles = new clsInvoiceTitles();
                        InvoiceTitles.Tipos = iInvoiceType;
                        InvoiceTitles.Seira = sSeira;
                        iNum = Convert.ToInt32(InvoiceTitles.GetInvoice_LastNumber()) + 1;

                        switch (Convert.ToInt32(fgList[iLine, "Service_ID"]))                       
                        {
                            case 2:                                                                 // 2 - Advisory
                                    sAitiologia = "Αμοιβή Επενδυτικών Συμβουλών";
                                    break;
                            case 3:                                                                 // 3 - Discretionary
                                    sAitiologia = "Αμοιβή Διαχείρισης";
                                    break;
                            default:
                                    sAitiologia = "Αμοιβή Επενδυτικών Συμβουλών";
                                    break;
                        }

                        // --- Country : Greece or Not ------------
                        if (fgList[iLine, "Country"].ToString() == "" || fgList[iLine, "Country"].ToString() == "Ελλάδα" || fgList[iLine, "Country"].ToString() == "Greece") sCountry = fgList[iLine, "Country"].ToString();
                        else sCountry = fgList[iLine, "CountryEnglish"].ToString();


                        if (Convert.ToInt32(fgList[iLine, "MiFID_2"]) == 1)
                        {
                            sProfileGr = "Επενδυτικό Προφίλ";
                            sProfileEn = "Investment Profile";
                            sProfile = fgList[iLine, "InvestmentProfile"] + "";
                        }

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

                        WordApp.Visible = false;

                        // --- check Temp folder  -------------
                        sPDF_FullPath = Application.StartupPath + "\\Temp";
                        if (!Directory.Exists(sPDF_FullPath))  Directory.CreateDirectory(sPDF_FullPath);

                        sTemp = sPDF_FullPath + "\\ManF_" + sNum + ".docx";
                        if (File.Exists(sTemp)) File.Delete(sTemp);

                        File.Copy(Application.StartupPath + "\\Templates\\" + sInvoiceMFTemplate, sTemp);
                        curDoc = WordApp.Documents.Open(sTemp);

                        sEafdss = "<%SL ;;" + fgList[iLine, "AFM"] + ";;;;;;" + sInvType + ";;" + sNum + ";0;0;" + Math.Abs(Convert.ToDecimal(fgList[iLine, "LastAmount"])) + 
                                  ";0;0;0;0;" + Math.Abs(Convert.ToSingle(fgList[iLine, "VAT_Amount"])) + ";0;" + Math.Abs(Convert.ToDecimal(fgList[iLine, "FinishAmount"])) + ";" + "EUR" + ";>";

                        curDoc.Content.Find.Execute(FindText: "{title_gr}", ReplaceWith: sInvTitleGr, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{title_en}", ReplaceWith: sInvTitleEn, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{code}", ReplaceWith: fgList[iLine, "Code"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{portfolio}", ReplaceWith: fgList[iLine, "Portfolio"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{contract_title}", ReplaceWith: fgList[iLine, "ContractTitle"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{invest_services}", ReplaceWith: fgList[iLine, "ServiceTitle"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{profile_gr}", ReplaceWith: sProfileGr, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{profile_en}", ReplaceWith: sProfileEn, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{invest_profile}", ReplaceWith: sProfile, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{surname}", ReplaceWith: fgList[iLine, "User1Name"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{firstname}", ReplaceWith: "", Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{address}", ReplaceWith: fgList[iLine, "Address"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{city}", ReplaceWith: fgList[iLine, "City"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{zip}", ReplaceWith: fgList[iLine, "Zip"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{country}", ReplaceWith: sCountry, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{AFM}", ReplaceWith: fgList[iLine, "AFM"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{DOY}", ReplaceWith: fgList[iLine, "DOY"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{invoice_num}", ReplaceWith: iNum, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{issue_date}", ReplaceWith: dIssueDate.ToString("dd/MM/yyyy"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{mfaitiologia_gr}", ReplaceWith: sAitiologia, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{apo}", ReplaceWith: sApo, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{eos}", ReplaceWith: sEos, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{amount}", ReplaceWith: Math.Abs(Convert.ToDecimal(fgList[iLine, "LastAmount"])).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{fpa}", ReplaceWith: Math.Abs(Convert.ToSingle(fgList[iLine, "VAT_Percent"])).ToString(), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{vat}", ReplaceWith: Math.Abs(Convert.ToSingle(fgList[iLine, "VAT_Amount"])).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{axia}", ReplaceWith: Math.Abs(Convert.ToDecimal(fgList[iLine, "FinishAmount"])).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{eafdss}", ReplaceWith: sEafdss, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);

                        sNewFile = sPDF_FullPath + "\\InvoiceMF_" + sNum + ".pdf";
                        curDoc.SaveAs2(sNewFile, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF);
                        WordApp.ScreenUpdating = false;
                        WordApp.Documents.Close();

                        sLastFileName = Global.DMS_UploadFile(sNewFile, "Customers/" + fgList[iLine, "ContractTitle"] + "/Invoices", Path.GetFileName(sNewFile));

                        iID = SaveRecord(iLine, iInvoiceType, sSeira, iNum, Path.GetFileName(sLastFileName), Convert.ToInt32(fgList[iLine, "ID"]), Convert.ToInt32(fgList[iLine, "Contract_ID"]));

                        fgList[iLine, 0] = false;
                        fgList[iLine, 1] = 1;
                        fgList[iLine, "Invoice_Num"] = sInvoiceCode + " " + (sSeira + " " + iNum).Trim();
                        fgList[iLine, "FileName"] = Path.GetFileName(sLastFileName);
                        fgList.Refresh();

                        clsManagmentFees_Recs MF_Recs = new clsManagmentFees_Recs();
                        MF_Recs.Record_ID = Convert.ToInt32(fgList[iLine, "ID"]);
                        MF_Recs.GetRecord();
                        MF_Recs.Invoice_ID = iID;
                        MF_Recs.Invoice_Num = fgList[iLine, "Invoice_Num"].ToString();
                        MF_Recs.Invoice_File = Path.GetFileName(sLastFileName);
                        MF_Recs.DateFees = dIssueDate;
                        MF_Recs.Status = 1;
                        MF_Recs.EditRecord();
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

                sTemp = sPDF_FullPath + "\\ManF_" + sNum + ".docx";
                if (File.Exists(sTemp)) File.Delete(sTemp);

                sTemp = sPDF_FullPath + "\\ManF_" + sNum + ".pdf";
                if (File.Exists(sTemp)) File.Delete(sTemp);

                sTemp = sPDF_FullPath + "\\ManF_" + sNum + "_sig.pdf";
                if (File.Exists(sTemp)) File.Delete(sTemp);
            }

            bCheckList = true;
        }
        private void PrintingInvoices_HFS()
        {
            int iLine;
            string sTemp, sPDF_FullPath, sInvoiceCode, sAitiologia, sApo, sEos, sLastFileName,
                   sInvTitleGr, sInvTitleEn, sCountry, sProfileGr, sProfileEn, sProfile, sNewFile, sNum, sInvType, sEafdss;
            var WordApp = new Microsoft.Office.Interop.Word.Application();
            var curDoc = new Microsoft.Office.Interop.Word.Document();

            bCheckList = false;
            iInvoiceType = 0;
            iClientType = 0;
            iNum = 0;
            iLine = 0;
            sNum = "";
            sSeira = "";
            sAitiologia = "";
            sApo = "";
            sEos = "";
            sPDF_FullPath = "";
            sInvTitleGr = "";
            sInvTitleEn = "";
            sInvoiceCode = "";
            sCountry = "";
            sInvType = "";
            sEafdss = "";
            sAitiologia = "";
            sProfileGr = "";
            sProfileEn = "";
            sProfile = "";
            dIssueDate = Convert.ToDateTime("30/09/2020");

            try
            {
                for (iLine = 2; iLine <= (fgList.Rows.Count - 1); iLine++)
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
                                sSeira = fgList[iLine, "Invoice_External"].ToString();
                                sInvTitleGr = sInvTitleAkyrotikoGr;
                                sInvTitleEn = sInvTitleAkyrotikoEn;
                                sInvType = sInvoiceTypeAkyrotiko;
                                break;
                        }

                        sApo = Convert.ToDateTime(fgList[iLine, "DateFrom"]).ToString("dd/MM/yyyy");
                        sEos = Convert.ToDateTime(fgList[iLine, "DateTo"]).ToString("dd/MM/yyyy");

                        clsInvoiceTitles InvoiceTitles = new clsInvoiceTitles();
                        InvoiceTitles.Tipos = iInvoiceType;
                        InvoiceTitles.Seira = sSeira;
                        iNum = Convert.ToInt32(InvoiceTitles.GetInvoice_LastNumber()) + 1;

                        switch (Convert.ToInt32(fgList[iLine, "Service_ID"]))
                        {
                            case 2:                                                                 // 2 - Advisory
                                sAitiologia = "Αμοιβή Επενδυτικών Συμβουλών";
                                break;
                            case 3:                                                                 // 3 - Discretionary
                                sAitiologia = "Αμοιβή Διαχείρισης";
                                break;
                            default:
                                sAitiologia = "Αμοιβή Επενδυτικών Συμβουλών";
                                break;
                        }

                        // --- Country : Greece or Not ------------
                        if (fgList[iLine, "Country"].ToString() == "" || fgList[iLine, "Country"].ToString() == "Ελλάδα" || fgList[iLine, "Country"].ToString() == "Greece") sCountry = fgList[iLine, "Country"].ToString();
                        else sCountry = fgList[iLine, "CountryEnglish"].ToString();


                        if (Convert.ToInt32(fgList[iLine, "MiFID_2"]) == 1)
                        {
                            sProfileGr = "Επενδυτικό Προφίλ";
                            sProfileEn = "Investment Profile";
                            sProfile = fgList[iLine, "InvestmentProfile"] + "";
                        }

                        sNum = fgList[iLine, "Invoice_Num"] + "";
                        i = sNum.IndexOf("/");
                        sNum = sNum.Substring(i+1);
                        iNum = Convert.ToInt32(sNum);
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

                        WordApp.Visible = false;

                        // --- check Temp folder  -------------
                        sPDF_FullPath = Application.StartupPath + "\\Temp";
                        if (!Directory.Exists(sPDF_FullPath)) Directory.CreateDirectory(sPDF_FullPath);

                        sTemp = sPDF_FullPath + "\\ManF_" + sNum + ".docx";
                        if (File.Exists(sTemp)) File.Delete(sTemp);

                        File.Copy(Application.StartupPath + "\\Templates\\InvoiceMFTemplate_HFS.docx", sTemp);
                        curDoc = WordApp.Documents.Open(sTemp);

                        sEafdss = "<%SL ;;" + fgList[iLine, "AFM"] + ";;;;;;" + sInvType + ";;" + sNum + ";0;0;" + Math.Abs(Convert.ToDecimal(fgList[iLine, "LastAmount"])) +
                                  ";0;0;0;0;" + Math.Abs(Convert.ToSingle(fgList[iLine, "VAT_Amount"])) + ";0;" + Math.Abs(Convert.ToDecimal(fgList[iLine, "FinishAmount"])) + ";" + "EUR" + ";>";

                        curDoc.Content.Find.Execute(FindText: "{title_gr}", ReplaceWith: sInvTitleGr, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{title_en}", ReplaceWith: sInvTitleEn, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{code}", ReplaceWith: fgList[iLine, "Code"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{portfolio}", ReplaceWith: fgList[iLine, "Portfolio"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{contract_title}", ReplaceWith: fgList[iLine, "ContractTitle"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{invest_services}", ReplaceWith: fgList[iLine, "ServiceTitle"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{profile_gr}", ReplaceWith: sProfileGr, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{profile_en}", ReplaceWith: sProfileEn, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{invest_policy}", ReplaceWith: fgList[iLine, "InvestPolicy"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{surname}", ReplaceWith: fgList[iLine, "User1Name"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{firstname}", ReplaceWith: "", Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{address}", ReplaceWith: fgList[iLine, "Address"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{city}", ReplaceWith: fgList[iLine, "City"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{zip}", ReplaceWith: fgList[iLine, "Zip"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{country}", ReplaceWith: sCountry, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{AFM}", ReplaceWith: fgList[iLine, "AFM"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{DOY}", ReplaceWith: fgList[iLine, "DOY"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{invoice_num}", ReplaceWith: fgList[iLine, "Invoice_Num"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{issue_date}", ReplaceWith: dIssueDate.ToString("dd/MM/yyyy"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{mfaitiologia_gr}", ReplaceWith: sAitiologia, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{apo}", ReplaceWith: sApo, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{eos}", ReplaceWith: sEos, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{amoivi}", ReplaceWith: Math.Abs(Convert.ToDecimal(fgList[iLine, "AmoiviAfter"])).ToString("0.00")+"%", Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{amount}", ReplaceWith: Math.Abs(Convert.ToDecimal(fgList[iLine, "LastAmount"])).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{fpa}", ReplaceWith: Math.Abs(Convert.ToSingle(fgList[iLine, "VAT_Percent"])).ToString("0.00") + "%", Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{vat_amount}", ReplaceWith: Math.Abs(Convert.ToSingle(fgList[iLine, "VAT_Amount"])).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{axia}", ReplaceWith: Math.Abs(Convert.ToDecimal(fgList[iLine, "FinishAmount"])).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{aum}", ReplaceWith: Math.Abs(Convert.ToDecimal(fgList[iLine, "AUM"])).ToString("###,####,###.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{eafdss}", ReplaceWith: sEafdss, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);

                        sTemp = sPDF_FullPath + "\\ManF_" + sNum + ".pdf";
                        curDoc.SaveAs2(sTemp, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF);
                        WordApp.Documents.Close();

                        Global.PrintPDF(sTemp);
                        sNewFile = sPDF_FullPath + "\\InvoiceMF_" + (sSeira + " " + iNum).Trim() + ".pdf";

                        for (i = 0; i <= 20; i++)
                            if (!File.Exists(sPDF_FullPath + "\\Signature_Processor__sig.pdf")) System.Threading.Thread.Sleep(3000);
                            else break;

                        if (File.Exists(sPDF_FullPath + "\\Signature_Processor__sig.pdf")) System.IO.File.Move(sPDF_FullPath + "\\Signature_Processor__sig.pdf", sNewFile);
                        else System.IO.File.Move(sPDF_FullPath + "\\ManF_" + sNum + ".pdf", sNewFile);

                        sLastFileName = Global.DMS_UploadFile(sNewFile, "\\Customers\\" + fgList[iLine, "ContractTitle"] + "\\Invoices", Path.GetFileName(sNewFile));

                        iID = SaveRecord(iLine, iInvoiceType, sSeira, iNum, Path.GetFileName(sLastFileName), Convert.ToInt32(fgList[iLine, "Record_ID"]), Convert.ToInt32(fgList[iLine, "Contract_ID"]));

                        fgList[iLine, 0] = false;
                        fgList[iLine, 1] = 1;
                        //fgList[iLine, "Invoice_Num"] = sInvoiceCode + " " + (sSeira + " " + iNum).Trim();
                        fgList[iLine, "FileName"] = Path.GetFileName(sLastFileName);
                        fgList.Refresh();

                        clsManagmentFees_Recs MF_Recs = new clsManagmentFees_Recs();
                        MF_Recs.Record_ID = Convert.ToInt32(fgList[iLine, "ID"]);
                        MF_Recs.GetRecord();
                        MF_Recs.Invoice_ID = iID;
                        MF_Recs.Invoice_Num = fgList[iLine, "Invoice_Num"].ToString();
                        MF_Recs.Invoice_File = Path.GetFileName(sLastFileName);
                        MF_Recs.DateFees = dIssueDate;
                        MF_Recs.Status = 1;
                        MF_Recs.EditRecord();
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

                sTemp = sPDF_FullPath + "\\ManF_" + sNum + ".docx";
                if (File.Exists(sTemp)) File.Delete(sTemp);

                sTemp = sPDF_FullPath + "\\ManF_" + sNum + ".pdf";
                if (File.Exists(sTemp)) File.Delete(sTemp);

                sTemp = sPDF_FullPath + "\\ManF_" + sNum + "_sig.pdf";
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
            InvoiceTitles.SourceType = 3;                                                   // 1 - RTO, 2 - FX, 3 - MF, 4 - AF, 5 - PF, 6 - CustodyF
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
        private void tsbSave_Click(object sender, EventArgs e)
        {
            clsManagmentFees_Recs MF_Recs = new clsManagmentFees_Recs();

            if (iAktion == 0)
            {
                MF_Recs.FT_ID = iFT_ID;
                MF_Recs.Client_ID = iClient_ID;
            }
            else
            {
                MF_Recs.Record_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                MF_Recs.GetRecord();
            }
                        
            MF_Recs.DateFrom = dFrom.Value;
            MF_Recs.DateTo = dTo.Value;
            MF_Recs.Code = lblCode.Text;
            MF_Recs.Portfolio = lblPortfolio.Text;
            MF_Recs.Currency = lblCurrency.Text;
            MF_Recs.Contract_ID = iContract_ID;
            MF_Recs.Contract_Details_ID = iContract_Details_ID;
            MF_Recs.Contract_Packages_ID = iContract_Packages_ID;
            MF_Recs.Days = Convert.ToInt32(lblDays.Text);
            MF_Recs.AUM = Convert.ToDecimal(txtAUM.Text);
            MF_Recs.AmoiviPro = Convert.ToSingle(lblAmoiviPro.Text);
            MF_Recs.AxiaPro = Convert.ToSingle(lblAxiaPro.Text);
            MF_Recs.AmoiviAfter = Convert.ToSingle(lblAmoiviAfter.Text);
            MF_Recs.AxiaAfter = Convert.ToSingle(txtAxiaAfter.Text);
            MF_Recs.Discount_Percent1 = Convert.ToSingle(txtDiscount_Percent1.Text);
            MF_Recs.Discount_Amount1 = Convert.ToSingle(txtDiscount_Amount1.Text);
            MF_Recs.Discount_Percent2 = Convert.ToSingle(txtDiscount_Percent2.Text);
            MF_Recs.Discount_Amount2 = Convert.ToSingle(txtDiscount_Amount2.Text);
            MF_Recs.Discount_Percent = Convert.ToSingle(lblDiscount_Percent.Text);
            MF_Recs.Discount_Amount = Convert.ToSingle(lblDiscount_Amount.Text);
            MF_Recs.MinAmoivi = Convert.ToSingle(lblMinAmoivi.Text);
            MF_Recs.MinAmoivi_Percent = Convert.ToSingle(txtMinAmoivi_Percent.Text);
            MF_Recs.FinishMinAmoivi = Convert.ToDecimal(txtFinishMinAmoivi.Text);
            MF_Recs.LastAmount = Convert.ToDecimal(txtLastAmount.Text);
            MF_Recs.VAT_Percent = Convert.ToSingle(txtVAT_Percent.Text);
            MF_Recs.VAT_Amount = Convert.ToSingle(txtVAT_Amount.Text);
            MF_Recs.FinishAmount = Convert.ToDecimal(txtFinishAmount.Text);
            MF_Recs.LastAmount_Percent = Convert.ToSingle(lblLastAmount_Percent.Text);
            MF_Recs.Invoice_Type = Convert.ToInt32(hdnInvoice_Type.Text);
            if (MF_Recs.Invoice_Type == 5) MF_Recs.Invoice_External = hdnSeira.Text;            // 5 - only for AKYROTIKO
            MF_Recs.Service_ID = iService_ID;
            MF_Recs.Notes = txtNotes.Text;
            MF_Recs.User_ID = Global.User_ID;
            MF_Recs.DateEdit = DateTime.Now;

            if (iAktion == 0) MF_Recs.InsertRecord();
            else              MF_Recs.EditRecord();

            DefineList();
            ShowList();

            panEdit.Visible = false;
        }
        private void picClose_Edit_Click(object sender, EventArgs e)
        {
            panEdit.Visible = false;
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
            EXL.Cells[1, 3].Value = "Τιμολόγηση Managment Fees";
            var loopTo = fgList.Rows.Count - 1;
            for (this.i = 0; this.i <= loopTo; this.i++)
            {
                for (this.j = 2; this.j <= 42; this.j++)                  
                    EXL.Cells[i + 2, j-1].Value = fgList[i, j];    
            }

            EXL.ScreenUpdating = true;
            EXL.Visible = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
        }
        private void tsbSettings_Click(object sender, EventArgs e)
        {
            frmOptions locOptions = new frmOptions();
            locOptions.StartPosition = FormStartPosition.CenterScreen;
            locOptions.RightsLevel = 2;
            locOptions.VisualFlags = "00100000";
            locOptions.Show();

            DefineOptions();
        }  
        private void panEdit_MouseDown(object sender, MouseEventArgs e)
        {
            this.position = e.Location;
            this.pMove = true;
        }
        private void panEdit_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.pMove == true)
                {
                    this.panEdit.Location = new Point(this.panEdit.Location.X + e.X - this.position.X, this.panEdit.Location.Y + e.Y - this.position.Y);
                }
            }
        }
        private void panEdit_MouseUp(object sender, MouseEventArgs e)
        {
            this.pMove = false;
        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
