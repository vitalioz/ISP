using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;

namespace Core
{
    public partial class ucDefaultList : UserControl
    {
        SqlConnection conn = new SqlConnection(Global.connStr);
        SqlCommand cmd;
        int i, iList_ID, iCashTables_ID, iAction, iRightsLevel;
        string sTemp, sTableName;
        bool bCheckList;
        clsDocTypes DocTypes = new clsDocTypes();
        public ucDefaultList()
        {
            InitializeComponent();
        }

        private void ucDefaultList_Load(object sender, EventArgs e)
        {            
            panDetails.Enabled = false;
            tsbSave.Enabled = false;

            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, style=Bold; BackColor:LightBlue; ForeColor:Black;}");
            fgList.RowColChange += new EventHandler(fgList_RowColChange);            
        }
        protected override void OnResize(EventArgs e)
        {
            grpData.Height = this.Height - 6;
            grpData.Width = this.Width - 20;
            fgList.Height = this.Height - 92;
        }
        public void StartInit(int iParentList_ID, int iParentCashTables_ID, string sParentTableName, string sParentListTitle)
        {
            bCheckList = false;
            iList_ID = iParentList_ID;
            iCashTables_ID = iParentCashTables_ID;
            sTableName = sParentTableName;
            lblListItemTitle.Text = sParentListTitle;

            switch (iList_ID) {
                case 13:                 // Έγγραφα πελατών
                    lblTitle1.Text = "Κωδικός";
                    lblTitle2.Text = "";
                    txtTitle1.Visible = true;
                    txtTitle2.Visible = false;
                    lblGroup.Text = "Ομάδα εγγράφων";
                    lblGroup.Visible = true;
                    cmbGroup.Visible = true;
                    chkFlag.Visible = false;
                    break;
                case 21:                 // Information Methods
                    lblTitle1.Text = "Τίτλος";
                    lblTitle2.Text = "";
                    lblTitle2.Visible = false;
                    txtTitle1.Visible = true;
                    txtTitle2.Text = "";
                    txtTitle2.Visible = false;
                    lblGroup.Text = "";
                    lblGroup.Visible = false;
                    cmbGroup.Visible = false;
                    chkFlag.Visible = false;
                    break;
                case 25:            // 25-Finance Services
                    lblTitle1.Text = "Κωδικός";
                    lblTitle2.Text = "Τίτλος";
                    lblTitle2.Visible = true;
                    txtTitle2.Visible = true;
                    lblGroup.Text = "";
                    lblGroup.Visible = false;
                    cmbGroup.Visible = false;
                    chkFlag.Visible = false;
                    break;
                default:           // misc simple lists
                    lblTitle1.Text = "Τίτλος";
                    lblTitle2.Text = "";
                    lblTitle2.Visible = false;
                    txtTitle1.Visible = true;
                    txtTitle2.Text = "";
                    txtTitle2.Visible = false;
                    lblGroup.Text = "";
                    lblGroup.Visible = false;
                    cmbGroup.Visible = false;
                    chkFlag.Visible = false;
                    break;
            }

            DefineList(sParentTableName);
            bCheckList = true;

            if (fgList.Rows.Count > 1) {
                fgList.Row = 1;
                ShowRecord();
                fgList.Focus();
            }
            
            if (iRightsLevel == 1) toolLeft.Enabled = false;
        }
        private void DefineList(string sTableName)
        {
            lblTableName.Text = sTableName;
            try
            {
                fgList.Redraw = false;
                fgList.Tree.Column = 0;
                fgList.Rows.Count = 1;

                switch (iList_ID)
                {
                    case 13:                 // Έγγραφα πελατών
                        DocTypes = new clsDocTypes();
                        DocTypes.GetList();
                        foreach (DataRow dtRow in DocTypes.List.Rows)
                            fgList.AddItem(dtRow["Title"] + "\t" + dtRow["ID"] + "\t" + dtRow["Title"] + "\t" + "0" + "\t" + "0");
                        break;
                    case 21:                 // Information Methods

                        break;
                    case 25:            // 25-Finance Services

                        break;
                    default:           // misc simple lists
                        clsSystem System = new clsSystem();
                        System.GetTable(sTableName);
                        foreach (DataRow dtRow in System.List.Rows)
                            fgList.AddItem(dtRow["Title"] + "\t" + dtRow["ID"] + "\t" + "" + "\t" + "0" + "\t" + "0");
                        break;
                }

                fgList.Redraw = true;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { }
        }
        private void ShowRecord()
        {
            txtTitle1.Text = fgList[fgList.Row, "Title"] + "";

            switch (iList_ID)
            {
                case 13:                                     //13 - Έγγραφα πελατών
                    DocTypes = new clsDocTypes();
                    DocTypes.Record_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                    DocTypes.GetRecord();
                    txtTitle1.Text = DocTypes.Title;
                    break;
                case 21:                                    // 21 - Information Methods
                    chkFlag.Checked = Convert.ToBoolean(fgList[fgList.Row, "Flag"]);
                    break;
                case 25:                                    // 25-Finance Services
                    txtTitle2.Text = fgList[fgList.Row, "TitleEng"]+"";
                    break;
                default:                                    // misc simple lists
                    break;
            }
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            i = fgList.Row;
            if (i > 0) { 
               if (MessageBox.Show(Global.GetLabel("attention_you_ask_for_deletion") + "." + "\n" + Global.GetLabel("are_you_sure_for_deletion"), Global.AppTitle,
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) {

                    if (Convert.ToInt32(fgList[i, "ID"]) != 0) {
                        clsSystem System = new clsSystem();
                        System.ExecSQL("DELETE " + lblTableName.Text + " WHERE ID = " + fgList[i, "ID"]);
                    }

                    txtTitle1.Text = "";
                    txtTitle2.Text = "";

                    fgList.RemoveItem(fgList.Row);
                    fgList.Row = 0;
                    if (fgList.Rows.Count <= i)  i = fgList.Rows.Count - 1;
                    fgList.Row = i;
                    fgList.Focus();
               }
            }
        }

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            iAction = 0;
            panDetails.Enabled = true;
            tsbSave.Enabled = true;
            txtTitle1.Text = "";
            txtTitle2.Text = "";
            chkFlag.Checked = false;
            cmbGroup.SelectedValue = 0;
            txtTitle1.Focus();
        }
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            iAction = 1;
            panDetails.Enabled = true;
            tsbSave.Enabled = true;
            txtTitle1.Focus();
        }

