using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;


namespace Custody
{
    public partial class frmTrx_Edit : Form
    {
        int iRec_ID, iMode;
        public frmTrx_Edit()
        {
            InitializeComponent();
        }
        public int Rec_ID { get { return this.iRec_ID; } set { this.iRec_ID = value; } }
        public int Mode { get { return this.iMode; } set { this.iMode = value; } }                                  // IN: 0 - Edit Mode, 2 - SecuritiesCheck mode     OUT: 1 - Save & Exit,   2 - Show only
        //public int BusinessType { get { return this.iBusinessType; } set { this.iBusinessType = value; } }
        //public int LastAktion { get { return this.iLastAktion; } set { this.iLastAktion = value; } }
        //public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
       //public int Editable { get { return this.iEditable; } set { this.iEditable = value; } }
    }
}
