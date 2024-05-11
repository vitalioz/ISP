using System;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using C1.Win.C1FlexGrid;
using Core;

namespace Accounting
{
    public partial class frmAcc_InvoicesFX : Form
    {
        int i, iID, iRightsLevel, iCopies, iInvoiceFisiko, iInvoiceNomiko, iInvoicePistotikoFisiko, iInvoicePistotikoNomiko, iInvoiceAkyrotiko;
        string sInvoicePrinter, sCodeAkyrotiko = "", sInvTitleFisikoGr = "", sInvTitleFisikoEn = "", sInvoiceCodeFisiko = "",
               sInvTitleNomikoGr = "", sInvTitleNomikoEn = "", sInvoiceCodeNomiko = "", sInvoiceTypeFisiko = "", sInvoiceTypeNomiko = "",
               sSeiraPistotikoFisiko = "", sSeiraPistotikoNomiko = "", sSeiraAkyrotiko = "", sInvoiceFXTemplate = "",
       sInvoiceCodePistotikoFisiko = "", sInvTitlePistotikoFisikoGr = "", sInvTitlePistotikoFisikoEn = "", sInvoiceTypePistotikoFisiko = "",
       sInvoiceCodePistotikoNomiko = "", sInvTitlePistotikoNomikoGr = "", sInvTitlePistotikoNomikoEn = "", sInvoiceTypePistotikoNomiko = "",
       sInvoiceCodeAkyrotiko = "", sInvTitleAkyrotikoGr = "", sInvTitleAkyrotikoEn = "", sInvoiceTypeAkyrotiko = "",
       sSeiraFisiko = "", sSeiraNomiko = "", sExtra;
        DateTime dIssueDate;
        DataView dtView;
        DataRow[] foundRows;
        bool bCheckList;
        C1.Win.C1FlexGrid.CellRange rng;
        Hashtable imgMap = new Hashtable();
        clsOrdersFX OrdersFX = new clsOrdersFX();
        public frmAcc_InvoicesFX()
        {
            InitializeComponent();

            dIssueDate = Convert.ToDateTime("1900/01/01");
        }

        private void frmAcc_InvoicesFX_Load(object sender, EventArgs e)
        {
            bCheckList = false;
            panTools.Visible = false;
            chkPrint.Visible = false;
            fgList.Visible = false;

            cmbServiceProviders.DataSource = Global.dtServiceProviders.Copy();
            cmbServiceProviders.DisplayMember = "Title";
            cmbServiceProviders.ValueMember = "ID";
            cmbServiceProviders.SelectedItem = 1;


            for (i = 0; i < imgFiles.Images.Count; i++) imgMap.Add(i, imgFiles.Images[i]);

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString(Global.GridStyle);
            fgList.DrawMode = DrawModeEnum.OwnerDraw;
            fgList.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgList_BeforeEdit);
            fgList.Click += new System.EventHandler(fgList_Click);
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
            rng.Data = Global.GetLabel("contract");

            fgList.Cols[4].AllowMerging = true;
            rng = fgList.GetCellRange(0, 4, 1, 4);
            rng.Data = Global.GetLabel("code");

            fgList.Cols[5].AllowMerging = true;
            rng = fgList.GetCellRange(0, 5, 1, 5);
            rng.Data = Global.GetLabel("subaccount");

            fgList.Cols[6].AllowMerging = true;
            rng = fgList.GetCellRange(0, 6, 1, 6);
            rng.Data = Global.GetLabel("provider");

            fgList.Cols[7].AllowMerging = true;
            rng = fgList.GetCellRange(0, 7, 1, 7);
            rng.Data = Global.GetLabel("service");

            fgList.Cols[8].AllowMerging = true;
            rng = fgList.GetCellRange(0, 8, 1, 8);
            rng.Data = Global.GetLabel("profile");

            fgList.Cols[9].AllowMerging = true;
            rng = fgList.GetCellRange(0, 9, 1, 9);
            rng.Data = "Ημερ.Συναλλαγής";

            fgList.Cols[10].AllowMerging = true;
            rng = fgList.GetCellRange(0, 10, 1, 10);
            rng.Data = Global.GetLabel("currency");

            fgList.Cols[11].AllowMerging = true;
            rng = fgList.GetCellRange(0, 11, 1, 11);
            rng.Data = Global.GetLabel("amount");

