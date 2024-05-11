using System;
using System.Windows.Forms;
using System.Drawing;

namespace Core
{
    public partial class frmClientData : Form
    {
        int iClient_ID, iClient_Type;
        public frmClientData()
        {
            InitializeComponent();
        }

        private void frmClientData_Load(object sender, EventArgs e)
        {
            clsClients klsClient = new clsClients();
            klsClient.Record_ID = iClient_ID;
            klsClient.EMail = "";
            klsClient.Mobile = "";
            klsClient.AFM = "";
            klsClient.DoB = Convert.ToDateTime("1900/01/01");
            klsClient.GetRecord();
            iClient_Type = klsClient.Type;

            // iClient_Type = 1 - Fisiko prosopo, 2- Nomiko Prosopo
            switch (iClient_Type)
            {
                case 1:
                    ucCD.panFP.Visible = true;
                    ucCD.panNP.Visible = false;
                    ucCD.tabClientData.TabPages[1].BackColor = Color.LightBlue;
                    ucCD.tabClientData.TabPages[2].BackColor = Color.LightBlue;
                    break;
                case 2:
                    ucCD.panFP.Visible = false;
                    ucCD.panNP.Visible = true;
                    ucCD.tabClientData.TabPages[1].BackColor = Color.FromArgb(192, 255, 192);
                    ucCD.tabClientData.TabPages[2].BackColor = Color.FromArgb(192, 255, 192);
                    break;
            }
            ucCD.ShowRecord(iClient_ID, 1, 1);
        }

        public int Client_ID { get { return this.iClient_ID; } set { this.iClient_ID = value; } }
    }
}
