using System;
using System.Data;
using System.Windows.Forms;
using System.Globalization;
using Excel = Microsoft.Office.Interop.Excel;
using Core;
namespace Products
{
    public partial class frmProductsPrices : Form
    {
        DataView dtView;
        int i, j, k, iID, iProduct_ID, iProductCategory_ID, iShareCode_ID, iIndex = 0, iFixedCols = 0, iFixedColumns = 0, iRightsLevel;
        string sTemp = "", sCode, sCurr, sExtra, sDate;
        bool bCheckList;
        DataRow[] foundRows;
        float fltClose;
        string[] sRow;
        DateTime dTemp;


        clsProductsCodes ProductsCodes = new clsProductsCodes();
        clsProductsPrices ProductsPrices = new clsProductsPrices();
        public frmProductsPrices()
        {
            InitializeComponent();
            iProduct_ID = 0;
            iProductCategory_ID = 0;
            iShareCode_ID = 0;
            ucDC.DateFrom = DateTime.Now.AddDays(-1);
            ucDC.DateTo = DateTime.Now.AddDays(-1);
        }

        private void frmProductsPrices_Load(object sender, EventArgs e)
        {
            bCheckList = false;

            //--- Products Search List --------------------------------------
            ucPS.StartInit(700, 400, 208, 20, 1);
            ucPS.TextOfLabelChanged += new EventHandler(ucPS_TextChanged);
            ucPS.Mode = 1;
            ucPS.ListType = 1;
            //ucPS.Filters = "Aktive >= 1 ";
            ucPS.ShowNonAccord = true;                                                          // Show NonAccordable products (oxi katallila) with red Background
            ucPS.ShowCancelled = false;                                                         // Don't show cancelled products

            //--- define cmbCritProducts Types list ---------------------------------
            cmbCritProducts.DataSource = Global.dtProductTypes.Copy();
            cmbCritProducts.DisplayMember = "Title";
            cmbCritProducts.ValueMember = "ID";

            bCheckList = true;
        }
        protected override void OnResize(EventArgs e)
        {
            tabMain.Width = this.Width - 32;
            tabMain.Height = this.Height - 56;

            panCrit1.Width = tabMain.Width - 22;
            btnSearch1.Left = tabMain.Width - 150;
            fgList.Width = tabMain.Width - 22;
            fgList.Height = tabMain.Height - 214;

            panCrit2.Width = tabMain.Width - 22;
            fgList2.Width = tabMain.Width - 22;
            fgList2.Height = tabMain.Height - 158;

            panCrit3.Width = tabMain.Width - 22;
            btnSearch3.Left = tabMain.Width - 150;
            fgList3.Height = tabMain.Height - 166;
        }

