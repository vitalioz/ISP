using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;

namespace Core
{
    public partial class ucProductsSearch : UserControl
    {
        int i, iShowWidth, iShowHeight, iMaxWidth, iMaxHeight, iMode, iShare_ID, iShareCode_ID, iOldClient_ID, iOldShareCode_ID, iListType;
        string sTemp, sFilters, sCodesList;
        bool bShowProductsList, bShowNonAccord, bShowCancelled, bBlockNonRecommended;
        Global.ProductData ShareProductData = new Global.ProductData();
        DataTable dtProductsContract;
        DataRow[] foundRows;
        CellStyle csCancel, csNonAccord;

        public event EventHandler TextOfLabelChanged;
        public ucProductsSearch()
        {
            InitializeComponent();

            bShowProductsList = true;
            bShowNonAccord = true;
            bShowCancelled = true;
        }

        private void ucProductsSearch_Load(object sender, EventArgs e)
        {
            iOldClient_ID = 0;
            iOldShareCode_ID = 0;
            
            sTemp = "";

            //------- fgList ----------------------------
            fgList.HighLight = C1.Win.C1FlexGrid.HighLightEnum.WithFocus;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:Gold; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:Gold; ForeColor:Black;}");
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.DoubleClick += new System.EventHandler(fgList_DoubleClick);
            fgList.MouseDown += new MouseEventHandler(fgList_MouseDown);
            fgList.BeforeEdit += new C1.Win.C1FlexGrid.RowColEventHandler(fgList_BeforeEdit);

            csCancel = fgList.Styles.Add("Cancelled");
            csCancel.ForeColor = Color.Red;

