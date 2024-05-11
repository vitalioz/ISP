using System;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace Core
{
    public partial class frmPrintInvoiceOptions : Form
    {
        string sInvoicePrinter;
        DateTime dDateIssue;
        int iMode;                                      // 1 - full options, 2 - hide IssueDate
        int iNumCopies, iLastAktion;

        public frmPrintInvoiceOptions()
        {
            InitializeComponent();
        }

        private void frmPrintInvoiceOptions_Load(object sender, EventArgs e)
        {
            foreach (string printer in PrinterSettings.InstalledPrinters) cmbPrinters.Items.Add(printer);

            cmbPrinters.Text = InvoicePrinter;
            dIssueDate.Value = dDateIssue;
            numCopies.Value = iNumCopies;

            dIssueDate.Value = DateTime.Now;
            if (iMode == 2) panIssueDate.Visible = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            sInvoicePrinter = cmbPrinters.Text;
            dDateIssue = dIssueDate.Value;
            iNumCopies = Convert.ToInt32(numCopies.Value);
            iLastAktion = 1;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            iLastAktion = 0;
            this.Close();
        }
        public int Mode { get { return this.iMode; } set { this.iMode = value; } }
        public string InvoicePrinter { get { return this.sInvoicePrinter; } set { this.sInvoicePrinter = value; } }
        public DateTime DateIssue { get { return this.dDateIssue; } set { this.dDateIssue = value; } }
        public int NumCopies { get { return this.iNumCopies; } set { this.iNumCopies = value; } }
        public int LastAktion { get { return this.iLastAktion; } set { this.iLastAktion = value; } }
    }
}
