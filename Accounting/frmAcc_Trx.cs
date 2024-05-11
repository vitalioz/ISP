using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using Core;

namespace Accounting
{
    public partial class frmAcc_Trx : Form
    {
        int iRightsLevel;
        clsAccountingTrx AccountingTrx = new clsAccountingTrx();
        public frmAcc_Trx()
        {
            InitializeComponent();
        }

        private void frmAcc_Trx_Load(object sender, EventArgs e)
        {
            dDateControl.Value = DateTime.Now.Date;

            gridView1 = grdList.MainView as GridView;
            //gridView1.FocusedRowObjectChanged += gridView1_FocusedRowObjectChanged;
            //gridView1.DoubleClick += gridView1_DoubleClick;
            //gridView1.RowCellStyle += new RowCellStyleEventHandler(gridView1_RowCellStyle);
            gridView1.HorzScrollVisibility = ScrollVisibility.Always;
        }
        protected override void OnResize(EventArgs e)
        {
            panCritiries.Width = this.Width - 30;
            btnSearch.Left = panCritiries.Width - 120;

            panTools.Width = this.Width - 30;

            grdList.Width = this.Width - 30;
            grdList.Height = this.Height - 146;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DefineList();
        }
        private void DefineList()
        {
            AccountingTrx = new clsAccountingTrx();
            AccountingTrx.DateIns = dDateControl.Value.Date;
            AccountingTrx.GetList();
            grdList.DataSource = AccountingTrx.List;

            GridColumn colAA = gridView1.Columns["AA"];
            colAA.Width = 30;

            GridColumn colTrxDate = gridView1.Columns["TrxDate"];
            colTrxDate.Width = 70;

            GridColumn colValeur = gridView1.Columns["Valeur"];
            colValeur.Width = 70;

            GridColumn colDateIns = gridView1.Columns["DateIns"];
            colDateIns.Width = 70;

            GridColumn colCode = gridView1.Columns["Code"];
            colCode.Caption = "Λογαριασμός";
            colCode.Width = 200;

            GridColumn colTitle = gridView1.Columns["Title"];
            colTitle.Caption = "Περιγραφή Λογαριασμού";
            colTitle.Width = 300;

            GridColumn colDebit = gridView1.Columns["Debit"];
            colDebit.Caption = "Χρέωση";
            colDebit.Width = 100;

            GridColumn colCredit = gridView1.Columns["Credit"];
            colCredit.Caption = "Πίστωση";
            colCredit.Width = 100;

            GridColumn colReferenceNo = gridView1.Columns["ReferenceNo"];
            colReferenceNo.Caption = "Παραστατικό";
            colReferenceNo.Width = 120;

            GridColumn colDescription = gridView1.Columns["Description"];
            colDescription.Caption = "Αιτιολογία";
            colDescription.Width = 200;

            GridColumn colID = gridView1.Columns["ID"];
            colID.Width = 30;
        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
 
    }
}
