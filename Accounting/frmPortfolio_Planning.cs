using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using Core;
using System.Diagnostics;
using System.Runtime.InteropServices;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Columns;
using C1.Win.C1FlexGrid;

namespace Accounting
{
    public partial class frmPortfolio_Planning : Form
    {
        int i = 0, j = 0, k = 0, iShareCodes_ID = 0, iBulkCommand_ID = 0, iStockExchange_ID = 0;
        int iCustodyProvider_ID = 0, iChoiceProduct_ID = 0, iChoiceProductCategory_ID = 0, iNewContract_ID = 0;
        string sNewCode = "", sNewPortfolio = "";
        decimal decAmount = 0, decQuantity = 0;
        bool bCheckList;
        DataRow[] foundRows;
        DataTable dtDetails;
        DataColumn dtCol;
        DataRow dtRow;
        DataView dtView;
        DataSet dataSet11;
        DXMenuItem[] menuItems;
        clsContracts_Balances Contracts_Balances = new clsContracts_Balances();
        clsContracts_BalancesRecs Contracts_BalancesRecs = new clsContracts_BalancesRecs();
        clsContracts klsContract = new clsContracts();
        clsProductsCodes klsProductsCodes = new clsProductsCodes();
        clsOrdersSecurity klsOrder = new clsOrdersSecurity();
        clsCompanyCodes klsCompanyCode = new clsCompanyCodes();

        public frmPortfolio_Planning()
        {
            InitializeComponent();
            InitializeMenuItems();
        }
        void InitializeMenuItems()
        {
            DXMenuItem itemEdit = new DXMenuItem("Προβολη SingleOrder", ItemEdit_Click);
            DXMenuItem itemDelete = new DXMenuItem("Προβολή Execution Order", ItemDelete_Click);
            menuItems = new DXMenuItem[] { itemEdit, itemDelete };
        }

