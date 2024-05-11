using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using Core;

namespace Custody
{
    public partial class frmTrx_Reasons : Form
    {
        int i, iAction, iRightsLevel;
        string sExtra;
        clsTrxReasons TrxReasons = new clsTrxReasons();

        public frmTrx_Reasons()
        {
            InitializeComponent();
        }

        private void frmTrx_Reasons_Load(object sender, EventArgs e)
        {
            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgList.RowColChange += new EventHandler(fgList_RowColChange);

            DefineList();

        }
        protected override void OnResize(EventArgs e)
        {
            fgList.Height = this.Height - 92;
        }
        private void DefineList()
        {
            try
            {
                i = 0;
                fgList.Redraw = false;
                fgList.Tree.Column = 0;
                fgList.Rows.Count = 1;

                TrxReasons = new clsTrxReasons();
                TrxReasons.GetList();
                foreach (DataRow dtRow in TrxReasons.List.Rows)
                {
                    i = i + 1;
                    fgList.AddItem(i + "\t" + dtRow["TrxCategory_Title"] + "\t" + dtRow["TrxType_Title"] + "\t" + dtRow["Title"] + "\t" + dtRow["ExecutionAgent"] + "\t" +
                                   dtRow["ExecutionVenue"] + "\t" + dtRow["Custodian"] + "\t" + dtRow["Depository"] + "\t" + dtRow["TaxHome"] + "\t" + dtRow["VAT"] + "\t" +
                                   dtRow["SalesFees"] + "\t" + dtRow["IncomeTax"] + "\t" + dtRow["ID"] + "\t" + dtRow["TrxCategory_ID"] + "\t" + dtRow["TrxType_ID"]);
                }

                fgList.Redraw = true;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { }
        }
        private void ShowRecord()
        {
            txtTitle.Text = fgList[fgList.Row, "Title"] + "";

            TrxReasons = new clsTrxReasons();
            TrxReasons.Record_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
            TrxReasons.GetList();
            txtTitle.Text = TrxReasons.Title;
        }


        private void tsbDelete_Click(object sender, EventArgs e)
        {
            {
                if (MessageBox.Show(Global.GetLabel("attention_you_ask_for_deletion") + "." + "\n" + Global.GetLabel("are_you_sure_for_deletion"), Global.AppTitle,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {

                    if (Convert.ToInt32(fgList[fgList.Row, "ID"]) != 0)
                    {
                        clsSystem System = new clsSystem();
                        System.ExecSQL("DELETE Trx_Reasons WHERE ID = " + fgList[fgList.Row, "ID"]);
                    }

                    txtTitle.Text = "";

                    fgList.RemoveItem(fgList.Row);
                    fgList.Row = 0;
                    if (fgList.Rows.Count <= i) i = fgList.Rows.Count - 1;
                    fgList.Row = i;
                    fgList.Focus();
                }
            }
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            iAction = 0;

        }
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            iAction = 1;
            panEdit.Enabled = true;
            tsbSave.Enabled = true;
            txtTitle.Focus();
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
        private void fgList_RowColChange(object sender, EventArgs e)
        {
            iAction = 1;
            panEdit.Enabled = false;
            tsbSave.Enabled = false;

            if (fgList.Row > 0) ShowRecord();

        }
        private void tsbSave_Click(object sender, EventArgs e)
        {

        }
        public int RightsLevel { get { return iRightsLevel; } set { iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
