using System;
using System.Data;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;
using C1.Win.C1FlexGrid;
using Excel = Microsoft.Office.Interop.Excel;
using Core;

namespace Accounting
{
    public partial class frmInvoicesControl : Form
    {
        DataTable dtInvoices, dtList;
        DataRow dtRow;
        DataView dtView;
        DataRow[] foundRows;
        int i, j, iRightsLevel;
        string sTemp, sExtra;
        string[] sDescription = { "", "Αμοιβή Λήψης & Διαβίβασης Εντολής", "Αμοιβή Διαβίβασης Εντολής Μετατροπής Νομίσματος", "Αμοιβή Επενδυτικών Συμβουλών", 
                                  "Αμοιβή Υποστήριξης Χαρτοφυλακίου", "Αμοιβή Υπεραπόδοσης", "Αμοιβή θεματοφυλακής" };
        bool bCheckList;
        Hashtable imgMap = new Hashtable();
        clsInvoiceTitles InvoiceTitles = new clsInvoiceTitles();
        public frmInvoicesControl()
        {
            InitializeComponent();
            ucDC.DateFrom = DateTime.Now;
            ucDC.DateTo = DateTime.Now;
            ucDC2.DateFrom = DateTime.Now;
            ucDC2.DateTo = DateTime.Now;
        }

        private void frmInvoicesControl_Load(object sender, EventArgs e)
        {
            bCheckList = false;

            for (i = 0; i < imgFile.Images.Count; i++) imgMap.Add(i, imgFile.Images[i]);

            //-------------- Define ServiceProviders List ------------------
            dtView = Global.dtServiceProviders.Copy().DefaultView;
            dtView.RowFilter = "ProviderType = 0 OR ProviderType = 1 OR ProviderType = 2";
            cmbServiceProviders.DataSource = dtView;
            cmbServiceProviders.DisplayMember = "Title";
            cmbServiceProviders.ValueMember = "ID";
            cmbServiceProviders.SelectedValue = 0;

            cmbTypes.SelectedIndex = 0;

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgList.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgList_BeforeEdit);
            fgList.DoubleClick += new System.EventHandler(fgList_DoubleClick);

            Column clm1 = fgList.Cols["image_map"];
            clm1.ImageMap = imgMap;
            clm1.ImageAndText = false;
            clm1.ImageAlign = ImageAlignEnum.CenterCenter;

            DefineList();            

            bCheckList = true;
        }
        protected override void OnResize(EventArgs e)
        {
            panCritiries.Width = this.Width - 30;
            btnSearch.Left = panCritiries.Width - 120;

            fgList.Height = this.Height - 164;
            fgList.Width = this.Width - 30;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DefineList();
        }
        private void DefineList()
        {
            dtList = new DataTable();
            dtList.Columns.Add("Client_ID", typeof(int));
            dtList.Columns.Add("ClientName", typeof(string));
            dtList.Columns.Add("AFM", typeof(string));
            dtList.Columns.Add("DOY", typeof(string));
            dtList.Columns.Add("Address", typeof(string));
            dtList.Columns.Add("City", typeof(string));
            dtList.Columns.Add("Zip", typeof(string));
            dtList.Columns.Add("Country_Title", typeof(string));

            InvoiceTitles = new clsInvoiceTitles();
            if (chkDateIns.Checked)
            {
                InvoiceTitles.DateFrom = ucDC.DateFrom;
                InvoiceTitles.DateTo = ucDC.DateTo;
            }
            else
            {
                InvoiceTitles.DateFrom = Convert.ToDateTime("2000/01/01");
                InvoiceTitles.DateTo = Convert.ToDateTime("2070/12/31");
            }
            if (chkDateIssued.Checked)
            {
                InvoiceTitles.DateIssuedFrom = ucDC2.DateFrom;
                InvoiceTitles.DateIssuedTo = ucDC2.DateTo;
            }
            else
            {
                InvoiceTitles.DateIssuedFrom = Convert.ToDateTime("2000/01/01");
                InvoiceTitles.DateIssuedTo = Convert.ToDateTime("2070/12/31");
            }

            InvoiceTitles.GetList();
            dtInvoices = InvoiceTitles.List;

            ShowList();
        }