            rng = fgList.GetCellRange(0, 12, 0, 17);
            rng.Data = "Έξοδα Διαβίβασης Εντολής Μετατροπής";
            fgList[1, 12] = "% σύμβασης";
            fgList[1, 13] = "% έκπτωση";
            fgList[1, 14] = "τελικό %";
            fgList[1, 15] = "ποσό μετά την έκπτωση";
            fgList[1, 16] = "Ισοτιμία μετατροπής (EUR)";
            fgList[1, 17] = "ποσό σε EUR";

            fgList.Cols[18].AllowMerging = true;
            rng = fgList.GetCellRange(0, 18, 1, 18);
            rng.Data = "ΦΠΑ";

            fgList.Cols[19].AllowMerging = true;
            rng = fgList.GetCellRange(0, 19, 1, 19);
            rng.Data = "Πληρωτέο Ποσό";

            fgList.Cols[20].AllowMerging = true;
            rng = fgList.GetCellRange(0, 20, 1, 20);
            rng.Data = "Αρ.Παραστατικου";

            fgList.Cols[21].AllowMerging = true;
            rng = fgList.GetCellRange(0, 21, 1, 21);

            fgList.Cols[22].AllowMerging = true;
            rng = fgList.GetCellRange(0, 22, 1, 22);
            rng.Data = Global.GetLabel("address");

            fgList.Cols[23].AllowMerging = true;
            rng = fgList.GetCellRange(0, 23, 1, 23);
            rng.Data = Global.GetLabel("city");

            fgList.Cols[24].AllowMerging = true;
            rng = fgList.GetCellRange(0, 24, 1, 24);
            rng.Data = Global.GetLabel("zip");

            fgList.Cols[25].AllowMerging = true;
            rng = fgList.GetCellRange(0, 25, 1, 25);
            rng.Data = Global.GetLabel("country");

            fgList.Cols[26].AllowMerging = true;
            rng = fgList.GetCellRange(0, 26, 1, 26);
            rng.Data = Global.GetLabel("afm");

            fgList.Cols[27].AllowMerging = true;
            rng = fgList.GetCellRange(0, 27, 1, 27);
            rng.Data = Global.GetLabel("doy");

            fgList.Cols[28].AllowMerging = true;
            rng = fgList.GetCellRange(0, 28, 1, 28);
            rng.Data = "Cash Account EUR";

            fgList.Cols[29].AllowMerging = true;
            rng = fgList.GetCellRange(0, 29, 1, 29);
            rng.Data = "Advisor";

            fgList.Cols[30].AllowMerging = true;
            rng = fgList.GetCellRange(0, 30, 1, 30);
            rng.Data = "RM";

            fgList.Cols[31].AllowMerging = true;
            rng = fgList.GetCellRange(0, 31, 1, 31);
            rng.Data = "Introducer";

            fgList.Cols[32].AllowMerging = true;
            rng = fgList.GetCellRange(0, 32, 1, 32);
            rng.Data = "Διαχειρηστής";       

            fgList.Cols[33].AllowMerging = true;
            rng = fgList.GetCellRange(0, 33, 1, 33);
            rng.Data = "ID εντολής";

            Column clm1 = fgList.Cols["image_map"];
            clm1.ImageMap = imgMap;
            clm1.ImageAndText = false;
            clm1.ImageAlign = ImageAlignEnum.CenterCenter;

            ucExec.DateFrom = DateTime.Now.AddDays(-30);
            ucExec.DateTo = DateTime.Now;

            DefineOptions();

