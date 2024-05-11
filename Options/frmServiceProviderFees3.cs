using System;
using System.Windows.Forms;
using Core;

namespace Options
{
    public partial class frmServiceProviderFees3 : Form
    {
        int iAktion, iInvestmentProfile_ID, iInvestmentPolicy_ID;
        bool bDefineList;
        public frmServiceProviderFees3()
        {
            InitializeComponent();
        }

        private void frmServiceProviderFees3_Load(object sender, EventArgs e)
        {
            bDefineList = false;

            cmbInvestmentProfile.DataSource = Global. dtCustomersProfiles.Copy();
            cmbInvestmentProfile.DisplayMember = "Title";
            cmbInvestmentProfile.ValueMember = "ID";

            cmbInvestmentPolicy.DataSource = Global.dtInvestPolicies.Copy();
            cmbInvestmentPolicy.DisplayMember = "Title";
            cmbInvestmentPolicy.ValueMember = "ID";

            bDefineList = true;

        if (iAktion == 1) {
                cmbInvestmentProfile.SelectedValue = iInvestmentProfile_ID;
                cmbInvestmentPolicy.SelectedValue = iInvestmentPolicy_ID;
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
        public int InvestmentProfile_ID { get { return this.iInvestmentProfile_ID; } set { this.iInvestmentProfile_ID = value; } }
        public int InvestmentPolicy_ID { get { return this.iInvestmentPolicy_ID; } set { this.iInvestmentPolicy_ID = value; } }
    }
}