        private void tsbExcel_Click(object sender, EventArgs e)
        {
            var EXL = new Microsoft.Office.Interop.Excel.Application();
            var oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            // System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-US")
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("el-GR");
            EXL.Workbooks.Add();
            EXL.ScreenUpdating = false;
            EXL.Caption = Global.AppTitle;
            EXL.Cells[1, 3].Value = "Λίστα";
            var loopTo = fgList.Rows.Count - 1;
            for (i = 0; i <= loopTo; i++)
            {
                EXL.Cells[i + 2, 1].Value = fgList[i, 0];
            }

            EXL.ScreenUpdating = true;
            EXL.Visible = true;
            System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
        }
        private void fgList_RowColChange(object sender, EventArgs e)
        {
            iAction = 1;
            panDetails.Enabled = false;
            tsbSave.Enabled = false;

            if (bCheckList)
                if (fgList.Row > 0)  ShowRecord();

        }
        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (txtTitle1.Text.Length != 0) {

                sTemp = "";

                if (iAction == 0) {                                      // 0 - ADD Mode
                    switch (iList_ID) {
                        case 21:                                        // Information Methods
                            sTemp = "INSERT INTO " + lblTableName.Text + " (Title, UseInvestIdees) VALUES ('" + txtTitle1.Text + "', '" + (chkFlag.Checked ? "1" : "0") + "')";
                            break;
                        case 25:                                        // 25-Finance Services
                            sTemp = "INSERT INTO " + lblTableName.Text + " (Title, TitleEng) VALUES ('" + txtTitle1.Text + "', '" + txtTitle2.Text + "')";
                            break;
                        default:                                        // misc simple lists
                            sTemp = "INSERT INTO " + lblTableName.Text + " (Title) VALUES ('" + txtTitle1.Text + "')";
                            break;
                    }
                }
                else  {
                    switch (iList_ID) {
                        case 21:             // Information Methods
                            sTemp = "UPDATE " + lblTableName.Text + " SET Title = '" + txtTitle1.Text + "', UseInvestIdees ='" + (chkFlag.Checked ? "1" : "0") + "' WHERE ID = " + fgList[fgList.Row, "ID"];
                            break;
                        case 25:             // 25-Finance Services
                            sTemp = "UPDATE " + lblTableName.Text + " SET Title = '" + txtTitle1.Text + "', TitleEng ='" + txtTitle2.Text + "' WHERE ID = " + fgList[fgList.Row, "ID"];
                            break;
                        default:           // misc simple lists
                            sTemp = "UPDATE " + lblTableName.Text + " SET Title = '" + txtTitle1.Text + "' WHERE ID = " + fgList[fgList.Row, "ID"];
                            break;
                    }
                }
                clsSystem System = new clsSystem();
                System.ExecSQL(sTemp);

                sTemp = txtTitle1.Text;
                DefineList(lblTableName.Text);
                txtTitle1.Text = sTemp;

                bCheckList = false;
                iAction = 1;

                tsbSave.Enabled = false;
                panDetails.Enabled = false;

                if (iCashTables_ID != 0)
                {
                    System = new clsSystem();
                    System.EditCashTables_LastEdit_Time(iCashTables_ID);
                }

                bCheckList = true;
                fgList.Row = 0;
                i = fgList.FindRow(txtTitle1.Text, 1, 0, false);                
                if (i > 0) fgList.Row = i;               
                fgList.Focus();
            }
        }        
    }
}
