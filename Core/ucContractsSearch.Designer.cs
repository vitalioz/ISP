namespace Core
{
    partial class ucContractsSearch
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucContractsSearch));
            this.Contract_ID = new System.Windows.Forms.Label();
            this.btnChoice = new System.Windows.Forms.Button();
            this.chkSelect = new System.Windows.Forms.CheckBox();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.txtContractTitle = new System.Windows.Forms.TextBox();
            this.panList = new System.Windows.Forms.Panel();
            this.mnuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuClientData = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuContractData = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.panList.SuspendLayout();
            this.mnuContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // Contract_ID
            // 
            this.Contract_ID.AutoSize = true;
            this.Contract_ID.Location = new System.Drawing.Point(746, 2);
            this.Contract_ID.Name = "Contract_ID";
            this.Contract_ID.Size = new System.Drawing.Size(28, 13);
            this.Contract_ID.TabIndex = 11;
            this.Contract_ID.Text = "-999";
            this.Contract_ID.Visible = false;
            this.Contract_ID.TextChanged += new System.EventHandler(this.Contract_ID_TextChanged);
            // 
            // btnChoice
            // 
            this.btnChoice.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnChoice.FlatAppearance.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.btnChoice.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnChoice.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.btnChoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChoice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChoice.Location = new System.Drawing.Point(363, 346);
            this.btnChoice.Name = "btnChoice";
            this.btnChoice.Size = new System.Drawing.Size(91, 25);
            this.btnChoice.TabIndex = 406;
            this.btnChoice.Text = "Επιλογή";
            this.btnChoice.UseVisualStyleBackColor = false;
            this.btnChoice.Click += new System.EventHandler(this.btnChoice_Click);
            // 
            // chkSelect
            // 
            this.chkSelect.AutoSize = true;
            this.chkSelect.Location = new System.Drawing.Point(13, 31);
            this.chkSelect.Name = "chkSelect";
            this.chkSelect.Size = new System.Drawing.Size(15, 14);
            this.chkSelect.TabIndex = 407;
            this.chkSelect.UseVisualStyleBackColor = true;
            this.chkSelect.CheckedChanged += new System.EventHandler(this.chkSelect_CheckedChanged);
            // 
            // picClose
            // 
            this.picClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picClose.Image = global::Core.Properties.Resources.cancel;
            this.picClose.Location = new System.Drawing.Point(754, 6);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(18, 18);
            this.picClose.TabIndex = 405;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            // 
            // fgList
            // 
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(6, 28);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 1;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Size = new System.Drawing.Size(765, 310);
            this.fgList.TabIndex = 5;
            this.fgList.CellChanged += new C1.Win.C1FlexGrid.RowColEventHandler(this.fgList_CellChanged);
            // 
            // txtContractTitle
            // 
            this.txtContractTitle.Location = new System.Drawing.Point(0, 0);
            this.txtContractTitle.Name = "txtContractTitle";
            this.txtContractTitle.Size = new System.Drawing.Size(200, 20);
            this.txtContractTitle.TabIndex = 9;
            this.txtContractTitle.TextChanged += new System.EventHandler(this.txtContractTitle_TextChanged);
            this.txtContractTitle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtContractTitle_KeyPress);
            // 
            // panList
            // 
            this.panList.BackColor = System.Drawing.Color.LightSalmon;
            this.panList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panList.Controls.Add(this.btnChoice);
            this.panList.Controls.Add(this.picClose);
            this.panList.Controls.Add(this.chkSelect);
            this.panList.Controls.Add(this.fgList);
            this.panList.Location = new System.Drawing.Point(0, 22);
            this.panList.Name = "panList";
            this.panList.Size = new System.Drawing.Size(779, 378);
            this.panList.TabIndex = 408;
            // 
            // mnuContext
            // 
            this.mnuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuClientData,
            this.mnuContractData});
            this.mnuContext.Name = "ContextMenuStrip1";
            this.mnuContext.Size = new System.Drawing.Size(183, 48);
            // 
            // mnuClientData
            // 
            this.mnuClientData.Name = "mnuClientData";
            this.mnuClientData.Size = new System.Drawing.Size(182, 22);
            this.mnuClientData.Text = "Στοιχεία τού πελάτη";
            this.mnuClientData.Click += new System.EventHandler(this.mnuClientData_Click);
            // 
            // mnuContractData
            // 
            this.mnuContractData.Name = "mnuContractData";
            this.mnuContractData.Size = new System.Drawing.Size(182, 22);
            this.mnuContractData.Text = "Στοιχεία Σύμβασης";
            this.mnuContractData.Click += new System.EventHandler(this.mnuContractData_Click);
            // 
            // ucContractsSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panList);
            this.Controls.Add(this.Contract_ID);
            this.Controls.Add(this.txtContractTitle);
            this.Name = "ucContractsSearch";
            this.Size = new System.Drawing.Size(780, 400);
            this.Load += new System.EventHandler(this.ucContracts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.panList.ResumeLayout(false);
            this.panList.PerformLayout();
            this.mnuContext.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.Button btnChoice;
        internal System.Windows.Forms.CheckBox chkSelect;
        internal System.Windows.Forms.PictureBox picClose;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgList;
        private System.Windows.Forms.Panel panList;
        public System.Windows.Forms.Label Contract_ID;
        public System.Windows.Forms.TextBox txtContractTitle;
        internal System.Windows.Forms.ContextMenuStrip mnuContext;
        internal System.Windows.Forms.ToolStripMenuItem mnuClientData;
        private System.Windows.Forms.ToolStripMenuItem mnuContractData;
    }
}
