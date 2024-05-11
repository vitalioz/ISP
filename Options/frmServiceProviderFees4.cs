using System;
using System.Windows.Forms;
using Core;

namespace Options
{
    public partial class frmServiceProviderFees4 : Form
    {
        int iAktion, iFinanceTools_ID;
        string sFeesCurrency;
        bool bDefineList;
        public frmServiceProviderFees4()
        {
            InitializeComponent();
        }

        private void frmServiceProviderFees4_Load(object sender, EventArgs e)
        {
            bDefineList = false;

            //--- define FinanceTools List ------------------
            cmbFinanceTools.DataSource = Global.dtFinanceTools.Copy();
            cmbFinanceTools.DisplayMember = "Title";
            cmbFinanceTools.ValueMember = "ID";

            //--- define FeesCurrencies List ------------------
            cmbFeesCurrencies.DataSource = Global.dtCurrencies.Copy();
            cmbFeesCurrencies.DisplayMember = "Title";
            cmbFeesCurrencies.ValueMember = "ID";

            bDefineList = true;

            if (iAktion == 1)
            {
                cmbFinanceTools.SelectedValue = iFinanceTools_ID;
                cmbFeesCurrencies.Text = sFeesCurrency;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            iAktion = 1;             // was saved (added)
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            iAktion = 0;             // don't saved (cancelled)
            this.Close();
        }
        public int Aktion { get { return this.iAktion; } set { this.iAktion = value; } }
        public int FinanceTools_ID { get { return this.iFinanceTools_ID; } set { this.iFinanceTools_ID = value; } }
        public string FeesCurrency { get { return this.sFeesCurrency; } set { this.sFeesCurrency = value; } }
    }
}
