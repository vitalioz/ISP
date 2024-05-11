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

public struct CFRec
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
    public partial class frmAcc_InvoicesCF : Form
    {
        DataView dtView;
        int i, j, iID, iCT_ID, iCF_Quart, iClient_ID, iClientType, iAktion, iRightsLevel, iNum, iInvoiceType, iInvoiceFisiko, iInvoiceNomiko,
            iInvoicePistotikoFisiko, iInvoicePistotikoNomiko, iInvoiceAkyrotiko, iCopies, iMode_FilePath,
            iService_ID, iContract_ID, iContract_Details_ID, iContract_Packages_ID, iSourceRows, iFoundRows;
        string sSeira, sInvoicePrinter, sCodeAkyrotiko = "", sInvTitleFisikoGr = "", sInvTitleFisikoEn = "", sInvoiceCodeFisiko = "",
               sInvTitleNomikoGr = "", sInvTitleNomikoEn = "", sInvoiceCodeNomiko = "", sInvoiceTypeFisiko = "", sInvoiceTypeNomiko = "",
               sSeiraPistotikoFisiko = "", sSeiraPistotikoNomiko = "", sSeiraAkyrotiko = "", sInvoiceCFTemplate = "", 
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
        CFRec stCFRec;
        bool bCheckList, bRecalcPrices;
        Point position;
        bool pMove;

        clsCustodyFees_Titles CustodyFees_Titles = new clsCustodyFees_Titles();
        clsCustodyFees_Recs CustodyFees_Recs = new clsCustodyFees_Recs();
        public frmAcc_InvoicesCF()
        {
            InitializeComponent();
        }
        private void frmAcc_InvoicesCF_Load(object sender, EventArgs e)
        {
            ucContracts.TextOfLabelChanged += new EventHandler(ucContracts_TextOfLabelChanged);
            ucContracts.ButtonClick += new EventHandler(ucContracts_ButtonClick);

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

            for (i = 2010; i <= DateTime.Now.Year; i++) cmbYear.Items.Add(i);

            i = (DateTime.Now.Month + 2) / 3;
            if (i == 1) { i = 4; cmbYear.SelectedIndex = cmbYear.Items.Count - 2; }
            else { i = i - 1; cmbYear.SelectedIndex = cmbYear.Items.Count - 1; }

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
            rng.Data = "Έξοδα Λήψης & Διαβίβασης";
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
            rng.Data = "Minimum Εξοδο λήψης & Διαβίβασης";
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
            rng.Data = "intro";

            fgList.Cols[35].AllowMerging = true;
            rng = fgList.GetCellRange(0, 35, 1, 35);
            rng.Data = "diax";

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
                iCF_Quart = 1;
                dStart = Convert.ToDateTime("01-01-" + cmbYear.Text);
                dFinish = Convert.ToDateTime("31-03-" + cmbYear.Text);
            }
            else
            {
                if (rb2.Checked)
                {
                    iCF_Quart = 2;
                    dStart = Convert.ToDateTime("01-04-" + cmbYear.Text);
                    dFinish = Convert.ToDateTime("30-06-" + cmbYear.Text);
                }
                else
                {
                    if (rb3.Checked)
                    {
                        iCF_Quart = 3;
                        dStart = Convert.ToDateTime("01-07-" + cmbYear.Text);
                        dFinish = Convert.ToDateTime("30-09-" + cmbYear.Text);
                    }
                    else
                    {
                        if (rb4.Checked)
                        {
                            iCF_Quart = 4;
                            dStart = Convert.ToDateTime("01-10-" + cmbYear.Text);
                            dFinish = Convert.ToDateTime("31-12-" + cmbYear.Text);
                        }
                    }
                }
            }

            CustodyFees_Titles = new clsCustodyFees_Titles();
            CustodyFees_Titles.SC_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
            CustodyFees_Titles.CF_Year = Convert.ToInt32(cmbYear.Text);
            CustodyFees_Titles.CF_Quart = Convert.ToInt32(iCF_Quart);
            CustodyFees_Titles.GetRecord_Title();
            iCT_ID = CustodyFees_Titles.Record_ID;
            if (iCT_ID == 0)
            {
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Νέο τρίμηνο.\n Είστε σίγουρος για αυτό;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    toolLeft.Visible = true;
                    cmbFilter.Visible = true;

                    clsCustodyFees_Titles CustodyFees_Titles = new clsCustodyFees_Titles();
                    CustodyFees_Titles.SC_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                    CustodyFees_Titles.CF_Quart = iCF_Quart;
                    CustodyFees_Titles.CF_Year = Convert.ToInt32(cmbYear.Text);
                    CustodyFees_Titles.DateIns = DateTime.Now;
                    CustodyFees_Titles.Author_ID = Global.User_ID;
                    iCT_ID = CustodyFees_Titles.InsertRecord();
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

        private void picClose_FilePath_Click(object sender, EventArgs e)
        {

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
            txtFilePath.Text = sExportFilePath + @"\CustodyFees_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
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
                                for (int i = 2; i <= rowCount; i++)
                                {
                                    range = (excelWorksheet.Cells[i, 3] as Excel.Range);
                                    sCode = range.Value.ToString();

                                    range = (excelWorksheet.Cells[i, 4] as Excel.Range);
                                    sPortfolio = range.Value.ToString();

                                    range = (excelWorksheet.Cells[i, 5] as Excel.Range);
                                    dFrom = Convert.ToDateTime(range.Value.ToString());

                                    range = (excelWorksheet.Cells[i, 6] as Excel.Range);
                                    dTo = Convert.ToDateTime(range.Value.ToString());

                                    range = (excelWorksheet.Cells[i, 7] as Excel.Range);
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
            else sFilter = "Code= '" + sCode + "' and Portfolio = '" + sPortfolio + "'";

            foundRows = CustodyFees_Recs.List.Select(sFilter);
            if (foundRows.Length > 0)
            {
                stCFRec.dFrom = Convert.ToDateTime(foundRows[0]["DateFrom"]);
                stCFRec.dTo = Convert.ToDateTime(foundRows[0]["DateTo"]);
                stCFRec.Days = Convert.ToInt32(foundRows[0]["Days"]);
                stCFRec.Contract_ID = Convert.ToInt32(foundRows[0]["Contract_ID"]);
                stCFRec.Contract_Packages_ID = Convert.ToInt32(foundRows[0]["Contracts_Packages_ID"]);
                stCFRec.Service_ID = Convert.ToInt32(foundRows[0]["Service_ID"]);
                iService_ID = Convert.ToInt32(foundRows[0]["Service_ID"]);
                stCFRec.AUM = decAUM;
                stCFRec.AmoiviPro = Convert.ToSingle(foundRows[0]["AmoiviPro"]);
                stCFRec.AxiaPro = Convert.ToSingle(foundRows[0]["AxiaPro"]);
                stCFRec.Climakas = foundRows[0]["Climakas"] + "";
                stCFRec.Discount_DateTo = foundRows[0]["Discount_DateTo"] + "";
                stCFRec.Discount_Percent1 = Convert.ToSingle(foundRows[0]["Discount_Percent1"]);
                stCFRec.Discount_Amount1 = Convert.ToSingle(foundRows[0]["Discount_Amount1"]);
                stCFRec.Discount_Percent2 = Convert.ToSingle(foundRows[0]["Discount_Percent2"]);
                stCFRec.Discount_Amount2 = Convert.ToSingle(foundRows[0]["Discount_Amount2"]);
                stCFRec.Discount_Percent = Convert.ToSingle(foundRows[0]["Discount_Percent"]);
                stCFRec.Discount_Amount = Convert.ToSingle(foundRows[0]["Discount_Amount"]);
                stCFRec.AmoiviAfter = Convert.ToSingle(foundRows[0]["AmoiviAfter"]);
                stCFRec.AxiaAfter = Convert.ToSingle(foundRows[0]["AxiaAfter"]);
                stCFRec.MinAmoivi = Convert.ToSingle(foundRows[0]["MinAmoivi"]);
                stCFRec.MinAmoivi_Percent = Convert.ToSingle(foundRows[0]["MinAmoivi_Percent"]);
                stCFRec.FinishMinAmoivi = Convert.ToDecimal(foundRows[0]["FinishMinAmoivi"]);
                stCFRec.LastAmount = Convert.ToDecimal(foundRows[0]["LastAmount"]);
                stCFRec.LastAmount_Percent = Convert.ToSingle(foundRows[0]["LastAmount_Percent"]);
                stCFRec.VAT_Percent = Convert.ToSingle(foundRows[0]["VAT_Percent"]);
                stCFRec.VAT_Amount = Convert.ToSingle(foundRows[0]["VAT_Amount"]);
                stCFRec.FinishAmount = Convert.ToDecimal(foundRows[0]["FinishAmount"]);
                stCFRec.Invoice_External = sInvoiceExternal;

                CalcFees_Step1();
                CalcFees_Step2();

                foundRows[0]["AUM"] = decAUM;
                foundRows[0]["AmoiviPro"] = stCFRec.AmoiviPro.ToString();
                foundRows[0]["AxiaPro"] = stCFRec.AxiaPro.ToString();
                foundRows[0]["Discount_Percent1"] = stCFRec.Discount_Percent1.ToString();
                foundRows[0]["Discount_Amount1"] = stCFRec.Discount_Amount1.ToString();
                foundRows[0]["Discount_Percent"] = stCFRec.Discount_Percent.ToString();
                foundRows[0]["Discount_Amount"] = stCFRec.Discount_Amount.ToString();
                foundRows[0]["AmoiviAfter"] = stCFRec.AmoiviAfter.ToString();
                foundRows[0]["AxiaAfter"] = stCFRec.AxiaAfter.ToString();
                foundRows[0]["MinAmoivi"] = stCFRec.MinAmoivi.ToString();
                foundRows[0]["MinAmoivi_Percent"] = stCFRec.MinAmoivi_Percent.ToString();
                foundRows[0]["FinishMinAmoivi"] = stCFRec.FinishMinAmoivi.ToString();
                foundRows[0]["LastAmount"] = stCFRec.LastAmount.ToString();
                foundRows[0]["LastAmount_Percent"] = stCFRec.LastAmount_Percent.ToString();
                foundRows[0]["VAT_Percent"] = stCFRec.VAT_Percent.ToString();
                foundRows[0]["VAT_Amount"] = stCFRec.VAT_Amount.ToString();
                foundRows[0]["FinishAmount"] = stCFRec.FinishAmount.ToString();
                foundRows[0]["Invoice_External"] = stCFRec.Invoice_External;

                //--- save record with AUM and calculating data ---------------------------------
                clsCustodyFees_Recs CF_Recs = new clsCustodyFees_Recs();
                CF_Recs.Record_ID = Convert.ToInt32(foundRows[0]["ID"]);
                CF_Recs.GetRecord();

                CF_Recs.AUM = decAUM;
                CF_Recs.AmoiviPro = stCFRec.AmoiviPro;
                CF_Recs.AxiaPro = stCFRec.AxiaPro;
                CF_Recs.Discount_Percent1 = stCFRec.Discount_Percent1;
                CF_Recs.Discount_Amount1 = stCFRec.Discount_Amount1;
                CF_Recs.Discount_Percent2 = stCFRec.Discount_Percent2;
                CF_Recs.Discount_Amount2 = stCFRec.Discount_Amount2;
                CF_Recs.Discount_Percent = stCFRec.Discount_Percent;
                CF_Recs.Discount_Amount = stCFRec.Discount_Amount;
                CF_Recs.AmoiviAfter = stCFRec.AmoiviAfter;
                CF_Recs.AxiaAfter = stCFRec.AxiaAfter;
                CF_Recs.MinAmoivi = stCFRec.MinAmoivi;
                CF_Recs.MinAmoivi_Percent = stCFRec.MinAmoivi_Percent;
                CF_Recs.FinishMinAmoivi = stCFRec.FinishMinAmoivi;
                CF_Recs.LastAmount = stCFRec.LastAmount;
                CF_Recs.VAT_Percent = stCFRec.VAT_Percent;
                CF_Recs.VAT_Amount = stCFRec.VAT_Amount;
                CF_Recs.FinishAmount = stCFRec.FinishAmount;
                CF_Recs.LastAmount_Percent = stCFRec.LastAmount_Percent;
                CF_Recs.Invoice_External = stCFRec.Invoice_External;
                CF_Recs.DateEdit = DateTime.Now;
                CF_Recs.EditRecord();

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
            if (fgList.Row > 1)
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
            ucContracts.Focus();
        }
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            iAktion = 1;                                                    // 0 - Add, 1 - Edit
            bRecalcPrices = false;
            if (fgList.Row > 1) ShowRecord(1);
            bRecalcPrices = true;
            ucContracts.Focus();
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
            fgList[fgList.Row, 0] = true;
            PrintInvoice();
        }
        private void DefineList()
        {
            CustodyFees_Recs.CT_ID = iCT_ID;
            CustodyFees_Recs.GetList();
        }
        private void ShowList()
        {
            if (bCheckList)
            {
                fgList.Redraw = false;
                fgList.Rows.Count = 2;
                int i = 0;

                foreach (DataRow dtRow in CustodyFees_Recs.List.Rows)
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

                stCFRec.dFrom = dStart;
                stCFRec.dTo = dFinish;
                stCFRec.Days = 90;

                ucContracts.StartInit(700, 400, 570, 20, 1);
                ucContracts.Visible = true;
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

                ucContracts.Visible = false;
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
                                else sSeira = sSeira + sInvoiceTypePistotikoNomiko;
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

            stCFRec.dFrom = dFrom.Value;
            stCFRec.dTo = dTo.Value;
            stCFRec.Days = Convert.ToInt32(lblDays.Text);
            stCFRec.Contract_ID = iContract_ID; ;
            stCFRec.Contract_Packages_ID = iContract_Packages_ID;
            stCFRec.Service_ID = iService_ID;
            stCFRec.AUM = Convert.ToDecimal(txtAUM.Text);
            stCFRec.AmoiviPro = Convert.ToSingle(lblAmoiviPro.Text);
            stCFRec.AxiaPro = Convert.ToSingle(lblAxiaPro.Text);
            stCFRec.Discount_DateTo = lblDiscount_DateTo.Text;
            stCFRec.Discount_Percent1 = Convert.ToSingle(txtDiscount_Percent1.Text);
            stCFRec.Discount_Amount1 = Convert.ToSingle(txtDiscount_Amount1.Text);
            stCFRec.Discount_Percent2 = Convert.ToSingle(txtDiscount_Percent2.Text);
            stCFRec.Discount_Amount2 = Convert.ToSingle(txtDiscount_Amount2.Text);
            stCFRec.Discount_Percent = Convert.ToSingle(lblDiscount_Percent.Text);
            stCFRec.Discount_Amount = Convert.ToSingle(lblDiscount_Amount.Text);
            stCFRec.AmoiviAfter = Convert.ToSingle(lblAmoiviAfter.Text);
            stCFRec.AxiaAfter = Convert.ToSingle(txtAxiaAfter.Text);
            stCFRec.Climakas = lblClimakas.Text;
            stCFRec.MinAmoivi = Convert.ToSingle(lblMinAmoivi.Text);
            stCFRec.MinAmoivi_Percent = Convert.ToSingle(txtMinAmoivi_Percent.Text);
            stCFRec.FinishMinAmoivi = Convert.ToDecimal(txtFinishMinAmoivi.Text);
            stCFRec.LastAmount = Convert.ToDecimal(txtLastAmount.Text);
            stCFRec.LastAmount_Percent = Convert.ToSingle(lblLastAmount_Percent.Text);
            stCFRec.VAT_Percent = Convert.ToSingle(txtVAT_Percent.Text);
            stCFRec.VAT_Amount = Convert.ToSingle(txtVAT_Amount.Text);
            stCFRec.FinishAmount = Convert.ToDecimal(txtFinishAmount.Text);

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
        protected void ucContracts_TextOfLabelChanged(object sender, EventArgs e)
        {
            //handle the event 
            stContractData = ucContracts.SelectedContractData;
            lblCode.Text = stContractData.Code;
            lblPortfolio.Text = stContractData.Portfolio;
            lblPackage.Text = stContractData.Package_Title;
            lblCurrency.Text = stContractData.Currency;
            iService_ID = stContractData.Service_ID;
            iContract_ID = stContractData.Contract_ID;
            iContract_Details_ID = stContractData.Contracts_Details_ID;
            iContract_Packages_ID = stContractData.Contracts_Packages_ID;
            txtVAT_Percent.Text = stContractData.VAT_Percent.ToString();

            stCFRec.Service_ID = stContractData.Service_ID;
            stCFRec.Contract_ID = stContractData.Contract_ID;
            //stCFRec.Contract_Details_ID = stContractData.Contracts_Details_ID;
            stCFRec.Contract_Packages_ID = stContractData.Contracts_Packages_ID;
            stCFRec.VAT_Percent = stContractData.VAT_Percent;

            ShowFeesTable();

            dFrom.Focus();
        }
        protected void ucContracts_ButtonClick(object sender, EventArgs e)
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

                iCT_ID = 0;

                if (rbc1.Checked)
                {
                    iIndex = 1;
                    dStart = Convert.ToDateTime("01-01-" + cmbYear.Text);
                    dFinish = Convert.ToDateTime("31-03-" + cmbYear.Text);
                }
                if (rbc2.Checked)
                {
                    iIndex = 2;
                    dStart = Convert.ToDateTime("01-04-" + cmbYear.Text);
                    dFinish = Convert.ToDateTime("30-06-" + cmbYear.Text);
                }
                if (rbc3.Checked)
                {
                    iIndex = 3;
                    dStart = Convert.ToDateTime("01-07-" + cmbYear.Text);
                    dFinish = Convert.ToDateTime("30-09-" + cmbYear.Text);
                }
                if (rbc4.Checked)
                {
                    iIndex = 4;
                    dStart = Convert.ToDateTime("01-10-" + cmbYear.Text);
                    dFinish = Convert.ToDateTime("31-12-" + cmbYear.Text);
                }

                clsCustodyFees_Titles klsCustodyFees_Title = new clsCustodyFees_Titles();
                klsCustodyFees_Title.SC_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                klsCustodyFees_Title.CF_Year = Convert.ToInt32(cmbYear.Text);
                klsCustodyFees_Title.CF_Quart = iIndex;
                klsCustodyFees_Title.GetRecord_Title();
                iCT_ID = klsCustodyFees_Title.Record_ID;
                if (iCT_ID == 0)
                {
                    var ExApp = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook xlWorkbook = ExApp.Workbooks.Open(txtFilePath_Import.Text);
                    Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                    Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

                    klsCustodyFees_Title.SC_ID = Convert.ToInt32(cmbServiceProviders.SelectedValue);
                    klsCustodyFees_Title.CF_Quart = iIndex;
                    klsCustodyFees_Title.CF_Year = Convert.ToInt32(cmbYear.Text);
                    klsCustodyFees_Title.DateIns = DateTime.Now;
                    klsCustodyFees_Title.Author_ID = Global.User_ID;
                    iCT_ID = klsCustodyFees_Title.InsertRecord();

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

                        clsCustodyFees_Recs CF_Recs = new clsCustodyFees_Recs();
                        CF_Recs.CT_ID = iCT_ID;
                        CF_Recs.Client_ID = Convert.ToInt32(klsContract.Client_ID);
                        CF_Recs.DateFrom = dStart;
                        CF_Recs.DateTo = dFinish;
                        CF_Recs.Code = xlRange.Cells[i, 1].Value.ToString();
                        CF_Recs.Portfolio = xlRange.Cells[i, 2].Value.ToString();
                        CF_Recs.Currency = klsContract.Currency + "";
                        CF_Recs.Contract_ID = Convert.ToInt32(klsContract.Record_ID);
                        CF_Recs.Contract_Details_ID = Convert.ToInt32(klsContract.Contract_Details_ID);
                        CF_Recs.Contract_Packages_ID = Convert.ToInt32(klsContract.Contract_Packages_ID);
                        CF_Recs.AUM = Convert.ToDecimal(xlRange.Cells[i, 23].Value);
                        CF_Recs.Days = 90;
                        CF_Recs.AmoiviPro = Convert.ToSingle(xlRange.Cells[i, 17].Value * 100);
                        CF_Recs.AxiaPro = 0;
                        CF_Recs.Climakas = "";
                        CF_Recs.Discount_DateFrom = "";
                        CF_Recs.Discount_DateTo = "";
                        CF_Recs.Discount_Percent1 = 0;
                        CF_Recs.Discount_Amount1 = 0;
                        CF_Recs.Discount_Percent2 = 0;
                        CF_Recs.Discount_Amount2 = 0;
                        CF_Recs.Discount_Percent = 0;
                        CF_Recs.Discount_Amount = 0;
                        CF_Recs.AmoiviAfter = Convert.ToSingle(xlRange.Cells[i, 17].Value * 100);
                        CF_Recs.AxiaAfter = Convert.ToSingle(xlRange.Cells[i, 19].Value);
                        CF_Recs.MinAmoivi = 0;
                        CF_Recs.MinAmoivi_Percent = 0;
                        CF_Recs.FinishMinAmoivi = 0;
                        CF_Recs.LastAmount = Convert.ToDecimal(xlRange.Cells[i, 19].Value);
                        CF_Recs.LastAmount_Percent = 0;
                        CF_Recs.VAT_Amount = Convert.ToSingle(xlRange.Cells[i, 20].Value);
                        CF_Recs.VAT_Percent = Convert.ToSingle(xlRange.Cells[i, 18].Value * 100);
                        CF_Recs.FinishAmount = Convert.ToDecimal(xlRange.Cells[i, 21].Value);
                        CF_Recs.Service_ID = klsContract.Service_ID;
                        CF_Recs.Invoice_ID = 0;
                        CF_Recs.Invoice_Num = xlRange.Cells[i, 14].Value.ToString();
                        CF_Recs.Invoice_File = "";
                        CF_Recs.DateFees = Convert.ToDateTime("1900/01/01");
                        CF_Recs.Invoice_Type = 0;
                        CF_Recs.Notes = xlRange.Cells[i, 16].Value.ToString();
                        CF_Recs.Invoice_External = "";
                        CF_Recs.Status = 1;                                                      // 1 - Active, 2 - Cancelled
                        CF_Recs.User_ID = Global.User_ID;
                        CF_Recs.DateEdit = DateTime.Now;
                        CF_Recs.InsertRecord();
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
            stCFRec.AUM = Convert.ToDecimal(txtAUM.Text);
            CalcFees_Step1();
            CalcFees_Step2();
            ShowNewValues();
        }
        private void txtDiscount_Percent1_LostFocus(object sender, EventArgs e)
        {
            stCFRec.Discount_Percent1 = Convert.ToSingle(txtDiscount_Percent1.Text);
            CalcFees_Step2();
            ShowNewValues();
        }
        private void txtDiscount_Amount1_LostFocus(object sender, EventArgs e)
        {
            stCFRec.Discount_Amount1 = Convert.ToSingle(txtDiscount_Amount1.Text);
            CalcFees_Step2();
            ShowNewValues();
        }
        private void txtDiscount_Percent2_LostFocus(object sender, EventArgs e)
        {
            stCFRec.Discount_Percent2 = Convert.ToSingle(txtDiscount_Percent2.Text);
            CalcFees_Step2();
            ShowNewValues();
        }
        private void txtDiscount_Amount2_LostFocus(object sender, EventArgs e)
        {
            stCFRec.Discount_Amount2 = Convert.ToSingle(txtDiscount_Amount2.Text);
            CalcFees_Step2();
            ShowNewValues();
        }
        private void txtAxiaAfter_LostFocus(object sender, EventArgs e)
        {
            stCFRec.AxiaAfter = Convert.ToSingle(txtAxiaAfter.Text);
            CalcFees_Step2();
            ShowNewValues();
        }
        private void txtLastAmount_LostFocus(object sender, EventArgs e)
        {
            stCFRec.LastAmount = Convert.ToDecimal(txtLastAmount.Text);
            stCFRec.VAT_Amount = (float)Math.Round((float)stCFRec.LastAmount * stCFRec.VAT_Percent / 100, 2);
            stCFRec.FinishAmount = Math.Round(stCFRec.LastAmount + (decimal)stCFRec.VAT_Amount, 2);
            ShowNewValues();
        }
        private void txtVAT_Percent_LostFocus(object sender, EventArgs e)
        {
            stCFRec.VAT_Percent = Convert.ToSingle(txtVAT_Percent.Text);
            stCFRec.VAT_Amount = (float)Math.Round((float)stCFRec.LastAmount * stCFRec.VAT_Percent / 100, 2);
            stCFRec.FinishAmount = Math.Round(stCFRec.LastAmount + (decimal)stCFRec.VAT_Amount, 2);
            ShowNewValues();
        }
        private void txtVAT_Amount_LostFocus(object sender, EventArgs e)
        {
            stCFRec.VAT_Amount = Convert.ToSingle(txtVAT_Amount.Text);
            stCFRec.FinishAmount = Math.Round(stCFRec.LastAmount + (decimal)stCFRec.VAT_Amount, 2);
            ShowNewValues();
        }
        private void CalcFees_Step1()
        {
            i = Convert.ToInt32((stCFRec.dTo - stCFRec.dFrom).TotalDays) + 1;
            if (i > 90) i = 90;
            i = stCFRec.Days;
            lblDays.Text = i.ToString();

            //stCFRec.dFrom = dFrom.Value;
            //stCFRec.dTo = dTo.Value;
            //stCFRec.Days = i;

            switch (iService_ID)
            {
                case 2:
                    clsClientsAdvisoryFees ContractAdvisoryFees = new clsClientsAdvisoryFees();
                    ContractAdvisoryFees.AUM = stCFRec.AUM;
                    ContractAdvisoryFees.Contract_ID = stCFRec.Contract_ID;
                    ContractAdvisoryFees.Contract_Packages_ID = stCFRec.Contract_Packages_ID;
                    ContractAdvisoryFees.DateFrom = stCFRec.dFrom;
                    ContractAdvisoryFees.DateTo = stCFRec.dTo;
                    ContractAdvisoryFees.Days = stCFRec.Days;
                    ContractAdvisoryFees.GetList_FeesData();
                    stCFRec.AmoiviPro = (float)Math.Round(ContractAdvisoryFees.FeesPercent, 4);
                    stCFRec.AxiaPro = (float)Math.Round(ContractAdvisoryFees.StartAmount, 2);
                    stCFRec.Discount_Percent1 = (float)Math.Round(ContractAdvisoryFees.Discount_Percent, 4);
                    stCFRec.Discount_Amount1 = (float)Math.Round(ContractAdvisoryFees.Discount_Amount, 2);
                    break;
                case 3:
                    clsClientsDiscretFees ContractDiscretFees = new clsClientsDiscretFees();
                    ContractDiscretFees.AUM = stCFRec.AUM;
                    ContractDiscretFees.Contract_ID = stCFRec.Contract_ID;
                    ContractDiscretFees.Contract_Packages_ID = stCFRec.Contract_Packages_ID;
                    ContractDiscretFees.DateFrom = stCFRec.dFrom;
                    ContractDiscretFees.DateTo = stCFRec.dTo;
                    ContractDiscretFees.Days = stCFRec.Days;
                    ContractDiscretFees.GetList_FeesData();
                    stCFRec.AmoiviPro = (float)Math.Round(ContractDiscretFees.FeesPercent, 4);
                    stCFRec.AxiaPro = (float)Math.Round(ContractDiscretFees.StartAmount, 2);
                    stCFRec.Discount_Percent1 = (float)Math.Round(ContractDiscretFees.Discount_Percent, 4);
                    stCFRec.Discount_Amount1 = (float)Math.Round(ContractDiscretFees.Discount_Amount, 2);
                    break;
                case 5:
                    clsClientsDealAdvisoryFees ContractDealAdvisoryFees = new clsClientsDealAdvisoryFees();
                    ContractDealAdvisoryFees.AUM = stCFRec.AUM;
                    ContractDealAdvisoryFees.Contract_ID = stCFRec.Contract_ID;
                    ContractDealAdvisoryFees.Contract_Packages_ID = stCFRec.Contract_Packages_ID;
                    ContractDealAdvisoryFees.DateFrom = stCFRec.dFrom;
                    ContractDealAdvisoryFees.DateTo = stCFRec.dTo;
                    ContractDealAdvisoryFees.Days = stCFRec.Days;
                    ContractDealAdvisoryFees.GetList_FeesData();
                    stCFRec.AmoiviPro = (float)Math.Round(ContractDealAdvisoryFees.FeesPercent, 4);
                    stCFRec.AxiaPro = (float)Math.Round(ContractDealAdvisoryFees.StartAmount, 2);
                    stCFRec.Discount_Percent1 = (float)Math.Round(ContractDealAdvisoryFees.Discount_Percent, 4);
                    stCFRec.Discount_Amount1 = (float)Math.Round(ContractDealAdvisoryFees.Discount_Amount, 2);
                    break;
            }
        }
        private void CalcFees_Step2()
        {
            if (bRecalcPrices)
            {
                stCFRec.Discount_Amount1 = (float)Math.Round(stCFRec.Discount_Percent1 * stCFRec.AxiaPro / 100, 2);                                                // Axia ekptosis 1
                stCFRec.Discount_Amount2 = (float)Math.Round(stCFRec.Discount_Percent2 * stCFRec.AxiaPro / 100, 2);                                                // Axia ekptosis 2
                stCFRec.Discount_Percent = (float)Math.Round(stCFRec.Discount_Percent1 + stCFRec.Discount_Percent2, 4);                                            // % Ekptosis    Synoliki
                stCFRec.Discount_Amount = (float)Math.Round(stCFRec.Discount_Amount1 + stCFRec.Discount_Amount2, 2);                                               // Axia ekptosis Synoliki  
                stCFRec.AxiaAfter = (float)Math.Round(stCFRec.AxiaPro - stCFRec.Discount_Amount, 2);                                                               // Poso meta tin ekptosis

                stCFRec.AmoiviAfter = 0;
                if (stCFRec.AxiaPro != 0) stCFRec.AmoiviAfter = (float)Math.Round((stCFRec.AxiaAfter * 36000) / (float)(stCFRec.AUM * stCFRec.Days), 4);

                if (stCFRec.FinishMinAmoivi > (decimal)stCFRec.AxiaAfter) stCFRec.LastAmount = Math.Round(stCFRec.FinishMinAmoivi, 2);
                else stCFRec.LastAmount = (decimal)Math.Round(stCFRec.AxiaAfter, 2);

                stCFRec.LastAmount_Percent = 0;
                if (stCFRec.AUM != 0 && stCFRec.Days != 0) stCFRec.LastAmount_Percent = (float)Math.Round((float)(stCFRec.LastAmount * 36000) / (float)(stCFRec.AUM * stCFRec.Days), 4);

                stCFRec.VAT_Amount = (float)Math.Round((float)stCFRec.LastAmount * stCFRec.VAT_Percent / 100, 2);                                                  // FPA
                stCFRec.FinishAmount = Math.Round(stCFRec.LastAmount + (decimal)stCFRec.VAT_Amount, 2);                                                            // Teliko poso - Poso me FPA
            }
        }
        private void ShowNewValues()
        {
            lblAmoiviPro.Text = stCFRec.AmoiviPro.ToString();
            lblAxiaPro.Text = stCFRec.AxiaPro.ToString();
            txtDiscount_Percent1.Text = stCFRec.Discount_Percent1.ToString();
            txtDiscount_Amount1.Text = stCFRec.Discount_Amount1.ToString();
            txtDiscount_Percent2.Text = stCFRec.Discount_Percent2.ToString();
            txtDiscount_Amount2.Text = stCFRec.Discount_Amount2.ToString();
            lblDiscount_Percent.Text = stCFRec.Discount_Percent.ToString();
            lblDiscount_Amount.Text = stCFRec.Discount_Amount.ToString();
            lblAmoiviAfter.Text = stCFRec.AmoiviAfter.ToString();
            txtAxiaAfter.Text = stCFRec.AxiaAfter.ToString();
            lblMinAmoivi.Text = stCFRec.MinAmoivi.ToString();
            txtMinAmoivi_Percent.Text = stCFRec.MinAmoivi_Percent.ToString();
            txtFinishMinAmoivi.Text = stCFRec.FinishMinAmoivi.ToString();
            txtLastAmount.Text = stCFRec.LastAmount.ToString();
            lblLastAmount_Percent.Text = stCFRec.LastAmount_Percent.ToString();
            txtVAT_Percent.Text = stCFRec.VAT_Percent.ToString();
            txtVAT_Amount.Text = stCFRec.VAT_Amount.ToString();
            txtFinishAmount.Text = stCFRec.FinishAmount.ToString();
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

            sInvoiceCFTemplate = Options.InvoiceCFTemplate;
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
            string sTemp, sPDF_FullPath, sInvoiceCode, sAitiologia, sApo, sEos, sInvTitleGr, sInvTitleEn, sCountry, sProfileGr, sProfileEn, sProfile, 
                   sNewFile, sFileName, sNum, sInvType, sEafdss;
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
                        if (!Directory.Exists(sPDF_FullPath)) Directory.CreateDirectory(sPDF_FullPath);

                        sTemp = sPDF_FullPath + "\\CustF_" + sNum + ".docx";
                        if (File.Exists(sTemp)) File.Delete(sTemp);

                        File.Copy(Application.StartupPath + "\\Templates\\" + sInvoiceCFTemplate, sTemp);
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
                        curDoc.Content.Find.Execute(FindText: "{cfaitiologia_gr}", ReplaceWith: sAitiologia, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{apo}", ReplaceWith: sApo, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{eos}", ReplaceWith: sEos, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{amount}", ReplaceWith: Math.Abs(Convert.ToDecimal(fgList[iLine, "LastAmount"])).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{fpa}", ReplaceWith: Math.Abs(Convert.ToSingle(fgList[iLine, "VAT_Percent"])).ToString(), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{vat}", ReplaceWith: Math.Abs(Convert.ToSingle(fgList[iLine, "VAT_Amount"])).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{axia}", ReplaceWith: Math.Abs(Convert.ToDecimal(fgList[iLine, "FinishAmount"])).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{eafdss}", ReplaceWith: sEafdss, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);

                        sTemp = sPDF_FullPath + "\\CustF_" + sNum + ".pdf";
                        curDoc.SaveAs2(sTemp, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF);
                        WordApp.Documents.Close();

                        Global.PrintPDF(sTemp);
                        sNewFile = sPDF_FullPath + "\\InvoiceCF_" + (sSeira + " " + iNum).Trim() + ".pdf";

                        for (i = 0; i <= 20; i++)
                            if (!File.Exists(sPDF_FullPath + "\\Signature_Processor__sig.pdf")) System.Threading.Thread.Sleep(3000);
                            else break;

                        if (File.Exists(sPDF_FullPath + "\\Signature_Processor__sig.pdf")) System.IO.File.Move(sPDF_FullPath + "\\Signature_Processor__sig.pdf", sNewFile);
                        else System.IO.File.Move(sPDF_FullPath + "\\CustF_" + sNum + ".pdf", sNewFile);

                        iID = SaveRecord(iLine, iInvoiceType, sSeira, iNum, Path.GetFileName(sNewFile), Convert.ToInt32(fgList[iLine, "ID"]), Convert.ToInt32(fgList[iLine, "Contract_ID"]));

                        sFileName = Global.DMS_UploadFile(sNewFile, "\\Customers\\" + fgList[iLine, "ContractTitle"] + "\\Invoices", Path.GetFileName(sNewFile));

                        fgList[iLine, 0] = false;
                        fgList[iLine, 1] = 1;
                        fgList[iLine, "Invoice_Num"] = sInvoiceCode + " " + (sSeira + " " + iNum).Trim();
                        fgList[iLine, "FileName"] = Path.GetFileName(sFileName);
                        fgList.Refresh();

                        clsCustodyFees_Recs CF_Recs = new clsCustodyFees_Recs();
                        CF_Recs.Record_ID = Convert.ToInt32(fgList[iLine, "ID"]);
                        CF_Recs.GetRecord();
                        CF_Recs.Invoice_ID = iID;
                        CF_Recs.Invoice_Num = fgList[iLine, "Invoice_Num"].ToString();
                        CF_Recs.Invoice_File = Path.GetFileName(sFileName);
                        CF_Recs.DateFees = dIssueDate;
                        CF_Recs.Status = 1;
                        CF_Recs.EditRecord();
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

                sTemp = sPDF_FullPath + "\\CustF_" + sNum + ".docx";
                if (File.Exists(sTemp)) File.Delete(sTemp);

                sTemp = sPDF_FullPath + "\\CustF_" + sNum + ".pdf";
                if (File.Exists(sTemp)) File.Delete(sTemp);

                sTemp = sPDF_FullPath + "\\CustF_" + sNum + "_sig.pdf";
                if (File.Exists(sTemp)) File.Delete(sTemp);
            }

            bCheckList = true;
        }
        private void PrintingInvoices_HFS()
        {
            int iLine;
            string sTemp, sPDF_FullPath, sInvoiceCode, sAitiologia, sApo, sEos,
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
                        sNum = sNum.Substring(i + 1);
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

                        sTemp = sPDF_FullPath + "\\CustF_" + sNum + ".docx";
                        if (File.Exists(sTemp)) File.Delete(sTemp);

                        File.Copy(Application.StartupPath + "\\Templates\\InvoiceCFTemplate_HFS.docx", sTemp);
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
                        curDoc.Content.Find.Execute(FindText: "{cfaitiologia_gr}", ReplaceWith: sAitiologia, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{apo}", ReplaceWith: sApo, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{eos}", ReplaceWith: sEos, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{amoivi}", ReplaceWith: Math.Abs(Convert.ToDecimal(fgList[iLine, "AmoiviAfter"])).ToString("0.00") + "%", Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{amount}", ReplaceWith: Math.Abs(Convert.ToDecimal(fgList[iLine, "LastAmount"])).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{fpa}", ReplaceWith: Math.Abs(Convert.ToSingle(fgList[iLine, "VAT_Percent"])).ToString("0.00") + "%", Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{vat_amount}", ReplaceWith: Math.Abs(Convert.ToSingle(fgList[iLine, "VAT_Amount"])).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{axia}", ReplaceWith: Math.Abs(Convert.ToDecimal(fgList[iLine, "FinishAmount"])).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{aum}", ReplaceWith: Math.Abs(Convert.ToDecimal(fgList[iLine, "AUM"])).ToString("###,####,###.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{eafdss}", ReplaceWith: sEafdss, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);

                        sTemp = sPDF_FullPath + "\\CustF_" + sNum + ".pdf";
                        curDoc.SaveAs2(sTemp, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF);
                        WordApp.Documents.Close();

                        Global.PrintPDF(sTemp);
                        sNewFile = sPDF_FullPath + "\\InvoiceCF_" + (sSeira + " " + iNum).Trim() + ".pdf";

                        for (i = 0; i <= 20; i++)
                            if (!File.Exists(sPDF_FullPath + "\\Signature_Processor__sig.pdf")) System.Threading.Thread.Sleep(3000);
                            else break;

                        if (File.Exists(sPDF_FullPath + "\\Signature_Processor__sig.pdf")) System.IO.File.Move(sPDF_FullPath + "\\Signature_Processor__sig.pdf", sNewFile);
                        else System.IO.File.Move(sPDF_FullPath + "\\CustF_" + sNum + ".pdf", sNewFile);

                        iID = SaveRecord(iLine, iInvoiceType, sSeira, iNum, Path.GetFileName(sNewFile), Convert.ToInt32(fgList[iLine, "ID"]), Convert.ToInt32(fgList[iLine, "Contract_ID"]));

                        Global.DMS_UploadFile(sNewFile, "\\Customers\\" + fgList[iLine, "ContractTitle"] + "\\Invoices", Path.GetFileName(sNewFile));

                        fgList[iLine, 0] = false;
                        fgList[iLine, 1] = 1;
                        //fgList[iLine, "Invoice_Num"] = sInvoiceCode + " " + (sSeira + " " + iNum).Trim();
                        fgList[iLine, "FileName"] = Path.GetFileName(sNewFile);
                        fgList.Refresh();

                        clsCustodyFees_Recs CF_Recs = new clsCustodyFees_Recs();
                        CF_Recs.Record_ID = Convert.ToInt32(fgList[iLine, "ID"]);
                        CF_Recs.GetRecord();
                        CF_Recs.Invoice_ID = iID;
                        CF_Recs.Invoice_Num = fgList[iLine, "Invoice_Num"].ToString();
                        CF_Recs.Invoice_File = Path.GetFileName(sNewFile);
                        CF_Recs.DateFees = dIssueDate;
                        CF_Recs.Status = 1;
                        CF_Recs.EditRecord();
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

                sTemp = sPDF_FullPath + "\\CustF_" + sNum + ".docx";
                if (File.Exists(sTemp)) File.Delete(sTemp);

                sTemp = sPDF_FullPath + "\\CustF_" + sNum + ".pdf";
                if (File.Exists(sTemp)) File.Delete(sTemp);

                sTemp = sPDF_FullPath + "\\CustF_" + sNum + "_sig.pdf";
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
            InvoiceTitles.SourceType = 6;                                                   // 1 - RTO, 2 - FX, 3 - MF, 4 - AF, 5 - PF, 6 - CustodyF
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
            clsCustodyFees_Recs CF_Recs = new clsCustodyFees_Recs();

            if (iAktion == 0)
            {
                CF_Recs.CT_ID = iCT_ID;
                CF_Recs.Client_ID = iClient_ID;
            }
            else
            {
                CF_Recs.Record_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                CF_Recs.GetRecord();
            }

            CF_Recs.DateFrom = dFrom.Value;
            CF_Recs.DateTo = dTo.Value;
            CF_Recs.Code = lblCode.Text;
            CF_Recs.Portfolio = lblPortfolio.Text;
            CF_Recs.Currency = lblCurrency.Text;
            CF_Recs.Contract_ID = iContract_ID;
            CF_Recs.Contract_Details_ID = iContract_Details_ID;
            CF_Recs.Contract_Packages_ID = iContract_Packages_ID;
            CF_Recs.Days = Convert.ToInt32(lblDays.Text);
            CF_Recs.AUM = Convert.ToDecimal(txtAUM.Text);
            CF_Recs.AmoiviPro = Convert.ToSingle(lblAmoiviPro.Text);
            CF_Recs.AxiaPro = Convert.ToSingle(lblAxiaPro.Text);
            CF_Recs.AmoiviAfter = Convert.ToSingle(lblAmoiviAfter.Text);
            CF_Recs.AxiaAfter = Convert.ToSingle(txtAxiaAfter.Text);
            CF_Recs.Discount_Percent1 = Convert.ToSingle(txtDiscount_Percent1.Text);
            CF_Recs.Discount_Amount1 = Convert.ToSingle(txtDiscount_Amount1.Text);
            CF_Recs.Discount_Percent2 = Convert.ToSingle(txtDiscount_Percent2.Text);
            CF_Recs.Discount_Amount2 = Convert.ToSingle(txtDiscount_Amount2.Text);
            CF_Recs.Discount_Percent = Convert.ToSingle(lblDiscount_Percent.Text);
            CF_Recs.Discount_Amount = Convert.ToSingle(lblDiscount_Amount.Text);
            CF_Recs.MinAmoivi = Convert.ToSingle(lblMinAmoivi.Text);
            CF_Recs.MinAmoivi_Percent = Convert.ToSingle(txtMinAmoivi_Percent.Text);
            CF_Recs.FinishMinAmoivi = Convert.ToDecimal(txtFinishMinAmoivi.Text);
            CF_Recs.LastAmount = Convert.ToDecimal(txtLastAmount.Text);
            CF_Recs.VAT_Percent = Convert.ToSingle(txtVAT_Percent.Text);
            CF_Recs.VAT_Amount = Convert.ToSingle(txtVAT_Amount.Text);
            CF_Recs.FinishAmount = Convert.ToDecimal(txtFinishAmount.Text);
            CF_Recs.LastAmount_Percent = Convert.ToSingle(lblLastAmount_Percent.Text);
            CF_Recs.Invoice_Type = Convert.ToInt32(hdnInvoice_Type.Text);
            if (CF_Recs.Invoice_Type == 5) CF_Recs.Invoice_External = hdnSeira.Text;            // 5 - only for AKYROTIKO
            CF_Recs.Service_ID = iService_ID;
            CF_Recs.Notes = txtNotes.Text;
            CF_Recs.User_ID = Global.User_ID;
            CF_Recs.DateEdit = DateTime.Now;

            if (iAktion == 0) CF_Recs.InsertRecord();
            else CF_Recs.EditRecord();

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
                    EXL.Cells[i + 2, j - 1].Value = fgList[i, j];
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
