using System;
using System.Data;
using System.Windows.Forms;
using Core;

namespace Options
{
    public partial class frmServiceProviderFees2 : Form
    {
        int iAktion, iProduct_ID, iCategory_ID, iStockExchange_ID, iMode;
        string sCurr;
        public frmServiceProviderFees2()
        {
            InitializeComponent();
        }

        private void frmServiceProviderFees2_Load(object sender, EventArgs e)
        {
            lblCompanyMeridio.Text = "Μερίδιο της " + Global.CompanyName;
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
        public string Curr { get { return this.sCurr; } set { this.sCurr = value; } }
        public int Mode { get { return this.iMode; } set { this.iMode = value; } }
    }
}