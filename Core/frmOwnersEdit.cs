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
    public partial class frmOwnersEdit : Form
    {
        int iLastAktion = 0, iRec_ID, iClient_ID, iCode_ID;
        string sCode, sDOY, sAFM;
        public frmOwnersEdit()
        {
            InitializeComponent();
            ucCS.StartInit(340, 240, 336, 20, 1);
        }

        private void frmOwnersEdit_Load(object sender, EventArgs e)
        {
            ucCS.TextOfLabelChanged += new EventHandler(ucCS_TextOfLabelChanged);
            ucCS.Filters = "Status = 1 AND Tipos < 3";             // Status = 0 - Cancelled, Status = 1 - Αctive       Tipos = 1 - idiotis, 2 - company, 3- join
            ucCS.ListType = 1;
            //ucCS.ShowClientsList = false;
            //ucCS.txtClientName.Text = "";
            //ucCS.ShowClientsList = true;

            if (ucCS.txtClientName.Text != "")
            {
                cmbDOY.Text = sDOY + "";
                cmbAFM.Text = sAFM + "";
            }
        }
        protected void ucCS_TextOfLabelChanged(object sender, EventArgs e)
        {
            iClient_ID = Convert.ToInt32(ucCS.Client_ID.Text);
            clsClients Clients = new clsClients();
            Clients.Record_ID = iClient_ID;
            Clients.EMail = "";
            Clients.Mobile = "";
            Clients.AFM = "";
            Clients.DoB = Convert.ToDateTime("1900/01/01");
            Clients.GetRecord();
            Define_DOY_AFM_Lists(Clients.DOY, Clients.DOY2, Clients.AFM, Clients.AFM2);
            txtFather.Text = Clients.FirstnameFather;
            txtADT.Text = Clients.ADT;
            txtPassport.Text = Clients.Passport;
            cmbDOY.Text = Clients.DOY;
            cmbAFM.Text = Clients.AFM;
            lblBorn.Text = Clients.DoB.ToString();
            lblSpecial.Text = Clients.Spec_Title;
        }
        private void Define_DOY_AFM_Lists(string sDOY1, string sDOY2, string sAFM1, string sAFM2)
        { 
            cmbDOY.Items.Clear();
            cmbDOY.Items.Add(sDOY1);
            cmbDOY.Items.Add(sDOY2);

            cmbAFM.Items.Clear();
            cmbAFM.Items.Add(sAFM1);
            cmbAFM.Items.Add(sAFM2);
        }
        public int LastAktion { get { return this.iLastAktion; } set { this.iLastAktion = value; } }
        public int Rec_ID { get { return this.iRec_ID; } set { this.iRec_ID = value; } }

        private void btnSave_Click(object sender, EventArgs e)
        {
            iLastAktion = 1;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            iLastAktion = 0;
            this.Close();
        }

        public int Client_ID { get { return this.iClient_ID; } set { this.iClient_ID = value; } }
        public int Code_ID { get { return this.iCode_ID; } set { this.iCode_ID = value; } }
        public string Code { get { return this.sCode; } set { this.sCode = value; } }
        public string DOY { get { return this.sDOY; } set { this.sDOY = value; } }
        public string AFM { get { return this.sAFM; } set { this.sAFM = value; } }

    }
}
