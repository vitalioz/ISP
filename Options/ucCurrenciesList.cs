using System;
using System.Data;
using System.Windows.Forms;
using Core;

namespace Options
{
    public partial class ucCurrenciesList : UserControl
    {
        int i, iID,  iAction, iRightsLevel;
        bool bCheckList;
        clsCurrencies Currencies = new clsCurrencies();
        clsCashTables CashTable = new clsCashTables();
        public ucCurrenciesList()
        {
            InitializeComponent();
        }
        private void ucCurrenciesList_Load(object sender, EventArgs e)
        {   
            //------- fgList ----------------------------
            fgList.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
            fgList.Styles.ParseString("Focus{Font:Microsoft Sans Serif, 8.25pt, BackColor:LightSteelBlue; ForeColor:Black;} Highlight{Font:Microsoft Sans Serif, 8.25pt, BackColor:Transparent; ForeColor:Black;}");
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
            lblListItemTitle.Text = sParentListTitle;

            //-------------- Define Curr_Convert List ------------------
            cmbCurr_Convert.DataSource = Global.dtCurrencies.Copy();
            cmbCurr_Convert.DisplayMember = "Title";
            cmbCurr_Convert.ValueMember = "ID";

            DefineList();

            if (fgList.Rows.Count > 1) {
                fgList.Row = 1;
                ShowRecord();
            }
            if (iRightsLevel == 1) toolLeft.Enabled = false;
        }
        private void DefineList()
        {
            try
            {
                fgList.Redraw = false;
                fgList.Rows.Count = 1;

                Currencies = new clsCurrencies();
                Currencies.GetList();
                foreach (DataRow dtRow in Currencies.List.Rows)
                    if (Convert.ToInt32(dtRow["ID"]) != 0)
                        fgList.AddItem(dtRow["Title"] + "\t" + dtRow["ID"]); 

                fgList.Redraw = true;
                bCheckList = true;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation); }
            finally { }
        }
        private void fgList_RowColChange(object sender, EventArgs e)
        {
            iAction = 1;                                                            // 1 - EDIT Mode
            panDetails.Enabled = false;
            tsbSave.Enabled = false;

            if (bCheckList)
                if (fgList.Row > 0) ShowRecord();
        }
        private void ShowRecord()
        {
            Currencies = new clsCurrencies();
            Currencies.Record_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
            Currencies.GetRecord();
            txtTitle.Text = Currencies.Title;
            txtCode.Text = Currencies.Code;
            txtCode_MStar.Text = Currencies.Code_MorningStar;
            txtKoef.Text = Currencies.Koef.ToString();
            cmbCurr_Convert.Text = Currencies.Code_Convert;
        } 
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            iAction = 0;                                                            // 0 - ADD mode
            txtTitle.Text = "";
            txtCode.Text = "";
            txtCode_MStar.Text = "";
            txtKoef.Text = "1";
            cmbCurr_Convert.Text = "";
            panDetails.Enabled = true;
            tsbSave.Enabled = true;
            txtTitle.Focus();
        }
        private void tsbEdit_Click(object sender, EventArgs e)
        {
            iAction = 1;                                                            // 1 - EDIT Mode
            panDetails.Enabled = true;
            tsbSave.Enabled = true;
            txtTitle.Focus();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            if (fgList.Row > 0)

                if (MessageBox.Show("ΠΡΟΣΟΧΗ! Ζητήσατε να διαγραφεί η εγγραφή.\nΕίστε σίγουρος για τη διαγραφή της;", Global.AppTitle, MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    Currencies = new clsCurrencies();
                    Currencies.Record_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                    Currencies.DeleteRecord();
                    fgList.RemoveItem(fgList.Row);

                    if (fgList.Rows.Count > 1)
                    {
                        fgList.Focus();
                        iID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                    }
                    fgList.Redraw = true;

                    iAction = 1;                                                          // 1 - EDIT Mode
                    ShowRecord();
                }
        } 
        private void tsbSave_Click(object sender, EventArgs e)
        {
            if (txtTitle.Text.Length != 0)
            {
                if (iAction == 0) {                                                       // 0 - ADD Mode
                    Currencies = new clsCurrencies();
                    Currencies.Title = txtTitle.Text;
                    Currencies.Code = txtCode.Text;
                    Currencies.Code_MorningStar = txtCode_MStar.Text;
                    Currencies.Koef = Convert.ToSingle(txtKoef.Text);
                    Currencies.Code_Convert = cmbCurr_Convert.Text;
                    iID = Currencies.InsertRecord();
                }
                else  {
                    iID = Convert.ToInt32(fgList[fgList.Row, "ID"]);

                    Currencies = new clsCurrencies();
                    Currencies.Record_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                    Currencies.GetRecord();
                    Currencies.Title = txtTitle.Text;
                    Currencies.Code = txtCode.Text;
                    Currencies.Code_MorningStar = txtCode_MStar.Text;
                    Currencies.Koef = Convert.ToSingle(txtKoef.Text);
                    Currencies.Code_Convert = cmbCurr_Convert.Text;
                    Currencies.EditRecord(); 
                }
                panDetails.Enabled = false;
                DefineList();

                //--- edit LastEdit_Time and LastEdit_User_ID in ListsTables.ID = 26 - Currencies Table ---------------------------
                CashTable = new clsCashTables();
                CashTable.Record_ID = 26;                                                    // ListsTables.ID = 26 - Currencies
                CashTable.GetRecord();
                CashTable.LastEdit_Time = DateTime.Now;
                CashTable.LastEdit_User_ID = Global.User_ID;
                CashTable.EditRecord();
                //-----------------------------------------------------------------------------------------------------------------

                i = fgList.FindRow(iID.ToString(), 1, 1, false);
                if (i > 0) fgList.Row = i;
                else fgList.Row = 1;

                ShowRecord();
            }
            else MessageBox.Show("Η εισαγωγή του τίτλου είναι υποχρεωτική", Global.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        public int RightsLevel { get { return this.iRightsLevel; } set { this.iRightsLevel = value; } }
    }
}
