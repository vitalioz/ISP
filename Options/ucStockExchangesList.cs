using System;
using System.Data;
using System.Collections;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Core;

namespace Options
{
    public partial class ucStockExchangesList : UserControl
    {
        int i, iID, iList_ID, iRow, iLevel, iMax1Level, iAction, iAddMode, iCashTables_ID, iRightsLevel;
        string sTemp, sTableName;
        bool bCheckList, bCollapsed;
        SortedList lstProviders = new SortedList();
        DataView dtView;
        clsStockExchanges StockExchanges = new clsStockExchanges();
        clsStockExchanges_Alias StockExchanges_Alias = new clsStockExchanges_Alias();
        public ucStockExchangesList()
        {
            InitializeComponent();
        }

        private void ucStockExchangesList_Load(object sender, EventArgs e)
        {
            panDetails.Enabled = false;
            tsbSave.Enabled = false;

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, BackColor:LightSteelBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, BackColor:Transparent; ForeColor:Black;}");
            fgList.RowColChange += new EventHandler(fgList_RowColChange);

            //------- fgAliases ----------------------------
            fgAliases.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgAliases.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, BackColor:LightSteelBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, BackColor:Transparent; ForeColor:Black;}");
            fgAliases.CellChanged += new RowColEventHandler(fgAliases_CellChanged);

        }
        protected override void OnResize(EventArgs e)
        {
            grpData.Height = this.Height - 6;
            grpData.Width = this.Width - 20;
            fgList.Height = this.Height - 92;
        }
        public void StartInit(int iParentList_ID, int iParentCashTables_ID, string sParentTableName, string sParentListTitle)
        {
            iList_ID = iParentList_ID;
            iCashTables_ID = iParentCashTables_ID;
            sTableName = sParentTableName;
            lblListItemTitle.Text = sParentListTitle;

            //-------------- Define Countries List ------------------
            dtView = Global.dtCountries.Copy().DefaultView;
            dtView.RowFilter = "Tipos = 1";
            cmbCountry.DataSource = dtView;
            cmbCountry.DisplayMember = "Title";
            cmbCountry.ValueMember = "ID";

            //-------------- Define Providers List ------------------
            lstProviders.Clear();
            foreach (DataRow dtRow in Global.dtServiceProviders.Rows) 
            lstProviders.Add(dtRow["ID"], dtRow["Title"]);

            fgAliases.Cols[0].DataMap = lstProviders;

            DefineList();

            if (fgList.Rows.Count > 1) {
                fgList.Row = 1;
                ShowRecord();
            }
            if (iRightsLevel == 1) toolLeft.Enabled = false;
        }
        private void DefineList()
        {
            try
            {
                iRow = 0;
                iMax1Level = 0;
                fgList.Redraw = false;
                fgList.Tree.Column = 0;
                fgList.Rows.Count = 1;

                StockExchanges = new clsStockExchanges();
                StockExchanges.GetList_Tree();
                foreach (DataRow dtRow in StockExchanges.List.Rows)
                {
                    if (Convert.ToInt32(dtRow["ID"]) != 0)
                    {
                        iRow = iRow + 1;
                        iLevel = 1;

                        if (Convert.ToInt32(dtRow["Parent_ID"]) != 0) iLevel = 2;

                        if (iLevel == 1)
                        {
                            i = Convert.ToInt32(dtRow["Parent_ID"]);
                            if (i > iMax1Level)
                            {
                                iMax1Level = i;
                            }
                        }

                        fgList.Rows.InsertNode(iRow, iLevel);
                        fgList[iRow, 0] = dtRow["Code"];
                        fgList[iRow, 1] = dtRow["Title"];
                        fgList[iRow, 2] = dtRow["ID"];
                        fgList[iRow, 3] = dtRow["Parent_ID"];
                        fgList[iRow, 4] = iLevel;

                        for (i = 1; i <= fgList.Rows.Count - 1; i++)
                            fgList.Rows[i].Node.Collapsed = bCollapsed;
                    }

                }

                for (i = 1; i <= fgList.Rows.Count - 1; i++)
                    fgList.Rows[i].Node.Collapsed = true;

                fgList.Redraw = true;
                tsbCollapse.Visible = false;
                tsbExtend.Visible = true;
                bCheckList = true;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally {}
        }
        private void fgList_RowColChange(object sender, EventArgs e)
        {
            iAction = 1;
            panDetails.Enabled = false;
            tsbSave.Enabled = false;

            if (bCheckList) 
               if (fgList.Row > 0) ShowRecord();
        }
        private void ShowRecord()
        {
            StockExchanges = new clsStockExchanges();
            StockExchanges.Record_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
            StockExchanges.GetRecord();
            txtMIC.Text = StockExchanges.Code;
            txtTitle.Text = StockExchanges.Title;
            txtReutersCode.Text = StockExchanges.ReutersCode;
            txtTitle_Bloomberg.Text = StockExchanges.BloombergCode;
            txtTitle_MStar.Text = StockExchanges.MstarTitle;
            cmbCountry.SelectedValue = StockExchanges.Country_ID;

            fgAliases.Redraw = false;
            fgAliases.Rows.Count = 1;

            StockExchanges_Alias = new clsStockExchanges_Alias();
            StockExchanges_Alias.Item_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
            StockExchanges_Alias.GetList();
            foreach (DataRow dtRow in StockExchanges_Alias.List.Rows) {
                fgAliases.AddItem(dtRow["ServiceProvider_Title"] + "\t" + dtRow["Code"] + "\t" + dtRow["ID"] + "\t" + dtRow["ServiceProvider_ID"]);
            }
            fgAliases.Redraw = true;
        }
        private void tsbExtend_Click(object sender, EventArgs e)
        {
            for (i = 1; i <= fgList.Rows.Count - 1; i++)
                fgList.Rows[i].Node.Collapsed = false;

            fgList.Redraw = true;
            tsbCollapse.Visible = true;
            tsbExtend.Visible = false;
            bCollapsed = !bCollapsed;
        }

        private void tsbCollapse_Click(object sender, EventArgs e)
        {
            for (i = 1; i <= fgList.Rows.Count - 1; i++)
                fgList.Rows[i].Node.Collapsed = true;

            fgList.Redraw = true;
            tsbCollapse.Visible = false;
            tsbExtend.Visible = true;
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            iAction = 0;                                       // 0 - Add
            iAddMode = 1;
            txtMIC.Text = "";
            txtTitle.Text = "";
            txtReutersCode.Text = "";
            txtTitle_Bloomberg.Text = "";
            txtTitle_MStar.Text = "";
            cmbCountry.SelectedValue = 0;
            fgAliases.Rows.Count = 1;
            panDetails.Enabled = true;
            tsbSave.Enabled = true;
            txtMIC.Focus();            
        }

        private void tsbAdd2_Click(object sender, EventArgs e)
        {                   
            iAction = 0;                                         // 0 - Add
            iAddMode = 2;
            txtMIC.Text = "";
            txtTitle.Text = "";
            txtReutersCode.Text = "";
            txtTitle_Bloomberg.Text = "";
            txtTitle_MStar.Text = "";
            cmbCountry.SelectedValue = 0;
            fgAliases.Rows.Count = 1;
            panDetails.Enabled = true;
            tsbSave.Enabled = true;
            txtMIC.Focus();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            iAction = 1;                                         // 1 - Add
            panDetails.Enabled = true;
            tsbSave.Enabled = true;
            txtReutersCode.Focus();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 0)
                if (fgAliases.Rows.Count == 1)
                {
                    if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        StockExchanges = new clsStockExchanges();
                        StockExchanges.Record_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                        StockExchanges.DeleteRecord();
                        fgList.RemoveItem(fgList.Row);

                        if (fgList.Rows.Count > 1)
                        {
                            fgList.Focus();
                            iID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                        }
                        fgList.Redraw = true;

                        iAction = 1;                                                          // 1 - EDIT Mode
                        ShowRecord();
                    }
                }
                else MessageBox.Show("Can't delete stock exchange", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        private void picAdd_Alias_Click(object sender, EventArgs e)
        {
            fgAliases.AddItem("" + "\t" + "" + "\t" + "0" + "\t" + "0");
        }

        private void picDel_Alias_Click(object sender, EventArgs e)
        {
            if (fgAliases.Row > 1)
                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    StockExchanges_Alias = new clsStockExchanges_Alias();
                    StockExchanges_Alias.Record_ID = Convert.ToInt32(fgAliases[fgAliases.Row, "ID"]);
                    StockExchanges_Alias.DeleteRecord();
                    fgAliases.RemoveItem(fgAliases.Row);
                }
        }
        private void fgAliases_CellChanged(object sender, RowColEventArgs e)
        {
            if (e.Col == 0) fgAliases[e.Row, 3] = fgAliases[e.Row, 0];
        }
        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text.Length != 0)
            {
                if (iAction == 0) {                                    // 0 - ADD Mode
                    StockExchanges = new clsStockExchanges();
                    if (iAddMode == 1) {
                        StockExchanges.Tipos = 1;
                        StockExchanges.Parent_ID = 0;
                        StockExchanges.SortIndex = 0;
                    }
                    else {
                        StockExchanges.Tipos = 2;
                        StockExchanges.Parent_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                        StockExchanges.SortIndex = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                    }
                    StockExchanges.Code = txtMIC.Text;
                    StockExchanges.Title = txtTitle.Text;
                    StockExchanges.ReutersCode = txtReutersCode.Text;
                    StockExchanges.BloombergCode = txtTitle_Bloomberg.Text;
                    StockExchanges.MstarTitle = txtTitle_MStar.Text;
                    StockExchanges.Country_ID = Convert.ToInt32(cmbCountry.SelectedValue);
                    iID = StockExchanges.InsertRecord();

                    if (iAddMode == 1) {
                        StockExchanges = new clsStockExchanges();
                        StockExchanges.Record_ID = iID;
                        StockExchanges.GetRecord();
                        StockExchanges.SortIndex = iID;
                        StockExchanges.Code = txtMIC.Text;
                        StockExchanges.Title = txtTitle.Text;
                        StockExchanges.ReutersCode = txtReutersCode.Text;
                        StockExchanges.BloombergCode = txtTitle_Bloomberg.Text;
                        StockExchanges.MstarTitle = txtTitle_MStar.Text;
                        StockExchanges.Country_ID = Convert.ToInt32(cmbCountry.SelectedValue);
                        StockExchanges.EditRecord();
                    }
                }
                else
                {
                    iID = Convert.ToInt32(fgList[fgList.Row, "ID"]);

                    StockExchanges = new clsStockExchanges();
                    StockExchanges.Record_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                    StockExchanges.GetRecord();
                    StockExchanges.Code = txtMIC.Text;
                    StockExchanges.Title = txtTitle.Text;
                    StockExchanges.ReutersCode = txtReutersCode.Text;
                    StockExchanges.BloombergCode = txtTitle_Bloomberg.Text;
                    StockExchanges.MstarTitle = txtTitle_MStar.Text;
                    StockExchanges.Country_ID = Convert.ToInt32(cmbCountry.SelectedValue);
                    StockExchanges.EditRecord();

                    
                    for (i = 1; i <= fgAliases.Rows.Count - 1; i++) {                        
                        if (Convert.ToInt32(fgAliases[i, "ID"]) == 0 )
                        {
                            StockExchanges_Alias = new clsStockExchanges_Alias();
                            StockExchanges_Alias.Item_ID = iID;
                            StockExchanges_Alias.ServiceProvider_ID = Convert.ToInt32(fgAliases[i, "ServiceProvider_ID"]);
                            StockExchanges_Alias.Code = fgAliases[i, "Code"]+"";
                            StockExchanges_Alias.InsertRecord();
                        }
                        else
                        {
                            StockExchanges_Alias = new clsStockExchanges_Alias();
                            StockExchanges_Alias.Record_ID = Convert.ToInt32(fgAliases[i, "ID"]);
                            StockExchanges_Alias.GetRecord();
                            StockExchanges_Alias.ServiceProvider_ID = Convert.ToInt32(fgAliases[i, "ServiceProvider_ID"]);
                            StockExchanges_Alias.Code = fgAliases[i, "Code"] + "";
                            StockExchanges_Alias.EditRecord();
                        }
                    }

                }
                panDetails.Enabled = false;
                DefineList();

                sTemp = iID.ToString();
                i = fgList.FindRow(iID, 1, 2, false);
                if (i > 0) fgList.Row = i;
                else fgList.Row = 1;

                ShowRecord();
            }
            else MessageBox.Show("Η εισαγωγή του τίτλου είναι υποχρεωτική", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
    }
}
