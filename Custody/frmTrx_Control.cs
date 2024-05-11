using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using Core;
using System.Diagnostics;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Columns;

namespace Custody
{
    public partial class frmTrx_Control : Form
    {
        int i = 0, iRightsLevel;
        string sExtra;
        DataTable dtDetails;
        DataColumn dtCol;
        DataRow dtRow;
        DataSet dataSet11;
        DXMenuItem[] menuItems;
        frmTrx_Edit locTrx_Edit = new frmTrx_Edit();

        public frmTrx_Control()
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

        private void frmTrx_Control_Load(object sender, EventArgs e)
        {
            ucDates.DateFrom = DateTime.Now;
            ucDates.Left = 16;
            ucDates.Top = 12;

            gridView1 = grdList.MainView as GridView;
            gridView1.FocusedRowObjectChanged += gridView1_FocusedRowObjectChanged;
            gridView1.DoubleClick += gridView1_DoubleClick;

            gridView2 = grdDetails.MainView as GridView;

            popMain.ItemLinks.Add(new BarButtonItem(barMain, "Show field and row handle"));
            popMain.ItemLinks.Add(new BarButtonItem(barMain, "RemoveCurrentRow"));
        }
        protected override void OnResize(EventArgs e)
        {
            tcMain.Width = this.Width - 16;

            grdList.Width = this.Width - 296;
            grdList.Height = this.Height - 220;

            grdDetails.Left = this.Width - 284;
            grdDetails.Height = this.Height - 220;
            grdDetails.Width = 260;

            panMain.Width = this.Width - 30;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DefineList();
        }
        private void DefineList()
        {
            i = 0;

            clsTrx klsTrx = new clsTrx();
            klsTrx.DateFrom = ucDates.DateFrom;
            klsTrx.DateTo = ucDates.DateTo;
            klsTrx.GetList();
            grdList.DataSource = klsTrx.List;
        }
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            locTrx_Edit.Rec_ID = i;
            //locTrx_Edit.BusinessType = iBusinessType_ID;
            //locTrx_Edit.RightsLevel = iRightsLevel;
            //locTrx_Edit.Editable = 1;
            locTrx_Edit.ShowDialog();
        }

        private void tsbEdit_Click(object sender, EventArgs e)
        {
            EditRow();
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
        private void gridView1_MouseUp(object sender, MouseEventArgs e) {
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

        private void gridView2_ShowGridMenu(object sender, GridMenuEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(e.Point);
            if (hitInfo.InRowCell)
            {
                view.FocusedRowHandle = rowHandle = hitInfo.RowHandle;
                column = hitInfo.Column;
                popMain.ShowPopup(barMain, view.GridControl.PointToScreen(e.Point));
            }
        }


        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            EditRow();
        }
        private void EditRow()
        {
            i = 0;
            
            int[] selectedRows = gridView1.GetSelectedRows();
            foreach (int rowHandle in selectedRows)
                i = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "ID"));

            if (i != 0)
            {
                locTrx_Edit.Rec_ID = i;
                //locTrx_Edit.BusinessType = iBusinessType_ID;
                //locTrx_Edit.RightsLevel = iRightsLevel;
                //locTrx_Edit.Editable = 1;
                locTrx_Edit.ShowDialog();
            }
        }
        void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            i = gridView1.FocusedRowHandle;

            dtDetails = new DataTable("TrxDetails_List");
            dtCol = dtDetails.Columns.Add("Title", System.Type.GetType("System.String"));
            dtCol = dtDetails.Columns.Add("EUR", System.Type.GetType("System.String"));
            dtCol = dtDetails.Columns.Add("Cur", System.Type.GetType("System.String"));

            int[] selectedRows = gridView1.GetSelectedRows();
            foreach (int rowHandle in selectedRows)
            {
                i = Convert.ToInt32(gridView1.GetRowCellValue(rowHandle, "ID"));

                dtRow = dtDetails.NewRow();
                this.dtRow["Title"] = "Accruals";
                this.dtRow["EUR"] = gridView1.GetRowCellValue(rowHandle, "Accruals_EUR");
                this.dtRow["Cur"] = gridView1.GetRowCellValue(rowHandle, "Accruals_Cur");
                dtDetails.Rows.Add(dtRow);

                dtRow = dtDetails.NewRow();
                this.dtRow["Title"] = "ExecFee";
                this.dtRow["EUR"] = gridView1.GetRowCellValue(rowHandle, "ExecFee_EUR");
                this.dtRow["Cur"] = gridView1.GetRowCellValue(rowHandle, "ExecFee_Cur");
                dtDetails.Rows.Add(dtRow);

                dtRow = dtDetails.NewRow();
                this.dtRow["Title"] = "ExecFeeReturn";
                this.dtRow["EUR"] = gridView1.GetRowCellValue(rowHandle, "ExecFeeReturn_EUR");
                this.dtRow["Cur"] = gridView1.GetRowCellValue(rowHandle, "ExecFeeReturn_Cur");
                dtDetails.Rows.Add(dtRow);

                dtRow = dtDetails.NewRow();
                this.dtRow["Title"] = "ExecFeeIncome";
                this.dtRow["EUR"] = gridView1.GetRowCellValue(rowHandle, "ExecFeeIncome_EUR");
                this.dtRow["Cur"] = gridView1.GetRowCellValue(rowHandle, "ExecFeeIncome_Cur");
                dtDetails.Rows.Add(dtRow);

                dtRow = dtDetails.NewRow();
                this.dtRow["Title"] = "SettleFee";
                this.dtRow["EUR"] = gridView1.GetRowCellValue(rowHandle, "SettleFee_EUR");
                this.dtRow["Cur"] = gridView1.GetRowCellValue(rowHandle, "SettleFee_Cur");
                dtDetails.Rows.Add(dtRow);

                dtRow = dtDetails.NewRow();
                this.dtRow["Title"] = "SettleFeeReturn";
                this.dtRow["EUR"] = gridView1.GetRowCellValue(rowHandle, "SettleFeeReturn_EUR");
                this.dtRow["Cur"] = gridView1.GetRowCellValue(rowHandle, "SettleFeeReturn_Cur");
                dtDetails.Rows.Add(dtRow);

            }
            grdDetails.DataSource = dtDetails;
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
        public int RightsLevel { get { return iRightsLevel; } set { iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
