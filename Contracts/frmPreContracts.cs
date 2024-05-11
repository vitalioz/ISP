using System;
using System.Windows.Forms;

namespace Contracts
{
    public partial class frmPreContracts : Form
    {
        int iRightsLevel;
        string sExtra;
        public frmPreContracts()
        {
            InitializeComponent();
        }

        private void frmPreContracts_Load(object sender, EventArgs e)
        {

        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
