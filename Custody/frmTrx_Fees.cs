using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using Core;

namespace Custody
{
    public partial class frmTrx_Fees : Form
    {
        int i, j, iAction, iRow, iView_ID, iRightsLevel;
        string sExtra;
        clsTrxFees TrxFees = new clsTrxFees();
        clsSystem clsSystem = new clsSystem();
        public frmTrx_Fees()
        {
            InitializeComponent();
        }

        private void frmTrx_Fees_Load(object sender, EventArgs e)
        {
            //-------------- Define Trx_Categories List ------------------
            clsSystem = new clsSystem();
            clsSystem.GetTrx_Categories();
            cmbTrxCategory.DataSource = clsSystem.List.Copy();
            cmbTrxCategory.DisplayMember = "Title";
            cmbTrxCategory.ValueMember = "ID";

            //-------------- Define Trx_Types List ------------------
            clsSystem = new clsSystem();
            clsSystem.GetTrx_Types();
            cmbTrxType.DataSource = clsSystem.List.Copy();
            cmbTrxType.DisplayMember = "Title";
            cmbTrxType.ValueMember = "ID";

            //-------------- Define Trx_Etiology List ------------------
            clsSystem = new clsSystem();
            clsSystem.GetTrx_Etiology();
            cmbTrxEtiology.DataSource = clsSystem.List.Copy();
            cmbTrxEtiology.DisplayMember = "Title";
            cmbTrxEtiology.ValueMember = "ID";

            //-------------- Define TrxClientsFees List ------------------
            clsSystem = new clsSystem();
            clsSystem.GetTrx_ClientsFees();
            cmbTrxClientsFees.DataSource = clsSystem.List.Copy();
            cmbTrxClientsFees.DisplayMember = "Title";
            cmbTrxClientsFees.ValueMember = "ID";

            //-------------- Define Trx_Earnings List ------------------
            clsSystem = new clsSystem();
            clsSystem.GetTrx_Earnings();
            cmbEarnings.DataSource = clsSystem.List.Copy();
            cmbEarnings.DisplayMember = "Title";
            cmbEarnings.ValueMember = "ID";

            //-------------- Define Trx_Revenue List ------------------
            clsSystem = new clsSystem();
            clsSystem.GetTrx_Revenue();
            cmbRevenue.DataSource = clsSystem.List.Copy();
            cmbRevenue.DisplayMember = "Title";
            cmbRevenue.ValueMember = "ID";

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgList.DoubleClick += new System.EventHandler(fgList_DoubleClick);

            DefineList();

        }
        protected override void OnResize(EventArgs e)
        {
            fgList.Height = this.Height - 88;
            fgList.Width = this.Width - 32;
        }
        private void DefineList()
        {
            try
            {
                i = 0;
                fgList.Redraw = false;
                fgList.Tree.Column = 0;
                fgList.Rows.Count = 1;

                TrxFees = new clsTrxFees();
                TrxFees.GetList();
                foreach (DataRow dtRow in TrxFees.List.Rows)
                {
                    i = i + 1;
                    fgList.AddItem(i + "\t" + dtRow["TrxCategory_Title"] + "\t" + dtRow["TrxType_Title"] + "\t" + dtRow["TrxEtiology_Title"] + "\t" + dtRow["TrxClientsFees_Title"] + "\t" +
                                   dtRow["Earnings_Title"] + "\t" + dtRow["Revenue_Title"] + "\t" + dtRow["Notes"] + "\t" + dtRow["ID"] + "\t" + dtRow["TrxCategory_ID"] + "\t" + 
                                   dtRow["TrxType_ID"] + "\t" + dtRow["TrxEtiology_ID"] + "\t" + dtRow["TrxClientsFees_ID"] + "\t" + dtRow["Earnings_ID"] + "\t" + dtRow["Revenue_ID"]);
                }

                fgList.Redraw = true;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { }
        }
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            iAction = 0;
            iView_ID = Convert.ToInt32(fgList[fgList.Row, "AA"]);
            tsbSave.Enabled = true;
            panEdit.Visible = true;
            cmbTrxCategory.Focus();
        }
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            EditRow();
        }
        private void fgList_DoubleClick(object sender, EventArgs e)
        {
            EditRow();
        }
        private void EditRow()
        {
            iRow = fgList.Row;
            if (iRow > 0)
            {
                iAction = 1;
                cmbTrxCategory.SelectedValue = Convert.ToInt32(fgList[iRow, "TrxCategory_ID"]);
                cmbTrxType.SelectedValue = Convert.ToInt32(fgList[iRow, "TrxType_ID"]);
                cmbTrxEtiology.SelectedValue = Convert.ToInt32(fgList[iRow, "TrxEtiology_ID"]);
                cmbTrxClientsFees.SelectedValue = Convert.ToInt32(fgList[iRow, "TrxClientsFees_ID"]);
                cmbEarnings.SelectedValue = Convert.ToInt32(fgList[iRow, "Earnings_ID"]);
                cmbRevenue.SelectedValue = Convert.ToInt32(fgList[iRow, "Revenue_ID"]);
                txtNotes.Text = fgList[iRow, "Notes"] + "";

                tsbSave.Enabled = true;
                panEdit.Visible = true;
                cmbTrxCategory.Focus();
            }
        }
        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Global.GetLabel("attention_you_ask_for_deletion") + "." + "\n" + Global.GetLabel("are_you_sure_for_deletion"), Global.AppTitle,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                if (Convert.ToInt32(fgList[fgList.Row, "ID"]) != 0)
                {
                    TrxFees = new clsTrxFees();
                    TrxFees.Record_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                    TrxFees.DeleteRecord();
                    TrxFees.Edit_View_ID();
                    DefineList();
                    fgList.Focus();
                }                
            }
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
            EXL.Cells[1, 3].Value = "Λίστα";
            var loopTo = fgList.Rows.Count - 1;
            for (i = 0; i <= loopTo; i++)
            {
                EXL.Cells[i + 2, 1].Value = fgList[i, 0];
            }

            EXL.ScreenUpdating = true;
            EXL.Visible = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
        }
        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (iAction == 0)
            {
                TrxFees = new clsTrxFees();
                TrxFees.TrxCategory_ID = Convert.ToInt32(cmbTrxCategory.SelectedValue);
                TrxFees.TrxType_ID = Convert.ToInt32(cmbTrxType.SelectedValue);
                TrxFees.TrxEtiology_ID = Convert.ToInt32(cmbTrxEtiology.SelectedValue);
                TrxFees.TrxClientsFees_ID = Convert.ToInt32(cmbTrxClientsFees.SelectedValue);
                TrxFees.Earnings_ID = Convert.ToInt32(cmbEarnings.SelectedValue);
                TrxFees.Revenue_ID = Convert.ToInt32(cmbRevenue.SelectedValue);
                TrxFees.View_ID = iView_ID;
                TrxFees.Notes = txtNotes.Text;
                j = TrxFees.InsertRecord();

                TrxFees = new clsTrxFees();
                TrxFees.Edit_View_ID();
            }
            else
            {
                TrxFees = new clsTrxFees();
                TrxFees.Record_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                TrxFees.GetRecord();
                TrxFees.TrxCategory_ID = Convert.ToInt32(cmbTrxCategory.SelectedValue);
                TrxFees.TrxType_ID = Convert.ToInt32(cmbTrxType.SelectedValue);
                TrxFees.TrxEtiology_ID = Convert.ToInt32(cmbTrxEtiology.SelectedValue);
                TrxFees.TrxClientsFees_ID = Convert.ToInt32(cmbTrxClientsFees.SelectedValue);
                TrxFees.Earnings_ID = Convert.ToInt32(cmbEarnings.SelectedValue);
                TrxFees.Revenue_ID = Convert.ToInt32(cmbRevenue.SelectedValue);
                TrxFees.View_ID = Convert.ToInt32(fgList[fgList.Row, "AA"]);
                TrxFees.Notes = txtNotes.Text;
                TrxFees.EditRecord();
                j = iRow;
            }

            DefineList();
            iRow = fgList.FindRow(j.ToString(), 1, 8, false);
            if (iRow > 0) fgList.Row = iRow;
            fgList.Focus();

            panEdit.Visible = false;
        }

        private void picClose_Import_Click(object sender, EventArgs e)
        {
            panEdit.Visible = false;
        }
        public int RightsLevel { get { return iRightsLevel; } set { iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
