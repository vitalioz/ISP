using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using Core;

namespace Products
{
    public partial class frmInvestmentCommetties : Form
    {
        int i, iAction, iRightsLevel;
        string sExtra;
        string[] sTipos = { "", "EUR Reference Ccy", "USD Reference Ccy", "Hellenic Portfolios" };
        clsInvestmentCommetties_AssetAllocation InvestmentCommetties_AssetAllocation = new clsInvestmentCommetties_AssetAllocation();
        clsInvestmentCommetties_AssetAllocationRecs InvestmentCommetties_AssetAllocationRecs = new clsInvestmentCommetties_AssetAllocationRecs();
        public frmInvestmentCommetties()
        {
            InitializeComponent();
        }

        private void frmInvestmentCommetties_Load(object sender, EventArgs e)
        {

            cmbProfile.DataSource = Global.dtCustomersProfiles.Copy();
            cmbProfile.DisplayMember = "Title";
            cmbProfile.ValueMember = "ID";
            cmbProfile.SelectedItem = 1;

            gridView1 = grdAssetAllocation.MainView as GridView;
            gridView1.FocusedRowObjectChanged += gridView1_FocusedRowObjectChanged;
            gridView1.DoubleClick += gridView1_DoubleClick;
            gridView1.RowCellStyle += new RowCellStyleEventHandler(gridView1_RowCellStyle);
            gridView1.HorzScrollVisibility = ScrollVisibility.Always;

            DefineAssetAllocationList();
        }
        protected override void OnResize(EventArgs e)
        {
            grpData.Width = this.Width - 400;
            grpData.Height = this.Height - 80;

            grpAssetAllocation.Width = this.Width - 432;
            grpAssetAllocation.Height = this.Height - 168;

            grdAssetAllocation.Width = this.Width - 460;
            grdAssetAllocation.Height = grpAssetAllocation.Height - 50;

            panEdit.Left = (Screen.PrimaryScreen.Bounds.Width - panEdit.Width) / 2;
            panEdit.Top = (Screen.PrimaryScreen.Bounds.Height - panEdit.Height) / 2;
        }

        void gridView1_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            GridView currentView = sender as GridView;
            if (e.Column.FieldName == "DebitBalance")
            {
                if (Convert.ToSingle(currentView.GetRowCellValue(e.RowHandle, "DebitBalance")) == 0)
                { e.Appearance.BackColor = System.Drawing.Color.LightCoral; e.Appearance.ForeColor = System.Drawing.Color.LightCoral; }
                else e.Appearance.ForeColor = System.Drawing.Color.Transparent;
            }

            if (e.Column.FieldName == "AssetAllocation")
            {
                if (Convert.ToSingle(currentView.GetRowCellValue(e.RowHandle, "AssetAllocation")) == 0)
                { e.Appearance.BackColor = System.Drawing.Color.LightCoral; e.Appearance.ForeColor = System.Drawing.Color.LightCoral; }
                else e.Appearance.ForeColor = System.Drawing.Color.Transparent;
            }

            if (e.Column.FieldName == "ContractualLimits")
                if (Convert.ToSingle(currentView.GetRowCellValue(e.RowHandle, "ContractualLimits")) == 0) { e.Appearance.BackColor = System.Drawing.Color.LightCoral; e.Appearance.ForeColor = System.Drawing.Color.Transparent; }

            if (e.Column.FieldName == "SuitableProducts")
                if (Convert.ToSingle(currentView.GetRowCellValue(e.RowHandle, "SuitableProducts")) == 0) { e.Appearance.BackColor = System.Drawing.Color.LightCoral; e.Appearance.ForeColor = System.Drawing.Color.Transparent; }

            if (e.Column.FieldName == "Leverage")
                if (Convert.ToSingle(currentView.GetRowCellValue(e.RowHandle, "Leverage")) == 0) { e.Appearance.BackColor = System.Drawing.Color.LightCoral; e.Appearance.ForeColor = System.Drawing.Color.Transparent; }
        }
        void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
          
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

