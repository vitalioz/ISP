namespace Core
{
    partial class ucClientSearch
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucClientSearch));
            this.Client_ID = new System.Windows.Forms.Label();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.panList = new System.Windows.Forms.Panel();
            this.txtClientName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.panList.SuspendLayout();
            this.SuspendLayout();
            // 
            // Client_ID
            // 
            this.Client_ID.AutoSize = true;
            this.Client_ID.Location = new System.Drawing.Point(369, 0);
            this.Client_ID.Name = "Client_ID";
            this.Client_ID.Size = new System.Drawing.Size(28, 13);
            this.Client_ID.TabIndex = 410;
            this.Client_ID.Text = "-999";
            this.Client_ID.Visible = false;
            this.Client_ID.TextChanged += new System.EventHandler(this.Client_ID_TextChanged);
            // 
            // picClose
            // 
            this.picClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picClose.Image = global::Core.Properties.Resources.cancel;
            this.picClose.Location = new System.Drawing.Point(374, 4);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(18, 18);
            this.picClose.TabIndex = 405;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            // 
            // fgList
            // 
            this.fgList.AllowEditing = false;
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(6, 28);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 1;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Size = new System.Drawing.Size(385, 313);
            this.fgList.TabIndex = 5;
            // 
            // panList
            // 
            this.panList.BackColor = System.Drawing.Color.LightSalmon;
            this.panList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panList.Controls.Add(this.picClose);
            this.panList.Controls.Add(this.fgList);
            this.panList.Location = new System.Drawing.Point(1, 22);
            this.panList.Name = "panList";
            this.panList.Size = new System.Drawing.Size(399, 350);
            this.panList.TabIndex = 411;
            // 
            // txtClientName
            // 
            this.txtClientName.Location = new System.Drawing.Point(0, 0);
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.Size = new System.Drawing.Size(200, 20);
            this.txtClientName.TabIndex = 409;
            this.txtClientName.TextChanged += new System.EventHandler(this.txtContractTitle_TextChanged);
            // 
            // ucClientSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Client_ID);
            this.Controls.Add(this.panList);
            this.Controls.Add(this.txtClientName);
            this.Name = "ucClientSearch";
            this.Size = new System.Drawing.Size(401, 372);
            this.Load += new System.EventHandler(this.ucClientSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.panList.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label Client_ID;
        internal System.Windows.Forms.PictureBox picClose;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
        private System.Windows.Forms.Panel panList;
        public System.Windows.Forms.TextBox txtClientName;
    }
}