        private void frmTrx_Balances_Load(object sender, EventArgs e)
        {
            ucDates.DateFrom = DateTime.Now;
            ucDates.Left = 16;
            ucDates.Top = 12;

            ucPS.StartInit(650, 350, 200, 20, 1);
            ucPS.TextOfLabelChanged += new EventHandler(ucPS_TextChanged);
            ucPS.Filters = "Aktive >= 1 ";
            ucPS.ListType = 1;                                                                  // iListType = 1 : Global.dtProducts - common list of products
            ucPS.ShowNonAccord = true;                                                          // Don't show NonAccordable products (oxi katallila) with red Background
            ucPS.ShowCancelled = false;

            ucPS2.StartInit(650, 350, 200, 20, 1);
            ucPS2.TextOfLabelChanged += new EventHandler(ucPS2_TextChanged);
            ucPS2.Filters = "Aktive >= 1 ";
            ucPS2.ListType = 1;                                                                  // iListType = 1 : Global.dtProducts - common list of products
            ucPS2.ShowNonAccord = true;                                                          // Don't show NonAccordable products (oxi katallila) with red Background
            ucPS2.ShowCancelled = false;

            //-------------- Define ServiceProviders List ------------------
            dtView = Global.dtServiceProviders.Copy().DefaultView;
            dtView.RowFilter = "ProviderType = 0 OR ProviderType = 1 OR ProviderType = 2";
            cmbProviders.DataSource = dtView;
            cmbProviders.DisplayMember = "Title";
            cmbProviders.ValueMember = "ID";
            cmbProviders.SelectedValue = 0;

            gridView1 = grdList.MainView as GridView;
            //gridView1.FocusedRowObjectChanged += gridView1_FocusedRowObjectChanged;
            //gridView1.DoubleClick += gridView1_DoubleClick;

            fgBuy.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(fgBuy_CellChanged);
            fgSell.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(fgSell_CellChanged);

        }
        protected override void OnResize(EventArgs e)
        {
            panMain.Width = this.Width - 32;
            btnSearch.Left = panMain.Width - 120;
            grdList.Width = this.Width - 32;
            grdList.Height = this.Height - 204;

            panBuy.Left = (Screen.PrimaryScreen.Bounds.Width - panBuy.Width) / 2;
            panBuy.Top = (Screen.PrimaryScreen.Bounds.Height - panBuy.Height) / 2;

            panSell.Left = (Screen.PrimaryScreen.Bounds.Width - panSell.Width) / 2;
            panSell.Top = (Screen.PrimaryScreen.Bounds.Height - panSell.Height) / 2;

            panAction.Left = (Screen.PrimaryScreen.Bounds.Width - panAction.Width) / 2;
            panAction.Top = (Screen.PrimaryScreen.Bounds.Height - panAction.Height) / 2;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            bCheckList = false;
            DefineList();
            bCheckList = true;
        }
        private void DefineList()
        {
            i = 0;

            Contracts_BalancesRecs = new clsContracts_BalancesRecs();
            Contracts_BalancesRecs.DateFrom = ucDates.DateFrom;
            Contracts_BalancesRecs.DateTo = ucDates.DateTo;
            Contracts_BalancesRecs.CDP_ID = 0;
            Contracts_BalancesRecs.GetList();
            grdList.DataSource = Contracts_BalancesRecs.List;
        }
        private void tsbExcel_Click(object sender, EventArgs e)
        {
            string path = "C:/Temp/ISP_output.xlsx";
            grdList.ExportToXlsx(path);
            // Open the created XLSX file with the default application.
            Process.Start(path);
        }
        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Right))
            {
                MessageBox.Show("aaaa");
                return;
            }

            //GridHitInfo info = (sender as GridView).CalcHitInfo(e.Location);
            //int rowHandle = info.InRow ? info.RowHandle : GridControl.InvalidRowHandle;
            //MessageBox.Show(rowHandle.ToString());
        }
        private void gridView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Right))
            {
                MessageBox.Show("bbbb");
                return;
            }

            //DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = this.gridView1.CalcHitInfo(e.Location); 
            //if (e.Button == MouseButtons.Right) { this.popupMenu1.ShowPopup(Control.MousePosition); } 
        }
        private void gridView1_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Column)//Determine whether it is the right-click menu for the column header
            {
                GridViewColumnMenu menu = e.Menu as GridViewColumnMenu;
                //menu.Items.RemoveAt(6);//Remove the seventh function in the right-click menu, start from 0
                menu.Items.Clear();//Clear all functions
                string strDisp = "aaaa";  // Right - click information you need to add

                DXMenuItem dxm = new DXMenuItem();
                dxm.Caption = strDisp;
                menu.Items.Add(dxm);
            }
        }

        private void gridView2_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                GridView view = sender as GridView;
                view.FocusedRowHandle = e.HitInfo.RowHandle;

                foreach (DXMenuItem item in menuItems)
                    e.Menu.Items.Add(item);
            }
        }



        private void barMain_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridView view = grdList.FocusedView as GridView;
            if (e.Item.Caption == "Show field and row handle")
                MessageBox.Show("Field = " + column.FieldName + ", RowHandle = " + rowHandle);
            if (e.Item.Caption == "RemoveCurrentRow")
                view.DeleteRow(gridView1.FocusedRowHandle);
        }
        int rowHandle;
        GridColumn column;

        private void gridView2_ShowGridMenu(object sender, PopupMenuShowingEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(e.Point);
            if (hitInfo.InRowCell)
            {
                view.FocusedRowHandle = rowHandle = hitInfo.RowHandle;
                column = hitInfo.Column;
            }
        }
        private void gridView1_FocusedRowHandle(object sender, FocusedRowChangedEventArgs e)
        {
            GridView view = sender as GridView;
            if (view == null) return;
            if (view.IsGroupRow(e.FocusedRowHandle))
            {
                bool expanded = view.GetRowExpanded(e.FocusedRowHandle);
                view.SetRowExpanded(e.FocusedRowHandle, !expanded);
            }
        }
        private void ItemEdit_Click(object sender, System.EventArgs e)
        {
            gridView1.ShowEditor();
        }
        private void ItemDelete_Click(object sender, System.EventArgs e)
        {
            gridView1.DeleteRow(gridView1.FocusedRowHandle);
        }
        #region ------------------- Buy ----------------------------------------------------------------
        private void btnBuy_Click(object sender, EventArgs e)
        {
            ucPS.ShowProductsList = false;
            ucPS.txtShareTitle.Text = "";
            ucPS.ShowProductsList = true;
            lblCurr.Text = "";
            txtPrice.Text = "0";
            txtBuyPercent.Text = "0";
            fgBuy.Rows.Count = 1;
            panBuy.Visible = true;
        }
        private void picBuy_Close_Click(object sender, EventArgs e)
        {
            panBuy.Visible = false;
        }
        private void txtPrice_LostFocus(object sender, EventArgs e)
        {
            bCheckList = false;
            fgBuy.Redraw = false;

            CalcBuySums();
        }
        private void fgBuy_CellChanged(object sender, RowColEventArgs e)
        {
            if (bCheckList)
                if (e.Col == 11) CalcBuySums();
        }
        private void CalcBuySums()
        {
            decAmount = 0;
            decQuantity = 0;
            for (i = 1; i < fgBuy.Rows.Count; i++)
            {
                fgBuy[i, "Buy_Amount"] = (Convert.ToSingle(fgBuy[i, "Order_Quantity"]) * Convert.ToSingle(txtPrice.Text)).ToString("###,##0.00");
                decAmount = decAmount + Convert.ToDecimal(fgBuy[i, "Buy_Amount"]);
                decQuantity = decQuantity + Convert.ToDecimal(fgBuy[i, "Order_Quantity"]);
            }
            lblBuyAmount.Text = decAmount.ToString("###,##0.00");
            lblBuyQuantity.Text = decQuantity.ToString("###,##0.00");
        }
        private void btnBuy_OK_Click(object sender, EventArgs e)
        {
            bCheckList = false;
            fgBuy.Redraw = false;
            fgBuy.Rows.Count = 1;

            i = 0;
            foreach (DataRow dtRow in Contracts_BalancesRecs.List.Rows) {
                j = fgBuy.FindRow(dtRow["CDP_ID"].ToString(), 1, 12, false);
                if (j < 0)
                {
                    i = i + 1;
                    fgBuy.AddItem(i + "\t" + dtRow["ContractTitle"] + "\t" + dtRow["ContractCode"] + "\t" + dtRow["ContractPortfolio"] + "\t" + 
                                  dtRow["Curr"] + "\t" + dtRow["TotalUnits"] + "\t" + "0" + "\t" + "0" + "\t" + "0" + "\t" + "0" + "\t" +
                                  "0" + "\t" + "0" + "\t" + dtRow["CDP_ID"] + "\t" + dtRow["Client_ID"]);
                    k = fgBuy.Rows.Count - 1;
                }
                else k = j;

                if (Convert.ToInt32(dtRow["ShareCodes_ID"]) == iShareCodes_ID)
                {
                    fgBuy[k, "TotalUnits"] = dtRow["TotalUnits"];
                    fgBuy[k, "Weight"] = dtRow["Participation_PRC"];
                    fgBuy[k, "CurrentValue_RepCcy"] = dtRow["CurrentValue_RepCcy"];                    
                }
            }
            fgBuy.Sort(SortFlags.Ascending, 6);
            for (i = 1; i < fgBuy.Rows.Count; i++)
            {
                fgBuy[i, 0] = i;
                fgBuy[i, "Buy_Amount"] = (Convert.ToDecimal(fgBuy[i, "TotalValue"]) * Convert.ToDecimal(txtBuyPercent.Text) / 100).ToString("###,##0.00");
                fgBuy[i, "Buy_Quantity"] = (Convert.ToSingle(fgBuy[i, "Buy_Amount"]) / Convert.ToSingle(txtPrice.Text)).ToString("###,##0.00");
                fgBuy[i, "Order_Quantity"] = Convert.ToSingle(fgBuy[i, "Buy_Quantity"]) - Convert.ToSingle(fgBuy[i, "TotalUnits"]);
            }
            fgBuy.Redraw = true;

            CalcBuySums();

            bCheckList = true;
        }
        private void btnGo_Buy_Click(object sender, EventArgs e)
        {
            dAktionDate.Value = DateTime.Now;
            cmbProviders.SelectedValue = 0;
            lblAction.Text = "BUY";
            lstType.SelectedIndex = 0;
            lblPrice.Text = txtPrice.Text;
            lblQuantity.Text = lblBuyQuantity.Text;
            lblAmount.Text = lblBuyAmount.Text;
            cmbConstant.SelectedIndex = 0;
            dConstant.Visible = false;

            j = 0;
            fgContracts.Redraw = false;
            fgContracts.Rows.Count = 1;
            for (i = 1; i < fgBuy.Rows.Count - 1; i++)
            {
                if (Convert.ToSingle(fgBuy[i, "Order_Quantity"]+"") != 0)
                {
                    j = j + 1;
                    fgContracts.AddItem(j + "\t" + fgBuy[i, "ContractTitle"] + "\t" + fgBuy[i, "Code"] + "\t" + fgBuy[i, "Portfolio"] + "\t" + fgBuy[i, "Order_Quantity"] + "\t" +
                                        fgBuy[i, "CDP_ID"] + "\t" + fgBuy[i, "Client_ID"]);
                }
            }

            fgContracts.Redraw = true;
            lblSumQuantity.Text = lblBuyQuantity.Text;

            panAction.Visible = true;
            panBuy.Visible = false;
            btnCreateOrders.Enabled = false;
        } 
        #endregion ---------------------------------------------------------------------------------
        #region ---------------- Sell --------------------------------------------------------------
        private void btnSell_Click(object sender, EventArgs e)
        {
            ucPS2.ShowProductsList = false;
            ucPS2.txtShareTitle.Text = "";
            ucPS2.ShowProductsList = true;
            lblCurr2.Text = "";
            txtPrice2.Text = "0";
            txtSellPercent.Text = "100";
            fgSell.Rows.Count = 1;
            panSell.Visible = true;
        }
        private void picSell_Close_Click(object sender, EventArgs e)
        {
            panSell.Visible = false;
        }
        private void txtPrice2_LostFocus(object sender, EventArgs e)
        {
            bCheckList = false;
            fgSell.Redraw = false;
            for (i = 1; i < fgSell.Rows.Count; i++)
            {
                fgSell[i, "Sell_Amount"] = (Convert.ToSingle(fgSell[i, "Order_Quantity"]) * Convert.ToSingle(txtPrice2.Text)).ToString("###,##0.00");
            }
            fgSell.Redraw = true;
            CalcSellSums();
            bCheckList = true;
        }
        private void fgSell_CellChanged(object sender, RowColEventArgs e)
        {
            if (bCheckList)
                if (e.Col == 11) CalcSellSums();
        }

        private void CalcSellSums()
        {
            decAmount = 0;
            decQuantity = 0;
            for (i = 1; i < fgSell.Rows.Count; i++)
            {
                fgSell[i, "Sell_Amount"] = (Convert.ToSingle(fgSell[i, "Order_Quantity"]) * Convert.ToSingle(txtPrice2.Text)).ToString("###,##0.00");
                decAmount = decAmount + Convert.ToDecimal(fgSell[i, "Sell_Amount"]);
                decQuantity = decQuantity + Convert.ToDecimal(fgSell[i, "Order_Quantity"]);
            }
            lblSellAmount.Text = decAmount.ToString("###,##0.00");
            lblSellQuantity.Text = decQuantity.ToString("###,##0.00");
        }
        private void btnSell_OK_Click(object sender, EventArgs e)
        {
            bCheckList = false;
            fgSell.Redraw = false;
            fgSell.Rows.Count = 1;

            i = 0;
            foreach (DataRow dtRow in Contracts_BalancesRecs.List.Rows)
            {
                if (Convert.ToInt32(dtRow["ShareCodes_ID"]) == iShareCodes_ID)
                {
                    i = i + 1;
                    fgSell.AddItem(i + "\t" + dtRow["ContractTitle"] + "\t" + dtRow["ContractCode"] + "\t" + dtRow["ContractPortfolio"] + "\t" +
                                    dtRow["Curr"] + "\t" + dtRow["TotalUnits"] + "\t" + dtRow["TotalUnits"] + "\t" + dtRow["Participation_PRC"] + "\t" + 
                                    dtRow["CurrentValue_RepCcy"] + "\t" + "0" + "\t" + "0" + "\t" + dtRow["CDP_ID"] + "\t" + dtRow["Client_ID"]);
                }
            }

            fgSell.Sort(SortFlags.Ascending, 6);
            for (i = 1; i < fgSell.Rows.Count; i++)
            {
                fgSell[i, 0] = i;
                fgSell[i, "Sell_Quantity"] = (Convert.ToDecimal(fgSell[i, "TotalUnits"]) * Convert.ToDecimal(txtSellPercent.Text) / 100).ToString("###,##0.00");
                fgSell[i, "Sell_Amount"] = (Convert.ToSingle(fgSell[i, "Sell_Quantity"]) * Convert.ToSingle(txtPrice2.Text)).ToString("###,##0.00");                
                fgSell[i, "Order_Quantity"] = Convert.ToSingle(fgSell[i, "Sell_Quantity"]);
            }
            fgSell.Redraw = true;

            CalcSellSums();
            bCheckList = true;

        }
        private void btnGo_Sell_Click(object sender, EventArgs e)
        {
            dAktionDate.Value = DateTime.Now;
            cmbProviders.SelectedValue = 0;
            lblAction.Text = "SELL";
            lstType.SelectedIndex = 0;
            lblPrice.Text = txtPrice.Text;
            lblQuantity.Text = lblSellQuantity.Text;
            lblAmount.Text = lblSellAmount.Text;
            cmbConstant.SelectedIndex = 0;
            dConstant.Visible = false;

            j = 0;
            fgContracts.Redraw = false;
            fgContracts.Rows.Count = 1;
            for (i = 1; i < fgSell.Rows.Count - 1; i++)
            {
                if (Convert.ToSingle(fgSell[i, "Order_Quantity"] + "") != 0)
                {
                    j = j + 1;
                    fgContracts.AddItem(j + "\t" + fgSell[i, "ContractTitle"] + "\t" + fgSell[i, "Code"] + "\t" + fgSell[i, "Portfolio"] + "\t" + fgSell[i, "Order_Quantity"] + "\t" +
                                        fgSell[i, "CDP_ID"] + "\t" + fgSell[i, "Client_ID"]);
                }
            }

            fgContracts.Redraw = true;
            lblSumQuantity.Text = lblSellQuantity.Text;

            panAction.Visible = true;
            panSell.Visible = false;
            btnCreateOrders.Enabled = false;
        }
        #endregion ---------------------------------------------------------------------------------
        #region ---------------- Action --------------------------------------------------------------
        private void btnCreateOrders_Click(object sender, EventArgs e)
        {    
            klsOrder = new clsOrdersSecurity();
            iBulkCommand_ID = klsOrder.GetNextBulkCommand();

            for (i = 1; i < fgContracts.Rows.Count; i++)
            {
                if (Convert.ToSingle(fgContracts[i, "Quantity"]) > 0)
                {
                    klsOrder = new clsOrdersSecurity();
                    klsOrder.BulkCommand = "<" + iBulkCommand_ID + ">";
                    klsOrder.BusinessType_ID = 1;
                    klsOrder.CommandType_ID = 1;                                                                // 1 - Single Order
                    klsOrder.Client_ID = Convert.ToInt32(fgContracts[i, "Client_ID"]);
                    klsOrder.Company_ID = Global.Company_ID;
                    klsOrder.ServiceProvider_ID = Global.Company_ID;
                    klsOrder.StockExchange_ID = iStockExchange_ID;
                    klsOrder.ServiceProvider_ID = Convert.ToInt32(cmbProviders.SelectedValue);
                    klsOrder.CustodyProvider_ID = iCustodyProvider_ID;
                    klsOrder.Depository_ID = 0;
                    klsOrder.II_ID = 0;
                    klsOrder.Parent_ID = 0;
                    klsOrder.Contract_ID = Convert.ToInt32(fgContracts[i, "Contract_ID"]);
                    klsOrder.Contract_Details_ID = Convert.ToInt32(fgContracts[i, "Contract_Details_ID"]);
                    klsOrder.Contract_Packages_ID = Convert.ToInt32(fgContracts[i, "Contract_Packages_ID"]);
                    klsOrder.Code = fgContracts[i, "Code"] + "";
                    klsOrder.ProfitCenter = fgContracts[i, "Portfolio"] + "";
                    klsOrder.AllocationPercent = 100;
                    klsOrder.Aktion = 1;                                                                         // BUY   
                    klsOrder.AktionDate = DateTime.Now;
                    klsOrder.Share_ID = iShareCodes_ID;
                    klsOrder.Product_ID = iChoiceProduct_ID;
                    klsOrder.ProductCategory_ID = iChoiceProductCategory_ID;
                    klsOrder.Curr = lblCurr.Text;
                    klsOrder.PriceType = 0;
                    klsOrder.Price = Convert.ToDecimal(txtPrice.Text);
                    klsOrder.Quantity = Convert.ToDecimal(lblBuyQuantity.Text);
                    klsOrder.Amount = Convert.ToDecimal(lblBuyAmount.Text);
                    klsOrder.Constant = cmbConstant.SelectedIndex;
                    klsOrder.ConstantDate = (cmbConstant.SelectedIndex == 2 ? dConstant.Value.ToString("dd/MM/yyyy") : "");
                    klsOrder.RecieveDate = DateTime.Now;
                    klsOrder.RecieveMethod_ID = 0;
                    klsOrder.SentDate = Convert.ToDateTime("1900/01/01");
                    klsOrder.FIX_A = -1;
                    klsOrder.Notes = "";
                    klsOrder.User_ID = Global.User_ID;
                    klsOrder.DateIns = DateTime.Now;
                    klsOrder.Status = 0;
                    klsOrder.InsertRecord();
                }
            }

            //--- add new Bulk or Execution Command - depend on iChoiceBusinessType_ID ----------------------------------
            klsOrder = new clsOrdersSecurity();

            klsOrder.BulkCommand = "<" + iBulkCommand_ID + ">";
            klsOrder.BusinessType_ID = 2;
            klsOrder.CommandType_ID = 2;                                                               // 2 - Execution
            klsOrder.Client_ID = 0;
            klsOrder.Company_ID = Global.Company_ID;
            klsOrder.ServiceProvider_ID = Global.Company_ID;
            klsOrder.StockExchange_ID = iStockExchange_ID;
            klsOrder.ServiceProvider_ID = Convert.ToInt32(cmbProviders.SelectedValue);
            klsOrder.CustodyProvider_ID = iCustodyProvider_ID;
            klsOrder.Depository_ID = 0;
            klsOrder.II_ID = 0;
            klsOrder.Parent_ID = 0;
            klsOrder.Contract_ID = iNewContract_ID;
            klsOrder.Contract_Details_ID = 0;
            klsOrder.Contract_Packages_ID = 0;
            klsOrder.Code = sNewCode;
            klsOrder.ProfitCenter = sNewPortfolio;
            klsOrder.AllocationPercent = 100;
            klsOrder.Aktion = 1;                                                                         // BUY   
            klsOrder.AktionDate = DateTime.Now;
            klsOrder.Share_ID = iShareCodes_ID;
            klsOrder.Product_ID = iChoiceProduct_ID;
            klsOrder.ProductCategory_ID = iChoiceProductCategory_ID;
            klsOrder.Curr = lblCurr.Text;
            klsOrder.PriceType = 0;
            klsOrder.Price = Convert.ToDecimal(txtPrice.Text);
            klsOrder.Quantity = Convert.ToDecimal(lblBuyQuantity.Text);
            klsOrder.Amount = Convert.ToDecimal(lblBuyAmount.Text);
            klsOrder.Constant = cmbConstant.SelectedIndex;
            klsOrder.ConstantDate = (cmbConstant.SelectedIndex == 2 ? dConstant.Value.ToString("dd/MM/yyyy") : "");
            klsOrder.RecieveDate = DateTime.Now;
            klsOrder.RecieveMethod_ID = 0;
            klsOrder.SentDate = Convert.ToDateTime("1900/01/01");
            klsOrder.FIX_A = -1;
            klsOrder.Notes = "";
            klsOrder.User_ID = Global.User_ID;
            klsOrder.DateIns = DateTime.Now;
            klsOrder.Status = 0;
            klsOrder.InsertRecord();

            panAction.Visible = false;
        }
        private void picAction_Close_Click(object sender, EventArgs e)
        {
            panAction.Visible = false;
        }
        #endregion ------------------------------------------------------------------------------------------------
        protected void ucPS_TextChanged(object sender, EventArgs e)
        {
            Global.ProductData stProduct = new Global.ProductData();
            stProduct = ucPS.SelectedProductData;
            iShareCodes_ID = stProduct.ShareCode_ID;
            iStockExchange_ID = stProduct.StockExchange_ID;
            lblCurr.Text = stProduct.Currency;
            txtPrice.Text = stProduct.LastClosePrice.ToString();
            iChoiceProduct_ID = stProduct.Product_ID;
            lblProduct_Title.Text = stProduct.Product_Title;
            iChoiceProductCategory_ID = stProduct.ProductCategory_ID;
            lblProductCategory_Title.Text = stProduct.Product_Category;
            lblTitle.Text = stProduct.Title;
            lblCode.Text = stProduct.Code;
            lblISIN.Text = stProduct.ISIN;
            lblCurrency.Text = stProduct.Currency;
            lblStockExchange.Text = stProduct.StockExchange_Code;
            iStockExchange_ID = stProduct.StockExchange_ID;
        }
        protected void ucPS2_TextChanged(object sender, EventArgs e)
        {
            Global.ProductData stProduct = new Global.ProductData();
            stProduct = ucPS2.SelectedProductData;
            iShareCodes_ID = stProduct.ShareCode_ID;
            iStockExchange_ID = stProduct.StockExchange_ID;
            lblCurr2.Text = stProduct.Currency;
            txtPrice2.Text = stProduct.LastClosePrice.ToString();
            iChoiceProduct_ID = stProduct.Product_ID;
            lblProduct_Title.Text = stProduct.Product_Title;
            iChoiceProductCategory_ID = stProduct.ProductCategory_ID;
            lblProductCategory_Title.Text = stProduct.Product_Category;
            lblTitle.Text = stProduct.Title;
            lblCode.Text = stProduct.Code;
            lblISIN.Text = stProduct.ISIN;
            lblCurrency.Text = stProduct.Currency;
            lblStockExchange.Text = stProduct.StockExchange_Code;
            iStockExchange_ID = stProduct.StockExchange_ID;
        }
        private void cmbProviders_SelectedValueChanged(object sender, EventArgs e)
        {
            if (bCheckList)
            {
                klsCompanyCode = new clsCompanyCodes();
                klsCompanyCode.Record_ID = 0;
                klsCompanyCode.ServiceProvider_ID = Convert.ToInt32(cmbProviders.SelectedValue);
                klsCompanyCode.GetRecord();
                sNewCode = klsCompanyCode.Code;
                sNewPortfolio = klsCompanyCode.Portfolio;

                klsContract = new clsContracts();
                klsContract.PackageType = 2;
                klsContract.ServiceProvider_ID = Convert.ToInt32(cmbProviders.SelectedValue);
                klsContract.DateStart = Convert.ToDateTime("1900/01/01");
                klsContract.DateFinish = Convert.ToDateTime("2071/12/31");
                klsContract.GetList_Provider_ID();
                foreach (DataRow dtRow in klsContract.List.Rows)
                {
                    sNewCode = dtRow["Code"] + "";
                    sNewPortfolio = dtRow["Portfolio"] + "";
                    iNewContract_ID = Convert.ToInt32(dtRow["ID"]);
                }

                btnCreateOrders.Enabled = true;
            }
        }
        private void cmbConstant_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbConstant.SelectedIndex == 2)
            {
                dConstant.Value = DateTime.Now;
                dConstant.Visible = true;
            }
            else dConstant.Visible = false;
        }
    }
}