        private void DefineAssetAllocationList()
        {
            InvestmentCommetties_AssetAllocation = new clsInvestmentCommetties_AssetAllocation();
            InvestmentCommetties_AssetAllocation.DateControl = DateTime.Now.Date;    // !!!!!!!!!!!!!!!!!!!
            InvestmentCommetties_AssetAllocation.Tipos = 0;
            InvestmentCommetties_AssetAllocation.Profile_ID = 0;
            InvestmentCommetties_AssetAllocation.GetAssetAllocationRecs();
            grdAssetAllocation.DataSource = InvestmentCommetties_AssetAllocation.List;

            GridColumn colAA = gridView1.Columns["AA"];
            colAA.Width = 30;

            GridColumn colDateFrom = gridView1.Columns["DateFrom"];
            colDateFrom.Width = 80;

            GridColumn colDateTo = gridView1.Columns["DateTo"];
            colDateTo.Width = 80;

            GridColumn colTipos_Title = gridView1.Columns["Tipos_Title"];
            colTipos_Title.Caption = "Τύπος";
            colTipos_Title.Width = 200;

            GridColumn colProfile_Title = gridView1.Columns["Profile_Title"];
            colProfile_Title.Caption = "Profile";
            colProfile_Title.Width = 350;

            GridColumn colTitle = gridView1.Columns["Title"];
            colTitle.Width = 150;

            GridColumn colMinValue = gridView1.Columns["MinValue"];
            colMinValue.Caption = "Minimum %";
            colMinValue.Width = 80;

            GridColumn colMainValue = gridView1.Columns["MainValue"];
            colMainValue.Caption = "Value %";
            colMainValue.Width = 80;

            GridColumn colMaxValue = gridView1.Columns["MaxValue"];
            colMaxValue.Caption = "Maximum %";
            colMaxValue.Width = 80;

            GridColumn colID = gridView1.Columns["ID"];
            colID.Visible = false;
            colID.Width = 60;

            GridColumn colTipos = gridView1.Columns["Tipos"];
            colTipos.Visible = false;
            colTipos.Width = 60;

            GridColumn colProfile_ID = gridView1.Columns["Profile_ID"];
            colProfile_ID.Visible = false;
            colProfile_ID.Width = 60;

            GridColumn colRecs_ID = gridView1.Columns["Recs_ID"];
            colRecs_ID.Visible = false;
            colRecs_ID.Width = 60;
        }
        private void tsbAdd_Edit_Click(object sender, EventArgs e)
        {
            iAction = 0;
            dFrom.Value = DateTime.Now.Date;
            dTo.Value = Convert.ToDateTime("2070/12/31");
            cmbTipos.SelectedIndex = 0;
            cmbProfile.SelectedValue = 0;
            txtTitle.Text = "";
            txtMainValue.Text = "0";
            txtMinValue.Text = "0";
            txtMaxValue.Text = "0";
            panEdit.Visible = true;
            dFrom.Focus();
        }
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Edit_AssetAllocation();
        }
        private void tsbEdit_Edit_Click(object sender, EventArgs e)
        {
            Edit_AssetAllocation();
        }
        private void Edit_AssetAllocation()
        {
            iAction = 1;
            lblRecs_ID.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Recs_ID") + "";
            dFrom.Value = Convert.ToDateTime(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DateFrom"));
            dTo.Value = Convert.ToDateTime(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "DateTo"));
            cmbTipos.SelectedIndex = Convert.ToInt16(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Tipos"));
            cmbProfile.SelectedValue = Convert.ToInt16(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Profile_ID"));
            txtTitle.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "Title") + "";
            txtMinValue.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MinValue") + "";
            txtMainValue.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MainValue") + "";
            txtMaxValue.Text = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "MaxValue") + "";
            panEdit.Visible = true;
            dFrom.Focus();
        }

        private void tsbDel_Edit_Click(object sender, EventArgs e)
        {

        }

        private void tsbSave_Edit_Click(object sender, EventArgs e)
        {
            if (iAction == 0)
            {

            }
            else
            {
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Tipos", cmbTipos.SelectedIndex);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Tipos_Title", cmbTipos.Text);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Profile_ID", cmbProfile.SelectedValue);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Profile_Title", cmbProfile.Text);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "Title", txtTitle.Text);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "MinValue", txtMinValue.Text);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "MainValue", txtMainValue.Text);
                gridView1.SetRowCellValue(gridView1.FocusedRowHandle, "MaxValue", txtMaxValue.Text);

                InvestmentCommetties_AssetAllocationRecs = new clsInvestmentCommetties_AssetAllocationRecs();
                InvestmentCommetties_AssetAllocationRecs.Record_ID = Convert.ToInt32(lblRecs_ID.Text);
                InvestmentCommetties_AssetAllocationRecs.GetRecord();
                InvestmentCommetties_AssetAllocationRecs.Title = txtTitle.Text;
                InvestmentCommetties_AssetAllocationRecs.MainValue = Convert.ToSingle(txtMainValue.Text);
                InvestmentCommetties_AssetAllocationRecs.MinValue = Convert.ToSingle(txtMinValue.Text);
                InvestmentCommetties_AssetAllocationRecs.MaxValue = Convert.ToSingle(txtMaxValue.Text);
                InvestmentCommetties_AssetAllocationRecs.EditRecord();
            }
            panEdit.Visible = false;
        }

        private void picClose_Edit_Click(object sender, EventArgs e)
        {
            panEdit.Visible = false;
        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
