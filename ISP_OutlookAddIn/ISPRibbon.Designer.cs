
namespace ISP_OutlookAddIn
{
    partial class ISPRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public ISPRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TabMail = this.Factory.CreateRibbonTab();
            this.grpISP = this.Factory.CreateRibbonGroup();
            this.btnSave = this.Factory.CreateRibbonButton();
            this.TabMail.SuspendLayout();
            this.grpISP.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabMail
            // 
            this.TabMail.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.TabMail.ControlId.OfficeId = "TabMail";
            this.TabMail.Groups.Add(this.grpISP);
            this.TabMail.Label = "TabMail";
            this.TabMail.Name = "TabMail";
            // 
            // grpISP
            // 
            this.grpISP.Items.Add(this.btnSave);
            this.grpISP.Label = "ISP commands";
            this.grpISP.Name = "grpISP";
            // 
            // btnSave
            // 
            this.btnSave.Image = global::ISP_OutlookAddIn.Properties.Resources.Mail_Save_16x16;
            this.btnSave.Label = "Save in ISP";
            this.btnSave.Name = "btnSave";
            this.btnSave.ShowImage = true;
            this.btnSave.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.btnSave_Click);
            // 
            // ISPRibbon
            // 
            this.Name = "ISPRibbon";
            this.RibbonType = "Microsoft.Outlook.Explorer, Microsoft.Outlook.Mail.Read";
            this.Tabs.Add(this.TabMail);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.ISPRibbon_Load);
            this.TabMail.ResumeLayout(false);
            this.TabMail.PerformLayout();
            this.grpISP.ResumeLayout(false);
            this.grpISP.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab TabMail;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup grpISP;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnSave;
    }

    partial class ThisRibbonCollection
    {
        internal ISPRibbon ISPRibbon
        {
            get { return this.GetRibbon<ISPRibbon>(); }
        }
    }
}
