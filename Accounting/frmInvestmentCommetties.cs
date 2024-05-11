using System;
using System.Data;
using System.Windows.Forms;
using Core;

namespace Accounting
{
    public partial class frmInvestmentCommetties : Form
    {
        int i, iAction;
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

            //------- fgAssetAllocation ----------------------------
            fgAssetAllocation.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgAssetAllocation.Styles.Focus.BackColor = Global.GridHighlightForeColor;
            fgAssetAllocation.Styles.ParseString(Global.GridStyle);
            fgAssetAllocation.DoubleClick += new System.EventHandler(fgAssetAllocation_DoubleClick);

            DefineAssetAllocationList();
        }
        protected override void OnResize(EventArgs e)
        {
            grpData.Width = this.Width - 400;
            grpData.Height = this.Height - 80;

            grpAssetAllocation.Width = this.Width - 432;
            grpAssetAllocation.Height = this.Height - 168;

            fgAssetAllocation.Width = this.Width - 536;
            fgAssetAllocation.Height = grpAssetAllocation.Height - 48;
        }
        private void DefineAssetAllocationList()
        {
            fgAssetAllocation.Redraw = false;
            fgAssetAllocation.Rows.Count = 1;

            InvestmentCommetties_AssetAllocation = new clsInvestmentCommetties_AssetAllocation();
            InvestmentCommetties_AssetAllocation.DateControl = DateTime.Now.Date;    // !!!!!!!!!!!!!!!!!!!
            InvestmentCommetties_AssetAllocation.Tipos = 0;
            InvestmentCommetties_AssetAllocation.Profile_ID = 0;
            InvestmentCommetties_AssetAllocation.GetAssetAllocationRecs();
            foreach (DataRow dtRow in InvestmentCommetties_AssetAllocation.List.Rows)
            {
                fgAssetAllocation.AddItem(dtRow["DateFrom"] + "\t" + dtRow["DateTo"] + "\t" + sTipos[Convert.ToInt16(dtRow["Tipos"])] + "\t" + dtRow["Profile_Title"] + "\t" + 
                                          dtRow["Title"] + "\t" + dtRow["MainValue"] + "\t" + dtRow["MinValue"] + "\t" + dtRow["MaxValue"] + "\t" + dtRow["ID"] + "\t" + 
                                          dtRow["Recs_ID"] + "\t" + dtRow["Tipos"] + "\t" + dtRow["Profile_ID"]);
            }
            fgAssetAllocation.Redraw = true;

        }
        private void fgAssetAllocation_DoubleClick(object sender, EventArgs e)
        {
            Edit_AssetAllocation();
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

        private void tsbEdit_Edit_Click(object sender, EventArgs e)
        {
            Edit_AssetAllocation();
        }
        private void Edit_AssetAllocation()
        {
            i = fgAssetAllocation.Row;
            if (i > 0)
            {
                iAction = 1;
                dFrom.Value = Convert.ToDateTime(fgAssetAllocation[i, "DateFrom"]);
                dTo.Value = Convert.ToDateTime(fgAssetAllocation[i, "DateTo"]);
                cmbTipos.SelectedIndex = Convert.ToInt16(fgAssetAllocation[i, "Tipos"]);
                cmbProfile.SelectedValue = Convert.ToInt16(fgAssetAllocation[i, "Profile_ID"]);
                txtTitle.Text = fgAssetAllocation[i, "Title"] + "";
                txtMainValue.Text = fgAssetAllocation[i, "MainValue"] + "";
                txtMinValue.Text = fgAssetAllocation[i, "MinValue"] + "";
                txtMaxValue.Text = fgAssetAllocation[i, "MaxValue"] + "";
                panEdit.Visible = true;
                dFrom.Focus();
            }
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
                i = fgAssetAllocation.Row;
                fgAssetAllocation[i, "DateFrom"] = dFrom.Value.ToString("dd/MM/yyyy");
                fgAssetAllocation[i, "DateTo"] = dTo.Value.ToString("dd/MM/yyyy");
                fgAssetAllocation[i, "Tipos_Title"] = cmbTipos.Text;
                fgAssetAllocation[i, "Tipos"] = cmbTipos.SelectedIndex;
                fgAssetAllocation[i, "Profile_Title"] = cmbProfile.Text;
                fgAssetAllocation[i, "Profile_ID"] = cmbProfile.SelectedValue;
                fgAssetAllocation[i, "Title"] = txtTitle.Text;
                fgAssetAllocation[i, "MainValue"] = txtMainValue.Text;
                fgAssetAllocation[i, "MaxValue"] = txtMaxValue.Text;
                fgAssetAllocation[i, "MinValue"] = txtMinValue.Text;

                InvestmentCommetties_AssetAllocationRecs = new clsInvestmentCommetties_AssetAllocationRecs();
                InvestmentCommetties_AssetAllocationRecs.Record_ID = Convert.ToInt32(fgAssetAllocation[i, "Recs_ID"]);
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
    }
}
