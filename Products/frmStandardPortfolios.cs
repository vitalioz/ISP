using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Products
{
    public partial class frmStandardPortfolios : Form
    {
        int iRightsLevel;
        string sExtra;
        public frmStandardPortfolios()
        {
            InitializeComponent();
        }

        private void frmStandardPortfolios_Load(object sender, EventArgs e)
        {

        }
        protected override void OnResize(EventArgs e)
        {
            fgList3.Height = this.Height - 156;
        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }
    }
}