        private void cmbProductType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList)
            {
                //-------------- Define Product Categories List ------------------
                dtView = Global.dtProductsCategories.Copy().DefaultView;
                dtView.RowFilter = "Product_ID = " + cmbProductType.SelectedValue + " OR Product_ID = 0";
                cmbProductCategory.DataSource = dtView;
                cmbProductCategory.DisplayMember = "Title";
                cmbProductCategory.ValueMember = "ID";
            }
        }
        private void btnSearch1_Click(object sender, EventArgs e)
        {
            i = 0;
            fgList.Rows.Count = 1;
            fgList.Redraw = false;

            ProductsPrices = new clsProductsPrices();
            ProductsPrices.DateFrom = ucDC.DateFrom;
            ProductsPrices.DateTo = ucDC.DateTo;
            ProductsPrices.ProductType_ID = Global.IsNumeric(cmbProductType.SelectedValue) ? Convert.ToInt32(cmbProductType.SelectedValue) : 0;
            ProductsPrices.ProductCategory_ID = Global.IsNumeric(cmbProductCategory.SelectedValue) ? Convert.ToInt32(cmbProductCategory.SelectedValue) : 0;
            ProductsPrices.Product_ID = 0; // iProduct_ID;
            ProductsPrices.Filter = ucPS.txtShareTitle.Text;
            ProductsPrices.GetList();
            foreach (DataRow dtRow in ProductsPrices.List.Rows)
            {
                i = i + 1;
                fgList.AddItem(i + "\t" + dtRow["Product_Title"] + "\t" + dtRow["ProductCategory_Title"] + "\t" + dtRow["Code"] + "\t" + dtRow["Code2"] + "\t" +
                               dtRow["ISIN"] + "\t" + dtRow["Title"] + "\t" + Convert.ToDateTime(dtRow["DateIns"]).ToString("dd/MM/yyyy") + "\t" + 
                               (Convert.ToSingle(dtRow["Close"]) == -999999 ? "-" : dtRow["Close"] + "") + "\t" +
                               (Convert.ToSingle(dtRow["Last"]) == -999999 ? "-" : dtRow["Last"] + "") + "\t" + dtRow["Curr"] + "\t" + dtRow["ID"] + "\t" +
                               dtRow["ShareCodes_ID"] + "\t" + dtRow["ShareType"]);
                ;
            }
            fgList.Redraw = true;
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
            EXL.Cells[1, 3].Value = "Προβολή Τιμών";
            var loopTo = fgList.Rows.Count - 1;
            for (i = 0; i <= loopTo; i++)
            {
                for (j = 0; j <= 10; j++)
                    EXL.Cells[i + 2, j + 1].Value = fgList[i, j];
            }

            EXL.ScreenUpdating = true;
            EXL.Visible = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
        }

        private void picFilesPath_Click(object sender, EventArgs e)
        {
            txtFilePath.Text = Global.FileChoice(Global.DefaultFolder);
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            switch (Convert.ToInt32(cmbSource.SelectedIndex))
            {
                case 0:                                                         // Bloomberg 1
                    break;
                case 1:                                                         // Bloomberg 2
                    break;
                case 2:                                                         // Indexes
                    if (txtFilePath.Text.Length > 0) InsertIndexesPrices();
                    break;
                case 3:                                                         // Manual
                    if (txtFilePath.Text.Length > 0) InsertManualPrices();
                    break;
            }


            this.Cursor = Cursors.Default;
        }
        private void InsertIndexesPrices()
        {
            var ExApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = ExApp.Workbooks.Open(txtFilePath.Text);
            Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

            //--- insert into fgList2 Row = 0 dates line
            iFixedCols = 1;
            j = 1;
            fgList2.Redraw = false;

            while (true)
            {
                j = j + 1;
                if ((xlRange.Cells[1, j].Value + "").Trim() != "")
                    fgList2[0, j - 1] = xlRange.Cells[1, j].Value;
                else break;
            }

            j = j - 1;       // columns count, last not EMPTY column
            i = 2;
            fgList2.Rows.Count = 1;
            fgList2.Cols.Count = j;

            while (true)
            {
                i = i + 1;
                if ((xlRange.Cells[i, 1].Value + "").Trim() != "")
                {
                    //---- создание строки для грида -------------------------
                    sTemp = Convert.ToDateTime(xlRange.Cells[i, 1].Value).ToString("dd/MM/yyyy");
                    for (k = 2; k <= j; k++)
                        sTemp = sTemp + "\t" + xlRange.Cells[i, k].Value;

                    //---- добавление строки в грид -------------------------
                    fgList2.AddItem(sTemp);
                }
                else break;
            }

            fgList2.Redraw = true;
            fgReport.Redraw = false;
            fgReport.Rows.Count = 1;
            SaveEquitiesPrices(5);                  // 5 - Indexes

            fgReport.Redraw = true;

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (fgReport.Rows.Count > 1) panReport.Visible = true;
        }
        private void InsertManualPrices()
        {
            var ExApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = ExApp.Workbooks.Open(txtFilePath.Text);
            Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

            //--- insert into fgList2 Row = 0 dates line
            iFixedCols = 3;
            j = 1;
            i = 1;

            sDate = Convert.ToDateTime(xlRange.Cells[1, 2].Value + "").ToString("dd/MM/yyyy");

            fgList2.Redraw = false;
            fgList2.Rows.Count = 1;
            fgList2.Cols.Count = 3;
            while (true)
            {
                i = i + 1;
                if ((xlRange.Cells[i, 2].Value + "").Trim() != "")
                    fgList2.AddItem(sDate + "\t" + xlRange.Cells[i, 1].Value + "\t" + xlRange.Cells[i, 2].Value);
                else break;
            }
            
            fgList2.Redraw = true;
            fgReport.Redraw = false;
            fgReport.Rows.Count = 1;
            SaveEquitiesManualPrices(Convert.ToInt32(cmbCritProducts.SelectedValue), sDate);

            fgReport.Redraw = true;

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (fgReport.Rows.Count > 1) panReport.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveEquitiesPrices(5);
        }
        private void SaveEquitiesPrices(int iShareType)
        {
            for (j = 1; j <= fgList2.Cols.Count - 1; j++)
            {

                //--- check if Product with ISIN = fgList2[i, 1] exists -------------
                iShareCode_ID = 0;
                iShareType = 0;
                sCode = "";
                sCurr = "";

                if ((fgList2[0, j] + "").Trim().Length > 0)
                {
                    ProductsCodes = new clsProductsCodes();
                    ProductsCodes.Code = fgList2[0, j] + "";
                    ProductsCodes.GetRecord_Code();
                    if (ProductsCodes.Aktive == 1)
                    {
                        iShareCode_ID = ProductsCodes.Record_ID;
                        iShareType = ProductsCodes.Product_ID;
                        sCode = ProductsCodes.Code + "";
                        sCurr = ProductsCodes.Curr + "";
                    }

                    if (iShareCode_ID != 0)
                    {
                        for (i = 1; i <= fgList2.Rows.Count - 1; i++)
                        {
                            dTemp = Convert.ToDateTime(fgList2[i, 0]);

                            //--- check if exists price of iShareCode_ID for dTemp - iID <> 0 ---------------
                            iID = 0;
                            ProductsPrices = new clsProductsPrices();
                            ProductsPrices.DateFrom = dTemp;
                            ProductsPrices.DateTo = dTemp;
                            ProductsPrices.ProductType_ID = 0;
                            ProductsPrices.ProductCategory_ID = 0;
                            ProductsPrices.Product_ID = iShareCode_ID;
                            ProductsPrices.Filter = "";
                            ProductsPrices.GetList();
                            foreach (DataRow dtRow in ProductsPrices.List.Rows)
                            {
                                iID = Convert.ToInt32(dtRow["ID"]);
                            }

                            fltClose = 0;
                            if (Global.IsNumeric(fgList2[i, j]))
                            {
                                fltClose = Convert.ToSingle(((fgList2[i, j] + "").Replace(".", ",")));
                                if (String.Equals(sCurr, "GBp", StringComparison.OrdinalIgnoreCase)) fltClose = fltClose / 100;
                            }
                            else fltClose = -999999;

                            if (iID == 0)
                            {
                                ProductsPrices = new clsProductsPrices();
                                ProductsPrices.ShareType = iShareType;
                                ProductsPrices.Code = sCode;
                                ProductsPrices.ShareCodes_ID = iShareCode_ID;
                                ProductsPrices.DateIns = dTemp;
                                ProductsPrices.Open = 0;
                                ProductsPrices.High = 0;
                                ProductsPrices.Open = 0;
                                ProductsPrices.Low = 0;
                                ProductsPrices.Close = fltClose;
                                ProductsPrices.Last = -999999;
                                ProductsPrices.Volume = 0;
                                ProductsPrices.InsertRecord();
                            }
                            else
                            {
                                ProductsPrices = new clsProductsPrices();
                                ProductsPrices.Record_ID = iID;
                                ProductsPrices.GetRecord();
                                ProductsPrices.ShareType = iShareType;
                                ProductsPrices.Code = sCode;
                                ProductsPrices.ShareCodes_ID = iShareCode_ID;
                                ProductsPrices.DateIns = dTemp;
                                ProductsPrices.Open = 0;
                                ProductsPrices.High = 0;
                                ProductsPrices.Open = 0;
                                ProductsPrices.Low = 0;
                                ProductsPrices.Close = fltClose;
                                ProductsPrices.Last = -999999;
                                ProductsPrices.Volume = 0;
                                ProductsPrices.EditRecord();
                            }
                        }
                    }
                }
            }
        }
        private void SaveEquitiesManualPrices(int iShareType, string sDate)
        {
            dTemp = Convert.ToDateTime(sDate);

            clsProductsPrices DayPrices = new clsProductsPrices();
            DayPrices.DateFrom = dTemp;
            DayPrices.DateTo = dTemp;
            DayPrices.ProductType_ID = 0;
            DayPrices.ProductCategory_ID = 0;
            DayPrices.Product_ID = 0;
            DayPrices.Filter = "";
            DayPrices.GetList();

            for (i = 1; i <= fgList2.Rows.Count - 1; i++)
            {
                //--- check if Product with ISIN = fgList2[i, 1] exists -------------
                iShareCode_ID = 0;
                iShareType = 0;
                sCode = (fgList2[i, 1] + "").Trim();
                sCurr = "";

                if (sCode.Length > 0)
                {
                    ProductsCodes = new clsProductsCodes();
                    ProductsCodes.Code = sCode + "";
                    ProductsCodes.GetRecord_Code();
                    if (ProductsCodes.Aktive == 1)
                    {
                        iShareCode_ID = ProductsCodes.Record_ID;
                        iShareType = ProductsCodes.Product_ID;
                        sCode = ProductsCodes.Code + "";
                        sCurr = ProductsCodes.Curr + "";

                        if (iShareCode_ID != 0)
                        {
                            //--- check if exists price of iShareCode_ID for dTemp - iID <> 0 ---------------
                            iID = 0;                                                                                // iID = SharePrices.ID
                            foundRows = DayPrices.List.Select("ShareCodes_ID = " + iShareCode_ID);
                            if (foundRows.Length > 0) iID = Convert.ToInt32(foundRows[0]["ID"]);

                            fltClose = 0;
                            if (Global.IsNumeric(fgList2[i, 2]))
                            {
                                fltClose = Convert.ToSingle(((fgList2[i, 2] + "").Replace(".", ",")));
                                if (String.Equals(sCurr, "GBp", StringComparison.OrdinalIgnoreCase)) fltClose = fltClose / 100;
                            }
                            else fltClose = -999999;

                            if (iID == 0)
                            {
                                ProductsPrices = new clsProductsPrices();
                                ProductsPrices.ShareType = iShareType;
                                ProductsPrices.Code = sCode;
                                ProductsPrices.ShareCodes_ID = iShareCode_ID;
                                ProductsPrices.DateIns = dTemp;
                                ProductsPrices.Open = 0;
                                ProductsPrices.High = 0;
                                ProductsPrices.Open = 0;
                                ProductsPrices.Low = 0;
                                ProductsPrices.Close = fltClose;
                                ProductsPrices.Last = -999999;
                                ProductsPrices.Volume = 0;
                                ProductsPrices.InsertRecord();
                            }
                            else
                            {
                                ProductsPrices = new clsProductsPrices();
                                ProductsPrices.Record_ID = iID;
                                ProductsPrices.GetRecord();
                                ProductsPrices.ShareType = iShareType;
                                ProductsPrices.Code = sCode;
                                ProductsPrices.ShareCodes_ID = iShareCode_ID;
                                ProductsPrices.DateIns = dTemp;
                                ProductsPrices.Open = 0;
                                ProductsPrices.High = 0;
                                ProductsPrices.Open = 0;
                                ProductsPrices.Low = 0;
                                ProductsPrices.Close = fltClose;
                                ProductsPrices.Last = -999999;
                                ProductsPrices.Volume = 0;
                                ProductsPrices.EditRecord();
                            }

                        }
                    }  
                }
            }
        }
        private void tsbEffect_Click(object sender, EventArgs e)
        {

        }

        private void tsbExport_Click(object sender, EventArgs e)
        {

        }
        protected void ucPS_TextChanged(object sender, EventArgs e)
        {
            if (ucPS.Mode == 1)
            {
                Global.ProductData stProduct = new Global.ProductData();
                stProduct = ucPS.SelectedProductData;
                iProduct_ID = stProduct.Product_ID;
                iProductCategory_ID = stProduct.ProductCategory_ID;
                iShareCode_ID = stProduct.ShareCode_ID;
                lblCode.Text = stProduct.Code;
                lblCode2.Text = stProduct.Code2;
                lblISIN.Text = stProduct.ISIN;
                lblTitle.Text = stProduct.Title;
            }
        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }

    }
}
