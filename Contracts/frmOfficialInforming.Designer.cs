namespace Contracts
{
    partial class frmOfficialInforming
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbTypos = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbTypos
            // 
            this.cmbTypos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTypos.FormattingEnabled = true;
            this.cmbTypos.Items.AddRange(new object[] {
            "Εκτελεσμένες Εντολές",
            "Τιμολόγηση RTO",
            "Τιμολόγηση εντολών μετατροπής νομίσματος",
            "Management Fees",
            "Administartion Fees",
            "Performance Fees",
            "Custody Fees",
            "Αλλαγές συμβάσεων",
            "Περιοδική Αξιολόγηση Καταλληλότητας",
            "ExPostCost",
            "Διάφορα"});
            this.cmbTypos.Location = new System.Drawing.Point(126, 11);
            this.cmbTypos.Name = "cmbTypos";
            this.cmbTypos.Size = new System.Drawing.Size(303, 21);
            this.cmbTypos.TabIndex = 248;
            this.cmbTypos.SelectedIndexChanged += new System.EventHandler(this.cmbTypos_SelectedIndexChanged);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(18, 15);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(103, 13);
            this.lblType.TabIndex = 249;
            this.lblType.Text = "Τύπος Ενημέρωσης";
            // 
            // frmOfficialInforming
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(575, 157);
            this.Controls.Add(this.cmbTypos);
            this.Controls.Add(this.lblType);
            this.Name = "frmOfficialInforming";
            this.Text = "Επίσημη Ενημέρωση Πελατών";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmOfficialInforming_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ComboBox cmbTypos;
        internal System.Windows.Forms.Label lblType;
    }
}