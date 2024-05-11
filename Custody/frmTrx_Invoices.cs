using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Core;

namespace Custody
{
    public partial class frmTrx_Invoices : Form
    {
        int i, iAction, iRightsLevel;
        string sExtra;
        C1.Win.C1FlexGrid.CellRange rng;
        clsTrxInvoices TrxInvoices = new clsTrxInvoices();

        public frmTrx_Invoices()
        {
            InitializeComponent();
        }

        private void frmTrx_Invoices_Load(object sender, EventArgs e)
        {
            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgList.RowColChange += new EventHandler(fgList_RowColChange);
            fgList.AllowMerging = C1.Win.C1FlexGrid.AllowMergingEnum.FixedOnly;
            fgList.Rows[0].AllowMerging = true;

            fgList.Cols[0].AllowMerging = true;
            rng = fgList.GetCellRange(0, 0, 1, 0);
            rng.Data = "ΑΑ";

            fgList.Cols[1].AllowMerging = true;
            rng = fgList.GetCellRange(0, 1, 1, 1);
            rng.Data = "Γενική Κατηγορία";

            fgList.Cols[2].AllowMerging = true;
            rng = fgList.GetCellRange(0, 2, 1, 2);
            rng.Data = "Είδος Κινησης";

            fgList.Cols[3].AllowMerging = true;
            rng = fgList.GetCellRange(0, 3, 1, 3);
            rng.Data = "Αιτιολογια ISP";

            fgList.Cols[4].AllowMerging = true;
            rng = fgList.GetCellRange(0, 4, 1, 4);
            rng.Data = "Αιτιολογια EFFECT";

            fgList.Cols[5].AllowMerging = true;
            rng = fgList.GetCellRange(0, 5, 1, 5);
            rng.Data = "Τύπος Προϊόντος";

            fgList.Cols[6].AllowMerging = true;
            rng = fgList.GetCellRange(0, 6, 1, 6);
            rng.Data = "Φόρμα Εκτύπωσης";

            rng = fgList.GetCellRange(0, 7, 0, 8);
            rng.Data = "Πελάτη 1";
            fgList[1, 7] = "Κατηγορία";
            fgList[1, 8] = "Λεπτομέρεις";

            rng = fgList.GetCellRange(0, 9, 0, 10);
            rng.Data = "Πελάτη 2";
            fgList[1, 9] = "Κατηγορία";
            fgList[1, 10] = "Λεπτομέρεις";
            fgList.Cols[0].AllowMerging = true;

            fgList.Cols[11].AllowMerging = true;
            rng = fgList.GetCellRange(0, 11, 1, 11);
            rng.Data = "Σχόλια";

            DefineList();

        }
        protected override void OnResize(EventArgs e)
        {
            fgList.Height = this.Height - 92;
            fgList.Width = this.Width - 44;
        }
        private void DefineList()
        {
            try
            {
                i = 0;
                fgList.Redraw = false;
                fgList.Tree.Column = 0;
                fgList.Rows.Count = 2;

                TrxInvoices = new clsTrxInvoices();
                TrxInvoices.GetList();
                foreach (DataRow dtRow in TrxInvoices.List.Rows)
                {
                    i = i + 1;
                    fgList.AddItem(i + "\t" + dtRow["TrxCategory_Title"] + "\t" + dtRow["TrxType_Title"] + "\t" + dtRow["Title_ISP"] + "\t" + dtRow["Title_Effect"] + "\t" +
                                   dtRow["ProductType_ID"] + "\t" + dtRow["Invoice_Template"] + "\t" + dtRow["ClientType1_ID"] + "\t" + dtRow["ClientType1_Details"] + "\t" + 
                                   dtRow["ClientType2_ID"] + "\t" + dtRow["ClientType2_Details"] + "\t" + dtRow["Notes"] + "\t" + dtRow["ID"] + "\t" + 
                                   dtRow["TrxCategory_ID"] + "\t" + dtRow["TrxType_ID"]);
                }

                fgList.Redraw = true;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { }
        }
        private void ShowRecord()
        {
            txtTitle.Text = fgList[fgList.Row, "Title"] + "";

            TrxInvoices = new clsTrxInvoices();
            TrxInvoices.Record_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
            TrxInvoices.GetList();
            txtTitle.Text = TrxInvoices.Title_ISP;
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
                        System.ExecSQL("DELETE Trx_Invoices WHERE ID = " + fgList[fgList.Row, "ID"]);
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
