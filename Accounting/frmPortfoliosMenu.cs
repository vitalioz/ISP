using System;

using System.Windows.Forms;

namespace Accounting
{
    public partial class frmPortfoliosMenu : Form
    {
        public frmPortfoliosMenu()
        {
            InitializeComponent();
        }

        private void btnGAP_Click(object sender, EventArgs e)
        {
            frmGenikoLogistikoSxedio locGenikoLogistikoSxedio = new frmGenikoLogistikoSxedio();
            locGenikoLogistikoSxedio.RightsLevel = 1;   // Convert.ToInt32(tokens[1]);
            locGenikoLogistikoSxedio.Extra = "";        // tokens[2];
            locGenikoLogistikoSxedio.Show();
        }
        private void btnAccTrx_Click(object sender, EventArgs e)
        {
            frmAcc_Trx locAcc_Trx = new frmAcc_Trx();
            locAcc_Trx.RightsLevel = 1;
            locAcc_Trx.Show();
        }
        private void btnPortfolio_Monitoring_Click(object sender, EventArgs e)
        {
            frmPortfolio_Monitoring locPortfolio_Monitoring = new frmPortfolio_Monitoring();
            locPortfolio_Monitoring.RightsLevel = 1;
            locPortfolio_Monitoring.Show();
        }
        private void btnPortfolio_Click(object sender, EventArgs e)
        {
            frmPortfolio locPortfolio = new frmPortfolio();
            locPortfolio.DateControl = DateTime.Now.Date;
            locPortfolio.CDP_ID = 0;
            locPortfolio.Contracts_Balances_ID = 0;
            locPortfolio.RightsLevel = 1;
            locPortfolio.Show();
        }    
        private void button7_Click(object sender, EventArgs e)
        {
            frmPortfolio_Planning locPortfolio_Planning = new frmPortfolio_Planning();
            locPortfolio_Planning.Show();
        }


    }
}