        private void cmbServiceProviders_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList) ShowList();
        }

        private void cmbTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bCheckList) ShowList();
        }
        private void ShowList()
        {
            fgList.Redraw = false;
            fgList.Rows.Count = 1;
            i = 0;
            foreach (DataRow dtRow1 in dtInvoices.Rows)
            {
                if ((Convert.ToInt32(cmbServiceProviders.SelectedValue) == 0 || Convert.ToInt32(cmbServiceProviders.SelectedValue) == Convert.ToInt32(dtRow1["ServiceProvider_ID"]))  &&
                    (Convert.ToInt32(cmbTypes.SelectedIndex) == 0 || Convert.ToInt32(cmbTypes.SelectedIndex) == Convert.ToInt32(dtRow1["SourceType"])))
                {
                    i = i + 1;

                    sTemp = sDescription[Convert.ToInt32(dtRow1["SourceType"])];
                    if (Convert.ToInt32(dtRow1["SourceType"]) == 3)
                        if (Convert.ToInt32(dtRow1["Service_ID"]) == 3) sTemp = "Αμοιβή Διαχείρισης";

                    fgList.AddItem(false + "\t" + dtRow1["ImageType"] + "\t" + i + "\t" + dtRow1["InvoiceNum"] + "\t" + dtRow1["DateIssued"] + "\t" + sTemp + "\t" + 
                                   dtRow1["AFM"] + "\t" + dtRow1["DOY"] + "\t" + dtRow1["ClientName"] + "\t" + dtRow1["Address"] + "\t" + dtRow1["City"] + "\t" + 
                                   dtRow1["Zip"] + "\t" + dtRow1["Country_Title"] + "\t" + dtRow1["AxiaKathari"] + "\t" + dtRow1["AxiaFPA"] + "\t" + dtRow1["AxiaTeliki"] + "\t" +
                                   dtRow1["Code"] + "\t" + dtRow1["Portfolio"] + "\t" + dtRow1["ServiceProvider_Title"] + "\t" + dtRow1["FileName"] + "\t" +
                                   dtRow1["ID"] + "\t" + dtRow1["ClientTipos"] + "\t" + dtRow1["Client_ID"] + "\t" + dtRow1["ContractTipos"] + "\t" + dtRow1["SourceType"] + "\t" +
                                   dtRow1["Service_ID"] + "\t" + dtRow1["ServiceProvider_ID"] + "\t" + dtRow1["SubPath"]);

                    //--- add record into dtList table ---------------------------
                    foundRows = dtList.Select("Client_ID=" + dtRow1["Client_ID"]);
                    if (foundRows.Length == 0)
                    {
                        dtRow = dtList.NewRow();
                        dtRow["Client_ID"] = dtRow1["Client_ID"];
                        dtRow["ClientName"] = dtRow1["ClientName"];
                        dtRow["AFM"] = dtRow1["AFM"];
                        dtRow["DOY"] = dtRow1["DOY"];
                        dtRow["Address"] = dtRow1["Address"];
                        dtRow["City"] = dtRow1["City"];
                        dtRow["Zip"] = dtRow1["Zip"];
                        dtRow["Country_Title"] = dtRow1["Country_Title"];
                        dtList.Rows.Add(dtRow);
                    }
                }
            }          
            fgList.Redraw = true;
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
            if (fgList.Row >= 1)
                if (fgList.Col == 1) ShowInvoice();
        }
        private void ShowInvoice()
        {
            if (fgList[fgList.Row, "FileName"].ToString().Length > 0)
            {
                try
                {
                    Global.DMS_ShowFile("Customers\\" + fgList[fgList.Row, "SubPath"] + "\\Invoices", fgList[fgList.Row, "FileName"].ToString());     // is DMS file, so show it into Web mode          
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
                finally { }
            }
        }
        private void tsbExport_Click(object sender, EventArgs e)
        {
            int i, j, k, m, n;
            string s = "";
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
            EXL.Cells[1, 1].Value = "A/A";
            EXL.Cells[1, 2].Value = "ΑΡΙΘΜΟΣ ΠΑΡΑΣΤΑΤΙΚΟΥ";
            EXL.Cells[1, 3].Value = "ΗΜΕΡΟΜΗΝΙΑ ΠΑΡΑΣΤΑΤΙΚΟΥ";
            EXL.Cells[1, 4].Value = "ΑΙΤΙΟΛΟΓΙΑ";
            EXL.Cells[1, 5].Value = "ΑΦΜ";
            EXL.Cells[1, 6].Value = "ΔΟΥ";
            EXL.Cells[1, 7].Value = "1ος ΔΙΚΑΙΟΥΧΟΣ";
            EXL.Cells[1, 8].Value = "ΔΙΕΘΥΝΣΗ";
            EXL.Cells[1, 9].Value = "ΠΟΛΗ";
            EXL.Cells[1, 10].Value = "ΤΚ";
            EXL.Cells[1, 11].Value = "ΧΩΡΑ";
            EXL.Cells[1, 12].Value = " ΑΞΙΑ";
            EXL.Cells[1, 13].Value = "χρ/πιστ";
            EXL.Cells[1, 14].Value = "Κίνηση χονδρική λιανική";
            EXL.Cells[1, 15].Value = "ΚΑ/ΦΠΑ/Σύνολο";
            EXL.Cells[1, 16].Value = "Λογαριασμός";
            EXL.Cells[1, 17].Value = "Client ID";

            m = 1;
            n = 0;
            for (i = 1; i <= fgList.Rows.Count - 1; i++)
            {
                if (Convert.ToInt32(fgList[i, "ClientTipos"]) == 1) {  
                    n = n + 1;

                    k = 1;
                    m = m + 1;
                    EXL.Cells[m, k].Value = n;
                    for (j = 3; j <= 15; j++)
                    {
                        k = k + 1;
                        if (j == 15) EXL.Cells[m, 12].Value = Convert.ToDecimal(fgList[i, j]).ToString("0.00");
                        else if (j == 13) s = "";
                        else if (j == 14) s = "";
                        else if (j == 4) EXL.Cells[m, 3].Value = Convert.ToDateTime(fgList[i, j]).ToString("dd/MM/yyyy");
                        else if (j == 6) EXL.Cells[m, 5].Value = fgList[i, j].ToString();
                        else EXL.Cells[m, k].Value = fgList[i, j];
                    }
                    EXL.Cells[m, 13].Value = "0";
                    EXL.Cells[m, 14].Value = "ΛΙΑΝΙΚΗ";
                    EXL.Cells[m, 15].Value = "ΣΥΝΟΛΟ";
                    EXL.Cells[m, 16].Value = "1";
                    EXL.Cells[m, 17].Value = fgList[i, "Client_ID"];

                    k = 1;
                    m = m + 1;
                    EXL.Cells[m, k].Value = n;                    
                    for (j = 3; j <= 15; j++)
                    {
                        k = k + 1;
                        if (j == 13) EXL.Cells[m, 12].Value = Convert.ToDecimal(fgList[i, j]).ToString("0.00");
                        else if (j == 14) s = "";
                        else if (j == 15) s = "";
                        else if (j == 4) EXL.Cells[m, 3].Value = Convert.ToDateTime(fgList[i, j]).ToString("dd/MM/yyyy");
                        else if (j == 6) EXL.Cells[m, 5].Value = fgList[i, j].ToString();
                        else EXL.Cells[m, k].Value = fgList[i, j];
                    }
                    EXL.Cells[m, 13].Value = "1";
                    EXL.Cells[m, 14].Value = "ΛΙΑΝΙΚΗ";
                    EXL.Cells[m, 15].Value = "ΚΑΘΑΡΗ ΑΞΙΑ";
                    EXL.Cells[m, 16].Value = "2";
                    EXL.Cells[m, 17].Value = fgList[i, "Client_ID"];

                    k = 1;
                    m = m + 1;
                    EXL.Cells[m, k].Value = n;
                    for (j = 3; j <= 15; j++)
                    {
                        k = k + 1;
                        if (j == 14) EXL.Cells[m, 12].Value = Convert.ToDecimal(fgList[i, j]).ToString("0.00");
                        else if (j == 13) s = "";
                        else if (j == 15) s = "";
                        else if (j == 4) EXL.Cells[m, 3].Value = Convert.ToDateTime(fgList[i, j]).ToString("dd/MM/yyyy");
                        else if (j == 6) EXL.Cells[m, 5].Value = fgList[i, j].ToString();
                        else EXL.Cells[m, k].Value = fgList[i, j];
                    }
                    EXL.Cells[m, 13].Value = "1";
                    EXL.Cells[m, 14].Value = "ΛΙΑΝΙΚΗ";
                    EXL.Cells[m, 15].Value = "ΦΠΑ";
                    EXL.Cells[m, 16].Value = "3";
                    EXL.Cells[m, 17].Value = fgList[i, "Client_ID"];

                }
            }

            //--- Sheet 2 ---------------------------------------------------------------------
            Excel.Worksheet ws2 = WB.Worksheets.Add(System.Reflection.Missing.Value, WB.Worksheets[WB.Worksheets.Count], System.Reflection.Missing.Value, System.Reflection.Missing.Value);

            ws2.Cells[1, 1].Value = "A/A";
            ws2.Cells[1, 2].Value = "ΑΡΙΘΜΟΣ ΠΑΡΑΣΤΑΤΙΚΟΥ";
            ws2.Cells[1, 3].Value = "ΗΜΕΡΟΜΗΝΙΑ ΠΑΡΑΣΤΑΤΙΚΟΥ";
            ws2.Cells[1, 4].Value = "ΑΙΤΙΟΛΟΓΙΑ";
            ws2.Cells[1, 5].Value = "ΑΦΜ";
            ws2.Cells[1, 6].Value = "ΔΟΥ";
            ws2.Cells[1, 7].Value = "1ος ΔΙΚΑΙΟΥΧΟΣ";
            ws2.Cells[1, 8].Value = "ΔΙΕΘΥΝΣΗ";
            ws2.Cells[1, 9].Value = "ΠΟΛΗ";
            ws2.Cells[1, 10].Value = "ΤΚ";
            ws2.Cells[1, 11].Value = "ΧΩΡΑ";
            ws2.Cells[1, 12].Value = " ΑΞΙΑ";
            ws2.Cells[1, 13].Value = "χρ/πιστ";
            ws2.Cells[1, 14].Value = "Κίνηση χονδρική λιανική";
            ws2.Cells[1, 15].Value = "ΚΑ/ΦΠΑ/Σύνολο";
            ws2.Cells[1, 16].Value = "Λογαριασμός";
            ws2.Cells[1, 17].Value = "Client ID";

            m = 1;
            n = 0;
            for (i = 1; i <= fgList.Rows.Count - 1; i++)
            {
                if (Convert.ToInt32(fgList[i, "ClientTipos"]) == 2)
                {
                    n = n + 1;

                    k = 1;
                    m = m + 1;
                    ws2.Cells[m, k].Value = n;
                    for (j = 3; j <= 15; j++)
                    {
                        k = k + 1;
                        if (j == 15) ws2.Cells[m, 12].Value = Convert.ToDecimal(fgList[i, j]).ToString("0.00");
                        else if (j == 13) s = "";
                        else if (j == 14) s = "";
                        else if (j == 4) ws2.Cells[m, 3].Value = Convert.ToDateTime(fgList[i, j]).ToString("dd/MM/yyyy");
                        else if (j == 6) ws2.Cells[m, 5].Value = fgList[i, j].ToString();
                        else ws2.Cells[m, k].Value = fgList[i, j];
                    }
                    ws2.Cells[m, 13].Value = "0";
                    ws2.Cells[m, 14].Value = "ΧΟΝΔΡΙΚΗ";
                    ws2.Cells[m, 15].Value = "ΣΥΝΟΛΟ";
                    ws2.Cells[m, 16].Value = "1";
                    ws2.Cells[m, 17].Value = fgList[i, "Client_ID"];

                    k = 1;
                    m = m + 1;
                    ws2.Cells[m, k].Value = n;
                    for (j = 3; j <= 15; j++)
                    {
                        k = k + 1;
                        if (j == 13) ws2.Cells[m, 12].Value = Convert.ToDecimal(fgList[i, j]).ToString("0.00");
                        else if (j == 14) s = "";
                        else if (j == 15) s = "";
                        else if (j == 4) ws2.Cells[m, 3].Value = Convert.ToDateTime(fgList[i, j]).ToString("dd/MM/yyyy");
                        else if (j == 6) ws2.Cells[m, 5].Value = fgList[i, j].ToString();
                        else ws2.Cells[m, k].Value = fgList[i, j];
                    }
                    ws2.Cells[m, 13].Value = "1";
                    ws2.Cells[m, 14].Value = "ΧΟΝΔΡΙΚΗ";
                    ws2.Cells[m, 15].Value = "ΚΑΘΑΡΗ ΑΞΙΑ";
                    ws2.Cells[m, 16].Value = "2";
                    ws2.Cells[m, 17].Value = fgList[i, "Client_ID"];

                    k = 1;
                    m = m + 1;
                    ws2.Cells[m, k].Value = n;
                    for (j = 3; j <= 15; j++)
                    {
                        k = k + 1;
                        if (j == 14) ws2.Cells[m, 12].Value = Convert.ToDecimal(fgList[i, j]).ToString("0.00");
                        else if (j == 13) s = "";
                        else if (j == 15) s = "";
                        else if (j == 4) ws2.Cells[m, 3].Value = Convert.ToDateTime(fgList[i, j]).ToString("dd/MM/yyyy");
                        else if (j == 6) ws2.Cells[m, 5].Value = fgList[i, j].ToString();
                        else ws2.Cells[m, k].Value = fgList[i, j];
                    }
                    ws2.Cells[m, 13].Value = "1";
                    ws2.Cells[m, 14].Value = "ΧΟΝΔΡΙΚΗ";
                    ws2.Cells[m, 15].Value = "ΦΠΑ";
                    ws2.Cells[m, 16].Value = "3";
                    ws2.Cells[m, 17].Value = fgList[i, "Client_ID"];
                }
            }

            //--- Sheet 3 ---------------------------------------------------------------------
            Excel.Worksheet ws3 = WB.Worksheets.Add(System.Reflection.Missing.Value, WB.Worksheets[WB.Worksheets.Count], System.Reflection.Missing.Value, System.Reflection.Missing.Value);

            ws3.Cells[1, 1].Value = "ΑΦΜ";
            ws3.Cells[1, 2].Value = "ΔΟΥ";
            ws3.Cells[1, 3].Value = "1ος ΔΙΚΑΙΟΥΧΟΣ";
            ws3.Cells[1, 4].Value = "ΔΙΕΘΥΝΣΗ";
            ws3.Cells[1, 5].Value = "ΠΟΛΗ";
            ws3.Cells[1, 6].Value = "ΤΚ";
            ws3.Cells[1, 7].Value = "ΧΩΡΑ";
            ws3.Cells[1, 8].Value = "Client ID";

            m = 1;
            foreach (DataRow dtRow1 in dtList.Rows)
            {
                m = m + 1;
                ws3.Cells[m, 1].Value = dtRow1["AFM"];
                ws3.Cells[m, 2].Value = dtRow1["DOY"];
                ws3.Cells[m, 3].Value = dtRow1["ClientName"];
                ws3.Cells[m, 4].Value = dtRow1["Address"];
                ws3.Cells[m, 5].Value = dtRow1["City"];
                ws3.Cells[m, 6].Value = dtRow1["Zip"];
                ws3.Cells[m, 7].Value = dtRow1["Country_Title"];
                ws3.Cells[m, 8].Value = dtRow1["Client_ID"];
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
        private void tsbExcel_Click(object sender, EventArgs e)
        {
            var EXL = new Microsoft.Office.Interop.Excel.Application();
            var oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            // System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("el-GR");
            EXL.Workbooks.Add();
            EXL.ScreenUpdating = false;
            EXL.Caption = Global.AppTitle;
            EXL.Cells[1, 3].Value = "Έλεγχος Παραστατικών";
            var loopTo = fgList.Rows.Count - 1;
            for (this.i = 0; this.i <= loopTo; this.i++)
            {
                for (this.j = 2; this.j <= 19; this.j++)
                        EXL.Cells[i + 3, j].Value = fgList[i, j];
            }

            EXL.ScreenUpdating = true;
            EXL.Visible = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
        }

        private void tsbHelp_Click(object sender, EventArgs e)
        {

        }

        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } } 
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
