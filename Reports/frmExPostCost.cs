using System;
using System.Data;
using System.IO;
using System.Globalization;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using System.Runtime.InteropServices;
using System.Threading;
using Core;

namespace Reports
{
    public partial class frmExPostCost : Form
    {
        int i, j, iEPCT_ID, iRow, iRightsLevel;
        clsExPostCost_Title klsExPostCost_Title = new clsExPostCost_Title();
        clsExPostCost_Recs klsExPostCost_Recs = new clsExPostCost_Recs();
        clsContracts klsContract = new clsContracts();
        public frmExPostCost()
        {
            InitializeComponent();

            panFinish.Left = (Screen.PrimaryScreen.Bounds.Width - panFinish.Width) / 2;
            panFinish.Top = (Screen.PrimaryScreen.Bounds.Height - panFinish.Height) / 2;
        }

        private void frmExPostCost_Load(object sender, EventArgs e)
        {

            for (i = 2010; i <= DateTime.Now.Year; i++) cmbYear.Items.Add(i);
            cmbYear.SelectedItem = DateTime.Now.Year;

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.Focus.BackColor = Global.GridHighlightForeColor;
            fgList.Styles.ParseString(Global.GridStyle);
            fgList.DrawMode = DrawModeEnum.OwnerDraw;
            fgList.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgList_BeforeEdit);
            fgList.MouseDown += new MouseEventHandler(fgList_MouseDown);
            fgList.ShowCellLabels = true;
        }
        protected override void OnResize(EventArgs e)
        {
            panCritiries.Width = this.Width - 30;
            btnSearch.Left = panCritiries.Width - 120;

            fgList.Height = this.Height - 140;
            fgList.Width = this.Width - 30;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            toolLeft.Enabled = true;

            fgList.Redraw = false;
            fgList.Rows.Count = 1;

            clsExPostCost_Title klsExPostCost_Title = new clsExPostCost_Title();
            klsExPostCost_Title.EPC_Year = Convert.ToInt32(cmbYear.Text);
            klsExPostCost_Title.GetRecord_Title();
            if (klsExPostCost_Title.Record_ID > 0) {
                iEPCT_ID = klsExPostCost_Title.Record_ID;

                clsExPostCost_Recs klsExPostCost_Recs = new clsExPostCost_Recs();
                klsExPostCost_Recs.EPCT_ID = iEPCT_ID;
                klsExPostCost_Recs.GetList();
                if (klsExPostCost_Recs.List.Rows.Count > 0) {
                    i = 0;
                    foreach (DataRow dtRow in klsExPostCost_Recs.List.Rows)
                    {
                        i = i + 1;
                        fgList.AddItem(false + "\t" + i + "\t" + dtRow["ContractTitle"] + "\t" + dtRow["ServiceProvider_Title"] + "\t" + dtRow["Code"] + "\t" + dtRow["Portfolio"] + "\t" +
                                   dtRow["Service_Title"] + "\t" + dtRow["DateFrom"] + "\t" + dtRow["DateTo"] + "\t" + dtRow["AverageExchangedFund"] + "\t" + dtRow["NetRTOFees"] + "\t" +
                                   dtRow["NetManagmetFees"] + "\t" + dtRow["NetSuccessFees"] + "\t" + dtRow["NetAdminFees"] + "\t" + dtRow["NetFXFees"] + "\t" + dtRow["VAT"] + "\t" + 
                                   dtRow["TotalFees"] + "\t" + dtRow["RTOFees_Percent"] + "\t" + dtRow["ManagmetFees_Percent"] + "\t" + dtRow["SuccessFees_Percent"] + "\t" + 
                                   dtRow["AdminFees_Percent"] + "\t" + dtRow["FXFees_Percent"] + "\t" + dtRow["VAT_Percent"] + "\t" + dtRow["Total_Percent"] + "\t" + 
                                   dtRow["FileName"] + "\t" + dtRow["DateSent"] + "\t" + dtRow["ID"] + "\t" + dtRow["Contract_ID"] + "\t" + dtRow["Contracts_Details_ID"] + "\t" + 
                                   dtRow["Contracts_Packages_ID"] + "\t" + dtRow["Client_ID"] + "\t" + dtRow["Service_ID"] + "\t" + dtRow["ServiceProvider_ID"]);
                    }
                }
            }
            else {
                if (MessageBox.Show("Ex Post Cost report αυτής της χρονίας δεν υπάρχει. \n\n Να δημιουργηθεί καινούργιο ; " , Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    klsExPostCost_Title = new clsExPostCost_Title();
                    klsExPostCost_Title.EPC_Year = Convert.ToInt32(cmbYear.Text);
                    klsExPostCost_Title.Author_ID = Global.User_ID;
                    klsExPostCost_Title.DateIns = DateTime.Now;
                    iEPCT_ID = klsExPostCost_Title.InsertRecord();
                }
            }

        fgList.Redraw = true;
        }
        private void tsbImport_Click(object sender, EventArgs e)
        {
            panImport.Visible = true;
        }
        private void tsbDel_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 0)
            {
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    klsExPostCost_Recs = new clsExPostCost_Recs();
                    klsExPostCost_Recs.Record_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                    klsExPostCost_Recs.DeleteRecord();
                    fgList.RemoveItem(fgList.Row);
                }
            }
        }
        private void picClose_Click(object sender, EventArgs e)
        {
            panImport.Visible = false;
        }

        private void picFilesPath_Click(object sender, EventArgs e)
        {
            txtFilePath.Text = Global.FileChoice(Global.DefaultFolder);
        }

        private void btnGetImport_Click(object sender, EventArgs e)
        {
            if (txtFilePath.Text.Length > 0)
            {
                string x6, x7, x8, x9, x10, x11, x12, x13, x14, x15;
                decimal x16, x17, x18, x19, x20, x21, x22;
                string sTemp = "";

                var ExApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = ExApp.Workbooks.Open(txtFilePath.Text);
                Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

                iEPCT_ID = 0;

                klsExPostCost_Title = new clsExPostCost_Title();
                klsExPostCost_Title.EPC_Year = Convert.ToInt32(cmbYear.Text);
                klsExPostCost_Title.GetRecord_Title();
                iEPCT_ID = klsExPostCost_Title.Record_ID;
                if (iEPCT_ID == 0)
                {
                    klsExPostCost_Title.EPC_Year = Convert.ToInt32(cmbYear.Text);
                    klsExPostCost_Title.DateIns = DateTime.Now;
                    klsExPostCost_Title.Author_ID = Global.User_ID;
                    iEPCT_ID = klsExPostCost_Title.InsertRecord();
                }

                this.Refresh();
                this.Cursor = Cursors.WaitCursor;

                i = 1;
                fgList.Redraw = false;
                while (true)
                {
                    i = i + 1;
                    sTemp = (xlRange.Cells[i, 3].Value + "").ToString();
                    if (sTemp == "") break;

                    klsContract = new clsContracts();
                    klsContract.Code = sTemp;

                    sTemp = (xlRange.Cells[i, 4].Value + "").ToString();
                    klsContract.Portfolio = sTemp;
                    klsContract.GetRecord_Code_Portfolio();

                    x6 = (xlRange.Cells[i, 6].Value + "").ToString();
                    x7 = (xlRange.Cells[i, 7].Value + "").ToString();
                    x8 = (xlRange.Cells[i, 8].Value + "").ToString();
                    x9 = (xlRange.Cells[i, 9].Value + "").ToString();
                    x10 = (xlRange.Cells[i, 10].Value + "").ToString();
                    x11 = (xlRange.Cells[i, 11].Value + "").ToString();
                    x12 = (xlRange.Cells[i, 12].Value + "").ToString();
                    x13 = (xlRange.Cells[i, 13].Value + "").ToString();
                    x14 = (xlRange.Cells[i, 14].Value + "").ToString();
                    x15 = (xlRange.Cells[i, 15].Value + "").ToString();                    
                    x16 = Convert.ToDecimal(xlRange.Cells[i, 16].Value);
                    x17 = Convert.ToDecimal(xlRange.Cells[i, 17].Value);
                    x18 = Convert.ToDecimal(xlRange.Cells[i, 18].Value);
                    x19 = Convert.ToDecimal(xlRange.Cells[i, 19].Value);
                    x20 = Convert.ToDecimal(xlRange.Cells[i, 20].Value);
                    x21 = Convert.ToDecimal(xlRange.Cells[i, 21].Value);
                    x22 = Convert.ToDecimal(xlRange.Cells[i, 22].Value);

                    fgList.AddItem(false + "\t" + (i - 1) + "\t" + klsContract.ContractTitle + "\t" + klsContract.BrokerageServiceProvider_Title + "\t" + klsContract.Code + "\t" +
                                           klsContract.Portfolio + "\t" + klsContract.Service_Title + "\t" + x6 + "\t" + x7 + "\t" +
                                           x8 + "\t" + x9 + "\t" + x10 + "\t" + x11 + "\t" + x12 + "\t" + x13 + "\t" + x14 + "\t" + x15 + "\t" +
                                           Convert.ToDecimal(x16) * 100 + "\t" + Convert.ToDecimal(x17) * 100 + "\t" + Convert.ToDecimal(x18) * 100 + "\t" +
                                           Convert.ToDecimal(x19) * 100 + "\t" + Convert.ToDecimal(x20) * 100 + "\t" + Convert.ToDecimal(x21) * 100 + "\t" + 
                                           Convert.ToDecimal(x22) * 100 + "\t" + "" + "\t" + "" + "\t" + 0 + "\t" + klsContract.Record_ID + "\t" + 
                                           klsContract.Contract_Details_ID + "\t" + klsContract.Contract_Packages_ID + "\t" + klsContract.Client_ID + "\t" + 
                                           klsContract.Packages.Service_ID + "\t" + klsContract.BrokerageServiceProvider_ID);
                }

                fgList.Redraw = true;
                fgList.Visible = true;

                this.Refresh();
                this.Cursor = Cursors.Default;

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

                panImport.Visible = false;

            }
            else
                MessageBox.Show("Καταχώρήστε το αρχείο εισαγωγής", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            EXL.Cells[1, 3].Value = "ExPostCost";
            var loopTo = fgList.Rows.Count - 1;
            for (this.i = 0; this.i <= loopTo; this.i++)
            {
                for (this.j = 1; this.j <= 25; this.j++)
                    EXL.Cells[i + 2, j].Value = fgList[i, j];
            }

            EXL.ScreenUpdating = true;
            EXL.Visible = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            for (i = 1; i <= fgList.Rows.Count - 1; i++) {
                klsExPostCost_Recs = new clsExPostCost_Recs();
                if (Convert.ToInt32(fgList[i, "ID"]) == 0) {                    
                    klsExPostCost_Recs.EPCT_ID = iEPCT_ID;
                    klsExPostCost_Recs.Contract_ID = Convert.ToInt32(fgList[i, "Contract_ID"]);
                    klsExPostCost_Recs.Contract_Details_ID = Convert.ToInt32(fgList[i, "Contract_Details_ID"]);
                    klsExPostCost_Recs.Contract_Packages_ID = Convert.ToInt32(fgList[i, "Contract_Packages_ID"]);
                    klsExPostCost_Recs.DateFrom = Convert.ToDateTime(fgList[i, "DateFrom"]);
                    klsExPostCost_Recs.DateTo = Convert.ToDateTime(fgList[i, "DateTo"]);
                    klsExPostCost_Recs.AverageExchangedFund = Convert.ToDecimal(fgList[i, "AverageExchangedFund"]);
                    klsExPostCost_Recs.NetRTOFees = Convert.ToDecimal(fgList[i, "NetRTOFees"]);
                    klsExPostCost_Recs.NetManagmetFees = Convert.ToDecimal(fgList[i, "NetManagmetFees"]);
                    klsExPostCost_Recs.NetSuccessFees = Convert.ToDecimal(fgList[i, "NetSuccessFees"]);
                    klsExPostCost_Recs.NetAdminFees = Convert.ToDecimal(fgList[i, "NetAdminFees"]);
                    klsExPostCost_Recs.NetFXFees = Convert.ToDecimal(fgList[i, "NetFXFees"]);
                    klsExPostCost_Recs.VAT = Convert.ToDecimal(fgList[i, "VAT"]);
                    klsExPostCost_Recs.TotalFees = Convert.ToDecimal(fgList[i, "TotalFees"]);
                    klsExPostCost_Recs.RTOFees_Percent = Convert.ToDecimal(fgList[i, "RTOFees_Percent"]);
                    klsExPostCost_Recs.ManagmetFees_Percent = Convert.ToDecimal(fgList[i, "ManagmetFees_Percent"]);
                    klsExPostCost_Recs.SuccessFees_Percent = Convert.ToDecimal(fgList[i, "SuccessFees_Percent"]);
                    klsExPostCost_Recs.AdminFees_Percent = Convert.ToDecimal(fgList[i, "AdminFees_Percent"]);
                    klsExPostCost_Recs.FXFees_Percent = Convert.ToDecimal(fgList[i, "FXFees_Percent"]);
                    klsExPostCost_Recs.VAT_Percent = Convert.ToDecimal(fgList[i, "VAT_Percent"]);
                    klsExPostCost_Recs.Total_Percent = Convert.ToDecimal(fgList[i, "Total_Percent"]);
                    klsExPostCost_Recs.FileName = fgList[i, "FileName"]+"";
                    klsExPostCost_Recs.DateSent = fgList[i, "DateSent"]+"";
                    klsExPostCost_Recs.InsertRecord();
                }
                else {
                    klsExPostCost_Recs = new clsExPostCost_Recs();
                    klsExPostCost_Recs.Record_ID = Convert.ToInt32(fgList[i, "ID"]);
                    klsExPostCost_Recs.GetRecord();
                    klsExPostCost_Recs.FileName = fgList[i, "FileName"]+"";
                    klsExPostCost_Recs.DateSent = fgList[i, "DateSent"]+"";
                    klsExPostCost_Recs.EditRecord();
                }
           }
        }

        private void tsbCreatePDF_Click(object sender, EventArgs e)
        {
            this.Refresh();
            this.Cursor = Cursors.WaitCursor;
            panFinish.Visible = true;

            string sApo, sEos, sPDF_FullPath, sExPostCostTemplate, sNewFile, sLastFileName, sRow1_Gr, sRow1_En, sRow2_Gr, sRow2_En, sRow3_Gr, sRow3_En, sRow4_Gr, sRow4_En, 
                   sRow5_Gr, sRow5_En, sRow6_Gr, sRow6_En;
            var WordApp = new Microsoft.Office.Interop.Word.Application();
            var curDoc = new Microsoft.Office.Interop.Word.Document();

            clsOptions klsOptions = new clsOptions();
            klsOptions.GetRecord();
            
            sPDF_FullPath = "";
            sRow1_Gr = "";
            sRow1_En = "";
            sRow2_Gr = "";
            sRow2_En = "";
            sRow3_Gr = "";
            sRow3_En = "";
            sRow4_Gr = "";
            sRow4_En = "";
            sRow5_Gr = "";
            sRow5_En = "";
            sRow6_Gr = "";
            sRow6_En = "";
            for (iRow = 1; iRow <= fgList.Rows.Count - 1; iRow++) {
                if (Convert.ToBoolean(fgList[iRow, 0]) && (fgList[iRow, "DateSent"] + "" == "")) {
                    try
                    {
                        WordApp.Visible = false;

                        switch (Convert.ToInt32(fgList[iRow, "Service_ID"]))
                        {
                            case 1:
                                sRow1_Gr = "Αμοιβή Λήψης και Διαβίβασης Εντολής";
                                sRow1_En = "Reception and Transmission Order Fee";

                                sRow2_Gr = "";
                                sRow2_En = "";

                                sRow3_Gr = "";
                                sRow3_En = "";

                                sRow4_Gr = "Αμοιβή Υποστήριξης Λογαριασμού";
                                sRow4_En = "Administration Fee";

                                sRow5_Gr = "Αμοιβή FX";
                                sRow5_En = "FX Fee";

                                sRow6_Gr = "ΦΠΑ";
                                sRow6_En = "VAT";
                                break;
                            case 2:
                                sRow1_Gr = "Αμοιβή Λήψης και Διαβίβασης Εντολής";
                                sRow1_En = "Reception and Transmission Order Fee";

                                sRow2_Gr = "Αμοιβή Επενδυτικών Συμβουλών";
                                sRow2_En = "Management Fee";

                                sRow3_Gr = "Αμοιβή Υπεραπόδοσης";
                                sRow3_En = "Performance Fee";

                                sRow4_Gr = "Αμοιβή Υποστήριξης Λογαριασμού";
                                sRow4_En = "Administration Fee";

                                sRow5_Gr = "Αμοιβή FX";
                                sRow5_En = "FX Fee";

                                sRow6_Gr = "ΦΠΑ";
                                sRow6_En = "VAT";
                                break;
                            case 3:
                                sRow1_Gr = "Αμοιβή Διαβίβασης Εντολής";
                                sRow1_En = "Transmission Order Fee";

                                sRow2_Gr = "Αμοιβή Διαχείρισης";
                                sRow2_En = "Management Fee";

                                sRow3_Gr = "Αμοιβή Υπεραπόδοσης";
                                sRow3_En = "Performance Fee";

                                sRow4_Gr = "Αμοιβή Υποστήριξης Λογαριασμού";
                                sRow4_En = "Administration Fee";

                                sRow5_Gr = "Αμοιβή FX";
                                sRow5_En = "FX Fee";

                                sRow6_Gr = "ΦΠΑ";
                                sRow6_En = "VAT";
                                break;

                            case 5:
                                sRow1_Gr = "Αμοιβή Λήψης και Διαβίβασης Εντολής";
                                sRow1_En = "Reception and Transmission Order Fee";

                                sRow2_Gr = "Αμοιβή Επενδυτικών Συμβουλών";
                                sRow2_En = "Management Fee";

                                sRow3_Gr = "Αμοιβή Υπεραπόδοσης";
                                sRow3_En = "Performance Fee";

                                sRow4_Gr = "Αμοιβή Υποστήριξης Λογαριασμού";
                                sRow4_En = "Administration Fee";

                                sRow5_Gr = "Αμοιβή FX";
                                sRow5_En = "FX Fee";

                                sRow6_Gr = "ΦΠΑ";
                                sRow6_En = "VAT";
                                break;
                        }

                        sApo = Convert.ToDateTime(fgList[iRow, "DateFrom"]+"").ToString("dd/MM/yyyy");
                        sEos = Convert.ToDateTime(fgList[iRow, "DateTo"]+"").ToString("dd/MM/yyyy");

                        // --- check Temp folder  -------------
                       sPDF_FullPath = Application.StartupPath + "/Temp";
                        if (!Directory.Exists(sPDF_FullPath)) Directory.CreateDirectory(sPDF_FullPath);

                        sExPostCostTemplate = klsOptions.ExPostCostTemplate;
                        if (Convert.ToInt32(fgList[iRow, "Service_ID"]) == 1)
                            sExPostCostTemplate = Path.GetFileNameWithoutExtension(sExPostCostTemplate) + "_RTO" + Path.GetExtension(sExPostCostTemplate);

                        if (File.Exists(sPDF_FullPath + "/" + sExPostCostTemplate)) File.Delete(sPDF_FullPath + "/" + sExPostCostTemplate);

                        sNewFile = "ExPostCost_" + fgList[iRow, "Code"] + "_" + fgList[iRow, "AA"] + Path.GetExtension(sExPostCostTemplate);
                        if (File.Exists(sPDF_FullPath + "/" + sNewFile)) File.Delete(sPDF_FullPath + "/" + sNewFile);

                        File.Copy(Application.StartupPath + "/Templates/" + sExPostCostTemplate, sPDF_FullPath + "/" + sNewFile);
                        curDoc = WordApp.Documents.Open(sPDF_FullPath + "/" + sNewFile);

                        curDoc.Content.Find.Execute(FindText: "{code}", ReplaceWith: fgList[iRow, "Code"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{portfolio}", ReplaceWith: fgList[iRow, "Portfolio"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{contract_title}", ReplaceWith: fgList[iRow, "ContractTitle"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{invest_services}", ReplaceWith: fgList[iRow, "ServiceTitle"], Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{average_exchanged_fund}", ReplaceWith: Convert.ToDecimal(fgList[iRow, "AverageExchangedFund"]).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{today}", ReplaceWith: DateTime.Now.ToString("d"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{apo}", ReplaceWith: sApo, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{eos}", ReplaceWith: sEos, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{row1_Gr}", ReplaceWith: sRow1_Gr, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{row1_En}", ReplaceWith: sRow1_En, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{amount1}", ReplaceWith: Convert.ToDecimal(fgList[iRow, "NetRTOFees"]).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{perc1}", ReplaceWith: Convert.ToDecimal(fgList[iRow, "RTOFees_Percent"]).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{row2_Gr}", ReplaceWith: sRow2_Gr, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{row2_En}", ReplaceWith: sRow2_En, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{amount2}", ReplaceWith: Convert.ToDecimal(fgList[iRow, "NetManagmetFees"]).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{perc2}", ReplaceWith: Convert.ToDecimal(fgList[iRow, "ManagmetFees_Percent"]).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{row3_Gr}", ReplaceWith: sRow3_Gr, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{row3_En}", ReplaceWith: sRow3_En, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{amount3}", ReplaceWith: Convert.ToDecimal(fgList[iRow, "NetSuccessFees"]).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{perc3}", ReplaceWith: Convert.ToDecimal(fgList[iRow, "SuccessFees_Percent"]).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{row4_Gr}", ReplaceWith: sRow4_Gr, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{row4_En}", ReplaceWith: sRow4_En, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{amount4}", ReplaceWith: Convert.ToDecimal(fgList[iRow, "NetAdminFees"]).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{perc4}", ReplaceWith: Convert.ToDecimal(fgList[iRow, "AdminFees_Percent"]).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{row5_Gr}", ReplaceWith: sRow5_Gr, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{row5_En}", ReplaceWith: sRow5_En, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{amount5}", ReplaceWith: Convert.ToDecimal(fgList[iRow, "NetFXFees"]).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{perc5}", ReplaceWith: Convert.ToDecimal(fgList[iRow, "FXFees_Percent"]).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{row6_Gr}", ReplaceWith: sRow6_Gr, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{row6_En}", ReplaceWith: sRow6_En, Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{amount6}", ReplaceWith: Convert.ToDecimal(fgList[iRow, "VAT"]).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{perc6}", ReplaceWith: Convert.ToDecimal(fgList[iRow, "VAT_Percent"]).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{sum_amount}", ReplaceWith: Convert.ToDecimal(fgList[iRow, "TotalFees"]).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);
                        curDoc.Content.Find.Execute(FindText: "{sum_perc}", ReplaceWith: Convert.ToDecimal(fgList[iRow, "Total_Percent"]).ToString("0.00"), Replace: Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll);

                        sLastFileName = sPDF_FullPath + "/ExPostCost_" + fgList[iRow, "Code"] + "_" + fgList[iRow, "AA"] + ".pdf";
                        curDoc.SaveAs2(sLastFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF);
                        Thread.Sleep(3000);
                        sLastFileName = Global.DMS_UploadFile(sLastFileName, "Customers/" + fgList[iRow, "ContractTitle"] + "/Informing", Path.GetFileName(sLastFileName));


                        fgList[iRow, "FileName"] = Path.GetFileName(sLastFileName);

                        klsExPostCost_Recs = new clsExPostCost_Recs();
                        klsExPostCost_Recs.Record_ID = Convert.ToInt32(fgList[iRow, "ID"]);
                        klsExPostCost_Recs.GetRecord();
                        klsExPostCost_Recs.FileName = fgList[iRow, "FileName"]+"";
                        klsExPostCost_Recs.DateSent = fgList[iRow, "DateSent"]+"";
                        klsExPostCost_Recs.EditRecord();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message, "DB Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                   finally { }
               }
               fgList[iRow, 0] = false;
            }

            WordApp.Documents.Close();
            WordApp.Application.Quit();
            WordApp = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Thread.Sleep(5000);
            //--- delete temporary files .docx and .pdf ----------------------------------------
            foreach (string f in Directory.EnumerateFiles(sPDF_FullPath, "ExPostCost_*.*"))
            {
                File.Delete(f);
            }

            panFinish.Visible = false;
            this.Refresh();
            this.Cursor = Cursors.Default;
        }

        private void mnuShowPDF_Click(object sender, EventArgs e)
        {
            if (fgList[fgList.Row, "FileName"].ToString().Length > 0) {
                try
                {
                    Global.DMS_ShowFile("Customers\\" + fgList[fgList.Row, "ContractTitle"] + "\\Informing", fgList[fgList.Row, "FileName"].ToString());     // is DMS file, so show it into Web mode          
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                finally { }
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
        private void chkList_CheckedChanged(object sender, EventArgs e)
        {
           for (i = 1; i <= fgList.Rows.Count - 1; i++) fgList[i, 0] = chkList.Checked;
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
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
    }
}
