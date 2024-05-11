using System;
using System.Data;
using System.Windows.Forms;
using C1.Win.C1FlexGrid;
using Core;

namespace Options
{
    public partial class frmOptions : Form
    {
        int iRightsLevel;
        string sExtra;
        clsOptions Options = new clsOptions();
        public frmOptions()
        {
            InitializeComponent();
        }

        private void frmOptions_Load(object sender, EventArgs e)
        {
            Options = new clsOptions();
            Options.GetRecord();

            txtTitle.Text = Options.Title;
            txtAddress.Text = Options.Address;
            txtDOY.Text = Options.DOY;
            txtAFM.Text = Options.AFM;
            txtLEI.Text = Options.LEI;

            txtSMTP.Text = Options.SMTP;
            txtEMailAddress.Text = Options.EMail_Sender;
            txtEMailUsername.Text = Options.EMail_Username;
            txtEMailPassword.Text = Options.EMail_Password;
            txtUsername.Text = Options.SMS_Username;
            txtPassword.Text = Options.SMS_Password;
            txtFrom.Text = Options.SMS_From;
            txtFTP_Username.Text = Options.FTP_Username;
            txtFTP_Password.Text = Options.FTP_Password;
            txtFIX_DB.Text = Options.FIX_DB_Server_Path;

            cmbPrinters.Text = Options.InvoicePrinter;


            cmbInvoiceFisiko.SelectedValue = Options.InvoiceFisiko;

            cmbInvoiceNomiko.SelectedValue = Options.InvoiceNomiko;


            cmbInvoicePistotikoFisiko.SelectedValue = Options.InvoicePistotikoFisiko;
 
            cmbInvoicePistotikoNomiko.SelectedValue = Options.InvoicePistotikoNomiko;

            cmbInvoiceAkyrotiko.SelectedValue = Options.InvoiceAkyrotiko;

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            Options = new clsOptions();
            Options.GetRecord();
            Options.FIX_DB_Server_Path = txtFIX_DB.Text;
            Options.EditRecord();
        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public string Extra { get { return this.sExtra; } set { this.sExtra = value; } }

    }
}
