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
    public partial class frmOptions : Form
    {
        int iAktion, iRightsLevel;
        string sVisualFlags;
        public frmOptions()
        {
            InitializeComponent();
        }

        private void frmOptions_Load(object sender, EventArgs e)
        {
            cmbInvoiceFisiko.DataSource = Global.dtInvoicesTypes.Copy();
            cmbInvoiceFisiko.DisplayMember = "Title";
            cmbInvoiceFisiko.ValueMember = "ID";

            cmbInvoiceNomiko.DataSource = Global.dtInvoicesTypes.Copy();
            cmbInvoiceNomiko.DisplayMember = "Title";
            cmbInvoiceNomiko.ValueMember = "ID";

            cmbInvoicePistotikoFisiko.DataSource = Global.dtInvoicesTypes.Copy();
            cmbInvoicePistotikoFisiko.DisplayMember = "Title";
            cmbInvoicePistotikoFisiko.ValueMember = "ID";

            cmbInvoicePistotikoNomiko.DataSource = Global.dtInvoicesTypes.Copy();
            cmbInvoicePistotikoNomiko.DisplayMember = "Title";
            cmbInvoicePistotikoNomiko.ValueMember = "ID";

            cmbInvoiceAkyrotiko.DataSource = Global.dtInvoicesTypes.Copy();
            cmbInvoiceAkyrotiko.DisplayMember = "Title";
            cmbInvoiceAkyrotiko.ValueMember = "ID";

            //--- define general options -----------------------------
            clsOptions Options = new clsOptions();
            Options.GetRecord();

            txtTitle.Text = Options.Title;
            txtAddress.Text = Options.Address;
            txtDOY.Text = Options.DOY;
            txtAFM.Text = Options.AFM;
            txtLEI.Text = Options.LEI;
            //txtReportFilesPath.Text = Options.ReportFilesPath;
            //chkFixedSizes.Checked = (Options.FixedSizes == 1 ? true : false);
            txtSMTP.Text = Options.SMTP;
            txtEMailAddress.Text = Options.EMail_Sender;
            txtEMailUsername.Text = Options.EMail_Username;
            txtEMailPassword.Text = Options.EMail_Password;
            txtUsername.Text = Options.SMS_Username;
            txtPassword.Text = Options.SMS_Password;
            txtFrom.Text = Options.SMS_From;
            txtFTP_Username.Text = Options.FTP_Username;
            txtFTP_Password.Text = Options.FTP_Password;
            //txtFeesFilePath.Text = Options.FeesFilePath;
            cmbPrinters.Text = Options.InvoicePrinter;
            txtInvoiceCopy.Text = Options.InvoiceCopies.ToString();
            txtInvoiceMFTemplate.Text = Options.InvoiceMFTemplate;
            txtInvoiceMFAnalysisTemplate.Text = Options.InvoiceMFAnalysisTemplate;
            txtInvoiceAFTemplate.Text = Options.InvoiceAFTemplate;

            cmbInvoiceFisiko.SelectedValue = Options.InvoiceFisiko;
            //txtSeiraFisiko.Text = Options.SeiraFisiko;
            cmbInvoiceNomiko.SelectedValue = Options.InvoiceNomiko;
            //txtSeiraNomiko.Text = Options.SeiraNomiko;
            //txtReutersFormulaShares.Text = Options.ReutersFormulaShares;
            //txtReutersFormulaBonds.Text = Options.ReutersFormulaBonds;
            cmbInvoicePistotikoFisiko.SelectedValue = Options.InvoicePistotikoFisiko;
            //txtSeiraPistotikoFisiko.Text = Options.SeiraPistotikoFisiko;
            cmbInvoicePistotikoNomiko.SelectedValue = Options.InvoicePistotikoNomiko;
            //txtSeiraPistotikoNomiko.Text = Options.SeiraPistotikoNomiko;
            cmbInvoiceAkyrotiko.SelectedValue = Options.InvoiceAkyrotiko;
            //txtSeiraAkyrotiko.Text = Options.SeiraAkyrotiko;



            if (iRightsLevel == 1)
            {
                btnSave.Enabled = false;
            }


            // --- Hide tabs with accordance with sVisualFlags --------
            if (sVisualFlags.Substring(0, 1) == "0") tabData.TabPages.Remove(tpGeneral);
            if (sVisualFlags.Substring(1, 1) == "0") tabData.TabPages.Remove(tpComms);
            if (sVisualFlags.Substring(2, 1) == "0") tabData.TabPages.Remove(tpManFees);
            if (sVisualFlags.Substring(3, 1) == "0") tabData.TabPages.Remove(tpFilters);
            if (sVisualFlags.Substring(4, 1) == "0") tabData.TabPages.Remove(tpPersonal);
            if (sVisualFlags.Substring(5, 1) == "0") tabData.TabPages.Remove(tpMenusItems);
            if (sVisualFlags.Substring(6, 1) == "0") tabData.TabPages.Remove(tpTemp);
 
            // ----- Hide some all tabs if user isn't Superuser except tpPersonal  ---------- 
            if (Global.UserStatus != 1)
            {
                tabData.TabPages.Remove(tpGeneral);
                tabData.TabPages.Remove(tpComms);
                tabData.TabPages.Remove(tpManFees);
                tabData.TabPages.Remove(tpFilters);
                // tabData.TabPages.Remove(tpPersonal)
                tabData.TabPages.Remove(tpMenusItems);
                tabData.TabPages.Remove(tpTemp);
            }
        }
        public int Aktion { get { return this.iAktion; } set { this.iAktion = value; } }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
        public String VisualFlags { get { return this.sVisualFlags; } set { this.sVisualFlags = value; } }
    }
}