            csNonAccord = fgList.Styles.Add("NonAccord");
            csNonAccord.BackColor = Color.Salmon;
        }
        public void StartInit(int iWidth, int iHeight, int iTxtWidth, int iTxtHeight, int iShownListType)
        {
            bShowProductsList = false;
            txtShareTitle.Text = "";
            bShowProductsList = true;

            iMaxWidth = iWidth;
            iMaxHeight = iHeight;
            iShowWidth = iTxtWidth;
            iShowHeight = iTxtHeight;

            ShareCode_ID.Text = "-999";

            if (iShowWidth != 0) txtShareTitle.Width = iShowWidth;
            if (iShowHeight != 0) txtShareTitle.Height = iShowHeight;

            this.Width = txtShareTitle.Width;
            this.Height = txtShareTitle.Height;
        }
        protected override void OnResize(EventArgs e)
        {
            panList.Width = this.Width - 1;
            panList.Height = this.Height - 22;

            if (iMode == 2) fgList.Height = this.Height - 90;
            else fgList.Height = this.Height - 60;

            fgList.Width = this.Width - 15;

            picClose.Left = this.Width - 26;
        }
        private void txtShareTitle_TextChanged(object sender, EventArgs e)
        {
            if (bShowProductsList)
            {
                if (iMode == 2) {                                       // 2 - multiple products selection mode
                    chkSelect.Visible = true;
                    fgList.Cols[0].Visible = true;
                    fgList.Height = this.Height - 90;
                    fgList.AllowEditing = true;
                    btnChoice.Visible = true;
                }
                else {
                    chkSelect.Visible = false;
                    fgList.Cols[0].Visible = false;
                    fgList.Height = this.Height - 60;
                    fgList.AllowEditing = false;
                    btnChoice.Visible = false;
                }

                this.Width = iMaxWidth;
                this.Height = iMaxHeight;

                DataFiltering();
            }
        }
        private void DataFiltering()
        {            
            ShareCode_ID.Text = "-888";
            lblFoundRecords.Text = "";
            fgList.Redraw = false;
            fgList.Rows.Count = 1;

            sTemp = txtShareTitle.Text.Trim();

            iOldClient_ID = -999;
            iOldShareCode_ID = -999;

            switch (iListType)
            {
                case 1:                                                             // iListType = 1 : Global.dtProducts - common list of products
                    sTemp = sFilters + " AND (Title LIKE '%" + sTemp + "%' OR Code LIKE '%" + sTemp + "%' OR Code2 LIKE '%" + sTemp +
                                       "%' OR ISIN LIKE '%" + sTemp + "%' OR Code_ISIN LIKE '%" + sTemp + "%' OR SecID LIKE '%" + sTemp + "%')";
                    foundRows = Global.dtProducts.Select(sTemp, "Title");
                    if (foundRows.Length > 0) { 

                        foreach (DataRow dtRow in foundRows) {
                            iShare_ID = Convert.ToInt32(dtRow["Shares_ID"]);
                            iShareCode_ID = Convert.ToInt32(dtRow["ID"]);

                            if ((iOldClient_ID != iShare_ID) || (iOldShareCode_ID != iShareCode_ID)) {
                                iOldClient_ID = iShare_ID;
                                iOldShareCode_ID = iShareCode_ID; 

                                fgList.AddItem(false + "\t" + dtRow["Title"] + "\t" + dtRow["Code"] + "\t" + dtRow["Code2"] + "\t" + dtRow["ISIN"] + "\t" +
                                               dtRow["Product"] + "\t" + dtRow["ProductCategory"] + "\t" + dtRow["StockExchange_Code"] + "\t" +
                                               dtRow["Currency"] + "\t" + dtRow["ID"] + "\t" + dtRow["Shares_ID"] + "\t" + dtRow["Product_ID"] + "\t" +
                                               dtRow["ProductCategory_ID"] + "\t" + dtRow["StockExchange_ID"] + "\t" + dtRow["Weight"] + "\t" +
                                               dtRow["LastClosePrice"] + "\t" + dtRow["URL_ID"] + "\t" + "1" + "\t" + dtRow["MIFID_Risk"] + "\t" + 
                                               dtRow["Aktive"] + "\t" + dtRow["HFIC_Recom"]);          // 1 - is pseudo Accordance flag because it's list of all products
                            }
                        }
                    }
                    break;
                case 2:                                                           // iListType = 2 : dtProductsContract - list of products for current contract
                    sTemp = sFilters + " AND (CodeTitle LIKE '%" + sTemp + "%' OR Code LIKE '%" + sTemp + "%' OR Code2 LIKE '%" + sTemp +
                                       "%' OR ISIN LIKE '%" + sTemp + "%')";
                    foundRows = dtProductsContract.Select(sTemp, "CodeTitle");
                    if (foundRows.Length > 0) {

                        foreach (DataRow dtRow in foundRows) {
                            iShare_ID = Convert.ToInt32(dtRow["Shares_ID"]);
                            iShareCode_ID = Convert.ToInt32(dtRow["ID"]);

                            if ((iOldClient_ID != iShare_ID) || (iOldShareCode_ID != iShareCode_ID))  {
                                iOldClient_ID = iShare_ID;
                                iOldShareCode_ID = iShareCode_ID;

                                fgList.AddItem(false + "\t" + dtRow["CodeTitle"] + "\t" + dtRow["Code"] + "\t" + dtRow["Code2"] + "\t" + dtRow["ISIN"] + "\t" +
                                               dtRow["Product_Title"] + "\t" + dtRow["ProductCategory_Title"] + "\t" + dtRow["StockExchange_Code"] + "\t" +
                                               dtRow["Currency"] + "\t" + dtRow["ID"] + "\t" + dtRow["Shares_ID"] + "\t" + dtRow["Product_ID"] + "\t" +
                                               dtRow["ProductCategory_ID"] + "\t" + dtRow["StockExchange_ID"] + "\t" + dtRow["Weight"] + "\t" +
                                               dtRow["LastClosePrice"] + "\t" + dtRow["IR_URL"] + "\t" + dtRow["OK_Flag"] + "\t" + dtRow["OK_String"] + "\t" + 
                                               dtRow["Aktive"] + "\t" + dtRow["HFIC_Recom"]);
                            }
                        }
                    }
                    break;
            }

            //fgList.Sort(SortFlags.Ascending, 1);
            fgList.Redraw = true;

            lblFoundRecords.Text = "Records: " + (fgList.Rows.Count - 1);
        }
        private void fgList_CellChanged(object sender, RowColEventArgs e)
        {
            if (bShowNonAccord) {
                if (e.Col == 17)
                    if (Convert.ToInt32(fgList[e.Row, "OK_Flag"]) == 0) fgList.Rows[e.Row].Style = csNonAccord;             // 17 - OK_Flag
                if (e.Col == 20)
                    if (Convert.ToInt32(fgList[e.Row, "HFIC_Recom"]) == 0) fgList.Rows[e.Row].Style = csNonAccord;          // 20 - HFIC_Recom
            }

            if (bShowCancelled && e.Col == 19)                                                                              // 19 - Aktive (ShareCode Status)
                if (Convert.ToInt32(fgList[e.Row, "Aktive"]) == 0) fgList.Rows[e.Row].Style = csCancel;
        }
        private void fgList_DoubleClick(object sender, EventArgs e)
        {
            if (iMode != 2) ProductChoice();
        }
        private void fgList_BeforeEdit(object sender, RowColEventArgs e)
        {
            if (iMode == 2) {
                if (e.Col == 0) e.Cancel = false;
                else e.Cancel = true;
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
        private void mnuProductData_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 0) {
                frmProductData locProductData = new frmProductData();
                locProductData.Product_ID = Convert.ToInt32(fgList[fgList.Row, "Product_ID"]);
                locProductData.ShareCode_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                locProductData.Text = Global.GetLabel("product");
                locProductData.Show();
            }
        }