            cmbFilter.SelectedIndex = 0;
            bCheckList = true;

        }
        protected override void OnResize(EventArgs e)
        {
            panCritiries.Width = this.Width - 30;
            btnSearch.Left = panCritiries.Width - 120;

            fgList.Height = this.Height - 152;
            fgList.Width = this.Width - 30;
            panTools.Width = this.Width - 30;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DefineList();
            bCheckList = true;
            ShowList();
            panTools.Visible = true;
            chkPrint.Visible = true;
            fgList.Visible = true;
        }
        private void fgList_DoubleClick(object sender, EventArgs e)
        {
            if (fgList.Row > 1) {
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
            locContract.ClientType = 1;
            locContract.ClientFullName = fgList[fgList.Row, "ContractTitle"]+"";
            locContract.RightsLevel = Convert.ToInt32(iRightsLevel);
            locContract.ShowDialog();
        }
        private void mnuClientData_Click(object sender, EventArgs e)
        {
            frmClientData locClientData = new frmClientData();
            locClientData.Client_ID = Convert.ToInt32(fgList[fgList.Row, "Client_ID"]);
            locClientData.Show();
        }
        private void fgList_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (bCheckList)
            {
                if (e.Col == 0) e.Cancel = false;
                else e.Cancel = true;
            }
        }
        private void fgList_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Row > 0)
            {
                //if (Convert.ToInt32(fgList[e.Row, "User_ID"]) != 0) fgList.Rows[e.Row].Style = csChecked;
            }
        }
        private void tsbAdd_Click(object sender, EventArgs e)
        {
                                              
            ShowRecord(0);                                                   
        }
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 1) ShowRecord(1);
        }

        private void cmbServiceProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bCheckList) ShowList();
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowList();
        }
        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            ShowList();
        }
        private void mnuPistotiko_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 1) ShowRecord(4);
        }

        private void mnuAkyrotiko_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 1) ShowRecord(5);
        }
        private void tsbFeesCalculation_Click(object sender, EventArgs e)
        {
            this.Refresh();
            this.Cursor = Cursors.WaitCursor;

            clsOrdersFX klsOrderFX = new clsOrdersFX();
            klsOrderFX.DateFrom = ucExec.DateFrom.Date;
            klsOrderFX.DateTo = ucExec.DateTo.Date;
            klsOrderFX.CalcRTOFees();

            DefineList();
            ShowList();
            this.Cursor = Cursors.Default;

            MessageBox.Show("Calculation Finished", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void chkPrint_CheckedChanged(object sender, EventArgs e)
        {
            for (i = 2; i <= fgList.Rows.Count - 2; i++) fgList[i, 0] = chkPrint.Checked;
        }
        private void tsbPrint_Click(object sender, EventArgs e)
        {
            PrintInvoice();
        }

        private void mnuPrintInvoice_Click(object sender, EventArgs e)
        {
            fgList[fgList.Row, 0] = true;
            PrintInvoice();
        }
        private void PrintInvoice()
        {            
            if (dIssueDate == Convert.ToDateTime("1900/01/01")) {
                sInvoicePrinter = Global.InvoicePrinter.Trim();
                frmPrintInvoiceOptions PrintInvoiceOptions = new frmPrintInvoiceOptions();
                PrintInvoiceOptions.Mode = 2;                                               // 1 - full options, 2 - hide IssueDate
                PrintInvoiceOptions.InvoicePrinter = Global.InvoicePrinter;
                PrintInvoiceOptions.NumCopies = iCopies;
                PrintInvoiceOptions.DateIssue = Convert.ToDateTime(fgList[fgList.Row, "ExecuteDate"]);
                PrintInvoiceOptions.ShowDialog();
                if (PrintInvoiceOptions.LastAktion == 1)
                {
                    sInvoicePrinter = PrintInvoiceOptions.InvoicePrinter;
                    iCopies = PrintInvoiceOptions.NumCopies;
                    dIssueDate = PrintInvoiceOptions.DateIssue;

                    PrintingInvoices();
                }
            }
            else PrintingInvoices();
        }
        private void DefineList()
        {
            OrdersFX = new clsOrdersFX();
            OrdersFX.CommandType_ID = 1;
            OrdersFX.DateFrom = ucExec.DateFrom;
            OrdersFX.DateTo = ucExec.DateTo;
            OrdersFX.StockCompany_ID = 0;
            OrdersFX.Code = "";
            OrdersFX.GetList();
        }
        private void ShowList()
        {
            if (bCheckList)
            {
                fgList.Redraw = false;
                fgList.Rows.Count = 2;
                int i = 0;

                dtView = new DataView(OrdersFX.List);
                dtView.RowFilter = "ExecuteDate > '1900/01/02 00:00:00'";
                dtView.Sort = "ExecuteDate ASC";

                foreach (DataRowView dtViewRow in dtView)
                {
                    if (((Convert.ToInt32(cmbServiceProviders.SelectedValue) == 0) || (Convert.ToInt32(dtViewRow["StockCompany_ID"]) == Convert.ToInt32(cmbServiceProviders.SelectedValue))) &&
                        ((cmbFilter.SelectedIndex < 1) || (cmbFilter.SelectedIndex == 1 && dtViewRow["Invoice_Num"].ToString() != "") || (cmbFilter.SelectedIndex == 2 && dtViewRow["Invoice_Num"].ToString() == "")) &&
                        (txtCode.Text.Trim() == "" || dtViewRow["Code"].ToString().Contains(txtCode.Text)) )
                    {

                        i = i + 1;
                        fgList.AddItem(false + "\t" + Convert.ToInt16(dtViewRow["ImageType"]) + "\t" + i + "\t" + dtViewRow["ContractTitle"] + "\t" + dtViewRow["Code"] + "\t" + dtViewRow["Portfolio"] + "\t" +
                             dtViewRow["Company_Title"] + "\t" + dtViewRow["ServiceTitle"] + "\t" + dtViewRow["ProfileTitle"] + "\t" + dtViewRow["ExecuteDate"] + "\t" +
                             dtViewRow["CurrFrom"] + "\t" + dtViewRow["RealAmountFrom"] + "\t" + dtViewRow["RTO_FeesPercent"] + "\t" + dtViewRow["RTO_DiscountPercent"] + "\t" + 
                             dtViewRow["RTO_FinishFeesPercent"] + "\t" + dtViewRow["RTO_FeesAmount"] + "\t" + dtViewRow["RTO_FeesRate"] + "\t" + dtViewRow["RTO_FeesAmountEUR"] + "\t" + 
                             "0" + "\t" + dtViewRow["RTO_FeesAmountEUR"] + "\t" + dtViewRow["Invoice_Num"] + "\t" +
                             dtViewRow["ClientName"] + "\t" + dtViewRow["Address"] + "\t" + dtViewRow["City"] + "\t" + dtViewRow["Zip"] + "\t" + dtViewRow["CountryTitleGr"] + "\t" +
                             dtViewRow["AFM"] + "\t" + dtViewRow["DOY"] + "\t" + dtViewRow["CashAccount_From"] + "\t" + dtViewRow["Advisor_Fullname"] + "\t" + dtViewRow["RM_Fullname"] + "\t" +
                             dtViewRow["Diax_Fullname"] + "\t" + dtViewRow["Intro_Fullname"] + "\t" + dtViewRow["ID"] + "\t" + dtViewRow["BusinessType_ID"] + "\t" +
                             dtViewRow["ClientTipos"] + "\t" + dtViewRow["Client_ID"] + "\t" + dtViewRow["Contract_ID"] + "\t" + dtViewRow["FileName"] + "\t" +
                             dtViewRow["Contracts_Details_ID"] + "\t" + dtViewRow["Contracts_Packages_ID"] + "\t" + dtViewRow["Invoice_Type"] + "\t" + dtViewRow["CountryTitleEn"]);
                    }
                }
                fgList.Redraw = true;
                DefineSums();
            }
        }
        private void DefineSums()
        {
            fgList.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, -1, 17, "");
            fgList.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, -1, 18, "");
            fgList.Subtotal(C1.Win.C1FlexGrid.AggregateEnum.Sum, 0, -1, 19, "");
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

                        sTemp = sPDF_FullPath + "\\FX_" + sNum + ".docx";
                        if (File.Exists(sTemp)) File.Delete(sTemp);
                                                
                        File.Copy(Application.StartupPath + "\\Templates\\" + sInvoiceFXTemplate, sTemp);
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
                        curDoc.Content.Find.Execute(FindText: "{address}", ReplaceWith: fgList[iLine, "Address"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{city}", ReplaceWith: fgList[iLine, "City"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{zip}", ReplaceWith: fgList[iLine, "Zip"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{country}", ReplaceWith: sCountry, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{AFM}", ReplaceWith: fgList[iLine, "AFM"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{DOY}", ReplaceWith: fgList[iLine, "DOY"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{invoice_num}", ReplaceWith: iNum, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        dIssueDate = Convert.ToDateTime(fgList[iLine, "ExecuteDate"]);
                        curDoc.Content.Find.Execute(FindText: "{issue_date}", ReplaceWith: dIssueDate.ToString("dd/MM/yyyy"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{amount}", ReplaceWith: Math.Abs(Convert.ToDecimal(fgList[iLine, "LastAmount"])).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{vat}", ReplaceWith: Math.Abs(Convert.ToSingle(fgList[iLine, "VAT_Amount"])).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{axia}", ReplaceWith: Math.Abs(Convert.ToDecimal(fgList[iLine, "FinishAmount"])).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{eafdss}", ReplaceWith: sEafdss, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);

                        sTemp = sPDF_FullPath + "\\InvoiceFX_" + sNum + ".pdf";
                        curDoc.SaveAs2(sTemp, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF);  
                        sNewFile ="InvoiceFX_" + (sSeira + " " + iNum).Trim() + ".pdf";
                        sLastFileName = Global.DMS_UploadFile(sTemp, "Customers/" + (fgList[iLine, "ContractTitle"] + "").Replace(".", "_") + "/Invoices", Path.GetFileName(sNewFile) );
                        iID = SaveRecord(iLine, iInvoiceType, sSeira, iNum, Path.GetFileName(sLastFileName), Convert.ToInt32(fgList[iLine, "ID"]), Convert.ToInt32(fgList[iLine, "Contract_ID"]));


                        /*
                        Global.PrintPDF(sTemp);
                        for (i = 0; i <= 20; i++)
                            if (!File.Exists(sPDF_FullPath + "\\Signature_Processor__sig.pdf")) System.Threading.Thread.Sleep(3000);
                            else break;

                        if (File.Exists(sPDF_FullPath + "\\Signature_Processor__sig.pdf")) System.IO.File.Move(sPDF_FullPath + "\\Signature_Processor__sig.pdf", sNewFile);
                        else System.IO.File.Move(sPDF_FullPath + "\\FX_" + sNum + ".pdf", sNewFile);

                        sLastFileName = Global.DMS_UploadFile(sNewFile, "/Customers/" + fgList[iLine, "ContractTitle"] + "/Invoices", Path.GetFileName(sNewFile));
                        */

                        //--- refresh fgList row ----------------------------------------------------------
                        fgList[iLine, 0] = false;
                        fgList[iLine, 1] = 1;
                        fgList[iLine, "Invoice_Num"] = sInvoiceCode + " " + (sSeira + " " + iNum).Trim();
                        fgList[iLine, "FileName"] = Path.GetFileName(sLastFileName);
                        fgList.Refresh();

                        //--- save Invoices_Titles.ID into CommandsFX table --------------------------------
                        clsOrdersFX OrdersFX2 = new clsOrdersFX();
                        OrdersFX2.Record_ID = Convert.ToInt32(fgList[iLine, "ID"]);
                        OrdersFX2.GetRecord();
                        OrdersFX2.InvoiceTitle_ID = iID;
                        OrdersFX2.EditRecord();

                        WordApp.ScreenUpdating = false;
                        WordApp.Documents.Close();
                        SendKeys.SendWait("{Enter}");

                        //--- delete temporary files .docx and .pdf ----------------------------------------
                        sTemp = sPDF_FullPath + "\\FX_" + sNum + ".docx";
                        if (File.Exists(sTemp)) File.Delete(sTemp);

                        sTemp = sPDF_FullPath + "\\InvoiceFX_" + sNum + ".pdf";
                        if (File.Exists(sTemp)) File.Delete(sTemp);
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
            InvoiceTitles.SourceType = 2;                                                   // 1 - RTO, 2 - FX, 3 - MF, 4 - AF, 5 - PF, 6 - CustodyF
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
            frmOrderFX locOrderFX = new frmOrderFX();
            locOrderFX.Record_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
            locOrderFX.Editable = 0;
            locOrderFX.Mode = 2;                            // 1 - from frmDailyFX, 2 - from frmAcc_InvoicesFX
            locOrderFX.ShowDialog();
            if (locOrderFX.LastAktion == 1) {
                fgList[fgList.Row, "RTO_FeesPercent"] = locOrderFX.lblRTO_FeesPercent.Text;
                fgList[fgList.Row, "RTO_DiscountPercent"] = locOrderFX.txtRTO_DiscountPercent.Text;
                fgList[fgList.Row, "RTO_FinishFeesPercent"] = locOrderFX.lblRTO_FinishFeesPercent.Text;
                fgList[fgList.Row, "RTO_FeesAmount"] = locOrderFX.lblRTO_FeesAmount.Text;
                fgList[fgList.Row, "RTO_FeesCurrRate"] = locOrderFX.txtRTO_FeesCurrRate.Text;
                fgList[fgList.Row, "LastAmount"] = locOrderFX.lblRTO_FeesAmountEUR.Text;
                fgList[fgList.Row, "FinishAmount"] = locOrderFX.lblRTO_FeesAmountEUR.Text;
            }
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

            sInvoiceFXTemplate = Options.InvoiceFXTemplate;
        }
        private void tsbExcel_Click(object sender, EventArgs e)
        {
            int j = 0;
            var EXL = new Microsoft.Office.Interop.Excel.Application();
            var oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            // System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US"]
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("el-GR");
            EXL.Workbooks.Add();
            EXL.ScreenUpdating = false;
            EXL.Caption = Global.AppTitle;
            EXL.Cells[1, 3].Value = "Τιμολόγηση FX Fees";
            for (i = 0; i <= (fgList.Rows.Count - 1); i++)
                for (j = 2; j <= 32; j++)
                    EXL.Cells[i + 2, j - 1].Value = fgList[i, j];

            EXL.ScreenUpdating = true;
            EXL.Visible = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
