namespace Contracts
{
    partial class frmClientsList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClientsList));
            this.panSelectType = new System.Windows.Forms.Panel();
            this.rbNomical = new System.Windows.Forms.RadioButton();
            this.rbPhysical = new System.Windows.Forms.RadioButton();
            this.Label1 = new System.Windows.Forms.Label();
            this.btnCancel_Tipos = new System.Windows.Forms.Button();
            this.btnOK_Tipos = new System.Windows.Forms.Button();
            this.chkContact = new System.Windows.Forms.CheckBox();
            this.chkDisable = new System.Windows.Forms.CheckBox();
            this.chkCandidate = new System.Windows.Forms.CheckBox();
            this.chkCustomer = new System.Windows.Forms.CheckBox();
            this.toolLeft = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbEdit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtFilter = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbFilter = new System.Windows.Forms.ToolStripButton();
            this.fgList = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.toolRight = new System.Windows.Forms.ToolStrip();
            this.ToolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbHistory = new System.Windows.Forms.ToolStripButton();
            this.panNotes = new System.Windows.Forms.Panel();
            this.Label2 = new System.Windows.Forms.Label();
            this.cmbDocTypes = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.Label12 = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.picFilePath = new System.Windows.Forms.PictureBox();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.panClients = new System.Windows.Forms.Panel();
            this.Label3 = new System.Windows.Forms.Label();
            this.btnCancelClient = new System.Windows.Forms.Button();
            this.btnSaveClient = new System.Windows.Forms.Button();
            this.fgClients = new C1.Win.C1FlexGrid.C1FlexGrid();
            this.ucCD = new Core.ucClientData();
            this.panSelectType.SuspendLayout();
            this.toolLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).BeginInit();
            this.toolRight.SuspendLayout();
            this.panNotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFilePath)).BeginInit();
            this.panClients.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgClients)).BeginInit();
            this.SuspendLayout();
            // 
            // panSelectType
            // 
            this.panSelectType.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panSelectType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panSelectType.Controls.Add(this.rbNomical);
            this.panSelectType.Controls.Add(this.rbPhysical);
            this.panSelectType.Controls.Add(this.Label1);
            this.panSelectType.Controls.Add(this.btnCancel_Tipos);
            this.panSelectType.Controls.Add(this.btnOK_Tipos);
            this.panSelectType.Location = new System.Drawing.Point(6, 69);
            this.panSelectType.Name = "panSelectType";
            this.panSelectType.Size = new System.Drawing.Size(316, 149);
            this.panSelectType.TabIndex = 583;
            this.panSelectType.Visible = false;
            // 
            // rbNomical
            // 
            this.rbNomical.AutoSize = true;
            this.rbNomical.Location = new System.Drawing.Point(78, 74);
            this.rbNomical.Name = "rbNomical";
            this.rbNomical.Size = new System.Drawing.Size(107, 17);
            this.rbNomical.TabIndex = 256;
            this.rbNomical.TabStop = true;
            this.rbNomical.Text = "Νομικό Πρόσωπο";
            this.rbNomical.UseVisualStyleBackColor = true;
            // 
            // rbPhysical
            // 
            this.rbPhysical.AutoSize = true;
            this.rbPhysical.Location = new System.Drawing.Point(78, 51);
            this.rbPhysical.Name = "rbPhysical";
            this.rbPhysical.Size = new System.Drawing.Size(111, 17);
            this.rbPhysical.TabIndex = 255;
            this.rbPhysical.TabStop = true;
            this.rbPhysical.Text = "Φυσικό Πρόσωπο";
            this.rbPhysical.UseVisualStyleBackColor = true;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(81, 29);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(137, 13);
            this.Label1.TabIndex = 254;
            this.Label1.Text = "Επιλέξτε τύπο του πελάτη";
            // 
            // btnCancel_Tipos
            // 
            this.btnCancel_Tipos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel_Tipos.Location = new System.Drawing.Point(180, 109);
            this.btnCancel_Tipos.Name = "btnCancel_Tipos";
            this.btnCancel_Tipos.Size = new System.Drawing.Size(100, 26);
            this.btnCancel_Tipos.TabIndex = 253;
            this.btnCancel_Tipos.Text = "   Άκυρο";
            this.btnCancel_Tipos.UseVisualStyleBackColor = true;
            this.btnCancel_Tipos.Click += new System.EventHandler(this.btnCancel_Tipos_Click);
            // 
            // btnOK_Tipos
            // 
            this.btnOK_Tipos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOK_Tipos.Location = new System.Drawing.Point(42, 109);
            this.btnOK_Tipos.Name = "btnOK_Tipos";
            this.btnOK_Tipos.Size = new System.Drawing.Size(100, 26);
            this.btnOK_Tipos.TabIndex = 251;
            this.btnOK_Tipos.Text = "OK";
            this.btnOK_Tipos.UseVisualStyleBackColor = true;
            this.btnOK_Tipos.Click += new System.EventHandler(this.btnOK_Tipos_Click);
            // 
            // chkContact
            // 
            this.chkContact.AutoSize = true;
            this.chkContact.BackColor = System.Drawing.Color.White;
            this.chkContact.Location = new System.Drawing.Point(8, 40);
            this.chkContact.Name = "chkContact";
            this.chkContact.Size = new System.Drawing.Size(65, 17);
            this.chkContact.TabIndex = 579;
            this.chkContact.Text = "Επαφές";
            this.chkContact.UseVisualStyleBackColor = false;
            this.chkContact.CheckedChanged += new System.EventHandler(this.chkContact_CheckedChanged);
            // 
            // chkDisable
            // 
            this.chkDisable.AutoSize = true;
            this.chkDisable.BackColor = System.Drawing.Color.Tomato;
            this.chkDisable.Location = new System.Drawing.Point(284, 40);
            this.chkDisable.Name = "chkDisable";
            this.chkDisable.Size = new System.Drawing.Size(81, 17);
            this.chkDisable.TabIndex = 582;
            this.chkDisable.Text = "Ανενεργός";
            this.chkDisable.UseVisualStyleBackColor = false;
            this.chkDisable.CheckedChanged += new System.EventHandler(this.chkDisable_CheckedChanged);
            // 
            // chkCandidate
            // 
            this.chkCandidate.AutoSize = true;
            this.chkCandidate.BackColor = System.Drawing.Color.LightYellow;
            this.chkCandidate.Checked = true;
            this.chkCandidate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCandidate.Location = new System.Drawing.Point(92, 40);
            this.chkCandidate.Name = "chkCandidate";
            this.chkCandidate.Size = new System.Drawing.Size(80, 17);
            this.chkCandidate.TabIndex = 580;
            this.chkCandidate.Text = "Υποψήφιοι";
            this.chkCandidate.UseVisualStyleBackColor = false;
            this.chkCandidate.CheckedChanged += new System.EventHandler(this.chkCandidate_CheckedChanged);
            // 
            // chkCustomer
            // 
            this.chkCustomer.AutoSize = true;
            this.chkCustomer.BackColor = System.Drawing.Color.PeachPuff;
            this.chkCustomer.Checked = true;
            this.chkCustomer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCustomer.Location = new System.Drawing.Point(192, 40);
            this.chkCustomer.Name = "chkCustomer";
            this.chkCustomer.Size = new System.Drawing.Size(72, 17);
            this.chkCustomer.TabIndex = 581;
            this.chkCustomer.Text = " Πελάτες";
            this.chkCustomer.UseVisualStyleBackColor = false;
            this.chkCustomer.CheckedChanged += new System.EventHandler(this.chkCustomer_CheckedChanged);
            // 
            // toolLeft
            // 
            this.toolLeft.AutoSize = false;
            this.toolLeft.BackColor = System.Drawing.Color.Gainsboro;
            this.toolLeft.Dock = System.Windows.Forms.DockStyle.None;
            this.toolLeft.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolLeft.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAdd,
            this.toolStripSeparator1,
            this.tsbEdit,
            this.toolStripSeparator2,
            this.tsbDelete,
            this.toolStripSeparator3,
            this.tsbPrint,
            this.toolStripSeparator4,
            this.toolStripLabel1,
            this.txtFilter,
            this.toolStripSeparator5,
            this.tsbFilter});
            this.toolLeft.Location = new System.Drawing.Point(8, 8);
            this.toolLeft.Name = "toolLeft";
            this.toolLeft.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolLeft.Size = new System.Drawing.Size(360, 25);
            this.toolLeft.TabIndex = 578;
            this.toolLeft.Text = "toolStrip1";
            // 
            // tsbAdd
            // 
            this.tsbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAdd.Image = global::Contracts.Properties.Resources.plus;
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(23, 22);
            this.tsbAdd.Text = "Προσθήκη νέου πελάτη";
            this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbEdit
            // 
            this.tsbEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbEdit.Image = global::Contracts.Properties.Resources.edit;
            this.tsbEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEdit.Name = "tsbEdit";
            this.tsbEdit.Size = new System.Drawing.Size(23, 22);
            this.tsbEdit.Text = "Επεξεργασία πελάτη";
            this.tsbEdit.Click += new System.EventHandler(this.tsbEdit_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbDelete
            // 
            this.tsbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbDelete.Image = global::Contracts.Properties.Resources.minus;
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(23, 22);
            this.tsbDelete.Text = "Διαγραφή πελάτη";
            this.tsbDelete.Click += new System.EventHandler(this.tsbDelete_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbPrint
            // 
            this.tsbPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbPrint.Image = global::Contracts.Properties.Resources.PrintHS;
            this.tsbPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrint.Name = "tsbPrint";
            this.tsbPrint.Size = new System.Drawing.Size(23, 22);
            this.tsbPrint.Text = "Έκτύπωση λίστας";
            this.tsbPrint.Click += new System.EventHandler(this.tsbPrint_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(45, 22);
            this.toolStripLabel1.Text = "Φίλτρο";
            // 
            // txtFilter
            // 
            this.txtFilter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(100, 25);
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbFilter
            // 
            this.tsbFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbFilter.Image = global::Contracts.Properties.Resources.filter;
            this.tsbFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFilter.Name = "tsbFilter";
            this.tsbFilter.Size = new System.Drawing.Size(23, 22);
            this.tsbFilter.Text = "Φίλτρο";
            // 
            // fgList
            // 
            this.fgList.AllowEditing = false;
            this.fgList.ColumnInfo = resources.GetString("fgList.ColumnInfo");
            this.fgList.Location = new System.Drawing.Point(8, 62);
            this.fgList.Name = "fgList";
            this.fgList.Rows.Count = 1;
            this.fgList.Rows.DefaultSize = 17;
            this.fgList.Size = new System.Drawing.Size(380, 782);
            this.fgList.TabIndex = 577;
            // 
            // toolRight
            // 
            this.toolRight.AutoSize = false;
            this.toolRight.BackColor = System.Drawing.Color.Gainsboro;
            this.toolRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolRight.Dock = System.Windows.Forms.DockStyle.None;
            this.toolRight.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolRight.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolRight.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripLabel3,
            this.tsbSave,
            this.toolStripSeparator7,
            this.tsbHistory});
            this.toolRight.Location = new System.Drawing.Point(415, 9);
            this.toolRight.Name = "toolRight";
            this.toolRight.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolRight.Size = new System.Drawing.Size(79, 27);
            this.toolRight.TabIndex = 584;
            this.toolRight.Text = "ToolStrip1";
            // 
            // ToolStripLabel3
            // 
            this.ToolStripLabel3.Name = "ToolStripLabel3";
            this.ToolStripLabel3.Size = new System.Drawing.Size(10, 24);
            this.ToolStripLabel3.Text = " ";
            // 
            // tsbSave
            // 
            this.tsbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbSave.Image = global::Contracts.Properties.Resources.save;
            this.tsbSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(24, 24);
            this.tsbSave.Text = "Αποθήκευση";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbHistory
            // 
            this.tsbHistory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbHistory.Image = global::Contracts.Properties.Resources.history;
            this.tsbHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHistory.Name = "tsbHistory";
            this.tsbHistory.Size = new System.Drawing.Size(23, 24);
            this.tsbHistory.Text = "Ιστορία";
            this.tsbHistory.Click += new System.EventHandler(this.tsbHistory_Click);
            // 
            // panNotes
            // 
            this.panNotes.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panNotes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panNotes.Controls.Add(this.Label2);
            this.panNotes.Controls.Add(this.cmbDocTypes);
            this.panNotes.Controls.Add(this.btnCancel);
            this.panNotes.Controls.Add(this.btnSave);
            this.panNotes.Controls.Add(this.Label12);
            this.panNotes.Controls.Add(this.txtNotes);
            this.panNotes.Controls.Add(this.Label11);
            this.panNotes.Controls.Add(this.picFilePath);
            this.panNotes.Controls.Add(this.txtFileName);
            this.panNotes.Location = new System.Drawing.Point(415, 104);
            this.panNotes.Name = "panNotes";
            this.panNotes.Size = new System.Drawing.Size(519, 194);
            this.panNotes.TabIndex = 586;
            this.panNotes.Visible = false;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(15, 91);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(91, 13);
            this.Label2.TabIndex = 252;
            this.Label2.Text = "Τύπος εγγράφου";
            // 
            // cmbDocTypes
            // 
            this.cmbDocTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDocTypes.FormattingEnabled = true;
            this.cmbDocTypes.Location = new System.Drawing.Point(122, 88);
            this.cmbDocTypes.Name = "cmbDocTypes";
            this.cmbDocTypes.Size = new System.Drawing.Size(386, 21);
            this.cmbDocTypes.TabIndex = 247;
            // 
            // btnCancel
            // 
            this.btnCancel.Image = global::Contracts.Properties.Resources.cancel;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancel.Location = new System.Drawing.Point(294, 157);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(102, 28);
            this.btnCancel.TabIndex = 253;
            this.btnCancel.Text = "   Άκυρο";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = global::Contracts.Properties.Resources.OK;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(156, 157);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(102, 28);
            this.btnSave.TabIndex = 251;
            this.btnSave.Text = "OK";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(15, 20);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(101, 13);
            this.Label12.TabIndex = 248;
            this.Label12.Text = "Σχόλιο - Αιτιολογία";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(122, 17);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(386, 65);
            this.txtNotes.TabIndex = 245;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(15, 119);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(54, 13);
            this.Label11.TabIndex = 247;
            this.Label11.Text = "Έγγραφο";
            // 
            // picFilePath
            // 
            this.picFilePath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picFilePath.Image = global::Contracts.Properties.Resources.FindFolder;
            this.picFilePath.Location = new System.Drawing.Point(480, 112);
            this.picFilePath.Name = "picFilePath";
            this.picFilePath.Size = new System.Drawing.Size(28, 25);
            this.picFilePath.TabIndex = 246;
            this.picFilePath.TabStop = false;
            this.picFilePath.Click += new System.EventHandler(this.picFilePath_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(122, 115);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(352, 20);
            this.txtFileName.TabIndex = 249;
            // 
            // panClients
            // 
            this.panClients.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panClients.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panClients.Controls.Add(this.Label3);
            this.panClients.Controls.Add(this.btnCancelClient);
            this.panClients.Controls.Add(this.btnSaveClient);
            this.panClients.Controls.Add(this.fgClients);
            this.panClients.Location = new System.Drawing.Point(287, 309);
            this.panClients.Name = "panClients";
            this.panClients.Size = new System.Drawing.Size(752, 367);
            this.panClients.TabIndex = 587;
            this.panClients.Visible = false;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(223, 16);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(373, 13);
            this.Label3.TabIndex = 256;
            this.Label3.Text = "Στο σύστημα υπάρχει ήδη ονοματεπώνυμιο με παρόμοια χαρακτηριστικά";
            // 
            // btnCancelClient
            // 
            this.btnCancelClient.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelClient.Location = new System.Drawing.Point(430, 325);
            this.btnCancelClient.Name = "btnCancelClient";
            this.btnCancelClient.Size = new System.Drawing.Size(100, 26);
            this.btnCancelClient.TabIndex = 255;
            this.btnCancelClient.Text = "Όχι";
            this.btnCancelClient.UseVisualStyleBackColor = true;
            // 
            // btnSaveClient
            // 
            this.btnSaveClient.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveClient.Location = new System.Drawing.Point(265, 325);
            this.btnSaveClient.Name = "btnSaveClient";
            this.btnSaveClient.Size = new System.Drawing.Size(100, 26);
            this.btnSaveClient.TabIndex = 254;
            this.btnSaveClient.Text = "Ναί";
            this.btnSaveClient.UseVisualStyleBackColor = true;
            // 
            // fgClients
            // 
            this.fgClients.ColumnInfo = resources.GetString("fgClients.ColumnInfo");
            this.fgClients.Location = new System.Drawing.Point(11, 46);
            this.fgClients.Name = "fgClients";
            this.fgClients.Rows.Count = 1;
            this.fgClients.Rows.DefaultSize = 17;
            this.fgClients.Size = new System.Drawing.Size(735, 263);
            this.fgClients.TabIndex = 1;
            // 
            // ucCD
            // 
            this.ucCD.BackColor = System.Drawing.Color.Gainsboro;
            this.ucCD.CheckTrack = false;
            this.ucCD.Client_ID = 0;
            this.ucCD.Location = new System.Drawing.Point(403, 62);
            this.ucCD.Name = "ucCD";
            this.ucCD.Record_ID = 0;
            this.ucCD.Size = new System.Drawing.Size(912, 786);
            this.ucCD.TabIndex = 585;
            this.ucCD.Users_List = null;
            // 
            // frmClientsList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Thistle;
            this.ClientSize = new System.Drawing.Size(1327, 855);
            this.Controls.Add(this.panClients);
            this.Controls.Add(this.panNotes);
            this.Controls.Add(this.ucCD);
            this.Controls.Add(this.toolRight);
            this.Controls.Add(this.panSelectType);
            this.Controls.Add(this.chkContact);
            this.Controls.Add(this.chkDisable);
            this.Controls.Add(this.chkCandidate);
            this.Controls.Add(this.chkCustomer);
            this.Controls.Add(this.toolLeft);
            this.Controls.Add(this.fgList);
            this.Name = "frmClientsList";
            this.Text = "frmClientsList";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Deactivate += new System.EventHandler(this.Form_Deactivate);
            this.Load += new System.EventHandler(this.frmClientsList_Load);
            this.panSelectType.ResumeLayout(false);
            this.panSelectType.PerformLayout();
            this.toolLeft.ResumeLayout(false);
            this.toolLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgList)).EndInit();
            this.toolRight.ResumeLayout(false);
            this.toolRight.PerformLayout();
            this.panNotes.ResumeLayout(false);
            this.panNotes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFilePath)).EndInit();
            this.panClients.ResumeLayout(false);
            this.panClients.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgClients)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Panel panSelectType;
        internal System.Windows.Forms.RadioButton rbNomical;
        internal System.Windows.Forms.RadioButton rbPhysical;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button btnCancel_Tipos;
        internal System.Windows.Forms.Button btnOK_Tipos;
        internal System.Windows.Forms.CheckBox chkContact;
        internal System.Windows.Forms.CheckBox chkDisable;
        internal System.Windows.Forms.CheckBox chkCandidate;
        internal System.Windows.Forms.CheckBox chkCustomer;
        private System.Windows.Forms.ToolStrip toolLeft;
        private System.Windows.Forms.ToolStripButton tsbAdd;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsbEdit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbPrint;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox txtFilter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton tsbFilter;
        private C1.Win.C1FlexGrid.C1FlexGrid fgList;
        internal System.Windows.Forms.ToolStrip toolRight;
        internal System.Windows.Forms.ToolStripLabel ToolStripLabel3;
        internal System.Windows.Forms.ToolStripButton tsbHistory;
        internal System.Windows.Forms.ToolStripButton tsbSave;
        private Core.ucClientData ucCD;
        internal System.Windows.Forms.Panel panNotes;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.ComboBox cmbDocTypes;
        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.TextBox txtNotes;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.PictureBox picFilePath;
        internal System.Windows.Forms.TextBox txtFileName;
        internal System.Windows.Forms.Panel panClients;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Button btnCancelClient;
        internal System.Windows.Forms.Button btnSaveClient;
        internal C1.Win.C1FlexGrid.C1FlexGrid fgClients;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
    }
}