using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Core
{
    public partial class ucProducts_Rates : UserControl
    {
        int iMode, iAction;
        public ucProducts_Rates()
        {
            InitializeComponent();
        }

        private void ucProducts_Rates_Load(object sender, EventArgs e)
        {

        }
        public void ShowRecord(int iShare_ID, int iShareTitle_ID, int iShareCode_ID, int iRightsLevel)
        {

        }
        public void AddRecord()
        {

        }
        public void EditRecord()
        {
            iAction = 1;                      // 1 - EDIT Mode
            tsbSave.Enabled = true;
            txtRateTitle.Focus();
        }
        public int Mode { get { return this.iMode; } set { this.iMode = value; } }
    }
}
