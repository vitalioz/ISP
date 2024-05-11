
namespace ISPServer
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.dFrom = new System.Windows.Forms.DateTimePicker();
            this.dTo = new System.Windows.Forms.DateTimePicker();
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuContextIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuShowForm = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.c1FlexGrid1 = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.chkAllRecords = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.menuContextIcon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // fgList
            // 
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(12, 69);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 1;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Size = new System.Drawing.Size(1102, 521);
            this.fgList.TabIndex = 0;
            // 
            // dFrom
            // 
            this.dFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dFrom.Location = new System.Drawing.Point(17, 25);
            this.dFrom.Name = "dFrom";
            this.dFrom.Size = new System.Drawing.Size(96, 20);
            this.dFrom.TabIndex = 1;
            // 
            // dTo
            // 
            this.dTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dTo.Location = new System.Drawing.Point(134, 25);
            this.dTo.Name = "dTo";
            this.dTo.Size = new System.Drawing.Size(96, 20);
            this.dTo.TabIndex = 2;
            // 
            // Timer1
            // 
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // menuContextIcon
            // 
            this.menuContextIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuShowForm,
            this.menuExit});
            this.menuContextIcon.Name = "menuContextIcon";
            this.menuContextIcon.Size = new System.Drawing.Size(135, 48);
            // 
            // menuShowForm
            // 
            this.menuShowForm.Name = "menuShowForm";
            this.menuShowForm.Size = new System.Drawing.Size(134, 22);
            this.menuShowForm.Text = "Show Form";
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.Size = new System.Drawing.Size(134, 22);
            this.menuExit.Text = "Exit";
            // 
            // c1FlexGrid1
            // 
            this.c1FlexGrid1.ColumnInfo = resources.GetString("c1FlexGrid1.ColumnInfo");
            this.c1FlexGrid1.Location = new System.Drawing.Point(12, 606);
            this.c1FlexGrid1.Name = "c1FlexGrid1";
            this.c1FlexGrid1.Rows.Count = 1;
            this.c1FlexGrid1.Rows.DefaultSize = 17;
            this.c1FlexGrid1.Size = new System.Drawing.Size(1102, 110);
            this.c1FlexGrid1.TabIndex = 3;
            // 
            // chkAllRecords
            // 
            this.chkAllRecords.AutoSize = true;
            this.chkAllRecords.Location = new System.Drawing.Point(331, 30);
            this.chkAllRecords.Name = "chkAllRecords";
            this.chkAllRecords.Size = new System.Drawing.Size(104, 17);
            this.chkAllRecords.TabIndex = 4;
            this.chkAllRecords.Text = "Show all records";
            this.chkAllRecords.UseVisualStyleBackColor = true;
            this.chkAllRecords.CheckedChanged += new System.EventHandler(this.chkAllRecords_CheckedChanged);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1130, 722);
            this.Controls.Add(this.chkAllRecords);
            this.Controls.Add(this.c1FlexGrid1);
            this.Controls.Add(this.dTo);
            this.Controls.Add(this.dFrom);
            this.Controls.Add(this.fgList);
            this.Name = "frmMain";
            this.Text = "ISP Server";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.menuContextIcon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1FlexGrid1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private C1.Win.C1FlexGrid.C1FlexGrid fgList;
        private System.Windows.Forms.DateTimePicker dFrom;
        private System.Windows.Forms.DateTimePicker dTo;
        private System.Windows.Forms.Timer Timer1;
        private System.Windows.Forms.ContextMenuStrip menuContextIcon;
        private System.Windows.Forms.ToolStripMenuItem menuShowForm;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private C1.Win.C1FlexGrid.C1FlexGrid c1FlexGrid1;
        private System.Windows.Forms.CheckBox chkAllRecords;
    }
}

