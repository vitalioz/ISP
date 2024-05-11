namespace Core
{
    partial class ucDoubleCalendar
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
            this.dTo = new System.Windows.Forms.DateTimePicker();
            this.dFrom = new System.Windows.Forms.DateTimePicker();
            this.lbOptions = new System.Windows.Forms.ListBox();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.picOptions = new System.Windows.Forms.PictureBox();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOptions)).BeginInit();
            this.SuspendLayout();
            // 
            // dTo
            // 
            this.dTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dTo.Location = new System.Drawing.Point(123, 0);
            this.dTo.Name = "dTo";
            this.dTo.Size = new System.Drawing.Size(86, 20);
            this.dTo.TabIndex = 352;
            this.dTo.ValueChanged += new System.EventHandler(this.dTo_ValueChanged);
            // 
            // dFrom
            // 
            this.dFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dFrom.Location = new System.Drawing.Point(25, 0);
            this.dFrom.Name = "dFrom";
            this.dFrom.Size = new System.Drawing.Size(86, 20);
            this.dFrom.TabIndex = 351;
            this.dFrom.ValueChanged += new System.EventHandler(this.dFrom_ValueChanged);
            // 
            // lbOptions
            // 
            this.lbOptions.FormattingEnabled = true;
            this.lbOptions.Items.AddRange(new object[] {
            "Today",
            "Yesterday",
            "This Week",
            "This Month",
            "Quarter",
            "Semester",
            "This Year",
            "Previous Week",
            "Previous Month",
            "Previous Year"});
            this.lbOptions.Location = new System.Drawing.Point(6, 6);
            this.lbOptions.Name = "lbOptions";
            this.lbOptions.Size = new System.Drawing.Size(216, 134);
            this.lbOptions.TabIndex = 3;
            this.lbOptions.SelectedIndexChanged += new System.EventHandler(this.lbOptions_SelectedIndexChanged);
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Panel1.Controls.Add(this.lbOptions);
            this.Panel1.Location = new System.Drawing.Point(-1, 24);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(228, 146);
            this.Panel1.TabIndex = 355;
            // 
            // picClose
            // 
            this.picClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picClose.Image = global::Core.Properties.Resources.cancel;
            this.picClose.Location = new System.Drawing.Point(211, 1);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(18, 18);
            this.picClose.TabIndex = 354;
            this.picClose.TabStop = false;
            this.picClose.Visible = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            // 
            // picOptions
            // 
            this.picOptions.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picOptions.Image = global::Core.Properties.Resources.dblCalendar;
            this.picOptions.Location = new System.Drawing.Point(2, 2);
            this.picOptions.Name = "picOptions";
            this.picOptions.Size = new System.Drawing.Size(19, 17);
            this.picOptions.TabIndex = 353;
            this.picOptions.TabStop = false;
            this.picOptions.Click += new System.EventHandler(this.picOptions_Click);
            // 
            // ucDoubleCalendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.dTo);
            this.Controls.Add(this.dFrom);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.picClose);
            this.Controls.Add(this.picOptions);
            this.Name = "ucDoubleCalendar";
            this.Size = new System.Drawing.Size(210, 20);
            this.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOptions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DateTimePicker dTo;
        internal System.Windows.Forms.DateTimePicker dFrom;
        internal System.Windows.Forms.ListBox lbOptions;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.PictureBox picClose;
        internal System.Windows.Forms.PictureBox picOptions;
    }
}
