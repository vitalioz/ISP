using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Accounting
{
    public partial class frmAcc_InvoicesRF : Form
    {
        int iRightsLevel;
        string sExtra;
        public frmAcc_InvoicesRF()
        {
            InitializeComponent();
        }

        private void frmAcc_InvoicesRF_Load(object sender, EventArgs e)
        {

        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
