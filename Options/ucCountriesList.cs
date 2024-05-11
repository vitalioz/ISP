using System;
using System.Data;
using System.Windows.Forms;
using Core;

namespace Options
{
    public partial class ucCountriesList : UserControl
    {
        int i, iID, iAction, iRightsLevel;
        bool bCheckList;
        clsCountries Countries = new clsCountries();
        clsCashTables CashTable = new clsCashTables();
        public ucCountriesList()
        {
            InitializeComponent();
        }
        private void ucCountriesList_Load(object sender, EventArgs e)
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

            //-------------- Define cmbCountriesGroups List ------------------
            cmbCountriesGroups.DataSource = Global.dtCountriesGroups.Copy();
            cmbCountriesGroups.DisplayMember = "Title";
            cmbCountriesGroups.ValueMember = "ID";

            DefineList();

            if (fgList.Rows.Count > 1)
            {
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

                Countries = new clsCountries();
                Countries.GetList();
                foreach (DataRow dtRow in Countries.List.Rows)
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
            Countries = new clsCountries();
            Countries.Record_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
            Countries.GetRecord();
            cmbType.SelectedIndex = Countries.Tipos;
            txtCode2.Text = Countries.Code;
            txtCode3.Text = Countries.Code3;
            txtTitle.Text = Countries.Title;
            txtTitle_MStar.Text = Countries.Title_MorningStar;
            txtTitleGreek.Text = Countries.Title_Greek;
            txtTitle_Alias.Text = Countries.Title_Alias;
            cmbCountriesGroups.SelectedValue = Countries.CountriesGroup_ID;
            cmbInvestGeography.SelectedIndex = Countries.InvestGeography_ID;
            txtPhoneCode.Text = Countries.PhoneCode;
        }
        private void tsbAdd_Click(object sender, EventArgs e)
        {
            iAction = 0;                                                            // 0 - ADD mode
            txtTitle.Text = "";
            txtCode2.Text = "";
            txtCode3.Text = "";
            txtTitle_MStar.Text = "1";
            cmbCountriesGroups.Text = "";
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
                    Countries = new clsCountries();
                    Countries.Record_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                    Countries.DeleteRecord();
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
                if (iAction == 0)
                {                                                       // 0 - ADD Mode
                    Countries = new clsCountries();
                    Countries.Tipos = Convert.ToInt16(cmbType.SelectedIndex);
                    Countries.Code = txtCode2.Text;
                    Countries.Code3 = txtCode3.Text;
                    Countries.Title = txtTitle.Text;
                    Countries.Title_MorningStar = txtTitle_MStar.Text;
                    Countries.Title_Greek = txtTitleGreek.Text;
                    Countries.Title_Alias = txtTitle_Alias.Text;
                    Countries.CountriesGroup_ID = Convert.ToInt32(cmbCountriesGroups.SelectedValue);
                    Countries.InvestGeography_ID = Convert.ToInt32(cmbInvestGeography.SelectedIndex);
                    Countries.PhoneCode = txtPhoneCode.Text;
                    iID = Countries.InsertRecord();
                }
                else
                {
                    iID = Convert.ToInt32(fgList[fgList.Row, "ID"]);

                    Countries = new clsCountries();
                    Countries.Record_ID = Convert.ToInt32(fgList[fgList.Row, "ID"]);
                    Countries.GetRecord();
                    Countries.Tipos = Convert.ToInt16(cmbType.SelectedIndex);
                    Countries.Code = txtCode2.Text;
                    Countries.Code3 = txtCode3.Text;
                    Countries.Title = txtTitle.Text;
                    Countries.Title_MorningStar = txtTitle_MStar.Text;
                    Countries.Title_Greek = txtTitleGreek.Text;
                    Countries.Title_Alias = txtTitle_Alias.Text;
                    Countries.CountriesGroup_ID = Convert.ToInt32(cmbCountriesGroups.SelectedValue);
                    Countries.InvestGeography_ID = Convert.ToInt32(cmbInvestGeography.SelectedIndex);
                    Countries.PhoneCode = txtPhoneCode.Text;
                    Countries.EditRecord();
                }
                panDetails.Enabled = false;
                DefineList();

                //--- edit LastEdit_Time and LastEdit_User_ID in ListsTables.ID = 26 - Countries Table ---------------------------
                CashTable = new clsCashTables();
                CashTable.Record_ID = 26;                                                    // ListsTables.ID = 26 - Countries
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
