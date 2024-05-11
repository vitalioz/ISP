using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Core
{
    public partial class frmIntroducerContract : Form
    {
        int iFinishAktion;
        public frmIntroducerContract()
        {
            InitializeComponent();
        }

        private void frmIntroducerContract_Load(object sender, EventArgs e)
        {

        }
        public int FinishAktion { get { return this.iFinishAktion; } set { this.iFinishAktion = value; } }
    }
}