        private void mnuCopyISIN_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 0) Clipboard.SetDataObject(fgList[fgList.Row, "ISIN"], true, 10, 100);
        }
        private void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            for (i = 1; i <= fgList.Rows.Count - 1; i++) fgList[i, 0] = chkSelect.Checked;
        }

        private void ProductChoice()
        {
            //if (txtShareTitle.Text.Length > 0) DefineProductData();
            //else ShareCode_ID.Text = fgList[fgList.Row, "ID"].ToString(); // "-999";
            if (bBlockNonRecommended && Convert.ToInt32(fgList[fgList.Row, "HFIC_Recom"]) == 0)
                MessageBox.Show("Το προϊόν δεν είναι επιλεγμένο", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
            {
                DefineProductData();

                this.Width = txtShareTitle.Width;
                this.Height = txtShareTitle.Height;
            }
        }
        private void DefineProductData()
        {
            ShareProductData.Title = fgList[fgList.Row, "Title"].ToString();
            ShareProductData.Code = fgList[fgList.Row, "Code"].ToString();
            ShareProductData.Code2 = fgList[fgList.Row, "Code2"].ToString();
            ShareProductData.ISIN = fgList[fgList.Row, "ISIN"].ToString();
            ShareProductData.Product_Title = fgList[fgList.Row, "Product_Title"].ToString();
            ShareProductData.Product_Category = fgList[fgList.Row, "Product_Category"].ToString();
            ShareProductData.StockExchange_ID = Convert.ToInt32(fgList[fgList.Row, "StockExchange_ID"]);
            ShareProductData.StockExchange_Code = fgList[fgList.Row, "StockExchange_Code"].ToString();
            ShareProductData.Currency = fgList[fgList.Row, "Currency"].ToString();
            ShareProductData.Weight = Convert.ToSingle(fgList[fgList.Row, "Weight"]);
            ShareProductData.LastClosePrice = Convert.ToSingle(fgList[fgList.Row, "LastClosePrice"]);
            ShareProductData.URL_ID = fgList[fgList.Row, "URL_ID"].ToString();
            ShareProductData.Shares_ID = Convert.ToInt32(fgList[fgList.Row, "Shares_ID"]);
            ShareProductData.ShareCode_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
            ShareProductData.Product_ID = Convert.ToInt32(fgList[fgList.Row, "Product_ID"]);
            ShareProductData.ProductCategory_ID = Convert.ToInt32(fgList[fgList.Row, "ProductCategory_ID"]);
            ShareProductData.OK_Flag = Convert.ToInt32(fgList[fgList.Row, "OK_Flag"]);
            ShareProductData.OK_String = fgList[fgList.Row, "OK_String"] + "";
            ShareProductData.HFIC_Recom = Convert.ToInt32(fgList[fgList.Row, "HFIC_Recom"]);

            bShowProductsList = false;
            txtShareTitle.Text = fgList[fgList.Row, "Title"].ToString();
            bShowProductsList = true;
            ShareCode_ID.Text = fgList[fgList.Row, "ID"].ToString();
        }
        private void btnChoice_Click(object sender, EventArgs e)
        {
            sCodesList = "";
            for (i = 1; i <= fgList.Rows.Count - 1; i++)  {
                if (Convert.ToBoolean(fgList[i, 0]))
                    sCodesList = sCodesList + fgList[i, "Code"] + "\t" + fgList[i, "ISIN"] + "\t" + fgList[i, "Title"] + "\t" + fgList[i, "ID"] + "~";
            }
            ShareCode_ID.Text = "-999";                                 // -1 - multiple records choice

            //bubble the event up to the parent
            //if (this.ButtonClick != null)
            //    this.ButtonClick(this, e);

            this.Width = txtShareTitle.Width;
            this.Height = txtShareTitle.Height;
        }
        public void ShareCode_ID_TextChanged(object sender, EventArgs e)
        {
            if (ShareCode_ID.Text != "-888")
               if (TextOfLabelChanged != null)  
                   TextOfLabelChanged(this, e);
        }
        private void picClose_Click(object sender, EventArgs e)
        {
            this.Width = txtShareTitle.Width;
            this.Height = txtShareTitle.Height;
        }
        public bool ShowProductsList { get { return this.bShowProductsList; } set { this.bShowProductsList = value; } }
        public bool ShowNonAccord { get { return this.bShowNonAccord; } set { this.bShowNonAccord = value; } }
        public bool ShowCancelled { get { return this.bShowCancelled; } set { this.bShowCancelled = value; } }
        public bool BlockNonRecommended { get { return this.bBlockNonRecommended; } set { this.bBlockNonRecommended = value; } }
        public int ShowWidth { get { return this.iShowWidth; } set { this.iShowWidth = value; } }
        public int ShowHeight { get { return this.iShowHeight; } set { this.iShowHeight = value; } }
        public int Mode { get { return this.iMode; } set { this.iMode = value; } }                                                      // 1 - one product selection mode, 2 - multiple products selection mode
        public string Filters { get { return this.sFilters; } set { this.sFilters = value; } }
        public Global.ProductData SelectedProductData { get { return this.ShareProductData; } set { this.ShareProductData = value; } }
        public string CodesList { get { return this.sCodesList; } set { this.sCodesList = value; } }
        public DataTable ProductsContract { get { return dtProductsContract; } set { dtProductsContract = value; } }
        public int ListType { get { return this.iListType; } set { this.iListType = value; } }                                          // 1 - dtProducts, 2 - dtProductsContract
    }
}

